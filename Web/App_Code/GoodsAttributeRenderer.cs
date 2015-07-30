using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nt.Model;
using Nt.Model.View;
using Nt.BLL;
using Nt.DAL;

/// <summary>
/// GoodsAttributeRenderer 的摘要说明
/// </summary>
public class GoodsAttributeRenderer
{

    int goodsid = 0;
    string attributeInfoJSArray = string.Empty;

    HttpResponse Response;
    DbAccessor Db;

    
    public GoodsAttributeRenderer(int goodsid)
    {
        this.goodsid = goodsid;
        Response = WebHelper.Response;
        Db = new DbAccessor();
        Db.List("View_GoodsParameter", "GoodsId=" + goodsid);
        Db.List("View_GoodsVariantAttributeValue", "AssociatedGoodsId=" + goodsid);
        Db.List("View_GoodsPicture", "GoodsId=" + goodsid + " and display=1", "displayorder");
        Db.Execute();
    }

    /// <summary>
    /// 商品id
    /// </summary>
    public int GoodsId { get { return goodsid; } set { goodsid = value; } }

    /// <summary>
    ///  GoodsVariantAttributeId,AttributeValueTypeId
    /// </summary>
    public string AttributeInfoJSArray { get { return attributeInfoJSArray; } }

    /// <summary>
    /// 渲染参数
    /// </summary>
    public void RenderParameters()
    {
        if (goodsid == 0) return;

        var data = new List<View_GoodsParameter>();
        DbAccessor.FetchListByDataTable<View_GoodsParameter>(Db[0], data, typeof(View_GoodsParameter));

        var grouppedD = data.GroupBy(x => x.GoodsParameterGroupId);

        Response.Write("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"border-left:1px solid #bbb;border-top:1px solid #bbb;width:100%;font-size:12px;color:#989898;\">");

        Response.Write("<colgroup>");
        Response.Write("<col width=\"120\"/>");
        Response.Write("<col width=\"363\"/>");
        Response.Write("</colgroup>");

        foreach (var group in grouppedD)
        {
            var first = group.FirstOrDefault();
            Response.Write("<tr >");
            Response.Write("<td style=\"border-right:1px solid #bbb;border-bottom:1px solid #bbb;text-align:center;\">");
            Response.Write(first.ParamGroupName);
            Response.Write("</td>");

            Response.Write("<td style=\"border-right:1px solid #bbb;border-bottom:1px solid #bbb;\">");
            Response.Write("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"");
            Response.Write("<colgroup>");
            Response.Write("<col width=\"100\"/>");
            Response.Write("<col width=\"263\"/>");
            Response.Write("</colgroup>");
            foreach (var item in group)
            {
                Response.Write("<tr>");
                Response.Write("<td style=\"padding-left:10px;\">");
                Response.Write(item.Name);
                Response.Write(":</td>");
                Response.Write("<td style=\"padding-left:10px;text-indent:2em;\">");
                Response.Write(item.Value);
                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("</td>");
            Response.Write("</tr>");
        }

        Response.Write("</table>");
    }

    /// <summary>
    /// 渲染规格属性
    /// </summary>
    public void RenderAttribute()
    {
        if (goodsid == 0) return;

        var data = new List<View_GoodsVariantAttributeValue>();
        DbAccessor.FetchListByDataTable<View_GoodsVariantAttributeValue>(Db[1], data, typeof(View_GoodsVariantAttributeValue));

        var grouppedData = data.GroupBy(x => x.GoodsVariantAttributeId);

        attributeInfoJSArray = "[";
        int i = 0;
        foreach (var group in grouppedData)
        {
            var first = group.FirstOrDefault(x => x.Selected);
            if (first == null)
                continue;
            if (i > 0)
                attributeInfoJSArray += ",";
            attributeInfoJSArray += "" + group.Key + "," + first.AttributeValueTypeId;
            switch (first.AttributeValueTypeId)
            {
                case Goods_Attribute_Mapping.CT_CHECKBOXES:
                    AttrRenderCheckboxList(group);
                    break;
                case Goods_Attribute_Mapping.CT_RADIOBUTTONLIST:
                    AttrRenderRadioButtonList(group);
                    break;
                case Goods_Attribute_Mapping.CT_COLORSQUARES:
                    AttrRenderColorSelector(group);
                    break;
                case Goods_Attribute_Mapping.CT_DROPDOWNLIST:
                    AttrRenderDropdownList(group);
                    break;
                case Goods_Attribute_Mapping.CT_MUTILINETEXTBOX:
                    AttrRenderMutiLineText(group);
                    break;
                case Goods_Attribute_Mapping.CT_TEXTBOX:
                    AttrRenderSingleLineText(group);
                    break;
                case Goods_Attribute_Mapping.CT_FILEUPLOAD:
                    break;
                case Goods_Attribute_Mapping.CT_DATEPICKER:
                    break;
            }
            i++;
        }

        attributeInfoJSArray += "]";
    }

    /// <summary>
    /// 获取图片
    /// </summary>
    /// <returns></returns>
    public List<View_GoodsPicture> GetGoodsPictures()
    {
        var data = new List<View_GoodsPicture>();
        DbAccessor.FetchListByDataTable<View_GoodsPicture>(Db[2], data, typeof(View_GoodsPicture));
        return data;
    }


    #region Render Attribute

    /// <summary>
    /// 颜色
    /// </summary>
    void AttrRenderColorSelector(IGrouping<int, View_GoodsVariantAttributeValue> group)
    {
        if (group == null || group.Count() < 1) return;
        View_GoodsVariantAttributeValue first = group.FirstOrDefault(x => x.Selected);
        string id = "attriControl_" + group.Key;
        Response.Write("<div class=\"products-attr attr-colors\">");
        Response.Write("<span>");
        Response.Write(first.AttributeName);
        Response.Write(":</span>");
        Response.Write("<ul id=\"ul_");
        Response.Write(group.Key);
        Response.Write("\">");
        int i = 0, j = 0;
        foreach (var item in group)
        {
            Response.Write("<li");
            if (item.Selected)
            {
                Response.Write(" class=\"active\"");
                j = i;
            }
            Response.Write("><a href=\"javascript:;\" title=\"");
            Response.Write(item.Name);
            Response.Write("\" onclick=\"var tar=G('");
            Response.Write(id);
            Response.Write("');tar.value=");
            Response.Write(item.Id);
            Response.Write(";tar.setAttribute('data-attr-label','");
            Response.Write(item.Name);
            Response.Write("');var ul=G('ul_");
            Response.Write(group.Key);
            Response.Write("');var i=parseInt(tar.getAttribute('data-attr-active'));ul.children[");
            Response.Write(i);
            Response.Write("].className='active';ul.children[i].className='';tar.setAttribute('data-attr-active',");
            Response.Write(i);
            Response.Write(");tar.setAttribute('data-attr-adjustment','");
            Response.Write(item.PriceAdjustment);
            Response.Write("')\"><span style=\"background-color:#");
            Response.Write(item.AttributeValue);
            Response.Write("\"></span></a>");
            Response.Write("</li>");
            i++;
        }
        Response.Write("</ul>");
        Response.Write("<input type=\"hidden\" data-attr-active=\"");
        Response.Write(j);
        Response.Write("\" data-attr-label=\"");
        Response.Write(first.Name);
        Response.Write("\" data-attr-name=\"");
        Response.Write(first.AttributeName);
        Response.Write("\" value=\"");
        Response.Write(first.Id);
        Response.Write("\" id=\"");
        Response.Write(id);
        Response.Write("\" data-attr-adjustment=\"");
        Response.Write(first.PriceAdjustment);
        Response.Write("\" name=\"");
        Response.Write(id);
        Response.Write("\"/>");
        Response.Write("</div>");
    }

    /// <summary>
    /// 复选框
    /// </summary>
    void AttrRenderCheckboxList(IGrouping<int, View_GoodsVariantAttributeValue> group)
    {
        if (group == null || group.Count() < 1) return;
        View_GoodsVariantAttributeValue first = group.FirstOrDefault(x => x.Selected);
        string id = "attriControl_" + group.Key;
        Response.Write("<div class=\"products-attr attr-checkbox\">");
        Response.Write("<span>");
        Response.Write(first.AttributeName);
        Response.Write(":</span><span>");
        foreach (var item in group)
        {
            Response.Write("<input");
            if (item.Selected)
                Response.Write(" checked");
            Response.Write(" value=\"");
            Response.Write(item.Id);
            Response.Write("\" type=\"checkbox\" data-attr-label=\"");
            Response.Write(item.Name);
            Response.Write("\" data-attr-name=\"");
            Response.Write(first.AttributeName);
            Response.Write("\" name=\"");
            Response.Write(id);
            Response.Write("\" id=\"attriValue_");
            Response.Write(item.Id);
            Response.Write("\"/>");
            Response.Write("<label for=\"attriValue_");
            Response.Write(item.Id);
            Response.Write("\">");
            Response.Write(item.AttributeValue);
            Response.Write("</label>");
        }
        Response.Write("</span></div>");

    }

    /// <summary>
    /// 单选框
    /// </summary>
    void AttrRenderRadioButtonList(IGrouping<int, View_GoodsVariantAttributeValue> group)
    {
        if (group == null || group.Count() < 1) return;
        View_GoodsVariantAttributeValue first = group.FirstOrDefault(x => x.Selected);
        string id = "attriControl_" + group.Key;
        Response.Write("<div class=\"products-attr attr-radio\">");
        Response.Write("<span>");
        Response.Write(first.AttributeName);
        Response.Write(":</span><span>");
        foreach (var item in group)
        {
            Response.Write("<input");
            if (item.Selected)
                Response.Write(" checked=\"checked\"");
            Response.Write(" value=\"");
            Response.Write(item.Id);
            Response.Write("\" type=\"radio\" name=\"");
            Response.Write(id);
            Response.Write("\" id=\"attriValue_");
            Response.Write(item.Id);

            Response.Write("\" onclick=\"var tar=G('");
            Response.Write(id);
            Response.Write("');tar.value=");
            Response.Write(item.Id);
            Response.Write(";tar.setAttribute('data-attr-label','");
            Response.Write(item.Name);
            Response.Write("');tar.setAttribute('data-attr-adjustment','");
            Response.Write(item.PriceAdjustment);
            Response.Write("\"/>");

            Response.Write("<label onclick=\"var tar=G('");
            Response.Write(id);
            Response.Write("');tar.value=");
            Response.Write(item.Id);
            Response.Write(";tar.setAttribute('data-attr-label','");
            Response.Write(item.Name);
            Response.Write("');tar.setAttribute('data-attr-adjustment','");
            Response.Write(item.PriceAdjustment);
            Response.Write("\" for=\"attriValue_");
            Response.Write(item.Id);
            Response.Write("\">");
            Response.Write(item.AttributeValue);
            Response.Write("</label>");

        }
        Response.Write("</span>");
        Response.Write("<input type=\"hidden\" data-attr-label=\"");
        Response.Write(first.Name);
        Response.Write("\" data-attr-name=\"");
        Response.Write(first.AttributeName);
        Response.Write("\" value=\"");
        Response.Write(first.Id);
        Response.Write("\" id=\"");
        Response.Write(id);
        Response.Write("\" data-attr-adjustment=\"");
        Response.Write(first.PriceAdjustment.ToString("f"));
        Response.Write("\" name=\"");
        Response.Write(id);
        Response.Write("\"/>");
        Response.Write("</div>");
    }

    /// <summary>
    /// 下拉框
    /// </summary>
    void AttrRenderDropdownList(IGrouping<int, View_GoodsVariantAttributeValue> group)
    {
        if (group == null || group.Count() < 1) return;
        View_GoodsVariantAttributeValue first = group.FirstOrDefault(x => x.Selected);
        string id = "attriControl_" + group.Key;
        Response.Write("<div class=\"products-attr attr-select\">");
        Response.Write("<span>");
        Response.Write(first.AttributeName);
        Response.Write(":</span><select data-attr-label=\"");
        Response.Write(first.Name);
        Response.Write("\"");
        Response.Write(" data-attr-adjustment=\"");
        Response.Write(first.PriceAdjustment);
        Response.Write("\"");
        Response.Write(" data-attr-name=\"");
        Response.Write(first.AttributeName);
        Response.Write("\" onchange=\"var option=this.options[this.selectedIndex];this.setAttribute('data-attr-label',option.getAttribute('data-attr-label'));this.setAttribute('data-attr-adjustment',option.getAttribute('data-attr-ajustment'));\" id=\"");
        Response.Write(id);
        Response.Write("\">");
        foreach (var item in group)
        {
            Response.Write("<option");
            if (item.Selected)
                Response.Write(" selected=\"selected\"");
            Response.Write(" data-attr-adjustment=\"");
            Response.Write(item.PriceAdjustment);
            Response.Write("\"");
            Response.Write(" value=\"");
            Response.Write(item.Id);
            Response.Write("\">");
            Response.Write(item.AttributeValue);
            Response.Write("</option>");
        }
        Response.Write("</select></div>");
    }

    /// <summary>
    /// 单行文本
    /// </summary>
    void AttrRenderSingleLineText(IGrouping<int, View_GoodsVariantAttributeValue> group)
    {
        if (group == null || group.Count() < 1) return;
        View_GoodsVariantAttributeValue first = group.FirstOrDefault(x => x.Selected);
        string id = "attriControl_" + group.Key;
        //基本不用
        Response.Write("<div class=\"products-attr attr-sinleline-text\">");
        Response.Write("<span>");
        Response.Write(first.AttributeName);
        Response.Write(":</span><p>");
        foreach (var item in group)
        {
            Response.Write(item.Name);
            Response.Write(":");
            Response.Write(item.AttributeValue);
            Response.Write(";");
        }
        Response.Write("</p></div>");
    }

    /// <summary>
    /// 多行文本
    /// </summary>
    void AttrRenderMutiLineText(IGrouping<int, View_GoodsVariantAttributeValue> group)
    {
        if (group == null || group.Count() < 1) return;
        View_GoodsVariantAttributeValue first = group.FirstOrDefault(x => x.Selected);
        string id = "attriControl_" + group.Key;
        //基本不用
        Response.Write("<div class=\"products-attr attr-sinleline-text\">");
        Response.Write("<span>");
        Response.Write(first.AttributeName);
        Response.Write(":</span><p>");
        foreach (var item in group)
        {
            Response.Write(item.Name);
            Response.Write(":");
            Response.Write(item.AttributeValue);
            Response.Write("<br/>");
        }
        Response.Write("</p></div>");
    }

    #endregion

}