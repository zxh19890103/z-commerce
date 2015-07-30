using Nt.DAL;
using Nt.Model.Setting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Nt.BLL
{
    public class WaterMarker
    {
        public const int GENERATE_GOODS_THUMBNAIL = 1;
        public const int GENERATE_GOODS_IMG_WATER = 2;
        public const int GENERATE_GOODS_TEXT_WATER = 3;
        public const int GENERATE_PRODUCT_THUMBNAIL = 4;
        public const int GENERATE_PRODUCT_IMG_WATER = 5;
        public const int GENERATE_PRODUCT_TEXT_WATER = 6;

        public const int DEL_THUMBNAIL = 7;
        public const int DEL_WATER = 8;
        public const int DEL_PRODUCT_THUMBNAIL = 9;
        public const int DEL_GOODS_THUMBNAIL = 10;
        public const int DEL_PRODUCT_WATER = 11;
        public const int DEL_GOODS_WATER = 12;

        #region singleton

        private static WaterMarker _instance = new WaterMarker();
        static readonly object padlock = new object();
        public static WaterMarker Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        #region fields

        private bool _isRunning = false;
        private int _interval = 5;//请求的时间间隔,秒
        private int _counter = 0;
        private int _collectCircle = 50;//回收周期
        private int _total = 0;

        #endregion

        #region props

        MediaService _mediaService;

        /// <summary>
        /// 计数
        /// </summary>
        public int Total { get { return _total; } }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 请求的时间间隔(秒)
        /// </summary>
        public int Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        /// <summary>
        /// 是否正在进行静态化
        /// </summary>
        public bool IsRunning
        {
            get { return _isRunning; }
        }

        /// <summary>
        /// 回收周期
        /// </summary>
        public int CollectCircle
        {
            get { return _collectCircle; }
            set { _collectCircle = value; }
        }

        int _action = 1;
        public int Action
        {
            get { return _action; }
            set
            {
                if (_isRunning)
                    return;
                _action = value;
            }
        }

        ThumbOn _currentThumbOn = ThumbOn.List;
        /// <summary>
        /// 当前缩略图生成位置
        /// </summary>
        public ThumbOn CurrentThumbOn
        {
            get { return _currentThumbOn; }
            set
            {
                if (_isRunning)
                    return;
                _currentThumbOn = value;
            }
        }

        string _languageCode;
        /// <summary>
        /// 语言
        /// </summary>
        public string LanguageCode
        {
            get
            {
                return _languageCode;
            }
            set
            {
                if (_isRunning) return;
                _languageCode = value;
            }
        }

        #endregion

        #region methods

        public void Run()
        {
            if (_isRunning)
            {
                Message = "程序正在运行，请稍后再试！";
                return;
            }

            if (string.IsNullOrEmpty(LanguageCode))
            {
                Message = "请提供语言版本符号！";
                return;
            }

            switch (Action)
            {
                case GENERATE_GOODS_THUMBNAIL:
                    new System.Threading.Thread(GenerateGoodsThumb).Start();
                    break;
                case GENERATE_GOODS_TEXT_WATER:
                    new System.Threading.Thread(GenerateGoodsWaterTextMark).Start();
                    break;
                case GENERATE_GOODS_IMG_WATER:
                    new System.Threading.Thread(GenerateGoodsWaterImgMark).Start();
                    break;
                case GENERATE_PRODUCT_THUMBNAIL:
                    new System.Threading.Thread(GenerateProductThumb).Start();
                    break;
                case GENERATE_PRODUCT_TEXT_WATER:
                    new System.Threading.Thread(GenerateProductWaterTextMark).Start();
                    break;
                case GENERATE_PRODUCT_IMG_WATER:
                    new System.Threading.Thread(GenerateProductWaterImgMark).Start();
                    break;
                case DEL_THUMBNAIL:
                    new System.Threading.Thread(DelAllThumbnail).Start();
                    break;
                case DEL_WATER:
                    new System.Threading.Thread(DelAllWaterMark).Start();
                    break;
                case DEL_PRODUCT_THUMBNAIL:
                    new System.Threading.Thread(DelProductThumbnail).Start();
                    break;
                case DEL_PRODUCT_WATER:
                    new System.Threading.Thread(DelProductWater).Start();
                    break;
                case DEL_GOODS_THUMBNAIL:
                    new System.Threading.Thread(DelGoodsThumbnail).Start();
                    break;
                case DEL_GOODS_WATER:
                    new System.Threading.Thread(DelGoodsWater).Start();
                    break;
                default:
                    Message = "无效操作!";
                    break;
            }
        }

        void Initialize()
        {
            Message = "空闲";
            _isRunning = true;
            _counter = 0;
            _total = 0;
            _mediaService = new MediaService();
        }

        void End()
        {
            _counter = 0;
            _isRunning = false;
            _mediaService = null;
        }

        /// <summary>
        /// 生成产品缩略图
        /// </summary>
        public void GenerateProductThumb()
        {
            Initialize();

            var setting = SettingService.GetSettingModel<ProductSetting>(LanguageCode);

            bool enable = false;
            string text = string.Empty,
                table = string.Empty,
                field = string.Empty;
            int width = 200, height = 160;
            ThumbnailGenerationMode mode = ThumbnailGenerationMode.CUT;

            switch (CurrentThumbOn)
            {
                case ThumbOn.Home:
                    enable = setting.EnableThumbOnHome;
                    width = setting.ThumbWidthOnHome;
                    height = setting.ThumbHeightOnHome;
                    mode = (ThumbnailGenerationMode)setting.ThumbModeOnHome;
                    table = "Product";
                    field = "PictureUrl";
                    text = "首页产品";
                    break;
                case ThumbOn.List:
                    enable = setting.EnableThumbOnList;
                    width = setting.ThumbWidthOnList;
                    height = setting.ThumbHeightOnList;
                    mode = (ThumbnailGenerationMode)setting.ThumbModeOnList;
                    table = "Product";
                    field = "PictureUrl";
                    text = "列表页产品";
                    break;
                case ThumbOn.Detail:
                    enable = setting.EnableThumbOnDetail;
                    width = setting.ThumbWidthOnDetail;
                    height = setting.ThumbHeightOnDetail;
                    mode = (ThumbnailGenerationMode)setting.ThumbModeOnDetail;
                    table = "View_ProductPicture";
                    field = "Src";
                    text = "详细页产品";
                    break;
                default:
                    break;
            }

            GenerateThumbBase(enable, width, height, mode, text, table, field);

        }

        /// <summary>
        /// 生成商品缩略图
        /// </summary>
        /// <param name="thumbOn"></param>
        public void GenerateGoodsThumb()
        {
            Initialize();

            var setting = SettingService.GetSettingModel<GoodsSetting>(LanguageCode);

            bool enable = false;
            string text = string.Empty,
                table = string.Empty,
                field = string.Empty;
            int width = 200, height = 160;
            ThumbnailGenerationMode mode = ThumbnailGenerationMode.CUT;

            switch (CurrentThumbOn)
            {
                case ThumbOn.Home:
                    enable = setting.EnableThumbOnHome;
                    width = setting.ThumbWidthOnHome;
                    height = setting.ThumbHeightOnHome;
                    mode = (ThumbnailGenerationMode)setting.ThumbModeOnHome;
                    table = "Goods";
                    field = "PictureUrl";
                    text = "首页商品";
                    break;
                case ThumbOn.List:
                    enable = setting.EnableThumbOnList;
                    width = setting.ThumbWidthOnList;
                    height = setting.ThumbHeightOnList;
                    mode = (ThumbnailGenerationMode)setting.ThumbModeOnList;
                    table = "Goods";
                    field = "PictureUrl";
                    text = "列表页商品";
                    break;
                case ThumbOn.Detail:
                    enable = setting.EnableThumbOnDetail;
                    width = setting.ThumbWidthOnDetail;
                    height = setting.ThumbHeightOnDetail;
                    mode = (ThumbnailGenerationMode)setting.ThumbModeOnDetail;
                    table = "View_GoodsPicture";
                    field = "Src";
                    text = "详细页商品";
                    break;
                default:
                    break;
            }

            GenerateThumbBase(enable, width, height, mode, text, table, field);
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="enable">是否开启此功能</param>
        /// <param name="width">缩略图宽</param>
        /// <param name="height">缩略图高</param>
        /// <param name="mode">缩略图模式</param>
        /// <param name="text">文本</param>
        /// <param name="table">表名</param>
        /// <param name="field">表示图片路劲的字段名</param>
        private void GenerateThumbBase(bool enable, int width, int height,
            ThumbnailGenerationMode mode, string text, string table, string field)
        {
            if (!enable)
            {
                Message = string.Format("没有开启生成{0}缩略图功能!", text);
                End();
                return;
            }

            Queue<string> que = new Queue<string>();

            using (SqlDataReader rs = SqlHelper.ExecuteReader(
                SqlHelper.GetConnection(), CommandType.Text,
                string.Format("Select [{1}] from [{0}]",
                DbAccessor.GetModifiedTableName(table), field)
                ))
            {
                while (rs.Read())
                {
                    que.Enqueue(rs[0].ToString());
                }
            }

            foreach (var item in que)
            {
                string generatedUrl = string.Empty;

                generatedUrl = _mediaService.MakeThumbnail(
                    item,
                    width,
                    height,
                    mode);

                _counter++;
                _total++;
                Message = string.Format("生成{1}缩略图:{0}", generatedUrl, text);

                if (_counter >= _collectCircle)
                {
                    System.GC.Collect();
                    _counter = 0;
                }
            }
            Message = string.Format(
                "缩略图生成完毕，一共生成{0}张图片!", _total);
            End();
        }

        /// <summary>
        /// 产品图文字水印
        /// </summary>
        public void GenerateProductWaterTextMark()
        {
            Initialize();
            var setting = SettingService.GetSettingModel<ProductSetting>(LanguageCode);

            #region 验证

            if (!setting.EnableTextMark)
            {
                Message = "没有开启产品文字水印功能!";
                End();
                return;
            }

            string markText;

            markText = setting.TextMark;
            if (markText == string.Empty)
            {
                Message = "没有提供水印文字!";
                End();
                return;
            }

            #endregion

            Queue<string> que = new Queue<string>();

            using (SqlDataReader rs = SqlHelper.ExecuteReader(
                SqlHelper.GetConnection(), CommandType.Text, "Select Src From View_ProductPicture"
                ))
            {
                while (rs.Read())
                {
                    que.Enqueue(rs[0].ToString());
                }
            }

            foreach (var item in que)
            {
                string generatedUrl = string.Empty;
                if (!File.Exists(WebHelper.MapPath(item)))
                    continue;
                generatedUrl = _mediaService.MakeTextWater(
                    item, markText, (WaterMarkPositionOption)setting.TextMarkPosition,
                     setting.ImgMarkAlpha, setting.HeightOfTextBox, setting.WidthOfTextBox,
                     setting.FontFamily);

                _counter++;
                _total++;
                Message = string.Format("生成文字水印图：{0}", generatedUrl);

                if (_counter >= _collectCircle)
                {
                    _counter = 0;
                    System.GC.Collect();
                }
            }
            End();
        }

        /// <summary>
        /// 生成产品水印
        /// </summary>
        public void GenerateProductWaterImgMark()
        {
            Initialize();
            var setting = SettingService.GetSettingModel<ProductSetting>(LanguageCode);

            #region 验证

            if (!setting.EnableImgMark)
            {
                Message = "没有开启产品图片水印功能!";
                End();
                return;
            }

            string markImg;

            markImg = setting.MarkImgUrl;

            if (!File.Exists(WebHelper.MapPath(markImg)))
            {
                Message = "没有提供水印图片!";
                End();
                return;
            }

            #endregion

            Queue<string> que = new Queue<string>();

            using (SqlDataReader rs = SqlHelper.ExecuteReader(
                SqlHelper.GetConnection(), CommandType.Text, "Select Src From View_ProductPicture"
                ))
            {
                while (rs.Read())
                {
                    que.Enqueue(rs[0].ToString());
                }
            }

            foreach (var item in que)
            {
                string generatedUrl = string.Empty;

                if (!File.Exists(WebHelper.MapPath(item)))
                    continue;

                generatedUrl = _mediaService.MakeImgWater(
                    item, markImg, string.Empty, (WaterMarkPositionOption)setting.TextMarkPosition,
                     setting.ImgMarkAlpha);

                _counter++;
                _total++;
                Message = string.Format("生成图片水印图{0}", generatedUrl);

                if (_counter >= _collectCircle)
                {
                    _counter = 0;
                    System.GC.Collect();
                }
            }
            End();
        }

        /// <summary>
        /// 商品文字水印
        /// </summary>
        public void GenerateGoodsWaterTextMark()
        {
            Initialize();
            var setting = SettingService.GetSettingModel<GoodsSetting>(LanguageCode);

            #region 验证

            if (!setting.EnableTextMark)
            {
                Message = "没有开启商品文字水印功能!";
                End();
                return;
            }

            string markText;

            markText = setting.TextMark;
            if (markText == string.Empty)
            {
                Message = "没有提供水印文字!";
                End();
                return;
            }

            #endregion

            Queue<string> que = new Queue<string>();

            using (SqlDataReader rs = SqlHelper.ExecuteReader(
                SqlHelper.GetConnection(), CommandType.Text, "Select Src From View_GoodsPicture"
                ))
            {
                while (rs.Read())
                {
                    que.Enqueue(rs[0].ToString());
                }
            }

            foreach (var item in que)
            {
                string generatedUrl = string.Empty;
                if (!File.Exists(WebHelper.MapPath(item)))
                    continue;
                generatedUrl = _mediaService.MakeTextWater(
                    item, markText, (WaterMarkPositionOption)setting.TextMarkPosition,
                     setting.ImgMarkAlpha, setting.HeightOfTextBox, setting.WidthOfTextBox,
                     setting.FontFamily);
                _counter++;
                _total++;
                Message = string.Format("生成文字水印图：{0}", generatedUrl);

                if (_counter >= _collectCircle)
                {
                    _counter = 0;
                    System.GC.Collect();
                }
            }
            End();
        }

        /// <summary>
        /// 商品图片水印
        /// </summary>
        public void GenerateGoodsWaterImgMark()
        {
            Initialize();
            var setting = SettingService.GetSettingModel<GoodsSetting>(LanguageCode);

            #region 验证

            if (!setting.EnableImgMark)
            {
                Message = "没有开启商品图片水印功能!";
                End();
                return;
            }

            string markImg;

            markImg = setting.MarkImgUrl;
            if (!File.Exists(WebHelper.MapPath(markImg)))
            {
                Message = "没有提供商品水印图片!";
                End();
                return;
            }

            #endregion

            Queue<string> que = new Queue<string>();

            using (SqlDataReader rs = SqlHelper.ExecuteReader(
                SqlHelper.GetConnection(), CommandType.Text, "Select Src From View_GoodsPicture"
                ))
            {
                while (rs.Read())
                {
                    que.Enqueue(rs[0].ToString());
                }
            }

            foreach (var item in que)
            {
                string generatedUrl = string.Empty;

                if (!File.Exists(WebHelper.MapPath(item)))
                    continue;

                generatedUrl = _mediaService.MakeImgWater(
                    item, markImg, string.Empty, (WaterMarkPositionOption)setting.TextMarkPosition,
                     setting.ImgMarkAlpha);

                _counter++;
                _total++;
                Message = string.Format("生成商品图片水印图{0}", generatedUrl);

                if (_counter >= _collectCircle)
                {
                    _counter = 0;
                    System.GC.Collect();
                }
            }

            End();
        }

        /// <summary>
        ///删除生成图片
        /// </summary>
        /// <param name="topDir">根目录</param>
        /// <param name="searchPatternPart1">生成文件名后缀</param>
        /// <param name="so">SearchOption</param>
        /// <param name="text">消息关键词</param>
        void DelBase(string topDir, string[] searchPatternPart1, SearchOption so, string text)
        {
            if (searchPatternPart1 == null || searchPatternPart1.Length == 0)
                return;
            _isRunning = true;
            _total = 0;
            string dirPathOnDisk = WebHelper.MapPath(topDir);
            string[] searchPatternPart2 = MediaService.ALLOWED_PIC_FORMAT.Split('|');
            string searchPattern = string.Empty;
            foreach (string part1 in searchPatternPart1)
            {
                foreach (string part2 in searchPatternPart2)
                {
                    searchPattern = string.Format("*_*{0}{1}", part1, part2);
                    foreach (string f in
                    Directory.GetFiles(dirPathOnDisk,
                    searchPattern,
                    so))
                    {
                        File.Delete(f);
                        _total++;
                    }
                }
            }
            Message = string.Format("成功删除{0}个{1}文件!", _total, text);
            _isRunning = false;
        }

        /// <summary>
        /// 删除所有缩略图
        /// </summary>
        public void DelAllThumbnail()
        {
            DelBase("/upload/", new string[] { "H", "W", "HW", "CUT", "CUTA" }, SearchOption.AllDirectories, "缩略图");
        }

        /// <summary>
        /// 删除水印图
        /// </summary>
        public void DelAllWaterMark()
        {
            DelBase("/upload/", new string[] { "mi", "mt" }, SearchOption.AllDirectories, "水印图");
        }


        public void DelProductThumbnail()
        {
            DelBase("/upload/product/", new string[] { "H", "W", "HW", "CUT", "CUTA" }, SearchOption.TopDirectoryOnly, "产品缩略图");
        }

        public void DelProductWater()
        {
            DelBase("/upload/product/", new string[] { "mi", "mt" }, SearchOption.TopDirectoryOnly, "产品水印图");
        }

        public void DelGoodsThumbnail()
        {
            DelBase("/upload/goods/", new string[] { "H", "W", "HW", "CUT", "CUTA" }, SearchOption.TopDirectoryOnly, "商品缩略图");
        }

        public void DelGoodsWater()
        {
            DelBase("/upload/goods/", new string[] { "mi", "mt" }, SearchOption.TopDirectoryOnly, "商品水印图");
        }

        #endregion
    }

    public enum ThumbOn : int
    {
        /// <summary>
        /// 首页缩略图
        /// </summary>
        Home = 10,
        /// <summary>
        /// 列表页缩略图
        /// </summary>
        List = 20,
        /// <summary>
        /// 详细页缩略图
        /// </summary>
        Detail = 30,
    }

}
