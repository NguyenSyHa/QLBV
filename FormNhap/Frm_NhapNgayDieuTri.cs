using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBV_Database;

namespace QLBV.FormNhap
{
    public partial class Frm_NhapNgayDieuTri : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int Mabn = 0;
        int Makp = 0;
        public Frm_NhapNgayDieuTri()
        {
            InitializeComponent();
        }
        public Frm_NhapNgayDieuTri(int mabn, int makp)
        {
            InitializeComponent();
            Mabn = mabn;
            Makp = makp;
        }
        public Frm_NhapNgayDieuTri(int mabn)
        {
            InitializeComponent();
            Mabn = mabn;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            var rv = DaTaContext.RaViens.Where(p => p.MaBNhan == Mabn).ToList();
            if (rv.Count > 0)
            {
                rv.First().SoNgaydt = Convert.ToInt32(txtSoNgayDT.Value);
                if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "27194")
                    rv.First().SoNgayLT = Convert.ToInt32(txtSoNgayLT.Value);
            }
            if (DaTaContext.SaveChanges() > 0)
            {
                MessageBox.Show("Lưu ngày thành công!", "Thông báo");
                this.Close();
            }
            else
            {
                MessageBox.Show("Lưu ngày không thành công!", "Thông báo");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {

        }

        private void Frm_NhapNgayDieuTri_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "27194")
            {
                label2.Visible = true;
                txtSoNgayLT.Visible = true;
            }

            var rv = DaTaContext.RaViens.Where(p => p.MaBNhan == Mabn).ToList();
            if (rv.Count > 0)
            {
                txtSoNgayDT.Value = rv.First().SoNgaydt != null ? (int)rv.First().SoNgaydt : 1;
                txtSoNgayLT.Value = rv.First().SoNgayLT != null ? (int)rv.First().SoNgayLT : 1;
            }
        }
    }
}