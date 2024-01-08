using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repPhieuNhapKho : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuNhapKho()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDuoc.DataBindings.Add("Text", DataSource, "TenDuoc");
            xrTableCell46.DataBindings.Add("Text", DataSource, "TenDuoc");
            colSoLo.DataBindings.Add("Text", DataSource, "SoLo");
            xrTableCell47.DataBindings.Add("Text", DataSource, "SoLo");
            colHanDung1.DataBindings.Add("Text", DataSource, "HanDung").ToString();
            colDVT.DataBindings.Add("Text", DataSource, "DonViTinh");
            xrTableCell49.DataBindings.Add("Text", DataSource, "DonViTinh");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell52.DataBindings.Add("Text", DataSource, "DonGia");
            if (DungChung.Bien.MaBV == "27023")
                colSLTN.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = "{0:##,##0.###}";
            else
        
            colSLTN.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
            xrTableCell51.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell53.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            coltongtien.DataBindings.Add("Text", DataSource, "ThanhTienN");
            coltongtien.Summary.FormatString = DungChung.Bien.FormatString[1];

            colThanhTienCong.DataBindings.Add("Text", DataSource, "ThanhTienN");
            colThanhTienCong.Summary.FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            double tongtien = 0;
            string tt = tongtien.ToString();
            if(TongTien.Value!=null && TongTien.Value.ToString()!="")
                tongtien = Convert.ToDouble(TongTien.Value);
            colSoTienchu.Text = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(tongtien," đồng");
            coltienchu.Text = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(tongtien, " đồng");
            colNhapNgay.Text = DungChung.Bien.MaBV == "20001" ? Ngay.Value.ToString() : "Nhập, " + Ngay.Value;
            colNhapNgay1.Text = DungChung.Bien.MaBV == "20001" ? Ngay.Value.ToString() : "";
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
            colKTT.Text = DungChung.Bien.KeToanTruong;
            colThuKho01.Text = DungChung.Bien.ThuKho;
            if (DungChung.Bien.MaBV == "26007")
            {
                xrTableKyTen.Visible = false;
                xrTableViXuyen.Visible = true;
                //lab1.Text = "Thủ trưởng đơn vị"; lab2.Text = "Kế toán trưởng"; lab3.Text = "Trưởng khoa dược"; lab4.Text = "Người lập biểu"; lab5.Text = "Thủ kho";
                lab1.Text = "Người lập biểu"; lab2.Text = "Thủ kho"; lab3.Text = "Trưởng khoa dược"; lab4.Text = "Kế toán trưởng"; lab5.Text = "Thủ trưởng đơn vị";
                cell_Ten6.Text = "Người giao hàng";
                col1.Text = DungChung.Bien.NguoiLapBieu; col2.Text =DungChung.Bien.ThuKho; col3.Text = DungChung.Bien.TruongKhoaDuoc; col4.Text = DungChung.Bien.KeToanTruong ;
                col5.Text = DungChung.Bien.GiamDoc;
                xrTableCell33.Text = "(Ký, họ tên)";
            }

            if (DungChung.Bien.MaBV == "30012")
            {
                xrTableKyTen.Visible = false;
                xrTableViXuyen.Visible = true;
                //lab1.Text = "Thủ trưởng đơn vị"; lab2.Text = "Kế toán trưởng"; lab3.Text = "Trưởng khoa dược"; lab4.Text = "Người lập biểu"; lab5.Text = "Thủ kho";
                lab1.Text = "Người lập biểu"; lab2.Text = "Thủ kho"; lab3.Text = "Trưởng khoa dược"; lab4.Text = "Kế toán trưởng"; lab5.Text = "Thủ trưởng đơn vị";
                cell_Ten6.Visible = false;
                xrTableCell33.Visible = false;
                col1.Text = DungChung.Bien.NguoiLapBieu; col2.Text = DungChung.Bien.ThuKho; col3.Text = DungChung.Bien.TruongKhoaDuoc; col4.Text = DungChung.Bien.KeToanTruong;
                col5.Text = DungChung.Bien.GiamDoc;
                xrTableCell33.Text = "(Ký, họ tên)";
            }
            if (DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "12102")
            {
                xrTableKyTen.Visible = false;
                xrTableViXuyen.Visible = true;
                lab1.Text = "Người lập biểu"; lab2.Text = "Người giao hàng"; lab3.Text = "Thủ kho"; lab4.Text = "Kế toán trưởng"; lab5.Text = "Thủ trưởng đơn vị";
                col1.Text = DungChung.Bien.NguoiLapBieu; col3.Text = DungChung.Bien.ThuKho; col4.Text = DungChung.Bien.KeToanTruong; col5.Text = DungChung.Bien.GiamDoc;
            }
            if (DungChung.Bien.MaBV == "20001")
            {
                xrTableKyTen.Visible = false;
                xrTableViXuyen.Visible = true;
                lab1.Text = "Người lập biểu"; lab2.Text = "Người giao hàng"; lab3.Text = "Thủ kho"; lab4.Text = "Kế toán trưởng"; lab5.Text = "Trưởng khoa dược";
                col1.Text = DungChung.Bien.NguoiLapBieu ; col3.Text = DungChung.Bien.ThuKho; col4.Text = DungChung.Bien.KeToanTruong; col5.Text = DungChung.Bien.TruongKhoaDuoc;
              
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                xrTableKyTen.Visible = false;
                xrTableViXuyen.Visible = true;
                lab1.Text = "Người lập biểu"; lab2.Text = "Người giao hàng"; lab3.Text = "Thủ kho"; lab4.Text = "Kế toán trưởng"; lab5.Text = "Trưởng khoa dược";
                col1.Text = DungChung.Bien.NguoiLapBieu; col3.Text = DungChung.Bien.ThuKho; col4.Text = DungChung.Bien.KeToanTruong; col5.Text = DungChung.Bien.TruongKhoaDuoc;
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
            if (DungChung.Bien.MaBV == "20001")
            {
                //xrTableCell26.Text = "(Hoặc phụ trách bộ phận có nhu cầu nhập)";
                //xrTableCell75.Text = "(Ký, họ tên)";
                //xrLabel10.Visible = true;
            }
            else
            {
                xrLabel10.Visible = false;
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
            else
                colHanDung.Text = "";

            if (DungChung.Bien.MaBV == "20001")
            {
                xrTable1.Visible = false;
            }
            else
            {
                xrTable4.Visible = false;
            }
        }


        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = txtTenCQ2.Text = "Đơn vị: " + DungChung.Bien.TenCQ;
            txtDiaChi.Text = txtDiaChi2.Text = "Địa chỉ: " + DungChung.Bien.DiaChi;
            txtMaDV.Text = txtMaDV2.Text = "Mã đơn vị có quan hệ với ngân sách: " + DungChung.Bien.MaNSach;
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "12121") 
            {
                GroupFooter1.Visible = true;
                ReportFooter.Visible = false;
                colgiamdoc.Text = DungChung.Bien.GiamDoc;
                colketoantruong.Text = DungChung.Bien.KeToanTruong;
                colthukho.Text = DungChung.Bien.ThuKho;
            }
            if (DungChung.Bien.MaBV == "20001")
            {
                xrLabel3.Text = "Mẫu số C30 - HD";
                xrLabel3.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
                xrLabel20.Text = "Theo:";
                xrLabel19.Text = "Nhập tại kho:";
                xrLabel8.Visible = xrLabel9.Visible = xrLabel60.Visible = xrLabel61.Visible = false;
                
                xrLabel58.Visible = xrLabel59.Visible = false;
                xrLabel12.Visible = xrLabel14.Visible = true; 
                xrLabel12.Text = "Địa điểm:";
                txtMaDV.Visible = true;
                SubBand3.Visible = true;
            }
            else
            {
                SubBand3.Visible = false;
            }

            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }

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
                        if(_slct.ToString().Length==1)
                        colSLCT.Text = "0"+_slct.ToString();
                       else colSLCT.Text = _slct.ToString();
                    }
                    else colSLCT.Text = "";
                }
            }
        }
        string tongtien = "";
        private void coltongtien_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void coltongtien_AfterPrint(object sender, EventArgs e)
        {
            //tongtien = coltongtien.Text;
        }

        private void colThanhTienCong_AfterPrint(object sender, EventArgs e)
        {
            tongtien = coltongtien.Text;
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
                PageHeader.Visible = false;
            else
                PageHeader.Visible = true;
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
            {
                GroupFooter1.Visible = false;
            }
        }


     
    }
}
