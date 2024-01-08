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
    public partial class rep_phieusieuamdoppler_tim_01830 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_phieusieuamdoppler_tim_01830()
        {
            InitializeComponent();
        }
        public string[] ketqua = new string[15];
        public string[] ketquac = new string[15];

        public string ketluans;
        public string loirans;
        public string hotes;
        public string gtinh;
        public string chidinh;
        public string dichis;
        public string tuois;
        public string khoas;
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
            GTinh.Text = gtinh;
            ChiDinh1.Text = chidinh;
            BSChidinh.Text = BSCD;
            Chuandoan.Text = chuandoans;
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
            if (ngaythuchiens != null && ngaythuchiens!="")
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

        }
    }
}
