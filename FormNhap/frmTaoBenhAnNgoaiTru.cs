using System;
using QLBV_Database;
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
    public partial class frmTaoBenhAnNgoaiTru : DevExpress.XtraEditors.XtraForm
    {
        int _int_maBN = 0;
        BNKB bnkb;
        public frmTaoBenhAnNgoaiTru(int mabn, BNKB _bnkb)
        {
            InitializeComponent();
            _int_maBN = mabn;
            this.bnkb = _bnkb;
        }
        QLBV_Database.QLBVEntities DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DienBien> _ldienbien = new List<DienBien>();
        bool isSave = false;

        private void frmTaoBenhAnNgoaiTru_Load(object sender, EventArgs e)
        {
            dtFromTime.DateTime = DateTime.Now;
            dtToTime.DateTime = DateTime.Now;
            // if (DungChung.Bien.MaBV == "30007") btn30007.Visible = true;

            if (DungChung.Bien.MaBV == "34019")
            {
                colHuongDtri.Visible = false;
            }
            DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var bn = DaTaContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList();
            idPerson = bn.First().IDPerson;
            _ldienbien = DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN).Where(p => p.Ploai == 99).OrderByDescending(p => p.ID).ToList();
            if (_ldienbien.Count > 0)
            {
                txtSoBA.Text = bn.First().SoHSBA;
            }
            //else
            //{
            //    setSoBA(bnkb.MaKP ?? 0, 0);
            //}

            binsDienBien.DataSource = _ldienbien;
            grcDienBien.DataSource = binsDienBien;
            _lCanBo = DaTaContext.CanBoes.OrderBy(p => p.TenCB).ToList();
            lupMaCB_dienBien.DataSource = _lCanBo;
            simpleButton2.Enabled = false;
            simpleButton5.Enabled = false;
            simpleButton3.Enabled = true;
            LoadControlByDanhSach(isDanhSach);
            //btnXoa.Enabled = (_ldienbien != null && _ldienbien.Count > 0) ? true : false;
            if (DungChung.Bien.MaBV == "30372")
            {
                simpleButton6.Visible = true;
                simpleButton7.Visible = true;
            }
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
                        var _ldichvu = DaTaContext.DichVus.Where(p => p.PLoai == 99).ToList();
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
                }

            }
            frmTaoBenhAnNgoaiTru_Load(sender, e);
        }

        private void setSoBA(int makp, int noiNgoaiTru)
        {
            if (DungChung.Bien.PP_SoBA != 1 && DungChung.Bien.PP_SoBA != 2)
                return;
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int soBA = 0;
            if (DungChung.Bien.PP_SoBA == 1)
            {
                if (makp <= 0)
                {
                    soBA = DungChung.Ham.GetSoPL(4, makp, noiNgoaiTru);
                }
                else
                {
                    MessageBox.Show("Chưa chọn khoa điều trị, không lấy được số bệnh án");
                    return;
                }
            }
            else if (DungChung.Bien.PP_SoBA == 2)
            {
                soBA = DungChung.Ham.GetSoPL(4, 0, noiNgoaiTru);
            }

            var qvv = DataContect.SoPLs.Where(p => p.PhanLoai == 4 && p.SoPL1 == (soBA - 1) && p.NgayNhap != null && p.NgayNhap.Value.Year == DateTime.Now.Year && p.NoiTru == 0).ToList();
            if (qvv.Count > 0)
            {
                foreach (var a in qvv)
                {
                    DataContect.SoPLs.Remove(a);
                }
                DataContect.SaveChanges();
            }
            SoPL soPLMoi = new SoPL();
            soPLMoi.SoPL1 = soBA;
            soPLMoi.Status = 1;
            soPLMoi.PhanLoai = 4;
            soPLMoi.NgayNhap = DateTime.Now;
            soPLMoi.NoiTru = 0;
            DataContect.SoPLs.Add(soPLMoi);
            DataContect.SaveChanges();

            txtSoBA.Text = soBA.ToString("D5");
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
            frmTaoBenhAnNgoaiTru_Load(sender, e);
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
            frmTaoBenhAnNgoaiTru_Load(sender, e);
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
                    var tungay = DungChung.Ham.NgayTu(dtFromTime.DateTime);
                    var denngay = DungChung.Ham.NgayDen(dtToTime.DateTime);
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
                                         join db in DaTaContext.DienBiens.Where(p => p.IDDon != null && p.IDDon >= 0).Where(p => isDanhSach ? (p.NgayNhap >= tungay && p.NgayNhap <= denngay) : (p.MaBNhan == _int_maBN)).Where(p => p.Ploai == 99) on a.IDDon equals db.IDDon
                                         join bn in DaTaContext.BenhNhans.Where(o => o.IDPerson == idPerson) on db.MaBNhan equals bn.MaBNhan
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

                        var q1 = (from db in DaTaContext.DienBiens.Where(p => isDanhSach ? (p.NgayNhap >= tungay && p.NgayNhap <= denngay) : (p.MaBNhan == _int_maBN)).Where(p => p.IDDon == null || p.IDDon <= 0).Where(p => p.Ploai == 99)
                                  join cb in DaTaContext.CanBoes on db.MaCB equals cb.MaCB
                                  join bn in DaTaContext.BenhNhans.Where(o => o.IDPerson == idPerson) on db.MaBNhan equals bn.MaBNhan
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
                        var dienbien = (from db in DaTaContext.DienBiens.Where(p => isDanhSach ? (p.NgayNhap >= tungay && p.NgayNhap <= denngay) : (p.MaBNhan == _int_maBN)).Where(p => p.Ploai == 99)
                                        join bn in DaTaContext.BenhNhans.Where(o => o.IDPerson == idPerson) on db.MaBNhan equals bn.MaBNhan
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
            DateTime? ngayNhap;

            public DateTime? NgayNhap
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
                var tungay = DungChung.Ham.NgayTu(dtFromTime.DateTime);
                var denngay = DungChung.Ham.NgayDen(dtToTime.DateTime);
                if (_int_maBN > 0)
                {
                    db1.Clear();
                    int noitru = 0;
                    var dienbien = (from db in DaTaContext.DienBiens.Where(p => p.Ploai == 99).Where(p => isDanhSach ? (p.NgayNhap >= tungay && p.NgayNhap <= denngay) : (p.MaBNhan == _int_maBN))
                                    join bn in DaTaContext.BenhNhans.Where(o => o.IDPerson == idPerson) on db.MaBNhan equals bn.MaBNhan
                                    join cb in DaTaContext.CanBoes on db.MaCB equals cb.MaCB into k1
                                    from k2 in k1.DefaultIfEmpty()
                                    select new DienBien30007
                                    {
                                        ID = db.ID,
                                        IDDon = db.IDDon,
                                        MaBNhan = db.MaBNhan,
                                        DienBien1 = db.DienBien1,
                                        MaCB = db.MaCB,
                                        YLenh = DungChung.Bien.MaBV == "27001" ? db.YLenh : db.YLenh + Environment.NewLine + db.HuongDTri,
                                        NgayNhap = db.NgayNhap,
                                        TenCB = k2 != null ? k2.CapBac + ": " + k2.TenCB : ""
                                    }).OrderBy(p => p.NgayNhap).ToList();
                    db1.AddRange(dienbien);
                    var ttbn = (from bn in DaTaContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                                join bnkb in DaTaContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                join kp in DaTaContext.KPhongs on bnkb.MaKP equals kp.MaKP
                                select new { bn.TenBNhan, bn.Tuoi, bn.GTinh, kp.TenKP, bnkb.Buong, bnkb.Giuong, bnkb.ChanDoan, bnkb.BenhKhac, bnkb.IDKB, bn.NoiTru }).OrderByDescending(p => p.IDKB).ToList();

                    //var ketluan = (from cls in DaTaContext.CLS.Where(p => p.MaBNhan == _int_maBN)
                    //            join cd in DaTaContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                    //            select new {cd.KetLuan }).ToList();



                    if (ttbn.Count > 0)
                        noitru = Convert.ToInt32(ttbn.First().NoiTru);
                    frmIn frm = new frmIn();
                    Phieu.rep_ToDieuTri rep = new Phieu.rep_ToDieuTri(noitru);
                    //if (DungChung.Bien.MaBV == "27001" && ketluan.Count > 0)
                    //{
                    //    string ketluan1 = "CĐHA: \r";
                    //    for (int i = 0; i < ketluan.Count; i++)
                    //    {
                    //        ketluan1 = ketluan1 + "- " + ketluan[i].KetLuan + "\r";
                    //    }
                    //    //rep.KetLuan.Value = ketluan1;
                    //}
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

        private bool CheckSave(List<DienBien> data)
        {
            bool result = true;
            if (data.Exists(o => o.NgayNhap == null))
            {
                MessageBox.Show("Ngày nhập không được để trống!", "Thông báo");
                return false;
            }
            if (data.Exists(o => string.IsNullOrWhiteSpace(o.DienBien1)))
            {
                MessageBox.Show("Diễn biến bệnh không được để trống!", "Thông báo");
                return false;
            }
            return result;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var dataSource = (List<DienBien>)binsDienBien.DataSource;
            if (!CheckSave(dataSource))
                return;
            int checkluu = 0;
            simpleButton2.Enabled = false;
            simpleButton5.Enabled = false;
            simpleButton3.Enabled = true;
            int TTLuu = 0;
            _ldienbien = DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN).Where(p => p.Ploai == 99).ToList();
            if (_ldienbien.Count > 0)
                TTLuu = 2;
            else
                TTLuu = 1;

            switch (TTLuu)
            {
                case 1:
                    if (bnkb == null)
                    {
                        MessageBox.Show("Bệnh nhân chưa có chẩn đoán tại khoa phòng");
                        return;
                    }
                    setSoBA(bnkb.MaKP ?? 0, 0);
                    for (int i = 0; i < grvDienBien.RowCount; i++)
                    {
                        DienBien dbmoi = new DienBien();
                        dbmoi.MaBNhan = _int_maBN;
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
                        if (grvDienBien.GetRowCellValue(i, gridColumn1) != null && grvDienBien.GetRowCellValue(i, gridColumn1).ToString() != "")
                            dbmoi.NgayNhap = Convert.ToDateTime(grvDienBien.GetRowCellValue(i, gridColumn1));

                        dbmoi.Ploai = 99;
                        DaTaContext.DienBiens.Add(dbmoi);
                        if (DaTaContext.SaveChanges() > 0)
                            checkluu++;
                    }
                    var benhNhanCreate = DaTaContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == _int_maBN);
                    if (benhNhanCreate != null && checkluu > 0)
                    {
                        benhNhanCreate.DTNT = true;
                        benhNhanCreate.NoiTru = 0;
                        benhNhanCreate.SoHSBA = txtSoBA.Text;
                    }
                    DaTaContext.SaveChanges();
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
                            var dbmoi = DaTaContext.DienBiens.Single(p => p.ID == id);
                            dbmoi.MaBNhan = _int_maBN;
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
                            if (grvDienBien.GetRowCellValue(i, gridColumn1) != null && grvDienBien.GetRowCellValue(i, gridColumn1).ToString() != "")
                                dbmoi.NgayNhap = Convert.ToDateTime(grvDienBien.GetRowCellValue(i, gridColumn1));
                            DaTaContext.SaveChanges();

                        }
                        else
                        {
                            DienBien dbmoi = new DienBien();
                            dbmoi.MaBNhan = _int_maBN;
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
                            if (grvDienBien.GetRowCellValue(i, gridColumn1) != null && grvDienBien.GetRowCellValue(i, gridColumn1).ToString() != "")
                                dbmoi.NgayNhap = Convert.ToDateTime(grvDienBien.GetRowCellValue(i, gridColumn1));
                            dbmoi.Ploai = 99;
                            DaTaContext.DienBiens.Add(dbmoi);
                            DaTaContext.SaveChanges();
                        }
                        TTLuu = 0;
                    }
                    var benhNhanUpdate = DaTaContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == _int_maBN);
                    if (benhNhanUpdate != null)
                    {
                        benhNhanUpdate.DTNT = true;
                        benhNhanUpdate.NoiTru = 0;
                        benhNhanUpdate.SoHSBA = txtSoBA.Text;
                    }
                    DaTaContext.SaveChanges();
                    break;

            }
            grvDienBien.OptionsBehavior.Editable = false;
            _ldienbien = DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN).Where(p => p.Ploai == 99).ToList();
            binsDienBien.DataSource = _ldienbien;
            grcDienBien.DataSource = binsDienBien;
            isSave = false;

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            simpleButton2.Enabled = false;
            simpleButton5.Enabled = false;
            simpleButton3.Enabled = true;
            grvDienBien.OptionsBehavior.Editable = false;
            txtSoBA.Text = "";
            frmTaoBenhAnNgoaiTru_Load(sender, e);
            isSave = false;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV != "30009" && DungChung.Bien.MaBV != "30303")
            {
                var ktraphuongan = DaTaContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PhuongAn == 0 || p.PhuongAn == 2).ToList();
                if (ktraphuongan.Count() > 0)
                {
                    MessageBox.Show("Bệnh nhân đã duyệt thanh toán, không thế sửa !");
                    return;
                }
                isSave = true;
                simpleButton2.Enabled = true;
                simpleButton5.Enabled = true;
                simpleButton3.Enabled = false;
                grvDienBien.OptionsBehavior.Editable = true;
            }
            else
            {
                var ktraphuongan = DaTaContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PhuongAn == 0 || p.PhuongAn == 2).ToList();
                isSave = true;
                simpleButton2.Enabled = true;
                simpleButton5.Enabled = true;
                simpleButton3.Enabled = false;
                grvDienBien.OptionsBehavior.Editable = true;
            }
        }

        public class MyObject
        {
            public int IDCD { get; set; }
            public int IDNhom { set; get; }
            public string TenTN { set; get; }
            public string TenRG { set; get; }

            public string TenNhom { get; set; }
            public string TenDVct { get; set; }
            public string KetQua { get; set; }
            public string MoTa { get; set; }
            public string KetLuan { get; set; }
            public DateTime? NgayTH { get; set; }
        }

        private void detungaydb_EditValueChanged(object sender, EventArgs e)
        {
            _ldienbien = DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN).Where(p => p.Ploai == 99).OrderByDescending(p => p.ID).ToList();
            binsDienBien.DataSource = _ldienbien;
            grcDienBien.DataSource = binsDienBien;
        }

        private void dedenngaydb_EditValueChanged(object sender, EventArgs e)
        {
            _ldienbien = DaTaContext.DienBiens.Where(p => p.MaBNhan == _int_maBN).Where(p => p.Ploai == 99).OrderByDescending(p => p.ID).ToList();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa diễn biến:" + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var xoa = DaTaContext.DienBiens.FirstOrDefault(o => o.MaBNhan == _int_maBN && o.Ploai == 99);
                if (xoa != null)
                {
                    DaTaContext.DienBiens.Remove(xoa);
                    DaTaContext.SaveChanges();
                    frmTaoBenhAnNgoaiTru_Load(null, null);
                }
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e) // Nút Tạo mới
        {
            var ktraphuongan = DaTaContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PhuongAn == 0 || p.PhuongAn == 2).ToList();
            if (ktraphuongan.Count() > 0)
            {
                MessageBox.Show("Bệnh nhân đã kết thúc khám, không thế tạo mới !!!");
                return;
            }
            simpleButton2.Enabled = true;
            simpleButton5.Enabled = true;
            simpleButton3.Enabled = false;
            grvDienBien.OptionsBehavior.Editable = true;
            string oldDienBien = "";

            if (_ldienbien != null && _ldienbien.Count > 0 && DungChung.Bien.MaBV != "24272")
            {
                var id = _ldienbien.FirstOrDefault().ID;
                var current = DaTaContext.DienBiens.FirstOrDefault(o => o.ID == id);
                if (current != null)
                {
                    oldDienBien = current.DienBien1;
                    DaTaContext.DienBiens.Remove(current);
                    DaTaContext.SaveChanges();
                    var makp = DungChung.Bien.PP_SoBA == 1 ? (bnkb.MaKP ?? 0) : 0;
                    DungChung.Ham.SoPLDeleteChuyenVien(makp, (!string.IsNullOrWhiteSpace(txtSoBA.Text) ? Convert.ToInt32(txtSoBA.Text) : 0), 4, 0);
                    txtSoBA.Text = "";
                }
                _ldienbien = new List<DienBien>();
            }

            if (_ldienbien != null || _ldienbien.Count <= 0)
            {
                DienBien db = new DienBien();

                #region CLS
                List<MyObject> _lall = new List<MyObject>();
                List<MyObject> qcd = (from cd in DaTaContext.ChiDinhs
                                      join cls in DaTaContext.CLS.Where(o => o.MaBNhan == _int_maBN) on cd.IdCLS equals cls.IdCLS
                                      join clsctNotNull in DaTaContext.CLScts on cd.IDCD equals clsctNotNull.IDCD into kq
                                      from clsct in kq.DefaultIfEmpty()
                                      join dv in DaTaContext.DichVus on cd.MaDV equals dv.MaDV
                                      join dvctNotNull in DaTaContext.DichVucts on clsct.MaDVct equals dvctNotNull.MaDVct into kq1
                                      from dvct in kq1.DefaultIfEmpty()
                                      join tn in DaTaContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                      join n in DaTaContext.NhomDVs on tn.IDNhom equals n.IDNhom
                                      select new MyObject
                                      {
                                          IDCD = cd.IDCD,
                                          IDNhom = tn.IDNhom ?? 0,
                                          TenNhom = n.TenNhom,
                                          TenTN = tn.TenRG,
                                          TenRG = dv.TenDV,
                                          TenDVct = dvct != null ? dvct.TenDVct : "",
                                          KetQua = clsct != null ? (DungChung.Bien.MaBV == "30009" ? (clsct.KetQua + " " + dvct.DonVi) : clsct.KetQua) : clsct.KetQua,
                                          KetLuan = cd.KetLuan,
                                          MoTa = cd.MoTa,
                                          NgayTH = cls.NgayTH,
                                      }).ToList();
                _lall.AddRange(qcd);

                _lall = _lall.OrderBy(p => p.NgayTH).ThenBy(p => p.TenTN).ThenBy(p => p.TenRG).ToList();

                var q1 = (from dv in _lall
                          group dv by new { dv.TenNhom, dv.IDNhom } into x
                          select new
                          {
                              x.Key.IDNhom,
                              TenNhom = x.Key.TenNhom,
                              TenRG = "- " + x.Key.TenNhom + ": " + string.Join(", ", x.Select(p => p.TenRG).ToList().Distinct()) + Environment.NewLine,
                              CDHA = x.Key.IDNhom == 2 ? /*batdau*/ string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).Select(p => ("- " + p.TenRG + (!string.IsNullOrWhiteSpace(p.KetQua) && DungChung.Bien.MaBV != "27001" ? (": " + DungChung.Bien.MaBV == "27001" ? (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "") : p.KetQua + (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "")) : (!string.IsNullOrWhiteSpace(p.KetLuan) ? (": " + p.KetLuan) : ""))))) : "",
                              XN = x.Key.IDNhom == 1 ? (string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG, }).Select(p => (("- " + (p.Count() == 1 ? (p.FirstOrDefault().TenDVct + " " + p.FirstOrDefault().KetQua) : (p.Key.TenRG + (p.Count() > 0 ? ": " : "") + string.Join(", ", p.Select(o => (o.TenDVct + (!string.IsNullOrWhiteSpace(o.KetQua) ? (" " + o.KetQua) : ""))))))))))) : "",
                              PTTT = x.Key.IDNhom == 8 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).Select(p => ("- " + p.TenRG + (!string.IsNullOrWhiteSpace(p.KetQua) ? (": " + p.KetQua + (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "")) : (!string.IsNullOrWhiteSpace(p.KetLuan) ? (": " + p.KetLuan) : ""))))) : "",
                              TDCN = x.Key.IDNhom == 3 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG }).Select(p => (("- " + (p.Count() == 1 ? (p.FirstOrDefault().TenDVct + " " + p.FirstOrDefault().KetQua) : (p.Key.TenRG + (p.Count() > 0 ? ": " : "") + string.Join(", ", p.Select(o => (o.TenDVct + (!string.IsNullOrWhiteSpace(o.KetQua) ? (" " + o.KetQua) : "")))))))))) : "",// his 120 11/05/2021
                              TDCN27001 = x.Key.IDNhom == 3 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG }).Select(p => (("\t- " + (p.Count() == 1 ? (p.FirstOrDefault().TenRG + ": " + p.FirstOrDefault().KetLuan) : (string.Join(", ", p.Select(o => (o.TenRG + ": " + (!string.IsNullOrWhiteSpace(o.KetLuan) ? (" " + o.KetLuan) : ""))).Distinct()))))))) : "",// his-327 27/07/2021
                          }).ToList();

                string dsCLS = q1.Count > 0 ? ("Chỉ định CLS:" + Environment.NewLine + string.Join("", q1.Select(p => p.TenRG))) : "";

                string klCDHA = (q1.FirstOrDefault(o => o.IDNhom == 2) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 2).CDHA)) ? ("CĐHA:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 2).CDHA) : "";
                string klXN = (q1.FirstOrDefault(o => o.IDNhom == 1) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 1).XN)) ? ("XN:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 1).XN) : "";
                string klPTTT = (q1.FirstOrDefault(o => o.IDNhom == 8) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 8).PTTT)) ? ("THỦ THUẬT PHẪU THUẬT:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 8).PTTT) : "";
                string klTDCN = (q1.FirstOrDefault(o => o.IDNhom == 3) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 3).TDCN)) ? (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "30007" ? (("THĂM DÒ CHỨC NĂNG:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 3).TDCN27001)) : ("THĂM DÒ CHỨC NĂNG:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 3).TDCN)) : ""; // his-327 27/07/2021
                #endregion

                #region Đơn thuốc
                var ktstt = (from dt in DaTaContext.DThuocs.Where(p => p.MaBNhan == _int_maBN && p.PLDV == 1)
                             join dtct in DaTaContext.DThuoccts on dt.IDDon equals dtct.IDDon
                             join dv in DaTaContext.DichVus on dtct.MaDV equals dv.MaDV
                             join tn in DaTaContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             select new {dtct.IDDonct, dv.MaDV, tn.TenRG, dv.DongY, TenTT05 = dv.TenDV, dv.HamLuong, dtct.DuongD, dtct.DonVi, dtct.GhiChu, dtct.MaKXuat, dtct.SoLuongct, dtct.SoLan, dtct.MoiLan, dtct.Luong, dtct.DviUong }).OrderBy(p => p.IDDonct).ToList();
                var dtGroup = (from d in ktstt
                               group d by new { d.MaDV, d.TenRG, d.DongY, d.TenTT05, d.HamLuong, d.DuongD, d.DonVi, d.GhiChu, d.MaKXuat, d.SoLuongct, d.SoLan, d.MoiLan, d.Luong, d.DviUong } into kq
                               select new
                               {
                                   kq.Key.MaDV,
                                   kq.Key.TenRG,
                                   kq.Key.DongY,
                                   kq.Key.TenTT05,
                                   kq.Key.HamLuong,
                                   kq.Key.DuongD,
                                   kq.Key.DonVi,
                                   kq.Key.GhiChu,
                                   kq.Key.MaKXuat,
                                   kq.Key.SoLuongct,
                                   kq.Key.SoLan,
                                   kq.Key.MoiLan,
                                   kq.Key.Luong,
                                   kq.Key.DviUong,
                                   SL = kq.Where(o => o.TenRG.Contains("Thuốc gây nghiện") || o.TenRG.Contains("Thuốc hướng tâm thần")).Count()
                               }
                          ).ToList();

                string dsDT = (dtGroup == null || dtGroup.Count == 0) ? "" : "THUỐC:" + Environment.NewLine;
                foreach (var item in dtGroup)
                {
                    var TenKP = DaTaContext.KPhongs.FirstOrDefault(p => p.MaKP == item.MaKXuat);

                    string hamluong = !string.IsNullOrWhiteSpace(item.HamLuong) ? ("(" + item.HamLuong + ")") : "";
                    //if (item.TenRG.Contains("Thuốc gây nghiện") || item.TenRG.Contains("Thuốc hướng tâm thần"))
                    //{
                    //    dsDT += "- " + "<b><u>" + (item.SL + 1).ToString() + "</u></b>" + item.TenTT05 + hamluong + " x " + item.SoLuongct.ToString() + "  " + item.DonVi + "  " + item.DuongD + (!string.IsNullOrWhiteSpace(item.GhiChu) ? (", " + item.GhiChu) : "") + ((TenKP != null && TenKP.DChi.ToLower() == "nhà thuốc bệnh viện") ? " (tự túc)" : "") + Environment.NewLine;
                    //}
                    //else
                    //{
                    dsDT += "- " + item.TenTT05 + hamluong + " x " + item.SoLuongct.ToString() + " " + item.DonVi + (DungChung.Bien.MaBV == "27001" ? "\r\n" : " ") + item.DuongD + " " + item.SoLan + " " + item.MoiLan + " " + item.Luong + " " + item.DviUong + (!string.IsNullOrWhiteSpace(item.GhiChu) ? (", " + item.GhiChu) : "") + ((TenKP != null && TenKP.DChi.ToLower() == "nhà thuốc bệnh viện") ? " (tự túc)" : "") + Environment.NewLine;
                    //}
                }
                #endregion

                db.Ploai = 99;

                //string DBmau = "+Giờ khám: " + DateTime.Now.Hour + "h" + " \r\n +Triệu chứng: \r\n-Ý thức \r\n-Cảm xúc: \r\n-Tri thức \r\n-Tư duy \r\n+Nội khoa: \r\n-Tim mạch \r\n-Hô hấp \r\n-Khác \r\n+Mạch:	N.độ: \r\n+HA:	Cân nặng:";
                string chamsoc = "";
                if (DungChung.Bien.MaBV != "34019")
                    chamsoc = "Dinh dưỡng:" + Environment.NewLine + "Loại chăm sóc:";
                //if (DungChung.Bien.MaBV != "30007")
                //{
                //    db.DienBien1 = !string.IsNullOrWhiteSpace(oldDienBien) ? oldDienBien : DBmau;
                //}
                //else
                //{
                //    db.DienBien1 = DungChung.Bien.MaBV == "30007" ? "Kết quả:" + Environment.NewLine + klXN30007 + Environment.NewLine + "Chẩn đoán:" + Environment.NewLine + klcdha30007 : klXN30007 + "\n\n" + klcdha30007;
                //}
                if (DungChung.Bien.MaBV == "24272")
                {
                    var ChanDoan = DaTaContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).Select(p => p.ChanDoan).FirstOrDefault();
                    db.DienBien1 = "BT" + Environment.NewLine + "Chẩn đoán:" + Environment.NewLine + ChanDoan + Environment.NewLine + ((!string.IsNullOrWhiteSpace(klCDHA) || !string.IsNullOrWhiteSpace(klPTTT) || !string.IsNullOrWhiteSpace(klTDCN) || !string.IsNullOrWhiteSpace(klXN)) ? ("Kết quả CLS:" + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klXN) ? (klXN + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klCDHA) ? (klCDHA + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klPTTT) ? (klPTTT + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klTDCN) ? (klTDCN + Environment.NewLine) : "");
                }
                else
                {
                    db.DienBien1 = "BT" + Environment.NewLine + ((!string.IsNullOrWhiteSpace(klCDHA) || !string.IsNullOrWhiteSpace(klPTTT) || !string.IsNullOrWhiteSpace(klTDCN) || !string.IsNullOrWhiteSpace(klXN)) ? ("Kết quả CLS:" + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klXN) ? (klXN + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klCDHA) ? (klCDHA + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klPTTT) ? (klPTTT + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klTDCN) ? (klTDCN + Environment.NewLine) : "");
                }
                db.HuongDTri = chamsoc;
                db.NgayNhap = DateTime.Now;
                db.YLenh = dsCLS + (!string.IsNullOrWhiteSpace(dsDT) ? dsDT : "");
                if (bnkb != null)
                    db.MaCB = bnkb.MaCB;

                _ldienbien.Add(db);
            }
            //setSoBA(bnkb.MaKP ?? 0, 0);

            binsDienBien.DataSource = _ldienbien;
            grcDienBien.BeginUpdate();
            grcDienBien.RefreshDataSource();
            grcDienBien.EndUpdate();
            isSave = true;
        }

        bool isDanhSach = false;
        private void btnDanhSach_Click(object sender, EventArgs e)
        {
            frmTaoBenhAnNgoaiTru_FormClosing(null, null);
            isDanhSach = true;
            LoadControlByDanhSach(isDanhSach);
            btnDanhSach.Visible = false;
            LoadDienBienByPerson(_int_maBN);
        }

        private void LoadControlByDanhSach(bool isDS)
        {
            simpleButton4.Visible = !isDS;
            //btngetKQCLS.Visible = !isDS;
            //simpleButton1.Visible = !isDS;
            //btnLayKQTT.Visible = !isDS;
            btnDanhSach.Visible = !isDS;
            label2.Visible = isDS;
            dtFromTime.Visible = isDS;
            label1.Visible = isDS;
            dtToTime.Visible = isDS;
            simpleButton3.Visible = !isDS;
            simpleButton2.Visible = !isDS;
            simpleButton5.Visible = !isDS;
            this.Text = isDS ? "Danh sách diễn biến" : "Thông tin diễn biến";
        }

        int? idPerson;
        private void LoadDienBienByPerson(int maBN)
        {
            DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            idPerson = DaTaContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == maBN).IDPerson;
            _ldienbien = (from db in DaTaContext.DienBiens.Where(p => p.Ploai == 99)
                          join bn in DaTaContext.BenhNhans.Where(o => o.IDPerson == idPerson) on db.MaBNhan equals bn.MaBNhan
                          select db).OrderByDescending(p => p.ID).ToList();

            binsDienBien.DataSource = _ldienbien;
            grcDienBien.DataSource = binsDienBien;
        }

        private void frmTaoBenhAnNgoaiTru_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSave && !isDanhSach)
            {
                if (MessageBox.Show("Bạn chưa lưu dữ liệu bạn có muốn lưu không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    simpleButton2_Click(null, null);
                }
            }
        }

        private void btn30007_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Bạn muốn lấy Kết quả XN & CĐHA vào diễn biến bệnh ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                var xetnghiem = (from cls in DaTaContext.CLS.Where(p => p.MaBNhan == _int_maBN)
                                 join cd in DaTaContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                 join clsct in DaTaContext.CLScts on cd.IDCD equals clsct.IDCD
                                 join dvct in DaTaContext.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                 join dv in DaTaContext.DichVus on cd.MaDV equals dv.MaDV
                                 join tn in DaTaContext.TieuNhomDVs.Where(p => p.IDNhom == 1 || p.IDNhom == 2) on dv.IdTieuNhom equals tn.IdTieuNhom //p.TenRG.Contains("XN")
                                 select new { cls.MaCBth, dv.TenDV, KetQua = "", cls.MaCB, cd.Status, cls.NgayTH, tn.IDNhom, cls.NgayThang, dvct.TenDVct, TenDVctkq = "Kết quả XN: \n" + dvct.TenDVct + ": " + clsct.KetQua + " " + dvct.DonVi, cls.IdCLS, kqcdha = "Kết quả CĐHA : \n" + dv.TenDV + ":" + cd.KetLuan }).OrderBy(p => p.IdCLS).ToList();
                var ketqua = (from a in xetnghiem.Where(p => p.Status == 1 && p.IDNhom == 2)
                              group new { a } by new { a.NgayTH, a.MaCBth } into kq
                              select new
                              {
                                  kq.Key,
                                  TenXN = string.Join("\n", kq.Select(p => p.a.TenDVct + "\n" + p.a.kqcdha).ToArray()),
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
                    if (dbbenh.Where(p => p.NgayNhap == item.Key.NgayThang).FirstOrDefault() != null)
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
                    if (dbbenh.Where(p => p.NgayNhap == item.Key.NgayTH).FirstOrDefault() != null)
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
            frmTaoBenhAnNgoaiTru_Load(sender, e);
        }

        private void Res_Btn_Xoa_Click(object sender, EventArgs e)
        {
            var row = (DienBien)grvDienBien.GetFocusedRow();
            if (row != null && MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (row.ID > 0)
                {
                    DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                    var dienBienDelete = DaTaContext.DienBiens.FirstOrDefault(o => o.ID == row.ID);
                    if (dienBienDelete != null)
                    {
                        DaTaContext.DienBiens.Remove(dienBienDelete);
                    }

                    var benhNhan = DaTaContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == row.MaBNhan);
                    if (benhNhan != null)
                    {
                        benhNhan.DTNT = false;
                    }
                    DaTaContext.SaveChanges();
                    var makp = DungChung.Bien.PP_SoBA == 1 ? (bnkb.MaKP ?? 0) : 0;
                    DungChung.Ham.SoPLDeleteChuyenVien(makp, (!string.IsNullOrWhiteSpace(txtSoBA.Text) ? Convert.ToInt32(txtSoBA.Text) : 0), 4, 0);
                    txtSoBA.Text = "";
                }
                binsDienBien.Remove(row);
                isSave = false;
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            var ktraphuongan = DaTaContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PhuongAn == 0 || p.PhuongAn == 2).ToList();
            if (ktraphuongan.Count() > 0)
            {
                MessageBox.Show("Bệnh nhân đã kết thúc khám, không thế tạo mới !!!");
                return;
            }
            simpleButton2.Enabled = true;
            simpleButton5.Enabled = true;
            simpleButton3.Enabled = false;
            grvDienBien.OptionsBehavior.Editable = true;
            string oldDienBien = "";

            if (_ldienbien != null && _ldienbien.Count > 0 && DungChung.Bien.MaBV != "24272")
            {
                var id = _ldienbien.FirstOrDefault().ID;
                var current = DaTaContext.DienBiens.FirstOrDefault(o => o.ID == id);
                if (current != null)
                {
                    oldDienBien = current.DienBien1;
                    DaTaContext.DienBiens.Remove(current);
                    DaTaContext.SaveChanges();
                    var makp = DungChung.Bien.PP_SoBA == 1 ? (bnkb.MaKP ?? 0) : 0;
                    DungChung.Ham.SoPLDeleteChuyenVien(makp, (!string.IsNullOrWhiteSpace(txtSoBA.Text) ? Convert.ToInt32(txtSoBA.Text) : 0), 4, 0);
                    txtSoBA.Text = "";
                }
                _ldienbien = new List<DienBien>();
            }

            if (_ldienbien != null || _ldienbien.Count <= 0)
            {
                DienBien db = new DienBien();

                #region CLS
                List<MyObject> _lall = new List<MyObject>();
                List<MyObject> qcd = (from cd in DaTaContext.ChiDinhs.Where(p => p.TrongBH == 0 || p.TrongBH == 2)
                                      join cls in DaTaContext.CLS.Where(o => o.MaBNhan == _int_maBN) on cd.IdCLS equals cls.IdCLS
                                      join clsctNotNull in DaTaContext.CLScts on cd.IDCD equals clsctNotNull.IDCD into kq
                                      from clsct in kq.DefaultIfEmpty()
                                      join dv in DaTaContext.DichVus on cd.MaDV equals dv.MaDV
                                      join dvctNotNull in DaTaContext.DichVucts on clsct.MaDVct equals dvctNotNull.MaDVct into kq1
                                      from dvct in kq1.DefaultIfEmpty()
                                      join tn in DaTaContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                      join n in DaTaContext.NhomDVs on tn.IDNhom equals n.IDNhom
                                      select new MyObject
                                      {
                                          IDCD = cd.IDCD,
                                          IDNhom = tn.IDNhom ?? 0,
                                          TenNhom = n.TenNhom,
                                          TenTN = tn.TenRG,
                                          TenRG = dv.TenDV,
                                          TenDVct = dvct != null ? dvct.TenDVct : "",
                                          KetQua = clsct != null ? (DungChung.Bien.MaBV == "30009" ? (clsct.KetQua + " " + dvct.DonVi) : clsct.KetQua) : clsct.KetQua,
                                          KetLuan = cd.KetLuan,
                                          MoTa = cd.MoTa,
                                          NgayTH = cls.NgayTH,
                                      }).ToList();
                _lall.AddRange(qcd);

                _lall = _lall.OrderBy(p => p.NgayTH).ThenBy(p => p.TenTN).ThenBy(p => p.TenRG).ToList();

                var q1 = (from dv in _lall
                          group dv by new { dv.TenNhom, dv.IDNhom } into x
                          select new
                          {
                              x.Key.IDNhom,
                              TenNhom = x.Key.TenNhom,
                              TenRG = "- " + x.Key.TenNhom + ": " + string.Join(", ", x.Select(p => p.TenRG).ToList().Distinct()) + Environment.NewLine,
                              CDHA = x.Key.IDNhom == 2 ? /*batdau*/ string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).Select(p => ("- " + p.TenRG + (!string.IsNullOrWhiteSpace(p.KetQua) && DungChung.Bien.MaBV != "27001" ? (": " + DungChung.Bien.MaBV == "27001" ? (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "") : p.KetQua + (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "")) : (!string.IsNullOrWhiteSpace(p.KetLuan) ? (": " + p.KetLuan) : ""))))) : "",
                              XN = x.Key.IDNhom == 1 ? (string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG, }).Select(p => (("- " + (p.Count() == 1 ? (p.FirstOrDefault().TenDVct + " " + p.FirstOrDefault().KetQua) : (p.Key.TenRG + (p.Count() > 0 ? ": " : "") + string.Join(", ", p.Select(o => (o.TenDVct + (!string.IsNullOrWhiteSpace(o.KetQua) ? (" " + o.KetQua) : ""))))))))))) : "",
                              PTTT = x.Key.IDNhom == 8 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).Select(p => ("- " + p.TenRG + (!string.IsNullOrWhiteSpace(p.KetQua) ? (": " + p.KetQua + (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "")) : (!string.IsNullOrWhiteSpace(p.KetLuan) ? (": " + p.KetLuan) : ""))))) : "",
                              TDCN = x.Key.IDNhom == 3 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG }).Select(p => (("- " + (p.Count() == 1 ? (p.FirstOrDefault().TenDVct + " " + p.FirstOrDefault().KetQua) : (p.Key.TenRG + (p.Count() > 0 ? ": " : "") + string.Join(", ", p.Select(o => (o.TenDVct + (!string.IsNullOrWhiteSpace(o.KetQua) ? (" " + o.KetQua) : "")))))))))) : "",// his 120 11/05/2021
                              TDCN27001 = x.Key.IDNhom == 3 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG }).Select(p => (("\t- " + (p.Count() == 1 ? (p.FirstOrDefault().TenRG + ": " + p.FirstOrDefault().KetLuan) : (string.Join(", ", p.Select(o => (o.TenRG + ": " + (!string.IsNullOrWhiteSpace(o.KetLuan) ? (" " + o.KetLuan) : ""))).Distinct()))))))) : "",// his-327 27/07/2021
                          }).ToList();

                string dsCLS = q1.Count > 0 ? ("Chỉ định CLS:" + Environment.NewLine + string.Join("", q1.Select(p => p.TenRG))) : "";

                string klCDHA = (q1.FirstOrDefault(o => o.IDNhom == 2) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 2).CDHA)) ? ("CĐHA:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 2).CDHA) : "";
                string klXN = (q1.FirstOrDefault(o => o.IDNhom == 1) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 1).XN)) ? ("XN:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 1).XN) : "";
                string klPTTT = (q1.FirstOrDefault(o => o.IDNhom == 8) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 8).PTTT)) ? ("THỦ THUẬT PHẪU THUẬT:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 8).PTTT) : "";
                string klTDCN = (q1.FirstOrDefault(o => o.IDNhom == 3) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 3).TDCN)) ? (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "30007" ? (("THĂM DÒ CHỨC NĂNG:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 3).TDCN27001)) : ("THĂM DÒ CHỨC NĂNG:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 3).TDCN)) : ""; // his-327 27/07/2021

                #endregion

                #region Đơn thuốc
                var ktstt = (from dt in DaTaContext.DThuocs.Where(p => p.MaBNhan == _int_maBN && p.PLDV == 1)
                             join dtct in DaTaContext.DThuoccts.Where(p => p.TrongBH == 0 || p.TrongBH == 2) on dt.IDDon equals dtct.IDDon
                             join dv in DaTaContext.DichVus on dtct.MaDV equals dv.MaDV
                             join tn in DaTaContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             select new { dv.MaDV, tn.TenRG, dv.DongY, TenTT05 = dv.TenDV, dv.HamLuong, dtct.DuongD, dtct.DonVi, dtct.GhiChu, dtct.MaKXuat, dtct.SoLuongct, dtct.SoLan, dtct.MoiLan, dtct.Luong, dtct.DviUong }).ToList();
                var dtGroup = (from d in ktstt
                               group d by new { d.MaDV, d.TenRG, d.DongY, d.TenTT05, d.HamLuong, d.DuongD, d.DonVi, d.GhiChu, d.MaKXuat, d.SoLuongct, d.SoLan, d.MoiLan, d.Luong, d.DviUong } into kq
                               select new
                               {
                                   kq.Key.MaDV,
                                   kq.Key.TenRG,
                                   kq.Key.DongY,
                                   kq.Key.TenTT05,
                                   kq.Key.HamLuong,
                                   kq.Key.DuongD,
                                   kq.Key.DonVi,
                                   kq.Key.GhiChu,
                                   kq.Key.MaKXuat,
                                   kq.Key.SoLuongct,
                                   kq.Key.SoLan,
                                   kq.Key.MoiLan,
                                   kq.Key.Luong,
                                   kq.Key.DviUong,
                                   SL = kq.Where(o => o.TenRG.Contains("Thuốc gây nghiện") || o.TenRG.Contains("Thuốc hướng tâm thần")).Count()
                               }
                          ).ToList();

                string dsDT = (dtGroup == null || dtGroup.Count == 0) ? "" : "THUỐC:" + Environment.NewLine;
                foreach (var item in dtGroup)
                {
                    var TenKP = DaTaContext.KPhongs.FirstOrDefault(p => p.MaKP == item.MaKXuat);

                    string hamluong = !string.IsNullOrWhiteSpace(item.HamLuong) ? ("(" + item.HamLuong + ")") : "";
                    //if (item.TenRG.Contains("Thuốc gây nghiện") || item.TenRG.Contains("Thuốc hướng tâm thần"))
                    //{
                    //    dsDT += "- " + "<b><u>" + (item.SL + 1).ToString() + "</u></b>" + item.TenTT05 + hamluong + " x " + item.SoLuongct.ToString() + "  " + item.DonVi + "  " + item.DuongD + (!string.IsNullOrWhiteSpace(item.GhiChu) ? (", " + item.GhiChu) : "") + ((TenKP != null && TenKP.DChi.ToLower() == "nhà thuốc bệnh viện") ? " (tự túc)" : "") + Environment.NewLine;
                    //}
                    //else
                    //{
                    dsDT += "- " + item.TenTT05 + hamluong + " x " + item.SoLuongct.ToString() + " " + item.DonVi + (DungChung.Bien.MaBV == "27001" ? "\r\n" : " ") + item.DuongD + " " + item.SoLan + " " + item.MoiLan + " " + item.Luong + " " + item.DviUong + (!string.IsNullOrWhiteSpace(item.GhiChu) ? (", " + item.GhiChu) : "") + ((TenKP != null && TenKP.DChi.ToLower() == "nhà thuốc bệnh viện") ? " (tự túc)" : "") + Environment.NewLine;
                    //}
                }
                #endregion

                db.Ploai = 99;

                //string DBmau = "+Giờ khám: " + DateTime.Now.Hour + "h" + " \r\n +Triệu chứng: \r\n-Ý thức \r\n-Cảm xúc: \r\n-Tri thức \r\n-Tư duy \r\n+Nội khoa: \r\n-Tim mạch \r\n-Hô hấp \r\n-Khác \r\n+Mạch:	N.độ: \r\n+HA:	Cân nặng:";
                string chamsoc = "";
                if (DungChung.Bien.MaBV != "34019")
                    chamsoc = "Dinh dưỡng:" + Environment.NewLine + "Loại chăm sóc:";
                //if (DungChung.Bien.MaBV != "30007")
                //{
                //    db.DienBien1 = !string.IsNullOrWhiteSpace(oldDienBien) ? oldDienBien : DBmau;
                //}
                //else
                //{
                //    db.DienBien1 = DungChung.Bien.MaBV == "30007" ? "Kết quả:" + Environment.NewLine + klXN30007 + Environment.NewLine + "Chẩn đoán:" + Environment.NewLine + klcdha30007 : klXN30007 + "\n\n" + klcdha30007;
                //}

                db.DienBien1 = "BT" + Environment.NewLine + ((!string.IsNullOrWhiteSpace(klCDHA) || !string.IsNullOrWhiteSpace(klPTTT) || !string.IsNullOrWhiteSpace(klTDCN) || !string.IsNullOrWhiteSpace(klXN)) ? ("Kết quả CLS:" + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klXN) ? (klXN + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klCDHA) ? (klCDHA + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klPTTT) ? (klPTTT + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klTDCN) ? (klTDCN + Environment.NewLine) : "");
                db.HuongDTri = chamsoc;
                db.NgayNhap = DateTime.Now;
                db.YLenh = dsCLS + (!string.IsNullOrWhiteSpace(dsDT) ? dsDT : "");
                if (bnkb != null)
                    db.MaCB = bnkb.MaCB;

                _ldienbien.Add(db);
            }
            //setSoBA(bnkb.MaKP ?? 0, 0);

            binsDienBien.DataSource = _ldienbien;
            grcDienBien.BeginUpdate();
            grcDienBien.RefreshDataSource();
            grcDienBien.EndUpdate();
            isSave = true;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            var ktraphuongan = DaTaContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PhuongAn == 0 || p.PhuongAn == 2).ToList();
            if (ktraphuongan.Count() > 0)
            {
                MessageBox.Show("Bệnh nhân đã kết thúc khám, không thế tạo mới !!!");
                return;
            }
            simpleButton2.Enabled = true;
            simpleButton5.Enabled = true;
            simpleButton3.Enabled = false;
            grvDienBien.OptionsBehavior.Editable = true;
            string oldDienBien = "";

            if (_ldienbien != null && _ldienbien.Count > 0 && DungChung.Bien.MaBV != "24272")
            {
                var id = _ldienbien.FirstOrDefault().ID;
                var current = DaTaContext.DienBiens.FirstOrDefault(o => o.ID == id);
                if (current != null)
                {
                    oldDienBien = current.DienBien1;
                    DaTaContext.DienBiens.Remove(current);
                    DaTaContext.SaveChanges();
                    var makp = DungChung.Bien.PP_SoBA == 1 ? (bnkb.MaKP ?? 0) : 0;
                    DungChung.Ham.SoPLDeleteChuyenVien(makp, (!string.IsNullOrWhiteSpace(txtSoBA.Text) ? Convert.ToInt32(txtSoBA.Text) : 0), 4, 0);
                    txtSoBA.Text = "";
                }
                _ldienbien = new List<DienBien>();
            }

            if (_ldienbien != null || _ldienbien.Count <= 0)
            {
                DienBien db = new DienBien();

                #region CLS
                List<MyObject> _lall = new List<MyObject>();
                List<MyObject> qcd = (from cd in DaTaContext.ChiDinhs.Where(p => p.TrongBH == 1)
                                      join cls in DaTaContext.CLS.Where(o => o.MaBNhan == _int_maBN) on cd.IdCLS equals cls.IdCLS
                                      join clsctNotNull in DaTaContext.CLScts on cd.IDCD equals clsctNotNull.IDCD into kq
                                      from clsct in kq.DefaultIfEmpty()
                                      join dv in DaTaContext.DichVus.Where(p => p.TrongDM == 1) on cd.MaDV equals dv.MaDV
                                      join dvctNotNull in DaTaContext.DichVucts on clsct.MaDVct equals dvctNotNull.MaDVct into kq1
                                      from dvct in kq1.DefaultIfEmpty()
                                      join tn in DaTaContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                      join n in DaTaContext.NhomDVs on tn.IDNhom equals n.IDNhom
                                      select new MyObject
                                      {
                                          IDCD = cd.IDCD,
                                          IDNhom = tn.IDNhom ?? 0,
                                          TenNhom = n.TenNhom,
                                          TenTN = tn.TenRG,
                                          TenRG = dv.TenDV,
                                          TenDVct = dvct != null ? dvct.TenDVct : "",
                                          KetQua = clsct != null ? (DungChung.Bien.MaBV == "30009" ? (clsct.KetQua + " " + dvct.DonVi) : clsct.KetQua) : clsct.KetQua,
                                          KetLuan = cd.KetLuan,
                                          MoTa = cd.MoTa,
                                          NgayTH = cls.NgayTH,
                                      }).ToList();
                _lall.AddRange(qcd);

                _lall = _lall.OrderBy(p => p.NgayTH).ThenBy(p => p.TenTN).ThenBy(p => p.TenRG).ToList();

                var q1 = (from dv in _lall
                          group dv by new { dv.TenNhom, dv.IDNhom } into x
                          select new
                          {
                              x.Key.IDNhom,
                              TenNhom = x.Key.TenNhom,
                              TenRG = "- " + x.Key.TenNhom + ": " + string.Join(", ", x.Select(p => p.TenRG).ToList().Distinct()) + Environment.NewLine,
                              CDHA = x.Key.IDNhom == 2 ? /*batdau*/ string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).Select(p => ("- " + p.TenRG + (!string.IsNullOrWhiteSpace(p.KetQua) && DungChung.Bien.MaBV != "27001" ? (": " + DungChung.Bien.MaBV == "27001" ? (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "") : p.KetQua + (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "")) : (!string.IsNullOrWhiteSpace(p.KetLuan) ? (": " + p.KetLuan) : ""))))) : "",
                              XN = x.Key.IDNhom == 1 ? (string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG, }).Select(p => (("- " + (p.Count() == 1 ? (p.FirstOrDefault().TenDVct + " " + p.FirstOrDefault().KetQua) : (p.Key.TenRG + (p.Count() > 0 ? ": " : "") + string.Join(", ", p.Select(o => (o.TenDVct + (!string.IsNullOrWhiteSpace(o.KetQua) ? (" " + o.KetQua) : ""))))))))))) : "",
                              PTTT = x.Key.IDNhom == 8 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).Select(p => ("- " + p.TenRG + (!string.IsNullOrWhiteSpace(p.KetQua) ? (": " + p.KetQua + (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "")) : (!string.IsNullOrWhiteSpace(p.KetLuan) ? (": " + p.KetLuan) : ""))))) : "",
                              TDCN = x.Key.IDNhom == 3 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG }).Select(p => (("- " + (p.Count() == 1 ? (p.FirstOrDefault().TenDVct + " " + p.FirstOrDefault().KetQua) : (p.Key.TenRG + (p.Count() > 0 ? ": " : "") + string.Join(", ", p.Select(o => (o.TenDVct + (!string.IsNullOrWhiteSpace(o.KetQua) ? (" " + o.KetQua) : "")))))))))) : "",// his 120 11/05/2021
                              TDCN27001 = x.Key.IDNhom == 3 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG }).Select(p => (("\t- " + (p.Count() == 1 ? (p.FirstOrDefault().TenRG + ": " + p.FirstOrDefault().KetLuan) : (string.Join(", ", p.Select(o => (o.TenRG + ": " + (!string.IsNullOrWhiteSpace(o.KetLuan) ? (" " + o.KetLuan) : ""))).Distinct()))))))) : "",// his-327 27/07/2021
                          }).ToList();

                string dsCLS = q1.Count > 0 ? ("Chỉ định CLS:" + Environment.NewLine + string.Join("", q1.Select(p => p.TenRG))) : "";

                string klCDHA = (q1.FirstOrDefault(o => o.IDNhom == 2) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 2).CDHA)) ? ("CĐHA:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 2).CDHA) : "";
                string klXN = (q1.FirstOrDefault(o => o.IDNhom == 1) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 1).XN)) ? ("XN:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 1).XN) : "";
                string klPTTT = (q1.FirstOrDefault(o => o.IDNhom == 8) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 8).PTTT)) ? ("THỦ THUẬT PHẪU THUẬT:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 8).PTTT) : "";
                string klTDCN = (q1.FirstOrDefault(o => o.IDNhom == 3) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 3).TDCN)) ? (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "30007" ? (("THĂM DÒ CHỨC NĂNG:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 3).TDCN27001)) : ("THĂM DÒ CHỨC NĂNG:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 3).TDCN)) : ""; // his-327 27/07/2021

                #endregion

                #region Đơn thuốc
                var ktstt = (from dt in DaTaContext.DThuocs.Where(p => p.MaBNhan == _int_maBN && p.PLDV == 1)
                             join dtct in DaTaContext.DThuoccts.Where(p => p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                             join dv in DaTaContext.DichVus on dtct.MaDV equals dv.MaDV
                             join tn in DaTaContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             select new { dv.MaDV, tn.TenRG, dv.DongY, TenTT05 = dv.TenDV, dv.HamLuong, dtct.DuongD, dtct.DonVi, dtct.GhiChu, dtct.MaKXuat, dtct.SoLuongct, dtct.SoLan, dtct.MoiLan, dtct.Luong, dtct.DviUong }).ToList();
                var dtGroup = (from d in ktstt
                               group d by new { d.MaDV, d.TenRG, d.DongY, d.TenTT05, d.HamLuong, d.DuongD, d.DonVi, d.GhiChu, d.MaKXuat, d.SoLuongct, d.SoLan, d.MoiLan, d.Luong, d.DviUong } into kq
                               select new
                               {
                                   kq.Key.MaDV,
                                   kq.Key.TenRG,
                                   kq.Key.DongY,
                                   kq.Key.TenTT05,
                                   kq.Key.HamLuong,
                                   kq.Key.DuongD,
                                   kq.Key.DonVi,
                                   kq.Key.GhiChu,
                                   kq.Key.MaKXuat,
                                   kq.Key.SoLuongct,
                                   kq.Key.SoLan,
                                   kq.Key.MoiLan,
                                   kq.Key.Luong,
                                   kq.Key.DviUong,
                                   SL = kq.Where(o => o.TenRG.Contains("Thuốc gây nghiện") || o.TenRG.Contains("Thuốc hướng tâm thần")).Count()
                               }
                          ).ToList();

                string dsDT = (dtGroup == null || dtGroup.Count == 0) ? "" : "THUỐC:" + Environment.NewLine;
                foreach (var item in dtGroup)
                {
                    var TenKP = DaTaContext.KPhongs.FirstOrDefault(p => p.MaKP == item.MaKXuat);

                    string hamluong = !string.IsNullOrWhiteSpace(item.HamLuong) ? ("(" + item.HamLuong + ")") : "";
                    //if (item.TenRG.Contains("Thuốc gây nghiện") || item.TenRG.Contains("Thuốc hướng tâm thần"))
                    //{
                    //    dsDT += "- " + "<b><u>" + (item.SL + 1).ToString() + "</u></b>" + item.TenTT05 + hamluong + " x " + item.SoLuongct.ToString() + "  " + item.DonVi + "  " + item.DuongD + (!string.IsNullOrWhiteSpace(item.GhiChu) ? (", " + item.GhiChu) : "") + ((TenKP != null && TenKP.DChi.ToLower() == "nhà thuốc bệnh viện") ? " (tự túc)" : "") + Environment.NewLine;
                    //}
                    //else
                    //{
                    dsDT += "- " + item.TenTT05 + hamluong + " x " + item.SoLuongct.ToString() + " " + item.DonVi + (DungChung.Bien.MaBV == "27001" ? "\r\n" : " ") + item.DuongD + " " + item.SoLan + " " + item.MoiLan + " " + item.Luong + " " + item.DviUong + (!string.IsNullOrWhiteSpace(item.GhiChu) ? (", " + item.GhiChu) : "") + ((TenKP != null && TenKP.DChi.ToLower() == "nhà thuốc bệnh viện") ? " (tự túc)" : "") + Environment.NewLine;
                    //}
                }
                #endregion

                db.Ploai = 99;

                //string DBmau = "+Giờ khám: " + DateTime.Now.Hour + "h" + " \r\n +Triệu chứng: \r\n-Ý thức \r\n-Cảm xúc: \r\n-Tri thức \r\n-Tư duy \r\n+Nội khoa: \r\n-Tim mạch \r\n-Hô hấp \r\n-Khác \r\n+Mạch:	N.độ: \r\n+HA:	Cân nặng:";
                string chamsoc = "";
                if (DungChung.Bien.MaBV != "34019")
                    chamsoc = "Dinh dưỡng:" + Environment.NewLine + "Loại chăm sóc:";
                //if (DungChung.Bien.MaBV != "30007")
                //{
                //    db.DienBien1 = !string.IsNullOrWhiteSpace(oldDienBien) ? oldDienBien : DBmau;
                //}
                //else
                //{
                //    db.DienBien1 = DungChung.Bien.MaBV == "30007" ? "Kết quả:" + Environment.NewLine + klXN30007 + Environment.NewLine + "Chẩn đoán:" + Environment.NewLine + klcdha30007 : klXN30007 + "\n\n" + klcdha30007;
                //}

                db.DienBien1 = "BT" + Environment.NewLine + ((!string.IsNullOrWhiteSpace(klCDHA) || !string.IsNullOrWhiteSpace(klPTTT) || !string.IsNullOrWhiteSpace(klTDCN) || !string.IsNullOrWhiteSpace(klXN)) ? ("Kết quả CLS:" + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klXN) ? (klXN + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klCDHA) ? (klCDHA + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klPTTT) ? (klPTTT + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klTDCN) ? (klTDCN + Environment.NewLine) : "");
                db.HuongDTri = chamsoc;
                db.NgayNhap = DateTime.Now;
                db.YLenh = dsCLS + (!string.IsNullOrWhiteSpace(dsDT) ? dsDT : "");
                if (bnkb != null)
                    db.MaCB = bnkb.MaCB;

                _ldienbien.Add(db);
            }
            //setSoBA(bnkb.MaKP ?? 0, 0);

            binsDienBien.DataSource = _ldienbien;
            grcDienBien.BeginUpdate();
            grcDienBien.RefreshDataSource();
            grcDienBien.EndUpdate();
            isSave = true;
        }
        class dthuoc
        {
            public string ylenh { get; set; }
        }
        class dbien
        {
            public string dienbien2 { get; set; }
        }
        private void btnLayDT_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvDienBien.RowCount; i++)
            {
                int id = 0;
                if (grvDienBien.GetRowCellValue(i, colIDdb) != null && grvDienBien.GetRowCellValue(i, colIDdb).ToString() != "")
                    id = Convert.ToInt32(grvDienBien.GetRowCellValue(i, colIDdb).ToString());
                if (id != 0)
                {
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        string ylenh = "";
                        var db = DaTaContext.DienBiens.Single(p => p.ID == id);
                        var ldt = (from dt in DaTaContext.DThuocs.Where(p => p.MaBNhan == _int_maBN && p.KieuDon == -1)
                                   join dtct in DaTaContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                   join dv in DaTaContext.DichVus.Where(p => p.PLoai == 1) on dtct.MaDV equals dv.MaDV
                                   select new
                                   {
                                       dv.TenDV,
                                       dv.HamLuong,
                                       dtct.SoLuong,
                                       dtct.DonVi,
                                       dtct.DuongD,
                                       dtct.SoLan,
                                       dtct.MoiLan,
                                       dtct.Luong,
                                       dtct.DviUong,
                                       dtct.IDDonct,
                                       dtct.GhiChu
                                   }).OrderBy(p => p.IDDonct).ToList();
                        List<MyObject> _lall = new List<MyObject>();
                        List<MyObject> qcd = (from cd in DaTaContext.ChiDinhs
                                              join cls in DaTaContext.CLS.Where(o => o.MaBNhan == _int_maBN && o.Status == 1) on cd.IdCLS equals cls.IdCLS
                                              join clsctNotNull in DaTaContext.CLScts on cd.IDCD equals clsctNotNull.IDCD into kq
                                              from clsct in kq.DefaultIfEmpty()
                                              join dv in DaTaContext.DichVus on cd.MaDV equals dv.MaDV
                                              join dvctNotNull in DaTaContext.DichVucts on clsct.MaDVct equals dvctNotNull.MaDVct into kq1
                                              from dvct in kq1.DefaultIfEmpty()
                                              join tn in DaTaContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                              join n in DaTaContext.NhomDVs on tn.IDNhom equals n.IDNhom
                                              select new MyObject
                                              {
                                                  IDCD = cd.IDCD,
                                                  IDNhom = tn.IDNhom ?? 0,
                                                  TenNhom = n.TenNhom,
                                                  TenTN = tn.TenRG,
                                                  TenRG = dv.TenDV,
                                                  TenDVct = dvct != null ? dvct.TenDVct : "",
                                                  KetQua = clsct != null ? (DungChung.Bien.MaBV == "30009" ? (clsct.KetQua + " " + dvct.DonVi) : clsct.KetQua) : clsct.KetQua,
                                                  KetLuan = cd.KetLuan,
                                                  MoTa = cd.MoTa,
                                                  NgayTH = cls.NgayTH,
                                              }).ToList();
                        _lall.AddRange(qcd);

                        _lall = _lall.OrderBy(p => p.NgayTH).ThenBy(p => p.TenTN).ThenBy(p => p.TenRG).ToList();

                        var q1 = (from dv in _lall
                                  group dv by new { dv.TenNhom, dv.IDNhom } into x
                                  select new
                                  {
                                      x.Key.IDNhom,
                                      TenNhom = x.Key.TenNhom,
                                      TenRG = "- " + x.Key.TenNhom + ": " + string.Join(", ", x.Select(p => p.TenRG).ToList().Distinct()) + Environment.NewLine,
                                      CDHA = x.Key.IDNhom == 2 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).Select(p => ("- " + p.TenRG + (!string.IsNullOrWhiteSpace(p.KetQua) && DungChung.Bien.MaBV != "27001" ? (": " + DungChung.Bien.MaBV == "27001" ? (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "") : p.KetQua + (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "")) : (!string.IsNullOrWhiteSpace(p.KetLuan) ? (": " + p.KetLuan) : ""))))) : "",
                                      XN = x.Key.IDNhom == 1 ? (string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG, }).Select(p => (("- " + (p.Count() == 1 ? (p.FirstOrDefault().TenDVct + " " + p.FirstOrDefault().KetQua) : (p.Key.TenRG + (p.Count() > 0 ? ": " : "") + string.Join(", ", p.Select(o => (o.TenDVct + (!string.IsNullOrWhiteSpace(o.KetQua) ? (" " + o.KetQua) : ""))))))))))) : "",
                                      PTTT = x.Key.IDNhom == 8 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).Select(p => ("- " + p.TenRG + (!string.IsNullOrWhiteSpace(p.KetQua) ? (": " + p.KetQua + (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "")) : (!string.IsNullOrWhiteSpace(p.KetLuan) ? (": " + p.KetLuan) : ""))))) : "",
                                      TDCN = x.Key.IDNhom == 3 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG }).Select(p => (("- " + (p.Count() == 1 ? (p.FirstOrDefault().TenDVct + " " + p.FirstOrDefault().KetQua) : (p.Key.TenRG + (p.Count() > 0 ? ": " : "") + string.Join(", ", p.Select(o => (o.TenDVct + (!string.IsNullOrWhiteSpace(o.KetQua) ? (" " + o.KetQua) : "")))))))))) : "",// his 120 11/05/2021
                                      TDCN27001 = x.Key.IDNhom == 3 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG }).Select(p => (("\t- " + (p.Count() == 1 ? (p.FirstOrDefault().TenRG + ": " + p.FirstOrDefault().KetLuan) : (string.Join(", ", p.Select(o => (o.TenRG + ": " + (!string.IsNullOrWhiteSpace(o.KetLuan) ? (" " + o.KetLuan) : ""))).Distinct()))))))) : "",// his-327 27/07/2021
                                  }).ToList();

                        string dsCLS = q1.Count > 0 ? ("Chỉ định CLS:" + Environment.NewLine + string.Join("", q1.Select(p => p.TenRG))) : "";
                        ylenh += dsCLS;
                        ylenh += "THUỐC:" + "\r\n";
                        foreach (var a in ldt)
                        {
                            dthuoc _lst = new dthuoc();
                            _lst.ylenh = "- " + a.TenDV + "(" + a.HamLuong + ")" + "x" + a.SoLuong + " " + a.DonVi + " " + a.DuongD + " " + a.SoLan + " " + a.MoiLan + " " + a.Luong + " " + a.DviUong + "," + a.GhiChu + "\r\n";

                                ylenh += _lst.ylenh;
                        }
                        if (!db.YLenh.Contains(ylenh))
                            db.YLenh = Environment.NewLine + db.YLenh + ylenh;
                    }

                    else
                    {
                        var db = DaTaContext.DienBiens.Single(p => p.ID == id);
                        db.YLenh = "";
                        var ldt = (from dt in DaTaContext.DThuocs.Where(p => p.MaBNhan == _int_maBN && p.KieuDon == -1)
                                   join dtct in DaTaContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                   join dv in DaTaContext.DichVus.Where(p => p.PLoai == 1) on dtct.MaDV equals dv.MaDV
                                   select new
                                   {
                                       dv.TenDV,
                                       dv.HamLuong,
                                       dtct.SoLuong,
                                       dtct.DonVi,
                                       dtct.DuongD,
                                       dtct.SoLan,
                                       dtct.MoiLan,
                                       dtct.Luong,
                                       dtct.DviUong,
                                       dtct.IDDonct,
                                       dtct.GhiChu
                                   }).OrderBy(p => p.IDDonct).ToList();
                        List<MyObject> _lall = new List<MyObject>();
                        List<MyObject> qcd = (from cd in DaTaContext.ChiDinhs
                                              join cls in DaTaContext.CLS.Where(o => o.MaBNhan == _int_maBN && o.Status == 1) on cd.IdCLS equals cls.IdCLS
                                              join clsctNotNull in DaTaContext.CLScts on cd.IDCD equals clsctNotNull.IDCD into kq
                                              from clsct in kq.DefaultIfEmpty()
                                              join dv in DaTaContext.DichVus on cd.MaDV equals dv.MaDV
                                              join dvctNotNull in DaTaContext.DichVucts on clsct.MaDVct equals dvctNotNull.MaDVct into kq1
                                              from dvct in kq1.DefaultIfEmpty()
                                              join tn in DaTaContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                              join n in DaTaContext.NhomDVs on tn.IDNhom equals n.IDNhom
                                              select new MyObject
                                              {
                                                  IDCD = cd.IDCD,
                                                  IDNhom = tn.IDNhom ?? 0,
                                                  TenNhom = n.TenNhom,
                                                  TenTN = tn.TenRG,
                                                  TenRG = dv.TenDV,
                                                  TenDVct = dvct != null ? dvct.TenDVct : "",
                                                  KetQua = clsct != null ? (DungChung.Bien.MaBV == "30009" ? (clsct.KetQua + " " + dvct.DonVi) : clsct.KetQua) : clsct.KetQua,
                                                  KetLuan = cd.KetLuan,
                                                  MoTa = cd.MoTa,
                                                  NgayTH = cls.NgayTH,
                                              }).ToList();
                        _lall.AddRange(qcd);

                        _lall = _lall.OrderBy(p => p.NgayTH).ThenBy(p => p.TenTN).ThenBy(p => p.TenRG).ToList();

                        var q1 = (from dv in _lall
                                  group dv by new { dv.TenNhom, dv.IDNhom } into x
                                  select new
                                  {
                                      x.Key.IDNhom,
                                      TenNhom = x.Key.TenNhom,
                                      TenRG = "- " + x.Key.TenNhom + ": " + string.Join(", ", x.Select(p => p.TenRG).ToList().Distinct()) + Environment.NewLine,
                                      CDHA = x.Key.IDNhom == 2 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).Select(p => ("- " + p.TenRG + (!string.IsNullOrWhiteSpace(p.KetQua) && DungChung.Bien.MaBV != "27001" ? (": " + DungChung.Bien.MaBV == "27001" ? (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "") : p.KetQua + (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "")) : (!string.IsNullOrWhiteSpace(p.KetLuan) ? (": " + p.KetLuan) : ""))))) : "",
                                      XN = x.Key.IDNhom == 1 ? (string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG, }).Select(p => (("- " + (p.Count() == 1 ? (p.FirstOrDefault().TenDVct + " " + p.FirstOrDefault().KetQua) : (p.Key.TenRG + (p.Count() > 0 ? ": " : "") + string.Join(", ", p.Select(o => (o.TenDVct + (!string.IsNullOrWhiteSpace(o.KetQua) ? (" " + o.KetQua) : ""))))))))))) : "",
                                      PTTT = x.Key.IDNhom == 8 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).Select(p => ("- " + p.TenRG + (!string.IsNullOrWhiteSpace(p.KetQua) ? (": " + p.KetQua + (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "")) : (!string.IsNullOrWhiteSpace(p.KetLuan) ? (": " + p.KetLuan) : ""))))) : "",
                                      TDCN = x.Key.IDNhom == 3 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG }).Select(p => (("- " + (p.Count() == 1 ? (p.FirstOrDefault().TenDVct + " " + p.FirstOrDefault().KetQua) : (p.Key.TenRG + (p.Count() > 0 ? ": " : "") + string.Join(", ", p.Select(o => (o.TenDVct + (!string.IsNullOrWhiteSpace(o.KetQua) ? (" " + o.KetQua) : "")))))))))) : "",// his 120 11/05/2021
                                      TDCN27001 = x.Key.IDNhom == 3 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG }).Select(p => (("\t- " + (p.Count() == 1 ? (p.FirstOrDefault().TenRG + ": " + p.FirstOrDefault().KetLuan) : (string.Join(", ", p.Select(o => (o.TenRG + ": " + (!string.IsNullOrWhiteSpace(o.KetLuan) ? (" " + o.KetLuan) : ""))).Distinct()))))))) : "",// his-327 27/07/2021
                                  }).ToList();

                        string dsCLS = q1.Count > 0 ? ("Chỉ định CLS:" + Environment.NewLine + string.Join("", q1.Select(p => p.TenRG))) : "";
                        db.YLenh += dsCLS;
                        db.YLenh = _ldienbien.First().YLenh + "\r\n" + "THUỐC:" + "\r\n";
                        foreach (var a in ldt)
                        {
                            dthuoc _lst = new dthuoc();
                            _lst.ylenh = "- " + a.TenDV + "(" + a.HamLuong + ")" + "x" + a.SoLuong + " " + a.DonVi + " " + a.DuongD + " " + a.SoLan + " " + a.MoiLan + " " + a.Luong + " " + a.DviUong + "," + a.GhiChu + "\r\n";
                            db.YLenh += _lst.ylenh;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bệnh nhân chưa có đơn thuốc, dịch vụ kỹ thuật");
                    return;
                }
            }
            DaTaContext.SaveChanges();
            frmTaoBenhAnNgoaiTru_Load(null, null);
        }

        private void btnLayDVKT_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvDienBien.RowCount; i++)
            {
                int id = 0;
                if (grvDienBien.GetRowCellValue(i, colIDdb) != null && grvDienBien.GetRowCellValue(i, colIDdb).ToString() != "")
                    id = Convert.ToInt32(grvDienBien.GetRowCellValue(i, colIDdb).ToString());
                if (id != 0)
                {
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        var db = DaTaContext.DienBiens.Single(p => p.ID == id);

                        var cs = (from ttbx in DaTaContext.TTboXungs.Where(p => p.MaBNhan == _int_maBN)
                                  select new
                                  {
                                      ttbx.Mach_NDo_HAp,
                                      ttbx.CanNang_ChieuCao,
                                  }).ToList();
                        string CSCT = "";
                        if (cs != null && !string.IsNullOrEmpty(cs.First().Mach_NDo_HAp) && !string.IsNullOrEmpty(cs.First().CanNang_ChieuCao))
                        {
                            CSCT = "CHỈ SỐ CƠ THỂ:" + Environment.NewLine + "Mạch: " + cs.First().Mach_NDo_HAp.Split(';')[0] + ", Nhiệt độ: " + cs.First().Mach_NDo_HAp.Split(';')[1] + ", Huyết áp: " + cs.First().Mach_NDo_HAp.Split(';')[2] + ", Nhịp thở: " + cs.First().Mach_NDo_HAp.Split(';')[3] + "\r\n" + "Cân nặng: " + cs.First().CanNang_ChieuCao.Split(';')[0] + ", Chiều cao: " + cs.First().CanNang_ChieuCao.Split(';')[1];
                            
                        }
                        if (!db.DienBien1.Contains(CSCT))
                            db.DienBien1 = db.DienBien1 + CSCT + Environment.NewLine;
                    }
                    else
                    {
                        var db = DaTaContext.DienBiens.Single(p => p.ID == id);
                        db.DienBien1 = "";
                        List<MyObject> _lall = new List<MyObject>();
                        List<MyObject> qcd = (from cd in DaTaContext.ChiDinhs
                                              join cls in DaTaContext.CLS.Where(o => o.MaBNhan == _int_maBN) on cd.IdCLS equals cls.IdCLS
                                              join clsctNotNull in DaTaContext.CLScts on cd.IDCD equals clsctNotNull.IDCD into kq
                                              from clsct in kq.DefaultIfEmpty()
                                              join dv in DaTaContext.DichVus on cd.MaDV equals dv.MaDV
                                              join dvctNotNull in DaTaContext.DichVucts on clsct.MaDVct equals dvctNotNull.MaDVct into kq1
                                              from dvct in kq1.DefaultIfEmpty()
                                              join tn in DaTaContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                              join n in DaTaContext.NhomDVs on tn.IDNhom equals n.IDNhom
                                              select new MyObject
                                              {
                                                  IDCD = cd.IDCD,
                                                  IDNhom = tn.IDNhom ?? 0,
                                                  TenNhom = n.TenNhom,
                                                  TenTN = tn.TenRG,
                                                  TenRG = dv.TenDV,
                                                  TenDVct = dvct != null ? dvct.TenDVct : "",
                                                  KetQua = clsct != null ? (DungChung.Bien.MaBV == "30009" ? (clsct.KetQua + " " + dvct.DonVi) : clsct.KetQua) : clsct.KetQua,
                                                  KetLuan = cd.KetLuan,
                                                  MoTa = cd.MoTa,
                                                  NgayTH = cls.NgayTH,
                                              }).ToList();
                        _lall.AddRange(qcd);

                        _lall = _lall.OrderBy(p => p.NgayTH).ThenBy(p => p.TenTN).ThenBy(p => p.TenRG).ToList();

                        var q1 = (from dv in _lall
                                  group dv by new { dv.TenNhom, dv.IDNhom } into x
                                  select new
                                  {
                                      x.Key.IDNhom,
                                      TenNhom = x.Key.TenNhom,
                                      TenRG = "- " + x.Key.TenNhom + ": " + string.Join(", ", x.Select(p => p.TenRG).ToList().Distinct()) + Environment.NewLine,
                                      CDHA = x.Key.IDNhom == 2 ? /*batdau*/ string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).Select(p => ("- " + p.TenRG + (!string.IsNullOrWhiteSpace(p.KetQua) && DungChung.Bien.MaBV != "27001" ? (": " + DungChung.Bien.MaBV == "27001" ? (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "") : p.KetQua + (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "")) : (!string.IsNullOrWhiteSpace(p.KetLuan) ? (": " + p.KetLuan) : ""))))) : "",
                                      XN = x.Key.IDNhom == 1 ? (string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG, }).Select(p => (("- " + (p.Count() == 1 ? (p.FirstOrDefault().TenDVct + " " + p.FirstOrDefault().KetQua) : (p.Key.TenRG + (p.Count() > 0 ? ": " : "") + string.Join(", ", p.Select(o => (o.TenDVct + (!string.IsNullOrWhiteSpace(o.KetQua) ? (" " + o.KetQua) : ""))))))))))) : "",
                                      PTTT = x.Key.IDNhom == 8 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).Select(p => ("- " + p.TenRG + (!string.IsNullOrWhiteSpace(p.KetQua) ? (": " + p.KetQua + (!string.IsNullOrWhiteSpace(p.KetLuan) ? "," : "")) : (!string.IsNullOrWhiteSpace(p.KetLuan) ? (": " + p.KetLuan) : ""))))) : "",
                                      TDCN = x.Key.IDNhom == 3 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG }).Select(p => (("- " + (p.Count() == 1 ? (p.FirstOrDefault().TenDVct + " " + p.FirstOrDefault().KetQua) : (p.Key.TenRG + (p.Count() > 0 ? ": " : "") + string.Join(", ", p.Select(o => (o.TenDVct + (!string.IsNullOrWhiteSpace(o.KetQua) ? (" " + o.KetQua) : "")))))))))) : "",// his 120 11/05/2021
                                      TDCN27001 = x.Key.IDNhom == 3 ? string.Join(Environment.NewLine, x.Where(o => o.NgayTH != null).GroupBy(o => new { o.IDCD, o.TenRG }).Select(p => (("\t- " + (p.Count() == 1 ? (p.FirstOrDefault().TenRG + ": " + p.FirstOrDefault().KetLuan) : (string.Join(", ", p.Select(o => (o.TenRG + ": " + (!string.IsNullOrWhiteSpace(o.KetLuan) ? (" " + o.KetLuan) : ""))).Distinct()))))))) : "",// his-327 27/07/2021
                                  }).ToList();

                        string dsCLS = q1.Count > 0 ? ("Chỉ định CLS:" + Environment.NewLine + string.Join("", q1.Select(p => p.TenRG))) : "";

                        string klCDHA = (q1.FirstOrDefault(o => o.IDNhom == 2) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 2).CDHA)) ? ("CĐHA:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 2).CDHA) : "";
                        string klXN = (q1.FirstOrDefault(o => o.IDNhom == 1) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 1).XN)) ? ("XN:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 1).XN) : "";
                        string klPTTT = (q1.FirstOrDefault(o => o.IDNhom == 8) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 8).PTTT)) ? ("THỦ THUẬT PHẪU THUẬT:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 8).PTTT) : "";
                        string klTDCN = (q1.FirstOrDefault(o => o.IDNhom == 3) != null && !string.IsNullOrWhiteSpace(q1.FirstOrDefault(o => o.IDNhom == 3).TDCN)) ? (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "30007" ? (("THĂM DÒ CHỨC NĂNG:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 3).TDCN27001)) : ("THĂM DÒ CHỨC NĂNG:" + Environment.NewLine + q1.FirstOrDefault(o => o.IDNhom == 3).TDCN)) : ""; // his-327 27/07/2021
                        var cs = (from ttbx in DaTaContext.TTboXungs.Where(p => p.MaBNhan == _int_maBN)
                                  select new
                                  {
                                      ttbx.Mach_NDo_HAp,
                                      ttbx.CanNang_ChieuCao,
                                  }).ToList();
                        string CSCT = "";
                        if (cs != null && !string.IsNullOrEmpty(cs.First().Mach_NDo_HAp) && !string.IsNullOrEmpty(cs.First().CanNang_ChieuCao))
                        {
                            CSCT = "CHỈ SỐ CƠ THỂ:" + Environment.NewLine + "Mạch: " + cs.First().Mach_NDo_HAp.Split(';')[0] + ", Nhiệt độ: " + cs.First().Mach_NDo_HAp.Split(';')[1] + ", Huyết áp: " + cs.First().Mach_NDo_HAp.Split(';')[2] + ", Nhịp thở: " + cs.First().Mach_NDo_HAp.Split(';')[3] + "\r\n" + "Cân nặng: " + cs.First().CanNang_ChieuCao.Split(';')[0] + ", Chiều cao: " + cs.First().CanNang_ChieuCao.Split(';')[1];
                        }
                        db.DienBien1 = "BT" + Environment.NewLine + ((!string.IsNullOrWhiteSpace(klCDHA) || !string.IsNullOrWhiteSpace(klPTTT) || !string.IsNullOrWhiteSpace(klTDCN) || !string.IsNullOrWhiteSpace(klXN)) ? ("Kết quả CLS:" + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klXN) ? (klXN + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klCDHA) ? (klCDHA + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klPTTT) ? (klPTTT + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(klTDCN) ? (klTDCN + Environment.NewLine) : "") + (!string.IsNullOrWhiteSpace(CSCT) ? (CSCT + Environment.NewLine) : "");
                    }
                }
            }

            DaTaContext.SaveChanges();
            frmTaoBenhAnNgoaiTru_Load(null, null);
        }
    }
}