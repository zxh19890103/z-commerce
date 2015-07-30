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
using System.IO;
using Nt.Model.Enum;

public partial class netin_sale_order_info : PageBase, IAllAjax
{
    protected int id = IMPOSSIBLE;
    protected View_Order Model { get; set; }

    public override string PermissionSysN
    {
        get
        {
            return "netin.sale.orders";
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Title = "订单详情";
        if (!Int32.TryParse(Request.QueryString["id"], out id))
            Goto("orders.aspx", "参数错误!");
        Model = DbAccessor.GetById<View_Order>(id);
        if (Model == null)
            Goto("orders.aspx", "未发现记录!");
    }

    protected void RenderOrderItems()
    {
        var source = DbAccessor.GetList<View_OrderItem>("OrderId=" + Model.Id);

        BeginTable("商品名", "单价(RMB)", "数量", "价格调整(RMB)", "折扣(RMB)", "总计");
        foreach (View_OrderItem item in source)
        {
            Row<View_OrderItem>(e =>
            {
                Td(item.GoodsName);
                Td(item.GoodsPrice.ToString("f"));
                Td(item.Quantity);
                Td(item.Adjustment.ToString("f"));
                Td(item.DiscountAmount.ToString("f"));
                Td(((item.GoodsPrice - item.DiscountAmount + item.Adjustment) * item.Quantity).ToString("f"));
            }, item);
        }
        EndTable();
    }

    /// <summary>
    /// 订单备注
    /// </summary>
    /// <param name="orderId">订单id</param>
    protected void RenderOrderNote(int orderId)
    {
        var notes = DbAccessor.GetList<OrderNote>("OrderId=" + orderId);
        BeginTable("选择", "备注", "向用户开放", "创建日期", "操作");
        foreach (var item in notes)
        {
            Row(() =>
            {
                TdKey(item.Id);
                Td(NtUtility.GetSubString(item.Note, 20));
                Td(item.DisplayToCustomer ? "Yes" : "No");
                Td(item.CreatedDate);
                TdEditViaAjax(item.Id);
            });
        }
        EndTable(() =>
        {
            TdSelectAll();
            TdSpan(3);
            Html(TAG_TD_BEGIN);
            AddNewButtonViaAjax();
            Html(TAG_TD_END);
        });
    }

    /// <summary>
    /// 订单备注
    /// </summary>
    public void RenderOrderNote()
    {
        RenderOrderNote(Model.Id);
    }

    /// <summary>
    /// 取消订单
    /// </summary>
    [AjaxMethod]
    public void CancelOrder()
    {
        int id = IMPOSSIBLE;
        NtJson.EnsureNumber("id", "参数错误:id", out id);
        SqlHelper.ExecuteNonQuery(string.Format("Update [{0}] Set Status=" + (int)OrderStatus.Cancelled + " Where Id={1};",
            DbAccessor.GetModifiedTableName("Order"), id));
        NtJson.ShowOK();
    }

    [AjaxMethod]
    public void TMPost()
    {
        OrderNote m = new OrderNote();
        m.InitDataFromPage();
        if (m.Id == 0)
            m.CreatedDate = DateTime.Now;
        DbAccessor.UpdateOrInsert(m);
        NtJson.ShowOK();
    }

    [AjaxMethod]
    public void TMDel()
    {
        int id=IMPOSSIBLE;
        NtJson.EnsureNumber("id","参数错误:id",out id);
        DbAccessor.Delete(typeof(OrderNote), id);
        NtJson.ShowOK();
    }

    [AjaxMethod]
    public void TMGet()
    {
        int orderNoteId = IMPOSSIBLE;
        NtJson.EnsureNumber("id", "参数错误：id", out orderNoteId);
        OrderNote m = DbAccessor.GetById<OrderNote>(orderNoteId);
        if (m == null)
            NtJson.ShowError("未发现记录!");
        NtJson json = new NtJson();
        json.ErrorCode = NtJson.OK;
        json.ParseJson(m);
        Response.Write(json);
    }

    [AjaxMethod]
    public void TMList()
    {
        int orderId = IMPOSSIBLE;
        NtJson.EnsureNumber("id", "参数错误：orderId", out orderId);
        RenderOrderNote(orderId);
    }
}