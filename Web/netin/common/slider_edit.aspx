<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="slider_edit.aspx.cs" Inherits="netin_common_slider_edit" %>

<%@ Register Src="~/netin/shared/uploader.ascx" TagPrefix="uc1" TagName="uploader" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        //保存
        function save() {
            editForm.submit();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <form action="slider_edit.aspx" method="post" id="editForm">
        <table class="adminContent">
            <tr>
                <td class="adminTitle">图片:</td>
                <td class="adminData">
                    <%uploader1.FieldValue = Model.PictureUrl; %>
                    <uc1:uploader runat="server" ID="uploader1" FieldName="PictureUrl" PostUrl="/Netin/handlers/uploadHandler.aspx" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">标题:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="Title" value="<%=Model.Title %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">Url(链接):</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="Url" value="<%=Model.Url %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">生效:</td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.Display, "Display"); %>
                </td>
            </tr>

               <tr>
                <td class="adminTitle">排序:</td>
                <td class="adminData">
                    <input type="text" class="input-number" maxlength="9" name="DisplayOrder" value="<%=Model.DisplayOrder %>" />
                </td>
            </tr>
                        
        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=NtID %>" />
            <input type="hidden" name="LanguageId" value="<%=Model.LanguageId %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <a class="a-button" href="<%BackScript(); %>">返回</a>
        </div>
    </form>
</asp:Content>
