using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang
{
    public partial class frm_tranfer : DevExpress.XtraEditors.XtraForm
    {
        int mabn = 0;
        public frm_tranfer(int mabn)
        {
            InitializeComponent();
            this.mabn = mabn;
        }
        public void resetcontrol()
        {
            txtDonViChuyen.Text = "";
            dtNgaychuyen.DateTime = DateTime.Now;
            txtNoiDung.Text = "";
            txtSoTien.Text = "";

        }
        void enabled(bool b)
        {
            txtDonViChuyen.Properties.ReadOnly = b;
            txtNoiDung.Properties.ReadOnly = b;
            txtSoTien.Properties.ReadOnly = b;
            dtNgaychuyen.Properties.ReadOnly = b;
            btnLuu.Enabled = !b;
            btnSua.Enabled = b;
            btnXoa.Enabled = b;
        }
        QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        double sotientt = 0;
        private void frm_tranfer_Load(object sender, EventArgs e)
        {
            _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var bn = _db.BenhNhans.Where(p => p.MaBNhan == mabn).FirstOrDefault();
            var tu = _db.TamUngs.Where(p => p.MaBNhan == mabn && p.PhanLoai == 1).Select(p => p.SoTien).FirstOrDefault();
            var q = _db.Tranfers.Where(p => p.MaBNhan == mabn).FirstOrDefault();
            var vienphi = (from vp in _db.VienPhis.Where(p => p.MaBNhan == mabn)
                           join vpct in _db.VienPhicts.Where(p => p.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                           group new { vpct, vp } by new { vp.MaBNhan } into kq
                           select new
                           {
                               kq.Key.MaBNhan,
                               SoTien = kq.Sum(p => p.vpct.TienBN)
                           }).ToList();

            if (bn != null)
            {
                txtMaBN.Text = bn.MaBNhan.ToString();
                txtTenBN.Text = bn.TenBNhan;
            }
            if (q != null)
            {
                enabled(true);
                txtDonViChuyen.Text = q.DonViChuyen;
                txtMaBN.Text = mabn.ToString();
                txtNoiDung.Text = q.NoiDung;
                txtSoTien.Text = q.SoTien.ToString();
                dtNgaychuyen.DateTime = q.NgayChuyen;
            }
            else
            {
                if (tu != null)
                {
                    txtSoTien.Text = tu.ToString();
                    sotientt = tu.Value;
                }
                else
                {
                    sotientt = vienphi.First().SoTien;
                }
               
                enabled(false);
                resetcontrol();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            enabled(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Bạn muốn hủy thông tin chuyển khoản?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var q = _db.Tranfers.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                _db.Tranfers.Remove(q);
                var tu = _db.TamUngs.Where(p => p.MaBNhan == mabn && p.PhanLoai == 1).FirstOrDefault();
               if(tu!=null)
                tu.ThanhToan = 0;
                if (_db.SaveChanges() >= 0)
                    frm_tranfer_Load(sender, e);
            }
        }
        bool checksave()
        {
            if (string.IsNullOrEmpty(txtDonViChuyen.Text))
            {
                MessageBox.Show("bạn chưa nhập đơn vị chuyển tiền");
                txtDonViChuyen.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSoTien.Text))
            {
                MessageBox.Show("bạn chưa nhập số tiền");
                txtSoTien.Focus();
                return false;
            }
            if (dtNgaychuyen.DateTime.Year<2001)
            {
                MessageBox.Show("Ngày nhận tiền không hợp lệ");
                dtNgaychuyen.Focus();
                return false;

            }
            var tt = _db.VienPhis.Where(p => p.MaBNhan == mabn).FirstOrDefault();
            if (tt == null)
            {
                MessageBox.Show("Bệnh nhân chưa được thanh toán");
                return false;
            }
            else
            {
                if (dtNgaychuyen.DateTime < tt.NgayTT.Value)
                {
                    MessageBox.Show("Ngày chuyển phải >= ngày thanh toán");
                    dtNgaychuyen.Focus();
                    return false;
                }
                if (dtNgaychuyen.DateTime > DateTime.Now)
                {
                    MessageBox.Show("Ngày chuyển phải <= ngày hiện tại");
                    dtNgaychuyen.Focus();
                    return false;
                }
            }
            if (Convert.ToDouble(txtSoTien.Text) != sotientt)
            {
                if (DialogResult.No == MessageBox.Show("Số tiền chuyển khoản khác số tiền bệnh nhân phải thanh toán\n Bạn vẫn muốn lưu?","Cảnh báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return false;
            }
            return true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!checksave())
                return;
            _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = _db.Tranfers.Where(p => p.MaBNhan == mabn).FirstOrDefault();
            if (q == null)
            {
                Tranfer moi = new Tranfer();
                moi.MaBNhan = mabn;
                moi.NgayChuyen = dtNgaychuyen.DateTime;
                moi.NoiDung = txtNoiDung.Text;
                moi.SoTien = Convert.ToDouble(txtSoTien.Text);
                moi.DonViChuyen = txtDonViChuyen.Text;
                _db.Tranfers.Add(moi);
                var tu = _db.TamUngs.Where(p => p.MaBNhan == mabn && p.PhanLoai == 1).FirstOrDefault();
                if (tu != null)
                    tu.ThanhToan = 1;
                _db.SaveChanges();
                frm_tranfer_Load(sender, e);
            }
            else
            {
                q.MaBNhan = mabn;
                q.NgayChuyen = dtNgaychuyen.DateTime;
                q.NoiDung = txtNoiDung.Text;
                q.SoTien = Convert.ToDouble(txtSoTien.Text);
                q.DonViChuyen = txtDonViChuyen.Text;
                _db.SaveChanges();
                frm_tranfer_Load(sender, e);
            }

        }

        private void txtSoTien_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoTien.Text))
                txtBangchu.Text = DungChung.Ham.DocTienBangChu(Convert.ToDouble(txtSoTien.Text), "đồng.");
            else txtBangchu.Text = "";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}