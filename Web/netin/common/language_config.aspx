<%@ Page Language="C#" AutoEventWireup="false" CodeFile="language_config.aspx.cs" Inherits="netin_common_language_config" %>

<%@ Register Src="~/netin/shared/head.ascx" TagPrefix="uc1" TagName="head" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:head runat="server" ID="head" />
    <title><%=Title %></title>
    <style type="text/css">
        #flags { display: none; width: 600px; background-color: #fff; border: 1px solid #ff6a00; }
            #flags ul { display: block; width: 100%; margin: 0; padding: 3px; }
                #flags ul li { display: block; width: 16px; height: 11px; margin: 3px; float: left; cursor: pointer; }
    </style>
    <script type="text/javascript">
        /*保存表单*/
        function save() {
            if (editForm.Name.value == '') {
                alert('语言名不能为空！');
                return;
            }

            modalDialogWinPostForm();
        }

    </script>
</head>
<body>
    <div class="edit-area">
        <form action="language_config.aspx" method="post" id="editForm">
            <table class="adminContent">
                <tr>
                    <td class="adminTitle">语言名:</td>
                    <td class="adminData">
                        <input type="text" class="input-short" name="Name" maxlength="512" value="<%=Model.Name %>" /></td>
                </tr>
                <tr>
                    <td class="adminTitle">发布:</td>
                    <td class="adminData">
                        <%HtmlRenderer.CheckBox(Model.Published, "Published"); %>    
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">排序:</td>
                    <td class="adminData">
                        <input type="text" class="input-number" name="DisplayOrder" maxlength="9" value="<%=Model.DisplayOrder %>" />
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">语言符号:</td>
                    <td class="adminData">
                        <input type="text" class="input-short" onfocus="$(flags).center();nt.showMask();" readonly="readonly" name="LanguageCode" value="<%=Model.LanguageCode %>" />
                        <img id="LangCodeImg" alt="<%=Model.LanguageCode  %>" src="../content/flags/<%=Model.LanguageCode %>.png" />
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
    <div id="flags">
        <ul>
            <%
                foreach (var item in LanguageCodeProvider.LanguageCodes)
                {
                    Html("<li title=\"{0}\"><img onclick=\"editForm.LanguageCode.value=this.alt;$('#LangCodeImg').attr('src',this.src);$('#flags').hide();nt.removeMask();\" src=\"../content/flags/{0}.png\" alt=\"{0}\" width=\"16\" height=\"11\"/></li>", item);
                }
            %>
        </ul>
    </div>
</body>
</html>
