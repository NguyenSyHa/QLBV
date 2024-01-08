using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_XNNuoiCayViKhuanLao : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_XNNuoiCayViKhuanLao()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            cel_SoXN.DataBindings.Add("Text", DataSource, "SoXN");
            cel_NgayLayMau.DataBindings.Add("Text", DataSource, "NgayLayMau");
            cel_NgayNhanMau.DataBindings.Add("Text", DataSource, "NgayNhanMau");
            cel_NgayCay.DataBindings.Add("Text", DataSource, "NgayCay");
            cel_HoTen.DataBindings.Add("Text", DataSource, "HoTenBN");
            cel_Nam.DataBindings.Add("Text", DataSource, "TuoiNam");
            cel_Nu.DataBindings.Add("Text", DataSource, "TuoiNu");
            cel_DiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            cel_DonViYC.DataBindings.Add("Text", DataSource, "DViYeuCau");
            cel_BP_Dom.DataBindings.Add("Text", DataSource, "BPDom");
            cel_BP_Khac.DataBindings.Add("Text", DataSource, "BPKhac");
            cel_TrangThaiBP.DataBindings.Add("Text", DataSource, "TrangThaiBP");
            cel_LyDoPhatHien.DataBindings.Add("Text", DataSource, "LyDoPhatHien");
            cel_LyDo_TheoDoi.DataBindings.Add("Text", DataSource, "TheoDoiThangThu");
            cel_KQSoi.DataBindings.Add("Text", DataSource, "KQSoi");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblDonVi.Text = "ĐƠN VỊ: " + DungChung.Bien.TenCQ.ToUpper();
            lblNam.Text = "Năm: " + DateTime.Now.Year;
        }
    }
}
