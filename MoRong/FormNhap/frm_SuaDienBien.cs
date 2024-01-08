using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.MoRong.FormNhap
{
    public partial class frm_SuaDienBien : Form
    {
        public delegate void DelegateSuaDienBien(DienBien _dienBien);
        DelegateSuaDienBien dlg;
        DienBien dienBien;

        public frm_SuaDienBien(DienBien _dienBien, DelegateSuaDienBien _dlg)
        {
            InitializeComponent();
            this.dienBien = _dienBien;
            this.dlg = _dlg;
        }

        private void frm_SuaDienBien_Load(object sender, EventArgs e)
        {
            txtDienBien.Text = dienBien.DienBien1;
            txtYLenh.Text = dienBien.YLenh;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dienBienUpdate = dataContext.DienBiens.FirstOrDefault(o => o.ID == dienBien.ID);
            dienBienUpdate.DienBien1 = txtDienBien.Text;
            dienBienUpdate.YLenh = txtYLenh.Text;
            dataContext.SaveChanges();
            if (dlg != null)
            {
                dlg(dienBienUpdate);
            }
        }
    }
}
