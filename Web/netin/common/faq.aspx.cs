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
using System.Text;
using System.Data.SqlClient;
using System.Data;

public partial class netin_common_faq : ListBase<Faq>, IAllAjax
{
    public override void List()
    {
        BeginTable("Select", "Type", "Question", "Action");

        foreach (var item in DataSource)
        {
            Row(() =>
            {
                TdKey(item.Id);
                Td(FindTextByValue(StaticDataProvider.Instance.FaqTypeProvider, item.Type));
                Td(item.Question);
                TdEditViaAjax(item.Id);
            });
        }
        EndTable(() =>
        {
            TdSelectAll();
            TdSpan(2);
            TdEditViaAjax();
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "Faq管理";
    }

    [AjaxMethod]
    public void TMPost()
    {
        AjaxSave<Faq>();
    }
    [AjaxMethod]
    public void TMDel()
    {
        AjaxDel<Faq>();
    }
    [AjaxMethod]
    public void TMGet()
    {
        AjaxGet<Faq>();
    }
    [AjaxMethod]
    public void TMList()
    {
        List();
    }
}