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

public partial class netin_sale_shippingmethod_config : EditBase<ShippingMethod>
{
    [AjaxMethod]
    public override void Post()
    {
        AjaxSave<ShippingMethod>();
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "配送方式";
    }

    public override string PermissionSysN
    {
        get
        {
            return "netin.sale.shippingmethod";
        }
    }

}