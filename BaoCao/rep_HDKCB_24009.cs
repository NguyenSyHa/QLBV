using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_HDKCB_24009 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_HDKCB_24009()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
           // Row_Tongso.DataBindings.Add("Text", DataSource, "Tongso");
            cel_STTNhom.DataBindings.Add("Text", DataSource, "STTNhom");
            celTenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            cel_CSYT.DataBindings.Add("Text", DataSource, "CSYT");
            cel_TongsoKCB.DataBindings.Add("Text", DataSource, "TongsoKCB");
            cel_nu_kcb.DataBindings.Add("Text", DataSource, "nu_kcb");
            cel_BHYT_kcb.DataBindings.Add("Text", DataSource, "BHYT_kcb");
            cel_YHCT_kcb.DataBindings.Add("Text", DataSource, "YHCT_kcb");
            cel_TE_kcb.DataBindings.Add("Text", DataSource, "TE_kcb");
            cel_Tongso_dtri.DataBindings.Add("Text", DataSource, "Tongso_dtri");
            cel_Nu_dtri.DataBindings.Add("Text", DataSource, "Nu_dtri");
            cel_BHYT_dtri.DataBindings.Add("Text", DataSource, "BHYT_dtri");
            cel_YHCT_dtri.DataBindings.Add("Text", DataSource, "YHCT_dtri");
            cel_TE_dtri.DataBindings.Add("Text", DataSource, "TE_dtri");
            cel_TSngaydtrinoitru.DataBindings.Add("Text", DataSource, "TSngaydtrinoitru");

            cel_TongsoKCB_tong.DataBindings.Add("Text", DataSource, "TongsoKCB");
            cel_nu_kcb_tong.DataBindings.Add("Text", DataSource, "nu_kcb");
            cel_BHYT_kcb_tong.DataBindings.Add("Text", DataSource, "BHYT_kcb");
            cel_YHCT_kcb_tong.DataBindings.Add("Text", DataSource, "YHCT_kcb");
            cel_TE_kcb_tong.DataBindings.Add("Text", DataSource, "TE_kcb");
            cel_Tongso_dtri_Tong.DataBindings.Add("Text", DataSource, "Tongso_dtri");
            cel_Nu_dtri_tong.DataBindings.Add("Text", DataSource, "Nu_dtri");
            cel_BHYT_dtri_tong.DataBindings.Add("Text", DataSource, "BHYT_dtri");
            cel_YHCT_dtri_tong.DataBindings.Add("Text", DataSource, "YHCT_dtri");
            cel_TE_dtri_tong.DataBindings.Add("Text", DataSource, "TE_dtri");
            cel_TSngaydtrinoitru_tong.DataBindings.Add("Text", DataSource, "TSngaydtrinoitru");
            GroupHeader1.GroupFields.Add(new GroupField("TenNhom"));
           // GroupHeader2.GroupFields.Add(new GroupField("TenNhom"));


        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            lab_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            cel_NLB.Text = DungChung.Bien.NguoiLapBieu;
            cel_TTDV.Text = DungChung.Bien.GiamDoc;
        }
    }
}