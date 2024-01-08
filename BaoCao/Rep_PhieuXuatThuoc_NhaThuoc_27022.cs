using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXuatThuoc_NhaThuoc_27022 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXuatThuoc_NhaThuoc_27022
()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lbTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            lbTenCQ.Text = DungChung.Bien.TenCQ;
        }

        public void BindingData() 
        {
            celThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            celDVT.DataBindings.Add("Text", DataSource, "DonVi");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia");
            celSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            celThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien");

            celTongTien.DataBindings.Add("Text", DataSource, "ThanhTien");
        }

    }
}
