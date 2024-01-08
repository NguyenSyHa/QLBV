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
    public partial class frm_BC_TKThuocTTBHYT_30009 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_TKThuocTTBHYT_30009()
        {
            InitializeComponent();
        }

        private void frm_BC_TKThuocTTBHYT_30009_Load(object sender, EventArgs e)
        {
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool KTBC()
        {
            if (dtTuNgay.EditValue == null)
            {
                MessageBox.Show("Từ ngày không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtTuNgay.Focus();
                return false;
            }
            if (dtDenNgay.EditValue == null)
            {
                MessageBox.Show("Đến ngày không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtDenNgay.Focus();
                return false;
            }
            if (dtDenNgay.EditValue != null && Convert.ToDateTime(dtDenNgay.DateTime) < Convert.ToDateTime(dtTuNgay.DateTime))
            {
                MessageBox.Show("Đến ngày không được nhỏ hơn Từ ngày.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtDenNgay.Focus();
                return false;
            }
            if (!ckBHYT.Checked && !ckDichVu.Checked)
            {
                MessageBox.Show("Chưa chọn đối tượng BN.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            int noitru = -1;
            if (radDT.SelectedIndex == 0)
                noitru = 0;
            if (radDT.SelectedIndex == 1)
                noitru = 1;
            if (radDT.SelectedIndex == 2)
                noitru = 2;
            if (KTBC())
            {
                var qvp = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                           join vpct in data.VienPhicts.Where(p => p.TrongBH == rdTrongBH.SelectedIndex) on vp.idVPhi equals vpct.idVPhi
                           select new { vp.MaBNhan, vpct.MaDV, vpct.SoLuong, vpct.ThanhTien, vpct.DonGia }).ToList();

                var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 1).Where(p => p.IDNhom == 4 || p.IDNhom == 6)
                           join dd in data.DuongDungs on dv.MaDuongDung equals dd.MaDuongDung into qdd
                           from qdd1 in qdd.DefaultIfEmpty()
                           join dvex in data.DichVuExes on dv.MaDV equals dvex.MaDV into qex
                           from qex1 in qex.DefaultIfEmpty()
                           select new { MaHC = qex1 == null ? "" : qex1.MaHC, dv.TenHC, MaDuongDung = qdd1 == null ? "" : qdd1.MaDuongDung, dv.DuongD, dv.HamLuong, dv.TenDV, dv.SoDK, dv.DonVi, dv.MaTam, dv.MaDV, dv.QCPC }).ToList();

                var qbn = (from rv in data.RaViens
                           join bn in data.BenhNhans.Where(p => (ckBHYT.Checked && p.DTuong.Equals("BHYT")) || (ckDichVu.Checked && p.DTuong.Equals("Dịch vụ"))) on rv.MaBNhan equals bn.MaBNhan
                           group bn by new { bn.NoiTru, bn.MaKCB, bn.MaBNhan, bn.DTuong } into a
                           select new { a.Key.NoiTru, a.Key.MaKCB, a.Key.MaBNhan, a.Key.DTuong }).Where(p => noitru == 2 || p.NoiTru == radDT.SelectedIndex).ToList();

                var query1 = (from a in qvp
                              join b in qdv on a.MaDV equals b.MaDV
                              group new { a, b } by new { a.MaBNhan, a.MaDV, a.SoLuong, a.DonGia, a.ThanhTien, b.MaHC, b.TenHC, b.MaDuongDung, b.DuongD, b.HamLuong, b.TenDV, b.SoDK, b.DonVi, b.MaTam, b.QCPC } into kq
                              select new
                              {
                                  kq.Key.MaBNhan,
                                  kq.Key.MaDV,
                                  kq.Key.SoLuong,
                                  kq.Key.DonGia,
                                  kq.Key.MaHC,
                                  kq.Key.TenHC,
                                  kq.Key.MaDuongDung,
                                  kq.Key.DuongD,
                                  kq.Key.HamLuong,
                                  kq.Key.TenDV,
                                  kq.Key.SoDK,
                                  kq.Key.DonVi,
                                  kq.Key.MaTam,
                                  kq.Key.QCPC,
                                  kq.Key.ThanhTien
                              }).ToList();

                var query = (from n in qbn
                             join m in query1 on n.MaBNhan equals m.MaBNhan
                             group new { n, m } by new
                             {
                                 n.MaKCB,
                                 m.DonGia,
                                 m.MaHC,
                                 m.TenHC,
                                 m.MaDuongDung,
                                 m.DuongD,
                                 m.HamLuong,
                                 m.TenDV,
                                 m.SoDK,
                                 m.DonVi,
                                 m.MaTam,
                                 m.QCPC
                             } into q
                             select new
                             {
                                 q.Key.MaKCB,
                                 SoLuong = q.Where(p => p.n.NoiTru == 1 || p.n.NoiTru == 0).Sum(p => p.m.SoLuong),
                                 q.Key.DonGia,
                                 q.Key.MaHC,
                                 q.Key.TenHC,
                                 q.Key.MaDuongDung,
                                 q.Key.DuongD,
                                 q.Key.HamLuong,
                                 q.Key.TenDV,
                                 q.Key.SoDK,
                                 q.Key.DonVi,
                                 q.Key.MaTam,
                                 q.Key.QCPC,
                                 ThanhTien = q.Sum(p => p.m.ThanhTien),
                                 NoiTruSL = q.Where(p => p.n.NoiTru == 1).Sum(p => p.m.SoLuong) == 0 ? "" : q.Where(p => p.n.NoiTru == 1).Sum(p => p.m.SoLuong).ToString(),
                                 NgoaiTruSL = q.Where(p => p.n.NoiTru == 0).Sum(p => p.m.SoLuong) == 0 ? "" : q.Where(p => p.n.NoiTru == 0).Sum(p => p.m.SoLuong).ToString()
                             }).Where(p => p.SoLuong != 0).OrderBy(p => p.TenDV).ToList();

                BaoCao.Rep_BC_TKThuocTTBHYT_30009 rep = new BaoCao.Rep_BC_TKThuocTTBHYT_30009();
                frmIn frm = new frmIn();
                if (ckBHYT.Checked)
                    rep.lblTieuDe.Text = "THỐNG KÊ THUỐC THANH TOÁN BHYT";
                if (ckDichVu.Checked)
                    rep.lblTieuDe.Text = "THỐNG KÊ THUỐC THANH TOÁN DỊCH VỤ";
                if (ckDichVu.Checked && ckBHYT.Checked)
                    rep.lblTieuDe.Text = "THỐNG KÊ THUỐC THANH TOÁN BHYT VÀ DỊCH VỤ";
                rep.lblThang.Text = "Tháng " + denngay.Month + " năm " + denngay.Year;
                rep.DataSource = query.ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }
    }
}