using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace QLBV.BaoCao
{
    public partial class Frm_ThongTinHoatDong_24012 : Form
    {
        public Frm_ThongTinHoatDong_24012()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_ThongTinHoatDong_24012_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now.Date;
            lupNgayden.DateTime = System.DateTime.Now.Date;
        }
        public class BCHD
        {

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            List<BCHD> _list = new List<BCHD>();
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            TimeSpan time = ngayden - ngaytu;
            if (time.TotalDays <= 366)
            {
                #region Lấy dữ liệu
                #region STT1
                string stt1 = "";
                var STT1 = (from kp in _Data.KPhongs.Where(p => p.SoGiuongKH != null)
                            select new
                            {
                                kp.SoGiuongKH
                            }).ToList();
                string chuoi = "";
                string namkehoach1 = "";
                string namkehoach2 = "";
                string nam1 = "";
                string nam2 = "";
                string nam6 = "";
                int nam4 = 0;
                int nam5 = 0;
                int tong21 = 0;
                int tong22 = 0;

                foreach (var item in STT1)
                {
                    chuoi += item.SoGiuongKH;
                }
                var a = chuoi.Split('|');
                nam4 = ngaytu.Year;
                nam5 = ngayden.Year;
                nam6 = nam4 + ";" + nam5;
                namkehoach1 = nam6.Split(';')[0];
                namkehoach2 = nam6.Split(';')[1];
                if (namkehoach1 == namkehoach2)
                {
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a[i].Contains(namkehoach1) && Convert.ToInt32(namkehoach1) >= ngaytu.Year)
                        {
                            tong21 += Convert.ToInt32(a[i].Split(':')[1]);
                            if (tong21 != 0)
                            {
                                nam1 = namkehoach1 + ": " + tong21;
                            }
                        }
                    }
                    stt1 = nam1;
                }
                else
                {
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a[i].Contains(namkehoach1) && Convert.ToInt32(namkehoach1) >= ngaytu.Year)
                        {
                            tong21 += Convert.ToInt32(a[i].Split(':')[1]);
                            if (tong21 != 0)
                            {
                                nam1 = namkehoach1 + ": " + tong21;
                            }
                        }
                        if (a[i].Contains(namkehoach2) && Convert.ToInt32(namkehoach2) >= ngaytu.Year)
                        {
                            tong22 += Convert.ToInt32(a[i].Split(':')[1]);
                            if (tong22 != 0)
                            {
                                nam2 = namkehoach2 + ": " + tong22;
                            }
                        }
                    }
                    stt1 = nam1 + ", " + nam2;
                }
                #endregion
                #region STT2
                var STT2 = (from dt in _Data.DThuoccts.Where(p => p.NgayNhap >= ngaytu && p.NgayNhap <= ngayden)
                            join dv in _Data.DichVus on dt.MaDV equals dv.MaDV
                            select new
                            {
                                dv.IDNhom
                            }).Where(p => p.IDNhom == 14 || p.IDNhom == 15).Count();
                #endregion
                #region STT3

                #endregion
                #region STT4

                #endregion
                #region STT5

                #endregion
                #region STT6

                #endregion
                #region STT7
                var STT7 = (from bn in _Data.BenhNhans.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden)
                            join bv in _Data.BenhViens on bn.MaCS equals bv.MaBV
                            where bn.NoiTinh == 1 || bv.MaChuQuan == bn.MaKCB
                            select new
                            {
                               bn.SThe
                            }).Count();
                #endregion
                #region STT8

                #endregion
                #region STT9

                #endregion
                #region STT10
                var STT10 = (from tamung in _Data.TamUngs
                             join rv in _Data.RaViens on tamung.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 tamung.PhanLoai
                             }).Distinct().Where(p => p.PhanLoai == 1 || p.PhanLoai == 3).Count();
                #endregion
                #region STT11

                #endregion
                #region STT12

                #endregion
                #region STT13

                #endregion
                #region STT14

                #endregion
                #region STT15
                var STT15 = (from bn in _Data.BenhNhans
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             && (bn.DTuong == "KSK")
                             select new
                             {
                             }).Count();
                #endregion
                #region STT16
                var STT16 = (from bn in _Data.BenhNhans
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.Tuoi
                             }).Where(p => p.Tuoi <= 6).Count();
                #endregion
                #region STT17
                var STT17 = (from bn in _Data.BenhNhans
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.Tuoi,
                                 bn.DTuong
                             }).Where(p => p.Tuoi <= 6 && p.DTuong == "BHYT").Count();
                #endregion
                #region STT18
                var STT18 = (from bn in _Data.BenhNhans.Where(p => p.Tuoi <= 6)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             join tamung in _Data.TamUngs on bn.MaBNhan equals tamung.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 tamung.PhanLoai
                             }).Where(p => p.PhanLoai == 1 || p.PhanLoai == 3).Count();
                #endregion
                #region STT19
                var STT19 = (from bn in _Data.BenhNhans.Where(p => p.Tuoi >= 60)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                             }).Count();
                #endregion
                #region STT20
                var STT20 = (from bn in _Data.BenhNhans.Where(p => p.Tuoi >= 60)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.DTuong
                             }).Where(p => p.DTuong == "BHYT").Count();
                #endregion
                #region STT21
                var STT21 = (from bn in _Data.BenhNhans.Where(p => p.Tuoi >= 60)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             join tamung in _Data.TamUngs on bn.MaBNhan equals tamung.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 tamung.PhanLoai
                             }).Where(p => p.PhanLoai == 1 || p.PhanLoai == 3).Count();
                #endregion
                #region STT22

                #endregion
                #region STT23

                #endregion
                #region STT24

                #endregion
                #region STT25

                #endregion
                #region STT26

                #endregion
                #region STT27
                var STT27 = (from bn in _Data.BenhNhans.Where(p => p.DTNT == true)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             { }).Count();
                #endregion
                #region STT28
                var STT28 = (from bn in _Data.BenhNhans.Where(p => p.DTNT == true)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 rv.SoNgaydt
                             }).Sum(p => p.SoNgaydt);
                #endregion
                #region STT29

                #endregion
                #region STT30
                var STT30 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             join tamung in _Data.TamUngs on bn.MaBNhan equals tamung.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 tamung.PhanLoai
                             }).Where(p => p.PhanLoai == 1 || p.PhanLoai == 3).Count();
                #endregion
                #region STT31
                var STT31 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1 && p.DTuong == "BHYT")
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             { }).Count();
                #endregion
                #region STT32
                var STT32 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1 && (p.MaDTuong == "HN" || p.DTuong == "Dịch vụ"))
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             { }).Count();
                #endregion
                #region STT33

                #endregion
                #region STT34

                #endregion
                #region STT35
                var STT35 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1 && p.MaKP == 9)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             { }).Count();
                #endregion
                #region STT36
                var STT36 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1 && p.Tuoi <= 6)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             { }).Count();
                #endregion
                #region STT37
                var STT37 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1 && p.Tuoi <= 6 && p.DTuong == "BHYT")
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             { }).Count();
                #endregion
                #region STT38
                var STT38 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1 && p.Tuoi <= 6)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             join tamung in _Data.TamUngs on bn.MaBNhan equals tamung.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 tamung.PhanLoai
                             }).Where(p => p.PhanLoai == 1 || p.PhanLoai == 3).Count();
                #endregion
                #region STT39
                var STT39 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1 && p.Tuoi >= 60)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             { }).Count();
                #endregion
                #region STT40
                var STT40 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1 && p.Tuoi >= 60 && p.DTuong == "BHYT")
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                             }).Count();
                #endregion
                #region STT41
                var STT41 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1 && p.Tuoi == 60)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             join tamung in _Data.TamUngs on bn.MaBNhan equals tamung.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 tamung.PhanLoai
                             }).Where(p => p.PhanLoai == 1 || p.PhanLoai == 3).Count();
                #endregion
                #region STT42

                #endregion
                #region STT43

                #endregion
                #region STT44
                var STT44 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 rv.KetQua
                             }).Where(p => p.KetQua.Contains("khỏi")).Count();
                #endregion
                #region STT45
                var STT45 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 rv.KetQua
                             }).Where(p => p.KetQua.Contains("Đỡ") || p.KetQua.Contains("Giảm")).Count();
                #endregion
                #region STT46
                var STT46 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 rv.KetQua
                             }).Where(p => p.KetQua.Contains("không")).Count();
                #endregion
                #region STT47
                var STT47 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 rv.KetQua
                             }).Where(p => p.KetQua.Contains("Nặng hơn")).Count();
                #endregion
                #region STT48
                var STT48 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 rv.KetQua
                             }).Where(p => p.KetQua.Contains("Tử vong")).Count();
                #endregion
                #region STT49
                var STT49 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden) && rv.KetQua.Contains("khỏi")
                             select new
                             {
                             }).Count();
                #endregion
                #region STT50
                var STT50 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden) && (rv.KetQua.Contains("Đỡ") || rv.KetQua.Contains("Giảm"))
                             select new
                             {
                             }).Count();
                #endregion
                #region STT51

                #endregion
                #region STT52

                #endregion
                #region STT53

                #endregion
                #region STT54
                var STT54 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 rv.SoNgaydt
                             }).Sum(p => p.SoNgaydt);
                #endregion
                #region STT55
                var STT55 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                             }).Count();
                #endregion
                #region STT56

                #endregion
                #region STT57
                var STT57 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                                && rv.KetQua.ToUpper().Contains("TỬ VONG")
                                && rv.NgayRa != null && bn.NNhap != null
                             select new
                             {
                                 rv.KetQua,
                                 bn.NNhap,
                                 rv.NgayRa
                             }).ToList();

                var stt57 = STT57.Where(w => w.NgayRa.Value.Subtract(w.NNhap.Value).TotalHours < 24).Count();
                #endregion
                #region STT58
                var STT58 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                                && rv.KetQua.ToUpper().Contains("TỬ VONG")
                                && rv.NgayRa != null && bn.NNhap != null
                             select new
                             {
                                 rv.KetQua,
                                 bn.NNhap,
                                 rv.NgayRa
                             }).ToList();

                var stt58 = STT58.Where(w => w.NgayRa.Value.Subtract(w.NNhap.Value).TotalHours > 24).Count();
                #endregion
                #region STT59

                #endregion
                #region STT60

                #endregion
                #region STT61

                #endregion
                #region STT62

                #endregion
                #region STT63

                #endregion
                #region STT64

                #endregion
                #region STT65

                #endregion
                #region STT66

                #endregion
                #region STT67

                #endregion
                #region STT68
                var STT68 = (from cls in _Data.CLS
                             join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.DTuong,
                                 dv.MaNhom5937,
                                 cls.Status,
                             }).Where(p => p.MaNhom5937 == 18 && p.Status == 1 &&(p.DTuong == "BHYT" || p.DTuong == "Dịch vụ")).Count();
                #endregion
                #region STT69
                var STT69 = (from cls in _Data.CLS
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden) && cls.Status == 1 && cd.MaDV == 447
                             select new
                             {
                             }).Count();
                #endregion
                #region STT70
                var STT70 = (from cls in _Data.CLS
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden) && cls.Status == 1 && (cd.MaDV == 380 || cd.MaDV == 38)
                             select new
                             {
                             }).Count();
                #endregion
                #region STT71
                var STT71 = (from cls in _Data.CLS
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden) && cls.Status == 1 && ((cd.MaDV == 890 || cd.MaDV == 1095 || cd.MaDV == 592 || cd.MaDV == 593 || cd.MaDV == 594 || cd.MaDV == 396 || cd.MaDV == 397 || cd.MaDV == 398 || cd.MaDV == 444 || cd.MaDV == 486)
                             || (dv.TenDV.Contains("Điện châm %") || dv.TenDV.Contains("Thủy châm %")))
                             select new
                             {
                             }).Count();
                #endregion
                #region STT72

                #endregion
                #region STT73

                #endregion
                #region STT74

                #endregion
                #region STT75

                #endregion
                #region STT76

                #endregion
                #region STT77

                #endregion
                #region STT78

                #endregion
                #region STT79

                #endregion
                #region STT80

                #endregion
                #region STT81
                #endregion
                #region STT82
                var STT82 = (from cls in _Data.CLS
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.NoiTru,
                                 bn.DTuong,
                                 dv.IdTieuNhom,
                                 cls.Status
                             }).Where(p => p.NoiTru == 1 && (p.DTuong != "KSK") && p.IdTieuNhom == 1 && p.Status == 1).Count();
                #endregion
                #region STT83
                var STT83 = (from cls in _Data.CLS
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.NoiTru,
                                 bn.DTuong,
                                 dv.IdTieuNhom,
                                 cls.Status
                             }).Where(p => p.NoiTru == 0 && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ") && p.IdTieuNhom == 1 && p.Status == 1).Count();
                #endregion
                #region STT84
                var STT84 = (from cls in _Data.CLS
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.DTuong,
                                 dv.IdTieuNhom,
                                 cls.Status
                             }).Where(p => p.DTuong == "KSK" && p.IdTieuNhom == 1 && p.Status == 1).Count();
                #endregion
                #region STT85

                #endregion
                #region STT86
                var STT86 = (from cls in _Data.CLS
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.NoiTru,
                                 bn.DTuong,
                                 dv.IdTieuNhom,
                                 cls.Status
                             }).Where(p => p.NoiTru == 1 && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ") && p.IdTieuNhom == 2 && p.Status == 1).Count();
                #endregion
                #region STT87
                var STT87 = (from cls in _Data.CLS
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.NoiTru,
                                 bn.DTuong,
                                 dv.IdTieuNhom,
                                 cls.Status
                             }).Where(p => p.NoiTru == 0 && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ") && p.IdTieuNhom == 2 && p.Status == 1).Count();
                #endregion
                #region STT88
                var STT88 = (from cls in _Data.CLS
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.NoiTru,
                                 bn.DTuong,
                                 dv.IdTieuNhom,
                                 cls.Status
                             }).Where(p => p.DTuong == "KSK" && p.IdTieuNhom == 2 && p.Status == 1).Count();
                #endregion
                #region STT89

                #endregion
                #region STT90
                var STT90 = (from cls in _Data.CLS
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.NoiTru,
                                 bn.DTuong,
                                 dv.IdTieuNhom,
                                 cls.Status
                             }).Where(p => p.NoiTru == 1 && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ") && p.IdTieuNhom == 110 && p.Status == 1).Count();
                #endregion
                #region STT91
                var STT91 = (from cls in _Data.CLS
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.NoiTru,
                                 bn.DTuong,
                                 dv.IdTieuNhom,
                                 cls.Status
                             }).Where(p => p.NoiTru == 0 && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ") && p.IdTieuNhom == 110 && p.Status == 1).Count();
                #endregion
                #region STT92
                var STT92 = (from cls in _Data.CLS
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.NoiTru,
                                 bn.DTuong,
                                 dv.IdTieuNhom,
                                 cls.Status
                             }).Where(p => p.DTuong == "KSK" && p.IdTieuNhom == 110 && p.Status == 1).Count();
                #endregion
                #region STT93

                #endregion
                #region STT94

                #endregion
                #region STT95

                #endregion
                #region STT96

                #endregion
                #region STT97

                #endregion
                #region STT98
                var STT98 = (from cls in _Data.CLS
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.NoiTru,
                                 bn.DTuong,
                                 dv.IdTieuNhom,
                                 cls.Status
                             }).Where(p => p.NoiTru == 1 /*&& (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ")*/ && p.DTuong != "KSK" && p.IdTieuNhom == 5 && p.Status == 1).Count();
                #endregion
                #region STT99
                var STT99 = (from cls in _Data.CLS
                             join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                             join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                             where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                             select new
                             {
                                 bn.NoiTru,
                                 bn.DTuong,
                                 dv.IdTieuNhom,
                                 cls.Status
                             }).Where(p => p.NoiTru == 0 && (p.DTuong != "KSK" /*|| p.DTuong == "Dịch vụ"*/) && p.IdTieuNhom == 5 && p.Status == 1).Count();
                #endregion
                #region STT100
                var STT100 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  bn.NoiTru,
                                  bn.DTuong,
                                  dv.IdTieuNhom,
                                  cls.Status
                              }).Where(p => p.DTuong == "KSK" && p.IdTieuNhom == 5 && p.Status == 1).Count();
                #endregion
                #region STT101

                #endregion
                #region STT102

                #endregion
                #region STT103

                #endregion
                #region STT104

                #endregion
                #region STT105

                #endregion
                #region STT106

                #endregion
                #region STT107

                #endregion
                #region STT108

                #endregion
                #region STT109

                #endregion
                #region STT110

                #endregion
                #region STT111

                #endregion
                #region STT112

                #endregion
                #region STT113

                #endregion
                #region STT114
                var STT114 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  bn.NoiTru,
                                  bn.DTuong,
                                  dv.IdTieuNhom,
                                  cls.Status
                              }).Where(p => p.NoiTru == 1 && (p.DTuong != "KSK" /*|| p.DTuong == "Dịch vụ"*/) && (p.IdTieuNhom == 4 || p.IdTieuNhom == 99) && p.Status == 1).Count();
                #endregion
                #region STT115
                var STT115 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  bn.NoiTru,
                                  bn.DTuong,
                                  dv.IdTieuNhom,
                                  cls.Status
                              }).Where(p => p.NoiTru == 0 && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ") && (p.IdTieuNhom == 4 || p.IdTieuNhom == 99) && p.Status == 1).Count();
                #endregion
                #region STT116
                var STT116 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  bn.NoiTru,
                                  bn.DTuong,
                                  dv.IdTieuNhom,
                                  cls.Status
                              }).Where(p => p.DTuong == "KSK" && (p.IdTieuNhom == 4 || p.IdTieuNhom == 99) && p.Status == 1).Count();
                #endregion
                #region STT117

                #endregion
                #region STT118
                var STT118 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  bn.NoiTru,
                                  dv.IdTieuNhom,
                                  cls.Status
                              }).Where(p => p.NoiTru == 1 && (p.IdTieuNhom == 43 || p.IdTieuNhom == 98) && p.Status == 1).Count();
                #endregion
                #region STT119
                var STT119 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  bn.NoiTru,
                                  dv.IdTieuNhom,
                                  cls.Status
                              }).Where(p => p.NoiTru == 0 && (p.IdTieuNhom == 43 || p.IdTieuNhom == 98) && p.Status == 1).Count();
                #endregion
                #region STT120

                #endregion
                #region STT121

                #endregion
                #region STT122

                #endregion
                #region STT123

                #endregion
                #region STT124

                #endregion
                #region STT125

                #endregion
                #region STT126

                #endregion
                #region STT127

                #endregion
                #region STT128

                #endregion
                #region STT129

                #endregion
                #region STT130

                #endregion
                #region STT131

                #endregion
                #region STT132

                #endregion
                #region STT133

                #endregion
                #region STT134

                #endregion
                #region STT135

                #endregion
                #region STT136

                #endregion
                #region STT137

                #endregion
                #region STT138

                #endregion
                #region STT139

                #endregion
                #region STT140

                #endregion
                #region STT141

                #endregion
                #region STT142

                #endregion
                #region STT143

                #endregion
                #region STT144

                #endregion
                #region STT145

                #endregion
                #region STT146

                #endregion
                #region STT147

                #endregion
                #region STT148

                #endregion
                #region STT149

                #endregion
                #region STT150
                var STT150 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  cls.Status,
                                  dv.TenDV,
                              }).Where(p => p.Status == 1 && p.TenDV.Contains("Thủy châm")).Count();
                #endregion
                #region STT151
                var STT151 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  cls.Status,
                                  dv.TenDV,
                              }).Where(p => p.Status == 1 && p.TenDV.Contains("Điện châm")).Count();
                #endregion
                #region STT152
                var STT152 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  cls.Status,
                                  dv.TenDV,
                              }).Where(p => p.Status == 1 && p.TenDV.Contains("Hào châm")).Count();
                #endregion
                #region STT153
                var STT153 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  cls.Status,
                                  dv.TenDV,
                              }).Where(p => p.Status == 1 && p.TenDV.Contains("Nhĩ châm")).Count();
                #endregion
                #region STT154
                var STT154 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  cls.Status,
                                  dv.TenDV,
                              }).Where(p => p.Status == 1 && p.TenDV.Contains("Cứu")).Count();
                #endregion
                #region STT155
                var STT155 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  cls.Status,
                                  dv.TenDV,
                              }).Where(p => p.Status == 1 && p.TenDV.Contains("Giác")).Count();
                #endregion
                #region STT156
                var STT156 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  cls.Status,
                                  dv.TenDV,
                              }).Where(p => p.Status == 1 && p.TenDV.Contains("Xoa bóp")).Count();
                #endregion
                #region STT157
                var STT157 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  cls.Status,
                                  dv.TenDV,
                              }).Where(p => p.Status == 1 && p.TenDV.Contains("Xông hơi thuốc")).Count();
                #endregion
                #region STT158
                var STT158 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  cls.Status,
                                  dv.TenDV,
                              }).Where(p => p.Status == 1 && p.TenDV.Contains("Ngâm thuốc")).Count();
                #endregion
                #region STT159
                var STT159 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  cls.Status,
                                  dv.TenDV,
                              }).Where(p => p.Status == 1 && p.TenDV.Contains("Đắp thuốc tại chỗ")).Count();
                #endregion
                #region STT160
                var STT160 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  cls.Status,
                                  dv.MaDV,
                              }).Where(p => p.Status == 1 && p.MaDV == 496 || p.MaDV == 494 || p.MaDV == 567 || p.MaDV == 573 || p.MaDV == 565 || p.MaDV == 562
                              || p.MaDV == 570 || p.MaDV == 579 || p.MaDV == 580 || p.MaDV == 581 || p.MaDV == 582 || p.MaDV == 583 || p.MaDV == 575 || p.MaDV == 588
                              || p.MaDV == 585 || p.MaDV == 586 || p.MaDV == 590 || p.MaDV == 584 || p.MaDV == 553 || p.MaDV == 569 || p.MaDV == 492 || p.MaDV == 499
                              || p.MaDV == 499 || p.MaDV == 498 || p.MaDV == 566 || p.MaDV == 1226 || p.MaDV == 586 || p.MaDV == 497 || p.MaDV == 491).Count();
                #endregion
                #region STT161
                var STT161 = (from cls in _Data.CLS
                              join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                              select new
                              {
                                  cls.Status,
                                  dv.MaDV,
                              }).Where(p => p.Status == 1 && p.MaDV == 444 || p.MaDV == 451 || p.MaDV == 471 || p.MaDV == 455 || p.MaDV == 440 || p.MaDV == 439
                              || p.MaDV == 452 || p.MaDV == 453 || p.MaDV == 1070 || p.MaDV == 448 || p.MaDV == 714 || p.MaDV == 447 || p.MaDV == 446 || p.MaDV == 441).Count();
                #endregion
                #region STT162

                #endregion
                #region STT163

                #endregion
                #region STT164

                #endregion
                #region STT165

                #endregion
                #region STT166

                #endregion
                #region STT167

                #endregion
                #region STT168

                #endregion
                #region STT169

                #endregion
                #region STT170

                #endregion
                #region STT171

                #endregion
                #region STT172

                #endregion
                #region STT173

                #endregion
                #region STT174

                #endregion
                #region STT175

                #endregion
                #region STT176

                #endregion
                #region STT177

                #endregion
                #region STT178

                #endregion
                #region STT179

                #endregion
                #region STT180

                #endregion
                #region STT181

                #endregion
                #region STT182

                #endregion
                #region STT183

                #endregion
                #region STT184

                #endregion
                #region STT185

                #endregion
                #region STT186

                #endregion
                #region STT187

                #endregion
                #region STT188

                #endregion
                #region STT189

                #endregion
                #region STT190

                #endregion
                #region STT191

                #endregion
                #region STT192

                #endregion
                #region STT193

                #endregion
                #region STT194

                #endregion
                #region STT195

                #endregion
                #region STT196

                #endregion
                #region STT197

                #endregion
                #region STT198

                #endregion
                #region STT199

                #endregion
                #region STT200

                #endregion
                #region STT201

                #endregion
                #region STT202

                #endregion
                #endregion
                BaoCao.Rep_BC_HD_24012 rep = new BaoCao.Rep_BC_HD_24012();
                frmIn frm = new frmIn();
                #region Đổ dữ liệu
                rep.Parameters["NgayThang"].Value = "Từ ngày " + ngaytu.ToString("dd/MM/yyyy") + " Đến ngày " + ngayden.ToString("dd/MM/yyyy");
                rep.Parameters["STT1"].Value = stt1;
                rep.Parameters["STT2"].Value = STT2;
                rep.Parameters["STT3"].Value = "0";
                rep.Parameters["STT4"].Value = "0";
                rep.Parameters["STT5"].Value = "0";
                rep.Parameters["STT6"].Value = "0";
                rep.Parameters["STT7"].Value = STT7;
                rep.Parameters["STT8"].Value = "0";
                rep.Parameters["STT9"].Value = STT10;
                rep.Parameters["STT10"].Value = STT10;
                rep.Parameters["STT11"].Value = "0";
                rep.Parameters["STT12"].Value = "0";
                rep.Parameters["STT13"].Value = "0";
                rep.Parameters["STT14"].Value = "0";
                rep.Parameters["STT15"].Value = STT15;
                rep.Parameters["STT16"].Value = STT16;
                rep.Parameters["STT17"].Value = STT17;
                rep.Parameters["STT18"].Value = STT18;
                rep.Parameters["STT19"].Value = STT19;
                rep.Parameters["STT20"].Value = STT20;
                rep.Parameters["STT21"].Value = STT21;
                rep.Parameters["STT22"].Value = "0";
                rep.Parameters["STT23"].Value = "0";
                rep.Parameters["STT24"].Value = "0";
                rep.Parameters["STT25"].Value = "0";
                rep.Parameters["STT26"].Value = "0";
                rep.Parameters["STT27"].Value = STT27;
                rep.Parameters["STT28"].Value = STT28;
                rep.Parameters["STT29"].Value = STT30 + STT31 + STT32;
                rep.Parameters["STT30"].Value = STT30;
                rep.Parameters["STT31"].Value = STT31;
                rep.Parameters["STT32"].Value = STT32;
                rep.Parameters["STT33"].Value = "0";
                rep.Parameters["STT34"].Value = STT35 + STT36 + STT37 + STT38;
                rep.Parameters["STT35"].Value = STT35;
                rep.Parameters["STT36"].Value = STT36;
                rep.Parameters["STT37"].Value = STT37;
                rep.Parameters["STT38"].Value = STT38;
                rep.Parameters["STT39"].Value = STT39;
                rep.Parameters["STT40"].Value = STT40;
                rep.Parameters["STT41"].Value = STT41;
                rep.Parameters["STT42"].Value = "0";
                rep.Parameters["STT43"].Value = STT44 + STT45 + STT46 + STT47 + STT48;
                rep.Parameters["STT44"].Value = STT44;
                rep.Parameters["STT45"].Value = STT45;
                rep.Parameters["STT46"].Value = STT46;
                rep.Parameters["STT47"].Value = STT47;
                rep.Parameters["STT48"].Value = STT48;
                rep.Parameters["STT49"].Value = STT49;
                rep.Parameters["STT50"].Value = "0";
                rep.Parameters["STT51"].Value = "0";
                rep.Parameters["STT52"].Value = "0";
                rep.Parameters["STT53"].Value = "0";
                rep.Parameters["STT54"].Value = STT54;
                rep.Parameters["STT55"].Value = STT55;
                rep.Parameters["STT56"].Value = stt57 + stt58;
                rep.Parameters["STT57"].Value = stt57;
                rep.Parameters["STT58"].Value = stt58;
                rep.Parameters["STT59"].Value = "0";
                rep.Parameters["STT60"].Value = "0";
                rep.Parameters["STT61"].Value = "0";
                rep.Parameters["STT62"].Value = "0";
                rep.Parameters["STT63"].Value = "0";
                rep.Parameters["STT64"].Value = "0";
                rep.Parameters["STT65"].Value = "0";
                rep.Parameters["STT66"].Value = "0";
                rep.Parameters["STT67"].Value = "0";
                rep.Parameters["STT68"].Value = STT68;
                rep.Parameters["STT69"].Value = STT69;
                rep.Parameters["STT70"].Value = STT70;
                rep.Parameters["STT71"].Value = STT71;
                rep.Parameters["STT72"].Value = STT68 - STT69 - STT70 - STT71;
                rep.Parameters["STT73"].Value = "0";
                rep.Parameters["STT74"].Value = "0";
                rep.Parameters["STT75"].Value = "0";
                rep.Parameters["STT76"].Value = "0";
                rep.Parameters["STT77"].Value = "0";
                rep.Parameters["STT78"].Value = "0";
                rep.Parameters["STT79"].Value = "0";
                rep.Parameters["STT80"].Value = "0";
                rep.Parameters["STT81"].Value = STT82 + STT83 + STT84;
                rep.Parameters["STT82"].Value = STT82;
                rep.Parameters["STT83"].Value = STT83;
                rep.Parameters["STT84"].Value = STT84;
                rep.Parameters["STT85"].Value = STT86 + STT87 + STT88;
                rep.Parameters["STT86"].Value = STT86;
                rep.Parameters["STT87"].Value = STT87;
                rep.Parameters["STT88"].Value = STT88;
                rep.Parameters["STT89"].Value = STT90 + STT91 + STT92;
                rep.Parameters["STT90"].Value = STT90;
                rep.Parameters["STT91"].Value = STT91;
                rep.Parameters["STT92"].Value = STT92;
                rep.Parameters["STT93"].Value = "0";
                rep.Parameters["STT94"].Value = "0";
                rep.Parameters["STT95"].Value = "0";
                rep.Parameters["STT96"].Value = "0";
                rep.Parameters["STT97"].Value = STT98 + STT99 + STT100;
                rep.Parameters["STT98"].Value = STT98;
                rep.Parameters["STT99"].Value = STT99;
                rep.Parameters["STT100"].Value = STT100;
                rep.Parameters["STT101"].Value = "0";
                rep.Parameters["STT102"].Value = "0";
                rep.Parameters["STT103"].Value = "0";
                rep.Parameters["STT104"].Value = "0";
                rep.Parameters["STT105"].Value = "0";
                rep.Parameters["STT106"].Value = "0";
                rep.Parameters["STT107"].Value = "0";
                rep.Parameters["STT108"].Value = "0";
                rep.Parameters["STT109"].Value = "0";
                rep.Parameters["STT110"].Value = "0";
                rep.Parameters["STT111"].Value = "0";
                rep.Parameters["STT112"].Value = "0";
                rep.Parameters["STT113"].Value = STT114 + STT115 + STT116;
                rep.Parameters["STT114"].Value = STT114;
                rep.Parameters["STT115"].Value = STT115;
                rep.Parameters["STT116"].Value = STT116;
                rep.Parameters["STT117"].Value = STT118 + STT119;
                rep.Parameters["STT118"].Value = STT118;
                rep.Parameters["STT119"].Value = STT119;
                rep.Parameters["STT120"].Value = "0";
                rep.Parameters["STT121"].Value = "0";
                rep.Parameters["STT122"].Value = "0";
                rep.Parameters["STT123"].Value = "0";
                rep.Parameters["STT124"].Value = "0";
                rep.Parameters["STT125"].Value = "0";
                rep.Parameters["STT126"].Value = "0";
                rep.Parameters["STT127"].Value = "0";
                rep.Parameters["STT128"].Value = "0";
                rep.Parameters["STT129"].Value = "0";
                rep.Parameters["STT130"].Value = "0";
                rep.Parameters["STT131"].Value = "0";
                rep.Parameters["STT132"].Value = "0";
                rep.Parameters["STT133"].Value = "0";
                rep.Parameters["STT134"].Value = "0";
                rep.Parameters["STT135"].Value = "0";
                rep.Parameters["STT136"].Value = "0";
                rep.Parameters["STT137"].Value = "0";
                rep.Parameters["STT138"].Value = "0";
                rep.Parameters["STT139"].Value = "0";
                rep.Parameters["STT140"].Value = "0";
                rep.Parameters["STT141"].Value = "0";
                rep.Parameters["STT142"].Value = "0";
                rep.Parameters["STT143"].Value = "0";
                rep.Parameters["STT144"].Value = "0";
                rep.Parameters["STT145"].Value = "0";
                rep.Parameters["STT146"].Value = "0";
                rep.Parameters["STT147"].Value = "0";
                rep.Parameters["STT148"].Value = "0";
                rep.Parameters["STT149"].Value = STT150 + STT159;
                rep.Parameters["STT150"].Value = STT150;
                rep.Parameters["STT151"].Value = STT151;
                rep.Parameters["STT152"].Value = STT152;
                rep.Parameters["STT153"].Value = STT153;
                rep.Parameters["STT154"].Value = STT154;
                rep.Parameters["STT155"].Value = STT155;
                rep.Parameters["STT156"].Value = STT156;
                rep.Parameters["STT157"].Value = STT157;
                rep.Parameters["STT158"].Value = STT158;
                rep.Parameters["STT159"].Value = STT159;
                rep.Parameters["STT160"].Value = STT160;
                rep.Parameters["STT161"].Value = STT161;
                rep.Parameters["STT162"].Value = "0";
                rep.Parameters["STT163"].Value = "0";
                rep.Parameters["STT164"].Value = "0";
                rep.Parameters["STT165"].Value = "0";
                rep.Parameters["STT166"].Value = "0";
                rep.Parameters["STT167"].Value = "0";
                rep.Parameters["STT168"].Value = "0";
                rep.Parameters["STT169"].Value = "0";
                rep.Parameters["STT170"].Value = "0";
                rep.Parameters["STT171"].Value = "0";
                rep.Parameters["STT172"].Value = "0";
                rep.Parameters["STT173"].Value = "0";
                rep.Parameters["STT174"].Value = "0";
                rep.Parameters["STT175"].Value = "0";
                rep.Parameters["STT176"].Value = "0";
                rep.Parameters["STT177"].Value = "0";
                rep.Parameters["STT178"].Value = "0";
                rep.Parameters["STT179"].Value = "0";
                rep.Parameters["STT180"].Value = "0";
                rep.Parameters["STT181"].Value = "0";
                rep.Parameters["STT182"].Value = "0";
                rep.Parameters["STT183"].Value = "0";
                rep.Parameters["STT184"].Value = "0";
                rep.Parameters["STT185"].Value = "0";
                rep.Parameters["STT186"].Value = "0";
                rep.Parameters["STT187"].Value = "0";
                rep.Parameters["STT188"].Value = "0";
                rep.Parameters["STT189"].Value = "0";
                rep.Parameters["STT190"].Value = "0";
                rep.Parameters["STT191"].Value = "0";
                rep.Parameters["STT192"].Value = "0";
                rep.Parameters["STT193"].Value = "0";
                rep.Parameters["STT194"].Value = "0";
                rep.Parameters["STT195"].Value = "0";
                rep.Parameters["STT196"].Value = "0";
                rep.Parameters["STT197"].Value = "0";
                rep.Parameters["STT198"].Value = "0";
                rep.Parameters["STT199"].Value = "0";
                #endregion
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Thời gian chọn báo cáo quá dài");

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        class stt
        {
            public string sttnam1 { get; set; }
            public string sttnam2 { get; set; }
            public string sttnam3 { get; set; }
        }
    }
}
