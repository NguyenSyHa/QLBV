using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_SoTheoDoiTTBN : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_SoTheoDoiTTBN()
        {
            InitializeComponent();
        }

        internal void BindingData()
        {
            celNgayXuat.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0: dd/MM/yyyy}";
            ngayXuat.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0: dd/MM/yyyy}";
            celTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            tenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            diaChi.DataBindings.Add("Text", DataSource, "DChi");
            celGTinh_Tuoi.DataBindings.Add("Text", DataSource, "GTinh_Tuoi");
            celChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            celBSKe.DataBindings.Add("Text", DataSource, "TenCB");
            celThuoc_SoLuong.DataBindings.Add("Text", DataSource, "Thuoc_SoLuong");
            tenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            SL.DataBindings.Add("Text", DataSource, "SoLuong");
            elCachDung.DataBindings.Add("Text", DataSource, "CachDung");
            celGhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
            GC.DataBindings.Add("Text", DataSource, "GhiChu");
            HC.DataBindings.Add("Text", DataSource, "TenHC");
            DVT.DataBindings.Add("Text", DataSource, "DonVi");


        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV != "27022")
            {
                xrLabel2.Visible = false;
            }
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022")
                xrTable1.Visible = false;
            else
                xrTable3.Visible = false;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022")
                xrTable2.Visible = false;
            else
                xrTable4.Visible = false;
        }
    }
}
