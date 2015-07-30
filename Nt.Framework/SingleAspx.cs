using Nt.DAL;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Framework
{
    public class SingleAspx : BaseAspx
    {
        int id = 0;

        public int NtID { get { return id; } set { id = value; } }

        SinglePage m;
        public SinglePage Model { get { return m; } }

        protected void FetchModel()
        {
            if (id == 0 && !int.TryParse(Request["id"], out id))
                Goto("/index.aspx", "参数错误!", 3);      

            m = DbAccessor.GetById<Nt.Model.SinglePage>(id);

            if (m == null)
                Goto("/index.aspx", "未发现记录!", 3);

            SeoTitle = m.SeoTitle;
            SeoKeywords = m.SeoKeywords;
            SeoDescription = m.SeoDescription;
        }
        
    }
}
