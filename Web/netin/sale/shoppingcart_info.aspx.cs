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

public partial class netin_sale_shoppingcart_info : EditBase<View_ShoppingCart, ShoppingCartItem>
{
    protected override void Prepare()
    {
        base.Prepare();
        if (!IsEdit)
            CloseWindow("参数错误:Id");
        Title = "购物车详情";
    }
}