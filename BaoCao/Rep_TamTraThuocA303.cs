using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_TamTraThuocA303 : DevExpress.XtraReports.UI.XtraReport
    {
        int _mau = 0;
        public Rep_TamTraThuocA303()
        {
            InitializeComponent();
        }
        public Rep_TamTraThuocA303(int mau)
        {
            mau = _mau;
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colSL1.DataBindings.Add("Text", DataSource, "SL10901");
            colSL2.DataBindings.Add("Text", DataSource, "SL11001");
            colSL3.DataBindings.Add("Text", DataSource, "SL11101");
            colSL4.DataBindings.Add("Text", DataSource, "SL11201");
            colSL5.DataBindings.Add("Text", DataSource, "SL11301");
            colSL6.DataBindings.Add("Text", DataSource, "SL11401");
            colSL7.DataBindings.Add("Text", DataSource, "SL11501");
            colSL8.DataBindings.Add("Text", DataSource, "SL11601");
            colSL9.DataBindings.Add("Text", DataSource, "SL11701");
            colSL10.DataBindings.Add("Text", DataSource, "SL11801");
            colSL12.DataBindings.Add("Text", DataSource, "Sl11901");
            colSL11.DataBindings.Add("Text", DataSource, "SL12001");
            colSL13.DataBindings.Add("Text", DataSource, "SL12101");
            colSL14.DataBindings.Add("Text", DataSource, "SL12201");
            colSL15.DataBindings.Add("Text", DataSource, "SL12301");
            colSL16.DataBindings.Add("Text", DataSource, "SL12401");
            colSL17.DataBindings.Add("Text", DataSource, "SL12501");
            colSL18.DataBindings.Add("Text", DataSource, "SL12601");
            colSL19.DataBindings.Add("Text", DataSource, "SL12701");
            colSL20.DataBindings.Add("Text", DataSource, "SL12801");
            colSL21.DataBindings.Add("Text", DataSource, "SL12901");
            colSL22.DataBindings.Add("Text", DataSource, "SL13001");
            colSL23.DataBindings.Add("Text", DataSource, "SL13101");
            colSL24.DataBindings.Add("Text", DataSource, "SL13201");
            colSL25.DataBindings.Add("Text", DataSource, "SL13301");
            colSL26.DataBindings.Add("Text", DataSource, "SL13401");
            colSL27.DataBindings.Add("Text", DataSource, "SL13501");
            colSL28.DataBindings.Add("Text", DataSource, "SL13601");
            colSL29.DataBindings.Add("Text", DataSource, "SL13701");
            colSL30.DataBindings.Add("Text", DataSource, "SL13801");
            colSL31.DataBindings.Add("Text", DataSource, "SL13901");
            colSL32.DataBindings.Add("Text", DataSource, "SL14001");
            colSL33.DataBindings.Add("Text", DataSource, "SL14101");
            colSL34.DataBindings.Add("Text", DataSource, "SL14201");
            colSL35.DataBindings.Add("Text", DataSource, "SL14301");
            colSL36.DataBindings.Add("Text", DataSource, "SL14401");
            colSL37.DataBindings.Add("Text", DataSource, "SL14501");
            colSL38.DataBindings.Add("Text", DataSource, "SL14601");
            colSL39.DataBindings.Add("Text", DataSource, "SL14701");
            colSL40.DataBindings.Add("Text", DataSource, "SL14801");
            colSL41.DataBindings.Add("Text", DataSource, "SL14901");
            colSL42.DataBindings.Add("Text", DataSource, "SL15001");
            colSL43.DataBindings.Add("Text", DataSource, "SL15101");
            coLSL44.DataBindings.Add("Text", DataSource, "SL15201");
            colSL45.DataBindings.Add("Text", DataSource, "SL15301");
            colSL46.DataBindings.Add("Text", DataSource, "SL15401");
            colSL47.DataBindings.Add("Text", DataSource, "SL15501");
            colSL48.DataBindings.Add("Text", DataSource, "SL15601");
            colSL49.DataBindings.Add("Text", DataSource, "SL15701");
            colSL50.DataBindings.Add("Text", DataSource, "SL15801");
            colSL51.DataBindings.Add("Text", DataSource, "SL15901");
            colSL52.DataBindings.Add("Text", DataSource, "SL16001");
            colSL53.DataBindings.Add("Text", DataSource, "SL16101");
            colSL54.DataBindings.Add("Text", DataSource, "SL16201");
            //colSL39.DataBindings.Add("Text", DataSource, "SL3901");

            colT1.DataBindings.Add("Text", DataSource, "SL10901");
            colT2.DataBindings.Add("Text", DataSource, "SL11001");
            colT3.DataBindings.Add("Text", DataSource, "SL11101");
            colT4.DataBindings.Add("Text", DataSource, "SL11201");
            colT5.DataBindings.Add("Text", DataSource, "SL11301");
            colT6.DataBindings.Add("Text", DataSource, "SL11401");
            colT7.DataBindings.Add("Text", DataSource, "SL11501");
            colT8.DataBindings.Add("Text", DataSource, "SL11601");
            colT9.DataBindings.Add("Text", DataSource, "SL11701");
            colT10.DataBindings.Add("Text", DataSource, "SL11801");
            colT11.DataBindings.Add("Text", DataSource, "Sl11901");
            colT12.DataBindings.Add("Text", DataSource, "SL12001");
            colT13.DataBindings.Add("Text", DataSource, "SL12101");
            colT14.DataBindings.Add("Text", DataSource, "SL12201");
            colT15.DataBindings.Add("Text", DataSource, "SL12301");
            colT16.DataBindings.Add("Text", DataSource, "SL12401");
            colT17.DataBindings.Add("Text", DataSource, "SL12501");
            colT18.DataBindings.Add("Text", DataSource, "SL12601");
            colT19.DataBindings.Add("Text", DataSource, "SL12701");
            colT20.DataBindings.Add("Text", DataSource, "SL12801");
            colT21.DataBindings.Add("Text", DataSource, "SL12901");
            colT22.DataBindings.Add("Text", DataSource, "SL13001");
            colT23.DataBindings.Add("Text", DataSource, "SL13101");
            colT24.DataBindings.Add("Text", DataSource, "SL13201");
            colT25.DataBindings.Add("Text", DataSource, "SL13301");
            colT26.DataBindings.Add("Text", DataSource, "SL13401");
            colT27.DataBindings.Add("Text", DataSource, "SL13501");
            colT28.DataBindings.Add("Text", DataSource, "SL13601");
            colT29.DataBindings.Add("Text", DataSource, "SL13701");
            colT30.DataBindings.Add("Text", DataSource, "SL13801");
            colT31.DataBindings.Add("Text", DataSource, "SL13901");
            colT32.DataBindings.Add("Text", DataSource, "SL14001");
            colT33.DataBindings.Add("Text", DataSource, "SL14101");
            colT34.DataBindings.Add("Text", DataSource, "SL14201");
            colT35.DataBindings.Add("Text", DataSource, "SL14301");
            colT36.DataBindings.Add("Text", DataSource, "SL14401");
            colT37.DataBindings.Add("Text", DataSource, "SL14501");
            colT38.DataBindings.Add("Text", DataSource, "SL14601");
            colT39.DataBindings.Add("Text", DataSource, "SL14701");
            colT40.DataBindings.Add("Text", DataSource, "SL14801");
            colT41.DataBindings.Add("Text", DataSource, "SL14901");
            colT42.DataBindings.Add("Text", DataSource, "SL15001");
            colT43.DataBindings.Add("Text", DataSource, "SL15101");
            colT44.DataBindings.Add("Text", DataSource, "SL15201");
            colT45.DataBindings.Add("Text", DataSource, "SL15301");
            colT46.DataBindings.Add("Text", DataSource, "SL15401");
            colT47.DataBindings.Add("Text", DataSource, "SL15501");
            colT48.DataBindings.Add("Text", DataSource, "SL15601");
            colT49.DataBindings.Add("Text", DataSource, "SL15701");
            colT50.DataBindings.Add("Text", DataSource, "SL15801");
            colT51.DataBindings.Add("Text", DataSource, "SL15901");
            colT52.DataBindings.Add("Text", DataSource, "SL16001");
            colT53.DataBindings.Add("Text", DataSource, "SL16101");
            colT54.DataBindings.Add("Text", DataSource, "SL16201");
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
