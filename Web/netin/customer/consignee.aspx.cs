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

public partial class netin_customer_consignee : ListBase
{

    public override string TableName
    {
        get { return "Customer_Consignee"; }
    }

    public override void List()
    {
        int customerid = IMPOSSIBLE;
        string filter = string.Empty;
        if (int.TryParse(Request.QueryString["customerid"], out customerid))
            filter = string.Format("CustomerId=" + customerid);
        var source = DbAccessor.GetList<Customer_Consignee>(filter);

        BeginTable("选择", "姓名", "固话", "手机", "邮箱", "住址", "邮编", "操作");

        foreach (var item in source)
        {
            Row<Customer_Consignee>(e =>
            {
                TdKey(item.Id);
                Td(item.Name);
                Td(item.Phone);
                Td(item.Mobile);
                Td(item.Email);
                Td(item.Address);
                Td(item.Zip);
                Td(() =>
                {
                    EditRowAnchor(item.Id, "&customerId=" + item.CustomerId);
                    DelRowAnchor(item.Id);
                });
            }, item);
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdSpan(6);
            Td(() =>
            {
                if (customerid != IMPOSSIBLE)
                    Html("<a href=\"consignee_edit.aspx?customerid={0}\" class=\"a-button\">添加</a>", customerid);
                Html("<a href=\"javascript:;\" class=\"a-button\" onclick=\"delSelected();\">删除所选</a>");
            });
        });
    }

    public override string EditPagePath
    {
        get { return "consignee_edit.aspx"; }
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "收货人信息";
    }
}