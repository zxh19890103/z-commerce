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

public partial class netin_common_language_config : EditBase<Language>
{
    [AjaxMethod]
    public override void Post()
    {
        AjaxSave<Language>();
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "语言信息";
    }

    public override string PermissionSysN
    {
        get
        {
            return "netin.common.language";
        }
    }
}