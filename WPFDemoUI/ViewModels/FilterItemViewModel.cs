using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDemoUI.Messages;

namespace WPFDemoUI.ViewModels
{
    public class FilterItemViewModel : PropertyChangedBase
    {
        private string _filterName;
        private bool _isFilterChecked;
        private IEventAggregator _eventAgg;

        public string FilterName
        {
            get { return _filterName; }
            set
            {
                _filterName = value;
                NotifyOfPropertyChange(() => FilterName);
            }
        }


        public FilterItemViewModel()
        {
            _eventAgg = IoC.Get<IEventAggregator>();
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

        public void ApplyFilterItem()
        {
            var filterActionMessage = new FilterActionMessage();
            filterActionMessage.RequestedFilters = RequestedFilters;
            _eventAgg.PublishOnUIThread(filterActionMessage);
        }
    }
}
