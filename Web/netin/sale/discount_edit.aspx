<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="discount_edit.aspx.cs" Inherits="netin_sale_discount_edit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">

    <script type="text/javascript">
        function save() {

            editForm.submit();
        }
    </script>

</asp:Content>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <form action="discount_edit.aspx" method="post" id="editForm">
        <table class="adminContent" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="adminTitle">折扣名:</td>
                <td class="adminData">
                    <input type="text" class="input-short" maxlength="256" name="Name" value="<%=Model.Name %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">
                    使用百分数:
                </td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.UsePercentage, "UsePercentage"); %>
                </td>
            </tr>

             <tr>
                <td class="adminTitle">
                    折扣百分数（折后）:
                </td>
                <td class="adminData">
                    <input type="text" class="input-decimal" maxlength="20" name="DiscountPercentage" value="<%=Model.DiscountPercentage %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">折扣量（不使用折扣百分比）:</td>
                <td class="adminData">
                    <input type="text" class="input-decimal" maxlength="20" name="DiscountAmount" value="<%=Model.DiscountAmount %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle" title="请勿超过1024个字符">描述:
                </td>
                <td class="adminData">
                    <textarea cols="20" rows="5" class="textarea-normal" name="Description"><%=Server.HtmlEncode(Model.Description) %></textarea>
                </td>
            </tr>

        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=NtID %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <a class="a-button" href="<%BackScript(); %>">返回</a>
        </div>
    </form>
</asp:Content>
