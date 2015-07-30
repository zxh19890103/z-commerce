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

public partial class netin_common_language : ListBase
{

    public override string TableName
    {
        get { return "Language"; }
    }

    public override string EditPagePath
    {
        get { return "language_config.aspx"; }
    }

    [AjaxMethod]
    public override void List()
    {
        var source = DbAccessor.GetList<Language>();

        BeginTable("ID", "Flag", "语言名", "排序", "发布", "操作");

        foreach (var item in source)
        {
            Row<Language>(e =>
            {
                Td(item.Id);
                Td(string.Format("<img src=\"../content/flags/{0}.png\"/>", item.LanguageCode));
                Td(item.Name);
                TdOrder(item.Id, item.DisplayOrder);
                TdSetBoolean(item.Id, item.Published, "Published");
                Td(() =>
                {
                    Anchor("javascript:openWindow({url:'language_config.aspx?id=" + item.Id + "'});", "", "edit");
                    DelRowAnchor(item.Id);
                });
            }, item);
        }

        EndTable(() =>
        {
            TdSpan(3);
            TdReOrder();
            Td();
            Td("<a href=\"javascript:openWindow('language_config.aspx');\" class=\"a-button\">添加</a>");
        });
    }


    protected override void Prepare()
    {
        base.Prepare();
        Title = "语言管理";
    }

}