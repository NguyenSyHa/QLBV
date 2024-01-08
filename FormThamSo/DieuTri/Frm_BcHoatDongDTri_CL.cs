using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{

    public partial class Frm_BcHoatDongDTri_CL : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcHoatDongDTri_CL()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }

        private class RVCV
        {

            private int MaBNhan;

            public int MaBNhan1
            {
                get { return MaBNhan; }
                set { MaBNhan = value; }
            }
            private string TenBNhan;

            public string TenBNhan1
            {
                get { return TenBNhan; }
                set { TenBNhan = value; }
            }
            private int MaKP;

            public int MaKP1
            {
                get { return MaKP; }
                set { MaKP = value; }
            }
            private double SoNgaydt;

            public double SoNgaydt1
            {
                get { return SoNgaydt; }
                set { SoNgaydt = value; }
            }
            private DateTime? NgayRa;

            public DateTime? NgayRa1
            {
                get { return NgayRa; }
                set { NgayRa = value; }
            }
            private int dem;

            public int Dem
            {
                get { return dem; }
                set { dem = value; }
            }
        }
        private class KPhong
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

        List<KPhong> _Kphong = new List<KPhong>();

        private void Frm_BcHoatDongDtri_TH04_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            rdHTThanhToan.SelectedIndex = 2;
            //rdCKhoan.SelectedIndex = 2;
            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
            var dtuong = (from a in data.DTBNs select a).ToList();
            List<DTBN> _ldtbn = data.DTBNs.OrderBy(p => p.DTBN1).ToList();
            _ldtbn.Add(new DTBN { IDDTBN = 0, DTBN1 = "Tất cả", MoTa = "", Status = 1, HTTT = 0 });
            cboDTuong.Properties.DataSource = _ldtbn;
        }
        public class DTri
        {
            private int makp;

            public int MaKP
            {
                get { return makp; }
                set { makp = value; }
            }
            private string khoa;

            public string Khoa
            {
                get { return khoa; }
                set { khoa = value; }
            }
            private int c1;

            public int C1
            {
                get { return c1; }
                set { c1 = value; }
            }
            private int c2;

            public int C2
            {
                get { return c2; }
                set { c2 = value; }
            }
            private int c3;

            public int C3
            {
                get { return c3; }
                set { c3 = value; }
            }
            private int c4;

            public int C4
            {
                get { return c4; }
                set { c4 = value; }
            }
            private int c5;

            public int C5
            {
                get { return c5; }
                set { c5 = value; }
            }
            private double c6;

            public double C6
            {
                get { return c6; }
                set { c6 = value; }
            }
            private int c7;

            public int C7
            {
                get { return c7; }
                set { c7 = value; }
            }
            private int c8;

            public int C8
            {
                get { return c8; }
                set { c8 = value; }
            }
            private int c9;

            public int C9
            {
                get { return c9; }
                set { c9 = value; }
            }
            private int c10;

            public int C10
            {
                get { return c10; }
                set { c10 = value; }
            }
            private int c11;

            public int C11
            {
                get { return c11; }
                set { c11 = value; }
            }
            private int c12;

            public int C12
            {
                get { return c12; }
                set { c12 = value; }
            }
            private double c13;

            public double C13
            {
                get { return c13; }
                set { c13 = value; }
            }
            private int vvdk;

            public int VVDK
            {
                get { return vvdk; }
                set { vvdk = value; }
            }
            private int vv;

            public int VV
            {
                get { return vv; }
                set { vv = value; }
            }
            private int rvdk;

            public int RVDK
            {
                get { return rvdk; }
                set { rvdk = value; }
            }
            private int rv;

            public int RV
            {
                get { return rv; }
                set { rv = value; }
            }

        }

        public class buong
        {
            private int makp;

            public int Makp
            {
                get { return makp; }
                set { makp = value; }
            }

            private int so;

            public int So
            {
                get { return so; }
                set { so = value; }
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            var kp = data.KPhongs.ToList();
            List<DTri> _lDT = new List<DTri>();
            List<DTri> _lDTbc = new List<DTri>();
            List<KPhong> _lKhoaP = new List<KPhong>();
            List<buong> _buong = new List<buong>();
            int HTThanhToan = rdHTThanhToan.SelectedIndex;
            //int Chuyenkhoan = rdCKhoan.SelectedIndex;
            if (KTtaoBc())
            {
                string doituong = cboDTuong.Text;
                _lDT.Clear(); _lKhoaP.Clear();
                var bv = data.BenhViens.ToList();
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                //frmIn frm = new frmIn();
                BaoCao.Rep_BcHoatDongDTri_CL rep = new BaoCao.Rep_BcHoatDongDTri_CL();
                #region Hiển thị thời gian
                int nam = Convert.ToInt32(tungay.Year);
                int thang = Convert.ToInt32(tungay.Month);
                string _ntn = "";
                if (radIn.SelectedIndex == 0)
                { _ntn = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text; }
                if (radIn.SelectedIndex == 1)
                {
                    if (thang > 1 && thang <= 3) { _ntn = "Quý I năm " + nam; }
                    if (thang > 3 && thang <= 6) { _ntn = "Quý II năm " + nam; }
                    if (thang > 6 && thang <= 9) { _ntn = "Quý III năm " + nam; }
                    if (thang > 9 && thang <= 12) { _ntn = "Quý IV năm " + nam; }
                }
                if (radIn.SelectedIndex == 2)
                {
                    _ntn = ("(Báo cáo thống kê 06 tháng " + nam + ")").ToUpper();
                }
                if (radIn.SelectedIndex == 3)
                {
                    _ntn = ("(Báo cáo thống kê 09 tháng " + nam + ")").ToUpper();
                }
                if (radIn.SelectedIndex == 4)
                { _ntn = ("(Báo cáo năm " + nam + ")").ToUpper(); }
                rep.TuNgay.Value = _ntn;
                int ngay = (denngay - tungay).Days + 1;
                #endregion

                _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
                _lKhoaP.Add(new KPhong { makp = 0, tenkp = "" });
                if (_lKhoaP.Count > 0)
                {
                    foreach (var a in _lKhoaP)
                    {
                        DTri them = new DTri();
                        them.MaKP = a.makp;
                        them.Khoa = a.tenkp;
                        _lDT.Add(them);
                    }
                }
                List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
                List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da1 = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
                foreach (var item in _Kphong)
                {

                    _da1 = frm_NhapBuongGiuongKeKoach.getBuongGiuong(data, item.makp, nam.ToString());
                    foreach (var item1 in _da1)
                    {
                        _da.Add(item1);
                    }
                }

                DateTime tungay1 = tungay.AddMonths(-2);
                List<RVCV> _lbnbk1 = new List<RVCV>();
                List<RaVien> _lRaVien = new List<RaVien>();
                List<VaoVien> _lVaoVien = new List<VaoVien>();
                _lVaoVien = (from vv in data.VaoViens.Where(p => p.NgayVao >= tungay1 && p.NgayVao <= denngay) select vv).OrderBy(p => p.MaBNhan).ToList();
                _lRaVien = data.RaViens.ToList();
                var _lbnbk11 = (from a in data.BNKBs.Where(p => p.NgayKham <= denngay)
                                join c in data.BenhNhans.Where(p => p.NoiTru == 1).Where(p => (doituong == "Tất cả" || doituong == "") ? true : ( p.DTuong == doituong)) on a.MaBNhan equals c.MaBNhan //||(p.NoiTru == 0 && p.DTNT == true)
                                join ttbx in data.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on c.MaBNhan equals ttbx.MaBNhan
                                join d in data.RaViens on a.MaBNhan equals d.MaBNhan into k
                                from k1 in k.DefaultIfEmpty()
                                select new { a.MaBNhan, a.MaKP, a.NgayNghi, a.NgayKham, NgayRa = k1.NgayRa , a.PhuongAn}).ToList();

               


                foreach (var item in _lbnbk11.Where(p => p.NgayRa > tungay || p.NgayRa == null))
                {
                    RVCV them = new RVCV();
                    them.MaBNhan1 = item.MaBNhan ?? 0;
                    them.MaKP1 = item.MaKP ?? 0;
                    them.Dem = 0;
                    if (item.NgayNghi == null || item.NgayNghi > denngay)
                        them.SoNgaydt1 = (denngay.Date - item.NgayKham.Value.Date).Days + 0.5;
                    else if ((item.NgayNghi.Value.Date - item.NgayKham.Value.Date).Days == 0 && item.PhuongAn != 0 )
                    {
                        them.SoNgaydt1 = 0.5;
                        them.Dem = 1;
                    }
                    else
                        them.SoNgaydt1 = (item.NgayNghi.Value.Date - item.NgayKham.Value.Date).Days;
                    them.NgayRa1 = item.NgayRa;
                    _lbnbk1.Add(them);
                }
                _lbnbk1 = (from a in _lbnbk1 group a by new { a.MaBNhan1, a.MaKP1, a.NgayRa1 } into kq select new RVCV { MaBNhan1 = kq.Key.MaBNhan1, MaKP1 = kq.Key.MaKP1, SoNgaydt1 = kq.Sum(p => p.SoNgaydt1), NgayRa1 = kq.Key.NgayRa1, Dem = kq.Sum(p => p.Dem) }).ToList();
                _lbnbk1 = (from a in _lbnbk1
                           join b in _lVaoVien on a.MaBNhan1 equals b.MaBNhan
                           join c in _lRaVien.Where(p => p.NgayRa > tungay) on a.MaBNhan1 equals c.MaBNhan into k
                           from k1 in k.DefaultIfEmpty()
                           select new RVCV { MaBNhan1 = a.MaBNhan1, MaKP1 = a.MaKP1,
                                             SoNgaydt1 = ((a.MaKP1 == b.MaKP && a.Dem == 0) ?  0.5 : 0) +  ((k1 != null && k1.MaKP == a.MaKP1) ? 0.5: 0) + a.SoNgaydt1,
                               //SoNgaydt1 = a.SoNgaydt1,
                               NgayRa1 = a.NgayRa1 }).ToList();

                #region Số giường bệnh
                var sogb = (from k in _lKhoaP
                            join a in _da on k.makp equals a.makp

                            group new { a, k } by new { k.makp, sogiuong = a.giuongKH } into kq
                            select new { kq.Key.makp, sogiuong = Convert.ToInt32(kq.Key.sogiuong) }).ToList();


                #endregion
                #region BN vào viện/ ra viện đầu kỳ
                var bn1 = (from a in data.BenhNhans.Where(p => p.NoiTru == 1).Where(p => p.MaKCB == DungChung.Bien.MaBV).Where(p => doituong == "Tất cả" || doituong == "" || p.DTuong == doituong)
                           join ttbx in data.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on a.MaBNhan equals ttbx.MaBNhan
                           join b in data.BNKBs on a.MaBNhan equals b.MaBNhan
                           join c in data.VaoViens on a.MaBNhan equals c.MaBNhan
                           join d in data.RaViens on a.MaBNhan equals d.MaBNhan into kq
                           from kq1 in kq.DefaultIfEmpty()
                           select new
                           {
                               a.MaBNhan,
                               c.NgayVao,
                               a.Status,
                               NgayRa = kq1 == null ? null : kq1.NgayRa,
                               b.IDKB,
                               b.NgayKham
                           }).ToList();
                var bn2 = (from a in bn1.Where(p => p.NgayKham <= denngay).Where(p => p.NgayRa > denngay || p.NgayRa == null)
                           group a by new
                           {
                               a.MaBNhan,
                               a.NgayVao,
                               a.Status,
                               a.NgayRa,
                           } into kq
                           select new
                           {
                               kq.Key.MaBNhan,
                               kq.Key.NgayVao,
                               kq.Key.Status,
                               kq.Key.NgayRa,
                               IDKB = kq.Max(p => p.IDKB)
                           }).ToList();
                var query2 = (from b in bn2
                              join a in data.BNKBs on b.IDKB equals a.IDKB
                              join c in _lKhoaP on a.MaKP equals c.makp
                              group new { a, b, c } by new { c.makp, c.tenkp } into kq
                              select new
                              {
                                  kq.Key.makp,
                                  kq.Key.tenkp,
                                  BNCu = 0,
                                  BNHienCo = kq.Count()
                              }).ToList();
                var bn3 = (from a in bn1.Where(p => p.NgayKham < tungay).Where(p => p.NgayRa > tungay || p.NgayRa == null)
                           group a by new
                           {
                               a.MaBNhan,
                               a.NgayVao,
                               a.Status,
                               a.NgayRa,
                           } into kq
                           select new
                           {
                               kq.Key.MaBNhan,
                               kq.Key.NgayVao,
                               kq.Key.Status,
                               kq.Key.NgayRa,
                               IDKB = kq.Max(p => p.IDKB)
                           }).ToList();
                var query3 = (from b in bn3
                              join a in data.BNKBs on b.IDKB equals a.IDKB
                              join c in _lKhoaP on a.MaKP equals c.makp
                              group new { a, b, c } by new { c.makp, c.tenkp } into kq
                              select new
                              {
                                  kq.Key.makp,
                                  kq.Key.tenkp,
                                  BNCu = kq.Count(),
                                  BNHienCo = 0
                              }).ToList();
                var query4 = query3.Concat(query2);
                //var qbnton = (from k in _lKhoaP
                //              join vv in data.VaoViens on k.makp equals vv.MaKP
                //              join rv in data.RaViens on vv.MaBNhan equals rv.MaBNhan into q
                //              join bn in data.BenhNhans.Where(p => p.NoiTru == 1) on vv.MaBNhan equals bn.MaBNhan
                //              from q1 in q.DefaultIfEmpty()
                //              group new { vv, q1 } by new { vv.MaKP, NgayRa = q1 == null ? null : q1.NgayRa } into kq
                //              select new
                //              {
                //                  kq.Key.MaKP,
                //                  TSVVDK = kq.Where(p => p.vv.NgayVao < tungay && kq.Key.NgayRa == null).Select(p => p.vv.MaBNhan).Count() +
                //                           kq.Where(p => p.vv.NgayVao < tungay && kq.Key.NgayRa > tungay).Select(p => p.vv.MaBNhan).Count(),
                //                  TSVV = kq.Where(n => n.vv.NgayVao <= denngay && kq.Key.NgayRa == null).Select(n => n.vv.MaBNhan).Count() +
                //                         kq.Where(n => n.vv.NgayVao <= denngay && kq.Key.NgayRa > denngay).Select(n => n.vv.MaBNhan).Count(),
                //              }).ToList();
                ////var qvv = (from k in _lKhoaP
                ////           join vv in data.VaoViens.Where(p => p.NgayVao <= denngay) on k.makp equals vv.MaKP
                ////           group new { vv } by new { vv.MaKP } into kq
                ////           select new
                ////           {
                ////               kq.Key.MaKP,
                ////               TSVVDK = kq.Where(p => p.vv.NgayVao < tungay).Select(p => p.vv.MaBNhan).Count(),
                ////               TSVV = kq.Select(p => p.vv.MaBNhan).Count(),
                ////           }).ToList();
                if (query4.Count() > 0)
                {
                    foreach (var a in _lDT)
                    {
                        foreach (var b in query4)
                        {
                            //if (b.MaKP == a.MaKP)
                            //{
                            a.VVDK = query4.Where(p => p.makp == a.MaKP).Sum(p => p.BNCu);
                            a.VV = query4.Where(p => p.makp == a.MaKP).Sum(p => p.BNHienCo);
                            //}
                        }
                    }
                }
                ////var qrv = (from k in _lKhoaP
                ////           join rv in data.RaViens.Where(p => p.NgayRa <= denngay) on k.makp equals rv.MaKP
                ////           group new { rv } by new { rv.MaKP } into kq
                ////           select new
                ////           {
                ////               kq.Key.MaKP,
                ////               TSRVDK = kq.Where(p => p.rv.NgayRa < tungay).Select(p => p.rv.MaBNhan).Count(),
                ////               TSRV = kq.Select(p => p.rv.MaBNhan).Count(),
                ////           }).ToList();
                ////if (qvv.Count > 0)
                ////{
                ////    foreach (var a in _lDT)
                ////    {
                ////        foreach (var b in qrv)
                ////        {
                ////            if (b.MaKP == a.MaKP)
                ////            {
                ////                a.RVDK = b.TSRVDK;
                ////                a.RV = b.TSRV;
                ////            }
                ////        }
                ////    }
                ////}
                #endregion
                #region Thống kê BN ra viện
                string _tk = cboTKBN.EditValue.ToString();
                if (_tk.Equals("BN điều trị tại khoa đã ra viện (Tính theo ngày ra viện)"))
                {
                    var qdt1 = (from rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay)
                                join bn in data.BenhNhans.Where(p => p.NoiTru == 1).Where(p => (doituong == "Tất cả" || doituong == "") ? true : (p.DTuong == doituong)) on rv.MaBNhan equals bn.MaBNhan
                                join ttbx in data.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on bn.MaBNhan equals ttbx.MaBNhan
                                select new { rv.MaBNhan, rv.NgayVao, rv.NgayRa, rv.MaKP, bn.Tuoi, bn.CapCuu, bn.DTuong, rv.KetQua, rv.SoNgaydt }).ToList();

                    var qdttest = (from a in qdt1
                                   select new
                                   {
                                       a.MaBNhan,
                                       a.NgayVao,
                                       a.NgayRa,
                                       a.MaKP,
                                       a.Tuoi,
                                       a.CapCuu,
                                       a.DTuong,
                                       a.KetQua,
                                       SoNgaydttl = ckcSoNgaydt.Checked == true ? ((a.KetQua.ToLower().Contains("đỡ|giảm") || a.KetQua.ToLower().Contains("khỏi")) ? a.SoNgaydt + 1 : a.SoNgaydt) : a.SoNgaydt
                                   }).ToList();

                    var qdt = (from p in _lKhoaP
                               join rv in qdttest on p.makp equals rv.MaKP//.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay)
                               join a in sogb on p.makp equals a.makp
                               group new { p, rv, a } by new { p.makp, p.tenkp, a.sogiuong } into kq
                               select new
                               {
                                   Khoa = kq.Key.tenkp,
                                   MaKP = kq.Key.makp,
                                   C3 = kq.Select(p => p.rv.MaBNhan).Count(),
                                   C4 = kq.Where(p => p.rv.Tuoi < 15).Select(p => p.rv.MaBNhan).Count(),
                                   C5 = kq.Where(p => p.rv.CapCuu == 1).Select(p => p.rv.MaBNhan).Count(),
                                   C6 = DungChung.Bien.MaBV == "12122" ? 0 : kq.Sum(p => p.rv.SoNgaydttl) == null ? 0 : kq.Sum(p => p.rv.SoNgaydttl),
                                   C7 = kq.Where(p => p.rv.KetQua == "Tử vong").Select(p => p.rv.MaBNhan).Count(),
                                   C8 = kq.Where(p => p.rv.Tuoi < 15).Where(p => p.rv.KetQua == "Tử vong").Select(p => p.rv.MaBNhan).Count(),
                                   C10 = kq.Where(p => p.rv.DTuong == "BHYT").Select(p => p.rv.MaBNhan).Count(),
                                   C12 = DungChung.Bien.MaBV == "12122" ? 0 : (kq.Select(p => p.rv.MaBNhan).Count() > 0) ? ((kq.Sum(p => p.rv.SoNgaydttl) == null ? 0 : kq.Sum(p => p.rv.SoNgaydttl)) / kq.Select(p => p.rv.MaBNhan).Count()) : 0,
                                   C13 = (kq.Key.sogiuong > 0) ? ((kq.Sum(p => p.rv.SoNgaydttl) == null ? 0 : (double)(kq.Sum(p => p.rv.SoNgaydttl) * 100) / (kq.Key.sogiuong * ngay))) : 0.00

                               }).ToList();

                    var test = (from a in _lbnbk1.Where(p => p.NgayRa1 >= tungay && p.NgayRa1 <= denngay)
                                group new { a } by new { a.MaKP1 } into kq
                                select new
                                {
                                    MaKP = kq.Key.MaKP1,
                                    SoNgayDT = kq.Sum(p => p.a.SoNgaydt1)
                                }).ToList();
                    
                    if (qdt.Count > 0)
                    {
                        foreach (var a in _lDT)
                        {
                            foreach (var b in qdt)
                            {
                                if (b.MaKP == a.MaKP)
                                {
                                    // a.C2 = (Convert.ToInt32(a.VVDK) - Convert.ToInt32(a.RVDK)).ToString();
                                    a.C3 = b.C3;
                                    a.C4 = b.C4;
                                    a.C5 = b.C5;
                                    a.C6 = b.C6 ?? 0; // Convert.ToInt32(b.C6);
                                    a.C7 = b.C7;
                                    a.C8 = b.C8;
                                    a.C10 = b.C10;
                                    a.C12 = Convert.ToInt32(b.C12);
                                    if (csg.Checked == true)
                                        a.C13 = Math.Round(Convert.ToDouble(b.C13), 2);
                                    // a.C11 = (Convert.ToInt32(a.VV) - Convert.ToInt32(a.RV)).ToString();
                                }
                            }
                        }
                    }
                    //if (test.Count > 0)
                    //{
                    //    foreach (var a in _lDT)
                    //    {
                    //        foreach (var b in test)
                    //        {
                    //            if (b.MaKP == a.MaKP)
                    //            {
                    //                // a.C2 = (Convert.ToInt32(a.VVDK) - Convert.ToInt32(a.RVDK)).ToString();
                    //                a.C6 = b.SoNgayDT;
                    //                a.C12 = (a.C3 > 0) ? Convert.ToInt32(b.SoNgayDT) / a.C3 : 0;
                    //                // a.C11 = (Convert.ToInt32(a.VV) - Convert.ToInt32(a.RV)).ToString();
                    //            }

                    //        }
                    //    }
                    //}

                }
                #endregion
                #region Thống kê BN điều trị
                if (_tk.Equals("BN đang điều trị tại khoa (BN vào khoa - BN đã ra)"))
                {
                    var test1 = (from a in data.BNKBs.Where(p => p.NgayKham <= tungay && p.NgayKham >= tungay1)
                                 join b in data.RaViens on a.MaBNhan equals b.MaBNhan into k
                                 from kq in k.DefaultIfEmpty()
                                 join c in data.BenhNhans.Where(p => p.NoiTru == 1).Where(p => doituong == "Tất cả" || doituong == "" || p.DTuong == doituong) on a.MaBNhan equals c.MaBNhan //||(p.NoiTru == 0 && p.DTNT == true)
                                 join ttbx in data.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on c.MaBNhan equals ttbx.MaBNhan
                                 where (kq.NgayRa > tungay || kq.NgayRa == null)
                                 group new { a, kq } by a.MaBNhan into kq1
                                 select new { kq1.Key, IDKB = kq1.Max(p => p.a.IDKB) }).ToList();
                    var test22 = (from b in test1
                                  join a in data.BNKBs.Where(p => p.PhuongAn != 1 && p.PhuongAn != 0) on b.IDKB equals a.IDKB
                                  join c in data.RaViens on a.MaBNhan equals c.MaBNhan into k
                                  from kk in k.DefaultIfEmpty()
                                  select new
                                  {
                                      a.MaBNhan,
                                      NgayNghi = (a.NgayNghi == null || a.NgayNghi.Value.Date >= denngay) ? denngay : a.NgayNghi.Value.Date,
                                      NgayKham = tungay,
                                      SoNgayDT = ((((a.NgayNghi == null || a.NgayNghi.Value.Date >= denngay) ? denngay : (a.NgayNghi.Value.Date >= tungay ? a.NgayNghi.Value.Date : tungay)) - tungay).Days + 1) > 0 ? ((((a.NgayNghi == null || a.NgayNghi.Value.Date >= denngay) ? denngay : (a.NgayNghi.Value.Date >= tungay ? a.NgayNghi.Value.Date : tungay)) - tungay).Days + 1) : 0,
                                      a.MaKP,
                                      a.IDKB
                                  }).ToList();
                    var test2 = (from a in _Kphong
                                 join b in test22 on a.makp equals b.MaKP
                                 select new { b.MaKP, b.SoNgayDT }).ToList();
                    var test3 = (from a in _lKhoaP
                                 join b in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Where(p => p.PhuongAn != 1 && p.PhuongAn != 0) on a.makp equals b.MaKP
                                 join c in data.RaViens.Where(p => p.NgayRa > denngay || p.NgayRa == null) on b.MaBNhan equals c.MaBNhan into k
                                 from kq in k.DefaultIfEmpty()
                                 join d in data.BenhNhans.Where(p => p.NoiTru == 1) on b.MaBNhan equals d.MaBNhan //||(p.NoiTru == 0 && p.DTNT == true)
                                 join ttbx in data.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on d.MaBNhan equals ttbx.MaBNhan
                                 select new
                                 {
                                     b.MaKP,
                                     SoNgayDT = ((b.NgayNghi == null || b.NgayNghi.Value.Date >= denngay) ? ((denngay - b.NgayKham.Value.Date).Days + 1) : ((b.NgayNghi.Value.Date - b.NgayKham.Value.Date).Days + 1)) > 0 ? ((b.NgayNghi == null || b.NgayNghi.Value.Date >= denngay) ? ((denngay - b.NgayKham.Value.Date).Days + 1) : ((b.NgayNghi.Value.Date - b.NgayKham.Value.Date).Days + 1)) : 0
                                 }).ToList();
                    var test4 = test2.Concat(test3);
                    var test = (from a in test4
                                group new { a } by new { a.MaKP } into kq
                                select new
                                {
                                    MaKP = kq.Key.MaKP ?? 0,
                                    SoNgayDT = Convert.ToDouble(kq.Sum(p => p.a.SoNgayDT))
                                }).ToList();
                    if (DungChung.Bien.MaBV == "12122")
                    {
                            test = (from a in _lbnbk1
                                group new { a } by new { a.MaKP1 } into kq
                                select new
                                {
                                    MaKP = kq.Key.MaKP1,
                                    SoNgayDT = kq.Sum(p => p.a.SoNgaydt1)
                                }).ToList();
                    }
                    var idmin = (from p in _lKhoaP
                                 join kb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on p.makp equals kb.MaKP
                                 group kb by kb.MaBNhan into kq
                                 select new { kq.Key, IDKB = DungChung.Bien.MaBV == "12122" ? kq.Max(p => p.IDKB) : kq.Min(p => p.IDKB) }).ToList();

                    var qdt1 = (from id in idmin
                                join kb in data.BNKBs on id.IDKB equals kb.IDKB
                                join bn in data.BenhNhans.Where(p => p.NoiTru == 1).Where(p => doituong == "Tất cả" || doituong == "" || p.DTuong == doituong) on kb.MaBNhan equals bn.MaBNhan
                                join ttbx in data.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on bn.MaBNhan equals ttbx.MaBNhan
                                select new { kb.MaBNhan, kb.MaKP, bn.Tuoi, bn.CapCuu, bn.DTuong }).ToList();

                    var qdt = (from p in _lKhoaP
                               join rv in qdt1 on p.makp equals rv.MaKP
                               group new { p, rv } by new { p.makp, p.tenkp } into kq
                               select new
                               {
                                   Khoa = kq.Key.tenkp,
                                   MaKP = kq.Key.makp,
                                   C3 = kq.Select(p => p.rv.MaBNhan).Count(),
                                   C4 = kq.Where(p => p.rv.Tuoi < 15).Select(p => p.rv.MaBNhan).Count(),
                                   C5 = kq.Where(p => p.rv.CapCuu == 1).Select(p => p.rv.MaBNhan).Count(),
                                   C10 = kq.Where(p => p.rv.DTuong == "BHYT").Select(p => p.rv.MaBNhan).Count()

                               }).ToList();
                    if (test.Count > 0)
                    {
                        foreach (var a in _lDT)
                        {
                            foreach (var b in test)
                            {
                                if (b.MaKP == a.MaKP)
                                {
                                    // a.C2 = (Convert.ToInt32(a.VVDK) - Convert.ToInt32(a.RVDK)).ToString();
                                    a.C6 = b.SoNgayDT;
                                    a.C12 = (a.C3 > 0) ? Convert.ToInt32(b.SoNgayDT) / a.C3 : 0;
                                    // a.C11 = (Convert.ToInt32(a.VV) - Convert.ToInt32(a.RV)).ToString();
                                }

                            }
                        }
                    }
                    if (qdt.Count > 0)
                    {
                        foreach (var a in _lDT)
                        {
                            foreach (var b in qdt)
                            {
                                if (b.MaKP == a.MaKP)
                                {
                                    // a.C2 = (Convert.ToInt32(a.VVDK) - Convert.ToInt32(a.RVDK)).ToString();
                                    a.C3 = b.C3;
                                    a.C4 = b.C4;
                                    a.C5 = b.C5;
                                    a.C10 = b.C10;
                                    // a.C11 = (Convert.ToInt32(a.VV) - Convert.ToInt32(a.RV)).ToString();
                                }

                            }
                        }
                    }
                    var qdt2 = (from rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay)
                                join bn in data.BenhNhans.Where(p => doituong == "Tất cả" || doituong == "" || p.DTuong == doituong) on rv.MaBNhan equals bn.MaBNhan
                                join ttbx in data.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on bn.MaBNhan equals ttbx.MaBNhan
                                select new { rv.MaBNhan, rv.MaKP, bn.Tuoi, bn.CapCuu, bn.DTuong, rv.KetQua, rv.NgayRa, rv.SoNgaydt }).ToList();

                    var qdtt = (from p in _lKhoaP
                                join rv in qdt2.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on p.makp equals rv.MaKP
                                join a in sogb on p.makp equals a.makp
                                group new { p, rv, a } by new { p.makp, p.tenkp, a.sogiuong } into kq
                                select new
                                {
                                    Khoa = kq.Key.tenkp,
                                    MaKP = kq.Key.makp,
                                    //C6 = kq.Sum(p => p.rv.SoNgaydt) == null ? 0 : kq.Sum(p => p.rv.SoNgaydt),
                                    C7 = kq.Where(p => p.rv.KetQua == "Tử vong").Select(p => p.rv.MaBNhan).Count(),
                                    C8 = kq.Where(p => p.rv.Tuoi < 15).Where(p => p.rv.KetQua == "Tử vong").Select(p => p.rv.MaBNhan).Count(),
                                    //C12 = (kq.Select(p => p.rv.MaBNhan).Count() > 0) ? (1 / kq.Select(p => p.rv.MaBNhan).Count()) : 0,
                                    C13 = (kq.Key.sogiuong > 0) ? ((kq.Sum(p => p.rv.SoNgaydt) == null ? 0 : (double)(100) / (kq.Key.sogiuong * ngay))) : 0
                                }).ToList();
                    if (qdt.Count > 0)
                    {
                        foreach (var a in _lDT)
                        {
                            foreach (var b in qdtt)
                            {
                                if (b.MaKP == a.MaKP)
                                {
                                    //  a.C2 = (Convert.ToInt32(a.VVDK) - Convert.ToInt32(a.RVDK)).ToString();
                                    //a.C6 = Convert.ToInt32(b.C6);
                                    a.C7 = b.C7;
                                    a.C8 = b.C8;
                                    a.C12 = (a.C3 > 0) ? Convert.ToInt32(a.C6) / a.C3 : 0;
                                    if (csg.Checked == true)
                                        a.C13 = Math.Round(Convert.ToDouble(b.C13 * a.C6), 2);
                                    //   a.C11 = (Convert.ToInt32(a.VV) - Convert.ToInt32(a.RV)).ToString();
                                }

                            }
                        }
                    }
                }

                #endregion
                if (sogb.Count() > 0)
                {
                    foreach (var a in _lDT)
                    {
                        foreach (var b in sogb)
                        {
                            if (b.makp == a.MaKP)
                            {
                                a.C1 = Convert.ToInt32(b.sogiuong);
                            }
                        }
                    }
                }

                var DT = _lDT.Select(p => new
                {
                    p.MaKP,
                    p.Khoa,
                    p.C1,
                    C2 = Convert.ToInt32(p.VVDK),
                    p.C3,
                    p.C4,
                    p.C5,
                    p.C6,
                    p.C7,
                    p.C8,
                    p.C10,
                    C11 = Convert.ToInt32(p.VV),
                    p.C12,
                    p.C13
                }).ToList();
                DT = DT.Where(p => p.MaKP != 0).Where(p => p.C1 > 0 || p.C2 > 0 || p.C3 > 0 || p.C4 > 0 || p.C5 > 0 || p.C6 > 0 || p.C7 > 0 || p.C8 > 0 || p.C10 > 0 || p.C11 > 0 || p.C12 > 0 || p.C13 > 0).OrderBy(p => p.Khoa).ToList();

                rep.DataSource = DT;
                #region xuat Excel

                string[] _arr = new string[] { "0", "@", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                string[] _tieude = { "STT", "Khoa", "Số giường bệnh", "Số người bệnh đầu kỳ", "TS người bệnh điều trị nội trú", "TS người bệnh ĐTNT là TE <15 tuổi", "TS người bệnh ĐTNT cấp cứu", "Số ngày ĐTNT", "TS người bệnh tử vong", "Số người bệnh tử vong là TE<15 tuổi", "Số người bệnh tử vong trước 24 giờ", "Người bệnh có thẻ", "Người bệnh còn lại cuối kỳ", "Số ngày điều trị trung bình Nội trú", "Công suất sử dụng giường bệnh KH" };
                int[] _arrWidth = new int[] { };// { 5, 15, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

                DungChung.Bien.MangHaiChieu = new Object[DT.Count + 18, 15];
                DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQCQ.ToUpper();
                DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.TenCQ.ToUpper();
                DungChung.Bien.MangHaiChieu[2, 2] = ("BÁO CÁO HOẠT ĐỘNG  ĐIỀU TRỊ NỘI TRÚ").ToUpper();
                DungChung.Bien.MangHaiChieu[3, 2] = _ntn;

                DungChung.Bien.MangHaiChieu[DT.Count() + 5, 7] = "Ngày ...... tháng ..... năm .....";
                DungChung.Bien.MangHaiChieu[DT.Count() + 6, 1] = ("Người lập biểu").ToUpper();
                DungChung.Bien.MangHaiChieu[DT.Count() + 10, 1] = DungChung.Bien.NguoiLapBieu;
                DungChung.Bien.MangHaiChieu[DT.Count() + 6, 4] = ("TRƯỞNG PHÒNG KHTH").ToUpper();
                DungChung.Bien.MangHaiChieu[DT.Count() + 10, 4] = "";
                DungChung.Bien.MangHaiChieu[DT.Count() + 6, 7] = ("Giám đốc").ToUpper();
                DungChung.Bien.MangHaiChieu[DT.Count() + 10, 7] = DungChung.Bien.GiamDoc;
                for (int i = 0; i < _tieude.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[4, i] = _tieude[i];
                }
                int num = 5;
                foreach (var r in DT)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num - 4;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.Khoa;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.C1;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.C2;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.C3;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.C4;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.C5;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.C6;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.C7;
                    DungChung.Bien.MangHaiChieu[num, 9] = r.C8;
                    DungChung.Bien.MangHaiChieu[num, 10] = "";
                    DungChung.Bien.MangHaiChieu[num, 11] = r.C10;
                    DungChung.Bien.MangHaiChieu[num, 12] = r.C11;
                    DungChung.Bien.MangHaiChieu[num, 13] = r.C12;
                    num++;

                }

                #endregion
                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo hoạt động điều trị", "C:\\BcHDDT.xls", true, this.Name);
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void rdHTThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rdHTThanhToan.SelectedIndex == 1)
            //{
            //    rdCKhoan.Enabled = true;
            //}
            //else
            //    rdCKhoan.Enabled = false;
        }
    }
}
