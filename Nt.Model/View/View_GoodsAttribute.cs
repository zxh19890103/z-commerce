using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_GoodsAttribute : Goods_Attribute_Mapping, IView
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxID { get; set; }

        public string GetScript()
        {
            string sql =
                " Select *," +
                " (Select max(ID) From ##Goods_Attribute_Mapping##) As MaxID " +
                " From ##Goods_Attribute_Mapping## As T0," +
                " (Select Id As TempID,Name,Description From ##GoodsAttribute##) As T1" +
                " Where T0.GoodsAttributeId=T1.TempID";
            return sql;
        }
    }
}
