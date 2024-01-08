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
    public partial class frm_PhieuTongXuat_12122 : DevExpress.XtraEditors.XtraForm
    {
        public frm_PhieuTongXuat_12122()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void frm_PhieuTongXuat_12122_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> lkp = data.KPhongs.Where(p => p.PLoai == "Khoa dược").ToList();
            lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKho.Properties.DataSource = lkp;
            lupKho.Properties.DisplayMember = "TenKP";
            lupKho.Properties.ValueMember = "MaKP";

            List<KPhong> lBoPhan = data.KPhongs.Where(p => p.TrongBV == 1).ToList();
            lBoPhan.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupBoPhan.Properties.DataSource = lBoPhan;
            lupBoPhan.Properties.DisplayMember = "TenKP";
            lupBoPhan.Properties.ValueMember = "MaKP";

            lupKho.EditValue = lupKho.Properties.GetKeyValueByDisplayText("Tất cả");
            lupBoPhan.EditValue = lupBoPhan.Properties.GetKeyValueByDisplayText("Tất cả");
          
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int maKP = 0;
            if (lupKho.EditValue != null)
                maKP = Convert.ToInt32(lupKho.EditValue);           
            int maBPhan = 0;
            if (lupBoPhan.Enabled == true && lupBoPhan.EditValue != null)
                maBPhan = Convert.ToInt32(lupBoPhan.EditValue);

            var qNhapd = (from nd in data.NhapDs.Where(p => p.PLoai == 2).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => maKP == 0 || p.MaKP == maKP).Where(p => maBPhan == 0 || p.MaKPnx == maBPhan)
                          join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          select new
                          {
                              nd.IDNhap,                             
                              ndct.MaDV,
                              ndct.DonGia,
                              ndct.DonVi,
                              ndct.SoLuongX,
                              ndct.ThanhTienX,
                          }).ToList();

            var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 1)
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new
                       {
                           dv.TenDV,
                           tn.TenTN,
                           dv.MaDV
                       }).ToList();


            var q1 = (from dt in qNhapd
                      join dv in qdv on dt.MaDV equals dv.MaDV
                      group new { dt, dv } by new { dv.TenTN, dv.TenDV, dt.DonVi, dt.DonGia, dt.MaDV} into kq
                      select new
                      {
                          kq.Key.TenTN,
                          kq.Key.TenDV,
                          kq.Key.DonVi,
                          kq.Key.DonGia,
                          SoLuong = kq.Sum(p=>p.dt.SoLuongX),
                          ThanhTien  = kq.Sum(p=>p.dt.ThanhTienX)
                      }).Where(p => p.SoLuong != 0 ).ToList();

            
                BaoCao.rep_PhieuTongXuat_12122 rep = new BaoCao.rep_PhieuTongXuat_12122(ckHT.Checked);
                frmIn frm = new frmIn();
                rep.DataSource = ckHT.Checked ? q1.OrderBy(p => p.TenTN).OrderBy(P => P.TenDV).ToList() : q1.OrderBy(P => P.TenDV).ToList();
                rep.celCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                rep.celCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                if(lupKho.Text != "Tất cả")
                rep.celKhoXuat.Text = lupKho.Text ;
                if(lupBoPhan.Text != "Tất cả")
                rep.celKhoaNhan.Text = lupBoPhan.Text ;
                rep.celTieuDe.Text = "PHIẾU TỔNG XUẤT";
            if(String.IsNullOrEmpty(txtNgayThangHT.Text))
                rep.celNgayThang.Text = "Từ ngày " + lupTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupDenNgay.DateTime.ToString("dd/MM/yyyy");
            else
                rep.celNgayThang.Text = txtNgayThangHT.Text;
                rep.Bindingdata();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            
        }
    }
}