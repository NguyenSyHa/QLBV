using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuLinhThuoc_27021 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuLinhThuoc_27021()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper() ;

        }

        public void BindingData()
        {
            Tendv.DataBindings.Add("Text", DataSource, "TenDV");
            Donvi.DataBindings.Add("Text", DataSource, "DonVi");
            SLYC.DataBindings.Add("Text", DataSource, "SoLuong");
            SLPhat.DataBindings.Add("Text", DataSource, "SoLuong"); 
            MaQD.DataBindings.Add("Text", DataSource, "MaQD");
            coldongia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colthanhtien.DataBindings.Add("Text", DataSource, "Thanhtien").FormatString = DungChung.Bien.FormatString[1];
            celTongTien.DataBindings.Add("Text", DataSource, "Thanhtien").FormatString = DungChung.Bien.FormatString[1];
            celTongTien.Summary.FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celTruongKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
        }


    }
}
