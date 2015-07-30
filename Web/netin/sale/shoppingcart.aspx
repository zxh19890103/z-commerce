<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="shoppingcart.aspx.cs" Inherits="netin_sale_shoppingcart" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <div class="list" id="list">
        <%List(); %>
    </div>
</asp:Content>
