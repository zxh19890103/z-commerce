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

public partial class netin_common_htmlblock : ListBase<CustomHtmlBlock>, IAllAjax
{
    public override void List()
    {
        SetUsePagerOn(12);
        DB.List(TableName, Pager.PageIndex, Pager.PageSize).AsName("ok");
        DB.Execute();
        Pager.RecordCount = DB.GetTotal("ok");
        var data = new List<CustomHtmlBlock>();
        var source = DbAccessor.FetchListByDataTable(DB["ok"], data, EntityType);
        BeginTable("选择", "Id", "Name", "操作");
        foreach (var item in data)
        {
            Row(() =>
            {
                TdKey(item.Id);
                Td(item.Id);
                Td(item.Name);
                TdEditViaAjax(item.Id);
            });
        }
        EndTable(() =>
        {
            TdSelectAll();
            TdPager(2);
            TdEditViaAjax();
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "自定义Html区管理";
    }

    [AjaxMethod]
    public void TMPost()
    {
        AjaxSave<CustomHtmlBlock>();
    }
    [AjaxMethod]
    public void TMDel()
    {
        AjaxDel<CustomHtmlBlock>();
    }
    [AjaxMethod]
    public void TMGet()
    {
        AjaxGet<CustomHtmlBlock>();
    }

    [AjaxMethod]
    public void TMList()
    {
        List();
    }
}