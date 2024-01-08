using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_TamTraThuocA302 : DevExpress.XtraReports.UI.XtraReport
    {
        int _mau = 0;
        public Rep_TamTraThuocA302()
        {
            InitializeComponent();
        }
        public Rep_TamTraThuocA302(int mau)
        {
            mau = _mau;
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colSL1.DataBindings.Add("Text", DataSource, "SL5501");
            colSL2.DataBindings.Add("Text", DataSource, "SL5601");
            colSL3.DataBindings.Add("Text", DataSource, "SL5701");
            colSL4.DataBindings.Add("Text", DataSource, "SL5801");
            colSL5.DataBindings.Add("Text", DataSource, "SL5901");
            colSL6.DataBindings.Add("Text", DataSource, "SL6001");
            colSL7.DataBindings.Add("Text", DataSource, "SL6101");
            colSL8.DataBindings.Add("Text", DataSource, "SL6201");
            colSL9.DataBindings.Add("Text", DataSource, "SL6301");
            colSL10.DataBindings.Add("Text", DataSource, "SL6401");
            colSL12.DataBindings.Add("Text", DataSource, "Sl6501");
            colSL11.DataBindings.Add("Text", DataSource, "SL6601");
            colSL13.DataBindings.Add("Text", DataSource, "SL6701");
            colSL14.DataBindings.Add("Text", DataSource, "SL6801");
            colSL15.DataBindings.Add("Text", DataSource, "SL6901");
            colSL16.DataBindings.Add("Text", DataSource, "SL7001");
            colSL17.DataBindings.Add("Text", DataSource, "SL7101");
            colSL18.DataBindings.Add("Text", DataSource, "SL7201");
            colSL19.DataBindings.Add("Text", DataSource, "SL7301");
            colSL20.DataBindings.Add("Text", DataSource, "SL7401");
            colSL21.DataBindings.Add("Text", DataSource, "SL7501");
            colSL22.DataBindings.Add("Text", DataSource, "SL7601");
            colSL23.DataBindings.Add("Text", DataSource, "SL7701");
            colSL24.DataBindings.Add("Text", DataSource, "SL7801");
            colSL25.DataBindings.Add("Text", DataSource, "SL7901");
            colSL26.DataBindings.Add("Text", DataSource, "SL8001");
            colSL27.DataBindings.Add("Text", DataSource, "SL8101");
            colSL28.DataBindings.Add("Text", DataSource, "SL8201");
            colSL29.DataBindings.Add("Text", DataSource, "SL8301");
            colSL30.DataBindings.Add("Text", DataSource, "SL8401");
            colSL31.DataBindings.Add("Text", DataSource, "SL8501");
            colSL32.DataBindings.Add("Text", DataSource, "SL8601");
            colSL33.DataBindings.Add("Text", DataSource, "SL8701");
            colSL34.DataBindings.Add("Text", DataSource, "SL8801");
            colSL35.DataBindings.Add("Text", DataSource, "SL8901");
            colSL36.DataBindings.Add("Text", DataSource, "SL9001");
            colSL37.DataBindings.Add("Text", DataSource, "SL9101");
            colSL38.DataBindings.Add("Text", DataSource, "SL9201");
            colSL39.DataBindings.Add("Text", DataSource, "SL9301");
            colSL40.DataBindings.Add("Text", DataSource, "SL9401");
            colSL41.DataBindings.Add("Text", DataSource, "SL9501");
            colSL42.DataBindings.Add("Text", DataSource, "SL9601");
            colSL43.DataBindings.Add("Text", DataSource, "SL9701");
            coLSL44.DataBindings.Add("Text", DataSource, "SL9801");
            colSL45.DataBindings.Add("Text", DataSource, "SL9901");
            colSL46.DataBindings.Add("Text", DataSource, "SL10001");
            colSL47.DataBindings.Add("Text", DataSource, "SL10101");
            colSL48.DataBindings.Add("Text", DataSource, "SL10201");
            colSL49.DataBindings.Add("Text", DataSource, "SL10301");
            colSL50.DataBindings.Add("Text", DataSource, "SL10401");
            colSL51.DataBindings.Add("Text", DataSource, "SL10501");
            colSL52.DataBindings.Add("Text", DataSource, "SL10601");
            colSL53.DataBindings.Add("Text", DataSource, "SL10701");
            colSL54.DataBindings.Add("Text", DataSource, "SL10801");

            colT1.DataBindings.Add("Text", DataSource, "SL5501");
            colT2.DataBindings.Add("Text", DataSource, "SL5601");
            colT3.DataBindings.Add("Text", DataSource, "SL5701");
            colT4.DataBindings.Add("Text", DataSource, "SL5801");
            colT5.DataBindings.Add("Text", DataSource, "SL5901");
            colT6.DataBindings.Add("Text", DataSource, "SL6001");
            colT7.DataBindings.Add("Text", DataSource, "SL6101");
            colT8.DataBindings.Add("Text", DataSource, "SL6201");
            colT9.DataBindings.Add("Text", DataSource, "SL6301");
            colT10.DataBindings.Add("Text", DataSource, "SL6401");
            colT11.DataBindings.Add("Text", DataSource, "Sl6501");
            colT12.DataBindings.Add("Text", DataSource, "SL6601");
            colT13.DataBindings.Add("Text", DataSource, "SL6701");
            colT14.DataBindings.Add("Text", DataSource, "SL6801");
            colT15.DataBindings.Add("Text", DataSource, "SL6901");
            colT16.DataBindings.Add("Text", DataSource, "SL7001");
            colT17.DataBindings.Add("Text", DataSource, "SL7101");
            colT18.DataBindings.Add("Text", DataSource, "SL7201");
            colT19.DataBindings.Add("Text", DataSource, "SL7301");
            colT20.DataBindings.Add("Text", DataSource, "SL7401");
            colT21.DataBindings.Add("Text", DataSource, "SL7501");
            colT22.DataBindings.Add("Text", DataSource, "SL7601");
            colT23.DataBindings.Add("Text", DataSource, "SL7701");
            colT24.DataBindings.Add("Text", DataSource, "SL7801");
            colT25.DataBindings.Add("Text", DataSource, "SL7901");
            colT26.DataBindings.Add("Text", DataSource, "SL8001");
            colT27.DataBindings.Add("Text", DataSource, "SL8101");
            colT28.DataBindings.Add("Text", DataSource, "SL8201");
            colT29.DataBindings.Add("Text", DataSource, "SL8301");
            colT30.DataBindings.Add("Text", DataSource, "SL8401");
            colT31.DataBindings.Add("Text", DataSource, "SL8501");
            colT32.DataBindings.Add("Text", DataSource, "SL8601");
            colT33.DataBindings.Add("Text", DataSource, "SL8701");
            colT34.DataBindings.Add("Text", DataSource, "SL8801");
            colT35.DataBindings.Add("Text", DataSource, "SL8901");
            colT36.DataBindings.Add("Text", DataSource, "SL9001");
            colT37.DataBindings.Add("Text", DataSource, "SL9101");
            colT38.DataBindings.Add("Text", DataSource, "SL9201");
            colT39.DataBindings.Add("Text", DataSource, "SL9301");
            colT40.DataBindings.Add("Text", DataSource, "SL9401");
            colT41.DataBindings.Add("Text", DataSource, "SL9501");
            colT42.DataBindings.Add("Text", DataSource, "SL9601");
            colT43.DataBindings.Add("Text", DataSource, "SL9701");
            colT44.DataBindings.Add("Text", DataSource, "SL9801");
            colT45.DataBindings.Add("Text", DataSource, "SL9901");
            colT46.DataBindings.Add("Text", DataSource, "SL10001");
            colT47.DataBindings.Add("Text", DataSource, "SL10101");
            colT48.DataBindings.Add("Text", DataSource, "SL10201");
            colT49.DataBindings.Add("Text", DataSource, "SL10301");
            colT50.DataBindings.Add("Text", DataSource, "SL10401");
            colT51.DataBindings.Add("Text", DataSource, "SL10501");
            colT52.DataBindings.Add("Text", DataSource, "SL10601");
            colT53.DataBindings.Add("Text", DataSource, "SL10701");
            colT54.DataBindings.Add("Text", DataSource, "SL10801");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_mau == 1)
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
