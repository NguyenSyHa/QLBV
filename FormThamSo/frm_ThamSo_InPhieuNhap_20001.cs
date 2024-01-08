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
    public partial class frm_ThamSo_InPhieuNhap_20001 : DevExpress.XtraEditors.XtraForm
    {
        public delegate void PassCB(string maCB, string tenCB);
        public PassCB passCB;
        public frm_ThamSo_InPhieuNhap_20001()
        {
            InitializeComponent();
            txtCanBo.Visible = false;
          
        }
        int Kieu = 0; // Kiểu 1 dành cho ID 166 , Kiểu 0 dành cho báo cáo khác;
        public frm_ThamSo_InPhieuNhap_20001(string Text, int Kieu)
        {
          
                InitializeComponent();
                this.Text = Text;
                this.Kieu = Kieu;
                lupTV4.Visible = false;
        }
        private void frm_ThamSo_InPhieuNhap_20001_Load(object sender, EventArgs e)
        {        
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var qcb = (from cb in data.CanBoes
                           join kp in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.TenKP == "Khoa Dược") on cb.MaKP equals kp.MaKP
                           select cb).OrderBy(p => p.TenCB).ToList();
                lupTV4.Properties.DataSource = qcb;
                lupTV4.EditValue = lupTV4.Properties.GetKeyValueByDisplayText("Nguyễn Thị Phương");//Hoàng Minh Doãn           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string macb = "";
            string tencb = "";
            if (Kieu == 1)
            {
                if (txtCanBo.Text != "")
                {
                    macb = "0";
                    tencb = txtCanBo.Text;
                }
                if (passCB != null)
                {
                    this.Close();
                    passCB(macb, tencb);
                } 
            }
            else
            {
                if (lupTV4.EditValue != null)
                {
                    macb = lupTV4.EditValue.ToString();
                    tencb = lupTV4.Text;
                }
                if (passCB != null)
                {
                    this.Close();
                    passCB(macb, tencb);
                }
            }
          
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            passCB("", "");
            
        }
    }
}