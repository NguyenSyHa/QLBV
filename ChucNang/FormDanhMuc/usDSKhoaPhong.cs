using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Text.RegularExpressions;

namespace QLBV.FormDanhMuc
{
    public partial class usDSKhoaPhong : DevExpress.XtraEditors.XtraUserControl
    {
        public usDSKhoaPhong()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        int TTLuu = 0;
        int Makp = 0;
        private void enableControl(bool T)
        {
            txtDChi.Properties.ReadOnly = !T;
            txtMaKP.Properties.ReadOnly = !T;
            txtTenKP.Properties.ReadOnly = !T;
            lupnguoiphat.Properties.ReadOnly = !T;
            txtGHCD.Properties.ReadOnly = !T;
            txtmakp9324.Properties.ReadOnly = !T;
            txtSTT.Properties.ReadOnly = !T;
            cboStatus.Properties.ReadOnly = !T;
            cboPLoai.Properties.ReadOnly = !T;
            lupNhomBP.Properties.ReadOnly = !T;
            cboTrongBV.Properties.ReadOnly = !T;
            cboChuyenKhoa.Properties.ReadOnly = !T;
            txtPPxuatduoc.Properties.ReadOnly = !T;
            //memoBuongGiuong.Properties.ReadOnly = !T;
            chk_TuTruc.Properties.ReadOnly = !T;
            ckcDY.Properties.ReadOnly = !T;
            chk_MuaNgoai.Properties.ReadOnly = !T; 
            cbo_pphhdy.Properties.ReadOnly = !T;
            txtMaQD.Properties.ReadOnly = !T;
            cboQLNT.Properties.ReadOnly = !T;
            lupMaBVSD.Properties.ReadOnly = !T;
            btnLuuKb.Enabled = T;
            btnMoiKb.Enabled = !T;
            btnSuaKb.Enabled = !T;
            btnXoaKb.Enabled = !T;
            btnBuongKH.Enabled = !T;
            grcKhoaPhong.Enabled = !T;
            cklKhoaPhong.Enabled = T;
        }
        private void resetControl()
        {
            txtDChi.Text = "";
            txtMaKP.Text = "";
            txtTenKP.Text = "";
            cboStatus.Text = "";
            cboPLoai.Text = "";
            txtSTT.Text = "";
            cklKhoaPhong.UnCheckAll();
            txtMaQD.ResetText();
            ckcDY.Checked = false;
        }
        private void btnMoiKb_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            TTLuu = 1;
        }
        #region
        private bool KTLuu()
        {
            //if (string.IsNullOrEmpty(txtMaKP.Text))
            //{
            //    MessageBox.Show("Bạn chưa nhập mã bộ phận");
            //    txtMaKP.Focus();
            //    return false;
            //}
            //int ot;

            //if (!Int32.TryParse(txtMaKP.Text, out ot))
            //{
            //    MessageBox.Show("mã bộ phận không hợp lệ!");
            //    txtMaKP.Focus();
            //    return false;
            //}
            if (string.IsNullOrEmpty(cboTrongBV.Text))
            {
                MessageBox.Show("Bạn chưa chọn trong hay ngoài BV");
                cboTrongBV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenKP.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên bộ phận");
                txtTenKP.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cboPLoai.Text))
            {
                MessageBox.Show("Bạn chưa chọn phân loại");
                cboPLoai.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboStatus.Text))
            {
                MessageBox.Show("Bạn chưa chọn trạng thái");
                cboStatus.Focus();
                return false;
            }
            if (cboPLoai.Text == "Khoa dược")
            {
                if (string.IsNullOrEmpty(txtPPxuatduoc.Text))
                {
                    MessageBox.Show("Bạn chưa chọn phương pháp xuất dược");
                    txtPPxuatduoc.Focus();
                    return false;
                }
            }
            if (lupMaBVSD.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn bệnh viện sử dụng!");
                lupMaBVSD.Focus();
                return false;
            }
            if (cboPLoai.Text == "Tủ trực" || cboChuyenKhoa.Text == "Trực cấp cứu")
            {
                if (lupNhomBP.EditValue == null)
                {
                    MessageBox.Show("Bạn chưa chọn nhóm khoa phòng");
                    lupNhomBP.Focus();
                    return false;
                }
            }
            return true;
        }
        #endregion
        List<KPhong> _lkhoaphong = new List<KPhong>();
        private void usDSKhoaPhong_Load(object sender, EventArgs e)
        {
            
            if (DungChung.Bien.MaBV == "30303")
                panelControl3.Visible = true;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                ckctruccapcuu.Visible = true;
            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            lupMaBVSD.Properties.DataSource = dataContext.BenhViens.Where(p => p.Connect).ToList();
            _lkhoaphong = null;
            _lkhoaphong = dataContext.KPhongs.OrderBy(p => p.TenKP).ToList();
            if (DungChung.Bien.MaBV == "24012")
            {
                List<KPhong> lkp = _lkhoaphong.Where(p => p.PLoai == ("Lâm sàng") || p.PLoai == ("Phòng khám") || p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang).OrderBy(p => p.TenKP).ToList();
                lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
                cklKhoaPhong.DataSource = lkp;
            }
            else
            {
                List<KPhong> lkp = dataContext.KPhongs.Where(p => p.Status == 1 && (p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)).OrderBy(p => p.TenKP).ToList();
                lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
                cklKhoaPhong.DataSource = lkp;
            }
            cklKhoaPhong.CheckAll();
            lupNhomBP.Properties.DataSource = _lkhoaphong.Where(p => p.PLoai == ("Lâm sàng") || p.PLoai == ("Phòng khám") || p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang);
            lupNhomKPgrv.DataSource = _lkhoaphong;
            grcKhoaPhong.DataSource = null;
            grcKhoaPhong.DataSource = _lkhoaphong.ToList();

            cboChuyenKhoa.Properties.DataSource = DungChung.Bien._lChuyenKhoa.Where(p => p.LoaiCK == 1 || p.LoaiCK == 3).OrderBy(p => p.ChuyenKhoa).ToList();

            enableControl(false);
            txtMaKP.Focus();
        }

        private void grvKhoaPhong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int makp = 0;
            cklKhoaPhong.UnCheckAll();
            if (grvKhoaPhong.GetFocusedRowCellValue(colPloai) != null)
            {
                if (DungChung.Bien.MaBV == "24012" && grvKhoaPhong.GetFocusedRowCellValue(colPloai).ToString() == "Tủ trực")
                {
                    panelControl3.Visible = true;
                }
            }
            if (grvKhoaPhong.GetFocusedRowCellValue(colMaKP) != null)
            {

                makp = Convert.ToInt32(grvKhoaPhong.GetFocusedRowCellValue(colMaKP));
                txtMaKP.Text = grvKhoaPhong.GetFocusedRowCellValue(colMaKP).ToString();
                if (grvKhoaPhong.GetFocusedRowCellValue(colTenKP) != null && grvKhoaPhong.GetFocusedRowCellValue(colTenKP).ToString() != "")
                {
                    txtTenKP.Text = grvKhoaPhong.GetFocusedRowCellValue(colTenKP).ToString();
                }
                else
                {
                    txtTenKP.Text = "";
                }
                if (grvKhoaPhong.GetFocusedRowCellValue(colBoPhanSD) != null && grvKhoaPhong.GetFocusedRowCellValue(colBoPhanSD).ToString() != "")
                {
                    string dskp = grvKhoaPhong.GetFocusedRowCellValue(colBoPhanSD).ToString();
                    List<string> lkp = dskp.Split(';').ToList();
                    foreach (string a in lkp)
                    {
                        for (int i = 1; i < cklKhoaPhong.ItemCount; i++)
                        {
                            if (cklKhoaPhong.GetItemValue(i).ToString() == a.Trim())
                                cklKhoaPhong.SetItemChecked(i, true);
                        }
                    }
                }
                else
                {
                    cklKhoaPhong.UnCheckAll();
                }

                if (_lkhoaphong.Where(p => p.MaKP == makp).First().NhomKP != null && _lkhoaphong.Where(p => p.MaKP == makp).First().NhomKP.ToString() != "")
                    lupNhomBP.EditValue = _lkhoaphong.Where(p => p.MaKP == makp).First().NhomKP;
                else
                    lupNhomBP.EditValue = "";
                cboChuyenKhoa.EditValue = _lkhoaphong.Where(p => p.MaKP == makp).First().MaCK;
                if (_lkhoaphong.Where(p => p.MaKP == makp).First().TrongBV != null && _lkhoaphong.Where(p => p.MaKP == makp).First().TrongBV.ToString() != "")
                    cboTrongBV.SelectedIndex = _lkhoaphong.Where(p => p.MaKP == makp).First().TrongBV.Value;
                else
                    cboTrongBV.SelectedIndex = -1;
                cboPLoai.Text = _lkhoaphong.Where(p => p.MaKP == makp).First().PLoai;
                if (_lkhoaphong.Where(p => p.MaKP == makp).First().PPXuat != null && _lkhoaphong.Where(p => p.MaKP == makp).First().PPXuat.ToString() != "")
                {
                    int ppx = _lkhoaphong.Where(p => p.MaKP == makp).First().PPXuat.Value;
                    txtPPxuatduoc.SelectedIndex = ppx;
                }
                else
                    txtPPxuatduoc.SelectedIndex = -1;

                txtDChi.Text = _lkhoaphong.Where(p => p.MaKP == makp).First().DChi;
                if (_lkhoaphong.Where(p => p.MaKP == makp).First().PLoai == "Phòng khám" || _lkhoaphong.Where(p => p.MaKP == makp).First().PLoai == "Lâm sàng")
                {
                    if (_lkhoaphong.Where(p => p.MaKP == makp).First().QuanLy == 1)
                        ckctruccapcuu.Checked = true;
                    else
                        ckctruccapcuu.Checked = false;
                }
                else
                    ckctruccapcuu.Checked = false;
                if (_lkhoaphong.Where(p => p.MaKP == makp).First().Status != null)
                {
                    cboStatus.SelectedIndex = _lkhoaphong.Where(p => p.MaKP == makp).First().Status.Value;
                }
                else
                {
                    cboStatus.SelectedIndex = -1;
                }
                if (_lkhoaphong.Where(p => p.MaKP == makp).First().IsDongY == true)
                    ckcDY.Checked = true;
                else
                    ckcDY.Checked = false;
                if (_lkhoaphong.Where(p => p.MaKP == makp).First().QuanLy != null)
                    cboQLNT.SelectedIndex = _lkhoaphong.Where(p => p.MaKP == makp).First().QuanLy.Value;
                else
                    cboQLNT.SelectedIndex = -1;
                if (_lkhoaphong.Where(p => p.MaKP == makp).First().STTHT != null)
                    txtSTT.Text = _lkhoaphong.Where(p => p.MaKP == makp).First().STTHT.ToString();
                else
                    txtSTT.Text = "";
                txtMaQD.Text = _lkhoaphong.Where(p => p.MaKP == makp).First().MaQD;
                //memoBuongGiuong.Text = _lkhoaphong.Where(p => p.MaKP == makp).First().BuongGiuong;
                cbo_pphhdy.SelectedIndex = _lkhoaphong.Where(p => p.MaKP == makp).First().PPHHDY;
                chk_TuTruc.Checked = _lkhoaphong.Where(p => p.MaKP == makp).First().TuTruc;
                chk_MuaNgoai.Checked = _lkhoaphong.Where(p => p.MaKP == makp).First().IsMuaNgoai.HasValue ? _lkhoaphong.Where(p => p.MaKP == makp).First().IsMuaNgoai.Value: false;
                lupMaBVSD.EditValue = _lkhoaphong.Where(p => p.MaKP == makp).First().MaBVsd;
                //var _lcb = dataContext.CanBoes.Where(p => p.MaKP == makp).ToList();
                //if (_lcb.Count > 0)
                if (!string.IsNullOrEmpty(_lkhoaphong.Where(p => p.MaKP == makp).First().NguoiPhat))
                    lupnguoiphat.Text = _lkhoaphong.Where(p => p.MaKP == makp).First().NguoiPhat.ToString();
                else
                    lupnguoiphat.Text = "";
                txtGHCD.EditValue = _lkhoaphong.FirstOrDefault(p => p.MaKP == makp).GHCD;
                //if (!string.IsNullOrEmpty(_lkhoaphong.Where(p => p.MaKP == makp).First().MaKP9324))
                //    txtmakp9324.Text = _lkhoaphong.Where(p => p.MaKP == makp).First().MaKP9324.ToString();
                //else
                //    txtmakp9324.Text = "";
                var ktra = _lkhoaphong.Where(p => p.MaKP == makp).ToList();
                if (ktra.Count > 0)
                {
                    if (ktra.First().PLoai == "Khoa dược" || ktra.First().PLoai == "Tủ trực")
                    {
                        lupnguoiphat.Visible = true;
                        labelControl9.Visible = true;
                    }
                    else
                    {
                        lupnguoiphat.Visible = false;
                        labelControl9.Visible = false;
                    }
                }
            }
            else
            {
                txtMaKP.Text = "";
            }
        }

        private void btnSuaKb_Click(object sender, EventArgs e)
        {
            enableControl(true);
            cbo_pphhdy.Properties.ReadOnly = true;
            //txtMaKP.Enabled = false;
            TTLuu = 2;
            txtTenKP.Focus();
            //btnBuongKH.Enabled = true;
        }

        private void btnLuuKb_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (TTLuu)
                {
                    case 1:
                        //Makp = Convert.ToInt32(txtMaKP.Text.Trim());
                        //var ma = dataContext.KPhongs.Where(p => p.MaKP == Makp).ToList();
                        //if (ma.Count > 0)
                        //{
                        //    MessageBox.Show("Mã bộ phận đã có, vui lòng nhập mã khác");
                        //}
                        //else
                        //{
                        KPhong bp = new KPhong();
                        //bp.MaKP = Makp;
                        bp.TenKP = txtTenKP.Text;
                        bp.PLoai = cboPLoai.Text;
                        bp.TrongBV = cboTrongBV.SelectedIndex;
                        bp.Status = cboStatus.SelectedIndex;
                        if (ckcDY.Checked == true)
                            bp.IsDongY = true;
                        else
                            bp.IsDongY = false;
                        //if (ckctruccapcuu.Checked==true)
                        //    bp.QuanLy = 1;
                        if (lupNhomBP.EditValue != null)
                            bp.NhomKP = Convert.ToInt32(lupNhomBP.EditValue);
                        else
                            bp.NhomKP = 0;
                        bp.DChi = txtDChi.Text;
                        if (!string.IsNullOrEmpty(cboChuyenKhoa.Text))
                        {
                            bp.ChuyenKhoa = cboChuyenKhoa.Text;
                            bp.MaCK = Convert.ToInt32(cboChuyenKhoa.EditValue);
                        }
                        else
                            bp.ChuyenKhoa = "";
                        if (cboPLoai.Text == "Khoa dược" || cboPLoai.Text == "Tủ trực")
                        {
                            bp.PPXuat = txtPPxuatduoc.SelectedIndex;
                            bp.QuanLy = cboQLNT.SelectedIndex;
                        }
                        else if (cboPLoai.Text == "Phòng khám" || cboPLoai.Text == "Lâm sàng")
                        {
                            bp.PPXuat = -1;
                            bp.QuanLy = 1;
                        }
                        else
                        {
                            bp.PPXuat = -1;
                            bp.QuanLy = -1;
                        }
                        if (cboPLoai.Text == "Lâm sàng")
                            bp.PPXuat = txtPPxuatduoc.SelectedIndex;
                        bp.MaQD = txtMaQD.Text.Trim();
                        //bp.BuongGiuong = memoBuongGiuong.Text.Trim();
                        bp.PPHHDY = Convert.ToByte(cbo_pphhdy.SelectedIndex);
                        bp.TuTruc = chk_TuTruc.Checked;
                        bp.IsMuaNgoai = chk_MuaNgoai.Checked;
                        bp.MaBVsd = lupMaBVSD.EditValue.ToString();
                        List<int> lMakpsd = new List<int>();
                        for (int i = 1; i < cklKhoaPhong.ItemCount; i++)
                        {
                            if (cklKhoaPhong.GetItemChecked(i))
                            {
                                lMakpsd.Add(Convert.ToInt32(cklKhoaPhong.GetItemValue(i)));
                            }
                        }
                        bp.MaKPsd = string.Join(";", lMakpsd);
                        bp.NguoiPhat = lupnguoiphat.Text;
                        if (txtGHCD.EditValue != null)
                            bp.GHCD = int.Parse(txtGHCD.EditValue.ToString());
                        else
                            bp.GHCD = null;
                        if (!string.IsNullOrEmpty(txtSTT.Text))
                            bp.STTHT = Convert.ToInt32(txtSTT.Text);
                        //bp.MaKP9324 = txtmakp9324.Text;
                        dataContext.KPhongs.Add(bp);
                        dataContext.SaveChanges();
                        #region add khoa phòng vào dịch vụ và đơn thuốc
                        var qdv = (from a in dataContext.DichVus.Where(p => p.MaKPsd != null) select a).ToList();
                        foreach (var dv in qdv)
                        {
                            dv.MaKPsd = dv.MaKPsd + bp.MaKP.ToString() + ";";
                        }
                        #endregion
                        try
                        {
                            dataContext.SaveChanges();
                            //ttluu = true;
                        }
                        catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                        {
                            //ttluu = false;
                            Exception raise = dbEx;

                            foreach (var validationErrors in dbEx.EntityValidationErrors)
                            {

                                foreach (var validationError in validationErrors.ValidationErrors)
                                {

                                    string message = string.Format("{0}:{1}",

                                      validationErrors.Entry.Entity.ToString(),

                                        validationError.ErrorMessage);

                                    // raise a new exception nesting

                                    // the current instance as InnerException

                                    raise = new InvalidOperationException(message, raise);

                                }

                            }

                            throw raise;
                        }
                        //dataContext.SaveChanges();
                        enableControl(false);
                        //usDSKhoaPhong_Load(sender, e);
                        TTLuu = 0;
                        //}
                        break;



                    case 2:
                        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                        if (!string.IsNullOrEmpty(txtMaKP.Text))
                        {
                            int makp = Convert.ToInt32(txtMaKP.Text);
                            KPhong bpsua = _data.KPhongs.Single(p => p.MaKP == (makp));
                            bpsua.TenKP = txtTenKP.Text;
                            bpsua.PLoai = cboPLoai.Text;
                            bpsua.TrongBV = cboTrongBV.SelectedIndex;
                            bpsua.Status = cboStatus.SelectedIndex;
                            if (ckcDY.Checked == true)
                                bpsua.IsDongY = true;
                            else
                                bpsua.IsDongY = false;
                            if (ckctruccapcuu.Checked == true)
                                bpsua.QuanLy = 1;
                            if (lupNhomBP.EditValue != null)
                                bpsua.NhomKP = Convert.ToInt32(lupNhomBP.EditValue);
                            else
                                bpsua.NhomKP = 0;
                            bpsua.DChi = txtDChi.Text.Trim();
                            if (!string.IsNullOrEmpty(cboChuyenKhoa.Text))
                            {
                                bpsua.ChuyenKhoa = cboChuyenKhoa.Text;
                                bpsua.MaCK = Convert.ToInt32(cboChuyenKhoa.EditValue);
                            }
                            else
                                bpsua.ChuyenKhoa = "";
                            if (cboPLoai.Text == "Khoa dược" || cboPLoai.Text == "Tủ trực")
                            {
                                bpsua.PPXuat = txtPPxuatduoc.SelectedIndex;
                                bpsua.QuanLy = cboQLNT.SelectedIndex;
                                bpsua.PPHHDY = Convert.ToByte(cbo_pphhdy.SelectedIndex);
                                bpsua.IsMuaNgoai = chk_MuaNgoai.Checked;
                            }
                            else if (cboPLoai.Text == "Phòng khám" || cboPLoai.Text == "Lâm sàng")
                            {
                                if (ckctruccapcuu.Checked == true)
                                {
                                    bpsua.PPXuat = -1;
                                    bpsua.QuanLy = 1;
                                }
                                else
                                {
                                    bpsua.PPXuat = -1;
                                    bpsua.QuanLy = -1;
                                }
                            }
                            else
                            {
                                bpsua.PPXuat = -1;
                                bpsua.QuanLy = -1;
                            }
                            if (cboPLoai.Text == "Lâm sàng")
                                bpsua.PPXuat = txtPPxuatduoc.SelectedIndex;
                            //bpsua.BuongGiuong = memoBuongGiuong.Text.Trim();
                            bpsua.NguoiPhat = lupnguoiphat.Text;
                            if (txtGHCD.EditValue != null)
                                bpsua.GHCD = int.Parse(txtGHCD.EditValue.ToString());
                            else
                                bpsua.GHCD = null;
                            //bpsua.MaKP9324 = txtmakp9324.Text;
                            bpsua.MaQD = txtMaQD.Text.Trim();
                            bpsua.TuTruc = chk_TuTruc.Checked;
                            bpsua.IsMuaNgoai = chk_MuaNgoai.Checked;
                            bpsua.MaBVsd = lupMaBVSD.EditValue.ToString();
                            List<int> lMakpsd2 = new List<int>();
                            for (int i = 1; i < cklKhoaPhong.ItemCount; i++)
                            {
                                if (cklKhoaPhong.GetItemChecked(i))
                                {
                                    lMakpsd2.Add(Convert.ToInt32(cklKhoaPhong.GetItemValue(i)));
                                }
                            }
                            bpsua.MaKPsd = string.Join(";", lMakpsd2);
                            if (!string.IsNullOrEmpty(txtSTT.Text))
                                bpsua.STTHT = Convert.ToInt32(txtSTT.Text);
                            _data.SaveChanges();
                            enableControl(false);
                            //usDSKhoaPhong_Load(sender, e);
                            TTLuu = 0;
                        }
                        break;
                }
                usDSKhoaPhong_Load(sender, e);
                 
            }
        }
        string _timkiem = "";
        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
                _timkiem = txtTimKiem.Text.ToLower().Trim();
            else
                _timkiem = "";
            grcKhoaPhong.DataSource = _lkhoaphong.Where(p => p.TenKP.ToLower().Contains(_timkiem)).ToList();
        }

        private void btnXoaKb_Click(object sender, EventArgs e)
        {

            int ot;
            int _int_maKP = 0;
            if (Int32.TryParse(txtMaKP.Text, out ot))
                _int_maKP = Convert.ToInt32(txtMaKP.Text);
            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (_int_maKP > 0)
            {
                bool _CoTheXoa = true;
                //var ktrakb = dataContext.BNKBs.Where(p => p.MaKP == _int_maKP).ToList();
                //var ktradthuoc = dataContext.DThuoccts.Where(p => p.MaKP == _int_maKP).ToList();
                //var ktrand = dataContext.NhapDs.Where(p => p.MaKP == _int_maKP).ToList();
                //if (ktrakb.Count > 0 || ktradthuoc.Count>0||ktrand.Count>0)
                //{
                //    MessageBox.Show("Khoa|Phòng đã được sử dụng, bạn không thể xóa !");
                //    _CoTheXoa = false;
                //}
                if (_CoTheXoa)
                {
                    try
                    {
                        DialogResult _result;
                        _result = MessageBox.Show("Bạn muốn xóa khoa|phòng: ", "Xóa khoa phòng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == DialogResult.Yes)
                        {
                            var xoa = dataContext.KPhongs.Single(p => p.MaKP == _int_maKP);
                            dataContext.KPhongs.Remove(xoa);
                            dataContext.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không xóa được!" + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có bộ phận để xóa");
            }
        }

        private void lupNhomBP_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //if (TTLuu == 2 || TTLuu == 1)
            //{
            //    if (lupNhomBP.EditValue != null)
            //    {
            //        if (!string.IsNullOrEmpty(cboPLoai.Text))
            //        {
            //            if (cboPLoai.Text != "Tủ trực" && cboChuyenKhoa.Text != "Trực cấp cứu")
            //            {
            //                MessageBox.Show("Những khoa phòng có phân loại là 'Tủ trực' hoặc chuyên khoa 'Trực cấp cứu' mới có nhóm khoa phòng");
            //                e.Cancel = true;
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Bạn chưa chọn phân loại");
            //            e.Cancel = true;
            //        }
            //    }
            //    else
            //    {
            //        if (!string.IsNullOrEmpty(cboPLoai.Text))
            //        {
            //            if (cboPLoai.Text != "Tủ trực" && cboChuyenKhoa.Text != "Trực cấp cứu")
            //            {
            //                MessageBox.Show("Những khoa phòng có phân loại là 'Tủ trực' hoặc chuyên khoa 'Trực cấp cứu' mới có nhóm khoa phòng");
            //                e.Cancel = true;
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Bạn chưa chọn phân loại");
            //            e.Cancel = true;
            //        }
            //    }
            //}
        }

        private void cboPLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPPxuatduoc.Properties.Items.Clear();
            bool b = false;
            if (cboPLoai.Text == "Khoa dược" || cboPLoai.Text == "Tủ trực")
            {
                labPPXD.Text = "PP xuất dược:";
                b = true;
                txtPPxuatduoc.Properties.Items.Add("Xuất theo giá trong danh mục");
                txtPPxuatduoc.Properties.Items.Add("Nhập trước xuất trước(có giá ưu tiên)");
                txtPPxuatduoc.Properties.Items.Add("Xuất theo hạn dùng(có giá ưu tiên)");
                txtPPxuatduoc.Properties.Items.Add("Nhập trước xuất trước(theo số lô)");
                lupnguoiphat.Visible = true;
                labelControl9.Visible = true;
            }
            chk_MuaNgoai.Visible = b;
            chk_TuTruc.Visible = b;
            chk_MuaNgoai.Visible = b;
            txtPPxuatduoc.Visible = b;
            labPPXD.Visible = b;
            cboQLNT.Visible = b;
            labQLNT.Visible = b;
            cbo_pphhdy.Visible = b;
            lab_PPDY.Visible = b;
            if (cboPLoai.Text == "Lâm sàng" || cboPLoai.Text == "Phòng khám")
            {
                ckctruccapcuu.Visible = true;
                if (cboPLoai.Text == "Lâm sàng")
                {
                    labPPXD.Visible = true;
                    txtPPxuatduoc.Visible = true;
                    labPPXD.Text = "Số giường KH:";
                    txtPPxuatduoc.Properties.Items.Clear();
                    for (int i = 0; i <= 500; i++)
                    {
                        txtPPxuatduoc.Properties.Items.Add(i);
                    }
                }
            }
            if (cboPLoai.Text == "Tủ trực" || cboPLoai.Text == "Cận lâm sàng")
            {
                lupNhomBP.Visible = true;
            }
            else
            {
                lupNhomBP.Visible = false;
                lupNhomBP.EditValue = null;
            }
            //if (TTLuu == 2 || TTLuu == 1)
            //{
            //    lupNhomBP.EditValue = "";
            //    //lupNhomBP.Text = "";
            //}
        }

        private void cboPLoai_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }

        private void grvKhoaPhong_DataSourceChanged(object sender, EventArgs e)
        {
            grvKhoaPhong_FocusedRowChanged(null, null);
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void memoBuongGiuong_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show("cấu trúc nhập buồng giường: \n'tenbuong1{giường1,giường2,...};tenbuong2{giường1,giường2};...'");
        }

        private void memoBuongGiuong_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cboPLoai_EditValueChanged(object sender, EventArgs e)
        {
            if (cboPLoai.Text == "Lâm sàng")
            {
                //memoBuongGiuong.Visible = true;
                //labelControl9.Visible = true;
                btnBuongKH.Visible = true;
            }
            else
            {
                //memoBuongGiuong.Visible = false;
                //labelControl9.Visible = false;
                btnBuongKH.Visible = false;
            }
        }

        private void btnChuyenKhoa_Click(object sender, EventArgs e)
        {
            //Frm_Dm_ChuyenKhoa frm = new Frm_Dm_ChuyenKhoa();
            //frm.ShowDialog();
        }

        private void btnBuongKH_Click(object sender, EventArgs e)
        {
            int makp = 0;
            if (grvKhoaPhong.GetFocusedRowCellValue(colMaKP) != null)
            {
                makp = Convert.ToInt32(grvKhoaPhong.GetFocusedRowCellValue(colMaKP));
                txtMaKP.Text = grvKhoaPhong.GetFocusedRowCellValue(colMaKP).ToString();
                QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach frm = new FormThamSo.frm_NhapBuongGiuongKeKoach(makp);
                frm.ShowDialog();
            }
        }

        private void lupNhomBP_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cboChuyenKhoa_EditValueChanged(object sender, EventArgs e)
        {
            if (cboChuyenKhoa.Text == "Trực cấp cứu")
            {
                lupNhomBP.Visible = true;
            }
            else
            {
                lupNhomBP.Visible = false;
                lupNhomBP.EditValue = null;
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            enableControl(false);
        }
        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }
        private void txtGHCD_Validating(object sender, CancelEventArgs e)
        {
            if(IsNumber(txtGHCD.Text)==false)
            {
                    e.Cancel = true;
                    dxErrorProvider1.SetError(txtGHCD, "Bạn hãy nhập số nguyên dương. Vui lòng nhập lại");
                    txtGHCD.Focus();
            }
            else
            {
                int n = int.Parse(txtGHCD.Text);
                if (n < 0)
                {
                    e.Cancel = true;
                    txtGHCD.Focus();
                    dxErrorProvider1.SetError(txtGHCD, "Bạn đã nhập sai. Vui lòng nhập lại");
                }
                else
                {
                    e.Cancel = false;
                    dxErrorProvider1.SetError(txtGHCD, null);
                    
                }
            }
            
        }

        private void grcKhoaPhong_Click(object sender, EventArgs e)
        {

        }
    }
}
