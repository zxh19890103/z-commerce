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


public partial class netin_single_pages : ListBase
{
    public override string TableName
    {
        get { return "SinglePage"; }
    }

    public override string EditPagePath
    {
        get { return "edit.aspx"; }
    }

    public override void List()
    {
        var source = DbAccessor.GetList<SinglePage>();

        BeginTable("ID", "标题", "操作", "ID", "标题", "操作");

        SinglePage item = null;
        int i = 0;
        bool even = true;
        for (; i < source.Count; i++)
        {
            even = i % 2 == 0;
            item = source[i];
            if (even)
                Html("<tr>");
            Td(item.Id);
            Td(item.Title);
            TdEdit(item.Id);
            if (!even)
                Html("</tr>");
        }

        //补充三个单元格完成一行
        if (i % 2 == 1)
        {
            TdSpan(3);
            Html("</tr>");
        }

        EndTable(() =>
        {
            Html("<td colspan=\"6\">");
            Html("<a href=\"edit.aspx\" class=\"a-button\">添加</a>");
            Html("</td>");
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "二级页面管理";
    }
}