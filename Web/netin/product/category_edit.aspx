<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="category_edit.aspx.cs" Inherits="netin_product_category_edit" %>

<%@ Register Src="~/Netin/shared/uploader.ascx" TagPrefix="uc1" TagName="uploader" %>
<%@ Register Src="~/netin/shared/editor.ascx" TagPrefix="uc1" TagName="editor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        //保存
        function save() {
            if (editForm.Parent.value == '') {
                alert('请选择父级分类!');
                return;
            }
            if (editForm.Name.value == '') {
                alert('类别名不能为空!');
                return;
            }

            editor.sync();

            editForm.submit();
        }


    </script>
    <uc1:editor runat="server" ID="editor" SimpleEditor="true" TextareaName="Description" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <form action="category_edit.aspx" method="post" id="editForm">
        <div class="submit-top">
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
        </div>
        <table class="adminContent">
            <tr>
                <td class="adminTitle">父类别:</td>
                <td class="adminData">
                    <%
                        if (Model.Id > 0)
                        {
                            Response.Write(Model.ParentName);
                            HtmlRenderer.Hidden(Model.Parent, "Parent");
                        }
                        else
                        {
                            Categories.Insert(0, new NtListItem("根级", 0));
                            NtUtility.ListItemSelect(Categories, Model.Parent);
                            HtmlRenderer.DropDownList(Categories, "Parent");
                        }
                    %>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">类别名:</td>
                <td class="adminData">
                    <input type="text" class="input-short" maxlength="256" name="Name" value="<%=Model.Name %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">生效:</td>
                <td class="adminData"><%HtmlRenderer.CheckBox(Model.Display, "Display"); %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">图片:</td>
                <td class="adminData"><%
                                          uploader.FieldValue = Model.PictureUrl;
                %>
                    <uc1:uploader runat="server" ID="uploader" PostUrl="/Netin/handlers/uploadHandler.aspx" FieldName="pictureurl" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">排序:</td>
                <td class="adminData">
                    <input type="text" class="input-number" maxlength="9" name="DisplayOrder" value="<%=Model.DisplayOrder %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">描述:
                </td>
                <td class="adminData">
                    <textarea cols="20" rows="5" class="textarea-keditor" style="width:600px;height:250px;"  name="Description"><%=Server.HtmlEncode(Model.Description) %></textarea>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">推广标题:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="SeoTitle" value="<%=Model.SeoTitle %>" />

                </td>
            </tr>

            <tr>
                <td class="adminTitle">推广关键词:
                </td>
                <td class="adminData">
                    <textarea cols="20" rows="5" class="textarea-small" name="SeoKeywords"><%=Model.SeoKeywords %></textarea>
                    <span class="tips">请勿超过1024个字符</span>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">推广描述:
                </td>
                <td class="adminData">
                    <textarea cols="20" rows="5" class="textarea-small" name="SeoDescription"><%=Model.SeoDescription %></textarea><span class="tips">请勿超过1024个字符</span>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">列表页模板:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" onfocus="selectTemplate(this);" name="ListTemplate" value="<%=Model.ListTemplate %>" />
                    <a href="javascript:;" onclick="editForm.ListTemplate.value='<%=Model.DefaultListTemplate %>';"><%=Model.DefaultListTemplate %></a>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">详细模板:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" onfocus="selectTemplate(this);" name="DetailTemplate" value="<%=Model.DetailTemplate %>" />
                    <a href="javascript:;" onclick="editForm.DetailTemplate.value='<%=Model.DefaultDetailTemplate %>';"><%=Model.DefaultDetailTemplate %></a>
                </td>
            </tr>
        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=NtID %>" />
            <input type="hidden" name="Depth" value="<%=Model.Depth %>" />
            <input type="hidden" name="Crumbs" value="<%=Model.Crumbs %>" />
            <input type="hidden" name="LanguageId" value="<%=Model.LanguageId %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <a class="a-button" href="<%BackScript(); %>">返回</a>
        </div>
    </form>

</asp:Content>
