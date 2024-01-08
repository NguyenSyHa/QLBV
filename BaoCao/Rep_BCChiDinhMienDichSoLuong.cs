using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCChiDinhMienDichSoLuong : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCChiDinhMienDichSoLuong()
        {
            InitializeComponent();
        }
        internal void BindingData()
        {
            celTenDV.DataBindings.Add("Text", DataSource, "ten");
            celDV.DataBindings.Add("Text", DataSource, "SoLuongDV").FormatString = DungChung.Bien.FormatString[0];
            celBH.DataBindings.Add("Text", DataSource, "SoLuongBH").FormatString = DungChung.Bien.FormatString[0];

            celDV_R.DataBindings.Add("Text", DataSource, "SoLuongDV");
            celDV_R.Summary.FormatString = DungChung.Bien.FormatString[0];

            celBH_R.DataBindings.Add("Text", DataSource, "SoLuongBH");
            celBH_R.Summary.FormatString = DungChung.Bien.FormatString[0];
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

    }
}
