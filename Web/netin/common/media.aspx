<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="media.aspx.cs" Inherits="netin_common_media" %>

<%@ Register Src="~/netin/shared/uploader.ascx" TagPrefix="uc1" TagName="uploader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        var mediaMgr = new nt.tableMgr({
            title: '图片',
            ajax2page: true,
            got: function (j) {
                var src = j.Src;
                tmForm.oldFile.value = src;
                $('#FileWrap').html('<input type="file" name="file" id="File"/>');
            },
            posted: function (j) { tmForm.oldFile.value = ''; }
        });
        mediaMgr.init();
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <div id="list" class="list">
        <%List(); %>
    </div>

    <div class="html-content-wrap">
        <div class="html-content" id="tmEditor" style="width: 500px;">
            <div class="html-content-top nt-drag-bar">
                <span class="html-content-title"></span><a href="javascript:;" role="close">x</a>
            </div>
            <div class="html-content-body">
                <form action="media.aspx" method="post" id="tmForm">
                    <table class="adminContent">
                        <tr>
                            <td class="adminTitle">Title:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="512" class="input-long" name="Title" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">Alt:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="512" class="input-long" name="Alt" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">Description:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="512" class="input-long" name="Description" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">Display:
                            </td>
                            <td class="adminData">
                                <%HtmlRenderer.CheckBox(true, "Display"); %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">DisplayOrder:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="9" class="input-number" name="DisplayOrder" value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">Src:
                            </td>
                            <td class="adminData">
                                <input type="text" name="Src" value="" />
                                <input type="hidden" name="oldFile" value="" />
                                <span id="FileWrap">
                                    <input type="file" name="file" id="File" value="" />
                                </span>
                            </td>
                        </tr>
                    </table>
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

