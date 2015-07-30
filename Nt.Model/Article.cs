using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Article : BaseLocaleEntity, IOrderable, IPictureAttachable, IDisplayable, IUserTarceable, IGuidModel, IDeletable
    {
        [FieldSize(512)]
        public string Title { get; set; }
        [FieldSize(256)]
        public string Author { get; set; }
        [FieldSize(256)]
        public string Source { get; set; }
        [FieldSize(1024)]
        public string Short { get; set; }
        [FieldSize(int.MaxValue)]
        public string Body { get; set; }
        public int Rating { get; set; }
        public bool SetTop { get; set; }
        public bool Recommended { get; set; }
        [FieldSize(256)]
        public string PictureUrl { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public Guid Guid { get; set; }

        [FieldSize(256)]
        public string FileUrl { get; set; }
        [FieldSize(256)]
        public string FileName { get; set; }
        [FieldSize(256)]
        public string FileSize { get; set; }

        [NtAttribute.FKConstraint("Article_Class", "Id")]
        public int ArticleClassId { get; set; }

        public bool Downloadable { get; set; }

        public bool Deleted { get; set; }

    }
}
