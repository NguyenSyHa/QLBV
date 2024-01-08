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
    public partial class frm_BC_KQKhamDT_27022 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_KQKhamDT_27022()
        {
            InitializeComponent();
        }

        private void radTuan_CheckedChanged(object sender, EventArgs e)
        {
            if (radTuan.Checked)
            {
                dtTuNgay.Enabled = false;
                dtDenNgay.Enabled = false;
                cbbThang.Enabled = true;
                cbbTuan.Enabled = true;
            }
        }

        List<Khoa> _lKhoa = new List<Khoa>();
        private void frm_BC_KQKhamDT_27022_Load(object sender, EventArgs e)
        {
            #region tháng
            List<int> dsThang = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                dsThang.Add(i);
            }
            cbbThang.DataSource = dsThang.ToList();
            #endregion
            if (radTuan.Checked)
            {
                dtTuNgay.Enabled = false;
                dtDenNgay.Enabled = false;
            }
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qkhoa = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám") select new { kp.TenKP, kp.MaKP }).OrderBy(p => p.TenKP).ToList();
            if (qkhoa.Count > 0)
            {
                Khoa moi1 = new Khoa();
                moi1.Chon = true;
                moi1.MaKP = 0;
                moi1.TenKP = "Chọn tất cả";
                _lKhoa.Add(moi1);
                foreach (var item in qkhoa)
                {
                    Khoa moi = new Khoa();
                    moi.Chon = true;
                    moi.MaKP = item.MaKP;
                    moi.TenKP = item.TenKP;
                    _lKhoa.Add(moi);
                }
                grcKhoaphong.DataSource = _lKhoa.ToList();
            }
        }

        private void radNgay_CheckedChanged(object sender, EventArgs e)
        {
            if (radNgay.Checked)
            {
                cbbThang.Enabled = false;
                cbbTuan.Enabled = false;
                dtTuNgay.Enabled = true;
                dtDenNgay.Enabled = true;
                dtTuNgay.DateTime = DateTime.Now;
                dtDenNgay.DateTime = DateTime.Now;
            }
        }

        #region class Khoa
        public class Khoa
        {
            public bool Chon { get; set; }
            public int MaKP { get; set; }
            public string TenKP { get; set; }
        }
        #endregion

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
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
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lKhoa.ToList();
                    }
                }
            }
        }

        List<KQ> _lKQ = new List<KQ>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            //DateTime ngay = FirstDay(DateTime.Now);
            //int week = GetWeekNo(new DateTime(2016,12,29));
            _lKQ.Clear();
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DateTime.Now;
            DateTime denngay = DateTime.Now;
            KQ moi = new KQ();
            List<Khoa> dskhoachon = new List<Khoa>();
            BaoCao.Rep_BC_KQKhamDT_27022 rep = new BaoCao.Rep_BC_KQKhamDT_27022();
            frmIn frm = new frmIn();
            if (radNgay.Checked)
            {
                tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
                rep.lblTieuDe.Text = "KẾT QUẢ KHÁM, ĐIỀU TRỊ";
                rep.lblTuNgay.Text = "(Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy") + ")";
            }
            if (radTuan.Checked)
            {
                int thang = Convert.ToInt32(cbbThang.SelectedValue);
                int tuan = Convert.ToInt32(cbbTuan.SelectedValue);
                DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, thang, 1);
                DateTime firstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);
                int firstweek = GetWeekNo(firstDayOfMonth);
                //int currentweek = GetWeekNo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                //int numweek = currentweek - firstweek;
                DateTime f = FirstDay(firstDayOfYear.AddDays((firstweek + tuan - 1) * 6));
                DateTime l = LastDay(firstDayOfYear.AddDays((firstweek + tuan - 1) * 6));
                tungay = DungChung.Ham.NgayTu(f);
                denngay = DungChung.Ham.NgayDen(l);
                rep.lblTieuDe.Text = "KẾT QUẢ KHÁM, ĐIỀU TRỊ TUẦN " + tuan + " THÁNG " + thang + " NĂM " + DateTime.Now.Year;
                rep.lblTuNgay.Text = "(Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy") + ")";
                rep.celTieuDeHd.Text = "Thực hiện tuần " + tuan + "/tháng " + thang;
            }
            var phongkham = (from kp in data.KPhongs.Where(p => p.PLoai == "Phòng khám") select new { kp.TenKP, kp.MaKP }).ToList();
            dskhoachon = _lKhoa.Where(p => p.MaKP > 0).Where(p => p.Chon == true).ToList();
            var pkhamchon = (from a in phongkham
                             join b in dskhoachon on a.MaKP equals b.MaKP
                             select new { b.MaKP, b.TenKP }).ToList();
            #region select
            //số lần khám
            var qbnkb = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                         join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                         join rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on bnkb.MaBNhan equals rv.MaBNhan
                         select new { bnkb.MaBNhan, bnkb.MaKP }).ToList();
            var qlankham = (from a in pkhamchon
                            join b in qbnkb on a.MaKP equals b.MaKP
                            select new { b.MaBNhan }).ToList();
            //dịch vụ
            var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 2)
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       join ndv in data.NhomDVs on tn.IDNhom equals ndv.IDNhom
                       select new { dv.MaDV, dv.TenDV, tn.TenRG, dv.Loai, ndv.TenNhomCT }).ToList();
            //điều trị nội trú
            var bnNoiTru = (from n in dskhoachon
                            join vv in data.VaoViens on n.MaKP equals vv.MaKP
                            join bn in data.BenhNhans.Where(p => p.NoiTru == 1) on vv.MaBNhan equals bn.MaBNhan
                            //join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into k
                            //from k1 in k.DefaultIfEmpty()
                            group new { n, vv, bn } by new { bn.MaBNhan, bn.NoiTru } into kq
                            select new { kq.Key.MaBNhan, kq.Key.NoiTru }).ToList();
            var qCLS = (from cls in data.CLS.Where(p => (p.NgayTH >= tungay && p.NgayTH <= denngay))
                        join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                        select new { cls.MaBNhan, MaICD = cls.MaICD.Contains(";") == false ? cls.MaICD : cls.MaICD.Substring(0, cls.MaICD.IndexOf(";")), cd.MaDV }).ToList();
            var bnTHCLS = (from b in bnNoiTru
                           join cls in qCLS on b.MaBNhan equals cls.MaBNhan
                           join dv in qdv on cls.MaDV equals dv.MaDV
                           group new { cls, b, dv } by new
                           {
                               dv.TenDV,
                               cls.MaBNhan,
                               cls.MaICD,
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
            var qvp = (from vp in data.VienPhis
                       join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                       select new { vpct.MaKP, vp.MaBNhan, vpct.MaDV }).ToList();
            var qrv = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                       join rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on bn.MaBNhan equals rv.MaBNhan
                       group new { bn, rv } by new { bn.MaBNhan, rv.SoNgaydt } into q
                       select new { q.Key.MaBNhan, q.Key.SoNgaydt }).ToList();
            var qngayDTNT = (from kp in dskhoachon
                             join v in qvp on kp.MaKP equals v.MaKP
                             join d in qdv.Where(p => p.TenRG.Equals("Ngày Giường")) on v.MaDV equals d.MaDV
                             join r in qrv on v.MaBNhan equals r.MaBNhan
                             select new { d.TenDV, r.MaBNhan, r.SoNgaydt }).ToList();
            var bnRV = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                        join rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on bn.MaBNhan equals rv.MaBNhan
                        group new { bn, rv } by new { bn.MaBNhan, rv.Status, rv.MaICD, rv.MaKP, bn.NoiTru } into kq
                        select new { kq.Key.MaKP, kq.Key.MaBNhan, kq.Key.Status, kq.Key.NoiTru, MaICD = kq.Key.MaICD.Contains(";") == false ? kq.Key.MaICD : kq.Key.MaICD.Substring(0, kq.Key.MaICD.IndexOf(";")) }).ToList();
            var qRV = (from kp in dskhoachon
                       join bnrv in bnRV on kp.MaKP equals bnrv.MaKP
                       select new { bnrv.MaBNhan, bnrv.Status, bnrv.MaICD, bnrv.NoiTru }).ToList();
            #endregion
            #region Số lần khám
            moi = new KQ();
            moi.Stt = 1;
            moi.TieuDe = "1. Số lần khám";
            moi.ChiTiet = "";
            moi.KQTH = qlankham.Count();
            _lKQ.Add(moi);
            #endregion
            #region Điều trị nội trú
            moi = new KQ();
            moi.Stt = 2;
            moi.TieuDe = "2. Điều trị nội trú";
            moi.ChiTiet = "";
            //moi.KQTH = bnTHCLS.Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 2;
            moi.TieuDe = "2. Điều trị nội trú";
            moi.ChiTiet = "Tổng phẫu thuật:";
            moi.KQTH = bnTHCLS.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 2;
            moi.TieuDe = "2. Điều trị nội trú";
            moi.ChiTiet = "- TTT:";
            moi.KQTH = bnTHCLS.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Contains("H25") || p.MaICD.Equals("H26.2")).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 2;
            moi.TieuDe = "2. Điều trị nội trú";
            moi.ChiTiet = "- G:";
            moi.KQTH = bnTHCLS.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Contains("H40")).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 2;
            moi.TieuDe = "2. Điều trị nội trú";
            moi.ChiTiet = "- M:";
            moi.KQTH = bnTHCLS.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Equals("H11.0")).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 2;
            moi.TieuDe = "2. Điều trị nội trú";
            moi.ChiTiet = "- Q:";
            moi.KQTH = bnTHCLS.Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.MaICD.Equals("H02.0")).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 2;
            moi.TieuDe = "2. Điều trị nội trú";
            moi.ChiTiet = "- Khác:";
            moi.KQTH = bnTHCLS.Where(p => p.TenRG.ToLower().Contains("phẫu thuật"))
                                            .Where(p => !p.MaICD.Equals("H02.0") && !p.MaICD.Equals("H11.0") && !p.MaICD.Contains("H40") && !p.MaICD.Contains("H25") && !p.MaICD.Equals("H26.2")).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 2;
            moi.TieuDe = "2. Điều trị nội trú";
            moi.ChiTiet = "- Nội khoa:";
            moi.KQTH = bnTHCLS.Where(p => !p.TenRG.ToLower().Contains("phẫu thuật")).Count();
            _lKQ.Add(moi);
            #endregion
            #region Ngày điều trị nội trú
            moi = new KQ();
            moi.Stt = 3;
            moi.TieuDe = "3. Ngày điều trị nội trú";
            moi.ChiTiet = "";
            //moi.KQTH = qngayDTNT.Sum(p => p.SoNgaydt);
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 3;
            moi.TieuDe = "3. Ngày điều trị nội trú";
            moi.ChiTiet = "- PT loại I:";
            moi.KQTH = qngayDTNT.Where(p => p.TenDV.ToLower().Contains("ngày giường bệnh ngoại khoa loại ii")).Sum(p => p.SoNgaydt);
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 3;
            moi.TieuDe = "3. Ngày điều trị nội trú";
            moi.ChiTiet = "- PT loại II:";
            moi.KQTH = qngayDTNT.Where(p => p.TenDV.ToLower().Contains("ngày giường bệnh ngoại khoa loại iii")).Sum(p => p.SoNgaydt);
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 3;
            moi.TieuDe = "3. Ngày điều trị nội trú";
            moi.ChiTiet = "- PT loại III:";
            moi.KQTH = qngayDTNT.Where(p => p.TenDV.ToLower().Contains("ngày giường bệnh ngoại khoa loại iv")).Sum(p => p.SoNgaydt);
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 3;
            moi.TieuDe = "3. Ngày điều trị nội trú";
            moi.ChiTiet = "- Nội khoa:";
            moi.KQTH = qngayDTNT.Where(p => p.TenDV.ToLower().Contains("ngày giường bệnh nội khoa")).Sum(p => p.SoNgaydt);
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 3;
            moi.TieuDe = "3. Ngày điều trị nội trú";
            moi.ChiTiet = "- Công suất giường bệnh theo tuần:";
            moi.KQTH = (double)qngayDTNT.Sum(p => p.SoNgaydt) / 175;
            moi.KQTH = Math.Round(moi.KQTH.GetValueOrDefault(), 2);
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 3;
            moi.TieuDe = "3. Ngày điều trị nội trú";
            moi.ChiTiet = "- Công suất giường bệnh theo tháng:";
            moi.KQTH = (double)qngayDTNT.Sum(p => p.SoNgaydt) / 700;
            moi.KQTH = Math.Round(moi.KQTH.GetValueOrDefault(), 2);
            _lKQ.Add(moi);
            #endregion
            #region Xét nghiệm
            moi = new KQ();
            moi.Stt = 4;
            moi.TieuDe = "4. Xét nghiệm";
            moi.ChiTiet = "";
            //moi.KQTH = bnTHCLS.Where(p => p.TenNhomCT.Equals("Xét nghiệm") || p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm) ||
            //                            p.TenDV.ToLower().Contains("soi đáy mắt trực tiếp") || p.TenDV.ToLower().Contains("soi góc tiền phòng")).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 4;
            moi.TieuDe = "4. Xét nghiệm";
            moi.ChiTiet = "- Máu:";
            moi.KQTH = bnTHCLS.Where(p => p.TenRG.Equals("Máu")).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 4;
            moi.TieuDe = "4. Xét nghiệm";
            moi.ChiTiet = "- Nước tiểu:";
            moi.KQTH = bnTHCLS.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 4;
            moi.TieuDe = "4. Xét nghiệm";
            moi.ChiTiet = "- Sinh hóa máu:";
            moi.KQTH = bnTHCLS.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 4;
            moi.TieuDe = "4. Xét nghiệm";
            moi.ChiTiet = "- Vi sinh:";
            moi.KQTH = bnTHCLS.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh)).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 4;
            moi.TieuDe = "4. Xét nghiệm";
            moi.ChiTiet = "- Siêu âm:";
            moi.KQTH = bnTHCLS.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 4;
            moi.TieuDe = "4. Xét nghiệm";
            moi.ChiTiet = "- Soi đáy mắt trực tiếp:";
            moi.KQTH = bnTHCLS.Where(p => p.TenDV.ToLower().Contains("soi đáy mắt trực tiếp")).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 4;
            moi.TieuDe = "4. Xét nghiệm";
            moi.ChiTiet = "- Soi góc TP:";
            moi.KQTH = bnTHCLS.Where(p => p.TenDV.ToLower().Contains("soi góc tiền phòng")).Count();
            _lKQ.Add(moi);
            #endregion
            #region Chuyển viện
            moi = new KQ();
            moi.Stt = 5;
            moi.TieuDe = "5. Chuyển viện";
            moi.ChiTiet = "";
            moi.KQTH = qRV.Where(p => p.Status == 1).Count();
            _lKQ.Add(moi);
            #endregion
            #region Ra viện
            moi = new KQ();
            moi.Stt = 6;
            moi.TieuDe = "6. Ra viện";
            moi.ChiTiet = "";
            //moi.KQTH = qRV.Where(p => p.NoiTru == 1).Where(p => p.Status == 2).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 6;
            moi.TieuDe = "6. Ra viện";
            moi.ChiTiet = "- Đục TTT";
            moi.KQTH = qRV.Where(p => p.NoiTru == 1).Where(p => p.Status == 2).Where(p => p.MaICD.Equals("Q12.0") || p.MaICD.Contains("H25") || p.MaICD.Contains("H26.2")).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 6;
            moi.TieuDe = "6. Ra viện";
            moi.ChiTiet = "- Glocom";
            moi.KQTH = qRV.Where(p => p.NoiTru == 1).Where(p => p.Status == 2).Where(p => p.MaICD.Contains("H40")).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 6;
            moi.TieuDe = "6. Ra viện";
            moi.ChiTiet = "- Mộng";
            moi.KQTH = qRV.Where(p => p.NoiTru == 1).Where(p => p.Status == 2).Where(p => p.MaICD.Equals("H11.0")).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 6;
            moi.TieuDe = "6. Ra viện";
            moi.ChiTiet = "- Quặm";
            moi.KQTH = qRV.Where(p => p.NoiTru == 1).Where(p => p.Status == 2).Where(p => p.MaICD.Equals("H02.0")).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 6;
            moi.TieuDe = "6. Ra viện";
            moi.ChiTiet = "- Khác";
            moi.KQTH = qRV.Where(p => p.NoiTru == 1).Where(p => p.Status == 2)
                           .Where(p => !p.MaICD.Equals("Q12.0") && !p.MaICD.Equals("H02.0") && !p.MaICD.Equals("H11.0") && !p.MaICD.Contains("H40") && !p.MaICD.Contains("H25") && !p.MaICD.Equals("H26.2")).Count();
            _lKQ.Add(moi);

            moi = new KQ();
            moi.Stt = 6;
            moi.TieuDe = "6. Ra viện";
            moi.ChiTiet = "- Nội khoa";
            _lKQ.Add(moi);
            #endregion

            rep.DataSource = _lKQ.ToList();
            rep.lblTuNgay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region class KQ
        private class KQ
        {
            public int Stt { get; set; }
            public string TieuDe { get; set; }
            public string ChiTiet { get; set; }
            public double? KQTH { get; set; }
        }
        #endregion
        #region Tính thứ khi biết ngày
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns>ngày đầu tiên của tuần</returns>
        private DateTime FirstDay(DateTime date)
        {
            //thu = 0: Chủ nhật,..... thu = 6: Thứ bảy
            int ngay = date.Day;
            int thang = date.Month;
            int nam = date.Year;
            if (thang < 3)
            {
                thang = thang + 12;
                nam = nam - 1;
            }
            int thu = Math.Abs(ngay + 2 * thang + 3 * (thang + 1) / 5 + nam + nam / 4) % 7;
            date = date.AddDays(-thu);
            return date;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date">trùng với ngày truyền vào Hàm FirstDay</param>
        /// <returns>ngày cuối cùng của tuần</returns>
        private DateTime LastDay(DateTime date)
        {
            DateTime last = FirstDay(date);
            last = last.AddDays(6);
            return last;
        }

        private int GetWeekNo(DateTime date)
        {
            System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.CreateSpecificCulture("vi-VN");
            System.Globalization.Calendar cal = cul.Calendar;
            int weekNo = cal.GetWeekOfYear(date, cul.DateTimeFormat.CalendarWeekRule, cul.DateTimeFormat.FirstDayOfWeek);
            return weekNo;
        }
        #endregion

        private void cbbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> dsTuan = new List<int>();
            int thang = Convert.ToInt32(cbbThang.SelectedValue);
            int LastDayOfMonth = DateTime.DaysInMonth(DateTime.Now.Year, thang);
            int firstweek = GetWeekNo(new DateTime(DateTime.Now.Year, thang, 1));
            int lastweek = GetWeekNo(new DateTime(DateTime.Now.Year, thang, LastDayOfMonth));
            int numweek = lastweek - firstweek;
            for (int i = 0; i < numweek; i++)
            {
                dsTuan.Add(i + 1);
            }
            cbbTuan.DataSource = dsTuan.ToList();
        }

    }
}