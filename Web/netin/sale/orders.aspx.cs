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
using System.Data.SqlClient;
using System.Data;

public partial class netin_sale_orders : ListBase
{
    protected int CancelledOrderCount = 0;
    protected int PendingOrderCount = 0;
    protected int ProcessingOrderCount = 0;
    protected int CompleteOrderCount = 0;
    protected int PaidOrderCount = 0;
    protected int RefundedOrderCount = 0;


    public override string TableName
    {
        get { return "Order"; }
    }

    public override string EditPagePath
    {
        get { return "order_info.aspx"; }
    }

    /// <summary>
    /// 搜索条件
    /// </summary>
    protected override string Where
    {
        get
        {
            string where = "Deleted=0";//删除的订单在页面上永不显示

            string[] searchTerms = new string[] 
            { 
                "cancelled", "pending", "processing", "complete", "paid", "refunded" 
            };

            string[] filterTerms = new string[] 
            { 
                "Status="+(int)OrderStatus.Cancelled, 
                "Status="+(int)OrderStatus.Pending, 
                "Status="+(int)OrderStatus.Processing, 
                "Status="+(int)OrderStatus.Complete, 
                "PaymentStatus="+(int)PaymentStatus.Paid,
                "PaymentStatus="+(int)PaymentStatus.PartiallyRefunded+
                " Or PaymentStatus="+(int)PaymentStatus.Refunded,
            };

            string queryValue = string.Empty;

            for (int i = 0; i < searchTerms.Length; i++)
            {
                queryValue = Request.QueryString[string.Format("se.{0}.order", searchTerms[i])];
                if (!string.IsNullOrEmpty(queryValue))
                    return where + " And " + filterTerms[i];
            }

            string seOrderId = Request.QueryString["se.order.id"],
                     seOrderCreatedDateStart = Request.QueryString["se.order.createddate.start"],
                     seOrderCreatedDateEnd = Request.QueryString["se.order.createddate.end"],
                     seOrderStatus = Request.QueryString["se.order.status"],
                     seOrderShippingStatus = Request.QueryString["se.order.shippingstatus"],
                     seOrderPaymentStatus = Request.QueryString["se.order.paymentstatus"];

            int id = 0;
            if (int.TryParse(seOrderId, out id))
            {
                where += " And Id=" + id;
            }
            else
            {
                int orderStatus, orderShippingStatus, orderPaymentStatus;
                if (int.TryParse(seOrderStatus, out orderStatus) && orderStatus > 0)
                    where += " And Status=" + orderStatus;
                if (int.TryParse(seOrderShippingStatus, out orderShippingStatus) && orderShippingStatus > 0)
                    where += " And ShippingStatus=" + orderShippingStatus;
                if (int.TryParse(seOrderPaymentStatus, out orderPaymentStatus) && orderPaymentStatus > 0)
                    where += " And PaymentStatus=" + orderPaymentStatus;

                DateTime start, end;
                if (DateTime.TryParse(seOrderCreatedDateStart, out start))
                {
                    where += " And datediff(d,'" + start + "',CreatedDate)>=0";
                }

                if (DateTime.TryParse(seOrderCreatedDateEnd, out end))
                {
                    where += " And datediff(d,'" + end + "',CreatedDate)<=0";
                }
            }
            return where;
        }
    }

    public override void List()
    {

        decimal totalPrice = 0.0M;
        decimal totalDiscount = 0.0M;
        decimal totalShippingExpense = 0.0M;
        decimal totalPaymentExpense = 0.0M;

        string filter = Where;

        Pager = new NtPager();

        var source = DbAccessor.GetList<View_Order>(filter, "CreatedDate desc", Pager.PageIndex, Pager.PageSize);
        Pager.RecordCount = DbAccessor.Total;

        BeginTable("选择", "订单状态", "配送状态", "支付状态", "订单总额(元)", "总折扣(元)", "配送费用(元)", "支付网关费(元)", "支付日期", "支付方式", "配送方式", "其它", "操作");

        foreach (var item in source)
        {
            Row<View_Order>(e =>
            {
                TdKey(item.Id);
                Td(string.Format("<label class=\"status-setting\" data-curr=\"{0}\" onclick=\"selectOrderStatus(this,{2});\">{1}</label>",
                    item.Status, FindTextByValue(StaticDataProvider.Instance.OrderStatusProvider,
                    item.Status), item.Id));
                Td(string.Format("<label class=\"status-setting\"  data-curr=\"{0}\"  onclick=\"selectShippingStatus(this,{2});\">{1}</label>",
                    item.ShippingStatus, FindTextByValue(StaticDataProvider.Instance.ShippingStatusProvider,
                    item.ShippingStatus), item.Id));
                Td(string.Format("<label class=\"status-setting\" data-curr=\"{0}\"  onclick=\"selectPaymentStatus(this,{2});\">{1}</label>",
                    item.PaymentStatus, FindTextByValue(StaticDataProvider.Instance.PaymentStatusProvider,
                    item.PaymentStatus), item.Id));
                Td(item.OrderTotal.ToString("f"));
                Td(item.OrderTotalDiscount.ToString("f"));
                Td(item.ShippingExpense.ToString("f"));
                Td(item.CommissionCharge.ToString("f"));
                if (item.PaymentStatus == (int)PaymentStatus.Paid)
                    Td(item.PaidDate.ToString("yyyy-MM-dd"));
                else
                    Td("尚未支付");
                Td(item.PaymentMethod);
                Td(item.ShippingMethod);
                Td(() =>
                {
                    Html("<a onclick=\"viewOrderMsg({0});\" href=\"javascript:;\">查看订单留言</a>", item.Id);
                    Html("<a onclick=\"openWindow({{url:'../customer/info_view.aspx?id={0}'}});\" href=\"javascript:;\">会员信息</a>", item.CustomerId);
                    Html("<a onclick=\"viewConsigneeInfo({0});\" href=\"javascript:;\">查看收货者信息</a>", item.Id);
                });
                Td(() =>
                {
                    EditRowAnchor(item.Id);
                    Html("<a href=\"javascript:deleteOrder({0})\" class=\"del\"></a>", item.Id);
                    Html("<div id=\"orderMsg_{0}\" class=\"hidden\">", item.Id);
                    Html(item.OrderMessage);
                    Html("</div>");
                    Html("<script type=\"text/javascript\">");
                    Html("ConsigneeInfoObj[{6}]={{name:'{0}',phone:'{1}',mobile:'{2}',email:'{3}',address:'{4}',zip:'{5}'}};",
                        item.Name, item.Phone, item.Mobile, item.Email, item.Address, item.Zip, item.Id);
                    Html("</script>");
                });
            }, item);
            totalPrice += item.OrderTotal;
            totalDiscount += item.OrderTotalDiscount;
            totalShippingExpense += item.ShippingExpense;
            totalPaymentExpense += item.CommissionCharge;
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdSpan(3);
            Td("总计:" + totalPrice.ToString("f"));
            Td("总计:" + totalDiscount.ToString("f"));
            Td("总计:" + totalShippingExpense.ToString("f"));
            Td("总计:" + totalPaymentExpense.ToString("f"));
            TdPager(4);
            Td();
        });
    }

    /// <summary>
    /// 设置状态
    /// </summary>
    [AjaxMethod]
    public void SetStatus()
    {
        int type = IMPOSSIBLE,
            status = IMPOSSIBLE,
            orderId = IMPOSSIBLE;
        NtJson.EnsureNumber("type", "参数错误:type", out type);
        NtJson.EnsureNumber("status", "参数错误:status", out status);
        NtJson.EnsureNumber("orderId", "参数错误:orderId", out orderId);

        string sql = string.Format("Update [{0}] Set [{1}]={2} Where Id={3}",
            M(typeof(Order)),
            (type == 0 ? "Status" : (type == 1 ? "ShippingStatus" : "PaymentStatus")),
            status,
            orderId);

        SqlHelper.ExecuteNonQuery(sql);

        NtJson.ShowOK();

    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "订单管理";
        string statistic = string.Format(
            "Select ({0} Status={1}) As C0,({0} Status={2}) As C1,({0} Status={3}) As C2,({0} Status={4}) As C3,({0} PaymentStatus={5}) As C4,({0} PaymentStatus={6} Or PaymentStatus={7}) As C5;\r\n",
            "Select Count(0) From " + DbAccessor.GetModifiedTableName("Order") + " Where",
            (int)OrderStatus.Cancelled,
            (int)OrderStatus.Pending,
            (int)OrderStatus.Processing,
            (int)OrderStatus.Complete,
            (int)PaymentStatus.Paid,
            (int)PaymentStatus.PartiallyRefunded,
            (int)PaymentStatus.Refunded);

        using (SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, statistic))
        {
            if (rs.Read())
            {
                CancelledOrderCount = rs.GetInt32(0);
                PendingOrderCount = rs.GetInt32(1);
                ProcessingOrderCount = rs.GetInt32(2);
                CompleteOrderCount = rs.GetInt32(3);
                PaidOrderCount = rs.GetInt32(4);
                RefundedOrderCount = rs.GetInt32(5);
            }
        }
    }
}