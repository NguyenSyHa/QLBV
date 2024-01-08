using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class repPhieuNhapKho_12001 : DevExpress.XtraReports.UI.XtraReport
    {
        List<QLBV.FormNhap.usNhapDuoc.PhieuNhap_12001> _kq = new List<FormNhap.usNhapDuoc.PhieuNhap_12001>();
        public repPhieuNhapKho_12001( List<QLBV.FormNhap.usNhapDuoc.PhieuNhap_12001> a )
        {
            InitializeComponent();
            _kq = a;
        }

        //public void BindingData()
        //{
        //    colTenDuoc.DataBindings.Add("Text", DataSource, "TenDuoc");
        //    colSoLo.DataBindings.Add("Text", DataSource, "MaQD");
        //    //colHanDung1.DataBindings.Add("Text", DataSource, "HanDung").ToString();
        //    colDVT.DataBindings.Add("Text", DataSource, "DonViTinh");
        //    colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
        //    colSLTN.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
        //    colSLCT.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
        //    colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
        //    celtongtien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
        //}

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //double tongtien = 0;
            //if(TongTien.Value!=null && TongTien.Value.ToString()!="")
            //tongtien =Convert.ToDouble(TongTien.Value);
            //colSoTienchu.Text = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(tongtien," đồng!");
            //colNhapNgay.Text = "Nhập, " + Ngay.Value;
            //colNLB.Text = DungChung.Bien.NguoiLapBieu;
            //colKTT.Text = DungChung.Bien.KeToanTruong;
            //colThuKho01.Text = DungChung.Bien.ThuKho;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (GetCurrentColumnValue("HanDung") != null && GetCurrentColumnValue("HanDung").ToString() != "")
            //{
            //    DateTime dt;
            //    if (DateTime.TryParse(GetCurrentColumnValue("HanDung").ToString(), out dt))
            //    {
            //        colHanDung.Text = dt.Day + "/" + dt.Month + "/" + dt.Year;
            //    }
            //}
        }
        string tenkho = "";
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtDiaChi.Text = DungChung.Bien.TenCQ.ToUpper();
            if (tenkho != null)
                tenkho = TenKho.Value.ToString();
            if (tenkho.ToLower().Contains("chương trình sốt rét") || tenkho.ToLower().Contains("chương trình lao"))
            {
                QLBV.BaoCao.rep_subPhieuNhap_12001_KhoSotRet rep = new rep_subPhieuNhap_12001_KhoSotRet();
                xrSubreport1.ReportSource = rep;
                rep.DataSource = _kq;
                rep.BindingData();
            }
            else if (tenkho.ToLower().Contains("chương trình tiêm chủng"))
            {
                //QLBV.BaoCao.rep_subPhieuNhap_12001_KhoTiemChung repSub = (QLBV.BaoCao.rep_subPhieuNhap_12001_KhoTiemChung)xrSubreport1.ReportSource;
                //repSub.DataSource = _kq;
                //repSub.BindingData();
                QLBV.BaoCao.rep_subPhieuNhap_12001_KhoTiemChung rep = new rep_subPhieuNhap_12001_KhoTiemChung();
                xrSubreport1.ReportSource = rep;
                rep.DataSource = _kq;
                rep.BindingData();
            }
            else
            {
                //QLBV.BaoCao.rep_subPhieuNhap_12001 repSub = (QLBV.BaoCao.rep_subPhieuNhap_12001)xrSubreport1.ReportSource;
                //repSub.DataSource = _kq;
                //repSub.BindingData();
                QLBV.BaoCao.rep_subPhieuNhap_12001 rep = new rep_subPhieuNhap_12001();
                xrSubreport1.ReportSource = rep;
                rep.DataSource = _kq;
                rep.BindingData();
            }
            //txtMaDV.Text = "Mã đơn vị có quan hệ với ngân sách: " + DungChung.Bien.MaNSach;
        }

        private void colSLCT_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "30003")
            //{
            //    if (this.GetCurrentColumnValue("SoLuongN") != null)
            //    {
            //        double _slct = Convert.ToDouble(this.GetCurrentColumnValue("SoLuongN"));
            //        if (_slct >= 0)
            //        {
            //            if (_slct.ToString().Length == 1)
            //                colSLCT.Text = "0" + _slct.ToString();
            //            else colSLCT.Text = _slct.ToString();
            //        }
            //        else colSLCT.Text = "";
            //    }
            //}
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (tenkho.ToLower().Contains("chương trình sốt rét") || tenkho.ToLower().Contains("chương trình lao"))
            {
                rep_subPhieuNhap_12001_KhoSotRet repSub = (rep_subPhieuNhap_12001_KhoSotRet)xrSubreport1.ReportSource;
                repSub.DataSource = _kq;
                repSub.BindingData();
            }
            else if (tenkho.ToLower().Contains("chương trình tiêm chủng"))
            {
                rep_subPhieuNhap_12001_KhoTiemChung repSub = (rep_subPhieuNhap_12001_KhoTiemChung)xrSubreport1.ReportSource;
                repSub.DataSource = _kq;
                repSub.BindingData();
            }
            else
            {
                rep_subPhieuNhap_12001 repSub = (rep_subPhieuNhap_12001)xrSubreport1.ReportSource;
                repSub.DataSource = _kq;
                repSub.BindingData();
            }
        }



    }
}
