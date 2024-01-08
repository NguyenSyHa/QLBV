using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repBcNXTXuat_BG : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNXTXuat_BG()
        {
            InitializeComponent();
        }
        string _kho="";
        public repBcNXTXuat_BG(string k)
        {
            InitializeComponent();
            _kho = k;
        }
        public void BindingData()
        {
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
           // txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSLX1.DataBindings.Add("Text", DataSource, "SL1");//.FormatString = DungChung.Bien.FormatString[1];
            colSLX2.DataBindings.Add("Text", DataSource, "SL2");//.FormatString = DungChung.Bien.FormatString[1];
            colSLX3.DataBindings.Add("Text", DataSource, "SL3");//.FormatString = DungChung.Bien.FormatString[1];
            colSLX4.DataBindings.Add("Text", DataSource, "SL4").FormatString = DungChung.Bien.FormatString[0];
            colSLX5.DataBindings.Add("Text", DataSource, "SL5").FormatString = DungChung.Bien.FormatString[0];
            colSLX6.DataBindings.Add("Text", DataSource, "SL6").FormatString = DungChung.Bien.FormatString[0];
            colSLX7.DataBindings.Add("Text", DataSource, "SL7").FormatString = DungChung.Bien.FormatString[0];
            colSLX8.DataBindings.Add("Text", DataSource, "SL8").FormatString = DungChung.Bien.FormatString[0];
            colSLX9.DataBindings.Add("Text", DataSource, "SL9").FormatString = DungChung.Bien.FormatString[0];
            colSLX10.DataBindings.Add("Text", DataSource, "SL10").FormatString = DungChung.Bien.FormatString[0];
            colSLX11.DataBindings.Add("Text", DataSource, "SL11").FormatString = DungChung.Bien.FormatString[0];
            colSLX12.DataBindings.Add("Text", DataSource, "SL12").FormatString = DungChung.Bien.FormatString[0];
            colSLX13.DataBindings.Add("Text", DataSource, "SL13").FormatString = DungChung.Bien.FormatString[0];
            colSLX14.DataBindings.Add("Text", DataSource, "SL14").FormatString = DungChung.Bien.FormatString[0];
            colSLX15.DataBindings.Add("Text", DataSource, "SL15").FormatString = DungChung.Bien.FormatString[0];
            colSLX16.DataBindings.Add("Text", DataSource, "SL16").FormatString = DungChung.Bien.FormatString[0];
            colSLX17.DataBindings.Add("Text", DataSource, "SL17").FormatString = DungChung.Bien.FormatString[0];
            colSLX18.DataBindings.Add("Text", DataSource, "SL18").FormatString = DungChung.Bien.FormatString[0];
            colSLX19.DataBindings.Add("Text", DataSource, "SL").FormatString = DungChung.Bien.FormatString[0];
            colTTX19.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
            colTTXT19.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];

     
        }
             QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
       
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //DateTime tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            //DateTime denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));

            //int _madv = 0; double _dongia = 0; string _dvt = "";//string _ten14 = ""; string _ten15 = ""; string _ten16 = ""; string _ten17 = "";
            //if (this.GetCurrentColumnValue("MaDV") != null)
            //{
            //    _madv = this.GetCurrentColumnValue("MaDV").ToString();
            //}
            //if (this.GetCurrentColumnValue("DonVi") != null)
            //{
            //    _dvt = this.GetCurrentColumnValue("DonVi").ToString();
            //}
            //if (this.GetCurrentColumnValue("DonGia") != null)
            //{
            //    _dongia =Convert.ToDouble(this.GetCurrentColumnValue("DonGia"));
            //}
            //double a1 = 0; double a2 = 0; double a3 = 0; double a4 = 0; double a5 = 0;
            //if (Convert.ToInt32(MaNCC1.Value)==-1)
            //{
            //    string _nhacc = MaNCC.Value.ToString();
            //    string xnt = Ten14.Value.ToString();
            //    if (Ten14.Value == "Xuất ngoại trú")
            //    {
            //        var qnxt0 = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2 && (p.KieuDon == 0)).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.MaKP == _kho)//.Where(p => p.MaKP== (_kho))
            //                     join nhapdct in data.NhapDcts.Where(p => p.MaDV == _madv && p.DonVi == _dvt && p.DonGia == _dongia) on nhapd.IDNhap equals nhapdct.IDNhap
            //                     join dv in data.DichVus.Where(p => p.MaCC == _nhacc) on nhapdct.MaDV equals dv.MaDV
            //                     group new { nhapd, nhapdct } by new { nhapd.MaKP, nhapdct.MaDV } into kq
            //                     select new
            //                     {
            //                         SLXNT = kq.Sum(p => p.nhapdct.SoLuongX),
            //                     }).ToList();
            //        if ((qnxt0.Count > 0))
            //        {

            //            a1 = qnxt0.First().SLXNT.Value;
            //            colSLX14.Text = a1.ToString("##,###");
            //        }
            //        else
            //            colSLX14.Text = "";
            //    }
            //     if (Ten15.Value == "Xuất xã phường")
            //    {
            //        var qnxt3 = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2 && p.KieuDon == 3).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.MaKP == _kho)//.Where(p => p.MaKP== (_kho))
            //                     join nhapdct in data.NhapDcts.Where(p => p.MaDV == _madv && p.DonVi == _dvt && p.DonGia == _dongia) on nhapd.IDNhap equals nhapdct.IDNhap
            //                     join dv in data.DichVus.Where(p => p.MaCC == _nhacc) on nhapdct.MaDV equals dv.MaDV
            //                     group new { nhapd, nhapdct } by new { nhapd.MaKP, nhapdct.MaDV } into kq
            //                     select new
            //                     {
            //                         SLXNBV = kq.Sum(p => p.nhapdct.SoLuongX),
            //                     }).ToList();
            //        if ((qnxt3.Count > 0))
            //        {

            //            a2 = qnxt3.First().SLXNBV.Value;
            //            colSLX15.Text = a2.ToString("##,###");
            //        }
            //        else
            //            colSLX15.Text = "";
            //    }
            //    if (Ten16.Value == "Xuất nhân dân")
            //    {
            //        var qnxt4 = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2 && (p.KieuDon == 4)).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.MaKP == _kho)//.Where(p => p.MaKP== (_kho))
            //                     join nhapdct in data.NhapDcts.Where(p => p.MaDV == _madv && p.DonVi == _dvt && p.DonGia == _dongia) on nhapd.IDNhap equals nhapdct.IDNhap
            //                     join dv in data.DichVus.Where(p => p.MaCC == _nhacc) on nhapdct.MaDV equals dv.MaDV
            //                     group new { nhapd, nhapdct } by new { nhapd.MaKP, nhapdct.MaDV } into kq
            //                     select new
            //                     {
            //                         SLXND = kq.Sum(p => p.nhapdct.SoLuongX),
            //                     }).ToList();
            //        if ((qnxt4.Count > 0))
            //        {

            //            a3 = qnxt4.First().SLXND.Value;
            //            colSLX16.Text = a3.ToString("##,###");
            //        }
            //        else
            //            colSLX16.Text = "";
            //    }
            //    if (Ten17.Value == "Xuất Cận Lâm sàng")
            //    {
            //        var qnxt5 = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2 && (p.KieuDon == 5)).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.MaKP == _kho)//.Where(p => p.MaKP== (_kho))
            //                     join nhapdct in data.NhapDcts.Where(p => p.MaDV == _madv && p.DonVi == _dvt && p.DonGia == _dongia) on nhapd.IDNhap equals nhapdct.IDNhap
            //                     join dv in data.DichVus.Where(p => p.MaCC == _nhacc) on nhapdct.MaDV equals dv.MaDV
            //                     group new { nhapd, nhapdct } by new { nhapd.MaKP, nhapdct.MaDV } into kq
            //                     select new
            //                     {
            //                         SLXCLS = kq.Sum(p => p.nhapdct.SoLuongX),
            //                     }).ToList();
            //        if ((qnxt5.Count > 0))
            //        {

            //            a4 = qnxt5.First().SLXCLS.Value;
            //            colSLX17.Text = a4.ToString("##,###");
            //        }
            //        else
            //            colSLX17.Text = "";
            //    }
            //    if (Ten17.Value == "Xuất Tủ trực")
            //    {
            //        var qnxt6 = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2 && (p.KieuDon == 6)).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.MaKP == _kho)//.Where(p => p.MaKP== (_kho))
            //                     join nhapdct in data.NhapDcts.Where(p => p.MaDV == _madv && p.DonVi == _dvt && p.DonGia == _dongia) on nhapd.IDNhap equals nhapdct.IDNhap
            //                     join dv in data.DichVus.Where(p => p.MaCC == _nhacc) on nhapdct.MaDV equals dv.MaDV
            //                     group new { nhapd, nhapdct } by new { nhapd.MaKP, nhapdct.MaDV } into kq
            //                     select new
            //                     {
            //                         SLXTT = kq.Sum(p => p.nhapdct.SoLuongX),
            //                     }).ToList();
            //        if ((qnxt6.Count > 0))
            //        {

            //            a5 = qnxt6.First().SLXTT.Value;
            //            colSLX18.Text = a5.ToString("##,###");
            //        }
            //        else
            //            colSLX18.Text = "";

            //    }
            //}
            //else
            //{
            //    if (Ten14.Value == "Xuất ngoại trú")
            //    {
            //        var qnxt0 = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2 && (p.KieuDon == 0)).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.MaKP == _kho)//.Where(p => p.MaKP== (_kho))
            //                     join nhapdct in data.NhapDcts.Where(p => p.MaDV == _madv && p.DonVi == _dvt && p.DonGia == _dongia) on nhapd.IDNhap equals nhapdct.IDNhap
            //                     group new { nhapd, nhapdct } by new { nhapd.MaKP, nhapdct.MaDV } into kq
            //                     select new
            //                     {
            //                         SLXNT = kq.Sum(p => p.nhapdct.SoLuongX),
            //                     }).ToList();
            //        if ((qnxt0.Count > 0))
            //        {

            //            a1 = qnxt0.First().SLXNT.Value;
            //            colSLX14.Text = a1.ToString("##,###");
            //        }
            //        else
            //            colSLX14.Text = "";
            //    }
            //    if (Ten15.Value == "Xuất xã phường")
            //    {
            //        var qnxt3 = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2 && p.KieuDon == 3).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.MaKP == _kho)//.Where(p => p.MaKP== (_kho))
            //                     join nhapdct in data.NhapDcts.Where(p => p.MaDV == _madv && p.DonVi == _dvt && p.DonGia == _dongia) on nhapd.IDNhap equals nhapdct.IDNhap
            //                     group new { nhapd, nhapdct } by new { nhapd.MaKP, nhapdct.MaDV } into kq
            //                     select new
            //                     {
            //                         SLXNBV = kq.Sum(p => p.nhapdct.SoLuongX),
            //                     }).ToList();
            //        if ((qnxt3.Count > 0))
            //        {

            //            a2 = qnxt3.First().SLXNBV.Value;
            //            colSLX15.Text = a2.ToString("##,###");
            //        }
            //        else
            //            colSLX15.Text = "";
            //    }
            //    if (Ten16.Value == "Xuất nhân dân")
            //    {
            //        var qnxt4 = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2 && (p.KieuDon == 4)).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.MaKP == _kho)//.Where(p => p.MaKP== (_kho))
            //                     join nhapdct in data.NhapDcts.Where(p => p.MaDV == _madv && p.DonVi == _dvt && p.DonGia == _dongia) on nhapd.IDNhap equals nhapdct.IDNhap
            //                     group new { nhapd, nhapdct } by new { nhapd.MaKP, nhapdct.MaDV } into kq
            //                     select new
            //                     {
            //                         SLXND = kq.Sum(p => p.nhapdct.SoLuongX),
            //                     }).ToList();
            //        if ((qnxt4.Count > 0))
            //        {

            //            a3 = qnxt4.First().SLXND.Value;
            //            colSLX16.Text = a3.ToString("##,###");
            //        }
            //        else
            //            colSLX16.Text = "";
            //    }
            //    if (Ten17.Value == "Xuất Cận Lâm sàng")
            //    {
            //        var qnxt5 = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2 && (p.KieuDon == 5)).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.MaKP == _kho)//.Where(p => p.MaKP== (_kho))
            //                     join nhapdct in data.NhapDcts.Where(p => p.MaDV == _madv && p.DonVi == _dvt && p.DonGia == _dongia) on nhapd.IDNhap equals nhapdct.IDNhap
            //                     group new { nhapd, nhapdct } by new { nhapd.MaKP, nhapdct.MaDV } into kq
            //                     select new
            //                     {
            //                         SLXCLS = kq.Sum(p => p.nhapdct.SoLuongX),
            //                     }).ToList();
            //        if ((qnxt5.Count > 0))
            //        {

            //            a4 = qnxt5.First().SLXCLS.Value;
            //            colSLX17.Text = a4.ToString("##,###");
            //        }
            //        else
            //            colSLX17.Text = "";
            //    }
            //    if (Ten17.Value == "Xuất Tủ trực")
            //    {
            //        var qnxt6 = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2 && (p.KieuDon == 6)).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.MaKP == _kho)//.Where(p => p.MaKP== (_kho))
            //                     join nhapdct in data.NhapDcts.Where(p => p.MaDV == _madv && p.DonVi == _dvt && p.DonGia == _dongia) on nhapd.IDNhap equals nhapdct.IDNhap
            //                     group new { nhapd, nhapdct } by new { nhapd.MaKP, nhapdct.MaDV } into kq
            //                     select new
            //                     {
            //                         SLXTT = kq.Sum(p => p.nhapdct.SoLuongX),
            //                     }).ToList();
            //        if ((qnxt6.Count > 0))
            //        {

            //            a5 = qnxt6.First().SLXTT.Value;
            //            colSLX18.Text = a5.ToString("##,###");
            //        }
            //        else
            //            colSLX18.Text = "";

            //    }
            //}
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //DateTime tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            //DateTime denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            //var qnxt = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.MaKP == _kho)//.Where(p => p.MaKP== (_kho))
            //            join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
            //            //Where (kp.TenKP.Contains(_kho)||kp.PLoai=="Xã phường") 
            //            group new { nhapd, nhapdct } by new { nhapd.KieuDon, nhapdct.MaDV } into kq
            //            select new
            //            {
            //                SLXXP = kq.Sum(p => p.nhapdct.SoLuongX),
            //                KieuDon = Convert.ToInt32(kq.Key.KieuDon),
            //            }).ToList();
            //if (qnxt.Where(p=>p.KieuDon=0).Count > 0)
            //    colTen14.Text = "Xuất ngoại trú";
        }
    }
}
