using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_DoanhthuVAT : DevExpress.XtraReports.UI.XtraReport
    {
        bool _ckcHTDoanhThu = true;
        public rep_BC_DoanhthuVAT(bool ckcHTDoanhThu)
        {
            InitializeComponent();
            _ckcHTDoanhThu = ckcHTDoanhThu;
        }
        public void BindingData()
        {
            if (_ckcHTDoanhThu)
            {
                colTenthuoc.DataBindings.Add("Text", DataSource, "TenDV");
                celDVT.DataBindings.Add("Text", DataSource, "DonVi");
                colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                colSLgX.DataBindings.Add("Text", DataSource, "Soluongxuat").FormatString = DungChung.Bien.FormatString[0];
                colTienBan.DataBindings.Add("Text", DataSource, "Thanhtienthu").FormatString = DungChung.Bien.FormatString[1];
                celTongDoanhThu.DataBindings.Add("Text", DataSource, "Thanhtienthu");
                celTongDoanhThu.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            else
            {
                colTenthuoc.DataBindings.Add("Text", DataSource, "TenDV");
                celDVT.DataBindings.Add("Text", DataSource, "DonVi");
                colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                colDongiaCT.DataBindings.Add("Text", DataSource, "Dongiachuathue").FormatString = DungChung.Bien.FormatString[1];
                colSLgX.DataBindings.Add("Text", DataSource, "Soluongxuat").FormatString = DungChung.Bien.FormatString[0];
                colTienNhap.DataBindings.Add("Text", DataSource, "Thanhtiennhap").FormatString = DungChung.Bien.FormatString[1];
                celTongGiaVon.DataBindings.Add("Text", DataSource, "Thanhtiennhap");
                celTongGiaVon.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTienBan.DataBindings.Add("Text", DataSource, "Thanhtienthu").FormatString = DungChung.Bien.FormatString[1];
                celTongDoanhThu.DataBindings.Add("Text", DataSource, "Thanhtienthu");
                celTongDoanhThu.Summary.FormatString = DungChung.Bien.FormatString[1];
                colDoanhthu.DataBindings.Add("Text", DataSource, "Doanhthu").FormatString = DungChung.Bien.FormatString[1];
                colTongdoanhthu.DataBindings.Add("Text", DataSource, "Doanhthu");
                colTongdoanhthu.Summary.FormatString = DungChung.Bien.FormatString[1];
            }

        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if(!_ckcHTDoanhThu)
            {
                DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
                xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
                this.celTongGiaVon.Summary = xrSummary3;

                DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
                xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
                this.colTongdoanhthu.Summary = xrSummary4;
            }
        }

        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30372")
            {
                xrTableCell7.Text = "Tổng cộng";
            }
        }
    }
}
