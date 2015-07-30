using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class ShoppingCartItem : BaseEntity
    {
        [FKConstraint("Customer", "Id")]
        public int CustomerId { get; set; }

        [FKConstraint("Goods", "Id")]
        public int GoodsId { get; set; }

        public int Quantity { get; set; }
        [FieldSize(1024)]
        public string AttributesXml { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
