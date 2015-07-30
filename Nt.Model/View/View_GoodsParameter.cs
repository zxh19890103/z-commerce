using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_GoodsParameter : Goods_Parameter, IView
    {
        public string ParamGroupName { get; set; }
        public int MaxID { get; set; }

        public string GetScript()
        {
            return
                " Select *," +
                " (Select max(ID) From ##Goods_Parameter##) As MaxID" +
                " From ##Goods_Parameter## As T0 " +
                " Left Join " +
                " (Select Id  As TempID,GroupName As ParamGroupName From ##Goods_ParameterGroup##) As T1" +
                " On T0.GoodsParameterGroupId=T1.TempID";
        }

    }
}
