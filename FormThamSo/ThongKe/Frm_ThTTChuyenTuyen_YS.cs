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
    public partial class Frm_ThTTChuyenTuyen_YS : DevExpress.XtraEditors.XtraForm
    {
        public Frm_ThTTChuyenTuyen_YS()
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
        private void Frm_ThTTChuyenTuyen_YS_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Today;
            DateTime tungay = lupTuNgay.DateTime.AddDays(1).AddHours(23).AddMinutes(59);
            lupDenNgay.DateTime = tungay;
            _Kphong.Clear();
            var kphong = (from kp in data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0 ;
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

        public class chuyentuyen
        {
            public int mabn;
            public string tenbn;
            public int tuoi;
            public string doituong;
            public string sthe;
            public string makp;
            public string chandoan;
            public string hinhthuc;
            public string lydo;
            public string mabv;
            public string s1a;
            public string s1b;
            public string s2;
            public string s3;
            public string s4;
            public string s5;
            public string cothe;
            public string nam;
            public string nu;
            private DateTime ngaychuyen;
            public string BSChuyen { get; set; }

            public DateTime Ngaychuyen
            {
                get { return ngaychuyen; }
                set { ngaychuyen = value; }
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
            public string HinhThucC
            {
                set { hinhthuc = value; }
                get { return hinhthuc; }
            }
            public string LyDoC
            {
                set { lydo = value; }
                get { return lydo; }
            }
            public string MaBVC
            {
                set { mabv = value; }
                get { return mabv; }
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            var kp = data.KPhongs.ToList();
            List<chuyentuyen> _lCTuyen = new List<chuyentuyen>();

            if (KTtaoBc())
            {
                var bv = data.BenhViens.ToList();
                ngaytu = lupTuNgay.DateTime;
                ngayden = lupDenNgay.DateTime;
                if (DungChung.Bien.MaBV != "27001")
                {
                    if (DungChung.Bien.MaBV == "30007")
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_ThTTChuyenTuyen_YS_30007 rep = new BaoCao.Rep_ThTTChuyenTuyen_YS_30007();
                        rep.TuNgay.Value = lupTuNgay.Text;
                        rep.DenNgay.Value = lupDenNgay.Text;
                        rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;
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
                                   join rv in data.RaViens.Where(p => p.Status == 1) on k.makp equals rv.MaKP
                                   join bn in data.BenhNhans.Where(p => p.NoiTru == _nt || p.NoiTru == _ngoaitru) on rv.MaBNhan equals bn.MaBNhan
                                   where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                                   select new
                                   {
                                       bn.MaBNhan,
                                       bn.TenBNhan,
                                       bn.Tuoi,
                                       bn.DTuong,
                                       bn.SThe,
                                       bn.GTinh,
                                       bn.MaKP,
                                       rv.ChanDoan,
                                       rv.HinhThucC,
                                       rv.LyDoC,
                                       rv.MaBVC,
                                       rv.NgayRa,
                                       rv.MaBS
                                   }).OrderBy(p => p.TenBNhan).ToList();
                        if (qbn.Count > 0)
                        {
                            foreach (var a in qbn)
                            {
                                chuyentuyen moi = new chuyentuyen();
                                moi.ChanDoan = a.ChanDoan;
                                if (a.DTuong == "BHYT")
                                    moi.DTuong = a.SThe;
                                if (a.GTinh.Value == 1)
                                    moi.Nam = a.Tuoi.ToString();
                                else
                                    moi.Nu = a.Tuoi.ToString();
                                moi.HinhThucC = a.HinhThucC;
                                moi.LyDoC = a.LyDoC;
                                moi.MaBNhan = a.MaBNhan;
                                moi.TenBNhan = a.TenBNhan;
                                moi.Ngaychuyen = a.NgayRa.Value;
                                if (!string.IsNullOrEmpty(a.MaBS))
                                {
                                    var tenbs = data.CanBoes.Where(p => p.MaCB == a.MaBS).FirstOrDefault();
                                    if (tenbs != null)
                                        moi.BSChuyen = tenbs.TenCB;
                                }
                                if (bv.Where(p => p.MaBV == (a.MaBVC)).ToList().Count > 0)
                                {
                                    moi.MaBVC = bv.Where(p => p.MaBV == (a.MaBVC)).First().TenBV;
                                    string hinhthuc = "";
                                    var q4 = data.BenhViens.Where(p => p.MaBV == a.MaBVC).ToList();
                                    string HaBVC = q4.First().TuyenBV.Trim();
                                    var q5 = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList();
                                    string HaBV = q5.First().TuyenBV.Trim();
                                    hinhthuc = TuyenBV(HaBV, HaBVC);
                                    switch (hinhthuc)
                                    {
                                        case "1":
                                            moi.S1a = "X";
                                            break;
                                        case "2":
                                            moi.S1b = "X";
                                            break;
                                        case "3":
                                            moi.S2 = "X";
                                            break;
                                        case "4":
                                            moi.S3 = "X";
                                            break;

                                    }
                                }
                                if (a.LyDoC == "Đủ điều kiện chuyển tuyến(đúng tuyến)")
                                    moi.S4 = "X";
                                else
                                    moi.S5 = "X";
                                if (kp.Where(p => p.MaKP == (a.MaKP)).ToList().Count > 0)
                                    moi.MaKP = kp.Where(p => p.MaKP == (a.MaKP)).ToList().First().TenKP;
                                _lCTuyen.Add(moi);
                            }
                        }

                        if (_lCTuyen.Count > 0)
                        {
                            rep.DataSource = _lCTuyen.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                            MessageBox.Show("Không có dữ liệu");
                    }
                    else
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_ThTTChuyenTuyen_YS rep = new BaoCao.Rep_ThTTChuyenTuyen_YS();
                        rep.TuNgay.Value = lupTuNgay.Text;
                        rep.DenNgay.Value = lupDenNgay.Text;
                        rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;
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
                                   join rv in data.RaViens.Where(p => p.Status == 1) on k.makp equals rv.MaKP
                                   join bn in data.BenhNhans.Where(p => p.NoiTru == _nt || p.NoiTru == _ngoaitru) on rv.MaBNhan equals bn.MaBNhan
                                   where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                                   select new
                                   {
                                       bn.MaBNhan,
                                       bn.TenBNhan,
                                       bn.Tuoi,
                                       bn.DTuong,
                                       bn.SThe,
                                       bn.GTinh,
                                       bn.MaKP,
                                       rv.ChanDoan,
                                       rv.HinhThucC,
                                       rv.LyDoC,
                                       rv.MaBVC,
                                       rv.NgayRa,
                                       rv.MaCB,
                                   }).OrderBy(p => p.TenBNhan).ToList();
                        if (qbn.Count > 0)
                        {
                            foreach (var a in qbn)
                            {
                                chuyentuyen moi = new chuyentuyen();
                                moi.ChanDoan = a.ChanDoan;
                                if (a.DTuong == "BHYT")
                                    moi.DTuong = a.SThe;
                                if (a.GTinh.Value == 1)
                                    moi.Nam = a.Tuoi.ToString();
                                else
                                    moi.Nu = a.Tuoi.ToString();
                                moi.HinhThucC = a.HinhThucC;
                                moi.LyDoC = a.LyDoC;
                                moi.MaBNhan = a.MaBNhan;
                                moi.TenBNhan = a.TenBNhan;
                                moi.Ngaychuyen = a.NgayRa.Value;
                                if (bv.Where(p => p.MaBV == (a.MaBVC)).ToList().Count > 0)
                                {
                                    moi.MaBVC = bv.Where(p => p.MaBV == (a.MaBVC)).First().TenBV;
                                    string hinhthuc = "";
                                    var q4 = data.BenhViens.Where(p => p.MaBV == a.MaBVC).ToList();
                                    string HaBVC = q4.First().TuyenBV.Trim();
                                    var q5 = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList();
                                    string HaBV = q5.First().TuyenBV.Trim();
                                    hinhthuc = TuyenBV(HaBV, HaBVC);
                                    switch (hinhthuc)
                                    {
                                        case "1":
                                            moi.S1a = "X";
                                            break;
                                        case "2":
                                            moi.S1b = "X";
                                            break;
                                        case "3":
                                            moi.S2 = "X";
                                            break;
                                        case "4":
                                            moi.S3 = "X";
                                            break;

                                    }
                                }
                                if (a.LyDoC == "Đủ điều kiện chuyển tuyến(đúng tuyến)")
                                    moi.S4 = "X";
                                else
                                    moi.S5 = "X";
                                if (kp.Where(p => p.MaKP == (a.MaKP)).ToList().Count > 0)
                                    moi.MaKP = kp.Where(p => p.MaKP == (a.MaKP)).ToList().First().TenKP;
                                _lCTuyen.Add(moi);
                            }
                        }

                        if (_lCTuyen.Count > 0)
                        {
                            rep.DataSource = _lCTuyen.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                            MessageBox.Show("Không có dữ liệu");
                    }
                }
                else
                {
                    frmIn frm = new frmIn();
                    BaoCao.Rep_ThTTChuyenTuyen_YS_27001 rep = new BaoCao.Rep_ThTTChuyenTuyen_YS_27001();
                    rep.TuNgay.Value = lupTuNgay.Text;
                    rep.DenNgay.Value = lupDenNgay.Text;
                    rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;
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
                               join rv in data.RaViens.Where(p => p.Status == 1) on k.makp equals rv.MaKP
                               join bn in data.BenhNhans.Where(p => p.NoiTru == _nt || p.NoiTru == _ngoaitru) on rv.MaBNhan equals bn.MaBNhan
                               where (rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden)
                               select new
                               {
                                   bn.MaBNhan,
                                   bn.TenBNhan,
                                   bn.Tuoi,
                                   bn.DTuong,
                                   bn.SThe,
                                   bn.GTinh,
                                   bn.MaKP,
                                   rv.ChanDoan,
                                   rv.HinhThucC,
                                   rv.LyDoC,
                                   rv.MaBVC,
                                   rv.NgayRa
                               }).OrderBy(p => p.TenBNhan).ToList();
                    if (qbn.Count > 0)
                    {
                        foreach (var a in qbn)
                        {
                            chuyentuyen moi = new chuyentuyen();
                            moi.ChanDoan = a.ChanDoan;
                            if (a.DTuong == "BHYT")
                                moi.DTuong = a.SThe;
                            if (a.GTinh.Value == 1)
                                moi.Nam = a.Tuoi.ToString();
                            else
                                moi.Nu = a.Tuoi.ToString();
                            moi.HinhThucC = a.HinhThucC;
                            moi.LyDoC = a.LyDoC;
                            moi.MaBNhan = a.MaBNhan;
                            moi.TenBNhan = a.TenBNhan;
                            moi.Ngaychuyen = a.NgayRa.Value;
                            if (bv.Where(p => p.MaBV == (a.MaBVC)).ToList().Count > 0)
                            {
                                moi.MaBVC = bv.Where(p => p.MaBV == (a.MaBVC)).First().TenBV;
                                string hinhthuc = "";
                                var q4 = data.BenhViens.Where(p => p.MaBV == a.MaBVC).ToList();
                                string HaBVC = q4.First().TuyenBV.Trim();
                                var q5 = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList();
                                string HaBV = q5.First().TuyenBV.Trim();
                                hinhthuc = TuyenBV(HaBV, HaBVC);
                                switch (hinhthuc)
                                {
                                    case "1":
                                        moi.S1a = "X";
                                        break;
                                    case "2":
                                        moi.S1b = "X";
                                        break;
                                    case "3":
                                        moi.S2 = "X";
                                        break;
                                    case "4":
                                        moi.S3 = "X";
                                        break;

                                }
                            }
                            if (a.LyDoC == "Đủ điều kiện chuyển tuyến(đúng tuyến)")
                                moi.S4 = "X";
                            else
                                moi.S5 = "X";
                            if (kp.Where(p => p.MaKP == (a.MaKP)).ToList().Count > 0)
                                moi.MaKP = kp.Where(p => p.MaKP == (a.MaKP)).ToList().First().TenKP;
                            _lCTuyen.Add(moi);
                        }
                    }

                    if (_lCTuyen.Count > 0)
                    {
                        rep.DataSource = _lCTuyen.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                        MessageBox.Show("Không có dữ liệu");
                }
            }

        }
        private string TuyenBV(string bv, string bvchuyen)
        {
            // return 1 là chuyển từ tuyến dưới lên tuyến trên liền kề
            // return 2 là chuyển người bệnh từ tuyến dưới lên tuyến trên không liền kề
            // return 3 là chuyển người bệnh cùng tuyến
            // retunr 4 là người bệnh từ tuyến trên về tuyến dưới
            string MaBV = bv.Trim();
            string MaBVC = bvchuyen.Trim();
            switch (MaBV)
            {
                case "A":
                    if (MaBVC == "A")
                    {
                        return "4";
                    }
                    if (MaBVC == "B")
                    {
                        return "3";
                    }
                    if (MaBVC == "C")
                    {
                        return "3";
                    }
                    if (MaBVC == "D")
                    {
                        return "3";
                    }
                    if (MaBVC == "E")
                    {
                        return "3";
                    }
                    break;
                case "B":
                    if (MaBVC == "A")
                    {
                        return "1";
                    }
                    if (MaBVC == "B")
                    {
                        return "4";
                    }
                    if (MaBVC == "C")
                    {
                        return "3";
                    }
                    if (MaBVC == "D")
                    {
                        return "3";
                    }
                    if (MaBVC == "E")
                    {
                        return "3";
                    }
                    break;
                case "C":
                    if (MaBVC == "A")
                    {
                        return "2";
                    }
                    if (MaBVC == "B")
                    {
                        return "1";
                    }
                    if (MaBVC == "C")
                    {
                        return "4";
                    }
                    if (MaBVC == "D")
                    {
                        return "3";
                    }
                    if (MaBVC == "E")
                    {
                        return "3";
                    }
                    break;
                case "D":
                    if (MaBVC == "A")
                    {
                        return "2";
                    }
                    if (MaBVC == "B")
                    {
                        return "2";
                    }
                    if (MaBVC == "C")
                    {
                        return "1";
                    }
                    if (MaBVC == "D")
                    {
                        return "4";
                    }
                    if (MaBVC == "E")
                    {
                        return "3";
                    }
                    break;
                case "E":
                    if (MaBVC == "A")
                    {
                        return "2";
                    }
                    if (MaBVC == "B")
                    {
                        return "2";
                    }
                    if (MaBVC == "C")
                    {
                        return "2";
                    }
                    if (MaBVC == "D")
                    {
                        return "1";
                    }
                    if (MaBVC == "E")
                    {
                        return "4";
                    }
                    break;
            }
            return "";
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