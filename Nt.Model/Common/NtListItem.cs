using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.Common
{
    public class NtListItem
    {
        public NtListItem()
        { }

        public NtListItem(object text, object value)
        {
            Text = text.ToString();
            Value = value.ToString();
        }
        
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
