using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repPhieuNhapKho24009 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuNhapKho24009()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDuoc.DataBindings.Add("Text", DataSource, "TenDuoc");
            colSoLo.DataBindings.Add("Text", DataSource, "SoLo");
            colHanDung.DataBindings.Add("Text", DataSource, "HanDung").FormatString="{0:dd/MM/yyyy}";
            colDVT.DataBindings.Add("Text", DataSource, "DonViTinh");
            colMaDuoc.DataBindings.Add("Text", DataSource, "MaTam");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSLTN.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString=DungChung.Bien.FormatString[0];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
           
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            double tongtien = 0;
            if(TongTien.Value!=null && TongTien.Value.ToString()!="")
            tongtien =Convert.ToDouble(TongTien.Value);
            colSoTienchu.Text = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(tongtien," đồng!");
            colNhapNgay.Text = "Nhập, " + Ngay.Value;
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
            colKTT.Text = DungChung.Bien.KeToanTruong;
            colThuKho01.Text = DungChung.Bien.ThuKho;
            if (DungChung.Bien.MaBV == "02005")
            {
                xrTableKyTen.Visible = false;
                xrTableViXuyen.Visible = true;
                lab1.Text = "Thủ trưởng đơn vị"; lab2.Text = "Kế toán trưởng"; lab3.Text = "Người lập biểu"; lab4.Text = "Người giao hàng"; lab5.Text = "Thủ kho";
                col1.Text = DungChung.Bien.GiamDoc; col2.Text = DungChung.Bien.KeToanTruong; col3.Text = DungChung.Bien.NguoiLapBieu; col5.Text = DungChung.Bien.ThuKho;
            }
            if (DungChung.Bien.MaBV == "30003")
            {
                xrTableKyTen.Visible = false;
                xrTableViXuyen.Visible = true;
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
                    colHanDung.Text = dt.Day +"/"+ dt.Month+"/"+ dt.Year;
                }
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text ="Đơn vị: "+ DungChung.Bien.TenCQ;
            txtDiaChi.Text ="Địa chỉ: "+ DungChung.Bien.DiaChi;
            txtMaDV.Text ="Mã đơn vị có quan hệ với ngân sách: "+ DungChung.Bien.MaNSach;
        }

        private void colSLCT_BeforePrint(object sender, CancelEventArgs e)
        {
         
        }


     
    }
}
