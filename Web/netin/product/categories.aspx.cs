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
using System.IO;

public partial class netin_product_categories : ListBase<View_ProductCategory, ProductCategory>
{

    public override void List()
    {

        string filter = string.Format("LanguageId={0} and Deleted=0", NtContext.Current.LanguageID);

        DB.Space = "&nbsp;&nbsp;&nbsp;&nbsp;";
        DB.TreeBrand = "<img src=\"/netin/content/images/tree-node-padding.png\"/>";

        var tree = DB.GetTree<View_ProductCategory>(filter);

        BeginTable("排序", "类别名", "生效", "添加", "操作");

        Row(() =>
        {
            Html("<td></td>");
            Html("<td style=\"padding-left:5px;text-align:left;\">");
            Html("根级");
            Html("</td>");
            Html("<td></td><td></td><td></td>");
        });

        foreach (View_ProductCategory item in tree)
        {
            Row(() =>
            {
                TdOrder(item.Id, item.DisplayOrder);

                Html("<td style=\"padding-left:5px;text-align:left;\">");
                Html(item.Name);
                Html("</td>");

                TdSetBoolean(item.Id, item.Display, "Display");

                Td(() =>
                {
                    Html("<a href=\"{0}?pid={1}\">{2}</a>", EditPagePath, item.Id, "添加子类别");
                    Html("<a href=\"javascript:;\" onclick=\"treeMigrate(this,{0},2);\">{1}</a>", item.Id, "类别迁移");
                    Html("<a href=\"edit.aspx?cid={0}\">{1}</a>", item.Id, "添加产品");
                });

                Html("<td>");
                EditRowAnchor(item.Id);
                DelRowAnchor(item.Id, true, true);
                Html("</td>");

            }, item);

        }

        EndTable(() =>
        {
            TdReOrder();
            TdSpan(3);
            Td("<a class=\"a-button\" href=\"category_edit.aspx\">添加根类别</a>");
        });

    }

    public override string EditPagePath
    {
        get
        {
            return "category_edit.aspx";
        }
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "产品目录管理";
    }

}