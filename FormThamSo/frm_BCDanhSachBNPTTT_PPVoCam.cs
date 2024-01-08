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
    public partial class frm_BCDanhSachBNPTTT_PPVoCam : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCDanhSachBNPTTT_PPVoCam()
        {
            InitializeComponent();
        }
        private class KPhongc
        {
            private string TenKP;
            private int MaKP;
            private string PLoai;
            private bool Chon;

            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }

            public int makp
            { set { MaKP = value; } get { return MaKP; } }

            public string ploai
            { set { PLoai = value; } get { return PLoai; } }

            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        QLBV_Database.QLBVEntities _data;
        List<KPhongc> _Kphong = new List<KPhongc>();
        List<KPhong> _lkpall = new List<KPhong>();
        private void frm_BCDanhSachBNPTTT_PPVoCam_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            deTuNgay.Focus();
            deTuNgay.DateTime = System.DateTime.Now;
            deDenNgay.DateTime = System.DateTime.Now;
            radTrongDM.SelectedIndex = 2;

            List<DTBN> _lDTBN = _data.DTBNs.ToList();
            _lDTBN.Add(new DTBN { IDDTBN = 99, DTBN1 = "Tất cả" });
            cbo_DoiTuong.Properties.DataSource = _lDTBN.OrderBy(p => p.IDDTBN);

            _lkpall = _data.KPhongs.Where(p => p.Status == 1).ToList();
            _lkpall = _data.KPhongs.Where(p => p.Status == 1).ToList();
            var kphong = (from kp in _lkpall
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP, kp.PLoai }).ToList();
            if (kphong.Count > 0)
            {
                KPhongc themmoi1 = new KPhongc();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                themmoi1.ploai = "";
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhongc themmoi = new KPhongc();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.ploai = a.PLoai;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }
        public class BN
        {
            public string TenBNhan { get; set; }
            public int MaBNhan { get; set; }
            public string Nam { get; set; }
            public string Nu { get; set; }
            public string SThe { get; set; }
            public string TenDV { get; set; }
            public string MaQD { get; set; }
            public string LoiDan { get; set; }
            public string tenkp { get; set; }
            public double DonGia { get; set; }
            public string chandoan { get; set; }
            public string NgayVao { get; set; }
            public string NgayRa { get; set; }
            public int MaKP { get; set; }
            public DateTime ngay { get; set; }

        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime _tungay = DungChung.Ham.NgayTu(deTuNgay.DateTime);
            DateTime _denngay = DungChung.Ham.NgayDen(deDenNgay.DateTime);

            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int _idDTBN = 99;
            if (cbo_DoiTuong.EditValue != null && cbo_DoiTuong.EditValue.ToString() != "")
                _idDTBN = Convert.ToInt32(cbo_DoiTuong.EditValue);

            List<KPhongc> _lKhoaP = new List<KPhongc>();
            _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();

            int _noitru = cbo_NoiTru.SelectedIndex; int trongdm = radTrongDM.SelectedIndex;

            var _ldv = (from dv in _data.DichVus
                        join tn in _data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat) on dv.IdTieuNhom equals tn.IdTieuNhom
                        join n in _data.NhomDVs on tn.IDNhom equals n.IDNhom
                        select new { dv, tn, n }).ToList();
            List<BN> _lkp = new List<BN>();
            if (rgNgayBC.SelectedIndex == 0)
            {
                var _lcls = (from bn in _data.BenhNhans.Where(p => _noitru == 2 ? true : p.NoiTru == _noitru).Where(p => p.IDDTBN == _idDTBN || _idDTBN == 99)
                             join cls in _data.CLS.Where(p => p.Status == 1).Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay) on bn.MaBNhan equals cls.MaBNhan
                             join cd in _data.ChiDinhs.Where(p => trongdm == 2 ? true : p.TrongBH == trongdm) on cls.IdCLS equals cd.IdCLS
                             select new { bn.TenBNhan, bn.MaBNhan, bn.GTinh, bn.Tuoi, bn.SThe, cd.MaDV, cd.LoiDan, cd.DonGia, cls.MaKP, cls.NgayTH }).ToList();

                var q1 = (from cls in _lcls//.Where(p => p.LoiDan.ToLower().Contains("mê") || p.LoiDan.ToLower().Contains("tê"))
                          join dv in _ldv on cls.MaDV equals dv.dv.MaDV
                          join kp in _lKhoaP on cls.MaKP equals kp.makp
                          select new BN
                          {
                              MaBNhan = cls.MaBNhan,
                              TenBNhan = cls.MaBNhan + "-" + cls.TenBNhan,
                              Nam = cls.GTinh == 1 ? cls.Tuoi.ToString() : "",
                              Nu = cls.GTinh == 0 ? cls.Tuoi.ToString() : "",
                              SThe = cls.SThe,
                              TenDV = dv.dv.TenDV,
                              MaQD = dv.dv.MaQD,
                              LoiDan = cls.LoiDan,
                              tenkp = kp.tenkp,
                              DonGia = cls.DonGia,
                              MaKP = cls.MaKP ?? 0,
                              ngay = cls.NgayTH.Value
                          }).OrderBy(p => p.TenBNhan).ToList();

                var q2 = (from a in q1
                          join vv in _data.VaoViens on a.MaBNhan equals vv.MaBNhan into k1
                          from k in k1.DefaultIfEmpty()
                          join rv in _data.RaViens on a.MaBNhan equals rv.MaBNhan into k2
                          from h in k2.DefaultIfEmpty()
                          select new BN
                          {
                              MaBNhan = a.MaBNhan,
                              TenBNhan = a.TenBNhan,
                              Nam = a.Nam,
                              Nu = a.Nu,
                              SThe = a.SThe,
                              TenDV = a.TenDV,
                              MaQD = a.MaQD,
                              LoiDan = a.LoiDan,
                              tenkp = a.tenkp,
                              DonGia = a.DonGia,
                              MaKP = a.MaKP,
                              NgayRa = h != null ? (h.NgayRa.Value.Day + "/" + h.NgayRa.Value.Month) : "",
                              NgayVao = k != null ? (k.NgayVao.Value.Day + "/" + k.NgayVao.Value.Month) : "",
                              ngay = a.ngay
                          }).Distinct().OrderBy(p => p.ngay).ToList();

                var qcd1 = (from dtct in _data.CLS.Where(p => p.Status == 1).Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay)
                            join bnkb in _data.BNKBs on dtct.MaBNhan equals bnkb.MaBNhan
                            select new { bnkb.MaBNhan, bnkb.MaKP, ChanDoan = bnkb.ChanDoan == null ? "" : bnkb.ChanDoan, BenhKhac = bnkb.BenhKhac == null ? "" : bnkb.BenhKhac }).ToList();

                var qcd = (from dt in qcd1
                           group dt by new { dt.MaBNhan, dt.MaKP, dt.ChanDoan, dt.BenhKhac } into kq
                           select new { kq.Key.MaBNhan, kq.Key.MaKP, ChanDoan = kq.Key.ChanDoan + (DungChung.Bien.MaBV == "30009" ? (kq.Key.ChanDoan == "" ? kq.Key.BenhKhac : ("; " + kq.Key.BenhKhac)) : "") }).ToList();
                foreach (var a in qcd)
                {
                    foreach (var b in q2)
                    {
                        //b.TenBNhan = b.MaBNhan + "-" + b.TenBNhan;
                        if (a.MaBNhan == b.MaBNhan)
                        {
                            if (a.MaKP == b.MaKP)
                            {
                                b.chandoan = a.ChanDoan.ToString();
                            }
                        }
                    }
                }
                _lkp.AddRange(q2);
                frmIn frm = new frmIn();
                BaoCao.Rep_DSBNPTTT_PPVoCam rep = new BaoCao.Rep_DSBNPTTT_PPVoCam();
                rep.NgayThang.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString();
                rep.DataSource = _lkp;
                rep.BinDingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                var _lcls = (from bn in _data.BenhNhans.Where(p => _noitru == 2 ? true : p.NoiTru == _noitru).Where(p => p.IDDTBN == _idDTBN || _idDTBN == 99)
                             join vp in _data.RaViens.Where(p => p.NgayRa >= _tungay && p.NgayRa <= _denngay) on bn.MaBNhan equals vp.MaBNhan
                             join cls in _data.CLS.Where(p => p.Status == 1) on bn.MaBNhan equals cls.MaBNhan
                             join cd in _data.ChiDinhs.Where(p => trongdm == 2 ? true : p.TrongBH == trongdm) on cls.IdCLS equals cd.IdCLS
                             select new { bn.TenBNhan, bn.MaBNhan, bn.GTinh, bn.Tuoi, bn.SThe, cd.MaDV, cd.LoiDan, cd.DonGia, cls.MaKP, vp.NgayRa }).ToList();

                var q1 = (from cls in _lcls//.Where(p => p.LoiDan.ToLower().Contains("mê") || p.LoiDan.ToLower().Contains("tê"))
                          join dv in _ldv on cls.MaDV equals dv.dv.MaDV
                          join kp in _lKhoaP on cls.MaKP equals kp.makp
                          select new
                          {
                              cls.MaBNhan,
                              TenBNhan = cls.MaBNhan + "-" + cls.TenBNhan,
                              Nam = cls.GTinh == 1 ? cls.Tuoi.ToString() : "",
                              Nu = cls.GTinh == 0 ? cls.Tuoi.ToString() : "",
                              cls.SThe,
                              dv.dv.TenDV,
                              dv.dv.MaQD,
                              cls.LoiDan,
                              kp.tenkp,
                              cls.DonGia,
                              cls.MaKP,
                              cls.NgayRa
                          }).OrderBy(p => p.TenBNhan).ToList();
                var q2 = (from a in q1
                          join vv in _data.VaoViens on a.MaBNhan equals vv.MaBNhan into k1
                          from k in k1.DefaultIfEmpty()
                          //join rv in _data.RaViens on a.MaBNhan equals rv.MaBNhan into k2
                          //from h in k2.DefaultIfEmpty()
                          select new BN
                          {
                              MaBNhan = a.MaBNhan,
                              TenBNhan = a.TenBNhan,
                              Nam = a.Nam,
                              Nu = a.Nu,
                              SThe = a.SThe,
                              TenDV = a.TenDV,
                              MaQD = a.MaQD,
                              LoiDan = a.LoiDan,
                              tenkp = a.tenkp,
                              DonGia = a.DonGia,
                              MaKP = a.MaKP ?? 0,
                              NgayRa = a.NgayRa.Value.Day + "/" + a.NgayRa.Value.Month,
                              NgayVao = k != null ? (k.NgayVao.Value.Day + "/" + k.NgayVao.Value.Month) : "",
                              ngay = a.NgayRa.Value
                          }).Distinct().OrderBy(p => p.ngay).ToList();

                var qcd1 = (from dtct in _data.CLS.Where(p => p.Status == 1).Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay)
                            join bnkb in _data.BNKBs on dtct.MaBNhan equals bnkb.MaBNhan
                            select new { bnkb.MaBNhan, bnkb.MaKP, ChanDoan = bnkb.ChanDoan == null ? "" : bnkb.ChanDoan, BenhKhac = bnkb.BenhKhac == null ? "" : bnkb.BenhKhac }).ToList();

                var qcd = (from dt in qcd1
                           group dt by new { dt.MaBNhan, dt.MaKP, dt.ChanDoan, dt.BenhKhac } into kq
                           select new { kq.Key.MaBNhan, kq.Key.MaKP, ChanDoan = kq.Key.ChanDoan + (DungChung.Bien.MaBV == "30009" ? (kq.Key.ChanDoan == "" ? kq.Key.BenhKhac : ("; " + kq.Key.BenhKhac)) : "") }).ToList();

                foreach (var a in qcd)
                {
                    foreach (var b in q2)
                    {
                        //b.TenBNhan = b.MaBNhan + "-" + b.TenBNhan;
                        if (a.MaBNhan == b.MaBNhan)
                        {
                            if (a.MaKP == b.MaKP)
                            {
                                b.chandoan = a.ChanDoan.ToString();
                            }
                        }
                    }
                }
                _lkp.AddRange(q2);
                frmIn frm = new frmIn();
                BaoCao.Rep_DSBNPTTT_PPVoCam rep = new BaoCao.Rep_DSBNPTTT_PPVoCam();
                rep.NgayThang.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString();
                rep.DataSource = q2;
                rep.BinDingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
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
    }
}