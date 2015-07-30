using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.BLL;
using Nt.DAL;
using Nt.Model;
using Nt.Framework;
using Nt.Framework.Admin;
using Nt.Model.View;

public partial class netin_goods_review_edit : EditBase
{

    public View_Goods_Review Model { get; set; }

    public override string PermissionSysN
    {
        get
        {
            return "netin.goods.review";
        }
    }

    public override string TableName
    {
        get { return "Goods_Review"; }
    }

    public override string ListPagePath
    {
        get { return "review.aspx"; }
    }

    public override void Get()
    {
        if (IsEdit)
        {
            Model = DbAccessor.GetById<View_Goods_Review>(NtID);
            if (Model == null)
                NotFound();
            AppendTitle(Model.Title);
        }
        else
        {
            Model = new View_Goods_Review();
            Model.InitData();
        }
    }

    public override void Post()
    {
        Goods_Review m = new Goods_Review();
        m.InitDataFromPage();
        DbAccessor.UpdateOrInsert(m);
        Goto(string.Format("review.aspx?id={0}&page={1}", m.Id, PageIndex), "保存成功!");
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "商品评论";
    }

}