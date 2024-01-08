using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using QLBV_Database;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class Frm_BCDoanhThuKhoaPhong24006 : Form
    {
        public Frm_BCDoanhThuKhoaPhong24006()
        {
            InitializeComponent();
        }
        private class KPhongc
        {
            private string TenKP;
            private string PLoai;
            private int MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public string ploai
            { set { PLoai = value; } get { return PLoai; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        List<KPhong> _lkpall = new List<KPhong>();
        List<KPhongc> _Kphong = new List<KPhongc>();
        QLBVEntities _Data = new QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_BCDoanhThuKhoaPhong24006_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            _lkpall = _Data.KPhongs.Where(p => p.Status == 1).ToList();
            var kphong = (from kp in _lkpall
                          where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
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

            List<int> lkp = _Kphong.Where(p => p.chon == true).Select(p => p.makp).ToList();
            var qdv = (from dv in _Data.DichVus
                       join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       join nhom in _Data.NhomDVs on tn.IDNhom equals nhom.IDNhom
                       select new { dv.MaDV, dv.TenDV, nhom.TenNhomCT, nhom.IDNhom }).ToList();
            var qbn = (from bn in _Data.BenhNhans
                       join vp in _Data.VienPhis.Where(p => p.NgayDuyet >= _Ngaytu && p.NgayDuyet <= _Ngayden) on bn.MaBNhan equals vp.MaBNhan
                       join rv in _Data.RaViens on vp.MaBNhan equals rv.MaBNhan
                       group new { vp, rv, bn } by new { rv.MaKP } into kq
                       select new
                       {
                           kq.Key.MaKP,
                           SoNgaydtntbhyt = kq.Where(p => p.bn.DTuong == "BHYT" && p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt),
                           SoNgaydtdtntbhyt = kq.Where(p => p.bn.DTuong == "BHYT" && p.bn.DTNT == true).Sum(p => p.rv.SoNgaydt),
                           SoNgaydtntDV = kq.Where(p => p.bn.DTuong == "Dịch vụ" && p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt),
                           SoNgaydtdtntDV = kq.Where(p => p.bn.DTuong == "Dịch vụ" && p.bn.DTNT == true).Sum(p => p.rv.SoNgaydt),
                       }).ToList();

            var qlk = (from bn in _Data.BenhNhans
                       join vp in _Data.VienPhis.Where(p => p.NgayDuyet >= _Ngaytu && p.NgayDuyet <= _Ngayden) on bn.MaBNhan equals vp.MaBNhan
                       join bnkb in _Data.BNKBs on vp.MaBNhan equals bnkb.MaBNhan
                       group new { vp, bnkb, bn } by new { bnkb.MaKP } into kq
                       select new
                       {
                           kq.Key.MaKP,
                           LuotKhamBHYTNoiTru = kq.Where(p => p.bn.DTuong == "BHYT" && p.bn.NoiTru == 1).Select(p => p.bnkb.IDKB).Count(),
                           LuotKhamBHYTNgTru = kq.Where(p => p.bn.DTuong == "BHYT" && p.bn.NoiTru == 0 && p.bn.DTNT == false).Select(p => p.bnkb.IDKB).Count(),
                           LuotKhamBHYTDTNgTru = kq.Where(p => p.bn.DTuong == "BHYT" && p.bn.NoiTru == 0 && p.bn.DTNT == true).Select(p => p.bnkb.IDKB).Count(),
                           LuotKhamDVNoiTru = kq.Where(p => p.bn.DTuong == "Dịch vụ" && p.bn.NoiTru == 1).Select(p => p.bnkb.IDKB).Count(),
                           LuotKhamDVNgTru = kq.Where(p => p.bn.DTuong == "Dịch vụ" && p.bn.NoiTru == 0 && p.bn.DTNT == false).Select(p => p.bnkb.IDKB).Count(),
                           LuotKhamDVDTNgTru = kq.Where(p => p.bn.DTuong == "Dịch vụ" && p.bn.NoiTru == 0 && p.bn.DTNT == true).Select(p => p.bnkb.IDKB).Count(),
                           LuotKhamKSK = kq.Where(p => p.bn.DTuong == "KSK").Select(p => p.bnkb.IDKB).Count(),
                       }).ToList();

            var qvp = (from bn in _Data.BenhNhans
                       join vp in _Data.VienPhis.Where(p => p.NgayDuyet >= _Ngaytu && p.NgayDuyet <= _Ngayden) on bn.MaBNhan equals vp.MaBNhan
                       join vpct in _Data.VienPhicts.Where(p => p.TrongBH == 1 || p.TrongBH == 0) on vp.idVPhi equals vpct.idVPhi
                       join kp in _Data.KPhongs on vpct.MaKP equals kp.MaKP
                       select new { vpct.ThanhTien, vpct.TienBN, vpct.MaDV, vpct.MaKP, kp.TenKP, bn.MaBNhan, bn.TenBNhan, bn.NoiTru, bn.DTuong, bn.DTNT, vpct.TrongBH }).ToList();

            var q1 = (from vp in qvp
                      join kp in lkp on vp.MaKP equals kp
                      join kp1 in _Data.KPhongs on kp equals kp1.MaKP
                      join dv in qdv on vp.MaDV equals dv.MaDV
                      group new { vp, kp, dv } by new
                      {
                          vp.MaBNhan,
                          vp.TenBNhan,
                          kp,
                          kp1.PLoai,
                          vp.TenKP,
                          vp.NoiTru,
                          vp.DTuong,
                          vp.DTNT,
                          vp.TrongBH
                      } into kq
                      select new
                      {
                          kq.Key.TenKP,
                          Thuoc = kq.Where(p => p.dv.IDNhom == 4).Sum(p => p.vp.ThanhTien),
                          CDHA = kq.Where(p => p.dv.TenNhomCT == "Chẩn đoán hình ảnh").Sum(p => p.vp.ThanhTien),
                          TDCN = kq.Where(p => p.dv.IDNhom == 3).Sum(p => p.vp.ThanhTien),
                          Congkham = kq.Where(p => p.dv.TenNhomCT == "Khám bệnh").Sum(p => p.vp.ThanhTien),
                          Xetnghiem = kq.Where(p => p.dv.TenNhomCT == "Xét nghiệm").Sum(p => p.vp.ThanhTien),
                          Mau = kq.Where(p => p.dv.TenNhomCT == "Máu và chế phẩm của máu").Sum(p => p.vp.ThanhTien),
                          TTPT = kq.Where(p => p.dv.TenNhomCT == "Thủ thuật, phẫu thuật").Sum(p => p.vp.ThanhTien),
                          VTTH = kq.Where(p => p.dv.TenNhomCT == "Vật tư y tế trong danh mục BHYT").Sum(p => p.vp.ThanhTien),
                          VTTT = kq.Where(p => p.dv.TenNhomCT == "VTYT thanh toán theo tỷ lệ").Sum(p => p.vp.ThanhTien),
                          DVKTC = kq.Where(p => p.dv.TenNhomCT == "DVKT thanh toán theo tỷ lệ").Sum(p => p.vp.ThanhTien),
                          TTG = kq.Where(p => p.dv.TenNhomCT == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục").Sum(p => p.vp.ThanhTien),
                          Vanchuyen = kq.Where(p => p.dv.TenNhomCT == "Vận chuyển").Sum(p => p.vp.ThanhTien),
                          BNchitra = kq.Sum(p => p.vp.TienBN),
                          TienGiuong = kq.Key.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham? 0 : kq.Where(p => p.dv.TenNhomCT == "Chẩn đoán hình ảnh").Sum(p => p.vp.ThanhTien) * 1.32,
                          TongCP = kq.Sum(p => p.vp.ThanhTien),

                      }).ToList();
            var q2 = (from a in q1
                      group new { a } by new { a.TenKP } into kq
                      select new _BC
                      {
                          TenKP = kq.Key.TenKP,
                          HPThuocVT = kq.Sum(p => p.a.CDHA),
                          TienThuocVT = kq.Sum(p => p.a.CDHA) * 1.68 + kq.Sum(p => p.a.TDCN),
                          LoiNhuanA = kq.Sum(p => p.a.CDHA) * 1.68 + kq.Sum(p => p.a.TDCN) - kq.Sum(p => p.a.CDHA),
                          TienDV = kq.Sum(p => p.a.CDHA) * 1.28 + kq.Sum(p => p.a.TDCN),
                          HPHoaChat = kq.Sum(p => p.a.CDHA) * 1.12,
                          LoiNhuanB = kq.Sum(p => p.a.CDHA) * 1.28 + kq.Sum(p => p.a.TDCN) - kq.Sum(p => p.a.CDHA) * 1.12,
                          TienGiuong = kq.Sum(p => p.a.TienGiuong),
                          TongLoiNhuan = kq.Sum(p => p.a.CDHA) * 1.68 + kq.Sum(p => p.a.TDCN) - kq.Sum(p => p.a.CDHA) + kq.Sum(p => p.a.CDHA) * 1.28 + kq.Sum(p => p.a.TDCN) - kq.Sum(p => p.a.CDHA) * 1.12 + kq.Sum(p => p.a.TienGiuong),
                      }).OrderBy(p => p.TenKP).ToList();

            #region In mới

            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ);
            _dic.Add("TenCQ", DungChung.Bien.TenCQ);
            string ngaythang = "Từ ngày " + lupNgaytu.DateTime.ToShortDateString() + " đến ngày " + lupNgayden.DateTime.ToShortDateString();
            _dic.Add("Ngaythang", ngaythang);
            string ngayKy = DungChung.Ham.GetTenTinh(DungChung.Bien.MaBV) + ", " + DungChung.Ham.NgaySangChu(DateTime.Now, 1);
            _dic.Add("NgayKy", ngayKy);
            DungChung.Ham.Print(DungChung.PrintConfig.Rep_DoanhThuKhoaPhong24006, q2, _dic, false);
            #endregion
        }
        public class _BC
        {
            public string TenKP { get; set; }
            public double HPThuocVT { get; set; }
            public double TienThuocVT { get; set; }
            public double LoiNhuanA { get; set; }
            public double TienDV { get; set; }
            public double HPHoaChat { get; set; }
            public double LoiNhuanB { get; set; }
            public double TienGiuong { get; set; }
            public double TongLoiNhuan { get; set; }

        }
    }
}
