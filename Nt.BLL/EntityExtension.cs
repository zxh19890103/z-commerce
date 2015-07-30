using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;

namespace Nt.BLL
{
    public static class EntityExtension
    {
        /// <summary>
        /// 子页面获取model的数据
        /// </summary>
        /// <param name="m">model</param>
        public static void InitDataFromPage(this  BaseModel m)
        {
            var properties = m.GetType().GetProperties();
            foreach (var item in properties)
            {
                if (item.PropertyType.Name == Nt.DAL.CSBaseType.Guid)
                    continue;
                var value = WebHelper.Request[item.Name];
                DAL.GenericUtility.SetValueToProp(item, m, value);
            }
        }

        /// <summary>
        /// 初始化Model数据
        /// </summary>
        /// <param name="m">model</param>
        public static void InitData(this BaseModel m)
        {
            var properties = m.GetType().GetProperties();
            foreach (var item in properties)
            {
                string code = item.PropertyType.Name;
                if (code == Nt.DAL.CSBaseType.Guid)
                    continue;
                item.SetValue(m,
                    DAL.GenericUtility.GetDefaultValueByTypeCode(code), null);
            }
        }

    }
}
