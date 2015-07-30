<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>


<uc1:top runat="server" ID="top1" />

<div class="content">
<div class="container">
    <div class="about_box">
        <div class="about_content" style="height: 800px;">
            <h2>品牌<span>brand</span></h2>
            <div class="line_01"><span></span></div>

            <div class="brand_box">
                <a href="#">
                    <img src="/content/img/logo01.jpg"></a>
                <a href="#">
                    <img src="/content/img/logo02.jpg"></a>
                <a href="#">
                    <img src="/content/img/logo03.jpg"></a>
                <a href="#">
                    <img src="/content/img/logo04.jpg"></a>
                <a href="#">
                    <img src="/content/img/logo05.jpg"></a>
                <a href="#">
                    <img src="/content/img/logo06.jpg"></a>
                <a href="#">
                    <img src="/content/img/logo07.jpg"></a>
            </div>
        </div>
    </div>
</div>

    </div>
<uc1:foot runat="server" ID="foot" />

