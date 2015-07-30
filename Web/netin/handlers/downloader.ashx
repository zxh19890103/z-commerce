<%@ WebHandler Language="C#" Class="downloader" %>

using System;
using System.Web;
using Nt.Framework;
using Nt.BLL;
using System.IO;

public class downloader : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        var Response = context.Response;
        
        string name = "";
        NtJson.EnsureNotNullOrEmpty("filename", "参数错误:filename", out name);

        string phy_filePath = WebHelper.MapPath("/app_data/backup/" + name);//路径

        //以字符流的形式下载文件
        FileStream fs = new FileStream(phy_filePath, FileMode.Open);
        byte[] bytes = new byte[(int)fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();
        //通知浏览器下载文件而不是打开
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8));
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}