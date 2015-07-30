using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Goods_ParameterGroup : BaseEntity, IOrderable, IDisplayable
    {
        [FieldSize(256)]
        public string GroupName { get; set; }
        [FieldSize(1024)]
        public string Description { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
    }
}
