<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="user_edit.aspx.cs" Inherits="netin_user_user_edit" %>

<%@ Register Src="~/netin/shared/uploader.ascx" TagPrefix="uc1" TagName="uploader" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        /*保存*/
        function save() {
            editForm.submit();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <form action="user_edit.aspx" method="post" id="editForm">
        <table class="adminContent">
            <tr>
                <td class="adminTitle">用户组:</td>
                <td class="adminData">
                    <%
                        NtUtility.ListItemSelect(UserLevels, Model.UserLevelId);
                        HtmlRenderer.DropDownList(UserLevels, "UserLevelId");
                    %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">用户名:</td>
                <td class="adminData">
                    <input type="text" <%=IsEdit?"readonly":"" %> class="input-short" maxlength="256" name="UserName" value="<%=Model.UserName %>" />
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
                <td class="adminTitle">邮箱:</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="Email" value="<%=Model.Email %>" />
                </td>
            </tr>

             <tr>
                <td class="adminTitle">头像:</td>
                <td class="adminData">
                    <uc1:uploader runat="server" ID="uploader"  FieldName="Profile" />
                </td>
            </tr>

        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=NtID %>" />
            <input type="hidden" name="Deleted" value="<%=Model.Deleted %>" />
            <input type="hidden" name="CreatedUserId" value="<%=Model.CreatedUserId %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <a class="a-button" href="users.aspx">返回</a>
        </div>
    </form>
</asp:Content>
