<%@ Page Language="C#" AutoEventWireup="false" CodeFile="attribute_edit.aspx.cs" Inherits="netin_goods_attribute_edit" %>

<%@ Register Src="~/netin/shared/head.ascx" TagPrefix="uc1" TagName="head" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:head runat="server" ID="head" />
    <title><%=Title %></title>
    <script type="text/javascript">
        //提交
        function save() {
            if (editForm.Name.value == '') {
                alert('属性名不能为空！');
                return;
            }
            modalDialogWinPostForm();
        }
    </script>
</head>
<body>
    <div class="edit-area">
        <form action="attribute_edit.aspx" method="post" id="editForm">
            <table class="adminContent">
                <tr>
                    <td class="adminTitle">属性名:</td>
                    <td class="adminData">
                        <input type="text" class="input-short" name="Name" maxlength="512" value="<%=Model.Name %>" /></td>
                </tr>
                <tr>
                    <td class="adminTitle">文字说明:</td>
                    <td class="adminData">
                        <textarea name="Description" cols="1" rows="2" class="textarea-small"><%=Model.Description %></textarea></td>
                </tr>
            </table>
            <div class="submit">
                <input type="hidden" name="Id" value="<%=Model.Id %>" />
                <a class="a-button" href="javascript:;" onclick="save();">保存</a>
                <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
                <a class="a-button" href="javascript:;" onclick="window.close();">关闭</a>
            </div>
        </form>
    </div>
</body>
</html>
