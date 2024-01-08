using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;

namespace QLBV.ChucNang.FormDanhMuc
{
    public partial class frm_HeThongMoRong : DevExpress.XtraEditors.XtraForm
    {
        public frm_HeThongMoRong()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data;
        private void BtnSave_Click(object sender, EventArgs e)
        {
            LuuHT();

        }
        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }
        private void LuuHT()
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string _mabv = DungChung.Bien.MaBV;
            var q = _data.HTHONGs.Where(p => p.MaBV == _mabv).ToList();
            if (q.Count > 0)
            {
                HTHONG _ht = _data.HTHONGs.Single(p => p.MaBV == _mabv);
                _ht.Email = txtEmail.EditValue != null ? txtEmail.EditValue.ToString() : "";
                short? _barcode = txtBarcodeNumber.Text != "" ? short.Parse(txtBarcodeNumber.EditValue.ToString()) : (short?)null;
                _ht.Barcode_Number = _barcode;

                _ht.Automatic_Barcode = checkEdit1.Checked;
                _ht.Website = txtWebsite.EditValue != null ? txtWebsite.EditValue.ToString() : "";
                _ht.Fax = txtFax.EditValue != null ? txtFax.EditValue.ToString() : "";
                _ht.TenNganHang = txtBanksName.EditValue != null ? txtBanksName.EditValue.ToString() : "";
                _ht.SoTaiKhoan = txtSoTK.EditValue != null ? txtSoTK.EditValue.ToString() : "";
                _ht.ImageURL = txtImageURL.EditValue != null ? txtImageURL.EditValue.ToString() : "";
                _ht.UrlPID = txtPID.Text ?? "";
                _ht.UrlUploadHSSK = txtupLoadHSSK.Text ?? "";
                _data.SaveChanges();
                MessageBox.Show("Lưu thành công!");
            }
            else
            {
                XtraMessageBox.Show("Bệnh viện chưa thiết lập hệ thống. Yêu cầu thiết lập thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ThongtinBanking_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _getInfo = _data.HTHONGs.Where(p => p.MaBV == DungChung.Bien.MaBV);
            if (_getInfo.Count() > 0)
            {
                txtBarcodeNumber.Text = _getInfo.First().Barcode_Number.ToString() ?? "";
                txtBanksName.Text = _getInfo.First().TenNganHang ?? "";
                txtEmail.Text = _getInfo.First().Email ?? "";
                txtFax.Text = _getInfo.First().Fax ?? "";
                txtImageURL.Text = _getInfo.First().ImageURL?? "";
                txtSoTK.Text = _getInfo.First().SoTaiKhoan ??"";
                checkEdit1.Checked = _getInfo.First().Automatic_Barcode == null ? false : (bool)_getInfo.First().Automatic_Barcode;
                txtWebsite.Text = _getInfo.First().Website ?? "";
                txtPID.Text = _getInfo.First().UrlPID ?? "";
                txtupLoadHSSK.Text = _getInfo.First().UrlUploadHSSK ?? "";
            }


        }

        private void txtBarcodeNumber_Validated(object sender, EventArgs e)
        {

        }

        private void txtFax_Validated(object sender, EventArgs e)
        {

        }

        private void txtBarcodeNumber_Validating(object sender, CancelEventArgs e)
        {
            if (IsNumber(txtBarcodeNumber.Text) == false)
            {
                if (txtBarcodeNumber.Text != "")
                {
                    e.Cancel = true;
                    txtBarcodeNumber.Focus();
                    dxErrorProvider1.SetError(txtBarcodeNumber, "Bạn nhập sai định dạng, vui lòng nhập lại !");
                    BtnSave.Enabled = false;
                }
                else
                {
                    e.Cancel = false;
                    dxErrorProvider1.SetError(txtBarcodeNumber, null);
                    BtnSave.Enabled = true;
                }

            }
            else
            {
                int n = int.Parse(txtBarcodeNumber.Text);
                if (n < -32768 || n > 32767)
                {
                    e.Cancel = true;
                    txtBarcodeNumber.Focus();
                    dxErrorProvider1.SetError(txtBarcodeNumber, "Bạn nhập quá giới hạn của dữ liệu. Vui lòng nhập lại");
                    BtnSave.Enabled = false;
                }
                else
                {
                    e.Cancel = false;
                    dxErrorProvider1.SetError(txtBarcodeNumber, null);
                    BtnSave.Enabled = true;
                }
            }
        }

        private void txtFax_Validating(object sender, CancelEventArgs e)
        {
            string s = txtFax.Text;
            if (s.Length > 50)
            {
                e.Cancel = true;
                txtFax.Focus();
                dxErrorProvider1.SetError(txtFax, "Bạn nhập quá giới hạn 50 kí tự. Vui lòng nhập lại");
                BtnSave.Enabled = false;
            }
            else
            {
                e.Cancel = false;
                dxErrorProvider1.SetError(txtFax, null);
                BtnSave.Enabled = true;
            }
        }

        private void txtSoTK_Validating(object sender, CancelEventArgs e)
        {
            string s = txtSoTK.Text;
            if (s.Length > 50)
            {
                e.Cancel = true;
                txtSoTK.Focus();
                dxErrorProvider1.SetError(txtSoTK, "Bạn nhập quá giới hạn 50 kí tự. Vui lòng nhập lại");
                BtnSave.Enabled = false;
            }
            else
            {
                e.Cancel = false;
                dxErrorProvider1.SetError(txtSoTK, null);
                BtnSave.Enabled = true;
            }
        }

        private void txtWebsite_Validating(object sender, CancelEventArgs e)
        {
            string s = txtWebsite.Text;
            if (s.Length > 50)
            {
                e.Cancel = true;
                txtWebsite.Focus();
                dxErrorProvider1.SetError(txtWebsite, "Bạn nhập quá giới hạn 50 kí tự. Vui lòng nhập lại");
                BtnSave.Enabled = false;
            }
            else
            {
                e.Cancel = false;
                dxErrorProvider1.SetError(txtWebsite, null);
                BtnSave.Enabled = true;
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string s = txtEmail.Text;
            if (s.Length > 50)
            {
                e.Cancel = true;
                txtEmail.Focus();
                dxErrorProvider1.SetError(txtEmail, "Bạn nhập quá giới hạn 50 kí tự. Vui lòng nhập lại");
                BtnSave.Enabled = false;
            }
            else
            {
                e.Cancel = false;
                dxErrorProvider1.SetError(txtEmail, null);
                BtnSave.Enabled = true;
            }
        }

        private void btnChooseImagePath_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Image Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "png",
                Filter = "logo files (*.png)|*.png",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true,

                Multiselect = false
            })
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        txtImageURL.Text = fileDialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}