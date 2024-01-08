using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class Frm_BCHoatDongKCBToanVien_27023 : Form
    {
        public Frm_BCHoatDongKCBToanVien_27023()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void Frm_BCHoatDongKCBToanVien_27023_Load(object sender, EventArgs e)
        {
            grcKP.DataSource = _data.KPhongs
                .Where(p => (p.PLoai == ("Lâm sàng") || p.PLoai == ("Phòng khám")) || p.PLoai == ("PK khu vực"))
                .OrderBy(p => p.TenKP).ToList();
            lupNgaytu.DateTime = lupngayden.DateTime = DateTime.Now;
            grvKP.SelectAll();
        }

        public void InBC(DateTime tungay, DateTime denngay)
        {

        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = Convert.ToDateTime(lupNgaytu.EditValue).Date;
            DateTime denngay = Convert.ToDateTime(lupngayden.EditValue).AddDays(1).Date;
            DateTime tungay1 = Convert.ToDateTime(lupNgaytu.EditValue).AddYears(-1).Date;
            DateTime denngay1 = Convert.ToDateTime(lupngayden.EditValue).AddYears(-1).AddDays(1).Date;
            DateTime tungay2 = Convert.ToDateTime(lupNgaytu.EditValue).AddYears(-2).Date;
            DateTime denngay2 = Convert.ToDateTime(lupngayden.EditValue).AddYears(-2).AddDays(1).Date;
            List<KP> dsKPChon = new List<KP>();
            List<int?> dsMaKpChon = new List<int?>();
            int[] index = grvKP.GetSelectedRows();
            string dskp = "";
            if (index.Count() == grvKP.RowCount)
            {
                dskp = "Tất cả";
            }
            else
            {
                for (int i = 0; i < index.Count(); i++)
                {
                    dskp += Convert.ToString(grvKP.GetRowCellValue(index[i], colTenKP)) + " ;";
                }
            }
            for (int i = 0; i < index.Count(); i++)
            {
                KP kp = new KP();
                kp.MaKP = Convert.ToInt32(grvKP.GetRowCellValue(index[i], colMaKP));
                kp.TenKP = Convert.ToString(grvKP.GetRowCellValue(index[i], colTenKP));
                dsMaKpChon.Add(kp.MaKP);
                dsKPChon.Add(kp);
            }
            //init list
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("Title", "Báo cáo tổng hợp hoạt động KCB ");
            _dic.Add("Time", "Từ ngày " + tungay.Day + "/" + tungay.Month + "/" + tungay.Year + " đến ngày " + denngay.Day + "/" + denngay.Month + "/" + denngay.Year);
            _dic.Add("dskp", dskp);

            #region năm hiện tại
            //1111111111
            //Giuong benh
            var GiuongKH = (from kp in _data.KPhongs.Where(p => p.SoGiuongKH != null && dsMaKpChon.Contains(p.MaKP)) select new { kp.MaKP, kp.TenKP, kp.SoGiuongKH }).ToList();
            int TongGiuong = 0;
            for (int i = 0; i < GiuongKH.Count; i++)
            {
                int SoGiuongTungKhoa = tachchuoi(GiuongKH[i].SoGiuongKH, tungay.Year);
                TongGiuong += SoGiuongTungKhoa;
            }
            _dic.Add("GiuongKH", TongGiuong);
            var q2 = (from bn in _data.BenhNhans
                      join bnkb in _data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Where(p => p.Giuong != null && dsMaKpChon.Contains(p.MaKP)) on bn.MaBNhan equals bnkb.MaBNhan
                      select bnkb).ToList();

            int getGiuongTT = q2.Select(p => p.Giuong).Distinct().Count();
            _dic.Add("GiuongTT", getGiuongTT);

            double CongSuatGiuongKH = Math.Round((double)getGiuongTT / TongGiuong, 4);
            _dic.Add("CongSuatGiuongKH", CongSuatGiuongKH);

            //22222222222
            //Luot kham
            var KCB = (from a in _data.BNKBs
                       join b in _data.KPhongs
                       on a.MaKP equals b.MaKP
                       where a.NgayKham >= tungay && a.NgayKham <= denngay
                                && dsMaKpChon.Contains(a.MaKP)
                                && b.PLoai == "Phòng khám"
                       select a).ToList();

            int KBenh_Sl = KCB.Count();
            _dic.Add("KBenh_Sl", KBenh_Sl);
            //BHYT
            int KBenhBHYT_Sl = (from a in KCB
                                join b in _data.BenhNhans on a.MaBNhan equals b.MaBNhan
                                where b.DTuong == "BHYT"
                                select b).Count();
            _dic.Add("KBenhBHYT_Sl", KBenhBHYT_Sl);
            //
            //Tong so BN chuyen vien tai phong kham
            var chuyenVienPK = (from bn in _data.BenhNhans
                                join bnkb in _data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                where bn.NoiTru == 0 && bnkb.PhuongAn == 2
                                        && bn.NNhap >= tungay && bn.NNhap <= denngay
                                        && dsMaKpChon.Contains(bn.MaKP)
                                         && bn.Status == 3
                                select bn).ToList();

            int CVTaiPK_Sl = chuyenVienPK.Count();
            _dic.Add("CVTaiPK_Sl", CVTaiPK_Sl);
            //BHYT
            int CVTaiPKBHYT_Sl = chuyenVienPK.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count();
            _dic.Add("CVTaiPKBHYT_Sl", CVTaiPKBHYT_Sl);
            //
            //Tong so BN ngoai tru
            var BNNgoaiTru01 = (from bn in _data.BenhNhans
                              join vv in _data.VaoViens
                                 on bn.MaBNhan equals vv.MaBNhan
                              where vv.NgayVao >= tungay && vv.NgayVao <= denngay
                                     && bn.NoiTru == 0 
                                     && bn.DTNT == true
                                     && dsMaKpChon.Contains(bn.MaKP)
                                select bn).ToList();
            var BNNgoaiTru02 = (from bn in _data.BenhNhans
                              join vv in _data.VaoViens
                                 on bn.MaBNhan equals vv.MaBNhan
                              where vv.NgayVao < tungay
                                     && bn.NoiTru == 0
                                     && bn.DTNT == true
                                     && (bn.Status == 0 || bn.Status == 1 || bn.Status == 4 || bn.Status == 5)
                                     && dsMaKpChon.Contains(bn.MaKP)
                                select bn).ToList();

            var BNNgoaiTru03 = (from bn in _data.BenhNhans
                              join rv in _data.RaViens
                                 on bn.MaBNhan equals rv.MaBNhan
                              where rv.NgayVao < tungay && rv.NgayRa >= tungay
                                     && bn.NoiTru == 0
                                     && bn.DTNT == true
                                     && dsMaKpChon.Contains(bn.MaKP)
                                select bn).ToList();

            int BnNgoaiTru_Sl = BNNgoaiTru01.Count() + BNNgoaiTru02.Count() + BNNgoaiTru03.Count();
            _dic.Add("BnNgoaiTru_Sl", BnNgoaiTru_Sl);
            //BHYT
            int BnNgoaiTruBHYT_Sl = BNNgoaiTru01.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count()
                                    + BNNgoaiTru02.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count()
                                    + BNNgoaiTru03.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count();
            _dic.Add("BnNgoaiTruBHYT_Sl", BnNgoaiTruBHYT_Sl);
            //3333333333
            //Hoat dong dieu tri noi tru
            var BNNoiTru01 = (from bn in _data.BenhNhans
                             join vv in _data.VaoViens
                                on bn.MaBNhan equals vv.MaBNhan
                             where vv.NgayVao >= tungay && vv.NgayVao <= denngay
                                    && bn.NoiTru == 1
                                    && dsMaKpChon.Contains(bn.MaKP)
                              select bn).ToList();
            var BNNoiTru02 = (from bn in _data.BenhNhans
                             join vv in _data.VaoViens
                                on bn.MaBNhan equals vv.MaBNhan
                             where vv.NgayVao < tungay
                                    && bn.NoiTru == 1
                                    && (bn.Status == 0 || bn.Status == 1 || bn.Status == 4 || bn.Status == 5)
                                    && dsMaKpChon.Contains(bn.MaKP)
                              select bn).ToList();

            var BNNoiTru03 = (from bn in _data.BenhNhans
                             join rv in _data.RaViens
                                on bn.MaBNhan equals rv.MaBNhan
                             where rv.NgayVao < tungay && rv.NgayRa >= tungay
                                    && bn.NoiTru == 1
                                    && dsMaKpChon.Contains(bn.MaKP)
                              select bn).ToList();

            int BnNoiTru_Sl = BNNoiTru01.Count() + BNNoiTru02.Count() + BNNoiTru03.Count();
            _dic.Add("BnNoiTru_Sl", BnNoiTru_Sl);


            //BHYT
            int BnNoiTruBHYT_Sl = BNNoiTru01.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count() 
                                    + BNNoiTru02.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count() 
                                    + BNNoiTru03.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count();
            _dic.Add("BnNoiTruBHYT_Sl", BnNoiTruBHYT_Sl);
            

            int songaydt01 = 0;
            int songaydt02 = 0;

            // cac bn da ra vien
            var dsbn1 = (from a in _data.BenhNhans
                         join b in _data.RaViens on a.MaBNhan equals b.MaBNhan
                         where a.NoiTru == 1 && a.Status == 3 || a.Status == 2
                                && dsMaKpChon.Contains(b.MaKP)
                         //&& b.NgayVao >= tungay && b.NgayRa <= denngay
                         select b).ToList();
            if (dsbn1.Count() > 0)
            {
                songaydt01 = (from a in dsbn1
                             select (a.NgayVao < tungay && a.NgayRa >= tungay && a.NgayRa <= denngay) ? (a.NgayRa - tungay).Value.Days :
                             (
                                (a.NgayVao < tungay && a.NgayRa > denngay) ? (denngay - tungay).Days :
                                (
                                    (tungay <= a.NgayVao && a.NgayVao <= denngay && a.NgayRa > denngay) ? (denngay - a.NgayVao).Value.Days :
                                    (
                                        (tungay <= a.NgayVao && a.NgayVao <= denngay && tungay <= a.NgayRa && a.NgayRa <= denngay) ? (a.NgayRa - a.NgayVao).Value.Days : 0
                                    )
                                )
                             )).Sum();
                var ngaydt01 = (from a in dsbn1
                               where a.NgayVao < tungay && a.NgayRa >= tungay && a.NgayRa <= denngay
                               select (a.NgayRa - tungay).Value.Days
                               ).Sum();
                var ngaydt02 = (from a in dsbn1
                               where a.NgayVao < tungay && a.NgayRa > denngay
                               select (denngay - tungay).Days
                               ).Sum();
                var ngaydt03 = (from a in dsbn1
                               where tungay <= a.NgayVao && a.NgayVao <= denngay && a.NgayRa > denngay
                               select (denngay - a.NgayVao).Value.Days
                               ).Sum();
                var ngaydt04 = (from a in dsbn1
                               where tungay <= a.NgayVao && a.NgayVao <= denngay && tungay <= a.NgayRa && a.NgayRa <= denngay
                               select (a.NgayRa - a.NgayVao).Value.Days
                               ).Sum();

                int ngaydt0 = ngaydt01 + ngaydt02 + ngaydt03 + ngaydt04;
            }
            //cac bn chua ra vien
            var dsbn2 = (from a in _data.BenhNhans
                         join b in _data.VaoViens on a.MaBNhan equals b.MaBNhan
                         where a.NoiTru == 1 && a.Status == 1 || a.Status == 4 || a.Status == 5
                                && dsMaKpChon.Contains(b.MaKP)
                         //&& b.NgayVao <= denngay
                         select b).ToList();
            if (dsbn2.Count() > 0)
            {
                songaydt02 = (from a in dsbn2
                             select (a.NgayVao < tungay ? (denngay.Date - tungay.Date).Days : a.NgayVao >= tungay && a.NgayVao<= denngay ? (denngay.Date - a.NgayVao.Value.Date).Days : 0)
                                 ).Sum();
            }

            int songaydt = songaydt01 + songaydt02;

            _dic.Add("DTNoiTru_Sl", songaydt);

            double DTNoiTru_Tb = BnNoiTru_Sl > 0 ? Math.Round((double)songaydt / BnNoiTru_Sl, 2) : 0;
            _dic.Add("DTNoiTru_Tb", DTNoiTru_Tb);

            var BnTuVong = (from rv in _data.RaViens
                            join bn in _data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                            where rv.NgayVao >= tungay && rv.NgayVao <= denngay
                              && dsMaKpChon.Contains(rv.MaKP)
                              && bn.NoiTru == 1
                              && rv.KetQua == "Tử vong"
                              && bn.Status == 3
                            select rv.SoNgaydt).Count();
            double TyLeTuVong = 0;
            if (BnNoiTru_Sl > 0)
                TyLeTuVong = Math.Round(((double)BnTuVong / (double)BnNoiTru_Sl), 4);
            _dic.Add("TyLeTuVong", TyLeTuVong);

            var chuyenVienNoiTru = (from bn in _data.BenhNhans
                                    join bnkb in _data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                    join rv in _data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                    where bn.NoiTru == 1 && bnkb.PhuongAn == 2
                                            && bnkb.NgayNghi >= tungay && bnkb.NgayNghi <= denngay
                                            && dsMaKpChon.Contains(bn.MaKP)
                                    select bn).ToList();

            int CVNoiTru_Sl = chuyenVienNoiTru.Count();
            _dic.Add("CVNoiTru_Sl", CVNoiTru_Sl);
            //BHYT
            int CVNoiTruBHYT_Sl = chuyenVienNoiTru.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count();
            _dic.Add("CVNoiTruBHYT_Sl", CVNoiTruBHYT_Sl);
            //4444444444444
            //Phau thuat
            var PhauThuat = (from bn in _data.BenhNhans
                             join cls in _data.CLS on bn.MaBNhan equals cls.MaBNhan
                             join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                             where dv.MaNhom5937 == 8
                                    && bn.NNhap >= tungay && bn.NNhap <= denngay
                                    && dsMaKpChon.Contains(bn.MaKP)
                                    && cd.Status == 1
                             select bn).ToList();

            int PhauThuat_Sl = PhauThuat.Count();
            _dic.Add("PhauThuat_Sl", PhauThuat_Sl);
            //PT co cbi
            int PTCoCbi_Sl = PhauThuat.Where(p => p.CapCuu == 0).Count();
            _dic.Add("PTCoCbi_Sl", PTCoCbi_Sl);

            //HoatDongCLS
            var HDCLS = (from cd in _data.ChiDinhs
                         join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                         join cls in _data.CLS on cd.IdCLS equals cls.IdCLS
                         where cd.NgayTH >= tungay && cd.NgayTH <= denngay
                               && dsMaKpChon.Contains(cls.MaKP)
                               && cd.Status == 1
                         select new { cd, dv }).ToList();

            int XN_Sl = HDCLS.Where(p => p.dv.MaNhom5937 == 1).Count();
            _dic.Add("XN_Sl", XN_Sl);

            int Xquang_Sl = HDCLS.Where(p => p.dv.TenDV.ToLower().Contains("chụp xquang")).Count();
            _dic.Add("Xquang_Sl", Xquang_Sl);

            int SieuAm_Sl = HDCLS.Where(p => p.dv.IdTieuNhom == 4 || p.dv.IdTieuNhom == 99).Count();
            _dic.Add("SieuAm_Sl", SieuAm_Sl);

            int NoiSoiPheQuan_Sl = HDCLS.Where(p => p.dv.IdTieuNhom == 43).Count();
            int NoiSoiTMH_Sl = HDCLS.Where(p => p.dv.IdTieuNhom == 98).Count();
            int NoiSoi_Sl = HDCLS.Where(p => p.dv.IdTieuNhom == 43 || p.dv.IdTieuNhom == 98).Count();
            _dic.Add("NoiSoiPheQuan_Sl", NoiSoiPheQuan_Sl);
            _dic.Add("NoiSoiTMH_Sl", NoiSoiTMH_Sl);
            _dic.Add("NoiSoi_Sl", NoiSoi_Sl);

            int CT_MRI_Sl = HDCLS.Where(p => p.dv.TenDV.ToLower().Contains("chụp clvt")
                                            || p.dv.TenDV.ToLower().Contains("chụp cắt lớp vi tính")
                                            || p.dv.TenDV.ToLower().Contains("chụp ct")).Count();
            _dic.Add("CT_MRI_Sl", CT_MRI_Sl);
            #endregion

            #region cùng kỳ năm trước
            //1111111111
            //Giuong benh
            var GiuongKH1 = (from kp in _data.KPhongs.Where(p => p.SoGiuongKH != null && dsMaKpChon.Contains(p.MaKP)) select new { kp.MaKP, kp.TenKP, kp.SoGiuongKH }).ToList();
            int TongGiuong1 = 0;
            for (int i = 0; i < GiuongKH1.Count; i++)
            {
                int SoGiuongTungKhoa1 = tachchuoi(GiuongKH1[i].SoGiuongKH, tungay1.Year);
                TongGiuong1 += SoGiuongTungKhoa1;
            }
            _dic.Add("GiuongKH1", TongGiuong1);
            var q21 = (from bn in _data.BenhNhans
                       join bnkb in _data.BNKBs.Where(p => p.NgayKham >= tungay1 && p.NgayKham <= denngay1).Where(p => p.Giuong != null && dsMaKpChon.Contains(p.MaKP)) on bn.MaBNhan equals bnkb.MaBNhan
                       select bnkb).ToList();

            int getGiuongTT1 = q21.Select(p => p.Giuong).Distinct().Count();
            _dic.Add("GiuongTT1", getGiuongTT1);
            double CongSuatGiuongKH1 = Math.Round((double)getGiuongTT1 / TongGiuong1, 4);
            _dic.Add("CongSuatGiuongKH1", CongSuatGiuongKH1);

            //22222222222
            //Luot kham
            var KCB1 = _data.BenhNhans.Where(p => p.NNhap >= tungay1 && p.NNhap <= denngay1 && dsMaKpChon.Contains(p.MaKP)).ToList();

            int KBenh_Sl1 = KCB1.Count();
            _dic.Add("KBenh_Sl1", KBenh_Sl1);
            //BHYT
            int KBenhBHYT_Sl1 = KCB1.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count();
            _dic.Add("KBenhBHYT_Sl1", KBenhBHYT_Sl1);
            //
            //Tong so BN chuyen vien tai phong kham
            var chuyenVienPK1 = (from bn in _data.BenhNhans
                                 join bnkb in _data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                 where bn.NoiTru == 0 && bnkb.PhuongAn == 2
                                         && bn.NNhap >= tungay1 && bn.NNhap <= denngay1
                                         && dsMaKpChon.Contains(bn.MaKP)
                                 select bn).ToList();

            int CVTaiPK_Sl1 = chuyenVienPK1.Count();
            _dic.Add("CVTaiPK_Sl1", CVTaiPK_Sl1);
            //BHYT
            int CVTaiPKBHYT_Sl1 = chuyenVienPK1.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count();
            _dic.Add("CVTaiPKBHYT_Sl1", CVTaiPKBHYT_Sl1);
            //
            //Tong so BN ngoai tru
            var BNNgoaiTru11 = (from bn in _data.BenhNhans
                                join vv in _data.VaoViens
                                   on bn.MaBNhan equals vv.MaBNhan
                                where vv.NgayVao >= tungay1 && vv.NgayVao <= denngay1
                                       && bn.NoiTru == 0
                                       && bn.DTNT == true
                                       && dsMaKpChon.Contains(bn.MaKP)
                                select bn).ToList();
            var BNNgoaiTru12 = (from bn in _data.BenhNhans
                                join vv in _data.VaoViens
                                   on bn.MaBNhan equals vv.MaBNhan
                                where vv.NgayVao < tungay1
                                       && bn.NoiTru == 0
                                       && bn.DTNT == true
                                       && (bn.Status == 0 || bn.Status == 1 || bn.Status == 4 || bn.Status == 5)
                                       && dsMaKpChon.Contains(bn.MaKP)
                                select bn).ToList();

            var BNNgoaiTru13 = (from bn in _data.BenhNhans
                                join rv in _data.RaViens
                                   on bn.MaBNhan equals rv.MaBNhan
                                where rv.NgayVao < tungay1 && rv.NgayRa >= tungay1
                                       && bn.NoiTru == 0
                                       && bn.DTNT == true
                                       && dsMaKpChon.Contains(bn.MaKP)
                                select bn).ToList();

            int BnNgoaiTru_Sl1 = BNNgoaiTru11.Count() + BNNgoaiTru12.Count() + BNNgoaiTru13.Count();
            _dic.Add("BnNgoaiTru_Sl1", BnNgoaiTru_Sl1);
            //BHYT
            int BnNgoaiTruBHYT_Sl1 = BNNgoaiTru11.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count()
                                    + BNNgoaiTru12.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count()
                                    + BNNgoaiTru13.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count();
            _dic.Add("BnNgoaiTruBHYT_Sl1", BnNgoaiTruBHYT_Sl1);
            //3333333333
            //Hoat dong dieu tri noi tru
            var BNNoiTru11 = (from bn in _data.BenhNhans
                              join vv in _data.VaoViens
                                 on bn.MaBNhan equals vv.MaBNhan
                              where vv.NgayVao >= tungay1 && vv.NgayVao <= denngay1
                                     && bn.NoiTru == 1
                                     && dsMaKpChon.Contains(bn.MaKP)
                              select bn).ToList();
            var BNNoiTru12 = (from bn in _data.BenhNhans
                              join vv in _data.VaoViens
                                 on bn.MaBNhan equals vv.MaBNhan
                              where vv.NgayVao < tungay1
                                     && bn.NoiTru == 1
                                     && (bn.Status == 0 || bn.Status == 1 || bn.Status == 4 || bn.Status == 5)
                                     && dsMaKpChon.Contains(bn.MaKP)
                              select bn).ToList();

            var BNNoiTru13 = (from bn in _data.BenhNhans
                              join rv in _data.RaViens
                                 on bn.MaBNhan equals rv.MaBNhan
                              where rv.NgayVao < tungay1 && rv.NgayRa >= tungay1
                                     && bn.NoiTru == 1
                                     && dsMaKpChon.Contains(bn.MaKP)
                              select bn).ToList();

            int BnNoiTru_Sl1 = BNNoiTru11.Count() + BNNoiTru12.Count() + BNNoiTru13.Count();
            _dic.Add("BnNoiTru_Sl1", BnNoiTru_Sl1);


            //BHYT
            int BnNoiTruBHYT_Sl1 = BNNoiTru11.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count()
                                    + BNNoiTru12.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count()
                                    + BNNoiTru13.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count();
            _dic.Add("BnNoiTruBHYT_Sl1", BnNoiTruBHYT_Sl1);

            int songaydt11 = 0;
            int songaydt12 = 0;

            // cac bn da ra vien
            if (dsbn1.Count() > 0)
            {
                songaydt11 = (from a in dsbn1
                              select (a.NgayVao < tungay1 && a.NgayRa >= tungay1 && a.NgayRa <= denngay1) ? (a.NgayRa - tungay1).Value.Days :
                              (
                                 (a.NgayVao < tungay1 && a.NgayRa > denngay1) ? (denngay1 - tungay1).Days :
                                 (
                                     (tungay1 <= a.NgayVao && a.NgayVao <= denngay1 && a.NgayRa > denngay1) ? (denngay1 - a.NgayVao).Value.Days :
                                     (
                                         (tungay1 <= a.NgayVao && a.NgayVao <= denngay1 && tungay1 <= a.NgayRa && a.NgayRa <= denngay1) ? (a.NgayRa - a.NgayVao).Value.Days : 0
                                     )
                                 )
                              )).Sum();
                var ngaydt11 = (from a in dsbn1
                                where a.NgayVao < tungay1 && a.NgayRa >= tungay1 && a.NgayRa <= denngay1
                                select (a.NgayRa - tungay).Value.Days
                               ).Sum();
                var ngaydt12 = (from a in dsbn1
                                where a.NgayVao < tungay1 && a.NgayRa > denngay1
                                select (denngay1 - tungay1).Days
                               ).Sum();
                var ngaydt13 = (from a in dsbn1
                                where tungay1 <= a.NgayVao && a.NgayVao <= denngay1 && a.NgayRa > denngay1
                                select (denngay1 - a.NgayVao).Value.Days
                               ).Sum();
                var ngaydt14 = (from a in dsbn1
                                where tungay1 <= a.NgayVao && a.NgayVao <= denngay1 && tungay1 <= a.NgayRa && a.NgayRa <= denngay1
                                select (a.NgayRa - a.NgayVao).Value.Days
                               ).Sum();

                int ngaydt1 = ngaydt11 + ngaydt12 + ngaydt13 + ngaydt14;
            }
            //cac bn chua ra vien
            if (dsbn2.Count() > 0)
            {
                songaydt12 = (from a in dsbn2
                              select (a.NgayVao < tungay1 ? (denngay1.Date - tungay1.Date).Days : a.NgayVao >= tungay1 && a.NgayVao <= denngay1 ? (denngay1.Date - a.NgayVao.Value.Date).Days : 0)
                                 ).Sum();
            }

            int songaydt1 = songaydt11 + songaydt12;

            _dic.Add("DTNoiTru_Sl1", songaydt1);

            double DTNoiTru_Tb1 = BnNoiTru_Sl1 > 0 ? Math.Round((double)songaydt / BnNoiTru_Sl1, 2) : 0;
            _dic.Add("DTNoiTru_Tb1", DTNoiTru_Tb1);

            var BnTuVong1 = (from rv in _data.RaViens
                             join bn in _data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                             where rv.NgayVao >= tungay1 && rv.NgayVao <= denngay1
                               && dsMaKpChon.Contains(rv.MaKP)
                               && bn.NoiTru == 1
                               && rv.KetQua == "Tử vong"
                             select rv.SoNgaydt).Count();
            double TyLeTuVong1 = 0;
            if (BnNoiTru_Sl1 > 0)
                TyLeTuVong1 = Math.Round((double)(BnTuVong1 / BnNoiTru_Sl1), 4);
            _dic.Add("TyLeTuVong1", TyLeTuVong1);

            var chuyenVienNoiTru1 = (from bn in _data.BenhNhans
                                     join bnkb in _data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                     join rv in _data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                     where bn.NoiTru == 1 && bnkb.PhuongAn == 2
                                             && bnkb.NgayNghi >= tungay1 && bnkb.NgayNghi <= denngay1
                                             && dsMaKpChon.Contains(bn.MaKP)
                                     select bn).ToList();

            int CVNoiTru_Sl1 = chuyenVienNoiTru1.Count();
            _dic.Add("CVNoiTru_Sl1", CVNoiTru_Sl1);
            //BHYT
            int CVNoiTruBHYT_Sl1 = chuyenVienNoiTru1.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count();
            _dic.Add("CVNoiTruBHYT_Sl1", CVNoiTruBHYT_Sl1);
            //4444444444444
            //Phau thuat
            var PhauThuat1 = (from bn in _data.BenhNhans
                              join cls in _data.CLS on bn.MaBNhan equals cls.MaBNhan
                              join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                              where dv.MaNhom5937 == 8
                                     && bn.NNhap >= tungay1 && bn.NNhap <= denngay1
                                     && dsMaKpChon.Contains(bn.MaKP)
                                     && cd.Status == 1
                              select bn).ToList();

            int PhauThuat_Sl1 = PhauThuat1.Count();
            _dic.Add("PhauThuat_Sl1", PhauThuat_Sl1);
            //PT co cbi
            int PTCoCbi_Sl1 = PhauThuat1.Where(p => p.CapCuu == 0).Count();
            _dic.Add("PTCoCbi_Sl1", PTCoCbi_Sl1);

            //HoatDongCLS
            var HDCLS1 = (from cd in _data.ChiDinhs
                          join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                          join cls in _data.CLS on cd.IdCLS equals cls.IdCLS
                          where cd.NgayTH >= tungay1 && cd.NgayTH <= denngay1
                                && dsMaKpChon.Contains(cls.MaKP)
                                && cd.Status == 1
                          select new { cd, dv }).ToList();

            int XN_Sl1 = HDCLS1.Where(p => p.dv.MaNhom5937 == 1).Count();
            _dic.Add("XN_Sl1", XN_Sl1);

            int Xquang_Sl1 = HDCLS1.Where(p => p.dv.TenDV.ToLower().Contains("chụp xquang")).Count();
            _dic.Add("Xquang_Sl1", Xquang_Sl1);

            int SieuAm_Sl1 = HDCLS1.Where(p => p.dv.IdTieuNhom == 4 || p.dv.IdTieuNhom == 99).Count();
            _dic.Add("SieuAm_Sl1", SieuAm_Sl1);

            int NoiSoiPheQuan_Sl1 = HDCLS1.Where(p => p.dv.IdTieuNhom == 43).Count();
            int NoiSoiTMH_Sl1 = HDCLS1.Where(p => p.dv.IdTieuNhom == 98).Count();
            int NoiSoi_Sl1 = HDCLS1.Where(p => p.dv.IdTieuNhom == 43 || p.dv.IdTieuNhom == 98).Count();
            _dic.Add("NoiSoiPheQuan_Sl1", NoiSoiPheQuan_Sl1);
            _dic.Add("NoiSoiTMH_Sl1", NoiSoiTMH_Sl1);
            _dic.Add("NoiSoi_Sl1", NoiSoi_Sl1);

            int CT_MRI_Sl1 = HDCLS1.Where(p => p.dv.TenDV.ToLower().Contains("chụp clvt")
                                            || p.dv.TenDV.ToLower().Contains("chụp cắt lớp vi tính")
                                            || p.dv.TenDV.ToLower().Contains("chụp ct")).Count();
            _dic.Add("CT_MRI_Sl1", CT_MRI_Sl1);
            #endregion

            #region cùng kỳ 2 năm trước
            //1111111111
            //Giuong benh
            var GiuongKH2 = (from kp in _data.KPhongs.Where(p => p.SoGiuongKH != null && dsMaKpChon.Contains(p.MaKP)) select new { kp.MaKP, kp.TenKP, kp.SoGiuongKH }).ToList();
            int TongGiuong2 = 0;
            for (int i = 0; i < GiuongKH2.Count; i++)
            {
                int SoGiuongTungKhoa2 = tachchuoi(GiuongKH2[i].SoGiuongKH, tungay2.Year);
                TongGiuong2 += SoGiuongTungKhoa2;
            }
            _dic.Add("GiuongKH2", TongGiuong2);
            var q22 = (from bn in _data.BenhNhans
                       join bnkb in _data.BNKBs.Where(p => p.NgayKham >= tungay2 && p.NgayKham <= denngay2).Where(p => p.Giuong != null && dsMaKpChon.Contains(p.MaKP)) on bn.MaBNhan equals bnkb.MaBNhan
                       select bnkb).ToList();

            int getGiuongTT2 = q22.Select(p => p.Giuong).Distinct().Count();
            _dic.Add("GiuongTT2", getGiuongTT2);
            double CongSuatGiuongKH2 = Math.Round((double)getGiuongTT2 / TongGiuong2, 4);
            _dic.Add("CongSuatGiuongKH2", CongSuatGiuongKH2);

            //22222222222
            //Luot kham
            var KCB2 = _data.BenhNhans.Where(p => p.NNhap >= tungay2 && p.NNhap <= denngay2 && dsMaKpChon.Contains(p.MaKP)).ToList();

            int KBenh_Sl2 = KCB2.Count();
            _dic.Add("KBenh_Sl2", KBenh_Sl2);
            //BHYT
            int KBenhBHYT_Sl2 = KCB2.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count();
            _dic.Add("KBenhBHYT_Sl2", KBenhBHYT_Sl2);
            //
            //Tong so BN chuyen vien tai phong kham
            var chuyenVienPK2 = (from bn in _data.BenhNhans
                                 join bnkb in _data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                 where bn.NoiTru == 0 && bnkb.PhuongAn == 2
                                         && bn.NNhap >= tungay2 && bn.NNhap <= denngay2
                                         && dsMaKpChon.Contains(bn.MaKP)
                                 select bn).ToList();

            int CVTaiPK_Sl2 = chuyenVienPK2.Count();
            _dic.Add("CVTaiPK_Sl2", CVTaiPK_Sl2);
            //BHYT
            int CVTaiPKBHYT_Sl2 = chuyenVienPK2.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count();
            _dic.Add("CVTaiPKBHYT_Sl2", CVTaiPKBHYT_Sl2);
            //
            //Tong so BN ngoai tru
            var BNNgoaiTru21 = (from bn in _data.BenhNhans
                                join vv in _data.VaoViens
                                   on bn.MaBNhan equals vv.MaBNhan
                                where vv.NgayVao >= tungay2 && vv.NgayVao <= denngay2
                                       && bn.NoiTru == 0
                                       && bn.DTNT == true
                                       && dsMaKpChon.Contains(bn.MaKP)
                                select bn).ToList();
            var BNNgoaiTru22 = (from bn in _data.BenhNhans
                                join vv in _data.VaoViens
                                   on bn.MaBNhan equals vv.MaBNhan
                                where vv.NgayVao < tungay2
                                       && bn.NoiTru == 0
                                       && bn.DTNT == true
                                       && (bn.Status == 0 || bn.Status == 1 || bn.Status == 4 || bn.Status == 5)
                                       && dsMaKpChon.Contains(bn.MaKP)
                                select bn).ToList();

            var BNNgoaiTru23 = (from bn in _data.BenhNhans
                                join rv in _data.RaViens
                                   on bn.MaBNhan equals rv.MaBNhan
                                where rv.NgayVao < tungay2 && rv.NgayRa >= tungay2
                                       && bn.NoiTru == 0
                                       && bn.DTNT == true
                                       && dsMaKpChon.Contains(bn.MaKP)
                                select bn).ToList();

            int BnNgoaiTru_Sl2 = BNNgoaiTru21.Count() + BNNgoaiTru22.Count() + BNNgoaiTru23.Count();
            _dic.Add("BnNgoaiTru_Sl2", BnNgoaiTru_Sl2);
            //BHYT
            int BnNgoaiTruBHYT_Sl2 = BNNgoaiTru21.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count()
                                    + BNNgoaiTru22.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count()
                                    + BNNgoaiTru23.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count();
            _dic.Add("BnNgoaiTruBHYT_Sl2", BnNgoaiTruBHYT_Sl2);
            //3333333333
            //Hoat dong dieu tri noi tru
            var BNNoiTru21 = (from bn in _data.BenhNhans
                              join vv in _data.VaoViens
                                 on bn.MaBNhan equals vv.MaBNhan
                              where vv.NgayVao >= tungay2 && vv.NgayVao <= denngay2
                                     && bn.NoiTru == 1
                                     && dsMaKpChon.Contains(bn.MaKP)
                              select bn).ToList();
            var BNNoiTru22 = (from bn in _data.BenhNhans
                              join vv in _data.VaoViens
                                 on bn.MaBNhan equals vv.MaBNhan
                              where vv.NgayVao < tungay1
                                     && bn.NoiTru == 1
                                     && (bn.Status == 0 || bn.Status == 1 || bn.Status == 4 || bn.Status == 5)
                                     && dsMaKpChon.Contains(bn.MaKP)
                              select bn).ToList();

            var BNNoiTru23 = (from bn in _data.BenhNhans
                              join rv in _data.RaViens
                                 on bn.MaBNhan equals rv.MaBNhan
                              where rv.NgayVao < tungay1 && rv.NgayRa >= tungay1
                                     && bn.NoiTru == 1
                                     && dsMaKpChon.Contains(bn.MaKP)
                              select bn).ToList();

            int BnNoiTru_Sl2 = BNNoiTru21.Count() + BNNoiTru22.Count() + BNNoiTru23.Count();
            _dic.Add("BnNoiTru_Sl2", BnNoiTru_Sl2);


            //BHYT
            int BnNoiTruBHYT_Sl2 = BNNoiTru21.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count()
                                    + BNNoiTru22.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count()
                                    + BNNoiTru23.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count();
            _dic.Add("BnNoiTruBHYT_Sl2", BnNoiTruBHYT_Sl2);

            int songaydt21 = 0;
            int songaydt22 = 0;

            // cac bn da ra vien
            {
                songaydt21 = (from a in dsbn1
                              select (a.NgayVao < tungay2 && a.NgayRa >= tungay2 && a.NgayRa <= denngay2) ? (a.NgayRa - tungay2).Value.Days :
                              (
                                 (a.NgayVao < tungay2 && a.NgayRa > denngay2) ? (denngay2 - tungay2).Days :
                                 (
                                     (tungay2 <= a.NgayVao && a.NgayVao <= denngay2 && a.NgayRa > denngay2) ? (denngay2 - a.NgayVao).Value.Days :
                                     (
                                         (tungay2 <= a.NgayVao && a.NgayVao <= denngay2 && tungay2 <= a.NgayRa && a.NgayRa <= denngay2) ? (a.NgayRa - a.NgayVao).Value.Days : 0
                                     )
                                 )
                              )).Sum();
                var ngaydt21 = (from a in dsbn1
                                where a.NgayVao < tungay2 && a.NgayRa >= tungay2 && a.NgayRa <= denngay2
                                select (a.NgayRa - tungay2).Value.Days
                               ).Sum();
                var ngaydt22 = (from a in dsbn1
                                where a.NgayVao < tungay2 && a.NgayRa > denngay2
                                select (denngay2 - tungay2).Days
                               ).Sum();
                var ngaydt23 = (from a in dsbn1
                                where tungay2 <= a.NgayVao && a.NgayVao <= denngay2 && a.NgayRa > denngay2
                                select (denngay2 - a.NgayVao).Value.Days
                               ).Sum();
                var ngaydt24 = (from a in dsbn1
                                where tungay2 <= a.NgayVao && a.NgayVao <= denngay2 && tungay2 <= a.NgayRa && a.NgayRa <= denngay2
                                select (a.NgayRa - a.NgayVao).Value.Days
                               ).Sum();

                int ngaydt2 = ngaydt21 + ngaydt22 + ngaydt23 + ngaydt24;
            }
            //cac bn chua ra vien
            if (dsbn2.Count() > 0)
            {
                songaydt22 = (from a in dsbn2
                              select (a.NgayVao < tungay2 ? (denngay2.Date - tungay2.Date).Days : a.NgayVao >= tungay2 && a.NgayVao <= denngay2 ? (denngay2.Date - a.NgayVao.Value.Date).Days : 0)
                                 ).Sum();
            }

            int songaydt2 = songaydt21 + songaydt22;

            _dic.Add("DTNoiTru_Sl2", songaydt2);

            double DTNoiTru_Tb2 = BnNoiTru_Sl2 > 0 ? Math.Round((double)songaydt2 / BnNoiTru_Sl, 2) : 0;
            _dic.Add("DTNoiTru_Tb2", DTNoiTru_Tb2);

            var BnTuVong2 = (from rv in _data.RaViens
                             join bn in _data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                             where rv.NgayVao >= tungay2 && rv.NgayVao <= denngay2
                               && dsMaKpChon.Contains(rv.MaKP)
                               && bn.NoiTru == 1
                               && rv.KetQua == "Tử vong"
                             select rv.SoNgaydt).Count();
            double TyLeTuVong2 = 0;
            if (BnNoiTru_Sl2 > 0)
                TyLeTuVong2 = Math.Round((double)(BnTuVong2 / BnNoiTru_Sl2), 4);
            _dic.Add("TyLeTuVong2", TyLeTuVong2);

            var chuyenVienNoiTru2 = (from bn in _data.BenhNhans
                                     join bnkb in _data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                     join rv in _data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                     where bn.NoiTru == 1 && bnkb.PhuongAn == 2
                                             && bnkb.NgayNghi >= tungay2 && bnkb.NgayNghi <= denngay2
                                             && dsMaKpChon.Contains(bn.MaKP)
                                     select bn).ToList();

            int CVNoiTru_Sl2 = chuyenVienNoiTru2.Count();
            _dic.Add("CVNoiTru_Sl2", CVNoiTru_Sl2);
            //BHYT
            int CVNoiTruBHYT_Sl2 = chuyenVienNoiTru2.Where(p => p.DTuong.ToLower().Equals("bhyt")).Count();
            _dic.Add("CVNoiTruBHYT_Sl2", CVNoiTruBHYT_Sl2);
            //4444444444444
            //Phau thuat
            var PhauThuat2 = (from bn in _data.BenhNhans
                              join cls in _data.CLS on bn.MaBNhan equals cls.MaBNhan
                              join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                              where dv.MaNhom5937 == 8
                                     && bn.NNhap >= tungay2 && bn.NNhap <= denngay2
                                     && dsMaKpChon.Contains(bn.MaKP)
                                     && cd.Status == 1
                              select bn).ToList();

            int PhauThuat_Sl2 = PhauThuat2.Count();
            _dic.Add("PhauThuat_Sl2", PhauThuat_Sl2);
            //PT co cbi
            int PTCoCbi_Sl2 = PhauThuat2.Where(p => p.CapCuu == 0).Count();
            _dic.Add("PTCoCbi_Sl2", PTCoCbi_Sl2);

            //HoatDongCLS
            var HDCLS2 = (from cd in _data.ChiDinhs
                          join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                          join cls in _data.CLS on cd.IdCLS equals cls.IdCLS
                          where cd.NgayTH >= tungay2 && cd.NgayTH <= denngay2
                                && dsMaKpChon.Contains(cls.MaKP)
                                && cd.Status == 1
                          select new { cd, dv }).ToList();

            int XN_Sl2 = HDCLS2.Where(p => p.dv.MaNhom5937 == 1).Count();
            _dic.Add("XN_Sl2", XN_Sl2);

            int Xquang_Sl2 = HDCLS2.Where(p => p.dv.TenDV.ToLower().Contains("chụp xquang")).Count();
            _dic.Add("Xquang_Sl2", Xquang_Sl2);

            int SieuAm_Sl2 = HDCLS2.Where(p => p.dv.IdTieuNhom == 4 || p.dv.IdTieuNhom == 99).Count();
            _dic.Add("SieuAm_Sl2", SieuAm_Sl2);

            int NoiSoiPheQuan_Sl2 = HDCLS2.Where(p => p.dv.IdTieuNhom == 43).Count();
            int NoiSoiTMH_Sl2 = HDCLS2.Where(p => p.dv.IdTieuNhom == 98).Count();
            int NoiSoi_Sl2 = HDCLS2.Where(p => p.dv.IdTieuNhom == 43 || p.dv.IdTieuNhom == 98).Count();
            _dic.Add("NoiSoiPheQuan_Sl2", NoiSoiPheQuan_Sl2);
            _dic.Add("NoiSoiTMH_Sl2", NoiSoiTMH_Sl2);
            _dic.Add("NoiSoi_Sl2", NoiSoi_Sl2);

            int CT_MRI_Sl2 = HDCLS2.Where(p => p.dv.TenDV.ToLower().Contains("chụp clvt")
                                            || p.dv.TenDV.ToLower().Contains("chụp cắt lớp vi tính")
                                            || p.dv.TenDV.ToLower().Contains("chụp ct")).Count();
            _dic.Add("CT_MRI_Sl2", CT_MRI_Sl2);
            #endregion

            DungChung.Ham.Print(DungChung.PrintConfig.Rep_BCHoatDongKCBToanVien_27023, null, _dic, false);
        }

        public int tachchuoi(string s, int year)
        {
            int sogiuong = 0;
            string[] arr = s.Split('|');

            for (int i = 0; i < arr.Count(); i++)
            {
                if (arr[i] != "")
                {
                    string[] arrGi = arr[i].Split(':');
                    if (year == Convert.ToInt32(arrGi[0].ToString()))
                    {
                        sogiuong = Convert.ToInt32(arrGi[1].ToString());
                    }
                }

            }
            return sogiuong;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
