using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_SuDungThuocCuaCacXa_30009 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_SuDungThuocCuaCacXa_30009()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lbl_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            lbl_MaCSYT.Text = "Mã CSYT: " + DungChung.Bien.MaBV;
        }

        public void BindingData()
        {
            gr_TenTYT.DataBindings.Add("Text", DataSource, "TenTYT");
            gr_TenTN.DataBindings.Add("Text", DataSource, "TenTN");
            cel_STT.DataBindings.Add("Text", DataSource, "SoQD");
            cel_STTTheoDM.DataBindings.Add("Text", DataSource, "SoTTqd");
            cel_TenHC.DataBindings.Add("Text", DataSource, "TenHC");
            cel_TenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            cel_DuongD.DataBindings.Add("Text", DataSource, "DuongD");
            cel_HamLuong.DataBindings.Add("Text", DataSource, "HamLuong");
            cel_SDK.DataBindings.Add("Text", DataSource, "SoDK");
            cel_DV.DataBindings.Add("Text", DataSource, "DonVi");
            cel_SoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            cel_DonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            cel_ThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            cel_TongCong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader2.GroupFields.Add(new GroupField("TenTYT"));
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            col_NLB.Text = DungChung.Bien.NguoiLapBieu;
            col_TruongKhoaD.Text = DungChung.Bien.TruongKhoaDuoc;
            col_GiamDoc.Text = DungChung.Bien.GiamDoc;
        }
    }
}
