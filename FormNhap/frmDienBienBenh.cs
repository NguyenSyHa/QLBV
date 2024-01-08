using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class frmDienBienBenh : DevExpress.XtraEditors.XtraForm
    {
        int _int_maBN = 0;
        public frmDienBienBenh(int mabn)
        {
            InitializeComponent();
            _int_maBN = mabn;
        }
        QLBV_Database.QLBVEntities DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DienBien> _ldienbien = new List<DienBien>();

        private void frmDienBienBenh_Load(object sender, EventArgs e)
        {
            DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dedenngaydb.DateTime = DungChung.Ham.NgayDen(DateTime.Now);
            var bn = DaTaContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList();
            detungaydb.DateTime = DungChung.Ham.NgayTu(bn.First().NNhap.Value);
            if (DungChung.Bien.MaBV == "30007")
            {
                detungaydb.Properties.DisplayFormat.FormatString = "dd/MM/yyy HH:mm";
                detungaydb.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                detungaydb.Properties.EditFormat.FormatString = "dd/MM/yyy HH:mm";
                detungaydb.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                detungaydb.Properties.Mask.EditMask = "dd/MM/yyy HH:mm";

                dedenngaydb.Properties.DisplayFormat.FormatString = "dd/MM/yyy HH:mm";
                dedenngaydb.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dedenngaydb.Properties.EditFormat.FormatString = "dd/MM/yyy HH:mm";
                dedenngaydb.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dedenngaydb.Properties.Mask.EditMask = "dd/MM/yyy HH:mm";
            }
            _ldienbien = DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN).Where(p => p.Ploai == 0).OrderByDescending(p => p.ID).ToList();
            binsDienBien.DataSource = _ldienbien;
            grcDienBien.DataSource = binsDienBien;
            _lCanBo = DaTaContext.CanBoes.OrderBy(p => p.TenCB).ToList();
            lupMaCB_dienBien.DataSource = _lCanBo;
            simpleButton2.Enabled = false;
            simpleButton5.Enabled = false;
            simpleButton3.Enabled = true;

        }

        private void btngetKQCLS_Click(object sender, EventArgs e)
        {
            int rs;
            if (DialogResult.Yes == MessageBox.Show("Lấy kết quả xét nghiệm vào mục diễn biến bệnh?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                var xetnghiem = (from cls in DaTaContext.CLS.Where(p => p.MaBNhan == _int_maBN)
                                 join cd in DaTaContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                 join clsct in DaTaContext.CLScts on cd.IDCD equals clsct.IDCD
                                 join dvct in DaTaContext.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                 join dv in DaTaContext.DichVus on dvct.MaDV equals dv.MaDV
                                 join tn in DaTaContext.TieuNhomDVs.Where(p => p.IDNhom == 1 || p.IDNhom == 2) on dv.IdTieuNhom equals tn.IdTieuNhom //p.TenRG.Contains("XN")
                                 select new { cls.MaCBth, dv.TenDV, clsct.KetQua, cls.MaCB, cd.Status, cls.NgayTH, tn.IDNhom, cls.NgayThang, dvct.TenDVct, TenDVctkq = dvct.TenDVct + ": " + clsct.KetQua + " " + dvct.DonVi, cls.IdCLS, kqcdha = dv.TenDV + ":" + cd.KetLuan }).OrderBy(p => p.IdCLS).ToList();
                var ketqua = (from a in xetnghiem.Where(p => p.Status == 1 && p.IDNhom == 1)
                              group new { a } by new { a.NgayTH, a.MaCBth } into kq
                              select new
                              {
                                  kq.Key,
                                  TenXN = string.Join("\n", kq.Select(p => p.a.TenDVctkq).ToArray()),
                              }).ToList();
                var chidinh = (from a in xetnghiem.Where(p => p.IDNhom == 1)
                               group new { a } by new { a.NgayThang, a.MaCB } into kq
                               select new
                               {
                                   kq.Key,
                                   TenXN = (DungChung.Bien.MaBV == "30007" && (kq.Select(p => p.a.TenDV).ToList().FirstOrDefault().ToLower().Contains("tổng phân tích tế bào máu ngoại vi") || kq.Select(p => p.a.TenDV).ToList().FirstOrDefault().ToLower().Contains("tổng phân tích nước tiểu"))) ? kq.Select(p => p.a.TenDV).ToList().FirstOrDefault() : string.Join(",\n ", kq.Select(p => p.a.TenDVct).ToArray()),
                               }).ToList();
                var Ktrabn = DaTaContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).Where(p => p.NoiTru == 0 && p.DTNT == false).ToList();
                string KqDienBienMau = "";
                if (DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30280")
                    KqDienBienMau = "Bệnh nhân tỉnh, tiếp xúc tốt \nDa, niêm mạc hồng \nTình trạng phù: \nMạch...............lần/phút \nHuyết áp.......................mmHg \nNhiệt độ.............°C \nTim: đều, rõ không có tiếng thối \nPhổi:................... \nBụng mềm, không chướng \nKhông liệt, hội chứng não-màng não: âm tính";
                var dbbenh = DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN && p.NgayNhap != null).ToList();
                int i = 0;
                foreach (var item in chidinh)
                {
                    if (dbbenh.Where(p => p.NgayNhap == item.Key.NgayThang).FirstOrDefault() == null)
                    {
                        DienBien dbmoi = new DienBien();
                        dbmoi.MaBNhan = _int_maBN;
                        dbmoi.NgayNhap = item.Key.NgayThang;
                        dbmoi.DienBien1 = (Ktrabn.Count > 0 && i == 0) ? KqDienBienMau : "";
                        dbmoi.YLenh = "Chỉ định xét nghiệm: \n" + item.TenXN;
                        dbmoi.MaCB = item.Key.MaCB;
                        DaTaContext.DienBiens.Add(dbmoi);
                        DaTaContext.SaveChanges();
                        i++;
                    }
                }

                if (Ktrabn.Count > 0)
                {
                    if (DialogResult.Yes == MessageBox.Show("Bệnh nhân ngoại trú \nBạn có muốn lấy cả chi tiết đơn thuốc vào diễn biến bệnh", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        var _ldichvu = DaTaContext.DichVus.Where(p => p.PLoai == 1).ToList();
                        var _lDThuocct = (from dt in DaTaContext.DThuocs.Where(p => p.PLDV == 1 && p.MaBNhan == _int_maBN)
                                          join dtct in DaTaContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                          select new { dtct.MaDV, dtct.SoLuong, dt.MaCB, dtct.DonVi, HuongDan = dtct.DuongD + dtct.SoLan + dtct.MoiLan + dtct.Luong + dtct.DviUong + dtct.GhiChu, dt.NgayKe }).ToList();
                        var _lketqua = (from dv in _ldichvu
                                        join dt in _lDThuocct on dv.MaDV equals dt.MaDV
                                        group new { dv, dt } by new { dv.TenDV, dt.MaDV, dt.HuongDan, dt.NgayKe, dt.DonVi, dt.MaCB, dv.HamLuong } into kq
                                        select new
                                        {
                                            kq.Key.NgayKe,
                                            kq.Key.MaDV,
                                            kq.Key.MaCB,
                                            TenDV = kq.Key.TenDV + (DungChung.Bien.MaBV == "30007" ? ("/ " + kq.Key.HamLuong) : "") + " x " + kq.Sum(p => p.dt.SoLuong) + "-" + kq.Key.DonVi + "\n" + kq.Key.HuongDan
                                        }).ToList();
                        string Ylenh = "";
                        foreach (var item in _lketqua)
                        {
                            Ylenh += item.TenDV + "\n";
                        }

                        if (ketqua.Count > 0)
                        {
                            int a = 0;
                            foreach (var item in ketqua)
                            {
                                if (dbbenh.Where(p => p.NgayNhap == item.Key.NgayTH).FirstOrDefault() == null)
                                {
                                    DienBien dbmoi = new DienBien();
                                    dbmoi.MaBNhan = _int_maBN;
                                    dbmoi.NgayNhap = item.Key.NgayTH;
                                    dbmoi.DienBien1 = "Sao xét nghiệm: \n" + item.TenXN;
                                    dbmoi.YLenh = a == 0 ? "Chi tiết đơn thuốc: \n" + Ylenh : "";
                                    dbmoi.MaCB = item.Key.MaCBth;// (DungChung.Bien.MaBV == "30003" && _lketqua.Count > 0) ? (item.Key.MaCBth + ";" + _lketqua.First().MaCB) : item.Key.MaCBth;
                                    DaTaContext.DienBiens.Add(dbmoi);
                                    DaTaContext.SaveChanges();
                                    a++;
                                }
                            }
                        }
                        else
                        {
                            if (_lketqua.Count > 0)
                            {
                                DienBien dbmoi = new DienBien();
                                dbmoi.MaBNhan = _int_maBN;
                                dbmoi.NgayNhap = _lketqua.Count() > 0 ? _lketqua.First().NgayKe : System.DateTime.Now;
                                dbmoi.DienBien1 = (DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30280") ? KqDienBienMau : "";
                                dbmoi.YLenh = "Chi tiết đơn thuốc: \n" + Ylenh;
                                dbmoi.MaCB = _lDThuocct.Count() > 0 ? _lDThuocct.First().MaCB : "";

                                DaTaContext.DienBiens.Add(dbmoi);
                                DaTaContext.SaveChanges();
                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {
                        foreach (var item in ketqua)
                        {
                            if (dbbenh.Where(p => p.NgayNhap == item.Key.NgayTH).FirstOrDefault() == null)
                            {
                                DienBien dbmoi = new DienBien();
                                dbmoi.MaBNhan = _int_maBN;
                                dbmoi.NgayNhap = item.Key.NgayTH;
                                dbmoi.DienBien1 = "Sao xét nghiệm: \n" + item.TenXN;
                                dbmoi.MaCB = item.Key.MaCBth;
                                DaTaContext.DienBiens.Add(dbmoi);
                                DaTaContext.SaveChanges();
                            }
                        }
                    }
                    if (DialogResult.Yes == MessageBox.Show("Bệnh nhân ngoại trú \nBạn có muốn lấy cả kết quả CĐHA vào diễn biến bệnh", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        var chidinhcdha = (from a in xetnghiem.Where(p => p.IDNhom == 2)
                                           group new { a } by new { a.NgayThang, a.MaCB, a.TenDV } into kq
                                           select new
                                           {
                                               kq.Key,
                                               TenXN = kq.Key.TenDV,
                                           }).ToList();
                        var ketquacdha = (from a in xetnghiem.Where(p => p.Status == 1 && p.IDNhom == 2)
                                          group new { a } by new { a.NgayThang, a.MaCBth } into kq
                                          select new
                                          {
                                              kq.Key,
                                              TenXN = string.Join("\n", kq.Select(p => p.a.kqcdha).ToArray())
                                          }).ToList();
                        foreach (var item in chidinhcdha)
                        {
                            if (dbbenh.Where(p => p.NgayNhap == item.Key.NgayThang).FirstOrDefault() == null)
                            {
                                DienBien dbmoi = new DienBien();
                                dbmoi.MaBNhan = _int_maBN;
                                dbmoi.NgayNhap = item.Key.NgayThang;
                                dbmoi.DienBien1 = "";
                                dbmoi.YLenh = "Chỉ định chẩn đoán hình ảnh: \n" + item.TenXN;
                                dbmoi.MaCB = item.Key.MaCB;
                                DaTaContext.DienBiens.Add(dbmoi);
                                DaTaContext.SaveChanges();
                                //i++;
                            }
                        }
                        foreach (var item in ketquacdha)
                        {
                            if (dbbenh.Where(p => p.NgayNhap == item.Key.NgayThang).FirstOrDefault() == null)
                            {
                                DienBien dbmoi = new DienBien();
                                dbmoi.MaBNhan = _int_maBN;
                                dbmoi.NgayNhap = item.Key.NgayThang;
                                dbmoi.DienBien1 = "Sao chẩn đoán hình ảnh: \n" + item.TenXN;
                                dbmoi.MaCB = item.Key.MaCBth;
                                DaTaContext.DienBiens.Add(dbmoi);
                                DaTaContext.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in ketqua)
                    {
                        if (dbbenh.Where(p => p.NgayNhap == item.Key.NgayTH).FirstOrDefault() == null)
                        {
                            DienBien dbmoi = new DienBien();
                            dbmoi.MaBNhan = _int_maBN;
                            dbmoi.NgayNhap = item.Key.NgayTH;
                            dbmoi.DienBien1 = "Sao xét nghiệm: \n" + item.TenXN;
                            dbmoi.MaCB = item.Key.MaCBth;
                            DaTaContext.DienBiens.Add(dbmoi);
                            DaTaContext.SaveChanges();
                        }
                    }
                    //}
                }

            }
            frmDienBienBenh_Load(sender, e);
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)//namnt (y/c a quý _ 20/4/2018) thêm lấy kết luận chẩn đoán hình ảnh
        {
            int rs;
            if (DialogResult.Yes == MessageBox.Show("Lấy kết quả chẩn đoán hình ảnh vào mục diễn biến bệnh?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                //if (DungChung.Bien.MaBV == "30007")
                //{
                //    LayCDHA30007(DaTaContext, _int_maBN);
                //}
                //else
                //{
                var xetnghiem = (from cls in DaTaContext.CLS.Where(p => p.MaBNhan == _int_maBN)
                                 join cd in DaTaContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                 join dv in DaTaContext.DichVus on cd.MaDV equals dv.MaDV
                                 join tn in DaTaContext.TieuNhomDVs.Where(p => p.IDNhom == 1 || p.IDNhom == 2) on dv.IdTieuNhom equals tn.IdTieuNhom //p.TenRG.Contains("XN")
                                 select new { cls.MaCBth, dv.TenDV, KetQua = "", cls.MaCB, cd.Status, cls.NgayTH, tn.IDNhom, cls.NgayThang, TenDVct = "", TenDVctkq = "", cls.IdCLS, kqcdha = dv.TenDV + ":" + cd.KetLuan }).OrderBy(p => p.IdCLS).ToList();
                var ketqua = (from a in xetnghiem.Where(p => p.Status == 1 && p.IDNhom == 2)
                              group new { a } by new { a.NgayTH, a.MaCBth } into kq
                              select new
                              {
                                  kq.Key,
                                  TenXN = string.Join("\n", kq.Select(p => p.a.kqcdha).ToArray()),
                              }).ToList();
                var chidinh = (from a in xetnghiem.Where(p => p.IDNhom == 2)
                               group new { a } by new { a.NgayThang, a.MaCB } into kq
                               select new
                               {
                                   kq.Key,
                                   TenXN = string.Join(",\n ", kq.Select(p => p.a.TenDV).ToArray()),
                               }).ToList();
                var Ktrabn = DaTaContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).Where(p => p.NoiTru == 0 && p.DTNT == false).ToList();
                string KqDienBienMau = "";
                //if (DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30280")
                //    KqDienBienMau = "Bệnh nhân tỉnh, tiếp xúc tốt \nDa, niêm mạc hồng \nTình trạng phù: \nMạch...............lần/phút \nHuyết áp.......................mmHg \nNhiệt độ.............°C \nTim: đều, rõ không có tiếng thối \nPhổi:................... \nBụng mềm, không chướng \nKhông liệt, hội chứng não-màng não: âm tính";
                var dbbenh = DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN && p.NgayNhap != null).ToList();
                int i = 0;
                foreach (var item in chidinh)
                {
                    if (dbbenh.Where(p => p.NgayNhap == item.Key.NgayThang).FirstOrDefault() == null)
                    {
                        DienBien dbmoi = new DienBien();
                        dbmoi.MaBNhan = _int_maBN;
                        dbmoi.NgayNhap = item.Key.NgayThang;
                        dbmoi.DienBien1 = "";
                        dbmoi.YLenh = "Chỉ định chẩn đoán hình ảnh: \n" + item.TenXN;
                        dbmoi.MaCB = item.Key.MaCB;
                        DaTaContext.DienBiens.Add(dbmoi);
                        DaTaContext.SaveChanges();
                        //i++;
                    }
                }
                foreach (var item in ketqua)
                {
                    if (dbbenh.Where(p => p.NgayNhap == item.Key.NgayTH).FirstOrDefault() == null)
                    {
                        DienBien dbmoi = new DienBien();
                        dbmoi.MaBNhan = _int_maBN;
                        dbmoi.NgayNhap = item.Key.NgayTH;
                        dbmoi.DienBien1 = "Sao chẩn đoán hình ảnh: \n" + item.TenXN;
                        dbmoi.MaCB = item.Key.MaCBth;
                        DaTaContext.DienBiens.Add(dbmoi);
                        DaTaContext.SaveChanges();
                    }
                    //}
                }
            }
            frmDienBienBenh_Load(sender, e);
        }

        private void btnLayKQTT_Click(object sender, EventArgs e)
        {
            int rs;
            if (DialogResult.Yes == MessageBox.Show("Lấy kết quả thủ thuật - phẫu thuật vào mục diễn biến bệnh?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                var xetnghiem = (from cls in DaTaContext.CLS.Where(p => p.MaBNhan == _int_maBN)
                                 join cd in DaTaContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                 join clsct in DaTaContext.CLScts on cd.IDCD equals clsct.IDCD
                                 join dvct in DaTaContext.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                 join dv in DaTaContext.DichVus on dvct.MaDV equals dv.MaDV
                                 join tn in DaTaContext.TieuNhomDVs.Where(p => p.IDNhom == 8) on dv.IdTieuNhom equals tn.IdTieuNhom //p.TenRG.Contains("XN")
                                 select new { cd.MaCBth, dv.TenDV, dv.MaDV, clsct.KetQua, cls.MaCB, cd.Status, cls.NgayTH, tn.IDNhom, cls.NgayThang, dvct.TenDVct, TenDVctkq = clsct.KetQua, cls.IdCLS, kqcdha = dv.TenDV }).OrderBy(p => p.IdCLS).ToList();
                var ketqua = (from a in xetnghiem
                              group new { a } by new { a.NgayThang, a.MaCB, a.TenDV, a.TenDVctkq } into kq
                              select new
                              {
                                  kq.Key.NgayThang,
                                  kq.Key.MaCB,
                                  TenXN = kq.Key.TenDV,//string.Join("\n", kq.Select(p => p.a.TenDVctkq).ToArray()),
                                  KetQua = kq.Key.TenDVctkq
                              }).ToList();
                var dbbenh = DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN && p.NgayNhap != null).ToList();
                foreach (var item in ketqua)
                {
                    if (dbbenh.Where(p => p.NgayNhap == item.NgayThang).FirstOrDefault() == null)
                    {
                        DienBien dbmoi = new DienBien();
                        dbmoi.MaBNhan = _int_maBN;
                        dbmoi.NgayNhap = item.NgayThang;
                        //dbmoi.DienBien1 = item.KetQua != null ? item.KetQua.ToString() : ""; //(Ktrabn.Count > 0 && i == 0) ? KqDienBienMau : "";
                        dbmoi.YLenh = "Chỉ định thủ thuật - phẫu thuật: \n" + item.TenXN;
                        dbmoi.MaCB = item.MaCB;
                        DaTaContext.DienBiens.Add(dbmoi);
                        DaTaContext.SaveChanges();
                    }
                }
            }
            frmDienBienBenh_Load(sender, e);
        }

        #region PhieuDieuTri
        public class DienBienYHCT
        {
            public string ThucHienYL { get; set; }
            public string HuongDtri { get; set; }
            public string DienBien1 { get; set; }
            public string MaCB { get; set; }
            public string YLenh { get; set; }
            public string TenCB { get; set; }
            public DateTime NgayNhap { get; set; }
        }
        public bool _PhieuDieuTri_YHCT(int _int_maBN)
        {
            try
            {
                if (_int_maBN > 0)
                {
                    DateTime tungay = DungChung.Ham.NgayTu(detungaydb.DateTime);
                    DateTime Denngay = DungChung.Ham.NgayDen(dedenngaydb.DateTime);
                    if (DungChung.Bien.MaBV == "20001")
                    {
                        List<DienBienYHCT> _lkq = new List<DienBienYHCT>();
                        List<CanBo> _lcb = new List<CanBo>();
                        _lcb = DaTaContext.CanBoes.ToList();
                        frmIn frm = new frmIn();
                        Phieu.rep_ToDieuTri_YHCT rep = new Phieu.rep_ToDieuTri_YHCT();
                        var dt = (from a in DaTaContext.DThuocs.Where(p => p.MaBNhan == _int_maBN)
                                  join b in DaTaContext.DThuoccts on a.IDDon equals b.IDDon
                                  join c in DaTaContext.DichVus on b.MaDV equals c.MaDV
                                  select new { a.IDDon, Loai = c.DongY == 1 ? 1 : 0 }).Distinct().ToList();
                        var dienbien1 = (from a in dt
                                         join db in DaTaContext.DienBiens.Where(p => p.IDDon != null && p.IDDon >= 0).Where(p => p.MaBNhan == _int_maBN).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= Denngay) on a.IDDon equals db.IDDon
                                         join cb in DaTaContext.CanBoes on db.MaCB equals cb.MaCB
                                         select new { a.Loai, db.ID, db.IDDon, db.ThucHienYL, db.HuongDTri, db.MaBNhan, DienBien1 = a.Loai == 0 ? db.DienBien1 : "", db.MaCB, db.YLenh, db.NgayNhap, TenCB = cb.CapBac + ": " + cb.TenCB }).OrderBy(p => p.Loai).ToList();
                        var dienbien2 = (from a in dienbien1
                                         group a by new { a.MaBNhan, NgayNhap = a.NgayNhap.Value.Date, a.ID, a.IDDon, a.ThucHienYL, a.HuongDTri, a.DienBien1, a.MaCB, a.YLenh, a.TenCB } into kq//, a.TenCB
                                         select new { kq.Key.MaBNhan, NgayNhap = kq.Key.NgayNhap, kq.Key.ID, kq.Key.IDDon, kq.Key.TenCB, kq.Key.ThucHienYL, kq.Key.YLenh, kq.Key.DienBien1, kq.Key.HuongDTri, kq.Key.MaCB }).ToList();
                        var dienbien3 = (from a in dienbien2
                                         group a by new { a.MaBNhan, a.NgayNhap, a.MaCB, a.TenCB } into kq
                                         select new DienBienYHCT
                                         {
                                             ThucHienYL = string.Join("\n", kq.Select(p => (p.ThucHienYL != "" && p.ThucHienYL != null) ? (p.ThucHienYL) : "").ToArray()),
                                             HuongDtri = string.Join("\n", kq.Select(p => (p.HuongDTri != "" && p.HuongDTri != null) ? (p.HuongDTri) : "").ToArray()),
                                             //MaBNhan = _int_maBN,
                                             DienBien1 = string.Join("\n", kq.Select(p => (p.DienBien1 != "" && p.DienBien1 != null) ? (p.DienBien1) : "").ToArray()),
                                             MaCB = kq.Key.MaCB,//string.Join("\n", kq.Select(p => (p.MaCB != "" && p.MaCB != null) ? (p.MaCB) : "").ToArray()),
                                             YLenh = string.Join("\n", kq.Select(p => (p.YLenh != "" && p.YLenh != null) ? ("\n" + p.YLenh) : "").ToArray()),
                                             NgayNhap = kq.Key.NgayNhap,
                                             TenCB = kq.Key.TenCB//string.Join("\n", kq.Select(p => p.YLenh.Contains("Chỉ định") ? (p.TenCB) : "").ToArray()),
                                         }).OrderBy(p => p.NgayNhap).ToList();

                        var q1 = (from db in DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN).Where(p => p.IDDon == null || p.IDDon <= 0).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= Denngay)
                                  join cb in DaTaContext.CanBoes on db.MaCB equals cb.MaCB
                                  select new { db.ID, db.IDDon, db.ThucHienYL, db.HuongDTri, db.MaBNhan, DienBien1 = db.DienBien1, db.MaCB, db.YLenh, db.NgayNhap, TenCB = cb.CapBac + ": " + cb.TenCB }).ToList();
                        var q2 = (from a in q1
                                  group a by new { a.MaBNhan, NgayNhap = a.NgayNhap.Value.Date, a.ID, a.IDDon, a.ThucHienYL, a.HuongDTri, a.DienBien1, a.MaCB, a.YLenh, a.TenCB } into kq//, a.TenCB
                                  select new { kq.Key.MaBNhan, NgayNhap = kq.Key.NgayNhap, kq.Key.ID, kq.Key.IDDon, kq.Key.TenCB, kq.Key.ThucHienYL, kq.Key.YLenh, kq.Key.DienBien1, kq.Key.HuongDTri, kq.Key.MaCB }).ToList();
                        var q3 = (from a in q2
                                  group a by new { a.MaBNhan, a.NgayNhap, a.MaCB, a.TenCB } into kq
                                  select new DienBienYHCT
                                  {
                                      ThucHienYL = string.Join("\n", kq.Select(p => (p.ThucHienYL != "" && p.ThucHienYL != null) ? (p.ThucHienYL) : "").ToArray()),
                                      HuongDtri = string.Join("\n", kq.Select(p => (p.HuongDTri != "" && p.HuongDTri != null) ? (p.HuongDTri) : "").ToArray()),
                                      //MaBNhan = _int_maBN,
                                      DienBien1 = string.Join("\n", kq.Select(p => (p.DienBien1 != "" && p.DienBien1 != null) ? (p.DienBien1) : "").ToArray()),
                                      MaCB = kq.Key.MaCB,//string.Join("\n", kq.Select(p => (p.MaCB != "" && p.MaCB != null) ? (p.MaCB) : "").ToArray()),
                                      YLenh = string.Join("\n", kq.Select(p => (p.YLenh != "" && p.YLenh != null) ? ("\n" + p.YLenh) : "").ToArray()),
                                      NgayNhap = kq.Key.NgayNhap,
                                      TenCB = kq.Key.TenCB//string.Join("\n", kq.Select(p => p.YLenh.Contains("Chỉ định") ? (p.TenCB) : "").ToArray()),
                                  }).OrderBy(p => p.NgayNhap).ToList();
                        _lkq.AddRange(dienbien3);
                        _lkq.AddRange(q3);
                        var q5 = (from a in _lkq
                                  group a by new { NgayNhap = a.NgayNhap.Date, a.ThucHienYL, a.DienBien1, a.MaCB, a.YLenh, a.TenCB, a.HuongDtri } into kq//, a.TenCB
                                  select new { NgayNhap = kq.Key.NgayNhap, kq.Key.TenCB, kq.Key.ThucHienYL, kq.Key.YLenh, kq.Key.DienBien1, kq.Key.HuongDtri, kq.Key.MaCB }).ToList();
                        var q4 = (from a in q5
                                  group a by new { a.TenCB, a.MaCB, a.NgayNhap } into kq
                                  select new DienBienYHCT
                                  {
                                      ThucHienYL = string.Join("\n", kq.Select(p => (p.ThucHienYL != "" && p.ThucHienYL != null) ? (p.ThucHienYL) : "").ToArray()),
                                      HuongDtri = string.Join("\n", kq.Select(p => (p.HuongDtri != "" && p.HuongDtri != null) ? (p.HuongDtri) : "").ToArray()),
                                      //MaBNhan = _int_maBN,
                                      DienBien1 = string.Join("\n", kq.Select(p => (p.DienBien1 != "" && p.DienBien1 != null) ? (p.DienBien1) : "").ToArray()),
                                      MaCB = kq.Key.MaCB,//string.Join("\n", kq.Select(p => (p.MaCB != "" && p.MaCB != null) ? (p.MaCB) : "").ToArray()),
                                      YLenh = string.Join("\n", kq.Select(p => (p.YLenh != "" && p.YLenh != null) ? ("\n" + p.YLenh) : "").ToArray()),
                                      NgayNhap = kq.Key.NgayNhap,
                                      TenCB = kq.Key.TenCB//string.Join("\n", kq.Select(p => p.YLenh.Contains("Chỉ định") ? (p.TenCB) : "").ToArray()),
                                  }).ToList();
                        foreach (var item in q4)
                        {
                            if (item.YLenh.Contains("Chỉ định thủ thuật - phẫu thuật:") && !item.YLenh.Contains("Chỉ định xét nghiệm:") && !item.YLenh.Contains("Chỉ định chẩn đoán hình ảnh:"))
                            {
                                item.YLenh = "Chỉ định thủ thuật - phẫu thuật:" + item.YLenh.Replace("\n\nChỉ định thủ thuật - phẫu thuật:", "");
                            }
                            if (item.YLenh.Contains("Chỉ định xét nghiệm:") && !item.YLenh.Contains("Chỉ định thủ thuật - phẫu thuật:") && !item.YLenh.Contains("Chỉ định chẩn đoán hình ảnh:"))
                            {
                                item.YLenh = "Chỉ định xét nghiệm:" + item.YLenh.Replace("\n\nChỉ định xét nghiệm:", "");
                            }
                            if (item.YLenh.Contains("Chỉ định chẩn đoán hình ảnh:") && !item.YLenh.Contains("Chỉ định thủ thuật - phẫu thuật:") && !item.YLenh.Contains("Chỉ định xét nghiệm:"))
                            {
                                item.YLenh = "Chỉ định chẩn đoán hình ảnh:" + item.YLenh.Replace("\n\nChỉ định chẩn đoán hình ảnh:", "");
                            }
                        }
                        var ttbn = (from bn in DaTaContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                                    join bnkb in DaTaContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                    join kp in DaTaContext.KPhongs on bnkb.MaKP equals kp.MaKP
                                    select new { bn.TenBNhan, bn.Tuoi, bn.GTinh, kp.TenKP, bnkb.Buong, bnkb.Giuong, bnkb.ChanDoan, bnkb.BenhKhac, bnkb.IDKB }).OrderByDescending(p => p.IDKB).ToList();
                        if (ttbn.Count > 0)
                        {
                            rep.TenBNhan.Value = ttbn.First().TenBNhan;
                            rep.Tuoi.Value = ttbn.First().Tuoi;
                            int gt = -1;
                            gt = ttbn.First().GTinh.Value;
                            if (gt == 0)
                                rep.GioiTinh.Value = "Nữ";
                            else
                                rep.GioiTinh.Value = "Nam";
                            rep.KhoaDT.Value = ttbn.First().TenKP;
                            rep.ChanDoan.Value = DungChung.Ham.FreshString(ttbn.First().ChanDoan + ";" + ttbn.First().BenhKhac);
                            rep.Buong.Value = ttbn.First().Buong;
                            rep.Giuong.Value = ttbn.First().Giuong;
                        }
                        var ktvv = DaTaContext.VaoViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                        if (ktvv.Count > 0)
                            rep.SoVV.Value = "Số vào viện: " + ktvv.First().SoVV;
                        else
                            rep.SoVV.Value = "Số vào viện: ..........";
                        // if (dienbien.Count > 0)
                        rep.DataSource = q4.OrderBy(p => p.NgayNhap).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                        return true;
                    }
                    else
                    {
                        frmIn frm = new frmIn();
                        Phieu.rep_ToDieuTri_YHCT rep = new Phieu.rep_ToDieuTri_YHCT();
                        var dienbien = (from db in DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= Denngay)
                                        join cb in DaTaContext.CanBoes on db.MaCB equals cb.MaCB
                                        select new { db.ID, IDDon = db.IDDon ?? 0, db.ThucHienYL, MaBNhan = db.MaBNhan ?? 0, db.DienBien1, db.MaCB, db.YLenh, db.HuongDTri, NgayNhap = db.NgayNhap.Value, TenCB = cb.CapBac + ": " + cb.TenCB }
                                           ).OrderBy(p => p.NgayNhap).ToList();
                        var ttbn = (from bn in DaTaContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                                    join bnkb in DaTaContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                    join kp in DaTaContext.KPhongs on bnkb.MaKP equals kp.MaKP
                                    select new { bn.TenBNhan, bn.Tuoi, bn.GTinh, kp.TenKP, bnkb.Buong, bnkb.Giuong, bnkb.ChanDoan, bnkb.BenhKhac, bnkb.IDKB }).OrderByDescending(p => p.IDKB).ToList();
                        if (ttbn.Count > 0)
                        {
                            rep.TenBNhan.Value = ttbn.First().TenBNhan;
                            rep.Tuoi.Value = ttbn.First().Tuoi;
                            int gt = -1;
                            gt = ttbn.First().GTinh.Value;
                            if (gt == 0)
                                rep.GioiTinh.Value = "Nữ";
                            else
                                rep.GioiTinh.Value = "Nam";
                            rep.KhoaDT.Value = ttbn.First().TenKP;
                            rep.ChanDoan.Value = DungChung.Ham.FreshString(ttbn.First().ChanDoan + ";" + ttbn.First().BenhKhac);
                            rep.Buong.Value = ttbn.First().Buong;
                            rep.Giuong.Value = ttbn.First().Giuong;
                        }
                        var ktvv = DaTaContext.VaoViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                        if (ktvv.Count > 0)
                            rep.SoVV.Value = "Số vào viện: " + ktvv.First().SoVV;
                        else
                            rep.SoVV.Value = "Số vào viện: ..........";
                        // if (dienbien.Count > 0)
                        rep.DataSource = dienbien.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                        return true;
                    }

                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn BN");
                    return false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("lỗi: " + ex.Message);
                return false;
            }
        }
        #endregion

        #region PhieuDieuTri
        public class DienBien30007
        {
            DateTime ngayNhap;

            public DateTime NgayNhap
            {
                get { return ngayNhap; }
                set { ngayNhap = value; }
            }
            string dienBien1;

            public string DienBien1
            {
                get { return dienBien1; }
                set { dienBien1 = value; }
            }
            string yLenh;

            public string YLenh
            {
                get { return yLenh; }
                set { yLenh = value; }
            }
            string tenCB;

            public string TenCB
            {
                get { return tenCB; }
                set { tenCB = value; }
            }
            int? maBNhan;

            public int? MaBNhan
            {
                get { return maBNhan; }
                set { maBNhan = value; }
            }
            string maCB;

            public string MaCB
            {
                get { return maCB; }
                set { maCB = value; }
            }
            int? iDDon;

            public int? IDDon
            {
                get { return iDDon; }
                set { iDDon = value; }
            }
            int iD;

            public int ID
            {
                get { return iD; }
                set { iD = value; }
            }
        }
        List<DienBien30007> db1 = new List<DienBien30007>();
        List<CanBo> _lCanBo = new List<CanBo>();
        public bool _PhieuDieuTri(int _int_maBN)
        {

            try
            {
                if (_int_maBN > 0)
                {
                    DateTime tungay = DungChung.Bien.MaBV == "30007" ? detungaydb.DateTime : DungChung.Ham.NgayTu(detungaydb.DateTime);
                    DateTime Denngay = DungChung.Bien.MaBV == "30007" ? dedenngaydb.DateTime : DungChung.Ham.NgayDen(dedenngaydb.DateTime);
                    db1.Clear();
                    int noitru = 0;
                    var dienbien = (from db in DaTaContext.DienBiens.Where(p => p.Ploai == 0).Where(p => p.MaBNhan == _int_maBN).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= Denngay)
                                    join cb in DaTaContext.CanBoes on db.MaCB equals cb.MaCB into k1
                                    from k2 in k1.DefaultIfEmpty()
                                    select new DienBien30007 { ID = db.ID, IDDon = db.IDDon, MaBNhan = db.MaBNhan, DienBien1 = db.DienBien1, MaCB = db.MaCB, YLenh = db.YLenh, NgayNhap = db.NgayNhap.Value, TenCB = k2 != null ? DungChung.Bien.MaBV == "24012" ? k2.TenCB : k2.CapBac + ": " + k2.TenCB  : "" }
                                       ).OrderBy(p => p.NgayNhap).ToList();
                    db1.AddRange(dienbien);
                    var ttbn = (from bn in DaTaContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                                join bnkb in DaTaContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                join kp in DaTaContext.KPhongs on bnkb.MaKP equals kp.MaKP
                                select new { bn.TenBNhan, bn.Tuoi, bn.GTinh, kp.TenKP, bnkb.Buong, bnkb.Giuong, bnkb.ChanDoan, bnkb.BenhKhac, bnkb.IDKB, bn.NoiTru }).OrderByDescending(p => p.IDKB).ToList();
                    if (ttbn.Count > 0)
                        noitru = Convert.ToInt32(ttbn.First().NoiTru);
                    frmIn frm = new frmIn();
                    Phieu.rep_ToDieuTri rep = new Phieu.rep_ToDieuTri(noitru);
                    if (ttbn.Count > 0)
                    {
                        if (DungChung.Bien.MaBV == "24012")
                        {
                            for (int i = 0; i < db1.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(db1[i].DienBien1))
                                {
                                    db1[i].DienBien1 += "\n\n\n\t" + db1[i].TenCB;
                                }

                                if (!string.IsNullOrEmpty(db1[i].YLenh))
                                {
                                    db1[i].YLenh += "\n\n\n\t" + db1[i].TenCB;
                                }
                                else
                                {
                                    db1[i].TenCB = "";
                                }
                            }
                        }
                        rep.TenBNhan.Value = ttbn.First().TenBNhan;
                        rep.Tuoi.Value = ttbn.First().Tuoi;
                        int gt = -1;
                        gt = ttbn.First().GTinh.Value;
                        if (gt == 0)
                            rep.GioiTinh.Value = "Nữ";
                        else
                            rep.GioiTinh.Value = "Nam";
                        rep.KhoaDT.Value = ttbn.First().TenKP;
                        rep.ChanDoan.Value = ttbn.First().ChanDoan + ";" + DungChung.Ham.FreshString(ttbn.First().BenhKhac);
                        rep.Buong.Value = ttbn.First().Buong;
                        rep.Giuong.Value = ttbn.First().Giuong;
                    }
                    var ktvv = DaTaContext.VaoViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                    if (ktvv.Count > 0)
                        rep.SoVV.Value = "Số vào viện: " + ktvv.First().SoVV;
                    else
                        rep.SoVV.Value = "Số vào viện: ..........";
                    // if (dienbien.Count > 0)
                    rep.DataSource = db1.OrderBy(p => p.NgayNhap).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    return true;
                    //}
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn BN");
                    return false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("lỗi: " + ex.Message);
                return false;
            }
        }
        #endregion

        private void cboInToDieuTri_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rs;
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (cboInToDieuTri.SelectedIndex == 0)
            {
                _PhieuDieuTri(_int_maBN);
            }
            if (cboInToDieuTri.SelectedIndex == 1)
            {
                _PhieuDieuTri_YHCT(_int_maBN);
            }
            cboInToDieuTri.SelectedIndex = -1;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            simpleButton2.Enabled = false;
            simpleButton5.Enabled = false;
            simpleButton3.Enabled = true;
            int TTLuu = 0;
            _ldienbien = DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN).Where(p => p.Ploai == 0).ToList();
            if (_ldienbien.Count > 0)
                TTLuu = 2;
            else
                TTLuu = 1;

            switch (TTLuu)
            {
                case 1:
                    for (int i = 0; i < grvDienBien.RowCount; i++)
                    {
                        if (grvDienBien.GetRowCellValue(i, colNgayNhapdb) != null && grvDienBien.GetRowCellValue(i, colNgayNhapdb).ToString() != "")
                        {
                            DienBien dbmoi = new DienBien();
                            dbmoi.MaBNhan = _int_maBN;
                            dbmoi.NgayNhap = Convert.ToDateTime(grvDienBien.GetRowCellValue(i, colNgayNhapdb));
                            if (grvDienBien.GetRowCellValue(i, colDienBien) != null && grvDienBien.GetRowCellValue(i, colDienBien).ToString() != "")
                                dbmoi.DienBien1 = grvDienBien.GetRowCellValue(i, colDienBien).ToString();
                            if (grvDienBien.GetRowCellValue(i, colYlenh) != null && grvDienBien.GetRowCellValue(i, colYlenh).ToString() != "")
                                dbmoi.YLenh = grvDienBien.GetRowCellValue(i, colYlenh).ToString();
                            if (grvDienBien.GetRowCellValue(i, colMaCB_db) != null && grvDienBien.GetRowCellValue(i, colMaCB_db).ToString() != "")
                                dbmoi.MaCB = grvDienBien.GetRowCellValue(i, colMaCB_db).ToString();
                            if (grvDienBien.GetRowCellValue(i, colHuongDtri) != null && grvDienBien.GetRowCellValue(i, colHuongDtri).ToString() != "")
                                dbmoi.HuongDTri = grvDienBien.GetRowCellValue(i, colHuongDtri).ToString();
                            if (grvDienBien.GetRowCellValue(i, colTHYL) != null && grvDienBien.GetRowCellValue(i, colTHYL).ToString() != "")
                                dbmoi.ThucHienYL = grvDienBien.GetRowCellValue(i, colTHYL).ToString();

                            dbmoi.Ploai = 0;
                            DaTaContext.DienBiens.Add(dbmoi);
                            DaTaContext.SaveChanges();
                        }
                    }
                    TTLuu = 0;
                    break;
                case 2:
                    for (int i = 0; i < grvDienBien.RowCount; i++)
                    {
                        int id = 0;
                        if (grvDienBien.GetRowCellValue(i, colIDdb) != null && grvDienBien.GetRowCellValue(i, colIDdb).ToString() != "")
                            id = Convert.ToInt32(grvDienBien.GetRowCellValue(i, colIDdb).ToString());
                        if (id > 0)
                        {
                            if (grvDienBien.GetRowCellValue(i, colNgayNhapdb) != null && grvDienBien.GetRowCellValue(i, colNgayNhapdb).ToString() != "")
                            {
                                var dbmoi = DaTaContext.DienBiens.Single(p => p.ID == id);
                                dbmoi.MaBNhan = _int_maBN;
                                dbmoi.NgayNhap = Convert.ToDateTime(grvDienBien.GetRowCellValue(i, colNgayNhapdb));
                                if (grvDienBien.GetRowCellValue(i, colDienBien) != null && grvDienBien.GetRowCellValue(i, colDienBien).ToString() != "")
                                    dbmoi.DienBien1 = grvDienBien.GetRowCellValue(i, colDienBien).ToString();
                                if (grvDienBien.GetRowCellValue(i, colYlenh) != null && grvDienBien.GetRowCellValue(i, colYlenh).ToString() != "")
                                    dbmoi.YLenh = grvDienBien.GetRowCellValue(i, colYlenh).ToString();
                                if (grvDienBien.GetRowCellValue(i, colMaCB_db) != null && grvDienBien.GetRowCellValue(i, colMaCB_db).ToString() != "")
                                    dbmoi.MaCB = grvDienBien.GetRowCellValue(i, colMaCB_db).ToString();
                                if (grvDienBien.GetRowCellValue(i, colHuongDtri) != null && grvDienBien.GetRowCellValue(i, colHuongDtri).ToString() != "")
                                    dbmoi.HuongDTri = grvDienBien.GetRowCellValue(i, colHuongDtri).ToString();
                                if (grvDienBien.GetRowCellValue(i, colTHYL) != null && grvDienBien.GetRowCellValue(i, colTHYL).ToString() != "")
                                    dbmoi.ThucHienYL = grvDienBien.GetRowCellValue(i, colTHYL).ToString();
                                DaTaContext.SaveChanges();

                            }
                        }
                        else
                        {
                            if (grvDienBien.GetRowCellValue(i, colNgayNhapdb) != null && grvDienBien.GetRowCellValue(i, colNgayNhapdb).ToString() != "")
                            {
                                DienBien dbmoi = new DienBien();
                                dbmoi.MaBNhan = _int_maBN;
                                dbmoi.NgayNhap = Convert.ToDateTime(grvDienBien.GetRowCellValue(i, colNgayNhapdb));
                                if (grvDienBien.GetRowCellValue(i, colDienBien) != null && grvDienBien.GetRowCellValue(i, colDienBien).ToString() != "")
                                    dbmoi.DienBien1 = grvDienBien.GetRowCellValue(i, colDienBien).ToString();
                                if (grvDienBien.GetRowCellValue(i, colYlenh) != null && grvDienBien.GetRowCellValue(i, colYlenh).ToString() != "")
                                    dbmoi.YLenh = grvDienBien.GetRowCellValue(i, colYlenh).ToString();
                                if (grvDienBien.GetRowCellValue(i, colMaCB_db) != null && grvDienBien.GetRowCellValue(i, colMaCB_db).ToString() != "")
                                    dbmoi.MaCB = grvDienBien.GetRowCellValue(i, colMaCB_db).ToString();
                                if (grvDienBien.GetRowCellValue(i, colHuongDtri) != null && grvDienBien.GetRowCellValue(i, colHuongDtri).ToString() != "")
                                    dbmoi.HuongDTri = grvDienBien.GetRowCellValue(i, colHuongDtri).ToString();
                                if (grvDienBien.GetRowCellValue(i, colTHYL) != null && grvDienBien.GetRowCellValue(i, colTHYL).ToString() != "")
                                    dbmoi.ThucHienYL = grvDienBien.GetRowCellValue(i, colTHYL).ToString();
                                dbmoi.Ploai = 0;
                                DaTaContext.DienBiens.Add(dbmoi);
                                DaTaContext.SaveChanges();
                            }
                        }
                        TTLuu = 0;
                    }
                    break;

            }
            grvDienBien.OptionsBehavior.Editable = false;
            _ldienbien = DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN).Where(p => p.Ploai == 0).ToList();
            binsDienBien.DataSource = _ldienbien;
            grcDienBien.DataSource = binsDienBien;

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            simpleButton2.Enabled = false;
            simpleButton5.Enabled = false;
            simpleButton3.Enabled = true;
            grvDienBien.OptionsBehavior.Editable = false;
            frmDienBienBenh_Load(sender, e);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var ravien = DaTaContext.RaViens.Where(p => p.MaBNhan == _int_maBN).ToList();
            if (ravien.Count() == 0)
            {
                simpleButton2.Enabled = true;
                simpleButton5.Enabled = true;
                simpleButton3.Enabled = false;
                grvDienBien.OptionsBehavior.Editable = true;
            }
            else
            {
                MessageBox.Show("Bệnh nhân đã ra viện không thể sửa!");
            }
        }

        private void detungaydb_EditValueChanged(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(detungaydb.DateTime);
            DateTime denngay = DungChung.Ham.NgayTu(dedenngaydb.DateTime);
            _ldienbien = DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN && p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.Ploai == 0).OrderByDescending(p => p.ID).ToList();
            binsDienBien.DataSource = _ldienbien;
            grcDienBien.DataSource = binsDienBien;
        }

        private void dedenngaydb_EditValueChanged(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(detungaydb.DateTime);
            DateTime denngay = DungChung.Ham.NgayTu(dedenngaydb.DateTime);
            _ldienbien = DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN && p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.Ploai == 0).OrderByDescending(p => p.ID).ToList();
            binsDienBien.DataSource = _ldienbien;
            grcDienBien.DataSource = binsDienBien;
        }

        private void grvDienBien_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.Name == "colXoadb")
                {
                    int rs;
                    var ktrarv = DaTaContext.RaViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                    if (ktrarv.Count <= 0)
                    {
                        int id = 0;
                        if (grvDienBien.GetFocusedRowCellValue(colIDdb) != null && grvDienBien.GetFocusedRowCellValue(colIDdb).ToString() != "")
                            id = Convert.ToInt32(grvDienBien.GetFocusedRowCellValue(colIDdb));
                        string macb = "";
                        if (grvDienBien.GetFocusedRowCellValue(colMaCB_db) != null && grvDienBien.GetFocusedRowCellValue(colMaCB_db).ToString() != "")
                            macb = grvDienBien.GetFocusedRowCellValue(colMaCB_db).ToString();
                        var xoa = DaTaContext.DienBiens.Where(p => p.ID == id).FirstOrDefault();
                        if (xoa != null)
                        {
                            if (DungChung.Bien.MaBV == "30007")
                            {
                                if (DungChung.Bien.MaCB == macb || DungChung.Bien.PLoaiKP == "Admin")
                                {
                                    DialogResult Result = MessageBox.Show("Bạn muốn xóa diễn biến ngày: " + xoa.NgayNhap.Value.ToShortDateString(), "Hỏi xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                    if (Result == DialogResult.OK)
                                    {
                                        DaTaContext.DienBiens.Remove(xoa);
                                        DaTaContext.SaveChanges();
                                        grvDienBien.DeleteSelectedRows();
                                        MessageBox.Show("Xóa thành công");
                                    }
                                }
                                else
                                {
                                    var ad = DaTaContext.ADMINs.Where(p => p.MaCB == macb).ToList().FirstOrDefault();
                                    if (ad != null)
                                        MessageBox.Show("Không phải bác sĩ kê không được xóa! \n Tài khoản: " + ad.TenDN + " đã tạo diễn biến này.");
                                    else
                                        MessageBox.Show("Không phải bác sĩ kê không được xóa!\n Tài khoản tạo không còn tồn tại liên hệ Admin để được hỗ trợ!");
                                }

                            }
                            else
                            {
                                DialogResult Result = MessageBox.Show("Bạn muốn xóa diễn biến ngày: " + xoa.NgayNhap.Value.ToShortDateString(), "Hỏi xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                if (Result == DialogResult.OK)
                                {
                                    DaTaContext.DienBiens.Remove(xoa);
                                    DaTaContext.SaveChanges();
                                    grvDienBien.DeleteSelectedRows();
                                    MessageBox.Show("Xóa thành công");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bệnh nhân đã ra viện, không thể xóa !");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa diễn biến:" + ex.Message);
            }
        }
    }
}