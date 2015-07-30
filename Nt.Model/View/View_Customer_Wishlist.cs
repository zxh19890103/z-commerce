using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Customer_Wishlist:Customer_Wishlist,IView
    {
        public int MaxID { get; set; }
        public string GoodsName { get; set; }
        public string CustomerName { get; set; }

        public string GetScript()
        {
            return "Select *,0 As MaxID," +
                " (Select Name From ##Goods## Where ID=T1.GoodsId) As GoodsName," +
                " (Select Name From ##Customer## Where ID=T1.CustomerId) As CustomerName" +
                " From ##Customer_Wishlist## As T1";
        }
    }
}
