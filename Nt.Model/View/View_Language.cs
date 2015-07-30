using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Language : Language, IView
    {
        public int MaxID { get; set; }

        public string GetScript()
        {
            return
               " Select *," +
               " (Select max(ID) From ##Language##) As MaxID" +
               " From ##Language## As T0";
        }
    }
}
