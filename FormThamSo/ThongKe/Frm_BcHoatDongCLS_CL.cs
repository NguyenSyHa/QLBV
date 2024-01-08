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

    public partial class Frm_BcHoatDongCLS_CL : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcHoatDongCLS_CL()
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;

            if (KTtaoBc())
            {
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                // frmIn frm = new frmIn();
                int _a = 0;
                if (chkHT.Checked == true)
                { _a = 2; }
                else
                { _a = 1; }
                BaoCao.Rep_BcHoatDongCLS_CL rep = new BaoCao.Rep_BcHoatDongCLS_CL(_a);
                #region Hiển thị thời gian
                int nam = Convert.ToInt32(tungay.Year);
                int thang = Convert.ToInt32(tungay.Month);
                if (radIn.SelectedIndex == 0)
                { rep.TuNgay.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text; }
                if (radIn.SelectedIndex == 1)
                {
                    if (thang > 1 && thang <= 3) { rep.TuNgay.Value = "Quý I năm " + nam; }
                    if (thang > 3 && thang <= 6) { rep.TuNgay.Value = "Quý II năm " + nam; }
                    if (thang > 6 && thang <= 9) { rep.TuNgay.Value = "Quý III năm " + nam; }
                    if (thang > 9 && thang <= 12) { rep.TuNgay.Value = "Quý IV năm " + nam; }
                }
                if (radIn.SelectedIndex == 2)
                {
                    rep.TuNgay.Value = ("(Báo cáo thống kê 06 tháng " + nam + ")").ToUpper();
                }
                if (radIn.SelectedIndex == 3)
                {
                    rep.TuNgay.Value = ("(Báo cáo thống kê 09 tháng " + nam + ")").ToUpper();
                }
                if (radIn.SelectedIndex == 4)
                { rep.TuNgay.Value = ("(Báo cáo năm " + nam + ")").ToUpper(); }
                #endregion
                List<KPhong> _lkp = new List<KPhong>();
                _lkp = _Kphong.Where(p => p.chon == true).ToList();
                _lkp.Add(new KPhong { makp = 0, tenkp = "" });
                var qdv = (from dv in data.DichVus
                           join nhom in data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                           join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                           select new { nhom.TenNhomCT, tnhom.TenRG, dv.TenDV, dv.MaDV ,dv.QCPC}).ToList();
                
                #region Tất cả BN thực hiện
                if (radTK.SelectedIndex == 0)
                {
                    var qdthuoc = (from dt in data.DThuocs
                                   join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on dt.IDDon equals dtct.IDDon
                                   join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                   select new { dt.MaBNhan, dt.MaKP, dtct.NgayNhap, dtct.SoLuong, bn.NoiTru, dtct.MaDV,dt.PLDV,dtct.Status }).ToList();
                    var q = (from dt in qdthuoc.Where(p => p.PLDV == 2 ? true : (p.Status == 1))
                             join dv in qdv on dt.MaDV equals dv.MaDV
                             select new { dt.MaBNhan, dv.TenNhomCT, dv.TenRG, dt.MaKP, dv.TenDV, dt.NgayNhap, dt.SoLuong, dt.NoiTru, dv.QCPC }).ToList();
                    var qts = (from ma in _lkp
                               join q1 in q on ma.makp equals q1.MaKP
                               group q1 by new { } into kq
                               select new
                               {
                                   TS1 = kq.Where(p => p.TenNhomCT == "Xét nghiệm").Sum(p => p.SoLuong),
                                   TS2 = kq.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Sum(p => p.SoLuong),
                                   TS3 = kq.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Sum(p => p.SoLuong),
                                   NuocTT = kq.Where(p => DungChung.Bien.MaBV == "30010"? p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo : p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Sum(p => p.SoLuong),
                                   NuocTNT = kq.Where(p => p.NoiTru == 1).Where(p => DungChung.Bien.MaBV == "30010" ? p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo : p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Sum(p => p.SoLuong),
                                   TS4 = kq.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac).Where(p => p.TenDV.Contains("Lậu") || p.TenDV.Contains("lậu") || p.TenDV.Contains("Lao") || p.TenDV.Contains("lao") || p.TenDV.Contains("Giang mai") || p.TenDV.Contains("giang mai") || p.TenDV.Contains("VGB")).Sum(p => p.SoLuong),
                                   TS5 = kq.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac).Where(p => p.TenDV.Contains("HIV")).Sum(p => p.SoLuong),

                                   TS7 = kq.Where(p => p.TenNhomCT.Equals("Chẩn đoán hình ảnh")).Where(p => !p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Sum(p => p.SoLuong),
                                   TS8 = kq.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.SoLuong),
                                   TS9 = kq.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Sum(p => p.SoLuong),
                                   TS10 = kq.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Where(p => p.TenDV.ToLower().Contains("chụp cắt lớp vi tính") || p.TenDV.ToLower().Contains("chụp clvt")).Sum(p => p.SoLuong),
                                   TS13 = kq.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Sum(p => p.SoLuong),
                                   TS14 = kq.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi)).Sum(p => p.SoLuong),
                                   TS15 = kq.Where(p => p.TenRG.Contains("Máu")).Sum(p => p.SoLuong),
                                   TS17 = kq.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong)).Sum(p => p.SoLuong),
                                   TS18 = kq.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap)).Sum(p => p.SoLuong),
                                   TSTMH = kq.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)).Sum(p => p.SoLuong),

                                   NT1 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenNhomCT == "Xét nghiệm").Sum(p => p.SoLuong),
                                   NT2 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Sum(p => p.SoLuong),
                                   NT3 = kq.Where(p => p.NoiTru == 1).Where(p => p.NoiTru == 1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Sum(p => p.SoLuong),
                                   NT4 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac).Where(p => p.TenDV.Contains("Lậu") || p.TenDV.Contains("lậu") || p.TenDV.Contains("Lao") || p.TenDV.Contains("lao") || p.TenDV.Contains("Giang mai") || p.TenDV.Contains("giang mai") || p.TenDV.Contains("VGB")).Sum(p => p.SoLuong),
                                   NT5 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac).Where(p => p.TenDV.Contains("HIV")).Sum(p => p.SoLuong),

                                   NT7 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenNhomCT.Equals("Chẩn đoán hình ảnh")).Where(p => !p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Sum(p => p.SoLuong),
                                   NT8 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.SoLuong),
                                   NT9 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Sum(p => p.SoLuong),
                                   NT10 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Where(p => p.TenDV.ToLower().Contains("chụp cắt lớp vi tính") || p.TenDV.ToLower().Contains("chụp clvt")).Sum(p => p.SoLuong),
                                   NT13 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Sum(p => p.SoLuong),
                                   NT14 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi)).Sum(p => p.SoLuong),
                                   NT15 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenDV.Contains("Truyền máu")).Sum(p => p.SoLuong),
                                   NT17 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong)).Sum(p => p.SoLuong),
                                   NT18 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap)).Sum(p => p.SoLuong),
                                   NTruTMH = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)).Sum(p => p.SoLuong)
                               }).ToList();
                    if (qts.Count() > 0)
                    {
                        rep.TS1.Value = qts.First().TS1;//tổng xét nghiệm
                        rep.TS2.Value = qts.First().TS2;//huyết học
                        rep.TS3.Value = qts.First().TS3;//hóa sinh
                        rep.TS4.Value = qts.First().TS4;//Vi khuẩn
                        rep.TS5.Value = qts.First().TS5;//HIV
                        rep.NuocTT.Value = qts.First().NuocTT;
                        rep.TS6.Value = qts.First().TS1 - qts.First().TS2 - qts.First().TS3 - qts.First().TS4 - qts.First().TS5 - qts.First().NuocTT;//Khac(xét nghiệm)
                        rep.TS7.Value = qts.First().TS7;// + qts.First().TS9 + qts.First().TS10;// - qts.First().TS13 - qts.First().TS14;//Chẩn đoán hình ảnh
                        rep.TS8.Value = qts.First().TS8 - qts.First().TS10;//X-quang
                        rep.TS9.Value = qts.First().TS9;//Siêu âm
                        rep.TS10.Value = qts.First().TS10;//CT-Scanner
                        rep.TS11.Value = qts.First().TS7 - qts.First().TS8 - qts.First().TS9;// -qts.First().TS10 - qts.First().TS13 - qts.First().TS14;//khác(chẩn đoán hình ảnh)
                        rep.TS12.Value = qts.First().TS13 + qts.First().TS14 + qts.First().TSTMH + qts.First().TS17 + qts.First().TS18;//thăm dò chức năng
                        rep.TS13.Value = qts.First().TS13;//điện tim
                        rep.TS14.Value = qts.First().TS14;//nội soi
                        rep.TS15.Value = qts.First().TS15;//truyền máu
                        rep.MauT.Value = qts.First().TS15;//số ml truyền máu
                        rep.TS17.Value = qts.First().TS17;//đo mật độ loãng xương
                        rep.TS18.Value = qts.First().TS18;//đo chức năng hô hấp
                        rep.TSTMH.Value = qts.First().TSTMH;//nội soi tai mũi họng

                        rep.NT1.Value = qts.First().NT1;
                        rep.NT2.Value = qts.First().NT2;
                        rep.NT3.Value = qts.First().NT3;
                        rep.NT4.Value = qts.First().NT4;
                        rep.NT5.Value = qts.First().NT5;
                        rep.NuocTNT.Value = qts.First().NuocTNT;
                        rep.NT6.Value = qts.First().NT1 - qts.First().NT2 - qts.First().NT3 - qts.First().NT4 - qts.First().NT5 - qts.First().NuocTNT;
                        rep.NT7.Value = qts.First().NT7;// - qts.First().NT13 - qts.First().NT14;
                        rep.NT8.Value = qts.First().NT8 - qts.First().NT10;
                        rep.NT9.Value = qts.First().NT9;
                        rep.NT10.Value = qts.First().NT10;
                        rep.NT11.Value = qts.First().NT7 - qts.First().NT8 - qts.First().NT9;// -qts.First().NT10 - qts.First().NT14;
                        rep.NT12.Value = qts.First().NT13 + qts.First().NT14 + qts.First().NTruTMH + qts.First().NT17 + qts.First().NT18;
                        rep.NT13.Value = qts.First().NT13;
                        rep.NT14.Value = qts.First().NT14;
                        rep.NT15.Value = qts.First().NT15;
                        rep.MauNT.Value = qts.First().NT15;
                        rep.NT17.Value = qts.First().NT17;
                        rep.NT18.Value = qts.First().NT18;
                        rep.NTruTMH.Value = qts.First().NTruTMH;
                    }
                    #region Lấy ra số lượng máu theo ml
                    var _DsDVMau = (from d in q.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) .Where(p => p.TenNhomCT == "Máu và chế phẩm của máu").Where(p => p.QCPC != null && p.QCPC.Trim() != "")
                                    join kp in _lkp on d.MaKP equals kp.makp
                                    group d by new { d.TenNhomCT, d.TenDV, d.QCPC,d.NoiTru } into kq
                                    select new
                                    {
                                        kq.Key.TenDV,
                                        kq.Key.QCPC,
                                        Sluong = kq.Sum(p => p.SoLuong),
                                        kq.Key.NoiTru
                                    }).ToList();
                    List<DSMau> DSmau = new List<DSMau>();
                    foreach (var item in _DsDVMau)
                    {
                        string[] _qcpc = item.QCPC.Split('/');
                        double a = Convert.ToDouble(_qcpc[1].ToString());
                        DSMau moi = new DSMau();
                        moi.tendv = item.TenDV;
                        moi.soluong = item.Sluong * a;
                        moi.noitru=item.NoiTru;
                        DSmau.Add(moi);
                    }
                    if(DSmau.Count()>0)
                    {
                        rep.MauT.Value = DSmau.Sum(p => p.soluong);
                        rep.MauNT.Value = DSmau.Where(p => p.noitru == 1).Sum(p => p.soluong);
                    }
                    #endregion
                    #region xuat Excel

                    string[] _arr = new string[] { "0", "@", "@", "0", "0" };
                    int num = 1;
                    string[] _tieude = { "STT", "Các xét nghiệm", "Đơn vị", "Tổng số", "Trong đó nội trú" };
                    DungChung.Bien.MangHaiChieu = new Object[20, 5];
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }
                    int[] _arrWidth = new int[] { 5, 20, 10, 10, 10 };

                    // int num = 0;
                    {
                        if (qts.Count() > 0)
                        {
                            DungChung.Bien.MangHaiChieu[1, 0] = "A";
                            DungChung.Bien.MangHaiChieu[2, 0] = "1";
                            DungChung.Bien.MangHaiChieu[3, 0] = "2";
                            DungChung.Bien.MangHaiChieu[4, 0] = "3";
                            DungChung.Bien.MangHaiChieu[5, 0] = "4";
                            DungChung.Bien.MangHaiChieu[6, 0] = "5";
                            DungChung.Bien.MangHaiChieu[7, 0] = "B";
                            DungChung.Bien.MangHaiChieu[8, 0] = "1";
                            DungChung.Bien.MangHaiChieu[9, 0] = "2";
                            DungChung.Bien.MangHaiChieu[10, 0] = "3";
                            DungChung.Bien.MangHaiChieu[11, 0] = "4";
                            DungChung.Bien.MangHaiChieu[12, 0] = "C";
                            DungChung.Bien.MangHaiChieu[13, 0] = "1";
                            DungChung.Bien.MangHaiChieu[14, 0] = "2";
                            DungChung.Bien.MangHaiChieu[15, 0] = "3";
                            DungChung.Bien.MangHaiChieu[16, 0] = "4";
                            DungChung.Bien.MangHaiChieu[17, 0] = "5";
                            DungChung.Bien.MangHaiChieu[18, 0] = "D";
                            DungChung.Bien.MangHaiChieu[19, 0] = "1";
                            DungChung.Bien.MangHaiChieu[1, 1] = "I. Các xét nghiệm";
                            DungChung.Bien.MangHaiChieu[2, 1] = "-Huyết học";
                            DungChung.Bien.MangHaiChieu[3, 1] = "-Hóa sinh";
                            DungChung.Bien.MangHaiChieu[4, 1] = "-Vi khuẩn";
                            DungChung.Bien.MangHaiChieu[5, 1] = "-HIV";
                            DungChung.Bien.MangHaiChieu[6, 1] = "-Khác";
                            DungChung.Bien.MangHaiChieu[7, 1] = "-II. Chẩn đoán hình ảnh";
                            DungChung.Bien.MangHaiChieu[8, 1] = "-Số lần chụp X-Quang";
                            DungChung.Bien.MangHaiChieu[9, 1] = "-Siêu âm";
                            DungChung.Bien.MangHaiChieu[10, 1] = "-CT-Scanner";
                            DungChung.Bien.MangHaiChieu[11, 1] = "-Khác";
                            DungChung.Bien.MangHaiChieu[12, 1] = "III. Thăm dò chức năng";
                            DungChung.Bien.MangHaiChieu[13, 1] = "-Điện tim";
                            DungChung.Bien.MangHaiChieu[14, 1] = "-Nội soi";
                            DungChung.Bien.MangHaiChieu[15, 1] = "-Nội soi tai mũi họng";
                            DungChung.Bien.MangHaiChieu[16, 1] = "-Đo mật độ loãng xương";
                            DungChung.Bien.MangHaiChieu[17, 1] = "-Đo chức năng hô hấp";
                            DungChung.Bien.MangHaiChieu[18, 1] = "IV. Truyền máu";
                            DungChung.Bien.MangHaiChieu[19, 1] = "-Số ml máu truyền";
                            DungChung.Bien.MangHaiChieu[1, 2] = "";
                            DungChung.Bien.MangHaiChieu[2, 2] = "Tiêu bản";
                            DungChung.Bien.MangHaiChieu[3, 2] = "Tiêu bản";
                            DungChung.Bien.MangHaiChieu[4, 2] = "Tiêu bản";
                            DungChung.Bien.MangHaiChieu[5, 2] = "Tiêu bản";
                            DungChung.Bien.MangHaiChieu[6, 2] = "Tiêu bản";
                            DungChung.Bien.MangHaiChieu[7, 2] = "";
                            DungChung.Bien.MangHaiChieu[8, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[9, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[10, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[11, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[12, 2] = "";
                            DungChung.Bien.MangHaiChieu[13, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[14, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[15, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[16, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[17, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[18, 2] = "";
                            DungChung.Bien.MangHaiChieu[19, 2] = "ml";
                            DungChung.Bien.MangHaiChieu[1, 3] = qts.First().TS1;
                            DungChung.Bien.MangHaiChieu[2, 3] = qts.First().TS2;
                            DungChung.Bien.MangHaiChieu[3, 3] = qts.First().TS3;
                            DungChung.Bien.MangHaiChieu[4, 3] = qts.First().TS4;
                            DungChung.Bien.MangHaiChieu[5, 3] = qts.First().TS5;
                            DungChung.Bien.MangHaiChieu[6, 3] = qts.First().TS1 - qts.First().TS2 - qts.First().TS3 - qts.First().TS4 - qts.First().TS5;
                            DungChung.Bien.MangHaiChieu[7, 3] = qts.First().TS7;// -qts.First().TS13 - qts.First().TS14;
                            DungChung.Bien.MangHaiChieu[8, 3] = qts.First().TS8 - qts.First().TS10;
                            DungChung.Bien.MangHaiChieu[9, 3] = qts.First().TS9;
                            DungChung.Bien.MangHaiChieu[10, 3] = qts.First().TS10;
                            DungChung.Bien.MangHaiChieu[11, 3] = qts.First().TS7 - qts.First().TS8 - qts.First().TS9;// -qts.First().TS10 - qts.First().TS14;
                            DungChung.Bien.MangHaiChieu[12, 3] = qts.First().TS13 + qts.First().TS14 + qts.First().TSTMH + qts.First().TS17 + qts.First().TS18;
                            DungChung.Bien.MangHaiChieu[13, 3] = qts.First().TS13;
                            DungChung.Bien.MangHaiChieu[14, 3] = qts.First().TS14;
                            DungChung.Bien.MangHaiChieu[15, 3] = qts.First().TSTMH;
                            DungChung.Bien.MangHaiChieu[16, 3] = qts.First().TS17;
                            DungChung.Bien.MangHaiChieu[17, 3] = qts.First().TS18;
                            DungChung.Bien.MangHaiChieu[18, 3] = qts.First().TS15;
                            DungChung.Bien.MangHaiChieu[19, 3] = qts.First().TS15;
                            DungChung.Bien.MangHaiChieu[1, 4] = qts.First().NT1;
                            DungChung.Bien.MangHaiChieu[2, 4] = qts.First().NT2;
                            DungChung.Bien.MangHaiChieu[3, 4] = qts.First().NT3;
                            DungChung.Bien.MangHaiChieu[4, 4] = qts.First().NT4;
                            DungChung.Bien.MangHaiChieu[5, 4] = qts.First().NT5;
                            DungChung.Bien.MangHaiChieu[6, 4] = qts.First().NT1 - qts.First().NT2 - qts.First().NT3 - qts.First().NT4 - qts.First().NT5;
                            DungChung.Bien.MangHaiChieu[7, 4] = qts.First().NT7;// -qts.First().NT13 - qts.First().NT14;
                            DungChung.Bien.MangHaiChieu[8, 4] = qts.First().NT8 - qts.First().NT10;
                            DungChung.Bien.MangHaiChieu[9, 4] = qts.First().NT9;
                            DungChung.Bien.MangHaiChieu[10, 4] = qts.First().NT10;
                            DungChung.Bien.MangHaiChieu[11, 4] = qts.First().NT7 - qts.First().NT8 - qts.First().NT9;// -qts.First().NT10 - qts.First().NT14;
                            DungChung.Bien.MangHaiChieu[12, 4] = qts.First().NT13 + qts.First().NT14 + qts.First().NTruTMH + qts.First().NT17 + qts.First().NT18;
                            DungChung.Bien.MangHaiChieu[13, 4] = qts.First().NT13;
                            DungChung.Bien.MangHaiChieu[14, 4] = qts.First().NT14;
                            DungChung.Bien.MangHaiChieu[15, 4] = qts.First().NTruTMH;
                            DungChung.Bien.MangHaiChieu[16, 4] = qts.First().NT17;
                            DungChung.Bien.MangHaiChieu[17, 4] = qts.First().NT18;
                            DungChung.Bien.MangHaiChieu[18, 4] = qts.First().NT15;
                            DungChung.Bien.MangHaiChieu[19, 4] = qts.First().NT15;
                        }
                    }
                    //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo hoạt động khám bệnh", "C:\\BcHDKB.xls", true);
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo hoạt động khám bệnh", "C:\\BcHDKB.xls", false, this.Name);
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    #endregion
                }
                #endregion
                #region Chỉ BN đã TT
                if (radTK.SelectedIndex == 1)
                {
                    var qdthuoc = (from dt in data.DThuocs
                                   join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                                   join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                   select new { dt.MaBNhan, dt.MaKP, dtct.NgayNhap, dtct.SoLuong, bn.NoiTru, dtct.MaDV,dt.PLDV,dtct.Status }).ToList();
                    var q = (from dt in qdthuoc.Where(p => p.PLDV == 2 ? true : (p.Status == 1))
                             join dv in qdv on dt.MaDV equals dv.MaDV
                             select new { dt.MaBNhan, dv.TenNhomCT, dv.TenRG, dt.MaKP, dv.TenDV, dt.NgayNhap, dt.SoLuong, dt.NoiTru, dv.QCPC }).ToList();
                    var qdt = (from a in q
                               join vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay) on a.MaBNhan equals vp.MaBNhan
                               select new { a.TenNhomCT, a.TenRG, a.MaKP, a.NoiTru, a.TenDV, a.SoLuong, vp.NgayTT,a.QCPC }).ToList();
                    var qts = (from ma in _lkp
                               join p in qdt on ma.makp equals p.MaKP
                               group p by new { } into kq
                               select new
                               {
                                   TS1 = kq.Where(p => p.TenNhomCT == "Xét nghiệm").Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.TenNhomCT == "Xét nghiệm").Sum(p => p.SoLuong),
                                   TS2 = kq.Where(p => p.TenRG == "XN huyết học").Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.TenRG == "XN huyết học").Sum(p => p.SoLuong),
                                   TS3 = kq.Where(p => p.TenRG == "XN hóa sinh máu").Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.TenRG == "XN hóa sinh máu").Sum(p => p.SoLuong),
                                   NuocTT = kq.Where(p => p.TenRG == (DungChung.Bien.MaBV == "30010" ? DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo : DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)).Sum(p => p.SoLuong),
                                   NuocTNT = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == (DungChung.Bien.MaBV == "30010" ? DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo : DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)).Sum(p => p.SoLuong),
                                   TS4 = kq.Where(p => p.TenRG == "XN khác").Where(p => p.TenDV.Contains("Lậu") || p.TenDV.Contains("lậu") || p.TenDV.Contains("Lao") || p.TenDV.Contains("lao") || p.TenDV.Contains("Giang mai") || p.TenDV.Contains("giang mai") || p.TenDV.Contains("VGB")).Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.TenRG == "XN khác").Where(p => p.TenDV.Contains("Lậu") || p.TenDV.Contains("lậu") || p.TenDV.Contains("Lao") || p.TenDV.Contains("lao") || p.TenDV.Contains("Giang mai") || p.TenDV.Contains("giang mai") || p.TenDV.Contains("VGB")).Sum(p => p.SoLuong),
                                   TS5 = kq.Where(p => p.TenRG == "XN khác").Where(p => p.TenDV.Contains("HIV")).Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.TenRG == "XN khác").Where(p => p.TenDV.Contains("HIV")).Sum(p => p.SoLuong),

                                   TS7 = kq.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh").Where(p => !p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh").Where(p => !p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Sum(p => p.SoLuong),
                                   TS8 = kq.Where(p => p.TenRG == "X-Quang").Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.TenRG == "X-Quang").Sum(p => p.SoLuong),
                                   TS9 = kq.Where(p => p.TenRG.Contains("Siêu âm")).Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.TenRG.Contains("Siêu âm")).Sum(p => p.SoLuong),
                                   TS10 = kq.Where(p => p.TenDV.Contains("canner")).Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.TenDV.Contains("canner")).Sum(p => p.SoLuong),
                                   TS13 = kq.Where(p => p.TenRG.Contains("Điện tim")).Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.TenRG.Contains("Điện tim")).Sum(p => p.SoLuong),
                                   TS14 = kq.Where(p => p.TenRG.Contains("Nội soi")).Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.TenRG.Contains("Nội soi")).Sum(p => p.SoLuong),
                                   TS15 = kq.Where(p => p.TenDV == "Truyền máu").Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.TenDV == "Truyền máu").Sum(p => p.SoLuong),

                                   NT1 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenNhomCT == "Xét nghiệm").Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.NoiTru == 1).Where(p => p.TenNhomCT == "Xét nghiệm").Sum(p => p.SoLuong),
                                   NT2 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == "XN huyết học").Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == "XN huyết học").Sum(p => p.SoLuong),
                                   NT3 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == "XN hóa sinh máu").Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.NoiTru == 1).Where(p => p.NoiTru == 1).Where(p => p.TenRG == "XN hóa sinh máu").Sum(p => p.SoLuong),
                                   NT4 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == "XN khác").Where(p => p.TenDV.Contains("Lậu") || p.TenDV.Contains("lậu") || p.TenDV.Contains("Lao") || p.TenDV.Contains("lao") || p.TenDV.Contains("Giang mai") || p.TenDV.Contains("giang mai") || p.TenDV.Contains("VGB")).Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == "XN khác").Where(p => p.TenDV.Contains("Lậu") || p.TenDV.Contains("lậu") || p.TenDV.Contains("Lao") || p.TenDV.Contains("lao") || p.TenDV.Contains("Giang mai") || p.TenDV.Contains("giang mai") || p.TenDV.Contains("VGB")).Sum(p => p.SoLuong),
                                   NT5 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == "XN khác").Where(p => p.TenDV.Contains("HIV")).Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == "XN khác").Where(p => p.TenDV.Contains("HIV")).Sum(p => p.SoLuong),

                                   NT7 = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh").Where(p => !p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Where(p => p.NoiTru == 1).Sum(p => p.SoLuong),
                                   NT8 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == "X-Quang").Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG == "X-Quang").Sum(p => p.SoLuong),
                                   NT9 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG.Contains("Siêu âm")).Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG.Contains("Siêu âm")).Sum(p => p.SoLuong),
                                   NT10 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenDV.Contains("canner")).Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.NoiTru == 1).Where(p => p.TenDV.Contains("canner")).Sum(p => p.SoLuong),
                                   NT13 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG.Contains("Điện tim")).Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG.Contains("Điện tim")).Sum(p => p.SoLuong),
                                   NT14 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG.Contains("Nội soi")).Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.NoiTru == 1).Where(p => p.TenRG.Contains("Nội soi")).Sum(p => p.SoLuong),
                                   NT15 = kq.Where(p => p.NoiTru == 1).Where(p => p.TenDV == "Truyền máu").Sum(p => p.SoLuong) == null ? 0 : kq.Where(p => p.NoiTru == 1).Where(p => p.TenDV == "Truyền máu").Sum(p => p.SoLuong),

                               }).ToList();
                    if (qts.Count() > 0)
                    {
                        rep.TS1.Value = qts.First().TS1;
                        rep.TS2.Value = qts.First().TS2;
                        rep.TS3.Value = qts.First().TS3;
                        rep.TS4.Value = qts.First().TS4;
                        rep.TS5.Value = qts.First().TS5;
                        rep.NuocTT.Value = qts.First().NuocTT;
                        rep.TS6.Value = qts.First().TS1 - qts.First().TS2 - qts.First().TS3 - qts.First().TS4 - qts.First().TS5 - qts.First().NuocTT;
                        rep.TS7.Value = qts.First().TS7 - qts.First().TS13 - qts.First().TS14;
                        rep.TS8.Value = qts.First().TS8;
                        rep.TS9.Value = qts.First().TS9;
                        rep.TS10.Value = qts.First().TS10;
                        rep.TS11.Value = qts.First().TS7 - qts.First().TS8 - qts.First().TS9;
                        rep.TS12.Value = qts.First().TS13 + qts.First().TS14;
                        rep.TS13.Value = qts.First().TS13;
                        rep.TS14.Value = qts.First().TS14;
                        rep.TS15.Value = qts.First().TS15;
                        rep.MauT.Value = qts.First().TS15;

                        rep.NT1.Value = qts.First().NT1;
                        rep.NT2.Value = qts.First().NT2;
                        rep.NT3.Value = qts.First().NT3;
                        rep.NT4.Value = qts.First().NT4;
                        rep.NT5.Value = qts.First().NT5;
                        rep.NuocTNT.Value = qts.First().NuocTNT;
                        rep.NT6.Value = qts.First().NT1 - qts.First().NT2 - qts.First().NT3 - qts.First().NT4 - qts.First().NT5 - qts.First().NuocTNT;
                        rep.NT7.Value = qts.First().NT7 - qts.First().NT13 - qts.First().NT14;
                        rep.NT8.Value = qts.First().NT8;
                        rep.NT9.Value = qts.First().NT9;
                        rep.NT10.Value = qts.First().NT10;
                        rep.NT11.Value = qts.First().NT7 - qts.First().NT8 - qts.First().NT9 - qts.First().NT10 - qts.First().NT13 - qts.First().NT14;
                        rep.NT12.Value = qts.First().NT13 + qts.First().NT14;
                        rep.NT13.Value = qts.First().NT13;
                        rep.NT14.Value = qts.First().NT14;
                        rep.NT15.Value = qts.First().NT15;
                        rep.MauNT.Value = qts.First().NT15;

                    }
                    #region Lấy ra số lượng máu theo ml
                    var _DsDVMau = (from d in qdt.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).Where(p => p.TenNhomCT == "Máu và chế phẩm của máu").Where(p => p.QCPC != null && p.QCPC.Trim() != "")
                                    join kp in _lkp on d.MaKP equals kp.makp
                                    group d by new { d.TenNhomCT, d.TenDV, d.QCPC, d.NoiTru } into kq
                                    select new
                                    {
                                        kq.Key.TenDV,
                                        kq.Key.QCPC,
                                        Sluong = kq.Sum(p => p.SoLuong),
                                        kq.Key.NoiTru
                                    }).ToList();
                    List<DSMau> DSmau = new List<DSMau>();
                    foreach (var item in _DsDVMau)
                    {
                        string[] _qcpc = item.QCPC.Trim().Split('/');
                        double a = Convert.ToDouble(_qcpc[1].ToString());
                        DSMau moi = new DSMau();
                        moi.tendv = item.TenDV;
                        moi.soluong = item.Sluong * a;
                        moi.noitru = item.NoiTru;
                        DSmau.Add(moi);
                    }
                    if (DSmau.Count() > 0)
                    {
                        rep.MauT.Value = DSmau.Sum(p => p.soluong);
                        rep.MauNT.Value = DSmau.Where(p => p.noitru == 1).Sum(p => p.soluong);
                    }
                    #endregion
                    #region xuat Excel
                    DungChung.Bien.MangHaiChieu = new Object[17, 5];
                    string[] _arr = new string[] { "0", "@", "@", "0", "0" };
                    string[] _tieude = { "STT", "Các xét nghiệm", "Đơn vị", "Tổng số", "Trong đó nội trú" };
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }
                    int[] _arrWidth = new int[] { 5, 20, 10, 10, 10 };

                    // int num = 0;
                    {
                        if (qts.Count > 0)
                        {
                            DungChung.Bien.MangHaiChieu[1, 0] = "A";
                            DungChung.Bien.MangHaiChieu[2, 0] = "1";
                            DungChung.Bien.MangHaiChieu[3, 0] = "2";
                            DungChung.Bien.MangHaiChieu[4, 0] = "3";
                            DungChung.Bien.MangHaiChieu[5, 0] = "4";
                            DungChung.Bien.MangHaiChieu[6, 0] = "5";
                            DungChung.Bien.MangHaiChieu[7, 0] = "B";
                            DungChung.Bien.MangHaiChieu[8, 0] = "1";
                            DungChung.Bien.MangHaiChieu[9, 0] = "2";
                            DungChung.Bien.MangHaiChieu[10, 0] = "3";
                            DungChung.Bien.MangHaiChieu[11, 0] = "4";
                            DungChung.Bien.MangHaiChieu[12, 0] = "C";
                            DungChung.Bien.MangHaiChieu[13, 0] = "1";
                            DungChung.Bien.MangHaiChieu[14, 0] = "2";
                            DungChung.Bien.MangHaiChieu[15, 0] = "D";
                            DungChung.Bien.MangHaiChieu[16, 0] = "1";
                            DungChung.Bien.MangHaiChieu[1, 1] = "I. Các xét nghiệm";
                            DungChung.Bien.MangHaiChieu[2, 1] = "-Huyết học";
                            DungChung.Bien.MangHaiChieu[3, 1] = "-Hóa sinh";
                            DungChung.Bien.MangHaiChieu[4, 1] = "-Vi khuẩn";
                            DungChung.Bien.MangHaiChieu[5, 1] = "-HIV";
                            DungChung.Bien.MangHaiChieu[6, 1] = "-Khác";
                            DungChung.Bien.MangHaiChieu[7, 1] = "-II. Chẩn đoán hình ảnh";
                            DungChung.Bien.MangHaiChieu[8, 1] = "-Số lần chụp X-Quang";
                            DungChung.Bien.MangHaiChieu[9, 1] = "-Siêu âm";
                            DungChung.Bien.MangHaiChieu[10, 1] = "-CT-Scanner";
                            DungChung.Bien.MangHaiChieu[11, 1] = "-Khác";
                            DungChung.Bien.MangHaiChieu[12, 1] = "III. Thăm dò chức năng";
                            DungChung.Bien.MangHaiChieu[13, 1] = "-Điện tim";
                            DungChung.Bien.MangHaiChieu[14, 1] = "-Nội soi";
                            DungChung.Bien.MangHaiChieu[15, 1] = "IV. Truyền máu";
                            DungChung.Bien.MangHaiChieu[16, 1] = "-Số ml máu truyền";
                            DungChung.Bien.MangHaiChieu[1, 2] = "";
                            DungChung.Bien.MangHaiChieu[2, 2] = "Tiêu bản";
                            DungChung.Bien.MangHaiChieu[3, 2] = "Tiêu bản";
                            DungChung.Bien.MangHaiChieu[4, 2] = "Tiêu bản";
                            DungChung.Bien.MangHaiChieu[5, 2] = "Tiêu bản";
                            DungChung.Bien.MangHaiChieu[6, 2] = "Tiêu bản";
                            DungChung.Bien.MangHaiChieu[7, 2] = "";
                            DungChung.Bien.MangHaiChieu[8, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[9, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[10, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[11, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[12, 2] = "";
                            DungChung.Bien.MangHaiChieu[13, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[14, 2] = "Lần";
                            DungChung.Bien.MangHaiChieu[15, 2] = "";
                            DungChung.Bien.MangHaiChieu[16, 2] = "ml";
                            DungChung.Bien.MangHaiChieu[1, 3] = qts.First().TS1;
                            DungChung.Bien.MangHaiChieu[2, 3] = qts.First().TS2;
                            DungChung.Bien.MangHaiChieu[3, 3] = qts.First().TS3;
                            DungChung.Bien.MangHaiChieu[4, 3] = qts.First().TS4;
                            DungChung.Bien.MangHaiChieu[5, 3] = qts.First().TS5;
                            DungChung.Bien.MangHaiChieu[6, 3] = qts.First().TS1 - qts.First().TS2 - qts.First().TS3 - qts.First().TS4 - qts.First().TS5;
                            DungChung.Bien.MangHaiChieu[7, 3] = qts.First().TS7 - qts.First().TS13 - qts.First().TS14;
                            DungChung.Bien.MangHaiChieu[8, 3] = qts.First().TS8 - qts.First().TS10;
                            DungChung.Bien.MangHaiChieu[9, 3] = qts.First().TS9;
                            DungChung.Bien.MangHaiChieu[10, 3] = qts.First().TS10;
                            DungChung.Bien.MangHaiChieu[11, 3] = qts.First().TS7 - qts.First().TS8 - qts.First().TS9 - qts.First().TS13 - qts.First().TS14;
                            DungChung.Bien.MangHaiChieu[12, 3] = qts.First().TS13 + qts.First().TS14;
                            DungChung.Bien.MangHaiChieu[13, 3] = qts.First().TS13;
                            DungChung.Bien.MangHaiChieu[14, 3] = qts.First().TS14;
                            DungChung.Bien.MangHaiChieu[15, 3] = qts.First().TS15;
                            DungChung.Bien.MangHaiChieu[16, 3] = qts.First().TS15;
                            DungChung.Bien.MangHaiChieu[1, 4] = qts.First().NT1;
                            DungChung.Bien.MangHaiChieu[2, 4] = qts.First().NT2;
                            DungChung.Bien.MangHaiChieu[3, 4] = qts.First().NT3;
                            DungChung.Bien.MangHaiChieu[4, 4] = qts.First().NT4;
                            DungChung.Bien.MangHaiChieu[5, 4] = qts.First().NT5;
                            DungChung.Bien.MangHaiChieu[6, 4] = qts.First().NT1 - qts.First().NT2 - qts.First().NT3 - qts.First().NT4 - qts.First().NT5;
                            DungChung.Bien.MangHaiChieu[7, 4] = qts.First().NT7 - qts.First().NT13 - qts.First().NT14;
                            DungChung.Bien.MangHaiChieu[8, 4] = qts.First().NT8 - qts.First().NT10;
                            DungChung.Bien.MangHaiChieu[9, 4] = qts.First().NT9;
                            DungChung.Bien.MangHaiChieu[10, 4] = qts.First().NT10;
                            DungChung.Bien.MangHaiChieu[11, 4] = qts.First().NT7 - qts.First().NT8 - qts.First().NT9 - qts.First().NT13 - qts.First().NT14;
                            DungChung.Bien.MangHaiChieu[12, 4] = qts.First().NT13 + qts.First().NT14;
                            DungChung.Bien.MangHaiChieu[13, 4] = qts.First().NT13;
                            DungChung.Bien.MangHaiChieu[14, 4] = qts.First().NT14;
                            DungChung.Bien.MangHaiChieu[15, 4] = qts.First().NT15;
                            DungChung.Bien.MangHaiChieu[16, 4] = qts.First().NT15;
                        }
                    }
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo hoạt động khám bệnh", "C:\\BcHDKB.xls", false, this.Name);
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    #endregion

                #endregion

                }
            }
        }
        private class DSMau
        {
            private string TenDV;
            private double Sluong;
            private int? NoiTru;
            public string tendv
            { set { TenDV = value; } get { return TenDV; } }
            public double soluong
            { set { Sluong = value; } get { return Sluong; } }
            public int? noitru
            { set { NoiTru = value; } get { return NoiTru; } }
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