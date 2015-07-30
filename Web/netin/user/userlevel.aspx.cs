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

public partial class netin_user_userlevel : ListBase
{
    protected int id;

    public override string TableName
    {
        get { return "UserLevel"; }
    }

    public override string EditPagePath
    {
        get { return "userlevel_edit.aspx"; }
    }
    
    [AjaxMethod]
    public override void List()
    {
        var user = NtContext.Current.CurrentUser;
        var source = DbAccessor.GetList<UserLevel>("id>=" + user.UserLevelId);

        BeginTable("ID", "组名", "启用", "添加组员", "操作");

        foreach (var item in source)
        {
            Row<UserLevel>(e =>
            {
                Td(item.Id);
                Td(item.Name);
                TdSetBoolean(item.Id, item.Active, "Active");
                TdF("<a href=\"user_edit.aspx?level={0}\">添加新组员</a>", item.Id);
                Td(() =>
                {
                    Anchor("javascript:openWindow({url:'userlevel_edit.aspx?id=" + item.Id + "'});", "", "edit");
                    DelRowAnchor(item.Id);
                });
            }, item);
        }

        EndTable(() =>
        {
            TdSpan(4);
            Td("<a href=\"javascript:openWindow({url:'userlevel_edit.aspx'});\" class=\"a-button\">添加</a>");
        });

    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "会员组管理";
    }

}