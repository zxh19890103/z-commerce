using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Goods : BaseLocaleEntity, IUserTarceable, IOrderable, IPictureAttachable, IDisplayable, IGuidModel, IDeletable
    {
        [FKConstraint("Goods_Class", "Id")]
        public int GoodsClassId { get; set; }
        [FieldSize(512)]
        public string Name { get; set; }
        public string GoodsGuid { get; set; }
        [FieldSize(100)]
        public string Measure { get; set; }
        public decimal Price { get; set; }
        public decimal MarketPrice { get; set; }
        public decimal Cost { get; set; }
        public decimal OldPrice { get; set; }
        public bool EnableVipPrice { get; set; }
        public decimal VipPrice { get; set; }
        public bool EnableSpecialPrice { get; set; }
        public decimal SpecialPrice { get; set; }
        public DateTime SpecialPriceStartDate { get; set; }
        public DateTime SpecialPriceEndDate { get; set; }

        public int Rating { get; set; }
        public int SellNumber { get; set; }
        public bool DisableBuyButton { get; set; }
        public bool DisableWishlistButton { get; set; }
        [FieldSize(1024)]
        public string OtherClass { get; set; }
        public int Points { get; set; }
        public int StockQuantity { get; set; }

        public decimal Weight { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        [FieldSize(512)]
        public string SeoTitle { get; set; }
        [FieldSize(1024)]
        public string SeoKeywords { get; set; }
        [FieldSize(1024)]
        public string SeoDescription { get; set; }
        [FieldSize(int.MaxValue)]
        public string ShortDescription { get; set; }
        [FieldSize(int.MaxValue)]
        public string FullDescription { get; set; }

        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public bool Deleted { get; set; }
        /// <summary>
        /// 下架
        /// </summary>
        public bool Removed { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UserId { get; set; }

        public bool Recommended { get; set; }
        public bool Hot { get; set; }
        public bool SetTop { get; set; }
        public bool IsNew { get; set; }
        [FieldSize(1024)]
        public string Tags { get; set; }

        public Guid Guid { get; set; }
        [FieldSize(512)]
        public string PictureUrl { get; set; }

        public int PageNum { get; set; }

        public int DiscountId { get; set; }
        public int BrandId { get; set; }

    }
}
