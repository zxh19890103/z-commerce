using Nt.DAL;
using Nt.Model;
using Nt.Model.Enum;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Nt.BLL
{
    public class SitemapHelper
    {
        #region const
        const string XMLNS = "http://www.sitemaps.org/schemas/sitemap/0.9";
        #endregion

        #region Props

        /// <summary>
        /// 是否是静态生成
        /// </summary>
        public bool IsHtml { get; set; }

        string _virtualDir2SaveXml = "/";
        public string VirtualDir2SaveXml
        {
            get { return _virtualDir2SaveXml; }
            set { _virtualDir2SaveXml = value; }
        }

        string _sitemapName = "sitemap.xml";
        /// <summary>
        /// the name of sitemap file,default:sitemap.xml
        /// </summary>
        public string SiteMapName
        {
            get { return _sitemapName; }
            set { _sitemapName = value; }
        }

        ChangeFrequency _changeFreq = ChangeFrequency.weekly;
        /// <summary>
        /// a value indicating the frequency this sitemap.xml being grabbed by search engine
        /// </summary>
        public ChangeFrequency ChangeFrequency
        {
            get { return _changeFreq; }
            private set { _changeFreq = value; }
        }

        private SitemapType _sitemapType = SitemapType.Baidu;
        /// <summary>
        /// Baidu,Google,Sougou,Sousou,default:Baidu
        /// </summary>
        public SitemapType SitemapType
        {
            get { return _sitemapType; }
            set { _sitemapType = value; }
        }

        int _countOfFound = 0;
        public int CountOfFound
        {
            get { return _countOfFound; }
        }

        #endregion

        public SitemapHelper() { }

        /// <summary>
        /// 通过分析数据库数据生成sitemap文件，
        /// 需要后台管理员指定有效的列表页和详细页模板路径
        /// 生成的内容有：
        /// 1/2、首页和各栏目首页，文件名为index.aspx或index.html,放在对应的目录下
        /// 3、文章详细页
        /// 4、产品详细页
        /// 5、商品详细页
        /// 6、二级页
        /// </summary>
        public void GenerateSitemap()
        {
            string dir = WebHelper.MapPath(VirtualDir2SaveXml);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            string phy_xmlfile_path = dir + _sitemapName;
            FileStream stream = null;
            if (File.Exists(phy_xmlfile_path))
                File.Delete(phy_xmlfile_path);
            stream = File.Create(phy_xmlfile_path);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            settings.IndentChars = "  ";

            using (XmlWriter xmlWriter = XmlWriter.Create(stream, settings))
            {
                xmlWriter.WriteStartDocument(false);
                if (_sitemapType == SitemapType.Google)
                    xmlWriter.WriteStartElement("urlset", XMLNS);
                else
                    xmlWriter.WriteStartElement("urlset");

                string urlbase = WebHelper.CurrentHost;
                decimal priority = 0.8M;
                string url = string.Empty;
                string tUrl = string.Empty;

                #region Index And Sub Index

                //1
                _changeFreq = ChangeFrequency.always;
                priority = 1.0M;
                url = "/index.aspx";
                if (File.Exists(WebHelper.MapPath(url)))
                {
                    if (IsHtml)
                        tUrl = urlbase + "/index.html";
                    else
                        tUrl = urlbase + url;
                    WriteOneElement(xmlWriter, tUrl, DateTime.Now, priority);
                }

                //2
                priority = 0.9M;
                DirectoryInfo rootDir = new DirectoryInfo(WebHelper.MapPath("/"));
                foreach (DirectoryInfo sub in rootDir.GetDirectories())
                {
                    if (NtContext.Current.SysReservedDirs.Contains(sub.Name.ToLower()))
                        continue;
                    url = "/" + sub.Name + "/index.aspx";
                    if (File.Exists(WebHelper.MapPath(url)))
                    {
                        if (IsHtml)
                            tUrl = urlbase + "/" + sub.Name + "/index.html";
                        else
                            tUrl = urlbase + url;
                        WriteOneElement(xmlWriter, tUrl, DateTime.Now, priority);
                    }
                }

                #endregion

                #region news detail,product detail,goods detail，single

                _changeFreq = ChangeFrequency.daily;
                priority = 0.8M;

                DbAccessor db = new DbAccessor();

                string orderby = "settop desc,recommended desc,displayorder desc,createddate desc";
                db.Columns("id", "DetailTemplate", "Guid", "UpdatedDate").List("View_Article", "display=1", orderby);
                db.Columns("id", "DetailTemplate", "Guid", "UpdatedDate").List("View_Goods", "display=1", orderby);
                db.Columns("id", "DetailTemplate", "Guid", "UpdatedDate").List("View_Product", "display=1", orderby);
                db.Columns("id", "DetailTemplate", "Guid", "UpdatedDate").List("SinglePage");
                db.Execute();

                for (int i = 0; i < 4; i++)
                {
                    foreach (DataRow r in db[i].Rows)
                    {
                        if (IsHtml)
                            tUrl = string.Format("{0}/html/{1}.html", urlbase, r[2]);
                        else
                            tUrl = string.Format("{0}{1}?id={2}", urlbase, r[1], r[0]);
                        WriteOneElement(xmlWriter, tUrl, (DateTime)r[3], priority);
                    }
                }

                #endregion

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
            }
            stream.Flush();
            stream.Close();
            stream.Dispose();
        }

        /// <summary>
        /// 根据导航配置生成sitemap文件
        /// </summary>
        public void GenerateSitemapAccordingToMenu()
        {
            var menuD = DbAccessor.GetList<Navigation>("display=1");

            if (menuD.Count < 1)
                throw new Exception("导航数据库中暂无数据!");

            string dir = WebHelper.MapPath(VirtualDir2SaveXml);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            string phy_xmlfile_path = dir + _sitemapName;
            FileStream stream = null;
            if (File.Exists(phy_xmlfile_path))
                File.Delete(phy_xmlfile_path);
            stream = File.Create(phy_xmlfile_path);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            settings.IndentChars = "  ";

            using (XmlWriter xmlWriter = XmlWriter.Create(stream, settings))
            {
                xmlWriter.WriteStartDocument(false);
                if (_sitemapType == SitemapType.Google)
                    xmlWriter.WriteStartElement("urlset", XMLNS);
                else
                    xmlWriter.WriteStartElement("urlset");

                string urlbase = WebHelper.CurrentHost;
                string url = "", tUrl = "";
                decimal priority = 0.0M;

                DbAccessor db = new DbAccessor();

                string orderby = "settop desc,recommended desc,displayorder desc,createddate desc";
                db.Columns("id", "DetailTemplate", "Guid", "UpdatedDate", "ClassCrumbs").List("View_Article", "display=1", orderby);
                db.Columns("id", "DetailTemplate", "Guid", "UpdatedDate", "ClassCrumbs").List("View_Goods", "display=1", orderby);
                db.Columns("id", "DetailTemplate", "Guid", "UpdatedDate", "CategoryCrumbs").List("View_Product", "display=1", orderby);
                db.Columns("id", "DetailTemplate", "Guid", "UpdatedDate").List("SinglePage");
                db.Execute();
                int table_index = 0;

                foreach (Navigation item in menuD)
                {
                    url = item.Path;
                    priority = GetPriorityByDepth(item.Depth);

                    switch ((ModuleType)item.NaviType)
                    {
                        case ModuleType.Link:
                            _changeFreq = ChangeFrequency.always;
                            if (!Regex.IsMatch(url, @"^https?://"))
                            {
                                tUrl = urlbase + url;//如果url不是有效的网络链接，那么要求Path值必须是绝对路径
                            }
                            WriteOneElement(xmlWriter,
                                tUrl, DateTime.Now, priority);
                            break;
                        case ModuleType.Article:
                        case ModuleType.Goods:
                        case ModuleType.Product:
                            _changeFreq = ChangeFrequency.daily;
                            table_index =
                                (item.NaviType == (int)ModuleType.Article) ? 0 :
                                (item.NaviType == (int)ModuleType.Goods) ? 1 : 2;
                            foreach (DataRow r in db[table_index].Rows)
                            {
                                if (!Regex.IsMatch("," + r[4].ToString(), string.Format(",{0},", item.RootSid)))
                                    continue;
                                if (IsHtml)
                                    tUrl = string.Format("{0}/html/{1}.html", urlbase, r[2]);
                                else
                                    tUrl = string.Format("{0}{1}?id={2}", urlbase, r[1], r[0]);
                                WriteOneElement(xmlWriter, tUrl, (DateTime)r[3], priority);
                            }
                            break;
                        case ModuleType.Page:
                            _changeFreq = ChangeFrequency.daily;
                            table_index = 3;
                            if (string.IsNullOrEmpty(item.PageIds))
                            {
                                //do nothing!
                            }
                            else
                            {
                                foreach (DataRow r in db[table_index].Rows)
                                {
                                    if (!Regex.IsMatch("," + item.PageIds + ",", string.Format(",{0},", r[0])))
                                        continue;
                                    if (IsHtml)
                                        tUrl = string.Format("{0}/html/{1}.html", urlbase, r[2]);
                                    else
                                        tUrl = string.Format("{0}{1}?id={2}", urlbase, r[1], r[0]);
                                    WriteOneElement(xmlWriter, tUrl, (DateTime)r[3], priority);
                                }
                            }
                            break;
                        case ModuleType.Download:
                            //not suport now.
                            break;
                        default: break;
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
            }
            stream.Flush();
            stream.Close();
            stream.Dispose();
        }

        /// <summary>
        /// get priority by depth
        /// </summary>
        /// <param name="depth">navigation depth</param>
        /// <returns></returns>
        decimal GetPriorityByDepth(int depth)
        {
            decimal minP = 0.2M;
            decimal p = 1.0M - depth * (0.2M);
            if (p <= minP)
                return minP;
            return p;
        }

        /// <summary>
        /// 向sitemap.xml文件中写入一个节点
        /// </summary>
        /// <param name="writer">XmlWriter</param>
        /// <param name="loc">链接</param>
        /// <param name="lastmod">更新日期</param>
        /// <param name="priority">优先级</param>
        void WriteOneElement(XmlWriter writer,
            string loc, DateTime lastmod, decimal priority)
        {
            writer.WriteStartElement("url");
            writer.WriteStartElement("loc");
            writer.WriteString(loc);
            writer.WriteEndElement();
            writer.WriteStartElement("lastmod");
            writer.WriteString(lastmod.ToString("yyyy-MM-dd"));
            writer.WriteEndElement();
            writer.WriteStartElement("changefreq");
            writer.WriteString(_changeFreq.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("priority");
            writer.WriteString(priority.ToString());
            writer.WriteEndElement();
            writer.WriteEndElement();
            _countOfFound++;
        }

    }
}

public enum ChangeFrequency
{
    always = 1, hourly, daily, weekly, monthly, yearly, never
}

public enum SitemapType { Baidu = 1, Google, Sougou, Sousou }