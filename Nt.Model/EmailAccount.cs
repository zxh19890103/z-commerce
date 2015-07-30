using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class EmailAccount : BaseEntity
    {
        [FieldSize(100)]
        public string Email { get; set; }
        [FieldSize(256)]
        public string DisplayName { get; set; }
        [FieldSize(256)]
        public string Host { get; set; }
        public int Port { get; set; }
        [FieldSize(256)]
        public string UserName { get; set; }
        [FieldSize(256)]
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool IsDefault { get; set; }
    }
}
