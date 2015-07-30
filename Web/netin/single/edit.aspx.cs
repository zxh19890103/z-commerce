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

public partial class netin_single_edit : EditBase
{
    public override string TableName
    {
        get { return "SinglePage"; }
    }

    public override string ListPagePath
    {
        get { return "pages.aspx"; }
    }

    public SinglePage Model { get; set; }

    public override void Get()
    {
        if (IsEdit)
        {
            Model = DbAccessor.GetById<SinglePage>(NtID);
            if (Model == null)
                NotFound();
            AppendTitle(Model.Title);            
        }
        else
        {
            Model = new SinglePage();
            Model.InitData();
            Model.LanguageId = NtContext.Current.LanguageID;
        }
    }

    public override void Post()
    {
        var m = new SinglePage();
        m.InitDataFromPage();
        m.FirstPicture = NtUtility.GetImageUrl(m.Body);//获取首图
        m.UpdatedDate = DateTime.Now;
        if (m.Id > 0)
            DbAccessor.Update(m);
        else
            DbAccessor.Insert(m);
        Htmlizer.Instance.Category = "4";
        Htmlizer.Instance.GenerateHtml(m);
        Goto(ListPagePath, "保存成功!");
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "二级页面";
    }
}