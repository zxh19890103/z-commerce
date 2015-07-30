<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/qb_top.ascx" TagPrefix="uc1" TagName="qb_top" %>
<%@ Register Src="~/shared/qb_foot.ascx" TagPrefix="uc1" TagName="qb_foot" %>


<%
    ChannelNo = 2;
    int id = IMPOSSIBLE;
    View_Goods M = null;
    if (!int.TryParse(Request.QueryString["id"], out id))
        Goto("list.aspx", "参数错误：Id!");

    M = DbAccessor.GetById<View_Goods>(id);
    if (M == null)
        Goto("list.aspx", "没有该记录!");

    GoodsAttributeRenderer gAR = new GoodsAttributeRenderer(M.Id); 
%>
<uc1:qb_top runat="server" ID="qb_top" />


<div class="goods-detail-info">
    <ul class="tags">
        <li><a href="#">
            <img src="/images/menvbt.png"></a></li>
        <li><a href="#">
            <img src="/images/moneybt.png"></a></li>
        <li><a href="#">
            <img src="/images/shopbt.png"></a></li>
    </ul>

    <div class="tag-contents">
        <div class="tag-content-item" id="tagContentItem0">
            <ul>
                <%
                    int code = 0;
                    foreach (var item in gAR.GetGoodsPictures())
                    {
                        code++;
                        Response.Write("<li class=\"goods-pictures-sid-");
                        Response.Write(M.GoodsClassId);
                        Response.Write("\">");
                        Response.Write("		<img class=\"goods-picture-item\" src=\"");
                        Response.Write(item.Src);
                        Response.Write("\"/>");
                        Response.Write("<p>");
                        Response.Write(M.Name);
                        Response.Write("--00");
                        Response.Write(code);
                        Response.Write("</p>");
                        Response.Write("<p>");
                        Response.Write(item.Title);
                        Response.Write("</p>");
                        Response.Write("</li>");
                    }
                %>
            </ul>
        </div>
        <div class="tag-content-item" id="tagContentItem1">
            <div class="tab2_box">
                <h2>详细</h2>
                <p>
                    <%=M.FullDescription %>
                </p>
            </div>
        </div>
        <div class="tag-content-item" id="tagContentItem2">
            <%gAR.RenderAttribute(); %>
            <div class="products-attr attr-ddl">
                <span>数量:</span>
                <span>
                    <select name="Quqntity" id="Quqntity">
                        <%
                            for (int i = 1; i < M.StockQuantity && i < 50; i++)
                            {
                                Response.Write("<option value=\"");
                                Response.Write(i);
                                Response.Write("\">");
                                Response.Write(i);
                                Response.Write("</option>");
                            }
                        %>
                    </select>
                </span>
            </div>
            <div class="product-parameters">
                <%gAR.RenderParameters(); %>
            </div>
            <a href="javascript:;" onclick="addtocart(<%=M.Id %>);" class="addtocartbutton">加入购物车</a>

        </div>
    </div>
</div>

<script type="text/javascript">

    var tab_li=$(".tags>li");
    tab_li.click(function(){
        $(this).addClass("tabli").siblings().removeClass("tabli");
        var $index=tab_li.index(this);
        $("#tagContentItem"+$index).show().siblings().hide();
    });
    
    window.AttriData=<%=gAR.AttributeInfoJSArray%>;

    var CT_DROPDOWNLIST = <%= Goods_Attribute_Mapping.CT_DROPDOWNLIST%>,
        CT_RADIOBUTTONLIST = <%= Goods_Attribute_Mapping.CT_RADIOBUTTONLIST%>,
                CT_CHECKBOXES = <%= Goods_Attribute_Mapping.CT_CHECKBOXES%>,
                CT_TEXTBOX = <%= Goods_Attribute_Mapping.CT_TEXTBOX%>,
                CT_MUTILINETEXTBOX = <%= Goods_Attribute_Mapping.CT_MUTILINETEXTBOX%>,
                CT_DATEPICKER = <%= Goods_Attribute_Mapping.CT_DATEPICKER%>,
                CT_FILEUPLOAD = <%= Goods_Attribute_Mapping.CT_FILEUPLOAD%>,
                CT_COLORSQUARES = <%= Goods_Attribute_Mapping.CT_COLORSQUARES%>;
</script>


<uc1:qb_foot runat="server" ID="qb_foot" />
