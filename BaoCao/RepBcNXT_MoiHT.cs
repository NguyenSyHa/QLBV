using System;using QLBV_Database;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace QLBV.BaoCao
{
    public partial class RepBcNXT_MoiHT : DevExpress.XtraReports.UI.XtraReport
    {
        public RepBcNXT_MoiHT()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
            //colTenHamLuongGh1.DataBindings.Add("Text", DataSource, "TenTieuNhomDuoc");
            colTenHamLuong.DataBindings.Add("Text", DataSource, "TenHamLuong");

            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            //colSoLo.DataBindings.Add("Text", DataSource, "SoLo");

            //txtTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL");
            //txtTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT");
            colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKSLGf1.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGf1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKSLGf2.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGf2.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
           

            //txtNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL");
            //txtNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT");
            colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTde.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKSLGf1.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGf1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKSLGf2.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGf2.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];

            //txtXuatNTSL.DataBindings.Add("Text", DataSource, "XuatNoiTruSL");
            //txtXuatNTTT.DataBindings.Add("Text", DataSource, "XuatNoiTruTT");
            colXuatTKNoiTruSL.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNoiTruTT.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNoiTruSLGf1.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNoiTruTTGf1.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNoiTruSLGf2.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNoiTruTTGf2.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNoiTruTTTong.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];

            //txtXuatNgTSL.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL");
            //txtXuatNgTTT.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT");
            colXuatTKNgoaiTruSL.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgoaiTruTT.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgoaiTruSLGf1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgoaiTruTTGf1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgoaiTruSLGf2.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgoaiTruTTGf2.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgoaiTruTTTong.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];

            //txtXuatTongSL.DataBindings.Add("Text", DataSource, "XuatTKTongSL");
            //txtXuatTongTT.DataBindings.Add("Text", DataSource, "XuatTKTongTT");
            colXuatTKTongSL.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTT.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString=DungChung.Bien.FormatString[1];
            colXuatTKTongSLGf1.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongSLGh2.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];

            //txtTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL");
            //txtTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT");
            colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKSLGf1.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKSLGf2.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf2.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            //GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhomDuoc"));
            GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
        }
                       

        private void TopMargin_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ;
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;
            
        }
        int sttGh2 = 1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (sttGh2)
            {
                case 1:
                    colSTTGh2.Text = "I";
                    break;
                case 2:
                    colSTTGh2.Text = "II";
                    break;
                case 3:
                    colSTTGh2.Text = "III";
                    break;
                case 4:
                    colSTTGh2.Text = "IV";
                    break;
                case 5:
                    colSTTGh2.Text = "IV";
                    break;

            }
            sttGh2++;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            //colThuKho.Text = DungChung.Bien.ThuKho;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (GetCurrentColumnValue("XuatTKTongTT") != null && GetCurrentColumnValue("XuatTKTongTT").ToString()== "0")
                
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            GroupHeader1.Visible = false;
            GroupFooter1.Visible = false;
        }

    }
}
