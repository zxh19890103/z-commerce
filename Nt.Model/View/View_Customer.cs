using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Customer : Customer, IView
    {
        public string RoleName { get; set; }
        public string RoleMeetPoints { get; set; }
        public int MaxID { get; set; }

        public int TotalOfShoppingCartItem { get; set; }
        public int TotalOfReviews { get; set; }
        public int TotalOfWishlist { get; set; }
        public int TotalOfAsk { get; set; }
        /// <summary>
        /// 未阅消息
        /// </summary>
        public int TotalOfMessage { get; set; }
        public int TotalOfConsignee { get; set; }

        public string GetScript()
        {
            return
                " Select *,(Select max(ID) From ##Customer##) As MaxID,"+
                " (select count(0) from ##shoppingcartitem## where CustomerId=t0.id) as TotalOfShoppingCartItem," +
                " (select count(0) from ##goods_review## where CustomerId=t0.id) as TotalOfReviews," +
                " (select count(0) from ##Customer_Wishlist## where CustomerId=t0.id) as TotalOfWishlist," +
                " (select count(0) from ##Goods_Ask## where CustomerId=t0.id) as TotalOfAsk," +
                " (select count(0) from ##Customer_Message_Customer_Mapping## where CustomerId=t0.id and Viewed=0) as TotalOfMessage," +
                " (select count(0) from ##Customer_Consignee## where CustomerId=t0.id) as TotalOfConsignee" +
                " From ##Customer## as T0" +
                " Left Join " +
                " (Select Id As TempID,Name As RoleName,MeetPoints As RoleMeetPoints From ##CustomerRole##) As T " +
                " On t0.CustomerRoleId=T.TempID";
        }

    }
}
