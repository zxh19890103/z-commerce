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

public partial class netin_sale_promotion : ListBase
{

    public override string TableName
    {
        get { return "Goods_Promotion"; }
    }

    public override void List()
    {

    }

    public override string EditPagePath
    {
        get { return "promotion.aspx"; }
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "暂未实现";
    }
}