using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_TamTraThuoc : DevExpress.XtraReports.UI.XtraReport
    {
        int _mau = 0;
        public Rep_TamTraThuoc()
        {
            InitializeComponent();
        }
        public Rep_TamTraThuoc(int mau)
        {
            mau = _mau;
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
            colGiuong.DataBindings.Add("Text", DataSource, "Giuong");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colSL1.DataBindings.Add("Text", DataSource, "SL101");
            colSL2.DataBindings.Add("Text", DataSource, "SL201");
            colSL3.DataBindings.Add("Text", DataSource, "SL301");
            colSL4.DataBindings.Add("Text", DataSource, "SL401");
            colSL5.DataBindings.Add("Text", DataSource, "SL501");
            colSL6.DataBindings.Add("Text", DataSource, "SL601");
            colSL7.DataBindings.Add("Text", DataSource, "SL701");
            colSL8.DataBindings.Add("Text", DataSource, "SL801");
            colSL9.DataBindings.Add("Text", DataSource, "SL901");
            colSL10.DataBindings.Add("Text", DataSource, "SL1001");
            colSL12.DataBindings.Add("Text", DataSource, "Sl1201");
            colSL11.DataBindings.Add("Text", DataSource, "SL1101");
            colSL13.DataBindings.Add("Text", DataSource, "SL1301");
            colSL14.DataBindings.Add("Text", DataSource, "SL1401");
            colSL15.DataBindings.Add("Text", DataSource, "SL1501");
            colSL16.DataBindings.Add("Text", DataSource, "SL1601");
            colSL17.DataBindings.Add("Text", DataSource, "SL1701");
            colSL18.DataBindings.Add("Text", DataSource, "SL1801");
            colSL19.DataBindings.Add("Text", DataSource, "SL1901");
            colSL20.DataBindings.Add("Text", DataSource, "SL2001");
            colSL21.DataBindings.Add("Text", DataSource, "SL2101");
            colSL22.DataBindings.Add("Text", DataSource, "SL2201");
            colSL23.DataBindings.Add("Text", DataSource, "SL2301");
            colSL24.DataBindings.Add("Text", DataSource, "SL2401");
            colSL25.DataBindings.Add("Text", DataSource, "SL2501");
            colSL26.DataBindings.Add("Text",DataSource ,"SL2601");
            colSL27.DataBindings.Add("Text", DataSource, "SL2701");
            colSL28.DataBindings.Add("Text", DataSource, "SL2801");
            colSL29.DataBindings.Add("Text", DataSource, "SL2901");
            colSL30.DataBindings.Add("Text", DataSource, "SL3001");
            colSL31.DataBindings.Add("Text", DataSource, "SL3101");
            colSL32.DataBindings.Add("Text", DataSource, "SL3201");
            colSL33.DataBindings.Add("Text", DataSource, "SL3301");
            colSL34.DataBindings.Add("Text", DataSource, "SL3401");
            colSL35.DataBindings.Add("Text", DataSource, "SL3501");
            colSL36.DataBindings.Add("Text", DataSource, "SL3601");
            colSL37.DataBindings.Add("Text", DataSource, "SL3701");
            colSL38.DataBindings.Add("Text", DataSource, "SL3801");
            colSL39.DataBindings.Add("Text", DataSource, "SL3901");
            colSL40.DataBindings.Add("Text", DataSource, "SL4001");
            colSL41.DataBindings.Add("Text", DataSource, "SL4101");
            colSL42.DataBindings.Add("Text", DataSource, "SL4201");
            colSL43.DataBindings.Add("Text", DataSource, "SL4301");
            colSL44.DataBindings.Add("Text", DataSource, "SL4401");
            colSL45.DataBindings.Add("Text", DataSource, "SL4501");
            colSL46.DataBindings.Add("Text", DataSource, "SL4601");

            //colDVT1.DataBindings.Add("Text", DataSource, "DVT1");
            //colDVT2.DataBindings.Add("Text", DataSource, "DVT2");
            //colDVT3.DataBindings.Add("Text", DataSource, "DVT3");
            //colDVT4.DataBindings.Add("Text", DataSource, "DVT4");
            //colDVT5.DataBindings.Add("Text", DataSource, "DVT5");
            //colDVT6.DataBindings.Add("Text", DataSource, "DVT6");
            //colDVT7.DataBindings.Add("Text", DataSource, "DVT7");
            //colDVT8.DataBindings.Add("Text", DataSource, "DVT8");
            //colDVT9.DataBindings.Add("Text", DataSource, "DVT9");
            //colDVT10.DataBindings.Add("Text", DataSource, "DVT10");
            //colDVT11.DataBindings.Add("Text", DataSource, "DVT11");
            //colDVT12.DataBindings.Add("Text", DataSource, "DVT12");
            //colDVT13.DataBindings.Add("Text", DataSource, "DVT13");
            //colDVT14.DataBindings.Add("Text", DataSource, "DVT14");
            //colDVT15.DataBindings.Add("Text", DataSource, "DVT15");
            //colDVT16.DataBindings.Add("Text", DataSource, "DVT16");
            //colDVT17.DataBindings.Add("Text", DataSource, "DVT17");
            //colDVT18.DataBindings.Add("Text", DataSource, "DVT18");
            //colDVT19.DataBindings.Add("Text", DataSource, "DVT19");
            //colDVT20.DataBindings.Add("Text", DataSource, "DVT20");
            //colDVT21.DataBindings.Add("Text", DataSource, "DVT21");
            //colDVT22.DataBindings.Add("Text", DataSource, "DVT22");
            //colDVT23.DataBindings.Add("Text", DataSource, "DVT23");
            //colDVT24.DataBindings.Add("Text", DataSource, "DVT24");
            //colDVT25.DataBindings.Add("Text", DataSource, "DVT25");
            //colDVT26.DataBindings.Add("Text", DataSource, "DVT26");
            //colDVT27.DataBindings.Add("Text", DataSource, "DVT27");
            //colDVT28.DataBindings.Add("Text", DataSource, "DVT28");
            //colDVT29.DataBindings.Add("Text", DataSource, "DVT29");
            //colDVT30.DataBindings.Add("Text", DataSource, "DVT30");
            //colDVT31.DataBindings.Add("Text", DataSource, "DVT31");
            //colDVT32.DataBindings.Add("Text", DataSource, "DVT32");
            //colDVT33.DataBindings.Add("Text", DataSource, "DVT33");
            //colDVT34.DataBindings.Add("Text", DataSource, "DVT34");
            //colDVT35.DataBindings.Add("Text", DataSource, "DVT35");
            //colDVT36.DataBindings.Add("Text", DataSource, "DVT36");
            //colDVT37.DataBindings.Add("Text", DataSource, "DVT37");
            //colDVT38.DataBindings.Add("Text", DataSource, "DVT38");
            //colDVT39.DataBindings.Add("Text", DataSource, "DVT39");
            //colDVT40.DataBindings.Add("Text", DataSource, "DVT40");
            //colDVT41.DataBindings.Add("Text", DataSource, "DVT41");
            //colDVT42.DataBindings.Add("Text", DataSource, "DVT42");
            //colDVT43.DataBindings.Add("Text", DataSource, "DVT43");
            //colDVT44.DataBindings.Add("Text", DataSource, "DVT44");
            //colDVT45.DataBindings.Add("Text", DataSource, "DVT45");
            //colDVT46.DataBindings.Add("Text", DataSource, "DVT46");
            //colSL47.DataBindings.Add("Text", DataSource, "SL3801");
            //colSL48.DataBindings.Add("Text", DataSource, "SL3801");
            //colSL49.DataBindings.Add("Text", DataSource, "SL3801");
            //colSL38.DataBindings.Add("Text", DataSource, "SL3801");
            //colSL38.DataBindings.Add("Text", DataSource, "SL3801");
            //colSL38.DataBindings.Add("Text", DataSource, "SL3801");
            //colSL38.DataBindings.Add("Text", DataSource, "SL3801");
            //colSL38.DataBindings.Add("Text", DataSource, "SL3801");
            //colSL39.DataBindings.Add("Text", DataSource, "SL3901");
            colT1.DataBindings.Add("Text", DataSource, "SL101");
            colT2.DataBindings.Add("Text", DataSource, "SL201");
            colT3.DataBindings.Add("Text", DataSource, "SL301");
            colT4.DataBindings.Add("Text", DataSource, "SL401");
            colT5.DataBindings.Add("Text", DataSource, "SL501");
            colT6.DataBindings.Add("Text", DataSource, "SL601");
            colT7.DataBindings.Add("Text", DataSource, "SL701");
            colT8.DataBindings.Add("Text", DataSource, "SL801");
            colT9.DataBindings.Add("Text", DataSource, "SL901");
            colT10.DataBindings.Add("Text", DataSource, "SL1001");
            colT11.DataBindings.Add("Text", DataSource, "SL1101");
            colT12.DataBindings.Add("Text", DataSource, "SL1201");
            colT13.DataBindings.Add("Text", DataSource, "SL1301");
            colT14.DataBindings.Add("Text", DataSource, "SL1401");
            colT15.DataBindings.Add("Text", DataSource, "SL1501");
            colT16.DataBindings.Add("Text", DataSource, "SL1601");
            colT17.DataBindings.Add("Text", DataSource, "SL1701");
            colT18.DataBindings.Add("Text", DataSource, "SL1801");
            colT19.DataBindings.Add("Text", DataSource, "SL1901");
            colT20.DataBindings.Add("Text", DataSource, "SL2001");
            colT21.DataBindings.Add("Text", DataSource, "SL2101");
            colT22.DataBindings.Add("Text", DataSource, "SL2201");
            colT23.DataBindings.Add("Text", DataSource, "SL2301");
            colT24.DataBindings.Add("Text", DataSource, "SL2401");
            colT25.DataBindings.Add("Text", DataSource, "SL2501");
            colT26.DataBindings.Add("Text", DataSource, "SL2601");
            colT27.DataBindings.Add("Text", DataSource, "SL2701");
            colT28.DataBindings.Add("Text", DataSource, "SL2801");
            colT29.DataBindings.Add("Text", DataSource, "SL2901");
            colT30.DataBindings.Add("Text", DataSource, "SL3001");
            colT31.DataBindings.Add("Text", DataSource, "SL3101");
            colT32.DataBindings.Add("Text", DataSource, "SL3201");
            colT33.DataBindings.Add("Text", DataSource, "SL3301");
            colT34.DataBindings.Add("Text", DataSource, "SL3401");
            colT35.DataBindings.Add("Text", DataSource, "SL3501");
            colT36.DataBindings.Add("Text", DataSource, "SL3601");
            colT37.DataBindings.Add("Text", DataSource, "SL3701");
            colT38.DataBindings.Add("Text", DataSource, "SL3801");
            colT39.DataBindings.Add("Text", DataSource, "SL3901");
            colT40.DataBindings.Add("Text", DataSource, "SL4001");
            colT41.DataBindings.Add("Text", DataSource, "SL4101");
            colT42.DataBindings.Add("Text", DataSource, "SL4201");
            colT43.DataBindings.Add("Text", DataSource, "SL4301");
            colT44.DataBindings.Add("Text", DataSource, "SL4401");
            colT45.DataBindings.Add("Text", DataSource, "SL4501");
            colT46.DataBindings.Add("Text", DataSource, "SL4601");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtSoty.Text = txtSoty2.Text = DungChung.Bien.TenCQCQ;
            txtBV.Text = txtBV2.Text = DungChung.Bien.TenCQ;
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30009")
            {
                colDDCS.Visible = true;
                colYTHC.Text = "Điều dưỡng hành chính";
            }
            if(_mau == 1)
            {
                xrLabel1.Text = "BIỂU CÔNG KHAI THUỐC HÀNG NGÀY";
            }
            else
            {
                xrLabel1.Text = "SỔ TỔNG HỢP THUỐC HÀNG NGÀY";
            }
        }

    }
}
