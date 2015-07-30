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

public partial class netin_article_article_class_edit : EditBase
{

    protected int pid;

    List<NtListItem> _categories;

    public View_ArticleClass Model { get; set; }

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

    public override string TableName
    {
        get { return "Article_Class"; }
    }

    public override string ListPagePath
    {
        get { return "article_class.aspx"; }
    }

    public override void Get()
    {
        if (IsEdit)
        {
            Model = DbAccessor.GetById<View_ArticleClass>(NtID);
            if (Model == null)
                NotFound();
        }
        else
        {
            pid = ParseInt32("pid");
            Model = new View_ArticleClass();
            Model.InitData();
            Model.Parent = pid;
            Model.Display = true;
            Model.DisplayOrder = Model.MaxID;
            Model.LanguageId = NtContext.Current.LanguageID;
            var parentModel = DbAccessor.GetById<View_ArticleClass>(pid);
            if (parentModel != null)
            {
                Model.DefaultListTemplate = parentModel.ListTemplate;
                Model.DefaultDetailTemplate = parentModel.DetailTemplate;
            }
        }
    }

    public override void Post()
    {
        var m = new Article_Class();
        m.InitDataFromPage();
        if (m.Id > 0)
            DbAccessor.Update(m);
        else
            DbAccessor.Insert(m);
        Logger.Log(m.Id > 0, "文章分类", m.Name);
        Goto(ListPagePath, "保存成功!");
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "文章分类";
    }
}