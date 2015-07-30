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

public partial class netin_sale_campaign_edit : EditBase<Campaign>
{
    public override void Post()
    {
        Campaign m = new Campaign();
        m.InitDataFromPage();
        DbAccessor.UpdateOrInsert(m);
        ExecuteJS("保存成功!", "window.opener.refreshList('list','List',false);window.close();", 3);
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "推销活动";
    }

    public override string PermissionSysN
    {
        get
        {
            return "netin.sale.campaign";
        }
    }

}