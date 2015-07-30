using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Product : BaseLocaleEntity, IOrderable, IDisplayable, IUserTarceable, IPictureAttachable, IGuidModel, IDeletable
    {
        [FieldSize(512)]
        public string Name { get; set; }

        [FieldSize(int.MaxValue)]
        public string ShortDescription { get; set; }
        [FieldSize(int.MaxValue)]
        public string FullDescription { get; set; }
        public int Rating { get; set; }

        public bool SetTop { get; set; }
        public bool Recommended { get; set; }
        public bool IsNew { get; set; }

        [FieldSize(512)]
        public string SeoTitle { get; set; }
        [FieldSize(1024)]
        public string SeoKeywords { get; set; }
        [FieldSize(1024)]
        public string SeoDescription { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        [FKConstraint(typeof(ProductCategory), "Id")]
        public int ProductCategoryId { get; set; }
        public int UserId { get; set; }
        public string Thumb { get; set; }
        public string PictureUrl { get; set; }

        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileSize { get; set; }

        public Guid Guid { get; set; }

        //miao
        public int AreaId { get; set; }
        public int Day { get; set; }

        #region additional fields

        [FieldSize(256)]
        public string F0 { get; set; }
        [FieldSize(256)]
        public string F1 { get; set; }
        [FieldSize(256)]
        public string F2 { get; set; }
        [FieldSize(256)]
        public string F3 { get; set; }
        [FieldSize(256)]
        public string F4 { get; set; }
        [FieldSize(256)]
        public string F5 { get; set; }
        [FieldSize(256)]
        public string F6 { get; set; }
        [FieldSize(256)]
        public string F7 { get; set; }
        [FieldSize(256)]
        public string F8 { get; set; }
        [FieldSize(256)]
        public string F9 { get; set; }
        [FieldSize(256)]
        public string F10 { get; set; }
        [FieldSize(256)]
        public string F11 { get; set; }
        [FieldSize(256)]
        public string F12 { get; set; }
        [FieldSize(256)]
        public string F13 { get; set; }
        [FieldSize(256)]
        public string F14 { get; set; }
        [FieldSize(256)]
        public string F15 { get; set; }

        #endregion

        /// <summary>
        /// is downloadable
        /// </summary>
        public bool Downloadable { get; set; }

        public bool Deleted { get; set; }
    }
}
