using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using GiamDinhBHXH.BHXH_Model;
using QLBV.DungChung;
using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace QLBV.ChucNang
{
    public partial class frm_GuiChungTu_BHYT : Form
    {
        public frm_GuiChungTu_BHYT()
        {
            InitializeComponent();
            //GridLocalizer.Active = new MyGridLocalizer();
        }

        List<BenhNhanADO> listSelecteds;
        List<BenhNhanADO> listAll;

        private void frm_GuiChungTu_BHYT_Load(object sender, EventArgs e)
        {
            listSelecteds = new List<BenhNhanADO>();
            txtSearch.ResetText();
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
            LoadDataToForm();
            gridColumn12.Image = imageList1.Images[0];
        }

        private void LoadDataToForm()
        {
            int loai = radioGroupLoai.SelectedIndex + 1;
            var tuNgay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            var denNgay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            listAll = (from bn in dataContext.BenhNhans
                       join vp in dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan
                       join rv in dataContext.RaViens.Where(o => o.NgayRa >= tuNgay && o.NgayRa <= denNgay) on bn.MaBNhan equals rv.MaBNhan
                       join hsct in dataContext.HSCT_BHXH.Where(o => o.LOAI == loai) on bn.MaBNhan equals hsct.MaBNhan
                       select new BenhNhanADO { MaBNhan = bn.MaBNhan, DChi = bn.DChi, TenBNhan = bn.TenBNhan, Tuoi = bn.Tuoi, Check = false, GTinh = bn.GTinh, NgaySinh = bn.NgaySinh, ThangSinh = bn.ThangSinh, NamSinh = bn.NamSinh, MaGD_HSCT = hsct.MA_GD, SThe = bn.SThe, HSCT_ID = hsct.ID, Is_Send = (bool?)hsct.IS_SEND ?? false, LOAI = hsct.LOAI }).ToList();
            listAll.ForEach(x => { if (listSelecteds.Exists(o => o.MaBNhan == x.MaBNhan)) x.Check = true; x.Is_Send = !string.IsNullOrWhiteSpace(x.MaGD_HSCT); });
            gridControlSearch.BeginUpdate();
            gridControlSearch.DataSource = listAll;
            gridControlSearch.EndUpdate();
        }

        public class MessageHSCT
        {
            public int MaBNhan { get; set; }
            public string Message { get; set; }
            public MessageHSCT(int _MaBNhan, string _Message)
            {
                this.MaBNhan = _MaBNhan;
                this.Message = _Message;
            }
        }

        public class CT03
        {
            public string SO_LUU_TRU { get; set; }
            public string MA_YTE { get; set; }
            public string MA_BHXH { get; set; }
            public string MA_THE { get; set; }
            public string MA_KHOA { get; set; }
            public string HO_TEN { get; set; }
            public string NGAY_SINH { get; set; }
            public string GIOI_TINH { get; set; }
            public string MA_DANTOC { get; set; }
            public string DIA_CHI { get; set; }
            public string NGHE_NGHIEP { get; set; }
            public string DINH_CHI_THAI_NGHEN { get; set; }
            public string TUOI_THAI { get; set; }
            public string HO_TEN_CHA { get; set; }
            public string HO_TEN_ME { get; set; }
            public string NGAY_VAO { get; set; }
            public string NGAY_RA { get; set; }
            public string CHAN_DOAN { get; set; }
            public string PP_DIEUTRI { get; set; }
            public string GHI_CHU { get; set; }
            public string THU_TRUONG_DVI { get; set; }
            public string MA_CCHN_TRUONGKHOA { get; set; }
            public string TEN_TRUONGKHOA { get; set; }
            public string NGAY_CHUNG_TU { get; set; }
            public string NGOAITRU_TUNGAY { get; set; }
            public string NGOAITRU_DENNGAY { get; set; }
            public string TEKT { get; set; }
        }

        public class CT04
        {
            public string MA_CT { get; set; }
            public string SO_SERI { get; set; }
            public string MA_BHXH { get; set; }
            public string MA_THE { get; set; }
            public string HO_TEN { get; set; }
            public string NGAY_SINH { get; set; }
            public string GIOI_TINH { get; set; }
            public string MA_DANTOC { get; set; }
            public string DIA_CHI { get; set; }
            public string NGHE_NGHIEP { get; set; }
            public string HO_TEN_CHA { get; set; }
            public string HO_TEN_ME { get; set; }
            public string NGUOI_GIAM_HO { get; set; }
            public string TEN_DONVI { get; set; }
            public string NGAY_VAO { get; set; }
            public string NGAY_RA { get; set; }
            public string CHAN_DOAN_VAO { get; set; }
            public string CHAN_DOAN_RA { get; set; }
            public string QT_BENHLY { get; set; }
            public string TOMTAT_KQ { get; set; }
            public string PP_DIEUTRI { get; set; }
            public string NGAY_SINHCON { get; set; }
            public string NGAY_CHETCON { get; set; }
            public string SO_CONCHET { get; set; }
            public string TT_RAVIEN { get; set; }
            public string GHI_CHU { get; set; }
            public string NGUOI_DAI_DIEN { get; set; }
            public string NGAY_CT { get; set; }
            public string TEKT { get; set; }
        }

        public class CT06
        {
            public string SO_SERI { get; set; }
            public string MA_CT { get; set; }
            public string SO_KCB { get; set; }
            public string MA_BHXH { get; set; }
            public string MA_THE { get; set; }
            public string HO_TEN { get; set; }
            public string NGAY_SINH { get; set; }
            public string TEN_DVI { get; set; }
            public string CHAN_DOAN { get; set; }
            public string NGAY_VAO { get; set; }
            public string NGAY_RA { get; set; }
            public string NGUOI_DAI_DIEN { get; set; }
            public string TEN_BS { get; set; }
            public string MA_BS { get; set; }
            public string NGAY_CT { get; set; }
        }

        public class CT07
        {
            public string SO_SERI { get; set; }
            public string MA_CT { get; set; }
            public string SO_KCB { get; set; }
            public string MA_BHXH { get; set; }
            public string MA_THE { get; set; }
            public string HO_TEN { get; set; }
            public string NGAY_SINH { get; set; }
            public string GIOI_TINH { get; set; }
            public string DON_VI { get; set; }
            public string CHANDOAN_DIEUTRI { get; set; }
            public string HO_TEN_CHA { get; set; }
            public string HO_TEN_ME { get; set; }
            public string TU_NGAY { get; set; }
            public string DEN_NGAY { get; set; }
            public string NGUOI_DAI_DIEN { get; set; }
            public string MA_CCHN { get; set; }
            public string TEN_NGUOI_HANH_NGHE { get; set; }
            public string THU_TRUONG_DV { get; set; }
            public string NGAY_CHUNG_TU { get; set; }
            public string TEKT { get; set; }
            public string MAU_SO { get; set; }
        }

        public XmlDocument SerializeToXml<T>(T source)
        {
            var document = new XmlDocument();
            var navigator = document.CreateNavigator();

            using (var writer = navigator.AppendChild())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, source);
            }
            return document;
        }

        private bool SendHSCT(QLBV_Database.QLBVEntities _dataContext, int _hsctID, int _loai, int _maBNhan, string path1, string path2, ref List<MessageHSCT> _listMessages)
        {
            bool rs = true;
            BenhNhan benhNhan = _dataContext.BenhNhans.FirstOrDefault(p => p.MaBNhan == _maBNhan);
            RaVien raVien = _dataContext.RaViens.FirstOrDefault(o => o.MaBNhan == _maBNhan);
            VaoVien vaoVien = _dataContext.VaoViens.FirstOrDefault(o => o.MaBNhan == _maBNhan);
            TTboXung ttbs = _dataContext.TTboXungs.FirstOrDefault(o => o.MaBNhan == _maBNhan);
            var hsctBHXH = _dataContext.HSCT_BHXH.FirstOrDefault(o => o.ID == _hsctID);
            if (hsctBHXH != null)
            {
                string namqt = raVien.NgayRa.Value.Year.ToString();
                string thanqt = raVien.NgayRa.Value.Month.ToString("D2");
                string file3 = "", file4 = "", file5 = "", file6 = "", file7 = "";
                string pathXML3 = path2 + "\\XML_CT_3_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + _maBNhan + ".xml";// tên file
                string pathXML4 = path2 + "\\XML_CT_4_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + _maBNhan + ".xml";// tên file
                string pathXML5 = path2 + "\\XML_CT_5_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + _maBNhan + ".xml";// tên file
                string pathXML6 = path2 + "\\XML_CT_6_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + _maBNhan + ".xml";// tên file
                string pathXML7 = path2 + "\\XML_CT_7_" + DungChung.Bien.MaBV + "_" + namqt + thanqt + "_" + _maBNhan + ".xml";// tên file
                int i = 0;
                if (hsctBHXH.LOAI == 1)
                {
                    CT04 ct04 = new CT04();
                    ct04.GHI_CHU = hsctBHXH.GHI_CHU;
                    ct04.MA_BHXH = hsctBHXH.MA_BHXH;
                    ct04.MA_CT = hsctBHXH.MA_CT;
                    ct04.NGAY_CHETCON = hsctBHXH.NGAY_CHETCON != null ? hsctBHXH.NGAY_CHETCON.Value.ToString("yyyyMMdd") : null;
                    ct04.NGAY_CT = hsctBHXH.NGAY_CT != null ? hsctBHXH.NGAY_CT.Value.ToString("yyyyMMdd") : null;
                    ct04.NGAY_SINHCON = hsctBHXH.NGAY_SINHCON != null ? hsctBHXH.NGAY_SINHCON.Value.ToString("yyyyMMdd") : null;
                    var cbDaiDien = _dataContext.CanBoes.FirstOrDefault(o => o.MaCB == hsctBHXH.NGUOI_DAI_DIEN);
                    if (cbDaiDien != null)
                    {
                        ct04.NGUOI_DAI_DIEN = cbDaiDien.TenCB;
                    }
                    ct04.PP_DIEUTRI = hsctBHXH.PP_DIEUTRI;
                    ct04.QT_BENHLY = hsctBHXH.QT_BENHLY;
                    ct04.SO_CONCHET = hsctBHXH.SO_CONCHET != null ? hsctBHXH.SO_CONCHET.Value.ToString() : null;
                    ct04.SO_SERI = hsctBHXH.SO_SERI;
                    ct04.TEKT = ((benhNhan.SThe.Contains("TE") && benhNhan.SThe.Contains("KT")) || (DungChung.Ham.CalculateAgeByDate(benhNhan.NgaySinh, benhNhan.ThangSinh, benhNhan.NamSinh) < 6 && string.IsNullOrWhiteSpace(benhNhan.SThe))) ? "1" : "0";
                    ct04.TEN_DONVI = hsctBHXH.DON_VI;
                    ct04.TOMTAT_KQ = hsctBHXH.TOMTAT_KQ;
                    ct04.NGAY_VAO = raVien.NgayVao != null ? raVien.NgayVao.Value.ToString("yyyyMMddHHmm") : null;
                    ct04.NGAY_RA = raVien.NgayRa != null ? raVien.NgayRa.Value.ToString("yyyyMMddHHmm") : null;
                    ct04.HO_TEN = benhNhan.TenBNhan;
                    var ngaySinh = DungChung.Ham.GhepNgaySinh("/", benhNhan.NamSinh, benhNhan.ThangSinh, benhNhan.NgaySinh);
                    if (ngaySinh.Count() > 4)
                        ct04.NGAY_SINH = DateTime.Parse(ngaySinh).ToString("yyyyMMdd");
                    else
                        ct04.NGAY_SINH = ngaySinh;
                    ct04.GIOI_TINH = benhNhan.GTinh == 1 ? "1" : "2";
                    ct04.DIA_CHI = benhNhan.DChi;
                    ct04.MA_THE = benhNhan.SThe.Trim();
                    var danToc = _dataContext.DanTocs.FirstOrDefault(o => o.MaDT == ttbs.MaDT);
                    if (danToc != null)
                        ct04.MA_DANTOC = danToc.MaDanToc;
                    var ngheNghiep = _dataContext.DmNNs.FirstOrDefault(o => o.MaNN == ttbs.MaNN);
                    if (ngheNghiep != null)
                    {
                        ct04.NGHE_NGHIEP = ngheNghiep.TenNN;
                    }
                    ct04.HO_TEN_CHA = hsctBHXH.HO_TEN_CHA;
                    ct04.HO_TEN_ME = hsctBHXH.HO_TEN_ME;
                    ct04.NGUOI_GIAM_HO = ttbs.NThan;
                    ct04.CHAN_DOAN_RA = raVien.ChanDoan;
                    if (vaoVien != null)
                        ct04.CHAN_DOAN_VAO = vaoVien.ChanDoan;
                    else
                    {
                        var bnkb = _dataContext.BNKBs.Where(o => o.MaBNhan == _maBNhan).OrderByDescending(o => o.IDKB).ToList();
                        if (bnkb != null && bnkb.Count > 0)
                        {
                            ct04.CHAN_DOAN_VAO = bnkb.FirstOrDefault().ChanDoan;
                        }
                    }

                    switch (raVien.Status)
                    {
                        case 1:
                            ct04.TT_RAVIEN = "2";
                            break;
                        case 2:
                            ct04.TT_RAVIEN = "1";
                            break;
                        case 3:
                            ct04.TT_RAVIEN = "3";
                            break;
                        case 4:
                            ct04.TT_RAVIEN = "4";
                            break;
                    }

                    if (!CheckDL_CT04(_maBNhan, benhNhan, ct04, ref _listMessages))
                        return false;

                    var xmlDocument4 = SerializeToXml<CT04>(ct04);
                    xmlDocument4.Save(pathXML4);
                    Byte[] bytes = File.ReadAllBytes(pathXML4);
                    file4 = Convert.ToBase64String(bytes);
                }
                if (hsctBHXH.LOAI == 2)
                {
                    CT06 ct06 = new CT06();
                    ct06.CHAN_DOAN = hsctBHXH.CHAN_DOAN;
                    ct06.MA_BHXH = hsctBHXH.MA_BHXH;
                    ct06.MA_CT = hsctBHXH.MA_CT;
                    ct06.NGAY_CT = hsctBHXH.NGAY_CT != null ? hsctBHXH.NGAY_CT.Value.ToString("yyyyMMdd") : null;
                    ct06.SO_SERI = hsctBHXH.SO_SERI;
                    ct06.TEN_DVI = hsctBHXH.DON_VI;
                    var cbDaiDien = _dataContext.CanBoes.FirstOrDefault(o => o.MaCB == hsctBHXH.NGUOI_DAI_DIEN);
                    if (cbDaiDien != null)
                    {
                        ct06.NGUOI_DAI_DIEN = cbDaiDien.TenCB;
                    }
                    var cb = _dataContext.CanBoes.FirstOrDefault(o => o.MaCB == hsctBHXH.MaCB);
                    if (cb != null)
                    {
                        ct06.MA_BS = cb.MaCCHN;
                        ct06.TEN_BS = cb.TenCB;
                    }
                    ct06.SO_KCB = hsctBHXH.SO_KCB;
                    ct06.MA_THE = benhNhan.SThe.Trim();
                    ct06.NGAY_VAO = hsctBHXH.NGAY_VAO != null ? hsctBHXH.NGAY_VAO.Value.ToString("yyyyMMdd") : null;
                    ct06.NGAY_RA = hsctBHXH.NGAY_RA != null ? hsctBHXH.NGAY_RA.Value.ToString("yyyyMMdd") : null;
                    ct06.HO_TEN = benhNhan.TenBNhan;
                    ct06.SO_KCB = benhNhan.SoKB.ToString();
                    var ngaySinh = DungChung.Ham.GhepNgaySinh("/", benhNhan.NamSinh, benhNhan.ThangSinh, benhNhan.NgaySinh);
                    if (ngaySinh.Count() > 4)
                        ct06.NGAY_SINH = DateTime.Parse(ngaySinh).ToString("yyyyMMdd");
                    else
                        ct06.NGAY_SINH = ngaySinh;

                    if (!CheckDL_CT06(_maBNhan, ct06, ref _listMessages))
                        return false;

                    var xmlDocument6 = SerializeToXml<CT06>(ct06);
                    xmlDocument6.Save(pathXML6);
                    Byte[] bytes = File.ReadAllBytes(pathXML6);
                    file6 = Convert.ToBase64String(bytes);
                }
                if (hsctBHXH.LOAI == 3)
                {
                    CT03 ct03 = new CT03();
                    ct03.GHI_CHU = hsctBHXH.GHI_CHU;
                    ct03.NGAY_CHUNG_TU = hsctBHXH.NGAY_CT != null ? hsctBHXH.NGAY_CT.Value.ToString("yyyyMMdd") : null;
                    ct03.MA_BHXH = hsctBHXH.MA_BHXH;
                    ct03.NGOAITRU_TUNGAY = hsctBHXH.NGOAITRU_TUNGAY != null ? hsctBHXH.NGOAITRU_TUNGAY.Value.ToString("yyyyMMdd") : null;
                    ct03.NGOAITRU_DENNGAY = hsctBHXH.NGOAITRU_DENNGAY != null ? hsctBHXH.NGOAITRU_DENNGAY.Value.ToString("yyyyMMdd") : null;
                    ct03.MA_CCHN_TRUONGKHOA = hsctBHXH.MA_CCHN_TRUONGKHOA;
                    ct03.DINH_CHI_THAI_NGHEN = hsctBHXH.DINH_CHI_THAI_NGHEN == true ? "1" : "0";
                    ct03.TUOI_THAI = hsctBHXH.DINH_CHI_THAI_NGHEN == true ? hsctBHXH.TUOI_THAI : "";
                    ct03.TEN_TRUONGKHOA = hsctBHXH.TEN_TRUONGKHOA;
                    ct03.PP_DIEUTRI = hsctBHXH.PP_DIEUTRI;
                    ct03.CHAN_DOAN = hsctBHXH.CHAN_DOAN;
                    ct03.SO_LUU_TRU = hsctBHXH.SO_LUU_TRU;
                    ct03.TEKT = ((benhNhan.SThe.Contains("TE") && benhNhan.SThe.Contains("KT")) || (DungChung.Ham.CalculateAgeByDate(benhNhan.NgaySinh, benhNhan.ThangSinh, benhNhan.NamSinh) < 7 && string.IsNullOrWhiteSpace(benhNhan.SThe))) ? "1" : "0";
                    ct03.THU_TRUONG_DVI = hsctBHXH.THU_TRUONG_DVI;
                    ct03.MA_KHOA = hsctBHXH.MA_KHOA;
                    ct03.NGAY_VAO = raVien.NgayVao != null ? raVien.NgayVao.Value.ToString("yyyyMMddHHmm") : null;
                    ct03.NGAY_RA = raVien.NgayRa != null ? raVien.NgayRa.Value.ToString("yyyyMMddHHmm") : null;
                    ct03.HO_TEN = benhNhan.TenBNhan;
                    var ngaySinh = DungChung.Ham.GhepNgaySinh("/", benhNhan.NamSinh, benhNhan.ThangSinh, benhNhan.NgaySinh);
                    if (ngaySinh.Count() > 4)
                        ct03.NGAY_SINH = DateTime.Parse(ngaySinh).ToString("yyyyMMdd");
                    else
                        ct03.NGAY_SINH = ngaySinh;
                    ct03.GIOI_TINH = benhNhan.GTinh == 1 ? "1" : "2";
                    ct03.DIA_CHI = benhNhan.DChi;
                    ct03.MA_THE = benhNhan.SThe.Trim();
                    var danToc = _dataContext.DanTocs.FirstOrDefault(o => o.MaDT == ttbs.MaDT);
                    if (danToc != null)
                        ct03.MA_DANTOC = danToc.MaDanToc;
                    var ngheNghiep = _dataContext.DmNNs.FirstOrDefault(o => o.MaNN == ttbs.MaNN);
                    if (ngheNghiep != null)
                    {
                        ct03.NGHE_NGHIEP = ngheNghiep.TenNN;
                    }
                    ct03.HO_TEN_CHA = hsctBHXH.HO_TEN_CHA;
                    ct03.HO_TEN_ME = hsctBHXH.HO_TEN_ME;

                    if (!CheckDL_CT03(_maBNhan, benhNhan, ct03, ref _listMessages))
                        return false;

                    var xmlDocument3 = SerializeToXml<CT03>(ct03);
                    xmlDocument3.Save(pathXML3);
                    Byte[] bytes = File.ReadAllBytes(pathXML3);
                    file3 = Convert.ToBase64String(bytes);
                }
                if (hsctBHXH.LOAI == 4)
                {
                    CT07 ct07 = new CT07();
                    ct07.CHANDOAN_DIEUTRI = hsctBHXH.CHAN_DOAN + Environment.NewLine + hsctBHXH.PP_DIEUTRI;
                    ct07.MA_BHXH = hsctBHXH.MA_BHXH;
                    ct07.MA_CT = hsctBHXH.MA_CT;
                    ct07.NGAY_CHUNG_TU = hsctBHXH.NGAY_CT != null ? hsctBHXH.NGAY_CT.Value.ToString("yyyyMMdd") : null;
                    ct07.SO_SERI = hsctBHXH.SO_SERI;
                    ct07.DON_VI = hsctBHXH.DON_VI;
                    ct07.MA_CCHN = hsctBHXH.MA_CCHN_TRUONGKHOA;
                    ct07.TEN_NGUOI_HANH_NGHE = hsctBHXH.TEN_TRUONGKHOA;
                    ct07.MA_THE = benhNhan.SThe.Trim();
                    ct07.TU_NGAY = hsctBHXH.NGAY_VAO != null ? hsctBHXH.NGAY_VAO.Value.ToString("yyyyMMdd") : null;
                    ct07.DEN_NGAY = hsctBHXH.NGAY_RA != null ? hsctBHXH.NGAY_RA.Value.ToString("yyyyMMdd") : null;
                    ct07.HO_TEN = benhNhan.TenBNhan;
                    ct07.SO_KCB = benhNhan.SoKB.ToString();
                    var ngaySinh = DungChung.Ham.GhepNgaySinh("/", benhNhan.NamSinh, benhNhan.ThangSinh, benhNhan.NgaySinh);
                    if (ngaySinh.Count() > 4)
                        ct07.NGAY_SINH = DateTime.Parse(ngaySinh).ToString("yyyyMMdd");
                    else
                        ct07.NGAY_SINH = ngaySinh;
                    ct07.GIOI_TINH = benhNhan.GTinh == 1 ? "1" : "2";
                    ct07.HO_TEN_CHA = hsctBHXH.HO_TEN_CHA;
                    ct07.HO_TEN_ME = hsctBHXH.HO_TEN_ME;
                    ct07.THU_TRUONG_DV = hsctBHXH.THU_TRUONG_DVI;
                    ct07.TEKT = ((benhNhan.SThe.Contains("TE") && benhNhan.SThe.Contains("KT")) || (DungChung.Ham.CalculateAgeByDate(benhNhan.NgaySinh, benhNhan.ThangSinh, benhNhan.NamSinh) < 7 && string.IsNullOrWhiteSpace(benhNhan.SThe))) ? "1" : "0";

                    if (!CheckDL_CT07(_maBNhan, ct07, ref _listMessages))
                        return false;

                    var xmlDocument7 = SerializeToXml<CT07>(ct07);
                    xmlDocument7.Save(pathXML7);
                    Byte[] bytes = File.ReadAllBytes(pathXML7);
                    file7 = Convert.ToBase64String(bytes);
                }

                XElement xEle1;
                xEle1 = new XElement(("HSCHUNGTU"),
                                         new XElement("THONGTINDONVI", new XElement("MACSKCB", DungChung.Bien.MaBV), new XElement("CHUKYDONVI")),
                                         new XElement("THONGTINHOSO",
                                             new XElement("NGAYLAP", DateTime.Now.ToString("yyyyMMdd")),//--------------------------------------
                                             new XElement("SOLUONGHOSO", 1),
                                             new XElement("DANHSACHHOSO",
                                                 (new XElement("HOSO"))
                                                 )));

                if (!String.IsNullOrEmpty(file3))
                    xEle1.Element("THONGTINHOSO").Element("DANHSACHHOSO").Element("HOSO").Add(new XElement("FILEHOSO",
                                                          new XElement("LOAIHOSO", "CT03"),
                                                          new XElement("NOIDUNGFILE", file3)
                                                          ));
                if (!String.IsNullOrEmpty(file4))
                    xEle1.Element("THONGTINHOSO").Element("DANHSACHHOSO").Element("HOSO").Add(new XElement("FILEHOSO",
                                                          new XElement("LOAIHOSO", "CT04"),
                                                          new XElement("NOIDUNGFILE", file4)
                                                          ));
                if (!String.IsNullOrEmpty(file6))
                    xEle1.Element("THONGTINHOSO").Element("DANHSACHHOSO").Element("HOSO").Add(new XElement("FILEHOSO",
                                                          new XElement("LOAIHOSO", "CT06"),
                                                          new XElement("NOIDUNGFILE", file6)
                                                          ));
                if (!String.IsNullOrEmpty(file7))
                    xEle1.Element("THONGTINHOSO").Element("DANHSACHHOSO").Element("HOSO").Add(new XElement("FILEHOSO",
                                                          new XElement("LOAIHOSO", "CT07"),
                                                          new XElement("NOIDUNGFILE", file7)
                                                          ));
                try
                {
                    xEle1.Save(path1 + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + benhNhan.SThe + "_CT_" + namqt + "_" + thanqt + ".xml");
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("Lỗi lưu file XML kiểm tra lại đường dẫn lưu file: " + path1);
                }
                string dsloi = "";
                KQGuiHSChungTu kq = new KQGuiHSChungTu();
                rs = GiamDinhBHXH.BHXH_Model.Gui_NhanDuLieu_BHXH.GuiHoSoChungTu(DungChung.Bien.xmlFilePath_LIS[10], DungChung.Bien.xmlFilePath_LIS[11], DungChung.Bien.MaBV, xEle1, ref kq, ref dsloi);
                if (rs)
                {
                    UpdateMaGD(_dataContext, kq, _hsctID);
                }
                else
                {
                    _listMessages.Add(new MessageHSCT(_maBNhan, dsloi));
                }
            }
            else
            {
                _listMessages.Add(new MessageHSCT(_maBNhan, "Không có dữ liệu"));
                return false;
            }
            return rs;
        }

        private bool CheckDL_CT03(int maBNhan, BenhNhan benhNhan, CT03 data, ref List<MessageHSCT> _listMessages)
        {
            bool rs = true;

            if (string.IsNullOrWhiteSpace(data.MA_BHXH))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập MA_BHXH"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.HO_TEN))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập HO_TEN"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.NGAY_SINH))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập NGAY_SINH"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.GIOI_TINH))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập GIOI_TINH"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.MA_DANTOC))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập MA_DANTOC"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.DIA_CHI))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập DIA_CHI"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.NGAY_VAO))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập NGAY_VAO"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.NGAY_RA))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập NGAY_RA"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.CHAN_DOAN))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập CHAN_DOAN"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.THU_TRUONG_DVI))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập THU_TRUONG_DVI"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.PP_DIEUTRI))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập PP_DIEUTRI"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.MA_KHOA))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập MA_KHOA"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.MA_CCHN_TRUONGKHOA))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập MA_CCHN_TRUONGKHOA"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.TEN_TRUONGKHOA))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập TEN_TRUONGKHOA"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.NGAY_CHUNG_TU))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập NGAY_CHUNG_TU"));
                rs = false;
            }
            if (data.TEKT == "1" && string.IsNullOrWhiteSpace(data.HO_TEN_CHA) && string.IsNullOrWhiteSpace(data.HO_TEN_ME))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Trẻ em không thẻ phải nhập họ tên cha hoặc mẹ"));
                rs = false;
            }

            return rs;
        }

        private bool CheckDL_CT04(int maBNhan, BenhNhan benhNhan, CT04 data, ref List<MessageHSCT> _listMessages)
        {
            bool rs = true;

            if (string.IsNullOrWhiteSpace(data.MA_BHXH))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập MA_BHXH"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.HO_TEN))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập HO_TEN"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.NGAY_SINH))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập NGAY_SINH"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.GIOI_TINH))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập GIOI_TINH"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.MA_DANTOC))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập MA_DANTOC"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.DIA_CHI))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập DIA_CHI"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.NGAY_VAO))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập NGAY_VAO"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.NGAY_RA))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập NGAY_RA"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.CHAN_DOAN_VAO))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập CHAN_DOAN_VAO"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.CHAN_DOAN_RA))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập CHAN_DOAN_RA"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.QT_BENHLY))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập QT_BENHLY"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.TOMTAT_KQ))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập TOMTAT_KQ"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.PP_DIEUTRI))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập PP_DIEUTRI"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.TT_RAVIEN))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập TT_RAVIEN"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.NGAY_CT))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập NGAY_CT"));
                rs = false;
            }
            if (data.TEKT == "1" && string.IsNullOrWhiteSpace(data.HO_TEN_CHA) && string.IsNullOrWhiteSpace(data.HO_TEN_ME))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Trẻ em không thẻ phải nhập họ tên cha hoặc mẹ"));
                rs = false;
            }
            if (DungChung.Ham.CalculateAgeByDate(benhNhan.NgaySinh, benhNhan.ThangSinh, benhNhan.NamSinh) < 6 && string.IsNullOrWhiteSpace(data.HO_TEN_CHA) && string.IsNullOrWhiteSpace(data.HO_TEN_ME) && string.IsNullOrWhiteSpace(data.NGUOI_GIAM_HO))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Trẻ em dưới 6 tuổi chưa nhập thông tin người thân"));
                rs = false;
            }

            return rs;
        }

        private bool CheckDL_CT06(int maBNhan, CT06 data, ref List<MessageHSCT> _listMessages)
        {
            bool rs = true;
            if (string.IsNullOrWhiteSpace(data.SO_KCB))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập SO_KCB"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.MA_BHXH))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập MA_BHXH"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.HO_TEN))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập HO_TEN"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.NGAY_SINH))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập NGAY_SINH"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.CHAN_DOAN))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập CHAN_DOAN"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.NGAY_VAO))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập NGAY_VAO"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.NGAY_RA))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập NGAY_RA"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.TEN_BS))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập TEN_BS"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.MA_BS))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập MA_BS"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.NGAY_CT))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập NGAY_CT"));
                rs = false;
            }
            return rs;
        }

        private bool CheckDL_CT07(int maBNhan, CT07 data, ref List<MessageHSCT> _listMessages)
        {
            bool rs = true;
            if (string.IsNullOrWhiteSpace(data.SO_KCB))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập SO_KCB"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.MA_BHXH))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập MA_BHXH"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.HO_TEN))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập HO_TEN"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.NGAY_SINH))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập NGAY_SINH"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.GIOI_TINH))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập GIOI_TINH"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.DON_VI))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập DON_VI"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.CHANDOAN_DIEUTRI))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập CHANDOAN_DIEUTRI"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.TU_NGAY))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập TU_NGAY"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.DEN_NGAY))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập DEN_NGAY"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.TEN_NGUOI_HANH_NGHE))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập TEN_NGUOI_HANH_NGHE)"));
                rs = false;
            }
            if (string.IsNullOrWhiteSpace(data.MA_CCHN))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Chưa nhập MA_CCHN"));
                rs = false;
            }
            if (data.TEKT == "1" && string.IsNullOrWhiteSpace(data.HO_TEN_CHA) && string.IsNullOrWhiteSpace(data.HO_TEN_ME))
            {
                _listMessages.Add(new MessageHSCT(maBNhan, "Trẻ em không thẻ phải nhập họ tên cha hoặc mẹ"));
                rs = false;
            }
            return rs;
        }

        private void UpdateMaGD(QLBV_Database.QLBVEntities dataContext, KQGuiHSChungTu kq, int hsctID)
        {
            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var hsct = dataContext.HSCT_BHXH.FirstOrDefault(o => o.ID == hsctID);
            if (hsct != null)
            {
                hsct.MA_GD = kq.MaGD;
                hsct.THOI_GIAN_GUI = DateTime.ParseExact(kq.ThoiGianTiepNhan, "yyyyMMddHHmmss",
                CultureInfo.InvariantCulture);
                hsct.IS_SEND = true;
                dataContext.SaveChanges();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            LienThongHSSK hssk = new LienThongHSSK();
            var dataSource = (List<BenhNhanADO>)gridControlChoose.DataSource;
            if (dataSource != null && dataSource.Count > 0)
            {
                List<BenhNhanADO> listMaBNhan = new List<BenhNhanADO>();
                List<string> listErrors = new List<string>();
                int i = 0;
                int j = 0;
                foreach (var item in dataSource)
                {
                    List<MessageHSCT> listMessages = new List<MessageHSCT>();
                    if (SendHSCT(dataContext, item.HSCT_ID, item.LOAI, item.MaBNhan, DungChung.Bien.xmlFilePath_LIS[7], DungChung.Bien.xmlFilePath_LIS[9], ref listMessages))
                    {
                        i++;
                        listMaBNhan.Add(item);
                    }
                    else
                    {
                        if (listSelecteds.Exists(o => o.MaBNhan == item.MaBNhan))
                        {
                            listSelecteds.FirstOrDefault(o => o.MaBNhan == item.MaBNhan).Error = string.Join(Environment.NewLine, listMessages.Select(o => o.Message));
                        }
                        j++;
                    }
                }
                gridControlSend.BeginUpdate();
                gridControlSend.DataSource = listMaBNhan;
                gridControlSend.EndUpdate();
                XtraMessageBox.Show(string.Format("Gửi thành công {0} bệnh nhân." + Environment.NewLine + "Gửi thất bại {1} bệnh nhân.", i, j));
                listSelecteds = listSelecteds.Where(o => !listMaBNhan.Select(p => p.MaBNhan).Contains(o.MaBNhan)).ToList();
                gridControlChoose.BeginUpdate();
                gridControlChoose.DataSource = listSelecteds;
                gridControlChoose.EndUpdate();
                LoadDataToForm();
                txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
            }
        }

        private void gridViewChoose_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            var row = (BenhNhanADO)gridViewChoose.GetRow(gridViewChoose.GetRowHandle(e.ListSourceRowIndex));
            if (row != null)
            {
                if (e.IsGetData && e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Object)
                {
                    if (e.Column.FieldName == "GioiTinh")
                        e.Value = row.GTinh == 1 ? "Nam" : "Nữ";
                    else if (e.Column.FieldName == "Tuoi_Str")
                        e.Value = DungChung.Ham.CalculateAgeByDate(row.NgaySinh, row.ThangSinh, row.NamSinh);
                }
            }
        }

        private void gridViewSearch_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            var row = (BenhNhanADO)gridViewSearch.GetRow(gridViewSearch.GetRowHandle(e.ListSourceRowIndex));
            if (row != null)
            {
                if (e.IsGetData && e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Object)
                {
                    if (e.Column.FieldName == "GioiTinh")
                        e.Value = row.GTinh == 1 ? "Nam" : "Nữ";
                    else if (e.Column.FieldName == "Tuoi_Str")
                        e.Value = DungChung.Ham.CalculateAgeByDate(row.NgaySinh, row.ThangSinh, row.NamSinh);
                }
            }
        }

        private void gridViewSend_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            var row = (BenhNhanADO)gridViewSend.GetRow(gridViewSend.GetRowHandle(e.ListSourceRowIndex));
            if (row != null)
            {
                if (e.IsGetData && e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Object)
                {
                    if (e.Column.FieldName == "GioiTinh")
                        e.Value = row.GTinh == 1 ? "Nam" : "Nữ";
                    else if (e.Column.FieldName == "Tuoi_Str")
                        e.Value = DungChung.Ham.CalculateAgeByDate(row.NgaySinh, row.ThangSinh, row.NamSinh);
                }
            }
        }

        private void repositoryItemButtonEdit_Delete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = (BenhNhanADO)gridViewChoose.GetFocusedRow();
            if (row != null)
            {
                listSelecteds.Remove(row);
                if (listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan) != null)
                {
                    listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan).Check = false;
                }
                gridControlChoose.BeginUpdate();
                gridControlChoose.DataSource = listSelecteds;
                gridControlChoose.EndUpdate();
                txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
            }
        }

        public class MyGridLocalizer : GridLocalizer
        {
            public override string GetLocalizedString(GridStringId id)
            {
                switch (id)
                {
                    case GridStringId.FindControlFindButton:
                        return "Tìm kiếm";
                    case GridStringId.FindControlClearButton:
                        return "Xóa";
                    case GridStringId.FilterPanelCustomizeButton:
                        return "Lọc";
                    default:
                        return base.GetLocalizedString(id);
                }
            }
        }

        public class BenhNhanADO : BenhNhan
        {
            public bool Check { get; set; }
            public string Error { get; set; }
            public bool Is_Send { get; set; }
            public string MaGD_HSCT { get; set; }
            public int HSCT_ID { get; set; }
            public int LOAI { get; set; }
            public BenhNhanADO() { }
            public BenhNhanADO(BenhNhan data)
            {
                LibraryStore.Mapper.DataObjectMapper.Map<BenhNhanADO>(this, data);
            }
        }

        private void gridViewSearch_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var row = (BenhNhanADO)gridViewSearch.GetRow(e.RowHandle);
            if (row != null && e.Column.FieldName == "Check")
            {
                ChooseRow(e, row);
                gridControlChoose.BeginUpdate();
                gridControlChoose.DataSource = listSelecteds;
                gridControlChoose.EndUpdate();
            }
        }

        private void ChooseRow(DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e, BenhNhanADO row)
        {
            if (listSelecteds.Exists(o => o.MaBNhan == row.MaBNhan))
            {
                if ((bool)e.Value)
                {
                    row.Check = (bool)e.Value;
                    listSelecteds.Add(row);
                }
                else
                {
                    listSelecteds = listSelecteds.Where(o => o.MaBNhan != row.MaBNhan).ToList();
                }
            }
            else
            {
                row.Check = (bool)e.Value;
                listSelecteds.Add(row);
            }
            if (listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan) != null)
                listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan).Check = (bool)e.Value;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Condition();
            }
        }

        private void dtTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataToForm();
            txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }

        private void dtDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataToForm();
            txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }

        private void gridViewChoose_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            var row = (BenhNhanADO)gridViewChoose.GetRow(e.RowHandle);
            if (row != null)
            {
                if (e.Column.FieldName == "Error")
                {
                    e.RepositoryItem = string.IsNullOrWhiteSpace(row.Error) ? repositoryItemButtonEdit_Error_Disable : repositoryItemButtonEdit_Error_Enable;
                }
            }
        }

        private void repositoryItemButtonEdit_Error_Enable_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = (BenhNhanADO)gridViewChoose.GetFocusedRow();
            if (row != null)
            {
                frm_DS_HSSK_Error frm = new frm_DS_HSSK_Error(row);
                frm.ShowDialog();
            }
        }

        bool IsCheckAll = false;
        private void gridViewSearch_MouseDown(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                GridView view = sender as GridView;
                GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
                GridHitInfo hi = view.CalcHitInfo(e.Location);

                if (hi.HitTest == GridHitTest.Column)
                {
                    if (hi.Column.FieldName == "Check")
                    {
                        gridControlSearch.BeginUpdate();
                        gridControlChoose.BeginUpdate();
                        if (IsCheckAll)
                        {
                            hi.Column.Image = imageList1.Images[0];
                            var dataSource = (List<BenhNhanADO>)gridControlSearch.DataSource;
                            if (dataSource != null)
                            {
                                foreach (BenhNhanADO row in dataSource)
                                {
                                    if (row.Is_Send) break;
                                    listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan).Check = false;
                                    var checkChoose = listSelecteds.FirstOrDefault(o => o.MaBNhan == row.MaBNhan);
                                    if (checkChoose != null)
                                    {
                                        listSelecteds = listSelecteds.Where(o => o.MaBNhan != row.MaBNhan).ToList();
                                    }
                                }
                                gridControlSearch.DataSource = listAll.Where(o => o.Is_Send == chkDaGui.Checked).ToList();
                                IsCheckAll = false;
                            }
                        }
                        else
                        {
                            hi.Column.Image = imageList1.Images[1];
                            var dataSource = (List<BenhNhanADO>)gridControlSearch.DataSource;
                            if (dataSource != null)
                            {
                                foreach (BenhNhanADO row in dataSource)
                                {
                                    if (row.Is_Send) break;
                                    listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan).Check = true;
                                    var checkChoose = listSelecteds.FirstOrDefault(o => o.MaBNhan == row.MaBNhan);
                                    if (checkChoose == null)
                                    {
                                        listSelecteds.Add(row);
                                    }
                                }
                                gridControlSearch.DataSource = listAll.Where(o => o.Is_Send == chkDaGui.Checked).ToList();
                                IsCheckAll = true;
                            }
                        }
                        gridControlChoose.DataSource = listSelecteds;
                        gridControlSearch.EndUpdate();
                        gridControlChoose.EndUpdate();
                    }
                }
            }
        }

        private void chkDaGui_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDaGui.Checked)
                listAll.ForEach(o => { o.Check = false; });
            listSelecteds = listSelecteds.Where(o => o.Is_Send == chkDaGui.Checked).ToList();
            gridControlChoose.BeginUpdate();
            gridControlChoose.DataSource = listSelecteds;
            gridControlChoose.EndUpdate();
            Search_Condition();
            btnSend.Enabled = !chkDaGui.Checked;
            btnCancel.Enabled = chkDaGui.Checked;
        }

        private void Search_Condition()
        {
            List<BenhNhanADO> search = new List<BenhNhanADO>();

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                search = listAll.Where(o => (o.MaBNhan.ToString() == txtSearch.Text || o.TenBNhan.ToLower().Contains(txtSearch.Text.ToLower()) || o.DChi.ToLower().Contains(txtSearch.Text.ToLower())) && (o.Is_Send == chkDaGui.Checked)).ToList();
            }
            else
                search = listAll.Where(o => o.Is_Send == chkDaGui.Checked).ToList();
            gridControlSearch.BeginUpdate();
            gridControlSearch.DataSource = search;
            gridControlSearch.EndUpdate();
        }

        private void gridViewSearch_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            var row = (BenhNhanADO)gridViewSearch.GetRow(e.RowHandle);
            if (row != null)
            {
                if (e.Column.FieldName == "Check")
                {
                    e.RepositoryItem = repositoryItemCheckEdit_Check;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var dataSource = (List<BenhNhanADO>)gridControlChoose.DataSource;
            if (dataSource != null && dataSource.Count > 0)
            {
                QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                foreach (var item in dataSource)
                {
                    var hsct = dataContext.HSCT_BHXH.FirstOrDefault(o => o.ID == item.HSCT_ID);
                    if (hsct != null)
                    {
                        hsct.MA_GD = null;
                        hsct.THOI_GIAN_GUI = null;
                        hsct.IS_SEND = null;
                    }
                }
                if (dataContext.SaveChanges() > 0)
                {
                    MessageBox.Show("Hủy thành công!");
                    listSelecteds = new List<BenhNhanADO>();
                    gridControlChoose.BeginUpdate();
                    gridControlChoose.DataSource = listSelecteds;
                    gridControlChoose.EndUpdate();
                    LoadDataToForm();
                    txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
                }
            }
        }

        private void radioGroupLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSelecteds = new List<BenhNhanADO>();
            gridControlChoose.BeginUpdate();
            gridControlChoose.DataSource = listSelecteds;
            gridControlChoose.EndUpdate();
            LoadDataToForm();
            txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }
    }
}
