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


public partial class netin_user_users : ListBase
{
    public override string TableName
    {
        get { return "User"; }
    }

    public override string EditPagePath
    {
        get { return "user_edit.aspx"; }
    }

    public override void List()
    {
        var us = new UserService();
        var user = NtContext.Current.CurrentUser;
        var source = DbAccessor.GetList<View_User>(
            string.Format("id in ({0})", us.GetAllSubUser(user.Id)));

        BeginTable("ID", "所属组", "用户名", "邮箱", "登录次数", "启用", "密码管理", "操作");

        foreach (var item in source)
        {
            Row<View_User>(e =>
            {
                Td(item.Id);
                Td(item.LevelName);
                Td(item.UserName);
                Td(item.Email);
                Td(item.LoginTimes);
                TdSetBoolean(item.Id, item.Active, "Active");
                Td(string.Format("<a href=\"javascript:;\" onclick=\"resetPassword({0});\">重置密码</a>", item.Id),
                    string.Format("<a href=\"javascript:;\" onclick=\"setPassword({0});\">修改密码</a>", item.Id));
                TdEdit(item.Id);
            }, item);
        }
        EndTable(() =>
        {
            TdSpan(7);
            Td("<a href=\"user_edit.aspx\" class=\"a-button\">添加</a>");
        });
    }

    [AjaxMethod]
    public void ResetPassword()
    {
        int id = IMPOSSIBLE;
        string pass;
        NtJson.EnsurePassword("pass", out pass);
        NtJson.EnsureNumber("id", "参数错误:ID", out id);
        UserService us = new UserService();
        us.ResetPassword(id, pass);
        NtJson.ShowOK();
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "管理员列表";
    }

}