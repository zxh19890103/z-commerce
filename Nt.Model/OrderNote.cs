using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class OrderNote : BaseEntity
    {
        [NtAttribute.FKConstraint("Order", "Id")]
        public int OrderId { get; set; }
        [NtAttribute.FieldSize(int.MaxValue)]
        public string Note { get; set; }
        public bool DisplayToCustomer { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
