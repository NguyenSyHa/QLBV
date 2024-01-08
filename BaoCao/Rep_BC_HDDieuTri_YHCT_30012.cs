using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_HDDieuTri_YHCT_30012 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_HDDieuTri_YHCT_30012()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lbl_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lbl_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celTruongKhoa.Text = DungChung.Bien.TruongKhoaLS;
            celLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }

        public void BindingData()
        {
            gr_STT.DataBindings.Add("Text", DataSource, "Stt");
            gr_TenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            gr_TongSoNBDT.DataBindings.Add("Text", DataSource, "TongSo");
            gr_SoNguoiDTNT.DataBindings.Add("Text", DataSource, "SoNguoiDTNT");
            gr_SoNgayDTNT.DataBindings.Add("Text", DataSource, "SoNgayDTNT");
            gr_CoBHYT.DataBindings.Add("Text", DataSource, "CoBHYT");
            gr_Nu.DataBindings.Add("Text", DataSource, "Nu");

            cel_TenChiTiet.DataBindings.Add("Text", DataSource, "TenChiTiet");
            cel_TongSo.DataBindings.Add("Text", DataSource, "TongSo");
            cel_SoNguoiDTNT.DataBindings.Add("Text", DataSource, "SoNguoiDTNT");
            cel_SoNgayDTNT.DataBindings.Add("Text", DataSource, "SoNgayDTNT");
            cel_CoBHYT.DataBindings.Add("Text", DataSource, "CoBHYT");
            cel_Nu.DataBindings.Add("Text", DataSource, "Nu");

            GroupHeader1.GroupFields.Add(new GroupField("Stt"));
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("TenChiTiet") != null)
            {
                string nt = this.GetCurrentColumnValue("TenChiTiet").ToString();
                if (nt == "")
                {
                    xrTableRow4.Visible = false;
                    //xrLine1.Visible = false;
                }
                else
                {
                    xrTableRow4.Visible = true;
                    //xrLine1.Visible = true;
                }

            }
        }

    }
}
