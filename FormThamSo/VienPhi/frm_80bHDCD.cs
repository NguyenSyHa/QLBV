using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_80bHDCD : DevExpress.XtraEditors.XtraForm
    {
        public frm_80bHDCD()
        {
            InitializeComponent();
        }

        private void frm_80bHD_Load(object sender, EventArgs e)
        {
            lupNgayden.DateTime = System.DateTime.Now;
            lupNgaytu.DateTime = System.DateTime.Now;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            BaoCao.rep_80bHDCD rep = new BaoCao.rep_80bHDCD();
            rep.ngayden.Value = lupNgayden.DateTime;
            rep.Ngaytu.Value = lupNgaytu.DateTime;
            rep.CreateDocument();
            frmIn frm = new frmIn();
            rep.DataMember = "Table";
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}