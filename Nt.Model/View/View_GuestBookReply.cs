using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_GuestBookReply : GuestBookReply, IView
    {
        public int MaxID { get; set; }
        public string GuestBookTitle { get; set; }

        public string GetScript()
        {
            return
               " Select *," +
               " 0 As MaxID," +
               " (Select Title From ##GuestBook## Where Id=T0.GuestBookId) As GuestBookTitle" +
               " From ##GuestBookReply## As T0";
        }
    }
}
