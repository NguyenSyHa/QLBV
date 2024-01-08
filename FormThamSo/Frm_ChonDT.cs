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
    public partial class Frm_ChonDT : Form
    {
        int mabn = 0;
        int id = 0;
        int idd = 0;
        int makp = 0;
        string kieuin = "";
        private static string _maCQCQ = "";
        public Frm_ChonDT(int _mabn, int _iddt, string ten, int _makp)
        {
            InitializeComponent();
            mabn = _mabn;
            id = _iddt;
            kieuin = ten;
            makp = _makp;
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_In_Click(object sender, EventArgs e)
        {

            if(rdb_DTThuong.Checked)
            {
                int rs;
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                if (mabn > 0)
                {
                    if (kieuin == "In đơn")
                    {
                        string tuoi = DungChung.Ham.TuoitheoThang(_data, mabn, "72-00");
                        if (tuoi.Length > 3)
                        {
                            var ktratt = _data.TTboXungs.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                            if (ktratt != null)
                            {
                                if (string.IsNullOrEmpty(ktratt.NThan) || !ktratt.NThan.Contains(";") || ktratt.NThan.Split(';').Length < 2)
                                {
                                    MessageBox.Show("với bệnh nhân dưới 72 tháng tuổi cần có thông tin người thân");
                                    frm_TTNgThanTreEm frm = new frm_TTNgThanTreEm(mabn);
                                    frm.ShowDialog();
                                    DungChung.Ham.InDon(id, mabn, makp, idd);
                                }
                                else
                                    DungChung.Ham.InDon(id, mabn, makp, idd);
                            }
                        }
                        else
                        {
                            DungChung.Ham.InDon(id, mabn, makp, idd);
                        }
                    }
                    else
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_PhieuTHCD rep = new BaoCao.Rep_PhieuTHCD();

                        rep.Chuandoan.Value = DungChung.Ham.getMaICDarr(_data, mabn, DungChung.Bien.GetICD, 0)[1];
                        var bn2 = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == mabn)
                                   select new { bn.TenBNhan, bn.MaBNhan, bn.DChi, bn.GTinh, bn.CapCuu, bn.Tuoi, bn.SThe }).ToList();
                        if (bn2.Count > 0)
                        {
                            rep.Diachi.Value = bn2.First().DChi;
                            rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(_data, mabn, DungChung.Bien.formatAge);
                            rep.TenBN.Value = bn2.First().TenBNhan;
                            int gioiTinh = int.Parse(bn2.First().GTinh.ToString());
                            if (gioiTinh == 1)
                            {
                                rep.Nu.Value = "/";
                                rep.Nam.Value = "";
                            }
                            else
                            {
                                rep.Nu.Value = "";
                                rep.Nam.Value = "/";
                            }
                            if (bn2.First().SThe.Length == 15)
                            {
                                rep.Sthe1.Value = bn2.First().SThe.Substring(0, 3);
                                rep.Sthe2.Value = bn2.First().SThe.Substring(3, 2);
                                rep.Sthe3.Value = bn2.First().SThe.Substring(5, 2);
                                rep.Sthe4.Value = bn2.First().SThe.Substring(7, 3);
                                rep.Sthe5.Value = bn2.First().SThe.Substring(10, 5);
                            }
                        }
                        string xn = "12321", cdha = "12321";
                        var XQ = (
                              from canls in _data.DThuocs.Where(p => p.MaBNhan == mabn).Where(p => p.PLDV == 2)
                              join kp in _data.KPhongs on canls.MaKP equals kp.MaKP
                              join cb in _data.CanBoes on canls.MaCB equals cb.MaCB
                              join chidinh in _data.DThuoccts on canls.IDDon equals chidinh.IDDon
                              join dichvu in _data.DichVus on chidinh.MaDV equals dichvu.MaDV
                              join tn in _data.TieuNhomDVs on dichvu.IdTieuNhom equals tn.IdTieuNhom
                              join Nhom in _data.NhomDVs on tn.IDNhom equals Nhom.IDNhom
                              where (Nhom.TenNhomCT.Contains("Thủ thuật, phẫu thuật") || Nhom.TenNhomCT.Contains("DVKT thanh toán theo tỷ lệ") || Nhom.TenNhomCT.Contains(cdha)
                              || Nhom.TenNhomCT.Contains(xn))
                              group new { canls, cb, kp, dichvu, tn, chidinh } by new { NgayThang = canls.NgayKe, cb.TenCB, kp.TenKP, TenDV = dichvu.TenDV, tn.TenTN } into kq
                              select new { kq.Key.NgayThang, kq.Key.TenCB, kq.Key.TenKP, kq.Key.TenDV, kq.Key.TenTN, SoLuong = kq.Sum(p => p.chidinh.SoLuong) }
                              ).ToList();
                        if (XQ.Count > 0)
                        {
                            DateTime _dt = System.DateTime.Now;
                            if (XQ.First().NgayThang != null)
                                _dt = XQ.First().NgayThang.Value;
                            rep.NgayGio.Value = DungChung.Ham.NgaySangChu(_dt, 2);
                            rep.BSCD.Value = XQ.First().TenCB;
                            rep.TenKP.Value = XQ.First().TenKP;
                            rep.DataSource = XQ;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Không có chỉ định nào!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không có BN để in đơn");
                }
            }  
            else if(rdb_DTT04.Checked)
            {
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                if (mabn > 0)
                {
                    DungChung.Ham.InDon_TT04(id, mabn, makp, idd);
                }
                else
                {
                    MessageBox.Show("Không có BN để in đơn");
                }
            }
            else if (rdb_DTN04.Checked)
            {
                string _kieuDon = "Thuốc gây nghiện";
                InDonGN_HTT_TT04(id, _kieuDon);
            }
            else if (rdb_DTTT04.Checked)
            {
                string _kieuDon = "Thuốc hướng thần";
                InDonGN_HTT_TT04(id, _kieuDon);
            }
        }
        public class InDonClass
        {
            public string TenDV { get; set; }
            public string HamLuong { get; set; }
            public int MaDV { get; set; }
            public int IDDonct { get; set; }
            public string DonVi { get; set; }
            public double SoLuong { get; set; }
            public string HuongDan { get; set; }
            public string TenHC { get; set; }
            public string TenDVMain { get; set; }
            public string MaTam { set; get; }
        }
        private static void InDonGN_HTT_TT04(int _idDon, string _tentn)
        {
            QLBV_Database.QLBVEntities DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            frmIn frm = new frmIn();
            var ktkd = (from dt in DaTaContext.DThuocs.Where(p => p.IDDon == _idDon)
                        join cb in DaTaContext.CanBoes on dt.MaCB equals cb.MaCB
                        join kp in DaTaContext.KPhongs on dt.MaKP equals kp.MaKP
                        select new { dt.MaDTQG, dt.GhiChu, dt.IDDon, dt.KieuDon, dt.LoaiDuoc, dt.MaBNhan, dt.NgayKe, dt.PLDV, cb.TenCB, cb.CapBac, kp.TenKP, cb.SoDT }).ToList();

            if (ktkd.Count > 0)// kiểm tra có đơn thuốc hay chưa
            {
                //if (DungChung.Bien.MaBV == "08602") // Na hang ( tùng y/c ngày 05/05/16)- thông tư 05
                //{
                #region in mẫu mới
                int _int_maBN = ktkd.First().MaBNhan ?? 0;
                var ttd = (from bn in DaTaContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                           join kb in DaTaContext.BNKBs on bn.MaBNhan equals kb.MaBNhan
                           join ttbs in DaTaContext.TTboXungs on bn.MaBNhan equals ttbs.MaBNhan
                           select new { bn.GTinh, bn.TenBNhan, bn.NamSinh, bn.MaBNhan, bn.NgaySinh, bn.ThangSinh, bn.NNhap, kb.MaICD, kb.ChanDoan, kb.BenhKhac, kb.IDKB, bn.SThe, bn.DChi, kb.GhiChu, kb.NguoiNhanThuoc, kb.SoCMNDNguoiNhanThuoc, ttbs.SoKSinh, ttbs.NThan, ttbs.DThoaiNT, ttbs.CanNang_ChieuCao, ttbs.DThoai }).OrderByDescending(p => p.IDKB).ToList();

                var qd1 = (from dt in DaTaContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.IDDon == _idDon).Where(p => p.PLDV == 1)
                           join dtct in DaTaContext.DThuoccts on dt.IDDon equals dtct.IDDon
                           join dv in DaTaContext.DichVus on dtct.MaDV equals dv.MaDV
                           join tn in DaTaContext.TieuNhomDVs.Where(p => DungChung.Bien.MaBV == "12122" ? p.IDNhom == 4 : true) on dv.IdTieuNhom equals tn.IdTieuNhom
                           //where tn.TenRG != "Thuốc gây nghiện" && tn.TenRG != "Thuốc hướng tâm thần"
                           select new { dt.MaDTQG, MaTam = dv.MaTam, TenTNRG = tn.TenRG, dv.TenRG, TenDV = _maCQCQ == "00000" ? dv.TenHC + " " + dv.HamLuong : ((DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") ? ((dv.TenDV ?? "") + " (" + (dv.TenHC ?? "") + ") " + (dv.HamLuong ?? "")) : ((dv.TenHC.Contains(",") || dv.TenHC.Contains("+") || dv.TenHC.Contains(";")) ? dv.TenDV : (dv.TenDV + " (" + dv.TenHC + ") " + dv.HamLuong))), dv.TenHC, dv.HamLuong, dv.MaDV, dv.DonVi, dtct.SoLuong, dtct.IDDonct, dtct.GhiChu, HuongDan = "", DuongD = dtct.DuongD ?? "", SoLan = dtct.SoLan ?? "", MoiLan = dtct.MoiLan ?? "", Luong = dtct.Luong ?? "", DviUong = dtct.DviUong ?? "", LoiDan = dt.GhiChu, TenDVMain = dv.TenDV }).OrderBy(p => p.IDDonct).ToList();
                //var donThuoc = new DonThuoc();
                // var cDonThuoc = donThuoc.Clone();

                if (_tentn == "Thuốc gây nghiện")
                {
                    Phieu.repDonThuoc_TT04_N repd = new Phieu.repDonThuoc_TT04_N();
                    repd.MaDonThuoc.Value = ktkd.First().MaDTQG;
                    if (ttd.Count > 0)
                    {
                        string _ghichu = ttd.First().GhiChu ?? "";
                        string[] ar = _ghichu.Split(';');
                        if (ar.Length > 2)
                            repd.paraDotDieuTri.Value = ar[2];
                        if (DungChung.Bien.MaBV == "24272")
                        {
                            repd._TenBNhan.Value = ttd.First().TenBNhan.ToUpper();
                        }
                        else
                        {
                            repd._TenBNhan.Value = ttd.First().TenBNhan;
                        }
                        repd._MaBNhan.Value = "Mã BN: " + ttd.First().MaBNhan;
                        string tuoi = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(DaTaContext, _int_maBN) : DungChung.Ham.TuoitheoThang(DaTaContext, _int_maBN, DungChung.Bien.formatAge);
                        repd.NSinh.Value = ttd.First().NgaySinh + "/" + ttd.First().ThangSinh + "/" + ttd.First().NamSinh;
                        // KT
                        switch (ttd.First().GTinh)
                        {
                            case 1:
                                repd.Nam.Value = "x";
                                break;

                            case 0:
                                repd.Nu.Value = "x";
                                break;
                        }
                        if (ttd.First().CanNang_ChieuCao != null && ttd.First().CanNang_ChieuCao.Contains(";"))
                        {
                            repd.CanNang.Value = ttd.First().CanNang_ChieuCao.Split(';')[0];
                        }
                        else
                        {
                            repd.CanNang.Value = ttd.First().CanNang_ChieuCao;
                        }
                        if (!string.IsNullOrEmpty(ttd.First().NThan) && ttd.First().NThan.Contains(";"))
                        {
                            string[] arrnt = ttd.First().NThan.Split(';');
                            repd.HoTenNguoiThan.Value = arrnt[0];
                        }
                        else
                        {
                            repd.HoTenNguoiThan.Value = ttd.First().NThan;
                        }
                        repd.ICD.Value = DungChung.Ham.getMaICDarr(DaTaContext, _int_maBN, DungChung.Bien.GetICD, 0)[0];
                        repd.SoTheBH.Value = ttd.First().SThe;
                        repd.ChanDoan.Value = DungChung.Ham.getMaICDarr(DaTaContext, _int_maBN, DungChung.Bien.GetICD, 0)[1];
                        repd.TenCB.Value = ktkd.First().CapBac + ": " + ktkd.First().TenCB;
                        repd.SdtBS.Value = "SĐT: " + ktkd.First().SoDT;
                        repd.TenKP.Value = ktkd.First().TenKP;
                        repd.DiaChi.Value = ttd.First().DChi;
                        repd.CMT.Value = ttd.First().SoKSinh;
                        repd._MaBNhan.Value = "Mã BN: " + _int_maBN.ToString();
                        if (ktkd.First().NgayKe.Value.Day > 0)
                            repd.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
                        if (int.Parse(tuoi) < 15)
                        {
                            //repd.SDTLienHeBNhan.Value = Bien.MaBV == "01830" ? ((ktkd.First().SoDT != null && ktkd.First().SoDT != "") ? (ktkd.First().SoDT + ";" + ttd.First().DThoai) : "") : "";
                            repd.SDTLienHeBNhan.Value = ttd.First().DThoaiNT != null ? ";" + ttd.First().DThoaiNT : "";
                            repd.SDTLienHeBNhan.Value += ";" + ttd.First().DThoai;
                        }
                        else
                        {
                            repd.SDTLienHeBNhan.Value = ttd.First().DThoai + ";" + ttd.First().DThoaiNT;
                        }
                    }

                    string ngay = ktkd.FirstOrDefault().NgayKe.Value.Day > 9 ? ktkd.FirstOrDefault().NgayKe.Value.Day.ToString() : "0" + ktkd.FirstOrDefault().NgayKe.Value.Day.ToString();
                    string thang = ktkd.First().NgayKe.Value.Month > 9 ? ktkd.FirstOrDefault().NgayKe.Value.Month.ToString() : "0" + ktkd.FirstOrDefault().NgayKe.Value.Month.ToString();
                    string nam = ktkd.FirstOrDefault().NgayKe.Value.Year.ToString();

                    repd.Today.Value = "Ngày " + ngay + " tháng " + thang + " năm " + nam;

                    var qd2 = (from dv in qd1
                               group dv by new { dv.TenDV, dv.MaDV, dv.DonVi, dv.HamLuong, dv.TenRG, dv.MaTam, dv.IDDonct } into kq
                               select new InDonClass { TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV, HamLuong = kq.Key.HamLuong, MaDV = kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.SoLuong), HuongDan = kq.First().DuongD + " " + kq.First().SoLan + " " + kq.First().MoiLan + " " + kq.First().Luong + " " + kq.First().DviUong + " " + "; " + kq.First().GhiChu, TenHC = kq.First().TenHC, TenDVMain = kq.First().TenDVMain, IDDonct = kq.Key.IDDonct, MaTam = kq.Key.MaTam }).OrderBy(p => p.IDDonct).ToList();
                    repd.DataSource = qd2.ToList();
                    repd.ThuKho.Value = DungChung.Bien.ThuKho;
                    repd.SDTLienHeBNhan.Value = ttd.First().DThoaiNT;
                    
                    repd.SDT_CB.Value = "Điện thoại: " + DungChung.Ham.GetSDTBV();
                    repd.LoiDanBS.Value = ktkd.First().GhiChu;
                    repd.LoiDanBS.Value += DungChung.Bien.MaBV == "24012" ? "\n\n\n" : "";
                    repd.BindData();
                    repd.CreateDocument();
                    frm.prcIN.PrintingSystem = repd.PrintingSystem;
                    frm.ShowDialog();
                }
                else if(_tentn == "Thuốc hướng thần")
                {
                    Phieu.repDonThuoc_TT04_H repd = new Phieu.repDonThuoc_TT04_H();
                    repd.MaDonThuoc.Value = ktkd.First().MaDTQG;
                    if (ttd.Count > 0)
                    {
                        string _ghichu = ttd.First().GhiChu ?? "";
                        string[] ar = _ghichu.Split(';');
                        if (ar.Length > 2)
                            repd.paraDotDieuTri.Value = ar[2];
                        repd._TenBNhan.Value = ttd.First().TenBNhan;
                        repd._MaBNhan.Value = "Mã BN: " + ttd.First().MaBNhan;
                        //repd.Tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(DaTaContext, _int_maBN) : DungChung.Ham.TuoitheoThang(DaTaContext, _int_maBN, DungChung.Bien.formatAge);
                        repd.NSinh.Value = ttd.First().NgaySinh + "/" + ttd.First().ThangSinh + "/" + ttd.First().NamSinh;
                        string tuoi = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(DaTaContext, _int_maBN) : DungChung.Ham.TuoitheoThang(DaTaContext, _int_maBN, DungChung.Bien.formatAge);
                        // KT
                        switch (ttd.First().GTinh)
                        {
                            case 1:
                                repd.Nam.Value = "x";
                                break;

                            case 0:
                                repd.Nu.Value = "x";
                                break;
                        }
                        if (ttd.First().CanNang_ChieuCao != null && ttd.First().CanNang_ChieuCao.Contains(";"))
                        {
                            repd.CanNang.Value = ttd.First().CanNang_ChieuCao.Split(';')[0];
                        }
                        else
                        {
                            repd.CanNang.Value = ttd.First().CanNang_ChieuCao;
                        }
                        if (!string.IsNullOrEmpty(ttd.First().NThan) && ttd.First().NThan.Contains(";"))
                        {
                            string[] arrnt = ttd.First().NThan.Split(';');
                            repd.HoTenNguoiThan.Value = arrnt[0];
                        }
                        else
                        {
                            repd.HoTenNguoiThan.Value = ttd.First().NThan;
                        }
                        repd.ICD.Value = DungChung.Ham.getMaICDarr(DaTaContext, _int_maBN, DungChung.Bien.GetICD, 0)[0];
                        repd.SoTheBH.Value = ttd.First().SThe;
                        repd.ChanDoan.Value = DungChung.Ham.getMaICDarr(DaTaContext, _int_maBN, DungChung.Bien.GetICD, 0)[1];
                        repd.TenCB.Value = ktkd.First().CapBac + ": " + ktkd.First().TenCB;
                        repd.CMT.Value = ttd.First().SoKSinh;
                        repd.SdtBS.Value = "SĐT: " + ktkd.First().SoDT;
                        repd.TenKP.Value = ktkd.First().TenKP;
                        repd.DiaChi.Value = ttd.First().DChi;
                        repd._MaBNhan.Value = "Mã BN: " + _int_maBN.ToString();
                        if (ktkd.First().NgayKe.Value.Day > 0)
                            repd.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
                        if (int.Parse(tuoi) < 15)
                        {
                            //repd.SDTLienHeBNhan.Value = Bien.MaBV == "01830" ? ((ktkd.First().SoDT != null && ktkd.First().SoDT != "") ? (ktkd.First().SoDT + ";" + ttd.First().DThoai) : "") : "";
                            repd.SDTLienHeBNhan.Value = ttd.First().DThoaiNT != null ? ";" + ttd.First().DThoaiNT : "";
                            repd.SDTLienHeBNhan.Value += ";" + ttd.First().DThoai;
                        }
                        else
                        {
                            repd.SDTLienHeBNhan.Value = ttd.First().DThoai + ";" + ttd.First().DThoaiNT;
                        }
                    }

                    string ngay = ktkd.FirstOrDefault().NgayKe.Value.Day > 9 ? ktkd.FirstOrDefault().NgayKe.Value.Day.ToString() : "0" + ktkd.FirstOrDefault().NgayKe.Value.Day.ToString();
                    string thang = ktkd.First().NgayKe.Value.Month > 9 ? ktkd.FirstOrDefault().NgayKe.Value.Month.ToString() : "0" + ktkd.FirstOrDefault().NgayKe.Value.Month.ToString();
                    string nam = ktkd.FirstOrDefault().NgayKe.Value.Year.ToString();

                    repd.Today.Value = "Ngày " + ngay + " tháng " + thang + " năm " + nam;

                    var qd2 = (from dv in qd1
                               group dv by new { dv.TenDV, dv.MaDV, dv.DonVi, dv.HamLuong, dv.TenRG, dv.MaTam, dv.IDDonct, dv.HuongDan } into kq
                               select new InDonClass { TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV, HamLuong = kq.Key.HamLuong, MaDV = kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.SoLuong), HuongDan = kq.Key.HuongDan + " " + kq.First().SoLan + " " + kq.First().MoiLan + " " + kq.First().Luong + " " + kq.First().DviUong + " " + "; " + kq.First().GhiChu, TenHC = kq.First().TenHC, TenDVMain = kq.First().TenDVMain, IDDonct = kq.Key.IDDonct, MaTam = kq.Key.MaTam }).OrderBy(p => p.IDDonct).ToList();

                    repd.DataSource = qd2.ToList();
                    repd.ThuKho.Value = DungChung.Bien.ThuKho;


                    repd.SDT_CB.Value = "Điện thoại: " + DungChung.Ham.GetSDTBV();
                    repd.LoiDanBS.Value = ktkd.First().GhiChu;
                    repd.LoiDanBS.Value += DungChung.Bien.MaBV == "24012" ? "\n\n\n" : "";
                    repd.BindData();
                    repd.CreateDocument();
                    frm.prcIN.PrintingSystem = repd.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
            }
        }

        private void Frm_ChonDT_Load(object sender, EventArgs e)
        {
            rdb_DTThuong.Checked = true;
        }
    }
}
