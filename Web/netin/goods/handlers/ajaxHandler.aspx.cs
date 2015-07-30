using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.Framework;
using System.Reflection;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Nt.DAL;
using Nt.Model;
using Nt.BLL;

public partial class Netin_Goods_handlers_ajaxHandler : OnlyAjaxPage
{
    /// <summary>
    /// id组删除图片
    /// </summary>
    [AjaxMethod]
    public void DelPictures()
    {
        string ids;
        string urlArr;
        bool fine = false;
        MediaService ms = new MediaService();
        if (!string.IsNullOrEmpty((urlArr = Request["urlArr"])))
        {
            NtJson.EnsureStrArray("urlArr", out urlArr);
            foreach (var url in urlArr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                ms.TryDeleteImages(url);
            }
            fine = true;
        }
        NtJson.EnsureInt32Range("ids", out ids);
        if (!fine)
        {
            var list = DbAccessor.GetList<Picture>("ID in (" + ids + ")");
            foreach (var item in list)
            {
                ms.TryDeleteImages(item.Src);
            }
        }
        DbAccessor.Delete("Picture", ids);
        NtJson.ShowOK("删除图片成功!");
    }

    /// <summary>
    /// 按照指定的url删除图片
    /// </summary>
    [AjaxMethod]
    public void DelPictureByAbsUrl()
    {
        string url;
        NtJson.EnsureAbsUrl("url", "图片路径不对", out url);
        MediaService ms = new MediaService();
        ms.TryDeleteImages(url);
    }

}