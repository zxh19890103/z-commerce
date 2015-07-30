using Nt.BLL;
using Nt.Model;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;

namespace Nt.Framework
{
    public class OnlyAjaxPage : Page
    {
        /// <summary>
        /// 当前欲操作的表名
        /// </summary>
        protected string table;

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            if (Request.Headers["X-Requested-With"] == null
                || !Request.Headers["X-Requested-With"].Equals("xmlhttprequest", StringComparison.OrdinalIgnoreCase))
            {
                Response.Write("不允许进行非Ajax请求");
                Response.End();
            }

            string action = string.Empty;
            table = Request["table"];
            NtJson.EnsureNotNullOrEmpty("action", "缺少参数:action", out action);

            Type t = this.GetType();
            MethodInfo method = null;
            AjaxMethodAttribute aaa = null;

            foreach (var m in t.GetMethods())
            {
                if (m.Name.Equals(action, StringComparison.OrdinalIgnoreCase))
                {
                    object[] attr = m.GetCustomAttributes(typeof(AjaxMethodAttribute), true);//检查AjaxAttribute特性
                    if (attr != null && attr.Length > 0)
                    {
                        aaa = attr[0] as AjaxMethodAttribute;
                        method = m;
                    }
                    break;
                }
            }

            if (method == null)
                NtJson.ShowError("未找到方法!");

            var aaa0 = aaa as AjaxAuthorizeAttribute;//检测授权
            if (aaa0 != null)
            {
                switch (aaa0.AuthorizeFlag)
                {
                    case AuthorizeFlag.User:
                        var user = Nt.BLL.NtContext.Current.CurrentUser;
                        if (user == null)
                            NtJson.ShowError(2, "管理员未登录!");
                        break;
                    case AuthorizeFlag.Customer:
                        var customer = Nt.BLL.NtContext.Current.CurrentCustomer;
                        if (customer == null)
                        {
                            NtJson.ShowError(2, "会员未登录!");
                        }
                        break;
                    default:
                        break;
                }
            }

            Response.Clear();
            method.Invoke(this, null);
            Response.End();
        }
        
        /// <summary>
        /// 当前会员
        /// </summary>
        protected View_Customer Customer
        {
            get { return NtContext.Current.CurrentCustomer; }
        }

        /// <summary>
        /// 当前管理员
        /// </summary>
        protected new View_User User
        {
            get { return NtContext.Current.CurrentUser; }
        }

        /// <summary>
        /// 当前语言
        /// </summary>
        protected Language Language
        {
            get { return NtContext.Current.CurrentLanguage; }
        }

    }
}
