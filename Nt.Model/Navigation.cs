using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Navigation : BaseTreeEntity, ILocaleEntity
    {
        [FieldSize(100)]
        public string Path { get; set; }
        [FieldSize(20)]
        public string AnchorTarget { get; set; }
        public int LanguageId { get; set; }
        [FieldSize(512)]
        public string SeoTitle { get; set; }
        [FieldSize(1024)]
        public string SeoKeywords { get; set; }
        [FieldSize(1024)]
        public string SeoDescription { get; set; }

        /// <summary>
        /// 模块类型
        /// </summary>
        public int NaviType { get; set; }

        /// <summary>
        /// 根类别id（新闻、产品、商品、下载）
        /// </summary>
        public int RootSid { get; set; }

        /// <summary>
        /// 二级页id集合，以逗号隔开
        /// </summary>
        [FieldSize(100)]
        public string PageIds { get; set; }

    }
}
