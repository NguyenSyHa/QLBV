using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_ToDieuTri_30010 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_ToDieuTri_30010()
        {
            InitializeComponent();
        }

        private void rep_ToDieuTri_30010_BeforePrint(object sender, CancelEventArgs e)
        {
            celCQCQ.Text = DungChung.Bien.TenCQCQ.ToString();
            celCQ.Text = DungChung.Bien.TenCQ.ToString();
        }

        public void BindingData()
        {
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celKetQua.DataBindings.Add("Text", DataSource, "KetQua");
            celTenThuoc.DataBindings.Add("Text", DataSource, "TenThuoc");
            celSoLuong.DataBindings.Add("Text", DataSource, "SoLuongct");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
        }
    }
}
