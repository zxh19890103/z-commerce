using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.Setting
{
    public class ArticleSetting : BaseSetting
    {
        public int PageSize { get; set; }
        public int PagerItemCount { get; set; }
        public string Notice { get; set; }
    }
}
