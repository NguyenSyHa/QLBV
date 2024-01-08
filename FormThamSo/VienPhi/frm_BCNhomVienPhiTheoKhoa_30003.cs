using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using QLBV.BaoCao;

namespace QLBV.FormThamSo
{
    public partial class frm_BCNhomVienPhiTheoKhoa_30003 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCNhomVienPhiTheoKhoa_30003()
        {
            InitializeComponent();
        }
        public class MyObject
        {
            public int Value { set; get; }
        }
        string DTBN11 = "";
        List<KPhong> _lKPhong = new List<KPhong>();
        private void frm_BCVienPhi_30003_Load(object sender, EventArgs e)
        {
            //load ds năm
            int namHT = DateTime.Now.Year;
            List<MyObject> _list = new List<MyObject>();
            List<MyObject> _listthang = new List<MyObject>();
            
            if (rdBaoHiem.SelectedIndex == 0)
                DTBN11 = "BHYT";
            else if (rdBaoHiem.SelectedIndex == 1)
                DTBN11 = "Dịch vụ";
            else
                DTBN11 = "KSK";

            for (int i = namHT - 10; i < namHT + 11; i++)
            {
                MyObject obj = new MyObject();
                obj.Value = i;
                _list.Add(obj);
            }
            rdNgay_SelectedIndexChanged(null,null);
            rdBaoHiem.SelectedIndex = 3;
            rgCHon.SelectedIndex = 4;
            for (int i = 1; i < 13; i++)
            {
                MyObject obj = new MyObject();
                obj.Value = i;
                _listthang.Add(obj);
            }
            cbNam.DisplayMember = "Value";
            cbNam.ValueMember = "Value";
            cbNam.DataSource = _list;
            cbNam.SelectedValue = namHT;

            cboThang.DisplayMember = "Value";
            cboThang.ValueMember = "Value";
            cboThang.DataSource = _listthang;
            cboThang.SelectedValue = DateTime.Now.Month - 1;
            _lKPhong = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).OrderBy(p => p.TenKP).ToList();
            _lKPhong.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả", PLoai = "Tất cả" });
            _lKPhong.Insert(1, new KPhong { MaKP = 1000, TenKP = "Ngoại trú" });
            _lKPhong.Insert(2, new KPhong { MaKP = 1001, TenKP = "Khám sức khỏe" });

            cklKhoaPhong.DataSource = _lKPhong;
            cklKhoaPhong.CheckAll();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnOK_Click(object sender, EventArgs e)
        {
            int _TrongDM = rgCHon.SelectedIndex;
            //tháng tìm kiếm
            DateTime tungayT = new DateTime(Convert.ToInt32(cbNam.SelectedValue), Convert.ToInt32(cboThang.SelectedValue), 1);
            DateTime denngayT = DungChung.Ham.NgayDen(tungayT.AddMonths(1).AddDays(-1));
            //Các tháng trước
            DateTime _tungayN = new DateTime(Convert.ToInt32(cbNam.SelectedValue), 1, 1);
            DateTime _denngayN = DungChung.Ham.NgayDen(tungayT.AddDays(-1));

            int Dtuong = rdBaoHiem.SelectedIndex;
            int NgayTK = rdNgay.SelectedIndex;//0:ngày thu,1:ngày duyệt
            int _LoaiBC = radioGroup1.SelectedIndex;//0:dịch vụ,1:thuốc
            List<int> _lkp = new List<int>();
            for (int i = 0; i < cklKhoaPhong.ItemCount; i++)
            {
                if (cklKhoaPhong.GetItemChecked(i))
                    _lkp.Add(Convert.ToInt32(cklKhoaPhong.GetItemValue(i)));
            }

            var _lkpnew = (from kp in _lKPhong
                           join k in _lkp on kp.MaKP equals k
                           select new { kp.MaKP, kp.TenKP, kp.PLoai }).Distinct().ToList();

            List<KQXHH> _listKetQua = new List<KQXHH>();
            var dv_tn = (from a in data.DichVus
                         join b in data.TieuNhomDVs on a.IdTieuNhom equals b.IdTieuNhom
                         join c in data.NhomDVs on b.IDNhom equals c.IDNhom
                         select new
                         {
                             a.MaDV,
                             b.TenRG,
                             c.TenNhom,
                             c.TenNhomCT,
                             a.DongY,
                             c.IDNhom,
                             a.Loai,
                             a.PLoai
                         }).ToList();
            if (NgayTK == 0)//Ngày thu
            {
                var q_tong = (from vp in data.TamUngs.Where(p => p.NgayThu >= _tungayN && p.NgayThu <= denngayT)
                              join vpct in data.TamUngcts.Where(p => p.Status == 0).Where(p => _TrongDM == 3 ? true : (_TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : p.TrongBH > 1))) on vp.IDTamUng equals vpct.IDTamUng
                              join bn in data.BenhNhans.Where(p => Dtuong == 3 ? true : (Dtuong == 0 ? p.DTuong == "BHYT" : (Dtuong == 1 ? p.DTuong == "Dịch vụ" : p.DTuong == "KSK"))) on vp.MaBNhan equals bn.MaBNhan
                              select new
                              {
                                  vpct.MaDV,
                                  vp.MaKP,
                                  vpct.SoTien,
                                  vpct.TrongBH,
                                  vpct.IDTamUngct,
                                  vp.MaBNhan,
                                  bn.DTuong,
                                  vp.PhanLoai,
                                  vp.NgayThu
                              }).ToList();
                if (_LoaiBC == 0)
                {
                    var _lsp = (from a in q_tong.Where(p => p.NgayThu >= tungayT && p.NgayThu <= denngayT).Where(p => p.PhanLoai == 3)
                                join kp in data.KPhongs on a.MaKP equals kp.MaKP
                                select new
                                {
                                    a.IDTamUngct,
                                    a.MaDV,
                                    a.MaBNhan,
                                    a.TrongBH,
                                    a.SoTien,
                                    MaKP = a.DTuong == "KSK" ? 1001 : ((kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) ? 1000 : a.MaKP)
                                    //TenKP = a.DTuong == "KSK" ? "Khám sức khỏe" : ((kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) ? "Ngoại trú" : kp.TenKP)
                                }).ToList();
                    var q1 = (from vp in _lsp
                              join dv in dv_tn on vp.MaDV equals dv.MaDV
                              join kp in _lkpnew on vp.MaKP equals kp.MaKP
                              group new { vp, dv, kp } by new { kp.MaKP, kp.TenKP } into kq
                              select new KQXHH
                              {
                                  MaKP = kq.Key.MaKP,
                                  TenKP = kq.Key.TenKP,
                                  XQ = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.vp.SoTien),
                                  Citi = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.SoTien),
                                  SAM = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Sum(p => p.vp.SoTien),
                                  DoLXUONG = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Sum(p => p.vp.SoTien),
                                  Tong = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.vp.SoTien) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.SoTien) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Sum(p => p.vp.SoTien) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Sum(p => p.vp.SoTien)
                              }).OrderBy(p => p.MaKP).ToList();
                    var q2 = (from a in q_tong.Where(p => p.NgayThu >= _tungayN && p.NgayThu <= _denngayN).Where(p => p.PhanLoai == 3)
                              join kp in data.KPhongs on a.MaKP equals kp.MaKP
                              select new
                              {
                                  a.MaDV,
                                  a.MaBNhan,
                                  a.TrongBH,
                                  a.SoTien,
                                  MaKP = a.DTuong == "KSK" ? 1001 : ((kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) ? 1000 : a.MaKP)
                              }).ToList();
                    var q3 = (from vp in q2
                              join dv in dv_tn on vp.MaDV equals dv.MaDV
                              join kp in _lkpnew on vp.MaKP equals kp.MaKP
                              group new { vp, dv, kp } by new { kp.MaKP, kp.TenKP } into kq
                              select new KQXHH
                              {
                                  MaKP = kq.Key.MaKP,
                                  TenKP = kq.Key.TenKP,
                                  XQ = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.vp.SoTien),
                                  Citi = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.SoTien),
                                  SAM = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Sum(p => p.vp.SoTien),
                                  DoLXUONG = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Sum(p => p.vp.SoTien),
                                  Tong = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.vp.SoTien) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.SoTien) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Sum(p => p.vp.SoTien) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Sum(p => p.vp.SoTien)
                              }).OrderBy(p => p.MaKP).ToList();
                    _listKetQua.AddRange(q1);
                    double XQ = 0, Citi = 0, SAm = 0, DoLXUONG = 0, Tong = 0;
                    double XQT = 0, CitiT = 0, SAmT = 0, DoLXUONGT = 0, TongT = 0;
                    double tONGtIEN = 0;
                    tONGtIEN = _listKetQua.Sum(P => P.Tong);
                    frmIn frm = new frmIn();
                    BaoCao.rep_BCThuDichVuTuBaoHiem_30003 rep = new BaoCao.rep_BCThuDichVuTuBaoHiem_30003();
                    rep.lab_tungaydenngay.Text = ("Tháng " + cboThang.Text + " năm " + cbNam.Text).ToUpper();
                    rep.celCongThangTitle.Text = "Cộng T" + cboThang.Text;
                    rep.celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
                    rep.celGD.Text = DungChung.Bien.GiamDoc;
                    string fomat = DungChung.Bien.FormatString[1];

                    XQ = q3.Sum(p => p.XQ); Citi = q3.Sum(p => p.Citi); SAm = q3.Sum(p => p.SAM); DoLXUONG = q3.Sum(p => p.DoLXUONG); Tong = q3.Sum(p => p.Tong);

                    rep.celCacThangTruoc_Title.Text = (Convert.ToInt32(cboThang.Text) > 1 ? (Convert.ToInt32(cboThang.Text) - 1) : 1) + " T MS";
                    rep.celCacThangTruocXQ.Text = String.Format(fomat, XQ);
                    rep.celCacThangTruocCiti.Text = String.Format(fomat, Citi);
                    rep.celCacThangTruocSA.Text = String.Format(fomat, SAm);
                    rep.celcacThangTruoc_LXuong.Text = String.Format(fomat, DoLXUONG);
                    rep.celCacThangTruoc_TongKhoa.Text = String.Format(fomat, Tong);

                    XQT = XQ + q1.Sum(p => p.XQ); CitiT = Citi + q1.Sum(p => p.Citi); SAmT = SAm + q1.Sum(p => p.SAM); DoLXUONGT = DoLXUONG + q1.Sum(p => p.DoLXUONG); TongT = Tong + q1.Sum(p => p.Tong);
                    rep.celXQ_thangT.Text = String.Format(fomat, XQT);
                    rep.celCiti_ThangT.Text = String.Format(fomat, CitiT);
                    rep.cel_SA_ThangT.Text = String.Format(fomat, SAmT);
                    rep.cel_LoangXuong_thangT.Text = String.Format(fomat, DoLXUONGT);
                    rep.celTongKhoa_ThangT.Text = String.Format(fomat, TongT);
                    rep.celTienBangChu.Text = DungChung.Ham.DocTienBangChu(tONGtIEN, " đồng");

                    var DemSL = (from dv in dv_tn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT)
                                 join t in _lsp on dv.MaDV equals t.MaDV
                                 select new { t.IDTamUngct, dv.Loai, dv.TenRG }).ToList();
                    if (DemSL.Count() > 0)
                    {
                        int XQ58 = 0, XQ83 = 0, XQ121 = 0, XQCT = 0;
                        XQ58 = DemSL.Where(p => p.Loai == 1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.IDTamUngct).Distinct().Count();
                        XQ83 = DemSL.Where(p => p.Loai == 2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.IDTamUngct).Distinct().Count();
                        XQ121 = DemSL.Where(p => p.Loai == 3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.IDTamUngct).Distinct().Count();
                        XQCT = DemSL.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Select(p => p.IDTamUngct).Distinct().Count();
                        rep.txtSoLuot.Text = "X-Quang58 = " + XQ58 + " ca;X-Quang83 = " + XQ83 + " ca;X-Quang121 = " + XQ121 + " ca;X-Quang Citi =" + XQCT + " ca;";
                    }
                    if (rdBaoHiem.SelectedIndex == 3)
                        rep.lab_Tieude.Text = "BÁO CÁO THU NGUỒN DỊCH VỤ - XHH";
                    else if (rdBaoHiem.SelectedIndex == 0)
                        rep.lab_Tieude.Text = "BÁO CÁO THU NGUỒN DỊCH VỤ - XHH TỪ BH";
                    else if (rdBaoHiem.SelectedIndex == 1)
                        rep.lab_Tieude.Text = "BÁO CÁO THU NGUỒN DỊCH VỤ - XHH TỪ VIỆN PHÍ";
                    else if (rdBaoHiem.SelectedIndex == 2)
                        rep.lab_Tieude.Text = "BÁO CÁO THU NGUỒN DỊCH VỤ - XHH TỪ KSK";
                    rep.DataSource = _listKetQua;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
            else
            {
                #region theo ngày duyệt
                #region bn BHYT, bn Dich vụ
                var _lvp = (from vp in data.VienPhis.Where(p => p.NgayDuyet >= _tungayN && p.NgayDuyet <= denngayT)
                            join vpct in data.VienPhicts.Where(P => P.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                            join rv in data.RaViens on vp.MaBNhan equals rv.MaBNhan
                            join bn in data.BenhNhans.Where(p => Dtuong == 3 ? true : (Dtuong == 0 ? p.DTuong == "BHYT" : (Dtuong == 1 ? p.DTuong == "Dịch vụ" : p.DTuong == "KSK"))) on rv.MaBNhan equals bn.MaBNhan
                            join tu in data.TamUngs.Where(p => p.PhanLoai == 1) on vp.MaBNhan equals tu.MaBNhan
                            select new
                            {
                                vpct.MaDV,
                                rv.MaKP,
                                vpct.TienBN,
                                bn.DTuong,
                                vpct.TrongBH,
                                vpct.idVPhict,
                                vp.NgayDuyet,
                                vp.MaBNhan,
                                vpct.Mien,
                                vpct.ThanhTien,
                                vpct.TyLeTT
                            }).ToList();
                var _lsp = (from a in _lvp
                            join kp in data.KPhongs on a.MaKP equals kp.MaKP
                            select new
                            {
                                a.MaDV,
                                a.MaBNhan,
                                a.TienBN,
                                a.idVPhict,
                                a.TrongBH,
                                a.NgayDuyet,
                                a.Mien,
                                MaKP = a.DTuong == "KSK" ? 1001 : ((kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) ? 1000 : a.MaKP)
                            }).ToList();

                var _lkqua = (from vp in _lsp.Where(p => p.NgayDuyet >= tungayT && p.NgayDuyet <= denngayT).Where(p => _TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : (_TrongDM == 2 ? (p.TrongBH != 0 && p.TrongBH != 1) : true)))
                              join dv in dv_tn on vp.MaDV equals dv.MaDV
                              join kp in _lkpnew on vp.MaKP equals kp.MaKP
                              group new { vp, dv, kp } by new { kp.MaKP, kp.TenKP } into kq
                              select new KQXHH
                              {
                                  TenKP = kq.Key.TenKP,
                                  MaKP = kq.Key.MaKP,
                                  XQ = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.vp.TienBN),
                                  Citi = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.TienBN),
                                  SAM = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Sum(p => p.vp.TienBN),
                                  DoLXUONG = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Sum(p => p.vp.TienBN),
                                  Tong = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.vp.TienBN) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.TienBN) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Sum(p => p.vp.TienBN) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Sum(p => p.vp.TienBN),

                                  soBNRV = kq.Select(p => p.vp.MaBNhan).Distinct().Count(),
                                  ThuocTayTieuHao = kq.Where(p => p.dv.DongY != 1).Where(p => p.dv.PLoai == 1).Sum(p => p.vp.TienBN),
                                  ThuocDongY = kq.Where(p => p.dv.DongY == 1).Where(p => p.dv.PLoai == 1).Sum(p => p.vp.TienBN),
                                  TongThuocVTYT = kq.Where(p => p.dv.PLoai == 1).Sum(p => p.vp.TienBN),
                                  ThuocVTMien = kq.Where(p => p.dv.PLoai == 1).Where(p => p.vp.Mien > 0).Sum(p => p.vp.TienBN * p.vp.Mien),
                                  ThuocVTKMien = kq.Where(p => p.dv.PLoai == 1).Where(p => p.vp.Mien <= 0).Sum(p => p.vp.TienBN),

                                  soBNMien = kq.Where(p => p.vp.Mien > 0).Where(p => p.dv.PLoai == 1).Select(p => p.vp.MaBNhan).Distinct().Count(),
                                  TongThuocVTKMien = kq.Where(p => p.dv.PLoai == 1).Where(p => p.dv.PLoai == 1).Where(p => p.vp.Mien > 0).Sum(p => p.vp.TienBN * p.vp.Mien)

                              }).ToList();
                var _lkthangtr = (from vp in _lsp.Where(p => p.NgayDuyet >= _tungayN && p.NgayDuyet <= _denngayN).Where(p => _TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : (_TrongDM == 2 ? (p.TrongBH != 0 && p.TrongBH != 1) : true)))
                              join dv in dv_tn on vp.MaDV equals dv.MaDV
                              join kp in _lkpnew on vp.MaKP equals kp.MaKP
                              group new { vp, dv, kp } by new { kp.MaKP, kp.TenKP } into kq
                              select new KQXHH
                              {
                                  XQ = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.vp.TienBN),
                                  Citi = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.TienBN),
                                  SAM = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Sum(p => p.vp.TienBN),
                                  DoLXUONG = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Sum(p => p.vp.TienBN),
                                  Tong = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.vp.TienBN) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.TienBN) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Sum(p => p.vp.TienBN) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Sum(p => p.vp.TienBN),

                                  soBNRV = kq.Select(p => p.vp.MaBNhan).Distinct().Count(),
                                  ThuocTayTieuHao = kq.Where(p => p.dv.DongY != 1).Where(p => p.dv.PLoai == 1).Sum(p => p.vp.TienBN),
                                  ThuocDongY = kq.Where(p => p.dv.DongY == 1).Where(p => p.dv.PLoai == 1).Sum(p => p.vp.TienBN),
                                  TongThuocVTYT = kq.Where(p => p.dv.PLoai == 1).Sum(p => p.vp.TienBN),
                                  ThuocVTMien = kq.Where(p => p.dv.PLoai == 1).Where(p => p.vp.Mien > 0).Sum(p => p.vp.TienBN * p.vp.Mien),
                                  ThuocVTKMien = kq.Where(p => p.dv.PLoai == 1).Where(p => p.vp.Mien <= 0).Sum(p => p.vp.TienBN),
                                  TongThuocVTKMien = kq.Where(p => p.dv.PLoai == 1).Sum(p => p.vp.TienBN),
                                  soBNMien = kq.Where(p => p.vp.Mien > 0).Select(p => p.vp.MaBNhan).Distinct().Count(),
                                  TongDonMien = kq.Where(p => p.dv.PLoai == 1).Where(p => p.vp.Mien > 0).Sum(p => p.vp.TienBN * p.vp.Mien)

                              }).ToList();

                if (_LoaiBC == 0)
                {

                    double XQ = 0, Citi = 0, SAm = 0,DoLXUONG=0, Tong = 0;
                    double XQT = 0, CitiT = 0, SAmT = 0,DoLXUONGT=0, TongT = 0;
                    double tONGtIEN = 0;
                    tONGtIEN = _lkqua.Sum(P => P.Tong);
                    frmIn frm = new frmIn();
                    BaoCao.rep_BCThuDichVuTuBaoHiem_30003 rep = new BaoCao.rep_BCThuDichVuTuBaoHiem_30003();
                    rep.lab_tungaydenngay.Text = ("Tháng " + cboThang.Text + " năm " + cbNam.Text).ToUpper();
                    rep.celCongThangTitle.Text = "Cộng T" + cboThang.Text;
                    rep.celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
                    rep.celGD.Text = DungChung.Bien.GiamDoc;
                    string fomat = DungChung.Bien.FormatString[1];

                    XQ = _lkthangtr.Sum(p => p.XQ); Citi = _lkthangtr.Sum(p => p.Citi); SAm = _lkthangtr.Sum(p => p.SAM); DoLXUONG = _lkthangtr.Sum(p => p.DoLXUONG); Tong = _lkthangtr.Sum(p => p.Tong);

                    rep.celCacThangTruoc_Title.Text = (Convert.ToInt32(cboThang.Text) > 1 ? (Convert.ToInt32(cboThang.Text) - 1) : 1) + " T MS";
                    rep.celCacThangTruocXQ.Text = String.Format(fomat, XQ);
                    rep.celCacThangTruocCiti.Text = String.Format(fomat, Citi);
                    rep.celCacThangTruocSA.Text = String.Format(fomat, SAm);
                    rep.celcacThangTruoc_LXuong.Text = String.Format(fomat, DoLXUONG);
                    rep.celCacThangTruoc_TongKhoa.Text = String.Format(fomat, Tong);

                    XQT = XQ + _lkqua.Sum(p => p.XQ); CitiT = Citi + _lkqua.Sum(p => p.Citi); SAmT = SAm + _lkqua.Sum(p => p.SAM); DoLXUONGT = DoLXUONG + _lkqua.Sum(p => p.DoLXUONG); TongT = Tong + _lkqua.Sum(p => p.Tong);
                    rep.celXQ_thangT.Text = String.Format(fomat, XQT);
                    rep.celCiti_ThangT.Text = String.Format(fomat, CitiT);
                    rep.cel_SA_ThangT.Text = String.Format(fomat, SAmT);
                    rep.cel_LoangXuong_thangT.Text = String.Format(fomat, DoLXUONG);
                    rep.celTongKhoa_ThangT.Text = String.Format(fomat, TongT);

                    var DemSL = (from dv in dv_tn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT)
                                 join t in _lsp on dv.MaDV equals t.MaDV
                                 select new { t.idVPhict, dv.Loai, dv.TenRG }).ToList();
                    if (DemSL.Count() > 0)
                    {
                        int XQ58 = 0, XQ83 = 0, XQ121 = 0, XQCT = 0;
                        XQ58 = DemSL.Where(p => p.Loai == 1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.idVPhict).Distinct().Count();
                        XQ83 = DemSL.Where(p => p.Loai == 2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.idVPhict).Distinct().Count();
                        XQ121 = DemSL.Where(p => p.Loai == 3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.idVPhict).Distinct().Count();
                        XQCT = DemSL.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Select(p => p.idVPhict).Distinct().Count();
                        rep.txtSoLuot.Text = "X-Quang58 = " + XQ58 + " ca;X-Quang83 = " + XQ83 + " ca;X-Quang121 = " + XQ121 + " ca;X-Quang Citi =" + XQCT + " ca;";
                    }
                    if (rdBaoHiem.SelectedIndex == 3)
                        rep.lab_Tieude.Text = "BÁO CÁO THU NGUỒN DỊCH VỤ - XHH";
                    else if (rdBaoHiem.SelectedIndex == 0)
                        rep.lab_Tieude.Text = "BÁO CÁO THU NGUỒN DỊCH VỤ - XHH TỪ BH";
                    else if (rdBaoHiem.SelectedIndex == 1)
                        rep.lab_Tieude.Text = "BÁO CÁO THU NGUỒN DỊCH VỤ - XHH TỪ VIỆN PHÍ";
                    else if (rdBaoHiem.SelectedIndex == 2)
                        rep.lab_Tieude.Text = "BÁO CÁO THU NGUỒN DỊCH VỤ - XHH TỪ KSK";

                    rep.celTienBangChu.Text = DungChung.Ham.DocTienBangChu(tONGtIEN, " đồng");
                    rep.DataSource = _lkqua;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    double soBNRV = 0, ThuocTayTieuHao = 0, ThuocDongY = 0, TongThuocVTYT = 0, ThuocVTMien = 0, ThuocVTKMien = 0, TongThuocVTKMien = 0, soBNMien = 0, TongDonMien = 0;
                    double TsoBNRV = 0, TThuocTayTieuHao = 0, TThuocDongY = 0, TTongThuocVTYT = 0, TThuocVTMien = 0, TThuocVTKMien = 0, TsoBNMien = 0, TTongThuocVTKMien = 0, TTongDonMien = 0;
                    frmIn frm = new frmIn();
                    BaoCao.rep_BCThuocVTYT_30003 rep = new BaoCao.rep_BCThuocVTYT_30003();
                    rep.celNgaythangnam.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;

                    if (rdBaoHiem.SelectedIndex == 0)
                        rep.lab_Tieude.Text = "BÁO CÁO RA VIỆN THÁNG " + cboThang.Text + " - NĂM " + cbNam.Text + " CỦA BN CÓ THẺ BHYT";
                    else if (rdBaoHiem.SelectedIndex == 1)
                        rep.lab_Tieude.Text = "BÁO CÁO RA VIỆN THÁNG " + cboThang.Text + " - NĂM " + cbNam.Text + " CỦA BN NGOÀI BẢO HIỂM";
                    else if (rdBaoHiem.SelectedIndex == 1)
                        rep.lab_Tieude.Text = "BÁO CÁO RA VIỆN THÁNG " + cboThang.Text + " - NĂM " + cbNam.Text + " CỦA BN KSK";
                    else
                        rep.lab_Tieude.Text = "BÁO CÁO RA VIỆN THÁNG " + cboThang.Text + " - NĂM " + cbNam.Text;


                    rep.celCongThangTitle.Text = "Cộng T" + cboThang.Text;
                    rep.celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
                    rep.celGD.Text = DungChung.Bien.GiamDoc;
                    string fomat = DungChung.Bien.FormatString[1];
                    soBNRV = _lkthangtr.Sum(p => p.soBNRV); ThuocTayTieuHao = _lkthangtr.Sum(p => p.ThuocTayTieuHao); ThuocDongY = _lkthangtr.Sum(p => p.ThuocDongY); TongThuocVTYT = _lkthangtr.Sum(p => p.TongThuocVTYT);
                    ThuocVTMien = _lkthangtr.Sum(p => p.ThuocVTMien); ThuocVTKMien = _lkthangtr.Sum(p => p.ThuocVTKMien); TongThuocVTKMien = _lkthangtr.Sum(p => p.TongThuocVTKMien); soBNMien = _lkthangtr.Sum(p => p.soBNMien); TongDonMien = _lkthangtr.Sum(p => p.TongDonMien);
                    rep.celCacThangTruoc_Title.Text = (Convert.ToInt32(cboThang.Text) > 1 ? (Convert.ToInt32(cboThang.Text) - 1) : 1) + " T MS";
                    rep.cel1tr.Text = String.Format(DungChung.Bien.FormatString[0], soBNRV);
                    rep.cel2tr.Text = String.Format(fomat, ThuocTayTieuHao);
                    rep.cel3tr.Text = String.Format(fomat, ThuocDongY);
                    rep.cel4tr.Text = String.Format(fomat, TongThuocVTYT);
                    rep.cel5tr.Text = String.Format(fomat, ThuocVTMien);
                    rep.cel6tr.Text = String.Format(fomat, ThuocVTKMien);
                    rep.cel7tr.Text = String.Format(fomat, TongThuocVTKMien);
                    rep.cel8tr.Text = String.Format(DungChung.Bien.FormatString[0], soBNMien);
                    rep.cel9tr.Text = String.Format(fomat, TongDonMien);


                    TsoBNRV = _lkqua.Sum(p => p.soBNRV) + soBNRV; TThuocTayTieuHao = _lkqua.Sum(p => p.ThuocTayTieuHao) + ThuocTayTieuHao; TThuocDongY = _lkqua.Sum(p => p.ThuocDongY) + ThuocDongY; TTongThuocVTYT = _lkqua.Sum(p => p.TongThuocVTYT) + TongThuocVTYT;
                    TThuocVTMien = _lkqua.Sum(p => p.ThuocVTMien) + ThuocVTMien; TThuocVTKMien = _lkqua.Sum(p => p.ThuocVTKMien) + ThuocVTKMien; TTongThuocVTKMien = _lkqua.Sum(p => p.TongThuocVTKMien) + TongThuocVTKMien; TsoBNMien = _lkqua.Sum(p => p.soBNMien) + soBNMien; TTongDonMien = _lkqua.Sum(p => p.TongDonMien) + TongDonMien;
                    rep.celTong1.Text = String.Format(DungChung.Bien.FormatString[0], TsoBNRV);
                    rep.celTong2.Text = String.Format(fomat, TThuocTayTieuHao);
                    rep.celTong3.Text = String.Format(fomat, TThuocDongY);
                    rep.celTong4.Text = String.Format(fomat, TTongThuocVTYT);
                    rep.celTong5.Text = String.Format(fomat, TThuocVTMien);
                    rep.celTong6.Text = String.Format(fomat, TThuocVTKMien);
                    rep.celTong7.Text = String.Format(fomat, TTongThuocVTYT);
                    rep.celTong8.Text = String.Format(DungChung.Bien.FormatString[0], TsoBNMien);
                    rep.celTong9.Text = String.Format(fomat, TTongDonMien);



                    rep.DataSource = _lkqua;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                }
                 
                #endregion

                #endregion
            }
            #region bỏ
            //string fomat = "{0:N0}";
            //int _TrongDM = rgCHon.SelectedIndex;
            ////tháng tìm kiếm
            //DateTime tungayT = new DateTime(Convert.ToInt32(cbNam.SelectedValue), Convert.ToInt32(cboThang.SelectedValue), 1);
            //DateTime denngayT = DungChung.Ham.NgayDen(tungayT.AddMonths(1).AddDays(-1));
            ////Các tháng trước
            //DateTime _tungay = new DateTime(Convert.ToInt32(cbNam.SelectedValue), 1, 1);
            //DateTime _denngay = DungChung.Ham.NgayDen(tungayT.AddDays(-1));
            //var dv_tn = (from a in data.DichVus
            //             join b in data.TieuNhomDVs on a.IdTieuNhom equals b.IdTieuNhom
            //             join c in data.NhomDVs on b.IDNhom equals c.IDNhom
            //             select new
            //             {
            //                 a.MaDV,
            //                 b.TenRG,
            //                 c.TenNhom,
            //                 c.TenNhomCT,
            //                 a.DongY,
            //                 c.IDNhom,
            //                 a.Loai,
            //                 a.PLoai
            //             }).ToList();
            //if (rdNgay.SelectedIndex == 1)
            //{
            //    #region bn BHYT, bn Dich vu
            //    var _lvp = (from vp in data.VienPhis.Where(p => p.NgayDuyet >= _tungay && p.NgayDuyet <= denngayT)
            //                join vpct in data.VienPhicts.Where(P => P.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
            //                join rv in data.RaViens on vp.MaBNhan equals rv.MaBNhan
            //                join bn in data.BenhNhans.Where(p => rdBaoHiem.SelectedIndex == 0 ? p.DTuong == "BHYT" : (rdBaoHiem.SelectedIndex == 1 ? p.DTuong == "Dịch vụ" : p.DTuong == "KSK")) on rv.MaBNhan equals bn.MaBNhan
            //                join tu in data.TamUngs.Where(p => p.PhanLoai == 1) on vp.MaBNhan equals tu.MaBNhan
            //                select new
            //                {
            //                    vpct.MaDV,
            //                    rv.MaKP,
            //                    vpct.TienBN,
            //                    vpct.TrongBH,
            //                    vpct.idVPhict,
            //                    vp.MaBNhan,
            //                    vpct.Mien,
            //                    vpct.ThanhTien,
            //                    vpct.TienBH,
            //                    vp.NgayDuyet
            //                }).ToList();
            //    var _lsp = (from a in _lvp
            //                join kp in data.KPhongs on a.MaKP equals kp.MaKP
            //                select new
            //                {
            //                    a.MaDV,
            //                    a.MaBNhan,
            //                    a.TienBN,
            //                    a.idVPhict,
            //                    a.TrongBH,
            //                    a.ThanhTien,
            //                    a.TienBH,
            //                    a.Mien,
            //                    MaKP = (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) ? 1000 : a.MaKP,
            //                    TenKP = (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) ? "Ngoại trú" : kp.TenKP,
            //                    a.NgayDuyet
            //                }).ToList();

            //    var _lkqua = (from vp in _lsp.Where(p => p.NgayDuyet >= tungayT && p.NgayDuyet <= denngayT).Where(p => _TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : (_TrongDM == 2 ? (p.TrongBH != 0 && p.TrongBH != 1) : true)))
            //                  join dv in dv_tn on vp.MaDV equals dv.MaDV
            //                  group new { vp, dv } by new { vp.MaKP, vp.TenKP } into kq
            //                  select new KQXHH
            //                  {
            //                      TenKP = kq.Key.TenKP,
            //                      MaKP = kq.Key.MaKP,
            //                      XQ = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.vp.TienBN),
            //                      Citi = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.TienBN),
            //                      SAM = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.vp.TienBN),
            //                      Tong = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.vp.TienBN) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.TienBN) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.vp.TienBN),
            //                  }).ToList();
            //    var dem = (from vp in _lsp.Where(p => p.NgayDuyet >= tungayT && p.NgayDuyet <= denngayT).Where(p => _TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : (_TrongDM == 2 ? (p.TrongBH != 0 && p.TrongBH != 1) : true)))
            //               join dv in dv_tn on vp.MaDV equals dv.MaDV
            //               select new
            //               {
            //                   vp.MaDV,
            //                   dv.Loai,
            //                   dv.TenRG,
            //                   vp.MaBNhan
            //               }).ToList();
            //    List<KQXHH> ketqua = (from k in _lkqua
            //                          group new { k } by new { k.MaKP, k.TenKP } into kq
            //                          select new KQXHH
            //                          {
            //                              MaKP = kq.Key.MaKP,
            //                              TenKP = kq.Key.TenKP,
            //                              XQ = kq.Sum(p => p.k.XQ),
            //                              Citi = kq.Sum(p => p.k.Citi),
            //                              SAM = kq.Sum(p => p.k.SAM),
            //                              Tong = kq.Sum(p => p.k.Tong)
            //                          }).OrderBy(p => p.TenKP).ToList();
            //    #endregion
            //    #region BNKSK -- Lấy tổng chi phí của tất cả bệnh nhân có phân loại = 1 or 2 trong bảng tạm ứng
            //    List<int> lbnDaDuyet = (from bn in data.BenhNhans.Where(p => rdNgay.SelectedIndex == 2 && p.DTuong == "KSK")
            //                            join tu in data.TamUngs.Where(p => p.NgayThu >= tungayT && p.NgayThu <= denngayT).Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).Where(p => rgCHon.SelectedIndex == 1 || rgCHon.SelectedIndex == 3)
            //                            on bn.MaBNhan equals tu.MaBNhan
            //                            select bn.MaBNhan).ToList();

            //    //Lấy tất cả chi phí thu thẳng cho các dịch vụ ( có join với bảng tamungct-vì có bn có chi phí thu thẳng nhưng ko join với bảng tamungct) làm các chi chi phí theo dịch vụ + ngoài ra là tiền khám



            //    #region Lấy chi phí không phải thu thẳng theo dịch vụ (Không join với bảng tạm ứng chi tiết (bao gồm cả PhanLoai = 3)

            //    var qThanhToan = (from bn in data.BenhNhans.Where(p => _TrongDM == 0 ? false : true).Where(p => lbnDaDuyet.Contains(p.MaBNhan))
            //                      join tu in data.TamUngs.Where(p => p.PhanLoai != 3) on bn.MaBNhan equals tu.MaBNhan
            //                      select new { bn.MaBNhan, tu.PhanLoai, tu.SoTien, MaKP = 1001, TenKP = "Khám sức khỏe" }).ToList();
            //    var qThanhToan1 = (from vp in qThanhToan
            //                       group new { vp } by new { vp.MaBNhan, vp.MaKP, vp.TenKP } into kq
            //                       select new KQXHH
            //                       {
            //                           TenKP = kq.Key.TenKP,
            //                           MaKP = kq.Key.MaKP,
            //                           MaBN = kq.Key.MaBNhan,
            //                           KhamBenh = kq.Where(p => p.vp.PhanLoai == 0 || p.vp.PhanLoai == 1).Sum(p => p.vp.SoTien ?? 0) - kq.Where(p => p.vp.PhanLoai == 2).Sum(p => p.vp.SoTien ?? 0),
            //                           Tong = kq.Where(p => p.vp.PhanLoai == 0 || p.vp.PhanLoai == 1).Sum(p => p.vp.SoTien ?? 0) - kq.Where(p => p.vp.PhanLoai == 2).Sum(p => p.vp.SoTien ?? 0)
            //                       }).ToList();


            //    #endregion
            //    #region Lấy tổng CP của BN KSK

            //    List<KQXHH> qCPKSK = (from vp in qThanhToan1
            //                          group vp by new { vp.MaKP, vp.TenKP } into kq
            //                          select new KQXHH
            //                          {
            //                              TenKP = kq.Key.TenKP,
            //                              MaKP = kq.Key.MaKP,
            //                              XQ = kq.Sum(p => p.XQ),
            //                              Citi = kq.Sum(p => p.Citi),
            //                              SAM = kq.Sum(p => p.SAM),
            //                              Tong = 0
            //                          }).ToList();
            //    ketqua.AddRange(qCPKSK);

            //    #endregion
            //    #endregion
            //    frmIn frm = new frmIn();
            //    //các tháng trước
            //    double XQ = 0;
            //    double Citi = 0;
            //    double SAm = 0;
            //    double Tong = 0;
            //    if (Convert.ToInt32(cboThang.Text) > 1)
            //    {
            //        var qCongT_1 = _lsp.Where(p => p.NgayDuyet >= _tungay && p.NgayDuyet <= _denngay).Where(p => _TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : (_TrongDM == 2 ? (p.TrongBH != 0 && p.TrongBH != 1) : true))).ToList();
            //        List<KQXHH> qCongT_2 = (from a in qCongT_1
            //                                join dv in dv_tn on a.MaDV equals dv.MaDV
            //                                select new KQXHH
            //                                {
            //                                    MaKP = a.MaKP,
            //                                    TenKP = a.TenKP,
            //                                    XQ = (dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang) ? a.TienBN : 0,
            //                                    Citi = (dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT) ? a.TienBN : 0,
            //                                    SAM = (dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler) ? a.TienBN : 0,
            //                                }).ToList();
            //        XQ = qCongT_2.Sum(p => p.XQ);
            //        Citi = qCongT_2.Sum(p => p.Citi);
            //        SAm = qCongT_2.Sum(p => p.SAM);
            //        Tong = XQ + Citi + SAm;
            //    }
            //    double XQTong = ketqua.Sum(p => p.XQ) + XQ;
            //    double CitiTong = ketqua.Sum(p => p.Citi) + Citi;
            //    double SAmTong = ketqua.Sum(p => p.SAM) + SAm;
            //    double TongAll = XQTong + CitiTong + SAmTong;
            //    double st = ketqua.Sum(p => p.Tong);
            //    #region Báo cáo viện phí dịch vụ
            //    if (radioGroup1.SelectedIndex == 0)
            //    {
            //        if (rdBaoHiem.SelectedIndex == 0)
            //        {
            //            #region báo cáo rep_BCThuDichVuTuBaoHiem_30003
            //            BaoCao.rep_BCThuDichVuTuBaoHiem_30003 rep = new BaoCao.rep_BCThuDichVuTuBaoHiem_30003();
            //            rep.lab_tungaydenngay.Text = ("Tháng " + cboThang.Text + " năm " + cbNam.Text).ToUpper();
            //            rep.celCongThangTitle.Text = "Cộng T" + cboThang.Text;
            //            rep.celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            //            rep.celGD.Text = DungChung.Bien.GiamDoc;

            //            if (Convert.ToInt32(cboThang.Text) > 1)
            //            {
            //                #region tổng các tháng trước
            //                rep.celCacThangTruoc_Title.Text = (Convert.ToInt32(cboThang.Text) - 1) + " T MS";
            //                rep.celCacThangTruocXQ.Text = String.Format(fomat, XQ);
            //                rep.celCacThangTruocCiti.Text = String.Format(fomat, Citi);
            //                rep.celCacThangTruocSA.Text = String.Format(fomat, SAm);
            //                rep.celCacThangTruoc_TongKhoa.Text = String.Format(fomat, Tong);
            //                #endregion
            //            }
            //            #region tổng = tháng hiện tại + các tháng trước
            //            rep.celXQ_thangT.Text = String.Format(fomat, XQTong);
            //            rep.celCiti_ThangT.Text = String.Format(fomat, CitiTong);
            //            rep.cel_SA_ThangT.Text = String.Format(fomat, SAmTong);
            //            rep.celTongKhoa_ThangT.Text = String.Format(fomat, TongAll);
            //            rep.celTienBangChu.Text = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
            //            #endregion
            //            rep.DataSource = ketqua;
            //            rep.BindingData();
            //            rep.CreateDocument();
            //            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            //            frm.ShowDialog();
            //            #endregion
            //        }
            //        else if (rdBaoHiem.SelectedIndex == 1 || rdBaoHiem.SelectedIndex == 2)
            //        {
            //            #region báo cáo rep_BCThuDichVuTuVienPhi_30003
            //            BaoCao.rep_BCThuDichVuTuVienPhi_30003 rep = new BaoCao.rep_BCThuDichVuTuVienPhi_30003();
            //            rep.lab_tungaydenngay.Text = ("Tháng " + cboThang.Text + " năm " + cbNam.Text + "(VIỆN PHÍ)").ToUpper();
            //            rep.celCongThangTitle.Text = "Cộng T" + cboThang.Text;
            //            rep.celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            //            rep.celGD.Text = DungChung.Bien.GiamDoc;
            //            if (rdBaoHiem.SelectedIndex == 2)
            //                rep.lab_Tieude.Text = "BÁO CÁO THU NGUỒN DỊCH VỤ - XHH TỪ KSK";

            //            if (Convert.ToInt32(cboThang.Text) > 1)
            //            {
            //                #region tổng các tháng trước
            //                rep.celCacThangTruoc_Title.Text = (Convert.ToInt32(cboThang.Text) - 1) + " T MS";
            //                rep.celCacThangTruocXQ.Text = String.Format(fomat, XQ);
            //                rep.celCacThangTruocCiti.Text = String.Format(fomat, Citi);
            //                rep.celCacThangTruocSA.Text = String.Format(fomat, SAm);
            //                rep.celCacThangTruoc_TongKhoa.Text = String.Format(fomat, Tong);
            //                #endregion
            //            }
            //            #region tổng = tháng hiện tại + các tháng trước
            //            rep.celXQ_thangT.Text = String.Format(fomat, XQTong);
            //            rep.celCiti_ThangT.Text = String.Format(fomat, CitiTong);
            //            rep.cel_SA_ThangT.Text = String.Format(fomat, SAmTong);
            //            rep.celTongKhoa_ThangT.Text = String.Format(fomat, TongAll);
            //            rep.celTienBangChu.Text = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
            //            #endregion
            //            var qXquang = dem.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).ToList();
            //            int Loai1 = qXquang.Where(p => p.Loai == 1).Select(p => p.MaBNhan).Count();
            //            int Loai2 = qXquang.Where(p => p.Loai == 2).Select(p => p.MaBNhan).Count();
            //            int Loai3 = qXquang.Where(p => p.Loai == 3).Select(p => p.MaBNhan).Count();
            //            int Loai0 = dem.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Select(p => p.MaBNhan).Count();

            //            rep.celTrongDo.Text = " XQ 58 = " + Loai1 + " ca; XQ83 = " + Loai2 + " ca; XQ121 = " + Loai3 + " ca; XQciti = " + Loai0 + " ca";

            //            rep.DataSource = ketqua;
            //            rep.BindingData();
            //            rep.CreateDocument();
            //            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            //            frm.ShowDialog();
            //            #endregion
            //        }
            //    }
            //    #endregion
            //    #region  báo cáo viện phí thuốc
            //    else
            //    {
            //        // nội trú
            //        // var benhnhan = qT_1.Where(p => p.Mien > 0).Select(p => p.MaBN).Distinct().ToList();
            //        var _lkqua1 = (from vp in _lsp.Where(p => p.NgayDuyet >= tungayT && p.NgayDuyet <= denngayT).Where(p => _TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : (_TrongDM == 2 ? (p.TrongBH != 0 && p.TrongBH != 1) : true)))
            //                       join dv in dv_tn on vp.MaDV equals dv.MaDV
            //                       select new KQXHH
            //                       {
            //                           MaKP = vp.MaKP,
            //                           TenKP = "%" + vp.TenKP.ToUpper(),
            //                           soBN = vp.MaBNhan == null ? 0 : vp.MaBNhan.Value,
            //                           ThuocTayTieuHao = (dv.PLoai == 1 && dv.TenRG != "Thuốc đông y") ? (vp.ThanhTien - vp.TienBH) : 0,
            //                           ThuocDongY = (dv.PLoai == 1 && dv.TenRG == "Thuốc đông y") ? (vp.ThanhTien - vp.TienBH) : 0,
            //                           TongThuocVTYT = (dv.PLoai == 1) ? (vp.ThanhTien - vp.TienBH) : 0,
            //                           ThuocVTMien = (vp.Mien > 0 && dv.PLoai == 1) ? (vp.ThanhTien - vp.TienBH - vp.TienBN) : 0,
            //                           ThuocVTKMien = (dv.PLoai == 1) ? (vp.TienBN) : 0,
            //                           TongDonMien = vp.Mien > 0 ? (vp.ThanhTien - vp.TienBH - vp.TienBN) : 0,
            //                           Mien = vp.Mien
            //                       }).ToList();

            //        var ketqua1 = (from a in _lkqua1
            //                       group a by new { a.TenKP, a.MaKP } into kq
            //                       select new KQXHH
            //                       {
            //                           TenKP = kq.Key.TenKP,
            //                           soBN = kq.Where(p => p.soBN != 0).Select(p => p.soBN).Distinct().Count(),
            //                           soBNMien = kq.Where(p => p.soBN != 0).Where(p => p.Mien > 0).Select(p => p.soBN).Distinct().Count(),
            //                           ThuocTayTieuHao = kq.Sum(p => p.ThuocTayTieuHao),
            //                           ThuocDongY = kq.Sum(p => p.ThuocDongY),
            //                           TongThuocVTYT = kq.Sum(p => p.TongThuocVTYT),
            //                           ThuocVTMien = kq.Sum(p => p.ThuocVTMien),
            //                           ThuocVTKMien = kq.Sum(p => p.ThuocVTKMien),
            //                           TongDonMien = kq.Sum(p => p.TongDonMien)
            //                       }).Where(p => p.TongDonMien != 0).ToList();
            //        frmIn frm1 = new frmIn();
            //        KQXHH cacthangtruoc = new KQXHH();

            //        if (Convert.ToInt32(cboThang.Text) > 1)
            //        {

            //            List<KQXHH> qCongT_2 = (from vp in _lsp.Where(p => p.NgayDuyet >= _tungay && p.NgayDuyet <= _denngay).Where(p => _TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : (_TrongDM == 2 ? (p.TrongBH != 0 && p.TrongBH != 1) : true)))
            //                                    join dv in dv_tn on vp.MaDV equals dv.MaDV
            //                                    select new KQXHH
            //                                    {
            //                                        MaKP = vp.MaKP,
            //                                        TenKP = "%" + vp.TenKP.ToUpper(),
            //                                        soBN = vp.MaBNhan == null ? 0 : vp.MaBNhan.Value,
            //                                        ThuocTayTieuHao = (dv.PLoai == 1 && dv.TenRG != "Thuốc đông y") ? (vp.ThanhTien - vp.TienBH) : 0,
            //                                        ThuocDongY = (dv.PLoai == 1 && dv.TenRG == "Thuốc đông y") ? (vp.ThanhTien - vp.TienBH) : 0,
            //                                        TongThuocVTYT = (dv.PLoai == 1) ? (vp.ThanhTien - vp.TienBH) : 0,
            //                                        ThuocVTMien = (vp.Mien > 0 && dv.PLoai == 1) ? (vp.ThanhTien - vp.TienBH - vp.TienBN) : 0,
            //                                        ThuocVTKMien = (dv.PLoai == 1) ? (vp.TienBN) : 0,
            //                                        TongDonMien = vp.Mien > 0 ? (vp.ThanhTien - vp.TienBH - vp.TienBN) : 0,
            //                                        Mien = vp.Mien
            //                                    }).Where(p => p.TongDonMien != 0).ToList();

            //            cacthangtruoc.soBN = qCongT_2.Where(p => p.soBN != 0).Select(p => p.soBN).Distinct().Count();
            //            cacthangtruoc.soBNMien = qCongT_2.Where(p => p.soBN != 0).Where(p => p.Mien > 0).Select(p => p.soBN).Distinct().Count();
            //            cacthangtruoc.ThuocTayTieuHao = qCongT_2.Sum(p => p.ThuocTayTieuHao);
            //            cacthangtruoc.ThuocDongY = qCongT_2.Sum(p => p.ThuocDongY);
            //            cacthangtruoc.TongThuocVTYT = qCongT_2.Sum(p => p.TongThuocVTYT);
            //            cacthangtruoc.ThuocVTMien = qCongT_2.Sum(p => p.ThuocVTMien);
            //            cacthangtruoc.ThuocVTKMien = qCongT_2.Sum(p => p.ThuocVTKMien);
            //            cacthangtruoc.TongDonMien = qCongT_2.Sum(p => p.TongDonMien);
            //        }
            //        KQXHH tong = new KQXHH();

            //        tong.soBN = ketqua1.Sum(p => p.soBN) + cacthangtruoc.soBN;
            //        tong.soBNMien = ketqua1.Sum(p => p.soBNMien) + cacthangtruoc.soBNMien;
            //        tong.ThuocTayTieuHao = ketqua1.Sum(p => p.ThuocTayTieuHao) + cacthangtruoc.ThuocTayTieuHao;
            //        tong.ThuocDongY = ketqua1.Sum(p => p.ThuocDongY) + cacthangtruoc.ThuocDongY;
            //        tong.TongThuocVTYT = ketqua1.Sum(p => p.TongThuocVTYT) + cacthangtruoc.TongThuocVTYT;
            //        tong.ThuocVTMien = ketqua1.Sum(p => p.ThuocVTMien) + cacthangtruoc.ThuocVTMien;
            //        tong.ThuocVTKMien = ketqua1.Sum(p => p.ThuocVTKMien) + cacthangtruoc.ThuocVTKMien;
            //        tong.TongDonMien = ketqua1.Sum(p => p.TongDonMien) + cacthangtruoc.TongDonMien;


            //        #region báo cáo rep_BCThuDichVuTuBaoHiem_30003
            //        BaoCao.rep_BCThuocVTYT_30003 rep = new BaoCao.rep_BCThuocVTYT_30003();
            //        rep.celNgaythangnam.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;

            //        if (rdBaoHiem.SelectedIndex == 0)
            //            rep.lab_Tieude.Text = "BÁO CÁO RA VIỆN THÁNG " + cboThang.Text + " - NĂM " + cbNam.Text + " CỦA BN CÓ THẺ BHYT";
            //        else if (rdBaoHiem.SelectedIndex == 1)
            //            rep.lab_Tieude.Text = "BÁO CÁO RA VIỆN THÁNG " + cboThang.Text + " - NĂM " + cbNam.Text + " CỦA BN NGOÀI BẢO HIỂM";
            //        else
            //            rep.lab_Tieude.Text = "BÁO CÁO RA VIỆN THÁNG " + cboThang.Text + " - NĂM " + cbNam.Text + " CỦA BN KSK";


            //        rep.celCongThangTitle.Text = "Cộng T" + cboThang.Text;
            //        rep.celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            //        rep.celGD.Text = DungChung.Bien.GiamDoc;

            //        if (Convert.ToInt32(cboThang.Text) > 1)
            //        {
            //            #region tổng các tháng trước
            //            rep.celCacThangTruoc_Title.Text = (Convert.ToInt32(cboThang.Text) - 1) + " T MS";
            //            rep.cel1tr.Text = String.Format("{0:N0}", cacthangtruoc.soBN);
            //            rep.cel2tr.Text = String.Format(fomat, cacthangtruoc.ThuocTayTieuHao);
            //            rep.cel3tr.Text = String.Format(fomat, cacthangtruoc.ThuocDongY);
            //            rep.cel4tr.Text = String.Format(fomat, cacthangtruoc.TongThuocVTYT);
            //            rep.cel5tr.Text = String.Format(fomat, cacthangtruoc.ThuocVTMien);
            //            rep.cel6tr.Text = String.Format(fomat, cacthangtruoc.ThuocVTKMien);
            //            rep.cel7tr.Text = String.Format(fomat, cacthangtruoc.TongThuocVTYT);
            //            rep.cel8tr.Text = String.Format("{0:N0}", cacthangtruoc.soBNMien);
            //            rep.cel9tr.Text = String.Format(fomat, cacthangtruoc.TongDonMien);
            //            #endregion
            //        }
            //        #region tổng = tháng hiện tại + các tháng trước
            //        rep.celTong1.Text = String.Format("{0:N0}", tong.soBN);
            //        rep.celTong2.Text = String.Format(fomat, tong.ThuocTayTieuHao);
            //        rep.celTong3.Text = String.Format(fomat, tong.ThuocDongY);
            //        rep.celTong4.Text = String.Format(fomat, tong.TongThuocVTYT);
            //        rep.celTong5.Text = String.Format(fomat, tong.ThuocVTMien);
            //        rep.celTong6.Text = String.Format(fomat, tong.ThuocVTKMien);
            //        rep.celTong7.Text = String.Format(fomat, tong.TongThuocVTYT);
            //        rep.celTong8.Text = String.Format("{0:N0}", tong.soBNMien);
            //        rep.celTong9.Text = String.Format(fomat, tong.TongDonMien);


            //        #endregion
            //        rep.DataSource = ketqua1;
            //        rep.BindingData();
            //        rep.CreateDocument();
            //        frm.prcIN.PrintingSystem = rep.PrintingSystem;
            //        frm.ShowDialog();
            //        #endregion
            //    }

            //    #endregion
            //}
            //else
            //{
            //    var _lvp = (from vp in data.TamUngs.Where(p => p.NgayThu >= _tungay && p.NgayThu <= denngayT).Where(p => p.PhanLoai == 3)
            //                join vpct in data.TamUngcts.Where(p => p.Status == 0) on vp.IDTamUng equals vpct.IDTamUng
            //                join bn in data.BenhNhans.Where(p => rdBaoHiem.SelectedIndex == 0 ? p.DTuong == "BHYT" : (rdBaoHiem.SelectedIndex == 1 ? p.DTuong == "Dịch vụ" : p.DTuong == "KSK")) on vp.MaBNhan equals bn.MaBNhan
            //                select new
            //                {
            //                    vpct.MaDV,
            //                    vp.MaKP,
            //                    TienBN = vpct.SoTien,
            //                    vpct.TrongBH,
            //                    vpct.IDTamUngct,
            //                    vp.MaBNhan,
            //                    vpct.Mien,
            //                    vpct.ThanhTien,
            //                    TienBH = 0,
            //                    vp.NgayThu
            //                }).ToList();
            //    var _lsp = (from a in _lvp
            //                join kp in data.KPhongs on a.MaKP equals kp.MaKP
            //                select new
            //                {
            //                    a.MaDV,
            //                    a.MaBNhan,
            //                    a.TienBN,
            //                    a.IDTamUngct,
            //                    a.TrongBH,
            //                    a.ThanhTien,
            //                    a.TienBH,
            //                    a.Mien,
            //                    MaKP = (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) ? 1000 : a.MaKP,
            //                    TenKP = (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) ? "Ngoại trú" : kp.TenKP,
            //                    a.NgayThu
            //                }).ToList();
            //    //if (rgCHon.SelectedIndex == 0)
            //    //{
            //    var _lkqua = (from vp in _lsp.Where(p => p.NgayThu >= tungayT && p.NgayThu <= denngayT).Where(p => _TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : (_TrongDM == 2 ? (p.TrongBH != 0 && p.TrongBH != 1) : true)))
            //                  join dv in dv_tn on vp.MaDV equals dv.MaDV
            //                  group new { vp, dv } by new { vp.MaKP, vp.TenKP } into kq
            //                  select new KQXHH
            //                  {
            //                      TenKP = kq.Key.TenKP,
            //                      MaKP = kq.Key.MaKP,
            //                      XQ = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.vp.TienBN),
            //                      Citi = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.TienBN),
            //                      SAM = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.vp.TienBN),
            //                      Tong = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.vp.TienBN) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.vp.TienBN) + kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.vp.TienBN),
            //                  }).ToList();
            //    var ketqua = (from k in _lkqua
            //                  group new { k } by new { k.MaKP, k.TenKP } into kq
            //                  select new KQXHH
            //                  {
            //                      MaKP = kq.Key.MaKP,
            //                      TenKP = kq.Key.TenKP,
            //                      XQ = kq.Sum(p => p.k.XQ),
            //                      Citi = kq.Sum(p => p.k.Citi),
            //                      SAM = kq.Sum(p => p.k.SAM),
            //                      Tong = kq.Sum(p => p.k.Tong)
            //                  }).OrderBy(p => p.TenKP).ToList();
            //    var dem = (from vp in _lsp.Where(p => p.NgayThu >= tungayT && p.NgayThu <= denngayT).Where(p => _TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : (_TrongDM == 2 ? (p.TrongBH != 0 && p.TrongBH != 1) : true)))
            //               join dv in dv_tn on vp.MaDV equals dv.MaDV
            //               select new
            //               {
            //                   vp.MaDV,
            //                   dv.Loai,
            //                   dv.TenRG,
            //                   vp.MaBNhan
            //               }).ToList();
            //    #region lấy tất cả chi phí (phân loại = 0,1,2,3)-- đang bỏ vì làm cho 30003




            //    #region BNKSK -- Lấy tổng chi phí của tất cả bệnh nhân KSK theo ngày thu (chỉ lấy chi phí thu thảng dịch vụ
            //    List<int> lbnDaDuyet = (from bn in data.BenhNhans.Where(p => rdNgay.SelectedIndex == 2 && p.DTuong == "KSK")
            //                            join tu in data.TamUngs.Where(p => p.NgayThu >= tungayT && p.NgayThu <= denngayT).Where(p => rgCHon.SelectedIndex == 1 || rgCHon.SelectedIndex == 3)
            //                            on bn.MaBNhan equals tu.MaBNhan
            //                            select bn.MaBNhan).ToList();

            //    //Lấy tất cả chi phí thu thẳng cho các dịch vụ ( có join với bảng tamungct-vì có bn có chi phí thu thẳng nhưng ko join với bảng tamungct) làm các chi chi phí theo dịch vụ + ngoài ra là tiền khám

            //    #region Lấy chi phí thu thẳng theo dịch vụ (có join với bảng tạm ứng chi tiết)
            //    var qCPThuThang = (from bn in data.BenhNhans.Where(p => _TrongDM == 0 ? false : true).Where(p => lbnDaDuyet.Contains(p.MaBNhan))
            //                       join tu in data.TamUngs.Where(p => p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan
            //                       join tuct in data.TamUngcts.Where(p => p.Status == 0) on tu.IDTamUng equals tuct.IDTamUng
            //                       select new { bn.MaBNhan, tu.PhanLoai, tuct.MaDV, ThanhTien = tuct.SoTien, MaKP = 1001, TenKP = "Khám sức khỏe" }).ToList();
            //    var qCPThuThang1 = (from vp in qCPThuThang
            //                        join dv in dv_tn on vp.MaDV equals dv.MaDV
            //                        group new { vp, dv } by new { vp.MaBNhan, vp.MaKP, vp.TenKP } into kq
            //                        select new KQXHH
            //                        {
            //                            TenKP = kq.Key.TenKP,
            //                            MaKP = kq.Key.MaKP,
            //                            MaBN = kq.Key.MaBNhan,
            //                            KhamBenh = kq.Where(p => p.vp.PhanLoai == 0 || p.vp.PhanLoai == 1).Sum(p => p.vp.ThanhTien) - kq.Where(p => p.vp.PhanLoai == 2).Sum(p => p.vp.ThanhTien),
            //                            Tong = kq.Where(p => p.vp.PhanLoai == 0 || p.vp.PhanLoai == 1).Sum(p => p.vp.ThanhTien) - kq.Where(p => p.vp.PhanLoai == 2).Sum(p => p.vp.ThanhTien)
            //                        }).ToList();
            //    #endregion


            //    #region Lấy tổng CP của BN KSK
            //    List<KQXHH> qCPKSK = (from vp in qCPThuThang1
            //                          group vp by new { vp.MaKP, vp.TenKP } into kq
            //                          select new KQXHH
            //                          {
            //                              TenKP = kq.Key.TenKP,
            //                              MaKP = kq.Key.MaKP,
            //                              XQ = kq.Sum(p => p.XQ),
            //                              Citi = kq.Sum(p => p.Citi),
            //                              SAM = kq.Sum(p => p.SAM),
            //                              Tong = kq.Sum(p => p.Tong)
            //                          }).ToList();
            //    ketqua.AddRange(qCPKSK);

            //    #endregion
            //    #endregion
            //    #endregion

            //    frmIn frm = new frmIn();
            //    //các tháng trước
            //    double XQ = 0;
            //    double Citi = 0;
            //    double SAm = 0;
            //    double Tong = 0;
            //    if (Convert.ToInt32(cboThang.Text) > 1)
            //    {
            //        var qCongT_1 = _lsp.Where(p => p.NgayThu >= _tungay && p.NgayThu <= _denngay).Where(p => _TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : (_TrongDM == 2 ? (p.TrongBH != 0 && p.TrongBH != 1) : true))).ToList();
            //        List<KQXHH> qCongT_2 = (from a in qCongT_1
            //                                join dv in dv_tn on a.MaDV equals dv.MaDV
            //                                select new KQXHH
            //                                {
            //                                    MaKP = a.MaKP,
            //                                    TenKP = a.TenKP,
            //                                    XQ = (dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang) ? a.TienBN : 0,
            //                                    Citi = (dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT) ? a.TienBN : 0,
            //                                    SAM = (dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler) ? a.TienBN : 0,
            //                                }).ToList();
            //        XQ = qCongT_2.Sum(p => p.XQ);
            //        Citi = qCongT_2.Sum(p => p.Citi);
            //        SAm = qCongT_2.Sum(p => p.SAM);
            //        Tong = XQ + Citi + SAm;
            //    }
            //    double XQTong = ketqua.Sum(p => p.XQ) + XQ;
            //    double CitiTong = ketqua.Sum(p => p.Citi) + Citi;
            //    double SAmTong = ketqua.Sum(p => p.SAM) + SAm;
            //    double TongAll = XQTong + CitiTong + SAmTong + Tong;
            //    double st = ketqua.Sum(p => p.Tong);
            //    #region Báo cáo viện phí dịch vụ
            //    if (radioGroup1.SelectedIndex == 0)
            //    {
            //        if (rdBaoHiem.SelectedIndex == 0)
            //        {
            //            #region báo cáo rep_BCThuDichVuTuBaoHiem_30003
            //            BaoCao.rep_BCThuDichVuTuBaoHiem_30003 rep = new BaoCao.rep_BCThuDichVuTuBaoHiem_30003();
            //            rep.lab_tungaydenngay.Text = ("Tháng " + cboThang.Text + " năm " + cbNam.Text).ToUpper();
            //            rep.celCongThangTitle.Text = "Cộng T" + cboThang.Text;
            //            rep.celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            //            rep.celGD.Text = DungChung.Bien.GiamDoc;

            //            if (Convert.ToInt32(cboThang.Text) > 1)
            //            {
            //                #region tổng các tháng trước
            //                rep.celCacThangTruoc_Title.Text = (Convert.ToInt32(cboThang.Text) - 1) + " T MS";
            //                rep.celCacThangTruocXQ.Text = String.Format(fomat, XQ);
            //                rep.celCacThangTruocCiti.Text = String.Format(fomat, Citi);
            //                rep.celCacThangTruocSA.Text = String.Format(fomat, SAm);
            //                rep.celCacThangTruoc_TongKhoa.Text = String.Format(fomat, Tong);
            //                #endregion
            //            }
            //            #region tổng = tháng hiện tại + các tháng trước
            //            rep.celXQ_thangT.Text = String.Format(fomat, XQTong);
            //            rep.celCiti_ThangT.Text = String.Format(fomat, CitiTong);
            //            rep.cel_SA_ThangT.Text = String.Format(fomat, SAmTong);
            //            rep.celTongKhoa_ThangT.Text = String.Format(fomat, TongAll);
            //            rep.celTienBangChu.Text = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
            //            #endregion
            //            rep.DataSource = ketqua;
            //            rep.BindingData();
            //            rep.CreateDocument();
            //            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            //            frm.ShowDialog();
            //            #endregion
            //        }
            //        else if (rdBaoHiem.SelectedIndex == 1 || rdBaoHiem.SelectedIndex == 2)
            //        {
            //            #region báo cáo rep_BCThuDichVuTuVienPhi_30003
            //            BaoCao.rep_BCThuDichVuTuVienPhi_30003 rep = new BaoCao.rep_BCThuDichVuTuVienPhi_30003();
            //            rep.lab_tungaydenngay.Text = ("Tháng " + cboThang.Text + " năm " + cbNam.Text + "(VIỆN PHÍ)").ToUpper();
            //            rep.celCongThangTitle.Text = "Cộng T" + cboThang.Text;
            //            rep.celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            //            rep.celGD.Text = DungChung.Bien.GiamDoc;
            //            if (rdBaoHiem.SelectedIndex == 2)
            //                rep.lab_Tieude.Text = "BÁO CÁO THU NGUỒN DỊCH VỤ - XHH TỪ KSK";

            //            if (Convert.ToInt32(cboThang.Text) > 1)
            //            {
            //                #region tổng các tháng trước
            //                rep.celCacThangTruoc_Title.Text = (Convert.ToInt32(cboThang.Text) - 1) + " T MS";
            //                rep.celCacThangTruocXQ.Text = String.Format(fomat, XQ);
            //                rep.celCacThangTruocCiti.Text = String.Format(fomat, Citi);
            //                rep.celCacThangTruocSA.Text = String.Format(fomat, SAm);
            //                rep.celCacThangTruoc_TongKhoa.Text = String.Format(fomat, Tong);
            //                #endregion
            //            }
            //            #region tổng = tháng hiện tại + các tháng trước
            //            rep.celXQ_thangT.Text = String.Format(fomat, XQTong);
            //            rep.celCiti_ThangT.Text = String.Format(fomat, CitiTong);
            //            rep.cel_SA_ThangT.Text = String.Format(fomat, SAmTong);
            //            rep.celTongKhoa_ThangT.Text = String.Format(fomat, TongAll);
            //            rep.celTienBangChu.Text = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
            //            #endregion
            //            var qXquang = dem.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).ToList();
            //            int Loai1 = qXquang.Where(p => p.Loai == 1).Select(p => p.MaBNhan).Count();
            //            int Loai2 = qXquang.Where(p => p.Loai == 2).Select(p => p.MaBNhan).Count();
            //            int Loai3 = qXquang.Where(p => p.Loai == 3).Select(p => p.MaBNhan).Count();
            //            int Loai0 = dem.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Select(p => p.MaBNhan).Count();

            //            rep.celTrongDo.Text = " XQ 58 = " + Loai1 + " ca; XQ83 = " + Loai2 + " ca; XQ121 = " + Loai3 + " ca; XQciti = " + Loai0 + " ca";

            //            rep.DataSource = ketqua;
            //            rep.BindingData();
            //            rep.CreateDocument();
            //            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            //            frm.ShowDialog();
            //            #endregion
            //        }
            //    }
            //    #endregion
            //    #region  báo cáo viện phí thuốc
            //    else
            //    {
            //        // nội trú
            //        // var benhnhan = qT_1.Where(p => p.Mien > 0).Select(p => p.MaBN).Distinct().ToList();
            //        var _lkqua1 = (from vp in _lsp.Where(p => p.NgayThu >= tungayT && p.NgayThu <= denngayT).Where(p => _TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : (_TrongDM == 2 ? (p.TrongBH != 0 && p.TrongBH != 1) : true)))
            //                       join dv in dv_tn on vp.MaDV equals dv.MaDV
            //                       select new KQXHH
            //                       {
            //                           MaKP = vp.MaKP,
            //                           TenKP = "%" + vp.TenKP.ToUpper(),
            //                           soBN = vp.MaBNhan == null ? 0 : vp.MaBNhan.Value,
            //                           ThuocTayTieuHao = (dv.PLoai == 1 && dv.TenRG != "Thuốc đông y") ? (vp.ThanhTien - vp.TienBH) : 0,
            //                           ThuocDongY = (dv.PLoai == 1 && dv.TenRG == "Thuốc đông y") ? (vp.ThanhTien - vp.TienBH) : 0,
            //                           TongThuocVTYT = (dv.PLoai == 1) ? (vp.ThanhTien - vp.TienBH) : 0,
            //                           ThuocVTMien = (vp.Mien > 0 && dv.PLoai == 1) ? (vp.ThanhTien - vp.TienBH - vp.TienBN) : 0,
            //                           ThuocVTKMien = (dv.PLoai == 1) ? (vp.TienBN) : 0,
            //                           TongDonMien = vp.Mien > 0 ? (vp.ThanhTien - vp.TienBH - vp.TienBN) : 0,
            //                           Mien = vp.Mien
            //                       }).ToList();

            //        var ketqua1 = (from a in _lkqua1
            //                       group a by new { a.TenKP, a.MaKP } into kq
            //                       select new
            //                       {
            //                           TenKP = kq.Key.TenKP,
            //                           SoBN = kq.Where(p => p.soBN != 0).Select(p => p.soBN).Distinct().Count(),
            //                           SoBNMien = kq.Where(p => p.soBN != 0).Where(p => p.Mien > 0).Select(p => p.soBN).Distinct().Count(),
            //                           ThuocTayTieuHao = kq.Sum(p => p.ThuocTayTieuHao),
            //                           ThuocDongY = kq.Sum(p => p.ThuocDongY),
            //                           TongThuocVTYT = kq.Sum(p => p.TongThuocVTYT),
            //                           ThuocVTMien = kq.Sum(p => p.ThuocVTMien),
            //                           ThuocVTKMien = kq.Sum(p => p.ThuocVTKMien),
            //                           TongDonMien = kq.Sum(p => p.TongDonMien)
            //                       }).Where(p => p.TongDonMien != 0).ToList();
            //        frmIn frm1 = new frmIn();
            //        KQXHH cacthangtruoc = new KQXHH();

            //        if (Convert.ToInt32(cboThang.Text) > 1)
            //        {

            //            List<KQXHH> qCongT_2 = (from vp in _lsp.Where(p => p.NgayThu >= _tungay && p.NgayThu <= _denngay).Where(p => _TrongDM == 0 ? p.TrongBH == 1 : (_TrongDM == 1 ? p.TrongBH == 0 : (_TrongDM == 2 ? (p.TrongBH != 0 && p.TrongBH != 1) : true)))
            //                                    join dv in dv_tn on vp.MaDV equals dv.MaDV
                                                
            //                                    select new KQXHH
            //                                    {
            //                                        MaKP = vp.MaKP,
            //                                        TenKP = "%" + vp.TenKP.ToUpper(),
            //                                        soBN = vp.MaBNhan == null ? 0 : vp.MaBNhan.Value,
            //                                        ThuocTayTieuHao = (dv.PLoai == 1 && dv.TenRG != "Thuốc đông y") ? (vp.ThanhTien - vp.TienBH) : 0,
            //                                        ThuocDongY = (dv.PLoai == 1 && dv.TenRG == "Thuốc đông y") ? (vp.ThanhTien - vp.TienBH) : 0,
            //                                        TongThuocVTYT = (dv.PLoai == 1) ? (vp.ThanhTien - vp.TienBH) : 0,
            //                                        ThuocVTMien = (vp.Mien > 0 && dv.PLoai == 1) ? (vp.ThanhTien - vp.TienBH - vp.TienBN) : 0,
            //                                        ThuocVTKMien = (dv.PLoai == 1) ? (vp.TienBN) : 0,
            //                                        TongDonMien = vp.Mien > 0 ? (vp.ThanhTien - vp.TienBH - vp.TienBN) : 0,
            //                                        Mien = vp.Mien
            //                                    }).Where(p => p.TongDonMien != 0).ToList();

            //            cacthangtruoc.soBN = qCongT_2.Where(p => p.soBN != 0).Select(p => p.soBN).Distinct().Count();
            //            cacthangtruoc.soBNMien = qCongT_2.Where(p => p.soBN != 0).Where(p => p.Mien > 0).Select(p => p.soBN).Distinct().Count();
            //            cacthangtruoc.ThuocTayTieuHao = qCongT_2.Sum(p => p.ThuocTayTieuHao);
            //            cacthangtruoc.ThuocDongY = qCongT_2.Sum(p => p.ThuocDongY);
            //            cacthangtruoc.TongThuocVTYT = qCongT_2.Sum(p => p.TongThuocVTYT);
            //            cacthangtruoc.ThuocVTMien = qCongT_2.Sum(p => p.ThuocVTMien);
            //            cacthangtruoc.ThuocVTKMien = qCongT_2.Sum(p => p.ThuocVTKMien);
            //            cacthangtruoc.TongDonMien = qCongT_2.Sum(p => p.TongDonMien);
            //        }
            //        KQXHH tong = new KQXHH();

            //        tong.soBN = ketqua1.Sum(p => p.SoBN) + cacthangtruoc.soBN;
            //        tong.soBNMien = ketqua1.Sum(p => p.SoBNMien) + cacthangtruoc.soBNMien;
            //        tong.ThuocTayTieuHao = ketqua1.Sum(p => p.ThuocTayTieuHao) + cacthangtruoc.ThuocTayTieuHao;
            //        tong.ThuocDongY = ketqua1.Sum(p => p.ThuocDongY) + cacthangtruoc.ThuocDongY;
            //        tong.TongThuocVTYT = ketqua1.Sum(p => p.TongThuocVTYT) + cacthangtruoc.TongThuocVTYT;
            //        tong.ThuocVTMien = ketqua1.Sum(p => p.ThuocVTMien) + cacthangtruoc.ThuocVTMien;
            //        tong.ThuocVTKMien = ketqua1.Sum(p => p.ThuocVTKMien) + cacthangtruoc.ThuocVTKMien;
            //        tong.TongDonMien = ketqua1.Sum(p => p.TongDonMien) + cacthangtruoc.TongDonMien;


            //        #region báo cáo rep_BCThuDichVuTuBaoHiem_30003
            //        BaoCao.rep_BCThuocVTYT_30003 rep = new BaoCao.rep_BCThuocVTYT_30003();
            //        rep.celNgaythangnam.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;

            //        if (rdBaoHiem.SelectedIndex == 0)
            //            rep.lab_Tieude.Text = "BÁO CÁO RA VIỆN THÁNG " + cboThang.Text + " - NĂM " + cbNam.Text + " CỦA BN CÓ THẺ BHYT";
            //        else if (rdBaoHiem.SelectedIndex == 1)
            //            rep.lab_Tieude.Text = "BÁO CÁO RA VIỆN THÁNG " + cboThang.Text + " - NĂM " + cbNam.Text + " CỦA BN NGOÀI BẢO HIỂM";
            //        else
            //            rep.lab_Tieude.Text = "BÁO CÁO RA VIỆN THÁNG " + cboThang.Text + " - NĂM " + cbNam.Text + " CỦA BN KSK";


            //        rep.celCongThangTitle.Text = "Cộng T" + cboThang.Text;
            //        rep.celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            //        rep.celGD.Text = DungChung.Bien.GiamDoc;

            //        if (Convert.ToInt32(cboThang.Text) > 1)
            //        {
            //            #region tổng các tháng trước
            //            rep.celCacThangTruoc_Title.Text = (Convert.ToInt32(cboThang.Text) - 1) + " T MS";
            //            rep.cel1tr.Text = String.Format("{0:N0}", cacthangtruoc.soBN);
            //            rep.cel2tr.Text = String.Format(fomat, cacthangtruoc.ThuocTayTieuHao);
            //            rep.cel3tr.Text = String.Format(fomat, cacthangtruoc.ThuocDongY);
            //            rep.cel4tr.Text = String.Format(fomat, cacthangtruoc.TongThuocVTYT);
            //            rep.cel5tr.Text = String.Format(fomat, cacthangtruoc.ThuocVTMien);
            //            rep.cel6tr.Text = String.Format(fomat, cacthangtruoc.ThuocVTKMien);
            //            rep.cel7tr.Text = String.Format(fomat, cacthangtruoc.TongThuocVTYT);
            //            rep.cel8tr.Text = String.Format("{0:N0}", cacthangtruoc.soBNMien);
            //            rep.cel9tr.Text = String.Format(fomat, cacthangtruoc.TongDonMien);
            //            #endregion
            //        }
            //        #region tổng = tháng hiện tại + các tháng trước
            //        rep.celTong1.Text = String.Format("{0:N0}", tong.soBN);
            //        rep.celTong2.Text = String.Format(fomat, tong.ThuocTayTieuHao);
            //        rep.celTong3.Text = String.Format(fomat, tong.ThuocDongY);
            //        rep.celTong4.Text = String.Format(fomat, tong.TongThuocVTYT);
            //        rep.celTong5.Text = String.Format(fomat, tong.ThuocVTMien);
            //        rep.celTong6.Text = String.Format(fomat, tong.ThuocVTKMien);
            //        rep.celTong7.Text = String.Format(fomat, tong.TongThuocVTYT);
            //        rep.celTong8.Text = String.Format("{0:N0}", tong.soBNMien);
            //        rep.celTong9.Text = String.Format(fomat, tong.TongDonMien);


            //        #endregion
            //        rep.DataSource = ketqua1;
            //        rep.BindingData();
            //        rep.CreateDocument();
            //        frm.prcIN.PrintingSystem = rep.PrintingSystem;
            //        frm.ShowDialog();
            //        #endregion
            //    }

            //    #endregion
            //}
            #endregion
        }

        class KQXHH
        {
            public int MaKP { set; get; }
            public string TenKP { set; get; }

            public double XQ { set; get; }
            public double Citi { set; get; }
            public double SAM { set; get; }
            public double DoLXUONG { set; get; }
            public double Tong { set; get; }

            public int soBNRV { set; get; }
            public double ThuocTayTieuHao { set; get; }
            public double ThuocDongY { set; get; }
            public double TongThuocVTYT { set; get; }

            public int soBNMien { set; get; }
            public double TongDonMien { set; get; }// tổng chi phí của đơn thuốc miễn

            public double ThuocVTMien { set; get; }// thuốc , vật tư miễn
            public double ThuocVTKMien { set; get; }// thuốc, vật tư không miễn
            public double TongThuocVTKMien { set; get; }// thuốc, vật tư không miễn
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdNgay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdNgay.SelectedIndex == 0)
            {
                radioGroup1.SelectedIndex = 0;
                radioGroup1.ReadOnly = true;
            }
            else
            {
                radioGroup1.ReadOnly = false;
            }
        }

        private void cklKhoaPhong_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklKhoaPhong.GetItemChecked(0) == true)
                    cklKhoaPhong.CheckAll();
                else
                    cklKhoaPhong.UnCheckAll();
            }
        }
    }
}