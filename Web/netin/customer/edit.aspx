<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Netin_Customer_edit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        /*保存*/
        function save() {
            if (!editForm.CustomerRoleId.value) {
                alert('请选择会员组!');
                return;
            }
            editForm.submit();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <form action="edit.aspx" method="post" id="editForm">
        <table class="adminContent">
            <tr>
                <td class="adminTitle">会员组:</td>
                <td class="adminData">
                    <%
                        NtUtility.ListItemSelect(CustomerRoles, Model.CustomerRoleId);
                        HtmlRenderer.DropDownList(CustomerRoles, "CustomerRoleId");
                    %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">会员名:</td>
                <td class="adminData">
                    <input type="text" <%=IsEdit?"readonly":"" %> class="input-short" maxlength="256" name="Name" value="<%=Model.Name %>" />
                </td>
            </tr>

            <%if (!IsEdit)
              {%>

            <tr>
                <td class="adminTitle">密码:</td>
                <td class="adminData">
                    <input type="password" class="input-short" name="Password" value="<%=Model.Password %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">再次输入密码:</td>
                <td class="adminData">
                    <input type="password" class="input-short" name="Password2" value="" />
                </td>
            </tr>

            <%} %>

            <tr>
                <td class="adminTitle">启用:</td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.Active, "Active"); %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">性别:</td>
                <td class="adminData">
                    <%HtmlRenderer.XY(Model.Gender, "Gender"); %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">真实姓名:</td>
                <td class="adminData">
                    <input type="text" class="input-normal" name="RealName" value="<%=Model.RealName %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">生日:</td>
                <td class="adminData">
                    <%HtmlRenderer.BirthDay(Model.BirthDay, "BirthDay"); %>
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
            <input type="hidden" name="CreatedDate" value="<%=Model.CreatedDate %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <a class="a-button" href="list.aspx">返回</a>
        </div>
    </form>
</asp:Content>
