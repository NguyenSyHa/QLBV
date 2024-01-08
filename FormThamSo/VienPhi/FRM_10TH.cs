using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class FRM_10TH : DevExpress.XtraEditors.XtraForm
    {
        public FRM_10TH()
        {
            InitializeComponent();
        }

        private void FRM_10TH_Load(object sender, EventArgs e)
        {
            lupngaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void OK_Click(object sender, EventArgs e)
        {
            DateTime Ngaytu = DungChung.Ham.NgayTu(lupngaytu.DateTime);
            DateTime Ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            var _lbv = (from bv in _data.BenhViens select new { bv.TenBV,bv.TuyenBV,bv.MaBV}).ToList();
            var dvu = (from a in _data.DichVus
                       join nhom in _data.NhomDVs on a.IDNhom equals nhom.IDNhom
                       select new
                       {
                           a.MaDV,
                           nhom.TenNhomCT
                       }).ToList();
            var q2 = (from bn in _data.BenhNhans.Where(p => p.DTuong == ("BHYT") && p.MaKCB == DungChung.Bien.MaBV).Where(p => p.NoiTinh == 2)
                      join vp in _data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                      join vpct in _data.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                      where (vp.NgayTT <= Ngayden && vp.NgayTT >= Ngaytu)// && vp.Duyet == 1 && vpct.Duyet == 1)
                     
                      select new
                      {
                          vp.Duyet,
                          vpct.MaDV,
                          bn.MaCS,
                          MaBNhan = bn.MaBNhan,
                          bn.NoiTru,
                          vpct.ThanhTien,
                          vpct.TienChenh,
                          vpct.TienBH,
                      }).ToList();
            var q3 = (from a in q2
                      join bv in _lbv on a.MaCS equals bv.MaBV
                      join b in dvu on a.MaDV equals b.MaDV
                      where (a.Duyet == 1 || a.Duyet == 2)
                      group new { a, bv,b } by new { a.MaBNhan, bv.TenBV, bv.TuyenBV } into kq
                      select new
                      {
                          Benhvien = kq.Key.TenBV,
                          tuyen = kq.Key.TuyenBV,
                          Mabn = kq.Key.MaBNhan,
                          BHYTNgoai = kq.Where(p => p.a.NoiTru == 0).Sum(p => p.a.TienBH) - kq.Where(p => p.a.NoiTru == 0).Sum(p => p.a.TienChenh),
                          BHYTNoi = kq.Where(p => p.a.NoiTru == 1).Sum(p => p.a.TienBH) - kq.Where(p => p.a.NoiTru == 1).Sum(p => p.a.TienChenh),
                          Ngoaids = kq.Where(p => p.a.NoiTru == 0).Where(p => p.b.TenNhomCT.Contains("Vận chuyển")).Sum(p => p.a.ThanhTien),
                          Noids = kq.Where(p => p.a.NoiTru == 1).Where(p => p.b.TenNhomCT.Contains("Vận chuyển")).Sum(p => p.a.ThanhTien),
                          SLNgoai = kq.Where(p => p.a.NoiTru == 0).Select(p => p.a.MaBNhan).Count(),
                          SLNoi = kq.Where(p => p.a.NoiTru == 1).Select(p => p.a.MaBNhan).Count()
                      }).OrderBy(p => p.tuyen).ThenBy(p => p.Benhvien).ToList();
            //var q = (from bn in _data.BenhNhans.Where(p=>p.DTuong== ("BHYT")).Where(p=>p.NoiTinh==2)
            //         join bv in _data.BenhViens on bn.MaCS equals bv.MaBV
            //         join vp in _data.VienPhis on bn.MaBNhan equals vp.MaBNhan
            //         join vpct in _data.VienPhicts on vp.idVPhi equals vpct.idVPhi
            //         join dv in _data.DichVus on vpct.MaDV equals dv.MaDV
            //         join nhom in _data.NhomDVs on dv.IDNhom equals nhom.IDNhom
            //         where (vp.NgayTT <= Ngayden && vp.NgayTT >= Ngaytu)// && vp.Duyet == 1 && vpct.Duyet == 1)
            //         where (vp.Duyet == 1 || vp.Duyet == 2)
            //         group new { bv, bn, vpct, nhom } by new { bv.MaBV, bv.TenBV, bv.TuyenBV } into kq
            //         select new
            //         {
            //             Benhvien = kq.Key.TenBV,
            //             tuyen=kq.Key.TuyenBV, 
            //             Mabn=kq.Key.MaBV,
            //             BHYTNgoai = kq.Where(p => p.bn.NoiTru == 0).Sum(p => p.vpct.TienBH) - kq.Where(p => p.bn.NoiTru == 0).Sum(p => p.vpct.TienChenh),
            //             BHYTNoi = kq.Where(p => p.bn.NoiTru == 1).Sum(p => p.vpct.TienBH) - kq.Where(p => p.bn.NoiTru == 1).Sum(p => p.vpct.TienChenh),
            //             Ngoaids = kq.Where(p => p.bn.NoiTru == 0).Where(p => p.nhom.TenNhomCT.Contains("Vận chuyển")).Sum(p => p.vpct.ThanhTien),
            //             Noids = kq.Where(p => p.bn.NoiTru == 1).Where(p => p.nhom.TenNhomCT.Contains("Vận chuyển")).Sum(p => p.vpct.ThanhTien),
            //             SLNgoai = kq.Where(p => p.bn.NoiTru == 0).Select(p => p.bn.MaBNhan).Count(),
            //             SLNoi = kq.Where(p => p.bn.NoiTru == 1).Select(p => p.bn.MaBNhan).Count()
            //         }).OrderBy(p=>p.tuyen).ToList();
            if (q3.Count > 0)
            {
                frmIn frm = new frmIn();
                BaoCao.Rep_10TH rep = new BaoCao.Rep_10TH();
                rep.DataSource = q3;
                rep.tungaydenngay.Value = "Từ ngày: " + lupngaytu.DateTime.ToString().Substring(0, 10) + " đến ngày: " + lupNgayden.DateTime.ToString().Substring(0, 10);
                rep.Nguoilb.Value = DungChung.Bien.NguoiLapBieu;
                rep.Ngayden.Value = Ngayden;
                rep.Ngaytu.Value = Ngaytu;
                rep.BindingData();
                rep.CreateDocument();
              
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu");
            }
        }

        private void Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}