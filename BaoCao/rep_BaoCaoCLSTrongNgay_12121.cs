using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BaoCaoCLSTrongNgay_12121 : DevExpress.XtraReports.UI.XtraReport
    {
        private bool HTTieuNhom;
        private bool HTDichVu;

        public rep_BaoCaoCLSTrongNgay_12121()
        {
            InitializeComponent();
        }

        public rep_BaoCaoCLSTrongNgay_12121(bool HTTieuNhom, bool HTDichVu)
        {

            this.HTTieuNhom = HTTieuNhom;
            this.HTDichVu = HTDichVu;
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            cel_diadanh.Text = "Ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " +  DateTime.Now.Year;
            cel_NguoiLap.Text = DungChung.Bien.NguoiLapBieu;
        }


        internal void BindingData()
        {
            celTenTN.DataBindings.Add("Text", DataSource, "TenTN");
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            cel1.DataBindings.Add("Text", DataSource, "SoLuong1");
            cel2.DataBindings.Add("Text", DataSource, "SoLuong2");
            cel3.DataBindings.Add("Text", DataSource, "SoLuong3");
            cel4.DataBindings.Add("Text", DataSource, "SoLuong4");
            cel5.DataBindings.Add("Text", DataSource, "SoLuong5");
            cel6.DataBindings.Add("Text", DataSource, "SoLuong6");
            cel7.DataBindings.Add("Text", DataSource, "SoLuong7");
            cel8.DataBindings.Add("Text", DataSource, "SoLuong8");
            celCong.DataBindings.Add("Text", DataSource, "Cong");

            cel1G.DataBindings.Add("Text", DataSource, "SoLuong1");
            cel2G.DataBindings.Add("Text", DataSource, "SoLuong2");
            cel3G.DataBindings.Add("Text", DataSource, "SoLuong3");
            cel4G.DataBindings.Add("Text", DataSource, "SoLuong4");
            cel5G.DataBindings.Add("Text", DataSource, "SoLuong5");
            cel6G.DataBindings.Add("Text", DataSource, "SoLuong6");
            cel7G.DataBindings.Add("Text", DataSource, "SoLuong7");
            cel8G.DataBindings.Add("Text", DataSource, "SoLuong8");
            celCongG.DataBindings.Add("Text", DataSource, "Cong");
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {           
                GroupHeader1.Visible = HTTieuNhom;

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            Detail.Visible = HTDichVu;
        }
    }
}
