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

public partial class Netin_Customer_list : ListBase
{

    public override string TableName
    {
        get { return "Customer"; }
    }

    public override void List()
    {
        var source = DbAccessor.GetList<View_Customer>();

        BeginTable("选择", "会员名", "所属组", "邮箱", "登录次数", "积分", "启用", "密码管理", "收货人管理", "操作");

        foreach (var item in source)
        {
            Row<View_Customer>(e =>
            {
                TdKey(item.Id);
                Td(item.Name);
                Td(item.RoleName);
                Td(item.Email);
                Td(item.LoginTimes);
                Td(item.Points);
                TdSetBoolean(item.Id, item.Active, "Active");
                Td(() =>
                {
                    Html("<a href=\"javascript:;\" onclick=\"resetPassword({0});\">重置密码</a>", item.Id);
                    Html("<a href=\"javascript:;\" onclick=\"setPassword({0});\">修改密码</a>", item.Id);
                });

                TdAnchor("consignee.aspx?customerId=" + item.Id, "收货人");
                Td(() =>
                {
                    EditRowAnchor(item.Id);
                    DelRowAnchor(item.Id);
                });
            }, item);
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdSpan(8);
            TdAButton("edit.aspx", "添加");
        });

    }

    public override string EditPagePath
    {
        get { return "edit.aspx"; }
    }

    [AjaxMethod]
    public void ResetPassword()
    {
        int id = IMPOSSIBLE;
        string pass;
        NtJson.EnsurePassword("pass", out pass);
        NtJson.EnsureNumber("id", "参数错误:ID", out id);
        CustomerService us = new CustomerService();
        us.ResetPassword(id, pass);
        NtJson.ShowOK();
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "会员列表";
    }

}