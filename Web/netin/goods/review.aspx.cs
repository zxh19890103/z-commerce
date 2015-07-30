using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.BLL;
using Nt.DAL;
using Nt.Model;
using Nt.Framework;
using Nt.Framework.Admin;
using Nt.Model.View;

public partial class netin_goods_review : ListBase
{

    public override string TableName
    {
        get { return "Goods_Review"; }
    }

    public override void List()
    {
        string filter = string.Empty;
        string orderby = "CreatedDate desc";
        int total = DbAccessor.GetRecordCount("Goods_Review", filter);
        Pager = new NtPager(total, 12);
        var source = DbAccessor.GetList<View_Goods_Review>(filter, orderby, Pager.PageIndex, Pager.PageSize);

        BeginTable("选择", "商品", "会员", "标题", "内容", "开放", "点击率", "评论日期", "操作");

        foreach (var item in source)
        {
            Row<View_Goods_Review>(e =>
            {
                TdKey(item.Id);
                TdF("<a href=\"goods_edit.aspx?id={0}\">{1}</a>", item.GoodsId, item.GoodsName);
                TdF("<a href=\"javascript:;\" onclick=\"openWindow('../customer/info_view.aspx?id={0}');\">{1}</a>", item.CustomerId, item.CustomerName);
                Td(item.Title);
                Td(NtUtility.GetSubString(item.Body, 25));
                TdSetBoolean(item.Id, item.IsApproved, "IsApproved");
                Td(item.Rating);
                Td(item.CreatedDate);
                TdEdit(item.Id);
            }, item);

        }

        EndTable(() =>
        {
            TdSelectAll();
            TdPager(7);
            TdDelSelected();
        });
    }

    public override string EditPagePath
    {
        get { return "review_edit.aspx"; }
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "商品评论";
    }

}