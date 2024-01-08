using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repPhieuNhapKho_27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuNhapKho_27023()
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
            if (DungChung.Bien.MaBV == "27023")
                colSLTN.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = "{0:##,##0.###}";
            else
                colSLTN.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            celThanhtienT.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //double tongtien = 0;
            //if(TongTien.Value!=null && TongTien.Value.ToString()!="")
            //tongtien =Convert.ToDouble(TongTien.Value);
            //colSoTienchu.Text = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(tongtien," đồng!");
            colNhapNgay.Text = "Nhập, " + Ngay.Value;
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
            colKTT.Text = DungChung.Bien.KeToanTruong;
            colThuKho01.Text = DungChung.Bien.ThuKho;
            if (DungChung.Bien.MaBV == "26007")
            {
                SubBand3.Visible = false;
                SubBand1.Visible = true;
                //lab1.Text = "Thủ trưởng đơn vị"; lab2.Text = "Kế toán trưởng"; lab3.Text = "Trưởng khoa dược"; lab4.Text = "Người lập biểu"; lab5.Text = "Thủ kho";
                lab1.Text = "Người lập biểu"; lab2.Text = "Thủ kho"; lab3.Text = "Trưởng khoa dược"; lab4.Text = "Kế toán trưởng"; lab5.Text = "Thủ trưởng đơn vị";
                cell_Ten6.Text = "Người giao hàng";
                col1.Text = DungChung.Bien.NguoiLapBieu; col2.Text = DungChung.Bien.ThuKho; col3.Text = DungChung.Bien.TruongKhoaDuoc; col4.Text = DungChung.Bien.KeToanTruong;
                col5.Text = DungChung.Bien.GiamDoc;
                xrTableCell33.Text = "(Ký, họ tên)";
            }

            else if (DungChung.Bien.MaBV == "27021")
            {
                SubBand2.Visible = false;
                SubBand3.Visible = false;
                SubBand1.Visible = true;
                xrTableCell35.Text = "";
                xrTableCell16.Text = "";
                xrTableCell19.Text = "";
                xrTableCell27.Text = "";
                xrTableCell28.Text = "";


                lab1.Text = "Khoa dược"; lab2.Text = ""; lab3.Text = "Kế toán"; lab4.Text = "Người nhận"; lab5.Text = "Thủ trưởng đơn vị";
                col1.Text = KhoaDuoc.Value.ToString(); col2.Text = ""; col3.Text = KeToan.Value.ToString(); col4.Text = NguoiNhan.Value.ToString();
                col5.Text = ThuTruong.Value.ToString();

                //xrTableCell27.Text = "";
            }
            else if (DungChung.Bien.MaBV == "30012")
            {
                SubBand2.Visible = false;
                SubBand3.Visible = false;
                SubBand1.Visible = true;
                //lab1.Text = "Thủ trưởng đơn vị"; lab2.Text = "Kế toán trưởng"; lab3.Text = "Trưởng khoa dược"; lab4.Text = "Người lập biểu"; lab5.Text = "Thủ kho";
                lab1.Text = "Người lập biểu"; lab2.Text = "Thủ kho"; lab3.Text = "Trưởng khoa dược"; lab4.Text = "Kế toán trưởng"; lab5.Text = "Thủ trưởng đơn vị";
                cell_Ten6.Visible = false;
                xrTableCell33.Visible = false;
                col1.Text = DungChung.Bien.NguoiLapBieu; col2.Text = DungChung.Bien.ThuKho; col3.Text = DungChung.Bien.TruongKhoaDuoc; col4.Text = DungChung.Bien.KeToanTruong;
                col5.Text = DungChung.Bien.GiamDoc;
                xrTableCell33.Text = "(Ký, họ tên)";
            }
            else if (DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "12102")
            {
                SubBand2.Visible = false;
                SubBand3.Visible = false;
                SubBand1.Visible = true;
                lab1.Text = "Người lập biểu"; lab2.Text = "Người giao hàng"; lab3.Text = "Thủ kho"; lab4.Text = "Kế toán trưởng"; lab5.Text = "Thủ trưởng đơn vị";
                col1.Text = DungChung.Bien.NguoiLapBieu; col3.Text = DungChung.Bien.ThuKho; col4.Text = DungChung.Bien.KeToanTruong; col5.Text = DungChung.Bien.GiamDoc;
            }
            else if (DungChung.Bien.MaBV == "30004")
            {
                SubBand2.Visible = false;
                SubBand3.Visible = false;
                SubBand1.Visible = true;
                lab1.Text = "Người lập biểu"; lab2.Text = "Người giao hàng"; lab3.Text = "Thủ kho"; lab4.Text = "Kế toán trưởng"; lab5.Text = "Trưởng khoa dược";
                col1.Text = DungChung.Bien.NguoiLapBieu; col3.Text = DungChung.Bien.ThuKho; col4.Text = DungChung.Bien.KeToanTruong; col5.Text = DungChung.Bien.TruongKhoaDuoc;
            }

            else if (DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "27022")
            {

                SubBand2.Visible = false;
                SubBand3.Visible = true;
                SubBand1.Visible = false;

            }
            //else if(DungChung.Bien.MaBV == "27021")
            //{

            //    SubBand2.Visible = true;
            //    SubBand3.Visible = false;
            //    SubBand1.Visible = false;
            //    celThuTruong.Text = ThuTruong.ToString();
            //    celKhoaDuoc.Text = KhoaDuoc.ToString();
            //    celKetoan.Text = KeToan.ToString();
            //    celNguoiNhan.Text = NguoiNhan.ToString();
            //}
            else
            {

                SubBand2.Visible = false;
                SubBand3.Visible = false;
                SubBand1.Visible = true;
                lab1.Text = "Người lập biểu"; lab2.Text = "Người giao hàng"; lab3.Text = "Thủ kho"; lab4.Text = "Kế toán trưởng"; lab5.Text = "Thủ trưởng đơn vị";
                col1.Text = DungChung.Bien.NguoiLapBieu; col3.Text = DungChung.Bien.ThuKho; col4.Text = DungChung.Bien.KeToanTruong; col5.Text = DungChung.Bien.GiamDoc;

            }

            //if (DungChung.Bien.MaBV == "3000..")
            //{
            //    xrTableKyTen.Visible = false;
            //    xrTableViXuyen.Visible = true;
            //    colTTDV_VX.Text = DungChung.Bien.GiamDoc;
            //    colThuKhoVX.Text = DungChung.Bien.ThuKho;
            //    colNguoiLapVX.Text = DungChung.Bien.NguoiLapBieu;
            //    colKTT_VX.Text = DungChung.Bien.KeToanTruong;
            //}
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
            txtTenCQ.Text = "Đơn vị: " + DungChung.Bien.TenCQ;
            txtDiaChi.Text = "Địa chỉ: " + DungChung.Bien.DiaChi;
            if (DungChung.Bien.MaBV == "27023")
            {
                xrLabel3.Visible = false;
                xrLabel4.Visible = false;
                xrLabel2.Visible = false;
            }

            if (DungChung.Bien.MaBV == "27022")
            {
                xrLabel2.Visible = true;
                xrLabel3.Visible = false;
            }
            //txtMaDV.Text = "Mã đơn vị có quan hệ với ngân sách: " + DungChung.Bien.MaNSach;
        }

        private void colSLCT_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30003")
            {
                if (this.GetCurrentColumnValue("SoLuongN") != null)
                {
                    double _slct = Convert.ToDouble(this.GetCurrentColumnValue("SoLuongN"));
                    if (_slct >= 0)
                    {
                        if (_slct.ToString().Length == 1)
                            colSLCT.Text = "0" + _slct.ToString();
                        else colSLCT.Text = _slct.ToString();
                    }
                    else colSLCT.Text = "";
                }
            }
        }



    }
}
