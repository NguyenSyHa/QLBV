using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QLBV.BaoCao;

namespace QLBV.FormThamSo
{
    public partial class frm_Congtacchuyentuyen : Form
    {
        public frm_Congtacchuyentuyen()
        {
            InitializeComponent();
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }      
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime dttungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime dtdenngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            frmIn frm = new frmIn();

            int ckNgay = 0;// bệnh nhân chuyển đến tìm theo ngày : 0: Cả hai, 1: ngày vào viện; 2: ngày ra viện
            if (ckNgayVaoVien.Checked && ckNgayRaVien.Checked)
                ckNgay = 0;
            else if (ckNgayVaoVien.Checked)
                ckNgay = 1;
            else if (ckNgayRaVien.Checked)
                ckNgay = 2;
            else
                ckNgay = -1;
            rep_ChuyenTuyen rep = new rep_ChuyenTuyen(dttungay, dtdenngay, ckNgay);
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }      
        private void frm_Congtacchuyentuyen_Load(object sender, EventArgs e)
        {           
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
            ckNgayRaVien.Checked = true;
            ckNgayVaoVien.Checked = true;
        }
    }
}
