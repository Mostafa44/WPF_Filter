using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDemoUI.Messages
{
    public class FilterDataGridMessage
    {
        public Dictionary<string, string> RequestedFilters { get; set; }
        public FilterDataGridMessage()
        {
            RequestedFilters = new Dictionary<string, string>();
        }
    }
}
