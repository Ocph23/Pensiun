using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PengusulanPensiun.ViewModels
{
    public interface IBaseViewModel<T>:IDataErrorInfo
    {
        ObservableCollection<T> Source { get; set; }
        CollectionView SourceView { get; set; }
        T SelectedItem { get; set; }
      

    }
}
