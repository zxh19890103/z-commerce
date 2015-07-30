using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Permission : Permission, IView
    {
        public int MaxID { get; set; }
        public string Ico { get; set; }

        public string GetScript()
        {
            return " Select *," +
               " (Select max(ID) From ##Permission##) As MaxID, " +
               " (case  when T0.IsCategory=1 then T0.Src \r\n"+
               "  else (Select Top 1 Src From ##Permission## Where ID=T0.FatherId) end) As Ico" +
               " From ##Permission## As T0";
        }
    }
}
