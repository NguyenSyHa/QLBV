using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using QLBV_Database;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class Frm_TheoDoiPhanLoaiMuaThuoc24012 : Form
    {
        public Frm_TheoDoiPhanLoaiMuaThuoc24012()
        {
            InitializeComponent();
            if(DungChung.Bien.MaBV == "24012")
            {
                lblTimTenNCC.Visible = true;
                txtTimTenNCC.Visible = true;
            }
        }
        private class KPhongc
        {
            private string TenKP;
            private string PLoai;
            private int? MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public string ploai
            { set { PLoai = value; } get { return PLoai; } }
            public int? makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        private class NCC
        {
            public string macc { get; set; }
            public string tencc { get; set; }
            public bool chon { get; set; }
        }
        List<KPhong> _lkpall = new List<KPhong>();
        List<KPhongc> _Kphong = new List<KPhongc>();
        List<NCC> _NCC = new List<NCC>();
        QLBVEntities _Data = new QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_BCDoanhThuKhoaPhong24006_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            _lkpall = _Data.KPhongs.Where(p => p.Status == 1).ToList();
            var kphong = (from kp in _lkpall
                          where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc)
                          where (kp.Status == 1)
                          select new { kp.TenKP, kp.MaKP, kp.PLoai }).ToList();
            if (kphong.Count > 0)
            {
                KPhongc themmoi1 = new KPhongc();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                themmoi1.ploai = "Lâm sàng";
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhongc themmoi = new KPhongc();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    themmoi.ploai = a.PLoai;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }

            // nhà cung cấp
            var ncc = (from cc in _Data.NhaCCs
                           where (cc.Status == 1)
                          where (cc.Status == 1)
                          select new { cc.TenCC, cc.MaCC}).ToList();
            if (ncc.Count > 0)
            {
                NCC themmoi1 = new NCC();
                themmoi1.tencc = "Chọn tất cả";
                themmoi1.macc = "0";
                themmoi1.chon = true;
                _NCC.Add(themmoi1);
                foreach (var a in ncc)
                {
                    NCC themmoi = new NCC();
                    themmoi.tencc = a.TenCC;
                    themmoi.macc = a.MaCC;
                    themmoi.chon = true;
                    _NCC.Add(themmoi);
                }
                grcKhoXuat.DataSource = _NCC.ToList();
            }

        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            }
        }
        private bool kt()
        {
            if (string.IsNullOrEmpty(lupNgaytu.Text))
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupNgaytu.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupNgayden.Text))
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupNgayden.Focus();
                return false;
            }
            else if ((lupNgayden.DateTime - lupNgaytu.DateTime).Days < 0)
            {
                MessageBox.Show("Ngày đến phải lớn hơn hoặc bằng ngày từ");
                lupNgayden.Focus();
                return false;
            }
            return true;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!kt())
            {
                return;
            }
            DateTime _Ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime _Ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);

            List<int?> lkp = _Kphong.Where(p => p.chon == true).Select(p => p.makp).ToList();
            List<string> lcc = _NCC.Where(p => p.chon == true).Select(p => p.macc).ToList();
            var listND = (from nd in _Data.NhapDs.Where(p => p.PLoai == 1 && p.KieuDon == 1)
                            join ndct in _Data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                            join dv in _Data.DichVus on ndct.MaDV equals dv.MaDV
                            where(nd.NgayNhap > _Ngaytu && nd.NgayNhap < _Ngayden)
                            where lkp.Contains(nd.MaKP)
                            where lcc.Contains(nd.MaCC)
                            select new
                            {
                                dv.TenDV,
                                dv.DonVi,
                                nd.SoCT,
                                nd.NgayNhap,
                                ndct.DonGia,
                                ndct.SoLuongN,
                                ndct.ThanhTienN,
                                dv.ISTrongThau
                            }).OrderBy(p => p.NgayNhap).ToList();
            var listND2 = (from nd in listND group nd by new { nd.TenDV, nd.DonVi, nd.DonGia } into kq
                           select new _BC
                           {
                               TenDV = kq.Key.TenDV,
                               DonVi = kq.Key.DonVi,
                               DonGia = kq.Key.DonGia,
                               SoLuongN_TT = kq.Where(p => p.ISTrongThau == true || p.ISTrongThau == null).Sum(p => p.SoLuongN),
                               ThanhTienN_TT = kq.Where(p => p.ISTrongThau == true || p.ISTrongThau == null).Sum(p => p.ThanhTienN),
                               SoLuongN_NT = kq.Where(p => p.ISTrongThau == false).Sum(p => p.SoLuongN),
                               ThanhTienN_NT = kq.Where(p => p.ISTrongThau == false).Sum(p => p.ThanhTienN)
                           }).ToList();
            List <_BC> results = new List<_BC>();
            if (listND.Count > 0)
            {
                foreach (var item in listND2)
                {
                    _BC a = new _BC();
                    a.TenDV = item.TenDV;
                    a.DonVi = item.DonVi;
                    a.DonGia = item.DonGia;
                    a.SoLuongN_TT = item.SoLuongN_TT != 0 ? item.SoLuongN_TT : null;
                    a.ThanhTienN_TT = item.ThanhTienN_TT != 0 ? item.ThanhTienN_TT : null;
                    a.SoLuongN_NT = item.SoLuongN_NT != 0 ? item.SoLuongN_NT : null;
                    a.ThanhTienN_NT = item.ThanhTienN_NT != 0 ? item.ThanhTienN_NT : null;
                    results.Add(a);
                }
                #region In mới
                var HoaDon = "";
                HoaDon = string.Join("; ", listND.Select(p => p.SoCT).Distinct().ToList());
                double Tien = listND.Sum(p => p.ThanhTienN);
                var kp = _NCC.Where(p => p.chon == true).Where(p => p.macc != "0").Select(p => p.tencc).ToList();
                string KP = "";
                if (kp.Count > 0)
                    KP = string.Join("; ", kp);

                var kho = _Kphong.Where(p => p.chon == true).Where(p => p.makp != 0).Select(p => p.tenkp).ToList();
                string Kho = "";
                if (kho.Count > 0)
                    Kho = string.Join("; ", kho);
                Dictionary<string, object> _dic = new Dictionary<string, object>();
                _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ.ToUpper());
                _dic.Add("TenCQ", DungChung.Bien.TenCQ.ToUpper());
                _dic.Add("TenNCC", KP);
                _dic.Add("KhoNhap", Kho);
                _dic.Add("HoaDon", HoaDon);
                _dic.Add("TienBangChu", DungChung.Ham.DocTienBangChu(Tien, " đồng."));
                string ngaythang = "Từ ngày " + lupNgaytu.DateTime.ToShortDateString() + " đến ngày " + lupNgayden.DateTime.ToShortDateString();
                if (lupNgaytu.DateTime.Date == lupNgayden.DateTime.Date)
                {
                    ngaythang = DungChung.Ham.NgaySangChu(lupNgayden.DateTime, 1);
                }
                _dic.Add("Ngaythang", ngaythang);
                string ngayKy = "Bắc Giang, " + DungChung.Ham.NgaySangChu(DateTime.Now, 1);
                _dic.Add("NgayKy", ngayKy);
                DungChung.Ham.Print(DungChung.PrintConfig.Rep_TheoDoiPhanLoaiMuaThuoc24012, results, _dic, false);
                #endregion
            }
            else
            {
                MessageBox.Show("Không có dữ liệu!");
            }

        }
        public class _BC
        {
            public DateTime? NgayNhap { get; set; }
            public string strNgayNhap { get; set; }
            public string TenDV { get; set; }
            public string DonVi { get; set; }
            public string SoCT { get; set; }
            public double? SoLuongN_TT { get; set; }
            public double DonGia { get; set; }
            public double? ThanhTienN_TT { get; set; }
            public double? SoLuongN_NT { get; set; }
            public double? ThanhTienN_NT { get; set; }
            public bool? IsTrongThau { get; set; }

        }

        private void grvKhoXuat_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "ChonX")
            {
                if (grvKhoXuat.GetFocusedRowCellValue("tencc") != null)
                {
                    string Ten = grvKhoXuat.GetFocusedRowCellValue("tencc").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_NCC.First().chon == true)
                        {
                            foreach (var a in _NCC)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _NCC)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoXuat.DataSource = "";
                        grcKhoXuat.DataSource = _NCC.ToList();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtTimTenNCC_TextChanged(object sender, EventArgs e)
        {
            string tenNCC = "";
            if (!string.IsNullOrEmpty(txtTimTenNCC.Text))
                tenNCC = txtTimTenNCC.Text.ToLower();
            grcKhoXuat.DataSource = _NCC.Where(p => tenNCC== "" || p.tencc.ToLower().Contains(tenNCC)).OrderBy(p => p.macc).ThenBy(p => p.tencc);
        }
    }
}
