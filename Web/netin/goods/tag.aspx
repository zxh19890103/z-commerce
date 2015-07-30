<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="tag.aspx.cs" Inherits="netin_goods_tag" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">

        var tagMgr = new nt.tableMgr({
            title: '标签',
            ajax2page: true
        });
        tagMgr.init();

    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <div class="list" id="list">
        <%List(); %>
    </div>

    <div class="html-content" id="tmEditor" style="width: 500px;">
        <div class="html-content-top nt-drag-bar">
            <span class="html-content-title">添加标签</span>
            <a href="javascript:;" role="close">x</a>
        </div>
        <form action="tag.aspx" method="post" id="tmForm">
            <div class="html-content-body">
                <table class="adminContent">
                    <tr>
                        <td class="adminTitle">标签：
                        </td>
                        <td class="adminData">
                            <input type="text" class="input-normal" name="Tag" value="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="adminTitle">生效：
                        </td>
                        <td class="adminData">
                            <%HtmlRenderer.CheckBox(true, "Display"); %>
                        </td>
                    </tr>
                    <tr>
                        <td class="adminTitle">排序：
                        </td>
                        <td class="adminData">
                            <input type="text" class="input-number" name="DisplayOrder" value="0" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="html-content-footer">
                <input type="hidden" name="Id" />
                <input type="hidden" name="CreatedDate" />
                <input type="hidden" name="LanguageId" value="<%=NtContext.Current.LanguageID %>" />
                <a href="javascript:;" class="a-button" role="post">保存</a>
                <a href="javascript:;" class="a-button" role="close">关闭</a>
            </div>
        </form>
    </div>

</asp:Content>
