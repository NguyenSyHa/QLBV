using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcNXT_12122 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcNXT_12122()
        {
            InitializeComponent();

        }
      
       
        public void BindingData()
        {
            if (DungChung.Bien.MaBV == "30003")
            {
                xrTable1.Visible = false;
                xrTable8.Visible = true;
                colTenKho.DataBindings.Add("Text", DataSource, "TenKP");
                // colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
                colTieuNhomDV.DataBindings.Add("Text", DataSource, "TenTN");
                tenDV.DataBindings.Add("Text", DataSource, "TenDV");
                MaNB.DataBindings.Add("Text", DataSource, "MaTam");
                DVT.DataBindings.Add("Text", DataSource, "DVT");
                DonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                //colSoLo.DataBindings.Add("Text", DataSource, "SoLo");

                TonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                TonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];


                NhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                NhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];

                NhapTraSL.DataBindings.Add("Text", DataSource, "NhapTraLaiSL").FormatString = DungChung.Bien.FormatString[0];
                NhapTraTT.DataBindings.Add("Text", DataSource, "NhapTraLaiTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTraTT_RF.DataBindings.Add("Text", DataSource, "NhapTraLaiTT").FormatString = DungChung.Bien.FormatString[1];

                XuatTKSL.DataBindings.Add("Text", DataSource, "TongXuatSL").FormatString = DungChung.Bien.FormatString[0];
                XuatTKTT.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTTTong.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];

                TonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
                TonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            }
            else
            {
                xrTable8.Visible = false;
                xrTable1.Visible = true;
                colTenKho.DataBindings.Add("Text", DataSource, "TenKP");
                // colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
                colTieuNhomDV.DataBindings.Add("Text", DataSource, "TenTN");
                colTenDV.DataBindings.Add("Text", DataSource, "TenDV");

                colDVT.DataBindings.Add("Text", DataSource, "DVT");
                colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                //colSoLo.DataBindings.Add("Text", DataSource, "SoLo");

                colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];


                colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                colNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];

                colNhapTraSL.DataBindings.Add("Text", DataSource, "NhapTraLaiSL").FormatString = DungChung.Bien.FormatString[0];
                colNhapTraTT.DataBindings.Add("Text", DataSource, "NhapTraLaiTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTraTT_RF.DataBindings.Add("Text", DataSource, "NhapTraLaiTT").FormatString = DungChung.Bien.FormatString[1];

                colXuatTKSL.DataBindings.Add("Text", DataSource, "TongXuatSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKTT.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTTTong.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];

                colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
                colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            }
            
            //GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            if (DungChung.Bien.MaBV == "30003")
            {
                SubBand3.Visible = false;
            }
            else
            {
                SubBand1.Visible = false;
                SubBand4.Visible = false;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            xrThuKho.Text = DungChung.Bien.ThuKho;
        }

   

    }
}
