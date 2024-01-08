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
    public partial class frm_BCHDTaiChinh_30010 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCHDTaiChinh_30010()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BCHDTaiChinh_30010_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupNgaytu.EditValue = System.DateTime.Now.Date;
            lupngayden.EditValue = System.DateTime.Now.Date;
            lupNgaytu.Focus();           
            radio_noitru.SelectedIndex = 2;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Nội ngoại trú
            int noitru = -1;
            if (radio_noitru.SelectedIndex != null)
                noitru = radio_noitru.SelectedIndex;


            //Thời gian
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);

         
            var qtn = (from tn in data.TieuNhomDVs join n in data.NhomDVs on tn.IDNhom equals n.IDNhom select new { tn.TenRG, tn.IdTieuNhom, n.IDNhom, n.TenNhomCT });
            // var qn = data.NhomDVs.ToList();
            frmIn frm = new frmIn();
                BaoCao.rep_BCHDTaiChinh_30010 rep = new BaoCao.rep_BCHDTaiChinh_30010(tungay, denngay, noitru);
                if (String.IsNullOrWhiteSpace(txtNgayThang.Text))
                    rep.celThoiGian.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                else
                    rep.celThoiGian.Text = txtNgayThang.Text;
                rep.CreateDocument();            
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
           
          


        }

       
    }
}