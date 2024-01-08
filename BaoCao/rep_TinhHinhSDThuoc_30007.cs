using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_TinhHinhSDThuoc_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_TinhHinhSDThuoc_30007()
        {
            InitializeComponent();
        }


        public void BindingData()
        {
            
            celNoiDung.DataBindings.Add("Text", DataSource, "TenTN");
            celTonDK.DataBindings.Add("Text", DataSource, "TonDK").FormatString = DungChung.Bien.FormatString[0];
            celNhapTK.DataBindings.Add("Text", DataSource, "NhapTK").FormatString = DungChung.Bien.FormatString[0];
            celSDTK.DataBindings.Add("Text", DataSource, "SuDungTK").FormatString = DungChung.Bien.FormatString[0];
            celTonCK.DataBindings.Add("Text", DataSource, "TonCK").FormatString = DungChung.Bien.FormatString[0];

            celSTTG2.DataBindings.Add("Text", DataSource, "STT");
            celNoiDungG2.DataBindings.Add("Text", DataSource, "NoiDung");
            celTonDKG2.DataBindings.Add("Text", DataSource, "TonDK");
            celNhapTKG2.DataBindings.Add("Text", DataSource, "NhapTK");
            celSDTKG2.DataBindings.Add("Text", DataSource, "SuDungTK");
            celTonCKG2.DataBindings.Add("Text", DataSource, "TonCK");
            GroupHeader2.GroupFields.Add(new GroupField("STT"));           

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
