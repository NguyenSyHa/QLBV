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

    public partial class Frm_BcHoatDongKKB_CL : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcHoatDongKKB_CL()
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

        private void Frm_BcHoatDongKKB_HL01_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;

            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Phòng khám")
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
        public class KB
        {
            private string chuyenkhoa;

            public string ChuyenKhoa
            {
                get { return chuyenkhoa; }
                set { chuyenkhoa = value; }
            }
            private string a1;

            public string A1
            {
                get { return a1; }
                set { a1 = value; }
            }
            private string a2;

            public string A2
            {
                get { return a2; }
                set { a2 = value; }
            }
            private string a3;

            public string A3
            {
                get { return a3; }
                set { a3 = value; }
            }
            private string a4;

            public string A4
            {
                get { return a4; }
                set { a4 = value; }
            }
            private string a5;

            public string A5
            {
                get { return a5; }
                set { a5 = value; }
            }
            private string a6;

            public string A6
            {
                get { return a6; }
                set { a6 = value; }
            }
            private string a7;

            public string A7
            {
                get { return a7; }
                set { a7 = value; }
            }
            private string a8;

            public string A8
            {
                get { return a8; }
                set { a8 = value; }
            }
            private string a9;

            public string A9
            {
                get { return a9; }
                set { a9 = value; }
            }

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            var kp = data.KPhongs.ToList();
            List<KB> _lKB = new List<KB>();

            if (KTtaoBc())
            {
                var bv = data.BenhViens.ToList();
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                DungChung.Bien.c_chuyenkhoa.f_ChuyenKhoa();
                //frmIn frm = new frmIn();
                BaoCao.Rep_BcHoatDongKKB_CL rep = new BaoCao.Rep_BcHoatDongKKB_CL();
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
                List<KPhong> _lKhoaP = new List<KPhong>();

                _lKhoaP = _Kphong.Where(p => p.makp > 0).Where(p => p.chon == true).ToList();
                #region Ko thống kê BN chuyển PK
                if (radioGroup1.SelectedIndex == 0)//Lươt KB || Ko thống kê BN chuyển PK
                {
                    _lKB.Clear();
                    var id = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay)
                              join k in data.KPhongs.Where(p => p.PLoai == "Phòng khám") on kb.MaKP equals k.MaKP
                              group kb by kb.MaBNhan into kq
                              select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();
                    var qkb2 = (from k in id
                                join kb in data.BNKBs on k.IDKB equals kb.IDKB
                                join bn in data.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                                group new { kb, bn } by new { kb.MaBNhan, bn.NoiTru, bn.DTNT, kb.MaKP, kb.PhuongAn, kb.MaCK, bn.Tuoi, bn.DTuong, bn.CapCuu, bn.GTinh } into kq
                                select new { kq.Key.DTNT, kq.Key.GTinh, kq.Key.MaBNhan, kq.Key.NoiTru, kq.Key.PhuongAn, kq.Key.MaKP, kq.Key.MaCK, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.CapCuu, IDKB = kq.Max(p => p.kb.IDKB) }).ToList();

                    var qkb = (from ma in _lKhoaP
                               join p in qkb2 on ma.makp equals p.MaKP
                               join ck in DungChung.Bien._lChuyenKhoa on p.MaCK equals ck.MaCK
                               group new { p } by new { ck.ChuyenKhoa } into kq
                               select new
                               {
                                   ChuyenKhoa = kq.Key.ChuyenKhoa,
                                   A1 = kq.Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Select(p => p.p.MaBNhan).Count().ToString(),
                                   A2 = kq.Where(p => p.p.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count().ToString(),
                                   A3 = kq.Where(p => p.p.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count().ToString(),
                                   A5 = kq.Where(p => p.p.CapCuu == 1).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.CapCuu == 1).Select(p => p.p.MaBNhan).Count().ToString(),
                                   A6 = kq.Where(p => p.p.PhuongAn == 1).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.PhuongAn == 1).Select(p => p.p.MaBNhan).Count().ToString(),
                                   A7 = kq.Where(p => p.p.PhuongAn == 2).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.PhuongAn == 2).Select(p => p.p.MaBNhan).Count().ToString(),

                               }).ToList();

                    if (qkb.Count > 0)
                    {
                        foreach (var a in qkb)
                        {
                            KB them = new KB();
                            them.ChuyenKhoa = a.ChuyenKhoa;
                            them.A1 = a.A1;
                            them.A2 = a.A2;
                            them.A3 = a.A3;
                            them.A4 = "";
                            them.A5 = a.A5;
                            them.A6 = a.A6;
                            them.A7 = a.A7;

                            _lKB.Add(them);
                        }

                    }
                    var qdtnt = (from ma in _lKhoaP
                                 join p in qkb2.Where(p => p.DTNT == true) on ma.makp equals p.MaKP
                                 join ck in DungChung.Bien._lChuyenKhoa on p.MaCK equals ck.MaCK
                                 join rv in data.RaViens on p.MaBNhan equals rv.MaBNhan
                                 group new { p, rv } by new { ck.ChuyenKhoa } into kq
                                 select new
                                 {
                                     ChuyenKhoa = kq.Key.ChuyenKhoa,
                                     SoNguoiDT = kq.Select(p => p.rv.MaBNhan).Count(),
                                     SSNDTNT = kq.Sum(p => p.rv.SoNgaydt),
                                 }).ToList();
                    if (qdtnt.Count > 0)
                    {
                        foreach (var a in _lKB)
                        {
                            foreach (var b in qdtnt)
                            {
                                if (a.ChuyenKhoa == b.ChuyenKhoa)
                                {
                                    a.A8 = b.SoNguoiDT.ToString();
                                    a.A9 = b.SSNDTNT.ToString();
                                }
                            }
                        }
                    }

                }
                #endregion
                #region TK cả BN chuyển PK
                if (radioGroup1.SelectedIndex == 1)
                {
                    _lKB.Clear();
                    var qbn2 = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay)
                                join bn in data.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                                group new { kb, bn } by new { kb.MaBNhan, kb.MaKP, kb.PhuongAn, kb.MaCK, bn.Tuoi, bn.DTuong, bn.CapCuu, bn.GTinh, bn.NoiTru, bn.DTNT } into kq
                                select new { kq.Key.DTNT, kq.Key.GTinh, kq.Key.MaBNhan, kq.Key.NoiTru, kq.Key.PhuongAn, kq.Key.MaKP, kq.Key.MaCK, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.CapCuu, IDKB = kq.Max(p => p.kb.IDKB) }).ToList();

                    var qbn = (from ma in _lKhoaP
                               join p in qbn2 on ma.makp equals p.MaKP
                               join ck in DungChung.Bien._lChuyenKhoa on p.MaCK equals ck.MaCK
                               group new { p } by new { ck.ChuyenKhoa } into kq
                               select new
                               {
                                   ChuyenKhoa = kq.Key.ChuyenKhoa,
                                   A1 = kq.Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Select(p => p.p.MaBNhan).Count().ToString(),
                                   A2 = kq.Where(p => p.p.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count().ToString(),
                                   A3 = kq.Where(p => p.p.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count().ToString(),
                                   A5 = kq.Where(p => p.p.CapCuu == 1).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.CapCuu == 1).Select(p => p.p.MaBNhan).Count().ToString(),
                                   A6 = kq.Where(p => p.p.PhuongAn == 1).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.PhuongAn == 1).Select(p => p.p.MaBNhan).Count().ToString(),
                                   A7 = kq.Where(p => p.p.PhuongAn == 2).Select(p => p.p.MaBNhan).Count() == 0 ? null : kq.Where(p => p.p.PhuongAn == 2).Select(p => p.p.MaBNhan).Count().ToString(),

                               }).ToList();

                    if (qbn.Count > 0)
                    {
                        foreach (var a in qbn)
                        {
                            KB them = new KB();
                            them.ChuyenKhoa = a.ChuyenKhoa;
                            them.A1 = a.A1;
                            them.A2 = a.A2;
                            them.A3 = a.A3;
                            them.A4 = "";
                            them.A5 = a.A5;
                            them.A6 = a.A6;
                            them.A7 = a.A7;

                            _lKB.Add(them);
                        }
                    }
                    var qdtnt = (from ma in _lKhoaP
                                 join p in qbn2.Where(p => p.DTNT == true) on ma.makp equals p.MaKP
                                 join ck in DungChung.Bien._lChuyenKhoa on p.MaCK equals ck.MaCK
                                 join rv in data.RaViens on p.MaBNhan equals rv.MaBNhan
                                 group new { p, rv } by new { ck.ChuyenKhoa } into kq
                                 select new
                                 {
                                     ChuyenKhoa = kq.Key.ChuyenKhoa,
                                     SoNguoiDT = kq.Select(p => p.rv.MaBNhan).Count(),
                                     SSNDTNT = kq.Sum(p => p.rv.SoNgaydt),
                                 }).ToList();
                    if (qdtnt.Count > 0)
                    {
                        foreach (var a in _lKB)
                        {
                            foreach (var b in qdtnt)
                            {
                                if (a.ChuyenKhoa == b.ChuyenKhoa)
                                {
                                    a.A8 = b.SoNguoiDT.ToString();
                                    a.A9 = b.SSNDTNT.ToString();
                                }
                            }
                        }
                    }
                }
                #endregion
                if (chkSX.Checked == false)
                {
                    rep.DataSource = _lKB.ToList();
                    #region xuat Excel
                    DungChung.Bien.MangHaiChieu = new Object[_lKB.Count + 1, 11];
                    string[] _arr = new string[] { "0", "@", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                    string[] _tieude = { "TT", "Khám chuyên khoa", "Tổng số lần KB", "TS lần khám bệnh BHYT", "TS lần khám bệnh viện phí", "TS lần khám bệnh không thu được", "Số lần cấp cứu", "Số người bệnh vào viện", "Số người bệnh chuyển viện", "Số người bệnh điều trị ngoại trú", "Số ngày điều trị ngoại trú" };
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }
                    int[] _arrWidth = new int[] { 5, 20, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

                    int num = 1;
                    foreach (var r in _lKB)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num + 1;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.ChuyenKhoa;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.A1;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.A2;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.A3;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.A4;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.A5;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.A6;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.A7;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.A8;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.A9;

                        num++;

                    }

                    #endregion
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo hoạt động khám bệnh", "C:\\BcHDKB.xls", false, this.Name);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    var KB = (from l in _lKB
                              join ck in data.ChuyenKhoas.Where(p => p.Status != 0) on l.ChuyenKhoa equals ck.TenCK
                              group l by new { l.ChuyenKhoa, l.A1, l.A2, l.A3, l.A4, l.A5, l.A6, l.A7, l.A8, l.A9, ck.Status } into kq
                              select new { kq.Key.ChuyenKhoa, kq.Key.Status, kq.Key.A1, kq.Key.A2, kq.Key.A3, kq.Key.A4, kq.Key.A5, kq.Key.A6, kq.Key.A7, kq.Key.A8, kq.Key.A9 }).ToList();
                    rep.DataSource = KB.OrderBy(p => p.Status).ToList();
                    #region xuat Excel
                    DungChung.Bien.MangHaiChieu = new Object[KB.Count + 1, 11];
                    string[] _arr = new string[] { "0", "@", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                    string[] _tieude = { "TT", "Khám chuyên khoa", "Tổng số lần KB", "TS lần khám bệnh BHYT", "TS lần khám bệnh viện phí", "TS lần khám bệnh không thu được", "Số lần cấp cứu", "Số người bệnh vào viện", "Số người bệnh chuyển viện", "Số người bệnh điều trị ngoại trú", "Số ngày điều trị ngoại trú" };
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }
                    int[] _arrWidth = new int[] { 5, 20, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

                    int num = 1;
                    foreach (var r in KB)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num + 1;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.ChuyenKhoa;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.A1;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.A2;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.A3;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.A4;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.A5;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.A6;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.A7;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.A8;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.A9;

                        num++;

                    }

                    #endregion
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo hoạt động khám bệnh", "C:\\BcHDKB.xls", false, this.Name);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }



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

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}