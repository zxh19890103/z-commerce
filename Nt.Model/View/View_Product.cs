using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Product : Product, IView, IHtmlizable, ISiblingsTraceable
    {
        public string CategoryName { get; set; }
        public string CategoryCrumbs { get; set; }
        public int MaxID { get; set; }
        public string ListTemplate { get; set; }
        public string DetailTemplate { get; set; }

        public string AreaName { get; set; }
        public string DayTitle { get; set; }
        public DateTime DayDate { get; set; }

        public int PreID { get; set; }
        public int NextID { get; set; }
        public string PreTitle { get; set; }
        public string NextTitle { get; set; }

        public string GetScript()
        {
            return
                " Select *,(Select max(ID) From ##Product##) As MaxID,0 As PreID,0 As NextID,'' As PreTitle,'' As NextTitle," +
                " (Select [Name] From ##Area## Where Id=##Product##.AreaId) As AreaName, " +
                " (Select [Date] From ##Day## Where Id=##Product##.Day) As DayDate, " +
                " (Select [Title] From ##Day## Where Id=##Product##.Day) As DayTitle From ##Product## " +
                " Left Join " +
                "(Select Id As TempID,Name As CategoryName,Crumbs As CategoryCrumbs,ListTemplate,DetailTemplate From ##ProductCategory##) As T " +
                " On ##Product##.ProductCategoryId=T.TempID";
        }
    }
}
