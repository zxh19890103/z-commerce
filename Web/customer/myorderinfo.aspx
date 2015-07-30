<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Nt.Model.Enum" %>
<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>

<uc1:top runat="server" ID="top1" />

<%Include("/html.inc/customer_top.html"); %>
<div class="nt-order-submit">
    <h3>订单提交</h3>
    <form action="/handlers/ajaxhandler.aspx" method="post" id="orderForm" name="orderForm">
        <div class="nt-order-submit-item nt-order-consignee">
            <h3>收货人信息</h3>
            <div class="nt-order-history-consignee">
                <%                                    
                    var changyongdizhi = DbAccessor.GetList<Customer_Consignee>("CustomerId=" + Customer.Id);
                    int k = 0;
                    if (changyongdizhi.Count > 0)
                    {
                        Response.Write("<h4>常用地址</h4>");
                        Response.Write("<ul>");
                        foreach (var item in changyongdizhi)
                        {
                            Response.Write("<li>");
                            Response.Write("<input type=\"radio\" onclick=\"");

                            #region onclick  value consignee

                            //ConsigneeName
                            Response.Write("orderForm.ConsigneeName.value='");
                            Response.Write(item.Name);
                            Response.Write("';");
                            //ConsigneeAddress
                            Response.Write("orderForm.ConsigneeAddress.value='");
                            Response.Write(item.Address);
                            Response.Write("';");
                            //ConsigneePhone
                            Response.Write("orderForm.ConsigneePhone.value='");
                            Response.Write(item.Phone);
                            Response.Write("';");
                            //ConsigneeMobile
                            Response.Write("orderForm.ConsigneeMobile.value='");
                            Response.Write(item.Mobile);
                            Response.Write("';");
                            //ConsigneeEmail
                            Response.Write("orderForm.ConsigneeEmail.value='");
                            Response.Write(item.Email);
                            Response.Write("';");
                            //ConsigneeZip
                            Response.Write("orderForm.ConsigneeZip.value='");
                            Response.Write(item.Zip);
                            Response.Write("';");

                            #endregion

                            Response.Write("\" name=\"consigneeId\" value=\"");
                            Response.Write(item.Id);
                            Response.Write("\" />");
                            Response.Write("<label>");
                            Response.Write(item.Name);
                            Response.Write(":");
                            Response.Write(item.Address);
                            Response.Write("</label>");
                        }
                        Response.Write("<li>");
                        Response.Write("<input type=\"radio\" checked=\"checked\" onclick=\"$('input','#nt_order_new_consignee').val('');\" name=\"consigneeId\" value=\"0\" />");
                        Response.Write("<label>使用新地址</label>");
                        Response.Write("</li>");
                        Response.Write("</ul>");
                        k++;
                    }
                    if (k == 0)
                        Response.Write("<input type=\"hidden\" value=\"0\" name=\"consigneeId\"/>");
                %>
            </div>
            <div class="nt-order-new-consignee" id="nt_order_new_consignee">
                <ul>
                    <li>
                        <label><b>*</b>姓&nbsp;&nbsp;&nbsp;&nbsp;名：</label><input class="nt-input" name="ConsigneeName" />
                    </li>
                    <li>
                        <label><b>*</b>送货地点：</label><input class="nt-input" name="ConsigneeAddress" />
                    </li>
                    <li>
                        <label><b>*</b>手&nbsp;&nbsp;&nbsp;&nbsp;机：</label><input class="nt-input" name="ConsigneeMobile" />
                    </li>
                    <li>
                        <label><b>*</b>固定电话：</label><input class="nt-input" name="ConsigneePhone" />
                    </li>
                    <li>
                        <label>&nbsp;&nbsp;电子邮箱：</label><input class="nt-input" name="ConsigneeEmail" />
                    </li>
                    <li>
                        <label>&nbsp;&nbsp;邮政编码：</label><input class="nt-input" name="ConsigneeZip" />
                    </li>
                </ul>
            </div>
        </div>

        <div class="nt-order-submit-item nt-order-payment-shipping">
            <h3>支付及配送方式</h3>

            <div class="nt-order-payment">
                <h4>支付方式</h4>
                <%                                    
                    var zhifufangshi = DbAccessor.GetList<PaymentMethod>("", "DisplayOrder");

                    if (zhifufangshi.Count > 0)
                    {
                        Response.Write("<ul>");

                        var fisrt = zhifufangshi[0];
                        k = 0;
                        foreach (var item in zhifufangshi)
                        {
                            Response.Write("<li><input type=\"radio\"");
                            if (k == 0)
                                Response.Write(" checked=\"checked\"");
                            Response.Write(" onclick=\"G('paymentDesc').innerHTML=G('payment_desc_");
                            Response.Write(item.Id);
                            Response.Write("').innerHTML;\" name=\"paymentId\" value=\"");
                            Response.Write(item.Id);
                            Response.Write("\" />");
                            Response.Write("<label>");
                            Response.Write(item.Name);
                            Response.Write("</label><desc id=\"payment_desc_");
                            Response.Write(item.Id);
                            Response.Write("\" style=\"display:none;\">");
                            Response.Write(item.Description);
                            Response.Write("</desc></li>");
                            k++;
                        }

                        Response.Write("</ul>");
                        Response.Write("<p class=\"nt-order-desc\">说明：<span id=\"paymentDesc\">");
                        Response.Write(fisrt.Description);
                        Response.Write("</span></p>");
                    }
                %>
            </div>

            <div class="nt-order-shipping">
                <h4>配送方式</h4>
                <%                                    
                    var peisongfangshi = DbAccessor.GetList<ShippingMethod>("", "DisplayOrder");

                    if (peisongfangshi.Count > 0)
                    {
                        Response.Write("<ul>");

                        var fisrt = peisongfangshi[0];
                        k = 0;
                        foreach (var item in peisongfangshi)
                        {
                            Response.Write("<li><input type=\"radio\"");
                            if (k == 0)
                                Response.Write(" checked=\"checked\"");
                            Response.Write(" onclick=\"G('shippingDesc').innerHTML=G('shipping_desc_");
                            Response.Write(item.Id);
                            Response.Write("').innerHTML;\" name=\"shippingId\" value=\"");
                            Response.Write(item.Id);
                            Response.Write("\" />");
                            Response.Write("<label>");
                            Response.Write(item.Name);
                            Response.Write("</label><desc id=\"shipping_desc_");
                            Response.Write(item.Id);
                            Response.Write("\" style=\"display:none;\">");
                            Response.Write(item.Description);
                            Response.Write("</desc></li>");
                            k++;
                        }

                        Response.Write("</ul>");
                        Response.Write("<p class=\"nt-order-desc\">说明：<span id=\"shippingDesc\">");
                        Response.Write(fisrt.Description);
                        Response.Write("</span></p>");
                    }
                %>
            </div>

        </div>

        <div class="nt-order-submit-item nt-order-goods">
            <h3>商品清单</h3>
            <table>
                <tr>
                    <th>商品编号</th>
                    <th>商品名称</th>
                    <th>奈特价</th>
                    <th>赠送积分</th>
                    <th>优惠</th>
                    <th>商品数量</th>
                </tr>

                <%
                    var shoppingcartinfo = DbAccessor.GetList<View_ShoppingCart>("CustomerId=" + Customer.Id, "createddate desc");

                    var pricer = new PriceHandler();


                    foreach (var item in shoppingcartinfo)
                    {
                        pricer.Run(item);

                        Response.Write("<tr>");
                        Response.Write("<td>");
                        Response.Write(item.GoodsGuid);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(item.GoodsName);
                        Response.Write("</td>");
                        Response.Write("<td>&#165;");
                        Response.Write(pricer.StandardPrice);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(item.Points);
                        Response.Write("</td>");
                        Response.Write("<td>&#165;");
                        Response.Write(pricer.DiscountAmount);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(item.Quantity);
                        Response.Write("</td>");
                        Response.Write("</tr>");
                    }
                %>
            </table>

        </div>

        <div class="nt-order-submit-item nt-order-sumary">
            <h3>结算信息</h3>
            <span>商品金额：<var class="money">&#165;<%=pricer.Total.ToString("f") %></var>
                +
                            运费：<var class="money">&#165;<%=pricer.TotalMoneyforshipping.ToString("f") %></var>
                -
                            优惠：<var class="money">&#165;<%=pricer.TotalDiscountAmount.ToString("f") %></var></span>
            <hr />
            <span>应付总额：<var class="money">&#165;<%=(pricer.Total-pricer.TotalDiscountAmount).ToString("f")  %></var></span>
        </div>

        <div class="nt-order-submit-item nt-order-note-submit">

            <div class="nt-order-note">
                <h4>订单备注</h4>
                <textarea cols="1" rows="2" class="nt-textarea" name="orderNote"></textarea>
            </div>

            <div class="nt-order-submit-button">
                <input type="button" onclick="submitOrder();" value="提交订单" class="nt-submit" />
            </div>
        </div>

    </form>
</div>
<%Include("/html.inc/customer_bottom.html"); %>

<script type="text/javascript">
    //提交订单
    function submitOrder() {
        nt.ajaxSubmit('#orderForm', {
            action: 'SubmitOrder',
            ok: function (j) {
                window.location.href = 'pay.aspx';
            }
        });
    }
</script>

<uc1:foot runat="server" ID="foot" />


<script runat="server">

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        InCustomerArea = true;
        //no goods in your carts
        if (Customer.TotalOfShoppingCartItem == 0)
        {
            Goto("/list.aspx", "您的购物车里还没有商品，请返回添加！");
        }
    }
</script>
