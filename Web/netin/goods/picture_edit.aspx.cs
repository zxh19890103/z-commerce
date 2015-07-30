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

public partial class Netin_Goods_picture_edit : EditBase
{    
    public override string TableName
    {
        get { return "Picture"; }
    }

    public override string ListPagePath
    {
        get { return ""; }
    }

    public override string PermissionSysN
    {
        get
        {
            return string.Empty;
        }
    }

    public Picture Model { get; set; }

    public override void Get()
    {
        Model = DbAccessor.GetById<Picture>(NtID);
        if (Model == null)
            CloseWindow("未发现记录");

        uploader.FieldValue = Model.Src;
    }

    [AjaxMethod]
    public override void Post()
    {
        AjaxSave<Picture>();
    }

}