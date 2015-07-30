using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public interface IDeletable
    {
        bool Deleted { get; set; }
    }
}
