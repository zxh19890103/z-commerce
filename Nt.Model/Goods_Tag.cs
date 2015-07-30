using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Goods_Tag : BaseLocaleEntity, IOrderable, IDisplayable
    {
        [FieldSize(256)]
        public string Tag { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
