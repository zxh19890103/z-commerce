using Nt.BLL;
using Nt.DAL;
using Nt.Model;
using Nt.Model.Common;
using Nt.Model.Setting;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Nt.Framework.Admin
{
    public class PageBase : AjaxiblePage
    {
        /// <summary>
        /// 0
        /// </summary>
        public const int IMPOSSIBLE = 0;
        public const string TAG_TD_BEGIN = "<td>";
        public const string TAG_TD_BEGIN_HALF = "<td";
        public const string TAG_TD_END = "</td>";
        public const string TAG_TH_BEGIN = "<th>";
        public const string TAG_TH_BEGIN_HALF = "<th";
        public const string TAG_TH_END = "</th>";
        protected string _permissionSysN = string.Empty;
        protected bool _authorizable = true;

        #region Service
        PermissionRecordProvider _permissionRecordProvider = null;
        Logger _logger = null;
        MailSender _mailSender = null;
        UserService _userService = null;
        HtmlRenderer _htmlRenderer = null;
        MediaService _mediaService = null;
        #endregion

        #region Public Properties

        /// <summary>
        /// 是否Post请求
        /// </summary>
        public bool IsHttpPost { get { return Request.HttpMethod == "POST"; } }

        /// <summary>
        /// 授权服务
        /// </summary>
        public PermissionRecordProvider PermissionRecordProvider { get { return _permissionRecordProvider; } }

        /// <summary>
        /// 登录日志服务
        /// </summary>
        public Logger Logger { get { return _logger; } }

        /// <summary>
        /// 邮件发送
        /// </summary>
        public MailSender MailSender { get { return _mailSender; } }

        /// <summary>
        /// html输出类
        /// </summary>
        public HtmlRenderer HtmlRenderer { get { return _htmlRenderer; } }

        /// <summary>
        /// 管理员
        /// </summary>
        public UserService UserService { get { return _userService; } }

        /// <summary>
        /// 图片处理程序
        /// </summary>
        public MediaService MediaService { get { return _mediaService; } }

        View_Permission _currentPermission;
        /// <summary>
        /// 当前授权项
        /// </summary>
        public View_Permission CurrentPermission
        {
            get
            {
                if (_currentPermission == null && _authorizable)
                {
                    string filter = string.Format("SystemName='{0}'", PermissionSysN);
                    _currentPermission = DbAccessor.GetFirstOrDefault<View_Permission>(filter, string.Empty);
                    _authorizable = _currentPermission != null;
                }
                return _currentPermission;
            }
        }

        /// <summary>
        /// 一个值，指示当前页面是否支持授权
        /// </summary>
        public bool Authorizable { get { return _authorizable; } }

        /// <summary>
        /// 当前会员
        /// </summary>
        protected new View_User User
        {
            get { return NtContext.Current.CurrentUser; }
        }

        /// <summary>
        /// 授权项系统名（唯一）
        /// </summary>
        public virtual string PermissionSysN
        {
            get
            {
                if (string.IsNullOrEmpty(_permissionSysN))
                {
                    _permissionSysN = Request.Url.AbsolutePath;
                    _permissionSysN = _permissionSysN.Substring(1, _permissionSysN.Length - 6).Replace('/', '.');
                }
                return _permissionSysN;
            }
        }

        WebsiteInfoSetting _websiteInfo;
        /// <summary>
        /// 网站设置
        /// </summary>
        public WebsiteInfoSetting WebsiteInfo
        {
            get
            {
                if (_websiteInfo == null)
                    _websiteInfo = SettingService.GetSettingModel<WebsiteInfoSetting>();
                return _websiteInfo;
            }
        }

        /// <summary>
        /// 是否依赖于用户
        /// </summary>
        public virtual bool BasedOnUser { get { return true; } }

        DbAccessor _db;
        /// <summary>
        /// 数据库访问类
        /// </summary>
        protected DbAccessor DB
        {
            get
            {
                if (_db == null)
                    _db = new DbAccessor();
                return _db;
            }
        }

        #endregion

        #region override

        /// <summary>
        /// 检查授权
        /// </summary>
        void CheckAuthorized()
        {
            var context = Nt.BLL.NtContext.Current;
            var currentUser = context.CurrentUser;
            /*如果未登陆则跳转到login.aspx*/
            if (BasedOnUser)
            {
                if (currentUser == null)
                {
                    ExecuteJS("未登录！", "window.location.href='/netin/login.aspx';", 3);
                }
                else
                {
                    //授权检测
                    //如果CurrentPermission为空，表示该页面不设权限
                    if (CurrentPermission != null
                        && !_userService.Authorized(currentUser.UserLevelId, CurrentPermission.Id))
                    {
                        Response.Redirect("/netin/notAuthorized.aspx", true);
                    }
                }
            }
        }

        /// <summary>
        /// 在这里初始化必要的字段
        /// </summary>
        protected override void InitRequiredVar()
        {
            _permissionRecordProvider = new PermissionRecordProvider();
            _logger = new Logger();
            _mailSender = new MailSender();
            _userService = new UserService();
            _htmlRenderer = new HtmlRenderer();
            _mediaService = new MediaService();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CheckAuthorized();
        }

        #endregion

        #region Table

        /// <summary>
        /// table标签和thead标签一起渲染,包括tbody的开始标签
        /// </summary>
        /// <param name="tableName">table的id</param>
        protected virtual void BeginTable(params string[] titles)
        {
            Response.Write("<table");
            Response.Write(" class=\"adminListView\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
            Response.Write("<thead>");
            Response.Write("<tr>");
            Th(titles);
            Response.Write("</tr>");
            Response.Write("</thead>");
            Response.Write("<tbody>");
            Response.Write("<tr></tr>");
        }

        /// <summary>
        /// tbody结束标签和tfoot标签以及table结束标签
        /// </summary>
        /// <param name="footConfig">用于配置tfoot内容的方法代理</param>
        protected virtual void EndTable(Action footConfig)
        {
            if (footConfig == null)
            {
                EndTable();
                return;
            }
            Response.Write("</tbody>");
            Response.Write("<tfoot>");
            Response.Write("<tr>");
            footConfig();
            Response.Write("</tr>");
            Response.Write("</tfoot>");
            Response.Write("</table>");
        }

        /// <summary>
        /// 结束table
        /// </summary>
        protected void EndTable()
        {
            Response.Write("</tbody>");
            Response.Write("</table>");
        }

        #endregion

        #region TR Renderer(行)

        /// <summary>
        /// 配置一个行的Html输出方式
        /// </summary>
        /// <typeparam name="E">行类型</typeparam>
        /// <param name="rowConfig">渲染行html的代理方法</param>
        /// <param name="e">用于渲染行的BaseEntity实体</param>
        protected void Row<E>(Action<E> rowConfig, E e)
            where E : BaseEntity, new()
        {
            if (rowConfig == null || e == null)
                return;
            Response.Write("<tr id=\"row_" + e.Id + "\">");
            rowConfig(e);
            Response.Write("</tr>");
        }

        /// <summary>
        /// 配置一个行的Html输出方式
        /// </summary>
        /// <typeparam name="E">行类型</typeparam>
        /// <param name="config">渲染行html的代理方法</param>
        /// <param name="e">用于渲染行的BaseEntity实体</param>
        protected void Row<E>(Action config, E e)
            where E : BaseEntity, new()
        {
            if (config == null)
                return;
            Response.Write("<tr id=\"row_" + e.Id + "\">");
            config();
            Response.Write("</tr>");
        }

        /// <summary>
        /// 配置一个行的Html输出方式
        /// </summary>
        /// <param name="config">渲染行html的代理方法</param>
        protected void Row(Action config)
        {
            if (config == null)
                return;
            Response.Write("<tr>");
            config();
            Response.Write("</tr>");
        }

        #endregion

        #region TD Renderer（单元格）

        /// <summary>
        /// th标签
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="w">宽</param>
        protected void Th(string title, int w)
        {
            Response.Write(TAG_TH_BEGIN_HALF);
            if (w > 0)
                Response.Write(" width=\"" + w + "\"");
            Response.Write(">");
            Response.Write(title);
            Response.Write("</th>");
        }

        /// <summary>
        /// 多th标签
        /// </summary>
        /// <param name="more">标题序列</param>
        protected void Th(params string[] more)
        {
            if (more != null && more.Length > 0)
            {
                foreach (string item in more)
                {
                    Response.Write(TAG_TH_BEGIN);
                    Response.Write(item);
                    Response.Write(TAG_TH_END);
                }
            }
        }

        /// <summary>
        /// td标签
        /// </summary>
        /// <param name="text">内容文本</param>
        protected void Td(object text)
        {
            Response.Write(TAG_TD_BEGIN);
            Response.Write(text);
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 多td标签
        /// </summary>
        /// <param name="more">内容序列</param>
        protected void Td(params string[] more)
        {
            Response.Write(TAG_TD_BEGIN);
            if (more != null && more.Length > 0)
            {
                foreach (string item in more)
                    Response.Write(item);
            }
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 格式化td标签
        /// </summary>
        /// <param name="format">格式化字符串</param>
        /// <param name="args">格式化参数</param>
        protected void TdF(string format, params object[] args)
        {
            Td(string.Format(format, args));
        }

        /// <summary>
        /// td标签
        /// </summary>
        /// <param name="config">用于生成内容的执行方法</param>
        /// <param name="colSpan">colspan属性值</param>
        protected void Td(Action config, int colSpan)
        {
            Response.Write(TAG_TD_BEGIN_HALF);
            if (colSpan > 1)
                Response.Write(" colspan=\"" + colSpan + "\"");
            Response.Write(">");
            if (config != null) config();
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// td标签
        /// </summary>
        /// <param name="config">用于生成内容的执行方法</param>
        protected void Td(Action config)
        {
            Response.Write(TAG_TD_BEGIN);
            if (config != null) config();
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 空内容td标签
        /// </summary>
        protected void Td()
        {
            Response.Write(TAG_TD_BEGIN);
            Response.Write(TAG_TD_END);
        }

        #endregion

        #region 复杂内容单元格
        /// <summary>
        /// 单元格|循环记录时，排序单元格
        /// </summary>
        /// <param name="id">当前记录id</param>
        /// <param name="order">当前排序值</param>
        protected void TdOrder(int id, int order)
        {
            Response.Write(TAG_TD_BEGIN);
            Response.Write("<input type=\"text\" class=\"input-number order-item\" data-item-id=\"");
            Response.Write(id);
            Response.Write("\"  value=\"");
            Response.Write(order);
            Response.Write("\"/>");
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 单元格|对当前记录的Boolean型数据字段的Ajax设置按钮的Html
        /// </summary>
        /// <param name="id">当前记录id</param>
        /// <param name="currentValue">当前的bool值</param>
        /// <param name="name">字段名</param>
        protected void TdSetBoolean(int id, bool currentValue, string name)
        {
            Response.Write(TAG_TD_BEGIN);
            Response.Write("<a href=\"javascript:;\" class=\"ajax-set-boolean");
            if (currentValue)
                Response.Write(" boolean-yes");
            Response.Write("\"");
            Response.Write(" onclick=\"setBoolean(this,'");
            Response.Write(name);
            Response.Write("',");
            Response.Write(id);
            Response.Write(");\"></a>");
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 单元格|重新排序按钮
        /// </summary>
        protected void TdReOrder()
        {
            Response.Write(TAG_TD_BEGIN);
            Response.Write("<a href=\"javascript:;\" title=\"重新排序\" class=\"nt-btn-reorder\" onclick=\"reOrder();\"></a>");
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 单元格|全部选择按钮
        /// </summary>
        protected void TdSelectAll()
        {
            Response.Write(TAG_TD_BEGIN);
            Response.Write("<input type=\"checkbox\" onchange=\"selectAll(this);\"/>");
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 单元格|删除所选按钮
        /// </summary>
        protected void TdDelSelected()
        {
            TdDelSelected(false);
        }

        /// <summary>
        ///  单元格|删除所选按钮(Ajax)
        /// </summary>
        /// <param name="deletable">是否为可回收型</param>
        protected void TdDelSelected(bool deletable)
        {
            Response.Write(TAG_TD_BEGIN);
            DelSelectedButton(deletable);
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 单元格|空内容
        /// </summary>
        /// <param name="colSpan">td的Colspan属性的值</param>
        protected void TdSpan(int colSpan)
        {
            Response.Write(TAG_TD_BEGIN_HALF);
            Response.Write(" colspan=\"");
            Response.Write(colSpan);
            Response.Write("\"");
            Response.Write(">");
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 单元格|选择控件Html(checkbox)
        /// </summary>
        /// <param name="id">当前记录的id</param>
        protected void TdKey(int id)
        {
            Response.Write(TAG_TD_BEGIN);
            Response.Write("<input type=\"checkbox\" class=\"key-item\" value=\"");
            Response.Write(id);
            Response.Write("\"/>");
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 默认宽高均为80像素
        /// </summary>
        /// <param name="src">图片的源</param>
        protected void TdImage(string src)
        {
            TdImage(src, 80, 80);
        }

        /// <summary>
        /// 指定宽高
        /// </summary>
        /// <param name="src">图片的源</param>
        /// <param name="size">宽高像素值</param>
        protected void TdImage(string src, int size)
        {
            TdImage(src, size, size);
        }

        /// <summary>
        /// 图片单元格
        /// </summary>
        /// <param name="src">源</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        protected void TdImage(string src, int width, int height)
        {
            Response.Write(TAG_TD_BEGIN);
            Response.Write("<img width=\"");
            Response.Write(width);
            Response.Write("\" height=\"");
            Response.Write(height);
            Response.Write("\" src=\"");
            Response.Write(src);
            Response.Write("\"/>");
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 单元格中嵌套一个a标签
        /// </summary>
        /// <param name="href">href值或onclick值</param>
        /// <param name="text">文本</param>
        protected void TdAnchor(string hrefOrOnClick, string text)
        {
            Response.Write(TAG_TD_BEGIN);
            Anchor(hrefOrOnClick, text);
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 单元格中嵌套一个a标签 a-button 为样式
        /// </summary>
        /// <param name="hrefOrOnClick">href值或onclick值</param>
        /// <param name="text">文本</param>
        protected void TdAButton(string hrefOrOnClick, string text)
        {
            Response.Write(TAG_TD_BEGIN);
            Anchor(hrefOrOnClick, text, "a-button");
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// a标签
        /// </summary>
        /// <param name="hrefOrOnClick">href的值或onclick的值</param>
        /// <param name="text">文本</param>
        /// <param name="style">样式</param>
        protected void Anchor(string hrefOrOnClick, string text, string style)
        {
            string href = "javascript:;", onclick = "";
            if (Regex.IsMatch(hrefOrOnClick,
@"^(((http[s]?://)?[\w\.\s\/]+\.?[\w]+(\?[&=\w\s\.%#]+)?)|(javascript:))$", RegexOptions.IgnoreCase))
                href = hrefOrOnClick;
            else
                onclick = hrefOrOnClick;
            Response.Write("<a href=\"");
            Response.Write(href);
            Response.Write("\" class=\"");
            Response.Write(style);
            Response.Write("\" onclick=\"");
            Response.Write(onclick);
            Response.Write("\">");
            Response.Write(text);
            Response.Write("</a>");
        }

        /// <summary>
        /// 使用默认样式的a标签：a-normal
        /// </summary>
        /// <param name="hrefOrOnClick">href的值或onclick的值</param>
        /// <param name="text">文本</param>
        protected void Anchor(string hrefOrOnClick, string text)
        {
            Anchor(hrefOrOnClick, text, "a-normal");
        }

        /// <summary>
        /// class为a-button的a元素
        /// </summary>
        /// <param name="hrefOrOnClick">href的值或onclick的值</param>
        /// <param name="text">文本</param>
        protected void AButton(string hrefOrOnClick, string text)
        {
            Anchor(hrefOrOnClick, text, "a-button");
        }

        /// <summary>
        /// 用于输出删除当前选择的a元素
        /// </summary>
        protected void DelSelectedButton()
        {
            DelSelectedButton(false);
        }

        /// <summary>
        /// 用于输出删除当前选择的a元素
        /// </summary>
        /// <param name="deletable">是否为可回收型</param>
        protected void DelSelectedButton(bool deletable)
        {
            Anchor(string.Format("delSelected({0});", deletable ? "true" : "false"), "删除所选", "a-button");
        }

        /// <summary>
        /// 输出指定的html
        /// </summary>
        /// <param name="html">html字符串</param>
        /// <param name="args">格式化参数</param>
        protected void Html(string html, params object[] args)
        {
            if (args == null || args.Length == 0)
                Response.Write(html);
            else
                Response.Write(string.Format(html, args));
        }

        #endregion

        #region Render Utility

        /// <summary>
        /// 列表时操作列的内容，此方法适用于Ajax操作
        /// 包含：编辑行和删除行
        /// </summary>
        /// <param name="id">当前记录的id</param>
        protected void TdEditViaAjax(int id)
        {
            Response.Write(TAG_TD_BEGIN);
            Response.Write("<a href=\"javascript:;\" data-id=\"");
            Response.Write(id);
            Response.Write("\"  class=\"edit\"></a>");
            Response.Write("<a href=\"javascript:;\" data-id=\"");
            Response.Write(id);
            Response.Write("\"  class=\"del\"></a>");
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 适用于Ajax操作的删除选项
        /// 删除所选的记录
        /// 添加新记录
        /// </summary>
        protected void TdEditViaAjax()
        {
            Response.Write(TAG_TD_BEGIN);
            Response.Write("<a href=\"javascript:;\" class=\"a-button\" role=\"del-selected-button\">删除选项</a>");
            AddNewButtonViaAjax();
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 删除行按钮Html
        /// </summary>
        /// <param name="id">当前记录id</param>
        protected void DelRowAnchor(int id)
        {
            DelRowAnchor(id, false, false);
        }

        /// <summary>
        /// 删除行按钮Html,此删除按钮考虑表是否Tree型数据
        /// </summary>
        /// <param name="id">当前记录id</param>
        /// <param name="tree">一个Bool值，指示是否为Tree型数据</param>
        protected void DelRowAnchor(int id, bool tree)
        {
            DelRowAnchor(id, tree, false);
        }

        /// <summary>
        /// 删除行按钮Html,此删除按钮表是否为可回收型（包含Deleted字段）
        /// </summary>
        /// <param name="detetable">一个Bool值，指示是否为可回收型数据（包含Deleted字段）</param>
        /// <param name="id">当前记录id</param>
        protected void DelRowAnchor(bool detetable, int id)
        {
            DelRowAnchor(id, false, detetable);
        }

        /// <summary>
        /// 删除行按钮Html,此删除按钮考虑表是否为Tree型数据,以及是否为可回收型（包含Deleted字段）
        /// </summary>
        /// <param name="id">当前记录id</param>
        /// <param name="tree">一个Bool值，指示是否为Tree型数据</param>
        /// <param name="detetable">一个Bool值，指示是否为可回收型数据（包含Deleted字段）</param>
        protected void DelRowAnchor(int id, bool tree, bool detetable)
        {
            Response.Write("<a href=\"javascript:;\"");
            Response.Write(" onclick=\"");
            Response.Write("delRow(");
            Response.Write(id);
            Response.Write(",");
            Response.Write(tree ? "true" : "false");
            Response.Write(",");
            Response.Write(detetable ? "true" : "false");
            Response.Write(");\"");
            Response.Write(" class=\"del\">");
            Response.Write("</a>");
        }

        /// <summary>
        /// 添加新记录（Ajax）
        /// </summary>
        protected void AddNewButtonViaAjax()
        {
            Response.Write("<a href=\"javascript:;\" class=\"a-button\" role=\"add\">添加</a>");
        }

        #endregion

        #region Utility

        /// <summary>
        /// 弹出提示信息后关闭窗口
        /// </summary>
        /// <param name="message">提示信息</param>
        public void CloseWindow(string message)
        {
            ExecuteJS(message, "window.close();", 3);
        }

        /// <summary>
        /// 弹窗后回到历史RegisterJavaScript
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="history">历史-1，-2，...</param>
        public void History(string message, int history)
        {
            ExecuteJS(message, "window.history.go(" + history + ");", 3);
        }

        /// <summary>
        /// 跳转到某页面
        /// </summary>
        /// <param name="url">页面url</param>
        /// <param name="message">消息</param>
        public virtual void Goto(string url, string message)
        {
            Goto(url, message, 3);
        }

        /// <summary>
        /// 跳转到某页面
        /// </summary>
        /// <param name="url">页面url</param>
        public virtual void Goto(string url)
        {
            Goto(url, "", 3);
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

        /// <summary>
        /// 输出脚本
        /// </summary>
        /// <param name="script">脚本</param>
        public void WriteHtmlPage(string body, string script, string title)
        {
            string template = string.Empty;
            string templatePath = MapPath("/netin/resource/skip.html");
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

            #region default
            Response.Clear();
            Response.AddHeader("Content-Type", "text/html;charset=utf-8");
            Response.AddHeader("CacheControl", "Private");
            Response.Write("<html>");
            Response.Write("<head><title>" + Server.HtmlEncode(title) + "</title>");
            Response.Write("</head>");
            Response.Write("<body style=\"font: 14px/1.5 '\\5FAE\\8F6F\\96C5\\9ED1', Arial,Helvetica,sans-serif; -ms-overflow-style: -ms-autohiding-scrollbar; margin: 5px; padding: 0;background-color: #E6E7E8;\">");
            Response.Write(body);
            Response.Write("<script type=\"text/javascript\">" + script + "</script>");
            Response.Write("</body>");
            Response.Write("</html>");
            Response.End();
            #endregion
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
        /// 字符串格式化函数
        /// </summary>
        /// <param name="format">需要格式化的字符串</param>
        /// <param name="args">格式化参数</param>
        /// <returns></returns>
        public string F(string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        /// 修正后的表名
        /// </summary>
        /// <param name="tab">原始表名</param>
        /// <returns></returns>
        public string M(string tab)
        {
            return DbAccessor.GetModifiedTableName(tab);
        }

        /// <summary>
        /// 修正后的表名
        /// </summary>
        /// <param name="t">BaseEntity的Type</param>
        /// <returns></returns>
        public string M(Type t)
        {
            return DbAccessor.GetModifiedTableName(t.Name);
        }

        /// <summary>
        /// 当请求失败时，使用指定默认值
        /// </summary>
        /// <param name="queryName">请求字段名</param>
        /// <param name="defaultValue">指定值</param>
        /// <returns></returns>
        public object Default(string queryName, object defaultValue)
        {
            if (string.IsNullOrEmpty(Request[queryName]))
                return defaultValue;
            return Request[queryName];
        }

        /// <summary>
        /// 追加标题内容
        /// </summary>
        /// <param name="titlePart">追加的标题部分</param>
        public void AppendTitle(string titlePart)
        {
            Title += "-" + titlePart;
        }

        /// <summary>
        /// 将HTTP请求字段的值解析为int32数据类型
        /// </summary>
        /// <param name="Query">请求字段名</param>
        /// <returns></returns>
        public int ParseInt32(string Query)
        {
            int i = 0;
            int.TryParse(Request[Query], out i);
            return i;
        }

        /// <summary>
        /// 用于ajax获取一个model
        /// </summary>
        /// <typeparam name="M">类型参数</typeparam>
        public void AjaxGet<M>()
            where M : BaseEntity, new()
        {
            int id = IMPOSSIBLE;
            NtJson.EnsureNumber("id", "参数错误：id", out id);
            M m = DbAccessor.GetById<M>(id);
            if (m == null)
                NtJson.ShowError("未发现记录!");
            NtJson json = new NtJson();
            json.ErrorCode = NtJson.OK;
            json.ParseJson(m);
            Response.Write(json);
        }

        /// <summary>
        /// 用于ajax保存一个model
        /// </summary>
        /// <typeparam name="M">类型参数</typeparam>
        public void AjaxSave<M>()
            where M : BaseEntity, new()
        {
            M m = new M();
            m.InitDataFromPage();
            DbAccessor.UpdateOrInsert(m);
            NtJson.ShowOK();
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <typeparam name="M">类型参数</typeparam>
        public void AjaxDel<M>()
            where M : BaseEntity, new()
        {
            int id = 0;
            NtJson.EnsureNumber("id", "参数错误:id", out id);
            DbAccessor.Delete(typeof(M), id);
            NtJson.ShowOK();
        }

        #endregion

    }
}
