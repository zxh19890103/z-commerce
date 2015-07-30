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

public partial class netin_sale_shoppingcart : ListBase<View_ShoppingCart, ShoppingCartItem>
{
    public override void List()
    {
        SetUsePagerOn();
        BeginTable("选择", "商品", "会员", "数量","查看", "操作");
        foreach (var item in DataSource)
        {
            Row(() =>
            {
                TdKey(item.Id);
                TdF("<a href=\"../goods/edit.aspx?id={0}\">{1}</a>",
                    item.GoodsId, item.GoodsName);
                TdF("<a href=\"javascript:;\" onclick=\"openWindow('../customer/info_view.aspx?id={0}');\">会员:{1}</a>",
                    item.CustomerId, item.CustomerName);
                Td(item.Quantity);
                Html("<td>");
                Html("<a href=\"shoppingcart_info.aspx?id=");
                Response.Write(item.Id);
                Html("\" target=\"_blank\" class=\"detail\"></a>");
                Html("</td>");
                Html("<td>");
                DelRowAnchor(item.Id);
                Html("</td>");
            });
        }
        EndTable(() =>
        {
            TdSelectAll();
            TdPager(4);
            TdDelSelected();
        });
    }

    protected override string OrderBy
    {
        get
        {
            return "CustomerId,CreatedDate desc";
        }
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "购物车";
    }

}