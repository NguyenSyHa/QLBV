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

    public partial class Frm_BcHoatDongKKB_TH01 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcHoatDongKKB_TH01()
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

        private void Frm_BcHoatDongKKB_HL01_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;

            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Phòng khám")
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
        }
        public class KB
        {
            private string chuyenkhoa;

            public string ChuyenKhoa
            {
                get { return chuyenkhoa; }
                set { chuyenkhoa = value; }
            }
            private string ts;

            public string TS
            {
                get { return ts; }
                set { ts = value; }
            }
            private string nu;

            public string Nu
            {
                get { return nu; }
                set { nu = value; }
            }
            private string bhyt;

            public string BHYT
            {
                get { return bhyt; }
                set { bhyt = value; }
            }
            private string vp;

            public string VP
            {
                get { return vp; }
                set { vp = value; }
            }
            private string ktd;

            public string KTD
            {
                get { return ktd; }
                set { ktd = value; }
            }
            private string te15;

            public string Te15
            {
                get { return te15; }
                set { te15 = value; }
            }
            private string te6;

            public string Te6
            {
                get { return te6; }
                set { te6 = value; }
            }
            private string te4;

            public string Te4
            {
                get { return te4; }
                set { te4 = value; }
            }
            private string tc60;

            public string TC60
            {
                get { return tc60; }
                set { tc60 = value; }
            }
            private string bhytcv;

            public string BHYTCV
            {
                get { return bhytcv; }
                set { bhytcv = value; }
            }
            private string vpcv;

            public string VPCV
            {
                get { return vpcv; }
                set { vpcv = value; }
            }
            private string cc;

            public string CC
            {
                get { return cc; }
                set { cc = value; }
            }
            private string vv;

            public string VV
            {
                get { return vv; }
                set { vv = value; }
            }
            private string nbdtnt;

            public string NBDTNT
            {
                get { return nbdtnt; }
                set { nbdtnt = value; }
            }
            private string sndtnt;

            public string SNDTNT
            {
                get { return sndtnt; }
                set { sndtnt = value; }
            }

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            var kp = data.KPhongs.ToList();
            List<KB> _lKB = new List<KB>();

            if (KTtaoBc())
            {
                var bv = data.BenhViens.ToList();
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                DungChung.Bien.c_chuyenkhoa.f_ChuyenKhoa();
                frmIn frm = new frmIn();
                BaoCao.Rep_BcHoatDongKKB_TH01 rep = new BaoCao.Rep_BcHoatDongKKB_TH01();
                rep.TG.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;
                List<KPhong> _lKhoaP = new List<KPhong>();

                _lKhoaP = _Kphong.Where(p => p.makp > 0).Where(p => p.chon == true).ToList();

                if (radioGroup1.SelectedIndex == 0)//Lươt KB || Ko thống kê BN chuyển PK
                {
                    _lKB.Clear();
                    var id = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay)
                              join k in data.KPhongs.Where(p => p.PLoai == "Phòng khám") on kb.MaKP equals k.MaKP
                              group kb by kb.MaBNhan into kq
                              select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();
                    var qkb2 = (from k in id
                                join kb in data.BNKBs on k.IDKB equals kb.IDKB
                                join bn in data.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                                group new { kb, bn } by new { kb.MaBNhan, bn.NoiTru, bn.DTNT, kb.MaKP, kb.PhuongAn, kb.MaCK, bn.Tuoi, bn.DTuong, bn.CapCuu, bn.GTinh } into kq
                                select new { kq.Key.DTNT, kq.Key.GTinh, kq.Key.MaBNhan, kq.Key.NoiTru, kq.Key.PhuongAn, kq.Key.MaKP, kq.Key.MaCK, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.CapCuu, IDKB = kq.Max(p => p.kb.IDKB) }).ToList();

                    var qkb = (from ma in _lKhoaP
                               join p in qkb2 on ma.makp equals p.MaKP
                               join ck in DungChung.Bien._lChuyenKhoa on p.MaCK equals ck.MaCK
                               group new { p } by new { ChuyenKhoa = ck.ChuyenKhoa } into kq
                               select new
                               {
                                   ChuyenKhoa = kq.Key.ChuyenKhoa,
                                   TS = kq.Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Select(p => p.p.MaBNhan).Count().ToString(),
                                   Nu = kq.Where(p => p.p.GTinh == 0).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.GTinh == 0).Select(p => p.p.MaBNhan).Count().ToString(),
                                   BHYT = kq.Where(p => p.p.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count().ToString(),
                                   VP = kq.Where(p => p.p.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count().ToString(),
                                   TE15 = kq.Where(p => p.p.Tuoi < 15).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.Tuoi < 15).Select(p => p.p.MaBNhan).Count().ToString(),
                                   TE6 = kq.Where(p => p.p.Tuoi < 6).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.Tuoi < 6).Select(p => p.p.MaBNhan).Count().ToString(),
                                   TE4 = kq.Where(p => p.p.Tuoi >= 0 && p.p.Tuoi <= 4).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.Tuoi >= 0 && p.p.Tuoi <= 4).Select(p => p.p.MaBNhan).Count().ToString(),
                                   CT60 = kq.Where(p => p.p.Tuoi > 60).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.Tuoi > 60).Select(p => p.p.MaBNhan).Count().ToString(),
                                   BHYTCV = kq.Where(p => p.p.PhuongAn == 2 && p.p.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.PhuongAn == 2 && p.p.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count().ToString(),
                                   VPCV = kq.Where(p => p.p.PhuongAn == 2 && p.p.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.PhuongAn == 2 && p.p.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count().ToString(),
                                   CC = kq.Where(p => p.p.CapCuu == 1).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.CapCuu == 1).Select(p => p.p.MaBNhan).Count().ToString(),
                                   VV = kq.Where(p => p.p.PhuongAn == 1).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.PhuongAn == 1).Select(p => p.p.MaBNhan).Count().ToString(),
                                   DTNT = kq.Where(p => p.p.DTNT == true).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.DTNT == true).Select(p => p.p.MaBNhan).Count().ToString(),

                               }).ToList();

                    if (qkb.Count > 0)
                    {
                        foreach (var a in qkb)
                        {
                            KB them = new KB();
                            them.ChuyenKhoa = a.ChuyenKhoa;
                            them.TS = a.TS;
                            them.Nu = a.Nu;
                            them.BHYT = a.BHYT;
                            them.VP = a.VP;
                            them.KTD = "";
                            them.Te15 = a.TE15;
                            them.Te6 = a.TE6;
                            them.Te4 = a.TE4;
                            them.TC60 = a.CT60;
                            them.BHYTCV = a.BHYTCV;
                            them.VPCV = a.VPCV;
                            them.CC = a.CC;
                            them.VV = a.VV;
                            them.NBDTNT = a.DTNT;
                            _lKB.Add(them);
                        }

                    }
                    var qdtnt = (from ma in _lKhoaP
                                 join p in qkb2.Where(p => p.DTNT == true) on ma.makp equals p.MaKP
                                 join ck in DungChung.Bien._lChuyenKhoa on p.MaCK equals ck.MaCK
                                 join rv in data.RaViens on p.MaBNhan equals rv.MaBNhan
                                 group new { p, rv } by new { ck.ChuyenKhoa } into kq
                                 select new
                                 {
                                     ChuyenKhoa = kq.Key.ChuyenKhoa,
                                     SSNDTNT = kq.Sum(p => p.rv.SoNgaydt),
                                 }).ToList();
                    if (qdtnt.Count > 0)
                    {
                        foreach (var a in _lKB)
                        {
                            foreach (var b in qdtnt)
                            {
                                if (a.ChuyenKhoa == b.ChuyenKhoa)
                                {
                                    a.SNDTNT = b.SSNDTNT.ToString();
                                }
                            }
                        }
                    }

                }
                //////////////////////////////
                if (radioGroup1.SelectedIndex == 1)//TK cả BN chuyển PK
                {
                    _lKB.Clear();
                    var qbn2 = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay)
                                join bn in data.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                                group new { kb, bn } by new { kb.MaBNhan, kb.MaKP, kb.PhuongAn, kb.MaCK, bn.Tuoi, bn.DTuong, bn.CapCuu, bn.GTinh, bn.NoiTru, bn.DTNT } into kq
                                select new { kq.Key.DTNT, kq.Key.GTinh, kq.Key.MaBNhan, kq.Key.NoiTru, kq.Key.PhuongAn, kq.Key.MaKP, kq.Key.MaCK, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.CapCuu, IDKB = kq.Max(p => p.kb.IDKB) }).ToList();

                    var qbn = (from ma in _lKhoaP
                               join p in qbn2 on ma.makp equals p.MaKP
                               join ck in DungChung.Bien._lChuyenKhoa on p.MaCK equals ck.MaCK
                               group new { p } by new { ck.ChuyenKhoa } into kq
                               select new
                               {
                                   ChuyenKhoa = kq.Key.ChuyenKhoa,
                                   TS = kq.Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Select(p => p.p.MaBNhan).Count().ToString(),
                                   Nu = kq.Where(p => p.p.GTinh == 0).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.GTinh == 0).Select(p => p.p.MaBNhan).Count().ToString(),
                                   BHYT = kq.Where(p => p.p.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count().ToString(),
                                   VP = kq.Where(p => p.p.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count().ToString(),
                                   TE15 = kq.Where(p => p.p.Tuoi < 15).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.Tuoi < 15).Select(p => p.p.MaBNhan).Count().ToString(),
                                   TE6 = kq.Where(p => p.p.Tuoi < 6).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.Tuoi < 6).Select(p => p.p.MaBNhan).Count().ToString(),
                                   TE4 = kq.Where(p => p.p.Tuoi >= 0 && p.p.Tuoi <= 4).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.Tuoi >= 0 && p.p.Tuoi <= 4).Select(p => p.p.MaBNhan).Count().ToString(),
                                   TC60 = kq.Where(p => p.p.Tuoi > 60).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.Tuoi > 60).Select(p => p.p.MaBNhan).Count().ToString(),
                                   BHYTCV = kq.Where(p => p.p.PhuongAn == 2 && p.p.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.PhuongAn == 2 && p.p.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count().ToString(),
                                   VPCV = kq.Where(p => p.p.PhuongAn == 2 && p.p.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.PhuongAn == 2 && p.p.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count().ToString(),
                                   CC = kq.Where(p => p.p.CapCuu == 1).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.CapCuu == 1).Select(p => p.p.MaBNhan).Count().ToString(),
                                   VV = kq.Where(p => p.p.PhuongAn == 1).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.PhuongAn == 1).Select(p => p.p.MaBNhan).Count().ToString(),
                                   DTNT = kq.Where(p => p.p.DTNT == true).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.DTNT == true).Select(p => p.p.MaBNhan).Count().ToString(),

                               }).ToList();

                    if (qbn.Count > 0)
                    {
                        foreach (var a in qbn)
                        {
                            KB them = new KB();
                            them.ChuyenKhoa = a.ChuyenKhoa;
                            them.TS = a.TS;
                            them.Nu = a.Nu;
                            them.BHYT = a.BHYT;
                            them.VP = a.VP;
                            them.KTD = "";
                            them.Te15 = a.TE15;
                            them.Te6 = a.TE6;
                            them.Te4 = a.TE4;
                            them.TC60 = a.TC60;
                            them.BHYTCV = a.BHYTCV;
                            them.VPCV = a.VPCV;
                            them.CC = a.CC;
                            them.VV = a.VV;
                            them.NBDTNT = a.DTNT;
                            _lKB.Add(them);
                        }

                    }
                    var qdtnt = (from ma in _lKhoaP
                                 join p in qbn2.Where(p => p.DTNT == true) on ma.makp equals p.MaKP
                                 join ck in DungChung.Bien._lChuyenKhoa on p.MaCK equals ck.MaCK
                                 join rv in data.RaViens on p.MaBNhan equals rv.MaBNhan
                                 group new { p, rv } by new { ck.ChuyenKhoa } into kq
                                 select new
                                 {
                                     ChuyenKhoa = kq.Key.ChuyenKhoa,
                                     SSNDTNT = kq.Sum(p => p.rv.SoNgaydt),
                                 }).ToList();
                    if (qdtnt.Count > 0)
                    {
                        foreach (var a in _lKB)
                        {
                            foreach (var b in qdtnt)
                            {
                                if (a.ChuyenKhoa == b.ChuyenKhoa)
                                {
                                    a.SNDTNT = b.SSNDTNT.ToString();
                                }
                            }
                        }
                    }
                }

                rep.DataSource = _lKB.ToList();

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
    }
}