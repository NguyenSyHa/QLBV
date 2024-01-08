using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
//using System.Text;
//using System.Data;

namespace QLBV.BaoCao
{
    public partial class rep_phieusieuamdoppler_tim_27001 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_phieusieuamdoppler_tim_27001()
        {
            InitializeComponent();
        }
        public string[] ketqua = new string[15];
        public string[] ketquac = new string[15];

        public string ketluans;
        public string loirans;
        public string hotes;
        public string dichis;
        public string tuois;
        public string thebhyts;
        public string gioitinhs;
        public string khoas;
        public string khoaThucHiens;
        public string yeucaus;
        public string chuandoans;
        public string CBTT;
        public string BSCD;
        public string ngaythuchiens;
        public string ngaychidinhs;
        public void hamloatphieu() 
        {
            HOten.Text = hotes;
            Diachi.Text = dichis;
            tuoi.Text = tuois;
            Chuandoan.Text = chuandoans;
            if (DungChung.Bien.MaBV == "27023")
            {
                txtKhoa.Text = khoaThucHiens;
            }
            //30372 pkdk thuan an
            txtKP.Text = khoas;
            txtBSChiDinh.Text = BSCD;
            txtSoThe.Text = thebhyts;
            txtGioiTinh.Text = gioitinhs;
            hoten1.Text = hotes;
            diachi1.Text = dichis;
            tuoi1.Text = tuois;
            chandoan1.Text = chuandoans;
            //end 30372
            so1.Text = ketqua[0];
            so2.Text = ketqua[1];
            so3.Text = ketqua[2];
            so4.Text = ketqua[3];
            so5.Text = ketqua[4];
            so6.Text = ketqua[5];
            so7.Text = ketqua[6];
            so8.Text = ketqua[7];
            so9.Text = ketqua[8];
            so10.Text = ketqua[9];
            so11.Text = ketqua[10];
            so12.Text = ketqua[11];
            so13.Text = ketqua[12];
            //so14.Text = ketqua[13];
            //so15.Text = ketqua[14];
            van.Text = ketquac[0];
            //van2.Text = ketquac[10];
            dongmach.Text = ketquac[1];
            //dongmach2.Text = ketquac[11];
            phoi.Text = ketquac[2];
            //phoi2.Text = ketquac[12];
            vanbala.Text = ketquac[3];
            //vanbala2.Text = ketquac[13];
            xrTableCell29.Text = ketquac[4];
            ketluan.Text = loirans;
            loidan.Text = ketluans;
            TENcbtt.Text = CBTT;
            //baocao.ketquac[k + 10](QLBV_Library.QLBV_Ham.convertHTML(ketquac[10], "red","red",'',':',"yes"));
            if (!string.IsNullOrEmpty(ngaythuchiens))
            {
                ngaythuchien.Text = ngaythuchiens;
            }



            //xrRichText1.Html = (QLBV_Library.QLBV_Ham.convertHTML(ketquac[10], "red", "red", 'G', 'y', "no"));
            xrRichText1.Html = (QLBV_Library.QLBV_Ham.convertHTML(ketquac[10], "red", "red", '\r', '|', "yes"));
            xrRichText2.Html = (QLBV_Library.QLBV_Ham.convertHTML(ketquac[11], "red", "red", '\r', '|', "yes"));
            xrRichText3.Html = (QLBV_Library.QLBV_Ham.convertHTML(ketquac[12], "red", "red", '\r', '|', "yes"));
            xrRichText4.Html = (QLBV_Library.QLBV_Ham.convertHTML(ketquac[13], "red", "red", '\r', '|', "yes"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //His 1465 bv thuận an
            if (DungChung.Bien.MaBV == "30372")
            {
                SubBand2.Visible = false;
                SubBand1.Visible = true;
                sdtthuanan.Visible = true;
                xrPictureBox1.Visible = true;
                tenbvthuanan.Text = DungChung.Bien.TenCQ.ToUpper();
                dcthuanan.Text = DungChung.Bien.DiaChi;
            }
            //end his 1465 bv thuận an
            else
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                cq.Text = DungChung.Bien.TenCQ.ToUpper();
                if (DungChung.Bien.DiaChi != null)
                {
                    dc.Text = "Đ/C: " + DungChung.Bien.DiaChi;
                }
                if (DungChung.Bien.MaBV == "27001")
                {
                    dc.Text = "Đ/C: Số 36 A Lê Phụng Hiểu, Phường Vệ An";
                    xrLabel3.Visible = true;
                    xrLabel2.Visible = true;
                    xrLabel7.Visible = true;
                }
            }
        }
    }
}
