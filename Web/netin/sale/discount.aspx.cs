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

public partial class netin_sale_discount : ListBase
{

    public override string TableName
    {
        get { return "Discount"; }
    }

    public override string EditPagePath
    {
        get { return "discount_edit.aspx"; }
    }

    public override void List()
    {
        string filter = string.Empty;

        var source = DbAccessor.GetList<Discount>(filter);

        BeginTable("折扣名", "是否使用折扣率", "折扣率", "折扣量", "操作");

        foreach (var item in source)
        {
            Row<Discount>(e =>
            {
                Td(item.Name);
                TdSetBoolean(item.Id, item.UsePercentage, "UsePercentage");
                Td(((item.DiscountPercentage) * 100).ToString("f"));
                Td(item.DiscountAmount.ToString("f"));
                TdEdit(item.Id);
            }, item);
        }
        EndTable(() =>
        {
            TdSpan(4);
            Td("<a href=\"discount_edit.aspx\" class=\"a-button\">添加</a>");
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "折扣管理";
    }

}