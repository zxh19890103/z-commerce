<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="categories.aspx.cs" Inherits="netin_product_categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="note">
        只允许在最底层类别上添加产品，否则会影响到查询!
    </div>
    <div id="list" class="list">
        <%List(); %>
    </div>
</asp:Content>
