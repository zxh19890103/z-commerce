using Nt.DAL;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using System.Xml;

namespace Nt.BLL
{
    /// <summary>
    /// Htmlizer 的摘要说明
    /// </summary>
    public class Htmlizer
    {
        #region singleton

        private static Htmlizer _instance = new Htmlizer();
        static readonly object padlock = new object();
        public static Htmlizer Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        HtmlizationMode _mode = HtmlizationMode.UseID;
        /// <summary>
        /// 静态生成模式
        /// </summary>
        public HtmlizationMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        string _category = string.Empty;
        /// <summary>
        /// 目录，默认为空
        /// </summary>
        public string Category { get { return _category; } set { _category = value; } }

        /// <summary>
        /// 生成静态页
        /// </summary>
        /// <param name="model">IHtmlizable</param>
        public void GenerateHtml(IHtmlizable model)
        {
            if (model == null)
                return;
            if (string.IsNullOrEmpty(model.DetailTemplate))//没有提供模板
                return;

            if (!RegexUtility.IsAbsUrl(model.DetailTemplate))
            {
                return;
            }

            string templateOnDisk = WebHelper.MapPath(model.DetailTemplate);
            if (!File.Exists(templateOnDisk))//没有提供模板
                return;

            FileInfo aspx = new FileInfo(templateOnDisk);
            string validUrl = string.Empty;
            string targetPath = "";

            switch (_mode)
            {
                case HtmlizationMode.UseGUID:
                    targetPath = string.Format("/html/{0}.html", model.Guid);
                    break;
                case HtmlizationMode.UseID:
                    if (_category == string.Empty)
                        targetPath = string.Format("/html/{0}.html", model.Id);
                    else
                        targetPath = string.Format("/html/{0}/{1}.html", _category, model.Id);
                    break;
                default:
                    break;
            }

            string htmlPathOnDisk = WebHelper.MapPath(targetPath);

            bool needReWrite = false;
            if (File.Exists(htmlPathOnDisk))//没有静态文件
            {
                needReWrite = true;
            }
            else
            {
                FileInfo html = new FileInfo(htmlPathOnDisk);
                //当模板文件的修改时间晚于静态页的修改时间，
                //或者数据库对应的实体的更新时间晚于静态页的修改时间
                //都需要生成静态页
                if (aspx.LastWriteTime > html.LastWriteTime
                    || model.UpdatedDate > html.LastWriteTime)
                {
                    needReWrite = true;
                }
            }

            //重写静态文件
            if (needReWrite)
            {
                validUrl = string.Format("{0}{1}?id={2}",
                    WebHelper.CurrentHost,
                    model.DetailTemplate,
                    model.Id);
                GenerateHtml(validUrl, targetPath);
            }
        }

        public void GenerateHtml<M>(int id)
            where M : BaseEntity, IHtmlizable, new()
        {
            var model = DbAccessor.GetById<M>(id);
            GenerateHtml(model);
        }

        /// <summary>
        /// 生成静态页
        /// </summary>
        /// <param name="validUrl">Http请求地址（必须是完整的包含‘http[s]://’，如果不包含则必须是绝对路径以‘/’开始）</param>
        /// <param name="targetPath">有效的路径，必须是绝对路径以‘/’开始</param>
        public void GenerateHtml(string validUrl, string targetPath)
        {
            if (string.IsNullOrEmpty(validUrl)
                || string.IsNullOrEmpty(targetPath))
                return;
            //targetPath无效
            if (!targetPath.StartsWith("/"))
                return;
            //validUrl既不是http开头也不是/开头
            if (!validUrl.StartsWith("http://"))
            {
                if (!validUrl.StartsWith("/"))
                    return;
                else
                    validUrl = string.Format("{0}{1}", WebHelper.CurrentHost, validUrl);
            }

            try
            {
                HttpWebRequest request = HttpWebRequest.Create(validUrl) as System.Net.HttpWebRequest;
                request.Method = "GET";
                using (WebResponse response = request.GetResponse())
                {
                    Stream data = response.GetResponseStream();
                    string phy_path = WebHelper.MapPath(targetPath);
                    FileStream html = File.Create(phy_path);
                    int i = 0;
                    while ((i = data.ReadByte()) != -1)
                    {
                        html.WriteByte((byte)i);
                    }
                    html.Flush();
                    html.Close();
                    response.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex.Message);

            }
        }

    }

    /// <summary>
    /// 静态生成模式
    /// UseGUID   使用GUID
    /// UseID        使用ID
    /// </summary>
    public enum HtmlizationMode { UseGUID = 10, UseID = 20 }

}