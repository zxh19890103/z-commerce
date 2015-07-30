<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>
<%@ Register Src="~/shared/filter.ascx" TagPrefix="uc1" TagName="filter" %>

<uc1:top runat="server" ID="top" />

<div class="content">
    <div class="container">
        <!--- products ---->
        <uc1:filter runat="server" ID="filter" />
        <div class="clearfix"></div>
        <!-- //products ---->
        <!----speical-products---->
        <div class="special-products all-poroducts">
            <div class="s-products-head">
                <div class="s-products-head-left">
                    <h3>特价 <span>产品</span></h3>
                </div>
                <div class="s-products-head-right">
                    <a href="/list.aspx"><span></span>查看全部</a>
                </div>
                <div class="clearfix"></div>
            </div>
            <!----special-products-grids---->
            <div class="special-products-grids">
                <%
                    foreach (var item in DbAccessor.Top<View_Goods>(6, "settop desc,recommended desc,displayorder desc,createddate desc"))
                    {
                        Response.Write("<div class=\"col-md-3 special-products-grid text-center\">");
                        //brand yes!
                        Response.Write("	<a class=\"brand-name\" href=\"list.aspx?brand=");
                        Response.Write(item.BrandId);
                        Response.Write("\">");
                        Response.Write("		<img src=\"");
                        Response.Write(item.BrandLogo);
                        Response.Write("\" title=\"");
                        Response.Write(item.BrandName);
                        Response.Write("\" /></a>");
                        Response.Write("	<a class=\"product-here\" href=\"detail.aspx?id=");
                        Response.Write(item.Id);
                        Response.Write("\">");
                        Response.Write("		<img src=\"");
                        Response.Write(item.PictureUrl);
                        Response.Write("\" title=\"");
                        Response.Write(item.Name);
                        Response.Write("\" /></a>");
                        Response.Write("	<h4><a href=\"detail.aspx?id=");
                        Response.Write(item.Id);
                        Response.Write("\">");
                        Response.Write(item.Name);
                        Response.Write("</a></h4>");
                        Response.Write("	<a class=\"product-btn\" href=\"detail.aspx?id=");
                        Response.Write(item.Id);
                        Response.Write("\"><span>￥");
                        Response.Write(item.Price);
                        Response.Write("</span><small>现在购买</small><label> </label>");
                        Response.Write("	</a>");
                        Response.Write("</div>");
                    }
                %>
                <div class="clearfix"></div>
            </div>
            <!---//special-products-grids---->
        </div>
        <!----->
        <div class="special-products all-poroducts latest-products">
            <div class="s-products-head">
                <div class="s-products-head-left">
                    <h3>最新 <span>产品</span></h3>
                </div>
                <div class="s-products-head-right">
                    <a href="/list.aspx"><span></span>查看全部</a>
                </div>
                <div class="clearfix"></div>
            </div>
            <!----special-products-grids---->
            <div class="special-products-grids">

                <%
                    foreach (var item in DbAccessor.Top<View_Goods>(6, "isnew desc,settop desc,recommended desc,displayorder desc,createddate desc"))
                    {
                        Response.Write("<div class=\"col-md-3 special-products-grid text-center\">");
                        //brand yes!
                        Response.Write("	<a class=\"brand-name\" href=\"list.aspx?brand=");
                        Response.Write(item.BrandId);
                        Response.Write("\">");
                        Response.Write("		<img src=\"");
                        Response.Write(item.BrandLogo);
                        Response.Write("\" title=\"");
                        Response.Write(item.BrandName);
                        Response.Write("\" /></a>");
                        Response.Write("	<a class=\"product-here\" href=\"detail.aspx?id=");
                        Response.Write(item.Id);
                        Response.Write("\">");
                        Response.Write("		<img src=\"");
                        Response.Write(item.PictureUrl);
                        Response.Write("\" title=\"");
                        Response.Write(item.Name);
                        Response.Write("\" /></a>");
                        Response.Write("	<h4><a href=\"detail.aspx?id=");
                        Response.Write(item.Id);
                        Response.Write("\">");
                        Response.Write(item.Name);
                        Response.Write("</a></h4>");
                        Response.Write("	<a class=\"product-btn\" href=\"detail.aspx?id=");
                        Response.Write(item.Id);
                        Response.Write("\"><span>￥");
                        Response.Write(item.Price);
                        Response.Write("</span><small>现在购买</small><label> </label>");
                        Response.Write("	</a>");
                        Response.Write("</div>");
                    }
                %>

                <div class="clearfix"></div>
            </div>
            <!---//special-products-grids---->
        </div>
        <!---//speical-products---->
    </div>
    <!----container---->
</div>
<uc1:foot runat="server" ID="foot" />

