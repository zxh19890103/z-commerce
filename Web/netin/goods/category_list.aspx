<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="category_list.aspx.cs" Inherits="Netin_Goods_category_list" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <div class="note">
        只允许在最底层类别上添加商品，否则会影响到查询!
    </div>
    <div id="list" class="list">
        <%List(); %>
    </div>
</asp:Content>
