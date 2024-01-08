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
    public partial class frm_ChamCongPhauThuat : DevExpress.XtraEditors.XtraForm
    {

        QLBV_Database.QLBVEntities _datacontext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DichVu> _lDichVu = new List<DichVu>();
        List<KPhong> _lKP = new List<KPhong>();
        List<ThanhToan> _lThanhToan = new List<ThanhToan>();
        List<CanBo> _lCanBo = new List<CanBo>();

        public frm_ChamCongPhauThuat()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            int tltt1 = 0;
            if (tltt.Text != "Tất cả" && tltt.Text != "")
                tltt1 = Convert.ToInt32(tltt.Text);
            if (kt())
            {   //nhap ngay;
                DateTime tungay = DungChung.Ham.NgayTu(deNgayTu.DateTime);
                DateTime denngay = DungChung.Ham.NgayDen(deNgayDen.DateTime);

                List<int> _dsLoaiPT = new List<int>();
                //0: loại đặc biệt, 1: loại I, 2: loại II, 3: loại III
                foreach (var item in checkedListBoxControl1.CheckedItems)
                {
                    LoaiPTTT a = (LoaiPTTT)item;
                    _dsLoaiPT.Add(a.Loai);
                }
                string macb = "";
                if (lupTenCB.EditValue != null)
                    macb = lupTenCB.EditValue.ToString();
                int nhomdichvu = Convert.ToInt32(lupNhomDichVu.EditValue);
                
                int thanhtoan = Convert.ToInt32(lupTT.EditValue); //0:Chưa thanh toán. 1: đã thanh toán , 2: tất cả
                if (lupNhomDichVu.GetColumnValue("TenRG").ToString().ToLower().Contains("phẫu thuật"))// Phẫu Thuật
                {
                    //var _ldv = (from dv in _datacontext.DichVus
                    //            join tn in _datacontext.TieuNhomDVs.Where(p => p.IdTieuNhom == nhomdichvu) on dv.IdTieuNhom equals tn.IdTieuNhom
                    //            select new
                    //            {
                    //                dv.MaDV,
                    //                dv.Loai,
                    //                dv.TenDV
                    //            }).ToList();
                    //var _ldt = (from dt in _datacontext.DThuocs
                    //            join dtct in _datacontext.DThuoccts.Where(p => macb == "" ? true : p.MaCB == macb).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on dt.IDDon equals dtct.IDDon
                    //            join kb in _datacontext.BNKBs on dtct.IDKB equals kb.IDKB into kqua
                    //            from kq2 in kqua.DefaultIfEmpty()
                    //            select new
                    //            {
                    //                dtct.MaDV,
                    //                dt.MaBNhan,
                    //                dtct.MaCB,
                    //                dtct.NgayNhap,
                    //                dtct.TrongBH,
                    //                dtct.DSCBTH,
                    //                dtct.MaKP,
                    //                ChanDoan = kq2 == null ? "" : kq2.ChanDoan,
                    //                Benhkhac = kq2 == null ? "" : kq2.BenhKhac
                    //            }).ToList();
                    List<BenhNhan> _bn=_datacontext.BenhNhans.ToList();
                    
                        var kq = (from bn in _datacontext.BenhNhans
                                  join dt in _datacontext.DThuocs on bn.MaBNhan equals dt.MaBNhan
                                  join dtct in _datacontext.DThuoccts.Where(p => macb == "" ? true : p.MaCB == macb).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => tltt1 == 0 || p.TyLeTT == tltt1) on dt.IDDon equals dtct.IDDon
                                  join cb in _datacontext.CanBoes on dtct.MaCB equals cb.MaCB into kqCB
                                  from kq1 in kqCB.DefaultIfEmpty()
                                  join kb in _datacontext.BNKBs on dtct.IDKB equals kb.IDKB into kqua
                                  from kq2 in kqua.DefaultIfEmpty()
                                  join dv in _datacontext.DichVus on dtct.MaDV equals dv.MaDV
                                  join tn in _datacontext.TieuNhomDVs.Where(p => p.IdTieuNhom == nhomdichvu) on dv.IdTieuNhom equals tn.IdTieuNhom
                                  select new { bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, SThe = bn.SThe == null ? "" : bn.SThe, dv.MaDV, dv.TenDV, dtct.NgayNhap, TenCB = kq1 == null ? "" : kq1.TenCB, dtct.TrongBH, dtct.DSCBTH, dtct.MaKP, dv.Loai, ChanDoan = kq2 == null ? "" : kq2.ChanDoan }).ToList();
  
                    if (_dsLoaiPT.Count > 0)
                    {
                        kq = (from lck in _dsLoaiPT
                              join k in kq on lck equals k.Loai
                              select k).ToList();
                    }
                    if (lupDichVu.EditValue != null && lupDichVu.EditValue.ToString() != "0")
                    {
                        kq = kq.Where(p => p.MaDV == Convert.ToInt32(lupDichVu.EditValue)).ToList();
                    }
                    if (lupKPhong.EditValue != null && lupKPhong.EditValue.ToString() != "0")
                    {
                        kq = kq.Where(p => p.MaKP == Convert.ToInt32(lupKPhong.EditValue)).ToList();
                    }
                    #region BV 01830 _Thêm cột phẫu thuật viên phụ 1,2,3
                    if (rgbc.SelectedIndex == 2)
                    {
                        BaoCao.rep_ChamCongPhauThuat_01830 rep = new BaoCao.rep_ChamCongPhauThuat_01830();
                        if (chkDT.Checked == true) { rep.DeTrang.Value = 1; }
                        rep.paramTenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                        if (lupKPhong.EditValue != null && lupKPhong.EditValue.ToString() != "0" && lupKPhong.GetColumnValue("TenKP") != null && lupKPhong.GetColumnValue("TenKP").ToString() != "0")
                        {
                            rep.paramKPhong.Value = lupKPhong.GetColumnValue("TenKP").ToString().ToUpper();
                        }
                        rep.paramTuNgayDenNgay.Value = "Từ " + DungChung.Ham.NgaySangChu(tungay) + " đến " + DungChung.Ham.NgaySangChu(denngay);
                        if (thanhtoan == 0)
                        {
                            var kq1 = (
                                from k in kq
                                where !(from vp in _datacontext.VienPhis select vp.MaBNhan).Contains(k.MaBNhan)
                                select new { k.TenBNhan, k.Tuoi, k.DChi, Sthe = k.SThe == null ? "" : k.SThe, k.TenDV, k.NgayNhap, k.TenCB, k.TrongBH, k.DSCBTH, k.Loai }).OrderBy(n => n.NgayNhap).ToList();
                            rep.DataSource = kq1;
                            rep.celTS1.Text = kq1.Where(p => p.Loai == 1).Count().ToString();
                            rep.celTS2.Text = kq1.Where(p => p.Loai == 2).Count().ToString();
                        }
                        if (thanhtoan == 1)
                        {
                            var kq2 = (
                                from k in kq
                                join vp in _datacontext.VienPhis on k.MaBNhan equals vp.MaBNhan
                                select new { k.TenBNhan, k.Tuoi, k.DChi, Sthe = k.SThe == null ? "" : k.SThe, k.TenDV, k.NgayNhap, k.TenCB, k.TrongBH, k.DSCBTH, k.Loai }).OrderBy(n => n.NgayNhap).ToList();
                            rep.DataSource = kq2;
                            rep.celTS1.Text = kq2.Where(p => p.Loai == 1).Count().ToString();
                            rep.celTS2.Text = kq2.Where(p => p.Loai == 2).Count().ToString();
                        }
                        if (thanhtoan == 2)
                        {
                            rep.DataSource = kq.OrderBy(p => p.NgayNhap).ToList();
                            rep.celTS1.Text = kq.Where(p => p.Loai == 1).Count().ToString();
                            rep.celTS2.Text = kq.Where(p => p.Loai == 2).Count().ToString();
                        }
                        rep.BindingData();
                        rep.CreateDocument();
                        frmIn frm1 = new frmIn();
                        frm1.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm1.ShowDialog();
                    }
                    #endregion
                    #region BV khác
                    if (rgbc.SelectedIndex != 2)
                    {
                        BaoCao.rep_ChamCongPhauThuat rep = new BaoCao.rep_ChamCongPhauThuat();
                        if (chkDT.Checked == true) { rep.DeTrang.Value = 1; }
                        rep.paramTenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                        if (lupKPhong.EditValue != null && lupKPhong.EditValue.ToString() != "0" && lupKPhong.GetColumnValue("TenKP") != null && lupKPhong.GetColumnValue("TenKP").ToString() != "0")
                        {
                            rep.paramKPhong.Value = lupKPhong.GetColumnValue("TenKP").ToString().ToUpper();
                        }
                        rep.paramTuNgayDenNgay.Value = "Từ " + DungChung.Ham.NgaySangChu(tungay) + " đến " + DungChung.Ham.NgaySangChu(denngay);
                        if (thanhtoan == 0)
                        {
                            var kq1 = (
                                from k in kq
                                where !(from vp in _datacontext.VienPhis select vp.MaBNhan).Contains(k.MaBNhan)
                                select new { k.TenBNhan, k.Tuoi, k.DChi, Sthe = k.SThe == null ? "" : k.SThe, k.TenDV, k.NgayNhap, k.TenCB, k.TrongBH, k.DSCBTH, k.Loai }).OrderBy(n => n.NgayNhap).ToList();
                            rep.DataSource = kq1;
                            rep.celTSKPL.Text = kq1.Where(p => p.Loai == -1).Count().ToString();
                            rep.celTSDB.Text = kq1.Where(p => p.Loai == 0).Count().ToString();
                            rep.celTS1.Text = kq1.Where(p => p.Loai == 1).Count().ToString();
                            rep.celTS2.Text = kq1.Where(p => p.Loai == 2).Count().ToString();
                            rep.celTS3.Text = kq1.Where(p => p.Loai == 3).Count().ToString();

                        }
                        if (thanhtoan == 1)
                        {
                            var kq2 = (
                                from k in kq
                                join vp in _datacontext.VienPhis on k.MaBNhan equals vp.MaBNhan
                                select new { k.TenBNhan, k.Tuoi, k.DChi, Sthe = k.SThe == null ? "" : k.SThe, k.TenDV, k.NgayNhap, k.TenCB, k.TrongBH, k.DSCBTH, k.Loai }).OrderBy(n => n.NgayNhap).ToList();
                            rep.DataSource = kq2;
                            rep.celTSKPL.Text = kq2.Where(p => p.Loai == -1).Count().ToString();
                            rep.celTSDB.Text = kq2.Where(p => p.Loai == 0).Count().ToString();
                            rep.celTS1.Text = kq2.Where(p => p.Loai == 1).Count().ToString();
                            rep.celTS2.Text = kq2.Where(p => p.Loai == 2).Count().ToString();
                            rep.celTS3.Text = kq2.Where(p => p.Loai == 3).Count().ToString();
                        }
                        if (thanhtoan == 2)
                        {
                            rep.DataSource = kq.OrderBy(p => p.NgayNhap).ToList();
                            rep.celTSKPL.Text = kq.Where(p => p.Loai == -1).Count().ToString();
                            rep.celTSDB.Text = kq.Where(p => p.Loai == 0).Count().ToString();
                            rep.celTS1.Text = kq.Where(p => p.Loai == 1).Count().ToString();
                            rep.celTS2.Text = kq.Where(p => p.Loai == 2).Count().ToString();
                            rep.celTS3.Text = kq.Where(p => p.Loai == 3).Count().ToString();
                        }
                        #region xuất Excel
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        int[] _arrWidth = new int[] { };
                        // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                        int num = 1;
                        DungChung.Bien.MangHaiChieu = new Object[kq.Count + 1, 20];
                        string[] _tieude = { "STT", "Tên người bệnh", "Tuổi", "Địa chỉ", "Số thẻ BHYT", "Tên DV", "Ngày làm", "PTV chính", "PTV phụ", "GM chính", "GM phụ", "giúp việc", "Loại" };
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
                        }

                        //for (int i = 0; i <= 17; i++) {
                        //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                        //}
                        foreach (var r in kq)
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.DChi;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.SThe;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.TenDV;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.NgayNhap;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.TenCB;
                            DungChung.Bien.MangHaiChieu[num, 8] = "";
                            DungChung.Bien.MangHaiChieu[num, 9] = "";
                            //if (DungChung.Bien.MaBV == "12001")
                            //    DungChung.Bien.MangHaiChieu[num, 9] = r.GMChinh;
                            DungChung.Bien.MangHaiChieu[num, 10] = "";
                            DungChung.Bien.MangHaiChieu[num, 11] = "";
                            DungChung.Bien.MangHaiChieu[num, 12] = r.Loai;
                            num++;
                        }
                        //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo cham cong PTTT", "C:\\BC_ChamCongPTTT.xls", true);
                        #endregion
                        frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo cham cong PTTT", "C:\\BC_ChamCongPTTT.xls", false, this.Name);
                        rep.BindingData();
                        rep.CreateDocument();
                        frm1.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm1.ShowDialog();
                    }
                    #endregion
                }
                else
                {
                    #region BV_27022 
                        if(rgbc.SelectedIndex == 1)
                    {
                        BaoCao.rep_ThanhToanThuThuat_27022 rep = new BaoCao.rep_ThanhToanThuThuat_27022();
                        rep.paramTenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                        if (chkDT.Checked == true) { rep.DeTrang.Value = 1; }
                        if (lupKPhong.EditValue != null && lupKPhong.EditValue.ToString() != "0" && lupKPhong.GetColumnValue("TenKP") != null && lupKPhong.GetColumnValue("TenKP").ToString() != "0")
                        {
                            rep.paramKPhong.Value = lupKPhong.GetColumnValue("TenKP").ToString().ToUpper();
                        }
                        rep.paramTuNgayDenNgay.Value = "Từ " + DungChung.Ham.NgaySangChu(tungay) + " đến " + DungChung.Ham.NgaySangChu(denngay);
                        //var kq0 = (from  dtct in _datacontext.DThuoccts.Where(p => macb == "" ? true : p.MaCB == macb).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                        //          join kb in _datacontext.BNKBs on dtct.IDKB equals kb.IDKB into kqua from kq1  in kqua.DefaultIfEmpty()
                        //           join cb in _datacontext.CanBoes on dtct.MaCB equals cb.MaCB into kqCB
                        //           from kq2 in kqCB.DefaultIfEmpty()
                        //           select new { dtct.MaDV, dtct.IDDon, dtct.NgayNhap, dtct.TrongBH, dtct.DSCBTH, dtct.MaKP, ChanDoan = kq1 == null ? "" : kq1.ChanDoan, TenCB = kq2 == null ? "" : kq2.TenCB }).ToList();

                        var kq3 = (from bn in _datacontext.BenhNhans
                                  join dt in _datacontext.DThuocs on bn.MaBNhan equals dt.MaBNhan
                                   join dtct in _datacontext.DThuoccts.Where(p => macb == "" ? true : p.MaCB == macb).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => tltt1 == 0 || p.TyLeTT == tltt1) on dt.IDDon equals dtct.IDDon
                                  join cb in _datacontext.CanBoes on dtct.MaCB equals cb.MaCB into kqCB
                                  from kq1 in kqCB.DefaultIfEmpty()
                                  join kb in _datacontext.BNKBs on dtct.IDKB equals kb.IDKB into kqua
                                  from kq2 in kqua.DefaultIfEmpty()
                                  join dv in _datacontext.DichVus on dtct.MaDV equals dv.MaDV
                                  join tn in _datacontext.TieuNhomDVs.Where(p => p.IdTieuNhom == nhomdichvu) on dv.IdTieuNhom equals tn.IdTieuNhom
                                  select new { bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, SThe = bn.SThe == null ? "" : bn.SThe, dv.MaDV, dv.TenDV, dtct.NgayNhap, ChanDoan = kq2 == null ? "" : kq2.ChanDoan, TenCB = kq1 == null ? "" : kq1.TenCB, dtct.TrongBH, dtct.DSCBTH, dtct.MaKP, dv.Loai }).ToList();

                        var kq = (from a in kq3
                                   join b in _datacontext.BNKBs on a.MaBNhan equals b.MaBNhan
                                   where(a.MaKP == b.MaKP)
                                   select new
                                   {
                                       a.MaBNhan,
                                       a.TenBNhan,
                                       a.Tuoi,
                                       a.DChi,
                                       a.SThe,
                                       a.MaDV,
                                       a.TenDV,
                                       a.NgayNhap,
                                       ChanDoan = (a.ChanDoan != "") ? a.ChanDoan : b.ChanDoan,
                                       a.TenCB,
                                       a.TrongBH,
                                       a.DSCBTH,
                                       a.MaKP,
                                       a.Loai,
                                   }).ToList();

                            if (_dsLoaiPT.Count > 0)
                        {
                            kq = (from lck in _dsLoaiPT
                                  join k in kq on lck equals k.Loai
                                  select k).ToList();
                        }
                        if (lupDichVu.EditValue != null && lupDichVu.EditValue.ToString() != "0")
                        {
                            kq = kq.Where(p => p.MaDV == Convert.ToInt32(lupDichVu.EditValue)).ToList();
                        }
                        if (lupKPhong.EditValue != null && lupKPhong.EditValue.ToString() != "0")
                        {
                            kq = kq.Where(p => p.MaKP == Convert.ToInt32(lupKPhong.EditValue)).ToList();
                        }
                        if (thanhtoan == 0)
                        {
                            var kq1 = (
                                from k in kq
                                where !(from vp in _datacontext.VienPhis select vp.MaBNhan).Contains(k.MaBNhan)
                                select new { k.TenBNhan, k.Tuoi, k.DChi, Sthe = k.SThe == null ? "" : k.SThe, k.TenDV, k.NgayNhap, k.TenCB, k.TrongBH, k.DSCBTH, k.Loai }).OrderBy(n => n.NgayNhap).ToList();
                            rep.DataSource = kq1;
                            rep.celTS1.Text = kq1.Where(p => p.Loai == 1).Count().ToString();
                            rep.celTS2.Text = kq1.Where(p => p.Loai == 2).Count().ToString();
                        }
                        if (thanhtoan == 1)
                        {
                            var kq2 = (
                                from k in kq
                                join vp in _datacontext.VienPhis on k.MaBNhan equals vp.MaBNhan
                                select new { k.TenBNhan, k.Tuoi, k.DChi, Sthe = k.SThe == null ? "" : k.SThe, k.TenDV, k.NgayNhap, k.TenCB, k.TrongBH, k.DSCBTH, k.Loai }).OrderBy(n => n.NgayNhap).ToList();
                            rep.DataSource = kq2;
                            rep.celTS1.Text = kq2.Where(p => p.Loai == 1).Count().ToString();
                            rep.celTS2.Text = kq2.Where(p => p.Loai == 2).Count().ToString();
                        }
                        if (thanhtoan == 2)
                        {
                            rep.DataSource = kq.OrderBy(p => p.NgayNhap).ToList();
                            rep.celTS1.Text = kq.Where(p => p.Loai == 1).Count().ToString();
                            rep.celTS2.Text = kq.Where(p => p.Loai == 2).Count().ToString();
                        }

                        #region xuất Excel
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        int[] _arrWidth = new int[] { };
                        // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                        int num = 1;
                        DungChung.Bien.MangHaiChieu = new Object[kq.Count + 1, 20];
                        string[] _tieude = { "STT", "Tên người bệnh", "Tuổi", "Địa chỉ", "Số thẻ BHYT", "Tên DV", "Ngày làm", "PTV chính", "PTV phụ", "GM chính", "GM phụ", "giúp việc", "Loại" };
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
                        }

                        //for (int i = 0; i <= 17; i++) {
                        //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                        //}
                        foreach (var r in kq)
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.DChi;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.SThe;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.TenDV;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.NgayNhap;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.TenCB;
                            DungChung.Bien.MangHaiChieu[num, 8] = "";
                            DungChung.Bien.MangHaiChieu[num, 9] = "";
                            DungChung.Bien.MangHaiChieu[num, 10] = "";
                            DungChung.Bien.MangHaiChieu[num, 11] = "";
                            DungChung.Bien.MangHaiChieu[num, 12] = r.Loai;
                            num++;
                        }
                        //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo cham cong PTTT", "C:\\BC_ChamCongPTTT.xls", true);
                        #endregion

                        rep.BindingData();
                        rep.CreateDocument();
                        frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo cham cong PTTT", "C:\\BC_ChamCongPTTT.xls", false, this.Name);
                        frm1.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm1.ShowDialog();
                    }
                    #endregion
                        #region BV:26007,Thêm vtyt, thuốc đính kèm
                        else if (rgbc.SelectedIndex == 3)
                        {
                            var benhnhan = (from bn in _datacontext.BenhNhans
                                      join dt in _datacontext.DThuocs on bn.MaBNhan equals dt.MaBNhan
                                      join dtct in _datacontext.DThuoccts.Where(p => macb == "" ? true : p.MaCB == macb).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on dt.IDDon equals dtct.IDDon
                                      join cb in _datacontext.CanBoes on dtct.MaCB equals cb.MaCB into kqCB
                                      from kq1 in kqCB.DefaultIfEmpty()
                                      join kb in _datacontext.BNKBs on dtct.IDKB equals kb.IDKB into kqua
                                      from kq2 in kqua.DefaultIfEmpty()
                                      join dv in _datacontext.DichVus on dtct.MaDV equals dv.MaDV
                                      join tn in _datacontext.TieuNhomDVs.Where(p => p.IdTieuNhom == nhomdichvu) on dv.IdTieuNhom equals tn.IdTieuNhom
                                      select new {
                                          Nam = bn.GTinh == 0 ? null : bn.Tuoi,
                                          Nu = bn.GTinh == 0 ? bn.Tuoi : null,
                                          dtct.ThanhTien,
                                          dtct.IDDonct,
                                          bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, SThe = bn.SThe == null ? "" : bn.SThe, dv.MaDV, dv.TenDV, dtct.NgayNhap, TenCB = kq1 == null ? "" : kq1.TenCB, dtct.TrongBH, dtct.DSCBTH, dtct.MaKP, dv.Loai, ChanDoan = kq2 == null ? "" : kq2.ChanDoan }).ToList();
                            var vtyt = (from dtct in _datacontext.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p=>p.AttachIDDonct !=null)
                                        join dv in _datacontext.DichVus on dtct.MaDV equals dv.MaDV
                                        join tn in _datacontext.TieuNhomDVs.Where(p => p.IDNhom == 10 || p.IDNhom == 4) on dv.IdTieuNhom equals tn.IdTieuNhom
                                        select new
                                        {
                                            dtct.AttachIDDonct,
                                            tn.IDNhom,
                                            dtct.MaDV,
                                            dtct.ThanhTien
                                        }).ToList();
                            var kqvtyt = (from vt in vtyt
                                          group vt by new { vt.AttachIDDonct, vt.IDNhom } into qk
                                          select new
                                          {
                                              qk.Key.IDNhom,
                                              qk.Key.AttachIDDonct,
                                              VTYT = qk.Where(p => p.IDNhom == 10).Sum(p => p.ThanhTien),
                                              THUOC = qk.Where(p => p.IDNhom == 4).Sum(p => p.ThanhTien)
                                          }).ToList();
                            var kqt = (from k in benhnhan
                                       join vt in kqvtyt on k.IDDonct equals vt.AttachIDDonct into kquavtyt
                                       from vt1 in kquavtyt.DefaultIfEmpty()
                                       group new {k,vt1} by k into kqs
                                       select new
                                       {
                                           kqs.Key.Nam,
                                           kqs.Key.Nu,
                                           kqs.Key.ThanhTien,
                                           kqs.Key.MaBNhan,
                                           kqs.Key.TenBNhan,
                                           kqs.Key.Tuoi,
                                           kqs.Key.DChi,
                                           kqs.Key.SThe,
                                           kqs.Key.MaDV,
                                           kqs.Key.TenDV,
                                           kqs.Key.NgayNhap,
                                           kqs.Key.TenCB,
                                           kqs.Key.TrongBH,
                                           kqs.Key.DSCBTH,
                                           kqs.Key.MaKP,
                                           kqs.Key.Loai,
                                           kqs.Key.ChanDoan,
                                           VTYT =kqs.Sum(p=>p.vt1 == null ? 0 : p.vt1.VTYT),
                                           THUOC = kqs.Sum(p => p.vt1 == null ? 0 : p.vt1.THUOC)
                                       }).ToList();
                            var kq = (from k in kqt
                                      group k by new
                                      {
                                          k.Nam,
                                          k.Nu,
                                          k.ThanhTien,
                                          k.MaBNhan,
                                          k.TenBNhan,
                                          k.Tuoi,
                                          k.DChi,
                                          k.SThe,
                                          k.MaDV,
                                          k.TenDV,
                                          k.NgayNhap,
                                          k.TenCB,
                                          k.TrongBH,
                                          k.DSCBTH,
                                          k.MaKP,
                                          k.Loai,
                                          k.ChanDoan,
                                      } into qk
                                      select new
                                      {
                                          qk.Key.Nam,
                                          qk.Key.Nu,
                                          qk.Key.ThanhTien,
                                          qk.Key.MaBNhan,
                                          qk.Key.TenBNhan,
                                          qk.Key.Tuoi,
                                          qk.Key.DChi,
                                          qk.Key.SThe,
                                          qk.Key.MaDV,
                                          qk.Key.TenDV,
                                          qk.Key.NgayNhap,
                                          qk.Key.TenCB,
                                          TrongBH = qk.Key.TrongBH,
                                          qk.Key.DSCBTH,
                                          qk.Key.MaKP,
                                          qk.Key.Loai,
                                          qk.Key.ChanDoan,
                                          VTYT = qk.Sum(p=>p.VTYT),
                                          THUOC = qk.Sum(p=>p.THUOC),
                                          GTCONLAI = qk.Sum(p => p.ThanhTien) - qk.Sum(p => p.VTYT) - qk.Sum(p => p.THUOC)
                                      }).ToList();
                            if (_dsLoaiPT.Count > 0)
                            {
                                kq = (from lck in _dsLoaiPT
                                      join k in kq on lck equals k.Loai
                                      select k).ToList();
                            }
                            if (lupDichVu.EditValue != null && lupDichVu.EditValue.ToString() != "0")
                            {
                                kq = kq.Where(p => p.MaDV == Convert.ToInt32(lupDichVu.EditValue)).ToList();
                            }
                            if (lupKPhong.EditValue != null && lupKPhong.EditValue.ToString() != "0")
                            {
                                kq = kq.Where(p => p.MaKP == Convert.ToInt32(lupKPhong.EditValue)).ToList();
                            }
                                BaoCao.rep_ChamCongThuThuat26007 rep = new BaoCao.rep_ChamCongThuThuat26007();
                                rep.paramTenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                if (chkDT.Checked == true) { rep.DeTrang.Value = 1; }
                                if (lupKPhong.EditValue != null && lupKPhong.EditValue.ToString() != "0" && lupKPhong.GetColumnValue("TenKP") != null && lupKPhong.GetColumnValue("TenKP").ToString() != "0")
                                {
                                    rep.paramKPhong.Value = lupKPhong.GetColumnValue("TenKP").ToString().ToUpper();
                                }
                                rep.paramTuNgayDenNgay.Value = "Từ " + DungChung.Ham.NgaySangChu(tungay) + " đến " + DungChung.Ham.NgaySangChu(denngay);
                                if (thanhtoan == 0)
                                {
                                    var kq1 = (
                                        from k in kq
                                        where !(from vp in _datacontext.VienPhis select vp.MaBNhan).Contains(k.MaBNhan)
                                        select new { k.TenBNhan, k.Tuoi, k.DChi, Sthe = k.SThe == null ? "" : k.SThe, k.TenDV, k.NgayNhap, k.TenCB, k.TrongBH, k.DSCBTH, k.Loai, k.ThanhTien }).OrderBy(n => n.NgayNhap).ToList();
                                    rep.DataSource = kq1;
                                    rep.celTS1.Text = kq1.Where(p => p.Loai == 1).Count().ToString();
                                    rep.celTS2.Text = kq1.Where(p => p.Loai == 2).Count().ToString();
                                }
                                if (thanhtoan == 1)
                                {
                                    var kq2 = (
                                        from k in kq
                                        join vp in _datacontext.VienPhis on k.MaBNhan equals vp.MaBNhan
                                        select new { k.TenBNhan, k.Tuoi, k.DChi, Sthe = k.SThe == null ? "" : k.SThe, k.TenDV, k.NgayNhap, k.TenCB, k.TrongBH, k.DSCBTH, k.Loai, k.ThanhTien }).OrderBy(n => n.NgayNhap).ToList();
                                    rep.DataSource = kq2;
                                    rep.celTS1.Text = kq2.Where(p => p.Loai == 1).Count().ToString();
                                    rep.celTS2.Text = kq2.Where(p => p.Loai == 2).Count().ToString();
                                }
                                if (thanhtoan == 2)
                                {
                                    rep.DataSource = kq.OrderBy(p => p.NgayNhap).ToList();
                                    rep.celTS1.Text = kq.Where(p => p.Loai == 1).Count().ToString();
                                    rep.celTS2.Text = kq.Where(p => p.Loai == 2).Count().ToString();
                                }

                                #region xuất Excel
                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                                int num = 1;
                                DungChung.Bien.MangHaiChieu = new Object[kq.Count + 1, 20];
                                string[] _tieude = { "STT", "Tên người bệnh", "Tuổi", "Địa chỉ", "Số thẻ BHYT", "Tên DV", "Ngày làm", "PTV chính", "PTV phụ", "GM chính", "GM phụ", "giúp việc", "Loại" };
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
                                }

                                //for (int i = 0; i <= 17; i++) {
                                //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                //}
                                foreach (var r in kq)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.DChi;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.SThe;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.TenDV;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.NgayNhap;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.TenCB;
                                    DungChung.Bien.MangHaiChieu[num, 8] = "";
                                    DungChung.Bien.MangHaiChieu[num, 9] = "";
                                    DungChung.Bien.MangHaiChieu[num, 10] = "";
                                    DungChung.Bien.MangHaiChieu[num, 11] = "";
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.Loai;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.ThanhTien;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.VTYT;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.THUOC;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.GTCONLAI;
                                    num++;
                                }
                                //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo cham cong PTTT", "C:\\BC_ChamCongPTTT.xls", true);
                                #endregion

                                rep.BindingData();
                                rep.CreateDocument();
                                frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo cham cong PTTT", "C:\\BC_ChamCongPTTT.xls", false, this.Name);
                                frm1.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm1.ShowDialog();
                            }
                        
                        #endregion
                        #region BV khác
                        else
                        {
                            BaoCao.rep_ChamCongThuThuat rep = new BaoCao.rep_ChamCongThuThuat();
                            rep.paramTenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            if (chkDT.Checked == true) { rep.DeTrang.Value = 1; }
                            if (lupKPhong.EditValue != null && lupKPhong.EditValue.ToString() != "0" && lupKPhong.GetColumnValue("TenKP") != null && lupKPhong.GetColumnValue("TenKP").ToString() != "0")
                            {
                                rep.paramKPhong.Value = lupKPhong.GetColumnValue("TenKP").ToString().ToUpper();
                            }
                            rep.paramTuNgayDenNgay.Value = "Từ " + DungChung.Ham.NgaySangChu(tungay) + " đến " + DungChung.Ham.NgaySangChu(denngay);
                            var _ldv = (from dv in _datacontext.DichVus
                                        join tn in _datacontext.TieuNhomDVs.Where(p => p.IdTieuNhom == nhomdichvu) on dv.IdTieuNhom equals tn.IdTieuNhom
                                        select new
                                        {
                                            dv.MaDV,
                                            dv.Loai,
                                            dv.TenDV
                                        }).ToList();
                            var _ldt = (from dt in _datacontext.DThuocs.Where(p => p.PLDV == 2)
                                        join dtct in _datacontext.DThuoccts.Where(p => macb == "" ? true : p.MaCB == macb).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on dt.IDDon equals dtct.IDDon
                                        join kb in _datacontext.BNKBs on dtct.IDKB equals kb.IDKB into kqua
                                        from kq2 in kqua.DefaultIfEmpty()
                                        select new
                                        {
                                            dtct.MaDV,
                                            dt.MaBNhan,
                                            dtct.MaCB,
                                            dtct.NgayNhap,
                                            dtct.TrongBH,
                                            dtct.DSCBTH,
                                            dtct.MaKP,
                                            ChanDoan = kq2 == null ? "" : kq2.ChanDoan,
                                            Benhkhac = kq2 == null ? "" : kq2.BenhKhac
                                        }).ToList();
                            List<BenhNhan> _bn = _datacontext.BenhNhans.ToList();

                            var kq = (from bn in _bn
                                      join dtct in _ldt on bn.MaBNhan equals dtct.MaBNhan
                                      join dv in _ldv on dtct.MaDV equals dv.MaDV
                                      join cb in _datacontext.CanBoes on dtct.MaCB equals cb.MaCB into kqCB
                                      from kq1 in kqCB.DefaultIfEmpty()
                                     // where (DungChung.Bien.MaBV == "12001" ? ChanDoan == dtct.ChanDoan + "/" + dtct.Benhkhac : ChanDoan == dtct.ChanDoan)
                                      select new { bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, SThe = bn.SThe == null ? "" : bn.SThe, dv.MaDV, dv.TenDV, dtct.NgayNhap, TenCB = kq1 == null ? "" : kq1.TenCB, dtct.TrongBH, dtct.DSCBTH, dtct.MaKP, dv.Loai, ChanDoan = DungChung.Bien.MaBV == "12001" ? (dtct.ChanDoan + "/" + DungChung.Ham.FreshString(dtct.Benhkhac)) : dtct.ChanDoan }).ToList();

                            if (_dsLoaiPT.Count > 0)
                            {
                                kq = (from lck in _dsLoaiPT
                                      join k in kq on lck equals k.Loai
                                      select k).ToList();
                            }
                            if (lupDichVu.EditValue != null && lupDichVu.EditValue.ToString() != "0")
                            {
                                kq = kq.Where(p => p.MaDV == Convert.ToInt32(lupDichVu.EditValue)).ToList();
                            }
                            if (lupKPhong.EditValue != null && lupKPhong.EditValue.ToString() != "0")
                            {
                                kq = kq.Where(p => p.MaKP == Convert.ToInt32(lupKPhong.EditValue)).ToList();
                            }
                            if (thanhtoan == 0)
                            {
                                var kq1 = (
                                    from k in kq
                                    where !(from vp in _datacontext.VienPhis select vp.MaBNhan).Contains(k.MaBNhan)
                                    select new { k.TenBNhan, k.Tuoi, k.DChi, Sthe = k.SThe == null ? "" : k.SThe, k.TenDV, k.NgayNhap, k.TenCB, k.TrongBH, k.DSCBTH, k.ChanDoan, k.Loai }).OrderBy(n => n.NgayNhap).ToList();
                                rep.DataSource = kq1;
                                rep.celTSKPL.Text = kq1.Where(p => p.Loai == -1).Count().ToString();
                                rep.celTSDB.Text = kq1.Where(p => p.Loai == 0).Count().ToString();
                                rep.celTS1.Text = kq1.Where(p => p.Loai == 1).Count().ToString();
                                rep.celTS2.Text = kq1.Where(p => p.Loai == 2).Count().ToString();
                                rep.celTS3.Text = kq1.Where(p => p.Loai == 3).Count().ToString();
                            }
                            if (thanhtoan == 1)
                            {
                                var kq2 = (
                                    from k in kq
                                    join vp in _datacontext.VienPhis on k.MaBNhan equals vp.MaBNhan
                                    select new { k.TenBNhan, k.Tuoi, k.DChi, Sthe = k.SThe == null ? "" : k.SThe, k.TenDV, k.NgayNhap, k.TenCB, k.TrongBH, k.DSCBTH, k.ChanDoan, k.Loai }).OrderBy(n => n.NgayNhap).ToList();
                                rep.DataSource = kq2;
                                rep.celTSKPL.Text = kq2.Where(p => p.Loai == -1).Count().ToString();
                                rep.celTSDB.Text = kq2.Where(p => p.Loai == 0).Count().ToString();
                                rep.celTS1.Text = kq2.Where(p => p.Loai == 1).Count().ToString();
                                rep.celTS2.Text = kq2.Where(p => p.Loai == 2).Count().ToString();
                                rep.celTS3.Text = kq2.Where(p => p.Loai == 3).Count().ToString();
                            }
                            if (thanhtoan == 2)
                            {
                                rep.DataSource = kq.OrderBy(p => p.NgayNhap).ToList();
                                rep.celTSKPL.Text = kq.Where(p => p.Loai == -1).Count().ToString();
                                rep.celTSDB.Text = kq.Where(p => p.Loai == 0).Count().ToString();
                                rep.celTS1.Text = kq.Where(p => p.Loai == 1).Count().ToString();
                                rep.celTS2.Text = kq.Where(p => p.Loai == 2).Count().ToString();
                                rep.celTS3.Text = kq.Where(p => p.Loai == 3).Count().ToString();
                            }

                            #region xuất Excel
                            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            int[] _arrWidth = new int[] { };
                            // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                            int num = 1;
                            DungChung.Bien.MangHaiChieu = new Object[kq.Count + 1, 20];
                            string[] _tieude = { "STT", "Tên người bệnh", "Tuổi", "Địa chỉ", "Số thẻ BHYT", "Tên DV", "Ngày làm", "PTV chính", "PTV phụ", "GM chính", "GM phụ", "giúp việc", "Loại" };
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
                            }

                            //for (int i = 0; i <= 17; i++) {
                            //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                            //}
                            foreach (var r in kq)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.DChi;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.SThe;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.TenDV;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.NgayNhap;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.TenCB;
                                DungChung.Bien.MangHaiChieu[num, 8] = "";
                                DungChung.Bien.MangHaiChieu[num, 9] = "";
                                //if (DungChung.Bien.MaBV == "12001")
                                //    DungChung.Bien.MangHaiChieu[num, 9] = r.GMChinh;
                                DungChung.Bien.MangHaiChieu[num, 10] = "";
                                DungChung.Bien.MangHaiChieu[num, 11] = "";
                                DungChung.Bien.MangHaiChieu[num, 12] = r.Loai;
                                num++;
                            }
                            //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo cham cong PTTT", "C:\\BC_ChamCongPTTT.xls", true);
                            #endregion

                            rep.BindingData();
                            rep.CreateDocument();
                            frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo cham cong PTTT", "C:\\BC_ChamCongPTTT.xls", false, this.Name);
                            frm1.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm1.ShowDialog();
                        }
                #endregion
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool kt()
        {
            if (lupNhomDichVu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn nhóm Dịch vụ.");
                lupNhomDichVu.Focus();
                return false;
            }
            if (lupDichVu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Dịch vụ.");
                lupDichVu.Focus();
                return false;
            }
            if (lupTT.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Thanh toán.");
                lupTT.Focus();
                return false;
            }
            return true;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ChamCongPhauThuat_Load(object sender, EventArgs e)
        {
            //in ra ngay hien tai;
            deNgayTu.EditValue = DateTime.Now.Date;
            deNgayDen.EditValue = DateTime.Now.Date;
            var nhomdichvu = (from tn in _datacontext.TieuNhomDVs
                              join nhom in _datacontext.NhomDVs on tn.IDNhom equals nhom.IDNhom
                              where (nhom.TenNhomCT == ("Thủ thuật, phẫu thuật") || nhom.TenNhomCT == ("Chẩn đoán hình ảnh") || nhom.TenNhomCT == ("Thăm dò chức năng"))
                              select new
                              {
                                  tn.IdTieuNhom,
                                  tn.TenTN,
                                  tn.TenRG,
                              }).ToList();
            lupNhomDichVu.Properties.DataSource = nhomdichvu;

            ThanhToan tt = new ThanhToan();
            tt.Giatri = 0;
            tt.KieuTT = "Chưa thanh toán";
            _lThanhToan.Add(tt);
            ThanhToan tt1 = new ThanhToan();
            tt1.Giatri = 1;
            tt1.KieuTT = "Đã thanh toán";
            _lThanhToan.Add(tt1);
            ThanhToan tt2 = new ThanhToan();
            tt2.Giatri = 2;
            tt2.KieuTT = "Cả hai";
            _lThanhToan.Add(tt2);
            lupTT.Properties.DataSource = _lThanhToan;
            lupTT.Properties.DisplayMember = "KieuTT";
            lupTT.Properties.ValueMember = "Giatri";
            lupTT.EditValue = 2;

            KPhong moi = new KPhong();
            moi.MaKP = 0;
            moi.TenKP = " Tất cả";
            _lKP.Add(moi);
            var kp = _datacontext.KPhongs.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng").OrderBy(p => p.TenKP).ToList();
            _lKP.AddRange(kp);
            lupKPhong.Properties.DataSource = _lKP;
            lupKPhong.EditValue = 0;

            // string tenCB = lupNhomDichVu.EditValue.ToString();
            var qcb = (from tcb in _datacontext.CanBoes 
                       join kph in _datacontext.KPhongs.Where(p=>p.MaBVsd == DungChung.Bien.MaBV).Where(d => d.PLoai == "Lâm sàng" || d.PLoai == "Phòng Khám" || d.PLoai == "Cận lâm sàng") on tcb.MaKP equals kph.MaKP
                       group tcb by new
                       {
                           tcb.MaCB, tcb.TenCB
                       }
                       into kq  select new  { MaCB= kq.Key.MaCB, TenCB = kq.Key.TenCB }).ToList();
            foreach(var a in qcb)
            {
                CanBo cb0 = new CanBo();
                cb0.MaCB = a.MaCB;
                cb0.TenCB =a.TenCB;
                _lCanBo.Add(cb0);
            }
            _lCanBo = _lCanBo.OrderBy(p => p.TenCB).ToList();
            CanBo cb = new CanBo();
            cb.MaCB = "";
            cb.TenCB = "Tất cả";
            _lCanBo.Insert(0,cb); 
            lupTenCB.Properties.DataSource = _lCanBo;
            lupTenCB.EditValue = lupTenCB.Properties.GetKeyValueByDisplayText("Tất cả");
        }
        #region clss ThanhToan
        public class ThanhToan
        {
            string kieuTT;
            int giatri;

            public string KieuTT
            {
                get { return kieuTT; }
                set { kieuTT = value; }
            }
            public int Giatri
            {
                get { return giatri; }
                set { giatri = value; }
            }

        }
        #endregion
        #region class Loại phẫu thuật, thủ thuật
        public class LoaiPTTT
        {
            private int loai;

            public int Loai
            {
                get { return loai; }
                set { loai = value; }
            }
            private string tenLoai;

            public string TenLoai
            {
                get { return tenLoai; }
                set { tenLoai = value; }
            }

        }
        #endregion

        private void lupNhomDichVu_EditValueChanged(object sender, EventArgs e)
        {
            _lDichVu.Clear();
            int IDTieuNhom = Convert.ToInt32(lupNhomDichVu.EditValue);
            var dichvu = (from dv in _datacontext.DichVus.Where(d => d.IdTieuNhom == IDTieuNhom)
                          select dv).ToList();
            DichVu dvu = new DichVu();
            dvu.MaDV = 0;
            dvu.TenDV = " Tất cả";
            _lDichVu.Add(dvu);
            _lDichVu.AddRange(dichvu);
            _lDichVu = _lDichVu.OrderBy(t => t.TenDV).ToList();
            lupDichVu.Properties.DataSource = _lDichVu;
            lupDichVu.EditValue = 0;
            //lấy loại phẫu thuật, thủ thuật theo tên rút gọn
            string tenRG = _datacontext.TieuNhomDVs.Where(p => p.IdTieuNhom == IDTieuNhom).FirstOrDefault().TenRG;
            var loai = (from dv in _datacontext.DichVus
                        join tn in _datacontext.TieuNhomDVs.Where(p => p.TenRG == tenRG) on dv.IdTieuNhom equals tn.IdTieuNhom
                        select new { dv.Loai }).Distinct().ToList();
            List<LoaiPTTT> _lLoaiPTTT = new List<LoaiPTTT>();
            if (tenRG.ToLower().Contains("phẫu thuật") || tenRG.ToLower().Contains("thủ thuật"))
            {
                LoaiPTTT loaiMoi = new LoaiPTTT();
                foreach (var item in loai)
                {
                    loaiMoi = new LoaiPTTT();
                    if (item.Loai == 0)
                    {
                        loaiMoi.Loai = 0;
                        loaiMoi.TenLoai = "Loại đặc biệt";
                    }
                    if (item.Loai == 1)
                    {
                        loaiMoi.Loai = 1;
                        loaiMoi.TenLoai = "Loại I";
                    }
                    if (item.Loai == 2)
                    {
                        loaiMoi.Loai = 2;
                        loaiMoi.TenLoai = "Loại II";
                    }
                    if (item.Loai == 3)
                    {
                        loaiMoi.Loai = 3;
                        loaiMoi.TenLoai = "Loại III";
                    }
                    else if (item.Loai == -1)
                    {
                        loaiMoi.Loai = -1;
                        loaiMoi.TenLoai = "Không phân loại (-1)";
                    }
                    _lLoaiPTTT.Add(loaiMoi);
                }
                checkedListBoxControl1.DataSource = _lLoaiPTTT;
            }
            else 
            {
                _lLoaiPTTT.Clear();
                checkedListBoxControl1.DataSource = _lLoaiPTTT;
            }
        }

        private void lupTenCB_EditValueChanged(object sender, EventArgs e)
        {

        }

        public string ChanDoan { get; set; }

        private void rgbc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgbc.SelectedIndex == 1)
            {
                tltt.Visible = true;
                labelControl9.Visible = true;
            }
            else
            {
                tltt.Text = "";
                tltt.Visible = false;
                labelControl9.Visible = false;
            }
        }
    }
}