using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBbKKThuoc_C32 : DevExpress.XtraReports.UI.XtraReport
    {
        //private bool HTNuocSX;
        //bool _Thang = true;
        public repBbKKThuoc_C32()
        {
            InitializeComponent();
           
        }

        //public repBbKKThuoc_C32()
        //{
        //    // TODO: Complete member initialization
        //    this.HTNuocSX = HTNuocSX;
        //    _Thang = Thang;
        //    InitializeComponent();
        //}
        public void BindingData()
        {

            colTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            celMaDV.DataBindings.Add("Text", DataSource, "SoDK");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia");
            celCLuongKK.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
            celTTKK.DataBindings.Add("Text", DataSource, "ThanhTienSS").FormatString = DungChung.Bien.FormatString[1];

            celTTKK_T.DataBindings.Add("Text", DataSource, "ThanhTienSS");
            celTTKK_T.Summary.FormatString = DungChung.Bien.FormatString[1];
        }

        private void repBbKKThuoc_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;
            TenCQ.Value = DungChung.Bien.TenCQ;
            string _nt = NT.Value.ToString();
            string _ct = NT.Value.ToString();


        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = colTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = colTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
            //xrRichText1.Text = TV1goi.Value.ToString();
            xrLine2.Visible = xrLine3.Visible = DungChung.Bien.MaBV == "20001" ? true : false;
        }

        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void colHanDung_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtGiamDoc.Text = DungChung.Bien.GiamDoc;
            txtThuKho.Text = DungChung.Bien.ThuKho;
            txtKeToanTR.Text = DungChung.Bien.KeToanTruong;
        }

        private void GroupHeader4_BeforePrint(object sender, CancelEventArgs e)
        {
//}

        }


    }
}
