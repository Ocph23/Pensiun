using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace PengusulanPensiun.Models
{
    [TableName("kelengkapanberkas")]
    public class kelengkapanberkas : BaseNotifyProperty
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

        [DbColumn("SKPengangkatan")]
        public bool SKPengangkatan
        {
            get { return _skpengangkatan; }
            set
            {
                _skpengangkatan = value;
                OnPropertyChange("SKPengangkatan");
            }
        }

        [DbColumn("SKKenaikanPangkat")]
        public bool SKKenaikanPangkat
        {
            get { return _skkenaikanpangkat; }
            set
            {
                _skkenaikanpangkat = value;
                OnPropertyChange("SKKenaikanPangkat");
            }
        }

        [DbColumn("Taspen")]
        public bool Taspen
        {
            get { return _taspen; }
            set
            {
                _taspen = value;
                OnPropertyChange("Taspen");
            }
        }

        [DbColumn("KartuPegawai")]
        public bool KartuPegawai
        {
            get { return _kartupegawai; }
            set
            {
                _kartupegawai = value;
                OnPropertyChange("KartuPegawai");
            }
        }

        [DbColumn("AktaNikah")]
        public bool AktaNikah
        {
            get { return _aktanikah; }
            set
            {
                _aktanikah = value;
                OnPropertyChange("AktaNikah");
            }
        }

        [DbColumn("AktaKelahiranAnak")]
        public bool AktaKelahiranAnak
        {
            get { return _aktakelahirananak; }
            set
            {
                _aktakelahirananak = value;
                OnPropertyChange("AktaKelahiranAnak");
            }
        }

        [DbColumn("SP4")]
        public bool SP4
        {
            get { return _sp4; }
            set
            {
                _sp4 = value;
                OnPropertyChange("SP4");
            }
        }

        [DbColumn("AlamatTerakhir")]
        public bool AlamatTerakhir
        {
            get { return _alamatterakhir; }
            set
            {
                _alamatterakhir = value;
                OnPropertyChange("AlamatTerakhir");
            }
        }

        [DbColumn("SKPTahunTerakhir")]
        public bool SKPTahunTerakhir
        {
            get { return _skptahunterakhir; }
            set
            {
                _skptahunterakhir = value;
                OnPropertyChange("SKPTahunTerakhir");
            }
        }

        [DbColumn("PegawaiId")]
        public int PegawaiId
        {
            get { return _pegawaiid; }
            set
            {
                _pegawaiid = value;
                OnPropertyChange("PegawaiId");
            }
        }

        private bool lengkap;

        public bool Lengkap
        {
            get {
                if (!this.AktaKelahiranAnak)
                    return false;
                if (!this.AktaNikah)
                    return false;
                if (!this.AlamatTerakhir)
                    return false;
                if (!this.KartuPegawai)
                    return false;
                if (!this.SKKenaikanPangkat)
                    return false;
                if (!this.SKPengangkatan)
                    return false;
                if (!this.SKPTahunTerakhir)
                    return false;
                if (!this.SP4)
                    return false;
                if (!this.Taspen)
                    return false;

                return true;
            }
            set { lengkap = value;
               
                OnPropertyChange("Lengkap");

            }
        }






        private int _id;
        private bool _skpengangkatan;
        private bool _skkenaikanpangkat;
        private bool _taspen;
        private bool _kartupegawai;
        private bool _aktanikah;
        private bool _aktakelahirananak;
        private bool _sp4;
        private bool _alamatterakhir;
        private bool _skptahunterakhir;
        private int _pegawaiid;
    }
}


