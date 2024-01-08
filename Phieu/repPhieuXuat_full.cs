using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuXuat_full : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuXuat_full()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            //colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            xrTableCell40.DataBindings.Add("Text", DataSource, "TenDV");
            colThanhtien.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString=DungChung.Bien.FormatString[1];
            colsoluongtt.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell46.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[1];
            colTongtienRep.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell48.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            colGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell47.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colDonvi.DataBindings.Add("Text", DataSource, "DonVi");
            xrTableCell42.DataBindings.Add("Text", DataSource, "DonVi");
            if (DungChung.Bien.MaBV == "30009")
            {
                colMaSo.DataBindings.Add("Text", DataSource, "MaNoiBo");
                xrTableCell41.DataBindings.Add("Text", DataSource, "MaNoiBo");
            }
            else
            {
                colMaSo.DataBindings.Add("Text", DataSource, "MaSo");
                xrTableCell41.DataBindings.Add("Text", DataSource, "MaSo");
            }
            colSoLo.DataBindings.Add("Text", DataSource, "SoLo");
            colHanDung.DataBindings.Add("Text", DataSource, "HanDung");//NuocSX
            colNuoSX.DataBindings.Add("Text", DataSource, "NuocSX");
            
      
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30009")
            {
                tttduoc.Text = "Trưởng khoa dược";
                nnphieu.Text = "Thống kê dược";
                headerMaso.Text = "Mã nội bộ";
                xrTableCell6.Text = "Mã nội bộ";
            }
            TenDV.Value = "Đơn vị: "+ DungChung.Bien.TenCQ;
            Diachi.Value = "Địa chỉ: " + DungChung.Bien.DiaChi;
            txtMaCS.Text = DungChung.Bien.MaBV == "20001" ? "Mã đơn vị có quan hệ với ngân sách: " : "Mã đơn vị có quan hệ với ngân sách: " + DungChung.Bien.MaNSach;
            if (DungChung.Bien.MaBV == "20001")
            {
                xrLabel15.Visible = xrLabel19.Visible = true;
                xrLabel28.Visible = xrLabel29.Visible = xrLabel60.Visible = xrLabel61.Visible = xrLabel26.Visible = xrLabel27.Visible = false;
                xrLabel6.Text = "Địa chỉ (bộ phận):";
                xrLabel7.Text = "Lý do xuất kho:";
                xrLabel8.Text = "Xuất tại kho (ngăn lô):";
                SubBand1.Visible = xrLabel16.Visible = xrLabel17.Visible = true;
            }
            else
                SubBand1.Visible = xrLabel15.Visible = xrLabel19.Visible = xrLabel16.Visible = xrLabel17.Visible = false;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30009")
            {
                xrTableCell15.Text = "";
            }
            //double tongtien = 0;
            //if(TongTien.Value!=null && TongTien.Value.ToString()!="")
            //tongtien =Convert.ToDouble(TongTien.Value);
            //txtsotien.Text = "Bằng chữ: " + DungChung.Ham.DocTienBangChu(tongtien," đồng!");


            colNguoiLapPhieu.Text = DungChung.Bien.NguoiLapBieu;
            colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            colThuKho.Text = DungChung.Bien.ThuKho;
            colTTDV.Text = DungChung.Bien.GiamDoc;
            if (DungChung.Bien.MaBV == "20001")
            {
                //xrTableCell15.Text = "";
                //xrTableCell37.Text = "(Hoặc phụ trách bộ phận)";
                colTTDV.Text = DungChung.Bien.TruongKhoaDuoc;
                xrTableCell37.Text = "(Ký, họ tên)";
                xrTableCell33.Text = "Trưởng khoa dược";
                xrTableRow2.Borders = DevExpress.XtraPrinting.BorderSide.None;
                xrTableCell49.Text = xrTableCell50.Text = xrTableCell51.Text = xrTableCell52.Text = xrTableCell8.Text = "";
                txtsotien.Visible = true;
            }
        }

        private void txtsotien_BeforePrint(object sender, CancelEventArgs e)
        {
            Double st = Convert.ToDouble(TongTien.Value);
            st = Math.Round(st, 0);
            txtsotien.Text = "Tổng số tiền (viết bằng chữ): " + DungChung.Ham.DocTienBangChu(st, " đồng");
        }

        private void colHanDung_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("HanDung") != null && GetCurrentColumnValue("HanDung").ToString().Length > 10)
                colHanDung.Text = GetCurrentColumnValue("HanDung").ToString().Substring(0, 10);
            else
                colHanDung.Text = "";
        }

        private void colThuKho_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
            {
                xrTable3.Visible = false;
                xrTableCell13.Text = "Tên, nhãn hiệu, quy cách, phẩm chất";
                
            }
                
            else
                xrTable5.Visible = false;
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
                PageHeader.Visible = false;
            else
                PageHeader.Visible = true;
                
        }

    }
}
