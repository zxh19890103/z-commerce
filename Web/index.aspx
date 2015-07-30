<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>


<!----container---->
<uc1:top runat="server" ID="top1" />

<!----start-slider-script---->
<script src="js/responsiveslides.min.js"></script>
<script>
    // You can also use "$(window).load(function() {"
    $(function () {
        // Slideshow 4
        $("#slider4").responsiveSlides({
            auto: true,
            pager: true,
            nav: true,
            speed: 500,
            namespace: "callbacks",
            before: function () {
                $('.events').append("<li>before event fired.</li>");
            },
            after: function () {
                $('.events').append("<li>after event fired.</li>");
            }
        });

    });
</script>
<!----//End-slider-script---->
<!-- Slideshow 4 -->
<div id="top" class="callbacks_container">
    <ul class="rslides" id="slider4">

        <%
                
            //sliders
            foreach (var s in DbAccessor.GetList<Slider>())
            {
                Response.Write("<li>");
                Response.Write("	<img src=\"");
                Response.Write(s.PictureUrl);
                Response.Write("\" alt=\"\"/>");
                Response.Write("	<div class=\"caption\">");
                Response.Write("		<div class=\"slide-text-info\">");
                Response.Write("<a href=\"");
                Response.Write(s.Url);
                Response.Write("\">");
                Response.Write("			<h1>");
                Response.Write(s.Title);
                Response.Write("</h1>");
                Response.Write("			</a>");
                Response.Write("		</div>");
                Response.Write("	</div>");
                Response.Write("</li>");
            }                
        %>
    </ul>
</div>
<div class="clearfix"></div>
<!----- //End-slider---->
<!----content---->
<div class="content">

    <div class="container">
        <!---top-row--->
        <div class="top-row">

            <%
                //三个商品类别
                foreach (var item in DbAccessor.Top<View_GoodsClass>(3, "Id in (1,2,3)", ""))
                {
                    Response.Write("<div class=\"col-xs-4\">");
                    Response.Write("	<div class=\"top-row-col1 text-center\">");
                    Response.Write("		<h2>");
                    Response.Write(item.Name);
                    Response.Write("</h2>");
                    Response.Write("		<img class=\"r-img text-center\" src=\"");
                    Response.Write(_mediaService.MakeThumbnail(item.PictureUrl, 189, 189, ThumbnailGenerationMode.HW));
                    Response.Write("\" title=\"");
                    Response.Write(item.Name);
                    Response.Write("\" />");
                    Response.Write("		<h4>TOTAL</h4>");
                    Response.Write("		<label>");
                    Response.Write(item.Total);
                    Response.Write(" 个商品</label>");
                    Response.Write("		<a class=\"r-list-w\" href=\"list.aspx?sid=");
                    Response.Write(item.Id);
                    Response.Write("\">");
                    Response.Write("			<img src=\"/content/images/list-icon.png\" title=\"list\" /></a>");
                    Response.Write("	</div>");
                    Response.Write("</div>");
                }
                    
            %>

            <div class="clearfix"></div>
        </div>
    </div>
    <!---top-row--->
    <div class="container">
        <!----speical-products---->
        <div class="special-products">
            <div class="s-products-head">
                <div class="s-products-head-left">
                    <h3>特价 <span>商品</span></h3>
                </div>
                <div class="s-products-head-right">
                    <a href="list.aspx"><span></span>浏览全部商品</a>
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
        <!---//speical-products---->
    </div>
    <!----content---->


    <!----container---->
</div>

<uc1:foot runat="server" ID="foot" />

