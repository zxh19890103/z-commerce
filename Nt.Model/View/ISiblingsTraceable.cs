using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public interface ISiblingsTraceable
    {
        int Id { get; set; }
        int PreID { get; set; }
        int NextID { get; set; }
        string PreTitle { get; set; }
        string NextTitle { get; set; }
    }
}
