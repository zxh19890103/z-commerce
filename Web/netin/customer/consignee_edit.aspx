<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="consignee_edit.aspx.cs" Inherits="netin_customer_consignee_edit" %>

<asp:Content runat="server" ContentPlaceHolderID="head">

    <script type="text/javascript">
        /*保存*/
        function save() {
            editForm.submit();
        }
    </script>

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <form action="<%=Request.RawUrl %>" method="post" id="editForm">
        <table class="adminContent">
            <tr>
                <td class="adminTitle">姓名:</td>
                <td class="adminData">
                    <input type="text" class="input-short" maxlength="256" name="Name" value="<%=Model.Name %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">邮箱:</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="Email" value="<%=Model.Email %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">固话:</td>
                <td class="adminData">
                    <input type="text" class="input-normal" name="Phone" maxlength="20" value="<%=Model.Phone %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">手机:</td>
                <td class="adminData">
                    <input type="text" class="input-normal" name="Mobile" maxlength="20" value="<%=Model.Mobile %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">邮编:</td>
                <td class="adminData">
                    <input type="text" class="input-short" name="Zip" maxlength="6" value="<%=Model.Zip %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">住址:</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="Address" maxlength="512" value="<%=Model.Address %>" />
                </td>
            </tr>

        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=NtID %>" />
            <input type="hidden" name="CustomerId" value="<%=Model.CustomerId %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <a class="a-button" href="<%BackScript(); %>">返回</a>
        </div>
    </form>
</asp:Content>
