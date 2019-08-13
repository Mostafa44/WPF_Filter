using Caliburn.Micro;
using DemoLibrary;
using DemoLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace WPFDemoUI.ViewModels
{
    public class ShellViewModel
    {
        public BindableCollection<PersonModel> People { get; set; }
        public ICollectionView PeopleCollection { get; set; }
       // public ICommand FilterFullNameCommand { get; set; }
        public ShellViewModel()
        {
            DataAccess da = new DataAccess();
            People = new BindableCollection<PersonModel>(da.GetPeople());
            PeopleCollection = CollectionViewSource.GetDefaultView(People);
            //FilterFullNameCommand = new RelayCommand
        }
        public bool CanFilterFullName(string obj)
        {
            return !string.IsNullOrEmpty(obj);
        }
        public void FilterFullName(string  obj)
        {
            this.PeopleCollection.Filter += item =>
            {
                PersonModel candidate = item as PersonModel;
                return candidate.FullName.Contains(obj);
            };

            this.PeopleCollection.Refresh();
        }

        public void ClearFilterFullName()
        {
            this.PeopleCollection.Filter += item =>
            {
                PersonModel candidate = item as PersonModel;
                return true;
            };
            this.PeopleCollection.Refresh();
        }
    }
}
