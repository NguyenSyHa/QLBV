using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repPhieuNhapKho_YHDT : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuNhapKho_YHDT()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDuoc.DataBindings.Add("Text", DataSource, "TenDuoc");
            colNguonGoc.DataBindings.Add("Text", DataSource, "NguonGoc");
            colTTNhap.DataBindings.Add("Text", DataSource, "TinhTNhap").ToString();
            colDVT.DataBindings.Add("Text", DataSource, "DonViTinh");
            colYCSD.DataBindings.Add("Text", DataSource, "YCSD");
            colPhuongPhap.DataBindings.Add("Text", DataSource, "PhuongPhap");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGiaDY").FormatString = DungChung.Bien.FormatString[1];
            colSLTN.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString=DungChung.Bien.FormatString[0];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienCong.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            double tongtien = 0;
            if(TongTien.Value!=null && TongTien.Value.ToString()!="")
            tongtien =Convert.ToDouble(TongTien.Value);
            colSoTienchu.Text = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(tongtien," đồng!");
            if(this.Ngay.Value!=null)
            colNhapNgay.Text =  Ngay.Value.ToString();
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
            colKTT.Text = DungChung.Bien.KeToanTruong;
            colThuKho01.Text = DungChung.Bien.ThuKho;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("HanDung") != null && GetCurrentColumnValue("HanDung").ToString() != "")
            {
                DateTime dt;
                if (DateTime.TryParse(GetCurrentColumnValue("HanDung").ToString(), out dt))
                {
                    colTTNhap.Text = dt.Day +"/"+ dt.Month+"/"+ dt.Year;
                }
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text ="Đơn vị: "+ DungChung.Bien.TenCQ;
            txtDiaChi.Text ="Địa chỉ: "+ DungChung.Bien.DiaChi;
            txtMaDV.Text ="Mã đơn vị có quan hệ với ngân sách: "+ DungChung.Bien.MaNSach;
        }


     
    }
}
