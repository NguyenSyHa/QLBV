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
    public partial class frm_80aHDTH : DevExpress.XtraEditors.XtraForm
    {
        public frm_80aHDTH()
        {
            InitializeComponent();
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
        private void btnOK_Click(object sender, EventArgs e)
        {
            setIDNhom();
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string _dtuong = "BHYT";
            if (chkNhandan.Checked == true)
                _dtuong = "Dịch vụ";
            if (kt())
            {
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                if (radTimKiem.SelectedIndex == 0)
                {
                    var q = (from bn in _dataContext.BenhNhans.Where(p => p.DTuong == _dtuong).Where(p => p.NoiTru == 1).Where(p => p.SThe.Length > 2)
                             join vp in _dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan
                             join vpct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                             join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                             join vv in _dataContext.VaoViens on bn.MaBNhan equals vv.MaBNhan
                             join rv in _dataContext.RaViens.Where(p => p.NgayRa <= ngayden).Where(p => p.NgayRa >= ngaytu) on bn.MaBNhan equals rv.MaBNhan
                             group new { bn, rv, dv, vpct, vp, } by new { bn.NoiTinh, vp.MaBNhan, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.MaCS, bn.Tuyen, vv.NgayVao, rv.NgayRa, rv.SoNgaydt, rv.MaKP, rv.MaICD } into kq
                             select new
                             {
                                 NoiTinh = kq.Key.NoiTinh,
                                 Tuyen = kq.Key.Tuyen,
                                 Makp = kq.Key.MaKP,
                                 MaBNhan = kq.Key.MaBNhan,
                                 TenBNhan = kq.Key.TenBNhan,
                                 NSinh = kq.Key.NamSinh,
                                 SThe = kq.Key.SThe,
                                 Nam = kq.Key.GTinh,
                                 MaCS = kq.Key.MaCS,
                                 MaICD = kq.Key.MaICD,
                                 Ngayvao = kq.Key.NgayVao,
                                 Ngayra = kq.Key.NgayRa,
                                 Songay = kq.Key.SoNgaydt,
                                 Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Sum(p => p.vpct.ThanhTien),
                                 CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.vpct.ThanhTien),
                                 Congkham = kq.Where(p => p.dv.IDNhom == idNgayGiuong).Sum(p => p.vpct.ThanhTien),
                                 Xetnghiem = kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.vpct.ThanhTien),
                                 Mau = kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.vpct.ThanhTien),
                                 TTPT = kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.vpct.ThanhTien),
                                 VTYT = kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.vpct.ThanhTien),
                                 DVKTC = kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.vpct.ThanhTien),
                                 ThuocKTCG = kq.Where(p => p.dv.IDNhom == idThuocUngThuCTG).Sum(p => p.vpct.ThanhTien),
                                 CPVanchuyen = kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.vpct.ThanhTien),
                                 ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                 Tongchi = kq.Sum(p => p.vpct.ThanhTien),
                                 Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                                 Nguoibenhchitra = kq.Sum(p => p.vpct.TienBN),
                                 TongcongBHYT = kq.Sum(p => p.vpct.TienBH),
                             }).OrderByDescending(p => p.NoiTinh).OrderBy(p => p.Tuyen).ToList();
                    if (lupKhoaphong.Text == "")
                    {
                        frmIn frm = new frmIn();
                        BaoCao.rep80aHDTH rep = new BaoCao.rep80aHDTH();
                        rep.DataSource = q;
                        rep.Ngaythang.Value = theoquy();
                        rep.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                        double st = 0;
                        st = q.Sum(p => p.TongcongBHYT);
                        rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        int _MaKP = lupKhoaphong.EditValue== null ? 0 :  Convert.ToInt32(lupKhoaphong.EditValue);
                        frmIn frm = new frmIn();
                        BaoCao.rep80aHDTH rep = new BaoCao.rep80aHDTH();
                        rep.DataSource = q.ToList().Where(p => p.Makp == _MaKP);
                        rep.Ngaythang.Value = theoquy();
                        double st = 0;
                        st = q.Where(p => p.Makp== (_MaKP)).Sum(p => p.TongcongBHYT);
                        rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                        rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
                else
                {// ngày thanh toán
                    var q = (from bn in _dataContext.BenhNhans.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 1).Where(p => p.SThe.Length > 2)
                             join vp in _dataContext.VienPhis.Where(p => p.NgayTT <= ngayden).Where(p => p.NgayTT >= ngaytu) on bn.MaBNhan equals vp.MaBNhan
                             join vpct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                             join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                             join vv in _dataContext.VaoViens on bn.MaBNhan equals vv.MaBNhan
                             join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                             group new { bn, rv, dv, vpct, vp, } by new { bn.NoiTinh, vp.MaBNhan, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.MaCS, bn.Tuyen, vv.NgayVao, rv.NgayRa, rv.SoNgaydt, rv.MaKP, rv.MaICD } into kq
                             select new
                             {
                                 //dt=kq.Key.Nhom,
                                 NoiTinh = kq.Key.NoiTinh,
                                 Tuyen = kq.Key.Tuyen,
                                 Makp = kq.Key.MaKP,
                                 MaBNhan = kq.Key.MaBNhan,
                                 TenBNhan = kq.Key.TenBNhan,
                                 NSinh = kq.Key.NamSinh,
                                 SThe = kq.Key.SThe,
                                 Nam = kq.Key.GTinh,
                                 MaCS = kq.Key.MaCS,
                                 MaICD = kq.Key.MaICD,
                                 Ngayvao = kq.Key.NgayVao,
                                 Ngayra = kq.Key.NgayRa,
                                 Songay = kq.Key.SoNgaydt,
                                 Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Sum(p => p.vpct.ThanhTien),
                                 CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.vpct.ThanhTien),
                                 Congkham = kq.Where(p => p.dv.IDNhom == idNgayGiuong).Sum(p => p.vpct.ThanhTien),
                                 Xetnghiem = kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.vpct.ThanhTien),
                                 Mau = kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.vpct.ThanhTien),
                                 TTPT = kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.vpct.ThanhTien),
                                 VTYT = kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.vpct.ThanhTien),
                                 DVKTC = kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.vpct.ThanhTien),
                                 ThuocKTCG = kq.Where(p => p.dv.IDNhom == idThuocUngThuCTG).Sum(p => p.vpct.ThanhTien),
                                 CPVanchuyen = kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.vpct.ThanhTien),
                                 ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                 Tongchi = kq.Sum(p => p.vpct.ThanhTien),
                                 Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                                 Nguoibenhchitra = kq.Sum(p => p.vpct.TienBN),
                                 TongcongBHYT = kq.Sum(p => p.vpct.TienBH),
                             }).OrderByDescending(p => p.NoiTinh).OrderBy(p => p.Tuyen).ToList();
                    if (lupKhoaphong.Text == "")
                    {
                        frmIn frm = new frmIn();
                        BaoCao.rep80aHDTH rep = new BaoCao.rep80aHDTH();
                        rep.DataSource = q;
                        rep.Ngaythang.Value = theoquy();
                        rep.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                        double st = 0;
                        st = q.Sum(a => a.TongcongBHYT);
                        rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
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
                        BaoCao.rep80aHDTH rep = new BaoCao.rep80aHDTH();
                        rep.DataSource = q.ToList().Where(p => p.Makp == _MaKP);
                        rep.Ngaythang.Value = theoquy();
                        double st = 0;
                        st = q.Where(p => p.Makp== (_MaKP)).Sum(p => p.TongcongBHYT);
                        rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                        rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
            }



        }
        private void frm_80ct_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from kp in _dataContext.KPhongs.Where(p => p.PLoai== ("Lâm sàng"))
                    select new { kp.TenKP, kp.MaKP };
            lupKhoaphong.Properties.DataSource = q.ToList();
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            lupNgaytu.EditValue = ngaytu;
            lupngayden.EditValue = ngayden;
            lupNgaytu.Focus();
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
                quy = "Từ ngày " + lupNgaytu.DateTime.ToString().Substring(0, 10) + " đến ngày " + lupngayden.DateTime.ToString().Substring(0, 10);
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
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdFont_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}