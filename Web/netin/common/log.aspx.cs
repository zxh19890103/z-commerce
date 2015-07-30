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

public partial class netin_common_log : ListBase
{
    public override string TableName
    {
        get { return "Log"; }
    }

    public override void List()
    {
        string filter, orderby;
        filter = string.Empty;
        orderby = "CreatedDate desc";

        int total = DbAccessor.GetRecordCount(typeof(Log), filter);
        Pager = new NtPager(total, 12);
        var source = DbAccessor.GetList<View_Log>(filter, orderby, Pager.PageIndex, Pager.PageSize);
        BeginTable("选择", "用户", "IP", "页面Url", "描述", "操作");
        foreach (var item in source)
        {
            Row<View_Log>(e =>
            {
                TdKey(e.Id);
                Td(item.UserName);
                Td(item.LoginIP);
                Td(item.RawUrl);
                Html("<td title=\"{0}\">{1}</td>", item.Description, NtUtility.GetSubString(item.Description, 20));
                Html("<td>");
                DelRowAnchor(item.Id);
                Html("</td>");
            }, item);
        }
        EndTable(() =>
        {
            TdSelectAll();
            TdPager(4);
            TdDelSelected();
        });
    }

    public override string EditPagePath
    {
        get { return "log_view.aspx"; }
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "系统日志";
    }
}