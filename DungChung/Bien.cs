using QLBV.Utilities.Commons;
using System;
using QLBV_Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLBV.DungChung
{
    public static class Bien
    {
        //minhvd
        public static int CheckInFull = 0; //1 in thuong CDHA, 2 in full CDHA, 3 in trong cls, 4 xem phieu trong cls
        public static int MauIn = 0; //cho viện 30372
        public static string checkcls = "";
        public static bool CheckbtnVsip = false;
        public static bool Check_CV3762 = false;
        public static bool Select_InDoc = false;
        public static string TenCQCQ = "";
        public static string DiaDanh = "";
        public static string TenCQrg = "";
        public static string MaCQ = "";
        public static string TenBV = "Phòng khám Đa khoa KCN VSIP";
        public static string DiaChiBV = "Ngã tư, Xóm Gạ, Đại Vy, Tiên Du, Bắc Ninh";
        public static string SDT = "0989770704";
        public static string TieuDeSoTiepDon = "SỔ TIẾP ĐÓN BỆNH NHÂN";
        public static string formInDon = "";
        public static int _tuyenduoi = 0;//0 bệnh viện, 1 XP, 2 PK KVS
        //public static string MaBV = "04006";
        //public static string TenCQ = "Bệnh viện đa khoa Trùng Khánh";
        //public static string DiaChi = "Tổ 9 tt Trùng Khánh, Huyện Trùng Khánh, Tỉnh Cao Bằng";
        //public static string MaBV = "04007";
        //public static string TenCQ = "Bệnh viện đa khoa Nguyên Bình";
        //public static string DiaChi = "Tổ dân phố 2 tt Nguyên Bình Huyện Nguyên Bình Tỉnh Cao Bằng";
        //public static string MaBV = "04008";
        //public static string TenCQ = "Bệnh viện đa khoa Hòa An";
        //public static string DiaChi = "";
        //public static string MaBV = "04011";
        //public static string TenCQ = "Bệnh viện đa khoa huyện Hạ Lang";
        //public static string DiaChi = "Xóm Đoỏng Hoan- thị trấn Thanh Nhật-huyện Hạ Lang";
        //public static string MaBV = "04004";
        //public static string TenCQ = "Bệnh viện đa khoa Hà Quảng";
        //public static string DiaChi = "";
        //public static string MaBV = "06007";
        //public static string TenCQ = "Trung tâm y tế huyện Chợ Mới";
        //public static string DiaChi = "";
        public static string MaBV = "12122";
        public static string TenCQ = "Bệnh viện đa khoa huyện Bình Giang";
        public static string DiaChi = "";
        public static bool ChiTamUng = false;

        public static string MaBenhVien = "";
        public static string TenBenhVien = "PHÒNG KHÁM ĐA KHOA HÀ NỘI BẮC NINH";
        public static string SDTBenhVien1 = "0984 668 965";
        public static string SDTBenhVien2 = "0912 652 400";
        public static string SDTBenhVien3 = "0222 3907 115";
        public static string DiaChiBenhVien = "Đường 295 đối diện KCN Đại Đồng - Hoàn Sơn - Tiên Du - Bắc Ninh";

        #region soncx
        public static string LogoPath = @"\Logo\logo.png";
        public static string ReportPath = AppDomain.CurrentDomain.BaseDirectory + @"Reports";
        public static string SoundPath = @"\Sound";
        //User password hóa đơn điện tử
        public static string UserPasswordHDDT = "";
        public static string UrlPID = "";
        public static string UrlUploadHSSK = "";
        public static string URL_BHXH = "http://egw.baohiemxahoi.gov.vn";
        public static string URL_BHXH_DaoTao = "http://wsdt.baohiemxahoi.gov.vn";

        public static string UrserNameBHXH = "";
        public static string PassWordBHXK = "";
        #region QLBV Config
        //Số bệnh nhân hiển thị trên màn hình gọi bệnh nhân
        public static int SoBenhNhanHienThi = 10;
        //Url kết nối pos agribank
        public static string URL_POS_AGRIGANK = "";
        //Tham số truyền vào POS
        public static string F1 = "";
        public static string F2 = "";
        public static string F3 = "";
        public static string F4 = "";
        public static string F5 = "";
        #endregion
        #endregion
        //public static string MaBV = "24009";
        //public static string TenCQ = "Bệnh viện đa khoa Việt Yên";
        //public static string DiaChi = "";
        //public static string MaBV = "08602";
        //public static string TenCQ = "Bệnh viện đa khoa huyện Na Hang";
        //public static string DiaChi = "";
        //public static string MaBV = "02005";
        //public static string TenCQ = "Bệnh viện đa khoa Vị Xuyên";
        //public static string DiaChi = "";
        //public static string MaBV = "";
        //public static string TenCQ = "Bệnh viện đa khoa Tân Uyên";
        //public static string DiaChi = "";
        public static int LamTronSo = 0;
        public static string TenCQBH = "";
        public static string TenCQCQBH = "";
        public static string MaHuyen = "";
        public static string MaTinh = "";
        public static string GiamDoc = "";
        public static int MaKP = 0;
        public static string PLoaiKP = "-1";
        public static string ChuyenKhoa = "";
        public static string MaCB = "0";
        public static string TenDN = "";
        public static int ID_LOGIN_LOG = 0;
        public static int GHanTT100 = 172500;
        public static bool CoTheChuyen = true;
        public static string TruongKhoaDuoc = "";
        public static string KeToanTruong = "";
        public static string KeToanVP = "";
        public static string KeToanVPnt = "";
        public static string NguoiLapBieu = "";
        public static string ThuKho = "";
        public static string GiamDinhBH = "";
        public static string GiamDinhBH2 = "";
        public static int PP_SoVV = 0;
        public static int PP_SoRV = 0;
        public static int PP_SoCV = 0;
        public static int PP_SoLT = 0;
        public static int PP_SoBA = 0;
        public static int PP_SoKB = 0;
        public static int PPTinhTon = 0;
        public static string SDTCQ = "";
        /// <summary>
        /// 0: gửi thủ công, 1: gửi tự động khi duyệt chi phí
        /// 0: ngoại trú; 1: nội trú-- dung
        /// </summary>
        public static int[] PPXuat_BHXH = new int[2] { -1, -1 };
        /// <summary>
        /// [0]: giờ bắt đầu buổi sáng
        /// [1]: giờ bắt đầu buổi chiều
        /// </summary>
        public static int[] GioTu = new int[2] { 07, 13 };
        /// <summary>
        /// [0]: phút bắt đầu buổi sáng
        /// [1]: phút bắt đầu buổi chiều
        /// </summary>
        /// 
        public static int[] PhutTu = new int[2] { 30, 30 };
        /// <summary>
        /// [0]: giờ kết thúc buổi sáng
        /// [1]: giờ kết thúc buổi chiều
        /// </summary>
        /// 
        public static int[] GioDen = new int[2] { 11, 17 };
        public static int[] PhutDen = new int[2] { 30, 30 };
        public static double SoLuongTon = 0;
        public static DateTime? HanDung = null;
        public static DateTime NgayKichHoat = System.DateTime.Now;
        public static DateTime NgayTH = System.DateTime.Now;
        public static int check = 0;
        public static int maidcls = 0;
        public static bool _checkpass = false;
        public static string TruongKhoaLS = "";
        public static string passql = "vss@2018";
        public static string accountsql = "sa";
        public static string TenCSDL = "QLBV";
        public static string TenServer = "server";

        private static string _strCon = @"metadata=res://*/QLBVEntity.csdl|res://*/QLBVEntity.ssdl|res://*/QLBVEntity.msl;provider=System.Data.SqlClient;provider connection string=';Data Source=" + TenServer + ";Initial Catalog=" + TenCSDL + ";User ID=" + accountsql + ";Password=" + passql + ";MultipleActiveResultSets=True';";
        public static string StrCon
        {
            get { return _strCon; }
            set
            {
                if (_strCon != value)
                {
                    _strCon = value;
                    AppConfig.SqlConnection = _strCon;
                    //AppConfig.SqlConn = new SqlConnection(new SqlConnectionStringBuilder() { DataSource = TenServer, InitialCatalog = TenCSDL, UserID = accountsql, Password = passql }.ConnectionString);
                    AppConfig.SqlConnectionString = new SqlConnectionStringBuilder() 
                    { 
                        DataSource = TenServer, 
                        InitialCatalog = 
                        TenCSDL, UserID = accountsql, 
                        Password = passql 
                    }.ConnectionString;
                }
            }
        }

        public static string[] FormatString = new string[2] { "{0:0,0}", "{0:0,0}" };
        public static int FormatDate = 1;
        public static int PPXuat = 1;
        public static string _maCC = "";
        //public static int CapDo = 1;
        public static int MaKho = 0;
        public static string LoaiPM = "QLBV";
        public static string MaNSach = "";
        public static string KeToanVPdv = "";
        public static int InPhoi = 0;
        public static int TamThuCLS = 0;
        public static bool keNhieuKho = false;
        /// <summary>
        /// 0: mặc định
        /// 1: chỉ hiện thị đường dùng và chọn HDSD trong danh sách
        /// </summary>
        public static int HDSDDonThuoc = 0;
        ////public static string LoaiPM = "QLDC";
        public static string thongtinketnoi = "server|CLS|sa|cuongthuong@1|KetQua|MaDVct|KetQua|";
        public static string DuongDan = "D:\\";
        public static int GetICD = 0;
        public static string formatAge = "36-45";
        public static string formatAge_24012 = "72-45";
        public static string ngayCNhat = "30/03/2016 10:00";
        public static string _inMauCD = "A5";
        public static bool[] _Visible_CDHA = new bool[4] { true, false, true, true };
        public static Object[,] MangHaiChieu;
        public static List<int> listKPHoatDong = new List<int>();
        /// <summary>
        /// 0: hang đặc biệt
        /// 1: hạng 1
        /// 2: hạng 2
        /// 3: hạng 3
        /// 4: hạng 4
        /// </summary>
        public static string[] mack_theoHangBV = new string[5] { "1895", "1896", "1897", "1898", "1899" };
        /// <summary>
        /// 0: lưu chỉ định
        /// 1: Lưu kết quả
        /// 2: lưu kết quả(backup)
        /// 3:
        /// 4:
        /// 5:
        /// 10: đường dẫn lưu file
        /// </summary>
        public static string[] xmlFilePath_LIS = new string[] { "C:\\VSSOFT", "C:\\VSSOFT", "C:\\VSSOFT", "", "", "", "", "", "", "", "", "" };
        public static string ImageFilePath_CBInfo = "";
        // public static string _test = "...Checking";
        public static string _test = "";
        public static int _idDTBHYT = -1;
        public static DateTime ngayGiaMoi = Convert.ToDateTime("15/08/2016");
        public static DateTime ngayGiaMoiDV = Convert.ToDateTime("01/06/2017");
        public static DateTime ngayGiaMoiTT39 = Convert.ToDateTime("01/01/1990");
        public static bool BC408_24009_ischecked;

        public struct st_PhanLoaiKP
        {
            public static string PhongKham = "Phòng khám";
            public static string LamSang = "Lâm sàng";
            public static string CanLamSang = "Cận lâm sàng";
            public static string ChucNang = "Chức năng";
            public static string KhoaDuoc = "Khoa dược";
            public static string Admin = "Admin";
            public static string KeToan = "Kế toán";
            public static string HanhChinh = "Hành chính";
            public static string TuTruc = "Tủ trực";
            public static string PKKhuVuc = "PK khu vực";
            public static string QuayThuoc = "Quầy thuốc";

            public static string XaPhuong = "Xã phường";
            public static string BHXH = "BHXH";
        }

        public struct st_CapBac
        {
            public static string BacSy = "Bác sỹ";
            public static string YSy = "Y sỹ";
            public static string YTa = "Y tá";
            public static string DieuDuong = "Điều dưỡng";
            public static string DuocSi = "Dược Sỹ";
            public static string Admin = "Admin";
            public static string KeToan = "Kế toán";
        }
        public struct st_TieuNhomRG_ChuyenKhoa
        {
            public static string Mau = "Máu";
            public static string SieuAm = "Siêu âm";
            public static string SieuAmMau = "Siêu âm màu";
            public static string SieuAm_Doppler = "Siêu âm ( Doppler )";
            public static string X_Quang = "X-Quang";
            public static string X_Quang_KTS = "X-Quang KTS";
            public static string DienTim = "Điện tim";
            public static string ThuThuat = "Thủ thuật";
            public static string PhauThuat = "Phẫu thuật";
            public static string NoiSoi = "Nội soi";
            public static string NoiSoiTaiMuiHong = "Nội soi Tai-Mũi-Họng";
            public static string ChucNangHoHap = "Chức năng hô hấp";
            public static string DoMatDoXuong = "Đo mật độ xương";
            public static string LuuHuyetNao = "Lưu huyết não";
            public static string XNHoaSinhMau = "XN hóa sinh máu";
            public static string XNHuyetHoc = "XN huyết học";
            public static string XNHoaSinhMienDich = "XN hóa sinh miễn dịch";
            public static string XNNuocTieu = "XN nước tiểu";
            public static string TracNghiemTamLy = "Trắc nghiệm tâm lý";
            public static string XNKhac = "XN khác";
            public static string XNDichChocDo = "XN dịch chọc dò";
            public static string XNHoaSinhNTTo = "XN hóa sinh nội tiết tố";
            public static string XNTeBaoNuocDich = "XN tế bào nước dịch";
            public static string XNViSinh = "XN vi sinh";
            public static string XNDom = "XN đờm";
            public static string CKNoi = "Nội";
            public static string CKNgoai = "Ngoại";
            public static string CKNhi = "Nhi";
            public static string CKRangHamMat = "Răng Hàm Mặt";
            public static string CKTaiMuiHong = "Tai Mũi Họng";
            public static string CKMat = "Mắt";
            public static string CKTruyenNhiem = "Truyền nhiễm";
            public static string CKDongY = "Đông y";
            public static string CKSan = "Sản";
            public static string CKDaLieu = "Da liễu";
            public static string CKPhucHoiCN = "Phục hồi chức năng";
            public static string CKKhamSucKhoe = "Khám sức khỏe";
            public static string CKTamThan = "Tâm thần";
            public static string CKLao = "Lao";
            public static string CKHSCCCD = "Hồi sức cấp cứu và chống độc";
            public static string CKNoiTiet = "Nội tiết";
            public static string CKGMHSuc = "Gây mê hồi sức";
            public static string CKBong = "Bỏng";
            public static string CKUngBuou = "Ung bướu";
            public static string CKDienNaoDo = "Điện não đồ";
            public static string CKPhuKhoa = "Phụ khoa";
            public static string ThuocThuong_vitamin = "Thuốc thường (Vitamin)";
            public static string ThuocThuong_khangsinh = "Thuốc thường (kháng sinh)";
            public static string ThuocThuong_CPYHCT = "Thuốc thường (Chế phẩm YHCT)";
            public static string ThuocThuong_DT = "Thuốc thường (DT)";
            public static string ThuocThuong_Corticoid = "Thuốc thường (Corticoid)";
            public static string ThuocDongY = "Thuốc đông y";
            public static string X_QuangCT = "X-Quang CT";
            public static string X_QuangKTS = "X-Quang KTS";
            public static string X_Quang_DR = "X-Quang DR";
            public static string X_Quang_CR = "X-Quang CR";
            public static string ThucPhamChucNang = "Thuốc thường (Thực phẩm chức năng)";
            public static string XNMoBenhHoc = "XN mô bệnh học";
            public static string XNDongCamMau = "XN Đông cầm máu";
            public static string TaiSan = "Tài sản";
            public static string ThuocThuong = "Thuốc thường";
            public static string NhaThuoc = "Nhà thuốc";
            public static string XNNongDoCon = "XN Nồng độ cồn";
            public static string XNUngThu = "XN Ung thư";
            public static string NoiSoiDaDay = "Nội soi Dạ Dày";
            public static string NoiSoiCoTuCung = "Nội soi Cổ Tử Cung";
            public static string XNMienDich = "XN miễn dịch";
            public static string XNTeBaoHoc = "XN Tế bào học";
            public static string GiuongNoiTru = "Giường điều trị nội trú";
            public static string GiuongNgoaiTru = "Giường điều trị ngoại trú";
            public static string GiuongLuu = "Giường lưu";
            public static string TeBaoDich = "XN tế bào dịch";
            public static string DoKhucXaMay = "Đo khúc xạ máy";
        }
        /// <summary>
        /// 0. "Khỏi"
        /// 1. "Đỡ|Giảm"
        /// 2. "Không T.đổi"
        /// 3. "Nặng hơn"
        /// 4. "Tử vong"
        /// </summary>

        public static string[] _ketQuaDT = new string[5] { "Khỏi", "Đỡ|Giảm", "Không T.đổi", "Nặng hơn", "Tử vong" };
        public static string[] _phuongAn = new string[5] { "Về nhà(Ra viện)", "Vào viện", "Chuyển viện", "Chuyển khoa|phòng", "Đang khám|điều trị" };
        #region Nhóm dịch vụ CV 9324
        public class NhomDV_QD
        {
            private string tennhomcu;

            public string Tennhomcu
            {
                get { return tennhomcu; }
                set { tennhomcu = value; }
            }
            private int iDNhom;
            public int IDNhom
            {
                get { return iDNhom; }
                set { iDNhom = value; }
            }


            private string tenNhom;

            public string TenNhom
            {
                get { return tenNhom; }
                set { tenNhom = value; }
            }
            private int status;

            public int Status
            {
                get { return status; }
                set { status = value; }
            }
            private int sTT;

            public int STT
            {
                get { return sTT; }
                set { sTT = value; }
            }


            private string tenNhomCT;

            public string TenNhomCT
            {
                get { return tenNhomCT; }
                set { tenNhomCT = value; }
            }


            private int bHYT;

            public int BHYT
            {
                get { return bHYT; }
                set { bHYT = value; }
            }

            static List<NhomDV_QD> _listNhomDV_QD = new List<NhomDV_QD>();
            public static List<NhomDV_QD> SetNhomDV()
            {
                _listNhomDV_QD = new List<NhomDV_QD>();
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 1, TenNhom = "Xét nghiệm", Status = 2, STT = 6, TenNhomCT = "Xét nghiệm", tennhomcu = "Nhóm xét nghiệm" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 2, TenNhom = "Chẩn đoán hình ảnh", Status = 2, STT = 4, TenNhomCT = "Chẩn đoán hình ảnh", tennhomcu = "Nhóm chẩn đoán hình ảnh & TDCN" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 3, TenNhom = "Thăm dò chức năng", Status = 2, STT = 5, TenNhomCT = "Thăm dò chức năng", tennhomcu = "" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 4, TenNhom = "Thuốc trong danh mục BHYT", Status = 1, STT = 10, TenNhomCT = "Thuốc trong danh mục BHYT", tennhomcu = "Nhóm thuốc, dịch truyền" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 5, TenNhom = "Thuốc K, CTG", Status = 1, STT = 15, TenNhomCT = "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục", tennhomcu = "Nhóm thuốc ung thư, chống thải ghép" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 6, TenNhom = "Thuốc thanh toán theo tỷ lệ", Status = 1, STT = 12, TenNhomCT = "Thuốc thanh toán theo tỷ lệ", tennhomcu = "" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 7, TenNhom = "Máu và chế phẩm của máu", Status = 1, STT = 8, TenNhomCT = "Máu và chế phẩm của máu", tennhomcu = "Nhóm máu và chế phẩm của máu" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 8, TenNhom = "Thủ thuật, phẫu thuật", Status = 2, STT = 7, TenNhomCT = "Thủ thuật, phẫu thuật", tennhomcu = "Nhóm phẫu thuật, thủ thuật" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 9, TenNhom = "DVKT thanh toán theo tỷ lệ", Status = 2, STT = 9, TenNhomCT = "DVKT thanh toán theo tỷ lệ", tennhomcu = "Nhóm dịch vụ kỹ thuật cao" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 10, TenNhom = "Vật tư y tế trong danh mục BHYT", Status = 1, STT = 11, TenNhomCT = "Vật tư y tế trong danh mục BHYT", tennhomcu = "Nhóm vật tư y tế tiêu hao" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 11, TenNhom = "VTYT thanh toán theo tỷ lệ", Status = 1, STT = 13, TenNhomCT = "VTYT thanh toán theo tỷ lệ", tennhomcu = "Nhóm vật tư y tế thay thế" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 12, TenNhom = "Vận chuyển", Status = 2, STT = 14, TenNhomCT = "Vận chuyển", tennhomcu = "Chi phí vận chuyển" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 13, TenNhom = "Khám bệnh", Status = 2, STT = 1, TenNhomCT = "Khám bệnh", tennhomcu = "Nhóm tiền công khám" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 14, TenNhom = "Giường điều trị ngoại trú", Status = 2, STT = 2, TenNhomCT = "Giường điều trị ngoại trú", tennhomcu = "" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 15, TenNhom = "Giường điều trị nội trú", Status = 2, STT = 3, TenNhomCT = "Giường điều trị nội trú", tennhomcu = "Nhóm ngày giường" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 16, TenNhom = "Tài sản", Status = 2, STT = 16, TenNhomCT = "Tài sản", tennhomcu = "Nhóm tài sản" });
                _listNhomDV_QD.Add(new NhomDV_QD { IDNhom = 22, TenNhom = "Văn phòng phẩm", Status = 1, STT = 22, TenNhomCT = "Văn phòng phẩm", tennhomcu = "Nhóm văn phòng phẩm" });
                return _listNhomDV_QD;
            }

        }

        #endregion
        #region phân loại xuất
        public class c_PhanLoaiXuat
        {
            string phanloai;

            public string PhanLoai
            {
                get { return phanloai; }
                set { phanloai = value; }
            }
            int id;

            public int Id
            {
                get { return id; }
                set { id = value; }
            }
            bool check;

            public bool Check
            {
                get { return check; }
                set { check = value; }
            }
            static List<c_PhanLoaiXuat> _lPhanLoaiXuat = new List<c_PhanLoaiXuat>();
            public static List<c_PhanLoaiXuat> _setPhanLoaiXuat()
            {
                _lPhanLoaiXuat = new List<c_PhanLoaiXuat>();
                _lPhanLoaiXuat.Add(new c_PhanLoaiXuat { Id = 0, PhanLoai = "Xuất ngoại trú", Check = true });
                _lPhanLoaiXuat.Add(new c_PhanLoaiXuat { Id = 1, PhanLoai = "Xuất nội trú", Check = true });
                _lPhanLoaiXuat.Add(new c_PhanLoaiXuat { Id = 2, PhanLoai = "Xuất nội bộ", Check = true });
                _lPhanLoaiXuat.Add(new c_PhanLoaiXuat { Id = 3, PhanLoai = "Xuất ngoài BV", Check = true });
                _lPhanLoaiXuat.Add(new c_PhanLoaiXuat { Id = 4, PhanLoai = "Xuất điều trị ngoại trú", Check = true });
                _lPhanLoaiXuat.Add(new c_PhanLoaiXuat { Id = 5, PhanLoai = "Xuất Cận Lâm Sàng", Check = true });
                _lPhanLoaiXuat.Add(new c_PhanLoaiXuat { Id = 6, PhanLoai = "Xuất tủ trực", Check = true });
                _lPhanLoaiXuat.Add(new c_PhanLoaiXuat { Id = 7, PhanLoai = "Xuất phòng khám", Check = true });
                _lPhanLoaiXuat.Add(new c_PhanLoaiXuat { Id = 8, PhanLoai = "Xuất kiểm nghiệm", Check = true });
                _lPhanLoaiXuat.Add(new c_PhanLoaiXuat { Id = 9, PhanLoai = "Xuất khác", Check = true });
                _lPhanLoaiXuat.Add(new c_PhanLoaiXuat { Id = 10, PhanLoai = "Xuất sản xuất", Check = true });
                _lPhanLoaiXuat.Add(new c_PhanLoaiXuat { Id = 11, PhanLoai = "Xuất lâm sàng", Check = true });
                return _lPhanLoaiXuat;
            }
        }

        #endregion
        #region Chuyên khoa
        public static List<c_chuyenkhoa> _lChuyenKhoa = new List<c_chuyenkhoa>();
        public class c_chuyenkhoa
        {
            int maCK;

            public int MaCK
            {
                get { return maCK; }
                set { maCK = value; }
            }
            int maCKqd;

            public int MaCK_QD
            {
                get { return maCKqd; }
                set { maCKqd = value; }
            }
            int loaiCK;

            public int LoaiCK
            {
                get { return loaiCK; }
                set { loaiCK = value; }
            }
            string chuyenKhoa;

            public string ChuyenKhoa
            {
                get { return chuyenKhoa; }
                set { chuyenKhoa = value; }
            }
            /// <summary>
            /// 1. Phòng khám ,Lâm sàng
            /// 2. Cận lâm sàng
            /// </summary>

            public static void f_ChuyenKhoa()
            {
                _lChuyenKhoa = new List<c_chuyenkhoa>();
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 0, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKNoi, MaCK_QD = 2, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 1, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKNgoai, MaCK_QD = 10, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 2, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKNhi, MaCK_QD = 3, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 3, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKRangHamMat, MaCK_QD = 16, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 4, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKTaiMuiHong, MaCK_QD = 15, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 5, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKMat, MaCK_QD = 14, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 6, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKTruyenNhiem, MaCK_QD = 2, LoaiCK = 1 });// chưa có
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 7, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKDongY, MaCK_QD = 8, LoaiCK = 1 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 8, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKSan, MaCK_QD = 13, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 9, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKDaLieu, MaCK_QD = 5, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 10, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKKhamSucKhoe, MaCK_QD = 2, LoaiCK = 3 });///

                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 11, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm, MaCK_QD = 2, LoaiCK = 3 });//
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 12, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 13, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 14, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 15, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 16, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 17, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 18, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 19, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 20, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 21, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.LuuHuyetNao, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 22, ChuyenKhoa = "Xét nghiệm", MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 23, ChuyenKhoa = "Trực cấp cứu", MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 24, ChuyenKhoa = "X-Quang CT", MaCK_QD = 2, LoaiCK = 3 });

                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 25, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 26, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 27, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 28, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 29, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 30, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 31, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNTeBaoNuocDich, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 32, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 33, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 34, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_DT, MaCK_QD = 2, LoaiCK = 2 });//"Thuốc thường
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 35, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_khangsinh, MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 36, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_vitamin, MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 37, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_CPYHCT, MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 38, ChuyenKhoa = "Thuốc gây nghiện", MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 39, ChuyenKhoa = "Thuốc hướng tâm thần", MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 40, ChuyenKhoa = "Vật tư y tế tiêu hao", MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 41, ChuyenKhoa = "Hóa chất", MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 42, ChuyenKhoa = "Thuốc trẻ em", MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 43, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocDongY, MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 44, ChuyenKhoa = "Y cụ", MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 45, ChuyenKhoa = "Tâm thần", MaCK_QD = 6, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 46, ChuyenKhoa = "Lao", MaCK_QD = 4, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 47, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKHSCCCD, MaCK_QD = 1, LoaiCK = 1 });//Hồi sức cấp cứu và chống độc", MaCK_QD = 1, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 48, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKNoiTiet, MaCK_QD = 7, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 49, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKGMHSuc, MaCK_QD = 9, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 50, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKBong, MaCK_QD = 11, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 51, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKUngBuou, MaCK_QD = 12, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 52, ChuyenKhoa = "Khám sức khỏe", MaCK_QD = 0, LoaiCK = 1 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 53, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_DT, MaCK_QD = 2, LoaiCK = 2 });//"Thuốc thường (DT)"
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 54, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_Corticoid, MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 55, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.Mau, MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 56, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKDienNaoDo, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 57, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 58, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.TracNghiemTamLy, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 59, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThucPhamChucNang, MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 60, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 61, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDongCamMau, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 62, ChuyenKhoa = "Tài sản", MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 63, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 64, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKPhuKhoa, MaCK_QD = 15, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 65, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NhaThuoc, MaCK_QD = 16, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 66, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKPhucHoiCN, MaCK_QD = 17, LoaiCK = 1 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 67, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNongDoCon, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 68, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNUngThu, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 69, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiDaDay, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 70, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiCoTuCung, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 71, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMienDich, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 72, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNTeBaoHoc, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 73, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAmMau, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 74, ChuyenKhoa = "Văn phòng phẩm", MaCK_QD = 2, LoaiCK = 2 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 75, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.GiuongNoiTru, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 76, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.GiuongNgoaiTru, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 77, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.GiuongLuu, MaCK_QD = 2, LoaiCK = 3 });
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 78, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang_CR, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 79, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang_DR, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 80, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.TeBaoDich, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 81, ChuyenKhoa = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoKhucXaMay, MaCK_QD = 2, LoaiCK = 3 });///
                _lChuyenKhoa.Add(new c_chuyenkhoa { MaCK = 82, ChuyenKhoa = "Vi khuẩn KST", MaCK_QD = 2, LoaiCK = 3 });
            }
        }
        #endregion
        #region danh mục tai nạn
        public static List<c_TaiNan> _listTaiNan = getDMTaiNan();
        public class c_TaiNan
        {
            private int id;

            public int Id
            {
                get { return id; }
                set { id = value; }
            }
            private int? stt;
            /// <summary>
            /// Số thứ tự hiển thị
            /// </summary>
            public int? Stt
            {
                get { return stt; }
                set { stt = value; }
            }
            private int idParent;

            /// <summary>
            /// id danh mục cha
            /// </summary>
            public int IdParent
            {
                get { return idParent; }
                set { idParent = value; }
            }

            private string tenloai;

            /// <summary>
            /// Tên loại tai nạn: VD: tai nạn giao thông
            /// </summary>
            public string Tenloai
            {
                get { return tenloai; }
                set { tenloai = value; }
            }
            private int? t0;

            public int? T0
            {
                get { return t0; }
                set { t0 = value; }
            }
            private int? t5;

            public int? T5
            {
                get { return t5; }
                set { t5 = value; }
            }
            private int? t15;

            public int? T15
            {
                get { return t15; }
                set { t15 = value; }
            }
            private int? t20;

            public int? T20
            {
                get { return t20; }
                set { t20 = value; }
            }
            private int? t60;

            public int? T60
            {
                get { return t60; }
                set { t60 = value; }
            }
            private int? nam;

            public int? Nam
            {
                get { return nam; }
                set { nam = value; }
            }
            private int? nu;

            public int? Nu
            {
                get { return nu; }
                set { nu = value; }
            }
            private int? tongso;

            public int? Tongso
            {
                get { return tongso; }
                set { tongso = value; }
            }
            private int? vaovien;

            public int? Vaovien
            {
                get { return vaovien; }
                set { vaovien = value; }
            }
            private int? chuyenvien;

            public int? Chuyenvien
            {
                get { return chuyenvien; }
                set { chuyenvien = value; }
            }
            private int? tuvong;

            public int? Tuvong
            {
                get { return tuvong; }
                set { tuvong = value; }
            }
            private int ma9324;

            public int Ma9324
            {
                get { return ma9324; }
                set { ma9324 = value; }
            }
            private string ten9324;

            public string Ten9324
            {
                get { return ten9324; }
                set { ten9324 = value; }
            }
            bool status;

            public bool Status
            {
                get { return status; }
                set { status = value; }
            }



        }

        public static List<c_TaiNan> getDMTaiNan()
        {
            List<c_TaiNan> _listTaiNan = new List<c_TaiNan>();
            _listTaiNan.Add(new c_TaiNan { Id = 0, Stt = 0, Tenloai = "Không", Ma9324 = 0, Ten9324 = "Không", Status = true });
            _listTaiNan.Add(new c_TaiNan { Id = 1, Stt = 1, Tenloai = "Tai nạn giao thông", Ma9324 = 1, Ten9324 = "Tai nạn giao thông", Status = false });
            _listTaiNan.Add(new c_TaiNan { Id = 2, IdParent = 1, Tenloai = "Đường bộ", Ma9324 = 1, Ten9324 = "Tai nạn giao thông", Status = true });
            _listTaiNan.Add(new c_TaiNan { Id = 3, IdParent = 1, Tenloai = "Đường sắt", Ma9324 = 1, Ten9324 = "Tai nạn giao thông", Status = true });
            _listTaiNan.Add(new c_TaiNan { Id = 4, IdParent = 1, Tenloai = "Đường sông", Ma9324 = 1, Ten9324 = "Tai nạn giao thông", Status = true });
            _listTaiNan.Add(new c_TaiNan { Id = 5, Stt = 2, Tenloai = "Tai nạn lao động", Ma9324 = 2, Ten9324 = "Tai nạn lao động", Status = true });
            _listTaiNan.Add(new c_TaiNan { Id = 6, Stt = 3, Tenloai = "Tai nạn sinh hoạt", Ma9324 = 8, Ten9324 = "Khác", Status = true });
            _listTaiNan.Add(new c_TaiNan { Id = 7, Stt = 4, Tenloai = "Đánh nhau", Ma9324 = 5, Ten9324 = "Bạo lực, xung đột", Status = true });
            _listTaiNan.Add(new c_TaiNan { Id = 8, Stt = 5, Tenloai = "Tự tử", Ma9324 = 6, Ten9324 = "Tự tử", Status = true });
            _listTaiNan.Add(new c_TaiNan { Id = 9, Stt = 6, Tenloai = "Ngộ độc", Ma9324 = 7, Ten9324 = "Ngộ độc các loại", Status = true });
            _listTaiNan.Add(new c_TaiNan { Id = 10, Stt = 7, Tenloai = "Đuối nước", Ma9324 = 3, Ten9324 = "Tai nạn dưới nước", Status = true });
            _listTaiNan.Add(new c_TaiNan { Id = 11, Stt = 8, Tenloai = "Bạo lực gia đình", Ma9324 = 5, Ten9324 = "Bạo lực, xung đột", Status = true });
            _listTaiNan.Add(new c_TaiNan { Id = 12, Stt = 9, Tenloai = "Khác", Ma9324 = 8, Ten9324 = "Khác", Status = true });
            return _listTaiNan;
        }
        #endregion
        #region Cỡ phim
        public static List<CoPhimXQuang> _lCoPhim = CoPhimXQuang.setCoPhimXQuang();
        public class CoPhimXQuang
        {
            int idCoPhim;

            public int IdCoPhim
            {
                get { return idCoPhim; }
                set { idCoPhim = value; }
            }
            string coPhim;

            public string CoPhim
            {
                get { return coPhim; }
                set { coPhim = value; }
            }
            public static List<CoPhimXQuang> setCoPhimXQuang()
            {
                List<CoPhimXQuang> _lCoPhim = new List<CoPhimXQuang>();
                if (DungChung.Bien.MaBV == "30009")
                    _lCoPhim.Add(new CoPhimXQuang { idCoPhim = 0, CoPhim = "8/10" });
                else if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "24297")
                {
                    _lCoPhim.Add(new CoPhimXQuang { idCoPhim = 0, CoPhim = "20/25" });
                    _lCoPhim.Add(new CoPhimXQuang { idCoPhim = 1, CoPhim = "25/30" });
                }
                else
                {
                    _lCoPhim.Add(new CoPhimXQuang { idCoPhim = 0, CoPhim = "Không XĐ" });
                    _lCoPhim.Add(new CoPhimXQuang { idCoPhim = 1, CoPhim = "13/18" });
                    _lCoPhim.Add(new CoPhimXQuang { idCoPhim = 2, CoPhim = "18/24" });
                    _lCoPhim.Add(new CoPhimXQuang { idCoPhim = 3, CoPhim = "24/30" });
                    _lCoPhim.Add(new CoPhimXQuang { idCoPhim = 4, CoPhim = "30/40" });

                }
                return _lCoPhim;
            }
        }
        #endregion

        public class KieuDonBN
        {
            public int value { set; get; }
            public string KieuDon { set; get; }
        }
        public static List<KieuDonBN> _lKieuDonBN()
        {
            List<KieuDonBN> list = new List<KieuDonBN>();
            list.Add(new KieuDonBN { value = 0, KieuDon = "Hàng ngày" });
            list.Add(new KieuDonBN { value = 1, KieuDon = "Bổ sung" });
            list.Add(new KieuDonBN { value = 2, KieuDon = "Trả thuốc(BN)" });
            list.Add(new KieuDonBN { value = 3, KieuDon = "Lĩnh về khoa" });
            list.Add(new KieuDonBN { value = 4, KieuDon = "Trả thuốc(Khoa)" });
            list.Add(new KieuDonBN { value = 5, KieuDon = "Trực" });
            return list;
        }

        public static bool Checkin = false;
        public static bool CheckinVTYT = false;
        public class ChanDoan
        {
            public string BenhChinh { get; set; }
            public string MaICD { get; set; }

            public string BenhPhu2 { get; set; }
            public string MaICD2 { get; set; }
            public string BenhPhu3 { get; set; }
            public string MaICD3 { get; set; }
            public string BenhPhu4 { get; set; }
            public string MaICD4 { get; set; }
        }


        public class InvoiceInfo
        {
            public string invoiceType { get; set; }
            public string templateCode { get; set; }
            public string invoiceSeries { get; set; }
            public string transactionUuid { get; set; }
            public long invoiceIssuedDate { get; set; }
            public string currencyCode { get; set; }
            public string adjustmentType { get; set; }
            public bool paymentStatus { get; set; }
            public string paymentType { get; set; }
            public string paymentTypeName { get; set; }
            public bool cusGetInvoiceRight { get; set; }
            public string buyerIdNo { get; set; }
            public string buyerIdType { get; set; }
            //public string invoiceNote { get; set; }
            //public string adjustmentInvoiceType { get; set; }
            //public string originalInvoiceId { get; set; }
            //public string originalInvoiceIssueDate { get; set; }
            //public string additionalReferenceDesc { get; set; }
            //public string additionalReferenceDate { get; set; }
        }

        public class BuyerInfo
        {
            public string buyerName { get; set; }
            public string buyerLegalName { get; set; }
            public string buyerTaxCode { get; set; }
            public string buyerBankAccount { get; set; }
            public string buyerBankName { get; set; }
            public string buyerAddressLine { get; set; }
            public string buyerPhoneNumber { get; set; }
            public string buyerEmail { get; set; }
            public string buyerIdNo { get; set; }
            public string buyerIdType { get; set; }
        }

        public class SellerInfo
        {
            public string sellerLegalName { get; set; }
            public string sellerTaxCode { get; set; }
            public string sellerFaxNumber { get; set; }
            public string sellerWebsite { get; set; }
            public string sellerAddressLine { get; set; }
            public string sellerPhoneNumber { get; set; }
            public string sellerEmail { get; set; }
            public string sellerBankName { get; set; }
            public string sellerBankAccount { get; set; }
        }

        public class ItemInfo
        {
            public int lineNumber { get; set; }
            public string itemCode { get; set; }
            public string itemName { get; set; }
            public string unitName { get; set; }
            public decimal unitPrice { get; set; }
            public decimal quantity { get; set; }
            public decimal itemTotalAmountWithoutTax { get; set; }
            public decimal taxPercentage { get; set; }
            public decimal taxAmount { get; set; }
            public decimal discount { get; set; }
            public decimal itemDiscount { get; set; }
            //public decimal adjustmentTaxAmount { get; set; }
            //public bool isIncreaseItem { get; set; }
        }

        public class ZipFileResponse
        {
            public string errorCode { get; set; }
            public string description { get; set; }
            public string fileName { get; set; }
            public byte[] fileToBytes { get; set; }
            public bool paymentStatus { get; set; }
        }

        public class GetFileRequest
        {
            public string invoiceNo { get; set; }
            public string fileType { get; set; }
            public string strIssueDate { get; set; }
            public string additionalReferenceDesc { get; set; }
            public string additionalReferenceDate { get; set; }
            public string pattern { get; set; }
            public string transactionUuid { get; set; }
            public string transactionID { get; set; }
        }

        public class PDFFileResponse
        {
            public string errorCode { get; set; }
            public string description { get; set; }
            public string fileName { get; set; }
            public byte[] fileToBytes { get; set; }
        }

        public class SummarizeInfo
        {
            public decimal sumOfTotalLineAmountWithoutTax { get; set; }
            public decimal totalAmountWithoutTax { get; set; }
            public decimal totalTaxAmount { get; set; }
            public decimal totalAmountWithTax { get; set; }
            public string totalAmountWithTaxInWords { get; set; }
            public decimal discountAmount { get; set; }
            public decimal settlementDiscountAmount { get; set; }
            public decimal taxPercentage { get; set; }
        }

        public class TaxBreakdowns
        {
            public decimal taxPercentage { get; set; }
            public decimal taxableAmount { get; set; }
            public decimal taxAmount { get; set; }
        }

        public class ExtAttribute
        {

        }

        public class Payment
        {
            public string paymentMethodName { get; set; }
        }

        public class DeliveryInfo
        {

        }
        public class DiscountItemInfo
        {

        }
        public class MeterReading
        {
            public string previousIndex { get; set; }
            public string currentIndex { get; set; }
            public string factor { get; set; }
            public string amount { get; set; }
        }

        public class Invoice
        {
            public InvoiceInfo generalInvoiceInfo { get; set; }
            public BuyerInfo buyerInfo { get; set; }
            public SellerInfo sellerInfo { get; set; }
            public List<ExtAttribute> extAttribute { get; set; }
            public List<Payment> payments { get; set; }
            public DeliveryInfo deliveryInfo { get; set; }
            public List<ItemInfo> itemInfo { get; set; }
            public List<DiscountItemInfo> discountItemInfo { get; set; }
            public List<MeterReading> meterReading { get; set; }
            public SummarizeInfo summarizeInfo { get; set; }
            public List<TaxBreakdowns> taxBreakdowns { get; set; }
        }

        public class SetFileRequest
        {
            public string supplierTaxCode { get; set; }
            public string invoiceNo { get; set; }
            public string templateCode { get; set; }
            public string transactionUuid { get; set; }
            public string transactionID { get; set; }
            public string fileType { get; set; }
            public string strIssueDate { get; set; }
            public string exchangeUser { get; set; }
        }
        public class SetFileRequest_v2
        {
            public string supplierTaxCode { get; set; }
            public string invoiceNo { get; set; }
            public string strIssueDate { get; set; }
            public string exchangeUser { get; set; }
        }
        public class DuLieuYC
        {
            public string client_id { get; set; }
            public string grant_type { get; set; }
            public string username { get; set; }
            public string password { get; set; }
        }
        public class ThongDiepTL
        {
            public string username { get; set; }
            public string HoTen { get; set; }
            public string id { get; set; }
            public string Domain { get; set; }
            public string IdDonVi { get; set; }
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public string token_type { get; set; }
        }

    }
}
