using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class rep_BcCTKhoaDuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BcCTKhoaDuoc()
        {
            InitializeComponent();
        }
        public double[] tienthuoc = new double[100];
        public string CHUYEN(double A)
        {  
            string ts = DungChung.Bien.FormatString[1];
            return string.Format(ts, A);


        }
        public string daingay(DateTime a,DateTime b) {
            string ketqua = string.Format("Từ ngày {0} Đến ngày {1}", a.ToShortDateString(), b.ToShortDateString());
            return ketqua;
        }
        public DateTime ngaybatdau;
        public DateTime ngayketthuc;
        public string [] ks = new string[3];
        public void hamin() 
        {
            CQCQ.Text = DungChung.Bien.TenCQCQ;
            CQ.Text = DungChung.Bien.TenCQ;
            if (DungChung.Bien.MaBV == "14017") //14017 y/c để trắng cột tỷ lệ
            {
                xrTableCell10.Text = ""; xrTableCell30.Text = ""; xrTableCell35.Text = ""; xrTableCell45.Text = ""; xrTableCell50.Text = ""; xrTableCell60.Text = ""; xrTableCell65.Text = ""; xrTableCell70.Text = ""; xrTableCell75.Text = "";
                xrTableCell80.Text = ""; xrTableCell100.Text = ""; xrTableCell105.Text = ""; xrTableCell110.Text = ""; xrTableCell115.Text = "";
            }
            //tongtienthuocnt.Text = CHUYEN(tienthuoc[0]+tienthuoc[1]);
            //tienthuocnn.Text = CHUYEN(tienthuoc[0]);
            //tienthuoctn.Text = CHUYEN(tienthuoc[1]);

            //tienthuocks.Text=CHUYEN(tienthuoc[3]);
            //tienthuocvitamin.Text=CHUYEN(tienthuoc[4]);
            //tienthuocdichchuyen.Text=CHUYEN(tienthuoc[5]);
            //tienthuoccorticoid.Text=CHUYEN(tienthuoc[6]);
            //tienthuockhac.Text = CHUYEN(tienthuoc[7]);
            //Tongtiensd.Text = CHUYEN(tienthuoc[7] + tienthuoc[6] + tienthuoc[5] + tienthuoc[4] + tienthuoc[3]);
            //tungay.Text = daingay(ngaybatdau, ngayketthuc);
            //danhdiaqud.Text=ks[0];
            //nhanxetqd.Text=ks[1];
            //kiennghi.Text = ks[2];
        
        }
    }
}
//        int _nam = 2000;
//        string _makho = "";
//        public rep_BcCTKhoaDuoc(int nam,string ma)
//        {
//            InitializeComponent();
//            _nam = nam;
//            _makho = ma;
//        }
       
//        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        
//        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
//        {
//            DateTime tungay = System.DateTime.Now.Date;
//            DateTime denngay = System.DateTime.Now.Date;
//            tungay = Convert.ToDateTime(NgayTu.Value);
//            denngay = Convert.ToDateTime(NgayDen.Value);
//            txtGiamDoc.Text = DungChung.Bien.GiamDoc;
//            txtKTT.Text = DungChung.Bien.KeToanTruong;
//            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
//            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
//            txtTruongKD.Text = DungChung.Bien.TruongKhoaDuoc;
//            //
//            //
//            var slthuoc = (from nd in _data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.KieuDon == 1)
//                           join ndct in _data.NhapDcts on nd.IDNhap equals ndct.IDNhap
//                           join dv in _data.DichVus on ndct.MaDV equals dv.MaDV
//                           join nhomdv in _data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
//                           join tieunhom in _data.TieuNhomDVs on dv.IdTieuNhom equals tieunhom.IdTieuNhom
//                           where (nd.NgayNhap>=tungay && nd.NgayNhap<=denngay)
//                           group new { ndct, nhomdv, tieunhom,dv,nd } by new { nhomdv.TenNhom,tieunhom.TenTN,dv.NuocSX,nd.XuatTD } into kq
//                           select new {kq.Key.XuatTD, kq.Key.TenNhom,kq.Key.TenTN,kq.Key.NuocSX, SoLuong = kq.Sum(p => p.ndct.ThanhTienN) }).ToList();
//            double _ttien = 0;
//            double _trongnuoc = 0;
//            double _tongtien = 0;
//            var slthuoc1 = (from nd in _data.NhapDs.Where(p=>p.KieuDon!=2&&p.KieuDon!=3&&p.KieuDon!=6&&p.KieuDon!=9)
//                           join ndct in _data.NhapDcts on nd.IDNhap equals ndct.IDNhap
//                           join dv in _data.DichVus on ndct.MaDV equals dv.MaDV
//                           join nhomdv in _data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
//                           join tieunhom in _data.TieuNhomDVs on dv.IdTieuNhom equals tieunhom.IdTieuNhom
//                           where (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay)
//                           group new { ndct, nhomdv, tieunhom, dv, nd } by new { nhomdv.TenNhom, tieunhom.TenTN, dv.NuocSX, nd.XuatTD } into kq
//                           select new { kq.Key.XuatTD, kq.Key.TenNhom, kq.Key.TenTN, kq.Key.NuocSX, SoLuong = kq.Sum(p => p.ndct.ThanhTienN) }).ToList();
//            if (slthuoc.Where(p => p.TenNhom.ToLower().Contains("thuốc")).ToList().Count>0) {
//                if (slthuoc.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Sum(p => p.SoLuong) != null)
//                {
//                    _ttien = slthuoc.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Sum(p => p.SoLuong).Value;
//                    _tongtien = _ttien;
//                }
//                xrTableCell9.Text = (_ttien/1000).ToString("##,###");
//                _ttien = 0;
//                if (slthuoc.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Where(p => p.NuocSX.ToLower().Contains("việt nam")).ToList().Count > 0)
//                {
//                    _ttien = slthuoc.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Where(p => p.NuocSX.ToLower().Contains("việt nam")).Sum(p => p.SoLuong).Value;
//                    _trongnuoc = _ttien;
//                }
//                xrTableCell24.Text = (_ttien / 1000).ToString("##,###");
//                _ttien = 0;
                
//                xrTableCell19.Text = (_tongtien/1000- _trongnuoc/1000).ToString("##,###");
//                _ttien = 0;
//            }
//            if (slthuoc.Where(p => p.TenNhom.ToLower().Contains("hóa chất")).ToList().Count > 0)
//            {
//                if (slthuoc.Where(p => p.TenNhom.ToLower().Contains("hóa chất")).Sum(p => p.SoLuong)!=null)
//                _ttien = slthuoc.Where(p => p.TenNhom.ToLower().Contains("hóa chất")).Sum(p => p.SoLuong).Value;
//                xrTableCell29.Text = (_ttien / 1000).ToString("##,###");
//                _ttien = 0;
//            }
//            if (slthuoc.Where(p => p.TenNhom.ToLower().Contains("y tế")).ToList().Count > 0)
//            {
//                if (slthuoc.Where(p => p.TenNhom.ToLower().Contains("y tế")).Sum(p => p.SoLuong)!=null)
//                    _ttien = slthuoc.Where(p => p.TenNhom.ToLower().Contains("y tế")).Sum(p => p.SoLuong).Value;
//                xrTableCell34.Text = (_ttien / 1000).ToString("##,###");
//                _ttien = 0;
//                 if (slthuoc.Where(p => p.TenNhom.ToLower().Contains("y tế")).Where(p=>p.TenTN.ToLower().Contains("vắc xin")).ToList().Count > 0)
//                     _ttien=slthuoc.Where(p => p.TenNhom.ToLower().Contains("y tế")).Where(p=>p.TenTN.ToLower().Contains("vắc xin")).Sum(p=>p.SoLuong).Value;
//                xrTableCell44.Text = (_ttien/100).ToString("##,###");
//                _ttien = 0;
//            }
            

//            //thuốc đã sử dụng
//            var slthuocsd = (from nd in _data.NhapDs.Where(p => p.NgayNhap.Value.Year == _nam).Where(p => p.MaKP == _makho).Where(p=>p.PLoai==5)
//                           join ndct in _data.NhapDcts on nd.IDNhap equals ndct.IDNhap
//                           join dv in _data.DichVus on ndct.MaDV equals dv.MaDV
//                           join nhomdv in _data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
//                           join tieunhom in _data.TieuNhomDVs on dv.IdTieuNhom equals tieunhom.IdTieuNhom
//                           where(nhomdv.TenNhom.ToLower().Contains("thuốc") || nhomdv.TenNhom.ToLower().Contains("máu"))
//                             group new { ndct, nhomdv, tieunhom } by new { nhomdv.TenNhom,tieunhom.TenTN } into kq
//                             select new { kq.Key.TenNhom,kq.Key.TenTN, SoLuong = kq.Sum(p => p.ndct.ThanhTienSD) }).ToList();
//            if (slthuocsd.Where(p => p.TenNhom.ToLower().Contains("thuốc")).ToList().Count > 0) {
//                if (slthuocsd.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Sum(p => p.SoLuong) != null)
//                    _ttien = slthuocsd.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Sum(p => p.SoLuong).Value;
//                xrTableCell49.Text = (_ttien / 100).ToString("##,###");
//                _ttien = 0;
//                if (slthuocsd.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Where(p => p.TenTN.ToLower().Contains("kháng sinh")).Sum(p => p.SoLuong) != null)
//                    _ttien = slthuocsd.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Where(p => p.TenTN.ToLower().Contains("kháng sinh")).Sum(p => p.SoLuong).Value;
//                xrTableCell59.Text = (_ttien / 100).ToString("##,###");
//                _ttien = 0;
//                if (slthuocsd.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Where(p => p.TenTN.ToLower().Contains("kháng sinh")).Sum(p => p.SoLuong) != null)
//                    _ttien = slthuocsd.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Where(p => p.TenTN.ToLower().Contains("kháng sinh")).Sum(p => p.SoLuong).Value;
//                xrTableCell64.Text = (_ttien / 100).ToString("##,###");
//                _ttien = 0;
//                if (slthuocsd.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Where(p => p.TenTN.ToLower().Contains("dịch truyền")).Sum(p => p.SoLuong) != null)
//                    _ttien = slthuocsd.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Where(p => p.TenTN.ToLower().Contains("dịch truyền")).Sum(p => p.SoLuong).Value;
//                xrTableCell69.Text = (_ttien / 100).ToString("##,###");
//                _ttien = 0;
//                if (slthuocsd.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Where(p => p.TenTN.ToLower().Contains("corticoid")).Sum(p => p.SoLuong) != null)
//                    _ttien = slthuocsd.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Where(p => p.TenTN.ToLower().Contains("corticoid")).Sum(p => p.SoLuong).Value;
//                xrTableCell74.Text = (_ttien / 100).ToString("##,###");
//                _ttien = 0;
//                if (slthuocsd.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Where(p => p.TenTN.ToLower().Contains("khác")).Sum(p => p.SoLuong) != null)
//                    _ttien = slthuocsd.Where(p => p.TenNhom.ToLower().Contains("thuốc")).Where(p => p.TenTN.ToLower().Contains("khác")).Sum(p => p.SoLuong).Value;
//                xrTableCell79.Text = (_ttien / 100).ToString("##,###");
//                _ttien = 0;
//            }
//            if (slthuocsd.Where(p => p.TenNhom.ToLower().Contains("máu")).ToList().Count > 0)
//            {
//                if (slthuocsd.Where(p => p.TenNhom.ToLower().Contains("máu")).Sum(p => p.SoLuong) != null)
//                    _ttien = slthuocsd.Where(p => p.TenNhom.ToLower().Contains("máu")).Sum(p => p.SoLuong).Value;
//                xrTableCell84.Text = (_ttien / 100).ToString("##,###");
//                _ttien = 0;
              
//            }
//        }
//    }
//}
