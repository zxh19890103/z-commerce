using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Log : Log, IView
    {
        public int MaxID { get; set; }
        public string UserName { get; set; }

        public string GetScript()
        {
            return
               " Select *," +
               " 0 As MaxID," +
               " (Select [UserName] From ##User## Where ID=T0.UserID) As UserName" +
               " From ##Log## As T0";
        }
    }
}
