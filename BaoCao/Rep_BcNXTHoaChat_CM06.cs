using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BcNXTHoaChat_CM06 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcNXTHoaChat_CM06()
        {
            InitializeComponent();
        }
        bool HThi = true;
        public Rep_BcNXTHoaChat_CM06(bool ht)
        {
            InitializeComponent();
            HThi = ht;
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public void BindingData()
        {
            colTenTieuNhomDV.DataBindings.Add("Text", DataSource, "TenTieuNhomDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");

            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];


            colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];

            colXuatCLSSL.DataBindings.Add("Text", DataSource, "XuatCLSSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatCLSTT.DataBindings.Add("Text", DataSource, "XuatCLSTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatCLSTKSLTong.DataBindings.Add("Text", DataSource, "XuatCLSTT").FormatString = DungChung.Bien.FormatString[1];

            colXuatPKSL.DataBindings.Add("Text", DataSource, "XuatPKSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatPKTT.DataBindings.Add("Text", DataSource, "XuatPKTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatPKTTTong.DataBindings.Add("Text", DataSource, "XuatPKTT").FormatString = DungChung.Bien.FormatString[1];


            colTongXuatSL.DataBindings.Add("Text", DataSource, "TongXuatSL").FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTT.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTTTong.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];

            colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhomDV"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            GroupHeader1.Visible = HThi;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        private void colXuatCLSSL_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            int _madv = 0;
            int _makp = 0;
            string _donvi = "";
            int _dongia = 0;
            _makp = String.IsNullOrEmpty( MaKP.Value.ToString()) ? 0 : Convert.ToInt32(MaKP.Value);
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32( GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("DVT") != null)
                _donvi = GetCurrentColumnValue("DVT").ToString();
            if (GetCurrentColumnValue("DonGia") != null)
                _dongia = int.Parse(this.GetCurrentColumnValue("DonGia").ToString());
            var qnxt = (from nhapd in data.NhapDs
                        join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                        join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
                        join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                        join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                        where (nhomdv.TenNhom.Contains("Hóa chất"))
                        where (kp.PLoai.Contains("Cận lâm sàng"))
                        where (nhapd.MaKP == _makp && nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 1)
                        group new { dv, nhapd, nhapdct } by new { dv.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapdct.SoLuongX } into kq
                        select new
                        {
                            MaDV = kq.Key.MaDV,
                            DVT = kq.Key.DonVi,
                            DonGia = kq.Key.DonGia,
                            SoLuongX = kq.Key.SoLuongX,
                        }).ToList();
            if (qnxt.Count > 0)
            {
                double a = Convert.ToDouble(qnxt.Where(p => p.MaDV == _madv).Where(p => p.DVT == _donvi).Where(p => p.DonGia == _dongia).Sum(p => p.SoLuongX));
                colXuatCLSSL.Text = a.ToString("##,###");
            }
        }

        private void colXuatCLSTT_BeforePrint(object sender, CancelEventArgs e)
        {

            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            int _madv = 0;
            int _makp = 0;
            string _donvi = "";
            int _dongia = 0;
            _makp = String.IsNullOrEmpty(MaKP.Value.ToString()) == null ? 0 : Convert.ToInt32(MaKP.Value);
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32( GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("DVT") != null)
                _donvi = GetCurrentColumnValue("DVT").ToString();
            if (GetCurrentColumnValue("DonGia") != null)
                _dongia = int.Parse(this.GetCurrentColumnValue("DonGia").ToString());
            var qnxt = (from nhapd in data.NhapDs
                        join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                        join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
                        join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                        join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                        where (nhomdv.TenNhom.Contains("Hóa chất"))
                        where (kp.PLoai.Contains("Cận lâm sàng"))
                        where (nhapd.MaKP == _makp && nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 1)
                        group new { dv, nhapd, nhapdct } by new { dv.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapdct.ThanhTienX } into kq
                        select new
                        {
                            MaDV = kq.Key.MaDV,
                            DVT = kq.Key.DonVi,
                            DonGia = kq.Key.DonGia,
                            ThanhTienX = kq.Key.ThanhTienX,
                        }).ToList();
            if (qnxt.Count > 0)
            {
                double a = Convert.ToDouble(qnxt.Where(p => p.MaDV == _madv).Where(p => p.DVT == _donvi).Where(p => p.DonGia == _dongia).Sum(p => p.ThanhTienX));
                colXuatCLSTT.Text = a.ToString("##,###");
            }
        }

        private void colXuatCLSTTTong_BeforePrint(object sender, CancelEventArgs e)
        {
            int _makp = 0;
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            _makp = String.IsNullOrEmpty(MaKP.Value.ToString()) ? 0 : Convert.ToInt32(MaKP.Value);
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));

            var qnxt = (from nhapd in data.NhapDs
                        join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                        join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
                        join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                        join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                        where (nhomdv.TenNhom.Contains("Hóa chất"))
                        where (kp.PLoai.Contains("Cận lâm sàng"))
                        where (nhapd.MaKP == _makp && nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 1)
                        group new { dv, nhapd, nhapdct } by new { nhapdct.ThanhTienX } into kq
                        select new
                        {
                            ThanhTienX = kq.Key.ThanhTienX,
                        }).ToList();
            if (qnxt.Count > 0)
            {
                double a = Convert.ToDouble(qnxt.Sum(p => p.ThanhTienX));
                colXuatCLSTTTong.Text = a.ToString("##,###");
            }
        }

        private void colXuatPKSL_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            int _madv = 0;
            int _makp = 0;
            string _donvi = "";
            int _dongia = 0;
            _makp = String.IsNullOrEmpty(MaKP.Value.ToString()) == null ? 0 : Convert.ToInt32(MaKP.Value);
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32( GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("DVT") != null)
                _donvi = GetCurrentColumnValue("DVT").ToString();
            if (GetCurrentColumnValue("DonGia") != null)
                _dongia = int.Parse(this.GetCurrentColumnValue("DonGia").ToString());
            var qnxt = (from nhapd in data.NhapDs
                        join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                        join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
                        join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                        join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                        where (nhomdv.TenNhom.Contains("Hóa chất"))
                        where (kp.PLoai.Contains("PK khu vực"))
                        where (nhapd.MaKP == _makp && nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 3)
                        group new { dv, nhapd, nhapdct } by new { dv.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapdct.SoLuongX } into kq
                        select new
                        {
                            MaDV = kq.Key.MaDV,
                            DVT = kq.Key.DonVi,
                            DonGia = kq.Key.DonGia,
                            SoLuongX = kq.Key.SoLuongX,
                        }).ToList();
            if (qnxt.Count > 0)
            {
                double a = Convert.ToDouble(qnxt.Where(p => p.MaDV == _madv).Where(p => p.DVT == _donvi).Where(p => p.DonGia == _dongia).Sum(p => p.SoLuongX));
                colXuatPKSL.Text = a.ToString("##,###");
            }
        }

        private void colXuatPKTT_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            int _madv = 0;
            int _makp = 0;
            string _donvi = "";
            int _dongia = 0;
            _makp = String.IsNullOrEmpty(MaKP.Value.ToString()) == null ? 0 : Convert.ToInt32(MaKP.Value);
            if (GetCurrentColumnValue("MaDV") != null)
                _madv =Convert.ToInt32( GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("DVT") != null)
                _donvi = GetCurrentColumnValue("DVT").ToString();
            if (GetCurrentColumnValue("DonGia") != null)
                _dongia = int.Parse(this.GetCurrentColumnValue("DonGia").ToString());
            var qnxt = (from nhapd in data.NhapDs
                        join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                        join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
                        join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                        join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                        where (nhomdv.TenNhom.Contains("Hóa chất"))
                        where (kp.PLoai.Contains("PK khu vực"))
                        where (nhapd.MaKP == _makp && nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 3)
                        group new { dv, nhapd, nhapdct } by new { dv.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapdct.ThanhTienX } into kq
                        select new
                        {
                            MaDV = kq.Key.MaDV,
                            DVT = kq.Key.DonVi,
                            DonGia = kq.Key.DonGia,
                            ThanhTienX = kq.Key.ThanhTienX,
                        }).ToList();
            if (qnxt.Count > 0)
            {
                double a = Convert.ToDouble(qnxt.Where(p => p.MaDV == _madv).Where(p => p.DVT == _donvi).Where(p => p.DonGia == _dongia).Sum(p => p.ThanhTienX));
                colXuatPKTT.Text = a.ToString("##,###");
            }
        }

        private void colXuatPKTTTong_BeforePrint(object sender, CancelEventArgs e)
        {
            int _makp = 0;
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            _makp = String.IsNullOrEmpty(MaKP.Value.ToString()) == null ? 0 : Convert.ToInt32(MaKP.Value);
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            var qnxt = (from nhapd in data.NhapDs
                        join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                        join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
                        join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                        join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                        where (nhomdv.TenNhom.Contains("Hóa chất"))
                        where (kp.PLoai.Contains("PK khu vực"))
                        where (nhapd.MaKP == _makp && nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 3)
                        group new { dv, nhapd, nhapdct } by new { nhapdct.ThanhTienX } into kq
                        select new
                        {
                            ThanhTienX = kq.Key.ThanhTienX,
                        }).ToList();
            if (qnxt.Count > 0)
            {
                double a = Convert.ToDouble(qnxt.Sum(p => p.ThanhTienX));
                colXuatPKTTTong.Text = a.ToString("##,###");
            }
        }

        private void colXuatKhacSL_BeforePrint(object sender, CancelEventArgs e)
        {

            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            int _madv = 0;
            int _makp = 0;
            string _donvi = "";
            int _dongia = 0;
            _makp = String.IsNullOrEmpty(MaKP.Value.ToString()) == null ? 0 : Convert.ToInt32(MaKP.Value);
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32( GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("DVT") != null)
                _donvi = GetCurrentColumnValue("DVT").ToString();
            if (GetCurrentColumnValue("DonGia") != null)
                _dongia = int.Parse(this.GetCurrentColumnValue("DonGia").ToString());
            var qnxt = (from nhapd in data.NhapDs
                        join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                        join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
                        join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                        join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                        where (nhomdv.TenNhom.Contains("Hóa chất"))
                        where (kp.PLoai.Contains("Khoa dược"))
                        where (nhapd.MaKP == _makp && nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 2)
                        group new { dv, nhapd, nhapdct } by new { dv.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapdct.SoLuongX } into kq
                        select new
                        {
                            MaDV = kq.Key.MaDV,
                            DVT = kq.Key.DonVi,
                            DonGia = kq.Key.DonGia,
                            SoLuongX = kq.Key.SoLuongX,
                        }).ToList();
            if (qnxt.Count > 0)
            {
                double a = Convert.ToDouble(qnxt.Where(p => p.MaDV == _madv).Where(p => p.DVT == _donvi).Where(p => p.DonGia == _dongia).Sum(p => p.SoLuongX));
                colXuatKhacSL.Text = a.ToString("##,###");
            }
        }

        private void colXuatKhacTT_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            int _madv = 0;
            int _makp = 0;
            string _donvi = "";
            int _dongia = 0;
            _makp = String.IsNullOrEmpty(MaKP.Value.ToString()) == null ? 0 : Convert.ToInt32(MaKP.Value);
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32( GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("DVT") != null)
                _donvi = GetCurrentColumnValue("DVT").ToString();
            if (GetCurrentColumnValue("DonGia") != null)
                _dongia = int.Parse(this.GetCurrentColumnValue("DonGia").ToString());
            var qnxt = (from nhapd in data.NhapDs
                        join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                        join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
                        join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                        join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                        where (nhomdv.TenNhom.Contains("Hóa chất"))
                        where (kp.PLoai.Contains("Khoa dược"))
                        where (nhapd.MaKP == _makp && nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 2)
                        group new { dv, nhapd, nhapdct } by new { dv.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapdct.ThanhTienX } into kq
                        select new
                        {
                            MaDV = kq.Key.MaDV,
                            DVT = kq.Key.DonVi,
                            DonGia = kq.Key.DonGia,
                            ThanhTienX = kq.Key.ThanhTienX,
                        }).ToList();
            if (qnxt.Count > 0)
            {
                double a = Convert.ToDouble(qnxt.Where(p => p.MaDV == _madv).Where(p => p.DVT == _donvi).Where(p => p.DonGia == _dongia).Sum(p => p.ThanhTienX));
                colXuatKhacTT.Text = a.ToString("##,###");
            }
        }

        private void colXuatKhacTTTong_BeforePrint(object sender, CancelEventArgs e)
        {
             int _makp = 0;
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            _makp = String.IsNullOrEmpty(MaKP.Value.ToString()) == null ? 0 : Convert.ToInt32(MaKP.Value);
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
          
            var qnxt = (from nhapd in data.NhapDs
                        join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                        join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
                        join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                        join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                        where (nhomdv.TenNhom.Contains("Hóa chất"))
                        where (kp.PLoai.Contains("Khoa dược"))
                        where (nhapd.MaKP == _makp && nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 2)
                        group new { dv, nhapd, nhapdct } by new {nhapdct.ThanhTienX } into kq
                        select new
                        {
                            
                            ThanhTienX = kq.Key.ThanhTienX,
                        }).ToList();
            if (qnxt.Count > 0)
            {
                double a=Convert.ToDouble(qnxt.Sum(p => p.ThanhTienX));
                colXuatKhacTTTong.Text = a.ToString("##,###");
            }
        }

        private void txtTienBangChu_BeforePrint(object sender, CancelEventArgs e)
        {
            Double st = Convert.ToDouble(TongTien.Value);
            st = Math.Round(st, 0);
            txtTienBangChu.Text = DungChung.Ham.DocTienBangChu(st, " đồng./");
        }
        
    }
}
