using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_ChiTieuChuyenMon_BVDKKimThanh : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_ChiTieuChuyenMon_BVDKKimThanh()
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
            gr_ChiTieuCaNam.DataBindings.Add("Text", DataSource, "ChiTieuCaNam");
            gr_ThucHienCaNam.DataBindings.Add("Text", DataSource, "ThucHienCaNam");
            gr_PtramCaNam.DataBindings.Add("Text", DataSource, "PtramCaNam");
            gr_QuyIThucHien.DataBindings.Add("Text", DataSource, "ThucHienQuyI");
            gr_QuyIPtram.DataBindings.Add("Text", DataSource, "PtramQuyI");
            gr_QuyIIThucHien.DataBindings.Add("Text", DataSource, "ThucHienQuyII");
            gr_QuyIIPtram.DataBindings.Add("Text", DataSource, "PtramQuyII");
            gr_QuyIIIThucHien.DataBindings.Add("Text", DataSource, "ThucHienQuyIII");
            gr_QuyIIIPtram.DataBindings.Add("Text", DataSource, "PtramQuyIII");
            gr_QuyIVThucHien.DataBindings.Add("Text", DataSource, "ThucHienQuyIV");
            gr_QuyIVPtram.DataBindings.Add("Text", DataSource, "PtramQuyIV");

            cel_ChiTietDM.DataBindings.Add("Text", DataSource, "ChiTietDanhMuc");
            cel_DVTinh.DataBindings.Add("Text", DataSource, "DVTinh");
            cel_ChiTieuCaNam.DataBindings.Add("Text", DataSource, "ChiTieuCaNam");
            cel_ThucHienCaNam.DataBindings.Add("Text", DataSource, "ThucHienCaNam");
            cel_PtramCaNam.DataBindings.Add("Text", DataSource, "PtramCaNam");
            cel_QuyIThucHien.DataBindings.Add("Text", DataSource, "ThucHienQuyI");
            cel_QuyIPtram.DataBindings.Add("Text", DataSource, "PtramQuyI");
            cel_QuyIIThucHien.DataBindings.Add("Text", DataSource, "ThucHienQuyII");
            cel_QuyIIPtram.DataBindings.Add("Text", DataSource, "PtramQuyII");
            cel_QuyIIIThucHien.DataBindings.Add("Text", DataSource, "ThucHienQuyIII");
            cel_QuyIIIPtram.DataBindings.Add("Text", DataSource, "PtramQuyIII");
            cel_QuyIVThucHien.DataBindings.Add("Text", DataSource, "ThucHienQuyIV");
            cel_QuyIVPtram.DataBindings.Add("Text", DataSource, "PtramQuyIV");

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
