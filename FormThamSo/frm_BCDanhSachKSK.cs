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
    public partial class frm_BCDanhSachKSK : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCDanhSachKSK()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

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
        private void frm_BCDanhSachKSK_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            rgTheoNgay.SelectedIndex = 2;

            var kphong = (from kp in data.KPhongs
                          where (/**DungChung.Bien.MaBV !="30002" ?  kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám" :*/  kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám" || kp.TenKP =="Phòng tiếp đón" )
                          select new { kp.TenKP, kp.MaKP,kp.PLoai }).ToList();
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

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            List<KPhong> _lKhoaP = new List<KPhong>();
            _lKhoaP = _Kphong.Where(p => p.makp != 0).Where(p => p.chon == true).ToList();
            DateTime tungaycls = tungay.AddMonths(-1);
            DateTime denngaycls = denngay.AddMonths(1);
            var _ldv = data.DichVus.Where(p => p.PLoai == 2).ToList();
            int theongay = rgTheoNgay.SelectedIndex;


            var q1 = (from bn in data.BenhNhans.Where(p => p.DTuong == "KSK").Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                      join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                      select new { bn.MaBNhan, bn.TenBNhan, bn.ChuyenKhoa, bn.MaKP, bn.SThe, bn.Tuoi, bn.GTinh, bn.DChi, bn.NNhap, bn.CDNoiGT, ttbx.NoiLV, bn.TChung, bn.NamSinh }).ToList();
            if (theongay == 0)
            {
                q1 = (from bn in data.BenhNhans.Where(p => p.DTuong == "KSK").Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                      join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                      where !(from vp in data.TamUngs.Where(p => p.PhanLoai == 1) select vp.MaBNhan).Contains(bn.MaBNhan)
                      select new { bn.MaBNhan, bn.TenBNhan, bn.ChuyenKhoa, bn.MaKP, bn.SThe, bn.Tuoi, bn.GTinh, bn.DChi, bn.NNhap, bn.CDNoiGT, ttbx.NoiLV, bn.TChung, bn.NamSinh }).ToList();
            }
            else if (theongay == 1)
            {
                q1 = (from bn in data.BenhNhans.Where(p => p.DTuong == "KSK").Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                      join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                      where (from vp in data.TamUngs.Where(p => p.PhanLoai == 1) select vp.MaBNhan).Contains(bn.MaBNhan)
                      select new { bn.MaBNhan, bn.TenBNhan, bn.ChuyenKhoa, bn.MaKP, bn.SThe, bn.Tuoi, bn.GTinh, bn.DChi, bn.NNhap, bn.CDNoiGT, ttbx.NoiLV, bn.TChung, bn.NamSinh }).ToList();
            }
            List<int> _lmabn = q1.Select(p => p.MaBNhan).ToList();
            var _lcls = (from bn in _lmabn
                         join cls in data.CLS.Where(p => p.NgayThang >= tungay && p.NgayThang <= denngay) on bn equals cls.MaBNhan
                         join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                         select new { cls.MaBNhan, cd.MaDV, cd.IDCD }).ToList();
            var q2 = (from cls in _lcls
                      join dv in _ldv on cls.MaDV equals dv.MaDV
                      group new { cls, dv } by new { cls.MaBNhan } into kq
                      select new { MaBNhan = kq.Key.MaBNhan ?? 0, TenDVcd = string.Join(";", kq.Select(p => p.dv.TenDV).Distinct().ToArray()) }).ToList();
            var q3 = (from a in q1
                      join kp in _lKhoaP on a.MaKP equals kp.makp
                      join b in q2 on a.MaBNhan equals b.MaBNhan into kq
                      from kq1 in kq.DefaultIfEmpty()
                      select new
                      {
                          a.MaBNhan,
                          a.TenBNhan,
                          a.ChuyenKhoa,
                          a.TChung,
                          a.NamSinh,
                          a.MaKP,
                          a.SThe,
                          a.Tuoi,
                          KetLuan = a.CDNoiGT + " " + a.NoiLV,
                          GTinh = a.GTinh == 1 ? "Nam" : "Nữ",
                          a.DChi,
                          NNhap = a.NNhap.Value.ToShortDateString(),
                          a.CDNoiGT,
                          a.NoiLV,
                          TenDV = kq1 != null ? kq1.TenDVcd : "",
                          kp.tenkp
                      }).ToList();

            frmIn frm = new frmIn();
            BaoCao.Rep_DSBenhNhanKSK rep = new BaoCao.Rep_DSBenhNhanKSK();
            rep.Ngaythang.Value = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
            rep.TieuDe.Value = "BÁO CÁO TÌNH HÌNH KHÁM SỨC KHỎE";
            rep.CQCQ.Value = DungChung.Bien.TenCQCQ;
            rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
            rep.BindingData();
            rep.DataSource = q3;
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}