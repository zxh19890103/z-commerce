using Nt.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Nt.BLL
{
    public class MediaService
    {
        #region Const
        /// <summary>
        /// /upload/
        /// </summary>
        public const string UPLOAD_FILE_DIR = "/upload/";
        /// <summary>
        /// /netin/content/images/no-image.gif
        /// </summary>
        public const string NO_IMAGE = "/netin/content/images/no-image.gif";
        /// <summary>
        /// .jpg|.bmp|.png|.gif|.jpeg
        /// </summary>
        public const string ALLOWED_PIC_FORMAT = ".jpg|.bmp|.png|.gif|.jpeg";
        /// <summary>
        /// 1048576=1M
        /// </summary>
        public const int MAX_LENGTH_OF_UPLOAD = 1048576; //1M
        /// <summary>
        /// .doc|.docx|.xls|.xlsx|.ppt|.htm|.html|.txt|.zip|.rar|.gz|.bz2
        /// </summary>
        public const string ALLOWED_File_FORMAT = ".doc|.docx|.xls|.xlsx|.ppt|.htm|.html|.txt|.zip|.rar|.gz|.bz2";
        #endregion

        #region Fields

        string fileUrl = string.Empty;
        long fileSize = 0;
        string fileName = string.Empty;
        public bool ThumbnailMaking = false;//当前是否正在进行生成图片的缩略图
        public bool WaterMaking = false;//是否正在进行生成水印

        ImageUtility imageUtility;

        #endregion

        #region props

        /// <summary>
        /// 上传文件的网络路径
        /// </summary>
        public string FileUrl { get { return string.IsNullOrEmpty(fileUrl) ? NO_IMAGE : fileUrl; } }
        /// <summary>
        /// 上传文件的大小，以字节为单位
        /// </summary>
        public long FileSize { get { return fileSize; } }
        public string FileName { get { return fileName; } }

        private int _maxLength = MAX_LENGTH_OF_UPLOAD;
        bool _maxLengthChanged = false;
        /// <summary>
        /// 上传的图片的上限  1M
        /// </summary>
        public int MaxLength
        {
            get
            {
                if (_maxLengthChanged)
                    return _maxLength;
                return _maxLength;
            }
            set
            {
                if (value != MAX_LENGTH_OF_UPLOAD)
                {
                    _maxLength = value;
                    _maxLengthChanged = true;
                }
            }
        }

        string _uploadFileRootDir = UPLOAD_FILE_DIR;
        bool _uploadFileRootDirChanged = false;
        /// <summary>
        /// 上传根目录
        /// </summary>
        public string UploadFileRootDir
        {
            get
            {
                if (_uploadFileRootDirChanged)
                    return _uploadFileRootDir;
                return _uploadFileRootDir;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) &&
                    !string.Equals(_uploadFileRootDir, value, StringComparison.InvariantCultureIgnoreCase))
                {
                    _uploadFileRootDir = value;
                    _uploadFileRootDirChanged = true;
                    string x = WebHelper.MapPath(_uploadFileRootDir);
                    if (!Directory.Exists(x))
                        Directory.CreateDirectory(x);
                }
            }
        }

        string _allowedUploadFormats = ALLOWED_PIC_FORMAT;
        bool _allowedUploadFormatsChanged = false;
        /// <summary>
        /// 允许的上传格式
        /// </summary>
        public string AllowedUploadFormats
        {
            get
            {
                if (_allowedUploadFormatsChanged)
                    return _allowedUploadFormats;
                return _allowedUploadFormats;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) &&
                    !string.Equals(ALLOWED_PIC_FORMAT, value, StringComparison.InvariantCultureIgnoreCase))
                {
                    _allowedUploadFormats = value;
                    _allowedUploadFormatsChanged = false;
                }
            }
        }

        string _currentUploadCategory;
        /// <summary>
        /// 当前上传文件的分类名称
        /// </summary>
        public string CurrentUploadCategory
        {
            get
            {
                if (string.IsNullOrEmpty(_currentUploadCategory))
                    _currentUploadCategory = DateTime.Now.ToString("yyyyMM");
                return _currentUploadCategory;
            }
            set
            {
                _currentUploadCategory = value;
                _currentUploadCategory.Trim('/');
            }
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public MediaService()
        {
            imageUtility = new ImageUtility();
        }

        /// <summary>
        /// 上传图片或其他
        /// </summary>
        /// <returns></returns>
        public void AsyncUpload()
        {
            var request = WebHelper.Request;

            //删除旧文件
            //string oldFile = request["oldfile"];
            //if (!string.IsNullOrEmpty(oldFile))
            //    TryDeleteImages(oldFile);

            HttpPostedFile httpPostedFile = request.Files["file"];
            if (httpPostedFile == null)
                throw new ArgumentException("没有载入的文件");
            if (httpPostedFile.ContentLength > MaxLength)
                throw new Exception("文件大小不能超过" + MaxLength / 1024 + "K");
            string fileExtension = "";
            if (!CheckFileFormat(httpPostedFile, out fileExtension))
                throw new Exception(string.Format(
                    "不允许上传格式除{0}以外的文件", AllowedUploadFormats));
            string newFileName = GetNewFileName(fileExtension);
            string currentDir = UploadFileRootDir + CurrentUploadCategory + "/";
            string dirpath = WebHelper.MapPath(currentDir);
            if (!Directory.Exists(dirpath))
                Directory.CreateDirectory(dirpath);
            string filePath = dirpath + newFileName;
            httpPostedFile.SaveAs(filePath);
            fileSize = httpPostedFile.ContentLength;
            fileName = httpPostedFile.FileName;
            fileUrl = currentDir + newFileName;
        }

        #region MakeThumbnail
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">原始图路径</param>
        /// <param name="height">高度</param>
        /// <param name="width">宽度</param>
        /// <param name="mode">裁剪模式</param>
        /// <returns></returns>
        public string MakeThumbnail(string originalImagePath, int height, int width, ThumbnailGenerationMode mode)
        {
            if (Regex.IsMatch(originalImagePath, @"^http[s]?://"))//网络图片,直接返回图片路径
                return originalImagePath;

            //非/upload目录里的文件，不生成缩略图
            if (!Regex.IsMatch(originalImagePath, @"^\/upload\/[\w\/]+\.\w+$", RegexOptions.IgnoreCase))
                return originalImagePath;

            ThumbnailMaking = true;
            string thumbnail = GetThumbnailUrl(originalImagePath, height, width, mode);//如果返回的为no-img.gif，则不生成缩略图
            imageUtility.TargetImagePathOnDisk = WebHelper.MapPath(thumbnail);
            if (File.Exists(imageUtility.TargetImagePathOnDisk))//如果早已存在则直接返回
                return thumbnail;

            imageUtility.OriginalImagePathOnDisk = WebHelper.MapPath(originalImagePath);
            if (!File.Exists(imageUtility.OriginalImagePathOnDisk))//原始文件不存在了
                return NO_IMAGE;

            imageUtility.TGM = mode;
            imageUtility.ThumbnailImageHeight = height;
            imageUtility.ThumbnailImageWidth = width;
            imageUtility.WriteThumbnail();
            ThumbnailMaking = false;
            return thumbnail;
        }

        /// <summary>
        /// mode=ThumbnailGenerationMode.HW
        /// </summary>
        /// <param name="originalImagePath">原始图路径</param>
        /// <param name="height">高度</param>
        /// <param name="width">宽度</param>
        /// <returns></returns>
        public string MakeThumbnail(string originalImagePath, int height, int width)
        {
            return MakeThumbnail(originalImagePath, height, width, ThumbnailGenerationMode.HW);
        }

        /// <summary>
        /// 按宽度生成缩略图
        /// </summary>
        /// <param name="originalImagePath">原始图路径</param>
        /// <param name="width">宽度</param>
        /// <returns></returns>
        public string MakeThumbnail(string originalImagePath, int width)
        {
            return MakeThumbnail(originalImagePath, 0, width, ThumbnailGenerationMode.W);
        }

        /// <summary>
        /// 按高度生成缩略图
        /// </summary>
        /// <param name="height">高度</param>
        /// <param name="originalImagePath">原始图路径</param>
        /// <returns></returns>
        public string MakeThumbnail(int height, string originalImagePath)
        {
            return MakeThumbnail(originalImagePath, height, 0, ThumbnailGenerationMode.H);
        }

        #endregion

        #region MakeImgWater
        /// <summary>
        /// 制作图片水印（_mi）
        /// </summary>
        /// <param name="originalImagePath">原始图片的绝对路径</param>
        /// <param name="waterImagePath">水印图的绝对路径</param>
        /// <param name="pos">水印图位置</param>
        /// <param name="alpha">水印图不透明度</param>
        /// <returns></returns>
        public string MakeImgWater(string originalImagePath, string waterImagePath, string text, WaterMarkPositionOption pos, float alpha)
        {
            WaterMaking = true;
            string url = GetWaterMakerUrl(originalImagePath, "mi");
            imageUtility.TargetImagePathOnDisk = WebHelper.MapPath(url);
            if (File.Exists(imageUtility.TargetImagePathOnDisk))//如果早已存在则直接返回
                return url;

            imageUtility.OriginalImagePathOnDisk = WebHelper.MapPath(originalImagePath);
            if (!File.Exists(imageUtility.OriginalImagePathOnDisk))//原始文件不存在了
                return NO_IMAGE;

            imageUtility.WaterImagePathOnDisk = WebHelper.MapPath(waterImagePath);
            if (!File.Exists(imageUtility.OriginalImagePathOnDisk))//水印文件不存在了
                return originalImagePath;

            imageUtility.WaterText = text;
            imageUtility.WMPO = pos;
            imageUtility.Alpha4WaterImage = alpha;
            imageUtility.AddImageWaterMark();
            WaterMaking = false;
            return url;
        }

        public string MakeImgWater(string originalImagePath, string waterImagePath, string text, WaterMarkPositionOption pos)
        {
            return MakeImgWater(originalImagePath, waterImagePath, text, pos, 0.8F);
        }

        public string MakeImgWater(string originalImagePath, string waterImagePath, WaterMarkPositionOption pos)
        {
            return MakeImgWater(originalImagePath, waterImagePath, string.Empty, pos, 0.8F);
        }

        public string MakeImgWater(string originalImagePath, string waterImagePath, string text)
        {
            return MakeImgWater(originalImagePath, waterImagePath, text, WaterMarkPositionOption.Center, 0.8F);
        }

        public string MakeImgWater(string originalImagePath, string waterImagePath)
        {
            return MakeImgWater(originalImagePath, waterImagePath, string.Empty, WaterMarkPositionOption.Center, 0.8F);
        }

        #endregion

        #region MakeTextWater

        /// <summary>
        /// 制作文字水印（_mt）
        /// </summary>
        /// <param name="originalImagePath">原始图片的绝对路径</param>
        /// <param name="text">文字内容</param>
        /// <param name="pos">位置</param>
        /// <param name="alpha">不透明度</param>
        /// <param name="wth">文本框的高度</param>
        /// <param name="wtw">文本框的宽度</param>
        /// <param name="ff">字体</param>
        /// <returns></returns>
        public string MakeTextWater(string originalImagePath, string text, WaterMarkPositionOption pos, float alpha, int wth, int wtw, string ff)
        {
            WaterMaking = true;
            string url = GetWaterMakerUrl(originalImagePath, "mt");
            imageUtility.TargetImagePathOnDisk = WebHelper.MapPath(url);
            if (File.Exists(imageUtility.TargetImagePathOnDisk))//如果早已存在则直接返回
                return url;

            imageUtility.OriginalImagePathOnDisk = WebHelper.MapPath(originalImagePath);
            if (!File.Exists(imageUtility.OriginalImagePathOnDisk))//原始文件不存在了
                return NO_IMAGE;

            if (string.IsNullOrEmpty(text))//文字内容为空
                return originalImagePath;

            imageUtility.WMPO = pos;
            imageUtility.Alpha4WaterImage = alpha;
            imageUtility.WaterText = text;
            imageUtility.WaterTextHeight = wth;
            imageUtility.WaterTextWidth = wtw;
            imageUtility.FontFamliy = ff;
            imageUtility.AddTextWaterMark();
            WaterMaking = false;
            return url;
        }

        /// <summary>
        /// 制作文字水印（_mt）
        /// </summary>
        /// <param name="originalImagePath">原始图片的绝对路径</param>
        /// <param name="text">文字内容</param>
        /// <param name="pos">位置</param>
        /// <param name="alpha">不透明度</param>
        /// <param name="wth">文本框的高度</param>
        /// <param name="wtw">文本框的宽度</param>
        /// <param name="ff">宋体</param>
        /// <returns></returns>
        public string MakeTextWater(string originalImagePath, string text, WaterMarkPositionOption pos, float alpha, int wth, int wtw)
        {
            return MakeTextWater(originalImagePath, text, pos, alpha, wth, wtw, "宋体");
        }

        /// <summary>
        /// 制作文字水印（_mt）
        /// </summary>
        /// <param name="originalImagePath">原始图片的绝对路径</param>
        /// <param name="text">文字内容</param>
        /// <param name="pos">位置</param>
        /// <param name="alpha">不透明度</param>
        /// <param name="wtw">文本框的宽度</param>
        /// <param name="wth">文本框的宽度*0.25</param>
        /// <param name="ff">宋体</param>
        /// <returns></returns>
        public string MakeTextWater(string originalImagePath, string text, WaterMarkPositionOption pos, float alpha, int wtw)
        {
            return MakeTextWater(originalImagePath, text, pos, alpha, (int)(wtw * 0.25), wtw, "宋体");
        }

        /// <summary>
        /// 制作文字水印（_mt）
        /// </summary>
        /// <param name="originalImagePath">原始图片的绝对路径</param>
        /// <param name="text">文字内容</param>
        /// <param name="pos">位置</param>
        /// <param name="alpha">不透明度</param>
        /// <param name="wtw">文本框的宽度400</param>
        /// <param name="wth">文本框的高度100</param>
        /// <param name="ff">宋体</param>
        /// <returns></returns>
        public string MakeTextWater(string originalImagePath, string text, WaterMarkPositionOption pos, float alpha)
        {
            return MakeTextWater(originalImagePath, text, pos, alpha, 100, 400, "宋体");
        }

        /// <summary>
        /// 制作文字水印（_mt）
        /// </summary>
        /// <param name="originalImagePath">原始图片的绝对路径</param>
        /// <param name="text">文字内容</param>
        /// <param name="pos">位置</param>
        /// <param name="alpha">不透明度1</param>
        /// <param name="wtw">文本框的宽度400</param>
        /// <param name="wth">文本框的高度100</param>
        /// <param name="ff">宋体</param>
        /// <returns></returns>
        public string MakeTextWater(string originalImagePath, string text, WaterMarkPositionOption pos)
        {
            return MakeTextWater(originalImagePath, text, pos, 1.0F, 100, 400, "宋体");
        }

        /// <summary>
        /// 制作文字水印（_mt）
        /// </summary>
        /// <param name="originalImagePath">原始图片的绝对路径</param>
        /// <param name="text">文字内容</param>
        /// <param name="pos">位置  正中间</param>
        /// <param name="alpha">不透明度1</param>
        /// <param name="wtw">文本框的宽度400</param>
        /// <param name="wth">文本框的高度100</param>
        /// <param name="ff">宋体</param>
        /// <returns></returns>
        public string MakeTextWater(string originalImagePath, string text)
        {
            return MakeTextWater(originalImagePath, text, WaterMarkPositionOption.Center, 1.0F, 100, 400, "宋体");
        }

        #endregion

        #region utility

        /// <summary>
        /// 检查文件格式
        /// </summary>
        /// <param name="fileName">文件的名称</param>
        /// <returns></returns>
        public bool CheckFileFormat(HttpPostedFile file, out string extension)
        {
            string fileName = Path.GetFileName(file.FileName);
            extension = Path.GetExtension(fileName).ToLowerInvariant();//with a dot
            return AllowedUploadFormats.Split('|')
                .Contains(extension);
        }

        /// <summary>
        /// 获取新的文件名
        /// </summary>
        /// <param name="extension">文件后缀</param>
        /// <returns></returns>
        public string GetNewFileName(string extension)
        {
            if (!extension.StartsWith("."))
                extension = "." + extension;
            return string.Format("{0}{1}",
                DateTime.Now.ToString("yyyyMMddhhmmssffff"), extension);
        }

        /// <summary>
        /// 获取图片url
        /// </summary>
        /// <param name="originalImagePath"></param>
        /// <returns></returns>
        public string GetPictureUrl(string originalImagePath)
        {
            if (Regex.IsMatch(originalImagePath, @"^http[s]?://"))
                return originalImagePath;
            string path = WebHelper.MapPath(originalImagePath);
            if (File.Exists(path))
                return originalImagePath;
            return NO_IMAGE;
        }

        /// <summary>
        /// 获取原始图所对应的缩略图的url
        /// </summary>
        /// <param name="originalImagePath">原始图路径</param>
        /// <param name="height">高</param>
        /// <param name="width">宽</param>
        /// <param name="mode">裁剪模式</param>
        /// <returns></returns>
        public string GetThumbnailUrl(string originalImagePath, int height, int width, ThumbnailGenerationMode mode)
        {
            //如果不在制作缩略图过程当中
            if (!ThumbnailMaking)
            {
                //非/upload目录里的文件直接返回原路径
                if (!Regex.IsMatch(originalImagePath, @"^\/upload\/[\w\/]+\.\w+$", RegexOptions.IgnoreCase))
                    return originalImagePath;
            }
            int pos = originalImagePath.LastIndexOf('.');
            string fileExtention = originalImagePath.Substring(pos);
            string pathWithoutExt = originalImagePath.Substring(0, pos);
            string thumbnailPath = string.Empty;
            switch (mode)
            {
                case ThumbnailGenerationMode.H:
                    thumbnailPath = string.Format("{0}_{1}{3}{2}", pathWithoutExt, height, fileExtention, mode);
                    break;
                case ThumbnailGenerationMode.W:
                    thumbnailPath = string.Format("{0}_{1}{3}{2}", pathWithoutExt, width, fileExtention, mode);
                    break;
                case ThumbnailGenerationMode.HW:
                case ThumbnailGenerationMode.CUT:
                case ThumbnailGenerationMode.CUTA:
                    thumbnailPath = string.Format("{0}_{1}x{3}{4}{2}",
                        pathWithoutExt, height, fileExtention, width, mode);
                    break;
                default:
                    throw new Exception(mode + "不是合法的裁剪模式.");
            }

            //如果通过调用此方法获得的缩略图的url不是用于制作缩略图的目标url，
            //则判断该url对应的文件是否存在于磁盘接返回thumbnailPath（无需生成）
            if (!ThumbnailMaking && !File.Exists(WebHelper.MapPath(thumbnailPath)))
                return NO_IMAGE;
            return thumbnailPath;
        }

        public string GetThumbnailUrl(string originalImagePath, int width)
        {
            return GetThumbnailUrl(originalImagePath, 0, width, ThumbnailGenerationMode.W);
        }

        public string GetThumbnailUrl(int height, string originalImagePath)
        {
            return GetThumbnailUrl(originalImagePath, height, 0, ThumbnailGenerationMode.H);
        }

        public string GetThumbnailUrl(string originalImagePath, int height, int width)
        {
            return GetThumbnailUrl(originalImagePath, height, width, ThumbnailGenerationMode.HW);
        }

        /// <summary>
        /// 获取原始图所对应的(图片或文字)水印图的url
        /// </summary>
        /// <param name="originalImagePath">原始图路径</param>
        /// <returns></returns>
        public string GetWaterMakerUrl(string originalImagePath, string extension)
        {
            if (!Regex.IsMatch(originalImagePath, @"^\/upload\/[\w\/]+\.\w+$", RegexOptions.IgnoreCase))
                return NO_IMAGE;
            int pos = originalImagePath.LastIndexOf('.');
            string fileExtention = originalImagePath.Substring(pos);
            string pathWithoutExt = originalImagePath.Substring(0, pos);
            string thumbnailPath = string.Empty;
            thumbnailPath = string.Format("{0}_{2}{1}", pathWithoutExt, fileExtention, extension);
            if (!WaterMaking && !File.Exists(WebHelper.MapPath(thumbnailPath)))
                return NO_IMAGE;
            return thumbnailPath;
        }

        #endregion

        #region delete

        /// <summary>
        /// 删除某个文件
        /// </summary>
        /// <param name="absoluteFilePath">文件路径</param>
        public void TryDeleteFile(string absoluteFilePath)
        {
            if (!absoluteFilePath.StartsWith("/upload/", StringComparison.OrdinalIgnoreCase))
                return;
            string filepath = WebHelper.MapPath(absoluteFilePath);
            if (File.Exists(filepath))
                FileSystemUtility.DeleteFile(absoluteFilePath);
        }

        /// <summary>
        /// 删除包括缩略图在内的所有图片
        /// </summary>
        /// <param name="absoluteFilePath">图片路径</param>
        public void TryDeleteImages(string absoluteFilePath)
        {
            if (!absoluteFilePath.StartsWith("/upload/", StringComparison.OrdinalIgnoreCase))
                return;
            string path = WebHelper.MapPath(absoluteFilePath);
            if (File.Exists(path))
                File.Delete(path);
            int pos = absoluteFilePath.LastIndexOf('/');
            int pos1 = absoluteFilePath.LastIndexOf('.');
            var pictureUrlWithoutExtention = absoluteFilePath.Substring(pos + 1, pos1 - pos - 1);
            string ext = absoluteFilePath.Substring(pos1 + 1);
            var searchPattern = string.Format("{0}_*.{1}", pictureUrlWithoutExtention, ext);
            var dirPath = WebHelper.MapPath(UPLOAD_FILE_DIR);
            foreach (var f in Directory.GetFiles(dirPath, searchPattern, SearchOption.AllDirectories))
                File.Delete(f);
        }

        #endregion

    }
}
