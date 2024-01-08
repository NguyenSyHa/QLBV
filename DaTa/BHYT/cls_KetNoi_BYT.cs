using System;using QLBV_Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Net;
using System.Xml;
using System.IO;
using System.Web;
using System.Security.Cryptography;
using System.Globalization;
namespace QLBV.DungChung
{
    class cls_KetNoi_BYT
    {
        #region class
        public class checkin
        {
            private string Ma_lk;


            public string ma_lk
            {
                get { return Ma_lk; }
                set { Ma_lk = value; }
            }
            private string Mabenhvien;


            public string mabenhvien
            {
                get { return Mabenhvien; }
                set { Mabenhvien = value; }
            }
            private string Ma_the;


            public string ma_the
            {
                get { return Ma_the; }
                set { Ma_the = value; }
            }
            private string Ma_kcbbd;


            public string ma_kcbbd
            {
                get { return Ma_kcbbd; }
                set { Ma_kcbbd = value; }
            }
            private string Ho_ten;


            public string ho_ten
            {
                get { return Ho_ten; }
                set { Ho_ten = value; }
            }
            private string Ngay_sinh;


            public string ngay_sinh
            {
                get { return Ngay_sinh; }
                set { Ngay_sinh = value; }
            }
            private string Nam_sinh;


            public string nam_sinh
            {
                get { return Nam_sinh; }
                set { Nam_sinh = value; }
            }
            private string Gioi_tinh;


            public string gioi_tinh
            {
                get { return Gioi_tinh; }
                set { Gioi_tinh = value; }
            }
            private string Dia_chi;


            public string dia_chi
            {
                get { return Dia_chi; }
                set { Dia_chi = value; }
            }
            private string Tu_ngay;


            public string tu_ngay
            {
                get { return Tu_ngay; }
                set { Tu_ngay = value; }
            }
            private string Den_ngay;


            public string den_ngay
            {
                get { return Den_ngay; }
                set { Den_ngay = value; }
            }
            private string Matinhquanhuyen;


            public string matinhquanhuyen
            {
                get { return Matinhquanhuyen; }
                set { Matinhquanhuyen = value; }
            }
            private string Ngaygiovao;

            public string ngaygiovao
            {
                get { return Ngaygiovao; }
                set { Ngaygiovao = value; }
            }
            private string Ngaydu5nam;

            public string ngaydu5nam
            {
                get { return Ngaydu5nam; }
                set { Ngaydu5nam = value; }
            }
            private string Ma_noichuyenden;

            public string ma_noichuyenden
            {
                get { return Ma_noichuyenden; }
                set { Ma_noichuyenden = value; }
            }
            private string Lydodenkham;


            public string lydodenkham
            {
                get { return Lydodenkham; }
                set { Lydodenkham = value; }
            }

            private string Tinhtrangvaovien;


            public string tinhtrangvaovien
            {
                get { return Tinhtrangvaovien; }
                set { Tinhtrangvaovien = value; }
            }
            private string Sokhambenh;

            public string sokhambenh
            {
                get { return Sokhambenh; }
                set { Sokhambenh = value; }
            }
            private string Sodienthoai_lh;


            public string sodienthoai_lh
            {
                get { return Sodienthoai_lh; }
                set { Sodienthoai_lh = value; }
            }

            private string Nguoilienhe;


            public string nguoilienhe
            {
                get { return Nguoilienhe; }
                set { Nguoilienhe = value; }
            }
            private string Ma_khuvuc;

            public string ma_khuvuc
            {
                get { return Ma_khuvuc; }
                set { Ma_khuvuc = value; }
            }


        }
        public class phieuchuyentuyen
        {
            private string Ma_lk;
            /// <summary>
            /// bắt buộc
            /// </summary>
            public string ma_lk
            {
                get { return Ma_lk; }
                set { Ma_lk = value; }
            }
            private string Coquanchuquan;

            public string coquanchuquan
            {
                get { return Coquanchuquan; }
                set { Coquanchuquan = value; }
            }
            private string Macskcbdi;
            /// <summary>
            /// bắt buộc
            /// </summary>
            public string macskcbdi
            {
                get { return Macskcbdi; }
                set { Macskcbdi = value; }
            }
            /// <summary>
            /// bắt buộc
            /// </summary>
            private string Tencskcbdi;

            /// <summary>
            /// bắt buộc
            /// </summary>
            public string tencskcbdi
            {
                get { return Tencskcbdi; }
                set { Tencskcbdi = value; }
            }
            private string Macskcbden;
            /// <summary>
            /// bắt buộc
            /// </summary>
            public string macskcbden
            {
                get { return Macskcbden; }
                set { Macskcbden = value; }
            }
            /// <summary>
            /// bắt buộc
            /// </summary>
            private string Tencskcbden;

            /// <summary>
            /// bắt buộc
            /// </summary>
            public string tencskcbden
            {
                get { return Tencskcbden; }
                set { Tencskcbden = value; }
            }
            private string Sohoso;

            /// <summary>
            /// bắt buộc
            /// </summary>
            public string sohoso
            {
                get { return Sohoso; }
                set { Sohoso = value; }
            }
            private string Sochuyentuyen;

            /// <summary>
            /// số hồ sơ do cskcb lưu trữ
            /// </summary>
            public string sochuyentuyen
            {
                get { return Sochuyentuyen; }
                set { Sochuyentuyen = value; }
            }
            private string Sogiay;
            /// <summary>
            /// số giấy do cskcb lưu trữ
            /// </summary>
            public string sogiay
            {
                get { return Sogiay; }
                set { Sogiay = value; }
            }
            private string Ho_ten;
            /// <summary>
            /// bắt buộc
            /// </summary>
            public string ho_ten
            {
                get { return Ho_ten; }
                set { Ho_ten = value; }
            }
            private string Gioi_tinh;
            /// <summary>
            /// bắt buộc
            /// </summary>
            public string gioi_tinh
            {
                get { return Gioi_tinh; }
                set { Gioi_tinh = value; }
            }
            private string Nam_sinh;
            /// <summary>
            /// bắt buộc, định dạng "yyyyMMdd" hoặc "yyyy"
            /// </summary>
            public string nam_sinh
            {
                get { return Nam_sinh; }
                set { Nam_sinh = value; }
            }
            private string Dia_chi;
            /// <summary>
            /// bắt buộc
            /// </summary>
            public string dia_chi
            {
                get { return Dia_chi; }
                set { Dia_chi = value; }
            }
            private string Dan_toc;
            /// <summary>
            /// 3
            /// </summary>
            public string dan_toc
            {
                get { return Dan_toc; }
                set { Dan_toc = value; }
            }
            private string Quoc_tich;
            /// <summary>
            /// 3
            /// </summary>
            public string quoc_tich
            {
                get { return Quoc_tich; }
                set { Quoc_tich = value; }
            }
            private string Nghe_nghiep;

            public string nghe_nghiep
            {
                get { return Nghe_nghiep; }
                set { Nghe_nghiep = value; }
            }
            private string Noi_lamviec;

            public string noi_lamviec
            {
                get { return Noi_lamviec; }
                set { Noi_lamviec = value; }
            }
            private string Ma_the;
            /// <summary>
            /// 15, bắt buộc
            /// </summary>
            public string ma_the
            {
                get { return Ma_the; }
                set { Ma_the = value; }
            }
            private string Gt_the_tu;
            /// <summary>
            /// Bắt buộc, định dạng : "yyyyMMdd"
            /// </summary>
            public string gt_the_tu
            {
                get { return Gt_the_tu; }
                set { Gt_the_tu = value; }
            }
            private string Gt_the_den;
            /// <summary>
            /// Bắt buộc, định dạng : "yyyyMMdd"
            /// </summary>
            public string gt_the_den
            {
                get { return Gt_the_den; }
                set { Gt_the_den = value; }
            }
            private string Khamdieutritai;
            /// <summary>
            /// 10, bắt buộc, tên cơ sở kcb ?
            /// </summary>
            public string khamdieutritai
            {
                get { return Khamdieutritai; }
                set { Khamdieutritai = value; }
            }
            private string Kham_tungay;
            /// <summary>
            /// định dạng "yyyyMMdd"
            /// </summary>
            public string kham_tungay
            {
                get { return Kham_tungay; }
                set { Kham_tungay = value; }
            }
            private string Kham_denngay;

            /// <summary>
            /// định dạng "yyyyMMdd"
            /// </summary>
            public string kham_denngay
            {
                get { return Kham_denngay; }
                set { Kham_denngay = value; }
            }
            private string Tuyentruoc_chuyenden;

            /// <summary>
            /// 20, tên tuyến trước khi chuyển đến
            /// </summary>
            public string tuyentruoc_chuyenden
            {
                get { return Tuyentruoc_chuyenden; }
                set { Tuyentruoc_chuyenden = value; }
            }
            private string Ngay_chuyenden;

            /// <summary>
            /// định dạng "yyyyMMdd"
            /// </summary>
            public string ngay_chuyenden
            {
                get { return Ngay_chuyenden; }
                set { Ngay_chuyenden = value; }
            }
            private string So_chuyenden;
            /// <summary>
            /// 30, số giấy chuyển đến
            /// </summary>
            public string so_chuyenden
            {
                get { return So_chuyenden; }
                set { So_chuyenden = value; }
            }
            private string Dauhieu_lamsang;

            public string dauhieu_lamsang
            {
                get { return Dauhieu_lamsang; }
                set { Dauhieu_lamsang = value; }
            }
            private string Xetnghiem;

            public string xetnghiem
            {
                get { return Xetnghiem; }
                set { Xetnghiem = value; }
            }
            private string Chuandoan;

            public string chuandoan
            {
                get { return Chuandoan; }
                set { Chuandoan = value; }
            }
            private string Phuongphap_dtri;

            public string phuongphap_dtri
            {
                get { return Phuongphap_dtri; }
                set { Phuongphap_dtri = value; }
            }
            private string Tinhtrangchuyen;

            public string tinhtrangchuyen
            {
                get { return Tinhtrangchuyen; }
                set { Tinhtrangchuyen = value; }
            }
            private string Lydo_chuyen;
            /// <summary>
            /// 1, bắt buộc, 1: đủ điều kiện chuyển, 2: theo yêu cầu của người bệnh hoặc người nhà
            /// </summary>
            public string lydo_chuyen
            {
                get { return Lydo_chuyen; }
                set { Lydo_chuyen = value; }
            }
            private string Huong_dtri;

            public string huong_dtri
            {
                get { return Huong_dtri; }
                set { Huong_dtri = value; }
            }
            private string Ngay_chuyentuyen;
            /// <summary>
            /// bắt buộc, định dạng "yyyyMMdd"
            /// </summary>
            public string ngay_chuyentuyen
            {
                get { return Ngay_chuyentuyen; }
                set { Ngay_chuyentuyen = value; }
            }
            private string Phuongtien_chuyen;

            public string phuongtien_chuyen
            {
                get { return Phuongtien_chuyen; }
                set { Phuongtien_chuyen = value; }
            }
            private string Thongtin_hotong;

            public string thongtin_hotong
            {
                get { return Thongtin_hotong; }
                set { Thongtin_hotong = value; }
            }
            private string Bacsy_dtri;

            public string bacsy_dtri
            {
                get { return Bacsy_dtri; }
                set { Bacsy_dtri = value; }
            }
            private string Tenfile;
            /// <summary>
            /// tên file đính kèm
            /// </summary>
            public string tenfile
            {
                get { return Tenfile; }
                set { Tenfile = value; }
            }
            private string Loaifile;
            /// <summary>
            /// định dạng file: pdf hoặc jpg
            /// </summary>
            public string loaifile
            {
                get { return Loaifile; }
                set { Loaifile = value; }
            }
            private string Noidung_file;
            /// <summary>
            /// mã  hóa base64 của nội dung file
            /// </summary>
            public string noidung_file
            {
                get { return Noidung_file; }
                set { Noidung_file = value; }
            }
            private string Trangthai;

            /// <summary>
            /// 1, trạng thái
            /// </summary>
            public string trangthai
            {
                get { return Trangthai; }
                set { Trangthai = value; }
            }
        }
        public class tonghop
        {
            private string Ma_lk;

            public string ma_lk
            {
                get { return Ma_lk; }
                set { Ma_lk = value; }
            }


            private string Stt;

            public string stt
            {
                get { return Stt; }
                set { Stt = value; }
            }
            private string Ma_bn;

            public string ma_bn
            {
                get { return Ma_bn; }
                set { Ma_bn = value; }
            }
            private string Ho_ten;

            public string ho_ten
            {
                get { return Ho_ten; }
                set { Ho_ten = value; }
            }
            private string Ngay_sinh;

            public string ngay_sinh
            {
                get { return Ngay_sinh; }
                set { Ngay_sinh = value; }
            }
            private string Gioi_tinh;

            public string gioi_tinh
            {
                get { return Gioi_tinh; }
                set { Gioi_tinh = value; }
            }
            private string Dia_chi;

            public string dia_chi
            {
                get { return Dia_chi; }
                set { Dia_chi = value; }
            }
            private string Ma_the;

            public string ma_the
            {
                get { return Ma_the; }
                set { Ma_the = value; }
            }
            private string Ma_dkbd;

            public string ma_dkbd
            {
                get { return Ma_dkbd; }
                set { Ma_dkbd = value; }
            }
            private string Gt_the_tu;

            public string gt_the_tu
            {
                get { return Gt_the_tu; }
                set { Gt_the_tu = value; }
            }
            private string Gt_the_den;

            public string gt_the_den
            {
                get { return Gt_the_den; }
                set { Gt_the_den = value; }
            }
            private string Ten_benh;

            public string ten_benh
            {
                get { return Ten_benh; }
                set { Ten_benh = value; }
            }
            private string Ma_benh;

            public string ma_benh
            {
                get { return Ma_benh; }
                set { Ma_benh = value; }
            }
            private string Ma_benhkhac;

            public string ma_benhkhac
            {
                get { return Ma_benhkhac; }
                set { Ma_benhkhac = value; }
            }
            private string Ma_lydo_vvien;

            public string ma_lydo_vvien
            {
                get { return Ma_lydo_vvien; }
                set { Ma_lydo_vvien = value; }
            }
            private string Ma_noi_chuyen;

            public string ma_noi_chuyen
            {
                get { return Ma_noi_chuyen; }
                set { Ma_noi_chuyen = value; }
            }
            private string Ma_tai_nan;

            public string ma_tai_nan
            {
                get { return Ma_tai_nan; }
                set { Ma_tai_nan = value; }
            }
            private string Ngay_vao;

            public string ngay_vao
            {
                get { return Ngay_vao; }
                set { Ngay_vao = value; }
            }
            private string Ngay_ra;

            public string ngay_ra
            {
                get { return Ngay_ra; }
                set { Ngay_ra = value; }
            }
            private string So_ngay_dtri;

            public string so_ngay_dtri
            {
                get { return So_ngay_dtri; }
                set { So_ngay_dtri = value; }
            }
            private string Ket_qua_dtri;

            public string ket_qua_dtri
            {
                get { return Ket_qua_dtri; }
                set { Ket_qua_dtri = value; }
            }
            private string Tinh_trang_rv;

            public string tinh_trang_rv
            {
                get { return Tinh_trang_rv; }
                set { Tinh_trang_rv = value; }
            }
            private string Ngay_ttoan;

            public string ngay_ttoan
            {
                get { return Ngay_ttoan; }
                set { Ngay_ttoan = value; }
            }
            private string Muc_huong;

            public string muc_huong
            {
                get { return Muc_huong; }
                set { Muc_huong = value; }
            }
            private string T_thuoc;

            public string t_thuoc
            {
                get { return T_thuoc; }
                set { T_thuoc = value; }
            }
            private string T_vtyt;

            public string t_vtyt
            {
                get { return T_vtyt; }
                set { T_vtyt = value; }
            }
            private string t_tongchi;

            public string T_tongchi
            {
                get { return t_tongchi; }
                set { t_tongchi = value; }
            }
            private string T_bntt;

            public string t_bntt
            {
                get { return T_bntt; }
                set { T_bntt = value; }
            }
            private string T_bhtt;

            public string t_bhtt
            {
                get { return T_bhtt; }
                set { T_bhtt = value; }
            }
            private string T_nguonkhac;

            public string t_nguonkhac
            {
                get { return T_nguonkhac; }
                set { T_nguonkhac = value; }
            }
            private string T_ngoaids;

            public string t_ngoaids
            {
                get { return T_ngoaids; }
                set { T_ngoaids = value; }
            }
            private string Nam_qt;

            public string nam_qt
            {
                get { return Nam_qt; }
                set { Nam_qt = value; }
            }
            private string Thang_qt;

            public string thang_qt
            {
                get { return Thang_qt; }
                set { Thang_qt = value; }
            }
            private string Ma_loai_kcb;

            public string ma_loai_kcb
            {
                get { return Ma_loai_kcb; }
                set { Ma_loai_kcb = value; }
            }
            private string Ma_khoa;

            public string ma_khoa
            {
                get { return Ma_khoa; }
                set { Ma_khoa = value; }
            }
            private string Ma_cskcb;

            public string ma_cskcb
            {
                get { return Ma_cskcb; }
                set { Ma_cskcb = value; }
            }
            private string Ma_khuvuc;

            public string ma_khuvuc
            {
                get { return Ma_khuvuc; }
                set { Ma_khuvuc = value; }
            }
            private string Ma_pttt_qt;

            public string ma_pttt_qt
            {
                get { return Ma_pttt_qt; }
                set { Ma_pttt_qt = value; }
            }
            private string Can_nang;

            public string can_nang
            {
                get { return Can_nang; }
                set { Can_nang = value; }
            }

        }
        public class chi_tiet_thuoc
        {
            private string Ma_lk;

            public string ma_lk
            {
                get { return Ma_lk; }
                set { Ma_lk = value; }
            }
            private string Stt;

            public string stt
            {
                get { return Stt; }
                set { Stt = value; }
            }
            private string Ma_thuoc;

            public string ma_thuoc
            {
                get { return Ma_thuoc; }
                set { Ma_thuoc = value; }
            }
            private string Ma_nhom;

            public string ma_nhom
            {
                get { return Ma_nhom; }
                set { Ma_nhom = value; }
            }
            private string Ten_thuoc;

            public string ten_thuoc
            {
                get { return Ten_thuoc; }
                set { Ten_thuoc = value; }
            }
            private string Don_vi_tinh;

            public string don_vi_tinh
            {
                get { return Don_vi_tinh; }
                set { Don_vi_tinh = value; }
            }
            private string Ham_luong;

            public string ham_luong
            {
                get { return Ham_luong; }
                set { Ham_luong = value; }
            }
            private string Duong_dung;

            public string duong_dung
            {
                get { return Duong_dung; }
                set { Duong_dung = value; }
            }
            private string Lieu_dung;

            public string lieu_dung
            {
                get { return Lieu_dung; }
                set { Lieu_dung = value; }
            }
            private string So_dang_ky;

            public string so_dang_ky
            {
                get { return So_dang_ky; }
                set { So_dang_ky = value; }
            }
            private string So_luong;

            public string so_luong
            {
                get { return So_luong; }
                set { So_luong = value; }
            }
            private string Don_gia;

            public string don_gia
            {
                get { return Don_gia; }
                set { Don_gia = value; }
            }
            private string Tyle_tt;

            public string tyle_tt
            {
                get { return Tyle_tt; }
                set { Tyle_tt = value; }
            }
            private string Thanh_tien;

            public string thanh_tien
            {
                get { return Thanh_tien; }
                set { Thanh_tien = value; }
            }
            private string Ma_khoa;

            public string ma_khoa
            {
                get { return Ma_khoa; }
                set { Ma_khoa = value; }
            }
            private string Ma_bac_si;

            public string ma_bac_si
            {
                get { return Ma_bac_si; }
                set { Ma_bac_si = value; }
            }
            private string Ma_benh;

            public string ma_benh
            {
                get { return Ma_benh; }
                set { Ma_benh = value; }
            }
            private string Ngay_yl;

            public string ngay_yl
            {
                get { return Ngay_yl; }
                set { Ngay_yl = value; }
            }
            private string Ma_pttt;

            public string ma_pttt
            {
                get { return Ma_pttt; }
                set { Ma_pttt = value; }
            }
        }
        public class chi_tiet_dvkt
        {
            private string Ma_lk;

            public string ma_lk
            {
                get { return Ma_lk; }
                set { Ma_lk = value; }
            }
            private string Stt;

            public string stt
            {
                get { return Stt; }
                set { Stt = value; }
            }
            private string Ma_dich_vu;

            public string ma_dich_vu
            {
                get { return Ma_dich_vu; }
                set { Ma_dich_vu = value; }
            }
            private string Ma_vat_tu;

            public string ma_vat_tu
            {
                get { return Ma_vat_tu; }
                set { Ma_vat_tu = value; }
            }
            private string Ma_nhom;

            public string ma_nhom
            {
                get { return Ma_nhom; }
                set { Ma_nhom = value; }
            }
            private string Ten_dich_vu;

            public string ten_dich_vu
            {
                get { return Ten_dich_vu; }
                set { Ten_dich_vu = value; }
            }


            private string Don_vi_tinh;

            public string don_vi_tinh
            {
                get { return Don_vi_tinh; }
                set { Don_vi_tinh = value; }
            }

            private string So_luong;

            public string so_luong
            {
                get { return So_luong; }
                set { So_luong = value; }
            }
            private string Don_gia;

            public string don_gia
            {
                get { return Don_gia; }
                set { Don_gia = value; }
            }
            private string Tyle_tt;

            public string tyle_tt
            {
                get { return Tyle_tt; }
                set { Tyle_tt = value; }
            }
            private string Thanh_tien;

            public string thanh_tien
            {
                get { return Thanh_tien; }
                set { Thanh_tien = value; }
            }
            private string Ma_khoa;

            public string ma_khoa
            {
                get { return Ma_khoa; }
                set { Ma_khoa = value; }
            }
            private string Ma_bac_si;

            public string ma_bac_si
            {
                get { return Ma_bac_si; }
                set { Ma_bac_si = value; }
            }
            private string Ma_benh;

            public string ma_benh
            {
                get { return Ma_benh; }
                set { Ma_benh = value; }
            }
            private string Ngay_yl;

            public string ngay_yl
            {
                get { return Ngay_yl; }
                set { Ngay_yl = value; }
            }
            private string Ngay_kq;

            public string ngay_kq
            {
                get { return Ngay_kq; }
                set { Ngay_kq = value; }
            }
            private string Ma_pttt;

            public string ma_pttt
            {
                get { return Ma_pttt; }
                set { Ma_pttt = value; }
            }
        }
        public class chi_tiet_dien_bien_benh
        {
            private string Ma_lk;

            public string ma_lk
            {
                get { return Ma_lk; }
                set { Ma_lk = value; }
            }
            private string Stt;

            public string stt
            {
                get { return Stt; }
                set { Stt = value; }
            }
            private string Dien_bien;

            public string dien_bien
            {
                get { return Dien_bien; }
                set { Dien_bien = value; }
            }
            private string Hoi_chan;

            public string hoi_chan
            {
                get { return Hoi_chan; }
                set { Hoi_chan = value; }
            }
            private string Phau_thuat;

            public string phau_thuat
            {
                get { return Phau_thuat; }
                set { Phau_thuat = value; }
            }

            private string Ngay_yl;

            public string ngay_yl
            {
                get { return Ngay_yl; }
                set { Ngay_yl = value; }
            }

        }
        public class chi_tiet_cls
        {
            private string Ma_lk;
            public string ma_lk
            {
                get { return Ma_lk; }
                set { Ma_lk = value; }
            }
            private string Stt;
            public string stt
            {
                get { return Stt; }
                set { Stt = value; }
            }
            private string Ma_dich_vu;
            public string ma_dich_vu
            {
                get { return Ma_dich_vu; }
                set { Ma_dich_vu = value; }
            }
            private string Ma_chi_so;
            public string ma_chi_so
            {
                get { return Ma_chi_so; }
                set { Ma_chi_so = value; }
            }

            private string Ten_chi_so;
            public string ten_chi_so
            {
                get { return Ten_chi_so; }
                set { Ten_chi_so = value; }
            }

            private string Gia_tri;

            public string gia_tri
            {
                get { return Gia_tri; }
                set { Gia_tri = value; }
            }
            private string Ma_may;
            public string ma_may
            {
                get { return Ma_may; }
                set { Ma_may = value; }
            }

            private string Mo_ta;
            public string mo_ta
            {
                get { return Mo_ta; }
                set { Mo_ta = value; }
            }

            private string Ket_luan;
            public string ket_luan
            {
                get { return Ket_luan; }
                set { Ket_luan = value; }
            }

            private string Ngay_kq;
            public string ngay_kq
            {
                get { return Ngay_kq; }
                set { Ngay_kq = value; }
            }

        }
        public class ErrMs
        {
            string table;
            /// <summary>
            /// tên bảng lỗi
            /// </summary>
            public string Table
            {
                get { return table; }
                set { table = value; }
            }
            string colName;
            /// <summary>
            /// tên trường lỗi
            /// </summary>
            public string ColName
            {
                get { return colName; }
                set { colName = value; }
            }
            string errIDinTable;

            // Căn cứ để xác định lỗi: VD:mã bn, mã dịch vụ, stt cột, idĐon, id viện phí ...
            public string ID
            {
                get { return errIDinTable; }
                set { errIDinTable = value; }
            }
            string ms;

            public string Ms
            {
                get { return ms; }
                set { ms = value; }
            }
        }
        #endregion
        #region biến
        public List<ErrMs> _listErrMs = new List<ErrMs>();
        List<ErrMs> _listErrMs1BN = new List<ErrMs>();
        List<MucTT> _listmuc = new List<MucTT>();
        List<BenhNhan> _listBenhNhan = new List<BenhNhan>();
        List<TTboXung> _listTTboXung = new List<TTboXung>();
        List<VaoVien> _listVaoVien = new List<VaoVien>();
        List<RaVien> _listRaVien = new List<RaVien>();
        List<VienPhi> _listVienPhi = new List<VienPhi>();
        List<DichVu> _listDichVu = new List<DichVu>();
        public static string[] _error = new string[] { "", "" };// lỗi thông điệp trả về
        public int stt = 0;
        #endregion
        #region lấy thông tin thẻ của từng bệnh nhân
        /// <summary>
        /// Trả về thông tin 1 bệnh nhân theo mã bệnh nhân theo đối tượng checkin
        /// </summary>
        /// <param name="data"></param>
        /// <param name="maBN"></param>
        /// <returns></returns>
        /// <summary>
        /// Trả về thông tin 1 bệnh nhân theo mã bệnh nhân theo đối tượng checkin
        /// </summary>
        /// <param name="data"></param>
        /// <param name="maBN"></param>
        /// <returns></returns>
        private checkin getBenhNhanVaoVien(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv)
        {
            stt = 0;
            checkin moi = new checkin();
            List<ErrMs> _listErrMs1BN;

            if (bn != null)//kiểm tra các trường dữ liệu 
            {
                _listErrMs1BN = Validatecheckin(bn, vv, rv);
                //  _listErrMs.AddRange(_listErrMs1BN);
                if (_listErrMs1BN.Count == 0)
                {
                    moi.ma_lk = bn.MaBNhan.ToString();
                    moi.mabenhvien = QLBV.DungChung.Bien.MaBV;
                    moi.ma_the = bn.SThe;
                    moi.ma_kcbbd = bn.MaCS;
                    moi.ho_ten = bn.TenBNhan;
                    moi.ngay_sinh = getNgaySinh(bn.NgaySinh, bn.ThangSinh, bn.NamSinh);
                    moi.nam_sinh = bn.NamSinh;
                    moi.gioi_tinh = bn.GTinh == 1 ? "1" : "2";
                    moi.dia_chi = bn.DChi;
                    moi.tu_ngay = bn.HanBHTu.Value.ToString("yyyyMMdd");
                    moi.den_ngay = bn.HanBHDen.Value.ToString("yyyyMMdd");
                    moi.matinhquanhuyen = QLBV.DungChung.Bien.MaBV.Substring(0, 2);
                    moi.ngaygiovao = bn.NNhap.Value.ToString("yyyyMMddhhmm"); //= vv == null ? rv.NgayVao.Value.ToString("yyyyMMddhhmm") : vv.NgayVao.Value.ToString("yyyyMMddhhmm");
                   if(bn.NgayHM != null && bn.NgayHM.Value.ToString("yyyyMMdd") != "00010101")
                      moi.ngaydu5nam =  bn.NgayHM.Value.ToString("yyyyMMdd");
                    moi.ma_noichuyenden = bn.MaBV;
                    moi.lydodenkham = "";// chưa có
                    moi.tinhtrangvaovien = bn.CapCuu == 1 ? "2" : (bn.NoiTru == 1 ? "1" : "3");
                    moi.sokhambenh = bn.MaBNhan.ToString();//??????????????????
                    if (ttbs != null)
                    {
                        moi.sodienthoai_lh = ttbs.DThoai;
                        moi.nguoilienhe = ttbs.NThan;
                    }
                    if (bn.KhuVuc.Length >= 2)
                        moi.ma_khuvuc = bn.KhuVuc.Substring(1, 1);
                    else
                        moi.ma_khuvuc = "";
                    return moi;
                }
                else return null;
            }
            else
                return null;

        }
        private phieuchuyentuyen getBenhNhanChuyenTuyen(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp)
        {
            stt = 0;
            phieuchuyentuyen moi = new phieuchuyentuyen();

            // chưa kiểm tra thông tin bệnh nhân bổ sung (ttbs, mã tỉnh, mã huyện), vào viện (nếu = null ==> thông báo)
            List<ErrMs> q = ValidateChuyenTuyen(bn, rv);
            _listErrMs.AddRange(q);
            if (bn != null && rv != null && q.Count == 0)//kiểm tra các trường dữ liệu 
            {
                moi.ma_lk = bn.MaBNhan.ToString();
                moi.coquanchuquan = QLBV.DungChung.Bien.TenCQCQ;//.MaCQCQ;???????
                moi.macskcbdi = QLBV.DungChung.Bien.MaBV;
                moi.tencskcbdi = QLBV.DungChung.Bien.TenCQ;//TenBV;??????
                moi.macskcbden = rv.MaBVC;
                moi.tencskcbden = getTenBVByMaBV(data, rv.MaBVC);
                moi.sohoso = rv.IdRaVien.ToString(); // chưa có số hồ sơ
                moi.sochuyentuyen = rv.SoChuyenVien == null ? rv.IdRaVien.ToString() : rv.SoChuyenVien.Value.ToString();//????????????
                moi.sogiay = rv.SoGT == null ? "" : rv.SoGT.Value.ToString();// số giấy
                moi.ho_ten = bn.TenBNhan;
                moi.gioi_tinh = bn.GTinh == 1 ? "1" : "2";
                moi.nam_sinh = bn.NamSinh;
                moi.dia_chi = bn.DChi;
                if (ttbs != null)
                {
                    moi.dan_toc = ttbs.MaDT;
                    moi.quoc_tich = ttbs.NgoaiKieu;
                    moi.nghe_nghiep = ttbs.MaNN; //NgheNghiep;????
                    moi.noi_lamviec = ttbs.NoiLV;
                }
                moi.ma_the = bn.SThe;
                moi.gt_the_tu = bn.HanBHTu.Value.ToString("yyyyMMdd");
                moi.gt_the_den = bn.HanBHDen.Value.ToString("yyyyMMdd");
                moi.khamdieutritai = QLBV.DungChung.Bien.TenCQ;// QLBV.DungChung.Bien.TenBV;//10 ký tự ???????
                moi.kham_tungay = rv.NgayVao.Value.ToString("yyyyMMdd");// kiểm tra ngày vào viện           
                moi.kham_denngay = rv.NgayRa.Value.ToString("yyyyMMdd");
                moi.tuyentruoc_chuyenden = getTuyen(data, bn.MaBV);
                moi.ngay_chuyenden = bn.NNhap.Value.ToString("yyyyMMdd");
                moi.so_chuyenden = "";// chưa có
                moi.dauhieu_lamsang = rv.ChanDoan;//?
                moi.xetnghiem = rv.KetQua; //?
                moi.phuongphap_dtri = rv.PPDTr;
                moi.tinhtrangchuyen = rv.TinhTrangC;
                moi.lydo_chuyen = rv.LyDoC == "Đủ điều kiện chuyển tuyến(đúng tuyến)" ? "1" : "2";
                moi.huong_dtri = rv.LoiDan;//?
                moi.ngay_chuyentuyen = rv.NgayRa.Value.ToString("yyyyMMdd");
                moi.phuongtien_chuyen = rv.HinhThucC;
                moi.thongtin_hotong = ""; //?
                moi.bacsy_dtri = GetTenCanBo(data, rv.MaCB);
                moi.tenfile = "";
                moi.loaifile = "";
                moi.noidung_file = "";
                moi.trangthai = "1"; //?
                return moi;
            }
            else return null;

        }
        private tonghop gettonghop(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp, int count)
        {
            stt = 0;
            tonghop moi = new tonghop(); ;
            var q1 = (
                      from vpct in data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi)
                      select new
                      {
                          vpct.MaDV,
                          vpct.SoLuong,
                          vpct.DonGia,
                          vpct.ThanhTien,
                          vpct.TienBH,
                          vpct.TienBN,
                          vpct.TrongBH,
                      }).ToList();
            var q2 = (from dv in data.DichVus
                      join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                      join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                      select new
                      {
                          dv.MaDV,
                          tn.IdTieuNhom,
                          n.IDNhom,
                          n.TenNhomCT

                      }).ToList();
            var qvpct = (from vpct in q1
                         join dv in q2 on vpct.MaDV equals dv.MaDV
                         select new
                         {
                             vpct.MaDV,
                             vpct.SoLuong,
                             vpct.DonGia,
                             vpct.ThanhTien,
                             vpct.TienBH,
                             vpct.TienBN,
                             vpct.TrongBH,
                             dv.IdTieuNhom,
                             dv.IDNhom,
                             dv.TenNhomCT
                         }).ToList();

            //var qvpct = (from vphi in data.VienPhis.Where(p => p.MaBNhan == maBN)
            //             join vpct in data.VienPhicts on vphi.idVPhi equals vpct.idVPhi
            //             join dv in data.DichVus on vpct.MaDV equals dv.MaDV
            //             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
            //             join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
            //             select new
            //             {
            //                 vpct.MaDV,
            //                 vpct.SoLuong,
            //                 vpct.DonGia,
            //                 vpct.ThanhTien,
            //                 vpct.TienBH,
            //                 vpct.TienBN,
            //                 vpct.TrongBH,
            //                 tn.IdTieuNhom,
            //                 n.IDNhom,
            //                 n.TenNhomCT

            //             }).ToList();

            List<ErrMs> q = ValidateTonghop(bn, rv, vp);
            _listErrMs.AddRange(q);
            if (bn != null && rv != null && vp != null && q.Count == 0)//kiểm tra các trường dữ liệu 
            {
                stt++;
                moi.ma_lk = bn.MaBNhan.ToString();
                moi.stt = stt.ToString();
                moi.ma_bn = bn.MaBNhan.ToString();
                moi.ho_ten = bn.TenBNhan;
                moi.ngay_sinh = getNgaySinh(bn.NgaySinh, bn.ThangSinh, bn.NamSinh);
                moi.gioi_tinh = bn.GTinh == 1 ? "1" : "2";
                moi.dia_chi = bn.DChi;
                moi.ma_the = bn.SThe;
                moi.ma_dkbd = bn.MaCS;
                moi.gt_the_tu = bn.HanBHTu.Value.ToString("yyyyMMdd");
                moi.gt_the_den = bn.HanBHDen.Value.ToString("yyyyMMdd");
                moi.ten_benh = rv.ChanDoan;
                moi.ma_benh = GetICD(rv.MaICD)[0];
                moi.ma_benhkhac = GetICD(rv.MaICD)[1];
                moi.ma_lydo_vvien = bn.CapCuu == 1 ? "2" : (bn.NoiTru == 1 ? "1" : "3");
                moi.ma_noi_chuyen = rv.MaBVC;
                moi.ma_tai_nan = "";// chưa có
                moi.ngay_vao = rv.NgayVao.Value.ToString("yyyyMMdd");
                moi.ngay_ra = rv.NgayRa.Value.ToString("yyyyMMdd");
                moi.so_ngay_dtri = rv.SoNgaydt.Value.ToString();
                if (rv.KetQua == null)
                    moi.ket_qua_dtri = "2";
                else if (rv.KetQua == "Khỏi")
                    moi.ket_qua_dtri = "1";
                else if (rv.KetQua == "Đỡ|Giảm")
                    moi.ket_qua_dtri = "2";
                else if (rv.KetQua == "Không T.đổi")
                    moi.ket_qua_dtri = "3";
                else if (rv.KetQua == "Nặng hơn")
                    moi.ket_qua_dtri = "4";
                else
                    moi.ket_qua_dtri = "5";
                if (rv.Status == null || rv.Status == 2)
                    moi.tinh_trang_rv = "1";
                else if (rv.Status == 1)
                    moi.tinh_trang_rv = "2";
                else
                    moi.tinh_trang_rv = rv.Status.Value.ToString();
                moi.ngay_ttoan = vp.NgayTT.Value.ToString("yyyyMMdd");
                if (bn.NoiTru == null || bn.Tuyen == null)
                    moi.muc_huong = "0";
                else
                {
                    int hangbv = DungChung.Ham.hangBV(QLBV.DungChung.Bien.MaBV);
                    moi.muc_huong = _getmuc(_listmuc, hangbv, bn.SThe, bn.Tuyen.Value, bn.NoiTru.Value, vp.NgayTT.Value).ToString();
                }
                double tienthuoc, tienvtyt, tongtien, t_bntt, t_bhtt, t_ngoaids;
                tienthuoc = qvpct.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).Sum(p => p.ThanhTien);// làm tròn trước nếu cần thiết
                tienvtyt = qvpct.Where(p =>  p.IDNhom == 10 || p.IDNhom == 11).Sum(p => p.ThanhTien);// làm tròn trước nếu cần thiết
                tongtien = qvpct.Sum(p => p.ThanhTien);// làm tròn trước nếu cần thiết
                t_bntt = qvpct.Sum(p => p.TienBN);// làm tròn trước nếu cần thiết
                t_bhtt = qvpct.Sum(p => p.TienBH);// làm tròn trước nếu cần thiết
                t_ngoaids = qvpct.Where(p => p.TenNhomCT == "Vận chuyển").Sum(p => p.ThanhTien);// chưa làm tròn
                moi.t_thuoc =Math.Round( tienthuoc,0).ToString();
                moi.t_vtyt = Math.Round(tienvtyt, 0).ToString();
                moi.T_tongchi = Math.Round(tongtien, 0).ToString();
                moi.t_bhtt = Math.Round(t_bhtt, 0).ToString();
                moi.t_bntt = Math.Round(t_bntt, 0).ToString();
                moi.t_nguonkhac = "0";
                moi.t_ngoaids = t_ngoaids.ToString();
                moi.nam_qt = vp.NgayTT.Value.Year.ToString();
                moi.thang_qt = vp.NgayTT.Value.Month.ToString("D2");
                if (bn.NoiTru == 1)
                    moi.ma_loai_kcb = "3";
                else if (bn.DTNT == true)
                    moi.ma_loai_kcb = "2";
                else
                    moi.ma_loai_kcb = "1";
                moi.ma_khoa = rv.MaKP.ToString();
                moi.ma_cskcb = QLBV.DungChung.Bien.MaBV;
                moi.ma_khuvuc = bn.KhuVuc;
                moi.ma_pttt_qt = "";// ??????
                moi.can_nang = "";//?????????     
                return moi;
            }
            else
                return null;

        }
        private List<chi_tiet_thuoc> getChitietThuoc(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp, int count)
        {
            stt = 0;
            List<chi_tiet_thuoc> listThuoc = new List<chi_tiet_thuoc>();

            var qvpct = (
                         from vpct in data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi)
                         join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                         join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                         join n in data.NhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6) on tn.IDNhom equals n.IDNhom
                         select new
                         {
                             dv.BHTT,
                             vpct.MaDV,
                             vpct.SoLuong,
                             vpct.DonGia,
                             vpct.ThanhTien,
                             vpct.TienBH,
                             vpct.TienBN,
                             vpct.TrongBH,
                             vpct.MaKP,
                             tn.IdTieuNhom,
                             n.IDNhom,
                             n.TenNhomCT,
                             dv.MaQD,
                             dv.SoQD,
                             dv.TenHC,
                             dv.DonVi,
                             dv.HamLuong,
                             dv.MaDuongDung,
                             dv.SoDK,
                             dv.TenDV,


                         }).ToList();


            List<ErrMs> q = ValidateTonghop(bn, rv, vp);//validate?????????
            _listErrMs.AddRange(q);
            if (bn != null && rv != null && vp != null && q.Count == 0)//kiểm tra các trường dữ liệu 
            {

                foreach (var a in qvpct)
                {
                    chi_tiet_thuoc moi = new chi_tiet_thuoc();
                    stt++;
                    moi.ma_lk = bn.MaBNhan.ToString();
                    moi.stt = stt.ToString();
                    moi.ma_thuoc = string.IsNullOrEmpty(a.MaQD) ? a.MaDV.ToString() : a.MaQD;
                    moi.ma_nhom = a.IDNhom.ToString();
                    moi.ten_thuoc =String.IsNullOrEmpty( a.TenHC)? a.TenDV : a.TenHC ; //????? Tên ghi đúng theo danh mục thuốc cục qld công bố
                    moi.don_vi_tinh = a.DonVi;
                    moi.ham_luong = a.HamLuong;
                    moi.duong_dung = a.MaDuongDung;
                    moi.lieu_dung = "";//?????????????????????????
                    moi.so_dang_ky = a.SoDK;
                    moi.so_luong = a.SoLuong.ToString();
                    moi.don_gia = a.DonGia.ToString("G", CultureInfo.InvariantCulture);
                        moi.tyle_tt = a.BHTT == null ? "100" : a.BHTT.ToString();
                    moi.thanh_tien = a.ThanhTien.ToString("G", CultureInfo.InvariantCulture);
                    moi.ma_khoa = getMaKhoa_QD(a.MaKP);// mã kpQD
                    moi.ma_bac_si = getMaBacSy(bn.MaBNhan, a.MaDV, a.MaKP);
                    moi.ma_benh = rv.MaICD;
                    moi.ma_pttt = "1";// Chưa có dữ liệu để lấy nên fix cứng
                    listThuoc.Add(moi);
                }
                return listThuoc;
            }
            else
                return null;

        }
        private List<chi_tiet_dvkt> getChitietDvkt(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp, int count)
        {
            stt = 0;
            List<chi_tiet_dvkt> listThuoc = new List<chi_tiet_dvkt>();

            var qvpct = (
                         from vpct in data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi)
                         join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                         join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                         join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                         where (dv.PLoai == 2 || (dv.PLoai == 1) && (n.IDNhom == 7 || n.IDNhom == 10 || n.IDNhom == 11))
                         select new
                         {
                             dv.BHTT,
                             vpct.MaDV,
                             vpct.SoLuong,
                             vpct.DonGia,
                             vpct.ThanhTien,
                             vpct.TienBH,
                             vpct.TienBN,
                             vpct.TrongBH,
                             vpct.MaKP,
                             tn.IdTieuNhom,
                             n.IDNhom,
                             n.TenNhomCT,
                             dv.MaQD,
                             dv.TenRG,
                             dv.TenDV,
                             dv.SoQD,
                             dv.TenHC,
                             dv.DonVi,
                             dv.HamLuong,
                             dv.MaDuongDung,
                             dv.SoDK,
                         }).ToList();


            List<ErrMs> q = ValidateTonghop(bn, rv, vp);//validate?????????
            _listErrMs.AddRange(q);
            if (bn != null && rv != null && vp != null && q.Count == 0)//kiểm tra các trường dữ liệu 
            {

                foreach (var a in qvpct)
                {
                    chi_tiet_dvkt moi = new chi_tiet_dvkt();
                    stt++;
                    moi.ma_lk = bn.MaBNhan.ToString();
                    moi.stt = stt.ToString();
                    moi.ma_dich_vu = String.IsNullOrEmpty(a.MaQD) ? a.MaDV.ToString() : a.MaQD;
                    moi.ma_vat_tu = a.MaDV.ToString();//?????? mã vật tư sử dụng quy định tại bộ mã danh mục dùng chung của bộ y tế, chỉ ghi các vật tư chưa có trong cơ cấu giá dịch vụ ??????
                    moi.ma_nhom = a.IDNhom.ToString();
                    moi.ten_dich_vu = a.TenDV; //????? Tên ghi đúng theo danh mục thuốc cục qld công bố
                    moi.don_vi_tinh = a.DonVi;
                    moi.so_luong = a.SoLuong.ToString();
                    moi.don_gia = a.DonGia.ToString("G", CultureInfo.InvariantCulture);
                        moi.tyle_tt = a.BHTT == null ? "100" : a.BHTT.ToString();
                    moi.thanh_tien = a.ThanhTien.ToString("G", CultureInfo.InvariantCulture);
                    moi.ma_khoa = getMaKhoa_QD(a.MaKP);// mã kpQD
                    moi.ma_bac_si = getMaBacSy(bn.MaBNhan, a.MaDV, a.MaKP);
                    moi.ma_benh = rv.MaICD;
                    moi.ngay_yl = getNgayYLenh_KetQua(bn.MaBNhan, a.MaDV, a.MaKP)[0];
                    moi.ngay_kq = getNgayYLenh_KetQua(bn.MaBNhan, a.MaDV, a.MaKP)[1];
                    moi.ma_pttt = "1";// Chưa có dữ liệu để lấy nên fix cứng
                    listThuoc.Add(moi);
                }
                return listThuoc;
            }
            else
                return null;
        }
        private List<chi_tiet_dien_bien_benh> getDenBien(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp, int count)
        {
            return new List<chi_tiet_dien_bien_benh>();
        }
        private List<chi_tiet_cls> getCLS(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp, int count)
        {
            stt = 0;
            List<chi_tiet_cls> listcls = new List<chi_tiet_cls>();
            var qcls = (from cls in data.CLS.Where(p => p.MaBNhan == bn.MaBNhan)
                        join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                        join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                        join dvct in data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                        select new
                        {
                            dvct.MaDV,// lấy ra mãQĐ
                            clsct.MaDVct,
                            dvct.TenDVct,
                            dvct.TenMay,// mã máy
                            clsct.KetQua,
                            KetquaXN = "",// clsct.KetquaXN,// mô tả????????
                            cls.NgayTH,// ngày kq
                            cd.KetLuan//kết luận
                        }).ToList();

            List<ErrMs> q = ValidateTonghop(bn, rv, vp);//validate?????????
            _listErrMs.AddRange(q);
            if (bn != null && rv != null && vp != null && q.Count == 0)//kiểm tra các trường dữ liệu 
            {
                // var qdv = data.DichVus.ToList();
                foreach (var a in qcls)
                {
                    if (!String.IsNullOrEmpty(a.KetQua))
                    {
                        chi_tiet_cls moi = new chi_tiet_cls();
                        stt++;
                        moi.ma_lk = bn.MaBNhan.ToString();
                        moi.stt = stt.ToString();
                        moi.ma_dich_vu = String.IsNullOrEmpty(getMaDvQD(a.MaDV)) ? a.MaDV.ToString() : getMaDvQD(a.MaDV);
                        moi.ma_chi_so = a.MaDVct;
                        moi.ten_chi_so = a.TenDVct;
                        moi.gia_tri = a.KetQua;
                        moi.ma_may = a.TenMay;
                        moi.mo_ta = "";//a.KetquaXN;?????????
                        moi.ket_luan = a.KetLuan;
                        moi.ngay_kq = a.NgayTH == null ? "" : a.NgayTH.Value.ToString("yyyyMMddhhmm");
                        listcls.Add(moi);
                    }
                }
                return listcls;
            }
            else
                return null;

        }
        public object getObject(int bang, QLBV_Database.QLBVEntities data, int maBN)
        {
            BenhNhan bn = data.BenhNhans.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            TTboXung ttbs = data.TTboXungs.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            VaoVien vv = data.VaoViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            RaVien rv = data.RaViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            VienPhi vp = data.VienPhis.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            // Object moi;

            List<ErrMs> q = Validatecheckin(bn, vv, rv);
            #region checkin
            if (bang == 1)
            {
                checkin moi = new checkin();
                //List<ErrMs> q = Validatecheckin(bn, vv);
                _listErrMs.AddRange(q);
                if (bn != null && q.Count == 0)//kiểm tra các trường dữ liệu 
                {

                    moi.ma_lk = maBN.ToString();
                    moi.mabenhvien = QLBV.DungChung.Bien.MaBV;
                    moi.ma_the = bn.SThe;
                    moi.ma_kcbbd = bn.MaCS;
                    moi.ho_ten = bn.TenBNhan;
                    moi.ngay_sinh = getNgaySinh(bn.NgaySinh, bn.ThangSinh, bn.NamSinh);
                    moi.nam_sinh = bn.NamSinh;
                    moi.gioi_tinh = bn.GTinh == 1 ? "1" : "2";
                    moi.dia_chi = bn.DChi;
                    moi.tu_ngay = bn.HanBHTu.Value.ToString("yyyyMMdd");
                    moi.den_ngay = bn.HanBHDen.Value.ToString("yyyyMMdd");
                    moi.matinhquanhuyen = bn.SThe.Substring(0, 4);
                    moi.ngaygiovao = vv.NgayVao.Value.ToString("yyyyMMddhhmm");
                    moi.ngaydu5nam = bn.NgayHM.Value.ToString("yyyyMMdd");
                    moi.ma_noichuyenden = bn.MaBV;
                    moi.lydodenkham = "";// chưa có
                    moi.tinhtrangvaovien = bn.CapCuu == 1 ? "2" : (bn.NoiTru == 1 ? "1" : "3");
                    moi.sokhambenh = bn.MaBNhan.ToString();//??????????????????
                    if (ttbs != null)
                    {
                        moi.sodienthoai_lh = ttbs.DThoai;
                        moi.nguoilienhe = ttbs.NThan;
                    }
                    moi.ma_khuvuc = bn.KhuVuc;
                }
                return moi;
            }
            #endregion
            #region getBenhNhanChuyenTuyen
            else if (bang == 2)
            {
                phieuchuyentuyen moi = new phieuchuyentuyen();
                //List<ErrMs> q = ValidateChuyenTuyen(bn, rv);
                _listErrMs.AddRange(q);
                if (bn != null && rv != null && q.Count == 0)//kiểm tra các trường dữ liệu 
                {
                    moi.ma_lk = maBN.ToString();
                    moi.coquanchuquan = DungChung.Bien.TenCQ; //QLBV.DungChung.Bien.MaCQCQ;
                    moi.macskcbdi = QLBV.DungChung.Bien.MaBV;
                    moi.tencskcbdi = DungChung.Bien.TenCQ;// QLBV.DungChung.Bien.TenBV;
                    moi.macskcbden = rv.MaBVC;
                    moi.tencskcbden = getTenBVByMaBV(data, rv.MaBVC);
                    moi.sohoso = rv.IdRaVien.ToString(); // chưa có số hồ sơ
                    moi.sochuyentuyen = rv.SoChuyenVien == null ? "" : rv.SoChuyenVien.Value.ToString();
                    moi.sogiay = rv.SoGT == null ? "" : rv.SoGT.Value.ToString();// số giấy
                    moi.ho_ten = bn.TenBNhan;
                    moi.gioi_tinh = bn.GTinh == 1 ? "1" : "2";
                    moi.nam_sinh = bn.NamSinh;
                    moi.dia_chi = bn.DChi;
                    if (ttbs != null)
                    {
                        moi.dan_toc = ttbs.MaDT;
                        moi.quoc_tich = ttbs.NgoaiKieu;
                        moi.nghe_nghiep = ttbs.MaNN; //NgheNghiep;//?????????
                        moi.noi_lamviec = ttbs.NoiLV;
                    }
                    moi.ma_the = bn.SThe;
                    moi.gt_the_tu = bn.HanBHTu.Value.ToString("yyyyMMdd");
                    moi.gt_the_den = bn.HanBHDen.Value.ToString("yyyyMMdd");
                    moi.khamdieutritai = DungChung.Bien.TenCQ;//10 ký tự ???????
                    moi.kham_tungay = rv.NgayVao.Value.ToString("yyyyMMdd");// kiểm tra ngày vào viện
                    moi.kham_denngay = rv.NgayRa.Value.ToString("yyyyMMdd");
                    moi.tuyentruoc_chuyenden = getTuyen(data, bn.MaBV);
                    moi.ngay_chuyenden = bn.NNhap.Value.ToString("yyyyMMdd");
                    moi.so_chuyenden = "";// chưa có
                    moi.dauhieu_lamsang = rv.ChanDoan;//?
                    moi.xetnghiem = rv.KetQua; //?
                    moi.phuongphap_dtri = rv.PPDTr;
                    moi.tinhtrangchuyen = rv.TinhTrangC;
                    moi.lydo_chuyen = rv.LyDoC == "Đủ điều kiện chuyển tuyến(đúng tuyến)" ? "1" : "2";
                    moi.huong_dtri = rv.LoiDan;//?
                    moi.ngay_chuyentuyen = rv.NgayRa.Value.ToString("yyyyMMdd");
                    moi.phuongtien_chuyen = rv.HinhThucC;
                    moi.thongtin_hotong = ""; //?
                    moi.bacsy_dtri = GetTenCanBo(data, rv.MaCB);
                    moi.tenfile = "";
                    moi.loaifile = "";
                    moi.noidung_file = "";
                    moi.trangthai = "1"; //?
                }
                return moi;
            }
            #endregion
            #region tonghop
            else if (bang == 3)
            {
                tonghop moi = new tonghop();
                var qvpct = (from vphi in data.VienPhis.Where(p => p.MaBNhan == maBN)
                             join vpct in data.VienPhicts on vphi.idVPhi equals vpct.idVPhi
                             join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                             select new
                             {
                                 vpct.MaDV,
                                 vpct.SoLuong,
                                 vpct.DonGia,
                                 vpct.ThanhTien,
                                 vpct.TienBH,
                                 vpct.TienBN,
                                 vpct.TrongBH,
                                 tn.IdTieuNhom,
                                 n.IDNhom,
                                 n.TenNhomCT
                             }).ToList();

                //  List<ErrMs> q = ValidateTonghop(bn, rv, vp);
                _listErrMs.AddRange(q);
                if (bn != null && rv != null && vp != null && q.Count == 0)//kiểm tra các trường dữ liệu 
                {
                    stt++;
                    moi.ma_lk = maBN.ToString();
                    moi.stt = stt.ToString();
                    moi.ma_bn = bn.MaBNhan.ToString();
                    moi.ho_ten = bn.TenBNhan;
                    moi.ngay_sinh = getNgaySinh(bn.NgaySinh, bn.ThangSinh, bn.NamSinh);
                    moi.gioi_tinh = bn.GTinh == 1 ? "1" : "2";
                    moi.dia_chi = bn.DChi;
                    moi.ma_the = bn.SThe;
                    moi.ma_dkbd = bn.MaCS;
                    moi.gt_the_tu = bn.HanBHTu.Value.ToString("yyyyMMdd");
                    moi.gt_the_den = bn.HanBHDen.Value.ToString("yyyyMMdd");
                    moi.ten_benh = rv.ChanDoan;
                    moi.ma_benh = GetICD(rv.MaICD)[0];
                    moi.ma_benhkhac = GetICD(rv.MaICD)[1];
                    moi.ma_lydo_vvien = bn.CapCuu == 1 ? "2" : (bn.NoiTru == 1 ? "1" : "3");
                    moi.ma_noi_chuyen = rv.MaBVC;
                    moi.ma_tai_nan = "";// chưa có
                    moi.ngay_vao = rv.NgayVao.Value.ToString("yyyyMMdd");
                    moi.ngay_ra = rv.NgayRa.Value.ToString("yyyyMMdd");
                    moi.so_ngay_dtri = rv.SoNgaydt.Value.ToString();
                    if (rv.KetQua == null)
                        moi.ket_qua_dtri = "2";
                    else if (rv.KetQua == "Khỏi")
                        moi.ket_qua_dtri = "1";
                    else if (rv.KetQua == "Đỡ|Giảm")
                        moi.ket_qua_dtri = "2";
                    else if (rv.KetQua == "Không T.đổi")
                        moi.ket_qua_dtri = "3";
                    else if (rv.KetQua == "Nặng hơn")
                        moi.ket_qua_dtri = "4";
                    else
                        moi.ket_qua_dtri = "5";
                    if (rv.Status == null || rv.Status == 2)
                        moi.tinh_trang_rv = "1";
                    else if (rv.Status == 1)
                        moi.tinh_trang_rv = "2";
                    else
                        moi.tinh_trang_rv = rv.Status.Value.ToString();
                    moi.ngay_ttoan = vp.NgayTT.Value.ToString("yyyyMMdd");
                    if (bn.NoiTru == null || bn.Tuyen == null)
                        moi.muc_huong = "0";
                    else
                    {
                        int hangbv = DungChung.Ham.hangBV(QLBV.DungChung.Bien.MaBV);
                        moi.muc_huong = _getmuc(_listmuc, hangbv, bn.SThe, bn.Tuyen.Value, bn.NoiTru.Value, vp.NgayTT.Value).ToString();
                    }
                    double tienthuoc, tienvtyt, tongtien, t_bntt, t_bhtt, t_ngoaids;
                    tienthuoc = qvpct.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).Sum(p => p.ThanhTien);// làm tròn trước nếu cần thiết
                    tienvtyt = qvpct.Where(p => p.IDNhom == 7 || p.IDNhom == 10 || p.IDNhom == 11).Sum(p => p.ThanhTien);// làm tròn trước nếu cần thiết
                    tongtien = qvpct.Sum(p => p.ThanhTien);// làm tròn trước nếu cần thiết
                    t_bntt = qvpct.Sum(p => p.TienBN);// làm tròn trước nếu cần thiết
                    t_bhtt = qvpct.Sum(p => p.TienBH);// làm tròn trước nếu cần thiết
                    t_ngoaids = qvpct.Where(p => p.TenNhomCT == "Vận chuyển").Sum(p => p.ThanhTien);// chưa làm tròn
                    moi.t_thuoc = tienthuoc.ToString();
                    moi.t_vtyt = tienvtyt.ToString();
                    moi.T_tongchi = tongtien.ToString();
                    moi.t_bhtt = t_bhtt.ToString();
                    moi.t_bntt = t_bntt.ToString();
                    moi.t_nguonkhac = "0";
                    moi.t_ngoaids = t_ngoaids.ToString();
                    moi.nam_qt = vp.NgayTT.Value.Year.ToString();
                    moi.thang_qt = vp.NgayTT.Value.Month.ToString("D2");
                    if (bn.NoiTru == 1)
                        moi.ma_loai_kcb = "3";
                    else if (bn.DTNT == true)
                        moi.ma_loai_kcb = "2";
                    else
                        moi.ma_loai_kcb = "1";
                    moi.ma_khoa = rv.MaKP.ToString();
                    moi.ma_cskcb = QLBV.DungChung.Bien.MaBV;
                    moi.ma_khuvuc = bn.KhuVuc;
                    moi.ma_pttt_qt = "";// ??????
                    moi.can_nang = "";//?????????           
                }
                return moi;
            }

            #endregion
            #region chi_tiet_thuoc
            else if (bang == 4)
            {
                stt = 0;
                List<chi_tiet_thuoc> listThuoc = new List<chi_tiet_thuoc>();
                var qvpct = (from vphi in data.VienPhis.Where(p => p.MaBNhan == maBN)
                             join vpct in data.VienPhicts on vphi.idVPhi equals vpct.idVPhi
                             join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             join n in data.NhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6) on tn.IDNhom equals n.IDNhom
                             select new
                             {
                                 vpct.MaDV,
                                 vpct.SoLuong,
                                 vpct.DonGia,
                                 vpct.ThanhTien,
                                 vpct.TienBH,
                                 vpct.TienBN,
                                 vpct.TrongBH,
                                 vpct.MaKP,
                                 tn.IdTieuNhom,
                                 n.IDNhom,
                                 n.TenNhomCT,
                                 dv.MaQD,
                                 dv.SoQD,
                                 dv.TenHC,
                                 dv.DonVi,
                                 dv.HamLuong,
                                 dv.MaDuongDung,
                                 dv.SoDK,
                             }).ToList();
                //List<ErrMs> q = ValidateTonghop(bn, rv, vp);//validate?????????
                _listErrMs.AddRange(q);
                if (bn != null && rv != null && vp != null && q.Count == 0)//kiểm tra các trường dữ liệu 
                {
                    foreach (var a in qvpct)
                    {
                        chi_tiet_thuoc moi = new chi_tiet_thuoc();
                        stt++;
                        moi.ma_lk = bn.Ma_lk.ToString();
                        moi.stt = stt.ToString();
                        moi.ma_thuoc = a.MaQD;
                        moi.ma_nhom = a.IDNhom.ToString();
                        moi.ten_thuoc = a.TenHC; //????? Tên ghi đúng theo danh mục thuốc cục qld công bố
                        moi.don_vi_tinh = a.DonVi;
                        moi.ham_luong = a.HamLuong;
                        moi.duong_dung = a.MaDuongDung;
                        moi.lieu_dung = "";//?????????????????????????
                        moi.so_dang_ky = a.SoDK;
                        moi.so_luong = a.SoLuong.ToString();
                        moi.don_gia = a.DonGia.ToString();
                        object tylett = data.DichVus.Where(p => p.MaDV == a.MaDV).Select(p => p.BHTT).FirstOrDefault();
                        if (tylett == null)
                            moi.tyle_tt = "0";
                        else
                            moi.tyle_tt = tylett.ToString();
                        moi.thanh_tien = a.ThanhTien.ToString();
                        moi.ma_khoa = getMaKhoa_QD(a.MaKP);// mã kpQD
                        moi.ma_bac_si = getMaBacSy(bn.MaBNhan, a.MaDV, a.MaKP);
                        moi.ma_benh = rv.MaICD;
                        moi.ma_pttt = "1";// Chưa có dữ liệu để lấy nên fix cứng
                        listThuoc.Add(moi);
                    }
                }
                return listThuoc;
            }
            #endregion
            #region chi_tiet_dvkt

            #endregion
            #region chi_tiet_dien_bien_benh
            #endregion
            #region chi_tiet_cls
            #endregion
            else return null;
        }
        #endregion
        #region các hàm phụ trợ
        private string GetTenCanBo(QLBV_Database.QLBVEntities data, string maCB)
        {
            string tenCB = "";
            var q = data.CanBoes.Where(p => p.MaCB == maCB).FirstOrDefault();
            if (q != null)
                tenCB = q.TenCB;
            return tenCB;

        }
        public void CreateExErrFile(List<ErrMs> lstErr, string path)
        {
            string _fileExcel = path + "\\" + "Loi_" + DateTime.Now.ToString("yyyyMMddhhmm") + ".xls";
            string[] _arr = new string[] { "@", "@", "@", "@", "@" };
            string[] _tieude = { "stt", "Tên trường", "Bảng", "ID", "Thông điệp lỗi" };

            int[] _arrWidth = new int[] { };
            DungChung.Bien.MangHaiChieu = new Object[lstErr.Count + 1, 5];
            for (int i = 0; i < 5; i++)
            {
                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
            }
            int num = 1;
            foreach (var r in _listErrMs)
            {
                DungChung.Bien.MangHaiChieu[num, 0] = num;
                DungChung.Bien.MangHaiChieu[num, 1] = r.ColName;
                DungChung.Bien.MangHaiChieu[num, 2] = r.Table;
                DungChung.Bien.MangHaiChieu[num, 3] = r.ID;
                DungChung.Bien.MangHaiChieu[num, 4] = r.Ms;
                num++;
            }

            QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Error", _fileExcel, true);
        }
        /// <summary>
        /// Lấy ra tuyến bệnh viện thông qua mã BV
        /// </summary>
        /// <param name="data"></param>
        /// <param name="maBV"></param>
        /// <returns></returns>
        private string getTuyen(QLBV_Database.QLBVEntities data, string maBV)
        {
            string tenTuyen = "";
            var q = data.BenhViens.Where(p => p.MaBV == maBV).Select(p => p.TuyenBV).FirstOrDefault();
            if (q != null)
                tenTuyen = q;
            return tenTuyen;
        }
        public string getNgaySinh(string ngaysinh, string thangsinh, string namsinh)
        {
            string rs = "";

            int ot;
            int ngs = 0, ths = 0, ns = 0;
            if (Int32.TryParse(ngaysinh, out ot))
                ngs = Convert.ToInt32(ngaysinh);
            if (Int32.TryParse(thangsinh, out ot))
                ths = Convert.ToInt32(thangsinh);
            if (Int32.TryParse(namsinh, out ot))
                ns = Convert.ToInt32(namsinh);
            if (namsinh.Trim().Length == 4)
            {
                if (ngs >= 1 && ngs <= 31 && ths >= 1 && ths <= 12)
                {
                    rs = ns.ToString() + ths.ToString("D2") + ngs.ToString("D2");
                }
                else
                    rs = ns.ToString()+"0101";
            }
            return rs;

        }
        /// <summary>
        /// Lấy tên bệnh viện qua mã bệnh viện
        /// </summary>
        /// <param name="maBV"></param>
        /// <returns></returns>
        private string getTenBVByMaBV(QLBV_Database.QLBVEntities data, string maBV)
        {
            string tenbv = "";
            tenbv = data.BenhViens.Where(p => p.MaBV == maBV).Select(p => p.TenBV).FirstOrDefault();
            return tenbv;
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
        public static double _getmuc(List<MucTT> _qmuc, int hangBV, string mathe, int tuyen, int noingoaitru, DateTime ngayTT)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            double _muctt = 0;
            string mamuc = "";

            if (mathe.Length > 2 && ngayTT != null)
            {
                mamuc = mathe.Substring(2, 1);
                var qmuc = _qmuc.Where(p => p.MaMuc == mamuc).ToList();
                if (qmuc.Count > 0 && qmuc.First().PTTT != null)
                {

                    // var q = data.MucTTs.(p => p.MaMuc == mamuc).PTTT;
                    if (tuyen == 1) // đúng tuyến
                    {
                        _muctt = Convert.ToDouble(qmuc.First().PTTT.ToString());
                    }
                    else // trái tuyến
                    {
                        double tylevuottuyen = 0;

                        if (noingoaitru == 0)
                        {
                            if (ngayTT >= new DateTime(2015, 1, 1) && ngayTT < new DateTime(2016, 1, 1))
                                switch (hangBV)
                                {
                                    case 3:
                                        tylevuottuyen = 0.7;
                                        break;
                                }
                            else if (ngayTT >= new DateTime(2016, 1, 1))
                            {
                                if (hangBV == 4 || hangBV == 3)
                                    tylevuottuyen = 1;
                            }
                        }
                        else if (noingoaitru == 1) // nội trú
                        {
                            if (ngayTT >= new DateTime(2015, 1, 1) && ngayTT < new DateTime(2016, 1, 1))
                                switch (hangBV)
                                {
                                    case 3:
                                        tylevuottuyen = 0.7;
                                        break;
                                    case 2:
                                        tylevuottuyen = 0.6;
                                        break;
                                    case 1:
                                        tylevuottuyen = 0.4;
                                        break;
                                }
                            else if (ngayTT >= new DateTime(2016, 1, 1) && ngayTT < new DateTime(2021, 1, 1))
                                switch (hangBV)
                                {
                                    case 4:
                                        tylevuottuyen = 1;
                                        break;
                                    case 3:
                                        tylevuottuyen = 1;
                                        break;
                                    case 2:
                                        tylevuottuyen = 0.6;
                                        break;
                                    case 1:
                                        tylevuottuyen = 0.4;
                                        break;
                                }
                            else if (ngayTT >= new DateTime(2021, 1, 1))
                            {
                                switch (hangBV)
                                {
                                    case 4:
                                        tylevuottuyen = 1;
                                        break;
                                    case 3:
                                        tylevuottuyen = 1;
                                        break;
                                    case 2:
                                        tylevuottuyen = 1;
                                        break;
                                    case 1:
                                        tylevuottuyen = 0.4;
                                        break;
                                }
                            }
                        }
                        _muctt = Convert.ToDouble(qmuc.First().PTTT) * tylevuottuyen;
                    }
                }
            }
            return _muctt;
        }
   
        private string getMaKhoa_QD(int? maKP)
        {
            if (maKP == null)
                return "";
            else
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var kp = data.KPhongs.Where(p => p.MaKP == maKP).Select(p => p.MaQD).FirstOrDefault();
                if (kp == null)
                    return "";
                else return kp;
            }
        }
        private string getMaDvQD(int? MaDV)
        {
            if (MaDV == null)
            {
                return "";
            }
            else
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var q = data.DichVus.Where(p => p.MaDV == MaDV).Select(p => p.MaQD).FirstOrDefault();
                return q;
            }
        }
        private string[] getNgayYLenh_KetQua(int maBN, int? maDV, int? maKP)
        {
            string[] rs = new string[] { "", "" };

            if (maKP != null && maDV != null)
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var qcls = (from cls in data.CLS.Where(p => p.MaBNhan == maBN)
                            join cd in data.ChiDinhs.Where(p => p.MaDV == maDV) on cls.IdCLS equals cd.IdCLS
                            select new { cls.NgayThang, cls.NgayTH }).FirstOrDefault();
                if (qcls != null)
                {

                    if (qcls.NgayThang != null)

                        rs[0] = qcls.NgayThang.Value.ToString("yyyyMMddhhmm");
                    if (qcls.NgayTH != null)
                        rs[1] = qcls.NgayTH.Value.ToString("yyyyMMddhhmm");
                }
            }
            return rs;
        }
        /// <summary>
        /// Lấy ra mã bác sỹ kê đơn
        /// </summary>
        /// <param name="maBN"></param>
        /// <param name="maDV">mã dịch vụ trong bảng vpct</param>
        /// <param name="maKP">Mã khoa phòng trong bảng vpct</param>
        /// <returns></returns>
        private string getMaBacSy(int maBN, int? maDV, int? maKP)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qdtct = (from dt in data.DThuocs.Where(p => p.MaBNhan == maBN)
                         join dtct in data.DThuoccts.Where(p => p.MaKP == maKP) on dt.IDDon equals dtct.IDDon
                         join cb in data.CanBoes on dtct.MaCB equals cb.MaCB
                         select new { cb.MaCB }).FirstOrDefault();// chỗ này cần lấy theo mã số chứng chỉ hành nghề
            if (qdtct == null)
                return "";
            else
                return qdtct.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bang">1: checkin, 2:phieuchuyentuyen, 3:tonghop, 4:chi_tiet_thuoc, 5:chi_tiet_dvkt, 6:chi_tiet_dien_bien_benh, 7:chi_tiet_cls</param>
        /// <param name="data"></param>
        /// <param name="maBN"></param>
        /// <returns></returns>
        #endregion

        //*********---------------------------------------------------------------------------------------------------------
        #region validate
        public List<ErrMs> Validatecheckin(BenhNhan bn, VaoVien vv, RaVien rv)
        {
            List<ErrMs> _listErr = new List<ErrMs>();
            ErrMs moi = new ErrMs();
            if (bn != null)
            {
                //kiểm tra mã cơ sở KCB ban đầu
                if (bn.MaCS == null)
                {
                    moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "BenhNhan";
                    moi.ColName = "MaCS";
                    moi.Ms = "Bệnh nhân chưa có mã cơ sở đăng ký KCB ban đầu";
                    _listErr.Add(moi);
                }
                // kiểm tra năm sinh
                if (bn.NamSinh == null)
                {
                    moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "BenhNhan";
                    moi.ColName = "NamSinh";
                    moi.Ms = "NamSinh null";
                    _listErr.Add(moi);
                }
                // kiểm tra giới tính
                if (bn.GTinh == null)
                {
                    moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "BenhNhan";
                    moi.ColName = "GTinh";
                    moi.Ms = "GTinh null";
                    _listErr.Add(moi);
                }
                //kiểm tra địa chỉ
                if (String.IsNullOrEmpty(bn.DChi))
                {
                    moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "BenhNhan";
                    moi.ColName = "DChi";
                    moi.Ms = "DChi null hoặc rỗng";
                    _listErr.Add(moi);
                }

                #region kiểm tra ngày vào tạm bỏ (lấy theo ngày nhập)
                //if (bn.NoiTru == 0)
                //{
                //    if (rv != null && rv.NgayVao == null)
                //    {
                //        moi = new ErrMs();
                //        moi.ID = bn.MaBNhan.ToString();
                //        moi.Table = "RaVien";
                //        moi.ColName = "NgayVao";
                //        moi.Ms = "Bệnh nhân không có ngày vào viện";
                //        _listErr.Add(moi);
                //    }
                //}
                //else // nội trú
                //{
                //    if (vv != null && vv.NgayVao == null)
                //    {
                //        moi = new ErrMs();
                //        moi.ID = bn.MaBNhan.ToString();
                //        moi.Table = "VaoVien";
                //        moi.ColName = "NgayVao";
                //        moi.Ms = "Bệnh nhân không có ngày vào viện";
                //        _listErr.Add(moi);
                //    }
                //}

                #endregion
            }
            return _listErr;

        }
        private List<ErrMs> ValidateChuyenTuyen(BenhNhan bn, RaVien rv)
        {
            List<ErrMs> listErr = new List<ErrMs>();
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (bn != null && rv != null)
            {
                if (rv.MaBVC == null)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "RaVien";
                    moi.ColName = "MaBVC";
                    moi.Ms = "Bệnh nhân chưa có mã bệnh viện chuyển đến";
                    listErr.Add(moi);
                }
                else if (rv.MaBVC.Length != 5)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "RaVien";
                    moi.ColName = "MaBVC";
                    moi.Ms = "Mã bệnh viện chuyển phải là 5 ký tự";
                    listErr.Add(moi);
                }
                else if (String.IsNullOrEmpty(getTenBVByMaBV(data, rv.MaBVC)))
                {

                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "RaVien";
                    moi.ColName = "MaBVC";
                    moi.Ms = "Không tìm thấy tên bệnh viện chuyển đến theo mã bệnh viện chuyển";
                    listErr.Add(moi);
                }

                if (rv.NgayVao == null)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "RaVien";
                    moi.ColName = "NgayVao";
                    moi.Ms = "Bệnh nhân chưa ngày vào";
                    listErr.Add(moi);
                }
                if (rv.NgayRa == null)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "RaVien";
                    moi.ColName = "NgayRa";
                    moi.Ms = "Bệnh nhân chưa ngày ra";
                    listErr.Add(moi);
                }
                if (String.IsNullOrEmpty(bn.DChi))
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "BenhNhan";
                    moi.ColName = "DChi";
                    moi.Ms = "Địa chỉ bệnh nhân bị trống";
                    listErr.Add(moi);
                }
                if (String.IsNullOrEmpty(bn.SThe) || bn.SThe.Length != 15)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "BenhNhan";
                    moi.ColName = "SThe";
                    moi.Ms = "Số thẻ không hợp lệ";
                    listErr.Add(moi);
                }
                if (bn.HanBHTu == null)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "BenhNhan";
                    moi.ColName = "HanBHTu";
                    moi.Ms = "Giới hạn thẻ từ bằng null";
                    listErr.Add(moi);
                }
                if (bn.HanBHDen == null)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "BenhNhan";
                    moi.ColName = "HanBHDen";
                    moi.Ms = "Giới hạn thẻ đến bằng null";
                    listErr.Add(moi);
                }
                if (rv.LyDoC == null)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "RaVien";
                    moi.ColName = "LyDoC";
                    moi.Ms = "Lý do chuyển tuyến bằng null";
                    listErr.Add(moi);
                }

            }
            return listErr;
        }
        private List<ErrMs> ValidateTonghop(BenhNhan bn, RaVien rv, VienPhi vp)
        {
            List<ErrMs> listErr = new List<ErrMs>();
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (bn != null && rv != null && vp != null)
            {
                if (String.IsNullOrEmpty(bn.DChi))
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "BenhNhan";
                    moi.ColName = "DChi";
                    moi.Ms = "Bệnh nhân chưa có địa chỉ";
                    listErr.Add(moi);
                }
                else if (String.IsNullOrEmpty(bn.MaCS))
                {

                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "BenhNhan";
                    moi.ColName = "MaCS";
                    moi.Ms = "Chưa có mã cơ sở đăng ký ban đầu";
                    listErr.Add(moi);
                }


                if (String.IsNullOrEmpty(bn.SThe) || bn.SThe.Length != 15)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "BenhNhan";
                    moi.ColName = "SThe";
                    moi.Ms = "Số thẻ không hợp lệ";
                    listErr.Add(moi);
                }
                if (bn.HanBHTu == null)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "BenhNhan";
                    moi.ColName = "HanBHTu";
                    moi.Ms = "Giới hạn thẻ từ bằng null";
                    listErr.Add(moi);
                }
                if (bn.HanBHDen == null)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "BenhNhan";
                    moi.ColName = "HanBHDen";
                    moi.Ms = "Giới hạn thẻ đến bằng null";
                    listErr.Add(moi);
                }
                if (rv.MaICD == null)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "RaVien";
                    moi.ColName = "MaICD";
                    moi.Ms = "Chưa có mã bệnh ICD";
                    listErr.Add(moi);
                }
                if (rv.NgayVao == null)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "RaVien";
                    moi.ColName = "NgayVao";
                    moi.Ms = "Ngày vào bằng null";
                    listErr.Add(moi);
                }
                if (rv.NgayRa == null)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "RaVien";
                    moi.ColName = "NgayRa";
                    moi.Ms = "Ngày ra viện bằng null";
                    listErr.Add(moi);
                }
                if (rv.SoNgaydt == null)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "RaVien";
                    moi.ColName = "SoNgaydt";
                    moi.Ms = "Số ngày điều trị bằng null";
                    listErr.Add(moi);
                }
                if (bn.Tuyen == null)
                {
                    ErrMs moi = new ErrMs();
                    moi.ID = bn.MaBNhan.ToString();
                    moi.Table = "BenhNhan";
                    moi.ColName = "Tuyen";
                    moi.Ms = "Tuyến bệnh viện bằng null";
                    listErr.Add(moi);
                }

            }
            return listErr;
        }
        #endregion

        /// <summary>
        /// Tạo file CheckIn
        /// </summary>
        /// <param name="data"></param>
        /// <param name="maBN"></param>
        /// <param name="path"></param>
        /// <param name="delete">0: Thêm, 1: sửa, 2: Xóa</param>
        /// <param name="bolXML">Xuất XML hoặc chỉ kiểm tra có đủ điều kiện xuất hay không</param>
        /// <param name="user">Tên đăng nhập</param>
        /// <param name="pass">pass để gửi dữ liệu</param>
        /// <returns></returns>
        public bool createCheckInFile(QLBV_Database.QLBVEntities data, int maBN, string path, int trangthai, bool bolXML, string user, string pass, int loai)
        {
            try { 
            string _fileExcel = path + "\\" + "Loi_" + DateTime.Now.ToString("yyyyMMddhhmm") + ".xls";
            BenhNhan bn = data.BenhNhans.Where(p => p.MaBNhan == maBN).Where(p => p.SThe != null && p.SThe.Length == 15).FirstOrDefault();
            VaoVien vv = data.VaoViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            RaVien rv = data.RaViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            TTboXung ttbs = data.TTboXungs.Where(p => p.MaBNhan == maBN).FirstOrDefault();

            checkin chIn = new checkin();
            bool rs = false;
            chIn = getBenhNhanVaoVien(data, bn, ttbs, vv, rv);


            if (chIn != null)
            {
                path = path + "\\" + DateTime.Now.ToString("yyyyMMddhhmm") + "_" + chIn.ma_the + "_" + "CheckIn.xml";
                var xEle = new XElement("DATA",
                              new XElement("HEADER",
                                  new XElement("MESSAGE_VERSION", "1.0"),
                                  new XElement("SENDER_CODE", "VSSOFT_" + DungChung.Bien.MaBV),
                                  new XElement("SENDER_NAME", DungChung.Bien.TenCQ),
                                  new XElement("TRANSACTION_TYPE", "M0001"),
                                  new XElement("TRANSACTION_NAME", "Web Service"),
                                  new XElement("TRANSACTION_DATE", DateTime.Now.ToString("yyyy-MM-dd")),
                                  new XElement("TRANSACTION_ID", ""),
                                  new XElement("REQUEST_ID", ""),
                                   new XElement("ACTION_TYPE", trangthai)
                                  ),
                                  new XElement("BODY",
                              new XElement("CHECKIN",

                                              new XElement("MA_LK", chIn.ma_lk),
                                              new XElement("MABENHVIEN", chIn.mabenhvien),
                                              new XElement("MA_THE", chIn.ma_the),
                                              new XElement("MA_KCBBD", chIn.ma_kcbbd),
                                              new XElement("HO_TEN",  chIn.ho_ten == null ? "" : chIn.ho_ten),
                                              new XElement("NGAY_SINH", chIn.ngay_sinh),
                                              new XElement("NAM_SINH", chIn.nam_sinh),
                                              new XElement("GIOI_TINH", chIn.gioi_tinh),
                                              new XElement("DIA_CHI", chIn.dia_chi == null ? "" : chIn.dia_chi),
                                              new XElement("TU_NGAY", chIn.tu_ngay),
                                              new XElement("DEN_NGAY", chIn.den_ngay),
                                             // new XElement("MATINHQUANHUYEN","01"),
                                             new XElement("MATINHQUANHUYEN", chIn.matinhquanhuyen),
                                              new XElement("NGAYGIOVAO", chIn.ngaygiovao),
                                              new XElement("NGAYDU5NAM", chIn.ngaydu5nam),
                                              new XElement("MA_NOICHUYENDEN", chIn.ma_noichuyenden),
                                              new XElement("LYDODENKHAM", String.IsNullOrEmpty(chIn.lydodenkham)? "1": chIn.lydodenkham),
                                              new XElement("TINHTRANGVAOVIEN", chIn.tinhtrangvaovien),
                                              new XElement("SOKHAMBENH", chIn.sokhambenh),
                                              new XElement("SODIENTHOAI_LH", chIn.sodienthoai_lh),
                                              new XElement("NGUOILIENHE",chIn.nguoilienhe == null ? "" : chIn.nguoilienhe),
                                              new XElement("MA_KHUVUC",CutString(chIn.ma_khuvuc,2))

                                          )));
                xEle.Save(path);
               string[] error= fn_PostData(xEle, 0, user, pass,loai);
                
               if (Convert.ToInt32(error[0]) == 0)
                   rs = true;
               else
                   rs = false;
            }
            if (_listErrMs.Count > 0)
            {
                string[] _arr = new string[] { "@", "@", "@", "@", "@" };
                string[] _tieude = { "stt", "Tên trường", "Bảng", "ID", "Thông điệp lỗi" };

                int[] _arrWidth = new int[] { };
                DungChung.Bien.MangHaiChieu = new Object[_listErrMs.Count + 1, 5];
                for (int i = 0; i < 5; i++)
                {
                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                }
                int num = 1;
                foreach (var r in _listErrMs)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.ColName;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.Table;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.ID;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.Ms;
                    num++;
                }

                QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "123", _fileExcel, true);
            }
            return rs;
            }
            catch
            {
                return false;
            }
        }
        private string CutString(string kq, int so)
        {
            if (kq != null)
                if (kq.Length > 50)
                {
                    return kq.Substring(0, 50);
                }
                else
                    return kq;
            else return "";
        }

        /// <summary>
        /// Tạo file CheckOut
        /// </summary>
        /// <param name="data"></param>
        /// <param name="maBN"></param>
        /// <param name="path"></param>
        /// <param name="trangthai">0: thêm; 1: sửa; 2: xóa</param>
        /// <param name="bolXML">Xuất XML hoặc chỉ kiểm tra có đủ điều kiện xuất hay không</param>
        /// <returns></returns>
        public bool createCheckOutFile(QLBV_Database.QLBVEntities data, int maBN, string path, int trangthai, bool bolXML, string user, string pass, int loai)
        {
            try { 
            bool rs = false;
            BenhNhan bn = (from bnhan in data.BenhNhans.Where(p => p.MaBNhan == maBN) join rvien in data.RaViens on bnhan.MaBNhan equals rvien.MaBNhan select bnhan).Where(p => p.SThe != null && p.SThe.Length == 15).FirstOrDefault();
            RaVien rv = data.RaViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            TTboXung ttbs = data.TTboXungs.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            VaoVien vv = data.VaoViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            VienPhi vp = data.VienPhis.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            // var qbn = _listBenhNhan.Select(p => p.MaBNhan).ToList();
            phieuchuyentuyen qct = new phieuchuyentuyen();
            tonghop qtonghop = new tonghop();
            List<chi_tiet_thuoc> qctthuoc = new List<chi_tiet_thuoc>();
            List<chi_tiet_dvkt> qctdv = new List<chi_tiet_dvkt>();
            List<chi_tiet_cls> qcls = new List<chi_tiet_cls>();
            _listErrMs = new List<ErrMs>();
            List<chi_tiet_dien_bien_benh> qdienbien = new List<chi_tiet_dien_bien_benh>();
            if (rv.Status == 1)
                qct = getBenhNhanChuyenTuyen(data, bn, ttbs, vv, rv, vp);
            else
                qct = null;
            List<ErrMs> qerr = ValidateTonghop(bn, rv, vp);//validate?????????
            _listErrMs.AddRange(qerr);
            {
                qtonghop = gettonghop(data, bn, ttbs, vv, rv, vp, qerr.Count);
                qctthuoc = getChitietThuoc(data, bn, ttbs, vv, rv, vp, qerr.Count);
                qctdv = getChitietDvkt(data, bn, ttbs, vv, rv, vp, qerr.Count);
                qcls = getCLS(data, bn, ttbs, vv, rv, vp, qerr.Count);
                qdienbien = getDenBien(data, bn, ttbs, vv, rv, vp, qerr.Count);
            }
            #region  Tạo file XML
            if (qtonghop != null)
            {
                path = path + "\\" + DateTime.Now.ToString("yyyyMMddhhmm") + "_" + qtonghop.ma_the + "_" + "CheckOut.xml";
                try
                {
                    var xEle1 = new XElement("DATA");
                    xEle1.Add(new XElement("HEADER",
                        new XElement("MESSAGE_VERSION", "3.0"),
                                new XElement("SENDER_CODE", "VSSOFT_" + DungChung.Bien.MaBV),
                                new XElement("SENDER_NAME", DungChung.Bien.TenCQ),
                                new XElement("TRANSACTION_TYPE", "M0002"),
                                new XElement("TRANSACTION_NAME", "Web Service"),
                                new XElement("TRANSACTION_DATE", DateTime.Now.ToString("yyyy-MM-dd")),
                                new XElement("TRANSACTION_ID", ""),
                                new XElement("REQUEST_ID", ""),
                                 new XElement("ACTION_TYPE", trangthai)
                        ));
                    xEle1.Add(new XElement("BODY"));

                    xEle1.Element("BODY").Add(new XElement("CHECKOUT"));
                    xEle1.Element("BODY").Element("CHECKOUT").Add(new XElement("PHIEUCHUYENTUYEN"));
                    if (qct != null)
                    {
                        xEle1.Element("BODY").Element("CHECKOUT").Element("PHIEUCHUYENTUYEN").Add(
                             new XElement("MA_LK", qct.ma_lk),
                             new XElement("COQUANCHUQUAN","1"),// qct.coquanchuquan),
                             new XElement("MACSKCBDI", qct.macskcbdi),
                             new XElement("TENCSKCBDI", qct.tencskcbdi),
                             new XElement("MACSKCBDEN", qct.macskcbden),
                             new XElement("TENCSKCBDEN", qct.tencskcbden),
                             new XElement("SOHOSO", qct.sohoso),
                             new XElement("SOCHUYENTUYEN", qct.sochuyentuyen),
                             new XElement("SOGIAY", qct.sogiay),
                             new XElement("HO_TEN", qct.ho_ten == null ? "" : qct.ho_ten),
                             new XElement("GIOI_TINH", qct.gioi_tinh),
                             new XElement("NAM_SINH", getNgaySinh(bn.NgaySinh, bn.ThangSinh, bn.NamSinh)),
                             new XElement("DIA_CHI", qct.dia_chi == null ? "" : qct.dia_chi),
                             new XElement("DAN_TOC", qct.dan_toc),
                             new XElement("QUOC_TICH","1"), //qct.quoc_tich),
                             new XElement("NGHE_NGHIEP", qct.nghe_nghiep),
                             new XElement("NOI_LAMVIEC", qct.noi_lamviec),
                             new XElement("MA_THE", qct.ma_the),
                             new XElement("GT_THE_TU", qct.gt_the_tu),
                             new XElement("GT_THE_DEN", qct.gt_the_den),
                             new XElement("KHAMDIEUTRITAI",DungChung.Bien.MaBV ),//qct.khamdieutritai),
                             new XElement("KHAM_TUNGAY", qct.kham_tungay),
                             new XElement("KHAM_DENNGAY", qct.kham_denngay),
                             new XElement("TUYENTRUOC_CHUYENDEN", qct.tuyentruoc_chuyenden),
                             new XElement("NGAY_CHUYENDEN", qct.ngay_chuyenden),
                             new XElement("SO_CHUYENDEN", qct.so_chuyenden),
                             new XElement("DAUHIEU_LAMSANG", qct.dauhieu_lamsang),
                             new XElement("XETNGHIEM", qct.xetnghiem),
                             new XElement("CHUANDOAN", qct.chuandoan),
                             new XElement("PHUONGPHAP_DTRI", qct.phuongphap_dtri),
                             new XElement("TINHTRANGCHUYEN", qct.tinhtrangchuyen),
                             new XElement("LYDO_CHUYEN", qct.lydo_chuyen),
                             new XElement("HUONG_DTRI", qct.huong_dtri),
                             new XElement("NGAY_CHUYENTUYEN", qct.ngay_chuyentuyen),
                             new XElement("PHUONGTIEN_CHUYEN", qct.phuongtien_chuyen),
                             new XElement("THONGTIN_HOTONG", qct.thongtin_hotong),
                             new XElement("BACSY_DTRI", qct.bacsy_dtri),
                             new XElement("TENFILE", qct.tenfile == null ? "" : qct.tenfile),
                             new XElement("LOAIFILE", qct.loaifile),
                             new XElement("NOIDUNG_FILE", qct.noidung_file),
                             new XElement("TRANGTHAI", qct.trangthai)

                             );
                    }

                    //if (qct != null)
                    //{
                    //    xEle1.Element("Body").Element("CHECKOUT").Add(new XElement("PHIEUCHUYENTUYEN",
                    //         new XElement("MA_LK", qct.ma_lk),
                    //         new XElement("COQUANCHUQUAN", qct.coquanchuquan),
                    //         new XElement("MACSKCBDI", qct.macskcbdi),
                    //         new XElement("TENCSKCBDI", qct.tencskcbdi),
                    //         new XElement("MACSKCBDEN", qct.macskcbden),
                    //         new XElement("TENCSKCBDEN", qct.tencskcbden),
                    //         new XElement("SOHOSO", qct.sohoso),
                    //         new XElement("SOCHUYENTUYEN", qct.sochuyentuyen),
                    //         new XElement("SOGIAY", qct.sogiay),
                    //         new XElement("HO_TEN", new XCData(qct.ho_ten == null ? "" : qct.ho_ten)),
                    //         new XElement("GIOI_TINH", qct.gioi_tinh),
                    //         new XElement("NAM_SINH", qct.nam_sinh),
                    //         new XElement("DIA_CHI", new XCData(qct.dia_chi == null ? "" : qct.dia_chi)),
                    //         new XElement("DAN_TOC", qct.dan_toc),
                    //         new XElement("QUOC_TICH", qct.quoc_tich),
                    //         new XElement("NGHE_NGHIEP", qct.nghe_nghiep),
                    //         new XElement("NOI_LAMVIEC", qct.noi_lamviec),
                    //         new XElement("MA_THE", qct.ma_the),
                    //         new XElement("GT_THE_TU", qct.gt_the_tu),
                    //         new XElement("GT_THE_DEN", qct.gt_the_den),
                    //         new XElement("KHAMDIEUTRITAI", qct.khamdieutritai),
                    //         new XElement("KHAM_TUNGAY", qct.kham_tungay),
                    //         new XElement("KHAM_DENNGAY", qct.kham_denngay),
                    //         new XElement("TUYENTRUOC_CHUYENDEN", qct.tuyentruoc_chuyenden),
                    //         new XElement("NGAY_CHUYENDEN", qct.ngay_chuyenden),
                    //         new XElement("SO_CHUYENDEN", qct.so_chuyenden),
                    //         new XElement("DAUHIEU_LAMSANG", qct.dauhieu_lamsang),
                    //         new XElement("XETNGHIEM", qct.xetnghiem),
                    //         new XElement("CHUANDOAN", qct.chuandoan),
                    //         new XElement("PHUONGPHAP_DTRI", qct.phuongphap_dtri),
                    //         new XElement("TINHTRANGCHUYEN", qct.tinhtrangchuyen),
                    //         new XElement("LYDO_CHUYEN", qct.lydo_chuyen),
                    //         new XElement("HUONG_DTRI", qct.huong_dtri),
                    //         new XElement("NGAY_CHUYENTUYEN", qct.ngay_chuyentuyen),
                    //         new XElement("PHUONGTIEN_CHUYEN", qct.phuongtien_chuyen),
                    //         new XElement("THONGTIN_HOTONG", qct.thongtin_hotong),
                    //         new XElement("BACSY_DTRI", qct.bacsy_dtri),
                    //         new XElement("TENFILE", new XCData(qct.tenfile == null ? "" : qct.tenfile)),
                    //         new XElement("LOAIFILE", qct.loaifile),
                    //         new XElement("NOIDUNG_FILE", qct.noidung_file)//,
                    //        //  new XElement("trangthai", qct.trangthai)

                    //         ));
                    //}
                    xEle1.Element("BODY").Element("CHECKOUT").Add(new XElement("TONG_HOP",
                                           new XElement("MA_LK", qtonghop.ma_lk),
                                           new XElement("MA_CSKCB", qtonghop.ma_cskcb),
                                           new XElement("STT", qtonghop.stt),//--------------------------------------
                                           new XElement("MA_BN", qtonghop.ma_bn),//---------------------------------------
                                           new XElement("HO_TEN",qtonghop.ho_ten == null ? "" : qtonghop.ho_ten),
                                           new XElement("NGAY_SINH", qtonghop.ngay_sinh),
                                           new XElement("GIOI_TINH", qtonghop.gioi_tinh),
                                           new XElement("DIA_CHI", qtonghop.dia_chi == null ? "" : qtonghop.dia_chi),
                                           new XElement("MA_THE", qtonghop.ma_the),
                                           new XElement("MA_DKBD", qtonghop.ma_dkbd),
                                           new XElement("GT_THE_TU", qtonghop.gt_the_tu),
                                           new XElement("GT_THE_DEN", qtonghop.gt_the_den),
                                           new XElement("TEN_BENH", qtonghop.ten_benh == null ? "" : qtonghop.ten_benh),
                                           new XElement("MA_BENH", qtonghop.ma_benh),//--------------------------------------
                                           new XElement("MA_BENHKHAC", qtonghop.ma_benhkhac),//---------------------------------------
                                           new XElement("MA_LYDO_VVIEN", qtonghop.ma_lydo_vvien),
                                           new XElement("MA_NOI_CHUYEN", qtonghop.ma_noi_chuyen),
                                           new XElement("MA_TAI_NAN", qtonghop.ma_tai_nan),
                                           new XElement("NGAY_VAO", qtonghop.ngay_vao),
                                           new XElement("NGAY_RA", qtonghop.ngay_ra),
                                           new XElement("SO_NGAY_DTRI", qtonghop.so_ngay_dtri),
                                           new XElement("KET_QUA_DTRI", qtonghop.ket_qua_dtri),
                                           new XElement("TINH_TRANG_RV", qtonghop.tinh_trang_rv),
                                           new XElement("NGAY_TTOAN", qtonghop.ngay_ttoan),
                                           new XElement("MUC_HUONG", qtonghop.muc_huong),
                                           new XElement("T_THUOC", qtonghop.t_thuoc),//--------------------------------------
                                           new XElement("T_VTYT", qtonghop.t_vtyt),//---------------------------------------
                                           new XElement("T_TONGCHI", qtonghop.T_tongchi),
                                           new XElement("T_BNTT", qtonghop.t_bntt),
                                           new XElement("T_BHTT", qtonghop.t_bhtt),
                                           new XElement("T_NGUONKHAC", qtonghop.t_nguonkhac),
                                           new XElement("T_NGOAIDS", qtonghop.t_ngoaids),
                                           new XElement("NAM_QT", qtonghop.nam_qt),
                                           new XElement("THANG_QT", qtonghop.thang_qt),
                                           new XElement("MA_LOAI_KCB", qtonghop.ma_loai_kcb),
                                           new XElement("MA_KHOA", qtonghop.ma_khoa),
                                           //new XElement("MA_CSKCB", qtonghop.ma_cskcb),
                                           new XElement("MA_KHUVUC", "1"),//qtonghop.ma_khuvuc),
                                           new XElement("MA_PTTT_QT", qtonghop.ma_pttt_qt),//---------------------------------------
                                           new XElement("CAN_NANG", qtonghop.can_nang)));

                    xEle1.Element("BODY").Element("CHECKOUT").Add(new XElement("THONGTINCHITIET"));
                    if (qctthuoc != null)
                    {
                        xEle1.Element("BODY").Element("CHECKOUT").Element("THONGTINCHITIET").Add(new XElement("DSACH_CHI_TIET_THUOC",
                                         from item2 in qctthuoc
                                         select
                                                 new XElement("CHI_TIET_THUOC",
                                                      new XElement("MA_LK", item2.ma_lk),
                                                      new XElement("STT", item2.stt),
                                                      new XElement("MA_THUOC", item2.ma_thuoc),
                                                      new XElement("MA_NHOM", item2.ma_nhom),
                                                      new XElement("TEN_THUOC",item2.ten_thuoc == null ? "" : item2.ten_thuoc),
                                                      new XElement("DON_VI_TINH", item2.don_vi_tinh),
                                                      new XElement("HAM_LUONG", item2.ham_luong == null ? "null" : item2.ham_luong),
                                                      new XElement("DUONG_DUNG", item2.duong_dung == null? "null": item2.duong_dung),
                                                      new XElement("LIEU_DUNG", item2.lieu_dung == null ? "" : item2.lieu_dung),
                                                      new XElement("SO_DANG_KY", item2.so_dang_ky == null ? "null" : item2.so_dang_ky),
                                                      new XElement("SO_LUONG", item2.so_luong),
                                                      new XElement("DON_GIA", item2.don_gia),
                                                      new XElement("TYLE_TT", item2.tyle_tt),
                                                      new XElement("THANH_TIEN", item2.thanh_tien),
                                                      new XElement("MA_KHOA", item2.ma_khoa),
                                                      new XElement("MA_BAC_SI", item2.ma_bac_si),
                                                      new XElement("MA_BENH", item2.ma_benh),
                                                      new XElement("NGAY_YL", item2.ngay_yl),
                                                      new XElement("MA_PTTT", item2.ma_pttt)
                                                      )));
                    }
                    if (qctdv != null)
                    {
                        xEle1.Element("BODY").Element("CHECKOUT").Element("THONGTINCHITIET").Add(new XElement("DSACH_CHI_TIET_DVKT",
                                                     from item3 in qctdv
                                                     select
                                                new XElement("CHI_TIET_DVKT",
                                                      new XElement("MA_LK", item3.ma_lk),
                                                      new XElement("STT", item3.stt),
                                                      new XElement("MA_DICH_VU", item3.ma_dich_vu),
                                                      new XElement("MA_VAT_TU", item3.ma_vat_tu),
                                                      new XElement("MA_NHOM", item3.ma_nhom),
                                                      new XElement("TEN_DICH_VU", item3.ten_dich_vu == null ? "" : item3.ten_dich_vu),
                                                      new XElement("DON_VI_TINH", item3.don_vi_tinh),
                                                      new XElement("SO_LUONG", item3.so_luong),
                                                      new XElement("DON_GIA", item3.don_gia),
                                                      new XElement("TYLE_TT", item3.tyle_tt),
                                                      new XElement("THANH_TIEN", item3.thanh_tien),
                                                       new XElement("MA_KHOA", item3.ma_khoa),
                                                      new XElement("MA_BAC_SI", item3.ma_bac_si),
                                                      new XElement("MA_BENH", item3.ma_benh),
                                                      new XElement("NGAY_YL", item3.ngay_yl),
                                                      new XElement("NGAY_KQ", item3.ngay_kq),
                                                      new XElement("MA_PTTT", item3.ma_pttt)
                                                      )));
                    }
                    if (qcls != null)
                    {
                        xEle1.Element("BODY").Element("CHECKOUT").Element("THONGTINCHITIET").Add(new XElement("DSACH_CHI_TIET_CLS",
                                                  from item4 in qcls
                                                  select
                                                  new XElement("CHI_TIET_CLS",
                                                      new XElement("MA_LK", item4.ma_lk),
                                                      new XElement("STT", item4.stt),
                                                      new XElement("MA_DICH_VU", item4.ma_dich_vu),
                                                      new XElement("MA_CHI_SO", item4.ma_chi_so),
                                                      new XElement("TEN_CHI_SO", item4.ten_chi_so == null ? "" : item4.ten_chi_so),
                                                      new XElement("GIA_TRI", item4.gia_tri == null ? "" : item4.gia_tri),
                                                      new XElement("MA_MAY", item4.ma_may),
                                                      new XElement("MO_TA", item4.mo_ta == null ? "" : item4.mo_ta),
                                                      new XElement("KET_LUAN",item4.ket_luan == null ? "" : item4.ket_luan),
                                                      new XElement("NGAY_KQ", item4.ngay_kq)

                                                      )));// hết Ctcls);
                    }
                    if (qdienbien != null)
                    {
                        xEle1.Element("BODY").Element("CHECKOUT").Element("THONGTINCHITIET").Add(new XElement("DSACH_CHI_TIET_DIEN_BIEN_BENH",
                                                     from item5 in qdienbien
                                                     select
                                                         new XElement("CHI_TIET_DIEN_BIEN_BENH",
                                                         new XElement("MA_LK", ""),
                                                         new XElement("STT", ""),
                                                         new XElement("DIEN_BIEN", item5.dien_bien == null ? "" : item5.dien_bien),
                                                         new XElement("HOI_CHAN", item5.hoi_chan == null ? "" : item5.hoi_chan),
                                                         new XElement("PHAU_THUAT", item5.phau_thuat == null ? "" : item5.phau_thuat),
                                                         new XElement("NGAY_YL", "")

                                              )));
                    }
                    //xEle1.Save(path);
                    // rs = true;
                    #region old
                    //                var xEle = new XElement("checkout",
                    //                    new XElement("phieuchuyentuyen",
                    //                        new XElement("ma_lk", qct.ma_lk),
                    //                        new XElement("coquanchuquan", qct.coquanchuquan),
                    //                        new XElement("macskcbdi", qct.macskcbdi),
                    //                        new XElement("tencskcbdi", qct.tencskcbdi),
                    //                        new XElement("macskcbden", qct.macskcbden),
                    //                        new XElement("tencskcbden", qct.tencskcbden),
                    //                        new XElement("sohoso", qct.sohoso),
                    //                        new XElement("sochuyentuyen", qct.sochuyentuyen),
                    //                        new XElement("sogiay", qct.sogiay),
                    //                        new XElement("ho_ten", qct.ho_ten),
                    //                        new XElement("gioi_tinh", qct.gioi_tinh),
                    //                        new XElement("nam_sinh", qct.nam_sinh),
                    //                        new XElement("dia_chi", qct.dia_chi),
                    //                        new XElement("dan_toc", qct.dan_toc),
                    //                        new XElement("quoc_tich", qct.quoc_tich),
                    //                        new XElement("nghe_nghiep", qct.nghe_nghiep),
                    //                        new XElement("noi_lamviec", qct.noi_lamviec),
                    //                        new XElement("ma_the", qct.ma_the),
                    //                        new XElement("gt_the_tu", qct.gt_the_tu),
                    //                        new XElement("gt_the_den", qct.gt_the_den),
                    //                        new XElement("khamdieutritai", qct.khamdieutritai),
                    //                        new XElement("kham_tungay", qct.kham_tungay),
                    //                        new XElement("kham_denngay", qct.kham_denngay),
                    //                        new XElement("tuyentruoc_chuyenden", qct.tuyentruoc_chuyenden),
                    //                        new XElement("ngay_chuyenden", qct.ngay_chuyenden),
                    //                        new XElement("so_chuyenden", qct.so_chuyenden),
                    //                        new XElement("dauhieu_lamsang", qct.dauhieu_lamsang),
                    //                         new XElement("xetnghiem", qct.xetnghiem),
                    //                        new XElement("chuandoan", qct.chuandoan),
                    //                        new XElement("phuongphap_dtri", qct.phuongphap_dtri),
                    //                        new XElement("tinhtrangchuyen", qct.tinhtrangchuyen),
                    //                        new XElement("lydo_chuyen", qct.lydo_chuyen),
                    //                        new XElement("huong_dtri", qct.huong_dtri),
                    //                        new XElement("ngay_chuyentuyen", qct.ngay_chuyentuyen),
                    //                        new XElement("phuongtien_chuyen", qct.phuongtien_chuyen),
                    //                        new XElement("bacsy_dtri", qct.bacsy_dtri),
                    //                         new XElement("tenfile", qct.tenfile),
                    //                         new XElement("loaifile", qct.loaifile),
                    //                        new XElement("noidung_file", qct.noidung_file),
                    //                        new XElement("trangthai", qct.trangthai)
                    //                        ),  
                    //                       new XElement("tonghop",
                    //                       new XElement("ma_lk", qtonghop.ma_lk),
                    //                       new XElement("stt", qtonghop.stt),//--------------------------------------
                    //                       new XElement("ma_bn", qtonghop.ma_bn),//---------------------------------------
                    //                       new XElement("ho_ten", qtonghop.ho_ten),
                    //                       new XElement("ngay_sinh", qtonghop.ngay_sinh),
                    //                       new XElement("gioi_tinh", qtonghop.gioi_tinh),
                    //                       new XElement("dia_chi",qtonghop.dia_chi ),
                    //                       new XElement("ma_the", qtonghop.ma_the),
                    //                       new XElement("ma_dkbd", qtonghop.ma_dkbd),
                    //                       new XElement("gt_the_tu", qtonghop.gt_the_tu),
                    //                       new XElement("gt_the_den", qtonghop.gt_the_den),
                    //                       new XElement("ten_benh", qtonghop.ten_benh),
                    //                       new XElement("ma_benh", qtonghop.ma_benh),//--------------------------------------
                    //                       new XElement("ma_benhkhac", qtonghop.ma_benhkhac),//---------------------------------------
                    //                       new XElement("ma_lydo_vvien", qtonghop.ma_lydo_vvien),
                    //                       new XElement("ma_noi_chuyen", qtonghop.ma_noi_chuyen),
                    //                       new XElement("ma_tai_nan", qtonghop.ma_tai_nan),
                    //                       new XElement("ngay_vao",qtonghop.ngay_vao ),
                    //                       new XElement("ngay_ra", qtonghop.ngay_ra),
                    //                       new XElement("so_ngay_dtri", qtonghop.so_ngay_dtri),
                    //                       new XElement("ket_qua_dtri", qtonghop.ket_qua_dtri),
                    //                       new XElement("tinh_trang_rv", qtonghop.tinh_trang_rv),
                    //                       new XElement("muc_huong", qtonghop.ngay_ttoan),
                    //                       new XElement("t_thuoc", qtonghop.t_thuoc),//--------------------------------------
                    //                       new XElement("t_vtyt", qtonghop.t_vtyt),//---------------------------------------
                    //                       new XElement("t_tongchi", qtonghop.T_tongchi),
                    //                       new XElement("t_bhtt", qtonghop.t_bhtt),
                    //                       new XElement("t_nguonkhac", qtonghop.t_nguonkhac),
                    //                       new XElement("t_ngoaids",qtonghop.t_ngoaids ),
                    //                       new XElement("nam_qt", qtonghop.nam_qt),
                    //                       new XElement("thang_qt", qtonghop.thang_qt),
                    //                       new XElement("ma_loai_kcb", qtonghop.ma_loai_kcb),
                    //                       new XElement("ma_khoa", qtonghop.ma_khoa),
                    //                       new XElement("ma_cskcb", qtonghop.ma_cskcb),
                    //                       new XElement("ma_khuvuc", qtonghop.ma_khuvuc),
                    //                       new XElement("ma_pttt_qt", qtonghop.ma_pttt_qt),//---------------------------------------
                    //                       new XElement("can_nang", qtonghop.can_nang)


                    //                       ),//hết tổng hợp
                    //                  new XElement("thongtinchitiet",

                    //                        new XElement("dsach_chi_tiet_thuoc",
                    //                     from item2 in qctthuoc
                    //                     select
                    //                             new XElement("chi_tiet_thuoc",
                    //                                  new XElement("ma_lk",item2. ma_lk),
                    //                                  new XElement("stt", item2.stt),                                
                    //                                  new XElement("ma_thuoc", item2.ma_thuoc),
                    //                                  new XElement("ma_nhom", item2.ma_nhom),
                    //                                  new XElement("ten_thuoc", item2.ten_thuoc),
                    //                                  new XElement("don_vi_tinh", item2.don_vi_tinh),
                    //                                  new XElement("ham_luong", item2.ham_luong),
                    //                                  new XElement("duong_dung", item2.duong_dung),                                  
                    //                                  new XElement("lieu_dung", item2.lieu_dung),
                    //                                  new XElement("so_dang_ky", item2.so_dang_ky),
                    //                                  new XElement("so_luong", item2.so_luong),
                    //                                  new XElement("don_gia", item2.don_gia),
                    //                                  new XElement("tyle_tt", item2.tyle_tt),
                    //                                  new XElement("thanh_tien", item2.thanh_tien),

                    //                                  new XElement("ma_khoa",item2. ma_khoa),
                    //                                  new XElement("ma_bac_si", item2.ma_bac_si),                                
                    //                                  new XElement("ma_benh", item2.ma_benh),
                    //                                  new XElement("ngay_yl", item2.ngay_yl),
                    //                                  new XElement("ma_pttt", item2.ma_pttt)                                 
                    //                                  )),// hết CtThuoc
                    //                            new XElement("dsach_chi_tiet_dvkt",
                    //                                 from item3 in qctdv
                    //                                 select
                    //                            new XElement("chi_tiet_dvkt",                                 
                    //                                  new XElement("ma_lk", item3.ma_lk),
                    //                                  new XElement("stt", item3.stt),
                    //                                  new XElement("ma_dich_vu", item3.ma_dich_vu),                                 
                    //                                  new XElement("ma_vat_tu", item3.ma_vat_tu),
                    //                                  new XElement("ma_nhom", item3.ma_nhom),
                    //                                  new XElement("ten_dich_vu", item3.ten_dich_vu),
                    //                                  new XElement("don_vi_tinh", item3.don_vi_tinh),
                    //                                  new XElement("so_luong", item3.so_luong),
                    //                                  new XElement("don_gia", item3.don_gia),
                    //                                  new XElement("tyle_tt", item3.tyle_tt),                                 
                    //                                  new XElement("thanh_tien", item3.thanh_tien),
                    //                                   new XElement("ma_khoa", item3.ma_khoa),
                    //                                  new XElement("ma_bac_si", item3.ma_bac_si),
                    //                                  new XElement("ma_benh", item3.ma_benh),                                 
                    //                                  new XElement("ngay_yl", item3.ngay_yl),
                    //                                  new XElement("ngay_kq", item3.ngay_kq),
                    //                                  new XElement("ma_pttt", item3.ma_pttt)
                    //                                  )),// hết Ct DICH VU

                    //                          new XElement("dsach_chi_tiet_cls",
                    //                              from item4 in qcls
                    //                              select
                    //                              new XElement("chi_tiet_cls", 
                    //                                  new XElement("ma_lk", item4.ma_lk),
                    //                                  new XElement("stt", item4.stt),
                    //                                  new XElement("ma_dich_vu", item4.ma_dich_vu),                                 
                    //                                  new XElement("ma_chi_so", item4.ma_chi_so),
                    //                                  new XElement("ten_chi_so", item4.ten_chi_so),
                    //                                  new XElement("gia_tri", item4.gia_tri),
                    //                                  new XElement("ma_may", item4.ma_may),
                    //                                  new XElement("mo_ta", item4.mo_ta),
                    //                                  new XElement("ket_luan", item4.ket_luan),
                    //                                  new XElement("ngay_kq", item4.ngay_kq)                                 

                    //                                  )),// hết Ctcls

                    //                            new XElement("dsach_chi_tiet_dien_bien_benh",
                    //                                 from item5 in qdienbien
                    //                                 select
                    //                                     new XElement("chi_tiet_cls",
                    //                                     new XElement("ma_lk", ""),
                    //                                     new XElement("stt", ""),
                    //                                     new XElement("dien_bien", ""),
                    //                                     new XElement("hoi_chan", ""),
                    //                                     new XElement("phau_thuat", ""),
                    //                                     new XElement("ngay_yl", "")
                    //                                     )// hết DIỄN BIẾN BỆNH
                    //                          )));
                    #endregion

                    xEle1.Save(path);
                   string[] _err= fn_PostData(xEle1, 1, user, pass, loai);
                   if (_err[0]!=null && _err[0]=="0")
                    rs = true;
                    else
                   {
                       rs = false;
                   }
                }
                catch (Exception e)
                {
                    rs = false;
                }
            #endregion tạo file xml


            }
            if (_listErrMs.Count > 0)
            {
                try { 
                CreateExErrFile(_listErrMs, path);}catch(Exception){}

            }
            return rs;
            }
            catch
            {

                return false;
            }
        }
        #region đẩy dữ liệu lên server BYT


        /// <summary>
        /// 
        /// </summary>
        /// <param name="xEle"><Thẻ xml (định dạng theo XElement) - nội dung trong file xml/param>
        /// <param name="funtion">Hàm sử dụng : 0: gửi file checkIn; 1: gửi file checkOut</param>
        /// <param name="user">Tên tài khoản đăng nhập</param>
        /// <param name="pass">pass tài khoản đăng nhập chưa mã hóa</param>      
        /// <returns></returns>
        /// 

        private string[] fn_PostData(XElement xEle, int funtion, string user, string pass,int loai)
        {
            try { 
            _error = new string[] { "", "" };
            MD5 md5 = new MD5CryptoServiceProvider();
            // md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pass));
            md5.ComputeHash(Encoding.UTF8.GetBytes(pass));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }
           // string pasMD5 = strBuilder.ToString();
            string pasMD5 = pass;


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xEle.ToString());// as an example
            string rawXml = doc.OuterXml;
            string xmlStr = "";

            if (loai == 1) {
                if (funtion == 0)
                {
                    ServiceReference1.guiTTNVRequest a = new ServiceReference1.guiTTNVRequest(user, pasMD5, rawXml);
                    ServiceReference1.WSPortalClient asd = new ServiceReference1.WSPortalClient();
                    xmlStr = asd.guiTTNV(user, pasMD5, rawXml);
                }
                else
                {
                    ServiceReference1.guiTTNVRequest a = new ServiceReference1.guiTTNVRequest(user, pasMD5, rawXml);
                    ServiceReference1.WSPortalClient asd = new ServiceReference1.WSPortalClient();
                    xmlStr = asd.guiTTXV(user, pasMD5, rawXml);

                }

            }
            else
            {
                if (funtion == 0)
                {
                    vn.congdulieuyte.WSPortalService a = new vn.congdulieuyte.WSPortalService();
                    xmlStr = a.guiTTNV(user, pasMD5, rawXml);
                }
                else if (funtion == 1)
                {
                    vn.congdulieuyte.WSPortalService a = new vn.congdulieuyte.WSPortalService();
                    xmlStr = a.guiTTXV(user, pasMD5, rawXml);

                }
            }
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(xmlStr);

                XmlNode returnNode = xdoc.SelectSingleNode("/Data/Error/Error_Number");
                XmlNode returnNode2 = xdoc.SelectSingleNode("/Data/Error/Error_Message");

                if (returnNode != null)
                {
                    //if (returnNode.InnerText == "0")
                    //    rs = true;
                    _error[0] = returnNode.InnerText;
                }
                if (returnNode2 != null)
                {
                    //if (returnNode.InnerText == "0")
                    //    rs = true;
                    _error[1] = returnNode2.InnerText;
                }
                xdoc.Save("D:\\VSSOFT\\1234.xml");
            }
            catch (Exception ex)
            {
                return _error;
            }
            return _error;
            }
            catch (Exception)
            {
                return _error;
            }

        }

        #endregion
    }
}
