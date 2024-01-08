using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    
    public partial class Frm_BcHoatDongKKB_HL01 : DevExpress.XtraEditors.XtraForm 
    {
        public Frm_BcHoatDongKKB_HL01()
        {
            InitializeComponent();
        }
        public class dulieu
        {
            string tS1, tS2, tS3, tS4, tS60, tSNu, bHYT1, bHYT2, bHYT3, bHYT4, bHYT60, bHYTNu, vP1, vP2, vP3, vP4, vP60, vPNu, cC1, cC2, cC3, cC4, cC60, cCNu;
            string vV1, vV2, vV3, vV4, vV60, vVNu, cV1, cV2, cV3, cV4, cV60, cVNu, bHNN;

            public string BHNN
            {
                get { return bHNN; }
                set { bHNN = value; }
            }
            public string CV60
            {
                get { return cV60; }
                set { cV60 = value; }
            }
            public string VV60
            {
                get { return vV60; }
                set { vV60 = value; }
            }
            public string CC60
            {
                get { return cC60; }
                set { cC60 = value; }
            }
            public string CCNu
            {
                get { return cCNu; }
                set { cCNu = value; }
            }
            public string VP60
            {
                get { return vP60; }
                set { vP60 = value; }
            }
            public string VPNu
            {
                get { return vPNu; }
                set { vPNu = value; }
            }
            public string BHYT60
            {
                get { return bHYT60; }
                set { bHYT60 = value; }
            }
            public string BHYTNu
            {
                get { return bHYTNu; }
                set { bHYTNu = value; }
            }
            public string TS60
            {
                get { return tS60; }
                set { tS60 = value; }
            }
            public string TSNu
            {
                get { return tSNu; }
                set { tSNu = value; }
            }
            public string CVNu
            {
                get { return cVNu; }
                set { cVNu = value; }
            }
            public string CV4
            {
                get { return cV4; }
                set { cV4 = value; }
            }
            public string CV3
            {
                get { return cV3; }
                set { cV3 = value; }
            }
            public string CV2
            {
                get { return cV2; }
                set { cV2 = value; }
            }
            public string CV1
            {
                get { return cV1; }
                set { cV1 = value; }
            }
            public string VVNu
            {
                get { return vVNu; }
                set { vVNu = value; }
            }
            public string VV4
            {
                get { return vV4; }
                set { vV4 = value; }
            }
            public string VV3
            {
                get { return vV3; }
                set { vV3 = value; }
            }
            public string VV2
            {
                get { return vV2; }
                set { vV2 = value; }
            }
            public string VV1
            {
                get { return vV1; }
                set { vV1 = value; }
            }
            public string CC4
            {
                get { return cC4; }
                set { cC4 = value; }
            }
            public string CC3
            {
                get { return cC3; }
                set { cC3 = value; }
            }
            public string CC2
            {
                get { return cC2; }
                set { cC2 = value; }
            }
            public string CC1
            {
                get { return cC1; }
                set { cC1 = value; }
            }
            public string VP4
            {
                get { return vP4; }
                set { vP4 = value; }
            }
            public string VP3
            {
                get { return vP3; }
                set { vP3 = value; }
            }
            public string VP2
            {
                get { return vP2; }
                set { vP2 = value; }
            }
            public string VP1
            {
                get { return vP1; }
                set { vP1 = value; }
            }
            public string BHYT4
            {
                get { return bHYT4; }
                set { bHYT4 = value; }
            }
            public string BHYT3
            {
                get { return bHYT3; }
                set { bHYT3 = value; }
            }
            public string BHYT2
            {
                get { return bHYT2; }
                set { bHYT2 = value; }
            }
            public string BHYT1
            {
                get { return bHYT1; }
                set { bHYT1 = value; }
            }
            public string TS4
            {
                get { return tS4; }
                set { tS4 = value; }
            }
            public string TS3
            {
                get { return tS3; }
                set { tS3 = value; }
            }
            public string TS2
            {
                get { return tS2; }
                set { tS2 = value; }
            }
            public string TS1
            {
                get { return tS1; }
                set { tS1 = value; }
            }
        }
        List<dulieu> _dulieu = new List<dulieu>();
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {
            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }
        private string theoquy()
        {
            string quy = "";

            if (ckQuy.Checked == true)
            {
                switch (timquy(lupTuNgay.DateTime.Month))
                {
                    case 1:
                        quy = " Qúy I Năm 2014";
                        break;
                    case 2:
                        quy = " Qúy II Năm 2014 ";
                        break;
                    case 3:
                        quy = " Qúy III Năm 2014";
                        break;
                    case 4:
                        quy = " Qúy IV Năm 2014";
                        break;
                }

            }
            else
            {
                quy = "Từ ngày  " + lupTuNgay.DateTime.ToString().Substring(0, 10) + "  đến ngày  " + lupDenNgay.DateTime.ToString().Substring(0, 10);
            }
            return quy;
        }

        private int timquy(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return (1);
            }
            if (month > 3 && month <= 6)
            {
                return (2);
            }
            if (month > 6 && month <= 9)
            {
                return (3);
            }
            else { return (4); }
        }
        private class KPhong
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

        List<KPhong> _Kphong = new List<KPhong>();

        private void Frm_BcHoatDongKKB_HL01_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;

            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang)
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;

            if (KTtaoBc())
            {
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                BaoCao.Rep_BcHoatDongKKB_HL01 rep = new BaoCao.Rep_BcHoatDongKKB_HL01();
                rep.TuNgay.Value = lupTuNgay.Text;
                rep.DenNgay.Value = lupDenNgay.Text;
                if (ckQuy.Checked == true)
                {
                    rep.TG.Value = theoquy();
                }
                else rep.TG.Value = theoquy();
                List<KPhong> kphong = new List<KPhong>();
                kphong = _Kphong.Where(p => p.chon == true).Where(p => p.makp > 0).ToList();
                #region tạm bỏ
                //int _MaKP1 = 0;
                //int _MaKP2 = 0;
                //int _MaKP3 = 0;
                //int _MaKP4 = 0;
                //int _MaKP5 = 0;
                //int _MaKP6 = 0;
                //int _MaKP7 = 0;
                //int _MaKP8 = 0;
                //int _MaKP9 = 0;
                //int _MaKP10 = 0;
                //int _MaKP11 = 0;
                //int _MaKP12 = 0;
                //int _MaKP13 = 0;
                //int _MaKP14 = 0;
                //int _MaKP15 = 0;
                //int _MaKP16 = 0;
                //int _MaKP17 = 0;
                //int _MaKP18 = 0;
                //int _MaKP19 = 0;
                //int _MaKP20 = 0;

                //for (int i = 0; i < _Kphong.Count; i++)
                //{
                //    if (_Kphong.Skip(i).First().chon == true)
                //    {
                //        switch (i)
                //        {
                //            case 0:
                //                _MaKP1 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 1:
                //                _MaKP2 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 2:
                //                _MaKP3 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 3:
                //                _MaKP4 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 4:
                //                _MaKP5 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 5:
                //                _MaKP6 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 6:
                //                _MaKP7 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 7:
                //                _MaKP8 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 8:
                //                _MaKP9 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 9:
                //                _MaKP10 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 10:
                //                _MaKP11 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 11:
                //                _MaKP12 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 12:
                //                _MaKP13 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 13:
                //                _MaKP14 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 14:
                //                _MaKP15 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 15:
                //                _MaKP16 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 16:
                //                _MaKP17 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 17:
                //                _MaKP18 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 18:
                //                _MaKP19 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 19:
                //                _MaKP20 = _Kphong.Skip(i).First().makp;
                //                break;

                //        }
                //    }
                //}
                #endregion
                dulieu item = new dulieu();
                _dulieu.Clear();
                #region theo độ tuổi
                if (rdgMauIn.SelectedIndex == 0)
                {
                    var qbn2 = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay)
                                join bn in data.BenhNhans.Where(p => p.DTuong == "BHYT" || p.DTuong == "Dịch vụ") on kb.MaBNhan equals bn.MaBNhan
                                group new { kb } by new { bn.MaBNhan, bn.SThe, bn.Tuoi, bn.DTuong, bn.CapCuu, kb.MaKP, bn.NoiTru, bn.GTinh } into kq
                                select new
                                {
                                    MaBNhan = kq.Key.MaBNhan,
                                    SThe = kq.Key.SThe,
                                    Tuoi = kq.Key.Tuoi,
                                    DTuong = kq.Key.DTuong,
                                    CapCuu = kq.Key.CapCuu,
                                    MaKP = kq.Key.MaKP,
                                    NoiTru = kq.Key.NoiTru,
                                    GTinh = kq.Key.GTinh
                                }).OrderBy(p => p.MaBNhan).ToList();
                    var qbn = (from p in qbn2
                               join k in kphong on p.MaKP equals k.makp
                               select new
                               {
                                   MaBNhan = p.MaBNhan,
                                   SThe = p.SThe,
                                   Tuoi = p.Tuoi,
                                   DTuong = p.DTuong,
                                   CapCuu = p.CapCuu,
                                   MaKP = p.MaKP,
                                   NoiTru = p.NoiTru,
                                   GTinh = p.GTinh
                               }).ToList();
                    if (radioGroup2.SelectedIndex == 1)
                    {
                        var id2 = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay)
                                   join bn in data.BenhNhans.Where(p => p.DTuong == "BHYT" || p.DTuong == "Dịch vụ") on kb.MaBNhan equals bn.MaBNhan
                                   group kb by new { kb.MaBNhan, bn.SThe, bn.Tuoi, bn.DTuong, bn.CapCuu, bn.NoiTru, bn.GTinh } into kq
                                   select new { kq.Key.MaBNhan, kq.Key.SThe, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.CapCuu, kq.Key.NoiTru, IDKB = kq.Max(p => p.IDKB), kq.Key.GTinh }).ToList();
                        qbn = (from p in id2
                               join a in data.BNKBs on p.IDKB equals a.IDKB
                               join b in kphong on a.MaKP equals b.makp
                               select new
                               {
                                   MaBNhan = p.MaBNhan ?? 0,
                                   SThe = p.SThe,
                                   Tuoi = p.Tuoi,
                                   DTuong = p.DTuong,
                                   CapCuu = p.CapCuu,
                                   MaKP = a.MaKP,
                                   NoiTru = p.NoiTru,
                                   GTinh = p.GTinh
                               }).ToList();
                    }
                        
                    if (qbn.Count > 0)
                    {
                        int n = -1;
                        if (DungChung.Bien.MaBV == "27021")
                        {
                            n = 0;
                        }
                        if (qbn.Where(p => p.NoiTru == n||n==-1).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.TS1 = qbn.Where(p => p.NoiTru == n||n==-1).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.TS1 = " "; }
                        if (qbn.Where(p => p.Tuoi > 6).Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi <= 15).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.TS2 = qbn.Where(p => p.Tuoi > 6).Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi <= 15).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.TS2 = " "; }
                        if (qbn.Where(p => p.Tuoi <= 6).Where(p => p.NoiTru == n||n==-1).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.TS3 = qbn.Where(p => p.Tuoi <= 6).Where(p => p.NoiTru == n||n==-1).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.TS3 = " "; }
                        if (qbn.Where(p => p.Tuoi > 15 && p.Tuoi < 60).Where(p => p.NoiTru == n||n==-1).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.TS4 = qbn.Where(p => p.Tuoi > 15 && p.Tuoi < 60).Where(p => p.NoiTru == n||n==-1).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.TS4 = " "; }
                        if (qbn.Where(p => p.Tuoi >= 60).Where(p => p.NoiTru == n||n==-1).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.TS60 = qbn.Where(p => p.Tuoi >= 60).Where(p => p.NoiTru == n||n==-1).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.TS60 = " "; }
                        if (qbn.Where(p => p.GTinh == 0).Where(p => p.NoiTru == n || n == -1).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.TSNu = qbn.Where(p => p.GTinh == 0).Where(p => p.NoiTru == n || n == -1).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.TSNu = " "; }

                        if (qbn.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == n||n==-1).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.BHYT1 = qbn.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == n||n==-1).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.BHYT1 = " "; }
                        if (qbn.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi > 6).Where(p => p.Tuoi <= 15).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.BHYT2 = qbn.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi > 6).Where(p => p.Tuoi <= 15).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.BHYT2 = " "; }
                        if (qbn.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.BHYT3 = qbn.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.BHYT3 = " "; }
                        if (qbn.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi > 15 && p.Tuoi < 60).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.BHYT4 = qbn.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi > 15 && p.Tuoi < 60).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.BHYT4 = " "; }
                        if (qbn.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 60).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.BHYT60 = qbn.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi >= 60).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.BHYT60 = " "; }
                        if (qbn.Where(p => p.DTuong == "BHYT").Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.BHYTNu = qbn.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == n || n == -1).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.BHYTNu = " "; }


                        if (qbn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NoiTru == n||n==-1).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.VP1 = qbn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NoiTru == n||n==-1).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.VP1 = " "; }
                        if (qbn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.Tuoi > 6).Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi <= 15).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.VP2 = qbn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi > 6).Where(p => p.Tuoi <= 15).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.VP2 = " "; }
                        if (qbn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.VP3 = qbn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.VP3 = " "; }
                        if (qbn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi >= 60).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.VP60 = qbn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi >= 60).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.VP60 = " "; }
                        if (qbn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi > 15 && p.Tuoi < 60).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.VP4 = qbn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NoiTru == n||n==-1).Where(p => p.Tuoi > 15 && p.Tuoi < 60).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.VP4 = " "; }
                        if (qbn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NoiTru == n || n == -1).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.VPNu = qbn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NoiTru == n || n == -1).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.VPNu = " "; }


                        if (qbn.Where(p => p.CapCuu == 1).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.CC1 = qbn.Where(p => p.CapCuu == 1).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.CC1 = " "; }
                        if (qbn.Where(p => p.CapCuu == 1).Where(p => p.Tuoi > 6).Where(p => p.Tuoi <= 15).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.CC2 = qbn.Where(p => p.CapCuu == 1).Where(p => p.Tuoi > 6).Where(p => p.Tuoi <= 15).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.CC2 = " "; }
                        if (qbn.Where(p => p.CapCuu == 1).Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.CC3 = qbn.Where(p => p.CapCuu == 1).Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.CC3 = " "; }
                        if (qbn.Where(p => p.CapCuu == 1).Where(p => p.Tuoi > 15 && p.Tuoi<60).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.CC4 = qbn.Where(p => p.CapCuu == 1).Where(p => p.Tuoi > 15 && p.Tuoi < 60).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.CC4 = " "; }
                        if (qbn.Where(p => p.CapCuu == 1).Where(p => p.Tuoi >= 60).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.CC60 = qbn.Where(p => p.CapCuu == 1).Where(p => p.Tuoi >= 60).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.CC60 = " "; }
                        if (qbn.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.CCNu = qbn.Where(p => p.CapCuu == 1).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.CCNu = " "; }

                        if (qbn.Select(p => p.SThe.Contains("HN")).ToString().Count() > 0)
                        {
                            item.BHNN = qbn.Where(p => p.SThe.Contains("HN")).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.BHNN = "........................................."; }
                    }
                    #region bệnh nhân vv, cc, chuyển viện
                    var qvv = (from k in kphong
                               join p in data.BNKBs.Where(p => p.PhuongAn == 1).Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay) on k.makp equals p.MaKP
                               join bn in data.BenhNhans.Where(p => p.DTuong == "BHYT" || p.DTuong == "Dịch vụ") on p.MaBNhan equals bn.MaBNhan
                               select new { p.MaBNhan, bn.Tuoi, bn.NoiTru, bn.GTinh }).ToList();
                    if (radioGroup2.SelectedIndex == 1)
                    {
                        var id2 = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay).Where(p => p.PhuongAn == 1)
                                   join bn in data.BenhNhans.Where(p => p.DTuong == "BHYT" || p.DTuong == "Dịch vụ") on kb.MaBNhan equals bn.MaBNhan
                                   group kb by new { kb.MaBNhan, bn.SThe, bn.Tuoi, bn.DTuong, bn.CapCuu, bn.NoiTru, bn.GTinh } into kq
                                   select new { kq.Key.MaBNhan, kq.Key.SThe, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.CapCuu, kq.Key.NoiTru, IDKB = kq.Max(p => p.IDKB), kq.Key.GTinh }).ToList();
                        qvv = (from p in id2
                               join a in data.BNKBs on p.IDKB equals a.IDKB
                               join b in kphong on a.MaKP equals b.makp
                               select new
                               {
                                   MaBNhan = p.MaBNhan,
                                   Tuoi = p.Tuoi,
                                   NoiTru = p.NoiTru,
                                   GTinh = p.GTinh
                               }).ToList();
                    }
                    if(DungChung.Bien.MaBV == "27021")
                    {
                        qvv = qvv.Where(p => p.NoiTru == 1).ToList();
                    }
                    if (qvv.Count > 0)
                    {
                        if (qvv.Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.VV1 = qvv.Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.VV1 = " "; }
                        if (qvv.Where(p => p.Tuoi > 6).Where(p => p.Tuoi <= 15).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.VV2 = qvv.Where(p => p.Tuoi > 6).Where(p => p.Tuoi <= 15).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.VV2 = " "; }
                        if (qvv.Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.VV3 = qvv.Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.VV3 = " "; }
                        if (qvv.Where(p => p.Tuoi > 15&&p.Tuoi<60).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.VV4 = qvv.Where(p => p.Tuoi > 15 && p.Tuoi < 60).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.VV4 = " "; }
                        if (qvv.Where(p => p.Tuoi >= 60).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.VV60 = qvv.Where(p => p.Tuoi >= 60).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.VV60 = " "; }
                        if (qvv.Where(p => p.GTinh==0).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.VVNu = qvv.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.VVNu = " "; }
                    }

                    var qcv = (from k in kphong
                               //join p in data.BNKBs.Where(p => p.PhuongAn == 2).Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay) on k.makp equals p.MaKP
                               join p in data.RaViens.Where(p=>p.Status==1).Where(p=>p.NgayRa>=tungay && p.NgayRa<=denngay) on k.makp equals p.MaKP
                               join bn in data.BenhNhans.Where(p => p.DTuong == "BHYT" || p.DTuong == "Dịch vụ") on p.MaBNhan equals bn.MaBNhan
                               select new { p.MaBNhan, bn.Tuoi, bn.NoiTru, bn.GTinh }).Distinct().ToList();
                    //if(DungChung.Bien.MaBV == "27021")
                    //{
                    //    qcv = qcv.Where(p => p.NoiTru == 0).ToList();
                    //}
                    if (qcv.Count > 0)
                    {

                        if (qcv.Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.CV1 = qcv.Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.CV1 = " "; }
                        if (qcv.Where(p => p.Tuoi > 6).Where(p => p.Tuoi <= 15).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.CV2 = qcv.Where(p => p.Tuoi > 6).Where(p => p.Tuoi <= 15).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.CV2 = " "; }
                        if (qcv.Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.CV3 = qcv.Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.CV3 = " "; }
                        if (qcv.Where(p => p.Tuoi > 15 && p.Tuoi<60).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.CV4 = qcv.Where(p => p.Tuoi > 15 && p.Tuoi < 60).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.CV4 = " "; }
                        if (qcv.Where(p => p.Tuoi >= 60).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.CV60 = qcv.Where(p => p.Tuoi >= 60).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.CV60 = " "; }
                        if (qcv.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count() > 0)
                        {
                            item.CVNu = qcv.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count().ToString();
                        }
                        else { item.CVNu = " "; }
                    }
                    #endregion
                    #region gán dữ liệu
                    _dulieu.Add(item);
                    rep.TS1.Value = _dulieu.First().TS1;
                    rep.TS2.Value = _dulieu.First().TS2;
                    rep.TS3.Value = _dulieu.First().TS3;
                    rep.TS4.Value = _dulieu.First().TS4;
                    rep.TS60.Value = _dulieu.First().TS60;
                    rep.TSNu.Value = _dulieu.First().TSNu;

                    rep.BHYT1.Value = _dulieu.First().BHYT1;
                    rep.BHYT2.Value = _dulieu.First().BHYT2;
                    rep.BHYT3.Value = _dulieu.First().BHYT3;
                    rep.BHYT4.Value = _dulieu.First().BHYT4;
                    rep.BHYT60.Value = _dulieu.First().BHYT60;
                    rep.BHYTNu.Value = _dulieu.First().BHYTNu;

                    rep.VP1.Value = _dulieu.First().VP1;
                    rep.VP2.Value = _dulieu.First().VP2;
                    rep.VP3.Value = _dulieu.First().VP3;
                    rep.VP4.Value = _dulieu.First().VP4;
                    rep.VP60.Value = _dulieu.First().VP60;
                    rep.VPNu.Value = _dulieu.First().VPNu;

                    rep.CC1.Value = _dulieu.First().CC1;
                    rep.CC2.Value = _dulieu.First().CC2;
                    rep.CC3.Value = _dulieu.First().CC3;
                    rep.CC4.Value = _dulieu.First().CC4;
                    rep.CC60.Value = _dulieu.First().CC60;
                    rep.CCNu.Value = _dulieu.First().CCNu;

                    rep.VV1.Value = _dulieu.First().VV1;
                    rep.VV2.Value = _dulieu.First().VV2;
                    rep.VV3.Value = _dulieu.First().VV3;
                    rep.VV4.Value = _dulieu.First().VV4;
                    rep.VV60.Value = _dulieu.First().VV60;
                    rep.VVNu.Value = _dulieu.First().VVNu;

                    rep.CV1.Value = _dulieu.First().CV1;
                    rep.CV2.Value = _dulieu.First().CV2;
                    rep.CV3.Value = _dulieu.First().CV3;
                    rep.CV4.Value = _dulieu.First().CV4;
                    rep.CV60.Value = _dulieu.First().CV60;
                    rep.CVNu.Value = _dulieu.First().CVNu;

                    rep.BHYTnn.Value = _dulieu.First().BHNN;
                    #endregion
                    #region xuat Excel
                    string _ntn = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text; 
                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@"};
                    string[] _tieude = { "STT", "Nội dung", "Tổng số lần khám", "Số lần khám BHYT", "Số lần khám viện phí", "Số lần khám không thu được", "Số bệnh nhân cấp cứu", "Số bệnh nhân vào viện", "Số bệnh nhân chuyển viện " };
                    int[] _arrWidth = new int[] { };// { 5, 15, 10, 10, 10, 10, 10, 10, 10, 10};

                    DungChung.Bien.MangHaiChieu = new Object[_dulieu.Count + 18, 15];
                    DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.TenCQ.ToUpper();
                    DungChung.Bien.MangHaiChieu[2, 2] = ("BÁO CÁO HOẠT ĐỘNG KHOA KHÁM BỆNH").ToUpper();
                    DungChung.Bien.MangHaiChieu[3, 2] = _ntn;

                    DungChung.Bien.MangHaiChieu[11, 7] = "Ngày ...... tháng ..... năm .....";
                    DungChung.Bien.MangHaiChieu[12, 1] = ("Người lập biểu").ToUpper();
                    DungChung.Bien.MangHaiChieu[16, 1] = DungChung.Bien.NguoiLapBieu;
                    DungChung.Bien.MangHaiChieu[12, 4] = ("Trưởng hoặc phó khoa").ToUpper();
                    DungChung.Bien.MangHaiChieu[16, 4] = DungChung.Bien.GiamDoc;

                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[5, i] = _tieude[i];
                    }

                    DungChung.Bien.MangHaiChieu[6, 0] = 1;
                    DungChung.Bien.MangHaiChieu[6, 2] = "Tổng số lần khám bệnh chung";
                    DungChung.Bien.MangHaiChieu[7, 2] = "Trong đó:";
                    DungChung.Bien.MangHaiChieu[8, 2] = "- Trẻ em 6 - 15 tuổi";
                    DungChung.Bien.MangHaiChieu[9, 2] = "- Dưới 6 tuổi";
                    DungChung.Bien.MangHaiChieu[10, 2] = "- Trên 60 tuổi";
                    DungChung.Bien.MangHaiChieu[11, 2] = "- Khác";

                    DungChung.Bien.MangHaiChieu[6, 3] = _dulieu.First().TS1;
                    DungChung.Bien.MangHaiChieu[7, 3] = "";
                    DungChung.Bien.MangHaiChieu[8, 3] = _dulieu.First().TS2;
                    DungChung.Bien.MangHaiChieu[9, 3] = _dulieu.First().TS3;
                    DungChung.Bien.MangHaiChieu[10, 3] = _dulieu.First().TS60;
                    DungChung.Bien.MangHaiChieu[11, 3] = _dulieu.First().TS4;

                    DungChung.Bien.MangHaiChieu[6, 4] = _dulieu.First().BHYT1;
                    DungChung.Bien.MangHaiChieu[7, 4] = "";
                    DungChung.Bien.MangHaiChieu[8, 4] = _dulieu.First().BHYT2;
                    DungChung.Bien.MangHaiChieu[9, 4] = _dulieu.First().BHYT3;
                    DungChung.Bien.MangHaiChieu[10, 4] = _dulieu.First().BHYT60;
                    DungChung.Bien.MangHaiChieu[11, 4] = _dulieu.First().BHYT4;

                    DungChung.Bien.MangHaiChieu[6, 5] = _dulieu.First().VP1;
                    DungChung.Bien.MangHaiChieu[7, 5] = "";
                    DungChung.Bien.MangHaiChieu[8, 5] = _dulieu.First().VP2;
                    DungChung.Bien.MangHaiChieu[9, 5] = _dulieu.First().VP3;
                    DungChung.Bien.MangHaiChieu[10, 5] = _dulieu.First().VP60;
                    DungChung.Bien.MangHaiChieu[11, 5] = _dulieu.First().VP4;

                    DungChung.Bien.MangHaiChieu[6, 6] = _dulieu.First().CC1;
                    DungChung.Bien.MangHaiChieu[7, 6] = "";
                    DungChung.Bien.MangHaiChieu[8, 6] = _dulieu.First().CC2;
                    DungChung.Bien.MangHaiChieu[9, 6] = _dulieu.First().CC3;
                    DungChung.Bien.MangHaiChieu[10, 6] = _dulieu.First().CC60;
                    DungChung.Bien.MangHaiChieu[11, 6] = _dulieu.First().CC4;

                    DungChung.Bien.MangHaiChieu[6, 7] = _dulieu.First().VV1;
                    DungChung.Bien.MangHaiChieu[7, 7] = "";
                    DungChung.Bien.MangHaiChieu[8, 7] = _dulieu.First().VV2;
                    DungChung.Bien.MangHaiChieu[9, 7] = _dulieu.First().VV3;
                    DungChung.Bien.MangHaiChieu[10, 7] = _dulieu.First().VV60;
                    DungChung.Bien.MangHaiChieu[11, 7] = _dulieu.First().VV4;

                    DungChung.Bien.MangHaiChieu[6, 8] = _dulieu.First().CV1;
                    DungChung.Bien.MangHaiChieu[7, 8] = "";
                    DungChung.Bien.MangHaiChieu[8, 8] = _dulieu.First().CV2;
                    DungChung.Bien.MangHaiChieu[9, 8] = _dulieu.First().CV3;
                    DungChung.Bien.MangHaiChieu[10, 8] = _dulieu.First().CV60;
                    DungChung.Bien.MangHaiChieu[11, 8] = _dulieu.First().CV4;
                    #endregion
                    rep.CreateDocument();
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", "C:\\BCHoatDongKKB.xls", true, this.Name);
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
                #region chi tiết theo khoa phòng 30009
                else
                {
                    
                    var id = (from k in kphong
                                join kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay) on k.makp equals kb.MaKP
                                join bn in data.BenhNhans.Where(p => p.DTuong == "BHYT" || p.DTuong == "Dịch vụ") on kb.MaBNhan equals bn.MaBNhan
                                group kb by new { kb.MaBNhan, bn.SThe, bn.Tuoi, bn.DTuong, bn.CapCuu, k.tenkp, kb.PhuongAn, bn.NoiTru } into kq
                                select new { MaBNhan = kq.Key.MaBNhan??0, kq.Key.SThe, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.CapCuu, kq.Key.tenkp, kq.Key.PhuongAn, kq.Key.NoiTru }).ToList();
                    int n1 = 1,n2=0;
                    //if (DungChung.Bien.MaBV == "27021")
                    //{
                    //    n1 = 1;
                    //    n2 = 0;
                    //}
                    if (DungChung.Bien.MaBV == "26007")
                        n2 = -1;
                    var q1 = (from n in id
                             group new { n } by new { n.tenkp } into kq
                             select new
                             {
                                 Group = "Tổng số lần khám bệnh chung",
                                 kq.Key.tenkp,
                                 TS = 0,
                                 BHYT = 0,
                                 VP = 0,
                                 CC = 0,
                                 VV = kq.Where(p => p.n.PhuongAn == 1).Where(p => p.n.NoiTru == n1||n1==-1).Select(p => p.n.MaBNhan).Distinct().Count(),
                                 CV = kq.Where(p => p.n.PhuongAn == 2).Select(p => p.n.MaBNhan).Distinct().Count(),
                                 HN = 0
                             }).ToList();
                    if (radioGroup2.SelectedIndex == 1)
                    {
                        var id1 = (from a in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay)
                                    join b in data.BenhNhans.Where(p => p.DTuong == "BHYT" || p.DTuong == "Dịch vụ") on a.MaBNhan equals b.MaBNhan
                                    group new { a, b } by new { b.MaBNhan, b.SThe, b.Tuoi, b.DTuong, b.CapCuu, b.NoiTru } into kq
                                    select new { kq.Key.MaBNhan, kq.Key.SThe, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.CapCuu, IDKB = kq.Max(p => p.a.IDKB), kq.Key.NoiTru }).ToList();
                        id = (from a in id1
                                join b in data.BNKBs on a.IDKB equals b.IDKB
                                join c in kphong on b.MaKP equals c.makp
                                select new { a.MaBNhan, a.SThe, a.Tuoi, a.DTuong, a.CapCuu, c.tenkp, b.PhuongAn, a.NoiTru }).ToList();
                    }
                    var q2 = (from n in id
                                group new { n } by new { n.tenkp } into kq
                                select new
                                {
                                    Group = "Tổng số lần khám bệnh chung",
                                    kq.Key.tenkp,
                                    TS = kq.Where(p => p.n.NoiTru == n2||n2==-1).Select(p => p.n.MaBNhan).Distinct().Count(),
                                    BHYT = kq.Where(p => p.n.DTuong.ToLower().Contains("bhyt")).Where(p => p.n.NoiTru == n2||n2==-1).Select(p => p.n.MaBNhan).Distinct().Count(),
                                    VP = kq.Where(p => p.n.DTuong.ToLower().Contains("dịch vụ")).Where(p => p.n.NoiTru == n2||n2==-1).Select(p => p.n.MaBNhan).Distinct().Count(),
                                    CC = kq.Where(p => p.n.CapCuu == 1).Select(p => p.n.MaBNhan).Distinct().Count(),
                                    VV = 0,
                                    CV = 0,
                                    HN = kq.Where(p => p.n.SThe.Contains("HN")).Select(p => p.n.MaBNhan).Distinct().Count()
                                }).ToList();
                    var q3 = q1.Concat(q2);
                    var q = (from a in q3
                             group a by new { a.Group, a.tenkp } into kq
                             select new {
                                 kq.Key.Group,
                                 kq.Key.tenkp,
                                 TS = kq.Sum(p => p.TS),
                                 BHYT = kq.Sum(p => p.BHYT),
                                 VP = kq.Sum(p => p.VP),
                                 CC = kq.Sum(p => p.CC),
                                 VV = kq.Sum(p => p.VV),
                                 CV = kq.Sum(p => p.CV),
                                 HN = kq.Sum(p => p.HN)
                             }).ToList();
                    #region xuat Excel
                    string _ntn = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;
                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@" };
                    string[] _tieude = { "STT", "Nội dung", "Tổng số lần khám", "Số lần khám BHYT", "Số lần khám viện phí", "Số lần khám không thu được", "Số bệnh nhân cấp cứu", "Số bệnh nhân vào viện", "Số bệnh nhân chuyển viện " };
                    int[] _arrWidth = new int[] { };// { 5, 15, 10, 10, 10, 10, 10, 10, 10, 10};

                    DungChung.Bien.MangHaiChieu = new Object[q.OrderBy(p => p.tenkp).Count() + 40, 15];
                    DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.TenCQ.ToUpper();
                    DungChung.Bien.MangHaiChieu[2, 2] = ("BÁO CÁO HOẠT ĐỘNG KHOA KHÁM BỆNH").ToUpper();
                    DungChung.Bien.MangHaiChieu[3, 2] = _ntn;

                    DungChung.Bien.MangHaiChieu[11, 7] = "Ngày ...... tháng ..... năm .....";
                    DungChung.Bien.MangHaiChieu[12, 1] = ("Người lập biểu").ToUpper();
                    DungChung.Bien.MangHaiChieu[16, 1] = DungChung.Bien.NguoiLapBieu;
                    DungChung.Bien.MangHaiChieu[12, 4] = ("Trưởng hoặc phó khoa").ToUpper();
                    DungChung.Bien.MangHaiChieu[16, 4] = DungChung.Bien.GiamDoc;

                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[5, i] = _tieude[i];
                    }
                    var rr = q.OrderBy(p => p.tenkp).ToList();
                    DungChung.Bien.MangHaiChieu[6, 1] = "Tổng số lần khám bệnh chung";
                    DungChung.Bien.MangHaiChieu[6, 2] = rr.Sum(p => p.TS);
                    DungChung.Bien.MangHaiChieu[6, 3] = rr.Sum(p => p.BHYT);
                    DungChung.Bien.MangHaiChieu[6, 4] = rr.Sum(p => p.VP);
                    DungChung.Bien.MangHaiChieu[6, 5] = "";
                    DungChung.Bien.MangHaiChieu[6, 6] = rr.Sum(p => p.CC);
                    DungChung.Bien.MangHaiChieu[6, 7] = rr.Sum(p => p.VV);
                    DungChung.Bien.MangHaiChieu[6, 8] = rr.Sum(p => p.CV);
                    int num = 7;
                    foreach (var r in q.OrderBy(p => p.tenkp).ToList())
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num - 4;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.tenkp;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.TS;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.BHYT;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.VP;
                        DungChung.Bien.MangHaiChieu[num, 5] = "";
                        DungChung.Bien.MangHaiChieu[num, 6] = r.CC;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.VV;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.CV;
                        num++;
                    }
                    DungChung.Bien.MangHaiChieu[num + 1, 0] = "Trong đó : BHYT người nghèo:  " + rr.Sum(p => p.HN);
                    #endregion
                    if (DungChung.Bien.MaBV == "26007")
                    {
                        BaoCao.Rep_BcHoatDongKKB_26007 rep1 = new BaoCao.Rep_BcHoatDongKKB_26007();
                        rep1.TuNgay.Value = lupTuNgay.Text;
                        rep1.DenNgay.Value = lupDenNgay.Text;
                        if (ckQuy.Checked == true)
                        {
                            rep1.TG.Value = theoquy();
                        }
                        else rep1.TG.Value = theoquy();
                        rep1.DataSource = q.OrderBy(p => p.tenkp).ToList();
                        rep1.BindingData();
                        rep1.CreateDocument();
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", "C:\\BCHoatDongKKB.xls", true, this.Name);
                        frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        BaoCao.Rep_BcHoatDongKKB_30009 rep1 = new BaoCao.Rep_BcHoatDongKKB_30009();


                        rep1.TuNgay.Value = lupTuNgay.Text;
                        rep1.DenNgay.Value = lupDenNgay.Text;
                        if (ckQuy.Checked == true)
                        {
                            rep1.TG.Value = theoquy();
                        }
                        else rep1.TG.Value = theoquy();
                        rep1.DataSource = q.OrderBy(p => p.tenkp).ToList();
                        rep1.BindingData();
                        rep1.CreateDocument();
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", "C:\\BCHoatDongKKB.xls", true, this.Name);
                        frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                        frm.ShowDialog();
                    } 
                }
                #endregion
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
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


    }
}