using System;using QLBV_Database;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuTruyenMau : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuTruyenMau()
        {
            InitializeComponent();
        }
        bool hiennhom = true;
        bool HTTrongNgoaiNuoc = false;
        string[] arr;
        public rep_PhieuTruyenMau(bool a, bool nk, string[] ar)
        {
            InitializeComponent();
            hiennhom = a;
            HTTrongNgoaiNuoc = nk;
            arr = ar;
            
        }
        public void BindingData()
        {
            ThoiGian.DataBindings.Add("Text", DataSource, "ThoiGian");
            TocDoTruyen.DataBindings.Add("Text", DataSource, "TocDoTruyen");
            MauSac.DataBindings.Add("Text", DataSource, "MauSac_NiemMac");
            NhipTho.DataBindings.Add("Text", DataSource, "NhipTho");
            Mach.DataBindings.Add("Text", DataSource, "Mach");
            HuyetAp.DataBindings.Add("Text", DataSource, "NhietDo");
            ThanNhiet.DataBindings.Add("Text", DataSource, "HuyetAp");
            DienBienKhac.DataBindings.Add("Text", DataSource, "GhiChu");
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
  
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel1.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel2.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
