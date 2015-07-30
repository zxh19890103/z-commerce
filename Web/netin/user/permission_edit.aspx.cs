using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Nt.Model;
using Nt.Model.Common;
using Nt.Model.Setting;
using Nt.Model.View;
using Nt.Framework;
using Nt.Framework.Admin;
using Nt.DAL;
using Nt.BLL;

public partial class netin_user_permission_edit : EditBase
{
    protected int fatherId = IMPOSSIBLE;
    protected bool addCategory = false;

    public override string TableName
    {
        get { return "permission"; }
    }

    public override string ListPagePath
    {
        get { return "permission.aspx"; }
    }

    public View_Permission Model { get; set; }

    public override void Get()
    {
        if (IsEdit)
        {
            Model = DbAccessor.GetById<View_Permission>(NtID);
            if (Model == null)
                NotFound();
        }
        else
        {
            Model = new View_Permission();
            Model.InitData();
            Model.IsCategory = addCategory;
            Model.FatherId = fatherId;
            Model.Display = true;
            Model.DisplayOrder = Model.MaxID;
        }
    }

    public override void Post()
    {
        var m = new Permission();
        m.InitDataFromPage();
        if (m.Id > 0)
            DbAccessor.Update(m);
        else
            DbAccessor.Insert(m);
        Goto("permission.aspx?id=" + m.Id, "保存成功!");
    }

    public void RenderSystemNames()
    {
        DirectoryInfo root = new DirectoryInfo(MapPath("/netin/"));
        Html("<ul  id=\"sysNames\" class=\"selector\">");
        Info(root, "netin");
        Html("</ul>");
    }

    void Info(DirectoryInfo folder, string preFix)
    {
        foreach (DirectoryInfo sub in folder.GetDirectories())
        {
            if ("article|common|customer|goods|guestbook|sale|single|user".IndexOf(sub.Name) > 0)
                Info(sub, string.Format("{0}.{1}", preFix, sub.Name));
        }

        foreach (FileInfo file in folder.GetFiles("*.aspx"))
        {
            Html("<li onclick=\"selectSysN(this);\">");
            Html(preFix);
            Html(".");
            Html(file.Name.Substring(0, file.Name.Length - 5));
            Html("</li>");
        }
    }

    protected override void Prepare()
    {
        base.Prepare();
        bool.TryParse(Request.QueryString["addCategory"], out addCategory);
        Int32.TryParse(Request.QueryString["fatherId"], out fatherId);
        if (!addCategory && fatherId == IMPOSSIBLE)
            addCategory = true;
        Title = EditPageTitlePrefix + "权限项";
    }

    public override string PermissionSysN
    {
        get
        {
            return "netin.user.permission";
        }
    }
}