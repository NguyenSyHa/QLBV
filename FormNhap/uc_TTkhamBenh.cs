using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class uc_TTkhamBenh : DevExpress.XtraEditors.XtraUserControl
    {
        int mabn = 0;
        public uc_TTkhamBenh(int mabn)
        {
            InitializeComponent();
            this.mabn = mabn;
        }

        public delegate bool LuuThoa(bool a);
        public LuuThoa thoat;

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCanNang.Text))
            {
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập cân nặng!", "Thông báo", MessageBoxButtons.OK);
                txtCanNang.Focus();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool ktraluu = true;
             string tuoi = DungChung.Ham.TuoitheoThang(_db, mabn, "72-0");
             if (tuoi.ToLower().Contains("tháng"))
             {
                 if(string.IsNullOrEmpty(txtCanNang.Text))
                 {
                     MessageBox.Show("Bệnh nhân <= 72 tháng tuổi, yêu cầu bắt buộc phải nhập cân nặng, \nBạn chưa nhập cân nặng!(Theo TT18)", "Thông báo", MessageBoxButtons.OK);
                     txtCanNang.Focus();
                     ktraluu = false;
                 }
             }
            _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var ttbx = _db.TTboXungs.Where(p => p.MaBNhan == mabn).FirstOrDefault();
            string cannang_chieucao = "", Mach_ND_HA = "";
            string[] arr = { "", "" };
            if (txtCanNang.Text != "") arr = txtCanNang.Text.Split('.');
            //if (arr[0] == "01") arr[0] = "1"; if (arr[0] == "02") arr[0] = "2"; if (arr[0] == "03") arr[0] = "3"; if (arr[0] == "04") arr[0] = "4"; if (arr[0] == "05") arr[0] = "5";
            //if (arr[0] == "06") arr[0] = "6"; if (arr[0] == "07") arr[0] = "7"; if (arr[0] == "08") arr[0] = "8"; if (arr[0] == "09") arr[0] = "9";
            //string x = arr[0] + "." + arr[1];
            //cannang_chieucao = x + ";" + txtChieuCao.Text;
            cannang_chieucao = txtCanNang.Text.Trim() + ";" + txtChieuCao.Text.Trim();
            Mach_ND_HA = txtMach.Text.Trim() + ";" + txtNhietDo.Text.Trim() + ";" + txtHuyetAp.Text.Trim() + ";" + txtnhipTho.Text.Trim();
            if (ktraluu)
            {
                if (ttbx != null)
                {
                    ttbx.CanNang_ChieuCao = cannang_chieucao;
                    ttbx.Mach_NDo_HAp = Mach_ND_HA;
                    _db.SaveChanges();
                    thoat(true);
                    MessageBox.Show("Lưu thành công");
                    this.uc_TTkhamBenh_Load(sender, e);
                }
                else
                {
                    TTboXung ttbx_new = new TTboXung();
                    ttbx_new.MaBNhan = mabn;
                    ttbx.CanNang_ChieuCao = cannang_chieucao;
                    ttbx.Mach_NDo_HAp = Mach_ND_HA;
                    _db.TTboXungs.Add(ttbx_new);
                    _db.SaveChanges();
                    thoat(true);
                    MessageBox.Show("Lưu thành công");
                    this.uc_TTkhamBenh_Load(sender, e);
                }
            }
            else
            {
                thoat(false);
            }
        }
        QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void uc_TTkhamBenh_Load(object sender, EventArgs e)
        {
            _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var ttbx = _db.TTboXungs.Where(p => p.MaBNhan == mabn).FirstOrDefault();
            if (ttbx != null)
            {
                if (!string.IsNullOrEmpty(ttbx.CanNang_ChieuCao) && ttbx.CanNang_ChieuCao.Contains(';'))
                {
                    string[] arrcannang = ttbx.CanNang_ChieuCao.Split(';'); if (arrcannang != null && arrcannang.Length > 0)
                        txtCanNang.Text = arrcannang[0];
                    if (arrcannang != null && arrcannang.Length > 1)
                    {
                        arrcannang[1] = arrcannang[1].Trim();
                        if (arrcannang[1] == "            ")
                        {
                            txtChieuCao.Text = "";
                        }
                        else
                        {
                            txtChieuCao.Text = arrcannang[1];
                        }
                    }
                } if (!string.IsNullOrEmpty(ttbx.Mach_NDo_HAp) && ttbx.Mach_NDo_HAp.Contains(';'))
                {
                    string[] machNDHA = ttbx.Mach_NDo_HAp.Split(';');

                    if (machNDHA != null && machNDHA.Length > 0)
                        txtMach.Text = machNDHA[0];

                    if (machNDHA != null && machNDHA.Length > 1)
                        txtNhietDo.Text = machNDHA[1];
                    if (machNDHA != null && machNDHA.Length > 2)
                        txtHuyetAp.Text = machNDHA[2];
                    if (machNDHA != null && machNDHA.Length > 3)
                        txtnhipTho.Text = machNDHA[3];
                }


                btnXoa.Enabled = true;
                btnLuu.Enabled = true;
            }
            else
            {
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Bạn muốn xóa thông tin khám bệnh?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var ttbx = _db.TTboXungs.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                if (ttbx != null)
                {
                    ttbx.CanNang_ChieuCao = "";
                    ttbx.Mach_NDo_HAp = "";
                    _db.SaveChanges();
                    this.uc_TTkhamBenh_Load(sender, e);
                    txtCanNang.Text = null;
                    txtMach.Text = null;
                    txtNhietDo.Text = null; txtHuyetAp.Text = null;
                    txtChieuCao.Text = null;
                    txtnhipTho.Text = null;
                }
            }

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCanNang_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
        }

        private void txtChieuCao_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
