using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_80bHDCD : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_80bHDCD()
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
            //         join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
            //         join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
            //         join nhom in _Data.NhomDVs on dv.IDNhom equals nhom.IDNhom
            //         join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
            //         where (vp.NgayTT>=nt&&vp.NgayTT<=nd)
            //         group new { bn, vpct, rv, nhom } by new { bn.NoiTinh } into kq
            //         select new
            //         {
            //             Noitinh = kq.Key.NoiTinh,
            //             soluot = kq.Select(p => p.bn.MaBNhan).Count(),
            //             songay = kq.Sum(p => p.rv.SoNgaydt),
            //             Tongcong = kq.Sum(p => p.vpct.ThanhTien),
            //             Xetnghiem = kq.Where(p => p.nhom.TenNhom.Contains("xét nghiệm")).Sum(p => p.vpct.ThanhTien),
            //             CDHA = kq.Where(p => p.nhom.TenNhom.Contains("CĐHA")).Sum(p => p.vpct.ThanhTien),
            //             Thuoc = kq.Where(p => p.nhom.TenNhom.Contains("Thuốc")).Sum(p => p.vpct.ThanhTien),
            //             Mau = kq.Where(p => p.nhom.TenNhom.Contains("máu")).Sum(p => p.vpct.ThanhTien),
            //             TTPT = kq.Where(p => p.nhom.TenNhom.Contains("thủ thuật")).Sum(p => p.vpct.ThanhTien),
            //             VTTH = kq.Where(p => p.nhom.TenNhom.Contains("vật tư")).Sum(p => p.vpct.ThanhTien),
            //             VTTT = kq.Where(p => p.nhom.TenNhom.Contains("VTTT")).Sum(p => p.vpct.ThanhTien),
            //             DVKTC = kq.Where(p => p.nhom.TenNhom.Contains("kỹ thuật cao")).Sum(p => p.vpct.ThanhTien),
            //             TTG = kq.Where(p => p.nhom.TenNhom.Contains("Thải ghép")).Sum(p => p.vpct.ThanhTien),
            //             tiengiuong = kq.Where(p => p.nhom.TenNhom.Contains("Ngày giường")).Sum(p => p.vpct.ThanhTien),
            //             Vanchuyen = kq.Where(p => p.nhom.TenNhom.ToLower().Contains("vận chuyển")).Sum(p => p.vpct.ThanhTien),
            //             Nguoibenh = kq.Sum(p => p.vpct.TienBN),
            //             BHYT = kq.Sum(p => p.vpct.TienBH)
            //         }).ToList();

            var q1 = (from bn in _Data.BenhNhans.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 1)
                      join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                      join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                      join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                      join nhom in _Data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                      join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                      where (vp.NgayTT >= nt && vp.NgayTT <= nd)
                      select new
                      {
                          bn.NoiTinh,
                          bn.MaBNhan,
                          rv.SoNgaydt,
                          vpct.ThanhTien,
                          nhom.TenNhomCT,
                          vpct.TienBN,
                          vpct.TienBH
                      }).ToList();

            var q2 = (from b in q1
                      group new { b } by new { b.NoiTinh, b.MaBNhan, b.TenNhomCT } into kq
                      select new
                      {
                          Noitinh = kq.Key.NoiTinh,
                          MaBNhan=kq.Key.MaBNhan,
                          songay = kq.Sum(p => p.b.SoNgaydt),
                          TenNhomCT=kq.Key.TenNhomCT,
                          Tongcong = kq.Sum(p => p.b.ThanhTien),
                          Xetnghiem = kq.Where(p => p.b.TenNhomCT.Contains("Xét nghiệm")).Sum(p => p.b.ThanhTien),
                          CDHA = kq.Where(p => p.b.TenNhomCT.Contains("Chẩn đoán hình ảnh")).Sum(p => p.b.ThanhTien),
                          Thuoc = kq.Where(p => p.b.TenNhomCT.Contains( "Thuốc trong danh mục BHYT" )).Sum(p => p.b.ThanhTien),
                          Mau = kq.Where(p => p.b.TenNhomCT.Contains("Máu và chế phẩm của máu")).Sum(p => p.b.ThanhTien),
                          TTPT = kq.Where(p => p.b.TenNhomCT.Contains("Thủ thuật, phẫu thuật")).Sum(p => p.b.ThanhTien),
                          VTTH = kq.Where(p => p.b.TenNhomCT.Contains("Vật tư y tế trong danh mục BHYT")).Sum(p => p.b.ThanhTien),
                          VTTT = kq.Where(p => p.b.TenNhomCT.Contains("VTYT thanh toán theo tỷ lệ")).Sum(p => p.b.ThanhTien),
                          DVKTC = kq.Where(p => p.b.TenNhomCT.Contains("DVKT thanh toán theo tỷ lệ")).Sum(p => p.b.ThanhTien),
                          TTG = kq.Where(p => p.b.TenNhomCT.Contains("Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")).Sum(p => p.b.ThanhTien),
                          tiengiuong = kq.Where(p => p.b.TenNhomCT.Contains("Giường điều trị nội trú")).Sum(p => p.b.ThanhTien),
                          Vanchuyen = kq.Where(p => p.b.TenNhomCT.ToLower().Contains("Vận chuyển")).Sum(p => p.b.ThanhTien),
                          Nguoibenh = kq.Sum(p => p.b.TienBN),
                          BHYT = kq.Sum(p => p.b.TienBH)
                      }).ToList();
            var q = (from b in q1
                      group new { b } by new { b.NoiTinh } into kq
                      select new
                      {
                          Noitinh = kq.Key.NoiTinh,
                          soluot = kq.Select(p => p.b.MaBNhan).Distinct().Count(),
                          songay = kq.Sum(p => p.b.SoNgaydt),
                          Tongcong = kq.Sum(p => p.b.ThanhTien),
                          Xetnghiem = kq.Where(p => p.b.TenNhomCT.Contains("Xét nghiệm")).Sum(p => p.b.ThanhTien),
                          CDHA = kq.Where(p => p.b.TenNhomCT.Contains("Chẩn đoán hình ảnh")).Sum(p => p.b.ThanhTien),
                          Thuoc = kq.Where(p => p.b.TenNhomCT.Contains( "Thuốc trong danh mục BHYT" )).Sum(p => p.b.ThanhTien),
                          Mau = kq.Where(p => p.b.TenNhomCT.Contains("Máu và chế phẩm của máu")).Sum(p => p.b.ThanhTien),
                          TTPT = kq.Where(p => p.b.TenNhomCT.Contains("Thủ thuật, phẫu thuật")).Sum(p => p.b.ThanhTien),
                          VTTH = kq.Where(p => p.b.TenNhomCT.Contains("Vật tư y tế trong danh mục BHYT")).Sum(p => p.b.ThanhTien),
                          VTTT = kq.Where(p => p.b.TenNhomCT.Contains("VTYT thanh toán theo tỷ lệ")).Sum(p => p.b.ThanhTien),
                          DVKTC = kq.Where(p => p.b.TenNhomCT.Contains("DVKT thanh toán theo tỷ lệ")).Sum(p => p.b.ThanhTien),
                          TTG = kq.Where(p => p.b.TenNhomCT.Contains("Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")).Sum(p => p.b.ThanhTien),
                          tiengiuong = kq.Where(p => p.b.TenNhomCT.Contains("Giường điều trị nội trú")).Sum(p => p.b.ThanhTien),
                          Vanchuyen = kq.Where(p => p.b.TenNhomCT.ToLower().Contains("Vận chuyển")).Sum(p => p.b.ThanhTien),
                          Nguoibenh = kq.Sum(p => p.b.TienBN),
                          BHYT = kq.Sum(p => p.b.TienBH)
                      }).ToList();
            
            
            if (q.Count > 0)
            {
                double a = 0;
                
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().soluot);
                    colSoluotIA.Text = a.ToString();
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
                if (q.Where(p => p.Noitinh == 1).First().songay != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().songay);
                    colSongayIA.Text = a.ToString();
                }

                if (q.Where(p => p.Noitinh == 1).First().Tongcong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().Tongcong);
                    colTongcongIA.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).First().Xetnghiem != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().Xetnghiem);
                    xrTableCell29.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).First().CDHA != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().CDHA);
                    xrTableCell30.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).First().Thuoc != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().Thuoc);
                    xrTableCell31.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).First().Mau != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().Mau);
                    xrTableCell32.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).First().TTPT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().TTPT);
                    xrTableCell33.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).First().VTTH != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().VTTH);
                    xrTableCell34.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).First().VTTT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().VTTT);
                    xrTableCell35.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).First().DVKTC != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().DVKTC);
                    xrTableCell36.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).First().TTG != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().TTG);
                    xrTableCell37.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).First().tiengiuong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().tiengiuong);
                    xrTableCell38.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).First().Vanchuyen != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().Vanchuyen);
                    xrTableCell39.Text = a.ToString("##,###");
                    xrTableCell42.Text = a.ToString("##,###");
                    xrTableCell323.Text = a.ToString("##,###");
                    a1 = a;
                }
                if (q.Where(p => p.Noitinh == 1).First().Nguoibenh != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().Nguoibenh);
                    xrTableCell40.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 1).First().BHYT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 1).First().BHYT);
                    xrTableCell41.Text = a.ToString("##,###");
                    xrTableCell321.Text = a.ToString("##,###");
                    b1 = a - a1;
                    xrTableCell322.Text=b1.ToString("##,###");
                }
                
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().soluot);
                    colSoluotIIA.Text = a.ToString();
                    xrTableCell338.Text = a.ToString();
              
                if (q.Where(p => p.Noitinh == 2).First().songay != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().songay);
                    colSongayIIA.Text = a.ToString();
                }
                if (q.Where(p => p.Noitinh == 2).First().Tongcong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().Tongcong);
                    xrTableCell46.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).First().Xetnghiem != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().Xetnghiem);
                    xrTableCell47.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).First().CDHA != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().CDHA);
                    xrTableCell48.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).First().Thuoc != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().Thuoc);
                    xrTableCell49.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).First().Mau != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().Mau);
                    xrTableCell50.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).First().TTPT != null)
                {
                     a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().TTPT);
                    xrTableCell51.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).First().VTTH != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().VTTH);
                    xrTableCell52.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).First().VTTT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().VTTT);
                    xrTableCell53.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).First().DVKTC != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().DVKTC);
                    xrTableCell54.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).First().TTG != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().TTG);
                    xrTableCell55.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).First().tiengiuong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().tiengiuong);
                    xrTableCell56.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).First().Vanchuyen != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().Vanchuyen);
                    xrTableCell57.Text = a.ToString("##,###");
                    xrTableCell60.Text = a.ToString("##,###");
                    xrTableCell341.Text = a.ToString("##,###");
                    a2 = a;
                }
                if (q.Where(p => p.Noitinh == 2).First().Nguoibenh != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().Nguoibenh);
                    xrTableCell58.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 2).First().BHYT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 2).First().BHYT);
                    xrTableCell59.Text = a.ToString("##,###");
                    xrTableCell339.Text = a.ToString("##,###");
                    b2 = a - a2;
                    xrTableCell340.Text = b2.ToString("##,###");
                }
                
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().soluot);
                    colSOluotIIIA.Text = a.ToString();
                    xrTableCell332.Text = a.ToString();
                
                if (q.Where(p => p.Noitinh == 3).First().songay != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().songay);
                    colSongayIIIA.Text = a.ToString();
                }
                if (q.Where(p => p.Noitinh == 3).First().Tongcong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().Tongcong);
                    xrTableCell64.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).First().Xetnghiem != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().Xetnghiem);
                    xrTableCell65.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).First().CDHA != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().CDHA);
                    xrTableCell66.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).First().Thuoc != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().Thuoc);
                    xrTableCell67.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).First().Mau != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().Mau);
                    xrTableCell68.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).First().TTPT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().TTPT);
                    xrTableCell69.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).First().VTTH != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().VTTH);
                    xrTableCell70.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).First().VTTT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().VTTT);
                    xrTableCell71.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).First().DVKTC != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().DVKTC);
                    xrTableCell72.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).First().TTG != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().TTG);
                    xrTableCell73.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).First().tiengiuong != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().tiengiuong);
                    xrTableCell74.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).First().Vanchuyen != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().Vanchuyen);
                    xrTableCell75.Text = a.ToString("##,###");
                    xrTableCell78.Text = a.ToString("##,###");
                    xrTableCell335.Text = a.ToString("##,###");
                    a3 = a;
                }
                if (q.Where(p => p.Noitinh == 3).First().Nguoibenh != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().Nguoibenh);
                    xrTableCell76.Text = a.ToString("##,###");
                }
                if (q.Where(p => p.Noitinh == 3).First().BHYT != null)
                {
                    a = Convert.ToDouble(q.Where(p => p.Noitinh == 3).First().BHYT);
                    xrTableCell77.Text = a.ToString("##,###");
                    xrTableCell333.Text = a.ToString("##,###");
                    b3 = a - a3;
                    xrTableCell334.Text = b3.ToString("##,###");
                }
                
                    a = Convert.ToDouble(q.Sum(p => p.soluot));
                    colSoluotIVA.Text = a.ToString();
                    xrTableCell326.Text = a.ToString();
                
                if (q.Sum(p => p.songay) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.songay));
                    colSongayIVA.Text = a.ToString();
                }
                if (q.Sum(p => p.Tongcong) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.Tongcong));
                    xrTableCell82.Text = a.ToString("##,###");
                }
                if (q.Sum(p => p.Xetnghiem) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.Xetnghiem));
                    xrTableCell83.Text = a.ToString("##,###");
                }
                if (q.Sum(p => p.CDHA) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.CDHA));
                    xrTableCell84.Text = a.ToString("##,###");
                }
                if (q.Sum(p => p.Thuoc) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.Thuoc));
                    xrTableCell85.Text = a.ToString("##,###");
                }
                if (q.Sum(p => p.Mau) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.Mau));
                    xrTableCell86.Text = a.ToString("##,###");
                }
                if (q.Sum(p => p.TTPT) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.TTPT));
                    xrTableCell87.Text = a.ToString("##,###");
                }
                if (q.Sum(p => p.VTTH) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.VTTH));
                    xrTableCell88.Text = a.ToString("##,###");
                }
                if (q.Sum(p => p.VTTT) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.VTTT));
                    xrTableCell89.Text = a.ToString("##,###");
                }
                if (q.Sum(p => p.DVKTC) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.DVKTC));
                    xrTableCell90.Text = a.ToString("##,###");
                }
                if (q.Sum(p => p.TTG) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.TTG));
                    xrTableCell91.Text = a.ToString("##,###");
                }
                if (q.Sum(p => p.tiengiuong) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.tiengiuong));
                    xrTableCell92.Text = a.ToString("##,###");
                }
                if (q.Sum(p => p.Vanchuyen) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.Vanchuyen));
                    xrTableCell93.Text = a.ToString("##,###");
                    xrTableCell96.Text = a.ToString("##,###");
                    xrTableCell329.Text = a.ToString("##,###");
                    a4 = a;
                }
                //double aa = Convert.ToDouble(q.Sum(p => p.Vanchuyen));
                //xrTableCell93.Text = aa.ToString("##,###");
                if (q.Sum(p => p.Nguoibenh) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.Nguoibenh));
                    xrTableCell94.Text = a.ToString("##,###");
                }
                //xrTableCell94.Text = string.Format(q.Sum(p => p.Nguoibenh).ToString(), "#,#,00");
                //xrTableCell94.Text = string.Format("{#,#}","100000");
                if (q.Sum(p => p.BHYT) != null)
                {
                    a = Convert.ToDouble(q.Sum(p => p.BHYT));
                    xrTableCell95.Text = a.ToString("##,###");
                    xrTableCell327.Text = a.ToString("##,###");
                    c = a;
                    b4 = a - a4;
                    xrTableCell328.Text = b4.ToString("##,###");
                    txtsotien.Text = DungChung.Ham.DocTienBangChu(c,"đồng/.");
                }

            }

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            txtMacs.Text = "Mã số: " + DungChung.Bien.MaBV.Substring(0, 2) + DungChung.Bien.MaBV.Substring(2, 3);
            txtTencs.Text = "Tên CSKCB: " + DungChung.Bien.TenCQ;
        }


    }
}
