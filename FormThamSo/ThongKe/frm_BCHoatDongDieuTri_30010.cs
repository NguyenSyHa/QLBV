using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach;

namespace QLBV.FormThamSo
{
    public partial class frm_BCHoatDongDieuTri_30010 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCHoatDongDieuTri_30010()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private class Khoa
        {
            private string TenKP;
            private int MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }

        List<Khoa> _Kphong = new List<Khoa>();
        
        private void frm_BCHoatDongDieuTri_30010_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                Khoa themmoi1 = new Khoa();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    Khoa themmoi = new Khoa();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }

        public class DTri
        {
            public string MaBNhan { set; get; }

            private int makp;

            public int MaKP
            {
                get { return makp; }
                set { makp = value; }
            }
            private string khoa;

            public string Khoa
            {
                get { return khoa; }
                set { khoa = value; }
            }
            private int c1;

            public int C1
            {
                get { return c1; }
                set { c1 = value; }
            }
            private int c2;

            public int C2
            {
                get { return c2; }
                set { c2 = value; }
            }
            private int c3;

            public int C3
            {
                get { return c3; }
                set { c3 = value; }
            }
            private int c4;

            public int C4
            {
                get { return c4; }
                set { c4 = value; }
            }
            private int c5;

            public int C5
            {
                get { return c5; }
                set { c5 = value; }
            }
            private int c6;

            public int C6
            {
                get { return c6; }
                set { c6 = value; }
            }
            private int c7;

            public int C7
            {
                get { return c7; }
                set { c7 = value; }
            }
            private int c8;

            public int C8
            {
                get { return c8; }
                set { c8 = value; }
            }
            private int c9;

            public int C9
            {
                get { return c9; }
                set { c9 = value; }
            }
            private int c10;

            public int C10
            {
                get { return c10; }
                set { c10 = value; }
            }
            private int c11;

            public int C11
            {
                get { return c11; }
                set { c11 = value; }
            }
            public int C12 { set; get; }
            public int C13 { set; get; }
            public int C14 { set; get; }
            public int C71 { set; get; }
        }

        public class TH
        {
            public int MaBNhan { set; get; }
            public string SThe { set; get; }
            public int GTinh { set; get; }
            public int CapCuu { set; get; }
            public int Tuoi { set; get; }
            public int MaKP { set; get; }
            public int MaCK { set; get; }
            public int IDKB { set; get; }           
            public int SoNgaydt { set; get; }
            public string KetQua { set; get; }
            public int Status { set; get; }
            public DateTime? NgayVao { set; get; }
            public DateTime? NgayRa { set; get; }

        }
        List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
        List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da1 = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;           
            List<DTri> _lDT = new List<DTri>();
            List<Khoa> _lKhoaP = new List<Khoa>();

            if (KTtaoBc())
            {
                _lDT.Clear();
                _lKhoaP.Clear();
                _da.Clear();
                _da1.Clear();
                var bv = data.BenhViens.ToList();
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                //frmIn frm = new frmIn();

                #region Hiển thị thời gian
                int nam = Convert.ToInt32(tungay.Year);
                int thang = Convert.ToInt32(tungay.Month);
                string _ntn = "";
                if (txtNgayThang.Text == "")
                { _ntn = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text; }
                else
                {
                    _ntn = txtNgayThang.Text;
                }

                #endregion
                _lKhoaP = _Kphong.Where(p => p.makp != 0).Where(p => p.chon == true).ToList();         
                foreach(var item in _Kphong)
                {

                    _da1 = frm_NhapBuongGiuongKeKoach.getBuongGiuong(data, item.makp, nam.ToString());
                    foreach(var item1 in _da1)
                    {
                        _da.Add(item1);
                    }
                }
                #region tìm theo ngày ra viện
                if (rdLoaiNgay.SelectedIndex == 0)
                {                   
                    var q1 = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                              join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                              join rv in data.RaViens.Where(p=>p.NgayRa >= tungay && p.NgayRa <= denngay) on bn.MaBNhan equals rv.MaBNhan                            
                              select new 
                              {
                                    MaBNhan = bn.MaBNhan,
                                    SThe = bn.SThe,
                                    GTinh = bn.GTinh ?? 0,
                                    Tuoi = bn.Tuoi ?? 0,
                                    CapCuu = bn.CapCuu ?? 0,
                                    SoNgaydt = rv.SoNgaydt??0, 
                                    KetQua = rv.KetQua,
                                    NgayVao = rv.NgayVao,
                                    NgayRa = rv.NgayRa,
                                    Status = rv.Status,
                                    MaCK = rv.MaCK,
                                    MaKP = rv.MaKP
                              }).ToList();
                  
                    var q4 = (from bn in q1
                              join kp in _lKhoaP on bn.MaKP equals kp.makp
                              //join bg in _da on kp.makp equals bg.makp
                              join ck in data.ChuyenKhoas on bn.MaCK equals ck.MaCK into kq from kq1 in kq.DefaultIfEmpty()
                              select new
                              {
                                  kp.tenkp,
                                  TenCK = kq1 == null ? "" :  kq1.TenCK,
                                  Nu = bn.GTinh == 0 ? 1 : 0,
                                  BHYT = (bn.SThe != null && bn.SThe != "") ? 1 : 0,
                                  TE = (bn.Tuoi < 15 ) ? 1 : 0,
                                  TE1 = (bn.Tuoi <= 6) ? 1 : 0,
                                  CapCuu = bn.CapCuu == 1 ? 1 : 0,
                                  bn.SoNgaydt,
                                  TuVong = bn.KetQua == "Tử vong" ? 1 : 0,
                                  TETuVong = ((bn.Tuoi < 15) && bn.KetQua == "Tử vong") ? 1 : 0,
                                  TuVong24 = (bn.KetQua == "Tử vong" && bn.NgayRa != null && bn.NgayVao != null && (bn.NgayRa.Value - bn.NgayVao.Value).TotalHours < 24) ? 1 : 0,
                                  ChuyenVien = bn.Status == 1 ? 1 : 0,
                                  //bg.giuongKH,
                                  //bg.giuongTT
                              }).ToList();


                    _lDT = (from bn in q4
                            group bn by new { bn.tenkp } into kq
                            select new DTri
                            {
                                Khoa = kq.Key.tenkp,
                                //C1 = Convert.ToInt32(kq.FirstOrDefault().giuongKH),
                                //C2 = kq.Count(),
                                C3 = kq.Count(),
                                C4 = kq.Sum(p => p.Nu),
                                C5 = kq.Sum(p => p.BHYT),
                                C6 = kq.Where(p => p.TenCK == "Đông y" || p.TenCK == "YHCT-PHCN").Count(),
                                C7 = kq.Sum(p => p.TE),
                                C71 = kq.Sum(p => p.TE1),
                                C8 = kq.Sum(p => p.CapCuu),
                                C9 = kq.Sum(p => p.SoNgaydt),
                                C11 = kq.Sum(p => p.TuVong),
                                C12 = kq.Sum(p => p.TETuVong),
                                C13 = kq.Sum(p => p.TuVong24),
                                C14 = kq.Sum(p => p.ChuyenVien)
                            }).ToList();
                }
                #endregion

                #region tìm theo ngày vào viện
                else if (rdLoaiNgay.SelectedIndex == 1)
                {
                    var q1 = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                              join vv in data.VaoViens.Where(p =>p.NgayVao >= tungay && p.NgayVao <= denngay) on bn.MaBNhan equals vv.MaBNhan
                              join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                              from kq1 in kq.DefaultIfEmpty()                             
                              select new
                              {
                                  bn.MaBNhan,
                                  bn.SThe,
                                  bn.GTinh,
                                  bn.CapCuu,
                                  bn.Tuoi,
                                  TenCKvv = vv.ChuyenKhoa,// tên chuyên khoa vào viện
                                  MaCK = (kq1 != null && kq1.NgayRa <= denngay) ? kq1.MaCK : null,//mã chuyên khoa ra viện
                                  MaKP = (kq1 != null && kq1.NgayRa <= denngay) ? kq1.MaKP : vv.MaKP,
                                  KetQua = (kq1 != null && kq1.NgayRa <= denngay) ? kq1.KetQua : "",
                                  Status = (kq1 != null && kq1.NgayRa <= denngay) ? kq1.Status : 0,
                                  NgayVao = vv.NgayVao,
                                  NgayRa = (kq1 != null && kq1.NgayRa <= denngay) ? kq1.NgayRa : null,
                                  SoNgaydt = (kq1 != null && kq1.NgayRa <= denngay) ? (kq1.SoNgaydt ?? 0) : 0
                              }).ToList();

                    var q4 = (from bn in q1
                              join kp in _lKhoaP on bn.MaKP equals kp.makp
                              //join bg in _da on kp.makp equals bg.makp
                              join ck in data.ChuyenKhoas on bn.MaCK equals ck.MaCK into kq
                              from kq1 in kq.DefaultIfEmpty()
                              select new
                              {
                                  kp.tenkp,
                                  TenCK = kq1 == null ? bn.TenCKvv : kq1.TenCK,
                                  Nu = bn.GTinh == 0 ? 1 : 0,
                                  BHYT = (bn.SThe != null && bn.SThe != "") ? 1 : 0,
                                  TE = (bn.Tuoi < 15) ? 1 : 0,
                                  TE1 = (bn.Tuoi <= 6) ? 1 : 0,
                                  CapCuu = bn.CapCuu == 1 ? 1 : 0,
                                  bn.SoNgaydt,
                                  TuVong = bn.KetQua == "Tử vong" ? 1 : 0,
                                  TETuVong = ((bn.Tuoi < 15) && bn.KetQua == "Tử vong") ? 1 : 0,
                                  TuVong24 = (bn.KetQua == "Tử vong" && bn.NgayRa != null && bn.NgayVao != null && (bn.NgayRa.Value - bn.NgayVao.Value).TotalHours < 24) ? 1 : 0,
                                  ChuyenVien = bn.Status == 1 ? 1 : 0,
                                  //bg.giuongTT,
                                  //bg.giuongKH
                              }).ToList();


                    _lDT = (from bn in q4
                            group bn by new { bn.tenkp } into kq
                            select new DTri
                            {
                                Khoa = kq.Key.tenkp,
                                //C1 = Convert.ToInt32(kq.FirstOrDefault().giuongKH),
                                //C2 = kq.Count(),
                                C3 = kq.Count(),
                                C4 = kq.Sum(p => p.Nu),
                                C5 = kq.Sum(p => p.BHYT),
                                C6 = kq.Where(p => p.TenCK == "Đông y" || p.TenCK == "YHCT-PHCN").Count(),
                                C7 = kq.Sum(p => p.TE),
                                C71 = kq.Sum(p => p.TE1),
                                C8 = kq.Sum(p => p.CapCuu),
                                C9 = kq.Sum(p => p.SoNgaydt),
                                C11 = kq.Sum(p => p.TuVong),
                                C12 = kq.Sum(p => p.TETuVong),
                                C13 = kq.Sum(p => p.TuVong24),
                                C14 = kq.Sum(p => p.ChuyenVien)
                            }).ToList();
                }
                #endregion

                #region tìm theo ngày điều trị
                else if (rdLoaiNgay.SelectedIndex == 2)
                {                   
                    var q1 = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                             // join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                              join vv in data.VaoViens.Where(p=>p.NgayVao <= denngay) on bn.MaBNhan equals vv.MaBNhan
                              join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                              from kq1 in kq.DefaultIfEmpty()
                              where kq1 == null || // bệnh nhân chưa ra viện                             
                              (kq1 != null && (( kq1.NgayRa > denngay && vv.NgayVao < tungay) || (kq1.NgayRa >= tungay && kq1.NgayRa <= denngay) || (vv.NgayVao >= tungay && vv.NgayVao <= denngay)))
                              select new
                              {
                                  bn.MaBNhan,
                                  bn.SThe,
                                  bn.GTinh,
                                  bn.CapCuu,
                                  bn.Tuoi,
                                  //bnkb.MaKP,
                                  //bnkb.IDKB,
                                  //bnkb.NgayKham,
                                  //bnkb.MaCK, 
                                  TenCKvv = vv.ChuyenKhoa,// tên chuyên khoa vào viện
                                  MaCK = (kq1 != null && kq1.NgayRa <= denngay) ?  kq1.MaCK :null,//mã chuyên khoa ra viện
                                  MaKP = (kq1 != null && kq1.NgayRa <= denngay) ?  kq1.MaKP :vv.MaKP ,
                                  KetQua = (kq1 != null && kq1.NgayRa <= denngay) ?  kq1.KetQua: "",
                                  Status = (kq1 != null && kq1.NgayRa <= denngay) ? kq1.Status : 0,
                                  NgayVao = vv.NgayVao,
                                  NgayRa = (kq1 != null && kq1.NgayRa <= denngay)  ? kq1.NgayRa : null,
                                  SoNgaydt = (kq1 != null && kq1.NgayRa <= denngay)  ?  (kq1.SoNgaydt ??0): 0
                              }).ToList();                   
                    
                    //var q3 = (from bn in q1 group bn by new { bn.MaBNhan, bn.Tuoi, bn.SThe, bn.CapCuu, bn.GTinh,  bn.NgayVao, bn.NgayRa, bn.SoNgaydt, bn.KetQua, bn.Status } 
                    //              into kq select new 
                    //              { kq.Key.MaBNhan, kq.Key.Tuoi, kq.Key.SThe, kq.Key.CapCuu, kq.Key.GTinh,  kq.Key.NgayVao, kq.Key.NgayRa, kq.Key.SoNgaydt, kq.Key.KetQua, kq.Key.Status,
                    //                  MaCK = q1.Where(p=>p.IDKB == kq.Max(t=>t.IDKB)).Select(p=>p.MaCK).FirstOrDefault(),
                    //                  MaKP = q1.Where(p => p.IDKB == kq.Max(t => t.IDKB)).Select(p => p.MaKP).FirstOrDefault(),
                    //              }).ToList();

                    var q4 = (from bn in q1
                              join kp in _lKhoaP on bn.MaKP equals kp.makp
                              //join bg in _da on kp.makp equals bg.makp
                              join ck in data.ChuyenKhoas on bn.MaCK equals ck.MaCK into kq
                              from kq1 in kq.DefaultIfEmpty()
                              select new
                              {
                                  kp.tenkp,
                                  TenCK = kq1 == null  ? bn.TenCKvv : kq1.TenCK,
                                  Nu = bn.GTinh == 0 ? 1 : 0,
                                  BHYT = (bn.SThe != null && bn.SThe != "") ? 1 : 0,
                                  TE = (bn.Tuoi < 15 ) ? 1 : 0,
                                  TE1 = (bn.Tuoi <= 6) ? 1 : 0,
                                  CapCuu = bn.CapCuu == 1 ? 1 : 0,
                                  bn.SoNgaydt,
                                  TuVong = bn.KetQua == "Tử vong" ? 1 : 0,
                                  TETuVong = ((bn.Tuoi < 15) && bn.KetQua == "Tử vong") ? 1 : 0,
                                  TuVong24 = (bn.KetQua == "Tử vong" && bn.NgayRa != null && bn.NgayVao != null && (bn.NgayRa.Value - bn.NgayVao.Value).TotalHours < 24) ? 1 : 0,
                                  ChuyenVien = bn.Status == 1 ? 1 : 0,
                                  //bg.giuongKH,
                                  //bg.giuongTT
                              }).ToList();


                    _lDT = (from bn in q4
                            group bn by new { bn.tenkp } into kq
                            select new DTri
                            {
                                Khoa = kq.Key.tenkp,
                                //C1 = Convert.ToInt32(kq.FirstOrDefault().giuongKH),
                                //C2 = kq.Count(),
                                C3 = kq.Count(),
                                C4 = kq.Sum(p => p.Nu),
                                C5 = kq.Sum(p => p.BHYT),
                                C6 = kq.Where(p => p.TenCK == "Đông y" || p.TenCK == "YHCT-PHCN").Count(),
                                C7 = kq.Sum(p => p.TE),
                                C71 = kq.Sum(p => p.TE1),
                                C8 = kq.Sum(p => p.CapCuu),
                                C9 = kq.Sum(p => p.SoNgaydt),
                                C11 = kq.Sum(p => p.TuVong),
                                C12 = kq.Sum(p => p.TETuVong),
                                C13 = kq.Sum(p => p.TuVong24),
                                C14 = kq.Sum(p => p.ChuyenVien)
                            }).ToList();
                }
                #endregion

                #region Lấy số giường bệnh thực kê
                List<KPhong> lkp = data.KPhongs.ToList();
                foreach (DTri dt in _lDT)
                {
                    dt.C1 = getSoGiuongBenhKH(dt.Khoa, lkp);
                    dt.C2 = getSoGiuongBenh(dt.Khoa, lkp);
                }
                #endregion

                #region xuat Excel

                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" ,"@", "@", "@" };
                string[] _tieude = { "STT", "Khoa", "Số giường bệnh kế hoạch", "Số giường bệnh thực kê", "TS người bệnh điều trị nội trú","Nữ", "BHYT","YHCT (kể cả kết hopwj YHHĐ)", "TE <= 6 tuổi","6 < TE <15 tuổi", "Số cấp cứu", "Số ngày ĐTNT", "TS người bệnh tử vong", "Số người bệnh tử vong là TE<15 tuổi", "Số người bệnh tử vong trước 24 giờ", "Người bệnh chuyển viện" };
                int[] _arrWidth = new int[] { };// { 5, 15, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

                DungChung.Bien.MangHaiChieu = new Object[_lDT.Count + 18, 16];
                DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQCQ.ToUpper();
                DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.TenCQ.ToUpper();
                DungChung.Bien.MangHaiChieu[2, 2] = ("BÁO CÁO HOẠT ĐỘNG  ĐIỀU TRỊ NỘI TRÚ").ToUpper();
                DungChung.Bien.MangHaiChieu[3, 2] = _ntn;

                DungChung.Bien.MangHaiChieu[_lDT.Count() + 5, 7] = "Ngày ...... tháng ..... năm .....";
                DungChung.Bien.MangHaiChieu[_lDT.Count() + 6, 1] = ("Người lập biểu").ToUpper();
                DungChung.Bien.MangHaiChieu[_lDT.Count() + 10, 1] = DungChung.Bien.NguoiLapBieu;
                DungChung.Bien.MangHaiChieu[_lDT.Count() + 6, 4] = ("TRƯỞNG PHÒNG KHTH").ToUpper();
                DungChung.Bien.MangHaiChieu[_lDT.Count() + 10, 4] = "";
                DungChung.Bien.MangHaiChieu[_lDT.Count() + 6, 7] = ("Giám đốc").ToUpper();
                DungChung.Bien.MangHaiChieu[_lDT.Count() + 10, 7] = DungChung.Bien.GiamDoc;
                for (int i = 0; i < _tieude.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[4, i] = _tieude[i];
                }
                int num = 5;
                foreach (var r in _lDT)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num - 4;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.Khoa;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.C1;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.C2;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.C3;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.C4;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.C5;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.C6;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.C7;
                    DungChung.Bien.MangHaiChieu[num, 9] = r.C71;
                    DungChung.Bien.MangHaiChieu[num, 10] = r.C8;
                    DungChung.Bien.MangHaiChieu[num, 11] = r.C9;
                    DungChung.Bien.MangHaiChieu[num, 12] = r.C11;
                    DungChung.Bien.MangHaiChieu[num, 13] = r.C12;
                    DungChung.Bien.MangHaiChieu[num, 14] = r.C13;
                    DungChung.Bien.MangHaiChieu[num, 15] = r.C14;
                    num++;

                }

                #endregion
                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo hoạt động điều trị", "C:\\BcHDDT.xls", true, this.Name);
                BaoCao.Rep_BcHoatDongDTri_30010 rep = new BaoCao.Rep_BcHoatDongDTri_30010();
                if (txtNgayThang.Text != "")
                    rep.TuNgay.Value = txtNgayThang.Text;
                else
                    rep.TuNgay.Value = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                rep.DataSource = _lDT;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
  
        private int getSoGiuongBenh(string tenKhoa, List<KPhong> lkp)
        {
            KPhong kp = lkp.Where(p => p.TenKP == tenKhoa).FirstOrDefault();
            int sogiuong = 0;
            if (kp != null)
            {
                int makkpp = kp.MaKP;
                if (_da.Where(p => p.makp == makkpp).Count() > 0)
                sogiuong = _da.Where(p => p.makp == makkpp).Count();
            }
            return sogiuong;

        }

        private int getSoGiuongBenhKH(string tenKhoa, List<KPhong> lkp)
        {
            KPhong kp = lkp.Where(p => p.TenKP == tenKhoa).FirstOrDefault();
            int giuongkh = 0;
            if (kp != null)
            {
                int makkpp = kp.MaKP;
                if (_da.Where(p => p.makp == makkpp).Count() > 0)
                giuongkh = Convert.ToInt32(_da.Where(p => p.makp == makkpp).FirstOrDefault().giuongKH);
            }
            return giuongkh;

        }

        private int songaydt(DateTime? ngaykham, DateTime? ngayra, int idkb, int maBNhan, List<BNKB> allBNKB)
        {
           
                var bnkb = (from a in allBNKB.Where(p => p.MaBNhan == maBNhan) join b in _Kphong on a.MaKP equals b.makp select a).OrderBy(p => p.IDKB).ToList();
                int sngaydt = 0;

                if (ngaykham == null || (ngaykham.Value.Day > DateTime.Now.Day))
                    return 0;
                else
                {
                    int num = 1;
                    DateTime _ngayra = DungChung.Ham.NgayTu(ngayra ?? DateTime.Now);
                    foreach (var a in bnkb)
                    {
                        if (a.IDKB == idkb)
                        {
                            DateTime _ngaykham = DungChung.Ham.NgayTu(ngaykham.Value);

                            if (num == bnkb.Count())//id khám bệnh là id lớn nhất
                            {
                                sngaydt = (int)(_ngayra - _ngaykham).TotalDays;
                                break;
                            }
                            else
                            {
                                DateTime _ngaykhamTiep = bnkb.Skip(num).First().NgayKham.Value;
                                sngaydt = (int)(_ngayra - _ngaykham).TotalDays;
                                break;
                            }
                        }
                        num++;
                    }
                    if (num == 1 && ngaykham.Value.Hour < 20)//lần khám đầu tiên
                        sngaydt = sngaydt + 1;
                    return sngaydt;
                }
           
        }
        private bool KTtaoBc()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }

        private void grvKhoaphong_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}