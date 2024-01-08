using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
//using DevExpress.XtraRichEdit.API.Word;

namespace QLBV.FormThamSo
{
    public partial class Frm_bcvttKSK : DevExpress.XtraEditors.XtraForm
    {
        public Frm_bcvttKSK()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public DateTime chuyendate(object a) 
        {
            string c = Convert.ToDateTime(a).ToShortDateString();
            return Convert.ToDateTime(c);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            DateTime tungay1 = DungChung.Ham.NgayTu(tungay.DateTime);
            DateTime denngay1 =  DungChung.Ham.NgayDen(denngay.DateTime);
       
            var dsbn = (from h in _data.TamUngs
                        join h2 in _data.BenhNhans on h.MaBNhan equals h2.MaBNhan
                        where (h2.DTuong == "KSK" && h.NgayThu >= tungay1 && h.NgayThu <= denngay1)
                        select new { h2.MaBNhan, h2.TenBNhan, h2.DChi, h.SoTien, h.NgayThu, h.IDTamUng, h.QuyenHD, h.SoHD }).ToList().Select(p => new { p.MaBNhan, p.TenBNhan, p.DChi, p.SoTien, p.IDTamUng, p.QuyenHD, p.SoHD, NgayThu = new DateTime(Convert.ToInt32(Convert.ToDateTime(p.NgayThu).Year), Convert.ToInt16(Convert.ToDateTime(p.NgayThu).Month), Convert.ToInt32(Convert.ToDateTime(p.NgayThu).Day)) });
            var ketquabaocao1 = (from ket in dsbn group ket by new { ket.MaBNhan, ket.TenBNhan, ket.DChi, ket.IDTamUng, ket.NgayThu,ket.SoHD,ket.QuyenHD } into h select new { h.Key.IDTamUng, TenBnhan = h.Key.TenBNhan, DChi = h.Key.DChi, NgayThu = h.Key.NgayThu,h.Key.SoHD,h.Key.QuyenHD,SoTien = h.Sum(p => p.SoTien) }).OrderBy(p=>p.NgayThu).ToList();           
            BaoCao.rep_bangkevp re = new BaoCao.rep_bangkevp();
            re.Tungay.Value = tungay.DateTime.ToShortDateString();
            re.dv.Value = denngay.DateTime.ToShortDateString();          
            re.DataSource = ketquabaocao1;
                re.bindingdata();
                re.CreateDocument();
                frmIn hhhhh = new frmIn();
                hhhhh.prcIN.PrintingSystem = re.PrintingSystem;
                hhhhh.ShowDialog(); 
        }
        private void baocaovienphi_Load(object sender, EventArgs e)
        {
            tungay.EditValue = DateTime.Now;
            denngay.EditValue = DateTime.Now;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        
 }

}

