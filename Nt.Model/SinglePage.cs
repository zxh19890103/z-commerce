using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class SinglePage : BaseLocaleEntity, IHtmlizable
    {
        [FieldSize(256)]
        public string Title { get; set; }
        [FieldSize(1024)]
        public string Short { get; set; }
        [FieldSize(int.MaxValue)]
        public string Body { get; set; }
        [FieldSize(100)]
        public string FirstPicture { get; set; }
        [FieldSize(1024)]
        public string SeoTitle { get; set; }
        [FieldSize(1024)]
        public string SeoKeywords { get; set; }
        [FieldSize(1024)]
        public string SeoDescription { get; set; }

        public string ListTemplate { get; set; }
        public string DetailTemplate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid Guid { get; set; }

    }
}
