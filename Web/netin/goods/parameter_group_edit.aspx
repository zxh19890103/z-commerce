<%@ Page Language="C#" AutoEventWireup="false" CodeFile="parameter_group_edit.aspx.cs" Inherits="netin_goods_parameter_group_edit" %>

<%@ Register Src="~/netin/shared/head.ascx" TagPrefix="uc1" TagName="head" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:head runat="server" ID="head" />
    <title><%=Title %></title>
    <script type="text/javascript">
        //提交
        function save() {
            if (editForm.GroupName.value == '') {
                alert('参数组名不能为空！');
                return;
            }
            modalDialogWinPostForm();
        }
    </script>
</head>
<body>
    <div class="edit-area">
        <form action="parameter_group_edit.aspx" method="post" id="editForm">
            <table class="adminContent">
                <tr>
                    <td class="adminTitle">参数组名:</td>
                    <td class="adminData">
                        <input type="text" class="input-short" name="GroupName" maxlength="512" value="<%=Model.GroupName %>" /></td>
                </tr>
                <tr>
                    <td class="adminTitle">排序:</td>
                    <td class="adminData">
                        <input type="text" class="input-number" maxlength="9" name="DisplayOrder" value="<%=Model.DisplayOrder %>" /></td>
                </tr>
                <tr>
                    <td class="adminTitle">生效:</td>
                    <td class="adminData">
                        <%HtmlRenderer.CheckBox(Model.Display, "Display"); %>
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">参数描述:</td>
                    <td class="adminData">
                        <textarea name="Description" cols="1" rows="2" class="textarea-small"><%=Model.Description %></textarea>
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
    </div>
</body>
</html>
