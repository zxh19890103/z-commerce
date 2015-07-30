using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Framework.Admin
{
    public interface IAllAjax
    {        
        void TMPost();
        void TMDel();
        void TMGet();
        void TMList();
    }
}
