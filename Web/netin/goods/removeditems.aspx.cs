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
using Nt.Model.Common;

public partial class netin_goods_removeditems : ListBase<View_Goods, Goods>
{
    protected override string Where
    {
        get
        {
            return "Removed=1";
        }
    }

    protected override string OrderBy
    {
        get
        {
            return "DisplayOrder desc,CreatedDate desc";
        }
    }

    public override void List()
    {
        SetUsePagerOn(30);
        BeginTable("选择", "图片", "分类", "商品名", "操作");
        foreach (var item in DataSource)
        {
            Row(() =>
            {
                TdKey(item.Id);
                TdImage(MediaService.MakeThumbnail(item.PictureUrl, 80, 80));
                Td(item.ClassName);
                Td(item.Name);
                Html(TAG_TD_BEGIN);
                EditRowAnchor(item.Id);
                DelRowAnchor(true, item.Id);
                Anchor("putOnOrOffSale(" + item.Id + ");", "", "nt-onsale");
                Html(TAG_TD_END);
            });
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdPager(3);
            Html(TAG_TD_BEGIN);
            AButton("putOnOrOffSale();", "上架选择项");
            DelSelectedButton(true);
            Html(TAG_TD_END);
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "下架商品管理";
    }
}