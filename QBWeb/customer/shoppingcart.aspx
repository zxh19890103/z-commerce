<%@ Page Language="C#" AutoEventWireup="true" Debug="true" CodeFile="shoppingcart.aspx.cs" Inherits="user_shoppingcart" %>

<%@ Register Src="~/shared/qb_top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/qb_foot.ascx" TagPrefix="uc1" TagName="foot" %>

<uc1:top runat="server" ID="top" />


<div style="height: 57px;" class="clear"></div>

<p class="shoppingcart_title">我的购物车</p>
<div style="height: 45px;" class="clear"></div>
<div class="shoppingmenv">
    <a class="menv01" href="#">1.放进购物车</a>
    <a class="menv02" href="#">2.填写订单信息</a>
    <a class="menv03" href="#">3.支付/确认订单</a>
</div>
<div style="height: 37px;" class="clear"></div>

<div class="shoppinglist_head">
    <span class="span01">商品名称</span>
    <span class="span02">积分</span>
    <span class="span03">单价</span>
    <span class="span04">数量</span>
    <span class="span05">优惠</span>
    <span class="span06">金额小计</span>
    <span class="span07">积分小计</span>
    <a class="a1">操作</a>
</div>

<div id="shopingcart-info">
    <%RenderShoppingCart(); %>
</div>

<div style="height: 20px;" class="clear"></div>
<div class="shopping_bt">
    <a class="shoppingon" href="/list.aspx"></a>
    <a class="settlement" href="orderinfo.aspx"></a>
</div>

<div style="height: 142px;" class="clear"></div>

<script type="text/javascript">
    /*
    增加（减少）购物车里的商品的数量，每次增加（减少）一个
    */
    function setQuantity(sender, id, addNum) {
        var tar = $(sender);
        var num = parseInt(tar.siblings('input#Quantity_' + id).val());
        if (num < 2 && addNum < 0) return;
        num += addNum;
        nt.ajax({
            data: { quantity: num, id: id },
            action: 'SetQuantity',
            success: function (json) {
                if (json.error == 1) { alert(json.message); }
                else {
                    refreshCart();
                }
            }
        });
    }

    /*刷新购物车*/
    function refreshCart() {
        nt.ajax({
            url: 'shoppingcart.aspx',
            action: 'RenderShoppingCart',
            success: function (text) {
                $('#shopingcart-info').html(text);
            },
            type: 'text'
        });
    }

    /*
    移除商品
    */
    function delFromCart(id) {
        confirm('您确定删除?', function () {
            nt.ajax({
                data: { id: id },
                action: 'DelFromCart',
                success: function (json) {
                    if (json.error == 1) { alert(json.message); }
                    else {
                        refreshCart();
                    }
                }
            });
        });
    }
    </script>

<uc1:foot runat="server" ID="foot" />


