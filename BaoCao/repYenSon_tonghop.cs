using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class repYenSon_tonghop : DevExpress.XtraReports.UI.XtraReport
    {
        public repYenSon_tonghop()
        {
            InitializeComponent();
        }
        private class benhnhan
        {
            private int MaBN;
            private int Phuongan;
            private int IDKB;
            private int Tuoi;
            private string Sothe;
            private string DTuong;
            private string MaKP;
            private string NTT;
            public int maBN
            { get { return MaBN; } set { MaBN = value; } }
            public int phuongan
            { get { return Phuongan; } set { Phuongan = value; } }
            public int idkb
            { get { return IDKB; } set { IDKB = value; } }
            public int tuoi
            { get { return Tuoi; } set { Tuoi = value; } }
            public string sothe
            { get { return Sothe; } set { Sothe = value; } }
            public string dtuong
            { get { return DTuong; } set { DTuong = value; } }
            public string makp
            { get { return MaKP; } set { MaKP = value; } }
            public string ntt
            { get { return NTT; } set { NTT = value; } }
        }
        List<benhnhan> _benhnhan = new List<benhnhan>();

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime NT = DungChung.Ham.NgayTu(Convert.ToDateTime(Ngaytu.Value));
            DateTime ND = DungChung.Ham.NgayDen(Convert.ToDateTime(Ngayden.Value));
            #region  BV Yên Sơn - tính theo ngày khám
            if (DungChung.Bien.MaBV == "08204")
            {
                var TV1 = (from kb in _Data.BNKBs.Where(p => p.NgayKham >= NT).Where(p => p.NgayKham <= ND) group kb by kb.MaBNhan into kq select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();
                foreach (var a in TV1)
                {
                    var q = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == a.Key) select new { bn.MaBNhan, bn.SThe, bn.DTuong, bn.Tuoi }).ToList();
                    if (q.Count > 0)
                    {
                        benhnhan themmoi = new benhnhan();
                        themmoi.maBN = a.Key == null ? 0 : Convert.ToInt32(a.Key);
                        themmoi.idkb = a.IDKB;
                        if (q.First().SThe != null)
                        { themmoi.sothe = q.First().SThe; }
                        if (q.First().Tuoi != null)
                        { themmoi.tuoi = q.First().Tuoi.Value; }
                        var id = (_Data.BNKBs.Where(p => p.IDKB == a.IDKB)).ToList();
                        if (id.Count > 0 && id.First().PhuongAn != null)
                        {
                            themmoi.phuongan = id.First().PhuongAn.Value;
                        }
                        if (q.First().DTuong != null)
                        { themmoi.dtuong = q.First().DTuong; }
                        _benhnhan.Add(themmoi);
                    }
                }


                if (_benhnhan.Count > 0)
                {
                    Tongso1.Value = _benhnhan.Where(p => p.dtuong == "BHYT").Select(p => p.maBN).Count();
                    Tongso2.Value = _benhnhan.Where(p => p.dtuong == "Dịch vụ").Select(p => p.maBN).Count();
                    Tongso3.Value = _benhnhan.Where(p => p.tuoi < 6).Select(p => p.maBN).Count();
                    Vaovien1.Value = _benhnhan.Where(p => p.phuongan == 1).Where(p => p.dtuong == "BHYT").Select(p => p.maBN).Count();
                    Vaovien2.Value = _benhnhan.Where(p => p.phuongan == 1).Where(p => p.dtuong == "Dịch vụ").Select(p => p.maBN).Count();
                    Vaovien3.Value = _benhnhan.Where(p => p.tuoi < 6).Where(p => p.phuongan == 1).Select(p => p.maBN).Count();
                    Chuyenvien1.Value = _benhnhan.Where(p => p.dtuong == "BHYT").Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Chuyenvien2.Value = _benhnhan.Where(p => p.dtuong == "Dịch vụ").Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Chuyenvien3.Value = _benhnhan.Where(p => p.tuoi < 6).Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Hongheo1.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.dtuong == "BHYT").Select(p => p.maBN).Count();
                    Hongheo2.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.dtuong == "Dịch vụ").Select(p => p.maBN).Count();
                    Hongheo3.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.tuoi < 6).Select(p => p.maBN).Count();
                    KSK.Value = _benhnhan.Where(p => p.dtuong.Contains("KSK")).Select(p => p.maBN).Count();
                }
                _benhnhan.Clear();
                foreach (var b in TV1)
                {
                    var q2 = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == b.Key)
                              join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                              join kp in _Data.KPhongs on vp.MaKP equals kp.MaKP
                              select new { bn.MaBNhan, kp.ChuyenKhoa, bn.Tuoi, bn.SThe, bn.DTuong }).ToList();
                    if (q2.Count > 0)
                    {
                        benhnhan themmoi2 = new benhnhan();
                        themmoi2.maBN = q2.First().MaBNhan;
                        themmoi2.idkb = b.IDKB;
                        if (q2.First().SThe != null)
                        {
                            themmoi2.sothe = q2.First().SThe;
                        }
                        if (q2.First().Tuoi != null)
                        {
                            themmoi2.tuoi = q2.First().Tuoi.Value;
                        }
                        if (q2.First().DTuong != null)
                        {
                            themmoi2.dtuong = q2.First().DTuong;
                        }
                        if (q2.First().ChuyenKhoa != null)
                        {
                            themmoi2.makp = q2.First().ChuyenKhoa;
                        }
                        var id = (_Data.BNKBs.Where(p => p.IDKB == b.IDKB)).ToList();
                        if (id.Count > 0 && id.First().PhuongAn != null)
                        {
                            themmoi2.phuongan = id.First().PhuongAn.Value;
                        }
                        _benhnhan.Add(themmoi2);
                    }
                }
                if (_benhnhan.Count > 0)
                {
                    Tongso4.Value = _benhnhan.Where(p => p.makp == "Mắt").Select(p => p.maBN).Count();
                    Tongso5.Value = _benhnhan.Where(p => p.makp == "Răng Hàm Mặt").Select(p => p.maBN).Count();
                    Chuyenvien4.Value = _benhnhan.Where(p => p.makp == "Mắt").Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Chuyenvien5.Value = _benhnhan.Where(p => p.makp == "Răng Hàm Mặt").Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Hongheo4.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.makp == "Mắt").Select(p => p.maBN).Count();
                    Hongheo5.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.makp == "Răng Hàm Mặt").Select(p => p.maBN).Count();
                    BH4.Value = _benhnhan.Where(p => p.makp == "Mắt").Where(p => p.dtuong == "BHYT").Select(p => p.maBN).Count();
                    BH5.Value = _benhnhan.Where(p => p.makp == "Răng Hàm Mặt").Where(p => p.dtuong == "BHYT").Select(p => p.maBN).Count();
                }

                _benhnhan.Clear();
                foreach (var c in TV1)
                {
                    var q3 = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == c.Key)
                              join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                              join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                              join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                              join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                              select new { bn.MaBNhan, tn.TenRG, bn.DTuong, bn.SThe }).ToList();
                    if (q3.Count > 0)
                    {
                        benhnhan themmoi3 = new benhnhan();
                        themmoi3.maBN = q3.First().MaBNhan;
                        themmoi3.idkb = c.IDKB;
                        if (q3.First().TenRG != null)
                        {
                            themmoi3.makp = q3.First().TenRG;
                        }
                        if (q3.First().DTuong != null)
                        {
                            themmoi3.dtuong = q3.First().DTuong;
                        }
                        if (q3.First().SThe != null)
                        {
                            themmoi3.sothe = q3.First().SThe;
                        }
                        var id = (_Data.BNKBs.Where(p => p.IDKB == c.IDKB)).ToList();
                        if (id.Count > 0 && id.First().PhuongAn != null)
                        {
                            themmoi3.phuongan = id.First().PhuongAn.Value;
                        }
                        _benhnhan.Add(themmoi3);
                    }
                }
                if (_benhnhan.Count > 0)
                {
                    Tongso6.Value = _benhnhan.Where(p => p.makp == "Điện Tim").Select(p => p.maBN).Count();
                    Tongso7.Value = _benhnhan.Where(p => p.makp == "Siêu âm").Select(p => p.maBN).Count();
                    Tongso8.Value = _benhnhan.Where(p => p.makp == "Nội soi").Select(p => p.maBN).Count();
                    Tongso9.Value = _benhnhan.Where(p => p.makp == "X-Quang").Select(p => p.maBN).Count();
                    Vaovien6.Value = _benhnhan.Where(p => p.makp == "Điện Tim").Where(p => p.phuongan == 1).Select(p => p.maBN).Count();
                    Vaovien7.Value = _benhnhan.Where(p => p.makp == "Siêu âm").Where(p => p.phuongan == 1).Select(p => p.maBN).Count();
                    Vaovien8.Value = _benhnhan.Where(p => p.makp == "Nội soi").Where(p => p.phuongan == 1).Select(p => p.maBN).Count();
                    Vaovien9.Value = _benhnhan.Where(p => p.makp == "X-Quang").Where(p => p.phuongan == 1).Select(p => p.maBN).Count();
                    Chuyenvien6.Value = _benhnhan.Where(p => p.makp == "Điện Tim").Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Chuyenvien7.Value = _benhnhan.Where(p => p.makp == "Siêu âm").Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Chuyenvien8.Value = _benhnhan.Where(p => p.makp == "Nội soi").Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Chuyenvien9.Value = _benhnhan.Where(p => p.makp == "X-Quang").Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Hongheo6.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.makp == "Điện Tim").Select(p => p.maBN).Count();
                    Hongheo7.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.makp == "Siêu âm").Select(p => p.maBN).Count();
                    Hongheo8.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.makp == "Nội soi").Select(p => p.maBN).Count();
                    Hongheo9.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.makp == "X-Quang").Select(p => p.maBN).Count();
                    BH3.Value = _benhnhan.Where(p => p.dtuong == "BHYT").Where(p => p.makp == "Điện Tim").Select(p => p.maBN).Count();
                    BH4.Value = _benhnhan.Where(p => p.dtuong == "BHYT").Where(p => p.makp == "Siêu âm").Select(p => p.maBN).Count();
                    BH5.Value = _benhnhan.Where(p => p.dtuong == "BHYT").Where(p => p.makp == "Nội soi").Select(p => p.maBN).Count();
                    BH6.Value = _benhnhan.Where(p => p.dtuong == "BHYT").Where(p => p.makp == "X-Quang").Select(p => p.maBN).Count();
                }

                _benhnhan.Clear();
                foreach (var d in TV1)
                {
                    var q3 = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == d.Key)
                              join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                              join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                              join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                              join tn in _Data.NhomDVs on dv.IDNhom equals tn.IDNhom
                              select new { bn.MaBNhan, tn.TenNhomCT, bn.DTuong, bn.SThe }).ToList();
                    if (q3.Count > 0)
                    {
                        benhnhan themmoi4 = new benhnhan();
                        themmoi4.maBN = q3.First().MaBNhan;
                        themmoi4.idkb = d.IDKB;
                        if (q3.First().TenNhomCT != null)
                        {
                            themmoi4.makp = q3.First().TenNhomCT;
                        }
                        if (q3.First().DTuong != null)
                        {
                            themmoi4.dtuong = q3.First().DTuong;
                        }
                        if (q3.First().SThe != null)
                        {
                            themmoi4.sothe = q3.First().SThe;
                        }
                        var id = (_Data.BNKBs.Where(p => p.IDKB == d.IDKB)).ToList();
                        if (id.Count > 0 && id.First().PhuongAn != null)
                        {
                            themmoi4.phuongan = id.First().PhuongAn.Value;
                        }
                        _benhnhan.Add(themmoi4);
                    }
                }
            }
            #endregion
            #region BV khác - tính theo ngày nhập bệnh nhân
            else
            {
                var qbnkb = (from bnkb in _Data.BNKBs select bnkb).ToList();
                var qbn = (from bn in _Data.BenhNhans.Where(p => p.NNhap >= NT && p.NNhap <= ND)
                           join bnkb in _Data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                           group new { bn, bnkb } by new { bn.MaBNhan, bn.SThe, bn.DTuong, bn.Tuoi, bnkb.MaCK } into kq
                           select new { kq.Key.MaBNhan, kq.Key.SThe, kq.Key.DTuong, kq.Key.Tuoi, kq.Key.MaCK, IDKB = kq.Max(p => p.bnkb.IDKB) }).ToList();
                foreach (var bn in qbn)
                {
                    benhnhan themmoi = new benhnhan();
                    themmoi.maBN = bn.MaBNhan;
                    themmoi.idkb = (qbnkb.Where(p => p.MaBNhan == bn.MaBNhan)).FirstOrDefault().IDKB;
                    if (bn.SThe != null)
                    { themmoi.sothe = bn.SThe; }
                    if (bn.Tuoi != null)
                    { themmoi.tuoi = bn.Tuoi.Value; }
                    var id = (qbnkb.Where(p => p.IDKB == themmoi.idkb)).ToList();
                    if (id.Count > 0 && id.First().PhuongAn != null)
                    {
                        themmoi.phuongan = id.First().PhuongAn.Value;
                    }
                    if (bn.DTuong != null)
                    { themmoi.dtuong = bn.DTuong; }
                    _benhnhan.Add(themmoi);
                }
                if (_benhnhan.Count > 0)
                {
                    Tongso1.Value = _benhnhan.Where(p => p.dtuong == "BHYT").Select(p => p.maBN).Count();
                    Tongso2.Value = _benhnhan.Where(p => p.dtuong == "Dịch vụ").Select(p => p.maBN).Count();
                    Tongso3.Value = _benhnhan.Where(p => p.tuoi < 6).Select(p => p.maBN).Count();
                    Vaovien1.Value = _benhnhan.Where(p => p.phuongan == 1).Where(p => p.dtuong == "BHYT").Select(p => p.maBN).Count();
                    Vaovien2.Value = _benhnhan.Where(p => p.phuongan == 1).Where(p => p.dtuong == "Dịch vụ").Select(p => p.maBN).Count();
                    Vaovien3.Value = _benhnhan.Where(p => p.tuoi < 6).Where(p => p.phuongan == 1).Select(p => p.maBN).Count();
                    Chuyenvien1.Value = _benhnhan.Where(p => p.dtuong == "BHYT").Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Chuyenvien2.Value = _benhnhan.Where(p => p.dtuong == "Dịch vụ").Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Chuyenvien3.Value = _benhnhan.Where(p => p.tuoi < 6).Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Hongheo1.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.dtuong == "BHYT").Select(p => p.maBN).Count();
                    Hongheo2.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.dtuong == "Dịch vụ").Select(p => p.maBN).Count();
                    Hongheo3.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.tuoi < 6).Select(p => p.maBN).Count();
                    KSK.Value = _benhnhan.Where(p => p.dtuong.Contains("KSK")).Select(p => p.maBN).Count();
                }
                _benhnhan.Clear();
                var q2_1 = (from a in qbn
                            join bn in _Data.BenhNhans on a.MaBNhan equals bn.MaBNhan
                            join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                            //join kp in _Data.KPhongs on vp.MaKP equals kp.MaKP
                            join ck in _Data.ChuyenKhoas on a.MaCK equals ck.MaCK
                            select new { bn.MaBNhan, ChuyenKhoa = ck.TenCK, bn.Tuoi, bn.SThe, bn.DTuong }).ToList();
                if (q2_1.Count > 0)
                {
                    foreach (var b in q2_1)
                    {
                        //var q2 = (from a in q2_1.Where(p => p.MaBNhan == b.MaBNhan) select a).ToList();
                        //if (q2.Count > 0)
                        //{
                        benhnhan themmoi2 = new benhnhan();
                        themmoi2.maBN = b.MaBNhan;
                        themmoi2.idkb = (qbnkb.Where(p => p.MaBNhan == b.MaBNhan)).FirstOrDefault().IDKB;
                        if (b.SThe != null)
                        {
                            themmoi2.sothe = b.SThe;
                        }
                        if (b.Tuoi != null)
                        {
                            themmoi2.tuoi = b.Tuoi.Value;
                        }
                        if (b.DTuong != null)
                        {
                            themmoi2.dtuong = b.DTuong;
                        }
                        if (b.ChuyenKhoa != null)
                        {
                            themmoi2.makp = b.ChuyenKhoa;
                        }
                        var id = (qbnkb.Where(p => p.IDKB == themmoi2.idkb)).ToList();
                        if (id.Count > 0 && id.First().PhuongAn != null)
                        {
                            themmoi2.phuongan = id.First().PhuongAn.Value;
                        }
                        _benhnhan.Add(themmoi2);
                        //}
                    }
                }
                if (_benhnhan.Count > 0)
                {
                    Tongso4.Value = _benhnhan.Where(p => p.makp.Contains("Mắt")).Select(p => p.maBN).Count();
                    Tongso5.Value = _benhnhan.Where(p => p.makp.Contains("Răng Hàm Mặt")).Select(p => p.maBN).Count();
                    Chuyenvien4.Value = _benhnhan.Where(p => p.makp.Contains("Mắt")).Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Chuyenvien5.Value = _benhnhan.Where(p => p.makp.Contains("Răng Hàm Mặt")).Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Hongheo4.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.makp.Contains("Mắt")).Select(p => p.maBN).Count();
                    Hongheo5.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.makp.Contains("Răng Hàm Mặt")).Select(p => p.maBN).Count();
                    BH4.Value = _benhnhan.Where(p => p.makp.Contains("Mắt")).Where(p => p.dtuong.Equals("BHYT")).Select(p => p.maBN).Count();
                    BH5.Value = _benhnhan.Where(p => p.makp.Contains("Răng Hàm Mặt")).Where(p => p.dtuong.Equals("BHYT")).Select(p => p.maBN).Count();
                }

                _benhnhan.Clear();
                var q3_1 = (from a in qbn
                            join bn in _Data.BenhNhans on a.MaBNhan equals bn.MaBNhan
                            join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                            join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                            join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                            join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            group new { bn, tn } by new { bn.MaBNhan, tn.TenRG, bn.DTuong, bn.SThe } into kq
                            select new { kq.Key.MaBNhan, kq.Key.TenRG, kq.Key.DTuong, kq.Key.SThe }).ToList();
                if (q3_1.Count > 0)
                {
                    foreach (var c in q3_1)
                    {
                        //var q3 = (from a in q3_1.Where(p => p.MaBNhan == c.MaBNhan) select a).ToList();
                        //if (q3.Count > 0)
                        //{
                        benhnhan themmoi3 = new benhnhan();
                        themmoi3.maBN = c.MaBNhan;
                        themmoi3.idkb = (qbnkb.Where(p => p.MaBNhan == c.MaBNhan)).FirstOrDefault().IDKB;
                        if (c.TenRG != null)
                        {
                            themmoi3.makp = c.TenRG;
                        }
                        if (c.DTuong != null)
                        {
                            themmoi3.dtuong = c.DTuong;
                        }
                        if (c.SThe != null)
                        {
                            themmoi3.sothe = c.SThe;
                        }
                        var id = (qbnkb.Where(p => p.IDKB == themmoi3.idkb)).ToList();
                        if (id.Count > 0 && id.First().PhuongAn != null)
                        {
                            themmoi3.phuongan = id.First().PhuongAn.Value;
                        }
                        _benhnhan.Add(themmoi3);
                        //}
                    }
                }
                if (_benhnhan.Count > 0)
                {
                    Tongso6.Value = _benhnhan.Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Select(p => p.maBN).Count();
                    Tongso7.Value = _benhnhan.Where(p => p.makp.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Select(p => p.maBN).Count();
                    Tongso8.Value = _benhnhan.Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi)).Select(p => p.maBN).Count();
                    Tongso9.Value = _benhnhan.Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Select(p => p.maBN).Count();
                    Vaovien6.Value = _benhnhan.Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Where(p => p.phuongan == 1).Select(p => p.maBN).Count();
                    Vaovien7.Value = _benhnhan.Where(p => p.makp.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Where(p => p.phuongan == 1).Select(p => p.maBN).Count();
                    Vaovien8.Value = _benhnhan.Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi)).Where(p => p.phuongan == 1).Select(p => p.maBN).Count();
                    Vaovien9.Value = _benhnhan.Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Where(p => p.phuongan == 1).Select(p => p.maBN).Count();
                    Chuyenvien6.Value = _benhnhan.Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Chuyenvien7.Value = _benhnhan.Where(p => p.makp.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Chuyenvien8.Value = _benhnhan.Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi)).Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Chuyenvien9.Value = _benhnhan.Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                    Hongheo6.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Select(p => p.maBN).Count();
                    Hongheo7.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.makp.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Select(p => p.maBN).Count();
                    Hongheo8.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi)).Select(p => p.maBN).Count();
                    Hongheo9.Value = _benhnhan.Where(p => p.sothe.Contains("HN")).Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Select(p => p.maBN).Count();
                    BH3.Value = _benhnhan.Where(p => p.dtuong == "BHYT").Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Select(p => p.maBN).Count();
                    BH4.Value = _benhnhan.Where(p => p.dtuong == "BHYT").Where(p => p.makp.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Select(p => p.maBN).Count();
                    BH5.Value = _benhnhan.Where(p => p.dtuong == "BHYT").Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi)).Select(p => p.maBN).Count();
                    BH6.Value = _benhnhan.Where(p => p.dtuong == "BHYT").Where(p => p.makp.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Select(p => p.maBN).Count();
                }

                _benhnhan.Clear();
                var q3_2 = (from a in qbn
                            join bn in _Data.BenhNhans on a.MaBNhan equals bn.MaBNhan
                            join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                            join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                            join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                            join tn in _Data.NhomDVs on dv.IDNhom equals tn.IDNhom
                            select new { bn.MaBNhan, tn.TenNhomCT, bn.DTuong, bn.SThe }).ToList();
                if (q3_2.Count > 0)
                {
                    foreach (var d in q3_2)
                    {
                        //var q3 = (from a in q3_2.Where(p => p.MaBNhan == d.MaBNhan) select a).ToList();
                        //if (q3.Count > 0)
                        //{
                        benhnhan themmoi4 = new benhnhan();
                        themmoi4.maBN = d.MaBNhan;
                        themmoi4.idkb = (qbnkb.Where(p => p.MaBNhan == d.MaBNhan)).FirstOrDefault().IDKB;
                        if (d.TenNhomCT != null)
                        {
                            themmoi4.makp = d.TenNhomCT;
                        }
                        if (d.DTuong != null)
                        {
                            themmoi4.dtuong = d.DTuong;
                        }
                        if (d.SThe != null)
                        {
                            themmoi4.sothe = d.SThe;
                        }
                        var id = (qbnkb.Where(p => p.IDKB == themmoi4.idkb)).ToList();
                        if (id.Count > 0 && id.First().PhuongAn != null)
                        {
                            themmoi4.phuongan = id.First().PhuongAn.Value;
                        }
                        _benhnhan.Add(themmoi4);
                        //}
                    }
                }
            }
            #endregion

            if (_benhnhan.Count > 0)
            {
                Tongso10.Value = _benhnhan.Where(p => p.makp.ToLower().Contains("xét nghiệm")).Select(p => p.maBN).Count();
                Vaovien10.Value = _benhnhan.Where(p => p.makp.ToLower().Contains("xét nghiệm")).Where(p => p.phuongan == 1).Select(p => p.maBN).Count();
                Chuyenvien10.Value = _benhnhan.Where(p => p.makp.ToLower().Contains("xét nghiệm")).Where(p => p.phuongan == 2).Select(p => p.maBN).Count();
                Hongheo10.Value = _benhnhan.Where(p => p.makp.ToLower().Contains("xét nghiệm")).Where(p => p.sothe.Contains("HN")).Select(p => p.maBN).Count();
                BH7.Value = _benhnhan.Where(p => p.makp.ToLower().Contains("xét nghiệm")).Where(p => p.dtuong == "BHYT").Select(p => p.maBN).Count();
            }

            txtbv.Text = DungChung.Bien.TenCQ;
            txtCQCQ.Text = DungChung.Bien.TenCQCQ;
            Ngaythang.Text = "Từ ngày " + NT.ToString().Substring(0, 10) + "  đến ngày " + ND.ToString().Substring(0, 10);

        }

    }
}
