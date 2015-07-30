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
using System.IO;
using System.Threading;

public partial class netin_html_htmlIndexPage : PageBase
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Title = "首页静态化";
    }

    [AjaxMethod]
    public void SaveConfig()
    {
        string cxt = Request["cxt"];
        File.WriteAllText(MapPath("indexfiles.config"), cxt);
        NtJson.ShowOK("文件已经保存.");
    }

    [AjaxMethod]
    public void GetConfig()
    {
        Response.Write(File.ReadAllText(MapPath("indexfiles.config")));
    }

    [AjaxMethod]
    public void Htmlize()
    {
        ThreadManager f = new ThreadManager();
        f.IndexPagesHtmlize();
        NtJson.ShowOK("正在生成静态页！");
    }
}