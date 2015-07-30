using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Log : BaseEntity
    {
        public int UserID { get; set; }
        [FieldSize(50)]
        public string LoginIP { get; set; }
        public string Description { get; set; }
        [FieldSize(256)]
        public string RawUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
