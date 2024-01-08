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
    public partial class Frm_BcKhamBenh_CL : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcKhamBenh_CL()
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

            return true;
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
        private void Frm_BcKhamBenh_CL_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            var kphong = (from kp in data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
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
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                List<KPhong> khoa = new List<KPhong>();
                khoa = _Kphong.Where(p => p.chon == true && p.makp > 0).ToList();
                #region bỏ
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
                //int _MaKP21 = 0;
                //int _MaKP22 = 0;
                //int _MaKP23 = 0;
                //int _MaKP24 = 0;
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
                //            case 20:
                //                _MaKP21 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 21:
                //                _MaKP22 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 22:
                //                _MaKP23 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 23:
                //                _MaKP24 = _Kphong.Skip(i).First().makp;
                //                break;
                //        }
                //    }
                //}
                #endregion
                int nt = 0;
                if (rdgNoiTru.SelectedIndex == 0)//ngoại trú
                    nt = 0;
                if (rdgNoiTru.SelectedIndex == 1)//nội trú
                    nt = 1;
                if (rdgNoiTru.SelectedIndex == 2)//cả hai
                    nt = 2;
                frmIn frm = new frmIn();
                BaoCao.Rep_BcKhamBenh_CL rep = new BaoCao.Rep_BcKhamBenh_CL();
                var id = (from kp in khoa
                          join kb in data.BNKBs.Where(p => p.NgayKham >= tungay) on kp.makp equals kb.MaKP
                          group kb by kb.MaBNhan into kq
                          select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();
                var q1 = (from k in id
                          join bnkb in data.BNKBs on k.IDKB equals bnkb.IDKB
                          join bn in data.BenhNhans.Where(p => nt == 2 || p.NoiTru == nt) on bnkb.MaBNhan equals bn.MaBNhan
                          group new { bnkb, bn } by new { } into kq
                          select new
                          {
                              tsi1 = kq.Select(p => p.bnkb.MaBNhan).Count(),
                              tsi2 = kq.Where(p => p.bn.NgoaiGio == 1).Select(p => p.bnkb.MaBNhan).Count(),
                              tsi3 = kq.Where(p => p.bn.DTuong == "BHYT").Select(p => p.bnkb.MaBNhan).Count(),
                              tsi4 = kq.Where(p => p.bn.DTuong == "KSK").Select(p => p.bnkb.MaBNhan).Count(),
                              tsi8 = kq.Where(p => p.bn.NoiTru == 0).Select(p => p.bnkb.MaBNhan).Count(),
                          }).ToList();
                if (q1.Count > 0)
                {
                    if (q1.First().tsi1 > 0) { rep.TSI1.Value = q1.First().tsi1; } else { rep.TSI1.Value = ""; }
                    if (q1.First().tsi2 > 0) { rep.TSI2.Value = q1.First().tsi2; } else { rep.TSI2.Value = ""; }
                    if (q1.First().tsi3 > 0) { rep.TSI3.Value = q1.First().tsi3; } else { rep.TSI4.Value = ""; }
                    if (q1.First().tsi4 > 0) { rep.TSI4.Value = q1.First().tsi4; } else { rep.TSI4.Value = ""; }
                    if (q1.First().tsi8 > 0) { rep.TSI8.Value = q1.First().tsi8; } else { rep.TSI8.Value = ""; }
                }
                var q2 = (from kp in khoa
                          join kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay) on kp.makp equals kb.MaKP
                          join bn in data.BenhNhans.Where(p => nt == 2 || p.NoiTru == nt) on kb.MaBNhan equals bn.MaBNhan
                          join pk in data.KPhongs on kb.MaKP equals pk.MaKP
                          group new { kb, bn, pk } by new { } into kq
                          select new
                          {
                              tsii1 = kq.Select(p => p.kb.MaBNhan).Count(),
                              tsii2 = kq.Where(p => p.pk.PLoai == "Phòng khám").Select(p => p.kb.MaBNhan).Count(),
                              tsii3 = kq.Where(p => p.pk.PLoai == "Phòng khám").Where(p => p.bn.DTuong == "BHYT").Select(p => p.kb.MaBNhan).Count(),
                              tsii4 = kq.Where(p => p.bn.Tuoi > 6).Select(p => p.kb.MaBNhan).Count(),
                              tsii5 = kq.Where(p => p.bn.Tuoi > 6 && p.bn.DTuong == "BHYT").Select(p => p.kb.MaBNhan).Count(),
                              tsii6 = kq.Where(p => p.bn.Tuoi > 6 && p.bn.GTinh == 0).Select(p => p.kb.MaBNhan).Count(),
                              tsii7 = kq.Where(p => p.bn.Tuoi >= 60).Select(p => p.kb.MaBNhan).Count(),
                              tsii8 = kq.Where(p => p.bn.Tuoi >= 60 && p.bn.DTuong == "BHYT").Select(p => p.kb.MaBNhan).Count(),
                              tsii12 = kq.Where(p => p.bn.Tuoi <= 6).Select(p => p.kb.MaBNhan).Count(),
                              tsii13 = kq.Where(p => p.bn.Tuoi <= 6 && p.bn.DTuong == "BHYT").Select(p => p.kb.MaBNhan).Count(),
                              tsii14 = kq.Where(p => p.bn.Tuoi < 6).Select(p => p.kb.MaBNhan).Count(),
                              tsii15 = kq.Where(p => p.bn.Tuoi < 6).Where(p => p.bn.DTuong == "BHYT").Select(p => p.kb.MaBNhan).Count(),
                              tsii16 = kq.Where(p => p.bn.Tuoi < 1).Select(p => p.kb.MaBNhan).Count(),
                              tsii17 = kq.Where(p => p.bn.Tuoi >= 1 && p.bn.Tuoi <= 6).Select(p => p.kb.MaBNhan).Count(),
                          }).ToList();
                if (q2.Count > 0)
                {
                    if (q2.First().tsii1 > 0) { rep.TSII1.Value = q2.First().tsii1; } else { rep.TSII1.Value = ""; }
                    if (q2.First().tsii2 > 0) { rep.TSII2.Value = q2.First().tsii2; } else { rep.TSII2.Value = ""; }
                    if (q2.First().tsii3 > 0) { rep.TSII3.Value = q2.First().tsii3; } else { rep.TSII3.Value = ""; }
                    if (q2.First().tsii4 > 0) { rep.TSII4.Value = q2.First().tsii4; } else { rep.TSII4.Value = ""; }
                    if (q2.First().tsii5 > 0) { rep.TSII5.Value = q2.First().tsii5; } else { rep.TSII5.Value = ""; }
                    if (q2.First().tsii6 > 0) { rep.TSII6.Value = q2.First().tsii6; } else { rep.TSII6.Value = ""; }
                    if (q2.First().tsii7 > 0) { rep.TSII7.Value = q2.First().tsii7; } else { rep.TSII7.Value = ""; }
                    if (q2.First().tsii8 > 0) { rep.TSII8.Value = q2.First().tsii8; } else { rep.TSII8.Value = ""; }

                    if (q2.First().tsii12 > 0) { rep.TSII12.Value = q2.First().tsii12; } else { rep.TSII12.Value = ""; }
                    if (q2.First().tsii13 > 0) { rep.TSII13.Value = q2.First().tsii13; } else { rep.TSII13.Value = ""; }
                    if (q2.First().tsii14 > 0) { rep.TSII14.Value = q2.First().tsii14; } else { rep.TSII14.Value = ""; }
                    if (q2.First().tsii15 > 0) { rep.TSII15.Value = q2.First().tsii15; } else { rep.TSII15.Value = ""; }
                    if (q2.First().tsii16 > 0) { rep.TSII16.Value = q2.First().tsii16; } else { rep.TSII16.Value = ""; }
                    if (q2.First().tsii17 > 0) { rep.TSII17.Value = q2.First().tsii17; } else { rep.TSII17.Value = ""; }
                }
                var q = ((from k in khoa
                          join kb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on k.makp equals kb.MaKP
                          join bn in data.BenhNhans.Where(p => nt == 2 || p.NoiTru == nt) on kb.MaBNhan equals bn.MaBNhan
                          join kp in data.KPhongs on kb.MaKP equals kp.MaKP
                          group new { kb, bn, kp } by new { kp.TenKP, kp.MaKP } into kq
                          select new
                          {
                              Khoa = kq.Key.TenKP,
                              MaKP = kq.Key.MaKP,
                              TS = kq.Select(p => p.kb.MaBNhan).Count(),
                              BHYT = kq.Where(p => p.bn.DTuong == "BHYT").Select(p => p.kb.MaBNhan).Count(),
                              CapCuu = kq.Where(p => p.bn.CapCuu == 1).Select(p => p.kb.MaBNhan).Count(),
                              VaoVien = kq.Where(p => p.kb.PhuongAn == 1).Select(p => p.kb.MaBNhan).Count(),
                              //CVNang = kq.Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bnkb.MaBNhan).Count(),
                              //CVXin = kq.Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bnkb.MaBNhan).Count(),
                          }).ToList()).Select(p => new
                          {
                              p.Khoa,
                              p.MaKP,
                              TS = p.TS.ToString() == "0" ? null : p.TS.ToString(),
                              BHYT = p.BHYT.ToString() == "0" ? null : p.BHYT.ToString(),
                              CapCuu = p.CapCuu.ToString() == "0" ? null : p.CapCuu.ToString(),
                              VaoVien = p.VaoVien.ToString() == "0" ? null : p.VaoVien.ToString(),
                              //CVNang=p.CVNang.ToString()=="0"?null:p.CVNang.ToString(),
                              //CVXin=p.CVXin.ToString()=="0"?null:p.CVXin.ToString(),
                          }).ToList();

                var qdv = (from kp in khoa
                           join cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on kp.makp equals cls.MaKP
                           join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                           join dv in data.DichVus on cd.MaDV equals dv.MaDV
                           join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Nội soi") || p.TenRG.Contains("Thủ thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                           join bn in data.BenhNhans.Where(p => nt == 2 || p.NoiTru == nt) on cls.MaBNhan equals bn.MaBNhan
                           group new { cls, bn, dv, tnhom } by new { } into kq
                           select new
                           {
                               BCoTS = kq.Where(p => p.dv.TenDV.Contains("Bướu cổ") || p.dv.TenDV.Contains("bướu cổ")).Select(p => p.bn.MaBNhan).Count(),
                               //BCoTSBH = kq.Where(p=>p.bn.DTuong=="BHYT").Where(p => p.dv.TenDV.Contains("Bướu cổ") || p.dv.TenDV.Contains("bướu cổ")).Select(p => p.bn.MaBNhan).Count(),
                               BoBotTS = kq.Where(p => p.dv.TenDV.Contains("Bó bột") || p.dv.TenDV.Contains("bó bột")).Select(p => p.bn.MaBNhan).Count(),
                               // BoBotTSBH = kq.Where(p => p.bn.DTuong == "BHYT").Where(p => p.dv.TenDV.Contains("Bó bột") || p.dv.TenDV.Contains("bó bột")).Select(p => p.bn.MaBNhan).Count(),
                               DLiTS = kq.Where(p => p.dv.TenDV.Contains("Da liễu") || p.dv.TenDV.Contains("da liễu")).Select(p => p.bn.MaBNhan).Count(),
                               //  DLiBH = kq.Where(p => p.bn.DTuong == "BHYT").Where(p => p.dv.TenDV.Contains("Da liễu") || p.dv.TenDV.Contains("da liễu")).Select(p => p.bn.MaBNhan).Count(),
                               DHoTS = kq.Where(p => p.dv.TenDV.Contains("Đốt họng") || p.dv.TenDV.Contains("đốt họng")).Select(p => p.bn.MaBNhan).Count(),
                               //  DHoBH = kq.Where(p => p.bn.DTuong == "BHYT").Where(p => p.dv.TenDV.Contains("Đốt họng") || p.dv.TenDV.Contains("đốt họng")).Select(p => p.bn.MaBNhan).Count(),
                               NSTMHTS = kq.Where(p => p.dv.TenDV.Contains("Nội soi tai") || p.dv.TenDV.Contains("nội soi tai") || p.dv.TenDV.Contains("Nội soi mũi") || p.dv.TenDV.Contains("nội soi mũi")).Select(p => p.bn.MaBNhan).Count(),
                               //  NSTMHBH = kq.Where(p => p.bn.DTuong == "BHYT").Where(p => p.dv.TenDV.Contains("Nội soi tai") || p.dv.TenDV.Contains("nội soi tai") || p.dv.TenDV.Contains("Nội soi mũi") || p.dv.TenDV.Contains("nội soi mũi")).Select(p => p.bn.MaBNhan).Count(),
                               NSTHTS = kq.Where(p => p.dv.TenDV.Contains("Nội soi thực quản, dạ dầy, tá tràng") || p.dv.TenDV.Contains("soi thực quản") || p.dv.TenDV.Contains("soi dạ dày") || p.dv.TenDV.Contains("soi tá tràng")).Select(p => p.bn.MaBNhan).Count(),
                               //   NSTHBH = kq.Where(p => p.bn.DTuong == "BHYT").Where(p => p.dv.TenDV.Contains("Nội soi thực quản, dạ dầy, tá tràng") || p.dv.TenDV.Contains("soi thực quản") || p.dv.TenDV.Contains("soi dạ dày") || p.dv.TenDV.Contains("soi tá tràng")).Select(p => p.bn.MaBNhan).Count(),
                               TPhTS = kq.Where(p => p.dv.TenDV.Contains("Tiêm phòng") || p.dv.TenDV.Contains("tiêm phòng")).Select(p => p.bn.MaBNhan).Count(),
                               //   TPhBH = kq.Where(p => p.bn.DTuong == "BHYT").Where(p => p.dv.TenDV.Contains("Tiêm phòng") || p.dv.TenDV.Contains("tiêm phòng")).Select(p => p.bn.MaBNhan).Count(),
                               TPhauTS = kq.Where(p => p.tnhom.TenRG == "Tiểu phẫu" || p.tnhom.TenRG == "tiểu phẫu").Select(p => p.bn.MaBNhan).Count(),
                               //    TPhauBH = kq.Where(p => p.bn.DTuong == "BHYT").Where(p => p.tnhom.TenRG=="Thủ thuật").Select(p => p.bn.MaBNhan).Count(),
                               TS = kq.Select(p => p.bn.MaBNhan).Count(),
                               //     BH = kq.Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),

                               NSTMHngtTS = kq.Where(p => p.bn.NoiTru == 0 && (p.dv.TenDV.Contains("Nội soi tai") || p.dv.TenDV.Contains("nội soi tai") || p.dv.TenDV.Contains("Nội soi mũi") || p.dv.TenDV.Contains("nội soi mũi"))).Select(p => p.bn.MaBNhan).Count(),
                               NSTMHngtBH = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTuong == "BHYT").Where(p => p.dv.TenDV.Contains("Nội soi tai") || p.dv.TenDV.Contains("nội soi tai") || p.dv.TenDV.Contains("Nội soi mũi") || p.dv.TenDV.Contains("nội soi mũi")).Select(p => p.bn.MaBNhan).Count(),
                               NSTMHntTS = kq.Where(p => p.bn.NoiTru == 1 && (p.dv.TenDV.Contains("Nội soi tai") || p.dv.TenDV.Contains("nội soi tai") || p.dv.TenDV.Contains("Nội soi mũi") || p.dv.TenDV.Contains("nội soi mũi"))).Select(p => p.bn.MaBNhan).Count(),
                               NSTMHntBH = kq.Where(p => p.bn.NoiTru == 1 && p.bn.DTuong == "BHYT").Where(p => p.dv.TenDV.Contains("Nội soi tai") || p.dv.TenDV.Contains("nội soi tai") || p.dv.TenDV.Contains("Nội soi mũi") || p.dv.TenDV.Contains("nội soi mũi")).Select(p => p.bn.MaBNhan).Count(),
                               NSTHngtTS = kq.Where(p => p.bn.NoiTru == 0 && (p.dv.TenDV.Contains("Nội soi thực quản, dạ dầy, tá tràng") || p.dv.TenDV.Contains("soi thực quản") || p.dv.TenDV.Contains("soi dạ dày") || p.dv.TenDV.Contains("soi tá tràng"))).Select(p => p.bn.MaBNhan).Count(),
                               NSTHngtBH = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTuong == "BHYT").Where(p => p.dv.TenDV.Contains("Nội soi thực quản, dạ dầy, tá tràng") || p.dv.TenDV.Contains("soi thực quản") || p.dv.TenDV.Contains("soi dạ dày") || p.dv.TenDV.Contains("soi tá tràng")).Select(p => p.bn.MaBNhan).Count(),
                               NSTHntTS = kq.Where(p => p.bn.NoiTru == 1 && (p.dv.TenDV.Contains("Nội soi thực quản, dạ dầy, tá tràng") || p.dv.TenDV.Contains("soi thực quản") || p.dv.TenDV.Contains("soi dạ dày") || p.dv.TenDV.Contains("soi tá tràng"))).Select(p => p.bn.MaBNhan).Count(),
                               NSTHntBH = kq.Where(p => p.bn.NoiTru == 1 && p.bn.DTuong == "BHYT").Where(p => p.dv.TenDV.Contains("Nội soi thực quản, dạ dầy, tá tràng") || p.dv.TenDV.Contains("soi thực quản") || p.dv.TenDV.Contains("soi dạ dày") || p.dv.TenDV.Contains("soi tá tràng")).Select(p => p.bn.MaBNhan).Count(),
                           }).ToList();
                if (qdv.Count > 0)
                {
                    if (qdv.First().BCoTS > 0) { rep.BCoTS.Value = qdv.First().BCoTS; } else { rep.BCoTS.Value = ""; }
                    // if (qdv.First().BCoTSBH > 0) { rep.BCoBH.Value = qdv.First().BCoTS; } else { rep.BCoBH.Value = ""; }
                    if (qdv.First().BoBotTS > 0) { rep.BBotTS.Value = qdv.First().BoBotTS; } else { rep.BBotTS.Value = ""; }
                    // if (qdv.First().BoBotTSBH > 0) { rep.BBotBH.Value = qdv.First().BCoTS; } else { rep.BBotBH.Value = ""; }
                    if (qdv.First().DLiTS > 0) { rep.DLiTS.Value = qdv.First().DLiTS; } else { rep.DLiTS.Value = ""; }
                    // if (qdv.First().DLiBH > 0) { rep.DLiBH.Value = qdv.First().BCoTS; } else { rep.DLiBH.Value = ""; }
                    if (qdv.First().DHoTS > 0) { rep.DHoTS.Value = qdv.First().DHoTS; } else { rep.DHoTS.Value = ""; }
                    // if (qdv.First().DHoBH > 0) { rep.DHoBH.Value = qdv.First().BCoTS; } else { rep.DHoBH.Value = ""; }
                    if (qdv.First().NSTMHTS > 0) { rep.NSTMHTS.Value = qdv.First().NSTMHTS; } else { rep.NSTMHTS.Value = ""; }
                    //  if (qdv.First().NSTMHBH > 0) { rep.NSTMHBH.Value = qdv.First().NSTMHBH; } else { rep.NSTMHBH.Value = ""; }
                    if (qdv.First().NSTHTS > 0) { rep.NSTHTS.Value = qdv.First().NSTHTS; } else { rep.NSTHTS.Value = ""; }
                    // if (qdv.First().NSTHBH > 0) { rep.NSTHBH.Value = qdv.First().NSTHBH; } else { rep.NSTHBH.Value = ""; }
                    if (qdv.First().TPhTS > 0) { rep.TPhTS.Value = qdv.First().TPhTS; } else { rep.TPhTS.Value = ""; }
                    //  if (qdv.First().TPhBH > 0) { rep.TPhBH.Value = qdv.First().TPhBH; } else { rep.TPhBH.Value = ""; }
                    if (qdv.First().TPhauTS > 0) { rep.TPhauTS.Value = qdv.First().TPhauTS; } else { rep.TPhauTS.Value = ""; }
                    // if (qdv.First().TPhauBH > 0) { rep.TPhauBH.Value = qdv.First().TPhauBH; } else { rep.TPhauBH.Value = ""; }

                    if (qdv.First().NSTMHngtTS > 0) { rep.NSTMHNgTTS.Value = qdv.First().NSTMHngtTS; } else { rep.NSTMHNgTTS.Value = ""; }
                    if (qdv.First().NSTMHntTS > 0) { rep.NSTMHntTS.Value = qdv.First().NSTMHntTS; } else { rep.NSTMHntTS.Value = ""; }
                    if (qdv.First().NSTMHngtBH > 0) { rep.NSTMHNgTBH.Value = qdv.First().NSTMHngtBH; } else { rep.NSTMHNgTTS.Value = ""; }
                    if (qdv.First().NSTMHntBH > 0) { rep.NSTMHntBH.Value = qdv.First().NSTMHntBH; } else { rep.NSTMHntBH.Value = ""; }
                    if (qdv.First().NSTHngtTS > 0) { rep.NSTHngtTS.Value = qdv.First().NSTHngtTS; } else { rep.NSTHngtTS.Value = ""; }
                    if (qdv.First().NSTHntTS > 0) { rep.NSTHntTS.Value = qdv.First().NSTHntTS; } else { rep.NSTHntTS.Value = ""; }
                    if (qdv.First().NSTHngtBH > 0) { rep.NSTHngtBH.Value = qdv.First().NSTHngtBH; } else { rep.NSTHngtBH.Value = ""; }
                    if (qdv.First().NSTHntBH > 0) { rep.NSTHntBH.Value = qdv.First().NSTHntBH; } else { rep.NSTHntBH.Value = ""; }

                    if (qdv.First().TS > 0)
                    {
                        rep.DVTS.Value = qdv.First().TS;
                        rep.KhacTS.Value = qdv.First().TS - qdv.First().BoBotTS - qdv.First().DLiTS - qdv.First().DHoTS - qdv.First().NSTMHTS - qdv.First().NSTHTS - qdv.First().TPhTS - qdv.First().TPhauTS;
                    }
                    else { rep.KhacTS.Value = ""; }
                }
                var dthuocngtru = (from k in khoa
                                   join kb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on k.makp equals kb.MaKP
                                   join bn in data.BenhNhans.Where(p => nt == 2 || p.NoiTru == nt) on kb.MaBNhan equals bn.MaBNhan
                                   join nd in data.NhapDs.Where(p => p.PLoai == 2 && p.KieuDon == 0) on bn.MaBNhan equals nd.MaBNhan
                                   group new { k, kb, bn, nd } by new { bn.MaBNhan } into kq
                                   select new
                                   {
                                       kq.Key.MaBNhan
                                   }).ToList();
                if (dthuocngtru.Count > 0)
                    rep.DThuocBNNgTru.Value = dthuocngtru.Count;
                else
                    rep.DThuocBNNgTru.Value = "";
                //if (qdv.First().BH > 0)
                //{
                //    rep.DVBH.Value = qdv.First().BH;
                //    rep.KhacBH.Value = qdv.First().BH - qdv.First().BoBotTSBH - qdv.First().DLiBH - qdv.First().DHoBH - qdv.First().NSTMHBH - qdv.First().NSTHBH - qdv.First().TPhBH - qdv.First().TPhauBH; } 
                //else { rep.KhacBH.Value = ""; }

                //var qdvlan = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) 
                //              join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                //           join dv in data.DichVus on cd.MaDV equals dv.MaDV
                //           join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                //           join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                //           group new { bn, dv, tnhom } by new {} into kq
                //           select new
                //           {
                //               NSTMHngtTS = kq.Where(p => p.bn.NoiTru == 0 && (p.dv.TenDV.Contains("Nội soi tai") || p.dv.TenDV.Contains("nội soi tai") || p.dv.TenDV.Contains("Nội soi mũi") || p.dv.TenDV.Contains("nội soi mũi"))).Select(p => p.bn.MaBNhan).Count(),
                //               NSTMHntTS = kq.Where(p => p.bn.NoiTru == 1 && (p.dv.TenDV.Contains("Nội soi tai") || p.dv.TenDV.Contains("nội soi tai") || p.dv.TenDV.Contains("Nội soi mũi") || p.dv.TenDV.Contains("nội soi mũi"))).Select(p => p.bn.MaBNhan).Count(),
                //               NSTMHngtBH = kq.Where(p => p.bn.NoiTru == 0&&p.bn.DTuong=="BHYT" && (p.dv.TenDV.Contains("Nội soi tai") || p.dv.TenDV.Contains("nội soi tai") || p.dv.TenDV.Contains("Nội soi mũi") || p.dv.TenDV.Contains("nội soi mũi"))).Select(p => p.bn.MaBNhan).Count(),
                //               NSTMHntBH = kq.Where(p => p.bn.NoiTru == 1 && p.bn.DTuong == "BHYT" && (p.dv.TenDV.Contains("Nội soi tai") || p.dv.TenDV.Contains("nội soi tai") || p.dv.TenDV.Contains("Nội soi mũi") || p.dv.TenDV.Contains("nội soi mũi"))).Select(p => p.bn.MaBNhan).Count(),
                //               NSTHngtTS = kq.Where(p => p.bn.NoiTru == 0 && (p.dv.TenDV.Contains("Nội soi thực quản, dạ dầy, tá tràng") || p.dv.TenDV.Contains("soi thực quản") || p.dv.TenDV.Contains("soi dạ dày") || p.dv.TenDV.Contains("soi tá tràng"))).Select(p => p.bn.MaBNhan).Count(),
                //               NSTHntTS = kq.Where(p => p.bn.NoiTru == 1 &&( p.dv.TenDV.Contains("Nội soi thực quản, dạ dầy, tá tràng") || p.dv.TenDV.Contains("soi thực quản") || p.dv.TenDV.Contains("soi dạ dày") || p.dv.TenDV.Contains("soi tá tràng"))).Select(p => p.bn.MaBNhan).Count(),
                //               NSTHngtBH = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTuong == "BHYT" && (p.dv.TenDV.Contains("Nội soi thực quản, dạ dầy, tá tràng") || p.dv.TenDV.Contains("soi thực quản") || p.dv.TenDV.Contains("soi dạ dày") || p.dv.TenDV.Contains("soi tá tràng"))).Select(p => p.bn.MaBNhan).Count(),
                //               NSTHntBH = kq.Where(p => p.bn.NoiTru == 1 && p.bn.DTuong == "BHYT" && (p.dv.TenDV.Contains("Nội soi thực quản, dạ dầy, tá tràng") || p.dv.TenDV.Contains("soi thực quản") || p.dv.TenDV.Contains("soi dạ dày") || p.dv.TenDV.Contains("soi tá tràng"))).Select(p => p.bn.MaBNhan).Count(),
                //           }).ToList();
                //if (qdvlan.First().NSTMHngtTS > 0) { rep.NSTMHNgTTS.Value = qdvlan.First().NSTMHngtTS; } else { rep.NSTMHNgTTS.Value = ""; }
                // if (qdvlan.First().NSTMHntTS > 0) { rep.NSTMHntTS.Value = qdvlan.First().NSTMHntTS; } else { rep.NSTMHntTS.Value = ""; }
                //if (qdvlan.First().NSTHngtTS > 0) { rep.NSTHngtTS.Value = qdvlan.First().NSTHngtTS; } else { rep.NSTHngtTS.Value = ""; }
                //if (qdvlan.First().NSTHntTS > 0) { rep.NSTHntTS.Value = qdvlan.First().NSTHntTS; } else { rep.NSTHntTS.Value = ""; }

                rep.TuNgay.Value = tungay; rep.DenNgay.Value = denngay;
                if (chkIn.Checked == false)
                {
                    rep.TieuDe.Value = ("báo cáo khoa khám bệnh từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10)).ToUpper();
                }
                else { rep.TieuDe.Value = ("báo cáo khoa khám bệnh tháng " + denngay.Month + " năm " + denngay.Year).ToUpper(); }
                if (_Kphong.First().chon == true)
                { rep.Phong.Value = ("Khoa khám bệnh").ToUpper(); }
                else
                {
                    var qkp = (from k in khoa
                               join kp in data.KPhongs on k.makp equals kp.MaKP
                               select new { kp.TenKP }).Distinct().OrderBy(p => p.TenKP).ToList();
                    if (qkp.Count > 0)
                    {
                        int i = qkp.Count();
                        if (i == 0) { rep.Phong.Value = ("Khoa khám bệnh ").ToUpper(); }
                        if (i == 1) { rep.Phong.Value = qkp.First().TenKP; }
                        if (i == 2) { rep.Phong.Value = qkp.First().TenKP + ", " + qkp.Skip(1).First().TenKP; }
                        if (i == 3) { rep.Phong.Value = qkp.First().TenKP + ", " + qkp.Skip(1).First().TenKP + ", " + qkp.Skip(2).First().TenKP; }
                        if (i == 4) { rep.Phong.Value = qkp.First().TenKP + ", " + qkp.Skip(1).First().TenKP + ", " + qkp.Skip(2).First().TenKP + ", " + qkp.Skip(3).First().TenKP; }
                        if (i == 5) { rep.Phong.Value = qkp.First().TenKP + ", " + qkp.Skip(1).First().TenKP + ", " + qkp.Skip(2).First().TenKP + ", " + qkp.Skip(3).First().TenKP + ", " + qkp.Skip(4).First().TenKP; }
                        if (i == 6) { rep.Phong.Value = qkp.First().TenKP + ", " + qkp.Skip(1).First().TenKP + ", " + qkp.Skip(2).First().TenKP + ", " + qkp.Skip(3).First().TenKP + ", " + qkp.Skip(4).First().TenKP + ", " + qkp.Skip(5).First().TenKP; }
                        if (i == 7) { rep.Phong.Value = qkp.First().TenKP + ", " + qkp.Skip(1).First().TenKP + ", " + qkp.Skip(2).First().TenKP + ", " + qkp.Skip(3).First().TenKP + ", " + qkp.Skip(4).First().TenKP + ", " + qkp.Skip(5).First().TenKP + ", " + qkp.Skip(6).First().TenKP; }
                        if (i == 8) { rep.Phong.Value = qkp.First().TenKP + ", " + qkp.Skip(1).First().TenKP + ", " + qkp.Skip(2).First().TenKP + ", " + qkp.Skip(3).First().TenKP + ", " + qkp.Skip(4).First().TenKP + ", " + qkp.Skip(5).First().TenKP + ", " + qkp.Skip(6).First().TenKP + ", " + qkp.Skip(7).First().TenKP; }
                        if (i > 8) { rep.Phong.Value = ("Khoa khám bệnh ").ToUpper(); }
                    }
                }
                rep.BindingData();
                rep.DataSource = q.ToList();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
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