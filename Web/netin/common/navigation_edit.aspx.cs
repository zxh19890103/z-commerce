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
using Nt.Model.Enum;

public partial class netin_common_navigation_edit : EditBase
{

    protected int pid;

    List<NtListItem> _categories;

    public override string PermissionSysN
    {
        get
        {
            return "netin.common.navigation";
        }
    }

    public View_Navigation Model { get; set; }

    /// <summary>
    /// 导航选项
    /// </summary>
    public List<NtListItem> Categories
    {
        get
        {
            if (_categories == null)
                _categories = DB.GetDropdownlist<Navigation>("Display=1");
            return _categories;
        }
    }

    public override string TableName
    {
        get { return "Navigation"; }
    }

    public override string ListPagePath
    {
        get { return "Navigation.aspx"; }
    }

    public override void Get()
    {
        Int32.TryParse(Request.QueryString["pid"], out pid);

        if (IsEdit)
        {
            Model = DbAccessor.GetById<View_Navigation>(NtID);
            if (Model == null)
                NotFound();
            AppendTitle(Model.Name);
        }
        else
        {
            Model = new View_Navigation();
            Model.InitData();
            Model.Parent = pid;
            Model.Display = true;
            Model.DisplayOrder = Model.MaxID;
            Model.NaviType = (int)ModuleType.Link;
            Model.LanguageId = NtContext.Current.LanguageID;
        }
    }

    public override void Post()
    {
        var m = new Navigation();
        m.InitDataFromPage();
        if (m.Id > 0)
            DbAccessor.Update(m);
        else
            DbAccessor.Insert(m);
        Logger.Log(m.Id > 0, "导航", m.Name);
        Goto(ListPagePath, "保存成功!");
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "导航";
    }

    public void OutRootSidOrPageIdsSelections()
    {
        DB.List("Article_Class", "display=1").List("Goods_Class", "display=1").List("ProductCategory", "display=1").List("SinglePage").Execute();
        DB.TreeSourceIndex = 0;
        Response.Write("var articleClasses=");
        Response.Write(NtUtility.GetJsObjectArrayFromList(new NtListItem("根", "0"), DB.GetDropdownlist<Article_Class>()));
        Response.Write(";");
        DB.TreeSourceIndex = 1;
        Response.Write("var goodsClasses=");
        Response.Write(NtUtility.GetJsObjectArrayFromList(new NtListItem("根", "0"), DB.GetDropdownlist<Goods_Class>()));
        Response.Write(";");
        DB.TreeSourceIndex = 2;
        Response.Write("var productCategories=");
        Response.Write(NtUtility.GetJsObjectArrayFromList(new NtListItem("根", "0"), DB.GetDropdownlist<ProductCategory>()));
        Response.Write(";");
        DB.DDLSourceIndex = 3;
        Response.Write("var singlePageIds=");
        Response.Write(NtUtility.GetJsObjectArrayFromList(DB.GetDropdownlist<SinglePage>("title", "id")));
        Response.Write(";");
    }

}