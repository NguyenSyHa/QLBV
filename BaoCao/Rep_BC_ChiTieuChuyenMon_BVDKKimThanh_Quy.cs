using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_ChiTieuChuyenMon_BVDKKimThanh_Quy : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_ChiTieuChuyenMon_BVDKKimThanh_Quy()
        {
            InitializeComponent();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            cel_GiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("ChiTietDanhMuc") != null)
            {
                string nt = this.GetCurrentColumnValue("ChiTietDanhMuc").ToString();
                if (nt == "")
                    xrTableRow6.Visible = false;
                else
                    xrTableRow6.Visible = true;
            }
        }

        public void BindingData()
        {
            gr_STT.DataBindings.Add("Text", DataSource, "Stt");
            gr_TenDanhMuc.DataBindings.Add("Text", DataSource, "DanhMuc");
            gr_DVTinh.DataBindings.Add("Text", DataSource, "DVTinh");
            gr_ChiTieuCaQuy.DataBindings.Add("Text", DataSource, "ChiTieuCaQuy");
            gr_ThucHienCaQuy.DataBindings.Add("Text", DataSource, "ThucHienCaQuy");
            gr_PtramCaQuy.DataBindings.Add("Text", DataSource, "PtramCaQuy");
            gr_Thang1ThucHien.DataBindings.Add("Text", DataSource, "ThucHienThang1");
            gr_Thang1Ptram.DataBindings.Add("Text", DataSource, "PtramThang1");
            gr_Thang2ThucHien.DataBindings.Add("Text", DataSource, "ThucHienThang2");
            gr_Thang2Ptram.DataBindings.Add("Text", DataSource, "PtramThang2");
            gr_Thang3ThucHien.DataBindings.Add("Text", DataSource, "ThucHienThang3");
            gr_Thang3Ptram.DataBindings.Add("Text", DataSource, "PtramThang3");

            cel_ChiTietDM.DataBindings.Add("Text", DataSource, "ChiTietDanhMuc");
            cel_DVTinh.DataBindings.Add("Text", DataSource, "DVTinh");
            cel_ChiTieuCaQuy.DataBindings.Add("Text", DataSource, "ChiTieuCaQuy");
            cel_ThucHienCaQuy.DataBindings.Add("Text", DataSource, "ThucHienCaQuy");
            cel_PtramCaQuy.DataBindings.Add("Text", DataSource, "PtramCaQuy");
            cel_Thang1ThucHien.DataBindings.Add("Text", DataSource, "ThucHienThang1");
            cel_Thang1Ptram.DataBindings.Add("Text", DataSource, "PtramThang1");
            cel_Thang2ThucHien.DataBindings.Add("Text", DataSource, "ThucHienThang2");
            cel_Thang2Ptram.DataBindings.Add("Text", DataSource, "PtramThang2");
            cel_Thang3ThucHien.DataBindings.Add("Text", DataSource, "ThucHienThang3");
            cel_Thang3Ptram.DataBindings.Add("Text", DataSource, "PtramThang3");

            GroupHeader1.GroupFields.Add(new GroupField("Stt"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            lblTieuDe.Text = "CHỈ TIÊU CHUYÊN MÔN";
        }

    }
}
