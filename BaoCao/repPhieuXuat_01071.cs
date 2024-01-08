using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuXuat_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuXuat_01071()
        {
            InitializeComponent();
            SubBand2.Visible = false;
        }
        public void BindingData()
        {
            //colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colHamLuong.DataBindings.Add("Text", DataSource, "HamLuong");
            colThanhtien.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            colsoluongtt.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[0];
            colTongtienRep.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            colTongtienRep.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTongtienRep2.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            colTongtienRep2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colDonvi.DataBindings.Add("Text", DataSource, "DonVi");
            colMaSo.DataBindings.Add("Text", DataSource, "MaTam");


        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12122")
            {
                TenDV.Value = DungChung.Bien.TenCQCQ;
                Diachi.Value = DungChung.Bien.TenCQ;
            }
            else
            {
                TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                Diachi.Value = DungChung.Bien.DiaChi;
            }
            if (DungChung.Bien.MaBV == "24009")
            {
                xrLabel27.Visible = false;
                xrLabel26.Visible = false;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

            //double tongtien = 0;
            //if(TongTien.Value!=null && TongTien.Value.ToString()!="")
            //tongtien =Convert.ToDouble(TongTien.Value);
            //txtsotien.Text = "Bằng chữ: " + DungChung.Ham.DocTienBangChu(tongtien," đồng!");
            colTTDV.Text = DungChung.Bien.GiamDoc;
            colThuKho.Text = DungChung.Bien.ThuKho;
            if (Nguoinhanhang.Value != null)
                colNguoiNhanHang.Text = Nguoinhanhang.Value.ToString();
            //if(DungChung.Bien.MaBV=="12122")
            //{
            //    colNguoiNhanHang.Text = "";
            //}
            
            if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30009")
            {
                colKeToanTruong.Text = DungChung.Bien.TruongKhoaDuoc;
                colNguoiLapPhieu.Text = "";
                col_KTT_td.Text = "Trưởng khoa dược";
                col_NLP_td.Text = "Thống kê dược";
            }
            else
                if (DungChung.Bien.MaBV == "26007")
                {
                    xrTableCell30.Text = "Thủ kho";
                    xrTableCell31.Text = "Trưởng khoa dược";
                    colThuKho.Text = DungChung.Bien.TruongKhoaDuoc;
                    colNguoiNhanHang.Text = DungChung.Bien.ThuKho;
                    xrTableRow4.Visible = true;
                    xrTableRow5.Visible = true;
                    xrTableCell9.Text = "Người nhận hàng";
                    xrTableCell13.Text = "(Ký, họ và tên)";
                    cell_NguoiNhan.Visible = false;
                    xrTableCell17.Visible = false;
                }
                else
                {

                    colNguoiLapPhieu.Text = DungChung.Bien.NguoiLapBieu;
                    colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
                    if(DungChung.Bien.MaBV=="24009")
                    colNguoiNhanHang.Text = NoiNhan.Value.ToString();

                }
        }

        private void txtsotien_BeforePrint(object sender, CancelEventArgs e)
        {
            Double st = Convert.ToDouble(TongTien.Value);
            st = Math.Round(st, 0);
            txtsotien2.Text = "Tổng số tiền (bằng chữ): " + DungChung.Ham.DocTienBangChu(st, " đồng!");
        }

        private void txtsotien_BeforePrint_1(object sender, CancelEventArgs e)
        {
            Double st = Convert.ToDouble(TongTien.Value);
            st = Math.Round(st, 0);
            txtsotien.Text = "Tổng số tiền (bằng chữ): " + DungChung.Ham.DocTienBangChu(st, " đồng!");
        }

    }
}
