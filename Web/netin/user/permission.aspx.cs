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


public partial class netin_user_permission : ListBase
{
    public override string TableName
    {
        get { return "Permission"; }
    }

    public override string EditPagePath
    {
        get { return "Permission_edit.aspx"; }
    }

    void SubList(List<Permission> data, int fid)
    {
        foreach (var item in data.Where(x => x.FatherId == fid))
        {
            Row<Permission>(e =>
            {
                TdKey(item.Id);
                Td(item.Id);
                TdOrder(item.Id, item.DisplayOrder);
                Td(() =>
                {
                    Html(item.Name);
                    if (item.IsCategory)
                    {
                        Html("&nbsp;&nbsp;&nbsp;[");
                        Anchor("permission_edit.aspx?addCategory=true&fatherId=" + item.Id, "添加子目录");
                        Anchor("permission_edit.aspx?addCategory=false&fatherId=" + item.Id, "添加子导航");
                        Html("]");
                    }
                });
                Td(item.SystemName);
                TdSetBoolean(item.Id, item.Display, "Display");
                Td(item.Src);
                TdEdit(item.Id);
            }, item);
            SubList(data, item.Id);
        }
    }

    public override void List()
    {
        var source = DbAccessor.GetList<Permission>("", "DisplayOrder");

        BeginTable("Select", "ID", "Order", "Name", "SystemName", "Display", "Src", "Action");

        SubList(source, 0);

        EndTable(() =>
        {
            TdSelectAll();
            Td();
            TdReOrder();
            TdSpan(4);
            Td("<a href=\"javascript:;\" class=\"a-button\" onclick=\"delSelected();\">删除所选</a><a href=\"permission_edit.aspx?addCategory=true&fatherId=0\" class=\"a-button\">添加根目录</a>");
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "权限项管理";
    }

    [AjaxMethod]
    public void DelMenuCache()
    {
        string path = MapPath("/app_data/menu/");
        if (Directory.Exists(path))
            foreach (string file in Directory.GetFiles(path))
                File.Delete(file);
        NtJson.ShowOK();
    }
}