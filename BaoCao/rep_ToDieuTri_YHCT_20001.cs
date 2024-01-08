using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.Phieu
{
    public partial class  rep_ToDieuTri_YHCT_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_ToDieuTri_YHCT_20001()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }
        public void BindingData()
        {
            col_CheDoDDCS.DataBindings.Add("Text", DataSource, "HuongDtri");
            colNgayGio.DataBindings.Add("Text", DataSource, "NgayNhapHT").FormatString = "{0:dd/MM/yyyy HH:mm}";
            colTenCBDB.DataBindings.Add("Text", DataSource, "TenCB");
            colDienbien.DataBindings.Add("Text", DataSource, "DienBien1");
            colYLenh.DataBindings.Add("Text", DataSource, "YLenh");
            colTenCB.DataBindings.Add("Text", DataSource, "TenCB");
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void rep_ToDieuTri_YHCT_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
                xrTableCell6.Text = "Ngày";

            xrLabel4.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel19.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
