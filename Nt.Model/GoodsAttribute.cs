using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class GoodsAttribute : BaseEntity
    {
        [FieldSize(256)]
        public string Name { get; set; }
        [FieldSize(int.MaxValue)]
        public string Description { get; set; }
    }
}
