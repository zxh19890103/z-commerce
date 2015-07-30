using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Customer_Consignee:BaseEntity
    {
        [FKConstraint("Customer","Id")]
        public int CustomerId { get; set; }

        [FieldSize(256)]
        public string Name { get; set; }
        [FieldSize(50)]
        public string Phone { get; set; }
        [FieldSize(50)]
        public string Mobile { get; set; }
        [FieldSize(100)]
        public string Email { get; set; }
        [FieldSize(256)]
        public string Address { get; set; }
        [FieldSize(50)]
        public string Zip { get; set; }

    }
}
