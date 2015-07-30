using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Goods_Picture_Mapping : BaseEntity
    {
        [NtAttribute.FKConstraint("Goods", "Id")]
        public int GoodsId { get; set; }
        [NtAttribute.FKConstraint("Picture", "Id")]
        public int PictureId { get; set; }
    }
}
