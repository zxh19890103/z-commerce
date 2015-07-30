<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false"
    CodeFile="websitelink.aspx.cs" Inherits="netin_common_websitelink" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var websiteLinkMgr = new nt.tableMgr({ title: '连接词' });
        websiteLinkMgr.init();

        /*应用连接词*/
        function apply(id) {
            var data = {};
            if (id) data.id = id;
            nt.ajax({
                action: 'apply',
                data: data,
                success: function (json) {
                    if (json.error) { error(json.message); }
                    else {
                        success("链接词添加成功!请查看!");
                    }
                }
            });
        }

        /*取消连接词应用*/
        function unApply(id) {
            var data = {};
            if (id) data.id = id;
            nt.ajax({
                action: 'unApply',
                data: data,
                success: function (json) {
                    if (json.error) { error(json.message); }
                    else {
                        success("链接词已取消!请查看!");
                    }
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">
    <div id="list" class="list">
        <%List(); %>
    </div>

    <div class="html-content-wrap">
        <div class="html-content" id="tmEditor">
            <div class="html-content-top nt-drag-bar">
                <span class="html-content-title"></span> <a href="javascript:;" role="close">x</a>
            </div>
            <div class="html-content-body">
                <form action="websitelink.aspx" method="post" id="tmForm">
                    <table class="adminContent">
                        <tr>
                            <td class="adminTitle">连接词:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="50" class="input-long" name="Word" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">链接:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="120" class="input-long" name="Url" />
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" name="Applied" value="False" />
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
