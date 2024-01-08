using System;using QLBV_Database;
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
    public partial class frm_DuTruThuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_DuTruThuoc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data;
        private void dtNgayKe_EditValueChanged(object sender, EventArgs e)
        {

        }
        List<KPhong> _lkp = new List<KPhong>();
        List<CanBo> _lcb = new List<CanBo>();
        List<DichVu> dsThuoc = new List<DichVu>();
        List<NhaCC> _lncc = new List<NhaCC>();
        List<DThuoctv> _ldtv = new List<DThuoctv>();
        List<DThuoctvct> _ldttvct = new List<DThuoctvct>();
        private void frm_DuTruThuoc_Load(object sender, EventArgs e)
        {
            Enablebutton(true);
            EnableControl(false); 
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lkp = _data.KPhongs.ToList();
            _lcb = _data.CanBoes.ToList();
            dsThuoc = _data.DichVus.Where(p => p.PLoai == 1).Where(p => p.Status == 1).ToList();
            var kp = _lkp.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).ToList();
            dtNgayKe.DateTime = DateTime.Now;
            lupTimBP.Properties.DataSource = kp;
            lupBPKe.Properties.DataSource = kp;
            _lncc = _data.NhaCCs.ToList();
            lupNhaCC.DataSource = _lncc;
            var dsthuoc = dsThuoc.OrderBy(p => p.TenDV).Select(p => new { p.MaDV, p.TenDV }).ToList();
            //List<DThuoctvct> lds = new List<DThuoctvct>();
            BinDThuocct.DataSource = _ldttvct;
            grcdsthuoc.DataSource = BinDThuocct;
            
            lupMaDuoc.DataSource = dsthuoc.ToList();
            //lupDuoc.DataSource = dsthuoc.ToList();
            dtTimTuNgay.DateTime = DateTime.Now;
            dtTimDenNgay.DateTime = DateTime.Now;
        }

        public void TimKiem()
        {
            DateTime _tungay = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            DateTime _denngay = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            int makp = 0;
            if (lupTimBP.EditValue != null)
                makp = Convert.ToInt32(lupTimBP.EditValue);
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _ldtv = _data.DThuoctvs.Where(p => p.Loai == 1).Where(p => p.NgayKe >= _tungay && p.NgayKe <= _denngay).Where(p => p.MaKP == makp).ToList();
            //grcDonThuocdt.DataSource = null;
            grcDonThuocdt.DataSource = _ldtv;
        }
        private void grvDonThuocdt_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDonThuocdt.GetFocusedRowCellValue(colIDDon) != null && grvDonThuocdt.GetFocusedRowCellValue(colIDDon).ToString() != "")
            {
                int _iddon = Convert.ToInt32(grvDonThuocdt.GetFocusedRowCellValue(colIDDon));
                var dthuoc = _data.DThuoctvs.Where(p => p.IDDontv == _iddon).ToList();
                if(dthuoc.Count()>0)
                {
                    lupBPKe.EditValue = dthuoc.First().MaKP;
                    lupNguoiKe.EditValue = dthuoc.First().MaCB;
                    dtNgayKe.DateTime = dthuoc.First().NgayKe.Value;
                    _ldttvct = _data.DThuoctvcts.Where(p => p.IDDontv == _iddon).ToList();
                    //grcdsthuoc.DataSource = null;

                    BinDThuocct.DataSource = _ldttvct;
                    grcdsthuoc.DataSource = BinDThuocct;
                }
            }
            else
            {
                dtNgayKe.DateTime = DateTime.Now;
                lupBPKe.EditValue = null;
                lupNguoiKe.EditValue = null;
                grcdsthuoc.DataSource = "";
            }
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

        private void lupBPKe_EditValueChanged(object sender, EventArgs e)
        {
            if (lupBPKe.EditValue != null)
            {
                int makp = Convert.ToInt32(lupBPKe.EditValue);
                string _makp = ";" + makp.ToString() + ";";
                var lcb = _lcb.Where(p => p.MaKPsd.Contains(_makp)).OrderBy(p => p.TenCB).ToList();
                lupNguoiKe.Properties.DataSource = lcb;
            }
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
            lupNguoiKe.Properties.ReadOnly = !status;
            //cboNhomDuoc.Properties.ReadOnly = !status;
            grvdsthuoc.OptionsBehavior.Editable = status;
            grcDonThuocdt.Enabled = !status;
        }
        private void ResetControl()
        {
            dtNgayKe.EditValue = System.DateTime.Now;
            lupBPKe.EditValue = 0;
            lupNguoiKe.EditValue = "";
            _ldttvct = _data.DThuoctvcts.Where(p => p.IDDontv == 0).ToList();
            BinDThuocct.DataSource = _ldttvct;
            grcdsthuoc.DataSource = BinDThuocct; 
        }
        private void grvDonThuocct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }
        int TrangThai = 0;
        private void btnMoi_Click(object sender, EventArgs e)
        {
            Enablebutton(false);
            EnableControl(true);
            ResetControl();
            lupBPKe.EditValue = DungChung.Bien.MaKP;
            TrangThai = 1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (grvDonThuocdt.GetFocusedRowCellValue(colIDDon) != null && grvDonThuocdt.GetFocusedRowCellValue(colIDDon) != "")
            {
                int iddon = Convert.ToInt32(grvDonThuocdt.GetFocusedRowCellValue(colIDDon));
                if (iddon > 0)
                {
                    Enablebutton(false);
                    EnableControl(true);
                    TrangThai = 2;
                }
            }
        }

        private void grvDonThuocct_CellValueChanged_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //switch (e.Column.Name)
            //{
            //    case "colstt":
            //        if (grvDonThuocct.GetFocusedRowCellValue(colstt) != null)
            //        {
            //            string a = grvDonThuocct.GetFocusedRowCellValue(colstt).ToString();
            //        }
            //        break;
            //    case "colMaDVct":
            //        if (grvDonThuocct.GetFocusedRowCellValue(colMaDVct) != null)
            //        {
            //            int madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVct));
            //            var dv = dsThuoc.Where(p => p.MaDV == madv).FirstOrDefault();
            //            if (dv != null)
            //            {
            //                grvDonThuocct.SetFocusedRowCellValue(colDonVi, dv.DonVi);
            //                grvDonThuocct.SetFocusedRowCellValue(colDonGia, dv.DonGiaTT15);
            //                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, 0);
            //            }
            //        }
            //        break;
            //    case "colSoLuong":
            //        {
            //            if (grvDonThuocct.GetFocusedRowCellValue(colSoLuong) != null && grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString() != "")
            //            {
            //                double a = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString());
            //                if (a > 0)
            //                {
            //                    double dongia = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString());
            //                    double tt = Math.Round(a * dongia, DungChung.Bien.LamTronSo);
            //                    grvDonThuocct.SetFocusedRowCellValue(colThanhTien, tt);
            //                }
            //            }
            //        }
            //        break;
            //}
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool KTLuu = true;
            if (lupBPKe.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn khoa kê!");
                KTLuu = false;
            }
            if (lupNguoiKe.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn người kê!");
                KTLuu = false;
            }
            if(KTLuu)
            {
                _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                #region Thêm mới
                if(TrangThai==1)
                {
                    DThuoctv moi = new DThuoctv();
                    moi.Loai = 1;
                    moi.NgayKe = dtNgayKe.DateTime;
                    moi.MaKP = Convert.ToInt32(lupBPKe.EditValue);
                    moi.MaCB = lupNguoiKe.EditValue.ToString();
                    _data.DThuoctvs.Add(moi);
                    if (_data.SaveChanges() >= 0)
                    {
                        int iddon = moi.IDDontv;
                        for (int i = 0; i < grvdsthuoc.DataRowCount; i++)
                        {
                            if (grvdsthuoc.GetRowCellValue(i, colMaDuoc) != null)
                            {
                                DThuoctvct dthuocct = new DThuoctvct();
                                dthuocct.IDDontv = iddon;
                                dthuocct.MaKP = Convert.ToInt32(lupBPKe.EditValue);
                                dthuocct.MaDV = Convert.ToInt32(grvdsthuoc.GetRowCellValue(i, colMaDuoc));
                                dthuocct.SoLuong = Convert.ToDouble(grvdsthuoc.GetRowCellValue(i, colSoLuong));
                                dthuocct.DonVi = grvdsthuoc.GetRowCellValue(i, colDonVi).ToString().Trim();
                                dthuocct.DonGia = Convert.ToDouble(grvdsthuoc.GetRowCellValue(i, colDonGia).ToString());
                                dthuocct.ThanhTien = Convert.ToDouble(grvdsthuoc.GetRowCellValue(i, colThanhTien).ToString());
                                dthuocct.NgayNhap = dtNgayKe.DateTime;
                                if (grvdsthuoc.GetRowCellValue(i, colNhaCC) != null)
                                    dthuocct.MaCC = grvdsthuoc.GetRowCellValue(i, colNhaCC).ToString();
                                _data.DThuoctvcts.Add(dthuocct);
                                _data.SaveChanges();
                            }
                        }
                    }
                    MessageBox.Show("Tạo đơn thành công!");
                    Enablebutton(true);
                    TrangThai = 0;
                    frm_DuTruThuoc_Load(sender, e);
                }
                #endregion
                #region Sửa
                else if (TrangThai == 2)
                {
                    if (grvDonThuocdt.GetFocusedRowCellValue(colIDDon) != null)
                    {
                        int iddon = Convert.ToInt32(grvDonThuocdt.GetFocusedRowCellValue(colIDDon));
                        var dthuoc = _data.DThuoctvs.Where(p => p.IDDontv == iddon).FirstOrDefault();
                        dthuoc.NgayKe = dtNgayKe.DateTime;
                        dthuoc.MaKP = Convert.ToInt32(lupBPKe.EditValue != null ? lupBPKe.EditValue : 0);
                        dthuoc.MaCB = lupNguoiKe.EditValue != null ? lupNguoiKe.EditValue.ToString() : null;
                        if(_data.SaveChanges()>=0)
                        {
                            for (int i = 0; i < grvdsthuoc.DataRowCount; i++)
                            {
                                if (grvdsthuoc.GetRowCellValue(i, colMaDuoc) != null)
                                {
                                    if (grvdsthuoc.GetRowCellValue(i, colSoLuong) != null && grvdsthuoc.GetRowCellValue(i, colSoLuong).ToString() != "0" && grvdsthuoc.GetRowCellValue(i, colSoLuong).ToString() != "")
                                    {
                                        if (grvdsthuoc.GetRowCellValue(i, colidDonct) != null && grvdsthuoc.GetRowCellValue(i, colidDonct).ToString() != "")
                                        {
                                            int idct = int.Parse(grvdsthuoc.GetRowCellValue(i, colidDonct).ToString());
                                            if (idct > 0)// sửa row
                                            {

                                                //if (grvDonThuocct.GetRowCellValue(i, colTTLuu) != null && grvDonThuocct.GetRowCellValue(i, colTTLuu).ToString() != "2")
                                                //{
                                                DThuoctvct dthuocct = _data.DThuoctvcts.Single(p => p.IDDontvct == idct);
                                                dthuocct.MaKP = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                                                dthuocct.MaDV = Convert.ToInt32(grvdsthuoc.GetRowCellValue(i, colMaDuoc));
                                                dthuocct.DonVi = grvdsthuoc.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                dthuocct.DonGia = Convert.ToDouble(grvdsthuoc.GetRowCellValue(i, colDonGia).ToString());
                                                dthuocct.SoLuong = Convert.ToDouble(grvdsthuoc.GetRowCellValue(i, colSoLuong).ToString());
                                                dthuocct.ThanhTien = Convert.ToDouble(grvdsthuoc.GetRowCellValue(i, colThanhTien).ToString());
                                                dthuocct.NgayNhap = dtNgayKe.DateTime;
                                                if (grvdsthuoc.GetRowCellValue(i, colNhaCC) != null)
                                                    dthuocct.MaCC = grvdsthuoc.GetRowCellValue(i, colNhaCC).ToString();
                                                _data.SaveChanges();
                                                // }
                                            }
                                            else
                                            {// lưu row mới 
                                                DThuoctvct dthuocct = new DThuoctvct();
                                                dthuocct.IDDontv = iddon;
                                                dthuocct.MaDV = Convert.ToInt32(grvdsthuoc.GetRowCellValue(i, colMaDuoc));
                                                dthuocct.DonVi = grvdsthuoc.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                dthuocct.DonGia = Convert.ToDouble(grvdsthuoc.GetRowCellValue(i, colDonGia).ToString());
                                                dthuocct.SoLuong = Convert.ToDouble(grvdsthuoc.GetRowCellValue(i, colSoLuong).ToString());
                                                dthuocct.ThanhTien = Convert.ToDouble(grvdsthuoc.GetRowCellValue(i, colThanhTien).ToString());
                                                dthuocct.NgayNhap = dtNgayKe.DateTime;
                                                if (grvdsthuoc.GetRowCellValue(i, colNhaCC) != null)
                                                    dthuocct.MaCC = grvdsthuoc.GetRowCellValue(i, colNhaCC).ToString();
                                                _data.DThuoctvcts.Add(dthuocct);
                                                _data.SaveChanges();
                                                //frm_LinhKhoa_Moi_Load(sender, e);
                                            }
                                        }
                                    }
                                }
                            }
                            MessageBox.Show("Sửa thành công!");
                            Enablebutton(true);
                            frm_DuTruThuoc_Load(sender, e);
                            TrangThai = 0;
                        }
                       
                    }
                }
                #endregion
            }
        }

        private void grvdsthuoc_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int madv = 0;
            switch (e.Column.Name)
            {
                case "colMaDuoc":
                    if (grvdsthuoc.GetFocusedRowCellValue(colMaDuoc) != null)
                    {
                        madv = Convert.ToInt32(grvdsthuoc.GetFocusedRowCellValue(colMaDuoc).ToString());
                        var thuoc = dsThuoc.Where(p => p.MaDV == madv).FirstOrDefault();
                        if(thuoc!=null)
                        {
                            grvdsthuoc.SetFocusedRowCellValue(colDonVi, thuoc.DonVi);
                            grvdsthuoc.SetFocusedRowCellValue(colDonGia, Math.Round(thuoc.DonGia, DungChung.Bien.LamTronSo));
                            grvdsthuoc.SetFocusedRowCellValue(colNhaCC, thuoc.MaCC);
                        }
                    }
                    break;
                case "colSoLuong":
                    if (grvdsthuoc.GetFocusedRowCellValue(colSoLuong) != null)
                    {
                        double soluong = Convert.ToDouble(grvdsthuoc.GetFocusedRowCellValue(colSoLuong).ToString());
                        double dongia = Convert.ToDouble(grvdsthuoc.GetFocusedRowCellValue(colDonGia).ToString());
                        grvdsthuoc.SetFocusedRowCellValue(colThanhTien, Math.Round(dongia * soluong, DungChung.Bien.LamTronSo));
                    }
                    break;
                case "colDonGia":
                    if (grvdsthuoc.GetFocusedRowCellValue(colDonGia) != null)
                    {
                        if (grvdsthuoc.GetFocusedRowCellValue(colSoLuong) != null)
                        {
                            double soluong = Convert.ToDouble(grvdsthuoc.GetFocusedRowCellValue(colSoLuong).ToString());
                            double dongia = Convert.ToDouble(grvdsthuoc.GetFocusedRowCellValue(colDonGia).ToString());
                            grvdsthuoc.SetFocusedRowCellValue(colThanhTien, Math.Round(dongia * soluong, DungChung.Bien.LamTronSo));
                        }
                    }
                    break;
            }
        }

        private void grvDonThuocdt_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //switch (e.Column.Name)
            //{
            //    case "colIDDon":
            //        if (grvDonThuocdt.GetFocusedRowCellValue(colIDDon) != null)
            //        {
            //            string a = grvDonThuocdt.GetFocusedRowCellValue(colIDDon).ToString();
            //        }
            //        break;
            //}
        }

        private void grcDonThuocdt_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(grvDonThuocdt.GetFocusedRowCellValue(colIDDon)!=null)
            {
                int iddon = Convert.ToInt32(grvDonThuocdt.GetFocusedRowCellValue(colIDDon));
                DialogResult Res = MessageBox.Show("Bạn muốn xóa đơn thuốc này ?", "Hỏi xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Res == DialogResult.OK)
                {
                    _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    var ktct = _data.DThuoctvcts.Where(p => p.IDDontv == iddon).Select(p => p.IDDontvct).ToList();
                    foreach (var i in ktct)
                    {
                        var xoact = _data.DThuoctvcts.Single(p => p.IDDontvct == i);
                        _data.DThuoctvcts.Remove(xoact);
                        _data.SaveChanges();
                    }
                    var xoa = _data.DThuoctvs.Single(p => p.IDDontv == iddon);
                    _data.DThuoctvs.Remove(xoa);
                    _data.SaveChanges();
                    TimKiem();
                }
            }
        }

        private void btnKLuu_Click(object sender, EventArgs e)
        {
            this.frm_DuTruThuoc_Load(sender, e);// frm_DuTruThuoc_Load(sender, e);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if(grvDonThuocdt.GetFocusedRowCellValue(colIDDon)!=null)
            {
                int iddon = Convert.ToInt32(grvDonThuocdt.GetFocusedRowCellValue(colIDDon));
                var dthuoc = _data.DThuoctvs.Where(p => p.IDDontv == iddon).ToList();
                var dthuocct = _data.DThuoctvcts.Where(p => p.IDDontv == iddon).ToList();
               
                

                if (dthuoc.Count() > 0)
                {
                    if(DungChung.Bien.MaBV == "14017")
                    {
                        frmIn frm = new frmIn();
                        BaoCao.PhieuTrathuocVTYT rep = new BaoCao.PhieuTrathuocVTYT();
                        rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                        //rep.SoPL.Value = _soPL.ToString();
                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                        rep.Benhvien.Value = DungChung.Bien.TenCQ;

                        rep.Khoa.Value = lupBPKe.Text.ToUpper();
                        //int tekho = dthuoc.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                        //var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                        rep.Kholinh.Value = "Kho lĩnh: " + lupTimBP.Text;
                        rep.theongay.Value = "Ngày kê: " + dthuoc.First().NgayKe;

                        var q33 = (from kd in _data.DThuocs.Where(p => p.IDDon == iddon)
                                   join kdct in _data.DThuoccts.Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                   join dv in _data.DichVus on kdct.MaDV equals dv.MaDV
                                   join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                   select new { TenRGTN = tn.TenRG, dv.SoDK, dv.TenRG, dv.TenDV, dv.TenHC, kdct, dv.MaTam, dv.HamLuong, LoaiDV = (kdct.LoaiDV == 3 || kdct.LoaiDV == 4) ? 1 : 0 }).ToList();

                        var q = (from kd in q33
                                 group new { kd } by new { kd.TenRGTN, kd.SoDK, kd.MaTam, kd.TenHC, kd.HamLuong, kd.kdct.DonGia, kd.kdct.DonVi, kd.kdct.MaDV, kd.TenDV, kd.TenRG, SoLo = kd.kdct.SoLo ?? "" } into kq
                                 select new 
                                 {
                                     HanDung = DungChung.Ham.getHanDung(kq.Key.MaDV ?? 0, kq.Key.SoLo),
                                     SoLo = kq.Key.SoLo,
                                     MaDV = kq.Key.MaDV ?? 0,
                                     TenDV = kq.Key.TenDV,
                                     HamLuong = kq.Key.HamLuong,
                                     TenHC = kq.Key.TenHC,
                                     TenRG = kq.Key.TenRG,
                                     MaTam = kq.Key.MaTam,
                                     DonGia = kq.Key.DonGia,
                                     SoDK = kq.Key.SoDK,
                                     DonVi = kq.Key.DonVi,
                                     //LoaiDuoc = kq.Key.LoaiDuoc,
                                     SoLuong = kq.Sum(p => p.kd.kdct.SoLuong) * (-1),
                                     ThanhTien = kq.Sum(p => p.kd.kdct.ThanhTien) * (-1)
                                 }).OrderBy(p => p.TenDV).ThenBy(p => p.DonVi).ThenBy(p => p.DonGia).ToList();

                        var q2 = (from kd in _data.DThuocs.Where(p => p.IDDon == iddon)
                                  join kdct in _data.DThuoccts.Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                  select new { kd.NgayKe }).First().NgayKe;

                        rep.xrLabel1.Text = DungChung.Ham.NgaySangChu(Convert.ToDateTime(q2));
                        rep.xrTableCell67.Text = q.Count().ToString();
                        //rep.xrTableCell75.Text = q.Sum(p => p.ThanhTien).ToString();
                        //rep.TTien.Value = q.First().ThanhTien;
                        //rep.xrTableCell75.Text = q.First().ThanhTien.ToString();

                        rep.DataSource = q.OrderBy(p => p.TenDV).ToList();
                        rep.BindingData();
                        //rep.DataMember = "";
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                        this.Dispose();
                    }
                    else
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_DThuocDuTru rep = new BaoCao.Rep_DThuocDuTru();
                        rep.TieuDe.Value = "DỰ TRÙ THUỐC THÁNG " + dthuoc.First().NgayKe.Value.Month + " NĂM " + dthuoc.First().NgayKe.Value.Year;
                        int makp = dthuoc.First().MaKP ?? 0;
                        var tenkp = _lkp.Where(p => p.MaKP == makp).Select(p => p.TenKP).FirstOrDefault();
                        if (tenkp != null)
                            rep.Khoa.Value = tenkp.ToUpper();
                        rep.NgayThang.Value = DungChung.Bien.DiaDanh + ", ngày...tháng...năm.....";
                        string macb = dthuoc.First().MaCB;
                        var tencb = _lcb.Where(p => p.MaCB == macb).Select(p => p.TenCB).FirstOrDefault();
                        if (tencb != null)
                            rep.NguoiLap.Value = tencb;
                        var q1 = (from dtct in dthuocct
                                  join dv in dsThuoc on dtct.MaDV equals dv.MaDV
                                  join ncc in _lncc on dtct.MaCC equals ncc.MaCC into kq
                                  from kq1 in kq.DefaultIfEmpty()
                                  select new
                                  {
                                      dv.TenDV,
                                      dtct.DonVi,
                                      dtct.DonGia,
                                      dtct.ThanhTien,
                                      dtct.SoLuong,
                                      NhaCC = kq1 == null ? "" : kq1.TenCC
                                  }).ToList();

                        rep.DataSource = q1.ToList();
                        rep.BinDingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    
                }
            }
        }

        private void grvdsthuoc_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == colstt)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void grcdsthuoc_Click(object sender, EventArgs e)
        {

        }

        private void grvdsthuoc_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXoact")
            {
                if (TrangThai == 1)
                {
                    grvdsthuoc.DeleteSelectedRows();
                }
                if (TrangThai == 2)
                {
                    int idct = Convert.ToInt32(grvdsthuoc.GetFocusedRowCellValue(colidDonct));
                    if (idct > 0)
                    {
                        var xoa = _data.DThuoctvcts.Single(p => p.IDDontvct == idct);



                        DialogResult _result = MessageBox.Show("Bạn muốn xóa thuốc: " + grvdsthuoc.GetFocusedRowCellDisplayText(colMaDuoc).ToString(), "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == DialogResult.Yes)
                        {
                            _data.DThuoctvcts.Remove(xoa);
                            _data.SaveChanges();
                            grvdsthuoc.DeleteSelectedRows();
                        }

                    }
                }
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //switch (e.Column.Name)
            //{
            //    case "colMaDV":
            //        if (gridView1.GetFocusedRowCellValue(colMaDV) != null)
            //        {
            //            int madv = 0;
            //            madv = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colMaDV).ToString());
            //            var thuoc = dsThuoc.Where(p => p.MaDV == madv).FirstOrDefault();
            //            if (thuoc != null)
            //            {
            //                //gridView1.SetFocusedRowCellValue(colDonVi, thuoc.DonVi);
            //                gridView1.SetFocusedRowCellValue(colGia, Math.Round(thuoc.DonGia, DungChung.Bien.LamTronSo));
            //            }
            //        }
            //        break;
            //    //case "colSoLuong":
            //    //    if (grvdsthuoc.GetFocusedRowCellValue(colSoLuong) != null)
            //    //    {
            //    //        double soluong = Convert.ToDouble(grvdsthuoc.GetFocusedRowCellValue(colSoLuong).ToString());
            //    //        double dongia = Convert.ToDouble(grvdsthuoc.GetFocusedRowCellValue(colDonGia).ToString());
            //    //        grvdsthuoc.SetFocusedRowCellValue(colThanhTien, Math.Round(dongia * soluong, DungChung.Bien.LamTronSo));
            //    //    }
            //    //    break;
            //}
        }

        private void grvDonThuocdt_DataSourceChanged(object sender, EventArgs e)
        {
            grvDonThuocdt_FocusedRowChanged(null, null);
        }
    }
}