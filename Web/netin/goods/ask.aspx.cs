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
using Nt.Model.View;
using System.Data;
using System.Data.SqlClient;

public partial class netin_goods_ask : ListBase
{
    public override string TableName
    {
        get { return "Goods_Ask"; }
    }

    public override void List()
    {
        string filter = string.Empty;
        string orderby = "CreatedDate desc";
        int total = DbAccessor.GetRecordCount(typeof(Goods_Ask), filter);
        Pager = new NtPager(total, 12);
        var source = DbAccessor.GetList<View_Goods_Ask>(filter, orderby, Pager.PageIndex, Pager.PageSize);

        BeginTable("选择", "商品", "会员", "标题", "内容", "咨询日期", "备注", "操作");

        foreach (var item in source)
        {
            Row<View_Goods_Ask>(e =>
            {
                TdKey(item.Id);
                TdF("<a href=\"goods_edit.aspx?id={0}\">{1}</a>", item.GoodsId, item.GoodsName);
                TdF("<a href=\"javascript:;\" onclick=\"openWindow('../customer/info_view.aspx?id={0}');\">{1}</a>", item.CustomerId, item.CustomerName);
                Td(item.Title);
                Td(item.Body);
                Td(item.CreatedDate);
                TdF("<a href=\"javascript:;\" onclick=\"askNote({0});\">编辑/查看备注</a>", item.Id);
                TdEdit(item.Id);
            }, item);
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdPager(6);
            TdDelSelected();
        });
    }

    public override string EditPagePath
    {
        get { return "ask_edit.aspx"; }
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "商品咨询";
    }

    [AjaxMethod]
    public void SaveAskNote()
    {
        string note = Request["note"];
        string id = Request["id"];
        SqlHelper.ExecuteNonQuery(
            SqlHelper.GetConnection(),
            CommandType.Text,
            F("Update [{0}] Set [Note]=@Note Where ID=@Id", M(typeof(Goods_Ask))),
            new SqlParameter[]{
                new SqlParameter("@Note",note),
                new SqlParameter("@Id",id)
            });
        NtJson.ShowOK();
    }

}