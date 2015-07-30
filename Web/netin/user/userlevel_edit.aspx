<%@ Page Language="C#" AutoEventWireup="false" CodeFile="userlevel_edit.aspx.cs" Inherits="netin_user_userlevel_edit" %>

<%@ Register Src="~/netin/shared/head.ascx" TagPrefix="uc1" TagName="head" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:head runat="server" ID="head" />
    <title><%=Title %></title>
    <script type="text/javascript">
        function save() {

            if (editForm.Name.value === '') {
                alert('组名不能为空!');
                return;
            }

            modalDialogWinPostForm();
        }
    </script>
</head>
<body>
    <form action="userlevel_edit.aspx" method="post" id="editForm" target="_self">
        <table class="adminContent">
            <tr>
                <td class="adminTitle">组名:</td>
                <td class="adminData">
                    <input type="text" class="input-short" name="Name" maxlength="512" value="<%=Model.Name %>" /></td>
            </tr>

            <tr>
                <td class="adminTitle">启用:</td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.Active, "Active"); %>    
                </td>
            </tr>

            <tr>
                <td class="adminTitle">备注:</td>
                <td class="adminData">
                    <textarea cols="1" rows="2" class="textarea-small" name="Note"><%=Model.Note %></textarea>
                </td>
            </tr>

        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=Model.Id %>" />
            <input type="hidden" name="IsAdmin" value="False" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <a class="a-button" href="javascript:;" onclick="window.close();">取消</a>
        </div>
    </form>
</body>
</html>
