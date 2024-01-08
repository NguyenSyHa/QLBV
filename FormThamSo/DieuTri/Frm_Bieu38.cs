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
    public partial class Frm_Bieu38 : DevExpress.XtraEditors.XtraForm
    {
        int _MaBenhNhan;
        public Frm_Bieu38()
        {
            InitializeComponent();
        }
        public Frm_Bieu38(int _MaBN)
        {
            InitializeComponent();
            _MaBenhNhan = _MaBN;
        }

        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
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

        private void Frm_Bieu38_Load(object sender, EventArgs e)
        {
            var kphong = (from dt in _Data.DThuocs.Where(p => p.MaBNhan == _MaBenhNhan)
                          join dtct in _Data.DThuoccts on dt.IDDon equals dtct.IDDon
                          join kp in _Data.KPhongs on dtct.MaKP equals kp.MaKP
                          //where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          group new { kp } by new { kp.MaKP, kp.TenKP } into kq
                          select new { TenKP = kq.Key.TenKP, MaKP = kq.Key.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = -1;
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
                grcKP.DataSource = _Kphong.ToList();
            }
        }

        private void grvKP_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKP.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKP.GetFocusedRowCellValue("tenkp").ToString();
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
                        grcKP.DataSource = "";
                        grcKP.DataSource = _Kphong.ToList();
                    }
                }
            }
        }

        private void sbtTaoBC_Click(object sender, EventArgs e)
        {
            int _makp = 0;
            string _tenbn = "nnghjfghj";
            string _DTuong = "";
            string _muc = "";
            int d1 = -1, d2 = -1;
            if (radDM.SelectedIndex == 0)
            {
                d1 = 1;
            }
            if (radDM.SelectedIndex == 1)
            { d1 = 0; }
            if (radDM.SelectedIndex == 2)
            { d1 = 0; d2 = 1; }
            var kp = (from bns in _Data.BNKBs.Where(p => p.MaBNhan == (_MaBenhNhan)) join kps in _Data.KPhongs on bns.MaKP equals kps.MaKP select new { kps.TenKP, kps.MaKP, bns.IDKB }).OrderByDescending(p => p.IDKB).ToList();
            var kq1 = (from k in kp
                       join x in _Kphong.Where(p => p.chon == true) on k.MaKP equals x.makp
                       select new { k.TenKP, k.IDKB }).OrderByDescending(p => p.IDKB).ToList();
            frmIn frm = new frmIn();
            BaoCao.rep_PhieuTTRV38 rep = new BaoCao.rep_PhieuTTRV38();
            _makp = DungChung.Bien.MaKP;
            if (kq1.Count > 0 && kq1.First().TenKP != null && kq1.First().TenKP.ToString() != "")
            {
                rep.KhoaPhong.Value = kq1.First().TenKP;
            }
            if (_Kphong.Where(p=>p.chon == true).Count() > 0)
            {
                rep.celKhoa.Text = string.Join(", ", _Kphong.Where(p => p.chon == true && p.makp != -1).Select(p=>p.tenkp));
            }
         
            rep.MaBNhan.Value = _MaBenhNhan;


            var par = (from bn in _Data.BenhNhans
                       join kb in _Data.BNKBs on bn.MaBNhan equals kb.MaBNhan
                       where bn.MaBNhan == (_MaBenhNhan)
                       select new { bn.MaBNhan, bn.DChi, bn.NamSinh, bn.NgaySinh, bn.ThangSinh, bn.GTinh, bn.CapCuu, bn.DTuong, bn.HanBHTu, bn.HanBHDen, bn.SThe, bn.MaCS, bn.NNhap, bn.Tuyen, bn.MaBV, kb.NgayKham, kb.MaICD, ChanDoan = kb.ChanDoan + "-" + kb.BenhKhac, kb.IDKB, bn.TenBNhan, kb.Buong, kb.Giuong }).OrderByDescending(p => p.IDKB).ToList();
            if (par.Count > 0)
            {
                rep.tenBN.Value = par.First().TenBNhan;
                rep.Buong.Value = par.First().Buong;
                rep.Giuong.Value = par.First().Giuong;
                rep.Tuoi.Value = par.First().NamSinh;
                rep.DiaChi.Value = par.First().DChi;
                int gioiTinh = int.Parse(par.First().GTinh.ToString());
                if (gioiTinh == 1)
                {
                    rep.Nu.Value = "/";
                }
                else if (gioiTinh == 0)
                {
                    rep.Nam.Value = "/";
                }
                _DTuong = (par.First().DTuong).ToString();
                var ngayv = (from v in _Data.VaoViens.Where(p => p.MaBNhan == _MaBenhNhan) select new { v.NgayVao }).ToList();
                if (ngayv.Count > 0)
                { rep.NgayVao.Value = ngayv.First().NgayVao; }
                //rep.NgayVao.Value = par.First().NgayKham;
                var tt = _Data.RaViens.Where(p => p.MaBNhan == (_MaBenhNhan)).Select(p => p.NgayRa).ToList();
                if (tt.Count > 0)
                    rep.NgayRa.Value = tt.First().Value;
                if (_DTuong.Contains("BHYT"))
                {
                    rep.SoThe.Value = par.First().SThe;
                    rep.coBH.Value = "X";
                    if (par.First().SThe != null && par.First().SThe.ToString() != "" && par.First().SThe.Length > 10)
                        _muc = par.First().SThe.Substring(2, 1);

                    rep.HanBHTu.Value = par.First().HanBHTu;
                    rep.HanBHDen.Value = par.First().HanBHDen;
                    string macs = "";
                    if (par.First().MaCS != null)
                    {
                        macs = par.First().MaCS;
                        rep.MaCS.Value = macs.Substring(0, 2) + "-" + macs.Substring(2, 3);
                    }
                    var csdkbd = _Data.BenhViens.Where(p => p.MaBV == macs).ToList();
                    if (csdkbd.Count > 0)
                    {
                        rep.CSDKKCB.Value = csdkbd.First().TenBV;
                    }


                    int dungtuyen = 0;
                    if (par.First().Tuyen != null)
                    {
                        dungtuyen = int.Parse(par.First().Tuyen.ToString());
                    }
                    if (dungtuyen == 1)
                    {
                        rep.DungTuyen.Value = "X";
                        rep.mucHuong.Value = "Mức hưởng: " + DungChung.Ham._PtramTT(_Data, _muc) + "%";
                    }
                    else if (dungtuyen == 2)
                    {
                        rep.TraiTuyen.Value = "X";
                        int hangbv = DungChung.Ham.hangBV(DungChung.Bien.MaBV);
                        switch (hangbv)
                        {
                            case 1:
                                rep.mucHuong.Value = "Mức hưởng: " + "30" + "%";
                                break;
                            case 2:
                                rep.mucHuong.Value = "Mức hưởng: " + "50" + "%";
                                break;
                            case 3:
                                rep.mucHuong.Value = "Mức hưởng: " + "70" + "%";
                                break;
                            case 4:
                                rep.mucHuong.Value = "Mức hưởng: " + "70" + "%";
                                break;

                        }

                    }
                    int capcuu = int.Parse(par.First().CapCuu.ToString());
                    if (capcuu == 1)
                    {
                        rep.CapCuu.Value = "X";
                        rep.DungTuyen.Value = "";
                        rep.TraiTuyen.Value = "";
                    }
                }
                else
                {
                    rep.mucHuong.Value = "Dành cho BN không có BHYT";
                    rep.koBH.Value = "X";

                    int capcuu = int.Parse(par.First().CapCuu.ToString());
                    if (capcuu == 1)
                    {
                        rep.CapCuu.Value = "X";
                    }
                    rep.HanBHTu.Value = "";
                }

                rep.ChanDoan.Value = DungChung.Ham.getMaICDarr(_Data, _MaBenhNhan, DungChung.Bien.GetICD, 0)[1];
                rep.MaICD.Value = DungChung.Ham.getMaICDarr(_Data, _MaBenhNhan, DungChung.Bien.GetICD, 0)[0];
                rep.TongNgay.Value = "1 ngày";
                string _ngaysinh = "";
                if (par.First().NgaySinh != null && par.First().NgaySinh.ToString() != "")
                    _ngaysinh = par.First().NgaySinh.ToString() + "/";
                if (par.First().ThangSinh != null && par.First().ThangSinh.ToString() != "")
                    _ngaysinh = _ngaysinh + par.First().ThangSinh.ToString() + "/";
                if (par.First().NamSinh != null && par.First().NamSinh.ToString() != "")
                    _ngaysinh = _ngaysinh + par.First().NamSinh.ToString();
                rep.NSinh.Value = _ngaysinh;
                var ngaytt = _Data.VienPhis.Where(p => p.MaBNhan == (_MaBenhNhan)).Select(p => p.NgayTT).ToList();
                if (ngaytt.Count > 0)
                {

                    //rep.NgayGD.Value = DungChung.Ham.NgaySangChu(ngaytt.First().Value);
                    rep.NgayGD.Value = "Ngày ..... tháng ..... năm 20...";
                    rep.NgayTT.Value = DungChung.Ham.NgaySangChu(ngaytt.First().Value);
                }

                var bk02 = (from vp1 in _Data.DThuocs
                            join vpct in _Data.DThuoccts.Where(p => p.TrongBH == d1 || p.TrongBH == d2) on vp1.IDDon equals vpct.IDDon
                            join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                            join nhomdv in _Data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                            where vp1.MaBNhan == (_MaBenhNhan)
                            group new { nhomdv, dv, vpct } by new { vpct.TrongBH, nhomdv.TenNhom, nhomdv.STT, dv.TenDV, vpct.DonVi, vpct.DonGia, vpct.MaKP } into kq
                            select new { makp = kq.Key.MaKP, TrongDM = kq.Key.TrongBH, kq.Key.TenNhom, kq.Key.STT, kq.Key.TenDV, kq.Key.DonVi, kq.Key.DonGia, SoLuong = kq.Sum(p => p.vpct.SoLuong), ThanhTien = kq.Sum(p => p.vpct.ThanhTien), TienBN = kq.Sum(p => p.vpct.TienBH) }).ToList();

                var bk01 = (from kb in bk02
                            join k in _Kphong.Where(p => p.chon == true) on kb.makp equals k.makp
                            group new { kb } by new { kb.TenNhom, kb.STT, kb.TenDV, kb.DonVi, kb.DonGia, kb.TrongDM } into kq
                            select new { TrongDM = kq.Key.TrongDM, kq.Key.TenNhom, kq.Key.STT, kq.Key.TenDV, kq.Key.DonVi, kq.Key.DonGia, SoLuong = kq.Sum(p => p.kb.SoLuong), ThanhTien = kq.Sum(p => p.kb.ThanhTien), TienBN = kq.Sum(p => p.kb.TienBN) }).ToList();
                rep.Tongtien.Value = bk01.Sum(p => p.ThanhTien);
                rep.TienBN.Value = bk01.Sum(p => p.TienBN);
                rep.DataSource = bk01.ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                //}
            }

        }

        private void sbtThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}