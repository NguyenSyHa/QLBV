using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BcNXTToanTT_CM09 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcNXTToanTT_CM09()
        {
            InitializeComponent();
        }
        bool HThi = true;
        public Rep_BcNXTToanTT_CM09(bool ht)
        {
            InitializeComponent();
            HThi = ht;
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
    
        public void BindingData()
        {
            // colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
            colTieuNhomDV.DataBindings.Add("Text", DataSource, "TieuNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");

            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            //colSoLo.DataBindings.Add("Text", DataSource, "SoLo");

            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
            colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGF.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];


            colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
            colNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGF.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];

            colSDTKSL.DataBindings.Add("Text", DataSource, "SDTKSL").FormatString = DungChung.Bien.FormatString[0];
            colSDTKTT.DataBindings.Add("Text", DataSource, "SDTKTT").FormatString = DungChung.Bien.FormatString[1];
            colSDTKTTGF.DataBindings.Add("Text", DataSource, "SDTKTT").FormatString = DungChung.Bien.FormatString[1];
            colSDTKTTTong.DataBindings.Add("Text", DataSource, "SDTKTT").FormatString = DungChung.Bien.FormatString[1];

            colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
            colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGF.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("TieuNhomDV"));
            //GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "06007")
                colTruongKD.Text = "PHÓ TRƯỞNG KHOA DƯỢC";
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            double _TT = 0;
            if (TongTien.Value != null)
                _TT =Convert.ToDouble(TongTien.Value);
            txtTienBangChu.Text = DungChung.Ham.DocTienBangChu(_TT, " đồng.");
        }

        //private void colSDTKSL_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _dongia = 0;
        //    DateTime tungay = System.DateTime.Now.Date;
        //    DateTime denngay = System.DateTime.Now.Date;
        //    tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
        //    denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("DonGia") != null)
        //        _dongia = int.Parse(this.GetCurrentColumnValue("DonGia").ToString());
        //    var qnxt = (from nhapd in data.NhapDs
        //                join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
        //                join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
        //                join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
        //                join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
        //                join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
        //                where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay)
        //                where (nhapd.PLoai == 2||nhapd.PLoai==5)
        //                group new {kp, nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { kp.PLoai,nhomdv.TenNhom, tieunhomdv.TenTN, dv.MaDV,nhapd.KieuDon, nhapdct.DonGia, nhapdct.SoLuongX, nhapdct.SoLuongSD } into kq
        //                select new
        //                {
        //                    PLoai=kq.Key.PLoai,
        //                    KieuDon=kq.Key.KieuDon,
        //                    MaDV = kq.Key.MaDV,
        //                    DonGia = kq.Key.DonGia,
        //                    SLX=kq.Key.SoLuongX,
        //                    SLSD = kq.Key.SoLuongSD,
        //                }).ToList();
        //    if (qnxt.Count > 0)
        //    {
        //        double a = Convert.ToDouble(qnxt.Where(p => p.KieuDon == 1).Where(p => p.MaDV == _madv).Where(p => p.DonGia == _dongia).Sum(p => p.SLX)
        //                 + qnxt.Where(p => p.KieuDon == 0).Where(p => p.MaDV == _madv).Where(p => p.DonGia == _dongia).Sum(p => p.SLX)
        //                 + qnxt.Where(p => p.KieuDon == 4).Where(p => p.MaDV == _madv).Where(p => p.DonGia == _dongia).Sum(p => p.SLX));
        //        double b = Convert.ToDouble(qnxt.Where(p => p.KieuDon == 1).Where(p => p.PLoai.Contains("Tủ trực")).Where(p => p.MaDV == _madv).Where(p => p.DonGia == _dongia).Sum(p => p.SLX));
        //        double c = Convert.ToDouble(qnxt.Where(p => p.MaDV == _madv).Where(p => p.DonGia == _dongia).Sum(p => p.SLSD));
        //        colSDTKSL.Text = (a + c - b).ToString();
        //    }
            
        //}
    }
}
