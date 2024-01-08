using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_phieuketsquatebaonuocdich : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_phieuketsquatebaonuocdich()
        {
            InitializeComponent();
        }
        public string socds;
        public string capcuas;
        public string hotens;
        public string diachis;
        public string gtinhs;
        public string tuois;
        public string namsinhs;
        public string sothes;
        public string buongs;
        public string giuongs;
        public string chuandoans;
        public string khoas;
        public string so1s;
        public string so2s;
        public string so3s;
        public string so4s;
        public string so5s;
        public string so6s;
        public string so7s;
        public string so8s;
        public string so9s;
        public string so10s;
        public string so11s;
        public string so12s;
        public string so13s;
        public string so14s;
        public string so15s;
        public string so16s;
        public string ketquas1;
        public string ketquas2;
        public string ketquas3;
        public string ketquas4;
        public string ketquas5;
        public string ketquas6;
        public string ketquas7;
        public string ketquas8;
        public string ketquas9;
        public string ketquas10;
        public string ketquas11;
        public string ketquas12;
        public string ketquas13;
        public string ketquas14;
        public string ketquas15;
        public string ketquas16;
        public  void binhdinh()
        {
            NAMSINH.Text = namsinhs;
            KHOA.Text = khoas;
            HOTEN.Text = hotens;
            DIACHI.Text = diachis;
            TUOI.Text = tuois;
            SOTHE.Text = sothes;
            BUONG.Text = buongs;
            GIUONG.Text = giuongs;
            CHUANDOAN.Text = chuandoans;
            if (capcuas == "True") {
                capcuu.Checked = true;

            }
            else {

                thuong.Checked = true;
            }
         
            if (gtinhs == "1") 
            {
                nam.Checked = true;
            }
            if (gtinhs == "0")
            {
                nu.Checked = true;
            }
            CQ.Text = DungChung.Bien.TenCQ;
            CQCQ.Text = DungChung.Bien.TenCQCQ;
            Socd.Text = socds;
            SO1.Text=so1s;
            SO2.Text =so2s;
            SO3.Text =so3s;
            SO4.Text =so4s;
            SO5.Text =so5s;
            SO6.Text =so6s;
            SO71.Text =so7s;
            SO8.Text =so8s;
            SO9.Text =so9s;
            SO10.Text =so10s;
            SO11.Text =so11s;
            SO12.Text =so12s;
            SO13.Text =so13s;
            SO14.Text =so14s;
            SO15.Text =so15s;
            SO16.Text = so16s;
            ketqua1.Text = ketquas1;
            ketqua2.Text = ketquas2;
            ketqua3.Text = ketquas3;
            ketqua4.Text = ketquas4;
            ketqua5.Text = ketquas5;
            ketqua6.Text = ketquas6;
            ketqua7.Text = ketquas7;
            ketqua8.Text = ketquas8;
            ketqua9.Text = ketquas9;
            ketqua10.Text = ketquas10;
            ketqua11.Text = ketquas11;
            ketqua12.Text = ketquas12;
            ketqua13.Text = ketquas13;
            ketqua14.Text = ketquas14;
            ketqua15.Text = ketquas15;
            ketqua16.Text = ketquas16;
            
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_PhieuXN_Sub repsub = (rep_PhieuXN_Sub)xrSubreport1.ReportSource;
            repsub.NGAYKIXN.Value = "Ngày........tháng..........năm..........  ";
            repsub.NGAYKYDT.Value = "Ngày........tháng..........năm..........  ";
            repsub.BSDT.Value = BSDT.Value.ToString();
            repsub.TKXN.Value = BSTH.Value.ToString();
            repsub.tb12128.ForeColor = System.Drawing.Color.Black;
        }

        private void rep_phieuketsquatebaonuocdich_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12128")
            {
                GroupFooter1.Visible = true;
                ReportFooter.Visible = false;
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "12345"&&DungChung.Bien.MaBV != "24297")
            {
                xrLabel28.Visible = false;
                NAMSINH.Visible = false;
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                txtBarcode.Visible = lblBarcode.Visible = true;
                lblKhoaXN.Visible = true;
                xrLabel3.Visible = false;
            }
            if(DungChung.Bien.MaBV == "27001")
            {
                xrLabel32.Text = "Y BÁC SỸ";
                xrLabel33.Text = "PHÒNG XÉT NGHIỆM";
            }
            if (DungChung.Bien.MaBV == "26007")
                xrBarCode1.Visible = true;
            if (DungChung.Bien.MaBV == "27777")
            {
                picLogo.Image = DungChung.Ham.GetLogo();
                colDiaChi.Text = DungChung.Ham.GetDiaChiBV();
            }

            if (DungChung.Bien.MaBV == "24272")
            {
                picLogo.Visible = false;
                xrPictureBox1.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }

            if (DungChung.Bien.MaBV == "30372")
            {
                picLogo.Visible = false;
                xrPictureBox2.Visible = true;
            }
        }
    }
}
