﻿using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_79aTH_1399_TienGiuong : DevExpress.XtraReports.UI.XtraReport
    {
        public bool HtTuyen = true;
        List<DTuong> _ldtuong = new List<DTuong>();
        int _a = 0;
        public Rep_79aTH_1399_TienGiuong(int a)
        {
            InitializeComponent();
            QLBV_Database.QLBVEntities _data=new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _a = a;
            _ldtuong = _data.DTuongs.ToList();
        }
        public void BindingData(int a)
        {
           
            colCDHAGF1.DataBindings.Add("Text", DataSource, "T_cdha").FormatString = DungChung.Bien.FormatString[1];
            colCDHAGF2.DataBindings.Add("Text", DataSource, "T_cdha").FormatString = DungChung.Bien.FormatString[1];
            colCDHARF.DataBindings.Add("Text", DataSource, "T_cdha").FormatString = DungChung.Bien.FormatString[1];
          
            colcongkhamGF1.DataBindings.Add("Text", DataSource, "T_kham").FormatString = DungChung.Bien.FormatString[1];
            colcongkhamGF2.DataBindings.Add("Text", DataSource, "T_kham").FormatString = DungChung.Bien.FormatString[1];
            colcongkhamRF.DataBindings.Add("Text", DataSource, "T_kham").FormatString = DungChung.Bien.FormatString[1];
          
            colCPNgoaiBHYTGF1.DataBindings.Add("Text", DataSource, "T_vchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYTGF2.DataBindings.Add("Text", DataSource, "T_vchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYTRF.DataBindings.Add("Text", DataSource, "T_vchuyen").FormatString = DungChung.Bien.FormatString[1];
           
            colDVKT_tl_GF2.DataBindings.Add("Text", DataSource, "T_dvkt_tyle").FormatString = DungChung.Bien.FormatString[1];
            colDVKT_tl_GF1.DataBindings.Add("Text", DataSource, "T_dvkt_tyle").FormatString = DungChung.Bien.FormatString[1];
            colDVKT_tl_RF.DataBindings.Add("Text", DataSource, "T_dvkt_tyle").FormatString = DungChung.Bien.FormatString[1];

            colTienGiuong.DataBindings.Add("Text", DataSource, "T_giuong").FormatString = DungChung.Bien.FormatString[1];
            colTienGiuongGF2.DataBindings.Add("Text", DataSource, "T_giuong").FormatString = DungChung.Bien.FormatString[1];
            colTienGiuongR.DataBindings.Add("Text", DataSource, "T_giuong").FormatString = DungChung.Bien.FormatString[1];
           
           
            colMauGF1.DataBindings.Add("Text", DataSource, "T_mau").FormatString = DungChung.Bien.FormatString[1];
            colMauGF2.DataBindings.Add("Text", DataSource, "T_mau").FormatString = DungChung.Bien.FormatString[1];
            colMauRF.DataBindings.Add("Text", DataSource, "T_mau").FormatString = DungChung.Bien.FormatString[1];
           
            colNguoibenhchitraRF.DataBindings.Add("Text", DataSource, "T_bntt").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraGF1.DataBindings.Add("Text", DataSource, "T_bntt").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraGF2.DataBindings.Add("Text", DataSource, "T_bntt").FormatString = DungChung.Bien.FormatString[1];
           // colTennhomGF2.DataBindings.Add("Text", DataSource, "Tennhom");
           
            colThuocGF1.DataBindings.Add("Text", DataSource, "T_thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocGF2.DataBindings.Add("Text", DataSource, "T_thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocRF.DataBindings.Add("Text", DataSource, "T_thuoc").FormatString = DungChung.Bien.FormatString[1];
            colVTYT_tl_GF1.DataBindings.Add("Text", DataSource, "T_vtyt_tyle").FormatString = DungChung.Bien.FormatString[1];
            colVTYT_tl_GF2.DataBindings.Add("Text", DataSource, "T_vtyt_tyle").FormatString = DungChung.Bien.FormatString[1];
            colVTYT_tl_RF.DataBindings.Add("Text", DataSource, "T_vtyt_tyle").FormatString = DungChung.Bien.FormatString[1];
           
            colCPVCGF1.DataBindings.Add("Text", DataSource, "T_vchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPVCGF2.DataBindings.Add("Text", DataSource, "T_vchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPVCRF.DataBindings.Add("Text", DataSource, "T_vchuyen").FormatString = DungChung.Bien.FormatString[1];
           
           
            colTongcongBHYTGF1.DataBindings.Add("Text", DataSource, "T_bhtt").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTGF2.DataBindings.Add("Text", DataSource, "T_bhtt").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTRF.DataBindings.Add("Text", DataSource, "T_bhtt").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGF1.DataBindings.Add("Text", DataSource, "Thanhtien").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGF2.DataBindings.Add("Text", DataSource, "Thanhtien").FormatString = DungChung.Bien.FormatString[1];
            colTongcongRF2.DataBindings.Add("Text", DataSource, "Thanhtien").FormatString = DungChung.Bien.FormatString[1];
          
            colTTPTGF1.DataBindings.Add("Text", DataSource, "T_pttt").FormatString = DungChung.Bien.FormatString[1];
            colTTPTGF2.DataBindings.Add("Text", DataSource, "T_pttt").FormatString = DungChung.Bien.FormatString[1];
            colTTPTRF.DataBindings.Add("Text", DataSource, "T_pttt").FormatString = DungChung.Bien.FormatString[1];
           // colTuyen.DataBindings.Add("Text", DataSource, "Ma_lydo_vvien").FormatString = DungChung.Bien.FormatString[1];
            
            colThuoc_tl_GF1.DataBindings.Add("Text", DataSource, "T_thuoc_tyle").FormatString = DungChung.Bien.FormatString[1];
            colThuoc_tl_GF2.DataBindings.Add("Text", DataSource, "T_thuoc_tyle").FormatString = DungChung.Bien.FormatString[1];
            colThuoc_tl_RF.DataBindings.Add("Text", DataSource, "T_thuoc_tyle").FormatString = DungChung.Bien.FormatString[1];

            colTTGF1.DataBindings.Add("Text", DataSource, "STT");
            //colcongkham1.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham2.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham3.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham4.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            
            colVTYTGF1.DataBindings.Add("Text", DataSource, "T_vtyt").FormatString = DungChung.Bien.FormatString[1];
            colVTYTGF2.DataBindings.Add("Text", DataSource, "T_vtyt").FormatString = DungChung.Bien.FormatString[1];
            colVTYTRF.DataBindings.Add("Text", DataSource, "T_vtyt").FormatString = DungChung.Bien.FormatString[1];
            
            ColXetnghiemGF1.DataBindings.Add("Text", DataSource, "T_xn").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemGF2.DataBindings.Add("Text", DataSource, "T_xn").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemRF.DataBindings.Add("Text", DataSource, "T_xn").FormatString = DungChung.Bien.FormatString[1];
            
           
            colSoLuotGF1.DataBindings.Add("Text", DataSource, "Soluot");
            colSoLuotGF2.DataBindings.Add("Text", DataSource, "Soluot");
            colSoLuotRF.DataBindings.Add("Text", DataSource, "Soluot");

           // colTuyenGrp2.DataBindings.Add("Text", DataSource, "Tuyen");
            colTennhomGF1.DataBindings.Add("Text", DataSource, "Tuyen");
            if (a != 1)
            {
                txtNoiTinh.DataBindings.Add("Text", DataSource, "NTinh").FormatString = DungChung.Bien.FormatString[1];
                GroupHeader2.GroupFields.Add(new GroupField("NTinh"));
            }
            else
                GroupHeader2.Visible = false;
           // GroupHeader1.GroupFields.Add(new GroupField("Ma_lydo_vvien"));
        }
        string tongcong = " Tổng cộng ";
        
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NTinh") != null)
            {
                string noitinh = this.GetCurrentColumnValue("NTinh").ToString();
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

        //private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    soluot = 0;
        //    if (this.GetCurrentColumnValue("Ma_lydo_vvien") != null)
        //    {
        //        int sttg2 = int.Parse(this.GetCurrentColumnValue("Ma_lydo_vvien").ToString().Trim());
        //        if (sttg2 == 2)
        //        {
        //            colTTGF1.Text = "II";
        //        }
        //        else
        //        {
        //            if (sttg2 == 1)
        //            {
        //                colTTGF1.Text = "I";
        //            }
        //        }
        //    }
        //    if (this.GetCurrentColumnValue("Ma_lydo_vvien") != null)
        //    {
        //        int tuyen = int.Parse(this.GetCurrentColumnValue("Ma_lydo_vvien").ToString().Trim());
        //        if (tuyen == 2)
        //        {
        //            colTuyenGrp2.Text = " Trái tuyến";
        //            colTennhomGF1.Text = " Trái tuyến";
        //        }
        //        if (tuyen == 1)
        //        {
        //            colTuyenGrp2.Text = " Đúng tuyến";
        //            colTennhomGF1.Text = " Đúng tuyến";
        //        }
        //    }
        //}


        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtMaCS.Text = DungChung.Bien.MaBV;
            if (_dt != "BHYT")
            {
               // GroupFooter1.Visible = false;
                GroupFooter2.Visible = false;
              //  GroupHeader1.Visible = false;
                GroupHeader2.Visible = false;
                txtTieuDe.Text = "DANH SÁCH NGƯỜI BỆNH NHÂN DÂN KHÁM CHỮA BỆNH NGOẠI TRÚ";
            }
            Detail.Visible = HtTuyen;
            if (DungChung.Bien.MaBV == "27021")
            {
                xrLabel1.Text = "Mẫu số: C79a – TH";
            }
        }
        string _dt = "BHYT";
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTongcong.Text = tongcong;
            if (DungChung.Bien.MaBV == "30303")
            {
                xrTableCell73.Text = "Trưởng phòng KHTCDD";
               // xrTableCell76.Text = "Phụ trách kế toán";
            }
            if (DungChung.Bien.MaBV == "30350")
            {
                xrTableCell73.Text = "Phụ trách BHYT";
                xrTableCell76.Text = "Kế toán BHYT";
                xrTableCell75.Text = "Bệnh Xá Trưởng";
            }
        }

    
       


    }
}
