using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Goods : Goods, IView, IHtmlizable, ISiblingsTraceable
    {
        public string ClassName { get; set; }
        public string ClassCrumbs { get; set; }
        public string DiscountName { get; set; }
        public bool UseDiscount { get; set; }
        public bool UsePercentage { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public string ListTemplate { get; set; }
        public string DetailTemplate { get; set; }
        public int MaxID { get; set; }
        public int Reviews { get; set; }
        public string BrandName { get; set; }
        public string BrandLogo { get; set; }

        public int PreID { get; set; }
        public int NextID { get; set; }
        public string PreTitle { get; set; }
        public string NextTitle { get; set; }

        public string GetScript()
        {
            return
                " Select *,(Select max(ID) From ##Goods##) As MaxID,0 As PreID,0 As NextID,'' As PreTitle,'' As NextTitle  " +
                " From (" +
                " Select *,cast(DiscountId as bit) As UseDiscount,"+
                " (select count(0) from ##goods_review## where goodsid=T0.Id) As Reviews,"+
                " (select [name] from ##brand## where Id=T0.BrandId) as BrandName," +
                " (select [pictureurl] from ##brand## where Id=T0.BrandId) as BrandLogo" +
                " From ##Goods## As T0" +
                " Left Join " +
                " (Select Id As TempID1, Name As ClassName,Crumbs As ClassCrumbs,ListTemplate,DetailTemplate From ##Goods_Class##) As T1" +
                " ON T0.GoodsClassId=T1.TempID1) As T0T1" +
                " Left Join " +
                " (Select Id As TempID2, Name As DiscountName,UsePercentage,DiscountPercentage,DiscountAmount From ##Discount##) As T2" +
                " ON T0T1.DiscountId=T2.TempID2";
        }

    }
}
