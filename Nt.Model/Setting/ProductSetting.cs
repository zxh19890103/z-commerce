using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.Setting
{
    public class ProductSetting : BaseSetting
    {
        public int PageSize { get; set; }
        public int PagerItemCount { get; set; }

        /// <summary>
        /// 开启列表页缩略图
        /// </summary>
        public bool EnableThumbOnList { get; set; }
        /// <summary>
        /// 列表页缩略图宽
        /// </summary>
        public int ThumbWidthOnList { get; set; }
        /// <summary>
        /// 列表页缩略图高
        /// </summary>
        public int ThumbHeightOnList { get; set; }

        /// <summary>
        /// 列表页缩略图处理模式
        /// </summary>
        public int ThumbModeOnList { get; set; }

        /// <summary>
        /// 开启首页缩略图
        /// </summary>
        public bool EnableThumbOnHome { get; set; }
        /// <summary>
        /// 首页缩略图宽
        /// </summary>
        public int ThumbWidthOnHome { get; set; }
        /// <summary>
        /// 首页缩略图高
        /// </summary>
        public int ThumbHeightOnHome { get; set; }
        /// <summary>
        /// 首页缩略图处理模式
        /// </summary>
        public int ThumbModeOnHome { get; set; }

        /// <summary>
        /// 开启产品详细页缩略图
        /// </summary>
        public bool EnableThumbOnDetail { get; set; }
        /// <summary>
        /// 首页缩略图宽
        /// </summary>
        public int ThumbWidthOnDetail { get; set; }
        /// <summary>
        /// 首页缩略图高
        /// </summary>
        public int ThumbHeightOnDetail { get; set; }
        /// <summary>
        /// 首页缩略图处理模式
        /// </summary>
        public int ThumbModeOnDetail { get; set; }

        /*文字水印*/
        public bool EnableTextMark { get; set; }
        public string TextMark { get; set; }
        public int TextMarkPosition { get; set; }
        public string FontFamily { get; set; }
        public int WidthOfTextBox { get; set; }
        public int HeightOfTextBox { get; set; }

        /*图片水印*/
        public bool EnableImgMark { get; set; }
        public float ImgMarkAlpha { get; set; }
        public int ImgMarkPosition { get; set; }
        public string MarkImgUrl { get; set; }
    }
}
