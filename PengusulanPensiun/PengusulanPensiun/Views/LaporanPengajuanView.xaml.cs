using DAL;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PengusulanPensiun.Views
{
    /// <summary>
    /// Interaction logic for LaporanPengajuanView.xaml
    /// </summary>
    public partial class LaporanPengajuanView : Window
    {
        private int PeriodeId;

        public LaporanPengajuanView(int PeriodeId)
        {
            this.PeriodeId = PeriodeId;
            InitializeComponent();
            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.PageWidth;
            this.Loaded += LaporanPengajuanView_Loaded;
        }

        private void LaporanPengajuanView_Loaded(object sender, RoutedEventArgs e)
        {
            reportViewer.LocalReport.DataSources.Clear();
            List<Models.PegawaiReportModel> data = new List<Models.PegawaiReportModel>();
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = from d in db.Periodes.Where(O=>O.Id==PeriodeId)
                                 join a in db.Pegawais.Where(O => O.PeriodeId == PeriodeId) on d.Id equals a.PeriodeId
                                 join b in db.Kelengkapans.Select() on a.Id equals b.PegawaiId
                                 join c in db.Instansis.Select() on a.InstansiId equals c.Id
                                 select new Models.PegawaiReportModel
                                 {
                                     Alamat = a.Alamat,
                                     Id = a.Id,
                                     InstansiId = a.InstansiId,
                                     Jabatan = a.Jabatan,
                                     MasaKerja = a.MasaKerja,
                                     Nama = a.Nama,
                                     NIP = a.NIP,
                                     NomorSeriKarpeg = a.NomorSeriKarpeg,
                                     PangkatGolongan = a.PangkatGolongan,
                                     PeriodeId = a.PeriodeId,
                                     TempatLahir = a.TempatLahir,
                                     TanggalLahir = a.TanggalLahir,
                                     InstansiName = c.Nama, KodePeriode=d.KodePeriode, TanggalPengajuan=d.TanggalPengajuan, TanggalRencanaPengiriman=d.TanggalRencanaPengiriman,
                                     Lengkap=b.Lengkap
                                 };
                  

                    var no = 1;

                    foreach (var item in result.ToList())
                    {
                       
                        if(item.Lengkap)
                        {
                            item.Nomor = no;
                            data.Add(item);
                            no++;
                        }
                     
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }


                ReportDataSource DataSet1 = new ReportDataSource();
                DataSet1.Name = "DataSet1";
                DataSet1.Value = data;
                reportViewer.LocalReport.ReportEmbeddedResource = "PengusulanPensiun.Views.ReportPengajuan.rdlc";
                reportViewer.LocalReport.DataSources.Add(DataSet1);
                System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
                ps.Landscape = true;
                ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
                ps.Margins = new System.Drawing.Printing.Margins(10, 10, 10, 10);
                ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
                reportViewer.ResetPageSettings();
                reportViewer.SetPageSettings(ps);
                reportViewer.RefreshReport();
            }
        }
    }
}