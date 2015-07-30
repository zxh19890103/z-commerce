using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_User_Permission : Permission, IView
    {
        public int PermissionId { get; set; }
        public int UserLevelId { get; set; }
        public int MaxID { get; set; }

        public string GetScript()
        {
            string sql =
               " Select * From "+
               " (Select PermissionId,UserLevelId From ##Permission_UserLevel_Mapping##) As T0" +
               " ,"+
               " (Select *,0 As MaxID From ##Permission##) As T1"+
               " Where T0.PermissionId=T1.Id";
            return sql;
        }
    }
}
