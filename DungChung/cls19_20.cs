using System;using QLBV_Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLBV.DungChung
{
    class cls19_20
    {
        private string tenrg; // mã khám bệnh

        public string Tenrg
        {
            get { return tenrg; }
            set { tenrg = value; }
        }
        string ma_CSKCB;

        public string Ma_CSKCB
        {
            get { return ma_CSKCB; }
            set { ma_CSKCB = value; }
        }
        private string ma_lk; // mã khám bệnh

        public string Ma_lk
        {
            get { return ma_lk; }
            set { ma_lk = value; }
        }
        string soQD;

        public string SoQD
        {
            get { return soQD; }
            set { soQD = value; }
        }
        private string ma_thuoc;

        public string Ma_thuoc
        {
            get { return ma_thuoc; }
            set { ma_thuoc = value; }
        }
        private int stt;

        public int Stt
        {
            get { return stt; }
            set { stt = value; }
        }
        private int ma_nhom;

        public int Ma_nhom
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
        private string duong_dung;// đường dùng

        public string Duong_dung
        {
            get { return duong_dung; }
            set { duong_dung = value; }
        }
        private string ma_DuongD;// mã đường dùng

        public string Ma_DuongD
        {
            get { return ma_DuongD; }
            set { ma_DuongD = value; }
        }

        private string so_dang_ky;// số đăng ký

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
       
        private double thanhtien_noitru;

        public double Thanhtien_noitru
        {
            get { return thanhtien_noitru; }
            set { thanhtien_noitru = value; }
        }
        private double thanhtien_ngtru;

        public double Thanhtien_ngtru
        {
            get { return thanhtien_ngtru; }
            set { thanhtien_ngtru = value; }
        }
        private double don_gia;

        public double Don_gia
        {
            get { return don_gia; }
            set { don_gia = value; }
        }
        private double don_giaBHYT;

        public double Don_giaBHYT
        {
            get { return don_giaBHYT; }
            set { don_giaBHYT = value; }
        }

        private double don_gia_tt39;

        public double Don_gia_tt39
        {
            get { return don_gia_tt39; }
            set { don_gia_tt39 = value; }
        }
        private string tyle_tt; // tỷ lệ thanh toán của BHYT đối với thuốc

        public string Tyle_tt
        {
            get { return tyle_tt; }
            set { tyle_tt = value; }
        }
        private double thanh_tien; // số lượng * đơn giá * tyleTt

        public double Thanh_tien
        {
            get { return thanh_tien; }
            set { thanh_tien = value; }
        }
        private double thanh_tien_BHYT; // số lượng * đơn giá * tyleTt

        public double Thanh_tien_BHYT
        {
            get { return thanh_tien_BHYT; }
            set { thanh_tien_BHYT = value; }
        }
        private string ma_khoa;

        public string Ma_khoa
        {
            get { return ma_khoa; }
            set { ma_khoa = value; }
        }
        private string ma_ba_sy; // bác sỹ chỉ định

        public string Ma_ba_sy
        {
            get { return ma_ba_sy; }
            set { ma_ba_sy = value; }
        }
        private string ma_benh;// nếu có nhiều bệnh cách nhau bằng dấu ";"

        public string Ma_benh
        {
            get { return ma_benh; }
            set { ma_benh = value; }
        }
        private string ngay_yl;// ngày y lệnh yyyymmddhhmm;

        public string Ngay_yl
        {
            get { return ngay_yl; }
            set { ngay_yl = value; }
        }

        private string ngay_kq;// ngày có kết quả

        public string Ngay_kq
        {
            get { return ngay_kq; }
            set { ngay_kq = value; }
        }

        // phần thêm ngoài công văn 2348
        private int trongBH;

        public int TrongBH
        {
            get { return trongBH; }
            set { trongBH = value; }
        }
        private string soTTqd;

        public string SoTTqd
        {
            get { return soTTqd; }
            set { soTTqd = value; }
        }
        private string tennhom;

        public string Tennhom
        {
            get { return tennhom; }
            set { tennhom = value; }
        }
        private string tenTN;

        public string TenTN
        {
            get { return tenTN; }
            set { tenTN = value; }
        }
        private string tenHC;

        public string TenHC
        {
            get { return tenHC; }
            set { tenHC = value; }
        }
        private string maQD;

        public string MaQD
        {
            get { return maQD; }
            set { maQD = value; }
        }
        private double soluongNT;

        public double SoluongNT
        {
            get { return soluongNT; }
            set { soluongNT = value; }
        }
        private double soluongNgT;

        public double SoluongNgT
        {
            get { return soluongNgT; }
            set { soluongNgT = value; }
        }
        private string maCC;

        public string MaCC
        {
            get { return maCC; }
            set { maCC = value; }
        }
        private string qcpc;

        public string QCPC
        {
            get { return qcpc; }
            set { qcpc = value; }
        }
        private double soLuong139;

        public double SoLuong139
        {
            get { return soLuong139; }
            set { soLuong139 = value; }
        }
        private double soLuongTE;

        public double SoLuongTE
        {
            get { return soLuongTE; }
            set { soLuongTE = value; }
        }
        private double soLuongBHYT_DV;

        public double SoLuongBHYT_DV
        {
            get { return soLuongBHYT_DV; }
            set { soLuongBHYT_DV = value; }
        }
        private string nuocSX;

        public string NuocSX
        {
            get { return nuocSX; }
            set { nuocSX = value; }
        }
        private string hangsx;

        public string Hangsx
        {
            get { return hangsx; }
            set { hangsx = value; }
        }
        private string dangBC;

        public string DangBC
        {
            get { return dangBC; }
            set { dangBC = value; }
        }
        private double giaThau;// giá thầu

        public double GiaThau
        {
            get { return giaThau; }
            set { giaThau = value; }
        }
        private string matam;

        public string Matam
        {
            get { return matam; }
            set { matam = value; }
        }
        private int? maNhom_5937;
        public int MaNhom5937
        {
            get { return maNhom_5937.Value; }
            set { maNhom_5937 = value; }
        }

        public int SapXep { get; set; }
        public string TenSapXep { get; set; }

        public double TyLeTT { get; set; }

        public double TienBH { get; set; }
        public string TenTieuNhom { get; set; }
        public int STTTieuNhom { get; set; }
        public string STTNhomHT { get; set; }
    }
}
