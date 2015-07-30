<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="removeditems.aspx.cs" Inherits="netin_goods_removeditems" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <script src="../script/goods.js" type="text/javascript"></script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <div class="list" id="list">
        <%
            List();
        %>
    </div>
</asp:Content>
