using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Faq : BaseEntity, IDisplayable, IOrderable
    {
        [FieldSize(1024)]
        public string Question { get; set; }

        [FieldSize(1024)]
        public string Answer { get; set; }

        public bool Display { get; set; }

        public int DisplayOrder { get; set; }

        public int Type { get; set; }

    }
}
