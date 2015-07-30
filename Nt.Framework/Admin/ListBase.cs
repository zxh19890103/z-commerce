using Nt.DAL;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Nt.BLL;

namespace Nt.Framework.Admin
{
    /// <summary>
    /// 列表页基类
    /// </summary>
    public abstract class ListBase : PageBase
    {
        #region Props
        /// <summary>
        /// 表的名称
        /// </summary>
        public abstract string TableName { get; }
        /// <summary>
        /// 列表数据输出
        /// </summary>
        public abstract void List();
        /// <summary>
        /// 编辑页面的路径
        /// </summary>
        public abstract string EditPagePath { get; }
        /// <summary>
        /// HttpGet
        /// </summary>
        protected virtual void Get() { }
        /// <summary>
        /// HttpPost
        /// </summary>
        protected virtual void Post() { }
        /// <summary>
        /// 在Get和Post之前调用
        /// </summary>
        protected virtual void Prepare() { }
        /// <summary>
        /// 输出列表时的条件
        /// </summary>
        protected virtual string Where { get { return string.Empty; } }
        /// <summary>
        /// 输出列表时的排序
        /// </summary>
        protected virtual string OrderBy { get { return string.Empty; } }

        internal NtPager _pager;
        /// <summary>
        /// 分页
        /// </summary>
        public NtPager Pager
        {
            get { return _pager; }
            set { _pager = value; }
        }

        #endregion

        #region  Ajax Methods  Utilities

        /// <summary>
        /// 重排序
        /// 所需参数：field, ids, orders;
        /// </summary>
        /// <returns></returns>
        [AjaxAuthorize(AuthorizeFlag.User)]
        public void ReOrder()
        {
            string field, ids, orders;
            NtJson.EnsureNotNullOrEmpty("field", "参数错误!", out field);
            NtJson.EnsureMatch("ids", @"^([1-9]\d*)(,[1-9]\d*)*$", "参数格式错误!", out ids);
            NtJson.EnsureMatch("orders", @"^([0-9]\d*)(,[0-9]\d*)*$", "参数格式错误!", out orders);
            string[] arr_ids = ids.Split(',');
            string[] arr_orders = orders.Split(',');
            string table = DbAccessor.GetModifiedTableName(TableName);
            if (arr_ids.Length != arr_orders.Length)
                NtJson.ShowError("参数组不对等!");

            string sqlbuilder = string.Empty;
            for (int i = 0; i < arr_ids.Length; i++)
            {
                sqlbuilder += string.Format("Update {0} Set [{3}]={1} Where Id={2}\r\n",
                    table, arr_orders[i], arr_ids[i], field);
            }
            if (sqlbuilder != "")
                SqlHelper.ExecuteNonQuery(sqlbuilder);
            NtJson.ShowOK("更新排序成功!");
        }

        /// <summary>
        /// 删除
        /// 所需参数：ids(id),tree[bool]
        /// </summary>
        /// <returns></returns>
        [AjaxAuthorize(AuthorizeFlag.User)]
        public void DeleteOnDB()
        {
            bool tree, detetable;

            bool.TryParse(Request["detetable"], out detetable);
            bool.TryParse(Request["tree"], out tree);

            string ids;
            NtJson.EnsureInt32Range("ids", out ids);
            if (tree)
                DbAccessor.Delete(TableName, "crumbs like '%," + ids + ",%' ", detetable);
            else
                DbAccessor.Delete(TableName, ids, detetable);

            NtJson.ShowOK("删除成功!");
        }

        /// <summary>
        /// 设置bool值（取反）
        /// 所需参数：ids,field
        /// </summary>
        /// <returns></returns>
        [AjaxAuthorize(AuthorizeFlag.User)]
        public void SetBoolean()
        {
            string ids, field;
            NtJson.EnsureMatch("ids", "", "参数格式错误!", out ids);
            NtJson.EnsureNotNullOrEmpty("field", "参数格式错误!", out field);
            string table = DbAccessor.GetModifiedTableName(TableName);
            string sql = string.Format("Update {0} Set [{2}]=1-[{2}] Where id in ({1})",
                table, ids, field);
            SqlHelper.ExecuteNonQuery(sql);
            NtJson.ShowOK();
        }

        #endregion

        #region Render Utility

        /// <summary>
        /// 输出页码信息
        /// </summary>
        /// <param name="query">QueryString字段</param>
        public void RenderPager(string query)
        {
            if (_pager == null) return;
            Response.Write("<form id=\"PagerForm\" name=\"PagerForm\" method=\"get\" action=\"");
            Response.Write(Request.Url.PathAndQuery);
            Response.Write("\">");
            Response.Write("<span class=\"pager\">");
            Response.Write("<a class=\"pager-button pager-info\">");
            Response.Write((Pager.PageIndex - 1) * Pager.PageSize + 1 + "-" + (Pager.PageIndex) * Pager.PageSize);
            Response.Write("&nbsp;of&nbsp;" + Pager.RecordCount + "</a>");
            Response.Write("<a class=\"pager-button pb-first\" href=\"javascript:;\"  onclick=\"goto(" + Pager.FirstPageNo + ");\"></a>");
            Response.Write("<a class=\"pager-button pb-prev\" href=\"javascript:;\"  onclick=\"goto(" + Pager.PrePageNo + ");\"></a>");


            foreach (var item in Pager.Pager)
            {
                Response.Write("<a class=\"pager-button");
                if (item.Selected)
                    Response.Write(" selected");
                Response.Write("\" href=\"javascript:;\" onclick=\"goto(" + item.Value + ");\">");
                Response.Write(item.Text);
                Response.Write("</a>");
            }

            Response.Write("<a class=\"pager-button pb-next\" href=\"javascript:;\"  onclick=\"goto(" + Pager.NextPageNo + ");\"></a>");
            Response.Write("<a class=\"pager-button pb-end\" href=\"javascript:;\"  onclick=\"goto(" + Pager.EndPageNo + ");\"></a>");
            Response.Write("</span>");
            Response.Write("<input type=\"hidden\" name=\"page\" value=\"" + Pager.PageIndex + "\"/>");

            var dict = NtUtility.ParseQuery(query);
            if (dict != null)
            {
                foreach (var item in dict)
                {
                    if (item.Key.Equals("page", StringComparison.OrdinalIgnoreCase))
                        continue;
                    Response.Write("<input type=\"hidden\" name=\"");
                    Response.Write(item.Key);
                    Response.Write("\" value=\"");
                    Response.Write(item.Value);
                    Response.Write("\"/>");
                }
            }

            Response.Write("</form>");
        }

        /// <summary>
        /// 开始table
        /// </summary>
        /// <param name="titles">标题序列</param>
        protected override void BeginTable(params string[] titles)
        {
            Response.Write("<table");
            Response.Write(" id=\"");
            Response.Write(TableName);
            Response.Write("\" class=\"adminListView\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
            Response.Write("<thead>");
            Response.Write("<tr>");
            Th(titles);
            Response.Write("</tr>");
            Response.Write("</thead>");
            Response.Write("<tbody>");
            Response.Write("<tr></tr>");
        }

        /// <summary>
        /// 单元格/翻页的页码序列
        /// </summary>
        /// <param name="query">QueryString字段</param>
        /// <param name="colSpan">单元格colspan属性的值</param>
        protected void TdPager(string query, int colSpan)
        {
            Response.Write(TAG_TD_BEGIN_HALF);
            if (colSpan > 1)
                Response.Write(" colspan=\"" + colSpan + "\"");
            Response.Write(">");
            RenderPager(query);
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 单元格/翻页的页码序列
        /// </summary>
        /// <param name="query">QueryString字段</param>
        protected void TdPager(string query)
        {
            TdPager(query, 1);
        }

        /// <summary>
        /// 单元格/翻页的页码序列
        /// </summary>
        /// <param name="colSpan">单元格colspan属性的值</param>
        protected void TdPager(int colSpan)
        {
            TdPager(Request.Url.Query, colSpan);
        }

        /// <summary>
        /// 单元格/翻页的页码序列
        /// </summary>
        protected void TdPager()
        {
            TdPager(Request.Url.Query, 1);
        }

        /// <summary>
        /// 行编辑
        /// </summary>
        /// <param name="id">当前访问记录的id</param>
        protected void TdEdit(int id)
        {
            Response.Write(TAG_TD_BEGIN);
            EditRowAnchor(id);
            DelRowAnchor(id);
            Response.Write(TAG_TD_END);
        }

        /// <summary>
        /// 行编辑
        /// </summary>
        protected void TdEdit()
        {
            DelSelectedButton();
            AddNewButton();
        }

        /// <summary>
        /// 编辑行按钮HTml
        /// </summary>
        /// <param name="id">当前访问记录的id</param>
        protected void EditRowAnchor(int id)
        {
            EditRowAnchor(id, string.Empty);
        }
        
        /// <summary>
        /// 编辑行按钮HTml
        /// </summary>
        /// <param name="id">当前访问记录的id</param>
        /// <param name="query">href属性上带的QueryString参数</param>
        protected void EditRowAnchor(int id, string query)
        {
            Response.Write("<a href=\"");
            Response.Write(EditPagePath);
            Response.Write("?id=" + id);
            if (_pager != null)
            {
                Response.Write("&page=");
                Response.Write(Pager.PageIndex);
            }
            Response.Write(query);
            Response.Write("\"");
            Response.Write(" class=\"edit-button edit\">");
            Response.Write("</a>");
        }

        /// <summary>
        /// 添加新纪录按钮HTml
        /// </summary>
        protected void AddNewButton()
        {
            Anchor(EditPagePath, "添加", "a-button");
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Prepare();
            if (IsHttpPost)
                Post();
            else
                Get();
        }

    }

    /// <summary>
    /// 列表页泛型基类
    /// </summary>
    /// <typeparam name="M">视图Model</typeparam>
    /// <typeparam name="E">数据Model</typeparam>
    public abstract class ListBase<M, E> : ListBase
        where M : BaseEntity, new()
        where E : BaseEntity, new()
    {
        string _tableName = string.Empty;
        Type _entityType;
        int _total = 0;

        /// <summary>
        /// 实体的类型
        /// </summary>
        public Type EntityType
        {
            get
            {
                if (_entityType == null)
                    _entityType = typeof(E);
                return _entityType;
            }
        }

        public override string TableName
        {
            get
            {
                if (_tableName == string.Empty)
                    _tableName = EntityType.Name;
                return _tableName;
            }
        }

        /// <summary>
        /// 记录的总条数
        /// </summary>
        protected virtual int Total { get { return _total; } }
        /// <summary>
        /// 使用Pager
        /// </summary>
        protected virtual void SetUsePagerOn(int size)
        {
            Pager = new NtPager();
            Pager.PageSize = size;
        }

        /// <summary>
        /// 使用Pager(默认size=12)
        /// </summary>
        protected virtual void SetUsePagerOn()
        {
            SetUsePagerOn(12);
        }

        List<M> _dataSource = null;
        /// <summary>
        /// 列表源数据
        /// </summary>
        protected virtual List<M> DataSource
        {
            get
            {
                if (_dataSource == null)
                {
                    if (Pager == null)
                        _dataSource = DbAccessor.GetList<M>(Where, OrderBy);
                    else
                    {
                        _dataSource = DbAccessor.GetList<M>(Where, OrderBy, Pager.PageIndex, Pager.PageSize);
                        Pager.RecordCount = DbAccessor.Total;
                        _total = DbAccessor.Total;
                    }
                }
                return _dataSource;
            }
            set { _dataSource = value; }
        }

        public override string EditPagePath
        {
            get { return "edit.aspx"; }
        }

        protected int id = IMPOSSIBLE;
        private M _model;
        public M Model { get { return _model; } }
        /// <summary>
        /// 有时会在列表页面编辑(Get)
        /// </summary>
        protected override void Get()
        {
            Int32.TryParse(Request.QueryString["id"], out id);

            if (id > 0)
            {
                _model = DbAccessor.GetById<M>(id);
            }
            if (_model == null)
            {
                _model = new M();
                _model.InitData();
            }
        }

        /// <summary>
        /// 有时会在列表页面编辑(Post)
        /// </summary>
        protected override void Post()
        {
            var e = new E();
            e.InitDataFromPage();
            DbAccessor.UpdateOrInsert(e);
        }

    }

    /// <summary>
    /// 列表页泛型基类
    /// </summary>
    /// <typeparam name="E">数据Model</typeparam>
    public abstract class ListBase<E> : ListBase<E, E>
        where E : BaseEntity, new()
    {
    }
}
