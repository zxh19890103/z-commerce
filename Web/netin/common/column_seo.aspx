<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false"
    CodeFile="column_seo.aspx.cs" Inherits="netin_common_column_seo" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function closeEditor() {
            $('#columnSeoEditor').hide();
            nt.removeMask();
        }

        function openEditor(id) {
            nt.showMask();
            nt.fetchModel(id, function (json) {
                $('#columnSeoForm').fillForm(json);
                $('#columnSeoEditor').center().movable();
            }, 'GetColumn');
        }

        function saveSeo() {
            $('#columnSeoForm').ajaxSubmit2({
                url: 'column_seo.aspx',
                action: 'SaveSeo',
                success: function (json) {
                    if (json.error)
                        error(json.message);
                    else
                        refreshList('list', 'List', false);
                    closeEditor();
                }
            });
        }
    
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="body">
    <div class="tips">
        栏目优化有助于您网站的推广
    </div>
    <div class="list" id="list">
        <%List(); %>
    </div>
    <div class="html-content-wrap">
        <div class="order-msg html-content" id="columnSeoEditor">
            <div class="html-content-top nt-drag-bar">
                <span class="html-content-title">修改栏目优化</span> <a onclick="closeEditor()" href="javascript:;">
                    x</a>
            </div>
            <div class="html-content-body">
                <form action="emailaccount.aspx" method="post" id="columnSeoForm">
                <table class="adminContent">
                    <tr>
                        <td class="adminTitle">
                            标题:
                        </th>
                        <td class="adminData">
                            <input type="text" class="input-long" name="SeoTitle" />
                        </td>
                    </tr>
                    <tr>
                        <td class="adminTitle">
                            关键词:
                        </th>
                        <td class="adminData">
                            <textarea name="SeoKeywords"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="adminTitle">
                            描述:
                        </th>
                        <td class="adminData">
                            <textarea name="SeoDescription"></textarea>
                        </td>
                    </tr>
                </table>
                <input type="hidden" name="Id" value="0" />
                </form>
            </div>
            <div class="html-content-footer">
                <a href="javascript:;" onclick="saveSeo();" class="a-button">保存</a> <a href="javascript:;"
                    onclick="closeEditor()" class="a-button">关闭</a>
            </div>
        </div>
    </div>
</asp:Content>
