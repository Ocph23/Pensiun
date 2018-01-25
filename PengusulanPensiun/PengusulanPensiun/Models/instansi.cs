using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace PengusulanPensiun.Models 
{ 
     [TableName("instansi")] 
     public class instansi:BaseNotifyProperty  
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

          [DbColumn("Nama")] 
          public string Nama 
          { 
               get{return _nama;} 
               set{ 
                      _nama=value; 
                     OnPropertyChange("Nama");
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

          [DbColumn("Keterangan")] 
          public string Keterangan 
          { 
               get{return _keterangan;} 
               set{ 
                      _keterangan=value; 
                     OnPropertyChange("Keterangan");
                     }
          } 

          private int  _id;
           private string  _nama;
           private string  _alamat;
           private string  _keterangan;
      }
}


