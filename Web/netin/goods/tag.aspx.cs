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

public partial class netin_goods_tag : ListBase<Goods_Tag>, IAllAjax
{
    public override void List()
    {
        string filter = string.Empty,
            orderby = "DisplayOrder desc,CreatedDate desc";

        Pager = new NtPager();
        var source = DbAccessor.GetList<Goods_Tag>(filter, orderby, Pager.PageIndex, Pager.PageSize);
        Pager.RecordCount = DbAccessor.Total;

        BeginTable("选择", "标签", "生效", "排序", "操作");

        foreach (var item in source)
        {
            Row<Goods_Tag>(e =>
            {
                TdKey(item.Id);
                Td(item.Tag);
                TdSetBoolean(item.Id, item.Display, "Display");
                TdOrder(item.Id, item.DisplayOrder);
                TdEditViaAjax(item.Id);
            }, item);
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdPager(2);
            TdReOrder();
            Html(TAG_TD_BEGIN);
            AddNewButtonViaAjax();
            Html(TAG_TD_END);
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "商品标签";
    }

    [AjaxMethod]
    public void TMPost()
    {
        AjaxSave<Goods_Tag>();
    }
    [AjaxMethod]
    public void TMDel()
    {
        AjaxDel<Goods_Tag>();
    }
    [AjaxMethod]
    public void TMGet()
    {
        AjaxGet<Goods_Tag>();
    }
    [AjaxMethod]
    public void TMList()
    {
        List();
    }
}