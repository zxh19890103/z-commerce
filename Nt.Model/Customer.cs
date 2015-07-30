using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Customer : BaseEntity
    {
        [FKConstraint("CustomerRole", "Id")]
        public int CustomerRoleId { get; set; }

        [FieldSize(256)]
        public string Name { get; set; }
        [FieldSize(1024)]
        public string Password { get; set; }
        [FieldSize(100)]
        public string Email { get; set; }
        public int Points { get; set; }
        public DateTime CreatedDate { get; set; }
        [FieldSize(100)]
        public string RealName { get; set; }
        [FieldSize(256)]
        public string Address { get; set; }
        [FieldSize(50)]
        public string Phone { get; set; }
        [FieldSize(50)]
        public string Mobile { get; set; }
        [FieldSize(50)]
        public string Zip { get; set; }

        public bool Active { get; set; }

        public DateTime BirthDay { get; set; }
        public bool Gender { get; set; }

        public string LastLoginIP { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int LoginTimes { get; set; }

    }
}
