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
    public partial class frm_TTNgThanTreEm : DevExpress.XtraEditors.XtraForm
    {
        int mabn = 0;
        public frm_TTNgThanTreEm(int _mabn)
        {
            InitializeComponent();
            mabn = _mabn;
        }
        private bool KtraLuu()
        {
            if (string.IsNullOrEmpty(txtTenBo.Text) && string.IsNullOrEmpty(txtTenMe.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên Bố|Mẹ!");
                txtTenBo.Focus();
                return false;
            }
            return true;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (KtraLuu())
            {
                TTboXung sua = data.TTboXungs.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                sua.NThan = txtTenBo.Text + "," + txtTenMe.Text + ";" + txtCMT.Text + ";" + txtSDT.Text + ";" + txtDChi.Text;
                try
                {
                    data.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {

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
                MessageBox.Show("Lưu thành thành công");
                btnhuy_Click(null, null);
            }
        }
        QLBV_Database.QLBVEntities data;
        private void frm_TTNgThanTreEm_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _lTTBN = (from bn in data.BenhNhans.Where(p => p.MaBNhan == mabn)
                          join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                          select new { bn.TenBNhan, ttbx.NThan }).FirstOrDefault();
            if (_lTTBN != null && !string.IsNullOrEmpty(_lTTBN.NThan))
            {
                if (_lTTBN.NThan.Contains(";"))
                {
                    string[] arrtt = _lTTBN.NThan.Split(';');
                    if (arrtt.Length > 1 && !string.IsNullOrEmpty(arrtt[0]))
                    {
                        string[] arrttt = arrtt[0].Split(',');
                        if (arrttt.Length > 1 && !string.IsNullOrEmpty(arrttt[0]))
                        {
                            txtTenBo.Text = arrttt[0];
                            txtTenMe.Text = arrttt[1];
                        }
                    }
                    if (arrtt.Length > 2 && !string.IsNullOrEmpty(arrtt[1]))
                        txtCMT.Text = arrtt[1];
                    if (arrtt.Length > 3 && !string.IsNullOrEmpty(arrtt[2]))
                        txtSDT.Text = arrtt[2];
                    if (arrtt.Length > 3 && !string.IsNullOrEmpty(arrtt[3]))
                        txtDChi.Text = arrtt[3];
                }
                else
                {
                    if (_lTTBN.NThan.Contains(","))
                    {
                        string[] arrttt = _lTTBN.NThan.Split(',');
                        if (arrttt.Length > 1 && !string.IsNullOrEmpty(arrttt[0]))
                        {
                            txtTenBo.Text = arrttt[0];
                            txtTenMe.Text = arrttt[1];
                        }
                    }
                    else
                        txtTenBo.Text = _lTTBN.NThan;
                }
            }
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            //if (KtraLuu())
                this.Close();
        }

        private void txtTen_Leave(object sender, EventArgs e)
        {
            txtTenBo.Text = DungChung.Ham.ToFirstUpper(txtTenBo.Text.Trim());
            txtTenMe.Text = DungChung.Ham.ToFirstUpper(txtTenMe.Text.Trim());
        }

        private void txtDChi_Leave(object sender, EventArgs e)
        {
            txtDChi.Text = DungChung.Ham.ToFirstUpper(txtDChi.Text.Trim());
        }

        private void frm_TTNgThanTreEm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            //if (KtraLuu())
            //{
            //    //simpleButton1_Click(null, null);
            //    e.Cancel = false;
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
        }
    }
}