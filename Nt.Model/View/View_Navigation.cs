using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Navigation : Navigation, IView
    {
        public string ParentName { get; set; }
        public int MaxID { get; set; }

        public string GetScript()
        {
            return
               " Select *," +
               " (Select max(ID) From ##Navigation##) As MaxID," +
               " Case [Parent]\r\n" +
               " When 0 Then N'Root'\r\n" +
               " Else " +
               " (Select Top 1 [Name] From ##Navigation## As T1 Where ID=T0.Parent) End\r\n" +
               " As ParentName" +
               " From ##Navigation## As T0";
        }
    }
}
