using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCNXT_30005 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCNXT_30005()
        {
            InitializeComponent();
        }
        public void Bindingdata()
        {
            grp_Tieunhom.DataBindings.Add("Text", DataSource, "TenTN");
            cel_Tenthuoc.DataBindings.Add("Text", DataSource, "TenDV");
            cel_DVT.DataBindings.Add("Text", DataSource, "DonVi");
            cel_DonGia.DataBindings.Add("Text", DataSource, "DonGia");
            cel_TonDKy.DataBindings.Add("Text", DataSource, "TonDK");
            cel_Nhapkho.DataBindings.Add("Text", DataSource, "NhapKho");
            cel_Tralai.DataBindings.Add("Text", DataSource, "NhapTraLai");
            cel_TongNhap.DataBindings.Add("Text", DataSource, "TongNhap");
            cel_Hongvo.DataBindings.Add("Text", DataSource, "HongVo");
            cel_XuatND.DataBindings.Add("Text", DataSource, "XuatNhanDan");
            cel_XuatBH.DataBindings.Add("Text", DataSource, "XuatBH");           
            cel_Xuatmien.DataBindings.Add("Text", DataSource, "XuatMien");
            cel_Tongxuat.DataBindings.Add("Text", DataSource, "TongXuat");
            cel_TonCK.DataBindings.Add("Text", DataSource, "TonCuoi");
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
           
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celTruongKhoa.Text = DungChung.Bien.TruongKhoaDuoc;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            colThuKho.Text = DungChung.Bien.ThuKho;
        }
    }
}
