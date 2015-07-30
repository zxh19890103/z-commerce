﻿using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Banner : BaseLocaleEntity, IOrderable, IPictureAttachable, IDisplayable
    {
        [FieldSize(256)]
        public string PictureUrl { get; set; }
        [FieldSize(256)]
        public string Url { get; set; }
        [FieldSize(512)]
        public string Title { get; set; }
        [FieldSize(1024)]
        public string Text { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
    }
}
