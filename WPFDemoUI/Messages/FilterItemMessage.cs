using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDemoUI.Messages
{
    public class FilterItemMessage
    {
        public string Name { get; set; }
        public Dictionary<string, string> RequiredFilters { get; set; }
        public FilterItemMessage()
        {
            RequiredFilters = new Dictionary<string, string>();
        }
    }
}
