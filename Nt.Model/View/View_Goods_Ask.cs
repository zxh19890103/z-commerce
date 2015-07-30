using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Goods_Ask : Goods_Ask, IView
    {
        public int MaxID { get; set; }
        public string GoodsName { get; set; }
        public string CustomerName { get; set; }

        public string GetScript()
        {
            return "Select *,0 As MaxID," +
                " (Select Name From ##Goods## Where ID=T1.GoodsId) As GoodsName," +
                " (Select Name From ##Customer## Where ID=T1.CustomerId) As CustomerName" +
                " From ##Goods_Ask## As T1";
        }
    }
}
