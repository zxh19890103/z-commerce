using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.Model;
using Nt.Model.Common;
using Nt.Model.Setting;
using Nt.Model.View;
using Nt.Framework;
using Nt.Framework.Admin;
using Nt.DAL;
using Nt.BLL;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class netin_db_execute : PageBase
{

    DataSet queryTables;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Title = "数据库执行";
    }

    protected void RenderTables()
    {
        if (queryTables != null)
        {
            foreach (DataTable data in queryTables.Tables)
            {
                if (data == null) return;

                Html("<h3>{0}</h3>", data.TableName);

                Html("<table class=\"adminListView\">");
                Html("<thead>");
                Html("<tr>");
                foreach (DataColumn col in data.Columns)
                {
                    Html("<th>");
                    Html(col.ColumnName);
                    Html("</th>");
                }
                Html("</tr>");
                Html("</thead>");
                int cols = data.Columns.Count;
                foreach (DataRow r in data.Rows)
                {
                    Row(() =>
                    {
                        for (int i = 0; i < cols; i++)
                        {
                            Td(r[i]);
                        }
                    });
                }

                EndTable();

                Html("<hr/>");
            }
        }
    }

    protected void ExeButton_Click(object sender, EventArgs e)
    {
        string sql = SqlBox.Text;
        if (string.IsNullOrEmpty(sql))
        {
            Msg.Text = "脚本不能为空!";
        }
        else
        {
            try
            {
                using (SqlConnection conn = SqlHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandTimeout = 60;
                    cmd.CommandType = CommandType.Text;
                    if (IsQuery.Checked)
                    {
                        cmd.CommandText = sql;
                        queryTables = new DataSet();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(queryTables);
                    }
                    else
                    {
                        //注意：go[Go、gO、GO、]的前一个字符必须是换行
                        foreach (string block in
                            sql.Split(new string[] { "\r\ngo", "\r\nGO", "\r\nGo", "\r\ngO" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            cmd.CommandText = block;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    Msg.Text = "脚本执行成功!";
                }
            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message;
            }
        }
    }

    protected void ReCreateProcsButton_Click(object sender, EventArgs e)
    {
        InstallService iis = new InstallService();
        iis.ExecuteScriptOnFile("proc", false);
        Msg.Text = "存储过程执行成功!";
    }

    protected void reCreateMenuButton_Click(object sender, EventArgs e)
    {
        InstallService iis = new InstallService();
        iis.ExecuteScriptOnFile("menu", false);
        Msg.Text = "后台目录执行成功!";
    }

}