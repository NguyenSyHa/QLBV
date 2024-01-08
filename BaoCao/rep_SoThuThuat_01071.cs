using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_SoThuThuat_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        int n = 1;
        public rep_SoThuThuat_01071()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            nam.DataBindings.Add("Text", DataSource, "Nam");
            nu.DataBindings.Add("Text", DataSource, "Nu");
            bhyt.DataBindings.Add("Text", DataSource, "BHYT");
            SoBA.DataBindings.Add("Text", DataSource, "SoBA");
            HoTen.DataBindings.Add("Text", DataSource, "HoTen");
            chandoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            Ngay01.DataBindings.Add("Text", DataSource, "Sl1");
            Ngay02.DataBindings.Add("Text", DataSource, "Sl2");
            Ngay03.DataBindings.Add("Text", DataSource, "Sl3");
            Ngay04.DataBindings.Add("Text", DataSource, "Sl4");
            Ngay05.DataBindings.Add("Text", DataSource, "Sl5");
            Ngay06.DataBindings.Add("Text", DataSource, "Sl6");
            Ngay07.DataBindings.Add("Text", DataSource, "Sl7");
            Ngay08.DataBindings.Add("Text", DataSource, "Sl8");
            Ngay09.DataBindings.Add("Text", DataSource, "Sl9");
            Ngay010.DataBindings.Add("Text", DataSource, "Sl10");
            Ngay011.DataBindings.Add("Text", DataSource, "Sl11");
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcqcq.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtcq.Text = "BỆNH VIỆN: " + DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
