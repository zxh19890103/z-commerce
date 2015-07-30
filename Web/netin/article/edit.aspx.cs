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

public partial class netin_article_edit : EditBase
{

    public override string TableName
    {
        get { return "Article"; }
    }

    public override string ListPagePath
    {
        get { return "articles.aspx"; }
    }


    protected int articleClassId = IMPOSSIBLE;
    List<NtListItem> _categories;

    public View_Article Model { get; set; }

    /// <summary>
    /// 文章分类选项
    /// </summary>
    public List<NtListItem> Categories
    {
        get
        {
            if (_categories == null)
            {
                _categories = DB.GetDropdownlist<Article_Class>("Display=1");
            }
            return _categories;
        }
    }
    
    public override void Get()
    {
        if (IsEdit)
        {
            Model = DbAccessor.GetById<View_Article>(NtID);
            if (Model == null)
                NotFound();
            uploaderfile.FileName = Model.FileName;
            uploaderfile.FileUrl = Model.FileUrl;
            uploaderfile.FileSize = Model.FileSize;
            AppendTitle(Model.Title);
        }
        else
        {
            Int32.TryParse(Request.QueryString["acid"], out articleClassId);

            Model = new View_Article();
            Model.InitData();
            Model.Display = true;
            Model.DisplayOrder = Model.MaxID;
            Model.ArticleClassId = articleClassId;
            Model.LanguageId = NtContext.Current.LanguageID;
        }
    }

    public override void Post()
    {
        Article m = new Article();
        m.InitDataFromPage();
        m.PictureUrl = NtUtility.GetImageUrl(m.Body);
        m.UserId = NtContext.Current.UserID;
        m.UpdatedDate = DateTime.Now;
        DbAccessor.UpdateOrInsert(m);
        Htmlizer.Instance.Category = "1";
        Htmlizer.Instance.GenerateHtml<View_Article>(m.Id);
        Logger.Log(m.Id > 0, "文章", m.Title);
        Goto("articles.aspx?id=" + NtID + "page=" + PageIndex, "保存成功!");
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "文章";
    }

}