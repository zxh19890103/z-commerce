<%@ Page Title="" Language="C#" AutoEventWireup="false" CodeFile="reply_config.aspx.cs"
    Inherits="netin_guestbook_reply_config" %>

<%@ Register Src="~/netin/shared/head.ascx" TagPrefix="uc1" TagName="head" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:head runat="server" ID="head" />
    <title>
        <%=Title %></title>
    <script type="text/javascript">
        function save() {
            modalDialogWinPostForm();
        }        
    </script>
</head>
<body>
    <form action="reply_config.aspx" method="post" id="editForm">
    <table class="adminContent">
        <tr>
            <td class="adminTitle">
                回复人:
            </td>
            <td class="adminData">
                <input type="text" class="input-short" name="ReplyMan" maxlength="512" value="<%=Model.ReplyMan %>" />
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                回复内容:
            </td>
            <td class="adminData">
                <textarea name="ReplyContent" cols="1" rows="2" class="textarea-small"><%=Model.ReplyContent %></textarea>
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                公开:
            </td>
            <td class="adminData">
                <%HtmlRenderer.CheckBox(Model.Display, "Display"); %>
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                排序:
            </td>
            <td class="adminData">
                <input type="text" class="input-number" maxlength="9" name="DisplayOrder" value="<%=Model.DisplayOrder %>" />
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                回复日期:
            </td>
            <td class="adminData">
                <%HtmlRenderer.DateTimePicker(Model.ReplyDate, "ReplyDate"); %>
            </td>
        </tr>
    </table>
    <div class="submit">
        <input type="hidden" name="Id" value="<%=Model.Id %>" />
        <input type="hidden" name="GuestBookId" value="<%=book %>" />
        <a class="a-button" href="javascript:;" onclick="save();">保存</a>
        <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
        <a class="a-button" href="javascript:;" onclick="window.close();">关闭</a>
    </div>
    </form>
    <script src="../script/datetime.js" type="text/javascript"></script>
</body>
</html>
