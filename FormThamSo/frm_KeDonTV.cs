using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Extensions;
using QLBV.DungChung;

namespace QLBV.FormThamSo
{
    public partial class frm_KeDonTV : DevExpress.XtraEditors.XtraForm
    {
        public frm_KeDonTV()
        {
            InitializeComponent();
        }
        string[] _sHDSDThuoc = new string[1000];
        int kieu = 0;
        void _setHDSDThuoc()
        {
            string HD = "6 giờ 10UI;6 giờ 7UI, 18 giờ 17UI;Chia 2 lần: 11 giờ, 18 giờ;Chia 2 lần: 6 giờ, 18 giờ;Chiều;Nhai trước ăn;Nhai trước ăn sáng;Sau ăn;Sau ăn 1 giờ" +
   ";Sau ăn no;Sau ăn sáng;Sau ăn sáng, trưa, Tối;Sau ăn tối;Sáng;Sáng 1/2 V, chiều 1/2 V;Sáng 1V, Chiều 1V;Sáng 1V, Chiều 1V, sau ăn;Sáng 1V, Chiều 1V, trước ăn;Sáng 1V, Chiều 2V" +
   ";Sáng 2V, Chiều 1V;Sáng 2V, Chiều 2V;Sáng 2V, Chiều 2V, sau ăn;Sáng 1V, Chiều 1V, trước ăn;Sáng 3V, Chiều 3V;Sáng 3V, Chiều 3V, sau ăn;Sáng 3V, Chiều 3V, sau ăn;Thủy châm;Trưa 1V, Chiều 2V, Sau ăn;Trưa 1V, Chiều 2V, Trước ăn;" +
   "Trưa 1V, Tối 1V, trước ăn;-	Trưa 1V, Tối 1V, sau ăn;Trưa 2V, Chiều 1V, trước ăn;Trước ăn sáng;Trước ăn sáng 1 giờ;Tối;Uống sau 1 giờ;Uống sau 2 giờ;Uống sau ăn;Uống trong bữa ăn;-	Uống trước bữa ăn;Uống trước bữa ăn 1 giờ";
            _sHDSDThuoc = HD.Split(';');
        }
        int _mabn = 0;
        int _idkb = 0;
        int makp1 = 0;
        public frm_KeDonTV(int MaBN, int IDKB)
        {
            InitializeComponent();
            _mabn = MaBN;
            _idkb = IDKB;
        }
        private class _ds
        {
            int iDDontv;
            int? maKP;

            public int? MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
            string tenCB, tenKP, ghiChu, maCB, dotdt;

            public string DotDT
            {
                get { return dotdt; }
                set { dotdt = value; }
            }

            public string MaCB
            {
                get { return maCB; }
                set { maCB = value; }
            }
            DateTime? ngayKe, ngayTu, ngayDen;

            public DateTime? NgayDen
            {
                get { return ngayDen; }
                set { ngayDen = value; }
            }

            public DateTime? NgayTu
            {
                get { return ngayTu; }
                set { ngayTu = value; }
            }

            public DateTime? NgayKe
            {
                get { return ngayKe; }
                set { ngayKe = value; }
            }

            public string GhiChu
            {
                get { return ghiChu; }
                set { ghiChu = value; }
            }

            public string TenKP
            {
                get { return tenKP; }
                set { tenKP = value; }
            }

            public string TenCB
            {
                get { return tenCB; }
                set { tenCB = value; }
            }

            public int IDDontv
            {
                get { return iDDontv; }
                set { iDDontv = value; }
            }

        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool kt()
        {
            if (lup_NgayKe.EditValue == null || lup_NgayKe.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày kê!", "Thông báo!");
                lup_NgayKe.Focus();
                return false;
            }
            if (lup_BSKe.EditValue == null || lup_BSKe.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chọn bác sĩ kê đơn!", "Thông báo!");
                lup_BSKe.Focus();
                return false;
            }
            return true;
        }
        private void frm_KeDonTV_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")//pk bảo ngọc
            {
                colTenThuoc.Visible = true;
                celMaDV.Visible = false;
                colTenThuoc.VisibleIndex = 0;
            }
            else
            {
                colTenThuoc.Visible = false;
                celMaDV.Visible = true;
                celMaDV.VisibleIndex = 0;
            }
            lup_NgayKe.DateTime = DateTime.Now;
            txtDotDT.Enabled = false;
            NgayTu.Enabled = false;
            NgayDen.Enabled = false;
            lup_NgayKe.Enabled = false;
            lup_BSKe.Enabled = false;
            GhiChu.Enabled = false;
            lup_KhoaKe.Enabled = false;
            cboTenThuoc.Items.Clear();
            var kp = _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang).Select(p => new { p.MaKP, p.TenKP }).ToList();
            var cb = _data.CanBoes.Select(p => new { p.MaCB, p.TenCB }).ToList();
            var dv = _data.DichVus.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).Select(p => new { p.MaDV, p.TenDV, p.DonVi, p.DonGia }).ToList();
            lup_KhoaKe.Properties.DataSource = kp;
            lup_BSKe.Properties.DataSource = cb;
            lup_Thuoc.DataSource = dv;
            List<string> lTenDV = dv.Select(p => p.TenDV).Distinct().OrderBy(p => p).ToList();
            cboTenThuoc.Items.AddRange(lTenDV);
            makp1 = _data.BNKBs.Where(p => p.IDKB == _idkb).FirstOrDefault().MaKP ?? 0;
            var dt = (from a in _data.DThuoctvs.Where(p => p.MaBNhan == _mabn && p.MaKP == makp1 && p.Loai == 0) select a).ToList();
            List<_ds> ds1 = new List<_ds>();
            foreach (var item in dt)
            {
                _ds ds = new _ds();
                ds.IDDontv = item.IDDontv;
                ds.NgayKe = item.NgayKe;
                ds.GhiChu = item.GhiChu;
                ds.TenCB = !string.IsNullOrWhiteSpace(item.MaCB) ? _data.CanBoes.FirstOrDefault(p => p.MaCB == item.MaCB).TenCB : "";
                ds.MaCB = item.MaCB;
                ds.MaKP = item.MaKP;
                int x1 = Convert.ToInt32(item.MaKP);
                ds.NgayDen = item.NgayDen;
                ds.NgayTu = item.NgayTu;
                ds.DotDT = item.DotDT;
                ds.TenKP = x1 > 0 ? _data.KPhongs.Where(p => p.MaKP == x1).FirstOrDefault().TenKP : "";
                ds1.Add(ds);
            }
            grcDonTV.Enabled = true;
            lup_KhoaKe.EditValue = makp1;
            bindingSource1.DataSource = ds1.ToList();
            grcDonTV.DataSource = bindingSource1;
            int x12 = ds1.Count > 0 ? ds1.FirstOrDefault().IDDontv : 0;
            bindingSource2.DataSource = _data.DThuoctvcts.Where(p => p.IDDontv == x12).ToList();
            grcDontvct.DataSource = bindingSource2;
            grvDontvct.OptionsBehavior.ReadOnly = true;
            this.IDDontv.OptionsColumn.ReadOnly = true;
            Luu.Enabled = false;
            KLuu.Enabled = false;
            Them.Enabled = true;
            if (dt.Count > 0)
            {
                Sua.Enabled = true;
                Xoa.Enabled = true;
                btn_SaoDon.Enabled = true;
            }

            else
            {
                Sua.Enabled = false;
                Xoa.Enabled = false;
                btn_SaoDon.Enabled = false;
            }
            _setHDSDThuoc();
            if (_sHDSDThuoc != null)
                cboGhiChu.Items.AddRange(_sHDSDThuoc);
            grvDonTV_CellValueChanged(null, null);
            grvDonTV_FocusedRowChanged(null, null);
            lup_KhoaKe_EditValueChanged(null, null);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string aa = "";
        int iddd = 0;
        private void grvDonTV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int idd = 0;
            if (grvDonTV.GetFocusedRowCellValue(IDDontv) != null && grvDonTV.GetFocusedRowCellValue(IDDontv).ToString() != "")
                idd = Convert.ToInt32(grvDonTV.GetFocusedRowCellValue(IDDontv).ToString());
            var dtct = _data.DThuoctvcts.Where(p => p.IDDontv == idd).ToList();
            bindingSource2.DataSource = dtct;
            grcDontvct.DataSource = bindingSource2;
            aa = grvDonTV.GetFocusedRowCellValue(x1) != null ? grvDonTV.GetFocusedRowCellValue(x1).ToString() : "";
            lup_BSKe.EditValue = aa;
            lup_KhoaKe.EditValue = grvDonTV.GetFocusedRowCellValue(x2) != null ? Convert.ToInt32(grvDonTV.GetFocusedRowCellValue(x2).ToString()) : 0;
            if (grvDonTV.GetFocusedRowCellValue(NgayKe) != null)
                lup_NgayKe.EditValue = grvDonTV.GetFocusedRowCellValue(NgayKe).ToString();
            else
                lup_NgayKe.EditValue = null;
            if (grvDonTV.GetFocusedRowCellValue(x6) != null)
                NgayTu.EditValue = grvDonTV.GetFocusedRowCellValue(x6).ToString();
            else
                NgayTu.EditValue = null;
            if (grvDonTV.GetFocusedRowCellValue(x5) != null)
                NgayDen.EditValue = grvDonTV.GetFocusedRowCellValue(x5).ToString();
            else
                NgayDen.EditValue = null;
            if (grvDonTV.GetFocusedRowCellValue(x3) != null && grvDonTV.GetFocusedRowCellValue(x3).ToString() != "")
                GhiChu.Text = grvDonTV.GetFocusedRowCellValue(x3).ToString();
            else
                GhiChu.Text = null;
            if (grvDonTV.GetFocusedRowCellValue(x4) != null && grvDonTV.GetFocusedRowCellValue(x4).ToString() != "")
                txtDotDT.Text = grvDonTV.GetFocusedRowCellValue(x4).ToString();
            else
                txtDotDT.Text = null;
            iddd = grvDonTV.GetFocusedRowCellValue(IDDontv) != null && grvDonTV.GetFocusedRowCellValue(IDDontv).ToString() != "0" ? Convert.ToInt32(grvDonTV.GetFocusedRowCellValue(IDDontv).ToString()) : 0;
        }

        private void btn_XoaCT_Click(object sender, EventArgs e)
        {
            if (kieu == 1)
            {
                if (grvDontvct.GetFocusedRowCellValue(IDDontvct) != null && grvDontvct.GetFocusedRowCellValue(IDDontvct).ToString() != "")
                    if (MessageBox.Show("Bạn có muốn xóa thuốc tư vấn không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int idd = Convert.ToInt32(grvDontvct.GetFocusedRowCellValue(IDDontvct).ToString());
                        DThuoctvct xoa2 = _data.DThuoctvcts.Single(p => p.IDDontvct == idd);
                        _data.DThuoctvcts.Remove(xoa2);
                        if (_data.SaveChanges() >= 0)
                        {
                            MessageBox.Show("Xóa thuốc tư vấn thành công!");
                            var dtct = _data.DThuoctvcts.Where(p => p.IDDontv == iddd).ToList();
                            bindingSource2.DataSource = dtct;
                            grcDontvct.DataSource = bindingSource2;
                        }
                    }
                    else
                    {
                        grvDontvct.DeleteRow(grvDontvct.FocusedRowHandle);
                    }
            }
            if (kieu == 0)
                grvDontvct.DeleteRow(grvDontvct.FocusedRowHandle);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            if (kt())
            {
                int dem = 0, dem1 = 0;
                int y = 0;
                for (int i = 0; i < grvDontvct.RowCount; i++)
                {
                    if (grvDontvct.GetRowCellValue(i, SoLuong) != null)
                    {
                        if ((grvDontvct.GetRowCellValue(i, celMaDV) != null && grvDontvct.GetRowCellValue(i, celMaDV).ToString() != "0") || (grvDontvct.GetRowCellValue(i, colTenThuoc) != null && grvDontvct.GetRowCellValue(i, colTenThuoc).ToString() != ""))
                        {

                        }
                        else
                        {
                            MessageBox.Show("Bạn chưa chọn thuốc!", "Thông báo!");
                            dem1++;
                        }
                    }
                    else
                    {
                        if (grvDontvct.GetRowCellValue(i, celMaDV) != null && grvDontvct.GetRowCellValue(i, celMaDV).ToString() != "0")
                        {
                            int x1 = Convert.ToInt32(grvDontvct.GetRowCellValue(i, celMaDV));
                            if (x1 != 0)
                            {
                                string thuoc = _data.DichVus.Where(p => p.MaDV == x1).FirstOrDefault().TenDV;
                                MessageBox.Show("Thuốc: " + thuoc + " chưa điền số lượng!", "Thông báo!");
                                dem1++;
                            }
                        }
                        else if (grvDontvct.GetRowCellValue(i, colTenThuoc) != null && grvDontvct.GetRowCellValue(i, colTenThuoc).ToString() != "")
                        {
                            string thuoc = grvDontvct.GetRowCellValue(i, colTenThuoc).ToString();
                            MessageBox.Show("Thuốc: " + thuoc + " chưa điền số lượng!", "Thông báo!");
                            dem1++;
                        }
                    }
                }
                if (dem1 == 0)
                    if (kieu == 0 || kieu == 2) //thêm mới
                    {

                        DThuoctv moi1 = new DThuoctv();
                        moi1.MaBNhan = _mabn;
                        moi1.MaCB = lup_BSKe.EditValue.ToString();
                        moi1.MaKP = Convert.ToInt32(lup_KhoaKe.EditValue.ToString());
                        moi1.NgayKe = Convert.ToDateTime(lup_NgayKe.DateTime);
                        moi1.GhiChu = GhiChu.Text;
                        moi1.DotDT = txtDotDT.Text;
                        moi1.Loai = 0;
                        if (NgayTu.Text != "")
                            moi1.NgayTu = Convert.ToDateTime(NgayTu.DateTime);
                        if (NgayDen.Text != "")
                            moi1.NgayDen = Convert.ToDateTime(NgayDen.DateTime);
                        _data.DThuoctvs.Add(moi1);
                        _data.SaveChanges();

                        for (int i = 0; i < grvDontvct.RowCount; i++)
                        {
                            int iddontv = moi1.IDDontv;
                            int iddontvct = Convert.ToInt32(grvDontvct.GetRowCellValue(i, IDDontvct));
                            if (kieu == 2)
                                iddontvct = 0;
                            var dtct = _data.DThuoctvcts.Where(p => p.IDDontvct == iddontvct).FirstOrDefault();
                            if (dtct == null)
                            {
                                DThuoctvct moi = new DThuoctvct();
                                moi.IDDontv = moi1.IDDontv;
                                if (grvDontvct.GetRowCellValue(i, celMaDV) != null && grvDontvct.GetRowCellValue(i, celMaDV).ToString() != "0")
                                {
                                    moi.MaDV = Convert.ToInt32(grvDontvct.GetRowCellValue(i, celMaDV));
                                    moi.TenDV = grvDontvct.GetRowCellDisplayText(i, celMaDV);
                                }
                                else
                                {
                                    moi.MaDV = 0;
                                    moi.TenDV = grvDontvct.GetRowCellDisplayText(i, colTenThuoc);

                                }

                                if (grvDontvct.GetRowCellValue(i, DonVi) != null)
                                    moi.DonVi = grvDontvct.GetRowCellValue(i, DonVi).ToString();
                                if (moi.MaDV == 0)
                                {
                                    if (moi.TenDV != null)
                                    {
                                        var dv = data.DichVus.Where(p => p.TenDV == moi.TenDV && p.DonVi == moi.DonVi).FirstOrDefault();
                                        if (dv != null)
                                            moi.MaDV = dv.MaDV;
                                    }
                                }

                                if (grvDontvct.GetRowCellValue(i, SoLan) != null)
                                    moi.SoLan = grvDontvct.GetRowCellValue(i, SoLan).ToString();

                                if (grvDontvct.GetRowCellValue(i, DuongD) != null)
                                    moi.DuongD = grvDontvct.GetRowCellValue(i, DuongD).ToString();

                                if (grvDontvct.GetRowCellValue(i, MoiLan) != null)
                                    moi.MoiLan = grvDontvct.GetRowCellValue(i, MoiLan).ToString();

                                if (grvDontvct.GetRowCellValue(i, DviUong) != null)
                                    moi.DviUong = grvDontvct.GetRowCellValue(i, DviUong).ToString();

                                if (grvDontvct.GetRowCellValue(i, Luong) != null)
                                    moi.Luong = grvDontvct.GetRowCellValue(i, Luong).ToString();

                                if (grvDontvct.GetRowCellValue(i, GhiChu1) != null)
                                    moi.GhiChu = grvDontvct.GetRowCellValue(i, GhiChu1).ToString();
                                if (grvDontvct.GetRowCellValue(i, SoLuong) != null)
                                    moi.SoLuong = Convert.ToInt32(grvDontvct.GetRowCellValue(i, SoLuong).ToString());
                                if ((grvDontvct.GetRowCellValue(i, celMaDV) != null && grvDontvct.GetRowCellValue(i, celMaDV).ToString() != "0") || (grvDontvct.GetRowCellValue(i, colTenThuoc) != null && grvDontvct.GetRowCellValue(i, colTenThuoc).ToString() != ""))
                                {
                                    _data.DThuoctvcts.Add(moi);
                                    dem++;
                                    _data.SaveChanges();
                                }
                            }
                            else
                            {
                                DThuoctvct sua = _data.DThuoctvcts.Single(p => p.IDDontvct == iddontvct);
                                if (grvDontvct.GetRowCellValue(i, celMaDV) != null && grvDontvct.GetRowCellValue(i, celMaDV).ToString() != "0")
                                {
                                    sua.MaDV = Convert.ToInt32(grvDontvct.GetRowCellValue(i, celMaDV));
                                    sua.TenDV = grvDontvct.GetRowCellDisplayText(i, celMaDV);
                                }
                                else
                                {
                                    sua.MaDV = 0;
                                    sua.TenDV = grvDontvct.GetRowCellDisplayText(i, colTenThuoc);
                                }

                                if (grvDontvct.GetRowCellValue(i, DonVi) != null)
                                    sua.DonVi = grvDontvct.GetRowCellValue(i, DonVi).ToString();
                                if (sua.MaDV == 0)
                                {
                                    if (sua.TenDV != null)
                                    {
                                        var dv = data.DichVus.Where(p => p.TenDV == sua.TenDV && p.DonVi == sua.DonVi).FirstOrDefault();
                                        if (dv != null)
                                            sua.MaDV = dv.MaDV;
                                    }
                                }
                                if (grvDontvct.GetRowCellValue(i, SoLuong) != null)
                                    sua.SoLuong = Convert.ToInt32(grvDontvct.GetRowCellValue(i, SoLuong).ToString());

                                if (grvDontvct.GetRowCellValue(i, SoLan) != null)
                                    sua.SoLan = grvDontvct.GetRowCellValue(i, SoLan).ToString();

                                if (grvDontvct.GetRowCellValue(i, DuongD) != null)
                                    sua.SoLan = grvDontvct.GetRowCellValue(i, DuongD).ToString();

                                if (grvDontvct.GetRowCellValue(i, MoiLan) != null)
                                    sua.MoiLan = grvDontvct.GetRowCellValue(i, MoiLan).ToString();

                                if (grvDontvct.GetRowCellValue(i, DviUong) != null)
                                    sua.DviUong = grvDontvct.GetRowCellValue(i, DviUong).ToString();

                                if (grvDontvct.GetRowCellValue(i, Luong) != null)
                                    sua.Luong = grvDontvct.GetRowCellValue(i, Luong).ToString();

                                if (grvDontvct.GetRowCellValue(i, GhiChu1) != null)
                                    sua.GhiChu = grvDontvct.GetRowCellValue(i, GhiChu1).ToString();
                                dem++;
                            }
                        }
                    }
                    else //sửa
                    {
                        int idd = 0;
                        if (grvDonTV.GetFocusedRowCellValue(IDDontv) != null && grvDonTV.GetFocusedRowCellValue(IDDontv).ToString() != "")
                            idd = Convert.ToInt32(grvDonTV.GetFocusedRowCellValue(IDDontv).ToString());
                        DThuoctv sua = _data.DThuoctvs.Single(p => p.IDDontv == idd);
                        sua.MaCB = lup_BSKe.EditValue.ToString();
                        sua.MaKP = Convert.ToInt32(lup_KhoaKe.EditValue.ToString());
                        sua.NgayKe = Convert.ToDateTime(lup_NgayKe.DateTime);
                        sua.GhiChu = GhiChu.Text;
                        sua.DotDT = txtDotDT.Text;
                        sua.Loai = 0;
                        if (NgayTu.Text != "")
                            sua.NgayTu = Convert.ToDateTime(NgayTu.DateTime);
                        if (NgayDen.Text != "")
                            sua.NgayDen = Convert.ToDateTime(NgayDen.DateTime);
                        _data.SaveChanges();
                        for (int i = 0; i < grvDontvct.RowCount; i++)
                        {
                            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                            int iddontvct = Convert.ToInt32(grvDontvct.GetRowCellValue(i, IDDontvct));
                            var dtct = _data.DThuoctvcts.Where(p => p.IDDontvct == iddontvct).FirstOrDefault();
                            if (dtct == null)
                            {
                                DThuoctvct moi2 = new DThuoctvct();
                                moi2.IDDontv = idd;
                                if (grvDontvct.GetRowCellValue(i, celMaDV) != null && grvDontvct.GetRowCellValue(i, celMaDV).ToString() != "0")
                                {
                                    moi2.MaDV = Convert.ToInt32(grvDontvct.GetRowCellValue(i, celMaDV));
                                    moi2.TenDV = grvDontvct.GetRowCellDisplayText(i, celMaDV);
                                }
                                else
                                {
                                    moi2.MaDV = 0;
                                    moi2.TenDV = grvDontvct.GetRowCellDisplayText(i, colTenThuoc);
                                }
                                if (grvDontvct.GetRowCellValue(i, SoLuong) != null)
                                    moi2.SoLuong = Convert.ToInt32(grvDontvct.GetRowCellValue(i, SoLuong).ToString());

                                if (grvDontvct.GetRowCellValue(i, DonVi) != null)
                                    moi2.DonVi = grvDontvct.GetRowCellValue(i, DonVi).ToString();
                                if (moi2.MaDV == 0)
                                {
                                    if (moi2.TenDV != null)
                                    {
                                        var dv = data.DichVus.Where(p => p.TenDV == moi2.TenDV && p.DonVi == moi2.DonVi).FirstOrDefault();
                                        if (dv != null)
                                            moi2.MaDV = dv.MaDV;
                                    }
                                }
                                if (grvDontvct.GetRowCellValue(i, SoLan) != null)
                                    moi2.SoLan = grvDontvct.GetRowCellValue(i, SoLan).ToString();

                                if (grvDontvct.GetRowCellValue(i, DuongD) != null)
                                    moi2.DuongD = grvDontvct.GetRowCellValue(i, DuongD).ToString();

                                if (grvDontvct.GetRowCellValue(i, MoiLan) != null)
                                    moi2.MoiLan = grvDontvct.GetRowCellValue(i, MoiLan).ToString();

                                if (grvDontvct.GetRowCellValue(i, DviUong) != null)
                                    moi2.DviUong = grvDontvct.GetRowCellValue(i, DviUong).ToString();

                                if (grvDontvct.GetRowCellValue(i, Luong) != null)
                                    moi2.Luong = grvDontvct.GetRowCellValue(i, Luong).ToString();

                                if (grvDontvct.GetRowCellValue(i, GhiChu1) != null)
                                    moi2.GhiChu = grvDontvct.GetRowCellValue(i, GhiChu1).ToString();
                                if ((grvDontvct.GetRowCellValue(i, celMaDV) != null && grvDontvct.GetRowCellValue(i, celMaDV).ToString() != "0") || (grvDontvct.GetRowCellValue(i, colTenThuoc) != null && grvDontvct.GetRowCellValue(i, colTenThuoc).ToString() != ""))
                                {
                                    _data.DThuoctvcts.Add(moi2);
                                    dem++;
                                    _data.SaveChanges();
                                }
                            }
                            else
                            {
                                DThuoctvct sua1 = _data.DThuoctvcts.Single(p => p.IDDontvct == iddontvct);
                                if (grvDontvct.GetRowCellValue(i, celMaDV) != null && grvDontvct.GetRowCellValue(i, celMaDV).ToString() != "0")
                                {
                                    sua1.MaDV = Convert.ToInt32(grvDontvct.GetRowCellValue(i, celMaDV));
                                    sua1.TenDV = grvDontvct.GetRowCellDisplayText(i, celMaDV);
                                }
                                else
                                {
                                    sua1.MaDV = 0;
                                    sua1.TenDV = grvDontvct.GetRowCellDisplayText(i, colTenThuoc);
                                }

                                if (grvDontvct.GetRowCellValue(i, DonVi) != null)
                                    sua1.DonVi = grvDontvct.GetRowCellValue(i, DonVi).ToString();
                                if (sua1.MaDV == 0)
                                {
                                    if (sua1.TenDV != null)
                                    {
                                        var dv = data.DichVus.Where(p => p.TenDV == sua1.TenDV && p.DonVi == sua1.DonVi).FirstOrDefault();
                                        if (dv != null)
                                            sua1.MaDV = dv.MaDV;
                                    }
                                }
                                if (grvDontvct.GetRowCellValue(i, SoLuong) != null)
                                    sua1.SoLuong = Convert.ToInt32(grvDontvct.GetRowCellValue(i, SoLuong).ToString());

                                if (grvDontvct.GetRowCellValue(i, SoLan) != null)
                                    sua1.SoLan = grvDontvct.GetRowCellValue(i, SoLan).ToString();

                                if (grvDontvct.GetRowCellValue(i, DuongD) != null)
                                    sua1.SoLan = grvDontvct.GetRowCellValue(i, DuongD).ToString();

                                if (grvDontvct.GetRowCellValue(i, MoiLan) != null)
                                    sua1.MoiLan = grvDontvct.GetRowCellValue(i, MoiLan).ToString();

                                if (grvDontvct.GetRowCellValue(i, DviUong) != null)
                                    sua1.DviUong = grvDontvct.GetRowCellValue(i, DviUong).ToString();

                                if (grvDontvct.GetRowCellValue(i, Luong) != null)
                                    sua1.Luong = grvDontvct.GetRowCellValue(i, Luong).ToString();

                                if (grvDontvct.GetRowCellValue(i, GhiChu1) != null)
                                    sua1.GhiChu = grvDontvct.GetRowCellValue(i, GhiChu1).ToString();
                                dem++;
                            }
                        }

                    }
                if (dem > 0 && y == 0)
                {
                    MessageBox.Show("Lưu thành công");
                    frm_KeDonTV_Load(sender, e);
                }
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            frm_KeDonTV_Load(sender, e);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            grvDontvct.OptionsBehavior.ReadOnly = false;
            lup_BSKe.Enabled = true;
            lup_KhoaKe.EditValue = makp1;
            lup_NgayKe.Enabled = true;
            lup_NgayKe.DateTime = DateTime.Now;
            GhiChu.Enabled = true;
            txtDotDT.Enabled = true;
            txtDotDT.Text = "";
            NgayTu.Enabled = true;
            NgayTu.DateTime = DateTime.Now;
            NgayDen.Enabled = true;
            NgayDen.DateTime = DateTime.Now;
            Them.Enabled = false;
            Luu.Enabled = true;
            KLuu.Enabled = true;
            Sua.Enabled = false;
            Xoa.Enabled = false;
            GhiChu.Text = "";
            kieu = 0;
            var dtct = _data.DThuoctvcts.Where(p => p.IDDontv == 0).ToList();
            bindingSource2.DataSource = dtct;
            grcDontvct.DataSource = bindingSource2;
            grcDonTV.Enabled = false;
            btn_SaoDon.Enabled = false;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            grvDontvct.OptionsBehavior.ReadOnly = false;
            lup_BSKe.Enabled = true;
            lup_NgayKe.Enabled = true;
            txtDotDT.Enabled = true;
            NgayTu.Enabled = true;
            NgayDen.Enabled = true;
            GhiChu.Enabled = true;
            Sua.Enabled = false;
            Luu.Enabled = true;
            KLuu.Enabled = true;
            Xoa.Enabled = false;
            Them.Enabled = false;
            lup_KhoaKe_EditValueChanged(null, null);
            kieu = 1;
            grcDonTV.Enabled = false;
            btn_SaoDon.Enabled = false;
        }


        private void lup_Thuoc_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void grvDontvct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "celMaDV":
                    int madv = 0;
                    if (grvDontvct.GetFocusedRowCellValue(celMaDV) != null)
                    {
                        madv = Convert.ToInt32(grvDontvct.GetFocusedRowCellValue(celMaDV));
                        int thuoc1 = 0;
                        if (grvDontvct.RowCount > 0)
                            for (int i = 0; i < grvDontvct.RowCount; i++)
                            {
                                if (grvDontvct.GetRowCellValue(i, celMaDV) != null)
                                {
                                    int madv1 = Convert.ToInt32(grvDontvct.GetRowCellValue(i, celMaDV));
                                    if (madv == madv1 && i != grvDontvct.FocusedRowHandle)
                                    {
                                        thuoc1++;
                                    }
                                }
                            }
                        if (thuoc1 > 0)
                        {
                            if (MessageBox.Show("Thuốc đã kê, bạn có muốn nhập tiếp?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {

                            }
                            else
                                grvDontvct.DeleteRow(grvDontvct.FocusedRowHandle);
                        }
                        grvDontvct.SetFocusedRowCellValue(DonVi, Ham._getDonVi(_data, madv));
                        grvDontvct.SetFocusedRowCellValue(DuongD, _getDDung(madv));
                        grvDontvct.SetFocusedRowCellValue(MoiLan, " lần, mỗi lần ");
                        grvDontvct.SetFocusedRowCellValue(SoLan, " 1 ");
                        grvDontvct.SetFocusedRowCellValue(Luong, " 1 ");
                        grvDontvct.SetFocusedRowCellValue(DviUong, " " + Ham._getDonVi(_data, madv));
                    }
                    break;
                case "colTenThuoc":
                    string tenthuoc = "";
                    if (grvDontvct.GetFocusedRowCellValue(colTenThuoc) != null)
                        tenthuoc = grvDontvct.GetFocusedRowCellValue(colTenThuoc).ToString();
                    int count = 0;
                    if (grvDontvct.RowCount > 0)
                        for (int i = 0; i < grvDontvct.RowCount; i++)
                        {
                            if (grvDontvct.GetRowCellValue(i, colTenThuoc) != null)
                            {
                                string tenthuoc1 = grvDontvct.GetRowCellValue(i, colTenThuoc).ToString();
                                if (tenthuoc == tenthuoc1 && i != grvDontvct.FocusedRowHandle)
                                {
                                    count++;
                                }
                            }
                        }
                    if (count > 0)
                    {
                        if (MessageBox.Show("Thuốc đã kê, bạn có muốn nhập tiếp?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                        }
                        else
                            grvDontvct.DeleteRow(grvDontvct.FocusedRowHandle);
                    }
                    string donvi = "", DDung = "";
                    var qdv = _data.DichVus.Where(p => p.TenDV == tenthuoc).FirstOrDefault();
                    if (qdv != null)
                    {
                        donvi = qdv.DonVi;
                        DDung = qdv.DuongD;
                    }
                    grvDontvct.SetFocusedRowCellValue(DonVi, donvi);
                    grvDontvct.SetFocusedRowCellValue(DuongD, DDung);
                    grvDontvct.SetFocusedRowCellValue(MoiLan, " lần, mỗi lần ");
                    grvDontvct.SetFocusedRowCellValue(SoLan, " 1 ");
                    grvDontvct.SetFocusedRowCellValue(Luong, " 1 ");
                    grvDontvct.SetFocusedRowCellValue(DviUong, " " + donvi);

                    break;
            }
        }
        private string _getDDung(int madv)
        {
            try
            {
                string dd = "";
                var ddung = _data.DichVus.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).Where(p => p.MaDV == madv).Select(p => p.DuongD).ToList();
                if (ddung.Count > 0)
                {
                    if (ddung.First() != null)
                        dd = ddung.First().ToString() + " ngày ";
                    else
                        dd = "";
                }
                return dd;
            }
            catch (Exception)
            {
                MessageBox.Show("Thuốc chưa có đường dùng");
                return "";
            }
        }

        private void grvDonTV_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string makp = "";
            if (grvDonTV.GetFocusedRowCellValue(MaKP) != null)
            {
                makp = grvDonTV.GetFocusedRowCellValue(MaKP).ToString(); ;
            }
            var cb = _data.CanBoes.Where(p => p.MaKPsd.Contains(makp) || makp == "").Select(p => new { p.MaCB, p.TenCB }).ToList();
            lup_BSKe.Properties.DataSource = cb;

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa đơn tư vấn không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int idd = Convert.ToInt32(grvDonTV.GetFocusedRowCellValue(IDDontv).ToString());
                var xoa2 = _data.DThuoctvcts.Where(p => p.IDDontv == idd).ToList();
                foreach (var item in xoa2)
                {
                    DThuoctvct xoact = _data.DThuoctvcts.Single(p => p.IDDontvct == item.IDDontvct);
                    _data.DThuoctvcts.Remove(xoact);
                }
                DThuoctv xoa1 = _data.DThuoctvs.Single(p => p.IDDontv == idd);
                _data.DThuoctvs.Remove(xoa1);
                if (_data.SaveChanges() >= 0)
                {
                    MessageBox.Show("Xóa đơn tư vấn thành công!");
                }
                frm_KeDonTV_Load(sender, e);
            }
        }

        private void lup_KhoaKe_EditValueChanged(object sender, EventArgs e)
        {
            string x = "";
            if (lup_KhoaKe.EditValue != null && lup_KhoaKe.EditValue.ToString() != "")
                x = lup_KhoaKe.EditValue.ToString();
            var cb = _data.CanBoes.Where(p => p.MaKPsd.Contains(x)).Select(p => new { p.MaCB, p.TenCB }).ToList();
            lup_BSKe.Properties.DataSource = cb;
            if (aa != "")
                lup_BSKe.EditValue = aa;
        }

        public static void Hamindon(int _mabn,int _idkb, int iddd)
        {
            QLBV_Database.QLBVEntities DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            

            var ktkd = (from dt in DaTaContext.DThuoctvs.Where(p => p.IDDontv == iddd)
                        join cb in DaTaContext.CanBoes on dt.MaCB equals cb.MaCB
                        join kp in DaTaContext.KPhongs on dt.MaKP equals kp.MaKP
                        select new { dt.GhiChu, dt.IDDontv, dt.MaBNhan, dt.NgayKe, cb.TenCB, cb.CapBac, kp.TenKP, dt.NgayTu, dt.NgayDen, dt.DotDT }).ToList();

            if (ktkd.Count > 0)// kiểm tra có đơn thuốc hay chưa
            {
                var ttd = (from bn in DaTaContext.BenhNhans.Where(p => p.MaBNhan == _mabn)
                           join bntt in DaTaContext.TTboXungs on bn.MaBNhan equals bntt.MaBNhan
                           join kb in DaTaContext.BNKBs.Where(p => p.IDKB == _idkb) on bn.MaBNhan equals kb.MaBNhan
                           select new { bntt.NThan, kb.NgayKham, bn.GTinh, bn.TenBNhan, bn.NamSinh, bn.NgaySinh, bn.ThangSinh, bn.NNhap, kb.MaICD, kb.ChanDoan, kb.BenhKhac, kb.IDKB, bn.SThe, bn.DChi, kb.GhiChu, bntt.SoKSinh }).OrderByDescending(p => p.IDKB).ToList();
                var qdv = (from dv in DaTaContext.DichVus.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6)
                           join tndv in DaTaContext.TieuNhomDVs on dv.IdTieuNhom equals tndv.IdTieuNhom
                           select new { dv.MaDV, dv.TenDV, tndv.TenRG, dv.DonVi }).ToList();

                var qdtct = DaTaContext.DThuoctvcts.Where(p => p.IDDontv == iddd).ToList();
                List<Don> listDSThuoc = new List<Don>();
                foreach (var a in qdtct)
                {
                    Don moi = new Don();
                    moi.MaDV = a.MaDV ?? 0;
                    moi.TenDV = a.TenDV;
                    moi.DonVi = a.DonVi;
                    moi.SoLuong = a.SoLuong;
                    moi.HuongDan = a.DuongD + a.SoLan + a.MoiLan + a.Luong + a.DviUong + ((a.GhiChu != null && a.GhiChu != "") ? (", " + a.GhiChu) : "");
                    if (moi.MaDV > 0)
                    {
                        var qtenrg = qdv.Where(p => p.MaDV == moi.MaDV).FirstOrDefault();
                        if (qtenrg != null)
                            moi.TenRG = qtenrg.TenRG;
                    }
                    else
                    {
                        var qtenrg = qdv.Where(p => p.TenDV == moi.TenDV).FirstOrDefault();
                        if (qtenrg != null)
                            moi.TenRG = qtenrg.TenRG;
                    }
                    listDSThuoc.Add(moi);
                }

                var qd1 = (from dtct in listDSThuoc
                           group dtct by new { dtct.MaDV, dtct.TenDV, dtct.DonVi, dtct.HuongDan, dtct.TenRG } into kq
                           select new Don { TenRG = kq.Key.TenRG, TenDV = kq.Key.TenDV, MaDV = kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.SoLuong), HuongDan = kq.Key.HuongDan }).OrderBy(p => p.MaDV).ToList();
                //var qd1 = (from dv in DaTaContext.DichVus.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6)
                //           join tndv in DaTaContext.TieuNhomDVs on dv.IdTieuNhom equals tndv.IdTieuNhom
                //           join dtct in DaTaContext.DThuoctvcts on dv.MaDV equals dtct.MaDV
                //           where (dtct.IDDontv == iddd)
                //           group new { dv, dtct, tndv } by new { dv.MaDV, dv.TenDV, dv.DonVi, dtct.DuongD, dtct.SoLan, dtct.MoiLan, dtct.Luong, dtct.DviUong, tndv.TenRG, dtct.GhiChu } into kq
                //           select new Don {TenRG = kq.Key.TenRG, TenDV = kq.Key.TenDV,MaDV = kq.Key.MaDV,DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dtct.SoLuong), HuongDan = kq.Key.DuongD + kq.Key.SoLan + kq.Key.MoiLan + kq.Key.Luong + kq.Key.DviUong + ((kq.Key.GhiChu != null && kq.Key.GhiChu != "") ? (", " + kq.Key.GhiChu) : "") }).OrderBy(p => p.MaDV).ToList();
                var don1 = qd1.Where(p => p.TenRG == "Thuốc gây nghiện").ToList();
                var don2 = qd1.Where(p => p.TenRG != "Thuốc gây nghiện").ToList();
                frmIn frm = new frmIn();

                if (don2.Count > 0)
                {
                    string tuoi = DungChung.Ham.TuoitheoThang(DaTaContext, _mabn, "72-00");
                    if (tuoi.Length > 3)
                    {
                        var ktratt = DaTaContext.TTboXungs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                        if (ktratt != null)
                        {
                            if (string.IsNullOrEmpty(ktratt.NThan) || !ktratt.NThan.Contains(";") || ktratt.NThan.Split(';').Length < 2)
                            {
                                MessageBox.Show("với bệnh nhân dưới 72 tháng tuổi cần có thông tin người thân");
                                frm_TTNgThanTreEm frm1 = new frm_TTNgThanTreEm(_mabn);
                                frm1.ShowDialog();
                            }
                        }
                    }
                    BaoCao.repDonThuoc_TT05_30007 repd = new BaoCao.repDonThuoc_TT05_30007();
                    repd._TenBNhan.Value = ttd.First().TenBNhan;
                    repd.Tuoi.Value = DungChung.Ham.TuoitheoThang(DaTaContext, _mabn, DungChung.Bien.formatAge);
                    repd.lblTuoi.Text = "Tuổi:";
                    //  repd.Tuoi.Value = DungChung.Ham.TuoitheoThang(_data, _IDBN, DungChung.Bien.formatAge);
                    //Lời dặn, họ tên người thân
                    string Tuoi = "";
                    Tuoi = DungChung.Ham.TuoitheoThang(DaTaContext, _mabn, "72-00");
                    repd.Tuoi.Value = Tuoi;
                    repd.lblTuoi.Text = "Tuổi:";
                    if (Tuoi.Length > 2)
                    {
                        if (!string.IsNullOrEmpty(ttd.First().NThan) && ttd.First().NThan.Contains(";"))
                        {
                            string[] arrnt = ttd.First().NThan.Split(';');
                            repd.paraHoTenNguoiThan.Value = arrnt[0];
                            repd.CMTND.Value = arrnt[1];
                        }
                        else
                        {
                            repd.paraHoTenNguoiThan.Value = ttd.First().NThan;
                        }
                    }

                    //if (ar.Length > 1)
                    //    repd.paraLoDanBS.Value = ar[1];

                    // KT
                    switch (ttd.First().GTinh)
                    {
                        case 1:
                            repd.GTinh.Value = "Nam";
                            repd.Nu.Value = "/";
                            break;
                        case 0:
                            repd.GTinh.Value = "Nữ";
                            repd.Nam.Value = "/";
                            break;
                    }
                    repd.ICD.Value = DungChung.Ham.getMaICDarr(DaTaContext, _mabn, DungChung.Bien.GetICD, 0)[0];
                    repd.SThe.Value = ttd.First().SThe;
                    repd.ChanDoan.Value = DungChung.Ham.getMaICDarr(DaTaContext, _mabn, DungChung.Bien.GetICD, _idkb)[1];
                    repd.DiaChi.Value = ttd.First().DChi;
                    repd._MaBNhan.Value = _mabn.ToString();


                    repd._idDon.Value = iddd;
                    if (ktkd.Count > 0 && ktkd.First().GhiChu != null)
                        repd.paraLoDanBS.Value = ktkd.First().GhiChu;
                    if (ktkd.Count > 0)
                    {

                        if (ktkd.First().NgayKe.Value.Day > 0)
                            repd.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
                    }
                    if (DungChung.Bien.MaBV == "30007")//in theo id khám bệnh của kp in đơn
                    {
                        var bnkb1 = DaTaContext.BNKBs.Where(p => p.IDKB == _idkb).FirstOrDefault();
                        if (bnkb1 != null)
                        {
                            string macb = bnkb1.MaCB;
                            var ttcb = DaTaContext.CanBoes.Where(p => p.MaCB == macb).FirstOrDefault();
                            if (ttcb != null)
                            {
                                repd.TenCB.Value = ttcb.CapBac + ": " + ttcb.TenCB;
                            }
                            int makp1 = bnkb1.MaKP ?? 0;
                            var ttkp = DaTaContext.KPhongs.Where(p => p.MaKP == makp1).FirstOrDefault();
                            if (ttkp != null)
                            {
                                repd.TenKP.Value = ttkp.TenKP;
                            }
                        }
                    }
                    else
                    {
                        repd.TenCB.Value = ktkd.First().CapBac + ": " + ktkd.First().TenCB;
                        repd.TenKP.Value = ktkd.Count > 0 ? ktkd.First().TenKP : "";
                    }
                    repd.DataSource = don2.ToList();
                    repd.ThuKho.Value = DungChung.Bien.ThuKho;
                    repd.BindData();
                    repd.CreateDocument();
                    frm.prcIN.PrintingSystem = repd.PrintingSystem;
                    frm.ShowDialog();
                }
                if (don1.Count > 0)
                    for (int i = 1; i < 4; i++)
                    {

                        BaoCao.repDonThuocTV repd = new BaoCao.repDonThuocTV();
                        repd._idDon.Value = ktkd.First().IDDontv;
                        if (ttd.Count > 0)
                        {
                            string dotdt = (ktkd.First().DotDT != "" && ktkd.First().DotDT != null ? ktkd.First().DotDT : "......") + " từ ngày ";
                            if (ktkd.First().NgayTu != null)
                                dotdt = dotdt + ktkd.First().NgayTu.Value.Day + "/" + ktkd.First().NgayTu.Value.Month + "/" + ktkd.First().NgayTu.Value.Year + " đến ngày ";
                            else
                                dotdt = dotdt + ".../..../......." + " đến ngày ";
                            if (ktkd.First().NgayDen != null)
                                dotdt = dotdt + ktkd.First().NgayDen.Value.Day + "/" + ktkd.First().NgayDen.Value.Month + "/" + ktkd.First().NgayDen.Value.Year;
                            else
                                dotdt = dotdt + ".../..../.......";
                            repd.paraDotDieuTri.Value = dotdt;
                            repd._TenBNhan.Value = ttd.First().TenBNhan;
                            repd.Tuoi.Value = DungChung.Ham.TuoitheoThang(DaTaContext, _mabn, DungChung.Bien.formatAge);
                            // KT
                            switch (ttd.First().GTinh)
                            {
                                case 1:
                                    repd.GTinh.Value = "Nam";
                                    repd.Nu.Value = "/";
                                    break;
                                case 0:
                                    repd.GTinh.Value = "Nữ";
                                    repd.Nam.Value = "/";
                                    break;
                            }
                            repd.ICD.Value = DungChung.Ham.getMaICDarr(DaTaContext, _mabn, DungChung.Bien.GetICD, 0)[0];
                            repd.SThe.Value = ttd.First().SThe;
                            repd.ChanDoan.Value = DungChung.Ham.getMaICDarr(DaTaContext, _mabn, DungChung.Bien.GetICD, 0)[1];
                            repd.TenCB.Value = ktkd.First().CapBac + ": " + ktkd.First().TenCB;
                            repd.TenKP.Value = ktkd.First().TenKP;
                            repd.DiaChi.Value = ttd.First().DChi;
                            repd._MaBNhan.Value = _mabn.ToString();
                            if (ktkd.First().NgayKe.Value.Day > 0)
                                repd.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
                        }
                        repd.DataSource = qd1.Where(p => p.TenRG == "Thuốc gây nghiện").ToList();
                        repd.ThuKho.Value = DungChung.Bien.ThuKho;
                        if (i == 1)
                        {
                            repd.lblLien.Text = "(Bản lưu tại cơ sở khám bệnh, chữa bệnh)";
                            repd.lblNguoiNhanThuoc.Text = "";
                            repd.txtbnky.Text = "";
                            repd.txttenBN.Text = "";
                            repd.txtCMTND.Text = "";
                            repd.xrTableCell7.Text = "";
                            repd.xrTableCell10.Text = "";
                        }
                        else if (i == 2)
                        {
                            repd.lblLien.Text = "(Bản lưu tại cơ sở cấp, bán thuốc)";
                            repd.lblNguoiNhanThuoc.Text = "NGƯỜI NHẬN THUỐC";
                            repd.txtbnky.Text = "(Ký, ghi rõ họ tên và số chứng minh nhân dân/ căn cước công dân)";
                            repd.xrTableCell7.Text = "Lô sản xuất:........";
                            repd.xrTableCell10.Text = "Hạn dùng:..........";
                            repd.txttenBN.Text = "";
                            //if (ttd.First().SoKSinh != null && ttd.First().SoKSinh != "")
                            //    repd.txtCMTND.Text = "  CMTND/CCCD: " + ttd.First().SoKSinh;
                            //else
                            repd.txtCMTND.Text = "";
                        }
                        else if (i == 3)
                        {
                            repd.lblLien.Text = "(Bản giao cho người bệnh)";
                            repd.lblNguoiNhanThuoc.Text = "";
                            repd.txtbnky.Text = "";
                            repd.txttenBN.Text = "";
                            repd.xrTableCell7.Text = "";
                            repd.xrTableCell10.Text = "";
                            repd.txtCMTND.Text = "";
                        }
                        repd.BindData();
                        repd.CreateDocument();
                        frm.prcIN.PrintingSystem = repd.PrintingSystem;
                        frm.ShowDialog();
                    }
            }
        }


        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);


            var ktkd = (from dt in DaTaContext.DThuoctvs.Where(p => p.IDDontv == iddd)
                        join cb in DaTaContext.CanBoes on dt.MaCB equals cb.MaCB
                        join kp in DaTaContext.KPhongs on dt.MaKP equals kp.MaKP
                        select new { dt.GhiChu, dt.IDDontv, dt.MaBNhan, dt.NgayKe, cb.TenCB, cb.CapBac, kp.TenKP, dt.NgayTu, dt.NgayDen, dt.DotDT }).ToList();

            if (ktkd.Count > 0)// kiểm tra có đơn thuốc hay chưa
            {
                var ttd = (from bn in DaTaContext.BenhNhans.Where(p => p.MaBNhan == _mabn)
                           join bntt in DaTaContext.TTboXungs on bn.MaBNhan equals bntt.MaBNhan
                           join kb in DaTaContext.BNKBs.Where(p => p.IDKB == _idkb) on bn.MaBNhan equals kb.MaBNhan
                           select new { bntt.NThan, kb.NgayKham, bn.GTinh, bn.TenBNhan, bn.NamSinh, bn.NgaySinh, bn.ThangSinh, bn.NNhap, kb.MaICD, kb.ChanDoan, kb.BenhKhac, kb.IDKB, bn.SThe, bn.DChi, kb.GhiChu, bntt.SoKSinh }).OrderByDescending(p => p.IDKB).ToList();
                var qdv = (from dv in DaTaContext.DichVus.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6)
                           join tndv in DaTaContext.TieuNhomDVs on dv.IdTieuNhom equals tndv.IdTieuNhom
                           select new { dv.MaDV, dv.TenDV, tndv.TenRG, dv.DonVi }).ToList();

                var qdtct = DaTaContext.DThuoctvcts.Where(p => p.IDDontv == iddd).ToList();
                List<Don> listDSThuoc = new List<Don>();
                foreach (var a in qdtct)
                {
                    Don moi = new Don();
                    moi.MaDV = a.MaDV ?? 0;
                    moi.TenDV = a.TenDV;
                    moi.DonVi = a.DonVi;
                    moi.SoLuong = a.SoLuong;
                    moi.HuongDan = a.DuongD + a.SoLan + a.MoiLan + a.Luong + a.DviUong + ((a.GhiChu != null && a.GhiChu != "") ? (", " + a.GhiChu) : "");
                    if (moi.MaDV > 0)
                    {
                        var qtenrg = qdv.Where(p => p.MaDV == moi.MaDV).FirstOrDefault();
                        if (qtenrg != null)
                            moi.TenRG = qtenrg.TenRG;
                    }
                    else
                    {
                        var qtenrg = qdv.Where(p => p.TenDV == moi.TenDV).FirstOrDefault();
                        if (qtenrg != null)
                            moi.TenRG = qtenrg.TenRG;
                    }
                    listDSThuoc.Add(moi);
                }

                var qd1 = (from dtct in listDSThuoc
                           group dtct by new { dtct.MaDV, dtct.TenDV, dtct.DonVi, dtct.HuongDan, dtct.TenRG } into kq
                           select new Don { TenRG = kq.Key.TenRG, TenDV = kq.Key.TenDV, MaDV = kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.SoLuong), HuongDan = kq.Key.HuongDan }).OrderBy(p => p.MaDV).ToList();
                //var qd1 = (from dv in DaTaContext.DichVus.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6)
                //           join tndv in DaTaContext.TieuNhomDVs on dv.IdTieuNhom equals tndv.IdTieuNhom
                //           join dtct in DaTaContext.DThuoctvcts on dv.MaDV equals dtct.MaDV
                //           where (dtct.IDDontv == iddd)
                //           group new { dv, dtct, tndv } by new { dv.MaDV, dv.TenDV, dv.DonVi, dtct.DuongD, dtct.SoLan, dtct.MoiLan, dtct.Luong, dtct.DviUong, tndv.TenRG, dtct.GhiChu } into kq
                //           select new Don {TenRG = kq.Key.TenRG, TenDV = kq.Key.TenDV,MaDV = kq.Key.MaDV,DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dtct.SoLuong), HuongDan = kq.Key.DuongD + kq.Key.SoLan + kq.Key.MoiLan + kq.Key.Luong + kq.Key.DviUong + ((kq.Key.GhiChu != null && kq.Key.GhiChu != "") ? (", " + kq.Key.GhiChu) : "") }).OrderBy(p => p.MaDV).ToList();
                var don1 = qd1.Where(p => p.TenRG == "Thuốc gây nghiện").ToList();
                var don2 = qd1.Where(p => p.TenRG != "Thuốc gây nghiện").ToList();
                frmIn frm = new frmIn();

                if (don2.Count > 0)
                {
                    string tuoi = DungChung.Ham.TuoitheoThang(_data, _mabn, "72-00");
                    if (tuoi.Length > 3)
                    {
                        var ktratt = _data.TTboXungs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                        if (ktratt != null)
                        {
                            if (string.IsNullOrEmpty(ktratt.NThan) || !ktratt.NThan.Contains(";") || ktratt.NThan.Split(';').Length < 2)
                            {
                                MessageBox.Show("với bệnh nhân dưới 72 tháng tuổi cần có thông tin người thân");
                                frm_TTNgThanTreEm frm1 = new frm_TTNgThanTreEm(_mabn);
                                frm1.ShowDialog();
                            }
                        }
                    }
                    BaoCao.repDonThuoc_TT05_30007 repd = new BaoCao.repDonThuoc_TT05_30007();
                    repd._TenBNhan.Value = ttd.First().TenBNhan;
                    repd.Tuoi.Value = DungChung.Ham.TuoitheoThang(_data, _mabn, DungChung.Bien.formatAge);
                    repd.lblTuoi.Text = "Tuổi:";
                    //  repd.Tuoi.Value = DungChung.Ham.TuoitheoThang(_data, _IDBN, DungChung.Bien.formatAge);
                    //Lời dặn, họ tên người thân
                    string Tuoi = "";
                    Tuoi = DungChung.Ham.TuoitheoThang(_data, _mabn, "72-00");
                    repd.Tuoi.Value = Tuoi;
                    repd.lblTuoi.Text = "Tuổi:";
                    if (Tuoi.Length > 2)
                    {
                        if (!string.IsNullOrEmpty(ttd.First().NThan) && ttd.First().NThan.Contains(";"))
                        {
                            string[] arrnt = ttd.First().NThan.Split(';');
                            repd.paraHoTenNguoiThan.Value = arrnt[0];
                            repd.CMTND.Value = arrnt[1];
                        }
                        else
                        {
                            repd.paraHoTenNguoiThan.Value = ttd.First().NThan;
                        }
                    }

                    //if (ar.Length > 1)
                    //    repd.paraLoDanBS.Value = ar[1];

                    // KT
                    switch (ttd.First().GTinh)
                    {
                        case 1:
                            repd.GTinh.Value = "Nam";
                            repd.Nu.Value = "/";
                            break;
                        case 0:
                            repd.GTinh.Value = "Nữ";
                            repd.Nam.Value = "/";
                            break;
                    }
                    repd.ICD.Value = DungChung.Ham.getMaICDarr(_data, _mabn, DungChung.Bien.GetICD, 0)[0];
                    repd.SThe.Value = ttd.First().SThe;
                    repd.ChanDoan.Value = DungChung.Ham.getMaICDarr(_data, _mabn, DungChung.Bien.GetICD, _idkb)[1];
                    repd.DiaChi.Value = ttd.First().DChi;
                    repd._MaBNhan.Value = _mabn.ToString();


                    repd._idDon.Value = iddd;
                    if (ktkd.Count > 0 && ktkd.First().GhiChu != null)
                        repd.paraLoDanBS.Value = ktkd.First().GhiChu;
                    if (ktkd.Count > 0)
                    {

                        if (ktkd.First().NgayKe.Value.Day > 0)
                            repd.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
                    }
                    if (DungChung.Bien.MaBV == "30007")//in theo id khám bệnh của kp in đơn
                    {
                        var bnkb1 = _data.BNKBs.Where(p => p.IDKB == _idkb).FirstOrDefault();
                        if (bnkb1 != null)
                        {
                            string macb = bnkb1.MaCB;
                            var ttcb = _data.CanBoes.Where(p => p.MaCB == macb).FirstOrDefault();
                            if (ttcb != null)
                            {
                                repd.TenCB.Value = ttcb.CapBac + ": " + ttcb.TenCB;
                            }
                            int makp1 = bnkb1.MaKP ?? 0;
                            var ttkp = _data.KPhongs.Where(p => p.MaKP == makp1).FirstOrDefault();
                            if (ttkp != null)
                            {
                                repd.TenKP.Value = ttkp.TenKP;
                            }
                        }
                    }
                    else
                    {
                        repd.TenCB.Value = ktkd.First().CapBac + ": " + ktkd.First().TenCB;
                        repd.TenKP.Value = ktkd.Count > 0 ? ktkd.First().TenKP : "";
                    }
                    repd.DataSource = don2.ToList();
                    repd.ThuKho.Value = DungChung.Bien.ThuKho;
                    repd.BindData();
                    repd.CreateDocument();
                    frm.prcIN.PrintingSystem = repd.PrintingSystem;
                    frm.ShowDialog();
                }
                if (don1.Count > 0)
                    for (int i = 1; i < 4; i++)
                    {

                        BaoCao.repDonThuocTV repd = new BaoCao.repDonThuocTV();
                        repd._idDon.Value = ktkd.First().IDDontv;
                        if (ttd.Count > 0)
                        {
                            string dotdt = (ktkd.First().DotDT != "" && ktkd.First().DotDT != null ? ktkd.First().DotDT : "......") + " từ ngày ";
                            if (ktkd.First().NgayTu != null)
                                dotdt = dotdt + ktkd.First().NgayTu.Value.Day + "/" + ktkd.First().NgayTu.Value.Month + "/" + ktkd.First().NgayTu.Value.Year + " đến ngày ";
                            else
                                dotdt = dotdt + ".../..../......." + " đến ngày ";
                            if (ktkd.First().NgayDen != null)
                                dotdt = dotdt + ktkd.First().NgayDen.Value.Day + "/" + ktkd.First().NgayDen.Value.Month + "/" + ktkd.First().NgayDen.Value.Year;
                            else
                                dotdt = dotdt + ".../..../.......";
                            repd.paraDotDieuTri.Value = dotdt;
                            repd._TenBNhan.Value = ttd.First().TenBNhan;
                            repd.Tuoi.Value = DungChung.Ham.TuoitheoThang(DaTaContext, _mabn, DungChung.Bien.formatAge);
                            // KT
                            switch (ttd.First().GTinh)
                            {
                                case 1:
                                    repd.GTinh.Value = "Nam";
                                    repd.Nu.Value = "/";
                                    break;
                                case 0:
                                    repd.GTinh.Value = "Nữ";
                                    repd.Nam.Value = "/";
                                    break;
                            }
                            repd.ICD.Value = DungChung.Ham.getMaICDarr(DaTaContext, _mabn, DungChung.Bien.GetICD, 0)[0];
                            repd.SThe.Value = ttd.First().SThe;
                            repd.ChanDoan.Value = DungChung.Ham.getMaICDarr(DaTaContext, _mabn, DungChung.Bien.GetICD, 0)[1];
                            repd.TenCB.Value = ktkd.First().CapBac + ": " + ktkd.First().TenCB;
                            repd.TenKP.Value = ktkd.First().TenKP;
                            repd.DiaChi.Value = ttd.First().DChi;
                            repd._MaBNhan.Value = _mabn.ToString();
                            if (ktkd.First().NgayKe.Value.Day > 0)
                                repd.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
                        }
                        repd.DataSource = qd1.Where(p => p.TenRG == "Thuốc gây nghiện").ToList();
                        repd.ThuKho.Value = DungChung.Bien.ThuKho;
                        if (i == 1)
                        {
                            repd.lblLien.Text = "(Bản lưu tại cơ sở khám bệnh, chữa bệnh)";
                            repd.lblNguoiNhanThuoc.Text = "";
                            repd.txtbnky.Text = "";
                            repd.txttenBN.Text = "";
                            repd.txtCMTND.Text = "";
                            repd.xrTableCell7.Text = "";
                            repd.xrTableCell10.Text = "";
                        }
                        else if (i == 2)
                        {
                            repd.lblLien.Text = "(Bản lưu tại cơ sở cấp, bán thuốc)";
                            repd.lblNguoiNhanThuoc.Text = "NGƯỜI NHẬN THUỐC";
                            repd.txtbnky.Text = "(Ký, ghi rõ họ tên và số chứng minh nhân dân/ căn cước công dân)";
                            repd.xrTableCell7.Text = "Lô sản xuất:........";
                            repd.xrTableCell10.Text = "Hạn dùng:..........";
                            repd.txttenBN.Text = "";
                            //if (ttd.First().SoKSinh != null && ttd.First().SoKSinh != "")
                            //    repd.txtCMTND.Text = "  CMTND/CCCD: " + ttd.First().SoKSinh;
                            //else
                            repd.txtCMTND.Text = "";
                        }
                        else if (i == 3)
                        {
                            repd.lblLien.Text = "(Bản giao cho người bệnh)";
                            repd.lblNguoiNhanThuoc.Text = "";
                            repd.txtbnky.Text = "";
                            repd.txttenBN.Text = "";
                            repd.xrTableCell7.Text = "";
                            repd.xrTableCell10.Text = "";
                            repd.txtCMTND.Text = "";
                        }
                        repd.BindData();
                        repd.CreateDocument();
                        frm.prcIN.PrintingSystem = repd.PrintingSystem;
                        frm.ShowDialog();
                    }
            }
        }

        private void grvDontvct_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator) //Nếu là dòng Indicator
            {
                if (e.RowHandle < 0)
                {
                    e.Info.ImageIndex = 0;
                    e.Info.DisplayText = string.Empty;
                }
                else
                {
                    e.Info.ImageIndex = -1; //Không hiển thị hình
                    e.Info.DisplayText = (e.RowHandle + 1).ToString(); //Số thứ tự tăng dần
                }
            }
            SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font); //Lấy kích thước của vùng hiển thị Text
            Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
            BeginInvoke(new MethodInvoker(delegate { cal(_Width, grvDontvct); })); //Tăng kích thước nếu Text vượt quá
        }

        bool cal(int _Width, DevExpress.XtraGrid.Views.Grid.GridView grvDontvct)
        {
            grvDontvct.IndicatorWidth = grvDontvct.IndicatorWidth < _Width ? _Width : grvDontvct.IndicatorWidth;
            return true;
        }

        private void btn_SaoDon_Click(object sender, EventArgs e)
        {
            grvDontvct.OptionsBehavior.ReadOnly = false;
            lup_BSKe.Enabled = true;
            lup_KhoaKe.EditValue = makp1;
            lup_NgayKe.Enabled = true;
            GhiChu.Enabled = true;
            txtDotDT.Enabled = true;
            txtDotDT.Text = "";
            NgayTu.Enabled = true;
            NgayTu.DateTime = DateTime.Now;
            NgayDen.Enabled = true;
            NgayDen.DateTime = DateTime.Now;
            lup_NgayKe.DateTime = DateTime.Now;
            lup_KhoaKe.Enabled = true;
            Them.Enabled = false;
            Luu.Enabled = true;
            KLuu.Enabled = true;
            Sua.Enabled = false;
            Xoa.Enabled = false;
            GhiChu.Text = "";
            kieu = 2;
            grcDonTV.Enabled = false;
            btn_SaoDon.Enabled = false;
        }
        private class Don
        {
            public string TenRG { get; set; }

            public string TenDV { get; set; }

            public int MaDV { get; set; }

            public string DonVi { get; set; }

            public double? SoLuong { get; set; }

            public string HuongDan { get; set; }
        }
    }


}