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

public partial class netin_common_slider_edit : EditBase
{

    public override string TableName
    {
        get { return "Slider"; }
    }

    public override string ListPagePath
    {
        get { return "Slider.aspx"; }
    }

    public override string PermissionSysN
    {
        get
        {
            return "netin.common.slider";
        }
    }

    public View_Slider Model { get; set; }

    public override void Get()
    {
        if (IsEdit)
        {
            Model = DbAccessor.GetById<View_Slider>(NtID);
            if (Model == null)
                NotFound();
            AppendTitle(Model.Title);
        }
        else
        {
            Model = new View_Slider();
            Model.InitData();
            Model.Display = true;
            Model.DisplayOrder = Model.MaxID;
            Model.LanguageId = NtConfig.CurrentLanguage;
        }
    }

    public override void Post()
    {
        var m = new Slider();
        m.InitDataFromPage();
        if (m.Id > 0)
            DbAccessor.Update(m);
        else
            DbAccessor.Insert(m);
        Logger.Log(m.Id > 0, "Slider(幻灯片)", m.Title);
        Goto(ListPagePath, "保存成功!");
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "Slider(幻灯片)";
    }
}