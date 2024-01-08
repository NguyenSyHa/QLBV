using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcNXTXuat_ct : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNXTXuat_ct()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
            colTenHamLuongGh1.DataBindings.Add("Text", DataSource, "TenTieuNhomDuoc");
            colTenHamLuong.DataBindings.Add("Text", DataSource, "TenHamLuong");

            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNoiTruSL.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString =  DungChung.Bien.FormatString[0];
            colXuatTKNoiTruTT.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString =  DungChung.Bien.FormatString[1];
            colXuatTKNoiTruSLGf1.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString =  DungChung.Bien.FormatString[0];
            colXuatTKNoiTruTTGf1.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString =  DungChung.Bien.FormatString[1];
            colXuatTKNoiTruSLGf2.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString =  DungChung.Bien.FormatString[0];
            colXuatTKNoiTruTTGf2.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString =  DungChung.Bien.FormatString[1];
            colXuatTKNoiTruTTTong.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString =  DungChung.Bien.FormatString[1];

            colXuatTKNgoaiTruSL.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString =  DungChung.Bien.FormatString[0];
            colXuatTKNgoaiTruTT.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString =  DungChung.Bien.FormatString[1];
            colXuatTKNgoaiTruSLGf1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString =  DungChung.Bien.FormatString[0];
            colXuatTKNgoaiTruTTGf1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString =  DungChung.Bien.FormatString[1];
            colXuatTKNgoaiTruSLGf2.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString =  DungChung.Bien.FormatString[0];
            colXuatTKNgoaiTruTTGf2.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString =  DungChung.Bien.FormatString[1];
            colXuatTKNgoaiTruTTTong.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString =  DungChung.Bien.FormatString[1];

            colSL3.DataBindings.Add("Text", DataSource, "SL3").FormatString =  DungChung.Bien.FormatString[0];
            colTT3.DataBindings.Add("Text", DataSource, "TT3").FormatString =  DungChung.Bien.FormatString[1];
            colSL4.DataBindings.Add("Text", DataSource, "SL4").FormatString =  DungChung.Bien.FormatString[0];
            colTT4.DataBindings.Add("Text", DataSource, "TT4").FormatString =  DungChung.Bien.FormatString[1];
            colTT4RF.DataBindings.Add("Text", DataSource, "TT4").FormatString =  DungChung.Bien.FormatString[1];
            colTT3RF.DataBindings.Add("Text", DataSource, "TT4").FormatString =  DungChung.Bien.FormatString[1];

            colXuatTKTongSL.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString =  DungChung.Bien.FormatString[0];
            colXuatTKTongTT.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString =  DungChung.Bien.FormatString[1];
            colXuatTKTongSLGf1.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString =  DungChung.Bien.FormatString[0];
            colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString =  DungChung.Bien.FormatString[1];
            colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString =  DungChung.Bien.FormatString[1];
            colXuatTKTongSLGh2.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString =  DungChung.Bien.FormatString[0];
            colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString =  DungChung.Bien.FormatString[1];


            GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhomDuoc"));
            GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            colTenCQ.Text = DungChung.Bien.TenCQ;
            GroupFooter1.Visible = false;
            GroupHeader1.Visible = false;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            //colThuKho.Text = DungChung.Bien.;
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
    }
}
