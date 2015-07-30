using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    /// <summary>
    /// 筛选
    /// </summary>
    public class Filter : BaseEntity
    {
        [FieldSize(int.MaxValue)]
        public string Query { get; set; }
        [FieldSize(256)]
        public string CookieID { get; set; }
    }
}
