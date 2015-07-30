using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.NtAttribute
{
    public class FKConstraintAttribute : Attribute
    {
        private string _tab;
        private string _field;

        public FKConstraintAttribute(string tab, string field)
        {
            _tab = tab;
            _field = field;
        }

        public FKConstraintAttribute(Type t, string field)
        {
            _tab = t.Name;
            _field = field;
        }

        /// <summary>
        /// 外键表
        /// </summary>
        public string ForeignTableName { get { return _tab; } }

        /// <summary>
        /// 外键字段
        /// </summary>
        public string Field { get { return _field; } }
    }
}
