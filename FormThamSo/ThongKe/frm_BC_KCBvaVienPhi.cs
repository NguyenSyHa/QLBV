using System;
using QLBV_Database;
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
    public partial class frm_BC_KCBvaVienPhi : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_KCBvaVienPhi()
        {
            InitializeComponent();
        }

        private void frm_BC_KCBvaVienPhi_Load(object sender, EventArgs e)
        {
            luptungay.Focus();
            luptungay.DateTime = System.DateTime.Now;
            lupdenngay.DateTime = System.DateTime.Now;
        }

        private void btntaobc_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(luptungay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupdenngay.DateTime);
            string _dtuong = "";
            _dtuong = cboDTuong.Text;
            List<KPhong> _lkp = _data.KPhongs.ToList();

            var _bn = (from bn in _data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong)
                       join rv in _data.RaViens.Where(p => p.NgayRa <= denngay) on bn.MaBNhan equals rv.MaBNhan
                       join vv in _data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                       group new { bn, rv, vv } by new { bn.MaBNhan, rv.MaKP } into kq
                       select new { idrv = kq.Max(p => p.rv.IdRaVien), kq.Key.MaBNhan, MaKP = kq.Key.MaKP }).ToList();

            var _lbn = (from bn in _data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong)
                        join rv in _data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on bn.MaBNhan equals rv.MaBNhan
                        join vv in _data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                        select new { bn.MaBNhan, SONGAY = rv.SoNgaydt ?? 0, MaKP = rv.MaKP, thanhtien = 0.0 }).ToList();

            var _vp = (from a in _data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                       join b in _data.VienPhicts on a.idVPhi equals b.idVPhi
                       group new { a, b } by new { a.MaBNhan, a.NgayTT, a.idVPhi } into kq
                       select new
                       {
                           kq.Key.MaBNhan,
                           kq.Key.NgayTT,
                           TienBN = kq.Sum(p => p.b.TienBN),
                           kq.Key.idVPhi
                       }).ToList();

            var _tu = (from a in _data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                       join b in _data.TamUngs.Where(p => p.PhanLoai == 3) on a.MaBNhan equals b.MaBNhan into q1
                       from q in q1.DefaultIfEmpty()
                       group new { a, q } by new { a.MaBNhan, a.NgayTT, a.idVPhi } into kq
                       select new
                       {
                           kq.Key.MaBNhan,
                           kq.Key.NgayTT,
                           SoTien = kq.Sum(p => p.q.SoTien ?? 0.0),
                           kq.Key.idVPhi
                       }).ToList();

            var _tien = (from a in _vp
                         join b in _tu on a.idVPhi equals b.idVPhi
                         join c in _bn on a.MaBNhan equals c.MaBNhan
                         group new { a, b, c } by new { a.MaBNhan, c.MaKP } into kq
                         select new
                         {
                             MaBNhan = kq.Key.MaBNhan ?? 0,
                             SONGAY = 0,
                             MaKP = kq.Key.MaKP,
                             thanhtien = (kq.Sum(p => p.a.TienBN - p.b.SoTien) > 0) ? kq.Sum(p => p.a.TienBN - p.b.SoTien) : 0.00
                         }).ToList();

            var _kq2 = _lbn.Concat(_tien).ToList();

            var _gr = (from a in _kq2
                       group a by new { a.MaBNhan } into kq
                       select new
                       {
                           kq.Key.MaBNhan,
                           SONGAY = kq.Sum(p => p.SONGAY),
                           MaKP = kq.Max(p => p.MaKP),
                           thanhtien = kq.Sum(p => p.thanhtien)
                       }).ToList();

            var _kq = (from a in _gr
                       join b in _data.KPhongs on a.MaKP equals b.MaKP
                       group new { a, b } by new { a.MaKP, b.TenKP } into kq
                       select new
                       {
                           kq.Key.MaKP,
                           kq.Key.TenKP,
                           SOBN = kq.Where(p => p.a.SONGAY > 0).Select(p => p.a.MaBNhan).Distinct().Count(),
                           SONGAY = kq.Sum(p => p.a.SONGAY),
                           TONGTIEN = kq.Sum(p => p.a.thanhtien)
                       }).OrderBy(p => p.TenKP).ToList();

            var _kq1 = (from k in _kq
                        group k by new { k.MaKP, k.TenKP, k.SOBN, k.SONGAY, k.TONGTIEN } into kq
                        select new
                        {
                            kq.Key.MaKP,
                            TenKP = kq.Key.TenKP,
                            SOBN = kq.Key.SOBN,
                            TONGTIEN = kq.Key.TONGTIEN,
                            SONGAY = kq.Key.SONGAY,
                            SONGAYTB = (kq.Key.SOBN > 0) ? Math.Round(Convert.ToDouble(kq.Key.SONGAY) / Convert.ToDouble(kq.Key.SOBN), 1) : 0.00,
                            CHIPHITB = (kq.Key.SOBN > 0) ? Math.Round(kq.Key.TONGTIEN / kq.Key.SOBN, 1) : 0.00
                        }).OrderBy(p => p.TenKP).ToList();
            double chiphitb = 0, ngaytb = 0;
            if (_kq1.Sum(p => p.SOBN) > 0)
            {
                chiphitb = Math.Round(_kq1.Sum(p => p.TONGTIEN) / Convert.ToDouble(_kq1.Sum(p => p.SOBN)), 1);
                ngaytb = Math.Round(Convert.ToDouble(_kq1.Sum(p => p.SONGAY)) / Convert.ToDouble(_kq1.Sum(p => p.SOBN)), 1);
            }
            frmIn frm = new frmIn();
            BaoCao.rep_BC_KCBvaVienPhi rep = new BaoCao.rep_BC_KCBvaVienPhi();
            rep.chiphitb.Value = chiphitb;
            rep.ngaytb.Value = ngaytb;
            rep.cqcq.Value = DungChung.Bien.TenCQCQ;
            rep.tenbv.Value = DungChung.Bien.TenCQ;
            if (_dtuong == "Cả hai")
            {
                rep.tieude.Value = "BÁO CÁO KCB VÀ VIỆN PHÍ CỦA BỆNH NHÂN RA VIỆN";
            }
            else
            {
                if (_dtuong == "BHYT")
                {
                    rep.tieude.Value = "BÁO CÁO KCB VÀ VIỆN PHÍ CỦA BỆNH NHÂN BHYT RA VIỆN";
                }
                else
                {
                    rep.tieude.Value = "BÁO CÁO KCB VÀ VIỆN PHÍ CỦA BỆNH NHÂN DỊCH VỤ RA VIỆN";
                }
            }
            rep.ngaythang.Value = "Từ ngày: " + tungay.ToShortDateString() + " Đến ngày: " + denngay.ToShortDateString();
            rep.DataSource = _kq1;
            rep.databinding();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}