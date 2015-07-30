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

public partial class netin_product_edit : EditBase<View_Product, Product>
{

    protected int cid = IMPOSSIBLE;
    List<NtListItem> _categories;

    /// <summary>
    /// 产品分类选项
    /// </summary>
    public List<NtListItem> Categories
    {
        get
        {
            if (_categories == null)
            {
                _categories = DB.GetDropdownlist<ProductCategory>("Display=1");
            }
            return _categories;
        }
    }

    public override void Get()
    {
        if (IsEdit)
        {
            Model = DbAccessor.GetById<View_Product>(NtID);
            if (Model == null)
                NotFound();
            uploader.FieldValue = Model.PictureUrl;
            uploader1.FieldValue = Model.Thumb;
            uploaderfile.FileUrl = Model.FileUrl;
            uploaderfile.FileSize = Model.FileSize;
            uploaderfile.FileName = Model.FileName;
            AppendTitle(Model.Name);
        }
        else
        {
            cid = ParseInt32("cid");
            Model = new View_Product();
            Model.InitData();
            Model.Display = true;
            Model.DisplayOrder = Model.MaxID;
            Model.ProductCategoryId = cid;
            Model.LanguageId = NtContext.Current.LanguageID;
        }
    }

    public override void Post()
    {
        Product m = new Product();
        m.InitDataFromPage();
        m.UserId = NtContext.Current.UserID;
        m.UpdatedDate = DateTime.Now;
        DbAccessor.UpdateOrInsert(m);
        Htmlizer.Instance.Category = "2";
        Htmlizer.Instance.GenerateHtml<View_Product>(m.Id);
        Logger.Log(m.Id > 0, "产品", m.Name);
        Goto("list.aspx?id=" + m.Id + "page=" + PageIndex, "保存成功!");
    }

    public override string TableName
    {
        get
        {
            return "Product";
        }
    }

    #region Pictures

    /// <summary>
    /// 渲染Product图片(ajax)
    /// </summary>
    [AjaxMethod]
    public void Ajax_RenderPictures()
    {
        int productId = 0;
        NtJson.EnsureNumber("id", "参数错误:productId", out productId);
        RenderPictures(productId);
    }

    /// <summary>
    /// 渲染Product图片
    /// </summary>
    /// <param name="goodsId">Id Of Speicified Product</param>
    void RenderPictures(int productId)
    {
        List<View_ProductPicture> d = DbAccessor.GetList<View_ProductPicture>("productId=" + productId);
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
        Response.Write("         new mutiUploader({url2upload:'handlers/pluploadHandler.aspx',keyOfForeignId:'productId'});");
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

    /// <summary>
    /// 参数
    /// </summary>
    public void RenderMutiParams()
    {
        Html("<table class=\"adminContent\">");
        Dictionary<int, string> dict = new Dictionary<int, string>();

        dict.Add(0, "展会时间");
        dict.Add(1, "展会时间1");
        dict.Add(2, "展会时间");
        dict.Add(3, "展会时间");
        dict.Add(4, "展会时间");
        dict.Add(5, "展会时间");
        dict.Add(6, "展会时间");
        dict.Add(7, "展会时间");
        dict.Add(8, "展会时间");
        dict.Add(9, "展会时间");
        dict.Add(10, "展会时间");
        dict.Add(11, "展会时间");
        dict.Add(12, "展会时间");
        dict.Add(13, "展会时间");
        dict.Add(14, "展会时间");
        dict.Add(15, "展会时间");

        var t = Model.GetType();
        foreach (var item in dict)
        {
            var p = t.GetProperty("F" + item.Key);
            if (p == null) continue;
            Html("<tr>");
            Html(" <td class=\"adminTitle\">{0}:</td>", item.Value);
            Html("<td class=\"adminData\"><input class=\"input-long\" name=\"F{1}\" maxlength=\"256\" type=\"text\" value=\"{0}\"/></td>", p.GetValue(Model, null), item.Value);
            Html("</tr>");
        }
        Html("</table>");
    }


    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "产品";
    }

}