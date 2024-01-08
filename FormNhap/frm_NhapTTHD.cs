using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class frm_NhapTTHD : Form
    {
        int idBN;
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public frm_NhapTTHD(int _idBN)
        {
            InitializeComponent();
            idBN = _idBN;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (idBN > 0)
            {
                _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var ttbs = _dataContext.TTboXungs.FirstOrDefault(o => o.MaBNhan == idBN);
                if (ttbs != null)
                {
                    ttbs.MaSoThue = txtMaSoThue.Text;
                    ttbs.TenNganHang = txtNganHang.Text;
                    ttbs.SoTaiKhoan = txtSoTaiKhoan.Text;
                    ttbs.TenDonVi = txtTenDonVi.Text;
                    ttbs.DiaChiNoiLV = txtDC.Text;
                    if (_dataContext.SaveChanges() > 0)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thông tin thất bại");
                    }
                }
            }
        }

        private void frm_NhapTTHD_Load(object sender, EventArgs e)
        {
            if (idBN > 0)
            {
                _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var ttbs = _dataContext.TTboXungs.FirstOrDefault(o => o.MaBNhan == idBN);
                if (ttbs != null)
                {
                    txtMaSoThue.Text = ttbs.MaSoThue;
                    txtNganHang.Text = ttbs.TenNganHang;
                    txtSoTaiKhoan.Text = ttbs.SoTaiKhoan;
                    txtTenDonVi.Text = ttbs.TenDonVi;
                    txtDC.Text = ttbs.DiaChiNoiLV;
                }
            }
        }
    }
}
