using Nt.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Nt.Framework
{
    /// <summary>
    /// BaseUserControl 的摘要说明
    /// </summary>
    public class BaseAscx : UserControl
    {
        WebsiteInfoSetting _websiteInfo;
        /// <summary>
        /// 网站基本信息
        /// </summary>
        public WebsiteInfoSetting WebsiteInfo
        {
            get
            {
                BaseAspx page = Page as BaseAspx;
                if (page == null)
                {
                    _websiteInfo = new WebsiteInfoSetting();
                }
                else
                {
                    _websiteInfo = page.WebsiteInfo;
                }
                return _websiteInfo;
            }
        }

        /// <summary>
        /// 此控件拖拽至继承BaseAspx基本Page类的页面时，该属性有效，它是对目页面的引用
        /// </summary>
        public BaseAspx PageBasedOn { get { return Page as BaseAspx; } }

        /// <summary>
        /// 手机站跳转
        /// </summary>
        public void RenderMobiSiteRedirectScript()
        {
            RenderMobiSiteRedirectScript(NtConfig.MobileSiteUrl);
        }

        /// <summary>
        /// 手机站跳转
        /// </summary>
        /// <param name="url">手机站网址</param>
        public void RenderMobiSiteRedirectScript(string url)
        {
            Response.Write("<script type=\"text/javascript\">");
            Response.Write("var mobileAgent = new Array(\"iphone\", \"ipod\", \"ipad\", \"android\", \"mobile\", \"blackberry\", \"webos\", \"incognito\", \"webmate\", \"bada\", \"nokia\", \"lg\", \"ucweb\", \"skyfire\");");
            Response.Write("var browser = navigator.userAgent.toLowerCase();");
            Response.Write("var isMobile = false;");
            Response.Write("for (var i = 0; i < mobileAgent.length; i++) {");
            Response.Write("if (browser.indexOf(mobileAgent[i]) != -1) {");
            Response.Write("isMobile = true;");
            Response.Write("window.location.href = '" + url + "';");
            Response.Write("break;}}");
            Response.Write("</script>");
        }
        
    }
}