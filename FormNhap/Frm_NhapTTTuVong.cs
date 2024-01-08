using DevExpress.XtraEditors;
using QLBV_Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class Frm_NhapTTTuVong : DevExpress.XtraEditors.XtraForm
    {
        private QLBVEntities _datacontext;
        int mabn = 0;
        public Frm_NhapTTTuVong(int mabn)
        {
            InitializeComponent();
            this.mabn = mabn;
            _datacontext = QLBV_Database.Common.EntityDbContext.DbContext;
        }

        private void Frm_NhapTTTuVong_Load(object sender, EventArgs e)
        {
            QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _datacontext = QLBV_Database.Common.EntityDbContext.DbContext;
            dtNgayCap.DateTime = DateTime.Now;
            dtNgayTV.DateTime = DateTime.Now;
            string stringMaKP = DungChung.Bien.MaKP.ToString();

            var canbos = _datacontext.CanBoes.Where(p => p.MaKPsd.Contains(stringMaKP)).ToList();
            lupNguoiGhiGiay.Properties.DataSource = canbos;

            var bn = _datacontext.BenhNhans.FirstOrDefault(p => p.MaBNhan == mabn);
            var gbt = _datacontext.GiayBaoTus.FirstOrDefault(p => p.MA_BN == mabn);

            txtMaBN.Text = mabn.ToString();
            txtTenBN.Text = bn.TenBNhan;

            if (gbt != null)
            {
                btnXoa.Enabled = true;
                dtNgayTV.DateTime = gbt.NGAY_TV;
                cboTinhTrangTV.SelectedIndex = gbt.TINH_TRANG_TV ?? 0;
                dtNgayCap.DateTime = gbt.NGAY_CAPGIAYBT ?? DateTime.Now;
                lupNguoiGhiGiay.EditValue = gbt.NGUOI_GHIGIAY;
                mmoNNhanTV.Text = gbt.NGUYENNHAN_TV;
                mmoNguoiThan.Text = gbt.NGUOI_THANTHICH;
            }
            else
            {
                dtNgayTV.DateTime = DateTime.Now;
                cboTinhTrangTV.SelectedIndex = 0;
                dtNgayCap.DateTime = DateTime.Now;
                lupNguoiGhiGiay.EditValue = 0;
                mmoNNhanTV.Text = "";
                mmoNguoiThan.Text = "";
                btnXoa.Enabled = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    DungChung.Ham.CallProcessWaitingForm(Save, "Đang lưu", "Lưu thông tin");
                    Frm_NhapTTTuVong_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Chưa nhập đủ thông tin. Vui lòng kiểm tra lại!");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void Save()
        {
            var bntv = _datacontext.GiayBaoTus.FirstOrDefault(p => p.MA_BN == mabn);
            if (bntv == null)  // thêm mới
            {
                GiayBaoTu gbt = new GiayBaoTu();

                gbt.MA_BN = mabn;
                gbt.NGAY_TV = dtNgayTV.DateTime;
                gbt.TINH_TRANG_TV = cboTinhTrangTV.SelectedIndex;
                gbt.NGAY_CAPGIAYBT = dtNgayCap.DateTime;
                gbt.NGUOI_GHIGIAY = lupNguoiGhiGiay.Text;
                gbt.NGUYENNHAN_TV = mmoNNhanTV.Text;
                gbt.NGUOI_THANTHICH = mmoNguoiThan.Text;

                var so = _datacontext.GiayBaoTus.OrderByDescending(p => p.NGAY_TV).ThenBy(p => p.SOCT).FirstOrDefault();
                if (so == null)
                {
                    gbt.SOCT = "00001";
                }
                else
                {
                    int SoCT = Convert.ToInt32(so.SOCT);
                    if (so != null)
                    {
                        if (DateTime.Now.Year != so.NGAY_TV.Year)
                        {
                            gbt.SOCT = "00001";
                        }
                        else
                        {
                            gbt.SOCT = (SoCT + 1).ToString("d5");
                        }
                    }
                }
                _datacontext.GiayBaoTus.Add(gbt);
            }
            else  //sửa
            {
                bntv.MA_BN = mabn;
                bntv.NGAY_TV = dtNgayTV.DateTime;
                bntv.TINH_TRANG_TV = cboTinhTrangTV.SelectedIndex;
                bntv.NGAY_CAPGIAYBT = dtNgayCap.DateTime;
                bntv.NGUOI_GHIGIAY = lupNguoiGhiGiay.Text;
                bntv.NGUYENNHAN_TV = mmoNNhanTV.Text;
                bntv.NGUOI_THANTHICH = mmoNguoiThan.Text;
            }
            _datacontext.SaveChanges();
            MessageBox.Show("Lưu thành công");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DungChung.Ham.CallProcessWaitingForm(Delete, "Đang xóa", "Xóa thông tin");
                Frm_NhapTTTuVong_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void Delete()
        {
            var bntv = _datacontext.GiayBaoTus.FirstOrDefault(p => p.MA_BN == mabn);
            _datacontext.GiayBaoTus.Remove(bntv);
            _datacontext.SaveChanges();
        }

        string errorMessage = string.Empty;
        private bool ValidateInput()
        {
            if (cboTinhTrangTV.SelectedIndex < 0 || string.IsNullOrEmpty(cboTinhTrangTV.Text))
            {
                cboTinhTrangTV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(mmoNNhanTV.Text))
            {
                mmoNNhanTV.Focus();
                return false;
            }
            if (lupNguoiGhiGiay.EditValue == null || lupNguoiGhiGiay.EditValue.ToString().Trim() == "")
            {
                lupNguoiGhiGiay.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(mmoNguoiThan.Text))
            {
                mmoNguoiThan.Focus();
                return false;
            }
            return true;
        }
    }
}