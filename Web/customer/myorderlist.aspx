<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>
<%
    
    InCustomerArea = true;
    
%>

<!----container---->
<uc1:top runat="server" ID="top1" />

<%Include("/html.inc/customer_top.html"); %>

<h3>我的订单</h3>
<%
    var data = DbAccessor.GetList<View_Order>("deleted=0 and customerid=" + Customer.Id, "createddate desc");

    Response.Write("<table class=\"nt-table\">");

    Response.Write("<thead><tr>");
    Response.Write("<th width=\"100\">订单编号</th>");
    Response.Write("<th width=\"80\">状态</th>");
    Response.Write("<th width=\"80\">支付状态</th>");
    Response.Write("<th width=\"80\">配送状态</th>");
    Response.Write("<th width=\"80\">支付方式</th>");
    Response.Write("<th width=\"80\">配送方式</th>");
    Response.Write("<th width=\"80\">金额</th>");
    Response.Write("<th width=\"80\">折扣</th>");
    Response.Write("<th width=\"80\">运费</th>");
    Response.Write("<th width=\"80\">创建日期</th>");
    Response.Write("<th width=\"80\">操作</th>");
    Response.Write("</tr></thead><tbody>");

    if (data.Count == 0)
    {
        Response.Write("<tr>");
        Response.Write("<td colspan=\"11\"><p class=\"no-record-found\">您暂无订单!</p></td>");
        Response.Write("</tr>");
    }
    else
    {

        foreach (var item in data)
        {
            Response.Write("<tr>");
            Response.Write("<td>");
            Response.Write("<a href=\"myorderdetail.aspx?id=");
            Response.Write(item.Id);
            Response.Write("\">");
            Response.Write(item.OrderGuid);
            Response.Write("</a>");
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write(FindTextByValue(StaticDataProvider.Instance.OrderStatusProvider, item.Status));
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write(FindTextByValue(StaticDataProvider.Instance.PaymentStatusProvider, item.PaymentStatus));
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write(FindTextByValue(StaticDataProvider.Instance.ShippingStatusProvider, item.ShippingStatus));
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write(item.PaymentMethod);
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write(item.ShippingMethod);
            Response.Write("</td>");
            Response.Write("<td>");
            RenderMoney(item.OrderTotal);
            Response.Write("</td>");
            Response.Write("<td>");
            RenderMoney(item.OrderTotalDiscount);
            Response.Write("</td>");
            Response.Write("<td>");
            RenderMoney(item.ShippingExpense);
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write(item.CreatedDate.ToString("yyyy/MM/dd hh:mm:ss"));
            Response.Write("</td>");
            Response.Write("<td><a href=\"pay.aspx?id=");
            Response.Write(item.Id);
            Response.Write("\">处理</a></td>");
            Response.Write("</tr>");
        }
    }
    Response.Write("</tbody></table>");
%>




<%Include("/html.inc/customer_bottom.html"); %>

<uc1:foot runat="server" ID="foot" />

