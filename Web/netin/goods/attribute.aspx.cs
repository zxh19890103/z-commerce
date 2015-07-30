using Nt.DAL;
using Nt.Framework.Admin;
using Nt.Model;
using Nt.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.Framework;

public partial class Netin_goods_attribute : ListBase
{

    public int id = IMPOSSIBLE;

    public override string TableName
    {
        get { return "GoodsAttribute"; }
    }

    public override string EditPagePath
    {
        get { return "attribute.aspx"; }
    }

    [AjaxMethod]
    public override void List()
    {
        var source = DbAccessor.GetList<GoodsAttribute>();

        BeginTable("属性名", "操作");

        foreach (var item in source)
        {
            Row<GoodsAttribute>(e =>
            {
                Td(item.Name);
                Td(() =>
                {
                    Anchor("javascript:openWindow({url:'attribute_edit.aspx?id=" + item.Id + "'});", "", "edit");
                    DelRowAnchor(item.Id);
                });
            }, item);
        }

        EndTable(() =>
        {
            Td();
            Td("<a href=\"javascript:openWindow('attribute_edit.aspx');\" class=\"a-button\">添加</a>");
        });
    }
        
    protected override void Prepare()
    {
        base.Prepare();
        Title = "属性管理";
    }
}