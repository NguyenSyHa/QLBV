using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcTonDuoc_BG : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcTonDuoc_BG()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colNhomDVG1.DataBindings.Add("Text", DataSource, "NhomDV");

            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            ceDonGia.DataBindings.Add("Text", DataSource, "DonGia");
            colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colKP1.DataBindings.Add("Text", DataSource, "SL1").FormatString = DungChung.Bien.FormatString[1];
            colKP2.DataBindings.Add("Text", DataSource, "SL2").FormatString = DungChung.Bien.FormatString[1];
            colKP3.DataBindings.Add("Text", DataSource, "SL3").FormatString = DungChung.Bien.FormatString[1];
            colKP4.DataBindings.Add("Text", DataSource, "SL4").FormatString = DungChung.Bien.FormatString[1];
            colKP5.DataBindings.Add("Text", DataSource, "SL5").FormatString = DungChung.Bien.FormatString[1];
            colKP6.DataBindings.Add("Text", DataSource, "SL6").FormatString = DungChung.Bien.FormatString[1];
            colKP7.DataBindings.Add("Text", DataSource, "SL7").FormatString = DungChung.Bien.FormatString[1];
            colKP8.DataBindings.Add("Text", DataSource, "SL8").FormatString = DungChung.Bien.FormatString[1];
            colKP9.DataBindings.Add("Text", DataSource, "SL9").FormatString = DungChung.Bien.FormatString[1];
            colKP10.DataBindings.Add("Text", DataSource, "SL10").FormatString = DungChung.Bien.FormatString[1];
            colKP11.DataBindings.Add("Text", DataSource, "Sl11").FormatString = DungChung.Bien.FormatString[1];
            colKP12.DataBindings.Add("Text", DataSource, "SL12").FormatString = DungChung.Bien.FormatString[1];
            colKP13.DataBindings.Add("Text", DataSource, "SL13").FormatString = DungChung.Bien.FormatString[1];
            colKP14.DataBindings.Add("Text", DataSource, "SL14").FormatString = DungChung.Bien.FormatString[1];
            colKP15.DataBindings.Add("Text", DataSource, "SL15").FormatString = DungChung.Bien.FormatString[1];
            colKP16.DataBindings.Add("Text", DataSource, "SL16").FormatString = DungChung.Bien.FormatString[1];
            colKP17.DataBindings.Add("Text", DataSource, "SL17").FormatString = DungChung.Bien.FormatString[1];
            colKP18.DataBindings.Add("Text", DataSource, "SL18").FormatString = DungChung.Bien.FormatString[1];
            colKP19.DataBindings.Add("Text", DataSource, "SL19").FormatString = DungChung.Bien.FormatString[1];
            colKPTong.DataBindings.Add("Text", DataSource, "TC").FormatString = DungChung.Bien.FormatString[1];
            colKPTongTT.DataBindings.Add("Text", DataSource, "TCTT").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("NhomDV"));
            tongtt.DataBindings.Add("Text", DataSource, "TCTT").FormatString = DungChung.Bien.FormatString[1];
        }
         
   
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            string nh = NhomDV.Value.ToString();
            if (nh != "0")
            {
                GroupHeader1.Visible = false;
            }
        }
    }
}
