using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDemoUI.ViewModels
{
    public class FilterItemViewModel : PropertyChangedBase
    {
        private string _filterName;
        private bool _isFilterChecked;
        public string FilterName
        {
            get { return _filterName; }
            set
            {
                _filterName = value;
                NotifyOfPropertyChange(() => FilterName);
            }
        }

       

        public bool IsFilterChecked
        {
            get { return _isFilterChecked; }
            set
            {
                _isFilterChecked = value;
                NotifyOfPropertyChange(() => IsFilterChecked);
            }
        }

        public Dictionary<string, string> RequestedFilters { get; set; }
    }
}
