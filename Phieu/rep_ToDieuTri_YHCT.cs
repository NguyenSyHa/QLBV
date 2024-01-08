using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.Phieu
{
    public partial class    rep_ToDieuTri_YHCT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_ToDieuTri_YHCT()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }
        public void BindingData() {
            col_CheDoDDCS.DataBindings.Add("Text", DataSource, "HuongDtri");
            if (DungChung.Bien.MaBV == "20001")
            {
                colNgayGio.DataBindings.Add("Text", DataSource, "NgayNhapHT").FormatString = "{0:dd/MM/yyyy HH:mm}";
                colTenCBDB.DataBindings.Add("Text", DataSource, "TenCB");
                colTenCBDB1.DataBindings.Add("Text", DataSource, "TenCB");
            }
            else
            {
                colNgayGio.DataBindings.Add("Text", DataSource, "NgayNhap");

            }
            if (DungChung.Bien.MaBV == "24272")
            {
                colTenCBDB.DataBindings.Add("Text", DataSource, "TenCB");
            }
            colDienbien.DataBindings.Add("Text", DataSource, "DienBien1");
            colYLenh.DataBindings.Add("Text", DataSource, "YLenh");
            colTenCB.DataBindings.Add("Text", DataSource, "TenCB");
            col_CheDoDDCS1.DataBindings.Add("Text", DataSource, "HuongDtri");
            colNgayGio1.DataBindings.Add("Text", DataSource, "NgayNhap");
            colDienbien1.DataBindings.Add("Text", DataSource, "DienBien1");
            colYLenh1.DataBindings.Add("Text", DataSource, "YLenh");
            colTenCB1.DataBindings.Add("Text", DataSource, "TenCB");


            colNgayGio2.DataBindings.Add("Text", DataSource, "NgayNhap");
            colKetLuanYHCT24014.DataBindings.Add("Text", DataSource, "DienBien1");
            colYLYHCT24012.DataBindings.Add("Text", DataSource, "YLenh");
            col_CheDoDDCS01.DataBindings.Add("Text", DataSource, "HuongDtri");
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void rep_ToDieuTri_YHCT_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
                xrTableCell6.Text = "Ngày";
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();

            xrLabel4.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel19.Text = DungChung.Bien.TenCQ.ToUpper();

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                SubBand2.Visible = true;
                SubBand4.Visible = true;
            }
            else if(DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24009")
            {
                SubBand5.Visible = true;
                SubBand6.Visible = true;
                SubBand3.Visible = false;
                SubBand4.Visible = false;
                SubBand2.Visible = false;
            }
            else
            {
                SubBand5.Visible = true;
                SubBand3.Visible = true;
            }
            

            if(DungChung.Bien.MaBV == "20001")
            {
                SubBand1.Visible = true;
                ReportHeader.Visible = false;
            }
            else
            {
                SubBand1.Visible = false;
                ReportHeader.Visible = true;
            }
        }
    }
}
