<%@ Page Language="C#" AutoEventWireup="false" CodeFile="campaign_edit.aspx.cs" Inherits="netin_sale_campaign_edit" %>

<%@ Register Src="~/netin/shared/head.ascx" TagPrefix="uc1" TagName="head" %>
<%@ Register Src="~/netin/shared/editor.ascx" TagPrefix="uc1" TagName="editor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:head runat="server" ID="head" />
    <title><%=Title %></title>
    <script type="text/javascript">
        function save() {
            if (editForm.Name.value === '') {
                alert('活动名不能为空!', function () {
                    editForm.Name.focus();
                });
                return;
            }
            if (editForm.Subject.value === '') {
                alert('活动主题能为空!', function () {
                    editForm.Subject.focus();
                });
                return;
            }
            editor.sync();

            if (editForm.Body.value === '') {
                alert('活动内容不能为空!', function () {
                    editForm.Body.focus();
                });
                return;
            }

            editForm.submit();
        }

    </script>
    <uc1:editor runat="server" ID="editor" SimpleEditor="true" TextareaName="Body" />
</head>
<body>
    <form action="campaign_edit.aspx" method="post" id="editForm">
        <table class="adminContent">
            <tr>
                <td class="adminTitle">活动名称:</td>
                <td class="adminData">
                    <input type="text" class="input-short" name="Name" maxlength="512" value="<%=Model.Name %>" /></td>
            </tr>
            <tr>
                <td class="adminTitle">主题:</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="Subject" maxlength="1024" value="<%=Model.Subject %>" /></td>
            </tr>
            <tr>
                <td class="adminTitle">活动内容:</td>
                <td class="adminData">
                    <textarea name="Body" cols="1" rows="2" class="textarea-keditor"><%=Server.HtmlEncode(Model.Body) %></textarea></td>
            </tr>

            <tr>
                <td class="adminTitle">发布:</td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.Display, "Display"); %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">排序:</td>
                <td class="adminData">
                    <input type="text" class="input-number" name="DisplayOrder" value="<%=Model.DisplayOrder %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">发布日期:</td>
                <td class="adminData">
                    <%HtmlRenderer.DateTimePicker(Model.CreatedDate, "CreatedDate"); %>
                </td>
            </tr>

        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=Model.Id %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <a class="a-button" href="javascript:;" onclick="window.close();">取消</a>
        </div>
    </form>
</body>
</html>
