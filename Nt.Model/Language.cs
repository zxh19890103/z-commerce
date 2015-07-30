using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Language:BaseEntity,IOrderable
    {
        [FieldSize(10)]
        public string LanguageCode { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        [FieldSize(256)]
        public string Name { get; set; }
    }
}
