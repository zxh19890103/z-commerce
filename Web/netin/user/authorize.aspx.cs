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
using System.IO;

public partial class netin_user_authorize : PageBase
{

    protected int level = IMPOSSIBLE;
    protected List<NtListItem> Levels = null;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Title = "管理员授权";

        var user = NtContext.Current.CurrentUser;
        Int32.TryParse(Request["level"], out level);

        if (IsHttpPost)
        {
            if (user.UserLevelId == level)
                Goto("authorize.aspx", "无需为自己所属组授权!");
            SaveAuthorize();
            Goto("authorize.aspx", "授权成功!");
        }

        Levels = DB.GetDropdownlist<UserLevel>("Name", "Id", "IsAdmin=0 And Id>=" + user.UserLevelId);

        if (Levels.Count == 0)
            Response.Redirect("users.aspx", true);

        if (level == IMPOSSIBLE)
            level = Convert.ToInt32(Levels[0].Value);

        NtUtility.ListItemSelect(Levels, level);
    }

    public void RenderRecords()
    {
        var user = NtContext.Current.CurrentUser;
        UserService us = new Nt.BLL.UserService();
        string filter = string.Empty;
        List<Permission> d = null;
        if (!user.IsAdmin)//非超级用户组成员
        {
            d = DbAccessor.GetList<View_User_Permission>(
                "UserLevelId=" + user.UserLevelId, "DisplayOrder").Cast<Permission>().ToList();
        }
        else
        {
            d = DbAccessor.GetList<Permission>("", "DisplayOrder");
        }

        string selected = us.GetPermissionRecordsByLevel(level);

        RenderSubRecords(d, 0, selected);

    }

    void RenderSubRecords(List<Permission> data, int fid, string selected)
    {
        foreach (var item in data.Where(x => x.FatherId == fid))
        {
            if (item.IsCategory)
            {
                Html("<fieldset>");
                Html("<legend>{0}", item.Name);
                Html("<input type=\"checkbox\" id=\"pms_{0}\" value=\"{0}\" onclick=\"categoryClick(this);\" name=\"Permission\"", item.Id);
                if (selected.Contains("," + item.Id + ","))
                    Html("checked=\"checked\" ");
                Html("/>");
                Html("</legend>");
                RenderSubRecords(data, item.Id, selected);
                Html("</fieldset>");
            }
            else
            {
                Html("<input type=\"checkbox\" id=\"pms_{0}\" value=\"{0}\" onclick=\"nodeClick(this);\" data-father-id=\"{1}\" class=\"father-{1}\" name=\"Permission\"",
                    item.Id, item.FatherId);
                if (selected.Contains("," + item.Id + ","))
                    Html("checked=\"checked\" ");
                Html("/>");
                Html("<label for=\"pms_{1}\">{0}</label>", item.Name, item.Id);
            }
        }
    }

    public void SaveAuthorize()
    {
        string permissionIds = Request.Form["Permission"];
        UserService us = new Nt.BLL.UserService();
        us.SaveAuthorize(permissionIds, level);
        //重新生成组id为level的侧边menu
        us.CreateMenu(level);

    }

}