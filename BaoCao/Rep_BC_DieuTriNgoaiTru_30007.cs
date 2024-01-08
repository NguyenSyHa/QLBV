using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_DieuTriNgoaiTru_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_DieuTriNgoaiTru_30007()
        {
            InitializeComponent();
        }

        public void Bindingdata()
        {
            cel_TenKhoa.DataBindings.Add("Text", DataSource, "TenKhoa");
            cel_TongMacBenh.DataBindings.Add("Text", DataSource, "TongMacBenh");
            cel_TongTuVong.DataBindings.Add("Text", DataSource, "TongTuVong");
            cel_TEMacBenh_T.DataBindings.Add("Text", DataSource, "TongTEMacBenh");
            cel_MacBenh_0_6.DataBindings.Add("Text", DataSource, "TongTEMacBenh_0_6");
            cel_TETuVong_T.DataBindings.Add("Text", DataSource, "TV_TongTE");
            cel_TETuVong_0_6.DataBindings.Add("Text", DataSource, "TV_TongTE_0_6");
            cel_CaoTuoi_T.DataBindings.Add("Text", DataSource, "TongNguoiCTMacBenh");
            cel_TongNgayDT.DataBindings.Add("Text", DataSource, "TongNgayDT");
            cel_TEDieuTri_T.DataBindings.Add("Text", DataSource, "TE_DieuTri_T");
            cel_TEDieuTri_0_6.DataBindings.Add("Text", DataSource, "TE_0_6_DT_T");
            cel_CaoTuoi_NgayDT.DataBindings.Add("Text", DataSource, "CaoTuoi_NgayDT");

            gr_TongMacBenh.DataBindings.Add("Text", DataSource, "TongMacBenh");
            gr_TongTuVong.DataBindings.Add("Text", DataSource, "TongTuVong");
            gr_TongNgayDT.DataBindings.Add("Text", DataSource, "TongNgayDT");
            gr_TongTreEmMacBenh.DataBindings.Add("Text", DataSource, "TongTEMacBenh");
            gr_TongTEDuoi6TMacBenh.DataBindings.Add("Text", DataSource, "TongTEMacBenh_0_6");
            gr_TongTETuVong.DataBindings.Add("Text", DataSource, "TV_TongTE");
            gr_TongTEDuoi6TTuVong.DataBindings.Add("Text", DataSource, "TV_TongTE_0_6");
            gr_TongNgayDTTE.DataBindings.Add("Text", DataSource, "TE_DieuTri_T");
            gr_TongNgayDTTEDuoi6T.DataBindings.Add("Text", DataSource, "TE_0_6_DT_T");
            gr_TongNguoiCTMacBenh.DataBindings.Add("Text", DataSource, "TongNguoiCTMacBenh");
            gr_NguoiCTTongNgayDT.DataBindings.Add("Text", DataSource, "CaoTuoi_NgayDT");
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            //celKHTH.Text = DungChung.Bien.PhoTPKHTH;
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
