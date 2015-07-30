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

public partial class netin_user_user_edit : EditBase
{
    public override string TableName
    {
        get { return "User"; }
    }

    public override string ListPagePath
    {
        get { return "users.aspx"; }
    }

    int level;
    View_User _m;
    public View_User Model { get { return _m; } }
    public List<NtListItem> UserLevels { get; set; }
    
    public override void Get()
    {
        UserLevels = DB.GetDropdownlist<UserLevel>("Name", "ID", "Active=1");
        if (UserLevels.Count == 0)
            Goto("userlevel.aspx", "请先添加用户组!");

        if (IsEdit)
        {
            _m = DbAccessor.GetById<View_User>(NtID);
            if (_m == null)
                NotFound();

            uploader.FieldValue = _m.Profile;
        }
        else
        {
            int.TryParse(Request.QueryString["level"], out level);
            _m = new View_User();
            _m.Active = true;
            _m.CreatedDate = DateTime.Now;
            _m.CreatedUserId = NtContext.Current.CurrentUser.Id;
            _m.Deleted = false;
            _m.UserLevelId = level;
        }
    }

    public override void Post()
    {
        var m = new User();
        m.InitDataFromPage();
        if (m.Id > 0)
        {
            DbAccessor.Update(m, "UserName", "Password", "LastLoginIp", "LoginTimes", "LastLoginDate", "CreatedDate");
            Goto("users.aspx", "修改成功!");
        }
        else
        {
            if (!RegexUtility.IsUserName(m.UserName))
                History("用户名格式不正确：以字母开头，4-20位字母或数字!", -1);
            if (!RegexUtility.IsPassword(m.Password))
                History("密码格式不正确：只能输入6-20个字母、数字、下划线!", -1);

            if (!Request.Form["Password2"].Equals(m.Password))
                History("两次密码输入不一致!", -1);

            if (!RegexUtility.IsValidEmail(m.Email))
                History("邮箱格式不正确", -1);

            SecurityService ss = new SecurityService();
            m.Password = ss.Md5(m.Password);
            DbAccessor.Insert(m);
            Goto("users.aspx", "添加成功!");
        }
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "用户";
    }

    public override string PermissionSysN
    {
        get
        {
            return "netin.user.users";
        }
    }
    
}