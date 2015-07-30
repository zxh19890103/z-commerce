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

public partial class netin_sale_discount_edit : EditBase
{

    public Discount Model { get; set; }

    public override string TableName
    {
        get { return "Discount"; }
    }

    public override string PermissionSysN
    {
        get
        {
            return "netin.sale.discount";
        }
    }

    public override string ListPagePath
    {
        get { return "discount.aspx"; }
    }

    public override void Get()
    {

        if (IsEdit)
        {
            Model = DbAccessor.GetById<Discount>(NtID);
            if (Model == null)
                NotFound();
        }
        else
        {
            Model = new Discount();
            Model.InitData();
        }
    }

    public override void Post()
    {
        var m = new Discount();
        m.InitDataFromPage();
        if (m.Id > 0)
            DbAccessor.Update(m);
        else
            DbAccessor.Insert(m);
        Goto(ListPagePath, "保存成功!");
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "折扣信息";
    }
}