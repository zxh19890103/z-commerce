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

public partial class netin_product_settings : SettingBase<ProductSetting>
{

    protected override void AfterPost()
    {

    }

    protected override void AfterGet()
    {
        uploader.FieldValue = Model.MarkImgUrl;
        Title = "产品设置";
    }

    [AjaxMethod]
    public void HandleMedias()
    {
        int code = 0;
        NtJson.EnsureNumber("code", "参数错误:code", out code);

        if (Request.Params["thumbon"] != null)
        {
            int thumbon = 0;
            NtJson.EnsureNumber("thumbon", "参数错误:thumbon", out thumbon);
            WaterMarker.Instance.CurrentThumbOn = (ThumbOn)thumbon;
        }

        WaterMarker.Instance.Action = code;
        WaterMarker.Instance.LanguageCode = NtContext.Current.CurrentLanguage.LanguageCode;
        WaterMarker.Instance.Run();
        NtJson.ShowOK();
    }

}