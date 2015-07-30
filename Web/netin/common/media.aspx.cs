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

public partial class netin_common_media : ListBase<Picture>, IAllAjax
{
    public override void List()
    {
        SetUsePagerOn();

        BeginTable("ID", "Src", "Title", "Alt", "Display", "DisplayOrder", "Description", "操作");

        foreach (var item in DataSource)
        {
            Row(() =>
            {
                Td(item.Id);
                TdF("<img src=\"{1}\" title=\"{0}\" width=\"80\" height=\"80\" alt=\"{0}\"/><br/><a href=\"{0}\" target=\"_blank\">{0}</a>", item.Src, MediaService.MakeThumbnail(item.Src, 80, 80, ThumbnailGenerationMode.HW));
                Td(item.Title);
                Td(item.Alt);
                Td(item.Display);
                TdOrder(item.Id, item.DisplayOrder);
                Html("<td>{0}</td>", NtUtility.GetSubString(item.Description, 20));
                TdEditViaAjax(item.Id);
            });
        }
        EndTable(() =>
        {
            Td();
            TdPager(4);
            TdReOrder();
            Td();
            Html(TAG_TD_BEGIN);
            AddNewButtonViaAjax();
            Html(TAG_TD_END);
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "图片管理";
    }

    [AjaxMethod]
    public void TMPost()
    {
        Picture m = new Picture();
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
                m.Src = newSrc;
        }

        DbAccessor.UpdateOrInsert(m);

        NtJson.ShowOK();

    }

    [AjaxMethod]
    public void TMDel()
    {
        AjaxDel<Picture>();
    }

    [AjaxMethod]
    public void TMGet()
    {
        AjaxGet<Picture>();
    }

    [AjaxMethod]
    public void TMList()
    {
        List();
    }

  

}