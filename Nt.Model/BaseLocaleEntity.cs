using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public abstract class BaseLocaleEntity : BaseEntity, ILocaleEntity
    {
        [NtAttribute.FKConstraint("Language", "Id")]
        public int LanguageId { get; set; }
    }
}
