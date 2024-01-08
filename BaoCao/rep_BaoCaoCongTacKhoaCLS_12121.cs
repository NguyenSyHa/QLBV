using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BaoCaoCongTacKhoaCLS_12121 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BaoCaoCongTacKhoaCLS_12121()
        {
            InitializeComponent();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            celNgayIn.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " + DateTime.Now.Year;
        }

        int countG = 0;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            countG++;
            switch (countG)
            {
                case 1:
                    celSTTG.Text = "I";
                    break;
                case 2:
                    celSTTG.Text = "II";
                    break;
                case 3:
                    celSTTG.Text = "III";
                    break;
                case 4:
                    celSTTG.Text = "IV";
                    break;
                case 5:
                    celSTTG.Text = "V";
                    break;
                case 6:
                    celSTTG.Text = "VI";
                    break;
                case 7:
                    celSTTG.Text = "VII";
                    break;
                case 8:
                    celSTTG.Text = "VIII";
                    break;
                case 9:
                    celSTTG.Text = "IX";
                    break;
                case 10:
                    celSTTG.Text = "X";
                    break;
                case 11:
                    celSTTG.Text = "XI";
                    break;
                case 12:
                    celSTTG.Text = "XII";
                    break;
                case 13:
                    celSTTG.Text = "XIII";
                    break;
                case 14:
                    celSTTG.Text = "XIV";
                    break;
                case 15:
                    celSTTG.Text = "XV";
                    break;
                case 16:
                    celSTTG.Text = "XVI";
                    break;
                case 17:
                    celSTTG.Text = "XVII";
                    break;
                case 18:
                    celSTTG.Text = "XVIII";
                    break;
                case 19:
                    celSTTG.Text = "XIX";
                    break;
                case 20:
                    celSTTG.Text = "XX";
                    break;
                
            }
        }


        internal void BindingData()
        {
            celKhoa1.DataBindings.Add("Text", DataSource, "Khoa1");
            celKhoa2.DataBindings.Add("Text", DataSource, "Khoa2");
            celKhoa3.DataBindings.Add("Text", DataSource, "Khoa3");
            //celKhoa4.DataBindings.Add("Text", DataSource, "Khoa4");
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celTS.DataBindings.Add("Text", DataSource, "TongSo");
            celNT_BH.DataBindings.Add("Text", DataSource, "NT_BH");
            celNT_HN.DataBindings.Add("Text", DataSource, "NT_HN");
            celNT_TE.DataBindings.Add("Text", DataSource, "NT_TE");
            celNT_VP.DataBindings.Add("Text", DataSource, "NT_VP");
            celNgT_BH.DataBindings.Add("Text", DataSource, "NgT_BH");
            celNgT_HN.DataBindings.Add("Text", DataSource, "NgT_HN");
            celNgT_TE.DataBindings.Add("Text", DataSource, "NgT_TE");
            celNgT_VP.DataBindings.Add("Text", DataSource, "NgT_VP");

            celTenTN.DataBindings.Add("Text", DataSource, "TenTN");
            celTSG.DataBindings.Add("Text", DataSource, "TongSo");
            celKhoa1_G.DataBindings.Add("Text", DataSource, "Khoa1");
            celKhoa2_G.DataBindings.Add("Text", DataSource, "Khoa2");
            celKhoa3_G.DataBindings.Add("Text", DataSource, "Khoa3");
            //celKhoa4_G.DataBindings.Add("Text", DataSource, "Khoa4");
            celNT_BHG.DataBindings.Add("Text", DataSource, "NT_BH");
            celNT_HNG.DataBindings.Add("Text", DataSource, "NT_HN");
            celNT_TEG.DataBindings.Add("Text", DataSource, "NT_TE");
            celNT_VPG.DataBindings.Add("Text", DataSource, "NT_VP");
            celNgT_BHG.DataBindings.Add("Text", DataSource, "NgT_BH");
            celNgT_HNG.DataBindings.Add("Text", DataSource, "NgT_HN");
            celNgT_TEG.DataBindings.Add("Text", DataSource, "NgT_TE");
            celNgT_VPG.DataBindings.Add("Text", DataSource, "NgT_VP");

            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "12121")
                celKP.Text = "KHOA: CẬN LÂM SÀNG";
        }
    }
}
