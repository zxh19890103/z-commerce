using Nt.Framework.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Nt.Framework;

public partial class netin_common_cachefileMgr : PageBase
{

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }

    protected void OutAllCacheFile()
    {
        DirectoryInfo dir = new DirectoryInfo(MapPath("/cache/"));
        Response.Write("<ul>");
        foreach (FileInfo file in dir.GetFiles("cache.*"))
        {
            Response.Write("<li>");
            Response.Write("<a href=\"javascript:;\" title=\"编辑\" onclick=\"loadFileCxt(this);\">");
            Response.Write(file.Name);
            Response.Write("</a>");
            Response.Write("</li>");
        }
        Response.Write("</ul>");
    }

    [AjaxMethod]
    public void FetchCacheFileCxt()
    {
        NtJson json = new NtJson();
        string file = Request["file"];
        string path = MapPath(string.Format("/cache/{0}", file));
        json.Json["cache"] = File.ReadAllText(path);
        json.ErrorCode = NtJson.OK;
        json.Message = "got!";
        Response.Write(json);
    }

    [AjaxMethod]
    public void PostCacheFileCxt()
    {
        NtJson json = new NtJson();
        string file = Request["file"];
        string path = MapPath(string.Format("/cache/{0}", file));
        if (File.Exists(path))
        {
            json.ErrorCode = NtJson.OK;
            json.Message = "got!";
            File.WriteAllText(path, Request["cache"]);
        }
        else
        {
            json.ErrorCode = NtJson.ERROR;
            json.Message = "oh!";
        }

        Response.Write(json);
    }

    [AjaxMethod]
    public void ClearCacheFiles()
    {
        DirectoryInfo dir = new DirectoryInfo(MapPath("/cache"));
        foreach (FileInfo file in dir.GetFiles("cache.*"))
        {
            File.Delete(file.FullName);
        }
        NtJson.ShowOK();
    }

}