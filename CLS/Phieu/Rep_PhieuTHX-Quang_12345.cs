using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuTHX_Quang_12345 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuTHX_Quang_12345()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            if(DungChung.Bien.MaBV == "24297")
            {
                colTenTN.DataBindings.Add("Text", DataSource, "TenTN");
                colTenTN1.DataBindings.Add("Text", DataSource, "TenTN");
                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
                colYlenh.DataBindings.Add("Text", DataSource, "ChiDinh1");
                colYlenh1.DataBindings.Add("Text", DataSource, "ChiDinh1");
                colTenchidinh.DataBindings.Add("Text", DataSource, "TenDV");
                colTenchidinh1.DataBindings.Add("Text", DataSource, "TenDV");
                colThanhTien.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                colThanhTienG.DataBindings.Add("Text", DataSource, "DonGia");
                colThanhTienG.Summary.FormatString = DungChung.Bien.FormatString[1];
                colThanhTienT.DataBindings.Add("Text", DataSource, "DonGia");
                colThanhTienT.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            else
            {
                colTenTN.DataBindings.Add("Text", DataSource, "TenTN");
                colYlenh.DataBindings.Add("Text", DataSource, "ChiDinh1");
                colTenchidinh.DataBindings.Add("Text", DataSource, "TenDV");
                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
                colThanhTien.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                colThanhTienG.DataBindings.Add("Text", DataSource, "DonGia");
                colThanhTienG.Summary.FormatString = DungChung.Bien.FormatString[1];
                colThanhTienT.DataBindings.Add("Text", DataSource, "DonGia");
                colThanhTienT.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenBSDT.Text = TenBS.Value.ToString();
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                //colTenTKXN.Visible = false;
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ;
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
                if (DungChung.Bien.MaBV == "24297")
                {
                    xrPictureBox1.Visible = false;
                    xrTable3.Visible = false;
                    xrTable7.Visible = true;
                    xrTable1.Visible = false;
                    xrTable6.Visible = false;
                    xrTable5.Visible = false;
                    xrTable8.Visible = true;
                    xrTable9.Visible = true;
                }
                else
                    xrPictureBox2.Visible = false;
            }
            else
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }

    }
}
