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
    public partial class frm_MauSo : DevExpress.XtraEditors.XtraForm
    {
        public frm_MauSo()
        {
            InitializeComponent();
        }

        public frm_MauSo(string mausoXN, int idCLS)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.mausoXN = mausoXN;
            this.idCLS = idCLS;
        }

        public frm_MauSo(string mausoXN, string trangthaibp, int idCLS)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.mausoXN = mausoXN;
            this.trangthaibp = trangthaibp;
            this.idCLS = idCLS;
        }

        //public delegate void PassMauSo(string maDV);
        //public PassMauSo passMauSo;
        private string mausoXN;
        private int idCLS;
        private string trangthaibp;
        private void frm_MauSo_Load(object sender, EventArgs e)
        {
            textEdit1.Text = mausoXN;
            textEdit2.Text = trangthaibp;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qxnDom = (from cd in data.ChiDinhs.Where(p => p.IdCLS == idCLS)
                          join dv in data.DichVus on cd.MaDV equals dv.MaDV
                          join tn in data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom) on dv.IdTieuNhom equals tn.IdTieuNhom
                          select cd).ToList();
            if (qxnDom.Count > 0)
            {
                textEdit1.Visible = true;
                labelControl1.Visible = true;
            }
            else
            {
                textEdit1.Visible = false;
                labelControl1.Visible = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            ChiDinh sua = (from cd in data.ChiDinhs.Where(p => p.IdCLS == idCLS)
                           select cd).FirstOrDefault();
            CL cls = data.CLS.Where(p => p.IdCLS == idCLS).FirstOrDefault();
            if (sua != null && cls != null)
            {
                sua.Mau_Lan_MTruongXN = textEdit1.Text;
                cls.TrangThaiBP = textEdit2.Text;
                data.SaveChanges();
                this.Close();
            }
            //if (passMauSo != null)
            //{
            //    
            //    passMauSo(textEdit1.Text);
            //}
        }
    }
}