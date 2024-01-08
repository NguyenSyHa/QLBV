using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV.ChucNang;
using System.Threading.Tasks;
using QLBV.DataCommunication.Models;
using QLBV_Database.Common;
using QLBV.Utilities.Commons;
using QLBV.DaTa.BHYT.Circular130;

namespace QLBV.BHYT
{
    public partial class us_Export_XML_2348 : XtraUserControl
    {
        private QLBVEntities _dataContext;

        public us_Export_XML_2348()
        {
            InitializeComponent();
            _dataContext = EntityDbContext.DbContext;
        }

        public static string user = "", pass = "";
        int idThuoc = -1, idMau = -1, idXN = -1, idCDHA = -1, idTTPT = -1, idCongKham = -1, idDVKTC = -1, idVTYT = -1, idNgayGiuongNoiTru = -1, idNgayGiuongNgoaiTru = -1, idChiPhiVC = -1, idVTTT = -1, idThuocUngThuCTG = -1, idHoaChat = -1;

        ExportFileCircular130 _exportFileCircular130;
        public ExportFileCircular130 ExportFileCircular130
        {
            get
            {
                if (_exportFileCircular130 == null)
                    _exportFileCircular130 = new ExportFileCircular130();

                return _exportFileCircular130;
            }
        }

        IList<KPhong> _kPhongs;
        public IList<KPhong> KPhongs
        {
            get => _kPhongs;
            set
            {
                if (_kPhongs != value)
                {
                    _kPhongs = value;
                    _kPhongs.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });

                    lupKhoaphong.Properties.DataSource = value.OrderBy(p => p.PLoai).ThenBy(p => p.TenKP).ToList();
                    lupKhoaphong.EditValue = 0;
                }
            }
        }

        IList<DichVu> _dichVus;
        public IList<DichVu> DichVus
        {
            get => _dichVus;
            set => _dichVus = value;
        }

        #region Tìm kiếm
        List<DungChung.Cls79_80.cl_79_80> _listVPBH = new List<DungChung.Cls79_80.cl_79_80>();

        public void TimKiem()
        {
            try
            {
                _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                _listVPBH.Clear();
                int id_DoiTuongKham = -1;
                var dtuong = _dataContext.DTBNs.Where(p => p.HTTT == 1).Select(p => p.IDDTBN).ToList();
                if (dtuong.Count > 0)
                    id_DoiTuongKham = dtuong.First();
                rad_loc.SelectedIndex = 2;
                int _noitru = -1;
                _noitru = radNoiTru.SelectedIndex;
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
                int _ngaytt = cbo_KieuNgay.SelectedIndex;
                int _MaKPc = 0;

                if (!string.IsNullOrEmpty(lupKhoaphong.Text))
                    _MaKPc = Convert.ToInt32(lupKhoaphong.EditValue);

                bool _export = false;
                if (rad_ExPort.SelectedIndex == 1)
                    _export = true;

                var q22 = (from bn in _dataContext.BenhNhans
                           join rv in _dataContext.RaViens.Where(p => DungChung.Bien.MaBV == "01049" ? (p.Status == 1 ? p.MaBVC != "01071" : true) : true) on bn.MaBNhan equals rv.MaBNhan
                           join vp in _dataContext.VienPhis on rv.MaBNhan equals vp.MaBNhan
                           join vpct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                           where (_ngaytt == 2 ? (vp.NgayDuyet >= ngaytu && vp.NgayDuyet <= ngayden) : (_ngaytt == 1 ? (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden) : (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)))

                           select new
                           {
                               vp.NgayGuiBHXH,
                               bn,
                               vp.LyDo,
                               vp.MaGD_BHXH,
                               vp.ExportBYT,
                               vp.Export,
                               vp.ExportBHXH,
                               vpct.MaDV,//MaDV = 0,// vpct.MaDV,
                               vp.MaBNhan,
                               vpct.TrongBH,//TrongBH = -1,//vpct.TrongBH,
                               bn.DTNT,
                               //vpct.MaKP,//MaKP = 0,    //  vpct.MaKP,
                               rv.MaKP,
                               rv.MaICD,
                               rv.NgayVao,
                               rv.NgayRa,
                               rv.SoNgaydt,
                               rv.Status,
                               rv.KetQua,
                               rv.ChanDoan,
                               vpct.ThanhTien,//ThanhTien = 0,// vpct.ThanhTien,
                               vpct.TienBN,//TienBN = 0,//vpct.TienBN,
                               vpct.TienBH,//TienBH = 0,// vpct.TienBH,
                               vp.NgayTT,
                               bn.MaKCB,
                               bn.NNhap
                           }).ToList().ToList();

                var q2 = (from a in q22.Where(p => p.MaKCB == DungChung.Bien.MaBV).Where(p => rad_FileGui.SelectedIndex == 0 ? (p.ExportBYT == _export) : (rad_FileGui.SelectedIndex == 1 ? p.ExportBHXH == _export : p.Export == _export)) select a).ToList();

                var q4 = (from a in q2
                          where ((_noitru == 2 ? true : a.bn.NoiTru == _noitru) && a.bn.IDDTBN == id_DoiTuongKham)
                          select new
                          {
                              a.NgayGuiBHXH,
                              ExPort = false,
                              a.LyDo,
                              a.MaGD_BHXH,
                              a.TrongBH,
                              a.MaKP,
                              a.bn.DChi,
                              a.bn.HanBHDen,
                              a.bn.HanBHTu,
                              a.bn.TuyenDuoi,
                              a.bn.DTNT,
                              a.bn.DTuong,
                              a.bn.NoiTru,
                              // bn.MaBNhan,
                              a.bn.NoiTinh,
                              a.bn.Tuyen,
                              a.bn.MaBNhan,
                              a.bn.TenBNhan,
                              a.bn.NamSinh,
                              a.bn.NgaySinh,
                              a.bn.ThangSinh,
                              a.bn.SThe,
                              a.bn.GTinh,
                              a.bn.MaCS,
                              a.bn.MaDTuong,
                              a.bn.CapCuu,
                              a.MaICD,
                              ChanDoan = (a.MaICD != null && a.MaICD != "") ? a.ChanDoan : "",
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
                              a.bn.Tuoi,
                              a.bn.KhuVuc,
                              a.bn.MaBV,
                              a.NNhap
                          }).OrderBy(p => p.MaBNhan).ToList();
                var q3 = (from a in q4
                          join dv in _dataContext.DichVus on a.MaDV equals dv.MaDV

                          where (a.TrongBH == 1)
                          where (_MaKPc == 0 ? true : a.MaKP == _MaKPc)
                          group new { a, dv } by new { a.NgayGuiBHXH, a.MaKP, a.MaGD_BHXH, a.LyDo, a.NNhap, a.ExPort, a.HanBHDen, a.HanBHTu, a.DChi, a.SoNgaydt, a.DTNT, a.TuyenDuoi, a.NgayTT, a.DTuong, a.NoiTru, a.TrongBH, a.NoiTinh, a.MaBNhan, a.TenBNhan, a.NamSinh, a.NgaySinh, a.ThangSinh, a.GTinh, a.SThe, a.MaCS, a.Tuyen, a.NgayVao, a.MaICD, a.ChanDoan, a.NgayRa, a.MaDTuong, a.CapCuu, a.KetQua, a.Status, a.Tuoi, a.KhuVuc, a.MaBV } into kq
                          select new
                          {
                              kq.Key.NgayGuiBHXH,
                              kq.Key.ExPort,
                              MaKP = MaKPQD(kq.Key.MaKP),
                              kq.Key.LyDo,
                              kq.Key.MaGD_BHXH,
                              kq.Key.SoNgaydt,
                              kq.Key.DTuong,
                              kq.Key.NoiTru,
                              kq.Key.TrongBH,
                              kq.Key.TuyenDuoi,
                              kq.Key.NNhap,
                              //kq.Key.MaBNhan,
                              kq.Key.NgayTT,
                              kq.Key.MaDTuong,
                              kq.Key.CapCuu,
                              kq.Key.NgaySinh,
                              kq.Key.ThangSinh,
                              kq.Key.KhuVuc,
                              kq.Key.MaBV,
                              kq.Key.KetQua,
                              kq.Key.DTNT,
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
                              ChanDoan = kq.Key.ChanDoan,
                              Ngaykham = kq.Key.NgayVao,
                              Ngayra = kq.Key.NgayRa,
                              Tuoi = kq.Key.Tuoi,
                              BHtu = kq.Key.HanBHTu,
                              BHden = kq.Key.HanBHDen,
                              Diachi = kq.Key.DChi,
                              Mabn = kq.Key.MaBNhan,
                              Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Where(p => p.dv.BHTT == 100).Sum(p => p.a.ThanhTien),
                              CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.a.ThanhTien) + kq.Where(p => p.dv.IDNhom == 3).Sum(p => p.a.ThanhTien),
                              TienGiuong = _noitru == 0 ? kq.Where(p => p.dv.IDNhom == idNgayGiuongNgoaiTru).Sum(p => p.a.ThanhTien) : kq.Where(p => p.dv.IDNhom == idNgayGiuongNoiTru).Sum(p => p.a.ThanhTien),
                              //Congkham = _noitru == 1 ? (kq.Where(p => p.dv.IDNhom == idNgayGiuong).Sum(p => p.a.ThanhTien)) : (kq.Where(p => p.dv.IDNhom == idCongKham).Sum(p => p.a.ThanhTien)),
                              Congkham = kq.Where(p => p.dv.IDNhom == idCongKham).Sum(p => p.a.ThanhTien),
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
                    DungChung.Cls79_80.cl_79_80 vpbh = new DungChung.Cls79_80.cl_79_80();
                    if (n.NgayGuiBHXH != null)
                        vpbh.NgayGuiBHXH = n.NgayGuiBHXH.Value;
                    if (n.Tuyen != null)
                        vpbh.Tuyen = n.Tuyen.Value;
                    vpbh.Status = n.ExPort;
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
                        {
                            if (n.DTNT == true)
                                vpbh.Ma_loaikcb = 2;
                            else
                                vpbh.Ma_loaikcb = 1;
                        }
                    }
                    if (n.KhuVuc != null)
                        vpbh.Ma_khuvuc = n.KhuVuc;
                    vpbh.Ma_bn = n.Mabn;
                    if (n.Tuyen != null)
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
                        vpbh.Ngay_sinh = n.NSinh == null ? "" : n.NSinh.ToString();
                    if (n.SThe != null)
                        vpbh.Ma_the = n.SThe;
                    vpbh.Gioi_tinh = Convert.ToBoolean(n.GTinh);
                    if (n.MaCS != null)
                        vpbh.Ma_dkbd = n.MaCS;
                    vpbh.Ma_cskcb = DungChung.Bien.MaBV;
                    if (n.MaICD != null)
                        vpbh.Ma_benh = n.MaICD;
                    vpbh.Capcuu = Convert.ToInt32(n.CapCuu);
                    vpbh.Ngay_vao = Convert.ToDateTime(n.NNhap);
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
                    vpbh.T_giuong = Convert.ToDouble(n.TienGiuong);
                    double tongtien = 0;
                    tongtien = n.Thuoc + n.CDHA + n.Congkham + n.TienGiuong + n.Xetnghiem + n.Mau + n.TTPT + n.VTYT + n.VTYT_tl + n.DVKT_tl + n.Thuoc_tl + n.CPVanchuyen;
                    vpbh.T_tongchi = Math.Round(tongtien, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);
                    vpbh.NgayTT = Convert.ToDateTime(n.NgayTT);

                    if (n.DTuong != null)
                        vpbh.DTuong = n.DTuong;

                    vpbh.Ngaykham = Convert.ToDateTime(n.Ngaykham);
                    vpbh.TongCong = Convert.ToDouble(n.Tongcong);
                    vpbh.Thanhtien = Convert.ToDouble(n.ThanhTien);

                    if (n.NoiTinh != null)
                        vpbh.NTinh = n.NoiTinh.Value;

                    vpbh.Soluot = 1;
                    if (n.MaDTuong != null)
                        vpbh.MaDTuong = n.MaDTuong;

                    if (n.KetQua != null)
                    {
                        if (n.KetQua == "Khỏi")
                            vpbh.Ket_qua_dtri = 1;
                        else if (n.KetQua == "Đỡ|Giảm")
                            vpbh.Ket_qua_dtri = 2;
                        else if (n.KetQua == "Không T.đổi")
                            vpbh.Ket_qua_dtri = 3;
                        else if (n.KetQua == "Nặng hơn")
                            vpbh.Ket_qua_dtri = 4;
                        else if (n.KetQua == "Tử vong")
                            vpbh.Ket_qua_dtri = 5;
                        else
                            vpbh.Ket_qua_dtri = 2;
                    }
                    else
                        vpbh.Ket_qua_dtri = 2;
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
                    vpbh.MaKhoaTongKet = n.MaKP;
                    vpbh.Chandoan = n.ChanDoan;
                    vpbh.Lydo_xt = n.LyDo ?? "" + n.MaGD_BHXH ?? "";

                    _listVPBH.Add(vpbh);
                }
                grc_Export_XML_2348.DataSource = null;
                grc_Export_XML_2348.DataSource = _listVPBH;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm! " + ex.Message);
            }
        }

        public void Search()
        {
            CursorState.SetBusyState();

            try
            {
                _listVPBH.Clear();

                int id_DoiTuongKham = -1;
                var dtuong = _dataContext.DTBNs.Where(p => p.HTTT == 1).Select(p => p.IDDTBN).ToList();
                if (dtuong.Count > 0)
                    id_DoiTuongKham = dtuong.First();

                rad_loc.SelectedIndex = 2;
                int _noitru = -1;
                _noitru = radNoiTru.SelectedIndex;
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
                int _ngaytt = cbo_KieuNgay.SelectedIndex;
                int _maKPc = 0;

                if (!string.IsNullOrEmpty(lupKhoaphong.Text))
                    _maKPc = Convert.ToInt32(lupKhoaphong.EditValue);

                bool _export = false;
                if (rad_ExPort.SelectedIndex == 1)
                    _export = true;

                var data = (from bn in _dataContext.BenhNhans.Where(p => p.MaKCB == DungChung.Bien.MaBV)
                            join rv in _dataContext.RaViens.Where(p => DungChung.Bien.MaBV == "01049" ? (p.Status == 1 ? p.MaBVC != "01071" : true) : true) on bn.MaBNhan equals rv.MaBNhan
                            join vp in _dataContext.VienPhis on rv.MaBNhan equals vp.MaBNhan
                            join vpct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                            where (_ngaytt == 2 ? (vp.NgayDuyet >= ngaytu && vp.NgayDuyet <= ngayden) : (_ngaytt == 1 ? (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden) : (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)))
                            where (rad_FileGui.SelectedIndex == 0 ? (vp.ExportBYT == _export) : (rad_FileGui.SelectedIndex == 1 ? vp.ExportBHXH == _export : vp.Export == _export))
                            where ((_noitru == 2 ? true : bn.NoiTru == _noitru) && bn.IDDTBN == id_DoiTuongKham)
                            select new
                            {
                                vp.NgayGuiBHXH,
                                ExPort = false,
                                vp.LyDo,
                                vp.MaGD_BHXH,
                                vpct.TrongBH,
                                vp.MaKP,
                                bn.DChi,
                                bn.HanBHDen,
                                bn.HanBHTu,
                                bn.TuyenDuoi,
                                bn.DTNT,
                                bn.DTuong,
                                bn.NoiTru,
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
                                rv.MaICD,
                                ChanDoan = (rv.MaICD != null && rv.MaICD != "") ? rv.ChanDoan : "",
                                rv.NgayVao,
                                rv.NgayRa,
                                rv.SoNgaydt,
                                rv.Status,
                                rv.KetQua,
                                vpct.MaDV,
                                vpct.ThanhTien,
                                vpct.TienBN,
                                vpct.TienBH,
                                vp.NgayTT,
                                bn.Tuoi,
                                bn.KhuVuc,
                                bn.MaBV,
                                bn.NNhap
                            }).ToList();

                var results = (from a in data
                               join dv in DichVus on a.MaDV equals dv.MaDV
                               where a.TrongBH == 1 && (_maKPc == 0 ? true : a.MaKP == _maKPc)
                               group new { a, dv } by new
                               {
                                   a.NgayGuiBHXH,
                                   a.MaKP,
                                   a.MaGD_BHXH,
                                   a.LyDo,
                                   a.NNhap,
                                   a.ExPort,
                                   a.HanBHDen,
                                   a.HanBHTu,
                                   a.DChi,
                                   a.SoNgaydt,
                                   a.DTNT,
                                   a.TuyenDuoi,
                                   a.NgayTT,
                                   a.DTuong,
                                   a.NoiTru,
                                   a.TrongBH,
                                   a.NoiTinh,
                                   a.MaBNhan,
                                   a.TenBNhan,
                                   a.NamSinh,
                                   a.NgaySinh,
                                   a.ThangSinh,
                                   a.GTinh,
                                   a.SThe,
                                   a.MaCS,
                                   a.Tuyen,
                                   a.NgayVao,
                                   a.MaICD,
                                   a.ChanDoan,
                                   a.NgayRa,
                                   a.MaDTuong,
                                   a.CapCuu,
                                   a.KetQua,
                                   a.Status,
                                   a.Tuoi,
                                   a.KhuVuc,
                                   a.MaBV
                               } into kq
                               select new
                               {
                                   kq.Key.NgayGuiBHXH,
                                   kq.Key.ExPort,
                                   MaKP = kq.Key.MaKP ?? 0,// MaKPQD(kq.Key.MaKP ?? 0),
                                   MaKhoaTongKet = "",
                                   kq.Key.LyDo,
                                   kq.Key.MaGD_BHXH,
                                   kq.Key.SoNgaydt,
                                   kq.Key.DTuong,
                                   kq.Key.NoiTru,
                                   kq.Key.TrongBH,
                                   kq.Key.TuyenDuoi,
                                   kq.Key.NNhap,
                                   kq.Key.NgayTT,
                                   kq.Key.MaDTuong,
                                   kq.Key.CapCuu,
                                   kq.Key.NgaySinh,
                                   kq.Key.ThangSinh,
                                   kq.Key.KhuVuc,
                                   kq.Key.MaBV,
                                   kq.Key.KetQua,
                                   kq.Key.DTNT,
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
                                   ChanDoan = kq.Key.ChanDoan,
                                   Ngaykham = kq.Key.NgayVao,
                                   Ngayra = kq.Key.NgayRa,
                                   Tuoi = kq.Key.Tuoi,
                                   BHtu = kq.Key.HanBHTu,
                                   BHden = kq.Key.HanBHDen,
                                   Diachi = kq.Key.DChi,
                                   Mabn = kq.Key.MaBNhan,
                                   Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Where(p => p.dv.BHTT == 100).Sum(p => p.a.ThanhTien),
                                   CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.a.ThanhTien) + kq.Where(p => p.dv.IDNhom == 3).Sum(p => p.a.ThanhTien),
                                   TienGiuong = _noitru == 0 ? kq.Where(p => p.dv.IDNhom == idNgayGiuongNgoaiTru).Sum(p => p.a.ThanhTien) : kq.Where(p => p.dv.IDNhom == idNgayGiuongNoiTru).Sum(p => p.a.ThanhTien),
                                   Congkham = kq.Where(p => p.dv.IDNhom == idCongKham).Sum(p => p.a.ThanhTien),
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
                               }).OrderBy(o => o.MaBNhan).ToList();

                foreach (var result in results)
                {
                    DungChung.Cls79_80.cl_79_80 vpbh = new DungChung.Cls79_80.cl_79_80();

                    if (result.NgayGuiBHXH != null)
                        vpbh.NgayGuiBHXH = result.NgayGuiBHXH.Value;
                    if (result.Tuyen != null)
                        vpbh.Tuyen = result.Tuyen.Value;
                    vpbh.Status = result.ExPort;
                    if (result.SoNgaydt != null)
                        vpbh.So_ngay_dtri = result.SoNgaydt.Value;
                    if (result.BHtu != null)
                        vpbh.Gt_the_tu = result.BHtu.Value;
                    if (result.BHden != null)
                        vpbh.Gt_the_den = result.BHden.Value;
                    if (result.Diachi != null)
                        vpbh.Dia_chi = result.Diachi;
                    if (result.NoiTru != null)
                    {
                        if (result.NoiTru == 1)
                            vpbh.Ma_loaikcb = 3;
                        else
                        {
                            if (result.DTNT == true)
                                vpbh.Ma_loaikcb = 2;
                            else
                                vpbh.Ma_loaikcb = 1;
                        }
                    }
                    if (result.KhuVuc != null)
                        vpbh.Ma_khuvuc = result.KhuVuc;
                    vpbh.Ma_bn = result.Mabn;
                    if (result.Tuyen != null)
                        vpbh.Tuyen = result.Tuyen.Value;
                    else
                        vpbh.Tuyen = -1;
                    if (result.Tuyen != null && result.CapCuu != null)
                    {
                        if (result.CapCuu == 0)// thường
                            if (result.Tuyen == 1)
                                vpbh.Ma_lydo_vvien = 1;// đúng tuyến
                            else
                                vpbh.Ma_lydo_vvien = 3;// trái tuyến
                        else// cấp cứu
                            vpbh.Ma_lydo_vvien = 2;
                    }

                    if (result.TenBNhan != null)
                        vpbh.Ho_ten = result.TenBNhan;
                    if (result.NSinh != null)
                        vpbh.NSinh = result.NSinh;
                    if (result.ThangSinh != null && result.NgaySinh != null)
                        vpbh.Ngay_sinh = result.NSinh.ToString() + (result.ThangSinh.ToString().Trim().Length == 1 ? ("0" + result.ThangSinh.ToString().Trim()) : result.ThangSinh.ToString()) + (result.NgaySinh.ToString().Trim().Length == 1 ? ("0" + result.NgaySinh.ToString().Trim()) : result.NgaySinh.ToString());
                    else
                        vpbh.Ngay_sinh = result.NSinh == null ? "" : result.NSinh.ToString();
                    if (result.SThe != null)
                        vpbh.Ma_the = result.SThe;
                    vpbh.Gioi_tinh = Convert.ToBoolean(result.GTinh);
                    if (result.MaCS != null)
                        vpbh.Ma_dkbd = result.MaCS;
                    vpbh.Ma_cskcb = DungChung.Bien.MaBV;
                    if (result.MaICD != null)
                        vpbh.Ma_benh = result.MaICD;
                    vpbh.Capcuu = Convert.ToInt32(result.CapCuu);
                    vpbh.Ngay_vao = Convert.ToDateTime(result.NNhap);
                    vpbh.Ngay_ra = Convert.ToDateTime(result.Ngayra);
                    vpbh.T_thuoc = Convert.ToDouble(result.Thuoc);
                    vpbh.T_cdha = Convert.ToDouble(result.CDHA);
                    vpbh.T_kham = Convert.ToDouble(result.Congkham);
                    vpbh.T_xn = Convert.ToDouble(result.Xetnghiem);
                    vpbh.T_mau = Convert.ToDouble(result.Mau);
                    vpbh.T_pttt = Convert.ToDouble(result.TTPT);
                    vpbh.T_vtyt = Convert.ToDouble(result.VTYT);
                    vpbh.T_vtyt_tyle = Convert.ToDouble(result.VTYT_tl);
                    vpbh.T_dvkt_tyle = Convert.ToDouble(result.DVKT_tl);
                    vpbh.T_thuoc_tyle = Convert.ToDouble(result.Thuoc_tl);
                    vpbh.T_vchuyen = Convert.ToDouble(result.CPVanchuyen);
                    vpbh.T_bhtt = Convert.ToDouble(result.TienBH);
                    vpbh.T_bntt = Convert.ToDouble(result.TienBN);
                    vpbh.T_giuong = Convert.ToDouble(result.TienGiuong);
                    double tongtien = result.Thuoc + result.CDHA + result.Congkham + result.TienGiuong + result.Xetnghiem + result.Mau + result.TTPT + result.VTYT + result.VTYT_tl + result.DVKT_tl + result.Thuoc_tl + result.CPVanchuyen;
                    vpbh.T_tongchi = Math.Round(tongtien, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);
                    vpbh.NgayTT = Convert.ToDateTime(result.NgayTT);

                    if (result.DTuong != null)
                        vpbh.DTuong = result.DTuong;

                    vpbh.Ngaykham = Convert.ToDateTime(result.Ngaykham);
                    vpbh.TongCong = Convert.ToDouble(result.Tongcong);
                    vpbh.Thanhtien = Convert.ToDouble(result.ThanhTien);

                    if (result.NoiTinh != null)
                        vpbh.NTinh = result.NoiTinh.Value;

                    vpbh.Soluot = 1;

                    if (result.MaDTuong != null)
                        vpbh.MaDTuong = result.MaDTuong;

                    if (result.KetQua != null)
                    {
                        if (result.KetQua == "Khỏi")
                            vpbh.Ket_qua_dtri = 1;
                        else if (result.KetQua == "Đỡ|Giảm")
                            vpbh.Ket_qua_dtri = 2;
                        else if (result.KetQua == "Không T.đổi")
                            vpbh.Ket_qua_dtri = 3;
                        else if (result.KetQua == "Nặng hơn")
                            vpbh.Ket_qua_dtri = 4;
                        else if (result.KetQua == "Tử vong")
                            vpbh.Ket_qua_dtri = 5;
                        else
                            vpbh.Ket_qua_dtri = 2;
                    }
                    else
                        vpbh.Ket_qua_dtri = 2;

                    if (result.Status != null)
                    {
                        if (result.Status == 1)
                            vpbh.Tinh_trang_rv = 2;
                        else if (result.Status == 2)
                            vpbh.Tinh_trang_rv = 1;
                        else
                            vpbh.Tinh_trang_rv = Convert.ToInt32(result.Status);
                    }

                    if (result.MaBV != null)
                        vpbh.Ma_noi_chuyen = result.MaBV;

                    vpbh.MaKhoaTongKet = MaKPQD(result.MaKP); //result.MaKP;
                    vpbh.Chandoan = result.ChanDoan;
                    vpbh.Lydo_xt = result.LyDo ?? "" + result.MaGD_BHXH ?? "";

                    _listVPBH.Add(vpbh);
                }

                grc_Export_XML_2348.DataSource = null;
                grc_Export_XML_2348.DataSource = _listVPBH;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm! " + ex.Message);
            }
        }

        #endregion
        public int CheckNoiNgoaiTru(int mabn)
        {
            var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == mabn).Select(p => p.NoiTru).FirstOrDefault();
            if (bn != null)
                return bn.Value;
            return -1;
        }

        private string MaKPQD(int _maKPc)
        {
            return KPhongs.FirstOrDefault(p => p.MaKP == _maKPc)?.MaQD;
        }

        private void us_ThamDinh_Load(object sender, EventArgs e)
        {
            InitData();

            if (DungChung.Bien.MaBV == "30009")
            {
                colTongcong.Visible = true;
            }
            else
                colTongcong.Visible = false;
            txtFilePath.Text = "C:\\Bieu" + "_79_80" + "_" + DungChung.Bien.MaBV + "_" + System.DateTime.Now.Year + "_" + System.DateTime.Now.Month + ".xls";
        }

        public void InitData()
        {
            KPhongs = _dataContext.KPhongs.Where(p => p.PLoai == ("Lâm sàng") || p.PLoai == ("Phòng khám")).ToList();
            DichVus = _dataContext.DichVus.ToList();

            dtTimTuNgay.DateTime = System.DateTime.Now;
            dtTimDenNgay.DateTime = System.DateTime.Now;

            GetServiceGroup();
        }

        private void GetServiceGroup()
        {
            var tenNhom = _dataContext.NhomDVs.ToList();
            foreach (var item in tenNhom)
            {
                switch (item.TenNhomCT)
                {
                    case "Thuốc trong danh mục BHYT":
                        idThuoc = item.IDNhom;
                        if (tenNhom.Where(a => a.TenNhomCT == "Thuốc trong danh mục BHYT").Count() > 1)
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
                    case "Vật tư y tế trong danh mục BHYT":
                        if (tenNhom.Where(a => a.TenNhomCT == "Vật tư y tế trong danh mục BHYT").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm vật tư y tế tiêu hao");
                        idVTYT = item.IDNhom;
                        break;
                    case "Giường điều trị ngoại trú":
                        if (tenNhom.Where(a => a.TenNhomCT == "Giường điều trị ngoại trú").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Giường điều trị ngoại trú");
                        if (item.IDNhom != 14)
                            MessageBox.Show("Nhóm Giường điều trị ngoại trú sai mã nhóm theo CV 9324");
                        idNgayGiuongNgoaiTru = item.IDNhom;
                        break;
                    case "Giường điều trị nội trú":
                        if (tenNhom.Where(a => a.TenNhomCT == "Giường điều trị nội trú").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Giường điều trị nội trú");
                        if (item.IDNhom != 15)
                            MessageBox.Show("Nhóm Giường điều trị nội trú sai mã nhóm theo CV 9324");
                        idNgayGiuongNoiTru = item.IDNhom;
                        break;
                    case "Vận chuyển":
                        if (tenNhom.Where(a => a.TenNhomCT == "Vận chuyển").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Chi phí vận chuyển");
                        idChiPhiVC = item.IDNhom;
                        break;
                    case "VTYT thanh toán theo tỷ lệ":
                        if (tenNhom.Where(a => a.TenNhomCT == "VTYT thanh toán theo tỷ lệ").Count() > 1)
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //TimKiem();
            Search();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            this.Hide();
            int _mabn = 0;
            string _tenbn = "";
            if (grv_Export_XML_2348.GetFocusedRowCellValue(colMaBNhan) != null)
            {
                _mabn = Convert.ToInt32(grv_Export_XML_2348.GetFocusedRowCellValue(colMaBNhan));
                _tenbn = grv_Export_XML_2348.GetFocusedRowCellValue(colTenBNhan).ToString();
            }

            BHYT.frm_DuyetBN frm = new frm_DuyetBN(_mabn, _tenbn, false);
            frm.ShowDialog();
            this.Show();
        }

        private void grvThamDinh_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXemCP")
            {
                btnXem_Click(sender, e);
            }
        }

        private void grvThamDinh_DoubleClick(object sender, EventArgs e)
        {
            if (grv_Export_XML_2348.GetFocusedRowCellValue(colMaBNhan) != null)
            {
                btnXem_Click(sender, e);
            }
        }

        private void grvThamDinh_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void btn_TimKiem_CheckedChanged(object sender, EventArgs e)
        {
            if (DungChung.Ham.checkQuyen(this.Name)[3])
            {
                pan_TimKiem.Enabled = !btn_TimKiem.Checked;

                if (rad_ExPort.SelectedIndex == 1)
                    btn_ExPort.Text = "Xóa dữ liệu";
                else
                    btn_ExPort.Text = "Gửi dữ liệu";

                if (btn_TimKiem.Checked)
                    //TimKiem();
                    Search();
            }
        }

        private void Loc()
        {
            grc_Export_XML_2348.DataSource = null;
            int chon = rad_loc.SelectedIndex;
            int rs = 0, mabn = 0;
            if (int.TryParse(txtTimKiem.Text, out rs))
                mabn = Convert.ToInt32(txtTimKiem.Text);

            grc_Export_XML_2348.DataSource = _listVPBH.Where(p => (chon == 0 ? p.Status == false : (chon == 1 ? p.Status == true : true))).Where(p => (chk_BNSai.Checked ? (p.Lydo_xt != null && p.Lydo_xt.Length > 0) : true)).Where(p => (p.Ma_the.ToLower().Contains(txtTimKiem.Text.ToLower()) || p.Ho_ten.ToLower().Contains(txtTimKiem.Text.ToLower()) || (p.Ma_bn == mabn))).ToList();
        }

        private void rad_loc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loc();
        }

        private void hyp_Chon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            foreach (var a in _listVPBH)
            {
                a.Status = true;
            }

            grc_Export_XML_2348.DataSource = null;
            grc_Export_XML_2348.DataSource = _listVPBH;
        }

        private void hyp_HuyChon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            foreach (var a in _listVPBH)
            {
                a.Status = false;
            }

            grc_Export_XML_2348.DataSource = null;
            grc_Export_XML_2348.DataSource = _listVPBH;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="_mabn"></param>
        /// <param name="_delete"></param>
        /// <param name="noigui">=0: gửi BYT; =1: gửi BHXH; =2: gửi khác</param>
        /// <returns></returns>                         
        public bool _updateExPort(QLBVEntities _data, List<DungChung.Cls79_80.cl_79_80> _listVPBH, int _mabn, bool _delete, int noigui)
        {
            try
            {
                bool b = false;
                var vp = _data.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
                bool chuyenvien = false;
                bool service = false;
                int noingoai = CheckNoiNgoaiTru(_mabn);
                bool ngoaigio = false;

                if (vp.Count > 0 && vp.First().NgoaiGio == 1 && (DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "30009"))
                    ngoaigio = true;

                if (DungChung.Bien.MaBV == "26007")
                {
                    var qchuyenvien = _data.RaViens.Where(p => p.MaBNhan == _mabn && p.Status == 1).FirstOrDefault();
                    if (qchuyenvien != null)
                        chuyenvien = true;
                }

                if ((noingoai == 0) && (DungChung.Bien.PPXuat_BHXH[0] == 1 || DungChung.Bien.PPXuat_BHXH[0] == 3 || DungChung.Bien.PPXuat_BHXH[0] == 5 || DungChung.Bien.PPXuat_BHXH[0] == 6) || ngoaigio || chuyenvien)
                    service = true;
                if ((noingoai == 1) && (DungChung.Bien.PPXuat_BHXH[1] == 1 || DungChung.Bien.PPXuat_BHXH[1] == 3 || DungChung.Bien.PPXuat_BHXH[1] == 5 || DungChung.Bien.PPXuat_BHXH[0] == 6) || ngoaigio || chuyenvien)
                    service = true;

                foreach (var item in vp)
                {
                    #region xóa gửi
                    if (_delete)
                    {
                        int mabn = item.MaBNhan == null ? 0 : item.MaBNhan.Value;

                        try
                        {
                            if (noigui == 1) // gửi BHXH
                            {
                                DungChung.cls_KetNoi_BHXH clsBHXH = new DungChung.cls_KetNoi_BHXH();
                                DungChung.cls_KetNoi_BHXH.updateMaGiaoDich(mabn, "");

                                b = false;
                            }
                            else if (noigui == 2)
                            {
                                // b = !DungChung.BenhNhan_AllInfo.CreateCheckOutFile(mabn, _data, DungChung.Bien.xmlFilePath_LIS[12], DungChung.Bien.xmlFilePath_LIS[13], true, true, false); 
                            }
                            else if (noigui == 0) // gửi BYT
                            {
                                DungChung.cls_KetNoi_BYT cls = new DungChung.cls_KetNoi_BYT();
                                b = !cls.createCheckOutFile(_data, mabn, DungChung.Bien.xmlFilePath_LIS[3], 2, true, user, pass, 2);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi gửi DL: " + ex.Message);
                        }

                        if (noigui == 1)
                            item.ExportBHXH = b;

                        else if (noigui == 0)
                            item.ExportBYT = b;
                        else
                            item.Export = b;

                        foreach (var a in _listVPBH)
                        {
                            if (a.Ma_bn == mabn)
                            {
                                a.Export = b;

                                break;
                            }
                        }
                        _data.SaveChanges();
                    }
                    #endregion
                    else
                    {
                        int mabn = item.MaBNhan == null ? 0 : item.MaBNhan.Value;

                        try
                        {
                            if (noigui == 1)
                            {
                                DungChung.cls_KetNoi_BHXH clsBHXH = new DungChung.cls_KetNoi_BHXH();
                                b = clsBHXH.XuatXML_HSBN(_data, mabn, DungChung.Bien.xmlFilePath_LIS[7], DungChung.Bien.xmlFilePath_LIS[9], service);
                            }
                            else if (noigui == 2)
                            {
                                DungChung.cls_KetNoi_BHXH clsBHXH = new DungChung.cls_KetNoi_BHXH();
                                b = clsBHXH.XuatXML_HSBN(_data, mabn, DungChung.Bien.xmlFilePath_LIS[3], DungChung.Bien.xmlFilePath_LIS[4], false);
                            }
                            else if (noigui == 0)
                            {
                                DungChung.cls_KetNoi_BYT cls = new DungChung.cls_KetNoi_BYT();
                                b = cls.createCheckOutFile(_data, mabn, DungChung.Bien.xmlFilePath_LIS[12], 0, true, user, pass, 2);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi gửi DL: " + ex.Message);
                        }

                        if (noigui == 1)
                            item.ExportBHXH = b;
                        else if (noigui == 0)
                            item.ExportBYT = b;
                        else
                            item.Export = b;

                        foreach (var a in _listVPBH)
                        {
                            if (a.Ma_bn == mabn)
                            {
                                a.Export = b;
                                break;
                            }
                        }
                        _data.SaveChanges();
                    }
                }
                if (_delete)
                    return !b;
                else
                    return b;
            }
            catch (Exception)
            {
                MessageBox.Show("Error update table VienPhi");
                return false;
            }
        }
        public bool _XuatXML(QLBVEntities _data, List<DungChung.Cls79_80.cl_79_80> _listVPBH, int _mabn, bool _delete, int noigui)
        {
            try
            {
                bool b = false;
                var vp = _data.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
                foreach (var item in vp)
                {
                    int mabn = item.MaBNhan == null ? 0 : item.MaBNhan.Value;
                    if (noigui == 1)
                    {
                        DungChung.cls_KetNoi_BHXH clsBHXH = new DungChung.cls_KetNoi_BHXH();
                        b = clsBHXH.XuatXML_HSBN(_data, mabn, DungChung.Bien.xmlFilePath_LIS[7], DungChung.Bien.xmlFilePath_LIS[9], false);
                        MessageBox.Show("Xuất XML thành công");
                    }
                    else if (noigui == 2)
                    {
                        DungChung.cls_KetNoi_BHXH clsBHXH = new DungChung.cls_KetNoi_BHXH();
                        b = clsBHXH.XuatXML_HSBN(_data, mabn, DungChung.Bien.xmlFilePath_LIS[3], DungChung.Bien.xmlFilePath_LIS[4], false);
                    }
                }
                return b;
            }
            catch (Exception)
            {
                MessageBox.Show("Error update table VienPhi");
                return false;
            }
        }

        public void _getvalue_US(string u, string p)
        {
            us_Export_XML_2348.user = u;
            us_Export_XML_2348.pass = p;
        }

        public static string[] _config = new string[20];
        public static string error = "";
        private void btn_ExPort_Click(object sender, EventArgs e)
        {
            CursorState.SetBusyState();

            DungChung.BenhNhan_AllInfo._listBenhNhanSai.Clear();
            BHYT.us_Export_XML_2348.error = "";

            if (DungChung.Ham.checkQuyen(this.Name)[0])
            {
                _ktmatkhau = false;
                ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass();
                frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
                frm.ShowDialog();
                string config = "";
                string[] configR = new string[20];
                config = QLBV_Library.QLBV_Ham.Read_Update("Cuong.F9324", 1);
                if (!string.IsNullOrEmpty(config))
                    configR = config.Split('*');

                for (int i = 0; i < 20; i++)
                {
                    if (i < configR.Length && configR[i] != null)
                    {
                        _config[i] = configR[i];
                    }
                    else
                    {
                        _config[i] = "";
                    }
                }

                string _tb = "Gửi dữ liệu thành công";
                if (_ktmatkhau)
                {
                    bool _delete = false;
                    if (btn_ExPort.Text == "Xóa dữ liệu")
                    {
                        _delete = true;
                        _tb = "Xóa dữ liệu thành công";
                    }
                    if (_listVPBH.Where(p => p.Status == true).ToList().Count > 0)
                    {
                        //
                        progressBarControl1.Visible = true;
                        progressBarControl1.Properties.Step = 1;
                        progressBarControl1.Properties.PercentView = true;
                        progressBarControl1.Properties.Minimum = 0;
                        progressBarControl1.Properties.Maximum = _listVPBH.Where(p => p.Status).ToList().Count;
                        progressBarControl1.EditValue = 0;

                        //
                        if (rad_FileGui.SelectedIndex == 0)
                        {
                            string td = "Nhập tài khoản do BYT cấp!";
                            ChucNang.CheckUser frm_c = new ChucNang.CheckUser(td);
                            frm_c.ok = new ChucNang.CheckUser._getdata(_getvalue_US);
                            frm_c.ShowDialog();

                        }
                        int soluongfilegui = 0;
                        foreach (var item in _listVPBH)
                        {
                            if (item.Status)
                            {
                                if (_updateExPort(_dataContext, _listVPBH, item.Ma_bn, _delete, rad_FileGui.SelectedIndex))
                                    soluongfilegui++;

                                progressBarControl1.PerformStep();
                                progressBarControl1.Update();
                            }
                        }
                        progressBarControl1.Visible = false;
                        MessageBox.Show(_tb + " " + soluongfilegui.ToString() + " bệnh nhân");
                        if (!string.IsNullOrEmpty(BHYT.us_Export_XML_2348.error.Trim()))
                            MessageBox.Show(BHYT.us_Export_XML_2348.error);
                        // xuất excell bệnh nhân sai
                        string ms = "";
                        foreach (var ar in DungChung.BenhNhan_AllInfo._listBenhNhanSai)
                        {
                            ms += string.Join(",", ar.MaBN, ar.TenBN, ar.ErrMessage) + "\n";
                        }

                        if (!string.IsNullOrEmpty(ms))
                            MessageBox.Show(ms);
                        //
                        btnIn_Click(sender, e);
                        btn_TimKiem_CheckedChanged(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa chọn bệnh nhân để gửi|xóa dữ liệu");
                    }
                }
            }
        }

        public void _getValue(bool a)
        {
            _ktmatkhau = a;
        }

        public bool _ktmatkhau = false;
        private void in_Ds(bool dssai)
        {
            CursorState.SetBusyState();

            if (_listVPBH.Where(p => p.Status == true && (dssai ? (p.Lydo_xt != null && p.Lydo_xt.Length > 1) : true)).ToList().Count > 0)
            {
                Phieu.rep_List_BN_export rep = new Phieu.rep_List_BN_export();
                if (btn_ExPort.Text == "Xóa dữ liệu")
                    rep.txtTieuDe.Text = "Danh Sách bệnh nhân xóa dữ liệu đã gửi".ToUpper();
                if (dssai)
                    rep.txtTieuDe.Text = "Danh Sách bệnh nhân sai dữ liệu".ToUpper();
                rep.DataSource = _listVPBH.Where(p => p.Status == true && (dssai ? (p.Lydo_xt != null && p.Lydo_xt.Length > 1) : true)).ToList();
                rep.BinDingData();
                rep.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            in_Ds(false);
        }

        private void btn_check_Click(object sender, EventArgs e)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "\\Check2348.exe";
            System.Diagnostics.Process.Start(path);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            CursorState.SetBusyState();

            var isExport = true;

            foreach (var item in _listVPBH)
            {
                if (item.Status)
                {
                    isExport = ExportFileCircular130.ExportXml(item.Ma_bn);
                }
            }

            if (isExport)
            {
                MessageBox.Show("Xuất dữ liệu XML thành công!");
            }
            else
            {
                MessageBox.Show("Xuất dữ liệu XML không thành công!");
            }
        }

        private void btnXuatXML_Click(object sender, EventArgs e)
        {
            
            if (_listVPBH.Where(p => p.Status == true).ToList().Count > 0)
            {
                foreach (var item in _listVPBH)
                {
                    if (item.Status)
                    {
                        _XuatXML(_dataContext, _listVPBH, item.Ma_bn, false, rad_FileGui.SelectedIndex);
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn bệnh nhân để gửi|xóa dữ liệu");
            }
        }

        private void btnSenHSYTCS_Click(object sender, EventArgs e)
        {
            new frm_GuiHSSKV20().ShowDialog();
        }

        private void btnDataCommunication_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormThamSo.Frm_Download frm = new FormThamSo.Frm_Download();
            frm.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormThamSo.Frm_Upload frm = new FormThamSo.Frm_Upload();
            frm.ShowDialog();
        }

        private void btn_checkDL_Click(object sender, EventArgs e)
        {
            CursorState.SetBusyState();

            if (rad_ExPort.SelectedIndex == 0)
            {
                MessageBox.Show("Áp dụng cho Bệnh nhân đã gửi dữ liệu có mã giao dịch chứa 'KCB'!");
                return;
            }
            DungChung.BenhNhan_AllInfo._listBenhNhanSai.Clear();
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //
            progressBarControl1.Visible = true;
            progressBarControl1.Properties.Step = 1;
            progressBarControl1.Properties.PercentView = true;
            progressBarControl1.Properties.Minimum = 0;
            progressBarControl1.Properties.Maximum = _listVPBH.Where(p => p.Status).ToList().Count;
            progressBarControl1.EditValue = 0;
            int noigui = 0;
            noigui = rad_FileGui.SelectedIndex;
            int dem = 0;
            foreach (var item in _listVPBH)
            {
                if (item.Status)
                {
                    bool b = false;
                    if (noigui == 1)
                    {
                        if ((rad_ExPort.SelectedIndex == 1) && item.Lydo_xt.Contains("KCB"))
                        {
                            dem++;
                            // check danh sach co ma giao dich, da gui
                            GiamDinhBHXH.BHXH_Model.Gui_NhanDuLieu_BHXH guinhanBHXH = new GiamDinhBHXH.BHXH_Model.Gui_NhanDuLieu_BHXH();
                            List<GiamDinhBHXH.BHXH_Model.ChiTietDSLoi> _ldsLoi = new List<GiamDinhBHXH.BHXH_Model.ChiTietDSLoi>();
                            string msg = "";

                            GiamDinhBHXH.BHXH_Model.Service.NhanChiTietLoiHS(DungChung.Bien.xmlFilePath_LIS[10], DungChung.Bien.xmlFilePath_LIS[11], DungChung.Bien.MaBV, item.Lydo_xt, ref _ldsLoi, ref msg);
                            foreach (var item2 in _ldsLoi)
                            {
                                msg += item2.maLoi + ": " + item2.moTaLoi + ";";
                            }
                            item.Err = msg;
                        }
                        else
                        {
                            if (rad_ExPort.SelectedIndex == 0)
                                b = !DungChung.BenhNhan_AllInfo.CreateCheckOutFile(item.Ma_bn, _data, DungChung.Bien.xmlFilePath_LIS[12], DungChung.Bien.xmlFilePath_LIS[13], false, false, false);
                        }
                    }
                    else if (noigui == 2)
                    { b = !DungChung.BenhNhan_AllInfo.CreateCheckOutFile(item.Ma_bn, _data, DungChung.Bien.xmlFilePath_LIS[12], DungChung.Bien.xmlFilePath_LIS[13], false, false, false); }
                    else if (noigui == 0)
                    {
                        DungChung.cls_KetNoi_BYT cls = new DungChung.cls_KetNoi_BYT();
                        int trangthai = 0; //0: mới, 1: sửa, 2 xóa
                        string user = "", pass = "";
                        b = !cls.createCheckOutFile(_data, item.Ma_bn, DungChung.Bien.xmlFilePath_LIS[3], trangthai, true, user, pass, 2);
                        b = !cls.createCheckInFile(_data, item.Ma_bn, DungChung.Bien.xmlFilePath_LIS[3], trangthai, true, user, pass, 2);

                    }
                    progressBarControl1.PerformStep();
                    progressBarControl1.Update();
                }
            }
            grc_Export_XML_2348.DataSource = null;
            grc_Export_XML_2348.DataSource = _listVPBH;
            progressBarControl1.Visible = false;
            MessageBox.Show("Hoàn thành kiểm tra dữ liệu:" + dem + "/" + _listVPBH.Count);
        }

        private void grv_Export_XML_2348_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (grv_Export_XML_2348.GetRowCellValue(e.RowHandle, Lydo_xt) != null && grv_Export_XML_2348.GetRowCellValue(e.RowHandle, Lydo_xt).ToString() != "")
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void chk_BNSai_CheckedChanged(object sender, EventArgs e)
        {
            Loc();
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            Loc();
        }

        SaveFileDialog dialog = new SaveFileDialog();
        private void btnChonFilePath_Click(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            dialog.InitialDirectory = "C:\\";
            dialog.Filter = "Excel files (*.xls or *.xlsx)|*.xls;*.xlsx";
            dialog.FilterIndex = 1;
            dialog.FileName = "Bieu" + "_79_80" + "_" + DungChung.Bien.MaBV + "_" + _ngay.Year + "_" + _ngay.Month + ".xls";
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dialog.FileName;
            }
        }

        private void btnXuatExcell_Click(object sender, EventArgs e)
        {
            if (_listVPBH.Count() <= 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất excel");
            }
            else if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageBox.Show("Chưa chọn đường dẫn lưu file excel");
            }
            else
            {
                if (QLBV.DungChung.Cls79_80.xuatExcelRange(_listVPBH, txtFilePath.Text, false, "TCVN3", false))
                    MessageBox.Show("Xuất thành công!");
                else
                    MessageBox.Show("Lỗi xuất Excel!");
            }
        }

        private void btnSendHSSK_Click(object sender, EventArgs e)
        {
            frm_GuiHSSK frm = new frm_GuiHSSK();
            frm.ShowDialog();
        }

        private void btnGuiChungTu_Click(object sender, EventArgs e)
        {
            frm_GuiChungTu_BHYT frm = new frm_GuiChungTu_BHYT();
            frm.ShowDialog();
        }
    }
}
