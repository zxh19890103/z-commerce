using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_index : BaseAspx
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Response.Redirect("/", true);
    }
}