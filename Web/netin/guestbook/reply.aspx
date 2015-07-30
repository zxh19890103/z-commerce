<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false"
    CodeFile="reply.aspx.cs" Inherits="netin_guestbook_reply" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <div class="list" id="list">
        <%List(); %>
    </div>
    <div>
        <a class="a-button" href="list.aspx?page=<%=Request.QueryString["page"] %>">返回到留言列表</a>
    </div>
</asp:Content>
