using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Customer_Message : Customer_Message, IView
    {
        public int MaxID { get; set; }
        public int TotalSent { get; set; }

        public string GetScript()
        {
            return "Select *,0 As MaxID," +
                " (Select Count(0) From ##Customer_Message_Customer_Mapping## Where CustomerMessageId=T0.Id) As TotalSent" +
                " From ##Customer_Message## As T0";
        }

    }
}
