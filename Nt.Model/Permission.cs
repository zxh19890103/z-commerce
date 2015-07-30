using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Permission : BaseEntity, IOrderable
    {
        public bool IsCategory { get; set; }
        public int FatherId { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string SystemName { get; set; }
        public string Src { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
    }
}
