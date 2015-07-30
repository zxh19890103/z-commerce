<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/qb_top.ascx" TagPrefix="uc1" TagName="qb_top" %>
<%@ Register Src="~/shared/qb_foot.ascx" TagPrefix="uc1" TagName="qb_foot" %>


<%
    ChannelNo = 1;        
%>

<uc1:qb_top runat="server" ID="qb_top" />

 <div class="bigpic"> 
       	<img src="/images/map.png">
        <p>地址：大连市中山区XX街XX号</p>
       </div>
       <div style="height:63px;"></div>
       <p class="p1">如果您有疑问，请与我们联系，我们会在一个工作日内与您取得联系！</p>
       <p class="p2">+86-0411-84790993</p>
       <p class="p3">请您填写下面的表格，以便我们能更好更快的为您服务</p>
       <div style="height:40px;"></div>
       <div class="d1"><input class="p4"><label style="position:absolute; left:10px; top:5px;">联系人</label></div>
       <div style="height:23px;"></div>
       <div class="d1"><input class="p5"><label style="position:absolute; left:10px; top:5px;">Email</label></div>
       <div style="height:23px;"></div>
       <div class="d1"><input class="p6"><label style="position:absolute; left:10px; top:5px;">地址</label></div>
       <div style="height:23px;"></div>
       <div class="d1"><input class="p7"><label style="position:absolute; left:10px; top:5px;">电话</label></div>
       <div style="height:25px;"></div>
       <div class="d1"><textarea class="p8"></textarea><label style="position:absolute; left:10px; top:5px;">内容</label></div>
       <div style="height:10px;"></div>
       <div class="sendbutton">
       		<a href="#" class="a1">发送</a>
            <a href="#" class="a2">重置</a>
       </div>


<uc1:qb_foot runat="server" ID="qb_foot" />
