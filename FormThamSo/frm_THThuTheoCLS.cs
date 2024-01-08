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
using QLBV.BaoCao;

namespace QLBV.FormThamSo
{
    public partial class frm_THThuTheoCLS : DevExpress.XtraEditors.XtraForm
    {
        public frm_THThuTheoCLS()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_THThuTheoCLS_Load(object sender, EventArgs e)
        {
            dtpTuNgay.DateTime = DungChung.Ham.NgayTu(DateTime.Now);
            dtpDenNgay.DateTime = DungChung.Ham.NgayDen(DateTime.Now);

            KhoaPhong();
            grcKhoaPhong.DataSource = kp.OrderBy(p => p.makp).ToList();
        }
        class Kp
        {
            public bool chon { get; set; }
            public int makp { get; set; }
            public string tenkp { get; set; }
        }
        List<Kp> kp = new List<Kp>();
        private void KhoaPhong()
        {

            Kp k = new Kp();
            k.chon = true;
            k.makp = 0;
            k.tenkp = "Tất cả";
            kp.Add(k);
            var k1 = (from kp1 in data.KPhongs.Where(p => p.PLoai.Contains("Cận lâm sàng") || p.PLoai.Contains("Phòng khám"))
                      select new { kp1.MaKP, kp1.TenKP }).ToList();
            foreach (var item in k1)
            {
                Kp k2 = new Kp();
                k2.chon = true;
                k2.makp = item.MaKP;
                k2.tenkp = item.TenKP;
                kp.Add(k2);
            }
        }

        private void grvKhoaPhong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == grvChon)
            {
                if (grvKhoaPhong.GetFocusedRowCellValue(grvChon) != null)
                {
                    string Ten = grvKhoaPhong.GetFocusedRowCellValue(grvTenKP).ToString();

                    if (Ten == "Tất cả")
                    {
                        if (kp.First().chon == true)
                        {
                            foreach (var a in kp)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in kp)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaPhong.DataSource = "";
                        grcKhoaPhong.DataSource = kp.ToList();
                    }
                }
            }
        }

        class repbc
        {
            public string Tencbcd { get; set; }
            public string Tencbth { get; set; }
            public string TenBN { get; set; }
            public string Tuoi { get; set; }
            public string NoiGui { get; set; }
            public int MaChiTieu { get; set; }
            public string TenChiTieu { get; set; }
            public string DonVi { get; set; }
            public double soluong { get; set; }
            public double TongTien { get; set; }
            public double Giam { get; set; }
            public double ThucThu { get; set; }
            public string SoHD { get; set; }

            public string DoiTuong { get; set; }
            public string KPth { get; set; }
            public string Tendv { get; set; }

            public double SumSoluong { get; set; }
            public double SumTongTien { get; set; }
            public double SumGiam { get; set; }
            public double SumTThucThu { get; set; }

            public double SumTSoluong { get; set; }
            public double SumTTongTien { get; set; }
            public double SumTGiam { get; set; }
            public double SumTTThucThu { get; set; }
        }

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            DungChung.Ham.CallProcessWaitingForm(TaoBaoCao, "Đang tạo báo cáo", "Xin chờ trong giây lát");
        }

        private void TaoBaoCao()
        {
            #region 30372
            if (DungChung.Bien.MaBV == "30372")
            {
               var kq = (from bn in data.BenhNhans
                      join tu in data.TamUngs.Where(p => p.NgayThu >= dtpTuNgay.DateTime && p.NgayThu <= dtpDenNgay.DateTime || p.IDTamUng == null) on bn.MaBNhan equals tu.MaBNhan
                      join tuct in data.TamUngcts.Where(p => p.Status == 0) on tu.IDTamUng equals tuct.IDTamUng
                      join cls in data.CLS on tu.MaBNhan equals cls.MaBNhan
                      join dv in data.DichVus on tuct.MaDV equals dv.MaDV
                      where dv.IDNhom == 1 || dv.IDNhom == 2 || dv.IDNhom == 3 || dv.IDNhom == 8
                      select new
                      {
                          bn.MaBNhan,
                          bn.TenBNhan,
                          bn.Tuoi,
                          bn.DTuong,
                          tuct.MaDV,
                          dv.TenDV,
                          tu.SoHD,
                          tuct.MaKP,
                          tuct.MaKPth,
                          dv.DonVi,
                          tuct.Mien,
                          tuct.ThanhTien,
                          tuct.SoLuong,
                          cls.IdCLS,
                          cls.MaCB,
                          cls.MaCBth,
                      }).ToList();

                var c = (from c1 in kq
                         join cd in data.ChiDinhs on c1.IdCLS equals cd.IdCLS
                         where cd.MaDV == c1.MaDV
                         select new
                         {
                             c1.MaBNhan,
                             c1.TenBNhan,
                             c1.Tuoi,
                             c1.DTuong,
                             c1.MaDV,
                             c1.TenDV,
                             c1.SoHD,
                             c1.MaKP,
                             c1.MaKPth,
                             c1.DonVi,
                             c1.Mien,
                             c1.ThanhTien,
                             c1.SoLuong,
                             c1.MaCB,
                             c1.MaCBth,
                         }).Distinct().ToList();
                var cb1 = (from c2 in c
                           join cb in data.CanBoes on c2.MaCB equals cb.MaCB
                           select new
                           {
                               c2.MaBNhan,
                               c2.MaDV,
                               cb.TenCB,
                           }).ToList();
                var cb2 = (from c2 in c
                           join cb in data.CanBoes on c2.MaCBth equals cb.MaCB
                           select new
                           {
                               c2.MaBNhan,
                               c2.MaDV,
                               cb.TenCB,
                           }).ToList();
                var kqq = (from k in c
                           join kpcd in data.KPhongs on k.MaKP equals kpcd.MaKP
                           join kpth in kp.Where(p => p.chon == true) on k.MaKPth equals kpth.makp
                           select new
                           {
                               k.MaBNhan,
                               TenBN = k.TenBNhan,
                               Tuoi = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01047") ? DungChung.Ham.TuoitheoThang(data, k.MaBNhan, "12-30") : k.Tuoi.ToString(),
                               DoiTuong = "Đối tượng: " + k.DTuong,
                               MaChiTieu = k.MaDV,
                               DonVi = k.DonVi,
                               TenChiTieu = k.TenDV,
                               SoHD = k.SoHD,
                               soluong = k.SoLuong,
                               TongTien = k.ThanhTien,
                               k.Mien,
                               Giam = (double)k.Mien * k.ThanhTien * 0.01,
                               KPth = "Phòng CLS: " + kpth.tenkp,
                               Tendv = "Dịch vụ: " + k.TenDV,
                               ThucThu = k.ThanhTien - ((double)k.Mien * k.ThanhTien * 0.01),
                               NoiGui = kpcd.TenKP,
                               Tencbth = cb2.Where(p => p.MaDV == k.MaDV && p.MaBNhan == k.MaBNhan).Select(p => p.TenCB).FirstOrDefault(),
                               Tencbcd = cb1.Where(p => p.MaDV == k.MaDV && p.MaBNhan == k.MaBNhan).Select(p => p.TenCB).FirstOrDefault(),
                           }).ToList();
                if (kqq.Count > 0)
                {
                    frmIn frm = new frmIn();
                    Rep_bcTongHopThuTheoTungPhongCLS rep = new Rep_bcTongHopThuTheoTungPhongCLS();
                    rep.DataSource = kqq.ToList();
                    rep.colSumSoLuong.Text = kqq.Sum(p => p.soluong).ToString("#,##");
                    rep.SumGiam.Text = kqq.Sum(p => p.Giam).ToString("#,##");
                    rep.SumTongTien.Text = kqq.Sum(p => p.TongTien).ToString("#,##");
                    rep.colSumThucThu.Text = kqq.Sum(p => p.ThucThu).ToString("#,##");
                    rep.Bindding();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    XtraMessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            #endregion
            #region BV Khác
            else
            {
                var kq = (from bn in data.BenhNhans
                          join tu in data.TamUngs.Where(p => p.NgayThu >= dtpTuNgay.DateTime && p.NgayThu <= dtpDenNgay.DateTime || p.IDTamUng == null) on bn.MaBNhan equals tu.MaBNhan
                          join tuct in data.TamUngcts.Where(p => p.Status == 0) on tu.IDTamUng equals tuct.IDTamUng
                          join cls in data.CLS on tu.MaBNhan equals cls.MaBNhan
                          join dv in data.DichVus on tuct.MaDV equals dv.MaDV
                          select new
                          {
                              bn.MaBNhan,
                              bn.TenBNhan,
                              bn.Tuoi,
                              bn.DTuong,
                              tuct.MaDV,
                              dv.TenDV,
                              tu.SoHD,
                              tuct.MaKP,
                              tuct.MaKPth,
                              dv.DonVi,
                              tuct.Mien,
                              tuct.ThanhTien,
                              tuct.SoLuong,
                              cls.IdCLS,
                              cls.MaCB,
                              cls.MaCBth,
                          }).ToList();
                var kqq = (from k in kq
                           join kpcd in data.KPhongs on k.MaKP equals kpcd.MaKP
                           join kpth in kp.Where(p => p.chon == true) on k.MaKPth equals kpth.makp
                           select new
                           {
                               k.MaBNhan,
                               k.TenBNhan,
                               k.Tuoi,
                               k.DTuong,
                               k.MaDV,
                               k.DonVi,
                               k.TenDV,
                               k.SoHD,
                               k.SoLuong,
                               k.ThanhTien,
                               k.Mien,
                               TenKPth = kpth.tenkp,
                               TenKPcd = kpcd.TenKP,
                           }).ToList();

                List<repbc> kqs = new List<repbc>();
                foreach (var item in kqq)
                {
                    int madv = 0;
                    int mabn = 0;
                    repbc rep = new repbc();
                    madv = item.MaDV;
                    mabn = item.MaBNhan;
                    rep.TenBN = item.TenBNhan;
                    rep.Tuoi = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01047") ? DungChung.Ham.TuoitheoThang(data, item.MaBNhan, "12-30") : item.Tuoi.ToString();
                    rep.NoiGui = item.TenKPcd;
                    rep.DoiTuong = "Đối tượng: " + item.DTuong;
                    rep.KPth = "Phòng CLS: " + item.TenKPth;
                    rep.MaChiTieu = item.MaDV;
                    rep.TenChiTieu = item.TenDV;
                    rep.DonVi = item.DonVi;
                    rep.soluong = item.SoLuong;
                    rep.TongTien = item.ThanhTien;
                    rep.Giam = (double)item.Mien * item.ThanhTien * 0.01;
                    rep.Tendv = "Dịch vụ: " + item.TenDV;
                    rep.ThucThu = rep.TongTien - rep.Giam;
                    rep.SoHD = item.SoHD.ToString();
                    //var kq1 = (from k in kq
                    //           join cb in data.CanBoes on k.MaCB equals cb.MaCB
                    //           select new
                    //           {
                    //               k.MaDV,
                    //               cb.TenCB,
                    //           }).Where(p => p.MaDV == madv).ToList();
                    //var kq2 = (from k in kq
                    //           join cb in data.CanBoes on k.MaCBth equals cb.MaCB
                    //           select new
                    //           {
                    //               k.MaDV,
                    //               cb.TenCB,
                    //           }).Where(p => p.MaDV == madv).ToList();
                    var kq1 = (from cd in data.ChiDinhs
                               join cls in data.CLS on cd.IdCLS equals cls.IdCLS
                               join cb in data.CanBoes on cls.MaCB equals cb.MaCB
                               where cd.MaDV == madv && cls.MaBNhan == mabn
                               select new
                               {
                                   cb.TenCB,
                               }).ToList();
                    var kq2 = (from cd in data.ChiDinhs
                               join cls in data.CLS on cd.IdCLS equals cls.IdCLS
                               join cb in data.CanBoes on cls.MaCBth equals cb.MaCB
                               where cd.MaDV == madv && cls.MaBNhan == mabn
                               select new
                               {
                                   cb.TenCB,
                               }).ToList();
                    if (kq2.Count() > 0)
                    {
                        rep.Tencbth = kq2.First().TenCB == null ? "" : kq2.First().TenCB;
                    }
                    if (kq1.Count() > 0)
                    {
                        rep.Tencbcd = kq1.First().TenCB == null ? "" : kq1.First().TenCB;
                    }
                    kqs.Add(rep);
                }
                if (kqs.Count > 0)
                {
                    frmIn frm = new frmIn();
                    Rep_bcTongHopThuTheoTungPhongCLS rep = new Rep_bcTongHopThuTheoTungPhongCLS();
                    rep.DataSource = kqs.ToList();
                    rep.colSumSoLuong.Text = kqs.Sum(p => p.soluong).ToString("#,##");
                    rep.SumGiam.Text = kqs.Sum(p => p.Giam).ToString("#,##");
                    rep.SumTongTien.Text = kqs.Sum(p => p.TongTien).ToString("#,##");
                    rep.colSumThucThu.Text = kqs.Sum(p => p.ThucThu).ToString("#,##");
                    rep.Bindding();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    XtraMessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            #endregion
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}