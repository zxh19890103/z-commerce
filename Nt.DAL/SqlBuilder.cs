using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nt.DAL
{
    public class SqlBuilder
    {
        #region static utility

        public static string Combine(string sqlbase, string filter, string orderby)
        {
            if (string.IsNullOrEmpty(sqlbase))
                throw new NtException("{0}不可为空字符串...", sqlbase);
            string sql = sqlbase;
            if (!string.IsNullOrEmpty(filter))
                sql += WHERE + filter;
            if (!string.IsNullOrEmpty(orderby))
                sql += ORDERBY + orderby;
            return sql;
        }

        #endregion

        string _sql;
        bool _whereValued;
        string _where;
        bool _orderbyValued;
        string _orderby;
        bool _pagerable;
        string _table;
        bool _IsSubSelect;
        string _cols = " * ";
        string[] _colsArray;
        const string AND = " AND ";
        const string COMMA = ",";
        const string WHERE = " WHERE ";
        const string ORDERBY = " ORDER BY ";

        #region ctors
        public SqlBuilder()
        { }

        public SqlBuilder(string table)
        {
            Table = table;
        }

        /// <summary>
        ///带有分页的Sql
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="index">当前页</param>
        /// <param name="size">每页显示个数</param>
        public SqlBuilder(string table, int index, int size)
        {
            Table = table;
            _pagerable = true;
            PageIndex = index;
            PageSize = size;
        }

        #endregion

        #region props
        /// <summary>
        /// 获取Sql语句
        /// </summary>
        public string Sql
        {
            get
            {
                if (_pagerable)
                    return GetPagerizedSql();
                else
                {
                    if (_IsSubSelect)
                        _sql = string.Format("Select {1} From {0}", Table, _cols);
                    else
                        _sql = string.Format("Select {1} From [{0}]", Table, _cols);
                    return Combine(_sql, Where, Orderby);
                }
            }
        }

        /// <summary>
        /// 表可以是视图或Select字句
        /// </summary>
        public string Table
        {
            get
            {
                return _table;
            }
            set
            {
                _table = value.Trim();
                BasicTable = _table;//
                //for start as 'select',ignore case
                if (Regex.IsMatch(_table, @"^select", RegexOptions.IgnoreCase))
                {
                    _IsSubSelect = true;
                }
                else
                    _table = DbAccessor.GetModifiedTableName(_table);
            }
        }

        public string BasicTable
        {
            get;
            set;
        }

        /// <summary>
        /// 对表名({0})或[{0}]
        /// </summary>
        public string ModifiedTable
        {
            get { return IsSubSelect ? "(" + _table + ")" : "[" + _table + "]"; }
        }

        /// <summary>
        /// 筛选条件
        /// </summary>
        public string Where
        {
            get { return _where; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                _where = value;
                _whereValued = true;
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public string Orderby
        {
            get { return _orderby; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                _orderby = value;
                _orderbyValued = true;
            }
        }

        /// <summary>
        /// 是否启用分页
        /// </summary>
        public bool Pagerable
        {
            get { return _pagerable; }
            set
            {
                _pagerable = value;
                if (_pagerable)
                {
                    PageSize = 12;
                    PageIndex = 1;
                }
            }
        }

        /// <summary>
        /// 是否以子查询为表
        /// </summary>
        public bool IsSubSelect
        {
            get { return _IsSubSelect; }
        }

        /// <summary>
        /// 字段
        /// </summary>
        public string Cols
        {
            get { return _cols; }
            set { _cols = value; }
        }

        /// <summary>
        /// 字段数组
        /// </summary>
        public string[] ColsArray { get { return _colsArray; } }

        #endregion

        #region methods

        public void InitColsByType(Type t)
        {
            if (t == null) return;
            var ps = t.GetProperties();
            _colsArray = new string[ps.Length];
            int i = 0;
            foreach (var p in ps)
            {
                _colsArray[i] = p.Name;
            }
            _cols = string.Join(",", _colsArray);
        }

        public void InitColsByArray(params string[] cols)
        {
            if (cols != null || cols.Length < 1) return;
            _colsArray=new string[cols.Length];
            int i = 0;
            foreach (var c in cols)
                _colsArray[i++] = c;
            _cols = string.Join(",", _colsArray);
        }

        /// <summary>
        /// 添加排序规则
        /// </summary>
        /// <param name="orderByItems"></param>
        /// <returns></returns>
        public SqlBuilder AddOrderby(params string[] orderByItems)
        {
            if (orderByItems != null && orderByItems.Length > 0)
            {
                foreach (string item in orderByItems)
                {
                    if (string.IsNullOrEmpty(item))
                        continue;
                    if (_orderbyValued)
                        Orderby += COMMA + item;
                    else
                        Orderby = item;
                }
            }
            return this;
        }

        /// <summary>
        /// 添加筛选规则
        /// </summary>
        /// <param name="filterItems"></param>
        /// <returns></returns>
        public SqlBuilder AddFilter(params string[] filterItems)
        {
            if (filterItems != null && filterItems.Length > 0)
            {
                foreach (string item in filterItems)
                {
                    if (string.IsNullOrEmpty(item))
                        continue;
                    if (_whereValued)
                        Where += AND + item;
                    else
                        Where = item;
                }
            }
            return this;
        }

        #region 分页

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        /// <summary>
        /// 处理分页的时候需要知道数据源自哪张表
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="size">页容量</param>
        /// <param name="table">表名</param>
        public void SetPagerable(int index, int size)
        {
            _pagerable = true;
            PageIndex = index;
            PageSize = size;
        }

        /// <summary>
        /// 获取分页的Sql语句
        /// </summary>
        /// <returns></returns>
        protected string GetPagerizedSql()
        {
            _sql = string.Empty;
            string t = ModifiedTable;
            if (PageIndex == 1)
            {
                _sql = string.Format("Select Top {0} {2} From {1} ",
                    PageSize, t, _cols);
                _sql = Combine(_sql, Where, Orderby);
            }
            else
            {
                string subSelect = string.Format("Select Top {0} ID From {1} ",
                    (PageIndex - 1) * PageSize, t);
                subSelect = Combine(subSelect, Where, Orderby);
                _sql = string.Format("Select Top {0} {2} From {1}", PageSize, t, _cols);
                string where = _whereValued ? Where + AND + "ID not in (" + subSelect + ")" : "ID not in (" + subSelect + ")";
                _sql = Combine(_sql, where, Orderby);
            }
            return _sql;
        }

        /// <summary>
        /// 获取计数统计的Sql语句
        /// </summary>
        /// <returns></returns>
        public string GetCountSql()
        {
            _sql = string.Format("Select Count(0)  From {0}\r\n", ModifiedTable);
            return Combine(_sql, Where, null);
        }

        /// <summary>
        /// 获取删除脚本
        /// </summary>
        /// <returns></returns>
        public string GetDeleteSql()
        {
            _sql = string.Format("delete from {0}\r\n", Table);
            return Combine(_sql, Where, null);
        }

        /// <summary>
        /// 获取回收脚本
        /// </summary>
        /// <returns></returns>
        public string GetGCSql()
        {
            _sql = string.Format("update [{0}] set deleted=1\r\n", Table);
            return Combine(_sql, Where, null);
        }

        /// <summary>
        /// 获取头i条记录
        /// </summary>
        /// <param name="top">条数</param>
        /// <returns></returns>
        public string GetTopSql(int top)
        {
            _sql = string.Format("select top {0} * from {1}\r\n", top, ModifiedTable);
            return Combine(_sql, Where, Orderby);
        }

        /// <summary>
        /// 获取最大ID
        /// </summary>
        /// <returns></returns>
        public string GetMaxIDSql()
        {
            _sql = string.Format("select isnull((select max(ID) from {0}),0)\r\n", ModifiedTable);
            return _sql;
        }

        #endregion

        public override string ToString()
        {
            return Sql;
        }

        #endregion

    }
}
