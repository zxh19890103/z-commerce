<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="htmlblock.aspx.cs" Inherits="netin_common_htmlblock" %>

<%@ Register Src="~/netin/shared/editor.ascx" TagPrefix="uc1" TagName="editor" %>
<asp:Content ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        var htmlMgr = new nt.tableMgr({
            title: '自定义Html区',
            width: 1000,
            ajax2page: true,
            onPost: function () {
                editor.sync();
            },
            formFilled: function (j) {
                editor.html(j.Html);
            },
            onAdd: function () {
                editor.html('');
            }
        });
        htmlMgr.init();
    </script>

    <uc1:editor runat="server" ID="editor" TextareaName="Html" />
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">

    <div id="list" class="list">
        <%List(); %>
    </div>

    <div class="html-content-wrap">
        <div class="html-content" id="tmEditor">
            <div class="html-content-top nt-drag-bar">
                <span class="html-content-title"></span><a href="javascript:;" role="close">x</a>
            </div>
            <div class="html-content-body">
                <form action="htmlblock.aspx" method="post" id="tmForm">
                    <table class="adminContent">
                        <tr>
                            <td class="adminTitle">Html区名称:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="100" class="input-long" name="Name" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">内容:
                            </td>
                            <td class="adminData">
                                <textarea cols="20" rows="5" name="Html" style="width: 800px; height: 300px;" class="textarea-big"></textarea>
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" name="Id" value="0" />
                </form>
            </div>
            <div class="html-content-footer">
                <a href="javascript:;" class="a-button" role="post">保存</a>
                <a href="javascript:;" class="a-button" role="close">关闭</a>
            </div>
        </div>
    </div>

</asp:Content>
