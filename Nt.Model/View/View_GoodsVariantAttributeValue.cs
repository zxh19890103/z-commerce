using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_GoodsVariantAttributeValue : Goods_VariantAttributeValue, IView,IViewMedium
    {
        public int AssociatedGoodsId { get; set; }
        public int AttributeValueTypeId { get; set; }
        public string AttributeName { get; set; }
        public int MaxID { get; set; }

        public string GetScript()
        {
            string sql =
                " Select *," +
                " (Select max(ID) From ##Goods_VariantAttributeValue##) As MaxID" +
                " From ##Goods_VariantAttributeValue## As T0" +
                " Left Join" +
                " (Select Id As TempID, GoodsId As AssociatedGoodsId,ControlType As AttributeValueTypeId,Name As AttributeName From View_GoodsAttribute) As T1" +
                " On T0.GoodsVariantAttributeId=T1.TempID";
            return sql;
        }

    }
}
