using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class GuestBook : BaseLocaleEntity, IOrderable, IDisplayable
    {
        [FieldSize(1024)]
        public string Title { get; set; }
        [FieldSize(256)]
        public string Name { get; set; }
        [FieldSize(50)]
        public string Tel { get; set; }
        public bool Gender { get; set; }
        [FieldSize(256)]
        public string NativePlace { get; set; }
        [FieldSize(256)]
        public string Nation { get; set; }
        [FieldSize(50)]
        public string PersonID { get; set; }
        [FieldSize(50)]
        public string EduDegree { get; set; }
        [FieldSize(50)]
        public string ZipCode { get; set; }
        [FieldSize(50)]
        public string PoliticalRole { get; set; }
        [FieldSize(256)]
        public string Address { get; set; }
        [FieldSize(256)]
        public string GraduatedFrom { get; set; }
        [FieldSize(256)]
        public string Grade { get; set; }
        public DateTime BirthDate { get; set; }
        [FieldSize(50)]
        public string Mobile { get; set; }
        [FieldSize(50)]
        public string Email { get; set; }
        [FieldSize(256)]
        public string Company { get; set; }
        public string Body { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedDate { get; set; }
        [FieldSize(1024)]
        public string Note { get; set; }
        public int Type { get; set; }
        /// <summary>
        /// 是否查看
        /// </summary>
        public bool Viewed { get; set; }

    }
}
