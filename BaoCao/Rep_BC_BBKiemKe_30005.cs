using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_BBKiemKe_30005 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_BBKiemKe_30005()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            grp_Tieunhom.DataBindings.Add("Text", DataSource, "TenTN");
            cel_TenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            cel_DV.DataBindings.Add("Text", DataSource, "DonVi");
            cel_DonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            cel_TonKhoDK.DataBindings.Add("Text", DataSource, "TonDK").FormatString = DungChung.Bien.FormatString[1];
            cel_NhapKho.DataBindings.Add("Text", DataSource, "NhapKho").FormatString = DungChung.Bien.FormatString[1];
            cel_Tra.DataBindings.Add("Text", DataSource, "NhapTraLai").FormatString = DungChung.Bien.FormatString[1];
            cel_TongCong.DataBindings.Add("Text", DataSource, "TongNhap").FormatString = DungChung.Bien.FormatString[1];
            cel_HongVo.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[1];
            cel_ND.DataBindings.Add("Text", DataSource, "XuatNhanDan").FormatString = DungChung.Bien.FormatString[1];
            cel_BaoHiem.DataBindings.Add("Text", DataSource, "XuatBH").FormatString = DungChung.Bien.FormatString[1];
            cel_TrangBi.DataBindings.Add("Text", DataSource, "XuatKhac").FormatString = DungChung.Bien.FormatString[1];
            cel_Mien.DataBindings.Add("Text", DataSource, "XuatMien").FormatString = DungChung.Bien.FormatString[1];
            cel_TongXuat.DataBindings.Add("Text", DataSource, "TongXuat").FormatString = DungChung.Bien.FormatString[1];
            cel_TonCuoiKy.DataBindings.Add("Text", DataSource, "TonCuoi").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            cel_NguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            cel_ThuKho.Text = DungChung.Bien.ThuKho;
            cel_KeToan.Text = DungChung.Bien.KeToanTruong;
            cel_TruongKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            cel_GiamDoc.Text = DungChung.Bien.GiamDoc;
        }
    }
}
