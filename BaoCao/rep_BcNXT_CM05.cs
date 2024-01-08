using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_BcNXT_CM05 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BcNXT_CM05()
        {
            InitializeComponent();
        }
        bool HThi = true;
        public rep_BcNXT_CM05(bool ht)
        {
            HThi = ht;
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colTenTieuNhomDV.DataBindings.Add("Text", DataSource, "TenTN");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            //colXuatNoiTTT.XlsxFormatString = DungChung.Bien.FormatString[1];

            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDK_SL").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTT.DataBindings.Add("Text", DataSource, "TonDK_TT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDK_TT").FormatString = DungChung.Bien.FormatString[1];

            colXuatTuTSL.DataBindings.Add("Text", DataSource, "XuatTuTruc_sl").FormatString = DungChung.Bien.FormatString[1];
            colXuatTuTTT.DataBindings.Add("Text", DataSource, "XuatTuTruc_tt").FormatString = DungChung.Bien.FormatString[1];
            colXuatTuTTTTong.DataBindings.Add("Text", DataSource, "XuatTuTruc_tt").FormatString = DungChung.Bien.FormatString[1];

            colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTK_sl").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTK_tt").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTK_tt").FormatString = DungChung.Bien.FormatString[1];

            colXuatNoiTSL.DataBindings.Add("Text", DataSource, "XuatNoiT_sl").FormatString = DungChung.Bien.FormatString[1];
            colXuatNoiTTT.DataBindings.Add("Text", DataSource, "XuatNoiT_tt").FormatString = DungChung.Bien.FormatString[1];
            colXuatNoiTTTTong.DataBindings.Add("Text", DataSource, "XuatNoiT_tt").FormatString = DungChung.Bien.FormatString[1];

            colXuatNgTSL.DataBindings.Add("Text", DataSource, "XuatNgoaiT_sl").FormatString = DungChung.Bien.FormatString[1];
            colXuatNgTTT.DataBindings.Add("Text", DataSource, "XuatNgoaiT_tt").FormatString = DungChung.Bien.FormatString[1];
            colXuatNgTSLTong.DataBindings.Add("Text", DataSource, "XuatNgoaiT_tt").FormatString = DungChung.Bien.FormatString[1];
            
            colXuatTuTraSL.DataBindings.Add("Text", DataSource, "XuatKhac_sl").FormatString = DungChung.Bien.FormatString[1];
            colXuatTuTraTT.DataBindings.Add("Text", DataSource, "XuatKhac_tt").FormatString = DungChung.Bien.FormatString[1];
            colXuatTuTraTTTong.DataBindings.Add("Text", DataSource, "XuatKhac_tt").FormatString = DungChung.Bien.FormatString[1];

            colTongXuatSL.DataBindings.Add("Text", DataSource, "XuatTK_sl").FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTT.DataBindings.Add("Text", DataSource, "XuatTK_tt").FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTTTong.DataBindings.Add("Text", DataSource, "XuatTK_tt").FormatString = DungChung.Bien.FormatString[1];

            colTonCKSL.DataBindings.Add("Text", DataSource, "TonCK_SL").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTT.DataBindings.Add("Text", DataSource, "TonCK_TT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCK_TT").FormatString = DungChung.Bien.FormatString[1];
            if (HThi)
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
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

        private void txtTienBangChu_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (!string.IsNullOrEmpty(colTonCKTTTong.Text))
     
           // if (GetCurrentColumnValue("TongTien") != null)
            //{
          
        }

        private void colXuatNoiTSL_BeforePrint(object sender, CancelEventArgs e)
        {

          
            //DateTime tungay = System.DateTime.Now.Date;
            //DateTime denngay = System.DateTime.Now.Date;
            // tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            //denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            //int _madv = 0;
            //string _makp = "";
            //string _donvi = "";
            //int _dongia = 0;
            //    _makp = MaKP.Value.ToString();
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            //if (GetCurrentColumnValue("DVT") != null)
            //    _donvi = GetCurrentColumnValue("DVT").ToString();
            //if (GetCurrentColumnValue("DonGia") != null)
            //    _dongia = int.Parse(this.GetCurrentColumnValue("DonGia").ToString());
            //var qnxt = (from nhapd in data.NhapDs
            //                join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
            //                join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
            //                join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
            //                join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
            //                join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
            //                where (nhapd.MaKP == _makp)
            //                where (kp.PLoai.Contains("Lâm sàng"))
            //                where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon ==1)
            //                group new { kp, nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { nhomdv.TenNhom, tieunhomdv.TenTN, dv.MaDV, dv.TenDV,nhapdct.DonVi,nhapdct.DonGia,nhapdct.SoLuongX} into kq
            //                select new
            //                {
            //                    MaDV = kq.Key.MaDV,
            //                    DVT = kq.Key.DonVi,
            //                    DonGia=kq.Key.DonGia,
            //                    SoLuongX=kq.Key.SoLuongX,
            //                }).ToList();
            //   if(qnxt.Count>0)
            //   {
            //        double a = Convert.ToDouble(qnxt.Where(p => p.MaDV == _madv).Where(p => p.DVT == _donvi).Where(p => p.DonGia == _dongia).Sum(p => p.SoLuongX));
            //        colXuatNoiTSL.Text = a.ToString("##,###");
            //   }
        }

        private void colXuatNoiTTT_BeforePrint(object sender, CancelEventArgs e)
        {

            //DateTime tungay = System.DateTime.Now.Date;
            //DateTime denngay = System.DateTime.Now.Date;
            //tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            //denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            //int _madv = 0;
            //string _makp = "";
            //string _donvi = "";
            //int _dongia = 0;
            //_makp = MaKP.Value.ToString();
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            //if (GetCurrentColumnValue("DVT") != null)
            //    _donvi = GetCurrentColumnValue("DVT").ToString();
            //if (GetCurrentColumnValue("DonGia") != null)
            //    _dongia = int.Parse(this.GetCurrentColumnValue("DonGia").ToString());
            //var qnxt = (from nhapd in data.NhapDs
            //            join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
            //            join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
            //            join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
            //            join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
            //            join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
            //            where (nhapd.MaKP == _makp)
            //            where (kp.PLoai.Contains("Lâm sàng"))
            //            where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 1)
            //            group new { kp, nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { nhomdv.TenNhom, tieunhomdv.TenTN, dv.MaDV, dv.TenDV, nhapdct.DonVi, nhapdct.DonGia, nhapdct.ThanhTienX} into kq
            //            select new
            //            {
            //                MaDV = kq.Key.MaDV,
            //                DVT = kq.Key.DonVi,
            //                DonGia = kq.Key.DonGia,
            //                ThanhTienX = kq.Key.ThanhTienX,
            //            }).ToList();
            //if (qnxt.Count > 0)
            //{
            //    double a=Convert.ToDouble(qnxt.Where(p => p.MaDV == _madv).Where(p => p.DVT == _donvi).Where(p => p.DonGia == _dongia).Sum(p => p.ThanhTienX));
            //    colXuatNoiTTT.Text = a.ToString("##,###");
            //}
        }

        private void colXuatTuTSL_BeforePrint(object sender, CancelEventArgs e)
        {
            //DateTime tungay = System.DateTime.Now.Date;
            //DateTime denngay = System.DateTime.Now.Date;
            //tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            //denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            //int _madv = 0;
            //string _makp = "";
            //string _donvi = "";
            //int _dongia = 0;
            //_makp = MaKP.Value.ToString();
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            //if (GetCurrentColumnValue("DVT") != null)
            //    _donvi = GetCurrentColumnValue("DVT").ToString();
            //if (GetCurrentColumnValue("DonGia") != null)
            //    _dongia = int.Parse(this.GetCurrentColumnValue("DonGia").ToString());
            //var qnxt = (from nhapd in data.NhapDs
            //            join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
            //            join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
            //            join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
            //            join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
            //            join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
            //            where (nhapd.MaKP == _makp)
            //            where (kp.PLoai.Contains("Tủ trực"))
            //            where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 1)
            //            group new { kp, nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { nhomdv.TenNhom, tieunhomdv.TenTN, dv.MaDV, dv.TenDV, nhapdct.DonVi, nhapdct.DonGia, dv.NuocSX, nhapdct.SoLuongX } into kq
            //            select new
            //            {
            //                MaDV = kq.Key.MaDV,
            //                DVT = kq.Key.DonVi,
            //                DonGia = kq.Key.DonGia,
            //                SoLuongX = kq.Key.SoLuongX,
            //            }).ToList();
            //if (qnxt.Count > 0)
            //{
            //    double a=Convert.ToDouble(qnxt.Where(p => p.MaDV == _madv).Where(p => p.DVT == _donvi).Where(p => p.DonGia == _dongia).Sum(p => p.SoLuongX));
            //    colXuatTuTSL.Text = a.ToString("##,###");
            //}
        }

        private void colXuatTuTTT_BeforePrint(object sender, CancelEventArgs e)
        {
            //DateTime tungay = System.DateTime.Now.Date;
            //DateTime denngay = System.DateTime.Now.Date;
            //tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            //denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            //int _madv = 0;
            //string _makp = "";
            //string _donvi = "";
            //int _dongia = 0;
            //_makp = MaKP.Value.ToString();
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            //if (GetCurrentColumnValue("DVT") != null)
            //    _donvi = GetCurrentColumnValue("DVT").ToString();
            //if (GetCurrentColumnValue("DonGia") != null)
            //    _dongia = int.Parse(this.GetCurrentColumnValue("DonGia").ToString());
            //var qnxt = (from nhapd in data.NhapDs
            //            join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
            //            join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
            //            join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
            //            join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
            //            join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
            //            where (nhapd.MaKP == _makp)
            //            where (kp.PLoai.Contains("Tủ trực"))
            //            where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 1)
            //            group new { kp, nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { nhomdv.TenNhom, tieunhomdv.TenTN, dv.MaDV, dv.TenDV, nhapdct.DonVi, nhapdct.DonGia, dv.NuocSX, nhapdct.ThanhTienX } into kq
            //            select new
            //            {
            //                MaDV = kq.Key.MaDV,
            //                DVT = kq.Key.DonVi,
            //                DonGia = kq.Key.DonGia,
            //                ThanhTienX = kq.Key.ThanhTienX,
            //            }).ToList();
            //if (qnxt.Count > 0)
            //{
            //    double a=Convert.ToDouble(qnxt.Where(p => p.MaDV == _madv).Where(p => p.DVT == _donvi).Where(p => p.DonGia == _dongia).Sum(p => p.ThanhTienX));
            //    colXuatTuTTT.Text = a.ToString("##,###");
            //}
            
        }

        private void colXuatNoiTTTTong_BeforePrint(object sender, CancelEventArgs e)
        {
           //// int _madv = 0;
           // int _makp = 0;
           // string _pldv = "";
           // DateTime tungay = System.DateTime.Now.Date;
           // DateTime denngay = System.DateTime.Now.Date;
           // _makp = MaKP.Value.ToString();
           // _pldv = PhanLoai.Value.ToString();
           // tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
           // denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
           // //if (GetCurrentColumnValue("MaDV") != null)
           // //    _madv = GetCurrentColumnValue("MaDV").ToString();
           // var qnxt = (from nhapd in data.NhapDs.Where(p=>p.MaKP==_makp)
           //             join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
           //             join kp in data.KPhongs.Where(p=>p.PLoai.Contains("Lâm sàng")) on nhapd.MaKPnx equals kp.MaKP
           //             join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
           //             join nhomdv in data.NhomDVs.Where(p=>p.TenNhom.Contains(_pldv)) on dv.IDNhom equals nhomdv.IDNhom
           //             join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
           //             where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 1)
           //             group new { kp, nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { nhomdv.TenNhom, tieunhomdv.TenTN, dv.MaDV, dv.TenDV, nhapdct.DonVi, nhapdct.DonGia, dv.NuocSX, nhapdct.ThanhTienX } into kq
           //             select new
           //             {
           //                 MaDV = kq.Key.MaDV,
           //                 ThanhTienX = kq.Key.ThanhTienX,
           //             }).ToList();
           // if (qnxt.Count > 0)
           // {
           //     double a =Convert.ToDouble(qnxt.Sum(p => p.ThanhTienX));
           //     colXuatNoiTTTTong.Text = a.ToString("##,###");
           // }
            
        }

        private void colXuatTuTTTTong_BeforePrint(object sender, CancelEventArgs e)
        {

        //    // int _madv = 0;
        //    int _makp = 0;
        //    string _pldv = "";
        //    DateTime tungay = System.DateTime.Now.Date;
        //    DateTime denngay = System.DateTime.Now.Date;
        //    _makp = MaKP.Value.ToString();
        //    _pldv = PhanLoai.Value.ToString();
        //    tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
        //    denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
        //    //if (GetCurrentColumnValue("MaDV") != null)
        //    //    _madv = GetCurrentColumnValue("MaDV").ToString();
        //    var qnxt = (from nhapd in data.NhapDs.Where(p => p.MaKP == _makp)
        //                join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
        //                join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
        //                join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
        //                join nhomdv in data.NhomDVs.Where(p => p.TenNhom.Contains(_pldv)) on dv.IDNhom equals nhomdv.IDNhom
        //                join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
        //                where (kp.PLoai.Contains("Tủ trực"))
        //                where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 1)
        //                group new { kp, nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { nhomdv.TenNhom, tieunhomdv.TenTN, dv.MaDV, dv.TenDV, nhapdct.DonVi, nhapdct.DonGia, dv.NuocSX, nhapdct.ThanhTienX } into kq
        //                select new
        //                {
        //                    MaDV = kq.Key.MaDV,
        //                    ThanhTienX = kq.Key.ThanhTienX,
        //                }).ToList();
        //    if (qnxt.Count > 0)
        //    {
        //        double a= Convert.ToDouble(qnxt.Sum(p => p.ThanhTienX));
        //        colXuatTuTTTTong.Text = a.ToString("##,###");
        //    }
        }

    }
}
