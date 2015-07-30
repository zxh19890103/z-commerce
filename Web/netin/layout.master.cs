using Nt.DAL;
using Nt.Framework.Admin;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class netin_layout : System.Web.UI.MasterPage
{
    /// <summary>
    /// 对当前母页面的引用
    /// </summary>
    public PageBase NtPage { get { return Page as PageBase; } }
    
    /// <summary>
    /// 输出语言版本选择器
    /// </summary>
    public void RenderLanguageSelector()
    {
        Response.Write("<ul id=\"content_language_selector\">");
        var d = DbAccessor.GetList<Language>("Published=1", "DisplayOrder");
        if (d.Count > 1)
        {
            var currLang = Nt.BLL.NtContext.Current.CurrentLanguage;
            foreach (var item in d)
            {
                Response.Write("<li class=\"");
                if (currLang.Id == item.Id)
                    Response.Write("current-language\"");
                else
                {
                    Response.Write("un-selected-lang-item\" onclick=\"selectLanguage(");
                    Response.Write(item.Id);
                    Response.Write(");\"");
                }
                Response.Write("><img src=\"/netin/content/flags/");
                Response.Write(item.LanguageCode);
                Response.Write(".png\" title=\"");
                Response.Write(item.Name);
                Response.Write("\" alt=\"");
                Response.Write(item.LanguageCode);
                Response.Write("\"/></li>");
            }
        }
        Response.Write("</ul>");
    }

}
