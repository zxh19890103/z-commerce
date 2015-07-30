<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/qb_top.ascx" TagPrefix="uc1" TagName="qb_top" %>
<%@ Register Src="~/shared/qb_foot.ascx" TagPrefix="uc1" TagName="qb_foot" %>

<%
    ChannelNo = 0;
%>

<uc1:qb_top runat="server" ID="qb_top" />

<ul class="columnbox">
    <li class="l1">
        <a target="_blank" href="two.aspx?cno=0">
            <img src="images/columnpic0.jpg">
            <h1>
                <img style="margin-top: 20px; margin-bottom: 15px;" src="images/wenzititle00.jpg"></h1>
        </a>
        <ul class="nav">
            <li><a target="_blank" href="two.aspx?cno=0#a1">相册</a></li>
            <li><a target="_blank" href="two.aspx?cno=0#a2">摆台</a></li>
            <li><a target="_blank" href="two.aspx?cno=0#a3">光盘</a></li>
        </ul>
    </li>
    <li>
        <a target="_blank" href="one.aspx?cno=1">
            <img src="images/columnpic1.jpg">
            <h1>
                <img style="margin-top: 20px; margin-bottom: 15px;" src="images/wenzititle01.jpg"></h1>
        </a>
        <ul class="nav">
            <li><a target="_blank" href="list.aspx?cno=1&sid=7">成长影集</a></li>
            <li><a target="_blank" href="list.aspx?cno=1&sid=3">个人照片</a></li>
        </ul>
    </li>
    <li>
        <a target="_blank" href="two.aspx?cno=2">
            <img src="images/columnpic2.jpg">
            <h1>
                <img style="margin-top: 20px; margin-bottom: 15px;" src="images/wenzititle02.jpg"></h1>
        </a>
        <ul class="nav">
            <li><a target="_blank" href="two.aspx?cno=2">校庆画册</a></li>
        </ul>
    </li>
    <li>
        <a target="_blank" href="one.aspx?cno=3">
            <img src="images/columnpic3.png">
            <h1>
                <img style="margin-top: 20px; margin-bottom: 15px;" src="images/wenzititle03.jpg"></h1>
        </a>
        <ul class="nav">
            <li><a target="_blank" href="list.aspx?cno=3&sid=5">传统文化</a></li>
            <li><a target="_blank" href="list.aspx?cno=3&sid=6">校园文化</a></li>
        </ul>
    </li>
    <li>
        <a target="_blank" href="one.aspx?cno=4">
            <img src="images/columnpic4.png">
            <h1>
                <img style="margin-top: 20px; margin-bottom: 15px;" src="images/wenzititle04.jpg"></h1>
        </a>
        <ul class="nav">
            <li><a target="_blank" href="list.aspx?cno=4&sid=15">作品欣赏</a></li>
            <li><a target="_blank" href="list.aspx?cno=4&sid=16">摄影欣赏</a></li>
            <li><a target="_blank" href="list.aspx?cno=4&sid=17">创意欣赏</a></li>
        </ul>
    </li>
</ul>


<uc1:qb_foot runat="server" ID="qb_foot" />
