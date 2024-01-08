using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repPhieuNhapKho_20001_DY : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuNhapKho_20001_DY()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDuoc.DataBindings.Add("Text", DataSource, "TenDuoc");
            celNguonGoc.DataBindings.Add("Text", DataSource, "NguonGoc");
            celTinhTrangN.DataBindings.Add("Text", DataSource, "TinhTNhap");
            //colHanDung1.DataBindings.Add("Text", DataSource, "HanDung").ToString();
            celYCSD.DataBindings.Add("Text", DataSource, "YCSD");
            colDVT.DataBindings.Add("Text", DataSource, "DonViTinh");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGiaDY").FormatString = DungChung.Bien.FormatString[1];
            colSLTN.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
            colSLCT.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            celtongtien.DataBindings.Add("Text", DataSource, "ThanhTienN");
            celtongtien.Summary.FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //double tongtien = 0;
            //if(TongTien.Value!=null && TongTien.Value.ToString()!="")
            //tongtien =Convert.ToDouble(TongTien.Value);
            //colSoTienchu.Text = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(tongtien," đồng!");
            //colNhapNgay.Text = "Nhập, " + Ngay.Value;
            //colNLB.Text = DungChung.Bien.NguoiLapBieu;
            xrTableCell27.Text = DungChung.Bien.KeToanTruong;
            thukho.Text = DungChung.Bien.ThuKho;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (GetCurrentColumnValue("HanDung") != null && GetCurrentColumnValue("HanDung").ToString() != "")
            //{
            //    DateTime dt;
            //    if (DateTime.TryParse(GetCurrentColumnValue("HanDung").ToString(), out dt))
            //    {
            //        celYCSD.Text = dt.Day + "/" + dt.Month + "/" + dt.Year;
            //    }
            //}
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtDiaChi.Text = "BỆNH VIỆN Y HỌC CỔ TRUYỀN";
            //txtMaDV.Text = "Mã đơn vị có quan hệ với ngân sách: " + DungChung.Bien.MaNSach;
        }

        private void colSLCT_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "30003")
            //{
            //    if (this.GetCurrentColumnValue("SoLuongN") != null)
            //    {
            //        double _slct = Convert.ToDouble(this.GetCurrentColumnValue("SoLuongN"));
            //        if (_slct >= 0)
            //        {
            //            if (_slct.ToString().Length == 1)
            //                colSLCT.Text = "0" + _slct.ToString();
            //            else colSLCT.Text = _slct.ToString();
            //        }
            //        else colSLCT.Text = "";
            //    }
            //}
        }



    }
}
