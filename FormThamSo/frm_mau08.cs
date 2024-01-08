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
    public partial class frm_mau08 : DevExpress.XtraEditors.XtraForm
    {
        public frm_mau08()
        {
            InitializeComponent();
        }
        int _maBN =0;
        public frm_mau08(int mabn)
        {
            _maBN = mabn;
            InitializeComponent();
        }
        public class danhsachDVSD
        {
            private string tedv;
            private int madv;
            private int idnhom;
            private bool chon;
            private string ploai;

            public string PLoai
            {
                get { return ploai; }
                set { ploai = value; }
            }
            public int MaDV
            {
                set { madv = value; }
                get { return madv; }
            }
            public string TenDV
            {
                set { tedv = value; }
                get { return tedv; }
            }
            public int IDNhom
            {
                set { idnhom = value; }
                get { return idnhom; }
            }
            public bool Chon
            {
                set { chon = value; }
                get { return chon; }
            }
        }
        List<danhsachDVSD> _lds = new List<danhsachDVSD>();
        private void frm_mau08_Load(object sender, EventArgs e)
        {
            _lds.Clear();
            QLBV_Database.QLBVEntities _data=new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //lup_MaDV.DataSource = _data.DichVus.ToList();
            var dthuoc = (from dt in _data.DThuocs.Where(p => p.MaBNhan== (_maBN))
                          join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                          join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                          join kp in _data.KPhongs on dtct.MaKP equals kp.MaKP
                          select new { MaDV = dtct.MaDV, IDNhom = dv.IDNhom, TenDV = dv.TenDV, PLoai = kp.PLoai }).Distinct().ToList();
            foreach (var a in dthuoc)
            {
                _lds.Add(new danhsachDVSD { MaDV = a.MaDV == null ? 0 : Convert.ToInt32(a.MaDV), Chon = true, TenDV = a.TenDV, IDNhom = a.IDNhom == null ? 0 : a.IDNhom.Value, PLoai = a.PLoai });
            }
            if (DungChung.Bien.MaBV == "30010")
            {
                checkPK.Visible = true;
                checkKDT.Visible = true;
                var dthuoc1 = (from dt in _data.CLS.Where(p => p.MaBNhan == (_maBN))
                                   join dtct in _data.ChiDinhs on dt.IdCLS equals dtct.IdCLS
                                   join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                                   join kp in _data.KPhongs on dt.MaKP equals kp.MaKP
                                   select new {MaDV = dtct.MaDV, IDNhom = dv.IDNhom, TenDV = dv.TenDV, PLoai = kp.PLoai }).Distinct().ToList();

                var dthuoc2 = (from dt in _data.DThuocs.Where(p => p.MaBNhan == (_maBN)).Where(p => p.PLDV == 1)
                                join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                                join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                                join kp in _data.KPhongs on dtct.MaKP equals kp.MaKP
                               select new { MaDV = dtct.MaDV, IDNhom = dv.IDNhom, TenDV = dv.TenDV, PLoai = kp.PLoai }).Distinct().ToList();

                var dthuoc3 = (from dt in _data.DThuocs.Where(p => p.MaBNhan == (_maBN)).Where(p => p.PLDV == 2)
                               join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                               join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                               join kp in _data.KPhongs on dtct.MaKP equals kp.MaKP
                               join tn in _data.TieuNhomDVs.Where(p => p.TenTN.ToLower().Contains("tiền công khám")) on dv.IdTieuNhom equals tn.IdTieuNhom
                               select new { MaDV = dtct.MaDV, IDNhom = dv.IDNhom, TenDV = dv.TenDV, PLoai = kp.PLoai }).Distinct().ToList();

                var dthuoc4 = dthuoc1.Concat(dthuoc2).Concat(dthuoc3);
                _lds.Clear();
                foreach (var a in dthuoc4)
                {
                    _lds.Add(new danhsachDVSD { MaDV = a.MaDV == null ? 0 : Convert.ToInt32(a.MaDV), Chon = true, TenDV = a.TenDV, IDNhom = a.IDNhom == null ? 0 : a.IDNhom.Value, PLoai = a.PLoai });
                }
            }
            
            grcThuocSD.DataSource = _lds.ToList().OrderBy(p=>p.IDNhom).ThenBy(p=>p.TenDV);
            
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {

                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                frmIn frm = new frmIn();
                List<ChiDinh> _lcd = new List<ChiDinh>();
                _lcd = (from cls in _data.CLS.Where(p => p.MaBNhan == _maBN)
                        join cdi in _data.ChiDinhs on cls.IdCLS equals cdi.IdCLS
                        select cdi).ToList();
                if (DungChung.Bien.MaBV == "30350")
                {
                    BaoCao.rep_PhieuKCBNgoaiT_A4 rep = new BaoCao.rep_PhieuKCBNgoaiT_A4(_lcd);
                    var ktkd = (from dt in _data.DThuocs.Where(p => p.MaBNhan == (_maBN))
                                join
                                    cb in _data.CanBoes on dt.MaCB equals cb.MaCB

                                select new { dt.PLDV, dt.GhiChu, dt.IDDon, dt.KieuDon, dt.LoaiDuoc, dt.MaBNhan, dt.NgayKe, cb.TenCB }).ToList().OrderBy(p => p.PLDV).ToList();
                    if (ktkd.Count > 0)
                    {
                        if (ktkd.First().NgayKe.Value.Day > 0)
                        {

                            rep.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
                        }
                    }


                    //rep._idDon.Value = ktkd.First().IDDon;

                    rep.ICD.Value = DungChung.Ham.getMaICDarr(_data, _maBN, DungChung.Bien.GetICD,0)[0];
                    rep.ChanDoan.Value = DungChung.Ham.getMaICDarr(_data, _maBN, DungChung.Bien.GetICD,0)[1];
                    var tt = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == (_maBN))
                              join kb in _data.BNKBs on bn.MaBNhan equals kb.MaBNhan
                              join cb in _data.CanBoes on kb.MaCB equals cb.MaCB
                              join kp in _data.KPhongs on kb.MaKP equals kp.MaKP

                              select new { bn.TenBNhan, kb.NgayKham, kp.TenKP, cb.CapBac, bn.SoTT, bn.GTinh, bn.MaCS, bn.NamSinh, cb.TenCB, bn.HanBHDen, bn.HanBHTu, bn.NgaySinh, bn.ThangSinh, bn.NNhap, kb.MaICD, kb.ChanDoan, kb.BenhKhac, kb.IDKB, bn.SThe, bn.DChi }).OrderBy(p => p.IDKB).ToList();
                    if (tt.Count > 0)
                    {
                        rep._TenBNhan.Value = tt.First().TenBNhan;
                        rep.TenCB.Value = tt.First().CapBac + ": " + tt.First().TenCB;
                        rep.Ngaykham.Value = tt.First().NgayKham.ToString().Substring(0, 10);
                        rep.TenKP.Value = tt.First().TenKP;
                        string _ngay = "", _thang = "";
                        if (!string.IsNullOrEmpty(tt.First().NgaySinh))
                            _ngay = tt.First().NgaySinh + "/";
                        if (!string.IsNullOrEmpty(tt.First().ThangSinh))
                            _thang = tt.First().ThangSinh + "/";
                        rep.Tuoi.Value = _ngay + _thang + tt.First().NamSinh;

                        switch (tt.First().GTinh)
                        {
                            case 1:
                                rep.GTinh.Value = "Nam";
                                break;
                            case 0:
                                rep.GTinh.Value = "Nữ";
                                break;
                        }
                        if (tt.First().HanBHDen != null && tt.First().HanBHDen.Value.Day > 0)
                            rep.HanDen.Value = tt.First().HanBHDen.ToString().Substring(0, 10);
                        if (tt.First().HanBHTu != null && tt.First().HanBHTu.Value.Day > 0)
                            rep.HanTu.Value = tt.First().HanBHTu.ToString().Substring(0, 10);
                        rep.MaCS.Value = tt.First().MaCS;
                        rep.SThe.Value = tt.First().SThe;

                        rep.DiaChi.Value = tt.First().DChi;
                        DateTime _ngaynhap = tt.First().NNhap.Value;
                        rep.SoPhieu.Value = "Phiếu số: " + tt.First().SoTT;
                        rep._MaBNhan.Value = _maBN;
                        string macs = tt.First().MaCS;
                        // lấy mã KCB ban đầu
                        var madkkcb = (from bv in _data.BenhViens.Where(p => p.MaBV == macs)
                                       select new { bv.TenBV }).FirstOrDefault();
                        if (madkkcb != null)
                            rep.dkkcbbd.Value = madkkcb.TenBV;
                    }
                    //int id = ktkd.First().IDDon;

                    var dthuoc = (from dt in _data.DThuocs.Where(p => p.MaBNhan == (_maBN))
                                  join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                                  join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                                  join tnhom in _data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                  join nhomdv in _data.NhomDVs on tnhom.IDNhom equals nhomdv.IDNhom
                                  select new { nhomdv.TenNhom, nhomdv.STT, dtct.MaDV, dv.DonVi, dv.TenDV, dtct.DonGia, dtct.IDCD, dtct.TrongBH, dtct.IDDonct, dtct.SoLuong, dtct.ThanhTien }).ToList();
                    var q = (from dv in _lds.Where(p => p.Chon == true)
                             join dtct in dthuoc on dv.MaDV equals dtct.MaDV
                             group new { dv, dtct } by new { dtct.TenNhom, dtct.STT, dv.TenDV, dv.MaDV, dtct.DonVi, dtct.DonGia, dtct.IDCD, dtct.TrongBH } into kq
                             select new { kq.Key.TrongBH, idmin = kq.Min(p => p.dtct.IDDonct), kq.Key.IDCD, kq.Key.TenNhom, kq.Key.STT, kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, kq.Key.DonGia, SoLuong = kq.Sum(p => p.dtct.SoLuong), ThanhTien = kq.Sum(p => p.dtct.ThanhTien) }).OrderBy(p => p.STT).ThenBy(p => p.idmin).ToList();
                    rep.DataSource = q.ToList();
                    rep.BindData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;

                    frm.ShowDialog();
                }
                else
                {
                    BaoCao.rep_PhieuKCBNgoaiT rep = new BaoCao.rep_PhieuKCBNgoaiT(_lcd);
                    var ktkd = (from dt in _data.DThuocs.Where(p => p.MaBNhan == (_maBN))
                                join
                                    cb in _data.CanBoes on dt.MaCB equals cb.MaCB

                                select new { dt.PLDV, dt.GhiChu, dt.IDDon, dt.KieuDon, dt.LoaiDuoc, dt.MaBNhan, dt.NgayKe, cb.TenCB }).ToList().OrderBy(p => p.PLDV).ToList();
                    if (ktkd.Count > 0)
                    {
                        if (ktkd.First().NgayKe.Value.Day > 0)
                        {

                            rep.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
                        }
                    }


                    //rep._idDon.Value = ktkd.First().IDDon;

                    rep.ICD.Value = DungChung.Ham.getMaICDarr(_data, _maBN, DungChung.Bien.GetICD,0)[0];
                    rep.ChanDoan.Value = DungChung.Ham.getMaICDarr(_data, _maBN, DungChung.Bien.GetICD,0)[1];
                    var tt = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == (_maBN))
                              join kb in _data.BNKBs on bn.MaBNhan equals kb.MaBNhan
                              join cb in _data.CanBoes on kb.MaCB equals cb.MaCB
                              join kp in _data.KPhongs on kb.MaKP equals kp.MaKP

                              select new { bn.TenBNhan, kb.NgayKham, kp.TenKP, cb.CapBac, bn.SoTT, bn.GTinh, bn.MaCS, bn.NamSinh, cb.TenCB, bn.HanBHDen, bn.HanBHTu, bn.NgaySinh, bn.ThangSinh, bn.NNhap, kb.MaICD, kb.ChanDoan, kb.BenhKhac, kb.IDKB, bn.SThe, bn.DChi }).OrderBy(p => p.IDKB).ToList();
                    if (tt.Count > 0)
                    {
                        rep._TenBNhan.Value = tt.First().TenBNhan;
                        rep.TenCB.Value = tt.First().CapBac + ": " + tt.First().TenCB;
                        rep.Ngaykham.Value = tt.First().NgayKham.ToString().Substring(0, 10);
                        rep.TenKP.Value = tt.First().TenKP;
                        string _ngay = "", _thang = "";
                        if (!string.IsNullOrEmpty(tt.First().NgaySinh))
                            _ngay = tt.First().NgaySinh + "/";
                        if (!string.IsNullOrEmpty(tt.First().ThangSinh))
                            _thang = tt.First().ThangSinh + "/";
                        rep.Tuoi.Value = _ngay + _thang + tt.First().NamSinh;

                        switch (tt.First().GTinh)
                        {
                            case 1:
                                rep.GTinh.Value = "Nam";
                                break;
                            case 0:
                                rep.GTinh.Value = "Nữ";
                                break;
                        }
                        if (tt.First().HanBHDen != null && tt.First().HanBHDen.Value.Day > 0)
                            rep.HanDen.Value = tt.First().HanBHDen.ToString().Substring(0, 10);
                        if (tt.First().HanBHTu != null && tt.First().HanBHTu.Value.Day > 0)
                            rep.HanTu.Value = tt.First().HanBHTu.ToString().Substring(0, 10);
                        rep.MaCS.Value = tt.First().MaCS;
                        rep.SThe.Value = tt.First().SThe;

                        rep.DiaChi.Value = tt.First().DChi;
                        DateTime _ngaynhap = tt.First().NNhap.Value;
                        rep.SoPhieu.Value = "Phiếu số: " + tt.First().SoTT;
                        rep._MaBNhan.Value = _maBN;
                        string macs = tt.First().MaCS;
                        // lấy mã KCB ban đầu
                        var madkkcb = (from bv in _data.BenhViens.Where(p => p.MaBV == macs)
                                       select new { bv.TenBV }).FirstOrDefault();
                        if (madkkcb != null)
                            rep.dkkcbbd.Value = madkkcb.TenBV;
                    }
                    //int id = ktkd.First().IDDon;

                    var dthuoc = (from dt in _data.DThuocs.Where(p => p.MaBNhan == (_maBN))
                                  join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                                  join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                                  join tnhom in _data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                  join nhomdv in _data.NhomDVs on tnhom.IDNhom equals nhomdv.IDNhom
                                  select new { nhomdv.TenNhom, nhomdv.STT, dtct.MaDV, dv.DonVi, dv.TenDV, dtct.DonGia, dtct.IDCD, dtct.TrongBH, dtct.IDDonct, dtct.SoLuong, dtct.ThanhTien }).ToList();
                    var q = (from dv in _lds.Where(p => p.Chon == true)
                             join dtct in dthuoc on dv.MaDV equals dtct.MaDV
                             group new { dv, dtct } by new { dtct.TenNhom, dtct.STT, dv.TenDV, dv.MaDV, dtct.DonVi, dtct.DonGia, dtct.IDCD, dtct.TrongBH } into kq
                             select new { kq.Key.TrongBH, idmin = kq.Min(p => p.dtct.IDDonct), kq.Key.IDCD, kq.Key.TenNhom, kq.Key.STT, kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, kq.Key.DonGia, SoLuong = kq.Sum(p => p.dtct.SoLuong), ThanhTien = kq.Sum(p => p.dtct.ThanhTien) }).OrderBy(p => p.STT).ThenBy(p => p.idmin).ToList();
                    rep.DataSource = q.ToList();
                    rep.BindData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;

                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không in được phiếu:" + ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void checkPK_CheckedChanged(object sender, EventArgs e)
        {
            if(checkPK.Checked== false)
            {
                foreach(var item in _lds)
                {
                    if(item.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                    item.Chon = false;
                }
            }
            else
            {
                foreach(var item in _lds)
                {
                    if(item.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                    item.Chon = true;
                }
            }
            grcThuocSD.DataSource = _lds.ToList().OrderBy(p => p.IDNhom).ThenBy(p => p.TenDV);
        }

        private void checkKDT_CheckedChanged(object sender, EventArgs e)
        {
            if(checkKDT.Checked == false)
            {
                foreach (var item in _lds)
                {
                    if (item.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang)
                    item.Chon = false;
                }
            }
            else
            {
                foreach (var item in _lds)
                {
                    if (item.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang)
                        item.Chon = true;
                }
            }
            grcThuocSD.DataSource = _lds.ToList().OrderBy(p => p.IDNhom).ThenBy(p => p.TenDV);
        }
    }
}