using Nt.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Netin_shared_uploader : System.Web.UI.UserControl
{
    bool _isFile = false;
    string _postUrl = "/Netin/handlers/uploadHandler.aspx";

    /// <summary>
    /// 字段名
    /// </summary>
    public string FieldName { get; set; }
    /// <summary>
    /// 字段值
    /// </summary>
    public string FieldValue { get; set; }

    /// <summary>
    /// 文件大小
    /// </summary>
    public string FileSize { get; set; }
    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; }
    /// <summary>
    /// 文件路径（绝对）
    /// </summary>
    public string FileUrl { get { return FieldValue; } set { FieldValue = value; } }

    [UrlProperty]
    public string PostUrl
    {
        get { return _postUrl; }
        set { _postUrl = value; }
    }

    /// <summary>
    /// 是否是非图片资料
    /// </summary>
    public bool IsFile { get { return _isFile; } set { _isFile = value; } }

    /// <summary>
    /// 图片Url（缩略图）200x200
    /// </summary>
    public string ImgUrl
    {
        get
        {
            if (string.IsNullOrEmpty(FieldValue))
                return MediaService.NO_IMAGE;
            else
            {
                var ms = new MediaService();
                return ms.MakeThumbnail(FieldValue, 200, 200);
            }
        }
    }
}