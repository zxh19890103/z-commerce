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
using Nt.Model.Common;

public partial class netin_product_list : ListBase<View_Product, Product>
{

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
                _categories = DB.GetDropdownlist<ProductCategory>("Display=1");
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
                "LanguageId={0} and Deleted=0",
                NtContext.Current.LanguageID);

            if (!User.IsAdmin)
                where += string.Format(" and UserId={0}", User.Id);

            string seProductId = Request.QueryString["se.product.id"],
                     seProductCategory = Request.QueryString["se.product.category"],
                     seProductName = Request.QueryString["se.product.name"],
                     seProductCreatedDateStart = Request.QueryString["se.product.createddate.start"],
                     seProductCreatedDateEnd = Request.QueryString["se.product.createddate.end"];

            int id = 0;
            if (int.TryParse(seProductId, out id))
            {
                where += " And Id='" + id + "'";
            }
            else
            {
                if (!string.IsNullOrEmpty(seProductName))
                {
                    where += " And [Name] like '%" + seProductName + "%'";
                }

                int productCategory = 0;
                if (int.TryParse(seProductCategory, out productCategory)
                    && productCategory > 0)
                {
                    where += " And CategoryCrumbs like '%," + productCategory + ",%'";
                }

                DateTime start, end;
                if (DateTime.TryParse(seProductCreatedDateStart, out start))
                {
                    where += " And datediff(d,'" + start + "',CreatedDate)>=0";
                }

                if (DateTime.TryParse(seProductCreatedDateEnd, out end))
                {
                    where += " And datediff(d,'" + end + "',CreatedDate)<=0";
                }
            }
            return where;
        }
    }


    public override void List()
    {
        SetUsePagerOn();

        BeginTable("选择", "图片", "排序", "分类", "产品名", "生效", "置顶", "推荐", "新品", "复制", "操作");

        foreach (var item in DataSource)
        {
            Row(() =>
            {
                TdKey(item.Id);
                TdImage(MediaService.MakeThumbnail(item.PictureUrl, 80, 80));
                TdOrder(item.Id, item.DisplayOrder);
                Td(item.CategoryName);
                Td(NtUtility.GetSubString(item.Name, 25));
                TdSetBoolean(item.Id, item.Display, "Display");
                TdSetBoolean(item.Id, item.SetTop, "SetTop");
                TdSetBoolean(item.Id, item.Recommended, "Recommended");
                TdSetBoolean(item.Id, item.IsNew, "IsNew");
                Html("<td><a href=\"javascript:;\" onclick=\"copy({0},1nt.reload);\" class=\"copy\"></a></td>", item.Id);
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
            TdPager(7);
            TdDelSelected(true);
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "产品列表";
    }

    protected override string OrderBy
    {
        get
        {
            return "DisplayOrder desc,CreatedDate desc,ID";
        }
    }
}