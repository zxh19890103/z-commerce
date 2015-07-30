using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class ShippingMethod : BaseEntity, IOrderable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}
