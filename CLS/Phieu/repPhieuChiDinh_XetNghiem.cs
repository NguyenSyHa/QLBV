using DevExpress.XtraReports.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuChiDinh_XetNghiem : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuChiDinh_XetNghiem()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27183")
            {
                SubBand6.Visible = false;
                SubBand7.Visible = false;
                SubBand9.Visible = true;
                sub_24012_2.Visible = false;
                sub_24012_1.Visible = false;
                SubBand2.Visible = false;
                SubBand10.Visible = true;
                SubBand1.Visible = false;
                SubBand3.Visible = true;
            }
            if (DungChung.Bien.MaBV == "24012")
            {
                SubBand6.Visible = false;
                SubBand9.Visible = false;
                SubBand2.Visible = false;
                sub_24012_2.Visible = true;
                sub_24012_1.Visible = true;
                SubBand10.Visible = false;
                SubBand1.Visible = false;
                SubBand3.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24272")
            {
                xrTable5.Visible = false;
                SubBand4.Visible = false;
                SubBand11.Visible = true;
                SubBand6.Visible = false;
                SubBand9.Visible = true;
                SubBand2.Visible = false;
                SubBand10.Visible = true;
                SubBand1.Visible = false;
                xrPictureBox1.Visible = false;
                
            }
            xrPictureBox3.Image = xrPictureBox2.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "30372")
            {
                SubBand4.Visible = false; 
                SubBand12.Visible = true;
                xrPictureBox1.Visible = true;
                xrPictureBox2.Visible = false;
            }
        }

        private bool HienThiGH = false;

        public repPhieuChiDinh_XetNghiem(bool hienthi)
        {
            InitializeComponent();
            HienThiGH = hienthi;
            if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27183")
            {
                SubBand6.Visible = false;
                SubBand9.Visible = true;
                SubBand2.Visible = false;
                sub_24012_2.Visible = false;
                sub_24012_1.Visible = false;
                SubBand10.Visible = true;
                SubBand1.Visible = false;
                SubBand3.Visible = true;
            }
            xrPictureBox3.Image = xrPictureBox2.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "30372")
            {
                SubBand4.Visible = false;
                SubBand12.Visible = true;
                xrPictureBox1.Visible = true;
                xrPictureBox2.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                SubBand4.Visible = true;
                xrPictureBox2.Visible = false;
            }
        }

        private QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public void BindingData()
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                colYCKT1.DataBindings.Add("Text", DataSource, "ylenh");
                colTenNhomDVg2.DataBindings.Add("Text", DataSource, "TenTN");
                colTenNhomDVSB.DataBindings.Add("Text", DataSource, "TenTN");
                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
                GroupHeader2.GroupFields.Add(new GroupField("STT"));
                ColDonGia.DataBindings.Add("Text", DataSource, "DonGia");
                ColThanhTien_24012.DataBindings.Add("Text", DataSource, "DonGia");
            }
            if (DungChung.Bien.MaBV == "24297")
            {
                colYCKT.DataBindings.Add("Text", DataSource, "TenDV");
                colTenNhomDVg2.DataBindings.Add("Text", DataSource, "TenTN");
                colTenNhomDVSB.DataBindings.Add("Text", DataSource, "TenTN");
                GroupHeader1.GroupFields.Add(new GroupField("STT"));
                GroupHeader2.GroupFields.Add(new GroupField("STT"));
                colThanhTienG.DataBindings.Add("Text", DataSource, "DonGia");
                colThanhTienG.Summary.FormatString = DungChung.Bien.FormatString[1];
                colThanhTienT.DataBindings.Add("Text", DataSource, "DonGia");
                colThanhTienT.Summary.FormatString = DungChung.Bien.FormatString[1];
                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                {
                    colThanhTien.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                }
                colThanhTienG.DataBindings.Add("Text", DataSource, "DonGia");
                colThanhTienG.Summary.FormatString = DungChung.Bien.FormatString[1];
                colThanhTienT.DataBindings.Add("Text", DataSource, "DonGia");
                colThanhTienT.Summary.FormatString = DungChung.Bien.FormatString[1];
                if (DungChung.Bien.MaBV == "30010")
                {
                    int mkpth = Convert.ToInt32(KpThucHien.Value);
                    var a = from a1 in _data.KPhongs.Where(p => p.MaKP == mkpth) select a1;
                    dckp.Text = a.Select(p => p.TenKP).First().ToString();
                }
            }
            else if (DungChung.Bien.MaBV == "24012")
            {
                colYCKT1.DataBindings.Add("Text", DataSource, "ylenh");
                colYCKT2.DataBindings.Add("Text", DataSource, "ylenh");
                colTenNhomDVg2.DataBindings.Add("Text", DataSource, "TenTN");
                colTenNhomDVSB.DataBindings.Add("Text", DataSource, "TenTN");
                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
                GroupHeader2.GroupFields.Add(new GroupField("STT"));
                ColDonGia.DataBindings.Add("Text", DataSource, "DonGia");
                ColThanhTien_24012.DataBindings.Add("Text", DataSource, "DonGia");
            }
            else if (DungChung.Bien.MaBV == "30004")//his-172 26/05/2021
            {
                colYCKT2.DataBindings.Add("Text", DataSource, "TenDV");
                //colTenNhomDVg2.DataBindings.Add("Text", DataSource, "TenTN");
                colTenNhomDVSB.DataBindings.Add("Text", DataSource, "TenTN");
                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
                //GroupHeader2.GroupFields.Add(new GroupField("STT"));
                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                {
                    colThanhTien.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                }
                colThanhTienG.DataBindings.Add("Text", DataSource, "DonGia");
                colThanhTienG.Summary.FormatString = DungChung.Bien.FormatString[1];
                colThanhTienT.DataBindings.Add("Text", DataSource, "DonGia");
                colThanhTienT.Summary.FormatString = DungChung.Bien.FormatString[1];
                if (DungChung.Bien.MaBV == "30010")
                {
                    int mkpth = Convert.ToInt32(KpThucHien.Value);
                    var a = from a1 in _data.KPhongs.Where(p => p.MaKP == mkpth) select a1;
                    dckp.Text = a.Select(p => p.TenKP).First().ToString();
                }
            }
            else
            {
                if (DungChung.Bien.MaBV == "30007")
                {
                    colYCKT.DataBindings.Add("Text", DataSource, "TenDV");
                }
                else
                    colYCKT.DataBindings.Add("Text", DataSource, "TenDV");
                if (DungChung.Bien.MaBV == "30372")
                {
                    colYCKT2.DataBindings.Add("Text", DataSource, "ylenh");
                }
                else
                {
                    colYCKT2.DataBindings.Add("Text", DataSource, "TenDV");
                }
                if (DungChung.Bien.MaBV == "24272")
                {
                    colYCKT.DataBindings.Add("Text", DataSource, "TenDVRG");
                }
                colTenNhomDVg2.DataBindings.Add("Text", DataSource, "TenTN");
                colTenNhomDVSB.DataBindings.Add("Text", DataSource, "TenTN");
                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
                GroupHeader2.GroupFields.Add(new GroupField("STT"));
                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "27777")
                {
                    colThanhTien.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                }

                colThanhTienG.DataBindings.Add("Text", DataSource, "DonGia");
                colThanhTienG.Summary.FormatString = DungChung.Bien.FormatString[1];
                colThanhTienT.DataBindings.Add("Text", DataSource, "DonGia");
                colThanhTienT.Summary.FormatString = DungChung.Bien.FormatString[1];
                if (DungChung.Bien.MaBV == "30010")
                {
                    int mkpth = Convert.ToInt32(KpThucHien.Value);
                    var a = from a1 in _data.KPhongs.Where(p => p.MaKP == mkpth) select a1;
                    dckp.Text = a.Select(p => p.TenKP).First().ToString();
                }

                if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27183")
                {
                    SubBand6.Visible = false;
                    SubBand9.Visible = true;
                    SubBand2.Visible = false;
                    SubBand10.Visible = true;
                    SubBand1.Visible = false;
                    SubBand3.Visible = true;
                }
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (MaCB.Value != null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCB.Value.ToString());
                if (DungChung.Bien.MaBV == "24012")
                {
                    colTenBSDT24012.Text = DungChung.Ham._getTenCB(DataContect, MaCB.Value.ToString());
                }
            }
            if (DungChung.Bien.MaBV == "30004")
                colTenBSDT.Visible = false;
        }

        private void ReportFooter_BeforePrint_1(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (MaCB.Value != null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCB.Value.ToString());
                if (DungChung.Bien.MaBV == "24012")
                {
                    colTenBSDT24012.Text = DungChung.Ham._getTenCB(DataContect, MaCB.Value.ToString());
                }
            }
            if (DungChung.Bien.MaBV == "30004")
                colTenBSDT.Visible = false;
            if (DungChung.Bien.MaBV == "14018")
            {
                SubBand5.Visible = true;
            }
            if (DungChung.Bien.MaBV == "24012")
            {
                SubBand1.Visible = true;
                SubBand8.Visible = true;
            }
            else
            {
                SubBand3.Visible = true;
                SubBand8.Visible = false;
            }
            if (/*DungChung.Bien.MaBV == "30372" ||*/ DungChung.Bien.MaBV == "27023")
            {
                xrLabel18.Visible = true;
                xrLabel14.Visible = true;
                xrLabel16.Visible = true;
                xrLabel17.Visible = true;
            }
        }

        private void ReportHeader_BeforePrint_1(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30372")
            {
                dckp.Visible = false;

            }
            if (DungChung.Bien.MaBV == "24012")
            {
                sub_24012_1.Visible = true;
                SubBand4.Visible = false;
                SubBand7.Visible = true;
                SubBand9.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27183")
            {
                SubBand4.Visible = true;
                SubBand6.Visible = false;
                SubBand9.Visible = true;
                SubBand2.Visible = false;
                SubBand10.Visible = true;
                SubBand1.Visible = false;
                SubBand3.Visible = true;
            }
            else
            {
                SubBand4.Visible = true;
            }
            if (DungChung.Bien.MaBV == "27777" || DungChung.Bien.MaBV == "24272")
            {
                xrTable5.Visible = true;
                xrPictureBox2.Visible = false;
                xrPictureBox1.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
                if (DungChung.Bien.MaBV == "24272")
                {
                    xrTable5.Visible = false;
                    SubBand4.Visible = false;
                    SubBand11.Visible = true;
                    SubBand6.Visible = false;
                    SubBand9.Visible = true;
                    SubBand2.Visible = false;
                    SubBand10.Visible = true;
                    SubBand1.Visible = false;
                    xrPictureBox1.Visible = false;
                    xrPictureBox3.Image = DungChung.Ham.GetLogo();
                }
            }
            if (DungChung.Bien.MaBV.Substring(0, 2) == "12")
                colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper() + "\n" + DungChung.Bien.TenCQ.ToUpper();
            else
                colTenCQCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "24012")
            {
                if (DungChung.Bien.MaBV.Substring(0, 2) == "12")
                    colTenCQCQ24012.Text = DungChung.Bien.TenCQCQ.ToUpper() + "\n" + DungChung.Bien.TenCQ.ToUpper();
                else
                    colTenCQCQ24012.Text = DungChung.Bien.TenCQ.ToUpper();
            }
            // colTenCQ.Text = DungChung.Bien.TenCQ;
            GroupHeader1.Visible = HienThiGH;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                lab2.Visible = false;
            if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "30010" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                dckp.Visible = true;
            if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27183" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                xrLabel12.Visible = true;
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                GroupHeader2.Visible = true;
                GroupHeader1.Visible = false;
                SubBand10.Visible = true;
                SubBand2.Visible = false;
                xrTable5.Visible = true;
                xrTableCell6.Text = "THÀNH TIỀN";
                this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
          | DevExpress.XtraPrinting.BorderSide.Bottom)));
                //this.colTenNhomDV.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
                //this.colYCKT.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            }
            else if (DungChung.Bien.MaBV == "24009")
            {
                xrTable5.Visible = false;
            }
            else
            {
                GroupHeader2.Visible = false;
                GroupHeader1.Visible = true;
            }

            if (DungChung.Bien.MaBV == "24297")
            {
                xrLabel5.Visible = xrLabel6.Visible = xrLabel7.Visible = dckp.Visible = false;
                //xrLabel6.Visible = xrLabel7.Visible = true;
                //his-151
                xrLabel8.LocationF = new PointF(14, 223);
                xrLabel9.LocationF = new PointF(66, 223);
                xrLabel10.LocationF = new PointF(116, 223);
                xrLabel11.LocationF = new PointF(193, 223);
            }
            else
            {
                xrPictureBox2.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24012")
            {
                sub_24012_1.Visible = true;
                SubBand4.Visible = false;
                SubBand7.Visible = true;
                SubBand9.Visible = false;
            }
            xrPictureBox3.Image = xrPictureBox2.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "30372")
            {
                SubBand4.Visible = false;
                SubBand12.Visible = true;
                xrPictureBox1.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
                xrPictureBox2.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                SubBand4.Visible = true;
                xrPictureBox2.Visible = false;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                sub_24012_2.Visible = true;
                SubBand10.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27183")
            {
                SubBand4.Visible = true;
                SubBand6.Visible = false;
                SubBand9.Visible = true;
                SubBand2.Visible = false;
                SubBand10.Visible = true;
                SubBand1.Visible = false;
                SubBand3.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                GroupHeader2.Visible = true;
                GroupHeader1.Visible = false;
                SubBand10.Visible = false;
                SubBand2.Visible = true;
                xrTable5.Visible = true;
                xrTableCell6.Text = "THÀNH TIỀN";
                this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
          | DevExpress.XtraPrinting.BorderSide.Bottom)));
                //this.colTenNhomDV.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
                //this.colYCKT.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            }
            else if (DungChung.Bien.MaBV == "24009")
            {
                xrTable5.Visible = false;
            }
            else
            {
                SubBand4.Visible = true;
                if (DungChung.Bien.MaBV == "24272")
                {
                    SubBand2.Visible = false;
                }
            }
            if (DungChung.Bien.MaBV == "24012")
            {
                sub_24012_1.Visible = true;
                SubBand4.Visible = false;
                SubBand7.Visible = true;
                SubBand9.Visible = false;
            }
            xrPictureBox3.Image = xrPictureBox2.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "30372")
            {
                xrPictureBox1.Visible = true;
                xrPictureBox2.Visible = false;
            }
        }

        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void SubBand8_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void PageFooter_BeforePrint_1(object sender, CancelEventArgs e)
        {
        }
    }
}