﻿using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_79a_HD_1399_BHYT : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_79a_HD_1399_BHYT()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHAGF1.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHAGF2.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHARF.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colcongkham.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colcongkhamGF1.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colcongkhamGF2.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colcongkhamRF.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYT.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYTGF1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYTGF2.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYTRF.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham2.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham3.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham4.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colDVKT_tl.DataBindings.Add("Text", DataSource, "DVKT_tl").FormatString = DungChung.Bien.FormatString[1];
            colDVKT_tl_GF2.DataBindings.Add("Text", DataSource, "DVKT_tl").FormatString = DungChung.Bien.FormatString[1];
            colDVKT_tl_GF1.DataBindings.Add("Text", DataSource, "DVKT_tl").FormatString = DungChung.Bien.FormatString[1];
            colDVKT_tl_RF.DataBindings.Add("Text", DataSource, "DVKT_tl").FormatString = DungChung.Bien.FormatString[1];
            txtGioitinh.DataBindings.Add("Text", DataSource, "GTinh");
            colHotenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colMabenh.DataBindings.Add("Text", DataSource, "MaICD");
            colMaCS.DataBindings.Add("Text", DataSource, "MaCS");
            colMau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauGF1.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauGF2.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauRF.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMStheBHYT.DataBindings.Add("Text", DataSource, "SThe");
            txtNamsinh.DataBindings.Add("Text", DataSource, "NSinh");
            colNgaykham.DataBindings.Add("Text", DataSource, "Ngaykham").FormatString = "{0:dd/MM/yy}";
            colNguoibenhchitra.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraRF.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraGF1.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraGF2.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            //colTennhomGF2.DataBindings.Add("Text", DataSource, "Tennhom");
            colThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocGF1.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocGF2.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocRF.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colVTYT_tl_GF1.DataBindings.Add("Text", DataSource, "VTYT_tl").FormatString = DungChung.Bien.FormatString[1];
            colVTYT_tl_GF2.DataBindings.Add("Text", DataSource, "VTYT_tl").FormatString = DungChung.Bien.FormatString[1];
            colVTYT_tl_RF.DataBindings.Add("Text", DataSource, "VTYT_tl").FormatString = DungChung.Bien.FormatString[1];
            colThuoc_tl_RF.DataBindings.Add("Text", DataSource, "VTYT_tl").FormatString = DungChung.Bien.FormatString[1];
            colCPVC.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPVCGF1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPVCGF2.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPVCRF.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTongcong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYT.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTGF1.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTGF2.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTRF.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGF1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGF2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongcongRF2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTGF1.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTGF2.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTRF.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTuyen.DataBindings.Add("Text", DataSource, "Tuyen").FormatString = DungChung.Bien.FormatString[1];
            colThuoc_tl.DataBindings.Add("Text", DataSource, "Thuoc_tl").FormatString = DungChung.Bien.FormatString[1];
            colThuoc_tl_GF1.DataBindings.Add("Text", DataSource, "Thuoc_tl").FormatString = DungChung.Bien.FormatString[1];
            colThuoc_tl_GF2.DataBindings.Add("Text", DataSource, "Thuoc_tl").FormatString = DungChung.Bien.FormatString[1];
            colThuoc_tl_RF.DataBindings.Add("Text", DataSource, "Thuoc_tl").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham1.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham2.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham3.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham4.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colVTYT.DataBindings.Add("Text", DataSource, "VTYTH").FormatString = DungChung.Bien.FormatString[1];
            colVTYTGF1.DataBindings.Add("Text", DataSource, "VTYTH").FormatString = DungChung.Bien.FormatString[1];
            colVTYTGF2.DataBindings.Add("Text", DataSource, "VTYTH").FormatString = DungChung.Bien.FormatString[1];
            colVTYTRF.DataBindings.Add("Text", DataSource, "VTYTH").FormatString = DungChung.Bien.FormatString[1];
            colxetnghiem.DataBindings.Add("Text", DataSource, "xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            ColXetnghiemGF1.DataBindings.Add("Text", DataSource, "xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemGF2.DataBindings.Add("Text", DataSource, "xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemRF.DataBindings.Add("Text", DataSource, "xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            txtNoiTinh.DataBindings.Add("Text", DataSource, "NoiTinh").FormatString = DungChung.Bien.FormatString[1];
            txtMabn.DataBindings.Add("Text", DataSource, "Mabn");
            GroupHeader2.GroupFields.Add(new GroupField("NoiTinh"));
            GroupHeader1.GroupFields.Add(new GroupField("Tuyen"));
        }
        string tongcong = " Tổng cộng ";
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NoiTinh") != null)
            {
                string noitinh = this.GetCurrentColumnValue("NoiTinh").ToString();
                switch (noitinh)
                {
                    case "1":
                        colNhomBNG1.Text = " Bệnh nhân nội tỉnh kcb ban đầu".ToUpper();
                        colSTTG1.Text = "A";
                        tongcong += "A";
                        colTennhomGF2.Text = " Cộng: A";
                        break;
                    case "2":
                        colNhomBNG1.Text = " Bệnh nhân nội tỉnh đến".ToUpper();
                        colSTTG1.Text = "B";
                        tongcong += "+B";
                        colTennhomGF2.Text = " Cộng: B";
                        break;
                    case "3":
                        colNhomBNG1.Text = " Bệnh nhân ngoại tỉnh đến".ToUpper();
                        colSTTG1.Text = "C";
                        tongcong += "+C";
                        colTennhomGF2.Text = " Cộng: C";
                        break;
                }
            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Tuyen") != null)
            {
                int sttg2 = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
                if (sttg2 == 2)
                {
                    colSoTTg2.Text = "II";
                }
                else
                {
                    if (sttg2 == 1)
                    {
                        colSoTTg2.Text = "I";
                    }
                }
            }
            if (this.GetCurrentColumnValue("Tuyen") != null)
            {
                int tuyen = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
                //int tt;
                //if (tuyen == 1)
                //{ tt = 0; }
                //else
                //{ tt = 1; }
                if (tuyen == 2)
                {
                    colTuyenGrp2.Text = " Trái tuyến";
                    colTennhomGF1.Text = " Cộng trái tuyến";
                }
                if (tuyen == 1)
                {
                    colTuyenGrp2.Text = " Đúng tuyến";
                    colTennhomGF1.Text = " Cộng đúng tuyến";
                }
            }
        }

        private void colNamsinh_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("GTinh") != null && this.GetCurrentColumnValue("NSinh") != null)
            {
                int gt = Convert.ToInt32(this.GetCurrentColumnValue("GTinh").ToString());
                string ns = this.GetCurrentColumnValue("NSinh").ToString();
                if (gt == 1)
                {
                    colNamsinh.Text = ns;
                    colGiotinh.Text = "";
                }
                else
                {
                    if (gt == 0)
                    {
                        colNamsinh.Text = "";
                        colGiotinh.Text = ns;
                    }
                    else
                    {
                        colNamsinh.Text = "";
                        colGiotinh.Text = "";
                    }
                }
            }
            else
            {
                colNamsinh.Text = "";
                colGiotinh.Text = "";
            }
        }

        private void colGiotinh_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("GTinh") != null && this.GetCurrentColumnValue("NSinh") != null)
            {
                int gt = Convert.ToInt32(this.GetCurrentColumnValue("GTinh").ToString());
                string ns = this.GetCurrentColumnValue("NSinh").ToString();
                if (gt == 1)
                {
                    colNamsinh.Text = ns;
                    colGiotinh.Text = "";
                }
                else
                {
                    if (gt == 0)
                    {
                        colNamsinh.Text = "";
                        colGiotinh.Text = ns;
                    }
                    else
                    {
                        colNamsinh.Text = "";
                        colGiotinh.Text = "";
                    }
                }
            }
            else
            {
                colNamsinh.Text = "";
                colGiotinh.Text = "";
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtMaCS.Text = DungChung.Bien.MaBV.ToUpper();
            if (_dt != "BHYT")
            {
                GroupFooter1.Visible = false;
                GroupFooter2.Visible = false;
                GroupHeader1.Visible = false;
                GroupHeader2.Visible = false;
                txtTieuDe.Text = "DANH SÁCH NGƯỜI BỆNH NHÂN DÂN KHÁM CHỮA BỆNH NGOẠI TRÚ";
            }
        }
        string _dt = "BHYT";
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTongcong.Text = tongcong;
            //Nguoilapbieu.Value = "Dinh";
            //if (this.GetCurrentColumnValue("TienBH") != null)
            //{
            double tien = 0;
            if(SotienDKTT.Value!=null)
            tien = Convert.ToDouble(this.SotienDKTT.Value);
                txtSoTien.Text = "Tổng số tiền thanh toán (viết bằng chữ): " +QLBV_Library.QLBV_Ham.DocTienBangChu(tien, " đồng.");
            //}
        }


    }
}
