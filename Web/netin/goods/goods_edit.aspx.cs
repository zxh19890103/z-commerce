using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Nt.Model;
using Nt.Model.View;
using Nt.Model.Common;
using Nt.Model.NtAttribute;
using Nt.Model.Setting;
using Nt.DAL;
using Nt.BLL;
using Nt.Framework;
using Nt.Framework.Admin;

public partial class Netin_Goods_goods_edit : EditBase
{
    protected int goodsClassId = IMPOSSIBLE;
    List<NtListItem> _categories;

    public override string TableName
    {
        get { return "Goods"; }
    }

    public override string ListPagePath
    {
        get { return "goods_list.aspx"; }
    }

    public View_Goods Model { get; set; }

    /// <summary>
    /// 商品分类选项
    /// </summary>
    public List<NtListItem> Categories
    {
        get
        {
            if (_categories == null)
            {
                _categories = DB.GetDropdownlist<Goods_Class>("Display=1");
            }
            return _categories;
        }
    }

    public override void Get()
    {
        if (IsEdit)
        {
            Model = DbAccessor.GetById<View_Goods>(NtID);
            if (Model == null)
                NotFound();
            AppendTitle(Model.Name);
        }
        else
        {
            Int32.TryParse(Request.QueryString["gcid"], out goodsClassId);
            Model = new View_Goods();
            Model.InitData();
            Model.Display = true;
            Model.DisplayOrder = Model.MaxID;
            Model.GoodsClassId = goodsClassId;
            Model.LanguageId = NtContext.Current.LanguageID;
        }
    }

    public override void Post()
    {
        Goods m = new Goods();
        m.InitDataFromPage();
        m.UserId = NtContext.Current.UserID;
        m.UpdatedDate = DateTime.Now;
        DbAccessor.UpdateOrInsert(m);
        Htmlizer.Instance.Category = "3";
        Htmlizer.Instance.GenerateHtml<View_Goods>(m.Id);
        Logger.Log(m.Id > 0, "商品", m.Name);
        Goto("goods_list.aspx?id=" + m.Id + "&page=" + PageIndex, "保存成功!");
    }

    #region Pictures

    /// <summary>
    /// 渲染商品图片(ajax)
    /// </summary>
    [AjaxMethod]
    public void Ajax_RenderPictures()
    {
        int goodsId = 0;
        NtJson.EnsureNumber("id", "参数错误:goodsId", out goodsId);
        RenderPictures(goodsId);
    }

    /// <summary>
    /// 渲染商品图片
    /// </summary>
    /// <param name="goodsId">Id Of Speicified Goods</param>
    void RenderPictures(int goodsId)
    {
        List<View_GoodsPicture> d = DbAccessor.GetList<View_GoodsPicture>("GoodsId=" + goodsId);
        foreach (var item in d)
        {
            Response.Write("<li id=\"img-" + item.Id + "\">");
            Response.Write("<img src=\"" + MediaService.MakeThumbnail(item.Src, 120) + "\"/>");
            Response.Write("<div>");
            Response.Write("<a href=\"javascript:;\" title=\"删除\" class=\"nt-image-del\" onclick=\"uploader.del('" + item.Id + "');\"></a>");
            Response.Write("<a href=\"javascript:;\" title=\"编辑图片信息\" class=\"nt-image-edit\" onclick=\"uploader.editImageInfo('" + item.Id + "');\"></a>");
            Response.Write("<a href=\"javascript:;\" title=\"显示\" class=\"nt-image-eye" + (item.Display ? "" : " nt-image-eye-close") + "\" data-display=\"" + (item.Display ? 1 : 0) + "\" onclick=\"uploader.setDisplay(this,'" + item.Id + "');\"></a>");
            Response.Write("</div>");
            Response.Write("</li>");
        }
    }

    //图片
    public void RenderPictures()
    {
        if (!IsEdit)
        {
            Response.Write("<div class=\"tips\">请先保存，然后才能编辑图片!</div>");
            return;
        }

        Response.Write("<script src=\"/netin/goods/plupload/plupload.full.min.js\" type=\"text/javascript\"></script>");
        Response.Write("<script src=\"/netin/goods/plupload/upload.js\" type=\"text/javascript\"></script>");
        Response.Write("<script type=\"text/javascript\">");
        Response.Write("	$(function(){");
        Response.Write("         new mutiUploader();");
        Response.Write("		uploader.setForeignId(" + Model.Id + ");");
        Response.Write("		$('.tips','#nt_pictures').text(uploader.myConfig.tipsMsg);");
        Response.Write("	}); ");
        Response.Write("</script>");

        Response.Write("<table class=\"adminContent\">");
        Response.Write("	<tr>");
        Response.Write("		<td  class=\"adminData\">");
        Response.Write("			<div id=\"nt_pictures\" class=\"nt-pictures\">");
        Response.Write("			<span class=\"tips\"></span>");
        Response.Write("				<ul id=\"uploadedPics\">");
        RenderPictures(Model.Id);
        Response.Write("				</ul>");
        Response.Write("			</div>");
        Response.Write("		</td>");
        Response.Write("	</tr>");
        Response.Write("	<tr>");
        Response.Write("		<td class=\"adminData\">");
        Response.Write("			<a href=\"javascript:;\" id=\"browse\" title=\"浏览本地图片\"></a>");
        Response.Write("			<a href=\"javascript:;\" id=\"start_upload\" onclick=\"uploader.start();\" title=\"开始上传\"></a>");
        Response.Write("		</td>");
        Response.Write("	</tr>");
        Response.Write("	<tr>");
        Response.Write("		<td class=\"adminData\">");
        Response.Write("			<div class=\"upload-progress\">");
        Response.Write("				<ul id=\"file-list\"></ul>");
        Response.Write("			<a href=\"javascript:;\" id=\"remove_files\" onclick=\"uploader.removeAll();\" title=\"全部清除\">全部清除</a>");
        Response.Write("			<a href=\"javascript:;\" id=\"add_remote_file\" onclick=\"uploader.addRemoteFile();\" title=\"添加网络图片\">添加网络图片</a>");
        Response.Write("			</div>");
        Response.Write("		</td>");
        Response.Write("	</tr>");
        Response.Write("</table>");
    }

    #endregion

    #region 参数表

    void RenderParams(int goodsId)
    {
        List<View_GoodsParameter> d = DbAccessor.GetList<View_GoodsParameter>("GoodsId=" + goodsId, "GoodsParameterGroupId,ID");

        Html("<table class=\"adminListView rowNoHover\"><thead><tr><th>组名</th><th>参数</th></tr></thead>");

        var grouppedD = d.GroupBy(x => x.GoodsParameterGroupId);
        foreach (var group in grouppedD)
        {
            var one = group.FirstOrDefault();
            Row<View_GoodsParameter>(e =>
            {
                Td(one.ParamGroupName);
                Td(() =>
                {
                    BeginTable("参数名", "参数值", "操作");
                    foreach (var item in group)
                    {
                        Row<View_GoodsParameter>(e1 =>
                        {
                            Html("<td>");
                            Html(item.Name);
                            Html("</td>");
                            Html("<td>");
                            Html(NtUtility.GetSubString(item.Value, 25));
                            Html("</td>");
                            Html("<td>");
                            Html("<a href=\"javascript:;\" class=\"edit\" onclick=\"param_edit(" + item.Id + ")\"></a>");
                            Html("<a href=\"javascript:;\" class=\"del\"  onclick=\"param_del(" + item.Id + ")\"></a>");
                            Html("</td>");
                        }, item);
                    }
                    EndTable();
                });
            }, one);
        }

        EndTable(() =>
        {
            Td();
            Td("<a href=\"javascript:;\" onclick=\"param_add();\" class=\"a-button\">添加参数</a>");
        });
    }

    public void RenderParams()
    {
        if (!IsEdit)
        {
            Response.Write("<div class=\"tips\">请先保存，然后才能编辑参数!</div>");
            return;
        }
        RenderParams(Model.Id);
    }

    [AjaxAuthorize(AuthorizeFlag.User)]
    public void Ajax_RenderParams()
    {
        int id = IMPOSSIBLE;
        NtJson.EnsureNumber("id", "参数错误!", out id);
        RenderParams(id);
    }

    #endregion

    #region 属性
    void RenderAttribute(int goodsId)
    {
        var d = DbAccessor.GetList<View_GoodsAttribute>("GoodsId=" + goodsId, "DisplayOrder");

        BeginTable("属性名", "属性类别", "是否必需", "值", "操作");

        var stp = StaticDataProvider.Instance.SpecificationTypeProvider;

        foreach (var item in d)
        {
            Row<View_GoodsAttribute>(e =>
            {
                Td(item.Name);
                Td(FindTextByValue(stp, item.ControlType));
                Td(item.IsRequired ? "是" : "否");
                TdF("<a href=\"javascript:;\" onclick=\"mgrAttributeValues(this, {0}, {1}, {2},'{3}');\">{4}</a>",
                    item.Id, item.ControlType, goodsId, Server.UrlEncode(item.Name), "管理属性值");
                Td(() =>
                {
                    Html("<a href=\"javascript:;\" class=\"edit\" onclick=\"attribute_edit(" + item.Id + ")\"></a>");
                    Html("<a href=\"javascript:;\" class=\"del\"  onclick=\"attribute_del(" + item.Id + ")\"></a>");
                });
            }, item);
        }
        EndTable(() =>
        {
            TdSpan(4);
            Td("<a href=\"javascript:;\" onclick=\"attribute_add();\" class=\"a-button\">添加属性</a>");
        });
    }

    /// <summary>
    /// 规格属性表
    /// </summary>
    public void RenderAttribute()
    {
        if (!IsEdit)
        {
            Response.Write("<div class=\"tips\">请先保存，然后才能编辑属性!</div>");
            return;
        }
        RenderAttribute(Model.Id);
    }

    [AjaxAuthorize(Nt.Framework.AuthorizeFlag.User)]
    public void Ajax_RenderAttribute()
    {
        int id = IMPOSSIBLE;
        NtJson.EnsureNumber("id", "参数错误", out id);
        RenderAttribute(id);
    }

    #endregion

    #region 相关商品

    /// <summary>
    /// 渲染相关商品列表 
    /// </summary>
    /// <param name="goodId">商品id</param>
    public void RenderBindingGoods(int goodId)
    {
        var bindings = DbAccessor.GetList<View_Goods_Binding>("GoodsId=" + goodId);
        BeginTable("商品", "操作");
        foreach (var item in bindings)
        {
            Row<View_Goods_Binding>(e =>
            {
                Td(item.BingGoodsName);
                Td("<a href=\"javascript:;\" onclick=\"delGoodsBinding(" + item.Id + ");\" class=\"del\"></a>");
            }, item);

        }

        EndTable(() =>
        {
            Td("<a href=\"javascript:;\" onclick=\"openWindow({url:'goods_binding.aspx?goodsId=" + goodId + "',target:'goodsbindingWin'});\">添加新的相关商品</a>");
            TdDelSelected();
        });
    }

    /// <summary>
    /// 渲染相关商品列表
    /// </summary>
    public void RenderBindingGoods()
    {
        if (!IsEdit)
        {
            Response.Write("<div class=\"tips\">请先保存，然后才能管理相关商品!</div>");
            return;
        }
        RenderBindingGoods(Model.Id);
    }

    /// <summary>
    /// 渲染相关商品列表 的ajax方法
    /// </summary>
    [AjaxMethod]
    public void Ajax_RenderBindingGoods()
    {
        int id = IMPOSSIBLE;
        NtJson.EnsureNumber("id", "参数错误!", out id);
        RenderBindingGoods(id);
    }

    #endregion

    #region 折扣

    public void RenderDiscount()
    {
        var source = DbAccessor.GetList<Discount>();
        BeginTable("选择", "折扣", "是否使用百分数", "折扣率", "折扣量");

        foreach (var item in source)
        {
            Row<Discount>(e =>
            {

                TdF("<input type=\"radio\" " +
                    (Model.DiscountId == e.Id ? "checked=\"checked\"" : string.Empty) + " name=\"DiscountId\" value=\"{0}\"/>",
                    item.Id);
                Td(item.Name);
                Td(item.UsePercentage ? "Yes" : "No");
                if (item.UsePercentage)
                    TdF("{0}%", (item.DiscountPercentage * 100).ToString("f"));
                else
                    Td(string.Empty);
                if (item.UsePercentage)
                    Td(string.Empty);
                else
                    Td(item.DiscountAmount.ToString("f"));
            }, item);
        }

        EndTable();
    }

    #endregion

    #region Tag

    public void RenderTags()
    {
        Html("<ul class=\"tags\">");
        int tagI = 0;
        foreach (string item in
            Model.Tags.Split(new char[] { ',' },
            StringSplitOptions.RemoveEmptyEntries))
        {
            Html("<li class=\"tag-item\" id=\"tag-item-{1}\"><span>{0}</span>", item, tagI);
            Html("<a href=\"javascript:;\" onclick=\"$('li#tag-item-" + tagI + "').remove();\"   class=\"x-tag\">x</a>", item, tagI);
            Html("<input type=\"hidden\" name=\"Tags\" value=\"{0}\"/></li>", item);
            tagI++;
        }
        Html("</ul>");
        Html("<input type=\"text\" class=\"input-tag\" maxlength=\"100\" id=\"tagEditor\" />");
        Html("<ul class=\"tag-resource\">");
        var tagsResource = DbAccessor.GetList<Goods_Tag>("Display=1", "DisplayOrder desc,CreatedDate desc");
        foreach (var item in tagsResource)
        {
            Html("<li>{0}</li>", item.Tag);
        }
        Html("</ul>");
    }

    #endregion

    #region OtherClass

    /// <summary>
    /// 扩展分类
    /// </summary>
    public void RenderOtherClass()
    {
        NtUtility.ListItemSelect(Categories, Model.OtherClass);
        BeginTable("选择", "分类名");
        foreach (var item in Categories)
        {
            int key = Convert.ToInt32(item.Value);
            Row(() =>
            {
                Td(() =>
                {
                    if (item.Value.Equals(Model.GoodsClassId.ToString()))
                        return;
                    Html("<input type=\"checkbox\" name=\"OtherClass\"");
                    if (item.Selected)
                        Html("checked=\"checked\"");
                    Html(" value=\"{0}\"/>", item.Value);
                });
                Html("<td style=\"padding-left:5px;text-align:left;\">{0}</td>", item.Text);
            });
        }

        EndTable();
    }

    #endregion

    #region Ajax Methods
    /// <summary>
    /// 保存商品参数
    /// </summary>
    [AjaxAuthorize(Nt.Framework.AuthorizeFlag.User)]
    public void SaveParam()
    {
        Goods_Parameter m = new Goods_Parameter();
        m.InitDataFromPage();
        if (m.Id > 0) { DbAccessor.Update(m); }
        else { DbAccessor.Insert(m); }
        NtJson.ShowOK("保存成功!");
    }

    [AjaxAuthorize(Nt.Framework.AuthorizeFlag.User)]
    public void GetGoodsParameter()
    {
        int id = 0;
        int.TryParse(Request["id"], out id);
        var m = DbAccessor.GetById<Goods_Parameter>(id);
        var json = new NtJson(m);
        Response.Write(json);
    }

    /// <summary>
    /// 保存商品属性
    /// </summary>
    [AjaxAuthorize(Nt.Framework.AuthorizeFlag.User)]
    public void SaveAttribute()
    {
        Goods_Attribute_Mapping m = new Goods_Attribute_Mapping();
        m.InitDataFromPage();
        if (m.Id > 0) { DbAccessor.Update(m); }
        else { DbAccessor.Insert(m); }
        NtJson.ShowOK("保存成功!");
    }

    [AjaxAuthorize(Nt.Framework.AuthorizeFlag.User)]
    public void GetGoodsAttribute()
    {
        int id = 0;
        int.TryParse(Request["id"], out id);
        var m = DbAccessor.GetById<Goods_Attribute_Mapping>(id);
        var json = new NtJson(m);
        Response.Write(json);
    }

    [AjaxMethod]
    public void VerifyUnique()
    {
        string goodsGuid = string.Empty;
        NtJson.EnsureNotNullOrEmpty("goodsGuid", "参数错误：goodsGuid", out goodsGuid);
        object thereExists = SqlHelper.ExecuteScalar(
            SqlHelper.GetConnection(),
            CommandType.Text,
            string.Format("Select Name From [{0}] Where GoodsGuid=@GoodsGuid", M(typeof(Goods))), new SqlParameter("@GoodsGuid", goodsGuid));
        if (thereExists != null)
            NtJson.ShowError(string.Format("与商品{0}的货号冲突", thereExists));
        else
            NtJson.ShowOK();
    }

    #endregion

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "商品";
    }

}