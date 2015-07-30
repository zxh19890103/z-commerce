using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Area : BaseEntity, IOrderable
    {
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}
