using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.Framework;
using Nt.BLL;
using Nt.Model;
using Nt.Model.Common;
using Nt.Model.NtAttribute;
using Nt.Model.Setting;
using Nt.Model.View;
using Nt.DAL;

public partial class user_pay : BaseAspx
{
    protected int id;
    View_Order m;
    public View_Order Model { get { return m; } }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        InCustomerArea = true;
        var c = NtContext.Current.CurrentCustomer;
        m = DbAccessor.GetFirstOrDefault<View_Order>("Status=10", "CreatedDate desc");
        if (m == null)
            Goto("index.aspx", "订单未找到", 3);
    }

}