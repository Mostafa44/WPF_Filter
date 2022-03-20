using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFDemoUI.Messages;

namespace WPFDemoUI.ViewModels
{
    [Export]
    public class PopupWindowViewModel : PropertyChangedBase, IDataErrorInfo
    {
        private IEventAggregator _eventAgg;
        private string _textContent;

        private string _fullName;

        private string _address;
        private bool _hasAddres;

        private bool _hasFullName;


        public bool HasFullName
        {
            get { return _hasFullName; }
            set
            {
                _hasFullName = value;
                NotifyOfPropertyChange(() => HasFullName);
            }
        }

        public bool HasAddress
        {
            get { return _hasAddres; }
            set
            {
                _hasAddres = value;
                NotifyOfPropertyChange(() => HasAddress);
            }
        }
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                HasFullName = !string.IsNullOrEmpty(_fullName);
                NotifyOfPropertyChange(() => FullName);
                NotifyOfPropertyChange(() => HasFullName);
            }
        }


        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                HasAddress = !string.IsNullOrEmpty(_address);
                NotifyOfPropertyChange(() => Address);
                NotifyOfPropertyChange(() => HasAddress);
            }
        }

        public PopupWindowViewModel(string textContent)
        {
            _textContent = textContent;
            _eventAgg = IoC.Get<IEventAggregator>();

        }
        public string TextContent
        {
            get { return _textContent; }
            set
            {
                _textContent = value;
                NotifyOfPropertyChange(() => TextContent);
            }
        }

        private string _filterName;

        public string FilterName
        {
            get { return _filterName; }
            set
            {
                _filterName = value;
                NotifyOfPropertyChange(() => FilterName);
            }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "FilterName")
                {
                    CanAddFilter = !string.IsNullOrEmpty(FilterName);
                    if (string.IsNullOrEmpty(FilterName))
                    {
                        result = "Please Enter a Name for this filter";
                        
                    }
                   
                }
                return result;
            }
        }

        public void ApplyFilter(UserControl u)
        {
            var filterMsg = new FilterDataGridMessage();
            if (HasFullName && !string.IsNullOrWhiteSpace(FullName))
            {
                filterMsg.RequestedFilters.Add(nameof(FullName), FullName);
            }
            if (HasAddress && !string.IsNullOrWhiteSpace(Address))
            {
                filterMsg.RequestedFilters.Add(nameof(Address), Address);
            }

            _eventAgg.PublishOnUIThread(filterMsg);

            //if (u != null)
            {
                Window parentWindow = Window.GetWindow(u);
                if (parentWindow != null)
                {
                    parentWindow.Close();
                }
            }
        }

        private bool _canAddFilter;

        public bool CanAddFilter
        {
            get { return _canAddFilter; }
            set
            {
                _canAddFilter = value;
                NotifyOfPropertyChange(() => CanAddFilter);
            }
        }


        public void AddFilter(UserControl u)
        {
            var filterMsg = new FilterItemMessage();
            filterMsg.Name = FilterName;
            if (HasFullName && !string.IsNullOrWhiteSpace(FullName))
            {
                filterMsg.RequiredFilters.Add(nameof(FullName), FullName);
            }
            if (HasAddress && !string.IsNullOrWhiteSpace(Address))
            {
                filterMsg.RequiredFilters.Add(nameof(Address), Address);
            }

            _eventAgg.PublishOnUIThread(filterMsg);

            //if (u != null)
            {
                Window parentWindow = Window.GetWindow(u);
                if (parentWindow != null)
                {
                    parentWindow.Close();
                }
            }
        }
    }


}
