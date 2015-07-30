using Nt.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nt.BLL
{
    public class BaseService : IService
    {
        /// <summary>
        /// 用于组合Sql语句的StringBuilder
        ///  </summary>
        private StringBuilder _sqlBuilder;

        public StringBuilder SqlBuilder
        {
            get { return _sqlBuilder == null ? (_sqlBuilder = new StringBuilder()) : _sqlBuilder; }
        }

        /// <summary>
        /// 商城系统全局变量上下文
        /// </summary>
        public NtContext NtContext { get { return NtContext.Current; } }

        private DbAccessor _db;
        /// <summary>
        /// 数据库上下文
        /// </summary>
        public DbAccessor DB { get { return _db == null ? (_db = new DbAccessor()) : _db; } }

        public BaseService()
        {

        }
        
    }
}
