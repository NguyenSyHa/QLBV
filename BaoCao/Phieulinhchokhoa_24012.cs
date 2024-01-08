using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Phieulinhchokhoa_24012 : DevExpress.XtraReports.UI.XtraReport
    {
        public Phieulinhchokhoa_24012()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong");
            colsoluongtp.DataBindings.Add("Text", DataSource, "SoLuong");
            colMaDV.DataBindings.Add("Text", DataSource, "MaTam");
            colHamLuong.DataBindings.Add("Text", DataSource, "HamLuong");
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");

            colTenHH1.DataBindings.Add("Text", DataSource, "TenDuoc");
            colsoluongyc1.DataBindings.Add("Text", DataSource, "SoLuongN");
            colsoluongtp1.DataBindings.Add("Text", DataSource, "SoLuongN");
            colMaDV1.DataBindings.Add("Text", DataSource, "MaTam");
            colDVT1.DataBindings.Add("Text", DataSource, "DonViTinh");
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Ngaythang1.Value != null && Ngaythang1.Value.ToString() != "")
                txtNgayThang.Text = "Ngày ... tháng ... năm 20...";
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string tenkp = "";
            if (Khoa1.Value != null)
                tenkp = Khoa1.Value.ToString();
        }
    }
}
