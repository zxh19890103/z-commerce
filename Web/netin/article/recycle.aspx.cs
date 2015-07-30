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

public partial class netin_article_recycle : ListBase<View_Article, Article>
{
    protected override string Where
    {
        get
        {
            return "Deleted=1";
        }
    }

    public override void List()
    {
        SetUsePagerOn();

        BeginTable("选择", "标题", "恢复");
        foreach (var item in DataSource)
        {
            Row(() =>
            {
                TdKey(item.Id);
                Td(item.Title);
                Html("<td>");
                Anchor("recovery(" + item.Id + ");", "", "nt-recovery");
                Html("</td>");
            });
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdPager(1);
            Html(TAG_TD_BEGIN);
            Anchor("recovery();", "", "nt-recovery");
            Html(TAG_TD_END);
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "回收站";
    }

}