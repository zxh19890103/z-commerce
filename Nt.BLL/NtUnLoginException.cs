using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.BLL
{
    public class NtUnLoginException : Exception
    {
        public NtUnLoginException()
            : base("尚未登录")
        { }
    }
}
