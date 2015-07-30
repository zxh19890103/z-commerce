using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class WebsiteLinkItem : BaseLocaleEntity
    {
        public string Word { get; set; }
        public string Url { get; set; }
        public bool Applied { get; set; }
    }
}
