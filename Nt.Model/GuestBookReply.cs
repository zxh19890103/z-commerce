using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class GuestBookReply : BaseEntity, IDisplayable, IOrderable
    {
        public string ReplyContent { get; set; }
        [FKConstraint("GuestBook", "Id")]
        public int GuestBookId { get; set; }
        public DateTime ReplyDate { get; set; }
        [FieldSize(256)]
        public string ReplyMan { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
    }
}
