using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repGiayCVNoiTru_TT146 : DevExpress.XtraReports.UI.XtraReport
    {
        public repGiayCVNoiTru_TT146()
        {
            InitializeComponent();
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
                //txtThe4.Text = sthe.Substring(5, 2);
                //txtThe5.Text = sthe.Substring(7, 3);
                txtThe6.Text = sthe.Substring(5, 10);
            }
            txtTenBV.Text = DungChung.Bien.TenCQ ;
            if (DungChung.Bien.MaBV != "20001")
            {
                txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            }
            //txtTenGD.Text = DungChung.Bien.GiamDoc;
            txtDTriTai.Text = DungChung.Bien.TenCQ;
            txtSo.Text ="/"+ System.DateTime.Now.Year + "/GCT";
            if (DungChung.Bien.MaBV.Equals("12001"))
            {
                SubBand3.Visible = true;
                SubBand5.Visible = true ;
            }
            else
            {
                SubBand3.Visible = false;
                SubBand5.Visible = false;
            }
            if (DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "20001")
                xrLabel60.Visible = false;

            if (DungChung.Bien.MaBV == "30003")
            {
                xrLabel7.Visible = false;
                xrLabel8.Visible = false;
                xrLabel5.Visible = false;
            }
        }


    }
}
