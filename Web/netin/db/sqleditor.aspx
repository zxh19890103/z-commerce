<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="sqleditor.aspx.cs" Inherits="netin_db_sqleditor" %>

<%@ Register Src="~/netin/shared/head.ascx" TagPrefix="uc1" TagName="head" %>


<asp:Content runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .msg-box { color: #f00; }
        .sql-box { }
            .sql-box textarea { }
        .gridview { overflow: auto; width: 1000px; min-height: 300px; max-height: 600px; }
        .exe-button { width: 120px; height: 40px; }
        .textarea-big { height: 800px; }
    </style>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <form id="SqlEditorForm" runat="server">
        <div class="msg-box">
            <asp:Literal ID="Msg" runat="server"></asp:Literal>
        </div>
        <div>
            <asp:ListBox ID="SqlFiles" runat="server" Height="200" Width="250" SelectionMode="Single" OnSelectedIndexChanged="SqlFiles_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="存储过程脚本文件" Value="proc"></asp:ListItem>
                <asp:ListItem Text="类别迁移脚本文件" Value="tree.migration"></asp:ListItem>
                <asp:ListItem Text="后台目录脚本文件" Value="menu"></asp:ListItem>
                <asp:ListItem Text="基本数据脚本文件" Value="insert"></asp:ListItem>
            </asp:ListBox>
            <br />
            <asp:TextBox ID="SqlText" runat="server" TextMode="MultiLine" CssClass="textarea-big"></asp:TextBox>
            <br />
            <asp:Button ID="SaveSqlButton" runat="server" CssClass="exe-button" Text="保存" OnClick="SaveSqlButton_Click" />
        </div>
    </form>
</asp:Content>


