using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Slider:Slider,IView
    {
        public int MaxID { get; set; }

        public string GetScript()
        {
            return
               " Select *," +
               " (Select max(ID) From ##Slider##) As MaxID" +
               " From ##Slider## As T0";
        }
    }
}
