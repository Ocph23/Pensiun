using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace PengusulanPensiun.Models 
{ 
     [TableName("pegawai")] 
     public class pegawai:BaseNotifyProperty  
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value; 
                     OnPropertyChange("Id");
                     }
          } 

          [DbColumn("NIP")] 
          public string NIP 
          { 
               get{return _nip;} 
               set{ 
                      _nip=value; 
                     OnPropertyChange("NIP");
                     }
          } 

          [DbColumn("Nama")] 
          public string Nama 
          { 
               get{return _nama;} 
               set{ 
                      _nama=value; 
                     OnPropertyChange("Nama");
                     }
          } 

          [DbColumn("TempatLahir")] 
          public string TempatLahir 
          { 
               get{return _tempatlahir;} 
               set{ 
                      _tempatlahir=value; 
                     OnPropertyChange("TempatLahir");
                     }
          } 

          [DbColumn("TanggalLahir")] 
          public DateTime TanggalLahir 
          { 
               get{return _tanggallahir;} 
               set{ 
                      _tanggallahir=value; 
                     OnPropertyChange("TanggalLahir");
                     }
          } 

          [DbColumn("NomorSeriKarpeg")] 
          public string NomorSeriKarpeg 
          { 
               get{return _nomorserikarpeg;} 
               set{ 
                      _nomorserikarpeg=value; 
                     OnPropertyChange("NomorSeriKarpeg");
                     }
          } 

          [DbColumn("PangkatGolongan")] 
          public string PangkatGolongan 
          { 
               get{return _pangkatgolongan;} 
               set{ 
                      _pangkatgolongan=value; 
                     OnPropertyChange("PangkatGolongan");
                     }
          } 

          [DbColumn("MasaKerja")] 
          public string MasaKerja 
          { 
               get{return _masakerja;} 
               set{ 
                      _masakerja=value; 
                     OnPropertyChange("MasaKerja");
                     }
          } 

          [DbColumn("Jabatan")] 
          public string Jabatan 
          { 
               get{return _jabatan;} 
               set{ 
                      _jabatan=value; 
                     OnPropertyChange("Jabatan");
                     }
          } 

          [DbColumn("Alamat")] 
          public string Alamat 
          { 
               get{return _alamat;} 
               set{ 
                      _alamat=value; 
                     OnPropertyChange("Alamat");
                     }
          } 

          [DbColumn("InstansiId")] 
          public int InstansiId 
          { 
               get{return _instansiid;} 
               set{ 
                      _instansiid=value; 
                     OnPropertyChange("InstansiId");
                     }
          } 

          [DbColumn("PeriodeId")] 
          public int PeriodeId 
          { 
               get{return _periodeid;} 
               set{ 
                      _periodeid=value; 
                     OnPropertyChange("PeriodeId");
                     }
          } 


          public kelengkapanberkas Kelengkapan { get; set; }
        public instansi Instansi { get; internal set; }

        private int  _id;
           private string  _nip;
           private string  _nama;
           private string  _tempatlahir;
           private DateTime  _tanggallahir;
           private string  _nomorserikarpeg;
           private string  _pangkatgolongan;
           private string  _masakerja;
           private string  _jabatan;
           private string  _alamat;
           private int  _instansiid;
           private int  _periodeid;
      }
}


