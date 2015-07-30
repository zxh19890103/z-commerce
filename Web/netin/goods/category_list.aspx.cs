using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.Framework;
using Nt.DAL;
using Nt.Model;
using Nt.Framework.Admin;
using Nt.BLL;

public partial class Netin_Goods_category_list : ListBase
{
    public override string TableName
    {
        get { return "Goods_Class"; }
    }

    public override string EditPagePath
    {
        get { return "category_edit.aspx"; }
    }

    public override void List()
    {
        string filter = string.Format("LanguageId={0} and Deleted=0", NtContext.Current.LanguageID);

        DB.Space = "&nbsp;&nbsp;&nbsp;&nbsp;";
        DB.TreeBrand = "<img src=\"/netin/content/images/tree-node-padding.png\"/>";

        var tree = DB.GetTree<Goods_Class>(filter);

        BeginTable("排序", "类别名", "生效", "添加", "操作");

        Row(() =>
        {
            Html("<td></td>");
            Html("<td style=\"padding-left:5px;text-align:left;\">");
            Html("根级");
            Html("</td>");
            Html("<td></td><td></td><td></td>");
        });

        foreach (var item in tree)
        {
            Row<Goods_Class>(e =>
            {
                TdOrder(item.Id, item.DisplayOrder);

                Html("<td style=\"padding-left:5px;text-align:left;\">");
                Html(item.Name);
                Html("</td>");

                TdSetBoolean(item.Id, item.Display, "Display");

                Td(() =>
                {
                    Html("<a href=\"{0}?pid={1}\">{2}</a>", EditPagePath, item.Id, "添加子类别");
                    Html("<a href=\"javascript:;\" onclick=\"treeMigrate(this,{0},3);\">{1}</a>", item.Id, "类别迁移");
                    Html("<a href=\"goods_edit.aspx?gcid={0}\">{1}</a>", item.Id, "添加商品");
                });

                Td(() =>
                {
                    EditRowAnchor(item.Id);
                    DelRowAnchor(item.Id, true, true);
                });
            }, item);

        }

        EndTable(() =>
        {
            TdReOrder();
            TdSpan(3);
            Td("<a class=\"a-button\" href=\"category_edit.aspx\">添加根类别</a>");
        });

    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "商品分类列表";
    }

}