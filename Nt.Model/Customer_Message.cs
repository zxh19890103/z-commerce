using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Customer_Message : BaseEntity
    {
        [FieldSize(256)]
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
