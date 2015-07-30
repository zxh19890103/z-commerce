using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_User : User, IView
    {
        public string LevelName { get; set; }
        public bool IsAdmin { get; set; }
        public bool LevelActive { get; set; }
        public int MaxID { get; set; }

        public string GetScript()
        {
            return
                " Select *," +
                " (Select max(ID) From ##User##) As MaxID" +
                " From ##User## As T0 " +
                " Left Join " +
                " (Select Id  As TempID,Name As LevelName,IsAdmin,Active As LevelActive From ##UserLevel##) As T1" +
                " On T0.UserLevelId=T1.TempID";
        }

    }
}
