<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="orders.aspx.cs" Inherits="netin_sale_orders" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .order-msg { width: 400px; }
            .order-msg .html-content-body { min-height: 300px; }
        .order-consignee { width: 400px; }
            .order-consignee .html-content-body { min-height: 300px; }
            .order-consignee p { text-indent: 0; padding-left: 10px; line-height: 16px; }
        order-consignee p span { padding-left: 2px; }

        .status-setting { cursor: pointer; }
    </style>
    <script type="text/javascript">
        var ConsigneeInfoObj = {};

        /*查看订单留言*/
        function viewOrderMsg(id) {
            $('#orderMsg p').html($('#orderMsg_' + id).html());
            $('#orderMsg').center().movable();
            nt.showMask();
        }

        /*删除订单*/
        function deleteOrder(id) {
            confirm('您确定删除此订单?',function(){
                $.post(
                    '../handlers/delOrderHandler.ashx',
                    {id:id},
                    function(json){
                        if(json.error)
                            error(json.message);
                        else{
                            nt.reload();
                        }
                    },'json');
            });            
        }

        /*查看收货者信息*/
        function viewConsigneeInfo(id) {
            var m = ConsigneeInfoObj[id];
            $('#orderConsignee_name').text(m.name);
            $('#orderConsignee_mobile').text(m.mobile);
            $('#orderConsignee_phone').text(m.phone);
            $('#orderConsignee_email').text(m.email);
            $('#orderConsignee_address').text(m.address);
            $('#orderConsignee_zip').text(m.zip);
            $('#orderConsignee').center().movable();
            nt.showMask();
        }

        var shippingStatusData = <%=NtUtility.GetJsObjectArrayFromList(StaticDataProvider.Instance.ShippingStatusProvider)%>;
        var paymentStatusData = <%=NtUtility.GetJsObjectArrayFromList(StaticDataProvider.Instance.PaymentStatusProvider)%>;
        var orderStatusData = <%=NtUtility.GetJsObjectArrayFromList(StaticDataProvider.Instance.OrderStatusProvider)%>;

        /*改变订单状态*/
        function selectOrderStatus(sender,orderId){
            nt.openSelectionWindow(
                '配置订单状态',
                orderStatusData,
                sender.getAttribute('data-curr'),
                function(v,t){
                    nt.ajax({
                        data:{type:0,status:v,orderId:orderId},
                        action:'SetStatus',
                        success:function(json){
                            if(json.error)error(json.message);
                            else{
                                success(json.message);
                                sender.innerHTML=t;
                                sender.setAttribute('data-curr',v);
                            }
                        }
                    });
                });
        }

        /*改变配送状态*/
        function selectShippingStatus(sender,orderId){
            nt.openSelectionWindow(
                '配置配送状态',
                shippingStatusData,
                sender.getAttribute('data-curr'),
                function(v,t){
                    nt.ajax({
                        data:{type:1,status:v,orderId:orderId},
                        action:'SetStatus',
                        success:function(json){
                            if(json.error)error(json.message);
                            else{
                                success(json.message);
                                sender.innerHTML=t;
                                sender.setAttribute('data-curr',v);
                            }
                        }
                    });
                });
        }

        /*改变支付状态*/
        function selectPaymentStatus(sender,orderId){
            nt.openSelectionWindow(
                '配置支付状态',
                paymentStatusData,
                sender.getAttribute('data-curr'),
                function(v,t){
                    nt.ajax({
                        data:{type:2,status:v,orderId:orderId},
                        action:'SetStatus',
                        success:function(json){
                            if(json.error)error(json.message);
                            else{
                                success(json.message);
                                sender.innerHTML=t;
                                sender.setAttribute('data-curr',v);
                            }
                        }
                    });
                });
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <div class="list-tool-head">
        <form action="orders.aspx" method="get" id="searchForm">
            <table class="adminContent">
                <tr>
                    <td class="adminTitle">订单状态:</td>
                    <td class="adminData">
                        <%
                            NtUtility.ListItemSelect(StaticDataProvider.Instance.OrderStatusProvider, Request["se.order.status"]);
                            HtmlRenderer.DropDownList(StaticDataProvider.Instance.OrderStatusProvider,
                                "se.order.status", true, new NtListItem("全部", 0));
                        %>
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">订单配送状态:</td>
                    <td class="adminData">
                        <%
                            NtUtility.ListItemSelect(StaticDataProvider.Instance.ShippingStatusProvider, Request["se.order.shippingstatus"]);
                            HtmlRenderer.DropDownList(StaticDataProvider.Instance.ShippingStatusProvider,
                                "se.order.shippingstatus", true, new NtListItem("全部", 0));
                        %>
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">订单支付状态:</td>
                    <td class="adminData">
                        <%
                            NtUtility.ListItemSelect(StaticDataProvider.Instance.PaymentStatusProvider, Request["se.order.paymentstatus"]);
                            HtmlRenderer.DropDownList(StaticDataProvider.Instance.PaymentStatusProvider,
                                "se.order.paymentstatus", true, new NtListItem("全部", 0));
                        %>
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">订单ID号:</td>
                    <td class="adminData">
                        <input type="text" class="input-short" name="se.order.id" value="<%=Default("se.order.id",string.Empty) %>" />
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">订单创建日期开始于:</td>
                    <td class="adminData">
                        <input type="text" class="input-datetime" name="se.order.createddate.start" value="<%=Default("se.order.createddate.start",NtConfig.MinDate.ToString("yyyy-MM-dd"))%>" />
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">订单创建日期结束于:</td>
                    <td class="adminData">
                        <input type="text" class="input-datetime" name="se.order.createddate.end" value="<%=Default("se.order.createddate.end",NtConfig.MaxDate.ToString("yyyy-MM-dd"))%>" />
                    </td>
                </tr>
            </table>
            <div class="submit">
                <input type="hidden" name="page" value="<%=Default("page",1) %>" />
                <a class="a-button" href="javascript:;" onclick="searchForm.submit();">搜索</a>
                <a class="a-button" href="javascript:;" onclick="clearSeach();">清除搜索</a>

                <a href="?se.cancelled.order=true">已取消的订单(<span class="red"><%=CancelledOrderCount %></span>)</a>
                <a href="?se.pending.order=true">等待处理的订单(<span class="red"><%=PendingOrderCount %></span>)</a>
                <a href="?se.processing.order=true">正在处理的订单(<span class="red"><%=ProcessingOrderCount %></span>)</a>
                <a href="?se.complete.order=true">已处理的订单(<span class="red"><%=CompleteOrderCount %></span>)</a>
                <a href="?se.paid.order=true">收款单(<span class="red"><%=PaidOrderCount %></span>)</a>
                <a href="?se.refunded.order=true">退款单(<span class="red"><%=RefundedOrderCount %></span>)</a>

            </div>
        </form>
    </div>
    <div class="list" id="list">
        <%List(); %>
    </div>
    <div class="html-content-wrap">
        <div class="order-msg html-content" id="orderMsg">
            <div class="html-content-top nt-drag-bar">
                <span class="html-content-title">订单留言</span>
                <a onclick="$('#orderMsg').hide();nt.removeMask();" href="javascript:;">x</a>
            </div>
            <div class="html-content-body">
                <p></p>
            </div>
        </div>

        <div class="order-consignee html-content" id="orderConsignee">
            <div class="html-content-top nt-drag-bar">
                <span class="html-content-title">收货者信息</span>
                <a onclick="$('#orderConsignee').hide();nt.removeMask();" href="javascript:;">x</a>
            </div>
            <div class="html-content-body">
                <table class="adminContent adminContentView">
                    <tr>
                        <th class="adminTitle">姓名:</th>
                        <td class="adminData">
                            <label id="orderConsignee_name"></label>
                        </td>
                    </tr>
                    <tr>
                        <th class="adminTitle">手机:</th>
                        <td class="adminData">
                            <label id="orderConsignee_mobile"></label>
                        </td>
                    </tr>
                    <tr>
                        <th class="adminTitle">固话:</th>
                        <td class="adminData">
                            <label id="orderConsignee_phone"></label>
                        </td>
                    </tr>
                    <tr>
                        <th class="adminTitle">Email:</th>
                        <td class="adminData">
                            <label id="orderConsignee_email"></label>
                        </td>
                    </tr>
                    <tr>
                        <th class="adminTitle">地址:</th>
                        <td class="adminData">
                            <label id="orderConsignee_address"></label>
                        </td>
                    </tr>
                    <tr>
                        <th class="adminTitle">邮编:</th>
                        <td class="adminData">
                            <label id="orderConsignee_zip"></label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>



</asp:Content>
