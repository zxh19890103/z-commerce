using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class CustomHtmlBlock : BaseEntity
    {
        [FieldSize(int.MaxValue)]
        public string Html { get; set; }
        [FieldSize(100)]
        public string Name { get; set; }
    }
}
