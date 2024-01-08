using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_DThuocDuTru : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_DThuocDuTru()
        {
            InitializeComponent();
        }
        public void BinDingData()
        {
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongThanhTien.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhaCC.DataBindings.Add("Text", DataSource, "NhaCC");
            clDonVi.DataBindings.Add("Text", DataSource, "DonVi");
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

    }
}
