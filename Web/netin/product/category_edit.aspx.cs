using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.BLL;
using Nt.DAL;
using Nt.Model;
using Nt.Framework;
using Nt.Framework.Admin;
using Nt.Model.View;
using Nt.Model.Common;

public partial class netin_product_category_edit : EditBase<View_ProductCategory, ProductCategory>
{
    protected int pid;
    List<NtListItem> _categories;
    /// <summary>
    /// 商品分类选项
    /// </summary>
    public List<NtListItem> Categories
    {
        get
        {
            if (_categories == null)
            {
                _categories = DB.GetDropdownlist<ProductCategory>("Display=1");
            }
            return _categories;
        }
    }

    public override void Get()
    {
        if (IsEdit)
        {
            Model = DbAccessor.GetById<View_ProductCategory>(NtID);
            if (Model == null)
                NotFound();//未发现相关记录
            AppendTitle(Model.Name);
        }
        else
        {
            pid = ParseInt32("pid");
            Model = new View_ProductCategory();            
            Model.InitData();
            Model.Parent = pid;
            Model.Display = true;
            Model.DisplayOrder = Model.MaxID;
            Model.LanguageId = NtContext.Current.LanguageID;
            var parentModel = DbAccessor.GetById<View_ProductCategory>(pid);
            if (parentModel != null)
            {
                Model.DefaultListTemplate = parentModel.ListTemplate;
                Model.DefaultDetailTemplate = parentModel.DetailTemplate;
            }
        }
    }

    public override string ListPagePath
    {
        get
        {
            return "categories.aspx";
        }
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "产品分类";
    }

}