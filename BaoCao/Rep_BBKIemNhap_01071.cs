using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BBKIemNhap_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BBKIemNhap_01071()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
        
            colTenDuoc.DataBindings.Add("Text", DataSource, "TenDV");
            colSoCT.DataBindings.Add("Text", DataSource, "SoCT");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colSoKS.DataBindings.Add("Text", DataSource, "Solo");
            colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            colHanDung.DataBindings.Add("Text", DataSource, "HanDung").FormatString = "{0:dd/MM/yyyy}";
            colDonGia.DataBindings.Add("Text", DataSource, "DonGiaCT");
            xrTableCell13.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colGhiChu.DataBindings.Add("Text", DataSource, "");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuongN");         
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienTong.Summary.FormatString = DungChung.Bien.FormatString[1];    
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
          //  xrTableCell16.Text = 
         //   xrTableCell17.Text = 
         //   xrTableCell18.Text = 
         //   xrTableCell19.Text = 
         //   xrTableCell20.Text = 
        }


    }
}
