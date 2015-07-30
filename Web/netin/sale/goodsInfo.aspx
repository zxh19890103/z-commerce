<%@ Page Language="C#" AutoEventWireup="false" CodeFile="goodsInfo.aspx.cs" Inherits="netin_sale_goodsInfo" %>

<%@ Register Src="~/netin/shared/head.ascx" TagPrefix="uc1" TagName="head" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:head runat="server" ID="head" />
    <title><%=Title %></title>
</head>
<body>
    <div class="list">
        <%List(); %>
    </div>
    <div>
        <a class="a-button" href="javascript:;" onclick="window.close();">关闭</a>
    </div>
</body>
</html>
