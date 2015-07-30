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

public partial class netin_guestbook_edit : EditBase<GuestBook>
{

    public override string TableName
    {
        get { return "GusetBook"; }
    }

    public override string ListPagePath
    {
        get { return "list.aspx"; }
    }

    public override void Get()
    {
        if (IsEdit)
        {
            Model = DbAccessor.GetById<GuestBook>(NtID);
            if (Model == null)
                NotFound();
            SqlHelper.ExecuteNonQuery("Update [" + M(typeof(GuestBook)) + "] Set Viewed=1 Where Id="+Model.Id);
            Model.Viewed = true;
            AppendTitle(Model.Title);
        }
        else
        {
            Model = new GuestBook();
            Model.InitData();
            Model.Display = true;
            Model.LanguageId = NtContext.Current.LanguageID;
        }
    }

    public override void Post()
    {
        var e = new GuestBook();
        e.InitDataFromPage();
        DbAccessor.UpdateOrInsert(e);
        Goto(string.Format("{0}?page={1}", ListPagePath, PageIndex), "保存成功!");
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "留言";
    }

    public override string PermissionSysN
    {
        get
        {
            return "netin.guestbook.list";
        }
    }

}