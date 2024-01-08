using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuChiDinh_CDHA : DevExpress.XtraReports.UI.XtraReport
    {
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public repPhieuChiDinh_CDHA()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27183")
            {
                SubBand2.Visible = false;
                SubBand3.Visible = true;
                SubBand1.Visible = false;
                SubBand4.Visible = true;
                xrTable5.Visible = false;
                GroupHeader1.Visible = false;

            }
            xrPictureBox1.Image = xrPictureBox2.Image = xrPictureBox3.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "27777")
            {
                SubBand6.Visible = false;
                SubBand7.Visible = false;
                SubBand8.Visible = true;
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV=="30372")
            {
                kpth.Visible = false;
            }
            xrPictureBox1.Image = xrPictureBox2.Image = xrPictureBox3.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV.Substring(0, 2) == "12")
                colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper() + "\n" + DungChung.Bien.TenCQ.ToUpper();
            else
                colTenCQCQ.Text = colTenCQCQ2.Text = xrLabel20.Text = DungChung.Bien.TenCQ.ToUpper();
            //colTenCQ.Text = DungChung.Bien.TenCQ;
            if (DungChung.Bien.MaBV == "30004")
                colTenBSDT.Visible = false;
            xrBarCode1.Visible = true;
            if (DungChung.Bien.MaBV == "30010" || DungChung.Bien.MaBV == "27001")
                kpth.Visible = true;
            if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27183" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                xrLabel15.Visible = true;
            if (DungChung.Bien.MaBV == "26007")
                xrBarCode1.Visible = true;
            if (DungChung.Bien.MaBV == "24297")
                xrLabel5.Visible = xrLabel7.Visible = xrLabel8.Visible = kpth.Visible = false;
            else if (DungChung.Bien.MaBV == "24012") 
            {
                SubBand2.Visible = true;
                SubBand3.Visible = false;
                xrPictureBox2.Visible = false;
                xrPictureBox1.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "27777")
            {
                SubBand6.Visible = false;
                SubBand7.Visible = false;
                SubBand8.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "24272")
            {
                SubBand6.Visible = false;
                SubBand7.Visible = true;
                xrPictureBox2.Visible = false;
                if (DungChung.Bien.MaBV == "24272")
                {
                    xrPictureBox1.Visible = true;
                    
                }
            }
            else
            {
                SubBand6.Visible = true;
                SubBand7.Visible = false;
                SubBand8.Visible = false;
                if (DungChung.Bien.MaBV != "24297")
                {
                    xrPictureBox2.Visible = false;
                }
            }
        }
        public void Databind()
        {
            if (DungChung.Bien.MaBV == "30010")
            {
                //int mkpth = Convert.ToInt32(KPthuchien.Value);
                //var a = from a1 in DataContect.KPhongs.Where(p => p.MaKP == mkpth) select a1;
                //ktThucHien.Value = a.Select(p => p.TenKP).First().ToString();
                ktThucHien.Value = KPthuchien.Value;
            }
            colTenNhomDVg2.DataBindings.Add("Text", DataSource, "TenTN");
            GroupHeader1.GroupFields.Add(new GroupField("STT"));
            colYCKT.DataBindings.Add("Text", DataSource, "TenDV");
            colYCKT2.DataBindings.Add("Text", DataSource, "TenDV");
            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "30372")
            {
                colYCKT.DataBindings.Add("Text", DataSource, "ylenh");
                colYCKT2.DataBindings.Add("Text", DataSource, "ylenh");
            }
            colThanhTien.DataBindings.Add("Text", DataSource, "DonGia");
            colThanhTien.Summary.FormatString = DungChung.Bien.FormatString[1];
            //colThanhTienG.DataBindings.Add("Text", DataSource, "DonGia");
            //colThanhTienG.Summary.FormatString = DungChung.Bien.FormatString[1];
            colThanhTienT.DataBindings.Add("Text", DataSource, "DonGia");
            colThanhTienT.Summary.FormatString = DungChung.Bien.FormatString[1];


        }

        public void BindingData()
        {
            //colYeuCau1.DataBindings.Add("Text", DataSource, "TenDV");
            colTenNhomDVg2.DataBindings.Add("Text", DataSource, "TenTN");
            GroupHeader1.GroupFields.Add(new GroupField("STT"));
            colYCKT.DataBindings.Add("Text", DataSource, "TenDV");
            colThanhTien.DataBindings.Add("Text", DataSource, "DonGia");
            colThanhTien.Summary.FormatString = DungChung.Bien.FormatString[1];
            //colThanhTienG.DataBindings.Add("Text", DataSource, "DonGia");
            //colThanhTienG.Summary.FormatString = DungChung.Bien.FormatString[1];
            colThanhTienT.DataBindings.Add("Text", DataSource, "DonGia");
            colThanhTienT.Summary.FormatString = DungChung.Bien.FormatString[1];

        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            colTenCQ.Text = DungChung.Bien.TenCQ;
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            if (BSDT.Value!=null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, BSDT.Value.ToString());
            }
            if(/*DungChung.Bien.MaBV =="30372" ||*/ DungChung.Bien.MaBV == "27023")
            {
                xrLabel3.Visible = true;
                xrLabel18.Visible = true;
                xrLabel17.Visible = true;
                xrLabel19.Visible = true;
            }
            if (DungChung.Bien.MaBV != "24012")
            {
                xrTable5.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "27777")
            {
                SubBand6.Visible = false;
                SubBand7.Visible = false;
                SubBand8.Visible = true;
            }
            else
            {
                SubBand6.Visible = true;
                SubBand7.Visible = false;
                SubBand8.Visible = false;
                if (DungChung.Bien.MaBV != "24297")
                {
                    xrPictureBox2.Visible = false;
                }
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                SubBand1.Visible = true;
                SubBand4.Visible = false;
            }
        }
    }
}
