using Nt.BLL;
using Nt.Model;
using Nt.Model.Common;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;

namespace Nt.Framework
{
    /// <summary>
    /// InitRequiredVar
    /// </summary>
    public abstract class AjaxiblePage : Page
    {
        /// <summary>
        /// 指示是否ajax请求
        /// </summary>
        public bool IsAjaxRequest
        {
            get
            {
                if (Request.Headers["X-Requested-With"] == null
                    || !Request.Headers["X-Requested-With"].Equals("xmlhttprequest",
                    StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 是否返回json格式
        /// </summary>
        public bool IsResponseJson
        {
            get
            {
                if (Request["responseType"] == null
                    || (!Request["responseType"].Equals("json", StringComparison.OrdinalIgnoreCase)
                    && !Request["responseType"].Equals("text", StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
                return Request["responseType"].Equals("json", StringComparison.OrdinalIgnoreCase);
            }
        }

        protected abstract void InitRequiredVar();

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            InitRequiredVar();
            if (IsAjaxRequest)
            {
                string action = Request["action"];
                if (string.IsNullOrEmpty(action))
                    NtJson.ShowError("参数错误!未指定方法:action");

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
                            var page = this as Admin.PageBase;
                            if (page != null)
                            {
                                if (!page.BasedOnUser)
                                    break;
                                var user = Nt.BLL.NtContext.Current.CurrentUser;
                                if (user == null)
                                    NtJson.ShowError(2, "管理员未登录!");
                                else
                                {
                                    if (page.CurrentPermission != null)
                                    {
                                        if (!page.UserService.Authorized(user.UserLevelId, page.CurrentPermission.Id))
                                            NtJson.ShowError(3, "未授权访问此模块!");
                                    }
                                }
                            }
                            break;
                        case AuthorizeFlag.Customer:
                            var customer = Nt.BLL.NtContext.Current.CurrentCustomer;
                            if (customer == null)
                            {
                                NtJson json = new NtJson();
                                json.Json["error"] = 2;
                                json.Json["message"] = "会员未登录!";
                                Response.Clear();
                                Response.Write(json);
                                Response.End();
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
        }

        #region Common Utility

        /// <summary>
        /// 输出人民币金额
        /// </summary>
        /// <param name="money"></param>
        public void RenderMoney(decimal money)
        {
            Response.Write("&#165;");
            Response.Write(money.ToString("f"));
        }

        /// <summary>
        /// 从from[List$NtListItem$]中根据v值获取键，未找到返回NULL
        /// </summary>
        /// <param name="from">键值对</param>
        /// <param name="v">值</param>
        /// <returns></returns>
        public string FindTextByValue(List<NtListItem> from, object v)
        {
            bool isNullOrEmpty = from == null || from.Count < 1;
            if (isNullOrEmpty && v == null) return "NULL";
            if (isNullOrEmpty) return v.ToString();
            if (v == null) return from[0].Text;
            foreach (var item in from)
            {
                if (item.Value.Equals(v.ToString()))
                {
                    return item.Text;
                }
            }
            return v.ToString();
        }

        #endregion

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
