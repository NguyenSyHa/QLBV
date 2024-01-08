using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCXuatTheoPLoai_A3 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCXuatTheoPLoai_A3()
        {
            InitializeComponent();
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
            colSLX17.DataBindings.Add("Text", DataSource, "SL9").FormatString = DungChung.Bien.FormatString[0];
            colSLX18.DataBindings.Add("Text", DataSource, "TT9").FormatString = DungChung.Bien.FormatString[0];
            colSLX19.DataBindings.Add("Text", DataSource, "SL10").FormatString = DungChung.Bien.FormatString[0];
            colSLX20.DataBindings.Add("Text", DataSource, "TT10").FormatString = DungChung.Bien.FormatString[0];
            colSLX21.DataBindings.Add("Text", DataSource, "SL11").FormatString = DungChung.Bien.FormatString[0];
            colSLX22.DataBindings.Add("Text", DataSource, "TT11").FormatString = DungChung.Bien.FormatString[0];
            colSLX23.DataBindings.Add("Text", DataSource, "SL12").FormatString = DungChung.Bien.FormatString[0];
            colSLX24.DataBindings.Add("Text", DataSource, "TT12").FormatString = DungChung.Bien.FormatString[0];
            colSLX25.DataBindings.Add("Text", DataSource, "SL13").FormatString = DungChung.Bien.FormatString[0];
            colSLX26.DataBindings.Add("Text", DataSource, "TT10").FormatString = DungChung.Bien.FormatString[0];
            colSLX27.DataBindings.Add("Text", DataSource, "SL").FormatString = DungChung.Bien.FormatString[0];
            colSLX28.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[0];

            colSLXT2.DataBindings.Add("Text", DataSource, "TT1").FormatString = DungChung.Bien.FormatString[1];
            colSLXT4.DataBindings.Add("Text", DataSource, "TT2").FormatString = DungChung.Bien.FormatString[1];
            colSLXT6.DataBindings.Add("Text", DataSource, "TT3").FormatString = DungChung.Bien.FormatString[1];
            colSLXT8.DataBindings.Add("Text", DataSource, "TT4").FormatString = DungChung.Bien.FormatString[1];
            colSLXT10.DataBindings.Add("Text", DataSource, "TT5").FormatString = DungChung.Bien.FormatString[1];
            colSLXT12.DataBindings.Add("Text", DataSource, "TT6").FormatString = DungChung.Bien.FormatString[1];
            colSLXT14.DataBindings.Add("Text", DataSource, "TT7").FormatString = DungChung.Bien.FormatString[1];
            colSLXT16.DataBindings.Add("Text", DataSource, "TT8").FormatString = DungChung.Bien.FormatString[1];
            colSLXT18.DataBindings.Add("Text", DataSource, "TT9").FormatString = DungChung.Bien.FormatString[1];
            colSLXT20.DataBindings.Add("Text", DataSource, "TT10").FormatString = DungChung.Bien.FormatString[1];
            colSLXT22.DataBindings.Add("Text", DataSource, "TT11").FormatString = DungChung.Bien.FormatString[1];
            colSLXT24.DataBindings.Add("Text", DataSource, "TT12").FormatString = DungChung.Bien.FormatString[1];
            colSLXT26.DataBindings.Add("Text", DataSource, "TT13").FormatString = DungChung.Bien.FormatString[1];
            colSLXT28.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];

            //T5.DataBindings.Add("Text", DataSource, "TT5").FormatString = DungChung.Bien.FormatString[1];
            //T6.DataBindings.Add("Text", DataSource, "TT6").FormatString = DungChung.Bien.FormatString[1];
            //T7.DataBindings.Add("Text", DataSource, "TT7").FormatString = DungChung.Bien.FormatString[1];
            //T8.DataBindings.Add("Text", DataSource, "TT8").FormatString = DungChung.Bien.FormatString[1];
            //TT.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ;
            colTenCQ.Text = DungChung.Bien.TenCQCQ;
        }
    }
}
