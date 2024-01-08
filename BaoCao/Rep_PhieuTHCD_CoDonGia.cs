using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuTHCD_CoDonGia : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuTHCD_CoDonGia()
        {
            InitializeComponent();

        }
        public void BindingData()
        {
            colTenTN1.DataBindings.Add("Text", DataSource, "TenTN");
            colTenTN.DataBindings.Add("Text", DataSource, "TenTN");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            colSoLuong1.DataBindings.Add("Text", DataSource, "SoLuong");
            colTenchidinh.DataBindings.Add("Text", DataSource, "TenDV");
            colTenchidinh1.DataBindings.Add("Text", DataSource, "TenDV");
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                GroupHeader1.SortingSummary.Enabled = true;
                GroupHeader1.SortingSummary.FieldName = "STT";
                GroupHeader1.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            celDonGia_T.DataBindings.Add("Text", DataSource, "DonGia");
            celDonGia_T.Summary.FormatString = DungChung.Bien.FormatString[1];

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenBSDT.Text = TenBS.Value.ToString();
            //txtGDBHYT.Text = DungChung.Bien.GiamDinhBH;
            //if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30004")
            //    colTenBSDT.Visible = false;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "27777")
            {
                xrLabel5.Visible = false;
                xrLabel6.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "27777")
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                xrTable2.Visible = true;
                xrTable1.Visible = true;
                xrTable8.Visible = false;
                xrTable5.Visible = true;
                if (DungChung.Bien.MaBV == "27777") 
                {
                    xrPictureBox1.Visible = false;
                    xrPictureBox2.Visible = true;
                }
            }
            else
            {
                SubBand2.Visible = true;
                SubBand1.Visible = false;
            }
        }

    }
}
