using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using COMExcel = Microsoft.Office.Interop.Excel;
using QLBV.DungChung;
using OpenXmlPackaging;
using System.IO;

namespace QLBV.FormThamSo
{

    public partial class frm_80aHD_1399 : DevExpress.XtraEditors.XtraForm
    {
        public frm_80aHD_1399()
        {
            InitializeComponent();
        }
        int idThuoc = -1, idMau = -1, idXN = -1, idCDHA = -1, idTTPT = -1, idCongKham = -1, idDVKTC = -1,
            idVTYT = -1, idNgayGiuong = -1, idChiPhiVC = -1, idVTTT = -1, idThuocUngThuCTG = -1, idHoaChat = -1;
        private void setIDNhom()
        {

            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var tenNhom = _data.NhomDVs.ToList();
            foreach (var item in tenNhom)
            {
                switch (item.TenNhomCT)
                {
                    case  "Thuốc trong danh mục BHYT" :
                        idThuoc = item.IDNhom;
                        if (tenNhom.Where(a => a.TenNhomCT ==  "Thuốc trong danh mục BHYT" ).Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm thuốc, dịch truyền");
                        break;
                    case "Máu và chế phẩm của máu":
                        if (tenNhom.Where(a => a.TenNhomCT == "Máu và chế phẩm của máu").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm máu và chế phẩm của máu");
                        idMau = item.IDNhom;
                        break;
                    case "Xét nghiệm":
                        if (tenNhom.Where(a => a.TenNhomCT == "Xét nghiệm").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm xét nghiệm");
                        idXN = item.IDNhom;
                        break;
                    case "Chẩn đoán hình ảnh":
                        if (tenNhom.Where(a => a.TenNhomCT == "Chẩn đoán hình ảnh").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm chẩn đoán hình ảnh & TDCN");
                        idCDHA = item.IDNhom;
                        break;
                    case "Thủ thuật, phẫu thuật":
                        if (tenNhom.Where(a => a.TenNhomCT == "Thủ thuật, phẫu thuật").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm phẫu thuật, thủ thuật");
                        idTTPT = item.IDNhom;
                        break;
                    case "Khám bệnh":
                        if (tenNhom.Where(a => a.TenNhomCT == "Khám bệnh").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm tiền công khám");
                        idCongKham = item.IDNhom;
                        break;
                    case "DVKT thanh toán theo tỷ lệ":
                        if (tenNhom.Where(a => a.TenNhomCT == "DVKT thanh toán theo tỷ lệ").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm dịch vụ kỹ thuật cao");
                        idDVKTC = item.IDNhom;
                        break;
                    case"Vật tư y tế trong danh mục BHYT":
                        if (tenNhom.Where(a => a.TenNhomCT =="Vật tư y tế trong danh mục BHYT").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm vật tư y tế tiêu hao");
                        idVTYT = item.IDNhom;
                        break;
                    case "Giường điều trị nội trú":
                        if (tenNhom.Where(a => a.TenNhomCT == "Giường điều trị nội trú").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm ngày giường");
                        idNgayGiuong = item.IDNhom;
                        break;
                    case "Vận chuyển":
                        if (tenNhom.Where(a => a.TenNhomCT == "Vận chuyển").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Chi phí vận chuyển");
                        idChiPhiVC = item.IDNhom;
                        break;
                    case"VTYT thanh toán theo tỷ lệ":
                        if (tenNhom.Where(a => a.TenNhomCT =="VTYT thanh toán theo tỷ lệ").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm vật tư y tế thay thế");
                        idVTTT = item.IDNhom;
                        break;
                    case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                        if (tenNhom.Where(a => a.TenNhomCT == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm thuốc ung thư, chống thải ghép");
                        idThuocUngThuCTG = item.IDNhom;
                        break;
                    case "Nhóm hóa chất":
                        if (tenNhom.Where(a => a.TenNhomCT == "Nhóm hóa chất").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm hóa chất");
                        idHoaChat = item.IDNhom;
                        break;
                }

            }
        }
        List<Cls79_80.cl_79_80> _listVPBH = new List<Cls79_80.cl_79_80>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            _listVPBH.Clear();
            setIDNhom();
            int trongBH = 0;
            trongBH = rdTrongBH.SelectedIndex;
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string DoiTuongKham = "BHYT";
            DoiTuongKham = lupDoituong.Text;
            int _ngaytt = radTimKiem.SelectedIndex;
            if (kt())
            {
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                int _MaKPc = 0;
                if (lupKhoaphong.EditValue != null)
                {
                    _MaKPc = Convert.ToInt32( lupKhoaphong.EditValue);
                }
                List<string> _ltamung = new List<string>();
                //if (_ngaytt == 2)
                //{
                //    _ltamung = (from tu in _dataContext.TamUngs
                //                where ((tu.PhanLoai == 2 || tu.PhanLoai == 1) && tu.NgayThu >= ngaytu && tu.NgayThu <= ngayden)
                //                select tu.MaBNhan).ToList();
                //    // lấy danh sách mã bệnh nhân trong bảng tạm ứng với phân loại = 2 ( đã duyệt) và ngày thu > từ ngày và < đến ngày
                //}
                //  _dataContext.CommandTimeout = 120;
                int _CP_BH = 0;
                if (ckTLBH.Checked)
                    _CP_BH = 1;
                if (ckTLBN.Checked)
                    _CP_BH = 2;
                if (ckTLBN.Checked && ckTLBH.Checked)
                    _CP_BH = 0;
              
                var q2 = (from
                      rv in _dataContext.RaViens
                          join vp in _dataContext.VienPhis on rv.MaBNhan equals vp.MaBNhan
                          join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                          where (_ngaytt == 2 ? (vp.NgayDuyet >= ngaytu && vp.NgayDuyet <= ngayden) : (_ngaytt == 1 ? (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden) : (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)))
                          select new
                          {
                        
                              vpct.MaDV,
                              vp.MaBNhan,
                              vpct.TrongBH,
                              vpct.MaKP,
                              rv.MaICD,
                              rv.NgayVao,
                              rv.NgayRa,
                              rv.SoNgaydt,
                              rv.Status,
                              rv.KetQua,
                              ThanhTien= _CP_BH == 0? vpct.ThanhTien:(_CP_BH == 1 ? vpct.TienBH :vpct.TienBN),
                              TienBN = _CP_BH == 1? 0: vpct.TienBN ,
                              TienBH = _CP_BH == 2? 0:  vpct.TienBH,
                              vp.NgayTT,
                          }).ToList();
                var q4 = (from
                        a in q2
                          join bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 1)
                          on a.MaBNhan equals bn.MaBNhan
                          where (bn.DTuong == lupDoituong.Text && bn.NoiTru==1)
                          select new
                          {
                              a.TrongBH,
                              a.MaKP,
                              bn.DChi,
                              bn.HanBHDen,
                              bn.HanBHTu,
                              bn.TuyenDuoi,
                              bn.DTNT,
                              bn.DTuong,
                              bn.NoiTru,
                             // bn.MaBNhan,
                              bn.NoiTinh,
                              bn.Tuyen,
                              bn.MaBNhan,
                              bn.TenBNhan,
                              bn.NamSinh,
                              bn.NgaySinh,
                              bn.ThangSinh,
                              bn.SThe,
                              bn.GTinh,
                              bn.MaCS,
                              bn.MaDTuong,
                              bn.CapCuu,
                              a.MaICD,
                              a.NgayVao,
                              a.NgayRa,
                              a.SoNgaydt,
                              a.Status,
                              a.KetQua,
                              a.MaDV,
                              a.ThanhTien,
                              a.TienBN,
                              a.TienBH,                              
                              a.NgayTT,
                              bn.Tuoi,
                              bn.KhuVuc,
                              bn.MaBV
                          }).OrderBy(p => p.MaBNhan).ToList();
                var q3 = (from a in q4
                          join dv in _ldv on a.MaDV equals dv.MaDV
                          where (lupDoituong.Text == "BHYT" ? ((radXP.SelectedIndex < 3 ? a.TuyenDuoi == radXP.SelectedIndex : true) && (cboNoiTinh.SelectedIndex > 0 ? a.NoiTinh == cboNoiTinh.SelectedIndex : true)) : true)
                          where (a.TrongBH == trongBH)
                          where (_MaKPc == 0 ? true : a.MaKP == _MaKPc)
                          group new { a,dv } by new { a.HanBHDen, a.HanBHTu, a.DChi, a.SoNgaydt, a.DTNT, a.TuyenDuoi, a.NgayTT, a.DTuong, a.NoiTru, a.TrongBH, a.NoiTinh, a.MaBNhan, a.TenBNhan, a.NamSinh, a.NgaySinh, a.ThangSinh, a.GTinh, a.SThe, a.MaCS, a.Tuyen, a.NgayVao, a.MaICD, a.NgayRa, a.MaDTuong, a.CapCuu, a.KetQua, a.Status, a.Tuoi, a.KhuVuc, a.MaBV } into kq
                          select new
                          {
                              kq.Key.SoNgaydt,
                              kq.Key.DTuong,
                              kq.Key.NoiTru,
                              kq.Key.TrongBH,
                              kq.Key.TuyenDuoi,
                             // kq.Key.MaBNhan,
                              kq.Key.NgayTT,
                              kq.Key.MaDTuong,
                              kq.Key.CapCuu,
                              kq.Key.NgaySinh,
                              kq.Key.ThangSinh,
                              kq.Key.KhuVuc,
                              kq.Key.MaBV,
                              kq.Key.KetQua,
                              kq.Key.Status,
                              NoiTinh = kq.Key.NoiTinh,
                              Tuyen = kq.Key.Tuyen,
                              MaBNhan = kq.Key.MaBNhan,
                              TenBNhan = kq.Key.TenBNhan,
                              NSinh = kq.Key.NamSinh,
                              SThe = kq.Key.SThe,
                              Nam = kq.Key.GTinh,
                              GTinh = kq.Key.GTinh,
                              MaCS = kq.Key.MaCS,
                              MaICD = kq.Key.MaICD,
                              Ngaykham = kq.Key.NgayVao,
                              Ngayra = kq.Key.NgayRa,
                              Tuoi = kq.Key.Tuoi,
                              BHtu = kq.Key.HanBHTu,
                              BHden = kq.Key.HanBHDen,
                              Diachi = kq.Key.DChi,
                              Mabn = kq.Key.MaBNhan,
                              Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Where(p => p.dv.BHTT == 100).Sum(p => p.a.ThanhTien),
                              CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.a.ThanhTien),
                              Congkham = kq.Where(p => p.dv.IDNhom == idNgayGiuong).Sum(p => p.a.ThanhTien),
                              Xetnghiem = kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.a.ThanhTien),
                              Mau = kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.a.ThanhTien),
                              TTPT = kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.a.ThanhTien),
                              VTYT = kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.a.ThanhTien),
                              DVKT_tl = kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.a.ThanhTien),
                              Thuoc_tl = kq.Where(p => p.dv.IDNhom == idThuocUngThuCTG).Where(p => p.dv.BHTT != 100).Sum(p => p.a.ThanhTien),
                              VTYT_tl = kq.Where(p => p.dv.IDNhom == idVTTT).Sum(p => p.a.ThanhTien),
                              CPVanchuyen = kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.a.ThanhTien),
                              CPNgoaiBH = kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.a.ThanhTien),
                              ThanhTien = kq.Sum(p => p.a.ThanhTien),
                              Tongchi = kq.Sum(p => p.a.ThanhTien),
                              Tongcong = kq.Sum(p => p.a.ThanhTien),
                              TienBN = kq.Sum(p => p.a.TienBN),
                              TienBH = kq.Sum(p => p.a.TienBH),
                          }).ToList();

                    List<DTuong> _lDTuong = new List<DTuong>();
                    _lDTuong = _dataContext.DTuongs.ToList();
                    foreach (var n in q3)
                    {
                        Cls79_80.cl_79_80 vpbh = new Cls79_80.cl_79_80();
                        if (n.Tuyen != null)
                            vpbh.Tuyen = n.Tuyen.Value;
                        if (n.SoNgaydt != null)
                            vpbh.So_ngay_dtri = n.SoNgaydt.Value;
                        if (n.BHtu != null)
                            vpbh.Gt_the_tu = n.BHtu.Value;
                        if (n.BHden != null)
                            vpbh.Gt_the_den = n.BHden.Value;
                        if (n.Diachi != null)
                            vpbh.Dia_chi = n.Diachi;
                        if (n.NoiTru != null)
                        {
                            if (n.NoiTru == 1)
                                vpbh.Ma_loaikcb = 3;
                            else
                                vpbh.Ma_loaikcb = 2;
                        }
                        if (n.KhuVuc != null)
                            vpbh.Ma_khuvuc = n.KhuVuc;
                        if (n.Mabn != null)
                            vpbh.Ma_bn = n.Mabn;
                        if (n.Tuyen != null && lupDoituong.Text == "BHYT")
                            vpbh.Tuyen = n.Tuyen.Value;
                        else
                            vpbh.Tuyen = -1;
                        if (n.Tuyen != null && n.CapCuu != null)
                        {
                            if (n.CapCuu == 0)// thường
                                if (n.Tuyen == 1)
                                    vpbh.Ma_lydo_vvien = 1;// đúng tuyến
                                else
                                    vpbh.Ma_lydo_vvien = 3;// trái tuyến
                            else// cấp cứu
                                vpbh.Ma_lydo_vvien = 2;
                        }

                        if (n.TenBNhan != null)
                            vpbh.Ho_ten = n.TenBNhan;
                        if (n.NSinh != null)
                            vpbh.NSinh = n.NSinh;
                        if (n.ThangSinh != null && n.NgaySinh != null)
                            vpbh.Ngay_sinh = n.NSinh.ToString() + (n.ThangSinh.ToString().Trim().Length == 1 ? ("0" + n.ThangSinh.ToString().Trim()) : n.ThangSinh.ToString()) + (n.NgaySinh.ToString().Trim().Length == 1 ? ("0" + n.NgaySinh.ToString().Trim()) : n.NgaySinh.ToString());
                        else
                            vpbh.Ngay_sinh = n.NSinh.ToString();
                        if (n.SThe != null)
                            vpbh.Ma_the = n.SThe;
                        vpbh.Gioi_tinh = Convert.ToBoolean(n.GTinh);
                        if (n.MaCS != null)
                            vpbh.Ma_dkbd = n.MaCS;
                        vpbh.Ma_cskcb = DungChung.Bien.MaBV;
                        if (n.MaICD != null)
                            vpbh.Ma_benh = n.MaICD;
                        vpbh.Capcuu = Convert.ToInt32(n.CapCuu);
                        vpbh.Ngay_vao = Convert.ToDateTime(n.Ngaykham);
                        vpbh.Ngay_ra = Convert.ToDateTime(n.Ngayra);
                        vpbh.T_thuoc = Convert.ToDouble(n.Thuoc);
                        vpbh.T_cdha = Convert.ToDouble(n.CDHA);
                        vpbh.T_kham = Convert.ToDouble(n.Congkham);
                        vpbh.T_xn = Convert.ToDouble(n.Xetnghiem);
                        vpbh.T_mau = Convert.ToDouble(n.Mau);
                        vpbh.T_pttt = Convert.ToDouble(n.TTPT);
                        vpbh.T_vtyt = Convert.ToDouble(n.VTYT);
                        vpbh.T_vtyt_tyle = Convert.ToDouble(n.VTYT_tl);
                        vpbh.T_dvkt_tyle = Convert.ToDouble(n.DVKT_tl);
                        vpbh.T_thuoc_tyle = Convert.ToDouble(n.Thuoc_tl);
                        vpbh.T_vchuyen = Convert.ToDouble(n.CPVanchuyen);
                        vpbh.T_bhtt = Convert.ToDouble(n.TienBH);
                        vpbh.T_bntt = Convert.ToDouble(n.TienBN);
                        vpbh.T_tongchi = Convert.ToDouble(n.Tongchi);
                        vpbh.NgayTT = Convert.ToDateTime(n.NgayTT);
                        if (n.DTuong != null)
                            vpbh.DTuong = n.DTuong;
                       // vpbh.IdBenhNhan = Convert.ToInt32(n.MaBNhan);
                        vpbh.Ngaykham = Convert.ToDateTime(n.Ngaykham);
                        vpbh.TongCong = Convert.ToDouble(n.Tongcong);
                        vpbh.Thanhtien = Convert.ToDouble(n.ThanhTien);

                        if (n.NoiTinh != null && lupDoituong.Text == "BHYT")
                            vpbh.NTinh = n.NoiTinh.Value;
                        else
                            vpbh.NTinh = -1;
                        vpbh.CPNgoaiBH = Convert.ToDouble(n.CPNgoaiBH);
                        vpbh.Soluot = 1;
                        if (n.MaDTuong != null)
                            vpbh.MaDTuong = n.MaDTuong;
                        if (n.MaDTuong != null)
                            vpbh.NhomDTuong = _lDTuong.Where(p => p.MaDTuong == n.MaDTuong).Count() > 0 ? _lDTuong.Single(p => p.MaDTuong == n.MaDTuong).Nhom : "";
                        if (n.KetQua != null)
                        {
                            if (n.KetQua == "Khỏi")
                                vpbh.Ket_qua_dtri = 1;
                            if (n.KetQua == "Đỡ|Giảm")
                                vpbh.Ket_qua_dtri = 2;
                            if (n.KetQua == "Không T.đổi")
                                vpbh.Ket_qua_dtri = 3;
                            if (n.KetQua == "Nặng hơn")
                                vpbh.Ket_qua_dtri = 4;
                            if (n.KetQua == "Tử vong")
                                vpbh.Ket_qua_dtri = 5;
                        }
                        if (n.Status != null)
                        {
                            if (n.Status == 1)
                                vpbh.Tinh_trang_rv = 2;
                            else if (n.Status == 2)
                                vpbh.Tinh_trang_rv = 1;
                            else
                                vpbh.Tinh_trang_rv = Convert.ToInt32(n.Status);
                        }
                        if (n.MaBV != null)
                            vpbh.Ma_noi_chuyen = n.MaBV;

                        _listVPBH.Add(vpbh);
                    }
               
               
                int stt = 1;
                #region
                //_listVPBH = (from c in _listVPBH
                //             group c by new
                //                 {
                //                     c.So_ngay_dtri,
                //                     c.Gt_the_tu,
                //                     c.Gt_the_den,
                //                     c.Dia_chi,
                //                     c.Ma_loaikcb,
                //                     c.Ma_bn,
                //                     c.Ma_lydo_vvien,
                //                     c.Ho_ten,
                //                     c.NSinh,
                //                     c.Ma_the,
                //                     c.Gioi_tinh,
                //                     c.Ma_cskcb,
                //                     c.Ma_dkbd,
                //                     c.Ma_benh,
                //                     c.Ngay_ra,
                //                     c.Ngay_vao,
                //                     c.NgayTT,
                //                     c.DTuong,
                //                     c.IdBenhNhan,
                //                     c.Ngaykham,
                //                     c.NTinh,
                //                     c.MaDTuong,
                //                     c.NhomDTuong,
                //                     c.Capcuu,
                //                    // c.CPNgoaiBH,
                //                     c.Soluot,
                //                     c.Tuyen,
                //                     c.Ket_qua_dtri,
                //                     c.Tinh_trang_rv,
                //                     c.Ngay_sinh,
                //                     c.Ma_khuvuc,
                //                     c.Ma_noi_chuyen,
                //                 } into kq
                //             select new Cls79_80.cl_79_80
                //                 {
                //                     STT = stt++,
                //                     So_ngay_dtri = kq.Key.So_ngay_dtri,
                //                     Gt_the_tu = kq.Key.Gt_the_tu,
                //                     Gt_the_den = kq.Key.Gt_the_den,
                //                     Dia_chi = kq.Key.Dia_chi,
                //                     Ma_loaikcb = kq.Key.Ma_loaikcb,
                //                     Ma_bn = kq.Key.Ma_bn,
                //                     Ma_lydo_vvien = kq.Key.Ma_lydo_vvien,
                //                     Ho_ten = kq.Key.Ho_ten,
                //                     NSinh = kq.Key.NSinh,
                //                     Ma_the = kq.Key.Ma_the,
                //                     Gioi_tinh = kq.Key.Gioi_tinh,
                //                     Ma_cskcb = kq.Key.Ma_cskcb,
                //                     Ma_dkbd = kq.Key.Ma_dkbd,
                //                     Ma_benh = kq.Key.Ma_benh,
                //                     Ngay_vao = kq.Key.Ngay_vao,
                //                     Ngay_ra = kq.Key.Ngay_ra,
                //                     NgayTT = kq.Key.NgayTT,
                //                     DTuong = kq.Key.DTuong,
                //                     IdBenhNhan = kq.Key.IdBenhNhan,
                //                     Ngaykham = kq.Key.Ngaykham,
                //                     NTinh = kq.Key.NTinh,
                //                     MaDTuong = kq.Key.MaDTuong,
                //                     NhomDTuong = kq.Key.NhomDTuong,
                //                     Capcuu = kq.Key.Capcuu,
                //                    // 
                //                     Ket_qua_dtri = kq.Key.Ket_qua_dtri,
                //                     Tinh_trang_rv = kq.Key.Tinh_trang_rv,
                //                     Ngay_sinh = kq.Key.Ngay_sinh,
                //                     Ma_khuvuc = kq.Key.Ma_khuvuc,
                //                     Ma_noi_chuyen = kq.Key.Ma_noi_chuyen,
                //                     MaKP = _MaKPc == "tc" ? "" : _MaKPc,
                //                     T_thuoc = kq.Sum(p => p.T_thuoc),
                //                     T_cdha = kq.Sum(p => p.T_cdha),
                //                     T_kham = kq.Sum(p => p.T_kham),
                //                     T_xn = kq.Sum(p => p.T_xn),
                //                     T_mau = kq.Sum(p => p.T_mau),
                //                     T_pttt = kq.Sum(p => p.T_pttt),
                //                     T_vtyt = kq.Sum(p => p.T_vtyt),
                //                     T_vtyt_tyle = kq.Sum(p => p.T_vtyt_tyle),
                //                     T_dvkt_tyle = kq.Sum(p => p.T_dvkt_tyle),
                //                     T_thuoc_tyle = kq.Sum(p => p.T_thuoc_tyle),
                //                     T_vchuyen = kq.Sum(p => p.T_vchuyen),
                //                     CPNgoaiBH = kq.Sum(p=> p.CPNgoaiBH),
                //                     T_bhtt = kq.Sum(p => p.T_bhtt),
                //                     T_bntt = kq.Sum(p => p.T_bntt),
                //                     T_tongchi = kq.Sum(p => p.T_tongchi),
                //                     Soluot = kq.Key.Soluot,
                //                     Tuyen = kq.Key.Tuyen,
                //                 }).OrderBy(p => p.IdBenhNhan).ToList();
                #endregion
                //frm_BNSai._ktbnsai(_listVPBH);
                if (_listVPBH.Count > 0)
                {
                    frmIn frm = new frmIn();
                    #region in chi tiết
                    if (rad_MauIn.SelectedIndex == 0)
                    {
                        if (cbosx.SelectedIndex == 0) // order by Mã bệnh nhân
                        {
                            _listVPBH = _listVPBH.OrderBy(p => p.IdBenhNhan).ToList();
                        }
                        else
                        {
                            if (cbosx.SelectedIndex == 1) //  order by ngày thanh toán
                            {
                                _listVPBH = _listVPBH.OrderBy(p => p.NTinh).ThenBy(p => p.Ma_lydo_vvien).ThenBy(p => p.NgayTT).ToList();
                            }
                            else
                            {
                                if (cbosx.SelectedIndex == 2)// order by theo Ngày ra
                                {
                                    _listVPBH = _listVPBH.OrderBy(p => p.NTinh).ThenBy(p => p.Ma_lydo_vvien).ThenBy(p => p.Ngay_ra).ToList();
                                }
                                else// order by theo Nhóm đối tượng
                                {
                                    _listVPBH = _listVPBH.OrderBy(p => p.NhomDTuong).ThenBy(p => p.MaDTuong).ThenBy(p => p.Ngay_ra).ToList();
                                }
                            }
                        }
                        string kieuNT = cboKieuNT.Text; // kiểu định dạng ngày tháng 
                        BaoCao.Rep_80a_HD_1399 rep = new BaoCao.Rep_80a_HD_1399(2);
                        rep.Dtuong.Value = lupDoituong.Text;
                        rep.Ngaythang.Value = theoquy();
                        double st = 0;
                        if (lupDoituong.Text == "BHYT" && ckTLBN.Checked == false)
                        {
                            rep.Title.Value = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh nội trú đề nghị thanh toán").ToUpper();
                            rep.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                            st = _listVPBH.Sum(p => p.T_bhtt);
                        }
                        else
                        {
                            rep.Title.Value = ("Danh sách người bệnh khám chữa bệnh nội trú thanh toán").ToUpper();
                            rep.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                            st = _listVPBH.Sum(p => p.T_bntt);
                        }
                        st = Math.Round(st, 0);
                        rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                        if (_MaKPc != null && _MaKPc != 0)
                            rep.paramKhoaPhong.Value = "  Khoa phòng: " + lupKhoaphong.Text; ;
                        rep.DataSource = _listVPBH.ToList();
                        rep.BindingData(kieuNT,2);
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                        bool chonFont = false;
                        if (rdFont.SelectedIndex == 0)
                            chonFont = true;
                        else
                            chonFont = false;
                        string font = "TCVN3";
                        if (Xuatex.Checked)
                        {
                            if (ck3360.Checked)
                            {
                                // if (Cls79_80.xuatExcel( _listVPBH, txtFilePath.Text,chonFont, font))
                                if (Cls79_80.xuatExcelRange(_listVPBH, txtFilePath.Text, chonFont, font,true))
                                    MessageBox.Show("Xuất thành công!");
                                else
                                    MessageBox.Show("Lỗi!");
                            }
                            else
                            {
                                if (Cls79_80.xuatExcelRange_Old(_listVPBH, txtFilePath.Text, chonFont, font,true))
                                    MessageBox.Show("Xuất thành công!");
                                else
                                    MessageBox.Show("Lỗi!");
                            }
                        }

                    }
                    #endregion
                    #region in tổng hợp
                    else
                    {
                        BaoCao.Rep_80aTH_1399 rep = new BaoCao.Rep_80aTH_1399();
                        rep.Ngaythang.Value = theoquy();
                        rep.Dtuong.Value = lupDoituong.Text;
                        var q = (from l in _listVPBH
                                 group l by new { l.Tuyen, l.NTinh } into kq
                                 select new
                                     {
                                         NTinh = kq.Key.NTinh,
                                         Tuyen = kq.Key.Tuyen == 1 ? "Đúng tuyến" : "Trái tuyến",
                                         STT = kq.Key.Tuyen == 1 ? "I" : "II",
                                         So_ngay_dtri = kq.Sum(p => p.So_ngay_dtri),
                                         T_thuoc = kq.Sum(p => p.T_thuoc),
                                         T_cdha = kq.Sum(p => p.T_cdha),
                                         T_kham = kq.Sum(p => p.T_kham),
                                         T_xn = kq.Sum(p => p.T_xn),
                                         T_mau = kq.Sum(p => p.T_mau),
                                         T_pttt = kq.Sum(p => p.T_pttt),
                                         T_vtyt = kq.Sum(p => p.T_vtyt),
                                         T_vtyt_tyle = kq.Sum(p => p.T_vtyt_tyle),
                                         T_dvkt_tyle = kq.Sum(p => p.T_dvkt_tyle),
                                         T_thuoc_tyle = kq.Sum(p => p.T_thuoc_tyle),
                                         T_vchuyen = kq.Sum(p => p.T_vchuyen),
                                         T_bhtt = kq.Sum(p => p.T_bhtt),
                                         T_bntt = kq.Sum(p => p.T_bntt),
                                         T_tongchi = kq.Sum(p => p.T_tongchi),
                                         IdBenhNhan = kq.Sum(p => p.IdBenhNhan),
                                         TongCong = kq.Sum(p => p.TongCong),
                                         Thanhtien = kq.Sum(p => p.Thanhtien),
                                         Soluot = kq.Sum(p => p.Soluot)
                                     }).OrderBy(p => p.NTinh).ThenBy(p => p.Tuyen).ToList();

                        double st = 0;
                        if (lupDoituong.Text == "BHYT" && ckTLBN.Checked == false)
                        {
                            rep.Title.Value = ("Danh sách đề nghị thanh toán chi phí KCB nội trú").ToUpper();
                            rep.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                            st = _listVPBH.Sum(p => p.T_bhtt);
                        }
                        else
                        {
                            rep.Title.Value = ("Danh sách thanh toán chi phí KCB nội trú của bệnh nhân").ToUpper();
                            rep.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                            st = _listVPBH.Sum(p => p.T_bntt);
                        }
                        st = Math.Round(st, 0);
                        rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                        if (_MaKPc != null && _MaKPc != 0)
                            rep.paramKhoaPhong.Value = "Khoa phòng: " + lupKhoaphong.Text;
                        rep.DataSource = q;
                        rep.BindingData(2);
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    #endregion

                }
                else
                {
                    MessageBox.Show("Không có dữ liệu");
                }
            }
        }

        private void xuatExcel(List<Cls79_80.cl_79_80> _listVPBH)
        {
            throw new NotImplementedException();
        }



        private void lupKhoaphong_EditValueChanged(object sender, EventArgs e)
        {

        }

        List<KPhong> _lKphong = new List<KPhong>();
        List<DichVu> _ldv = new List<DichVu>();
        private void frm_80ct_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _ldv = _dataContext.DichVus.ToList();
            _lKphong = _dataContext.KPhongs
                .Where(p => p.PLoai == ("Lâm sàng"))
                .ToList();
            _lKphong.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKhoaphong.Properties.DataSource = _lKphong;
            lupKhoaphong.EditValue = 0;
            lupNgaytu.EditValue = System.DateTime.Now.Date;
            lupngayden.EditValue = System.DateTime.Now.Date;
            lupNgaytu.Focus();
            List<DTBN> _lDTBN = new List<DTBN>();
            _lDTBN = _dataContext.DTBNs.Where(p => p.Status == 1).ToList();
            lupDoituong.Properties.DataSource = _lDTBN;
            lupDoituong.Text = "BHYT";
            Xuatex_CheckedChanged(sender, e);

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
        SaveFileDialog dialog = new SaveFileDialog();
        private void btnChonFilePath_Click(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            dialog.InitialDirectory = "C:\\";
            dialog.Filter = "Excel files (*.xls or *.xlsx)|*.xls;*.xlsx";
            dialog.FilterIndex = 1;
            dialog.FileName = "Bieu80_" + DungChung.Bien.MaBV + "_" + _ngay.Year + "_" + _ngay.Month + ".xls";
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dialog.FileName;
            }
        }

        private void Xuatex_CheckedChanged(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            txtFilePath.Enabled = Xuatex.Checked;
            btnChonFilePath.Enabled = Xuatex.Checked;
            rdFont.Enabled = Xuatex.Checked; ;
            ck3360.Enabled = Xuatex.Checked;
            if (Xuatex.Checked)
                txtFilePath.Text = "C:\\Bieu80_" + DungChung.Bien.MaBV + "_" + _ngay.Year + "_" + _ngay.Month + ".xls";
            else
                txtFilePath.Text = "";
        }

        private void ckTLBH_CheckedChanged(object sender, EventArgs e)
        {
            if (ckTLBH.Checked)
                ckTLBN.Checked = false;
        }

        private void ckTLBN_CheckedChanged(object sender, EventArgs e)
        {
            if (ckTLBN.Checked)
                ckTLBH.Checked = false;
        }

        private void rdTrongBH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lupDoituong_EditValueChanged(object sender, EventArgs e)
        {
            if (lupDoituong.Text == "Dịch vụ")
            {
                rdTrongBH.SelectedIndex = 0;
                rdTrongBH.Properties.ReadOnly = true;
            }
            else
            {
                rdTrongBH.SelectedIndex = 1;
                rdTrongBH.Properties.ReadOnly = false;
            }
        }


    }
}
