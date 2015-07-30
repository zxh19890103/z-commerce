using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Permission_UserLevel_Mapping : BaseEntity
    {
        [NtAttribute.FKConstraint("Permission", "Id")]
        public int PermissionId { get; set; }

        [NtAttribute.FKConstraint("UserLevel", "Id")]
        public int UserLevelId { get; set; }
    }
}
