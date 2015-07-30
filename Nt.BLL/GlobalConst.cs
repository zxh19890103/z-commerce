using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.BLL
{
    public class GlobalConst
    {
        #region ajax
        public const string AJAX_NO_ACTION = "no-action";
        public const string AJAX_REQUEST_KEY_OF_ACTION = "action";
        #endregion

        #region security
        public const int SECURITY_NOT_LOGIN = 0;
        public const int SECURITY_NOT_AUTHORIZED = 1;
        public const int SECURITY_PASS = 2;
        #endregion

    }
}
