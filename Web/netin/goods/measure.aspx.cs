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

public partial class netin_goods_measure : ListBase<Measure>, IAllAjax
{
    [AjaxMethod]
    public override void List()
    {
        BeginTable("选择", "排序", "单位名", "操作");

        foreach (var item in DataSource)
        {
            Row(() =>
            {
                TdKey(item.Id);
                TdOrder(item.Id, item.DisplayOrder);
                Td(item.Name);
                TdEditViaAjax(item.Id);
            });
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdReOrder();
            Td();
            TdEditViaAjax();
        });
    }

    [AjaxMethod]
    public void TMPost()
    {
        AjaxSave<Measure>();
    }

    [AjaxMethod]
    public void TMDel()
    {
        AjaxDel<Measure>();
    }

    [AjaxMethod]
    public void TMGet()
    {
        AjaxGet<Measure>();
    }

    [AjaxMethod]
    public void TMList()
    {
        List();
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "单位管理";
    }

}