using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.Model;
using Nt.Model.Common;
using Nt.Model.Setting;
using Nt.Model.View;
using Nt.Framework;
using Nt.Framework.Admin;
using Nt.DAL;
using Nt.BLL;

public partial class netin_common_sitemap : PageBase
{
    [AjaxMethod]
    public void GenSitemap()
    {
        int type = 0;
        bool isHtml = false;
        bool byMenu = false;
        NtJson.EnsureNumber("type", "参数错误:type", out type);
        NtJson.EnsureBoolean("isHtml", "参数错误:isHtml", out isHtml);
        NtJson.EnsureBoolean("byMenu", "参数错误:byMenu", out byMenu);

        string postUrl = "javascript:notice('no link');";

        SitemapHelper sitemap = new SitemapHelper();//不包含列表
        sitemap.SitemapType = (SitemapType)type;

        if (sitemap.SitemapType == SitemapType.Baidu)
        {
            sitemap.SiteMapName = "sitemap.xml";
            postUrl = "http://zhanzhang.baidu.com/";
        }
        else if (sitemap.SitemapType == SitemapType.Google)
        {
            sitemap.SiteMapName = "sitemap-google.xml";
            postUrl = "http://www.google.cn/webmasters/";
        }
        else if (sitemap.SitemapType == SitemapType.Sougou)
        {
            sitemap.SiteMapName = "sitemap-sougou.xml";
        }
        else if (sitemap.SitemapType == SitemapType.Sousou)
        {
            sitemap.SiteMapName = "sitemap-sousou.xml";
        }

        sitemap.VirtualDir2SaveXml = "/";//xml文件放在网站根目录下
        sitemap.IsHtml = isHtml;
        NtJson json = new NtJson();
        try
        {
            if (byMenu)
                sitemap.GenerateSitemapAccordingToMenu();
            else
                sitemap.GenerateSitemap();
            json.ErrorCode = NtJson.OK;
            json.Message = sitemap.SiteMapName + "生成成功!";
        }
        catch (Exception ex)
        {
            json.ErrorCode = NtJson.ERROR;
            json.Message = ex.Message;
        }

        
        json.Json["countOfFound"] = sitemap.CountOfFound;
        json.Json["sitemapPath"] = sitemap.VirtualDir2SaveXml + sitemap.SiteMapName;
        json.Json["postUrl"] = postUrl;
        Response.Write(json);
        Response.End();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Title = "网站地图";
    }

}