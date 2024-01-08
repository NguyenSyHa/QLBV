using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_HDDieuTri_MS02_30012 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_HDDieuTri_MS02_30012()
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
            gr_TenChiTieu.DataBindings.Add("Text", DataSource, "TenNhom");
            gr_TongSo.DataBindings.Add("Text", DataSource, "TongSo");
            gr_Duoi1T.DataBindings.Add("Text", DataSource, "Duoi1T");
            gr_Duoi6T.DataBindings.Add("Text", DataSource, "Duoi6T");
            gr_Duoi15T.DataBindings.Add("Text", DataSource, "Duoi15T");
            gr_CapCuu.DataBindings.Add("Text", DataSource, "CapCuu");
            gr_SoNgayDT.DataBindings.Add("Text", DataSource, "SoNgayDT");
            gr_CoBHYT.DataBindings.Add("Text", DataSource, "CoBHYT");
            gr_KhongBHYT.DataBindings.Add("Text", DataSource, "KhongBHYT");
            gr_Nu.DataBindings.Add("Text", DataSource, "Nu");
            gr_TongDTNT.DataBindings.Add("Text", DataSource, "SoNguoiDTNT");
            gr_TongNgayDTNT.DataBindings.Add("Text", DataSource, "SoNgayDTNT");

            cel_TenChiTiet.DataBindings.Add("Text", DataSource, "TenChiTiet");
            cel_TongSo.DataBindings.Add("Text", DataSource, "TongSo");
            cel_Duoi1T.DataBindings.Add("Text", DataSource, "Duoi1T");
            cel_Duoi6T.DataBindings.Add("Text", DataSource, "Duoi6T");
            cel_Duoi15T.DataBindings.Add("Text", DataSource, "Duoi15T");
            cel_CapCuu.DataBindings.Add("Text", DataSource, "CapCuu");
            cel_SoNgayDT.DataBindings.Add("Text", DataSource, "SoNgayDT");
            cel_CoBHYT.DataBindings.Add("Text", DataSource, "CoBHYT");
            cel_KhongBHYT.DataBindings.Add("Text", DataSource, "KhongBHYT");
            cel_Nu.DataBindings.Add("Text", DataSource, "Nu");
            cel_SoNguoiDTNT.DataBindings.Add("Text", DataSource, "SoNguoiDTNT");
            cel_SoNgayDTNT.DataBindings.Add("Text", DataSource, "SoNgayDTNT");

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
