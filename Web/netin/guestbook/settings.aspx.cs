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

public partial class netin_guestbook_settings : SettingBase<GuestBookSetting>
{
    protected override void AfterPost()
    {

    }

    protected override void AfterGet()
    {
        Title = "留言设置";
    }
}