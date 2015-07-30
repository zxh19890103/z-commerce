using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class ProductCategory : BaseTreeEntity, IPictureAttachable, ILocaleEntity, IDeletable
    {
        public int LanguageId { get; set; }
        [FieldSize(512)]
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        [FieldSize(256)]
        public string PictureUrl { get; set; }
        [FieldSize(int.MaxValue)]
        public string Description { get; set; }
        [FieldSize(256)]
        public string ListTemplate { get; set; }
        [FieldSize(256)]
        public string DetailTemplate { get; set; }

        public bool Deleted { get; set; }
    }
}
