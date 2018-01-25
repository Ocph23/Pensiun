using DAL;
using PengusulanPensiun.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace PengusulanPensiun.ViewModels
{
    public class AddPeriodeViewModel:periode,IDataErrorInfo
    {
        private string error;

        public Action WindowClose { get; set; }
        public bool IsSaved { get; set; }

        public AddPeriodeViewModel()
        {
            this.Loaded();
            this.Aktif = true;
        }

        public AddPeriodeViewModel(periode selectedItem)
        {
            this.Aktif = selectedItem.Aktif;
            this.Id = selectedItem.Id;
            this.KodePeriode = selectedItem.KodePeriode;
            this.TanggalPengajuan = selectedItem.TanggalPengajuan;
            this.TanggalRencanaPengiriman = selectedItem.TanggalRencanaPengiriman;
            this.Loaded();
        }

        private void Loaded()
        {
            SaveCommand = new CommandHandler { CanExecuteAction = SaveCommandValidate, ExecuteAction = SaveCommandAction };

        }

        private void SaveCommandAction(object obj)
        {

            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    if (Id == 0)
                    {
                        var saved = db.Periodes.Update(O => new { O.Aktif }, new periode { Aktif = false },
                          O => O.Aktif == true);
                        Id = db.Periodes.InsertAndGetLastID(this);
                        if (Id > 0)
                        {
                            MessageBox.Show("Data Berhasil Disimpan", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            IsSaved = true;
                            trans.Commit();
                            this.WindowClose();
                        }
                        else
                        {
                            MessageBox.Show("Data Tidak Tersimpan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        var saved = db.Periodes.Update(O => new { O.KodePeriode, O.TanggalPengajuan, O.TanggalRencanaPengiriman }, this,
                            O => O.Id == this.Id);
                        if (saved)
                        {
                            MessageBox.Show("Data Berhasil Disimpan", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            IsSaved = true;
                            trans.Commit();
                            this.WindowClose();
                        }
                        else
                        {
                            MessageBox.Show("Data Tidak Tersimpan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private bool SaveCommandValidate(object obj)
        {
            if (string.IsNullOrEmpty(KodePeriode))
                return false;
            if (TanggalPengajuan == new DateTime() || TanggalRencanaPengiriman== new DateTime())
                return false;
            return true;
        }

        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                OnPropertyChange("Error");
            }
        }

        public CommandHandler SaveCommand { get; private set; }

        public string this[string columnName] =>
            Helper.ValidateModel<periode>(this,columnName, Error);


    }
}