using Nt.BLL;
using Nt.DAL;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nt.Framework
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public class NtConfig
    {
        #region Templates Path Root

        /// <summary>
        /// 当前应使用的模板的绝对路径
        /// </summary>
        static string _currentTemplatesPath = string.Empty;
        public static string CurrentTemplatesPath
        {
            get
            {
                if (string.IsNullOrEmpty(_currentTemplatesPath))
                    _currentTemplatesPath = GetTargetTemplatesPath(CurrentLanguage);
                return _currentTemplatesPath;
            }
            set { _currentTemplatesPath = value; }
        }

        /// <summary>
        /// 当前栏目的根目录,带/
        /// </summary>
        static string _currentChannelRootUrl = string.Empty;
        public static string CurrentChannelRootUrl
        {
            get
            {
                int lastIndexOfSlash = WebHelper.Request.Url.AbsolutePath.LastIndexOf('/');
                _currentChannelRootUrl = WebHelper.Request.Url.AbsolutePath.Substring(0, lastIndexOfSlash + 1); ;
                return _currentChannelRootUrl;
            }
            set { _currentChannelRootUrl = value; }
        }

        static int _currentLanguage = 0;
        /// <summary>
        /// 当前语言版本id
        /// </summary>
        public static int CurrentLanguage
        {
            get
            {
                if (_currentLanguage == 0)
                {
                    _currentLanguage = TargetLanguage;
                }
                return _currentLanguage;
            }
            set { _currentLanguage = value; }
        }

        static Language _currentLanguageModel;
        /// <summary>
        /// 当前语言版本的信息
        /// </summary>
        public static Language CurrentLanguageModel
        {
            get
            {
                if (_currentLanguageModel == null)
                {
                    _currentLanguageModel = DbAccessor.GetById<Language>(CurrentLanguage);
                    if (_currentLanguageModel == null)
                        throw new Exception("appSettings配置错误");
                }
                return _currentLanguageModel;
            }
            set { _currentLanguageModel = value; }
        }

        /// <summary>
        /// 目标语言版本id
        /// </summary>
        public static int TargetLanguage
        {
            get
            {
                string currentAbsPath = WebHelper.Request.Url.AbsolutePath.ToLower();
                int pos = currentAbsPath.IndexOf('/');
                int pos1 = currentAbsPath.Substring(pos + 1).IndexOf('/') + 1;
                if (pos > -1)
                {
                    if (pos1 > -1)
                    {
                        string path = currentAbsPath.Substring(pos, pos1 - pos + 1);
                        foreach (var m in LangCodeIDMappings)
                        {
                            if (path.Equals(m.Value))
                                return m.Key;
                        }
                    }
                }
                return LangCodeIDMappings.First().Key;
            }
        }

        /// <summary>
        /// 获取目标模板的根目录
        /// </summary>
        /// <param name="lang">语言id</param>
        /// <returns></returns>
        public static string GetTargetTemplatesPath(int lang)
        {
            foreach (var m in LangCodeIDMappings)
            {
                if (lang == m.Key)
                    return m.Value;
            }
            return "/";
        }

        /// <summary>
        /// 当切换语言版本的时候，调用此方法，
        /// </summary>
        public static void EmptyStaticResources()
        {
            CurrentChannelRootUrl = string.Empty;
            CurrentTemplatesPath = string.Empty;
            CurrentLanguage = 0;
            CurrentLanguageModel = null;
        }

        #endregion

        #region  多语言信息
        /// <summary>
        /// 指示是否使用了多语言
        /// </summary>
        public static bool HasMutiLanguage
        {
            get
            {
                return LangCodeIDMappings.Count > 0;
            }
        }

        public static string LangCodeIDMappingsConfig
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["lang.templates.path.mappings"]; }
        }

        static Dictionary<int, string> _langCodeIDMappings = null;
        public static Dictionary<int, string> LangCodeIDMappings
        {
            get
            {
                if (_langCodeIDMappings == null)
                {
                    _langCodeIDMappings = new Dictionary<int, string>();
                    string[] mappings = LangCodeIDMappingsConfig.Split(new char[] { ':', ',' });
                    for (int i = 0; i < mappings.Length; )
                    {
                        _langCodeIDMappings[Convert.ToInt32(mappings[i++])] = mappings[i++];
                    }
                }

                return _langCodeIDMappings;
            }
        }
        #endregion

        /// <summary>
        /// 手机版网站的域名或根路径
        /// </summary>
        public static string MobileSiteUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["mobile.website.url"];
            }
        }

        /// <summary>
        /// 主机名
        /// </summary>
        public static string DomainName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["domain.name"];
            }
        }

        public static readonly DateTime MinDate = new DateTime(1753, 1, 1);
        public static readonly DateTime MaxDate = new DateTime(2999, 12, 31);
    }
}