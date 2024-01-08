using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.Phieu
{
    public partial class rep_ToDieuTri_YHCT_20001_QD1941 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_ToDieuTri_YHCT_20001_QD1941()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }
        public void BindingData()
        {
            //col_CheDoDDCS.DataBindings.Add("Text", DataSource, "HuongDtri");
            colNgayGio.DataBindings.Add("Text", DataSource, "NgayNhapHT").FormatString = "{0:dd/MM/yyyy HH:mm}";
            colDienbien.DataBindings.Add("Text", DataSource, "DienBien1");
            colYLenh.DataBindings.Add("Text", DataSource, "YLenh");
            if (DungChung.Bien.MaBV == "24297")
            {
                colTenCBDB.DataBindings.Add("Text", DataSource, "TenCB");
                colTenCBYL.DataBindings.Add("Text", DataSource, "TenCB");
            }
        }
        int a = 0;
        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (a % 2 == 0)
            {
                SubBand1.Visible = true;
            }
            else
                SubBand1.Visible = false;
            a++;
        }

        private void rep_ToDieuTri_YHCT_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "20001")
            //    xrTableCell6.Text = "Ngày";
            xrLabel19.Text = DungChung.Bien.TenCQ.ToUpper();
            //xrLabel1.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void PageHeader_AfterPrint(object sender, EventArgs e)
        {
           
        }
    }
}
