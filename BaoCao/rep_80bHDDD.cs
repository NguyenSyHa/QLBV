using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_80bHDDD : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_80bHDDD()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void xrLabel7_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime nd = DungChung.Ham.NgayDen(Convert.ToDateTime(ngayden.Value));
            DateTime nt = DungChung.Ham.NgayTu(Convert.ToDateTime(Ngaytu.Value));
            //var q = (from bn in _Data.BenhNhans.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 1)
            //         join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
            //         join vpct in _Data.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
            //         join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
            //         join nhom in _Data.NhomDVs on dv.IDNhom equals nhom.IDNhom
            //         join vv in _Data.VaoViens on bn.MaBNhan equals vv.MaBNhan
            //         join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
            //         where (vp.NgayTT>=nt&&vp.NgayTT<=nd)
            //         group new { bn, vpct, rv, nhom,vv } by new { vp.Duyet,bn.NoiTinh, bn.MaBNhan,rv.SoNgaydt} into kq
            //         select new
            //         {
            //             Noitinh = kq.Key.NoiTinh,
            //             Mabn=kq.Key.MaBNhan,
            //             duyet=kq.Key.Duyet,
            //             soluot = kq.Select(p => p.bn.MaBNhan).Count(),
            //             songay = kq.Key.SoNgaydt,
            //             Tongcong = kq.Sum(p => p.vpct.ThanhTien),
            //             TongcongCL = kq.Sum(p => p.vpct.ThanhTien) - kq.Sum(p => p.vpct.TienChenh) - kq.Sum(p => p.vpct.TienChenhBN),
            //             Xetnghiem = kq.Where(p => p.nhom.TenNhom.Contains("xét nghiệm")).Sum(p => p.vpct.ThanhTien),
            //             XetnghiemCL = kq.Where(p => p.nhom.TenNhom.Contains("xét nghiệm")).Sum(p => p.vpct.ThanhTien) - kq.Where(p => p.nhom.TenNhom.Contains("xét nghiệm")).Sum(p => p.vpct.TienChenh) - kq.Where(p => p.nhom.TenNhom.Contains("xét nghiệm")).Sum(p => p.vpct.TienChenhBN),
            //             CDHA = kq.Where(p => p.nhom.TenNhom.Contains("CĐHA")).Sum(p => p.vpct.ThanhTien),
            //             CDHACL = kq.Where(p => p.nhom.TenNhom.Contains("CĐHA")).Sum(p => p.vpct.ThanhTien) - kq.Where(p => p.nhom.TenNhom.Contains("CĐHA")).Sum(p => p.vpct.TienChenh) - kq.Where(p => p.nhom.TenNhom.Contains("CĐHA")).Sum(p => p.vpct.TienChenhBN),
            //             Thuoc = kq.Where(p => p.nhom.TenNhom.Contains("Thuốc")).Sum(p => p.vpct.ThanhTien),
            //             ThuocCL = kq.Where(p => p.nhom.TenNhom.Contains("Thuốc")).Sum(p => p.vpct.ThanhTien) - kq.Where(p => p.nhom.TenNhom.Contains("Thuốc")).Sum(p => p.vpct.TienChenh) - kq.Where(p => p.nhom.TenNhom.Contains("Thuốc")).Sum(p => p.vpct.TienChenhBN),
            //             Mau = kq.Where(p => p.nhom.TenNhom.Contains("máu")).Sum(p => p.vpct.ThanhTien),
            //             MauCL = kq.Where(p => p.nhom.TenNhom.Contains("máu")).Sum(p => p.vpct.ThanhTien) - kq.Where(p => p.nhom.TenNhom.Contains("máu")).Sum(p => p.vpct.TienChenh) - kq.Where(p => p.nhom.TenNhom.Contains("máu")).Sum(p => p.vpct.TienChenhBN),
            //             TTPT = kq.Where(p => p.nhom.TenNhom.Contains("thủ thuật")).Sum(p => p.vpct.ThanhTien),
            //             TTPTCL = kq.Where(p => p.nhom.TenNhom.Contains("thủ thuật")).Sum(p => p.vpct.ThanhTien) - kq.Where(p => p.nhom.TenNhom.Contains("thủ thuật")).Sum(p => p.vpct.TienChenh) - kq.Where(p => p.nhom.TenNhom.Contains("thủ thuật")).Sum(p => p.vpct.TienChenhBN),
            //             VTTH = kq.Where(p => p.nhom.TenNhom.Contains("vật tư")).Sum(p => p.vpct.ThanhTien),
            //             VTTHCL = kq.Where(p => p.nhom.TenNhom.Contains("vật tư")).Sum(p => p.vpct.ThanhTien) - kq.Where(p => p.nhom.TenNhom.Contains("vật tư")).Sum(p => p.vpct.TienChenh) - kq.Where(p => p.nhom.TenNhom.Contains("vật tư")).Sum(p => p.vpct.TienChenhBN),
            //             VTTT = kq.Where(p => p.nhom.TenNhom.Contains("VTTT")).Sum(p => p.vpct.ThanhTien),
            //             VTTTCL = kq.Where(p => p.nhom.TenNhom.Contains("VTTT")).Sum(p => p.vpct.ThanhTien) - kq.Where(p => p.nhom.TenNhom.Contains("VTTT")).Sum(p => p.vpct.TienChenh) - kq.Where(p => p.nhom.TenNhom.Contains("VTTT")).Sum(p => p.vpct.TienChenhBN),
            //             DVKTC = kq.Where(p => p.nhom.TenNhom.Contains("kỹ thuật cao")).Sum(p => p.vpct.ThanhTien),
            //             DVKTCCL = kq.Where(p => p.nhom.TenNhom.Contains("kỹ thuật cao")).Sum(p => p.vpct.ThanhTien) - kq.Where(p => p.nhom.TenNhom.Contains("kỹ thuật cao")).Sum(p => p.vpct.TienChenh) - kq.Where(p => p.nhom.TenNhom.Contains("kỹ thuật cao")).Sum(p => p.vpct.TienChenhBN),
            //             TTG = kq.Where(p => p.nhom.TenNhom.Contains("Thải ghép")).Sum(p => p.vpct.ThanhTien),
            //             TTGCL = kq.Where(p => p.nhom.TenNhom.Contains("Thải ghép")).Sum(p => p.vpct.ThanhTien) - kq.Where(p => p.nhom.TenNhom.Contains("Thải ghép")).Sum(p => p.vpct.TienChenh) - kq.Where(p => p.nhom.TenNhom.Contains("Thải ghép")).Sum(p => p.vpct.TienChenhBN),
            //             tiengiuong = kq.Where(p => p.nhom.TenNhom.Contains("Ngày giường")).Sum(p => p.vpct.ThanhTien),
            //             tiengiuongCL = kq.Where(p => p.nhom.TenNhom.Contains("Ngày giường")).Sum(p => p.vpct.ThanhTien) - kq.Where(p => p.nhom.TenNhom.Contains("Ngày giường")).Sum(p => p.vpct.TienChenh) - kq.Where(p => p.nhom.TenNhom.Contains("Ngày giường")).Sum(p => p.vpct.TienChenhBN),
            //             Vanchuyen = kq.Where(p => p.nhom.TenNhom.ToLower().Contains("vận chuyển")).Sum(p => p.vpct.ThanhTien),
            //             VanchuyenCL = kq.Where(p => p.nhom.TenNhom.ToLower().Contains("vận chuyển")).Sum(p => p.vpct.ThanhTien) - kq.Where(p => p.nhom.TenNhom.ToLower().Contains("vận chuyển")).Sum(p => p.vpct.TienChenh) - kq.Where(p => p.nhom.TenNhom.ToLower().Contains("vận chuyển")).Sum(p => p.vpct.TienChenhBN),
            //             Nguoibenh = kq.Sum(p => p.vpct.TienBN),
            //             NguoibenhCL= kq.Sum(p => p.vpct.TienBN)- kq.Sum(p => p.vpct.TienChenhBN),
            //             BHYT = kq.Sum(p => p.vpct.TienBH),
            //             BHYTCL=kq.Sum(p => p.vpct.TienBH)-kq.Sum(p => p.vpct.TienChenh)
            //         }).ToList();

            var q1 = (from bn in _Data.BenhNhans.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 1)
                      join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                      join vpct in _Data.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                      join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                      join nhom in _Data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                      join vv in _Data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                      join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                      where (vp.NgayTT >= nt && vp.NgayTT <= nd)
                      //group new { bn, vpct, rv, nhom, vv } by new { vp.Duyet, bn.NoiTinh, bn.MaBNhan, rv.SoNgaydt } into kq
                      select new
                      {
                          vp.Duyet, bn.NoiTinh, bn.MaBNhan, rv.SoNgaydt,
                          vpct.ThanhTien,vpct.TienChenhBN, vpct.TienBH, vpct.TienBN, vpct.TienChenh, nhom.TenNhomCT
                      }).ToList();
            var q= (from b in q1 group new {b} by new {b.Duyet, b.NoiTinh, b.MaBNhan, b.SoNgaydt} into kq
                     select new
                     {
                         Noitinh = kq.Key.NoiTinh,
                         Mabn=kq.Key.MaBNhan,
                         duyet=kq.Key.Duyet,
                         soluot = kq.Select(p => p.b.MaBNhan).Distinct().Count(),
                         songay = kq.Key.SoNgaydt,
                         Tongcong = kq.Sum(p => p.b.ThanhTien),
                         TongcongCL = kq.Sum(p => p.b.ThanhTien) - kq.Sum(p => p.b.TienChenh) - kq.Sum(p => p.b.TienChenhBN),
                         Xetnghiem = kq.Where(p => p.b.TenNhomCT== ("Xét nghiệm")).Sum(p => p.b.ThanhTien),
                         XetnghiemCL = kq.Where(p => p.b.TenNhomCT== ("Xét nghiệm")).Sum(p => p.b.ThanhTien) - kq.Where(p => p.b.TenNhomCT== ("Xét nghiệm")).Sum(p => p.b.TienChenh) - kq.Where(p => p.b.TenNhomCT== ("Xét nghiệm")).Sum(p => p.b.TienChenhBN),
                         CDHA = kq.Where(p => p.b.TenNhomCT== ("Chẩn đoán hình ảnh")).Sum(p => p.b.ThanhTien),
                         CDHACL = kq.Where(p => p.b.TenNhomCT== ("Chẩn đoán hình ảnh")).Sum(p => p.b.ThanhTien) - kq.Where(p => p.b.TenNhomCT== ("Chẩn đoán hình ảnh")).Sum(p => p.b.TienChenh) - kq.Where(p => p.b.TenNhomCT== ("Chẩn đoán hình ảnh")).Sum(p => p.b.TienChenhBN),
                         Thuoc = kq.Where(p => p.b.TenNhomCT== ( "Thuốc trong danh mục BHYT" )).Sum(p => p.b.ThanhTien),
                         ThuocCL = kq.Where(p => p.b.TenNhomCT== ( "Thuốc trong danh mục BHYT" )).Sum(p => p.b.ThanhTien) - kq.Where(p => p.b.TenNhomCT== ( "Thuốc trong danh mục BHYT" )).Sum(p => p.b.TienChenh) - kq.Where(p => p.b.TenNhomCT== ( "Thuốc trong danh mục BHYT" )).Sum(p => p.b.TienChenhBN),
                         Mau = kq.Where(p => p.b.TenNhomCT== ("Máu và chế phẩm của máu")).Sum(p => p.b.ThanhTien),
                         MauCL = kq.Where(p => p.b.TenNhomCT== ("Máu và chế phẩm của máu")).Sum(p => p.b.ThanhTien) - kq.Where(p => p.b.TenNhomCT== ("Máu và chế phẩm của máu")).Sum(p => p.b.TienChenh) - kq.Where(p => p.b.TenNhomCT== ("Máu và chế phẩm của máu")).Sum(p => p.b.TienChenhBN),
                         TTPT = kq.Where(p => p.b.TenNhomCT== ("Thủ thuật, phẫu thuật")).Sum(p => p.b.ThanhTien),
                         TTPTCL = kq.Where(p => p.b.TenNhomCT== ("Thủ thuật, phẫu thuật")).Sum(p => p.b.ThanhTien) - kq.Where(p => p.b.TenNhomCT== ("Thủ thuật, phẫu thuật")).Sum(p => p.b.TienChenh) - kq.Where(p => p.b.TenNhomCT== ("Thủ thuật, phẫu thuật")).Sum(p => p.b.TienChenhBN),
                         VTTH = kq.Where(p => p.b.TenNhomCT== ("Vật tư y tế trong danh mục BHYT")).Sum(p => p.b.ThanhTien),
                         VTTHCL = kq.Where(p => p.b.TenNhomCT== ("Vật tư y tế trong danh mục BHYT")).Sum(p => p.b.ThanhTien) - kq.Where(p => p.b.TenNhomCT== ("Vật tư y tế trong danh mục BHYT")).Sum(p => p.b.TienChenh) - kq.Where(p => p.b.TenNhomCT== ("Vật tư y tế trong danh mục BHYT")).Sum(p => p.b.TienChenhBN),
                         VTTT = kq.Where(p => p.b.TenNhomCT== ("VTYT thanh toán theo tỷ lệ")).Sum(p => p.b.ThanhTien),
                         VTTTCL = kq.Where(p => p.b.TenNhomCT== ("VTYT thanh toán theo tỷ lệ")).Sum(p => p.b.ThanhTien) - kq.Where(p => p.b.TenNhomCT== ("VTYT thanh toán theo tỷ lệ")).Sum(p => p.b.TienChenh) - kq.Where(p => p.b.TenNhomCT== ("VTYT thanh toán theo tỷ lệ")).Sum(p => p.b.TienChenhBN),
                         DVKTC = kq.Where(p => p.b.TenNhomCT== ("DVKT thanh toán theo tỷ lệ")).Sum(p => p.b.ThanhTien),
                         DVKTCCL = kq.Where(p => p.b.TenNhomCT== ("DVKT thanh toán theo tỷ lệ")).Sum(p => p.b.ThanhTien) - kq.Where(p => p.b.TenNhomCT== ("DVKT thanh toán theo tỷ lệ")).Sum(p => p.b.TienChenh) - kq.Where(p => p.b.TenNhomCT== ("DVKT thanh toán theo tỷ lệ")).Sum(p => p.b.TienChenhBN),
                         TTG = kq.Where(p => p.b.TenNhomCT== ("Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")).Sum(p => p.b.ThanhTien),
                         TTGCL = kq.Where(p => p.b.TenNhomCT== ("Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")).Sum(p => p.b.ThanhTien) - kq.Where(p => p.b.TenNhomCT== ("Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")).Sum(p => p.b.TienChenh) - kq.Where(p => p.b.TenNhomCT== ("Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")).Sum(p => p.b.TienChenhBN),
                         tiengiuong = kq.Where(p => p.b.TenNhomCT== ("Giường điều trị nội trú")).Sum(p => p.b.ThanhTien),
                         tiengiuongCL = kq.Where(p => p.b.TenNhomCT== ("Giường điều trị nội trú")).Sum(p => p.b.ThanhTien) - kq.Where(p => p.b.TenNhomCT== ("Giường điều trị nội trú")).Sum(p => p.b.TienChenh) - kq.Where(p => p.b.TenNhomCT== ("Giường điều trị nội trú")).Sum(p => p.b.TienChenhBN),
                         Vanchuyen = kq.Where(p => p.b.TenNhomCT.ToLower()== ("Vận chuyển")).Sum(p => p.b.ThanhTien),
                         VanchuyenCL = kq.Where(p => p.b.TenNhomCT.ToLower()== ("Vận chuyển")).Sum(p => p.b.ThanhTien) - kq.Where(p => p.b.TenNhomCT.ToLower()== ("Vận chuyển")).Sum(p => p.b.TienChenh) - kq.Where(p => p.b.TenNhomCT.ToLower()== ("Vận chuyển")).Sum(p => p.b.TienChenhBN),
                         Nguoibenh = kq.Sum(p => p.b.TienBN),
                         NguoibenhCL= kq.Sum(p => p.b.TienBN)- kq.Sum(p => p.b.TienChenhBN),
                         BHYT = kq.Sum(p => p.b.TienBH),
                         BHYTCL=kq.Sum(p => p.b.TienBH)-kq.Sum(p => p.b.TienChenh)
                     }).ToList();
            //string d = q.First().soluot.ToString();
            if (q.Where(p => p.duyet == 1).ToList().Count > 0)
            {
                double a = 0;
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).First().soluot != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Select(p=>p.Mabn).Count());
                    xrTableCell131.Text = a.ToString();
                    //xrTableCell131.Text = "11111";
                    double d = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p=>p.duyet!=0).Where(p=>p.duyet!=3).Where(p=>p.duyet!=null).Select(p=>p.Mabn).Count());
                    xrTableCell320.Text = d.ToString();

                }
                a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet != 0).Where(p=>p.duyet!=3).Where(p=>p.duyet!=null).Select(p => p.Mabn).Count());
                xrTableCell320.Text = a.ToString();
                double a1 = 0;
                double a2 = 0;
                double a3 = 0;
                double a4 = 0;
                double b1 = 0;
                double b2 = 0;
                double b3 = 0;
                double b4 = 0;
                double c = 0;
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).First().songay != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p=>p.songay));
                    xrTableCell132.Text = a.ToString();
                }

                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).First().Tongcong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p => p.Tongcong));
                    xrTableCell133.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).First().Xetnghiem != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p => p.Xetnghiem));
                    xrTableCell134.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).First().CDHA != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p => p.CDHA));
                    xrTableCell135.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).First().Thuoc != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p => p.Thuoc));
                    xrTableCell136.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).First().Mau != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p => p.Mau));
                    xrTableCell137.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).First().TTPT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p => p.TTPT));
                    xrTableCell138.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).First().VTTH != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p => p.VTTH));
                    xrTableCell139.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).First().VTTT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p => p.VTTT));
                    xrTableCell140.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).First().DVKTC != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p => p.DVKTC));
                    xrTableCell141.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).First().TTG != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p => p.TTG));
                    xrTableCell142.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).First().tiengiuong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p => p.tiengiuong));
                    xrTableCell143.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).First().Vanchuyen != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p => p.Vanchuyen));
                    xrTableCell144.Text = a.ToString("##,###");
                    // xrTableCell42.Text = a.ToString("##,###");
                    double d = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).Sum(p => p.VanchuyenCL));
                    xrTableCell323.Text = d.ToString("##,###");
                    a1 = a;
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).First().Nguoibenh != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p => p.Nguoibenh));
                    xrTableCell145.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).First().BHYT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 1).Sum(p => p.BHYT));
                    xrTableCell146.Text = a.ToString("##,###");
                    double d = Convert.ToDouble(q.Where(p=>p.duyet!=0).Where(p=>p.duyet!=3).Where(p=>p.duyet!=null).Where(p => p.Noitinh == 1).Sum(p=>p.BHYTCL));
                    xrTableCell321.Text = d.ToString("##,###");
                    b1 = d - Convert.ToDouble(q.Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).Where(p => p.Noitinh == 1).Sum(p => p.VanchuyenCL));
                    xrTableCell322.Text = b1.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).ToList().Count > 0)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Select(p=>p.Mabn).Count());
                    xrTableCell150.Text = a.ToString();
                    double d = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p=>p.duyet!=0).Where(p=>p.duyet!=3).Where(p=>p.duyet!=null).Select(p=>p.Mabn).Count());
                    xrTableCell338.Text = d.ToString();
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).First().songay != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p=>p.songay));
                    xrTableCell151.Text = a.ToString();
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).First().Tongcong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p => p.Tongcong));
                    xrTableCell152.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).First().Xetnghiem != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p => p.Xetnghiem));
                    xrTableCell153.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).First().CDHA != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p => p.CDHA));
                    xrTableCell154.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).First().Thuoc != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p => p.Thuoc));
                    xrTableCell155.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).First().Mau != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p => p.Mau));
                    xrTableCell156.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).First().TTPT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p => p.TTPT));
                    xrTableCell157.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).First().VTTH != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p => p.VTTH));
                    xrTableCell158.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).First().VTTT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p => p.VTTT));
                    xrTableCell159.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).First().DVKTC != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p => p.DVKTC));
                    xrTableCell160.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).First().TTG != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p => p.TTG));
                    xrTableCell161.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).First().tiengiuong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p => p.tiengiuong));
                    xrTableCell162.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).First().Vanchuyen != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p => p.Vanchuyen));
                    xrTableCell163.Text = a.ToString("##,###");
                    xrTableCell60.Text = a.ToString("##,###");
                    double d = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p=>p.duyet!=0).Where(p=>p.duyet!=3).Where(p=>p.duyet!=null).Sum(p=>p.VanchuyenCL));
                    xrTableCell341.Text = d.ToString("##,###");
                    a2 = a;
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).First().Nguoibenh != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p => p.Nguoibenh));
                    xrTableCell164.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).First().BHYT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 1).Sum(p => p.BHYT));
                    xrTableCell165.Text = a.ToString("##,###");
                    double d = Convert.ToDouble(q.Where(p=>p.duyet!=0).Where(p=>p.duyet!=3).Where(p=>p.duyet!=null).Where(p => p.Noitinh == 2).Sum(p=>p.BHYTCL));
                    xrTableCell339.Text = d.ToString("##,###");
                    b2 = d - Convert.ToDouble(q.Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).Where(p => p.Noitinh == 2).Sum(p => p.VanchuyenCL));
                    xrTableCell340.Text = b2.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).ToList().Count > 0)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Select(p=>p.Mabn).Count());
                    xrTableCell169.Text = a.ToString();
                    double d = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p=>p.duyet!=0).Where(p=>p.duyet!=3).Where(p=>p.duyet!=null).Select(p=>p.Mabn).Count());
                    xrTableCell332.Text = d.ToString();
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).First().songay != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p=>p.songay));
                    xrTableCell170.Text = a.ToString();
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).First().Tongcong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p => p.Tongcong));
                    xrTableCell171.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).First().Xetnghiem != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p => p.Xetnghiem));
                    xrTableCell172.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).First().CDHA != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p => p.CDHA));
                    xrTableCell173.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).First().Thuoc != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p => p.Thuoc));
                    xrTableCell174.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).First().Mau != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p => p.Mau));
                    xrTableCell175.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).First().TTPT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p => p.TTPT));
                    xrTableCell176.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).First().VTTH != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p => p.VTTH));
                    xrTableCell177.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).First().VTTT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p => p.VTTT));
                    xrTableCell178.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).First().DVKTC != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p => p.DVKTC));
                    xrTableCell179.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).First().TTG != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p => p.TTG));
                    xrTableCell180.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).First().tiengiuong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p => p.tiengiuong));
                    xrTableCell181.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).First().Vanchuyen != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p => p.Vanchuyen));
                    xrTableCell182.Text = a.ToString("##,###");
                    xrTableCell78.Text = a.ToString("##,###");
                    double d = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p=>p.duyet!=0).Where(p=>p.duyet!=3).Where(p=>p.duyet!=null).Sum(p=>p.VanchuyenCL));
                    xrTableCell335.Text = d.ToString("##,###");
                    a3 = a;
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).First().Nguoibenh != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p => p.Nguoibenh));
                    xrTableCell183.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).First().BHYT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 1).Sum(p => p.BHYT));
                    xrTableCell184.Text = a.ToString("##,###");
                    double d = Convert.ToDouble(q.Where(p=>p.duyet!=0).Where(p=>p.duyet!=3).Where(p=>p.duyet!=null).Where(p => p.Noitinh == 3).Sum(p=>p.BHYTCL));
                    xrTableCell333.Text = a.ToString("##,###");
                    b3 = d - Convert.ToDouble(q.Where(p=>p.duyet!=0).Where(p=>p.duyet!=3).Where(p=>p.duyet!=null).Where(p => p.Noitinh == 3).Sum(p=>p.VanchuyenCL));
                    xrTableCell334.Text = b3.ToString("##,###");
                }

                a = Convert.ToDouble(q.Where(p => p.duyet == 1).Select(p=>p.Mabn).Count());
                xrTableCell188.Text = a.ToString();
                double f = Convert.ToDouble(q.Where(p => p.duyet != 0).Where(p=>p.duyet!=3).Where(p=>p.duyet!=null).Select(p => p.Mabn).Count());
                xrTableCell326.Text = f.ToString();
                xrLabel26.Text = "B1 - THẨM ĐỊNH DUYỆT NHƯ ĐỀ NGHỊ GỒM " + a.ToString() + " LƯỢT NGƯỜI";
                xrLabel25.Text = "B - TỔNG SỐ THẨM ĐỊNH ĐƯỢC DUYỆT GỒM " + f.ToString() + " LƯỢT NGƯỜI";

                if (q.Where(p => p.duyet == 1).Sum(p => p.songay) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.songay));
                    xrTableCell189.Text = a.ToString();
                }
                if (q.Where(p => p.duyet == 1).Sum(p => p.Tongcong) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.Tongcong));
                    xrTableCell190.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 1).Sum(p => p.Xetnghiem) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.Xetnghiem));
                    xrTableCell191.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 1).Sum(p => p.CDHA) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.CDHA));
                    xrTableCell192.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 1).Sum(p => p.Thuoc) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.Thuoc));
                    xrTableCell193.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 1).Sum(p => p.Mau) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.Mau));
                    xrTableCell194.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 1).Sum(p => p.TTPT) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.TTPT));
                    xrTableCell195.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 1).Sum(p => p.VTTH) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.VTTH));
                    xrTableCell196.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 1).Sum(p => p.VTTT) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.VTTT));
                    xrTableCell197.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 1).Sum(p => p.DVKTC) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.DVKTC));
                    xrTableCell198.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 1).Sum(p => p.TTG) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.TTG));
                    xrTableCell199.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 1).Sum(p => p.tiengiuong) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.tiengiuong));
                    xrTableCell200.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 1).Sum(p => p.Vanchuyen) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.Vanchuyen));
                    xrTableCell201.Text = a.ToString("##,###");
                    xrTableCell96.Text = a.ToString("##,###");
                    double d = Convert.ToDouble(q.Where(p=>p.duyet!=0).Where(p=>p.duyet!=3).Where(p=>p.duyet!=null).Sum(p => p.VanchuyenCL));
                    xrTableCell329.Text = d.ToString("##,###");
                    a4 = a;
                }
                //double aa = Convert.ToDouble(q.Sum(p => p.Vanchuyen));
                //xrTableCell93.Text = aa.ToString("##,###");
                if (q.Where(p => p.duyet == 1).Sum(p => p.Nguoibenh) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.Nguoibenh));
                    xrTableCell202.Text = a.ToString("##,###");
                }
                //xrTableCell94.Text = string.Format(q.Sum(p => p.Nguoibenh).ToString(), "#,#,00");
                //xrTableCell94.Text = string.Format("{#,#}","100000");
                if (q.Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).Sum(p => p.BHYT) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 1).Sum(p => p.BHYT));
                    xrTableCell203.Text = a.ToString("##,###");
                    double d = Convert.ToDouble(q.Where(p=>p.duyet!=0).Where(p=>p.duyet!=3).Where(p=>p.duyet!=null).Sum(p => p.BHYTCL));
                    double db = Convert.ToDouble(q.Where(p => p.duyet != 0).Where(p => p.duyet != 3).Where(p => p.duyet != null).Sum(p => p.BHYTCL));
                    xrTableCell327.Text = db.ToString("##,###");
                    c = a;
                    b4 = d - Convert.ToDouble(q.Where(p=>p.duyet!=0).Where(p=>p.duyet!=3).Where(p=>p.duyet!=null).Sum(p => p.VanchuyenCL));
                    xrTableCell328.Text = b4.ToString("##,###");
                    txtsotien.Text = DungChung.Ham.DocTienBangChu(d, "đồng/.");
                }

            }
            if (q.Where(p => p.duyet == 2).ToList().Count > 0)
            {
                double a = 0;
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().soluot != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Select(p=>p.Mabn).Count());
                    xrTableCell233.Text = a.ToString();
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().songay != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p=>p.songay));
                    xrTableCell234.Text = a.ToString();
                }

                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().Tongcong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p => p.TongcongCL));
                    //double e=
                    xrTableCell235.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().Xetnghiem != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p => p.XetnghiemCL));
                    xrTableCell236.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().CDHA != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p => p.CDHACL));
                    xrTableCell237.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().Thuoc != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p => p.ThuocCL));
                    xrTableCell238.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().Mau != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p => p.MauCL));
                    xrTableCell239.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().TTPT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p => p.TTPTCL));
                    xrTableCell240.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().VTTH != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p => p.VTTHCL));
                    xrTableCell241.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().VTTT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p => p.VTTTCL));
                    xrTableCell242.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().DVKTC != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p => p.DVKTCCL));
                    xrTableCell243.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().TTG != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p => p.TTGCL));
                    xrTableCell244.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().tiengiuong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p => p.tiengiuongCL));
                    xrTableCell245.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().Vanchuyen != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p => p.VanchuyenCL));
                    xrTableCell246.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().Nguoibenh != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p => p.NguoibenhCL));
                    xrTableCell247.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).First().BHYT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).Where(p => p.duyet == 2).Sum(p => p.BHYTCL));
                    xrTableCell248.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet != 0).ToList().Count > 0)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Select(p=>p.Mabn).Count());
                    xrTableCell252.Text = a.ToString();
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().songay != null)//gjhgjhg
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p=>p.songay));
                    xrTableCell253.Text = a.ToString();
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().Tongcong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p => p.TongcongCL));
                    xrTableCell254.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().Xetnghiem != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p => p.XetnghiemCL));
                    xrTableCell255.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().CDHA != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p => p.CDHACL));
                    xrTableCell256.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().Thuoc != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p => p.ThuocCL));
                    xrTableCell257.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().Mau != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p => p.MauCL));
                    xrTableCell258.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().TTPT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p => p.TTPTCL));
                    xrTableCell259.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().VTTH != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p => p.VTTHCL));
                    xrTableCell260.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().VTTT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p => p.VTTTCL));
                    xrTableCell261.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().DVKTC != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p => p.DVKTCCL));
                    xrTableCell262.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().TTG != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p => p.TTGCL));
                    xrTableCell263.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().tiengiuong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p => p.tiengiuongCL));
                    xrTableCell264.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().Vanchuyen != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p => p.VanchuyenCL));
                    xrTableCell265.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().Nguoibenh != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p => p.NguoibenhCL));
                    xrTableCell266.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).First().BHYT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).Where(p => p.duyet == 2).Sum(p => p.BHYTCL));
                    xrTableCell267.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Where(p => p.duyet == 2).ToList().Count > 0)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Select(p=>p.Mabn).Count());
                    xrTableCell271.Text = a.ToString();
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().songay != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p=>p.songay));
                    xrTableCell272.Text = a.ToString();
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().Tongcong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p => p.TongcongCL));
                    xrTableCell273.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().Xetnghiem != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p => p.XetnghiemCL));
                    xrTableCell274.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().CDHA != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p => p.CDHACL));
                    xrTableCell275.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().Thuoc != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p => p.ThuocCL));
                    xrTableCell276.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().Mau != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p => p.MauCL));
                    xrTableCell277.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().TTPT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p => p.TTPTCL));
                    xrTableCell278.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().VTTH != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p => p.VTTHCL));
                    xrTableCell279.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().VTTT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p => p.VTTTCL));
                    xrTableCell280.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().DVKTC != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p => p.DVKTCCL));
                    xrTableCell281.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().TTG != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p => p.TTGCL));
                    xrTableCell282.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().tiengiuong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p => p.tiengiuongCL));
                    xrTableCell283.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().Vanchuyen != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p => p.VanchuyenCL));
                    xrTableCell284.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().Nguoibenh != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p => p.NguoibenhCL));
                    xrTableCell285.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).ToList().Count > 0 && q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).First().BHYT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).Where(p => p.duyet == 2).Sum(p => p.BHYTCL));
                    xrTableCell286.Text = a.ToString("##,###");
                }

                a = Convert.ToDouble(q.Where(p => p.duyet == 2).Select(p=>p.Mabn).Count());
                xrTableCell290.Text = a.ToString();
                xrLabel44.Text = "B2 - THẨM ĐỊNH CÓ ĐIỀU CHỈNH CHI PHÍ GỒM " + a.ToString() + " LƯỢT NGƯỜI";
                xrLabel70.Text = "C1 - DANH SÁCH BỆNH NHÂN THẨM ĐỊNH TỪ CHỐI THANH TOÁN MỘT PHẦN GỒM " + a.ToString() + " LƯỢT NGƯỜI";

                if (q.Where(p => p.duyet == 2).Sum(p => p.songay) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.songay));
                    xrTableCell291.Text = a.ToString();  
                }
                if (q.Where(p => p.duyet == 2).Sum(p => p.Tongcong) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.TongcongCL));
                    xrTableCell292.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 2).Sum(p => p.XetnghiemCL) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.XetnghiemCL));
                    xrTableCell293.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 2).Sum(p => p.CDHA) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.CDHACL));
                    xrTableCell294.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 2).Sum(p => p.Thuoc) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.ThuocCL));
                    xrTableCell295.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 2).Sum(p => p.Mau) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.MauCL));
                    xrTableCell296.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 2).Sum(p => p.TTPT) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.TTPTCL));
                    xrTableCell297.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 2).Sum(p => p.VTTH) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.VTTHCL));
                    xrTableCell298.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 2).Sum(p => p.VTTT) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.VTTTCL));
                    xrTableCell299.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 2).Sum(p => p.DVKTC) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.DVKTCCL));
                    xrTableCell300.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 2).Sum(p => p.TTG) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.TTGCL));
                    xrTableCell301.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 2).Sum(p => p.tiengiuong) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.tiengiuongCL));
                    xrTableCell302.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 2).Sum(p => p.Vanchuyen) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.VanchuyenCL));
                    xrTableCell303.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 2).Sum(p => p.Nguoibenh) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.NguoibenhCL));
                    xrTableCell304.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.duyet == 2).Sum(p => p.BHYT) != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.duyet == 2).Sum(p => p.BHYTCL));
                    xrTableCell305.Text = a.ToString("##,###");
                }
            }

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            txtMacs.Text = "Mã số: " + DungChung.Bien.MaBV.Substring(0, 2) + DungChung.Bien.MaBV.Substring(2, 3);
            txtTencs.Text = "Tên CSKCB: " + DungChung.Bien.TenCQ;
        }
        public void BindingData()
        {
            //colMathe1.DataBindings.Add("Text", DataSource, "SThe");
            colHoten.DataBindings.Add("Text", DataSource, "TenBNhan");
            //colNgayvao.DataBindings.Add("Text", DataSource, "Ngayvao");
            //colNgayra.DataBindings.Add("Text", DataSource, "Ngayra");
            txtNgayvao.DataBindings.Add("Text", DataSource, "Ngayvao");
            txtNgayra.DataBindings.Add("Text", DataSource, "Ngayra");
            colSotienTC.DataBindings.Add("Text", DataSource, "Tongcong").FormatString=DungChung.Bien.FormatString[1];
            colSotienNguoibenh.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            colSotienquyettoan.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colLydo.DataBindings.Add("Text", DataSource, "Lydo");
            colGF1SotienTC.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colGFSotienNguoibenh.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            colGFSotienQT.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colGF2Sotien.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colGF2Sotiennguoibenh.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            colGF2SotienQT.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colRFSotien.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colRFNguoibenh.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            colRFSotienQT.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader4.GroupFields.Add(new GroupField("NoiTinh"));
            GroupHeader3.GroupFields.Add(new GroupField("Tuyen"));
        }

        private void colHoten_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Ngayvao") != null)
            {
                colNgayvao.Text = this.GetCurrentColumnValue("Ngayvao").ToString().Substring(0, 10);
            }
            if (this.GetCurrentColumnValue("Ngayra") != null)
            {
                colNgayra.Text = this.GetCurrentColumnValue("Ngayra").ToString().Substring(0, 10);
            }
        }
        string tongcong = " Tổng cộng ";
        private void GroupHeader4_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NoiTinh") != null)
            {
                string noitinh = this.GetCurrentColumnValue("NoiTinh").ToString();
                switch (noitinh)
                {
                    case "1":
                        xrTableCell358.Text = " Bệnh nhân nội tỉnh kcb ban đầu".ToUpper();
                        xrTableCell352.Text = "A";
                        tongcong += "A";
                        xrTableCell374.Text = " Cộng: A";
                        break;
                    case "2":
                        xrTableCell358.Text = " Bệnh nhân nội tỉnh đến".ToUpper();
                        xrTableCell352.Text = "B";
                        tongcong += "+B";
                        xrTableCell374.Text = " Cộng: B";
                        break;
                    case "3":
                        xrTableCell358.Text = " Bệnh nhân ngoại tỉnh đến".ToUpper();
                        xrTableCell352.Text = "C";
                        tongcong += "+C";
                        xrTableCell374.Text = " Cộng: C";
                        
                        break;
                }
            }
        }

        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Tuyen") != null)
            {
                int sttg2 = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
                if (sttg2 == 2)
                {
                    xrTableCell353.Text = "II";
                }
                else
                {
                    if (sttg2 == 1)
                    {
                        xrTableCell353.Text = "I";
                    }
                }
            }
            if (this.GetCurrentColumnValue("Tuyen") != null)
            {
                int tuyen = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
                //int tt;
                //if (tuyen == 1)
                //{ tt = 0; }
                //else
                //{ tt = 1; }
                if (tuyen == 2)
                {
                    xrTableCell364.Text = " Trái tuyến";
                    xrTableCell349.Text = " Cộng trái tuyến";
                }
                if (tuyen == 1)
                {
                    xrTableCell364.Text = " Đúng tuyến";
                    xrTableCell349.Text = " Cộng đúng tuyến";
                }
            }
        }


    }
}
