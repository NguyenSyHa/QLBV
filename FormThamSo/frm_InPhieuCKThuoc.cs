using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_InPhieuCKThuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_InPhieuCKThuoc()
        {
            InitializeComponent();
        }
        int _MaBN = 0;
        public frm_InPhieuCKThuoc(int _mabn)
        {
            InitializeComponent();
            _MaBN = _mabn;
        }
        #region In phiếu công khai
        private void _InPhieuCKhai(int _int_maBN)
        {


        }
        #endregion
        private void frm_InPhieuCKThuoc_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();

        }
        static string _maCQCQ = "";
        private void btnInbc_Click(object sender, EventArgs e)
        {
            int _makp = 0;
            timer1.Stop();
            List<FormNhap.usDieuTri.l_CTThuoc> _ldthuocct = new List<FormNhap.usDieuTri.l_CTThuoc>();
            DateTime ngayvao = System.DateTime.Now.Date;
            DateTime ngayra = System.DateTime.Now.Date;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            int _idnhomthuoc = 4, _idnhomVT = 10, _idnhomVTTyle = 11;
            int _idnhomthuocTyle = 4, _idnhom5 = 4;
            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" || DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24303" || DungChung.Bien.MaBV == "24388" || DungChung.Bien.MaBV == "24272")
            {
                _idnhomthuocTyle = 6;
                _idnhom5 = 5;
            }

            if (!ckcVTYT.Checked)
            {
                _idnhomVT = _idnhomthuoc;
                _idnhomVTTyle = _idnhomthuoc;
            }
            if (DungChung.Bien.MaBV != "27022")
                _idnhomVTTyle = _idnhomVT;
            var qtong = (from dthc in data.DThuocs.Where(p => p.MaBNhan == _MaBN)
                         join dtct in data.DThuoccts on dthc.IDDon equals dtct.IDDon
                         join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                         join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                         join nhomdv in data.NhomDVs on tn.IDNhom equals nhomdv.IDNhom
                         where (tn.IDNhom == _idnhomthuoc || tn.IDNhom == _idnhomVT || tn.IDNhom == _idnhomVTTyle || tn.IDNhom == _idnhomthuocTyle|| tn.IDNhom == _idnhom5)
                         select new { nhomdv.IDNhom, dthc, dtct, dv.TenDV, dv.DonVi, nhomdv.TenNhom, dv.TenRG, dv.DongY }).ToList();

            List<thuoc> thuocs1 = new List<thuoc>(); // ds thuốc theo thang
            foreach (var item in qtong.Where(p => p.DongY == 1))
            {
                for (int i = 0; i < item.dtct.Loai; i++)
                {
                    thuoc t = new thuoc()
                    {
                        MaDV = (int)item.dtct.MaDV,
                        NgayKe = item.dtct.NgayNhap.Value.AddDays(i),
                        SoLuong = item.dtct.SoLuongct
                    };
                    thuocs1.Add(t);
                }
            }
            thuocs1 = (from a in thuocs1
                       group a by new { a.MaDV, a.NgayKe } into kq
                       select new thuoc {
                           MaDV = kq.Key.MaDV,
                           NgayKe = kq.Key.NgayKe,
                           SoLuong = kq.Sum(p => p.SoLuong)
                       }).ToList();
            if (rgChonMau.SelectedIndex == 1)
            {
                #region thuốc và vtyt khác mẫu
                if (DungChung.Bien.MaBV == "24272")
                {
                    for (int f = 0; f < 1; f++)
                    {
                        if (_MaBN > 0)
                        {
                            var dt_rep = (from dt in qtong.Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : (p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle || p.IDNhom == _idnhom5))
                                          group dt by new { dt.dtct.DonGia, dt.dthc.NgayKe, dt.dtct.DonVi, dt.dtct.MaDV } into kq
                                          select new { kq.Key.DonGia, kq.Key.DonVi, kq.Key.NgayKe, kq.Key.MaDV, SoLuong = kq.Sum(p => p.dtct.SoLuong) }).Where(p => p.SoLuong != 0).ToList();
                            if (DungChung.Bien.MaBV == "24272")
                            {
                                dt_rep = (from dt in qtong.Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : (p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle|| p.IDNhom == _idnhom5))
                                          from t in thuocs1
                                          where dt.dtct.MaDV == t.MaDV
                                          group dt by new { dt.dtct.DonGia, dt.dtct.DonVi, dt.dtct.MaDV, t.NgayKe, t.SoLuong } into kq
                                          select new { kq.Key.DonGia, kq.Key.DonVi, kq.Key.NgayKe, kq.Key.MaDV, SoLuong = kq.Key.SoLuong }).Where(p => p.SoLuong != 0).ToList();

                            }
                        
                            foreach (var a in dt_rep)
                            {
                                FormNhap.usDieuTri.l_CTThuoc moi = new FormNhap.usDieuTri.l_CTThuoc();
                                moi.MaDV = a.MaDV == null ? 0 : a.MaDV.Value;
                                moi.NgayKe = a.NgayKe.Value;
                                moi.SoLuong = a.SoLuong;
                                moi.DonGia = a.DonGia;
                                moi.DonVi = a.DonVi;
                                _ldthuocct.Add(moi);
                            }
                            string[] _songay;


                            var ngaykd = (from nk in qtong.Where(p => p.dthc.PLDV == 1).Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : (p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle|| p.IDNhom == _idnhom5))

                                          where nk.dthc.NgayKe != null
                                          select new { nk.dthc.NgayKe }).ToList().Select(x => new { x.NgayKe.Value.Date }).Distinct().OrderBy(p => p.Date).ToList();
                        
                            var ngaykd_24272 = (from nk in qtong.Where(p => p.dthc.PLDV == 1).Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : (p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle || p.IDNhom == _idnhom5))

                                        where nk.dthc.NgayKe != null
                                        select new { nk.dthc.NgayKe, nk.dtct.Loai }).ToList().Select(x => new { x.NgayKe.Value.Date, Loai = 18 }).Distinct().OrderBy(p => p.Date).ToList();
                        
                            List<DateTime> ngay = new List<DateTime>();
                            foreach (var item in ngaykd_24272)
                            {
                                for (int index = 0; index < item.Loai; index++)
                                {
                                    ngay.Add(item.Date.AddDays(index));
                                }
                            }
                            int i = 0;
                            ngay = ngay.Distinct().ToList();
                            if (DungChung.Bien.MaBV == "24272")
                            {
                                _songay = new string[ngay.Count];
                                foreach (var a in ngay)
                                {
                                    _songay[i] = a.Date.ToShortDateString();
                                    if (_songay[i] == null)
                                    {
                                        _songay[i] = a.Date.AddDays(i).ToShortDateString();
                                    }
                                    i++;
                                }
                            }
                            else
                            {
                                _songay = new string[ngaykd.Count];
                                foreach (var a in ngaykd)
                                {
                                    _songay[i] = a.Date.ToShortDateString();
                                    i++;
                                }
                            }
                            var par = (from bn in data.BenhNhans.Where(p => p.MaBNhan == _MaBN)
                                       join kb in data.BNKBs.OrderByDescending(p => p.IDKB) on bn.MaBNhan equals kb.MaBNhan
                                       join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                       //join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                       select new
                                       {
                                           GTinh = bn.GTinh == 0 ? "Nữ" : "Nam",
                                           kb.Buong,
                                           kb.Giuong,
                                           bn.DTuong,
                                           bn.MaBNhan,
                                           bn.DChi,
                                           bn.Tuoi,
                                           bn.SThe,
                                           bn.MaCS,
                                           bn.NoiTru,
                                           bn.HanBHTu,
                                           bn.HanBHDen,
                                           kb.MaICD,
                                           kb.MaICD2,
                                           kb.ChanDoan,
                                           vv.NgayVao,
                                           kb.IDKB,
                                           kb.BenhKhac,
                                           bn.NgaySinh,
                                           bn.ThangSinh,
                                           bn.NamSinh,
                                           bn.NNhap,
                                           kb.BenhKhacYHCT,
                                           kb.ChanDoanYHCT,
                                           kb.MaYHCT
                                       }).OrderByDescending(p => p.IDKB).ToList();


                            if (DungChung.Bien.MaBV == "24272")
                            {
                                for (int j = 0; j < _songay.Length; j += 18)
                                {
                                    bool _InCongKhoan = false;
                                    if (j > _songay.Length - 2)
                                        _InCongKhoan = true;
                                    int m = 0;
                                    string[] _songayT1 = new string[18];
                                    for (int l = 0; l < 18; l++)
                                    {
                                        _songayT1[l] = "";
                                    }
                                    for (int k = j; k < _songay.Length; k++)
                                    {
                                        if (m < 18)
                                        {
                                            _songayT1[m] = _songay[k];
                                            m++;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    #region Mẫu cho 20001: bỏ mã ICD, thanh tiền, đơn giá, ghi chú
                                    //if (DungChung.Bien.MaBV == "20001")
                                    //{
                                    //    BaoCao.rep_PhieuCongKhaiThuoc_TT23_20001 rep = new BaoCao.rep_PhieuCongKhaiThuoc_TT23_20001(_songayT1, _MaBN, _ldthuocct, _InCongKhoan);
                                    //    if (par.Count > 0)
                                    //    {
                                    //        ngayvao = DungChung.Ham.NgayTu(par.First().NgayVao.Value);
                                    //        if (f == 1)
                                    //            rep.colMauSo.Text = "Ms: 10/YHCT-2014";
                                    //        rep.So.Value = "Số: ...........";
                                    //        var khoa = (from kp in data.KPhongs join bnkb in data.BNKBs.Where(p => p.MaBNhan == _MaBN) on kp.MaKP equals bnkb.MaKP select new { kp.TenKP, bnkb.IDKB }).OrderByDescending(p => p.IDKB).ToList();
                                    //        if (khoa.Count > 0)
                                    //            rep.Khoa.Value = khoa.First().TenKP.ToUpper();
                                    //        var tenbn = data.BenhNhans.Where(p => p.MaBNhan == _MaBN).FirstOrDefault();
                                    //        if (tenbn != null)
                                    //            rep.TenBN.Value = tenbn.TenBNhan.ToString().ToUpper();
                                    //        rep.TuoiBN.Value = DungChung.Ham.TuoitheoThang(data, _MaBN, DungChung.Bien.formatAge);

                                    //        rep.TieuDe.Value = f == 0 ? "PHIẾU CÔNG KHAI THUỐC" : "PHIẾU CÔNG KHAI VẬT TƯ Y TẾ TIÊU HAO";
                                    //        rep.GioiTinh.Value = par.First().GTinh;
                                    //        rep.DiaChi.Value = par.First().DChi;
                                    //        rep.SoThe.Value = par.First().SThe + " - " + par.First().MaCS;
                                    //        if (par.First().DTuong == "BHYT")
                                    //        {
                                    //            rep.GTTu.Value = par.First().HanBHTu;
                                    //            rep.GTDen.Value = par.First().HanBHDen;
                                    //        }
                                    //        else
                                    //        {
                                    //            rep.GTTu.Value = null;
                                    //            rep.GTDen.Value = null;
                                    //        }
                                    //        rep.Buong.Value = par.First().Buong;
                                    //        rep.Giuong.Value = par.First().Giuong;
                                    //        rep.NgayVV.Value = par.First().NgayVao;
                                    //        rep.DiaChi.Value = DungChung.Ham.GetICDstr(par.First().ChanDoan + par.First().BenhKhac);
                                    //        //rep.MaChanDoan.Value = par.First().MaICD;
                                    //        var rvien = data.RaViens.Where(p => p.MaBNhan == _MaBN).ToList();
                                    //        if (rvien.Count > 0)
                                    //        {
                                    //            rep.NgayRV.Value = rvien.First().NgayRa;
                                    //        }
                                    //        double thuoc = 0;
                                    //        var q = (from dt in qtong.Where(p => p.dthc.PLDV == 1).Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : p.IDNhom == _idnhomVT)

                                    //                 //where (dt.MaKP== (DungChung.Bien.MaKP))
                                    //                 group dt by new { dt.TenNhom, dt.dtct.MaDV, dt.TenDV, dt.TenRG, dt.dtct.DonVi, dt.dtct.DonGia } into kq
                                    //                 select new
                                    //                 {
                                    //                     MaDV = kq.Key.MaDV,
                                    //                     kq.Key.DonGia,
                                    //                     TenNhomDV = kq.Key.TenNhom.ToUpper(),
                                    //                     TenDV = kq.Key.TenDV,
                                    //                     DonVi = kq.Key.DonVi,
                                    //                     SoLuongT = kq.Sum(p => p.dtct.SoLuong),
                                    //                     ThanhTienT = kq.Sum(p => p.dtct.ThanhTien)
                                    //                 }).Where(p => p.SoLuongT != 0).ToList();
                                    //        if (q.Count > 0)
                                    //            thuoc = q.Sum(p => p.ThanhTienT);
                                    //        rep.DataSource = q.ToList();
                                    //        rep.BindingData();
                                    //        rep.CreateDocument();
                                    //        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    //        frm.ShowDialog();

                                    //    }
                                    //    else
                                    //    {
                                    //        MessageBox.Show("Bệnh nhân chưa có khám bệnh vào viện");
                                    //    }

                                    //}
                                    //#endregion
                                    //#region BV khác
                                    //else
                                    //{
                                    #endregion // bo // bỏ theo y/c thụy 05-09 đức
                                    BaoCao.rep_PhieuCongKhaiThuoc_TT23 rep = new BaoCao.rep_PhieuCongKhaiThuoc_TT23(_songayT1, _MaBN, _ldthuocct, _InCongKhoan);

                                    if (par.Count > 0)
                                    {
                                        ngayvao = DungChung.Ham.NgayTu(par.First().NgayVao.Value);

                                        var khoa = (from kp in data.KPhongs join bnkb in data.BNKBs.Where(p => p.MaBNhan == _MaBN) on kp.MaKP equals bnkb.MaKP select new { bnkb.MaKP, kp.TenKP, bnkb.IDKB, kp.IsDongY }).OrderByDescending(p => p.IDKB).ToList();
                                        if (khoa.Count > 0)
                                        {
                                            rep.Khoa.Value = khoa.First().TenKP.ToUpper();
                                            _makp = khoa.First().MaKP.Value;
                                        }
                                        var tenbn = data.BenhNhans.Where(p => p.MaBNhan == _MaBN).FirstOrDefault();
                                        if (tenbn != null)
                                            rep.TenBN.Value = tenbn.TenBNhan.ToString().ToUpper();
                                        rep.TuoiBN.Value = DungChung.Ham.TuoitheoThang(data, _MaBN, DungChung.Bien.formatAge);

                                        rep.TieuDe.Value = f == 0 ? "PHIẾU CÔNG KHAI THUỐC" : "PHIẾU CÔNG KHAI VẬT TƯ Y TẾ TIÊU HAO";
                                        rep.GioiTinh.Value = par.First().GTinh;
                                        rep.DiaChi.Value = par.First().DChi;
                                        rep.SoThe.Value = par.First().SThe + " - " + par.First().MaCS;
                                        if (par.First().DTuong == "BHYT")
                                        {
                                            rep.GTTu.Value = par.First().HanBHTu;
                                            rep.GTDen.Value = par.First().HanBHDen;
                                        }
                                        else
                                        {
                                            rep.GTTu.Value = null;
                                            rep.GTDen.Value = null;
                                        }
                                        rep.Buong.Value = par.First().Buong;
                                        rep.Giuong.Value = par.First().Giuong;
                                        rep.NgayVV.Value = par.First().NgayVao;
                                        string[] arrThongTinBNKB = DungChung.Ham.laythongtinBNKB(data, _MaBN, _makp, true);
                                        if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24297")
                                        {
                                            var icd = data.ICD10.Where(o => true).ToList();
                                            rep.ChanDoan.Value = DungChung.Ham.GhepChuoiChanDoanYHCT(icd, par.First().MaICD, par.First().MaICD2);
                                        }
                                        else if (DungChung.Bien.MaBV == "24272")
                                        {
                                            rep.ChanDoan.Value = arrThongTinBNKB[1];
                                        }
                                        else
                                            rep.ChanDoan.Value = DungChung.Ham.GetICDstr(par.First().ChanDoan + par.First().BenhKhac);
                                        if (DungChung.Bien.MaBV == "24297")
                                        {
                                            if (khoa.First().IsDongY == true)
                                                rep.MaChanDoan.Value = par.First().MaYHCT;
                                            else
                                                rep.MaChanDoan.Value = par.First().MaICD;
                                        }

                                        else
                                            rep.MaChanDoan.Value = par.First().MaICD;
                                        var rvien = data.RaViens.Where(p => p.MaBNhan == _MaBN).ToList();
                                        if (rvien.Count > 0)
                                        {
                                            rep.NgayRV.Value = rvien.First().NgayRa;
                                        }
                                        double thuoc = 0;
                                        var q = (from dt in qtong.Where(p => p.dthc.PLDV == 1).Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : (p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle || p.IDNhom == _idnhom5))

                                                     //where (dt.MaKP== (DungChung.Bien.MaKP))
                                                 group dt by new { dt.TenNhom, dt.dtct.MaDV, dt.TenDV, dt.TenRG, dt.dtct.DonVi, dt.dtct.DonGia, dt.DongY } into kq
                                                 select new
                                                 {
                                                     MaDV = kq.Key.MaDV,
                                                     kq.Key.DonGia,
                                                     TenNhomDV = kq.Key.TenNhom.ToUpper(),
                                                     TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009" || DungChung.Bien.MaBV == "01830") ? (kq.Key.TenRG ?? "") : kq.Key.TenDV,
                                                     DonVi = kq.Key.DonVi,
                                                     SoLuongT = kq.Sum(p => p.dtct.SoLuong),
                                                     ThanhTienT = kq.Sum(p => p.dtct.ThanhTien),
                                                     DongY = kq.Key.DongY ?? 0
                                                 }).Where(p => p.SoLuongT != 0).ToList();
                                        if (q.Count > 0)
                                            thuoc = q.Sum(p => p.ThanhTienT);
                                        if (DungChung.Bien.MaBV == "24297")
                                            q = q.OrderBy(o => o.DongY).ToList();
                                        rep.DataSource = q.ToList();
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                    }
                                    else
                                    {
                                        MessageBox.Show("Bệnh nhân chưa có khám bệnh vào viện");
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < _songay.Length; j += 17)
                                {
                                    bool _InCongKhoan = false;
                                    if (j > _songay.Length - 2)
                                        _InCongKhoan = true;
                                    int m = 0;
                                    string[] _songayT1 = new string[18];
                                    for (int l = 0; l < 18; l++)
                                    {
                                        _songayT1[l] = "";
                                    }
                                    for (int k = j; k < _songay.Length; k++)
                                    {
                                        if (m < 17)
                                        {
                                            _songayT1[m] = _songay[k];
                                            m++;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    #region Mẫu cho 20001: bỏ mã ICD, thanh tiền, đơn giá, ghi chú
                                    //if (DungChung.Bien.MaBV == "20001")
                                    //{
                                    //    BaoCao.rep_PhieuCongKhaiThuoc_TT23_20001 rep = new BaoCao.rep_PhieuCongKhaiThuoc_TT23_20001(_songayT1, _MaBN, _ldthuocct, _InCongKhoan);
                                    //    if (par.Count > 0)
                                    //    {
                                    //        ngayvao = DungChung.Ham.NgayTu(par.First().NgayVao.Value);
                                    //        if (f == 1)
                                    //            rep.colMauSo.Text = "Ms: 10/YHCT-2014";
                                    //        rep.So.Value = "Số: ...........";
                                    //        var khoa = (from kp in data.KPhongs join bnkb in data.BNKBs.Where(p => p.MaBNhan == _MaBN) on kp.MaKP equals bnkb.MaKP select new { kp.TenKP, bnkb.IDKB }).OrderByDescending(p => p.IDKB).ToList();
                                    //        if (khoa.Count > 0)
                                    //            rep.Khoa.Value = khoa.First().TenKP.ToUpper();
                                    //        var tenbn = data.BenhNhans.Where(p => p.MaBNhan == _MaBN).FirstOrDefault();
                                    //        if (tenbn != null)
                                    //            rep.TenBN.Value = tenbn.TenBNhan.ToString().ToUpper();
                                    //        rep.TuoiBN.Value = DungChung.Ham.TuoitheoThang(data, _MaBN, DungChung.Bien.formatAge);

                                    //        rep.TieuDe.Value = f == 0 ? "PHIẾU CÔNG KHAI THUỐC" : "PHIẾU CÔNG KHAI VẬT TƯ Y TẾ TIÊU HAO";
                                    //        rep.GioiTinh.Value = par.First().GTinh;
                                    //        rep.DiaChi.Value = par.First().DChi;
                                    //        rep.SoThe.Value = par.First().SThe + " - " + par.First().MaCS;
                                    //        if (par.First().DTuong == "BHYT")
                                    //        {
                                    //            rep.GTTu.Value = par.First().HanBHTu;
                                    //            rep.GTDen.Value = par.First().HanBHDen;
                                    //        }
                                    //        else
                                    //        {
                                    //            rep.GTTu.Value = null;
                                    //            rep.GTDen.Value = null;
                                    //        }
                                    //        rep.Buong.Value = par.First().Buong;
                                    //        rep.Giuong.Value = par.First().Giuong;
                                    //        rep.NgayVV.Value = par.First().NgayVao;
                                    //        rep.DiaChi.Value = DungChung.Ham.GetICDstr(par.First().ChanDoan + par.First().BenhKhac);
                                    //        //rep.MaChanDoan.Value = par.First().MaICD;
                                    //        var rvien = data.RaViens.Where(p => p.MaBNhan == _MaBN).ToList();
                                    //        if (rvien.Count > 0)
                                    //        {
                                    //            rep.NgayRV.Value = rvien.First().NgayRa;
                                    //        }
                                    //        double thuoc = 0;
                                    //        var q = (from dt in qtong.Where(p => p.dthc.PLDV == 1).Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : p.IDNhom == _idnhomVT)

                                    //                 //where (dt.MaKP== (DungChung.Bien.MaKP))
                                    //                 group dt by new { dt.TenNhom, dt.dtct.MaDV, dt.TenDV, dt.TenRG, dt.dtct.DonVi, dt.dtct.DonGia } into kq
                                    //                 select new
                                    //                 {
                                    //                     MaDV = kq.Key.MaDV,
                                    //                     kq.Key.DonGia,
                                    //                     TenNhomDV = kq.Key.TenNhom.ToUpper(),
                                    //                     TenDV = kq.Key.TenDV,
                                    //                     DonVi = kq.Key.DonVi,
                                    //                     SoLuongT = kq.Sum(p => p.dtct.SoLuong),
                                    //                     ThanhTienT = kq.Sum(p => p.dtct.ThanhTien)
                                    //                 }).Where(p => p.SoLuongT != 0).ToList();
                                    //        if (q.Count > 0)
                                    //            thuoc = q.Sum(p => p.ThanhTienT);
                                    //        rep.DataSource = q.ToList();
                                    //        rep.BindingData();
                                    //        rep.CreateDocument();
                                    //        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    //        frm.ShowDialog();

                                    //    }
                                    //    else
                                    //    {
                                    //        MessageBox.Show("Bệnh nhân chưa có khám bệnh vào viện");
                                    //    }

                                    //}
                                    //#endregion
                                    //#region BV khác
                                    //else
                                    //{
                                    #endregion // bo // bỏ theo y/c thụy 05-09 đức
                                    BaoCao.rep_PhieuCongKhaiThuoc_TT23 rep = new BaoCao.rep_PhieuCongKhaiThuoc_TT23(_songayT1, _MaBN, _ldthuocct, _InCongKhoan);

                                    if (par.Count > 0)
                                    {
                                        ngayvao = DungChung.Ham.NgayTu(par.First().NgayVao.Value);

                                        var khoa = (from kp in data.KPhongs join bnkb in data.BNKBs.Where(p => p.MaBNhan == _MaBN) on kp.MaKP equals bnkb.MaKP select new { kp.TenKP, bnkb.IDKB, kp.IsDongY }).OrderByDescending(p => p.IDKB).ToList();
                                        if (khoa.Count > 0)
                                            rep.Khoa.Value = khoa.First().TenKP.ToUpper();
                                        var tenbn = data.BenhNhans.Where(p => p.MaBNhan == _MaBN).FirstOrDefault();
                                        if (tenbn != null)
                                            rep.TenBN.Value = tenbn.TenBNhan.ToString().ToUpper();
                                        rep.TuoiBN.Value = DungChung.Ham.TuoitheoThang(data, _MaBN, DungChung.Bien.formatAge);

                                        rep.TieuDe.Value = f == 0 ? "PHIẾU CÔNG KHAI THUỐC" : "PHIẾU CÔNG KHAI VẬT TƯ Y TẾ TIÊU HAO";
                                        rep.GioiTinh.Value = par.First().GTinh;
                                        rep.DiaChi.Value = par.First().DChi;
                                        rep.SoThe.Value = par.First().SThe + " - " + par.First().MaCS;
                                        if (par.First().DTuong == "BHYT")
                                        {
                                            rep.GTTu.Value = par.First().HanBHTu;
                                            rep.GTDen.Value = par.First().HanBHDen;
                                        }
                                        else
                                        {
                                            rep.GTTu.Value = null;
                                            rep.GTDen.Value = null;
                                        }
                                        rep.Buong.Value = par.First().Buong;
                                        rep.Giuong.Value = par.First().Giuong;
                                        rep.NgayVV.Value = par.First().NgayVao;
                                        string[] arrThongTinBNKB = DungChung.Ham.laythongtinBNKB(data, _MaBN, _makp, true);
                                        if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24297")
                                        {
                                            var icd = data.ICD10.Where(o => true).ToList();
                                            rep.ChanDoan.Value = DungChung.Ham.GhepChuoiChanDoanYHCT(icd, par.First().MaICD, par.First().MaICD2);
                                        }
                                        else if (DungChung.Bien.MaBV == "24272")
                                        {
                                            rep.ChanDoan.Value = arrThongTinBNKB[1];
                                        }
                                        else
                                            rep.ChanDoan.Value = DungChung.Ham.GetICDstr(par.First().ChanDoan + par.First().BenhKhac);
                                        if (DungChung.Bien.MaBV == "24297")
                                        {
                                            if (khoa.First().IsDongY == true)
                                                rep.MaChanDoan.Value = par.First().MaYHCT;
                                            else
                                                rep.MaChanDoan.Value = par.First().MaICD;
                                        }

                                        else
                                            rep.MaChanDoan.Value = par.First().MaICD;
                                        var rvien = data.RaViens.Where(p => p.MaBNhan == _MaBN).ToList();
                                        if (rvien.Count > 0)
                                        {
                                            rep.NgayRV.Value = rvien.First().NgayRa;
                                        }
                                        double thuoc = 0;
                                        var q = (from dt in qtong.Where(p => p.dthc.PLDV == 1).Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : (p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle || p.IDNhom == _idnhom5))

                                                     //where (dt.MaKP== (DungChung.Bien.MaKP))
                                                 group dt by new { dt.TenNhom, dt.dtct.MaDV, dt.TenDV, dt.TenRG, dt.dtct.DonVi, dt.dtct.DonGia, dt.DongY } into kq
                                                 select new
                                                 {
                                                     MaDV = kq.Key.MaDV,
                                                     kq.Key.DonGia,
                                                     TenNhomDV = kq.Key.TenNhom.ToUpper(),
                                                     TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009" || DungChung.Bien.MaBV == "01830") ? (kq.Key.TenRG ?? "") : kq.Key.TenDV,
                                                     DonVi = kq.Key.DonVi,
                                                     SoLuongT = kq.Sum(p => p.dtct.SoLuong),
                                                     ThanhTienT = kq.Sum(p => p.dtct.ThanhTien),
                                                     DongY = kq.Key.DongY ?? 0
                                                 }).Where(p => p.SoLuongT != 0).ToList();
                                        if (q.Count > 0)
                                            thuoc = q.Sum(p => p.ThanhTienT);
                                        if (DungChung.Bien.MaBV == "24297")
                                            q = q.OrderBy(o => o.DongY).ToList();
                                        rep.DataSource = q.ToList();
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                    }
                                    else
                                    {
                                        MessageBox.Show("Bệnh nhân chưa có khám bệnh vào viện");
                                    }
                                }
                            }
                        
                        }
                        else
                        {
                            MessageBox.Show("Bạn chưa chọn BN hoặc không có BN");
                        }
                    }
                }
                else
                {
                    for (int f = 0; f < 2; f++)
                    {
                        if (_MaBN > 0)
                        {
                            var dt_rep = (from dt in qtong.Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : (p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle || p.IDNhom == _idnhom5))
                                          group dt by new { dt.dtct.DonGia, dt.dthc.NgayKe, dt.dtct.DonVi, dt.dtct.MaDV } into kq
                                          select new { kq.Key.DonGia, kq.Key.DonVi, kq.Key.NgayKe, kq.Key.MaDV, SoLuong = kq.Sum(p => p.dtct.SoLuong) }).Where(p => p.SoLuong != 0).ToList();
                            if (DungChung.Bien.MaBV == "24272")
                            {
                                dt_rep = (from dt in qtong.Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : (p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle || p.IDNhom == _idnhom5))
                                          from t in thuocs1
                                          where dt.dtct.MaDV == t.MaDV
                                          group dt by new { dt.dtct.DonGia, dt.dtct.DonVi, dt.dtct.MaDV, t.NgayKe, t.SoLuong } into kq
                                          select new { kq.Key.DonGia, kq.Key.DonVi, kq.Key.NgayKe, kq.Key.MaDV, SoLuong = kq.Key.SoLuong }).Where(p => p.SoLuong != 0).ToList();

                            }

                            foreach (var a in dt_rep)
                            {
                                FormNhap.usDieuTri.l_CTThuoc moi = new FormNhap.usDieuTri.l_CTThuoc();
                                moi.MaDV = a.MaDV == null ? 0 : a.MaDV.Value;
                                moi.NgayKe = a.NgayKe.Value;
                                moi.SoLuong = a.SoLuong;
                                moi.DonGia = a.DonGia;
                                moi.DonVi = a.DonVi;
                                _ldthuocct.Add(moi);
                            }
                            string[] _songay;


                            var ngaykd = (from nk in qtong.Where(p => p.dthc.PLDV == 1).Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : (p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle || p.IDNhom == _idnhom5))

                                          where nk.dthc.NgayKe != null
                                          select new { nk.dthc.NgayKe }).ToList().Select(x => new { x.NgayKe.Value.Date }).Distinct().OrderBy(p => p.Date).ToList();

                            var ngaykd_24272 = (from nk in qtong.Where(p => p.dthc.PLDV == 1).Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : (p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle || p.IDNhom == _idnhom5))

                                                where nk.dthc.NgayKe != null
                                                select new { nk.dthc.NgayKe, nk.dtct.Loai }).ToList().Select(x => new { x.NgayKe.Value.Date, Loai = 18 }).Distinct().OrderBy(p => p.Date).ToList();

                            List<DateTime> ngay = new List<DateTime>();
                            foreach (var item in ngaykd_24272)
                            {
                                for (int index = 0; index < item.Loai; index++)
                                {
                                    ngay.Add(item.Date.AddDays(index));
                                }
                            }
                            int i = 0;
                            ngay = ngay.Distinct().ToList();
                            if (DungChung.Bien.MaBV == "24272")
                            {
                                _songay = new string[ngay.Count];
                                foreach (var a in ngay)
                                {
                                    _songay[i] = a.Date.ToShortDateString();
                                    if (_songay[i] == null)
                                    {
                                        _songay[i] = a.Date.AddDays(i).ToShortDateString();
                                    }
                                    i++;
                                }
                            }
                            else
                            {
                                _songay = new string[ngaykd.Count];
                                foreach (var a in ngaykd)
                                {
                                    _songay[i] = a.Date.ToShortDateString();
                                    i++;
                                }
                            }
                            var par = (from bn in data.BenhNhans.Where(p => p.MaBNhan == _MaBN)
                                       join kb in data.BNKBs.OrderByDescending(p => p.IDKB) on bn.MaBNhan equals kb.MaBNhan
                                       join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                       //join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                       select new
                                       {
                                           GTinh = bn.GTinh == 0 ? "Nữ" : "Nam",
                                           kb.Buong,
                                           kb.Giuong,
                                           bn.DTuong,
                                           bn.MaBNhan,
                                           bn.DChi,
                                           bn.Tuoi,
                                           bn.SThe,
                                           bn.MaCS,
                                           bn.NoiTru,
                                           bn.HanBHTu,
                                           bn.HanBHDen,
                                           kb.MaICD,
                                           kb.MaICD2,
                                           kb.ChanDoan,
                                           vv.NgayVao,
                                           kb.IDKB,
                                           kb.BenhKhac,
                                           bn.NgaySinh,
                                           bn.ThangSinh,
                                           bn.NamSinh,
                                           bn.NNhap,
                                           kb.BenhKhacYHCT,
                                           kb.ChanDoanYHCT,
                                           kb.MaYHCT
                                       }).OrderByDescending(p => p.IDKB).ToList();


                            if (DungChung.Bien.MaBV == "24272")
                            {
                                for (int j = 0; j < _songay.Length; j += 18)
                                {
                                    bool _InCongKhoan = false;
                                    if (j > _songay.Length - 2)
                                        _InCongKhoan = true;
                                    int m = 0;
                                    string[] _songayT1 = new string[18];
                                    for (int l = 0; l < 18; l++)
                                    {
                                        _songayT1[l] = "";
                                    }
                                    for (int k = j; k < _songay.Length; k++)
                                    {
                                        if (m < 18)
                                        {
                                            _songayT1[m] = _songay[k];
                                            m++;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    #region Mẫu cho 20001: bỏ mã ICD, thanh tiền, đơn giá, ghi chú
                                    //if (DungChung.Bien.MaBV == "20001")
                                    //{
                                    //    BaoCao.rep_PhieuCongKhaiThuoc_TT23_20001 rep = new BaoCao.rep_PhieuCongKhaiThuoc_TT23_20001(_songayT1, _MaBN, _ldthuocct, _InCongKhoan);
                                    //    if (par.Count > 0)
                                    //    {
                                    //        ngayvao = DungChung.Ham.NgayTu(par.First().NgayVao.Value);
                                    //        if (f == 1)
                                    //            rep.colMauSo.Text = "Ms: 10/YHCT-2014";
                                    //        rep.So.Value = "Số: ...........";
                                    //        var khoa = (from kp in data.KPhongs join bnkb in data.BNKBs.Where(p => p.MaBNhan == _MaBN) on kp.MaKP equals bnkb.MaKP select new { kp.TenKP, bnkb.IDKB }).OrderByDescending(p => p.IDKB).ToList();
                                    //        if (khoa.Count > 0)
                                    //            rep.Khoa.Value = khoa.First().TenKP.ToUpper();
                                    //        var tenbn = data.BenhNhans.Where(p => p.MaBNhan == _MaBN).FirstOrDefault();
                                    //        if (tenbn != null)
                                    //            rep.TenBN.Value = tenbn.TenBNhan.ToString().ToUpper();
                                    //        rep.TuoiBN.Value = DungChung.Ham.TuoitheoThang(data, _MaBN, DungChung.Bien.formatAge);

                                    //        rep.TieuDe.Value = f == 0 ? "PHIẾU CÔNG KHAI THUỐC" : "PHIẾU CÔNG KHAI VẬT TƯ Y TẾ TIÊU HAO";
                                    //        rep.GioiTinh.Value = par.First().GTinh;
                                    //        rep.DiaChi.Value = par.First().DChi;
                                    //        rep.SoThe.Value = par.First().SThe + " - " + par.First().MaCS;
                                    //        if (par.First().DTuong == "BHYT")
                                    //        {
                                    //            rep.GTTu.Value = par.First().HanBHTu;
                                    //            rep.GTDen.Value = par.First().HanBHDen;
                                    //        }
                                    //        else
                                    //        {
                                    //            rep.GTTu.Value = null;
                                    //            rep.GTDen.Value = null;
                                    //        }
                                    //        rep.Buong.Value = par.First().Buong;
                                    //        rep.Giuong.Value = par.First().Giuong;
                                    //        rep.NgayVV.Value = par.First().NgayVao;
                                    //        rep.DiaChi.Value = DungChung.Ham.GetICDstr(par.First().ChanDoan + par.First().BenhKhac);
                                    //        //rep.MaChanDoan.Value = par.First().MaICD;
                                    //        var rvien = data.RaViens.Where(p => p.MaBNhan == _MaBN).ToList();
                                    //        if (rvien.Count > 0)
                                    //        {
                                    //            rep.NgayRV.Value = rvien.First().NgayRa;
                                    //        }
                                    //        double thuoc = 0;
                                    //        var q = (from dt in qtong.Where(p => p.dthc.PLDV == 1).Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : p.IDNhom == _idnhomVT)

                                    //                 //where (dt.MaKP== (DungChung.Bien.MaKP))
                                    //                 group dt by new { dt.TenNhom, dt.dtct.MaDV, dt.TenDV, dt.TenRG, dt.dtct.DonVi, dt.dtct.DonGia } into kq
                                    //                 select new
                                    //                 {
                                    //                     MaDV = kq.Key.MaDV,
                                    //                     kq.Key.DonGia,
                                    //                     TenNhomDV = kq.Key.TenNhom.ToUpper(),
                                    //                     TenDV = kq.Key.TenDV,
                                    //                     DonVi = kq.Key.DonVi,
                                    //                     SoLuongT = kq.Sum(p => p.dtct.SoLuong),
                                    //                     ThanhTienT = kq.Sum(p => p.dtct.ThanhTien)
                                    //                 }).Where(p => p.SoLuongT != 0).ToList();
                                    //        if (q.Count > 0)
                                    //            thuoc = q.Sum(p => p.ThanhTienT);
                                    //        rep.DataSource = q.ToList();
                                    //        rep.BindingData();
                                    //        rep.CreateDocument();
                                    //        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    //        frm.ShowDialog();

                                    //    }
                                    //    else
                                    //    {
                                    //        MessageBox.Show("Bệnh nhân chưa có khám bệnh vào viện");
                                    //    }

                                    //}
                                    //#endregion
                                    //#region BV khác
                                    //else
                                    //{
                                    #endregion // bo // bỏ theo y/c thụy 05-09 đức
                                    BaoCao.rep_PhieuCongKhaiThuoc_TT23 rep = new BaoCao.rep_PhieuCongKhaiThuoc_TT23(_songayT1, _MaBN, _ldthuocct, _InCongKhoan);

                                    if (par.Count > 0)
                                    {
                                        ngayvao = DungChung.Ham.NgayTu(par.First().NgayVao.Value);

                                        var khoa = (from kp in data.KPhongs join bnkb in data.BNKBs.Where(p => p.MaBNhan == _MaBN) on kp.MaKP equals bnkb.MaKP select new { kp.TenKP, bnkb.IDKB, kp.IsDongY }).OrderByDescending(p => p.IDKB).ToList();
                                        if (khoa.Count > 0)
                                            rep.Khoa.Value = khoa.First().TenKP.ToUpper();
                                        var tenbn = data.BenhNhans.Where(p => p.MaBNhan == _MaBN).FirstOrDefault();
                                        if (tenbn != null)
                                            rep.TenBN.Value = tenbn.TenBNhan.ToString().ToUpper();
                                        rep.TuoiBN.Value = DungChung.Ham.TuoitheoThang(data, _MaBN, DungChung.Bien.formatAge);

                                        rep.TieuDe.Value = f == 0 ? "PHIẾU CÔNG KHAI THUỐC" : "PHIẾU CÔNG KHAI VẬT TƯ Y TẾ TIÊU HAO";
                                        rep.GioiTinh.Value = par.First().GTinh;
                                        rep.DiaChi.Value = par.First().DChi;
                                        rep.SoThe.Value = par.First().SThe + " - " + par.First().MaCS;
                                        if (par.First().DTuong == "BHYT")
                                        {
                                            rep.GTTu.Value = par.First().HanBHTu;
                                            rep.GTDen.Value = par.First().HanBHDen;
                                        }
                                        else
                                        {
                                            rep.GTTu.Value = null;
                                            rep.GTDen.Value = null;
                                        }
                                        rep.Buong.Value = par.First().Buong;
                                        rep.Giuong.Value = par.First().Giuong;
                                        rep.NgayVV.Value = par.First().NgayVao;
                                        if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24297")
                                        {
                                            var icd = data.ICD10.Where(o => true).ToList();
                                            rep.ChanDoan.Value = DungChung.Ham.GhepChuoiChanDoanYHCT(icd, par.First().MaICD, par.First().MaICD2);
                                        }
                                        else
                                            rep.ChanDoan.Value = DungChung.Ham.GetICDstr(par.First().ChanDoan + par.First().BenhKhac);
                                        if (DungChung.Bien.MaBV == "24297")
                                        {
                                            if (khoa.First().IsDongY == true)
                                                rep.MaChanDoan.Value = par.First().MaYHCT;
                                            else
                                                rep.MaChanDoan.Value = par.First().MaICD;
                                        }

                                        else
                                            rep.MaChanDoan.Value = par.First().MaICD;
                                        var rvien = data.RaViens.Where(p => p.MaBNhan == _MaBN).ToList();
                                        if (rvien.Count > 0)
                                        {
                                            rep.NgayRV.Value = rvien.First().NgayRa;
                                        }
                                        double thuoc = 0;
                                        var q = (from dt in qtong.Where(p => p.dthc.PLDV == 1).Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : (p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle || p.IDNhom == _idnhom5))

                                                     //where (dt.MaKP== (DungChung.Bien.MaKP))
                                                 group dt by new { dt.TenNhom, dt.dtct.MaDV, dt.TenDV, dt.TenRG, dt.dtct.DonVi, dt.dtct.DonGia, dt.DongY } into kq
                                                 select new
                                                 {
                                                     MaDV = kq.Key.MaDV,
                                                     kq.Key.DonGia,
                                                     TenNhomDV = kq.Key.TenNhom.ToUpper(),
                                                     TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009" || DungChung.Bien.MaBV == "01830") ? (kq.Key.TenRG ?? "") : kq.Key.TenDV,
                                                     DonVi = kq.Key.DonVi,
                                                     SoLuongT = kq.Sum(p => p.dtct.SoLuong),
                                                     ThanhTienT = kq.Sum(p => p.dtct.ThanhTien),
                                                     DongY = kq.Key.DongY ?? 0
                                                 }).Where(p => p.SoLuongT != 0).ToList();
                                        if (q.Count > 0)
                                            thuoc = q.Sum(p => p.ThanhTienT);
                                        if (DungChung.Bien.MaBV == "24297")
                                            q = q.OrderBy(o => o.DongY).ToList();
                                        rep.DataSource = q.ToList();
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                    }
                                    else
                                    {
                                        MessageBox.Show("Bệnh nhân chưa có khám bệnh vào viện");
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < _songay.Length; j += 17)
                                {
                                    bool _InCongKhoan = false;
                                    if (j > _songay.Length - 2)
                                        _InCongKhoan = true;
                                    int m = 0;
                                    string[] _songayT1 = new string[18];
                                    for (int l = 0; l < 18; l++)
                                    {
                                        _songayT1[l] = "";
                                    }
                                    for (int k = j; k < _songay.Length; k++)
                                    {
                                        if (m < 17)
                                        {
                                            _songayT1[m] = _songay[k];
                                            m++;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    #region Mẫu cho 20001: bỏ mã ICD, thanh tiền, đơn giá, ghi chú
                                    //if (DungChung.Bien.MaBV == "20001")
                                    //{
                                    //    BaoCao.rep_PhieuCongKhaiThuoc_TT23_20001 rep = new BaoCao.rep_PhieuCongKhaiThuoc_TT23_20001(_songayT1, _MaBN, _ldthuocct, _InCongKhoan);
                                    //    if (par.Count > 0)
                                    //    {
                                    //        ngayvao = DungChung.Ham.NgayTu(par.First().NgayVao.Value);
                                    //        if (f == 1)
                                    //            rep.colMauSo.Text = "Ms: 10/YHCT-2014";
                                    //        rep.So.Value = "Số: ...........";
                                    //        var khoa = (from kp in data.KPhongs join bnkb in data.BNKBs.Where(p => p.MaBNhan == _MaBN) on kp.MaKP equals bnkb.MaKP select new { kp.TenKP, bnkb.IDKB }).OrderByDescending(p => p.IDKB).ToList();
                                    //        if (khoa.Count > 0)
                                    //            rep.Khoa.Value = khoa.First().TenKP.ToUpper();
                                    //        var tenbn = data.BenhNhans.Where(p => p.MaBNhan == _MaBN).FirstOrDefault();
                                    //        if (tenbn != null)
                                    //            rep.TenBN.Value = tenbn.TenBNhan.ToString().ToUpper();
                                    //        rep.TuoiBN.Value = DungChung.Ham.TuoitheoThang(data, _MaBN, DungChung.Bien.formatAge);

                                    //        rep.TieuDe.Value = f == 0 ? "PHIẾU CÔNG KHAI THUỐC" : "PHIẾU CÔNG KHAI VẬT TƯ Y TẾ TIÊU HAO";
                                    //        rep.GioiTinh.Value = par.First().GTinh;
                                    //        rep.DiaChi.Value = par.First().DChi;
                                    //        rep.SoThe.Value = par.First().SThe + " - " + par.First().MaCS;
                                    //        if (par.First().DTuong == "BHYT")
                                    //        {
                                    //            rep.GTTu.Value = par.First().HanBHTu;
                                    //            rep.GTDen.Value = par.First().HanBHDen;
                                    //        }
                                    //        else
                                    //        {
                                    //            rep.GTTu.Value = null;
                                    //            rep.GTDen.Value = null;
                                    //        }
                                    //        rep.Buong.Value = par.First().Buong;
                                    //        rep.Giuong.Value = par.First().Giuong;
                                    //        rep.NgayVV.Value = par.First().NgayVao;
                                    //        rep.DiaChi.Value = DungChung.Ham.GetICDstr(par.First().ChanDoan + par.First().BenhKhac);
                                    //        //rep.MaChanDoan.Value = par.First().MaICD;
                                    //        var rvien = data.RaViens.Where(p => p.MaBNhan == _MaBN).ToList();
                                    //        if (rvien.Count > 0)
                                    //        {
                                    //            rep.NgayRV.Value = rvien.First().NgayRa;
                                    //        }
                                    //        double thuoc = 0;
                                    //        var q = (from dt in qtong.Where(p => p.dthc.PLDV == 1).Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : p.IDNhom == _idnhomVT)

                                    //                 //where (dt.MaKP== (DungChung.Bien.MaKP))
                                    //                 group dt by new { dt.TenNhom, dt.dtct.MaDV, dt.TenDV, dt.TenRG, dt.dtct.DonVi, dt.dtct.DonGia } into kq
                                    //                 select new
                                    //                 {
                                    //                     MaDV = kq.Key.MaDV,
                                    //                     kq.Key.DonGia,
                                    //                     TenNhomDV = kq.Key.TenNhom.ToUpper(),
                                    //                     TenDV = kq.Key.TenDV,
                                    //                     DonVi = kq.Key.DonVi,
                                    //                     SoLuongT = kq.Sum(p => p.dtct.SoLuong),
                                    //                     ThanhTienT = kq.Sum(p => p.dtct.ThanhTien)
                                    //                 }).Where(p => p.SoLuongT != 0).ToList();
                                    //        if (q.Count > 0)
                                    //            thuoc = q.Sum(p => p.ThanhTienT);
                                    //        rep.DataSource = q.ToList();
                                    //        rep.BindingData();
                                    //        rep.CreateDocument();
                                    //        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    //        frm.ShowDialog();

                                    //    }
                                    //    else
                                    //    {
                                    //        MessageBox.Show("Bệnh nhân chưa có khám bệnh vào viện");
                                    //    }

                                    //}
                                    //#endregion
                                    //#region BV khác
                                    //else
                                    //{
                                    #endregion // bo // bỏ theo y/c thụy 05-09 đức
                                    BaoCao.rep_PhieuCongKhaiThuoc_TT23 rep = new BaoCao.rep_PhieuCongKhaiThuoc_TT23(_songayT1, _MaBN, _ldthuocct, _InCongKhoan);

                                    if (par.Count > 0)
                                    {
                                        ngayvao = DungChung.Ham.NgayTu(par.First().NgayVao.Value);

                                        var khoa = (from kp in data.KPhongs join bnkb in data.BNKBs.Where(p => p.MaBNhan == _MaBN) on kp.MaKP equals bnkb.MaKP select new { kp.TenKP, bnkb.IDKB, kp.IsDongY }).OrderByDescending(p => p.IDKB).ToList();
                                        if (khoa.Count > 0)
                                            rep.Khoa.Value = khoa.First().TenKP.ToUpper();
                                        var tenbn = data.BenhNhans.Where(p => p.MaBNhan == _MaBN).FirstOrDefault();
                                        if (tenbn != null)
                                            rep.TenBN.Value = tenbn.TenBNhan.ToString().ToUpper();
                                        rep.TuoiBN.Value = DungChung.Ham.TuoitheoThang(data, _MaBN, DungChung.Bien.formatAge);

                                        rep.TieuDe.Value = f == 0 ? "PHIẾU CÔNG KHAI THUỐC" : "PHIẾU CÔNG KHAI VẬT TƯ Y TẾ TIÊU HAO";
                                        rep.GioiTinh.Value = par.First().GTinh;
                                        rep.DiaChi.Value = par.First().DChi;
                                        rep.SoThe.Value = par.First().SThe + " - " + par.First().MaCS;
                                        if (par.First().DTuong == "BHYT")
                                        {
                                            rep.GTTu.Value = par.First().HanBHTu;
                                            rep.GTDen.Value = par.First().HanBHDen;
                                        }
                                        else
                                        {
                                            rep.GTTu.Value = null;
                                            rep.GTDen.Value = null;
                                        }
                                        rep.Buong.Value = par.First().Buong;
                                        rep.Giuong.Value = par.First().Giuong;
                                        rep.NgayVV.Value = par.First().NgayVao;
                                        if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24297")
                                        {
                                            var icd = data.ICD10.Where(o => true).ToList();
                                            rep.ChanDoan.Value = DungChung.Ham.GhepChuoiChanDoanYHCT(icd, par.First().MaICD, par.First().MaICD2);
                                        }
                                        else
                                            rep.ChanDoan.Value = DungChung.Ham.GetICDstr(par.First().ChanDoan + par.First().BenhKhac);
                                        if (DungChung.Bien.MaBV == "24297")
                                        {
                                            if (khoa.First().IsDongY == true)
                                                rep.MaChanDoan.Value = par.First().MaYHCT;
                                            else
                                                rep.MaChanDoan.Value = par.First().MaICD;
                                        }

                                        else
                                            rep.MaChanDoan.Value = par.First().MaICD;
                                        var rvien = data.RaViens.Where(p => p.MaBNhan == _MaBN).ToList();
                                        if (rvien.Count > 0)
                                        {
                                            rep.NgayRV.Value = rvien.First().NgayRa;
                                        }
                                        double thuoc = 0;
                                        var q = (from dt in qtong.Where(p => p.dthc.PLDV == 1).Where(p => f == 0 ? p.IDNhom == _idnhomthuoc : (p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle || p.IDNhom == _idnhom5))

                                                     //where (dt.MaKP== (DungChung.Bien.MaKP))
                                                 group dt by new { dt.TenNhom, dt.dtct.MaDV, dt.TenDV, dt.TenRG, dt.dtct.DonVi, dt.dtct.DonGia, dt.DongY } into kq
                                                 select new
                                                 {
                                                     MaDV = kq.Key.MaDV,
                                                     kq.Key.DonGia,
                                                     TenNhomDV = kq.Key.TenNhom.ToUpper(),
                                                     TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009" || DungChung.Bien.MaBV == "01830") ? (kq.Key.TenRG ?? "") : kq.Key.TenDV,
                                                     DonVi = kq.Key.DonVi,
                                                     SoLuongT = kq.Sum(p => p.dtct.SoLuong),
                                                     ThanhTienT = kq.Sum(p => p.dtct.ThanhTien),
                                                     DongY = kq.Key.DongY ?? 0
                                                 }).Where(p => p.SoLuongT != 0).ToList();
                                        if (q.Count > 0)
                                            thuoc = q.Sum(p => p.ThanhTienT);
                                        if (DungChung.Bien.MaBV == "24297")
                                            q = q.OrderBy(o => o.DongY).ToList();
                                        rep.DataSource = q.ToList();
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                    }
                                    else
                                    {
                                        MessageBox.Show("Bệnh nhân chưa có khám bệnh vào viện");
                                    }
                                }
                            }

                        }


                        else
                        {
                            MessageBox.Show("Bạn chưa chọn BN hoặc không có BN");
                        }
                    }
                }
                
                #endregion
            }

            else
            {
                if (_MaBN > 0)
                {
                    var dt_rep = (from dt in qtong.Where(p => p.IDNhom == _idnhomthuoc || p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle|| p.IDNhom == _idnhom5)
                                  group dt by new { dt.dtct.DonGia, dt.dthc.NgayKe, dt.dtct.DonVi, dt.dtct.MaDV } into kq
                                  select new { kq.Key.DonGia, kq.Key.DonVi, kq.Key.NgayKe, kq.Key.MaDV, SoLuong = kq.Sum(p => p.dtct.SoLuong) }).Where(p => p.SoLuong != 0).ToList();
                    
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        dt_rep = (from dt in qtong.Where(p => p.IDNhom == _idnhomthuoc || p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle || p.IDNhom == _idnhom5)
                                  from t in thuocs1
                                  where dt.dtct.MaDV == t.MaDV
                                  group dt by new { dt.dtct.DonGia, dt.dtct.DonVi, dt.dtct.MaDV, t.NgayKe, t.SoLuong } into kq
                                  select new { kq.Key.DonGia, kq.Key.DonVi, kq.Key.NgayKe, kq.Key.MaDV, SoLuong = kq.Key.SoLuong }).Where(p => p.SoLuong != 0).ToList();

                    }
                    foreach (var a in dt_rep)
                    {
                        FormNhap.usDieuTri.l_CTThuoc moi = new FormNhap.usDieuTri.l_CTThuoc();
                        moi.MaDV = a.MaDV == null ? 0 : a.MaDV.Value;
                        moi.NgayKe = a.NgayKe.Value;
                        moi.SoLuong = a.SoLuong;
                        moi.DonGia = a.DonGia;
                        moi.DonVi = a.DonVi;
                        _ldthuocct.Add(moi);
                    }
                    string[] _songay;


                    var ngaykd = (from nk in qtong.Where(p => p.dthc.PLDV == 1).Where(p => p.IDNhom == _idnhomthuoc || p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle|| p.IDNhom == _idnhom5)

                                  where nk.dthc.NgayKe != null
                                  select nk.dthc.NgayKe).ToList().Select(x => new { x.Value.Date }).Distinct().OrderBy(p => p.Date).ToList();
                    var ngaykd_24272 = (from nk in qtong.Where(p => p.dthc.PLDV == 1).Where(p => p.IDNhom == _idnhomthuoc || p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle || p.IDNhom == _idnhom5)

                                        where nk.dthc.NgayKe != null
                                        select new { nk.dthc.NgayKe, nk.dtct.Loai }).ToList().Select(x => new { x.NgayKe.Value.Date, x.Loai }).Distinct().OrderBy(p => p.Date).ToList();

                    List<DateTime> ngay = new List<DateTime>();
                    foreach (var item in ngaykd_24272)
                    {
                        if (item.Loai > 1)
                        {
                            for (int index = 0; index < item.Loai; index++)
                            {
                                ngay.Add(item.Date.AddDays(index));
                            }
                        }
                    }
                    int i = 0;
                    ngay = ngay.Distinct().ToList();
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        _songay = new string[ngay.Count];
                        foreach (var a in ngay)
                        {
                            _songay[i] = a.Date.ToShortDateString();
                            i++;
                        }
                    }
                    else
                    {
                        _songay = new string[ngaykd.Count];
                        foreach (var a in ngaykd)
                        {
                            _songay[i] = a.Date.ToShortDateString();
                            i++;
                        }
                    }
                    var par = (from bn in data.BenhNhans.Where(p => p.MaBNhan == _MaBN)
                               join kb in data.BNKBs.OrderByDescending(p => p.IDKB) on bn.MaBNhan equals kb.MaBNhan
                               join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                               select new
                               {
                                   GTinh = bn.GTinh == 0 ? "Nữ" : "Nam",
                                   kb.Buong,
                                   kb.Giuong,
                                   bn.DTuong,
                                   bn.MaBNhan,
                                   bn.DChi,
                                   bn.Tuoi,
                                   bn.SThe,
                                   bn.MaCS,
                                   bn.NoiTru,
                                   bn.HanBHTu,
                                   bn.HanBHDen,
                                   kb.MaICD,
                                   kb.MaYHCT,
                                   kb.MaICD2,
                                   kb.ChanDoan,
                                   vv.NgayVao,
                                   kb.IDKB,
                                   kb.BenhKhac,
                                   bn.NgaySinh,
                                   bn.ThangSinh,
                                   bn.NamSinh,
                                   bn.NNhap,
                                   kb.ChanDoanYHCT,
                                   kb.BenhKhacYHCT
                               }).OrderByDescending(p => p.IDKB).ToList();



                    for (int j = 0; j < _songay.Length; j += 17)
                    {
                        bool _InCongKhoan = false;
                        if (j > _songay.Length - 2)
                            _InCongKhoan = true;
                        int m = 0;
                        string[] _songayT1 = new string[18];
                        for (int l = 0; l < 18; l++)
                        {
                            _songayT1[l] = "";
                        }
                        for (int k = j; k < _songay.Length; k++)
                        {
                            if (m < 17)
                            {
                                _songayT1[m] = _songay[k];
                                m++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        #region Mẫu cho 20001: bỏ mã ICD, thanh tiền, đơn giá, ghi chú
                        //if (DungChung.Bien.MaBV == "20001")
                        //{
                        //    BaoCao.rep_PhieuCongKhaiThuoc_TT23_20001 rep = new BaoCao.rep_PhieuCongKhaiThuoc_TT23_20001(_songayT1, _MaBN, _ldthuocct, _InCongKhoan);
                        //    if (par.Count > 0)
                        //    {
                        //        ngayvao = DungChung.Ham.NgayTu(par.First().NgayVao.Value);
                        //        rep.colMauSo.Text = "Ms: 10/YHCT-2014";
                        //        rep.So.Value = "Số: ...........";
                        //        var khoa = (from kp in data.KPhongs join bnkb in data.BNKBs.Where(p => p.MaBNhan == _MaBN) on kp.MaKP equals bnkb.MaKP select new { kp.TenKP, bnkb.IDKB }).OrderByDescending(p => p.IDKB).ToList();
                        //        if (khoa.Count > 0)
                        //            rep.Khoa.Value = khoa.First().TenKP.ToUpper();
                        //        var tenbn = data.BenhNhans.Where(p => p.MaBNhan == _MaBN).FirstOrDefault();
                        //        if (tenbn != null)
                        //            rep.TenBN.Value = tenbn.TenBNhan.ToString().ToUpper();
                        //        rep.TuoiBN.Value = DungChung.Ham.TuoitheoThang(data, _MaBN, DungChung.Bien.formatAge);

                        //        rep.TieuDe.Value = "PHIẾU CÔNG KHAI THUỐC, VẬT TƯ Y TẾ TIÊU HAO";
                        //        rep.GioiTinh.Value = par.First().GTinh;
                        //        rep.DiaChi.Value = par.First().DChi;
                        //        rep.SoThe.Value = par.First().SThe + " - " + par.First().MaCS;
                        //        if (par.First().DTuong == "BHYT")
                        //        {
                        //            rep.GTTu.Value = par.First().HanBHTu;
                        //            rep.GTDen.Value = par.First().HanBHDen;
                        //        }
                        //        else
                        //        {
                        //            rep.GTTu.Value = null;
                        //            rep.GTDen.Value = null;
                        //        }
                        //        rep.Buong.Value = par.First().Buong;
                        //        rep.Giuong.Value = par.First().Giuong;
                        //        rep.NgayVV.Value = par.First().NgayVao;
                        //        rep.DiaChi.Value = DungChung.Ham.GetICDstr(par.First().ChanDoan + par.First().BenhKhac);
                        //        //rep.MaChanDoan.Value = par.First().MaICD;
                        //        var rvien = data.RaViens.Where(p => p.MaBNhan == _MaBN).ToList();
                        //        if (rvien.Count > 0)
                        //        {
                        //            rep.NgayRV.Value = rvien.First().NgayRa;
                        //        }
                        //        double thuoc = 0;
                        //        var q = (from dt in qtong.Where(p => p.dthc.PLDV == 1).Where(p => p.IDNhom == _idnhomthuoc || p.IDNhom == _idnhomVT)

                        //                 //where (dt.MaKP== (DungChung.Bien.MaKP))
                        //                 group dt by new { dt.TenNhom, dt.dtct.MaDV, dt.TenDV, dt.TenRG, dt.dtct.DonVi, dt.dtct.DonGia } into kq
                        //                 select new
                        //                 {
                        //                     MaDV = kq.Key.MaDV,
                        //                     kq.Key.DonGia,
                        //                     TenNhomDV = kq.Key.TenNhom.ToUpper(),
                        //                     TenDV = kq.Key.TenDV,
                        //                     DonVi = kq.Key.DonVi,
                        //                     SoLuongT = kq.Sum(p => p.dtct.SoLuong),
                        //                     ThanhTienT = kq.Sum(p => p.dtct.ThanhTien)
                        //                 }).Where(p => p.SoLuongT != 0).ToList();
                        //        if (q.Count > 0)
                        //            thuoc = q.Sum(p => p.ThanhTienT);
                        //        rep.DataSource = q.ToList();
                        //        rep.BindingData();
                        //        rep.CreateDocument();
                        //        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        //        frm.ShowDialog();

                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Bệnh nhân chưa có khám bệnh vào viện");
                        //    }

                        //}
                        #endregion //bỏ
                        #region BV khác

                        BaoCao.rep_PhieuCongKhaiThuoc_TT23 rep = new BaoCao.rep_PhieuCongKhaiThuoc_TT23(_songayT1, _MaBN, _ldthuocct, _InCongKhoan);

                        if (par.Count > 0)
                        {
                            ngayvao = DungChung.Ham.NgayTu(par.First().NgayVao.Value);
                            var khoa = (from kp in data.KPhongs join bnkb in data.BNKBs.Where(p => p.MaBNhan == _MaBN) on kp.MaKP equals bnkb.MaKP select new { bnkb.MaKP, kp.TenKP, bnkb.IDKB, kp.IsDongY }).OrderByDescending(p => p.IDKB).ToList();
                            if (khoa.Count > 0)
                            {
                                rep.Khoa.Value = khoa.First().TenKP.ToUpper();
                                _makp = khoa.First().MaKP.Value;
                            }
                            var tenbn = data.BenhNhans.Where(p => p.MaBNhan == _MaBN).FirstOrDefault();
                            if (tenbn != null)
                                rep.TenBN.Value = tenbn.TenBNhan.ToString().ToUpper();
                            rep.TuoiBN.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(data, _MaBN) : DungChung.Ham.TuoitheoThang(data, _MaBN, DungChung.Bien.formatAge);

                            rep.TieuDe.Value = "PHIẾU CÔNG KHAI THUỐC, VẬT TƯ Y TẾ TIÊU HAO";
                            rep.GioiTinh.Value = par.First().GTinh;
                            rep.DiaChi.Value = par.First().DChi;
                            rep.SoThe.Value = par.First().SThe + " - " + par.First().MaCS;
                            if (par.First().DTuong == "BHYT")
                            {
                                rep.GTTu.Value = par.First().HanBHTu;
                                rep.GTDen.Value = par.First().HanBHDen;
                            }
                            else
                            {
                                rep.GTTu.Value = null;
                                rep.GTDen.Value = null;
                            }
                            rep.Buong.Value = par.First().Buong;
                            rep.Giuong.Value = par.First().Giuong;
                            rep.NgayVV.Value = par.First().NgayVao;
                            if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24297")
                            {
                                var icd = data.ICD10.Where(o => true).ToList();
                                rep.ChanDoan.Value = DungChung.Ham.GhepChuoiChanDoanYHCT(icd, par.First().MaICD, par.First().MaICD2);
                            }
                            else
                                rep.ChanDoan.Value = DungChung.Ham.GetICDstr(par.First().ChanDoan + "; " + par.First().BenhKhac);
                            if (DungChung.Bien.MaBV == "24297")
                            {
                                if (khoa.First().IsDongY == true)
                                    rep.MaChanDoan.Value = par.First().MaYHCT;
                                else
                                    rep.MaChanDoan.Value = par.First().MaICD;
                            }
                                
                            else
                                rep.MaChanDoan.Value = par.First().MaICD;
                            var rvien = data.RaViens.Where(p => p.MaBNhan == _MaBN).ToList();
                            if (rvien.Count > 0)
                            {
                                rep.NgayRV.Value = rvien.First().NgayRa;
                            }
                            double thuoc = 0;
                            var q = (from dt in qtong.Where(p => p.dthc.PLDV == 1).Where(p => p.IDNhom == _idnhomthuoc || p.IDNhom == _idnhomVT || p.IDNhom == _idnhomVTTyle || p.IDNhom == _idnhomthuocTyle|| p.IDNhom == _idnhom5)

                                         //where (dt.MaKP== (DungChung.Bien.MaKP))
                                     group dt by new { dt.TenNhom, dt.dtct.MaDV, dt.TenDV, dt.TenRG, dt.dtct.DonVi, dt.dtct.DonGia, dt.DongY } into kq
                                     select new
                                     {
                                         MaDV = kq.Key.MaDV,
                                         kq.Key.DonGia,
                                         TenNhomDV = kq.Key.TenNhom.ToUpper(),
                                         TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009" || DungChung.Bien.MaBV == "01830") ? (kq.Key.TenRG ?? "") : kq.Key.TenDV,
                                         DonVi = kq.Key.DonVi,
                                         SoLuongT = kq.Sum(p => p.dtct.SoLuong),
                                         ThanhTienT = kq.Sum(p => p.dtct.ThanhTien),
                                         DongY = kq.Key.DongY
                                     }).Where(p => p.SoLuongT != 0).ToList();
                            if (q.Count > 0)
                                thuoc = q.Sum(p => p.ThanhTienT);
                            if (DungChung.Bien.MaBV == "24297")
                                q = q.OrderBy(o => o.DongY).ToList();
                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("Bệnh nhân chưa có khám bệnh vào viện");
                        }

                    }
                }
                #endregion
                else
                {
                    MessageBox.Show("Bạn chưa chọn BN hoặc không có BN");
                }
            }
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int second = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            second = second + 1;
            if (second == 4)
            {
                timer1.Stop();
                btnInbc_Click(sender, e);
            }
        }

        private void ckcVTYT_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            if (ckcVTYT.Checked)
            {
                rgChonMau.Enabled = true;
            }
            else
            {
                rgChonMau.Enabled = false;
            }
        }

        private void rgChonMau_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        public class thuoc
        {
            public int MaDV { get; set; }
            public DateTime? NgayKe { get; set; }
            public double SoLuong { get; set; }
        }
    }
}