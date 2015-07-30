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

public partial class netin_sale_wishlist : ListBase
{

    public override string TableName
    {
        get { return "Customer_Wishlist"; }
    }

    public override void List()
    {
        string filter = string.Empty,
            orderby = string.Empty;
        int total = DbAccessor.GetRecordCount(typeof(Customer_Wishlist), filter);
        Pager = new NtPager(total, 12);
        var source = DbAccessor.GetList<View_Customer_Wishlist>(filter, orderby, Pager.PageIndex, Pager.PageSize);

        BeginTable("选择", "商品", "会员", "操作");
        foreach (var item in source)
        {
            Row <View_Customer_Wishlist>(e =>
            {
                TdKey(e.Id);
                TdF("<a href=\"goods_edit.aspx?id={0}\">{1}</a>", item.GoodsId, item.GoodsName);
                TdF("<a href=\"javascript:;\" onclick=\"openWindow('../customer/info_view.aspx?id={0}');\">{1}</a>", item.CustomerId, item.CustomerName);
                TdEdit(item.Id);
            }, item);
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdSpan(2);
            TdDelSelected();
        });
    }

    public override string EditPagePath
    {
        get { return Request.RawUrl; }
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "收藏夹";
    }

}