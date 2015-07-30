using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Measure : BaseLocaleEntity, IDisplayable, IOrderable
    {
        [FieldSize(256)]
        public string Name { get; set; }
        [FieldSize(512)]
        public string Description { get; set; }

        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
    }
}
