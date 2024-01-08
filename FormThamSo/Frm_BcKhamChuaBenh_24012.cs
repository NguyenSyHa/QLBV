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
    public partial class Frm_BcKhamChuaBenh_24012 : Form
    {
        public static bool PrintNow = false;
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public Frm_BcKhamChuaBenh_24012()
        {
            InitializeComponent();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tuNgay1 = Convert.ToDateTime(dteTuNgay.EditValue);
            DateTime denNgay1 = Convert.ToDateTime(dteDenNgay.EditValue);

            DateTime tuNgay2 = tuNgay1.AddMonths(-1);
            DateTime denNgay2 = denNgay1.AddMonths(-1);

            DateTime tuNgay3 = tuNgay1.AddYears(-1);
            DateTime denNgay3 = denNgay1.AddYears(-1);

            Dictionary<string, object> dic = new Dictionary<string, object>();

            //dic.Add("ThangNam", "THÁNG " + cboThang.Text + " NĂM " + cboNam.Text);

            //Thang da chon
            var bn1 = _data.BenhNhans.Where(p => p.NNhap > tuNgay1 && p.NNhap < denNgay1).ToList();
            var rv = (from bn in _data.BenhNhans
                      join rv1 in _data.RaViens.Where(p => p.NgayRa > tuNgay1 && p.NgayRa < denNgay1) on bn.MaBNhan equals rv1.MaBNhan
                      select new {bn , rv1}).ToList();

            var KBenh1 = bn1.Count();
            dic.Add("KBenh1", KBenh1);
            var KBenhBHYT1 = bn1.Where(p => p.DTuong.ToLower().Contains("bhyt")).Count();
            dic.Add("KBenhBHYT1", KBenhBHYT1);
            var KBenhBHYTNNgheo1 = bn1.Where(p => p.DTuong.ToLower().Contains("bhyt") && p.SThe.ToLower().Contains("hn")).Count();
            dic.Add("KBenhBHYTNNgheo1", KBenhBHYTNNgheo1);
            var DTtriBHYTNNgheo1 = bn1.Where(p => p.SThe.ToLower().Contains("hn") && p.NoiTru == 1).Count();
            dic.Add("DTtriBHYTNNgheo1", DTtriBHYTNNgheo1);
            var DTNoitru1 = rv.Where(p => p.bn.NoiTru == 1).Count();
            dic.Add("DTNoitru1", DTNoitru1);
            var DTNgoaitru1 = bn1.Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
            dic.Add("DTNgoaitru1", DTNgoaitru1);

            var ChuyenTuyen1 = (from a in bn1
                                join b in _data.BNKBs on a.MaBNhan equals b.MaBNhan
                                where b.PhuongAn == 2
                                select a
                          ).Count();
            dic.Add("ChuyenTuyen1", ChuyenTuyen1);

            var TongNgayDTNoitru1 = (from a in _data.DThuocs
                                     join b in _data.DThuoccts on a.IDDon equals b.IDDon
                                     join c in _data.BenhNhans on a.MaBNhan equals c.MaBNhan
                                     join d in _data.RaViens on a.MaBNhan equals d.MaBNhan
                                     where c.NoiTru == 1
                                           && d.NgayRa > tuNgay1
                                           && d.NgayRa < denNgay1
                                           && b.DonVi.ToLower().Contains("ngày")
                                     select b.SoLuong
                          ).Count();
            dic.Add("TongNgayDTNoitru1", TongNgayDTNoitru1);

            var TuVongTaiCSYT1 = (from a in _data.BenhNhans
                                  join b in _data.RaViens on a.MaBNhan equals b.MaBNhan
                                  where b.KetQua.ToLower().Contains("tử vong")
                                        && b.NgayRa > tuNgay1
                                        && b.NgayRa < denNgay1
                                  select new { a, b }
                          ).Count();
            dic.Add("TuVongTaiCSYT1", TuVongTaiCSYT1);


            var cls1 = (from a in _data.CLS
                        join b in _data.ChiDinhs on a.IdCLS equals b.IdCLS
                        join c in _data.DichVus on b.MaDV equals c.MaDV
                        where a.Status == 1
                              && a.NgayTH > tuNgay1
                              && a.NgayTH < denNgay1
                        select c
                       ).ToList();

            var XetNghiem1 = cls1.Where(p => p.IDNhom == 1).Count();
            dic.Add("XetNghiem1", XetNghiem1);
            var XQuang1 = cls1.Where(p => p.IdTieuNhom == 5).Count();
            dic.Add("XQuang1", XQuang1);
            var SieuAm1 = cls1.Where(p => p.IdTieuNhom == 4).Count();
            dic.Add("SieuAm1", SieuAm1);
            var DienTim1 = cls1.Where(p => p.IdTieuNhom == 15).Count();
            dic.Add("DienTim1", DienTim1);
            var NoiSoiTMH1 = cls1.Where(p => p.IdTieuNhom == 98).Count();
            dic.Add("NoiSoiTMH1", NoiSoiTMH1);
            var LuuHuyetNao1 = cls1.Where(p => p.IdTieuNhom == 133).Count();
            dic.Add("LuuHuyetNao1", LuuHuyetNao1);
            var VLTL_PHCN1 = cls1.Where(p => p.IDNhom == 8).Count();
            dic.Add("VLTL_PHCN1", VLTL_PHCN1);

            // So sanh voi thang truoc
            var bn2 = _data.BenhNhans.Where(p => p.NNhap > tuNgay2 && p.NNhap < denNgay2).ToList();
            var rv2 = (from bn in _data.BenhNhans
                      join rv1 in _data.RaViens.Where(p => p.NgayRa > tuNgay2 && p.NgayRa < denNgay2) on bn.MaBNhan equals rv1.MaBNhan
                      select new { bn, rv1 }).ToList();


            var KBenh2 = KBenh1 - bn2.Count();
            if (KBenh2 > 0)
                dic.Add("KBenh2", "+" + KBenh2);
            else
                dic.Add("KBenh2", KBenh2);

            var KBenhBHYT2 = KBenhBHYT1 - bn2.Where(p => p.DTuong.ToLower().Contains("bhyt")).Count();
            if (KBenhBHYT2 > 0)
                dic.Add("KBenhBHYT2", "+" + KBenhBHYT2);
            else
                dic.Add("KBenhBHYT2", KBenhBHYT2);

            var KBenhBHYTNNgheo2 = KBenhBHYTNNgheo1 - bn2.Where(p => p.DTuong.ToLower().Contains("bhyt") && p.SThe.ToLower().Contains("hn")).Count();
            if (KBenhBHYTNNgheo2 > 0)
                dic.Add("KBenhBHYTNNgheo2", "+" + KBenhBHYTNNgheo2);
            else
                dic.Add("KBenhBHYTNNgheo2", KBenhBHYTNNgheo2);
            var DTtriBHYTNNgheo2 = DTtriBHYTNNgheo1 - bn2.Where(p => p.SThe.ToLower().Contains("hn") && p.NoiTru == 1).Count();
            if (DTtriBHYTNNgheo2 > 0)
                dic.Add("DTtriBHYTNNgheo2", "+" + DTtriBHYTNNgheo2);
            else
                dic.Add("DTtriBHYTNNgheo2", DTtriBHYTNNgheo2);
            var DTNoitru2 = DTNoitru1 - rv2.Where(p => p.bn.NoiTru == 1).Count();
            if (DTNoitru2 > 0)
                dic.Add("DTNoitru2", "+" + DTNoitru2);
            else
                dic.Add("DTNoitru2", DTNoitru2);
            var DTNgoaitru2 = DTNgoaitru1 - bn2.Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
            if (DTNgoaitru2 > 0)
                dic.Add("DTNgoaitru2", "+" + DTNgoaitru2);
            else
                dic.Add("DTNgoaitru2", DTNgoaitru2);

            var ChuyenTuyen2 = ChuyenTuyen1 - (from a in bn2
                                               join b in _data.BNKBs on a.MaBNhan equals b.MaBNhan
                                               where b.PhuongAn == 2
                                               select a
                                              ).Count();
            if (ChuyenTuyen2 > 0)
                dic.Add("ChuyenTuyen2", "+" + ChuyenTuyen2);
            else
                dic.Add("ChuyenTuyen2", ChuyenTuyen2);

            var TongNgayDTNoitru2 = TongNgayDTNoitru1 - (from a in _data.DThuocs
                                                         join b in _data.DThuoccts on a.IDDon equals b.IDDon
                                                         join c in _data.BenhNhans on a.MaBNhan equals c.MaBNhan
                                                         join d in _data.RaViens on a.MaBNhan equals d.MaBNhan
                                                         where c.NoiTru == 1
                                                               && d.NgayRa > tuNgay2
                                                               && d.NgayRa < denNgay2
                                                               && b.DonVi.ToLower().Contains("ngày")
                                                         select b.SoLuong
                                                          ).Count();
            if (TongNgayDTNoitru2 > 0)
                dic.Add("TongNgayDTNoitru2", "+" + TongNgayDTNoitru2);
            else
                dic.Add("TongNgayDTNoitru2", TongNgayDTNoitru2);

            var TuVongTaiCSYT2 = TuVongTaiCSYT1 - (from a in _data.BenhNhans
                                                   join b in _data.RaViens on a.MaBNhan equals b.MaBNhan
                                                   where b.KetQua.ToLower().Contains("tử vong")
                                                         && b.NgayRa > tuNgay2
                                                         && b.NgayRa < denNgay2
                                                   select new { a, b }
                                                  ).Count();
            if (TuVongTaiCSYT2 > 0)
                dic.Add("TuVongTaiCSYT2", "+" + TuVongTaiCSYT2);
            else
                dic.Add("TuVongTaiCSYT2", TuVongTaiCSYT2);


            var cls2 = (from a in _data.CLS
                        join b in _data.ChiDinhs on a.IdCLS equals b.IdCLS
                        join c in _data.DichVus on b.MaDV equals c.MaDV
                        where a.Status == 1
                              && a.NgayTH > tuNgay2
                              && a.NgayTH < denNgay2
                        select c
                       ).ToList();

            var XetNghiem2 = XetNghiem1 - cls2.Where(p => p.IDNhom == 1).Count();
            if (XetNghiem2 > 0)
                dic.Add("XetNghiem2", "+" + XetNghiem2);
            else
                dic.Add("XetNghiem2", XetNghiem2);
            var XQuang2 = XQuang1 - cls2.Where(p => p.IdTieuNhom == 5).Count();
            if (XQuang2 > 0)
                dic.Add("XQuang2", "+" + XQuang2);
            else
                dic.Add("XQuang2", XQuang2);
            var SieuAm2 = SieuAm1 - cls2.Where(p => p.IdTieuNhom == 4).Count();
            if (SieuAm2 > 0)
                dic.Add("SieuAm2", "+" + SieuAm2);
            else
                dic.Add("SieuAm2", SieuAm2);
            var DienTim2 = DienTim1 - cls2.Where(p => p.IdTieuNhom == 15).Count();
            if (DienTim2 > 0)
                dic.Add("DienTim2", "+" + DienTim2);
            else
                dic.Add("DienTim2", DienTim2);
            var NoiSoiTMH2 = NoiSoiTMH1 - cls2.Where(p => p.IdTieuNhom == 98).Count();
            if (NoiSoiTMH2 > 0)
                dic.Add("NoiSoiTMH2", "+" + NoiSoiTMH2);
            else
                dic.Add("NoiSoiTMH2", NoiSoiTMH2);
            var LuuHuyetNao2 = LuuHuyetNao1 - cls2.Where(p => p.IdTieuNhom == 133).Count();
            if (LuuHuyetNao2 > 0)
                dic.Add("LuuHuyetNao2", "+" + LuuHuyetNao2);
            else
                dic.Add("LuuHuyetNao2", LuuHuyetNao2);
            var VLTL_PHCN2 = VLTL_PHCN1 - cls2.Where(p => p.IDNhom == 8).Count();
            if (VLTL_PHCN2 > 0)
                dic.Add("VLTL_PHCN2", "+" + VLTL_PHCN2);
            else
                dic.Add("VLTL_PHCN2", VLTL_PHCN2);

            // So sanh voi cung ky nam truoc
            var bn3 = _data.BenhNhans.Where(p => p.NNhap > tuNgay3 && p.NNhap < denNgay3).ToList();
            var rv3 = (from bn in _data.BenhNhans
                      join rv1 in _data.RaViens.Where(p => p.NgayRa > tuNgay3 && p.NgayRa < denNgay3) on bn.MaBNhan equals rv1.MaBNhan
                      select new { bn, rv1 }).ToList();

            var KBenh3 = KBenh1 - bn3.Count();
            if (KBenh3 > 0)
                dic.Add("KBenh3", "+" + KBenh3);
            else
                dic.Add("KBenh3", KBenh3);

            var KBenhBHYT3 = KBenhBHYT1 - bn3.Where(p => p.DTuong.ToLower().Contains("bhyt")).Count();
            if (KBenhBHYT3 > 0)
                dic.Add("KBenhBHYT3", "+" + KBenhBHYT3);
            else
                dic.Add("KBenhBHYT3", KBenhBHYT3);

            var KBenhBHYTNNgheo3 = KBenhBHYTNNgheo1 - bn3.Where(p => p.DTuong.ToLower().Contains("bhyt") && p.SThe.ToLower().Contains("hn")).Count();
            if (KBenhBHYTNNgheo3 > 0)
                dic.Add("KBenhBHYTNNgheo3", "+" + KBenhBHYTNNgheo3);
            else
                dic.Add("KBenhBHYTNNgheo3", KBenhBHYTNNgheo3);
            var DTtriBHYTNNgheo3 = DTtriBHYTNNgheo1 - bn3.Where(p => p.SThe.ToLower().Contains("hn") && p.NoiTru == 1).Count();
            if (DTtriBHYTNNgheo3 > 0)
                dic.Add("DTtriBHYTNNgheo3", "+" + DTtriBHYTNNgheo3);
            else
                dic.Add("DTtriBHYTNNgheo3", DTtriBHYTNNgheo3);
            var DTNoitru3 = DTNoitru1 - rv3.Where(p => p.bn.NoiTru == 1).Count();
            if (DTNoitru3 > 0)
                dic.Add("DTNoitru3", "+" + DTNoitru3);
            else
                dic.Add("DTNoitru3", DTNoitru3);
            var DTNgoaitru3 = DTNgoaitru1 - bn3.Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
            if (DTNgoaitru3 > 0)
                dic.Add("DTNgoaitru3", "+" + DTNgoaitru3);
            else
                dic.Add("DTNgoaitru3", DTNgoaitru3);

            var ChuyenTuyen3 = ChuyenTuyen1 - (from a in bn3
                                               join b in _data.BNKBs on a.MaBNhan equals b.MaBNhan
                                               where b.PhuongAn == 2
                                               select a
                                              ).Count();
            if (ChuyenTuyen3 > 0)
                dic.Add("ChuyenTuyen3", "+" + ChuyenTuyen3);
            else
                dic.Add("ChuyenTuyen3", ChuyenTuyen3);

            var TongNgayDTNoitru3 = TongNgayDTNoitru1 - (from a in _data.DThuocs
                                                         join b in _data.DThuoccts on a.IDDon equals b.IDDon
                                                         join c in _data.BenhNhans on a.MaBNhan equals c.MaBNhan
                                                         join d in _data.RaViens on a.MaBNhan equals d.MaBNhan
                                                         where c.NoiTru == 1
                                                               && d.NgayRa > tuNgay3
                                                               && d.NgayRa < denNgay3
                                                               && b.DonVi.ToLower().Contains("ngày")
                                                         select b.SoLuong
                                                          ).Count();
            if (TongNgayDTNoitru3 > 0)
                dic.Add("TongNgayDTNoitru3", "+" + TongNgayDTNoitru3);
            else
                dic.Add("TongNgayDTNoitru3", TongNgayDTNoitru3);

            var TuVongTaiCSYT3 = TuVongTaiCSYT1 - (from a in _data.BenhNhans
                                                   join b in _data.RaViens on a.MaBNhan equals b.MaBNhan
                                                   where b.KetQua.ToLower().Contains("tử vong")
                                                         && b.NgayRa > tuNgay3
                                                         && b.NgayRa < denNgay3
                                                   select new { a, b }
                                                  ).Count();
            if (TuVongTaiCSYT3 > 0)
                dic.Add("TuVongTaiCSYT3", "+" + TuVongTaiCSYT3);
            else
                dic.Add("TuVongTaiCSYT3", TuVongTaiCSYT3);


            var cls3 = (from a in _data.CLS
                        join b in _data.ChiDinhs on a.IdCLS equals b.IdCLS
                        join c in _data.DichVus on b.MaDV equals c.MaDV
                        where a.Status == 1
                              && a.NgayTH > tuNgay3
                              && a.NgayTH < denNgay3
                        select c
                       ).ToList();

            var XetNghiem3 = XetNghiem1 - cls3.Where(p => p.IDNhom == 1).Count();
            if (XetNghiem3 > 0)
                dic.Add("XetNghiem3", "+" + XetNghiem3);
            else
                dic.Add("XetNghiem3", XetNghiem3);
            var XQuang3 = XQuang1 - cls3.Where(p => p.IdTieuNhom == 5).Count();
            if (XQuang3 > 0)
                dic.Add("XQuang3", "+" + XQuang3);
            else
                dic.Add("XQuang3", XQuang3);
            var SieuAm3 = SieuAm1 - cls3.Where(p => p.IdTieuNhom == 4).Count();
            if (SieuAm3 > 0)
                dic.Add("SieuAm3", "+" + SieuAm3);
            else
                dic.Add("SieuAm3", SieuAm3);
            var DienTim3 = DienTim1 - cls3.Where(p => p.IdTieuNhom == 15).Count();
            if (DienTim3 > 0)
                dic.Add("DienTim3", "+" + DienTim3);
            else
                dic.Add("DienTim3", DienTim3);
            var NoiSoiTMH3 = NoiSoiTMH1 - cls3.Where(p => p.IdTieuNhom == 98).Count();
            if (NoiSoiTMH3 > 0)
                dic.Add("NoiSoiTMH3", "+" + NoiSoiTMH3);
            else
                dic.Add("NoiSoiTMH3", NoiSoiTMH3);
            var LuuHuyetNao3 = LuuHuyetNao1 - cls3.Where(p => p.IdTieuNhom == 133).Count();
            if (LuuHuyetNao3 > 0)
                dic.Add("LuuHuyetNao3", "+" + LuuHuyetNao3);
            else
                dic.Add("LuuHuyetNao3", LuuHuyetNao3);
            var VLTL_PHCN3 = VLTL_PHCN1 - cls3.Where(p => p.IDNhom == 8).Count();
            if (VLTL_PHCN3 > 0)
                dic.Add("VLTL_PHCN3", "+" + VLTL_PHCN3);
            else
                dic.Add("VLTL_PHCN3", VLTL_PHCN3);


            DungChung.Ham.Print(DungChung.PrintConfig.Rep_BCHoatDongKhamChuaBenh_24012, null, dic, PrintNow);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
