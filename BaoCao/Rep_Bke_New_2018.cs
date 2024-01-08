using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_Bke_New_2018 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_Bke_New_2018()
        {
            InitializeComponent();
        }
        public void Bindingdata()
        {
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            celSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            celDonGiaBV.DataBindings.Add("Text", DataSource, "DonGiaBV").FormatString = DungChung.Bien.FormatString[1];
            celDonGiaBH.DataBindings.Add("Text", DataSource, "DonGiaBH").FormatString = DungChung.Bien.FormatString[1];
            celTyLeDV.DataBindings.Add("Text", DataSource, "TyLeTTDV");

            celThanhTienBV.DataBindings.Add("Text", DataSource, "ThanhTienBV").FormatString = DungChung.Bien.FormatString[1];
            celThanhTianBV_T.DataBindings.Add("Text", DataSource, "ThanhTienBV");
            celThanhTianBV_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTTBV_G2.DataBindings.Add("Text", DataSource, "ThanhTienBV");
            celTTBV_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTTBV_G1.DataBindings.Add("Text", DataSource, "ThanhTienBV");
            celTTBV_G1.Summary.FormatString = DungChung.Bien.FormatString[1];

            celThanhTienBH.DataBindings.Add("Text", DataSource, "ThanhTienBH").FormatString = DungChung.Bien.FormatString[1];
            celThanhTienBH_T.DataBindings.Add("Text", DataSource, "ThanhTienBH");
            celThanhTienBH_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTTBH_G.DataBindings.Add("Text", DataSource, "ThanhTienBH");
            celTTBH_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTTBH_G1.DataBindings.Add("Text", DataSource, "ThanhTienBH");
            celTTBH_G1.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTyLeBH.DataBindings.Add("Text", DataSource, "TyLeTTBH");
            celTienBH.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            celTienBH_T.DataBindings.Add("Text", DataSource, "TienBH");
            celTienBH_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBH_G.DataBindings.Add("Text", DataSource, "TienBH");
            celTienBH_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBH_G1.DataBindings.Add("Text", DataSource, "TienBH");
            celTienBH_G1.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTienBN.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            celTienBN_T.DataBindings.Add("Text", DataSource, "TienBN");
            celTienBN_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBN_G.DataBindings.Add("Text", DataSource, "TienBN");
            celTienBN_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBN_G1.DataBindings.Add("Text", DataSource, "TienBN");
            celTienBN_G1.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTiengNGDM.DataBindings.Add("Text", DataSource, "TienNgBH").FormatString = DungChung.Bien.FormatString[1];
            celTianNGDM_T.DataBindings.Add("Text", DataSource, "TienNgBH");
            celTianNGDM_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            ceTienDV_G.DataBindings.Add("Text", DataSource, "TienNgBH");
            ceTienDV_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienDV_G1.DataBindings.Add("Text", DataSource, "TienNgBH");
            celTienDV_G1.Summary.FormatString = DungChung.Bien.FormatString[1];

            coltienkhac.DataBindings.Add("Text", DataSource, "TienKhac").FormatString = DungChung.Bien.FormatString[1];
            coltienkhac_R.DataBindings.Add("Text", DataSource, "TienKhac");
            coltienkhac_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            coltienkhac_G.DataBindings.Add("Text", DataSource, "TienKhac");
            coltienkhac_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            coltienkhac_G1.DataBindings.Add("Text", DataSource, "TienKhac");
            coltienkhac_G1.Summary.FormatString = DungChung.Bien.FormatString[1];

            celNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            celTieuNhom.DataBindings.Add("Text", DataSource, "TenTieuNhom");

            GroupHeader2.GroupFields.Add(new GroupField("STTNhom"));
            GroupHeader1.GroupFields.Add(new GroupField("STTTieuN"));
        }
        int a = 1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("STTNhom") != null)
            {
                string n = GetCurrentColumnValue("STTNhom").ToString();
                if (n == "2" || n == "11")
                {
                    GroupHeader1.Visible = true;
                    celTienBH_G.Text = "";
                    celTTBV_G2.Text = "";
                    celTTBH_G.Text = "";
                    celTienBN_G.Text = "";
                    ceTienDV_G.Text = "";
                    //DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.None;
                    //this.celTienBH_G.Summary = xrSummary1;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.None;
                    //this.celTTBV_G2.Summary = xrSummary2;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.None;
                    //this.celTTBH_G.Summary = xrSummary3;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.None;
                    //this.celTienBN_G.Summary = xrSummary4;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.None;
                    //this.ceTienDV_G.Summary = xrSummary5;
                }
                else if (n == "5")
                {
                    GroupHeader1.Visible = false;
                    Detail.Visible = false;
                    celTienBH_G.Text = "";
                    celTTBV_G2.Text = "";
                    celTTBH_G.Text = "";
                    celTienBN_G.Text = "";
                    ceTienDV_G.Text = "";
                }
                else
                {
                    GroupHeader1.Visible = false;
                    Detail.Visible = true;
                    //DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
                    //this.celTienBH_G.Summary = xrSummary1;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
                    //this.celTTBV_G2.Summary = xrSummary2;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
                    //this.celTTBH_G.Summary = xrSummary3;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
                    //this.celTienBN_G.Summary = xrSummary4;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
                    //this.ceTienDV_G.Summary = xrSummary5;
                }

            }
            //sttNhom.Text = a.ToString() + ". ";
            //a++;
            //b = 1;
        }
        int b = 1;
        private void stttn_BeforePrint(object sender, CancelEventArgs e)
        {
            //stttn.Text = a.ToString() + "." + b.ToString() + ". ";
            //b++;
        }
    }
}
