using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.Framework;
using System.Data.SqlClient;
using System.Data;
using Nt.Model;
using Nt.DAL;
using Nt.Model.View;
using Nt.Framework.Admin;
using Nt.BLL;

public partial class Netin_Goods_parameter_group : ListBase
{
    
    public override string TableName
    {
        get { return "Goods_ParameterGroup"; }
    }

    public override string EditPagePath
    {
        get { return "parameter_group.aspx"; }
    }

    [AjaxMethod]
    public override void List()
    {
        var source = DbAccessor.GetList<Goods_ParameterGroup>("", "DisplayOrder desc");

        BeginTable("排序", "参数组名", "生效", "操作");

        foreach (var item in source)
        {
            Row<Goods_ParameterGroup>(e =>
            {
                TdOrder(item.Id, item.DisplayOrder);
                Td(item.GroupName);
                TdSetBoolean(item.Id, item.Display, "Display");
                Td(() =>
                {
                    Anchor("javascript:openWindow({url:'parameter_group_edit.aspx?id=" + item.Id + "'});", "", "edit");
                    DelRowAnchor(item.Id);
                });
            }, item);
        }

        EndTable(() =>
        {
            TdReOrder();
            TdSpan(2);
            Td("<a href=\"javascript:openWindow('parameter_group_edit.aspx');\" class=\"a-button\">添加</a>");
        });

    }
        
    protected override void Prepare()
    {
        base.Prepare();
        Title = "商品参数组管理";
    }

}