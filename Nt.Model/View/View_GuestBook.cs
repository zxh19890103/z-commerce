using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_GuestBook : GuestBook, IView
    {
        public int MaxID { get; set; }
        public bool Replied { get; set; }
        public int RepliedCount { get; set; }

        public string GetScript()
        {
            return
                " Select *,0 As MaxID," +
                " (Select Count(Id) From ##GuestBookReply## Where GuestBookId=T0.Id) As RepliedCount," +
                " cast((Select Count(Id) From ##GuestBookReply## Where GuestBookId=T0.Id) as bit) As Replied" +
                " From ##GuestBook## As T0";
        }
    }
}
