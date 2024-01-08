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
    public partial class Frm_TkCPVanChuyenBHYT_YS : DevExpress.XtraEditors.XtraForm
    {
        public Frm_TkCPVanChuyenBHYT_YS()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
   
        private bool KTtaoBc()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }
        List<KPhong> _lkp = new List<KPhong>();
        private void Frm_TkCPVanChuyenBHYT_YS_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            _lkp = _dataContext.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).ToList();
            _lkp.Add(new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKP.Properties.DataSource = _lkp.OrderBy(p => p.MaKP).ToList();
            lupKP.EditValue = 0;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (KTtaoBc())
            {
                int _noitru = -1, _ngoaitru = -1;
                if (radNoiNgoaiTru.SelectedIndex == 2)
                {
                    _noitru = 1;
                    _ngoaitru = 0;
                }
                else
                {
                    _ngoaitru = radNoiNgoaiTru.SelectedIndex;
                    _noitru = radNoiNgoaiTru.SelectedIndex;
                }
                string _BHYT = "",_Dichvu="";
                if (radioDTuong.SelectedIndex == 2)
                {
                    _BHYT = "BHYT";
                    _Dichvu = "Dịch vụ";
                }
                else
                {
                    if (radioDTuong.SelectedIndex == 1)
                        _Dichvu = "Dịch vụ";
                    else
                    _BHYT = "BHYT";
                }
                int makp = 0;
                if (lupKP.EditValue != null)
                    makp = Convert.ToInt32(lupKP.EditValue);
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                 frmIn frm = new frmIn();
                BaoCao.Rep_TkCPVanChuyenBHYT_YS rep = new BaoCao.Rep_TkCPVanChuyenBHYT_YS();
                rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
               Array   bv = (from b in _dataContext.BenhViens select new { b.MaBV, b.TenBV }).ToArray();
                var dichvu = (from dv in _dataContext.DichVus
                              join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                              join nhom in _dataContext.NhomDVs.Where(p => p.TenNhomCT == ("Vận chuyển")) on tn.IDNhom equals nhom.IDNhom
                              select dv.MaDV).ToList();

                var q2 = (from bn in _dataContext.BenhNhans
                          join rv in _dataContext.RaViens.Where(p => makp == 0 ? true : p.MaKP == makp).Where(p => p.Status == 1) on bn.MaBNhan equals rv.MaBNhan
                          join vp in _dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan
                          join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                          where radTimKiem.SelectedIndex == 0 ? (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden) : (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                          select new { bn.DTuong, bn.NoiTru, bn.TenBNhan, bn.MaBNhan, bn.NoiTinh, bn.MaCS, bn.SThe, rv.MaBVC, rv.SoGT, vpct.MaDV, vpct.SoLuong, vpct.ThanhTien, rv.MaKP, rv.NgayRa }).ToList();
                var q = (from bn in q2.Where(p => p.DTuong == _BHYT || p.DTuong == _Dichvu).Where(p => p.NoiTru == _ngoaitru || p.NoiTru == _noitru)
                         join dv in dichvu on bn.MaDV equals dv
                         join kp in _lkp on bn.MaKP equals kp.MaKP
                         group new { bn, kp } by new { bn.MaBNhan, bn.NoiTinh, bn.TenBNhan, bn.SThe, bn.MaCS, bn.MaBVC, bn.SoGT, kp.MaKP, kp.TenKP, bn.NgayRa } into kq
                         select new
                         {
                             kq.Key.SoGT,
                             NoiTinh = kq.Key.NoiTinh,
                             Mabn = kq.Key.MaBNhan,
                             TenBNhan = kq.Key.TenBNhan,
                             SThe = kq.Key.SThe,
                             MaCS = kq.Key.MaCS,
                             MaBVC = kq.Key.MaBVC,
                             SoLuong = kq.Sum(p => p.bn.SoLuong),
                             ThanhTien = kq.Sum(p => p.bn.ThanhTien),
                             kq.Key.TenKP,
                             NgayRa = kq.Key.NgayRa.Value.ToShortDateString()
                         }).OrderBy(p => p.TenBNhan).ToList();
                    rep.DataSource = q.OrderBy(p => p.TenBNhan);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
               
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }
    }
}