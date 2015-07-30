using Nt.Framework.Admin;
using Nt.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class netin_article_settings : SettingBase<ArticleSetting>
{

    protected override void AfterPost()
    {
       
    }

    protected override void AfterGet()
    {
        Title = "文章设置";
    }
}