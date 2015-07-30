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

public partial class netin_sale_goodsInfo : ListBase<View_OrderItem, OrderItem>
{
    int orderId = 0;

    public override void List()
    {
        BeginTable("货号", "商品名", "单价(RMB)", "数量");

        foreach (var item in DataSource)
        {
            Row(() =>
            {
                Td(item.GoodsGuid);
                Td(item.GoodsName);
                Td(item.GoodsPrice.ToString("f"));
                Td(item.Quantity);
            });
        }

        EndTable();
    }

    protected override string Where
    {
        get
        {
            return "OrderId=" + orderId;
        }
    }

    protected override void Prepare()
    {
        base.Prepare();
        if (!int.TryParse(Request["orderId"], out orderId))
            CloseWindow("参数错误:orderId");
        Title = "货物详情";
    }

    public override string PermissionSysN
    {
        get
        {
            return "netin.sale.shippings";
        }
    }

}