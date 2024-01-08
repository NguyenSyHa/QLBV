using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repGiayCVNoiTru_HL : DevExpress.XtraReports.UI.XtraReport
    {
        public repGiayCVNoiTru_HL()
        {
            InitializeComponent();
        }

               private void colsothetu_BeforePrint(object sender, CancelEventArgs e)
        {
            //colsothetu.Text = colsothetu.Text.ToString().Substring(0, 10);
        }

        private void colsotheden_BeforePrint(object sender, CancelEventArgs e)
        {
            //colsotheden.Text = colsotheden.Text.ToString().Substring(0, 10);
        }

        private void colngaytu_BeforePrint(object sender, CancelEventArgs e)
        {
            //colngaytu.Text = colngaytu.Text.ToString().Substring(0, 10);
        }

        private void colNgayden_BeforePrint(object sender, CancelEventArgs e)
        {
           // colNgayden.Text = colNgayden.Text.ToString().Substring(0, 10);
        }

        private void colSothe_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            string sthe = SHBHYT.Value.ToString();
            if (sthe.Length == 15)
            {
                txtThe1.Text = sthe.Substring(0, 2);
                txtThe2.Text = sthe.Substring(2, 1);
                txtThe3.Text = sthe.Substring(3, 2);
                txtThe4.Text = sthe.Substring(5, 2);
                txtThe5.Text = sthe.Substring(7, 3);
                txtThe6.Text = sthe.Substring(10, 5);
            }
            txtTenBV.Text = DungChung.Bien.TenCQ + " trân trọng giới thiệu :";
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            //txtTenGD.Text = DungChung.Bien.GiamDoc;
            txtDTriTai.Text = DungChung.Bien.TenCQ;
            txtSo.Text ="/"+ System.DateTime.Now.Year + "/GCT";
            //this.txttieuDe.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            //            | DevExpress.XtraPrinting.BorderSide.Right)
            //            | DevExpress.XtraPrinting.BorderSide.Bottom)));
        }

    }
}
