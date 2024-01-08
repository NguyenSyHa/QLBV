using QLBV_Database;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace QLBV.DungChung
{////Chú ý: khi làm phần chuyển tuyến cần kiểm tra trước status trong bảng ravien = 1
    class cls_KetNoi_BHXH
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
            private string Ma_noi_di;
            public string ma_noi_di
            {
                get { return Ma_noi_di; }
                set { Ma_noi_di = value; }
            }
            private string Ma_noi_den;
            public string ma_noi_den
            {
                get { return Ma_noi_den; }
                set { Ma_noi_den = value; }
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


            public string t_bncct { get; set; }

            public string mien_cung_ct { get; set; }
            //public string ngay_vao_noi_tru { get; set; }
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

            public double SoLuong { get; set; }

            public string muc_huong { get; set; }

            public string t_bhtt { get; set; }

            public string t_bncct { get; set; }

            public string t_nguonkhac { get; set; }

            public string t_bntt { get; set; }

            public string t_ngoaids { get; set; }

            public string tt_thau { get; set; }

            public string pham_vi { get; set; }
        }
        public class chi_tiet_dvkt
        {
            private int Ma_Nhom_5937 { get; set; }
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

            public string goi_VTYT { get; set; }

            public string ten_vat_tu { get; set; }

            public string t_tranTT { get; set; }

            public string muc_huong { get; set; }

            public string t_nguonkhac { get; set; }

            public string t_bntt { get; set; }

            public string t_bhtt { get; set; }

            public string t_bncct { get; set; }

            public string t_ngoaids { get; set; }

            public string ma_giuong { get; set; }

            public string tt_thau { get; set; }

            public string pham_vi { get; set; }
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
            private string Ten_benh;
            public string ten_benh
            {
                get { return Ten_benh; }
                set { Ten_benh = value; }
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

        string PID = "";

        public int stt = 0;
        #endregion
        #region lấy thông tin thẻ của từng bệnh nhân
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

            if (bn != null && (rv != null || vv != null))//kiểm tra các trường dữ liệu 
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
                    moi.matinhquanhuyen = bn.SThe.Substring(0, 4);
                    moi.ngaygiovao = vv == null ? rv.NgayVao.Value.ToString("yyyyMMddhhmm") : vv.NgayVao.Value.ToString("yyyyMMddhhmm");
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
                    return moi;
                }
                else return null;
            }
            else
                return null;

        }
        private XElement getBenhNhanChuyenTuyen(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp)
        {
            #region lấy ra object chuyển tuyến
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
                #region đổ vào xml
                var xEle1 = new XElement("PHIEUCHUYENTUYEN",
                           new XElement("MA_LK", moi.ma_lk),
                           new XElement("COQUANCHUQUAN", moi.coquanchuquan),
                           new XElement("MACSKCBDI", moi.macskcbdi),
                           new XElement("TENCSKCBDI", moi.tencskcbdi),
                           new XElement("MACSKCBDEN", moi.macskcbden),
                           new XElement("TENCSKCBDEN", moi.tencskcbden),
                           new XElement("SOHOSO", moi.sohoso),
                           new XElement("SOCHUYENTUYEN", moi.sochuyentuyen),
                           new XElement("SOGIAY", moi.sogiay),
                           new XElement("HO_TEN", new XCData(moi.ho_ten == null ? "" : moi.ho_ten)),
                           new XElement("GIOI_TINH", moi.gioi_tinh),
                           new XElement("NAM_SINH", moi.nam_sinh),
                           new XElement("DIA_CHI", new XCData(moi.dia_chi == null ? "" : moi.dia_chi)),
                           new XElement("DAN_TOC", moi.dan_toc),
                           new XElement("QUOC_TICH", moi.quoc_tich),
                           new XElement("NGHE_NGHIEP", moi.nghe_nghiep),
                           new XElement("NOI_LAMVIEC", moi.noi_lamviec),
                           new XElement("MA_THE", moi.ma_the),
                           new XElement("GT_THE_TU", moi.gt_the_tu),
                           new XElement("GT_THE_DEN", moi.gt_the_den),
                           new XElement("KHAMDIEUTRITAI", moi.khamdieutritai),
                           new XElement("KHAM_TUNGAY", moi.kham_tungay),
                           new XElement("KHAM_DENNGAY", moi.kham_denngay),
                           new XElement("TUYENTRUOC_CHUYENDEN", moi.tuyentruoc_chuyenden),
                           new XElement("NGAY_CHUYENDEN", moi.ngay_chuyenden),
                           new XElement("SO_CHUYENDEN", moi.so_chuyenden),
                           new XElement("DAUHIEU_LAMSANG", moi.dauhieu_lamsang),
                           new XElement("XETNGHIEM", moi.xetnghiem),
                           new XElement("CHUANDOAN", moi.chuandoan),
                           new XElement("PHUONGPHAP_DTRI", moi.phuongphap_dtri),
                           new XElement("TINHTRANGCHUYEN", moi.tinhtrangchuyen),
                           new XElement("LYDO_CHUYEN", moi.lydo_chuyen),
                           new XElement("HUONG_DTRI", moi.huong_dtri),
                           new XElement("NGAY_CHUYENTUYEN", moi.ngay_chuyentuyen),
                           new XElement("PHUONGTIEN_CHUYEN", moi.phuongtien_chuyen),
                           new XElement("THONGTIN_HOTONG", moi.thongtin_hotong),
                           new XElement("BACSY_DTRI", moi.bacsy_dtri),
                           new XElement("TENFILE", new XCData(moi.tenfile == null ? "" : moi.tenfile)),
                           new XElement("LOAIFILE", moi.loaifile),
                           new XElement("NOIDUNG_FILE", moi.noidung_file)//,


                           );
                return xEle1;
            }
            else
                return null;
            #endregion
            #endregion
        }
        private XElement gettonghop(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp)
        {
            try
            {
                #region lấy ra thông tin chung
                stt = 0;
                tonghop moi = new tonghop(); ;
                var q1 = (
                          from vpct in data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi && p.TrongBH == 1)
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
                #endregion
                #region get Object tonghop
                if (bn != null && rv != null && vp != null)//kiểm tra các trường dữ liệu 
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
                    moi.ma_lydo_vvien = bn.CapCuu == 1 ? "2" : (bn.Tuyen == 1 ? "1" : "3");

                    if (!string.IsNullOrEmpty(bn.MaBV))
                        moi.ma_noi_chuyen = bn.MaBV;
                    else
                        moi.ma_noi_chuyen = "";
                    moi.ma_tai_nan = "";// chưa có
                    moi.ngay_vao = rv.NgayVao.Value.ToString("yyyyMMddHHmm");// khác byt
                    moi.ngay_ra = rv.NgayRa.Value.ToString("yyyyMMddHHmm");// khác Byt
                    moi.so_ngay_dtri = rv.SoNgaydt.Value.ToString();
                    if (rv.KetQua == null)
                        moi.ket_qua_dtri = "2";
                    else if (rv.KetQua == "Khỏi")
                        moi.ket_qua_dtri = "1";
                    else if (rv.KetQua == "Đỡ|Giảm")
                        moi.ket_qua_dtri = "2";
                    else if (rv.KetQua == "Không T.đổi")
                        moi.ket_qua_dtri = "3";
                    else if (rv.KetQua == "Tử vong")
                        moi.ket_qua_dtri = "5";
                    else
                        moi.ket_qua_dtri = "4";
                    if (rv.Status == null || rv.Status == 2)// ra viện
                        moi.tinh_trang_rv = "1";
                    else if (rv.Status == 1)// chuyển viện
                        moi.tinh_trang_rv = "2";
                    else
                        moi.tinh_trang_rv = rv.Status.Value.ToString();// 3: trốn viện, 4: xin ra viện
                    moi.ngay_ttoan = vp.NgayTT.Value.ToString("yyyyMMddHHmm");// khác BYT
                    if (bn.NoiTru == null || bn.Tuyen == null)
                        moi.muc_huong = "0";
                    else
                    {
                        int hangbv = DungChung.Ham.hangBV(QLBV.DungChung.Bien.MaBV);
                        moi.muc_huong = _getmuc(hangbv, bn.SThe, bn.Tuyen.Value, bn.NoiTru.Value, vp.NgayTT.Value).ToString();
                    }
                    double tienthuoc, tienvtyt, tongtien, t_bntt, t_bhtt, t_ngoaids;
                    tienthuoc = qvpct.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).Sum(p => p.ThanhTien);// làm tròn sau khi tính tổng ( khi tạo file xml)
                    tienvtyt = qvpct.Where(p => p.IDNhom == 10 || p.IDNhom == 11).Sum(p => p.ThanhTien);// làm tròn sau khi tính tổng ( khi tạo file xml)
                    tongtien = qvpct.Sum(p => p.ThanhTien);/// làm tròn sau khi tính tổng ( khi tạo file xml)
                    t_bntt = qvpct.Sum(p => p.TienBN);// làm tròn sau khi tính tổng ( khi tạo file xml)
                    t_bhtt = qvpct.Sum(p => p.TienBH);// làm tròn sau khi tính tổng ( khi tạo file xml)
                    t_ngoaids = qvpct.Where(p => p.TenNhomCT == "Vận chuyển").Sum(p => p.ThanhTien);// chưa làm tròn
                    moi.t_thuoc = Math.Round(tienthuoc, 0).ToString();
                    moi.t_vtyt = Math.Round(tienvtyt, 0).ToString();
                    moi.T_tongchi = Math.Round(tongtien, 0).ToString();
                    moi.t_bhtt = Math.Round(t_bhtt, 0).ToString();
                    moi.t_bntt = Math.Round(t_bntt, 0).ToString();
                    moi.t_nguonkhac = "0";
                    moi.t_ngoaids = t_ngoaids.ToString();
                    moi.nam_qt = vp.NgayTT.Value.Year.ToString();
                    moi.thang_qt = vp.NgayTT.Value.Month.ToString("D2");
                    //if (bn.NoiTru == 1 && ((TimeSpan)(rv.NgayRa - rv.NgayVao)).TotalHours < 4)
                    //    moi.ma_loai_kcb = "9";
                    //else if (bn.NoiTru == 1)
                    //    moi.ma_loai_kcb = "3";
                    //else if (bn.DTNT == true)
                    //    moi.ma_loai_kcb = "2";
                    //else
                    //    moi.ma_loai_kcb = "1";
                    if (bn.NoiTru == 1)
                        moi.ma_loai_kcb = "3";
                    else if (bn.DTNT == true)
                        moi.ma_loai_kcb = "2";
                    else
                        moi.ma_loai_kcb = "1";
                    moi.ma_khoa = getMaKhoa_QD(rv.MaKP);
                    moi.ma_cskcb = bn.MaKCB;
                    moi.ma_khuvuc = bn.KhuVuc;
                    moi.ma_pttt_qt = "";// ??????
                    moi.can_nang = "";//?????????     
                    //moi.ngay_vao_noi_tru = bn.NoiTru == 1 ? vv.NgayVao.Value.ToString("yyyyMMddHHmm") : "";
                }
                else
                    moi = null;
                #endregion
                #region xuất xml
                if (moi != null)
                {
                    //string path = path + "\\XML1_" + DungChung.Bien.MaBV + "_" + moi.nam_qt + moi.thang_qt + "_" + moi.ma_lk + ".xml";
                    var xEle1 = new XElement("TONG_HOP",
                            new XElement("MA_LK", moi.ma_lk),
                                                   // new XElement("MA_CSKCB", moi.ma_cskcb),
                                                   new XElement("STT", moi.stt),//--------------------------------------
                                                   new XElement("MA_BN", moi.ma_bn),//---------------------------------------
                                                   new XElement("HO_TEN", new XCData(moi.ho_ten == null ? "" : moi.ho_ten)),
                                                   new XElement("NGAY_SINH", moi.ngay_sinh),
                                                   new XElement("GIOI_TINH", moi.gioi_tinh),
                                                   new XElement("DIA_CHI", new XCData(moi.dia_chi == null ? "" : moi.dia_chi)),
                                                   new XElement("MA_THE", moi.ma_the),
                                                   new XElement("MA_DKBD", moi.ma_dkbd),
                                                   new XElement("GT_THE_TU", moi.gt_the_tu),
                                                   new XElement("GT_THE_DEN", moi.gt_the_den),
                                                   new XElement("TEN_BENH", new XCData(moi.ten_benh == null ? "" : moi.ten_benh)),
                                                   new XElement("MA_BENH", moi.ma_benh),//--------------------------------------
                                                   new XElement("MA_BENHKHAC", moi.ma_benhkhac),//---------------------------------------
                                                   new XElement("MA_LYDO_VVIEN", moi.ma_lydo_vvien),
                                                   new XElement("MA_NOI_CHUYEN", moi.ma_noi_chuyen),
                                                   //new XElement("MA_NOI_DI", moi.ma_noi_di),
                                                   //new XElement("MA_NOI_DEN", moi.ma_noi_den),
                                                   new XElement("MA_TAI_NAN", moi.ma_tai_nan),
                                                   new XElement("NGAY_VAO", moi.ngay_vao),
                                                   new XElement("NGAY_RA", moi.ngay_ra),
                                                   new XElement("SO_NGAY_DTRI", moi.so_ngay_dtri),
                                                   new XElement("KET_QUA_DTRI", moi.ket_qua_dtri),
                                                   new XElement("TINH_TRANG_RV", moi.tinh_trang_rv),
                                                   new XElement("NGAY_TTOAN", moi.ngay_ttoan),
                                                   new XElement("MUC_HUONG", moi.muc_huong),
                                                   new XElement("T_THUOC", moi.t_thuoc),//--------------------------------------
                                                   new XElement("T_VTYT", moi.t_vtyt),//---------------------------------------
                                                   new XElement("T_TONGCHI", moi.T_tongchi),
                                                   new XElement("T_BNTT", moi.t_bntt),
                                                   new XElement("T_BHTT", moi.t_bhtt),
                                                   new XElement("T_NGUONKHAC", moi.t_nguonkhac),
                                                   new XElement("T_NGOAIDS", moi.t_ngoaids),
                                                   new XElement("NAM_QT", moi.nam_qt),
                                                   new XElement("THANG_QT", moi.thang_qt),
                                                   new XElement("MA_LOAI_KCB", moi.ma_loai_kcb),
                                                   new XElement("MA_KHOA", moi.ma_khoa),
                                                   new XElement("MA_CSKCB", moi.ma_cskcb),
                                                   new XElement("MA_KHUVUC", moi.ma_khuvuc),
                                                   new XElement("MA_PTTT_QT", moi.ma_pttt_qt),//---------------------------------------
                                                   new XElement("CAN_NANG", moi.can_nang));
                    //new XElement("NGAY_VAO_NOI_TRU", moi.ngay_vao_noi_tru));
                    return xEle1;
                }
                else return null;
                #endregion
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML1: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="bn"></param>
        /// <param name="ttbs"></param>
        /// <param name="vv"></param>
        /// <param name="rv"></param>
        /// <param name="vp"></param>
        /// <param name="lvpct">dùng  để lấy tỷ lệ BH Thanh toán</param>
        /// <returns></returns>
        private XElement gettonghop(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp, List<VienPhict> lvpct)
        {
            try
            {
                #region lấy ra thông tin chung
                stt = 0;
                tonghop moi = new tonghop(); ;
                var q1 = (
                          from vpct in data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi && p.TrongBH == 1)
                          select new
                          {
                              vpct.MaDV,
                              vpct.SoLuong,
                              vpct.DonGia,
                              vpct.ThanhTien,
                              vpct.TienBH,
                              vpct.TienBN,
                              vpct.TrongBH,
                              vpct.TyLeBHTT,
                              vpct.TyLeTT,
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
                                 SoLuong = Math.Round(vpct.SoLuong, 2),
                                 DonGia = Math.Round(vpct.DonGia, 2),
                                 ThanhTien = Math.Round(Math.Round(vpct.SoLuong, 2) * Math.Round(vpct.DonGia, 2) * (vpct.TyLeTT / 100), 2),
                                 vpct.TienBH,
                                 vpct.TienBN,
                                 vpct.TrongBH,
                                 dv.IdTieuNhom,
                                 dv.IDNhom,
                                 dv.TenNhomCT,
                                 vpct.TyLeBHTT,
                             }).ToList();
                double tyleBHTT = 0;
                tyleBHTT = (qvpct.First().TyLeBHTT) / 100;
                #endregion
                #region get Object tonghop
                if (bn != null && rv != null && vp != null)//kiểm tra các trường dữ liệu 
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
                    moi.ma_benhkhac = DungChung.Ham.FreshString(GetICD(rv.MaICD)[1]);
                    moi.ma_lydo_vvien = (bn.CapCuu == 1 ? "2" : (bn.Tuyen == 1 ? "1" : "3"));
                    if (!string.IsNullOrEmpty(bn.MaBV))
                        moi.ma_noi_chuyen = bn.MaBV;
                    else
                        moi.ma_noi_chuyen = "";
                    moi.ma_tai_nan = "";// chưa có
                    moi.ngay_vao = rv.NgayVao.Value.ToString("yyyyMMddHHmm");// khác byt
                    moi.ngay_ra = rv.NgayRa.Value.ToString("yyyyMMddHHmm");// khác Byt
                    moi.so_ngay_dtri = rv.SoNgaydt.Value.ToString();
                    if (rv.KetQua == null)
                        moi.ket_qua_dtri = "2";
                    else if (rv.KetQua == "Khỏi")
                        moi.ket_qua_dtri = "1";
                    else if (rv.KetQua == "Đỡ|Giảm")
                        moi.ket_qua_dtri = "2";
                    else if (rv.KetQua == "Không T.đổi")
                        moi.ket_qua_dtri = "3";
                    else if (rv.KetQua == "Tử vong")
                        moi.ket_qua_dtri = "5";
                    else
                        moi.ket_qua_dtri = "4";
                    if (rv.Status == null || rv.Status == 2)// ra viện
                        moi.tinh_trang_rv = "1";
                    else if (rv.Status == 1)// chuyển viện
                        moi.tinh_trang_rv = "2";
                    else
                        moi.tinh_trang_rv = rv.Status.Value.ToString();// 3: trốn viện, 4: xin ra viện
                    moi.ngay_ttoan = vp.NgayTT.Value.ToString("yyyyMMddHHmm");// khác BYT
                    if (bn.NoiTru == null || bn.Tuyen == null)
                        moi.muc_huong = "0";
                    else
                    {
                        var qtylett = lvpct.Where(p => p.TyLeBHTT > 0).ToList();
                        if (qtylett.Count() > 0)
                            moi.muc_huong = lvpct.First().TyLeBHTT.ToString();
                        else
                        {
                            int hangbv = DungChung.Ham.hangBV(QLBV.DungChung.Bien.MaBV);
                            moi.muc_huong = _getmuc(hangbv, bn.SThe, bn.Tuyen.Value, bn.NoiTru.Value, vp.NgayTT.Value).ToString();
                        }
                    }
                    double tienthuoc, tienvtyt, tongtien, t_bntt, t_bhtt, t_ngoaids;
                    tienthuoc = qvpct.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7).Sum(p => p.ThanhTien);// làm tròn sau khi tính tổng ( khi tạo file xml)
                    tienvtyt = qvpct.Where(p => p.IDNhom == 10 || p.IDNhom == 11).Sum(p => p.ThanhTien);// làm tròn sau khi tính tổng ( khi tạo file xml)
                    var tong = (from a in qvpct
                                group a by a.IDNhom into kq
                                select new
                                {
                                    thanhtien = Math.Round(kq.Sum(p => p.ThanhTien), 2)
                                }).ToList();

                    tongtien = tong.Sum(p => p.thanhtien);/// làm tròn sau khi tính tổng ( khi tạo file xml)


                    t_bhtt = Math.Round(tongtien * tyleBHTT, 0, MidpointRounding.AwayFromZero); ;// làm tròn sau khi tính tổng ( khi tạo file xml)
                    tongtien = Math.Round(tongtien, 0, MidpointRounding.AwayFromZero);
                    t_bntt = tongtien - t_bhtt;// làm tròn sau khi tính tổng ( khi tạo file xml)
                    var test = (from vpct in lvpct
                                join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                                join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                                select new
                                {
                                    vpct.ThanhTien,
                                    ndv.TenNhomCT,
                                    dv.TenDV
                                }).ToList();
                    var checkDV = test.Where(p => p.TenDV.Contains("Thận nhân tạo thường qui") || p.TenDV.Contains("Lọc màng bụng cấp cứu liên tục") || p.TenDV.Contains("Lọc màng bụng chu kỳ"));
                    //var checkICD = _listRaVien.Where(p => p.MaICD.Contains("C0") || p.MaICD.Contains("D0"));
                    if (rv.MaICD.Contains("C0") || rv.MaICD.Contains("D0"))
                    {
                        t_ngoaids = test.Sum(p => p.ThanhTien);
                    }
                    else if (checkDV != null && checkDV.Count() > 0)
                    {
                        t_ngoaids = test.Sum(p => p.ThanhTien);
                    }
                    else if ((bn.SThe.Substring(0, 2)) == "QN" || (bn.SThe.Substring(0, 2)) == "CA" || (bn.SThe.Substring(0, 2)) == "CY")
                    {
                        t_ngoaids = test.Sum(p => p.ThanhTien);
                    }
                    else
                        t_ngoaids = test.Where(p => p.TenNhomCT == "Vận chuyển").Sum(p => p.ThanhTien);

                    //t_ngoaids = qvpct.Where(p => p.TenNhomCT == "Vận chuyển").Sum(p => p.ThanhTien);// chưa làm tròn
                    moi.t_thuoc = Math.Round(tienthuoc, 2).ToString();
                    moi.t_vtyt = Math.Round(tienvtyt, 2).ToString();
                    moi.T_tongchi = tongtien.ToString();
                    moi.t_bhtt = t_bhtt.ToString();
                    moi.t_bntt = t_bntt.ToString();
                    moi.t_nguonkhac = "0";
                    moi.t_ngoaids = t_ngoaids.ToString();
                    moi.nam_qt = vp.NgayTT.Value.Year.ToString();
                    moi.thang_qt = vp.NgayTT.Value.Month.ToString("D2");
                    //if (bn.NoiTru == 1 && ((TimeSpan)(rv.NgayRa - rv.NgayVao)).TotalHours < 4)
                    //    moi.ma_loai_kcb = "9";
                    //else if (bn.NoiTru == 1)
                    //    moi.ma_loai_kcb = "3";
                    //else if (bn.DTNT == true)
                    //    moi.ma_loai_kcb = "2";
                    //else
                    //    moi.ma_loai_kcb = "1";
                    if (bn.NoiTru == 1)
                        moi.ma_loai_kcb = "3";
                    else if (bn.DTNT == true)
                        moi.ma_loai_kcb = "2";
                    else
                        moi.ma_loai_kcb = "1";
                    moi.ma_khoa = getMaKhoa_QD(rv.MaKP);
                    moi.ma_cskcb = bn.MaKCB;
                    moi.ma_khuvuc = bn.KhuVuc;
                    moi.ma_pttt_qt = "";// ??????
                    moi.can_nang = "";//?????????     
                    //moi.ngay_vao_noi_tru = bn.NoiTru == 1 ? vv.NgayVao.Value.ToString("yyyyMMddHHmm") : "";
                }
                else
                    moi = null;
                #endregion
                #region xuất xml
                if (moi != null)
                {
                    //string path = path + "\\XML1_" + DungChung.Bien.MaBV + "_" + moi.nam_qt + moi.thang_qt + "_" + moi.ma_lk + ".xml";
                    var xEle1 = new XElement("TONG_HOP",
                            new XElement("MA_LK", moi.ma_lk),
                                                   // new XElement("MA_CSKCB", moi.ma_cskcb),
                                                   new XElement("STT", moi.stt),//--------------------------------------
                                                   new XElement("MA_BN", moi.ma_bn),//---------------------------------------
                                                   new XElement("HO_TEN", new XCData(moi.ho_ten == null ? "" : moi.ho_ten)),
                                                   new XElement("NGAY_SINH", moi.ngay_sinh),
                                                   new XElement("GIOI_TINH", moi.gioi_tinh),
                                                   new XElement("DIA_CHI", new XCData(moi.dia_chi == null ? "" : moi.dia_chi)),
                                                   new XElement("MA_THE", moi.ma_the),
                                                   new XElement("MA_DKBD", moi.ma_dkbd),
                                                   new XElement("GT_THE_TU", moi.gt_the_tu),
                                                   new XElement("GT_THE_DEN", moi.gt_the_den),
                                                   new XElement("TEN_BENH", new XCData(moi.ten_benh == null ? "" : moi.ten_benh)),
                                                   new XElement("MA_BENH", moi.ma_benh),//--------------------------------------
                                                   new XElement("MA_BENHKHAC", moi.ma_benhkhac),//---------------------------------------
                                                   new XElement("MA_LYDO_VVIEN", moi.ma_lydo_vvien),
                                                   new XElement("MA_NOI_CHUYEN", moi.ma_noi_chuyen),
                                                   //new XElement("MA_NOI_DI", moi.ma_noi_di),
                                                   //new XElement("MA_NOI_DEN", moi.ma_noi_den),
                                                   new XElement("MA_TAI_NAN", moi.ma_tai_nan),
                                                   new XElement("NGAY_VAO", moi.ngay_vao),
                                                   new XElement("NGAY_RA", moi.ngay_ra),
                                                   new XElement("SO_NGAY_DTRI", moi.so_ngay_dtri),
                                                   new XElement("KET_QUA_DTRI", moi.ket_qua_dtri),
                                                   new XElement("TINH_TRANG_RV", moi.tinh_trang_rv),
                                                   new XElement("NGAY_TTOAN", moi.ngay_ttoan),
                                                   new XElement("MUC_HUONG", moi.muc_huong),
                                                   new XElement("T_THUOC", moi.t_thuoc),//--------------------------------------
                                                   new XElement("T_VTYT", moi.t_vtyt),//---------------------------------------
                                                   new XElement("T_TONGCHI", moi.T_tongchi),
                                                   new XElement("T_BNTT", moi.t_bntt),
                                                   new XElement("T_BHTT", moi.t_bhtt),
                                                   new XElement("T_NGUONKHAC", moi.t_nguonkhac),
                                                   new XElement("T_NGOAIDS", moi.t_ngoaids),
                                                   new XElement("NAM_QT", moi.nam_qt),
                                                   new XElement("THANG_QT", moi.thang_qt),
                                                   new XElement("MA_LOAI_KCB", moi.ma_loai_kcb),
                                                   new XElement("MA_KHOA", moi.ma_khoa),
                                                   new XElement("MA_CSKCB", moi.ma_cskcb),
                                                   new XElement("MA_KHUVUC", moi.ma_khuvuc),
                                                   new XElement("MA_PTTT_QT", moi.ma_pttt_qt),//---------------------------------------
                                                   new XElement("CAN_NANG", moi.can_nang));
                    //new XElement("NGAY_VAO_NOI_TRU", moi.ngay_vao_noi_tru));
                    return xEle1;
                }
                else return null;
                #endregion
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML1: " + ex.Message);
                return null;
            }
        }
        // kiểm tra thông tuyên
        /// <summary>
        /// 1. đúng tuyến, 4 là thông tuyến
        /// </summary>
        /// <param name="maNoiKcb"></param>
        /// <param name="maDkKcb"></param>
        /// <returns></returns>
        static int checkThongTuyen(string maNoiKcb, string maDkKcb)
        {
            int ttuyen = 1;
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var bv = (from a in _db.BenhViens select new { a.MaBV, a.HangBV, a.TuyenBV, a.MaHuyen }).ToList();
            var bvkham = bv.Where(p => p.MaBV == maNoiKcb).Select(p => p).FirstOrDefault();
            var bvdk = bv.Where(p => p.MaBV == maDkKcb).Select(p => p).FirstOrDefault();
            if (bvdk != null && bvkham != null)
            {
                if (bvkham.TuyenBV.Trim() == "C" && (bvdk.TuyenBV.Trim() == "C" || bvdk.TuyenBV.Trim() == "D"))
                {
                    if (bvkham.MaHuyen != bvdk.MaHuyen)
                    {
                        ttuyen = 4;
                    }
                }
            }
            return ttuyen;
        }
        //
        class tongtienXML2_XML3
        {
            public double T_THUOC { set; get; }
            public double T_VTYT { set; get; }
            public double T_TONGCHI { set; get; }
            public double T_BNTT { set; get; }
            public double T_BNCCT { set; get; }
            public double T_NGUONKHAC { set; get; }
            public double T_BHTT { set; get; }
            public double T_NGOAIDS { set; get; }
            public string MA_PTTT_QT { set; get; }
        }
        static bool KtraTreEm(DateTime _NgaySinh, DateTime _NgayVao)//Ktra xem bn có nhỏ hơn 1 tuổi
        {
            var ngaySinh = _NgaySinh.Date;
            var ngayVao = _NgayVao.Date;
            if (ngaySinh.AddYears(1) >= ngayVao)
                return true;
            else
                return false;
        }
        private XElement gettonghop_4210(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp, List<VienPhict> lvpct, tongtienXML2_XML3 _tongtienXML2_XML3)
        {
            try
            {
                #region lấy ra thông tin chung
                stt = 0;
                tonghop moi = new tonghop();

                #endregion
                #region get Object tonghop
                int Demicd = 0, Demcn = 0, demtt = 0;
                if (bn != null && rv != null && vp != null)//kiểm tra các trường dữ liệu 
                {
                    stt++;
                    moi.ma_lk = bn.MaBNhan.ToString();
                    moi.stt = stt.ToString();
                    moi.ma_bn = bn.MaBNhan.ToString();
                    moi.ho_ten = bn.TenBNhan;
                    moi.ngay_sinh = getNgaySinh(bn.NgaySinh, bn.ThangSinh, bn.NamSinh);
                    moi.gioi_tinh = bn.GTinh == 1 ? "1" : (bn.GTinh == 3 ? "3" : "2");
                    moi.dia_chi = bn.DChi;
                    string sthe = bn.SThe;
                    string madkbd = bn.MaCS;
                    string hanBHtu = bn.HanBHTu != null ? bn.HanBHTu.Value.ToString("yyyyMMdd") : "";
                    string hanBHden = bn.HanBHDen != null ? bn.HanBHDen.Value.ToString("yyyyMMdd") : "";
                    if (!string.IsNullOrEmpty(ttbs.TTTheBHYTold) && ttbs.TTTheBHYTold.Contains(';'))
                    {
                        string[] ttthemoi = ttbs.TTTheBHYTold.Split(';');
                        string hantu = "";
                        if (ttthemoi.Length >= 5 && !string.IsNullOrEmpty(ttthemoi[5]))
                        {
                            hantu = Convert.ToDateTime(ttthemoi[5]).ToString("yyyyMMdd");
                        }
                        string handen = "";
                        if (ttthemoi.Length >= 6 && !string.IsNullOrEmpty(ttthemoi[6]) && madkbd != ttthemoi[6])
                        {
                            handen = Convert.ToDateTime(ttthemoi[6]).ToString("yyyyMMdd");


                        }
                        if (!string.IsNullOrEmpty(ttthemoi[0]) && ((sthe != ttthemoi[0]) || (hanBHtu != hantu) || (handen != hanBHden) || (madkbd != ttthemoi[7])))
                        {
                            sthe = sthe + ";" + ttthemoi[0];
                            hanBHtu = hanBHtu + ";" + hantu;
                            hanBHden = hanBHden + ";" + handen;
                            madkbd = madkbd + ";" + ttthemoi[7];
                        }

                    }
                    else
                    {
                        sthe = !string.IsNullOrEmpty(bn.SThe) ? bn.SThe : PID;
                        if (bn.DTuong.ToLower() == "dịch vụ") // 22/04/2022 xml1 đang bị sai ngày từ, ngày đến -> tách riêng TH bn dịch vụ mới vào case này
                        {
                            hanBHtu = DungChung.Ham.ngayBHYT(bn.NNhap ?? DateTime.Now);
                            hanBHden = DungChung.Ham.ngayBHYT(rv.NgayRa ?? DateTime.Now);
                        }
                    }
                    moi.ma_the = sthe;
                    moi.ma_dkbd = !string.IsNullOrEmpty(madkbd) ? madkbd : (DungChung.Bien.MaBV.Substring(0, 2) + "000");
                    moi.gt_the_tu = hanBHtu;
                    moi.gt_the_den = hanBHden;

                    string miencungct = "";
                    if (bn.NgayHM != null)
                    {
                        if (bn.NgayHM.Value.Year > 1000 && bn.NgayHM.Value.Year < 2999)
                            moi.mien_cung_ct = bn.NgayHM == null ? "" : (bn.NgayHM.Value.Year + bn.NgayHM.Value.Month.ToString("D2") + bn.NgayHM.Value.Day.ToString("D2"));
                    }

                    moi.ten_benh = rv.ChanDoan;
                    moi.ma_benh = DungChung.Ham.FreshString(GetICD(rv.MaICD)[0].Trim());
                    if (string.IsNullOrEmpty(moi.ma_benh))
                    {
                        Demicd++;
                    }
                    moi.ma_benhkhac = DungChung.Ham.FreshString(GetICD(rv.MaICD)[1].Trim());
                    string malydo = "";
                    if ((DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24000" || DungChung.Bien.MaBV == "24208" || DungChung.Bien.MaBV == "24209" || DungChung.Bien.MaBV == "24210" || DungChung.Bien.MaBV == "24211" || DungChung.Bien.MaBV == "24212" || DungChung.Bien.MaBV == "24213" || DungChung.Bien.MaBV == "24214" || DungChung.Bien.MaBV == "24215" || DungChung.Bien.MaBV == "24216" || DungChung.Bien.MaBV == "24217" || DungChung.Bien.MaBV == "24218" || DungChung.Bien.MaBV == "24219" || DungChung.Bien.MaBV == "24220" || DungChung.Bien.MaBV == "24221" || DungChung.Bien.MaBV == "24223" || DungChung.Bien.MaBV == "24224" || DungChung.Bien.MaBV == "24225" || DungChung.Bien.MaBV == "24226") && bn.BNhanLao == true)
                    {
                        malydo = "7";
                    }
                    else if (bn.CapCuu == 1)
                    {
                        malydo = "2";
                    }
                    else
                    {
                        if (bn.Tuyen == 1)
                        {
                            malydo = checkThongTuyen(bn.MaKCB, bn.MaCS).ToString();
                        }
                        else
                        {
                            malydo = "3";
                        }
                    }
                    moi.ma_lydo_vvien = malydo;
                    //moi.ma_noi_chuyen = bn.MaBV;//rv.MaBVC;
                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                    {
                        if (!string.IsNullOrEmpty(bn.MaBV))
                            moi.ma_noi_chuyen = bn.MaBV;
                        else
                            moi.ma_noi_chuyen = "";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(bn.MaBV))
                            moi.ma_noi_chuyen = bn.MaBV;
                        else
                            moi.ma_noi_chuyen = "";

                        if (!string.IsNullOrEmpty(rv.MaBVC))
                            moi.ma_noi_chuyen = rv.MaBVC;
                        else
                            moi.ma_noi_chuyen = rv.MaBVC;
                    }
                    if (!string.IsNullOrEmpty(bn.MaBV))
                        moi.ma_noi_di = bn.MaBV;
                    else
                        moi.ma_noi_di = "";
                    if (!string.IsNullOrEmpty(rv.MaBVC))
                        moi.ma_noi_den = rv.MaBVC;
                    else
                        moi.ma_noi_den = "";
                    int? matainan = DungChung.Bien._listTaiNan.Where(p => p.Tenloai == bn.ChuyenKhoa).Select(p => p.Ma9324).FirstOrDefault();
                    moi.ma_tai_nan = matainan == null ? "" : matainan.ToString();
                    moi.ngay_vao = bn.NNhap.Value.ToString("yyyyMMddHHmm");// khác byt
                    moi.ngay_ra = rv.NgayRa.Value.ToString("yyyyMMddHHmm");// khác Byt
                    moi.so_ngay_dtri = rv.SoNgaydt.Value.ToString();
                    if (rv.KetQua == null)
                        moi.ket_qua_dtri = "2";
                    else if (rv.KetQua == "Khỏi")
                        moi.ket_qua_dtri = "1";
                    else if (rv.KetQua == "Đỡ|Giảm")
                        moi.ket_qua_dtri = "2";
                    else if (rv.KetQua == "Không T.đổi")
                        moi.ket_qua_dtri = "3";
                    else if (rv.KetQua == "Tử vong")
                        moi.ket_qua_dtri = "5";
                    else
                        moi.ket_qua_dtri = "4";
                    if (rv.Status == null || rv.Status == 2)// ra viện
                        moi.tinh_trang_rv = "1";
                    else if (rv.Status == 1)// chuyển viện
                        moi.tinh_trang_rv = "2";
                    else
                        moi.tinh_trang_rv = rv.Status.Value.ToString();// 3: trốn viện, 4: xin ra viện
                    if (vp.NgayTT.Value >= DateTime.Now)
                    {
                        demtt++;
                    }
                    else
                        moi.ngay_ttoan = vp.NgayTT.Value.ToString("yyyyMMddHHmm");// khác BYT


                    moi.t_thuoc = Math.Round(_tongtienXML2_XML3.T_THUOC, 2).ToString();
                    moi.t_vtyt = Math.Round(_tongtienXML2_XML3.T_VTYT, 2).ToString();
                    moi.T_tongchi = Math.Round(_tongtienXML2_XML3.T_TONGCHI, 2).ToString();
                    moi.t_bhtt = Math.Round(_tongtienXML2_XML3.T_BHTT, 2).ToString();
                    moi.t_bncct = Math.Round(_tongtienXML2_XML3.T_BNCCT, 2).ToString(); ; //4210
                    moi.t_nguonkhac = Math.Round(_tongtienXML2_XML3.T_NGUONKHAC, 2).ToString();
                    moi.t_bntt = Math.Round(_tongtienXML2_XML3.T_BNTT, 2).ToString();
                    double t_ngoaids = 0;
                    var test = (from vpct in lvpct
                                join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                                join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                                select new
                                {
                                    vpct.ThanhTien,
                                    ndv.TenNhomCT,
                                    dv.TenDV
                                }).ToList();
                    var checkDV = test.Where(p => p.TenDV.Contains("Thận nhân tạo thường qui") || p.TenDV.Contains("Lọc màng bụng cấp cứu liên tục") || p.TenDV.Contains("Lọc màng bụng chu kỳ"));
                    if (rv.MaICD.Contains("C0") || rv.MaICD.Contains("D0"))
                    {
                        t_ngoaids = 0;
                        //t_ngoaids = test.Sum(p => p.ThanhTien);
                    }
                    else if (checkDV != null && checkDV.Count() > 0)
                    {
                        t_ngoaids = 0;
                        //t_ngoaids = test.Sum(p => p.ThanhTien);
                    }
                    else if ((bn.SThe.Substring(0, 2)) == "QN" || (bn.SThe.Substring(0, 2)) == "CA" || (bn.SThe.Substring(0, 2)) == "CY")
                    {
                        t_ngoaids = 0;

                        //t_ngoaids = test.Sum(p => p.ThanhTien);
                    }
                    else
                    {
                        t_ngoaids = 0;
                        //t_ngoaids = test.Where(p => p.TenNhomCT == "Vận chuyển").Sum(p => p.ThanhTien);
                    }
                    moi.t_ngoaids = "0";
                    //moi.t_ngoaids = Math.Round(t_ngoaids, 2).ToString();
                    moi.nam_qt = vp.NgayTT.Value.Year.ToString();
                    moi.thang_qt = vp.NgayTT.Value.Month.ToString("D2");
                    //if (bn.NoiTru == 1 && ((TimeSpan)(rv.NgayRa - rv.NgayVao)).TotalHours < 4)
                    //    moi.ma_loai_kcb = "9";
                    if ((DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24000" || DungChung.Bien.MaBV == "24208" || DungChung.Bien.MaBV == "24209" || DungChung.Bien.MaBV == "24210" || DungChung.Bien.MaBV == "24211" || DungChung.Bien.MaBV == "24212" || DungChung.Bien.MaBV == "24213" || DungChung.Bien.MaBV == "24214" || DungChung.Bien.MaBV == "24215" || DungChung.Bien.MaBV == "24216" || DungChung.Bien.MaBV == "24217" || DungChung.Bien.MaBV == "24218" || DungChung.Bien.MaBV == "24219" || DungChung.Bien.MaBV == "24220" || DungChung.Bien.MaBV == "24221" || DungChung.Bien.MaBV == "24223" || DungChung.Bien.MaBV == "24224" || DungChung.Bien.MaBV == "24225" || DungChung.Bien.MaBV == "24226") && bn.BNhanLao == true)
                    {
                        moi.ma_loai_kcb = "7";
                    }
                    else if (bn.NoiTru == 1)
                        moi.ma_loai_kcb = "3";
                    else if (bn.DTNT == true)
                        moi.ma_loai_kcb = "2";
                    else
                        moi.ma_loai_kcb = "1";
                    moi.ma_khoa = getMaKhoa_QD(rv.MaKP);
                    moi.ma_cskcb = bn.MaKCB;
                    moi.ma_khuvuc = bn.KhuVuc;

                    if (!string.IsNullOrEmpty(_tongtienXML2_XML3.MA_PTTT_QT))
                        moi.ma_pttt_qt = _tongtienXML2_XML3.MA_PTTT_QT;
                    else
                        moi.ma_pttt_qt = "";
                    string Ngaysinh = (string.IsNullOrEmpty(bn.NgaySinh != null ? bn.NgaySinh.Trim() : "") ? "1" : bn.NgaySinh) + "/" + (string.IsNullOrEmpty(bn.ThangSinh != null ? bn.ThangSinh.Trim() : "") ? "1" : bn.ThangSinh) + "/" + (string.IsNullOrEmpty(bn.NamSinh.Trim()) ? "1" : bn.NamSinh);
                    DateTime _ngaysinh = Convert.ToDateTime(Ngaysinh);
                    if (KtraTreEm(_ngaysinh, bn.NNhap.Value))
                    {
                        if (ttbs.CanNang_ChieuCao != null && ttbs.CanNang_ChieuCao.Contains(";"))
                        {
                            if (!string.IsNullOrEmpty(ttbs.CanNang_ChieuCao.Split(';')[0]))
                            {
                                string cn = ttbs.CanNang_ChieuCao.Split(';')[0];
                                if (cn.Contains("."))
                                {

                                    if (cn.Split('.')[1].Length != 2)
                                    {
                                        Demcn++;
                                    }
                                    else
                                    {
                                        moi.can_nang = cn;
                                    }
                                }
                                else
                                {
                                    moi.can_nang = cn;
                                }
                            }
                            else
                            {
                                Demcn++;
                            }
                        }
                        else
                            Demcn++;
                    }

                }
                else
                    moi = null;
                if (Demcn > 0)
                {
                    checkData += " Bệnh nhân dưới 1 tuổi chưa có cân nặng hoặc cân nặng ko đúng định dạng";
                    moi = null;
                }
                if (Demicd > 0)
                {
                    checkData += " Bệnh nhân chưa có bệnh chính";
                    moi = null;
                }
                if (demtt > 0)
                {
                    checkData += " Ngày thanh toán không được lớn hơn ngày giờ gửi dữ liệu";
                    moi = null;
                }
                //moi.ngay_vao_noi_tru = bn.NoiTru == 1 ? vv.NgayVao.Value.ToString("yyyyMMddHHmm") : "";
                #endregion
                #region xuất xml
                if (moi != null)
                {
                    //string path = path + "\\XML1_" + DungChung.Bien.MaBV + "_" + moi.nam_qt + moi.thang_qt + "_" + moi.ma_lk + ".xml";
                    var xEle1 = new XElement("TONG_HOP",
                            new XElement("MA_LK", moi.ma_lk),
                                                   // new XElement("MA_CSKCB", moi.ma_cskcb),
                                                   new XElement("STT", moi.stt),//--------------------------------------
                                                   new XElement("MA_BN", moi.ma_bn),//---------------------------------------
                                                   new XElement("HO_TEN", new XCData(moi.ho_ten == null ? "" : moi.ho_ten)),
                                                   new XElement("NGAY_SINH", moi.ngay_sinh),
                                                   new XElement("GIOI_TINH", moi.gioi_tinh),
                                                   new XElement("DIA_CHI", new XCData(moi.dia_chi == null ? "" : moi.dia_chi)),
                                                   new XElement("MA_THE", moi.ma_the),
                                                   new XElement("MA_DKBD", moi.ma_dkbd),
                                                   new XElement("GT_THE_TU", moi.gt_the_tu ?? ""),
                                                   new XElement("GT_THE_DEN", moi.gt_the_den ?? ""),
                                                   new XElement("MIEN_CUNG_CT", moi.mien_cung_ct),// thời điểm nguwoif bệnh bắt đầu được miễn cùng chi trả theo giấy xác nhân của cq BHXH, gồm 8 ký tự: 4kys tự năm + 2 ký tự tự tháng + 2 ký tự ngày ?????????????????
                                                   new XElement("TEN_BENH", new XCData(moi.ten_benh == null ? "" : moi.ten_benh)),
                                                   new XElement("MA_BENH", moi.ma_benh),//--------------------------------------
                                                   new XElement("MA_BENHKHAC", moi.ma_benhkhac),//---------------------------------------
                                                   new XElement("MA_LYDO_VVIEN", moi.ma_lydo_vvien),
                                                   new XElement("MA_NOI_CHUYEN", moi.ma_noi_chuyen),
                                                   //new XElement("MA_NOI_DI", moi.ma_noi_di),
                                                   //new XElement("MA_NOI_DEN", moi.ma_noi_den),
                                                   new XElement("MA_TAI_NAN", moi.ma_tai_nan),
                                                   new XElement("NGAY_VAO", moi.ngay_vao),
                                                   new XElement("NGAY_RA", moi.ngay_ra),
                                                   new XElement("SO_NGAY_DTRI", moi.so_ngay_dtri),
                                                   new XElement("KET_QUA_DTRI", moi.ket_qua_dtri),
                                                   new XElement("TINH_TRANG_RV", moi.tinh_trang_rv),
                                                   new XElement("NGAY_TTOAN", moi.ngay_ttoan),
                                                   // new XElement("MUC_HUONG", moi.muc_huong),
                                                   new XElement("T_THUOC", moi.t_thuoc),//--------------------------------------
                                                   new XElement("T_VTYT", moi.t_vtyt),//---------------------------------------
                                                   new XElement("T_TONGCHI", moi.T_tongchi),
                                                   new XElement("T_BNTT", moi.t_bntt),
                                                   new XElement("T_BNCCT", moi.t_bncct),// Tổng số tiền nguwoif bệnh cùng chi trả trong phạm vi quyền lợi được hưởng BHYT  trên XML2,3, làm tròn đến 2 chữ số thập phân, sử dụng dấu Chấm để phân cách giữa số nguyên với số thập phân  ????????????????????????????????????
                                                   new XElement("T_BHTT", moi.t_bhtt),
                                                   new XElement("T_NGUONKHAC", moi.t_nguonkhac),
                                                   new XElement("T_NGOAIDS", moi.t_ngoaids),
                                                   new XElement("NAM_QT", moi.nam_qt),
                                                   new XElement("THANG_QT", moi.thang_qt),
                                                   new XElement("MA_LOAI_KCB", moi.ma_loai_kcb),
                                                   new XElement("MA_KHOA", moi.ma_khoa),
                                                   new XElement("MA_CSKCB", moi.ma_cskcb),
                                                   new XElement("MA_KHUVUC", moi.ma_khuvuc),
                                                   new XElement("MA_PTTT_QT", moi.ma_pttt_qt),//---------------------------------------
                                                   new XElement("CAN_NANG", moi.can_nang));
                    //new XElement("NGAY_VAO_NOI_TRU", moi.ngay_vao_noi_tru));
                    return xEle1;
                }
                else return null;
                #endregion
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML1: " + ex.Message);
                return null;
            }
        }
        private XElement getChitietThuoc(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp)
        {
            try
            {
                #region lấy ra thông tin chung
                stt = 0;
                List<chi_tiet_thuoc> listThuoc = new List<chi_tiet_thuoc>();

                var qvpct = (
                             from vpct in data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi && p.TrongBH == 1)
                             join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             join n in data.NhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7) on tn.IDNhom equals n.IDNhom
                             select new
                             {
                                 vpct.NgayChiPhi,
                                 vpct.TyLeTT,
                                 vpct.TyLeBHTT,
                                 vpct.MaDV,
                                 vpct.SoLuong,
                                 vpct.DonGia,
                                 vpct.ThanhTien,
                                 vpct.TienBH,
                                 vpct.TienBN,
                                 vpct.TrongBH,
                                 vpct.MaKP,
                                 vp.NgayRa,//tạm lấy ngày y lệnh
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
                                 dv.TenDV
                             }).ToList();


                #endregion
                #region get DS thuốc
                if (bn != null && rv != null && vp != null)//kiểm tra các trường dữ liệu 
                {
                    int kt = 0;

                    foreach (var a in qvpct)
                    {
                        chi_tiet_thuoc moi = new chi_tiet_thuoc();
                        stt++;
                        moi.ma_lk = bn.MaBNhan.ToString();
                        moi.stt = stt.ToString();
                        if (String.IsNullOrEmpty(a.MaQD))
                        {
                            checkData += a.TenDV + "; ";
                            kt++;
                        }
                        moi.ma_thuoc = String.IsNullOrEmpty(a.MaQD) ? "" : a.MaQD;
                        moi.ma_nhom = a.IDNhom.ToString();
                        moi.ten_thuoc = String.IsNullOrEmpty(a.TenDV) ? a.TenHC : a.TenDV; //????? Tên ghi đúng theo danh mục thuốc cục qld công bố
                        moi.don_vi_tinh = a.DonVi;
                        moi.ham_luong = a.HamLuong;
                        moi.duong_dung = a.MaDuongDung;
                        moi.lieu_dung = "";//?????????????????????????
                        moi.so_dang_ky = a.SoDK;
                        double soluong = 0, dongia = 0;
                        soluong = (Math.Round(a.SoLuong, 2));
                        dongia = (Math.Round(a.DonGia, 2));
                        double tyleBHtt = a.TyLeBHTT / 100;
                        double tylett = a.TyLeTT / 100;
                        moi.SoLuong = a.SoLuong;
                        moi.so_luong = soluong.ToString("G", CultureInfo.InvariantCulture);
                        moi.don_gia = dongia.ToString("G", CultureInfo.InvariantCulture);
                        moi.tyle_tt = a.TyLeTT == null ? "100" : a.TyLeTT.ToString();
                        moi.thanh_tien = (soluong * dongia * tylett).ToString("G", CultureInfo.InvariantCulture); //a.ThanhTien.ToString("G", CultureInfo.InvariantCulture);
                        string makhoa = getMaKhoa_QD(a.MaKP);// mã kpQD
                        if (String.IsNullOrEmpty(makhoa))
                        {
                            checkData += a.TenDV + "; ";
                            kt++;
                        }
                        moi.ma_khoa = makhoa;
                        moi.ma_bac_si = getMaBacSy(bn.MaBNhan, a.MaDV, a.MaKP);

                        moi.ma_benh = rv.MaICD;
                        moi.ngay_yl = a.NgayChiPhi.ToString("yyyyMMddHHmm");// đang tạm lấy bằng ra viện
                        moi.ma_pttt = "1";// Chưa có dữ liệu để lấy nên fix cứng
                        listThuoc.Add(moi);
                    }
                    if (kt > 0)
                    {
                        checkData += " chưa có mã dùng chung hoặc mã khoa";
                        return null;
                    }

                }
                #region trừ thuốc trả dược
                //List<chi_tiet_thuoc> _lThuocTraDuoc = listThuoc.Where(p => p.SoLuong < 0).ToList();
                //listThuoc = listThuoc.Where(p => p.SoLuong > 0).ToList();
                //foreach (var traduoc in _lThuocTraDuoc)
                //{
                //    double soluongTD = -traduoc.SoLuong; // số lượng trả dược
                //    foreach (var kedon in listThuoc)
                //    {
                //        if (traduoc.ma_thuoc == kedon.ma_thuoc)
                //        {
                //            if(soluongTD <= kedon.SoLuong)
                //            {
                //                kedon.SoLuong = kedon.SoLuong - soluongTD;
                //                kedon.so_luong = kedon.SoLuong.ToString("G", CultureInfo.InvariantCulture);
                //                break;
                //            }
                //            else
                //            {
                //                soluongTD = soluongTD - kedon.SoLuong;
                //                kedon.so_luong = "0";
                //                kedon.SoLuong = 0;                                
                //            }

                //        }
                //    }
                //}
                //listThuoc = listThuoc.Where(p => p.SoLuong > 0).ToList();
                #endregion
                #endregion
                #region get XMl
                if (listThuoc.Count > 0)
                {
                    var xEle1 = new XElement("DSACH_CHI_TIET_THUOC",
                                             from item2 in listThuoc
                                             select
                                                     new XElement("CHI_TIET_THUOC",
                                                          new XElement("MA_LK", item2.ma_lk),
                                                          new XElement("STT", item2.stt),
                                                          new XElement("MA_THUOC", item2.ma_thuoc),
                                                          new XElement("MA_NHOM", item2.ma_nhom),
                                                          new XElement("TEN_THUOC", new XCData(item2.ten_thuoc == null ? "" : item2.ten_thuoc)),
                                                          new XElement("DON_VI_TINH", item2.don_vi_tinh),
                                                          new XElement("HAM_LUONG", new XCData(item2.ham_luong == null ? "" : item2.ham_luong)),
                                                          new XElement("DUONG_DUNG", item2.duong_dung),
                                                          new XElement("LIEU_DUNG", new XCData(item2.lieu_dung == null ? "" : item2.lieu_dung)),
                                                          new XElement("SO_DANG_KY", item2.so_dang_ky),
                                                          new XElement("SO_LUONG", item2.so_luong),
                                                          new XElement("DON_GIA", item2.don_gia),
                                                          new XElement("TYLE_TT", item2.tyle_tt),
                                                          new XElement("THANH_TIEN", item2.thanh_tien),
                                                          new XElement("MA_KHOA", item2.ma_khoa),
                                                          new XElement("MA_BAC_SI", item2.ma_bac_si),
                                                          new XElement("MA_BENH", item2.ma_benh),
                                                          new XElement("NGAY_YL", item2.ngay_yl),
                                                          new XElement("MA_PTTT", item2.ma_pttt)
                                                          ));
                    return xEle1;
                }
                else
                    return null;
                #endregion
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML2: " + ex.Message);
                return null;
            }

        }
        #region làm tròn số lên vd:0.745=>0.75
        public double Round_custom(double a)
        {
            double kq = Math.Round(a, 2, MidpointRounding.AwayFromZero);
            string z = a.ToString();
            if (z.Contains('.'))
            {
                string[] arr_z = z.Split('.');
                if (arr_z[1].Length > 2)
                {
                    double so3 = Convert.ToDouble(arr_z[1].Substring(2, 1));
                    double so2 = Convert.ToDouble(arr_z[1].Substring(1, 1));
                    if (so3 == 5)
                    {
                        kq = Math.Round(a + 0.001, 2);
                    }
                }
            }
            return kq;
        }
        #endregion

        private XElement getChitietThuoc_4210(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp, List<VienPhict> lvpct, ref tongtienXML2_XML3 _tongtienXML2_XML3)
        {
            try
            {
                #region lấy ra thông tin chung
                stt = 0;
                List<chi_tiet_thuoc> listThuoc = new List<chi_tiet_thuoc>();
                var qvpct1 = (from vpct in data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi && p.TrongBH == 1) // lấy dv chạy thận (ID nhóm = 8 )
                              join dv in data.DichVus.Where(p => p.TenDV.Contains("Thận nhân tạo thường qui")) on vpct.MaDV equals dv.MaDV
                              select new
                              {
                                  dv.TenDV,
                              }).ToList();
                var qvpct = (
                             from vpct in data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi && p.TrongBH == 1)
                             join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             join n in data.NhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7) on tn.IDNhom equals n.IDNhom
                             join cb in data.CanBoes on vpct.MaCB equals cb.MaCB into kq
                             from kq1 in kq.DefaultIfEmpty()
                             select new
                             {
                                 vpct.ThuocDVGayTe,
                                 vpct.NgayChiPhi,
                                 vpct.TyLeTT,
                                 vpct.TyLeBHTT,
                                 vpct.MaDV,
                                 vpct.SoLuong,
                                 vpct.DonGia,
                                 vpct.ThanhTien,
                                 vpct.TienBH,
                                 vpct.TienNguonKhac,
                                 vpct.TienBN,
                                 vpct.TrongBH,
                                 vpct.MaKP,
                                 vp.NgayRa,//tạm lấy ngày y lệnh
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
                                 dv.MaNhom,
                                 dv.NhomThau,
                                 MaCB = kq1 == null ? "" : kq1.MaCCHN,
                                 dv.DongY,
                                 dv.ARV
                             }).ToList();


                #endregion

                string malydo = "";
                if (bn.CapCuu == 1)
                {
                    malydo = "2";
                }
                else
                {
                    if (bn.Tuyen == 1)
                    {
                        malydo = checkThongTuyen(bn.MaKCB, bn.MaCS).ToString();
                    }
                    else
                    {
                        malydo = "3";
                    }
                }
                #region get DS thuốc
                int _NoiTru = 0;
                string _KhuVuc = "";
                if (bn != null && rv != null && vp != null)//kiểm tra các trường dữ liệu 
                {
                    string muchuong = "";
                    int hangbv = 0;
                    _NoiTru = bn.NoiTru ?? 0;
                    _KhuVuc = bn.KhuVuc;
                    hangbv = DungChung.Ham.hangBV(QLBV.DungChung.Bien.MaBV);
                    if (bn.NoiTru == null || bn.Tuyen == null)
                        muchuong = "0";
                    else
                    {

                    }
                    int kt = 0;
                    int ktthau = 0;
                    int ktMaBS = 0;
                    string dichvuChuaCoMaBS = "";
                    int ktLieuDung = 0, ktsodk = 0;
                    string thuocChuaCoLieuDung = "", chuacosodk = "";
                    string thongtinthau = "";
                    var bvBD = data.BenhViens.FirstOrDefault(o => o.MaBV == bn.MaCS);
                    var bv = data.BenhViens.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                    foreach (var a in qvpct)
                    {
                        var qtylett = a.TyLeBHTT;
                        if (qtylett > 0)
                            muchuong = qtylett.ToString();
                        else
                        {
                            muchuong = _getmuc(hangbv, bn.SThe, bn.Tuyen.Value, bn.NoiTru.Value, vp.NgayTT.Value).ToString();
                        }
                        chi_tiet_thuoc moi = new chi_tiet_thuoc();
                        stt++;
                        moi.ma_lk = bn.MaBNhan.ToString();
                        moi.stt = stt.ToString();
                        if (String.IsNullOrEmpty(a.MaQD))
                        {
                            checkData += a.TenDV + "; ";
                            kt++;
                        }
                        if (!String.IsNullOrEmpty(a.ThuocDVGayTe))
                        {
                            int madv = Int32.Parse(a.ThuocDVGayTe);
                            string mathuoc;
                            var dv = data.DichVus.Where(p => p.MaDV == madv).Select(p => p.MaQD).FirstOrDefault();
                            if (dv.Count() > 0)
                                moi.ma_thuoc = a.MaQD + "_" + dv;
                        }
                        else
                            moi.ma_thuoc = String.IsNullOrEmpty(a.MaQD) ? "" : a.MaQD;
                        moi.ma_nhom = a.IDNhom.ToString();// id nhóm dịch vụ
                        moi.ten_thuoc = String.IsNullOrEmpty(a.TenDV) ? a.TenHC : a.TenDV; //????? Tên ghi đúng theo danh mục thuốc cục qld công bố
                        moi.don_vi_tinh = a.DonVi;
                        moi.ham_luong = a.HamLuong;
                        moi.duong_dung = a.MaDuongDung;
                        moi.lieu_dung = getLieuDung(bn.MaBNhan, a.MaDV ?? 0, a.MaKP ?? 0);
                        if (moi.lieu_dung == "")
                        {
                            thuocChuaCoLieuDung += a.TenDV + ", ";
                            ktLieuDung++;
                        }
                        moi.so_dang_ky = a.SoDK;
                        if (moi.so_dang_ky == "" && a.DongY != 1) //riêng đông y ko bắt lỗi thiếu sô đăng kí a quý 03-05
                        {
                            if (a.TenDV.ToLower().Contains("ôxy") || a.TenDV.ToLower().Contains("oxy") || a.IDNhom == 7)// || a.DongY == 1 
                            {

                            }
                            else
                            {
                                chuacosodk += a.TenDV + ", ";
                                ktsodk++;
                            }
                        }

                        moi.tt_thau = a.SoQD + ";" + a.MaNhom + ";" + a.NhomThau; // số quyết định trúng thầu + gói thầu + nhóm thầu
                        if (a.DongY != 1) //thuốc đông y ko cần check lỗi a quý 03-05
                        {
                            if ((string.IsNullOrEmpty(a.SoQD) || string.IsNullOrEmpty(a.MaNhom) || string.IsNullOrEmpty(a.NhomThau)) && (a.IDNhom != 7))
                            {
                                moi.tt_thau = "";
                                thongtinthau += a.TenDV + ", ";
                                ktthau++;
                            }
                        }
                        moi.pham_vi = "1";
                        double tyleBHtt = Math.Round(a.TyLeBHTT / 100, 2);
                        double tylett = Math.Round(a.TyLeTT / 100, 2);
                        double soluong = 0, dongia = 0, thanhtien = 0, bhtt = 0, bntt = 0, bncct = 0, nguonkhac = a.TienNguonKhac ?? 0;
                        soluong = Math.Round(a.SoLuong, 2);
                        dongia = Math.Round(a.DonGia, 3);
                        thanhtien = Round_custom(Math.Round(a.ThanhTien, 4));
                        nguonkhac = Round_custom(Math.Round(nguonkhac, 4)); // thuốc ARV được quỹ toàn cầu trả thay cho BN
                        if (a.TrongBH == 1)
                        {
                            bhtt = Round_custom(Math.Round(thanhtien * tylett * tyleBHtt, 4));
                            if (malydo == "3")
                            {
                                if ((!string.IsNullOrWhiteSpace(bn.SThe) && bvBD != null && DungChung.Bien.MaBV.Substring(0, 2) != bvBD.MaTinh.Trim() && bv != null && bv.TuyenBV != null && bv.TuyenBV.Trim() == "C") || (!string.IsNullOrWhiteSpace(bn.SThe) && bvBD != null && DungChung.Bien.MaBV.Substring(0, 2) == bvBD.MaTinh.Trim() && (bvBD.TuyenBV == "A" || bvBD.TuyenBV == "B")))
                                {
                                    bncct = Round_custom(Math.Round(thanhtien * tylett * 100 / 100, 4) - bhtt);
                                }
                                else
                                {
                                    if (_NoiTru == 1 || _NoiTru != 1)
                                    {
                                        if (a.NgayChiPhi >= DateTime.Parse("01/01/2021"))
                                            bncct = Round_custom(Math.Round(thanhtien * tylett * 100 / 100, 4) - bhtt);
                                        else
                                        {
                                            if (_KhuVuc != null && _KhuVuc.ToLower().Contains("k"))
                                            {
                                                bncct = Round_custom(Math.Round(thanhtien * tylett * 100 / 100, 4) - bhtt);
                                            }
                                            else
                                            {
                                                if (hangbv == 1)
                                                {
                                                    bncct = Round_custom(Math.Round(thanhtien * tylett * 40 / 100, 4) - bhtt);
                                                }
                                                else if (hangbv == 2)
                                                {

                                                    bncct = Round_custom(Math.Round(thanhtien * tylett * 60 / 100, 4) - bhtt);
                                                }
                                                else
                                                    bncct = 0;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        bncct = 0;
                                    }
                                }
                            }
                            else
                            {
                                bncct = Round_custom(Math.Round(thanhtien * tylett, 4) - bhtt);
                            }
                            if (a.ARV == true)
                            {
                                nguonkhac = bncct;
                                bncct = 0;
                            }
                            bntt = Round_custom(thanhtien - bhtt - bncct - nguonkhac);
                        }
                        else
                        {
                            bntt = thanhtien;
                        }
                        if (rv.MaICD.Contains("C0") || rv.MaICD.Contains("D0"))
                        {
                            //moi.t_ngoaids = Round_custom(Math.Round(a.ThanhTien, 4)).ToString();
                            moi.t_ngoaids = "0";
                        }
                        else if (a.TenDV.Contains("Thận nhân tạo thường qui") || a.TenDV.Contains("Lọc màng bụng cấp cứu liên tục") || a.TenDV.Contains("Lọc màng bụng chu kỳ"))
                        {
                            moi.t_ngoaids = "0";
                            //moi.t_ngoaids = Round_custom(Math.Round(a.ThanhTien,4)).ToString();
                        }
                        else if ((bn.SThe.Substring(0, 2)) == "QN" || (bn.SThe.Substring(0, 2)) == "CA" || (bn.SThe.Substring(0, 2)) == "CY")
                        {
                            moi.t_ngoaids = "0";
                            //moi.t_ngoaids = Round_custom(Math.Round(a.ThanhTien, 4)).ToString();
                        }
                        else if (a.TenNhomCT == "Vận chuyển")
                        {
                            moi.t_ngoaids = "0";
                            //moi.t_ngoaids = Round_custom(Math.Round(a.ThanhTien, 4)).ToString();
                        }
                        else if (qvpct1.Count > 0)
                        {
                            moi.t_ngoaids = "0";
                            //moi.t_ngoaids = Round_custom(Math.Round(a.ThanhTien, 4)).ToString();
                        }
                        moi.SoLuong = a.SoLuong;
                        moi.so_luong = soluong.ToString("G", CultureInfo.InvariantCulture);
                        moi.don_gia = dongia.ToString("G", CultureInfo.InvariantCulture);
                        moi.tyle_tt = a.TyLeTT.ToString();
                        // 
                        _tongtienXML2_XML3.T_THUOC += thanhtien;
                        _tongtienXML2_XML3.T_TONGCHI += thanhtien;
                        _tongtienXML2_XML3.T_BNTT += bntt;
                        _tongtienXML2_XML3.T_BHTT += bhtt;
                        _tongtienXML2_XML3.T_BNCCT += bncct;
                        _tongtienXML2_XML3.T_NGUONKHAC += nguonkhac;
                        _tongtienXML2_XML3.T_NGOAIDS += 0;
                        //
                        moi.thanh_tien = (thanhtien).ToString("G", CultureInfo.InvariantCulture);
                        moi.muc_huong = muchuong;

                        moi.t_bhtt = (bhtt).ToString("G", CultureInfo.InvariantCulture);//4210
                        moi.t_bncct = (bncct).ToString("G", CultureInfo.InvariantCulture);//4210

                        moi.t_bntt = (bntt).ToString("G", CultureInfo.InvariantCulture);
                        moi.t_nguonkhac = (nguonkhac).ToString("G", CultureInfo.InvariantCulture);//4210


                        string makhoa = getMaKhoa_QD(a.MaKP);// mã kpQD
                        if (String.IsNullOrEmpty(makhoa))
                        {
                            checkData += a.TenDV + "; ";
                            kt++;
                        }
                        moi.ma_khoa = makhoa;
                        moi.ma_bac_si = a.MaCB;
                        if (string.IsNullOrEmpty(moi.ma_bac_si))
                            moi.ma_bac_si = getMaBacSy(bn.MaBNhan, a.MaDV, a.MaKP);
                        if (moi.ma_bac_si == "")
                        {
                            dichvuChuaCoMaBS += a.TenDV + ", ";
                            ktMaBS++;
                        }
                        moi.ma_benh = DungChung.Ham.FreshString(rv.MaICD.Trim());
                        moi.ngay_yl = a.NgayChiPhi.ToString("yyyyMMddHHmm");// đang tạm lấy bằng ra viện
                        moi.ma_pttt = "1";// Chưa có dữ liệu để lấy nên fix cứng
                        listThuoc.Add(moi);
                    }
                    if (kt > 0)
                    {
                        checkData += " chưa có mã dùng chung hoặc mã khoa. ";
                        return null;
                    }
                    if (ktMaBS > 0)
                    {
                        checkData += dichvuChuaCoMaBS + " chưa có mã bác sỹ. ";
                        return null;
                    }
                    if (ktLieuDung > 0)
                    {
                        checkData += thuocChuaCoLieuDung + " chưa có liều dùng. ";
                        return null;
                    }
                    if (ktthau > 0)
                    {
                        checkData += thongtinthau + " chưa có đầy đủ thông tin thầu. ";
                        return null;
                    }
                    if (ktsodk > 0)
                    {
                        checkData += chuacosodk + " chưa có số đăng ký.  ";
                        return null;
                    }

                }

                #endregion
                #region get XMl
                if (listThuoc.Count > 0)
                {
                    var xEle1 = new XElement("DSACH_CHI_TIET_THUOC",
                                             from item2 in listThuoc
                                             select
                                                     new XElement("CHI_TIET_THUOC",
                                                          new XElement("MA_LK", item2.ma_lk),
                                                          new XElement("STT", item2.stt),
                                                          new XElement("MA_THUOC", item2.ma_thuoc),
                                                          new XElement("MA_NHOM", item2.ma_nhom),
                                                          new XElement("TEN_THUOC", new XCData(item2.ten_thuoc == null ? "" : item2.ten_thuoc)),
                                                          new XElement("DON_VI_TINH", item2.don_vi_tinh),
                                                          new XElement("HAM_LUONG", new XCData(item2.ham_luong == null ? "" : item2.ham_luong)),
                                                          new XElement("DUONG_DUNG", item2.duong_dung),
                                                          new XElement("LIEU_DUNG", new XCData(item2.lieu_dung == null ? "" : item2.lieu_dung)),
                                                          new XElement("SO_DANG_KY", item2.so_dang_ky),
                                                          new XElement("TT_THAU", item2.tt_thau),//tHÔNG TIN THẦU: SỐ QUYẾT ĐỊNH trúng thầy, gói thầu, nhóm thầu, cách nhau bằng dấu ";" vd: thuốc trúng thầu theo quyết định số 12/QQD_SYT  thuộc gói generic nhóm 2 ghi 12/QĐ-SYT; G1; N2; TH ko có quyết định thầu ghi số công văn gửi cơ quan BHXH ??????????????
                                                          new XElement("PHAM_VI", item2.pham_vi),// 1: THUỐC trong phạm vi hưởng BHYT (trong danh mục thuốc do quỹ BHYT chi trả); 2: ngoài phạm vi hưởng BHYT (ngoài danh mục thuốc do quỹ BHYT chi trả)
                                                          new XElement("TYLE_TT", item2.tyle_tt),
                                                          new XElement("SO_LUONG", item2.so_luong),
                                                          new XElement("DON_GIA", item2.don_gia),
                                                          new XElement("THANH_TIEN", item2.thanh_tien),
                                                          new XElement("MUC_HUONG", item2.muc_huong),
                                                          new XElement("T_NGUONKHAC", item2.t_nguonkhac),
                                                          new XElement("T_BNTT", item2.t_bntt),
                                                          new XElement("T_BHTT", item2.t_bhtt),
                                                          new XElement("T_BNCCT", item2.t_bncct),
                                                          new XElement("T_NGOAIDS", item2.t_ngoaids),
                                                          new XElement("MA_KHOA", item2.ma_khoa),
                                                          new XElement("MA_BAC_SI", item2.ma_bac_si),
                                                          new XElement("MA_BENH", item2.ma_benh),
                                                          new XElement("NGAY_YL", item2.ngay_yl),
                                                          new XElement("MA_PTTT", item2.ma_pttt)
                                                          ));
                    return xEle1;
                }
                else
                    return null;
                #endregion
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML2: " + ex.Message);
                return null;
            }

        }

        private string getLieuDung(int mabn, int MaDV, int MaKP)
        {

            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string LieuDung = "";
            var qdt = (from dt in data.DThuocs.Where(p => p.MaBNhan == mabn).Where(p => p.PLDV == 1)
                       join dtct in data.DThuoccts.Where(p => p.MaDV == MaDV).Where(p => p.Luong != null && p.SoLan != null) on dt.IDDon equals dtct.IDDon
                       select dtct).FirstOrDefault();
            if (qdt != null)
                LieuDung = qdt.Luong + qdt.DviUong + "/lần" + "*" + qdt.SoLan + "lần/ngày";
            return LieuDung;

        }
        private XElement getChitietDvkt(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp)
        {
            try
            {
                #region lấy ra thoong tin chung
                stt = 0;
                List<chi_tiet_dvkt> listDV = new List<chi_tiet_dvkt>();

                var qvpct = (
                             from vpct in data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi && p.TrongBH == 1)
                             join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                             where (dv.PLoai == 2 || dv.PLoai == 3 || (dv.PLoai == 1 && (n.IDNhom == 10 || n.IDNhom == 11)))
                             select new
                             {
                                 vpct.NgayChiPhi,
                                 vpct.NgayYLenh,
                                 vp.NgayRa,//tạm lấy ngày y lệnh
                                 dv.BHTT,
                                 vpct.TyLeTT,
                                 vpct.TyLeBHTT,
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
                                 MaQD = dv.MaQD ?? "",
                                 dv.TenRG,
                                 dv.TenDV,
                                 dv.SoQD,
                                 dv.TenHC,
                                 dv.DonVi,
                                 dv.HamLuong,
                                 dv.MaDuongDung,
                                 dv.SoDK,
                                 dv.PLoai,

                             }).ToList();

                var phauthuat_thuthuat = (from a in qvpct where a.IDNhom == 8 select new { a.MaDV, a.MaQD }).ToList();

                #endregion
                #region get DS Dịch vụ
                if (bn != null && rv != null && vp != null)//kiểm tra các trường dữ liệu 
                {
                    int kt = 0;
                    foreach (var a in qvpct)
                    {
                        chi_tiet_dvkt moi = new chi_tiet_dvkt();
                        stt++;

                        moi.ma_lk = bn.MaBNhan.ToString();
                        moi.stt = stt.ToString();
                        if (String.IsNullOrEmpty(a.MaQD))
                        {
                            checkData += a.TenDV + "; ";
                            kt++;
                        }
                        moi.ma_dich_vu = a.MaQD == null ? "" : a.MaQD;
                        moi.ma_vat_tu = "";
                        if (a.IDNhom == 10 || a.IDNhom == 11)
                        {

                            if (a.TrongBH == 1)
                            {
                                moi.ma_dich_vu = "";
                                moi.ma_vat_tu = a.MaQD == null ? "" : a.MaQD; //?????? mã vật tư sử dụng quy định tại bộ mã danh mục dùng chung của bộ y tế, chỉ ghi các vật tư chưa có trong cơ cấu giá dịch vụ ??????
                            }
                            else
                            {
                                // lấy mã của phẫu thuật
                                if (phauthuat_thuthuat.Count > 0)
                                    moi.ma_dich_vu = phauthuat_thuthuat.First().MaQD;
                            }
                        }

                        moi.ma_nhom = a.IDNhom.ToString();
                        moi.ten_dich_vu = (a.PLoai == 1 && DungChung.Bien.MaBV != "30007") ? a.TenHC : a.TenDV; //????? Tên ghi đúng theo danh mục thuốc cục qld công bố
                        moi.don_vi_tinh = a.DonVi;
                        double soluong = 0, dongia = 0;
                        soluong = (Math.Round(a.SoLuong, 2));
                        dongia = (Math.Round(a.DonGia, 2));
                        double tyleBHtt = a.TyLeBHTT / 100;
                        double tylett = a.TyLeTT / 100;
                        moi.so_luong = (soluong).ToString("G", CultureInfo.InvariantCulture);
                        moi.don_gia = (dongia).ToString("G", CultureInfo.InvariantCulture);
                        moi.tyle_tt = a.TyLeTT == 0 ? "100" : a.TyLeTT.ToString();
                        moi.thanh_tien = (soluong * dongia * tylett).ToString("G", CultureInfo.InvariantCulture); //a.ThanhTien.ToString("G", CultureInfo.InvariantCulture);
                        string makhoa = getMaKhoa_QD(a.MaKP);// mã kpQD
                        if (String.IsNullOrEmpty(makhoa))
                        {
                            checkData += a.TenDV + "; ";
                            kt++;
                        }
                        moi.ma_khoa = makhoa;// mã kpQD
                        moi.ma_bac_si = getMaBacSy(bn.MaBNhan, a.MaDV, a.MaKP);
                        moi.ma_benh = rv.MaICD;
                        //moi.ngay_yl = getNgayYLenh_KetQua(bn.MaBNhan, a.MaDV, a.MaKP)[0];
                        //moi.ngay_kq = getNgayYLenh_KetQua(bn.MaBNhan, a.MaDV, a.MaKP)[1];
                        moi.ngay_yl = a.NgayYLenh.Value.ToString("yyyyMMddHHmm");
                        moi.ngay_kq = a.NgayChiPhi.ToString("yyyyMMddHHmm");
                        moi.ma_pttt = "1";// Chưa có dữ liệu để lấy nên fix cứng
                        listDV.Add(moi);
                    }
                    if (kt > 0)
                    {
                        checkData += " chưa có mã dùng chung hoặc mã khoa";
                        return null;
                    }
                }
                #endregion
                #region get XML
                if (listDV.Count > 0)
                {
                    var xEle1 = new XElement("DSACH_CHI_TIET_DVKT",
                                                         from item3 in listDV
                                                         select
                                                    new XElement("CHI_TIET_DVKT",
                                                          new XElement("MA_LK", item3.ma_lk),
                                                          new XElement("STT", item3.stt),
                                                          new XElement("MA_DICH_VU", item3.ma_dich_vu),
                                                          new XElement("MA_VAT_TU", item3.ma_vat_tu),
                                                          new XElement("MA_NHOM", item3.ma_nhom),
                                                          new XElement("TEN_DICH_VU", new XCData(item3.ten_dich_vu == null ? "" : item3.ten_dich_vu)),
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
                                                          ));
                    return xEle1;
                }
                else
                    return null;
                #endregion
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML3: " + ex.Message);
                return null;
            }
        }
        private XElement getChitietDvkt_4210(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp, List<VienPhict> lvpct, ref tongtienXML2_XML3 _tongtienXML2_XML3)
        {
            try
            {
                #region lấy ra thoong tin chung
                stt = 0;
                List<chi_tiet_dvkt> listDV = new List<chi_tiet_dvkt>();
                var qvpct1 = (from vpct in data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi && p.TrongBH == 1) // lấy dv chạy thận (ID nhóm = 8 )
                              join dv in data.DichVus.Where(p => p.TenDV.Contains("Thận nhân tạo thường qui")) on vpct.MaDV equals dv.MaDV
                              select new
                              {
                                  dv.TenDV,
                              }).ToList();
                var qvpct = (
                             from vpct in data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi && p.TrongBH == 1)
                             join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                             join cb in data.CanBoes on vpct.MaCB equals cb.MaCB into kq
                             from kq1 in kq.DefaultIfEmpty()
                             where (dv.PLoai == 2 || dv.PLoai == 3 || (dv.PLoai == 1 && (n.IDNhom == 10 || n.IDNhom == 11)))
                             select new
                             {
                                 dv.MaNhom5937,
                                 vpct.NgayChiPhi,
                                 vpct.NgayYLenh,
                                 vp.NgayRa,//tạm lấy ngày y lệnh
                                 dv.BHTT,
                                 vpct.TyLeTT,
                                 vpct.TyLeBHTT,
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
                                 MaQD = dv.MaQD ?? "",
                                 dv.TenRG,
                                 dv.TenDV,
                                 dv.SoQD,
                                 dv.TenHC,
                                 dv.DonVi,
                                 dv.HamLuong,
                                 dv.MaDuongDung,
                                 dv.SoDK,
                                 dv.PLoai,
                                 dv.NgayQD,
                                 dv.MaNhom,
                                 dv.NhomThau,
                                 dv.GiaBHGioiHanTT,
                                 MaCB = kq1 == null ? "" : kq1.MaCCHN

                             }).OrderBy(p => p.NgayYLenh).ToList();

                var phauthuat_thuthuat = (from a in qvpct where a.MaNhom5937 == 8 select new { a.MaDV, a.MaQD, a.MaNhom }).ToList();

                _tongtienXML2_XML3.MA_PTTT_QT = string.Join(";", phauthuat_thuthuat.Where(p => p.MaNhom != null && p.MaNhom != "").Select(p => p.MaNhom).ToArray());

                #endregion
                string malydo = "";
                if (bn.CapCuu == 1)
                {
                    malydo = "2";
                }
                else
                {
                    if (bn.Tuyen == 1)
                    {
                        malydo = checkThongTuyen(bn.MaKCB, bn.MaCS).ToString();
                    }
                    else
                    {
                        malydo = "3";
                    }
                }
                #region get DS Dịch vụ
                if (bn != null && rv != null && vp != null)//kiểm tra các trường dữ liệu 
                {
                    int kt = 0;
                    int ktMaBS = 0;
                    string dichvuChuaCoMaBS = "";
                    int ktgiuong = 0, ktragiuongnew = 0;
                    string muchuong = "", _khuvuc = "";
                    int hangbv = 0, _noitru = 0;
                    _khuvuc = bn.KhuVuc;
                    _noitru = bn.NoiTru ?? 0;
                    hangbv = DungChung.Ham.hangBV(QLBV.DungChung.Bien.MaBV);

                    var qVTYT0 = (from dt in data.DThuocs.Where(p => p.MaBNhan == bn.MaBNhan)
                                  join dtct in data.DThuoccts.Where(p => p.AttachIDDonct != null && p.AttachIDDonct > 0) on dt.IDDon equals dtct.IDDon
                                  select dtct).ToList();
                    var qVTYT1 = (from a in qVTYT0 group a by new { a.AttachIDDonct } into kq select new { kq.Key.AttachIDDonct }).OrderBy(p => p.AttachIDDonct).ToList();
                    int num = 1;
                    var qVTYT2 = (from a in qVTYT1 select new { STT = num++, a.AttachIDDonct }).ToList();
                    int ktthau = 0;
                    string thongtinthau = "";
                    var bvBD = data.BenhViens.FirstOrDefault(o => o.MaBV == bn.MaCS);
                    var bv = data.BenhViens.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                    foreach (var a in qvpct)
                    {
                        if (bn.NoiTru == null || bn.Tuyen == null)
                            muchuong = "0";
                        else
                        {
                            var qtylett = a.TyLeBHTT;
                            if (qtylett > 0)
                                muchuong = qtylett.ToString();
                            else
                            {

                                muchuong = _getmuc(hangbv, bn.SThe, bn.Tuyen.Value, bn.NoiTru.Value, vp.NgayTT.Value).ToString();
                            }
                        }
                        chi_tiet_dvkt moi = new chi_tiet_dvkt();
                        stt++;

                        moi.ma_lk = bn.MaBNhan.ToString();
                        moi.stt = stt.ToString();
                        if (String.IsNullOrEmpty(a.MaQD))
                        {
                            checkData += a.TenDV + "; ";
                            kt++;
                        }
                        moi.ma_dich_vu = a.MaQD == null ? "" : a.MaQD;
                        moi.ma_vat_tu = "";

                        moi.ten_dich_vu = (a.PLoai == 1 && DungChung.Bien.MaBV != "30007") ? a.TenHC : a.TenDV; //????? Tên ghi đúng theo danh mục thuốc cục qld công bố
                        moi.ten_vat_tu = "";

                        if (a.MaNhom5937 == 10 || a.MaNhom5937 == 11)
                        {

                            if (a.TrongBH == 1)
                            {
                                moi.ma_dich_vu = "";
                                moi.ma_vat_tu = a.MaQD == null ? "" : a.MaQD; //?????? mã vật tư sử dụng quy định tại bộ mã danh mục dùng chung của bộ y tế, chỉ ghi các vật tư chưa có trong cơ cấu giá dịch vụ ??????

                                moi.ten_dich_vu = "";
                                moi.ten_vat_tu = a.TenDV;
                            }
                            else
                            {
                                // lấy mã của phẫu thuật
                                if (phauthuat_thuthuat.Count > 0)
                                    moi.ma_dich_vu = phauthuat_thuthuat.First().MaQD;
                            }

                            if (qVTYT2.Count > 0)
                            {
                                var qVTYT3 = (from dt in qVTYT0.Where(p => p.MaDV == a.MaDV)
                                              join iddon in qVTYT2 on dt.AttachIDDonct equals iddon.AttachIDDonct
                                              group iddon by new { iddon.STT } into kq
                                              select new { GoiVTYT = "G" + kq.Key.STT }).OrderBy(p => p.GoiVTYT).ToList();

                                moi.goi_VTYT = string.Join(";", qVTYT3.Select(p => p.GoiVTYT).ToArray());//Lấy ra gói vật tư y tế (vật tư y tế dính kem, được kê cho các dịch vụ có iddonct là (attackIddonct)
                            }
                            string _manhom = "", _soqd = "";
                            if (!string.IsNullOrEmpty(a.MaNhom) && a.MaNhom.Trim().Length <= 2)
                            {
                                if (getMaNhom(a.MaNhom))
                                    _manhom = a.MaNhom;
                            }
                            if (!string.IsNullOrEmpty(a.SoQD))
                                _soqd = a.SoQD;
                            if (a.NgayQD != null && _manhom != "" && _soqd != "")
                            {
                                moi.tt_thau = a.NgayQD.Value.Year.ToString() + "." + _manhom + "." + _soqd;

                            }
                            else
                                moi.tt_thau = "";
                            if (moi.tt_thau == "")
                            {
                                thongtinthau += a.TenDV + ", ";
                                ktthau++;
                            }
                        }
                        else
                            moi.tt_thau = "";

                        if (a.IdTieuNhom == 22 && a.MaNhom5937 == 80000000000)
                        {
                            moi.ma_nhom = "18";
                        }
                        else
                        {
                            moi.ma_nhom = a.MaNhom5937.ToString();
                        }

                        moi.don_vi_tinh = a.DonVi;
                        moi.pham_vi = "1";
                        double tyleBHtt = a.TyLeBHTT / 100;
                        double tylett = a.TyLeTT / 100;
                        double soluong = 0, dongia = 0, thanhtien = 0, bhtt = 0, bntt = 0, bncct = 0;
                        soluong = Math.Round(a.SoLuong, 2);
                        dongia = Math.Round(a.DonGia, 3);
                        if (a.MaNhom5937 == 13 || a.MaNhom5937 == 15 || a.MaNhom5937 == 8)
                        {
                            thanhtien = Round_custom(Math.Round(soluong * dongia * tylett, 4)); //Round_custom(Math.Round(soluong * dongia * tylett, 4));
                            if (a.TrongBH == 1)
                            {
                                bhtt = Round_custom(Math.Round((thanhtien * tyleBHtt), 4)); //làm tròn đến số thập phân thứ 2 VD 0,5=>1
                                if (malydo == "3")
                                {
                                    if ((!string.IsNullOrWhiteSpace(bn.SThe) && bvBD != null && DungChung.Bien.MaBV.Substring(0, 2) != bvBD.MaTinh.Trim() && bv != null && bv.TuyenBV != null && bv.TuyenBV.Trim() == "C") || (!string.IsNullOrWhiteSpace(bn.SThe) && bvBD != null && DungChung.Bien.MaBV.Substring(0, 2) == bvBD.MaTinh.Trim() && (bvBD.TuyenBV == "A" || bvBD.TuyenBV == "B")))
                                    {
                                        bncct = Round_custom(Math.Round(thanhtien, 4) - bhtt);
                                    }
                                    else
                                    {
                                        if (_noitru == 1 || _noitru != 1)
                                        {
                                            if (a.NgayChiPhi >= DateTime.Parse("01/01/2021"))
                                                bncct = Round_custom(Math.Round(thanhtien * 100 / 100, 2) - bhtt);
                                            else
                                            {
                                                if (_khuvuc != null && _khuvuc.ToLower().Contains("k"))
                                                {
                                                    bncct = Round_custom(Math.Round(thanhtien * 100 / 100, 2) - bhtt);
                                                }
                                                else
                                                {
                                                    if (hangbv == 1)
                                                    {
                                                        bncct = Round_custom(Math.Round(thanhtien * 40 / 100, 2) - bhtt);
                                                    }
                                                    else if (hangbv == 2)
                                                    {
                                                        bncct = Round_custom(Math.Round(thanhtien * 60 / 100, 2) - bhtt);
                                                    }
                                                    else
                                                    {
                                                        bncct = 0;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (DungChung.Bien.MaBV == "30303")
                                                bncct = Round_custom(Math.Round(thanhtien, 2) - bhtt);
                                            else
                                                bncct = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    bncct = Round_custom(Math.Round(thanhtien, 2) - bhtt);
                                }
                                bntt = Round_custom(thanhtien - bhtt - bncct);
                            }
                            else
                            {
                                bntt = thanhtien;
                            }
                        }
                        else
                        {
                            thanhtien = Round_custom(Math.Round(soluong * dongia * tylett, 4));
                            if (a.TrongBH == 1)
                            {
                                if (DungChung.Bien.MaBV == "27022" && a.GiaBHGioiHanTT > 0)
                                {
                                    double ttbh = Round_custom(Math.Round(soluong * a.GiaBHGioiHanTT, 4));
                                    bhtt = Round_custom(Math.Round((ttbh * tyleBHtt * tylett), 4)); //làm tròn đến số thập phân thứ 2 VD 0,5=>1
                                    if (malydo == "3")
                                    {
                                        if (_noitru == 1)
                                        {
                                            if (a.NgayChiPhi >= DateTime.Parse("01/01/2021"))
                                                bncct = Round_custom(Math.Round(ttbh * tylett * 100 / 100, 2) - bhtt);
                                            else
                                            {
                                                if (_khuvuc != null && _khuvuc.ToLower().Contains("k"))
                                                {
                                                    bncct = Round_custom(Math.Round(ttbh * tylett * 100 / 100, 2) - bhtt);
                                                }
                                                else
                                                {
                                                    if (hangbv == 1)
                                                    {
                                                        bncct = Round_custom(Math.Round(ttbh * tylett * 40 / 100, 2) - bhtt);
                                                    }
                                                    else if (hangbv == 2)
                                                    {
                                                        bncct = Round_custom(Math.Round(ttbh * tylett * 60 / 100, 2) - bhtt);
                                                    }
                                                    else
                                                    {
                                                        bncct = 0;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bncct = 0;
                                        }

                                    }

                                    else
                                    {
                                        bncct = Round_custom(Math.Round(ttbh * tylett, 2) - bhtt);
                                    }
                                    bntt = Round_custom(thanhtien - bhtt - bncct);
                                }
                                else
                                {
                                    bhtt = Round_custom(Math.Round((thanhtien * tyleBHtt), 4)); //làm tròn đến số thập phân thứ 2 VD 0,5=>1
                                    if (malydo == "3")
                                    {
                                        if ((!string.IsNullOrWhiteSpace(bn.SThe) && bvBD != null && DungChung.Bien.MaBV.Substring(0, 2) != bvBD.MaTinh.Trim() && bv != null && bv.TuyenBV != null && bv.TuyenBV.Trim() == "C") || (!string.IsNullOrWhiteSpace(bn.SThe) && bvBD != null && DungChung.Bien.MaBV.Substring(0, 2) == bvBD.MaTinh.Trim() && (bvBD.TuyenBV == "A" || bvBD.TuyenBV == "B")))
                                        {
                                            bncct = Round_custom(Math.Round(thanhtien * 100 / 100, 4) - bhtt);
                                        }
                                        else
                                        {
                                            if (_noitru == 1 || _noitru != 1)
                                            {
                                                if (a.NgayChiPhi >= DateTime.Parse("01/01/2021"))
                                                    bncct = Round_custom(Math.Round(thanhtien * 100 / 100, 2) - bhtt);
                                                else
                                                {
                                                    if (_khuvuc != null && _khuvuc.ToLower().Contains("k"))
                                                    {
                                                        bncct = Round_custom(Math.Round(thanhtien * 100 / 100, 2) - bhtt);
                                                    }
                                                    else
                                                    {
                                                        if (hangbv == 1)
                                                        {
                                                            bncct = Round_custom(Math.Round(thanhtien * 40 / 100, 2) - bhtt);
                                                        }
                                                        else if (hangbv == 2)
                                                        {
                                                            bncct = Round_custom(Math.Round(thanhtien * 60 / 100, 2) - bhtt);
                                                        }
                                                        else
                                                        {
                                                            bncct = 0;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bncct = 0;
                                            }
                                        }
                                    }

                                    else
                                    {
                                        bncct = Round_custom(Math.Round(thanhtien, 2) - bhtt);
                                    }
                                    bntt = Round_custom(thanhtien - bhtt - bncct); //Math.Round(thanhtien - bhtt - bncct, 2);
                                }
                            }
                            else
                            {
                                bntt = thanhtien;
                            }
                        }
                        // 
                        if (a.MaNhom5937 == 10 || a.MaNhom5937 == 11)
                        {
                            _tongtienXML2_XML3.T_VTYT += thanhtien;
                        }

                        _tongtienXML2_XML3.T_TONGCHI += thanhtien;
                        _tongtienXML2_XML3.T_BNTT += bntt;
                        _tongtienXML2_XML3.T_BHTT += bhtt;
                        _tongtienXML2_XML3.T_BNCCT += bncct;
                        //_tongtienXML2_XML3.T_NGOAIDS += 0;
                        //
                        moi.so_luong = (soluong).ToString("G", CultureInfo.InvariantCulture);
                        moi.don_gia = (dongia).ToString("G", CultureInfo.InvariantCulture);

                        moi.tt_thau = (a.NgayQD == null ? "" : a.NgayQD.Value.Year.ToString()) + "." + a.MaNhom + "." + a.SoQD;
                        if (a.NgayQD == null || string.IsNullOrEmpty(a.MaNhom) || string.IsNullOrEmpty(a.SoQD))
                        {
                            moi.tt_thau = "";
                            if ((a.MaNhom5937 == 10 || a.MaNhom5937 == 11) && moi.tt_thau == "")
                            {
                                thongtinthau += a.TenDV + ", ";
                                ktthau++;
                            }
                        }
                        moi.tyle_tt = a.TyLeTT == 0 ? "100" : a.TyLeTT.ToString();
                        moi.thanh_tien = (thanhtien).ToString("G", CultureInfo.InvariantCulture);
                        moi.t_tranTT = DungChung.Bien.MaBV == "27022" ? (a.GiaBHGioiHanTT).ToString("G", CultureInfo.InvariantCulture) : "0";
                        moi.muc_huong = muchuong;
                        moi.t_nguonkhac = "0";//4210
                        moi.t_bntt = (bntt).ToString("G", CultureInfo.InvariantCulture);
                        moi.t_bhtt = (bhtt).ToString("G", CultureInfo.InvariantCulture);//4210
                        moi.t_bncct = (bncct).ToString("G", CultureInfo.InvariantCulture);//4210 

                        if (rv.MaICD.Contains("C0") || rv.MaICD.Contains("D0"))
                        {
                            moi.t_ngoaids = "0";
                            //moi.t_ngoaids = a.ThanhTien.ToString();
                        }
                        else if (a.TenDV.Contains("Thận nhân tạo thường qui") || a.TenDV.Contains("Lọc màng bụng cấp cứu liên tục") || a.TenDV.Contains("Lọc màng bụng chu kỳ"))
                        {
                            moi.t_ngoaids = "0";
                            //moi.t_ngoaids = a.ThanhTien.ToString();
                        }
                        else if ((bn.SThe.Substring(0, 2)) == "QN" || (bn.SThe.Substring(0, 2)) == "CA" || (bn.SThe.Substring(0, 2)) == "CY")
                        {
                            moi.t_ngoaids = "0";
                            //moi.t_ngoaids = a.ThanhTien.ToString();
                        }
                        else if (a.TenNhomCT == "Vận chuyển")
                        {
                            moi.t_ngoaids = "0";
                            //moi.t_ngoaids = a.ThanhTien.ToString();
                        }
                        else if (qvpct1.Count > 0)
                        {
                            moi.t_ngoaids = "0";
                            //moi.t_ngoaids = a.ThanhTien.ToString();
                        }
                        string makhoa = getMaKhoa_QD(a.MaKP);// mã kpQD
                        if (String.IsNullOrEmpty(makhoa))
                        {
                            checkData += a.TenDV + "; ";
                            kt++;
                        }
                        moi.ma_khoa = makhoa;
                        var qbnkb = data.BNKBs.Where(p => p.MaBNhan == bn.MaBNhan && p.Giuong != null && p.Giuong != "").Select(p => p.Giuong).ToList();
                        string giuong = string.Join(";", qbnkb);
                        if (giuong.Length > 14)
                        {
                            ktgiuong++;
                        }
                        else
                            moi.ma_giuong = giuong; // Lấy mã giường trong bảng bn khám bệnh
                        if (bn.NoiTru == 1 && string.IsNullOrEmpty(giuong))
                        {
                            ktragiuongnew++;
                        }
                        moi.ma_bac_si = a.MaCB;
                        if (string.IsNullOrEmpty(moi.ma_bac_si))
                            moi.ma_bac_si = getMaBacSy(bn.MaBNhan, a.MaDV, a.MaKP);
                        if (moi.ma_bac_si == "")
                        {
                            dichvuChuaCoMaBS += a.TenDV + "; ";
                            ktMaBS++;
                        }
                        moi.ma_benh = DungChung.Ham.FreshString(rv.MaICD.Trim());
                        moi.ngay_yl = a.NgayYLenh.Value.ToString("yyyyMMddHHmm");
                        moi.ngay_kq = a.NgayChiPhi.ToString("yyyyMMddHHmm");
                        moi.ma_pttt = "1";
                        listDV.Add(moi);
                    }
                    if (kt > 0)
                    {
                        checkData += " chưa có mã dùng chung hoặc mã khoa. ";
                        return null;
                    }
                    if (ktMaBS > 0)
                    {
                        checkData += dichvuChuaCoMaBS + " chưa có mã bác sỹ. ";
                        return null;
                    }
                    if (ktthau > 0)
                    {
                        checkData += thongtinthau + " chưa có đầy đủ hoặc sai thông tin thầu. ";
                        return null;
                    }
                    if (ktgiuong > 0)
                    {
                        checkData += " MA_GIUONG nhiều hơn 15 kí tự";
                        return null;
                    }
                    if (ktragiuongnew > 0)
                    {
                        checkData += " MA_GIUONG không được để trống";
                        return null;
                    }

                }
                #endregion
                #region get XML
                if (listDV.Count > 0)
                {
                    var xEle1 = new XElement("DSACH_CHI_TIET_DVKT",
                                                         from item3 in listDV
                                                         select
                                                    new XElement("CHI_TIET_DVKT",
                                                          new XElement("MA_LK", item3.ma_lk),
                                                          new XElement("STT", item3.stt),
                                                          new XElement("MA_DICH_VU", item3.ma_dich_vu),
                                                          new XElement("MA_VAT_TU", item3.ma_vat_tu),
                                                          new XElement("MA_NHOM", item3.ma_nhom),
                                                          new XElement("GOI_VTYT", item3.goi_VTYT),// GHI MÃ gói VTYT trong 1 lần sử dụng dịch vụ kỹ thuật (lần thứ nhất ghi G1, lần thứ 2 ghi G2...)
                                                          new XElement("TEN_VAT_TU", new XCData(item3.ten_vat_tu == null ? "" : item3.ten_vat_tu)),
                                                          new XElement("TEN_DICH_VU", new XCData(item3.ten_dich_vu == null ? "" : item3.ten_dich_vu)),
                                                          new XElement("DON_VI_TINH", item3.don_vi_tinh),
                                                          new XElement("PHAM_VI", item3.pham_vi),// 1: dv trong phạm vi hưởng BHYT (trong danh mục thuốc do quỹ BHYT chi trả); 2: ngoài phạm vi hưởng BHYT (ngoài danh mục thuốc do quỹ BHYT chi trả)
                                                          new XElement("SO_LUONG", item3.so_luong),
                                                          new XElement("DON_GIA", item3.don_gia),
                                                          new XElement("TT_THAU", item3.tt_thau),//4210
                                                          new XElement("TYLE_TT", item3.tyle_tt),
                                                          new XElement("THANH_TIEN", item3.thanh_tien),
                                                          new XElement("T_TRANTT", item3.t_tranTT),//4210
                                                          new XElement("MUC_HUONG", item3.muc_huong),//4210//thêm ????????????????????????
                                                          new XElement("T_NGUONKHAC", item3.t_nguonkhac),//4210//thêm ????????????????????????
                                                          new XElement("T_BNTT", item3.t_bntt),//4210 thêm ????????????????????????
                                                          new XElement("T_BHTT", item3.t_bhtt),//4210 thêm ????????????????????????
                                                          new XElement("T_BNCCT", item3.t_bncct),//4210 thêm ????????????????????????
                                                          new XElement("T_NGOAIDS", item3.t_ngoaids),//4210 thêm ????????????????????????
                                                          new XElement("MA_KHOA", item3.ma_khoa),
                                                          new XElement("MA_GIUONG", item3.ma_giuong),//4210
                                                          new XElement("MA_BAC_SI", item3.ma_bac_si),
                                                          new XElement("MA_BENH", item3.ma_benh),
                                                          new XElement("NGAY_YL", item3.ngay_yl),
                                                          new XElement("NGAY_KQ", item3.ngay_kq),
                                                          new XElement("MA_PTTT", item3.ma_pttt)
                                                          ));
                    return xEle1;
                }
                else
                    return null;
                #endregion
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML3: " + ex.Message);
                return null;
            }
        }
        private bool getMaNhom(string Manhom)
        {
            bool kt = true;
            foreach (char item in Manhom)
            {
                if (!char.IsDigit(item))
                    kt = false;
            }
            return kt;
        }

        public class DienBienNew
        {
            public string Dien_Bien { get; set; }
            public string Hoi_Chan { get; set; }
            public string Phau_Thuat { get; set; }
            public DateTime Ngay_Y_Lenh { get; set; }
            public string Ten_Benh { get; set; }
        }


        private XElement getDenBien(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp)
        {
            try
            {
                #region lấy thông tin chung
                stt = 0;
                List<chi_tiet_dien_bien_benh> listDienBien = new List<chi_tiet_dien_bien_benh>();
                var _lDienBien = data.DienBiens.Where(p => p.MaBNhan == bn.MaBNhan).Where(p => p.Ploai == 0 || p.Ploai == 99).ToList();
                var _lBBHC = data.BBHCs.Where(p => p.MaBNhan == bn.MaBNhan).ToList();

                string ten_benh = "";
                if (!string.IsNullOrEmpty(rv.ChanDoan))
                {
                    var chandoan = rv.ChanDoan.Split(';');
                    if (chandoan.Length > 0 && chandoan[0] != null)
                    {
                        ten_benh = chandoan[0].ToString();
                    }
                }

                List<DienBienNew> _lkq = new List<DienBienNew>();
                if (DungChung.Bien.MaBV == "20001")
                {
                    var NgayYLenh = (from a in _lDienBien
                                     group a by new { a.NgayNhap.Value.Date } into kq
                                     select new
                                     {
                                         kq.Key.Date,
                                     }).Distinct().ToList();
                    foreach (var item1 in NgayYLenh)
                    {
                        var a = _lDienBien.Where(p => p.NgayNhap.Value.Date == item1.Date).ToList();
                        if (a.Count > 0)
                        {
                            string db = "";
                            DienBienNew moi = new DienBienNew();
                            moi.Hoi_Chan = "";
                            moi.Phau_Thuat = "";
                            moi.Ngay_Y_Lenh = item1.Date;
                            foreach (var item in a)
                            {
                                db += item.DienBien1 + ";";
                            }
                            moi.Dien_Bien = DungChung.Ham.FreshString(db);
                            _lkq.Add(moi);
                        }
                    }
                }
                else if (bn.NoiTru == 0 && !bn.DTNT)
                {
                    DienBienNew moi = new DienBienNew();
                    moi.Hoi_Chan = "";
                    moi.Phau_Thuat = "";
                    var bnkb = data.BNKBs.Where(p => p.MaBNhan == bn.MaBNhan).ToList();
                    foreach (var item in bnkb)
                    {
                        string db = "";
                        DienBienNew a = new DienBienNew();
                        a.Hoi_Chan = "";
                        a.Phau_Thuat = "";
                        a.Ngay_Y_Lenh = item.NgayKham ?? DateTime.Now;
                        a.Dien_Bien = DungChung.Ham.FreshString(item.ChanDoanBD);
                        _lkq.Add(a);
                    }
                }
                else
                {
                    foreach (var item in _lDienBien)
                    {
                        DienBienNew moi = new DienBienNew();
                        moi.Dien_Bien = item.DienBien1;
                        moi.Hoi_Chan = "";
                        moi.Phau_Thuat = "";
                        moi.Ngay_Y_Lenh = Convert.ToDateTime(item.NgayNhap);
                        _lkq.Add(moi);
                    }
                }
                foreach (var item in _lBBHC)
                {
                    DienBienNew moi = new DienBienNew();
                    moi.Dien_Bien = item.QTDBDT;
                    moi.Hoi_Chan = item.KetLuan;
                    moi.Phau_Thuat = item.PPPhauThuat;
                    moi.Ngay_Y_Lenh = Convert.ToDateTime(item.NgayHC);
                    _lkq.Add(moi);
                }

                //var _LKQ=from a in _lDienBien
                //         join b in _lBBHC on a.MaBNhan equals b.MaBNhan
                //         group new {a,b} by new {a.MaBNhan,a.NgayNhap,b.NgayHC}
                #endregion
                #region get DS Diễn biến
                if (bn != null && rv != null && vp != null)//kiểm tra các trường dữ liệu 
                {
                    // var qdv = data.DichVus.ToList();
                    foreach (var a in _lkq.OrderByDescending(p => p.Ngay_Y_Lenh))
                    {
                        stt++;
                        chi_tiet_dien_bien_benh moi = new chi_tiet_dien_bien_benh();
                        moi.ma_lk = bn.MaBNhan.ToString();
                        moi.stt = stt.ToString();
                        if (DungChung.Bien.MaBV == "24272")
                        {
                            moi.dien_bien = ten_benh + "; " + a.Dien_Bien;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(a.Dien_Bien))
                            {
                                //kt++;
                                //Ngayylenhsai += a.Ngay_Y_Lenh.ToShortDateString() + ";";
                                moi.dien_bien = ";";
                                if ((DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "27001") && bn.NoiTru == 0 && bn.DTNT == false && stt == 1)
                                {
                                    moi.dien_bien = ";" + " " + ten_benh;
                                }
                            }
                            else if ((DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "27001") && bn.NoiTru == 0 && bn.DTNT == false)
                            {
                                moi.dien_bien = a.Dien_Bien + "\n" + ten_benh;
                            }
                            else
                                moi.dien_bien = a.Dien_Bien;
                        }
                        moi.hoi_chan = a.Hoi_Chan;
                        moi.phau_thuat = a.Phau_Thuat;
                        moi.ngay_yl = a.Ngay_Y_Lenh.ToString("yyyyMMddHHmm");
                        listDienBien.Add(moi);
                    }
                    //if (kt > 0)
                    //{
                    //    checkData += Ngayylenhsai + " chưa có diễn biến";
                    //    return null;
                    //}
                }
                #endregion
                #region get xml Element
                if (listDienBien.Count > 0)
                {
                    var xEle1 = new XElement("DSACH_CHI_TIET_DIEN_BIEN_BENH",
                                                      from item4 in listDienBien
                                                      select new XElement("CHI_TIET_DIEN_BIEN_BENH",
                                                          new XElement("MA_LK", item4.ma_lk),
                                                          new XElement("STT", item4.stt),
                                                          new XElement("DIEN_BIEN", item4.dien_bien),
                                                          new XElement("HOI_CHAN", item4.hoi_chan),
                                                          new XElement("PHAU_THUAT", new XCData(item4.phau_thuat == null ? "" : item4.phau_thuat)),
                                                          new XElement("NGAY_YL", new XCData(item4.ngay_yl))
                                                          ));
                    return xEle1;
                }
                else
                    return null;

                #endregion
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML5: " + ex.Message);
                return null;
            }
        }
        private XElement getCLS(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp)
        {
            try
            {
                #region lấy thông tin chung
                stt = 0;
                List<chi_tiet_cls> listcls = new List<chi_tiet_cls>();
                var _ldv = (from dv in data.DichVus
                            join dvct in data.DichVucts on dv.MaDV equals dvct.MaDV
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join nhom in data.NhomDVs on tn.IDNhom equals nhom.IDNhom
                            select new { dv.MaDV, tn.TenRG, nhom.TenNhom, nhom.TenNhomCT, dv.MaQD, dvct.MaDVct, dvct.TenDVct, dv.TenDV }).ToList();
                var _lcls = (from cls in data.CLS.Where(p => p.MaBNhan == bn.MaBNhan)
                             join cd in data.ChiDinhs.Where(p => p.TrongBH == 1 && p.Status == 1) on cls.IdCLS equals cd.IdCLS
                             join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                             select new { cd.MaDV, cls.NgayTH, cd.MaMay, cd.IDCD, cd.KetLuan, clsct.KetQua, clsct.MaDVct }).ToList();
                var qcls = (from dv in _ldv
                            join cls in _lcls on dv.MaDVct equals cls.MaDVct
                            select new
                            {
                                dv.MaQD,
                                dv.MaDVct,
                                dv.TenDVct,
                                dv.TenNhom,
                                cls.KetLuan,
                                cls.KetQua,
                                cls.MaMay,
                                cls.NgayTH,
                                dv.TenDV
                            }).OrderByDescending(p => p.NgayTH).Distinct().ToList();

                #endregion
                #region get DS CLS
                if (bn != null && rv != null && vp != null)//kiểm tra các trường dữ liệu 
                {
                    int ktmaqd = 0, ktmamay = 0, ktngayth = 0;
                    string ktmaqds = "", ktmamays = "", ktngayths = "";
                    // var qdv = data.DichVus.ToList();
                    foreach (var a in qcls)
                    {
                        stt++;
                        chi_tiet_cls moi = new chi_tiet_cls();
                        moi.ma_lk = bn.MaBNhan.ToString();
                        moi.stt = stt.ToString();
                        if (String.IsNullOrEmpty(a.MaQD))
                        {
                            ktmaqds += a.TenDV + "; ";
                            ktmaqd++;
                        }
                        else
                        {
                            moi.ma_dich_vu = a.MaQD;
                        }
                        //if (a.TenNhom.ToLower().Contains("phẫu thuật") || a.TenNhom.ToLower().Contains("thủ thuật"))
                        //{
                        //    moi.ma_may = "";
                        //}
                        //else
                        //{
                        //    if (string.IsNullOrEmpty(a.MaMay))
                        //    {
                        //        ktmamay++;
                        //        ktmamays += a.TenDV + "; ";
                        //    }
                        //    else
                        //        moi.ma_may = a.MaMay;
                        //}
                        moi.ma_may = a.MaMay;
                        moi.ma_chi_so = a.MaDVct;
                        moi.ten_chi_so = a.TenDVct;
                        if (a.NgayTH != null)
                            moi.ngay_kq = a.NgayTH.Value.ToString("yyyyMMddHHmm");
                        else
                        {
                            ktngayths = a.TenDV + "; ";
                            ktngayth++;
                        }
                        if (a.TenNhom.Contains("Xét nghiệm"))
                        {

                            moi.gia_tri = a.KetQua;

                            moi.mo_ta = "";
                            moi.ket_luan = "";

                        }
                        else
                        {
                            moi.gia_tri = "";
                            moi.mo_ta = a.KetQua;
                            moi.ket_luan = a.KetLuan;
                        }
                        listcls.Add(moi);
                    }
                    if (ktmaqd > 0)
                    {
                        checkData += ktmaqds + " chưa có mã dùng chung";
                        return null;
                    }
                    //if(ktmamay>0)
                    //{
                    //    checkData += ktmamays + " chưa có mã máy";//tạm bỏ mã máy để ktra lỗi trên cổng nếu ko có mã máy
                    //    return null;
                    //}
                    if (ktngayth > 0)
                    {
                        checkData += ktngayths + " chưa có ngày thực hiện";
                        return null;
                    }
                }
                #endregion
                #region get xml Element
                if (listcls.Count > 0)
                {
                    var xEle1 = new XElement("DSACH_CHI_TIET_CLS",
                                                      from item4 in listcls
                                                      select
                                                      new XElement("CHI_TIET_CLS",
                                                          new XElement("MA_LK", item4.ma_lk),
                                                          new XElement("STT", item4.stt),
                                                          new XElement("MA_DICH_VU", item4.ma_dich_vu),
                                                          new XElement("MA_CHI_SO", item4.ma_chi_so),
                                                          new XElement("TEN_CHI_SO", new XCData(item4.ten_chi_so == null ? "" : item4.ten_chi_so)),
                                                          new XElement("GIA_TRI", new XCData(CutString(item4.gia_tri, 50))),
                                                          new XElement("MA_MAY", item4.ma_may),
                                                          new XElement("MO_TA", new XCData(item4.mo_ta == null ? "" : item4.mo_ta)),
                                                          new XElement("KET_LUAN", new XCData(item4.ket_luan == null ? "" : item4.ket_luan)),
                                                          new XElement("NGAY_KQ", item4.ngay_kq)

                                                          ));
                    return xEle1;
                }
                else
                    return null;

                #endregion
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML4: " + ex.Message);
                return null;
            }
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
                        moi.muc_huong = _getmuc(hangbv, bn.SThe, bn.Tuyen.Value, bn.NoiTru.Value, vp.NgayTT.Value).ToString();
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
            try
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
                    {
                        //if (ths >= 1 && ths <= 12)
                        //    rs = ns.ToString() + ths.ToString("D2");
                        //else
                        rs = ns.ToString();
                    }
                }
                return rs;
            }
            catch
            {
                return namsinh;
            }
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
        /// <summary>
        /// return mảng 2 phần tử
        /// </summary>
        /// <param name="maBenh"></param>
        /// <returns></returns>
        private static string[] GetICD(string maBenh)
        {
            string _strIcd = "";
            string _strIcdKhac = "";
            int index = maBenh.LastIndexOf(';');
            if (maBenh.Length > 1 && maBenh.Length - 1 == index)
                maBenh = maBenh.Substring(0, maBenh.Length - 1);
            if (maBenh != "")
            {
                string[] _arr = maBenh.Split(';');

                for (int i = 0; i < _arr.Length; i++)
                {
                    if (i == 0)
                        _strIcd = _arr[i];
                    else
                    {
                        if (_arr[i].Length > 1)
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
            }
            return new string[] { _strIcd, _strIcdKhac };
        }
        public static double _getmuc(int hangBV, string mathe, int tuyen, int noingoaitru, DateTime ngayTT)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            double _muctt = 0;
            string mamuc = "";

            if (mathe.Length > 2 && ngayTT != null)
            {
                mamuc = mathe.Substring(2, 1);
                var qmuc = data.MucTTs.Where(p => p.MaMuc == mamuc).ToList();
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
                            {
                                switch (hangBV)
                                {
                                    case 3:
                                        tylevuottuyen = 0.7;
                                        break;
                                }
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
                else if (kp.Length < 2)
                    return "";
                else
                    return kp;
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
            var qdtct = (from dthuoc in data.DThuocs.Where(p => p.MaBNhan == maBN)
                         join dtct in data.DThuoccts.Where(p => p.MaDV == maDV) on dthuoc.IDDon equals dtct.IDDon
                         join cb in data.CanBoes on dthuoc.MaCB equals cb.MaCB
                         select new { dthuoc.MaKP, cb.MaCCHN, cb.MaCB }).ToList();

            if (qdtct.Count == 0)
            {
                return "";
            }
            else
            {
                var qdtct1 = (from dt in qdtct.Where(p => p.MaKP == maKP) select dt).FirstOrDefault();// chỗ này cần lấy theo mã số chứng chỉ hành nghề
                if (qdtct1 != null)
                {
                    if (string.IsNullOrEmpty(qdtct1.MaCCHN))
                        return "";
                    else
                        return qdtct1.MaCCHN.ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(qdtct.First().MaCCHN))
                        return "";
                    else
                        return qdtct.First().MaCCHN.ToString();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bang">1: checkin, 2:phieuchuyentuyen, 3:tonghop, 4:chi_tiet_thuoc, 5:chi_tiet_dvkt, 6:chi_tiet_dien_bien_benh, 7:chi_tiet_cls</param>
        /// <param name="data"></param>
        /// <param name="maBN"></param>
        /// <returns></returns>
        private string getByte64File(string path, XElement exle)
        {
            String file = "";
            if (exle != null)
            {
                exle.Save(path);
                Byte[] bytes = File.ReadAllBytes(path);
                file = Convert.ToBase64String(bytes);
            }
            return file;
        }
        /// <summary>
        /// Cắt chuỗ nếu chuỗi ký tự > số ký tự cho trước
        /// </summary>
        /// <param name="kq"></param>
        ///  <param name="so">Số ky tự</param>
        /// <returns></returns>
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


                if (bn.NoiTru == 0)
                {
                    if (rv != null && rv.NgayVao == null)
                    {
                        moi = new ErrMs();
                        moi.ID = bn.MaBNhan.ToString();
                        moi.Table = "RaVien";
                        moi.ColName = "NgayVao";
                        moi.Ms = "Bệnh nhân không có ngày vào viện";
                        _listErr.Add(moi);
                    }
                }
                else // nội trú
                {
                    if (vv != null && vv.NgayVao == null)
                    {
                        moi = new ErrMs();
                        moi.ID = bn.MaBNhan.ToString();
                        moi.Table = "VaoVien";
                        moi.ColName = "NgayVao";
                        moi.Ms = "Bệnh nhân không có ngày vào viện";
                        _listErr.Add(moi);
                    }
                }


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
        private List<ErrMs> ValidateTonghop(BenhNhan bn, RaVien rv, VienPhi vv)
        {
            List<ErrMs> listErr = new List<ErrMs>();
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (bn != null && rv != null)
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
        public bool XuatXML_HSBN(QLBV_Database.QLBVEntities data, List<int> lmaBN, string path, string path9324)
        {
            bool rs = false;
            try
            {

                var xEle1 = new XElement("GIAMDINHHS", new XAttribute("xmlns:xsi", "http://ww.w3.org/2001/XMLSchema-instance"),
                    new XAttribute("xmlns:xsd", "http://ww.w3.org/2001/XMLSchema"),
                                                    new XElement("THONGTINDONVI", new XElement("MACSKCB", DungChung.Bien.MaBV)),
                                                    new XElement("THONGTINHOSO",
                                                        new XElement("NGAYLAP", DateTime.Now.ToString("yyyyMMdd")),//--------------------------------------
                                                        new XElement("SOLUONGHOSO", "1"),
                                                        new XElement("DANHSACHHOSO")),
                                                    new XElement("CHUKYDONVI"));
                int count = 0;
                _listmuc = data.MucTTs.ToList();
                foreach (int maBN in lmaBN)
                {
                    BenhNhan bn = (from bnhan in data.BenhNhans.Where(p => p.MaBNhan == maBN) join rvien in data.RaViens on bnhan.MaBNhan equals rvien.MaBNhan select bnhan).Where(p => p.SThe != null && p.SThe.Length == 15).FirstOrDefault();
                    RaVien rv = data.RaViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
                    TTboXung ttbs = data.TTboXungs.Where(p => p.MaBNhan == maBN).FirstOrDefault();
                    VaoVien vv = data.VaoViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
                    VienPhi vp = data.VienPhis.Where(p => p.MaBNhan == maBN).FirstOrDefault();
                    if (bn != null && rv != null && vp != null)
                    {

                        string namqt = vp.NgayTT.Value.Year.ToString();
                        string thanqt = vp.NgayTT.Value.Month.ToString("D2");
                        string file1, file2, file3, file4, file5;
                        string pathXML1 = path9324 + "\\XML1_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                        string pathXML2 = path9324 + "\\XML2_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                        string pathXML3 = path9324 + "\\XML3_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                        string pathXML4 = path9324 + "\\XML4_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                        string pathXML5 = path9324 + "\\XML5_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                        file1 = getByte64File(pathXML1, gettonghop(data, bn, ttbs, vv, rv, vp));
                        file2 = getByte64File(pathXML2, getChitietThuoc(data, bn, ttbs, vv, rv, vp));
                        file3 = getByte64File(pathXML3, getChitietDvkt(data, bn, ttbs, vv, rv, vp));
                        file4 = getByte64File(pathXML4, getCLS(data, bn, ttbs, vv, rv, vp));
                        file5 = getByte64File(pathXML5, getDenBien(data, bn, ttbs, vv, rv, vp)); // chưa kiểm tra
                        //if(!string.IsNullOrEmpty(checkData))
                        //{

                        //}
                        if (!String.IsNullOrEmpty(file1))
                        {
                            count++;

                            var xEle2 = new XElement("HOSO");
                            xEle2.Add(new XElement("FILEHOSO",
                                new XElement("LOAIHOSO", "XML1"),
                                                                 new XElement("NOIDUNGFILE", file1)));
                            if (!String.IsNullOrEmpty(file2))
                                xEle2.Add(new XElement("FILEHOSO",
                                                                      new XElement("LOAIHOSO", "XML2"),
                                                                      new XElement("NOIDUNGFILE", file2)
                                                                      ));
                            if (!String.IsNullOrEmpty(file3))
                                xEle2.Add(new XElement("FILEHOSO",
                                                                      new XElement("LOAIHOSO", "XML3"),
                                                                      new XElement("NOIDUNGFILE", file3)
                                                                      ));
                            if (!String.IsNullOrEmpty(file4))
                                xEle2.Add(new XElement("FILEHOSO",
                                                                      new XElement("LOAIHOSO", "XML4"),
                                                                      new XElement("NOIDUNGFILE", file4)
                                                                      ));
                            if (!String.IsNullOrEmpty(file5))
                                xEle2.Add(new XElement("FILEHOSO",
                                                                      new XElement("LOAIHOSO", "XML5"),
                                                                      new XElement("NOIDUNGFILE", file5)
                                                                      ));
                            xEle1.Element("THONGTINHOSO").Element("DANHSACHHOSO").Add(xEle2);
                        }
                        rs = true;
                    }
                }
                if (rs == true)
                    xEle1.Save(path + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss_") + count.ToString() + ".xml");
                return rs;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region updateMaGiaoDich
        public static bool updateMaGiaoDich(int maBN, string magiaodich)
        {
            try
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var vp = data.VienPhis.Where(p => p.MaBNhan == maBN).ToList();
                foreach (var item in vp)
                {
                    item.MaGD_BHXH = magiaodich;
                    item.NgayGuiBHXH = DateTime.Now;
                    data.SaveChanges();
                }
                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        #endregion
        #region GetDataToPost
        /// <summary>
        /// Lấy ra dữ liệu file đã chuyển sang dạng base64
        /// </summary>
        /// <param name="data"></param>
        /// <param name="maBN"></param>
        /// <param name="path">Thư mục để lưu các file XML</param>
        /// <param name="user"></param>
        /// <param name="pass">Thư mục chưa file giám định</param>
        /// <param name="path9324">thư mục chưa file theo CV9324 (XML1, XML2...)</param>
        /// <returns></returns>
        /// 
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        string checkData = "";
        public bool XuatXML_HSBN(QLBV_Database.QLBVEntities data, int maBN, string path, string path9324, bool service)
        {
            bool guiHSGD4210 = false;
            bool rs = false;
            checkData = "";

            RaVien rv = data.RaViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            BenhNhan bn = (from bnhan in data.BenhNhans.Where(p => p.MaBNhan == maBN) select bnhan).Where(p => p.SThe != null && p.SThe.Length == 15).FirstOrDefault();
            TTboXung ttbs = data.TTboXungs.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            VaoVien vv = data.VaoViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            VienPhi vp = data.VienPhis.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            int db = data.DienBiens.Where(p => p.MaBNhan == maBN).Count();
            List<CL> cls = data.CLS.Where(p => p.MaBNhan == maBN).ToList();
            List<VienPhict> lvpct = new List<VienPhict>();
            if (vp.NgayTT.Value.Date >= new DateTime(2017, 11, 04))
                guiHSGD4210 = true;
            if (bn != null && rv != null && vp != null)
            {
                try
                {
                    lvpct = data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi).Where(p => p.TrongBH == 1).ToList();
                    if (vp.NgayTT != null && rv.NgayRa != null)
                    {
                        if ((vp.NgayTT.Value - rv.NgayRa.Value).Minutes < 0)
                        {
                            updateMaGiaoDich(maBN, "Lỗi: ngày TT < ngày ra viện");
                            return false;
                        }
                    }
                    else
                    {
                        updateMaGiaoDich(maBN, "Lỗi: không có ngày TT hoặc ngày RV");
                        return false;
                    }
                    if (lvpct.Where(p => p.NgayChiPhi > DateTime.Now).Count() > 0)
                    {
                        updateMaGiaoDich(maBN, "Lỗi: Bệnh nhân có ngày kê hoặc ngày y lệnh lớn hơn ngày giờ hiện tại");
                        return false;

                    }
                    string namqt = vp.NgayTT.Value.Year.ToString();
                    string thanqt = vp.NgayTT.Value.Month.ToString("D2");

                    string file1 = "", file2 = "", file3 = "", file4 = "", file5 = "";
                    string pathXML1 = path9324 + "\\XML1_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML2 = path9324 + "\\XML2_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML3 = path9324 + "\\XML3_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML4 = path9324 + "\\XML4_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML5 = path9324 + "\\XML5_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    try
                    {
                        if (!guiHSGD4210)
                        {
                            file1 = getByte64File(pathXML1, gettonghop(data, bn, ttbs, vv, rv, vp, lvpct));
                            file2 = getByte64File(pathXML2, getChitietThuoc(data, bn, ttbs, vv, rv, vp));
                            file3 = getByte64File(pathXML3, getChitietDvkt(data, bn, ttbs, vv, rv, vp));
                            file4 = getByte64File(pathXML4, getCLS(data, bn, ttbs, vv, rv, vp));
                            file5 = getByte64File(pathXML5, getDenBien(data, bn, ttbs, vv, rv, vp));// chưa kiểm tra
                        }
                        else
                        {
                            tongtienXML2_XML3 _tongtienXML2_XML3 = new tongtienXML2_XML3();

                            file2 = getByte64File(pathXML2, getChitietThuoc_4210(data, bn, ttbs, vv, rv, vp, lvpct, ref _tongtienXML2_XML3));
                            file3 = getByte64File(pathXML3, getChitietDvkt_4210(data, bn, ttbs, vv, rv, vp, lvpct, ref _tongtienXML2_XML3));
                            file1 = getByte64File(pathXML1, gettonghop_4210(data, bn, ttbs, vv, rv, vp, lvpct, _tongtienXML2_XML3));
                            if (cls.Count > 0)
                                file4 = getByte64File(pathXML4, getCLS(data, bn, ttbs, vv, rv, vp));

                            file5 = getByte64File(pathXML5, getDenBien(data, bn, ttbs, vv, rv, vp));
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Lỗi tạo các file chi tiết!" + ex.InnerException + ex.Data + ex.Message);
                    }
                    if (!string.IsNullOrEmpty(checkData))
                    {
                        updateMaGiaoDich(maBN, checkData);
                        return false;
                    }
                    try
                    {
                        if (!String.IsNullOrEmpty(file1))
                        {
                            XElement xEle1;
                            //xEle1 = new XElement(("GIAMDINHHS"), new XAttribute("xmlnsxsi", "http://ww.w3.org/2001/XMLSchema-instance"),
                            //                   new XAttribute("xmlnsxsd", "http://ww.w3.org/2001/XMLSchema"),
                            xEle1 = new XElement(("GIAMDINHHS"),
                                                     new XElement("THONGTINDONVI", new XElement("MACSKCB", DungChung.Bien.MaBV)),
                                                     new XElement("THONGTINHOSO",
                                                         new XElement("NGAYLAP", DateTime.Now.ToString("yyyyMMdd")),//--------------------------------------
                                                         new XElement("SOLUONGHOSO", "1"),
                                                         new XElement("DANHSACHHOSO",
                                                             (new XElement("HOSO",
                                                                   new XElement("FILEHOSO",
                                                                       new XElement("LOAIHOSO", "XML1"),
                                                                       new XElement("NOIDUNGFILE", file1)
                                                                       )
                                                                 )))),

                                                     new XElement("CHUKYDONVI"));

                            if (!String.IsNullOrEmpty(file2))
                                xEle1.Element("THONGTINHOSO").Element("DANHSACHHOSO").Element("HOSO").Add(new XElement("FILEHOSO",
                                                                      new XElement("LOAIHOSO", "XML2"),
                                                                      new XElement("NOIDUNGFILE", file2)
                                                                      ));
                            if (!String.IsNullOrEmpty(file3))
                                xEle1.Element("THONGTINHOSO").Element("DANHSACHHOSO").Element("HOSO").Add(new XElement("FILEHOSO",
                                                                      new XElement("LOAIHOSO", "XML3"),
                                                                      new XElement("NOIDUNGFILE", file3)
                                                                      ));
                            if (!String.IsNullOrEmpty(file4))
                                xEle1.Element("THONGTINHOSO").Element("DANHSACHHOSO").Element("HOSO").Add(new XElement("FILEHOSO",
                                                                      new XElement("LOAIHOSO", "XML4"),
                                                                      new XElement("NOIDUNGFILE", file4)
                                                                      ));
                            if (!String.IsNullOrEmpty(file5))
                                xEle1.Element("THONGTINHOSO").Element("DANHSACHHOSO").Element("HOSO").Add(new XElement("FILEHOSO",
                                                                      new XElement("LOAIHOSO", "XML5"),
                                                                      new XElement("NOIDUNGFILE", file5)
                                                                      ));

                            try
                            {
                                xEle1.Save(path + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + bn.SThe + "_" + namqt + "_" + thanqt + ".xml");
                            }
                            catch
                            {
                                System.Windows.Forms.MessageBox.Show("Lỗi lưu file XML kiểm tra lại đường dẫn lưu file: " + path);
                            }

                            rs = true;
                            //guinhanBHXH.GuiHoSoGiamDinh(frm.user, frm.pass, DungChung.Bien.MaBV, ref filehsDung, ref filehsSai);
                            string filehsDung = ""; // mã giao dịch
                            string filehsSai = ""; // lỗi trả về !string.isnull.. có lỗi
                            if (service)
                            {
                                try
                                {
                                    GiamDinhBHXH.BHXH_Model.Gui_NhanDuLieu_BHXH guinhanBHXH = new GiamDinhBHXH.BHXH_Model.Gui_NhanDuLieu_BHXH();
                                    guinhanBHXH.basePath = path;
                                    BHYT.us_Export_XML_2348 frm = new BHYT.us_Export_XML_2348();
                                    if (guiHSGD4210)
                                        rs = GiamDinhBHXH.BHXH_Model.Gui_NhanDuLieu_BHXH.GuiHoSoGiamDinh4210(DungChung.Bien.xmlFilePath_LIS[10], DungChung.Bien.xmlFilePath_LIS[11], DungChung.Bien.MaBV, xEle1, ref filehsDung, ref filehsSai);
                                    else
                                        rs = GiamDinhBHXH.BHXH_Model.Gui_NhanDuLieu_BHXH.GuiHoSoGiamDinh(DungChung.Bien.xmlFilePath_LIS[10], DungChung.Bien.xmlFilePath_LIS[11], DungChung.Bien.MaBV, xEle1, ref filehsDung, ref filehsSai);
                                    BHYT.us_Export_XML_2348.error += filehsSai + "\n";
                                }
                                catch (Exception ex)
                                {
                                    System.Windows.Forms.MessageBox.Show("Lỗi gửi dữ liệu qua service:" + ex.Message);
                                }
                            }
                            updateMaGiaoDich(maBN, filehsDung + filehsSai);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Lỗi tạo các thẻ" + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML tổng: " + ex.Message, "Lỗi");
                    return false;
                }

            }
            return rs;
        }
        public bool XuatXML(QLBV_Database.QLBVEntities data, int maBN, string path)
        {

            bool rs = false;
            BenhNhan bn = (from bnhan in data.BenhNhans.Where(p => p.MaBNhan == maBN) join rvien in data.RaViens on bnhan.MaBNhan equals rvien.MaBNhan select bnhan).Where(p => p.SThe != null && p.SThe.Length == 15).FirstOrDefault();
            RaVien rv = data.RaViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            TTboXung ttbs = data.TTboXungs.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            VaoVien vv = data.VaoViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            VienPhi vp = data.VienPhis.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            if (bn != null && rv != null && vp != null)
            {
                try
                {
                    string namqt = vp.NgayTT.Value.Year.ToString();
                    string thanqt = vp.NgayTT.Value.Month.ToString("D2");


                    string pathXML1 = path + "\\XML1_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML2 = path + "\\XML2_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML3 = path + "\\XML3_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML4 = path + "\\XML4_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML5 = path + "\\XML5_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    XElement ex1 = gettonghop(data, bn, ttbs, vv, rv, vp);
                    XElement ex2 = getChitietThuoc(data, bn, ttbs, vv, rv, vp);
                    XElement ex3 = getChitietDvkt(data, bn, ttbs, vv, rv, vp);
                    XElement ex4 = getCLS(data, bn, ttbs, vv, rv, vp);
                    XElement ex5 = getDenBien(data, bn, ttbs, vv, rv, vp);
                    if (ex1 != null)
                    {
                        ex1.Save(pathXML1);
                        if (ex2 != null)
                            ex2.Save(pathXML2);
                        if (ex3 != null)
                            ex3.Save(pathXML3);
                        if (ex4 != null)
                            ex4.Save(pathXML4);
                        if (ex5 != null)
                            ex5.Save(pathXML5);
                        rs = true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            return rs;
        }
        public bool GetDataToPost(QLBV_Database.QLBVEntities data, int maBN, string path, string user, string pass)
        {
            string fileExcel = path + "\\" + "Loi_" + DateTime.Now.ToString("yyyyMMddhhmm") + ".xls";

            bool rs = false;
            BenhNhan bn = (from bnhan in data.BenhNhans.Where(p => p.MaBNhan == maBN) join rvien in data.RaViens on bnhan.MaBNhan equals rvien.MaBNhan select bnhan).Where(p => p.SThe != null && p.SThe.Length == 15).FirstOrDefault();
            RaVien rv = data.RaViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            TTboXung ttbs = data.TTboXungs.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            VaoVien vv = data.VaoViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            VienPhi vp = data.VienPhis.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            if (bn != null && rv != null && vp != null)
            {
                try
                {
                    string namqt = vp.NgayTT.Value.Year.ToString();
                    string thanqt = vp.NgayTT.Value.Month.ToString("D2");

                    string file1, file2, file3, file4, file5;
                    string pathXML1 = path + "\\XML1_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML2 = path + "\\XML2_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML3 = path + "\\XML3_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML4 = path + "\\XML4_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML5 = path + "\\XML5_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    file1 = getByte64File(pathXML1, gettonghop(data, bn, ttbs, vv, rv, vp));
                    file2 = getByte64File(pathXML2, getChitietThuoc(data, bn, ttbs, vv, rv, vp));
                    file3 = getByte64File(pathXML3, getChitietDvkt(data, bn, ttbs, vv, rv, vp));
                    file4 = getByte64File(pathXML4, getCLS(data, bn, ttbs, vv, rv, vp));
                    file5 = getByte64File(pathXML5, getDenBien(data, bn, ttbs, vv, rv, vp));
                    PostData(file1, file2, file3, file4, file5, user, pass);

                    rs = true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            return rs;
        }

        private void PostData(string file1, string file2, string file3, string file4, string file5, string user, string pass)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Xuất XML gửi cổng HSSK
        public string XuatXML_4210_HSSK(QLBV_Database.QLBVEntities data, int maBN, string pathPostAPI, string pathXML, BenhNhan bn, RaVien rv, TTboXung ttbs, string PersonID)
        {
            bool rs = false;
            if (!string.IsNullOrEmpty(PersonID))
                PID = PersonID;
            checkData = "";
            VaoVien vv = data.VaoViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            VienPhi vp = data.VienPhis.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            int db = data.DienBiens.Where(p => p.MaBNhan == maBN).Count();
            List<CL> cls = data.CLS.Where(p => p.MaBNhan == maBN).ToList();
            List<VienPhict> lvpct = new List<VienPhict>();
            if (bn != null && rv != null && vp != null)
            {
                try
                {
                    lvpct = data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi).Where(p => p.TrongBH == 1).ToList();
                    if (vp.NgayTT != null && rv.NgayRa != null)
                    {
                        if ((vp.NgayTT.Value - rv.NgayRa.Value).Minutes < 0)
                        {
                            updateMaGiaoDich(maBN, "Lỗi: ngày TT < ngày ra viện");
                            return "";

                        }

                    }
                    else
                    {
                        updateMaGiaoDich(maBN, "Lỗi: không có ngày TT hoặc ngày RV");
                        return "";
                    }
                    if (lvpct.Where(p => p.NgayChiPhi > DateTime.Now).Count() > 0)
                    {
                        updateMaGiaoDich(maBN, "Lỗi: Bệnh nhân có ngày kê hoặc ngày y lệnh lớn hơn ngày giờ hiện tại");
                        return "";

                    }
                    string namqt = vp.NgayTT.Value.Year.ToString();
                    string thanqt = vp.NgayTT.Value.Month.ToString("D2");

                    string file1 = "", file2 = "", file3 = "", file4 = "", file5 = "";
                    string pathXML1 = pathXML + "\\XML1_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML2 = pathXML + "\\XML2_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML3 = pathXML + "\\XML3_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML4 = pathXML + "\\XML4_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    string pathXML5 = pathXML + "\\XML5_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + bn.MaBNhan + ".xml";// tên file
                    try
                    {
                        tongtienXML2_XML3 _tongtienXML2_XML3 = new tongtienXML2_XML3();

                        file1 = getByte64File(pathXML1, gettonghop_4210(data, bn, ttbs, vv, rv, vp, lvpct, _tongtienXML2_XML3));
                        file2 = getByte64File(pathXML2, getChitietThuoc_4210(data, bn, ttbs, vv, rv, vp, lvpct, ref _tongtienXML2_XML3));
                        file3 = getByte64File(pathXML3, getChitietDvkt_4210(data, bn, ttbs, vv, rv, vp, lvpct, ref _tongtienXML2_XML3));

                        if (cls.Count > 0)
                            file4 = getByte64File(pathXML4, getCLS(data, bn, ttbs, vv, rv, vp));
                        if (db > 0 || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
                            file5 = getByte64File(pathXML5, getDenBien(data, bn, ttbs, vv, rv, vp));
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Lỗi tạo các file chi tiết!" + ex.InnerException + ex.Data + ex.Message);

                    }
                    if (!string.IsNullOrEmpty(checkData))
                    {
                        updateMaGiaoDich(maBN, checkData);
                        return "";
                    }
                    try
                    {
                        if (!String.IsNullOrEmpty(file1))
                        {
                            XElement xEle1;
                            //xEle1 = new XElement(("GIAMDINHHS"), new XAttribute("xmlnsxsi", "http://ww.w3.org/2001/XMLSchema-instance"),
                            //                   new XAttribute("xmlnsxsd", "http://ww.w3.org/2001/XMLSchema"),
                            xEle1 = new XElement(("GIAMDINHHS"),
                                                     new XElement("THONGTINDONVI", new XElement("MACSKCB", DungChung.Bien.MaBV)),
                                                     new XElement("THONGTINHOSO",
                                                         new XElement("NGAYLAP", DateTime.Now.ToString("yyyyMMdd")),//--------------------------------------
                                                         new XElement("SOLUONGHOSO", "1"),
                                                         new XElement("DANHSACHHOSO",
                                                             (new XElement("HOSO",
                                                                   new XElement("FILEHOSO",
                                                                       new XElement("LOAIHOSO", "XML1"),
                                                                       new XElement("NOIDUNGFILE", file1)
                                                                       )
                                                                 )))),

                                                     new XElement("CHUKYDONVI"));

                            if (!String.IsNullOrEmpty(file2))
                                xEle1.Element("THONGTINHOSO").Element("DANHSACHHOSO").Element("HOSO").Add(new XElement("FILEHOSO",
                                                                      new XElement("LOAIHOSO", "XML2"),
                                                                      new XElement("NOIDUNGFILE", file2)
                                                                      ));
                            if (!String.IsNullOrEmpty(file3))
                                xEle1.Element("THONGTINHOSO").Element("DANHSACHHOSO").Element("HOSO").Add(new XElement("FILEHOSO",
                                                                      new XElement("LOAIHOSO", "XML3"),
                                                                      new XElement("NOIDUNGFILE", file3)
                                                                      ));
                            if (!String.IsNullOrEmpty(file4))
                                xEle1.Element("THONGTINHOSO").Element("DANHSACHHOSO").Element("HOSO").Add(new XElement("FILEHOSO",
                                                                      new XElement("LOAIHOSO", "XML4"),
                                                                      new XElement("NOIDUNGFILE", file4)
                                                                      ));
                            if (!String.IsNullOrEmpty(file5))
                                xEle1.Element("THONGTINHOSO").Element("DANHSACHHOSO").Element("HOSO").Add(new XElement("FILEHOSO",
                                                                      new XElement("LOAIHOSO", "XML5"),
                                                                      new XElement("NOIDUNGFILE", file5)
                                                                      ));

                            try
                            {
                                string filePath = pathPostAPI + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + bn.SThe + "_" + namqt + "_" + thanqt + ".xml";
                                xEle1.Save(filePath);
                                return filePath;

                            }
                            catch
                            {
                                System.Windows.Forms.MessageBox.Show("Lỗi lưu file XML kiểm tra lại đường dẫn lưu file: " + pathPostAPI);
                                return "";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Lỗi tạo các thẻ" + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML tổng: " + ex.Message, "Lỗi");
                    return "";
                }

            }
            return "";
        }
        #endregion
    }
}
