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
    public partial class frm_TTKhamBenh : DevExpress.XtraEditors.XtraForm
    {

        int mabn = 0;
        public frm_TTKhamBenh(int mabn)
        {
            InitializeComponent();
            this.mabn = mabn;
        }
        bool okthoat = true;

        public bool Ktrathoat(bool thoat)
        {
            okthoat = thoat;
            return thoat;

        }
        string macqcq = "";
        private void frm_TTKhamBenh_Load(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            uc_TTkhamBenh us = new uc_TTkhamBenh(mabn);
            us.thoat = new uc_TTkhamBenh.LuuThoa(Ktrathoat);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(us);
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = _db.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList();
            if (q.Count() > 0)
                macqcq = q.First().MaChuQuan;
        }

        private void frm_TTKhamBenh_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24009" || (!string.IsNullOrEmpty(macqcq) && macqcq == "24009") || DungChung.Bien.MaBV == "30010")
            {
                QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                string tuoi = DungChung.Ham.TuoitheoThang(_db, mabn, "12-0");

                if (tuoi.ToLower().Contains("tháng"))
                {
                    var bn = _db.TTboXungs.Where(p => p.MaBNhan == mabn).ToList();
                    if (bn.First().CanNang_ChieuCao != null)
                    {
                        string[] arrcannang = bn.First().CanNang_ChieuCao.Split(';');
                        if (arrcannang != null && arrcannang.Length > 0 && arrcannang[0] != "")
                            this.Close();
                        else
                        {
                            MessageBox.Show("Bạn phải lưu trước khi thoát!", "Thông báo", MessageBoxButtons.OK);

                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn phải lưu trước khi thoát!", "Thông báo", MessageBoxButtons.OK);

                        e.Cancel = true;
                    }
                }
            }
            
           
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}