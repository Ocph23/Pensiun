using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using PengusulanPensiun.ViewModels;

namespace PengusulanPensiun.Models
{
    [TableName("periode")]
    public class periode : BaseNotifyProperty
    {
        [PrimaryKey("Id")]
        [DbColumn("Id")]
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange("Id");
            }
        }

        [DbColumn("KodePeriode")]
        public string KodePeriode
        {
            get { return _kodeperiode; }
            set
            {
                _kodeperiode = value;
                OnPropertyChange("KodePeriode");
            }
        }

        [DbColumn("TanggalPengajuan")]
        public DateTime TanggalPengajuan
        {
            get
            {
                if (_tanggalpengajuan == new DateTime())
                    _tanggalpengajuan = DateTime.Now;
                return _tanggalpengajuan;
            }
            set
            {
                _tanggalpengajuan = value;
                OnPropertyChange("TanggalPengajuan");
            }
        }

        [DbColumn("TanggalRencanaPengiriman")]
        public DateTime TanggalRencanaPengiriman
        {
            get
            {
                if (_tanggalrencanapengiriman == new DateTime())
                    _tanggalrencanapengiriman = DateTime.Now;
                return _tanggalrencanapengiriman;
            }
            set
            {
                _tanggalrencanapengiriman = value;
                OnPropertyChange("TanggalRencanaPengiriman");
            }
        }

        [DbColumn("Aktif")]
        public bool Aktif
        {
            get { return _aktif; }
            set { _aktif = value; OnPropertyChange("Aktif"); }
        }


        private bool _aktif;

        private int _id;
        private string _kodeperiode;
        private DateTime _tanggalpengajuan;
        private DateTime _tanggalrencanapengiriman;


    }
}


