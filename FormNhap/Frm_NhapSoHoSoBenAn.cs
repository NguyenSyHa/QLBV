using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class Frm_NhapSoHoSoBenAn : DevExpress.XtraEditors.XtraForm
    {
        public Frm_NhapSoHoSoBenAn()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private int Mabn;
        public Frm_NhapSoHoSoBenAn(int _Mabn)
        {
            InitializeComponent();
            this.Mabn = _Mabn;

        }
        private void Frm_NhapSoHoSoBenAn_Load(object sender, EventArgs e)
        {
            lblThongTinBenhNhan.Text = data.BenhNhans.Where(p => p.MaBNhan == Mabn).First().TenBNhan;
            txtSoHSBA.Text = data.BenhNhans.Where(p => p.MaBNhan == Mabn).First().SoHSBA == null ? "" : data.BenhNhans.Where(p => p.MaBNhan == Mabn).First().SoHSBA;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (txtSoHSBA.Text == null || txtSoHSBA.Text == "")
            {
              
                if (NhapSoHS(Mabn) > 0)
                {
                    XtraMessageBox.Show("Đã thêm mới thành công \n Số hồ sơ bệnh án cho bệnh nhân: " + lblThongTinBenhNhan.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (NhapSoHS(Mabn) > 0)
                {
                    XtraMessageBox.Show("Đã sửa thành công \n Số hồ sơ bệnh án cho bệnh nhân: " + lblThongTinBenhNhan.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }
        private int NhapSoHS(int MaBn)
        {
            int Value = 0;
            BenhNhan bn = data.BenhNhans.Where(p => p.MaBNhan == Mabn).Single();
            bn.SoHSBA = txtSoHSBA.Text;
            if (data.SaveChanges() > 0)
            {
                Value = 1;
            }

            return Value;
        }
    }
}