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
    public partial class frm_TonghopDTTheoKhoa : DevExpress.XtraEditors.XtraForm
    {
        public frm_TonghopDTTheoKhoa()
        {
            InitializeComponent();
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
        private class BC
        {
            
            public int MaKP
            { get; set; }
            public int MaBN
            { get; set; }
            public string TenBN
            { get; set; }
            public int Gtinh
            { get; set; }
            public int Tuoi
            { get; set; }
            public string DiaChi
            { get; set; }
            public string Sothe
            { get; set; }
            public string ChuanDoan
            { get; set; }
            public string Ngayvao
            { get; set; }
            public string Ngayra
            { get; set; }
            public string Songay
            { get; set; }
            public int Nhom
            { get; set; }

        }
        List<BC> _BC = new List<BC>();
        List<KPhong> _Kphong = new List<KPhong>();
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_TonghopDTTheoKhoa_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            _Kphong.Clear();
            var kphong = (from kp in _Data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = false;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = false;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }

        private void sbtOK_Click(object sender, EventArgs e)
        {
            DateTime NT = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime ND = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            var q = (from kp in _Kphong.Where(p => p.chon == true)
                     join vv in _Data.VaoViens on kp.makp equals vv.MaKP
                     join bn in _Data.BenhNhans   on vv.MaBNhan equals bn.MaBNhan
                     where (vv.NgayVao >= NT && vv.NgayVao <= ND)
                     select new { vv.NgayVao,bn.TenBNhan, bn.GTinh, bn.NamSinh, bn.DChi, bn.SThe, bn.Tuoi, bn.MaBNhan, vv.MaKP, bn.DTuong }).OrderBy(p=>p.NgayVao).ToList();
            var qBnkb = (from bn in q
                         join bnkb in _Data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                         select bnkb).ToList();
            var qRV = (from bn in q
                       join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                       select rv).ToList();
            _BC.Clear();
            if (q.Count > 0)
            {
                int _Mabn = 0;
                int _Makp = 0;
                foreach (var a in q)
                {
                    BC themmoi = new BC();
                    themmoi.Gtinh = Convert.ToInt32(a.GTinh);
                    themmoi.Sothe = a.SThe;
                    themmoi.TenBN = a.TenBNhan;
                    themmoi.Tuoi = Convert.ToInt32(a.Tuoi);
                    themmoi.DiaChi = a.DChi;
                    themmoi.MaKP = a.MaKP == null? 0 : a.MaKP.Value;
                    themmoi.Ngayvao = a.NgayVao == null ? "" : a.NgayVao.Value.ToShortDateString();
                    _Mabn = a.MaBNhan;
                    themmoi.MaBN = _Mabn;
                    _Makp = a.MaKP == null ? 0 : a.MaKP.Value;

                    var b = (from kb in qBnkb.Where(p => p.MaBNhan == _Mabn).Where(p => p.MaKPdt == _Makp) select new { kb.ChanDoan, kb.NgayKham }).ToList();
                    if (b.Count > 0)
                    {
                        themmoi.ChuanDoan = b.First().ChanDoan;
                    }
                    else
                    {
                        var b2 = (from kb in qBnkb.Where(p => p.MaBNhan == _Mabn).Where(p => p.MaKP == _Makp) select new { kb.ChanDoan, kb.NgayKham }).ToList();
                        if (b2.Count > 0)
                        {
                            themmoi.ChuanDoan = b2.First().ChanDoan;
                        }
                    }
                    var v = (from rv in qRV.Where(p => p.MaBNhan == _Mabn) select new { rv.NgayRa, rv.SoNgaydt }).ToList();
                    if (v.Count > 0)
                    {
                        if (v.First().NgayRa != null)
                        {
                            themmoi.Ngayra = v.First().NgayRa.ToString().Substring(0, 10);
                        }
                        if (v.First().SoNgaydt != null)
                        {
                            themmoi.Songay = v.First().SoNgaydt.ToString();
                        }
                    }
                    if (Convert.ToInt32(a.Tuoi) <= 15)
                    { themmoi.Nhom = 1; }
                    if (Convert.ToInt32(a.Tuoi) > 15 && a.DTuong == "BHYT" && a.SThe.Contains("HN"))
                    {
                        themmoi.Nhom = 2;
                    }
                    if (Convert.ToInt32(a.Tuoi) > 15 && a.DTuong == "BHYT" && a.SThe.Substring(0, 2) != "HN")
                    { themmoi.Nhom = 3; }
                    if (Convert.ToInt32(a.Tuoi) > 15 && a.DTuong == "Dịch vụ")
                    {
                        themmoi.Nhom = 4;
                    }
                    _BC.Add(themmoi);
                }
            }
            BaoCao.RepTonghopDTTheoKhoa rep = new BaoCao.RepTonghopDTTheoKhoa();
            rep.DataSource = _BC.ToList();
            rep.BindingData();
            rep.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
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

        private void sbtHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdFont_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}