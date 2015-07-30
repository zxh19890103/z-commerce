using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model.NtAttribute;

namespace Nt.Model
{
    public class Goods_Ask : BaseEntity
    {
        [FKConstraint("Goods", "Id")]
        public int GoodsId { get; set; }
        [FKConstraint(typeof(Customer), "Id")]
        public int CustomerId { get; set; }
        [FieldSize(256)]
        public string Type { get; set; }
        [FieldSize(512)]
        public string Title { get; set; }
        [FieldSize(1024)]
        public string Body { get; set; }
        [FieldSize(1024)]
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}