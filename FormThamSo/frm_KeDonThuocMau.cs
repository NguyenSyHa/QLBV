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
using QLBV.FormNhap;

namespace QLBV.FormThamSo
{
    public partial class frm_KeDonThuocMau : DevExpress.XtraEditors.XtraForm
    {
        public frm_KeDonThuocMau()
        {
            InitializeComponent();
        }
        int kieu = 0;
        int makho1 = 0;
        int loai = 0;
        public frm_KeDonThuocMau(int kieu1)
        {
            InitializeComponent();
            loai = kieu1;
        }
        public frm_KeDonThuocMau(int kieu2, int makho)
        {
            InitializeComponent();
            loai = kieu2;
            makho1 = makho;
        }
        string[] _sHDSDThuoc = new string[1000];
        
        public int iddonlay = 0;
        void _setHDSDThuoc()
        {
            string HD = "6 giờ 10UI;6 giờ 7UI, 18 giờ 17UI;Chia 2 lần: 11 giờ, 18 giờ;Chia 2 lần: 6 giờ, 18 giờ;Chiều;Nhai trước ăn;Nhai trước ăn sáng;Sau ăn;Sau ăn 1 giờ" +
   ";Sau ăn no;Sau ăn sáng;Sau ăn sáng, trưa, Tối;Sau ăn tối;Sáng;Sáng 1/2 V, chiều 1/2 V;Sáng 1V, Chiều 1V;Sáng 1V, Chiều 1V, sau ăn;Sáng 1V, Chiều 1V, trước ăn;Sáng 1V, Chiều 2V" +
   ";Sáng 2V, Chiều 1V;Sáng 2V, Chiều 2V;Sáng 2V, Chiều 2V, sau ăn;Sáng 1V, Chiều 1V, trước ăn;Sáng 3V, Chiều 3V;Sáng 3V, Chiều 3V, sau ăn;Sáng 3V, Chiều 3V, sau ăn;Thủy châm;Trưa 1V, Chiều 2V, Sau ăn;Trưa 1V, Chiều 2V, Trước ăn;" +
   "Trưa 1V, Tối 1V, trước ăn;-	Trưa 1V, Tối 1V, sau ăn;Trưa 2V, Chiều 1V, trước ăn;Trước ăn sáng;Trước ăn sáng 1 giờ;Tối;Uống sau 1 giờ;Uống sau 2 giờ;Uống sau ăn;Uống trong bữa ăn;-	Uống trước bữa ăn;Uống trước bữa ăn 1 giờ";
            _sHDSDThuoc = HD.Split(';');
        }
        private class _ds
        {
            int iDDonMau;
            int? maKP;

            public int? MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
            string tenCB, tenKP, maCB, tenDTM;

            public string TenDTM
            {
                get { return tenDTM; }
                set { tenDTM = value; }
            }

            public string MaCB
            {
                get { return maCB; }
                set { maCB = value; }
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

            public int IDDonMau
            {
                get { return iDDonMau; }
                set { iDDonMau = value; }
            }
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool kt()
        {
            if (txtTenDTM.EditValue == null)
            {
                MessageBox.Show("Bạn chưa nhập tên đơn!", "Thông báo!");
                txtTenDTM.Focus();
                return false;
            }
            if (lup_BSKe.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn bác sĩ kê đơn!", "Thông báo!");
                lup_BSKe.Focus();
                return false;
            }
            if (lup_KhoaKe.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn kho kê đơn!", "Thông báo!");
                lup_BSKe.Focus();
                return false;
            }
            return true;
        }
        List<_ds> ds1 = new List<_ds>();
        private void frm_KeDonMau_Load(object sender, EventArgs e)
        {
            var kp = _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).Select(p => new { p.MaKP, p.TenKP }).ToList();
            var cb = _data.CanBoes.Select(p => new { p.MaCB, p.TenCB }).ToList();
            var dv = _data.DichVus.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).Select(p => new { p.MaDV, p.TenDV, p.DonVi, p.DonGia }).ToList();
            lup_Thuoc.DataSource = dv;
            lup_KhoaKe.Properties.DataSource = kp;
            lup_BSKe.Properties.DataSource = cb;
            var dt = (from a in _data.DThuocMaus select a).ToList();
            if (loai == 1 || loai == 2)
                dt = (from a in _data.DThuocMaus.Where(p => p.MaKP == makho1) select a).ToList();
            ds1.Clear();
            foreach (var item in dt)
            {
                _ds ds = new _ds();
                ds.IDDonMau = item.IDDonMau;
                ds.TenCB = _data.CanBoes.Where(p => p.MaCB == item.MaCB).Count() > 0 ? _data.CanBoes.Where(p => p.MaCB == item.MaCB).First().TenCB : "";
                ds.MaCB = item.MaCB;
                ds.MaKP = item.MaKP;
                ds.TenDTM = item.TenDTM;
                int x1 = Convert.ToInt32(item.MaKP);
                ds.TenKP = _data.KPhongs.Where(p => p.MaKP == x1).Count() > 0 ? _data.KPhongs.Where(p => p.MaKP == x1).First().TenKP : " ";
                ds1.Add(ds);
            }
            grcDonMau.Enabled = true;
            
            grvDonMauct.OptionsBehavior.ReadOnly = true;
            this.IDDonMau.OptionsColumn.ReadOnly = true;
            Luu.Enabled = false;
            KLuu.Enabled = false;
            Them.Enabled = true;
            if (dt.Count > 0)
            {
                Sua.Enabled = true;
                Xoa.Enabled = true;
            }

            else
            {
                Sua.Enabled = false;
                Xoa.Enabled = false;
            }
            _setHDSDThuoc();
            if (_sHDSDThuoc != null)
                cboGhiChu.Items.AddRange(_sHDSDThuoc);
            grvDonTV_CellValueChanged(null, null);
            lup_KhoaKe.EditValue = "";
            lup_KhoaKe_EditValueChanged(null, null);
            lup_BSKe.Enabled = false;
            txtTenDTM.Enabled = false;
            lup_KhoaKe.Enabled = false;
            txtTimKiemTenDon.Enabled = true;
            if (loai == 0)
                btn_LayDon.Visible = false;
            else
            {
                btn_LayDon.Visible = true;
                lup_KhoaKe.EditValue = makho1;
            }
            ds1 =  ds1.OrderByDescending(p => p.IDDonMau).ToList();
            bindingSource1.DataSource = ds1.ToList();
            grcDonMau.DataSource = bindingSource1;
            int x12 = ds1.Count > 0 ? ds1.FirstOrDefault().IDDonMau : 0;
            var dtct = _data.DThuocMaucts.Where(p => p.IDDonMau == x12).ToList();
            bindingSource2.DataSource = dtct;
            grcDonMauct.DataSource = bindingSource2;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            usDieuTri.laydon = 0;
            this.Close();
        }
        string aa = "";
        int iddd = 0;
        private void grvDonTV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int idd = 0;
            if (grvDonMau.GetFocusedRowCellValue(IDDonMau) != null && grvDonMau.GetFocusedRowCellValue(IDDonMau).ToString() != "")
                idd = Convert.ToInt32(grvDonMau.GetFocusedRowCellValue(IDDonMau).ToString());
            var dtct = _data.DThuocMaucts.Where(p =>p.IDDonMau == idd).ToList();
            bindingSource2.DataSource = dtct;
            grcDonMauct.DataSource = bindingSource2;
            aa = grvDonMau.GetFocusedRowCellValue(x1) != null ? grvDonMau.GetFocusedRowCellValue(x1).ToString() : "";
            lup_BSKe.EditValue = aa;
            txtTenDTM.Text =(grvDonMau.GetFocusedRowCellValue(TenDTM) != null ? grvDonMau.GetFocusedRowCellValue(TenDTM).ToString() : "");
            lup_KhoaKe.EditValue = grvDonMau.GetFocusedRowCellValue(x2) != null ? Convert.ToInt32( grvDonMau.GetFocusedRowCellValue(x2).ToString()) : 0;
            iddd = grvDonMau.GetFocusedRowCellValue(IDDonMau) != null && grvDonMau.GetFocusedRowCellValue(IDDonMau).ToString() != "0" ? Convert.ToInt32(grvDonMau.GetFocusedRowCellValue(IDDonMau).ToString()) : 0;
            if (grvDonMau.GetFocusedRowCellValue(IDDonMau) != null)
            {
                iddonlay = Convert.ToInt32(grvDonMau.GetFocusedRowCellValue(IDDonMau).ToString());
            }
        }

        private void btn_XoaCT_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa thuốc không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (grvDonMauct.GetFocusedRowCellValue(celIDDonMauct) != null && grvDonMauct.GetFocusedRowCellValue(celIDDonMauct).ToString() != "0" && grvDonMauct.GetFocusedRowCellValue(celIDDonMauct).ToString() != "")
                {
                    int idd = Convert.ToInt32(grvDonMauct.GetFocusedRowCellValue(celIDDonMauct).ToString());
                    DThuocMauct xoa2 = _data.DThuocMaucts.Single(p => p.IDDonMauct == idd);
                    _data.DThuocMaucts.Remove(xoa2);
                    if (_data.SaveChanges() >= 0)
                    {
                        MessageBox.Show("Xóa thuốc thành công!");
                        var dtct = _data.DThuocMaucts.Where(p => p.IDDonMau == iddd).ToList();
                        bindingSource2.DataSource = dtct;
                        grcDonMauct.DataSource = bindingSource2;
                    }
                }
                else
                {
                    grvDonMauct.DeleteRow(grvDonMauct.FocusedRowHandle);
                    MessageBox.Show("Xóa thuốc thành công!");
                }
            }
        }
        
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (kt())
            {
                int dem = 0, dem1 = 0;
                int y = 0;
                for (int i = 0; i < grvDonMauct.RowCount; i++)
                {
                    if (grvDonMauct.GetRowCellValue(i, SoLuong) != null)
                    {
                        if (grvDonMauct.GetRowCellValue(i, celMaDV) != null)
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
                        int x1 = Convert.ToInt32(grvDonMauct.GetRowCellValue(i, celMaDV));
                        if (x1 != 0)
                        {
                            string thuoc = _data.DichVus.Where(p => p.MaDV == x1).FirstOrDefault().TenDV;
                            MessageBox.Show("Thuốc: " + thuoc + " chưa điền số lượng!", "Thông báo!");
                            dem1++;
                        }
                    }
                }
                if (dem1 == 0)
                if (kieu == 0) //thêm mới
                {

                    DThuocMau moi1 = new DThuocMau();
                    moi1.TenDTM = txtTenDTM.Text;
                    moi1.MaCB = lup_BSKe.EditValue.ToString();
                    moi1.MaKP = Convert.ToInt32(lup_KhoaKe.EditValue.ToString());
                    _data.DThuocMaus.Add(moi1);
                    _data.SaveChanges();
                    
                    for (int i = 0; i < grvDonMauct.RowCount; i++)
                    {
                        int IDDonMau = moi1.IDDonMau;
                        y = moi1.IDDonMau;
                        int IDDonMauct = Convert.ToInt32(grvDonMauct.GetRowCellValue(i, celIDDonMauct));
                        var dtct = _data.DThuocMaucts.Where(p => p.IDDonMauct == IDDonMauct).FirstOrDefault();
                        if (dtct == null)
                        {
                            DThuocMauct moi = new DThuocMauct();
                            moi.IDDonMau = moi1.IDDonMau;
                            if (grvDonMauct.GetRowCellValue(i, celMaDV) != null)
                                moi.MaDV = Convert.ToInt32(grvDonMauct.GetRowCellValue(i, celMaDV));

                            if (grvDonMauct.GetRowCellValue(i, DonVi) != null)
                                moi.DonVi = grvDonMauct.GetRowCellValue(i, DonVi).ToString();

                            if (grvDonMauct.GetRowCellValue(i, SoLan) != null)
                                moi.SoLan = grvDonMauct.GetRowCellValue(i, SoLan).ToString();

                            if (grvDonMauct.GetRowCellValue(i, DuongD) != null)
                                moi.DuongD = grvDonMauct.GetRowCellValue(i, DuongD).ToString();

                            if (grvDonMauct.GetRowCellValue(i, MoiLan) != null)
                                moi.MoiLan = grvDonMauct.GetRowCellValue(i, MoiLan).ToString();

                            if (grvDonMauct.GetRowCellValue(i, DviUong) != null)
                                moi.DviUong = grvDonMauct.GetRowCellValue(i, DviUong).ToString();

                            if (grvDonMauct.GetRowCellValue(i, Luong) != null)
                                moi.Luong = grvDonMauct.GetRowCellValue(i, Luong).ToString();

                            if (grvDonMauct.GetRowCellValue(i, GhiChu1) != null)
                                moi.GhiChu = grvDonMauct.GetRowCellValue(i, GhiChu1).ToString();
                            if (grvDonMauct.GetRowCellValue(i, SoLuong) != null)
                                moi.SoLuong = DungChung.Bien.MaBV == "14017" ? Convert.ToDouble(grvDonMauct.GetRowCellValue(i, SoLuong).ToString()) : Convert.ToInt32(grvDonMauct.GetRowCellValue(i, SoLuong).ToString());
                            if (grvDonMauct.GetRowCellValue(i, celMaDV) != null)
                            {
                                _data.DThuocMaucts.Add(moi);
                                dem++;
                                _data.SaveChanges();
                            }
                        }
                        else
                        {
                            DThuocMauct sua = _data.DThuocMaucts.Single(p => p.IDDonMauct == IDDonMauct);
                            if (grvDonMauct.GetRowCellValue(i, celMaDV) != null)
                                sua.MaDV = Convert.ToInt32(grvDonMauct.GetRowCellValue(i, celMaDV));

                            if (grvDonMauct.GetRowCellValue(i, DonVi) != null)
                                sua.DonVi = grvDonMauct.GetRowCellValue(i, DonVi).ToString();

                            if (grvDonMauct.GetRowCellValue(i, SoLuong) != null)
                                sua.SoLuong = DungChung.Bien.MaBV == "14017" ? Convert.ToDouble(grvDonMauct.GetRowCellValue(i, SoLuong).ToString()) : Convert.ToInt32(grvDonMauct.GetRowCellValue(i, SoLuong).ToString());

                            if (grvDonMauct.GetRowCellValue(i, SoLan) != null)
                                sua.SoLan = grvDonMauct.GetRowCellValue(i, SoLan).ToString();

                            if (grvDonMauct.GetRowCellValue(i, DuongD) != null)
                                sua.SoLan = grvDonMauct.GetRowCellValue(i, DuongD).ToString();

                            if (grvDonMauct.GetRowCellValue(i, MoiLan) != null)
                                sua.MoiLan = grvDonMauct.GetRowCellValue(i, MoiLan).ToString();

                            if (grvDonMauct.GetRowCellValue(i, DviUong) != null)
                                sua.DviUong = grvDonMauct.GetRowCellValue(i, DviUong).ToString();

                            if (grvDonMauct.GetRowCellValue(i, Luong) != null)
                                sua.Luong = grvDonMauct.GetRowCellValue(i, Luong).ToString();

                            if (grvDonMauct.GetRowCellValue(i, GhiChu1) != null)
                                sua.GhiChu = grvDonMauct.GetRowCellValue(i, GhiChu1).ToString();
                            dem++;
                        }
                    }
                }
                else //sửa
                {
                    int idd = 0;
                    if (grvDonMau.GetFocusedRowCellValue(IDDonMau) != null && grvDonMau.GetFocusedRowCellValue(IDDonMau).ToString() != "")
                        idd = Convert.ToInt32(grvDonMau.GetFocusedRowCellValue(IDDonMau).ToString());
                    y = Convert.ToInt32(grvDonMau.GetFocusedRowCellValue(IDDonMau).ToString());
                    DThuocMau sua = _data.DThuocMaus.Single(p => p.IDDonMau == idd);
                    sua.MaCB = lup_BSKe.EditValue.ToString();
                    sua.MaKP = Convert.ToInt32(lup_KhoaKe.EditValue.ToString());
                    sua.TenDTM = txtTenDTM.Text;
                    _data.SaveChanges();
                    for (int i = 0; i < grvDonMauct.RowCount; i++)
                    {
                        _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                        int IDDonMauct = Convert.ToInt32(grvDonMauct.GetRowCellValue(i, celIDDonMauct));
                        var dtct = _data.DThuocMaucts.Where(p => p.IDDonMauct == IDDonMauct).FirstOrDefault();
                        if (dtct == null)
                        {
                            DThuocMauct moi2 = new DThuocMauct();
                            moi2.IDDonMau = idd;
                            if (grvDonMauct.GetRowCellValue(i, celMaDV) != null)
                                moi2.MaDV = Convert.ToInt32(grvDonMauct.GetRowCellValue(i, celMaDV));

                            if (grvDonMauct.GetRowCellValue(i, SoLuong) != null)
                                moi2.SoLuong = DungChung.Bien.MaBV == "14017" ? Convert.ToDouble(grvDonMauct.GetRowCellValue(i, SoLuong).ToString()): Convert.ToInt32(grvDonMauct.GetRowCellValue(i, SoLuong).ToString());

                            if (grvDonMauct.GetRowCellValue(i, DonVi) != null)
                                moi2.DonVi = grvDonMauct.GetRowCellValue(i, DonVi).ToString();

                            if (grvDonMauct.GetRowCellValue(i, SoLan) != null)
                                moi2.SoLan = grvDonMauct.GetRowCellValue(i, SoLan).ToString();

                            if (grvDonMauct.GetRowCellValue(i, DuongD) != null)
                                moi2.DuongD = grvDonMauct.GetRowCellValue(i, DuongD).ToString();

                            if (grvDonMauct.GetRowCellValue(i, MoiLan) != null)
                                moi2.MoiLan = grvDonMauct.GetRowCellValue(i, MoiLan).ToString();

                            if (grvDonMauct.GetRowCellValue(i, DviUong) != null)
                                moi2.DviUong = grvDonMauct.GetRowCellValue(i, DviUong).ToString();

                            if (grvDonMauct.GetRowCellValue(i, Luong) != null)
                                moi2.Luong = grvDonMauct.GetRowCellValue(i, Luong).ToString();

                            if (grvDonMauct.GetRowCellValue(i, GhiChu1) != null)
                                moi2.GhiChu = grvDonMauct.GetRowCellValue(i, GhiChu1).ToString();
                            if (grvDonMauct.GetRowCellValue(i, celMaDV) != null)
                            {
                                _data.DThuocMaucts.Add(moi2);
                                dem++;
                                _data.SaveChanges();
                            }
                        }
                        else
                        {
                            DThuocMauct sua1 = _data.DThuocMaucts.Single(p => p.IDDonMauct == IDDonMauct);
                            if (grvDonMauct.GetRowCellValue(i, celMaDV) != null)
                                sua1.MaDV = Convert.ToInt32(grvDonMauct.GetRowCellValue(i, celMaDV));

                            if (grvDonMauct.GetRowCellValue(i, DonVi) != null)
                                sua1.DonVi = grvDonMauct.GetRowCellValue(i, DonVi).ToString();

                            if (grvDonMauct.GetRowCellValue(i, SoLuong) != null)
                                sua1.SoLuong = DungChung.Bien.MaBV =="14017" ? Convert.ToDouble(grvDonMauct.GetRowCellValue(i, SoLuong).ToString()) : Convert.ToInt32(grvDonMauct.GetRowCellValue(i, SoLuong).ToString());

                            if (grvDonMauct.GetRowCellValue(i, SoLan) != null)
                                sua1.SoLan = grvDonMauct.GetRowCellValue(i, SoLan).ToString();

                            if (grvDonMauct.GetRowCellValue(i, DuongD) != null)
                                sua1.SoLan = grvDonMauct.GetRowCellValue(i, DuongD).ToString();

                            if (grvDonMauct.GetRowCellValue(i, MoiLan) != null)
                                sua1.MoiLan = grvDonMauct.GetRowCellValue(i, MoiLan).ToString();

                            if (grvDonMauct.GetRowCellValue(i, DviUong) != null)
                                sua1.DviUong = grvDonMauct.GetRowCellValue(i, DviUong).ToString();

                            if (grvDonMauct.GetRowCellValue(i, Luong) != null)
                                sua1.Luong = grvDonMauct.GetRowCellValue(i, Luong).ToString();

                            if (grvDonMauct.GetRowCellValue(i, GhiChu1) != null)
                                sua1.GhiChu = grvDonMauct.GetRowCellValue(i, GhiChu1).ToString();
                            dem++;
                        }
                    }

                }
                if (dem > 0)
                {
                    MessageBox.Show("Lưu thành công");
                    frm_KeDonMau_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Đơn chưa có thuốc!");
                    if (_data.DThuocMaus.Where(p => p.IDDonMau == y).Count() > 0)
                    {
                        DThuocMau xoa1 = _data.DThuocMaus.Single(p => p.IDDonMau == y);
                        _data.DThuocMaus.Remove(xoa1);
                        _data.SaveChanges();
                    }
                    if (kieu != 0)
                    {
                        frm_KeDonMau_Load(sender, e);
                    }
                }
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            frm_KeDonMau_Load(sender, e);
        }

        private void simpleButton3_Click(object sender, EventArgs e) // thêm mới
        {
            grvDonMauct.OptionsBehavior.ReadOnly = false;
            lup_BSKe.Enabled = true;
            txtTenDTM.Enabled = true;
            lup_KhoaKe.Enabled = true;
            Them.Enabled = false;
            Luu.Enabled = true;
            KLuu.Enabled = true;
            Sua.Enabled = false;
            Xoa.Enabled = false;
            txtTenDTM.Text = "";
            kieu = 0;
            var dtct = _data.DThuocMaucts.Where(p => p.IDDonMau == 0).ToList();
            bindingSource2.DataSource = dtct;
            grcDonMauct.DataSource = bindingSource2;
            grcDonMau.Enabled = false;
            txtTimKiemTenDon.Enabled = false;
        }

        private void simpleButton5_Click(object sender, EventArgs e) // sửa
        {
            grvDonMauct.OptionsBehavior.ReadOnly = false;
            lup_BSKe.Enabled = true;
            txtTenDTM.Enabled = true;
            lup_KhoaKe.Enabled = true;
            Sua.Enabled = false;
            Luu.Enabled = true;
            KLuu.Enabled = true;
            Xoa.Enabled = false;
            Them.Enabled = false;
            lup_KhoaKe_EditValueChanged(null,null);
            kieu = 1;
            grcDonMau.Enabled = false;
            txtTimKiemTenDon.Enabled = false;
        }

        private void lup_Thuoc_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void grvDontvct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch(e.Column.Name)
            {
                case "celMaDV":
                    int madv = 0;
                    if (grvDonMauct.GetFocusedRowCellValue(celMaDV) != null)
                    {
                        madv = Convert.ToInt32(grvDonMauct.GetFocusedRowCellValue(celMaDV));
                        int thuoc1 = 0;
                        if (grvDonMauct.RowCount > 0)
                        for (int i = 0; i < grvDonMauct.RowCount; i++)
                        {
                            if (grvDonMauct.GetRowCellValue(i, celMaDV) != null)
                            {
                                int madv1 = Convert.ToInt32(grvDonMauct.GetRowCellValue(i, celMaDV));
                                if (madv == madv1 && i != grvDonMauct.FocusedRowHandle)
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
                                grvDonMauct.DeleteRow(grvDonMauct.FocusedRowHandle);
                        }

                        grvDonMauct.SetFocusedRowCellValue(DonVi, Ham._getDonVi(_data, madv));
                        grvDonMauct.SetFocusedRowCellValue(DuongD, _getDDung(madv));
                        grvDonMauct.SetFocusedRowCellValue(MoiLan, " lần, mỗi lần ");
                        grvDonMauct.SetFocusedRowCellValue(SoLan, " 1 ");
                        grvDonMauct.SetFocusedRowCellValue(Luong, " 1 ");
                        grvDonMauct.SetFocusedRowCellValue(DviUong, " " + Ham._getDonVi(_data, madv));

                    }
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
            string macb = "";
            if (grvDonMau.GetFocusedRowCellValue(x1) != null)
            {
                macb = grvDonMau.GetFocusedRowCellValue(x1).ToString();
            }
            lup_BSKe.EditValue= macb;
            string makp = "";
            if (grvDonMau.GetFocusedRowCellValue(x2) != null)
            {
                makp = grvDonMau.GetFocusedRowCellValue(x2).ToString();
            }
            
            lup_KhoaKe.EditValue = makp;
            
        }

        private void simpleButton6_Click(object sender, EventArgs e) //xóa
        {
            if (MessageBox.Show("Bạn có muốn xóa đơn mẫu không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int idd = Convert.ToInt32(grvDonMau.GetFocusedRowCellValue(IDDonMau).ToString());
                var xoa2 = _data.DThuocMaucts.Where(p => p.IDDonMau == idd).ToList();
                foreach (var item in xoa2)
                {
                    DThuocMauct xoact = _data.DThuocMaucts.Single(p => p.IDDonMauct == item.IDDonMauct);
                    _data.DThuocMaucts.Remove(xoact);
                }
                DThuocMau xoa1 = _data.DThuocMaus.Single(p => p.IDDonMau == idd);
                _data.DThuocMaus.Remove(xoa1);
                if (_data.SaveChanges() >= 0)
                {
                    MessageBox.Show("Xóa đơn mẫu thành công!");
                }
                frm_KeDonMau_Load(sender, e);
            }
        }

        private void lup_KhoaKe_EditValueChanged(object sender, EventArgs e)
        {
            string x = "";
            if(lup_KhoaKe.EditValue != null && lup_KhoaKe.EditValue.ToString() != "")
            x = lup_KhoaKe.EditValue.ToString();
            if (DungChung.Bien.MaBV == "14017")
            {
                int number;
                bool num = Int32.TryParse(x, out number);
                if (num)
                { 
                    var tendv = (from dv in _data.DichVus
                                 join n in _data.NhapDcts on dv.MaDV equals n.MaDV
                                 join nhap in _data.NhapDs on n.IDNhap equals nhap.IDNhap
                                 where (nhap.MaKP == number)
                                 where (dv.MaKPsd.Contains(x))
                                 select new { dv.MaDV, dv.TenDV, dv.DonVi, dv.DonGia }
                             ).ToList();
                    lup_Thuoc.DataSource = tendv.Distinct();
                }
            }
            else
            {
                var dv = _data.DichVus.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 10 || p.IDNhom == 11).Where(p => p.MaKPsd.Contains(x) || x == "").Select(p => new { p.MaDV, p.TenDV, p.DonVi, p.DonGia }).ToList();
                lup_Thuoc.DataSource = dv;
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
            BeginInvoke(new MethodInvoker(delegate { cal(_Width, grvDonMauct); })); //Tăng kích thước nếu Text vượt quá
        }

        bool cal(int _Width, DevExpress.XtraGrid.Views.Grid.GridView grvDontvct)
        {
            grvDontvct.IndicatorWidth = grvDontvct.IndicatorWidth < _Width ? _Width : grvDontvct.IndicatorWidth;
            return true;
        }

        private void txtTimKiemTenDon_EditValueChanged(object sender, EventArgs e)
        {
            timkiem();
        }
        void timkiem()
        {
            try
            {
                string _timkiem = "";
                if (!string.IsNullOrEmpty(txtTimKiemTenDon.Text) && txtTimKiemTenDon.Text != "Tìm tên đơn mẫu")
                    _timkiem = txtTimKiemTenDon.Text;
                _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var dt = (from a in _data.DThuocMaus.Where(p => p.TenDTM.ToLower().Contains(_timkiem.ToLower()) || _timkiem == "Tìm tên đơn mẫu" || _timkiem == "") select a).ToList();
                ds1.Clear();
                foreach (var item in dt)
                {
                    _ds ds = new _ds();
                    ds.IDDonMau = item.IDDonMau;
                    ds.TenCB = _data.CanBoes.Where(p => p.MaCB == item.MaCB).FirstOrDefault().TenCB;
                    ds.MaCB = item.MaCB;
                    ds.MaKP = item.MaKP;
                    ds.TenDTM = item.TenDTM;
                    int x1 = Convert.ToInt32(item.MaKP);
                    ds.TenKP = _data.KPhongs.Where(p => p.MaKP == x1).FirstOrDefault().TenKP;
                    ds1.Add(ds);
                }
                bindingSource1.DataSource = ds1.ToList();
                grcDonMau.DataSource = bindingSource1;
                grvDonTV_CellValueChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void btn_LayDon_Click(object sender, EventArgs e)
        {
            if (loai == 1)
            {
                usDieuTri.iddthuocmau = iddonlay;
                usDieuTri.laydon = 1;
            }
            else
            {
                usKhamBenh.iddthuocmau1 = iddonlay;
                usKhamBenh.laydon1 = 1;
            }
            this.Close();
        }

        private void grcDonMauct_Click(object sender, EventArgs e)
        {

        }
    }
}