using DAL;
using PengusulanPensiun.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PengusulanPensiun.ViewModels
{
    public class InstansiViewModel : BaseViewModel<instansi>
    {
        public InstansiViewModel()
        {
            using (var db = new OcphDbContext())
            {
                AddPeriodeCommand = new CommandHandler
                {
                    CanExecuteAction = x => true,
                    ExecuteAction = AddPeriodeCommandAction
                };

                EditPeriodeCommand = new CommandHandler
                {
                    CanExecuteAction = x => SelectedItem != null,
                    ExecuteAction = EditPeriodeCommandAction
                };

                CloseCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
                Source = new ObservableCollection<instansi>(db.Instansis.Select().ToList());
                SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
                SourceView.Refresh();
            }
        }

        private void EditPeriodeCommandAction(object obj)
        {
            var form = new Views.AddInstansiView();
            var viewmodel = new ViewModels.AddInstansiViewModel(SelectedItem) { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            if (viewmodel.IsSaved)
            {
                SelectedItem.Alamat = viewmodel.Alamat;
                SelectedItem.Keterangan = viewmodel.Keterangan;
                SelectedItem.Nama = viewmodel.Nama;
            }
        }

        private void AddPeriodeCommandAction(object obj)
        {
            var form = new Views.AddInstansiView();
            var viewmodel = new ViewModels.AddInstansiViewModel() { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            if (viewmodel.IsSaved)
            {
                var model = (instansi)viewmodel;
                Source.Add(model);
                SourceView.Refresh();
            }
        }

        public CommandHandler AddPeriodeCommand { get; }
        public CommandHandler EditPeriodeCommand { get; }
        public CommandHandler CloseCommand { get; }
    }
}
