using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_SoBanThuocKhongTheoDon : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_SoBanThuocKhongTheoDon()
        {
            InitializeComponent();
        }

        internal void BindingData()
        {
            celNgayXuat.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0: dd/MM/yyyy}";
            celTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            tenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            diaChi.DataBindings.Add("Text", DataSource, "DChi");
            celTrieuChung.DataBindings.Add("Text", DataSource, "ChanDoan");
            celTenthuoc.DataBindings.Add("Text", DataSource, "TenThuoc");
            tenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            celSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            sl.DataBindings.Add("Text", DataSource, "SoLuong");
            celCachDung.DataBindings.Add("Text", DataSource, "CachDung");
            celGhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
            ghiChu.DataBindings.Add("Text", DataSource, "GhiChu");
            dvt.DataBindings.Add("Text", DataSource, "DonVi");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTable1.Visible = false;
            }
            else
            {
                xrTable3.Visible = false;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTable2.Visible = false;
            }
            else
            {
                xrTable4.Visible = false;
            }
        }
    }
}
