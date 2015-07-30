using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Day:BaseEntity
    {
        public string Title { get; set; }
        public string EnglishTitle { get;set;}
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime Date { get; set; }
    }
}
