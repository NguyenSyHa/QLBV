using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuTHX_Quang : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuTHX_Quang()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "27001")
            {
                SubBand2.Visible = false;
            }
        }
        public void BindingData()
        {
            colTenTN.DataBindings.Add("Text", DataSource, "TenTN");
            colYlenh.DataBindings.Add("Text", DataSource, "ChiDinh1");
            colTenchidinh.DataBindings.Add("Text", DataSource, "ylenh");
            if (DungChung.Bien.MaBV == "24012")
            {
                colYlenh.Visible = false;
                SubBand2.Visible = false;
                colTenchidinh.DataBindings.Add("Text", DataSource, "ylenh");
            }
            
            if (DungChung.Bien.MaBV == "24272")
            {
                colTenchidinh.DataBindings.Add("Text", DataSource, "TenRG");
            }
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenBSDT.Text = TenBS.Value.ToString();
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                //colTenTKXN.Visible = false;
            }
            if(DungChung.Bien.MaBV =="14018")
            {
                SubBand1.Visible = true;
            }
            if(DungChung.Bien.MaBV=="14017")
            {
                chidan.Visible = true;
            }
            else { chidan.Visible = false; }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV=="14017")
            {
                SubBand2.Visible = false;
                colYlenh.Visible = false;
            }
        }

    }
}
