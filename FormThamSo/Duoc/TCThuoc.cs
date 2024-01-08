using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.TraCuu
{
    public partial class TCThuoc : DevExpress.XtraEditors.XtraForm
    {
        public TCThuoc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<KhoaKho> _listKho = new List<KhoaKho>();
        List<KhoaKho> _listKhoaPhong = new List<KhoaKho>();

        private void TCThuoc_Load(object sender, EventArgs e)
        {
            _listKho.Clear();
            _listKhoaPhong.Clear();
            dtTimTuNgay.DateTime = System.DateTime.Now;
            dtTimDenNgay.DateTime = System.DateTime.Now;
            var listKho = (_data.KPhongs.Where(p => p.PLoai == "Khoa Dược")).ToList();
            if (listKho.Count() > 0)
            {
                KhoaKho kho = new KhoaKho();
                kho.TenKP = "Chọn tất cả";
                kho.MaKP = 0;
                kho.Chon = true;
                _listKho.Add(kho);
                foreach (var i in listKho)
                {
                    KhoaKho kk = new KhoaKho();
                    kk.TenKP = i.TenKP;
                    kk.MaKP = i.MaKP;
                    kk.Chon = true;
                    _listKho.Add(kk);
                }
                grcKhoDuoc.DataSource = _listKho.ToList();
            }
            var kPhong = (_data.KPhongs.Where(p => p.PLoai == "Lâm sàng")).ToList();
            if (kPhong.Count() > 0)
            {
                KhoaKho khoa = new KhoaKho();
                khoa.TenKP = "Chọn tất cả";
                khoa.MaKP = 0;
                khoa.Chon = true;
                _listKhoaPhong.Add(khoa);
                foreach (var i in kPhong)
                {
                    KhoaKho kk = new KhoaKho();
                    kk.TenKP = i.TenKP;
                    kk.MaKP = i.MaKP;
                    kk.Chon = true;
                    _listKhoaPhong.Add(kk);
                }
                grcKhoa.DataSource = _listKhoaPhong.ToList();
            }
            // _trangThai.Add(new TrangThai { status = 0, trangThai = "Chưa xuất" }());
        }
        int makho = 0;
        int madv = 0;
        private class Thuoc
        {
            public string tenDV { get; set; }
            public int maDV { get; set; }
            public string dichVu { get; set; }
            public int maKP { get; set; }
        }
        private class KhoaKho
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
        private class c_DanhSach
        {
            double _DonGia;

            public double DonGia
            {
                get { return _DonGia; }
                set { _DonGia = value; }
            }
            DateTime _NgayKe;

            public DateTime NgayKe
            {
                get { return _NgayKe; }
                set { _NgayKe = value; }
            }
            int _IDDon;

            public int IDDon
            {
                get { return _IDDon; }
                set { _IDDon = value; }
            }
            double _SoLuong;

            public double SoLuong
            {
                get { return _SoLuong; }
                set { _SoLuong = value; }
            }
            int _SoPL;

            public int SoPL
            {
                get { return _SoPL; }
                set { _SoPL = value; }
            }
            string _TenKP;

            public string TenKP
            {
                get { return _TenKP; }
                set { _TenKP = value; }
            }
            string _TenBN;

            public string TenBN
            {
                get { return _TenBN; }
                set { _TenBN = value; }
            }
            int _MaBNhan;

            public int MaBNhan
            {
                get { return _MaBNhan; }
                set { _MaBNhan = value; }
            }
            int _MaKP;

            public int MaKP
            {
                get { return _MaKP; }
                set { _MaKP = value; }
            }
            public int MaKXuat { get; set; }
            int _MaDV;

            public int MaDV
            {
                get { return _MaDV; }
                set { _MaDV = value; }
            }
            string _TenDV;

            public string TenDV
            {
                get { return _TenDV; }
                set { _TenDV = value; }
            }

            int _IDDonct;

            public int IDDonct
            {
                get { return _IDDonct; }
                set { _IDDonct = value; }
            }
        }
        private class DsThuoc
        {
            public int MaDV { get; set; }
            public string TenDV { get; set; }
            public double DonGia { get; set; }
            public double SoLuong { get; set; }

        }

        List<c_DanhSach> _lDanhsach = new List<c_DanhSach>();
        List<DsThuoc> _lthuoc = new List<DsThuoc>();

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            _lDanhsach.Clear();
            _lthuoc.Clear();
            DateTime tungay = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            int mathuoc = 0;
            if (!string.IsNullOrEmpty(txtMaDV.Text))
            {
                mathuoc = Convert.ToInt32(txtMaDV.Text);
            }

            List<KhoaKho> dskho = new List<KhoaKho>();
            List<KhoaKho> dskhoa = new List<KhoaKho>();
            dskho = _listKho.Where(p => p.MaKP > 0 && p.Chon == true).ToList();
            dskhoa = _listKhoaPhong.Where(p => p.MaKP > 0 && p.Chon == true).ToList();

            _lDanhsach = (from dt in _data.DThuocs.Where(p => p.NgayKe >= tungay).Where(p => p.NgayKe <= denngay)
                          join bn in _data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                          join kp in _data.KPhongs on dt.MaKP equals kp.MaKP
                          join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                          join dv in _data.DichVus.Where(p => p.PLoai == 1).Where(p => mathuoc == 0 || p.MaDV == mathuoc) on dtct.MaDV equals dv.MaDV
                          where (dtct.Status == 0)
                          //&& dtct.MaDV == mathuoc
                          select new c_DanhSach
                          {
                              TenDV = dv.TenDV,
                              MaDV = dv.MaDV,
                              DonGia = dtct.DonGia == null ? 0 : dtct.DonGia,
                              //IDDonct = dtct.IDDonct,
                              NgayKe = dt.NgayKe == null ? new DateTime() : dt.NgayKe.Value,
                              IDDon = dtct.IDDon.Value,
                              SoLuong = dtct.SoLuong,
                              SoPL = dtct.SoPL == null ? 0 : dtct.SoPL,
                              //Status = dt.Status == 0 ? "Chưa xuất" : "",
                              MaBNhan = dt.MaBNhan == null ? 0 : dt.MaBNhan.Value,
                              MaKP = dt.MaKP == null ? 0 : dt.MaKP.Value,//khoa
                              MaKXuat = dt.MaKXuat == null ? 0 : dt.MaKXuat.Value,//kho,
                              TenKP = kp.TenKP,
                              TenBN = bn.TenBNhan
                          }).OrderBy(p => p.MaDV).ToList();

            _lthuoc = (from kp in dskho
                       join ds in _lDanhsach on kp.MaKP equals ds.MaKXuat
                       join kx in dskhoa on ds.MaKP equals kx.MaKP
                       group new { ds, kp, kx } by new { ds.MaDV, ds.TenDV, ds.DonGia } into kq//, ds.MaKP, ds.MaKXuat 
                       select new DsThuoc { MaDV = kq.Key.MaDV, TenDV = kq.Key.TenDV, DonGia = kq.Key.DonGia, SoLuong = kq.Sum(p => p.ds.SoLuong) }).ToList();
            if (_lDanhsach.Count() > 0)
            {
                grcTraCuu.DataSource = _lthuoc.ToList();
            }

        }

        private void grvKho_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "chonKho")
            {
                if (grvKho.GetFocusedRowCellValue("TenKP") != null)
                {
                    string ten = grvKho.GetFocusedRowCellValue("TenKP").ToString();
                    if (ten == "Chọn tất cả")
                    {
                        if (_listKho.First().Chon == true)
                        {
                            foreach (var i in _listKho)
                            {
                                i.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var i in _listKho)
                            {
                                i.Chon = true;
                            }

                        }
                        grcKhoDuoc.DataSource = _listKho.ToList();
                    }
                }
            }
        }
        private void grvKhoaPhong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "chonKhoaPhong")
            {
                string ten = grvKhoaPhong.GetFocusedRowCellValue("TenKP").ToString();
                if (ten == "Chọn tất cả")
                {
                    if (_listKhoaPhong.First().Chon == true)
                    {
                        foreach (var kp in _listKhoaPhong)
                        {
                            kp.Chon = false;
                        }
                    }
                    else
                    {
                        foreach (var kp in _listKhoaPhong)
                        {
                            kp.Chon = true;
                        }
                    }
                    grcKhoa.DataSource = _listKhoaPhong.ToList();
                }

            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KhoaKho> dskho = new List<KhoaKho>();
            List<KhoaKho> dskhoa = new List<KhoaKho>();
            dskho = _listKho.Where(p => p.MaKP > 0 && p.Chon == true).ToList();
            dskhoa = _listKhoaPhong.Where(p => p.MaKP > 0 && p.Chon == true).ToList();
            if (_lDanhsach.Count > 0)
            {
                var list = (from kp in dskho
                            join ds in _lDanhsach on kp.MaKP equals ds.MaKXuat
                            join kx in dskhoa on ds.MaKP equals kx.MaKP
                            group new { ds, kp, kx } by new { ds.MaDV, ds.TenDV, ds.DonGia } into kq//, ds.MaKP, ds.MaKXuat 
                            select new { kq.Key.MaDV, kq.Key.TenDV, kq.Key.DonGia, SoLuong = kq.Sum(p => p.ds.SoLuong) }).ToList();
                string[] _arr = new string[] { "0", "@", "@", "@", "@" };
                string[] _tieude = { "Stt", "Mã thuốc", "Tên thuốc", "Đơn giá", "Số lượng" };
                string _filePath = "C:\\" + "DanhSachThuocKeDon.xls";
                int[] _arrWidth = new int[] { };
                var qexcel = list;
                DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 5];
                for (int i = 0; i < 5; i++)
                {
                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                }
                int num = 1;
                foreach (var r in qexcel)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.MaDV;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.TenDV;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.SoLuong;
                    num++;
                }
                QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "DanhSachThuocKeDon", _filePath, true);
            }
        }
        private void grvTraCuu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            List<KhoaKho> dskho = new List<KhoaKho>();
            List<KhoaKho> dskhoa = new List<KhoaKho>();
            dskho = _listKho.Where(p => p.MaKP > 0 && p.Chon == true).ToList();
            dskhoa = _listKhoaPhong.Where(p => p.MaKP > 0 && p.Chon == true).ToList();
            if (grvTraCuu.GetFocusedRowCellValue(colMaDV) != null && grvTraCuu.GetFocusedRowCellValue(colMaDV) != "")
            {
                int maDV = Convert.ToInt32(grvTraCuu.GetFocusedRowCellValue(colMaDV));
                double dongia = Convert.ToDouble(grvTraCuu.GetFocusedRowCellValue(colDonGia));
                var chitiet = (from kp in dskho
                               join ds in _lDanhsach.Where(p => p.MaDV == maDV && p.DonGia == dongia) on kp.MaKP equals ds.MaKXuat
                               join kx in dskhoa on ds.MaKP equals kx.MaKP
                               //where ds.MaDV == maDV// && ds.DonGia 
                               //group ds by 
                               select ds).OrderBy(p => p.NgayKe).ToList();
                if (chitiet.Count() > 0)
                {
                    grcChiTietThuoc.DataSource = chitiet;
                }
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            BaoCao.rep_BC_TCTT rep = new BaoCao.rep_BC_TCTT();
            frmIn frm = new frmIn();
            rep.DataSource = _lthuoc;
            //repT.lab_tungaydenngay.Text = "Từ ngày " + lupTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupDenNgay.DateTime.ToString("dd/MM/yyyy");
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}