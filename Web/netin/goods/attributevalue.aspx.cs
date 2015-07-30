using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.Framework;
using System.Data.SqlClient;
using System.Data;
using Nt.Model;
using Nt.DAL;
using Nt.Model.View;
using Nt.Framework.Admin;
using Nt.BLL;

public partial class Netin_Goods_attributevalue : ListBase
{

    protected int id;//属性值id
    protected int attrid;//属性id
    protected int t;//属性展现形式
    protected int gid;//产品id
    protected string attrname;//属性的名称

    protected int valueCount = 0;//记录个数

    public override string TableName
    {
        get { return "Goods_VariantAttributeValue"; }
    }

    public override string EditPagePath
    {
        get { return "attributevalue.aspx"; }
    }

    public override string PermissionSysN
    {
        get
        {
            return "netin.goods.attribute";
        }
    }

    public override void List()
    {
        var source = DbAccessor.GetList<View_GoodsVariantAttributeValue>("GoodsVariantAttributeId=" + attrid, "DisplayOrder desc");

        BeginTable("排序", "值名称", "属性值", "价格微调量(RMB)", "预选", "操作");

        foreach (var item in source)
        {

            Row<View_GoodsVariantAttributeValue>(e =>
            {
                TdOrder(item.Id, item.DisplayOrder);
                Td(item.Name);
                if (item.AttributeValueTypeId == Goods_Attribute_Mapping.CT_COLORSQUARES)
                    Td("<div style=\"width:30px;height:20px;background-color:#" + item.AttributeValue + "\"></div>");
                else
                    Td(item.AttributeValue);
                Td(item.PriceAdjustment.ToString("f"));
                TdSetBoolean(item.Id, item.Selected, "Selected");
                Td(() =>
                {
                    string editPath = string.Format("attributevalue.aspx?id={0}&attrid={1}&t={2}&gid={3}", item.Id, attrid, t, gid);
                    Html("<a href=\"{0}\" class=\"edit\"></a>", editPath);
                    Html("<a href=\"javascript:;\" class=\"del\"  onclick=\"delRow({0})\"></a>", e.Id);
                });

            }, item);
        }

        EndTable(() =>
        {
            TdReOrder();
            TdSpan(4);
            TdF("<a href=\"attributevalue.aspx?attrid={0}&t={1}&gid={2}\" class=\"a-button\">添加</a>", attrid, t, gid);
        });


        valueCount = source.Count;
    }

    View_GoodsVariantAttributeValue _model;
    public View_GoodsVariantAttributeValue Model
    {
        get
        {
            return _model;
        }
    }

    protected override void Get()
    {
        if (!Int32.TryParse(Request.QueryString["attrid"], out attrid))
            CloseWindow("参数错误!:attrid");

        if (!Int32.TryParse(Request.QueryString["t"], out t))
            CloseWindow("参数错误!:t");

        if (!Int32.TryParse(Request.QueryString["gid"], out gid))
            CloseWindow("参数错误!:gid");

        attrname = Request.QueryString["attrname"];

        if (!string.IsNullOrEmpty(attrname))
            attrname = Server.UrlDecode(attrname);

        Int32.TryParse(Request.QueryString["id"], out id);

        if (id > 0)
        {
            _model = DbAccessor.GetById<View_GoodsVariantAttributeValue>(id);
        }

        if (_model == null)
        {
            _model = new View_GoodsVariantAttributeValue();
            _model.InitData();
            _model.GoodsVariantAttributeId = attrid;
            _model.AssociatedGoodsId = gid;
            _model.AttributeName = attrname;
            _model.AttributeValueTypeId = t;
        }
    }

    protected override void Post()
    {
        Goods_VariantAttributeValue m = new Goods_VariantAttributeValue();
        m.InitDataFromPage();
        if (m.Id > 0)
            DbAccessor.Update(m);
        else
            DbAccessor.Insert(m);
        Response.Redirect(Request.Url.PathAndQuery, true);
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "属性值管理";
    }

}