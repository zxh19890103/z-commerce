using Nt.Model;
using Nt.Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nt.DAL
{
    public class DbAccessor : IEnumerator<DataTable>, IEnumerable<DataTable>, IDisposable
    {
        #region static

        static int total = 0;
        static int maxId = 0;

        /// <summary>
        /// 不需要更新的字段
        /// </summary>
        public static string[] ExcludeFieldsWithoutUpdate
        {
            get { return new string[] { "Id", "UserId", "LanguageId", "Guid" }; }
        }

        static int Counter = 0;

        #region utility

        /// <summary>
        /// 获取M类型实体列表数据，数据源为提供的DataTable
        /// </summary>
        /// <typeparam name="M">M类型</typeparam>
        /// <param name="source">数据源</param>
        /// <returns>实体列表</returns>
        public static List<M> CreateListByDataTable<M>(DataTable source)
            where M : BaseEntity, new()
        {
            List<M> data = new List<M>();
            FetchListByDataTable<M>(source, data, typeof(M));
            return data;
        }

        public static List<M> FetchListByDataTable<M>(DataTable source, List<M> data, Type t)
             where M : BaseEntity, new()
        {
            M m = null;
            var properties = t.GetProperties();
            foreach (DataRow r in source.Rows)
            {
                m = new M();
                foreach (var item in properties)
                {
                    GenericUtility.SetValueToProp(item, m, r[item.Name]);
                }
                data.Add(m);
            }
            return data;
        }

        static void FetchListBySql<M>(string sql, string sql4count, string sql4maxid, List<M> data, Type t, out int total, out int maxId)
           where M : BaseEntity, new()
        {

            DataTable rawD = new DataTable();

            using (var conn = new SqlConnection(SqlHelper.GetConnSting()))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = 60;
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(rawD);

                cmd.CommandText = sql4count;
                total = (int)cmd.ExecuteScalar();

                cmd.CommandText = sql4maxid;
                maxId = (int)cmd.ExecuteScalar();

                conn.Close();
                cmd.Dispose();
                adp.Dispose();
            }

            FetchListByDataTable<M>(rawD, data, t);

            rawD.Dispose();

        }

        static void FetchListBySql<M>(SqlBuilder sql, List<M> data, Type t)
          where M : BaseEntity, new()
        {
            FetchListBySql<M>(sql.ToString(), sql.GetCountSql(), sql.GetMaxIDSql(), data, t, out total, out maxId);
        }

        public const string TABLE_NAME_PREFIX = "Nt";

        /// <summary>
        /// 获取修正后的表名
        /// </summary>
        /// <param name="table">原始表名</param>
        /// <returns></returns>
        public static string GetModifiedTableName(string table)
        {
            if (table.StartsWith("Nt_"))
                return table;
            if (table.StartsWith("View_"))
                return table;
            if (string.IsNullOrEmpty(TABLE_NAME_PREFIX))
                return table;
            return string.Format("{0}_{1}", TABLE_NAME_PREFIX, table);
        }

        /// <summary>
        /// 获取外键
        /// </summary>
        /// <param name="thisTab"></param>
        /// <param name="foreignTab">外键table</param>
        /// <returns></returns>
        public static string GetForeignKey(string thisTab, string foreignTab)
        {
            return string.Format("FK_{0}_{1}",
                GetModifiedTableName(thisTab),
                GetModifiedTableName(foreignTab));
        }

        /// <summary>
        /// 执行sql语句之前修正sql语句
        /// </summary>
        /// <param name="sql"></param>
        public static void ModifySql(ref string sql)
        {
            sql = "SET ANSI_WARNINGS OFF\r\nDECLARE @newid INT;" + sql;
            sql = sql + "SET ANSI_WARNINGS ON\r\nSelect @@IDENTITY;\r\n";
        }

        /// <summary>
        /// 执行sql语句之前修正sql语句
        /// </summary>
        /// <param name="sql"></param>
        public static void ModifySql(ref StringBuilder sql)
        {
            sql.Insert(0, "SET ANSI_WARNINGS OFF\r\nDECLARE @newid INT;");
            sql.Append("SET ANSI_WARNINGS ON\r\nSelect @@IDENTITY;\r\n");
        }

        #endregion

        #region get local  static

        /// <summary>
        /// get list from db
        /// </summary>
        /// <param name="filter">condition to filterate data</param>
        /// <param name="orderby">order</param>
        /// <returns>DataTable</returns>
        public static List<M> GetList<M>(string filter, string orderby)
            where M : BaseEntity, new()
        {
            Type t = typeof(M);
            string table = t.Name;
            SqlBuilder sql = new SqlBuilder();
            sql.Table = table;
            sql.AddFilter(filter);
            sql.AddOrderby(orderby);
            List<M> data = new List<M>();
            FetchListBySql<M>(sql, data, t);
            return data;
        }

        /// <summary>
        /// get all data
        /// </summary>
        /// <param name="table">table's name</param>
        /// <returns></returns>
        public static List<M> GetList<M>()
            where M : BaseEntity, new()
        {
            return GetList<M>(string.Empty, string.Empty);
        }

        public static List<M> GetList<M>(string filter)
           where M : BaseEntity, new()
        {
            return GetList<M>(filter, string.Empty);
        }

        /// <summary>
        /// get pagerized list from db
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="filter">condition to filterate data</param>
        /// <param name="orderby">order</param>
        /// <param name="pageindex">index of page</param>
        /// <param name="pagesize">the  number of record displaying on each page</param>
        /// <returns>DataTable</returns>
        public static List<M> GetList<M>(string filter, string orderby, int pageindex, int pagesize)
            where M : BaseEntity, new()
        {
            if (pageindex < 1)
                throw new NtException("参数pageindex无效,它必须是正整数");
            if (pagesize < 1)
                throw new NtException("参数pagesize无效,它必须是正整数");
            Type t = typeof(M);
            string table = t.Name;
            SqlBuilder sql = new SqlBuilder(table, pageindex, pagesize);
            sql.AddFilter(filter);
            sql.AddOrderby(orderby);
            List<M> data = new List<M>();
            FetchListBySql<M>(sql, data, t);
            return data;
        }

        /// <summary>
        /// get pagerized list from db
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="orderby">order</param>
        /// <param name="pageindex">index of page</param>
        /// <param name="pagesize">the  number of record displaying on each page</param>
        /// <returns>DataTable</returns>
        public static List<M> GetList<M>(string orderby, int pageindex, int pagesize)
            where M : BaseEntity, new()
        {
            return GetList<M>(string.Empty, orderby, pageindex, pagesize);
        }

        /// <summary>
        ///  get pagerized list from db
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="pageindex">index of current page</param>
        /// <param name="pagesize">the  number of record displaying on each page<</param>
        /// <param name="filter">condition to filterate data</param>
        /// <returns>DataTable</returns>
        public static List<M> GetList<M>(int pageindex, int pagesize, string filter)
            where M : BaseEntity, new()
        {
            return GetList<M>(filter, string.Empty, pageindex, pagesize);
        }

        /// <summary>
        /// get pagerized list from db
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="pageindex">index of current page</param>
        /// <param name="pagesize">the  number of record displaying on each page</param>
        /// <returns>DataTable</returns>
        public static List<M> GetList<M>(int pageindex, int pagesize)
            where M : BaseEntity, new()
        {
            return GetList<M>(string.Empty, string.Empty, pageindex, pagesize);
        }

        /// <summary>
        /// 获取前Top项
        /// </summary>
        /// <typeparam name="M">类型</typeparam>
        /// <param name="top">获取数目</param>
        /// <param name="filter">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public static List<M> Top<M>(int top, string filter, string orderby)
            where M : BaseEntity, new()
        {
            SqlBuilder sql = new SqlBuilder();
            Type t = typeof(M);
            sql.Table = t.Name;
            sql.AddFilter(filter);
            sql.AddOrderby(orderby);
            List<M> data = new List<M>();
            FetchListBySql<M>(sql.GetTopSql(top), sql.GetCountSql(), sql.GetMaxIDSql(), data, t, out total, out maxId);
            return data;
        }

        public static List<M> Top<M>(int top, string orderby)
            where M : BaseEntity, new()
        {
            return Top<M>(top, string.Empty, orderby);
        }

        public static List<M> Top<M>(string filter, int top)
            where M : BaseEntity, new()
        {
            return Top<M>(top, filter, string.Empty);
        }

        public static List<M> Top<M>(int top)
           where M : BaseEntity, new()
        {
            return Top<M>(top, string.Empty, string.Empty);
        }

        #endregion

        #region Props

        /// <summary>
        /// 总数
        /// </summary>
        public static int Total { get { return total; } }

        /// <summary>
        /// 最大id
        /// </summary>
        public static int MaxID { get { return maxId; } }

        #endregion

        #region get count
        /// <summary>
        /// get the number  of data from db with filter
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="filter">condition to filterate the data</param>
        /// <returns>the number of query result</returns>
        public static int GetRecordCount(string table, string filter)
        {
            if (string.IsNullOrEmpty(filter))
                return GetRecordCount(table);
            SqlBuilder sql = new SqlBuilder();
            sql.Table = table;
            sql.AddFilter(filter);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql.GetCountSql()));
        }

        /// <summary>
        /// get the number  of data from db with filter
        /// </summary>
        /// <param name="t">Type Of Entity</param>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public static int GetRecordCount(Type t, string filter)
        {
            return GetRecordCount(t.Name, filter);
        }

        /// <summary>
        /// get the number  of data from db with filter
        /// </summary>
        /// <param name="t">Type Of Entity</param>
        /// <returns></returns>
        public static int GetRecordCount(Type t)
        {
            return GetRecordCount(t.Name);
        }

        /// <summary>
        /// get the number  of data from db with filter
        /// </summary>
        /// <param name="table">table's name</param>
        /// <returns></returns>
        public static int GetRecordCount(string table)
        {
            SqlBuilder sql = new SqlBuilder();
            sql.Table = table;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql.GetCountSql()));
        }

        public static int GetMaxID(string table)
        {
            SqlBuilder sql = new SqlBuilder();
            sql.Table = table;
            var o = SqlHelper.ExecuteScalar(sql.GetMaxIDSql());
            if (o == null || o == DBNull.Value)
                return 0;
            return Convert.ToInt32(o);
        }

        public static int GetMaxID(Type t)
        {
            return GetMaxID(t.Name);
        }

        #endregion

        #region update or insert

        /// <summary>
        /// use this function to update a model which is BaseViewModel
        /// </summary>
        /// <param name="m">model</param>
        /// <param name="exclude">fields should not be updated</param>
        public static void Update(BaseEntity m, params string[] exclude)
        {
            string sql = "";
            SqlParameter[] sqlParameters = null;
            OutSqlToUpdate(m, out sql, out sqlParameters, exclude);
            ModifySql(ref sql);
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnSting(), CommandType.Text, sql, sqlParameters);
        }

        public static void UpdateRange<T>(IEnumerable<T> ms, params string[] exclude)
            where T : BaseEntity, new()
        {
            if (ms == null || ms.Count() < 1)
                return;
            StringBuilder sqlBuilder = new StringBuilder();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            string sql = string.Empty;
            SqlParameter[] sqlParameters_part = null;
            foreach (var item in ms)
            {
                OutSqlToUpdate(item, out sql, out sqlParameters_part, exclude);
                sqlBuilder.AppendLine(sql);
                sqlParameters.AddRange(sqlParameters_part);
            }
            ModifySql(ref sqlBuilder);
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnSting(), CommandType.Text, sqlBuilder.ToString(), sqlParameters.ToArray());
        }

        public static void OutSqlToUpdate(BaseEntity m, out string script, out SqlParameter[] sqlParameters, params string[] exclude)
        {
            Type t = m.GetType();
            var id = m.Id;
            string table = t.Name;
            table = GetModifiedTableName(table);
            var properties = t.GetProperties();
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("if (select count(*) From {0} where id={1})>0\r\n begin\r\n", table, id);
            sql.AppendFormat("UPDATE [{0}] SET", table);
            SqlParameter[] parameters = new SqlParameter[properties.Length];
            int i = 0;
            foreach (var item in properties)
            {
                string code = item.PropertyType.Name;

                if (ExcludeFieldsWithoutUpdate.Any(x =>
                    x.Equals(item.Name, StringComparison.OrdinalIgnoreCase)))
                    continue;

                //the exclude means these specified fields should not be updated
                //so we continue
                if (exclude != null
                    && exclude.Length > 0
                    && exclude.Any(x =>
                        x.Equals(item.Name, StringComparison.OrdinalIgnoreCase)))
                    continue;

                //参数的名字
                string paramName = string.Format("{0}_{1}_{2}", table,
                    item.Name, Counter++);

                if (i == 0)
                    sql.AppendFormat(" [{0}]=@{1}", item.Name, paramName);
                else
                    sql.AppendFormat(",[{0}]=@{1}", item.Name, paramName);
                var value = item.GetValue(m, null);

                if (value == null)
                {
                    parameters[i++] = new SqlParameter("@" + paramName,
                        GenericUtility.GetDefaultValueByTypeCode(code));
                }
                else
                    parameters[i++] = new SqlParameter("@" + paramName, value);
            }
            sql.AppendFormat(" Where [Id]={0};\r\n", id);
            sql.Append("end\r\n");
            script = sql.ToString();
            sqlParameters = parameters;
        }

        /// <summary>
        /// use this function to insert a model to db
        /// this model should be BaseEntity or its subclass
        /// </summary>
        /// <param name="m">this model</param>
        /// <returns>this new id it will be</returns>
        public static int Insert(BaseEntity m)
        {
            string sql = "";
            SqlParameter[] sqlParameters = null;
            OutSqlToInsert(m, out sql, out sqlParameters);
            ModifySql(ref sql);
            var result = SqlHelper.ExecuteScalar(SqlHelper.GetConnection(),
                CommandType.Text, sql, sqlParameters);
            if (result == null)
                throw new Exception("result == null");
            int id = Convert.ToInt32(result);
            m.Id = id;
            return m.Id;
        }

        public static void InsertRange<T>(IEnumerable<T> ms)
            where T : BaseEntity, new()
        {
            if (ms == null || ms.Count() < 1)
                return;
            StringBuilder sqlBuilder = new StringBuilder();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            string sql = string.Empty;
            SqlParameter[] sqlParameters_part = null;
            foreach (var item in ms)
            {
                OutSqlToInsert(item, out sql, out sqlParameters_part);
                sqlBuilder.AppendLine(sql);
                sqlParameters.AddRange(sqlParameters_part);
            }
            ModifySql(ref sqlBuilder);
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnSting(), CommandType.Text, sqlBuilder.ToString(), sqlParameters.ToArray());
        }

        public static void OutSqlToInsert(BaseEntity m, out string script, out SqlParameter[] sqlParameters)
        {
            if (m is IGuidModel)
            {
                var guidM = m as IGuidModel;
                guidM.Guid = Guid.NewGuid();
            }

            Type t = m.GetType();
            var properties = t.GetProperties();
            string table = t.Name;
            table = GetModifiedTableName(table);
            StringBuilder sql = new StringBuilder(string.Format("INSERT INTO [{0}]("
                , table));
            StringBuilder sql_values = new StringBuilder();
            SqlParameter[] parameters = new SqlParameter[properties.Length];
            int i = 0;
            foreach (var item in properties)
            {
                string code = item.PropertyType.Name;
                if (item.Name == "Id")
                    continue;
                string paramName = string.Format("{0}_{1}_{2}", table,
                    item.Name, Counter++);

                if (i == 0)
                {
                    sql.AppendFormat(" [{0}]", item.Name);
                    sql_values.Append("@" + paramName);
                }
                else
                {
                    sql.AppendFormat(",[{0}]", item.Name);
                    sql_values.Append(",@" + paramName);
                }
                var value = item.GetValue(m, null);
                if (value == null)
                {
                    parameters[i++] = new SqlParameter("@" + paramName,
                        GenericUtility.GetDefaultValueByTypeCode(code));
                }
                else
                    parameters[i++] = new SqlParameter("@" + paramName, value);
            }
            sql.AppendFormat(")Values({0});\r\n", sql_values.ToString());

            if (t.BaseType == typeof(BaseTreeEntity))
            {
                sql.Append("set @newid=(Select @@IDENTITY);\r\n");

                BaseTreeEntity tree = (BaseTreeEntity)m;
                if (tree.Parent == 0)
                    sql.AppendFormat("Update [{0}] Set Depth=1,[Crumbs]='0,'+cast(@newid as varchar)+',' Where id=@newid;\r\n", table);
                else
                {
                    string sql4parentCrumbs = string.Format("Select Crumbs From [{0}] Where [Id]={1}", table, tree.Parent);
                    string sql4parentDepth = string.Format("Select Depth From [{0}] Where [Id]={1}", table, tree.Parent);
                    sql.AppendFormat("Update [{0}] Set Depth=({2})+1,Crumbs=({1})+cast(@newid as varchar)+',' Where [Id]=@newid;\r\n",
                        table, sql4parentCrumbs, sql4parentDepth);
                }
            }
            script = sql.ToString();
            sqlParameters = parameters;
        }

        /// <summary>
        /// 更新或插入,根据id是否大于0来判断是更新还是插入
        /// </summary>
        /// <param name="m">需要更新或插入的实体类</param>
        public static void UpdateOrInsert(BaseEntity m)
        {
            if (m.Id > 0)
                Update(m);
            else
                Insert(m);
        }

        #endregion

        #region Delete

        /// <summary>
        /// delete one record
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="id">specified id which you want to delete</param>
        /// <returns>the number of rows which is deleted successfully</returns>
        public static int Delete(string table, int id, bool detetable)
        {
            if (id < 1)
                return 0;
            SqlBuilder sql = new SqlBuilder();
            sql.Table = table;
            sql.AddFilter("id=" + id);
            return SqlHelper.ExecuteNonQuery(detetable ? sql.GetGCSql() : sql.GetDeleteSql());
        }

        public static int Delete(string table, int id)
        {
            return Delete(table, id, false);
        }

        public static int Delete(Type t, int id)
        {
            bool deletable = false;
            deletable = t.GetInterface("IDeletable") != null;
            return Delete(t.Name, id, deletable);
        }

        /// <summary>
        /// delete many record
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="ids">specified ids(separated by comma) which you want to delete</param>
        /// <returns>the number of rows which is deleted successfully</returns>
        public static int Delete(string table, string arg, bool detetable)
        {
            if (string.IsNullOrEmpty(arg))
                throw new NtException("参数arg无效");
            string filter = arg;
            if (Regex.IsMatch(arg, @"^\d+(,\d+)*$"))
                filter = "ID in (" + arg + ") ";
            SqlBuilder sql = new SqlBuilder();
            sql.Table = table;
            sql.AddFilter(filter);
            return SqlHelper.ExecuteNonQuery(detetable ? sql.GetGCSql() : sql.GetDeleteSql());
        }

        public static int Delete(string table, string arg)
        {
            return Delete(table, arg, false);
        }

        public static int Delete(Type t, string arg)
        {
            bool deletable = false;
            deletable = t.GetInterface("IDeletable") != null;
            return Delete(t.Name, arg, deletable);
        }

        #endregion

        #region get one
        /// <summary>
        /// get on model by id
        /// </summary>
        /// <typeparam name="M">type which is BaseModel</typeparam>
        /// <param name="id">the specified id</param>
        /// <returns>Model</returns>
        public static M GetById<M>(int id)
            where M : BaseEntity, new()
        {
            if (id < 1)
                return null;
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                conn.Open();
                Type t = typeof(M);
                SqlBuilder sql = new SqlBuilder(t.Name);
                sql.AddFilter("ID=" + id);
                var properties = t.GetProperties();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql.Sql;
                SqlDataReader rs = cmd.ExecuteReader(CommandBehavior.SingleRow);
                M m = null;
                if (rs.Read())
                {
                    m = new M();
                    foreach (var item in properties)
                        GenericUtility.SetValueToProp(item, m, rs[item.Name]);
                }
                rs.Close();
                rs.Dispose();
                conn.Close();
                conn.Dispose();
                return m;
            }
        }

        /// <summary>
        /// 获取第一个或默认的
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <returns></returns>
        public static M GetFirstOrDefault<M>()
            where M : BaseEntity, new()
        {
            return GetFirstOrDefault<M>(string.Empty, string.Empty);
        }

        public static M GetFirstOrDefault<M>(string orderby)
           where M : BaseEntity, new()
        {
            return GetFirstOrDefault<M>(string.Empty, orderby);
        }

        public static M GetFirstOrDefault<M>(string filter, string orderby)
            where M : BaseEntity, new()
        {
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                conn.Open();
                var t = typeof(M);
                var properties = t.GetProperties();
                SqlCommand cmd = conn.CreateCommand();
                SqlBuilder sql = new SqlBuilder(t.Name);
                sql.AddFilter(filter);
                sql.AddOrderby(orderby);
                cmd.CommandText = sql.Sql;
                SqlDataReader rs = cmd.ExecuteReader(CommandBehavior.SingleRow);
                M m = null;
                if (rs.Read())
                {
                    m = new M();
                    foreach (var item in properties)
                        GenericUtility.SetValueToProp(item, m, rs[item.Name]);
                }
                rs.Close();
                rs.Dispose();
                conn.Close();
                conn.Dispose();
                return m;
            }
        }

        #endregion

        #region exists
        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="t">类型</param>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public bool Exists(Type t, string filter)
        {
            var o = SqlHelper.ExecuteScalar(string.Format("select id from [{0}] where {1};", GetModifiedTableName(t.Name), filter));
            return o != null;
        }

        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="t">类型</param>
        /// <returns></returns>
        public bool Exists(Type t)
        {
            var o = SqlHelper.ExecuteScalar(string.Format("select id from [{0}];", GetModifiedTableName(t.Name)));
            return o != null;
        }

        #endregion

        #endregion

        #region dynamic

        #region Fields
        StringBuilder _sqlBuilder;
        StringBuilder _sqlBuilderToCountRows;
        DataSet _dataContainer;
        Dictionary<int, string> _tabNamesContainer;
        List<SqlParameter> _sqlParams;
        int _counter = -1;
        bool _colsSpecified = false;
        string _cols = string.Empty;//列
        #endregion

        /// <summary>
        /// 存储数据
        /// </summary>
        public DataSet DataContainer
        {
            get
            {
                return _dataContainer;
            }
        }

        /// <summary>
        /// 当前Sql语句
        /// </summary>
        public string CurrentSql
        {
            get
            {
                return _sqlBuilder.ToString();
            }
        }

        public DbAccessor()
        {
            _sqlBuilder = new StringBuilder();
            _sqlBuilderToCountRows = new StringBuilder();
            _tabNamesContainer = new Dictionary<int, string>();
            _sqlParams = new List<SqlParameter>();
        }

        public DataTable this[int index]
        {
            get
            {
                return GetTable(index);
            }
        }

        public DataTable this[string name]
        {
            get
            {
                return GetTable(name);
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        public DbAccessor Execute()
        {
            if (_sqlBuilder.Length < 1)
                throw new NtException("empty query t-sql should not be execute!");
            if (_counter >= 0)
            {
                _sqlBuilderToCountRows.Append("-1 as SqlEnd");
                _sqlBuilder.Append("Select ");
                _sqlBuilder.Append(_sqlBuilderToCountRows.ToString());
            }

            _dataContainer = new DataSet();
            using (var conn = new SqlConnection(SqlHelper.GetConnSting()))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = _sqlBuilder.ToString();
                cmd.Parameters.AddRange(_sqlParams.ToArray());
                cmd.CommandTimeout = 60;
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(_dataContainer);
                conn.Close();
                cmd.Dispose();
                adp.Dispose();
            }

            //empty sqlbuider
            _sqlBuilder.Remove(0, _sqlBuilder.Length);
            _sqlBuilderToCountRows.Remove(0, _sqlBuilderToCountRows.Length);
            _sqlParams.RemoveRange(0, _sqlParams.Count);
            _counter = -1;
            return this;
        }

        /// <summary>
        /// 追加Sql查询语句
        /// </summary>
        /// <param name="sqlFormat">格式字符串</param>
        /// <param name="args">参数</param>
        void AppendSqlFormat(string sqlFormat, params string[] args)
        {
            sqlFormat = sqlFormat.Trim();
            if (!sqlFormat.EndsWith("\r\n"))
                sqlFormat += "\r\n";
            _sqlBuilder.AppendFormat(sqlFormat, args);
        }

        /// <summary>
        /// append sql
        /// </summary>
        /// <param name="sql"></param>
        public void AppendSql(string sql)
        {
            sql = sql.Trim();
            if (Regex.IsMatch(sql, "^select", RegexOptions.IgnoreCase))
                throw new Exception("the Sql cannot be Select Query");
            if (!sql.EndsWith("\r\n"))
                sql += "\r\n";
            _sqlBuilder.Append(sql);
        }

        /// <summary>
        /// 为表附名
        /// </summary>
        /// <param name="name">表名</param>
        /// <returns></returns>
        public DbAccessor AsName(string name)
        {
            if (_tabNamesContainer.ContainsKey(_counter))
                _tabNamesContainer[_counter] = name;
            else
                _tabNamesContainer.Add(_counter, name);
            return this;
        }

        /// <summary>
        /// 添加需要获取的列
        /// </summary>
        /// <param name="cols"></param>
        public DbAccessor Columns(params string[] cols)
        {
            if (cols != null
                && cols.Length > 0)
            {
                foreach (string item in cols)
                {
                    if (_colsSpecified)
                        _cols += "," + item;
                    else
                    {
                        _cols += item;
                        _colsSpecified = true;
                    }
                }
            }
            return this;
        }

        /// <summary>
        /// 重置列
        /// </summary>
        void ResetColumns()
        {
            _colsSpecified = false;
            _cols = string.Empty;
        }

        #region GETList
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="filter">条件</param>
        /// <param name="orderby">排序</param>
        public DbAccessor List(string table, string filter, string orderby)
        {
            if (string.IsNullOrEmpty(table))
                throw new NtException("table should not be empty!");
            SqlBuilder sqlBuilder = new SqlBuilder(table);
            if (_colsSpecified) sqlBuilder.Cols = _cols;
            sqlBuilder.AddFilter(filter);
            sqlBuilder.AddOrderby(orderby);
            AppendSqlFormat(sqlBuilder.Sql);
            _counter++;
            //dont't forget the comma
            _sqlBuilderToCountRows.AppendFormat("({0}) as C{1},", sqlBuilder.GetCountSql(), _counter);
            ResetColumns();
            return this;
        }

        public DbAccessor List(string table)
        {
            return List(table, string.Empty, string.Empty);
        }

        public DbAccessor List(string table, string filter)
        {
            return List(table, filter, string.Empty);
        }

        public DbAccessor List(string table, string filter, string orderby, int pageindex, int pagesize)
        {
            if (string.IsNullOrEmpty(table))
                throw new NtException("table should not be empty!");
            if (pageindex < 1)
                throw new NtException("pageindex must be integer!");
            if (pagesize < 1)
                throw new NtException("pagesize must be integer!");
            SqlBuilder sqlBuilder = new SqlBuilder(table, pageindex, pagesize);
            if (_colsSpecified) sqlBuilder.Cols = _cols;
            sqlBuilder.AddFilter(filter);
            sqlBuilder.AddOrderby(orderby);
            AppendSqlFormat(sqlBuilder.Sql);
            _counter++;
            _sqlBuilderToCountRows.AppendFormat("({0}) as C{1},", sqlBuilder.GetCountSql(), _counter);
            ResetColumns();
            return this;
        }

        public DbAccessor List(string table, string filter, int pageindex, int pagesize)
        {
            return List(table, filter, string.Empty, pageindex, pagesize);
        }

        public DbAccessor List(string table, int pageindex, int pagesize, string orderby)
        {
            return List(table, string.Empty, orderby, pageindex, pagesize);
        }

        public DbAccessor List(string table, int pageindex, int pagesize)
        {
            return List(table, string.Empty, string.Empty, pageindex, pagesize);
        }

        #endregion

        #region Top

        public DbAccessor Top(string table, int topNum, string filter, string orderby)
        {
            SqlBuilder sql = new SqlBuilder();
            sql.Table = table;
            sql.AddFilter(filter);
            sql.AddOrderby(orderby);
            AppendSqlFormat(sql.GetTopSql(topNum));
            return this;
        }

        public DbAccessor Top(string table, int topNum, string filter)
        {
            return Top(table, topNum, filter, string.Empty);
        }

        public DbAccessor Top(string table, int topNum)
        {
            return Top(table, topNum, string.Empty, string.Empty);
        }

        public DbAccessor Top(string table, string orderby, int topNum)
        {
            return Top(table, topNum, string.Empty, orderby);
        }

        #endregion

        #region Get Count And Table

        /// <summary>
        /// 获取记录的条数
        /// </summary>
        /// <param name="name">表名</param>
        /// <returns></returns>
        public int GetTotal(string name)
        {
            if (DataContainer == null)
                throw new NtException("no any table!");
            int index = -1;
            foreach (var item in _tabNamesContainer)
            {
                if (item.Value.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    index = item.Key;
                    break;
                }
            }
            if (index == -1)
                throw new NtException("no table you want!");
            int c = DataContainer.Tables.Count;
            return Convert.ToInt32
            (DataContainer.Tables[c - 1].Rows[0][index]);
        }

        /// <summary>
        /// 获取记录的条数
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public int GetTotal(int index)
        {
            if (DataContainer == null)
                throw new NtException("no any table!");
            int c = DataContainer.Tables.Count;
            return Convert.ToInt32
            (DataContainer.Tables[c - 1].Rows[0][index]);
        }

        /// <summary>
        /// 获取表
        /// </summary>
        /// <param name="name">表名</param>
        /// <returns></returns>
        public DataTable GetTable(string name)
        {
            if (DataContainer == null)
                throw new NtException("no any table!");
            int index = -1;
            foreach (var item in _tabNamesContainer)
            {
                if (item.Value.Equals(name,
                    StringComparison.OrdinalIgnoreCase))
                {
                    index = item.Key;
                    break;
                }
            }
            if (index == -1)
                throw new NtException("no table you want!");
            return DataContainer.Tables[index];
        }

        /// <summary>
        /// 根据系数获取表
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public DataTable GetTable(int index)
        {
            if (DataContainer == null)
                throw new NtException("no any table!");
            return DataContainer.Tables[index];
        }

        #endregion

        #region Add And Change
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public DbAccessor Add(BaseEntity m)
        {
            string sql = string.Empty;
            SqlParameter[] sqlParams = null;
            DbAccessor.OutSqlToInsert(m, out sql, out sqlParams);
            foreach (var item in sqlParams)
            {
                if (item != null)
                    _sqlParams.Add(item);
            }
            AppendSql(sql);
            return this;
        }

        public DbAccessor Change(BaseEntity m, params string[] exclude)
        {
            string sql = string.Empty;
            SqlParameter[] sqlParams = null;
            DbAccessor.OutSqlToUpdate(m, out sql, out sqlParams, exclude);
            foreach (var item in sqlParams)
            {
                if (item != null)
                    _sqlParams.Add(item);
            }
            _sqlBuilder.Append(sql);
            return this;
        }

        #endregion

        #endregion

        #region Tree

        string _space = "&nbsp;&nbsp;";
        /// <summary>
        /// Default  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        /// </summary>
        public string Space
        {
            get { return _space; }
            set { _space = value; }
        }

        string _tree_brand = "├";
        /// <summary>
        /// Default  ├
        /// </summary>
        public string TreeBrand
        {
            get { return _tree_brand; }
            set { _tree_brand = value; }
        }

        string _treeNodeTextStyle = "tree-node-text";
        /// <summary>
        /// 节点文字样式
        /// </summary>
        public string TreeNodeTextStyle
        {
            get { return _treeNodeTextStyle; }
            set { _treeNodeTextStyle = value; }
        }

        int _treeSourceIndex = -1;
        /// <summary>
        /// 在调用GetTree之前,指定层次结构数据源的当前表索引
        /// </summary>
        public int TreeSourceIndex { get { return _treeSourceIndex; } set { _treeSourceIndex = value; } }

        int _ddlSourceIndex = -1;
        /// <summary>
        /// 在调用GetDropdownlist之前，指定下拉列表数据源的当前表索引
        /// </summary>
        public int DDLSourceIndex { get { return _ddlSourceIndex; } set { _ddlSourceIndex = value; } }

        /// <summary>
        /// get category selector order by crumbs,thus its hierarchical structure will be obvious
        /// </summary>
        /// <param name="table">table's name</param>
        /// <returns>listitem list</returns>
        public List<E> GetTree<E>(string filter)
            where E : BaseTreeEntity, new()
        {
            Type t = typeof(E);
            var list = new List<E>();
            List<E> d = new List<E>();
            if (TreeSourceIndex > -1)
                d = FetchListByDataTable<E>(this[TreeSourceIndex], d, t);
            else
                d = DbAccessor.GetList<E>(filter, "DisplayOrder");
            FindSubList(d, 0, list);
            TreeSourceIndex = -1;
            return list;
        }

        public List<E> GetTree<E>()
            where E : BaseTreeEntity, new()
        {
            return GetTree<E>(string.Empty);
        }

        public List<E> GetTree<E>(int treeSourceIndex)
            where E : BaseTreeEntity, new()
        {
            _treeSourceIndex = treeSourceIndex;
            return GetTree<E>();
        }

        public List<NtListItem> GetDropdownlist<E>(string filter)
            where E : BaseTreeEntity, new()
        {
            var data = GetTree<E>(filter);
            List<NtListItem> list = new List<NtListItem>();
            foreach (var item in data)
                list.Add(new NtListItem(item.Name, item.Id));
            return list;
        }

        public List<NtListItem> GetDropdownlist<E>()
            where E : BaseTreeEntity, new()
        {
            return GetDropdownlist<E>(string.Empty);
        }

        void FindSubList<E>(List<E> source, int pid, List<E> addTo)
            where E : BaseTreeEntity, new()
        {
            foreach (var item in source.Where(x => x.Parent == pid))
            {
                string prefix = string.Empty;

                for (int i = 0; i < item.Depth; i++)
                    prefix += Space;
                prefix += TreeBrand;
                item.Name = string.Format(
                    "{0}<span class=\"{1}\">{2}</span>",
                    prefix, TreeNodeTextStyle, item.Name);
                addTo.Add(item);
                FindSubList(source, item.Id, addTo);
            }
        }

        /// <summary>
        /// 获取Dropdownlist数据
        /// </summary>
        /// <typeparam name="E">类型</typeparam>
        /// <param name="textField">文本字段</param>
        /// <param name="valueField">值字段</param>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public List<NtListItem> GetDropdownlist<E>(string textField, string valueField, string filter)
            where E : BaseEntity, new()
        {
            return GetDropdownlist<E>(textField, valueField, filter, null);
        }

        public List<NtListItem> GetDropdownlist<E>(string textField, string valueField)
           where E : BaseEntity, new()
        {
            return GetDropdownlist<E>(textField, valueField, null, null);
        }

        public List<NtListItem> GetDropdownlist<E>(string textField, string valueField, string filter, string orderby)
           where E : BaseEntity, new()
        {
            List<NtListItem> list = new List<NtListItem>();
            DataTable data;

            if (DDLSourceIndex > -1)
            {
                data = this[DDLSourceIndex];
            }
            else
            {
                SqlBuilder sql = new SqlBuilder();
                sql.Table = typeof(E).Name;
                sql.InitColsByArray(textField, valueField);
                sql.AddFilter(filter);
                sql.AddOrderby(orderby);

                data = new DataTable();
                using (SqlConnection conn = new SqlConnection(SqlHelper.GetConnSting()))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.CommandTimeout = 60;
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(data);

                    conn.Close();
                    cmd.Dispose();
                    adp.Dispose();
                }
            }

            foreach (DataRow r in data.Rows)
            {
                list.Add(new NtListItem(r[textField], r[valueField]));
            }

            DDLSourceIndex = -1;

            return list;
        }

        #endregion

        #region for IEnumerator

        int position = -1;//from -1 on

        public DataTable Current
        {
            get { return _dataContainer.Tables[position]; }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return _dataContainer.Tables[position]; }
        }

        public bool MoveNext()
        {
            position++;
            return position < _dataContainer.Tables.Count - 1;
        }

        public void Reset()
        {
            position = -1;
        }

        public IEnumerator<DataTable> GetEnumerator()
        {
            return this;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this;
        }

        #endregion

        public void Dispose()
        {
            if (_dataContainer != null)
                _dataContainer.Dispose();
        }
    }
}
