using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace PengusulanPensiun.ViewModels
{
    public class BaseViewModel<T> :DAL.BaseNotifyProperty,IBaseViewModel<T>
    {
        public Action WindowClose { get;  set; }
        public ObservableCollection<T> Source { get; set; }
        public CollectionView SourceView { get; set; }
        public virtual T SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value;
                OnPropertyChange("SelectedItem");
            }
        }

        private string error;
        PropertyInfo[] properties = typeof(T).GetProperties();
        private T selectedItem;

        public string this[string columnName] {
            get
            {
                Error = null;
                var r = properties.Where(O => O.Name == columnName).FirstOrDefault();
                return Error;
            }
        }

        public string Error {
            get { return error; }
            set
            {
                error = value;
                OnPropertyChange("Error");
            }
        }
    }
}