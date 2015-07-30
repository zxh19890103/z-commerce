<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="emailaccount.aspx.cs" Inherits="netin_common_emailaccount" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //设置默认使用的账号
        function setThisDefault(id) {
            nt.ajax({
                action: 'SetDefaultEmailAccount',
                data: { id: id },
                success: function (json) {
                    if (json.error) { error(json.message); }
                    else {
                        nt.reload();
                    }
                }
            });
        }

    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <%List(); %>
</asp:Content>
