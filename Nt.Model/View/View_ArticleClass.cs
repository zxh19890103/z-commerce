using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_ArticleClass : Article_Class, IView
    {
        public string ParentName { get; set; }
        public int MaxID { get; set; }
        public string DefaultListTemplate { get; set; }
        public string DefaultDetailTemplate { get; set; }
        public int Total { get; set; }

        public string GetScript()
        {
            return
               " Select *,(Select max(ID) From ##Article_Class##) As MaxID,Case [Parent]\r\n" +
               " When 0 Then N'Root'\r\n" +
               " Else " +
               " (Select Top 1 [Name] From ##Article_Class## As T1 Where ID=T0.Parent) End\r\n" +
               " As ParentName," +
               " Case [Parent]\r\n" +
               " When 0 Then ListTemplate\r\n" +
               " Else "+
               " (Select Top 1 [ListTemplate] From ##Article_Class## As T1 Where ID=T0.Parent) End\r\n" +
               " As DefaultListTemplate," +
               " Case [Parent]\r\n" +
               " When 0 Then DetailTemplate\r\n" +
               " Else " +
               " (Select Top 1 [DetailTemplate] From ##Article_Class## As T1 Where ID=T0.Parent) End\r\n" +
               " As DefaultDetailTemplate," +
               " (Select Count(0) From ##Article## Where Display=1 And ArticleClassId=T0.Id) As Total " +
               " From ##Article_Class## As T0";
        }
    }
}
