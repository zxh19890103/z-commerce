using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class User:BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        [NtAttribute.FKConstraint("UserLevel","Id")]
        public int UserLevelId { get; set; }

        public string LastLoginIp { get; set; }
        public int LoginTimes { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }

        public bool Active { get; set; }
        public bool Deleted { get; set; }

        public string Profile { get; set; }
        
    }
}
