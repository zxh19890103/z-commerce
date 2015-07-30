<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="review_edit.aspx.cs" Inherits="netin_goods_review_edit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function save() {
            if (editForm.Title.value === '') {
                error('标题不能为空!');
                editForm.Title.focus();
            }
            editForm.submit();
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <form action="review_edit.aspx" method="post" id="editForm">
        <table class="adminContent">
            <tr>
                <td class="adminTitle">商品:</td>
                <td class="adminData">
                    <a href="goods_edit.aspx?id=<%=Model.GoodsId %>"><%=Model.GoodsName %></a>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">会员:</td>
                <td class="adminData">
                    <a href="javascript:;" onclick="openWindow('../customer/info_view.aspx?id=<%=Model.CustomerId %>');"><%=Model.CustomerName %></a>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">审核通过:</td>
                <td class="adminData"><%HtmlRenderer.CheckBox(Model.IsApproved, "IsApproved"); %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">标题:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="Title" value="<%=Model.Title %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">内容:
                </td>
                <td>
                    <textarea cols="20" rows="5" class="textarea-small" name="Body"><%=Model.Body %></textarea>
                    <span class="tips">请勿超过1024个字符</span>

                </td>
            </tr>

            <tr>
                <td class="adminTitle">查看次数:</td>
                <td class="adminData">
                    <input type="text" class="input-number" maxlength="9" name="Rating" value="<%=Model.Rating %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">创建日期:</td>
                <td class="adminData">
                    <%HtmlRenderer.DateTimePicker(Model.CreatedDate, "CreatedDate"); %>
                </td>
            </tr>

        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=NtID %>" />
            <input type="hidden" name="GoodsId" value="<%=Model.GoodsId %>" />
            <input type="hidden" name="CustomerId" value="<%=Model.CustomerId %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <a class="a-button" href="review.aspx">返回</a>
        </div>
    </form>
</asp:Content>
