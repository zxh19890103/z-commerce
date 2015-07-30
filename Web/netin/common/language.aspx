<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="language.aspx.cs" Inherits="netin_common_language" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <div id="list" class="list">
        <%List(); %>
    </div>
</asp:Content>
