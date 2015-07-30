using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Article : Article, IView, IHtmlizable, ISiblingsTraceable
    {
        public string ClassName { get; set; }
        public string ClassCrumbs { get; set; }
        public int MaxID { get; set; }
        public string ListTemplate { get; set; }
        public string DetailTemplate { get; set; }
        public int PreID { get; set; }
        public int NextID { get; set; }
        public string PreTitle { get; set; }
        public string NextTitle { get; set; }

        public string GetScript()
        {
            return
                " Select *,(Select max(ID) From ##Article##) As MaxID,0 As PreID,0 As NextID,'' As PreTitle,'' As NextTitle  From ##Article## " +
                " Left Join " +
                "(Select Id As TempID,Name As ClassName,Crumbs As ClassCrumbs,ListTemplate,DetailTemplate From ##Article_Class##) As T " +
                " On ##Article##.ArticleClassId=T.TempID";
        }
    }
}
