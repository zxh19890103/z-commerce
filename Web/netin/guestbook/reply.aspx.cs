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

public partial class netin_guestbook_reply : ListBase<View_GuestBookReply, GuestBookReply>
{

    protected int book = IMPOSSIBLE;

    public void ListBase(int book)
    {
        BeginTable("选择", "排序", "留言标题", "回复人", "回复日期", "公开", "操作");
        foreach (var item in DataSource)
        {
            Row(() =>
            {
                TdKey(item.Id);
                TdOrder(item.Id, item.DisplayOrder);
                Td(item.GuestBookTitle);
                Td(item.ReplyMan);
                Td(item.ReplyDate);
                TdSetBoolean(item.Id, item.Display, "Display");
                Td(() =>
                {
                    Html("<a href=\"javascript:openWindow('reply_config.aspx?id={0}&book={1}');\" class=\"edit\"></a>", item.Id, book);
                    DelRowAnchor(item.Id);
                });
            });
        }
        EndTable(() =>
        {
            TdSelectAll();
            TdReOrder();
            TdSpan(4);
            Td(() =>
            {
                DelSelectedButton();
                AButton(string.Format("javascript:openWindow('reply_config.aspx?book={0}');", book), "添加回复");
            });
        });
    }

    [AjaxMethod]
    public void Ajax_List()
    {
        NtJson.EnsureNumber("book", "参数错误:book", out book);
        ListBase(book);
    }

    [AjaxMethod]
    public override void List()
    {
        if (IsAjaxRequest)
            NtJson.EnsureNumber("book", "参数错误:book", out book);
        ListBase(book);
    }

    protected override string Where
    {
        get
        {
            if (book > IMPOSSIBLE)
                return "GuestBookId=" + book;
            else
                return string.Empty;
        }
    }

    protected override string OrderBy
    {
        get
        {
            return "ReplyDate desc,DisplayOrder desc";
        }
    }

    protected override void Prepare()
    {
        base.Prepare();
        int.TryParse(Request.QueryString["book"], out book);
        if (book == IMPOSSIBLE)
            Goto("list.aspx", "参数错误:book");

        object e = SqlHelper.ExecuteScalar("Select Count(0) From [" + M(typeof(GuestBook)) + "] Where Id=" + book);
        if (Convert.ToInt32(e) == 0)
            Goto("list.aspx", "不存在ID为" + book + "的留言。");

        Title = "留言回复管理";
    }

    public override string PermissionSysN
    {
        get
        {
            return "netin.guestbook.list";
        }
    }

}