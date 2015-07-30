<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>
<%
    InCustomerArea = true;
%>


<!----container---->
<uc1:top runat="server" ID="top1" />

<%Include("/html.inc/customer_top.html"); %>
<h3>我的积分</h3>
我的当前积分值：
    <%=Customer.Points %>

<%Include("/html.inc/customer_bottom.html"); %>

<uc1:foot runat="server" ID="foot" />

