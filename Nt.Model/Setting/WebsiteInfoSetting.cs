using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.Setting
{
    public class WebsiteInfoSetting : BaseSetting
    {
        public string WebsiteName { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string WebsiteUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Linkman { get; set; }
        public string ZipCode { get; set; }
        public string ICP { get; set; }
        public bool SetHtmlOn { get; set; }
        public string QQ { get; set; }
        public bool SetWebsiteOff { get; set; }
        public string Logo { get; set; }
        public bool HtmlMode { get; set; }
    }
}
