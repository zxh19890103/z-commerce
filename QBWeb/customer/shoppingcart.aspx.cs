using Nt.DAL;
using Nt.Framework;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_shoppingcart : BaseAspx
{

    protected PriceHandler pricer;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        InCustomerArea = true;
    }
    
    /// <summary>
    /// 渲染购物车列表
    /// </summary>
    [AjaxAuthorize(Nt.Framework.AuthorizeFlag.Customer)]
    public void RenderShoppingCart()
    {
        var m = Nt.BLL.NtContext.Current.CurrentCustomer;

        List<View_ShoppingCart> list = DbAccessor.GetList<View_ShoppingCart>("CustomerId=" + m.Id);

        pricer = new PriceHandler();

        foreach (var item in list)
        {
            pricer.Run(item);
            Response.Write("<div class=\"shoppinglist_content\" id=\"cart-item-" + item.Id + "\">");
            Response.Write("	<img class=\"artpic\" width=\"75\" height=\"75\" src=\"" + item.PictureUrl + "\"/>");
            Response.Write("	<div class=\"commodity_name\">");
            Response.Write("		<p>" + item.GoodsName + "</p>");
            Response.Write("		<p>" + item.GoodsGuid + "</p>");
            Response.Write("	</div>");
            Response.Write("	<span class=\"credits\">+" + item.Points + "</span>");
            Response.Write("	<span class=\"price\">￥" + pricer.StandardPrice.ToString("f2") + "</span>");
            Response.Write("	<div class=\"number\">");
            Response.Write("<input type=\"hidden\" value=\"" + item.Quantity + "\" name=\"Goods.Quantity\" id=\"Quantity_" + item.Id + "\"/>");
            Response.Write("		<span class=\"span01\" onclick=\"setQuantity(this," + item.Id + ",-1);\"></span>");
            Response.Write("		<span class=\"span02\">" + item.Quantity + "</span>");
            Response.Write("		<span class=\"span03\" onclick=\"setQuantity(this," + item.Id + ",1);\"></span>");
            Response.Write("	</div>");
            Response.Write("	<span class=\"privilege\">￥" + pricer.SubTotalDiscountAmount.ToString("f2") + "</span>");
            Response.Write("	<span class=\"pricesubtotle\">￥" + pricer.SubTotal + "</span>");
            Response.Write("	<span class=\"creditssubtotle\">" + pricer.SubTotalPoints + "</span>");
            Response.Write("	<span class=\"operation\" onclick=\"delFromCart(" + item.Id + ")\">删除</span>");
            Response.Write("</div>");
        }

        Response.Write("<div style=\"height: 3px;\" class=\"clear\"></div>");
        Response.Write("<div class=\"shoppinglist_foot\">");
        Response.Write("	<p class=\"p1\">" + pricer.TotalQuantity + "件</p>");
        Response.Write("	<p class=\"p2\">￥" + pricer.Total.ToString("f2") + "</p>");
        Response.Write("	<p class=\"p3\">" + pricer.TotalPoints + "分</p>");
        Response.Write("</div>");
    }
}