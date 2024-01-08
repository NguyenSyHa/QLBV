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
    public partial class Frm_ThTTNhanTuyen : DevExpress.XtraEditors.XtraForm
    {
        public Frm_ThTTNhanTuyen()
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
        private void Frm_ThTTNhanTuyen_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            _Kphong.Clear();
            var kphong = (from kp in data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = false;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = false;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }

        public class nhantuyen
        {
            public int mabn;
            public string tenbn;
            public int tuoi;
            public string doituong;
            public string sthe;
            public string makp;
            public string chandoan;
            public string mabv;
            public string s1a;
            public string s1b;
            public string s2;
            public string s3;
            public string s4;
            public string s5;
            private string s6;
            private string s7;
            private string s8;
            private string s9;
            public string cothe;
            public string nam;
            public string nu;
            public string ketqua;
            public string cdnoigt;
            private string tenbv;
            private int gtinh;
            private int irv;
            private string ngaychuyen;
            private string solt;

            public string SoLT
            {
                get { return solt; }
                set { solt = value; }
            }
            public string NgayChuyen
            {
                get { return ngaychuyen; }
                set { ngaychuyen = value; }
            }
            public int GTinh
            {
                get { return gtinh; }
                set { gtinh = value; }
            }

            public string TenBV
            {
                get { return tenbv; }
                set { tenbv = value; }
            }

            public string S6
            {
                get { return s6; }
                set { s6 = value; }
            }

            public string S7
            {
                get { return s7; }
                set { s7 = value; }
            }

            public string S8
            {
                get { return s8; }
                set { s8 = value; }
            }

            public string S9
            {
                get { return s9; }
                set { s9 = value; }
            }

            public string KetQua
            {
                get { return ketqua; }
                set { ketqua = value; }
            }

            public string CDNoiGT
            {
                get { return cdnoigt; }
                set { cdnoigt = value; }
            }
            public string Nam
            {
                set { nam = value; }
                get { return nam; }
            }
            public string Nu
            {
                set { nu = value; }
                get { return nu; }
            }
            public string CoThe
            {
                set { cothe = value; }
                get { return cothe; }
            }
            public string S1a
            {
                set { s1a = value; }
                get { return s1a; }
            }
            public string S1b
            {
                set { s1b = value; }
                get { return s1b; }
            }
            public string S2
            {
                set { s2 = value; }
                get { return s2; }
            }
            public string S3
            {
                set { s3 = value; }
                get { return s3; }
            }
            public string S4
            {
                set { s4 = value; }
                get { return s4; }
            }
            public string S5
            {
                set { s5 = value; }
                get { return s5; }
            }
            public int MaBNhan
            {
                set { mabn = value; }
                get { return mabn; }
            }
            public string TenBNhan
            {
                set { tenbn = value; }
                get { return tenbn; }
            }
            public int Tuoi
            {
                set { tuoi = value; }
                get { return tuoi; }
            }
            public string DTuong
            {
                set { doituong = value; }
                get { return doituong; }
            }
            public string SThe
            {
                set { sthe = value; }
                get { return sthe; }
            }
            public string MaKP
            {
                set { makp = value; }
                get { return makp; }
            }
            public string ChanDoan
            {
                set { chandoan = value; }
                get { return chandoan; }
            }
            public string MaBVC
            {
                set { mabv = value; }
                get { return mabv; }
            }
            public int IRV
            {
                set { irv = value; }
                get { return irv; }
            }
        }
        string A1 = "";
        string B1 = "", B2 = "", B3 = "";
        string C1 = "", C2 = "", C3 = "", C4 = "";
        string D1 = "";
        private void Tuyen(string T, string Mac)
        {
            string _T = T;
            string _Mac = Mac;
            switch (_T)
            {
                case "A":

                    break;
                case "B":
                    A1 = "C";
                    B1 = "D";
                    B2 = "E";
                    C1 = "A";
                    D1 = "B";
                    break;
                case "C":
                    A1 = "D";
                    B1 = "E";
                    C1 = "A";
                    C2 = "B";
                    D1 = "C";
                    break;
                case "D":
                    A1 = "E";
                    C1 = "A";
                    C2 = "B";
                    C3 = "C";
                    D1 = "D";
                    break;
                case "E":
                    C1 = "A";
                    C2 = "B";
                    C3 = "C";
                    C4 = "D";
                    D1 = "E";
                    break;
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            var kp = data.KPhongs.ToList();
            List<nhantuyen> _lNTuyen = new List<nhantuyen>();

            if (KTtaoBc())
            {
                var bv = data.BenhViens.ToList();
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

                BaoCao.Rep_ThTTNhanTuyen rep = new BaoCao.Rep_ThTTNhanTuyen();
                BaoCao.Rep_ThTTNhanTuyen_27001 rep1 = new BaoCao.Rep_ThTTNhanTuyen_27001();

                List<KPhong> _lKhoaP = new List<KPhong>();
                for (int i = 0; i < grvKhoaphong.RowCount; i++)
                {
                    if (grvKhoaphong.GetRowCellValue(i, Chọn) != null && grvKhoaphong.GetRowCellValue(i, Chọn).ToString() == "True")
                    {
                        KPhong newkp = new KPhong();
                        newkp.chon = true;
                        newkp.makp = grvKhoaphong.GetRowCellValue(i, MaKP) == null ? 0 : Convert.ToInt32(grvKhoaphong.GetRowCellValue(i, MaKP));
                        if (grvKhoaphong.GetRowCellValue(i, TenKP) != null)
                            newkp.tenkp = grvKhoaphong.GetRowCellValue(i, TenKP).ToString();
                        _lKhoaP.Add(newkp);
                    }
                }
                _lKhoaP.Add(new KPhong { makp = 0, tenkp = "", chon = true });
                int _nt = -1, _ngoaitru = -1;
                if (RadBN.SelectedIndex == 0)
                { _nt = 1; _ngoaitru = 0; }
                if (RadBN.SelectedIndex == 1) { _nt = 1; }
                if (RadBN.SelectedIndex == 2) { _nt = 0; }
                var qbn = (from k in _lKhoaP
                           join bn in data.BenhNhans.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden).Where(p => p.NoiTru == _nt || p.NoiTru == _ngoaitru) on k.makp equals bn.MaKP
                           join bvien in data.BenhViens on bn.MaBV equals bvien.MaBV
                           select new
                           {
                               bn.MaBNhan,
                               bn.TenBNhan,
                               bn.Tuoi,
                               bn.DTuong,
                               bn.GTinh,
                               bn.SThe,
                               bn.MaKP,
                               bn.MaBV,
                               bvien.TenBV,
                               bn.CDNoiGT,

                           }).OrderBy(p => p.TenBNhan).ToList();
                var rv = data.RaViens.Select(p => new { p.MaBNhan, p.KetQua, p.ChanDoan, p.NgayRa, p.SoLT, p.Status }).ToList();
                var qbnRV = (from a in qbn
                             join b in rv on a.MaBNhan equals b.MaBNhan into kq
                             from kq1 in kq.DefaultIfEmpty()
                             select new
                             {
                                 a.MaBNhan,
                                 a.TenBNhan,
                                 a.Tuoi,
                                 a.DTuong,
                                 a.GTinh,
                                 a.SThe,
                                 a.MaKP,
                                 a.MaBV,
                                 a.TenBV,
                                 a.CDNoiGT,
                                 kq1
                             }).ToList();

                if (qbnRV.Count > 0)
                {
                    string hinhthuc = "";
                    if (bv.Where(p => p.MaBV == (DungChung.Bien.MaBV)).First().TuyenBV != null)
                        hinhthuc = bv.Where(p => p.MaBV == (DungChung.Bien.MaBV)).First().TuyenBV.ToString().Trim();
                    foreach (var a in qbnRV)
                    {
                        nhantuyen moi = new nhantuyen();
                        moi.MaBNhan = a.MaBNhan;
                        moi.TenBNhan = a.TenBNhan;
                        moi.CDNoiGT = a.CDNoiGT;
                        if (a.DTuong == "BHYT")
                            moi.DTuong = "X";
                        moi.GTinh = Convert.ToInt32(a.GTinh);
                        if (a.GTinh.Value == 1)
                            moi.Nam = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && a.Tuoi.ToString() != "") ? DungChung.Ham.TuoitheoThang(data, Convert.ToInt32(a.MaBNhan), "12-30") : a.Tuoi.ToString();
                        else
                            moi.Nu = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && a.Tuoi.ToString() != "") ? DungChung.Ham.TuoitheoThang(data, Convert.ToInt32(a.MaBNhan), "12-30") : a.Tuoi.ToString();
                        moi.TenBV = a.TenBV;
                        moi.CDNoiGT = a.CDNoiGT;
                        if (a.kq1 != null)
                        {
                            moi.SoLT = a.kq1.SoLT;
                            if (a.kq1.Status == 1)
                                moi.NgayChuyen = String.Format("{0:dd/MM/yyyy}", a.kq1.NgayRa);
                        }
                        if (bv.Where(p => p.MaBV == (a.MaBV)).ToList().Count > 0 && bv.Where(p => p.MaBV == (a.MaBV)).First().MaBV != null && bv.Where(p => p.MaBV == (a.MaBV)).First().MaBV.ToString() != "")
                        {
                            moi.MaBVC = bv.Where(p => p.MaBV == (a.MaBV)).First().MaBV.Trim();
                            string _MBV = bv.Where(p => p.MaBV == (a.MaBV)).First().TuyenBV.Trim();
                           
                            //Tuyen(bv.Where(p => p.MaBV== (a.MaBV)).First().TenBV);

                            switch (hinhthuc)
                            {
                                case "A":
                                    if (_MBV == "A")
                                    { moi.S3 = "X"; }
                                    if (_MBV == "B")
                                    { moi.S1a = "X"; }
                                    if (_MBV == "C" || _MBV == "D" || _MBV == "E")
                                    { moi.S1b = "X"; }
                                    break;
                                case "B":
                                    if (_MBV == "A")
                                    { moi.S2 = "X"; }
                                    if (_MBV == "B")
                                    { moi.S3 = "X"; }
                                    if (_MBV == "C")
                                    { moi.S1a = "X"; }
                                    if (_MBV == "D" || _MBV == "E")
                                    { moi.S1b = "X"; }

                                    break;
                                case "C":
                                    if (_MBV == "A" || _MBV == "B")
                                    { moi.S2 = "X"; }
                                    if (_MBV == "C")
                                    { moi.S3 = "X"; }
                                    if (_MBV == "D")
                                    { moi.S1a = "X"; }
                                    if (_MBV == "E")
                                    { moi.S1b = "X"; }
                                    break;
                                case "D":
                                    if (_MBV == "A" || _MBV == "B" || _MBV == "C")
                                    { moi.S2 = "X"; }
                                    if (_MBV == "D")
                                    { moi.S3 = "X"; }
                                    if (_MBV == "E")
                                    { moi.S1a = "X"; }
                                    break;
                            }
                        }
                        moi.S4 = "X";
                        _lNTuyen.Add(moi);
                    }
                }

                if (rv.Count() > 0)
                {
                    foreach (var a in _lNTuyen)
                    {
                        foreach (var b in rv)
                        {
                            if (a.MaBNhan == b.MaBNhan)
                            {
                                a.ChanDoan = b.ChanDoan;
                                a.IRV = 1;
                                string kq = b.KetQua;
                                switch (kq)
                                {
                                    case "Khỏi":
                                        a.S6 = "X";
                                        break;
                                    case "Đỡ|Giảm":
                                        a.S6 = "X";
                                        break;
                                    case "Không T.đổi":
                                        a.S7 = "X";
                                        break;
                                    case "Nặng hơn":
                                        a.S7 = "X";
                                        break;
                                    case "Tử vong":
                                        a.S8 = "X";
                                        break;
                                }
                            }
                        }
                    }
                }
                #region Mẫu BC dùng chung
                if (radMauBC.SelectedIndex == 0)
                {
                    #region  Xuat excel
                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    string[] _tieude = { "Stt", "Tên bệnh nhân", "Nam", "Nữ", "Có thẻ BHYT", "CSKBCB Chuyển BN", "Chẩn đoán CSKBCB Chuyển BN", "Hình thức chuyển 1a", "Hình thức chuyển 1b", "Hình thức chuyển 2", "Hình thức chuyển 3", "Lý do chuyển 4", "Lý do chuyển 5", "KQ điều trị 6", "KQ điều trị 7", "KQ điều trị 8", "KQ điều trị 9", "Chẩn đoán ra viện" };
                    string _filePath = "C:\\" + "NguoiBenhChuyenTuCacTuyenDen.xls";
                    int[] _arrWidth = new int[] { };
                    var qexcel = _lNTuyen;
                    DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 18];
                    for (int i = 0; i < 18; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
                    }
                    int num = 1;
                    foreach (var r in qexcel)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                        DungChung.Bien.MangHaiChieu[num, 2] = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && r.Nam != "") ? DungChung.Ham.TuoitheoThang(data, Convert.ToInt32(r.MaBNhan), "12-30") : r.Nam;
                        DungChung.Bien.MangHaiChieu[num, 3] = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && r.Nu != "") ? DungChung.Ham.TuoitheoThang(data, Convert.ToInt32(r.MaBNhan), "12-30") : r.Nu;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.DTuong;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.TenBV;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.CDNoiGT;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.S1a;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.S1b;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.S2;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.S3;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.S4;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.S5;
                        DungChung.Bien.MangHaiChieu[num, 13] = r.S6;
                        DungChung.Bien.MangHaiChieu[num, 14] = r.S7;
                        DungChung.Bien.MangHaiChieu[num, 15] = r.S8;
                        DungChung.Bien.MangHaiChieu[num, 16] = r.S9;
                        DungChung.Bien.MangHaiChieu[num, 17] = r.ChanDoan;
                        num++;
                    }
                    //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "123", _filePath, true);
                    #endregion
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                    rep.Nam.Value = lupTuNgay.Text;
                    rep.Nu.Value = lupDenNgay.Text;
                    rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;
                    rep.Nam.Value = _lNTuyen.Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count()).ToString();
                    rep.Nu.Value = _lNTuyen.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count()).ToString();
                    rep.BHYT.Value = _lNTuyen.Where(p => p.DTuong == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.DTuong == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep.HT1a.Value = _lNTuyen.Where(p => p.S1a == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S1a == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep.HT1b.Value = _lNTuyen.Where(p => p.S1b == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S1b == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep.HT2.Value = _lNTuyen.Where(p => p.S2 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S2 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep.HT3.Value = _lNTuyen.Where(p => p.S3 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S3 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep.LD4.Value = _lNTuyen.Where(p => p.S4 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S4 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep.LD5.Value = _lNTuyen.Where(p => p.S5 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S5 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep.KQ6.Value = _lNTuyen.Where(p => p.S6 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S6 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep.KQ6.Value = _lNTuyen.Where(p => p.S7 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S7 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep.KQ8.Value = _lNTuyen.Where(p => p.S8 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S8 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep.KQ9.Value = _lNTuyen.Where(p => p.S9 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S9 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    if (radIn.SelectedIndex == 0)
                    {
                        rep.DataSource = _lNTuyen.Where(p => p.IRV == 1).ToList();
                    }
                    else
                    {
                        rep.DataSource = _lNTuyen.ToList();
                    }
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
                #region Mẫu BC TTYT TP.Bắc Ninh
                if (radMauBC.SelectedIndex == 1)
                {
                    #region  Xuat excel
                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    string[] _tieude = { "Stt", "Tên bệnh nhân", "Nam", "Nữ", "Có thẻ BHYT", "CSKBCB Chuyển BN", "Chẩn đoán CSKBCB Chuyển BN", "Hình thức chuyển 1a", "Hình thức chuyển 1b", "Hình thức chuyển 2", "Hình thức chuyển 3", "Lý do chuyển 4", "Lý do chuyển 5", "KQ điều trị 6", "KQ điều trị 7", "KQ điều trị 8", "KQ điều trị 9", "Chẩn đoán ra viện", "Ngày chuyển", "Số lưu trữ" };
                    string _filePath = "C:\\" + "NguoiBenhChuyenTuCacTuyenDen_27001.xls";
                    int[] _arrWidth = new int[] { };
                    var qexcel = _lNTuyen;
                    DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 20];
                    for (int i = 0; i < 20; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
                    }
                    int num = 1;
                    foreach (var r in qexcel)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                        DungChung.Bien.MangHaiChieu[num, 2] = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && r.Nam != "") ? DungChung.Ham.TuoitheoThang(data, Convert.ToInt32(r.MaBNhan), "12-30") : r.Nam;
                        DungChung.Bien.MangHaiChieu[num, 3] = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && r.Nu != "") ? DungChung.Ham.TuoitheoThang(data, Convert.ToInt32(r.MaBNhan), "12-30") : r.Nu;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.DTuong;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.TenBV;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.CDNoiGT;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.S1a;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.S1b;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.S2;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.S3;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.S4;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.S5;
                        DungChung.Bien.MangHaiChieu[num, 13] = r.S6;
                        DungChung.Bien.MangHaiChieu[num, 14] = r.S7;
                        DungChung.Bien.MangHaiChieu[num, 15] = r.S8;
                        DungChung.Bien.MangHaiChieu[num, 16] = r.S9;
                        DungChung.Bien.MangHaiChieu[num, 17] = r.ChanDoan;
                        DungChung.Bien.MangHaiChieu[num, 18] = r.NgayChuyen;
                        DungChung.Bien.MangHaiChieu[num, 19] = r.SoLT;
                        num++;
                    }
                    QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "123", _filePath, true);
                    #endregion
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                    rep1.Nam.Value = lupTuNgay.Text;
                    rep1.Nu.Value = lupDenNgay.Text;
                    rep1.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;
                    rep1.Nam.Value = _lNTuyen.Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count()).ToString();
                    rep1.Nu.Value = _lNTuyen.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count()).ToString();
                    rep1.BHYT.Value = _lNTuyen.Where(p => p.DTuong == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.DTuong == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep1.HT1a.Value = _lNTuyen.Where(p => p.S1a == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S1a == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep1.HT1b.Value = _lNTuyen.Where(p => p.S1b == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S1b == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep1.HT2.Value = _lNTuyen.Where(p => p.S2 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S2 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep1.HT3.Value = _lNTuyen.Where(p => p.S3 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S3 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep1.LD4.Value = _lNTuyen.Where(p => p.S4 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S4 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep1.LD5.Value = _lNTuyen.Where(p => p.S5 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S5 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep1.KQ6.Value = _lNTuyen.Where(p => p.S6 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S6 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep1.KQ6.Value = _lNTuyen.Where(p => p.S7 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S7 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep1.KQ8.Value = _lNTuyen.Where(p => p.S8 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S8 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    rep.KQ9.Value = _lNTuyen.Where(p => p.S9 == "X").Select(p => p.MaBNhan).Count() == 0 ? null : (_lNTuyen.Where(p => p.S9 == "X").Select(p => p.MaBNhan).Count()).ToString();
                    if (radIn.SelectedIndex == 0)
                    {
                        rep1.DataSource = _lNTuyen.Where(p => p.IRV == 1).ToList();
                    }
                    else
                    {
                        rep1.DataSource = _lNTuyen.ToList();
                    }
                    rep1.BindingData();
                    rep1.CreateDocument();
                    frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
            }

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

        private void RadBN_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}