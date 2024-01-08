using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_TracNghiemTamLy : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_TracNghiemTamLy()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        public void BindingData()
        {
            celPP.DataBindings.Add("Text", DataSource, "TenTN");
            lblChon.DataBindings.Add("Text", DataSource, "Chon");
            celTenTracNghiem.DataBindings.Add("Text", DataSource, "TenDVct");
            celDTuong.DataBindings.Add("Text", DataSource, "TSBT");

            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
        }
    }
}
