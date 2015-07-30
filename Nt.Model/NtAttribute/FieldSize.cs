using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.NtAttribute
{
    /// <summary>
    /// 字段长度，默认1024
    /// </summary>
    public class FieldSizeAttribute : Attribute
    {
        private int _size = 1024;

        public FieldSizeAttribute(int size)
        {
            if (size < 1)
                return;
            _size = size;
        }

        public FieldSizeAttribute() { }

        /// <summary>
        /// 字段长度
        /// </summary>
        public string Size
        {
            get
            {
                return _size == int.MaxValue ? "max" : _size.ToString();
            }
        }
    }
}
