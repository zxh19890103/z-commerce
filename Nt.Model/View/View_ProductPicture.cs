using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_ProductPicture:Picture,IView
    {
        public int ProductId { get; set; }
        public int MaxID { get; set; }

        public string GetScript()
        {
            return
                " Select " +
                " (Select max(ID) From ##Picture##) As MaxID," +
                " DisplayOrder,Alt,Title,Description,Display,Src,Id,ProductId From  ##Picture##  As T1," +
                " (Select ProductId,PictureId From ##Product_Picture_Mapping##) As T0  Where T0.PictureId=T1.Id";
        }
    }
}
