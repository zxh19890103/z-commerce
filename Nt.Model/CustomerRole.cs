using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class CustomerRole:BaseEntity
    {
        [FieldSize(256)]
        public string Name { get; set; }
        public int MeetPoints { get; set; }
        [FieldSize(1024)]
        public string Note { get; set; }
        public bool Active { get; set; }
    }
}
