using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_TKNhapTungLoaiThuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_TKNhapTungLoaiThuoc()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            colMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colDonGiaCT.DataBindings.Add("Text", DataSource, "DonGiaCT").FormatString = DungChung.Bien.FormatString[1];
            colSL.DataBindings.Add("Text", DataSource, "SL");
            colMaNCC.DataBindings.Add("Text", DataSource, "MaCC");
            colNgayNhap.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0:dd/MM/yyyy}";
            colTongSL.DataBindings.Add("Text", DataSource, "SL");

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBC.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
