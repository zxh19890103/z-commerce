using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_ShoppingCart : ShoppingCartItem, IView, IViewMedium
    {
        public string CustomerName { get; set; }

        public string GoodsName { get; set; }
        public string GoodsGuid { get; set; }
        public string PictureUrl { get; set; }
        public string ClassName { get; set; }
        public string Measure { get; set; }

        public decimal UnitPrice { get; set; }
        public bool EnableVipPrice { get; set; }
        public decimal UnitVipPrice { get; set; }
        public bool EnableSpecialPrice { get; set; }
        public decimal UnitSpecialPrice { get; set; }
        public DateTime SpecialPriceStartDate { get; set; }
        public DateTime SpecialPriceEndDate { get; set; }

        public int Points { get; set; }

        public bool UseDiscount { get; set; }
        public string DiscountName { get; set; }
        public bool UsePercentage { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal OriginalGoodsCost { get; set; }

        public decimal ItemWeight { get; set; }
        public decimal ItemHeight { get; set; }
        public decimal ItemWidth { get; set; }
        public decimal ItemLength { get; set; }

        public int MaxID { get; set; }

        public string GetScript()
        {
            return
                " Select *," +
                " (Select max(ID) From ##ShoppingCartItem##) As MaxID, " +
                " (select [name] from ##customer## where id=t0.customerid) as CustomerName"+
                " From ##ShoppingCartItem## As T0" +
                " Left Join (Select Id As TempID, Name As GoodsName," +
                " GoodsGuid,PictureUrl,ClassName,Price As UnitPrice,EnableVipPrice,VipPrice As UnitVipPrice,SpecialPrice As UnitSpecialPrice," +
                " Points,DiscountName,UsePercentage,DiscountPercentage,DiscountAmount," +
                " EnableSpecialPrice,SpecialPriceStartDate,SpecialPriceEndDate," +
                " [Weight] As ItemWeight, Cost As OriginalGoodsCost,Measure,UseDiscount," +
                " Height As ItemHeight, Width As ItemWidth, Length As ItemLength" +
                " From View_Goods) As T1" +
                " On T0.GoodsId=T1.TempID";
        }

    }

}
