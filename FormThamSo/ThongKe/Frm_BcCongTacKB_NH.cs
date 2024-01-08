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
    public partial class Frm_BcCongTacKB_NH : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcCongTacKB_NH()
        {
            InitializeComponent();
        }
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
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }

            else return true;
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
        private class VV
        {
            private string TenKP;
            private int MaKP;
            private string SLT;
            private string SL1;
            private string SL2;
            private string SL3;
            private string SL4;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public string slt
            { set { SLT = value; } get { return SLT; } }
            public string sl1
            { set { sl1 = value; } get { return sl1; } }
            public string sl2
            { set { SL2 = value; } get { return SL2; } }
            public string sl3
            { set { SL3 = value; } get { return SL3; } }
            public string sl4
            { set { SL4 = value; } get { return SL4; } }
        }

        List<KPhong> _Kphong = new List<KPhong>();
        List<VV> _VV = new List<VV>();

        private void Frm_BcCongTacKB_NH_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng")
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

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBc())
            {

                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);//dateDenNgay.DateTime;

                frmIn frm = new frmIn();
                BaoCao.Rep_BcCongTacKB_NH rep = new BaoCao.Rep_BcCongTacKB_NH();
                BaoCao.Rep_BcCongTacKB_30007 rep1 = new BaoCao.Rep_BcCongTacKB_30007();
                rep.Ngay.Value = "(Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text + ")";
                //rep.TS.Value = lupTuNgay.Text;
                //rep.TS_BHYT.Value = lupDenNgay.Text;
                rep.Thang.Value = "Tháng " + Convert.ToDateTime(lupDenNgay.Text).Month + " năm " + Convert.ToDateTime(lupDenNgay.Text).Year;
                int _MaKP1 = 0;
                int _MaKP2 = 0;
                int _MaKP3 = 0;
                int _MaKP4 = 0;
                int _MaKP5 = 0;
                int _MaKP6 = 0;
                int _MaKP7 = 0;
                int _MaKP8 = 0;
                int _MaKP9 = 0;
                int _MaKP10 = 0;
                int _MaKP11 = 0;
                int _MaKP12 = 0;
                int _MaKP13 = 0;
                int _MaKP14 = 0;
                int _MaKP15 = 0;
                int _MaKP16 = 0;
                int _MaKP17 = 0;
                int _MaKP18 = 0;
                int _MaKP19 = 0;
                int _MaKP20 = 0;

                for (int i = 0; i < _Kphong.Count; i++)
                {
                    if (_Kphong.Skip(i).First().chon == true)
                    {
                        switch (i)
                        {
                            case 0:
                                _MaKP1 = _Kphong.Skip(i).First().makp;
                                break;
                            case 1:
                                _MaKP2 = _Kphong.Skip(i).First().makp;
                                break;
                            case 2:
                                _MaKP3 = _Kphong.Skip(i).First().makp;
                                break;
                            case 3:
                                _MaKP4 = _Kphong.Skip(i).First().makp;
                                break;
                            case 4:
                                _MaKP5 = _Kphong.Skip(i).First().makp;
                                break;
                            case 5:
                                _MaKP6 = _Kphong.Skip(i).First().makp;
                                break;
                            case 6:
                                _MaKP7 = _Kphong.Skip(i).First().makp;
                                break;
                            case 7:
                                _MaKP8 = _Kphong.Skip(i).First().makp;
                                break;
                            case 8:
                                _MaKP9 = _Kphong.Skip(i).First().makp;
                                break;
                            case 9:
                                _MaKP10 = _Kphong.Skip(i).First().makp;
                                break;
                            case 10:
                                _MaKP11 = _Kphong.Skip(i).First().makp;
                                break;
                            case 11:
                                _MaKP12 = _Kphong.Skip(i).First().makp;
                                break;
                            case 12:
                                _MaKP13 = _Kphong.Skip(i).First().makp;
                                break;
                            case 13:
                                _MaKP14 = _Kphong.Skip(i).First().makp;
                                break;
                            case 14:
                                _MaKP15 = _Kphong.Skip(i).First().makp;
                                break;
                            case 15:
                                _MaKP16 = _Kphong.Skip(i).First().makp;
                                break;
                            case 16:
                                _MaKP17 = _Kphong.Skip(i).First().makp;
                                break;
                            case 17:
                                _MaKP18 = _Kphong.Skip(i).First().makp;
                                break;
                            case 18:
                                _MaKP19 = _Kphong.Skip(i).First().makp;
                                break;
                            case 19:
                                _MaKP20 = _Kphong.Skip(i).First().makp;
                                break;
                        }
                    }
                }
                var id = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay)
                          group kb by kb.MaBNhan into kq
                          select new
                          {
                              kq.Key,
                              IDKB = kq.Max(p => p.IDKB)
                          }).OrderBy(p => p.Key).ToList();
                //// Khám bệnh
                var qkb1 = (from k in id
                            join kb in data.BNKBs on k.IDKB equals kb.IDKB
                            join bn in data.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                            group new { kb, bn } by new { kb.MaBNhan, bn.SThe, bn.Tuoi, bn.DTuong } into kq
                            select new { kq.Key.MaBNhan, IDKB = kq.Max(p => p.kb.IDKB), kq.Key.SThe, kq.Key.Tuoi, kq.Key.DTuong }).ToList();
                var qkb = (from k in qkb1
                           join p in data.BNKBs on k.IDKB equals p.IDKB
                           join bn in data.BenhNhans on p.MaBNhan equals bn.MaBNhan
                           where (p.MaKP == _MaKP1 || p.MaKP == _MaKP2 || p.MaKP == _MaKP3 || p.MaKP == _MaKP4 || p.MaKP == _MaKP5 || p.MaKP == _MaKP6 || p.MaKP == _MaKP7 || p.MaKP == _MaKP8 || p.MaKP == _MaKP9 || p.MaKP == _MaKP10 ||
                           p.MaKP == _MaKP11 || p.MaKP == _MaKP12 || p.MaKP == _MaKP13 || p.MaKP == _MaKP14 || p.MaKP == _MaKP15 || p.MaKP == _MaKP16 || p.MaKP == _MaKP17 || p.MaKP == _MaKP18 || p.MaKP == _MaKP19 || p.MaKP == _MaKP20)
                           group new { p, bn } by new { p.MaBNhan, bn.SThe, bn.Tuoi, bn.DTuong } into kq
                           select new
                           {
                               TS = kq.Select(p => p.p.MaBNhan).Count(),
                               TS_BHYT = kq.Where(p => p.bn.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count(),
                               BHNN = kq.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.SThe.Contains("HN") || p.bn.SThe.Contains("CN")).Select(p => p.p.MaBNhan).Count(),
                               TE6 = kq.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi < 6).Select(p => p.p.MaBNhan).Count(),
                               //    BHKhac=kq.Where(p=>p.bn.DTuong=="BHYT").Select(p=>p.kb.MaBNhan).Count()- kq.Where(p=>p.bn.SThe.Contains("HN")).Select(p=>p.kb.MaBNhan).Count(),
                               BN60 = kq.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi > 60).Select(p => p.p.MaBNhan).Count(),
                               DichVu = kq.Where(p => p.bn.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count(),
                               DV_BN60 = kq.Where(p => p.bn.DTuong == "Dịch vụ").Where(p => p.bn.Tuoi > 60).Select(p => p.p.MaBNhan).Count(),
                               dv = kq.Where(p => p.bn.DTuong == "Dịch vụ").Where(p => p.bn.Tuoi <= 60).Select(p => p.p.MaBNhan).Count()
                           }).ToList();
                if (qkb.Count > 0)
                {
                    if (radMauBC.SelectedIndex == 0)// na hang
                    {
                        rep.TS.Value = qkb.Sum(p => p.TS).ToString();
                        rep.TS_BHYT.Value = qkb.Sum(p => p.TS_BHYT).ToString();
                        rep.BHNN.Value = qkb.Sum(p => p.BHNN).ToString();
                        rep.BHKhac.Value = (qkb.Sum(p => p.TS_BHYT) - qkb.Sum(p => p.BHNN) - qkb.Sum(p => p.TE6)).ToString();
                        rep.TE6.Value = qkb.Sum(p => p.TE6).ToString();
                        rep.BN60.Value = qkb.Sum(p => p.BN60).ToString();
                        rep.BNDV.Value = qkb.Sum(p => p.DichVu).ToString();
                    }
                    else // tứ kỳ
                    {
                        rep1.TS.Value = qkb.Sum(p => p.TS).ToString();
                        rep1.TS_BHYT.Value = qkb.Sum(p => p.TS_BHYT).ToString();
                        rep1.BHNN.Value = qkb.Sum(p => p.BHNN).ToString();
                        rep1.BHKhac.Value = (qkb.Sum(p => p.TS_BHYT) - qkb.Sum(p => p.BHNN) - qkb.Sum(p => p.TE6)).ToString();
                        rep1.TE6.Value = qkb.Sum(p => p.TE6).ToString();
                        rep1.BN60.Value = qkb.Sum(p => p.BN60).ToString();
                        rep1.BNDV.Value = qkb.Sum(p => p.DichVu).ToString();
                        rep1.DV_BNKhac.Value = qkb.Sum(p => p.DV_BN60).ToString();
                        rep1.DV_BN60.Value = qkb.Sum(p => p.DV_BN60).ToString();
                        string a = qkb.Sum(p => p.dv).ToString();
                    }
                }
                ////BN kê đơn
                var maBNDThuoc = (from dt in data.DThuocs select dt.MaBNhan).ToList();
                var qkbkd1 = (from k in id
                              where maBNDThuoc.Contains(k.Key)
                              join kb in data.BNKBs on k.IDKB equals kb.IDKB
                              join bn in data.BenhNhans.Where(p=>DungChung.Bien.MaBV == "12122" ? (p.NoiTru == 0) : true
                              ) on kb.MaBNhan equals bn.MaBNhan
                              group new { kb, bn } by new { kb.MaBNhan, bn.SThe, bn.Tuoi, bn.DTuong } into kq
                              select new { kq.Key.MaBNhan, IDKB = kq.Max(p => p.kb.IDKB), kq.Key.SThe, kq.Key.Tuoi, kq.Key.DTuong }).ToList();
                var qkd = (from k in qkbkd1
                           join p in data.BNKBs on k.IDKB equals p.IDKB
                           where (p.MaKP == _MaKP1 || p.MaKP == _MaKP2 || p.MaKP == _MaKP3 || p.MaKP == _MaKP4 || p.MaKP == _MaKP5 || p.MaKP == _MaKP6 || p.MaKP == _MaKP7 || p.MaKP == _MaKP8 || p.MaKP == _MaKP9 || p.MaKP == _MaKP10 ||
                            p.MaKP == _MaKP11 || p.MaKP == _MaKP12 || p.MaKP == _MaKP13 || p.MaKP == _MaKP14 || p.MaKP == _MaKP15 || p.MaKP == _MaKP16 || p.MaKP == _MaKP17 || p.MaKP == _MaKP18 || p.MaKP == _MaKP19 || p.MaKP == _MaKP20)
                           join bn in data.BenhNhans on p.MaBNhan equals bn.MaBNhan
                           group new { p, bn } by new { p.MaBNhan, bn.SThe, bn.Tuoi, bn.DTuong } into kq
                           select new
                           {
                               TS = kq.Select(p => p.p.MaBNhan).Count(),
                               BH = kq.Where(p => p.bn.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count(),
                               BHNN = kq.Where(p => p.bn.SThe.Contains("HN") || p.bn.SThe.Contains("CN")).Select(p => p.p.MaBNhan).Count(),
                               TE6 = kq.Where(p => p.bn.Tuoi < 6).Select(p => p.p.MaBNhan).Count(),
                               //    BHKhac=kq.Where(p=>p.bn.DTuong=="BHYT").Select(p=>p.kb.MaBNhan).Count()- kq.Where(p=>p.bn.SThe.Contains("HN")).Select(p=>p.kb.MaBNhan).Count(),
                               DichVu = kq.Where(p => p.bn.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count(),
                           }).ToList();
                if (qkd.Count > 0)
                {
                    if (radMauBC.SelectedIndex == 0)
                    {
                        rep.BNKD.Value = qkd.Sum(p => p.TS).ToString();
                        rep.BNKD_NN.Value = qkd.Sum(p => p.BHNN).ToString();
                        rep.BNKD_TE6.Value = qkd.Sum(p => p.TE6).ToString();
                        rep.BNKD_BHKhac.Value = (qkd.Sum(p => p.TS) - qkd.Sum(p => p.BHNN) - qkd.Sum(p => p.TE6) - qkd.Sum(p => p.DichVu)).ToString();
                        rep.BNKD_koThe.Value = qkd.Sum(p => p.DichVu).ToString();
                    }
                    else
                    {
                        rep1.BNKD.Value = qkd.Sum(p => p.TS).ToString();
                        rep1.BNKD_NN.Value = qkd.Sum(p => p.BHNN).ToString();
                        rep1.BNKD_TE6.Value = qkd.Sum(p => p.TE6).ToString();
                        rep1.BNKD_BHKhac.Value = (qkd.Sum(p => p.TS) - qkd.Sum(p => p.BHNN) - qkd.Sum(p => p.TE6) - qkd.Sum(p => p.DichVu)).ToString();
                        rep1.BNKD_koThe.Value = qkd.Sum(p => p.DichVu).ToString();
                    }
                }
                //BN chuyển viện
                var qkbcv1 = (from k in id
                              join kb in data.BNKBs.Where(p => p.PhuongAn == 2) on k.IDKB equals kb.IDKB
                              join bn in data.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                              //   join kp in data.KPhongs on kb.MaKPdt equals kp.MaKP
                              group new { kb, bn } by new { kb.MaKP, kb.MaKPdt, kb.MaBNhan, bn.SThe, bn.Tuoi, bn.DTuong } into kq
                              select new { kq.Key.MaBNhan, kq.Key.MaKP, IDKB = kq.Max(p => p.kb.IDKB), kq.Key.SThe, kq.Key.Tuoi, kq.Key.DTuong }).ToList();
                var qcv = (from k in qkbcv1
                           join p in data.BNKBs on k.IDKB equals p.IDKB
                           where (p.MaKP == _MaKP1 || p.MaKP == _MaKP2 || p.MaKP == _MaKP3 || p.MaKP == _MaKP4 || p.MaKP == _MaKP5 || p.MaKP == _MaKP6 || p.MaKP == _MaKP7 || p.MaKP == _MaKP8 || p.MaKP == _MaKP9 || p.MaKP == _MaKP10 ||
                            p.MaKP == _MaKP11 || p.MaKP == _MaKP12 || p.MaKP == _MaKP13 || p.MaKP == _MaKP14 || p.MaKP == _MaKP15 || p.MaKP == _MaKP16 || p.MaKP == _MaKP17 || p.MaKP == _MaKP18 || p.MaKP == _MaKP19 || p.MaKP == _MaKP20)
                           join bn in data.BenhNhans on p.MaBNhan equals bn.MaBNhan
                           group new { p, bn } by new { p.MaBNhan, bn.SThe, bn.Tuoi, bn.DTuong } into kq
                           select new
                           {
                               TS = kq.Select(p => p.p.MaBNhan).Count(),
                               BH = kq.Where(p => p.bn.DTuong == "BHYT").Select(p => p.p.MaBNhan).Count(),
                               BHNN = kq.Where(p => p.bn.SThe.Contains("HN") || p.bn.SThe.Contains("CN")).Select(p => p.p.MaBNhan).Count(),
                               TE6 = kq.Where(p => p.bn.Tuoi < 6).Select(p => p.p.MaBNhan).Count(),
                               DichVu = kq.Where(p => p.bn.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Count(),
                           }).ToList();
                if (qcv.Count > 0)
                {
                    if (radMauBC.SelectedIndex == 0)
                    {
                        rep.BNCV.Value = qcv.Sum(p => p.TS).ToString();
                        rep.BNCV_NN.Value = qcv.Sum(p => p.BHNN).ToString();
                        rep.BNCV_TE6.Value = qcv.Sum(p => p.TE6).ToString();
                        rep.BNCV_BHKhac.Value = (qcv.Sum(p => p.BH) - qcv.Sum(p => p.BHNN) - qcv.Sum(p => p.TE6)).ToString();
                        rep.BNCV_KoBH.Value = qcv.Sum(p => p.DichVu).ToString();
                    }
                    else
                    {
                        rep1.BNCV.Value = qcv.Sum(p => p.TS).ToString();
                        rep1.BNCV_NN.Value = qcv.Sum(p => p.BHNN).ToString();
                        rep1.BNCV_TE6.Value = qcv.Sum(p => p.TE6).ToString();
                        rep1.BNCV_BHKhac.Value = (qcv.Sum(p => p.BH) - qcv.Sum(p => p.BHNN) - qcv.Sum(p => p.TE6)).ToString();
                        rep1.BNCV_KoBH.Value = qcv.Sum(p => p.DichVu).ToString();
                    }
                }
                // BN Khám sức khỏe
                var ksk = (from sk in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay && p.DTuong == "KSK")
                           group sk by sk.MaBNhan into kq
                           select new { kq.Key }).ToList();
                if (ksk.Count > 0) { rep.KCK.Value = ksk.Count(); }

                //BN điều trị ngoại trú
                var qdtnt1 = (from k in id
                              join vv in data.VaoViens on k.Key equals vv.MaBNhan
                              join kb in data.BNKBs on k.IDKB equals kb.IDKB
                              join bn in data.BenhNhans.Where(p => p.NoiTru == 0) on kb.MaBNhan equals bn.MaBNhan
                              group new { kb, bn } by new { kb.MaBNhan, bn.SThe, bn.Tuoi, bn.DTuong } into kq
                              select new { kq.Key.MaBNhan, IDKB = kq.Max(p => p.kb.IDKB), kq.Key.SThe, kq.Key.Tuoi, kq.Key.DTuong }).ToList();
                var qdtnt = (from k in qdtnt1
                             join p in data.BNKBs on k.IDKB equals p.IDKB
                             where (p.MaKP == _MaKP1 || p.MaKP == _MaKP2 || p.MaKP == _MaKP3 || p.MaKP == _MaKP4 || p.MaKP == _MaKP5 || p.MaKP == _MaKP6 || p.MaKP == _MaKP7 || p.MaKP == _MaKP8 || p.MaKP == _MaKP9 || p.MaKP == _MaKP10 ||
                              p.MaKP == _MaKP11 || p.MaKP == _MaKP12 || p.MaKP == _MaKP13 || p.MaKP == _MaKP14 || p.MaKP == _MaKP15 || p.MaKP == _MaKP16 || p.MaKP == _MaKP17 || p.MaKP == _MaKP18 || p.MaKP == _MaKP19 || p.MaKP == _MaKP20)
                             join bn in data.BenhNhans on p.MaBNhan equals bn.MaBNhan
                             group new { p, bn } by new { p.MaBNhan } into kq
                             select new
                             {
                                 TS = kq.Select(p => p.p.MaBNhan).Count(),

                             }).ToList();
                if (qdtnt.Count > 0)
                {
                    if (radMauBC.SelectedIndex == 0)
                    {
                        rep.DTNgT.Value = qdtnt.Count();
                    }
                    else {
                        rep1.DTNgT.Value = qdtnt.Count();
                    }
                }
                // BN vào viện

                var qkbvv = (from p in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay)
                             join bn in data.BenhNhans.Where(p => p.NoiTru == 1) on p.MaBNhan equals bn.MaBNhan
                             join kp in data.KPhongs on p.MaKPdt equals kp.MaKP
                             where (p.MaKP == _MaKP1 || p.MaKP == _MaKP2 || p.MaKP == _MaKP3 || p.MaKP == _MaKP4 || p.MaKP == _MaKP5 || p.MaKP == _MaKP6 || p.MaKP == _MaKP7 || p.MaKP == _MaKP8 || p.MaKP == _MaKP9 || p.MaKP == _MaKP10 ||
                               p.MaKP == _MaKP11 || p.MaKP == _MaKP12 || p.MaKP == _MaKP13 || p.MaKP == _MaKP14 || p.MaKP == _MaKP15 || p.MaKP == _MaKP16 || p.MaKP == _MaKP17 || p.MaKP == _MaKP18 || p.MaKP == _MaKP19 || p.MaKP == _MaKP20)
                            // where(DungChung.Bien.MaBV == "12122" ? (p.PhuongAn==1 ): true)//||bn.NoiTru==1)
                             group new { p, bn } by new { p.MaKPdt, kp.TenKP, p.MaBNhan } into kq
                             select new
                             {
                                 MaKPdt = kq.Key.MaKPdt,
                                 TenKP = kq.Key.TenKP, 
                                 MaBNhan = kq.Key.MaBNhan,
                                 SLT = kq.Select(p => p.p.MaBNhan).Distinct().Count(),
                                 SL1 = kq.Where(p => p.bn.SThe.Contains("HN") || p.bn.SThe.Contains("CN")).Select(p => p.p.MaBNhan).Distinct().Count(),
                                 SL2 = kq.Where(p => p.bn.Tuoi < 6).Select(p => p.p.MaBNhan).Distinct().Count(),
                                 SL3 = kq.Where(p => p.bn.DTuong == "BHYT").Select(p => p.p.MaBNhan).Distinct().Count(),
                                 SL4 = kq.Where(p => p.bn.DTuong == "Dịch vụ").Select(p => p.p.MaBNhan).Distinct().Count(),
                             }).OrderBy(p => p.MaKPdt).ToList();
                var qvv = (from kb in qkbvv
                           group new { kb } by new { kb.MaKPdt, kb.TenKP } into kq
                           select new
                           {
                               MaKPdt = kq.Key.MaKPdt,
                               TenKP = "BN vào khoa " + kq.Key.TenKP,

                               SLT = kq.Sum(p => p.kb.SLT) == 0 ? null : kq.Sum(p => p.kb.SLT).ToString(),
                               SL1 = kq.Sum(p => p.kb.SL1) == 0 ? null : kq.Sum(p => p.kb.SL1).ToString(),
                               SL2 = kq.Sum(p => p.kb.SL2) == 0 ? null : kq.Sum(p => p.kb.SL2).ToString(),
                               SL3 = (kq.Sum(p => p.kb.SL3) - kq.Sum(p => p.kb.SL2) - kq.Sum(p => p.kb.SL1)) <= 0 ? null : (kq.Sum(p => p.kb.SL3) - kq.Sum(p => p.kb.SL2) - kq.Sum(p => p.kb.SL1)).ToString(),
                               SL4 = kq.Sum(p => p.kb.SL4) == 0 ? null : kq.Sum(p => p.kb.SL4).ToString(),
                           }).ToList();

                if (radMauBC.SelectedIndex == 0)
                {
                    rep.BNNoiT.Value = qkbvv.Count();
                    rep.DataSource = qvv;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    rep1.BNNoiT.Value = qkbvv.Count();
                    rep1.DataSource = qvv;
                    rep1.BindingData();
                    rep1.CreateDocument();
                    frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                    frm.ShowDialog();
                }
            }
            else MessageBox.Show("Không có dữ liệu");


        }

        private void btnHuy_Click(object sender, EventArgs e)
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