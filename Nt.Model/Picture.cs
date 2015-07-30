using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Picture : BaseEntity, IOrderable, IDisplayable
    {
        public int DisplayOrder { get; set; }
        public string Alt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Display { get; set; }
        public string Src { get; set; }
    }
}
