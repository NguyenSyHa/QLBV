using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_BCDieuTriNoiTruTaiKP_12122 : DevExpress.XtraReports.UI.XtraReport
    {
        List<KPhong> lkp = new List<KPhong>();
        public rep_BCDieuTriNoiTruTaiKP_12122()
        {
            InitializeComponent();
              QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
          lkp = data.KPhongs.ToList();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }


        internal void BindingData()
        {
            lblMaKhoa.DataBindings.Add("Text", DataSource, "MaKP");
           
            cel4.DataBindings.Add("Text", DataSource, "DK");
            cel5.DataBindings.Add("Text", DataSource, "VV");
            cel6.DataBindings.Add("Text", DataSource, "TSNT");
            cel7.DataBindings.Add("Text", DataSource, "TS6T");
            cel8.DataBindings.Add("Text", DataSource, "TS15T");
            cel9.DataBindings.Add("Text", DataSource, "TSHN");
            cel10.DataBindings.Add("Text", DataSource, "TVTS");
            cel11.DataBindings.Add("Text", DataSource, "TV15");
            cel12.DataBindings.Add("Text", DataSource, "TV24H");
            cel13.DataBindings.Add("Text", DataSource, "RV");
            cel14.DataBindings.Add("Text", DataSource, "KQDoGiam");
            cel15.DataBindings.Add("Text", DataSource, "KQKTD");
            cel16.DataBindings.Add("Text", DataSource, "KQKhoi");
            cel17.DataBindings.Add("Text", DataSource, "KQNangHon");
            cel18.DataBindings.Add("Text", DataSource, "BNYHCT");
            cel19.DataBindings.Add("Text", DataSource, "bnConLai");
            cel20.DataBindings.Add("Text", DataSource, "SoNGayDT");
           

           
            cel4R.DataBindings.Add("Text", DataSource, "DK");
            cel5R.DataBindings.Add("Text", DataSource, "VV");
            cel6R.DataBindings.Add("Text", DataSource, "TSNT");
            cel7R.DataBindings.Add("Text", DataSource, "TS6T");
            cel8R.DataBindings.Add("Text", DataSource, "TS15T");
            cel9R.DataBindings.Add("Text", DataSource, "TSHN");
            cel10R.DataBindings.Add("Text", DataSource, "TVTS");
            cel11R.DataBindings.Add("Text", DataSource, "TV15");
            cel12R.DataBindings.Add("Text", DataSource, "TV24H");
            cel13R.DataBindings.Add("Text", DataSource, "RV");
            cel14R.DataBindings.Add("Text", DataSource, "KQDoGiam");
            cel15R.DataBindings.Add("Text", DataSource, "KQKTD");
            cel16R.DataBindings.Add("Text", DataSource, "KQKhoi");
            cel17R.DataBindings.Add("Text", DataSource, "KQNangHon");
            cel18R.DataBindings.Add("Text", DataSource, "BNYHCT");
            cel19R.DataBindings.Add("Text", DataSource, "bnConLai");
            cel20R.DataBindings.Add("Text", DataSource, "SoNGayDT");
           

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

            if (GetCurrentColumnValue("MaKP") != null)
            {
                int MaKP = Convert.ToInt32(GetCurrentColumnValue("MaKP"));
                var tenkp = lkp.Where(p => p.MaKP == MaKP).FirstOrDefault();
                if (tenkp != null)
                {
                    celKhoa.Text = tenkp.TenKP.ToUpper();
                    if (celKhoa.Text.Contains("HSCC") || celKhoa.Text.Contains("HỒI SỨC CẤP CỨU"))
                        cel3.Text = "3";
                    else if (celKhoa.Text=="KHOA LAO")
                        cel3.Text = "20";                   
                    else if (celKhoa.Text.Contains("LAO NGOÀI PHỔI"))
                        cel3.Text = "9";
                    else if (celKhoa.Text.Contains("KHOA PHỔI"))
                        cel3.Text = "28";
                }

            }
        }
    }
}
