using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Brand : BaseLocaleEntity, IPictureAttachable, IDisplayable, IOrderable
    {
        [FieldSize(256)]
        public string Name { get; set; }
        [FieldSize(256)]
        public string PictureUrl { get; set; }
        [FieldSize(1024)]
        public string Description { get; set; }

        [FieldSize(256)]
        public string Url { get; set; }

        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
    }
}
