<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false"
    CodeFile="navigation_edit.aspx.cs" Inherits="netin_common_navigation_edit" %>

<%@ Register Src="~/Netin/shared/uploader.ascx" TagPrefix="uc1" TagName="uploader" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
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
            editForm.submit();
        }

        /*选择target属性值*/
        function selectTarget() {
            var data = [
                { text: '_blank', value: '_blank' },
                { text: '_parent', value: '_parent' },
                { text: '_search', value: '_search' },
                { text: '_self', value: '_self' },
                { text: '_top', value: '_top' },
            ];
            var c = '<%=Model.AnchorTarget%>';
            nt.openSelectionWindow('选择Target',
                data,
                c,
                function (v, t) {
                    editForm.AnchorTarget.value = v;
                });
        }

        /*选择模块类型*/
        function selectRootSidOrPageIds(sender) {
            var M_ARTICLE = '<%=(int)Nt.Model.Enum.ModuleType.Article%>',
                M_PRODUCT = '<%=(int)Nt.Model.Enum.ModuleType.Product%>',
                M_PAGE = '<%=(int)Nt.Model.Enum.ModuleType.Page%>',
                M_DOWNLOAD = '<%=(int)Nt.Model.Enum.ModuleType.Download%>',
                M_GOODS = '<%=(int)Nt.Model.Enum.ModuleType.Goods%>',
                M_LINK = '<%=(int)Nt.Model.Enum.ModuleType.Link%>',
                M_FOLDER = '<%=(int)Nt.Model.Enum.ModuleType.Folder%>';

            <%OutRootSidOrPageIdsSelections();%>

            var c = editForm.NaviType.value;
            var rootSid = -1;
            var pageIds = editForm.PageIds.value;

            switch (c) {
                case M_ARTICLE:
                    nt.openSelectionWindow('文章分类选项', articleClasses, rootSid, function (v, t) {
                        $('#nt-msg').html('您选择了文章分类：' + t);
                        editForm.RootSid.value = v;
                    });
                    break;
                case M_PRODUCT:
                    nt.openSelectionWindow('产品分类选项', productCategories, rootSid, function (v, t) {
                        $('#nt-msg').html('您选择了产品分类：' + t);
                        editForm.RootSid.value = v;
                    });
                    break;
                case M_PAGE:
                    nt.openMutiSelectorWin('页面选项', singlePageIds, pageIds, function () {
                        var ids = '';
                        $('input:checked', this).each(function () {
                            if (ids != '') ids += ',';
                            ids += this.value;
                        });
                        editForm.PageIds.value = ids;
                    });
                    break;
                case M_DOWNLOAD:
                    error('not surport!');
                    break;
                case M_GOODS:
                    nt.openSelectionWindow('商品分类选项', goodsClasses, rootSid, function (v, t) {
                        $('#nt-msg').html('您选择了商品分类：' + t);
                        editForm.RootSid.value = v;
                    });
                    break;
                case M_LINK:
                    break;
                case M_FOLDER:
                    break;
                default:
                    break;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <form action="navigation_edit.aspx" method="post" id="editForm">
        <table class="adminContent">
            <tr>
                <td class="adminTitle">父类别:
                </td>
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
                <td class="adminTitle">类别名:
                </td>
                <td class="adminData">
                    <input type="text" class="input-short" maxlength="256" name="Name" value="<%=Model.Name %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">生效:
                </td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.Display, "Display"); %>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">路径:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="Path" value="<%=Model.Path %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">Target:
                </td>
                <td class="adminData">
                    <input type="text" class="input-normal" readonly="readonly" maxlength="9" name="AnchorTarget"
                        value="<%=Model.AnchorTarget %>" onfocus="selectTarget();" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">模块类型:
                </td>
                <td class="adminData">

                    <% 
                        HtmlRenderer.DropDownList(NtUtility.ListItemSelect(StaticDataProvider.Instance.NaviTypeProvider, Model.NaviType),
                            "NaviType", "selectRootSidOrPageIds(this);", 100);
                    %>
                    <span class="nt-msg" id="nt-msg"></span>
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
                <td class="adminTitle">推广标题:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="SeoTitle" value="<%=Model.SeoTitle %>" />
                    <span class="tips">请勿超过512个字符</span>
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
                    <textarea cols="20" rows="5" class="textarea-small" name="SeoDescription"><%=Model.SeoDescription %></textarea>
                    <span class="tips">请勿超过1024个字符</span>
                </td>
            </tr>
        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=NtID %>" />
            <input type="hidden" name="Depth" value="<%=Model.Depth %>" />
            <input type="hidden" name="RootSid" value="<%=Model.RootSid %>" />
            <input type="hidden" name="PageIds" value="<%=Model.PageIds %>" />
            <input type="hidden" name="Crumbs" value="<%=Model.Crumbs %>" />
            <input type="hidden" name="LanguageId" value="<%=Model.LanguageId %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a> <a class="a-button"
                href="javascript:;" onclick="editForm.reset();">重置</a> <a class="a-button" href="<%BackScript(); %>">返回</a>
        </div>
    </form>
</asp:Content>
