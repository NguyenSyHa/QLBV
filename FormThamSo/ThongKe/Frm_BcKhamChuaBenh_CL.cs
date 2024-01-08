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
    public partial class Frm_BcKhamChuaBenh_CL : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcKhamChuaBenh_CL()
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
            if (lupKhoa.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn khoa");
                lupKhoa.Focus();
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
        List<KPhong> _Kphong = new List<KPhong>();
        private void Frm_BcKhamChuaBenh_CL_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            var kphong = (from kp in data.KPhongs.Where(p =>p.PLoai=="Lâm sàng")
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
               
            }
            lupKhoa.Properties.DataSource = _Kphong.ToList();
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
                BaoCao.Rep_BcKhamChuaBenh_CL rep = new BaoCao.Rep_BcKhamChuaBenh_CL();
                 if (chkIn.Checked == true)
                {
                    rep.Thang.Value = "Tháng " + Convert.ToDateTime(lupDenNgay.Text).Month + " năm " + Convert.ToDateTime(lupDenNgay.Text).Year;
                }
                else
                {
                    rep.Thang.Value = "(Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text + ")";

                }
                int _makp = 0;
                if (lupKhoa.EditValue != null) { _makp = Convert.ToInt32( lupKhoa.EditValue); }

                List<int>  qrv = (from rv in data.RaViens select rv.MaBNhan).ToList();
                var qbn2 = (from vv in data.VaoViens
                           join bn in data.BenhNhans.Where(p => p.NoiTru == 1) on vv.MaBNhan equals bn.MaBNhan
                           select new { vv.MaKP, vv.MaBNhan, bn.DTuong, bn.Tuoi, bn.GTinh, vv.NgayVao, bn.CapCuu, bn.NgoaiGio }).ToList();
                var qbn = (from vv in qbn2
                          where  (vv.MaBNhan == null || !(qrv.Contains(vv.MaBNhan)))
                           select new { vv.MaKP, vv.MaBNhan, vv.DTuong, vv.Tuoi, vv.GTinh, vv.NgayVao, vv.CapCuu, vv.NgoaiGio }).ToList();
                var id = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay)
                          group kb by kb.MaBNhan into kq
                          select new { kq.Key, IDKB = kq.Max(p => p.IDKB)}).ToList();
             
                    var qbnnt = (from k in qbn.Where(p =>_makp==0?true: p.MaKP == _makp)
                                 group k by new { k.MaBNhan } into kq
                                 select new
                                 {
                                     TS = kq.Select(p => p.MaBNhan).Count(),
                                     TS_BHYT = kq.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Count(),
                                     CapCuu = kq.Where(p => p.CapCuu == 1).Select(p => p.MaBNhan).Count(),
                                     CapCuuTE = kq.Where(p => p.CapCuu == 1).Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Count(),
                                     CapCuuTE1 = kq.Where(p => p.CapCuu == 1).Where(p => p.Tuoi < 1).Select(p => p.MaBNhan).Count(),
                                     TSTE = kq.Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Count(),
                                     TSTE1 = kq.Where(p => p.Tuoi < 1).Select(p => p.MaBNhan).Count(),
                                     TSTE1_6 = kq.Where(p => p.Tuoi > 1 && p.Tuoi < 6).Select(p => p.MaBNhan).Count(),
                                     TSNL = kq.Where(p => p.Tuoi > 6).Select(p => p.MaBNhan).Count(),
                                     TSNLNu = kq.Where(p => p.Tuoi > 6 && p.GTinh == 0).Select(p => p.MaBNhan).Count(),
                                     BNDKy = kq.Where(p => p.NgayVao <= tungay).Select(p => p.MaBNhan).Count(),
                                     BNTKy = kq.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Select(p => p.MaBNhan).Count(),
                                     BNTKyNG = kq.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Where(p => p.NgoaiGio == 1).Select(p => p.MaBNhan).Count(),
                                     BNCKy = kq.Where(p => p.NgayVao <= denngay).Select(p => p.MaBNhan).Count(),
                                 }).ToList();

                    if (qbnnt.Count > 0)
                    {
                        rep.TS.Value = qbnnt.Sum(p => p.TS) == 0 ? null : qbnnt.Sum(p => p.TS).ToString();
                        rep.TS_BHYT.Value = qbnnt.Sum(p => p.TS_BHYT) == 0 ? null : qbnnt.Sum(p => p.TS_BHYT).ToString();
                        rep.TS_CC.Value = qbnnt.Sum(p => p.CapCuu) == 0 ? null : qbnnt.Sum(p => p.CapCuu).ToString();
                        rep.TS_TECC.Value = qbnnt.Sum(p => p.CapCuuTE) == 0 ? null : qbnnt.Sum(p => p.CapCuuTE).ToString();
                        rep.TS_TECC1.Value = qbnnt.Sum(p => p.CapCuuTE1) == 0 ? null : qbnnt.Sum(p => p.CapCuuTE1).ToString();
                        rep.BNDKy.Value = qbnnt.Sum(p => p.BNDKy) == 0 ? null : qbnnt.Sum(p => p.BNDKy).ToString();
                        rep.VVTKy.Value = qbnnt.Sum(p => p.BNTKy) == 0 ? null : qbnnt.Sum(p => p.BNTKy).ToString();
                        rep.VVTKyHC.Value = qbnnt.Sum(p => p.BNTKyNG) == 0 ? null : qbnnt.Sum(p => p.BNTKyNG).ToString();
                        rep.TSBNTE.Value = qbnnt.Sum(p => p.TSTE) == 0 ? null : qbnnt.Sum(p => p.TSTE).ToString();
                        rep.TSBNTE1.Value = qbnnt.Sum(p => p.TSTE1) == 0 ? null : qbnnt.Sum(p => p.TSTE1).ToString();
                        rep.TSBNTE1_6.Value = qbnnt.Sum(p => p.TSTE1_6) == 0 ? null : qbnnt.Sum(p => p.TSTE1_6).ToString();
                        rep.TSBNNL.Value = qbnnt.Sum(p => p.TSNL) == 0 ? null : qbnnt.Sum(p => p.TSNL).ToString();
                        rep.TSBNNLNu.Value = qbnnt.Sum(p => p.TSNLNu) == 0 ? null : qbnnt.Sum(p => p.TSNLNu).ToString();

                    }
                    var qbnrv2 = (from k in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                  join bn in data.BenhNhans on k.MaBNhan equals bn.MaBNhan
                                  select new {bn.Tuoi,bn.GTinh, k.KetQua, bn.DTuong, bn.MaBNhan }).ToList();
                    var qbnrv = (from k in qbnrv2
                                 group new { k } by new { } into kq
                                 select new
                                 {
                                     TS = kq.Select(p => p.k.MaBNhan).Count(),
                                     TS_BHYT = kq.Where(p => p.k.DTuong == "BHYT").Select(p => p.k.MaBNhan).Count(),
                                     BNKhoi = kq.Where(p => p.k.KetQua == "Khỏi").Select(p => p.k.MaBNhan).Count(),
                                     BNDo = kq.Where(p => p.k.KetQua == "Đỡ|Giảm").Select(p => p.k.MaBNhan).Count(),
                                     BNKoTD = kq.Where(p => p.k.KetQua == "Không T.đổi").Select(p => p.k.MaBNhan).Count(),
                                     BNNang = kq.Where(p => p.k.KetQua == "Nặng hơn").Select(p => p.k.MaBNhan).Count(),

                                     //BNCV=kq.Where(p=>p.idkb.PA==3).Select(p=>p.k.MaBNhan).Count(),
                                     //BNCVXin = kq.Where(p => p.idkb.PA == 3&&p.k.LyDoC.Contains("Không đủ")).Select(p => p.k.MaBNhan).Count(),

                                     TERV = kq.Where(p => p.k.Tuoi <= 6).Select(p => p.k.MaBNhan).Count(),
                                     TERV_BHYT = kq.Where(p => p.k.Tuoi <= 6 && p.k.DTuong == "BHYT").Select(p => p.k.MaBNhan).Count(),
                                     TERV1 = kq.Where(p => p.k.Tuoi < 1).Select(p => p.k.MaBNhan).Count(),
                                     TERV1_6 = kq.Where(p => p.k.Tuoi >= 1 && p.k.Tuoi <= 6).Select(p => p.k.MaBNhan).Count(),
                                     TERV6 = kq.Where(p => p.k.Tuoi <= 6).Select(p => p.k.MaBNhan).Count(),

                                     NLRV = kq.Where(p => p.k.Tuoi > 6).Select(p => p.k.MaBNhan).Count(),
                                     NLRV_BHYT = kq.Where(p => p.k.Tuoi > 6 && p.k.DTuong == "BHYT").Select(p => p.k.MaBNhan).Count(),

                                     BN60RV = kq.Where(p => p.k.Tuoi >= 60).Select(p => p.k.MaBNhan).Count(),
                                     BN60RV_BHYT = kq.Where(p => p.k.Tuoi >= 60 && p.k.DTuong == "BHYT").Select(p => p.k.MaBNhan).Count(),

                                     BNTVong = kq.Where(p => p.k.KetQua == "Tử vong").Select(p => p.k.MaBNhan).Count(),
                                     BNTVongTE = kq.Where(p => p.k.Tuoi <= 6 && p.k.KetQua == "Tử vong").Select(p => p.k.MaBNhan).Count(),
                                     BNTVongTE1 = kq.Where(p => p.k.Tuoi < 1 && p.k.KetQua == "Tử vong").Select(p => p.k.MaBNhan).Count(),
                                     BNTVongTE1_6 = kq.Where(p => p.k.Tuoi <= 1 && p.k.Tuoi <= 6 && p.k.KetQua == "Tử vong").Select(p => p.k.MaBNhan).Count(),

                                     BNTVongNL = kq.Where(p => p.k.Tuoi > 6 && p.k.KetQua == "Tử vong").Select(p => p.k.MaBNhan).Count(),
                                     BNTVongNLNu = kq.Where(p => p.k.Tuoi > 1 && p.k.GTinh == 0 && p.k.KetQua == "Tử vong").Select(p => p.k.MaBNhan).Count(),
                                 }).ToList();

                    if (qbnnt.Count > 0)
                    {
                        rep.TSBNRV.Value = qbnrv.Sum(p => p.TS) == 0 ? null : qbnrv.Sum(p => p.TS).ToString();
                        rep.TSBNRV_BHYT.Value = qbnrv.Sum(p => p.TS_BHYT) == 0 ? null : qbnrv.Sum(p => p.TS_BHYT).ToString();
                        rep.BNRV_Khoi.Value = qbnrv.Sum(p => p.BNKhoi) == 0 ? null : qbnrv.Sum(p => p.BNKhoi).ToString();
                        rep.BNRV_Do.Value = qbnrv.Sum(p => p.BNDo) == 0 ? null : qbnrv.Sum(p => p.BNDo).ToString();
                        rep.BNRV_KoTD.Value = qbnrv.Sum(p => p.BNKoTD) == 0 ? null : qbnrv.Sum(p => p.BNKoTD).ToString();
                        rep.BNRV_Nang.Value = qbnrv.Sum(p => p.BNNang) == 0 ? null : qbnrv.Sum(p => p.BNNang).ToString();

                        rep.TERV.Value = qbnrv.Sum(p => p.TERV) == 0 ? null : qbnrv.Sum(p => p.TERV).ToString();
                        rep.TERV_BHYT.Value = qbnrv.Sum(p => p.TERV_BHYT) == 0 ? null : qbnrv.Sum(p => p.TERV_BHYT).ToString();
                        rep.TERV_1.Value = qbnrv.Sum(p => p.TERV1) == 0 ? null : qbnrv.Sum(p => p.TERV1).ToString();
                        rep.TERV_1_6.Value = qbnrv.Sum(p => p.TERV1_6) == 0 ? null : qbnrv.Sum(p => p.TERV1_6).ToString();

                        rep.NLRV.Value = qbnrv.Sum(p => p.NLRV) == 0 ? null : qbnrv.Sum(p => p.NLRV).ToString();
                        rep.NLRV_BHYT.Value = qbnrv.Sum(p => p.NLRV_BHYT) == 0 ? null : qbnrv.Sum(p => p.NLRV_BHYT).ToString();

                        rep.BN60RV.Value = qbnrv.Sum(p => p.BN60RV) == 0 ? null : qbnrv.Sum(p => p.BN60RV).ToString();
                        rep.BN60RV_BHYT.Value = qbnrv.Sum(p => p.BN60RV_BHYT) == 0 ? null : qbnrv.Sum(p => p.BN60RV_BHYT).ToString();
                        rep.BNTV.Value = qbnrv.Sum(p => p.BNTVong) == 0 ? null : qbnrv.Sum(p => p.BNTVong).ToString();
                        rep.TVTE.Value = qbnrv.Sum(p => p.BNTVongTE) == 0 ? null : qbnrv.Sum(p => p.BNTVongTE).ToString();
                        rep.TVTE1.Value = qbnrv.Sum(p => p.BNTVongTE1) == 0 ? null : qbnrv.Sum(p => p.BNTVongTE1).ToString();
                        rep.TVTE1_6.Value = qbnrv.Sum(p => p.BNTVongTE1_6) == 0 ? null : qbnrv.Sum(p => p.BNTVongTE1_6).ToString();
                        rep.TVNL.Value = qbnrv.Sum(p => p.BNTVongNL) == 0 ? null : qbnrv.Sum(p => p.BNTVongNL).ToString();
                        rep.TVNL_Nu.Value = qbnrv.Sum(p => p.BNTVongNLNu) == 0 ? null : qbnrv.Sum(p => p.BNTVongNLNu).ToString();
                    }
                    var qcv = (from kb in data.BNKBs.Where(p => p.PhuongAn == 3)
                               join rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => _makp == 0 ? true : p.MaKP == _makp) on kb.MaBNhan equals rv.MaBNhan
                               group new { kb, rv } by new { } into kq
                               select new
                               {
                                   BNCV = kq.Select(p => p.kb.MaBNhan).Count(),
                                   BNCVXin = kq.Where(p => p.rv.KetQua == "Không đủ điều kiện chuyển tuyến/chuyển tuyến theo yêu cầu người bệnh...(vượt tuyến)").Select(p => p.kb.MaBNhan).Count(),
                               }).ToList();
                    if (qcv.Count > 0)
                    {
                        rep.BNCV.Value = qcv.Sum(p => p.BNCV) == 0 ? null : qcv.Sum(p => p.BNCV).ToString();
                        rep.BNCV_Xin.Value = qcv.Sum(p => p.BNCVXin) == 0 ? null : qcv.Sum(p => p.BNCVXin).ToString();

                    }
                    rep.TSBNCKy.Value = qbnnt.Sum(p => p.BNCKy) - qbnrv.Sum(p => p.TS) == 0 ? null : (qbnnt.Sum(p => p.BNCKy) - qbnrv.Sum(p => p.TS)).ToString();
                    var dvu = (from dv in data.DichVus
                              join tnhom in data.TieuNhomDVs.Where(p => p.TenRG == "Thủ thuật") on dv.IdTieuNhom equals tnhom.IdTieuNhom
                              select new { dv.MaDV, dv.Loai }).ToList();
                    var qtt3 = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                join bn in data.BenhNhans.Where(p => p.NoiTru == 1) on cls.MaBNhan equals bn.MaBNhan
                                join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                select new { cls.MaBNhan,cd.MaDV  }).ToList();
                    var qtt1 = (from cls in qtt3
                                join dv in dvu on cls.MaDV equals dv.MaDV
                                select new { cls.MaBNhan, dv.Loai }).ToList();
                    var qtt = (from qt in qtt1
                               group qt by new { qt.MaBNhan } into kq
                               select new
                               {
                                   BNTT = kq.Select(p => p.MaBNhan).Count(),
                                   BNL1 = kq.Where(p => p.Loai == 1).Select(p => p.MaBNhan).Count(),
                                   BNL2 = kq.Where(p => p.Loai == 2).Select(p => p.MaBNhan).Count(),
                                   BNL3 = kq.Where(p => p.Loai == 3).Select(p => p.MaBNhan).Count(),
                               }).ToList();
                    if (qtt.Count > 0)
                    {
                        rep.TSThuT.Value = qtt.Sum(p => p.BNTT) == 0 ? null : qtt.Sum(p => p.BNTT).ToString();
                        rep.TSThuT_L1.Value = qtt.Sum(p => p.BNL1) == 0 ? null : qtt.Sum(p => p.BNL1).ToString();
                        rep.TSThuT_L2.Value = qtt.Sum(p => p.BNL2) == 0 ? null : qtt.Sum(p => p.BNL2).ToString();
                        rep.TSThuT_L3.Value = qtt.Sum(p => p.BNL3) == 0 ? null : qtt.Sum(p => p.BNL3).ToString();

                    }
                    var bnngt = (from vv in data.VaoViens.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                 join bn in data.BenhNhans.Where(p => p.NoiTru == 0) on vv.MaBNhan equals bn.MaBNhan
                                 group new { vv } by new { vv.MaBNhan } into kq
                                 select new { MaBN = kq.Select(p => p.vv.MaBNhan).Count() }).ToList();
                    if (bnngt.Count > 0)
                    {
                        rep.BNDTNgT.Value = bnngt.Sum(p => p.MaBN) == 0 ? null : bnngt.Sum(p => p.MaBN).ToString();
                    }
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

        }
    }
}