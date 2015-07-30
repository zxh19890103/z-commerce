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

public partial class netin_common_emailaccount_config : EditBase<EmailAccount>
{
    public override string PermissionSysN
    {
        get
        {
            return "netin.common.emailaccount";
        }
    }

    public override string ListPagePath
    {
        get
        {
            return "emailaccount.aspx";
        }
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = EditPageTitlePrefix + "邮箱账号";
    }

    [AjaxMethod]
    public void TrySendMail()
    {
        string mailAddress;
        int accountId;
        NtJson.EnsureEmail("mailAddress", "参数错误:mailAddress", out mailAddress);
        NtJson.EnsureNumber("accountId", "参数错误:accountId", out accountId);

        bool flag = true;
        string message = "Error";
        try
        {
            MailSender sender = new MailSender();
            sender.EmailAccount = DbAccessor.GetById<EmailAccount>(accountId);
            if (sender.EmailAccount == null)
                throw new Exception("邮箱账号不存在!");
            sender.SendMail("测试邮件", "大连奈特有限公司测试邮件", mailAddress, "ceshi");
        }
        catch (Exception ex)
        {
            flag = false;
            message = ex.Message;
        }

        NtJson json = new NtJson();
        json.ErrorCode = flag ? 0 : 1;
        json.Json["message"] = flag ? "邮件已发送，请查看！" : message;
        Response.Write(json);
    }

}