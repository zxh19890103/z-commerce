<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Import Namespace="System.Linq" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>
<%@ Register Src="~/shared/filter.ascx" TagPrefix="uc1" TagName="filter" %>

<%
    int id = IMPOSSIBLE;
    View_Goods M = null;
    if (!int.TryParse(Request.QueryString["id"], out id))
        Goto("list.aspx", "参数错误：Id!");

    M = DbAccessor.GetById<View_Goods>(id);
    if (M == null)
        Goto("list.aspx", "没有该记录!");

    GoodsAttributeRenderer r = new GoodsAttributeRenderer(M.Id);
    
    RegisterStyle("content/css/goods-detail.css");
    SeoTitle = M.Name;
%>

<uc1:top runat="server" ID="top" />

<div class="content">
    <div class="container">
        <uc1:filter runat="server" ID="filter" />
    </div>
    <div class="clearfix"></div>
    <!-- //products ---->
    <!----product-details--->
    <div class="product-details">
        <div class="container">
            <div class="product-details-row1">
                <div class="product-details-row1-head">
                    <h2><%=M.Name %></h2>
                    <p><%=M.ClassName %></p>
                </div>
                <div class="col-md-4 product-details-row1-col1">
                    <!----details-product-slider--->
                    <!-- Include the Etalage files -->
                    <link type="text/css" rel="stylesheet" href="/content/css/etalage.css" />
                    <script src="js/jquery.etalage.min.js" type="text/javascript"></script>
                    <!-- Include the Etalage files -->
                    <script type="text/javascript">
                        jQuery(document).ready(function ($) {

                            $('#etalage').etalage({
                                thumb_image_width: 300,
                                thumb_image_height: 400,
                                source_image_width: 900,
                                source_image_height: 1000,
                                show_hint: true,
                                click_callback: function (image_anchor, instance_id) {
                                    alert('Callback example:\nYou clicked on an image with the anchor: "' + image_anchor + '"\n(in Etalage instance: "' + instance_id + '")');
                                }
                            });
                            // This is for the dropdown list example:
                            $('.dropdownlist').change(function () {
                                etalage_show($(this).find('option:selected').attr('class'));
                            });
                        });
                    </script>
                    <!----//details-product-slider--->
                    <div class="details-left">
                        <div class="details-left-slider">
                            <ul id="etalage">

                                <%
                                    foreach (var item in DbAccessor.GetList<View_GoodsPicture>(
                                        "GoodsId=" + M.Id + " and display=1 ", "displayorder desc"))
                                    {
                                        Response.Write("<li>");
                                        //Response.Write("	<a href=\"optionallink.html\">"); no link!
                                        Response.Write("		<img class=\"etalage_thumb_image\" src=\"");
                                        Response.Write(Thumb(item.Src, 357, 515));
                                        Response.Write("\"/>");
                                        Response.Write("		<img class=\"etalage_source_image\" src=\"");
                                        Response.Write(item.Src);
                                        Response.Write("\" />");
                                        // Response.Write("	</a>");
                                        Response.Write("</li>");
                                    }
                                %>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-md-8 product-details-row1-col2">
                    <div class="product-rating">
                        <a class="rate" href="#"><span></span></a>
                        <label><a href="#tab-3">查看 <b><%=M.Reviews %></b> 条评论</a></label>
                    </div>
                    <div class="product-price">
                        <div class="product-price-left text-right">
                            <span>￥<%=M.OldPrice.ToString("f") %></span>
                            <h5>￥<%=M.Price.ToString("f") %></h5>
                        </div>
                        <div class="product-price-right">
                            <a href="#"><span></span></a>
                            <label>节省 <b>￥<%=(M.OldPrice-M.Price).ToString("f") %></b></label>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="product-price-details">
                        <p class="text-right">
                            <%=M.ShortDescription %>
                        </p>
                        <a class="shipping" href="#"><span></span>Free shipping</a>
                        <div class="clearfix"></div>
                        <div class="product-size-qty">

                            <%
                                r.RenderAttribute();
                            %>

                            <div class="pro-qty">
                                <span>数量:</span>

                                <%
                                    if (M.StockQuantity > 0)
                                    {
                                        Response.Write("<select id=\"Quqntity\">");
                                        for (int i = 1; i <= M.StockQuantity && i < 50; i++)
                                        {
                                            Response.Write("<option>");
                                            Response.Write(i);
                                            Response.Write("</option>");
                                        }
                                        Response.Write("</select>");
                                    }
                                    else
                                    {
                                        Response.Write("暂时缺货!");
                                    }
                                %>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="product-cart-share">
                            <div class="add-cart-btn">
                                <input type="button" <%if (M.StockQuantity == 0) Response.Write("disabled=\"disabled\""); %> value="加入购物车" onclick="addtocart(<%=M.Id%>);" />
                            </div>
                            <div class="clearfix"></div>
                            <ul class="product-share text-right">
                                <h3>分享此页面:</h3>
                                <ul>
                                    <li><a class="share-163-weibo" href="#"><span></span></a></li>
                                    <li><a class="share-baidu" href="#"><span></span></a></li>
                                    <li><a class="share-kaixin" href="#"><span></span></a></li>
                                    <li><a class="share-qq" href="#"><span></span></a></li>
                                    <li><a class="share-qzone" href="#"><span></span></a></li>
                                    <li><a class="share-qq-weibo" href="#"><span></span></a></li>
                                    <li><a class="share-sina" href="#"><span></span></a></li>
                                    <li><a class="share-renren" href="#"><span></span></a></li>
                                    <li><a class="share-souhu-weibo" href="#"><span></span></a></li>
                                    <li><a class="share-yahoo" href="#"><span></span></a></li>
                                    <li><a class="share-flickr" href="#"><span></span></a></li>
                                    <li><a class="share-delicious" href="#"><span></span></a></li>
                                    <li><a class="share-face" href="#"><span></span></a></li>
                                    <li><a class="share-twitter" href="#"><span></span></a></li>
                                    <li><a class="share-google" href="#"><span></span></a></li>
                                    <li><a class="share-rss" href="#"><span></span></a></li>
                                    <div class="clear"></div>
                                </ul>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <!--//product-details--->
            </div>
            <!---- product-details ---->
            <div class="product-tabs">
                <!--Horizontal Tab-->
                <div id="horizontalTab">
                    <ul>
                        <li><a href="#tab-1">商品概况</a></li>
                        <li><a href="#tab-2">参数</a></li>
                        <li><a href="#tab-3">评论</a></li>
                    </ul>
                    <!--文本-->
                    <div id="tab-1" class="product-complete-info">
                        <h3>描述:</h3>
                        <p>
                            <%=M.ShortDescription %>
                        </p>
                        <span>详细:</span>
                        <div class="product-fea">
                            <%=M.FullDescription %>
                        </div>
                    </div>
                    <!--参数-->
                    <div id="tab-2" class="product-complete-info">
                        <%
                            r.RenderParameters();                                
                        %>
                    </div>
                    <!--评论-->
                    <div id="tab-3" class="product-complete-info">
                        <ul class="nt-reviews">
                            <%
                                foreach (var item in DbAccessor.GetList<View_Goods_Review>("GoodsId=" + M.Id))
                                {
                                    Response.Write("<li class=\"nt-review-item\">");
                                    Response.Write("<span class=\"nt-review-body\">");
                                    Response.Write(item.Body);
                                    Response.Write("</span><div class=\"nt-review-customer-date\">");
                                    Response.Write("<span class=\"nt-review-customer\">");
                                    Response.Write(item.CustomerName);
                                    Response.Write("</span>");
                                    Response.Write("<span class=\"nt-review-date\">");
                                    Response.Write(item.CreatedDate);
                                    Response.Write("</span><span class=\"nt-review-nice\" onclick=\"rating(this,");
                                    Response.Write(item.Id);
                                    Response.Write(");\">赞(<rating>");
                                    Response.Write(item.Rating);
                                    Response.Write("</rating>)</span></div>");
                                    Response.Write("</li>");
                                }
                            %>
                        </ul>
                        <form action="handlers/ajaxHandler.aspx" id="reviewForm">
                            <textarea cols="1" rows="2" class="nt-textarea" name="review" id="ReviewBody"></textarea>
                            <br />
                            <input type="submit" class="nt-submit" value="提交" onclick="return postReview(<%=M.Id%>);" />
                        </form>
                    </div>
                </div>
                <!-- Responsive Tabs JS -->
                <script src="js/jquery.responsiveTabs.js" type="text/javascript"></script>

                <script type="text/javascript">
                    $(document).ready(function () {
                        $('#horizontalTab').responsiveTabs({
                            rotate: false,
                            startCollapsed: 'accordion',
                            collapsible: 'accordion',
                            setHash: true,
                            disabled: [3, 4],
                            activate: function (e, tab) {
                                $('.info').html('Tab <strong>' + tab.id + '</strong> activated!');
                            }
                        });

                        $('#start-rotation').on('click', function () {
                            $('#horizontalTab').responsiveTabs('active');
                        });
                        $('#stop-rotation').on('click', function () {
                            $('#horizontalTab').responsiveTabs('stopRotation');
                        });
                        $('#start-rotation').on('click', function () {
                            $('#horizontalTab').responsiveTabs('active');
                        });
                        $('.select-tab').on('click', function () {
                            $('#horizontalTab').responsiveTabs('activate', $(this).val());
                        });

                    });
                </script>

            </div>
            <!-- //product-details ---->
        </div>
    </div>
</div>
<div class="clearfix"></div>

<script type="text/javascript">

    window.AttriData=<%=r.AttributeInfoJSArray%>;

    var CT_DROPDOWNLIST = <%= Goods_Attribute_Mapping.CT_DROPDOWNLIST%>,
        CT_RADIOBUTTONLIST = <%= Goods_Attribute_Mapping.CT_RADIOBUTTONLIST%>,
                CT_CHECKBOXES = <%= Goods_Attribute_Mapping.CT_CHECKBOXES%>,
                CT_TEXTBOX = <%= Goods_Attribute_Mapping.CT_TEXTBOX%>,
                CT_MUTILINETEXTBOX = <%= Goods_Attribute_Mapping.CT_MUTILINETEXTBOX%>,
                CT_DATEPICKER = <%= Goods_Attribute_Mapping.CT_DATEPICKER%>,
                CT_FILEUPLOAD = <%= Goods_Attribute_Mapping.CT_FILEUPLOAD%>,
                CT_COLORSQUARES = <%= Goods_Attribute_Mapping.CT_COLORSQUARES%>;
</script>

<!----container---->

<uc1:foot runat="server" ID="foot" />
