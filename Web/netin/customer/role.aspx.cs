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

public partial class Netin_Customer_role : ListBase
{

    protected int id;
    private CustomerRole _m;

    public CustomerRole Model { get { return _m; } }

    public override string TableName
    {
        get { return "CustomerRole"; }
    }

    public override void List()
    {
        var source = DbAccessor.GetList<CustomerRole>();
        BeginTable("ID", "组名", "启用", "添加新组员", "操作");

        foreach (var item in source)
        {

            Row<CustomerRole>(e =>
            {

                Td(item.Id);
                Td(item.Name);
                TdSetBoolean(item.Id, item.Active, "Active");
                TdF("<a href=\"edit.aspx?role={0}\">添加</a>", item.Id);
                Td(() =>
                {
                    Anchor("javascript:openWindow({url:'role_config.aspx?id=" + item.Id + "'});", "", "edit");
                    DelRowAnchor(item.Id);
                });

            }, item);
        }

        EndTable(() =>
        {
            TdSpan(4);
            Td("<a href=\"javascript:openWindow('role_config.aspx');\" class=\"a-button\">添加</a>");
        });
    }

    public override string EditPagePath
    {
        get { return "role.aspx"; }
    }

    protected override void Get()
    {
        int.TryParse(Request.QueryString["id"], out id);
        if (id > 0)
        {
            _m = DbAccessor.GetById<CustomerRole>(id);
        }
        if (_m == null)
        {
            _m = new CustomerRole();
            _m.InitData();
            _m.Active = true;
        }
    }

    protected override void Post()
    {
        var m = new CustomerRole();
        m.InitDataFromPage();
        DbAccessor.UpdateOrInsert(m);
        Logger.Log(m.Id > 0, "会员组", m.Name);
        Response.Redirect("role.aspx", true);
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "会员组列表";
    }
}