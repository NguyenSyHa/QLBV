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
    public partial class frm_80bHDDD : DevExpress.XtraEditors.XtraForm
    {
        public frm_80bHDDD()
        {
            InitializeComponent();
        }

        private void frm_80bHD_Load(object sender, EventArgs e)
        {
            lupNgayden.DateTime = System.DateTime.Now;
            lupNgaytu.DateTime = System.DateTime.Now;
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ok_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            //var q = (from bn in _Data.BenhNhans.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 1)
            //         join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
            //         join vpct in _Data.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
            //         join vv in _Data.VaoViens on bn.MaBNhan equals vv.MaBNhan
            //         join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
            //         where (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden&&vp.Duyet==2)
            //         group new { bn, rv, vpct,  vp } by new { bn.NoiTinh, vp.MaBNhan, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.MaCS, bn.Tuyen, vv.NgayVao, rv.NgayRa, rv.SoNgaydt, vp.MaKP, rv.MaICD,vp.LyDo} into kq
            //         select new
            //         {
            //             NoiTinh = kq.Key.NoiTinh,
            //             Tuyen = kq.Key.Tuyen,
            //             MaBNhan = kq.Key.MaBNhan,
            //             TenBNhan = kq.Key.TenBNhan,
            //             SThe = kq.Key.SThe,
            //             Ngayvao = kq.Key.NgayVao,
            //             Ngayra = kq.Key.NgayRa,
            //             Songay = kq.Key.SoNgaydt,
            //             Lydo=kq.Key.LyDo,
            //             Tongcong = kq.Where(p => p.vpct.Duyet != 1).Where(p => p.vpct.Duyet != 0).Sum(p => p.vpct.TienChenh),
            //             Nguoibenhchitra = kq.Sum(p => p.vpct.TienBN) - kq.Sum(p => p.vpct.TienChenhBN),
            //             TongcongBHYT = kq.Sum(p => p.vpct.TienBH) - kq.Sum(p => p.vpct.TienChenh),

            //         }).OrderByDescending(p => p.NoiTinh).OrderBy(p => p.Tuyen).ToList();

            var q1 = (from bn in _Data.BenhNhans.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 1)
                      join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                      join vpct in _Data.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                      join vv in _Data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                      join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                      where (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden && vp.Duyet == 2)
                      //group new { bn, rv, vpct, vp } by new { bn.NoiTinh, vp.MaBNhan, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.MaCS, bn.Tuyen, vv.NgayVao, rv.NgayRa, rv.SoNgaydt, vp.MaKP, rv.MaICD, vp.LyDo } into kq
                      select new
                      {
                          bn.NoiTinh,
                          vp.MaBNhan,
                          bn.TenBNhan,
                          bn.NamSinh,
                          bn.GTinh,
                          bn.SThe,
                          bn.MaCS,
                          bn.Tuyen,
                          vv.NgayVao,
                          rv.NgayRa,
                          rv.SoNgaydt,
                          vp.MaKP,
                          rv.MaICD,
                          vp.LyDo,
                          vpct.Duyet,
                          vpct.TienChenh,
                          vpct.TienChenhBN, vpct.TienBH, vpct.TienBN

                      }).OrderByDescending(p => p.NoiTinh).OrderBy(p => p.Tuyen).ToList();
            var q = (from b in q1
                      group new { b } by new { b.NoiTinh, b.MaBNhan, b.TenBNhan, b.NamSinh, b.GTinh, b.SThe, b.MaCS, b.Tuyen, b.NgayVao, b.NgayRa, b.SoNgaydt, b.MaKP, b.MaICD, b.LyDo } into kq
                      select new
                      {
                          NoiTinh = kq.Key.NoiTinh,
                          Tuyen = kq.Key.Tuyen,
                          MaBNhan = kq.Key.MaBNhan,
                          TenBNhan = kq.Key.TenBNhan,
                          SThe = kq.Key.SThe,
                          Ngayvao = kq.Key.NgayVao,
                          Ngayra = kq.Key.NgayRa,
                          Songay = kq.Key.SoNgaydt,
                          Lydo = kq.Key.LyDo,
                          Tongcong = kq.Where(p => p.b.Duyet != 1).Where(p => p.b.Duyet != 0).Sum(p => p.b.TienChenh),
                          Nguoibenhchitra = kq.Sum(p => p.b.TienBN) - kq.Sum(p => p.b.TienChenhBN),
                          TongcongBHYT = kq.Sum(p => p.b.TienBH) - kq.Sum(p => p.b.TienChenh),
                      }).ToList();

            if (q.Count > 0)
            {
                BaoCao.rep_80bHDDD rep = new BaoCao.rep_80bHDDD();
                rep.ngayden.Value = lupNgayden.DateTime;
                rep.Ngaytu.Value = lupNgaytu.DateTime;
                rep.DataSource = q.ToList();
                rep.BindingData();
                rep.CreateDocument();
                frmIn frm = new frmIn();
                rep.DataMember = "Table";
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                BaoCao.rep_80bHDDD rep = new BaoCao.rep_80bHDDD();
                rep.ngayden.Value = lupNgayden.DateTime;
                rep.Ngaytu.Value = lupNgaytu.DateTime;
                //rep.DataSource = q.ToList();
                //rep.BindingData();
                rep.CreateDocument();
                frmIn frm = new frmIn();
                rep.DataMember = "Table";
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}