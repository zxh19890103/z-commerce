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

public partial class netin_db_sqleditor : PageBase
{
    protected void SqlFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pathOnDisk = MapPath(string.Format("/app_data/script/{0}.sql", SqlFiles.SelectedValue));
        SqlText.Text = File.ReadAllText(pathOnDisk);
    }

    protected void SaveSqlButton_Click(object sender, EventArgs e)
    {
        string pathOnDisk = MapPath(string.Format("/app_data/script/{0}.sql", SqlFiles.SelectedValue));
        string newText = SqlText.Text;
        File.WriteAllText(pathOnDisk, newText);
        Msg.Text = "恭喜!保存成功!";
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Title = "SQL脚本文件编辑器";
    }
}