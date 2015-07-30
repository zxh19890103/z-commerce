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
using System.Text;

public partial class netin_db_backup : PageBase
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Title = "备份数据库";
    }

    [AjaxMethod]
    public void RenderTablesInfo()
    {
        Html("<h4>选择需要备份的数据表：</h4>");
        Html("<input type=\"checkbox\" id=\"select_all_tables\" onclick=\"selectAllTables();\" /><label for=\"select_all_tables\">全选</label>");
        Html("<br />");
        Html("<span id=\"tables\">");
        //select * from sysobjects where xtype=N'U'  //all tables
        using (SqlDataReader rs = SqlHelper.ExecuteReader("select [id],[name] from sysobjects where type=N'U'"))
        {
            int i = 0;
            while (rs.Read())
            {
                Html("<input type=\"checkbox\" name=\"tables\" data-text=\"{0}\" id=\"tables_{1}\" value=\"{2}\"/><label for=\"tables_{1}\">{0}</label><br/>", rs[1], i, rs[0]);
                i++;
            }
        }
        Html("</span>");
        Html("<hr />");
    }

    [AjaxMethod]
    public void Backup()
    {
        int type = 0;
        NtJson.EnsureNumber("type", "参数错误:type", out type);

        NtJson json = new NtJson();
        json.ErrorCode = 0;
        json.Json["message"] = "数据库备份成功,您可以下载备份文件!";

        if (type == 0)
        {
            //以脚本形式备份
            string tables = "";
            string tables_id = "";
            NtJson.EnsureStrArray("tables", out tables);
            NtJson.EnsureStrArray("tables_id", out tables_id);

            string[] tablesArr = tables.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] tablesIdArr = tables_id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (tablesArr.Length != tablesIdArr.Length)
                NtJson.ShowError("表名组和表id组数目不等!");

            string dir = "/app_data/backup/";
            string dirPathOnDisk = MapPath(dir);
            if (!Directory.Exists(dirPathOnDisk))
                Directory.CreateDirectory(dirPathOnDisk);

            string fileN = "backup_" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + ".sql";
            int cols = 0;
            StreamWriter sw = File.CreateText(dirPathOnDisk + fileN);

            try
            {
                using (SqlConnection conn = SqlHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandTimeout = 60;
                    cmd.CommandType = CommandType.Text;

                    for (int i = 0; i < tablesArr.Length; i++)
                    {
                        cols = 0;
                        string insertSqlLeftPart = "insert into [" + tablesArr[i] + "](";
                        cmd.CommandText = "select [name] from syscolumns where id=" + tablesIdArr[i];//处理列信息

                        SqlDataReader rs0 = cmd.ExecuteReader();
                        while (rs0.Read())
                        {
                            if (cols > 0)
                                insertSqlLeftPart += ",[" + rs0.GetString(0) + "]";
                            else
                                insertSqlLeftPart += "[" + rs0.GetString(0) + "]";
                            cols++;
                        }
                        rs0.Close();
                        insertSqlLeftPart += ")values(";
                        
                        cmd.CommandText = "select * from " + tablesArr[i];//处理列信息

                        SqlDataReader rs1 = cmd.ExecuteReader();
                        sw.WriteLine("SET IDENTITY_INSERT [" + tablesArr[i] + "] ON");
                        while (rs1.Read())
                        {
                            sw.Write(insertSqlLeftPart);
                            for (int j = 0; j < cols; j++)
                            {
                                if (j > 0)
                                    sw.Write(",");
                                sw.Write("N'");
                                sw.Write(rs1[j].ToString().Replace('\'','’'));//脚本中的英文引号不可以在内容文本中出现，将其替换为中文引号
                                sw.Write("'");
                            }
                            sw.WriteLine(")");
                        }
                        rs1.Close();
                        sw.WriteLine("SET IDENTITY_INSERT [" + tablesArr[i] + "] OFF");
                        sw.WriteLine();
                    }
                }
                json.Json["link"] = fileN;
            }
            catch (Exception ex)
            {
                json.ErrorCode = 1;
                json.Json["message"] = ex.Message;
                Logger.Log(ex.Message);
            }
            finally
            {
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }
        else
        {
            try
            {
                //以文件形式备份
                string fileN = "backup_" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + ".bak";
                string sql = "backup database [" + NtContext.Current.DataBase + "] to disk=N'" + MapPath("/app_data/backup/") + fileN + "';";
                SqlHelper.ExecuteNonQuery(sql);
                json.Json["link"] = fileN;
            }
            catch (Exception ex)
            {
                json.ErrorCode = 1;
                json.Json["message"] = ex.Message;
            }
        }

        Response.Write(json);
    }

    [AjaxMethod]
    public void ReWriteSql()
    {
        NtJson json = new NtJson();
        json.ErrorCode = NtJson.OK;
        json.Message = "恭喜，脚本文件生成成功!您可以下载脚本文件";
        try
        {
            string fileN = "sql_" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + ".sql";
            Class2SqlGenerator gen = new Class2SqlGenerator("/app_data/backup/" + fileN);
            gen.Run();
            json.Json["link"] = fileN;
        }
        catch (Exception ex)
        {
            json.ErrorCode = NtJson.ERROR;
            json.Message = ex.Message;
        }

        Response.Write(json);
    }
}