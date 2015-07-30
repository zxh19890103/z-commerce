<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pay.aspx.cs" Inherits="user_pay" %>

<%@ Register Src="~/shared/qb_top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/qb_foot.ascx" TagPrefix="uc1" TagName="foot" %>

<uc1:top runat="server" ID="top" />

<div style="height: 57px;" class="clear"></div>
<p class="shoppingcart_title">我的购物车</p>
<div style="height: 45px;" class="clear"></div>
<div class="shoppingmenv">
    <a class="menv04" href="#">1.放进购物车</a>
    <a class="menv05" href="#">2.填写订单信息</a>
    <a class="menv06" href="#">3.支付/确认订单</a>
</div>
<div style="height: 37px;" class="clear"></div>
<div class="receipt">
    <span>支付中心</span>
    <a href="#">返回购物车</a>
</div>
<div style="height: 3px;" class="clear"></div>
<div class="payment_content">
    <div class="pay_text">
        <span>订单号：</span>
        <span class="span1"><%=Model.OrderGuid %></span>
    </div>
    <div class="pay_text">
        <span>收货人姓名：</span>
        <span class="span1"><%=Model.Name %></span>
    </div>
    <div class="pay_text">
        <span>送货地址：</span>
        <span class="span1"><%=Model.Address %></span>
    </div>
    <div class="pay_text">
        <span>联系方式：</span>
        <span class="span1"><%=Model.Mobile %></span>
    </div>
    <div class="pay_text">
        <span>支付金额：</span>
        <span class="span1">￥</span>
        <span class="span2"><%=(Model.OrderTotal- Model.OrderTotalDiscount).ToString("f2")%></span>
    </div>
    <div class="pay_text">
        <span>支付方式：</span>
        <span class="span1"><%=Model.PaymentMethod %></span>
    </div>
    <div class="pay_text">
        <span class="liuyan">备注留言：</span>
        <span class="span1 liuyan">
            <%=Model.OrderMessage %>
        </span>
    </div>
</div>
<div style="height: 22px;" class="clear"></div>
<div class="message_down">
    <a href="javascript:;" class="a1" onclick="alert('确认支付');"></a>
    <a href="javascript:;" class="a2" onclick="alert('订单已经提交，暂不支持返回修改!');"></a>
</div>

<div style="height: 72px;" class="clear"></div>

<uc1:foot runat="server" ID="foot" />
