<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/qb_top.ascx" TagPrefix="uc1" TagName="qb_top" %>
<%@ Register Src="~/shared/qb_foot.ascx" TagPrefix="uc1" TagName="qb_foot" %>


<%
    ChannelNo = 1;        
%>

<uc1:qb_top runat="server" ID="qb_top" />


<p class="about">关于我们</p>
    <div class="bigpic"><img src="/images/pic.png"></div>
    <div class="textcontent">
    	<p>关于秋白演示文案关于秋白演示文案关于秋白演示文案关于秋白演示文案关于秋白演示文案关于秋白演示文案关于秋白演示文案关于秋白演示文案关于秋白演示文案关于秋白演示文案关于秋白演示文案关于秋白演示文案关</p>
    </div>
    <div style="width:1000px; margin:30px auto;" >
    	<img src="/images/yewufanwei.png" style="width:1000px;">
    </div>

<uc1:qb_foot runat="server" ID="qb_foot" />
