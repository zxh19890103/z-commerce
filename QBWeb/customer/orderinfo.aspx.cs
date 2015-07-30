using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using Nt.Model.Enum;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_orderinfo : BaseAspx
{

    protected PriceHandler pricer;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        InCustomerArea = true;
        DB.List("PaymentMethod", "", "DisplayOrder");
        DB.List("ShippingMethod", "", "DisplayOrder");
        DB.List("View_ShoppingCart", "CustomerId=" + NtContext.Current.CustomerID);
        DB.List("Customer_Consignee", "CustomerId=" + NtContext.Current.CustomerID);
        DB.Execute();
    }

    /// <summary>
    /// 支付方式
    /// </summary>
    public void RenderPaymentMethodList()
    {
        var d = new List<PaymentMethod>();
        DbAccessor.FetchListByDataTable<PaymentMethod>(DB[0], d, typeof(PaymentMethod));
        int i = 0;
        foreach (var item in d)
        {
            if (i == 0)
                Response.Write("<input type=\"hidden\" name=\"paymentId\" id=\"paymentId\" value=\"" + item.Id + "\" />");
            Response.Write("<div class=\"pay_column payment\">");
            Response.Write("<a onclick=\"radioSelect(this,'.payment','#paymentId'," + item.Id + ");\" class=\"weixuanzhong" + (i == 0 ? " xuanzhong" : "") + "\" href=\"javascript:;\"></a>");
            Response.Write("<span class=\"span01\">" + item.Name + "</span>");
            Response.Write("<span class=\"span02\">| 手续费：1.00元</span>");
            Response.Write("</div>");
            i++;
        }
    }

    /// <summary>
    /// 配送方式
    /// </summary>
    public void RenderShippingMethodList()
    {
        var d = new List<ShippingMethod>();
        DbAccessor.FetchListByDataTable<ShippingMethod>(DB[1], d, typeof(ShippingMethod));
        int i = 0;
        foreach (var item in d)
        {
            if (i == 0)
                Response.Write("<input type=\"hidden\" name=\"shippingId\" id=\"shippingId\" value=\"" + item.Id + "\" />");
            Response.Write("<div class=\"pay_column shipping\">");
            Response.Write("<a onclick=\"radioSelect(this,'.shipping','#shippingId'," + item.Id + ");\" class=\"weixuanzhong" + (i == 0 ? " xuanzhong" : "") + "\" href=\"javascript:;\"></a>");
            Response.Write("<span class=\"span01\">" + item.Name + "</span>");
            Response.Write("<span class=\"span02\">| 手续费：1.00元</span>");
            Response.Write("</div>");
            i++;
        }
    }

    /// <summary>
    /// 渲染购物清单
    /// </summary>
    public void RenderGoodsBill()
    {
        var d = new List<View_ShoppingCart>();
        DbAccessor.FetchListByDataTable<View_ShoppingCart>(DB[2], d, typeof(View_ShoppingCart));
        pricer = new PriceHandler();

        foreach (var item in d)
        {

            pricer.Run(item);

            Response.Write("<div style=\"height: 3px;\" class=\"clear\"></div>");
            Response.Write("<div class=\"indent_shoppinglist_content\">");
            Response.Write("   <img width=\"75\" height=\"75\" class=\"artpic\" src=\"" + item.PictureUrl + "\" />");
            Response.Write("   <div class=\"commodity_name\">");
            Response.Write("       <p>" + item.GoodsName + "</p>");
            Response.Write("       <p>" + item.GoodsGuid + "</p>");
            Response.Write("   </div>");
            Response.Write("   <span class=\"credits\">+" + item.Points + "</span>");
            Response.Write("   <span class=\"price\">￥" + pricer.SubTotal + "</span>");
            Response.Write("   <div class=\"number\">");
            Response.Write("       " + item.Quantity);
            Response.Write("   </div>");
            Response.Write("   <span class=\"privilege\">￥" + pricer.SubTotalDiscountAmount + "</span>");
            Response.Write("   <span class=\"pricesubtotle\">￥" + pricer.SubTotal + "</span>");
            Response.Write("   <span class=\"creditssubtotle\">" + pricer.SubTotalPoints + "</span>");
            Response.Write("</div>");
        }

    }

    /// <summary>
    /// 收货方式
    /// </summary>
    public void RenderConsignee()
    {
        var d = new List<Customer_Consignee>();
        DbAccessor.FetchListByDataTable<Customer_Consignee>(DB[3], d, typeof(Customer_Consignee));
        Response.Write("<span>");
        int rand = NtUtility.GenerateRandomInteger(0, int.MaxValue);
        Response.Write("<input type=\"radio\" checked=\"checked\" id=\"" +
                rand + "_0\" name=\"ConsigneeId\" value=\"0\"/>");
        Response.Write("<label class=\"span02\" style=\"padding-left:5px;\"  for=\"" + rand + "_0\">填写新的</label>");
        foreach (var item in d)
        {
            Response.Write("<input type=\"radio\" id=\"" +
                rand + "_" + item.Id + "\" name=\"ConsigneeId\" value=\"" + item.Id + "\"/>");
            Response.Write("<label class=\"span02\" style=\"padding-left:5px;\"  for=\"" +
                rand + "_" + item.Id + "\">" + item.Name + "</label>");
        }
        Response.Write("</span>");
    }

}