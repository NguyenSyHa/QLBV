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
    public partial class frm_BC_HoatDongDTNoiTru : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_HoatDongDTNoiTru()
        {
            InitializeComponent();
            date_tungay.DateTime = DateTime.Now;
            date_ngayden.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(date_tungay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(date_ngayden.DateTime);

            var bn = (from a in data.BenhNhans.Where(p => p.NoiTru == 1)
                      join b in data.VaoViens on a.MaBNhan equals b.MaBNhan
                      join c in data.RaViens on b.MaBNhan equals c.MaBNhan into kq
                      join d in data.KPhongs on b.MaKP equals d.MaKP
                      from kq1 in kq.DefaultIfEmpty()
                      select new
                      {
                          a.MaBNhan,
                          a.Tuoi,
                          a.CapCuu,
                          a.DTuong,
                          b.NgayVao,
                          NgayRa = kq1 == null ? null : kq1.NgayRa,
                          SoNgaydt = kq1 == null ? 0 : kq1.SoNgaydt,
                          KetQua = kq1 == null ? "" : kq1.KetQua,
                          d.TenKP
                      }).ToList();

            var _bn = (from a in bn
                       group a by new {a.TenKP } into kq
                       select new
                       {
                           kq.Key.TenKP,

                           dauky = kq.Where(p => p.NgayVao < tungay && ( p.NgayRa >= tungay || p.NgayRa == null)).Count(),
                           tongdt = kq.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Count(),
                           treemnt = kq.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Where(p => p.Tuoi < 15).Where(p => p.CapCuu != 1).Count(),
                           capcuunt = kq.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Where(p => p.CapCuu == 1).Count(),
                           songaydt = kq.Where(p => p.NgayRa <= denngay && p.NgayRa >= tungay).Sum(p => p.SoNgaydt),
                           tongtuvong = kq.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua.Contains("tử vong")).Count(),
                           treemtv = kq.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua.Contains("tử vong")).Where(p => p.Tuoi < 15).Count(),
                           tvtruoc24h = kq.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua.Contains("tử vong")).Where(p => (p.NgayRa.Value - p.NgayVao.Value).Hours < 24).Count(),
                           bhyt = kq.Where(p => p.NgayVao <= denngay).Where(p => p.DTuong == "BHYT").Count(),
                           conlai = kq.Where(p => p.NgayVao <= denngay && ( p.NgayRa > denngay || p.NgayRa == null )).Count(),
                           songaydttb = ( kq.Where(p => p.NgayRa <= denngay && p.NgayRa >= tungay).Count() >0 ) ? (kq.Where(p => p.NgayRa <= denngay && p.NgayRa >= tungay).Sum(p => p.SoNgaydt) / kq.Where(p => p.NgayRa <= denngay && p.NgayRa >= tungay).Count()) : 0
                       }).OrderBy(p => p.TenKP).ToList();

            BaoCao.rep_BC_HoatDongDTNoiTru rep = new BaoCao.rep_BC_HoatDongDTNoiTru();
            frmIn frm = new frmIn();
            rep.Ngaytu.Value = date_tungay.Text;
            rep.Ngayden.Value = date_ngayden.Text;
            rep.DTTB.Value =(_bn.Count() != 0)? (_bn.Sum(p => p.songaydttb) / _bn.Count()) : 0;
            rep.DataSource = _bn;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }


    }
}