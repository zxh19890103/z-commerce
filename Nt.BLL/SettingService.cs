using Nt.DAL;
using Nt.Model.Setting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Nt.BLL
{
    /// <summary>
    /// 提取和保存设置的类
    /// </summary>
    /// <typeparam name="S">必须是BaseSetting子类</typeparam>
    public class SettingService<S>
         where S : BaseSetting, new()
    {
        #region Fields
        /// <summary>
        /// 0--Class Name
        /// 1--LanguageCode
        /// </summary>
        private const string SETTING_File_PATTERN = "/App_Data/Settings/{0}.{1}.setting";
        string _xmlpath;
        string _rootName;
        Type _t;
        #endregion
        
        public SettingService(string langCode)
        {
            _t = typeof(S);
            _rootName = _t.Name;
            _xmlpath = string.Format(SETTING_File_PATTERN,
                _rootName, langCode);
        }

        #region Methods
        /// <summary>
        /// 从Xml获取S
        /// </summary>
        /// <returns></returns>
        public S ResolveSetting()
        {
            var pathOnDisk = WebHelper.MapPath(_xmlpath);
            if (File.Exists(pathOnDisk))
                return GetSettingFromXml();
            else
                CreateSettingXml();
            var s = new S();
            s.InitData();
            return s;
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="s"></param>
        public void SaveSetting(S s)
        {
            var pathOnDisk = WebHelper.MapPath(_xmlpath);
            var xdoc = new XmlDocument();
            xdoc.Load(pathOnDisk);
            XmlNode root = xdoc.SelectSingleNode(_rootName);
            object value;
            XmlNode node;
            foreach (var p in _t.GetProperties())
            {
                value = p.GetValue(s, null);
                node = root[p.Name];
                //当不存在查询节点时，创建
                if (node == null)
                {
                    XmlElement e = xdoc.CreateElement(p.Name);
                    e.InnerText = value.ToString();
                    root.AppendChild(e);
                }
                else
                {
                    node.InnerText = value.ToString();
                }
            }
            xdoc.Save(pathOnDisk);
        }

        /// <summary>
        /// 从Xml文档中获取S的属性值
        /// </summary>
        /// <returns></returns>
        private S GetSettingFromXml()
        {
            var pathOnDisk = WebHelper.MapPath(_xmlpath);
            S s = new S();
            var xdoc = new XmlDocument();
            xdoc.Load(pathOnDisk);
            XmlNode root = xdoc.SelectSingleNode(_rootName);;
            foreach (var p in _t.GetProperties())
            {
                var node = root[p.Name];
                if (node != null)
                {
                    GenericUtility.SetValueToProp(p, s, node.InnerText);
                }
            }
            return s;
        }

        /// <summary>
        /// 根据S的属性结构创建Xml文档
        /// </summary>
        private void CreateSettingXml()
        {
            var xdoc = new XmlDocument();
            XmlDeclaration declaration = xdoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xdoc.AppendChild(declaration);
            XmlElement root = xdoc.CreateElement(_rootName);
            xdoc.AppendChild(root);
            foreach (var p in _t.GetProperties())
            {
                XmlElement e = xdoc.CreateElement(p.Name);
                e.InnerText = GenericUtility
                    .GetDefaultValueByTypeCode(p.PropertyType.Name)
                    .ToString();
                root.AppendChild(e);
            }
            var pathOnDisk = WebHelper.MapPath(_xmlpath);
            xdoc.Save(pathOnDisk);
        }
        #endregion

    }

    /// <summary>
    /// 获取Setting的类
    /// </summary>
    public class SettingService
    {
        /// <summary>
        /// 获取指定类型的Setting
        /// </summary>
        /// <typeparam name="S">设置的类</typeparam>
        /// <param name="languageCode">语言标志</param>
        /// <returns></returns>
        public static S GetSettingModel<S>(string languageCode)
            where S : BaseSetting, new()
        {
            SettingService<S> ss = new SettingService<S>(languageCode);
            return ss.ResolveSetting();
        }

        /// <summary>
        /// 获取当前语言版本下的Setting
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <returns></returns>
        public static S GetSettingModel<S>()
            where S : BaseSetting, new()
        {
            return SettingService.GetSettingModel<S>(
                NtContext.Current.CurrentLanguage.LanguageCode);
        }
        
    }

}
