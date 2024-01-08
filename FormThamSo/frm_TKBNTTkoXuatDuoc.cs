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
    public partial class frm_TKBNTTkoXuatDuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_TKBNTTkoXuatDuoc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnOK_Click(object sender, EventArgs e)
        {
            frmIn frm = new frmIn();
            bool _chk = true;
               int Makp =0;
            if (cboKhoaPhong.EditValue != null)
            {
                 Makp = (int)cboKhoaPhong.EditValue;
            }
           
            if (chkXuatDuoc.Checked == false)
                _chk = false;
            BaoCao.rep_TKBNTTkoXuatDuoc rep = new BaoCao.rep_TKBNTTkoXuatDuoc(_chk);
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            string _dtuong = "";
            if (!string.IsNullOrEmpty(cboDoiTuong.Text))
                _dtuong = cboDoiTuong.Text;
            tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);//dateTuNgay.DateTime;
            denngay = DungChung.Ham.NgayDen(lupNgayden.DateTime);//dateDenNgay.DateTime;
            List<DichVu> _ldv = _data.DichVus.ToList();
            var q3 = (from bn in _data.BenhNhans.Where(p => p.NoiTru == rdNoiTru.SelectedIndex).Where(p => p.DTuong == _dtuong)
                      join vp in _data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                      join vpct in _data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                      join kp in _data.KPhongs on bn.MaKP equals kp.MaKP
                      where (vp.NgayTT >= tungay && vp.NgayTT <= denngay)
                      select new { bn.TenBNhan, bn.MaBNhan, vpct.MaDV, vpct.ThanhTien, kp.MaKP, kp.TenKP }).ToList();
            if (chkXuatDuoc.Checked == false)
            {

                var q = (from bn in q3
                         join dv in _ldv on bn.MaDV equals dv.MaDV
                         group new { bn, dv } by new { bn.TenBNhan, bn.MaBNhan, bn.TenKP } into kq
                         select new { kq.Key.TenBNhan, kq.Key.MaBNhan, kq.Key.TenKP, TienThuoc = kq.Where(p => p.dv.PLoai == 1).Sum(p => p.bn.ThanhTien), TienDV = kq.Where(p => p.dv.PLoai == 2).Sum(p => p.bn.ThanhTien), ThanhTien = kq.Sum(p => p.bn.ThanhTien) }).ToList();
                rep.TieuDe.Value = ("Danh sách Bệnh Nhân có đơn thuốc đã thanh toán").ToUpper();
                rep.ThoiGian.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " đến ngày: " + denngay.ToString().Substring(0, 10);
                rep.DataSource = q.ToList().Where(p => p.TienThuoc > 0);
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                if (cboThanhToan.SelectedIndex == 0) // BN đã thanh toán  chưa xuất dược
                {
                    List<int> _lnhap = _data.NhapDs.Where(p => p.KieuDon == 0 || p.KieuDon == 4).Select(p => p.MaBNhan ?? 0).ToList();

                    var q4 = (from bn in q3 select bn.MaBNhan).Distinct().ToList();
                    var q2 = (from a in q4
                              where !(from vp in _lnhap select vp).Contains(a)
                              select a).ToList();
                    //var q2 = (from bn in q4.RemoveAll(p=>_lnhap.Contains(p.MaBNhan) )
                    //         //join nd in _lnhap on bn.MaBNhan equals nd.MaBNhan into dulieu

                    //          select new { bn.TenBNhan, bn.MaBNhan, bn.PLoai, bn.ThanhTien }).ToList();
                    var q = (from b in q2
                             join bn in q3 on b equals bn.MaBNhan
                             join dv in _ldv on bn.MaDV equals dv.MaDV
                             join kp in _data.KPhongs.Where(p=>(Makp == 0 ? true : p.MaKP == Makp)) on bn.MaKP equals kp.MaKP
                             group new { b, bn, dv } by new { bn.TenBNhan, bn.MaBNhan, bn.TenKP } into kq
                             select new { kq.Key.TenBNhan, kq.Key.MaBNhan, kq.Key.TenKP, TienThuoc = kq.Where(p => p.dv.PLoai == 1).Sum(p => p.bn.ThanhTien), TienDV = kq.Where(p => p.dv.PLoai == 2).Sum(p => p.bn.ThanhTien), ThanhTien = kq.Sum(p => p.bn.ThanhTien) }).ToList();
                    rep.TieuDe.Value = ("Danh sách Bệnh Nhân đã thanh toán chưa xuất dược").ToUpper();
                    rep.ThoiGian.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " đến ngày: " + denngay.ToString().Substring(0, 10);
                    rep.DataSource = q.ToList().Where(p => p.TienThuoc > 0);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    var q4 = (from bn in _data.BenhNhans.Where(p => p.NoiTru == 0)
                              join dt in _data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                              join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                              join kp in _data.KPhongs.Where(p=>(Makp == 0 ? true : p.MaKP == Makp)) on bn.MaKP equals kp.MaKP
                              where (bn.NNhap >= tungay && bn.NNhap <= denngay)
                              select new { bn.TenBNhan, bn.MaBNhan, PLoai = dt.PLDV, dtct.ThanhTien, kp.TenKP }).ToList();
                    List<int> _lvp = _data.VienPhis.Select(p => p.MaBNhan ?? 0).ToList();
                    var q5 = q4.Select(p => p.MaBNhan).Distinct().ToList();
                    var q2 = (from bn in q5
                              where !(from vp in _lvp select vp).Contains(bn)
                              select new { bn }).ToList();
                    var q = (from b in q2
                             join bn in q4 on b.bn equals bn.MaBNhan
                             group new { b, bn } by new { bn.TenBNhan, bn.MaBNhan, bn.TenKP } into kq
                             select new { kq.Key.TenBNhan, kq.Key.MaBNhan, kq.Key.TenKP, TienThuoc = kq.Where(p => p.bn.PLoai == 1).Sum(p => p.bn.ThanhTien), TienDV = kq.Where(p => p.bn.PLoai == 2).Sum(p => p.bn.ThanhTien), ThanhTien = kq.Sum(p => p.bn.ThanhTien) }).ToList();
                    rep.TieuDe.Value = ("Danh sách Bệnh Nhân đã kê đơn chưa thanh toán").ToUpper();
                    rep.ThoiGian.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " đến ngày: " + denngay.ToString().Substring(0, 10);
                    rep.DataSource = q.ToList().Where(p => p.TienThuoc > 0);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frm_TKBNTTkoXuatDuoc_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            KPhong();
        }

        private void chkXuatDuoc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkXuatDuoc.Checked)
            {
                cboThanhToan.Properties.ReadOnly = false;
            
            }
            else
            {
                cboThanhToan.SelectedIndex = 0;
                cboThanhToan.Properties.ReadOnly = true;
            }
            KPhong();
        }
        private class kphong
        {
            public int? MaKP { get; set; }
            public string TenKphong { get; set; }
        }


        private void cboThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {
            KPhong();
        }
        List<kphong> _listKPs = new List<kphong>();
        private void KPhong()
        {
            _listKPs.Clear();

            if (chkXuatDuoc.Checked)
            {
                if (cboThanhToan.SelectedIndex == 0)
                {
                    _listKPs = (from vp in _data.VienPhicts
                                join kp in _data.KPhongs on vp.MaKP equals kp.MaKP
                                select new kphong { MaKP = vp.MaKP, TenKphong = kp.TenKP }).Distinct().ToList();
                }
                else
                {
                    _listKPs = (from dt in _data.DThuocs
                                join kp in _data.KPhongs on dt.MaKP equals kp.MaKP
                                select new kphong { MaKP = dt.MaKP, TenKphong = kp.TenKP }).Distinct().ToList();
                }
            }
            else
            {
                _listKPs = (from vp in _data.VienPhicts
                            join kp in _data.KPhongs on vp.MaKP equals kp.MaKP
                            select new kphong { MaKP = vp.MaKP, TenKphong = kp.TenKP }).Distinct().ToList();
            }
            _listKPs.Insert(0, new kphong { MaKP = 0, TenKphong = "Tất cả" });
            cboKhoaPhong.Properties.DataSource = _listKPs.OrderBy(p => p.MaKP).ToList();
            cboKhoaPhong.EditValue = 0;
        }
    }
}