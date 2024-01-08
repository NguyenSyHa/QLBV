using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BcXuatTheoPLoai_BX : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcXuatTheoPLoai_BX()
        {
            InitializeComponent();
        }
        string _kho="";
        public Rep_BcXuatTheoPLoai_BX(string k)
        {
            InitializeComponent();
            _kho = k;
        }
        public void BindingData()
        {
            colTenHamLuong.DataBindings.Add("Text", DataSource, "TenDV");
          //  txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSLX1.DataBindings.Add("Text", DataSource, "SL1").FormatString = DungChung.Bien.FormatString[0];
            colSLX2.DataBindings.Add("Text", DataSource, "TT1").FormatString = DungChung.Bien.FormatString[1];
            colSLX3.DataBindings.Add("Text", DataSource, "SL2").FormatString = DungChung.Bien.FormatString[0];
            colSLX4.DataBindings.Add("Text", DataSource, "TT2").FormatString = DungChung.Bien.FormatString[1];
            colSLX5.DataBindings.Add("Text", DataSource, "SL3").FormatString = DungChung.Bien.FormatString[0];
            colSLX6.DataBindings.Add("Text", DataSource, "TT3").FormatString = DungChung.Bien.FormatString[1];
            colSLX7.DataBindings.Add("Text", DataSource, "SL4").FormatString = DungChung.Bien.FormatString[0];
            colSLX8.DataBindings.Add("Text", DataSource, "TT4").FormatString = DungChung.Bien.FormatString[1];
            colSLX9.DataBindings.Add("Text", DataSource, "SL5").FormatString = DungChung.Bien.FormatString[0];
            colSLX10.DataBindings.Add("Text", DataSource, "TT5").FormatString = DungChung.Bien.FormatString[1];
            colSLX11.DataBindings.Add("Text", DataSource, "SL6").FormatString = DungChung.Bien.FormatString[0];
            colSLX12.DataBindings.Add("Text", DataSource, "TT6").FormatString = DungChung.Bien.FormatString[1];
            colSLX13.DataBindings.Add("Text", DataSource, "SL7").FormatString = DungChung.Bien.FormatString[0];
            colSLX14.DataBindings.Add("Text", DataSource, "TT7").FormatString = DungChung.Bien.FormatString[1];
            colSLX15.DataBindings.Add("Text", DataSource, "SL8").FormatString = DungChung.Bien.FormatString[0];
            colSLX16.DataBindings.Add("Text", DataSource, "TT8").FormatString = DungChung.Bien.FormatString[1];
            colSLX19.DataBindings.Add("Text", DataSource, "SLT").FormatString = DungChung.Bien.FormatString[0];
            colTTX19.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];

            colSLXT2.DataBindings.Add("Text", DataSource, "TT1").FormatString = DungChung.Bien.FormatString[1];
            colSLXT4.DataBindings.Add("Text", DataSource, "TT2").FormatString = DungChung.Bien.FormatString[1];
            colSLXT6.DataBindings.Add("Text", DataSource, "TT3").FormatString = DungChung.Bien.FormatString[1];
            colSLXT8.DataBindings.Add("Text", DataSource, "TT4").FormatString = DungChung.Bien.FormatString[1];
            T5.DataBindings.Add("Text", DataSource, "TT5").FormatString = DungChung.Bien.FormatString[1];
            T6.DataBindings.Add("Text", DataSource, "TT6").FormatString = DungChung.Bien.FormatString[1];
            T7.DataBindings.Add("Text", DataSource, "TT7").FormatString = DungChung.Bien.FormatString[1];
            T8.DataBindings.Add("Text", DataSource, "TT8").FormatString = DungChung.Bien.FormatString[1];
            TT.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];

        }
             QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
       
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

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
        }
    }
}
