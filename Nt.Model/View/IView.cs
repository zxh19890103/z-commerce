using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public interface IView
    {
        string GetScript();
        int MaxID { get; set; }
    }
}
