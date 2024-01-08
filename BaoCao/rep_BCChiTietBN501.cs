using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCChiTietBN501 : DevExpress.XtraReports.UI.XtraReport
    {
        int n = 1;
        public rep_BCChiTietBN501()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colMaBN.DataBindings.Add("Text", DataSource, "MaBNhan");
            colHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            colNgay.DataBindings.Add("Text", DataSource, "Ngay").FormatString = "{0:dd/MM}";
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia");
            colNgayCD.DataBindings.Add("Text", DataSource, "NgayThu").FormatString = "{0:dd/MM}";
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien");
            colTH.DataBindings.Add("Text", DataSource, "ThanhTien");
            colTH.Summary.FormatString = DungChung.Bien.FormatString[1];
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
