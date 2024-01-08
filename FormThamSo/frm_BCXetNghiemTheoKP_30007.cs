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
    public partial class frm_BCXetNghiemTheoKP_30007 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCXetNghiemTheoKP_30007()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data;
        List<KPhongc> _Kphong = new List<KPhongc>();
        List<KPhong> _lkpall = new List<KPhong>();
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
        private void frm_BCXetNghiemTheoKP_30007_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            deTuNgay.Focus();
            deTuNgay.DateTime = System.DateTime.Now;
            deDenNgay.DateTime = System.DateTime.Now;
            _lkpall = _data.KPhongs.Where(p => p.Status == 1).ToList();
            var kphong = (from kp in _lkpall
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP,kp.PLoai }).ToList();
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
            var _listNhomDV = _data.NhomDVs.Where(p => p.IDNhom == 1 || p.IDNhom == 2 || p.IDNhom == 3 || p.IDNhom == 8).Where(p => p.Status > 0).ToList();
            cklNhomDV.DisplayMember = "TenNhomCT";
            cklNhomDV.ValueMember = "IDNhom";
            cklNhomDV.DataSource = _listNhomDV;
            cklNhomDV.CheckAll();
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
        public class _BC
        {
            public string TenDV { get; set; }
            public int MaDV { get; set; }
            public string DonVi { get; set; }
            public string LoaiPTTT { get; set; }
            public string TenNhom { get; set; }
            public int IDNhom { get; set; }

            public double Tong { get; set; }

            public double NoiTBHYT1 { get; set; }
            public double NoiTDV1 { get; set; }
            public double NgTBHYT1 { get; set; }
            public double NgTDV1 { get; set; }

            public double NoiTBHYT2 { get; set; }
            public double NoiTDV2 { get; set; }
            public double NgTBHYT2 { get; set; }
            public double NgTDV2 { get; set; }

            public double NoiTBHYT3 { get; set; }
            public double NoiTDV3 { get; set; }
            public double NgTBHYT3 { get; set; }
            public double NgTDV3 { get; set; }

            public double NoiTBHYT4 { get; set; }
            public double NoiTDV4 { get; set; }
            public double NgTBHYT4 { get; set; }
            public double NgTDV4 { get; set; }

            public double NoiTBHYT5 { get; set; }
            public double NoiTDV5 { get; set; }
            public double NgTBHYT5 { get; set; }
            public double NgTDV5 { get; set; }

            public double NoiTBHYT6 { get; set; }
            public double NoiTDV6 { get; set; }
            public double NgTBHYT6 { get; set; }
            public double NgTDV6 { get; set; }

            public double NoiTBHYT7 { get; set; }
            public double NoiTDV7 { get; set; }
            public double NgTBHYT7 { get; set; }
            public double NgTDV7 { get; set; }

            public double NoiTBHYT8 { get; set; }
            public double NoiTDV8 { get; set; }
            public double NgTBHYT8 { get; set; }
            public double NgTDV8 { get; set; }

            public double NoiTBHYT9 { get; set; }
            public double NoiTDV9 { get; set; }
            public double NgTBHYT9 { get; set; }
            public double NgTDV9 { get; set; }

            public double NoiTBHYT10 { get; set; }
            public double NoiTDV10 { get; set; }
            public double NgTBHYT10 { get; set; }
            public double NgTDV10 { get; set; }

            public double NoiTBHYT11 { get; set; }
            public double NoiTDV11 { get; set; }
            public double NgTBHYT11 { get; set; }
            public double NgTDV11 { get; set; }

        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            bool TaoBC = true;
            List<KPhongc> _lKhoaP = new List<KPhongc>();
            _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
            if (_lKhoaP.Count() == 0)
            {
                TaoBC = false;
                MessageBox.Show("Chưa chọn khoa phòng chỉ định");
            }
            List<int> _idNhomDV = new List<int>();
            for (int i = 0; i < cklNhomDV.ItemCount; i++)
            {
                if (cklNhomDV.GetItemCheckState(i) == CheckState.Checked)
                    _idNhomDV.Add(Convert.ToInt32(cklNhomDV.GetItemValue(i)));
            }
            if(_idNhomDV.Count()==0)
            {
                TaoBC = false;
                MessageBox.Show("Chưa chọn nhóm dịch vụ");
            }
            if (TaoBC)
            {
                int rgtheongay = rgChonNgay.SelectedIndex;
                List<_BC> lkqbc = new List<_BC>();
                DateTime _tungay = DungChung.Ham.NgayTu(deTuNgay.DateTime);
                DateTime _denngay = DungChung.Ham.NgayDen(deDenNgay.DateTime);

                var _ldv = (from dv in _data.DichVus
                            join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join n in _data.NhomDVs.Where(p => _idNhomDV.Contains(p.IDNhom)) on tn.IDNhom equals n.IDNhom
                            select new { dv, tn, n }).ToList();
                if (_lKhoaP.Count() <= 11)
                {

                    int mkp1 = 0, mkp2 = 0, mkp3 = 0, mkp4 = 0, mkp5 = 0, mkp6 = 0, mkp7 = 0, mkp8 = 0, mkp9 = 0, mkp10 = 0, mkp11 = 0;
                    string tenkp1 = "", tenkp2 = "", tenkp3 = "", tenkp4 = "", tenkp5 = "", tenkp6 = "", tenkp7 = "", tenkp8 = "", tenkp9 = "", tenkp10 = "", tenkp11 = "";
                    string ploai1 = "", ploai2 = "", ploai3 = "", ploai4 = "", ploai5 = "", ploai6 = "", ploai7 = "", ploai8 = "", ploai9 = "", ploai10 = "", ploai11 = "";
                    if (_lKhoaP.Count > 0)
                    {
                        mkp1 = _lKhoaP.First().makp;
                        tenkp1 = _lKhoaP.First().tenkp;
                        ploai1 = _lKhoaP.First().ploai;
                    }
                    if (_lKhoaP.Count > 1)
                    {
                        mkp2 = _lKhoaP.Skip(1).First().makp;
                        tenkp2 = _lKhoaP.Skip(1).First().tenkp;
                        ploai2 = _lKhoaP.Skip(1).First().ploai;
                    }
                    if (_lKhoaP.Count > 2)
                    {
                        mkp3 = _lKhoaP.Skip(2).First().makp;
                        tenkp3 = _lKhoaP.Skip(2).First().tenkp;
                        ploai3 = _lKhoaP.Skip(2).First().ploai;
                    }
                    if (_lKhoaP.Count > 3)
                    {
                        mkp4 = _lKhoaP.Skip(3).First().makp;
                        tenkp4 = _lKhoaP.Skip(3).First().tenkp;
                        ploai4 = _lKhoaP.Skip(3).First().ploai;
                    }
                    if (_lKhoaP.Count > 4)
                    {
                        mkp5 = _lKhoaP.Skip(4).First().makp;
                        tenkp5 = _lKhoaP.Skip(4).First().tenkp;
                        ploai5 = _lKhoaP.Skip(4).First().ploai;
                    }
                    if (_lKhoaP.Count > 5)
                    {
                        mkp6 = _lKhoaP.Skip(5).First().makp;
                        tenkp6 = _lKhoaP.Skip(5).First().tenkp;
                        ploai6 = _lKhoaP.Skip(5).First().ploai;
                    }
                    if (_lKhoaP.Count > 6)
                    {
                        mkp7 = _lKhoaP.Skip(6).First().makp;
                        tenkp7 = _lKhoaP.Skip(6).First().tenkp;
                        ploai7 = _lKhoaP.Skip(6).First().ploai;
                    }
                    if (_lKhoaP.Count > 7)
                    {
                        mkp8 = _lKhoaP.Skip(7).First().makp;
                        tenkp8 = _lKhoaP.Skip(7).First().tenkp;
                        ploai8 = _lKhoaP.Skip(7).First().ploai;
                    }
                    if (_lKhoaP.Count > 8)
                    {
                        mkp9 = _lKhoaP.Skip(8).First().makp;
                        tenkp9 = _lKhoaP.Skip(8).First().tenkp;
                        ploai9 = _lKhoaP.Skip(8).First().ploai;
                    }
                    if (_lKhoaP.Count > 9)
                    {
                        mkp10 = _lKhoaP.Skip(9).First().makp;
                        tenkp10 = _lKhoaP.Skip(9).First().tenkp;
                        ploai10 = _lKhoaP.Skip(9).First().ploai;
                    }
                    if (_lKhoaP.Count > 10)
                    {
                        mkp11 = _lKhoaP.Skip(10).First().makp;
                        tenkp11 = _lKhoaP.Skip(10).First().tenkp;
                        ploai11 = _lKhoaP.Skip(10).First().ploai;
                    }

                    if (rgtheongay == 0)
                    {
                        var _lcls = (from bn in _data.BenhNhans
                                     join cls in _data.CLS.Where(p => p.Status == 1).Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay) on bn.MaBNhan equals cls.MaBNhan
                                     join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                     select new { bn, cls, cd }).ToList();

                        var q1 = (from cls in _lcls
                                  join dv in _ldv on cls.cd.MaDV equals dv.dv.MaDV
                                  join kp in _lKhoaP on cls.cls.MaKP equals kp.makp
                                  group new { cls, dv, kp } by new { dv.dv.MaDV, dv.dv.TenDV, dv.n.IDNhom, dv.n.TenNhom, dv.dv.Loai, dv.tn.TenRG } into kq
                                  select new _BC
                                  {
                                      MaDV = kq.Key.MaDV,
                                      TenDV = kq.Key.TenDV,
                                      TenNhom = kq.Key.TenNhom,
                                      IDNhom = kq.Key.IDNhom,
                                      LoaiPTTT = (kq.Key.IDNhom != 8 || kq.Key.Loai < 0) ? "" : (kq.Key.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat ? ("P" + (kq.Key.Loai == 0 ? "DB" : kq.Key.Loai.ToString())) : ("T" + (kq.Key.Loai == 0 ? "DB" : kq.Key.Loai.ToString()))),

                                      NoiTBHYT1 = ploai1 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp1).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV1 = ploai1 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp1).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT1 = ploai1 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp1).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp1).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV1 = ploai1 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp1).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp1).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT2 = ploai2 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp2).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV2 = ploai2 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp2).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT2 = ploai2 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp2).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp2).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV2 = ploai2 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp2).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp2).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT3 = ploai3 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp3).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV3 = ploai3 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp3).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT3 = ploai3 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp3).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp3).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV3 = ploai3 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp3).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp3).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT4 = ploai4 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp4).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV4 = ploai4 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp4).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT4 = ploai4 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp4).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp4).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV4 = ploai4 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp4).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp4).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT5 = ploai5 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp5).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV5 = ploai5 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp5).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT5 = ploai5 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp5).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp5).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV5 = ploai5 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp5).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp5).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT6 = ploai6 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp6).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV6 = ploai6 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp6).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT6 = ploai6 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp6).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp6).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV6 = ploai6 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp6).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp6).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT7 = ploai7 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp7).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV7 = ploai7 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp7).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT7 = ploai7 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp7).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp7).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV7 = ploai7 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp7).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp7).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT8 = ploai8 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp8).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV8 = ploai8 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp8).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT8 = ploai8 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp8).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp8).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV8 = ploai8 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp8).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp8).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT9 = ploai9 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp9).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV9 = ploai9 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp9).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT9 = ploai9 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp9).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp9).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV9 = ploai9 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp9).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp9).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT10 = ploai10 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp10).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV10 = ploai10 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp10).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT10 = ploai10 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp10).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp10).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV10 = ploai10 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp10).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp10).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT11 = ploai11 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp11).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV11 = ploai11 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp11).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT11 = ploai11 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp11).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp11).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV11 = ploai11 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp11).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp11).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                  }).ToList();
                        lkqbc.AddRange(q1);
                    }
                    else
                    {
                        List<int> dsbntt = new List<int>();
                        var q2 = (from bn in _data.BenhNhans
                                  join cls in _data.VienPhis.Where(p => p.NgayTT >= _tungay && p.NgayTT <= _denngay) on bn.MaBNhan equals cls.MaBNhan
                                  select new { bn }).ToList();

                        dsbntt = q2.Select(p => p.bn.MaBNhan).ToList();

                        var _lcls = (from bn in _data.BenhNhans.Where(p => dsbntt.Contains(p.MaBNhan))
                                     join cls in _data.CLS.Where(p => p.Status == 1) on bn.MaBNhan equals cls.MaBNhan
                                     join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                     select new { bn, cls, cd }).ToList();

                        var q1 = (from cls in _lcls
                                  join dv in _ldv on cls.cd.MaDV equals dv.dv.MaDV
                                  join kp in _lKhoaP on cls.cls.MaKP equals kp.makp
                                  group new { cls, dv, kp } by new { dv.dv.MaDV, dv.dv.TenDV, dv.n.IDNhom, dv.n.TenNhom, dv.dv.Loai, dv.tn.TenRG } into kq
                                  select new _BC
                                  {
                                      MaDV = kq.Key.MaDV,
                                      TenDV = kq.Key.TenDV,
                                      TenNhom = kq.Key.TenNhom,
                                      IDNhom = kq.Key.IDNhom,
                                      LoaiPTTT = (kq.Key.IDNhom != 8 || kq.Key.Loai < 0) ? "" : (kq.Key.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat ? ("P" + (kq.Key.Loai == 0 ? "DB" : kq.Key.Loai.ToString())) : ("T" + (kq.Key.Loai == 0 ? "DB" : kq.Key.Loai.ToString()))),

                                      NoiTBHYT1 = ploai1 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp1).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV1 = ploai1 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp1).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT1 = ploai1 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp1).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp1).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV1 = ploai1 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp1).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp1).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT2 = ploai2 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp2).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV2 = ploai2 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp2).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT2 = ploai2 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp2).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp2).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV2 = ploai2 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp2).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp2).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT3 = ploai3 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp3).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV3 = ploai3 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp3).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT3 = ploai3 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp3).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp3).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV3 = ploai3 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp3).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp3).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT4 = ploai4 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp4).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV4 = ploai4 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp4).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT4 = ploai4 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp4).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp4).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV4 = ploai4 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp4).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp4).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT5 = ploai5 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp5).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV5 = ploai5 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp5).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT5 = ploai5 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp5).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp5).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV5 = ploai5 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp5).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp5).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT6 = ploai6 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp6).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV6 = ploai6 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp6).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT6 = ploai6 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp6).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp6).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV6 = ploai6 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp6).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp6).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT7 = ploai7 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp7).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV7 = ploai7 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp7).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT7 = ploai7 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp7).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp7).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV7 = ploai7 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp7).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp7).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT8 = ploai8 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp8).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV8 = ploai8 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp8).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT8 = ploai8 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp8).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp8).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV8 = ploai8 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp8).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp8).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT9 = ploai9 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp9).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV9 = ploai9 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp9).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT9 = ploai9 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp9).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp9).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV9 = ploai9 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp9).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp9).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT10 = ploai10 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp10).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV10 = ploai10 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp10).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT10 = ploai10 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp10).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp10).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV10 = ploai10 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp10).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp10).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),

                                      NoiTBHYT11 = ploai11 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp11).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NoiTDV11 = ploai11 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 0 : kq.Where(p => p.cls.cls.MaKP == mkp11).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 1).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTBHYT11 = ploai11 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp11).Where(p => p.cls.bn.DTuong == "BHYT").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp11).Where(p => p.cls.bn.DTuong == "BHYT" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                      NgTDV11 = ploai11 == DungChung.Bien.st_PhanLoaiKP.PhongKham ? kq.Where(p => p.cls.cls.MaKP == mkp11).Where(p => p.cls.bn.DTuong == "Dịch vụ").Select(p => p.cls.cls.IdCLS).Count() : kq.Where(p => p.cls.cls.MaKP == mkp11).Where(p => p.cls.bn.DTuong == "Dịch vụ" && p.cls.bn.NoiTru == 0).Select(p => p.cls.cls.IdCLS).Count(),
                                  }).ToList();
                        lkqbc.AddRange(q1);
                    }
                    lkqbc = (from a in lkqbc
                             group new { a } by new { a.TenDV, a.MaDV, a.TenNhom, a.IDNhom, a.LoaiPTTT } into kq
                             select new _BC
                             {
                                 MaDV = kq.Key.MaDV,
                                 TenDV = kq.Key.TenDV,
                                 TenNhom = kq.Key.TenNhom,
                                 IDNhom = kq.Key.IDNhom,
                                 LoaiPTTT = kq.Key.LoaiPTTT,

                                 NoiTBHYT1 = kq.Sum(p => p.a.NoiTBHYT1),
                                 NoiTDV1 = kq.Sum(p => p.a.NoiTDV1),
                                 NgTBHYT1 = kq.Sum(p => p.a.NgTBHYT1),
                                 NgTDV1 = kq.Sum(p => p.a.NgTDV1),

                                 NoiTBHYT2 = kq.Sum(p => p.a.NoiTBHYT2),
                                 NoiTDV2 = kq.Sum(p => p.a.NoiTDV2),
                                 NgTBHYT2 = kq.Sum(p => p.a.NgTBHYT2),
                                 NgTDV2 = kq.Sum(p => p.a.NgTDV2),

                                 NoiTBHYT3 = kq.Sum(p => p.a.NoiTBHYT3),
                                 NoiTDV3 = kq.Sum(p => p.a.NoiTDV3),
                                 NgTBHYT3 = kq.Sum(p => p.a.NgTBHYT3),
                                 NgTDV3 = kq.Sum(p => p.a.NgTDV3),

                                 NoiTBHYT4 = kq.Sum(p => p.a.NoiTBHYT4),
                                 NoiTDV4 = kq.Sum(p => p.a.NoiTDV4),
                                 NgTBHYT4 = kq.Sum(p => p.a.NgTBHYT4),
                                 NgTDV4 = kq.Sum(p => p.a.NgTDV4),

                                 NoiTBHYT5 = kq.Sum(p => p.a.NoiTBHYT5),
                                 NoiTDV5 = kq.Sum(p => p.a.NoiTDV5),
                                 NgTBHYT5 = kq.Sum(p => p.a.NgTBHYT5),
                                 NgTDV5 = kq.Sum(p => p.a.NgTDV5),

                                 NoiTBHYT6 = kq.Sum(p => p.a.NoiTBHYT6),
                                 NoiTDV6 = kq.Sum(p => p.a.NoiTDV6),
                                 NgTBHYT6 = kq.Sum(p => p.a.NgTBHYT6),
                                 NgTDV6 = kq.Sum(p => p.a.NgTDV6),

                                 NoiTBHYT7 = kq.Sum(p => p.a.NoiTBHYT7),
                                 NoiTDV7 = kq.Sum(p => p.a.NoiTDV7),
                                 NgTBHYT7 = kq.Sum(p => p.a.NgTBHYT7),
                                 NgTDV7 = kq.Sum(p => p.a.NgTDV7),

                                 NoiTBHYT8 = kq.Sum(p => p.a.NoiTBHYT8),
                                 NoiTDV8 = kq.Sum(p => p.a.NoiTDV8),
                                 NgTBHYT8 = kq.Sum(p => p.a.NgTBHYT8),
                                 NgTDV8 = kq.Sum(p => p.a.NgTDV8),

                                 NoiTBHYT9 = kq.Sum(p => p.a.NoiTBHYT9),
                                 NoiTDV9 = kq.Sum(p => p.a.NoiTDV9),
                                 NgTBHYT9 = kq.Sum(p => p.a.NgTBHYT9),
                                 NgTDV9 = kq.Sum(p => p.a.NgTDV9),

                                 NoiTBHYT10 = kq.Sum(p => p.a.NoiTBHYT10),
                                 NoiTDV10 = kq.Sum(p => p.a.NoiTDV10),
                                 NgTBHYT10 = kq.Sum(p => p.a.NgTBHYT10),
                                 NgTDV10 = kq.Sum(p => p.a.NgTDV10),

                                 NoiTBHYT11 = kq.Sum(p => p.a.NoiTBHYT11),
                                 NoiTDV11 = kq.Sum(p => p.a.NoiTDV11),
                                 NgTBHYT11 = kq.Sum(p => p.a.NgTBHYT11),
                                 NgTDV11 = kq.Sum(p => p.a.NgTDV11),

                                 Tong = kq.Sum(p => p.a.NoiTBHYT1 + p.a.NoiTDV1 + p.a.NgTBHYT1 + p.a.NgTDV1 + p.a.NoiTBHYT2 + p.a.NoiTDV2 + p.a.NgTBHYT2 + p.a.NgTDV2 +
                                     p.a.NoiTBHYT3 + p.a.NoiTDV3 + p.a.NgTBHYT3 + p.a.NgTDV3 + p.a.NoiTBHYT4 + p.a.NoiTDV4 + p.a.NgTBHYT4 + p.a.NgTDV4 +
                                     p.a.NoiTBHYT5 + p.a.NoiTDV5 + p.a.NgTBHYT5 + p.a.NgTDV5 + p.a.NoiTBHYT6 + p.a.NoiTDV6 + p.a.NgTBHYT6 + p.a.NgTDV6 +
                                     p.a.NoiTBHYT7 + p.a.NoiTDV7 + p.a.NgTBHYT7 + p.a.NgTDV7 + p.a.NoiTBHYT8 + p.a.NoiTDV8 + p.a.NgTBHYT8 + p.a.NgTDV8 +
                                     p.a.NoiTBHYT9 + p.a.NoiTDV9 + p.a.NgTBHYT9 + p.a.NgTDV9 + p.a.NoiTBHYT10 + p.a.NoiTDV10 + p.a.NgTBHYT10 + p.a.NgTDV10 +
                                     p.a.NoiTBHYT11 + p.a.NoiTDV11 + p.a.NgTBHYT11 + p.a.NgTDV11)
                             }).OrderBy(p => p.IDNhom).ThenBy(p => p.TenDV).ToList();

                    if (_lKhoaP.Count <= 3)
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_BCXetNgiemTheoKP_30007 rep = new BaoCao.Rep_BCXetNgiemTheoKP_30007();
                        rep.TenKP1.Value = tenkp1;
                        rep.TenKP2.Value = tenkp2;
                        rep.TenKP3.Value = tenkp3;
                        rep.NgayThang.Value = "Từ ngày: " + _tungay.ToShortTimeString() + " đến ngày " + _denngay.ToShortTimeString();

                        rep.DataSource = lkqbc;
                        rep.BinDingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else if (_lKhoaP.Count <= 7)
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_BCXetNgiemTheoKP_30007 rep = new BaoCao.Rep_BCXetNgiemTheoKP_30007();
                        rep.TenKP1.Value = tenkp1;
                        rep.TenKP2.Value = tenkp2;
                        rep.TenKP3.Value = tenkp3;
                        rep.NgayThang.Value = "Từ ngày: " + _tungay.ToShortTimeString() + " đến ngày " + _denngay.ToShortTimeString();

                        rep.DataSource = lkqbc;
                        rep.BinDingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();

                        frmIn frm1 = new frmIn();
                        BaoCao.Rep_BCXetNgiemTheoKP_30007_Trang2 rep1 = new BaoCao.Rep_BCXetNgiemTheoKP_30007_Trang2(1);
                        rep1.TenKP1.Value = tenkp4;
                        rep1.TenKP2.Value = tenkp5;
                        rep1.TenKP3.Value = tenkp6;
                        rep1.TenKP4.Value = tenkp7;
                        rep1.NgayThang.Value = "Từ ngày: " + _tungay.ToShortTimeString() + " đến ngày " + _denngay.ToShortTimeString();

                        rep1.DataSource = lkqbc;
                        rep1.BinDingData();
                        rep1.CreateDocument();
                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                        frm1.ShowDialog();
                    }
                    else //if (_lKhoaP.Count < 8)
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_BCXetNgiemTheoKP_30007 rep = new BaoCao.Rep_BCXetNgiemTheoKP_30007();
                        rep.TenKP1.Value = tenkp1;
                        rep.TenKP2.Value = tenkp2;
                        rep.TenKP3.Value = tenkp3;
                        rep.NgayThang.Value = "Từ ngày: " + _tungay.ToShortTimeString() + " đến ngày " + _denngay.ToShortTimeString();

                        rep.DataSource = lkqbc;
                        rep.BinDingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();

                        frmIn frm1 = new frmIn();
                        BaoCao.Rep_BCXetNgiemTheoKP_30007_Trang2 rep1 = new BaoCao.Rep_BCXetNgiemTheoKP_30007_Trang2(1);
                        rep1.TenKP1.Value = tenkp4;
                        rep1.TenKP2.Value = tenkp5;
                        rep1.TenKP3.Value = tenkp6;
                        rep1.TenKP4.Value = tenkp7;
                        rep1.NgayThang.Value = "Từ ngày: " + _tungay.ToShortTimeString() + " đến ngày " + _denngay.ToShortTimeString();

                        rep1.DataSource = lkqbc;
                        rep1.BinDingData();
                        rep1.CreateDocument();
                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                        frm1.ShowDialog();

                        frmIn frm2 = new frmIn();
                        BaoCao.Rep_BCXetNgiemTheoKP_30007_Trang2 rep2 = new BaoCao.Rep_BCXetNgiemTheoKP_30007_Trang2(2);
                        rep2.TenKP1.Value = tenkp8;
                        rep2.TenKP2.Value = tenkp9;
                        rep2.TenKP3.Value = tenkp10;
                        rep2.TenKP4.Value = tenkp11;
                        rep2.NgayThang.Value = "Từ ngày: " + _tungay.ToShortTimeString() + " đến ngày " + _denngay.ToShortTimeString();

                        rep2.DataSource = lkqbc;
                        rep2.BinDingData();
                        rep2.CreateDocument();
                        frm2.prcIN.PrintingSystem = rep2.PrintingSystem;
                        frm2.ShowDialog();
                    }

                }
                else
                {
                    MessageBox.Show("Báo cáo lấy tối đa 11 khoa|phòng, chọn lại ít hơn 11");
                }
            }
        }
    }
}