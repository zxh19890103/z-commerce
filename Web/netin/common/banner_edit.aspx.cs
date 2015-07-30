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

public partial class netin_common_banner_edit : EditBase
{

    public override string TableName
    {
        get { return "Banner"; }
    }

    public override string ListPagePath
    {
        get { return "banner.aspx"; }
    }

    public override string PermissionSysN
    {
        get
        {
            return "netin.common.banner";
        }
    }

    public View_Banner Model { get; set; }

    public override void Get()
    {
        if (IsEdit)
        {
            Model = DbAccessor.GetById<View_Banner>(NtID);
            if (Model == null)
                NotFound();
            AppendTitle(Model.Title);
        }
        else
        {
            Model = new View_Banner();
            Model.InitData();
            Model.Display = true;
            Model.DisplayOrder = Model.MaxID;
            Model.LanguageId = NtContext.Current.LanguageID;
        }
    }

    public override void Post()
    {
        var m = new Banner();
        m.InitDataFromPage();
        if (m.Id > 0)
            DbAccessor.Update(m);
        else
            DbAccessor.Insert(m);
        Logger.Log(m.Id > 0, "Banner图", m.Title);
        Goto(ListPagePath, "保存成功!");
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "Banner图";
    }
}