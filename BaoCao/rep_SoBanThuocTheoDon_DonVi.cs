using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_SoBanThuocTheoDon_DonVi : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_SoBanThuocTheoDon_DonVi()
        {
            InitializeComponent();
        }

        internal void BindingData()
        {
            celNgayXuat.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0: dd/MM/yyyy}";
            celTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            celBSKe.DataBindings.Add("Text", DataSource, "TenCB");
            celTenthuoc.DataBindings.Add("Text", DataSource, "TenThuoc");
            celSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            celCachDung.DataBindings.Add("Text", DataSource, "CachDung");
            celGhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
