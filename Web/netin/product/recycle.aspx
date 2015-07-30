<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="recycle.aspx.cs" Inherits="netin_product_recycle" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <div id="list" class="list">
        <%List(); %>
    </div>
</asp:Content>
