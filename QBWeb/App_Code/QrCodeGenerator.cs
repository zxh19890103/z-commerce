using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using ThoughtWorks.QRCode.Codec;

/// <summary>
/// QrCodeGenerator 的摘要说明
/// </summary>
public class QrCodeGenerator
{

    private string _codeContent;

    /// <summary>
    /// the string of code
    /// </summary>
    public string CodeContent { get { return _codeContent; } set { _codeContent = value; } }


    public QrCodeGenerator()
    {
    }

    public QrCodeGenerator(string codeContent)
    {
        _codeContent = codeContent;
    }



    #region 二维码生成


    public void Run()
    {
        Bitmap img = Create_ImgCode(_codeContent,1);
        SaveImg(Nt.BLL.WebHelper.MapPath("/upload/qr/"), img);
    }



    /// <summary>
    /// 保存图片
    /// </summary>
    /// <param name="strPath">保存路径</param>
    /// <param name="img">图片</param>
    public void SaveImg(string strPath, Bitmap img)
    {
        //保存图片到目录
        if (Directory.Exists(strPath))
        {
            //文件名称
            string guid = Guid.NewGuid().ToString().Replace("-", "") + ".png";
            img.Save(strPath + "/" + guid, System.Drawing.Imaging.ImageFormat.Png);
        }
        else
        {
            //当前目录不存在，则创建
            Directory.CreateDirectory(strPath);
        }
    }

    /// <summary>
    /// 生成二维码图片
    /// </summary>
    /// <param name="codeNumber">要生成二维码的字符串</param>     
    /// <param name="size">大小尺寸</param>
    /// <returns>二维码图片</returns>
    public Bitmap Create_ImgCode(string codeNumber, int size)
    {
        //创建二维码生成类
        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        //设置编码模式
        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
        //设置编码测量度
        qrCodeEncoder.QRCodeScale = size;
        //设置编码版本
        qrCodeEncoder.QRCodeVersion = 0;
        //设置编码错误纠正
        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
        //生成二维码图片
        System.Drawing.Bitmap image = qrCodeEncoder.Encode(codeNumber);

        return image;
    }

    #endregion



}