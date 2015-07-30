using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Goods_Binding : Goods_Binding, IView
    {
        public int MaxID { get; set; }
        public string BingGoodsName { get; set; }

        public string GetScript()
        {
            return "Select *,0 As MaxID From ##Goods_Binding## As T0" +
                " Left Join (Select Id As TempID,Name As BingGoodsName From ##Goods##) As T1" +
                " On T0.BindingId=T1.TempID";
        }

    }
}
