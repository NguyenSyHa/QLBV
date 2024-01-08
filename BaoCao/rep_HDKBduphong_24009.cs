using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_HDKBduphong_24009 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_HDKBduphong_24009()
        {
            InitializeComponent();
        }
        public void BindingData() 
        {
            row_Tongso.DataBindings.Add("Text", DataSource, "TongSo");
            cel_STTNhom.DataBindings.Add("Text", DataSource, "STTNhom");
            celTenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            lblSTT.DataBindings.Add("Text", DataSource, "Tuyen");
            cel_CSYT.DataBindings.Add("Text", DataSource, "Cosoyte");
            cel_TSluotkhamduphong.DataBindings.Add("Text", DataSource, "TSluotkhamduphong");
            cel_TStuvong.DataBindings.Add("Text", DataSource, "TStuvong");
            cel_TS_1tuoi.DataBindings.Add("Text", DataSource, "TS_1tuoi");
            cel_nu_1tuoi.DataBindings.Add("Text", DataSource, "Nu_1tuoi");
            cel_DTocitnguoi_1tuoi.DataBindings.Add("Text", DataSource, "DTocitnguoi_1tuoi");
            cel_TS_5tuoi.DataBindings.Add("Text", DataSource, "TS_5tuoi");
            cel_nu_5tuoi.DataBindings.Add("Text", DataSource, "Nu_5tuoi");
            cel_DTocitnguoi_5tuoi.DataBindings.Add("Text", DataSource, "DTocitnguoi_5tuoi");
            cel_xetnghiem.DataBindings.Add("Text", DataSource, "Xetnghiem");
            cel_sieuam.DataBindings.Add("Text", DataSource, "Sieuam");
            cel_Xquang.DataBindings.Add("Text", DataSource, "Xquang");
            cel_chupCT.DataBindings.Add("Text", DataSource, "ChupCT");

            cel_TSluotkhamduphong_T.DataBindings.Add("Text", DataSource, "TSluotkhamduphong");
            cel_TStuvong_T.DataBindings.Add("Text", DataSource, "TStuvong");
            cel_TS_1tuoi_T.DataBindings.Add("Text", DataSource, "TS_1tuoi");
            cel_nu_1tuoi_T.DataBindings.Add("Text", DataSource, "Nu_1tuoi");
            cel_DTocitnguoi_1tuoi_T.DataBindings.Add("Text", DataSource, "DTocitnguoi_1tuoi");
            cel_TS_5tuoi_T.DataBindings.Add("Text", DataSource, "TS_5tuoi");
            cel_nu_5tuoi_T.DataBindings.Add("Text", DataSource, "Nu_5tuoi");
            cel_DTocitnguoi_5tuoi_T.DataBindings.Add("Text", DataSource, "DTocitnguoi_5tuoi");
            cel_xetnghiem_T.DataBindings.Add("Text", DataSource, "Xetnghiem");
            cel_sieuam_T.DataBindings.Add("Text", DataSource, "Sieuam");
            cel_Xquang_T.DataBindings.Add("Text", DataSource, "Xquang");
            cel_chupCT_T.DataBindings.Add("Text", DataSource, "ChupCT");
            GroupHeader1.GroupFields.Add(new GroupField("Tuyen"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lab_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            cel_NLB.Text = DungChung.Bien.NguoiLapBieu;
            cel_TTDV.Text = DungChung.Bien.GiamDoc;

        }
    }
}
