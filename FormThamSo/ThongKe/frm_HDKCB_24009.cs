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
    public partial class frm_HDKCB_24009 : DevExpress.XtraEditors.XtraForm
    {
        public frm_HDKCB_24009()
        {
            InitializeComponent();
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;
        }

        QLBV_Database.QLBVEntities data;
        int mackhoa(string tenck, List<ChuyenKhoa> lck)
        {
            if (tenck == null)
                return 99;

            var ten = lck.Where(p => p.TenCK == tenck.ToString()).Select(p => p.MaCK).ToList();
            if (ten.Count > 0)
                return ten.First();
            else return 99;
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
            List<ChuyenKhoa> lck = data.ChuyenKhoas.ToList();
            List<int> _listDTBN = new List<int>();
            for (int i = 0; i < cklDTBN.ItemCount; i++)
            {
                if (cklDTBN.GetItemChecked(i))
                    _listDTBN.Add(Convert.ToInt32(cklDTBN.GetItemValue(i)));
            }
            if ((lupngayden.DateTime - lupNgaytu.DateTime).Days < 0)
            { MessageBox.Show("Ngày đến phải lớn hơn hoặc bằng ngày từ"); }
            else
            {
                //    if ((lupngayden.DateTime - lupNgaytu.DateTime.AddMonths(3)).Days <= 0)
                //        strThang = "3";
                //    else if ((lupngayden.DateTime - lupNgaytu.DateTime.AddMonths(6)).Days <= 0)
                //        strThang = "6";
                //    else if ((lupngayden.DateTime - lupNgaytu.DateTime.AddMonths(9)).Days <= 0)
                //        strThang = "9";
                //    else
                //        strThang = "12";

                var q11 = (from bn in data.BenhNhans
                           join dtbn in data.DTBNs.Where(p => _listDTBN.Contains(p.IDDTBN)) on bn.IDDTBN equals dtbn.IDDTBN
                           join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                           join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan into kq2
                           from kq1 in kq.DefaultIfEmpty()
                           from kq3 in kq2.DefaultIfEmpty()
                           where bn.NoiTru == 0 ? (bn.NNhap >= ngaytu && bn.NNhap <= ngayden) : (
                           (kq3 != null && kq3.NgayVao < ngaytu && kq1!=null && kq3.NgayVao > ngayden)||
                           (kq3 != null && kq3.NgayVao >= ngaytu && kq3.NgayVao <= ngayden) ||
                           (kq1 != null && (kq1.NgayRa >= ngaytu && kq1.NgayRa <= ngayden)) || (kq3 != null && kq3.NgayVao < ngaytu && kq3.NgayVao > ngayden))
                           select new
                           {

                               bn.MaBNhan,
                               bn.TuyenDuoi,
                               bn.Tuoi,
                               bn.NNhap,
                               bn.NgaySinh,
                               bn.ThangSinh,
                               bn.NamSinh,
                               bn.GTinh,
                               bn.SThe,
                               bn.NoiTru,
                               MaCSKCB = bn.TuyenDuoi == 0 ? DungChung.Bien.MaBV : (bn.ChuyenKhoa == null ? "" : bn.ChuyenKhoa),
                               NgayRa = kq1 != null ? kq1.NgayRa : null,
                               MaCK = kq1 != null ? kq1.MaCK : 99,
                               ckhoa = kq3 != null ? kq3.ChuyenKhoa : "",
                               SoNgaydt = kq1 != null ? kq1.SoNgaydt : 0,
                               KetQua = kq1 != null ? kq1.KetQua : "",
                               dtbn.DTBN1,
                               vvien = kq3 != null ? kq3.MaBNhan : 0
                           }
                          ).ToList();
                var q0 = (from bn in q11
                          select new
                          {
                              bn.MaBNhan,
                              bn.TuyenDuoi,
                              bn.Tuoi,
                              bn.NNhap,
                              bn.NgaySinh,
                              bn.ThangSinh,
                              bn.NamSinh,
                              bn.GTinh,
                              bn.SThe,
                              bn.NoiTru,
                              bn.MaCSKCB,
                              bn.NgayRa,
                              MaCK = bn.MaCK == 99 ? mackhoa(bn.ckhoa, lck) : bn.MaCK,
                              bn.SoNgaydt,
                              bn.KetQua,
                              bn.DTBN1,
                              bn.vvien
                          }
                         ).ToList();
                var bv = (from a in data.BenhViens select new { a.TenBV, a.MaBV, a.HangBV }).Where(p => p.MaBV != null).ToList();

                var q1 = (from bn in q0
                          join bv1 in data.BenhViens on bn.MaCSKCB equals bv1.MaBV into kq
                          from kq1 in kq.DefaultIfEmpty()
                          select new { bn.vvien, bn.MaBNhan, bn.TuyenDuoi, bn.Tuoi, bn.NNhap, bn.NgaySinh, bn.ThangSinh, bn.NamSinh, bn.GTinh, bn.SThe, bn.NoiTru, bn.MaCSKCB, bn.NgayRa, bn.MaCK, bn.SoNgaydt, bn.DTBN1, bn.KetQua, HangBV = kq1 == null ? -1 : (kq1.HangBV == null ? -1 : kq1.HangBV.Value), TenBV = kq1 == null ? "" : (kq1.TenBV == null ? "" : kq1.TenBV) }).ToList();

                if (radBieu.SelectedIndex == 0)
                {
                    var q2 = (from bn in q1
                              join ck in data.ChuyenKhoas on bn.MaCK equals ck.MaCK into kq
                              from kq1 in kq.DefaultIfEmpty()
                              select new
                              {
                                  bn.MaBNhan,
                                  bn.TuyenDuoi,
                                  bn.Tuoi,
                                  bn.GTinh,
                                  bn.SThe,
                                  bn.NoiTru,
                                  bn.MaCSKCB,
                                  bn.NgayRa,
                                  bn.MaCK,
                                  bn.SoNgaydt,
                                  bn.DTBN1,
                                  bn.HangBV,
                                  bn.TenBV,
                                  TenCK = kq1 == null ? "" : kq1.TenCK,
                                  bn.vvien

                              }).ToList();
                    var q3 = q2.Distinct().ToList();
                    var q = (from bn in q3
                             group bn by new { bn.MaCSKCB, bn.TenBV } into kq
                             select new
                             {
                                 Tuyen = kq.Key.MaCSKCB == null ? 3 : (kq.Key.MaCSKCB == DungChung.Bien.MaBV ? 1 : 2),
                                 STTNhom = kq.Key.MaCSKCB == null ? "III" : (kq.Key.MaCSKCB == DungChung.Bien.MaBV ? "I" : "II"),
                                 TenNhom = kq.Key.MaCSKCB == null ? "" : (kq.Key.MaCSKCB == DungChung.Bien.MaBV ? "Tuyến huyện" : "Tuyến xã"),
                                 CSYT = kq.Key.TenBV,
                                 TongsoKCB = kq.Where(p => p.NoiTru == 0).Count(),
                                 nu_kcb = kq.Where(p => p.NoiTru == 0).Where(p => p.GTinh == 0).Count(),
                                 BHYT_kcb = kq.Where(p => p.NoiTru == 0).Where(p => p.DTBN1 == "BHYT").Count(),
                                 YHCT_kcb = kq.Where(p => p.NoiTru == 0).Where(p => p.TenCK == "Đông y").Count(),
                                 TE_kcb = kq.Where(p => p.NoiTru == 0).Where(p => p.Tuoi < 15).Count(),
                                 Tongso_dtri = kq.Where(p => p.NoiTru == 1 && p.vvien > 0).Count(),
                                 Nu_dtri = kq.Where(p => p.NoiTru == 1 && p.vvien > 0).Where(p => p.GTinh == 0).Count(),
                                 BHYT_dtri = kq.Where(p => p.NoiTru == 1 && p.vvien > 0).Where(p => p.DTBN1 == "BHYT").Count(),
                                 YHCT_dtri = kq.Where(p => p.NoiTru == 1 && p.vvien > 0).Where(p => p.TenCK == "Đông y").Count(),
                                 TE_dtri = kq.Where(p => p.NoiTru == 1 && p.vvien > 0).Where(p => p.Tuoi < 15).Count(),
                                 TSngaydtrinoitru = kq.Where(p => p.NoiTru == 1 && p.vvien > 0).Sum(p => p.SoNgaydt)
                             }).OrderBy(p => p.Tuyen).ThenBy(p => p.CSYT).ToList();
                    BaoCao.rep_HDKCB_24009 rep = new BaoCao.rep_HDKCB_24009();
                    if (txtNgayThang.Text.Trim() != "")
                        rep.lab_tungaydenngay.Text = txtNgayThang.Text;
                    else
                        rep.lab_tungaydenngay.Text = "Từ ngày " + lupNgaytu.DateTime.ToShortDateString() + " đến ngày " + lupngayden.DateTime.ToShortDateString();
                    frmIn frm = new frmIn();
                    rep.DataSource = q;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    var q2vd = (from rv in data.RaViens
                                where rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden
                                join vp in data.VienPhis on rv.MaBNhan equals vp.MaBNhan
                                join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                select new
                                {
                                    rv.MaBNhan,
                                    vpct.idVPhict,
                                    vpct.MaKP,
                                    vpct.MaDV,
                                    vpct.SoLuong,
                                }).ToList();
                    var dichvu = (from dv in data.DichVus join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom select new { dv.MaDV, tn.TenRG }).ToList();
                    var q2 = (from a in q0
                              join b in q2vd on a.MaBNhan equals b.MaBNhan
                              join c in bv on a.MaCSKCB equals c.MaBV
                              select new
                              {
                                  b.MaDV,
                                  a.MaBNhan,
                                  a.TuyenDuoi,
                                  a.Tuoi,
                                  a.NNhap,
                                  a.NgaySinh,
                                  a.ThangSinh,
                                  a.NamSinh,
                                  a.GTinh,
                                  a.SThe,
                                  a.NoiTru,
                                  a.MaCSKCB,
                                  a.NgayRa,
                                  a.MaCK,
                                  a.SoNgaydt,
                                  a.DTBN1,
                                  c.HangBV,
                                  c.TenBV,
                                  a.KetQua,
                                  b.idVPhict,
                                  b.MaKP,
                                  b.SoLuong
                              }).ToList();

                    var q4 = (from bn in q2
                              join d in dichvu on bn.MaDV equals d.MaDV
                              group new { bn, d } by new
                              {
                                  bn.MaBNhan,
                                  bn.TuyenDuoi,
                                  bn.Tuoi,
                                  bn.NNhap,
                                  bn.NgaySinh,
                                  bn.ThangSinh,
                                  bn.NamSinh,
                                  bn.GTinh,
                                  bn.SThe,
                                  bn.NoiTru,
                                  bn.MaCSKCB,
                                  bn.NgayRa,
                                  bn.MaCK,
                                  bn.SoNgaydt,
                                  bn.DTBN1,
                                  bn.HangBV,
                                  bn.TenBV,
                                  bn.MaKP,
                                  bn.KetQua,
                                  d.TenRG,
                              } into kq
                              select new
                              {
                                  kq.Key.TenRG,
                                  kq.Key.MaBNhan,
                                  kq.Key.TuyenDuoi,
                                  kq.Key.Tuoi,
                                  kq.Key.NNhap,
                                  kq.Key.NgaySinh,
                                  kq.Key.ThangSinh,
                                  kq.Key.NamSinh,
                                  kq.Key.GTinh,
                                  kq.Key.SThe,
                                  kq.Key.NoiTru,
                                  kq.Key.MaCSKCB,
                                  kq.Key.NgayRa,
                                  kq.Key.MaCK,
                                  kq.Key.SoNgaydt,
                                  kq.Key.DTBN1,
                                  kq.Key.HangBV,
                                  kq.Key.TenBV,
                                  kq.Key.KetQua,
                                  SoLuot = kq.Sum(p => p.bn.SoLuong)
                              }).ToList();
                    var q5 = (from a in q0
                              select new
                              {
                                  a.MaBNhan,
                                  a.TuyenDuoi,
                                  Tuoi1 = CheckTuoi1(a.NNhap, a.NgaySinh, a.ThangSinh, a.NamSinh, a.Tuoi),
                                  a.Tuoi,
                                  a.GTinh,
                                  a.DTBN1,
                                  MaCSKCB = a.MaCSKCB,
                                  a.KetQua,
                              }).ToList();
                    var q = (from bn in q4
                             group bn by new { bn.MaCSKCB, bn.TenBV } into kq
                             select new
                             {
                                 Tuyen = kq.Key.MaCSKCB == DungChung.Bien.MaBV ? 1 : 2,
                                 STTNhom = kq.Key.MaCSKCB == DungChung.Bien.MaBV ? "I" : "II",
                                 TenNhom = kq.Key.MaCSKCB == DungChung.Bien.MaBV ? "Tuyến huyện" : "Trạm y tế",
                                 Cosoyte = kq.Key.TenBV,
                                 TSluotkhamduphong = q5.Where(p => p.MaCSKCB == kq.Key.MaCSKCB).Where(p => p.DTBN1 == "KSK").Count(),
                                 TStuvong = q5.Where(p => p.MaCSKCB == kq.Key.MaCSKCB).Where(p => p.KetQua == DungChung.Bien._ketQuaDT[4]).Count(),
                                 TS_1tuoi = q5.Where(p => p.MaCSKCB == kq.Key.MaCSKCB).Where(p => p.Tuoi1).Where(p => p.KetQua == DungChung.Bien._ketQuaDT[4]).Count(),
                                 Nu_1tuoi = q5.Where(p => p.MaCSKCB == kq.Key.MaCSKCB).Where(p => p.GTinh == 0).Where(p => p.Tuoi1).Where(p => p.KetQua == DungChung.Bien._ketQuaDT[4]).Count(),
                                 TS_5tuoi = q5.Where(p => p.MaCSKCB == kq.Key.MaCSKCB).Where(p => p.Tuoi < 5).Where(p => p.KetQua == DungChung.Bien._ketQuaDT[4]).Count(),
                                 Nu_5tuoi = q5.Where(p => p.MaCSKCB == kq.Key.MaCSKCB).Where(p => p.GTinh == 0).Where(p => p.Tuoi < 5).Where(p => p.KetQua == DungChung.Bien._ketQuaDT[4]).Count(),
                                 Xetnghiem = kq.Where(p => p.TenRG.Contains("XN")).Sum(p => p.SoLuot),
                                 Sieuam = kq.Where(p => p.TenRG == "Siêu âm").Sum(p => p.SoLuot),
                                 Xquang = kq.Where(p => p.TenRG == "X-Quang").Sum(p => p.SoLuot),
                                 ChupCT = kq.Where(p => p.TenRG == "Chụp CT").Sum(p => p.SoLuot),
                             }).OrderBy(p => p.Tuyen).ThenBy(p => p.Cosoyte).ToList();
                    BaoCao.rep_HDKBduphong_24009 rep = new BaoCao.rep_HDKBduphong_24009();
                    if (txtNgayThang.Text.Trim() != "")
                        rep.lab_tungaydenngay.Text = txtNgayThang.Text;
                    else
                        rep.lab_tungaydenngay.Text = "Từ ngày " + lupNgaytu.DateTime.ToShortDateString() + " đến ngày " + lupngayden.DateTime.ToShortDateString();
                    frmIn frm = new frmIn();
                    rep.DataSource = q;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }
        /// <summary>
        /// Trả về giá trị True nếu bệnh nhân dưới 1 tuổi, trả về false nếu bệnh nhân lớn hơn hoặc bằng 1 tuổi
        /// </summary>
        /// <param name="_ngaynhap"></param>
        /// <param name="_ngayS"></param>
        /// <param name="_thangS"></param>
        /// <param name="_namS"></param>
        /// <param name="_tuoi"></param>
        /// <returns></returns>
        public static bool CheckTuoi1(DateTime? _ngaynhap, string _ngayS, string _thangS, string _namS, int? _tuoi)
        {
            if (_tuoi == 0)
                return true;
            else if (_tuoi == 1)
            {
                if (_ngaynhap == null)
                    return false;
                else if (_ngayS == null)
                    return false;
                else if (_thangS == null)
                    return false;
                else if (_namS == null)
                    return false;
                else if (_ngayS.Trim() == "" || _ngayS == "00")
                    return false;
                else if (_thangS.Trim() == "" || _thangS == "00")
                    return false;
                else if (_namS.Length != 4)
                    return false;
                else
                {
                    int thang = Convert.ToInt16(_thangS);
                    if (_ngaynhap.Value.Year - Convert.ToInt32(_namS) <= 0)
                        return true;
                    else if (_ngaynhap.Value.Year - Convert.ToInt32(_namS) > 1)
                        return false;
                    else
                    {
                        if (_ngaynhap.Value.Month > thang)
                            return false;
                        else if (_ngaynhap.Value.Month < thang)
                            return true;
                        else if (_ngaynhap.Value.Day >= Convert.ToInt16(_ngayS))
                            return false;
                        else return true;
                    }
                }
            }
            else
                return false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_HDKCB_24009_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = data.DTBNs.ToList();
            cklDTBN.DataSource = q;
            for (int i = 0; i < cklDTBN.ItemCount; i++)
            {
                cklDTBN.SetItemChecked(i, true);
            }
        }
    }
}