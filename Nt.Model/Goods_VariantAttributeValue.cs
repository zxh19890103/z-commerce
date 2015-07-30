using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Goods_VariantAttributeValue : BaseEntity, IOrderable
    {
        [FKConstraint("Goods_Attribute_Mapping", "Id")]
        public int GoodsVariantAttributeId { get; set; }
        [FieldSize(400)]
        public string Name { get; set; }
        [FieldSize(256)]
        public string AttributeValue { get; set; }
        public bool Selected { get; set; }
        public decimal PriceAdjustment { get; set; }//价格调整
        public int DisplayOrder { get; set; }
    }
}
