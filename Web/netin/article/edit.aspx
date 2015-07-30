<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false"
    CodeFile="edit.aspx.cs" Inherits="netin_article_edit" %>
<%@ OutputCache Duration="30"  VaryByParam="id"%>

<%@ Register Src="~/netin/shared/editor.ascx" TagPrefix="uc1" TagName="editor" %>
<%@ Register Src="~/netin/shared/uploader.ascx" TagPrefix="uc1" TagName="uploader" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        /*文章保存*/
        function save() {

            if (!editForm.ArticleClassId.value) {
                alert('请选择分类!');
                return false;
            }

            if (editForm.Title.value == '') {
                alert('请输入标题!', function () {
                    editForm.Title.focus();
                });
                return false;
            }

            editor.sync();

            if (editForm.Body.value == '') {
                alert('内容不能为空!');
                return false;
            }

            editForm.submit();
        }
    </script>
    <uc1:editor runat="server" ID="editor" TextareaName="Body" />
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <form action="<%=Request.RawUrl %>" method="post" id="editForm">
        <div class="submit-top">
            <a class="a-button" href="javascript:;" onclick="save();">保存</a> <a class="a-button"
                href="javascript:;" onclick="editForm.reset();">重置</a>
        </div>
        <table class="adminContent" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="adminTitle">商品分类:
                </td>
                <td class="adminData">
                    <%
                        if (Model.Id > 0)
                        {
                            Response.Write(Model.ClassName);
                            HtmlRenderer.Hidden(Model.ArticleClassId, "ArticleClassId");
                        }
                        else
                        {
                            if (Categories.Count < 1)
                                Goto("article_class_edit.aspx", "请先添加文章分类！");
                            NtUtility.ListItemSelect(Categories, Model.ArticleClassId);
                            HtmlRenderer.DropDownList(Categories, "ArticleClassId");
                        }
                    %>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">标题:
                </td>
                <td class="adminData">
                    <input type="text" class="input-short" maxlength="256" name="Title" value="<%=Model.Title %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">简述:
                </td>
                <td class="adminData">
                    <textarea cols="20" rows="5" name="Short" class="textarea-small"><%=Model.Short %></textarea><span class="tips">请勿超过1024个字符</span>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">作者:
                </td>
                <td class="adminData">
                    <input type="text" class="input-short" maxlength="256" name="Author" value="<%=Model.Author %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">来源:
                </td>
                <td class="adminData">
                    <input type="text" class="input-short" maxlength="256" name="Source" value="<%=Model.Source %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">内容:
                </td>
                <td class="adminData">
                    <textarea cols="20" rows="5" name="Body" style="width:800px;height:300px;" class="textarea-big"><%=Server.HtmlEncode(Model.Body) %></textarea>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">推广关键词:
                </td>
                <td class="adminData">
                    <textarea cols="20" rows="5" name="SeoKeywords" class="textarea-small"><%=Model.SeoKeywords %></textarea>
                    <span class="tips">请勿超过1024个字符</span>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">推广描述:
                </td>
                <td class="adminData">
                    <textarea cols="20" rows="5" name="SeoDescription" class="textarea-small"><%=Model.SeoDescription %></textarea>
                    <span class="tips">请勿超过1024个字符</span>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">属性:
                </td>
                <td class="adminData">
                    <span class="checkbox-with-label">
                        <%HtmlRenderer.CheckBox(Model.Display, "Display", "显示"); %>
                    </span><span class="checkbox-with-label">
                        <%HtmlRenderer.CheckBox(Model.SetTop, "SetTop", "置顶"); %>
                    </span><span class="checkbox-with-label">
                        <%HtmlRenderer.CheckBox(Model.Recommended, "Recommended", "推荐"); %>
                    </span>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">点击次数:
                </td>
                <td class="adminData">
                    <input type="text" class="input-number" maxlength="9" name="Rating" value="<%=Model.Rating %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">排序:
                </td>
                <td class="adminData">
                    <input type="text" class="input-number" maxlength="9" name="DisplayOrder" value="<%=Model.DisplayOrder %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">添加日期:
                </td>
                <td class="adminData">
                    <%HtmlRenderer.DateTimePicker(Model.CreatedDate, "CreatedDate"); %>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">修改日期:
                </td>
                <td class="adminData">
                    <%HtmlRenderer.DateTimePicker(Model.UpdatedDate, "UpdatedDate"); %>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">是否可上传:
                </td>
                <td class="adminData">
                    <input type="checkbox" name="Downloadable" <%if (Model.Downloadable) Html("checked=\"checked\""); %> onchange="if(this.checked){$('#fileUploadArea').show();this.value='true';}else{$('#fileUploadArea').hide();this.value='false';}" value="<%=Model.Downloadable?"true":"false" %>" />
                </td>
            </tr>
            <tr id="fileUploadArea" <%if (!Model.Downloadable) Html("style=\"display: none;\""); %>>
                <td class="adminTitle">资料上传:
                </td>
                <td class="adminData">
                    <uc1:uploader runat="server" FieldName="FileUrl" IsFile="true" ID="uploaderfile" />
                </td>
            </tr>
        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=NtID %>" />
            <input type="hidden" name="LanguageId" value="<%=Model.LanguageId %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a> <a class="a-button"
                href="javascript:;" onclick="editForm.reset();">重置</a> <a class="a-button" href="<%BackScript(); %>">返回</a>
        </div>
    </form>
</asp:Content>
