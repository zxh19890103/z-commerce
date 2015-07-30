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

public partial class netin_product_day : ListBase<Day>, IAllAjax
{
    [AjaxMethod]
    public void TMPost()
    {
        AjaxSave<Day>();
    }

    [AjaxMethod]
    public void TMDel()
    {
        AjaxDel<Day>();
    }

    [AjaxMethod]
    public void TMGet()
    {
        AjaxGet<Day>();
    }

    [AjaxMethod]
    public void TMList()
    {
        List();
    }

    public override void List()
    {
        BeginTable("选择","排序", "标题", "英文标题", "日期", "操作");

        foreach (var item in DataSource)
        {
            Row(() =>
            {
                TdKey(item.Id);
                TdOrder(item.Id,item.DisplayOrder);
                Td(item.Title);
                Td(item.EnglishTitle);
                Td(item.Date.ToString("yyyy-MM-dd"));
                TdEditViaAjax(item.Id);
            });
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdReOrder();
            TdSpan(3);
            TdEditViaAjax();
        });

    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "日期管理";
    }


}