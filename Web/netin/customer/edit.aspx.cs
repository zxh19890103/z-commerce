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

public partial class Netin_Customer_edit : EditBase
{

    public override string TableName
    {
        get { return "Customer"; }
    }

    public override string ListPagePath
    {
        get { return "list.aspx"; }
    }

    int role;
    View_Customer _m;
    public View_Customer Model { get { return _m; } }
    public List<NtListItem> CustomerRoles { get; set; }

    public override void Get()
    {
        CustomerRoles = DB.GetDropdownlist<CustomerRole>("Name", "ID", "Active=1");
        if (CustomerRoles.Count == 0)
            Goto("role.aspx", "请先添加会员组!");

        if (IsEdit)
        {
            _m = DbAccessor.GetById<View_Customer>(NtID);
            if (_m == null)
                NotFound();
        }
        else
        {
            int.TryParse(Request.QueryString["role"], out role);
            _m = new View_Customer();
            _m.Active = true;
            _m.CreatedDate = DateTime.Now;
            _m.CustomerRoleId = role;
        }
    }

    public override void Post()
    {
        var m = new Customer();
        m.InitDataFromPage();
        if (m.Id > 0)
        {
            DbAccessor.Update(m, "Name", "Points", "Password", "LastLoginIp", "LoginTimes", "LastLoginDate", "CreatedDate");
            Logger.Log(true, "会员信息", m.Name);
            Goto("list.aspx", "修改成功!");
        }
        else
        {
            if (!RegexUtility.IsUserName(m.Name))
                History("用户名格式不正确：以字母开头，4-20位字母或数字!", -1);
            if (!RegexUtility.IsPassword(m.Password))
                History("密码格式不正确：只能输入6-20个字母、数字、下划线!", -1);
            if (!Request.Form["Password2"].Equals(m.Password))
                History("两次密码输入不一致!", -1);

            SecurityService ss = new SecurityService();
            m.Password = ss.Md5(m.Password);
            DbAccessor.Insert(m);
            Logger.Log(false, "会员信息", m.Name);
            Goto("list.aspx", "添加成功!");
        }
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "会员";
    }

}