<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false"
    CodeFile="order_info.aspx.cs" Inherits="netin_sale_order_info" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#order_tab').tabs();
        });
        
        /*
        取消订单
        */
        function CancelOrder() {
            var cancelled=<%=Model.PaymentStatus==(int)Nt.Model.Enum.OrderStatus.Cancelled?"true":"false"%>;
            if(cancelled){
                error('订单已取消');
                return;
            }
            nt.ajax({
                action: 'CancelOrder',
                data: {id:<%=Model.Id%>},
                success:function(json){
                    if(json.error)
                        error(json.message);
                    else{
                        $('#cancelOrderMsg').text('取消订单!');
                        success('订单取消!');
                    }
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <div class="nt-tabs-wrap" id="order_tab">
        <div class="nt-tabs-wrap-inner">
            <ul class="nt-tabs">
                <li class="active"><a href="javascript:;">订单基本信息</a></li>
                <li><a href="javascript:;">收货者信息</a></li>
                <li><a href="javascript:;">购买商品</a></li>
                <li><a href="javascript:;">订单备注</a></li>
            </ul>
            <div class="nt-tabs-content-wrap">
                <div class="tab-item tab-item-selected">
                    <table class="adminContent adminContentView">
                        <tr>
                            <td class="adminTitle">取消订单:
                            </td>
                            <td class="adminData">
                                <span class="tips" id="cancelOrderMsg">
                                    <%=Model.Deleted?"作废订单":"" %></span> <a class="a-button" href="javascript:;" onclick="CancelOrder();">取消订单</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">订单状态:
                            </td>
                            <td class="adminData">
                                <%=FindTextByValue(StaticDataProvider.Instance.OrderStatusProvider,Model.Status) %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">订单ID:
                            </td>
                            <td class="adminData">
                                <%=Model.Id %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">GUID:
                            </td>
                            <td class="adminData">
                                <%=Model.OrderGuid %>
                            </td>
                        </tr>
                        <tr class="adminSeparator">
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">会员:
                            </td>
                            <td class="adminData">
                                <a href="javascript:;" onclick="openWindow('../customer/info_view.aspx?id=<%=Model.CustomerId %>');">查看会员信息:<%=Model.CustomerId %></a>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">会员IP:
                            </td>
                            <td class="adminData">
                                <%=Model.CustomerIp %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">订单运输费:
                            </td>
                            <td class="adminData">
                                <%RenderMoney(Model.ShippingExpense); %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">订单支付手续费:
                            </td>
                            <td class="adminData">
                                <%RenderMoney(Model.CommissionCharge); %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">订单折扣:
                            </td>
                            <td class="adminData">
                                <%RenderMoney(Model.OrderTotalDiscount); %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">订单总额:
                            </td>
                            <td class="adminData">
                                <%RenderMoney(Model.OrderTotal); %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">应付额(总额-总折扣-支付网关费-运费):
                            </td>
                            <td class="adminData">
                                <%RenderMoney(Model.OrderTotal - Model.OrderTotalDiscount - Model.ShippingExpense - Model.CommissionCharge); %>
                            </td>
                        </tr>
                        <tr class="adminSeparator">
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">支付状态:
                            </td>
                            <td class="adminData">
                                <%=FindTextByValue(StaticDataProvider.Instance.PaymentStatusProvider,Model.PaymentStatus) %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">支付方式:
                            </td>
                            <td class="adminData">
                                <%=Model.PaymentMethod %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">下单日期:
                            </td>
                            <td class="adminData">
                                <%=Model.CreatedDate %>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tab-item">
                    <table class="adminContent adminContentView">
                        <tr>
                            <td class="adminTitle">收货地址:
                            </td>
                            <td class="adminData">
                                <table style="border: 1px solid #000; width: 25%; margin: 0;">
                                    <tr>
                                        <td>姓名:
                                        </td>
                                        <td>
                                            <%=Model.Name %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>手机:
                                        </td>
                                        <td>
                                            <%=Model.Mobile %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>固话:
                                        </td>
                                        <td>
                                            <%=Model.Phone %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>现住址:
                                        </td>
                                        <td>
                                            <%=Model.Address %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>邮编:
                                        </td>
                                        <td>
                                            <%=Model.Zip %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>邮箱地址:
                                        </td>
                                        <td>
                                            <%=Model.Email %>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">配送状态:
                            </td>
                            <td class="adminData">
                                <%=FindTextByValue(StaticDataProvider.Instance.ShippingStatusProvider,Model.ShippingStatus) %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">配送方式:
                            </td>
                            <td class="adminData">
                                <%=Model.ShippingMethod %>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tab-item">
                    <%RenderOrderItems(); %>
                </div>
                <div class="tab-item">
                    <script type="text/javascript">

                        var orderNoteMgr = new nt.tableMgr({
                            editor: '#orderNoteEditor',
                            form: '#orderNoteForm',
                            listDivId: 'orderNotes',
                            title:'订单备注'
                        });
                        orderNoteMgr.init();
                    </script>
                    <div id="orderNotes">
                        <%RenderOrderNote(); %>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="submit">
        <input type="hidden" name="Id" value="<%=Model.Id %>" />
        <a class="a-button" href="orders.aspx">返回</a>
    </div>
    <!--编辑对话框-->
    <div class="html-content-wrap">
        <div id="orderNoteEditor" class="html-content">
            <div class="html-content-top  nt-drag-bar">
                <span class="html-content-title">编辑订单备注 </span><a href="javascript:;" role="close">x</a>
            </div>
            <div class="html-content-body">
                <form action="order_info.aspx" method="post" id="orderNoteForm">
                    <table class="adminContent">
                        <tr>
                            <td class="adminTitle">备注内容：
                            </td>
                            <td class="adminData">
                                <textarea cols="1" rows="2" class="textarea-small" name="Note"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">是否开放于用户：
                            </td>
                            <td class="adminData">
                                <%HtmlRenderer.CheckBox(true, "DisplayToCustomer"); %>
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" name="CreatedDate" value="<%=Model.CreatedDate %>" />
                    <input type="hidden" name="OrderId" value="<%=Model.Id %>" />
                    <input type="hidden" name="Id" value="0" />
                </form>
            </div>
            <div class="html-content-footer">
                <a href="javascript:;" role="post" class="a-button">保存</a> <a href="javascript:;"
                    role="close" class="a-button">关闭</a>
            </div>
        </div>
    </div>
</asp:Content>
