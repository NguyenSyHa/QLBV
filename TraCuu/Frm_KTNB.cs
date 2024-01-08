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
    public partial class Frm_KTNB : DevExpress.XtraEditors.XtraForm
    {
        public Frm_KTNB()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_KTNB_Load(object sender, EventArgs e)
        {
            lupNgayVao.DateTime = System.DateTime.Now;
            LupNgayRa.DateTime = System.DateTime.Now;
        }
        private void sbtKiemtra_Click(object sender, EventArgs e)
        {
            DateTime NT = DungChung.Ham.NgayTu(lupNgayVao.DateTime);
            DateTime ND = DungChung.Ham.NgayDen(LupNgayRa.DateTime);
            var q = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1).Where(p => p.DTuong == "BHYT").Where(p=>p.NNhap>=NT)
                     join rv in _Data.RaViens.Where(p => p.NgayRa <= ND) on bn.MaBNhan equals rv.MaBNhan
                     select new { bn.MaBNhan, bn.TenBNhan, bn.SThe, bn.DChi, bn.Tuoi, rv.NgayRa, bn.NNhap }).ToList();
            var q2 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 0).Where(p => p.DTuong == "BHYT")
                      join rv in _Data.RaViens.Where(p => p.NgayRa >= NT && p.NgayRa <= ND) on bn.MaBNhan equals rv.MaBNhan
                      select new { bn.MaBNhan, bn.TenBNhan, bn.SThe, bn.DChi, bn.Tuoi, rv.NgayRa, bn.NNhap }).ToList();
            var q3 = (from a in q
                      join b in q2 on a.SThe equals b.SThe
                      where (b.NNhap <= a.NgayRa && b.NNhap >= a.NNhap)
                      select new { b.MaBNhan, b.DChi, b.NNhap, b.NgayRa, b.SThe, b.TenBNhan, b.Tuoi}).ToList();
            GrcDSBN.DataSource = q3.OrderBy(p=>p.NNhap);
        }
    }
}