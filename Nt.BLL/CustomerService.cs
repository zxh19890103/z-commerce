using Nt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.BLL
{
    public class CustomerService : BaseService
    {
        /// <summary>
        /// 重设密码
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="newpass">新密码</param>
        public void ResetPassword(int id, string newpass)
        {
            SecurityService ss = new SecurityService();
            string md5pass = ss.Md5(newpass);
            string sql = string.Format("update [{0}] Set [Password]='{1}' Where ID={2}", DbAccessor.GetModifiedTableName("Customer"), md5pass, id);
            SqlHelper.ExecuteNonQuery(sql);
        }
    }
}
