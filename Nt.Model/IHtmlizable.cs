using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public interface IHtmlizable : IGuidModel
    {
        int Id { get; set; }
        string ListTemplate { get; set; }
        string DetailTemplate { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
