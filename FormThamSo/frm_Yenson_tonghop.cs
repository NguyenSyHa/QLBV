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
    public partial class frm_Yenson_tonghop : DevExpress.XtraEditors.XtraForm
    {
        public frm_Yenson_tonghop()
        {
            InitializeComponent();
        }

        private void frm_Yenson_tonghop_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
        }

        private void sbtHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sbtTao_Click(object sender, EventArgs e)
        {
            frmIn frm = new frmIn();
            BaoCao.repYenSon_tonghop rep = new BaoCao.repYenSon_tonghop();
            rep.Ngayden.Value = lupNgayden.DateTime.ToString();
            rep.Ngaytu.Value = lupNgaytu.DateTime.ToString();
            //rep.BindingData();
            rep.CreateDocument();
            //rep.DataMember = "Table";
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}