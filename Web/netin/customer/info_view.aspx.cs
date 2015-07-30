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
using System.IO;

public partial class Netin_Customer_info_view : EditBase
{
    View_Customer _m;
    public View_Customer Model { get { return _m; } }

    public override string PermissionSysN
    {
        get
        {
            return string.Empty;
        }
    }

    public override string TableName
    {
        get { return "Customer"; }
    }

    public override string ListPagePath
    {
        get { return "list.aspx"; }
    }

    public override void Get()
    {
        if (!IsEdit)
            CloseWindow("参数错误!");
        _m = DbAccessor.GetById<View_Customer>(NtID);
        if (_m == null)
            CloseWindow("未发现该记录!");
    }

    public override void Post()
    {
        //free
    }
}