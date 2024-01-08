using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{

    public partial class Frm_BcHoatDongKKB_TH_30010 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcHoatDongKKB_TH_30010()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

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

        private class KhPhong
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

        List<KhPhong> _Kphong = new List<KhPhong>();

        private void Frm_BcHoatDongKKB_HL01_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            rdLoaiNgay.SelectedIndex = 3;

            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KhPhong themmoi1 = new KhPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KhPhong themmoi = new KhPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }
        #region class KB
        public class KB
        {
            private string chuyenkhoa;

            public string ChuyenKhoa
            {
                get { return chuyenkhoa; }
                set { chuyenkhoa = value; }
            }
            public int TS { get; set; }// tổng số
            public int Nu// số bn nữ
            {
                set;
                get;
            }
            public int TE6 { get; set; } // trẻ em dưới 6 tuổi
            public int BH { get; set; }
            public int TE { get; set; }//trẻ em dưới 15 tuổi
            public int YHCT { get; set; }// Y học cổ truyền
            public int CC { get; set; }// cấp cứu
            public int VV { get; set; }// vào viện
            public int CV { get; set; }//chuyển viện
            public int TSDTri { get; set; }//tổng số người đtrị ngoại trú
            public int SoNgay { get; set; }
            public string kphong { set; get; }
        }
        #endregion

        #region class TH
        public class TH
        {
            public int MaBNhan { set; get; }
            public int NoiTru { set; get; }
            public bool DTNT { set; get; }
            public int GTinh { set; get; }
            public string SThe { set; get; }
            public int Tuoi { set; get; }
            public int CapCuu { set; get; }
            public int PhuongAn { set; get; }
            public DateTime NgayKham { set; get; }
            public int IDKB { set; get; }
            public int NgayDTri { set; get; }
            public int BNDara { set; get; }
            public int MaKP { set; get; }
            public string TenKP { set; get; }
            public int MaCK { set; get; }
            public string TenCK { set; get; }
            public string TenCKDauTien { set; get; }
            public string KpDauTien { set; get; }
            public int ChanDoan { set; get; }
            public int VaoVien { set; get; }
            public int ChuyenVien { set; get; }
            public int SoNguoiDTNT { set; get; }
        }
        #endregion
        //--++-- số ngày điều trị (theo người khám) ngoại trú = --++--
        //-------- bệnh nhân nội trú hoặc khám bệnh : 0
        //-------- Là khoa khám bệnh đầu tiên:
        //-------- + bệnh nhân đái tháo đường, phổi tắc nghẽn mạn tính (POPD) (tính ở trên) :30
        //-------- + bệnh nhân chuyên khoa răng hàm mặt hoặc YHCT : 5
        //-------- + chưa ra viện trong khoảng t/g đó : 1                   

        //--++-- số ngày điều trị (theo số lượt khám) ngoại trú = --++--
        //-------- bệnh nhân nội trú hoặc khám bệnh : 0                 
        //-------- bệnh nhân đái tháo đường, phổi tắc nghẽn mạn tính (POPD) (tính ở trên) :30
        //-------- bệnh nhân chuyên khoa răng hàm mặt hoặc YHCT : 5
        //-------- chưa ra viện trong khoảng t/g đó : 1
        //-------- Là khoa khám bệnh tiếp sau: 1
        private void btnOK_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            ////Bệnh nhân tiểu đường, tăng huyết áp, COPD điều trị ngoại trú tính 30 ngày, bệnh nhân YHCT 10ngày, BN RHM: 5 ngày
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            //  var qkphong = data.KhPhongs.ToList();
            List<KB> _lKB = new List<KB>();
            List<KB> _lNguoiBenh = new List<KB>();

            if (KTtaoBc())
            {
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

                List<KPhong> _lKhoaP = new List<KPhong>();
                List<TH> _lTH = new List<TH>();// danh sách tổng hợp bệnh nhân khám bệnh tìm thấy          
                _lKhoaP = (from kp in _Kphong.Where(p => p.makp > 0).Where(p => p.chon == true) select new KPhong { MaKP = kp.makp, TenKP = kp.tenkp }).ToList();

                var bv = data.BenhViens.ToList();
                _lKB.Clear();
                _lNguoiBenh.Clear();
                var lchuyenkhoa = (from ck in data.ChuyenKhoas select new { TenCKRG = (ck.TenCK.Contains("Tai Mũi Họng") || ck.TenCK.Contains("Răng Hàm Mặt") || ck.TenCK.Contains("Mắt")) ? "Khám liên chuyên khoa" : ck.TenCK, MaCK = ck.MaCK, Status = ck.Status, TenChiTiet = ck.TenChiTiet, TenCK = ck.TenCK }).ToList();
                #region lấy theo ngày ra viện
                if (rdLoaiNgay.SelectedIndex == 1)
                {


                    var qBNKB0 = (from rv in data.RaViens.Where(p => p.NgayRa <= denngay && p.NgayRa >= tungay)
                                  join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                                  join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                  select new
                                  {
                                      bn.MaBNhan,
                                      bn.NoiTru,
                                      bn.DTNT,
                                      bn.GTinh,
                                      bn.SThe,
                                      bn.Tuoi,
                                      bn.CapCuu,
                                      bnkb.NgayKham,
                                      bnkb.MaKP,
                                      bnkb.MaCK,

                                      bnkb.IDKB,
                                      rv.SoNgaydt,
                                      rv.Status,
                                      rv.NgayVao,
                                  }).ToList();
                    var qBNKB = (from bn in qBNKB0
                                 join ck in lchuyenkhoa on bn.MaCK equals ck.MaCK into k
                                 from k1 in k.DefaultIfEmpty()
                                 select new
                                 {
                                     bn.MaBNhan,
                                     bn.NoiTru,
                                     bn.DTNT,
                                     bn.GTinh,
                                     bn.SThe,
                                     bn.Tuoi,
                                     bn.CapCuu,
                                     bn.NgayKham,
                                     bn.MaKP,
                                     bn.MaCK,
                                     TenCK = (k1 != null) ? k1.TenCKRG : "Khác",
                                     TenKP = k1 != null ? k1.TenCK : "Khác",
                                     bn.IDKB,
                                     bn.SoNgaydt,
                                     bn.Status,
                                     bn.NgayVao,
                                 }).ToList();

                    #region số lượt khám
                    if (rdMau.SelectedIndex == 0)
                        _lKB = (from kp in _lKhoaP
                                join bn in qBNKB on kp.MaKP equals bn.MaKP
                                group bn by new
                                {
                                    bn.TenCK,
                                    bn.TenKP,

                                } into kq
                                select new KB
                                {
                                    ChuyenKhoa = kq.Key.TenCK,
                                    kphong = kq.Key.TenKP,
                                    TS = kq.Count(),
                                    Nu = kq.Where(p => p.GTinh == 0).Count(),
                                    BH = kq.Where(p => p.SThe != null && p.SThe != "").Count(),
                                    YHCT = kq.Where(p => p.TenCK == "Đông y" || p.TenCK == "YHCT-PHCN").Count(),
                                    TE = kq.Where(p => p.Tuoi < 15).Count(),
                                    TE6 = kq.Where(p => p.Tuoi <= 6).Count(),
                                    CC = kq.Where(p => p.CapCuu == 1).Count(),
                                    VV = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    CV = kq.Where(p => p.Status == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    TSDTri = kq.Where(p => p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Distinct().Count(),
                                    SoNgay = kq.Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt ?? 0)
                                }).ToList();
                    #endregion
                    #region mẫu 2
                    else if (rdMau.SelectedIndex == 1)
                    {
                        _lKB = (from kp in _lKhoaP
                                join bn in qBNKB on kp.MaKP equals bn.MaKP
                                group bn by new
                                {
                                    bn.TenCK,
                                    bn.TenKP,
                                } into kq
                                select new KB
                                {
                                    ChuyenKhoa = kq.Key.TenCK,
                                    kphong = kq.Key.TenKP,
                                    TS = kq.Select(p => p.MaBNhan).Distinct().Count(),
                                    Nu = kq.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Distinct().Count(),
                                    BH = kq.Where(p => p.SThe != null && p.SThe != "").Select(p => p.MaBNhan).Distinct().Count(),
                                    YHCT = kq.Where(p => p.TenCK == "Đông y" || p.TenCK == "YHCT-PHCN").Select(p => p.MaBNhan).Distinct().Count(),
                                    TE = kq.Where(p => p.Tuoi < 15).Select(p => p.MaBNhan).Distinct().Count(),
                                    TE6 = kq.Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Distinct().Count(),
                                    CC = kq.Where(p => p.CapCuu == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    VV = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    CV = kq.Where(p => p.Status == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    TSDTri = kq.Where(p => p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Distinct().Count(),
                                    SoNgay = kq.Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt ?? 0)
                                }).ToList();
                    }
                    #endregion
                }
                #endregion
                #region lấy theo ngày vào viện
                else if (rdLoaiNgay.SelectedIndex == 0)
                {
                    //var qBNKB = (from bn in data.BenhNhans.Where(p => p.NNhap <= denngay && p.NNhap >= tungay)
                    //             join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                    //             join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                    //             from kq1 in kq.DefaultIfEmpty()
                    //             join ck in data.ChuyenKhoas on bnkb.MaCK equals ck.MaCK into k
                    //             from k1 in k.DefaultIfEmpty()
                    //             select new
                    //             {
                    //                 bn.MaBNhan,
                    //                 bn.NoiTru,
                    //                 bn.DTNT,
                    //                 bn.GTinh,
                    //                 bn.SThe,
                    //                 bn.Tuoi,
                    //                 bn.CapCuu,
                    //                 bnkb.NgayKham,
                    //                 bnkb.MaKP,
                    //                 bnkb.MaCK,
                    //                 TenCK = (k1 != null) ? ((k1.TenCK.Contains("Tai Mũi Họng") || k1.TenCK.Contains("Răng Hàm Mặt") || k1.TenCK.Contains("Mắt")) ? "Khám liên chuyên khoa" : k1.TenCK) : "Khác",
                    //                 TenKP = k1 != null ? k1.TenCK : "Khác",
                    //                 bnkb.IDKB,
                    //                 SoNgaydt = kq1 == null ? 0 : (kq1.SoNgaydt ?? 0),
                    //                 Status = kq1 == null ? 0 : (kq1.Status ?? 0),
                    //             }).ToList();

                    var qBNKB0 = (from bn in data.BenhNhans.Where(p => p.NNhap <= denngay && p.NNhap >= tungay)
                                  join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                  join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                                  from kq1 in kq.DefaultIfEmpty()
                                  select new
                                  {
                                      bn.MaBNhan,
                                      bn.NoiTru,
                                      bn.DTNT,
                                      bn.GTinh,
                                      bn.SThe,
                                      bn.Tuoi,
                                      bn.CapCuu,
                                      bnkb.NgayKham,
                                      bnkb.MaKP,
                                      bnkb.MaCK,
                                      bnkb.IDKB,
                                      SoNgaydt = kq1 == null ? 0 : (kq1.SoNgaydt ?? 0),
                                      Status = kq1 == null ? 0 : (kq1.Status ?? 0),
                                  }).ToList();

                    var qBNKB = (from bn in qBNKB0
                                 join ck in lchuyenkhoa on bn.MaCK equals ck.MaCK into k
                                 from k1 in k.DefaultIfEmpty()
                                 select new
                                 {
                                     bn.MaBNhan,
                                     bn.NoiTru,
                                     bn.DTNT,
                                     bn.GTinh,
                                     bn.SThe,
                                     bn.Tuoi,
                                     bn.CapCuu,
                                     bn.NgayKham,
                                     bn.MaKP,
                                     bn.MaCK,
                                     TenCK = (k1 != null) ? k1.TenCKRG : "Khác",
                                     TenKP = k1 != null ? k1.TenCK : "Khác",
                                     bn.IDKB,
                                     SoNgaydt = bn.SoNgaydt,
                                     Status = bn.Status,
                                 }).ToList();

                    #region số lượt khám
                    if (rdMau.SelectedIndex == 0)
                        _lKB = (from kp in _lKhoaP
                                join bn in qBNKB on kp.MaKP equals bn.MaKP
                                group bn by new
                                {
                                    bn.TenCK,
                                    bn.TenKP,

                                } into kq
                                select new KB
                                {
                                    ChuyenKhoa = kq.Key.TenCK,
                                    kphong = kq.Key.TenKP,
                                    TS = kq.Count(),
                                    Nu = kq.Where(p => p.GTinh == 0).Count(),
                                    BH = kq.Where(p => p.SThe != null && p.SThe != "").Count(),
                                    YHCT = kq.Where(p => p.TenCK == "Đông y" || p.TenCK == "YHCT-PHCN").Count(),
                                    TE = kq.Where(p => p.Tuoi < 15).Count(),
                                    TE6 = kq.Where(p => p.Tuoi <= 6).Count(),
                                    CC = kq.Where(p => p.CapCuu == 1).Count(),
                                    VV = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    CV = kq.Where(p => p.Status == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    TSDTri = kq.Where(p => p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Distinct().Count(),
                                    SoNgay = kq.Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt)
                                }).ToList();
                    #endregion
                    #region mẫu 2
                    else if (rdMau.SelectedIndex == 1)
                    {
                        _lKB = (from kp in _lKhoaP
                                join bn in qBNKB on kp.MaKP equals bn.MaKP
                                group bn by new
                                {
                                    bn.TenCK,
                                    bn.TenKP,
                                } into kq
                                select new KB
                                {
                                    ChuyenKhoa = kq.Key.TenCK,
                                    kphong = kq.Key.TenKP,
                                    TS = kq.Select(p => p.MaBNhan).Distinct().Count(),
                                    Nu = kq.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Distinct().Count(),
                                    BH = kq.Where(p => p.SThe != null && p.SThe != "").Select(p => p.MaBNhan).Distinct().Count(),
                                    YHCT = kq.Where(p => p.TenCK == "Đông y" || p.TenCK == "YHCT-PHCN").Select(p => p.MaBNhan).Distinct().Count(),
                                    TE = kq.Where(p => p.Tuoi < 15).Select(p => p.MaBNhan).Distinct().Count(),
                                    TE6 = kq.Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Distinct().Count(),
                                    CC = kq.Where(p => p.CapCuu == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    VV = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    CV = kq.Where(p => p.Status == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    TSDTri = kq.Where(p => p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Distinct().Count(),
                                    SoNgay = kq.Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt)
                                }).ToList();
                    }
                    #endregion
                }
                #endregion
                #region lấy theo các bệnh nhân đang khám và điều trị ngoại trú tại khoa trong khoảng thời gian đó
                else if (rdLoaiNgay.SelectedIndex == 2)
                {

                    //var qBNKB = (from bn in data.BenhNhans.Where(p => p.NNhap <= denngay)
                    //             join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                    //             join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                    //             from kq1 in kq.DefaultIfEmpty()
                    //             join ck in data.ChuyenKhoas on bnkb.MaCK equals ck.MaCK into k
                    //             from k1 in k.DefaultIfEmpty()
                    //             where (kq1 == null || (kq1 != null && ((kq1.NgayRa >= tungay && kq1.NgayRa <= denngay) || (bn.NNhap >= tungay) || (bn.NNhap < tungay && kq1.NgayRa > denngay))))
                    //             select new
                    //             {
                    //                 bn.MaBNhan,
                    //                 bn.NoiTru,
                    //                 bn.DTNT,
                    //                 bn.GTinh,
                    //                 bn.SThe,
                    //                 bn.Tuoi,
                    //                 bn.CapCuu,
                    //                 bnkb.NgayKham,
                    //                 bnkb.MaKP,
                    //                 bnkb.MaCK,
                    //                 TenCK = (k1 != null) ? ((k1.TenCK.Contains("Tai Mũi Họng") || k1.TenCK.Contains("Răng Hàm Mặt") || k1.TenCK.Contains("Mắt")) ? "Khám liên chuyên khoa" : k1.TenCK) : "Khác",
                    //                 TenKP = k1 != null ? k1.TenCK : "Khác",
                    //                 bnkb.IDKB,
                    //                 SoNgaydt = kq1 == null ? 0 : (kq1.SoNgaydt ?? 0),
                    //                 Status = kq1 == null ? 0 : (kq1.Status ?? 0),
                    //             }).ToList();
                    var qBNKB0 = (from bn in data.BenhNhans.Where(p => p.NNhap <= denngay)
                                  join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                  join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                                  from kq1 in kq.DefaultIfEmpty()

                                  where (kq1 == null || (kq1 != null && ((kq1.NgayRa >= tungay && kq1.NgayRa <= denngay) || (bn.NNhap >= tungay) || (bn.NNhap < tungay && kq1.NgayRa > denngay))))
                                  select new
                                  {
                                      bn.MaBNhan,
                                      bn.NoiTru,
                                      bn.DTNT,
                                      bn.GTinh,
                                      bn.SThe,
                                      bn.Tuoi,
                                      bn.CapCuu,
                                      bnkb.NgayKham,
                                      bnkb.MaKP,
                                      bnkb.MaCK,
                                      bnkb.IDKB,
                                      SoNgaydt = kq1 == null ? 0 : (kq1.SoNgaydt ?? 0),
                                      Status = kq1 == null ? 0 : (kq1.Status ?? 0),
                                  }).ToList();
                    var qBNKB = (from bn in qBNKB0
                                 join ck in lchuyenkhoa on bn.MaCK equals ck.MaCK into k
                                 from k1 in k.DefaultIfEmpty()
                                 select new
                                 {
                                     bn.MaBNhan,
                                     bn.NoiTru,
                                     bn.DTNT,
                                     bn.GTinh,
                                     bn.SThe,
                                     bn.Tuoi,
                                     bn.CapCuu,
                                     bn.NgayKham,
                                     bn.MaKP,
                                     bn.MaCK,
                                     TenCK = (k1 != null) ? k1.TenCKRG : "Khác",
                                     TenKP = k1 != null ? k1.TenCK : "Khác",
                                     bn.IDKB,
                                     SoNgaydt = bn.SoNgaydt,
                                     Status = bn.Status,
                                 }).ToList();


                    #region số lượt khám
                    if (rdMau.SelectedIndex == 0)
                        _lKB = (from kp in _lKhoaP
                                join bn in qBNKB on kp.MaKP equals bn.MaKP
                                group bn by new
                                {
                                    bn.TenCK,
                                    bn.TenKP,

                                } into kq
                                select new KB
                                {
                                    ChuyenKhoa = kq.Key.TenCK,
                                    kphong = kq.Key.TenKP,
                                    TS = kq.Count(),
                                    Nu = kq.Where(p => p.GTinh == 0).Count(),
                                    BH = kq.Where(p => p.SThe != null && p.SThe != "").Count(),
                                    YHCT = kq.Where(p => p.TenCK == "Đông y" || p.TenCK == "YHCT-PHCN").Count(),
                                    TE = kq.Where(p => p.Tuoi < 15).Count(),
                                    TE6 = kq.Where(p => p.Tuoi <= 6).Count(),
                                    CC = kq.Where(p => p.CapCuu == 1).Count(),
                                    VV = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    CV = kq.Where(p => p.Status == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    TSDTri = kq.Where(p => p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Distinct().Count(),
                                    SoNgay = kq.Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt)
                                }).ToList();
                    #endregion
                    #region mẫu 2
                    else if (rdMau.SelectedIndex == 1)
                    {
                        _lKB = (from kp in _lKhoaP
                                join bn in qBNKB on kp.MaKP equals bn.MaKP
                                group bn by new
                                {
                                    bn.TenCK,
                                    bn.TenKP,
                                } into kq
                                select new KB
                                {
                                    ChuyenKhoa = kq.Key.TenCK,
                                    kphong = kq.Key.TenKP,
                                    TS = kq.Select(p => p.MaBNhan).Distinct().Count(),
                                    Nu = kq.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Distinct().Count(),
                                    BH = kq.Where(p => p.SThe != null && p.SThe != "").Select(p => p.MaBNhan).Distinct().Count(),
                                    YHCT = kq.Where(p => p.TenCK == "Đông y" || p.TenCK == "YHCT-PHCN").Select(p => p.MaBNhan).Distinct().Count(),
                                    TE = kq.Where(p => p.Tuoi < 15).Select(p => p.MaBNhan).Distinct().Count(),
                                    TE6 = kq.Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Distinct().Count(),
                                    CC = kq.Where(p => p.CapCuu == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    VV = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    CV = kq.Where(p => p.Status == 1).Select(p => p.MaBNhan).Distinct().Count(),
                                    TSDTri = kq.Where(p => p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Distinct().Count(),
                                    SoNgay = kq.Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt)
                                }).ToList();
                    }
                    #endregion
                }
                #endregion
                #region lấy theo ngày khám bệnh
                else if (rdLoaiNgay.SelectedIndex == 3)
                {
                    var qBNKB0 = (from bnkb in data.BNKBs.Where(p => p.NgayKham <= denngay && p.NgayKham >= tungay)
                                  join bn in data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                                  join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                                  from kq1 in kq.DefaultIfEmpty()
                                  select new
                                  {
                                      bn.MaBNhan,
                                      bn.NoiTru,
                                      bn.DTNT,
                                      bn.GTinh,
                                      bn.SThe,
                                      bn.Tuoi,
                                      bn.CapCuu,
                                      bnkb.NgayKham,
                                      bnkb.MaKP,
                                      bnkb.MaCK,
                                      bnkb.PhuongAn,
                                      bnkb.IDKB,
                                      SoNgaydt = kq1 == null ? 0 : (kq1.SoNgaydt ?? 0),
                                      Status = kq1 == null ? 0 : (kq1.Status ?? 0),
                                      LyDo = kq1.LyDoC == null ? "" : kq1.LyDoC
                                  }).ToList();
                    var qBNKB = (from bn in qBNKB0
                                 join ck in lchuyenkhoa on bn.MaCK equals ck.MaCK into k
                                 from k1 in k.DefaultIfEmpty()
                                 select new
                                 {
                                     bn.MaBNhan,
                                     bn.NoiTru,
                                     bn.DTNT,
                                     bn.GTinh,
                                     bn.SThe,
                                     bn.Tuoi,
                                     bn.CapCuu,
                                     bn.NgayKham,
                                     bn.MaKP,
                                     bn.MaCK,
                                     bn.PhuongAn,
                                     TenCK = (k1 != null) ? k1.TenCKRG : "Khác",
                                     TenKP = k1 != null ? k1.TenCK : "Khác",
                                     bn.IDKB,
                                     SoNgaydt = bn.SoNgaydt,
                                     Status = bn.Status,
                                     LyDo = bn.LyDo
                                 }).ToList();

                    #region số lượt khám
                    if (rdMau.SelectedIndex == 0)
                        _lKB = (from kp in _lKhoaP
                                join bn in qBNKB on kp.MaKP equals bn.MaKP

                                group new { bn, kp } by new
                                {
                                    bn.TenCK,
                                    bn.TenKP,
                                } into kq
                                select new KB
                                {
                                    ChuyenKhoa = kq.Key.TenCK,
                                    kphong = kq.Key.TenKP,
                                    TS = kq.Count(),
                                    Nu = kq.Where(p => p.bn.GTinh == 0).Count(),
                                    BH = kq.Where(p => p.bn.SThe != null && p.bn.SThe != "").Count(),
                                    YHCT = kq.Where(p => p.bn.TenCK.Contains("Đông y") || p.bn.TenCK.Contains("YHCT-PHCN")).Count(),
                                    TE = kq.Where(p => p.bn.Tuoi < 15).Count(),
                                    TE6 = kq.Where(p => p.bn.Tuoi <= 6).Count(),
                                    CC = kq.Where(p => p.bn.CapCuu == 1).Count(),
                                    VV = kq.Where(p => p.bn.PhuongAn == 1 && p.bn.NoiTru == 1).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    CV = kq.Where(p => p.bn.PhuongAn == 2 && (p.bn.LyDo.Equals("Đủ điều kiện chuyển tuyến(đúng tuyến)") || p.bn.LyDo.Equals("Không đủ điều kiện chuyển tuyến/chuyển tuyến theo yêu cầu người bệnh...(vượt tuyến)"))).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    TSDTri = kq.Where(p => p.bn.PhuongAn == 1 && p.bn.NoiTru == 0 && p.bn.DTNT == true).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    SoNgay = kq.Where(p => p.bn.PhuongAn == 1 && p.bn.NoiTru == 0 && p.bn.DTNT == true).Sum(p => p.bn.SoNgaydt)
                                }).ToList();
                    #endregion
                    #region mẫu 2
                    else if (rdMau.SelectedIndex == 1)
                    {
                        _lKB = (from kp in _lKhoaP
                                join bn in qBNKB on kp.MaKP equals bn.MaKP
                                group new { bn, kp } by new
                                {
                                    bn.TenCK,
                                    bn.TenKP,
                                } into kq
                                select new KB
                                {
                                    ChuyenKhoa = kq.Key.TenCK,
                                    kphong = kq.Key.TenKP,
                                    TS = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    Nu = kq.Where(p => p.bn.GTinh == 0).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    BH = kq.Where(p => p.bn.SThe != null && p.bn.SThe != "").Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    YHCT = kq.Where(p => p.bn.TenCK == "Đông y" || p.bn.TenCK == "YHCT-PHCN").Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    TE = kq.Where(p => p.bn.Tuoi < 15).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    TE6 = kq.Where(p => p.bn.Tuoi <= 6).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    CC = kq.Where(p => p.bn.CapCuu == 1).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    VV = kq.Where(p => p.bn.PhuongAn == 1 && p.bn.NoiTru == 1).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    CV = kq.Where(p => p.bn.PhuongAn == 2 && (p.bn.LyDo.Equals("Đủ điều kiện chuyển tuyến(đúng tuyến)") || p.bn.LyDo.Equals("Không đủ điều kiện chuyển tuyến/chuyển tuyến theo yêu cầu người bệnh...(vượt tuyến)"))).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    TSDTri = kq.Where(p => p.bn.PhuongAn == 1 && p.bn.NoiTru == 0 && p.bn.DTNT == true).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    SoNgay = kq.Where(p => p.bn.PhuongAn == 1 && p.bn.NoiTru == 0 && p.bn.DTNT == true).Sum(p => p.bn.SoNgaydt)
                                }).ToList();
                    }
                    #endregion

                }
                #endregion
                #region lấy số hồ sơ và số ngày điều trị bệnh nhân mãn tính
                List<TTBNManTinh> _lmt = new List<TTBNManTinh>();

                //var ntbhyt = (from a in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay && p.NoiTru == 0 && p.DTNT == false && p.DTuong == "BHYT")
                //              join c in data.BNManTinhs on a.SThe equals c.STheSoCMT
                //              join b in data.BNKBs on a.MaBNhan equals b.MaBNhan
                //              select new TTBNManTinh { SoNgay = 1, MaBN = a.MaBNhan, MaCK = b.MaCK, MaKP = b.MaKP ?? 0 }).Distinct().ToList();
                //var ntdv = (from a in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay && p.NoiTru == 0 && p.DTNT == false && p.DTuong == "Dịch vụ")
                //            join b in data.TTboXungs on a.MaBNhan equals b.MaBNhan
                //            join c in data.BNManTinhs on b.CMT equals c.STheSoCMT
                //            join d in data.BNKBs on a.MaBNhan equals d.MaBNhan
                //            select new TTBNManTinh { SoNgay = 1, MaBN = a.MaBNhan, MaKP = d.MaKP ?? 0, MaCK = d.MaCK }).Distinct().ToList();

                var ntbhyt01 = (from a in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay && p.NoiTru == 0 && p.DTNT == false && p.DTuong == "BHYT")
                              join c in data.BNManTinhs on a.SThe equals c.STheSoCMT                             
                              select new TTBNManTinh { MaBN = a.MaBNhan }).Distinct().ToList();
                var ntbhy02t = (from a in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay && p.NoiTru == 0 && p.DTNT == false && p.DTuong == "BHYT")                             
                              join b in data.BNKBs on a.MaBNhan equals b.MaBNhan
                              select new TTBNManTinh { MaBN = a.MaBNhan, MaCK = b.MaCK, MaKP = b.MaKP ?? 0 }).Distinct().ToList();

                var ntbhyt = (from a in ntbhyt01                             
                              join b in ntbhy02t on a.MaBN equals b.MaBN
                              select new TTBNManTinh { SoNgay = 1, MaBN = a.MaBN, MaCK = b.MaCK, MaKP = b.MaKP}).ToList();

                var ntdv01 = (from a in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay && p.NoiTru == 0 && p.DTNT == false && p.DTuong == "Dịch vụ")
                            join b in data.TTboXungs on a.MaBNhan equals b.MaBNhan
                            join c in data.BNManTinhs on b.CMT equals c.STheSoCMT                          
                            select new TTBNManTinh {MaBN = a.MaBNhan }).Distinct().ToList();

                var ntdv02 = (from a in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay && p.NoiTru == 0 && p.DTNT == false && p.DTuong == "Dịch vụ")                           
                            join d in data.BNKBs on a.MaBNhan equals d.MaBNhan
                            select new TTBNManTinh {  MaBN = a.MaBNhan, MaKP = d.MaKP ?? 0, MaCK = d.MaCK }).Distinct().ToList();

                var ntdv = (from a in ntdv01
                            join b in ntdv02 on a.MaBN equals b.MaBN
                            select new TTBNManTinh { SoNgay = 1, MaBN = a.MaBN, MaKP = b.MaKP , MaCK = b.MaCK }).ToList();

                _lmt.AddRange(ntbhyt);
                _lmt.AddRange(ntdv);



                var concat = (from a in _lmt
                              join c in _lKhoaP on a.MaKP equals c.MaKP
                              join b in lchuyenkhoa on a.MaCK equals b.MaCK into k
                              from k1 in k.DefaultIfEmpty()
                              group new { a, k1, c } by new
                              {
                                  TenCK = (k1 != null) ? k1.TenCKRG : "Khác",
                                  TenKP = k1 != null ? k1.TenCK : "Khác",
                              } into kq
                              select new
                              {
                                  kq.Key.TenCK,
                                  kq.Key.TenKP,
                                  SoNgaydt = kq.Sum(p => p.a.SoNgay)
                              }).ToList();
                #endregion
                var ds = (from a in _lKB
                          join b in concat on new { kphong = a.kphong, ChuyenKhoa = a.ChuyenKhoa } equals new { kphong = b.TenKP, ChuyenKhoa = b.TenCK } into k
                          from k1 in k.DefaultIfEmpty()
                          select new
                          {
                              a.ChuyenKhoa,
                              a.kphong,
                              a.TS,
                              a.Nu,
                              a.BH,
                              a.YHCT,
                              a.TE,
                              a.TE6,
                              a.CC,
                              a.VV,
                              a.CV,
                              TSDTri = a.TSDTri + (k1 != null ? k1.SoNgaydt : 0),
                              SoNgay = a.SoNgay + (k1 != null ? k1.SoNgaydt : 0)
                          }).ToList();
                string _ntn = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;

                #region xuat Excel

                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                string[] _tieude = { "STT", "Khám chuyên khoa", "Tổng số lần khám", "Số lần nữ khám", "Số lần khám BHYT", "Số lần khám YHCT (kể cả kết hợp YHHÐ)", "Số lần TE <15 tuổi khám", "Số lần khám cấp cứu", "TỔng số người bệnh vào viện", "Số người bệnh chuyển viện tại phòng khám", "Số người bệnh điều trị ngoại trú", "Số ngày điều trị ngoại trí" };
                int[] _arrWidth = new int[] { };// { 5, 15, 10, 10, 10, 10, 10, 10, 10, 10};

                DungChung.Bien.MangHaiChieu = new Object[_lKB.Count + 18, 15];
                DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQCQ.ToUpper();
                DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.TenCQ.ToUpper();
                DungChung.Bien.MangHaiChieu[2, 2] = ("BÁO CÁO HOẠT ĐỘNG KHÁM CHỮA BỆNH").ToUpper();
                DungChung.Bien.MangHaiChieu[3, 2] = _ntn;

                DungChung.Bien.MangHaiChieu[_lKB.Count() + 5, 7] = "Ngày ...... tháng ..... năm .....";
                DungChung.Bien.MangHaiChieu[_lKB.Count() + 6, 1] = ("Người lập biểu").ToUpper();
                DungChung.Bien.MangHaiChieu[_lKB.Count() + 10, 1] = DungChung.Bien.NguoiLapBieu;
                DungChung.Bien.MangHaiChieu[_lKB.Count() + 6, 4] = ("TRƯỞNG PHÒNG KHTH").ToUpper();
                DungChung.Bien.MangHaiChieu[_lKB.Count() + 10, 4] = "";
                DungChung.Bien.MangHaiChieu[_lKB.Count() + 6, 7] = ("Giám đốc").ToUpper();
                DungChung.Bien.MangHaiChieu[_lKB.Count() + 10, 7] = DungChung.Bien.GiamDoc;
                for (int i = 0; i < _tieude.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[4, i] = _tieude[i];
                }
                int num = 5;
                foreach (var r in _lKB)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num - 4;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.ChuyenKhoa;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.TS;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.Nu;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.BH;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.YHCT;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.TE;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.CC;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.VV;
                    DungChung.Bien.MangHaiChieu[num, 9] = r.CV;
                    DungChung.Bien.MangHaiChieu[num, 10] = r.TSDTri;
                    DungChung.Bien.MangHaiChieu[num, 11] = r.SoNgay;
                    num++;

                }

                #endregion
                BaoCao.Rep_BcHoatDongKKB_TH01_30010 rep = new BaoCao.Rep_BcHoatDongKKB_TH01_30010();
                rep.celTitleKham.Text = rdMau.SelectedIndex == 0 ? "Số lần khám" : "Số người khám bệnh";
                rep.TG.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;
                rep.DataSource = ds.ToList();
                rep.BindingData();
                rep.CreateDocument();
                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", "C:\\BcHoatDongKKB.xls", true, this.Name);
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();


            }

        }
        public class TTBNManTinh
        {
            public int MaBN { get; set; }
            public int SoNgay { get; set; }
            public int MaKP { get; set; }
            public int MaCK { get; set; }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}