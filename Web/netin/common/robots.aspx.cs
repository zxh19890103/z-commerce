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
using Nt.Model.Enum;

public partial class netin_common_robots : PageBase
{
    protected RobotsHelper rh;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        Title = "爬虫协议";

        rh = new RobotsHelper();

        if (IsHttpPost)
        {
            rh.PrepareReWriteRobotsFile(Request.Form["User-agent"]);
            rh.AppendBlock(Request.Form["RobotsBlock"]);
            rh.CompleteReWriteRobotsFile(Request.Form["Sitemap"]);
            Goto("robots.aspx", "恭喜!Robots.txt文件保存成功!");
        }
        else
        {
            rh.Fetch();
        }
    }

}