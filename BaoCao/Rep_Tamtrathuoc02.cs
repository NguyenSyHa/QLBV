using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_Tamtrathuoc02 : DevExpress.XtraReports.UI.XtraReport
    {
        int _mau = 0;
        public Rep_Tamtrathuoc02()
        {
            InitializeComponent();
        }
        public Rep_Tamtrathuoc02(int mau)
        {
            mau = _mau;
            InitializeComponent();
        }
        public void BindingData()
        {
            celTenBNhan.DataBindings.Add("Text", DataSource, "TenBN");
            celTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colSL1.DataBindings.Add("Text", DataSource, "SL4701");
            colSL2.DataBindings.Add("Text", DataSource, "SL4801");
            colSL3.DataBindings.Add("Text", DataSource, "SL4901");
            colSL4.DataBindings.Add("Text", DataSource, "SL5001");
            colSL5.DataBindings.Add("Text", DataSource, "SL5101");
            colSL6.DataBindings.Add("Text", DataSource, "SL5201");
            colSL7.DataBindings.Add("Text", DataSource, "SL5301");
            colSL8.DataBindings.Add("Text", DataSource, "SL5401");
            colSL9.DataBindings.Add("Text", DataSource, "SL5501");
            colSL10.DataBindings.Add("Text", DataSource, "SL5601");
            colSL12.DataBindings.Add("Text", DataSource, "Sl5701");
            colSL11.DataBindings.Add("Text", DataSource, "SL5801");
            colSL13.DataBindings.Add("Text", DataSource, "SL5901");
            colSL14.DataBindings.Add("Text", DataSource, "SL6001");
            colSL15.DataBindings.Add("Text", DataSource, "SL6101");
            colSL16.DataBindings.Add("Text", DataSource, "SL6201");
            colSL17.DataBindings.Add("Text", DataSource, "SL6301");
            colSL18.DataBindings.Add("Text", DataSource, "SL6401");
            colSL19.DataBindings.Add("Text", DataSource, "SL6501");
            colSL20.DataBindings.Add("Text", DataSource, "SL6601");
            colSL21.DataBindings.Add("Text", DataSource, "SL6701");
            colSL22.DataBindings.Add("Text", DataSource, "SL6801");
            colSL23.DataBindings.Add("Text", DataSource, "SL6901");
            colSL24.DataBindings.Add("Text", DataSource, "SL7001");
            colSL25.DataBindings.Add("Text", DataSource, "SL7101");
            colSL26.DataBindings.Add("Text", DataSource, "SL7201");
            colSL27.DataBindings.Add("Text", DataSource, "SL7301");
            colSL28.DataBindings.Add("Text", DataSource, "SL7401");
            colSL29.DataBindings.Add("Text", DataSource, "SL7501");
            colSL30.DataBindings.Add("Text", DataSource, "SL7601");
            colSL31.DataBindings.Add("Text", DataSource, "SL7701");
            colSL32.DataBindings.Add("Text", DataSource, "SL7801");
            colSL33.DataBindings.Add("Text", DataSource, "SL7901");
            colSL34.DataBindings.Add("Text", DataSource, "SL8001");
            colSL35.DataBindings.Add("Text", DataSource, "SL8101");
            colSL36.DataBindings.Add("Text", DataSource, "SL8201");
            colSL37.DataBindings.Add("Text", DataSource, "SL8301");
            colSL38.DataBindings.Add("Text", DataSource, "SL8401");
            colSL39.DataBindings.Add("Text", DataSource, "SL8501");
            colSL40.DataBindings.Add("Text", DataSource, "SL8601");
            colSL41.DataBindings.Add("Text", DataSource, "SL8701");
            colSL42.DataBindings.Add("Text", DataSource, "SL8801");
            colSL43.DataBindings.Add("Text", DataSource, "SL8901");
            colSL44.DataBindings.Add("Text", DataSource, "SL9001");
            colSL45.DataBindings.Add("Text", DataSource, "SL9101");
            colSL46.DataBindings.Add("Text", DataSource, "SL9201");
            colSL47.DataBindings.Add("Text", DataSource, "SL9301");
            colSL48.DataBindings.Add("Text", DataSource, "SL9401");
            colSL49.DataBindings.Add("Text", DataSource, "SL9501");
            colSL50.DataBindings.Add("Text", DataSource, "SL9601");
            colSL51.DataBindings.Add("Text", DataSource, "SL9701");
            colSL52.DataBindings.Add("Text", DataSource, "SL9801");
            //colSL53.DataBindings.Add("Text", DataSource, "SL9901");
            //colSL38.DataBindings.Add("Text", DataSource, "SL3801");
            //colSL39.DataBindings.Add("Text", DataSource, "SL3901");
            colT1.DataBindings.Add("Text", DataSource, "SL4701");
            colT2.DataBindings.Add("Text", DataSource, "SL4801");
            colT3.DataBindings.Add("Text", DataSource, "SL4901");
            colT4.DataBindings.Add("Text", DataSource, "SL5001");
            colT5.DataBindings.Add("Text", DataSource, "SL5101");
            colT6.DataBindings.Add("Text", DataSource, "SL5201");
            colT7.DataBindings.Add("Text", DataSource, "SL5301");
            colT8.DataBindings.Add("Text", DataSource, "SL5401");
            colT9.DataBindings.Add("Text", DataSource, "SL5501");
            colT10.DataBindings.Add("Text", DataSource, "SL5601");
            colT11.DataBindings.Add("Text", DataSource, "SL5701");
            colT12.DataBindings.Add("Text", DataSource, "SL5801");
            colT13.DataBindings.Add("Text", DataSource, "SL5901");
            colT14.DataBindings.Add("Text", DataSource, "SL6001");
            colT15.DataBindings.Add("Text", DataSource, "SL6101");
            colT16.DataBindings.Add("Text", DataSource, "SL6201");
            colT17.DataBindings.Add("Text", DataSource, "SL6301");
            colT18.DataBindings.Add("Text", DataSource, "SL6401");
            colT19.DataBindings.Add("Text", DataSource, "SL6501");
            colT20.DataBindings.Add("Text", DataSource, "SL6601");
            colT21.DataBindings.Add("Text", DataSource, "SL6701");
            colT22.DataBindings.Add("Text", DataSource, "SL6801");
            colT23.DataBindings.Add("Text", DataSource, "SL6901");
            colT24.DataBindings.Add("Text", DataSource, "SL7001");
            colT25.DataBindings.Add("Text", DataSource, "SL7101");
            colT26.DataBindings.Add("Text", DataSource, "SL7201");
            colT27.DataBindings.Add("Text", DataSource, "SL7301");
            colT28.DataBindings.Add("Text", DataSource, "SL7401");
            colT29.DataBindings.Add("Text", DataSource, "SL7501");
            colT30.DataBindings.Add("Text", DataSource, "SL7601");
            colT31.DataBindings.Add("Text", DataSource, "SL7701");
            colT32.DataBindings.Add("Text", DataSource, "SL7801");
            colT33.DataBindings.Add("Text", DataSource, "SL7901");
            colT34.DataBindings.Add("Text", DataSource, "SL8001");
            colT35.DataBindings.Add("Text", DataSource, "SL8101");
            colT36.DataBindings.Add("Text", DataSource, "SL8201");
            colT37.DataBindings.Add("Text", DataSource, "SL8301");
            colT38.DataBindings.Add("Text", DataSource, "SL8401");
            colT39.DataBindings.Add("Text", DataSource, "SL8501");
            colT40.DataBindings.Add("Text", DataSource, "SL8601");
            colT41.DataBindings.Add("Text", DataSource, "SL8701");
            colT42.DataBindings.Add("Text", DataSource, "SL8801");
            colT43.DataBindings.Add("Text", DataSource, "SL8901");
            colT44.DataBindings.Add("Text", DataSource, "SL9001");
            colT45.DataBindings.Add("Text", DataSource, "SL9101");
            colT46.DataBindings.Add("Text", DataSource, "SL9201");
            colT47.DataBindings.Add("Text", DataSource, "SL9301");
            colT48.DataBindings.Add("Text", DataSource, "SL9401");
            colT49.DataBindings.Add("Text", DataSource, "SL9501");
            colT50.DataBindings.Add("Text", DataSource, "SL9601");
            colT51.DataBindings.Add("Text", DataSource, "SL9701");
            colT52.DataBindings.Add("Text", DataSource, "SL9801");
            //colT53.DataBindings.Add("Text", DataSource, "SL9901");
        }
        public void BindingData2()
        {
            //colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
            //colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            celTenBNhan.DataBindings.Add("Text", DataSource, "TenBN");
            celTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colSL1.DataBindings.Add("Text", DataSource, "SL10001");
            colSL2.DataBindings.Add("Text", DataSource, "SL10101");
            colSL3.DataBindings.Add("Text", DataSource, "SL10201");
            colSL4.DataBindings.Add("Text", DataSource, "SL10301");
            colSL5.DataBindings.Add("Text", DataSource, "SL10401");
            colSL6.DataBindings.Add("Text", DataSource, "SL10501");
            colSL7.DataBindings.Add("Text", DataSource, "SL10601");
            colSL8.DataBindings.Add("Text", DataSource, "SL10701");
            colSL9.DataBindings.Add("Text", DataSource, "SL10801");
            colSL10.DataBindings.Add("Text", DataSource, "S109601");
            colSL12.DataBindings.Add("Text", DataSource, "S110701");
            colSL11.DataBindings.Add("Text", DataSource, "SL11101");
            colSL13.DataBindings.Add("Text", DataSource, "SL11201");
            colSL14.DataBindings.Add("Text", DataSource, "SL11301");
            colSL15.DataBindings.Add("Text", DataSource, "SL11401");
            colSL16.DataBindings.Add("Text", DataSource, "SL11501");
            colSL17.DataBindings.Add("Text", DataSource, "SL11601");
            colSL18.DataBindings.Add("Text", DataSource, "SL11701");
            colSL19.DataBindings.Add("Text", DataSource, "SL11801");
            colSL20.DataBindings.Add("Text", DataSource, "SL11901");
            colSL21.DataBindings.Add("Text", DataSource, "SL12001");
            colSL22.DataBindings.Add("Text", DataSource, "SL12101");
            colSL23.DataBindings.Add("Text", DataSource, "SL12201");
            colSL24.DataBindings.Add("Text", DataSource, "SL12301");
            colSL25.DataBindings.Add("Text", DataSource, "SL12401");
            colSL26.DataBindings.Add("Text", DataSource, "SL12501");
            colSL27.DataBindings.Add("Text", DataSource, "SL12601");
            colSL28.DataBindings.Add("Text", DataSource, "SL12701");
            colSL29.DataBindings.Add("Text", DataSource, "SL12801");
            colSL30.DataBindings.Add("Text", DataSource, "SL12901");
            colSL31.DataBindings.Add("Text", DataSource, "SL13001");
            colSL32.DataBindings.Add("Text", DataSource, "SL13101");
            colSL33.DataBindings.Add("Text", DataSource, "SL13201");
            colSL34.DataBindings.Add("Text", DataSource, "SL13301");
            colSL35.DataBindings.Add("Text", DataSource, "SL13401");
            colSL36.DataBindings.Add("Text", DataSource, "SL13501");
            colSL37.DataBindings.Add("Text", DataSource, "SL13601");
            colSL38.DataBindings.Add("Text", DataSource, "SL13701");
            colSL39.DataBindings.Add("Text", DataSource, "SL13801");
            colSL40.DataBindings.Add("Text", DataSource, "SL13901");
            colSL41.DataBindings.Add("Text", DataSource, "SL14001");
            colSL42.DataBindings.Add("Text", DataSource, "SL14101");
            colSL43.DataBindings.Add("Text", DataSource, "SL14201");
            colSL44.DataBindings.Add("Text", DataSource, "SL14301");
            colSL45.DataBindings.Add("Text", DataSource, "SL14401");
            colSL46.DataBindings.Add("Text", DataSource, "SL14501");
            colSL47.DataBindings.Add("Text", DataSource, "SL14601");
            colSL48.DataBindings.Add("Text", DataSource, "SL14701");
            colSL49.DataBindings.Add("Text", DataSource, "SL14801");
            colSL50.DataBindings.Add("Text", DataSource, "SL14901");
            colSL51.DataBindings.Add("Text", DataSource, "SL15001");
            colSL52.DataBindings.Add("Text", DataSource, "SL15101");
            //colSL53.DataBindings.Add("Text", DataSource, "SL15201");
            //colSL38.DataBindings.Add("Text", DataSource, "SL3801");
            //colSL39.DataBindings.Add("Text", DataSource, "SL3901");
            colT1.DataBindings.Add("Text", DataSource, "SL10001");
            colT2.DataBindings.Add("Text", DataSource, "SL10101");
            colT3.DataBindings.Add("Text", DataSource, "SL10201");
            colT4.DataBindings.Add("Text", DataSource, "SL10301");
            colT5.DataBindings.Add("Text", DataSource, "SL10401");
            colT6.DataBindings.Add("Text", DataSource, "SL10501");
            colT7.DataBindings.Add("Text", DataSource, "SL10601");
            colT8.DataBindings.Add("Text", DataSource, "SL10701");
            colT9.DataBindings.Add("Text", DataSource, "SL10801");
            colT10.DataBindings.Add("Text", DataSource, "SL10901");
            colT11.DataBindings.Add("Text", DataSource, "SL11001");
            colT12.DataBindings.Add("Text", DataSource, "SL11101");
            colT13.DataBindings.Add("Text", DataSource, "SL11201");
            colT14.DataBindings.Add("Text", DataSource, "SL11301");
            colT15.DataBindings.Add("Text", DataSource, "SL11401");
            colT16.DataBindings.Add("Text", DataSource, "SL11501");
            colT17.DataBindings.Add("Text", DataSource, "SL11601");
            colT18.DataBindings.Add("Text", DataSource, "SL11701");
            colT19.DataBindings.Add("Text", DataSource, "SL11801");
            colT20.DataBindings.Add("Text", DataSource, "SL11901");
            colT21.DataBindings.Add("Text", DataSource, "SL12001");
            colT22.DataBindings.Add("Text", DataSource, "SL12101");
            colT23.DataBindings.Add("Text", DataSource, "SL12201");
            colT24.DataBindings.Add("Text", DataSource, "SL12301");
            colT25.DataBindings.Add("Text", DataSource, "SL12401");
            colT26.DataBindings.Add("Text", DataSource, "SL12501");
            colT27.DataBindings.Add("Text", DataSource, "SL12601");
            colT28.DataBindings.Add("Text", DataSource, "SL12701");
            colT29.DataBindings.Add("Text", DataSource, "SL12801");
            colT30.DataBindings.Add("Text", DataSource, "SL12901");
            colT31.DataBindings.Add("Text", DataSource, "SL13001");
            colT32.DataBindings.Add("Text", DataSource, "SL13101");
            colT33.DataBindings.Add("Text", DataSource, "SL13201");
            colT34.DataBindings.Add("Text", DataSource, "SL13301");
            colT35.DataBindings.Add("Text", DataSource, "SL13401");
            colT36.DataBindings.Add("Text", DataSource, "SL13501");
            colT37.DataBindings.Add("Text", DataSource, "SL13601");
            colT38.DataBindings.Add("Text", DataSource, "SL13701");
            colT39.DataBindings.Add("Text", DataSource, "SL13801");
            colT40.DataBindings.Add("Text", DataSource, "SL13901");
            colT41.DataBindings.Add("Text", DataSource, "SL14001");
            colT42.DataBindings.Add("Text", DataSource, "SL14101");
            colT43.DataBindings.Add("Text", DataSource, "SL14201");
            colT44.DataBindings.Add("Text", DataSource, "SL14301");
            colT45.DataBindings.Add("Text", DataSource, "SL14401");
            colT46.DataBindings.Add("Text", DataSource, "SL14501");
            colT47.DataBindings.Add("Text", DataSource, "SL14601");
            colT48.DataBindings.Add("Text", DataSource, "SL14701");
            colT49.DataBindings.Add("Text", DataSource, "SL14801");
            colT50.DataBindings.Add("Text", DataSource, "SL14901");
            colT51.DataBindings.Add("Text", DataSource, "SL15001");
            colT52.DataBindings.Add("Text", DataSource, "SL15101");
            //colT53.DataBindings.Add("Text", DataSource, "SL15201");
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
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
