<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="edit.aspx.cs" Inherits="netin_single_edit" %>

<%@ Register Src="~/netin/shared/editor.ascx" TagPrefix="uc1" TagName="editor" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        //保存
        function save() {
            editor.sync();
            editForm.submit();
        }

    </script>

    <uc1:editor runat="server" ID="editor" TextareaName="Body" />

</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <form action="edit.aspx" method="post" id="editForm">
        <table class="adminContent">
            <tr>
                <td class="adminTitle">标题:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="Title" value="<%=Model.Title %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">简述:</td>
                <td class="adminData">
                    <textarea cols="2" rows="3" name="Short" class="textarea-small"><%=Model.Short %></textarea>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">内容:</td>
                <td class="adminData">
                    <textarea cols="2" rows="3" name="Body" class="textarea-keditor"><%=Model.Body %></textarea>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">Seo标题:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="SeoTitle" value="<%=Model.SeoTitle %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">Seo关键词:</td>
                <td class="adminData">
                    <textarea cols="2" rows="3" name="SeoKeywords" class="textarea-small"><%=Model.SeoKeywords %></textarea>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">Seo描述:</td>
                <td class="adminData">
                    <textarea cols="2" rows="3" name="SeoDescription" class="textarea-small"><%=Model.SeoDescription %></textarea>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">模板页:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" onfocus="selectTemplate(this);" name="DetailTemplate" value="<%=Model.DetailTemplate %>" />
                </td>
            </tr>

        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=NtID %>" />
            <input type="hidden" name="LanguageId" value="<%=Model.LanguageId %>" />
            <input type="hidden" name="ListTemplate" value="<%=Model.ListTemplate %>" />
            <input type="hidden" name="CreatedDate" value="<%=Model.CreatedDate %>" />
            <input type="hidden" name="UpdatedDate" value="<%=DateTime.Now %>" />
            <input type="hidden" name="Guid" value="<%=Model.Guid %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <a class="a-button" href="<%BackScript(); %>">返回</a>
        </div>
    </form>
</asp:Content>
