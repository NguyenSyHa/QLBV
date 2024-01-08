using System;using QLBV_Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace QLBV.DungChung
{


    public class BenhNhan_AllInfo
    {
        #region class
        public class DsChuyenVien
        {
            private string maBV;

            public string MaBV
            {
                get { return maBV; }
                set { maBV = value; }
            }
            private string tuyen;//A,B,C

            public string Tuyen
            {
                get { return tuyen; }
                set { tuyen = value; }
            }
            private string tungay;

            public string Tungay
            {
                get { return tungay; }
                set { tungay = value; }
            }
            private string denngay;

            public string Denngay
            {
                get { return denngay; }
                set { denngay = value; }
            }
        }
        public class CT_CLS
        {
            private string ma_lk;

            public string Ma_lk
            {
                get { return ma_lk; }
                set { ma_lk = value; }
            }
            private int stt;

            public int Stt
            {
                get { return stt; }
                set { stt = value; }
            }
            private string ma_dich_vu;

            public string Ma_dich_vu
            {
                get { return ma_dich_vu; }
                set { ma_dich_vu = value; }
            }
            private string ma_chi_so;

            public string Ma_chi_so
            {
                get { return ma_chi_so; }
                set { ma_chi_so = value; }
            }
            private string ten_chi_so;

            public string Ten_chi_so
            {
                get { return ten_chi_so; }
                set { ten_chi_so = value; }
            }
            private string gia_tri;

            public string Gia_tri
            {
                get { return gia_tri; }
                set { gia_tri = value; }
            }
            private string ma_may;

            public string Ma_may
            {
                get { return ma_may; }
                set { ma_may = value; }
            }
            private string mo_ta;

            public string Mo_ta
            {
                get { return mo_ta; }
                set { mo_ta = value; }
            }
            private string ket_luan;

            public string Ket_luan
            {
                get { return ket_luan; }
                set { ket_luan = value; }
            }
            private string ngay_kq;

            public string Ngay_kq
            {
                get { return ngay_kq; }
                set { ngay_kq = value; }
            }

        }
        public class DienBienBenh
        {
            private string ma_lk;

            public string Ma_lk
            {
                get { return ma_lk; }
                set { ma_lk = value; }
            }
            private int stt;

            public int Stt
            {
                get { return stt; }
                set { stt = value; }
            }
            private string dien_bien;

            public string Dien_bien
            {
                get { return dien_bien; }
                set { dien_bien = value; }
            }
            private string hoi_chan;

            public string Hoi_chan
            {
                get { return hoi_chan; }
                set { hoi_chan = value; }
            }
            private string phau_thuat;

            public string Phau_thuat
            {
                get { return phau_thuat; }
                set { phau_thuat = value; }
            }
            private string ngay_yl;

            public string Ngay_yl
            {
                get { return ngay_yl; }
                set { ngay_yl = value; }
            }
        }
        public class BenhNhanSai
        {
            private int maBN;

            public int MaBN
            {
                get { return maBN; }
                set { maBN = value; }
            }
            private string tenBN;

            public string TenBN
            {
                get { return tenBN; }
                set { tenBN = value; }
            }
            private string errMessage;

            /// <summary>
            /// Thông tin lỗi của mỗi bệnh nhân
            /// </summary>
            public string ErrMessage
            {
                get { return errMessage; }
                set { errMessage = value; }
            }

        }
        #endregion
        public static List<BenhNhanSai> _listBenhNhanSai = new List<BenhNhanSai>();
        /// <summary>
        /// </summary>
        /// <param name="maBNhan"></param>
        /// <param name="data"></param>
        /// <param name="path">VD: "D:\\"</param>
        /// /// <param name="bolXML">Xuất XML hoặc chỉ kiểm tra có đủ điều kiện xuất hay không</param>
        /// <returns></returns>
        public static bool CreateCheckOutFile(int maBNhan, QLBV_Database.QLBVEntities data, string path,string path_bak, bool delete, bool bolXML,bool BHXH)
        {
            try { 
            bool rs = false;
            int _idvc = -1;
            if (maBNhan <= 0)
                return false;
            else
            {
                var TTboXungs = (from tt in data.TTboXungs.Where(p => p.MaBNhan == maBNhan)
                                 select tt).ToList();

                var qkt = data.BenhNhans.Where(p => p.MaBNhan == maBNhan && p.SThe.Length == 15).ToList();
                if (qkt.Count > 0)
                {
                    var qbn = qkt.First();
                    var qrv = data.RaViens.Single(p=>p.MaBNhan == maBNhan);
                    string strNgaySinh = GetNgaySinh(qbn.NgaySinh, qbn.ThangSinh, qbn.NamSinh);
                    #region qtonghop
                    var qtonghop = (from bn in data.BenhNhans.Where(p => p.MaBNhan == maBNhan && p.SThe.Length == 15)
                                    join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                    join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                                    join vpct in data.VienPhicts.Where(p=>p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi 
                                    join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                                    join kp in data.KPhongs on vpct.MaKP equals kp.MaKP
                                    select new
                                    {
                                        bn.MaKCB,
                                        rv,
                                        dv.PLoai,
                                        dv.MaQD,
                                        dv.IDNhom,
                                        dv.Status,// 1: dược, 2 dịch vụ
                                        Ma_bn = maBNhan,
                                        Ho_ten = bn.TenBNhan,
                                        Ngay_sinh = strNgaySinh,
                                        Gioi_tinh = bn.GTinh,
                                        Dia_chi = bn.DChi,
                                        Ma_the = bn.SThe,
                                        Ma_dkbd = bn.MaCS,
                                        Gt_the_tu = bn.HanBHTu,
                                        Gt_the_den = bn.HanBHDen,
                                        Ma_benh = rv.MaICD,
                                        Ten_benh = rv.ChanDoan,
                                        //  Ma_lydo_vvien = (bn.Tuyen == null || bn.CapCuu == null) ? "" : (bn.CapCuu != 0 ? "2" : (bn.Tuyen == 1 ? "1" : "3")),
                                        Ma_lydo_vvien = bn.CapCuu == 1 ? "2" : (bn.Tuyen == 1 ? "1" : (bn.Tuyen == 2 ? "3" : "")),
                                        Ma_noi_chuyen = bn.MaBV,
                                        Ma_tai_nan = "",
                                        Ngay_vao = rv.NgayVao,
                                        Ngay_ra = rv.NgayRa,
                                        So_ngay_dtri = rv.SoNgaydt,
                                        Ket_qua_dtri = rv.KetQua,// == null ? "" : (rv.KetQua == "Khỏi" ? "1" : (rv.KetQua == "Đỡ|Giảm" ? "2" : (rv.KetQua == "Không T.đổi" ? "3" : (rv.KetQua == "Nặng hơn" ? "4": "5")))),
                                        // rvStatus = rv.Status,// == null ? "" : (rv.Status == 1 ? "2" :(rv.Status == 1 ? "2" :rv.Status.ToString())),
                                        Ngay_ttoan = vp.NgayTT,
                                        ThanhTien = vpct.ThanhTien == null ? 0 : Math.Round(vpct.ThanhTien, 0),
                                        t_bhtt = vpct.TienBH == null ? 0 : Math.Round(vpct.TienBH, 0),
                                        t_bntt = vpct.TienBN == null ? 0 : Math.Round(vpct.TienBN, 0),
                                        t_nguonkhac = 0,
                                        ma_khuvuc = bn.KhuVuc,
                                        ma_pttt_qt = "",
                                        Sophieu = maBNhan,
                                        Ngay_quyettoan = "",
                                        Tinh_trang_rv = (rv.Status == null || rv.Status == 2) ? 1 : (rv.Status == 1 ? 2 : (rv.Status)),
                                        Ma_loaikcb = (bn.NoiTru == null) ? "" : (bn.NoiTru == 1 ? "3" : "2"),
                                        /////--------------------------
                                        NgayTT = vp.NgayTT,
                                        CapCuu = bn.CapCuu,
                                        NoiTru = bn.NoiTru,
                                        Tuyen = bn.Tuyen,
                                        //NamSinh = bn.NamSinh,
                                        //ThangSinh = bn.ThangSinh,
                                        //NgaySinh = bn.NgaySinh,
                                        ////--------------------------Biểu 20
                                        dv.MaDV,
                                        dv.TenDV,
                                        DonVi = dv.DonVi,
                                        dv.HamLuong,
                                        dv.DuongD,
                                        dv.SoDK,
                                        vpct.SoLuong,
                                        vpct.DonGia,
                                        TyleTT = dv.BHTT == null ? 0 : dv.BHTT.Value,//-------------------tỷ lệ thanh toán ?????????
                                       MaKP=kp.MaQD,
                                        MaBSi = "",
                                        NgayYL = "",
                                        NgayKQ = "",

                                    }).ToList();

                    #endregion qtonghop
                    #region check BNsai

                    string mss = "";
                    if (strNgaySinh == "")
                    {
                        mss = "Ngày sinh không được để trống; ";
                    }
                    if(qbn.MaCS == "")
                       mss += "Mã đăng ký khám chữa bệnh ban đầu không thể để trống; ";
                    if (qbn.NoiTru == null)
                        mss += "Mã loại khám chữa bệnh không được để trống - kiểm tra trường nội trú; ";
                    if (qbn.GTinh == null)
                        mss += "giới tính không được để trống; ";
                    if (qbn.HanBHTu == null || qbn.HanBHDen == null)
                        mss += "hạn bảo hiểm không được để trống; ";
                    if (qrv.NgayVao == null)
                    {
                        mss += "Ngày vào viện phải khác null; ";
                    }
                    if (qrv.NgayRa == null)
                    {
                        mss += "Ngày ra không hợp lệ; ";
                    }
                    if (qrv.NgayVao != null && qrv.NgayRa != null)
                    {
                        if ((qrv.NgayRa.Value - qrv.NgayVao.Value).TotalMilliseconds < 0)
                            mss += "Ngày ra phải lớn hơn ngày vào";
                    }
                    if (qrv.Status == 1 && (qrv.MaBVC == null || qrv.MaBVC.Trim() == ""))
                    {
                        mss += "Mã bệnh viện chuyển không được để trống; ";
                    }
                    if (qrv.MaICD == null ||qrv.MaICD.Trim() == "")
                    {
                        mss += "Mã ICD không hợp lệ; ";
                    }
                    if (qrv.ChanDoan == null || qrv.ChanDoan.Trim() == "")
                    {
                        mss += "Chẩn đoán không được để trống; ";
                    }                    
                    

                    foreach (var a in qtonghop)
                    {
                       
                        if ((a.IDNhom == 4 ||a.IDNhom == 5 || a.IDNhom == 6))
                        {                            
                            if (a.DonVi == null || a.DonVi.Trim() == "")
                                mss += "Đơn vị tính  không được để trống; ";
                        }
                        break;
                    }
                    if (mss.Length > 0)
                    {
                        _listBenhNhanSai.Add(new BenhNhanSai { MaBN = qbn.MaBNhan, TenBN = qbn.TenBNhan, ErrMessage = mss });
                        return false;
                    }
                    #endregion check BNsai
                    else if (!bolXML)//chỉ kiểm tra bn, không xuất ra file
                    {
                        return true;
                    }
                    else
                    {
                        #region qChuyenTuyen
                        //var qChuyenTuyen = (from ct in qtonghop
                        //                    where ct.Status == 1
                        //                    select new
                        //                    {
                        //                        Sohoso = "",
                        //                        Sochuyentuyen = ct.SoChuyenVien,
                        //                        Ma_bv_chuyenden = ct.MaBVC,
                        //                        Ma_bv_khambenh = DungChung.Bien.MaBV,
                        //                        Ten_cs_khambenh = DungChung.Bien.TenCQ,
                        //                        Nghenghiep = ct.MaNN,
                        //                        Noilamviec = ct.NoiLV,
                        //                        Lamsang = ct.ChanDoan,
                        //                        Ketquaxetnghiem = ct.KetQua,
                        //                        Chandoan = ct.ChanDoan,
                        //                        Phuongphapdieutri = ct.PPDTr,
                        //                        Tinhtrangnguoibenh = ct.TinhTrangC,
                        //                        Lydo_chuyentuyen = ct.LyDoC == null ? "" : (ct.LyDoC == "Đủ điều kiện chuyển tuyến(đúng tuyến)" ? "1" : "2"),
                        //                        Huongdieutri = ct.LoiDan,
                        //                        Thoigian_chuyen = ct.NgayRa,
                        //                        Phuongtien = "",
                        //                        Nguoi_hotong = "",
                        //                        Ma_quoctich = "",
                        //                        Ma_dantoc = ct.MaDT,

                        //                    }).ToList();


                        #endregion qChuyenTuyen
                        List<CT_CLS> _lCLS = new List<CT_CLS>();
                        List<DienBienBenh> _lDienBien = new List<DienBienBenh>();
                        List<DsChuyenVien> _lDSchuyenVien = new List<DsChuyenVien>();
                        List<MucTT> _listmuc = new List<MucTT>();
                        _listmuc = data.MucTTs.ToList();
                       
                        #region lấy ra hạng bệnh viện
                        int _hangbv = DungChung.Ham.hangBV(DungChung.Bien.MaBV);
                        #endregion
                        string ngayvao = "", sthe = "";
                        if (qtonghop.Count > 0)
                        {
                            if (qtonghop.First().Ngay_vao != null)
                                ngayvao = Convert.ToDateTime(qtonghop.First().Ngay_vao).ToString("yyyyMMddhhmm") + "_";
                            if (qtonghop.First().Ma_the != null)
                                sthe = qtonghop.First().Ma_the + "_";
                            #region _lTongHop
                            var _lTongHop = (from qth in qtonghop
                                             
                                             group qth by new
                                                 {
                                                     qth.MaKCB,
                                                     qth.Ma_bn,
                                                     qth.Ho_ten,
                                                     qth.Ngay_sinh,
                                                     qth.Gioi_tinh,
                                                     qth.Dia_chi,
                                                     qth.Ma_the,
                                                     qth.Ma_dkbd,
                                                     qth.Gt_the_tu,
                                                     qth.Gt_the_den,
                                                     qth.Ma_benh,
                                                     qth.Ten_benh,
                                                     qth.Ma_lydo_vvien,
                                                     qth.Ma_noi_chuyen,
                                                     qth.Ma_tai_nan,
                                                     qth.Ngay_vao,
                                                     qth.Ngay_ra,
                                                     qth.So_ngay_dtri,
                                                     qth.Ket_qua_dtri,
                                                     qth.Tinh_trang_rv,
                                                     qth.Ngay_ttoan,
                                                     qth.ma_khuvuc,
                                                     qth.ma_pttt_qt,
                                                     qth.Sophieu,
                                                     qth.Ngay_quyettoan,
                                                     qth.Ma_loaikcb,
                                                     //-------------------------
                                                     qth.NgayTT,
                                                     qth.CapCuu,
                                                     qth.NoiTru,
                                                     qth.Tuyen,
                                                     //qth.NamSinh,
                                                     //qth.ThangSinh,
                                                     //qth.NgaySinh,
                                                     qth.rv.Status,
                                                 } into kq
                                             select new
                                             {
                                                 kq.Key.MaKCB,
                                                 Ma_bn = kq.Key.Ma_bn,
                                                 Ho_ten = kq.Key.Ho_ten,
                                                 Ngay_sinh = kq.Key.Ngay_sinh,
                                                 Gioi_tinh = kq.Key.Gioi_tinh,
                                                 Dia_chi = kq.Key.Dia_chi,
                                                 Ma_the = kq.Key.Ma_the,
                                                 Ma_dkbd = kq.Key.Ma_dkbd,
                                                 Gt_the_tu = kq.Key.Gt_the_tu,
                                                 Gt_the_den = kq.Key.Gt_the_den,
                                                 Ma_benh = kq.Key.Ma_benh,
                                                 Ten_benh = kq.Key.Ten_benh,
                                                 Ma_lydo_vvien = kq.Key.Ma_lydo_vvien,
                                                 Ma_noi_chuyen = kq.Key.Ma_noi_chuyen,
                                                 Ma_tai_nan = kq.Key.Ma_tai_nan,
                                                 Ngay_vao = kq.Key.Ngay_vao,
                                                 Ngay_ra = kq.Key.Ngay_ra,
                                                 So_ngay_dtri = (kq.Key.So_ngay_dtri == null) ? 1 : kq.Key.So_ngay_dtri,
                                                 Ket_qua_dtri = kq.Key.Ket_qua_dtri,
                                                 Tinh_trang_rv = kq.Key.Tinh_trang_rv,
                                                 Ngay_ttoan = kq.Key.Ngay_ttoan,
                                                 T_tongchi = kq.Sum(p => p.ThanhTien),
                                                 T_bntt = kq.Sum(p => p.t_bntt),
                                                 T_bhtt = kq.Sum(p => p.t_bhtt),
                                                 T_nguonkhac = 0,
                                                 T_ngoaids = kq.Where(p => p.IDNhom == _idvc).Sum(p => p.ThanhTien),
                                                 T_Thuoc = kq.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).Sum(p => p.ThanhTien),
                                                 t_vtyt = kq.Where(p => p.IDNhom == 7 || p.IDNhom == 10 || p.IDNhom == 11).Sum(p => p.ThanhTien),
                                                 Ma_loaikcb = kq.Key.Ma_loaikcb,
                                                 Ma_khuvuc = kq.Key.ma_khuvuc,
                                                 Ma_pttt_qt = kq.Key.ma_pttt_qt,
                                                 Sophieu = kq.Key.Sophieu,
                                                 Ngay_quyettoan = kq.Key.Ngay_quyettoan,
                                                 //-------------------
                                                 kq.Key.NgayTT,
                                                 kq.Key.CapCuu,
                                                 kq.Key.NoiTru,
                                                 kq.Key.Tuyen,
                                                 //kq.Key.NamSinh,
                                                 //kq.Key.ThangSinh,
                                                 //kq.Key.NgaySinh,
                                                 kq.Key.Status

                                             }
                                                             ).ToList();
                            #endregion _lTongHop
                            #region  Tạo file XML
                            int stt_thuoc = 1, stt_dv = 1, stt_cls = 1, stt_dienbien = 1;
                            try
                            {
                                var xEle = new XElement("CHECKOUT",
                                    new XElement("THONGTINBENHNHAN",
                                        new XElement("MA_LK", maBNhan),
                                        new XElement("NGAYGIOVAO", qtonghop.First().Ngay_vao == null ? "" : Convert.ToDateTime(qtonghop.First().Ngay_vao).ToString("yyyyMMddhhmm")),
                                        new XElement("NGAYGIORA", qtonghop.First().Ngay_ra == null ? "" : Convert.ToDateTime(qtonghop.First().Ngay_ra).ToString("yyyyMMddhhmm")),
                                        new XElement("MABENHVIEN", DungChung.Bien.MaBV),
                                        new XElement("CHANDOAN", qtonghop.First().rv.ChanDoan),
                                        new XElement("TRANGTHAI", delete ? 9 : (qtonghop.First().Status != null && Convert.ToInt32(qtonghop.First().Status) == 1) ? 3 : 2),
                                        new XElement("KETQUA", qtonghop.First().Status == 2 ? (qtonghop.First().rv.KetQua == null ? "2" : (qtonghop.First().rv.KetQua == "Khỏi" ? "1" : (qtonghop.First().rv.KetQua == "Đỡ|Giảm" ? "2" : (qtonghop.First().rv.KetQua == "Không T.đổi" ? "3" : (qtonghop.First().rv.KetQua == "Nặng hơn" ? "4" : "5"))))) : "2"),
                                        new XElement("SODIENTHOAI_LT", TTboXungs.Count > 0 ? TTboXungs.First().DThoai : ""),
                                        new XElement("NGUOILIENHE", TTboXungs.Count > 0 ? TTboXungs.First().NThan : "")),
                                   from qct in _lTongHop
                                   where (qct.Status == 1 && !delete)
                                   select

                                       new XElement("CHUYENTUYEN",
                                       new XElement("SOHOSO", qtonghop.First().rv.IdRaVien),//---------------------------Chưa có số hồ sơ
                                       new XElement("SOCHUYENTUYEN", qtonghop.First().rv.SoChuyenVien == null ? qtonghop.First().rv.IdRaVien : qtonghop.First().rv.SoChuyenVien),//--------------------------------------
                                       new XElement("MA_BV_CHUYENDEN", qtonghop.First().rv.MaBVC),//---------------------------------------
                                       new XElement("MA_BV_KHAMBENH", qtonghop.First().MaKCB),
                                       new XElement("TEN_CS_KHAMBENH", DungChung.Bien.TenCQ),
                                       new XElement("NGHENGHIEP", TTboXungs.Count > 0 ? TTboXungs.First().MaNN : ""),
                                       new XElement("NOILAMVIEC", TTboXungs.Count > 0 ? TTboXungs.First().NoiLV : ""),
                                       new XElement("LAMSANG", qtonghop.First().rv.LoiDan),
                                       new XElement("KETQUAXETNGHIEM", qtonghop.First().Status == 1 ? qtonghop.First().rv.KetQua : ""),
                                       new XElement("CHANDOAN", qtonghop.First().rv.ChanDoan),
                                       new XElement("PHUONGPHAPDIEUTRI", qtonghop.First().rv.PPDTr),
                                       new XElement("TINHTRANGNGUOIBENH", qtonghop.First().rv.TinhTrangC),// tinhftrangj chuyển
                                       new XElement("LYDO_CHUYENTUYEN", qtonghop.First().rv.LyDoC == "Đủ điều kiện chuyển tuyến(đúng tuyến)" ? 1 : 2),
                                       new XElement("HUONGDIEUTRI", ""),
                                       new XElement("THOIGIAN_CHUYEN", qtonghop.First().rv.NgayRa == null ? "" : Convert.ToDateTime(qtonghop.First().rv.NgayRa).ToString("yyyyMMdd")),
                                       new XElement("PHUONGTIEN", qtonghop.First().rv.HinhThucC),
                                       new XElement("NGUOI_HOTONG", qtonghop.First().rv.MaCB),
                                       new XElement("MA_QUOCTICH", TTboXungs.Count > 0 ? TTboXungs.First().NgoaiKieu : ""),
                                       new XElement("MA_DANTOC", TTboXungs.Count > 0 ? TTboXungs.First().MaDT : ""),
                                       new XElement("DSCHUYENVIEN",
                                       //from item2 in _lDSchuyenVien
                                       //select
                                       new XElement("THONGTINBENHVIEN",
                                       new XElement("MABV", qtonghop.First().rv.MaBVC),
                                       new XElement("TUYEN", GetTuyen(qtonghop.First().rv.MaBVC == null ? "" : qtonghop.First().rv.MaBVC.ToString(), data)),
                                       new XElement("TUNGAY", qtonghop.First().Ngay_ra == null ? "" : Convert.ToDateTime(qtonghop.First().Ngay_ra).ToString("yyyyMMdd")),
                                       new XElement("DENNGAY", qtonghop.First().rv.HanC == null ? "" : Convert.ToDateTime(qtonghop.First().rv.HanC).ToString("yyyyMMdd"))
                                       )//hết THONGTINBENHVIEN
                                       )//hết DSCHUYENVIEN
                                       ),//hết CHUYENTUYEN
                                  new XElement("THONGTINCHITIET",
                                      new XElement("TONGHOP",
                                          new XElement("MA_LK", maBNhan),
                                          new XElement("STT", maBNhan),
                                          new XElement("MA_BN", maBNhan),
                                          new XElement("HO_TEN", _lTongHop.First().Ho_ten),
                                    //new XElement("NGAY_SINH", GetNgaySinh(_lTongHop.First().NgaySinh, _lTongHop.First().ThangSinh, _lTongHop.First().NamSinh)),
                                           new XElement("NGAY_SINH", _lTongHop.First().Ngay_sinh),
                                          new XElement("GIOI_TINH", _lTongHop.First().Gioi_tinh == 0 ? 2 : 1),
                                          new XElement("DIA_CHI", _lTongHop.First().Dia_chi),
                                          new XElement("MA_THE", _lTongHop.First().Ma_the),
                                          new XElement("MA_DKBD", _lTongHop.First().Ma_dkbd),
                                          new XElement("GT_THE_TU", _lTongHop.First().Gt_the_tu == null ? "" : Convert.ToDateTime(_lTongHop.First().Gt_the_tu).ToString("yyyyMMdd")),
                                          new XElement("GT_THE_DEN", _lTongHop.First().Gt_the_den == null ? "" : Convert.ToDateTime(_lTongHop.First().Gt_the_den).ToString("yyyyMMdd")),
                                          new XElement("MA_BENH", GetICD(_lTongHop.First().Ma_benh == null ? "" : _lTongHop.First().Ma_benh)[0]),
                                          new XElement("MA_BENHKHAC", GetICD(_lTongHop.First().Ma_benh == null ? "" : _lTongHop.First().Ma_benh)[1]),
                                          new XElement("TEN_BENH", _lTongHop.First().Ten_benh),
                                          new XElement("MA_LYDO_VVIEN", _lTongHop.First().Ma_lydo_vvien),
                                          new XElement("MA_NOI_CHUYEN", _lTongHop.First().Ma_noi_chuyen),
                                          new XElement("MA_TAI_NAN", _lTongHop.First().Ma_tai_nan),
                                          new XElement("NGAY_VAO", _lTongHop.First().Ngay_vao == null ? "" : Convert.ToDateTime(_lTongHop.First().Ngay_vao).ToString("yyyyMMddHHmm")),
                                          new XElement("NGAY_RA", _lTongHop.First().Ngay_ra == null ? "" : Convert.ToDateTime(_lTongHop.First().Ngay_ra).ToString("yyyyMMddHHmm")),
                                          new XElement("SO_NGAY_DTRI", _lTongHop.First().So_ngay_dtri),
                                          new XElement("KET_QUA_DTRI", _lTongHop.First().Status == 2 ? (_lTongHop.First().Ket_qua_dtri == null ? "2" : (_lTongHop.First().Ket_qua_dtri == "Khỏi" ? "1" : (_lTongHop.First().Ket_qua_dtri == "Đỡ|Giảm" ? "2" : (_lTongHop.First().Ket_qua_dtri == "Không T.đổi" ? "3" : (_lTongHop.First().Ket_qua_dtri == "Nặng hơn" ? "4" : "5"))))) : "2"),
                                          new XElement("TINH_TRANG_RV", _lTongHop.First().Tinh_trang_rv),
                                          new XElement("NGAY_TTOAN", _lTongHop.First().Ngay_ttoan == null ? "" : Convert.ToDateTime(_lTongHop.First().Ngay_ttoan).ToString("yyyyMMddHHmm")),
                                          new XElement("MUC_HUONG", (_lTongHop.First().Ma_the == null || _lTongHop.First().Tuyen == null || _lTongHop.First().NoiTru == null || _lTongHop.First().NgayTT == null) ? 0 : (DungChung.Ham._getmuc(_listmuc, _hangbv, _lTongHop.First().Ma_the.ToString(), Convert.ToInt16(_lTongHop.First().Tuyen), Convert.ToInt16(_lTongHop.First().NoiTru), Convert.ToDateTime(_lTongHop.First().NgayTT)))),
                                          new XElement("T_TONGCHI", _lTongHop.First().T_tongchi),
                                          new XElement("T_BNTT", _lTongHop.First().T_bntt),
                                          new XElement("T_BHTT", _lTongHop.First().T_bhtt),
                                          new XElement("T_NGUONKHAC", _lTongHop.First().T_nguonkhac),
                                          new XElement("T_NGOAIDS", _lTongHop.First().T_ngoaids),
                                          new XElement("NAM_QT", _lTongHop.First().NgayTT == null ? "" : Convert.ToDateTime(_lTongHop.First().NgayTT).Year.ToString()),
                                          new XElement("THANG_QT", _lTongHop.First().NgayTT == null ? "" : Convert.ToDateTime(_lTongHop.First().NgayTT).Month.ToString()),
                                          new XElement("MA_LOAI_KCB", _lTongHop.First().Ma_loaikcb),
                                          new XElement("MA_CSKCB", _lTongHop.First().MaKCB),
                                          new XElement("MA_KHUVUC", _lTongHop.First().Ma_khuvuc),
                                          new XElement("MA_PTTT_QT", _lTongHop.First().Ma_pttt_qt),
                                          new XElement("T_THUOC", _lTongHop.First().T_Thuoc),//----------------------------------------------------
                                          new XElement("T_VTYT",_lTongHop.First().t_vtyt),//----------------------------------------------------
                                          new XElement("CAN_NANG", ""),
                                          new XElement("SO_PHIEU", _lTongHop.First().Sophieu),
                                          new XElement("NGAY_QUYETTOAN", _lTongHop.First().Ngay_quyettoan)
                                          ),// hết tổng hợp
                                        new XElement("Bang_CTTHUOC",
                                     from item2 in qtonghop.Where(p =>p.IDNhom== 4 || p.IDNhom == 5 || p.IDNhom == 6)
                                     select
                                             new XElement("CTTHUOC",
                                                  new XElement("MA_LK", maBNhan),
                                                  new XElement("STT", stt_thuoc++),
                                                  new XElement("MA_THUOC", (item2.MaQD == null || item2.MaQD == "") ? item2.MaDV + "_chk" : item2.MaQD),
                                                  new XElement("MA_NHOM", item2.IDNhom),
                                                  new XElement("TEN_THUOC", item2.TenDV),
                                                  new XElement("DON_VI_TINH", item2.DonVi),
                                                  new XElement("HAM_LUONG", item2.HamLuong),
                                                  new XElement("DUONG_DUNG", item2.DuongD),
                                                  new XElement("SO_DANG_KY", item2.SoDK),
                                                  new XElement("SO_LUONG",Math.Round( item2.SoLuong,2)),
                                                  new XElement("DON_GIA",Math.Round( item2.DonGia,2)),
                                                  new XElement("TYLE_TT", item2.TyleTT),
                                                  new XElement("THANH_TIEN",item2.ThanhTien),
                                                  new XElement("MA_KHOA", item2.MaKP),
                                                  new XElement("MA_BAC_SI", item2.MaBSi),
                                                  new XElement("MA_BENH", item2.Ma_benh),
                                                  new XElement("NGAY_YL", item2.NgayYL),
                                                  new XElement("LIEU_DUNG", ""),//-------------------------------------CHUA CO
                                                  new XElement("MA_PTTT", 1),//--------------------------------MÃ PHƯƠNG THỨC THANH TOÁN : PHÍ DỊCH VỤ = 0; ĐỊNH SUẤT = 1; DRG = 2
                                                  new XElement("TEN_KHOABV", ""),//TÊN KHOA BỆNH NHÂN ĐƯỢC CHỈ ĐỊNH SỬ DỤNG THUỐC
                                                  new XElement("DON_GIA_BV", ""),// ĐƠN GIÁ THUỐC CỦA BỆNH VIỆN LÀM TRÒN 2 SỐ TP
                                                  new XElement("NGUON_KHAC", item2.t_nguonkhac)
                                                  )),// hết CtThuoc
                                            new XElement("Bang_CTDV",
                                                 from item3 in qtonghop.Where(p => p.PLoai == 2 || (p.PLoai == 1 && (p.IDNhom == 7 || p.IDNhom == 10 || p.IDNhom == 11)))
                                                 select
                                            new XElement("CTDV",
                                                  new XElement("MA_LK", maBNhan),
                                                  new XElement("STT", stt_dv++),
                                                  new XElement("MA_DICH_VU", (item3.MaQD == null || item3.MaQD == "") ? item3.MaDV + "_chk" : item3.MaQD),
                                                  new XElement("MA_VAT_TU", ""),
                                                  new XElement("MA_NHOM", item3.IDNhom),
                                                  new XElement("TEN_DICH_VU", item3.TenDV),
                                                  new XElement("DON_VI_TINH", item3.DonVi),
                                                  new XElement("SO_LUONG",Math.Round( item3.SoLuong)),
                                                  new XElement("DON_GIA",Math.Round( item3.DonGia,2)),
                                                  new XElement("TYLE_TT", item3.TyleTT),
                                                  new XElement("THANH_TIEN",item3.ThanhTien),
                                                  new XElement("MA_KHOA", item3.MaKP),
                                                  new XElement("MA_BAC_SI", item3.MaBSi),
                                                  new XElement("MA_BENH", item3.Ma_benh),
                                                  new XElement("NGAY_YL", item3.NgayYL),
                                                  new XElement("NGAY_KQ", item3.NgayKQ),
                                                  new XElement("MA_PTTT", 1),//--------------------------------MÃ PHƯƠNG THỨC THANH TOÁN : PHÍ DỊCH VỤ = 0; ĐỊNH SUẤT = 1; DRG = 2
                                                  new XElement("TEN_KHOABV", ""),//TÊN KHOA BỆNH NHÂN ĐƯỢC CHỈ ĐỊNH SỬ DỤNG THUỐC
                                                  new XElement("DON_GIA_BV", ""),// ĐƠN GIÁ THUỐC CỦA BỆNH VIỆN LÀM TRÒN 2 SỐ TP
                                                  new XElement("NGUON_KHAC", item3.t_nguonkhac)
                                                  )),// hết Ct DICH VU

                                          new XElement("BANG_CT_CLS",
                                              from item4 in _lCLS
                                              select
                                                  new XElement("CLS",
                                                  new XElement("MA_LK", maBNhan),
                                                  new XElement("STT", stt_cls++),
                                                  new XElement("MA_DICH_VU", ""),
                                                  new XElement("MA_CHI_SO", ""),
                                                  new XElement("TEN_CHI_SO", ""),
                                                  new XElement("GIA_TRI", ""),
                                                  new XElement("MA_MAY", ""),
                                                  new XElement("MO_TA", ""),
                                                  new XElement("KET_LUAN", ""),
                                                  new XElement("NGAY_KQ", "")
                                                  )),// hết Ctcls

                                            new XElement("BANG_DIENBIENBENH",
                                                 from item5 in _lDienBien
                                                 select
                                                     new XElement("DIENBIENBENH",
                                                     new XElement("MA_LK", maBNhan),
                                                     new XElement("STT", stt_dienbien++),
                                                     new XElement("DIEN_BIEN", ""),
                                                     new XElement("HOI_CHAN", ""),
                                                     new XElement("PHAU_THUAT", ""),
                                                     new XElement("NGAY_YL", "")
                                                     )// hết DIỄN BIẾN BỆNH
                                          )));
                                //xEle.Save(path + "\\" + ngayvao + sthe + (delete ? "Delete" : "CHECKOUT.xml"));
                                string _tenfile = ngayvao + sthe + (delete ? "Delete.xml" : "CHECKOUT.xml");
                                xEle.Save(path + "\\" + _tenfile);
                                try { xEle.Save(path_bak + "\\" + _tenfile); }
                                catch { 
                                }
                                try
                                {
                                    
                                    xEle.Save(QLBV.BHYT.us_Export_XML_2348._config[1] + "\\" + _tenfile);
                                }
                                catch(Exception ex)
                                {
                                  //  error += ex.Message;
                                }
                                try  //gửi Server
                                {
                                    if (QLBV.BHYT.us_Export_XML_2348._config[4].Contains("ftp"))
                                    {
                                        //ftp ftpClient = new ftp(@"ftp://118.70.117.247/", taikhoan, MK);
                                        
                                        ftp ftpClient = new ftp(@QLBV.BHYT.us_Export_XML_2348._config[4], QLBV.BHYT.us_Export_XML_2348._config[5], QLBV.BHYT.us_Export_XML_2348._config[6]);
                                        ftpClient.upload(DungChung.Bien.MaBV + "/9324/" + _tenfile, path + "\\" + _tenfile);
                                    }
                                }
                                catch(Exception ex)
                                {
                                   // error += ex.Message;
                                    //System.Windows.Forms.MessageBox.Show("");
                                }
                                return true;
                            }
                            catch (Exception ex)
                            {
                                BHYT.us_Export_XML_2348.error += ex.Message;
                                return false;
                            }
                            #endregion tạo file xml 
                            
                        }
                        else return false;
                    }
                }
                else
                    return false;

            }
            }
            catch (Exception ex)
            {
                BHYT.us_Export_XML_2348.error += ex.Message;
                return false;
            }
        }

        private static string[] GetICD(string maBenh)
        {
            
            string _strIcd = "";
            string _strIcdKhac = "";
            if (maBenh != "")
            {
                string[] _arr = maBenh.Split(';');

                for (int i = 0; i < _arr.Length; i++)
                {
                    if (i == 0)
                        _strIcd = _arr[i];
                    if (_arr.Length > 1)
                    {
                        if (i == _arr.Length - 1)
                        {
                            _strIcdKhac += _arr[i];
                        }
                        else
                        {
                            _strIcdKhac += _arr[i] + ";";
                        }
                    }
                }
            }
            return new string[] { _strIcd, _strIcdKhac };
        }
        private static string GetTuyen(string maBV, QLBV_Database.QLBVEntities data)
        {
            string tuyen = "";
            var q = data.BenhViens.Where(p => p.MaBV == maBV).Select(p => p.TuyenBV).ToList();
            if (q.Count > 0 && q.First() != null)
                tuyen = q.First().ToString().Trim();
            return tuyen;
        }
        private static string GetNgaySinh(string ngay, string thang, string nam)
        {
            string trave = "";
            int int_ngay = 0, int_thang = 0;
            int ot;
            if (ngay != null && Int32.TryParse(ngay.ToString(), out ot))
                int_ngay = Convert.ToInt32(ngay);
            if ( thang != null && Int32.TryParse(thang.ToString(), out ot))
                int_thang = Convert.ToInt32(thang);
            if (int_thang > 0 && int_ngay > 0 && nam != null && nam.Trim() != "")
                trave = nam.ToString().Trim() + (thang.ToString().Trim().Length == 1 ? ("0" + thang.ToString().Trim()) : thang.ToString().Trim()) + (ngay.ToString().Trim().Length == 1 ? ("0" + ngay.ToString().Trim()) : ngay.ToString().Trim());
            else
                trave = (nam == null || nam.Trim() == "") ? "" : nam.ToString().Trim();
            return trave;
        }


    }
}
