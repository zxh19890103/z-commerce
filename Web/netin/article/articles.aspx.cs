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

public partial class netin_article_articles : ListBase
{

    public override string TableName
    {
        get { return "Article"; }
    }

    public override string EditPagePath
    {
        get { return "edit.aspx"; }
    }

    List<NtListItem> _categories;

    public View_ArticleClass Model { get; set; }

    /// <summary>
    /// 文章分类选项
    /// </summary>
    public List<NtListItem> Categories
    {
        get
        {
            if (_categories == null)
            {
                _categories = DB.GetDropdownlist<Article_Class>("Display=1");
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

            string seArticleId = Request.QueryString["se.article.id"],
                    seArticleClass = Request.QueryString["se.article.class"],
                     seArticleTitle = Request.QueryString["se.article.title"],
                     seArticleCreatedDateStart = Request.QueryString["se.article.createddate.start"],
                     seArticleCreatedDateEnd = Request.QueryString["se.article.createddate.end"];

            int id = 0;
            if (int.TryParse(seArticleId, out id))
            {
                where += " And Id='" + id + "'";
            }
            else
            {
                if (!string.IsNullOrEmpty(seArticleTitle))
                {
                    where += " And [Title] like '%" + seArticleTitle + "%'";
                }

                int articleClass = 0;
                if (int.TryParse(seArticleClass, out articleClass)
                    && articleClass > 0)
                {
                    where += " And ClassCrumbs like '%," + articleClass + ",%'";
                }

                DateTime start, end;
                if (DateTime.TryParse(seArticleCreatedDateStart, out start))
                {
                    where += " And datediff(d,'" + start + "',CreatedDate)>=0";
                }

                if (DateTime.TryParse(seArticleCreatedDateEnd, out end))
                {
                    where += " And datediff(d,'" + end + "',CreatedDate)<=0";
                }
            }
            return where;
        }
    }

    public override void List()
    {
        string filter = Where;
        //Response.Write(filter);
        //Response.End();
        string orderBy = "DisplayOrder desc,CreatedDate desc,ID desc";
        Pager = new NtPager();
        Pager.PageSize = 12;
        Pager.PagerItemCount = 5;
        List<View_Article> source = null;
        source = DbAccessor.GetList<View_Article>(filter, orderBy, Pager.PageIndex, Pager.PageSize);
        Pager.RecordCount = DbAccessor.Total;

        BeginTable("选择", "排序", "分类", "文章标题", "生效", "置顶", "推荐", "复制", "操作");

        foreach (var item in source)
        {
            Row<View_Article>(e =>
            {
                TdKey(item.Id);
                TdOrder(item.Id, item.DisplayOrder);
                Td(item.ClassName);
                Td(NtUtility.GetSubString(item.Title, 25));
                TdSetBoolean(item.Id, item.Display, "Display");
                TdSetBoolean(item.Id, item.SetTop, "SetTop");
                TdSetBoolean(item.Id, item.Recommended, "Recommended");
                Html("<td><a href=\"javascript:;\" onclick=\"copy({0},2,nt.reload);\" class=\"copy\"></a></td>", item.Id);
                Html("<td>");
                EditRowAnchor(item.Id);
                DelRowAnchor(true, item.Id);
                Html("</td>");
            }, item);
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdReOrder();
            TdPager(6);
            TdDelSelected(true);
        });

    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "文章列表";
    }

}