using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_KiemKeThuoc_30007 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_KiemKeThuoc_30007()
        {
            InitializeComponent();
        }
        List<KhoDuoc> _lKhoDuoc = new List<KhoDuoc>();
        private void frm_BC_KiemKeThuoc_30007_Load(object sender, EventArgs e)
        {
            date_TuNgay.DateTime = DateTime.Now;
            date_DenNgay.DateTime = DateTime.Now;

            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kphong = data.KPhongs.Where(p => p.PLoai == "Khoa dược" || (DungChung.Bien.MaBV == "30007" ?  p.PLoai == "Tủ trực" : false)).ToList();
            if (kphong.Count > 0)
            {
                KhoDuoc themmoi1 = new KhoDuoc();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Chon = true;
                _lKhoDuoc.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KhoDuoc themmoi = new KhoDuoc();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Chon = true;
                    _lKhoDuoc.Add(themmoi);
                }
                grcKhoaphong.DataSource = _lKhoDuoc.ToList();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(date_TuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(date_DenNgay.DateTime);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            List<KhoDuoc> _kpChon = new List<KhoDuoc>();
            _kpChon = _lKhoDuoc.Where(p => p.MaKP > 0).Where(p => p.Chon == true).ToList();
            List<DichVu> _ldv = data.DichVus.ToList();
            List<KPhong> _kp = data.KPhongs.ToList();
            var qNhapd = (from nd in data.NhapDs.Where(p => p.PLoai == 1 || p.PLoai == 2 || p.PLoai == 3)
                          join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          //join dtBN in data.DTBNs on nd.IDDTBN equals dtBN.IDDTBN into kq
                          //from kq1 in kq.DefaultIfEmpty()
                          select new
                          {
                              nd.IDNhap,
                              ndct.IDDTBN,
                              //DTBN1 = kq1 == null ? "" : kq1.DTBN1,
                              nd.Mien,
                              PhanLoai = nd.PLoai,
                              nd.KieuDon,
                              nd.XuatTD,
                              nd.NgayNhap,
                              nd.MaKP,
                              ndct.MaDV,
                              ndct.DonGia,
                              ndct.SoLuongX,
                              ndct.SoLuongN,
                              ndct.ThanhTienN,
                              nd.TraDuoc_KieuDon,
                              nd.MaKPnx,
                              nd.MaCC
                          }).ToList();

            var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 1)
                       join dvEx in data.DichVuExes on dv.MaDV equals dvEx.MaDV into kq
                       from kq1 in kq.DefaultIfEmpty()
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new
                       {
                           dv.TenDV,
                           dv.TenHC,
                           kq1.MaATC,
                           VEN = kq1.VEN == null ? "" : kq1.VEN,
                           dv.MaTam,
                           dv.HamLuong,
                           dv.DuongD,
                           dv.DonVi,
                           tn.TenTN,
                           dv.MaDV,
                           SoDangKy = dv.SoDK,
                           dv.NuocSX,
                           dv.NhaSX,
                           dv.MaCC
                           //dv.DonGia
                       }).ToList();

            var q1 = (from kp in _kpChon
                      join dt in qNhapd on kp.MaKP equals dt.MaKP
                      join dv in qdv on dt.MaDV equals dv.MaDV
                      join kp1 in _kp on kp.MaKP equals kp1.MaKP 
                      group new { dt, dv, kp1} by new { dv.TenTN,dv.MaCC, dv.VEN, dv.TenHC, dv.MaATC, dv.MaTam, dv.SoDangKy, dv.NuocSX, dv.HamLuong, dv.DonVi, dv.DuongD, dt.DonGia, dv.MaDV, dv.TenDV ,dv.NhaSX} into kq
                      select new
                      {
                          kq.Key.TenTN,
                          kq.Key.TenHC,
                          kq.Key.MaATC,
                          kq.Key.VEN,
                          kq.Key.MaTam,
                          TenDV = kq.Key.TenDV,
                          kq.Key.MaDV,
                          kq.Key.SoDangKy,
                          kq.Key.NuocSX,
                          kq.Key.HamLuong,
                          kq.Key.DonVi, 
                          kq.Key.DuongD,
                          kq.Key.DonGia,
                          kq.Key.NhaSX,
                          kq.Key.MaCC,
                          TonDK = kq.Where(p => p.dt.NgayNhap < tungay).Where(p => p.dt.PhanLoai == 1).Sum(p => p.dt.SoLuongN) -
                                  kq.Where(p => p.dt.NgayNhap < tungay).Where(p => p.dt.PhanLoai == 2 || p.dt.PhanLoai == 3).Sum(p => p.dt.SoLuongX),
                          NhapKho = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay)
                          .Where(p => p.dt.PhanLoai == 1 && p.dt.KieuDon == 1).Where(p => p.kp1.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).Sum(p => p.dt.SoLuongN), // nhập theo hóa đơn và nhập từ kho khác
                          NhapTraLai = 0.0,
                          //kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay)
                          //              .Where(p => p.dt.PhanLoai == 1 && p.dt.KieuDon != 1 && p.dt.KieuDon != 2).Sum(p => p.dt.SoLuongN), // - nhập trả dược
                          XuatBN = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PhanLoai == 2)
                                     .Where(p => p.dt.KieuDon == 0 || p.dt.KieuDon == 1 || p.dt.KieuDon == 4).Where(p => p.dt.Mien <= 0).Sum(p => p.dt.SoLuongX)

                                     + kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PhanLoai == 2)
                                     .Where(p => p.dt.KieuDon == 3).Where(p => p.dt.Mien <= 0).Sum(p => p.dt.SoLuongX) //thêm Xuất Ngoài BV vào cột Xuất BN (Xuất Ngoài BV, MaKPnx.PLoai=Xã phường) // namnt

                                     - kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PhanLoai == 1 && p.dt.KieuDon == 2)
                                     .Where(p => p.dt.TraDuoc_KieuDon == 0 || p.dt.TraDuoc_KieuDon == 1 || p.dt.TraDuoc_KieuDon == 4).Sum(p => p.dt.SoLuongN),//???,
                          XuatKhac = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay)
                                    .Where(p => (p.dt.PhanLoai == 2 && p.dt.KieuDon == 8) || p.dt.PhanLoai == 3)
                                    .Where(p => p.dt.Mien <= 0).Sum(p => p.dt.SoLuongX), 
                                      //.Where(p => p.dt.KieuDon != 0 && p.dt.KieuDon != 1 && p.dt.KieuDon != 4)
                                      //- kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PhanLoai == 1 && p.dt.KieuDon == 2)
                                      //.Where(p => p.dt.TraDuoc_KieuDon != 0 && p.dt.TraDuoc_KieuDon != 1 && p.dt.TraDuoc_KieuDon != 4).Sum(p => p.dt.SoLuongN),
                          TongXuat = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PhanLoai == 2).Sum(p => p.dt.SoLuongX),
                          TonCuoi = kq.Where(p => p.dt.NgayNhap <= denngay).Where(p => p.dt.PhanLoai == 1).Sum(p => p.dt.SoLuongN) -
                                    kq.Where(p => p.dt.NgayNhap <= denngay).Where(p => p.dt.PhanLoai == 2 || p.dt.PhanLoai == 3).Sum(p => p.dt.SoLuongX)
                      }).ToList();
            var dtt = (from dthuocct in data.DThuoccts.Where(p => p.Status != 2)
                       join dthuoc in data.DThuocs on dthuocct.IDDon equals dthuoc.IDDon
                       where (dthuocct.SoLuong > 0 || (dthuocct.SoLuong < 0 && dthuoc.KieuDon == 2))
                       select new {dthuocct.MaDV, dthuoc.MaKXuat, dthuoc.NgayKe, dthuocct.DonGia, dthuocct.SoLuong }).ToList();
            var qxtt2 = (from dv in qdv
                         join dt in dtt on dv.MaDV equals dt.MaDV
 // kiểu trả thuốc chỉ lấy những thuốc đã kê cho bệnh nhân trong khoa xuất từ tủ trực 
                         select new { dv.TenTN, dv.VEN, dv.TenHC, dv.MaATC, dv.MaTam, dv.SoDangKy, dv.NuocSX,
                             dv.HamLuong, dv.DonVi, dv.DuongD, dt.MaKXuat, dt.NgayKe, dv.MaDV,
                             dv.TenDV, dt.DonGia, dt.SoLuong,dv.NhaSX,dv.MaCC}).ToList();
            var qxtt = (from dv in qxtt2
                        join tt in _kpChon on dv.MaKXuat equals tt.MaKP
                        join kp1 in _kp.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc) on tt.MaKP equals kp1.MaKP
                        group new { dv } by new { dv.DonGia,dv.NhaSX,dv.MaCC, dv.TenTN, dv.VEN, dv.TenHC, dv.MaATC, dv.MaTam, dv.SoDangKy, dv.NuocSX, dv.HamLuong, dv.DonVi, dv.DuongD, dv.MaDV, dv.TenDV } into kq
                        select new
                        {
                            kq.Key.TenTN,
                            kq.Key.TenHC,
                            kq.Key.MaATC,
                            kq.Key.VEN,
                            kq.Key.MaTam,
                            TenDV = kq.Key.TenDV,
                            kq.Key.MaDV,
                            kq.Key.SoDangKy,
                            kq.Key.NuocSX,
                            kq.Key.HamLuong,
                            kq.Key.DonVi,
                            kq.Key.DuongD,
                            kq.Key.DonGia,
                            kq.Key.NhaSX,
                            kq.Key.MaCC,
                            TonDK = kq.Where(p => p.dv.NgayKe < tungay).Sum(p => p.dv.SoLuong) * (-1),
                            NhapKho =0.0, 
                            NhapTraLai = 0.0,
                            XuatBN = kq.Where(p => p.dv.NgayKe >= tungay).Where(p => p.dv.NgayKe <= denngay).Sum(p => p.dv.SoLuong),//???,
                            XuatKhac = 0.0,
                            TongXuat = kq.Where(p => p.dv.NgayKe >= tungay).Where(p => p.dv.NgayKe <= denngay).Sum(p => p.dv.SoLuong),
                            TonCuoi = kq.Where(p => p.dv.NgayKe <= denngay).Sum(p => p.dv.SoLuong) * (-1),
                        }).ToList();
            //var q = (from a in q1
            //             join b in qxtt on a.MaDV equals b.MaDV
            //             where(a.TenTN == b.TenTN && a.TenHC == b.TenHC && a.MaATC == b.MaATC && a.VEN == b.VEN && a.SoDangKy == b.SoDangKy && a.MaTam == b.MaTam && a.TenDV == b.TenDV
            //             && a.NuocSX == b.NuocSX && a.HamLuong == b.HamLuong && a.DonVi == b.DonVi&& a.DuongD == b.DuongD&& a.DonGia == b.DonGia)
            //             );
            var _lcc = data.NhaCCs.ToList();
            var q = q1.Union(qxtt);
            var qqq = (from a in q
                       join cc in _lcc on a.MaCC equals cc.MaCC into kq
                       from b in kq.DefaultIfEmpty()
                       select new { a.TenTN, a.VEN, a.NhaSX, a.TenHC, a.MaATC, a.MaTam, a.SoDangKy, a.NuocSX, a.HamLuong, a.DonVi, a.DuongD, a.DonGia, a.MaDV, a.TenDV, Nhacc = b != null ? b.TenCC : "", a.TonDK, a.NhapKho, a.NhapTraLai, a.XuatBN, a.XuatKhac, a.TongXuat, a.TonCuoi }).ToList();
            var qq = (from a in qqq
                      group a by new { a.TenTN, a.VEN,a.NhaSX, a.TenHC, a.MaATC, a.MaTam, a.SoDangKy, a.NuocSX, a.HamLuong, a.DonVi, a.DuongD, a.DonGia, a.MaDV, a.TenDV,a.Nhacc } into kq
                      select new
                      {
                          kq.Key.TenTN,
                          kq.Key.TenHC,
                          kq.Key.MaATC,
                          kq.Key.VEN,
                          kq.Key.MaTam,
                          TenDV = kq.Key.TenDV,
                          kq.Key.MaDV,
                          kq.Key.SoDangKy,
                          kq.Key.NuocSX,
                          kq.Key.HamLuong,
                          kq.Key.DonVi,
                          kq.Key.DuongD,
                          kq.Key.DonGia,
                          kq.Key.NhaSX,
                          kq.Key.Nhacc,
                          TonDK = kq.Sum(p => p.TonDK),
                          NhapKho = kq.Sum(p => p.NhapKho),
                          NhapTraLai = kq.Sum(p => p.NhapTraLai),
                          XuatBN = kq.Sum(p => p.XuatBN),//???,
                          XuatKhac = kq.Sum(p => p.XuatKhac),
                          TongXuat = kq.Sum(p => p.TongXuat),
                          TonCuoi = kq.Sum(p => p.TonCuoi),
                      }).ToList();
            if (rgChonMau.SelectedIndex == 0)
            {
                BaoCao.Rep_BC_KiemKeThuoc_30007 rep = new BaoCao.Rep_BC_KiemKeThuoc_30007();
                frmIn frm = new frmIn();
                rep.DataSource = qq.OrderBy(p => p.TenTN).ThenBy(p => p.TenDV);
                rep.lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                rep.lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                rep.lblThoiGian.Text = "( Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy") + " )";
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                BaoCao.Rep_BC_KiemKeThuoc_TT22 rep = new BaoCao.Rep_BC_KiemKeThuoc_TT22();
                frmIn frm = new frmIn();
                rep.DataSource = qq.OrderBy(p => p.TenTN).ThenBy(p => p.TenDV);
                rep.lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                rep.lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                rep.lblThoiGian.Text = "( Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy") + " )";
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void grcKhoaphong_Click(object sender, EventArgs e)
        {

        }

        #region class Kho dược
        private class KhoDuoc
        {
            private string tenKP;

            public string TenKP
            {
                get { return tenKP; }
                set { tenKP = value; }
            }
            private int maKP;

            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }

            private bool chon;

            public bool Chon
            {
                get { return chon; }
                set { chon = value; }
            }
        }
        #endregion

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lKhoDuoc.First().Chon == true)
                        {
                            foreach (var a in _lKhoDuoc)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKhoDuoc)
                            {
                                a.Chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lKhoDuoc.ToList();
                    }
                }
            }
        }
    }
}