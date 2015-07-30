<%@ Page Language="C#" AutoEventWireup="true" CodeFile="orderinfo.aspx.cs" Inherits="user_orderinfo" %>

<%@ Register Src="~/shared/qb_top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/qb_foot.ascx" TagPrefix="uc1" TagName="foot" %>

<uc1:top runat="server" ID="top" />

<div style="height: 57px;" class="clear"></div>
<p class="shoppingcart_title">我的购物车</p>
<div style="height: 45px;" class="clear"></div>
<div class="shoppingmenv">
    <a class="menv07" href="#">1.放进购物车</a>
    <a class="menv08" href="#">2.填写订单信息</a>
    <a class="menv09" href="#">3.支付/确认订单</a>
</div>

<form  action="/handlers/ajaxHandler.aspx" method="post" id="OrderInfoForm">

    <div style="height: 37px;" class="clear"></div>
    <div class="receipt">
        <span>1.收货信息</span>
        <a href="shoppingcart.aspx">返回购物车</a>
    </div>

    <div style="height: 3px;" class="clear"></div>
    <div class="receipt_content">
        <div class="receipt_column">
            <span class="span01">收货方式</span>
            <%
                RenderConsignee();
            %>
        </div>
        <div style="height: 14px;" class="clear"></div>
        <div class="receipt_column">
            <span class="span01">收货人姓名</span>
            <input class="input01" name="Name" maxlength="256" />
            <span class="span02">*收货人姓名</span>
        </div>
        <div style="height: 14px;" class="clear"></div>
        <div class="receipt_column">
            <span class="span01">收货地址</span>
            <input class="input02" name="Address" maxlength="256" />
            <span class="span02">*收货人的详细地址</span>
        </div>
        <div style="height: 14px;" class="clear"></div>
        <div class="receipt_column">
            <span class="span01">邮政编码</span>
            <input class="input01" name="ZipCode" maxlength="10" />
            <span class="span02">所在地区的邮政编码</span>
        </div>
        <div style="height: 14px;" class="clear"></div>
        <div class="receipt_column">
            <span class="span01">手机号码</span>
            <input class="input01" name="Mobile" maxlength="20" />
            <span class="span02">*收货人的手机号码</span>
        </div>
        <div style="height: 14px;" class="clear"></div>
        <div class="receipt_column">
            <span class="span01">联系电话</span>
            <input class="input01" maxlength="20" name="Phone" />
            <span class="span02">收货人的联系电话</span>
        </div>
        <div style="height: 14px;" class="clear"></div>
        <div class="receipt_column">
            <span class="span01">电子邮箱</span>
            <input class="input01" maxlength="20" name="Email" />
            <span class="span02">收货人的电子邮箱</span>
        </div>
    </div>

    <div style="height: 3px;" class="clear"></div>
    <div class="receipt_column">
        <div class="receipt">
            <span>2.支付方式</span>
        </div>

        <div style="height: 3px;" class="clear"></div>
        <div class="pay_content">
            <%
                //支付方式
                RenderPaymentMethodList();
            %>
        </div>
    </div>

    <div style="height: 3px;" class="clear"></div>
    <div class="receipt">
        <span>3.配送方式</span>
    </div>
    <div style="height: 3px;" class="clear"></div>
    <div class="pay_content">
        <% 
            //配送方式            
            RenderShippingMethodList();
        %>
    </div>


    <div style="height: 3px;" class="clear"></div>
    <div class="receipt">
        <span>4.产品清单</span>
    </div>
    <div style="height: 3px;" class="clear"></div>
    <div class="indent_shoppinglist_head">
        <span class="span01">商品名称</span>
        <span class="span02">积分</span>
        <span class="span03">单价</span>
        <span class="span04">数量</span>
        <span class="span05">优惠</span>
        <span class="span06">金额小计</span>
        <span class="span07">积分小计</span>
    </div>

    <%
        RenderGoodsBill();
    %>

    <div style="height: 3px;" class="clear"></div>
    <div class="receipt">
        <span>4.订单汇总</span>
    </div>
    <div style="height: 3px;" class="clear"></div>
    <div class="indent_settlement">
        <div class="message">
            <p>订单留言字数控制在100个字符内</p>
            <textarea cols="1" rows="1" name="ordermsg"></textarea>
        </div>
        <div class="message_text">
            <div>
                <span class="span01">商品件数：<%=pricer.TotalQuantity %> 件</span>
                <span class="span02">总积分：<%=pricer.TotalPoints %> 分</span>
            </div>
            <p>
                商品金额： ￥<%=pricer.Total.ToString("f2") %> + 
                            运费：￥<%=pricer.TotalMoneyforshipping.ToString("f2") %> + 
                            手续费： ￥<%=pricer.TotalMoneyforpayment.ToString("f2") %>
            </p>
            <div>
                <span class="span03">应付总金额：</span>
                <span class="span04">￥<%=(pricer.Total+pricer.TotalMoneyforshipping+pricer.TotalMoneyforpayment).ToString("f2") %></span>
                <input type="hidden" name="TotalPrice" value="<%=pricer.Total.ToString("f2") %>" />
                <input type="hidden" name="ShippingExpense" value="<%=pricer.TotalMoneyforshipping.ToString("f2") %>" />
                <input type="hidden" name="CommissionCharge" value="<%=pricer.TotalMoneyforpayment.ToString("f2") %>" />
                <input type="hidden" name="TotalPoints" value="<%=pricer.TotalPoints %>" />
            </div>
            <div class="message_down">
                <a href="javascript:;" onclick="submitOrder();" class="a1"></a>
                <a href="shoppingcart.aspx" class="a2"></a>
            </div>
        </div>
    </div>
</form>

<div style="height: 63px;" class="clear"></div>

    <script type="text/javascript">
        /*单项选择*/
        function radioSelect(sender, group, jqInp, id) {
            $('a.xuanzhong', group).removeClass('xuanzhong');
            $(sender).addClass('xuanzhong');
            $(jqInp).val(id);
        }

        //提交订单
        function submitOrder() {
            nt.ajaxSubmit('#OrderInfoForm', {
                action: 'SubmitOrder',
                ok: function (j) {
                    window.location.href = 'pay.aspx';
                }
            });
        }

    </script>
<uc1:foot runat="server" ID="foot" />

