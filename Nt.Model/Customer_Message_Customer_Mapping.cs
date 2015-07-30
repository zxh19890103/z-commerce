using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Customer_Message_Customer_Mapping : BaseEntity
    {
        [NtAttribute.FKConstraint(typeof(Customer), "Id")]
        public int CustomerId { get; set; }

        [NtAttribute.FKConstraint(typeof(Customer_Message), "Id")]
        public int CustomerMessageId { get; set; }

        public bool Viewed { get; set; }
        public DateTime SentDate { get; set; }

    }
}
