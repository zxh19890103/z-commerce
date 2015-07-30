using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Goods_Review : BaseEntity
    {
        [FKConstraint("Customer", "Id")]
        public int CustomerId { get; set; }
        [FKConstraint("Goods", "Id")]
        public int GoodsId { get; set; }
        public bool IsApproved { get; set; }
        [FieldSize(256)]
        public string Title { get; set; }
        [FieldSize(1024)]
        public string Body { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
