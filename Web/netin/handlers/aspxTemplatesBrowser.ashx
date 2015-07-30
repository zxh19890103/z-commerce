<%@ WebHandler Language="C#" Class="aspxTemplatesBrowser" %>

using System;
using System.Web;
using System.IO;
using Nt.BLL;

public class aspxTemplatesBrowser : IHttpHandler
{
    HttpResponse Response;
    HttpRequest Request;
    string excludeFolders = NtContext.Current.SysReservedDirs;
    int idCounter = 0;
    int preFixLength = 0;

    public void ProcessRequest(HttpContext context)
    {
        Response = context.Response;
        Request = context.Request;
        Response.ContentType = "text/html";
        string rootPath = WebHelper.MapPath("/");
        DirectoryInfo root = new DirectoryInfo(rootPath);
        preFixLength = AppDomain.CurrentDomain.BaseDirectory.Length;
        Response.Write("<div id=\"ntTemplatesFiles\">");
        Response.Write("<ul class=\"nt-templates-item-wrap\">");
        DG(root);
        Response.Write("</ul>");
        Response.Write("</div>");
    }

    void DG(DirectoryInfo dir)
    {
        Response.Write("<li class=\"nt-templates-item nt-templates-item-folder-wrap\">");
        Response.Write("<a href=\"javascript:;\" class=\"nt-templates-item-folder\" data-target-folder-id=\"");
        Response.Write(idCounter);
        Response.Write("\">");
        Response.Write(dir.FullName.Replace((char)92, (char)47).Substring(preFixLength - 1));
        Response.Write("</a><ul id=\"nt-templates-folder-");
        Response.Write(idCounter);
        Response.Write("\" class=\"nt-templates-item-wrap\">");
        idCounter++;
        foreach (var subDir in dir.GetDirectories())
        {
            if (excludeFolders.IndexOf(subDir.Name.ToLower()) > -1)
                continue;
            DG(subDir);
        }
        foreach (var f in dir.GetFiles("*.aspx"))
        {
            Response.Write("<li class=\"nt-templates-item\"><a  class=\"nt-templates-item-file\"  href=\"javascript:;\" onclick=\"\">");
            Response.Write(f.FullName.Replace((char)92, (char)47).Substring(preFixLength - 1));
            Response.Write("</a></li>");
        }
        Response.Write("</ul>");
        Response.Write("</li>");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}