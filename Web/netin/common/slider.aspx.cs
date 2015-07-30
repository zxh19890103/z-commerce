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

public partial class netin_common_slider : ListBase
{
    public override string TableName
    {
        get { return "Slider"; }
    }

    public override string EditPagePath
    {
        get { return "slider_edit.aspx"; }
    }

    public override void List()
    {
        var source = DbAccessor.GetList<Slider>("LanguageId=" + NtContext.Current.LanguageID);

        BeginTable("ID", "图片", "排序", "Url(链接)", "标题", "生效", "操作");

        foreach (var item in source)
        {
            Row<Slider>(e =>
            {
                Td(item.Id);
                TdImage(MediaService.MakeThumbnail(item.PictureUrl, 80, 80));
                TdOrder(item.Id, item.DisplayOrder);
                Td(item.Url);
                Td(item.Title);
                TdSetBoolean(item.Id, item.Display, "Display");
                TdEdit(item.Id);
            }, item);
        }

        EndTable(() =>
        {
            TdSpan(2);
            TdReOrder();
            TdSpan(3);
            Td("<a href=\"slider_edit.aspx\" class=\"a-button\">添加</a>");
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "Slider图管理";
    }
}