using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Goods_Promotion : BaseEntity
    {
        [FieldSize(256)]
        public string Tag { get; set; }
        [FieldSize(1024)]
        public string Description { get; set; }
        public bool IsTimeLimited { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPrice { get; set; }
        public int UsePoint { get; set; }
        public bool IsUsePoint { get; set; }
    }
}
