using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_KhamChuaBenhHangNgay : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_KhamChuaBenhHangNgay()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            //cel_STT.DataBindings.Add("Text", DataSource, "Stt");
            cel_TenChiTiet.DataBindings.Add("Text", DataSource, "TenChiTiet");
            cel_DVTinh.DataBindings.Add("Text", DataSource, "DonVi");
            cel_SoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            cel_GhiChu.DataBindings.Add("Text", DataSource, "GhiChu");

            gr_STT.DataBindings.Add("Text", DataSource, "Stt");
            gr_TenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            gr_DVTinh.DataBindings.Add("Text", DataSource, "DonVi");
            gr_SoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            gr_GhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
            GroupHeader1.GroupFields.Add(new GroupField("Stt"));
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("TenChiTiet") != null)
            {
                string nt = this.GetCurrentColumnValue("TenChiTiet").ToString();
                if (nt == "")
                    xrTableRow3.Visible = false;
                else
                    xrTableRow3.Visible = true;
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lbl_TenCQCQ.Text = DungChung.Bien.TenCQCQ;
            lbl_tenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
        }

    }
}
