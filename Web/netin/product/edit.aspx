<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false"
    CodeFile="edit.aspx.cs" Inherits="netin_product_edit" %>
<%@ OutputCache Duration="30"  VaryByParam="id"%>

<%@ Register Src="~/netin/shared/editor.ascx" TagPrefix="uc1" TagName="editor" %>
<%@ Register Src="~/netin/shared/uploader.ascx" TagPrefix="uc1" TagName="uploader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        /*产品保存*/
        function save() {

            if (!editForm.ProductCategoryId.value) {
                alert('请选择分类!');
                return false;
            }

            if (editForm.Name.value == '') {
                alert('请输入标题!', function () {
                    editForm.Title.focus();
                });
                return false;
            }

            if (editForm.PictureUrl.value == '') {
                alert('请上传图片!');
                return false;
            }

            editor.sync();

            if (editForm.FullDescription.value == '') {
                alert('内容不能为空!');
                return false;
            }

            if (editForm.Thumb.value == '')
                editForm.Thumb.value = editForm.PictureUrl.value;//默认以大图替代缩略图

            editForm.submit();
        }

        $(document).ready(function () {
            $('#ntTabsWrap').tabs();
        });

    </script>
    <uc1:editor runat="server" ID="editor" TextareaName="FullDescription" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form action="<%=Request.RawUrl %>" method="post" id="editForm">
        <div class="submit-top">
            <a class="a-button" href="javascript:;" onclick="save();">保存</a> <a class="a-button"
                href="javascript:;" onclick="editForm.reset();">重置</a>
        </div>
        <div class="nt-tabs-wrap" id="ntTabsWrap">
            <div class="nt-tabs-wrap-inner">
                <ul class="nt-tabs" id="nt_tabs">
                    <li><a href="javascript:;">基本信息</a></li>
                    <li><a href="javascript:;">产品图片</a></li>
                    <li><a href="javascript:;">其它参数</a></li>
                </ul>
                <div class="nt-tabs-content-wrap" id="nt_tab_content_wrap">
                    <div class="tab-item">
                        <table class="adminContent" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td class="adminTitle">商品分类:
                                </td>
                                <td class="adminData">
                                    <%
                                        if (Model.Id > 0)
                                        {
                                            Response.Write(Model.CategoryName);
                                            HtmlRenderer.Hidden(Model.ProductCategoryId, "ProductCategoryId");
                                        }
                                        else
                                        {
                                            if (Categories.Count < 1)
                                                Goto("category_edit.aspx", "请先添加产品分类！");
                                            NtUtility.ListItemSelect(Categories, Model.ProductCategoryId);
                                            HtmlRenderer.DropDownList(Categories, "ProductCategoryId");
                                        }
                                    %>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">产品名:
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-short" maxlength="256" name="Name" value="<%=Model.Name %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">简述:
                                </td>
                                <td class="adminData">
                                    <textarea cols="20" rows="5" name="ShortDescription" class="textarea-small"><%=Model.ShortDescription %></textarea><span class="tips">请勿超过1024个字符</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">内容:
                                </td>
                                <td class="adminData">
                                    <textarea cols="20" rows="5" name="FullDescription" style="width: 800px; height: 300px;" class="textarea-big"><%=Server.HtmlEncode(Model.FullDescription) %></textarea>
                                </td>
                            </tr>
                            <!--<tr>
                                <td class="adminTitle">(MZ)Day:
                                </td>
                                <td class="adminData">
                                    <script type="text/javascript">
                                        function selectDay(){
                                            nt.openSelectionWindow(
                                               '日次选择',
                                               <%=NtUtility.GetJsObjectArrayFromList(DB.GetDropdownlist < Day > ("Title", "Id")) %>,
                                        editForm.Day.value,
                                        function(v,t){
                                            editForm.DayText.value=t;
                                            editForm.Day.value=v;
                                        });
                                        }
                                    </script>
                                    <input type="text" readonly="readonly" onfocus="selectDay();" name="DayText" value="<%=Model.DayTitle %>" />
                                    <input type="hidden" name="Day" value="<%=Model.Day %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">(MZ)Area:
                                </td>
                                <td class="adminData">
                                    <script type="text/javascript">
                                        function selectArea(){
                                            nt.openSelectionWindow(
                                               '地区选择',
                                               <%=NtUtility.GetJsObjectArrayFromList(DB.GetDropdownlist < Area > ("Name", "Id")) %>,
                                        editForm.AreaId.value,
                                        function(v,t){
                                            editForm.AreaIdText.value=t;
                                            editForm.AreaId.value=v;
                                        });
                                        }

                                    </script>
                                    <input type="text" class="input-short" onfocus="selectArea();" readonly="readonly"
                                        name="AreaIdText" value="<%=Model.AreaName %>" />
                                    <input type="hidden" name="AreaId" value="<%=Model.AreaId %>" />
                                </td>
                            </tr>-->
                            <tr>
                                <td class="adminTitle">推广关键词:
                                </td>
                                <td class="adminData">
                                    <textarea cols="20" rows="5" name="SeoKeywords" class="textarea-small"><%=Model.SeoKeywords %></textarea><span class="tips">请勿超过1024个字符</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">推广描述:
                                </td>
                                <td class="adminData">
                                    <textarea cols="20" rows="5" name="SeoDescription" class="textarea-small"><%=Model.SeoDescription %></textarea><span class="tips">请勿超过1024个字符</span>
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
                                    </span><span class="checkbox-with-label">
                                        <%HtmlRenderer.CheckBox(Model.IsNew, "IsNew", "新品"); %>
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
                                <td class="adminTitle">缩略图:
                                </td>
                                <td class="adminData">
                                    <uc1:uploader runat="server" ID="uploader1" FieldName="Thumb" PostUrl="/netin/handlers/uploadHandler.aspx" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">大图片:
                                </td>
                                <td class="adminData">
                                    <uc1:uploader runat="server" ID="uploader" FieldName="PictureUrl" PostUrl="/netin/handlers/uploadHandler.aspx" />
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
                    </div>
                    <div class="tab-item">
                        <%
                            RenderPictures();
                        %>
                    </div>
                    <div class="tab-item">
                        <%
                            RenderMutiParams();
                        %>
                    </div>
                </div>
            </div>
        </div>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=NtID %>" />
            <input type="hidden" name="LanguageId" value="<%=Model.LanguageId %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a> <a class="a-button"
                href="javascript:;" onclick="editForm.reset();">重置</a> <a class="a-button" href="<%BackScript(); %>">返回</a>
        </div>
    </form>
</asp:Content>
