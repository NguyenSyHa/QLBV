using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV.FormThamSo;
namespace QLBV.FormDanhMuc
{
    public partial class NhapCanBo : DevExpress.XtraEditors.XtraUserControl
    {
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public NhapCanBo()
        {
            InitializeComponent();
        
        }
        private int TTLuu=0;
        private void EnableControl(bool T)
        {
            txtCapBac.Properties.ReadOnly = !T;
            chkKhoa.Properties.ReadOnly = !T;
            txtChucVu.Properties.ReadOnly = !T;
            txtDiaChi.Properties.ReadOnly = !T;
            txtHSLuong.Properties.ReadOnly = !T;
            deNgayTL.Properties.ReadOnly = !T;
            mmBangCap.Properties.ReadOnly = !T;
            txtNgoaiNgu.Properties.ReadOnly = !T;
            txtMaNgach.Properties.ReadOnly = !T;
            txtTinHoc.Properties.ReadOnly = !T;
            deNgayCapCCHN.Properties.ReadOnly = !T;
            lupKphongBC.Properties.ReadOnly = !T;
            rgBienChe.Properties.ReadOnly = !T;
            txtMaCB.Properties.ReadOnly = !T;
            txtSoDT.Properties.ReadOnly = !T;
            txtSTT.Properties.ReadOnly = !T;
            txtTenCB.Properties.ReadOnly = !T;
            txt_MaBHXH.Properties.ReadOnly = !T;
            txtCCHN.Properties.ReadOnly = !T;
            dtNamSinh.Properties.ReadOnly = !T;
            lupDToc.Properties.ReadOnly = !T;
            lupMaKP.Properties.ReadOnly = !T;
            btnLuuKb.Enabled = T;
            radNamNu.Properties.ReadOnly = !T;
            if (moi)
                btnMoiKb.Enabled = !T;
            if (sua)
                btnSuaKb.Enabled = !T;
            if (xoa)
                btnXoaKb.Enabled = !T;
            grcCanBo.Enabled = !T;
            cklKP.Enabled = T;
            ckcSudung.Enabled = T;
       
        }
        private void resetcontrol() {
            txtCapBac.Text = "";
            txtChucVu.Text = "";
            txtDiaChi.Text = "";
            dtNamSinh.Text = "01/01/1970";
            txtHSLuong.Text = "";
            deNgayTL.DateTime = Convert.ToDateTime("01/01/2000");
            mmBangCap.Text = "";
            txtMaNgach.Text = "";
            txtNgoaiNgu.Text = "";
            txtTinHoc.Text = "";
            deNgayCapCCHN.DateTime = Convert.ToDateTime("01/01/2000");
            lupKphongBC.EditValue = 0;
            rgBienChe.SelectedIndex = 1;
            txtMaCB.Text = "";
            txtSoDT.Text = "";
            txtSTT.Text = "";
            txtTenCB.Text="";
            txt_MaBHXH.Text="";
            txtCCHN.Text = "";          
            lupMaKP.EditValue = 0;
            chkKhoa.Checked = false;
        }
        private void grvCanBo_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
        }
        #region checkSave
        private  bool checkSave() { 
        if(string.IsNullOrEmpty(txtMaCB.Text)){
        MessageBox.Show("Bạn chưa nhập mã cán bộ");
            txtMaCB.Focus();
            return false;
        }
        if (!string.IsNullOrEmpty(txtTenCB.Text)) {
            MessageBox.Show("Bạn chưa nhập tên cán bộ");
            txtTenCB.Focus();
            return false;
        }
        if (dtNamSinh.EditValue == null) {
            MessageBox.Show("Bạn chưa chọn năm sinh");
            dtNamSinh.Focus();
            return false;
        }
        if (radNamNu.SelectedIndex != 0 || radNamNu.SelectedIndex != 1) {
            MessageBox.Show("Bạn chưa chọn giới tính");
            radNamNu.Focus();
            return false;
        }
        if (lupDToc.EditValue == null)
        {
            MessageBox.Show("Bạn chưa chọn dân tộc");
            lupDToc.Focus();
            return false;
        }
        if (lupMaKP.EditValue == null) {
            MessageBox.Show("Bạn chưa chọn mã khoa phòng");
            lupMaKP.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtChucVu.Text)) {
            MessageBox.Show("Bạn chưa chọn chức vụ");
            txtChucVu.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtCapBac.Text)) {
            MessageBox.Show("Bạn chưa chọn cấp bậc");
            txtCapBac.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtSoDT.Text)) {
            MessageBox.Show("Bạn chưa nhập số điện thoại");
            txtSoDT.Focus();
        }
            return true;
        }
        #endregion
        public static void _loadKPsd(string dsKPsd, List<KhoaPhong> _lKPsd, CheckedListBoxControl cklKP)
        {

            try
            {
                if (!string.IsNullOrEmpty(dsKPsd))
                {
                    string[] kp = dsKPsd.Split(';');
                    for (int i = 0; i < cklKP.ItemCount; i++)
                    {

                        cklKP.SetItemChecked(i, false);

                    }
                    foreach (var item in kp)
                    {
                        foreach (var item2 in _lKPsd)
                        {
                            if (!string.IsNullOrEmpty(item))
                                if (Convert.ToInt32(item) == item2.MaKP)
                                {
                                    item2.Check = true;
                                    for (int i = 0; i < cklKP.ItemCount; i++)
                                    {
                                        if (cklKP.GetItemValue(i) != null && Convert.ToInt32(cklKP.GetItemValue(i)) == item2.MaKP)
                                        {
                                            cklKP.SetItemChecked(i, true);
                                            break;
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load Danh sách khoa phòng sử dụng: " + ex.Message);
            }

        }
        private void grvCanBo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvCanBo.GetFocusedRowCellValue(colMaCB) != null && grvCanBo.GetFocusedRowCellValue(colMaCB).ToString() != "")
            {
                string Macb=grvCanBo.GetFocusedRowCellValue(colMaCB).ToString();
                txtMaCB.Text = Macb;
                if (grvCanBo.GetFocusedRowCellValue(colTenCB) != null && grvCanBo.GetFocusedRowCellValue(colTenCB).ToString() != "")
                {
                    txtTenCB.Text = grvCanBo.GetFocusedRowCellValue(colTenCB).ToString();
                }
                else {
                    txtTenCB.Text = "";
                }
                if (grvCanBo.GetFocusedRowCellValue(colBHXH) != null && grvCanBo.GetFocusedRowCellValue(colBHXH).ToString() != "")
                {
                    txt_MaBHXH.Text = grvCanBo.GetFocusedRowCellValue(colBHXH).ToString();
                }
                else
                {
                    txt_MaBHXH.Text = "";
                }
                if (grvCanBo.GetFocusedRowCellValue(colMaKP) != null )
                {
                    lupMaKP.EditValue= Convert.ToInt32( grvCanBo.GetFocusedRowCellValue(colMaKP));
                }
                else
                {
                    lupMaKP.EditValue = 0;
                }
                if (grvCanBo.GetFocusedRowCellValue(colChucVu) != null && grvCanBo.GetFocusedRowCellValue(colChucVu).ToString() != "")
                {
                    txtChucVu.Properties.NullText = grvCanBo.GetFocusedRowCellValue(colChucVu).ToString();
                }
                else
                {
                    txtChucVu.Properties.NullText = "";
                }
                if (grvCanBo.GetFocusedRowCellValue(colCapBac) != null && grvCanBo.GetFocusedRowCellValue(colCapBac).ToString() != "")
                {
                    txtCapBac.Properties.NullText = grvCanBo.GetFocusedRowCellValue(colCapBac).ToString();
                }
                else
                {
                    txtCapBac.Properties.NullText = "";
                }
                
                var makpsd = dataContext.CanBoes.Where(p => p.MaCB == Macb).FirstOrDefault();

                if (makpsd != null)
                {
                    _loadKPsd(makpsd.MaKPsd, _lKPsd, cklKP);
                    if (!string.IsNullOrEmpty(makpsd.HSLuong))
                        txtHSLuong.Text = makpsd.HSLuong;
                    else
                        txtHSLuong.Text = "";

                    if (makpsd.NgayTangLuong != null)
                        deNgayTL.DateTime = makpsd.NgayTangLuong.Value;
                    else
                        deNgayTL.DateTime = Convert.ToDateTime("01/01/2000");

                    if (makpsd.NgayCapCCHN != null)
                        deNgayCapCCHN.DateTime = makpsd.NgayCapCCHN.Value;
                    else
                        deNgayCapCCHN.DateTime = Convert.ToDateTime("01/01/2000");

                    if (makpsd.MaKPBC != null)
                        lupKphongBC.EditValue = makpsd.MaKPBC;
                    else
                        lupKphongBC.EditValue = 0;
                    if (makpsd.BienChe != null)
                        rgBienChe.SelectedIndex = makpsd.BienChe ?? 0;
                    else
                        rgBienChe.SelectedIndex = 1;
                    //sua
                        if (makpsd.GioiTinh == 0)
                            //radNamNu.SelectedIndex = makpsd.GioiTinh ?? 0;
                            radNamNu.SelectedIndex = 1;
                        else
                            radNamNu.SelectedIndex = 0;


                    if (!string.IsNullOrEmpty( makpsd.NamSinh))
                    {
                        if (makpsd.NamSinh.Length > 4)
                            dtNamSinh.Text = makpsd.NamSinh;
                        else
                        {
                            dtNamSinh.Text = "01/01/" + makpsd.NamSinh;
                        }
                    }
                    else
                        dtNamSinh.Text = "01/01/1970";
                    if (!string.IsNullOrEmpty(makpsd.BangCap))
                        mmBangCap.Text = makpsd.BangCap;
                    else
                        mmBangCap.Text = "";
                    if (makpsd.STTHT != null)
                        txtSTT.Text = makpsd.STTHT.ToString();
                    else
                        txtSTT.Text = "";
                    if (!string.IsNullOrEmpty(makpsd.MaNgach))
                        txtMaNgach.Text = makpsd.MaNgach;
                    else
                        txtMaNgach.Text = "";
                    if (!string.IsNullOrEmpty(makpsd.NgoaiNgu))
                        txtNgoaiNgu.Text = makpsd.NgoaiNgu;
                    else
                        txtNgoaiNgu.Text = "";
                    if (!string.IsNullOrEmpty(makpsd.TinHoc))
                        txtTinHoc.Text = makpsd.TinHoc;
                    else
                        txtTinHoc.Text = "";
                }
                ckcSudung.Checked = Convert.ToBoolean(makpsd.Status);
                if (grvCanBo.GetFocusedRowCellValue(colDiaChi) != null && grvCanBo.GetFocusedRowCellValue(colDiaChi).ToString() != "")
                {
                    txtDiaChi.Text = grvCanBo.GetFocusedRowCellValue(colDiaChi).ToString();
                }
                else
                {
                    txtDiaChi.Text = "";
                }
                if (grvCanBo.GetFocusedRowCellValue(colSoDT) != null && grvCanBo.GetFocusedRowCellValue(colSoDT).ToString() != "")
                {
                    txtSoDT.Text = grvCanBo.GetFocusedRowCellValue(colSoDT).ToString();
                }
                else
                {
                    txtSoDT.Text = "";
                }
                if (grvCanBo.GetFocusedRowCellValue(colKhoa) != null && grvCanBo.GetFocusedRowCellValue(colKhoa).ToString() != "")
                {
                    if (grvCanBo.GetFocusedRowCellValue(colKhoa).ToString() == "1")
                        chkKhoa.Checked = true;
                    else
                        chkKhoa.Checked = false;
                }
                else
                {
                    chkKhoa.Checked = false;
                }
                if (grvCanBo.GetFocusedRowCellValue(colCCHN) != null && grvCanBo.GetFocusedRowCellValue(colCCHN).ToString() != "")
                {
                    txtCCHN.Text = grvCanBo.GetFocusedRowCellValue(colCCHN).ToString();
                }
                else
                {
                    txtCCHN.Text = "";
                }
            }
            else {
                txtMaCB.Text = "";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        private bool KTLuu() 
        {
            if(string.IsNullOrEmpty(txtMaCB.Text)){
                MessageBox.Show("Chưa có mã cán bộ");
                return false;
            }
            if (string.IsNullOrEmpty(txtTenCB.Text)) {
                MessageBox.Show("Chưa nhập tên cán bộ!");
                txtTenCB.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupMaKP.Text))
            {
                MessageBox.Show("Chưa chọn khoa phòng!");
                lupMaKP.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtChucVu.Text))
            {
                MessageBox.Show("Chưa nhập chức vụ!");
                txtChucVu.Focus();
                return false;
            }
            if (txtCapBac.Text.ToLower().Trim().Contains("bs") || txtCapBac.Text.ToLower().Contains("bác sĩ"))
            {
                if(string.IsNullOrEmpty(txtCCHN.Text))
                {
                    MessageBox.Show("Chức vụ bác sĩ cần nhập Chứng chỉ hành nghề");
                    txtCCHN.Focus();
                    return false;
                }
                else
                {
                   
                }
            }
            // Bỏ chặn 
            //if(!string.IsNullOrEmpty(txtCCHN.Text))
            //{
            //     if (lupKphongBC.EditValue != null)
            //        {
            //            if (Convert.ToInt32(lupKphongBC.EditValue) != 0)
            //            {
            //                dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //                int makpbc = Convert.ToInt32(lupKphongBC.EditValue);
            //                //if (TTLuu == 1)
            //                //{
            //                var ktcchn = dataContext.CanBoes.Where(p => p.MaCCHN == txtCCHN.Text.Trim() && p.MaKPBC != makpbc).ToList();
            //                if (ktcchn.Count() > 0)
            //                {
            //                    MessageBox.Show("Chứng chỉ hành nghề chỉ có 1 khoa phòng biên chế, không thể lưu!");
            //                    txtCCHN.Focus();
            //                    return false;
            //                }
            //                if (TTLuu == 1)
            //                {
            //                    var ktcchn1 = dataContext.CanBoes.Where(p => p.MaCCHN == txtCCHN.Text.Trim() && p.MaKPBC == makpbc).ToList();
            //                    if (ktcchn1.Count() > 0)
            //                    {
            //                        MessageBox.Show("Chứng chỉ hành nghề chỉ có 1 khoa phòng biên chế, không thể lưu!");
            //                        txtCCHN.Focus();
            //                        return false;
            //                    }
            //                }

            //            //}
            //                else if (TTLuu == 2)
            //                {
            //                    var ktcchn2 = dataContext.CanBoes.Where(p => p.MaCCHN == txtCCHN.Text.Trim() && p.MaCB != txtMaCB.Text.Trim() && p.MaKPBC == makpbc).ToList();
            //                    if (ktcchn2.Count() > 0)
            //                    {
            //                        MessageBox.Show("Chứng chỉ hành nghề trùng lặp, không thể lưu!");
            //                        txtCCHN.Focus();
            //                        return false;
            //                    }
            //                }
            //            }
            //        }
           // }
            int dem = 0;
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemCheckState(i) == CheckState.Checked)
                    dem++;
            }
            if (dem == 0)
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng hoạt động");
                return false;
            }
            //if (string.IsNullOrEmpty(txtCapBac.Text))
            //{
            //    MessageBox.Show("Chưa nhập cấp bậc!");
            //    txtCapBac.Focus();
            //    return false;
            //}
            return true;
        }
        private void btnLuuKb_Click(object sender, EventArgs e)
        {
            CanBo canbo = new CanBo();

            if (KTLuu())
            {

                switch (TTLuu)
                {
                    case 1:
                        string macb=txtMaCB.Text;
                        var ma = dataContext.CanBoes.Where(p => p.MaCB == (macb)).ToList();
                        if (ma.Count > 0)
                        {
                            MessageBox.Show("Mã cán bộ đã có, vui lòng nhập mã khác");
                        }
                        else
                        {
                            if ((txtChucVu.Text.ToLower().Contains("bác sĩ") || txtChucVu.Text.ToLower().Contains("bác sỹ")) && txtCCHN.Text == "")
                            {
                                MessageBox.Show("Thiếu chứng chỉ hành nghề!");
                            }
                            else
                            {
                                if (chkKhoa.Checked)
                                    canbo.Khoa = 1;
                                else
                                    canbo.Khoa = 0;
                                canbo.MaCB = txtMaCB.Text;
                                canbo.TenCB = txtTenCB.Text;
                                if (txt_MaBHXH.Text.Length > 10)
                                {
                                    MessageBox.Show("Mã BHXH không được quá 10 ký tự");
                                    return;
                                }
                                else
                                {
                                    canbo.MaBHXH = txt_MaBHXH.Text;
                                }
                                canbo.MaCCHN = txtCCHN.Text;
                                canbo.MaKP = lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue);
                                canbo.ChucVu = txtChucVu.Text;
                                canbo.CapBac = txtCapBac.Text;
                                canbo.SoDT = txtSoDT.Text;
                                canbo.NamSinh = dtNamSinh.Text;
                                canbo.NgayCapCCHN = deNgayCapCCHN.DateTime;
                                canbo.HSLuong = txtHSLuong.Text;
                                //canbo.b
                                canbo.NgayTangLuong = deNgayTL.DateTime;
                                canbo.BangCap = mmBangCap.Text;
                                canbo.TinHoc = txtTinHoc.Text;
                                canbo.NgoaiNgu = txtNgoaiNgu.Text;
                                canbo.MaNgach = txtMaNgach.Text;
                                canbo.BienChe = rgBienChe.SelectedIndex;
                                //if (lupKphongBC.EditValue != null)
                                canbo.MaKPBC = lupKphongBC.EditValue == null ? 0 : Convert.ToInt32(lupKphongBC.EditValue);
                                canbo.Status = Convert.ToInt32(ckcSudung.Checked);
                                if (lupDToc.EditValue != null && lupDToc.EditValue.ToString() != "")
                                    canbo.MaDT = lupDToc.EditValue.ToString();
                                string _makpsd = ";";
                                for (int i = 0; i < cklKP.ItemCount; i++)
                                {
                                    if (cklKP.GetItemCheckState(i) == CheckState.Checked)
                                        _makpsd += cklKP.GetItemValue(i) + ";";
                                }
                                canbo.MaKPsd = _makpsd;
                                canbo.DiaChi = txtDiaChi.Text;
                                if (radNamNu.SelectedIndex == 0)
                                {
                                    canbo.GioiTinh = 1;
                                }
                                else
                                {
                                    canbo.GioiTinh = 0;
                                }
                                if (!string.IsNullOrEmpty(txtSTT.Text))
                                    canbo.STTHT = Convert.ToInt32(txtSTT.Text);
                                dataContext.CanBoes.Add(canbo);
                                if (dataContext.SaveChanges() >= 0)
                                {
                                    MessageBox.Show("Thêm mới thành công");
                                    NhapCanBo_Load(sender, e);
                                }
                            }
                        }
                        break;
                    case 2:
                        
                        if (!string.IsNullOrEmpty(txtMaCB.Text))
                        {
                            string id = txtMaCB.Text;
                            CanBo canbosua = dataContext.CanBoes.Single(p => p.MaCB== (id));
                            if (chkKhoa.Checked)
                                canbosua.Khoa = 1;
                            else
                                canbosua.Khoa = 0;
                            canbosua.MaCCHN = txtCCHN.Text;
                            canbosua.TenCB = txtTenCB.Text;
                            if (txt_MaBHXH.Text.Length > 10)
                            {
                                MessageBox.Show("Mã BHXH không được quá 10 ký tự");
                                return;
                            }
                            else
                            {
                                canbosua.MaBHXH = txt_MaBHXH.Text;
                            }
                            canbosua.MaKP = lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue);
                            canbosua.ChucVu = txtChucVu.Text.Trim();
                            canbosua.CapBac = txtCapBac.Text.Trim();
                            canbosua.SoDT = txtSoDT.Text.Trim();
                            canbosua.DiaChi = txtDiaChi.Text.Trim();
                            canbosua.NamSinh = dtNamSinh.Text.Trim();
                            canbosua.NgayCapCCHN = deNgayCapCCHN.DateTime;
                            canbosua.HSLuong = txtHSLuong.Text;
                            canbosua.BangCap = mmBangCap.Text;
                            canbosua.TinHoc = txtTinHoc.Text;
                            canbosua.NgoaiNgu = txtNgoaiNgu.Text;
                            canbosua.MaNgach = txtMaNgach.Text;
                            canbosua.NgayTangLuong = deNgayTL.DateTime;
                            canbosua.BienChe = rgBienChe.SelectedIndex;
                            //if (lupKphongBC.EditValue != null)
                            //    canbosua.MaKPBC = Convert.ToInt32(lupKphongBC.EditValue);
                            canbosua.MaKPBC = lupKphongBC.EditValue == null ? 0 : Convert.ToInt32(lupKphongBC.EditValue);
                            canbosua.Status = Convert.ToInt32(ckcSudung.Checked);
                            string _makpsd = ";";
                            for (int i = 0; i < cklKP.ItemCount; i++)
                            {
                                if (cklKP.GetItemCheckState(i) == CheckState.Checked)
                                    _makpsd += cklKP.GetItemValue(i) + ";";
                            }
                            canbosua.MaKPsd = _makpsd;
                            if (radNamNu.SelectedIndex == 0)
                            {
                                canbosua.GioiTinh = 1;
                            }
                            else
                            {
                                canbosua.GioiTinh = 0;
                            }
                            if (!string.IsNullOrEmpty(txtSTT.Text))
                                canbosua.STTHT = Convert.ToInt32(txtSTT.Text);
                            if ((txtChucVu.Text.ToLower().Contains("bác sĩ") || txtChucVu.Text.ToLower().Contains("bác sỹ")) && txtCCHN.Text == "")
                            {
                                MessageBox.Show("Thiếu chứng chỉ hành nghề!");
                            }
                            else
                            {
                                if (dataContext.SaveChanges() >= 0)
                                {
                                    MessageBox.Show("Sửa thành công");
                                    NhapCanBo_Load(sender, e);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("bạn chưa chọn hoặc không có cán bộ để sửa");
                        }
                        break;
                }

            }
        }
          public class KhoaPhong
        {
            public bool _check;
            public int _maKP;
            public string _kp;

            public int MaKP { get { return _maKP; } set { _maKP = value; } }
            public bool Check { get { return _check; } set { _check = value; } }
            public string TenKP { get { return _kp; } set { _kp = value; } }
        }

          bool moi = false, sua = false, xoa = false;
        List<CanBo> _lcanbo = new List<CanBo>();
        List<KhoaPhong> _lKPsd = new List<KhoaPhong>();
        List<KPhong> _lKPall = new List<KPhong>();
        private void NhapCanBo_Load(object sender, EventArgs e)
        {
            //int ns = 1950;
            //List<string> namsinha = new List<string>();
            //for (int i = 1950; i < 2001; i++)
            //{
            //    namsinha.Add(i.ToString());
            //}
            //dtNamSinh.Properties.Items.AddRange(namsinha);
            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var perm = dataContext.Permissions.Where(p => p.TenDN == DungChung.Bien.TenDN).Where(p => p.ID == 9052).ToList();
            if (perm.Count > 0)
            {
                if (perm.First().C_New == true)
                    moi = true;
                if (perm.First().C_Edit == true)
                    sua = true;
                if (perm.First().C_Delete == true)
                    xoa = true;                
            }
            CanBo canbo = new CanBo();
            //if (radNamNu.SelectedIndex == 0)
            //{
            //    canbo.GioiTinh = 1;
            //}
            //else
            //{
            //    canbo.GioiTinh = 0;
            //}
           
            //if (radNamNu.SelectedIndex == 0 )
            //{
            //    canbo.GioiTinh = 0;
            //}
            //else if ( radNamNu.SelectedIndex == 1)
            //{
            //    canbo.GioiTinh = 1;
            //}
            EnableControl(false);
            _lKPall = dataContext.KPhongs.OrderBy(p => p.PLoai).ThenBy(p => p.TenKP).ToList();
            lupMaKP.Properties.DataSource = _lKPall;
            lupMaKPds.DataSource = _lKPall;
            lupKphongBC.Properties.DataSource = _lKPall;
            _lcanbo=dataContext.CanBoes.ToList();;
            grcCanBo.DataSource = _lcanbo;
            var datoc = dataContext.DanTocs.ToList();
            lupDToc.Properties.DataSource = datoc.ToList();
            //txt_MaCanBo.DataBindings.Clear();
            //txt_MaCanBo.DataBindings.Add("Text", dataContext.CanBoes, "TenCB");
            var dt = _lKPall.Where(p => p.MaKP == (DungChung.Bien.MaKP)).ToList();
            if (dt.Count > 0 && dt.First().PLoai == "Admin")
            {
                panelControl4.Enabled = true;
            }
            else
                panelControl4.Enabled = false;
            _lKPsd = (from kp in _lKPall
                      select new KhoaPhong()
                      {
                          Check = false,
                          MaKP = kp.MaKP,
                          TenKP = kp.TenKP
                      }).Distinct().OrderBy(p => p.TenKP).ToList();
            cklKP.DataSource = _lKPsd;
            ckcSudung.Checked = true;
            dmcapbac(sender, e);
            dmchucvu(sender, e);
        }

        private void btnMoiKb_Click(object sender, EventArgs e)
        {
            EnableControl(true);
            resetcontrol();
            txtTenCB.Focus();
            TTLuu = 1;
            
        }

        private void btnSuaKb_Click(object sender, EventArgs e)
        {
            EnableControl(true);
                TTLuu=2;
                txtTenCB.Focus();
                txtMaCB.Enabled = false;
          
        }

        private void txtMaCB_TextChanged(object sender, EventArgs e)
        {

        }
        string _timkiem = "";
        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
                _timkiem = txtTimKiem.Text.ToLower().Trim();
            else
                _timkiem = "";
            grcCanBo.DataSource = _lcanbo.Where(p => p.TenCB.ToLower().Contains(_timkiem) || p.MaCB.ToLower().Contains(_timkiem)).ToList();
        }

        private void grcCanBo_DataSourceChanged(object sender, EventArgs e)
        {
            grvCanBo_FocusedRowChanged(null, null);
        }

        private void grvCanBo_DataSourceChanged(object sender, EventArgs e)
        {
            grvCanBo_FocusedRowChanged(null, null);
        }

        private void btnXoaKb_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaCB.Text))
            {
                var ktdtct = dataContext.DThuoccts.Where(p => p.MaCB == txtMaCB.Text).ToList();
                if (ktdtct.Count > 0)
                {
                    MessageBox.Show(txtTenCB.Text+ " đã được sử dụng, bạn không thể xóa");
                    return;

                }
                var ktdt = dataContext.DThuocs.Where(p => p.MaCB == txtMaCB.Text).ToList();
                if (ktdt.Count > 0)
                {
                    MessageBox.Show(txtTenCB.Text + " đã được sử dụng, bạn không thể xóa");
                    return;

                }
                var ktnd = dataContext.NhapDs.Where(p => p.MaCB == txtMaCB.Text).ToList();
                if (ktnd.Count > 0)
                {
                    MessageBox.Show(txtTenCB.Text + " đã được sử dụng, bạn không thể xóa");
                    return;

                }
                DialogResult result = MessageBox.Show("Bạn muốn xóa thông tin cán bộ: " + txtTenCB.Text + " ?","Hỏi xóa ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var _lcb = dataContext.CanBoes.Single(p => p.MaCB == txtMaCB.Text);
                        dataContext.CanBoes.Remove(_lcb);
                        dataContext.SaveChanges();
                        MessageBox.Show("Đã xóa thành công");
                        NhapCanBo_Load(null, null);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);

                    }
                }
            }
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtChucVu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                frm_DMChucVu frm = new frm_DMChucVu();
                frm.ShowDialog();
                dmchucvu(sender, e);
            }
        }

        private void txtCapBac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                frm_DMcCapBac frm = new frm_DMcCapBac();
                frm.ShowDialog();
                dmcapbac(sender, e);
            }
        }
        List<ChucVu> _chuvu = new List<ChucVu>();
        List<CapBac> _capbac = new List<CapBac>();
        private void dmchucvu(object sender, EventArgs e)
        {
            _chuvu.Clear();
            var chucvu = dataContext.ChucVus.Where(p => p.Status == 1).ToList();
            foreach (var item in chucvu)
            {
                ChucVu moi = new ChucVu();
                moi.ID_CV = item.ID_CV;
                //moi.MoTa = item.MoTa;
                //moi.Status = item.Status;
                moi.Ten_CV = item.Ten_CV;
                _chuvu.Add(moi);
            }
            bindingSource1.DataSource = _chuvu.ToList();
            txtChucVu.Properties.DataSource = bindingSource1;
        }

        private void dmcapbac(object sender, EventArgs e)
        {
            _capbac.Clear();
            var capbac = dataContext.CapBacs.Where(p => p.Status == 1).ToList();
            foreach (var item in capbac)
            {
                CapBac moi = new CapBac();
                moi.ID_CB = item.ID_CB;
                //moi.MoTa = item.MoTa;
                //moi.Status = item.Status;
                moi.Ten_CB = item.Ten_CB;
                _capbac.Add(moi);
            }
            bindingSource2.DataSource = _capbac.ToList();
            txtCapBac.Properties.DataSource = bindingSource2;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    var _lcbup = dataContext.CanBoes.Where(p => p.MaKPsd == null).ToList();
            //    foreach (var item in _lcbup)
            //    {
            //        item.MaKPsd = item.MaKP.ToString() + ";";
            //    }
            //    MessageBox.Show("Update thành công :" + _lcbup.Count + " bản ghi");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Update xảy ra lỗi: " + ex.ToString());
            //}
        }

        private void lupMaKP_EditValueChanged(object sender, EventArgs e)
        {
            if (lupMaKP.EditValue != null)
            {
                int makp = Convert.ToInt32(lupMaKP.EditValue);
                for (int i = 0; i < cklKP.ItemCount; i++)
                {
                    if (cklKP.GetItemValue(i) != null && Convert.ToInt32(cklKP.GetItemValue(i)) == makp)
                    {
                        cklKP.SetItemChecked(i, true);
                        break;
                    }
                }
            }
        }

        private void radNamNu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panelControl5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grvCanBo_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
                grvCanBo_FocusedRowChanged(null, null);
        }

        private void lupKphongBC_EditValueChanged(object sender, EventArgs e)
        {
            if (lupKphongBC.EditValue != null)
            {
                int makp = Convert.ToInt32(lupKphongBC.EditValue);
                for (int i = 0; i < cklKP.ItemCount; i++)
                {
                    if (cklKP.GetItemValue(i) != null && Convert.ToInt32(cklKP.GetItemValue(i)) == makp)
                    {
                        cklKP.SetItemChecked(i, true);
                        break;
                    }
                }
            }
        }
    }
}
