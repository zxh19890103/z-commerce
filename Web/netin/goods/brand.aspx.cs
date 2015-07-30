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

public partial class netin_goods_brand : ListBase<Brand>, IAllAjax
{
    public override void List()
    {
        var source = DbAccessor.GetList<Brand>("LanguageId=" + NtContext.Current.LanguageID, "displayorder");

        BeginTable("选择", "Logo", "排序", "Url(链接)", "品牌名", "是否使用", "操作");

        foreach (var item in source)
        {
            Row<Brand>(e =>
            {
                Td(item.Id);
                Html("<td><img src=\"{0}\" alt=\"{1}\"/></td>", item.PictureUrl, item.Name);
                TdOrder(item.Id, item.DisplayOrder);
                Td(item.Url);
                Td(item.Name);
                TdSetBoolean(item.Id, item.Display, "Display");

                TdEditViaAjax(item.Id);

            }, item);
        }

        EndTable(() =>
        {
            TdSpan(2);
            TdReOrder();
            TdSpan(3);
            Html(TAG_TD_BEGIN);
            AddNewButtonViaAjax();
            Html(TAG_TD_END);
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "品牌信息管理";
    }

    [AjaxMethod]
    public void TMPost()
    {
        Brand m = new Brand();
        m.InitDataFromPage();

        if (Request.Files.Count > 0)
        {
            bool error = false;
            string newSrc = "";
            try
            {
                MediaService.AsyncUpload();
                newSrc = MediaService.FileUrl;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                error = true;
            }
            if (!error)
                m.PictureUrl = newSrc;
        }

        DbAccessor.UpdateOrInsert(m);

        NtJson.ShowOK();
    }

    [AjaxMethod]
    public void TMDel()
    {
        AjaxDel<Brand>();
    }

    [AjaxMethod]
    public void TMGet()
    {
        AjaxGet<Brand>();
    }

    [AjaxMethod]
    public void TMList()
    {
        List();
    }
}