using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QLBV.FormThamSo
{
    public partial class frm_BCnghiom : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCnghiom()
        {
            InitializeComponent();
        }

        private void frm_BCnghiom_Load(object sender, EventArgs e)
        {
            detungay.DateTime = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy" + " " + "00:00:00"));
            dedenngay.DateTime = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy" + " " + "23:59:59"));
            
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dskp = (from kp in _data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám")
                        select new
                        {
                            MaKP = kp.MaKP,
                            TenKP = kp.TenKP
                        }).OrderBy(p => p.TenKP).ToList();
            List<KPhong> _kp = new List<KPhong>();
          _kp.Add(new KPhong { TenKP = "Tất cả", MaKP = -1 });
          foreach (var item in dskp)
            {
                _kp.Add(new KPhong { TenKP = item.TenKP, MaKP = item.MaKP });

            }
            cklKP.DataSource = _kp;
            for (int i = 0; i < cklKP.ItemCount; i++)   
            {
                if (cklKP.GetItemValue(i) != null && Convert.ToInt32(cklKP.GetItemValue(i)) == DungChung.Bien.MaKP)
                    cklKP.SetItemChecked(i, false);
                else
                    cklKP.SetItemChecked(i, true);
            }
        }
        
        private void btntao_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            List<KPhong> _kpChon = new List<KPhong>();
            tungay = DungChung.Ham.NgayTu(detungay.DateTime);
            denngay = DungChung.Ham.NgayDen(dedenngay.DateTime);
            int _DT = rgDTuong.SelectedIndex;
            var _lcb = _data.CanBoes.ToList();
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                    _kpChon.Add(new KPhong { TenKP = cklKP.GetItemText(i), MaKP = cklKP.GetItemValue(i) == null ? 0 : Convert.ToInt32(cklKP.GetItemValue(i)) });
                
            }
            if(rdgchonmau.SelectedIndex==0)
            {
                var qcanbo = _data.CanBoes.ToList();
                //var _lbn = (from kb in _data.BNKBs.Where(p => p.NghiOmTu >= tungay && p.NghiOmTu <= denngay).Where(p => p.NghiOmTu != null && p.NghiOmDen != null)
                //            join bn in _data.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                //            join cb in _data.CanBoes on kb.MaCB equals cb.MaCB
                //            select new { bn.MaBNhan, bn.TenBNhan, bn.NgaySinh, bn.ThangSinh, bn.NamSinh, bn.SThe, kb.GhiChu, kb.NghiOmDen, kb.NghiOmTu, kb.SoNghiOm, cb.TenCB, kb.MaKP }).ToList();

                var _lbn0 = (from kb in _data.BNKBs.Where(p => p.NghiOmTu >= detungay.DateTime && p.NghiOmTu <= dedenngay.DateTime).Where(p => p.NghiOmTu != null && p.NghiOmDen != null)
                             join bn in _data.BenhNhans.Where(p => _DT == 2 ? true : (_DT == 0 ? p.DTuong == "BHYT" : p.DTuong == "Dịch vụ")) on kb.MaBNhan equals bn.MaBNhan 
                            join ttbx in _data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                             select new { bn.MaBNhan, bn.TenBNhan, bn.NgaySinh, bn.ThangSinh, bn.NamSinh, bn.SThe, kb.GhiChu, kb.NghiOmDen, kb.NghiOmTu, kb.SoNghiOm, kb.MaKP, kb.MaCB, ttbx.NoiLV, bn.GTinh, bn.Tuoi, kb.NguoiDaiDien, ttbx.NThan, ttbx.So_eTBM, kb.NgayChungNhanNghiOm }).ToList();

                var _lbn = (from kb in _lbn0
                            join cb in qcanbo on kb.MaCB equals cb.MaCB
                            select new { kb.MaBNhan, kb.TenBNhan, kb.NgaySinh, kb.ThangSinh, kb.NamSinh, kb.SThe, kb.GhiChu, kb.NghiOmDen, kb.NghiOmTu, kb.SoNghiOm, kb.MaKP, cb.TenCB, kb.NoiLV, kb.GTinh, kb.Tuoi, kb.NguoiDaiDien, kb.NThan, cb.MaCCHN, kb.So_eTBM, kb.NgayChungNhanNghiOm }).ToList();

                var _lbn1 = (from bn in _lbn
                             join kp in _kpChon on bn.MaKP equals kp.MaKP
                             group new { bn, kp } by new { bn.MaBNhan, bn.TenBNhan, bn.SThe, bn.GhiChu, bn.NghiOmDen, bn.NghiOmTu, bn.SoNghiOm, bn.NgaySinh, bn.NamSinh, bn.ThangSinh, bn.TenCB, bn.NoiLV, kp.TenKP, bn.GTinh, bn.Tuoi, bn.NguoiDaiDien, bn.NThan, bn.MaCCHN, bn.So_eTBM, bn.NgayChungNhanNghiOm } into kq
                             select new
                             {
                                 kq.Key.MaBNhan,
                                 kq.Key.TenBNhan,
                                 kq.Key.SThe,
                                 kq.Key.TenCB,
                                 kq.Key.TenKP,
                                 kq.Key.NguoiDaiDien,
                                 kq.Key.So_eTBM,
                                 kq.Key.GTinh,
                                 kq.Key.Tuoi,
                                 kq.Key.MaCCHN,
                                 kq.Key.NThan,
                                 kq.Key.NgayChungNhanNghiOm,
                                 NgayHen = Convert.ToDateTime(kq.Key.NghiOmDen),
                                 NgayNghi = Convert.ToDateTime(kq.Key.NghiOmTu),
                                 kq.Key.GhiChu,
                                 kq.Key.SoNghiOm,
                                 kq.Key.NoiLV,
                                 NgaySinh = kq.Key.NgaySinh.Trim() == "" ? (kq.Key.ThangSinh.Trim() == "" ? kq.Key.NamSinh : (kq.Key.ThangSinh + "/" + kq.Key.NamSinh)) : (kq.Key.NgaySinh + "/" + kq.Key.ThangSinh + "/" + kq.Key.NamSinh)
                             }).ToList();
              //  var qttbx = _data.TTboXungs.ToList();
                var _kq = (from bn in _lbn1
                          // join ttbx in qttbx on bn.MaBNhan equals ttbx.MaBNhan
                           group new { bn } by new { bn.MaBNhan, bn.TenBNhan, bn.SThe, bn.GhiChu, bn.NgayHen, bn.NgayNghi, bn.NoiLV, bn.SoNghiOm, bn.NgaySinh, bn.TenKP, bn.TenCB, bn.GTinh, bn.Tuoi, bn.NguoiDaiDien, bn.NThan, bn.MaCCHN, bn.So_eTBM, bn.NgayChungNhanNghiOm } into kq
                           select new
                           {
                               kq.Key.SoNghiOm,
                               kq.Key.MaBNhan,
                               kq.Key.TenBNhan,
                               kq.Key.NgaySinh,
                               kq.Key.SThe,
                               kq.Key.GhiChu,
                               kq.Key.NgayHen,
                               kq.Key.NgayNghi,
                               kq.Key.TenKP,
                               kq.Key.TenCB,
                               kq.Key.NguoiDaiDien,
                               kq.Key.So_eTBM,
                               kq.Key.GTinh,
                               kq.Key.Tuoi,
                               kq.Key.MaCCHN,
                               kq.Key.NThan,
                               kq.Key.NgayChungNhanNghiOm,
                               NoiLV = kq.Key.NoiLV == null ? "" : kq.Key.NoiLV,
                               songay = Math.Round(((kq.Key.NgayHen - kq.Key.NgayNghi).TotalDays + 1), MidpointRounding.AwayFromZero)
                           }).ToList();
                if (sxsoSeri.Checked == true)
                    _kq = _kq.OrderBy(p => p.SoNghiOm).ToList();
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    frmIn frm = new frmIn();
                    BaoCao.rep_BCnghiom_ngang rep = new BaoCao.rep_BCnghiom_ngang();
                    rep.pa_ngathang.Value = "Đợt từ ngày: " + detungay.DateTime.ToShortDateString() + " Đến ngày: " + dedenngay.DateTime.ToShortDateString();
                    rep.pa_tencqcq.Value = DungChung.Bien.TenCQCQ.ToUpper().ToString();
                    rep.pa_tenbv.Value = DungChung.Bien.TenCQ.ToUpper().ToString();
                    rep.pa_ngaylap.Value = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                    rep.DataSource = _kq;
                    rep.databinding();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {

                    frmIn frm = new frmIn();
                    BaoCao.rep_BCnghiom rep = new BaoCao.rep_BCnghiom();
                    rep.pa_ngathang.Value = "Đợt từ ngày: " + tungay.ToLongDateString() + " Đến ngày: " + denngay.ToLongDateString();
                    rep.pa_tencqcq.Value = DungChung.Bien.TenCQCQ.ToUpper().ToString();
                    rep.pa_tenbv.Value = DungChung.Bien.TenCQ.ToUpper().ToString();
                    rep.pa_ngaylap.Value = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                    rep.DataSource = _kq;
                    rep.databinding();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                if (Xuatex.Checked)
                {
                    List<NGHI_OM> _lkq = new List<NGHI_OM>();
                    foreach (var item in _kq)
                    {
                        NGHI_OM moi = new NGHI_OM();
                        if (item.So_eTBM.Contains("|"))
                        {
                            moi.MA_CT = item.So_eTBM.Split('|')[1];
                            moi.MA_SOBHXH = item.So_eTBM.Split('|')[0];
                        }
                        moi.MA_BV = DungChung.Bien.MaBV;
                        moi.MA_BS = item.MaCCHN;
                       
                        if (!string.IsNullOrEmpty(item.SThe))
                            moi.MA_THE = item.SThe;
                        else
                        {
                            if (item.So_eTBM.Contains("|") && item.So_eTBM.Split('|').Length > 2)
                            {
                                moi.MA_THE = item.So_eTBM.Split('|')[2];
                            }
                        }
                        moi.HO_TEN = item.TenBNhan;
                        moi.NGAY_SINH = item.NgaySinh;
                        moi.GIOI_TINH = item.GTinh == 0 ? 2 : 1;
                        moi.PP_DIEUTRI = item.GhiChu;
                        moi.TEN_BSY = item.TenCB;
                        moi.SERI = item.SoNghiOm;
                        if (item.Tuoi < 7)
                        {
                            if (item.NThan.Contains(";"))
                            {
                                string[] arr = item.NThan.Split(';');
                                if (arr[0].Contains(","))
                                {
                                    string[] arrttt = arr[0].Split(',');
                                    moi.HO_TEN_CHA = arrttt[0];
                                    moi.HO_TEN_ME = arrttt[1];
                                }
                            }
                        }
                        moi.TEN_DVI = item.NoiLV;
                        moi.TU_NGAY = item.NgayNghi.Day.ToString("D2") + "/" + item.NgayNghi.Month.ToString("D2") + "/" + item.NgayNghi.Year;//item.NgayNghi.ToShortDateString();
                        moi.DEN_NGAY = item.NgayHen.Day.ToString("D2") + "/" + item.NgayHen.Month.ToString("D2") + "/" + item.NgayHen.Year;//item.NgayHen.ToShortDateString();
                        moi.SO_NGAY = item.songay;
                        var tencb = _lcb.Where(p => p.MaCB == item.NguoiDaiDien).ToList();
                        if (tencb.Count > 0)
                            moi.NGUOI_DAI_DIEN = tencb.First().TenCB;
                        moi.NGAY_CT = item.NgayChungNhanNghiOm.Value.Day.ToString("D2") + "/" + item.NgayChungNhanNghiOm.Value.Month.ToString("D2") + "/" + item.NgayChungNhanNghiOm.Value.Year;//item.NgayChungNhanNghiOm.Value.ToShortDateString();
                        moi.SO_KCB = item.MaBNhan;
                        _lkq.Add(moi);
                    }
                    bool chonFont = false;
                    if (rdFont.SelectedIndex == 0)
                        chonFont = true;
                    else
                        chonFont = false;
                    string font = "TCVN3";
                    if (xuatExcel_ds(_lkq, txtFilePath.Text, chonFont, font))
                        MessageBox.Show("Xuất thành công!");
                    else
                        MessageBox.Show("Lỗi xuất Excel!");
                }

            }
            else
            {
                var _lbn2 = (from bn in _data.BenhNhans.Where(p => _DT == 2 ? true : (_DT == 0 ? p.DTuong == "BHYT" : p.DTuong == "Dịch vụ"))
                            join kb in _data.BNKBs.Where(p => p.NghiOmTu != null && p.NghiOmDen != null).Where(p => p.NgayChungNhanNghiOm >= tungay && p.NgayChungNhanNghiOm <= denngay) on bn.MaBNhan equals kb.MaBNhan
                            join cb in _data.CanBoes on kb.MaCB equals cb.MaCB
                            select new { bn.MaBNhan, bn.TenBNhan, bn.NgaySinh, bn.SThe, kb.GhiChu, kb.NghiOmDen, kb.NghiOmTu, kb.SoNghiOm, bn.NamSinh, bn.ThangSinh, cb.TenCB, kb.MaKP,cb.MaCCHN, bn.GTinh, bn.Tuoi, kb.NguoiDaiDien, kb.NgayChungNhanNghiOm }).ToList();
                var _lbn = (from bn in _lbn2
                             join kp in _kpChon on bn.MaKP equals kp.MaKP
                            select new { bn.MaBNhan, bn.TenBNhan, bn.NgaySinh, bn.SThe, bn.GhiChu, bn.NghiOmDen, bn.NghiOmTu, bn.SoNghiOm, bn.NamSinh, bn.ThangSinh, bn.TenCB, kp.TenKP, bn.MaCCHN, bn.GTinh, bn.Tuoi, bn.NguoiDaiDien, bn.NgayChungNhanNghiOm }).ToList();
                var _lbn1 = (from bn in _lbn
                             group bn by new { bn.MaBNhan, bn.TenBNhan, bn.SThe, bn.GhiChu, bn.NghiOmDen, bn.NghiOmTu, bn.SoNghiOm, bn.NgaySinh, bn.NamSinh, bn.ThangSinh, bn.TenCB, bn.TenKP, bn.MaCCHN, bn.GTinh, bn.Tuoi, bn.NguoiDaiDien, bn.NgayChungNhanNghiOm } into kq
                             select new
                             {
                                 kq.Key.MaBNhan,
                                 kq.Key.TenBNhan,
                                 kq.Key.SThe,
                                 kq.Key.TenCB,
                                 kq.Key.TenKP,
                                 kq.Key.MaCCHN,
                                 kq.Key.GTinh,
                                 kq.Key.Tuoi,
                                 kq.Key.NgayChungNhanNghiOm,
                                 NgayHen = Convert.ToDateTime(kq.Key.NghiOmDen),
                                 NgayNghi = Convert.ToDateTime(kq.Key.NghiOmTu),
                                 kq.Key.GhiChu,
                                 kq.Key.SoNghiOm,
                                 kq.Key.NguoiDaiDien,
                                 NgaySinh = kq.Key.NgaySinh.Trim() == "" ? (kq.Key.ThangSinh.Trim() == "" ? kq.Key.NamSinh : (kq.Key.ThangSinh + "/" + kq.Key.NamSinh)) : (kq.Key.NgaySinh + "/" + kq.Key.ThangSinh + "/" + kq.Key.NamSinh)
                             }).ToList();
                var _kq = (from bn in _lbn1
                           join ttbx in _data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                           group new { bn, ttbx } by new { bn.MaBNhan, bn.TenBNhan, bn.NgaySinh, bn.SThe, bn.GhiChu, bn.NgayHen, bn.NgayNghi, ttbx.NoiLV, bn.SoNghiOm, bn.TenKP, bn.TenCB, bn.MaCCHN, ttbx.So_eTBM, bn.GTinh, ttbx.NThan, bn.Tuoi, bn.NguoiDaiDien, bn.NgayChungNhanNghiOm } into kq
                           select new
                           {
                               kq.Key.SoNghiOm,
                               kq.Key.MaBNhan,
                               kq.Key.TenBNhan,
                               kq.Key.NgaySinh,
                               kq.Key.SThe,
                               kq.Key.GhiChu,
                               kq.Key.NgayHen,
                               kq.Key.NgayNghi,
                               kq.Key.TenKP,
                               kq.Key.TenCB,
                               kq.Key.MaCCHN,
                               kq.Key.So_eTBM,
                               kq.Key.GTinh,
                               kq.Key.NThan,
                               kq.Key.Tuoi,
                               kq.Key.NguoiDaiDien,
                               kq.Key.NgayChungNhanNghiOm,
                               songay = Math.Round(((kq.Key.NgayHen - kq.Key.NgayNghi).TotalDays + 1), MidpointRounding.AwayFromZero),
                               NoiLV = kq.Key.NoiLV == null ? "" : kq.Key.NoiLV,
                               //STT = (kq.Key.SoNghiOm == null || kq.Key.SoNghiOm == "") ? 0 : Convert.ToInt32(kq.Key.SoNghiOm)

                           }).ToList();
                if (sxsoSeri.Checked == true)
                    _kq = _kq.OrderBy(p => p.SoNghiOm).ToList();
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    frmIn frm = new frmIn();
                    BaoCao.rep_BCnghiom_ngang rep = new BaoCao.rep_BCnghiom_ngang();
                    rep.pa_ngathang.Value = "Đợt từ ngày: " + tungay.ToShortDateString() + " Đến ngày: " + denngay.ToShortDateString();
                    rep.pa_tencqcq.Value = DungChung.Bien.TenCQCQ.ToString();
                    rep.pa_tenbv.Value = DungChung.Bien.TenCQ.ToString();
                    rep.pa_ngaylap.Value = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                    rep.DataSource = _kq;
                    rep.databinding();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    frmIn frm = new frmIn();
                    BaoCao.rep_BCnghiom rep = new BaoCao.rep_BCnghiom();
                    rep.pa_ngathang.Value = "Đợt từ ngày: " + tungay.ToShortDateString() + " Đến ngày: " + denngay.ToShortDateString();
                    rep.pa_tencqcq.Value = DungChung.Bien.TenCQCQ.ToString();
                    rep.pa_tenbv.Value = DungChung.Bien.TenCQ.ToString();
                    rep.pa_ngaylap.Value = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                    rep.DataSource = _kq;
                    rep.databinding();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                if (Xuatex.Checked)
                {
                    string bnloi = "";
                    List<NGHI_OM> _lkq = new List<NGHI_OM>();
                    foreach (var item in _kq)
                    {
                        NGHI_OM moi = new NGHI_OM();
                        if (item.So_eTBM.Contains("|"))
                        {
                            moi.MA_CT = item.So_eTBM.Split('|')[1];
                            moi.MA_SOBHXH = item.So_eTBM.Split('|')[0];
                        }
                        moi.MA_BV = DungChung.Bien.MaBV;
                        moi.MA_BS = item.MaCCHN;

                        if (!string.IsNullOrEmpty(item.SThe))
                            moi.MA_THE = item.SThe;
                        else
                        {
                            if (item.So_eTBM.Contains("|") && item.So_eTBM.Split('|').Length > 2)
                            {
                                moi.MA_THE = item.So_eTBM.Split('|')[2];
                            }
                        }
                        moi.HO_TEN = item.TenBNhan;
                        moi.NGAY_SINH = item.NgaySinh;
                        moi.GIOI_TINH = item.GTinh == 0 ? 2 : 1;
                        moi.PP_DIEUTRI = item.GhiChu;
                        moi.TEN_BSY = item.TenCB;
                        moi.SERI = item.SoNghiOm;
                        if (item.Tuoi < 7)
                        {
                            string[] arr = item.NThan.Split(';');
                            if (arr[0].Contains(","))
                            {
                                string[] arrttt = arr[0].Split(',');
                                moi.HO_TEN_CHA = arrttt[0];
                                moi.HO_TEN_ME = arrttt[1];
                            }
                        }
                        moi.TEN_DVI = item.NoiLV;
                        moi.TU_NGAY = item.NgayNghi.Day.ToString("D2") + "/" + item.NgayNghi.Month.ToString("D2") + "/" + item.NgayNghi.Year;//item.NgayNghi.ToShortDateString();
                        moi.DEN_NGAY = item.NgayHen.Day.ToString("D2") + "/" + item.NgayHen.Month.ToString("D2") + "/" + item.NgayHen.Year;//item.NgayHen.ToShortDateString();
                        moi.SO_NGAY = item.songay;
                        var tencb = _lcb.Where(p => p.MaCB == item.NguoiDaiDien).ToList();
                        if (tencb.Count > 0)
                            moi.NGUOI_DAI_DIEN = tencb.First().TenCB;
                        //moi.NGUOI_DAI_DIEN = item.NguoiDaiDien;
                        moi.NGAY_CT = item.NgayChungNhanNghiOm.Value.Day.ToString("D2") + "/" + item.NgayChungNhanNghiOm.Value.Month.ToString("D2") + "/" + item.NgayChungNhanNghiOm.Value.Year;//item.NgayChungNhanNghiOm.Value.ToShortDateString();
                        moi.SO_KCB = item.MaBNhan;
                        _lkq.Add(moi);
                    }
                    bool chonFont = false;
                    if (rdFont.SelectedIndex == 0)
                        chonFont = true;
                    else
                        chonFont = false;
                    string font = "TCVN3";
                    if (xuatExcel_ds(_lkq, txtFilePath.Text, chonFont, font))
                        MessageBox.Show("Xuất thành công!");
                    else
                        MessageBox.Show("Lỗi xuất Excel!");
                }
                
                  //_lkq=from a in _kq
                  //     select new NGHI_OM
                  //     {
                  //         MA_CT=a.SoNghiOm,
                  //         MA_BV=DungChung.Bien.MaBV,
                  //         MA_BS=a.MaCCHN,
                  //         MA_SOBHXH=a.So_eTBM,
                  //         MA_THE=a.SThe,
                  //         HO_TEN=a.TenBNhan,
                  //         NGAY_SINH=a.NgaySinh,
                  //         GIOI_TINH=a.GTinh??0,
                  //         HO_TEN_CHA=
                  //     }
              //}
            //  }
            //}

            }
        }

        public class NGHI_OM
        {
            public string MA_CT { get; set; }
            public string MA_BV { get; set; }
            public string MA_BS { get; set; }
            public string MA_SOBHXH { get; set; }
            public string MA_THE { get; set; }
            public string HO_TEN { get; set; }
            public string NGAY_SINH { get; set; }
            public int GIOI_TINH { get; set; }
            public string HO_TEN_CHA { get; set; }
            public string HO_TEN_ME { get; set; }
            public string PP_DIEUTRI { get; set; }
            public string MA_DVI { get; set; }
            public string TEN_DVI { get; set; }
            public string TU_NGAY { get; set; }
            public string DEN_NGAY { get; set; }
            public double SO_NGAY { get; set; }
            public string NGAY_CT { get; set; }
            public string NGUOI_DAI_DIEN { get; set; }
            public int SO_KCB { get; set; }
            public string TEN_BSY { get; set; }
            public string SERI { get; set; }
        }

        public static bool xuatExcel_ds(List<NGHI_OM> _lkq, string path, bool convert, string font)
        {
            bool rs = false;
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> _lKP = _dataContext.KPhongs.ToList();
            // gọi ứng dụng excel--------------------------------------------------------------------------------------------------

            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);
            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];

            exSheet.Name = "Danh sách bệnh nhân nghỉ ốm";

            // mảng tên cột--------------------------------------------------------------------------------------------------------
            string[] _arr = new string[] { "STT", "MA_CT", "SO_KCB", "MA_BV", "MA_BS", "MA_SOBHXH", "MA_THE", "HO_TEN", "NGAY_SINH", "GIOI_TINH", "PP_DIEUTRI", "MA_DVI", "TEN_DVI", "TU_NGAY", "DEN_NGAY", "SO_NGAY", "HO_TEN_CHA", "HO_TEN_ME", "NGAY_CT", "NGUOI_DAI_DIEN", "TEN_BSY", "SERI","MAU_SO" };

            // add tên cột vào sheet exSheet---------------------------------------------------------------------------------------
            int k = 0;
            foreach (var b in _arr)
            {
                k++;
                COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                r.Value2 = DungChung.Ham.convertFont(convert, b.ToString(), font);
                r.Columns.AutoFit();
                r.Cells.Font.Bold = true;
            }

            // đổ dữ liệu từ list vào mảng 2 chiều---------------------------------------------------------------------------------
            int row = _lkq.Count;
            int col = _arr.Length;
            Object[,] _arr2 = new Object[row, col]; // tạo ra mảng count, col phần tử
            if (row > 0)
            {
                int num = 0;
                int stt = 1;
                foreach (var item in _lkq)
                {
                    _arr2[num, 0] = stt;
                    _arr2[num, 1] = item.MA_CT;
                    _arr2[num, 2] = item.SO_KCB;
                    _arr2[num, 3] = "'" + item.MA_BV;
                    _arr2[num, 4] = item.MA_BS;
                    _arr2[num, 5] = "'" + item.MA_SOBHXH;
                    _arr2[num, 6] = item.MA_THE;
                    _arr2[num, 7] = item.HO_TEN;

                    _arr2[num, 8] = "'" + item.NGAY_SINH;

                    _arr2[num, 9] = item.GIOI_TINH;
                    _arr2[num, 10] = item.PP_DIEUTRI;
                    _arr2[num, 11] = "1";
                    _arr2[num, 12] = item.TEN_DVI;
                    _arr2[num, 13] = "'" + item.TU_NGAY;
                    _arr2[num, 14] = "'" + item.DEN_NGAY;
                    _arr2[num, 15] = item.SO_NGAY;
                    _arr2[num, 16] = item.HO_TEN_CHA;
                    _arr2[num, 17] = item.HO_TEN_ME;
                    _arr2[num, 18] = "'" + item.NGAY_CT;
                    _arr2[num, 19] = item.NGUOI_DAI_DIEN;
                    _arr2[num, 20] = item.TEN_BSY;
                    _arr2[num, 21] = "'" + item.SERI;
                    _arr2[num, 22] = "CT07";
                    stt++;
                    num++;
                }
                // chú ý: định dạng cho ngày ra ngày vào, nếu thay đổi thứ tự cột phải set lại thứ tự range format
                //exSheet.Range[exSheet.Cells[2, 2], exSheet.Cells[row + 1, 2]].NumberFormat = "@";// ma_dkbd
                //exSheet.Range[exSheet.Cells[2, 3], exSheet.Cells[row + 1, 3]].NumberFormat = "@";
                //exSheet.Range[exSheet.Cells[2, 16], exSheet.Cells[row + 1, 16]].NumberFormat = "@";
                //exSheet.Range[exSheet.Cells[2, 17], exSheet.Cells[row + 1, 17]].NumberFormat = "@";
                //exSheet.Range[exSheet.Cells[2, 19], exSheet.Cells[row + 1, 19]].NumberFormat = "@";

                //exSheet.Range[exSheet.Cells[2, 13], exSheet.Cells[row + 1, 13]].NumberFormat = "dd/MM/yyyy";
                //exSheet.Range[exSheet.Cells[2, 14], exSheet.Cells[row + 1, 14]].NumberFormat = "dd/MM/yyyy";
                //exSheet.Range[exSheet.Cells[2, 18], exSheet.Cells[row + 1, 18]].NumberFormat = "dd/MM/yyyy";
                //exSheet.Range[exSheet.Cells[2, 25], exSheet.Cells[row + 1, 25]].NumberFormat = "dd/MM/yyyy";
                //exSheet.Range[exSheet.Cells[2, 26], exSheet.Cells[row + 1, 26]].NumberFormat = "dd/MM/yyyy";

                //exSheet.Range[exSheet.Cells[2, 31], exSheet.Cells[row + 1, 31]].NumberFormat = "@";
                //exSheet.Range[exSheet.Cells[2, 32], exSheet.Cells[row + 1, 32]].NumberFormat = "@";
                //exSheet.Range[exSheet.Cells[2, 33], exSheet.Cells[row + 1, 33]].NumberFormat = "@";
                //exSheet.Range[exSheet.Cells[2, 33], exSheet.Cells[row + 1, 55]].NumberFormat = "@";
                //exSheet.Range[exSheet.Cells[2, 33], exSheet.Cells[row + 1, 56]].NumberFormat = "@";


                //-------------------------------------------------------------------------------------------
                exSheet.Range[exSheet.Cells[2, 1], exSheet.Cells[row + 1, col]].Value = _arr2;
                exApp.Visible = true;
                try
                {

                    exQLBV.SaveAs(path, COMExcel.XlFileFormat.xlWorkbookNormal,
                                    null, null, false, false,
                                    COMExcel.XlSaveAsAccessMode.xlExclusive,
                                    false, false, false, false, false);
                    rs = true;
                }
                catch (Exception ex)
                {
                    rs = false;
                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                }

            }

            return rs;
        }
        private void btnhuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cklKP_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {

                if (cklKP.GetItemChecked(0))
                {

                    cklKP.CheckAll();

                }
                else
                {

                    cklKP.UnCheckAll();

                }
            }

        }

        private void Xuatex_CheckedChanged(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            // txtFilePath.Enabled = Xuatex.Checked;
            btnChonFilePath.Enabled = Xuatex.Checked;
            if (Xuatex.Checked)
                txtFilePath.Text = "C:\\DanhSachBNXacNhanNghiOm_" + DungChung.Bien.MaBV + "_" + _ngay.Year + "_" + _ngay.Month + ".xls";
            else
                txtFilePath.Text = "";
        }
        SaveFileDialog dialog = new SaveFileDialog();
        private void btnChonFilePath_Click(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            dialog.InitialDirectory = "C:\\";
            dialog.Filter = "Excel files (*.xls or *.xlsx)|*.xls;*.xlsx";
            dialog.FilterIndex = 1;
            dialog.FileName = "DanhSachBNXacNhanNghiOm_" + DungChung.Bien.MaBV + "_" + _ngay.Year + "_" + _ngay.Month + ".xls";
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dialog.FileName;
            }
        }
    }
}