using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nt.DAL
{
    public static class GenericUtility
    {
        /// <summary>
        /// 根据提供的typecode获取获取默认值
        /// </summary>
        /// <param name="typecode">typecode</param>
        /// <returns></returns>
        public static object GetDefaultValueByTypeCode(string code)
        {
            switch (code)
            {
                case CSBaseType.Boolean:
                    return false;
                case CSBaseType.Char:
                    return Char.MinValue;
                case CSBaseType.DateTime:
                    return DateTime.Now;
                case CSBaseType.Decimal:
                    return 0.0M;
                case CSBaseType.Double:
                    return 0.0D;
                case CSBaseType.Single:
                    return 0.0F;
                case CSBaseType.Int16:
                case CSBaseType.Int32:
                case CSBaseType.Int64:
                case CSBaseType.UInt16:
                case CSBaseType.UInt32:
                case CSBaseType.UInt64:
                case CSBaseType.SByte:
                case CSBaseType.Byte:
                    return 0;
                case CSBaseType.String:
                    return string.Empty;
                case CSBaseType.Guid:
                    return Guid.NewGuid();
                default:
                    throw new Exception("不受支持的数据类型");
            }
        }

        /// <summary>
        /// 给指定的某实例的属性赋值
        /// </summary>
        /// <param name="pi">属性引用</param>
        /// <param name="obj">改属性所属的实例</param>
        /// <param name="value">值</param>
        public static void SetValueToProp(PropertyInfo pi, object obj, object value)
        {
            string code = pi.PropertyType.Name;
            if (value == null
                || string.IsNullOrEmpty(value.ToString()))
                pi.SetValue(obj,
                    GetDefaultValueByTypeCode(code), null);
            else
            {
                #region  set value converted by code
                switch (code)
                {
                    case CSBaseType.Boolean:
                        pi.SetValue(obj, Convert.ToBoolean(value), null);
                        break;
                    case CSBaseType.Byte:
                        pi.SetValue(obj, Convert.ToByte(value), null);
                        break;
                    case CSBaseType.Int16:
                        pi.SetValue(obj, Convert.ToInt16(value), null);
                        break;
                    case CSBaseType.Int32:
                        pi.SetValue(obj, Convert.ToInt32(value), null);
                        break;
                    case CSBaseType.Int64:
                        pi.SetValue(obj, Convert.ToInt64(value), null);
                        break;
                    case CSBaseType.DateTime:
                        pi.SetValue(obj, Convert.ToDateTime(value), null);
                        break;
                    case CSBaseType.String:
                        pi.SetValue(obj, value.ToString().TrimEnd(), null);
                        break;
                    case CSBaseType.Guid:
                        pi.SetValue(obj,(Guid)value, null);
                        break;
                    case CSBaseType.Double:
                        pi.SetValue(obj, Convert.ToDouble(value), null);
                        break;
                    case CSBaseType.Single:
                        pi.SetValue(obj, Convert.ToSingle(value), null);
                        break;
                    case CSBaseType.Decimal:
                        pi.SetValue(obj, Convert.ToDecimal(value), null);
                        break;
                    default:
                        throw new Exception("不接受的数据类型");
                }
                #endregion
            }
        }

    }
}
