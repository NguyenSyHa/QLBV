using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using QLBV.DungChung;
using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_VP_TheoKeToan_TrongNgay : XtraForm
    {
        List<CanBo> canBos = new List<CanBo>();
        //List<CanBo> canBoChecks = new List<CanBo>();
        //GridCheckMarksSelection gridCheckMarksSA;
        public frm_BC_VP_TheoKeToan_TrongNgay()
        {
            InitializeComponent();
        }

        private void frm_BC_VP_TheoKeToan_TrongNgay_Load(object sender, EventArgs e)
        {
            LoadCombo();
            //cboCanBo.Enabled = false;
            //cboCanBo.Enabled = true;
            dtDenNgay.DateTime = DateTime.Now.Date;
            dtTuNgay.DateTime = DateTime.Now.Date;
        }
        void gridCheckMarks_SelectionChanged(object sender, EventArgs e)
        {
            if (ActiveControl is GridLookUpEdit)
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                foreach (CanBo f in (sender as GridCheckMarksSelection).Selection)
                {
                    if (sb.ToString().Length > 0) { sb.Append(", "); sb2.Append("|"); }
                    sb.Append(f.TenCB);
                }
                (ActiveControl as GridLookUpEdit).Text = sb.ToString();
            }
        }

        private void gridLookUpEdit1_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            GridCheckMarksSelection gridCheckMark = sender is GridLookUpEdit ? (sender as GridLookUpEdit).Properties.Tag as GridCheckMarksSelection :
                                                                                    (sender as GridLookUpEdit).Tag as GridCheckMarksSelection;

            if (gridCheckMark == null) return;
            foreach (CanBo f in gridCheckMark.Selection)
            {
                if (sb.ToString().Length > 0) { sb.Append(", "); sb2.Append("|"); }

                sb.Append(f.TenCB);
            }
            e.DisplayText = sb.ToString();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //if (gridCheckMarksSA != null)
            //    canBoChecks = gridCheckMarksSA.Selection.Cast<CanBo>().ToList();
            if (KTIn())
            {
                InBaoCao();
            }
        }

        private bool KTIn()
        {
            bool rs = true;

            if (checkedListBoxControlCanBo.CheckedItemsCount <= 0)
            {
                MessageBox.Show("Chưa chọn cán bộ");
                return false;
            }
            if (dtTuNgay.EditValue == null || dtDenNgay.EditValue == null)
            {
                MessageBox.Show("Thời gian không được để trống");
                return false;
            }
            if (dtTuNgay.DateTime > dtDenNgay.DateTime)
            {
                MessageBox.Show("Thời gian từ phải nhỏ hơn thời gian đến");
                return false;
            }
            return rs;
        }
        private int GetSongay()
        {
            TimeSpan tsp = dtDenNgay.DateTime - dtTuNgay.DateTime;

            int songay = tsp.Days;
            return songay;
        }
        private static void InBaoCaoExtracted(out List<CanBo> checkCanBos, out List<string> maCanBos, out List<string> _tenCanbos)
        {
            checkCanBos = new List<CanBo>();
            maCanBos = new List<string>();
            _tenCanbos = new List<string>();
        }
        private void InBaoCao()
        {
            List<VienPhiKT> listVienPhi = new List<VienPhiKT>();
            int songay = GetSongay();
            for (int i = 0; i <= songay; i++)
            {
                var ngay = dtTuNgay.DateTime.AddDays(i).Date;
                List<CanBo> checkCanBos;
                List<string> maCanBos;
                List<string> _tenCanbos;
                InBaoCaoExtracted(out checkCanBos, out maCanBos, out _tenCanbos);
                foreach (var itemChecked in checkedListBoxControlCanBo.CheckedItems)
                {
                    CanBo cb = itemChecked as CanBo;
                    checkCanBos.Add(cb);
                    maCanBos.Add(cb.MaCB);
                    _tenCanbos.Add(cb.TenCB);
                }

                //Lấy thông tin bệnh nhân và cán bộ theo ngày đang xét và gói dịch vụ
                var qThuThang0 = (from bn in dataContext.BenhNhans.Where(p => p.TuyenDuoi == 0)
                                  join tu in dataContext.TamUngs.Where(p => p.IDGoiDV <= 0 || p.IDGoiDV == null).Where(o => o.NgayThu.Value.Year == ngay.Date.Year && o.NgayThu.Value.Month == ngay.Date.Month && o.NgayThu.Value.Day == ngay.Date.Day).Where(p => p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan
                                  join tuct in dataContext.TamUngcts.Where(p => p.Status == 0) on tu.IDTamUng equals tuct.IDTamUng
                                  select new { tu.MaCB, bn.MaBNhan, bn.IDDTBN, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, tu.PhanLoai, tu.NgayThu, tuct.MaDV, tuct.Mien, tu.MaKP, tuct.SoLuong, tuct.TienBN, tuct.TrongBH, tuct.ThanhTien, tuct.DonGia, tuct.SoTien, tu.NgoaiGio }).ToList();


                //lấy đối tượng bệnh nhân theo idDTBN
                var qThuThang1 = (from bn in qThuThang0
                                  join dt in dataContext.DTBNs on bn.IDDTBN equals dt.IDDTBN
                                  select new { bn.MaBNhan, bn.TenBNhan, dt.DTBN1, bn.MaDV, bn.TienBN, bn.MaCB, bn.NgoaiGio }).ToList();
                //Lấy thông tin bệnh nhân khám ngoài giờ và KSK
                var qThuThang2 = (from bn in qThuThang1
                                  join dv in dataContext.DichVus on bn.MaDV equals dv.MaDV
                                  select new
                                  {
                                      bn.NgoaiGio,
                                      bn.MaBNhan,
                                      bn.TenBNhan,
                                      DTBN1 = bn.DTBN1.ToUpper().Trim(),
                                      bn.MaDV,
                                      bn.MaCB,
                                      TienBN = (bn.DTBN1 == "KSK" && dv.IDNhom == 13) ? 0 : bn.TienBN,
                                  }).ToList();
                //Tổng số tiền thu thẳng của cán bộ viện
                var qThuThang3 = (from sumTu in qThuThang2
                                  group sumTu by new { sumTu.MaCB } into kq
                                  select new
                                  {
                                      kq.Key.MaCB,
                                      ThuThang = kq.Where(o => o.NgoaiGio != 1).Sum(o => o.TienBN),
                                      Truc = kq.Where(o => o.NgoaiGio == 1).Sum(o => o.TienBN),
                                  }).ToList();

                //Số bệnh nhân đã nộp thu thẳng cho 1 bác sĩ trong ngày đang xét
                var qThuThang4 = (from sumTu in qThuThang2.Where(p => p.MaCB == maCanBos[0])
                                  group sumTu by new { sumTu.MaBNhan, sumTu.TenBNhan } into kq
                                  select new
                                  {
                                      kq.Key.MaBNhan,
                                      kq.Key.TenBNhan,
                                      ThuThang = kq.Where(o => o.NgoaiGio != 1).Where(o => o.MaCB == maCanBos[0]).Sum(o => o.TienBN),
                                      Truc = kq.Where(o => o.NgoaiGio == 1).Where(o => o.MaCB == maCanBos[0]).Sum(o => o.TienBN),
                                  }).ToList();

                //Lấy thông tin bệnh nhân khám sức khỏe trong ngày đang xét
                var qKsk0 = (from bn in dataContext.BenhNhans
                             where bn.IDDTBN == 3
                             join tu in dataContext.TamUngs.Where(p => p.NgayThu.Value.Year == ngay.Date.Year && p.NgayThu.Value.Month == ngay.Date.Month && p.NgayThu.Value.Day == ngay.Date.Day && p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan
                             join tuct in dataContext.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                             join dv in dataContext.DichVus on tuct.MaDV equals dv.MaDV
                             join tn in dataContext.TieuNhomDVs.Where(p => p.IDNhom == 13) on dv.IdTieuNhom equals tn.IdTieuNhom
                             select new
                             {
                                 tu.MaCB,
                                 bn.MaBNhan,
                                 bn.TenBNhan,
                                 tu.SoTien
                             }).ToList();
                // Lấy thông tin Số tiền mà bệnh nhân khám
                var qKsk1 = (from bn in dataContext.BenhNhans
                             where bn.IDDTBN == 3
                             join tu in dataContext.TamUngs on bn.MaBNhan equals tu.MaBNhan
                             where (tu.NgayThu.Value.Year == ngay.Date.Year && tu.NgayThu.Value.Month == ngay.Date.Month && tu.NgayThu.Value.Day == ngay.Date.Day && tu.PhanLoai == 1)
                             select new
                             {
                                 tu.MaCB,
                                 bn.MaBNhan,
                                 bn.TenBNhan,
                                 tu.SoTien
                             }).ToList();

                var qKsk2 = (from tu in qKsk1
                             join bn in qKsk0 on tu.MaBNhan equals bn.MaBNhan into kq
                             from kq1 in kq.DefaultIfEmpty()
                             where kq1 == null
                             select tu).ToList();
                var qKsk3 = qKsk0.Union(qKsk2);
                var qKsk4 = (from sumTu in qKsk3
                             group sumTu by new { sumTu.MaCB } into kq
                             select new
                             {
                                 kq.Key.MaCB,
                                 Ksk = kq.Sum(o => o.SoTien),
                             }).ToList();
                // Lấy thông tin số tiền KSK của từng bệnh nhân
                var qKsk5 = (from sumTu in qKsk3
                             group sumTu by new { sumTu.MaCB, sumTu.MaBNhan } into kq
                             select new
                             {
                                 kq.Key.MaCB,
                                 Ksk = kq.Sum(o => o.SoTien),
                             }).ToList();

                var qNTNT0 = (from bn in dataContext.BenhNhans.Where(o => o.DTuong == "BHYT")
                              join vp in dataContext.VienPhis.Where(o => o.NgayDuyet != null) on bn.MaBNhan equals vp.MaBNhan
                              join tu in dataContext.TamUngs.Where(o => (o.PhanLoai == 1 || o.PhanLoai == 2) && o.NgayThu.Value.Year == ngay.Date.Year && o.NgayThu.Value.Month == ngay.Date.Month && o.NgayThu.Value.Day == ngay.Date.Day) on bn.MaBNhan equals tu.MaBNhan
                              join cbTT in dataContext.CanBoes.Where(o => maCanBos.Contains(o.MaCB)) on tu.MaCB equals cbTT.MaCB
                              join vpct in dataContext.VienPhicts.Where(o => o.TienBN > 0 && (o.TrongBH == 1 || o.TrongBH == 0) && o.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                              select new
                              {
                                  tu.MaCB,
                                  cbTT.TenCB,
                                  vpct.TienBN,
                                  bn.NoiTru,
                                  bn.DTNT,
                                  vp.NgayDuyet
                              }
                                 ).ToList();
                var qNTNT1 = (from sumTu in qNTNT0
                              group sumTu by new { sumTu.MaCB, sumTu.TenCB } into kq
                              select new
                              {
                                  kq.Key.MaCB,
                                  HoTenCB = kq.Key.TenCB,
                                  NgayThang = kq.First().NgayDuyet,
                                  NgoaiTru = kq.Where(o => o.NoiTru == 0 && !o.DTNT).Sum(o => o.TienBN),
                                  NoiTru = kq.Where(o => o.NoiTru == 1 || o.DTNT).Sum(o => o.TienBN),
                              }
                             ).OrderBy(o => o.HoTenCB).ToList();

                var qVPND0 = (from vp in dataContext.VienPhis.Where(o => o.NgayDuyet != null && o.NgayDuyet.Value.Year == ngay.Date.Year && o.NgayDuyet.Value.Month == ngay.Date.Month && o.NgayDuyet.Value.Day == ngay.Date.Day)
                              join vpct in dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                              select new
                              {
                                  vpct.XHH,
                                  vpct.IDTamUng,
                                  vpct.MaDV,
                                  vp.MaBNhan,
                                  vp.NgayDuyet,
                                  vpct.TrongBH,
                                  vpct.MaKP,
                                  vpct.LoaiDV,
                                  vpct.TyLeBHTT,
                                  vpct.ThanhToan,
                                  ThanhTien = vpct.ThanhTien,
                                  TienBN = vpct.TBNCTT + vpct.TBNTT,
                                  TienBH = vpct.TienBH,
                                  vp.NgayTT
                              }).ToList();
                var qVPND1 = (from a in qVPND0
                              select a.MaBNhan).Distinct().ToList();

                DateTime ngaytunew = DungChung.Ham.NgayTu(dtTuNgay.DateTime).AddMonths(-6);
                DateTime ngaydennew = DungChung.Ham.NgayDen(dtDenNgay.DateTime).AddMonths(6);
                var qVPND2 = (from bn in dataContext.BenhNhans
                              join rv in dataContext.RaViens.Where(p => p.NgayRa >= ngaytunew && p.NgayRa <= ngaydennew) on bn.MaBNhan equals rv.MaBNhan
                              join ttbx in dataContext.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                              where (bn.IDDTBN == 2 && bn.NoiTru == 1)
                              select new
                              {
                                  MaKCB = bn.MaKCB == null ? "" : bn.MaKCB.Trim().ToUpper(),
                                  bn.DChi,
                                  bn.HanBHDen,
                                  bn.HanBHTu,
                                  bn.TuyenDuoi,
                                  bn.DTNT,
                                  bn.DTuong,
                                  bn.NoiTru,
                                  bn.NoiTinh,
                                  bn.Tuyen,
                                  bn.MaBNhan,
                                  bn.TenBNhan,
                                  bn.NamSinh,
                                  bn.NgaySinh,
                                  bn.ThangSinh,
                                  bn.SThe,
                                  bn.GTinh,
                                  bn.MaCS,
                                  MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong.Trim().ToUpper(),
                                  bn.CapCuu,
                                  bn.Tuoi,
                                  bn.KhuVuc,
                                  bn.MaBV,
                                  bn.SoDK,
                                  bn.NNhap,
                                  rv.MaICD,
                                  rv.NgayVao,
                                  rv.NgayRa,
                                  rv.SoNgaydt,
                                  rv.Status,
                                  rv.KetQua,
                                  rv.ChanDoan,
                                  KhoaTongKet = rv.MaKP,
                                  MaKPBn = bn.MaKP
                              }).Distinct().ToList();
                var qVPND3 = (from a in qVPND1
                              join bn in qVPND2 on a equals bn.MaBNhan
                              select new
                              {
                                  bn.MaKCB,
                                  bn.DChi,
                                  bn.HanBHDen,
                                  bn.HanBHTu,
                                  bn.TuyenDuoi,
                                  bn.DTNT,
                                  bn.DTuong,
                                  bn.NoiTru,
                                  bn.NoiTinh,
                                  bn.Tuyen,
                                  bn.MaBNhan,
                                  bn.TenBNhan,
                                  bn.NamSinh,
                                  bn.NgaySinh,
                                  bn.ThangSinh,
                                  bn.SThe,
                                  bn.GTinh,
                                  bn.MaCS,
                                  bn.MaDTuong,
                                  bn.CapCuu,
                                  bn.Tuoi,
                                  bn.KhuVuc,
                                  bn.MaBV,
                                  bn.SoDK,
                                  bn.MaICD,
                                  bn.NgayVao,
                                  bn.NgayRa,
                                  bn.SoNgaydt,
                                  bn.Status,
                                  bn.KetQua,
                                  bn.ChanDoan,
                                  bn.NNhap,
                                  bn.MaKPBn,
                                  KhoaTongKet = bn.KhoaTongKet
                              }).OrderBy(p => p.MaBNhan).ToList();

                var maDTuong = dataContext.DTuongs.Where(o => o.Status == 1).Select(o => o.MaDTuong).ToList();
                maDTuong.Add("");
                var qVPND4 = (from a in qVPND0.Where(p => p.LoaiDV == 0) // check để lấy DV theo yêu cầu
                              join bn in qVPND3 on a.MaBNhan equals bn.MaBNhan
                              join dt in maDTuong on bn.MaDTuong equals dt
                              join cskcb in dataContext.BenhViens.Where(o => o.Connect) on bn.MaKCB equals cskcb.MaBV
                              where (a.ThanhToan == 0)
                              select new
                              {
                                  a.XHH,
                                  bn.MaKCB,
                                  a.TrongBH,
                                  a.MaKP,
                                  bn.DChi,
                                  bn.HanBHDen,
                                  bn.HanBHTu,
                                  bn.TuyenDuoi,
                                  bn.DTNT,
                                  bn.DTuong,
                                  bn.NoiTru,
                                  bn.SoDK,
                                  bn.NoiTinh,
                                  bn.Tuyen,
                                  bn.MaBNhan,
                                  bn.TenBNhan,
                                  bn.NamSinh,
                                  bn.NgaySinh,
                                  bn.ThangSinh,
                                  bn.SThe,
                                  bn.GTinh,
                                  bn.MaCS,
                                  bn.MaDTuong,
                                  bn.CapCuu,
                                  bn.MaICD,
                                  ChanDoan = (bn.MaICD != null && bn.MaICD != "") ? bn.ChanDoan : "",
                                  bn.NgayVao,
                                  bn.NgayRa,
                                  bn.SoNgaydt,
                                  bn.Status,
                                  bn.KetQua,
                                  a.MaDV,
                                  a.ThanhTien,
                                  //a.ThanhTienVTYT,
                                  a.TienBN,
                                  a.TienBH,
                                  a.NgayTT,
                                  bn.Tuoi,
                                  bn.KhuVuc,
                                  bn.MaBV,
                                  bn.NNhap,
                                  TyLeBHTT = bn.DTuong == "BHYT" ? a.TyLeBHTT : 0,
                                  KhoaTongKet = MaKPQD(bn.KhoaTongKet),
                                  bn.MaKPBn
                              }).OrderBy(p => p.MaBNhan).ToList();



                var qVPND5 = (from a in qVPND4
                              join dvx in dataContext.DichVus on a.MaDV equals dvx.MaDV
                              where a.TrongBH == 0
                              //where ckcDVTheoYC.Checked? 
                              where a.XHH == 0
                              group new { a, dv = dvx } by new { a.TyLeBHTT, a.NNhap, a.MaKCB, a.SoDK, a.HanBHDen, a.HanBHTu, a.DChi, a.SoNgaydt, a.DTNT, a.TuyenDuoi, a.NgayTT, a.DTuong, a.NoiTru, a.MaBNhan, a.NoiTinh, a.TenBNhan, a.NamSinh, a.NgaySinh, a.ThangSinh, a.GTinh, a.SThe, a.MaCS, a.Tuyen, a.NgayVao, a.MaICD, a.ChanDoan, a.NgayRa, a.MaDTuong, a.CapCuu, a.KetQua, a.Status, a.Tuoi, a.KhuVuc, a.MaBV, a.KhoaTongKet } into kq
                              select new
                              {
                                  kq.Key.MaBNhan,
                                  kq.Key.MaKCB,
                                  MaKP = MaKPQD(0),
                                  kq.Key.SoNgaydt,
                                  kq.Key.DTuong,
                                  kq.Key.NoiTru,
                                  TrongBH = 0,
                                  TyLeBHTT = kq.Key.TyLeBHTT,
                                  Ma_pttt_qt = String.Join(";", kq.Where(p => p.dv.IDNhom == 10).Select(p => p.dv.MaQD).Where(p => p != null).Distinct()),
                                  Thuoc = kq.Where(p => p.dv.IDNhom == 4).Where(p => p.dv.BHTT == 100).Sum(p => p.a.ThanhTien),
                                  CDHA = kq.Where(p => p.dv.IDNhom == 2 || p.dv.IDNhom == 3).Sum(p => p.a.ThanhTien),
                                  Congkham = kq.Where(p => p.dv.IDNhom == 13).Sum(p => p.a.ThanhTien),
                                  TienGiuong = kq.Where(p => p.dv.IDNhom == 14 || p.dv.IDNhom == 15).Sum(p => p.a.ThanhTien),
                                  Xetnghiem = kq.Where(p => p.dv.IDNhom == 1).Sum(p => p.a.ThanhTien),
                                  Mau = kq.Where(p => p.dv.IDNhom == 7).Sum(p => p.a.ThanhTien),
                                  TTPT = kq.Where(p => p.dv.IDNhom == 8).Sum(p => p.a.ThanhTien),
                                  VTYT = kq.Where(p => p.dv.IDNhom == 10).Sum(p => p.a.ThanhTien),
                                  DVKT_tl = kq.Where(p => p.dv.IDNhom == 9).Sum(p => p.a.ThanhTien),
                                  Thuoc_tl = kq.Where(p => p.dv.IDNhom == 6).Sum(p => p.a.ThanhTien),
                                  VTYT_tl = kq.Where(p => p.dv.IDNhom == 11).Sum(p => p.a.ThanhTien),
                                  CPVanchuyen = kq.Where(p => p.dv.IDNhom == 12).Sum(p => p.a.ThanhTien),
                                  CPNgoaiBH = kq.Where(p => p.dv.IDNhom == 12).Sum(p => p.a.ThanhTien),
                                  ThanhTien = kq.Sum(p => p.a.ThanhTien),
                                  Tongchi = kq.Sum(p => p.a.ThanhTien),
                                  Tongcong = kq.Sum(p => p.a.ThanhTien),
                                  TienBN = kq.Sum(p => p.a.TienBN),
                                  TienBH = kq.Sum(p => p.a.TienBH),
                              }).ToList();
                var qVPND6 = qVPND5.Select(x => new
                {
                    x.MaBNhan,
                    x.MaKCB,
                    x.MaKP,
                    x.SoNgaydt,
                    x.DTuong,
                    x.NoiTru,
                    x.TrongBH,
                    TyLeBHTT = x.TyLeBHTT,
                    x.Ma_pttt_qt,
                    TongTien = x.Thuoc + x.CDHA + x.Congkham + x.TienGiuong + x.Xetnghiem + x.Mau + x.TTPT + x.VTYT + x.VTYT_tl + x.DVKT_tl + x.Thuoc_tl + x.CPVanchuyen,
                    Thuoc = x.Thuoc,
                    CDHA = x.CDHA,
                    Congkham = x.Congkham,
                    TienGiuong = x.TienGiuong,
                    Xetnghiem = x.Xetnghiem,
                    Mau = x.Mau,
                    TTPT = x.TTPT,
                    VTYT = x.VTYT,
                    DVKT_tl = x.DVKT_tl,
                    x.Thuoc_tl,
                    VTYT_tl = x.VTYT_tl,
                    CPVanchuyen = x.CPVanchuyen,
                    CPNgoaiBH = x.CPNgoaiBH,
                    ThanhTien = x.ThanhTien,
                    Tongchi = x.Tongchi,
                    Tongcong = x.Tongcong,
                    TienBH = x.TienBH,
                    TienBN = x.TienBN,

                }).ToList();

                var qVPND7 = (from vp in qVPND6
                              join tu in dataContext.TamUngs.Where(o => (o.PhanLoai == 1 || o.PhanLoai == 2)) on vp.MaBNhan equals tu.MaBNhan
                              join cb in dataContext.CanBoes.Where(o => maCanBos.Contains(o.MaCB)) on tu.MaCB equals cb.MaCB
                              select new { cb.MaCB, cb.TenCB, vp.TongTien }).ToList();

                var qVPND8 = (from sumTu in qVPND7
                              group sumTu by new { sumTu.MaCB, sumTu.TenCB } into kq
                              select new
                              {
                                  kq.Key.MaCB,
                                  HoTenCB = kq.Key.TenCB,
                                  VienPhiNhanDan = kq.Sum(o => o.TongTien),
                              }
                            ).OrderBy(o => o.HoTenCB).ToList();

                #region Chi tiết từng cán bộ viện thu tiền từng khoản,mục ( Thu thằng, KSK, % Ngoại trú,% Nội trú, Viện phí nhân dân, Trực, tổng thu)
                int dem = 1;
                foreach (var item in checkCanBos)
                {
                    VienPhiKT vp = new VienPhiKT();
                    vp.STT = dem;
                    vp.HoTenCB = item.TenCB;
                    vp.MaCB = item.MaCB;
                    vp.NgayThang = ngay.ToString("dd/MM/yyyy");

                    var cb1 = qThuThang3.FirstOrDefault(o => o.MaCB == item.MaCB);
                    if (cb1 != null)
                    {
                        vp.ThuThang = cb1.ThuThang;
                        vp.Truc = cb1.Truc;
                    }
                    var cb2 = qNTNT1.FirstOrDefault(o => o.MaCB == item.MaCB);
                    if (cb2 != null)
                    {
                        vp.NoiTru = cb2.NoiTru;
                        vp.NgoaiTru = cb2.NgoaiTru;
                    }

                    var cb3 = qVPND8.FirstOrDefault(o => o.MaCB == item.MaCB);
                    if (cb3 != null)
                    {
                        vp.VienPhiNhanDan = cb3.VienPhiNhanDan;
                    }

                    var cb4 = qKsk4.FirstOrDefault(o => o.MaCB == item.MaCB);
                    if (cb4 != null)
                    {
                        vp.Ksk = cb4.Ksk ?? 0;
                    }

                    vp.TongThu = vp.Ksk + vp.ThuThang + vp.NgoaiTru + vp.NoiTru + vp.VienPhiNhanDan + vp.Truc;
                    
                    listVienPhi.Add(vp);
                    dem++;
                }

                #endregion

            }
            frmIn frm = new frmIn();

            BaoCao.Rep_BC_VienPhiHangNgayTheoKeToan_30005 rep = new BaoCao.Rep_BC_VienPhiHangNgayTheoKeToan_30005();
            rep.DataSource = listVienPhi;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void LoadCombo()
        {
            canBos = (from cb in dataContext.CanBoes
                      join kp in dataContext.KPhongs on cb.MaKP equals kp.MaKP
                      where kp.PLoai == "Kế toán"
                      select cb).ToList();

            checkedListBoxControlCanBo.DataSource = canBos;
            checkedListBoxControlCanBo.CheckAll();

            //cboCanBo.Properties.DataSource = canBos;
            //cboCanBo.Properties.ValueMember = "MaCB";
            //cboCanBo.Properties.DisplayMember = "TenCB";
            //cboCanBo.CustomDisplayText += new CustomDisplayTextEventHandler(gridLookUpEdit1_CustomDisplayText);
            //cboCanBo.Properties.PopulateViewColumns();


            //gridCheckMarksSA = new GridCheckMarksSelection(cboCanBo);
            //gridCheckMarksSA.SelectionChanged += new GridCheckMarksSelection.SelectionChangedEventHandler(gridCheckMarks_SelectionChanged);
            //cboCanBo.Properties.Tag = gridCheckMarksSA;
            //if (gridCheckMarksSA != null)
            //    gridCheckMarksSA.SelectAll(canBos);

        }

        public class VienPhiKT
        {
            public string NgayThang { get; set; }
            public int STT { get; set; }
            public string MaCB { get; set; }
            public string HoTenCB { get; set; }
            public double ThuThang { get; set; }
            public double Ksk { get; set; }
            public double NgoaiTru { get; set; }
            public double NoiTru { get; set; }
            public double VienPhiNhanDan { get; set; }
            public double Truc { get; set; }
            public double TongThu { get; set; }
            public string Mabenhnhan { get; set; }
            public string Tenbnhan { get; set; }
        }

        public class VPktNhanvien
        {
            public DateTime NgayThang { get; set; }
            public string Theongay { get; set; }
            public int STT { get; set; }
            public string MaCB { get; set; }
            public string HoTenCB { get; set; }
            public string Mabenhnhan { get; set; }
            public string Tenbnhan { get; set; }
            public double ThuThang { get; set; }
            public double Ksk { get; set; }
            public double NgoaiTru { get; set; }
            public double NoiTru { get; set; }
            public double VienPhiNhanDan { get; set; }
            public double Truc { get; set; }
            public double TongThu { get; set; }
            public string Nguoilapbieu { get; set; }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                checkedListBoxControlCanBo.CheckAll();
            }
            else
            {
                checkedListBoxControlCanBo.UnCheckAll();
            }
        }

        private string MaKPQD(int _MaKPc)
        {
            string rs = "";
            var q = dataContext.KPhongs.Where(p => p.MaKP == _MaKPc && p.PLoai == "Lâm sàng").ToList();
            if (q.Count > 0)
                rs = q.First().MaQD == null ? "" : q.First().MaQD.ToString();
            return rs;
        }

        private void checkedListBoxControlCanBo_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            //checkedListBoxControlCanBo.Refresh();
            if (DungChung.Bien.MaBV == "30005")
            {
                int n = checkedListBoxControlCanBo.CheckedItemsCount;
                if (n == 1)
                {
                    btnMauchitiet.Visible = true;
                    btnOK.Enabled = false;
                    //_mabn=checkedListBoxControlCanBo
                }

                else {
                    btnMauchitiet.Visible = false;
                    btnOK.Enabled = true;
                }
            }
            else btnMauchitiet.Visible = false;
        }

        private void btnMauchitiet_Click(object sender, EventArgs e)
        {
            #region Chi tiết mỗi bệnh nhân đã nộp các khoản (( Thu thằng, KSK, % Ngoại trú,% Nội trú, Viện phí nhân dân, Trực, tổng thu) cho mỗi cán bộ viện

            List<VPktNhanvien> listVPktNhanvien = new List<VPktNhanvien>();
            int songay = GetSongay();
            for (int i = 0; i <= songay; i++)
            {
                
                var ngay = dtTuNgay.DateTime.AddDays(i).Date;
                List<CanBo> checkCanBos;
                List<string> maCanBos;
                List<string> _tenCanbos;
                InBaoCaoExtracted(out checkCanBos, out maCanBos, out _tenCanbos);
                foreach (var itemChecked in checkedListBoxControlCanBo.CheckedItems)
                {
                    CanBo cb = itemChecked as CanBo;
                    checkCanBos.Add(cb);
                    maCanBos.Add(cb.MaCB);
                    _tenCanbos.Add(cb.TenCB);
                }
                    //Lấy thông tin bệnh nhân và cán bộ theo ngày đang xét và gói dịch vụ
                    var qThuThang0 = (from bn in dataContext.BenhNhans.Where(p => p.TuyenDuoi == 0)
                                      join tu in dataContext.TamUngs.Where(p => p.IDGoiDV <= 0 || p.IDGoiDV == null).Where(o => o.NgayThu.Value.Year == ngay.Date.Year && o.NgayThu.Value.Month == ngay.Date.Month && o.NgayThu.Value.Day == ngay.Date.Day).Where(p => p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan
                                      join tuct in dataContext.TamUngcts.Where(p => p.Status == 0) on tu.IDTamUng equals tuct.IDTamUng
                                      select new { tu.MaCB, bn.MaBNhan, bn.IDDTBN, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, tu.PhanLoai, tu.NgayThu, tuct.MaDV, tuct.Mien, tu.MaKP, tuct.SoLuong, tuct.TienBN, tuct.TrongBH, tuct.ThanhTien, tuct.DonGia, tuct.SoTien, tu.NgoaiGio }).ToList();


                    //lấy đối tượng bệnh nhân theo idDTBN
                    var qThuThang1 = (from bn in qThuThang0
                                      join dt in dataContext.DTBNs on bn.IDDTBN equals dt.IDDTBN
                                      select new { bn.MaBNhan, bn.TenBNhan, dt.DTBN1, bn.MaDV, bn.TienBN, bn.MaCB, bn.NgoaiGio }).ToList();
                    //Lấy thông tin bệnh nhân khám ngoài giờ và KSK
                    var qThuThang2 = (from bn in qThuThang1
                                      join dv in dataContext.DichVus on bn.MaDV equals dv.MaDV
                                      select new
                                      {
                                          bn.NgoaiGio,
                                          bn.MaBNhan,
                                          bn.TenBNhan,
                                          DTBN1 = bn.DTBN1.ToUpper().Trim(),
                                          bn.MaDV,
                                          bn.MaCB,
                                          TienBN = (bn.DTBN1 == "KSK" && dv.IDNhom == 13) ? 0 : bn.TienBN,
                                      }).ToList();
                    //Số bệnh nhân đã nộp thu thẳng cho 1 bác sĩ trong ngày đang xét
                    var qThuThang4 = (from sumTu in qThuThang2.Where(p => p.MaCB == maCanBos[0])
                                      group sumTu by new { sumTu.MaBNhan, sumTu.TenBNhan } into kq
                                      select new
                                      {
                                          kq.Key.MaBNhan,
                                          kq.Key.TenBNhan,
                                          ThuThang = kq.Where(o => o.NgoaiGio != 1).Where(o => o.MaCB == maCanBos[0]).Sum(o => o.TienBN),
                                          Truc = kq.Where(o => o.NgoaiGio == 1).Where(o => o.MaCB == maCanBos[0]).Sum(o => o.TienBN),
                                      }).ToList();

                    var qThuThang5 = (from sumTu in qThuThang2.Where(p => p.MaCB == maCanBos[0])
                                      group sumTu by new { sumTu.MaBNhan, sumTu.TenBNhan } into kq
                                      select new
                                      {
                                          kq.Key.MaBNhan,
                                          kq.Key.TenBNhan,
                                      }).ToList();

                    //Lấy thông tin bệnh nhân khám sức khỏe trong ngày đang xét
                    var qKsk0 = (from bn in dataContext.BenhNhans
                                 where bn.IDDTBN == 3
                                 join tu in dataContext.TamUngs.Where(p => p.NgayThu.Value.Year == ngay.Date.Year && p.NgayThu.Value.Month == ngay.Date.Month && p.NgayThu.Value.Day == ngay.Date.Day && p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan
                                 join tuct in dataContext.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                                 join dv in dataContext.DichVus on tuct.MaDV equals dv.MaDV
                                 join tn in dataContext.TieuNhomDVs.Where(p => p.IDNhom == 13) on dv.IdTieuNhom equals tn.IdTieuNhom
                                 select new
                                 {
                                     tu.MaCB,
                                     bn.MaBNhan,
                                     bn.TenBNhan,
                                     tu.SoTien
                                 }).ToList();
                    // Lấy thông tin Số tiền mà bệnh nhân khám
                    var qKsk1 = (from bn in dataContext.BenhNhans
                                 where bn.IDDTBN == 3
                                 join tu in dataContext.TamUngs on bn.MaBNhan equals tu.MaBNhan
                                 where (tu.NgayThu.Value.Year == ngay.Date.Year && tu.NgayThu.Value.Month == ngay.Date.Month && tu.NgayThu.Value.Day == ngay.Date.Day && tu.PhanLoai == 1)
                                 select new
                                 {
                                     tu.MaCB,
                                     bn.MaBNhan,
                                     bn.TenBNhan,
                                     tu.SoTien
                                 }).ToList();

                    var qKsk2 = (from tu in qKsk1
                                 join bn in qKsk0 on tu.MaBNhan equals bn.MaBNhan into kq
                                 from kq1 in kq.DefaultIfEmpty()
                                 where kq1 == null
                                 select tu).ToList();
                    var qKsk3 = qKsk0.Union(qKsk2);
                    var qKsk4 = (from sumTu in qKsk3.Where(p => p.MaCB == maCanBos[0])
                                 group sumTu by new { sumTu.MaBNhan, sumTu.TenBNhan } into kq
                                 select new
                                 {
                                     kq.Key.MaBNhan,
                                     kq.Key.TenBNhan,
                                     Ksk = kq.Where(p => p.MaCB == maCanBos[0]).Sum(o => o.SoTien),
                                 }).ToList();
                    var qKsk5 = (from sumTu in qKsk3.Where(p => p.MaCB == maCanBos[0])
                                 group sumTu by new { sumTu.MaBNhan, sumTu.TenBNhan } into kq
                                 select new
                                 {
                                     kq.Key.MaBNhan,
                                     kq.Key.TenBNhan
                                 }).ToList();

                    var th = qThuThang5.Union(qKsk5);
                    #region
                    //Lấy dữ liệu nội trú
                    var qNTNT0 = (from bn in dataContext.BenhNhans.Where(o => o.DTuong == "BHYT")
                                  join vp in dataContext.VienPhis.Where(o => o.NgayDuyet != null) on bn.MaBNhan equals vp.MaBNhan
                                  join tu in dataContext.TamUngs.Where(o => (o.PhanLoai == 1 || o.PhanLoai == 2) && o.NgayThu.Value.Year == ngay.Date.Year && o.NgayThu.Value.Month == ngay.Date.Month && o.NgayThu.Value.Day == ngay.Date.Day) on bn.MaBNhan equals tu.MaBNhan
                                  join cbTT in dataContext.CanBoes.Where(o => maCanBos.Contains(o.MaCB)) on tu.MaCB equals cbTT.MaCB
                                  join vpct in dataContext.VienPhicts.Where(o => o.TienBN > 0 && (o.TrongBH == 1 || o.TrongBH == 0) && o.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                                  select new
                                  {
                                      tu.MaCB,
                                      cbTT.TenCB,
                                      bn.MaBNhan,
                                      bn.TenBNhan,
                                      vpct.TienBN,
                                      bn.NoiTru,
                                      bn.DTNT,
                                      vp.NgayDuyet
                                  }).ToList();
                    #endregion
                    #region
                    var qNTNT1 = (from sumTu in qNTNT0.Where(p=>p.MaCB == maCanBos[0])
                                  group sumTu by new { sumTu.MaBNhan, sumTu.TenBNhan } into kq
                                  select new
                                  {
                                      kq.Key.MaBNhan,
                                      HotenBN = kq.Key.TenBNhan,
                                      NgayThang = kq.First().NgayDuyet,
                                      NgoaiTru = kq.Where(o => o.NoiTru == 0 && !o.DTNT).Where(p=>p.MaCB == maCanBos[0]).Sum(o => o.TienBN),
                                      NoiTru = kq.Where(o => o.NoiTru == 1 || o.DTNT).Where(p => p.MaCB == maCanBos[0]).Sum(o => o.TienBN),
                                  }
                                 ).OrderBy(o => o.HotenBN).ToList();
                    #endregion
                    #region
                    var qNTNT2 = (from sumTu in qNTNT0.Where(p => p.MaCB == maCanBos[0])
                                  group sumTu by new { sumTu.MaBNhan, sumTu.TenBNhan } into kq
                                  select new
                                  {
                                      kq.Key.MaBNhan,
                                      kq.Key.TenBNhan
                                  }
                                 ).ToList();
                    #endregion
                    var th1 = th.Union(qNTNT2);
                    #region
                    var qVPND0 = (from vp in dataContext.VienPhis.Where(o => o.NgayDuyet != null && o.NgayDuyet.Value.Year == ngay.Date.Year && o.NgayDuyet.Value.Month == ngay.Date.Month && o.NgayDuyet.Value.Day == ngay.Date.Day)
                                  join vpct in dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                  select new
                                  {
                                      vpct.XHH,
                                      vpct.IDTamUng,
                                      vpct.MaDV,
                                      vp.MaBNhan,
                                      vp.NgayDuyet,
                                      vpct.TrongBH,
                                      vpct.MaKP,
                                      vpct.LoaiDV,
                                      vpct.TyLeBHTT,
                                      vpct.ThanhToan,
                                      ThanhTien = vpct.ThanhTien,
                                      TienBN = vpct.TBNCTT + vpct.TBNTT,
                                      TienBH = vpct.TienBH,
                                      vp.NgayTT
                                  }).ToList();

                    var qVPND1 = (from a in qVPND0
                                  select a.MaBNhan).Distinct().ToList();
                    #endregion
                    #region
                    DateTime ngaytunew = DungChung.Ham.NgayTu(dtTuNgay.DateTime).AddMonths(-6);
                    DateTime ngaydennew = DungChung.Ham.NgayDen(dtDenNgay.DateTime).AddMonths(6);

                    var qVPND2 = (from bn in dataContext.BenhNhans
                                  join rv in dataContext.RaViens.Where(p => p.NgayRa >= ngaytunew && p.NgayRa <= ngaydennew) on bn.MaBNhan equals rv.MaBNhan
                                  join ttbx in dataContext.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                                  where (bn.IDDTBN == 2 && bn.NoiTru == 1)
                                  select new
                                  {
                                      MaKCB = bn.MaKCB == null ? "" : bn.MaKCB.Trim().ToUpper(),
                                      bn.DChi,
                                      bn.HanBHDen,
                                      bn.HanBHTu,
                                      bn.TuyenDuoi,
                                      bn.DTNT,
                                      bn.DTuong,
                                      bn.NoiTru,
                                      bn.NoiTinh,
                                      bn.Tuyen,
                                      bn.MaBNhan,
                                      bn.TenBNhan,
                                      bn.NamSinh,
                                      bn.NgaySinh,
                                      bn.ThangSinh,
                                      bn.SThe,
                                      bn.GTinh,
                                      bn.MaCS,
                                      MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong.Trim().ToUpper(),
                                      bn.CapCuu,
                                      bn.Tuoi,
                                      bn.KhuVuc,
                                      bn.MaBV,
                                      bn.SoDK,
                                      bn.NNhap,
                                      rv.MaICD,
                                      rv.NgayVao,
                                      rv.NgayRa,
                                      rv.SoNgaydt,
                                      rv.Status,
                                      rv.KetQua,
                                      rv.ChanDoan,
                                      KhoaTongKet = rv.MaKP,
                                      MaKPBn = bn.MaKP
                                  }).Distinct().ToList();
                    #endregion
                    #region
                    var qVPND3 = (from a in qVPND1
                                  join bn in qVPND2 on a equals bn.MaBNhan
                                  select new
                                  {
                                      bn.MaKCB,
                                      bn.DChi,
                                      bn.HanBHDen,
                                      bn.HanBHTu,
                                      bn.TuyenDuoi,
                                      bn.DTNT,
                                      bn.DTuong,
                                      bn.NoiTru,
                                      bn.NoiTinh,
                                      bn.Tuyen,
                                      bn.MaBNhan,
                                      bn.TenBNhan,
                                      bn.NamSinh,
                                      bn.NgaySinh,
                                      bn.ThangSinh,
                                      bn.SThe,
                                      bn.GTinh,
                                      bn.MaCS,
                                      bn.MaDTuong,
                                      bn.CapCuu,
                                      bn.Tuoi,
                                      bn.KhuVuc,
                                      bn.MaBV,
                                      bn.SoDK,
                                      bn.MaICD,
                                      bn.NgayVao,
                                      bn.NgayRa,
                                      bn.SoNgaydt,
                                      bn.Status,
                                      bn.KetQua,
                                      bn.ChanDoan,
                                      bn.NNhap,
                                      bn.MaKPBn,
                                      KhoaTongKet = bn.KhoaTongKet
                                  }).OrderBy(p => p.MaBNhan).ToList();
                    #endregion
                    var maDTuong = dataContext.DTuongs.Where(o => o.Status == 1).Select(o => o.MaDTuong).ToList();
                    maDTuong.Add("");
                    #region
                    var qVPND4 = (from a in qVPND0.Where(p => p.LoaiDV == 0) // check để lấy DV theo yêu cầu
                                  join bn in qVPND3 on a.MaBNhan equals bn.MaBNhan
                                  join dt in maDTuong on bn.MaDTuong equals dt
                                  join cskcb in dataContext.BenhViens.Where(o => o.Connect) on bn.MaKCB equals cskcb.MaBV
                                  where (a.ThanhToan == 0)
                                  select new
                                  {
                                      a.XHH,
                                      bn.MaKCB,
                                      a.TrongBH,
                                      a.MaKP,
                                      bn.DChi,
                                      bn.HanBHDen,
                                      bn.HanBHTu,
                                      bn.TuyenDuoi,
                                      bn.DTNT,
                                      bn.DTuong,
                                      bn.NoiTru,
                                      bn.SoDK,
                                      bn.NoiTinh,
                                      bn.Tuyen,
                                      bn.MaBNhan,
                                      bn.TenBNhan,
                                      bn.NamSinh,
                                      bn.NgaySinh,
                                      bn.ThangSinh,
                                      bn.SThe,
                                      bn.GTinh,
                                      bn.MaCS,
                                      bn.MaDTuong,
                                      bn.CapCuu,
                                      bn.MaICD,
                                      ChanDoan = (bn.MaICD != null && bn.MaICD != "") ? bn.ChanDoan : "",
                                      bn.NgayVao,
                                      bn.NgayRa,
                                      bn.SoNgaydt,
                                      bn.Status,
                                      bn.KetQua,
                                      a.MaDV,
                                      a.ThanhTien,
                                      //a.ThanhTienVTYT,
                                      a.TienBN,
                                      a.TienBH,
                                      a.NgayTT,
                                      bn.Tuoi,
                                      bn.KhuVuc,
                                      bn.MaBV,
                                      bn.NNhap,
                                      TyLeBHTT = bn.DTuong == "BHYT" ? a.TyLeBHTT : 0,
                                      KhoaTongKet =  MaKPQD(bn.KhoaTongKet),
                                      bn.MaKPBn
                                  }).OrderBy(p => p.MaBNhan).ToList();

#endregion
                    #region
                    var qVPND5 = (from a in qVPND4
                                  join dvx in dataContext.DichVus on a.MaDV equals dvx.MaDV
                                  where a.TrongBH == 0
                                  //where ckcDVTheoYC.Checked? 
                                  where a.XHH == 0
                                  group new { a, dv = dvx } by new { a.TyLeBHTT, a.NNhap, a.MaKCB, a.SoDK, a.HanBHDen, a.HanBHTu, a.DChi, a.SoNgaydt, a.DTNT, a.TuyenDuoi, a.NgayTT, a.DTuong, a.NoiTru, a.MaBNhan, a.NoiTinh, a.TenBNhan, a.NamSinh, a.NgaySinh, a.ThangSinh, a.GTinh, a.SThe, a.MaCS, a.Tuyen, a.NgayVao, a.MaICD, a.ChanDoan, a.NgayRa, a.MaDTuong, a.CapCuu, a.KetQua, a.Status, a.Tuoi, a.KhuVuc, a.MaBV, a.KhoaTongKet } into kq
                                  select new
                                  {
                                      kq.Key.MaBNhan,
                                      kq.Key.TenBNhan,
                                      kq.Key.MaKCB,
                                      MaKP = MaKPQD(0),
                                      kq.Key.SoNgaydt,
                                      kq.Key.DTuong,
                                      kq.Key.NoiTru,
                                      TrongBH = 0,
                                      TyLeBHTT = kq.Key.TyLeBHTT,
                                      Ma_pttt_qt = String.Join(";", kq.Where(p => p.dv.IDNhom == 10).Select(p => p.dv.MaQD).Where(p => p != null).Distinct()),
                                      Thuoc = kq.Where(p => p.dv.IDNhom == 4).Where(p => p.dv.BHTT == 100).Sum(p => p.a.ThanhTien),
                                      CDHA = kq.Where(p => p.dv.IDNhom == 2 || p.dv.IDNhom == 3).Sum(p => p.a.ThanhTien),
                                      Congkham = kq.Where(p => p.dv.IDNhom == 13).Sum(p => p.a.ThanhTien),
                                      TienGiuong = kq.Where(p => p.dv.IDNhom == 14 || p.dv.IDNhom == 15).Sum(p => p.a.ThanhTien),
                                      Xetnghiem = kq.Where(p => p.dv.IDNhom == 1).Sum(p => p.a.ThanhTien),
                                      Mau = kq.Where(p => p.dv.IDNhom == 7).Sum(p => p.a.ThanhTien),
                                      TTPT = kq.Where(p => p.dv.IDNhom == 8).Sum(p => p.a.ThanhTien),
                                      VTYT = kq.Where(p => p.dv.IDNhom == 10).Sum(p => p.a.ThanhTien),
                                      DVKT_tl = kq.Where(p => p.dv.IDNhom == 9).Sum(p => p.a.ThanhTien),
                                      Thuoc_tl = kq.Where(p => p.dv.IDNhom == 6).Sum(p => p.a.ThanhTien),
                                      VTYT_tl = kq.Where(p => p.dv.IDNhom == 11).Sum(p => p.a.ThanhTien),
                                      CPVanchuyen = kq.Where(p => p.dv.IDNhom == 12).Sum(p => p.a.ThanhTien),
                                      CPNgoaiBH = kq.Where(p => p.dv.IDNhom == 12).Sum(p => p.a.ThanhTien),
                                      ThanhTien = kq.Sum(p => p.a.ThanhTien),
                                      Tongchi = kq.Sum(p => p.a.ThanhTien),
                                      Tongcong = kq.Sum(p => p.a.ThanhTien),
                                      TienBN = kq.Sum(p => p.a.TienBN),
                                      TienBH = kq.Sum(p => p.a.TienBH),
                                  }).ToList();
#endregion
                    #region
                    var qVPND6 = qVPND5.Select(x => new
                    {
                        x.MaBNhan,
                        x.TenBNhan,
                        x.MaKCB,
                        x.MaKP,
                        x.SoNgaydt,
                        x.DTuong,
                        x.NoiTru,
                        x.TrongBH,
                        TyLeBHTT = x.TyLeBHTT,
                        x.Ma_pttt_qt,
                        TongTien = x.Thuoc + x.CDHA + x.Congkham + x.TienGiuong + x.Xetnghiem + x.Mau + x.TTPT + x.VTYT + x.VTYT_tl + x.DVKT_tl + x.Thuoc_tl + x.CPVanchuyen,
                        Thuoc = x.Thuoc,
                        CDHA = x.CDHA,
                        Congkham = x.Congkham,
                        TienGiuong = x.TienGiuong,
                        Xetnghiem = x.Xetnghiem,
                        Mau = x.Mau,
                        TTPT = x.TTPT,
                        VTYT = x.VTYT,
                        DVKT_tl = x.DVKT_tl,
                        x.Thuoc_tl,
                        VTYT_tl = x.VTYT_tl,
                        CPVanchuyen = x.CPVanchuyen,
                        CPNgoaiBH = x.CPNgoaiBH,
                        ThanhTien = x.ThanhTien,
                        Tongchi = x.Tongchi,
                        Tongcong = x.Tongcong,
                        TienBH = x.TienBH,
                        TienBN = x.TienBN,

                    }).ToList();
                    #endregion
                    #region
                    var qVPND7 = (from vp in qVPND6
                                  join tu in dataContext.TamUngs.Where(o => (o.PhanLoai == 1 || o.PhanLoai == 2)) on vp.MaBNhan equals tu.MaBNhan
                                  join cb in dataContext.CanBoes.Where(o => maCanBos.Contains(o.MaCB)) on tu.MaCB equals cb.MaCB
                                  select new {vp.MaBNhan,vp.TenBNhan, cb.MaCB, cb.TenCB, vp.TongTien }).ToList();

                    var qVPND8 = (from sumTu in qVPND7.Where(p=>p.MaCB == maCanBos[0])
                                  group sumTu by new { sumTu.MaBNhan,sumTu.TenBNhan } into kq
                                  select new
                                  {
                                      kq.Key.MaBNhan,
                                      kq.Key.TenBNhan,
                                      VienPhiNhanDan = kq.Where(p=>p.MaCB == maCanBos[0]).Sum(o => o.TongTien),
                                  }
                                ).ToList();
                    var qVPND9 = (from sumTu in qVPND7.Where(p => p.MaCB == maCanBos[0])
                                  group sumTu by new { sumTu.MaBNhan, sumTu.TenBNhan } into kq
                                  select new
                                  {
                                      kq.Key.MaBNhan,
                                      kq.Key.TenBNhan,
                                  }
                                    ).ToList();
                    #endregion
                    var th2 = th1.Union(qVPND9);
                    if(th2.Count() >0)
                    {
                        foreach (var iten in th2)
                        {
                            VPktNhanvien vpct = new VPktNhanvien();
                            vpct.Theongay = "Từ ngày "+dtTuNgay.Text+" đến ngày "+dtDenNgay.Text;
                            vpct.NgayThang = ngay.Date;
                            vpct.HoTenCB = _tenCanbos[0];
                            vpct.Mabenhnhan = iten.MaBNhan.ToString();
                            vpct.Tenbnhan = iten.TenBNhan;
                            vpct.Nguoilapbieu = DungChung.Bien.NguoiLapBieu;
                            foreach (var tt in qThuThang4)
                            {
                                if (iten.MaBNhan == tt.MaBNhan)
                                {
                                    vpct.ThuThang = tt.ThuThang;
                                    vpct.Truc = tt.Truc;
                                    break;
                                }
                            }
                            foreach (var sk in qKsk4)
                            {
                                if (iten.MaBNhan == sk.MaBNhan)
                                {
                                    vpct.Ksk = sk.Ksk.Value;
                                    break;
                                }
                            }
                            foreach (var qntnt in qNTNT1)
                            {
                                if (iten.MaBNhan == qntnt.MaBNhan)
                                {
                                    vpct.NoiTru = qntnt.NoiTru;
                                    vpct.NgoaiTru = qntnt.NgoaiTru;
                                    break;
                                }
                            }
                            foreach (var vpnd in qVPND8)
                            {
                                if (iten.MaBNhan == vpnd.MaBNhan)
                                {
                                    vpct.VienPhiNhanDan = vpnd.VienPhiNhanDan;

                                }
                            }
                            vpct.TongThu = vpct.Ksk + vpct.ThuThang + vpct.NgoaiTru + vpct.NoiTru + vpct.VienPhiNhanDan + vpct.Truc;

                            listVPktNhanvien.Add(vpct);
                        }
                    }
                //else
                //    {
                //        MessageBox.Show("Không có dữ liệu !","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //        //VPktNhanvien vpkt = new VPktNhanvien();
                //        //vpkt.HoTenCB = _tenCanbos[0];
                //        //vpkt.ThuThang = 0;
                //        //vpkt.NoiTru = 0;
                //        //vpkt.VienPhiNhanDan = 0;
                //        //vpkt.Truc = 0;
                //        //vpkt.NgoaiTru = 0;
                //        //vpkt.TongThu = 0;
                //        //vpkt.NgayThang = ngay.Date;
                //        //listVPktNhanvien.Add(vpkt);
                //        //vpkt.Nguoilapbieu = DungChung.Bien.NguoiLapBieu;
                //    }
                    
            }
            if (listVPktNhanvien.Count > 0) 
            {
                DungChung.Ham.Print(DungChung.PrintConfig.Rp_DetailOfEachEmployee, listVPktNhanvien, new Dictionary<string, object>(), false);
            }
            else
                MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                #endregion
        }

    }
}
