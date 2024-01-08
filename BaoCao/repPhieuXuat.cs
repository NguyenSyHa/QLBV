using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuXuat : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuXuat()
        {
            InitializeComponent();
            SubBand2.Visible = false;

            
            //cell_NguoiNhan.Font = new System.Drawing.Font("Times New Roman", 12.75F, FontStyle.Bold);
            //colnguoinhan.Font = new System.Drawing.Font("Times New Roman", 12.75F, FontStyle.Bold);
            //txtThukho.Font = new System.Drawing.Font("Times New Roman", 12.75F, FontStyle.Bold);

        }
        public void BindingData()
        {
            
                
                cellTenDV.DataBindings.Add("Text", DataSource, "TenDV");
                
                cellThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
                cellsoluongtt.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[0];
                cellsoluongyc.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[0];
                colTongtienRep.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
                colTongtienRep.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTongtienRep2.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
                colTongtienRep2.Summary.FormatString = DungChung.Bien.FormatString[1];
                cellDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                cellDonVi.DataBindings.Add("Text", DataSource, "DonVi");
                cellMaSo.DataBindings.Add("Text", DataSource, "MaTam");
            
            //colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            



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
                TenDV.Value = "Đơn vị: " + DungChung.Bien.TenCQ;
                Diachi.Value = "Địa chỉ: " + DungChung.Bien.DiaChi;
            }
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand5.Visible = false;
                SubBand6.Visible = true;
            }
            if (DungChung.Bien.MaBV == "24009")
            {
                xrLabel27.Visible = false;
                xrLabel26.Visible = false;
            }
            if (DungChung.Bien.MaBV == "27023")
            {
                xrLabel26.Visible = false;
                xrLabel27.Visible = false;
            }
            if (DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "20001")
            {
                xrLabel26.Visible = false;
                xrLabel27.Visible = false;
                if (DungChung.Bien.MaBV == "20001")
                {
                    xrLabel28.Visible = xrLabel29.Visible = xrLabel60.Visible = xrLabel61.Visible = false;
                    xrLabel6.Text = "Địa chỉ (bộ phận):";
                    xrLabel7.Text = "Lý do xuất kho:";
                    xrLabel8.Text = "Địa điểm:";
                    khoanhan.Value = "";
                    xrLabel6.Text = "Địa chỉ (bộ phận):";
                    xrLabel7.Text = "Lý do xuất kho:";
                    xrLabel8.Text = "Xuất tại kho (ngăn lô):";
                    xrTableCell22.Text = "Trưởng khoa dược";
                    SubBand1.Visible = xrLabel20.Visible = xrLabel22.Visible = true;
                }
                else
                    SubBand1.Visible = xrLabel20.Visible = xrLabel22.Visible = false;
            }
            else
            {
                SubBand1.Visible = false;
                xrLabel9.Visible = false;
                xrLabel19.Visible = false;
            }

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

            //double tongtien = 0;
            //if(TongTien.Value!=null && TongTien.Value.ToString()!="")
            //tongtien =Convert.ToDouble(TongTien.Value);
            //txtsotien.Text = "Bằng chữ: " + DungChung.Ham.DocTienBangChu(tongtien," đồng!");
            colThuKho.Text = DungChung.Bien.ThuKho;
            if (Nguoinhanhang.Value != null)
                colNguoiNhanHang.Text = Nguoinhanhang.Value.ToString();
            //if(DungChung.Bien.MaBV=="12122")
            //{
            //    colNguoiNhanHang.Text = "";
            //}
            if (DungChung.Bien.MaBV == "27023")
            {
                Double st = Convert.ToDouble(TongTien.Value);
                st = Math.Round(st, 0);
                txtsotien.Text = "Tổng số tiền (bằng chữ): " + DungChung.Ham.DocTienBangChu(st, " đồng!");
                txtsotien.Visible = true;
            }

            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell37.Text = "(Hoặc phụ trách bộ phận)(Ký, họ và tên)";
                xrTableCell33.Text = "(Ký, họ và tên, đóng dấu)";
            }

            if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30009")
            {
                colKeToanTruong.Text = DungChung.Bien.TruongKhoaDuoc;
                colNguoiLapPhieu.Text = "";
                col_KTT_td.Text = "Trưởng khoa dược";
                col_NLP_td.Text = "Thống kê dược";
            }
            else if (DungChung.Bien.MaBV == "26007")
            {
                xrTableCell30.Text = "Thủ kho";
                xrTableCell31.Text = "Trưởng khoa dược";
                txtThukho.Text = DungChung.Bien.TruongKhoaDuoc;
                colNguoiNhanHang.Text = DungChung.Bien.ThuKho;
                xrTableRow4.Visible = true;
                //xrTableRow5.Visible = true;
                xrTableCell9.Text = "Người nhận hàng";

                cell_NguoiNhan.Visible = false;
                //xrTableCell17.Visible = false;
            }
            else if(DungChung.Bien.MaBV == "20001")
            {
                //xrLabel18.Visible = false;
                //xrTableCell37.Text = "(Hoặc phụ trách bộ phận)";
                //xrTableCell23.Text = "(Ký, họ tên)";               
                txtsotien2.Visible = true;
                colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
                colTTDV.Text = DungChung.Bien.TruongKhoaDuoc;
                xrTableCell37.Text = "(Ký, họ tên)";
                xrTableCell57.Text = "";
            }
                
            else
            {
                if (DungChung.Bien.MaBV == "30002")
                {
                    col_NLP_td.Text = "Thống kê dược";
                }
                colNguoiLapPhieu.Text = DungChung.Bien.NguoiLapBieu;
                colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
                if (DungChung.Bien.MaBV == "24009")
                    colNguoiNhanHang.Text = NoiNhan.Value.ToString();

            }


        }

        private void txtsotien_BeforePrint(object sender, CancelEventArgs e)
        {
            Double st = Convert.ToDouble(TongTien.Value);
            st = Math.Round(st, 0);
            txtsotien2.Text = "Tổng số tiền (viết bằng chữ): " + DungChung.Ham.DocTienBangChu(st, " đồng");
        }

        private void txtsotien_BeforePrint_1(object sender, CancelEventArgs e)
        {
            Double st = Convert.ToDouble(TongTien.Value);
            st = Math.Round(st, 0);
            txtsotien.Text = "Tổng số tiền (bằng chữ): " + DungChung.Ham.DocTienBangChu(st, " đồng");
        }

        private void SubBand1_BeforePrint(object sender, CancelEventArgs e)
        {
         
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
            {
                PageHeader.Visible = false;
            } 
        }

        private void txtSoCT_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

    }
}
