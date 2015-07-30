using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.BLL;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Nt.Model.Setting;
using Nt.Model.Enum;
using Nt.Model.View;

public partial class handlers_ajax : OnlyAjaxPage
{
    [AjaxMethod]
    public void Ajax()
    {
        NtJson.ShowOK();
    }
}