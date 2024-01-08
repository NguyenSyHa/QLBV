using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PLThuoctheothang_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PLThuoctheothang_20001()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            Tendv.DataBindings.Add("Text", DataSource, "TenDV");
            Donvi.DataBindings.Add("Text", DataSource, "DonVi");
            SLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            STHANG.DataBindings.Add("Text", DataSource, "SoThang");
            TongSoL.DataBindings.Add("Text", DataSource, "SoLuongTong");
            ThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien");
           // RFThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel1.Text = DungChung.Bien.TenCQ;
        }

        private void Donvi_BeforePrint(object sender, CancelEventArgs e)
        {
         
        }

    }
}
