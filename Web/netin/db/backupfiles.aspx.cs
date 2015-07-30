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

public partial class netin_db_backupfiles : PageBase
{
    /// <summary>
    /// 输出文件列表
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="style">样式class</param>
    /// <param name="searchPattern">搜索模式</param>
    protected void RenderBKFilesList(string title, string style, string searchPattern)
    {
        Html("<h4>{0}</h4>", title);
        Html("<ol class=\"{0}\">", style);
        int i = 0;
        string filename = "";
        DirectoryInfo dir = new DirectoryInfo(MapPath("/app_data/backup/"));
        foreach (var file in dir.GetFiles(searchPattern).OrderByDescending(x => x.LastWriteTime))
        {
            filename = file.Name;
            Html("<li id=\"id_{0}\"><span>", i);
            Html(filename);
            Html("</span><a href=\"javascript:;\" onclick=\"downloadBackup('{0}');\">下载</a>", filename);
            Html("<a href=\"javascript:;\" onclick=\"delBackup({1},'{0}');\">删除</a>", filename, i);
            Html("<i>{0}</i>", file.LastWriteTime.ToString("yyyy/MM/dd hh:mm:ss"));
            Html("</li>");
            i++;
        }
        Html("</ol>");
    }

    /// <summary>
    /// 删除备份文件
    /// </summary>
    [AjaxMethod]
    public void DelBackup()
    {
        string filename = "";
        NtJson.EnsureNotNullOrEmpty("filename", "参数错误:filename", out filename);
        string pathOnDisk = MapPath("/app_data/backup/" + filename);
        if (!File.Exists(pathOnDisk))
            NtJson.ShowError("文件不存在!");
        File.Delete(pathOnDisk);
        NtJson.ShowOK();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Title = "数据库备份文件管理";
    }

}