using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Goods_Attribute_Mapping : BaseEntity, IOrderable
    {
        /// <summary>
        /// 10
        /// </summary>
        public const int CT_DROPDOWNLIST = 10;
        /// <summary>
        /// 20
        /// </summary>
        public const int CT_RADIOBUTTONLIST = 20;
        /// <summary>
        /// 30
        /// </summary>
        public const int CT_CHECKBOXES = 30;
        /// <summary>
        /// 40
        /// </summary>
        public const int CT_TEXTBOX = 40;
        /// <summary>
        /// 50
        /// </summary>
        public const int CT_MUTILINETEXTBOX = 50;
        /// <summary>
        /// 60
        /// </summary>
        public const int CT_DATEPICKER = 60;
        /// <summary>
        /// 70
        /// </summary>
        public const int CT_FILEUPLOAD = 70;
        /// <summary>
        /// 80
        /// </summary>
        public const int CT_COLORSQUARES = 80;

        public bool IsRequired { get; set; }
        public int ControlType { get; set; }
        [FieldSize]
        public string TextPrompt { get; set; }
        public int DisplayOrder { get; set; }

        [FKConstraint("GoodsAttribute", "Id")]
        public int GoodsAttributeId { get; set; }

        [FKConstraint("Goods", "Id")]
        public int GoodsId { get; set; }

    }

}
