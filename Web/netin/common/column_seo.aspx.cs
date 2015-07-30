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
using System.Data;
using System.Data.SqlClient;

public partial class netin_common_column_seo : ListBase<Navigation>
{
    [AjaxMethod]
    public void GetColumn()
    {
        AjaxGet<Navigation>();
    }

    [AjaxMethod]
    public void SaveSeo()
    {
        SqlHelper.ExecuteNonQuery(
            SqlHelper.GetConnection(),
            CommandType.Text,
           string.Format(
           "Update [{0}] Set SeoTitle=@SeoTitle,SeoKeywords=@SeoKeywords,SeoDescription=@SeoDescription Where Id=@Id", M(typeof(Navigation))),
            new SqlParameter[]{
                new SqlParameter("@SeoTitle",Request["SeoTitle"]),
                new SqlParameter("@SeoKeywords",Request["SeoKeywords"]),
                new SqlParameter("@SeoDescription",Request["SeoDescription"]),
                new SqlParameter("@Id",Request["Id"]),
            });

        NtJson.ShowOK();
    }

    protected override string Where
    {
        get
        {
            return "Display=1";
        }
    }

    protected override string OrderBy
    {
        get
        {
            return "DisplayOrder";
        }
    }

    [AjaxMethod]
    public override void List()
    {
        BeginTable("导航名", "标题", "关键字", "描述", "操作");

        var source = DB.GetTree<Navigation>(Where);
        foreach (var item in source)
        {
            Row(() =>
            {
                Html("<td style=\"text-align:left;padding-left:5px;\">{0}</td>",item.Name);
                Td(NtUtility.GetSubString(item.SeoTitle, 20));
                Td(NtUtility.GetSubString(item.SeoKeywords, 40));
                Td(NtUtility.GetSubString(item.SeoDescription, 40));
                TdF("<a href=\"javascript:;\" onclick=\"openEditor({0});\">修改</a>", item.Id);
            });
        }

        EndTable();
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "栏目优化";
    }

}