using System;using QLBV_Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using QLBV;
using System.Globalization;
using COMExcel = Microsoft.Office.Interop.Excel;
namespace Lib_QLBV_Connect_XP
{
    /// <summary>
    /// kết nối XP viện phí _Song Ân 06072016
    /// </summary>

    public class clsKetNoiXP
    {
        public class ThongTinChung
        {
            private string ma_lk;

            public string Ma_lk
            {
                get { return ma_lk; }
                set { ma_lk = value; }
            }
            private string ngaygiovao;

            public string Ngaygiovao
            {
                get { return ngaygiovao; }
                set { ngaygiovao = value; }
            }
            private string ngaygiora;

            public string Ngaygiora
            {
                get { return ngaygiora; }
                set { ngaygiora = value; }
            }
            private string mabenhvien;

            public string Mabenhvien
            {
                get { return mabenhvien; }
                set { mabenhvien = value; }
            }
            private string chandoan;

            public string Chandoan
            {
                get { return chandoan; }
                set { chandoan = value; }
            }
            private string trangthai;

            public string Trangthai
            {
                get { return trangthai; }
                set { trangthai = value; }
            }
            private string ketqua;

            public string Ketqua
            {
                get { return ketqua; }
                set { ketqua = value; }
            }
            private string sodienthoai_lh;

            public string Sodienthoai_lh
            {
                get { return sodienthoai_lh; }
                set { sodienthoai_lh = value; }
            }
            private string nguoilienhe;

            public string Nguoilienhe
            {
                get { return nguoilienhe; }
                set { nguoilienhe = value; }
            }
        }
        public class ThongTinBenhNhan
        {
            private bool check;
            public bool Check
            {
                get { return check; }
                set { check = value; }
            }
            private string ma_lk;
            /// <summary>
            /// mã bệnh nhân
            /// </summary>
            public string Ma_lk
            {
                get { return ma_lk; }
                set { ma_lk = value; }
            }


            private string ma_bn;
            /// <summary>
            /// mã bệnh nhân
            /// </summary>
            public string Ma_bn
            {
                get { return ma_bn; }
                set { ma_bn = value; }
            }
            private string ho_ten;
            public string Ho_ten
            {
                get { return ho_ten; }
                set { ho_ten = value; }
            }

            private string ngay_sinh;
            /// <summary>
            /// 8 ký tự, dạng yyyyMMdd, nếu không có ngày, tháng ghi 4 ký tự năm
            /// </summary>
            public string Ngay_sinh
            {
                get { return ngay_sinh; }
                set { ngay_sinh = value; }
            }

            private int gioi_tinh;
            /// <summary>
            /// Giới tính: 1: nam; 2: nữ
            /// </summary>
            public int Gioi_tinh
            {
                get { return gioi_tinh; }
                set { gioi_tinh = value; }
            }

            private string dia_chi;
            /// <summary>
            /// Địa chỉ ghi trên thẻ BHYT, nếu trẻ em không có thẻ ghi trên giấy tờ thay thế
            /// </summary>
            public string Dia_chi
            {
                get { return dia_chi; }
                set { dia_chi = value; }
            }

            private string ma_the;

            public string Ma_the
            {
                get { return ma_the; }
                set { ma_the = value; }
            }
            private string ma_dkbd;

            public string Ma_dkbd
            {
                get { return ma_dkbd; }
                set { ma_dkbd = value; }
            }

            private string gt_the_tu;
            /// <summary>
            /// 8 ký tự, dạng yyyyMMdd
            /// </summary>
            public string Gt_the_tu
            {
                get { return gt_the_tu; }
                set { gt_the_tu = value; }
            }

            private string gt_the_den;
            /// <summary>
            /// 8 ký tự, dạng yyyyMMdd
            /// </summary>
            public string Gt_the_den
            {
                get { return gt_the_den; }
                set { gt_the_den = value; }
            }

            private string ma_benh;
            /// <summary>
            /// mã bệnh chính theo ICD10
            /// </summary>
            public string Ma_benh
            {
                get { return ma_benh; }
                set { ma_benh = value; }
            }

            private string ma_benhkhac;
            /// <summary>
            /// mã bệnh kèm theo ICD10, có nhiều mã được phân cách bằng dấu ";"
            /// </summary>
            public string Ma_benhkhac
            {
                get { return ma_benhkhac; }
                set { ma_benhkhac = value; }
            }

            private string ten_benh;
            /// <summary>
            /// Ghi đầy đủ chẩn đoán khi ra viện
            /// </summary>
            public string Ten_benh
            {
                get { return ten_benh; }
                set { ten_benh = value; }
            }

            private string ma_lydo_vvien;
            /// <summary>
            /// 1: đúng tuyến, 2: cấp cứu; 3 trái tuyến
            /// </summary>
            public string Ma_lydo_vvien
            {
                get { return ma_lydo_vvien; }
                set { ma_lydo_vvien = value; }
            }

            private string ma_noi_chuyen;
            public string Ma_noi_chuyen
            {
                get { return ma_noi_chuyen; }
                set { ma_noi_chuyen = value; }
            }

            private string ma_tai_nan;
            /// <summary>
            /// Mã hóa tham chiếu bảng 8 ( bảng tai nạn thương tích)
            /// </summary>
            public string Ma_tai_nan
            {
                get { return ma_tai_nan; }
                set { ma_tai_nan = value; }
            }

            private string ngay_vao;
            /// <summary>
            /// 12 ký tự dạng yyyyMMddHHmm
            /// </summary>
            public string Ngay_vao
            {
                get { return ngay_vao; }
                set { ngay_vao = value; }
            }

            private string ngay_ra;
            /// <summary>
            /// 12 ký tự dạng yyyyMMddHHmm
            /// </summary>
            public string Ngay_ra
            {
                get { return ngay_ra; }
                set { ngay_ra = value; }
            }
            private int so_ngay_dtri;

            public int So_ngay_dtri
            {
                get { return so_ngay_dtri; }
                set { so_ngay_dtri = value; }
            }

            private string ket_qua_dtri;
            /// <summary>
            /// 1: khỏi; 2: đỡ; 3:Không thay đổi; 4: Nặng hơn; 5: tử vong
            /// </summary>
            public string Ket_qua_dtri
            {
                get { return ket_qua_dtri; }
                set { ket_qua_dtri = value; }
            }


            private string tinh_trang_rv;
            /// <summary>
            /// 1: ra viện: 2: chuyển viện: 3: trốn viện; 4: xin ra viện
            /// </summary>
            public string Tinh_trang_rv
            {
                get { return tinh_trang_rv; }
                set { tinh_trang_rv = value; }
            }

            private string ngay_ttoan;
            /// <summary>
            /// TRả về kiểu string
            /// 12 ký tự dạng yyyyMMddHHmm
            /// </summary>
            public string Ngay_ttoan
            {
                get { return ngay_ttoan; }
                set { ngay_ttoan = value; }
            }
            private int muc_huong;

            /// <summary>
            /// TRả về kiểu int
            /// Mức hưởng tương ứng với quyền lợi được hưởng của người bệnh (nếu trái tuyến tuyến : mức hưởng * tỷ lệ hưởng theo hạng bệnh viện)
            /// </summary>
            public int Muc_huong
            {
                get { return muc_huong; }
                set { muc_huong = value; }
            }

            private double t_thuoc;
            /// <summary>
            /// TRả về kiểu double
            /// Số chi phí tiền thuốc trong đợt điều trị
            /// </summary>
            public double T_thuoc
            {
                get { return t_thuoc; }
                set { t_thuoc = value; }
            }
            private double t_vtyt;
            /// <summary>
            /// TRả về kiểu double
            /// Số chi phí vật tư y tế trong đợt điều trị
            /// </summary>
            public double T_vtyt
            {
                get { return t_vtyt; }
                set { t_vtyt = value; }
            }

            /// <summary>
            /// TRả về kiểu double
            /// Số chi phí trong đợt điều trị
            /// </summary>
            private double t_tongchi;
            public double T_tongchi
            {
                get { return t_tongchi; }
                set { t_tongchi = value; }
            }
            private double t_bntt;
            /// <summary>
            /// TRả về kiểu double
            /// tiền bệnh nhân thanh toán (bao gồm phần cùng trả và tự trả)
            /// </summary>
            public double T_bntt
            {
                get { return t_bntt; }
                set { t_bntt = value; }
            }

            private double t_bhtt;
            /// <summary>
            /// TRả về kiểu double
            /// tiền đề nghị bảo hiểm thanh toán 
            /// </summary>
            public double T_bhtt
            {
                get { return t_bhtt; }
                set { t_bhtt = value; }
            }
            private double t_nguonkhac;
            /// <summary>
            /// TRả về kiểu double
            /// tiền nguồn khác hỗ trợ
            /// </summary>
            public double T_nguonkhac
            {
                get { return t_nguonkhac; }
                set { t_nguonkhac = value; }
            }
            private double t_ngoaids;
            /// <summary>
            /// TRả về kiểu double
            /// chi phí ngoài định suất
            /// </summary>
            public double T_ngoaids
            {
                get { return t_ngoaids; }
                set { t_ngoaids = value; }
            }
            private string nam_qt;
            /// <summary>
            /// TRả về kiểu int
            /// năm quyết toán (4 ký tự)
            /// </summary>
            public string Nam_qt
            {
                get { return nam_qt; }
                set { nam_qt = value; }
            }
            private string thang_qt;
            /// <summary>
            /// TRả về kiểu int
            /// tháng quyết toán (2 ký tự)
            /// </summary>
            public string Thang_qt
            {
                get { return thang_qt; }
                set { thang_qt = value; }
            }
            private string ma_loai_kcb;
            /// <summary>
            /// trả về kiểu int
            /// 1: khám bệnh; 2 điều trị ngoại trú; 3 điều trị nội trú
            /// </summary>
            public string Ma_loai_kcb
            {
                get { return ma_loai_kcb; }
                set { ma_loai_kcb = value; }
            }
            private string ma_cskcb;

            public string Ma_cskcb
            {
                get { return ma_cskcb; }
                set { ma_cskcb = value; }
            }
            private string ma_khuvuc;
            /// <summary>
            /// mã nơi sinh sống ghi trên thẻ
            /// </summary>
            public string Ma_khuvuc
            {
                get { return ma_khuvuc; }
                set { ma_khuvuc = value; }
            }
            private string ma_pttt_qt;
            /// <summary>
            ///  mã phẫu thuật thủ thuật quốc tế theo ICD 9 vol3; có nhiều mã pttt thì cách nhau bởi dấu  (;)
            /// </summary>
            public string Ma_pttt_qt
            {
                get { return ma_pttt_qt; }
                set { ma_pttt_qt = value; }
            }
            private string can_nang;
            /// <summary>
            /// Ghi với trẻ em dưới 1 tuổi; dấu thập phân là dấu (,), ghi đến 2 chữ số sau dấu thập phân
            /// </summary>
            public string Can_nang
            {
                get { return can_nang; }
                set { can_nang = value; }
            }
            string ma_khoa;

            public string Ma_khoa
            {
                get { return ma_khoa; }
                set { ma_khoa = value; }
            }
            private int trangthai;

            /// <summary>
            /// 
            /// </summary>
            public int Trangthai
            {
                get { return trangthai; }
                set { trangthai = value; }
            }

        }
        public class CTThuoc
        {

            private string ma_lk;

            public string Ma_lk
            {
                get { return ma_lk; }
                set { ma_lk = value; }
            }

            private string ma_thuoc;
            /// <summary>
            /// Mã quy định theo danh mục dùng chung của Bộ Y tế 
            /// </summary>
            public string Ma_thuoc
            {
                get { return ma_thuoc; }
                set { ma_thuoc = value; }
            }

            private int ma_DV;
            /// <summary>
            /// mã thuốc thực tế đơn vị dùng hiện tại
            /// </summary>
            public int Ma_DV
            {
                get { return ma_DV; }
                set { ma_DV = value; }
            }

            /// <summary>
            /// Mã nhóm dịch vụ
            /// </summary>
            private string ma_nhom;
            public string Ma_nhom
            {
                get { return ma_nhom; }
                set { ma_nhom = value; }
            }
            private string ten_thuoc;

            public string Ten_thuoc
            {
                get { return ten_thuoc; }
                set { ten_thuoc = value; }
            }
            private string don_vi_tinh;

            public string Don_vi_tinh
            {
                get { return don_vi_tinh; }
                set { don_vi_tinh = value; }
            }
            private string ham_luong;

            public string Ham_luong
            {
                get { return ham_luong; }
                set { ham_luong = value; }
            }
            private string duong_dung;

            public string Duong_dung
            {
                get { return duong_dung; }
                set { duong_dung = value; }
            }

            private string lieu_dung;
            /// <summary>
            /// liều dùng trong ngày
            /// </summary>
            public string Lieu_dung
            {
                get { return lieu_dung; }
                set { lieu_dung = value; }
            }
            private int ma_pttt;

            public int Ma_pttt
            {
                get { return ma_pttt; }
                set { ma_pttt = value; }
            }
            private string so_dang_ky;
            public string So_dang_ky
            {
                get { return so_dang_ky; }
                set { so_dang_ky = value; }
            }
            private double so_luong;

            public double So_luong
            {
                get { return so_luong; }
                set { so_luong = value; }
            }
            private double don_gia;
            /// <summary>
            /// giá trị trả về: double
            /// Làm tròn đến đơn vị đồng
            /// </summary>
            public double Don_gia
            {
                get { return don_gia; }
                set { don_gia = value; }
            }
            private double tyle_tt;
            /// <summary>
            /// tỷ lệ thanh toán BHYT đối với thuốc có quy định tỷ lệ , số nguyên dương
            /// </summary>
            public double Tyle_tt
            {
                get { return tyle_tt; }
                set { tyle_tt = value; }
            }
            private double thanh_tien;
            /// <summary>
            /// số lượng * đơn giá * tỷ lệ thanh toán hoặc tiền đề nghị cơ quan BHXH thanh toán (làm tròn đến đơn vị đồng)
            /// </summary>
            public double Thanh_tien
            {
                get { return thanh_tien; }
                set { thanh_tien = value; }
            }
            private string ma_khoa;
            /// <summary>
            ///  mã khoa bn được chỉ định dùng thuốc 
            /// </summary>
            public string Ma_khoa
            {
                get { return ma_khoa; }
                set { ma_khoa = value; }
            }
            private string ma_bac_si;
            /// <summary>
            /// số chứng chỉ hành nghề của người chỉ định
            /// </summary>
            public string Ma_bac_si
            {
                get { return ma_bac_si; }
                set { ma_bac_si = value; }
            }
            private string ma_benh;
            /// <summary>
            /// mã bệnh chính, nếu có các bệnh khác kèm theo cách nhau bởi dấu (;)
            /// </summary>
            public string Ma_benh
            {
                get { return ma_benh; }
                set { ma_benh = value; }
            }
            private string ngay_yl;
            /// <summary>
            /// ngày ra y lệnh; 12 ký tự dạng yyyyMMddHHmm
            /// </summary>
            public string Ngay_yl
            {
                get { return ngay_yl; }
                set { ngay_yl = value; }
            }
            private string ten_khoabv;

            public string Ten_khoabv
            {
                get { return ten_khoabv; }
                set { ten_khoabv = value; }
            }
            private double don_gia_bv;

            public double Don_gia_bv
            {
                get { return don_gia_bv; }
                set { don_gia_bv = value; }
            }
            private double nguon_khac;

            public double Nguon_khac
            {
                get { return nguon_khac; }
                set { nguon_khac = value; }
            }
        }
        public class CTDV
        {
            private string ma_lk;

            public string Ma_lk
            {
                get { return ma_lk; }
                set { ma_lk = value; }
            }

            /// <summary>
            /// maQD
            /// </summary>
            private string ma_dich_vu;
            public string Ma_dich_vu
            {
                get { return ma_dich_vu; }
                set { ma_dich_vu = value; }
            }
            private string ma_vat_tu;

            public string Ma_vat_tu
            {
                get { return ma_vat_tu; }
                set { ma_vat_tu = value; }
            }

            private int ma_DV;
            /// <summary>
            /// mã dịch vụ thực tế đơn vị dùng hiện tại
            /// </summary>
            public int Ma_DV
            {
                get { return ma_DV; }
                set { ma_DV = value; }
            }
            private string ma_nhom;

            public string Ma_nhom
            {
                get { return ma_nhom; }
                set { ma_nhom = value; }
            }
            private string ten_dich_vu;

            public string Ten_dich_vu
            {
                get { return ten_dich_vu; }
                set { ten_dich_vu = value; }
            }
            private string don_vi_tinh;

            public string Don_vi_tinh
            {
                get { return don_vi_tinh; }
                set { don_vi_tinh = value; }
            }
            private double so_luong;

            public double So_luong
            {
                get { return so_luong; }
                set { so_luong = value; }
            }
            private double don_gia;

            public double Don_gia
            {
                get { return don_gia; }
                set { don_gia = value; }
            }
            private double tyle_tt;

            public double Tyle_tt
            {
                get { return tyle_tt; }
                set { tyle_tt = value; }
            }
            private double thanh_tien;

            public double Thanh_tien
            {
                get { return thanh_tien; }
                set { thanh_tien = value; }
            }
            private string ma_khoa;

            public string Ma_khoa
            {
                get { return ma_khoa; }
                set { ma_khoa = value; }
            }
            private string ma_bac_si;

            public string Ma_bac_si
            {
                get { return ma_bac_si; }
                set { ma_bac_si = value; }
            }
            private string ma_benh;

            public string Ma_benh
            {
                get { return ma_benh; }
                set { ma_benh = value; }
            }
            private string ngay_yl;
            /// <summary>
            ///  12 ký tự dạng yyyyMMddHHmm
            /// </summary>
            public string Ngay_yl
            {
                get { return ngay_yl; }
                set { ngay_yl = value; }
            }
            private string ngay_kq;

            /// <summary>
            ///  12 ký tự dạng yyyyMMddHHmm
            /// </summary>
            public string Ngay_kq
            {
                get { return ngay_kq; }
                set { ngay_kq = value; }
            }
            private int ma_pttt;// mã phương thức thanh toán: phí dịch vụ: 0; định suất:1; DRG: 2

            public int Ma_pttt
            {
                get { return ma_pttt; }
                set { ma_pttt = value; }
            }
            private string ten_khoabv; //tên khoa phát sinh dịch vụ

            public string Ten_khoabv
            {
                get { return ten_khoabv; }
                set { ten_khoabv = value; }
            }
            private double don_gia_bv; //đơn giá của dịch vụ sử dụng tại bênh viện, làm tròn đến 2 chữ số thập phân

            public double Don_gia_bv
            {
                get { return don_gia_bv; }
                set { don_gia_bv = value; }
            }
            private double nguon_khac;// nguồn khác chi trả: không tính BHTT và bệnh nhân

            public double Nguon_khac
            {
                get { return nguon_khac; }
                set { nguon_khac = value; }
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

            private string ma_dich_vu;
            public string Ma_dich_vu
            {
                get { return ma_dich_vu; }
                set { ma_dich_vu = value; }
            }
            private string ma_chi_so;
            /// <summary>
            /// mã chỉ số xét nghiệm
            /// </summary>
            public string Ma_chi_so
            {
                get { return ma_chi_so; }
                set { ma_chi_so = value; }
            }
            private string ten_chi_so;
            /// <summary>
            /// tên chỉ số xét nghiệm
            /// </summary>
            public string Ten_chi_so
            {
                get { return ten_chi_so; }
                set { ten_chi_so = value; }
            }
            private string gia_tri;
            /// <summary>
            /// kết quả xét nghiệm
            /// </summary>
            public string Gia_tri
            {
                get { return gia_tri; }
                set { gia_tri = value; }
            }
            private string ma_may;
            /// <summary>
            /// mã máy CLS
            /// </summary>
            public string Ma_may
            {
                get { return ma_may; }
                set { ma_may = value; }
            }
            private string mo_ta;
            /// <summary>
            /// mô tả do người đọc kết quả ghi
            /// </summary>
            public string Mo_ta
            {
                get { return mo_ta; }
                set { mo_ta = value; }
            }
            private string ket_luan;
            /// <summary>
            /// kết luận của người đọc kết quả
            /// </summary>
            public string Ket_luan
            {
                get { return ket_luan; }
                set { ket_luan = value; }
            }
            private string ngay_kq;
            /// <summary>
            ///  12 ký tự dạng yyyyMMddHHmm
            /// </summary>
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

            /// <summary>
            ///  12 ký tự dạng yyyyMMddHHmm
            /// </summary>
            public string Ngay_yl
            {
                get { return ngay_yl; }
                set { ngay_yl = value; }
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
        //thiếu chuyển tuyến
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List<clsKetNoiXP.ThongTinBenhNhan> fn_ThongTinBenhNhan(XDocument doc)
        {
            List<clsKetNoiXP.ThongTinBenhNhan> _listDSBN;// thông tin chung của bệnh nhân 
            doc = UppercaseXDocument(doc);
            #region Thông tin bệnh nhân
            try
            {
                if (doc.Element("BENHNHAN") != null && doc.Element("BENHNHAN").Element("BANG1") != null)
                {
                    _listDSBN = (from ttt in
                                     (from bn in doc.Descendants("BENHNHAN") select bn).Descendants("BANG1")
                                 select new clsKetNoiXP.ThongTinBenhNhan
                                 {
                                     Ma_lk = ttt.Element("MA_LK") == null ? "" : ttt.Element("MA_LK").Value,
                                     Ma_bn = ttt.Element("MA_BN") == null ? "" : ttt.Element("MA_BN").Value,
                                     Ho_ten = ttt.Element("HO_TEN") == null ? "" : ttt.Element("HO_TEN").Value,
                                     Ngay_sinh = ttt.Element("NGAY_SINH") == null ? "" : ttt.Element("NGAY_SINH").Value,
                                     Gioi_tinh = ttt.Element("GIOI_TINH") == null ? 0 : Convert.ToInt16(ttt.Element("GIOI_TINH").Value),
                                     Dia_chi = ttt.Element("DIA_CHI") == null ? "" : ttt.Element("DIA_CHI").Value,
                                     Ma_the = ttt.Element("MA_THE") == null ? "" : ttt.Element("MA_THE").Value,
                                     Ma_dkbd = ttt.Element("MA_DKBD") == null ? "" : ttt.Element("MA_DKBD").Value,
                                     Gt_the_tu = ttt.Element("GT_THE_TU") == null ? "" : ttt.Element("GT_THE_TU").Value,
                                     Gt_the_den = ttt.Element("GT_THE_DEN") == null ? "" : ttt.Element("GT_THE_DEN").Value,
                                     Ma_benh = ttt.Element("MA_BENH") == null ? "" : ttt.Element("MA_BENH").Value,
                                     Ma_benhkhac = ttt.Element("MA_BENHKHAC") == null ? "" : ttt.Element("MA_BENHKHAC").Value,
                                     Ten_benh = ttt.Element("TEN_BENH") == null ? "" : ttt.Element("TEN_BENH").Value,
                                     Ma_lydo_vvien = ttt.Element("MA_LYDO_VVIEN") == null ? "" : ttt.Element("MA_LYDO_VVIEN").Value,
                                     Ma_noi_chuyen = ttt.Element("MA_NOI_CHUYEN") == null ? "" : ttt.Element("MA_NOI_CHUYEN").Value,
                                     Ma_tai_nan = ttt.Element("MA_TAI_NAN") == null ? "" : ttt.Element("MA_TAI_NAN").Value,
                                     Ngay_vao = ttt.Element("NGAY_VAO") == null ? "" : ttt.Element("NGAY_VAO").Value,
                                     Ngay_ra = ttt.Element("NGAY_RA") == null ? "" : ttt.Element("NGAY_RA").Value,
                                     So_ngay_dtri = ttt.Element("SO_NGAY_DTRI") == null ? 0 : Convert.ToInt16(ttt.Element("SO_NGAY_DTRI").Value),
                                     Ket_qua_dtri = ttt.Element("KET_QUA_DTRI") == null ? "" : ttt.Element("KET_QUA_DTRI").Value,
                                     Tinh_trang_rv = ttt.Element("TINH_TRANG_RV") == null ? "" : ttt.Element("TINH_TRANG_RV").Value,
                                     Ngay_ttoan = ttt.Element("NGAY_TTOAN") == null ? "" : ttt.Element("NGAY_TTOAN").Value,
                                     Muc_huong = ttt.Element("MUC_HUONG") == null ? 0 : Convert.ToInt16(ttt.Element("MUC_HUONG").Value),
                                     T_tongchi = ttt.Element("T_TONGCHI") == null ? 0 : Convert.ToDouble(ttt.Element("T_TONGCHI").Value),
                                     T_bntt = ttt.Element("T_BNTT") == null ? 0 : Convert.ToDouble(ttt.Element("T_BNTT").Value),
                                     T_bhtt = ttt.Element("T_BHTT") == null ? 0 : Convert.ToDouble(ttt.Element("T_BHTT").Value),
                                     T_nguonkhac = ttt.Element("T_NGUONKHAC") == null ? 0 : Convert.ToDouble(ttt.Element("T_NGUONKHAC").Value),
                                     T_ngoaids = ttt.Element("T_NGOAIDS") == null ? 0 : Convert.ToDouble(ttt.Element("T_NGOAIDS").Value),
                                     Nam_qt = ttt.Element("NAM_QT") == null ? "" : ttt.Element("NAM_QT").Value,
                                     Thang_qt = ttt.Element("THANG_QT") == null ? "" : ttt.Element("THANG_QT").Value,
                                     Ma_loai_kcb = ttt.Element("MA_LOAIKCB") == null ? "" : ttt.Element("MA_LOAIKCB").Value,
                                     Ma_cskcb = ttt.Element("MA_CSKCB") == null ? "" : ttt.Element("MA_CSKCB").Value,
                                     Ma_khoa = ttt.Element("MA_KHOA") == null ? "" : ttt.Element("MA_KHOA").Value,
                                     Ma_khuvuc = ttt.Element("MA_KHUVUC") == null ? "" : ttt.Element("MA_KHUVUC").Value,
                                     Ma_pttt_qt = ttt.Element("MA_PTTT_QT") == null ? "" : ttt.Element("MA_PTTT_QT").Value,
                                     Check = false,
                                     T_thuoc = ttt.Element("T_THUOC") == null ? 0 : Convert.ToDouble(ttt.Element("T_THUOC").Value),
                                     T_vtyt = ttt.Element("T_VTYT") == null ? 0 : Convert.ToDouble(ttt.Element("T_VTYT").Value),
                                     Can_nang = ttt.Element("CAN_NANG") == null ? "" : ttt.Element("CAN_NANG").Value,
                                     Trangthai = doc.Element("BENHNHAN").Attribute("TT") == null ? -1 : Convert.ToInt32(doc.Element("BENHNHAN").Attribute("TT").Value),

                                 }).Where(p=>p.Ma_lk.ToString().Trim() != "").ToList();

                }
                else
                {
                    _listDSBN = new List<clsKetNoiXP.ThongTinBenhNhan>();
                }
            }
            catch (Exception)
            {

                _listDSBN = new List<clsKetNoiXP.ThongTinBenhNhan>();
            }



            #endregion


            return _listDSBN;
        }
        public static XDocument UppercaseXDocument(XDocument doc)
        {
            foreach (XElement item in doc.Descendants())
            {
                string a = item.Name.ToString().ToUpper();
                item.Name = a;
            }
            XDocument _dddd = doc;
            return _dddd;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List<clsKetNoiXP.CTThuoc> fn_CTThuoc(XDocument doc)
        {
            List<clsKetNoiXP.CTThuoc> _data;
            doc = UppercaseXDocument(doc);
            #region Chi tiết thuốc
            try
            {
                if (doc.Element("BENHNHAN") != null && doc.Element("BENHNHAN").Element("BANG2") != null && doc.Element("BENHNHAN").Element("BANG2").Element("THUOC") != null)
                {
                    _data = (from rs in
                                 (from ttt in (from bn in doc.Descendants("BENHNHAN") select bn).Descendants("BANG2") select ttt).Descendants("THUOC")
                             select new clsKetNoiXP.CTThuoc
                             {

                                 Ma_lk = rs.Element("MA_LK") == null ? "" : rs.Element("MA_LK").Value,//
                                 Ma_thuoc = rs.Element("MA_THUOC") == null ? "" : rs.Element("MA_THUOC").Value,//
                                 //Ma_DV = rs.Element("MA_DV") == null ? 0 : Convert.ToInt32(rs.Element("MA_DV").Value),
                                 //Ma_DV = rs.Element("MA_THUOC") == null ? 0 : Convert.ToInt32(rs.Element("MA_THUOC").Value),
                                 Ma_nhom = rs.Element("MA_NHOM") == null ? "" : rs.Element("MA_NHOM").Value,//
                                 Ten_thuoc = rs.Element("TEN_THUOC") == null ? "" : rs.Element("TEN_THUOC").Value,//
                                 Don_vi_tinh = rs.Element("DON_VI_TINH") == null ? "" : rs.Element("DON_VI_TINH").Value,//
                                 Ham_luong = rs.Element("HAM_LUONG") == null ? "" : rs.Element("HAM_LUONG").Value,//
                                 Duong_dung = rs.Element("DUONG_DUNG") == null ? "" : rs.Element("DUONG_DUNG").Value,//
                                 So_dang_ky = rs.Element("SO_DANG_KY") == null ? "" : rs.Element("SO_DANG_KY").Value,//
                                 So_luong = (rs.Element("SO_LUONG") == null || String.IsNullOrEmpty(rs.Element("SO_LUONG").Value) || rs.Element("SO_LUONG").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("SO_LUONG").Value),//
                                 Don_gia = (rs.Element("DON_GIA") == null || String.IsNullOrEmpty(rs.Element("DON_GIA").Value) || rs.Element("DON_GIA").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("DON_GIA").Value),//
                                 Tyle_tt = (rs.Element("TYLE_TT") == null || String.IsNullOrEmpty(rs.Element("TYLE_TT").Value) || rs.Element("TYLE_TT").Value.ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("TYLE_TT").Value),//
                                 Thanh_tien = (rs.Element("THANH_TIEN") == null || String.IsNullOrEmpty(rs.Element("THANH_TIEN").Value) || rs.Element("THANH_TIEN").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("THANH_TIEN").Value),//
                                 Ma_khoa = rs.Element("MA_KHOA") == null ? "" : rs.Element("MA_KHOA").Value,//
                                 Ma_bac_si = rs.Element("MA_BAC_SI") == null ? "" : rs.Element("MA_BAC_SI").Value,//
                                 Ma_benh = rs.Element("MA_BENH") == null ? "" : rs.Element("MA_BENH").Value,//
                                 Ngay_yl = rs.Element("NGAY_YL") == null ? "" : rs.Element("NGAY_YL").Value,//
                                 Lieu_dung = rs.Element("LIEU_DUNG") == null ? "" : rs.Element("LIEU_DUNG").Value,//
                                 Ma_pttt = (rs.Element("MA_PTTT") == null || String.IsNullOrEmpty(rs.Element("MA_PTTT").Value) || rs.Element("MA_PTTT").Value.Trim() == "") ? 0 : Convert.ToInt16(rs.Element("MA_PTTT").Value),//
                                 Ten_khoabv = rs.Element("TEN_KHOABV") == null ? "" : rs.Element("TEN_KHOABV").Value,//
                                 Don_gia_bv = (rs.Element("DON_GIA_BV") == null || String.IsNullOrEmpty(rs.Element("DON_GIA_BV").Value) || rs.Element("DON_GIA_BV").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("DON_GIA_BV").Value),//
                                 Nguon_khac = (rs.Element("NGUON_KHAC") == null || String.IsNullOrEmpty(rs.Element("NGUON_KHAC").Value) || rs.Element("NGUON_KHAC").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("NGUON_KHAC").Value),//


                             }).Where(p=>p.Ma_lk.ToString().Trim() != "").ToList();
                }
                else
                {
                    _data = new List<clsKetNoiXP.CTThuoc>();
                }
            }
            catch (Exception)
            {

                _data = new List<clsKetNoiXP.CTThuoc>();
            }


            #endregion

            return _data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List<clsKetNoiXP.CTDV> fn_CTDV(XDocument doc)
        {
            List<clsKetNoiXP.CTDV> _data;//= new List<clsKetNoiXP.CTDV>();
            doc = UppercaseXDocument(doc);
            #region Chi tiết dịch vụ

            try
            {
                if (doc.Element("BENHNHAN") != null && doc.Element("BENHNHAN").Element("BANG3") != null && doc.Element("BENHNHAN").Element("BANG3").Element("DV") != null)
                {
                    _data = (from rs in
                                 (from ttt in (from bn in doc.Descendants("BENHNHAN") select bn).Descendants("BANG3") select ttt).Descendants("DV")
                             select new clsKetNoiXP.CTDV
                             {
                                 Ma_lk = rs.Element("MA_LK") == null ? "" : rs.Element("MA_LK").Value,
                                 Ma_dich_vu = rs.Element("MA_DICH_VU") == null ? "" : rs.Element("MA_DICH_VU").Value,

                                 ///////////////////////////////////////////
                                 Ma_DV = rs.Element("MA_DICH_VU") == null ? 0 : Convert.ToInt32(rs.Element("MA_DICH_VU").Value),
                                 Ma_vat_tu = rs.Element("MA_VAT_TU") == null ? "" : rs.Element("MA_VAT_TU").Value,
                                 Ma_nhom = rs.Element("MA_NHOM") == null ? "" : rs.Element("MA_NHOM").Value,
                                 Ten_dich_vu = rs.Element("TEN_DICH_VU") == null ? "" : rs.Element("TEN_DICH_VU").Value,
                                 Don_vi_tinh = rs.Element("DON_VI_TINH") == null ? "" : rs.Element("DON_VI_TINH").Value,
                                 So_luong = (rs.Element("SO_LUONG") == null || String.IsNullOrEmpty(rs.Element("SO_LUONG").Value) || rs.Element("SO_LUONG").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("SO_LUONG").Value),
                                 Don_gia = (rs.Element("DON_GIA") == null || String.IsNullOrEmpty(rs.Element("DON_GIA").Value) || rs.Element("DON_GIA").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("DON_GIA").Value),
                                 Tyle_tt = (rs.Element("TYLE_TT") == null || String.IsNullOrEmpty(rs.Element("TYLE_TT").Value) || rs.Element("TYLE_TT").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("TYLE_TT").Value),
                                 Thanh_tien = (rs.Element("THANH_TIEN") == null || String.IsNullOrEmpty(rs.Element("THANH_TIEN").Value) || rs.Element("THANH_TIEN").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("THANH_TIEN").Value),
                                 Ma_khoa = rs.Element("MA_KHOA") == null ? "" : rs.Element("MA_KHOA").Value,
                                 Ma_bac_si = rs.Element("MA_BAC_SI") == null ? "" : rs.Element("MA_BAC_SI").Value,
                                 Ma_benh = rs.Element("MA_BENH") == null ? "" : rs.Element("MA_BENH").Value,
                                 Ngay_yl = rs.Element("NGAY_YL") == null ? "" : rs.Element("NGAY_YL").Value,
                                 Ngay_kq = rs.Element("NGAY_KQ") == null ? "" : rs.Element("NGAY_KQ").Value,
                                 Ma_pttt = (rs.Element("MA_PTTT") == null || String.IsNullOrEmpty(rs.Element("MA_PTTT").Value) || rs.Element("MA_PTTT").Value.Trim() == "") ? 0 : Convert.ToInt16(rs.Element("MA_PTTT").Value),
                                 Ten_khoabv = rs.Element("TEN_KHOABV") == null ? "" : rs.Element("TEN_KHOABV").Value,
                                 Don_gia_bv = (rs.Element("DON_GIA_BV") == null || String.IsNullOrEmpty(rs.Element("DON_GIA_BV").Value) || rs.Element("DON_GIA_BV").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("DON_GIA_BV").Value),
                                 Nguon_khac = (rs.Element("NGUON_KHAC") == null || String.IsNullOrEmpty(rs.Element("NGUON_KHAC").Value) || rs.Element("NGUON_KHAC").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("NGUON_KHAC").Value),



                             }).Where(p=>p.Ma_lk.ToString().Trim() != "").ToList();
                }
                else
                {
                    _data = new List<clsKetNoiXP.CTDV>();
                }
            }
            catch (Exception)
            {

                _data = new List<clsKetNoiXP.CTDV>();
            }


            #endregion

            return _data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List<clsKetNoiXP.CT_CLS> fn_CTCLS(XDocument doc)
        {
            List<clsKetNoiXP.CT_CLS> _data;// = new List<clsKetNoiXP.CT_CLS>();
            doc = UppercaseXDocument(doc);
            #region Chi tiết cận lâm sàng
            try
            {
                if (doc.Element("BENHNHAN") != null && doc.Element("BENHNHAN").Element("BANG4") != null && doc.Element("BENHNHAN").Element("BANG4").Element("CLS") != null)
                {
                    _data = (from rs in
                                 (from ttt in (from bn in doc.Descendants("BENHNHAN") select bn).Descendants("BANG4") select ttt).Descendants("CLS")
                             select new clsKetNoiXP.CT_CLS
                             {
                                 Ma_lk = rs.Element("MA_LK") == null ? "" : rs.Element("MA_LK").Value,
                                 Ma_dich_vu = rs.Element("MA_DICH_VU") == null ? "" : rs.Element("MA_DICH_VU").Value,
                                 Ma_chi_so = rs.Element("MA_CHI_SO") == null ? "" : rs.Element("MA_CHI_SO").Value,
                                 Ten_chi_so = rs.Element("TEN_CHI_SO") == null ? "" : rs.Element("TEN_CHI_SO").Value,
                                 Gia_tri = rs.Element("GIA_TRI") == null ? "" : rs.Element("GIA_TRI").Value,
                                 Ma_may = rs.Element("MA_MAY") == null ? "" : rs.Element("MA_MAY").Value,
                                 Mo_ta = rs.Element("MO_TA") == null ? "" : rs.Element("MO_TA").Value,
                                 Ket_luan = rs.Element("KET_LUAN") == null ? "" : rs.Element("KET_LUAN").Value,
                                 Ngay_kq = rs.Element("NGAY_KQ") == null ? "" : rs.Element("NGAY_KQ").Value

                             }).Where(p=>p.Ma_lk.ToString().Trim() != "").ToList();
                }
                else
                {
                    _data = new List<clsKetNoiXP.CT_CLS>();
                }
            }
            catch (Exception)
            {

                _data = new List<clsKetNoiXP.CT_CLS>();
            }
            #endregion
            return _data;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List<clsKetNoiXP.DienBienBenh> fn_DienBienBenh(XDocument doc)
        {
            List<clsKetNoiXP.DienBienBenh> _data;//= new List<clsKetNoiXP.DienBienBenh>();
            doc = UppercaseXDocument(doc);
            #region Diễn biến bệnh
            try
            {
                if (doc.Element("BENHNHAN") != null && doc.Element("BENHNHAN").Element("BANG5") != null && doc.Element("BENHNHAN").Element("BANG5").Element("DB") != null)
                {
                    _data = (from rs in
                                 (from ttt in (from bn in doc.Descendants("BENHNHAN") select bn).Descendants("BANG5") select ttt).Descendants("DB")
                             select new clsKetNoiXP.DienBienBenh
                             {
                                 Ma_lk = rs.Element("MA_LK") == null ? "" : rs.Element("MA_LK").Value,
                                 Dien_bien = rs.Element("DIEN_BIEN") == null ? "" : rs.Element("DIEN_BIEN").Value,
                                 Hoi_chan = rs.Element("HOI_CHAN") == null ? "" : rs.Element("HOI_CHAN").Value,
                                 Phau_thuat = rs.Element("PHAU_THUAT") == null ? "" : rs.Element("PHAU_THUAT").Value,
                                 Ngay_yl = rs.Element("NGAY_YL") == null ? "" : rs.Element("NGAY_YL").Value,

                             }).Where(p=>p.Ma_lk.ToString().Trim() != "").ToList();
                }
                else
                {
                    _data = new List<clsKetNoiXP.DienBienBenh>();
                }
            }
            catch (Exception)
            {
                _data = new List<clsKetNoiXP.DienBienBenh>();
            }


            #endregion


            return _data;
        }
        //public static List<clsKetNoiXP.ThongTinChung> fn_ThongTinChung(XDocument doc)
        //{
        //    List<clsKetNoiXP.ThongTinChung> _listDSBN = new List<clsKetNoiXP.ThongTinChung>();// thông tin chung của bệnh nhân 
        //    try
        //    {
        //        if (doc.Element("CHECKOUT") != null && doc.Element("CHECKOUT").Element("THONGTINBENHNHAN") != null)
        //        {
        //            _listDSBN = (from rs in
        //                             (from bn in doc.Descendants("CHECKOUT") select bn).Descendants("THONGTINBENHNHAN")
        //                         select new clsKetNoiXP.ThongTinChung
        //                         {

        //                             Ma_lk = rs.Element("MA_LK") == null ? "" : rs.Element("MA_LK").Value,
        //                             Ngaygiovao = rs.Element("NGAYGIOVAO") == null ? "" : rs.Element("NGAYGIOVAO").Value,
        //                             Ngaygiora = rs.Element("NGAYGIORA") == null ? "" : rs.Element("NGAYGIORA").Value,
        //                             Mabenhvien = rs.Element("MABENHVIEN") == null ? "" : rs.Element("MABENHVIEN").Value,
        //                             Chandoan = rs.Element("CHANDOAN") == null ? "" : rs.Element("CHANDOAN").Value,
        //                             Trangthai = rs.Element("TRANGTHAI") == null ? "" : rs.Element("TRANGTHAI").Value,
        //                             Ketqua = rs.Element("KETQUA") == null ? "" : rs.Element("KETQUA").Value,
        //                             Sodienthoai_lh = rs.Element("SODIENTHOAI_LT") == null ? "" : rs.Element("SODIENTHOAI_LT").Value,
        //                             Nguoilienhe = rs.Element("NGUOILIENHE") == null ? "" : rs.Element("NGUOILIENHE").Value

        //                         }).ToList();

        //            return _listDSBN;
        //        }
        //        else
        //        {
        //            return new List<ThongTinChung>();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<ThongTinChung>();
        //    }
        //}
        #region lấy file cũ
        //public static List<clsKetNoiXP.ThongTinBenhNhan> fn_ThongTinBenhNhan(XDocument doc)
        //{
        //    List<clsKetNoiXP.ThongTinBenhNhan> _listDSBN = new List<clsKetNoiXP.ThongTinBenhNhan>();// thông tin chung của bệnh nhân 
        //    try
        //    {
        //        if (doc.Element("CHECKOUT") != null && doc.Element("CHECKOUT").Element("THONGTINCHITIET") != null && doc.Element("CHECKOUT").Element("THONGTINCHITIET").Element("TONGHOP") != null)
        //        {
        //            _listDSBN = (from rs in
        //                             (from ttct in
        //                                  (from bn in doc.Descendants("CHECKOUT") select bn).Descendants("THONGTINCHITIET")
        //                              select ttct).Descendants("TONGHOP")
        //                         select new clsKetNoiXP.ThongTinBenhNhan
        //                         {
        //                             Check = false,
        //                             Ma_lk = rs.Element("MA_LK") == null ? "" : rs.Element("MA_LK").Value,
        //                             Ma_bn = rs.Element("MA_BN") == null ? "" : rs.Element("MA_BN").Value,
        //                             Ho_ten = rs.Element("HO_TEN") == null ? "" : rs.Element("HO_TEN").Value,
        //                             Ngay_sinh = rs.Element("NGAY_SINH") == null ? "" : rs.Element("NGAY_SINH").Value,
        //                             Gioi_tinh = rs.Element("GIOI_TINH") == null ? 0 : Convert.ToInt16(rs.Element("GIOI_TINH").Value),
        //                             Dia_chi = rs.Element("DIA_CHI") == null ? "" : rs.Element("DIA_CHI").Value,
        //                             Ma_the = rs.Element("MA_THE") == null ? "" : rs.Element("MA_THE").Value,
        //                             Ma_dkbd = rs.Element("MA_DKBD") == null ? "" : rs.Element("MA_DKBD").Value,
        //                             Gt_the_tu = rs.Element("GT_THE_TU") == null ? "" : rs.Element("GT_THE_TU").Value,
        //                             Gt_the_den = rs.Element("GT_THE_DEN") == null ? "" : rs.Element("GT_THE_DEN").Value,
        //                             Ma_benh = rs.Element("MA_BENH") == null ? "" : rs.Element("MA_BENH").Value,
        //                             Ma_benhkhac = rs.Element("MA_BENHKHAC") == null ? "" : rs.Element("MA_BENHKHAC").Value,
        //                             Ten_benh = rs.Element("TEN_BENH") == null ? "" : rs.Element("TEN_BENH").Value,
        //                             Ma_lydo_vvien = rs.Element("MA_LYDO_VVIEN") == null ? "" : rs.Element("MA_LYDO_VVIEN").Value,
        //                             Ma_noi_chuyen = rs.Element("MA_NOI_CHUYEN") == null ? "" : rs.Element("MA_NOI_CHUYEN").Value,
        //                             Ma_tai_nan = rs.Element("MA_TAI_NAN") == null ? "" : rs.Element("MA_TAI_NAN").Value,
        //                             Ngay_vao = rs.Element("NGAY_VAO") == null ? "" : rs.Element("NGAY_VAO").Value,
        //                             Ngay_ra = rs.Element("NGAY_RA") == null ? "" : rs.Element("NGAY_RA").Value,
        //                             So_ngay_dtri = rs.Element("SO_NGAY_DTRI") == null ? 0 : Convert.ToInt16(rs.Element("SO_NGAY_DTRI").Value),
        //                             Ket_qua_dtri = rs.Element("KET_QUA_DTRI") == null ? "" : rs.Element("KET_QUA_DTRI").Value,
        //                             Tinh_trang_rv = rs.Element("TINH_TRANG_RV") == null ? "" : rs.Element("TINH_TRANG_RV").Value,
        //                             Ngay_ttoan = rs.Element("NGAY_TTOAN") == null ? "" : rs.Element("NGAY_TTOAN").Value,
        //                             Muc_huong = rs.Element("MUC_HUONG") == null ? 0 : Convert.ToInt16(rs.Element("MUC_HUONG").Value),
        //                             T_thuoc = rs.Element("T_THUOC") == null ? 0 : Convert.ToDouble(rs.Element("T_THUOC").Value),
        //                             T_vtyt = rs.Element("T_VTYT") == null ? 0 : Convert.ToDouble(rs.Element("T_VTYT").Value),
        //                             T_tongchi = rs.Element("T_TONGCHI") == null ? 0 : Convert.ToDouble(rs.Element("T_TONGCHI").Value),
        //                             T_bntt = rs.Element("T_BNTT") == null ? 0 : Convert.ToDouble(rs.Element("T_BNTT").Value),
        //                             T_bhtt = rs.Element("T_BHTT") == null ? 0 : Convert.ToDouble(rs.Element("T_BHTT").Value),
        //                             T_nguonkhac = rs.Element("T_NGUONKHAC") == null ? 0 : Convert.ToDouble(rs.Element("T_NGUONKHAC").Value),
        //                             T_ngoaids = rs.Element("T_NGOAIDS") == null ? 0 : Convert.ToDouble(rs.Element("T_NGOAIDS").Value),
        //                             Nam_qt = rs.Element("NAM_QT") == null ? "" : rs.Element("NAM_QT").Value,
        //                             Thang_qt = rs.Element("THANG_QT") == null ? "" : rs.Element("THANG_QT").Value,
        //                             Ma_loai_kcb = rs.Element("MA_LOAIKCB") == null ? "" : rs.Element("MA_LOAIKCB").Value,
        //                             Ma_cskcb = rs.Element("MA_CSKCB") == null ? "" : rs.Element("MA_CSKCB").Value,
        //                             Ma_khuvuc = rs.Element("MA_KHUVUC") == null ? "" : rs.Element("MA_KHUVUC").Value,
        //                             Ma_pttt_qt = rs.Element("MA_PTTT_QT") == null ? "" : rs.Element("MA_PTTT_QT").Value,
        //                             Can_nang = rs.Element("CAN_NANG") == null ? "" : rs.Element("CAN_NANG").Value,
        //                             Ma_khoa = rs.Element("MA_KHOA") == null ? "" : rs.Element("MA_KHOA").Value,
        //                         }).ToList();

        //            return _listDSBN;
        //        }
        //        else
        //        {
        //            return new List<ThongTinBenhNhan>();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<ThongTinBenhNhan>();
        //    }
        //}
        //public static List<clsKetNoiXP.CTThuoc> fn_CTThuoc(XDocument doc)
        //{
        //    List<clsKetNoiXP.CTThuoc> _listThuoc = new List<clsKetNoiXP.CTThuoc>();
        //    try
        //    {
        //        #region thuốc
        //        if (doc.Element("CHECKOUT") != null && doc.Element("CHECKOUT").Element("THONGTINCHITIET") != null && doc.Element("CHECKOUT").Element("THONGTINCHITIET").Element("Bang_CTTHUOC") != null)
        //        {
        //            _listThuoc = (from rs in
        //                              (from ds in
        //                                   (from ttct in
        //                                        (from bn in doc.Descendants("CHECKOUT") select bn).Descendants("THONGTINCHITIET")
        //                                    select ttct).Descendants("Bang_CTTHUOC")
        //                               select ds).Descendants("CTTHUOC")
        //                          select new clsKetNoiXP.CTThuoc
        //                          {
        //                              Ma_lk = rs.Element("MA_LK") == null ? "" : rs.Element("MA_LK").Value,
        //                              Ma_thuoc = rs.Element("MA_THUOC") == null ? "" : rs.Element("MA_THUOC").Value,
        //                              Ma_DV = rs.Element("MA_DV") == null ? 0 : Convert.ToInt32(rs.Element("MA_DV").Value),
        //                              Ma_nhom = rs.Element("MA_NHOM") == null ? "" : rs.Element("MA_NHOM").Value,
        //                              Ten_thuoc = rs.Element("TEN_THUOC") == null ? "" : rs.Element("TEN_THUOC").Value,
        //                              Don_vi_tinh = rs.Element("DON_VI_TINH") == null ? "" : rs.Element("DON_VI_TINH").Value,
        //                              Ham_luong = rs.Element("HAM_LUONG") == null ? "" : rs.Element("HAM_LUONG").Value,
        //                              Duong_dung = rs.Element("DUONG_DUNG") == null ? "" : rs.Element("DUONG_DUNG").Value,
        //                              So_dang_ky = rs.Element("SO_DANG_KY") == null ? "" : rs.Element("SO_DANG_KY").Value,
        //                              So_luong = (rs.Element("SO_LUONG") == null || String.IsNullOrEmpty(rs.Element("SO_LUONG").Value) || rs.Element("SO_LUONG").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("SO_LUONG").Value),
        //                              Don_gia = (rs.Element("DON_GIA") == null || String.IsNullOrEmpty(rs.Element("DON_GIA").Value) || rs.Element("DON_GIA").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("DON_GIA").Value),
        //                              Tyle_tt = (rs.Element("TYLE_TT") == null || String.IsNullOrEmpty(rs.Element("TYLE_TT").Value) || rs.Element("TYLE_TT").Value.ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("TYLE_TT").Value),
        //                              Thanh_tien = (rs.Element("THANH_TIEN") == null || String.IsNullOrEmpty(rs.Element("THANH_TIEN").Value) || rs.Element("THANH_TIEN").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("THANH_TIEN").Value),
        //                              Ma_khoa = rs.Element("MA_KHOA") == null ? "" : rs.Element("MA_KHOA").Value,
        //                              Ma_bac_si = rs.Element("MA_BAC_SI") == null ? "" : rs.Element("MA_BAC_SI").Value,
        //                              Ma_benh = rs.Element("MA_BENH") == null ? "" : rs.Element("MA_BENH").Value,
        //                              Ngay_yl = rs.Element("NGAY_YL") == null ? "" : rs.Element("NGAY_YL").Value,
        //                              Lieu_dung = rs.Element("LIEU_DUNG") == null ? "" : rs.Element("LIEU_DUNG").Value,
        //                              Ma_pttt = (rs.Element("MA_PTTT") == null || String.IsNullOrEmpty(rs.Element("MA_PTTT").Value) || rs.Element("MA_PTTT").Value.Trim() == "") ? 0 : Convert.ToInt16(rs.Element("MA_PTTT").Value),
        //                              Ten_khoabv = rs.Element("TEN_KHOABV") == null ? "" : rs.Element("TEN_KHOABV").Value,
        //                              Don_gia_bv = (rs.Element("DON_GIA_BV") == null || String.IsNullOrEmpty(rs.Element("DON_GIA_BV").Value) || rs.Element("DON_GIA_BV").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("DON_GIA_BV").Value),
        //                              Nguon_khac = (rs.Element("NGUON_KHAC") == null || String.IsNullOrEmpty(rs.Element("NGUON_KHAC").Value) || rs.Element("NGUON_KHAC").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("NGUON_KHAC").Value),

        //                          }).ToList();


        //            return _listThuoc;
        //        }
        //        else
        //        {
        //            return new List<CTThuoc>();
        //        }
        //        #endregion thuốc

        //    }
        //    catch (Exception)
        //    {
        //        return new List<CTThuoc>();
        //    }
        //}
        //public static List<clsKetNoiXP.CTDV> fn_CTDV(XDocument doc)
        //{
        //    List<clsKetNoiXP.CTDV> _listDV = new List<clsKetNoiXP.CTDV>();

        //    try
        //    {
        //        if (doc.Element("CHECKOUT") != null && doc.Element("CHECKOUT").Element("THONGTINCHITIET") != null && doc.Element("CHECKOUT").Element("THONGTINCHITIET").Element("Bang_CTDV") != null)
        //        {
        //            #region dịch vụ
        //            _listDV = (from rs in
        //                           (from ds in
        //                                (from ttct in
        //                                     (from bn in doc.Descendants("CHECKOUT") select bn).Descendants("THONGTINCHITIET")
        //                                 select ttct).Descendants("Bang_CTDV")
        //                            select ds).Descendants("CTDV")
        //                       select new clsKetNoiXP.CTDV
        //                       {
        //                           Ma_lk = rs.Element("MA_LK") == null ? "" : rs.Element("MA_LK").Value,
        //                           Ma_dich_vu = rs.Element("MA_DICH_VU") == null ? "" : rs.Element("MA_DICH_VU").Value,
        //                           Ma_vat_tu = rs.Element("MA_VAT_TU") == null ? "" : rs.Element("MA_VAT_TU").Value,
        //                           Ma_nhom = rs.Element("MA_NHOM") == null ? "" : rs.Element("MA_NHOM").Value,
        //                           Ten_dich_vu = rs.Element("TEN_DICH_VU") == null ? "" : rs.Element("TEN_DICH_VU").Value,
        //                           Don_vi_tinh = rs.Element("DON_VI_TINH") == null ? "" : rs.Element("DON_VI_TINH").Value,
        //                           So_luong = (rs.Element("SO_LUONG") == null || String.IsNullOrEmpty(rs.Element("SO_LUONG").Value) || rs.Element("SO_LUONG").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("SO_LUONG").Value),
        //                           Don_gia = (rs.Element("DON_GIA") == null || String.IsNullOrEmpty(rs.Element("DON_GIA").Value) || rs.Element("DON_GIA").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("DON_GIA").Value),
        //                           Tyle_tt = (rs.Element("TYLE_TT") == null || String.IsNullOrEmpty(rs.Element("TYLE_TT").Value) || rs.Element("TYLE_TT").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("TYLE_TT").Value),
        //                           Thanh_tien = (rs.Element("THANH_TIEN") == null || String.IsNullOrEmpty(rs.Element("THANH_TIEN").Value) || rs.Element("THANH_TIEN").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("THANH_TIEN").Value),
        //                           Ma_khoa = rs.Element("MA_KHOA") == null ? "" : rs.Element("MA_KHOA").Value,
        //                           Ma_bac_si = rs.Element("MA_BAC_SI") == null ? "" : rs.Element("MA_BAC_SI").Value,
        //                           Ma_benh = rs.Element("MA_BENH") == null ? "" : rs.Element("MA_BENH").Value,
        //                           Ngay_yl = rs.Element("NGAY_YL") == null ? "" : rs.Element("NGAY_YL").Value,
        //                           Ngay_kq = rs.Element("NGAY_KQ") == null ? "" : rs.Element("NGAY_KQ").Value,
        //                           Ma_pttt = (rs.Element("MA_PTTT") == null || String.IsNullOrEmpty(rs.Element("MA_PTTT").Value) || rs.Element("MA_PTTT").Value.Trim() == "") ? 0 : Convert.ToInt16(rs.Element("MA_PTTT").Value),
        //                           Ten_khoabv = rs.Element("TEN_KHOABV") == null ? "" : rs.Element("TEN_KHOABV").Value,
        //                           Don_gia_bv = (rs.Element("DON_GIA_BV") == null || String.IsNullOrEmpty(rs.Element("DON_GIA_BV").Value) || rs.Element("DON_GIA_BV").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("DON_GIA_BV").Value),
        //                           Nguon_khac = (rs.Element("NGUON_KHAC") == null || String.IsNullOrEmpty(rs.Element("NGUON_KHAC").Value) || rs.Element("NGUON_KHAC").ToString().Trim() == "") ? 0 : Convert.ToDouble(rs.Element("NGUON_KHAC").Value),
        //                       }).ToList();
        //            #endregion dịch vụ
        //            return _listDV;
        //        }
        //        else
        //        {
        //            return new List<CTDV>();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new List<CTDV>();
        //    }
        //}
        //public static List<clsKetNoiXP.CT_CLS> fn_CTCLS(XDocument doc)
        //{
        //    List<clsKetNoiXP.CT_CLS> _listCLS = new List<clsKetNoiXP.CT_CLS>();
        //    try
        //    {
        //        if (doc.Element("CHECKOUT") != null && doc.Element("CHECKOUT").Element("THONGTINCHITIET") != null && doc.Element("CHECKOUT").Element("THONGTINCHITIET").Element("Bang_CT_CLS") != null)
        //        {
        //            #region CLS
        //            _listCLS = (from rs in
        //                            (from ds in
        //                                 (from ttct in
        //                                      (from bn in doc.Descendants("CHECKOUT") select bn).Descendants("THONGTINCHITIET")
        //                                  select ttct).Descendants("Bang_CT_CLS")
        //                             select ds).Descendants("CLS")
        //                        select new clsKetNoiXP.CT_CLS
        //                        {
        //                            Ma_lk = rs.Element("MA_LK") == null ? "" : rs.Element("MA_LK").Value,
        //                            Ma_dich_vu = rs.Element("MA_DICH_VU") == null ? "" : rs.Element("MA_DICH_VU").Value,
        //                            Ma_chi_so = rs.Element("MA_CHI_SO") == null ? "" : rs.Element("MA_CHI_SO").Value,
        //                            Ten_chi_so = rs.Element("TEN_CHI_SO") == null ? "" : rs.Element("TEN_CHI_SO").Value,
        //                            Gia_tri = rs.Element("GIA_TRI") == null ? "" : rs.Element("GIA_TRI").Value,
        //                            Ma_may = rs.Element("MA_MAY") == null ? "" : rs.Element("MA_MAY").Value,
        //                            Mo_ta = rs.Element("MO_TA") == null ? "" : rs.Element("MO_TA").Value,
        //                            Ket_luan = rs.Element("KET_LUAN") == null ? "" : rs.Element("KET_LUAN").Value,
        //                            Ngay_kq = rs.Element("NGAY_KQ") == null ? "" : rs.Element("NGAY_KQ").Value,
        //                        }).ToList();
        //            #endregion CLS
        //            return _listCLS;
        //        }
        //        else
        //        {
        //            return new List<CT_CLS>();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new List<CT_CLS>();
        //    }
        //}
        //public static List<clsKetNoiXP.DienBienBenh> fn_DienBienBenh(XDocument doc)
        //{

        //    List<clsKetNoiXP.DienBienBenh> _listDienBienBenh = new List<clsKetNoiXP.DienBienBenh>();
        //    try
        //    {
        //        if (doc.Element("CHECKOUT") != null && doc.Element("CHECKOUT").Element("THONGTINCHITIET") != null && doc.Element("CHECKOUT").Element("THONGTINCHITIET").Element("Bang_DienBienBenh") != null)
        //        {
        //            #region BANG_DIENBIENBENH
        //            _listDienBienBenh = (from rs in
        //                                     (from ds in
        //                                          (from ttct in
        //                                               (from bn in doc.Descendants("CHECKOUT") select bn).Descendants("THONGTINCHITIET")
        //                                           select ttct).Descendants("Bang_DienBienBenh")
        //                                      select ds).Descendants("DienBienBenh")
        //                                 select new clsKetNoiXP.DienBienBenh
        //                                 {
        //                                     Ma_lk = rs.Element("MA_LK") == null ? "" : rs.Element("MA_LK").Value,
        //                                     Dien_bien = rs.Element("DIEN_BIEN") == null ? "" : rs.Element("DIEN_BIEN").Value,
        //                                     Hoi_chan = rs.Element("HOI_CHAN") == null ? "" : rs.Element("HOI_CHAN").Value,
        //                                     Phau_thuat = rs.Element("PHAU_THUAT") == null ? "" : rs.Element("PHAU_THUAT").Value,
        //                                     Ngay_yl = rs.Element("NGAY_YL") == null ? "" : rs.Element("NGAY_YL").Value,
        //                                 }).ToList();
        //            #endregion CLS

        //            return _listDienBienBenh;
        //        }
        //        else
        //        {
        //            return new List<DienBienBenh>();
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        return new List<DienBienBenh>();
        //    }
        //}
        #endregion 
    }
    public class GetFile
    {
        #region biến
        static string strcon = "";
        private static string passql = "123456";
        private static string accountsql = "sa";
        private static string TenCSDL = "QLBV_PMEM";
        private static string TenServer = "MANHCUONG-PC\\SQLEXPRESS";
        static string fromPath = "";// thư mục lấy file dữ liệu
        static string desPath = "";// thư mục file di chuyển đến

        static List<Lib_QLBV_Connect_XP.clsKetNoiXP.ThongTinChung> _listThongTinChung = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.ThongTinChung>();
        static List<Lib_QLBV_Connect_XP.clsKetNoiXP.ThongTinBenhNhan> _listDSBN = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.ThongTinBenhNhan>();// thông tin chung của bệnh nhân
        static List<int> _listMaBN = new List<int>();//Danh sách mã bệnh nhân sai, sẽ không được import vào CSDL ( nhưng bệnh nhân có mã dịch vụ không có  tồn tại mã QD tương ứng trong danh mục dùng chung)
        static List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc> _listThuoc = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc>();
        static List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc> _listThuocNotExist = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc>();
        static List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV> _listDV = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV>();
        static List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV> _listDVNotExist = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV>();
        static List<Lib_QLBV_Connect_XP.clsKetNoiXP.CT_CLS> _listCLS = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CT_CLS>();
        static List<Lib_QLBV_Connect_XP.clsKetNoiXP.DienBienBenh> _listDienBienBenh = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.DienBienBenh>();
        static List<DichVu> _listDV_SQL = new List<DichVu>();
        private static Object[,] MangHaiChieu;
        #endregion biến
        public static void loadDanhSachBenhNhan()
        {
            passql = "123456";
            accountsql = "sa";
            TenCSDL = "QLBV_PMEM";
            TenServer = "MANHCUONG-PC\\SQLEXPRESS";
            strcon = @"metadata=res://*/QLBVEntity.csdl|res://*/QLBVEntity.ssdl|res://*/QLBVEntity.msl;provider=System.Data.SqlClient;provider connection string=';Data Source=" + TenServer + ";Initial Catalog=" + TenCSDL + ";User ID=" + accountsql + ";Password=" + passql + ";MultipleActiveResultSets=True';";
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(strcon);
            _listDV_SQL = data.DichVus.ToList();
            fromPath = "D:\\datagui";
            desPath = "D:\\databak";
            string pathExcel = "D:\\";
            pathExcel = pathExcel + "dichvu_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";
            _listDVNotExist = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV>();
            _listThuocNotExist = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc>();
            if (fromPath != "")
            {
                DirectoryInfo d = new DirectoryInfo(fromPath);//Assuming Test is your Folder
                FileInfo[] Files = d.GetFiles("*.xml"); //Getting Text files   
                List<FileInfo> _listFile = Files.OrderBy(p => p.LastWriteTime).ToList();
                foreach (FileInfo file in _listFile)
                {
                    if (file.Name.Contains("CHECKOUT"))
                    {
                        if (!readFileXML(file.Name))
                        {
                            foreach (var a in _listDSBN)
                            {
                                checkAndDelete(a.Ma_lk, a.Ma_cskcb);
                                BackFile(desPath, fromPath, file.Name);
                            }
                        }
                    }
                    else if (file.Name.Contains("DELETE"))
                    {
                        #region Lấy ra thông tin bệnh nhân cần xóa
                        XDocument doc = new XDocument();
                        bool kt = true;
                        _listDSBN = Lib_QLBV_Connect_XP.clsKetNoiXP.fn_ThongTinBenhNhan(doc);
                        foreach (var a in _listDSBN)
                        {
                            if (checkAndDelete(a.Ma_lk, a.Ma_cskcb))
                            {
                                MoveFile(fromPath, desPath, file.Name);
                            }
                        }
                        #endregion Lấy ra thông tin bệnh nhân cần xóa
                    }
                }
                #region XuatExcel danh mục thuốc lỗi
                _listThuocNotExist = _listThuocNotExist.OrderBy(p => p.Ma_lk).ToList();
                _listMaBN = _listMaBN.Distinct().ToList();
                xuatExcel(pathExcel);
                #endregion
            }
        }
        /// <summary>
        /// Đọc 1 file XML => lấy dữ liệu đổ ra list ds bệnh nhân, danh sách dịch vụ (thuốc , dịch vụ), danh sách diễn biến bệnh, list CLS 
        /// </summary>
        /// <param name="fileName">Tên file XML (VD: 201512280148_GD4301000801020_CHECKOUT.xml _ trong đó 201512280148 là ngày giờ vào, GD4301000801020 là mã thẻ )</param>
        /// <returns></returns>
        private static bool readFileXML(string fileName)
        {
            XDocument doc = new XDocument();
            bool kt = true;
            //_listThongTinChung = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.ThongTinChung>();
            //_listDSBN = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.ThongTinBenhNhan>();
            //_listThuoc = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc>();
            //_listDV = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV>();
            //_listCLS = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CT_CLS>();
            //_listDienBienBenh = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.DienBienBenh>();
            try
            {
                doc = XDocument.Load(fromPath + "\\" + fileName);
              //  _listThongTinChung = Lib_QLBV_Connect_XP.clsKetNoiXP.fn_ThongTinChung(doc);
                _listDSBN = Lib_QLBV_Connect_XP.clsKetNoiXP.fn_ThongTinBenhNhan(doc);
                _listThuoc = Lib_QLBV_Connect_XP.clsKetNoiXP.fn_CTThuoc(doc);
                _listDV = Lib_QLBV_Connect_XP.clsKetNoiXP.fn_CTDV(doc);
                _listCLS = Lib_QLBV_Connect_XP.clsKetNoiXP.fn_CTCLS(doc);
                _listDienBienBenh = Lib_QLBV_Connect_XP.clsKetNoiXP.fn_DienBienBenh(doc);
                foreach (Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV dv in _listDV)
                {
                    List<DichVu> q1 = _listDV_SQL.Where(p => p.MaQD == dv.Ma_dich_vu).ToList(); // thiếu so sánh kết hợp với tên dịch vụ
                    DichVu dichvu = new DichVu();
                    if (q1.Count > 0)
                    {
                        dichvu = q1.First();
                        dv.Ma_DV = dichvu.MaDV;
                    }
                    else // Chưa có dịch vụ có mã QD = dv.Ma_DV
                    {
                        _listDVNotExist.Add(dv);
                        _listMaBN.Add(Convert.ToInt32(dv.Ma_lk));
                        kt = false;
                    }
                }
                foreach (Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc thuoc in _listThuoc)
                {
                    List<DichVu> q1 = _listDV_SQL.Where(p => p.MaQD == thuoc.Ma_thuoc).ToList();
                    DichVu dichvu = new DichVu();
                    if (q1.Count > 0)
                    {
                        dichvu = q1.First();
                        thuoc.Ma_DV = dichvu.MaDV;
                    }
                    else // Chưa có dịch vụ có mã QD = dv.Ma_DV
                    {
                        _listThuocNotExist.Add(thuoc);
                        _listMaBN.Add(Convert.ToInt32(thuoc.Ma_lk));
                        kt = false;
                    }
                }
            }
            catch (Exception)
            {
                kt = false;
            }
            if (kt)
            {
                ImportXMLtoSQL();
                MoveFile(fromPath, desPath, fileName);
            }
            return kt;
        }
        /// <summary>
        /// Lấy mã KP từ Mã KPQĐ (KPQĐ là Mã bệnh viện đối với bệnh viện tuyến dưới)
        /// </summary>
        /// <param name="maKpQD"></param>
        /// <returns></returns>
        private static int getMaKP(string maKpQD)
        {
            int makp = 0;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(strcon);
            var qKP = data.KPhongs.Where(p => p.MaQD == maKpQD).ToList();
            if (qKP.Count > 0)
                makp = qKP.First().MaKP;
            return makp;

        }
        /// <summary>
        /// Xóa 1 bệnh nhân có  mã lk và ma_CSKCB cho trước
        /// </summary>
        /// <param name="ma_lk">Mã bệnh nhân tuyến dưới</param>
        private static bool checkAndDelete(string ma_lk, string ma_csKCB)
        {
            bool kt = true;
            try
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(strcon);
                List<BenhNhan> q = data.BenhNhans.Where(p => p.Ma_lk == ma_lk && p.MaBV == ma_csKCB).ToList();
                foreach (BenhNhan bn in q)
                {
                    int maBN = bn.MaBNhan;
                    data.BenhNhans.Remove(bn);
                    // xóa bảng đơn thuocs và đơn thuốc chi tiết
                    List<DThuoc> qdthuoc = data.DThuocs.Where(p => p.MaBNhan == maBN).ToList();
                    foreach (DThuoc dt in qdthuoc)
                    {
                        List<DThuocct> qdthuocct = data.DThuoccts.Where(p => p.IDDon == dt.IDDon).ToList();
                        foreach (DThuocct dtct in qdthuocct)
                            data.DThuoccts.Remove(dtct);
                        data.DThuocs.Remove(dt);
                    }

                    //xóa bảng viện phí và viện phí chi tiết
                    List<VienPhi> qvp = data.VienPhis.Where(p => p.MaBNhan == maBN).ToList();
                    foreach (VienPhi vp in qvp)
                    {
                        List<VienPhict> qvpct = data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi).ToList();
                        foreach (VienPhict vpct in qvpct)
                            data.VienPhicts.Remove(vpct);
                        data.VienPhis.Remove(vp);
                    }

                    //xóa bảng ra viện
                    List<RaVien> qrv = data.RaViens.Where(p => p.MaBNhan == maBN).ToList();
                    foreach (RaVien rv in qrv)
                    {
                        data.RaViens.Remove(rv);
                    }
                    data.SaveChanges();
                }
            }
            catch (Exception)
            {
                kt = false;
            }
            return kt;
        }
        /// <summary>
        /// Đổ vào CSDL danh sách bệnh nhân, thuốc, dịch vụ, CLS,  ra viện lấy từ 1 file XML (thiếu diễn biến bệnh)
        /// </summary>
        /// <returns></returns>
        private static bool ImportXMLtoSQL()
        {
            bool kt = true;
            try
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(strcon);
                int _maBN = 0;
                var qKPhong = data.KPhongs.ToList();
                var qDTBN = data.DTBNs.ToList();

                foreach (var n in _listDSBN)
                {

                    data = new QLBV_Database.QLBVEntities(strcon);
                    checkAndDelete(n.Ma_lk, n.Ma_cskcb);

                    #region add table BenhNhan
                    //   int _idBN = data.BenhNhans.Max(p => p.IDBNhan);
                    BenhNhan bn = new BenhNhan();
                    string[] formatDateTime = { "yyyyMMddHHmm" };
                    string[] fomatDate = { "yyyyMMdd" };
                    DateTime date;
                    DateTime hanbhtu = new DateTime();
                    DateTime hanbhden = new DateTime();
                    DateTime ngaytt = new DateTime();
                    string ngaysinh = "", thangsinh = "", namsinh = "";
                    // int maBN = 0;
                    bn.TenBNhan = n.Ho_ten;
                    bn.Tuoi = Convert.ToInt32(n.Ngay_vao.ToString().Substring(0, 4)) - Convert.ToInt32(n.Ngay_sinh.ToString().Substring(0, 4));
                    bn.GTinh = n.Gioi_tinh == 2 ? 0 : 1;
                    bn.DChi = n.Dia_chi;
                    bn.DTuong = String.IsNullOrEmpty(n.Ma_the) ? "Dịch vụ" : "BHYT";
                    if (n.Ngay_vao.ToString().Length == 12)
                        bn.NNhap = DateTime.ParseExact(n.Ngay_vao.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                    else if (n.Ngay_vao.ToString().Length == 8)
                        bn.NNhap = DateTime.ParseExact(n.Ngay_vao.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    bn.SThe = n.Ma_the;
                    bn.MaCS = n.Ma_dkbd;

                    if (n.Ma_loai_kcb == "3")
                        bn.NoiTru = 1;
                    else bn.NoiTru = 0;
                    //else if (n.Ma_loai_kcb == "2")
                    //    bn.NoiTru = 0;
                    //else if (n.Ma_loai_kcb == "1")// ??? Khám bệnh
                    //    bn.NoiTru = 0;
                    //  bn.MaKP = n.Ma_cskcb;
                    bn.MaBV = n.Ma_cskcb;

                    if (n.Ma_lydo_vvien == "3")// trái
                        bn.Tuyen = 2;
                    else if (n.Ma_lydo_vvien == "1")// đúng tuyến
                        bn.Tuyen = 1;// đúng tuyến
                    else if (n.Ma_lydo_vvien == "2")// cấp cứu
                        bn.Tuyen = 1;

                    bn.CapCuu = n.Ma_lydo_vvien == "2" ? 1 : 0;
                    // thiếu nội tỉnh
                    bn.Status = 3;
                    if (!String.IsNullOrEmpty(n.Gt_the_tu))
                    {
                        if (DateTime.TryParseExact(n.Gt_the_tu,
                                               fomatDate,
                                               System.Globalization.CultureInfo.InvariantCulture,
                                               System.Globalization.DateTimeStyles.None,
                                               out date))
                            hanbhtu = DateTime.ParseExact(n.Gt_the_tu.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                        bn.HanBHTu = hanbhtu;
                    }

                    if (!String.IsNullOrEmpty(n.Gt_the_den))
                    {
                        if (DateTime.TryParseExact(n.Gt_the_den,
                                               fomatDate,
                                               System.Globalization.CultureInfo.InvariantCulture,
                                               System.Globalization.DateTimeStyles.None,
                                               out date))
                            hanbhden = DateTime.ParseExact(n.Gt_the_den.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                        bn.HanBHDen = hanbhden;

                    }
                    if (n.Ngay_sinh != null)
                    {
                        if (n.Ngay_sinh.Length == 8)
                        {
                            ngaysinh = n.Ngay_sinh.ToString().Substring(6, 2);
                            thangsinh = n.Ngay_sinh.ToString().Substring(4, 2);
                            namsinh = n.Ngay_sinh.ToString().Substring(0, 4);
                            bn.NgaySinh = ngaysinh;
                            bn.ThangSinh = thangsinh;
                            bn.NamSinh = namsinh;
                        }
                        else if (n.Ngay_sinh.Length == 4)
                        {
                            namsinh = n.Ngay_sinh;
                            bn.NamSinh = namsinh;
                        }
                    }
                    bn.MaBV = n.Ma_cskcb;

                    if (qKPhong.Where(p => p.MaQD == n.Ma_cskcb && p.PLoai == "Xã phường").Count() > 0)
                    {
                        bn.TuyenDuoi = 1;
                        bn.MaKP = qKPhong.Where(p => p.MaQD == n.Ma_cskcb && p.PLoai == "Xã phường").First().MaKP;
                    }
                    else if (qKPhong.Where(p => p.MaQD == n.Ma_cskcb && p.PLoai == "PK khu vực").Count() > 0)
                    {
                        bn.TuyenDuoi = 2;
                        bn.MaKP = qKPhong.Where(p => p.MaQD == n.Ma_cskcb && p.PLoai == "PK khu vực").First().MaKP;
                    }

                    if (n.Ma_the != null && n.Ma_the.Length > 2)
                        bn.MucHuong = Convert.ToInt16(n.Ma_the.ToString().Substring(2, 1));
                    bn.KhuVuc = n.Ma_khuvuc;

                    if (n.Ma_loai_kcb == "2")
                        bn.DTNT = true;
                    else// if (n.Ma_loai_kcb == "1")// ??? Khám bệnh                
                        bn.DTNT = false;

                    bn.Ma_lk = n.Ma_lk;


                    if (n.Ma_the != null)
                    {
                        if (n.Ma_the.Length > 1)
                        {
                            bn.MaDTuong = n.Ma_the.ToString().Substring(0, 2);
                        }
                        // id person
                        var qP = data.People.Where(p => p.SThe == n.Ma_the).ToList();
                        if (qP.Count > 0)
                        {
                            bn.IDPerson = qP.OrderByDescending(p => p.IDPerson).First().IDPerson;
                        }
                        else
                        {
                            // insert vào bảng person
                            Person ps = new Person();
                            ps.SThe = n.Ma_the;
                            if (n.Ma_the.Length > 1)
                            {
                                ps.MaDTuong = n.Ma_the.ToString().Substring(0, 2);
                            }
                            ps.TenBNhan = n.Ten_benh;
                            ps.GTinh = n.Gioi_tinh == 2 ? 0 : 1;
                            ps.DChi = n.Dia_chi;
                            ps.HanBHTu = hanbhtu;
                            ps.HanBHDen = hanbhden;
                            ps.MaCS = n.Ma_dkbd;
                            int num;
                            if (Int32.TryParse(namsinh, out num))
                                ps.NSinh = Convert.ToInt32(namsinh);
                            ps.NgaySinh = ngaysinh;
                            ps.ThangSinh = thangsinh;
                            ps.KhuVuc = n.Ma_khuvuc;
                            //thiếu ngày cấp, Status, mã tỉnh, mã huyện, mã xã
                            data.People.Add(ps);
                            if (data.SaveChanges() > 0)
                            {
                                int idPs = data.People.OrderByDescending(p => p.IDPerson).First().IDPerson;
                                bn.IDPerson = idPs;
                            }
                        }
                    }
                    //idDTBN
                    if (n.Ma_the != null)
                    {
                        if (qDTBN.Where(p => p.DTBN1 == "BHYT").Count() > 0)
                            bn.IDDTBN = qDTBN.Where(p => p.DTBN1 == "BHYT").First().IDDTBN;
                    }
                    else
                    {
                        if (qDTBN.Where(p => p.DTBN1 == "Dịch vụ").Count() > 0)
                            bn.IDDTBN = qDTBN.Where(p => p.DTBN1 == "Dịch vụ").First().IDDTBN;
                    }
                    data.BenhNhans.Add(bn);
                    #endregion add table BenhNhan
                    if (data.SaveChanges() > 0)
                    {
                        _maBN = data.BenhNhans.Where(p => p.Ma_lk == n.Ma_lk && p.MaBV == n.Ma_cskcb).OrderByDescending(p => p.MaBNhan).First().MaBNhan;
                        #region add table DThuoc

                        var qdonThuocCT = _listThuoc.Where(p => p.Ma_lk == n.Ma_lk).ToList();
                        var qDichvuCT = _listDV.Where(p => p.Ma_lk == n.Ma_lk).ToList();
                        var qdonThuoc = (from a in qdonThuocCT
                                         group a by new { a.Ngay_yl } into kq
                                         select new
                                         {
                                             kq.Key.Ngay_yl,
                                             Ma_bac_si = kq.Max(p => p.Ma_bac_si),
                                             Ma_khoa = kq.Max(p => p.Ma_khoa)
                                         }
                                        ).ToList();

                        var qDichvu = (from a in qDichvuCT
                                       group a by new { a.Ngay_yl } into kq
                                       select new
                                       {
                                           kq.Key.Ngay_yl,
                                           Ma_bac_si = kq.Max(p => p.Ma_bac_si),
                                           Ma_khoa = kq.Max(p => p.Ma_khoa)
                                       }
                                        ).ToList();


                        foreach (var m in qdonThuoc)
                        {
                            DThuoc don = new DThuoc();
                            don.MaKP = getMaKP(m.Ma_khoa);
                            don.MaBNhan = _maBN;
                            don.MaCB = m.Ma_bac_si;
                            if (!String.IsNullOrEmpty(m.Ngay_yl))
                            {
                                if (m.Ngay_yl.ToString().Length == 12)
                                    don.NgayKe = DateTime.ParseExact(m.Ngay_yl.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                else if (m.Ngay_yl.ToString().Length == 8)
                                    don.NgayKe = DateTime.ParseExact(m.Ngay_yl.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                            }
                            don.MaKXuat = 0;
                            //don.Status = -1;
                            don.KieuDon = -1;
                            don.LoaiDuoc = -1;
                            //don.SoPL = -1;
                            don.PLDV = 1;
                            data.DThuocs.Add(don);
                            if (data.SaveChanges() > 0)
                            {
                                #region add table DThuocct
                                int _idDon = data.DThuocs.Where(p => p.MaBNhan == _maBN).OrderByDescending(p => p.IDDon).First().IDDon;
                                var qdonCTByIDDon = qdonThuocCT.Where(p => p.Ngay_yl == m.Ngay_yl).ToList();
                                foreach (var o in qdonCTByIDDon)
                                {
                                    DThuocct donct = new DThuocct();
                                    donct.IDDon = _idDon;
                                    //  donct.MaDV = o.Ma_thuoc;
                                    donct.MaDV = o.Ma_DV;
                                    donct.DonVi = o.Don_vi_tinh;
                                    donct.DonGia = o.Don_gia;
                                    donct.SoLuong = o.So_luong;
                                    double _tienbh = Math.Round(Math.Round(o.Thanh_tien, 3) * n.Muc_huong / 100, 3);
                                    double _thanhtien = Math.Round(o.Thanh_tien, 3);
                                    donct.ThanhTien = _thanhtien;
                                    donct.TienBH = _tienbh;
                                    donct.TienBN = _thanhtien - _tienbh;
                                    donct.TrongBH = n.Muc_huong > 0 ? 1 : 0; //????????
                                    data.DThuoccts.Add(donct);
                                    if (data.SaveChanges() <= 0)
                                        kt = false;
                                }

                                #endregion add table DThuocct
                            }
                            else
                                kt = false;
                        }
                        #region add dịch vụ
                        foreach (var m in qDichvu)
                        {
                            DThuoc don = new DThuoc();
                            // don.MaKP = m.Ma_khoa;

                            don.MaBNhan = _maBN;
                            don.MaCB = m.Ma_bac_si;
                            if (!String.IsNullOrEmpty(m.Ngay_yl))
                            {
                                if (m.Ngay_yl.ToString().Length == 12)
                                    don.NgayKe = DateTime.ParseExact(m.Ngay_yl.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                else if (m.Ngay_yl.ToString().Length == 8)
                                    don.NgayKe = DateTime.ParseExact(m.Ngay_yl.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                            }
                            don.MaKXuat = 0;
                            //don.Status = -1;
                            don.KieuDon = -1;
                            don.LoaiDuoc = -1;
                            //don.SoPL = -1;
                            don.PLDV = 2;
                            data.DThuocs.Add(don);
                            if (data.SaveChanges() > 0)
                            {
                                #region add table DThuocct
                                data = new QLBV_Database.QLBVEntities(strcon);
                                int _idDon = data.DThuocs.Where(p => p.MaBNhan == _maBN).OrderByDescending(p => p.IDDon).First().IDDon;
                                var qdonCTDVByIDDon = qDichvuCT.Where(p => p.Ngay_yl == m.Ngay_yl).ToList();
                                foreach (var o in qdonCTDVByIDDon)
                                {
                                    DThuocct donct = new DThuocct();
                                    donct.IDDon = _idDon;
                                    // donct.MaDV = o.Ma_dich_vu;
                                    donct.MaDV = o.Ma_DV;
                                    donct.DonVi = o.Don_vi_tinh;
                                    donct.DonGia = o.Don_gia;
                                    donct.SoLuong = o.So_luong;
                                    double _tienbh = Math.Round(Math.Round(o.Thanh_tien, 3) * n.Muc_huong / 100, 3);
                                    double _thanhtien = Math.Round(o.Thanh_tien, 3);
                                    donct.ThanhTien = _thanhtien;
                                    donct.TienBH = _tienbh;
                                    donct.TienBN = _thanhtien - _tienbh;
                                    donct.TrongBH = n.Muc_huong > 0 ? 1 : 0; //????????
                                    data.DThuoccts.Add(donct);
                                    if (data.SaveChanges() <= 0)
                                        kt = false;

                                }
                                #endregion add table DThuocct
                            }
                            else
                                kt = false;
                        }
                        #endregion add dịch vụ
                        #endregion add table DThuoc
                        #region add table VienPhi
                        VienPhi vp = new VienPhi();
                        if (!String.IsNullOrEmpty(n.Ngay_ttoan))
                        {
                            if (n.Ngay_ttoan.ToString().Length == 12)
                                vp.NgayTT = DateTime.ParseExact(n.Ngay_ttoan.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                            else if (n.Ngay_ttoan.ToString().Length == 8)
                                vp.NgayTT = DateTime.ParseExact(n.Ngay_ttoan.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                        }
                        vp.MaBNhan = _maBN;
                        // vp.MaKP = n.Ma_cskcb;
                        // vp.MucHuong = Convert.ToByte(n.Muc_huong);
                        data.VienPhis.Add(vp);
                        if (data.SaveChanges() > 0)
                        {
                            int _idVP = data.VienPhis.Where(p => p.MaBNhan == _maBN).Where(p => p.MaBNhan == _maBN).OrderByDescending(p => p.idVPhi).First().idVPhi;
                            #region add table VienPhict
                            foreach (var o in qdonThuocCT)
                            {
                                VienPhict vpct = new VienPhict();
                                vpct.idVPhi = _idVP;
                                // vpct.MaDV = o.Ma_thuoc;
                                vpct.MaDV = o.Ma_DV;
                                vpct.DonVi = o.Don_vi_tinh;
                                vpct.DonGia = o.Don_gia;
                                vpct.SoLuong = o.So_luong;
                                double _tienbh = Math.Round(Math.Round(o.Thanh_tien, 3) * n.Muc_huong / 100, 3);
                                double _thanhtien = Math.Round(o.Thanh_tien, 3);
                                vpct.ThanhTien = _thanhtien;
                                vpct.TienBH = _tienbh;
                                vpct.TienBN = _thanhtien - _tienbh;
                                vpct.TrongBH = n.Muc_huong > 0 ? 1 : 0;
                                //vpct.MaKP = o.Ma_khoa;
                                data.VienPhicts.Add(vpct);
                                if (data.SaveChanges() <= 0)
                                    kt = false;
                                // MessageBox.Show("Lỗi thêm mới viện phí chi tiết bệnh nhân " + n.Ho_ten + " (" + n.Ma_bn + ")_Mã bệnh nhân :" + _maBN);
                            }
                            foreach (var o in qDichvuCT)
                            {
                                VienPhict vpct = new VienPhict();
                                vpct.idVPhi = _idVP;
                                // vpct.MaDV = o.Ma_dich_vu;
                                vpct.MaDV = o.Ma_DV;
                                vpct.DonVi = o.Don_vi_tinh;
                                vpct.DonGia = o.Don_gia;
                                vpct.SoLuong = o.So_luong;
                                double _tienbh = Math.Round(Math.Round(o.Thanh_tien, 3) * n.Muc_huong / 100, 3);
                                double _thanhtien = Math.Round(o.Thanh_tien, 3);
                                vpct.ThanhTien = _thanhtien;
                                vpct.TienBH = _tienbh;
                                vpct.TienBN = _thanhtien - _tienbh;
                                vpct.TrongBH = n.Muc_huong > 0 ? 1 : 0;
                                // vpct.MaKP = o.Ma_khoa;
                                data.VienPhicts.Add(vpct);
                                if (data.SaveChanges() <= 0)
                                    kt = false;
                                //  MessageBox.Show("Lỗi thêm mới viện phí chi tiết bệnh nhân " + n.Ho_ten + " (" + n.Ma_bn + ")_Mã bệnh nhân :" + _maBN);
                            }
                            #endregion add table VienPhict
                        }
                        else
                        {
                            kt = false;
                            //MessageBox.Show("Lỗi thêm mới viện phí bệnh nhân " + n.Ho_ten + " (" + n.Ma_bn + ")_Mã bệnh nhân :" + _maBN);
                        }
                        #endregion add table VienPhi
                        #region add table Ravien
                        RaVien rv = new RaVien();
                        if (!String.IsNullOrEmpty(n.Ngay_ra))
                        {
                            if (n.Ngay_ra.ToString().Length == 12)
                                rv.NgayRa = DateTime.ParseExact(n.Ngay_ra.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                            else if (n.Ngay_ra.ToString().Length == 8)
                                rv.NgayRa = DateTime.ParseExact(n.Ngay_ra.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                        }
                        rv.MaBNhan = _maBN;
                        rv.SoNgaydt = n.So_ngay_dtri;
                        if (n.Tinh_trang_rv == "2")
                            rv.Status = 1;
                        else if (n.Tinh_trang_rv == "1")
                            rv.Status = 2;
                        else if (n.Tinh_trang_rv == "3" || n.Tinh_trang_rv == "4")
                            rv.Status = Convert.ToInt16(n.Tinh_trang_rv);
                        // rv.MaKP = n.Ma_cskcb;
                        rv.MaICD = n.Ma_benhkhac == "" ? n.Ma_benh : (n.Ma_benh + ";" + n.Ma_benhkhac);
                        rv.ChanDoan = n.Ten_benh;
                        if (n.Ket_qua_dtri == "1")
                            rv.KetQua = "Khỏi";
                        else if (n.Ket_qua_dtri == "2")
                            rv.KetQua = "Đỡ|Giảm";
                        else if (n.Ket_qua_dtri == "3")
                            rv.KetQua = "Không T.đổi";
                        else if (n.Ket_qua_dtri == "4")
                            rv.KetQua = "Nặng hơn";
                        else if (n.Ket_qua_dtri == "5")
                            rv.KetQua = "Tử vong";
                        if (!String.IsNullOrEmpty(n.Ngay_vao))
                        {
                            if (n.Ngay_vao.ToString().Length == 12)
                                rv.NgayVao = DateTime.ParseExact(n.Ngay_vao.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                            else if (n.Ngay_vao.ToString().Length == 8)
                                rv.NgayVao = DateTime.ParseExact(n.Ngay_vao.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                        }
                        data.RaViens.Add(rv);
                        if (data.SaveChanges() <= 0)
                            kt = false;
                        //MessageBox.Show("Lỗi thêm mới Bảng ra viện bệnh nhân: " + n.Ho_ten + " (" + n.Ma_bn + ")_Mã bệnh nhân :" + _maBN);

                        #endregion add table Ravien
                    }
                    else
                    {
                        kt = false;
                    }

                }
            }
            catch (Exception)
            { kt = false; }
            return kt;
        }
        /// <summary>
        /// Di chuyển file
        /// </summary>
        /// <param name="fromPath">Thư mục file nhận</param>
        /// <param name="desPath">Thư mục đến</param>
        /// <param name="fileName">Tên file di chuyển</param>
        private static void MoveFile(string fromPath, string desPath, string fileName)
        {
            DirectoryInfo d = new DirectoryInfo(fromPath);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.xml"); //Getting Text files 
            foreach (FileInfo file in Files)
            {
                if (file.Name == fileName)
                {
                    if (System.IO.File.Exists(desPath + "\\" + fileName))
                    {
                        System.IO.File.Delete(desPath + "\\" + fileName);
                    }
                    File.Move(file.FullName, desPath + "\\" + fileName);
                    break;
                }
            }
        }

        /// <summary>
        /// Xuất ra file excel các dịch vụ không tồn tại mã QĐ hợp lệ
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool xuatExcel(string path)
        {
            bool rs = false;
            List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV> _listDV1 = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV>();
            // gọi ứng dụng excel--------------------------------------------------------------------------------------------------
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);
            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
            string tenbieu = "DVSai";
            exSheet.Name = tenbieu;

            // mảng tên cột--------------------------------------------------------------------------------------------------------
            string[] _arr;
            _arr = new string[] { "stt", "ma_bn", "ma_dv", "ten_dv" };

            // add dữ liệu từ 2 list _listThuocNotExist và _listDVNotExist vào 1 list chung _listDV1--------------------------------
            foreach (var item in _listThuocNotExist)
            {
                Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV moi = new clsKetNoiXP.CTDV();
                moi.Ma_lk = item.Ma_lk;
                moi.Ma_dich_vu = item.Ma_thuoc;
                moi.Ten_dich_vu = item.Ten_thuoc;
                _listDV1.Add(moi);
            }

            foreach (var item in _listDVNotExist)
            {
                // item.Ma_cskcb = item.Ma_cskcb;
                Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV moi = new clsKetNoiXP.CTDV();
                moi.Ma_lk = item.Ma_lk;
                moi.Ma_dich_vu = item.Ma_dich_vu;
                moi.Ten_dich_vu = item.Ten_dich_vu;
                _listDV1.Add(moi);
            }

            _listDV1 = (from a in _listDV1
                        group a by new { a.Ma_dich_vu, a.Ten_dich_vu } into kq
                        select new clsKetNoiXP.CTDV
                        {
                            Ma_lk = String.Join(",", kq.Select(p => p.Ma_lk).Distinct()),
                            Ma_dich_vu = kq.Key.Ma_dich_vu,
                            Ten_dich_vu = kq.Key.Ten_dich_vu
                        }).ToList();


            // add tên cột vào sheet exSheet---------------------------------------------------------------------------------------
            int k = 0;
            foreach (var b in _arr)
            {
                k++;
                COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                r.Value2 = b.ToString();
                r.Columns.AutoFit();
                r.Cells.Font.Bold = true;
            }

            // đổ dữ liệu từ list vào mảng 2 chiều---------------------------------------------------------------------------------
            int row = _listDV1.Count;
            int col = _arr.Length;
            Object[,] _arr2 = new Object[row, col]; // tạo ra mảng count, col phần tử


            if (row > 0)
            {
                int num = 0;

                foreach (var item in _listDV1)
                {
                    // item.Ma_cskcb = item.Ma_cskcb;
                    _arr2[num, 0] = num + 1;
                    _arr2[num, 1] = item.Ma_lk;
                    _arr2[num, 2] = item.Ma_dich_vu;
                    _arr2[num, 3] = item.Ten_dich_vu;
                    num++;
                }
                exSheet.Range[exSheet.Cells[2, 1], exSheet.Cells[row + 1, col]].NumberFormat = "@";// ma_dkbd
                //-------------------------------------------------------------------------------------------
                exSheet.Range[exSheet.Cells[2, 1], exSheet.Cells[row + 1, col]].Value = _arr2;
                //  exApp.Visible = true;
                exApp.DisplayAlerts = false;
                try
                {

                    exQLBV.SaveAs(path, COMExcel.XlFileFormat.xlWorkbookNormal,
                                    null, null, false, false,
                                    COMExcel.XlSaveAsAccessMode.xlExclusive,
                                    false, false, false, false, false);
                    rs = true;
                }
                catch (Exception ex)
                {
                    rs = false;
                }
                finally
                {
                    exQLBV.Close(true, false, false);
                    exApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                }
            }

            return rs;
        }

        /// <summary>
        /// Chuyển file trở lại thư mục gửi từ thư mục đích
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="desPath"></param>
        /// <param name="fileName"></param>
        private static void BackFile(string desPath, string fromPath, string fileName)
        {
            DirectoryInfo d = new DirectoryInfo(desPath);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.xml"); //Getting Text files 
            foreach (FileInfo file in Files)
            {
                if (file.Name == fileName)
                {
                    if (!System.IO.File.Exists(fromPath + "\\" + fileName))
                    {
                        File.Move(file.FullName, desPath + "\\" + fileName);
                    }
                    break;

                }
            }
        }
    }
}
