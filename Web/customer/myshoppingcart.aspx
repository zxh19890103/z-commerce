<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>
<%
    InCustomerArea = true;
%>
<!----container---->
<uc1:top runat="server" ID="top1" />

<%Include("/html.inc/customer_top.html"); %>
<h3>我的购物车</h3>

<table class="nt-table">
    <thead>
        <tr>
            <th>商品</th>
            <th>单价</th>
            <th>优惠</th>
            <th>价格微调</th>
            <th>属性</th>
            <th>数量</th>
            <th>小计</th>
            <th>移除</th>
        </tr>
    </thead>

    <tbody id="shoppingcartinfo">
        <%
            RenderShoppingCartInfo();
        %>
    </tbody>

</table>

<%Include("/html.inc/customer_bottom.html"); %>

<script type="text/javascript">
    //remove specified item in shopping cart
    function removeCartItem(id) {
        confirm('删除后将无法恢复!您确定删除此项?',
            function () {
                nt.ajax({
                    data: { id: id },
                    action: 'delfromcart',
                    success: function (j) {
                        if (j.error) error(j.message);
                        else {
                            G('shoppingcartItemTotal').innerHTML = j.quantity;//ui of quantity
                            nt.showLoading();
                            $('#shoppingcartinfo').load(
                                '',
                                { action: 'RenderShoppingCartInfo' },
                                function () {
                                    nt.removeLoading();
                                });
                        }
                    }
                });
            }, null);
    }

    /*
    according to add quantity,set quantity of specified goods
    */
    function setQuantity(id, n, byIncrement) {
        var ctrl = document.getElementById('quantity_' + id);
        if (!byIncrement && !/^[1-9]\d*$/.test(n)) {
            ctrl.value = ctrl.getAttribute('data-old-value');
            return;
        }
        var quantity = 0, min = 1;

        if (byIncrement)
            quantity = parseInt(ctrl.value) + n;
        else
            quantity = n;

        //var max = parseInt(ctrl.getAttribute('data-max-quantity'));

        if (quantity < min) return;

        nt.ajax({
            action: 'setQuantity',
            data: { id: id, quantity: quantity },
            success: function (j) {
                if (j.error) error(j.message);
                else {
                    $('#shoppingcartinfo').load(
                                '',
                                { action: 'RenderShoppingCartInfo' },
                                function () {
                                    nt.removeLoading();
                                });
                }
            }
        });
    }

    //go myorderinfo.aspx to submit my order
    function toSubmitOrder() {
        window.location.href = 'myorderinfo.aspx';
    }

</script>

<uc1:foot runat="server" ID="foot" />



<script runat="server">


    [AjaxMethod]
    public void RenderShoppingCartInfo()
    {
        var cartitems = DbAccessor.GetList<View_ShoppingCart>("CustomerId=" + Customer.Id, "");

        if (cartitems.Count == 0)
        {
            Response.Write("<tr><td colspan=\"8\"><p class=\"no-record-found\"><a href=\"/list.aspx\">您的购物车里没有商品！请浏览并添加你想购买的商品。</a></p></td></tr>");
            return;
        }

        var pricer = new PriceHandler();

        foreach (var item in cartitems)
        {
            pricer.Run(item);

            Response.Write("<tr>");
            Response.Write("		<td>");
            Response.Write("			<a target=\"_blank\" href=\"/detail.aspx?id=");
            Response.Write(item.GoodsId);
            Response.Write("\">");
            Response.Write(item.GoodsName);
            Response.Write("</a>");
            Response.Write("			<br />");
            Response.Write("			<img alt=\"goods name\" src=\"");
            Response.Write(item.PictureUrl);
            Response.Write("\" width=\"80\" height=\"80\" />");
            Response.Write("		</td>");
            Response.Write("		<td>");
            Response.Write("			<span>￥");
            Response.Write(pricer.StandardPrice.ToString("f"));
            Response.Write("</span>");
            Response.Write("		</td>");
            Response.Write("		<td>");
            Response.Write("			<span>￥");
            Response.Write(pricer.DiscountAmount.ToString("f"));
            Response.Write("</span>");
            Response.Write("		</td>");
            Response.Write("		<td>");
            Response.Write("			<span>￥");
            Response.Write(pricer.Adjustment.ToString("f"));
            Response.Write("</span>");
            Response.Write("		</td>");
            Response.Write("		<td class=\"nt-cart-item-xml\">");
            Response.Write(item.AttributesXml);
            Response.Write("		</td>");
            Response.Write("		<td>");
            Response.Write("			<span><a class=\"nt-minus\" href=\"javascript:;\" onclick=\"setQuantity(");
            Response.Write(item.Id);
            Response.Write(",-1,true);\">-</a>");
            Response.Write("<input type=\"text\" data-max-quantity=\"");
            Response.Write(item.Quantity);
            Response.Write("\" onchange=\"setQuantity(");
            Response.Write(item.Id);
            Response.Write(",this.value,false);\" data-old-value=\"");
            Response.Write(item.Quantity);
            Response.Write("\" style=\"width:50px;height:24px;text-align:center;line-height:24px;padding:0;margin:0;\" id=\"quantity_");
            Response.Write(item.Id);
            Response.Write("\" value=\"");
            Response.Write(item.Quantity);
            Response.Write("\" /><a class=\"nt-plus\" href=\"javascript:;\" onclick=\"setQuantity(");
            Response.Write(item.Id);
            Response.Write(",1,true);\">+</a></span>");
            Response.Write("		</td>");
            Response.Write("		<td>");
            Response.Write("			<span>￥");
            Response.Write((pricer.SubTotal - pricer.SubTotalDiscountAmount).ToString("f"));
            Response.Write("</span>");
            Response.Write("		</td>");
            Response.Write("		<td>");
            Response.Write("			<a href=\"javascript:;\" onclick=\"removeCartItem(");
            Response.Write(item.Id);
            Response.Write(");\">删除</a>");
            Response.Write("		</td>");
            Response.Write("	</tr>");
        }

        Response.Write("<tr><td colspan=\"8\">");
        Response.Write("<p style=\"text-align:right\">");

        Response.Write("您选中了<span class=\"money\">");
        Response.Write(pricer.TotalQuantity);
        Response.Write("</span>件商品，");

        Response.Write(" 共计：<span class=\"money\">");
        RenderMoney(pricer.Total);
        Response.Write("</span>，");

        Response.Write(" 优惠：<span class=\"money\">");
        RenderMoney(pricer.TotalDiscountAmount);
        Response.Write("</span>，");

        Response.Write(" 运费：<span class=\"money\">");
        RenderMoney(pricer.TotalMoneyforshipping);
        Response.Write("</span>，");

        Response.Write(" 应付：<span class=\"money\">");
        RenderMoney(pricer.Total - pricer.TotalDiscountAmount);
        Response.Write("</span>.");

        Response.Write("<input type=\"button\" onclick=\"toSubmitOrder();\" class=\"nt-submit buy-now\" value=\"结算\" />");

        Response.Write("<p></td></tr>");
    }
</script>
