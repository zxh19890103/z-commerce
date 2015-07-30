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

public partial class netin_guestbook_list : ListBase
{
    public override string TableName
    {
        get { return "GuestBook"; }
    }

    public override string EditPagePath
    {
        get { return "edit.aspx"; }
    }

    public override void List()
    {
        string filter = string.Empty;
        string orderBy = "DisplayOrder desc,ID desc";
        Pager = new NtPager();
        List<View_GuestBook> source = null;
        source = DbAccessor.GetList<View_GuestBook>(filter, orderBy, Pager.PageIndex, Pager.PageSize);
        Pager.RecordCount = DbAccessor.Total;

        BeginTable("选择", "排序", "分类", "已阅读", "标题", "审核", "已回复", "查看回复", "操作");

        foreach (var item in source)
        {
            Row<GuestBook>(e =>
            {
                TdKey(item.Id);
                TdOrder(item.Id, item.DisplayOrder);
                Td(FindTextByValue(StaticDataProvider.Instance.GuestBookTypeProvider, item.Type));
                Td(item.Viewed ? "是" : "否");
                Td(item.Title);
                TdSetBoolean(item.Id, item.Display, "Display");
                Td(item.Replied ? "是(" + item.RepliedCount + ")" : "否");
                TdF("<a href=\"reply.aspx?book={0}&page={1}\">回复</a>",
                    item.Id, Pager.PageIndex);
                TdEdit(item.Id);
            }, item);
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdReOrder();
            TdPager(6);
            TdDelSelected();
        });

    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "游客留言";
    }
}