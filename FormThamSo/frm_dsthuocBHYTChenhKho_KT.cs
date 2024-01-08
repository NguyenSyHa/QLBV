using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_dsthuocBHYTChenhKho_KT : DevExpress.XtraEditors.XtraForm
    {
        public frm_dsthuocBHYTChenhKho_KT()
        {
            InitializeComponent();
        }
        public class ngaythangmoi
        {
            public int MaDV1 { get; set; }
            public string thang2 { get; set; }
            public string TenDV1 { get; set; }
            public string DonVi1 { get; set; }
            public double xuatvp1 { get; set; }
            public double TonDKSL1 { get; set; }
            public double NhapTKSL1 { get; set; }
            public double TonCKSL1 { get; set; }
            public double XuatNgoaiTruSL1 { get; set; }
            public double TongXuatTKSL1 { get; set; }
        }
        List<ngaythangmoi> _da = new List<ngaythangmoi>();
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_dsthuocBHYTChenhKho_KT_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now;
            lupngayden.DateTime = System.DateTime.Now;       
            var dskp = (from kp in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc)
                        select new
                        {
                            MaKP = kp.MaKP,
                            TenKP = kp.TenKP
                        }).OrderBy(p => p.TenKP).ToList();

            cklKP.DataSource = dskp;
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemValue(i) != null && Convert.ToInt32(cklKP.GetItemValue(i)) == DungChung.Bien.MaKP)
                    cklKP.SetItemChecked(i, true);
                else
                    cklKP.SetItemChecked(i, false);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bttimkiem_Click(object sender, EventArgs e)
        {
            _da.Clear();
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);

            List<KPhong> _kpChon = new List<KPhong>();
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                    _kpChon.Add(new KPhong { TenKP = cklKP.GetItemText(i), MaKP = cklKP.GetItemValue(i) == null ? 0 : Convert.ToInt32(cklKP.GetItemValue(i)) });
            }
            if (_kpChon.Count() > 0)
            {
                BaoCao.rep_dsthuocBHYTChenhKho_KT rep = new BaoCao.rep_dsthuocBHYTChenhKho_KT();
                int so = (denngay.Year - tungay.Year) * 12 + denngay.Month - tungay.Month + 1;
                int thangbd = tungay.Month; int nambd = tungay.Year; int namkt = denngay.Year;
                var qnxt2 = (from nhapd in data.NhapDs
                                join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                where ((nhapd.PLoai == 1) || (nhapd.PLoai == 2))
                                select new { nhapd.TraDuoc_KieuDon, nhapd.XuatTD, nhapd.MaKP, nhapd.MaKPnx, nhapdct.MaDV, nhapdct.IDDTBN, nhapd.PLoai, nhapd.KieuDon, nhapd.NgayNhap, nhapdct.DonGia, nhapdct.SoLuongN, nhapdct.SoLuongX, nhapdct.ThanhTienN, nhapdct.ThanhTienX }).OrderByDescending(p => p.NgayNhap).ToList();
                var dichvu = (from dv in data.DichVus.Where(p => p.IDNhom == 4)
                                select new { dv.MaDV, TenDV = (dv.TenDV + " " + dv.HamLuong), dv.DonVi }).ToList();
                var vienphi = (from a in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                join b in data.VienPhicts.Where(p => p.ThanhToan == 0) on a.idVPhi equals b.idVPhi
                                join c in data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == false) on a.MaBNhan equals c.MaBNhan
                                select new { a.NgayTT, b.MaDV, b.SoLuong, a.MaKP }).ToList();
                //DateTime test = System.DateTime.Now;
                var tesst = (from a in qnxt2
                                join b in _kpChon on a.MaKP equals b.MaKP
                                group a by new { a.MaDV } into k1
                                select new
                                {
                                    k1.Key.MaDV,
                                    TonDKSL = k1.Where(p => p.NgayNhap <= tungay).Sum(p => p.SoLuongN) - k1.Where(p => p.NgayNhap <= tungay).Sum(p => p.SoLuongX),
                                    TonCKSL = k1.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN) - k1.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                }).ToList();
                DateTime tu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                DateTime den = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                for (int i = 0; i < so; i++)
                {
                    if (nambd <= namkt)
                    {
                        if (thangbd <= 12)
                        {
                            if (GetFirstDayOfMonth(nambd, thangbd).Date <= tungay.Date)
                            {
                                tu = DungChung.Ham.NgayTu(tungay);
                            }
                            else
                            {
                                tu = DungChung.Ham.NgayTu(GetFirstDayOfMonth(nambd, thangbd).Date);
                            }
                            if (GetLastDayOfMonth(nambd, thangbd).Date >= denngay.Date)
                            {
                                den = DungChung.Ham.NgayDen(denngay);
                            }
                            else
                            {
                                den = DungChung.Ham.NgayDen(GetLastDayOfMonth(nambd, thangbd));
                            }
                            var qnxt3 = (from a in qnxt2.Where(p => p.NgayNhap <= den)
                                            join c in dichvu on a.MaDV equals c.MaDV
                                            join b in _kpChon on a.MaKP equals b.MaKP 
                                            group a by new { a.MaDV, c.TenDV, c.DonVi } into kq
                                            select new
                                            {
                                                MaDV = kq.Key.MaDV,
                                                kq.Key.TenDV,
                                                kq.Key.DonVi,
                                                NgayTT = "Tháng " + thangbd + "/" + nambd,
                                                xuatvp = 0.0,
                                                TonDKSL = kq.Where(p => p.NgayNhap <= tu).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= tu).Sum(p => p.SoLuongX),

                                                NhapTKSL = kq.Where(p => p.NgayNhap >= tu).Where(p => p.NgayNhap <= den && p.PLoai == 1).Sum(p => p.SoLuongN),

                                                TonCKSL = kq.Where(p => p.NgayNhap <= den).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= den).Sum(p => p.SoLuongX),

                                                XuatNgoaiTruSL = (kq.Where(p => p.KieuDon == 0 && p.PLoai == 2).Where(p => p.NgayNhap >= tu).Where(p => p.NgayNhap <= den).Sum(p => p.SoLuongX)),

                                                TongXuatTKSL = kq.Where(p => p.NgayNhap >= tu).Where(p => p.NgayNhap.Value.Date.Month <= denngay.Month && p.PLoai == 2 && p.KieuDon != 0).Where(p => p.NgayNhap >= tungay).Sum(p => p.SoLuongX),

                                            }).OrderByDescending(p => p.NgayTT).ToList();

                            foreach (var item in qnxt3)
                            {
                                _da.Add(new ngaythangmoi { MaDV1 = item.MaDV ?? -1, TenDV1 = item.TenDV, DonVi1 = item.DonVi, thang2 = item.NgayTT, xuatvp1 = item.xuatvp, TonDKSL1 = item.TonDKSL, NhapTKSL1 = item.NhapTKSL, TonCKSL1 = item.TonCKSL, XuatNgoaiTruSL1 = item.XuatNgoaiTruSL, TongXuatTKSL1 = item.TongXuatTKSL });
                            }

                            var qnxt1 = (from b in vienphi
                                            join c in dichvu on b.MaDV equals c.MaDV
                                            group new { b } by new { b.MaDV, c.TenDV, c.DonVi } into kq
                                            select new
                                            {
                                                MaDV = kq.Key.MaDV,
                                                kq.Key.TenDV,
                                                kq.Key.DonVi,
                                                NgayTT = "Tháng " + thangbd + "/" + nambd,
                                                xuatvp = kq.Where(p => p.b.NgayTT >= tu && p.b.NgayTT <= den).Sum(p => p.b.SoLuong),
                                                TonDKSL = 0.0,
                                                NhapTKSL = 0.0,
                                                TonCKSL = 0.0,
                                                XuatNgoaiTruSL = 0.0,
                                                TongXuatTKSL = 0.0,
                                            }).ToList();

                            foreach (var item in qnxt1)
                            {
                                _da.Add(new ngaythangmoi { MaDV1 = item.MaDV ?? -1, TenDV1 = item.TenDV, DonVi1 = item.DonVi, thang2 = item.NgayTT, xuatvp1 = item.xuatvp, TonDKSL1 = item.TonDKSL, NhapTKSL1 = item.NhapTKSL, TonCKSL1 = item.TonCKSL, XuatNgoaiTruSL1 = item.XuatNgoaiTruSL, TongXuatTKSL1 = item.TongXuatTKSL });
                            }
                            thangbd += 1;
                        }
                        else if (thangbd > 12)
                        {
                            thangbd = 1;
                            nambd += 1;
                            if (GetFirstDayOfMonth(nambd, thangbd).Date <= tungay.Date)
                            {
                                tu = DungChung.Ham.NgayTu(tungay);
                            }
                            else
                            {
                                tu = DungChung.Ham.NgayTu(GetFirstDayOfMonth(nambd, thangbd).Date);
                            }
                            if (GetLastDayOfMonth(nambd, thangbd).Date >= denngay.Date)
                            {
                                den = DungChung.Ham.NgayDen(denngay);
                            }
                            else
                            {
                                den = DungChung.Ham.NgayDen(GetLastDayOfMonth(nambd, thangbd));
                            }
                            var qnxt3 = (from a in qnxt2.Where(p => p.NgayNhap <= den)
                                            join c in dichvu on a.MaDV equals c.MaDV
                                            join b in _kpChon on a.MaKP equals b.MaKP
                                            group a by new { a.MaDV, c.TenDV, c.DonVi } into kq
                                            select new
                                            {
                                                MaDV = kq.Key.MaDV,
                                                kq.Key.TenDV,
                                                kq.Key.DonVi,
                                                NgayTT = "Tháng " + thangbd + "/" + nambd,
                                                xuatvp = 0.0,
                                                TonDKSL = kq.Where(p => p.NgayNhap <= tu).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= tu).Sum(p => p.SoLuongX),

                                                NhapTKSL = kq.Where(p => p.NgayNhap >= tu).Where(p => p.NgayNhap <= den && p.PLoai == 1).Sum(p => p.SoLuongN),

                                                TonCKSL = kq.Where(p => p.NgayNhap <= den).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= den).Sum(p => p.SoLuongX),

                                                XuatNgoaiTruSL = (kq.Where(p => p.KieuDon == 0 && p.PLoai == 2).Where(p => p.NgayNhap >= tu).Where(p => p.NgayNhap <= den).Sum(p => p.SoLuongX)),

                                                TongXuatTKSL = kq.Where(p => p.NgayNhap >= tu).Where(p => p.NgayNhap.Value.Date.Month <= denngay.Month && p.PLoai == 2 && p.KieuDon != 0).Where(p => p.NgayNhap >= tungay).Sum(p => p.SoLuongX),

                                            }).OrderByDescending(p => p.NgayTT).ToList();

                            foreach (var item in qnxt3)
                            {
                                _da.Add(new ngaythangmoi { MaDV1 = item.MaDV ?? -1, TenDV1 = item.TenDV, DonVi1 = item.DonVi, thang2 = item.NgayTT, xuatvp1 = item.xuatvp, TonDKSL1 = item.TonDKSL, NhapTKSL1 = item.NhapTKSL, TonCKSL1 = item.TonCKSL, XuatNgoaiTruSL1 = item.XuatNgoaiTruSL, TongXuatTKSL1 = item.TongXuatTKSL });
                            }

                            var qnxt1 = (from b in vienphi
                                            join c in dichvu on b.MaDV equals c.MaDV
                                            group new { b } by new { b.MaDV, c.TenDV, c.DonVi } into kq
                                            select new
                                            {
                                                MaDV = kq.Key.MaDV,
                                                kq.Key.TenDV,
                                                kq.Key.DonVi,
                                                NgayTT = "Tháng " + thangbd + "/" + nambd,
                                                xuatvp = kq.Where(p => p.b.NgayTT >= tu && p.b.NgayTT <= den).Sum(p => p.b.SoLuong),
                                                TonDKSL = 0.0,
                                                NhapTKSL = 0.0,
                                                TonCKSL = 0.0,
                                                XuatNgoaiTruSL = 0.0,
                                                TongXuatTKSL = 0.0,
                                            }).ToList();

                            foreach (var item in qnxt1)
                            {
                                _da.Add(new ngaythangmoi { MaDV1 = item.MaDV ?? -1, TenDV1 = item.TenDV, DonVi1 = item.DonVi, thang2 = item.NgayTT, xuatvp1 = item.xuatvp, TonDKSL1 = item.TonDKSL, NhapTKSL1 = item.NhapTKSL, TonCKSL1 = item.TonCKSL, XuatNgoaiTruSL1 = item.XuatNgoaiTruSL, TongXuatTKSL1 = item.TongXuatTKSL });
                            }
                        }
                    }
                }

                var qnxt4 = (from a in _da
                                group new { a } by new { a.MaDV1, a.thang2, a.TenDV1, a.DonVi1 } into kq
                                select new
                                {
                                    kq.Key.MaDV1,
                                    kq.Key.TenDV1,
                                    kq.Key.DonVi1,
                                    kq.Key.thang2,
                                    xuatvp1 = kq.Sum(p => p.a.xuatvp1),
                                    TonDKSL1 = kq.Sum(p => p.a.TonDKSL1),
                                    NhapTKSL1 = kq.Sum(p => p.a.NhapTKSL1),
                                    TonCKSL1 = kq.Sum(p => p.a.TonCKSL1),
                                    XuatNgoaiTruSL1 = kq.Sum(p => p.a.XuatNgoaiTruSL1),
                                    sosanh = kq.Sum(p => p.a.xuatvp1) - kq.Sum(p => p.a.XuatNgoaiTruSL1)
                                }).Where(p => p.xuatvp1 != 0 || p.XuatNgoaiTruSL1 != 0 || p.NhapTKSL1 != 0).OrderBy(p => p.thang2).ToList();
                if (check.Checked == true)
                {
                    qnxt4 = qnxt4.Where(p => p.sosanh != 0).ToList();
                }
                rep.DataSource = qnxt4.ToList();
                rep.BindingData();
                rep.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn kho");

            }
        }

        //lấy ngày đầu tháng từ 1 ngày trong tháng
        private static DateTime tungay1(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        //lấy ngày cuối tháng từ 1 ngày trong tháng
        private static DateTime denngay1(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
        //lấy ngày đầu tháng của 1 tháng truyền vào
        public static DateTime GetFirstDayOfMonth(int Year, int iMonth)
        {
            DateTime dtResult = new DateTime(Year, iMonth, 1);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        // lấy ngày cuối tháng của 1 tháng truyền vào
        public static DateTime GetLastDayOfMonth(int Year, int iMonth)
        {
            DateTime dtResult1 = new DateTime(Year, iMonth, 1);
            dtResult1 = dtResult1.AddMonths(1);
            dtResult1 = dtResult1.AddDays(-(dtResult1.Day));
            return DungChung.Ham.NgayDen(dtResult1);
        }
    }
}