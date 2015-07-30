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

public partial class netin_product_area :ListBase<Area>, IAllAjax
{
    [AjaxMethod]
    public void TMPost()
    {
        AjaxSave<Area>();
    }

    [AjaxMethod]
    public void TMDel()
    {
        AjaxDel<Area>();
    }

    [AjaxMethod]
    public void TMGet()
    {
        AjaxGet<Area>();
    }

    [AjaxMethod]
    public void TMList()
    {
        List();
    }

    public override void List()
    {
        BeginTable("选择","排序", "地区名", "英文地区名","操作");

        foreach (var item in DataSource)
        {
            Row(() =>
            {
                TdKey(item.Id);
                TdOrder(item.Id,item.DisplayOrder);
                Td(item.Name);
                Td(item.EnglishName);
                TdEditViaAjax(item.Id);
            });
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdReOrder();
            TdSpan(2);
            TdEditViaAjax();
        });

    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "地区管理";
    }

}