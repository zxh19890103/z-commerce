using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Framework.Admin;
using Nt.Model;
using Nt.Model.Common;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Netin_Goods_goods_list : ListBase
{
    public override string TableName
    {
        get { return "Goods"; }
    }

    public override string EditPagePath
    {
        get { return "goods_edit.aspx"; }
    }

    List<NtListItem> _categories;
    /// <summary>
    /// 商品分类选项
    /// </summary>
    public List<NtListItem> Categories
    {
        get
        {
            if (_categories == null)
            {
                _categories = DB.GetDropdownlist<Goods_Class>("Display=1");
            }
            return _categories;
        }
    }

    /// <summary>
    /// 搜索条件
    /// </summary>
    protected override string Where
    {
        get
        {
            string where = string.Format(
                "LanguageId={0}  and Deleted=0 and Removed=0",
                NtContext.Current.LanguageID);

            if (!User.IsAdmin)
                where += string.Format(" and UserId={0}", User.Id);

            string seGoodsGuid = Request.QueryString["se.goods.goodsguid"],
                     seGoodsClass = Request.QueryString["se.goods.class"],
                     seGoodsId = Request.QueryString["se.goods.id"],
                     seGoodsName = Request.QueryString["se.goods.name"],
                     seGoodsCreatedDateStart = Request.QueryString["se.goods.createddate.start"],
                     seGoodsCreatedDateEnd = Request.QueryString["se.goods.createddate.end"];

            if (!string.IsNullOrEmpty(seGoodsGuid))
            {
                where += " And GoodsGuid='" + seGoodsGuid + "'";
            }
            else
            {
                int id = 0;
                if (int.TryParse(seGoodsId, out id))
                {
                    where += " And Id='" + id + "'";
                }
                else
                {
                    if (!string.IsNullOrEmpty(seGoodsName))
                    {
                        where += " And [Name] like '%" + seGoodsName + "%'";
                    }

                    int goodsClass = 0;
                    if (int.TryParse(seGoodsClass, out goodsClass)
                        && goodsClass > 0)
                    {
                        where += " And ClassCrumbs like '%," + goodsClass + ",%'";
                    }

                    DateTime start, end;
                    if (DateTime.TryParse(seGoodsCreatedDateStart, out start))
                    {
                        where += " And datediff(d,'" + start + "',CreatedDate)>=0";
                    }

                    if (DateTime.TryParse(seGoodsCreatedDateEnd, out end))
                    {
                        where += " And datediff(d,'" + end + "',CreatedDate)<=0";
                    }
                }
            }
            return where;
        }
    }

    public override void List()
    {
        string orderby = "DisplayOrder desc,CreatedDate desc,ID desc";

        Pager = new NtPager();
        Pager.PageSize = 12;
        Pager.PagerItemCount = 5;
        List<View_Goods> source = null;
        source = DbAccessor.GetList<View_Goods>(Where, orderby, Pager.PageIndex, Pager.PageSize);
        Pager.RecordCount = DbAccessor.Total;

        BeginTable("选择", "图片", "排序", "分类", "商品名", "生效", "置顶", "推荐", "新品", "下架", "复制", "操作");

        foreach (var item in source)
        {
            Row<View_Goods>(e =>
            {
                TdKey(item.Id);
                TdImage(MediaService.MakeThumbnail(item.PictureUrl, 80, 80));
                TdOrder(item.Id, item.DisplayOrder);
                Td(item.ClassName);
                Td(NtUtility.GetSubString(item.Name, 25));
                TdSetBoolean(item.Id, item.Display, "Display");
                TdSetBoolean(item.Id, item.SetTop, "SetTop");
                TdSetBoolean(item.Id, item.Recommended, "Recommended");
                TdSetBoolean(item.Id, item.IsNew, "IsNew");
                Html(TAG_TD_BEGIN);
                Anchor("putOnOrOffSale(" + item.Id + ");", "", "nt-offsale");
                Html(TAG_TD_END);
                Html("<td><a href=\"javascript:;\" onclick=\"copy({0},0,nt.reload);\" class=\"copy\"></a></td>", item.Id);
                Html("<td>");
                EditRowAnchor(item.Id);
                DelRowAnchor(true, item.Id);
                Html("</td>");
            }, item);

        }

        EndTable(() =>
        {
            TdSelectAll();
            Td();
            TdReOrder();
            TdPager(8);
            Html(TAG_TD_BEGIN);
            AButton("putOnOrOffSale();", "下架选择项");
            DelSelectedButton(true);
            Html(TAG_TD_END);
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "商品列表";
    }
}