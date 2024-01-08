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
    public partial class frm_BC_NXT_04011 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_NXT_04011()
        {
            InitializeComponent();
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
        List<KhoDuoc> _lKhoDuoc = new List<KhoDuoc>();
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BC_NXT_04011_Load(object sender, EventArgs e)
        {
            date_TuNgay.DateTime = DateTime.Now;
            date_DenNgay.DateTime = DateTime.Now;
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kphong = data.KPhongs.Where(p => p.PLoai == "Khoa dược").ToList();
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
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(date_TuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(date_DenNgay.DateTime);
            List<KhoDuoc> khoChon = new List<KhoDuoc>();
            khoChon = _lKhoDuoc.Where(p => p.MaKP > 0).Where(p => p.Chon == true).ToList();

            var qNhapd = (from nd in data.NhapDs.Where(p => p.PLoai == 1 || p.PLoai == 2 || p.PLoai == 3)
                          join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          select new
                          {
                              nd.PLoai,
                              nd.IDNhap,
                              ndct.IDDTBN,
                              nd.Mien,
                              nd.KieuDon,
                              nd.NgayNhap,
                              nd.MaKP,
                              ndct.MaDV,
                              ndct.DonGia,
                              ndct.SoLuongX,
                              ndct.ThanhTienX,
                              ndct.SoLuongN,
                              ndct.ThanhTienN
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
                           dv.MaTam,
                           dv.HamLuong,
                           dv.DuongD,
                           dv.DonVi,
                           tn.TenTN,
                           dv.MaDV,
                           SoDangKy = dv.SoDK,
                           dv.NuocSX
                       }).ToList();
            var q1 = (from kp in khoChon
                      join dt in qNhapd on kp.MaKP equals dt.MaKP
                      join dv in qdv on dt.MaDV equals dv.MaDV
                      group new { dt, dv } by new { dv.TenTN, dv.TenHC, dv.MaATC, dv.MaTam, dv.SoDangKy, dv.NuocSX, dv.HamLuong, dv.DonVi, dv.DuongD, dt.DonGia, dv.MaDV, dv.TenDV } into kq
                      select new
                      {
                          kq.Key.TenTN,
                          kq.Key.TenHC,
                          kq.Key.MaATC,
                          kq.Key.MaTam,
                          TenDV = kq.Key.TenDV,
                          kq.Key.SoDangKy,
                          kq.Key.NuocSX,
                          kq.Key.HamLuong,
                          kq.Key.DonVi,
                          kq.Key.DuongD,
                          kq.Key.DonGia,
                          TonDK = kq.Where(p => p.dt.NgayNhap < tungay).Where(p => p.dt.PLoai == 1).Sum(p => p.dt.SoLuongN) -
                                  kq.Where(p => p.dt.NgayNhap < tungay).Where(p => p.dt.PLoai == 2 || p.dt.PLoai == 3).Sum(p => p.dt.SoLuongX),
                          NhapHD = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 1 && p.dt.KieuDon == 1).Sum(p => p.dt.SoLuongN), // nhập theo hóa đơn
                          NhapKhac = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 1 && p.dt.KieuDon != 1).Sum(p => p.dt.SoLuongN), // kiểu đơn còn lại
                          Xuat = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2)
                                     .Where(p => p.dt.KieuDon != 9).Where(p => p.dt.Mien <= 0).Sum(p => p.dt.SoLuongX),//???,
                          XuatKhac = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2)
                                       .Where(p => p.dt.Mien <= 0).Where(p => p.dt.KieuDon == 9).Sum(p => p.dt.SoLuongX),
                          TonCK = kq.Where(p => p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 1).Sum(p => p.dt.SoLuongN) -
                                    kq.Where(p => p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2 || p.dt.PLoai == 3).Sum(p => p.dt.SoLuongX)
                      }).ToList();
            #region  Xuat excel
            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
            string[] _tieude = { "Stt", "Tên hoạt chất", "Mã ATC", "Mã nội bộ", "Tên biệt dược", "Số ĐK", "Nơi SX", "Nồng độ", "Đơn vị", "Đường dùng", "Đơn giá", "Tồn đầu kỳ", "Nhập", "Nhập khác", "Xuất", "Xuất khác", "Tồn cuối kỳ" };
            string _filePath = "C:\\" + "BCNhapXuatTon_HaLang.xls";
            int[] _arrWidth = new int[] { };
            var qexcel = q1;
            DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 17];
            for (int i = 0; i < 17; i++)
            {
                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
            }
            int num = 1;
            foreach (var r in qexcel)
            {
                DungChung.Bien.MangHaiChieu[num, 0] = num;
                DungChung.Bien.MangHaiChieu[num, 1] = r.TenHC;
                DungChung.Bien.MangHaiChieu[num, 2] = r.MaATC;
                DungChung.Bien.MangHaiChieu[num, 3] = r.MaTam;
                DungChung.Bien.MangHaiChieu[num, 4] = r.TenDV;
                DungChung.Bien.MangHaiChieu[num, 5] = r.SoDangKy;
                DungChung.Bien.MangHaiChieu[num, 6] = r.NuocSX;
                DungChung.Bien.MangHaiChieu[num, 7] = r.HamLuong;
                DungChung.Bien.MangHaiChieu[num, 8] = r.DonVi;
                DungChung.Bien.MangHaiChieu[num, 9] = r.DuongD;
                DungChung.Bien.MangHaiChieu[num, 10] = r.DonGia;
                DungChung.Bien.MangHaiChieu[num, 11] = r.TonDK;
                DungChung.Bien.MangHaiChieu[num, 12] = r.NhapHD;
                DungChung.Bien.MangHaiChieu[num, 13] = r.NhapKhac;
                DungChung.Bien.MangHaiChieu[num, 14] = r.Xuat;
                DungChung.Bien.MangHaiChieu[num, 15] = r.XuatKhac;
                DungChung.Bien.MangHaiChieu[num, 16] = r.TonCK;
                num++;
            }
            //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "123", _filePath, true);
            #endregion
            BaoCao.Rep_BC_NXT_04011 rep = new BaoCao.Rep_BC_NXT_04011();
            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
            rep.DataSource = q1.OrderBy(p => p.TenTN).ThenBy(p => p.TenDV);
            rep.lblThoiGian.Text = "( Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy") + " )";
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
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