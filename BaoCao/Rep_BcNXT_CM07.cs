using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BcNXT_CM07 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcNXT_CM07()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public void BindingData()
        {
            colNhomDV.DataBindings.Add("Text", DataSource, "NhomDV");
            colTieuNhomDV.DataBindings.Add("Text", DataSource, "TieuNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "maDV");
   
            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];


            colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];

            colSDSL.DataBindings.Add("Text", DataSource, "SDSL").FormatString = DungChung.Bien.FormatString[1];
            colSDTT.DataBindings.Add("Text", DataSource, "SDTT").FormatString = DungChung.Bien.FormatString[1];
          
            colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("TieuNhomDV"));
            GroupHeader2.GroupFields.Add(new GroupField("NhomDV"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            GroupHeader2.Visible = false;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        //private void colSDSL_BeforePrint(object sender, CancelEventArgs e)
        //{

        //    int _madv = 0;
        //    string _donvi = "";
        //    int _dongia = 0;
        //    DateTime tungay = System.DateTime.Now.Date;
        //    DateTime denngay = System.DateTime.Now.Date;
        //    tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
        //    denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("DVT") != null)
        //        _donvi = GetCurrentColumnValue("DVT").ToString();
          
        //    if(GetCurrentColumnValue("DonGia")!=null)
        //        _dongia = int.Parse(this.GetCurrentColumnValue("DonGia").ToString());
        //    var qnxt = (from nhapd in data.NhapDs
        //                join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
        //                join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP
        //                join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
        //                join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
        //                join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
        //                //   where (xp.MaXa ==_xp)
        //                where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay)
        //                where (nhapd.PLoai == 5)
        //                group new { nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { nhomdv.TenNhom, tieunhomdv.TenTN, dv.MaDV, dv.NuocSX, nhapd.MaCC, nhapdct.DonVi, nhapdct.DonGia, nhapdct.SoLuongSD } into kq
        //                select new
        //                {
        //                    MaDV = kq.Key.MaDV,
        //                    DVT=kq.Key.DonVi,
        //                    DonGia=kq.Key.DonGia,
        //                    SDSL = kq.Key.SoLuongSD,
        //                }).ToList();
        //    if (qnxt.Count > 0)
        //    {
        //        colSDSL.Text = qnxt.Where(p => p.MaDV == _madv).Where(p=>p.DonGia ==_dongia).Where(p=>p.DVT ==_donvi).Sum(p => p.SDSL).ToString();
        //    }
            
        //}

        //private void colSDTT_BeforePrint(object sender, CancelEventArgs e)
        //{

        //    int _madv = 0;
        //    string _donvi = "";
        //    int _dongia = 0;
        //    DateTime tungay = System.DateTime.Now.Date;
        //    DateTime denngay = System.DateTime.Now.Date;
        //    tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
        //    denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("DVT") != null)
        //        _donvi = GetCurrentColumnValue("DVT").ToString();

        //    if (GetCurrentColumnValue("DonGia") != null)
        //        _dongia = int.Parse(this.GetCurrentColumnValue("DonGia").ToString());
        //    var qnxt = (from nhapd in data.NhapDs
        //                join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
        //                join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP
        //                join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
        //                join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
        //                join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
        //                //   where (xp.MaXa ==_xp)
        //                where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay)
        //                where (nhapd.PLoai == 5)
        //                group new { nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { nhomdv.TenNhom, tieunhomdv.TenTN, dv.MaDV, dv.NuocSX, nhapd.MaCC, nhapdct.DonVi, nhapdct.DonGia, nhapdct.ThanhTienSD } into kq
        //                select new
        //                {
        //                    MaDV = kq.Key.MaDV,
        //                    DVT = kq.Key.DonVi,
        //                    DonGia = kq.Key.DonGia,
        //                    SDTT = kq.Key.ThanhTienSD,
        //                }).ToList();
        //    if (qnxt.Count > 0)
        //    {
        //        colSDTT.Text = qnxt.Where(p => p.MaDV == _madv).Where(p => p.DonGia == _dongia).Where(p => p.DVT == _donvi).Sum(p => p.SDTT).ToString();
        //    }
            
        //}

        //private void colSDTTTong_BeforePrint(object sender, CancelEventArgs e)
        //{
           
        //    DateTime tungay = System.DateTime.Now.Date;
        //    DateTime denngay = System.DateTime.Now.Date;
        //    tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
        //    denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
           
        //    var qnxt = (from nhapd in data.NhapDs
        //                join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
        //                join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP
        //                join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
        //                join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
        //                join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
        //                //   where (xp.MaXa ==_xp)
        //                where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay)
        //                where (nhapd.PLoai == 5)
        //                group new { nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { nhomdv.TenNhom, tieunhomdv.TenTN, dv.MaDV, dv.NuocSX, nhapd.MaCC, nhapdct.DonVi, nhapdct.DonGia, nhapdct.ThanhTienSD } into kq
        //                select new
        //                {
        //                    MaDV = kq.Key.MaDV,
        //                    DVT = kq.Key.DonVi,
        //                    DonGia = kq.Key.DonGia,
        //                    SDTT = kq.Key.ThanhTienSD,
        //                }).ToList();
        //    if (qnxt.Count > 0)
        //    {
        //        colSDTT.Text = qnxt.Sum(p => p.SDTT).ToString();
        //    }
            
        //}

    }
}
