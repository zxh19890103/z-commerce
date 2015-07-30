using Nt.Framework.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Netin_notauthorized : PageBase
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Title = "您没有权限访问此页面";
    }
}