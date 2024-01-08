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
    public partial class frm_BCCM_27022 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCCM_27022()
        {
            InitializeComponent();
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        #region class NoiDung
        public class NoiDung
        {
            public int Stt { get; set; }
            public string Title { get; set; }
            public int? Quantity { get; set; }
        }
        #endregion
        #region class Khoa
        public class Khoa
        {
            public bool Chon { get; set; }
            public int MaKP { get; set; }
            public string TenKP { get; set; }
        }
        #endregion
        List<Khoa> _lKhoa = new List<Khoa>();
        private void frm_BCCM_27022_Load(object sender, EventArgs e)
        {
            radDTuong.SelectedIndex = 1;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
            var qkhoa = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám").Where(p => p.Status == 1)
                         select new { kp.MaKP, kp.TenKP }).OrderBy(p => p.TenKP).ToList();
            if (qkhoa.Count > 0)
            {
                Khoa moi = new Khoa();
                moi.Chon = true;
                moi.MaKP = 0;
                moi.TenKP = "Tất cả";
                _lKhoa.Add(moi);
                foreach (var item in qkhoa)
                {
                    Khoa moi1 = new Khoa();
                    moi1.Chon = true;
                    moi1.MaKP = item.MaKP;
                    moi1.TenKP = item.TenKP;
                    _lKhoa.Add(moi1);
                }
                grcKhoa.DataSource = _lKhoa.ToList();
            }
        }

        private bool KTBC()
        {
            if (dtTuNgay.EditValue == null)
            {
                MessageBox.Show("Chưa chọn ngày bắt đầu in báo cáo", "Thông báo");
                dtTuNgay.Focus();
                return false;
            }
            if (dtDenNgay.EditValue == null)
            {
                MessageBox.Show("Chưa chọn ngày kết thúc in báo cáo", "Thông báo");
                dtDenNgay.Focus();
                return false;
            }
            if (Convert.ToDateTime(dtTuNgay.EditValue) > Convert.ToDateTime(dtTuNgay.EditValue))
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu", "Thông báo");
                dtDenNgay.Focus();
                return false;
            }
            return true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        List<NoiDung> _lNoiDung = new List<NoiDung>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lNoiDung.Clear();
            if (KTBC())
            {
                List<Khoa> dsKhoa = new List<Khoa>();
                NoiDung ndung = new NoiDung();
                DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
                DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
                dsKhoa.Clear();
                dsKhoa = _lKhoa.Where(p => p.MaKP > 0 && p.Chon == true).ToList();
                int noitru = -1;
                if (radDTuong.SelectedIndex == 0)
                    noitru = 0;
                if (radDTuong.SelectedIndex == 1)
                    noitru = 1;
                if (radDTuong.SelectedIndex == 2)
                    noitru = 2;
                #region select
                var qBNKB = (from bn in data.BenhNhans.Where(p => noitru == 2 || p.NoiTru == noitru)
                             join bnkb in data.BNKBs.Where(p => p.NgayKham <= denngay) on bn.MaBNhan equals bnkb.MaBNhan
                             join rv in data.RaViens on bnkb.MaBNhan equals rv.MaBNhan into k
                             from kq in k.DefaultIfEmpty()
                             group new { bn, bnkb, kq } by new { bn.NoiTru, bn.MaBNhan, bn.TenBNhan, kq.Status, kq.NgayRa} into kq
                             select new { kq.Key.NoiTru, kq.Key.MaBNhan, kq.Key.Status, IDKB = kq.Max(p => p.bnkb.IDKB), kq.Key.NgayRa }).ToList();
                var qBNKBkq = (from a in qBNKB
                               join c in data.BNKBs on a.IDKB equals c.IDKB
                               join b in dsKhoa on c.MaKP equals b.MaKP
                               group new { a, c } by new { a.MaBNhan, a.NoiTru, c.MaKP, a.Status, c.MaICD, a.NgayRa, c.NgayKham} into q
                               select new { q.Key.MaBNhan, q.Key.NoiTru, q.Key.MaKP, q.Key.Status, q.Key.MaICD, q.Key.NgayRa, q.Key.NgayKham }).ToList();
                var qBNKBkq1 = (from a in qBNKB
                                join c in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on a.IDKB equals c.IDKB
                                join b in dsKhoa on c.MaKP equals b.MaKP
                               group new { a, c } by new { a.MaBNhan, a.NoiTru, c.MaKP, a.Status, c.MaICD} into q
                               select new { q.Key.MaBNhan, q.Key.NoiTru, q.Key.MaKP, q.Key.Status, q.Key.MaICD }).ToList();

                var dv1 = (from dv in data.DichVus
                          join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                          join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                          select new { dv.MaDV, dv.TenDV, tn.TenRG, ndv.TenNhomCT}).ToList();
                var bnPhauThuat = (from n in dsKhoa
                                   join b in qBNKBkq on n.MaKP equals b.MaKP
                                   join cls in data.CLS.Where(p => (p.NgayTH >= tungay && p.NgayTH <= denngay)) on b.MaBNhan equals cls.MaBNhan
                                   join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                   join dv in dv1 on cd.MaDV equals dv.MaDV
                                   group new { cls, n, b, cd, dv1} by new
                                   {
                                       dv.TenDV,
                                       cls.MaBNhan,
                                       b.MaICD,
                                       b.NoiTru,
                                       dv.TenRG,
                                       dv.TenNhomCT
                                   } into q
                                   select new
                                   {
                                       q.Key.TenDV,
                                       q.Key.MaBNhan,
                                       q.Key.MaICD,
                                       q.Key.NoiTru,
                                       q.Key.TenRG,
                                       q.Key.TenNhomCT
                                   }).ToList();

                var bnPhauThuat1 = (from n in dsKhoa
                                   join b in qBNKBkq on n.MaKP equals b.MaKP
                                    join dt in data.DThuocs on b.MaBNhan equals dt.MaBNhan
                                    join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.IDCD == null) on dt.IDDon equals dtct.IDDon
                                    join dv in dv1 on dtct.MaDV equals dv.MaDV
                                    group new { dt, n, b, dtct, dv } by new
                                   {
                                       dv.TenDV,
                                       dt.MaBNhan,
                                       b.MaICD,
                                       b.NoiTru,
                                       dv.TenRG,
                                       dv.TenNhomCT
                                   } into q
                                   select new
                                   {
                                       q.Key.TenDV,
                                       q.Key.MaBNhan,
                                       q.Key.MaICD,
                                       q.Key.NoiTru,
                                       q.Key.TenRG,
                                       q.Key.TenNhomCT
                                   }).ToList();

                var bnPhauThuat3 = (from n in dsKhoa
                                   join cls in data.CLS.Where(p => (p.NgayTH >= tungay && p.NgayTH <= denngay)) on n.MaKP equals cls.MaKP
                                   join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                   join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                   join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                   join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                                    join bn in data.BenhNhans.Where(p => noitru == 2 || p.NoiTru == noitru) on cls.MaBNhan equals bn.MaBNhan
                                   group new { cls, n, cd, dv, tn } by new
                                   {
                                       dv.TenDV,
                                       cls.MaBNhan,
                                       tn.TenRG,
                                       ndv.TenNhomCT,
                                       Ngay = cls.NgayTH.Value.Date
                                   } into q
                                   select new
                                   {
                                       q.Key.TenDV,
                                       q.Key.MaBNhan,
                                       q.Key.TenRG,
                                       q.Key.TenNhomCT,
                                       q.Key.Ngay
                                   }).ToList();

                var bnPhauThuat4 = (from n in dsKhoa
                                    join dt in data.DThuocs on n.MaKP equals dt.MaKP
                                    join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.IDCD == null) on dt.IDDon equals dtct.IDDon
                                    join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                    join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                    join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                                    join bn in data.BenhNhans.Where(p => noitru == 2 || p.NoiTru == noitru) on dt.MaBNhan equals bn.MaBNhan
                                    group new { dt, n, dtct, dv, tn } by new
                                    {
                                        dv.TenDV,
                                        dt.MaBNhan,
                                        tn.TenRG,
                                        ndv.TenNhomCT,
                                        Ngay = dtct.NgayNhap.Value.Date
                                    } into q
                                    select new
                                    {
                                        q.Key.TenDV,
                                        q.Key.MaBNhan,
                                        q.Key.TenRG,
                                        q.Key.TenNhomCT,
                                        q.Key.Ngay
                                    }).ToList();

                var bn1 = (from a in data.BenhNhans.Where(p => noitru == 2 || p.NoiTru == noitru)
                           join b in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on a.MaBNhan equals b.MaBNhan into k
                           from kq in k.DefaultIfEmpty() 
                           group new {a,kq} by new {a.MaBNhan, a.NoiTru, kq.MaKP} into k1
                           select new
                           {
                               k1.Key.MaBNhan,
                               k1.Key.NoiTru,
                               k1.Key.MaKP
                           }).ToList();
                var bnPhauThuat2 = (from n in dsKhoa
                                    join b in bn1 on n.MaKP equals b.MaKP
                                    join dt in data.DThuocs on b.MaBNhan equals dt.MaBNhan
                                    join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                                    join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                    join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                    join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                                    group new { dt, n, b, dtct, dv, tn } by new
                                    {
                                        dv.TenDV,
                                        dt.MaBNhan,
                                        b.NoiTru,
                                        tn.TenRG,
                                        ndv.TenNhomCT,
                                        dtct.IDCD
                                    } into q
                                    select new
                                    {
                                        q.Key.TenDV,
                                        q.Key.MaBNhan,
                                        q.Key.NoiTru,
                                        q.Key.TenRG,
                                        q.Key.TenNhomCT,
                                        q.Key.IDCD
                                    }).ToList();
                if (radBNhan.SelectedIndex == 1)
                {
                    bnPhauThuat = bnPhauThuat1;
                    bnPhauThuat3 = bnPhauThuat4;
                }
                    
                #endregion

                #region Số BN khám tại viện
                ndung = new NoiDung();
                ndung.Stt = 1;
                ndung.Title = "1) Số bệnh nhân khám tại viện:";
                ndung.Quantity = qBNKBkq1.GroupBy(p=> p.MaBNhan).Count();
                _lNoiDung.Add(ndung);
                 
                ndung = new NoiDung();
                ndung.Stt = 2;
                ndung.Title = "- Nội trú:";
                ndung.Quantity = qBNKBkq1.Where(p => p.NoiTru == 1).GroupBy(p => p.MaBNhan).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 3;
                ndung.Title = "- Ngoại trú:";
                ndung.Quantity = qBNKBkq1.Where(p => p.NoiTru == 0).GroupBy(p => p.MaBNhan).Count();
                _lNoiDung.Add(ndung);
                #endregion
                #region BN phẫu thuật
                ndung = new NoiDung();
                ndung.Stt = 4;
                ndung.Title = "2) Số bệnh nhân phẫu thuật:";
                ndung.Quantity = bnPhauThuat.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).GroupBy(p => p.MaBNhan).Count();
                _lNoiDung.Add(ndung);

                var a1 = bnPhauThuat.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Contains("H11.0")).GroupBy(p => p.MaBNhan).ToList();
                ndung = new NoiDung();
                ndung.Stt = 5;
                ndung.Title = "- Thủy tinh thể:";
                ndung.Quantity = bnPhauThuat.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Contains("H25") || p.MaICD.Equals("H26.2")).GroupBy(p => p.MaBNhan).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 6;
                ndung.Title = "- Glocom:";
                ndung.Quantity = bnPhauThuat.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Contains("H40")).GroupBy(p => p.MaBNhan).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 7;
                ndung.Title = "- Mộng:";
                ndung.Quantity = bnPhauThuat.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Equals("H11.0")).GroupBy(p => p.MaBNhan).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 8;
                ndung.Title = "- Quặm:";
                ndung.Quantity = bnPhauThuat.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Equals("H02.0")).GroupBy(p => p.MaBNhan).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 9;
                ndung.Title = "- Thẩm mỹ:";
                ndung.Quantity = bnPhauThuat.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Equals("H02.8")).GroupBy(p => p.MaBNhan).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 10;
                ndung.Title = "- Khác:";
                ndung.Quantity = bnPhauThuat.Where(p => p.TenRG.ToLower().Contains("phẫu thuật"))
                                            .Where(p => !p.MaICD.Equals("H02.8") && !p.MaICD.Equals("H02.0") && !p.MaICD.Equals("H11.0") && !p.MaICD.Contains("H40") && !p.MaICD.Contains("H25") && !p.MaICD.Equals("H26.2")).GroupBy(p => p.MaBNhan).Count();
                _lNoiDung.Add(ndung);
                #endregion
                #region Nội khoa
                ndung = new NoiDung();
                ndung.Stt = 11;
                ndung.Title = "3) Nội khoa:";
                var rvv = (from a in bnPhauThuat2
                            join b in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on a.MaBNhan equals b.MaBNhan
                            select new { a}).ToList();
                ndung.Quantity = rvv.Where(p => p.a.NoiTru == 1).GroupBy(p => p.a.MaBNhan).Count() - rvv.Where(p => p.a.NoiTru == 1).Where(p => p.a.TenRG.ToLower().Contains("phẫu thuật")).GroupBy(p => p.a.MaBNhan).Count();
                _lNoiDung.Add(ndung);
                #endregion
                #region BN TH CLS
                ndung = new NoiDung();
                ndung.Stt = 12;
                ndung.Title = "4) Số bệnh nhân:";
                ndung.Quantity = bnPhauThuat3.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm) ||
                                                        p.TenDV.ToLower().Contains("soi góc tiền phòng") ||
                                                        p.TenDV.ToLower().Contains("soi đáy mắt") ||
                                                        p.TenDV.ToLower().Contains("soi trực tiếp nhuộm soi") ||
                                                        p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau) ||
                                                        p.TenRG.ToLower().Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu) ||
                                                        p.TenRG.ToLower().Contains("thời gian máu chảy") || p.TenRG.ToLower().Contains("thời gian máu đông")).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 13;
                ndung.Title = "- Siêu âm:";
                ndung.Quantity = bnPhauThuat3.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 14;
                ndung.Title = "- Soi góc TP:";
                ndung.Quantity = bnPhauThuat3.Where(p => p.TenDV.ToLower().Contains("soi góc tiền phòng")).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 15;
                ndung.Title = "- Soi ĐM:";
                ndung.Quantity = bnPhauThuat3.Where(p => p.TenDV.ToLower().Contains("soi đáy mắt")).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 16;
                ndung.Title = "- Nạo ổ loét GM:";
                //ndung.Quantity = bnPhauThuat.Where(p => p.TenDV.ToLower().Contains("soi đáy mắt")).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 17;
                ndung.Title = "- Xét nghiệm:";
                ndung.Quantity = bnPhauThuat3.Where(p => p.TenDV.ToLower().Contains("soi trực tiếp nhuộm soi") ||
                                                        p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau) ||
                                                        //p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc) ||
                                                        p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu) ||
                                                        //p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom) ||
                                                        //p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich) ||
                                                        //p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo) ||
                                                        //p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac) ||
                                                        //p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNTeBaoNuocDich) ||
                                                        //p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh) ||
                                                        p.TenRG.ToLower().Contains("thời gian máu chảy") || p.TenRG.ToLower().Contains("thời gian máu đông")).GroupBy(p => new { p.MaBNhan, p.Ngay, p.TenRG}).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 18;
                ndung.Title = " + Soi TT - NS:";
                ndung.Quantity = bnPhauThuat3.Where(p => p.TenDV.ToLower().Contains("soi trực tiếp nhuộm soi")).GroupBy(p => new { p.MaBNhan, p.Ngay }).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 19;
                ndung.Title = " + Sinh hóa máu:";
                ndung.Quantity = bnPhauThuat3.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)).GroupBy(p => new { p.MaBNhan, p.Ngay, p.TenRG }).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 20;
                ndung.Title = " + Nước tiểu:";
                ndung.Quantity = bnPhauThuat3.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)).GroupBy(p => new { p.MaBNhan, p.Ngay }).Count();
                _lNoiDung.Add(ndung);

                ndung = new NoiDung();
                ndung.Stt = 21;
                ndung.Title = " + Công thức máu:";
                ndung.Quantity = bnPhauThuat3.Where(p => p.TenRG.ToLower().Contains("thời gian máu chảy") || p.TenRG.ToLower().Contains("thời gian máu đông")).GroupBy(p => new { p.MaBNhan, p.Ngay }).Count();
                _lNoiDung.Add(ndung);
                #endregion
                #region chuyển viện
                ndung = new NoiDung();
                ndung.Stt = 22;
                ndung.Title = "5) Chuyển viện:";
                ndung.Quantity = qBNKBkq1.Where(p => p.Status == 1).GroupBy(p=> p.MaBNhan).Count();
                _lNoiDung.Add(ndung);
                #endregion
                BaoCao.Rep_BCCM_27022 rep = new BaoCao.Rep_BCCM_27022();
                frmIn frm = new frmIn();
                rep.DataSource = _lNoiDung.OrderBy(p => p.Stt).ToList();
                rep.lblTuNgay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void grvKhoa_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKhoa.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoa.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten.Equals("Tất cả"))
                    {
                        if (_lKhoa.First().Chon == true)
                        {
                            foreach (var a in _lKhoa)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKhoa)
                            {
                                a.Chon = true;
                            }
                        }
                        grcKhoa.DataSource = "";
                        grcKhoa.DataSource = _lKhoa.ToList();
                    }
                }
            }
        }
    }
}