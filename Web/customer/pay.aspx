<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Import Namespace="Nt.Model.Enum" %>
<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>

<!----container---->
<uc1:top runat="server" ID="top1" />
<%Include("/html.inc/customer_top.html"); %>
<h3>订单信息</h3>
<h5>收货人:</h5>
<span class="padding"><%=Order.Name %></span>
<h5>收货人联系电话:</h5>
<span class="padding">
    <span><%=Order.Mobile %></span>
    或<span><%=Order.Phone %></span>
</span>
<h5>收货地址:</h5>
<span class="padding"><%=Order.Address %></span>
<h5>配送方式：</h5>
<span class="padding"><%=Order.ShippingMethod %></span>
<h5>支付方式：</h5>
<span class="padding"><%=Order.PaymentMethod %></span>
<h5>应付金额：</h5>
<span class="padding">
    <var class="money">&#165;<%=Order.OrderTotal-Order.OrderTotalDiscount %></var></span>
<hr class="nt-hr" />
<div class="nt-customer-save-button">
    <input type="button" class="nt-submit" onclick="pay();" value="确定支付" />
</div>
<%Include("/html.inc/customer_bottom.html"); %>

<script type="text/javascript">
    function pay() {
        alert('sorry,stop here for no pay method has been created.');
    }
</script>

<uc1:foot runat="server" ID="foot" />


<script runat="server">

    protected View_Order Order;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        InCustomerArea = true;
        int id = 0;
        string filter = string.Format("CustomerId={0} and PaymentStatus={1} {2}",
            Customer.Id,
           (int)PaymentStatus.Pending,
           (Int32.TryParse(Request.QueryString["id"], out id) && id > 0) ? " and id=" + id : "");

        Order = DbAccessor.GetFirstOrDefault<View_Order>(filter, "");
        if (Order == null)
            Goto("/list.aspx", "您没有未处理的订单!");
    }
</script>
