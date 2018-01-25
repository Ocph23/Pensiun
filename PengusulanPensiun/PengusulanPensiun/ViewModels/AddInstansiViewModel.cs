using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PengusulanPensiun.Models;
using System.ComponentModel;
using System.Windows;
using DAL;

namespace PengusulanPensiun.ViewModels
{
    public class AddInstansiViewModel:instansi,IDataErrorInfo
    {
        private instansi selectedItem;
        private string error;

        public AddInstansiViewModel()
        {
            this.Load();
        }

        private void Load()
        {

          
            SaveCommand = new CommandHandler { CanExecuteAction = SaveCommandValidate, ExecuteAction = SaveCommandAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
        }
        public AddInstansiViewModel(instansi selectedItem)
        {
            this.Load();
            this.Id = selectedItem.Id;
            this.Nama = selectedItem.Nama;
            this.Keterangan = selectedItem.Keterangan;
        }
        private void SaveCommandAction(object obj)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    if (Id == 0)
                    {
                        Id = db.Instansis.InsertAndGetLastID(this);
                        if (Id > 0)
                        {
                            MessageBox.Show("Data Berhasil Disimpan", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            IsSaved = true;
                            this.WindowClose();
                        }
                        else
                        {
                            MessageBox.Show("Data Tidak Tersimpan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        var saved = db.Instansis.Update(O => new { O.Nama, O.Alamat, O.Keterangan}, this,
                            O => O.Id == this.Id);
                        if (saved)
                        {
                            MessageBox.Show("Data Berhasil Disimpan", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            IsSaved = true;
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
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private bool SaveCommandValidate(object obj)
        {
            if (string.IsNullOrEmpty(Nama) || string.IsNullOrEmpty(Alamat) || string.IsNullOrEmpty(Keterangan))
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

        public string this[string columnName] =>
           Helper.ValidateModel<instansi>(this, columnName, Error);

     

        public Action WindowClose { get; internal set; }
        public bool IsSaved { get; internal set; }
        public CommandHandler SaveCommand { get; private set; }
        public CommandHandler CancelCommand { get; private set; }
      
    }
}
