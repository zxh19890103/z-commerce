using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Nt.Framework.Admin;
using Nt.BLL;
using Nt.Model.View;
using Nt.Model.Common;

public partial class Netin_Goods_category_edit : EditBase
{

    protected int pid;

    

    public View_GoodsClass Model { get; set; }

    public override string TableName
    {
        get { return "Goods_Class"; }
    }

    public override string ListPagePath
    {
        get { return "category_list.aspx"; }
    }


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
                _categories = DB.GetDropdownlist<Goods_Class>("Display=1");
            }
            return _categories;
        }
    }


    public override void Get()
    {
        if (IsEdit)
        {
            Model = DbAccessor.GetById<View_GoodsClass>(NtID);
            if (Model == null)
                NotFound();
        }
        else
        {
            pid = ParseInt32("pid");            
            Model = new View_GoodsClass();
            Model.InitData();
            Model.Parent = pid;
            Model.Display = true;
            Model.DisplayOrder = Model.MaxID;
            Model.LanguageId = NtContext.Current.LanguageID;
            var parentModel = DbAccessor.GetById<View_GoodsClass>(pid);
            if (parentModel != null)
            {
                Model.DefaultListTemplate = parentModel.ListTemplate;
                Model.DefaultDetailTemplate = parentModel.DetailTemplate;
            }
        }
    }

    public override void Post()
    {
        var m = new Goods_Class();
        m.InitDataFromPage();
        DbAccessor.UpdateOrInsert(m);
        Logger.Log(m.Id > 0, "商品分类", m.Name);
        Goto(ListPagePath, "保存成功!");
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "商品分类";
    }


}