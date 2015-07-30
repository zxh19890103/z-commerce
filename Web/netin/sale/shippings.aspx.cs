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
using Nt.Model.Enum;

public partial class netin_sale_shippings : ListBase<View_Order, Order>
{
    public override void List()
    {
        BeginTable("联系人", "送货地址", "固话", "手机", "邮箱", "邮编", "货物详情");
        foreach (var item in DataSource)
        {
            Row(() =>
            {
                Td(item.Name);
                Td(item.Address);
                Td(item.Mobile);
                Td(item.Phone);
                Td(item.Email);
                Td(item.Zip);
                TdF("<a href=\"javascript:;\" onclick=\"openWindow({{url:'goodsInfo.aspx?orderid={0}',width:600,height:400}});\">查看货物详情</a>", item.Id);
            });
        }
        EndTable();
    }

    protected override string Where
    {
        get
        {
            return "ShippingStatus=" + (int)ShippingStatus.NotYetShipped;
        }
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "发货单";
    }

}