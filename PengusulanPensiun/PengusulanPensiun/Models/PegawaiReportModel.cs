using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PengusulanPensiun.Models
{
   public class PegawaiReportModel :Models.periode
   {
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        public string NIP
        {
            get { return _nip; }
            set
            {
                _nip = value;
            }
        }

        public string Nama
        {
            get { return _nama; }
            set
            {
                _nama = value;
            }
        }

        public string TempatLahir
        {
            get { return _tempatlahir; }
            set
            {
                _tempatlahir = value;
            }
        }

        public DateTime TanggalLahir
        {
            get { return _tanggallahir; }
            set
            {
                _tanggallahir = value;
            }
        }

        public string NomorSeriKarpeg
        {
            get { return _nomorserikarpeg; }
            set
            {
                _nomorserikarpeg = value;
            }
        }

        public string PangkatGolongan
        {
            get { return _pangkatgolongan; }
            set
            {
                _pangkatgolongan = value;
            }
        }

        public string MasaKerja
        {
            get { return _masakerja; }
            set
            {
                _masakerja = value;
            }
        }

        public string Jabatan
        {
            get { return _jabatan; }
            set
            {
                _jabatan = value;
            }
        }

        public string Alamat
        {
            get { return _alamat; }
            set
            {
                _alamat = value;
            }
        }

        public int InstansiId
        {
            get { return _instansiid; }
            set
            {
                _instansiid = value;
            }
        }
        public int PeriodeId
        {
            get { return _periodeid; }
            set
            {
                _periodeid = value;
            }
        }

        public string InstansiName { get; set; }
        public int Nomor { get; set; }
        public bool Lengkap { get; internal set; }

        private int _id;
        private string _nip;
        private string _nama;
        private string _tempatlahir;
        private DateTime _tanggallahir;
        private string _nomorserikarpeg;
        private string _pangkatgolongan;
        private string _masakerja;
        private string _jabatan;
        private string _alamat;
        private int _instansiid;
        private int _periodeid;
}
}
