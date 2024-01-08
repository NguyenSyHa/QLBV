using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using QLBV_Database;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class Frm_BCDanhSachBNTraThuocNgoaiTru : Form
    {
        public Frm_BCDanhSachBNTraThuocNgoaiTru()
        {
            InitializeComponent();
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
        List<KPhong> _lkpall = new List<KPhong>();
        List<KPhongc> _Kphong = new List<KPhongc>();
        List<KPhongc> _KXuat = new List<KPhongc>();
        QLBVEntities _Data = new QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_BCDoanhThuKhoaPhong24006_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            _lkpall = _Data.KPhongs.Where(p => p.Status == 1).ToList();
            var kphong = (from kp in _lkpall
                          where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
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

            // kho xuất
            var khoXuat = (from kp in _lkpall
                          where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc)
                          where (kp.Status == 1)
                          select new { kp.TenKP, kp.MaKP, kp.PLoai }).ToList();
            if (khoXuat.Count > 0)
            {
                KPhongc themmoi1 = new KPhongc();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                themmoi1.ploai = "Khoa dược";
                _KXuat.Add(themmoi1);
                foreach (var a in khoXuat)
                {
                    KPhongc themmoi = new KPhongc();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    themmoi.ploai = a.PLoai;
                    _KXuat.Add(themmoi);
                }
                grcKhoXuat.DataSource = _KXuat.ToList();
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
            List<int?> lkx = _KXuat.Where(p => p.chon == true).Select(p => p.makp).ToList();
            var listND = (from nd in _Data.NhapDs
                            join ndct in _Data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                            join dv in _Data.DichVus on ndct.MaDV equals dv.MaDV
                            join bn in _Data.BenhNhans on nd.MaBNhan equals bn.MaBNhan
                            where(nd.NgayNhap > _Ngaytu && nd.NgayNhap < _Ngayden)
                            where(ndct.SoLuongX < 0)
                            where lkp.Contains(nd.MaKPnx)
                            where lkx.Contains(nd.MaKP)
                            select new
                            {
                                TenBNhan = bn.TenBNhan,
                                SThe = bn.SThe,
                                NamSinh = bn.NamSinh,
                                bn.ThangSinh,
                                bn.NgaySinh,
                                nd.MaBNhan,
                                nd.NgayNhap,
                                dv.MaTam,
                                dv.TenDV,
                                ndct.SoLuongX,
                                ndct.DonGia,
                                ndct.ThanhTienX,
                            }).OrderBy(p => p.NgayNhap).ToList();
            List<_BC> results = new List<_BC>();
            if (listND.Count > 0)
            {
                foreach (var item in listND)
                {
                    _BC a = new _BC();
                    a.TenBNhan = item.TenBNhan;
                    a.MaBNhan = item.MaBNhan;
                    a.SThe = item.SThe;
                    a.NamSinh = !string.IsNullOrWhiteSpace(item.NgaySinh) ? (item.NgaySinh + "/" + item.ThangSinh + "/" + item.NamSinh) : !string.IsNullOrWhiteSpace(item.ThangSinh) ? (item.ThangSinh + "/" + item.NamSinh) : item.NamSinh;
                    a.strNgayNhap = DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.NgayNhap), 7);
                    a.MaTam = item.MaTam;
                    a.TenDV = item.TenDV;
                    a.SoLuongX = -item.SoLuongX;
                    a.DonGia = item.DonGia;
                    a.ThanhTienX = -item.ThanhTienX;
                    results.Add(a);
                }
                #region In mới
                var kp = _KXuat.Where(p => p.chon == true).Where(p => p.makp != 0).Select(p => p.tenkp).ToList();
                string KP = "";
                if (kp.Count > 0)
                    KP = string.Join("; ", kp);
                Dictionary<string, object> _dic = new Dictionary<string, object>();
                _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ.ToUpper());
                _dic.Add("TenCQ", DungChung.Bien.TenCQ.ToUpper());
                _dic.Add("TenKP", KP);
                string ngaythang = "Từ ngày " + lupNgaytu.DateTime.ToShortDateString() + " đến ngày " + lupNgayden.DateTime.ToShortDateString();
                _dic.Add("Ngaythang", ngaythang);
                string ngayKy = DungChung.Ham.GetTenTinh(DungChung.Bien.MaBV) + ", " + DungChung.Ham.NgaySangChu(DateTime.Now, 1);
                _dic.Add("NgayKy", ngayKy);
                DungChung.Ham.Print(DungChung.PrintConfig.Rep_DSBNTraThuocNgoaiTru, results, _dic, false);
                #endregion
            }
            else
            {
                MessageBox.Show("Không có dữ liệu!");
            }

        }
        public class _BC
        {
            public string TenBNhan { get; set; }
            public int? MaBNhan { get; set; }
            public string SThe { get; set; }
            public string NamSinh { get; set; }
            public DateTime? NgayNhap { get; set; }
            public string strNgayNhap { get; set; }
            public string MaTam { get; set; }
            public string TenDV { get; set; }
            public double SoLuongX { get; set; }
            public double DonGia { get; set; }
            public double ThanhTienX { get; set; }

        }

        private void grvKhoXuat_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "ChonX")
            {
                if (grvKhoXuat.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoXuat.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_KXuat.First().chon == true)
                        {
                            foreach (var a in _KXuat)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _KXuat)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoXuat.DataSource = "";
                        grcKhoXuat.DataSource = _KXuat.ToList();
                    }
                }
            }
        }
    }
}
