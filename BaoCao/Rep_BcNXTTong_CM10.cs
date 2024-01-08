using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BcNXTTong_CM10 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcNXTTong_CM10()
        {
            InitializeComponent();
        }
        bool HThi = false;
        public Rep_BcNXTTong_CM10(bool ht)
        {
            InitializeComponent();
            HThi = ht;
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colTenTieuNhomDV.DataBindings.Add("Text", DataSource, "TenTieuNhomDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            //colXuatNoiTTT.XlsxFormatString = DungChung.Bien.FormatString[1];

            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGF.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];


            colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGF.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];

            //colXuatNoiTSL.DataBindings.Add("Text", DataSource, "XuatNoiTSL").FormatString = DungChung.Bien.FormatString[1];
            //colXuatNoiTTT.DataBindings.Add("Text", DataSource, "XuatNoiTTT").FormatString = DungChung.Bien.FormatString[1];
            //colXuatNoiTTKSLTong.DataBindings.Add("Text", DataSource, "XuatTT").FormatString = DungChung.Bien.FormatString[1];

            colXuatKhoSL.DataBindings.Add("Text", DataSource, "XuatKhoSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatKhoTT.DataBindings.Add("Text", DataSource, "XuatKhoTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatKhoTTGF.DataBindings.Add("Text", DataSource, "XuatKhoTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatKhoTTTong.DataBindings.Add("Text", DataSource, "XuatKhoTT").FormatString = DungChung.Bien.FormatString[1];

            colXuatXaSL.DataBindings.Add("Text", DataSource, "XuatXaSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatXaTT.DataBindings.Add("Text", DataSource, "XuatXaTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatXaTTGF.DataBindings.Add("Text", DataSource, "XuatXaTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatXaTTTong.DataBindings.Add("Text", DataSource, "XuatXaTT").FormatString = DungChung.Bien.FormatString[1];

            colTongXuatSL.DataBindings.Add("Text", DataSource, "TongXuatSL").FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTT.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTTGF.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTTTong.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];

            colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGF.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            colXuatKhacSL.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatKhacTT.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatKhacTTGF.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatKhacTTTong.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhomDV"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            GroupHeader1.Visible = HThi;
            GroupFooter1.Visible = HThi;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        private void colXuatXaSL_BeforePrint(object sender, CancelEventArgs e)
        {
        //    DateTime tungay = System.DateTime.Now.Date;
        //    DateTime denngay = System.DateTime.Now.Date;
        //    tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
        //    denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
        //    int _madv = 0;
        //    int _makp = 0;
        //    int _dongia = 0;
        //    _makp = MaKP.Value.ToString();
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("DonGia") != null)
        //        _dongia = int.Parse(this.GetCurrentColumnValue("DonGia").ToString());
        //    var qnxt = (from nhapd in data.NhapDs.Where(p=>p.PLoai==2).Where(p=>p.MaKP==_makp)
        //                join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
        //                join kp in data.KPhongs on nhapd.MaKPnx equals kp.MaKP
        //                join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
        //                //join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
        //                //join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
        //                where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay && nhapd.PLoai == 2 && nhapd.KieuDon == 1)
        //                group new { kp,dv, nhapd, nhapdct } by new { dv.MaDV,nhapdct.DonGia, nhapdct.SoLuongX } into kq
        //                select new
        //                {
        //                        SoLuongX = kq.Key.SoLuongX,
        //                }).ToList();
        //    if (qnxt.Count > 0)
        //    {
        //        double a = Convert.ToDouble(qnxt.Where(p
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if ((GetCurrentColumnValue("TonDKSL") != null && GetCurrentColumnValue("TonDKSL").ToString() != "" && Convert.ToDouble(GetCurrentColumnValue("TonDKSL")) > 0) || (GetCurrentColumnValue("NhapTKSL") != null && GetCurrentColumnValue("NhapTKSL").ToString() != "" && Convert.ToDouble(GetCurrentColumnValue("NhapTKSL")) > 0))
            //{
            //    xrTable1.Visible = true;
            //    xrLine1.Visible = true;
            //}
            //else
            //{
            //    xrTable1.Visible = false;
            //    xrLine1.Visible = false;
            //}
        }

    }
}
