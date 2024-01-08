using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_XuatDuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_XuatDuoc()
        {
            InitializeComponent();
        }

        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell20.Text = "Phó trưởng khoa phòng khám_Dược_CLS";
            }
            celTruongKhoa.Text = DungChung.Bien.TruongKhoaDuoc;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            colThuKho.Text = DungChung.Bien.ThuKho;
        }

        public void BindingData()
        {
            gr_TenNhom.DataBindings.Add("Text", DataSource, "TenTN");
            cel_TenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            cel_DVi.DataBindings.Add("Text", DataSource, "DonVi");
            cel_DonGia.DataBindings.Add("Text", DataSource, "DonGia");
            cel_NoiTru_BHYT_SL.DataBindings.Add("Text", DataSource, "XuatNT_BHYT_SL").FormatString = DungChung.Bien.FormatString[1];
            cel_NoiTru_BHYT_TT.DataBindings.Add("Text", DataSource, "XuatNT_BHYT_TT").FormatString = DungChung.Bien.FormatString[1];
            cel_NoiTru_DV_SL.DataBindings.Add("Text", DataSource, "XuatNT_DV_SL").FormatString = DungChung.Bien.FormatString[1];
            cel_NoiTru_DV_TT.DataBindings.Add("Text", DataSource, "XuatNT_DV_TT").FormatString = DungChung.Bien.FormatString[1];
            cel_NgoaiTru_BHYT_SL.DataBindings.Add("Text", DataSource, "XuatNgoaiTru_BHYT_SL").FormatString = DungChung.Bien.FormatString[1];
            cel_NgoaiTru_BHYT_TT.DataBindings.Add("Text", DataSource, "XuatNgoaiTru_BHYT_TT").FormatString = DungChung.Bien.FormatString[1];
            cel_Ngoaitru_DV_SL.DataBindings.Add("Text", DataSource, "XuatNgoaiTru_DV_SL").FormatString = DungChung.Bien.FormatString[1];
            cel_NgoaiTru_DV_TT.DataBindings.Add("Text", DataSource, "XuatNgoaiTru_DV_TT").FormatString = DungChung.Bien.FormatString[1];
            cel_XuatNhuong_SL.DataBindings.Add("Text", DataSource, "XuatNhuong_SL").FormatString = DungChung.Bien.FormatString[1];
            cel_XuatNhuong_TT.DataBindings.Add("Text", DataSource, "XuatNhuong_TT").FormatString = DungChung.Bien.FormatString[1];
            cel_XuatKN_SL.DataBindings.Add("Text", DataSource, "XuatKiemNghiem_SL").FormatString = DungChung.Bien.FormatString[1];
            cel_XuatKN_TT.DataBindings.Add("Text", DataSource, "XuatKiemNghiem_TT").FormatString = DungChung.Bien.FormatString[1];
            cel_XuatXa_SL.DataBindings.Add("Text", DataSource, "XuatXa_SL").FormatString = DungChung.Bien.FormatString[1];
            cel_XuatXa_TT.DataBindings.Add("Text", DataSource, "XuatXa_TT").FormatString = DungChung.Bien.FormatString[1];
            cel_Hong_SL.DataBindings.Add("Text", DataSource, "HongVo_SL").FormatString = DungChung.Bien.FormatString[1];
            cel_Hong_TT.DataBindings.Add("Text", DataSource, "HongVo_TT").FormatString = DungChung.Bien.FormatString[1];

            gr_Tong_NoiTru_BHYT_SL.DataBindings.Add("Text", DataSource, "XuatNT_BHYT_SL");
            gr_Tong_NoiTru_BHYT_TT.DataBindings.Add("Text", DataSource, "XuatNT_BHYT_TT");
            gr_Tong_NoiTru_DV_SL.DataBindings.Add("Text", DataSource, "XuatNT_DV_SL");
            gr_Tong_NoiTru_DV_TT.DataBindings.Add("Text", DataSource, "XuatNT_DV_TT");
            gr_Tong_NgoaiTru_BHYT_SL.DataBindings.Add("Text", DataSource, "XuatNgoaiTru_BHYT_SL");
            gr_Tong_NgoaiTru_BHYT_TT.DataBindings.Add("Text", DataSource, "XuatNgoaiTru_BHYT_TT");
            gr_Tong_NgoaiTru_DV_SL.DataBindings.Add("Text", DataSource, "XuatNgoaiTru_DV_SL");
            gr_Tong_NgoaiTru_DV_TT.DataBindings.Add("Text", DataSource, "XuatNgoaiTru_DV_TT");
            gr_Tong_XuatNhuong_SL.DataBindings.Add("Text", DataSource, "XuatNhuong_SL");
            gr_Tong_XuatNhuong_TT.DataBindings.Add("Text", DataSource, "XuatNhuong_TT");
            gr_Tong_XuatKN_SL.DataBindings.Add("Text", DataSource, "XuatKiemNghiem_SL");
            gr_Tong_XuatKN_TT.DataBindings.Add("Text", DataSource, "XuatKiemNghiem_TT");
            gr_Tong_XuatXa_SL.DataBindings.Add("Text", DataSource, "XuatXa_SL");
            gr_Tong_XuatXa_TT.DataBindings.Add("Text", DataSource, "XuatXa_TT");
            gr_Tong_Hong_SL.DataBindings.Add("Text", DataSource, "HongVo_SL");
            gr_Tong_Hong_TT.DataBindings.Add("Text", DataSource, "HongVo_TT");

            gr_Header.GroupFields.Add(new GroupField("TenTN"));
        }
    }
}
