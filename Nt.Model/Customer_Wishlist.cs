using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Customer_Wishlist : BaseEntity
    {
        [NtAttribute.FKConstraint("Customer", "Id")]
        public int CustomerId { get; set; }

        [NtAttribute.FKConstraint("Goods", "Id")]
        public int GoodsId { get; set; }
    }
}
