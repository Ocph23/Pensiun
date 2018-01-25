using DAL;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System;
using PengusulanPensiun.Models;

namespace PengusulanPensiun.ViewModels
{
    internal class PeriodeViewModel:BaseViewModel<Models.periode>
    {
        public PeriodeViewModel()
        {

            using (var db = new OcphDbContext())
            {
                AddPeriodeCommand = new CommandHandler
                {
                    CanExecuteAction = x =>true,
                    ExecuteAction = AddPeriodeCommandAction
                };

                EditPeriodeCommand = new CommandHandler
                {
                    CanExecuteAction = x => SelectedItem != null,
                    ExecuteAction = EditPeriodeCommandAction
                };

                CloseCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
                Source =new ObservableCollection<Models.periode>( db.Periodes.Select().ToList());
                SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
                SourceView.Refresh();
            }
        }

        private void EditPeriodeCommandAction(object obj)
        {
            var form = new Views.AddPeriodeView();
            var viewmodel = new ViewModels.AddPeriodeViewModel(SelectedItem) { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            if (viewmodel.IsSaved)
            {
                SelectedItem.Aktif = viewmodel.Aktif;
                SelectedItem.KodePeriode = viewmodel.KodePeriode;
                SelectedItem.TanggalPengajuan = viewmodel.TanggalPengajuan;
                SelectedItem.TanggalRencanaPengiriman = viewmodel.TanggalRencanaPengiriman;
            }
        }
        private void AddPeriodeCommandAction(object obj)
        {
            var form = new Views.AddPeriodeView();
           var viewmodel = new ViewModels.AddPeriodeViewModel() { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            if(viewmodel.IsSaved)
            {
                periode model = (periode)viewmodel;
                Source.Add(model);
                SourceView.Refresh();
            }
        }

        public CommandHandler AddPeriodeCommand { get; }
        public CommandHandler EditPeriodeCommand { get; }
        public CommandHandler CloseCommand { get; }

       
    }
}