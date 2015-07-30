using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_FriendLink:FriendLink,IView
    {
        public int MaxID { get; set; }

        public string GetScript()
        {
            return
               " Select *," +
               " (Select max(ID) From ##FriendLink##) As MaxID" +
               " From ##FriendLink## As T0";
        }
    }
}
