using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Framework
{
    public class AjaxMethodAttribute : Attribute
    {
    }

    public class AjaxAuthorizeAttribute : AjaxMethodAttribute
    {
        public AuthorizeFlag AuthorizeFlag { get; set; }

        public AjaxAuthorizeAttribute(AuthorizeFlag af):base()
        {
            AuthorizeFlag = af;
        }
    }

    public enum AuthorizeFlag { User = 10, Customer = 20, }

}
