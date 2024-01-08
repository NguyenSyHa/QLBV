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
    public partial class frm_rep79aTH : DevExpress.XtraEditors.XtraForm
    {
        public frm_rep79aTH()
        {
            InitializeComponent();
        }
        string _dtuong = "";
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int idThuoc = -1, idMau = -1, idXN = -1, idCDHA = -1, idTTPT = -1, idCongKham = -1, idDVKTC = -1,
            idVTYT = -1, idNgayGiuong = -1, idChiPhiVC = -1, idVTTT = -1, idThuocUngThuCTG = -1, idHoaChat = -1;
        private void setIDNhom()
        {

            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var tenNhom = _data.NhomDVs.Select(a => new { a.IDNhom, a.TenNhomCT }).ToList();
            foreach (var item in tenNhom)
            {
                switch (item.TenNhomCT)
                {
                    case  "Thuốc trong danh mục BHYT" :
                        idThuoc = item.IDNhom;
                        break;
                    case "Máu và chế phẩm của máu":
                        idMau = item.IDNhom;
                        break;
                    case "Xét nghiệm":
                        idXN = item.IDNhom;
                        break;
                    case "Chẩn đoán hình ảnh":
                        idCDHA = item.IDNhom;
                        break;
                    case "Thủ thuật, phẫu thuật":
                        idTTPT = item.IDNhom;
                        break;
                    case "Khám bệnh":
                        idCongKham = item.IDNhom;
                        break;
                    case "DVKT thanh toán theo tỷ lệ":
                        idDVKTC = item.IDNhom;
                        break;
                    case"Vật tư y tế trong danh mục BHYT":
                        idVTYT = item.IDNhom;
                        break;
                    case "Giường điều trị nội trú":
                        idNgayGiuong = item.IDNhom;
                        break;
                    case "Vận chuyển":
                        idChiPhiVC = item.IDNhom;
                        break;
                    case"VTYT thanh toán theo tỷ lệ":
                        idVTTT = item.IDNhom;
                        break;
                    case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                        idThuocUngThuCTG = item.IDNhom;
                        break;
                    case "Nhóm hóa chất":
                        idHoaChat = item.IDNhom;
                        break;

                }

            }
        }
        //private class mau79a { 
        //int tuoi,noitinh,tuyen,
        //}
        private void btnOK_Click(object sender, EventArgs e)
        {
            setIDNhom();
            if (chkNhandan.Checked == false)
            {
                _dtuong = "BHYT";
                QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                if (kt())
                {
                    DateTime ngaytu = System.DateTime.Now.Date;
                    DateTime ngayden = System.DateTime.Now.Date;
                    ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                    ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);

                    var q2 = (from bn in _dataContext.BenhNhans.Where(p => p.DTuong == "BHYT")
                              join vp in _dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan
                              join vpct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                              join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                              join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                              where (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden)
                              group new { bn, rv, dv, vpct, vp, } by new { bn.NoiTru, bn.Tuoi, bn.TuyenDuoi, bn.NoiTinh, vp.MaBNhan, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.MaCS, bn.Tuyen, bn.NNhap, rv.MaKP, rv.MaICD } into kq
                              select new

                              {
                                  kq.Key.NoiTru,
                                  kq.Key.TuyenDuoi,
                                  kq.Key.Tuoi,
                                  NoiTinh = kq.Key.NoiTinh,
                                  Tuyen = kq.Key.Tuyen,
                                  Makp = kq.Key.MaKP,
                                  Mabn = kq.Key.MaBNhan,
                                  TenBNhan = kq.Key.TenBNhan,
                                  NSinh = kq.Key.NamSinh,
                                  SThe = kq.Key.SThe,
                                  GTinh = kq.Key.GTinh,
                                  MaCS = kq.Key.MaCS,
                                  MaICD = kq.Key.MaICD,
                                  Ngaykham = kq.Key.NNhap,
                                  Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Sum(p => p.vpct.ThanhTien),
                                  CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.vpct.ThanhTien),
                                  Congkham = kq.Where(p => p.dv.IDNhom == idCongKham).Sum(p => p.vpct.ThanhTien),
                                  xetnghiem = kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.vpct.ThanhTien),
                                  Mau = kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.vpct.ThanhTien),
                                  TTPT = kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.vpct.ThanhTien),
                                  VTYTH = kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.vpct.ThanhTien),
                                  VTTT = kq.Where(p => p.dv.IDNhom == idVTTT).Sum(p => p.vpct.ThanhTien),
                                  DVKTC = kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.vpct.ThanhTien),
                                  ThuocKTCG = kq.Where(p => p.dv.IDNhom == idThuocUngThuCTG).Sum(p => p.vpct.ThanhTien),
                                  CPVanchuyen = kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.vpct.ThanhTien),
                                  ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                  Tongchi = kq.Sum(p => p.vpct.ThanhTien),
                                  Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                                  TienBN = kq.Sum(p => p.vpct.TienBN),
                                  TienBH = kq.Sum(p => p.vpct.TienBH),
                              }).ToList();
                    if (radtuyenduoi.SelectedIndex == 0)
                    {

                        var q = q2.Where(p => p.NoiTru == 0 && p.TuyenDuoi == 0).OrderByDescending(p => p.NoiTinh).ThenBy(p => p.Tuyen).ToList();
                        if (lupKhoaphong.Text == "")
                        {
                            frmIn frm = new frmIn();
                            BaoCao.rep79aTH rep = new BaoCao.rep79aTH();
                            rep.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                            int soluotDT1 = q.Where(p => p.SThe.Length > 2).Select(p => p.Mabn).Count();
                            rep.SLAdungtuyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 1).Sum(p => p.Tuoi);
                            rep.SLAtraituyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 2).Sum(p => p.Tuoi);
                            rep.SLBdungtuyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 1).Sum(p => p.Tuoi);
                            rep.SLBTraituyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 2).Sum(p => p.Tuoi);
                            rep.SLCdungtuyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 1).Sum(p => p.Tuoi);
                            rep.SLCTraituyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 2).Sum(p => p.Tuoi);
                            rep.SLTong.Value = q.Where(p => p.SThe.Length > 2).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Sum(p => p.Tuoi);

                            rep.Ngaythang.Value = theoquy();
                            rep.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                            double st = 0;
                            st = q.Sum(a => a.TienBH);
                            rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                            rep.DataSource = q;
                            rep.BindingData();
                            rep.CreateDocument();
                            //rep.DataMember = "Table";
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            int _MaKP = lupKhoaphong.EditValue== null ? 0 : Convert.ToInt32(lupKhoaphong.EditValue);
                            frmIn frm = new frmIn();
                            BaoCao.rep79aTH rep = new BaoCao.rep79aTH();
                            rep.MaCS.Value = DungChung.Bien.MaBV;
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                            rep.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                            rep.SLAdungtuyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count();
                            rep.SLAtraituyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count();
                            rep.SLBdungtuyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count();
                            rep.SLBTraituyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count();
                            rep.SLCdungtuyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count();
                            rep.SLCTraituyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count();
                            rep.DataSource = q.Where(a => a.Makp == _MaKP).ToList();
                            rep.Ngaythang.Value = theoquy();
                            double st = 0;
                            st = q.Where(p => p.Makp == _MaKP).Sum(p => p.TienBH);
                            rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                    else {
                        if (radtuyenduoi.SelectedIndex == 1)
                        {

                            var q = q2.Where(p => p.NoiTru == 0 && p.TuyenDuoi == 1).OrderByDescending(p => p.NoiTinh).ThenBy(p => p.Tuyen).ToList();
                            if (lupKhoaphong.Text == "")
                            {
                                frmIn frm = new frmIn();
                                BaoCao.rep79aTH rep = new BaoCao.rep79aTH();
                                rep.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                                int soluotDT1 = q.Where(p => p.SThe.Length > 2).Select(p => p.Mabn).Count();
                                rep.SLAdungtuyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 1).Sum(p => p.Tuoi);
                                rep.SLAtraituyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 2).Sum(p => p.Tuoi);
                                rep.SLBdungtuyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 1).Sum(p => p.Tuoi);
                                rep.SLBTraituyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 2).Sum(p => p.Tuoi);
                                rep.SLCdungtuyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 1).Sum(p => p.Tuoi);
                                rep.SLCTraituyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 2).Sum(p => p.Tuoi);
                                rep.SLTong.Value = q.Where(p => p.SThe.Length > 2).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Sum(p => p.Tuoi);

                                rep.Ngaythang.Value = theoquy();
                                rep.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                                rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                rep.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                                double st = 0;
                                st = q.Sum(a => a.TienBH);
                                rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                rep.DataSource = q;
                                rep.BindingData();
                                rep.CreateDocument();
                                //rep.DataMember = "Table";
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else
                            {
                                int _MaKP = lupKhoaphong.EditValue== null ? 0 : Convert.ToInt32(lupKhoaphong.EditValue);
                                frmIn frm = new frmIn();
                                BaoCao.rep79aTH rep = new BaoCao.rep79aTH();
                                rep.MaCS.Value = DungChung.Bien.MaBV;
                                rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                                rep.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                                rep.SLAdungtuyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count();
                                rep.SLAtraituyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count();
                                rep.SLBdungtuyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count();
                                rep.SLBTraituyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count();
                                rep.SLCdungtuyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count();
                                rep.SLCTraituyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count();
                                rep.DataSource = q.Where(a => a.Makp == _MaKP).ToList();
                                rep.Ngaythang.Value = theoquy();
                                double st = 0;
                                st = q.Where(p => p.Makp == _MaKP).Sum(p => p.TienBH);
                                rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                        }
                        else {
                                var q = q2.Where(p => p.NoiTru == 0).OrderByDescending(p => p.NoiTinh).ThenBy(p => p.Tuyen).ToList();
                                if (lupKhoaphong.Text == "")
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.rep79aTH rep = new BaoCao.rep79aTH();
                                    rep.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                                    int soluotDT1 = q.Where(p => p.SThe.Length > 2).Select(p => p.Mabn).Count();
                                    rep.SLAdungtuyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 1).Sum(p => p.Tuoi);
                                    rep.SLAtraituyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 2).Sum(p => p.Tuoi);
                                    rep.SLBdungtuyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 1).Sum(p => p.Tuoi);
                                    rep.SLBTraituyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 2).Sum(p => p.Tuoi);
                                    rep.SLCdungtuyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 1).Sum(p => p.Tuoi);
                                    rep.SLCTraituyen.Value = q.Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 2).Sum(p => p.Tuoi);
                                    rep.SLTong.Value = q.Where(p => p.SThe.Length > 2).Select(p => p.Mabn).Count() + q.Where(p => p.SThe.Length == 2).Sum(p => p.Tuoi);

                                    rep.Ngaythang.Value = theoquy();
                                    rep.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                                    rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                    rep.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                                    double st = 0;
                                    st = q.Sum(a => a.TienBH);
                                    rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                    rep.DataSource = q;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    int _MaKP = lupKhoaphong.EditValue== null ? 0 : Convert.ToInt32( lupKhoaphong.EditValue);
                                    frmIn frm = new frmIn();
                                    BaoCao.rep79aTH rep = new BaoCao.rep79aTH();
                                    rep.MaCS.Value = DungChung.Bien.MaBV;
                                    rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                    rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                                    rep.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                                    rep.SLAdungtuyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count();
                                    rep.SLAtraituyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 1).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count();
                                    rep.SLBdungtuyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count();
                                    rep.SLBTraituyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 2).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count();
                                    rep.SLCdungtuyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 1).Select(p => p.Mabn).Count();
                                    rep.SLCTraituyen.Value = q.Where(p => p.Makp == _MaKP).Where(p => p.SThe.Length > 2).Where(p => p.NoiTinh == 3).Where(p => p.Tuyen == 2).Select(p => p.Mabn).Count();
                                    rep.DataSource = q.Where(a => a.Makp == _MaKP).ToList();
                                    rep.Ngaythang.Value = theoquy();
                                    double st = 0;
                                    st = q.Where(p => p.Makp == _MaKP).Sum(p => p.TienBH);
                                    rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            
                        }
                    }



                }
            }
            else
            {
                QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                if (kt())
                {
                    _dtuong = "Dịch vụ";
                    DateTime ngaytu = System.DateTime.Now.Date;
                    DateTime ngayden = System.DateTime.Now.Date;
                    ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                    ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                    var q = (from bn in _dataContext.BenhNhans.Where(p => p.DTuong != "BHYT").Where(p => p.NoiTru == 0)
                             join vp in _dataContext.VienPhis.Where(p => p.NgayTT >= ngaytu).Where(p => p.NgayTT <= ngayden) on bn.MaBNhan equals vp.MaBNhan
                             join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                             join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                             join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                             group new { bn, rv, dv, vpct, vp } by new { bn.NoiTinh, bn.NNhap, vp.MaBNhan, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.MaCS, bn.Tuyen, rv.MaKP, rv.MaICD } into kq
                             select new
                             {
                                 NoiTinh = kq.Key.NoiTinh,
                                 Tuyen = kq.Key.Tuyen,
                                 Makp = kq.Key.MaKP,
                                 Mabn = kq.Key.MaBNhan,
                                 TenBNhan = kq.Key.TenBNhan,
                                 NSinh = kq.Key.NamSinh,
                                 SThe = kq.Key.SThe,
                                 GTinh = kq.Key.GTinh,
                                 MaCS = kq.Key.MaCS,
                                 MaICD = kq.Key.MaICD,
                                 Ngaykham = kq.Key.NNhap,
                                 Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Sum(p => p.vpct.ThanhTien),
                                 CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.vpct.ThanhTien),
                                 Congkham = kq.Where(p => p.dv.IDNhom == idCongKham).Sum(p => p.vpct.ThanhTien),
                                 xetnghiem = kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.vpct.ThanhTien),
                                 Mau = kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.vpct.ThanhTien),
                                 TTPT = kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.vpct.ThanhTien),
                                 VTYTH = kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.vpct.ThanhTien),
                                 VTTT = kq.Where(p => p.dv.IDNhom == idVTTT).Sum(p => p.vpct.ThanhTien),
                                 DVKTC = kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.vpct.ThanhTien),
                                 ThuocKTCG = kq.Where(p => p.dv.IDNhom == idThuocUngThuCTG).Sum(p => p.vpct.ThanhTien),
                                 CPVanchuyen = kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.vpct.ThanhTien),
                                 ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                 Tongchi = kq.Sum(p => p.vpct.ThanhTien),
                                 Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                                 TienBN = kq.Sum(p => p.vpct.TienBN),
                                 TienBH = kq.Sum(p => p.vpct.TienBH),

                             }).OrderByDescending(p => p.NoiTinh).OrderBy(p => p.Tuyen).ToList();
                    if (lupKhoaphong.Text == "")
                    {
                        frmIn frm = new frmIn();
                        BaoCao.rep79aTH rep = new BaoCao.rep79aTH(_dtuong);

                        rep.DataSource = q;
                        rep.Ngaythang.Value = theoquy();
                        rep.MaCS.Value = DungChung.Bien.MaBV;
                        rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                        rep.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                        double st = 0;
                        if (q.Count > 0)
                        {
                            st = q.Sum(p => p.TienBH);
                        }
                        rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        int _MaKP = lupKhoaphong.EditValue == null ? 0 : Convert.ToInt32(lupKhoaphong.EditValue);
                        frmIn frm = new frmIn();
                        BaoCao.rep79aTH rep = new BaoCao.rep79aTH();
                        rep.MaCS.Value = DungChung.Bien.MaBV;
                        rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                        rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                        rep.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                        rep.DataSource = q.ToList().Where(p => p.Makp == _MaKP);
                        rep.Ngaythang.Value = theoquy();
                        double st = 0;
                        st = q.Where(p => p.Makp == _MaKP).Sum(p => p.TienBH);
                        rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                        rep.BindingData();
                        rep.CreateDocument();
                        //rep.DataMember = "Table";
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void frm_rep79aCT_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from kp in _dataContext.KPhongs
                    where (kp.PLoai== ("Lâm sàng") || kp.PLoai== ("Phòng khám"))
                    select new { kp.TenKP, kp.MaKP };
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            lupNgaytu.EditValue = ngaytu;
            lupngayden.EditValue = ngayden;
            lupNgaytu.Focus();
            lupKhoaphong.Properties.DataSource = q.ToList();
        }
        private bool kt()
        {
            if (lupNgaytu.Text == "" || lupngayden.Text == "")
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupNgaytu.Focus();
                return false;
            }
            return true;
        }
        private string theoquy()
        {
            string quy = "";

            if (ckBC.Checked == true)
            {
                switch (timquy(lupNgaytu.DateTime.Month))
                {
                    case 1:
                        quy = "Quý I";
                        break;
                    case 2:
                        quy = "Quý II";
                        break;
                    case 3:
                        quy = "Quý III";
                        break;
                    case 4:
                        quy = "Quý IV";
                        break;
                }

            }
            else
            {
                quy = "Từ ngày: " + lupNgaytu.DateTime.ToString().Substring(0, 10) + "   đến ngày: " + lupngayden.DateTime.ToString().Substring(0, 10);
            }
            return quy;
        }
        private int timquy(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return (1);
            }
            if (month > 3 && month <= 6)
            {
                return (2);
            }
            if (month > 6 && month <= 9)
            {
                return (3);
            }
            else { return (4); }
        }

        private void lupKhoaphong_EditValueChanged(object sender, EventArgs e)
        {
            // if(
        }
    }
}