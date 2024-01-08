using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repPhieuNhapKho_NBinh : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuNhapKho_NBinh()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDuoc.DataBindings.Add("Text", DataSource, "TenDuoc");
            colSoLo.DataBindings.Add("Text", DataSource, "SoLo");
            colHanDung1.DataBindings.Add("Text", DataSource, "HanDung").ToString();
            colDVT.DataBindings.Add("Text", DataSource, "DonViTinh");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSLTN.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienCong.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            double tongtien = 0;
            if (TongTien.Value != null && TongTien.Value.ToString() != "")
                tongtien = Convert.ToDouble(TongTien.Value);
            colSoTienchu.Text = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(tongtien, " đồng!");
            colNhapNgay.Text = "Nhập, " + Ngay.Value;
            colNLB.Text = DungChung.Bien.NguoiLapBieu;

            colThuKho01.Text = DungChung.Bien.ThuKho;
            colTTDV.Text = DungChung.Bien.GiamDoc;
            if (DungChung.Bien.MaBV == "12012")
            {
                colKTTruong.Text = "Trưởng khoa dược";
                colKTT.Text = DungChung.Bien.TruongKhoaDuoc;
            }
            else
            {
               colKTTruong.Text= "Kế toán trưởng";
                colKTT.Text = DungChung.Bien.KeToanTruong;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("HanDung") != null && GetCurrentColumnValue("HanDung").ToString() != "")
            {
                DateTime dt;
                if (DateTime.TryParse(GetCurrentColumnValue("HanDung").ToString(), out dt))
                {
                    colHanDung.Text = dt.Day + "/" + dt.Month + "/" + dt.Year;
                }
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = txtTenCQ2.Text = "Đơn vị: " + DungChung.Bien.TenCQ;
            txtDiaChi.Text = txtDiaChi2.Text = "Địa chỉ: " + DungChung.Bien.DiaChi;
            txtMaDV.Text = txtMaDV2.Text = "Mã đơn vị có quan hệ với ngân sách: " + DungChung.Bien.MaNSach;
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }



    }
}
