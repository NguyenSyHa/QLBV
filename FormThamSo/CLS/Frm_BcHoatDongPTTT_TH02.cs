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

    public partial class Frm_BcHoatDongPTTT_TH02 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcHoatDongPTTT_TH02()
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

        private class KPhong
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

        List<KPhong> _Kphong = new List<KPhong>();

        private void Frm_BcHoatDongPTTT_TT02_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV != "30009")
                radLoaiIn.SelectedIndex = 1;
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;

            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }
        #region class PTTT
        public class PTTT
        {
            private string loaihinh;

            public string LoaiHinh
            {
                get { return loaihinh; }
                set { loaihinh = value; }
            }

            private double ts;

            public double TS
            {
                get { return ts; }
                set { ts = value; }
            }
            double tKH;

            public double TKH
            {
                get { return tKH; }
                set { tKH = value; }
            }
            private double cc;

            public double CC
            {
                get { return cc; }
                set { cc = value; }
            }
            private double bhyt;

            public double BHYT
            {
                get { return bhyt; }
                set { bhyt = value; }
            }
            private double tp;

            public double TP
            {
                get { return tp; }
                set { tp = value; }
            }
            private double noitru;

            public double NoiTru
            {
                get { return noitru; }
                set { noitru = value; }
            }
            private double ngoaitru;

            public double NgoaiTru
            {
                get { return ngoaitru; }
                set { ngoaitru = value; }
            }
            private double tstb;

            public double TSTB
            {
                get { return tstb; }
                set { tstb = value; }
            }
            private double gmhs;

            public double GMHS
            {
                get { return gmhs; }
                set { gmhs = value; }
            }
            private double nk;

            public double NK
            {
                get { return nk; }
                set { nk = value; }
            }
            private double tbk;

            public double TBK
            {
                get { return tbk; }
                set { tbk = value; }
            }
            private double tstv;

            public double TSTV
            {
                get { return tstv; }
                set { tstv = value; }
            }
            private double tbpt;

            public double TBPT
            {
                get { return tbpt; }
                set { tbpt = value; }
            }
            private double gio24;

            public double Gio14
            {
                get { return gio24; }
                set { gio24 = value; }
            }
            private double loai;

            public double Loai
            {
                get { return loai; }
                set { loai = value; }
            }
            private string tenloai;

            public string Tenloai
            {
                get { return tenloai; }
                set { tenloai = value; }
            }
            private double PL;

            public double pl
            {
                get { return PL; }
                set { PL = value; }
            }
        }
        #endregion
        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            var kp = data.KPhongs.ToList();
            List<PTTT> _lPTTT = new List<PTTT>();

            if (KTtaoBc())
            {
                _lPTTT.Clear();
                var bv = data.BenhViens.ToList();
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                // frmIn frm = new frmIn();
                #region Hiển thị thời gian
                int nam = Convert.ToInt32(tungay.Year);
                int thang = Convert.ToInt32(tungay.Month);
                string _ntn = "";
                if (radIn.SelectedIndex == 0)
                { _ntn = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text; }
                if (radIn.SelectedIndex == 1)
                {
                    if (thang > 1 && thang <= 3) { _ntn = "Quý I năm " + nam; }
                    if (thang > 3 && thang <= 6) { _ntn = "Quý II năm " + nam; }
                    if (thang > 6 && thang <= 9) { _ntn = "Quý III năm " + nam; }
                    if (thang > 9 && thang <= 12) { _ntn = "Quý IV năm " + nam; }
                }
                if (radIn.SelectedIndex == 2)
                {
                    _ntn = ("(Báo cáo thống kê 06 tháng " + nam + ")").ToUpper();
                }
                if (radIn.SelectedIndex == 3)
                {
                    _ntn = ("(Báo cáo thống kê 09 tháng " + nam + ")").ToUpper();
                }
                if (radIn.SelectedIndex == 4)
                { _ntn = ("(Báo cáo năm " + nam + ")").ToUpper(); }

                #endregion
                List<KPhong> _lkp = new List<KPhong>();
                _lkp = _Kphong.Where(p => p.makp > 0).Where(p => p.chon == true).ToList();
                var dvu = (from dv in data.DichVus
                           join tnhom in data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat|| p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                           select new { dv.MaDV, dv.TenDV, tnhom.TenRG, dv.Loai }).ToList();
                var dthuoc = (from dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                              join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                              join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                              join rv in data.RaViens.Where(p => p.KetQua == "Tử vong") on bn.MaBNhan equals rv.MaBNhan into lefjoin
                              from lj in lefjoin.DefaultIfEmpty()
                              select new { dtct.SoLuong, dtct.MaKP, bn.CapCuu, bn.MaBNhan, dtct.MaDV, bn.IDDTBN, bn.NoiTru, bn.DTuong, tuvong = lj == null ? 0 : 1 }).ToList();
                List<int> bnhan = dthuoc.Select(p => p.MaBNhan).Distinct().ToList();
                List<int> _lstringMaBN = bnhan;
                // bệnh nhân đã thanh toán
                //var bntt = (from b in bnhan join vp in data.VienPhis on b equals vp.MaBNhan select b).ToList();
                var bnth = (from b in bnhan
                            join vp in data.VienPhis on b equals vp.MaBNhan into k
                            from k1 in k.DefaultIfEmpty()
                            select new { b, k1 }).ToList();
                if (cboTKBN.SelectedIndex == 1)//bn đã thanh toán
                {
                    //_lstringMaBN = bntt;
                    _lstringMaBN = bnth.Where(p => p.k1 != null).Select(p => p.b).ToList();
                }
                if (cboTKBN.SelectedIndex == 2) // bệnh nhân chưa thanh toán
                {
                    //_lstringMaBN = (from b in bnhan where !(from a in bntt select a).Contains(b) select b).ToList();
                    _lstringMaBN = bnth.Where(p => p.k1 == null).Select(p => p.b).ToList();
                }
                var qlh = (from ma in _lkp
                           join dtct in dthuoc on ma.makp equals dtct.MaKP
                           join dv in dvu on dtct.MaDV equals dv.MaDV
                           join bn in _lstringMaBN on dtct.MaBNhan equals bn
                           group new { dtct, dv, } by new { dv.TenRG, dv.Loai } into kq
                           select new
                           {
                               kq.Key.TenRG,
                               kq.Key.Loai,
                               TS = kq.Select(p => p.dtct.MaBNhan).Distinct().Count(),
                               CC = kq.Where(p => p.dtct.CapCuu == 1).Select(p=>p.dtct.MaBNhan).Distinct().Count(),
                               BHYT = kq.Where(p => p.dtct.DTuong == "BHYT").Select(p => p.dtct.MaBNhan).Distinct().Count(),
                               TP = kq.Where(p => p.dtct.DTuong == "Dịch vụ").Select(p => p.dtct.MaBNhan).Distinct().Count(),
                               NoiTru = kq.Where(p => p.dtct.NoiTru == 1).Select(p => p.dtct.MaBNhan).Distinct().Count(),
                               NgTru = kq.Where(p => p.dtct.NoiTru == 0).Select(p => p.dtct.MaBNhan).Distinct().Count(),
                               TSTV = kq.Where(p => p.dtct.tuvong == 1).Select(p => p.dtct.MaBNhan).Distinct().Count()
                           }).ToList();
                //var tuvong =( from b in bnhan
                //             join rv in data.RaViens.Where(p => p.KetQua == "Tử vong") on b equals rv.MaBNhan
                //             select new { rv.MaBNhan }).ToList();
                #region Lấy tên loai PTTT
                foreach (var a in qlh)
                {
                    PTTT them = new PTTT();
                    them.TS = a.TS;
                    them.TKH = a.TS - a.CC;
                    them.CC = a.CC;
                    them.BHYT = a.BHYT;
                    them.TP = a.TP;
                    them.NoiTru = a.NoiTru;
                    them.NgoaiTru = a.NgTru;
                    them.LoaiHinh = a.TenRG;
                    them.TSTV = a.TSTV;
                    if (a.Loai != null)
                    {
                        them.Loai = Convert.ToInt32(a.Loai);
                        int _loai = Convert.ToInt32(a.Loai);
                        switch (_loai)
                        {
                            case 1:
                                them.Tenloai = "Loại 1";
                                break;
                            case 11:
                                them.Tenloai = "Loại 1A";
                                break;
                            case 12:
                                them.Tenloai = "Loại 1B";
                                break;
                            case 13:
                                them.Tenloai = "Loại 1C";
                                break;
                            case 2:
                                them.Tenloai = "Loại 2";
                                break;
                            case 21:
                                them.Tenloai = "Loại 2A";
                                break;
                            case 22:
                                them.Tenloai = "Loại 2B";
                                break;
                            case 23:
                                them.Tenloai = "Loại 2C";
                                break;
                            case 3:
                                them.Tenloai = "Loại 3";
                                break;
                            case 31:
                                them.Tenloai = "Loại 3A";
                                break;
                            case 32:
                                them.Tenloai = "Loại 3B";
                                break;
                            case 33:
                                them.Tenloai = "Loại 3C";
                                break;
                            case 0:
                                them.Tenloai = "Loại đặc biệt";
                                break;
                        }
                        _loai++;
                    }
                    _lPTTT.Add(them);
                }

                #endregion

                List<PTTT> _lpt = new List<PTTT>();
                List<PTTT> _ltt = new List<PTTT>();
                _lpt = _lPTTT.Where(p => p.LoaiHinh.ToLower().Contains("phẫu thuật")).ToList();
                _ltt = _lPTTT.Where(p => p.LoaiHinh.ToLower().Contains("thủ thuật")).ToList();
                if (radLoaiIn.SelectedIndex == 0)//BV thanh hà
                {
                    #region xuat Excel
                    string[] _arr = new string[] { "0", "@", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                    string[] _tieude = { "STT", "Loại hình", "Tổng số", "TS theo kế hoạch", "TS cấp cứu", "TS BN có BHYT", "TS BN thu phí", "TS Nội trú", "TS ngoại trú", "TS tai biến", "TS tai biến có GMHS", "TS tai biến  - Nhiễm khuẩn", "TS tai biến khác", "Số tử vong", "Số tử trên bàn phẫu thuật", "Sổ tử vong trong 24 giờ" };
                    DungChung.Bien.MangHaiChieu = new Object[_lPTTT.Count + 20, 16];
                    DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQCQ.ToUpper();
                    DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.TenCQ.ToUpper();
                    DungChung.Bien.MangHaiChieu[2, 2] = ("BÁO CÁO HOẠT ĐỘNG PHẪU THUẬT, THỦ THUẬT").ToUpper();
                    DungChung.Bien.MangHaiChieu[3, 2] = _ntn;

                    DungChung.Bien.MangHaiChieu[_lPTTT.Count() + 7, 7] = "Ngày ...... tháng ..... năm .....";
                    DungChung.Bien.MangHaiChieu[_lPTTT.Count() + 8, 1] = ("Người lập biểu").ToUpper();
                    DungChung.Bien.MangHaiChieu[_lPTTT.Count() + 12, 1] = DungChung.Bien.NguoiLapBieu;
                    DungChung.Bien.MangHaiChieu[_lPTTT.Count() + 8, 4] = ("TRƯỞNG PHÒNG KHTH").ToUpper();
                    DungChung.Bien.MangHaiChieu[_lPTTT.Count() + 12, 4] = "";
                    DungChung.Bien.MangHaiChieu[_lPTTT.Count() + 8, 7] = ("Giám đốc").ToUpper();
                    DungChung.Bien.MangHaiChieu[_lPTTT.Count() + 12, 7] = DungChung.Bien.GiamDoc;
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[4, i] = _tieude[i];
                    }
                    int[] _arrWidth = new int[] { };// { 5, 15, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
                    DungChung.Bien.MangHaiChieu[5, 0] = "A.";
                    DungChung.Bien.MangHaiChieu[5, 1] = "Phẫu thuật";
                    int num = 6;
                    foreach (var r in _lpt.OrderBy(p => p.Tenloai))
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num - 5;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.Tenloai;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.TS;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.TKH;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.CC;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.BHYT;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.TP;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.NoiTru;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.NgoaiTru;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.TSTB;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.GMHS;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.NK;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.TBK;
                        DungChung.Bien.MangHaiChieu[num, 13] = r.TSTV;
                        DungChung.Bien.MangHaiChieu[num, 14] = "";
                        DungChung.Bien.MangHaiChieu[num, 15] = "";

                        num++;
                    }
                    DungChung.Bien.MangHaiChieu[num + _lpt.Count - 2, 0] = "B.";
                    DungChung.Bien.MangHaiChieu[num + _lpt.Count - 2, 1] = "Thủ thuật";
                    foreach (var r in _ltt.OrderBy(p => p.Tenloai))
                    {
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 0] = num - 5;
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 1] = r.Tenloai;
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 2] = r.TS;
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 3] = "";
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 4] = r.CC;
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 5] = r.BHYT;
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 6] = r.TP;
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 7] = r.NoiTru;
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 8] = r.NgoaiTru;
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 9] = r.TSTB;
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 10] = r.GMHS;
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 11] = r.NK;
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 12] = r.TBK;
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 13] = r.TSTV;
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 14] = "";
                        DungChung.Bien.MangHaiChieu[num + _lpt.Count - 1, 15] = "";

                        num++;
                    }
                    #endregion
                    BaoCao.Rep_BcHoatDongPTTT_TH02 rep = new BaoCao.Rep_BcHoatDongPTTT_TH02();
                    rep.DataSource = _lPTTT.OrderBy(p => p.Tenloai).ToList();
                    rep.TG.Value = _ntn;
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Hoạt động phẫu thuật thủ thuật", "C:\\BcHDPTTT.xls", true, this.Name);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else if (radLoaiIn.SelectedIndex == 1)//bv việt yên
                {
                    #region xuat Excel

                    string[] _arr = new string[] { "0", "@", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                    string[] _tieude = { "STT", "Loại hình", "Tổng số", "TS theo kế hoạch", "TS cấp cứu", "TS BN có BHYT", "TS BN thu phí", "TS Nội trú", "TS ngoại trú", "TS tai biến", "TS tai biến có GMHS", "TS tai biến  - Nhiễm khuẩn", "TS tai biến khác", "Số tử vong", "Số tử trên bàn phẫu thuật", "Sổ tử vong trong 24 giờ" };
                    DungChung.Bien.MangHaiChieu = new Object[_lPTTT.Count + 22, 16];
                    DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQCQ.ToUpper();
                    DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.TenCQ.ToUpper();
                    DungChung.Bien.MangHaiChieu[2, 2] = ("BÁO CÁO HOẠT ĐỘNG PHẪU THUẬT, THỦ THUẬT").ToUpper();
                    DungChung.Bien.MangHaiChieu[3, 2] = _ntn;

                    DungChung.Bien.MangHaiChieu[_lPTTT.Count() + 7, 7] = "Ngày ...... tháng ..... năm .....";
                    DungChung.Bien.MangHaiChieu[_lPTTT.Count() + 8, 1] = ("Người lập biểu").ToUpper();
                    DungChung.Bien.MangHaiChieu[_lPTTT.Count() + 12, 1] = DungChung.Bien.NguoiLapBieu;
                    DungChung.Bien.MangHaiChieu[_lPTTT.Count() + 8, 4] = ("TRƯỞNG PHÒNG KHTH").ToUpper();
                    DungChung.Bien.MangHaiChieu[_lPTTT.Count() + 12, 4] = "";
                    DungChung.Bien.MangHaiChieu[_lPTTT.Count() + 8, 7] = ("Giám đốc").ToUpper();
                    DungChung.Bien.MangHaiChieu[_lPTTT.Count() + 12, 7] = DungChung.Bien.GiamDoc;
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[4, i] = _tieude[i].ToUpper();
                    }
                    int[] _arrWidth = new int[] { };// { 5, 15, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
                    DungChung.Bien.MangHaiChieu[5, 0] = "A.";
                    DungChung.Bien.MangHaiChieu[5, 1] = "Phẫu thuật";
                    int num = 6;
                    foreach (var r in _lpt.OrderBy(p => p.Tenloai))
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num - 5;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.Tenloai;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.TS;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.TKH;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.CC;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.TSTB;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.GMHS;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.NK;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.TBK;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.TSTV;
                        DungChung.Bien.MangHaiChieu[num, 10] = "";
                        DungChung.Bien.MangHaiChieu[num, 11] = "";

                        num++;

                    }
                    //DungChung.Bien.MangHaiChieu[num + _lpt.Count - 2, 0] = "B.";
                    //DungChung.Bien.MangHaiChieu[num + _lpt.Count - 2, 1] = "Thủ thuật";
                    DungChung.Bien.MangHaiChieu[num , 0] = "B.";
                    DungChung.Bien.MangHaiChieu[num , 1] = "Thủ thuật";
                    num++;
                   
                    foreach (var r in _ltt.OrderBy(p => p.Tenloai))
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num - 6;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.Tenloai;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.TS;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.TKH;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.CC;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.TSTB;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.GMHS;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.NK;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.TBK;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.TSTV;
                        DungChung.Bien.MangHaiChieu[num, 10] = "";
                        DungChung.Bien.MangHaiChieu[num, 11] = "";

                        num++;

                    }

                    #endregion
                    BaoCao.Rep_BcHoatDongPTTT_24009 rep = new BaoCao.Rep_BcHoatDongPTTT_24009();
                    rep.DataSource = _lPTTT.OrderBy(p => p.Tenloai).ToList();
                    rep.TG.Value = _ntn;
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Hoạt động phẫu thuật thủ thuật", "C:\\BcHDPTTT.xls", true, this.Name);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
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

        private void xtraTabPage2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}