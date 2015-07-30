using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Banner : Banner, IView
    {
        public int MaxID { get; set; }

        public string GetScript()
        {
            return
               " Select *," +
               " (Select max(ID) From ##Banner##) As MaxID" +
               " From ##Banner## As T0";
        }
    }
}
