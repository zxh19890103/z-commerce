<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="execute.aspx.cs" Inherits="netin_db_execute" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .msg-box { color: #f00; }
        .sql-box { }
            .sql-box textarea { }
        .gridview { overflow: auto; width: 1000px; min-height: 300px; max-height: 600px; }
        .exe-button { width: 120px; height: 40px; }
    </style>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <form id="form1" runat="server">
        <div class="msg-box">
            <asp:Literal ID="Msg" runat="server"></asp:Literal>
        </div>
        <div class="sql-box">
            <asp:CheckBox ID="IsQuery" Checked="false" Text="是否表数据查询" runat="server" />
            <br />
            <br />
            <asp:TextBox ID="SqlBox" TextMode="MultiLine" CssClass="textarea-big" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="ExeButton" CssClass="exe-button" runat="server" OnClick="ExeButton_Click" Text="提交脚本" />
            <asp:Button ID="ReCreateProcsButton" CssClass="exe-button" runat="server" Text="重新创建存储过程" OnClick="ReCreateProcsButton_Click" />
            <asp:Button ID="reCreateMenuButton" CssClass="exe-button" runat="server" Text="重新创建后台目录"  OnClick="reCreateMenuButton_Click"/>
        </div>
        <div class="gridview">
            <%RenderTables(); %>
        </div>
    </form>
</asp:Content>
