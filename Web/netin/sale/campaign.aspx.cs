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

public partial class netin_sale_campaign : ListBase
{
    public Campaign Model { get; set; }

    public override string TableName
    {
        get { return "Campaign"; }
    }

    public override string EditPagePath
    {
        get { return "Campaign_edit.aspx"; }
    }

    [AjaxMethod]
    public override void List()
    {
        List<Campaign> source = DbAccessor.GetList<Campaign>();
        BeginTable("选择", "ID", "名称", "主题", "添加日期", "操作");

        foreach (var item in source)
        {
            Row<Campaign>(e =>
            {
                TdKey(item.Id);
                Td(item.Id);
                Td(item.Name);
                Td(item.Subject);
                Td(item.CreatedDate);
                Td(() =>
                {
                    Anchor("javascript:openWindow({url:'campaign_edit.aspx?id=" + item.Id + "',width:1000});", "", "edit");
                    DelRowAnchor(item.Id);
                });
            }, item);
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdSpan(4);
            Td(() =>
            {
                Html("<a href=\"javascript:openWindow({url:'campaign_edit.aspx',width:1000});\" class=\"a-button\">添加</a>");
                Html("<a href=\"javascript:;\" onclick=\"delSelected();\" class=\"a-button\">删除所选</a>");
            });
        });

    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "活动管理";
    }
}