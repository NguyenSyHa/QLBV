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
    public partial class frm_BC_KQHD_27022 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_KQHD_27022()
        {
            InitializeComponent();
        }

        private void frm_BC_KQHD_27022_Load(object sender, EventArgs e)
        {
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<NoiDung> _lND = new List<NoiDung>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            DateTime tungaythangtruoc = tungay.AddMonths(-1);
            DateTime denngaythangtruoc = denngay.AddMonths(-1);
            _lND.Clear();
            NoiDung moi = new NoiDung();
            #region select
            var qBNRV = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                         join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                         join rv in data.RaViens on vv.MaBNhan equals rv.MaBNhan into k
                         from k1 in k.DefaultIfEmpty()
                         select new
                         {
                             bn.MaBNhan,
                             bn.TenBNhan,
                             bn.SThe,
                             SoNgaydt = k1 != null ? k1.SoNgaydt : null,
                             KetQua = k1 != null ? k1.KetQua : null,
                             bn.Tuoi,
                             MaICD = k1 != null ? (k1.MaICD.Contains(";") == false ? k1.MaICD : k1.MaICD.Substring(0, k1.MaICD.IndexOf(";"))) : null,
                             NgayRa = k1 != null ? k1.NgayRa : null
                         }).ToList();
            var qDV = (from dv in data.DichVus.Where(p => p.PLoai == 2)
                       join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new { dv.MaDV, tn.TenRG, ndv.TenNhomCT, dv.TenDV }).ToList();
            var qDThuoc = (from dt in data.DThuocs
                           join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                           select new { dt.MaBNhan, dtct.MaDV, dtct.NgayNhap, dt.IDDon, dtct.IDCD }).ToList();
            //số lần khám bệnh của bệnh nhân ngoại trú
            var qKhamNgTru = (from bn in data.BenhNhans.Where(p => p.NoiTru == 0)
                              join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                              join kp in data.KPhongs.Where(p => p.PLoai.Equals("Phòng khám")) on bnkb.MaKP equals kp.MaKP
                              select new { bnkb.MaBNhan, bn.SThe, bnkb.NgayKham, bnkb.PhuongAn }).Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).ToList();
            //số lần khám của bệnh nhân nội trú
            var qKhamNTru = (from bn in qBNRV.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay)
                             join dt in qDThuoc on bn.MaBNhan equals dt.MaBNhan
                             join dv in qDV.Where(p => p.TenRG.ToLower().Contains("ngày giường")) on dt.MaDV equals dv.MaDV
                             group new { bn, dt, dv } by new { bn.MaBNhan, bn.TenBNhan, bn.SThe, bn.SoNgaydt, bn.KetQua, bn.Tuoi } into kq
                             select new { kq.Key.MaBNhan, kq.Key.TenBNhan, kq.Key.SThe, kq.Key.SoNgaydt, kq.Key.KetQua, kq.Key.Tuoi }).ToList();
            //bệnh nhân tháng trước còn lại
            var qBNDTNoiTru = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                               join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                               join rv in data.RaViens on vv.MaBNhan equals rv.MaBNhan into q
                               from q1 in q.DefaultIfEmpty()
                               select new { bn.MaBNhan, bn.TenBNhan, bn.SThe, q1.NgayRa, vv.NgayVao }).ToList();

            var bnkb1 = (from a in data.BenhNhans.Where(p => p.NoiTru == 1)
                         join b in data.BNKBs.Where(p => p.NgayKham <= denngay) on a.MaBNhan equals b.MaBNhan
                         group new { a, b } by new { a.MaBNhan, a.SThe, a.NoiTru } into kq
                         select new { kq.Key.MaBNhan, kq.Key.SThe, IDKB = kq.Max(p => p.b.IDKB), kq.Key.NoiTru }).ToList();

            //bệnh nhân thực hiện CLS và phẫu thuật
            var qBNTHDV = (from bn in bnkb1
                           join dt in qDThuoc.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.IDCD == null) on bn.MaBNhan equals dt.MaBNhan
                           join dv in qDV on dt.MaDV equals dv.MaDV
                           join a in data.BNKBs on bn.IDKB equals a.IDKB
                           select new { bn.MaBNhan, bn.SThe, dv.TenRG, dv.TenNhomCT, dv.TenDV, a.MaICD, IDCD = dt.IDDon, NgayTH = dt.NgayNhap.Value.Date }).ToList();

            var qCLS = (from cls in data.CLS
                        join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                        join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                        select new { cd.MaDV, clsct.KetQua, cls.MaBNhan, IDCD = cls.IdCLS,cls.NgayTH }).ToList();
            var qCLS1 = (from cls in data.CLS
                        join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                        join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                        select new { cd.MaDV, clsct.KetQua, cls.MaBNhan, IDCD = cls.IdCLS, cls.NgayTH, cd.Status }).ToList();
            //bệnh nhân xét nghiệm HIV
            var qXNHIV = (from bn in bnkb1
                          join cls in qCLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on bn.MaBNhan equals cls.MaBNhan
                          join dv in qDV.Where(p => p.TenDV.ToLower().Contains("hiv")) on cls.MaDV equals dv.MaDV
                          select new { cls.KetQua }).ToList();

            var pXN = (from bn in bnkb1.Where(p => p.NoiTru == 1)
                       join cls in qCLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on bn.MaBNhan equals cls.MaBNhan
                          join dv in qDV on cls.MaDV equals dv.MaDV
                          join a in data.BNKBs on bn.IDKB equals a.IDKB
                       select new { bn.MaBNhan, bn.SThe, dv.TenRG, dv.TenNhomCT, dv.TenDV, a.MaICD, cls.IDCD, NgayTH = cls.NgayTH.Value.Date }).ToList();
            var aa = (from bn in bnkb1
                  join dt in qDThuoc on bn.MaBNhan equals dt.MaBNhan
                  join dv in qDV on dt.MaDV equals dv.MaDV
                  join a in data.BNKBs on bn.IDKB equals a.IDKB
                  select new { bn.MaBNhan, bn.SThe, dv.TenRG, dv.TenNhomCT, dv.TenDV, a.MaICD, IDCD = dt.IDDon, NgayTH = dt.NgayNhap, Status = 1 }).ToList();
            if (radBNhan.SelectedIndex == 1)
            {
                pXN = qBNTHDV.ToList();
                qXNHIV = (from bn in bnkb1.Where(p => p.NoiTru == 1)
                          join cls in qDThuoc.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on bn.MaBNhan equals cls.MaBNhan
                          join dv in qDV.Where(p => p.TenDV.ToLower().Contains("hiv")) on cls.MaDV equals dv.MaDV
                          select new { KetQua = cls.IDDon.ToString() }).ToList();
            }

            #endregion
            #region 1. số lần khám bệnh
            moi = new NoiDung();
            moi.Group = "I. Hoạt động khám chữa bệnh";
            moi.Stt = 1;
            moi.Nhom = "Số lần khám bệnh";
            moi.ChiTiet = "";
            moi.DonVi = "Lượt";
            moi.SoLieu = qKhamNgTru.Count() + qKhamNTru.Sum(p => p.SoNgaydt);
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "I. Hoạt động khám chữa bệnh";
            moi.Stt = 1;
            moi.Nhom = "Số lần khám bệnh";
            moi.ChiTiet = "Trong đó: Khám có BHYT";
            moi.DonVi = "Lượt";
            moi.SoLieu = qKhamNgTru.Where(p => p.SThe != null && p.SThe != "").Count() + qKhamNTru.Where(p => p.SThe != null && p.SThe != "").Count();
            _lND.Add(moi);
            #endregion
            #region 2. BN điều trị nội trú
            moi = new NoiDung();
            moi.Group = "I. Hoạt động khám chữa bệnh";
            moi.Stt = 2;
            moi.Nhom = "Bệnh nhân điều trị nội trú";
            moi.ChiTiet = "";
            moi.DonVi = "Người";
            moi.SoLieu = qBNDTNoiTru.Where(p => p.NgayVao <= denngay).Where(p => p.NgayRa > denngay || p.NgayRa == null).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "I. Hoạt động khám chữa bệnh";
            moi.Stt = 2;
            moi.Nhom = "Bệnh nhân điều trị nội trú";
            moi.ChiTiet = "Trong đó: Số bệnh nhân tháng trước còn lại";
            moi.DonVi = "Người";
            moi.SoLieu = qBNDTNoiTru.Where(p => p.NgayVao <= denngaythangtruoc).Where(p => p.NgayRa > denngay || p.NgayRa == null).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "I. Hoạt động khám chữa bệnh";
            moi.Stt = 2;
            moi.Nhom = "Bệnh nhân điều trị nội trú";
            moi.ChiTiet = "                Số BN mới";
            moi.DonVi = "Người";
            moi.SoLieu = qBNDTNoiTru.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Where(p => p.NgayRa > denngay || p.NgayRa == null).Count();
            _lND.Add(moi);
            #endregion
            #region 3. tổng số ngày điều trị nội trú
            moi = new NoiDung();
            moi.Group = "I. Hoạt động khám chữa bệnh";
            moi.Stt = 3;
            moi.Nhom = "Tổng số ngày điều trị nội trú";
            moi.ChiTiet = "";
            moi.DonVi = "Ngày";
            moi.SoLieu = qKhamNTru.Sum(p => p.SoNgaydt);
            _lND.Add(moi);
            #endregion
            #region 4. công suất sử dụng giường bệnh
            moi = new NoiDung();
            moi.Group = "I. Hoạt động khám chữa bệnh";
            moi.Stt = 4;
            moi.Nhom = "Công suất sử dụng giường bệnh";
            moi.ChiTiet = "";
            moi.DonVi = "";
            moi.SoLieu = (double)qKhamNTru.Sum(p => p.SoNgaydt) / 700;
            moi.SoLieu = Math.Round(moi.SoLieu.GetValueOrDefault(), 2);
            _lND.Add(moi);
            #endregion
            #region 5. BN tử vong
            moi = new NoiDung();
            moi.Group = "I. Hoạt động khám chữa bệnh";
            moi.Stt = 5;
            moi.Nhom = "Bệnh nhân tử vong";
            moi.ChiTiet = "";
            moi.DonVi = "Người";
            moi.SoLieu = qKhamNTru.Where(p => p.KetQua.Equals("Tử vong")).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "I. Hoạt động khám chữa bệnh";
            moi.Stt = 5;
            moi.Nhom = "Bệnh nhân tử vong";
            moi.ChiTiet = "Trong đó: Trẻ em";
            moi.DonVi = "Người";
            moi.SoLieu = qKhamNTru.Where(p => p.KetQua.Equals("Tử vong") && p.Tuoi <= 6).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "I. Hoạt động khám chữa bệnh";
            moi.Stt = 5;
            moi.Nhom = "Bệnh nhân điều trị nội trú";
            moi.ChiTiet = "                Tử vong < 24h";
            moi.DonVi = "Người";
            //moi.SoLieu = qBNDTNoiTru.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Count();
            _lND.Add(moi);
            #endregion
            #region 6. bệnh nhân ngoại trú
            moi = new NoiDung();
            moi.Group = "I. Hoạt động khám chữa bệnh";
            moi.Stt = 6;
            moi.Nhom = "Bệnh nhân ngoại trú";
            moi.ChiTiet = "";
            moi.DonVi = "Người";
            moi.SoLieu = qKhamNgTru.GroupBy(p => p.MaBNhan).Count();
            _lND.Add(moi);
            #endregion
            #region 7. điều trị nội khoa
            moi = new NoiDung();
            moi.Group = "I. Hoạt động khám chữa bệnh";
            moi.Stt = 7;
            moi.Nhom = "Điều trị nội khoa";
            moi.ChiTiet = "";
            moi.DonVi = "Người";
            var nk = (from a in aa 
                      join b in data.BenhNhans.Where(p => p.NoiTru == 1) on a.MaBNhan equals b.MaBNhan 
                      join c in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on a.MaBNhan equals c.MaBNhan 
                      select new { a.MaBNhan, a.SThe, a.TenRG, a.TenNhomCT, a.TenDV, a.MaICD, a.IDCD, a.Status}).ToList();
            moi.SoLieu = (nk.GroupBy(p => p.MaBNhan).Count() - nk.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).GroupBy(p => p.MaBNhan).Count() > 0 ) ? nk.GroupBy(p => p.MaBNhan).Count() - nk.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).GroupBy(p => p.MaBNhan).Count() : 0;
            _lND.Add(moi);
            #endregion
            #region 8. điều trị nội khoa
            moi = new NoiDung();
            moi.Group = "I. Hoạt động khám chữa bệnh";
            moi.Stt = 8;
            moi.Nhom = "Bệnh nhân chuyển viện tại phòng khám";
            moi.ChiTiet = "";
            moi.DonVi = "Người";
            moi.SoLieu = qKhamNgTru.Where(p => p.PhuongAn == 2).GroupBy(p => p.MaBNhan).Count();
            _lND.Add(moi);
            #endregion  
            #region 9. xét nghiệm
            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 9;
            moi.Nhom = "Xét nghiệm";
            moi.ChiTiet = "";
            moi.DonVi = "Lượt";
            if (radBNhan.SelectedIndex == 0)
            moi.SoLieu = (from a in pXN.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm") && !p.TenDV.ToLower().Contains("hiv")) group a by new { a.MaBNhan, a.IDCD, a.TenRG } into kq select new { kq.Key.IDCD }).Count();
            if(radBNhan.SelectedIndex == 1)
                moi.SoLieu = (from a in pXN.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm") && !p.TenDV.ToLower().Contains("hiv")) group a by new { a.MaBNhan, a.IDCD, a.TenRG, a.NgayTH.Date  } into kq select new { kq.Key.IDCD }).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 9;
            moi.Nhom = "Xét nghiệm";
            moi.ChiTiet = "Trong đó: Huyết học";
            moi.DonVi = "Lượt";
            if(radBNhan.SelectedIndex == 0)
            moi.SoLieu = pXN.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)).GroupBy(p => p.IDCD).Count();
            if (radBNhan.SelectedIndex == 1)
                moi.SoLieu = pXN.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)).GroupBy(p => new { p.IDCD, p.NgayTH.Date }).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 9;
            moi.Nhom = "Xét nghiệm";
            moi.ChiTiet = "                Sinh hóa";
            moi.DonVi = "Người";
            if (radBNhan.SelectedIndex == 0)
                moi.SoLieu = pXN.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)).GroupBy(p => p.IDCD).Count();
            if (radBNhan.SelectedIndex == 1)
                moi.SoLieu = pXN.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)).GroupBy(p => new { p.IDCD, p.NgayTH.Date }).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 9;
            moi.Nhom = "Xét nghiệm";
            moi.ChiTiet = "                Nước tiểu";
            moi.DonVi = "Người";
            if (radBNhan.SelectedIndex == 0)
                moi.SoLieu = pXN.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)).GroupBy(p => p.IDCD).Count();
            if (radBNhan.SelectedIndex == 1)
                moi.SoLieu = pXN.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)).GroupBy(p => new { p.IDCD, p.NgayTH.Date }).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 9;
            moi.Nhom = "Xét nghiệm";
            moi.ChiTiet = "                Soi tươi";
            moi.DonVi = "Người";
            //moi.SoLieu = qBNDTNoiTru.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 9;
            moi.Nhom = "Xét nghiệm";
            moi.ChiTiet = "                XN khác";
            moi.DonVi = "Người";
            if (radBNhan.SelectedIndex == 0)
                moi.SoLieu = pXN.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac)).Where(p => !p.TenDV.ToLower().Contains("hiv")).GroupBy(p => p.IDCD).Count();
            if (radBNhan.SelectedIndex == 1)
                moi.SoLieu = pXN.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac)).Where(p => !p.TenDV.ToLower().Contains("hiv")).GroupBy(p => new { p.IDCD, p.NgayTH.Date }).Count();
            _lND.Add(moi);
            #endregion
            #region 10. siêu âm
            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 10;
            moi.Nhom = "Siêu âm";
            moi.ChiTiet = "";
            moi.DonVi = "Lượt";
            if (radBNhan.SelectedIndex == 1)
            moi.SoLieu = pXN.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).GroupBy(p => new { p.MaBNhan, p.NgayTH}).Count();
            else moi.SoLieu = pXN.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
            _lND.Add(moi);
            #endregion
            #region 11. XN HIV
            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 11;
            moi.Nhom = "XN HIV";
            moi.ChiTiet = "";
            moi.DonVi = "Lượt";
            moi.SoLieu = qXNHIV.Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 11;
            moi.Nhom = "XN HIV";
            moi.ChiTiet = "Trong đó: (+)";
            moi.DonVi = "Lượt";
            moi.SoLieu = qXNHIV.Where(p => p.KetQua.ToLower().Contains("dương tính")).Count();
            _lND.Add(moi);
            #endregion
            #region 12. soi góc tiền phòng
            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 12;
            moi.Nhom = "Soi góc tiền phòng";
            moi.ChiTiet = "";
            moi.DonVi = "Lượt";
            if (radBNhan.SelectedIndex == 0)
            moi.SoLieu = pXN.Where(p => p.TenDV.ToLower().Contains("soi góc tiền phòng")).Count();
            else moi.SoLieu = pXN.Where(p => p.TenDV.ToLower().Contains("soi góc tiền phòng")).GroupBy(p => new { p.MaBNhan, p.NgayTH }).Count();
            _lND.Add(moi);
            #endregion
            #region 13. tổng số phẫu thuật
            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 13;
            moi.Nhom = "Tổng số phẫu thuật";
            moi.ChiTiet = "";
            moi.DonVi = "Lượt";
            moi.SoLieu = pXN.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).GroupBy(p => p.MaBNhan).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 13;
            moi.Nhom = "Tổng số phẫu thuật";
            moi.ChiTiet = "Trong đó: Đục thủy tinh thể";
            moi.DonVi = "Lượt";
            moi.SoLieu = pXN.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Contains("H25") || p.MaICD.Equals("H26.2")).GroupBy(p => p.MaBNhan).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 13;
            moi.Nhom = "Tổng số phẫu thuật";
            moi.ChiTiet = "                Glocom";
            moi.DonVi = "Lượt";
            moi.SoLieu = pXN.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Contains("H40")).GroupBy(p => p.MaBNhan).Count();
            _lND.Add(moi);

            var a1 = pXN.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Equals("H11.0")).GroupBy(p => p.MaBNhan).ToList();
            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 13;
            moi.Nhom = "Tổng số phẫu thuật";
            moi.ChiTiet = "                Mộng";
            moi.DonVi = "Lượt";
            moi.SoLieu = pXN.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Equals("H11.0")).GroupBy(p => p.MaBNhan).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 13;
            moi.Nhom = "Tổng số phẫu thuật";
            moi.ChiTiet = "                Quặm";
            moi.DonVi = "Lượt";
            moi.SoLieu = pXN.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Equals("H02.0")).GroupBy(p => p.MaBNhan).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 13;
            moi.Nhom = "Tổng số phẫu thuật";
            moi.ChiTiet = "                Cắt bỏ túi lệ và u mi";
            moi.DonVi = "Lượt";
            moi.SoLieu = pXN.Where(p => p.TenDV.ToLower().Contains("cắt bỏ túi lệ")).GroupBy(p => p.MaBNhan).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 13;
            moi.Nhom = "Tổng số phẫu thuật";
            moi.ChiTiet = "                Tiếp khẩu lệ mũi";
            moi.DonVi = "Lượt";
            //moi.SoLieu = qBNDTNoiTru.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 13;
            moi.Nhom = "Tổng số phẫu thuật";
            moi.ChiTiet = "                Thẩm mỹ";
            moi.DonVi = "Lượt";
            moi.SoLieu = pXN.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Equals("H02.8")).GroupBy(p => p.MaBNhan).Count();
            _lND.Add(moi);

            moi = new NoiDung();
            moi.Group = "II. Cận lâm sàng và phẫu thuật";
            moi.Stt = 13;
            moi.Nhom = "Tổng số phẫu thuật";
            moi.ChiTiet = "                Phẫu thuật khác";
            moi.DonVi = "Lượt";
            moi.SoLieu = pXN.Where(p => p.TenRG.ToLower().Contains("phẫu thuật"))
                                            .Where(p => !p.MaICD.Equals("H02.8") && !p.MaICD.Equals("H02.0") && !p.MaICD.Equals("H11.0") && !p.MaICD.Contains("H40") && !p.MaICD.Contains("H25") && !p.MaICD.Equals("H26.2")).GroupBy(p => p.MaBNhan).Count();
            _lND.Add(moi);
            #endregion
            
            BaoCao.Rep_BC_KQHD_27022 rep = new BaoCao.Rep_BC_KQHD_27022();
            frmIn frm = new frmIn();
            rep.DataSource = _lND.ToList();
            rep.celThang.Text = "Tháng " + denngay.Month;
            rep.lblTieuDeBC.Text = ("báo cáo kết quả hoạt động tháng " + denngay.Month + " / " + denngay.Year).ToUpper();
            
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        #region class NoiDung
        private class NoiDung
        {
            public string Group { get; set; }
            public int Stt { get; set; }
            public string Nhom { get; set; }
            public string ChiTiet { get; set; }
            public string DonVi { get; set; }
            public double? SoLieu { get; set; }
        }
        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string mess = @"HƯỚNG DẪN IN BÁO CÁO
                            - Số lần khám: khám ở phòng khám(BN ngoại trú) + số ngày giường(BN nội trú)
                              + Khám có BHYT: bệnh nhân ngoại trú khám ở phòng khám có thẻ BHYT
                            - Bệnh nhân điều trị nội trú
                              + BN tháng trước: BN nội trú vào tháng trước chưa ra viện
                              + BN mới vào: BN nội trú mới vào trong khoảng thời gian tìm kiếm
                            - Số ngày điều trị nội trú: Tổng số ngày điều trị của BN nội trú
                            - Công suất sử dụng giường bệnh: tổng số ngày điều trị/700
                            - Bệnh nhân tử vong
                              + Trẻ em: BN nội trú dưới 6 tuổi tử vong
                            - Bệnh nhân ngoại trú: BN ngoại trú khám tại phòng khám
                            - Bệnh nhân nội khoa: BN vào viện
                            - Xét nghiệm:
                              + Huyết học: dịch vụ có TenRG (Tiểu nhóm dv): XN huyết học
                              + Sinh hóa: dịch vụ có TenRG (Tiểu nhóm dv): XN hóa sinh máu
                              + Nước tiểu: dịch vụ có TenRG (Tiểu nhóm dv): XN nước tiểu
                              + Soi tươi: ???
                              + XN khác: dịch vụ có TenRG (Tiểu nhóm dv): XN khác
                            - Siêu âm: dịch vụ có TenRG (Tiểu nhóm dv): Siêu âm
                            - XN HIV: dịch vụ có tên chứa: hiv
                              + (+): BN có kết quả xét nghiệm là: dương tính
                            - Soi góc tiền phòng: BN thực hiện DV có tên là: soi góc tiền phòng
                            - Phẫu thuật:
                              + Đục thủy tinh thể: BN có Mã ICD: H25 hoặc H26.2
                              + Glocom: BN có Mã ICD: H40
                              + Mộng: BN có Mã ICD: H11.0
                              + Quặm: BN có Mã ICD: H02.0
                              + Cắt bỏ túi lệ và u mi: BN thực hiện DV có tên chứa: cắt bỏ túi lệ
                              + Tiếp khẩu lệ mũi: ???
                              + Thẩm mỹ: BN có Mã ICD: H02.8
                              + Khác: khác các loại trên";
            MessageBox.Show(mess, "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}