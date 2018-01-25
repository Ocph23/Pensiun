using DAL;
using PengusulanPensiun.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Collections.Generic;
using System.Windows;

namespace PengusulanPensiun.ViewModels
{
    internal class PengajuanPensiunViewModel:BaseViewModel<pegawai>
    {
        public PengajuanPensiunViewModel()
        {
            using (var db = new OcphDbContext())
            {
              
                AddPegawaiCommand = new CommandHandler
                {
                    CanExecuteAction = AddPegawaiValidate,
                    ExecuteAction = AddPegawaiCommandAction
                };

                PrintCommand = new CommandHandler
                {
                    CanExecuteAction=PrintValidation,
                    ExecuteAction = PrintCommandAction
                };



                EditPegawaiCommand = new CommandHandler
                {
                    CanExecuteAction = x => SelectedItem != null,
                    ExecuteAction = EditPegawaiCommandAction
                };

                CloseCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
               
               

                Source = new ObservableCollection<Models.pegawai>();
                SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
                SourceView.Refresh();
                this.Periodes = db.Periodes.Select().ToList();
                if(this.Periodes.Count>0)
                {
                    this.LastPeriode = this.Periodes.Last();
                    if (LastPeriode != null)
                        PeriodeSelected = LastPeriode;
                }else
                {
                    MessageBox.Show("Anda Belum Menambahkan Periode");
                }
              
            }
        }

        private bool PrintValidation(object obj)
        {
            if (PeriodeSelected !=null && Source != null && Source.Count > 0)
                return true;
            else
                return false;
        }

        private void PrintCommandAction(object obj)
        {
            var form = new Views.LaporanPengajuanView(PeriodeSelected.Id);
            form.ShowDialog();
        }

        private List<pegawai> GetSource(periode per)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = from a in db.Pegawais.Where(O => O.PeriodeId == per.Id)
                                 join b in db.Kelengkapans.Select() on a.Id equals b.PegawaiId
                                 join c in db.Instansis.Select() on a.InstansiId equals c.Id
                                 select new pegawai
                                 {
                                     Alamat = a.Alamat,
                                     Id = a.Id,
                                     InstansiId = a.InstansiId,
                                     Jabatan = a.Jabatan,
                                     Kelengkapan = b,
                                     MasaKerja = a.MasaKerja,
                                     Nama = a.Nama,
                                     NIP = a.NIP,
                                     NomorSeriKarpeg = a.NomorSeriKarpeg,
                                     PangkatGolongan = a.PangkatGolongan,
                                     PeriodeId = a.PeriodeId,
                                     TempatLahir = a.TempatLahir,
                                     TanggalLahir = a.TanggalLahir,
                                     Instansi = c
                                 };
                    return result.ToList();
                }
                catch (Exception ex)
                {

                    throw;
                }
             
               
            }
        }

        private bool AddPegawaiValidate(object obj)
        {
            if (LastPeriode == PeriodeSelected)
                return true;
            else
                return false;
        }

        public override pegawai SelectedItem
        {
            get => base.SelectedItem;
            set => base.SelectedItem = value;

        }

        private void EditPegawaiCommandAction(object obj)
        {
            var form = new Views.AddPegawaiView();
            var viewmodel = new ViewModels.AddPegawaiViewModel(SelectedItem) { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            SourceView.Refresh();

        }

        private void AddPegawaiCommandAction(object obj)
        {
            var form = new Views.AddPegawaiView();
            var viewmodel = new ViewModels.AddPegawaiViewModel() { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            if (viewmodel.IsSaved)
            {
                var model = (pegawai)viewmodel;
                Source.Add(model);
                SourceView.Refresh();
            }
        }

        private periode _periode;

        public periode PeriodeSelected
        {
            get { return _periode; }
            set {
                _periode = value;
                Source.Clear();
                using (var db = new OcphDbContext())
                {
                    var result = this.GetSource(value);
                    foreach(var item in result)
                    {
                        Source.Add(item);
                    }
                    SourceView.Refresh();
                }
                OnPropertyChange("PeriodeSelected"); }
        }



        public CommandHandler CloseCommand { get; }
        public List<periode> Periodes { get; }
        
        public periode LastPeriode { get; }
        public CommandHandler AddPegawaiCommand { get; }
        public CommandHandler PrintCommand { get; }
        public CommandHandler EditPegawaiCommand { get; }
    }
}