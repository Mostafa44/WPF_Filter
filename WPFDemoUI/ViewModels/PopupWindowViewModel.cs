using Caliburn.Micro;
using System;
using System.Collections.Generic;
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
    public class PopupWindowViewModel : PropertyChangedBase
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
       

    }


}
