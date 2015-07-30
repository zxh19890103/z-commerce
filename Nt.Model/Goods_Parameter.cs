using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Goods_Parameter:BaseEntity
    {
        [FKConstraint("Goods_ParameterGroup", "Id")]
        public int GoodsParameterGroupId { get; set; }
        [FKConstraint("Goods", "Id")]
        public int GoodsId { get; set; }
        [FieldSize(256)]
        public string Name { get; set; }
        [FieldSize(int.MaxValue)]
        public string Value { get; set; }
    }
}
