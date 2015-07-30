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
using Nt.Model.Enum;

public partial class netin_common_navigation : ListBase
{
    public override string TableName
    {
        get { return "Navigation"; }
    }

    public override string EditPagePath
    {
        get { return "Navigation_edit.aspx"; }
    }

    public override void List()
    {
        string filter = "LanguageId=" + NtContext.Current.LanguageID;

        DB.Space = "&nbsp;&nbsp;";
        DB.TreeBrand = "<img src=\"/netin/content/images/tree-node-padding.png\"/>";

        var tree = DB.GetTree<Navigation>(filter);

        BeginTable("ID", "排序", "导航名", "模块类型", "生效", "其他", "操作");

        Row(() =>
        {
            Html("<td></td><td></td>");
            Html("<td style=\"padding-left:5px;text-align:left;\">");
            Html("根级");
            Html("</td>");
            Html("<td></td><td></td><td></td><td></td>");
        });

        foreach (var item in tree)
        {
            Row(() =>
            {
                Td(item.Id);
                TdOrder(item.Id, item.DisplayOrder);
                Html("<td style=\"padding-left:5px;text-align:left;\">");
                Html(item.Name);
                Html("</td>");
                Td(FindTextByValue(StaticDataProvider.Instance.NaviTypeProvider, item.NaviType));
                TdSetBoolean(item.Id, item.Display, "Display");
                if (item.NaviType == (int)ModuleType.Folder)
                    TdF("<a href=\"{0}?pid={1}\">添加子导航</a>", EditPagePath, item.Id);
                else
                {
                    if (item.NaviType == (int)ModuleType.Link)
                        Td(item.Path);
                    else if (item.NaviType == (int)ModuleType.Page)
                        Td(item.PageIds);
                    else
                        Td(item.RootSid);
                }
                Td(() =>
                {
                    EditRowAnchor(item.Id);
                    DelRowAnchor(item.Id, true);
                });
            }, item);
        }

        EndTable(() =>
        {
            Td();
            TdReOrder();
            TdSpan(4);
            Td("<a href=\"navigation_edit.aspx\" class=\"a-button\">添加根级导航</a>");
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "导航管理";
    }
}