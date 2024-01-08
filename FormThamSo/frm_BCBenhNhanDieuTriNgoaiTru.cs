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
    public partial class frm_BCBenhNhanDieuTriNgoaiTru : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCBenhNhanDieuTriNgoaiTru()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_BCBenhNhanDieuTriNgoaiTru_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);
            int kieutim = radTimKiem.SelectedIndex;
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<BC> _list = new List<BC>();
            #region tìm theo ngày ra
            if (kieutim == 0)
            {
                var qbn = (from rv in _data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay)
                           join bn in _data.BenhNhans.Where(p => p.DTNT == true && p.NoiTru == 0) on rv.MaBNhan equals bn.MaBNhan
                           join vv in _data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                           select new
                           {
                               rv.MaKP,
                               bn.MaBNhan,
                               bn.TenBNhan,
                               bn.GTinh,
                               bn.NamSinh,
                               bn.SThe,
                               vv.NgayVao,
                               rv.NgayRa,
                               rv.SoNgaydt,
                               rv.ChanDoan
                           }).ToList();
                _list = (from bn in qbn
                         select new BC
                             {
                                 MaBNhan = bn.MaBNhan,
                                 TenBNhan = bn.TenBNhan,
                                 MaKP = bn.MaKP,
                                 NSinhNam = bn.GTinh == 1 ? bn.NamSinh : "",
                                 NSinhNu = bn.GTinh == 0 ? bn.NamSinh : "",
                                 SThe = bn.SThe,
                                 NgayVao = bn.NgayVao,
                                 NgayRa = bn.NgayRa,
                                 SoNgayDT = bn.SoNgaydt,
                                 ChanDoan = bn.ChanDoan
                             }).ToList();

            }
            #endregion
            #region tìm theo ngày vào
            else
            {
                var qbn = (from vv in _data.VaoViens.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay)
                           join bn in _data.BenhNhans.Where(p => p.DTNT == true && p.NoiTru == 0) on vv.MaBNhan equals bn.MaBNhan
                           join rv in _data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq from kq1 in kq.DefaultIfEmpty()
                           select new
                           {
                               MaKP = kq1 != null ? kq1.MaKP : bn.MaKP,
                               bn.MaBNhan,
                               bn.TenBNhan,
                               bn.GTinh,
                               bn.NamSinh,
                               bn.SThe,
                               vv.NgayVao,
                               NgayRa = kq1 != null? kq1.NgayRa : null,
                               SoNgaydt = kq1 != null ? kq1.SoNgaydt : null
                           }).ToList();

                 var qkb = (from vv in _data.VaoViens.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay)
                                  join bnkb in _data.BNKBs on vv.MaBNhan equals bnkb.MaBNhan
                                  select new { bnkb.MaBNhan, bnkb.ChanDoan}).ToList();
                 var qkb2 = (from kb in qkb group kb by kb.MaBNhan into kq select new { MaBNhan = kq.Key.Value, ChanDoan = String.Join(";", kq.Where(p => !string.IsNullOrEmpty(p.ChanDoan)).Select(p => p.ChanDoan).Distinct()) }).ToList();


                _list = (from bn in qbn
                         join kb in qkb2 on bn.MaBNhan equals kb.MaBNhan
                         select new BC
                         {
                             MaBNhan = bn.MaBNhan,
                             TenBNhan = bn.TenBNhan,
                             MaKP = bn.MaKP ?? 0,
                             NSinhNam = bn.GTinh == 1 ? bn.NamSinh : "",
                             NSinhNu = bn.GTinh == 0 ? bn.NamSinh : "",
                             SThe = bn.SThe,
                             NgayVao = bn.NgayVao,
                             NgayRa = bn.NgayRa,
                             SoNgayDT = bn.SoNgaydt,
                             ChanDoan = kb.ChanDoan
                         }).ToList();
            }
            #endregion

            var qkp1 = _list.Select(p => p.MaKP).Distinct();
            var qkp2 = _data.KPhongs.ToList();
            var qkp3 = (from kp1 in qkp1 join kp2 in qkp2 on kp1 equals kp2.MaKP select new { kp2.MaKP, kp2.TenKP, kp2.PLoai }).OrderByDescending(p => p.PLoai).ToList();
           int count = 1;
            var qkp = (from kp in qkp3 select new { STT = count ++, kp.MaKP, kp.TenKP}).ToList();
            _list = (from l in _list
                     join kp in qkp on l.MaKP equals kp.MaKP
                     select new BC
                     {
                         MaBNhan = l.MaBNhan,
                         TenBNhan = l.TenBNhan,
                         MaKP = l.MaKP,
                         NSinhNam = l.NSinhNam,
                         NSinhNu = l.NSinhNu,
                         SThe = l.SThe,
                         NgayVao = l.NgayVao,
                         NgayRa = l.NgayRa,
                         SoNgayDT = l.SoNgayDT,
                         ChanDoan = DungChung.Ham.FreshString(l.ChanDoan),
                         TenKP = kp.TenKP,
                         STTNhom = kp.STT
                     }).OrderBy(p=>p.STTNhom).ToList();
            BaoCao.rep_BCBenhNhanDieuTriNgoaiTru rep = new BaoCao.rep_BCBenhNhanDieuTriNgoaiTru();
            frmIn frm = new frmIn();
            rep.celNgayThang.Text = "Từ ngày: " + lupNgaytu.DateTime.ToString().Substring(0, 10) + " Đến ngày: " + lupngayden.DateTime.ToString().Substring(0, 10);
            rep.DataSource = _list;
            rep.BindingData();
            rep.CreateDocument();
            //rep.DataMember = "Table";
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private class BC
        {
            public int MaBNhan { set; get; }
            public string TenBNhan { set; get; }
            public int MaKP { set; get; }
            public string TenKP { set; get; }
            public string NSinhNam { set; get; }
            public string NSinhNu { set; get; }
            public string SThe { set; get; }
            public DateTime?  NgayVao { set; get; }
            public DateTime? NgayRa { set; get; }

            public int? SoNgayDT { set; get; }
            public string ChanDoan { set; get; }
            public int STTNhom { set; get; }

        }
    }
}