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

public partial class netin_html_htmlDetailPages : PageBase
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Title = "详细页静态化";
    }

    [AjaxMethod]
    public void Htmlize()
    {
        int flag;
        NtJson.EnsureNumber("flag", "参数错误:flag!", out flag);
        switch (flag)
        {
            case 1:
                ThreadManager.Instance.DetailPagesHtmlize<View_Article>();
                break;
            case 2:
                ThreadManager.Instance.DetailPagesHtmlize<View_Product>();
                break;
            case 3:
                ThreadManager.Instance.DetailPagesHtmlize<View_Goods>();
                break;
            case 4:
                ThreadManager.Instance.DetailPagesHtmlize<SinglePage>();
                break;
            default:
                break;
        }
        NtJson.ShowOK("正在生成静态页！");
    }
}