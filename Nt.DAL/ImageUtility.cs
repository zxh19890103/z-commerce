using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Nt.DAL
{
    public class ImageUtility
    {
        /// <summary>
        /// 水印文字最小宽
        /// </summary>
        public const int WATER_TEXT_MIN_WIDTH = 40;
        /// <summary>
        /// 水印文字最小高
        /// </summary>
        public const int WATER_TEXT_MIN_HEIGHT = 30;
        /// <summary>
        /// 缩略图最小宽
        /// </summary>
        public const int THUMBNAIL_MIN_WIDTH = 40;
        /// <summary>
        /// 缩略图最小高
        /// </summary>
        public const int THUMBNAIL_MIN_HEIGHT = 30;

        #region config

        private string _originalImagePathOnDisk;
        bool _hasOriginalImagePath = false;
        /// <summary>
        /// 原始图片路径
        /// </summary>
        public string OriginalImagePathOnDisk
        {
            get { return _originalImagePathOnDisk; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                _originalImagePathOnDisk = value;
                _hasOriginalImagePath = true;
            }
        }

        private string _targetImagePathOnDisk;
        bool _hastargetImagePath = false;
        /// <summary>
        /// 目标存储路径
        /// </summary>
        public string TargetImagePathOnDisk
        {
            get
            {
                return _targetImagePathOnDisk;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                _targetImagePathOnDisk = value;
                _hastargetImagePath = true;
            }
        }

        private string _waterImagePathOnDisk;
        bool _hasWaterImagePath = false;
        /// <summary>
        /// 图片水印路径
        /// </summary>
        public string WaterImagePathOnDisk
        {
            get
            {
                return _waterImagePathOnDisk;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                _waterImagePathOnDisk = value;
                _hasWaterImagePath = true;
            }
        }

        private string _fontFamliy;
        bool _hasFontFamliy = false;
        /// <summary>
        /// 字体
        /// </summary>
        public string FontFamliy
        {
            get
            {
                if (_hasFontFamliy)
                    return _fontFamliy;
                return "宋体 ";
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                _fontFamliy = value;
                _hasFontFamliy = true;
            }

        }

        int _waterTextWidth = 400;
        /// <summary>
        /// 文字水印的宽
        /// </summary>
        public int WaterTextWidth
        {
            get
            {
                if (_waterTextWidth < WATER_TEXT_MIN_WIDTH)
                    return WATER_TEXT_MIN_WIDTH;
                return _waterTextWidth;
            }
            set { _waterTextWidth = value; }
        }

        int _waterTextHeight = 300;
        /// <summary>
        /// 文字水印的高
        /// </summary>
        public int WaterTextHeight
        {
            get
            {
                if (_waterTextWidth < WATER_TEXT_MIN_HEIGHT)
                    return WATER_TEXT_MIN_HEIGHT;
                return _waterTextHeight;
            }
            set
            {
                _waterTextHeight = value;
            }
        }

        private string _waterText = string.Empty;
        /// <summary>
        /// 水印文字
        /// </summary>
        public string WaterText
        {
            get
            {
                return _waterText;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                _waterText = value;
            }
        }

        private int _thumbnailImageWidth;
        /// <summary>
        /// 缩略图宽
        /// </summary>
        public int ThumbnailImageWidth
        {
            get { return _thumbnailImageWidth; }
            set
            {
                if (value < THUMBNAIL_MIN_WIDTH)
                    _thumbnailImageWidth = THUMBNAIL_MIN_WIDTH;
                _thumbnailImageWidth = value;
            }
        }

        private int _thumbnailImageHeight;
        /// <summary>
        /// 缩略图高
        /// </summary>
        public int ThumbnailImageHeight
        {

            get
            {
                return _thumbnailImageHeight;
            }
            set
            {
                if (value < THUMBNAIL_MIN_HEIGHT)
                    _thumbnailImageHeight = THUMBNAIL_MIN_HEIGHT;
                _thumbnailImageHeight = value;
            }

        }

        private float _alpha4WaterImage = 0.5f;
        public float Alpha4WaterImage
        {
            get { return _alpha4WaterImage; }
            set
            {
                if (value < 0 || value > 1)
                    return;
                _alpha4WaterImage = value;
            }
        }

        /// <summary>
        /// 缩略图生成模式
        /// </summary>
        public ThumbnailGenerationMode TGM { get; set; }

        /// <summary>
        /// 文字水印位置选项
        /// </summary>
        public WaterMarkPositionOption WMPO { get; set; }

        #endregion

        /// <summary>
        /// 给指定的图片加上文字水印，请先设置源图片路径和目标图片路径
        /// </summary>
        /// <param name="text">水印文字</param>
        /// <returns></returns>
        public void AddTextWaterMark()
        {
            try
            {
                if (!_hasOriginalImagePath)
                    throw new Exception("没有提供源图片路径");
                if (!_hastargetImagePath)
                    throw new Exception("没有提供目标图片路径");
                BuildWatermarkText(OriginalImagePathOnDisk,
                    TargetImagePathOnDisk, WaterText, FontFamliy,
                    WaterTextWidth, WaterTextHeight, WMPO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 生成图片水印,带文字，请先设置源图片路径和目标图片路径和水印图片路径
        /// </summary>
        /// <returns></returns>
        public void AddImageWaterMark()
        {
            try
            {
                if (!_hasOriginalImagePath)
                    throw new Exception("没有提供源图片路径");
                if (!_hastargetImagePath)
                    throw new Exception("没有提供目标图片路径");
                if (!_hasWaterImagePath)
                    throw new Exception("没有提供水印图片路径");
                BuildWatermark(OriginalImagePathOnDisk,
                    WaterImagePathOnDisk, WaterText,
                    TargetImagePathOnDisk, WMPO,
                    Alpha4WaterImage);
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// 生成缩略图，请先设置源图片路径和目标图片路径
        /// </summary>
        /// <returns></returns>
        public void WriteThumbnail()
        {
            try
            {
                if (!_hasOriginalImagePath)
                    throw new Exception("没有提供源图片路径");
                if (!_hastargetImagePath)
                    throw new Exception("没有提供目标图片路径");
                BuildThumbnail(OriginalImagePathOnDisk, TargetImagePathOnDisk,
                    ThumbnailImageWidth, ThumbnailImageHeight, TGM);
            }
            catch (Exception e) { throw e; }
        }

        #region utility

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式 HW,W,H,Cut,CutA</param>
        private void BuildThumbnail(string originalImagePath, string thumbnailPath, int width, int height, ThumbnailGenerationMode mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case ThumbnailGenerationMode.HW://指定高宽缩放（可能变形）
                    break;
                case ThumbnailGenerationMode.W://指定宽，高按比例
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ThumbnailGenerationMode.H://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ThumbnailGenerationMode.CUT://指定高宽裁减
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                case ThumbnailGenerationMode.CUTA://指定高宽裁减（不变形）自定义
                    if (ow <= towidth && oh <= toheight)
                    {
                        x = -(towidth - ow) / 2;
                        y = -(toheight - oh) / 2;
                        ow = towidth;
                        oh = toheight;
                    }
                    else
                    {
                        if (ow > oh)//宽大于高 
                        {
                            x = 0;
                            y = -(ow - oh) / 2;
                            oh = ow;
                        }
                        else//高大于宽 
                        {
                            y = 0;
                            x = -(oh - ow) / 2;
                            ow = oh;
                        }
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以白色背景色填充
            g.Clear(Color.White);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
             new Rectangle(x, y, ow, oh),
             GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }


        /// <summary>
        /// Creating a Watermarked Photograph with GDI+ for .NET      
        /// </summary>      
        /// <param name="rSrcImgPath">原始图片的物理路径</param>      
        /// <param name="rMarkImgPath">水印图片的物理路径</param>      
        /// <param name="rMarkText">水印文字（不显示水印文字设为空串）</param>      
        /// <param name="rDstImgPath">输出合成后的图片的物理路径</param>      
        private void BuildWatermark(string rSrcImgPath, string rMarkImgPath,
            string rMarkText, string rDstImgPath
            , WaterMarkPositionOption pos,
            float alpha)
        {
            #region prepare

            //以下（代码）从一个指定文件创建了一个Image 对象，然后为它的 Width 和 Height定义变量。      
            //这些长度待会被用来建立一个以24 bits 每像素的格式作为颜色数据的Bitmap对象。      
            Image imgPhoto = Image.FromFile(rSrcImgPath);
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(72, 72);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            //这个代码载入水印图片，水印图片已经被保存为一个BMP文件，以绿色(A=0,R=0,G=255,B=0)作为背景颜色。      
            //再一次，会为它的Width 和Height定义一个变量
            Image imgWatermark = new Bitmap(rMarkImgPath);
            int wmWidth = imgWatermark.Width;
            int wmHeight = imgWatermark.Height;
            //这个代码以100%它的原始大小绘制imgPhoto 到Graphics 对象的（x=0,y=0）位置。      
            //以后所有的绘图都将发生在原来照片的顶部。      
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            grPhoto.DrawImage(
                 imgPhoto,
                 new Rectangle(0, 0, phWidth, phHeight),
                 0,
                 0,
                 phWidth,
                 phHeight,
                 GraphicsUnit.Pixel);

            #endregion

            #region draw text
            //为了最大化版权信息的大小，我们将测试7种不同的字体大小来决定我们能为我们的照片宽度使用的可能的最大大小。      
            //为了有效地完成这个，我们将定义一个整型数组，接着遍历这些整型值测量不同大小的版权字符串。      
            //一旦我们决定了可能的最大大小，我们就退出循环，绘制文本      
            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();
            for (int i = 0; i < 7; i++)
            {
                crFont = new Font("arial", sizes[i],
                      FontStyle.Bold);
                crSize = grPhoto.MeasureString(rMarkText,
                      crFont);
                if ((ushort)crSize.Width < (ushort)phWidth)
                    break;
            }
            //因为所有的照片都有各种各样的高度，所以就决定了从图象底部开始的5%的位置开始。      
            //使用rMarkText字符串的高度来决定绘制字符串合适的Y坐标轴。      
            //通过计算图像的中心来决定X轴，然后定义一个StringFormat 对象，设置StringAlignment 为Center。      
            int yPixlesFromBottom = (int)(phHeight * .05);
            float yPosFromBottom = ((phHeight -
                 yPixlesFromBottom) - (crSize.Height / 2));
            float xCenterOfImg = (phWidth / 2);
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;
            //现在我们已经有了所有所需的位置坐标来使用60%黑色的一个Color(alpha值153)创建一个SolidBrush 。      
            //在偏离右边1像素，底部1像素的合适位置绘制版权字符串。      
            //这段偏离将用来创建阴影效果。使用Brush重复这样一个过程，在前一个绘制的文本顶部绘制同样的文本。      
            SolidBrush semiTransBrush2 =
                 new SolidBrush(Color.FromArgb(153, 0, 0, 0));
            grPhoto.DrawString(rMarkText,
                 crFont,
                 semiTransBrush2,
                 new PointF(xCenterOfImg + 1, yPosFromBottom + 1),
                 StrFormat);
            SolidBrush semiTransBrush = new SolidBrush(
                 Color.FromArgb(153, 255, 255, 255));
            grPhoto.DrawString(rMarkText,
                 crFont,
                 semiTransBrush,
                 new PointF(xCenterOfImg, yPosFromBottom),
                 StrFormat);

            #endregion

            #region draw image
            //根据前面修改后的照片创建一个Bitmap。把这个Bitmap载入到一个新的Graphic对象。      
            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(
                 imgPhoto.HorizontalResolution,
                 imgPhoto.VerticalResolution);
            Graphics grWatermark =
                 Graphics.FromImage(bmWatermark);
            //通过定义一个ImageAttributes 对象并设置它的两个属性，我们就是实现了两个颜色的处理，以达到半透明的水印效果。      
            //处理水印图象的第一步是把背景图案变为透明的(Alpha=0, R=0, G=0, B=0)。我们使用一个Colormap 和定义一个RemapTable来做这个。      
            //就像前面展示的，我的水印被定义为100%绿色背景，我们将搜到这个颜色，然后取代为透明。      
            ImageAttributes imageAttributes =
                 new ImageAttributes();
            ColorMap colorMap = new ColorMap();
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };
            //第二个颜色处理用来改变水印的不透明性。      
            //通过应用包含提供了坐标的RGBA空间的5x5矩阵来做这个。      
            //通过设定第三行、第三列为0.3f我们就达到了一个不透明的水平。结果是水印会轻微地显示在图象底下一些。      

            imageAttributes.SetRemapTable(remapTable,
                 ColorAdjustType.Bitmap);
            float[][] colorMatrixElements = {       
                                                 new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},      
                                                 new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},      
                                                 new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},      
                                                 new float[] {0.0f,  0.0f,  0.0f,  alpha, 0.0f},      
                                                 new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}      
                                            };
            ColorMatrix wmColorMatrix = new
                 ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(wmColorMatrix,
                 ColorMatrixFlag.Default,
                 ColorAdjustType.Bitmap);
            //随着两个颜色处理加入到imageAttributes 对象，我们现在就能在照片右手边上绘制水印了。      
            //我们会偏离10像素到底部，10像素到左边。      
            int markWidth;
            int markHeight;
            //mark比原来的图宽      
            if (phWidth <= wmWidth)
            {
                markWidth = phWidth - 10;
                markHeight = (markWidth * wmHeight) / wmWidth;
            }
            else if (phHeight <= wmHeight)
            {
                markHeight = phHeight - 10;
                markWidth = (markHeight * wmWidth) / wmHeight;
            }
            else
            {
                markWidth = wmWidth;
                markHeight = wmHeight;
            }

            #region position of water mark

            int xPosOfWm = (phWidth - markWidth) / 2;
            int yPosOfWm = (phHeight - markHeight) / 2;

            switch (pos)
            {
                case WaterMarkPositionOption.LeftUp:
                    xPosOfWm = 10;
                    yPosOfWm = 10;
                    break;
                case WaterMarkPositionOption.Up:
                    yPosOfWm = 10;
                    break;
                case WaterMarkPositionOption.RightUp:
                    xPosOfWm = (phWidth - markWidth) - 10;
                    yPosOfWm = 10;
                    break;
                case WaterMarkPositionOption.Center:
                    break;
                case WaterMarkPositionOption.LeftDown:
                    xPosOfWm = 10;
                    yPosOfWm = (phHeight - markHeight) - 10;
                    break;
                case WaterMarkPositionOption.Down:
                    yPosOfWm = (phHeight - markHeight) - 10;
                    break;
                case WaterMarkPositionOption.RightDown:
                    xPosOfWm = (phWidth - markWidth) - 10;
                    yPosOfWm = (phHeight - markHeight) - 10;
                    break;
                default:
                    break;
            }
            #endregion

            grWatermark.DrawImage(imgWatermark,
                 new Rectangle(xPosOfWm, yPosOfWm, markWidth,
                 markHeight),
                 0,
                 0,
                 wmWidth,
                 wmHeight,
                 GraphicsUnit.Pixel,
                 imageAttributes);
            //最后的步骤将是使用新的Bitmap取代原来的Image。 销毁两个Graphic对象，然后把Image 保存到文件系统。      



            #endregion

            try
            {
                imgPhoto = bmWatermark;
                imgPhoto.Save(rDstImgPath, ImageFormat.Jpeg);
            }
            catch (Exception e) { throw e; }
            finally
            {
                grPhoto.Dispose();
                grWatermark.Dispose();
                imgPhoto.Dispose();
                imgWatermark.Dispose();
            }
        }


        /// <summary>
        /// 文字水印
        /// </summary>
        /// <param name="srcPath">原图路径</param>
        /// <param name="targetPath">目标图片路径</param>
        /// <param name="watermarkText">文字</param>
        /// <param name="fontFamily">字体</param>
        /// <param name="width">文本框的宽度</param>
        /// <param name="height">文本框的高度</param>
        /// <param name="pos">位置，1:左上,2:居上,3:右上,4:居中,5:左下,6:居下,7:右下,默认为居中</param>
        private void BuildWatermarkText(string srcPath, string targetPath, string watermarkText,
            string fontFamily, int width, int height, WaterMarkPositionOption pos)
        {
            Image img = Image.FromFile(srcPath);
            Graphics picture = Graphics.FromImage(img);

            // 确定水印文字的字体大小
            int[] sizes = new int[] { 32, 30, 28, 26, 24, 22, 20, 18, 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();
            for (int i = 0; i < sizes.Length; i++)
            {
                crFont = new Font(fontFamily, sizes[i], FontStyle.Bold);
                crSize = picture.MeasureString(watermarkText, crFont);
                if ((ushort)crSize.Width < (ushort)width)
                    break;
            }

            // 生成水印图片（将文字写到图片中）
            Bitmap floatBmp = new Bitmap((int)crSize.Width + 3,
                  (int)crSize.Height + 3, PixelFormat.Format32bppArgb);
            Graphics fg = Graphics.FromImage(floatBmp);
            PointF pt = new PointF(0, 0);
            // 画阴影文字
            Brush TransparentBrush0 = new SolidBrush(Color.FromArgb(255, Color.Black));
            Brush TransparentBrush1 = new SolidBrush(Color.FromArgb(255, Color.Black));
            fg.DrawString(watermarkText, crFont, TransparentBrush0, pt.X, pt.Y + 1);
            fg.DrawString(watermarkText, crFont, TransparentBrush0, pt.X + 1, pt.Y);
            fg.DrawString(watermarkText, crFont, TransparentBrush1, pt.X + 1, pt.Y + 1);
            fg.DrawString(watermarkText, crFont, TransparentBrush1, pt.X, pt.Y + 2);
            fg.DrawString(watermarkText, crFont, TransparentBrush1, pt.X + 2, pt.Y);
            TransparentBrush0.Dispose();
            TransparentBrush1.Dispose();
            // 画文字
            fg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fg.DrawString(watermarkText,
             crFont, new SolidBrush(Color.White),
             pt.X, pt.Y, StringFormat.GenericDefault);
            // 保存刚才的操作
            fg.Save();
            fg.Dispose();

            #region Calculate Position

            int fw = floatBmp.Width;
            int fh = floatBmp.Height;
            int sw = img.Width;
            int sh = img.Height;

            int x = 0, y = 0;

            switch (pos)
            {
                case WaterMarkPositionOption.LeftUp:
                    x = 5;
                    y = 5;
                    break;
                case WaterMarkPositionOption.Up:
                    x = (sw - fw) / 2 <= 0 ? 5 : (sw - fw) / 2;
                    y = 5;
                    break;
                case WaterMarkPositionOption.RightUp:
                    x = sw - fw;
                    y = 5;
                    break;
                case WaterMarkPositionOption.Center:
                    x = (sw - fw) / 2 <= 0 ? 5 : (sw - fw) / 2;
                    y = (sh - fh) / 2 <= 0 ? 5 : (sh - fh) / 2;
                    break;
                case WaterMarkPositionOption.LeftDown:
                    x = 5;
                    y = sh - fh;
                    break;
                case WaterMarkPositionOption.Down:
                    x = (sw - fw) / 2 <= 0 ? 5 : (sw - fw) / 2;
                    y = sh - fh;
                    break;
                case WaterMarkPositionOption.RightDown:
                    x = sw - fw;
                    y = sh - fh;
                    break;
                default:
                    x = (sw - fw) / 2 <= 0 ? 5 : (sw - fw) / 2;
                    y = (sh - fh) / 2 <= 0 ? 5 : (sh - fh) / 2;
                    break;
            }

            #endregion

            try
            {
                picture.DrawImage(floatBmp, new Point(x, y));
                img.Save(targetPath, ImageFormat.Jpeg);
            }
            catch (Exception e) { throw e; }
            finally
            {
                picture.Dispose();
                img.Dispose();
            }

        }

        #endregion

    }

    /// <summary>
    /// 缩略图生成模式
    /// </summary>
    public enum ThumbnailGenerationMode : int
    {
        /// <summary>
        ///指定高宽缩放（可能变形）
        /// </summary>
        HW = 1,
        /// <summary>
        /// 指定高，宽按比例
        /// </summary>
        H = 10,
        /// <summary>
        /// 指定宽，高按比例
        /// </summary>
        W = 20,
        /// <summary>
        /// 指定高宽裁减
        /// </summary>
        CUT = 30,
        /// <summary>
        /// 指定高宽裁减（不变形）自定义
        /// </summary>
        CUTA = 40
    }

    /// <summary>
    /// 文字水印位置选项
    /// </summary>
    public enum WaterMarkPositionOption : int
    {
        /// <summary>
        /// 居左上
        /// </summary>
        LeftUp = 10,
        /// <summary>
        /// 居上
        /// </summary>
        Up = 20,
        /// <summary>
        /// 居右上
        /// </summary>
        RightUp = 30,
        /// <summary>
        /// 居中
        /// </summary>
        Center = 40,
        /// <summary>
        /// 居左下
        /// </summary>
        LeftDown = 50,
        /// <summary>
        /// 居下
        /// </summary>
        Down = 60,
        /// <summary>
        /// 居右下
        /// </summary>
        RightDown = 70
    }
}
