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

public partial class netin_customer_message : ListBase<View_Customer_Message, Customer_Message>, IAllAjax
{

    protected override void Prepare()
    {
        base.Prepare();
        Title = "站内消息";
    }

    public override void List()
    {
        SetUsePagerOn();
        BeginTable("选择", "发送总数", "标题", "创建日期", "发送", "操作");
        foreach (var item in DataSource)
        {
            Row(() =>
            {
                TdKey(item.Id);
                Td(item.TotalSent);
                Td(NtUtility.GetSubString(item.Title, 24));
                Td(item.CreatedDate);
                TdF("<a href=\"javascript:;\" onclick=\"sendmsg({0});\">发送消息</a>", item.Id);
                TdEditViaAjax(item.Id);
            });
        }
        EndTable(() =>
        {
            TdSelectAll();
            TdPager(4);
            TdEditViaAjax();
        });
    }

    [AjaxMethod]
    public void SendMsg()
    {
        string ids = "";
        int msgid = 0;
        NtJson.EnsureNumber("msgid", "参数错误:msgid", out msgid);
        NtJson.EnsureInt32Range("ids", out ids);
        StringBuilder sql = new StringBuilder();
        foreach (var id in ids.Split(','))
        {
            sql.AppendFormat("insert into [{0}](CustomerId,CustomerMessageId,Viewed,SentDate)values({1},{2},0,getdate())\r\n",
                M("Customer_Message_Customer_Mapping"), id, msgid);
        }
        SqlHelper.ExecuteNonQuery(sql.ToString());
        NtJson.ShowOK("发送成功！");
    }

    [AjaxMethod]
    public void TMPost()
    {
        AjaxSave<Customer_Message>();
    }
    [AjaxMethod]
    public void TMDel()
    {
        AjaxDel<Customer_Message>();
    }
    [AjaxMethod]
    public void TMGet()
    {
        AjaxGet<View_Customer_Message>();
    }
    [AjaxMethod]
    public void TMList()
    {
        List();
    }
}