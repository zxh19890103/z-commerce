<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="attribute.aspx.cs" Inherits="Netin_goods_attribute" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <div id="list" class="list">
        <%List(); %>
    </div>
</asp:Content>
