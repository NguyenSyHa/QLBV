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
    public partial class frm_PhiVCDuocMien : DevExpress.XtraEditors.XtraForm
    {
        public frm_PhiVCDuocMien()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_PhiVCDuocMien_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupNgaytu.EditValue = System.DateTime.Now.Date;
            lupngayden.EditValue = System.DateTime.Now.Date;
            lupNgaytu.Focus();           
            radio_noitru.SelectedIndex = 3;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Nội ngoại trú
            int noitru = 3;
            if (radio_noitru.SelectedIndex != null)
                noitru = radio_noitru.SelectedIndex;

           
            //Thời gian
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var qbn = (from bn in data.BenhNhans.Where(p => noitru == 3 || (noitru == 1 && p.NoiTru == 1) || (noitru == 0 && (p.NoiTru == 0 && p.DTNT == false)) || (noitru == 2 && (p.NoiTru == 0 && p.DTNT == true)))
                       join dt in data.DTBNs.Where(p => p.DTBN1 == "BHYT") on bn.IDDTBN equals dt.IDDTBN
                       join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                         join kp in data.KPhongs on rv.MaKP equals kp.MaKP
                       select new
                       {
                           bn.MaBNhan,
                           bn.TenBNhan,
                           bn.NamSinh,
                           bn.SThe,
                           bn.DChi,
                           rv.NgayVao,
                           rv.NgayRa,
                           rv.SoNgaydt,
                           rv.MaKP,
                           kp.TenKP
                       }).ToList();
            var vp2 = (from vp1 in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                      join vpct in data.VienPhicts.Where(p => p.ThanhTien == p.TienBH)//.Where(p=>p.Mien > 0) 
                       on vp1.idVPhi equals vpct.idVPhi
                      select new
                      {
                          vp1.MaBNhan,
                          vpct.MaDV,
                          vpct.DonGia,
                          vpct.SoLuong,
                          vpct.ThanhTien,
                          vpct.TienBH,
                          vpct.TienBN,
                          TienMien = vpct.TienBH
                      }).ToList();
            var qvp = (from bn in qbn                      
                      join vp1 in vp2 on bn.MaBNhan equals vp1.MaBNhan
                      select new {
                      bn.TenBNhan, 
                      bn.NamSinh,
                      bn.SThe,
                      bn.DChi,
                      bn.NgayVao,
                      bn.NgayRa,
                      bn.SoNgaydt,
                      bn.MaKP,
                      bn.TenKP,
                      vp1.MaDV, 
                      vp1.DonGia,
                      vp1.SoLuong,
                      vp1.ThanhTien,
                      vp1.TienBH,
                      vp1.TienBN,
                      TienMien = vp1.TienBH//vpct.ThanhTien - vpct.TienBN - vpct.TienBH
                      }).ToList();
            var qdv = (from dv in data.DichVus
                       join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 12) on dv.IdTieuNhom equals tn.IdTieuNhom // tiền vận chuyển
                       select new {dv.MaDV, dv.TenDV }).ToList();
            var q1 = (from vp in qvp
                      join dv in qdv on vp.MaDV equals dv.MaDV
                      group vp by new
                      {
                          vp.TenBNhan,
                          vp.NamSinh,
                          vp.SThe,
                          vp.DChi,
                          vp.TenKP,
                          vp.NgayVao,
                          vp.NgayRa,
                          vp.SoNgaydt,
                          vp.MaKP,
                          vp.MaDV,
                          vp.DonGia,
                          dv.TenDV

                      } into kq
                      select new
                      {
                          kq.Key.TenBNhan,
                          kq.Key.NamSinh,
                          kq.Key.SThe,
                          kq.Key.DChi,
                          kq.Key.NgayVao,
                          kq.Key.NgayRa,
                          kq.Key.SoNgaydt,
                          kq.Key.MaKP,
                          kq.Key.TenKP,
                          kq.Key.MaDV,
                          kq.Key.TenDV,
                          kq.Key.DonGia,
                          SoLuong = kq.Sum(p => p.SoLuong),
                          TienMien = kq.Sum(p => p.TienMien)
                      }).OrderBy(p=>p.NgayRa).ThenBy(p=>p.TenBNhan).ToList();


            BaoCao.rpt_PhiVCDuocMien rep = new BaoCao.rpt_PhiVCDuocMien();
            frmIn frm = new frmIn();

            rep.DataSource = q1;
            
            if (String.IsNullOrEmpty(txtTieude.Text))
            {
                rep.celTit.Text = "XĂNG XE BỆNH NHÂN BHYT ĐƯỢC MIỄN TỪ NGÀY " + tungay.ToString("dd/MM/yyyy") + " ĐẾN NGÀY " +denngay.ToString("dd/MM/yyyy");
            }
            else
            { rep.celTit.Text = txtTieude.Text.ToUpper(); }
            rep.DataBinding();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}