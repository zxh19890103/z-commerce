using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Text;
using Nt.Model;
using Nt.DAL;
using Nt.Model.View;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace Nt.BLL
{
    public sealed class NtContext
    {
        #region singleton
        private NtContext() { }

        static NtContext _current = null;
        static readonly object padlock = new object();

        public static NtContext Current
        {
            get
            {
                lock (padlock)
                {
                    if (_current == null)
                    {
                        _current = new NtContext();
                    }
                    return _current;
                }
            }
        }
        #endregion

        #region const
        public const string COOKIE_KEY_OF_LANGUAGE = "ntCurrentLanguage";
        public const string SESSION_KEY_OF_USER = "ntUser";
        public const string SESSION_KEY_OF_CUSTOMER = "ntCustomer";
        public const string CACHE_KEY_OF_CURRENT_USER = "nt-cache-current-user-{0}";
        public const string CACHE_KEY_OF_CURRENT_LANGUAGE = "nt-cache-current-langauge-{0}";
        public const string CACHE_KEY_OF_CURRENT_CUSTOMER = "nt-cache-current-cutomer-{0}";
        #endregion

        #region UserID LanguageID,CustomerID

        /// <summary>
        /// 当CustomerID=0时，退出登录
        /// </summary>
        public int UserID
        {
            get
            {
                if (CurrentUser == null)
                    return 0;
                return CurrentUser.Id;
            }
            set
            {
                if (value == 0)
                {
                    UserLogOut();
                    return;
                }
                WebHelper.Session[SESSION_KEY_OF_USER] = value;
            }
        }

        /// <summary>
        /// LanguageID=xx,set language
        /// </summary>
        public int LanguageID
        {
            get
            {
                if (CurrentLanguage == null)
                    return 0;
                return CurrentLanguage.Id;
            }
            set
            {
                var cookie = new HttpCookie(COOKIE_KEY_OF_LANGUAGE
                    , value.ToString());
                cookie.Expires = DateTime.Now.AddDays(30);
                WebHelper.Response.SetCookie(cookie);
            }
        }

        /// <summary>
        /// 当CustomerID=0时，退出登录
        /// </summary>
        public int CustomerID
        {
            get
            {
                if (CurrentCustomer == null)
                    return 0;
                return CurrentCustomer.Id;
            }
            set
            {
                if (value == 0)
                {
                    CustomerLogOut();
                    return;
                }
                WebHelper.Session[SESSION_KEY_OF_CUSTOMER] = value;
            }
        }

        #endregion

        #region CurrentLanguage  CurrentUser CurrentCustomer

        public Language CurrentLanguage { get { return TryGetCurrentLanguage(); } }

        public View_User CurrentUser { get { return TryGetCurrentUser(); } }

        public View_Customer CurrentCustomer { get { return TryGetCurrentCusomer(); } }

        #endregion

        /// <summary>
        /// 管理员退出
        /// </summary>
        public void UserLogOut()
        {
            string key = string.Format(CACHE_KEY_OF_CURRENT_LANGUAGE, LanguageID);
            CachingManager.Current.Remove(key);
            key = string.Format(CACHE_KEY_OF_CURRENT_USER, UserID);
            CachingManager.Current.Remove(key);
            WebHelper.Session.Contents.Remove(SESSION_KEY_OF_USER);
        }

        /// <summary>
        /// 会员退出
        /// </summary>
        public void CustomerLogOut()
        {
            string key = string.Format(CACHE_KEY_OF_CURRENT_CUSTOMER, CustomerID);
            CachingManager.Current.Remove(key);
            WebHelper.Session.Contents.Remove(SESSION_KEY_OF_CUSTOMER);
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void ClearCache()
        {
            string key = string.Format(CACHE_KEY_OF_CURRENT_LANGUAGE, LanguageID);
            CachingManager.Current.Remove(key);
            key = string.Format(CACHE_KEY_OF_CURRENT_USER, UserID);
            CachingManager.Current.Remove(key);
            key = string.Format(CACHE_KEY_OF_CURRENT_CUSTOMER, CustomerID);
            CachingManager.Current.Remove(key);
        }

        /// <summary>
        /// 获取当前登录的用户
        /// </summary>
        /// <returns></returns>
        View_User TryGetCurrentUser()
        {
            int userid = 0;

            if (IsDebugging > 0)
            {
                userid = IsDebugging;//Debug
            }
            else
            {
                var t = WebHelper.Session[SESSION_KEY_OF_USER];
                if (t == null)
                    return null;
                userid = Convert.ToInt32(t);
            }
            string key = string.Format(CACHE_KEY_OF_CURRENT_USER, userid);
            if (CachingManager.Current.HasCached(key))
                return CachingManager.Current.Get<View_User>(key);

            View_User currentUser = DbAccessor.GetById<View_User>(userid);

            if (currentUser == null)
                return null;

            CachingManager.Current.Cache(
                   key,
                   currentUser
                   );
            return currentUser;
        }

        /// <summary>
        /// 获取当前语言版本
        /// </summary>
        Language TryGetCurrentLanguage()
        {
            int langid = 0;
            var t = WebHelper.Request.Cookies[COOKIE_KEY_OF_LANGUAGE];

            if (t != null)
                langid = Convert.ToInt32(t.Value);

            Language currentLanguage = null;
            string key = string.Empty;

            if (langid != 0)
            {
                key = string.Format(CACHE_KEY_OF_CURRENT_LANGUAGE, langid);
                if (CachingManager.Current.HasCached(key))
                    return CachingManager.Current.Get<Language>(key);

                currentLanguage = DbAccessor.GetById<Language>(langid);
                if (currentLanguage != null)
                    return currentLanguage;
            }

            currentLanguage = DbAccessor.GetFirstOrDefault<Language>();

            if (currentLanguage == null)
                throw new Exception("数据库中不存在任何语言信息!");

            key = string.Format(CACHE_KEY_OF_CURRENT_LANGUAGE, currentLanguage.Id);

            CachingManager.Current.Cache(
               key,
               currentLanguage
               );

            LanguageID = currentLanguage.Id;

            return currentLanguage;
        }

        /// <summary>
        /// 尝试获取会员
        /// </summary>
        /// <returns></returns>
        View_Customer TryGetCurrentCusomer()
        {
            int customerid = 0;

            if (IsCustomerDebuging > 0)
            {
                customerid = IsCustomerDebuging;//Debug
            }
            else
            {
                var t = WebHelper.Session[SESSION_KEY_OF_CUSTOMER];
                if (t == null)
                    return null;
                customerid = Convert.ToInt32(t);
            }
            string key = string.Format(CACHE_KEY_OF_CURRENT_CUSTOMER, customerid);
            if (CachingManager.Current.HasCached(key))
                return CachingManager.Current.Get<View_Customer>(key);

            View_Customer currentCustomer = DbAccessor.GetById<View_Customer>(customerid);

            if (currentCustomer == null)
                return null;

            CachingManager.Current.Cache(
                   key,
                   currentCustomer
                   );
            return currentCustomer;
        }

        /// <summary>
        /// 是否管理员调试阶段
        /// </summary>
        public int IsDebugging
        {
            get
            {
                int isDebug = 0;

                var setting = ConfigurationManager.AppSettings["admin.isDebugging"];

                int.TryParse(setting, out isDebug);

                return isDebug;
            }
        }

        /// <summary>
        /// 是否会员调试阶段
        /// </summary>
        public int IsCustomerDebuging
        {
            get
            {
                int isDebug = 0;

                var setting = ConfigurationManager.AppSettings["customer.isDebugging"];

                int.TryParse(setting, out isDebug);

                return isDebug;
            }
        }

        /// <summary>
        /// 友情链接是否需要图片
        /// </summary>
        public bool IsFriendLinkWithImage
        {
            get
            {
                bool need = false;

                var setting = ConfigurationManager.AppSettings["admin.friendlink.with.image"];

                bool.TryParse(setting, out need);

                return need;
            }
        }

        /// <summary>
        /// 系统保留目录
        /// </summary>
        public string SysReservedDirs
        {
            get
            {
                return AppSettings("sys.reserved.dirs");
            }
        }

        /// <summary>
        /// 根据name获取Setting
        /// </summary>
        /// <param name="setting">Setting的name</param>
        /// <returns></returns>
        public string AppSettings(string setting)
        {
            var value = ConfigurationManager.AppSettings[setting];
            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// 根据index获取Setting
        /// </summary>
        /// <param name="index">Setting的index</param>
        /// <returns></returns>
        public string AppSettings(int index)
        {
            var value = ConfigurationManager.AppSettings[index];
            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        bool _dataBaseCreated = false;
        /// <summary>
        /// 数据库是否建立
        /// </summary>
        public bool DataBaseCreated
        {
            get
            {
                var db = DataBase;
                return _dataBaseCreated;
            }
        }

        string _dataBase = string.Empty;
        /// <summary>
        /// 数据库名
        /// </summary>
        public string DataBase
        {
            get
            {
                if (string.IsNullOrEmpty(_dataBase))
                {
                    string pathOnDisk = WebHelper.MapPath("/app_data/connection.txt");
                    if (File.Exists(pathOnDisk))
                    {
                        Regex reg = new Regex(@"initial catalog=(\w+);");
                        Match mat = reg.Match(File.ReadAllText(pathOnDisk));
                        if (mat.Success)
                        {
                            var group = mat.Groups[1];
                            if (group.Success)
                            {
                                _dataBaseCreated = true;
                                _dataBase = group.Value;
                            }
                        }
                    }
                }
                return _dataBase;
            }
        }

        /// <summary>
        /// 重启App
        /// </summary>
        public void ReStartApplication()
        {
            try
            {
                File.SetLastWriteTimeUtc(WebHelper.MapPath("~/global.asax"), DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex.Message);
                try
                {
                    File.SetLastWriteTimeUtc(WebHelper.MapPath("~/web.config"), DateTime.UtcNow);
                }
                catch (Exception ex2)
                {
                    Logger.Instance.Log(ex2.Message);
                }
            }
        }

        /// <summary>
        /// 一个值，指示是否有会员登录
        /// </summary>
        public bool CustomerLogined
        {
            get { return CurrentCustomer != null; }
        }
    }
}
