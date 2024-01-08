using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_HDBH_08204 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_HDBH_08204()
        {
            InitializeComponent();
        }
        public void Bindingdata()
        {
            cel_NMH.DataBindings.Add("Text", DataSource, "TenBNhan");
            cel_Diachi_MH.DataBindings.Add("Text", DataSource, "DChi");
            cel_TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            cel_DVtinh.DataBindings.Add("Text", DataSource, "DonVi");
            cel_SL.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1]; ;
            cel_Dongia.DataBindings.Add("Text", DataSource, "Dongia");
            cel_Thanhtien.DataBindings.Add("Text", DataSource, "Thanhtien").FormatString = DungChung.Bien.FormatString[1]; ;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //lab_MST_BH.Text = DungChung.Bien.MST;
            lab_diachi.Text = DungChung.Bien.DiaChi;
            lab_TenDVBH.Text = DungChung.Bien.TenCQ;
            cel_NgBH.Text = DungChung.Bien.NguoiLapBieu;
        }

    }
}
