using DAL;
using PengusulanPensiun.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace PengusulanPensiun.ViewModels
{
    internal class AddPegawaiViewModel:pegawai,IDataErrorInfo
    {
        private string error;

        public AddPegawaiViewModel()
        {
            this.Kelengkapan = new kelengkapanberkas();
            Load();
          
        }

        public AddPegawaiViewModel(pegawai selected)
        {
            Load();
            this.Alamat = selected.Alamat;
            this.Id = selected.Id;
            this.InstansiId = selected.InstansiId;
            this.Jabatan = selected.Jabatan;
            this.Kelengkapan = selected.Kelengkapan;
            this.MasaKerja = selected.MasaKerja;
            this.Nama = selected.Nama;
            this.NIP = selected.NIP;
            this.NomorSeriKarpeg = selected.NomorSeriKarpeg;
            this.PangkatGolongan = selected.PangkatGolongan;
            this.PeriodeId = selected.PeriodeId;
            this.TanggalLahir = selected.TanggalLahir;
            this.TempatLahir = selected.TempatLahir;
            this.Kelengkapan = selected.Kelengkapan;
        }

        private void Load()
        {
            var periode = Helper.GetLastPeriode();
            if (periode == null)
            {
                MessageBox.Show("Periode Pengajuan Belum Ada , Silahkan Tambahkan Periode");
                WindowClose();
            }
            else
            {
                if(periode.TanggalRencanaPengiriman.Subtract(TimeSpan.FromDays(1))<DateTime.Now)
                {
                    MessageBox.Show("Batas Pengajuan Telah Selesai, Silahkan Tambahkan Periode Baru");
                }else
                {
                    this.PeriodeId = periode.Id;
                }
            }


            using (var db = new OcphDbContext())
            {
                Instansies = db.Instansis.Select().ToList();
            }

            SaveCommand = new CommandHandler { CanExecuteAction = SaveCommandValidate, ExecuteAction = SaveCommandAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
        }

        private void SaveCommandAction(object obj)
        {
            using (var db = new OcphDbContext())
            {
              var trans=  db.Connection.BeginTransaction();
                try
                {
                    if(this.Id<=0)
                    {
                        Id = db.Pegawais.InsertAndGetLastID(this);
                        if(Id>0)
                        {
                            this.Kelengkapan.PegawaiId = Id;
                            this.Kelengkapan.Id = db.Kelengkapans.InsertAndGetLastID(this.Kelengkapan);
                           
                        }

                        if(Id>0 && this.Kelengkapan.Id>0)
                        {
                            trans.Commit();
                            WindowClose();
                        }else
                        {
                            throw new SystemException("Data Tidak Tersimpan");
                        }
                    }else
                    {
                        if(db.Pegawais.Update(O=> new
                        {
                            O.Alamat,
                            O.InstansiId,
                            O.Jabatan,
                            O.MasaKerja,
                            O.Nama,
                            O.NIP,
                            O.NomorSeriKarpeg,
                            O.PangkatGolongan,
                            O.PeriodeId,
                            O.TanggalLahir,
                            O.TempatLahir
                        },this,O=>O.Id==Id) && 

                        db.Kelengkapans.Update(O=>new
                        {
                            O.AktaKelahiranAnak,
                            O.AktaNikah,
                            O.AlamatTerakhir,
                            O.KartuPegawai,
                            O.PegawaiId,
                            O.SKKenaikanPangkat,
                            O.SKPengangkatan,
                            O.SKPTahunTerakhir,
                            O.SP4,
                            O.Taspen
                        },this.Kelengkapan,O=>O.Id==Kelengkapan.Id))
                        {
                            trans.Commit();
                            WindowClose();
                        }else
                        {
                            WindowClose();
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
            if (string.IsNullOrEmpty(error))
                return true;
            else
                return false;
        }

        public Action WindowClose { get; set; }
        public bool IsSaved { get; internal set; }

        public string Error =>error;

        public CommandHandler SaveCommand { get; private set; }
        public CommandHandler CancelCommand { get; private set; }
        public object Instansies { get; private set; }

        public string this[string columnName] =>
          Helper.ValidateModel<pegawai>(this, columnName, Error);



    }
}