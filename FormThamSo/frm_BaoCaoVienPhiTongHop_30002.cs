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
    public partial class frm_BaoCaoVienPhiTongHop_30002 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BaoCaoVienPhiTongHop_30002()
        {
            InitializeComponent();
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private class KPhongc
        {
            private string TenKP;
            private int MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        List<KPhong> _lkpall = new List<KPhong>();
        List<KPhongc> _Kphong = new List<KPhongc>();
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BaoCaoVienPhiTongHop_30002_Load(object sender, EventArgs e)
        {
            //_Kphong = new List<KPhong>();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            _lkpall = _Data.KPhongs.Where(p => p.Status == 1).ToList();
            var kphong = (from kp in _lkpall
                          where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                          where (kp.Status == 1)
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhongc themmoi1 = new KPhongc();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhongc themmoi = new KPhongc();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
            rdNoiNgoaiTru.SelectedIndex = 2;
            rdBHYT.SelectedIndex = 3;
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //int _ngayBC = rdLoaiNgay.SelectedIndex;
            
            int _noitru = rdNoiNgoaiTru.SelectedIndex;

            string _dt = "";
            if (rdBHYT.SelectedIndex == 0)
            {
                _dt = "BHYT";
            }
            else if (rdBHYT.SelectedIndex == 1)
            {
                _dt = "Dịch vụ";
            }
            else if (rdBHYT.SelectedIndex == 2)
                _dt = "KSK";

            DateTime _Ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime _Ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);

            List<int> lkp = _Kphong.Where(p => p.chon == true).Select(p => p.makp).ToList();
            var qdv = (from dv in _Data.DichVus
                       join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       join nhom in _Data.NhomDVs on tn.IDNhom equals nhom.IDNhom
                       select new { dv.MaDV, dv.TenDV, nhom.TenNhomCT, nhom.IDNhom }).ToList();
            var qbn = (from bn in _Data.BenhNhans.Where(p => _dt == "" ? true : p.DTuong == _dt).Where(p => _noitru == 2 ? true : p.NoiTru == _noitru)
                       join vp in _Data.VienPhis.Where(p => rdLoaiNgay.SelectedIndex == 0 ? (p.NgayTT >= _Ngaytu && p.NgayTT <= _Ngayden) : (p.NgayDuyet >= _Ngaytu && p.NgayDuyet <= _Ngayden)) on bn.MaBNhan equals vp.MaBNhan
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

            var qlk = (from bn in _Data.BenhNhans.Where(p => _dt == "" ? true : p.DTuong == _dt).Where(p => _noitru == 2 ? true : p.NoiTru == _noitru)
                       join vp in _Data.VienPhis.Where(p => rdLoaiNgay.SelectedIndex == 0 ? (p.NgayTT >= _Ngaytu && p.NgayTT <= _Ngayden) : (p.NgayDuyet >= _Ngaytu && p.NgayDuyet <= _Ngayden)) on bn.MaBNhan equals vp.MaBNhan
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

            var qvp = (from bn in _Data.BenhNhans.Where(p => _dt == "" ? true : p.DTuong == _dt).Where(p => _noitru == 2 ? true : p.NoiTru == _noitru)
                       join vp in _Data.VienPhis.Where(p => rdLoaiNgay.SelectedIndex == 0 ? (p.NgayTT >= _Ngaytu && p.NgayTT <= _Ngayden) : (p.NgayDuyet >= _Ngaytu && p.NgayDuyet <= _Ngayden)) on bn.MaBNhan equals vp.MaBNhan
                       join vpct in _Data.VienPhicts.Where(p => ckcCPDinhKem.Checked ? true : (p.TrongBH == 1 || p.TrongBH == 0)) on vp.idVPhi equals vpct.idVPhi
                       join kp in _Data.KPhongs on vpct.MaKP equals kp.MaKP
                       select new { vpct.ThanhTien, vpct.TienBN, vpct.MaDV, vpct.MaKP, kp.TenKP, bn.MaBNhan, bn.TenBNhan, bn.NoiTru, bn.DTuong, bn.DTNT, vpct.TrongBH }).ToList();

            var q1 = (from vp in qvp
                      join kp in lkp on vp.MaKP equals kp
                      join dv in qdv on vp.MaDV equals dv.MaDV
                      group new { vp, kp, dv } by new
                      {
                          vp.MaBNhan,
                          vp.TenBNhan,
                          kp,
                          vp.TenKP,
                          vp.NoiTru,
                          vp.DTuong,
                          vp.DTNT,
                          vp.TrongBH
                      } into kq
                      select new
                      {
                          IDNhom1 = ( kq.Key.TrongBH==1|| kq.Key.TrongBH == 2) ? 1 : 2,
                          TenNhom1 = ( kq.Key.TrongBH == 1 || kq.Key.TrongBH == 2) ? "BHYT" : "Viện phí nhân dân",
                          IDNhom2 = kq.Key.DTuong == "KSK" ? 4 : (kq.Key.NoiTru == 1 ? 1 : ((kq.Key.NoiTru == 0 && kq.Key.DTNT == false) ? 2 : 3)),
                          TenNhom2 = kq.Key.DTuong == "KSK" ? "Khám sức khỏe" : (kq.Key.NoiTru == 1 ? ((kq.Key.TrongBH == 1 || kq.Key.TrongBH == 2) ? "Nội trú BHYT" : ("Nội trú")) : ((kq.Key.NoiTru == 0 && kq.Key.DTNT == false) ? ((kq.Key.TrongBH == 1 || kq.Key.TrongBH == 2) ? "Ngoại trú BHYT" : ("Ngoại trú")) : ((kq.Key.TrongBH == 1 || kq.Key.TrongBH == 2) ? "Điều trị ngoại trú BHYT" : ("Điều trị ngoại trú")))),//kq.Key.DTuong == "BHYT" ? (kq.Key.NoiTru == 1 ? "Nội trú BHYT" : (kq.Key.NoiTru == 0 && kq.Key.DTNT == false) ? "Ngoại trú BHYT" : "Điều trị ngoại trú BHYT") : (kq.Key.DTuong == "Dịch vụ" ? (kq.Key.NoiTru == 1 ? "Nội trú" : (kq.Key.NoiTru == 0 && kq.Key.DTNT == false) ? "Ngoại trú" : "Điều trị ngoại trú") : "Khám sức khỏe"),
                          Makp = kq.Key.kp,
                          Mabn = kq.Key.MaBNhan,
                          TenBN = kq.Key.TenBNhan,
                          kq.Key.NoiTru,
                          kq.Key.DTuong,
                          kq.Key.DTNT,
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
                          TongCP = kq.Sum(p => p.vp.ThanhTien),
                          BNchitra = kq.Sum(p => p.vp.TienBN),
                          TienGiuong = kq.Where(p => rdNoiNgoaiTru.SelectedIndex == 0 ? (p.dv.TenNhomCT == "Giường điều trị ngoại trú") : (p.dv.TenNhomCT == "Giường điều trị nội trú")).Sum(p => p.vp.ThanhTien),
                      }).ToList();
            var q2 = (from a in q1
                      group new { a } by new { a.IDNhom1, a.TenNhom1, a.IDNhom2, a.TenNhom2, a.Makp, a.TenKP } into kq
                      select new _BC
                      {
                          IDNhom1 = kq.Key.IDNhom1,
                          IDNhom2 = kq.Key.IDNhom2,
                          TenNhom1 = kq.Key.TenNhom1,
                          TenNhom2 = kq.Key.TenNhom2,
                          MakP = kq.Key.Makp,
                          TenKP = kq.Key.TenKP,
                          XetNghiem = kq.Sum(p => p.a.Xetnghiem),
                          CDHA = kq.Sum(p => p.a.CDHA),
                          TDCN = kq.Sum(p => p.a.TDCN),
                          ThuocDT = kq.Sum(p => p.a.Thuoc),
                          Mau = kq.Sum(p => p.a.Mau),
                          TTPT = kq.Sum(p => p.a.TTPT),
                          VTYTTieuHao = kq.Sum(p => p.a.VTTH),
                          VTYTTyLe = kq.Sum(p => p.a.VTTT),
                          DVKTC = kq.Sum(p => p.a.DVKTC),
                          TTG = kq.Sum(p => p.a.TTG),
                          CongKham = kq.Sum(p => p.a.Congkham),
                          VanChuyen = kq.Sum(p => p.a.Vanchuyen),
                          TienGiuong = kq.Sum(p => p.a.TienGiuong),
                          TongCP = kq.Sum(p => p.a.TongCP),
                          BNCungChiTra = kq.Sum(p => p.a.BNchitra)
                      }).OrderBy(p => p.IDNhom1).ThenBy(p => p.IDNhom2).ThenBy(p => p.TenKP).ToList();
            foreach (var item in q2)
            {
                if (item.IDNhom1 == 1)
                {
                    var songdt = qbn.Where(p => p.MaKP == item.MakP).FirstOrDefault();
                    if (songdt != null)
                    {
                        if (item.IDNhom2 == 1)
                        {
                            item.SoNgaydt = songdt.SoNgaydtntbhyt ?? 0;
                        }
                        else if (item.IDNhom2 == 3)
                        {
                            item.SoNgaydt = songdt.SoNgaydtdtntbhyt ?? 0;
                        }
                    }

                    var solk = qlk.Where(p => p.MaKP == item.MakP).FirstOrDefault();
                    if (solk != null)
                    {
                        if (item.IDNhom2 == 1)
                        {
                            item.SoLuotKham = solk.LuotKhamBHYTNoiTru;
                        }
                        else if (item.IDNhom2 == 2)
                        {
                            item.SoLuotKham = solk.LuotKhamBHYTNgTru;
                        }
                        else if (item.IDNhom2 == 3)
                        {
                            item.SoLuotKham = solk.LuotKhamBHYTDTNgTru;
                        }
                    }
                }
                else
                {
                    var songdt = qbn.Where(p => p.MaKP == item.MakP).FirstOrDefault();
                    if (songdt != null)
                    {
                        if (item.IDNhom2 == 1)
                        {
                            item.SoNgaydt = songdt.SoNgaydtntDV ?? 0;
                        }
                        else if (item.IDNhom2 == 3)
                        {
                            item.SoNgaydt = songdt.SoNgaydtdtntDV ?? 0;
                        }
                    }

                    var solk = qlk.Where(p => p.MaKP == item.MakP).FirstOrDefault();
                    if (solk != null)
                    {
                        if (item.IDNhom2 == 1)
                        {
                            item.SoLuotKham = solk.LuotKhamDVNoiTru;
                        }
                        else if (item.IDNhom2 == 2)
                        {
                            item.SoLuotKham = solk.LuotKhamDVNgTru;
                        }
                        else if (item.IDNhom2 == 3)
                        {
                            item.SoLuotKham = solk.LuotKhamDVDTNgTru;
                        }
                        else if (item.IDNhom2 == 4)
                        {
                            item.SoLuotKham = solk.LuotKhamKSK;
                        }
                    }
                }
            }
            frmIn frm = new frmIn();
            BaoCao.Rep_ThongkeCPTheoKP_30002 rep = new BaoCao.Rep_ThongkeCPTheoKP_30002();

            rep.paramTungayDenngay.Value = "Từ ngày " + lupNgaytu.DateTime.ToShortDateString() + " đến ngày " + lupNgayden.DateTime.ToShortDateString();
            rep.DataSource = q2;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        public class _BC
        {
            public int IDNhom1 { get; set; }
            public string TenNhom1 { get; set; }
            public int IDNhom2 { get; set; }
            public string TenNhom2 { get; set; }
            public int MakP { get; set; }
            public string TenKP { get; set; }
            public int? SoLuotKham { get; set; }
            public int SoNgaydt { get; set; }
            public double XetNghiem { get; set; }
            public double CDHA { get; set; }
            public double TDCN { get; set; }
            public double ThuocDT { get; set; }
            public double Mau { get; set; }
            public double TTPT { get; set; }
            public double VTYTTieuHao { get; set; }
            public double VTYTTyLe { get; set; }
            public double DVKTC { get; set; }
            public double TTG { get; set; }
            public double CongKham { get; set; }
            public double VanChuyen { get; set; }
            public double TienGiuong { get; set; }
            public double TongCP { get; set; }
            public double BNCungChiTra { get; set; }

        }

        private void grvKhoaphong_CellValueChanging_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
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
    }
}