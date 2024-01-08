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
    public partial class frm_SoChungSinh : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoChungSinh()
        {
            InitializeComponent();
        }

        private void frm_SoChungSinh_Load(object sender, EventArgs e)
        {
            detungay.DateTime = DateTime.Now;
            dedenngay.DateTime = DateTime.Now;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btntaobc_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(detungay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dedenngay.DateTime);
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qkq = (from cs in _data.TheoDoiThaiSans.Where(p => p.ThoiGianSinh != null && p.ThoiGianSinh >= tungay && p.ThoiGianSinh <= denngay)
                       join bn in _data.BenhNhans on cs.MaBNhan equals bn.MaBNhan
                       join ttbx in _data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan into kq
                       from kq1 in kq.DefaultIfEmpty()
                       select new { bn.MaBNhan, bn.TenBNhan, bn.NamSinh, HoKhau = bn.DChi, bn.SThe, SoCMT = kq1 == null ? "" : kq1.SoKSinh, NgayCap = (kq1 != null) ? kq1.NgayCapCMT : null, NoiCap = (kq1 != null) ? kq1.NoiCapCMT : null, ThoiGianSinh = cs.ThoiGianSinh, TenBo = cs.TenBo, TenCon1 = cs.Ten1, TenCon2 = cs.Ten2, TenCon3 = cs.Ten3, TenCon4 = cs.Ten4, GTinh1 = cs.GioiTinhCon1, GTinh2 = cs.GioiTinhCon2, GTinh3 = cs.GioiTinhCon3, GTinh4 = cs.GioiTinhCon4, CanNang1 = cs.CanNang1, CanNang2 = cs.CanNang2, CanNang3 = cs.CanNang3, CanNang4 = cs.CanNang4, cs.GhiChu, cs.SoCon }).ToList();
            var qkq2 = (from bn in qkq select new { bn.MaBNhan, bn.TenBNhan, bn.NamSinh, HoKhau = bn.HoKhau, bn.SThe, SoCMT = bn.SoCMT, NgayCap = bn.NgayCap == null ? "" : bn.NgayCap.Value.ToString("dd/MM/yy"), NoiCap = bn.NoiCap, ThoiGianSinh = bn.ThoiGianSinh == null ? "" : bn.ThoiGianSinh.Value.ToString("dd/MM/yy HH:mm"), TenBo = bn.TenBo, TenCon1 = bn.TenCon1 ?? "", TenCon2 = bn.TenCon2 ?? "", TenCon3 = bn.TenCon3 ?? "", TenCon4 = bn.TenCon4 ?? "", GTinh1 = bn.GTinh1 == 1 ? "Nam" : (bn.GTinh1 == 0 ? "Nữ" : "KXĐ"), GTinh2 = bn.GTinh2 == 1 ? "Nam" : (bn.GTinh2 == 0 ? "Nữ" : "KXĐ"), GTinh3 = bn.GTinh3 == 1 ? "Nam" : (bn.GTinh3 == 0 ? "Nữ" : "KXĐ"), GTinh4 = bn.GTinh4 == 1 ? "Nam" : (bn.GTinh4 == 0 ? "Nữ" : "KXĐ"), CanNang1 = bn.CanNang1 == null ? "" : bn.CanNang1.ToString(), CanNang2 = bn.CanNang2 == null ? "" : bn.CanNang2.ToString(), CanNang3 = bn.CanNang3 == null ? "" : bn.CanNang3.ToString(), CanNang4 = bn.CanNang4 == null ? "" : bn.CanNang4.ToString(), bn.GhiChu, SoCon = bn.SoCon ?? 0 }).ToList();
            var qkq3 = (from bn in qkq2
                        select new
                        {
                            bn.MaBNhan,
                            bn.TenBNhan,
                            bn.NamSinh,
                            HoKhau = bn.HoKhau,
                            bn.SThe,
                            SoCMT = bn.SoCMT,
                            NgayCap = bn.NgayCap,
                            NoiCap = bn.NoiCap,
                            ThoiGianSinh = bn.ThoiGianSinh,
                            TenBo = bn.TenBo,
                            bn.TenCon1,
                            TenCon2 = bn.TenCon2,
                            TenCon3 = bn.TenCon3,
                            TenCon4 = bn.TenCon4,
                            GTinh1 = bn.GTinh1,
                            GTinh2 = bn.GTinh2,
                            GTinh3 = bn.GTinh3,
                            GTinh4 = bn.GTinh4,
                            CanNang1 = bn.CanNang1,
                            CanNang2 = bn.CanNang2,
                            CanNang3 = bn.CanNang3,
                            CanNang4 = bn.CanNang4,
                            bn.GhiChu,
                            bn.SoCon,
                            TenCon = bn.SoCon == 0 ? "" : (bn.SoCon == 1 ? bn.TenCon1 : (bn.SoCon == 2 ? (bn.TenCon1 + Environment.NewLine + bn.TenCon2) : (bn.SoCon == 3 ? (bn.TenCon1 + Environment.NewLine + bn.TenCon2 + Environment.NewLine + bn.TenCon3) : (bn.SoCon == 4 ? (bn.TenCon1 + Environment.NewLine + bn.TenCon2 + Environment.NewLine + bn.TenCon3 + Environment.NewLine + bn.TenCon4) : "")))),
                            GTinh = bn.SoCon == 0 ? "" : (bn.SoCon == 1 ? bn.GTinh1 : (bn.SoCon == 2 ? (bn.GTinh1 + Environment.NewLine + bn.GTinh2) : (bn.SoCon == 3 ? (bn.GTinh1 + Environment.NewLine + bn.GTinh2 + Environment.NewLine + bn.GTinh3) : (bn.SoCon == 4 ? (bn.GTinh1 + Environment.NewLine + bn.GTinh2 + Environment.NewLine + bn.GTinh3 + Environment.NewLine + bn.GTinh4) : "")))),
                            CNang = bn.SoCon == 0 ? "" : (bn.SoCon == 1 ? bn.CanNang1 : (bn.SoCon == 2 ? (bn.CanNang1 + Environment.NewLine + bn.CanNang2) : (bn.SoCon == 3 ? (bn.CanNang1 + Environment.NewLine + bn.CanNang2 + Environment.NewLine + bn.CanNang3) : (bn.SoCon == 4 ? (bn.CanNang1 + Environment.NewLine + bn.CanNang2 + Environment.NewLine + bn.CanNang3 + Environment.NewLine + bn.CanNang4) : "")))),
                        }).ToList();

            BaoCao.rep_SoChungSinh rep = new BaoCao.rep_SoChungSinh();
            frmIn frm = new frmIn();
            rep.celNgayThang.Text = "Từ ngày: " + detungay.DateTime.ToString().Substring(0, 10) + " Đến ngày: " + dedenngay.DateTime.ToString().Substring(0, 10);
            rep.DataSource = qkq3;
            rep.BindingData();
            rep.CreateDocument();
            //rep.DataMember = "Table";
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}