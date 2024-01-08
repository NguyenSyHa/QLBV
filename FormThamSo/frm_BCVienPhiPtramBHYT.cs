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
    public partial class frm_BCVienPhiPtramBHYT : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCVienPhiPtramBHYT()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data;
        List<KPhong> _lKPhong = new List<KPhong>();
        private void frm_BCVienPhiPtramBHYT_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lKPhong = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).OrderBy(p => p.TenKP).ToList();           
            _lKPhong.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả", PLoai = "Tất cả" });
            _lKPhong.Insert(1, new KPhong { MaKP = 1000, TenKP = "Ngoại trú" });
            _lKPhong.Insert(2, new KPhong { MaKP = 1001, TenKP = "Khám sức khỏe" });

            cklKhoaPhong.DataSource = _lKPhong;
            cklKhoaPhong.CheckAll();
            detungay.DateTime = DateTime.Now;
            dedenngay.DateTime = DateTime.Now;
        }

        private void btntaobc_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime _tungay = DungChung.Ham.NgayTu(detungay.DateTime);
            DateTime _denngay = DungChung.Ham.NgayDen(dedenngay.DateTime);
            List<int> _lkp = new List<int>();
            for (int i = 0; i < cklKhoaPhong.ItemCount; i++)
            {
                if (cklKhoaPhong.GetItemChecked(i))
                    _lkp.Add(Convert.ToInt32(cklKhoaPhong.GetItemValue(i)));
            }
            int _TrongDM = rgCHon.SelectedIndex;

            var _lkpnew = (from kp in _lKPhong
                           join k in _lkp on kp.MaKP equals k
                           select new { kp.MaKP, kp.TenKP, kp.PLoai }).Distinct().ToList();

            var dv_tn = (from a in data.DichVus
                         join b in data.TieuNhomDVs on a.IdTieuNhom equals b.IdTieuNhom
                         join c in data.NhomDVs on b.IDNhom equals c.IDNhom
                         select new
                         {
                             a.MaDV,
                             b.TenRG,
                             c.TenNhom,
                             c.TenNhomCT,
                             a.DongY,
                             c.IDNhom
                         }).ToList();
            if (rgchonmau.SelectedIndex == 0)
            {
                #region bn BHYT, bn Dich vu
                var _lvp = (from vp in data.VienPhis.Where(p => p.NgayDuyet >= _tungay && p.NgayDuyet <= _denngay)
                            join vpct in data.VienPhicts.Where(P=>P.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                            join rv in data.RaViens on vp.MaBNhan equals rv.MaBNhan
                            join bn in data.BenhNhans.Where(p => (ckBHYT.Checked && p.DTuong == "BHYT") || (ckDichVu.Checked && p.DTuong == "Dịch vụ")) on rv.MaBNhan equals bn.MaBNhan
                            join tu in data.TamUngs.Where(p => p.PhanLoai == 1) on vp.MaBNhan equals tu.MaBNhan
                            select new
                            {
                                vpct.MaDV,
                                rv.MaKP,
                                vpct.TienBN,
                                vpct.TrongBH,
                                vpct.idVPhict,
                                vp.MaBNhan
                            }).ToList();
                var _lsp = (from a in _lvp
                            join kp in data.KPhongs on a.MaKP equals kp.MaKP
                            select new
                            {
                                a.MaDV,
                                a.MaBNhan,
                                a.TienBN,
                                a.idVPhict,
                                a.TrongBH,
                                MaKP = (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) ? 1000 : a.MaKP,
                                TenKP = (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) ? "Ngoại trú" : kp.TenKP
                            }).ToList();

                var _lkqua = (from vp in _lsp.Where(p => _TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : (_TrongDM == 2 ? (p.TrongBH != 0 && p.TrongBH != 1) : true)))
                              join dv in dv_tn on vp.MaDV equals dv.MaDV
                              join kp in _lkpnew on vp.MaKP equals kp.MaKP
                              group new { vp, dv, kp } by new { vp.MaKP, vp.TenKP } into kq
                              select new RP
                              {
                                  TenKP = kq.Key.TenKP,
                                  MaKP = kq.Key.MaKP,
                                  SoLuot = kq.Select(p => p.vp.MaBNhan).Distinct().Count(),
                                  ThuocThuong = kq.Where(p => p.dv.IDNhom == 4 || p.dv.IDNhom == 5 || p.dv.IDNhom == 6).Sum(p => p.vp.TienBN) - kq.Where(p => p.dv.DongY == 1).Sum(p => p.vp.TienBN),
                                  ThuocDongY = kq.Where(p => p.dv.DongY == 1).Sum(p => p.vp.TienBN),
                                  VatTu = kq.Where(p => p.dv.IDNhom == 10 || p.dv.IDNhom == 11).Sum(p => p.vp.TienBN),
                                  GiuongBenh = kq.Where(p => p.dv.IDNhom == 14 || p.dv.IDNhom == 15).Sum(p => p.vp.TienBN),
                                  KhamBenh = kq.Where(p => p.dv.IDNhom == 13).Sum(p => p.vp.TienBN),
                                  XetNghiem = kq.Where(p =>DungChung.Bien.MaBV == "30003" ?( p.dv.IDNhom == 1 || p.dv.IDNhom == 7) : p.dv.IDNhom == 1).Sum(p => p.vp.TienBN),
                                  XQuang = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.TienBN),
                                  SieuAm = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.vp.TienBN),
                                  DienTim = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Sum(p => p.vp.TienBN),
                                  ThuThuat = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Sum(p => p.vp.TienBN),
                                  Khac = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Sum(p => p.vp.TienBN),
                                  Tong = kq.Sum(p => p.vp.TienBN)
                              }).ToList();
                List<RP> ketqua = (from k in _lkqua
                              group new { k } by new { k.MaKP, k.TenKP } into kq
                              select new RP
                              {
                                  MaKP = kq.Key.MaKP,
                                  TenKP = kq.Key.TenKP,
                                  SoLuot = kq.Sum(p => p.k.SoLuot),
                                  ThuocThuong = kq.Sum(p => p.k.ThuocThuong),
                                  ThuocDongY = kq.Sum(p => p.k.ThuocDongY),
                                  VatTu = kq.Sum(p => p.k.VatTu),
                                  GiuongBenh = kq.Sum(p => p.k.GiuongBenh),
                                  KhamBenh = kq.Sum(p => p.k.KhamBenh),
                                  XetNghiem = kq.Sum(p => p.k.XetNghiem),
                                  XQuang = kq.Sum(p => p.k.XQuang),
                                  SieuAm = kq.Sum(p => p.k.SieuAm),
                                  DienTim = kq.Sum(p => p.k.DienTim),
                                  ThuThuat = kq.Sum(p => p.k.ThuThuat),//nội soi thay cho cột khác
                                  Khac = kq.Sum(p => p.k.Khac),//kq.Sum(p => p.k.Tong) - (kq.Sum(p => p.k.ThuocThuong) + kq.Sum(p => p.k.ThuocDongY) + kq.Sum(p => p.k.VatTu) + kq.Sum(p => p.k.GiuongBenh) + kq.Sum(p => p.k.KhamBenh) + kq.Sum(p => p.k.XetNghiem) + kq.Sum(p => p.k.XQuang) + kq.Sum(p => p.k.SieuAm) + kq.Sum(p => p.k.DienTim) + kq.Sum(p => p.k.ThuThuat)),
                                  Tong = kq.Sum(p => p.k.Tong)
                              }).OrderBy(p => p.TenKP).ToList();
                #endregion
                #region BNKSK -- Lấy tổng chi phí của tất cả bệnh nhân có phân loại = 1 or 2 trong bảng tạm ứng
                List<int> lbnDaDuyet = (from bn in data.BenhNhans.Where(p => ckKSK.Checked && p.DTuong == "KSK")
                                        join tu in data.TamUngs.Where(p => p.NgayThu >= _tungay && p.NgayThu <= _denngay).Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).Where(p => rgCHon.SelectedIndex == 1 || rgCHon.SelectedIndex == 3)
                                        on bn.MaBNhan equals tu.MaBNhan
                                        select bn.MaBNhan).ToList();

                //Lấy tất cả chi phí thu thẳng cho các dịch vụ ( có join với bảng tamungct-vì có bn có chi phí thu thẳng nhưng ko join với bảng tamungct) làm các chi chi phí theo dịch vụ + ngoài ra là tiền khám

         

                #region Lấy chi phí không phải thu thẳng theo dịch vụ (Không join với bảng tạm ứng chi tiết (bao gồm cả PhanLoai = 3)

                var qThanhToan = (from bn in data.BenhNhans.Where(p => _TrongDM == 0 ? false : true).Where(p => lbnDaDuyet.Contains(p.MaBNhan))
                                  join tu in data.TamUngs.Where(p=>p.PhanLoai !=3) on bn.MaBNhan equals tu.MaBNhan                                 
                                  select new { bn.MaBNhan, tu.PhanLoai, tu.SoTien, MaKP = 1001, TenKP = "Khám sức khỏe" }).ToList();
                var qThanhToan1 = (from vp in qThanhToan
                                   join kp in _lkpnew on vp.MaKP equals kp.MaKP
                                   group new { vp, kp } by new { vp.MaBNhan, vp.MaKP, vp.TenKP } into kq
                                   select new RP
                                   {
                                       TenKP = kq.Key.TenKP,
                                       MaKP = kq.Key.MaKP,
                                       MaBNhan = kq.Key.MaBNhan,
                                       KhamBenh = kq.Where(p => p.vp.PhanLoai == 0 || p.vp.PhanLoai == 1 ).Sum(p => p.vp.SoTien ?? 0) - kq.Where(p => p.vp.PhanLoai == 2).Sum(p => p.vp.SoTien ?? 0)
                                     
                                   }).ToList();


                #endregion
                #region Lấy tổng CP của BN KSK
                foreach (RP rp in qThanhToan1) //qThanhToan1 đã bao gồm tất cả bệnh nhân khám sức khỏe
                {               
                        rp.Tong = rp.KhamBenh;                  
                }
                List<RP> qCPKSK = (from vp in qThanhToan1
                                    group vp by new { vp.MaKP, vp.TenKP } into kq
                                    select new RP
                                    {
                                        TenKP = kq.Key.TenKP,
                                        MaKP = kq.Key.MaKP,
                                        SoLuot = kq.Select(p => p.MaBNhan).Distinct().Count(),
                                        ThuocThuong = kq.Sum(p => p.ThuocThuong),
                                        ThuocDongY = kq.Sum(p => p.ThuocDongY),
                                        VatTu = kq.Sum(p => p.VatTu),
                                        GiuongBenh = kq.Sum(p => p.GiuongBenh),
                                        KhamBenh = kq.Sum(p => p.KhamBenh),
                                        XetNghiem = kq.Sum(p => p.XetNghiem),
                                        XQuang = kq.Sum(p => p.XQuang),
                                        SieuAm = kq.Sum(p => p.SieuAm),
                                        DienTim = kq.Sum(p => p.DienTim),
                                        ThuThuat = kq.Sum(p => p.ThuThuat),
                                        Khac = kq.Sum(p=>p.Khac),
                                        Tong = kq.Sum(p => p.Tong),
                                    }).ToList();
                ketqua.AddRange(qCPKSK);

                #endregion
                #endregion
                frmIn frm = new frmIn();
                BaoCao.rep_BCVienPhiPTramBHYT rep = new BaoCao.rep_BCVienPhiPTramBHYT();
                rep.TIEUDE.Value = "BÁO CÁO THU VIỆN PHÍ";
                double tongtien = ketqua.Count > 0 ? ketqua.Sum(p => p.Tong) : 0;
                rep.tongtien.Value = DungChung.Ham.DocTienBangChu(tongtien, " đồng");
                rep.cqcq.Value = DungChung.Bien.TenCQCQ.ToUpper();
                rep.tencq.Value = DungChung.Bien.TenCQ.ToUpper();
                rep.NGAYTHANG.Value = "Từ ngày: " + _tungay.ToShortDateString() + " đến ngày: " + _denngay.ToShortDateString();
                rep.DataSource = ketqua;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                var _lvp = (from vp in data.TamUngs.Where(p => p.NgayThu >= _tungay && p.NgayThu <= _denngay).Where(p => p.PhanLoai == 3)
                            join vpct in data.TamUngcts.Where(p => p.Status == 0) on vp.IDTamUng equals vpct.IDTamUng
                            join bn in data.BenhNhans.Where(p => (ckBHYT.Checked && p.DTuong == "BHYT") || (ckDichVu.Checked && p.DTuong == "Dịch vụ")) on vp.MaBNhan equals bn.MaBNhan
                            select new
                            {
                                vpct.MaDV,
                                vp.MaKP,
                                vpct.SoTien,
                                vpct.TrongBH,
                                vpct.IDTamUngct,
                                vp.MaBNhan
                            }).ToList();
                var _lsp = (from a in _lvp
                            join kp in data.KPhongs on a.MaKP equals kp.MaKP
                            select new
                            {
                                a.MaDV,
                                a.MaBNhan,
                                a.SoTien,
                                a.IDTamUngct,
                                a.TrongBH,
                                MaKP = (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) ? 1000 : a.MaKP,
                                TenKP = (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) ? "Ngoại trú" : kp.TenKP
                            }).ToList();
                //if (rgCHon.SelectedIndex == 0)
                //{
                var _lkqua = (from vp in _lsp.Where(p => _TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : (_TrongDM == 2 ? (p.TrongBH != 0 && p.TrongBH != 1) : true)))
                              join dv in dv_tn on vp.MaDV equals dv.MaDV
                              join kp in _lkpnew on vp.MaKP equals kp.MaKP
                              group new { vp, dv, kp } by new { vp.MaKP, vp.TenKP } into kq
                              select new RP
                              {
                                  TenKP = kq.Key.TenKP,
                                  MaKP = kq.Key.MaKP,
                                  SoLuot = kq.Select(p => p.vp.MaBNhan).Distinct().Count(),
                                  ThuocThuong = kq.Where(p => p.dv.IDNhom == 4 || p.dv.IDNhom == 5 || p.dv.IDNhom == 6).Sum(p => p.vp.SoTien) - kq.Where(p => p.dv.DongY == 1).Sum(p => p.vp.SoTien),
                                  ThuocDongY = kq.Where(p => p.dv.DongY == 1).Sum(p => p.vp.SoTien),
                                  VatTu = kq.Where(p => p.dv.IDNhom == 10 || p.dv.IDNhom == 11).Sum(p => p.vp.SoTien),
                                  GiuongBenh = kq.Where(p => p.dv.IDNhom == 14 || p.dv.IDNhom == 15).Sum(p => p.vp.SoTien),
                                  KhamBenh = kq.Where(p => p.dv.IDNhom == 13).Sum(p => p.vp.SoTien),
                                  XetNghiem = kq.Where(p =>DungChung.Bien.MaBV == "30003" ?( p.dv.IDNhom == 1 || p.dv.IDNhom == 7) : p.dv.IDNhom == 1).Sum(p => p.vp.SoTien),
                                  XQuang = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.SoTien),
                                  SieuAm = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.vp.SoTien),
                                  DienTim = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Sum(p => p.vp.SoTien),
                                  ThuThuat = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Sum(p => p.vp.SoTien),
                                  Khac = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Sum(p => p.vp.SoTien),
                                  Tong = kq.Sum(p => p.vp.SoTien)
                              }).ToList();
                var ketqua = (from k in _lkqua
                              group new { k } by new { k.MaKP, k.TenKP } into kq
                              select new RP
                              {
                                  MaKP = kq.Key.MaKP,
                                  TenKP = kq.Key.TenKP,
                                  SoLuot = kq.Sum(p => p.k.SoLuot),
                                  ThuocThuong = kq.Sum(p => p.k.ThuocThuong),
                                  ThuocDongY = kq.Sum(p => p.k.ThuocDongY),
                                  VatTu = kq.Sum(p => p.k.VatTu),
                                  GiuongBenh = kq.Sum(p => p.k.GiuongBenh),
                                  KhamBenh = kq.Sum(p => p.k.KhamBenh),
                                  XetNghiem = kq.Sum(p => p.k.XetNghiem),
                                  XQuang = kq.Sum(p => p.k.XQuang),
                                  SieuAm = kq.Sum(p => p.k.SieuAm),
                                  DienTim = kq.Sum(p => p.k.DienTim),
                                  ThuThuat = kq.Sum(p => p.k.ThuThuat),
                                  Khac = kq.Sum(p => p.k.Khac),// kq.Sum(p => p.k.Tong) - (kq.Sum(p => p.k.ThuocThuong) + kq.Sum(p => p.k.ThuocDongY) + kq.Sum(p => p.k.VatTu) + kq.Sum(p => p.k.GiuongBenh) + kq.Sum(p => p.k.KhamBenh) + kq.Sum(p => p.k.XetNghiem) + kq.Sum(p => p.k.XQuang) + kq.Sum(p => p.k.SieuAm) + kq.Sum(p => p.k.DienTim) + kq.Sum(p => p.k.ThuThuat)),
                                  Tong = kq.Sum(p => p.k.Tong)
                              }).OrderBy(p => p.TenKP).ToList();
                #region lấy tất cả chi phí (phân loại = 0,1,2,3)-- đang bỏ vì làm cho 30003

               


                #region BNKSK -- Lấy tổng chi phí của tất cả bệnh nhân KSK theo ngày thu (chỉ lấy chi phí thu thảng dịch vụ
                List<int> lbnDaDuyet = (from bn in data.BenhNhans.Where(p => ckKSK.Checked && p.DTuong == "KSK")
                                        join tu in data.TamUngs.Where(p => p.NgayThu >= _tungay && p.NgayThu <= _denngay).Where(p => rgCHon.SelectedIndex == 1 || rgCHon.SelectedIndex == 3)
                                        on bn.MaBNhan equals tu.MaBNhan
                                        select bn.MaBNhan).ToList();

                //Lấy tất cả chi phí thu thẳng cho các dịch vụ ( có join với bảng tamungct-vì có bn có chi phí thu thẳng nhưng ko join với bảng tamungct) làm các chi chi phí theo dịch vụ + ngoài ra là tiền khám

                #region Lấy chi phí thu thẳng theo dịch vụ (có join với bảng tạm ứng chi tiết)
                var qCPThuThang = (from bn in data.BenhNhans.Where(p => _TrongDM == 0 ? false : true).Where(p => lbnDaDuyet.Contains(p.MaBNhan))
                                   join tu in data.TamUngs.Where(p => p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan
                                   join tuct in data.TamUngcts.Where(p =>p.Status == 0) on tu.IDTamUng equals tuct.IDTamUng
                                   select new { bn.MaBNhan, tu.PhanLoai, tuct.MaDV,ThanhTien = tuct.SoTien, MaKP = 1001, TenKP = "Khám sức khỏe" }).ToList();
                var qCPThuThang1 = (from vp in qCPThuThang
                                    join dv in dv_tn on vp.MaDV equals dv.MaDV
                                    join kp in _lkpnew on vp.MaKP equals kp.MaKP
                                    group new { vp, dv, kp } by new { vp.MaBNhan, vp.MaKP, vp.TenKP } into kq
                                    select new RP
                                    {
                                        TenKP = kq.Key.TenKP,
                                        MaKP = kq.Key.MaKP,
                                        MaBNhan = kq.Key.MaBNhan,
                                        ThuocThuong = kq.Where(p => p.dv.IDNhom == 4 || p.dv.IDNhom == 5 || p.dv.IDNhom == 6).Sum(p => p.vp.ThanhTien) - kq.Where(p => p.dv.DongY == 1).Sum(p => p.vp.ThanhTien),
                                        ThuocDongY = kq.Where(p => p.dv.DongY == 1).Sum(p => p.vp.ThanhTien),
                                        VatTu = kq.Where(p => p.dv.IDNhom == 10 || p.dv.IDNhom == 11).Sum(p => p.vp.ThanhTien),
                                        GiuongBenh = kq.Where(p => p.dv.IDNhom == 14 || p.dv.IDNhom == 15).Sum(p => p.vp.ThanhTien),
                                        KhamBenh = kq.Where(p => p.dv.IDNhom == 13).Sum(p => p.vp.ThanhTien),
                                        XetNghiem = kq.Where(p => DungChung.Bien.MaBV == "30003" ? (p.dv.IDNhom == 1 || p.dv.IDNhom == 7) : p.dv.IDNhom == 1).Sum(p => p.vp.ThanhTien),
                                        XQuang = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.ThanhTien),
                                        SieuAm = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.vp.ThanhTien),
                                        DienTim = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Sum(p => p.vp.ThanhTien),
                                        ThuThuat = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Sum(p => p.vp.ThanhTien),
                                        Khac = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Sum(p => p.vp.ThanhTien),
                                        Tong = kq.Sum(p => p.vp.ThanhTien)
                                    }).ToList();
                #endregion

             
                #region Lấy tổng CP của BN KSK
                List<RP> qCPKSK = (from vp in qCPThuThang1
                                   group vp by new { vp.MaKP, vp.TenKP } into kq
                                   select new RP
                                   {
                                       TenKP = kq.Key.TenKP,
                                       MaKP = kq.Key.MaKP,
                                       SoLuot = kq.Select(p => p.MaBNhan).Distinct().Count(),
                                       ThuocThuong = kq.Sum(p => p.ThuocThuong),
                                       ThuocDongY = kq.Sum(p => p.ThuocDongY),
                                       VatTu = kq.Sum(p => p.VatTu),
                                       GiuongBenh = kq.Sum(p => p.GiuongBenh),
                                       KhamBenh = kq.Sum(p => p.KhamBenh),
                                       XetNghiem = kq.Sum(p => p.XetNghiem),
                                       XQuang = kq.Sum(p => p.XQuang),
                                       SieuAm = kq.Sum(p => p.SieuAm),
                                       DienTim = kq.Sum(p => p.DienTim),
                                       ThuThuat = kq.Sum(p => p.ThuThuat),
                                       Khac = kq.Sum(p => p.Khac),
                                       Tong = kq.Sum(p => p.Tong),
                                   }).ToList();
                ketqua.AddRange(qCPKSK);

                #endregion
                #endregion
                #endregion

                frmIn frm = new frmIn();
                BaoCao.rep_BCVienPhiPTramBHYT rep = new BaoCao.rep_BCVienPhiPTramBHYT();
                rep.TIEUDE.Value = "BÁO CÁO THU VIỆN PHÍ";
                double tongtien = ketqua.Count > 0 ? ketqua.Sum(p => p.Tong) : 0;
                rep.tongtien.Value = DungChung.Ham.DocTienBangChu(tongtien, " đồng");
                rep.cqcq.Value = DungChung.Bien.TenCQCQ.ToUpper();
                rep.tencq.Value = DungChung.Bien.TenCQ.ToUpper();
                rep.NGAYTHANG.Value = "Từ ngày: " + _tungay.ToShortDateString() + " đến ngày: " + _denngay.ToShortDateString();
                rep.DataSource = ketqua;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
          
        }

        private void cklKhoaPhong_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklKhoaPhong.GetItemChecked(0) == true)
                    cklKhoaPhong.CheckAll();
                else
                    cklKhoaPhong.UnCheckAll();
            }
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rgDTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ckBHYT.Checked)
            {
                rgCHon.SelectedIndex = 0;
                rgCHon.Enabled = true;
                //rgchonmau.SelectedIndex = 0;
                //rgchonmau.Enabled = false;

            }
            else
            {
                //if (rgDTuong.SelectedIndex == 1)
                //{
                //    rgchonmau.SelectedIndex = 0;
                //    rgchonmau.Enabled = true;
                //}
                //else
                //{
                //    rgchonmau.SelectedIndex = 0;
                //    rgchonmau.Enabled = false;
                //}
                rgCHon.SelectedIndex = 2;
                rgCHon.Enabled = false;
            }
        }

        private void rgCHon_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cklKhoaPhong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    class RP
    {
        public int? MaKP { get; set; }

        public string TenKP { get; set; }

        public int SoLuot { get; set; }

        public double ThuocThuong { get; set; }

        public double ThuocDongY { get; set; }

        public double VatTu { get; set; }

        public double GiuongBenh { get; set; }

        public double KhamBenh { get; set; }

        public double XetNghiem { get; set; }

        public double XQuang { get; set; }

        public double SieuAm { get; set; }

        public double DienTim { get; set; }

        public double ThuThuat { get; set; }

        public double Khac { get; set; }

        public double Tong { get; set; }

        public int MaBNhan { get; set; }
    }
}