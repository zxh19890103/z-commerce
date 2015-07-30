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

public partial class netin_sale_paymentmethod : ListBase
{

    public int id = IMPOSSIBLE;
    public PaymentMethod Model { get; set; }


    public override string TableName
    {
        get { return "PaymentMethod"; }
    }

    public override string EditPagePath
    {
        get { return "paymentmethod_config.aspx"; }
    }

    [AjaxMethod]
    public override void List()
    {
        List<PaymentMethod> source = DbAccessor.GetList<PaymentMethod>("", "DisplayOrder");
        BeginTable("选择", "ID", "排序", "支付方式名称", "操作");

        foreach (var item in source)
        {
            Row<PaymentMethod>(e =>
            {
                TdKey(item.Id);
                Td(item.Id);
                TdOrder(item.Id, item.DisplayOrder);
                Td(item.Name);
                Td(() =>
                {
                    Anchor("javascript:openWindow('paymentmethod_config.aspx?id=" + item.Id + "');", "", "edit");
                    DelRowAnchor(item.Id);
                });
            }, item);
        }
        EndTable(() =>
        {
            TdSelectAll();
            Td();
            TdReOrder();
            Td();
            Td(() =>
            {
                Html("<a href=\"javascript:openWindow('paymentmethod_config.aspx');\" class=\"a-button\">添加</a>");
                Html("<a href=\"javascript:;\" onclick=\"delSelected();\" class=\"a-button\">删除所选</a>");
            });
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "支付方式管理";
    }

}