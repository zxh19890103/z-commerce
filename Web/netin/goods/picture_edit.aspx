<%@ Page Language="C#" AutoEventWireup="false" CodeFile="picture_edit.aspx.cs" Inherits="Netin_Goods_picture_edit" %>

<%@ Register Src="~/netin/shared/head.ascx" TagPrefix="uc1" TagName="head" %>
<%@ Register Src="~/netin/shared/uploader.ascx" TagPrefix="uc1" TagName="uploader" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>图片信息编辑</title>
    <uc1:head runat="server" ID="head" />
    <script type="text/javascript">
        function save() {
            $('#editForm').ajaxSubmit2({
                action: 'Post',
                success: function (json) {
                    if (json.error) error(json.message);
                    else {
                        GetOpener().refreshList('uploadedPics', 'Ajax_RenderPictures', false);//refresh the pictures ui
                        window.close();
                    }
                }
            });
        }
    </script>
</head>
<body>

    <form action="picture_edit.aspx" method="post" id="editForm">
        <table class="adminContent">
            <tr>
                <td class="adminTitle">图片:</td>
                <td class="adminData">
                    <uc1:uploader runat="server" ID="uploader"  FieldName="Src" IsFile="false" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">标题:</td>
                <td class="adminData">
                    <input type="text" class="input-short" name="Title" maxlength="512" value="<%=Model.Title %>" /></td>
            </tr>
            <tr>
                <td class="adminTitle">Alt:</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="Alt" maxlength="1024" value="<%=Model.Alt %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">描述:</td>
                <td class="adminData">
                    <textarea name="Description" rows="1" cols="2" class="textarea-normal"><%=Model.Description %></textarea>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">排序:</td>
                <td class="adminData">
                    <input type="text" class="input-number" maxlength="9" name="DisplayOrder" value="<%=Model.DisplayOrder %>" /></td>
            </tr>
        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=Model.Id %>" />
            <input type="hidden" name="Display" value="<%=Model.Display%>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <a class="a-button" href="javascript:;" onclick="window.close();">关闭窗口</a>
        </div>
    </form>

</body>
</html>
