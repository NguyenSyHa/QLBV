using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormDanhMuc
{
    public partial class frmAdmin : DevExpress.XtraEditors.XtraForm
    {
        public frmAdmin()
        {
            InitializeComponent();
        }

        int TTluu = 2;
        string tenDN = "";
        public frmAdmin(string TenDN, int TT)
        {
            tenDN = TenDN;
            TTluu = TT;
            InitializeComponent();
        }
        private void lupMaCB_EditValueChanged(object sender, EventArgs e)
        {

        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(lupMaCB.Text))
            {
                MessageBox.Show("bạn chưa chọn tên cán bộ");
                lupMaCB.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenDN.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên đăng nhập");
                txtTenDN.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMatKhauCu.Text))
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu!");
                txtMatKhauCu.Focus();
                return false;
            }
            else
            {
                 var kt = dataContext.ADMINs.Where(p => p.TenDN == tenDN).ToList();
                    if (kt.Count > 0)
                    {
                        String mk = QLBV_Library.QLBV_Ham.MaHoa(txtMatKhauCu.Text);
                        if (kt.First().MatK != (mk))
                        {
                            MessageBox.Show("Mật khẩu chưa đúng!");
                            txtMatKhauCu.Focus();
                            return false;
                        }
                    }
              
            }
            if (TTluu == 1)
            {
                if (string.IsNullOrEmpty(txtMatKhauMoi.Text))
                {
                    MessageBox.Show("Bạn chưa nhập mật khẩu mới");
                    txtMatKhauMoi.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtXacNhan.Text))
                {
                    MessageBox.Show("Bạn chưa nhập xác nhận mật khẩu");
                    txtXacNhan.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(cboCapDo.Text))
            {
                MessageBox.Show("Bạn chưa chọn cấp độ");
                cboCapDo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboSTATUS.Text))
            {
                MessageBox.Show("Bạn chưa chọn trạng thái");
                cboSTATUS.Focus();
                return false;
            }
            return true;
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            var a = (from cb in dataContext.CanBoes.OrderBy(p => p.TenCB)
                     join kp in dataContext.KPhongs on cb.MaKP equals kp.MaKP
                     select new { cb.MaCB, cb.TenCB, kp.TenKP }).ToList();
            lupMaCB.Properties.DataSource = a;
            if (!string.IsNullOrEmpty(tenDN))
            {
                var ktAdmin = from cb in dataContext.CanBoes.Where(p => p.MaCB == DungChung.Bien.MaCB)
                              join kp in dataContext.KPhongs on cb.MaKP equals kp.MaKP
                              where kp.PLoai == "Admin"
                              select cb;
                if (ktAdmin.Count() == 0)
                {
                    cboCapDo.Properties.ReadOnly = true;
                    cboSTATUS.Properties.ReadOnly = true;
                }
                List<ADMIN> _dataAdmin = new List<ADMIN>();
                _dataAdmin = dataContext.ADMINs.Where(p => p.TenDN == tenDN).ToList();
                if (_dataAdmin.Count > 0)
                {
                    txtID.Text = _dataAdmin.First().ID.ToString();
                    txtTenDN.Text = _dataAdmin.First().TenDN;
                    cboCapDo.Text = _dataAdmin.First().CapDo.ToString();
                    cboSTATUS.Text = _dataAdmin.First().STATUS;
                    lupMaCB.EditValue = _dataAdmin.First().MaCB;
                }
                txtID.Properties.ReadOnly = true;
                txtTenDN.Properties.ReadOnly = true;
                lupMaCB.Properties.ReadOnly = true;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (TTluu)
                {
                    case 1:
                        var kt = dataContext.ADMINs.Where(p => p.TenDN == (txtTenDN.Text.Trim())).ToList();
                        if (kt.Count > 0)
                        {
                            MessageBox.Show("Tên đăng nhập đã tồn tại");
                        }
                        else
                        {
                            if (txtMatKhauMoi.Text.Length >= 6 && txtMatKhauMoi.Text.Length <= 20)
                            {
                                if (txtXacNhan.Text == (txtMatKhauMoi.Text))
                                {
                                    ADMIN admin = new ADMIN();
                                    admin.MaCB = lupMaCB.EditValue.ToString();
                                    admin.TenGoi = lupMaCB.Text;
                                    admin.TenDN = txtTenDN.Text.Trim();
                                    if (!string.IsNullOrEmpty(txtMatKhauMoi.Text))
                                        admin.MatK = QLBV_Library.QLBV_Ham.MaHoa(txtMatKhauMoi.Text.Trim());
                                    admin.CapDo = int.Parse(cboCapDo.Text);
                                    admin.STATUS = cboSTATUS.Text.Trim();
                                    dataContext.ADMINs.Add(admin);
                                    dataContext.SaveChanges();
                                    MessageBox.Show("Lưu thành công");
                                }
                                else
                                {
                                    MessageBox.Show("Mật khẩu không khớp nhau");
                                    txtMatKhauMoi.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Mật khẩu phải có độ dài lớn hơn 6 ký tự và nhỏ hơn 20 ký tự!");
                            }
                        }
                        break;
                    case 2:
                        ADMIN adminsua = dataContext.ADMINs.Single(p => p.TenDN == tenDN);
                        adminsua.TenGoi = lupMaCB.Text;
                        adminsua.TenDN = txtTenDN.Text.Trim();
                        if (!string.IsNullOrEmpty(txtMatKhauMoi.Text) || !string.IsNullOrEmpty(txtXacNhan.Text))
                        {
                            if (txtMatKhauMoi.Text.Length >= 6 && txtMatKhauMoi.Text.Length <= 50)
                            {
                                if (txtXacNhan.Text == (txtMatKhauMoi.Text))
                                {
                                    adminsua.MatK = QLBV_Library.QLBV_Ham.MaHoa(txtMatKhauMoi.Text.Trim());
                                    adminsua.CapDo = int.Parse(cboCapDo.Text.Trim());
                                    adminsua.STATUS = cboSTATUS.Text.Trim();
                                    dataContext.SaveChanges();
                                    MessageBox.Show("Sửa thành công");
                                }
                                else
                                {
                                    MessageBox.Show("Mật khẩu không khớp nhau!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Mật khẩu phải có độ dài lớn hơn 6 ký tự và nhỏ hơn 50 ký tự!");
                            }
                        }
                        else
                        {
                            adminsua.CapDo = int.Parse(cboCapDo.Text.Trim());
                            adminsua.STATUS = cboSTATUS.Text.Trim();
                            dataContext.SaveChanges();
                            MessageBox.Show("Sửa thành công");
                        }
                        break;
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //frmCapDo frm = new frmCapDo();
            //this.Hide();
            //frm.ShowDialog();
        }

        private void txtTenDN_Leave(object sender, EventArgs e)
        {
            if (TTluu == 1)
            {
                String tenDN = txtTenDN.Text;
                var ktTK = dataContext.ADMINs.Where(p => p.TenDN.ToLower() == tenDN.ToLower()).ToList();
                if (ktTK.Count > 0)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!");
                }
            }
        }
    }
}