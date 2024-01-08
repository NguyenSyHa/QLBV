using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Collections;


namespace QLBV.FormThamSo
{
    public partial class Frm_BangTheoDoiBNBHYT : Form
    {
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public Frm_BangTheoDoiBNBHYT()
        {
            InitializeComponent();
        }

        private void Frm_BangTheoDoiBNBHYT_Load(object sender, EventArgs e)
        {
            tungay.DateTime = DateTime.Now;
            denngay.DateTime = DateTime.Now;
            if (denngay.DateTime > DateTime.Now)
            {
                denngay.DateTime = DateTime.Now;
            }
        }
        public class TDBN
        {
            public string Ngay_Thong_Ke { get; set; }
            public int Count_BN1 { get; set; }
            public int Count_BN2 { get; set; }
            public int Count_NgoaiTru { get; set; }
            public int Count_NoiTru { get; set; }
            public int Count_BN_RaVien { get; set; }
            public int Count_NoiTru_TaiThoiDiemBaoCao { get; set; }
            public int Count_NgoaiTru_DYCK { get; set; }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //tinh tu 6h den 17h cung ngay
            DateTime a = DungChung.Ham.NgayTu(tungay.DateTime).AddHours(6);
            DateTime b = DungChung.Ham.NgayDen(denngay.DateTime).AddHours(17);
            DateTime d = DungChung.Ham.NgayTu(tungay.DateTime);


            int tong = 0;
            int days = (b - a).Days;
            List<string> list = new List<string>();
            if (days > 0)
            {
                for (int i = 0; i < days; i++)
                {
                    list.Add("" + d);
                    d = d.AddDays(1);
                }

                List<TDBN> tt = new List<TDBN>();

                if (list.Count() > 0)
                {
                    var BN_BH = _data.BenhNhans.Where(p => p.DTuong == "BHYT");
                    var BNKB = _data.BNKBs.Where(p => p.PhuongAn == 4);
                    var RaVien = _data.RaViens;
                    foreach (var item in list)
                    {
                        DateTime cuoingay = DateTime.Parse(item).AddHours(-7);
                        DateTime daungay = DateTime.Parse(item).AddHours(6);
                        DateTime homnay_17h = DateTime.Parse(item).AddHours(17);

                        DateTime daungay1 = daungay.AddDays(-1);

                        int Tong_BN_Dang_DieuTri = 0;
                        int Tong_BN_RaVien_BanDem = 0;
                        //minhvd
                        int Tong_BN_Dang_TrongKhoa_6hSang = 0;
                        int Tong_BN_RaVien_TrongKhoang_6h_17h = 0;
                        int Tong_BN_TrongVien_17h_NgoaiTru = 0;
                        int Tong_BN_TrongVien_17h_NoiTru = 0;
                        int Tong_BN_NoiTru_TaiThoiDiemBaoCao = 0;
                        int Tong_BN_NgoaiTru_DYCK = 0;


                        #region cot 1
                        var BN_DangDieuTri = BN_BH.Where(p => p.NNhap <= cuoingay && p.Status != 2 && p.Status != 3 && p.NoiTru == 1).Count();
                        var BN_RaVien_BanDem = (from bn in BN_BH.Where(p => p.NNhap <= cuoingay && p.NoiTru == 1)
                                                join rv in RaVien.Where(p => p.NgayRa > cuoingay) on bn.MaBNhan equals rv.MaBNhan
                                                select new
                                                {
                                                    bn.MaBNhan
                                                }
                                           ).Count();
                        #endregion

                        #region cot 2
                        var BN_DangDieuTri_Truoc6h = BN_BH.Where(p => p.NNhap <= daungay && p.Status != 2 && p.Status != 3 && p.NoiTru == 1).Count();
                        var BN_RaVien_Sau6h = (from bn in BN_BH.Where(p => p.NNhap < daungay && p.NoiTru == 1)
                                               join rv in RaVien.Where(p => p.NgayRa > daungay) on bn.MaBNhan equals rv.MaBNhan
                                               select new
                                               {
                                                   bn.MaBNhan
                                               }
                                           ).Count();
                        Tong_BN_Dang_TrongKhoa_6hSang = BN_DangDieuTri_Truoc6h + BN_RaVien_Sau6h;
                        #endregion

                        #region cot 3
                        var BN_NgoaiTru = BN_BH.Where(p => p.NNhap > daungay && p.NNhap < homnay_17h && p.NoiTru == 0).Count();
                        #endregion

                        #region cot 4
                        var BN_NoiTru = BN_BH.Where(p => p.NNhap > daungay && p.NNhap < homnay_17h && p.NoiTru == 1).Count();
                        #endregion
                        #region cot 5
                        //var BN_RV_TrongNgay = RaVien.Where(p => p.NgayRa > daungay && p.NgayRa < homnay_17h).Count();
                        var BN_RV_TrongNgay = (from rv in RaVien.Where(p => p.NgayRa > daungay && p.NgayRa < homnay_17h)
                                               join bn in BN_BH.Where(p => p.NoiTru == 1) on rv.MaBNhan equals bn.MaBNhan
                                              select new
                                              {
                                                  bn.MaBNhan
                                              }).Count();
                        #endregion
                        #region cot 6
                        Tong_BN_NoiTru_TaiThoiDiemBaoCao = Tong_BN_Dang_TrongKhoa_6hSang + BN_NoiTru - BN_RV_TrongNgay;
                        #endregion
                        TDBN tonghop = new TDBN();
                        tonghop.Ngay_Thong_Ke = DungChung.Ham.NgaySangChu(Convert.ToDateTime(item), 7);
                        tonghop.Count_BN1 = BN_DangDieuTri + BN_RaVien_BanDem;
                        tonghop.Count_BN2 = Tong_BN_Dang_TrongKhoa_6hSang;
                        tonghop.Count_BN_RaVien = BN_RV_TrongNgay;
                        tonghop.Count_NoiTru = BN_NoiTru;
                        tonghop.Count_NgoaiTru = BN_NgoaiTru;
                        tonghop.Count_NoiTru_TaiThoiDiemBaoCao = Tong_BN_NoiTru_TaiThoiDiemBaoCao;
                        tonghop.Count_NgoaiTru_DYCK = Tong_BN_NgoaiTru_DYCK;
                        tong = Tong_BN_NoiTru_TaiThoiDiemBaoCao;
                        tt.Add(tonghop);

                    }

                    frmIn frm = new frmIn();
                    BaoCao.Rep_BaoCaoBenhNhanBHYT rep1 = new BaoCao.Rep_BaoCaoBenhNhanBHYT();

                    //foreach (var item in tt)
                    //{
                    //    rep1.NgayThangNam.Value = item.Ngay_Thong_Ke;
                    //    rep1.BN17hHomTruoc.Value = item.Count_BN1;
                    //    rep1.BN6hTrongKhoa.Value = item.Count_BN2;
                    //    rep1.NgoaiTru.Value = item.Count_NgoaiTru;
                    //    rep1.NoiTru.Value = item.Count_NoiTru;
                    //    rep1.RaVienTu6hDen17h.Value = item.Count_BN_RaVien;
                    //    rep1.TaiThoiDiem17h.Value = item.Count_NoiTru_TaiThoiDiemBaoCao;
                    //    rep1.BNDieuTriNgoaiTru.Value = item.Count_NgoaiTru_DYCK;
                    //}
                    rep1.Tong1.Value = tong;
                    rep1.DataSource = tt.ToList();
                    rep1.binhding();
                    rep1.CreateDocument();
                    frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }

        private static DateTime NgayTu1(DateTime ngaydmy)
        {
            int d = ngaydmy.Day;
            int m = ngaydmy.Month;
            int y = ngaydmy.Year;
            return Convert.ToDateTime(d.ToString() + "/" + m.ToString() + "/" + y.ToString() + "");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
