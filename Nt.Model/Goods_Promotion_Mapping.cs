using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Goods_Promotion_Mapping : BaseEntity
    {
        [NtAttribute.FKConstraint("Goods", "Id")]
        public int GoodsId { get; set; }
        [NtAttribute.FKConstraint("Goods_Promotion", "Id")]
        public int PromotionId { get; set; }
    }
}
