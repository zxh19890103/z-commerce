using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Nt.Model;
using Nt.Model.Common;
using Nt.Model.Setting;
using Nt.Model.View;
using Nt.Framework;
using Nt.Framework.Admin;
using Nt.DAL;
using Nt.BLL;
using System.Data;

public partial class netin_goods_goods_binding : ListBase
{
    protected int goodsId = IMPOSSIBLE;

    public override string TableName
    {
        get { return "Goods"; }
    }

    public override string PermissionSysN
    {
        get
        {
            return "netin.goods.list";
        }
    }

    public override void List()
    {
        string filter = string.Format("Id Not In (Select BindingId From [{0}] Where GoodsId={1})",
            DbAccessor.GetModifiedTableName("Goods_Binding"), goodsId);
        int total = DbAccessor.GetRecordCount("Goods", filter);
        Pager = new NtPager(total, 12);
        var source = DbAccessor.GetList<Goods>(filter, "DisplayOrder desc", Pager.PageIndex, Pager.PageSize);

        BeginTable("选择", "名称", "显示");

        foreach (var item in source)
        {
            Row<Goods>(e =>
            {
                TdKey(item.Id);
                Td(item.Name);
                Td(item.Display ? "Yes" : "No");
            }, item);
        }

        EndTable(() =>
        {
            TdSelectAll();
            TdPager(2);
        });
    }

    public override string EditPagePath
    {
        get { return ""; }
    }

    protected override void Prepare()
    {
        base.Prepare();
        int.TryParse(Request.QueryString["goodsId"], out goodsId);
    }

    /// <summary>
    /// 保存
    /// </summary>
    [AjaxMethod]
    public void SaveBindings()
    {
        string bindings = string.Empty;
        int goods = IMPOSSIBLE;
        NtJson.EnsureNotNullOrEmpty("bindings", "参数错误!", out bindings);
        NtJson.EnsureNumber("goods", "参数错误!", out goods);

        string sql = string.Empty;
        string insertPattern = string.Format("Insert Into [{0}](GoodsId,BindingId)values({1},{2});\r\n",
            DbAccessor.GetModifiedTableName("Goods_Binding"), goods, "{0}");

        foreach (var item in
            bindings.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            sql += string.Format(insertPattern, item);
        }

        SqlHelper.ExecuteNonQuery(sql);

        NtJson.ShowOK();
    }

}