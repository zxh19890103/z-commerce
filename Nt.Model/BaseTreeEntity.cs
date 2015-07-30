using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public abstract class BaseTreeEntity : BaseEntity, IDisplayable, IOrderable
    {
        public int Depth { get; set; }
        public int Parent { get; set; }
        [FieldSize(512)]
        public string Crumbs { get; set; }
        [FieldSize(256)]
        public string Name { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
    }
}
