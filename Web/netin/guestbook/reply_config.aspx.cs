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

public partial class netin_guestbook_reply_config : EditBase<GuestBookReply>
{
    protected int book = IMPOSSIBLE;

    [AjaxMethod]
    public override void Post()
    {
        AjaxSave<GuestBookReply>();
    }

    protected override void Prepare()
    {
        base.Prepare();
        int.TryParse(Request.QueryString["book"], out book);
        if (book == IMPOSSIBLE)
            CloseWindow("参数错误:book!");
        Title = "留言回复";
    }
}