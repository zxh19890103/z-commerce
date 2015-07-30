using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public abstract class BaseView : BaseModel, IView
    {
        public abstract string GetScript();
        public abstract int MaxID { get; set; }
    }
}
