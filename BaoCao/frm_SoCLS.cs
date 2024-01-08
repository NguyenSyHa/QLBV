using QLBV.Models.Business.SoCLS;
using QLBV.Providers.StoredProcedure;
using QLBV_Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.BaoCao
{
    public partial class frm_SoCLS : Form
    {
        private ExcuteStoredProcedureProvider _excuteStoredProcedureProvider;
        public frm_SoCLS()
        {
            _excuteStoredProcedureProvider = new ExcuteStoredProcedureProvider();
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        #region Class
        class BCCLS
        {
            public string TenBNhan { get; set; }
            public int GTinh { get; set; }
            public int Tuoi { get; set; }
            public string DChi { get; set; }
            public string DTuong { get; set; }
            public int MaKP { get; set; }
            public string MaCB { get; set; }
            public string MaCBth { get; set; }
            public string RBC { get; set; }
            public string HGB { get; set; }
            public string HCT { get; set; }
            public string MCV { get; set; }
            public string MCH { get; set; }
            public string MCHC { get; set; }
            public string HCconhan { get; set; }
            public string HCluoi { get; set; }
            public string RDWCV { get; set; }
            public string RDWSD { get; set; }
            public string Tebaobatthuong { get; set; }
            public string WBC { get; set; }
            public string NEUT { get; set; }
            public string NEUTP { get; set; }
            public string LYMP { get; set; }
            public string LYMPP { get; set; }
            public string MONO { get; set; }
            public string MONOP { get; set; }
            public string EO { get; set; }
            public string EOP { get; set; }
            public string BSAO { get; set; }
            public string BSAOP { get; set; }
            public string PLT { get; set; }
            public string PDW { get; set; }
            public string MPV { get; set; }
            public string PCT { get; set; }
            public string Maulang1h { get; set; }
            public string Maulang2h { get; set; }
            public string Thoigianmauchay { get; set; }
            public string Thoigianmaudong { get; set; }
            public string NhommauABO { get; set; }
            public string NhommauRH { get; set; }
            public string TrongDM { get; set; }
        }
        class BCCLSHS
        {
            public string TenBNhan { get; set; }
            public int GTinh { get; set; }
            public int Tuoi { get; set; }
            public string DChi { get; set; }
            public string DTuong { get; set; }
            public int MaKP { get; set; }
            public string MaCB { get; set; }
            public string MaCBth { get; set; }
            public string Glucose { get; set; }
            public string Urea { get; set; }
            public string Creatinin { get; set; }
            public string CholesterolTP { get; set; }
            public string Triglycorid { get; set; }
            public string HDLC { get; set; }
            public string LDLC { get; set; }
            public string ASTGOT { get; set; }
            public string ALTGPT { get; set; }
            public string GGT { get; set; }
            public string AcidUric { get; set; }
            public string BilirubinTP { get; set; }
            public string BilirubinTT { get; set; }
            public string BilirubinGT { get; set; }
            public string ProteinTP { get; set; }
            public string Albumin { get; set; }
            public string Amylase { get; set; }
            public string Fe { get; set; }
            public string CalciTP { get; set; }
            public string CRPCReactiveProtein { get; set; }
            public string HbA1c { get; set; }
            public string Na { get; set; }
            public string K { get; set; }
            public string CL { get; set; }
            public string TrongDM { get; set; }
        }
        class BCCLSXQ
        {
            public string TenBNhan { get; set; }
            public int GTinh { get; set; }
            public int Tuoi { get; set; }
            public string DChi { get; set; }
            public string DTuong { get; set; }
            public string ChanDoan { get; set; }
            public string KetQua { get; set; }
            public int MaKP { get; set; }
            public string MaCB { get; set; }
            public string MaCBth { get; set; }
            public string SoPhieu { get; set; }
            public string TrongDM { get; set; }
        }
        class NoiSoi
        {
            public string TenBNhan { get; set; }
            public int GTinh { get; set; }
            public int Tuoi { get; set; }
            public string DChi { get; set; }
            public string DTuong { get; set; }
            public string ChanDoan { get; set; }
            public string KQNS { get; set; }
            public int MaKP { get; set; }
            public string MaCB { get; set; }
            public string MaCBth { get; set; }
            public string TrongDM { get; set; }
        }
        private class c_KPhong
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
        private class TieuNhom
        {
            private string TenTN;
            private int MaTN;
            private bool Chon;
            public string tentn
            { set { TenTN = value; } get { return TenTN; } }
            public int matn
            { set { MaTN = value; } get { return MaTN; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        public class KP
        {
            private int makp;
            private string tenkp;
            public int MaKP
            {
                set { makp = value; }
                get { return makp; }
            }
            public string TenKP
            {
                set { tenkp = value; }
                get { return tenkp; }
            }
        }
        public class TN
        {
            private int matn;
            private string tentn;
            public int MaTN
            {
                set { matn = value; }
                get { return matn; }
            }
            public string TenTN
            {
                set { tentn = value; }
                get { return tentn; }
            }
        }
        List<c_KPhong> _Kphong = new List<c_KPhong>();
        List<TieuNhom> _Tnhom = new List<TieuNhom>();
        List<KP> _lkp = new List<KP>();
        List<TN> _tn = new List<TN>();
        List<TieuNhomDV> _ltnhom = new List<TieuNhomDV>();
        List<BCCLS> bchh = new List<BCCLS>();
        List<BCCLSHS> bchhsh = new List<BCCLSHS>();
        List<BCCLSXQ> cdhd = new List<BCCLSXQ>();
        List<NoiSoi> ns = new List<NoiSoi>();
        #endregion
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_SoCLS_Load(object sender, EventArgs e)
        {
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _Kphong.Clear();
            _Tnhom.Clear();
            lupNgaytu.DateTime = System.DateTime.Now.Date;
            lupNgayden.DateTime = System.DateTime.Now.Date;
            #region Load KP
            var kphong = (from kp in _Data.KPhongs.Where(p => p.PLoai.Contains("Lâm sàng") || p.PLoai.Contains("Khám bệnh"))
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                c_KPhong themmoi1 = new c_KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {

                    c_KPhong themmoi = new c_KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
            #endregion
            #region Load TN
            var _ltn = (from tn in _Data.TieuNhomDVs
                        where tn.IDNhom == 1 || tn.IDNhom == 2 || tn.TenTN.Contains("nội soi") || tn.TenTN.Contains("Điện Tim")
                        select new { tn.TenTN, tn.IdTieuNhom }).ToList();
            if (_ltn.Count > 0)
            {
                TieuNhom themmoi1 = new TieuNhom();
                themmoi1.tentn = "Chọn tất cả";
                themmoi1.matn = 0;
                themmoi1.chon = true;
                _Tnhom.Add(themmoi1);
                foreach (var a in _ltn)
                {

                    TieuNhom themmoi = new TieuNhom();
                    themmoi.tentn = a.TenTN;
                    themmoi.matn = (int)a.IdTieuNhom;
                    themmoi.chon = true;
                    _Tnhom.Add(themmoi);
                }
                grcTieuNhom.DataSource = _Tnhom.ToList();
            }
            #endregion
        }
        private void butTaoBC_Click(object sender, EventArgs e)
        {
            DungChung.Ham.CallProcessWaitingForm(TaoBaoCao, "Đang tạo sổ", "Xin chờ trong giây lát");
        }
        private void TaoBaoCao()
        {
            string chuoi = "";
            string tenkp = "";
            string id = "";
            string tentn = "";
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            string dtuong = "";
            int trongDM = 0;
            if (radTrongDM.SelectedIndex == 1)
            {
                trongDM = 1;
            }
            if (radTrongDM.SelectedIndex == 2)
            {
                trongDM = 2;
            }
            frmIn frm = new frmIn();
            #region Chọn đối tượng
            if (cbx_Dtuong.SelectedIndex != -1)
            {
                switch (cbx_Dtuong.SelectedIndex)
                {
                    case 0:
                        dtuong = "0";
                        break;
                    case 1:
                        dtuong = "BHYT";
                        break;
                    case 2:
                        dtuong = "Dịch vụ";
                        break;
                    case 3:
                        dtuong = "KSK";
                        break;
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn đối tượng!!");
                return;
            }
            #endregion
            #region chọn trong DM|ngoài DM
            //if (radTrongDM.SelectedIndex != -1)
            //{
            //    switch (radTrongDM.SelectedIndex)
            //    {
            //        case 0:
            //            trongDM = "Ngoài DM";
            //            break;
            //        case 1:
            //            trongDM = "Trong DM";
            //            break;
            //        case 2:
            //            trongDM = "Tất cả";
            //            break;
            //    }
            //}
            #endregion
            #region Chọn KP
            _lkp.Clear();
            for (int k = 0; k < grvKhoaphong.RowCount; k++)
            {
                if (grvKhoaphong.GetRowCellValue(k, Chọn).ToString().ToLower() == "true")
                {
                    _lkp.Add(new KP { MaKP = grvKhoaphong.GetRowCellValue(k, MaKP) == null ? 0 : Convert.ToInt32(grvKhoaphong.GetRowCellValue(k, MaKP)) });
                }
            }
            if (_Kphong.Count == _lkp.Count)
            {
                chuoi = "0";
                tenkp = "Tất cả";
            }
            else
            {
                foreach (var item in _lkp)
                {
                    chuoi += item.MaKP.ToString() + ";";
                    tenkp += item.TenKP + " - ";
                }
            }
            #endregion
            #region Chọn TN
            _tn.Clear();
            for (int k = 0; k < grvTieuNhom.RowCount; k++)
            {
                if (grvTieuNhom.GetRowCellValue(k, Chon).ToString().ToLower() == "true")
                {
                    _tn.Add(new TN { MaTN = grvTieuNhom.GetRowCellValue(k, MaTN) == null ? 0 : Convert.ToInt32(grvTieuNhom.GetRowCellValue(k, MaTN)) });
                }
            }
            if (_Tnhom.Count == _tn.Count)
            {
                id = "0";
            }
            else
            {
                foreach (var item in _tn)
                {
                    id += item.MaTN.ToString() + ";";
                    var tenn = _Data.TieuNhomDVs.Where(p => p.IdTieuNhom == item.MaTN).ToList();
                    tentn += tenn.First().TenTN + ";";
                }
            }
            #endregion
            #region Lấy dữ liệu
            string spName = "sp_SoCLS";
            Dictionary<string, string> para = new Dictionary<string, string>()
            {
                {"@tungay",ngaytu.Date.ToString("yyyy/MM/dd")},
                {"@denngay",ngayden.Date.ToString("yyyy/MM/dd")},
                {"@trongDM", trongDM.ToString() },
                {"@dtuong",dtuong},
                {@"makp",chuoi},
                {@"idtn",id},
            };
            var lstBC = _excuteStoredProcedureProvider.ExcuteStoredProcedure<SoCLSModel>(spName, para);
            var _lstbc = (from a in lstBC
                          group new { a } by new { a.TenBNhan, a.GTinh, a.Tuoi, a.DChi, a.DTuong, a.MaKP, a.MaCB, a.MaCBth, a.TrongDM } into kq
                          select new
                          {
                              kq.Key.TenBNhan,
                              kq.Key.GTinh,
                              kq.Key.Tuoi,
                              kq.Key.DChi,
                              kq.Key.DTuong,
                              kq.Key.MaKP,
                              kq.Key.MaCB,
                              kq.Key.MaCBth,
                              kq.Key.TrongDM,
                          }).ToList();
            #endregion
            #region Xét nghiệm huyết học
            if (tentn.Contains("Xét nghiệm huyết học"))
            {
                if (_lstbc.Count() > 0)
                {
                    bchh.Clear();
                    foreach (var item in _lstbc)
                    {
                        BCCLS cls = new BCCLS();
                        cls.TenBNhan = item.TenBNhan;
                        cls.GTinh = item.GTinh;
                        cls.Tuoi = item.Tuoi;
                        cls.DTuong = item.DTuong;
                        cls.MaKP = item.MaKP;
                        cls.MaCB = item.MaCB;
                        cls.MaCBth = item.MaCBth;
                        cls.DChi = item.DChi;
                        cls.TrongDM = item.TrongDM;
                        var abc = lstBC.Where(p => p.TenBNhan == item.TenBNhan).ToList();
                        foreach (var a in abc)
                        {
                            if (!string.IsNullOrEmpty(a.RBC))
                            {
                                cls.RBC = a.RBC;
                            }
                            if (!string.IsNullOrEmpty(a.HGB))
                            {
                                cls.HGB = a.HGB;
                            }
                            if (!string.IsNullOrEmpty(a.HCT))
                            {
                                cls.HCT = a.HCT;
                            }
                            if (!string.IsNullOrEmpty(a.MCV))
                            {
                                cls.MCV = a.MCV;
                            }
                            if (!string.IsNullOrEmpty(a.MCH))
                            {
                                cls.MCH = a.MCH;
                            }
                            if (!string.IsNullOrEmpty(a.MCHC))
                            {
                                cls.MCHC = a.MCHC;
                            }
                            if (!string.IsNullOrEmpty(a.HCconhan))
                            {
                                cls.HCconhan = a.HCconhan;
                            }
                            if (!string.IsNullOrEmpty(a.HCluoi))
                            {
                                cls.HCluoi = a.HCluoi;
                            }
                            if (!string.IsNullOrEmpty(a.RDWCV))
                            {
                                cls.RDWCV = a.RDWCV;
                            }
                            if (!string.IsNullOrEmpty(a.RDWSD))
                            {
                                cls.RDWSD = a.RDWSD;
                            }
                            if (!string.IsNullOrEmpty(a.Tebaobatthuong))
                            {
                                cls.Tebaobatthuong = a.Tebaobatthuong;
                            }
                            if (!string.IsNullOrEmpty(a.WBC))
                            {
                                cls.WBC = a.WBC;
                            }
                            if (!string.IsNullOrEmpty(a.NEUT))
                            {
                                cls.NEUT = a.NEUT;
                            }
                            if (!string.IsNullOrEmpty(a.NEUTP))
                            {
                                cls.NEUTP = a.NEUTP;
                            }
                            if (!string.IsNullOrEmpty(a.LYMP))
                            {
                                cls.LYMP = a.LYMP;
                            }
                            if (!string.IsNullOrEmpty(a.LYMPP))
                            {
                                cls.LYMPP = a.LYMPP;
                            }
                            if (!string.IsNullOrEmpty(a.MONO))
                            {
                                cls.MONO = a.MONO;
                            }
                            if (!string.IsNullOrEmpty(a.MONOP))
                            {
                                cls.MONOP = a.MONOP;
                            }
                            if (!string.IsNullOrEmpty(a.EO))
                            {
                                cls.EO = a.EO;
                            }
                            if (!string.IsNullOrEmpty(a.EOP))
                            {
                                cls.EOP = a.EOP;
                            }
                            if (!string.IsNullOrEmpty(a.BSAO))
                            {
                                cls.BSAO = a.BSAO;
                            }
                            if (!string.IsNullOrEmpty(a.BSAOP))
                            {
                                cls.BSAOP = a.BSAOP;
                            }
                            if (!string.IsNullOrEmpty(a.PLT))
                            {
                                cls.PLT = a.PLT;
                            }
                            if (!string.IsNullOrEmpty(a.PDW))
                            {
                                cls.PDW = a.PDW;
                            }
                            if (!string.IsNullOrEmpty(a.MPV))
                            {
                                cls.MPV = a.MPV;
                            }
                            if (!string.IsNullOrEmpty(a.PCT))
                            {
                                cls.PCT = a.PCT;
                            }
                            if (!string.IsNullOrEmpty(a.Maulang1h))
                            {
                                cls.Maulang1h = a.Maulang1h;
                            }
                            if (!string.IsNullOrEmpty(a.Maulang2h))
                            {
                                cls.Maulang2h = a.Maulang2h;
                            }
                            if (!string.IsNullOrEmpty(a.Thoigianmauchay))
                            {
                                cls.Thoigianmauchay = a.Thoigianmauchay;
                            }
                            if (!string.IsNullOrEmpty(a.Thoigianmaudong))
                            {
                                cls.Thoigianmaudong = a.Thoigianmaudong;
                            }
                            if (!string.IsNullOrEmpty(a.NhommauABO))
                            {
                                cls.NhommauABO = a.NhommauABO;
                            }
                            if (!string.IsNullOrEmpty(a.NhommauRH))
                            {
                                cls.NhommauRH = a.NhommauRH;
                            }
                        }
                        bchh.Add(cls);
                    }
                    BaoCao.Rep_SoCLS_XNHH rep = new Rep_SoCLS_XNHH();
                    rep.Parameters["TenCQCQ"].Value = DungChung.Bien.TenCQCQ;
                    rep.Parameters["TenCQ"].Value = DungChung.Bien.TenCQ;
                    rep.Parameters["Ngay"].Value = "Từ ngày " + ngaytu.ToString("dd/MM/yyyy") + " đến ngày " + ngayden.ToString("dd/MM/yyyy");
                    rep.DataSource = bchh.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Sổ CLS xét nghiệm huyết học không có dữ liệu!!");
                }
            }
            #endregion
            #region Xét nghiệm sinh hoá - vi sinh - nước tiểu - md
            if (tentn.Contains("Xét nghiệm hoá sinh máu") || tentn.Contains("Xét nghiệm nước tiểu") || tentn.Contains("Xét nghiệm vi sinh") || tentn.Contains("Xét nghiệm miễn dịch"))
            {
                if (_lstbc.Count() > 0)
                {
                    bchhsh.Clear();
                    foreach (var item in _lstbc)
                    {
                        BCCLSHS cls = new BCCLSHS();
                        cls.TenBNhan = item.TenBNhan;
                        cls.GTinh = item.GTinh;
                        cls.Tuoi = item.Tuoi;
                        cls.DTuong = item.DTuong;
                        cls.MaKP = item.MaKP;
                        cls.MaCB = item.MaCB;
                        cls.MaCBth = item.MaCBth;
                        cls.DChi = item.DChi;
                        cls.TrongDM = item.TrongDM;
                        var abc = lstBC.Where(p => p.TenBNhan == item.TenBNhan).ToList();
                        foreach (var a in abc)
                        {
                            if (!string.IsNullOrEmpty(a.Glucose))
                            {
                                cls.Glucose = a.Glucose;
                            }
                            if (!string.IsNullOrEmpty(a.Urea))
                            {
                                cls.Urea = a.Urea;
                            }
                            if (!string.IsNullOrEmpty(a.Creatinin))
                            {
                                cls.Creatinin = a.Creatinin;
                            }
                            if (!string.IsNullOrEmpty(a.CholesterolTP))
                            {
                                cls.CholesterolTP = a.CholesterolTP;
                            }
                            if (!string.IsNullOrEmpty(a.Triglycorid))
                            {
                                cls.Triglycorid = a.Triglycorid;
                            }
                            if (!string.IsNullOrEmpty(a.HDLC))
                            {
                                cls.HDLC = a.HDLC;
                            }
                            if (!string.IsNullOrEmpty(a.LDLC))
                            {
                                cls.LDLC = a.LDLC;
                            }
                            if (!string.IsNullOrEmpty(a.ASTGOT))
                            {
                                cls.ASTGOT = a.ASTGOT;
                            }
                            if (!string.IsNullOrEmpty(a.ALTGPT))
                            {
                                cls.ALTGPT = a.ALTGPT;
                            }
                            if (!string.IsNullOrEmpty(a.GGT))
                            {
                                cls.GGT = a.GGT;
                            }
                            if (!string.IsNullOrEmpty(a.AcidUric))
                            {
                                cls.AcidUric = a.AcidUric;
                            }
                            if (!string.IsNullOrEmpty(a.BilirubinTP))
                            {
                                cls.BilirubinTP = a.BilirubinTP;
                            }
                            if (!string.IsNullOrEmpty(a.BilirubinTT))
                            {
                                cls.BilirubinTT = a.BilirubinTT;
                            }
                            if (!string.IsNullOrEmpty(a.BilirubinGT))
                            {
                                cls.BilirubinGT = a.BilirubinGT;
                            }
                            if (!string.IsNullOrEmpty(a.ProteinTP))
                            {
                                cls.ProteinTP = a.ProteinTP;
                            }
                            if (!string.IsNullOrEmpty(a.Albumin))
                            {
                                cls.Albumin = a.Albumin;
                            }
                            if (!string.IsNullOrEmpty(a.Amylase))
                            {
                                cls.Amylase = a.Amylase;
                            }
                            if (!string.IsNullOrEmpty(a.Fe))
                            {
                                cls.Fe = a.Fe;
                            }
                            if (!string.IsNullOrEmpty(a.CalciTP))
                            {
                                cls.CalciTP = a.CalciTP;
                            }
                            if (!string.IsNullOrEmpty(a.CRPCReactiveProtein))
                            {
                                cls.CRPCReactiveProtein = a.CRPCReactiveProtein;
                            }
                            if (!string.IsNullOrEmpty(a.HbA1c))
                            {
                                cls.HbA1c = a.HbA1c;
                            }
                            if (!string.IsNullOrEmpty(a.Na))
                            {
                                cls.Na = a.Na;
                            }
                            if (!string.IsNullOrEmpty(a.K))
                            {
                                cls.K = a.K;
                            }
                            if (!string.IsNullOrEmpty(a.CL))
                            {
                                cls.CL = a.CL;
                            }
                        }
                        bchhsh.Add(cls);
                    }
                    BaoCao.Rep_SoCLS_XNSH_VS_NT_MD rep = new Rep_SoCLS_XNSH_VS_NT_MD();
                    rep.Parameters["TenBC"].Value = "SỔ CẬN LÂM SÀNG XÉT NGHIỆM SINH HOÁ - VI SINH - NƯỚC TIỂU - MIỄN DỊCH";
                    rep.Parameters["TenCQCQ"].Value = DungChung.Bien.TenCQCQ;
                    rep.Parameters["TenCQ"].Value = DungChung.Bien.TenCQ;
                    rep.Parameters["Ngay"].Value = "Từ ngày " + ngaytu.ToString("dd/MM/yyyy") + " đến ngày " + ngayden.ToString("dd/MM/yyyy");
                    rep.DataSource = bchhsh.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Sổ CLS xét nghiệm khác không có dữ liệu!!");
                }
            }
            #endregion
            #region Siêu âm - điện tim - xq - ct
            if (tentn.Contains("Siêu âm") || tentn.Contains("X-Quang") || tentn.Contains("Điện tim") || tentn.Contains("Chụp CT"))
            {
                if (_lstbc.Count() > 0)
                {
                    cdhd.Clear();
                    foreach (var item in _lstbc)
                    {
                        BCCLSXQ cls = new BCCLSXQ();
                        cls.TenBNhan = item.TenBNhan;
                        cls.GTinh = item.GTinh;
                        cls.Tuoi = item.Tuoi;
                        cls.DChi = item.DChi;
                        cls.DTuong = item.DTuong;
                        cls.MaKP = item.MaKP;
                        cls.MaCB = item.MaCB;
                        cls.MaCBth = item.MaCBth;
                        cls.TrongDM = item.TrongDM;
                        var abc = lstBC.Where(p => p.TenBNhan == item.TenBNhan).ToList();
                        foreach (var a in abc)
                        {
                            if (!string.IsNullOrEmpty(a.SoPhieu))
                            {
                                cls.SoPhieu = a.SoPhieu;
                            }
                            if (!string.IsNullOrEmpty(a.ChanDoan))
                            {
                                cls.ChanDoan = a.ChanDoan;
                            }
                            if (!string.IsNullOrEmpty(a.KetQua))
                            {
                                cls.KetQua = a.KetQua;
                            }
                        }
                        cdhd.Add(cls);
                    }
                    BaoCao.Rep_SoCLS_XQ_DT_SA rep = new Rep_SoCLS_XQ_DT_SA();
                    rep.Parameters["TenBC"].Value = "SỔ CẬN LÂM SÀNG SIÊU ÂM - ĐIỆN TIM - XQUANG - CT";
                    rep.Parameters["TenCQCQ"].Value = DungChung.Bien.TenCQCQ;
                    rep.Parameters["TenCQ"].Value = DungChung.Bien.TenCQ;
                    rep.Parameters["Ngay"].Value = "Từ ngày " + ngaytu.ToString("dd/MM/yyyy") + " đến ngày " + ngayden.ToString("dd/MM/yyyy");
                    rep.DataSource = cdhd.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Sổ CLS CDHA không có dữ liệu!!");
                }
            }
            #endregion
            #region Nội soi
            if (tentn.Contains("Nội soi"))
            {
                if (_lstbc.Count() > 0)
                {
                    ns.Clear();
                    foreach (var item in _lstbc)
                    {
                        NoiSoi cls = new NoiSoi();
                        cls.TenBNhan = item.TenBNhan;
                        cls.GTinh = item.GTinh;
                        cls.Tuoi = item.Tuoi;
                        cls.DTuong = item.DTuong;
                        cls.DChi = item.DChi;
                        cls.MaCB = item.MaCB;
                        cls.MaCBth = item.MaCBth;
                        cls.MaKP = item.MaKP;
                        cls.TrongDM = item.TrongDM;
                        var abc = lstBC.Where(p => p.TenBNhan == item.TenBNhan).ToList();
                        foreach (var a in abc)
                        {
                            if (!string.IsNullOrEmpty(a.ChanDoan))
                            {
                                cls.ChanDoan = a.ChanDoan;
                            }
                            if (!string.IsNullOrEmpty(a.KQNS))
                            {
                                cls.KQNS = a.KQNS;
                            }
                        }
                        ns.Add(cls);
                    }
                    BaoCao.Rep_SoCLS_NS rep = new BaoCao.Rep_SoCLS_NS();
                    rep.Parameters["TenCQCQ"].Value = DungChung.Bien.TenCQCQ;
                    rep.Parameters["TenCQ"].Value = DungChung.Bien.TenCQ;
                    rep.Parameters["Ngay"].Value = "Từ ngày " + ngaytu.ToString("dd/MM/yyyy") + " đến ngày " + ngayden.ToString("dd/MM/yyyy");
                    rep.DataSource = ns.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Sổ CLS nội soi không có dữ liệu!!");
                }
            }
            #endregion
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

        private void grvTieuNhom_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chon")
            {
                if (grvTieuNhom.GetFocusedRowCellValue("tentn") != null)
                {
                    string Ten = grvTieuNhom.GetFocusedRowCellValue("tentn").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Tnhom.First().chon == true)
                        {
                            foreach (var a in _Tnhom)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Tnhom)
                            {
                                a.chon = true;
                            }
                        }
                        grcTieuNhom.DataSource = "";
                        grcTieuNhom.DataSource = _Tnhom.ToList();
                    }
                }
            }
        }
    }
}
