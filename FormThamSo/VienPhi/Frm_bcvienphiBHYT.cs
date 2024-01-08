using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Collections;

namespace QLBV.FormThamSo
{
    public partial class Frm_bcvienphiBHYT : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public Frm_bcvienphiBHYT()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime a = DungChung.Ham.NgayTu(tungay.DateTime);
            DateTime b = DungChung.Ham.NgayDen(denngay.DateTime);
            var benhnhan = (from bn in _data.BenhNhans.Where(p => p.DTuong == "BHYT") join h in _data.TamUngs.Where(p => p.NgayThu >= a && p.NgayThu <= b) on bn.MaBNhan equals h.MaBNhan select new
            { bn.DChi, bn.TenBNhan,
                bn.NoiTru, h.MaBNhan, 
                h.SoTien, h.SoHD, 
                h.QuyenHD,
                h.NgayThu }).ToList().Select(p=> new  {p.NoiTru,
                    NgayThu = Convert.ToDateTime(p.NgayThu).ToShortDateString(),
                    p.TenBNhan,
                    p.DChi, p.SoHD, 
                    p.QuyenHD, p.SoTien,
                    p.MaBNhan});

            var ketqua1 = (from k in benhnhan group k by new { k.MaBNhan, k.TenBNhan, k.NgayThu, k.SoHD, k.QuyenHD } into h 
                           select new { TenBNhan = h.Max(p => p.TenBNhan),
                               DChi = h.Max(p => p.DChi), 
                               SoTien = h.Sum(p => p.SoTien),
                               h.Key.SoHD, h.Key.QuyenHD,
                               NoiTru = h.Max(p => p.NoiTru)
                               ,h.Key.NgayThu,
                           h.Key.MaBNhan}).OrderBy(p=>p.NgayThu);
            var ketqua2 = from vien in ketqua1 join v in _data.VienPhis on vien.MaBNhan equals v.MaBNhan select vien;
            BaoCao.rep_baocaotienthucungchicha bao = new BaoCao.rep_baocaotienthucungchicha();
            bao.parameter1.Value = "Từ ngày: " + tungay.DateTime.ToShortDateString()+ "   Đến ngày: " + denngay.DateTime.ToShortDateString();
            if (radootatca.Checked == true)
            {
                bao.DataSource = ketqua2.ToList().Where(p=>p.SoTien!=0);
            }
            else if (ngoaitru.Checked == true)
            {
                bao.DataSource = ketqua2.Where(p => p.NoiTru == 0).ToList().Where(p => p.SoTien != 0);
            }
            else {

                bao.DataSource = ketqua2.Where(p => p.NoiTru == 1).ToList().Where(p => p.SoTien != 0);
            
            }

            var tet = new {h="the",h1="100"};
            bao.binhdin();
            bao.CreateDocument();
            frmIn hhhhh = new frmIn();
            hhhhh.prcIN.PrintingSystem = bao.PrintingSystem;
            hhhhh.ShowDialog();
        }

        private void baocaothutienbh_Load(object sender, EventArgs e)
        {
            tungay.DateTime = DateTime.Now;
            denngay.DateTime = DateTime.Now;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}