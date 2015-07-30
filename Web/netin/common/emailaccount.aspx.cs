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

public partial class netin_common_emailaccount : ListBase<EmailAccount>
{

    public override string EditPagePath
    {
        get
        {
            return "emailaccount_config.aspx";
        }
    }

    public override void List()
    {
        BeginTable("ID", "发送方Email地址", "发送者显示名", "主机名", "端口", "用户名", "当前使用", "操作");

        foreach (var item in DataSource)
        {
            Row(() =>
            {
                Td(item.Id);
                Td(item.Email);
                Td(item.DisplayName);
                Td(item.Host);
                Td(item.Port);
                Td(item.UserName);
                TdF("<a href=\"javascript:;\" onclick=\"setThisDefault({0});\">{1}</a>",
                    item.Id, item.IsDefault ? "是" : "否");
                TdEdit(item.Id);
            });
        }

        EndTable(() =>
        {
            TdSpan(7);
            Td("<a href=\"emailaccount_config.aspx\" class=\"a-button\">添加</a>");
        });
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "邮箱账号管理";
    }

    [AjaxMethod]
    public void SetDefaultEmailAccount()
    {
        int id = IMPOSSIBLE;
        NtJson.EnsureNumber("id", "参数错误:id", out id);
        SqlHelper.ExecuteNonQuery(string.Format(
            "Update [{0}] Set IsDefault=0;\r\n Update [{0}] Set IsDefault=1 Where ID={1};", M(typeof(EmailAccount)), id));
        NtJson.ShowOK();
    }

}