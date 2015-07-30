using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Nt.Framework;
using Nt.BLL;
using Nt.Model;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using Nt.Model.View;
using Nt.DAL;
using System.Configuration;
using System.Reflection;
using Nt.Model.Setting;
using System.ComponentModel;
using Nt.Model.Enum;
using Nt.Model.Common;
using System.Data.SqlClient;

namespace Nt.Framework
{
    /// <summary>
    /// BasePage 的摘要说明
    /// </summary>
    public class BaseAspx : AjaxiblePage
    {
        /// <summary>
        /// 0
        /// </summary>
        public const int IMPOSSIBLE = 0;
        private StringBuilder _cssContent;
        private StringBuilder _jsContent;

        #region Counter
        protected int _counter = 0;
        #endregion

        #region Service
        protected MailSender _mailSender = null;
        protected Logger _logger = null;
        protected MediaService _mediaService = null;
        #endregion

        #region props

        string _seoTitle;
        /// <summary>
        /// 网站标题
        /// </summary>
        public string SeoTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_seoTitle))
                {
                    _seoTitle = CurrentNaviItem.SeoTitle;
                    if (!string.IsNullOrEmpty(CurrentNaviItem.SeoTitle))
                        return string.Format("{0}-{1}", CurrentNaviItem.SeoTitle, WebsiteInfo.SeoTitle);
                    return WebsiteInfo.SeoTitle;
                }
                return string.Format("{0}-{1}", _seoTitle, WebsiteInfo.SeoTitle);
            }
            set { _seoTitle = value; }
        }

        string _seoKeywords;
        /// <summary>
        ///  关键词
        /// </summary>
        public virtual string SeoKeywords
        {
            get
            {
                if (string.IsNullOrEmpty(_seoKeywords))
                {
                    if (!string.IsNullOrEmpty(CurrentNaviItem.SeoKeywords))
                        return CurrentNaviItem.SeoKeywords;
                    return WebsiteInfo.SeoKeywords;
                }
                return _seoKeywords;
            }
            set { _seoKeywords = value; }
        }

        string _seoDescription;
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string SeoDescription
        {
            get
            {
                if (string.IsNullOrEmpty(_seoDescription))
                {
                    if (!string.IsNullOrEmpty(CurrentNaviItem.SeoDescription))
                        return CurrentNaviItem.SeoDescription;
                    return WebsiteInfo.SeoDescription;
                }
                return _seoDescription;
            }
            set { _seoDescription = value; }
        }

        Navigation _currentNaviItem;
        /// <summary>
        /// 当前导航项数据
        /// </summary>
        public Navigation CurrentNaviItem
        {
            get
            {
                if (_currentNaviItem == null)
                {
                    _currentNaviItem = NavigationData.FirstOrDefault(x => x.Id == ChannelNo);
                    if (_currentNaviItem == null)
                    {
                        _currentNaviItem = new Navigation();
                    }
                }
                return _currentNaviItem;
            }
        }

        /// <summary>
        /// 频道ID
        /// </summary>
        public int ChannelNo { get; set; } //channel id,0 means home


        bool _inCustomerArea = false;
        /// <summary>
        /// 检查是否有会员登录,
        /// 如果当前页面依赖于会员,
        /// 则跳到登录页面
        /// </summary>
        protected bool InCustomerArea
        {
            get { return _inCustomerArea; }
            set
            {
                _inCustomerArea = value;
                if (value)
                {
                    var c = NtContext.Current.CurrentCustomer;
                    if (c == null)
                        Goto("/customer/login.aspx?redirectUrl=" + Server.UrlEncode(Request.RawUrl), "未登录!");
                }
            }
        }

        HtmlRenderer _htmlRenderer;
        /// <summary>
        /// Html渲染器
        /// </summary>
        public HtmlRenderer HtmlRenderer { get { return _htmlRenderer; } }

        DbAccessor _db;
        /// <summary>
        /// 数据库访问者
        /// </summary>
        public DbAccessor DB { get { return _db; } }

        #endregion

        #region  Data

        bool _treeDataFetched = false;
        /// <summary>
        /// navi、article、goods、product、pages
        /// </summary>
        protected void FetchTreeData()
        {
            if (_treeDataFetched)
                return;
            DB.List("Navigation", null, "DisplayOrder")
                .List("Article_Class", null, "DisplayOrder")
                .List("Goods_Class", null, "DisplayOrder")
                .List("ProductCategory", null, "DisplayOrder")
                .List("SinglePage", null, null)
                .Execute();
            _treeDataFetched = true;
            _naviData = new List<Navigation>();
            DbAccessor.FetchListByDataTable<Navigation>(DB[0], _naviData, typeof(Navigation));
            _articleClasses = new List<Article_Class>();
            DbAccessor.FetchListByDataTable<Article_Class>(DB[1], _articleClasses, typeof(Article_Class));
            _goodsClasses = new List<Goods_Class>();
            DbAccessor.FetchListByDataTable<Goods_Class>(DB[2], _goodsClasses, typeof(Goods_Class));
            _productCategories = new List<ProductCategory>();
            DbAccessor.FetchListByDataTable<ProductCategory>(DB[3], _productCategories, typeof(ProductCategory));
            _pages = new List<SinglePage>();
            DbAccessor.FetchListByDataTable<SinglePage>(DB[4], _pages, typeof(SinglePage));
        }

        List<Navigation> _naviData;
        /// <summary>
        /// 生成导航的
        /// </summary>
        public List<Navigation> NavigationData
        {
            get
            {
                if (!_treeDataFetched)
                    FetchTreeData();
                return _naviData;
            }
        }

        List<Article_Class> _articleClasses;
        /// <summary>
        /// 新闻类别
        /// </summary>
        public List<Article_Class> ArticleClasses
        {
            get
            {
                if (!_treeDataFetched)
                    FetchTreeData();
                return _articleClasses;
            }
        }

        List<Goods_Class> _goodsClasses;
        /// <summary>
        /// 新闻类别
        /// </summary>
        public List<Goods_Class> GoodsClasses
        {
            get
            {
                if (!_treeDataFetched)
                    FetchTreeData();
                return _goodsClasses;
            }
        }

        List<ProductCategory> _productCategories;
        /// <summary>
        /// 新闻类别
        /// </summary>
        public List<ProductCategory> ProductCategories
        {
            get
            {
                if (!_treeDataFetched)
                    FetchTreeData();
                return _productCategories;
            }
        }

        List<SinglePage> _pages;
        public List<SinglePage> Pages
        {
            get
            {
                if (!_treeDataFetched)
                    FetchTreeData();
                return _pages;
            }
        }

        #endregion

        #region Settings

        private WebsiteInfoSetting _webSiteInfo;
        /// <summary>
        /// 网站基本信息
        /// </summary>
        public WebsiteInfoSetting WebsiteInfo
        {
            get
            {
                if (_webSiteInfo == null)
                {
                    _webSiteInfo = SettingService.GetSettingModel<WebsiteInfoSetting>(
                        NtConfig.CurrentLanguageModel.LanguageCode);
                }
                return _webSiteInfo;
            }
        }

        ProductSetting _pSettings = null;
        /// <summary>
        /// 产品设置
        /// </summary>
        public ProductSetting PSettings
        {
            get
            {
                if (_pSettings == null)
                    _pSettings = SettingService.GetSettingModel<ProductSetting>(NtConfig.CurrentLanguageModel.LanguageCode);
                return _pSettings;
            }
        }

        ArticleSetting _aSettings = null;
        /// <summary>
        /// 文章设置
        /// </summary>
        public ArticleSetting ASettings
        {
            get
            {
                if (_aSettings == null)
                    _aSettings = SettingService.GetSettingModel<ArticleSetting>(NtConfig.CurrentLanguageModel.LanguageCode);
                return _aSettings;
            }
        }

        GoodsSetting _gSettings = null;
        /// <summary>
        /// 商品设置
        /// </summary>
        public GoodsSetting GSettings
        {
            get
            {
                if (_gSettings == null)
                    _gSettings = SettingService.GetSettingModel<GoodsSetting>(NtConfig.CurrentLanguageModel.LanguageCode);
                return _gSettings;
            }
        }

        GuestBookSetting _gbSettings = null;
        /// <summary>
        /// 留言设置
        /// </summary>
        public GuestBookSetting GBSettings
        {
            get
            {
                if (_gbSettings == null)
                    _gbSettings = SettingService.GetSettingModel<GuestBookSetting>(NtConfig.CurrentLanguageModel.LanguageCode);
                return _gbSettings;
            }
        }

        #endregion

        #region override

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            TrySkipToMobileSite();//尝试跳转到手机站

            if (WebsiteInfo.SetWebsiteOff)
            {
                CloseWebsite();//关闭网站
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            NtConfig.EmptyStaticResources();
            DB.Dispose();
        }

        /// <summary>
        /// 初始化一些基本的变量
        /// </summary>
        protected override void InitRequiredVar()
        {
            _mailSender = new MailSender();
            _logger = new Logger();
            _htmlRenderer = new HtmlRenderer();
            _db = new DbAccessor();
            _mediaService = new MediaService();
            _cssContent = new StringBuilder();
            _jsContent = new StringBuilder();
        }

        /// <summary>
        /// 跳转到手机网站
        /// </summary>
        public void TrySkipToMobileSite()
        {
            string mUrl = NtConfig.MobileSiteUrl;
            //没有手机网站
            if (string.IsNullOrEmpty(mUrl)) { }
            else
            {
                string requestUrl = Request.Url.AbsolutePath.ToLower();
                if (mUrl.StartsWith("/"))
                {
                    if (!requestUrl.Contains(mUrl)
                        && IsMobileDevice)
                    {
                        Response.Redirect(mUrl);
                    }
                }
                else if (Regex.IsMatch(mUrl, "^http[s]?://"))
                {
                    if (IsMobileDevice)
                    {
                        Response.Redirect(mUrl);
                    }
                }
            }
        }

        /// <summary>
        /// 关闭网站
        /// </summary>
        protected void CloseWebsite()
        {
            if (File.Exists(MapPath("/html.inc/close.html")))
                Server.Transfer("/html.inc/close.html");
            else
            {
                Response.ContentType = "text/html";
                Response.Write("<div style=\"text-align:center;margin:0 auto;font-size:20px;font-weight:bold;\">网站已经关闭</div>");
                Response.End();
            }
        }

        /// <summary>
        /// 是否是移动设备请求
        /// </summary>
        protected bool IsMobileDevice
        {
            get
            {
                bool flag = false;
                string agent = Request.UserAgent;
                string[] keywords = { "Android", "iPhone", "iPod", "iPad", "Windows Phone", "MQQBrowser" };
                //排除Window 桌面系统 和 苹果桌面系统
                if (!agent.Contains("Windows NT") && !agent.Contains("Macintosh"))
                {
                    foreach (string item in keywords)
                    {
                        if (agent.Contains(item))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                return flag;
            }
        }

        #endregion

        #region 导航

        /// <summary>
        /// 输出导航，编号001
        /// </summary>
        /// <param name="outerTag">导航最外围的标签，一般为：ul</param>
        /// <param name="liTag">列表标签，一般为：li</param>
        /// <param name="currentStyle">当前频道的样式，默认：class="menu-current"</param>
        /// <param name="liTemplate">导航循环项的模板，关键替换字 {text}，{url}，{target}</param>
        /// <param name="depth">递归的深度，0表示一级，1表示递归到二级，...</param>
        /// <param name="wrapAttrs">outerTag的属性，eg: id="nav"</param>
        public void RenderMenu(string outerTag, string liTag, string currentStyle, string liTemplate, int depth, string wrapAttrs)
        {
            string cachePath = MapPath(string.Format("/cache/cache.menu.{0}", depth));
            if (File.Exists(cachePath))
            {
                Response.Write(File.ReadAllText(cachePath));
            }
            else
            {
                _counter = -1;//计数器初始化
                StreamWriter writer = new StreamWriter(cachePath, false, Encoding.UTF8);
                RenderSubMenu(0, outerTag, liTag, currentStyle, liTemplate, depth, wrapAttrs, writer);
                writer.Flush();
                writer.Close();
                writer.Dispose();
            }
        }

        /// <summary>
        /// 输出导航，编号002
        /// outerTag="ul"， 
        /// liTag="li"，
        /// currentStyle="menu-current"，
        /// wrapAttrs=null
        /// </summary>
        /// <param name="liTemplate">导航循环项的模板，关键替换字 {text}，{url}，{target}</param>
        /// <param name="depth">递归的深度，0表示一级，1表示递归到二级，...</param>
        public void RenderMenu(string liTemplate, int depth)
        {
            RenderMenu("ul", "li", "menu-current", liTemplate, depth, null);
        }

        /// <summary>
        /// 输出导航，编号003
        /// outerTag="ul"， 
        /// liTag="li"，
        /// currentStyle="menu-current"，
        /// liTemplate="〈a href=\"{url}\" target=\"{target}\"〉{text}〈/a〉"，
        /// wrapAttrs=null
        /// </summary>
        /// <param name="depth">递归的深度，0表示一级，1表示递归到二级，...</param>
        public void RenderMenu(int depth)
        {
            RenderMenu("ul", "li", "menu-current", "<a href=\"{url}\" target=\"{target}\">{text}</a>", depth, null);
        }

        /// <summary>
        /// 递归函数
        /// </summary>
        private void RenderSubMenu(int pid, string outerTag, string liTag, string currentStyle, string liTemplate, int depth, string wrapAttrs, StreamWriter writer)
        {
            _counter++;
            if (_counter > depth)
            {
                _counter--;
                return;
            }
            IEnumerable<Navigation> data = NavigationData.Where(x => x.Parent == pid);
            if (data != null && data.Count() < 1)
            {
                _counter--;
                return;
            }
            CacheWrite("<", writer);
            CacheWrite(outerTag, writer);
            if (_counter == 0 && !string.IsNullOrEmpty(wrapAttrs))
            {
                CacheWrite(" ", writer);
                CacheWrite(wrapAttrs, writer);
            }
            CacheWrite(">", writer);

            string li = string.Empty;

            foreach (var item in data)
            {
                if (!item.Display)//如果Display属性设置为False，则不显示
                    continue;

                switch ((ModuleType)item.NaviType)
                {
                    case ModuleType.Link:
                        li = liTemplate
                          .Replace("{text}", item.Name)
                          .Replace("{target}", item.AnchorTarget)
                          .Replace("{url}", item.Path);
                        CacheWrite("<", writer);
                        CacheWrite(liTag, writer);
                        if (_counter == 0 && item.Id == ChannelNo)
                        {
                            CacheWrite(" class=", writer);
                            CacheWrite("\"", writer);
                            CacheWrite(currentStyle, writer);
                            CacheWrite("\"", writer);
                        }
                        CacheWrite(">", writer);
                        CacheWrite(li, writer);
                        CacheWrite("</", writer);
                        CacheWrite(liTag, writer);
                        CacheWrite(">", writer);
                        break;
                    case ModuleType.Article:
                        foreach (var subitem in _articleClasses.Where(x => x.Parent == item.RootSid && x.Display))
                        {
                            li = liTemplate
                              .Replace("{text}", subitem.Name)
                              .Replace("{target}", item.AnchorTarget)
                              .Replace("{url}", subitem.ListTemplate + "?sortid=" + subitem.Id);
                            CacheWrite("<", writer);
                            CacheWrite(liTag, writer);
                            CacheWrite(">", writer);
                            CacheWrite(li, writer);
                            CacheWrite("</", writer);
                            CacheWrite(liTag, writer);
                            CacheWrite(">", writer);
                        }
                        break;
                    case ModuleType.Goods:
                        foreach (var subitem in _goodsClasses.Where(x => x.Parent == item.RootSid && x.Display))
                        {
                            li = liTemplate
                              .Replace("{text}", subitem.Name)
                              .Replace("{target}", item.AnchorTarget)
                              .Replace("{url}", subitem.ListTemplate + "?sortid=" + subitem.Id);
                            CacheWrite("<", writer);
                            CacheWrite(liTag, writer);
                            CacheWrite(">", writer);
                            CacheWrite(li, writer);
                            CacheWrite("</", writer);
                            CacheWrite(liTag, writer);
                            CacheWrite(">", writer);
                        }
                        break;
                    case ModuleType.Page://单页面
                        if (string.IsNullOrEmpty(item.PageIds))
                            break;
                        string[] pageIds = item.PageIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var subitem in _pages)
                        {
                            if (pageIds.Contains(subitem.Id.ToString()))
                            {
                                li = liTemplate
                                  .Replace("{text}", subitem.Title)
                                  .Replace("{target}", item.AnchorTarget)
                                  .Replace("{url}", subitem.DetailTemplate + "?id=" + subitem.Id);
                                CacheWrite("<", writer);
                                CacheWrite(liTag, writer);
                                CacheWrite(">", writer);
                                CacheWrite(li, writer);
                                CacheWrite("</", writer);
                                CacheWrite(liTag, writer);
                                CacheWrite(">", writer);
                            }
                        }
                        break;
                    case ModuleType.Product:
                        foreach (var subitem in _productCategories.Where(x => x.Parent == item.RootSid && x.Display))
                        {
                            li = liTemplate
                              .Replace("{text}", subitem.Name)
                              .Replace("{target}", item.AnchorTarget)
                              .Replace("{url}", subitem.ListTemplate + "?sortid=" + subitem.Id);
                            CacheWrite("<", writer);
                            CacheWrite(liTag, writer);
                            CacheWrite(">", writer);
                            CacheWrite(li, writer);
                            CacheWrite("</", writer);
                            CacheWrite(liTag, writer);
                            CacheWrite(">", writer);
                        }
                        break;
                    case ModuleType.Download:
                        //not support yet！
                        break;
                    case ModuleType.Folder:
                        li = liTemplate
                          .Replace("{text}", item.Name)
                          .Replace("{target}", item.AnchorTarget)
                          .Replace("{url}", item.Path);
                        CacheWrite("<", writer);
                        CacheWrite(liTag, writer);
                        if (_counter == 0 && item.Id == ChannelNo)
                        {
                            CacheWrite(" class=", writer);
                            CacheWrite("\"", writer);
                            CacheWrite(currentStyle, writer);
                            CacheWrite("\"", writer);
                        }
                        CacheWrite(">", writer);
                        CacheWrite(li, writer);
                        RenderSubMenu(item.Id, outerTag, liTag, currentStyle, liTemplate, depth, wrapAttrs, writer);
                        CacheWrite("</", writer);
                        CacheWrite(liTag, writer);
                        CacheWrite(">", writer);
                        break;
                    default:
                        break;
                }
            }
            CacheWrite("</", writer);
            CacheWrite(outerTag, writer);
            CacheWrite(">", writer);
            _counter--;
        }

        #endregion

        #region 侧边导航

        /// <summary>
        /// 侧边导航
        /// </summary>
        /// <param name="pid">导航父级id</param>
        /// <param name="outerTag">导航最外围的标签，例如ul</param>
        /// <param name="liTag">列表标签，一般为li</param>
        /// <param name="currentStyle">当前频道的样式，class="left-menu-current"</param>
        /// <param name="liTemplate">导航循环项的模板，关键替换字 {text}，{url}，{target}</param>
        /// <param name="depth">递归的深度，0表示一级，1表示递归到二级，...</param>
        public void RenderLeftMenu(int pid, string outerTag, string liTag, string currentStyle, string liTemplate, int depth, string wrapAttrs)
        {
            string cachePath = MapPath(string.Format("/cache/cache.menu.sub.{0}.{1}", pid, depth));
            if (File.Exists(cachePath))
            {
                Response.Write(File.ReadAllText(cachePath));
            }
            else
            {
                _counter = 0;
                StreamWriter writer = new StreamWriter(cachePath, false, Encoding.UTF8);
                RenderSubMenu(pid, outerTag, liTag, currentStyle, liTemplate, depth, wrapAttrs, writer);
                writer.Flush();
                writer.Close();
                writer.Dispose();
            }
        }

        /// <summary>
        /// 侧边导航
        /// outerTag="ul"，
        /// liTag="li"，
        /// currentStyle="left-menu-current"
        /// </summary>
        /// <param name="pid">导航父级id</param>
        /// <param name="liTemplate">导航循环项的模板，关键替换字 {text}，{url}，{target}</param>
        /// <param name="depth">递归的深度，0表示一级，1表示递归到二级...</param>
        public void RenderLeftMenu(int pid, string liTemplate, int depth)
        {
            RenderLeftMenu(pid, "ul", "li", "left-menu-current", liTemplate, depth, null);
        }

        /// <summary>
        /// 侧边导航
        /// outerTag="ul"，
        /// liTag="li"，
        /// currentStyle="left-menu-current"，
        /// liTemplate="〈a href=\"{url}\" target=\"{target}\"〉{text}〈/a〉"，
        /// </summary>
        /// <param name="pid">导航父级id</param>
        /// <param name="depth">递归的深度，0表示一级，1表示递归到二级...</param>
        public void RenderLeftMenu(int pid, int depth)
        {
            RenderLeftMenu(pid, "ul", "li", "left-menu-current", "<a href=\"{url}\" target=\"{target}\">{text}</a>", depth, null);
        }

        /// <summary>
        /// 侧边导航
        /// pid=ChannelNo
        /// outerTag="ul"，
        /// liTag="li"，
        /// currentStyle="left-menu-current"，
        /// liTemplate="〈a href=\"{url}\" target=\"{target}\"〉{text}〈/a〉"，
        /// </summary>
        /// <param name="depth">递归的深度，0表示一级，1表示递归到二级...</param>
        public void RenderLeftMenu(int depth)
        {
            RenderLeftMenu(ChannelNo, "ul", "li", "left-menu-current", "<a href=\"{url}\" target=\"{target}\">{text}</a>", depth, null);
        }

        #endregion

        #region 新闻、产品、商品分类信息输出

        int _sortId = 0;
        /// <summary>
        /// 新闻、产品、商品类别Id
        /// Request["sortid"]
        /// </summary>
        public int SortID
        {
            get
            {
                if (_sortId == IMPOSSIBLE)
                {
                    int.TryParse(Request["sortid"], out _sortId);
                }
                return _sortId;
            }
            set { _sortId = value; }
        }

        int _cds = 1;
        /// <summary>
        /// 设置分类数据源
        /// 1：文章
        /// 2：产品
        /// 3：商品
        /// </summary>
        /// <param name="cds">Catalog Data Source</param>
        public void SetCatalogDataSource(int cds)
        {
            if (cds < 1 || cds > 3)
                throw new Exception("CatalogDataSource的值只能为1、2、3");
            _cds = cds;
        }

        /// <summary>
        /// 输出类别的树形层次
        /// </summary>
        /// <param name="sortid">类别id</param>
        /// <param name="outerTag">外围标签</param>
        /// <param name="liTag">循环标签</param>
        /// <param name="currentStyle">当前样式</param>
        /// <param name="liTemplate">项模板{text},{value}</param>
        /// <param name="depth">递归深度</param>
        /// <param name="wrapAttrs">外围标签(ul)的属性</param>        
        public void RenderCatalog(int sortid, string outerTag, string liTag, string currentStyle, string liTemplate, int depth, string wrapAttrs)
        {
            string cachePath = MapPath(string.Format("/cache/cache.catalog.{0}.{1}.{2}", _cds, sortid, depth));
            if (File.Exists(cachePath))
            {
                Response.Write(File.ReadAllText(cachePath));
            }
            else
            {
                _counter = -1;
                StreamWriter writer = new StreamWriter(cachePath, false, Encoding.UTF8);
                try
                {
                    if (_cds == 1)//文章
                        RenderSubCatalog(ArticleClasses, sortid, outerTag, liTag, currentStyle, liTemplate, depth, wrapAttrs, writer);
                    else if (_cds == 2)//产品
                        RenderSubCatalog(ProductCategories, sortid, outerTag, liTag, currentStyle, liTemplate, depth, wrapAttrs, writer);
                    else if (_cds == 3)//商品
                        RenderSubCatalog(GoodsClasses, sortid, outerTag, liTag, currentStyle, liTemplate, depth, wrapAttrs, writer);
                    else
                        throw new Exception("指定的CatalogDataSource值不合法!");
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    writer.Flush();
                    writer.Close();
                    writer.Dispose();
                }
            }
        }

        /// <summary>
        /// 递归函数
        /// </summary>
        void RenderSubCatalog<T>(IEnumerable<T> dataSource, int sortid, string outerTag, string liTag, string currentStyle, string liTemplate, int depth, string wrapAttrs, StreamWriter writer)
        where T : BaseTreeEntity, new()
        {
            _counter++;
            if (_counter > depth)
            {
                _counter--;
                return;
            }

            IEnumerable<T> data = dataSource.Where(x => x.Parent == sortid);

            if (data != null && data.Count() < 1)
            {
                _counter--;
                return;
            }

            CacheWrite("<", writer);
            CacheWrite(outerTag, writer);
            if (_counter == 0 && !string.IsNullOrEmpty(wrapAttrs))
            {
                CacheWrite(" ", writer);
                CacheWrite(wrapAttrs, writer);
            }
            CacheWrite(">", writer);

            foreach (var item in data)
            {
                if (!item.Display) continue;

                CacheWrite("<", writer);
                CacheWrite(liTag, writer);
                if (_counter == 0 && SortID == item.Id)
                {
                    CacheWrite(" class=", writer);
                    CacheWrite("\"", writer);
                    CacheWrite(currentStyle, writer);
                    CacheWrite("\"", writer);
                }
                CacheWrite(">", writer);

                CacheWrite(liTemplate
                .Replace("{text}", item.Name)
                .Replace("{value}", item.Id.ToString()), writer);

                RenderSubCatalog(dataSource, item.Id, outerTag, liTag, currentStyle, liTemplate, depth, wrapAttrs, writer);

                CacheWrite("</", writer);
                CacheWrite(liTag, writer);
                CacheWrite(">", writer);
            }
            CacheWrite("</", writer);
            CacheWrite(outerTag, writer);
            CacheWrite(">", writer);
            _counter--;
        }

        /// <summary>
        /// 输出类别的树形层次
        /// </summary>
        /// <param name="sortid">类别id</param>
        /// <param name="currentStyle">当前样式</param>
        /// <param name="liTemplate">项模板{text},{value}</param>
        /// <param name="depth">递归深度</param>
        /// <param name="wrapAttrs">外围标签(ul)的属性</param>
        public void RenderCatalog(int sortid, string currentStyle, string liTemplate, int depth, string wrapAttrs)
        {
            RenderCatalog(sortid, "ul", "li", currentStyle, liTemplate, depth, wrapAttrs);
        }

        /// <summary>
        /// 输出类别的树形层次
        /// </summary>
        /// <param name="sortid">类别id</param>
        /// <param name="liTemplate">项模板{text},{value}</param>
        /// <param name="depth">递归深度</param>
        /// <param name="wrapAttrs">外围标签(ul)的属性</param>
        public void RenderCatalog(int sortid, string liTemplate, int depth, string wrapAttrs)
        {
            RenderCatalog(sortid, "ul", "li", "catalog-active", liTemplate, depth, wrapAttrs);
        }

        /// <summary>
        /// 输出类别的树形层次
        /// </summary>
        /// <param name="sortid">类别id</param>
        /// <param name="depth">递归深度</param>
        /// <param name="liTemplate">项模板{text},{value}</param>
        public void RenderCatalog(int sortid, int depth, string liTemplate)
        {
            RenderCatalog(sortid, "ul", "li", "catalog-active", liTemplate, depth, null);
        }

        #endregion

        #region 面包屑导航Crumbs

        /// <summary>
        /// 输出面包屑导航
        /// 从指定的sid回溯到根级root， root值可以自己设置
        /// </summary>
        /// <param name="root">根级root</param>
        /// <param name="sid">当前类别的id</param>
        /// <param name="liTemplate">项模板</param>
        /// <param name="separator">分隔符</param>
        public void RenderCrumbs(int root, int sid, string liTemplate, string separator)
        {
            string cachePath = MapPath(string.Format("/cache/cache.crumbs.{0}.{1}.{2}", _cds, root, sid));
            if (File.Exists(cachePath))
            {
                Response.Write(File.ReadAllText(cachePath));
            }
            else
            {
                StreamWriter writer = new StreamWriter(cachePath, false, Encoding.UTF8);
                try
                {
                    if (_cds == 1)//文章
                        RenderCrumbs(ArticleClasses, root, sid, liTemplate, separator, writer);
                    else if (_cds == 2)//产品
                        RenderCrumbs(ProductCategories, root, sid, liTemplate, separator, writer);
                    else if (_cds == 3)//商品
                        RenderCrumbs(GoodsClasses, root, sid, liTemplate, separator, writer);
                    else
                        throw new Exception("指定的CatalogDataSource值不合法!");
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    writer.Flush();
                    writer.Close();
                    writer.Dispose();
                }
            }
        }

        /// <summary>
        /// 输出面包屑导航
        /// 从指定的sid回溯到根级root， root值可以自己设置
        /// </summary>
        /// <param name="sid">当前类别的id</param>
        /// <param name="liTemplate">项模板</param>
        /// <param name="separator">分隔符</param>
        public void RenderCrumbs(int sid, string liTemplate, string separator)
        {
            RenderCrumbs(0, sid, liTemplate, separator);
        }

        /// <summary>
        /// 输出面包屑导航
        /// 从指定的sid回溯到根级root， root值可以自己设置
        /// sid=SortID
        /// </summary>
        /// <param name="liTemplate">项模板</param>
        /// <param name="separator">分隔符</param>
        public void RenderCrumbs(string liTemplate, string separator)
        {
            RenderCrumbs(0, SortID, liTemplate, separator);
        }

        /// <summary>
        /// 输出面包屑导航
        /// separator=&gt;
        /// </summary>
        /// <param name="liTemplate">项模板</param>
        public void RenderCrumbs(string liTemplate)
        {
            RenderCrumbs(0, SortID, liTemplate, "&gt;");
        }

        /// <summary>
        /// 内部渲染面包屑导航方法
        /// </summary>
        void RenderCrumbs<T>(List<T> dataSource, int root, int sid, string liTemplate, string separator, StreamWriter writer)
            where T : BaseTreeEntity, new()
        {
            if (root == sid) return;

            T n = dataSource.FirstOrDefault(x => x.Id == sid);
            //没有发现节点
            if (n == null) return;
            string crumbs = n.Crumbs;
            int pos = 0;
            if (root > 0 && (pos = crumbs.IndexOf("," + root + ",")) > -1)
                crumbs = crumbs.Substring(pos);
            IEnumerable<T> subD = dataSource.Where(x =>
            {
                if (crumbs.IndexOf("," + x.Id + ",") > -1)
                    return true;
                return false;
            }).OrderBy(x => x.Depth);

            foreach (var t in subD)
            {
                CacheWrite(separator, writer);
                CacheWrite(liTemplate.Replace("{value}", t.Id.ToString()).Replace("{text}", t.Name), writer);
            }

        }

        #endregion

        #region Pager  页码

        /// <summary>
        /// use inner
        /// </summary>
        /// <param name="template">template</param>
        /// <param name="text">text</param>
        /// <param name="value">value</param>
        /// <returns>pager li html</returns>
        void _RenderPagerLi(string liTag, string template, string text, int value, string style)
        {
            Response.Write("<");
            Response.Write(liTag);
            Response.Write(" class=\"");
            Response.Write(style);
            Response.Write("\">");

            Response.Write(Regex.Replace(template, @"(\{text\})|(\{value\})|(href="".+"")", (m) =>
                {
                    var val = m.Value;
                    if (val == "{text}")
                        return text;
                    if (val == "{value}")
                        return value.ToString();
                    if (val.StartsWith("href"))
                    {
                        if (value < 1)
                            return "href=\"javascript:;\"";
                        else
                            return Regex.Replace(val, "{value}", value.ToString());
                    }
                    return val;
                }));

            Response.Write("</");
            Response.Write(liTag);
            Response.Write(">");
        }


        /// <summary>
        /// 渲染页码样式
        /// </summary>
        /// <param name="pager">NtPager 一个类型的实例，包含了构建页码的信息</param>
        /// <param name="outerTag">外层标签</param>
        /// <param name="liTag">循环项标签</param>
        /// <param name="liTemplate">循环项内部模板   {value},{text}</param>
        /// <param name="wrapAttrs">外层标签的属性</param>
        /// <param name="first">“首页”显示名  nt-pager-first</param>
        /// <param name="previous">“上一页”显示名  nt-pager-previous</param>
        /// <param name="next">“下一页”显示名  nt-pager-next</param>
        /// <param name="last">“尾页”显示名  nt-pager-last</param>
        protected void RenderPager(NtPager pager,
            string outerTag, string liTag, string liTemplate, string wrapAttrs,
            string first, string previous, string next, string last)
        {
            Response.Write("<");
            Response.Write(outerTag);
            if (!string.IsNullOrEmpty(wrapAttrs))
            {
                Response.Write(" ");
                Response.Write(wrapAttrs);
            }
            Response.Write(">");

            //first
            _RenderPagerLi(liTag, liTemplate, first, pager.FirstPageNo, "nt-pager-first");

            //previous
            _RenderPagerLi(liTag, liTemplate, previous, pager.PrePageNo, "nt-pager-previous");

            //for
            foreach (var i in pager.Pager)
            {
                _RenderPagerLi(liTag, liTemplate, i.Text, Int32.Parse(i.Value), "nt-pager-item");
            }

            //next
            _RenderPagerLi(liTag, liTemplate, next, pager.NextPageNo, "nt-pager-next");

            //last
            _RenderPagerLi(liTag, liTemplate, last, pager.EndPageNo, "nt-pager-last");

            Response.Write("</");
            Response.Write(outerTag);
            Response.Write(">");
        }

        /// <summary>
        /// 渲染页码样式
        /// outerTag="ul",
        /// liTag="li",
        /// first="Home",
        /// previous="Previous",
        /// next="Next",
        /// last="Last",
        /// </summary>
        /// <param name="pager">NtPager 一个类型的实例，包含了构建页码的信息</param>
        /// <param name="liTemplate">循环项内部模板   {value},{text}</param>
        /// <param name="wrapAttrs">外层标签的属性</param>
        protected void RenderPager(NtPager pager, string liTemplate, string wrapAttrs)
        {
            RenderPager(pager, "ul", "li", liTemplate, wrapAttrs, "Home", "Previous", "Next", "Last");
        }

        /// <summary>
        /// 渲染页码样式
        /// outerTag="ul",
        /// liTag="li",
        /// first="Home",
        /// previous="Previous",
        /// next="Next",
        /// last="Last",
        /// wrapAttrs=null
        /// </summary>
        /// <param name="pager">NtPager 一个类型的实例，包含了构建页码的信息</param>
        /// <param name="liTemplate">循环项内部模板   {value},{text}</param>
        protected void RenderPager(NtPager pager, string liTemplate)
        {
            RenderPager(pager, "ul", "li", liTemplate, null, "Home", "Previous", "Next", "Last");
        }

        /// <summary>
        /// 渲染页码样式
        /// outerTag="ul",
        /// liTag="li",
        /// first="Home",
        /// previous="Previous",
        /// next="Next",
        /// last="Last",
        /// wrapAttrs=null,
        /// liTemplate="〈a href="xxx.aspx?page={value}[&sortid=n]"〉{text}〈/a〉"
        /// </summary>
        /// <param name="pager">NtPager 一个类型的实例，包含了构建页码的信息</param>
        protected void RenderPager(NtPager pager)
        {
            string template = "<a href=\"?page={value}\">{text}</a>";
            if (SortID > 0)
                template = "<a href=\"?sortid=" + SortID + "&page={value}\">{text}</a>";
            RenderPager(pager, "ul", "li", template, null, "Home", "Previous", "Next", "Last");
        }

        /// <summary>
        /// 渲染页码样式
        /// </summary>
        /// <param name="pager">NtPager 一个类型的实例，包含了构建页码的信息</param>
        /// <param name="first">首页</param>
        /// <param name="previous">上一页</param>
        /// <param name="next">下一页</param>
        /// <param name="last">尾页</param>
        protected void RenderPager(NtPager pager, string first, string previous, string next, string last)
        {
            string template = "<a href=\"?page={value}\">{text}</a>";
            if (SortID > 0)
                template = "<a href=\"?sortid=" + SortID + "&page={value}\">{text}</a>";
            RenderPager(pager, "ul", "li", template, null, first, previous, next, last);
        }

        /// <summary>
        /// 获取详细页Url
        /// </summary>
        /// <param name="id">内容id</param>
        /// <returns>Url</returns>
        protected string GetDetailUrl(int id)
        {
            return GetDetailUrl(id, Request.Url.AbsolutePath);
        }

        /// <summary>
        /// 获取详细页 Url
        /// </summary>
        /// <param name="id">内容id</param>
        /// <param name="absfilepath">aspx页面的绝对路径</param>
        /// <returns></returns>
        protected string GetDetailUrl(int id, string absfilepath)
        {
            if (id < 1) return "javascript:;";
            if (WebsiteInfo.SetHtmlOn && WebsiteInfo.HtmlMode)
            {
                return string.Format("/html/{0}/{1}.html", _cds, id);
            }
            return absfilepath + "?id=" + id;
        }

        #endregion

        #region 工具库

        /// <summary>
        /// 包含一个文本文件
        /// </summary>
        /// <param name="path">虚拟路径</param>
        public void Include(string path)
        {
            string pathOnDisk = MapPath(path);
            if (File.Exists(pathOnDisk))
            {
                try
                {
                    string c = File.ReadAllText(pathOnDisk);
                    Response.Write(c);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        /// <summary>
        /// 输出脚本
        /// </summary>
        /// <param name="script"></param>
        public void WriteHtmlPage(string body, string script, string title)
        {
            string template = string.Empty;
            string templatePath = MapPath("/html.inc/skip.html");
            if (File.Exists(templatePath))
            {
                template = File.ReadAllText(templatePath);
                Response.Clear();
                Response.Write(template
                    .Replace("##title##", title)
                    .Replace("##body##", body)
                    .Replace("##script##", script));
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.AddHeader("Content-Type", "text/html;charset=utf-8");
                Response.AddHeader("CacheControl", "Private");
                Response.Write("<html>");
                Response.Write("<head><title>" + title + "</title>");
                Response.Write("</head>");
                Response.Write("<body style=\"font: 14px/1.5 '\\5FAE\\8F6F\\96C5\\9ED1', Arial,Helvetica,sans-serif; -ms-overflow-style: -ms-autohiding-scrollbar; margin: 5px; padding: 0;background-color: #E6E7E8;\">");
                Response.Write(body);
                Response.Write("<script type=\"text/javascript\">" + script + "</script>");
                Response.Write("</body>");
                Response.Write("</html>");
                Response.End();
            }
        }

        /// <summary>
        /// 弹出消息对话框，并delay秒后执行指定的脚本，如果脚本为url则跳转
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="scriptOrUrl">Url</param>
        /// <param name="delay">延时(秒)</param>
        public void Goto(string url, string message, int delay)
        {
            message = string.IsNullOrEmpty(message) ? "空消息" : message;
            delay = delay < 3 ? 3 : delay;
            string script = string.Format("var timer={0};var i=setInterval(\"tick();\",1000);function tick(){{timer--;display_timer.innerHTML=timer.toString();if(timer==0){{clearInterval(i);window.location.href='{1}';}}}}", delay, url);
            string html = "<span>" + message + "</span><br/><span id=\"display_timer\">" + delay + "</span>秒后自动跳转.<a href=\"" + url + "\">点击跳转</a>";
            WriteHtmlPage(html, script, message);
        }

        public void Goto(string url, string msg)
        {
            Goto(url, msg, 3);
        }

        public void Goto(string url)
        {
            Goto(url, "", 3);
        }

        /// <summary>
        /// 导向错误页面
        /// 错误文件放在/error目录中
        /// </summary>
        /// <param name="errorCode">错误码</param>
        public void GotoErrorPage(int errorCode)
        {
            Response.Redirect("/error/" + errorCode + ".html", true);
        }

        /// <summary>
        /// 提示消息并执行js
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="script">js</param>
        /// <param name="delay">延时(秒)</param>
        public void ExecuteJS(string message, string script, int delay)
        {
            message = string.IsNullOrEmpty(message) ? "空消息" : message;
            delay = delay < 3 ? 3 : delay;
            string v_script = string.Format("var timer={0};var i=setInterval(\"tick();\",1000);function tick(){{timer--;display_timer.innerHTML=timer.toString();if(timer==0){{clearInterval(i);{1};}}}}", delay, script);
            string html = "<span>" + message + "</span><br/><span id=\"display_timer\">" + delay + "</span>秒后自动执行.<a href=\"javascript:;\" onclick=\"timer=1;\">点击执行</a>";
            WriteHtmlPage(html, v_script, message);
        }

        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="src">源图片</param>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        /// <returns></returns>
        public string Thumb(string src, int w, int h)
        {
            return _mediaService.MakeThumbnail(src, h, w, ThumbnailGenerationMode.HW);
        }

        /// <summary>
        /// 向页面head标签加入样式文件 link
        /// </summary>
        protected void RegisterStyle(string urlOfCssFile)
        {
            _cssContent.AppendFormat("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", urlOfCssFile);
            _cssContent.AppendLine();
        }

        /// <summary>
        /// 向页面head标签加入脚本文件 script
        /// </summary>
        protected void RegisterScript(string urlOfScriptFile)
        {
            _cssContent.AppendFormat("<script type=\"text/javascript\" src=\"{0}\"></script>", urlOfScriptFile);
            _cssContent.AppendLine();
        }

        /// <summary>
        /// 在head标签内部输出Css和js引用
        /// </summary>
        public void RenderCssAndJsContent()
        {
            if (_cssContent.Length > 0)
                Response.Write(_cssContent);
            if (_jsContent.Length > 0)
                Response.Write(_jsContent);
        }

        #endregion

        /// <summary>
        /// 计算上一篇和下一篇
        /// </summary>
        public void CalcSiblings(ISiblingsTraceable istM, string filter, string orderby)
        {
            Type t = istM.GetType();
            string tablename = t.Name;
            string namefield = "name";
            string sql = File.ReadAllText(WebHelper.MapPath("/app_data/script/calc.siblings.sql"));
            if (istM is Article)
                namefield = "title";
            if (string.IsNullOrEmpty(filter)) filter = "1=1";
            if (string.IsNullOrEmpty(orderby)) orderby = "1";
            sql = sql
                .Replace("{namefield}", namefield)
                .Replace("{tablename}", tablename)
                .Replace("{filter}", filter)
                .Replace("{orderby}", orderby)
                .Replace("{specifiedid}", istM.Id.ToString());
            using (SqlConnection conn = new SqlConnection(SqlHelper.GetConnSting()))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                SqlDataReader rs = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (rs.Read())
                {
                    istM.PreID = rs.GetInt32(0);
                    istM.PreTitle = rs.GetString(1);
                    istM.NextID = rs.GetInt32(2);
                    istM.NextTitle = rs.GetString(3);

                    if (string.IsNullOrEmpty(istM.PreTitle))
                        istM.PreTitle = "No Previous.";

                    if (string.IsNullOrEmpty(istM.NextTitle))
                        istM.PreTitle = "No Next.";

                }
                conn.Close();
            }
        }
        
        /// <summary>
        /// 带html缓存的Response.Write方法
        /// </summary>
        public void CacheWrite(object obj, StreamWriter writer)
        {
            Response.Write(obj);
            writer.Write(obj);
        }

        /// <summary>
        /// 带html缓存的Response.Write方法
        /// </summary>
        public void CacheWrite(string s, StreamWriter writer)
        {
            Response.Write(s);
            writer.Write(s);
        }

    }
}