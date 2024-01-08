using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BcKKvaSDThuoc_BLac : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcKKvaSDThuoc_BLac()
        {
            InitializeComponent();
        }
        bool HThi = true;
        public Rep_BcKKvaSDThuoc_BLac(bool ht)
        {
            InitializeComponent();
            HThi = ht;
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
    
        public void BindingData()
        {
            colTieuNhomDV.DataBindings.Add("Text", DataSource, "TieuNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");

            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
    
            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
            colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGF.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];


            colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
            colNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGF.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];

            colSL1.DataBindings.Add("Text", DataSource, "SL1").FormatString = DungChung.Bien.FormatString[0];
            colTT1.DataBindings.Add("Text", DataSource, "TT1").FormatString = DungChung.Bien.FormatString[1];
            colTT1c.DataBindings.Add("Text", DataSource, "TT1").FormatString = DungChung.Bien.FormatString[1];
            colTT1tc.DataBindings.Add("Text", DataSource, "TT1").FormatString = DungChung.Bien.FormatString[1];

            colSL2.DataBindings.Add("Text", DataSource, "SL2").FormatString = DungChung.Bien.FormatString[0];
            colTT2.DataBindings.Add("Text", DataSource, "TT2").FormatString = DungChung.Bien.FormatString[1];
            colTT2c.DataBindings.Add("Text", DataSource, "TT2").FormatString = DungChung.Bien.FormatString[1];
            colTT2tc.DataBindings.Add("Text", DataSource, "TT2").FormatString = DungChung.Bien.FormatString[1];
            
            colSL3.DataBindings.Add("Text", DataSource, "SL3").FormatString = DungChung.Bien.FormatString[0];
            colTT3.DataBindings.Add("Text", DataSource, "TT3").FormatString = DungChung.Bien.FormatString[1];
            colTT3c.DataBindings.Add("Text", DataSource, "TT3").FormatString = DungChung.Bien.FormatString[1];
            colTT3tc.DataBindings.Add("Text", DataSource, "TT3").FormatString = DungChung.Bien.FormatString[1];
            
            colSL4.DataBindings.Add("Text", DataSource, "SL4").FormatString = DungChung.Bien.FormatString[0];
            colTT4.DataBindings.Add("Text", DataSource, "TT4").FormatString = DungChung.Bien.FormatString[1];
            colTT4c.DataBindings.Add("Text", DataSource, "TT4").FormatString = DungChung.Bien.FormatString[1];
            colTT4tc.DataBindings.Add("Text", DataSource, "TT4").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("TieuNhomDV"));
            //GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell8.Text = "PHÓ TRƯỞNG KHOA PHÒNG KHÁM_DƯỢC_CLS";
                colKhoaDuoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 20, 0);
                colGiamDoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 20, 0);
                colKeToanTruong.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 20, 0);
                colNguoiLapBang.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 20, 0);
            }



            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
          
        }

       
    }
}
