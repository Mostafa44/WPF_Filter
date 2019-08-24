using Caliburn.Micro;
using DemoLibrary;
using DemoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WPFDemoUI.Messages;

namespace WPFDemoUI.ViewModels
{
    [Export]
    public class ShellViewModel: IHandle<FilterDataGridMessage>,IHandle<FilterItemMessage>
    {
        private readonly IWindowManager _windowManager;
        public ObservableCollection<FilterItemViewModel> Filters { get; set; }
        public BindableCollection<PersonModel> People { get; set; }
        public ICollectionView PeopleCollection { get; set; }
        // public ICommand FilterFullNameCommand { get; set; }
        Dictionary<string, Predicate<PersonModel>> filters = new Dictionary<string, Predicate<PersonModel>>();
        private readonly IEventAggregator _eventAgg;

        [ImportingConstructor]
        public ShellViewModel(IWindowManager windowManager,
                               IEventAggregator eventAggregator)
        {
            _eventAgg = eventAggregator;
            _eventAgg.Subscribe(this);
            DataAccess da = new DataAccess();
            _windowManager = windowManager;
            People = new BindableCollection<PersonModel>(da.GetPeople());
            Filters = new BindableCollection<FilterItemViewModel>();
            Filters.Add(new FilterItemViewModel() { FilterName = "HardCoded Filter", IsFilterChecked = false });
            PeopleCollection = CollectionViewSource.GetDefaultView(People);
            PeopleCollection.Filter = FilterPeople;
            //FilterFullNameCommand = new RelayCommand
        }
        public bool CanFilterFullName(string obj)
        {
            return !string.IsNullOrEmpty(obj);
        }
        public void FilterFullName(string  obj)
        {
            AddFilterAndRefresh("FilterFullName", PersonModel => PersonModel.FullName.Contains(obj));
            //this.PeopleCollection.Filter += item =>
            //{
            //    PersonModel candidate = item as PersonModel;
            //    return candidate.FullName.Contains(obj);
            //};

            //this.PeopleCollection.Refresh();
        }
        public void FilterAddress(string obj)
        {
            AddFilterAndRefresh("FilterAddress",
                                PersonModel => PersonModel.PrimaryAddress.FullAddress.Contains(obj));
        }


        private bool FilterPeople(object obj)
        {
            PersonModel p = (PersonModel)obj;
            return filters.Values.Aggregate(true, (prevValue, predicate) => prevValue && predicate(p));
        }

        private void AddFilterAndRefresh(string name, Predicate<PersonModel> predicate)
        {
            filters.Add(name, predicate);
            PeopleCollection.Refresh();
        }
        public void RemoveFilter(string filterName)
        {
            if (filters.Remove(filterName))
            {
                PeopleCollection.Refresh();
            }
        }

        public void ClearFilters()
        {
            filters.Clear();
            PeopleCollection.Refresh();
        }
        public void OpenPopup()
        {
            var vm = new PopupWindowViewModel("Filter Criteria");
            //_windowService.ShowWindow(vm);
            _windowManager.ShowDialog(vm);
        }

        public void Handle(FilterDataGridMessage message)
        {
            ClearFilters();
            //Check for each criteria if found , then add a predicate for it
            if (message.RequestedFilters.ContainsKey("FullName"))
            {
                AddFilterAndRefresh("FullName", PersonModel => PersonModel.FullName.Contains(message.RequestedFilters.GetValueOrDefault("FullName")));
            }
            if (message.RequestedFilters.ContainsKey("Address"))
            {
                AddFilterAndRefresh("Address", PersonModel => PersonModel.PrimaryAddress.FullAddress.Contains(message.RequestedFilters.GetValueOrDefault("Address")));
            }
           
        }

        public void Handle(FilterItemMessage message)
        {
            var filterItemVM = new FilterItemViewModel()
            {
                FilterName = message.Name,
                RequestedFilters = message.RequiredFilters
            };

            Filters.Add(filterItemVM);
        }
    }
}
