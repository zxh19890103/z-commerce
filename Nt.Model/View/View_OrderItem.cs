using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_OrderItem : OrderItem, IView
    {
        public string GoodsGuid { get; set; }
        public string GoodsName { get; set; }
        public decimal GoodsPrice { get; set; }
        public decimal GoodsWeight { get; set; }
        public decimal GoodsHeight { get; set; }
        public decimal GoodsWidth { get; set; }
        public decimal GoodsLength { get; set; }

        public int MaxID { get; set; }

        public string GetScript()
        {
            return
                " Select *," +
                " 0 As MaxID From ##OrderItem## As T0 Left Join" +
                " (Select Id As TempId,GoodsGuid, Name As GoodsName,Price As GoodsPrice,Weight As GoodsWeight," +
                "  Height As GoodsHeight,Width As GoodsWidth,Length As GoodsLength" +
                " From ##Goods##) As T1" +
                " On T0.GoodsId=T1.TempId";
        }
    }
}
