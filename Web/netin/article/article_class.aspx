<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="article_class.aspx.cs" Inherits="netin_article_article_class" %>

<asp:Content runat="server" ContentPlaceHolderID="head">

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <div class="note">
        只允许在最底层类别上添加文章，否则会影响到查询!
    </div>
    <div id="list" class="list">
        <%List(); %>
    </div>    
</asp:Content>
