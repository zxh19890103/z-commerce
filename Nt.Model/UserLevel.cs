using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class UserLevel:BaseEntity
    {
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public bool Active { get; set; }
        public string Note { get; set; }
    }
}
