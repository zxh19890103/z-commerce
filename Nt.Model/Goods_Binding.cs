using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Goods_Binding : BaseEntity
    {
        [FKConstraint("Goods", "Id")]
        public int GoodsId { get; set; }
        public int BindingId { get; set; }
    }
}
