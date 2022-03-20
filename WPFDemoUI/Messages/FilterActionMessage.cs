using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDemoUI.Messages
{
    public class FilterActionMessage
    {
        public Dictionary<string, string> RequestedFilters { get; set; }
        public FilterActionMessage()
        {
            RequestedFilters = new Dictionary<string, string>();
        }
    }
}
