using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_ChiTieuChuyenMon_BVDKKimThanh_LCK_New : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_ChiTieuChuyenMon_BVDKKimThanh_LCK_New()
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
            gr_ChiTieuNam2.DataBindings.Add("Text", DataSource, "ChiTieuNam2");
            gr_ThucHienNam2.DataBindings.Add("Text", DataSource, "ThucHienNam2");
            gr_PtramNam2.DataBindings.Add("Text", DataSource, "PtramNam2");

            gr_ChiTieuNam1.DataBindings.Add("Text", DataSource, "ChiTieuNam1");
            gr_ThucHienNam1.DataBindings.Add("Text", DataSource, "ThucHienNam1");
            
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
            cel_ChiTieuNam2.DataBindings.Add("Text", DataSource, "ChiTieuNam2");
            cel_ThucHienNam2.DataBindings.Add("Text", DataSource, "ThucHienNam2");
            cel_PtramNam2.DataBindings.Add("Text", DataSource, "PtramNam2");

            cel_ChiTieuNam1.DataBindings.Add("Text", DataSource, "ChiTieuNam1");
            cel_ThucHienNam1.DataBindings.Add("Text", DataSource, "ThucHienNam1");

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
        }

    }
}
