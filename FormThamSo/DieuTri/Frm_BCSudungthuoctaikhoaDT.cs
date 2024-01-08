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
    public partial class Frm_BCSudungthuoctaikhoaDT : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BCSudungthuoctaikhoaDT()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KTtaoBcMau20()
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
        List<NhomDV> _lnhom = new List<NhomDV>();
        List<KPhong> _lKP = new List<KPhong>();
        private void Frm_BCSudungthuoctaikhoaDT_Load(object sender, EventArgs e)
        {
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTuNgay.DateTime = System.DateTime.Now;
            var kp = (from k in data.KPhongs where (k.PLoai.Contains("Lâm sàng") || k.PLoai.Contains("Phòng khám")) select k).ToList();
            _lKP = kp.ToList();
            _lKP.Add(new KPhong { TenKP = " Tất cả", MaKP =0 });
            lupMaKP.Properties.DataSource = _lKP.ToList().OrderBy(p=>p.TenKP);
            radTimKiem.SelectedIndex = 1;
            radNoiTru.SelectedIndex = 1;
            _lnhom=data.NhomDVs.Where(p => p.Status == 1 || p.Status==2).ToList();
            _lnhom.Add(new NhomDV { TenNhom = " Tất cả", IDNhom = 0 });
            lupNhomDuoc.Properties.DataSource = _lnhom.OrderBy(p=>p.TenNhom);
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            int _makp = 0;
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            int _noitru = -1, _a1=-2, _a2=-2, _a3=-2;
            int trongBH = 0;
            string _doituong = "";
            if (lupMaKP.EditValue != null )
                _makp =Convert.ToInt32( lupMaKP.EditValue);
            if (!string.IsNullOrEmpty(cboDoiTuong.Text))
            {
                _doituong = cboDoiTuong.Text;
            }
            
            if (KTtaoBcMau20())
            {
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                _noitru = radNoiTru.SelectedIndex;
                string _nhomdv = "THUỐC - DỊCH VỤ";
                int idNhom1 = 0;
                if (lupNhomDuoc.EditValue != null && lupNhomDuoc.EditValue.ToString() != "" && lupNhomDuoc.EditValue.ToString() != "0")
                {
                    _nhomdv = lupNhomDuoc.Text.ToUpper();
                idNhom1 = Convert.ToInt16(lupNhomDuoc.EditValue);}
                if (radTimKiem.SelectedIndex == 0)
                {
                     var q2 = (from rv in data.RaViens.Where(p => p.NgayRa <= ngayden).Where(p => p.NgayRa >= ngaytu)
                             join bn in data.BenhNhans.Where(p => p.DTuong == _doituong).Where(p => _noitru == 1 ? (p.NoiTru == 1) : (_noitru == 0 ? (p.NoiTru == 0 && p.DTNT == false) : (p.NoiTru == 0 && p.DTNT == true))) on rv.MaBNhan equals bn.MaBNhan
                             join vp in data.DThuocs on bn.MaBNhan equals vp.MaBNhan
                             join vpct in data.DThuoccts.Where(p=>p.TrongBH==radTrongDM.SelectedIndex) on vp.IDDon equals vpct.IDDon
                             join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                               select new
                               {
                                   vpct.MaKP,
                                   dv.TenDV,
                                   vpct.DonGia,
                                   vpct.DonVi,
                                   vpct.SoLuong,
                                   vpct.ThanhTien,
                                   tn.IDNhom,
                                   dv.MaDV
                               }).ToList();
                     var q = (from a in q2
                              group a by new {a.MaKP, a.IDNhom, a.MaDV, a.TenDV, a.DonVi, a.DonGia } into kq
                             select new
                             {
                               kq.Key.MaKP,
                                 IdNhom = kq.Key.IDNhom,
                                 //TenNhomThuoc = kq.Key.TenNhom,
                                 TenThuoc = kq.Key.TenDV,
                                 //HamLuong = kq.Key.HamLuong,
                                 DonVi = kq.Key.DonVi,
                                 //SoDK = kq.Key.SoDK,
                                 MaDV = kq.Key.MaDV,
                                 //NoiTru = kq.Key.NoiTru,
                                 //TrongBH = kq.Key.TrongBH,
                                 DonGia = kq.Key.DonGia,
                                 SoLuong = kq.Sum(p => p.SoLuong),
                                 ThanhTien = kq.Sum(p => p.ThanhTien)
                             }).ToList();
                    frmIn frm = new frmIn();
                    BaoCao.Rep_BCSudungthuockhoaDT rep = new BaoCao.Rep_BCSudungthuockhoaDT();
                   
                    rep.TieuDe.Value = "BÁO CÁO SỬ DỤNG " + _nhomdv + " TẠI KHOA ĐIỀU TRỊ";
                    rep.paramKhoaPhong.Value = lupMaKP.Text.ToUpper();
                    rep.Ngaythang.Value = "Từ ngày  " + lupTuNgay.DateTime.ToString().Substring(0, 10) + "  đến ngày  " + lupDenNgay.DateTime.ToString().Substring(0, 10);
                    if (idNhom1 == 0)
                    {
                        if (_makp>0)
                            rep.DataSource = q.Where(p => p.MaKP == _makp).ToList();
                        else { 
                            var q3=(from a in q group a by new {a.MaDV,a.DonGia,a.DonVi,a.TenThuoc} into kq
                                   select new
                                   {
                                   kq.Key.DonGia, kq.Key.DonVi,kq.Key.MaDV,kq.Key.TenThuoc
                                   , SoLuong=kq.Sum(p=>p.SoLuong),
                                   ThanhTien=kq.Sum(p=>p.ThanhTien),
                                   }).ToList();
                            rep.DataSource = q3.OrderBy(p => p.TenThuoc);
                        }
                    }
                    else
                    {
                        if (_makp>0)
                            rep.DataSource = q.Where(p => p.IdNhom == idNhom1 && p.MaKP == _makp).ToList();
                        else {
                            var q3 = (from a in q.Where(p=>p.IdNhom==idNhom1)
                                      group a by new { a.MaDV, a.DonGia, a.DonVi, a.TenThuoc } into kq
                                      select new
                                      {
                                          kq.Key.DonGia,
                                          kq.Key.DonVi,
                                          kq.Key.MaDV,
                                          kq.Key.TenThuoc
                                      ,
                                          SoLuong = kq.Sum(p => p.SoLuong),
                                          ThanhTien = kq.Sum(p => p.ThanhTien),
                                      }).ToList();
                            rep.DataSource = q3.OrderBy(p=>p.TenThuoc);
                        }
                    }
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    var q2 = (from   vphi in data.VienPhis.Where(p => p.NgayTT <= ngayden).Where(p => p.NgayTT >= ngaytu)
                              join bn in data.BenhNhans.Where(p => p.DTuong == _doituong).Where(p => _noitru == 1 ? (p.NoiTru == 1) : (_noitru == 0 ? (p.NoiTru == 0 && p.DTNT == false) : (p.NoiTru == 0 && p.DTNT == true))) on vphi.MaBNhan equals bn.MaBNhan
                             join vp in data.DThuocs on bn.MaBNhan equals vp.MaBNhan
                             join vpct in data.DThuoccts.Where(p=>p.TrongBH==radTrongDM.SelectedIndex) on vp.IDDon equals vpct.IDDon
                              join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             select new{
                                 vpct.MaKP,
                                 dv.TenDV,
                                 vpct.DonGia,
                                 vpct.DonVi,
                                 vpct.SoLuong,
                                 vpct.ThanhTien,
                                 tn.IDNhom,
                                 dv.MaDV
                             }).ToList();
                    var q = (from   a in  q2
                             group a by new {a.MaKP,  a.IDNhom, a.MaDV, a.TenDV, a.DonVi, a.DonGia} into kq
                             select new
                             {
                                 kq.Key.MaKP,
                                 IdNhom = kq.Key.IDNhom,
                                // TenNhomThuoc = kq.Key.TenNho,
                                 TenThuoc = kq.Key.TenDV,
                                 //HamLuong = kq.Key.HamLuong,
                                 DonVi = kq.Key.DonVi,
                                 //SoDK = kq.Key.SoDK,
                                 MaDV = kq.Key.MaDV,
                                 //NoiTru = kq.Key.NoiTru,
                                 //TrongBH = kq.Key.TrongBH,
                                 DonGia = kq.Key.DonGia,
                                 SoLuong = kq.Sum(p => p.SoLuong),
                                 ThanhTien = kq.Sum(p => p.ThanhTien)
                             }).ToList();
                    frmIn frm = new frmIn();
                    BaoCao.Rep_BCSudungthuockhoaDT rep = new BaoCao.Rep_BCSudungthuockhoaDT();
                    rep.TieuDe.Value = "BÁO CÁO SỬ DỤNG " + _nhomdv + " TẠI KHOA ĐIỀU TRỊ";
                    rep.paramKhoaPhong.Value = lupMaKP.Text.ToUpper();
                    rep.Ngaythang.Value = "Từ ngày  " + lupTuNgay.DateTime.ToString().Substring(0, 10) + "  đến ngày  " + lupDenNgay.DateTime.ToString().Substring(0, 10);
                    if (idNhom1 == 0)
                    {
                        if (_makp>0)
                            rep.DataSource = q.Where(p => p.MaKP == _makp).ToList().OrderBy(p => p.TenThuoc);
                        else
                        {
                            var q3 = (from a in q
                                      group a by new { a.MaDV, a.DonGia, a.DonVi, a.TenThuoc } into kq
                                      select new
                                      {
                                          kq.Key.DonGia,
                                          kq.Key.DonVi,
                                          kq.Key.MaDV,
                                          kq.Key.TenThuoc,
                                          SoLuong = kq.Sum(p => p.SoLuong),
                                          ThanhTien = kq.Sum(p => p.ThanhTien),
                                      }).ToList();
                            rep.DataSource = q3.OrderBy(p => p.TenThuoc);
                        }
                    }
                    else
                    {
                        if (_makp>0)
                            rep.DataSource = q.Where(p => p.IdNhom == idNhom1 && p.MaKP == _makp).ToList().OrderBy(p => p.TenThuoc);
                        else
                        {
                            var q3 = (from a in q.Where(p => p.IdNhom == idNhom1)
                                      group a by new { a.MaDV, a.DonGia, a.DonVi, a.TenThuoc } into kq
                                      select new
                                      {
                                          kq.Key.DonGia,
                                          kq.Key.DonVi,
                                          kq.Key.MaDV,
                                          kq.Key.TenThuoc
                                      ,
                                          SoLuong = kq.Sum(p => p.SoLuong),
                                          ThanhTien = kq.Sum(p => p.ThanhTien),
                                      }).ToList();
                            rep.DataSource = q3.OrderBy(p => p.TenThuoc);
                        }
                    }
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}