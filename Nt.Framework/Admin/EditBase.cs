using Nt.DAL;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Nt.BLL;

namespace Nt.Framework.Admin
{
    /// <summary>
    /// 编辑页面基类
    /// </summary>
    public abstract class EditBase : PageBase
    {
        #region Props

        int _ntid = IMPOSSIBLE;
        /// <summary>
        /// id
        /// </summary>
        public int NtID { get { return _ntid; } }

        /// <summary>
        /// 是否编辑
        /// </summary>
        public bool IsEdit { get; set; }

        int _maxID = IMPOSSIBLE;
        /// <summary>
        /// 最大id
        /// </summary>
        public int MaxID
        {
            get
            {
                if (_maxID == IMPOSSIBLE)
                {
                    _maxID = DbAccessor.GetMaxID(TableName);
                }
                return _maxID;
            }
        }

        /// <summary>
        /// 编辑页标题的前缀部分（修改 或 添加）
        /// </summary>
        public string EditPageTitlePrefix
        {
            get { return IsEdit ? "修改" : "添加"; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public abstract string TableName { get; }
        public abstract string ListPagePath { get; }

        int _pageIndex = 1;
        public int PageIndex { get { return _pageIndex; } }

        #endregion

        #region  virtual methods

        public abstract void Get();
        public abstract void Post();
        protected virtual void Prepare() { }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Int32.TryParse(Request.QueryString["page"], out _pageIndex);
            if (_pageIndex == 0) _pageIndex = 1;
            if (Request["id"] != null)
            {
                IsEdit = Int32.TryParse(Request["id"], out _ntid) && _ntid > 0;
            }
            Prepare();
            if (IsHttpPost)
                Post();
            else
                Get();
        }

        #region Utility

        /// <summary>
        /// 未发现记录
        /// </summary>
        /// <param name="url">跳转的页面</param>
        public void NotFound(string url)
        {
            Goto(url, string.Format("没有发现ID为{0}的记录!", NtID));
        }

        /// <summary>
        /// 未发现记录  url=ListPagePath
        /// </summary>
        public void NotFound()
        {
            Goto(ListPagePath, string.Format("没有发现ID为{0}的记录!", NtID));
        }

        /// <summary>
        /// 返回到列表页的js脚本
        /// </summary>
        /// <param name="onclick">是否click时间触发</param>
        public void BackScript(bool onclick)
        {
            Response.Write(
                string.Format("{2}window.location.href='{0}?{1}&id={3}';",
                ListPagePath,
                PageIndex == 0 ? "" : "page=" + PageIndex,
                onclick ? string.Empty : "javascript:", NtID));
        }

        /// <summary>
        /// 返回到列表页的js脚本.onclick=false
        /// </summary>
        public void BackScript()
        {
            BackScript(false);
        }

        #endregion

    }

    /// <summary>
    /// 编辑页面泛型基类
    /// </summary>
    /// <typeparam name="M">视图Model</typeparam>
    /// <typeparam name="E">数据Model</typeparam>
    public abstract class EditBase<M, E> : EditBase
        where M : BaseEntity, new()
        where E : BaseEntity, new()
    {
        string _tableName = string.Empty;

        /// <summary>
        /// 表名
        /// </summary>
        public override string TableName
        {
            get
            {
                if (_tableName == string.Empty)
                    _tableName = typeof(E).Name;
                return _tableName;
            }
        }

        /// <summary>
        /// 视图实体
        /// </summary>
        public M Model { get; protected set; }

        /// <summary>
        /// 获取视图
        /// </summary>
        public override void Get()
        {
            if (IsEdit)
            {
                Model = DbAccessor.GetById<M>(NtID);
                if (Model == null)
                    NotFound();
            }
            else
            {
                Model = new M();
                Model.InitData();
            }
        }

        /// <summary>
        /// 提交实体
        /// </summary>
        public override void Post()
        {
            var e = new E();
            e.InitDataFromPage();
            DbAccessor.UpdateOrInsert(e);
            Goto(string.Format("{0}?page={1}", ListPagePath, PageIndex), "保存成功!");
        }

        /// <summary>
        /// 列表页面
        /// </summary>
        public override string ListPagePath
        {
            get { return "list.aspx"; }
        }
    }

    /// <summary>
    /// 编辑页面泛型基类
    /// </summary>
    /// <typeparam name="E">视图Model，也是数据Model</typeparam>
    public abstract class EditBase<E> : EditBase<E, E>
         where E : BaseEntity, new()
    {

    }

}
