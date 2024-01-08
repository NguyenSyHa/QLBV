using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_TCTT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_TCTT()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xlbTenCongTy.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xlbPhongKham.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        public void BindingData()
        {
            celMaThuoc.DataBindings.Add("Text", DataSource, "MaDV");
            celTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia");
            celSL.DataBindings.Add("Text", DataSource, "SoLuong");
        }
    }
}
