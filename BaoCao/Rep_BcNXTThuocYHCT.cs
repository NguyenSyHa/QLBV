using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BcNXTThuocYHCT : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcNXTThuocYHCT()
        {
            InitializeComponent();
        }
        public  void BindingData()
        {
                colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TieuNhom");

            colTenHamLuong.DataBindings.Add("Text", DataSource, "TenDV");

            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colTTNhap.DataBindings.Add("Text", DataSource, "TTNhap");
            colNguonGoc.DataBindings.Add("Text", DataSource, "NguonGoc");
            colBPSD.DataBindings.Add("Text", DataSource, "BPSD");
            colYCSD.DataBindings.Add("Text", DataSource, "YCSD");

            colDonGia.DataBindings.Add("Text", DataSource, "DonGiaDY").FormatString = "{0:##,###.###}";
            colDonGiaX.DataBindings.Add("Text", DataSource, "DonGia").FormatString = "{0:##,###.###}";
            //colSoLo.DataBindings.Add("Text", DataSource, "SoLo");

            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
           // colTonDKSLGf2.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGf2.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];


            colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTde.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
           // colNhapTKSLGf2.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGf2.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];

            colXuatTKTongSL.DataBindings.Add("Text", DataSource, "XuatTKSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTT.DataBindings.Add("Text", DataSource, "XuatTKTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "XuatTKTT").FormatString = DungChung.Bien.FormatString[1];
          //  colXuatTKTongSLGh2.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "XuatTKTT").FormatString = DungChung.Bien.FormatString[1];

            colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
             // colTonCKSLGf2.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf2.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhomDuoc"));
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }
             QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
   
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
         
            }
        

    }
}
