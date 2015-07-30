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

public partial class netin_customer_consignee_edit : EditBase
{
    public Customer_Consignee Model { get; set; }
    public int customerId = IMPOSSIBLE;

    public override string TableName
    {
        get { return "Customer_Consignee"; }
    }

    public override string ListPagePath
    {
        get { return "consignee.aspx"; }
    }

    public override string PermissionSysN
    {
        get
        {
            return "netin.customer.consignee";
        }
    }

    public override void Get()
    {
        if (!Int32.TryParse(Request.QueryString["customerId"], out customerId))
            Goto(ListPagePath, "参数错误:customerId");

        if (IsEdit)
        {
            Model = DbAccessor.GetById<Customer_Consignee>(NtID);
            if (Model == null)
                NotFound();
        }
        else
        {
            Model = new Customer_Consignee();
            Model.CustomerId = customerId;
        }
    }

    public override void Post()
    {
        Customer_Consignee m = new Customer_Consignee();
        m.InitDataFromPage();
        if (m.Id > 0)
            DbAccessor.Update(m);
        else
            DbAccessor.Insert(m);
        Goto("consignee.aspx?customerId=" + m.CustomerId, "保存成功!");
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "收货人";
    }
}