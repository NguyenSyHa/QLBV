using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_PTDChatLuongThuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PTDChatLuongThuoc()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcqcq.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtcq.Text = DungChung.Bien.TenCQ.ToUpper();
        }
        public void BindingData()
        {
            NgayNhap.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0:dd/MM/yyyy}";
            NgayNhap1.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0:dd/MM/yyyy}";
            SoCT.DataBindings.Add("Text", DataSource, "SoCT");
            SoLo.DataBindings.Add("Text", DataSource, "SoLo");
            NhaSX.DataBindings.Add("Text", DataSource, "NhaSX");
            SoLuong.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
            HanDung.DataBindings.Add("Text", DataSource, "HanDung").FormatString = "{0:dd/MM/yyyy}";
        }
    }
}
