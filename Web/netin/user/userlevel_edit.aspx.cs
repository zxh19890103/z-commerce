﻿using System;
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

public partial class netin_user_userlevel_edit : EditBase<UserLevel>
{
    [AjaxMethod]
    public override void Post()
    {
        AjaxSave<UserLevel>();
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "用户组";
    }
}