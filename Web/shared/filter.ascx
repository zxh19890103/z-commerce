<%@ Control Language="C#" ClassName="filter" Inherits="Nt.Framework.BaseAscx" %>

<div class="products">
    <div class="product-filter">
        <h1><a href="#">筛选</a></h1>
        <div class="product-filter-grids">
            <div class="col-md-3 product-filter-grid1-brands">
                <h3>品牌</h3>
                <%
                    int rows = 9, r = 0, c = 0;
                    var brands = DbAccessor.GetList<Brand>();

                    Response.Write("<ul class=\"col-md-6 unstyled-list b-list1\">");

                    foreach (var item in brands)
                    {
                        Response.Write("<li><a href=\"list.aspx?brand=");
                        Response.Write(item.Id);
                        Response.Write("\">");
                        Response.Write(item.Name);
                        Response.Write("</a></li>");

                        r++;
                        c++;

                        if (c == brands.Count)
                        {
                            Response.Write("<div class=\"clearfix\"></div></ul>");
                        }

                        if (r == rows)
                        {
                            r = 0;
                            Response.Write("<div class=\"clearfix\"></div></ul>");
                            Response.Write("<ul class=\"col-md-6 unstyled-list b-list2\">");
                        }
                    }
                %>
                <div class="clearfix"></div>
            </div>
            <!---->
            <div class="col-md-6 product-filter-grid1-brands-col2">
                <div class="producst-cate-grids">

                    <%
                        //三个商品类别
                        foreach (var item in DbAccessor.Top<View_GoodsClass>(3, "Id in (1,2,3)", ""))
                        {
                            Response.Write("<div class=\"col-md-4 producst-cate-grid text-center\">");
                            Response.Write("	<h2>");
                            Response.Write(item.Name);
                            Response.Write("</h2>");
                            Response.Write("	<img class=\"r-img text-center img-responsive\" src=\"");
                            Response.Write(item.PictureUrl);
                            Response.Write("\" title=\"");
                            Response.Write(item.Name);
                            Response.Write("\">");
                            Response.Write("	<h4>TOTAL</h4>");
                            Response.Write("	<label>");
                            Response.Write(item.Total);
                            Response.Write(" 商品</label>");
                            Response.Write("	<a class=\"r-list-w\" href=\"/list.aspx?sid=");
                            Response.Write(item.Id);
                            Response.Write("\">");
                            Response.Write("		<img src=\"/content/images/list-icon.png\" title=\"list\"></a>");
                            Response.Write("</div>");
                        }
                    
                    %>
                </div>
            </div>
            <!---->

            <div class="product-filter-grid1-brands-col3">
                <%--<div class="products-colors">
                    <h3>SELECT COLOR </h3>
                    <li><a href="#"><span class="color1"></span></a></li>
                    <li><a href="#"><span class="color2"></span></a></li>
                    <li><a href="#"><span class="color3"></span></a></li>
                    <li><a href="#"><span class="color4"></span></a></li>
                    <li><a href="#"><span class="color5"></span></a></li>
                    <li><a href="#"><span class="color6"></span></a></li>
                    <li><a href="#"><span class="color7"></span></a></li>
                    <li><a href="#"><span class="color8"></span></a></li>
                    <li><a href="#"><span class="color9"></span></a></li>
                    <li><a href="#"><span class="color10"></span></a></li>
                    <li><a href="#"><span class="color11"></span></a></li>
                    <li><a href="#"><span class="color12"></span></a></li>
                    <li><a href="#"><span class="color13"></span></a></li>
                    <li><a href="#"><span class="color14"></span></a></li>
                    <li><a href="#"><span class="color15"></span></a></li>
                    <li><a href="#"><span class="color16"></span></a></li>
                    <li><a href="#"><span class="color17"></span></a></li>
                    <li><a href="#"><span class="color18"></span></a></li>
                    <li><a href="#"><span class="color19"></span></a></li>
                    <li><a href="#"><span class="color20"></span></a></li>
                    <div class="clearfix"></div>
                </div>--%>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
</div>
