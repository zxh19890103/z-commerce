using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// 负责所有图片上传的程序
/// </summary>
public partial class netin_handlers_uploadHandler : System.Web.UI.Page
{
    protected override void OnLoad(EventArgs e)
    {
        if (Request.HttpMethod == "POST")
        {
            bool isfile = false;
            NtJson.EnsureBoolean("isfile", "参数错误:isfile", out isfile);
            MediaService ms = new MediaService();
            string thumbnail = string.Empty;
            if (isfile)
            {
                ms.AllowedUploadFormats = MediaService.ALLOWED_File_FORMAT;
                ms.CurrentUploadCategory = "files";
            }
            else
                ms.AllowedUploadFormats = MediaService.ALLOWED_PIC_FORMAT;
            bool error = false;
            string message = string.Empty;
            try
            {
                ms.AsyncUpload();
                if (!isfile)
                {
                    ms.MakeThumbnail(ms.FileUrl, 80, 80);//用于在后台列表页显示
                    thumbnail = ms.MakeThumbnail(ms.FileUrl, 200, 200);//用于编辑页显示
                }
            }
            catch (Exception ex)
            {
                error = true;
                message = ex.Message;
            }
            if (error)
                NtJson.ShowError(message);
            else
                NtJson.ShowOK("ok", new { fileUrl = ms.FileUrl, fileSize = ms.FileSize, fileName = ms.FileName, thumbnail = thumbnail });
        }
    }
}