<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/netin/layout.master" CodeFile="brand.aspx.cs" Inherits="netin_goods_brand" %>

<asp:Content runat="server" ContentPlaceHolderID="head">

    <script type="text/javascript">

        var brandMgr = new nt.tableMgr({
            title: '品牌信息',
            got: function (j) {
                var src = j.PictureUrl;
                tmForm.oldFile.value = src;
                $('#FileWrap').html('<input type="file" name="file" id="File"/>');
            },
            posted: function (j) { tmForm.oldFile.value = ''; }
        });

        brandMgr.init();

    </script>

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="body">

    <div id="list" class="list">
        <%List(); %>
    </div>
    <div class="html-content-wrap">
        <div class="html-content" id="tmEditor">
            <div class="html-content-top nt-drag-bar">
                <span class="html-content-title"></span><a href="javascript:;" role="close">x</a>
            </div>
            <div class="html-content-body">
                <form action="brand.aspx" method="post" id="tmForm">
                    <table class="adminContent">
                        <tr>
                            <td class="adminTitle">品牌名:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="50" class="input-long" name="Name" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">链接:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="120" class="input-long" name="Url" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">品牌描述:
                            </td>
                            <td class="adminData">
                                <textarea name="Description" cols="2" rows="1"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">是否使用:
                            </td>
                            <td class="adminData">
                                <%HtmlRenderer.CheckBox(true, "Display"); %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">排序:
                            </td>
                            <td class="adminData">
                                <input type="text" class="input-number" name="DisplayOrder" maxlength="9" value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">图片:
                            </td>
                            <td class="adminData">
                                <input type="text" name="PictureUrl" value="" />
                                <input type="hidden" name="oldFile" value="" />
                                <span id="FileWrap">
                                    <input type="file" name="file" id="File" value="" />
                                </span>
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" name="LanguageId" value="<%=NtContext.Current.CurrentLanguage.Id %>" />
                    <input type="hidden" name="Id" value="0" />
                </form>
            </div>
            <div class="html-content-footer">
                <a href="javascript:;" class="a-button" role="post">保存</a> <a href="javascript:;"
                    class="a-button" role="close">关闭</a>
            </div>
        </div>
    </div>

</asp:Content>
