using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV.FormNhap;
using System.Collections;

namespace QLBV.FormThamSo
{
    //trả dược cho lĩnh về khoa: tìm kiếm thuốc (thêm kiểu đơn = 1 - do xuất nội trú, xuất cho lĩnh dược về khoa có kiểu đơn là 1) - dungtt_ngày 28022019
    public partial class frm_LinhKhoa_Moi : DevExpress.XtraEditors.XtraUserControl
    {
        public frm_LinhKhoa_Moi()
        {
            InitializeComponent();
        }
        private void Enablebutton(bool Status)
        {
            btnLuu.Enabled = !Status;
            btnMoi.Enabled = Status;
            btnSua.Enabled = Status;
            btnXoa.Enabled = Status;
            btnKLuu.Enabled = !Status;
        }
        private void EnableControl(bool status)
        {
            dtNgayKe.Properties.ReadOnly = !status;
            lupBPKe.Properties.ReadOnly = !status;
            lupKhoXuat.Properties.ReadOnly = !status;
            lupNguoiKe.Properties.ReadOnly = !status;
            // cboNhomDuoc.Properties.ReadOnly = !status;
            grvDonThuocct.OptionsBehavior.Editable = status;
            grcDonThuocdt.Enabled = !status;
            cboKieuPL.Properties.ReadOnly = !status;
        }
        private void ResetControl()
        {
            dtNgayKe.EditValue = System.DateTime.Now;
            // cboNhomDuoc.Text = "";
            lupBPKe.EditValue = 0;
            lupKhoXuat.EditValue = 0;
            lupNguoiKe.EditValue = "";
        }
        public class DV
        {
            public int? MaDV { get; set; }
            public string TenDV { get; set; }
            public string HamLuong { get; set; }
            public string SoLo { get; set; }
            public DateTime? HanDung { get; set; }
        }
        private bool KtraLuu()
        {
            if (dtNgayKe.EditValue == null || dtNgayKe.EditValue.ToString() == "")
            {
                MessageBox.Show("Ngày kê không hợp lệ!");
                dtNgayKe.Focus();
                return false;
            }
            if (lupBPKe.EditValue == null || string.IsNullOrEmpty(lupBPKe.Text))
            {
                MessageBox.Show("Bộ phận kê không hợp lệ");
                lupBPKe.Focus();
                return false;
            }
            if (lupKhoXuat.EditValue == null)
            {
                MessageBox.Show("Kho xuất không hợp lệ");
                lupKhoXuat.Focus();
                return false;
            }
            if (grvDonThuocct.GetRowCellValue(0, colMaDVdt) == null)
            {
                MessageBox.Show("Bạn chưa chọn thuốc");
                lupKhoXuat.Focus();
                return false;
            }

            return true;
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int TrangThai = 0;
        List<DThuoc> _ldthuoc = new List<DThuoc>();
        List<DThuocct> _ldthuocct = new List<DThuocct>();
        DateTime _dttu = new DateTime();
        DateTime _dtden = new DateTime();
        int _makp = 0;
        int StatusDT = 1;
        int SoPL = -1;
        int _selIndex = -1;
        int ppxuat = -1;
        static double tonthuoc = 0;
        static double soluongt = 0;// số lượng một loại thuốc được kê trên cùng 1 đơn thuốc
        double _TT = 0;
        int iddon = 0;

        bool isLoad = false;
        private void TimKiem()
        {
            if (!isLoad)
            {
                return;
            }

            //_data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            _ldthuoc.Clear();

            if (lupTimBP.EditValue != null)
            {
                _makp = Convert.ToInt32(lupTimBP.EditValue);
            }

            var tong = (from dt in _data.DThuocs
                        join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                        where (dt.NgayKe >= _dttu && dt.NgayKe <= _dtden)
                        where (dt.MaKP == _makp)
                        where (dt.PLDV == 1 && ((dt.KieuDon == 3) || (dt.KieuDon == 4)))
                        select new { dt, dtct });//.OrderByDescending(p => p.dt.NgayKe).ToList();
            //var _lDonThuoc = (from c in tong
            //                  join kp in _data.KPhongs on c.dt.MaKP equals kp.MaKP
            //                  group c by new { a = c.dt, kp.PLoai } into kq
            //                  select new { kq.Key.a.IDDon, kq.Key.a.NgayKe, kq.Key.a.KieuDon, kq.Key.a.MaBNhan, ChuaLinh = string.Join(";", kq.Where(p => p.dtct.Status == 0 && p.dtct.SoPL >= 0).Select(p => p.dtct.SoPL).ToList().Distinct()), DaLinh = string.Join(";", kq.Where(p => p.dtct.Status == 1 && p.dtct.SoPL > 0).Select(p => p.dtct.SoPL).ToList().Distinct()), kq.Key.PLoai, kq.Key.a.MaBNhanChiTiet }).OrderByDescending(p => p.IDDon).ToList();

            var _lDonThuoc = (from c in tong
                              join kp in _data.KPhongs on c.dt.MaKP equals kp.MaKP
                              group c by new
                              {
                                  a = c.dt,
                                  kp.PLoai
                              } into kq
                              select new DonThuoc()
                              {
                                  IDDon = kq.Key.a.IDDon,
                                  NgayKe = kq.Key.a.NgayKe,
                                  KieuDon = kq.Key.a.KieuDon,
                                  MaBNhan = kq.Key.a.MaBNhan,
                                  ChuaLinhs = kq.Where(p => p.dtct.Status == 0 && p.dtct.SoPL >= 0).Select(p => p.dtct.SoPL),
                                  DaLinhs = kq.Where(p => p.dtct.Status == 1 && p.dtct.SoPL > 0).Select(p => p.dtct.SoPL),
                                  PLoai = kq.Key.PLoai,
                                  MaBNhanChiTiet = kq.Key.a.MaBNhanChiTiet
                              }).OrderByDescending(o => new
                              {
                                  o.IDDon,
                                  o.NgayKe
                              }).ToList();//.OrderByDescending(p => p.IDDon).ToList();

            foreach (var donThuoc in _lDonThuoc)
            {
                donThuoc.DaLinh = string.Join(";", donThuoc.DaLinhs.ToList().Distinct());
                donThuoc.ChuaLinh = string.Join(";", donThuoc.ChuaLinhs.ToList().Distinct());
            }

            grcDonThuocdt.DataSource = _lDonThuoc;
        }
        bool BothuocKoSD = false;
        private void frm_LinhKhoa_Moi_Load(object sender, EventArgs e)
        {
            isLoad = false;

            if (DungChung.Bien.MaBV == "24012")
            {
                colHanDung.Visible = true;
            }
            else
            {
                colHanDung.Visible = false;
            }
            if (ppxuat == 3)
            {
                colSoLo.Visible = true;
                colHanDung.Visible = true;
            }
            else
            {
                colSoLo.Visible = false;
                colHanDung.Visible = false;
            }
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var a2 = (from dt in _data.DThuocs.Where(p => p.KieuDon == 2).Where(p => p.MaBNhan == null)
                      join dtct in _data.DThuoccts.Where(p => p.SoLuong < 0) on dt.IDDon equals dtct.IDDon
                      select dt).ToList();
            if (a2.Count > 0)
            {
                foreach (var b in a2)
                {
                    var sua = _data.DThuocs.Single(p => p.IDDon == b.IDDon);
                    sua.KieuDon = 4;
                    _data.SaveChanges();
                }
            }
            dtTimTuNgay.DateTime = System.DateTime.Now;
            dtTimDenNgay.DateTime = System.DateTime.Now;
            Enablebutton(true);
            EnableControl(false);
            var kp = (from kphong in _data.KPhongs
                      where (kphong.Status == 1 && (kphong.PLoai.ToLower() == "lâm sàng" || kphong.PLoai.ToLower() == "phòng khám" || kphong.PLoai.ToLower() == "khoa dược" || kphong.PLoai.ToLower() == "cận lâm sàng" || kphong.PLoai.ToLower() == "tủ trực"))
                      select new { kphong.MaKP, kphong.TenKP, kphong.PLoai }).ToList();
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                lupBPKe.Properties.DataSource = kp;
                lupTimBP.Properties.DataSource = kp;
            }
            else
            {
                var kptheotk = (from k in kp
                                join p in DungChung.Bien.listKPHoatDong on k.MaKP equals p
                                select k).ToList();
                lupBPKe.Properties.DataSource = kptheotk;
                lupTimBP.Properties.DataSource = kptheotk;
            }
            lupKhoXuat.Properties.DataSource = kp.Where(p => p.PLoai.ToLower().Contains("khoa dược"));
            TrangThai = 0;
            lupTimBP.EditValue = DungChung.Bien.MaKP;
            binSDonThuocct.DataSource = _ldthuocct;
            grcDonThuocct.DataSource = binSDonThuocct;

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
            {
                panelGhiChu.Visible = true;
            }

            isLoad = true;

            TimKiem();
        }

        private void dtTimTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimBP_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void GetDonThuocCT()
        {
            int makho = 0;

            if (lupKhoXuat.EditValue != null)
                makho = Convert.ToInt32(lupKhoXuat.EditValue);

            var kp = _data.KPhongs.Where(p => p.MaKP == makho).ToList();
            if (kp.Count > 0 && kp.First().PPXuat == 3)
            {
                colDonGia.OptionsColumn.ReadOnly = true;
                ppxuat = 3;
            }
            else
            {
                colSoLo.OptionsColumn.ReadOnly = true;
                ppxuat = -1;
            }
            if (ppxuat == 3)
            {
                colSoLo.Visible = true;
                colHanDung.Visible = true;
                if (DungChung.Bien.MaBV == "24012")
                {
                    colSoLo.VisibleIndex = 7;
                    colHanDung.VisibleIndex = colSoLo.VisibleIndex + 1;
                    colXoactdt.VisibleIndex = colHanDung.VisibleIndex + 1;
                }
            }
            else
            {
                colSoLo.Visible = false;
                colHanDung.Visible = false;
            }

            if (lupBPKe.EditValue != null)
            {
                _makp = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                string makhoake = ";" + _makp.ToString() + ";";

                var dvu = (from duoc in _data.DichVus.Where(p => BothuocKoSD == true ? p.Status == 1 : true).Where(p => p.MaKPsd.Contains(makhoake))
                           join tn in _data.TieuNhomDVs
                           on duoc.IdTieuNhom equals tn.IdTieuNhom
                           select duoc);//.ToList();

                if (cboKieuPL.SelectedIndex == 1)
                {
                    var qnd = (from nd in _data.NhapDs.Where(p => p.MaKP == makho && p.MaKPnx == _makp).Where(p => p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 2 || p.KieuDon == 5 || p.KieuDon == 6 || p.KieuDon == 7 || p.KieuDon == 11))
                               join ndct in _data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                               select new
                               {
                                   ndct.MaDV,
                                   ndct.DonGia,
                                   ndct.DonVi,
                                   ndct.SoLo,
                                   ndct.HanDung
                               }).Distinct();//.ToList();

                    var duoc = (from DT in qnd
                                join tenduoc in dvu on DT.MaDV equals tenduoc.MaDV
                                group new { DT, tenduoc } by new { tenduoc.TenRG, tenduoc.TenDV, tenduoc.MaDV, tenduoc.DonVi, tenduoc.HamLuong, DT.SoLo, DT.HanDung } into kq
                                select new
                                {
                                    TenDV = (DungChung.Bien.MaBV == "04011" || DungChung.Bien.MaBV == "24009") ? kq.Key.TenRG : kq.Key.TenDV,
                                    kq.Key.MaDV,
                                    kq.Key.DonVi,
                                    kq.Key.HamLuong,
                                    kq.Key.SoLo,
                                    kq.Key.HanDung
                                }).OrderBy(p => p.TenDV).ToList();

                    List<DV> _lDV = new List<DV>();
                    if (DungChung.Bien.MaBV == "24012")
                    {
                        int kho = lupKhoXuat.EditValue != null ? Convert.ToInt32(lupKhoXuat.EditValue) : 0;

                        foreach (var item in duoc)
                        {
                            var dsgia = DungChung.Ham._getDSGia(_data, item.MaDV, kho, true);
                            if (dsgia != null && dsgia.Count > 0)
                            {
                                DV a = new DV();
                                a.MaDV = item.MaDV;
                                a.TenDV = item.TenDV;
                                a.HamLuong = item.HamLuong;
                                a.SoLo = item.SoLo;
                                a.HanDung = item.HanDung;
                                _lDV.Add(a);
                            }
                        }

                        lupMaDuocdt.DataSource = _lDV;
                    }
                    else
                    {
                        lupMaDuocdt.DataSource = duoc.ToList();
                    }
                }

                else
                {
                    var duoc2 = (from nhapduoc in _data.NhapDcts
                                 join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                                 select new
                                 {
                                     nhapduoc.MaDV,
                                     nhapduoc.DonGia,
                                     nhapduoc.DonVi,
                                     nduoc.MaKP,
                                     nhapduoc.SoLuongN,
                                     nhapduoc.SoLo,
                                     nhapduoc.HanDung
                                 });//.ToList();

                    if (ppxuat == 3)
                    {
                        var duoc = (from tenduoc in duoc2
                                    join nduoc in dvu on tenduoc.MaDV equals nduoc.MaDV
                                    group new { tenduoc, nduoc } by new { nduoc.TenRG, tenduoc.MaKP, nduoc.TenDV, tenduoc.MaDV, nduoc.DonVi, nduoc.HamLuong, tenduoc.SoLo, tenduoc.HanDung, nduoc.DonGia } into kq
                                    select new
                                    {
                                        TenDV = (DungChung.Bien.MaBV == "04011" || DungChung.Bien.MaBV == "24009") ? kq.Key.TenRG :
                                        kq.Key.TenDV,
                                        kq.Key.MaDV,
                                        kq.Key.DonVi,
                                        kq.Key.MaKP,
                                        kq.Key.HamLuong,
                                        kq.Key.SoLo,
                                        kq.Key.HanDung,
                                        kq.Key.DonGia
                                    }).OrderBy(p => p.TenDV).ToList();

                        List<DV> _lDV = new List<DV>();
                        if (DungChung.Bien.MaBV == "24012")
                        {
                            int kho = 0;
                            kho = lupKhoXuat.EditValue != null ? Convert.ToInt32(lupKhoXuat.EditValue) : 0;
                            foreach (var item in duoc)
                            {
                                int madv = item.MaDV ?? 0;
                                if (grvDonThuocct.OptionsBehavior.Editable)
                                {
                                    var dsgia = DungChung.Ham._getDSGia(_data, madv, kho, true);
                                }

                                DV dv = new DV()
                                {
                                    MaDV = item.MaDV,
                                    TenDV = item.TenDV,
                                    HamLuong = item.HamLuong,
                                    SoLo = item.SoLo,
                                    HanDung = item.HanDung,
                                };

                                // TH thêm mới, sửa, thì kiểm tra tồn
                                if (!grvDonThuocct.OptionsBehavior.Editable)
                                {
                                    _lDV.Add(dv);
                                }
                                else if (DungChung.Ham._checkTon_KD1(_data, madv, kho, item.DonGia, 0, item.SoLo, item.HanDung) > 0)
                                {
                                    _lDV.Add(dv);
                                }
                            }

                            lupMaDuocdt.DataSource = _lDV;
                        }
                        else
                        {
                            lupMaDuocdt.DataSource = duoc.ToList();
                        }
                    }
                    else
                    {
                        var duoc = (from tenduoc in duoc2
                                    join nduoc in dvu on tenduoc.MaDV equals nduoc.MaDV
                                    group new { tenduoc, nduoc } by new { nduoc.TenRG, tenduoc.MaKP, nduoc.TenDV, tenduoc.MaDV, nduoc.DonVi, nduoc.HamLuong } into kq
                                    select new
                                    {
                                        TenDV = (DungChung.Bien.MaBV == "04011" || DungChung.Bien.MaBV == "24009") ? kq.Key.TenRG : kq.Key.TenDV,
                                        kq.Key.MaDV,
                                        kq.Key.DonVi,
                                        kq.Key.MaKP,
                                        kq.Key.HamLuong
                                    }).OrderBy(p => p.TenDV).ToList();

                        lupMaDuocdt.DataSource = duoc.ToList();
                    }

                    if (lupBPKe.EditValue != null)
                    {
                        _makp = Convert.ToInt32(lupBPKe.EditValue);
                    }

                    if (lupKhoXuat.EditValue != null)
                    {
                        makho = Convert.ToInt32(lupKhoXuat.EditValue);
                    }
                }
            }
        }

        private bool isGrvDonThuocDtFocusedRowChanged = false;
        private void grvDonThuocdt_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            isGrvDonThuocDtFocusedRowChanged = false;

            if (grvDonThuocdt.GetFocusedRowCellValue(colIDDon) != null && grvDonThuocdt.GetFocusedRowCellValue(colIDDon).ToString() != "")
            {
                iddon = Convert.ToInt32(grvDonThuocdt.GetFocusedRowCellValue(colIDDon));
                _ldthuoc = _data.DThuocs.Where(p => p.IDDon == iddon).ToList();
                _ldthuocct = _data.DThuoccts.Where(p => p.IDDon == iddon).OrderBy(p => p.SoPL).ThenBy(p => p.IDDonct).ToList();

                if (_ldthuoc.Count > 0)
                {
                    lupBPKe.EditValue = _ldthuoc.First().MaKP;
                    lupKhoXuat.EditValue = _ldthuoc.First().MaKXuat;
                    lupNguoiKe.EditValue = _ldthuoc.First().MaCB;

                    if (_ldthuoc.First().NgayKe != null)
                        dtNgayKe.DateTime = _ldthuoc.First().NgayKe.Value;

                    if (_ldthuoc.First().KieuDon == 3)
                    {
                        cboKieuPL.SelectedIndex = 0;
                    }
                    else
                    {
                        cboKieuPL.SelectedIndex = 1;
                    }

                    var sp = (from d in _ldthuocct select new { d.SoPL }).ToList();
                    if (sp.Count > 0 && sp.First().SoPL != null && sp.First().SoPL.ToString() != "")
                    {
                        SoPL = sp.First().SoPL;
                    }
                    else
                        SoPL = -1;

                    GetDonThuocCT();

                    //int kp = _ldthuoc.First().MaKP == null ? 0 : Convert.ToInt32(_ldthuoc.First().MaKP);
                    //int kx = _ldthuoc.First().MaKXuat == null ? 0 : Convert.ToInt32(_ldthuoc.First().MaKXuat);
                    //string _makpsd = "";
                    //if (!string.IsNullOrEmpty(lupBPKe.Text))
                    //{
                    //    _makpsd = ";" + lupBPKe.EditValue.ToString() + ";";
                    //}
                    //var q_Tong = (from tenduoc in _data.DichVus.Where(p => p.PLoai == 1 || p.PLoai == 5).Where(p => (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "01830") ? p.Status == 1 : true)
                    //              join nhapduoc in _data.NhapDcts on tenduoc.MaDV equals nhapduoc.MaDV
                    //              join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == kx) on nhapduoc.IDNhap equals nduoc.IDNhap
                    //              select new
                    //              {
                    //                  tenduoc.TenRG,
                    //                  tenduoc.MaKPsd,
                    //                  tenduoc.TenDV,
                    //                  tenduoc.MaDV,
                    //                  tenduoc.DonVi,
                    //                  nduoc.MaKP,
                    //                  nhapduoc.DonGia,
                    //                  nhapduoc.SoLo,
                    //                  nhapduoc.HanDung,
                    //                  tenduoc.HamLuong
                    //              });//.OrderBy(p => p.TenDV).ToList();

                    //var duoc = (from dongia in q_Tong
                    //            where (dongia.MaKPsd.Contains(_makpsd))
                    //            group dongia by new { dongia.TenRG, dongia.MaKP, dongia.TenDV, dongia.MaDV, dongia.DonVi, dongia.SoLo } into kq
                    //            select new
                    //            {
                    //                TenDV = (DungChung.Bien.MaBV == "04011" || DungChung.Bien.MaBV == "24009") ? kq.Key.TenRG : kq.Key.TenDV,
                    //                kq.Key.SoLo,
                    //                kq.Key.MaDV,
                    //                kq.Key.DonVi,
                    //                kq.Key.MaKP
                    //            });//.OrderBy(p => p.TenDV).ToList();

                    //var duoc1 = (from dongia in q_Tong
                    //             where (dongia.MaKPsd.Contains(_makpsd))
                    //             group dongia by new { dongia.TenRG, dongia.TenDV, dongia.MaDV, dongia.DonVi, dongia.HamLuong, dongia.SoLo, dongia.HanDung } into kq
                    //             select new
                    //             {
                    //                 kq.Key.MaDV,
                    //                 TenDV = (DungChung.Bien.MaBV == "04011" || DungChung.Bien.MaBV == "24009") ? kq.Key.TenRG : kq.Key.TenDV,
                    //                 kq.Key.HamLuong,
                    //                 kq.Key.SoLo,
                    //                 kq.Key.HanDung
                    //             }// kq.Key.DonGia, 
                    //         ).OrderBy(p => p.TenDV).ToList();

                    //List<DV> _lDV = new List<DV>();
                    //if (DungChung.Bien.MaBV == "24012")
                    //{
                    //    int kho = 0;
                    //    kho = lupKhoXuat.EditValue != null ? Convert.ToInt32(lupKhoXuat.EditValue) : 0;
                    //    foreach (var item in duoc1)
                    //    {
                    //        //var dsgia = DungChung.Ham._getDSGia(_data, item.MaDV, kho, true);

                    //        //if (DungChung.Bien.SoLuongTon > 0)
                    //        //{
                    //        DV a = new DV();
                    //        a.MaDV = item.MaDV;
                    //        a.TenDV = item.TenDV;
                    //        a.HamLuong = item.HamLuong;
                    //        a.SoLo = item.SoLo;
                    //        a.HanDung = item.HanDung;
                    //        _lDV.Add(a);
                    //        //}
                    //    }
                    //    lupMaDuocdt.DataSource = _lDV;
                    //}
                    //else
                    //{
                    //    lupMaDuocdt.DataSource = duoc.OrderBy(p => p.TenDV).ToList();
                    //}
                }

                binSDonThuocct.DataSource = _ldthuocct;
                grcDonThuocct.DataSource = binSDonThuocct;
            }
            else
            {
                iddon = 0;
                StatusDT = 1;
                SoPL = -1;
                StatusDT = -1;
                grcDonThuocct.DataSource = "";
            }

            isGrvDonThuocDtFocusedRowChanged = true;
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            Enablebutton(false);
            EnableControl(true);
            BothuocKoSD = true;
            groupControl5.Enabled = false;
            ResetControl();
            lupBPKe.EditValue = DungChung.Bien.MaKP;
            lupNguoiKe.EditValue = DungChung.Bien.MaCB;
            binSDonThuocct.DataSource = _ldthuocct.Where(p => p.IDDon == 0).ToList();
            grcDonThuocct.DataSource = binSDonThuocct;
            TrangThai = 1;
        }

        private void lupBPKe_EditValueChanged(object sender, EventArgs e)
        {
            lupKhoXuat_EditValueChanged(sender, e);

            int makp = 0;
            string makptk = "";
            if (lupBPKe.EditValue != null)
            {
                makp = Convert.ToInt32(lupBPKe.EditValue);
                makptk = ";" + makp + ";";
            }
            var _cb = _data.CanBoes.Where(p => p.MaKPsd.Contains(makptk)).Where(p => p.Status == 1).ToList();
            if (_cb.Count > 0)
                lupNguoiKe.Properties.DataSource = _cb;
        }

        private void cboKieuPL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isGrvDonThuocDtFocusedRowChanged)
            {
                return;
            }

            if (TrangThai == 1 || TrangThai == 2)
            {
                int i = 0;
                if (cboKieuPL.SelectedIndex == _selIndex)
                {
                    lupKhoXuat_EditValueChanged(sender, e);

                    return;
                }

                if (grvDonThuocct.RowCount >= 1 && grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                {
                    cboKieuPL.SelectedIndex = _selIndex;
                    lupKhoXuat_EditValueChanged(sender, e);

                    return;
                }
            }

            _selIndex = cboKieuPL.SelectedIndex;

            lupKhoXuat_EditValueChanged(sender, e);
        }

        //private void lupKhoXuat_EditValueChanged(object sender, EventArgs e)
        //{
        //    int makho = 0;

        //    if (lupKhoXuat.EditValue != null)
        //        makho = Convert.ToInt32(lupKhoXuat.EditValue);

        //    var kp = _data.KPhongs.Where(p => p.MaKP == makho).ToList();
        //    if (kp.Count > 0 && kp.First().PPXuat == 3)
        //    {
        //        colDonGia.OptionsColumn.ReadOnly = true;
        //        ppxuat = 3;
        //    }
        //    else
        //    {
        //        colSoLo.OptionsColumn.ReadOnly = true;
        //        ppxuat = -1;
        //    }
        //    if (ppxuat == 3)
        //    {
        //        colSoLo.Visible = true;
        //        colHanDung.Visible = true;
        //        if (DungChung.Bien.MaBV == "24012")
        //        {
        //            colSoLo.VisibleIndex = 7;
        //            colHanDung.VisibleIndex = colSoLo.VisibleIndex + 1;
        //            colXoactdt.VisibleIndex = colHanDung.VisibleIndex + 1;
        //        }
        //    }
        //    else
        //    {
        //        colSoLo.Visible = false;
        //        colHanDung.Visible = false;
        //    }

        //    if (lupBPKe.EditValue != null)
        //    {
        //        _makp = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
        //        string makhoake = ";" + _makp.ToString() + ";";

        //        var dvu = (from duoc in _data.DichVus.Where(p => BothuocKoSD == true ? p.Status == 1 : true).Where(p => p.MaKPsd.Contains(makhoake))
        //                   join tn in _data.TieuNhomDVs
        //                   on duoc.IdTieuNhom equals tn.IdTieuNhom
        //                   select duoc);//.ToList();

        //        if (cboKieuPL.SelectedIndex == 1)
        //        {
        //            var qnd = (from nd in _data.NhapDs.Where(p => p.MaKP == makho && p.MaKPnx == _makp).Where(p => p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 2 || p.KieuDon == 5 || p.KieuDon == 6 || p.KieuDon == 7 || p.KieuDon == 11))
        //                       join ndct in _data.NhapDcts on nd.IDNhap equals ndct.IDNhap
        //                       select new
        //                       {
        //                           ndct.MaDV,
        //                           ndct.DonGia,
        //                           ndct.DonVi,
        //                           ndct.SoLo,
        //                           ndct.HanDung
        //                       }).Distinct();//.ToList();

        //            var duoc = (from DT in qnd
        //                        join tenduoc in dvu on DT.MaDV equals tenduoc.MaDV
        //                        group new { DT, tenduoc } by new { tenduoc.TenRG, tenduoc.TenDV, tenduoc.MaDV, tenduoc.DonVi, tenduoc.HamLuong, DT.SoLo, DT.HanDung } into kq
        //                        select new
        //                        {
        //                            TenDV = (DungChung.Bien.MaBV == "04011" || DungChung.Bien.MaBV == "24009") ? kq.Key.TenRG : kq.Key.TenDV,
        //                            kq.Key.MaDV,
        //                            kq.Key.DonVi,
        //                            kq.Key.HamLuong,
        //                            kq.Key.SoLo,
        //                            kq.Key.HanDung
        //                        }).OrderBy(p => p.TenDV).ToList();

        //            List<DV> _lDV = new List<DV>();
        //            if (DungChung.Bien.MaBV == "24012")
        //            {
        //                int kho = lupKhoXuat.EditValue != null ? Convert.ToInt32(lupKhoXuat.EditValue) : 0;

        //                foreach (var item in duoc)
        //                {
        //                    var dsgia = DungChung.Ham._getDSGia(_data, item.MaDV, kho, true);
        //                    if (dsgia != null && dsgia.Count > 0)
        //                    {
        //                        DV a = new DV();
        //                        a.MaDV = item.MaDV;
        //                        a.TenDV = item.TenDV;
        //                        a.HamLuong = item.HamLuong;
        //                        a.SoLo = item.SoLo;
        //                        a.HanDung = item.HanDung;
        //                        _lDV.Add(a);
        //                    }
        //                }

        //                lupMaDuocdt.DataSource = _lDV;
        //            }
        //            else
        //            {
        //                lupMaDuocdt.DataSource = duoc.ToList();
        //            }
        //        }

        //        else
        //        {
        //            var duoc2 = (from nhapduoc in _data.NhapDcts
        //                         join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == makho) on nhapduoc.IDNhap equals nduoc.IDNhap
        //                         select new
        //                         {
        //                             nhapduoc.MaDV,
        //                             nhapduoc.DonGia,
        //                             nhapduoc.DonVi,
        //                             nduoc.MaKP,
        //                             nhapduoc.SoLuongN,
        //                             nhapduoc.SoLo,
        //                             nhapduoc.HanDung
        //                         });//.ToList();

        //            if (ppxuat == 3)
        //            {
        //                var duoc = (from tenduoc in duoc2
        //                            join nduoc in dvu on tenduoc.MaDV equals nduoc.MaDV
        //                            group new { tenduoc, nduoc } by new { nduoc.TenRG, tenduoc.MaKP, nduoc.TenDV, tenduoc.MaDV, nduoc.DonVi, nduoc.HamLuong, tenduoc.SoLo, tenduoc.HanDung } into kq
        //                            select new
        //                            {
        //                                TenDV = (DungChung.Bien.MaBV == "04011" || DungChung.Bien.MaBV == "24009") ? kq.Key.TenRG :
        //                                kq.Key.TenDV,
        //                                kq.Key.MaDV,
        //                                kq.Key.DonVi,
        //                                kq.Key.MaKP,
        //                                kq.Key.HamLuong,
        //                                kq.Key.SoLo,
        //                                kq.Key.HanDung
        //                            }).OrderBy(p => p.TenDV).ToList();

        //                List<DV> _lDV = new List<DV>();
        //                if (DungChung.Bien.MaBV == "24012")
        //                {
        //                    int kho = 0;
        //                    kho = lupKhoXuat.EditValue != null ? Convert.ToInt32(lupKhoXuat.EditValue) : 0;
        //                    foreach (var item in duoc)
        //                    {
        //                        int madv = item.MaDV ?? 0;
        //                        var dsgia = DungChung.Ham._getDSGia(_data, madv, kho, true);

        //                        if (DungChung.Bien.SoLuongTon > 0)
        //                        {
        //                            DV a = new DV();
        //                            a.MaDV = item.MaDV;
        //                            a.TenDV = item.TenDV;
        //                            a.HamLuong = item.HamLuong;
        //                            a.SoLo = item.SoLo;
        //                            a.HanDung = item.HanDung;
        //                            _lDV.Add(a);
        //                        }
        //                    }
        //                    lupMaDuocdt.DataSource = _lDV;
        //                }
        //                else
        //                {
        //                    lupMaDuocdt.DataSource = duoc.ToList();
        //                }
        //            }
        //            else
        //            {
        //                var duoc = (from tenduoc in duoc2
        //                            join nduoc in dvu on tenduoc.MaDV equals nduoc.MaDV
        //                            group new { tenduoc, nduoc } by new { nduoc.TenRG, tenduoc.MaKP, nduoc.TenDV, tenduoc.MaDV, nduoc.DonVi, nduoc.HamLuong } into kq
        //                            select new
        //                            {
        //                                TenDV = (DungChung.Bien.MaBV == "04011" || DungChung.Bien.MaBV == "24009") ? kq.Key.TenRG : kq.Key.TenDV,
        //                                kq.Key.MaDV,
        //                                kq.Key.DonVi,
        //                                kq.Key.MaKP,
        //                                kq.Key.HamLuong
        //                            }).OrderBy(p => p.TenDV).ToList();

        //                List<DV> _lDV = new List<DV>();
        //                lupMaDuocdt.DataSource = duoc.ToList();
        //            }

        //            if (lupBPKe.EditValue != null)
        //            {
        //                _makp = Convert.ToInt32(lupBPKe.EditValue);
        //            }

        //            if (lupKhoXuat.EditValue != null)
        //            {
        //                makho = Convert.ToInt32(lupKhoXuat.EditValue);
        //            }
        //        }
        //    }
        //}

        private void lupKhoXuat_EditValueChanged(object sender, EventArgs e)
        {
            if (isGrvDonThuocDtFocusedRowChanged)
            {
                GetDonThuocCT();
            }
        }

        private class _dongia
        {
            public double DonGia { set; get; }
        }

        private void grvDonThuocct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<DungChung.Ham.giaSoLoHSD> dsgia = new List<DungChung.Ham.giaSoLoHSD>();
            DungChung.Ham.giaSoLoHSD dsgianew = new DungChung.Ham.giaSoLoHSD();
            string _solo = "";
            DateTime? _handung = new DateTime();
            int makho = 0;
            int madv = 0;
            int maKhoKe = 0;

            string tempsolo = "";
            DateTime? temphandung = new DateTime();
            double tempgia = 0;
            double soluongsua = 0;
            if (lupBPKe.EditValue != null)
                maKhoKe = Convert.ToInt32(lupBPKe.EditValue);
            int _mak = 0;
            if (lupKhoXuat.EditValue != null)
            {
                _mak = Convert.ToInt32(lupKhoXuat.EditValue);
            }
            if (grvDonThuocct.GetFocusedRowCellValue(colMaDVdt) != null)
                madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));

            var qtt = _data.KPhongs.Where(parameters => parameters.MaKP == maKhoKe && parameters.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).ToList();
            var qdtct = _data.DThuoccts.Where(p => p.IDDon == iddon).Where(p => p.MaDV == madv).ToList();
            if (qdtct.Count > 0)
                soluongsua = qdtct.Sum(p => p.SoLuong);
            int ppxuat = 0;
            var kp = _data.KPhongs.Where(p => p.MaKP == _mak).Select(p => p.PPXuat).ToList();
            if (kp.Count > 0 && kp.First() != null)
                ppxuat = kp.First().Value;
            switch (e.Column.Name)
            {
                case "colMaDVdt":
                    #region colMaDVdt

                    if (grvDonThuocct.GetFocusedRowCellValue(colMaDVdt) != null)
                    {
                        if (DungChung.Bien.MaBV == "24012")
                        {
                            cboDonGia.Items.Clear();
                            if (cboKieuPL.SelectedIndex == 0)
                            {

                                madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));
                                grvDonThuocct.SetFocusedRowCellValue(colDonVi, DungChung.Ham._getDonVi(_data, madv));

                                grvDonThuocct.SetFocusedRowCellValue(colMaCC, DungChung.Bien._maCC);
                                soluongt = 0;

                                //for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                //{
                                //    if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                //        if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                //        {
                                //            if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                //            {

                                //                if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                //                    soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                //            }
                                //        }
                                //}
                                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                grvDonThuocct.SetFocusedRowCellValue(colThanhTien, "0");
                                if (grvDonThuocct.GetFocusedRowCellValue(colSoLo) != null)
                                    _solo = grvDonThuocct.GetFocusedRowCellValue(colSoLo).ToString();
                                if (grvDonThuocct.GetFocusedRowCellValue(colHanDung) != null)
                                    _handung = Convert.ToDateTime(grvDonThuocct.GetFocusedRowCellValue(colHanDung));
                                dsgia = DungChung.Ham._getDSGia(_data, madv, _mak, true);

                                if (dsgia.Count > 0)
                                {
                                    grvDonThuocct.SetFocusedRowCellValue(colSoLo, dsgia.First().SoLo);
                                    grvDonThuocct.SetFocusedRowCellValue(colHanDung, dsgia.First().HanDung);
                                    grvDonThuocct.SetFocusedRowCellValue(colDonGia, dsgia.First().Gia);
                                    double soluongttheosolo = 0;

                                    foreach (var gia in dsgia)
                                    {
                                        soluongt = 0;
                                        double soton = DungChung.Ham._checkTon_KD1(_data, madv, _mak, gia.Gia, 0, gia.SoLo, gia.HanDung);
                                        for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                        {
                                            if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                                if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                                {
                                                    if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                                    {
                                                        if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                                            if (grvDonThuocct.GetRowCellValue(i, colSoLo).ToString() == gia.SoLo)
                                                                soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                                    }
                                                }
                                        }
                                        if (soton - soluongt > 0)
                                        {
                                            tempsolo = gia.SoLo;
                                            temphandung = gia.HanDung;
                                            tempgia = gia.Gia;
                                            grvDonThuocct.SetFocusedRowCellValue(colSoLo, gia.SoLo);
                                            grvDonThuocct.SetFocusedRowCellValue(colHanDung, gia.HanDung);
                                            grvDonThuocct.SetFocusedRowCellValue(colDonGia, gia.Gia);
                                            break;
                                        }
                                    }
                                    for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                    {
                                        if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                            if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                            {
                                                if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                                {
                                                    if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                                        if (grvDonThuocct.GetRowCellValue(i, colSoLo).ToString() == tempsolo)
                                                            soluongttheosolo += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                                }
                                            }
                                    }
                                    tonthuoc = DungChung.Ham._checkTon_KD1(_data, madv, _mak, tempgia, 0, tempsolo, temphandung);
                                    grpDThuocct.Text = "Số lượng tồn: " + (tonthuoc - soluongttheosolo).ToString();
                                }
                                else
                                {
                                    grpDThuocct.Text = "Số lượng tồn: 0 ";
                                }

                            }
                            else
                            {
                                madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));
                                _makp = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                                makho = Convert.ToInt32(lupKhoXuat.EditValue);

                                var duoc1 = (from nd in _data.NhapDs.Where(p => p.MaKP == makho).Where(p => p.MaKPnx == _makp).Where(p => (p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 2 || p.KieuDon == 5 || p.KieuDon == 6 || p.KieuDon == 7 || p.KieuDon == 11)) || (p.PLoai == 1 && p.KieuDon == 2 && (p.TraDuoc_KieuDon == 1 || p.TraDuoc_KieuDon == 2 || p.TraDuoc_KieuDon == 6 || p.TraDuoc_KieuDon == 5 || p.TraDuoc_KieuDon == 7 || p.TraDuoc_KieuDon == 11)))
                                             join ndct in _data.NhapDcts.Where(p => p.MaDV == madv) on nd.IDNhap equals ndct.IDNhap
                                             join tenduoc in _data.DichVus on ndct.MaDV equals tenduoc.MaDV
                                             group new { nd, ndct, tenduoc } by new { tenduoc.TenDV, tenduoc.MaDV, ndct.DonGia, ndct.DonVi, ndct.SoLo } into kq
                                             select new { kq.Key.TenDV, kq.Key.SoLo, kq.Key.MaDV, kq.Key.DonVi, SL = kq.Sum(p => p.ndct.SoLuongX) - kq.Sum(p => p.ndct.SoLuongN), kq.Key.DonGia}
                                       ).OrderBy(p => p.TenDV).Where(p => p.SL > 0).ToList();
                                var duoc = (from d in duoc1
                                            group new { d } by new { d.TenDV, d.MaDV, d.DonVi, d.DonGia, d.SoLo, d.SL } into k
                                            select new
                                            {
                                                k.Key.TenDV,
                                                k.Key.SoLo,
                                                k.Key.SL,
                                                k.Key.DonVi,
                                                k.Key.DonGia,
                                                k.Key.MaDV,
                                            }).ToList();
                                string Dv = "";
                                if (duoc.Count() > 0)
                                    Dv = duoc.Where(p => p.MaDV == madv).First().DonVi;
                                grvDonThuocct.SetFocusedRowCellValue(colDonVi, Dv);
                                cboDonGia.Items.Clear();
                                grvDonThuocct.SetFocusedRowCellValue(colDonGia, "0");
                                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                grvDonThuocct.SetFocusedRowCellValue(colThanhTien, "0");

                                foreach (var a in duoc)
                                {
                                    cboDonGia.Items.Add(a.DonGia);
                                    if (!string.IsNullOrEmpty(a.SoLo))
                                        cboSoLo.Items.Add(a.SoLo);
                                    grvDonThuocct.SetFocusedRowCellValue(colSoLo, a.SoLo);
                                    grvDonThuocct.SetFocusedRowCellValue(colDonGia, a.DonGia);
                                }
                            }
                        }
                        else
                        {
                            cboDonGia.Items.Clear();
                            if (cboKieuPL.SelectedIndex == 0)
                            {

                                madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));
                                grvDonThuocct.SetFocusedRowCellValue(colDonVi, DungChung.Ham._getDonVi(_data, madv));

                                grvDonThuocct.SetFocusedRowCellValue(colMaCC, DungChung.Bien._maCC);
                                soluongt = 0;

                                for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                {
                                    if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                        if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                        {
                                            if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                            {

                                                if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                                    soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                            }
                                        }
                                }
                                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                grvDonThuocct.SetFocusedRowCellValue(colThanhTien, "0");
                                if (grvDonThuocct.GetFocusedRowCellValue(colSoLo) != null)
                                    _solo = grvDonThuocct.GetFocusedRowCellValue(colSoLo).ToString();
                                if (grvDonThuocct.GetFocusedRowCellValue(colHanDung) != null)
                                    _handung = Convert.ToDateTime(grvDonThuocct.GetFocusedRowCellValue(colHanDung));
                                dsgia = DungChung.Ham._getDSGia(_data, madv, _mak, true);

                                if (dsgia.Count > 0)
                                {
                                    grvDonThuocct.SetFocusedRowCellValue(colSoLo, dsgia.First().SoLo);
                                    grvDonThuocct.SetFocusedRowCellValue(colDonGia, dsgia.First().Gia);
                                    tonthuoc = DungChung.Bien.SoLuongTon;
                                    grpDThuocct.Text = "Số lượng tồn: " + (tonthuoc - soluongt).ToString();
                                }
                                else
                                {
                                    grpDThuocct.Text = "Số lượng tồn: 0 ";
                                }

                            }
                            else
                            {
                                madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));
                                _makp = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                                makho = Convert.ToInt32(lupKhoXuat.EditValue);

                                var duoc1 = (from nd in _data.NhapDs.Where(p => p.MaKP == makho).Where(p => p.MaKPnx == _makp).Where(p => (p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 2 || p.KieuDon == 5 || p.KieuDon == 6 || p.KieuDon == 7 || p.KieuDon == 11)) || (p.PLoai == 1 && p.KieuDon == 2 && (p.TraDuoc_KieuDon == 1 || p.TraDuoc_KieuDon == 2 || p.TraDuoc_KieuDon == 6 || p.TraDuoc_KieuDon == 5 || p.TraDuoc_KieuDon == 7 || p.TraDuoc_KieuDon == 11)))
                                             join ndct in _data.NhapDcts.Where(p => p.MaDV == madv) on nd.IDNhap equals ndct.IDNhap
                                             join tenduoc in _data.DichVus on ndct.MaDV equals tenduoc.MaDV
                                             group new { nd, ndct, tenduoc } by new { tenduoc.TenDV, tenduoc.MaDV, ndct.DonGia, ndct.DonVi, ndct.SoLo, ndct.HanDung } into kq
                                             select new { kq.Key.TenDV, kq.Key.SoLo, kq.Key.MaDV, kq.Key.DonVi, SL = kq.Sum(p => p.ndct.SoLuongX) - kq.Sum(p => p.ndct.SoLuongN), kq.Key.DonGia, kq.Key.HanDung }
                                       ).OrderBy(p => p.TenDV).Where(p => p.SL > 0).ToList();
                                var duoc = (from d in duoc1
                                            group new { d } by new { d.TenDV, d.MaDV, d.DonVi, d.DonGia, d.SoLo, d.SL } into k
                                            select new
                                            {
                                                k.Key.TenDV,
                                                k.Key.SoLo,
                                                k.Key.SL,
                                                k.Key.DonVi,
                                                k.Key.DonGia,
                                                k.Key.MaDV,
                                            }).ToList();
                                string Dv = duoc.Where(p => p.MaDV == madv).First().DonVi;
                                grvDonThuocct.SetFocusedRowCellValue(colDonVi, Dv);
                                cboDonGia.Items.Clear();
                                grvDonThuocct.SetFocusedRowCellValue(colDonGia, "0");
                                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                grvDonThuocct.SetFocusedRowCellValue(colThanhTien, "0");

                                foreach (var a in duoc)
                                {
                                    cboDonGia.Items.Add(a.DonGia);
                                    if (!string.IsNullOrEmpty(a.SoLo))
                                        cboSoLo.Items.Add(a.SoLo);
                                    grvDonThuocct.SetFocusedRowCellValue(colSoLo, a.SoLo);
                                    grvDonThuocct.SetFocusedRowCellValue(colDonGia, a.DonGia);
                                }
                            }
                        }
                    }
                    break;
                #endregion
                case "colDonGia":
                    #region colDonGia

                    if (grvDonThuocct.GetFocusedRowCellValue(colMaDVdt) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() != "")
                    {

                        madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));
                        _makp = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                        makho = Convert.ToInt32(lupKhoXuat.EditValue);
                        double _dongia = 0;
                        _dongia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colDonGia));
                        if (grvDonThuocct.GetFocusedRowCellValue(colSoLo) != null)
                            _solo = grvDonThuocct.GetFocusedRowCellValue(colSoLo).ToString();
                        if (grvDonThuocct.GetFocusedRowCellValue(colHanDung) != null)
                            _handung = Convert.ToDateTime(grvDonThuocct.GetFocusedRowCellValue(colHanDung));
                        grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                        grvDonThuocct.SetFocusedRowCellValue(colThanhTien, "0");
                        if (cboKieuPL.SelectedIndex == 1)
                        {
                            //ppxuat = -1;
                            //var kpx = _data.KPhongs.Where(p => p.MaKP == makho).FirstOrDefault();
                            //if (kp != null && kp.PPXuat != null)
                            //    ppxuat = kp.PPXuat.Value;
                            if (qtt.Count > 0)
                            {
                                #region khoa kê là tủ trực
                                var duoc = (from nd in _data.NhapDs.Where(p => p.MaKP == makho).Where(p => p.MaKPnx == _makp).Where(p => (p.PLoai == 2 && (p.KieuDon == 2 || p.KieuDon == 6)) || (p.PLoai == 1 && p.KieuDon == 2 && (p.TraDuoc_KieuDon == 2 || p.TraDuoc_KieuDon == 6)))
                                            join ndct in _data.NhapDcts.Where(p => ppxuat == 3 ? p.SoLo == _solo && p.HanDung == _handung : true) on nd.IDNhap equals ndct.IDNhap
                                            join tenduoc in _data.DichVus on ndct.MaDV equals tenduoc.MaDV
                                            group new { nd, ndct, tenduoc } by new { tenduoc.TenDV, tenduoc.MaDV, ndct.DonGia, ndct.DonVi, ndct.SoLo, ndct.HanDung } into kq
                                            select new { kq.Key.TenDV, kq.Key.SoLo, kq.Key.HanDung, kq.Key.MaDV, kq.Key.DonVi, SL = kq.Sum(p => p.ndct.SoLuongX) - kq.Sum(p => p.ndct.SoLuongN), kq.Key.DonGia }
                                       ).OrderBy(p => p.TenDV).ToList();

                                if (duoc.Count > 0)
                                {
                                    var solo = duoc.Where(p => p.MaDV == madv).Where(p => p.DonGia == _dongia);
                                    if (solo.Count() > 0)
                                    {
                                        _solo = solo.First().SoLo;
                                        _handung = solo.First().HanDung;
                                    }
                                        
                                    grvDonThuocct.SetFocusedRowCellValue(colSoLo, _solo);
                                }
                                double _slton = DungChung.Bien.MaBV == "24012" ? DungChung.Ham._checkTon_KD1(_data, madv, _makp, _dongia, 0, _solo, _handung) : DungChung.Ham._checkTon_KD(_data, madv, _makp, _dongia, 0, _solo);
                                DungChung.Bien.SoLuongTon = _slton;
                                grpDThuocct.Text = "Số lượng tồn: " + DungChung.Bien.SoLuongTon;
                                //dsgia=DungChung.Ham._getGia(_data, madv, maKhoKe);
                                //tonthuoc = DungChung.Bien.SoLuongTon;
                                //_TT = tonthuoc;
                                //grvDonThuocct.SetFocusedRowCellValue(colMaCC, DungChung.Bien._maCC);
                                //soluongt = 0;
                                //for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                //{
                                //    if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                //        if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                //        {
                                //            if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                //            {
                                //                string n = grvDonThuocct.GetRowCellValue(i, colIDDonct).ToString();
                                //                if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                //                    soluongt += Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                //            }
                                //        }
                                //}
                                //grpDThuocct.Text = "Số lượng tồn: " + (tonthuoc + soluongt).ToString();
                                //tonthuoc = tonthuoc + soluongt;
                                // et colum colTTluu để biết là tạo mới row hay sửa row trên chi tiết đơn
                                if (TrangThai == 2)
                                {
                                    if (grvDonThuocct.GetFocusedRowCellValue(colIDDonct) != null && grvDonThuocct.GetFocusedRowCellValue(colIDDonct).ToString() != "")
                                    {
                                        if (int.Parse(grvDonThuocct.GetFocusedRowCellValue(colIDDonct).ToString()) > 0)
                                        {
                                            grvDonThuocct.SetFocusedRowCellValue(colTTLuu, "2");
                                        }
                                    }
                                }


                                #endregion
                            }
                            else
                            {
                                #region khoa kê không phải tủ trực
                                double Dongia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString());
                                var duoc1 = (from DT in _data.DThuocs.Where(p => p.KieuDon == 3).Where(p => p.MaKXuat == makho).Where(p => p.MaKP == _makp)
                                             join DTct in _data.DThuoccts.Where(p => p.DonGia == Dongia).Where(p => ppxuat == 3 ? p.SoLo == _solo && p.HanDung == _handung : true).Where(p => p.Status == 1) on DT.IDDon equals DTct.IDDon
                                             join tenduoc in _data.DichVus.Where(p => p.MaDV == madv) on DTct.MaDV equals tenduoc.MaDV
                                             group new { DTct, tenduoc } by new { tenduoc.TenDV, tenduoc.MaDV, DTct.DonGia, DTct.DonVi } into kq
                                             select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, SL = kq.Sum(p => p.DTct.SoLuong), kq.Key.DonGia }
                                    ).OrderBy(p => p.TenDV).ToList();
                                var duoc2 = (from DT in _data.DThuocs.Where(p => p.KieuDon == 4).Where(p => p.MaKXuat == makho).Where(p => p.MaKP == _makp)
                                             join DTct in _data.DThuoccts.Where(p => ppxuat == 3 ? p.SoLo == _solo && p.HanDung == _handung : true).Where(p => p.DonGia == Dongia) on DT.IDDon equals DTct.IDDon
                                             join tenduoc in _data.DichVus.Where(p => p.MaDV == madv) on DTct.MaDV equals tenduoc.MaDV
                                             group new { DTct, tenduoc } by new { tenduoc.TenDV, tenduoc.MaDV, DTct.DonGia, DTct.DonVi } into kq
                                             select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, SL = kq.Sum(p => p.DTct.SoLuong), kq.Key.DonGia }
                                   ).OrderBy(p => p.TenDV).ToList();

                                if (duoc1.Count > 0)
                                {
                                    if (duoc2.Count <= 0)
                                    {
                                        //LupDonGia.DataSource = duoc1;
                                        tonthuoc = duoc1.First().SL;
                                        _TT = duoc1.First().SL;
                                        grvDonThuocct.SetFocusedRowCellValue(colMaCC, DungChung.Bien._maCC);
                                        soluongt = 0;
                                        for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                        {
                                            if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                                if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                                {
                                                    if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)) && _dongia == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colDonGia)))
                                                    {
                                                        string n = grvDonThuocct.GetRowCellValue(i, colIDDonct).ToString();
                                                        if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                                            soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                                    }
                                                }
                                        }
                                        grpDThuocct.Text = "Số lượng tồn: " + (tonthuoc + soluongt).ToString();
                                        // et colum colTTluu để biết là tạo mới row hay sửa row trên chi tiết đơn
                                        if (TrangThai == 2)
                                        {
                                            if (grvDonThuocct.GetFocusedRowCellValue(colIDDonct) != null && grvDonThuocct.GetFocusedRowCellValue(colIDDonct).ToString() != "")
                                            {
                                                if (int.Parse(grvDonThuocct.GetFocusedRowCellValue(colIDDonct).ToString()) > 0)
                                                {
                                                    grvDonThuocct.SetFocusedRowCellValue(colTTLuu, "2");
                                                }
                                            }
                                        }
                                        //grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                        //grvDonThuocct.SetFocusedRowCellValue(colThanhTien, "0");
                                    }
                                    else
                                    {
                                        tonthuoc = duoc1.First().SL + duoc2.First().SL;
                                        _TT = duoc1.First().SL;
                                        grvDonThuocct.SetFocusedRowCellValue(colMaCC, DungChung.Bien._maCC);
                                        soluongt = 0;
                                        for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                        {
                                            if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                                if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                                {
                                                    if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)) && _dongia == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colDonGia)))
                                                    {
                                                        string n = grvDonThuocct.GetRowCellValue(i, colIDDonct).ToString();
                                                        if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                                            soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                                    }
                                                }
                                        }
                                        grpDThuocct.Text = "Số lượng tồn: " + (tonthuoc + soluongt).ToString();
                                        tonthuoc = tonthuoc + soluongt;
                                        // et colum colTTluu để biết là tạo mới row hay sửa row trên chi tiết đơn
                                        if (TrangThai == 2)
                                        {
                                            if (grvDonThuocct.GetFocusedRowCellValue(colIDDonct) != null && grvDonThuocct.GetFocusedRowCellValue(colIDDonct).ToString() != "")
                                            {
                                                if (int.Parse(grvDonThuocct.GetFocusedRowCellValue(colIDDonct).ToString()) > 0)
                                                {
                                                    grvDonThuocct.SetFocusedRowCellValue(colTTLuu, "2");
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            double _slton = DungChung.Bien.MaBV == "24012" ? DungChung.Ham._checkTon_KD1(_data, madv, makho, _dongia, 0, _solo, _handung) : DungChung.Ham._checkTon_KD(_data, madv, makho, _dongia, 0, _solo);
                            DungChung.Bien.SoLuongTon = _slton;
                            grpDThuocct.Text = "Số lượng tồn: " + DungChung.Bien.SoLuongTon.ToString();
                        }

                    }

                    break;
                #endregion
                case "colSoLuong":
                    #region lĩnh dược
                    if (cboKieuPL.SelectedIndex == 0)
                    {
                        if (grvDonThuocct.GetFocusedRowCellValue(colSoLuong) != null && grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                        {
                            #region 24012
                            if (DungChung.Bien.MaBV == "24012")
                            {
                                double a = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString());
                                if (a != 0)
                                {
                                    switch (TrangThai)
                                    {
                                        case 1: // khi tao don moi
                                            if (a < 0)
                                            {
                                                MessageBox.Show("Số lượng phải >0");
                                                grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                                                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                            }
                                            else if (a > 0)
                                            {
                                                double soluongt = 0;
                                                double dongia = 0;
                                                double soton = 0;

                                                if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colDonGia) != null)
                                                {
                                                    dongia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colDonGia));
                                                }
                                                if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colSoLo) != null)
                                                {
                                                    _solo = Convert.ToString(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colSoLo));
                                                }
                                                if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colHanDung) != null)
                                                {
                                                    _handung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colHanDung));
                                                }

                                                dsgianew = DungChung.Ham._getGia(_data, madv, _mak);
                                                var dsgia1 = DungChung.Ham._getDSGia(_data, madv, _mak, true).Where(p => p.SoLo == _solo && p.HanDung == _handung).ToList();
                                                if (dsgia1.Count() > 0)
                                                {
                                                    dsgianew = dsgia1.First();
                                                }

                                                soton = DungChung.Ham._checkTon_KD1(_data, madv, _mak, dongia, 0, _solo, _handung);

                                                for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                                {
                                                    if (i != grvDonThuocct.FocusedRowHandle && grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                                        if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                                        {
                                                            if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                                            {
                                                                double dongiact = 0;
                                                                if (grvDonThuocct.GetRowCellValue(i, colDonGia) != null)
                                                                {
                                                                    dongiact = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia));
                                                                }
                                                                if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                                                    if (dsgianew.SoLo == grvDonThuocct.GetRowCellValue(i, colSoLo).ToString())
                                                                        soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                                            }
                                                        }
                                                }

                                                if (ppxuat == 3)
                                                {
                                                    if (_solo == dsgianew.SoLo && _handung == dsgianew.HanDung)
                                                        tonthuoc = soton - soluongt - a;
                                                    else
                                                        tonthuoc = 0 - soluongt - a;
                                                }
                                                else
                                                    tonthuoc = DungChung.Bien.SoLuongTon - soluongt - a;

                                                if (tonthuoc >= 0)
                                                {

                                                    if (grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                                    {
                                                        double b = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString());
                                                        grvDonThuocct.SetFocusedRowCellValue(colThanhTien, a * b);
                                                    }
                                                    grpDThuocct.Text = "Số lượng tồn: " + tonthuoc.ToString();
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Số lượng trong kho không đủ");
                                                    grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                                    grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                                                                                                                  //DungChung.Bien.SoLuongTon = 0;
                                                    grpDThuocct.Text = "Số lượng tồn: " + (tonthuoc - a).ToString();
                                                }
                                            }
                                            break;
                                        case 2:// khi sua don
                                               //soluongt = a;
                                            if (a < 0)
                                            {
                                                MessageBox.Show("Số lượng phải >0");
                                                grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];
                                                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                            }
                                            else if (a > 0)
                                            {
                                                double soluongt = 0;
                                                double dongia = 0;


                                                if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colDonGia) != null)
                                                {
                                                    dongia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colDonGia));
                                                }
                                                if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colSoLo) != null)
                                                {
                                                    _solo = Convert.ToString(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colSoLo));
                                                }
                                                if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colHanDung) != null)
                                                {
                                                    _handung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colHanDung));
                                                }

                                                dsgianew = DungChung.Ham._getGia(_data, madv, _mak);
                                                var qdtct1 = _data.DThuoccts.Where(p => p.IDDon == iddon).Where(p => p.MaDV == madv && p.SoLo == _solo && p.HanDung == _handung).ToList();
                                                if (qdtct1.Count > 0)
                                                    soluongsua = qdtct1.Sum(p => p.SoLuong);
                                                dongia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colDonGia));
                                                var soton = DungChung.Ham._checkTon_KD1(_data, madv, _mak, dongia, 0, _solo, _handung);
                                                // tonthuoc = DungChung.Bien.SoLuongTon;


                                                var dsgianew1 = DungChung.Ham._getDSGia(_data, madv, _mak, true).Where(p => p.SoLo == _solo && p.HanDung == _handung).ToList();
                                                if (dsgianew1.Count() > 0)
                                                {
                                                    dsgianew = dsgianew1.First();
                                                }

                                                for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                                {
                                                    if (i != grvDonThuocct.FocusedRowHandle && grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                                        if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                                        {
                                                            if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                                            {
                                                                double dongiact = 0;
                                                                if (grvDonThuocct.GetRowCellValue(i, colDonGia) != null)
                                                                {
                                                                    dongiact = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia));
                                                                }
                                                                if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                                                    if (dsgianew.SoLo == _solo)
                                                                        soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                                            }
                                                        }
                                                }

                                                if (ppxuat == 3)
                                                {
                                                    if (_solo == dsgianew.SoLo && _handung == dsgianew.HanDung)
                                                        tonthuoc = soton + soluongsua - soluongt - a;
                                                    else
                                                        tonthuoc = 0 + soluongsua - soluongt - a;
                                                }
                                                else
                                                    tonthuoc = DungChung.Bien.SoLuongTon + soluongsua - soluongt - a;
                                                //tonthuoc = DungChung.Bien.SoLuongTon - soluongt;
                                                if (tonthuoc >= 0)
                                                {

                                                    if (grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                                    {
                                                        double b = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString());
                                                        grvDonThuocct.SetFocusedRowCellValue(colThanhTien, a * b);
                                                    }
                                                    grpDThuocct.Text = "Số lượng tồn: " + tonthuoc.ToString();
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Số lượng trong kho không đủ");
                                                    grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                                    grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                                                                                                                  //DungChung.Bien.SoLuongTon = 0;
                                                    grpDThuocct.Text = "Số lượng tồn: " + (tonthuoc + a).ToString();
                                                    break;
                                                }
                                            }

                                            //xem lại lượng tồn
                                            break;
                                    }
                                }
                            }
                            #endregion
                            #region BV khác
                            else
                            {
                                double a = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString());
                                if (a != 0)
                                {
                                    switch (TrangThai)
                                    {
                                        case 1: // khi tao don moi
                                            if (a < 0)
                                            {
                                                MessageBox.Show("Số lượng phải >0");
                                                grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                                                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                            }
                                            else if (a > 0)
                                            {

                                                dsgianew = DungChung.Ham._getGia(_data, madv, _mak);
                                                double soluongt = 0;
                                                double dongia = 0;
                                                if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colDonGia) != null)
                                                {
                                                    dongia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colDonGia));
                                                }
                                                if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colSoLo) != null)
                                                {
                                                    _solo = Convert.ToString(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colSoLo));
                                                }
                                                if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colHanDung) != null)
                                                {
                                                    _handung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colHanDung));
                                                }
                                                for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                                {
                                                    if (i != grvDonThuocct.FocusedRowHandle && grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                                        if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                                        {
                                                            if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                                            {
                                                                double dongiact = 0;
                                                                if (grvDonThuocct.GetRowCellValue(i, colDonGia) != null)
                                                                {
                                                                    dongiact = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia));
                                                                }
                                                                if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                                                    soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                                            }
                                                        }
                                                }

                                                if (ppxuat == 3)
                                                {
                                                    if (_solo == dsgianew.SoLo && _handung == dsgianew.HanDung)
                                                        tonthuoc = dsgianew.SoLuong - soluongt - a;
                                                    else
                                                        tonthuoc = 0 - soluongt - a;
                                                }
                                                else
                                                    tonthuoc = DungChung.Bien.SoLuongTon - soluongt - a;

                                                if (tonthuoc >= 0)
                                                {

                                                    if (grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                                    {
                                                        double b = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString());
                                                        grvDonThuocct.SetFocusedRowCellValue(colThanhTien, a * b);
                                                    }
                                                    grpDThuocct.Text = "Số lượng tồn: " + tonthuoc.ToString();
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Số lượng trong kho không đủ");
                                                    grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                                    grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                                                                                                                  //DungChung.Bien.SoLuongTon = 0;
                                                    grpDThuocct.Text = "Số lượng tồn: " + (tonthuoc - a).ToString();
                                                }
                                            }
                                            break;
                                        case 2:// khi sua don
                                               //soluongt = a;
                                            if (a < 0)
                                            {
                                                MessageBox.Show("Số lượng phải >0");
                                                grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];
                                                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                            }
                                            else if (a > 0)
                                            {

                                                dsgianew = DungChung.Ham._getGia(_data, madv, _mak);

                                                // tonthuoc = DungChung.Bien.SoLuongTon;
                                                double soluongt = 0;
                                                double dongia = 0;
                                                if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colDonGia) != null)
                                                {
                                                    dongia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colDonGia));
                                                }
                                                if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colSoLo) != null)
                                                {
                                                    _solo = Convert.ToString(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colSoLo));
                                                }
                                                if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colHanDung) != null)
                                                {
                                                    _handung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colHanDung));
                                                }

                                                for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                                {
                                                    if (i != grvDonThuocct.FocusedRowHandle && grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                                        if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                                        {
                                                            if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                                            {
                                                                double dongiact = 0;
                                                                if (grvDonThuocct.GetRowCellValue(i, colDonGia) != null)
                                                                {
                                                                    dongiact = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia));
                                                                }
                                                                if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                                                    soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                                            }
                                                        }
                                                }

                                                if (ppxuat == 3)
                                                {
                                                    if (_solo == dsgianew.SoLo && _handung == dsgianew.HanDung)
                                                        tonthuoc = dsgianew.SoLuong + soluongsua - soluongt - a;
                                                    else
                                                        tonthuoc = 0 + soluongsua - soluongt - a;
                                                }
                                                else
                                                    tonthuoc = DungChung.Bien.SoLuongTon + soluongsua - soluongt - a;
                                                //tonthuoc = DungChung.Bien.SoLuongTon - soluongt;
                                                if (tonthuoc >= 0)
                                                {

                                                    if (grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                                    {
                                                        double b = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString());
                                                        grvDonThuocct.SetFocusedRowCellValue(colThanhTien, a * b);
                                                    }
                                                    grpDThuocct.Text = "Số lượng tồn: " + tonthuoc.ToString();
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Số lượng trong kho không đủ");
                                                    grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                                    grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                                                                                                                  //DungChung.Bien.SoLuongTon = 0;
                                                    grpDThuocct.Text = "Số lượng tồn: " + (tonthuoc + a).ToString();
                                                    break;
                                                }
                                            }

                                            //xem lại lượng tồn
                                            break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("Bạn chưa nhập số lượng ");
                            grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                            grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                        }
                    }
                    #endregion
                    #region trả dược
                    else
                    {
                        if (grvDonThuocct.GetFocusedRowCellValue(colSoLuong) != null && grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString() != "" && double.Parse(grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString()) <= 0)
                        {

                            double a = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString());
                            switch (TrangThai)
                            {
                                case 1: // khi tao don moi
                                    if (a > 0)
                                    {
                                        MessageBox.Show("Số lượng phải <0");
                                        grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                                        grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                    }
                                    else if (a < 0)
                                    {
                                        if (grvDonThuocct.GetFocusedRowCellValue(colMaDVdt) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                        {

                                            tonthuoc = 0;
                                            madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));
                                            _makp = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                                            makho = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                                            double Dongia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString());

                                            #region trả dược từ tủ trực (tính tồn theo thực tế số tồn còn lại trong tủ trực
                                            if (qtt.Count > 0 && cboKieuPL.SelectedIndex == 1)
                                            {
                                                //DungChung.Ham._getGia(_data, madv, maKhoKe);
                                                tonthuoc = DungChung.Bien.SoLuongTon;
                                                //_TT = tonthuoc;
                                            }
                                            #endregion

                                            #region khác
                                            else
                                            {

                                                var duoc1 = (from DT in _data.DThuocs.Where(p => p.KieuDon == 3).Where(p => p.MaKXuat == makho).Where(p => p.MaKP == _makp)
                                                             join DTct in _data.DThuoccts.Where(p => p.DonGia == Dongia).Where(p => p.Status == 1) on DT.IDDon equals DTct.IDDon
                                                             join tenduoc in _data.DichVus.Where(p => p.MaDV == madv) on DTct.MaDV equals tenduoc.MaDV
                                                             group new { DTct, tenduoc } by new { tenduoc.TenDV, tenduoc.MaDV, DTct.DonGia, DTct.DonVi } into kq
                                                             select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, SL = kq.Sum(p => p.DTct.SoLuong), kq.Key.DonGia }
                                                    ).OrderBy(p => p.TenDV).ToList();
                                                var duoc2 = (from DT in _data.DThuocs.Where(p => p.KieuDon == 4).Where(p => p.MaKXuat == makho).Where(p => p.MaKP == _makp)
                                                             join DTct in _data.DThuoccts.Where(p => p.DonGia == Dongia) on DT.IDDon equals DTct.IDDon
                                                             join tenduoc in _data.DichVus.Where(p => p.MaDV == madv) on DTct.MaDV equals tenduoc.MaDV
                                                             group new { DTct, tenduoc } by new { tenduoc.TenDV, tenduoc.MaDV, DTct.DonGia, DTct.DonVi } into kq
                                                             select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, SL = kq.Sum(p => p.DTct.SoLuong), kq.Key.DonGia }
                                                   ).OrderBy(p => p.TenDV).ToList();
                                                if (duoc1.Count > 0)
                                                {
                                                    if (duoc2.Count > 0)
                                                    {

                                                        tonthuoc = duoc1.First().SL + duoc2.First().SL;
                                                        _TT = duoc1.First().SL;
                                                    }
                                                    else
                                                    {
                                                        tonthuoc = duoc1.First().SL;
                                                        _TT = duoc1.First().SL;
                                                    }
                                                }
                                            }
                                            #endregion



                                            soluongt = 0;
                                            for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                            {
                                                if (i != grvDonThuocct.FocusedRowHandle && grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                                    {
                                                        if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                                        {
                                                            string n = grvDonThuocct.GetRowCellValue(i, colIDDonct).ToString();
                                                            if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                                                soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                                        }
                                                    }
                                            }
                                        }
                                        tonthuoc = tonthuoc + soluongt;

                                        // if (a <= 0 && (a >= (tonthuoc * -1) || tonthuoc == 0) || (cboKieuPL.SelectedIndex == 1 && qtt.Count == 0) || (cboKieuPL.SelectedIndex == 1 && qtt.Count > 0 && (a + tonthuoc) >= 0))       //  || cboKieuPL.SelectedIndex==1 tạm thời
                                        if (tonthuoc + a >= 0)
                                        {

                                            tonthuoc = tonthuoc + a;
                                            if (grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                            {
                                                double b = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString());
                                                grvDonThuocct.SetFocusedRowCellValue(colThanhTien, a * b);

                                            }
                                            grpDThuocct.Text = "Số lượng tồn: " + tonthuoc.ToString();
                                        }
                                        else
                                        {

                                            //grvDonThuocct.SetFocusedRowCellValue(colSoLuong, 0);
                                            grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];
                                            MessageBox.Show("Số lượng không đủ để trả lại");
                                            grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                            // set focus
                                            //DungChung.Bien.SoLuongTon = 0;
                                            grpDThuocct.Text = "Số lượng tồn: " + tonthuoc.ToString();
                                        }
                                    }
                                    break;
                                case 2:// khi sua don
                                       //soluongt = a;
                                    if (a > 0)
                                    {
                                        MessageBox.Show("Số lượng phải <0");
                                        grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];
                                        grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                    }
                                    else if (a < 0)
                                    {

                                        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                                        if (grvDonThuocct.GetFocusedRowCellValue(colMaDVdt) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                        {
                                            madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));

                                            _makp = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                                            makho = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                                            double Dongia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString());
                                            if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colSoLo) != null)
                                            {
                                                _solo = Convert.ToString(grvDonThuocct.GetFocusedRowCellValue(colSoLo));
                                            }
                                            if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colHanDung) != null)
                                            {
                                                _handung = Convert.ToDateTime(grvDonThuocct.GetFocusedRowCellValue(colHanDung));
                                            }
                                            #region trả dược từ tủ trực (tính tồn theo thực tế số tồn còn lại trong tủ trực
                                            if (qtt.Count > 0 && cboKieuPL.SelectedIndex == 1)
                                            {
                                                //DungChung.Ham._getGia(_data, madv, maKhoKe);
                                                tonthuoc = DungChung.Ham._checkTon_KD1(_data, madv, _makp, Dongia, 0, _solo, _handung);
                                                //tonthuoc = DungChung.Bien.SoLuongTon;
                                                _TT = tonthuoc;
                                            }
                                            #endregion

                                            #region khác
                                            else
                                            {

                                                var duoc1 = (from DT in _data.DThuocs.Where(p => p.KieuDon == 3).Where(p => p.MaKXuat == makho).Where(p => p.MaKP == _makp)
                                                             join DTct in _data.DThuoccts.Where(p => p.DonGia == Dongia).Where(p => p.Status == 1) on DT.IDDon equals DTct.IDDon
                                                             join tenduoc in _data.DichVus.Where(p => p.MaDV == madv) on DTct.MaDV equals tenduoc.MaDV
                                                             group new { DTct, tenduoc } by new { tenduoc.TenDV, tenduoc.MaDV, DTct.DonGia, DTct.DonVi } into kq
                                                             select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, SL = kq.Sum(p => p.DTct.SoLuong), kq.Key.DonGia }
                                                    ).OrderBy(p => p.TenDV).ToList();
                                                var duoc2 = (from DT in _data.DThuocs.Where(p => p.KieuDon == 4).Where(p => p.MaKXuat == makho).Where(p => p.MaKP == _makp)
                                                             join DTct in _data.DThuoccts.Where(p => p.DonGia == Dongia) on DT.IDDon equals DTct.IDDon
                                                             join tenduoc in _data.DichVus.Where(p => p.MaDV == madv) on DTct.MaDV equals tenduoc.MaDV
                                                             group new { DTct, tenduoc } by new { tenduoc.TenDV, tenduoc.MaDV, DTct.DonGia, DTct.DonVi } into kq
                                                             select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, SL = kq.Sum(p => p.DTct.SoLuong), kq.Key.DonGia }
                                                   ).OrderBy(p => p.TenDV).ToList();
                                                if (duoc1.Count > 0)
                                                {
                                                    if (duoc2.Count > 0)
                                                    {

                                                        tonthuoc = duoc1.First().SL + duoc2.First().SL;
                                                        _TT = duoc1.First().SL;
                                                    }
                                                    else
                                                    {
                                                        tonthuoc = duoc1.First().SL;
                                                        _TT = duoc1.First().SL;
                                                    }
                                                }
                                                soluongt = 0;


                                                for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                                {
                                                    if (i != grvDonThuocct.FocusedRowHandle && grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                                        if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                                        {
                                                            if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                                            {
                                                                string n = grvDonThuocct.GetRowCellValue(i, colIDDonct).ToString();
                                                                if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                                                    soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                                            }
                                                        }
                                                }
                                            }
                                            #endregion
                                        }

                                        tonthuoc = tonthuoc + soluongt;
                                        if (a <= 0 && (tonthuoc - soluongsua + a >= 0))       // || cboKieuPL.SelectedIndex==1 tạm thời
                                        {
                                            tonthuoc = tonthuoc - soluongsua + a;
                                            if (grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                            {
                                                double b = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString());
                                                grvDonThuocct.SetFocusedRowCellValue(colThanhTien, a * b);
                                            }
                                            grpDThuocct.Text = "Số lượng tồn: " + tonthuoc.ToString();
                                        }
                                        else
                                        {
                                            tonthuoc = tonthuoc - soluongsua + a;
                                            MessageBox.Show("Số lượng trong kho không đủ");
                                            grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                            //MessageBox.Show("1");
                                            grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                                                                                                          ////DungChung.Bien.SoLuongTon = 0;
                                            grpDThuocct.Text = "Số lượng tồn: " + tonthuoc.ToString();
                                        }
                                    }

                                    //xem lại lượng tồn

                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Số lượng nhập trả thuốc phải nhỏ hơn 0");
                            grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                            grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                        }
                    }
                    #endregion trả dược
                    break;
            }
        }

        private void lupKhoXuat_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (TrangThai == 1 || TrangThai == 2)
            {
                if (grvDonThuocct.RowCount < 1)
                {
                    //MessageBox.Show("OK"); 
                }
                else
                {
                    int i = 0;
                    if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                    {

                        MessageBox.Show("Đơn đã có thuốc bạn không được thay đổi kho kê");
                        e.Cancel = true;
                    }
                    else
                    { e.Cancel = false; }

                }
            }
        }

        private void lupBPKe_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (TrangThai == 1 || TrangThai == 2)
            {
                if (grvDonThuocct.RowCount < 1)
                {
                    //MessageBox.Show("OK");
                }
                else
                {
                    int i = 0;
                    if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                    {
                        MessageBox.Show("Đơn đã có thuốc bạn không được thay đổi khoa kê");
                        e.Cancel = true;
                    }
                    else
                    { e.Cancel = false; }
                }
            }
        }

        private void grvDonThuocct_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (iddon > 0)
            {
                BothuocKoSD = true;
                int _mabp = 0;
                if (lupBPKe.EditValue != null)
                    _mabp = Convert.ToInt32(lupBPKe.EditValue);
                if (DungChung.Bien.listKPHoatDong.Where(p => p == _mabp).Count() > 0 || DungChung.Bien.PLoaiKP == "Admin")
                {
                    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    var qdt = data.DThuoccts.Where(p => p.IDDon == iddon).ToList();
                    if (qdt.Count > 0)
                    {
                        if (qdt.Where(p => p.SoPL <= 0).Count() > 0)
                        {
                            Enablebutton(false);
                            EnableControl(true);
                            groupControl5.Enabled = false;
                            TrangThai = 2;
                        }
                        else if (qdt.Where(p => p.Status == null || p.Status <= 0).Count() > 0)
                            MessageBox.Show("Phiếu lĩnh đã in, bạn không thể sửa");
                        else
                            MessageBox.Show("Phiếu lĩnh đã được xuất dược, bạn không được sửa");
                    }
                    else
                        MessageBox.Show("Không có phiếu lĩnh để sửa");
                }
            }
            else
            {
                MessageBox.Show("Không có phiếu lĩnh để sửa");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KtraLuu())
            {
                QLBV_Database.QLBVEntities D = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                #region thêm mới
                if (TrangThai == 1)
                {
                    DThuoc dthuoc = new DThuoc();
                    dthuoc.NgayKe = dtNgayKe.DateTime;
                    dthuoc.MaKP = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                    dthuoc.MaCB = lupNguoiKe.EditValue.ToString();
                    dthuoc.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                    dthuoc.PLDV = 1;
                    if (cboKieuPL.SelectedIndex == 0)
                    {
                        dthuoc.KieuDon = 3;
                    }
                    else
                    { dthuoc.KieuDon = 4; }
                    D.DThuocs.Add(dthuoc);
                    if (D.SaveChanges() >= 0)
                    {
                        int maxid = 0;
                        var que = (from max in _data.DThuocs.OrderByDescending(p => p.IDDon) select max.IDDon).ToList();
                        if (que.Count > 0)
                        {
                            maxid = int.Parse(que.First().ToString());
                        }
                        for (int i = 0; i < grvDonThuocct.DataRowCount; i++)
                        {
                            if (grvDonThuocct.GetRowCellDisplayText(i, colSoPLct) == null || (grvDonThuocct.GetRowCellDisplayText(i, colSoPLct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellDisplayText(i, colSoPLct)) <= 0))
                            {
                                if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                {
                                    if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "0" && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "")
                                    {
                                        DThuocct dthuocct = new DThuocct();
                                        dthuocct.SoPL = 0;
                                        dthuocct.IDDon = maxid;
                                        dthuocct.MaKP = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                                        dthuocct.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                                        dthuocct.MaDV = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt));
                                        dthuocct.SoLuong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                        dthuocct.DonVi = grvDonThuocct.GetRowCellValue(i, colDonVi).ToString().Trim();
                                        dthuocct.DonGia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia).ToString());
                                        dthuocct.ThanhTien = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colThanhTien).ToString());
                                        dthuocct.NgayNhap = dtNgayKe.DateTime;
                                        if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                            dthuocct.SoLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();
                                        if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null)
                                            dthuocct.HanDung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung).ToString());
                                        if (grvDonThuocct.GetRowCellValue(i, colMaCC) != null)
                                            dthuocct.MaCC = grvDonThuocct.GetRowCellValue(i, colMaCC).ToString();
                                        dthuocct.Status = 0;
                                        D.DThuoccts.Add(dthuocct);
                                        D.SaveChanges();
                                    }
                                }
                            }
                        }
                        TrangThai = 0;
                        MessageBox.Show("Tạo đơn thành công!");
                        Enablebutton(true);
                        EnableControl(true);
                        ResetControl();

                        groupControl5.Enabled = true;
                        frm_LinhKhoa_Moi_Load(sender, e);
                    }
                }
                #endregion
                #region sửa
                else
                {
                    if (TrangThai == 2)
                    {
                        QLBV_Database.QLBVEntities D1 = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                        if (iddon > 0)
                        {
                            int id = iddon;
                            var dthuoc = D1.DThuocs.Single(p => p.IDDon == id);
                            dthuoc.NgayKe = dtNgayKe.DateTime;
                            dthuoc.MaKP = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                            dthuoc.MaCB = lupNguoiKe.EditValue.ToString();
                            dthuoc.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                            dthuoc.PLDV = 1;
                            if (cboKieuPL.SelectedIndex == 0)
                            {
                                dthuoc.KieuDon = 3;
                            }
                            else
                            { dthuoc.KieuDon = 4; }
                            if (D1.SaveChanges() >= 0)
                            {
                                // lưu chi tiết đơn
                                for (int i = 0; i < grvDonThuocct.DataRowCount; i++)
                                {
                                    if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                    {
                                        if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "0" && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "")
                                        {
                                            if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && grvDonThuocct.GetRowCellValue(i, colIDDonct).ToString() != "")
                                            {
                                                int idct = int.Parse(grvDonThuocct.GetRowCellValue(i, colIDDonct).ToString());
                                                if (idct > 0)// sửa row
                                                {
                                                    DThuocct dthuocct = D1.DThuoccts.Single(p => p.IDDonct == idct);
                                                    dthuocct.IDDon = id;
                                                    dthuocct.MaKP = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                                                    dthuocct.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                                                    dthuocct.MaDV = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt));
                                                    dthuocct.DonVi = grvDonThuocct.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    dthuocct.DonGia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia).ToString());
                                                    dthuocct.SoLuong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString());
                                                    dthuocct.ThanhTien = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colThanhTien).ToString());
                                                    dthuocct.NgayNhap = dtNgayKe.DateTime;
                                                    if (grvDonThuocct.GetRowCellValue(i, colMaCC) != null)
                                                        dthuocct.MaCC = grvDonThuocct.GetRowCellValue(i, colMaCC).ToString();
                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                                        dthuocct.SoLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();
                                                    if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null)
                                                        dthuocct.HanDung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung));
                                                    dthuocct.Status = 0;
                                                    D1.SaveChanges();
                                                }
                                                else
                                                {// lưu row mới 
                                                    DThuocct dthuocct = new DThuocct();
                                                    dthuocct.IDDon = id;
                                                    dthuocct.SoPL = 0;
                                                    dthuocct.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                                                    dthuocct.MaDV = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt));
                                                    dthuocct.DonVi = grvDonThuocct.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    dthuocct.DonGia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia).ToString());
                                                    dthuocct.SoLuong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString());
                                                    dthuocct.ThanhTien = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colThanhTien).ToString());
                                                    dthuocct.NgayNhap = dtNgayKe.DateTime;
                                                    if (grvDonThuocct.GetRowCellValue(i, colMaCC) != null)
                                                        dthuocct.MaCC = grvDonThuocct.GetRowCellValue(i, colMaCC).ToString();
                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                                        dthuocct.SoLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();
                                                    if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null)
                                                        dthuocct.HanDung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung));
                                                    dthuocct.Status = 0;
                                                    D1.DThuoccts.Add(dthuocct);
                                                    D1.SaveChanges();
                                                }
                                            }
                                        }
                                    }
                                }
                                TrangThai = 0;
                                MessageBox.Show("Sửa thành công!");
                                groupControl5.Enabled = true;
                                Enablebutton(true);
                                ResetControl();
                                frm_LinhKhoa_Moi_Load(sender, e);

                            }
                        }
                    }
                }
                #endregion
            }
        }

        private void btnKLuu_Click(object sender, EventArgs e)
        {
            groupControl5.Enabled = true;
            this.frm_LinhKhoa_Moi_Load(sender, e);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (iddon > 0)
            {
                int id = iddon;
                var sopl = (from dt in _data.DThuocs.Where(p => p.IDDon == id)
                            join dtct in _data.DThuoccts
                                on dt.IDDon equals dtct.IDDon
                            join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                            select new { dt.MaKP, dtct.SoPL, dv.PLoai, dt.MaBNhanChiTiet }).Distinct().ToList();

                if (sopl.Count > 0)
                {
                    if (sopl.Where(p => p.SoPL == 0).Count() > 0)
                    {
                        FormNhap.frm_Check_moi frm = new FormNhap.frm_Check_moi(id, 3);
                        frm.ShowDialog();
                        groupControl5.Enabled = true;
                        this.frm_LinhKhoa_Moi_Load(sender, e);
                    }
                    else
                    {
                        foreach (var a in sopl)
                        {
                            string _spl = a.SoPL.ToString();
                            int _makp1 = a.MaKP == null ? 0 : a.MaKP.Value;
                            FormNhap.frm_Check_moi frm = new FormNhap.frm_Check_moi();
                            string[] pl1 = new string[2] { "", "" };
                            pl1[0] = _spl;
                            pl1[1] = _makp1.ToString();
                            frm.InPhieu(pl1, 3);

                        }
                    }
                }
                else
                {
                    FormNhap.frm_Check_moi frm = new FormNhap.frm_Check_moi(id, 3);
                    frm.ShowDialog();
                    groupControl5.Enabled = true;
                    this.frm_LinhKhoa_Moi_Load(sender, e);
                }

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (iddon != 0)
            {
                QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int id = iddon;
                var kt = (from dt in Data.DThuocs.Where(p => p.IDDon == id) join dtct in Data.DThuoccts on dt.IDDon equals dtct.IDDon select new { dtct.Status, dtct.SoPL }).ToList();
                if (kt.Count > 0)
                {
                    if (kt.Where(p => p.Status != 0).Count() > 0)
                    {
                        MessageBox.Show("Có phiếu lĩnh đã xuất hoặc đã hủy, Bạn không được xóa");

                    }
                    else
                    {
                        if (kt.Where(p => p.SoPL != null && p.SoPL > 0).Count() > 0)
                        {
                            var ploai = grvDonThuocdt.GetFocusedRowCellValue(colPLoai) != null ? grvDonThuocdt.GetFocusedRowCellValue(colPLoai).ToString() : "";
                            var mabnhanchitiet = grvDonThuocdt.GetFocusedRowCellValue(colMaBNhanChiTiet) != null ? grvDonThuocdt.GetFocusedRowCellValue(colMaBNhanChiTiet).ToString() : "";
                            if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && ploai.ToString() == "Tủ trực" && !string.IsNullOrEmpty(mabnhanchitiet.ToString()))
                            {
                                DialogResult _result = MessageBox.Show("Bạn muốn xóa phiếu lĩnh?", "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.Yes)
                                {
                                    var ktct = Data.DThuoccts.Where(p => p.IDDon == id).Select(p => p.IDDonct).ToList();
                                    foreach (var i in ktct)
                                    {
                                        string sopl = "";
                                        var xoact = Data.DThuoccts.Single(p => p.IDDonct == i);
                                        sopl = xoact.SoPL.ToString();
                                        Data.DThuoccts.Remove(xoact);
                                        if (Data.SaveChanges() > 0)
                                        {
                                            // dthuocct.sopl của đơn bù quan hệ 1-n vs dthuocct.dscbth của đơn kê bn -> khi xóa đơn bù thì sửa đơn kê bn dthuocct.dscbth = null & dthuocct.status = 0
                                            var editdt = Data.DThuoccts.Where(p => p.DSCBTH == sopl).Select(p => p.IDDonct).ToList();
                                            foreach (var item in editdt)
                                            {
                                                var dtct = Data.DThuoccts.Single(p => p.IDDonct == item);
                                                dtct.DSCBTH = null;
                                                dtct.Status = 0;
                                                Data.SaveChanges();
                                            }
                                        }
                                    }
                                    var xoa = Data.DThuocs.Single(p => p.IDDon == id);
                                    Data.DThuocs.Remove(xoa);
                                    Data.SaveChanges();
                                    TimKiem();
                                }
                            }
                            else
                                MessageBox.Show("Phiếu lĩnh đã in, bạn không được xóa");
                        }
                        else
                        {
                            DialogResult _result = MessageBox.Show("Bạn muốn xóa phiếu lĩnh?", "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.Yes)
                            {
                                var ktct = Data.DThuoccts.Where(p => p.IDDon == id).Select(p => p.IDDonct).ToList();
                                foreach (var i in ktct)
                                {
                                    var xoact = Data.DThuoccts.Single(p => p.IDDonct == i);
                                    Data.DThuoccts.Remove(xoact);
                                    Data.SaveChanges();
                                }
                                var xoa = Data.DThuocs.Single(p => p.IDDon == id);
                                Data.DThuocs.Remove(xoa);
                                Data.SaveChanges();
                                TimKiem();
                            }
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Không có phiếu lĩnh để xóa");
            }
        }

        private void grvDonThuocdt_DataSourceChanged(object sender, EventArgs e)
        {
            if (grvDonThuocdt.DataSource != null
                && ((IList)grvDonThuocdt.DataSource).Count == 0)
                isGrvDonThuocDtFocusedRowChanged = true;
            else
                isGrvDonThuocDtFocusedRowChanged = false;

            //grvDonThuocdt_FocusedRowChanged(null, null);
        }

        private void grvDonThuocct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (grvDonThuocct.GetFocusedRowCellValue(colIDDonct) != null)
            {
                if (grvDonThuocct.GetFocusedRowCellValue(colSoPLct) != null)
                {
                    if (Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colSoPLct)) > 0)
                    {
                        colMaDVdt.OptionsColumn.ReadOnly = true;
                        colSoLuong.OptionsColumn.ReadOnly = true;
                    }
                    else
                    {
                        colMaDVdt.OptionsColumn.ReadOnly = false;
                        colSoLuong.OptionsColumn.ReadOnly = false;
                    }
                }
                else
                {
                    colMaDVdt.OptionsColumn.ReadOnly = false;
                    colSoLuong.OptionsColumn.ReadOnly = false;
                }
            }
            else
            {
                colMaDVdt.OptionsColumn.ReadOnly = false;
                colSoLuong.OptionsColumn.ReadOnly = false;
            }
        }

        private void grvDonThuocct_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXoactdt")
            {
                if (TrangThai == 1)
                {
                    grvDonThuocct.DeleteSelectedRows();
                }
                if (TrangThai == 2)
                {
                    int idct = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colIDDonct));
                    if (idct > 0)
                    {
                        var xoa = _data.DThuoccts.Single(p => p.IDDonct == idct);


                        if (xoa.Status == 0)
                        {
                            if (xoa.SoPL > 0)
                            {
                                MessageBox.Show("Thuốc đã lên phiếu lĩnh, bạn không thể xóa");
                            }
                            else
                            {
                                DialogResult _result = MessageBox.Show("Bạn muốn xóa thuốc: " + grvDonThuocct.GetFocusedRowCellDisplayText(colMaDVdt).ToString(), "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.Yes)
                                {
                                    _data.DThuoccts.Remove(xoa);
                                    _data.SaveChanges();
                                    grvDonThuocct.DeleteSelectedRows();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Thuốc đã hủy hoặc đã xuất, bạn không thể xóa");
                        }
                    }

                }
            }
        }

        private void lupMaDuocdt_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grvDonThuocct_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {

        }

        private void btnLinhThuoc_Click(object sender, EventArgs e)
        {
            frmPhieulinh frm = new frmPhieulinh();
            frm.ShowDialog();
        }

        private void btnDuTruThuoc_Click(object sender, EventArgs e)
        {
            QLBV.FormThamSo.frm_DuTruThuoc frm = new frm_DuTruThuoc();
            frm.ShowDialog();
        }

        private void grvDonThuocct_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grcDonThuocct_Click(object sender, EventArgs e)
        {

        }

        private void grvDonThuocdt_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
            {
                if (grvDonThuocdt.GetRowCellValue(e.RowHandle, colIDDon) != null)
                {
                    var ploai = grvDonThuocdt.GetRowCellValue(e.RowHandle, colPLoai) != null ? grvDonThuocdt.GetRowCellValue(e.RowHandle, colPLoai).ToString() : "";
                    var mabnhanchitiet = grvDonThuocdt.GetRowCellValue(e.RowHandle, colMaBNhanChiTiet) != null ? grvDonThuocdt.GetRowCellValue(e.RowHandle, colMaBNhanChiTiet).ToString() : "";
                    if (ploai.ToString() != "Tủ trực")// lĩnh về khoa
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(mabnhanchitiet.ToString())) // bổ sung tủ trực
                        {
                            e.Appearance.ForeColor = Color.OrangeRed;
                        }
                        else // lĩnh bù tủ trực
                        {
                            e.Appearance.ForeColor = Color.Green;
                        }
                    }
                }
            }
        }

        private void grvDonThuocdt_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {

        }
    }
    public class thuoc
    {
        public DateTime? HanDung { get; set; }
        public string TenDV { get; set; }
        public string SoLo { get; set; }
        public double? SL { get; set; }
        public string DonVi { get; set; }
        public double DonGia { get; set; }
        public int? MaDV { get; set; }
    }

    public class DonThuoc
    {
        public int IDDon { get; set; }
        public DateTime? NgayKe { get; set; }
        public int? KieuDon { get; set; }
        public int? MaBNhan { get; set; }
        public IEnumerable<int> ChuaLinhs { get; set; }
        public IEnumerable<int> DaLinhs { get; set; }
        public string ChuaLinh { get; set; }
        public string DaLinh { get; set; }
        public string PLoai { get; set; }
        public int? MaBNhanChiTiet { get; set; }
    }
}
