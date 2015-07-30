using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Product_Picture_Mapping : BaseEntity
    {
        [FKConstraint("Product", "Id")]
        public int ProductId { get; set; }
        [FKConstraint("Picture", "Id")]
        public int PictureId { get; set; }
    }
}
