<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="permission.aspx.cs" Inherits="netin_user_permission" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        /*删除目录文件缓存*/
        function delMenuCache() {
            nt.ajax({
                action: 'DelMenuCache',
                success: function (json) {
                    if (json.error) error(json.message);
                    else {
                        success(json.message);
                    }
                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <div class="list-tool-head">
        <a href="javascript:;" class="a-button" onclick="delMenuCache();">删除目录文件缓存</a>
    </div>
    <div class="list">
        <%List(); %>
    </div>
</asp:Content>
