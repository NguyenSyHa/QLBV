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

    public partial class Frm_BcHoatDongCLS_TH03 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcHoatDongCLS_TH03()
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
            private string PLoai;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public string ploai
            { set { PLoai = value; } get { return PLoai; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }

        List<KPhong> _Kphong = new List<KPhong>();

        private void Frm_BcHoatDongPTTT_TT02_Load(object sender, EventArgs e)
        {
            //if(DungChung.Bien.MaBV=="26007")
            //{
            //    grchonmau.SelectedIndex = 1;
            //    ckbnguoi.Enabled = false;
            //}

            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;

            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng")
                          select new { kp.TenKP, kp.MaKP, kp.PLoai }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.ploai = "";
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.ploai = a.PLoai;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }
        public class CLS
        {
            private string tennhomct;

            public string TenNhomCT
            {
                get { return tennhomct; }
                set { tennhomct = value; }
            }
            private string tentn;

            public string TenTN
            {
                get { return tentn; }
                set { tentn = value; }
            }
            private double ts;

            public double TS
            {
                get { return ts; }
                set { ts = value; }
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

            private string dvt;

            public string DVT
            {
                get { return dvt; }
                set { dvt = value; }
            }



            public string TenDV { get; set; }
        }
        public class DV
        {

            public string TenNhomCT { get; set; }

            public string TenTN { get; set; }

            public int? MaBNhan { get; set; }

            public int? MaKP { get; set; }

            public string DTuong { get; set; }

            public int? NoiTru { get; set; }

            public string TenDV { get; set; }

            public double SoLuong { get; set; }

            public string TenRG { get; set; }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            var kp = data.KPhongs.ToList();
            List<CLS> _lcls = new List<CLS>();

            if (KTtaoBc())
            {
                _lcls.Clear();
                var bv = data.BenhViens.ToList();
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                frmIn frm = new frmIn();

                List<KPhong> _lkp = new List<KPhong>();
                _lkp = _Kphong.Where(p => p.chon == true).ToList();
                _lkp.Add(new KPhong { makp = 0, tenkp = "" });

                string aa = cboTKBN.EditValue.ToString();
                #region bệnh viện thanh hà 26007 tính theo lượt thực hiện (xét nghiệm)
                if (grchonmau.SelectedIndex == 1)
                {
                    _lcls = new List<CLS>();
                    List<DV> lAll = new List<DV>();
                    var qIDCD = data.CLScts.Where(p => p.Status == 1).Select(p => p.IDCD).Distinct().ToList();
                    #region Tất cả BN thực hiện

                    var qdv = (from dv in data.DichVus.Where(parameters => parameters.PLoai == 2 && parameters.Status == 1)
                               join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                               join nhom in data.NhomDVs.Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh") on dv.IDNhom equals nhom.IDNhom
                               select new { dv.MaDV, dv.TenDV, tnhom.TenTN, tnhom.TenRG, nhom.TenNhomCT }).ToList();
                    if (aa == "Tất cả BN thực hiện (Thống kê theo ngày thực hiện)")
                    {
                        lAll = new List<DV>();
                        if (chkBNcoCLS.Checked == true)
                        {

                            var q0 = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                      join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                      // join clsct in data.CLScts.Where(p=>p.Status ==1) on cd.IDCD equals clsct.IDCD
                                      join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                      // group new {cls, cd,bn} by new {bn.MaBNhan, cls.MaKP,bn.DTuong, NoiTru = bn.NoiTru,cd.MaDV } into kq
                                      // select new { kq.Key.MaBNhan, kq.Key.MaKP, kq.Key.DTuong, NoiTru = kq.Key.NoiTru, kq.Key.MaDV }).ToList();
                                      select new { bn.MaBNhan, cls.MaKP, bn.DTuong, NoiTru = bn.NoiTru, cd.MaDV, cd.IDCD }).ToList();
                            var q1 = (from a in q0
                                      join idcd in qIDCD on a.IDCD equals idcd
                                      select new { a.MaBNhan, a.MaKP, a.DTuong, NoiTru = a.NoiTru, a.MaDV, a.IDCD }
                                     ).ToList();
                            //var q1 = (from a in q0 group a by new {a.MaBNhan, a.MaKP, a.DTuong, NoiTru = a.NoiTru, a.MaDV,a.IDCD } into kq
                            //          select new { kq.Key.MaBNhan, kq.Key.MaKP, kq.Key.DTuong, NoiTru = kq.Key.NoiTru, kq.Key.MaDV, kq.Key.IDCD }).ToList();

                            var q = (from cls in q1
                                     join dv in qdv on cls.MaDV equals dv.MaDV
                                     select new DV { TenNhomCT = dv.TenNhomCT, TenTN = dv.TenTN, TenRG = dv.TenRG, MaBNhan = cls.MaBNhan, MaKP = cls.MaKP, DTuong = cls.DTuong, NoiTru = cls.NoiTru, TenDV = dv.TenDV }).ToList();

                            lAll.AddRange(q);
                        }
                        if (chkBNkhongCLS.Checked == true)
                        {
                            var qdt = (from dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.IDCD == null || p.IDCD <= 0)
                                       join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                       join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                       join nhom in data.NhomDVs.Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh") on dv.IDNhom equals nhom.IDNhom
                                       join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                       join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                       select new DV { TenNhomCT = nhom.TenNhomCT, TenTN = tnhom.TenTN, TenRG = tnhom.TenRG, MaBNhan = dt.MaBNhan, MaKP = dtct.MaKP, DTuong = bn.DTuong, NoiTru = bn.NoiTru, TenDV = dv.TenDV, SoLuong = dtct.SoLuong }).ToList();
                            qdt = (from dt in qdt join ma in _lkp on dt.MaKP equals ma.makp select dt).ToList();
                            lAll.AddRange(qdt);
                        }
                    }
                    #endregion
                    #region Chỉ BN đã TT
                    if (aa == "Chỉ BN đã thanh toán (Thống kê theo ngày thanh toán)")
                    {
                        lAll = new List<DV>();
                        if (chkBNcoCLS.Checked == true)
                        {
                            var q0 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                      join cls in data.CLS on vp.MaBNhan equals cls.MaBNhan
                                      join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                      // join clsct in data.CLScts.Where(p=>p.Status == 1) on cd.IDCD equals clsct.IDCD
                                      join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                      //group new { cls, cd, bn } by new { bn.MaBNhan, cls.MaKP, bn.DTuong, NoiTru = bn.NoiTru, cd.MaDV } into kq
                                      // select new { kq.Key.MaBNhan, kq.Key.MaKP, kq.Key.DTuong, NoiTru = kq.Key.NoiTru, kq.Key.MaDV }).ToList();
                                      select new { bn.MaBNhan, cls.MaKP, bn.DTuong, NoiTru = bn.NoiTru, cd.MaDV, cd.IDCD }).ToList();
                            var q1 = (from a in q0
                                      join idcd in qIDCD on a.IDCD equals idcd
                                      select new { a.MaBNhan, a.MaKP, a.DTuong, NoiTru = a.NoiTru, a.MaDV, a.IDCD }
                                      ).ToList();
                            var q = (from ma in _lkp
                                     join cls in q1 on ma.makp equals cls.MaKP
                                     join dv in qdv on cls.MaDV equals dv.MaDV
                                     select new DV { TenNhomCT = dv.TenNhomCT, TenTN = dv.TenTN, TenRG = dv.TenRG, MaBNhan = cls.MaBNhan, MaKP = cls.MaKP, DTuong = cls.DTuong, NoiTru = cls.NoiTru, TenDV = dv.TenDV }).ToList();

                            lAll.AddRange(q);

                        }
                        if (chkBNkhongCLS.Checked == true)
                        {
                            var qdt = (from dtct in data.DThuoccts.Where(p => p.IDCD == null || p.IDCD <= 0)
                                       join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                       join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                       join nhom in data.NhomDVs.Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh") on dv.IDNhom equals nhom.IDNhom
                                       join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                       join vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay) on dt.MaBNhan equals vp.MaBNhan
                                       join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                       select new DV { TenNhomCT = nhom.TenNhomCT, TenTN = tnhom.TenTN, TenRG = tnhom.TenRG, MaBNhan = dt.MaBNhan, MaKP = dtct.MaKP, DTuong = bn.DTuong, NoiTru = bn.NoiTru, TenDV = dv.TenDV, SoLuong = dtct.SoLuong }).ToList();

                            qdt = (from dt in qdt join ma in _lkp on dt.MaKP equals ma.makp select dt).ToList();
                            lAll.AddRange(qdt);
                        }
                    }

                    #endregion


                    #region đổ dữ liệu ra báo cáo
                    List<string> CDHA = (from a in lAll.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh") group a by a.TenTN into kq select kq.Key).ToList();
                    foreach (var a in CDHA)
                    {
                        CLS moi = new CLS();
                        moi.TenNhomCT = "Chẩn đoán hình ảnh";
                        moi.TenTN = a;
                        moi.DVT = "Lần";
                        moi.TS = lAll.Where(p => p.TenTN == a).Count();
                        moi.BHYT = lAll.Where(p => p.TenTN == a).Where(p => p.DTuong == "BHYT").Count();
                        moi.TP = lAll.Where(p => p.TenTN == a).Where(p => p.DTuong != "BHYT").Count();
                        moi.NoiTru = lAll.Where(p => p.TenTN == a).Where(p => p.NoiTru == 1).Count();
                        moi.NgoaiTru = lAll.Where(p => p.TenTN == a).Where(p => p.NoiTru == 0).Count();
                        _lcls.Add(moi);
                    }

                    CLS moi1 = new CLS();
                    moi1.TenNhomCT = "Truyền máu";
                    moi1.TenTN = "Số ml máu truyền";
                    moi1.DVT = "ml";
                    moi1.TS = lAll.Where(p => p.TenDV.Contains("Truyền máu")).Sum(p => p.SoLuong);
                    moi1.BHYT = lAll.Where(p => p.TenDV.Contains("Truyền máu")).Where(p => p.DTuong == "BHYT").Sum(p => p.SoLuong);
                    moi1.TP = lAll.Where(p => p.TenDV.Contains("Truyền máu")).Where(p => p.DTuong != "BHYT").Sum(p => p.SoLuong);
                    moi1.NoiTru = lAll.Where(p => p.TenDV.Contains("Truyền máu")).Where(p => p.NoiTru == 1).Sum(p => p.SoLuong);
                    moi1.NgoaiTru = lAll.Where(p => p.TenDV.Contains("Truyền máu")).Where(p => p.NoiTru == 0).Sum(p => p.SoLuong);
                    _lcls.Add(moi1);


                    CLS moi2 = new CLS();
                    moi2.TenNhomCT = "Xét nghiệm";
                    moi2.TenTN = "HBsAg test nhanh";
                    moi2.DVT = "Tiêu bản";
                    moi2.TS = lAll.Where(p => p.TenDV.Contains("HBsAg test nhanh")).Count();
                    moi2.BHYT = lAll.Where(p => p.TenDV.Contains("HBsAg test nhanh")).Where(p => p.DTuong == "BHYT").Count();
                    moi2.TP = lAll.Where(p => p.TenDV.Contains("HBsAg test nhanh")).Where(p => p.DTuong != "BHYT").Count();
                    moi2.NoiTru = lAll.Where(p => p.TenDV.Contains("HBsAg test nhanh")).Where(p => p.NoiTru == 1).Count();
                    moi2.NgoaiTru = lAll.Where(p => p.TenDV.Contains("HBsAg test nhanh")).Where(p => p.NoiTru == 0).Count();
                    _lcls.Add(moi2);

                    CLS moi3 = new CLS();
                    moi3.TenNhomCT = "Xét nghiệm";
                    moi3.TenTN = "Xét nghiệm HIV";
                    moi3.DVT = "Tiêu bản";
                    moi3.TS = lAll.Where(p => p.TenDV.Contains("Xét nghiệm HIV")).Count();
                    moi3.BHYT = lAll.Where(p => p.TenDV.Contains("Xét nghiệm HIV")).Where(p => p.DTuong == "BHYT").Count();
                    moi3.TP = lAll.Where(p => p.TenDV.Contains("Xét nghiệm HIV")).Where(p => p.DTuong != "BHYT").Count();
                    moi3.NoiTru = lAll.Where(p => p.TenDV.Contains("Xét nghiệm HIV")).Where(p => p.NoiTru == 1).Count();
                    moi3.NgoaiTru = lAll.Where(p => p.TenDV.Contains("Xét nghiệm HIV")).Where(p => p.NoiTru == 0).Count();
                    _lcls.Add(moi3);

                    var qxnHoaSinh = lAll.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Where(p => !p.TenDV.Contains("HBsAg test nhanh")).Where(p => !p.TenDV.Contains("Xét nghiệm HIV")).Where(p => !p.TenDV.Contains("Truyền máu")).ToList();
                    var qxnhoasinhMauKhac = qxnHoaSinh.Where(p => p.TenDV != "Định lượng HbA1c [Máu]").Where(p => p.TenDV != "Điện giải đồ (Na, K, Cl) [Máu]").ToList();
                    CLS moi4 = new CLS();
                    moi4.TenNhomCT = "Xét nghiệm";
                    moi4.TenTN = "Xét nghiệm hóa sinh máu";
                    moi4.DVT = "Tiêu bản";
                    moi4.TS = qxnhoasinhMauKhac.Count();
                    moi4.BHYT = qxnhoasinhMauKhac.Where(p => p.DTuong == "BHYT").Count();
                    moi4.TP = qxnhoasinhMauKhac.Where(p => p.DTuong != "BHYT").Count();
                    moi4.NoiTru = qxnhoasinhMauKhac.Where(p => p.NoiTru == 1).Count();
                    moi4.NgoaiTru = qxnhoasinhMauKhac.Where(p => p.NoiTru == 0).Count();
                    _lcls.Add(moi4);

                    CLS moi41 = new CLS();
                    moi41.TenNhomCT = "Xét nghiệm";
                    moi41.TenTN = "Xét nghiệm hóa sinh máu";
                    moi41.TenDV = "      - Định lượng HbA1c [Máu]";
                    moi41.DVT = "Tiêu bản";
                    moi41.TS = qxnHoaSinh.Where(p => p.TenDV == "Định lượng HbA1c [Máu]").Count();
                    moi41.BHYT = qxnHoaSinh.Where(p => p.TenDV == "Định lượng HbA1c [Máu]").Where(p => p.DTuong == "BHYT").Count();
                    moi41.TP = qxnHoaSinh.Where(p => p.TenDV == "Định lượng HbA1c [Máu]").Where(p => p.DTuong != "BHYT").Count();
                    moi41.NoiTru = qxnHoaSinh.Where(p => p.TenDV == "Định lượng HbA1c [Máu]").Where(p => p.NoiTru == 1).Count();
                    moi41.NgoaiTru = qxnHoaSinh.Where(p => p.TenDV == "Định lượng HbA1c [Máu]").Where(p => p.NoiTru == 0).Count();
                    _lcls.Add(moi41);

                    CLS moi42 = new CLS();
                    moi42.TenNhomCT = "Xét nghiệm";
                    moi42.TenTN = "Xét nghiệm hóa sinh máu";
                    moi42.TenDV = "      - Điện giải đồ (Na, K, Cl) [Máu]";
                    moi42.DVT = "Tiêu bản";
                    moi42.TS = qxnHoaSinh.Where(p => p.TenDV == "Điện giải đồ (Na, K, Cl) [Máu]").Count();
                    moi42.BHYT = qxnHoaSinh.Where(p => p.TenDV == "Điện giải đồ (Na, K, Cl) [Máu]").Where(p => p.DTuong == "BHYT").Count();
                    moi42.TP = qxnHoaSinh.Where(p => p.TenDV == "Điện giải đồ (Na, K, Cl) [Máu]").Where(p => p.DTuong != "BHYT").Count();
                    moi42.NoiTru = qxnHoaSinh.Where(p => p.TenDV == "Điện giải đồ (Na, K, Cl) [Máu]").Where(p => p.NoiTru == 1).Count();
                    moi42.NgoaiTru = qxnHoaSinh.Where(p => p.TenDV == "Điện giải đồ (Na, K, Cl) [Máu]").Where(p => p.NoiTru == 0).Count();
                    _lcls.Add(moi42);



                    var qxnHuyetHoc = lAll.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Where(p => !p.TenDV.Contains("HBsAg test nhanh")).Where(p => !p.TenDV.Contains("Xét nghiệm HIV")).Where(p => !p.TenDV.Contains("Truyền máu")).ToList();
                    CLS moi5 = new CLS();
                    moi5.TenNhomCT = "Xét nghiệm";
                    moi5.TenTN = "Xét nghiệm huyết học";
                    moi5.DVT = "Tiêu bản";
                    moi5.TS = qxnHuyetHoc.Count();
                    moi5.BHYT = qxnHuyetHoc.Where(p => p.DTuong == "BHYT").Count();
                    moi5.TP = qxnHuyetHoc.Where(p => p.DTuong != "BHYT").Count();
                    moi5.NoiTru = qxnHuyetHoc.Where(p => p.NoiTru == 1).Count();
                    moi5.NgoaiTru = qxnHuyetHoc.Where(p => p.NoiTru == 0).Count();
                    _lcls.Add(moi5);

                    CLS moi6 = new CLS();
                    moi6.TenNhomCT = "Xét nghiệm";
                    moi6.TenTN = "Xét nghiệm nước tiểu";
                    moi6.DVT = "Tiêu bản";
                    moi6.TS = lAll.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi6.BHYT = lAll.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Where(p => p.DTuong == "BHYT").Count();
                    moi6.TP = lAll.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Where(p => p.DTuong != "BHYT").Count();
                    moi6.NoiTru = lAll.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Where(p => p.NoiTru == 1).Count();
                    moi6.NgoaiTru = lAll.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Where(p => p.NoiTru == 0).Count();
                    _lcls.Add(moi6);

                    CLS moi7 = new CLS();
                    var qxnKhac = lAll.Where(p => p.TenNhomCT == "Xét nghiệm").Where(p => p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Where(p => p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Where(p => p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Where(p => !p.TenDV.Contains("HBsAg test nhanh")).Where(p => !p.TenDV.Contains("Xét nghiệm HIV")).Where(p => !p.TenDV.Contains("Truyền máu")).Where(p => !p.TenDV.Contains("AFB trực tiếp nhuộm Ziehl_Neelsen")).Where(p => !p.TenDV.Contains("Xét nghiệm Morphin")).ToList();
                    moi7.TenNhomCT = "Xét nghiệm";
                    moi7.TenTN = "Xét nghiệm khác";
                    moi7.DVT = "Tiêu bản";
                    moi7.TS = qxnKhac.Count();
                    moi7.BHYT = qxnKhac.Where(p => p.DTuong == "BHYT").Count();
                    moi7.TP = qxnKhac.Where(p => p.DTuong != "BHYT").Count();
                    moi7.NoiTru = qxnKhac.Where(p => p.NoiTru == 1).Count();
                    moi7.NgoaiTru = qxnKhac.Where(p => p.NoiTru == 0).Count();
                    _lcls.Add(moi7);

                    CLS moi71 = new CLS();
                    var qxnKhac1 = lAll.Where(p => p.TenDV == "Xét nghiệm Morphin").ToList();//.Where(p => p.TenNhomCT == "Xét nghiệm").Where(p => p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Where(p => p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Where(p => p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Where(p => !p.TenDV.Contains("HBsAg test nhanh")).Where(p => !p.TenDV.Contains("Xét nghiệm HIV")).Where(p => !p.TenDV.Contains("Truyền máu")).ToList();
                    moi71.TenNhomCT = "Xét nghiệm";
                    moi71.TenTN = "Xét nghiệm khác";
                    moi71.TenDV = "      - Xét nghiệm Morphin";
                    moi71.DVT = "Tiêu bản";
                    moi71.TS = qxnKhac1.Count();
                    moi71.BHYT = qxnKhac1.Where(p => p.DTuong == "BHYT").Count();
                    moi71.TP = qxnKhac1.Where(p => p.DTuong != "BHYT").Count();
                    moi71.NoiTru = qxnKhac1.Where(p => p.NoiTru == 1).Count();
                    moi71.NgoaiTru = qxnKhac1.Where(p => p.NoiTru == 0).Count();
                    _lcls.Add(moi71);

                    CLS moi72 = new CLS();
                    var qxnKhac2 = lAll.Where(p => p.TenDV == "AFB trực tiếp nhuộm Ziehl-Neelsen").ToList();
                    moi72.TenNhomCT = "Xét nghiệm";
                    moi72.TenTN = "Xét nghiệm khác";
                    moi72.TenDV = "      - AFB trực tiếp nhuộm Ziehl-Neelsen";
                    moi72.DVT = "Tiêu bản";
                    moi72.TS = qxnKhac2.Count();
                    moi72.BHYT = qxnKhac2.Where(p => p.DTuong == "BHYT").Count();
                    moi72.TP = qxnKhac2.Where(p => p.DTuong != "BHYT").Count();
                    moi72.NoiTru = qxnKhac2.Where(p => p.NoiTru == 1).Count();
                    moi72.NgoaiTru = qxnKhac2.Where(p => p.NoiTru == 0).Count();
                    _lcls.Add(moi72);

                    #endregion
                    BaoCao.Rep_BcHoatDongCLS_TH03_26007 rep = new BaoCao.Rep_BcHoatDongCLS_TH03_26007();
                    rep.TG.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;
                    rep.DataSource = _lcls.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion

                #region bệnh viện khác
                if (grchonmau.SelectedIndex == 0)
                {
                    #region Tạm bỏ
                    //    #region tính theo số bệnh nhân
                    //    if (ckbnguoi.SelectedIndex==0)
                    //    {
                    //        var qlh1 = (from dv in data.DichVus
                    //                    join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                    //                    join nhom in data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                    //                    select new { nhom.TenNhomCT, tnhom.TenTN }).ToList();

                    //        var qlh = (from lh in qlh1.Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh")
                    //                   group lh by new { lh.TenNhomCT, lh.TenTN } into kq
                    //                   select new
                    //                   {
                    //                       kq.Key.TenNhomCT,
                    //                       kq.Key.TenTN,
                    //                   }).ToList();
                    //        if (qlh.Count > 0)
                    //        {
                    //            {
                    //                CLS them1 = new CLS();

                    //                them1.TenNhomCT = "Xét nghiệm";
                    //                // them1.TenTN = "HIV";
                    //                them1.TenTN = "HBsAg test nhanh";
                    //                them1.DVT = "Tiêu bản";
                    //                _lcls.Add(them1);
                    //            }
                    //            {
                    //                CLS them3 = new CLS();

                    //                them3.TenNhomCT = "Xét nghiệm";
                    //                them3.TenTN = "HIV";
                    //                //them1.TenTN = "Viêm gan B (HbsAg)";
                    //                them3.DVT = "Tiêu bản";
                    //                _lcls.Add(them3);
                    //            }
                    //            {
                    //                CLS them2 = new CLS();

                    //                them2.TenNhomCT = "Truyền máu";
                    //                them2.TenTN = "Số ml máu truyền";
                    //                them2.DVT = "ml";
                    //                _lcls.Add(them2);
                    //            }

                    //            foreach (var a in qlh)
                    //            {
                    //                CLS them = new CLS();
                    //                them.TenNhomCT = a.TenNhomCT;
                    //                them.TenTN = a.TenTN;
                    //                if (a.TenNhomCT == "Xét nghiệm")
                    //                {
                    //                    them.DVT = "Tiêu bản";
                    //                }
                    //                if (a.TenNhomCT == "Chẩn đoán hình ảnh")
                    //                {
                    //                    them.DVT = "Lần";
                    //                }
                    //                //if (a.TenNhomCT == "Truyền máu")
                    //                //{
                    //                //    them.DVT = "ml";
                    //                //}
                    //                _lcls.Add(them);

                    //            }
                    //        }
                    //        #region Tất cả BN thực hiện
                    //        //string aa = cboTKBN.EditValue.ToString();
                    //        if (aa == "Tất cả BN thực hiện (Thống kê theo ngày thực hiện)")
                    //        {
                    //            if (chkBNcoCLS.Checked == true)
                    //            {
                    //                var q = (from cls in data.CLS
                    //                         join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                    //                         join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                    //                         join dv in data.DichVus on cd.MaDV equals dv.MaDV
                    //                         join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                    //                         join nhom in data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                    //                         select new { nhom.TenNhomCT, tnhom.TenTN, cls.MaBNhan, cls.MaKP, bn.DTuong, bn.NoiTru, dv.TenDV, cls.NgayTH, }).ToList();
                    //                var qth = (from ma in _lkp
                    //                           join q1 in q.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh").Where(p => p.TenDV != "Xét nghiệm HIV" || p.TenDV != "HBsAg test nhanh" || p.TenDV != "Truyền máu") on ma.makp equals q1.MaKP
                    //                           group q1 by new { q1.TenNhomCT, q1.TenTN } into kq
                    //                           select new
                    //                           {
                    //                               kq.Key.TenNhomCT,
                    //                               kq.Key.TenTN,
                    //                               TS = kq.Select(p => p.MaBNhan).Distinct().Count(),
                    //                               BHYT = kq.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               TP = kq.Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NoiTru = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NgTru = kq.Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count(),

                    //                           }).ToList();
                    //                if (qth.Count() > 0)
                    //                {
                    //                    foreach (var a in _lcls)
                    //                    {
                    //                        foreach (var b in qth)
                    //                        {
                    //                            if (b.TenNhomCT == a.TenNhomCT && a.TenTN.Contains(b.TenTN))
                    //                            {
                    //                                a.TS = a.TS + Convert.ToInt32(b.TS);
                    //                                a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                    //                                a.TP = a.TP + Convert.ToInt32(b.TP);
                    //                                a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                    //                                a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);
                    //                            }
                    //                        }

                    //                    }
                    //                }
                    //                var qdv = (from q1 in q.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh").Where(p => p.TenDV == "Xét nghiệm HIV" || p.TenDV == "Viêm gan B (HbsAg)" || p.TenDV == "Truyền máu")
                    //                           group q1 by new { q1.TenNhomCT, q1.TenDV } into kq
                    //                           select new
                    //                           {
                    //                               kq.Key.TenNhomCT,
                    //                               kq.Key.TenDV,
                    //                               TS = kq.Select(p => p.MaBNhan).Distinct().Count(),
                    //                               BHYT = kq.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               TP = kq.Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NoiTru = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NgTru = kq.Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count(),
                    //                           }).ToList();
                    //                if (qth.Count() > 0)
                    //                {
                    //                    foreach (var a in _lcls)
                    //                    {
                    //                        foreach (var b in qdv)
                    //                        {
                    //                            if (b.TenNhomCT == a.TenNhomCT && b.TenDV.Contains(a.TenTN))
                    //                            {
                    //                                a.TS = a.TS + Convert.ToInt32(b.TS);
                    //                                a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                    //                                a.TP = a.TP + Convert.ToInt32(b.TP);
                    //                                a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                    //                                a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                    //                            }
                    //                        }

                    //                    }
                    //                }

                    //            }
                    //            if (chkBNkhongCLS.Checked == true)
                    //            {
                    //                var qdt = (from dtct in data.DThuoccts
                    //                           join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                    //                           join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                    //                           join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                    //                           join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                    //                           join nhom in data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                    //                           select new { nhom.TenNhomCT, tnhom.TenTN, dt.MaBNhan, dtct.MaKP, dtct.NgayNhap, dtct.IDCD, bn.DTuong, bn.NoiTru, dv.TenDV, }).ToList();
                    //                var qth = (from ma in _lkp
                    //                           join q1 in qdt.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.IDCD == null || p.IDCD <= 0).Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh").Where(p => p.TenDV != "Xét nghiệm HIV" || p.TenDV != "Viêm gan B (HbsAg)" || p.TenDV != "Truyền máu") on ma.makp equals q1.MaKP
                    //                           group q1 by new { q1.TenNhomCT, q1.TenTN } into kq
                    //                           select new
                    //                           {
                    //                               kq.Key.TenNhomCT,
                    //                               kq.Key.TenTN,
                    //                               TS = kq.Select(p => p.MaBNhan).Distinct().Count(),
                    //                               BHYT = kq.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               TP = kq.Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NoiTru = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NgTru = kq.Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count(),

                    //                           }).ToList();
                    //                if (qth.Count() > 0)
                    //                {
                    //                    foreach (var a in _lcls)
                    //                    {
                    //                        foreach (var b in qth)
                    //                        {
                    //                            if (b.TenNhomCT == a.TenNhomCT && a.TenTN.Contains(b.TenTN))
                    //                            {
                    //                                a.TS = a.TS + Convert.ToInt32(b.TS);
                    //                                a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                    //                                a.TP = a.TP + Convert.ToInt32(b.TP);
                    //                                a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                    //                                a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                    //                            }
                    //                        }

                    //                    }
                    //                }
                    //                var qdv = (from q1 in qdt.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh").Where(p => p.TenDV == "Xét nghiệm HIV" || p.TenDV == "Viêm gan B (HbsAg)" || p.TenDV == "Truyền máu")
                    //                           group q1 by new { q1.TenNhomCT, q1.TenDV } into kq
                    //                           select new
                    //                           {
                    //                               kq.Key.TenNhomCT,
                    //                               kq.Key.TenDV,
                    //                               TS = kq.Select(p => p.MaBNhan).Distinct().Count(),
                    //                               BHYT = kq.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               TP = kq.Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NoiTru = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NgTru = kq.Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count(),


                    //                           }).ToList();
                    //                if (qth.Count() > 0)
                    //                {
                    //                    foreach (var a in _lcls)
                    //                    {
                    //                        foreach (var b in qdv)
                    //                        {
                    //                            if (b.TenNhomCT == a.TenNhomCT && b.TenDV.Contains(a.TenTN))
                    //                            {
                    //                                a.TS = a.TS + Convert.ToInt32(b.TS);
                    //                                a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                    //                                a.TP = a.TP + Convert.ToInt32(b.TP);
                    //                                a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                    //                                a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                    //                            }
                    //                        }

                    //                    }
                    //                }
                    //                var qtm1 = (from dt in data.DThuocs
                    //                            join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                    //                            join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                    //                            join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                    //                            select new { bn.DTuong, bn.NoiTru, dtct.SoLuong, dv.TenDV, dtct.NgayNhap }).ToList();
                    //                var qtm = (from tm in qtm1.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.TenDV.Contains("Truyền máu"))
                    //                           group tm by new { tm.TenDV } into kq

                    //                           select new
                    //                           {
                    //                               kq.Key.TenDV,
                    //                               TS = kq.Sum(p => p.SoLuong) == 0 ? null : kq.Sum(p => p.SoLuong).ToString(),
                    //                               BHYT = kq.Where(p => p.DTuong == "BHYT").Sum(p => p.SoLuong) == 0 ? null : kq.Where(p => p.DTuong == "BHYT").Sum(p => p.SoLuong).ToString(),
                    //                               TP = kq.Where(p => p.DTuong == "Dịch vụ").Sum(p => p.SoLuong) == 0 ? null : kq.Where(p => p.DTuong == "Dịch vụ").Sum(p => p.SoLuong).ToString(),
                    //                               NoiTru = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong) == 0 ? null : kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong).ToString(),
                    //                               NgTru = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong) == 0 ? null : kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong).ToString(),

                    //                           }).ToList();
                    //                if (qtm.Count() > 0)
                    //                {
                    //                    foreach (var a in _lcls)
                    //                    {
                    //                        foreach (var b in qtm)
                    //                        {
                    //                            if (b.TenDV.Contains(a.TenNhomCT))
                    //                            {
                    //                                a.TS = a.TS + Convert.ToInt32(b.TS);
                    //                                a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                    //                                a.TP = a.TP + Convert.ToInt32(b.TP);
                    //                                a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                    //                                a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                    //                            }
                    //                        }

                    //                    }
                    //                }
                    //            }
                    //            BaoCao.Rep_BcHoatDongCLS_TH03 rep = new BaoCao.Rep_BcHoatDongCLS_TH03();
                    //            rep.TG.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;
                    //            rep.DataSource = _lcls.ToList();
                    //            rep.BindingData();
                    //            rep.CreateDocument();
                    //            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    //            frm.ShowDialog();
                    //        }
                    //        #endregion
                    //        #region Chỉ BN đã TT
                    //        if (aa == "Chỉ BN đã thanh toán (Thống kê theo ngày thanh toán)")
                    //        {
                    //            if (chkBNcoCLS.Checked == true)
                    //            {
                    //                var q = (from ma in _lkp
                    //                         join cls in data.CLS on ma.makp equals cls.MaKP
                    //                         join vp in data.VienPhis on cls.MaBNhan equals vp.MaBNhan
                    //                         join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                    //                         join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                    //                         join dv in data.DichVus on cd.MaDV equals dv.MaDV
                    //                         join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                    //                         join nhom in data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                    //                         select new { nhom.TenNhomCT, tnhom.TenTN, cls.MaBNhan, cls.MaKP, bn.DTuong, bn.NoiTru, dv.TenDV, cls.NgayTH, vp.NgayTT }).ToList();
                    //                var qth = (from ma in _lkp
                    //                           join q1 in q.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh").Where(p => p.TenDV != "Xét nghiệm HIV" || p.TenDV != "Viêm gan B (HbsAg)" || p.TenDV != "Truyền máu") on ma.makp equals q1.MaKP
                    //                           group q1 by new { q1.TenNhomCT, q1.TenTN } into kq
                    //                           select new
                    //                           {
                    //                               kq.Key.TenNhomCT,
                    //                               kq.Key.TenTN,
                    //                               TS = kq.Select(p => p.MaBNhan).Distinct().Count(),
                    //                               BHYT = kq.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               TP = kq.Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NoiTru = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NgTru = kq.Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count(),

                    //                           }).ToList();
                    //                if (qth.Count() > 0)
                    //                {
                    //                    foreach (var a in _lcls)
                    //                    {
                    //                        foreach (var b in qth)
                    //                        {
                    //                            if (b.TenNhomCT == a.TenNhomCT && a.TenTN.Contains(b.TenTN))
                    //                            {
                    //                                a.TS = a.TS + Convert.ToInt32(b.TS);
                    //                                a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                    //                                a.TP = a.TP + Convert.ToInt32(b.TP);
                    //                                a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                    //                                a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                    //                            }
                    //                        }

                    //                    }
                    //                }
                    //                var qdv = (from q1 in q.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh").Where(p => p.TenDV == "Xét nghiệm HIV" || p.TenDV == "Viêm gan B (HbsAg)" || p.TenDV == "Truyền máu")
                    //                           group q1 by new { q1.TenNhomCT, q1.TenDV } into kq
                    //                           select new
                    //                           {
                    //                               kq.Key.TenNhomCT,
                    //                               kq.Key.TenDV,
                    //                               TS = kq.Select(p => p.MaBNhan).Distinct().Count(),
                    //                               BHYT = kq.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               TP = kq.Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NoiTru = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NgTru = kq.Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count(),


                    //                           }).ToList();
                    //                if (qth.Count() > 0)
                    //                {
                    //                    foreach (var a in _lcls)
                    //                    {
                    //                        foreach (var b in qdv)
                    //                        {
                    //                            if (b.TenNhomCT == a.TenNhomCT && b.TenDV.Contains(a.TenTN))
                    //                            {
                    //                                a.TS = a.TS + Convert.ToInt32(b.TS);
                    //                                a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                    //                                a.TP = a.TP + Convert.ToInt32(b.TP);
                    //                                a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                    //                                a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                    //                            }
                    //                        }

                    //                    }
                    //                }

                    //            }
                    //            if (chkBNkhongCLS.Checked == true)
                    //            {
                    //                var qdt = (from dtct in data.DThuoccts
                    //                           join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                    //                           join vp in data.VienPhis on dt.MaBNhan equals vp.MaBNhan
                    //                           join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                    //                           join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                    //                           join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                    //                           join nhom in data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                    //                           select new { nhom.TenNhomCT, tnhom.TenTN, dt.MaBNhan, dtct.MaKP, dtct.NgayNhap, dtct.IDCD, bn.DTuong, bn.NoiTru, dv.TenDV, vp.NgayTT }).ToList();
                    //                var qth = (from ma in _lkp
                    //                           join q1 in qdt.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).Where(p => p.IDCD == null || p.IDCD <= 0).Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh").Where(p => p.TenDV != "Xét nghiệm HIV" || p.TenDV != "Viêm gan B (HbsAg)" || p.TenDV != "Truyền máu") on ma.makp equals q1.MaKP
                    //                           group q1 by new { q1.TenNhomCT, q1.TenTN } into kq
                    //                           select new
                    //                           {
                    //                               kq.Key.TenNhomCT,
                    //                               kq.Key.TenTN,
                    //                               TS = kq.Select(p => p.MaBNhan).Distinct().Count(),
                    //                               BHYT = kq.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               TP = kq.Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NoiTru = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NgTru = kq.Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count(),

                    //                           }).ToList();
                    //                if (qth.Count() > 0)
                    //                {
                    //                    foreach (var a in _lcls)
                    //                    {
                    //                        foreach (var b in qth)
                    //                        {
                    //                            if (b.TenNhomCT == a.TenNhomCT && a.TenTN.Contains(b.TenTN))
                    //                            {
                    //                                a.TS = a.TS + Convert.ToInt32(b.TS);
                    //                                a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                    //                                a.TP = a.TP + Convert.ToInt32(b.TP);
                    //                                a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                    //                                a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                    //                            }
                    //                        }

                    //                    }
                    //                }
                    //                var qdv = (from q1 in qdt.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh").Where(p => p.TenDV == "Xét nghiệm HIV" || p.TenDV == "Viêm gan B (HbsAg)" || p.TenDV == "Truyền máu")
                    //                           group q1 by new { q1.TenNhomCT, q1.TenDV } into kq
                    //                           select new
                    //                           {
                    //                               kq.Key.TenNhomCT,
                    //                               kq.Key.TenDV,
                    //                               TS = kq.Select(p => p.MaBNhan).Distinct().Count(),
                    //                               BHYT = kq.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               TP = kq.Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NoiTru = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count(),
                    //                               NgTru = kq.Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count(),


                    //                           }).ToList();
                    //                if (qth.Count() > 0)
                    //                {
                    //                    foreach (var a in _lcls)
                    //                    {
                    //                        foreach (var b in qdv)
                    //                        {
                    //                            if (b.TenNhomCT == a.TenNhomCT && b.TenDV.Contains(a.TenTN))
                    //                            {
                    //                                a.TS = a.TS + Convert.ToInt32(b.TS);
                    //                                a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                    //                                a.TP = a.TP + Convert.ToInt32(b.TP);
                    //                                a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                    //                                a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                    //                            }
                    //                        }

                    //                    }
                    //                }
                    //                var qtm1 = (from dt in data.DThuocs
                    //                            join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                    //                            join vp in data.VienPhis on dt.MaBNhan equals vp.MaBNhan
                    //                            join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                    //                            join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                    //                            select new { bn.DTuong, bn.NoiTru, dtct.SoLuong, dv.TenDV, vp.NgayTT }).ToList();
                    //                var qtm = (from tm in qtm1.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).Where(p => p.TenDV.Contains("Truyền máu"))
                    //                           group tm by new { tm.TenDV } into kq

                    //                           select new
                    //                           {
                    //                               kq.Key.TenDV,
                    //                               TS = kq.Sum(p => p.SoLuong) == 0 ? null : kq.Sum(p => p.SoLuong).ToString(),
                    //                               BHYT = kq.Where(p => p.DTuong == "BHYT").Sum(p => p.SoLuong) == 0 ? null : kq.Where(p => p.DTuong == "BHYT").Sum(p => p.SoLuong).ToString(),
                    //                               TP = kq.Where(p => p.DTuong == "Dịch vụ").Sum(p => p.SoLuong) == 0 ? null : kq.Where(p => p.DTuong == "Dịch vụ").Sum(p => p.SoLuong).ToString(),
                    //                               NoiTru = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong) == 0 ? null : kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong).ToString(),
                    //                               NgTru = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong) == 0 ? null : kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong).ToString(),

                    //                           }).ToList();
                    //                if (qtm.Count() > 0)
                    //                {
                    //                    foreach (var a in _lcls)
                    //                    {
                    //                        foreach (var b in qtm)
                    //                        {
                    //                            if (b.TenDV.Contains(a.TenNhomCT))
                    //                            {
                    //                                a.TS = a.TS + Convert.ToInt32(b.TS);
                    //                                a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                    //                                a.TP = a.TP + Convert.ToInt32(b.TP);
                    //                                a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                    //                                a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                    //                            }
                    //                        }

                    //                    }
                    //                }

                    //            }
                    //            BaoCao.Rep_BcHoatDongCLS_TH03 rep = new BaoCao.Rep_BcHoatDongCLS_TH03();
                    //            rep.TG.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;
                    //            rep.DataSource = _lcls.ToList();
                    //            rep.BindingData();
                    //            rep.CreateDocument();
                    //            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    //            frm.ShowDialog();
                    //        }

                    //        #endregion
                    //    }

                    //#endregion
                    #endregion
                    #region tính theo số lượt chỉ định
                    var _ldv = (from dv in data.DichVus
                                join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                join nhom in data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                                select new { tnhom, nhom, dv }).ToList();

                    var _ltn = (from lh in _ldv.Where(p => p.nhom.TenNhomCT == "Xét nghiệm" || p.nhom.TenNhomCT == "Chẩn đoán hình ảnh" || p.nhom.TenNhomCT == "Thăm dò chức năng")
                                group lh by new { lh.nhom.TenNhomCT, lh.tnhom.TenTN } into kq
                                select new
                                {
                                    kq.Key.TenNhomCT,
                                    kq.Key.TenTN,
                                }).ToList();
                    if (_ltn.Count > 0)
                    {
                        {
                            CLS them1 = new CLS();

                            them1.TenNhomCT = "Xét nghiệm";
                            // them1.TenTN = "HIV";
                            them1.TenTN = "HBsAg test nhanh";
                            them1.DVT = "Tiêu bản";
                            _lcls.Add(them1);
                        }
                        {
                            CLS them3 = new CLS();

                            them3.TenNhomCT = "Xét nghiệm";
                            them3.TenTN = "HIV test nhanh";
                            //them1.TenTN = "Viêm gan B (HbsAg)";
                            them3.DVT = "Tiêu bản";
                            _lcls.Add(them3);
                        }
                        {
                            CLS them2 = new CLS();

                            them2.TenNhomCT = "Truyền máu";
                            them2.TenTN = "Số ml máu truyền";
                            them2.DVT = "ml";
                            _lcls.Add(them2);
                        }
                        //{
                        //    CLS them4 = new CLS();

                        //    them4.TenNhomCT = "Chẩn đoán hình ảnh";
                        //    them4.TenTN = "Tống số X-Quang(Lượt chụp)";
                        //    them4.DVT = "Lượt";
                        //    _lcls.Add(them4);
                        //}
                        foreach (var a in _ltn)
                        {
                            CLS them = new CLS();
                            them.TenNhomCT = a.TenNhomCT;
                            them.TenTN = a.TenTN;
                            if (a.TenNhomCT == "Xét nghiệm")
                            {
                                them.DVT = "Tiêu bản";
                            }
                            if (a.TenNhomCT == "Chẩn đoán hình ảnh")
                            {
                                them.DVT = "Lần";
                            }
                            if (a.TenNhomCT == "Thăm dò chức năng")
                            {
                                them.DVT = "Lần";
                            }
                            //if (a.TenNhomCT == "Truyền máu")
                            //{
                            //    them.DVT = "ml";
                            //}
                            _lcls.Add(them);

                        }
                    }
                    #region tất cả bệnh nhân thực hiên
                    if (aa == "Tất cả BN thực hiện (Thống kê theo ngày thực hiện)")
                    {
                        if (chkBNcoCLS.Checked == true)
                        {
                            var _lbn = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                        join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                        join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                        select new { bn.MaBNhan, cls.MaKP, bn.DTuong, bn.NoiTru, cls.NgayTH, cd.MaDV, cd.IDCD }).ToList();

                            var q = (from cls in _lbn
                                     join dv in _ldv on cls.MaDV equals dv.dv.MaDV

                                     //join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                     //join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                     //join nhom in data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                                     select new { dv.nhom.TenNhomCT, dv.tnhom.TenTN, cls.MaBNhan, cls.MaKP, cls.DTuong, cls.NoiTru, TenDV = dv.dv.TenDV.Trim(), dv.tnhom.TenRG, dv.dv.Loai, cls.NgayTH, cls.IDCD }).ToList();
                            var qth = (from ma in _lkp
                                       join q1 in q.Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == "Thăm dò chức năng").Where(p => p.TenDV != "HIV test nhanh" && p.TenDV != "HBsAg test nhanh" && p.TenDV != "Truyền máu") on ma.makp equals q1.MaKP
                                       group new { q1, ma } by new { q1.TenNhomCT, q1.TenTN } into kq
                                       select new
                                       {
                                           kq.Key.TenNhomCT,
                                           kq.Key.TenTN,
                                           TS = kq.Select(p => p.q1.IDCD).Distinct().Count(),
                                           BHYT = kq.Where(p => p.q1.DTuong == "BHYT").Select(p => p.q1.IDCD).Distinct().Count(),
                                           TP = kq.Where(p => p.q1.DTuong == "Dịch vụ").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NoiTru = kq.Where(p => p.ma.ploai == "Lâm sàng").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NgTru = kq.Where(p => p.ma.ploai == "Phòng khám").Select(p => p.q1.IDCD).Distinct().Count(),

                                       }).ToList();
                            if (qth.Count() > 0)
                            {
                                foreach (var a in _lcls)
                                {
                                    foreach (var b in qth)
                                    {
                                        if (b.TenNhomCT == a.TenNhomCT && a.TenTN == b.TenTN)
                                        {
                                            a.TS = a.TS + Convert.ToInt32(b.TS);
                                            a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                                            a.TP = a.TP + Convert.ToInt32(b.TP);
                                            a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                                            a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);
                                        }
                                    }

                                }
                            }
                            //var _lxquang = (from ma in _lkp
                            //                join dv in q.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh" && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang) on ma.makp equals dv.MaKP
                            //                group new { dv, ma } by new { dv.TenNhomCT, dv.TenRG } into kq
                            //                select new CLS
                            //                {
                            //                    TenNhomCT = "Tống số X-Quang(Lượt chụp)",
                            //                    //TenTN = "Tống số X-Quang(Lượt chụp)",
                            //                    DVT = "Lượt",
                            //                    TS = Convert.ToDouble(kq.Sum(p => p.dv.Loai)),
                            //                    BHYT = Convert.ToDouble(kq.Where(p => p.dv.DTuong == "BHYT").Sum(p => p.dv.Loai)),
                            //                    TP = Convert.ToDouble(kq.Where(p => p.dv.DTuong == "Dịch vụ").Sum(p => p.dv.Loai)),
                            //                    NoiTru = Convert.ToDouble(kq.Where(p => p.ma.ploai == "Lâm sàng").Sum(p => p.dv.Loai)),
                            //                    NgoaiTru = Convert.ToDouble(kq.Where(p => p.ma.ploai == "Phòng khám").Sum(p => p.dv.Loai))
                            //                }).ToList();
                            //_lcls.AddRange(_lxquang);

                            var qdv = (from q1 in q.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == "Thăm dò chức năng").Where(p => p.TenDV == "HIV test nhanh" || p.TenDV == "HBsAg test nhanh" || p.TenDV == "Truyền máu")
                                       join ma in _lkp on q1.MaKP equals ma.makp
                                       group new { q1, ma } by new { q1.TenNhomCT, q1.TenDV, q1.MaKP, ma.ploai } into kq
                                       select new
                                       {
                                           kq.Key.TenNhomCT,
                                           kq.Key.TenDV,
                                           TS = kq.Select(p => p.q1.IDCD).Distinct().Count(),
                                           BHYT = kq.Where(p => p.q1.DTuong == "BHYT").Select(p => p.q1.IDCD).Distinct().Count(),
                                           TP = kq.Where(p => p.q1.DTuong == "Dịch vụ").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NoiTru = kq.Where(p => p.ma.ploai == "Lâm sàng").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NgTru = kq.Where(p => p.ma.ploai == "Phòng khám").Select(p => p.q1.IDCD).Distinct().Count(),
                                       }).ToList();
                            if (qth.Count() > 0)
                            {
                                foreach (var a in _lcls)
                                {
                                    foreach (var b in qdv)
                                    {
                                        if (b.TenNhomCT == a.TenNhomCT && b.TenDV == a.TenTN)
                                        {
                                            a.TS = a.TS + Convert.ToInt32(b.TS);
                                            a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                                            a.TP = a.TP + Convert.ToInt32(b.TP);
                                            a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                                            a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                                        }
                                    }

                                }
                            }

                        }
                        if (chkBNkhongCLS.Checked == true)
                        {
                            var _ldt = (from dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.IDCD > 0)
                                        join dt in data.DThuocs.Where(p => p.PLDV == 2) on dtct.IDDon equals dt.IDDon
                                        join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                        select new { dt.MaBNhan, dtct.MaKP, dtct.NgayNhap, dtct.IDCD, bn.DTuong, bn.NoiTru, dtct.MaDV, dtct.SoLuong }).ToList();
                            var qdt = (from dt in _ldt
                                       //from dtct in data.DThuoccts
                                       //       join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                       //       join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                       join dv in _ldv on dt.MaDV equals dv.dv.MaDV
                                       //join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                       //join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                       //join nhom in data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                                       select new { dv.nhom.TenNhomCT, dv.tnhom.TenTN, dt.MaBNhan, dt.MaKP, dt.NgayNhap, dt.IDCD, dt.DTuong, dt.NoiTru, dv.dv.TenDV, }).ToList();
                            var qth = (from ma in _lkp
                                       join q1 in qdt.Where(p => p.IDCD == null || p.IDCD <= 0).Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == "Thăm dò chức năng").Where(p => p.TenDV != "HIV test nhanh" && p.TenDV != "HBsAg test nhanh" && p.TenDV != "Truyền máu") on ma.makp equals q1.MaKP
                                       group new { q1, ma } by new { q1.TenNhomCT, q1.TenTN, q1.MaKP, ma.ploai } into kq
                                       select new
                                       {
                                           kq.Key.TenNhomCT,
                                           kq.Key.TenTN,
                                           TS = kq.Select(p => p.q1.IDCD).Distinct().Count(),
                                           BHYT = kq.Where(p => p.q1.DTuong == "BHYT").Select(p => p.q1.IDCD).Distinct().Count(),
                                           TP = kq.Where(p => p.q1.DTuong == "Dịch vụ").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NoiTru = kq.Where(p => p.ma.ploai == "Lâm sàng").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NgTru = kq.Where(p => p.ma.ploai == "Phòng khám").Select(p => p.q1.IDCD).Distinct().Count(),

                                       }).ToList();
                            if (qth.Count() > 0)
                            {
                                foreach (var a in _lcls)
                                {
                                    foreach (var b in qth)
                                    {
                                        if (b.TenNhomCT == a.TenNhomCT && a.TenTN == b.TenTN)
                                        {
                                            a.TS = a.TS + Convert.ToInt32(b.TS);
                                            a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                                            a.TP = a.TP + Convert.ToInt32(b.TP);
                                            a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                                            a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                                        }
                                    }

                                }
                            }
                            var qdv = (from q1 in qdt.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == "Thăm dò chức năng").Where(p => p.TenDV == "HIV test nhanh" || p.TenDV == "HBsAg test nhanh")
                                       join ma in _lkp on q1.MaKP equals ma.makp
                                       group new { ma, q1 } by new { q1.TenNhomCT, q1.TenDV, q1.MaKP, ma.ploai } into kq
                                       select new
                                       {
                                           kq.Key.TenNhomCT,
                                           kq.Key.TenDV,
                                           TS = kq.Select(p => p.q1.IDCD).Distinct().Count(),
                                           BHYT = kq.Where(p => p.q1.DTuong == "BHYT").Select(p => p.q1.IDCD).Distinct().Count(),
                                           TP = kq.Where(p => p.q1.DTuong == "Dịch vụ").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NoiTru = kq.Where(p => p.ma.ploai == "Lâm sàng").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NgTru = kq.Where(p => p.ma.ploai == "Phòng khám").Select(p => p.q1.IDCD).Distinct().Count(),


                                       }).ToList();
                            if (qth.Count() > 0)
                            {
                                foreach (var a in _lcls)
                                {
                                    foreach (var b in qdv)
                                    {
                                        if (b.TenNhomCT == a.TenNhomCT && b.TenDV == a.TenTN)
                                        {
                                            a.TS = a.TS + Convert.ToInt32(b.TS);
                                            a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                                            a.TP = a.TP + Convert.ToInt32(b.TP);
                                            a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                                            a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                                        }
                                    }

                                }
                            }
                            var qtm1 = (from dt in _ldt
                                        join dv in data.DichVus on dt.MaDV equals dv.MaDV
                                        select new { dt.DTuong, dt.NoiTru, dt.SoLuong, dv.TenDV, dt.NgayNhap, dt.MaKP }).ToList();

                            var qtm = (from tm in qtm1.Where(p => p.TenDV.Contains("Truyền máu"))
                                       join ma in _lkp on tm.MaKP equals ma.makp
                                       group new { ma, tm } by new { tm.TenDV, tm.MaKP, ma.ploai } into kq

                                       select new
                                       {
                                           kq.Key.TenDV,
                                           TS = kq.Sum(p => p.tm.SoLuong) == 0 ? null : kq.Sum(p => p.tm.SoLuong).ToString(),
                                           BHYT = kq.Where(p => p.tm.DTuong == "BHYT").Sum(p => p.tm.SoLuong) == 0 ? null : kq.Where(p => p.tm.DTuong == "BHYT").Sum(p => p.tm.SoLuong).ToString(),
                                           TP = kq.Where(p => p.tm.DTuong == "Dịch vụ").Sum(p => p.tm.SoLuong) == 0 ? null : kq.Where(p => p.tm.DTuong == "Dịch vụ").Sum(p => p.tm.SoLuong).ToString(),
                                           NoiTru = kq.Where(p => p.ma.ploai == "Lâm sàng").Sum(p => p.tm.SoLuong) == 0 ? null : kq.Where(p => p.ma.ploai == "Lâm sàng").Sum(p => p.tm.SoLuong).ToString(),
                                           NgTru = kq.Where(p => p.ma.ploai == "Phòng khám").Sum(p => p.tm.SoLuong) == 0 ? null : kq.Where(p => p.ma.ploai == "Phòng khám").Sum(p => p.tm.SoLuong).ToString(),

                                       }).ToList();
                            if (qtm.Count() > 0)
                            {
                                foreach (var a in _lcls)
                                {
                                    foreach (var b in qtm)
                                    {
                                        if (b.TenDV == a.TenNhomCT)
                                        {
                                            a.TS = a.TS + Convert.ToInt32(b.TS);
                                            a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                                            a.TP = a.TP + Convert.ToInt32(b.TP);
                                            a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                                            a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                                        }
                                    }

                                }
                            }
                        }
                        BaoCao.Rep_BcHoatDongCLS_TH03 rep = new BaoCao.Rep_BcHoatDongCLS_TH03();
                        rep.TG.Value = "Từ ngày " + lupTuNgay.Text + " Ðến ngày " + lupDenNgay.Text;
                        rep.DataSource = _lcls.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    #endregion
                    #region Chỉ BN dã TT
                    if (aa == "Chỉ BN đã thanh toán (Thống kê theo ngày thanh toán)")
                    {
                        if (chkBNcoCLS.Checked == true)
                        {
                            var _lbn = (from cls in data.CLS
                                        join vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay) on cls.MaBNhan equals vp.MaBNhan
                                        join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                        join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                        select new
                                        {
                                            cls.MaBNhan,
                                            cls.MaKP,
                                            bn.DTuong,
                                            bn.NoiTru,
                                            cls.NgayTH,
                                            vp.NgayTT,
                                            cd.MaDV,
                                            cd.IDCD
                                        }).ToList();
                            var q = (from ma in _lkp
                                     //join cls in data.CLS on ma.makp equals cls.MaKP
                                     //join vp in data.VienPhis on cls.MaBNhan equals vp.MaBNhan
                                     //join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                     //join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                     //join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                     //join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                     //join nhom in data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                                     join bn in _lbn on ma.makp equals bn.MaKP
                                     join dv in _ldv on bn.MaDV equals dv.dv.MaDV
                                     select new { dv.nhom.TenNhomCT, dv.tnhom.TenTN, bn.MaBNhan, bn.MaKP, bn.DTuong, bn.NoiTru, dv.dv.TenDV, bn.NgayTH, bn.NgayTT, bn.IDCD }).ToList();
                            var qth = (from ma in _lkp
                                       join q1 in q.Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == "Thăm dò chức năng").Where(p => DungChung.Bien.MaBV == "30004" ? (p.TenDV != "HIV test nhanh" || p.TenDV != "HBsAg test nhanh" || p.TenDV != "Truyền máu") : (p.TenDV != "Xét nghiệm HIV" || p.TenDV != "Viêm gan B (HbsAg)" || p.TenDV != "Truyền máu")) on ma.makp equals q1.MaKP
                                       group new { q1, ma } by new { q1.TenNhomCT, q1.TenTN, q1.MaKP, ma.ploai } into kq
                                       select new
                                       {
                                           kq.Key.TenNhomCT,
                                           kq.Key.TenTN,
                                           TS = kq.Select(p => p.q1.IDCD).Distinct().Count(),
                                           BHYT = kq.Where(p => p.q1.DTuong == "BHYT").Select(p => p.q1.IDCD).Distinct().Count(),
                                           TP = kq.Where(p => p.q1.DTuong == "Dịch vụ").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NoiTru = kq.Where(p => p.ma.ploai == "Lâm sàng").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NgTru = kq.Where(p => p.ma.ploai == "Phòng khám").Select(p => p.q1.IDCD).Distinct().Count(),

                                       }).ToList();
                            if (qth.Count() > 0)
                            {
                                foreach (var a in _lcls)
                                {
                                    foreach (var b in qth)
                                    {
                                        if (b.TenNhomCT == a.TenNhomCT && a.TenTN == b.TenTN)
                                        {
                                            a.TS = a.TS + Convert.ToInt32(b.TS);
                                            a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                                            a.TP = a.TP + Convert.ToInt32(b.TP);
                                            a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                                            a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                                        }
                                    }

                                }
                            }
                            var qdv = (from q1 in q.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == "Thăm dò chức năng").Where(p => DungChung.Bien.MaBV == "30004" ? (p.TenDV == "HIV test nhanh" || p.TenDV == "HBsAg test nhanh") : (p.TenDV == "Xét nghiệm HIV" || p.TenDV == "Viêm gan B (HbsAg)"))
                                       join ma in _lkp on q1.MaKP equals ma.makp
                                       group new { q1, ma } by new { q1.TenNhomCT, q1.TenDV, q1.MaKP, ma.ploai } into kq
                                       select new
                                       {
                                           kq.Key.TenNhomCT,
                                           kq.Key.TenDV,
                                           TS = kq.Select(p => p.q1.IDCD).Distinct().Count(),
                                           BHYT = kq.Where(p => p.q1.DTuong == "BHYT").Select(p => p.q1.IDCD).Distinct().Count(),
                                           TP = kq.Where(p => p.q1.DTuong == "Dịch vụ").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NoiTru = kq.Where(p => p.ma.ploai == "Lâm sàng").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NgTru = kq.Where(p => p.ma.ploai == "Phòng khám").Select(p => p.q1.IDCD).Distinct().Count(),


                                       }).ToList();
                            if (qth.Count() > 0)
                            {
                                foreach (var a in _lcls)
                                {
                                    foreach (var b in qdv)
                                    {
                                        if (b.TenNhomCT == a.TenNhomCT && b.TenDV.Contains(a.TenTN))
                                        {
                                            a.TS = a.TS + Convert.ToInt32(b.TS);
                                            a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                                            a.TP = a.TP + Convert.ToInt32(b.TP);
                                            a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                                            a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                                        }
                                    }

                                }
                            }

                        }
                        if (chkBNkhongCLS.Checked == true)
                        {
                            var _ldt = (from dtct in data.DThuoccts.Where(p => p.IDCD > 0)
                                        join dt in data.DThuocs.Where(p => p.PLDV == 2) on dtct.IDDon equals dt.IDDon
                                        join vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay) on dt.MaBNhan equals vp.MaBNhan
                                        join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                        select new { dt.MaBNhan, dtct.MaKP, dtct.NgayNhap, dtct.IDCD, bn.DTuong, bn.NoiTru, vp.NgayTT, dtct.MaDV, dtct.SoLuong }).ToList();
                            var qdt = (from dtct in _ldt
                                       join dv in _ldv on dtct.MaDV equals dv.dv.MaDV
                                       //    data.DThuoccts
                                       //join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                       //join vp in data.VienPhis on dt.MaBNhan equals vp.MaBNhan
                                       //join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                       //join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                       //join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                       //join nhom in data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                                       select new { dv.nhom.TenNhomCT, dv.tnhom.TenTN, dtct.MaBNhan, dtct.MaKP, dtct.NgayNhap, dtct.IDCD, dtct.DTuong, dtct.NoiTru, dv.dv.TenDV, dtct.NgayTT }).ToList();
                            var qth = (from ma in _lkp
                                       join q1 in qdt.Where(p => p.IDCD == null || p.IDCD <= 0).Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == "Thăm dò chức năng").Where(p => DungChung.Bien.MaBV == "30004" ? (p.TenDV != "HIV test nhanh" || p.TenDV != "HBsAg test nhanh" || p.TenDV != "Truyền máu") : (p.TenDV != "Xét nghiệm HIV" || p.TenDV != "Viêm gan B (HbsAg)" || p.TenDV != "Truyền máu")) on ma.makp equals q1.MaKP
                                       group new { q1, ma } by new { q1.TenNhomCT, q1.TenTN, q1.MaKP, ma.ploai } into kq
                                       select new
                                       {
                                           kq.Key.TenNhomCT,
                                           kq.Key.TenTN,
                                           TS = kq.Select(p => p.q1.IDCD).Distinct().Count(),
                                           BHYT = kq.Where(p => p.q1.DTuong == "BHYT").Select(p => p.q1.IDCD).Distinct().Count(),
                                           TP = kq.Where(p => p.q1.DTuong == "Dịch vụ").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NoiTru = kq.Where(p => p.ma.ploai == "Lâm sàng").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NgTru = kq.Where(p => p.ma.ploai == "Phòng khám").Select(p => p.q1.IDCD).Distinct().Count(),

                                       }).ToList();
                            if (qth.Count() > 0)
                            {
                                foreach (var a in _lcls)
                                {
                                    foreach (var b in qth)
                                    {
                                        if (b.TenNhomCT == a.TenNhomCT && a.TenTN == b.TenTN)
                                        {
                                            a.TS = a.TS + Convert.ToInt32(b.TS);
                                            a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                                            a.TP = a.TP + Convert.ToInt32(b.TP);
                                            a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                                            a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                                        }
                                    }

                                }
                            }
                            var qdv = (from q1 in qdt.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == "Thăm dò chức năng").Where(p => p.TenDV == "HIV test nhanh" || p.TenDV == "HBsAg test nhanh")
                                       join ma in _Kphong on q1.MaKP equals ma.makp
                                       group new { q1, ma } by new { q1.TenNhomCT, q1.TenDV, q1.MaKP, ma.ploai } into kq
                                       select new
                                       {
                                           kq.Key.TenNhomCT,
                                           kq.Key.TenDV,
                                           TS = kq.Select(p => p.q1.IDCD).Distinct().Count(),
                                           BHYT = kq.Where(p => p.q1.DTuong == "BHYT").Select(p => p.q1.IDCD).Distinct().Count(),
                                           TP = kq.Where(p => p.q1.DTuong == "Dịch vụ").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NoiTru = kq.Where(p => p.ma.ploai == "Lâm sàng").Select(p => p.q1.IDCD).Distinct().Count(),
                                           NgTru = kq.Where(p => p.ma.ploai == "Phòng khám").Select(p => p.q1.IDCD).Distinct().Count(),


                                       }).ToList();
                            if (qth.Count() > 0)
                            {
                                foreach (var a in _lcls)
                                {
                                    foreach (var b in qdv)
                                    {
                                        if (b.TenNhomCT == a.TenNhomCT && b.TenDV == a.TenTN)
                                        {
                                            a.TS = a.TS + Convert.ToInt32(b.TS);
                                            a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                                            a.TP = a.TP + Convert.ToInt32(b.TP);
                                            a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                                            a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                                        }
                                    }

                                }
                            }
                            var qtm1 = (from dt in _ldt
                                        //data.DThuocs
                                        //join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                                        //join vp in data.VienPhis on dt.MaBNhan equals vp.MaBNhan
                                        //join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                        join dv in data.DichVus on dt.MaDV equals dv.MaDV
                                        select new { dt.DTuong, dt.NoiTru, dt.SoLuong, dv.TenDV, dt.NgayTT, dt.MaKP }).ToList();
                            var qtm = (from tm in qtm1.Where(p => p.TenDV.Contains("Truyền máu"))
                                       join ma in _lkp on tm.MaKP equals ma.makp
                                       group new { tm, ma } by new { tm.TenDV, tm.MaKP, ma.ploai } into kq

                                       select new
                                       {
                                           kq.Key.TenDV,
                                           TS = kq.Sum(p => p.tm.SoLuong) == 0 ? null : kq.Sum(p => p.tm.SoLuong).ToString(),
                                           BHYT = kq.Where(p => p.tm.DTuong == "BHYT").Sum(p => p.tm.SoLuong) == 0 ? null : kq.Where(p => p.tm.DTuong == "BHYT").Sum(p => p.tm.SoLuong).ToString(),
                                           TP = kq.Where(p => p.tm.DTuong == "Dịch vụ").Sum(p => p.tm.SoLuong) == 0 ? null : kq.Where(p => p.tm.DTuong == "Dịch vụ").Sum(p => p.tm.SoLuong).ToString(),
                                           NoiTru = kq.Where(p => p.ma.ploai == "Lâm sàng").Sum(p => p.tm.SoLuong) == 0 ? null : kq.Where(p => p.ma.ploai == "Lâm sàng").Sum(p => p.tm.SoLuong).ToString(),
                                           NgTru = kq.Where(p => p.ma.ploai == "Phòng khám").Sum(p => p.tm.SoLuong) == 0 ? null : kq.Where(p => p.ma.ploai == "Phòng khám").Sum(p => p.tm.SoLuong).ToString(),

                                       }).ToList();
                            if (qtm.Count() > 0)
                            {
                                foreach (var a in _lcls)
                                {
                                    foreach (var b in qtm)
                                    {
                                        if (b.TenDV == a.TenNhomCT)
                                        {
                                            a.TS = a.TS + Convert.ToInt32(b.TS);
                                            a.BHYT = a.BHYT + Convert.ToInt32(b.BHYT);
                                            a.TP = a.TP + Convert.ToInt32(b.TP);
                                            a.NoiTru = a.NoiTru + Convert.ToInt32(b.NoiTru);
                                            a.NgoaiTru = a.NgoaiTru + Convert.ToInt32(b.NgTru);

                                        }
                                    }

                                }
                            }

                        }
                        BaoCao.Rep_BcHoatDongCLS_TH03 rep = new BaoCao.Rep_BcHoatDongCLS_TH03();
                        rep.TG.Value = "Từ ngày " + lupTuNgay.Text + " Ðến ngày " + lupDenNgay.Text;
                        rep.DataSource = _lcls.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    #endregion
                }
                    #endregion

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

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void ckbnguoi_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckbluot_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}