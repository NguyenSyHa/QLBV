using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_SoChungSinh : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_SoChungSinh()
        {
            InitializeComponent();
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrTableCell1.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrTableCell2.Text = DungChung.Bien.TenCQ.ToUpper();
        }


        internal void BindingData()
        {
            celTenMe.DataBindings.Add("Text", DataSource, "TenBNhan");
            celNamSinh.DataBindings.Add("Text", DataSource, "NamSinh");
            celNoiDKTT.DataBindings.Add("Text", DataSource, "HoKhau");
            celSoThe.DataBindings.Add("Text", DataSource, "SThe");
            celSoCMT.DataBindings.Add("Text", DataSource, "SoCMT");
            celNgayCap.DataBindings.Add("Text", DataSource, "NgayCap");
            celNoiCap.DataBindings.Add("Text", DataSource, "NoiCap");
            celThoiGianSinh.DataBindings.Add("Text", DataSource, "ThoiGianSinh");
            celTenCha.DataBindings.Add("Text", DataSource, "TenBo");
            celTenCon.DataBindings.Add("Text", DataSource, "TenCon");
            celGioiTinh.DataBindings.Add("Text", DataSource, "GTinh");
            celCanNang.DataBindings.Add("Text", DataSource, "CNang");
            celGhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
           
        }
    }
}
