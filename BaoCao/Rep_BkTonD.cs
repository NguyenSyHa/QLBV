using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BkTonD : DevExpress.XtraReports.UI.XtraReport
    {
        List<QLBV.FormThamSo.Frm_BkTonD.DSKP> _DSKP = new List<QLBV.FormThamSo.Frm_BkTonD.DSKP>();
        public Rep_BkTonD()
        {
            
        }
        public Rep_BkTonD(List<QLBV.FormThamSo.Frm_BkTonD.DSKP> _lKP)
        {
            InitializeComponent();
            _DSKP = _lKP;
        }
        public void BindingData()
        {
            colNhomDVG1.DataBindings.Add("Text", DataSource, "TenNhomCT");
            colTenDV.DataBindings.Add("Text", DataSource, "tendv");
            colDVT.DataBindings.Add("Text", DataSource, "dvt");
            colNuocSX.DataBindings.Add("Text", DataSource, "nuocsx");
            colKPTong.DataBindings.Add("Text", DataSource, "TC").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenNhomCT"));
            colKP1.DataBindings.Add("Text", DataSource, "sl1");
            colKP2.DataBindings.Add("Text", DataSource, "sl2");
            colKP3.DataBindings.Add("Text", DataSource, "sl3");
            colKP4.DataBindings.Add("Text", DataSource, "sl4");
            colKP5.DataBindings.Add("Text", DataSource, "sl5");
            colKP6.DataBindings.Add("Text", DataSource, "sl6");
            colKP7.DataBindings.Add("Text", DataSource, "sl7");
            colKP8.DataBindings.Add("Text", DataSource, "sl8");
            colKP9.DataBindings.Add("Text", DataSource, "sl9");
            colKP10.DataBindings.Add("Text", DataSource, "sl10");
            colKP11.DataBindings.Add("Text", DataSource, "sl11");
            colKP12.DataBindings.Add("Text", DataSource, "sl12");
            colKP13.DataBindings.Add("Text", DataSource, "sl13");
            colKP14.DataBindings.Add("Text", DataSource, "sl14");
            colKP15.DataBindings.Add("Text", DataSource, "sl15");
            colKP16.DataBindings.Add("Text", DataSource, "sl16");
            colKP17.DataBindings.Add("Text", DataSource, "sl17");
            colKP18.DataBindings.Add("Text", DataSource, "sl18");
            colKP19.DataBindings.Add("Text", DataSource, "sl19");
            colKP20.DataBindings.Add("Text", DataSource, "sl20");
            colKP21.DataBindings.Add("Text", DataSource, "sl21");
            colKP22.DataBindings.Add("Text", DataSource, "sl22");
            colKP23.DataBindings.Add("Text", DataSource, "sl23");
            colKP24.DataBindings.Add("Text", DataSource, "sl24");
            colKP25.DataBindings.Add("Text", DataSource, "sl25");
            colKP26.DataBindings.Add("Text", DataSource, "sl26");
            colKP27.DataBindings.Add("Text", DataSource, "sl27");
            colKP28.DataBindings.Add("Text", DataSource, "sl28");

           
        }
        private void xrLabel1_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void colKPTong_BeforePrint(object sender, CancelEventArgs e)
        {
            //int sl1 = 0; int sl2 = 0;
            //if(this.GetCurrentColumnValue("SL1")!=null)
            //     sl1=Convert.ToInt32(this.GetCurrentColumnValue("SL1"));
            //if (this.GetCurrentColumnValue("SL1") != null)
            //    sl1 = Convert.ToInt32(this.GetCurrentColumnValue("SL1"));
            //colKPTong.Text = sl1.ToString() + sl2.ToString();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void xrTable1_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24009") {
                xrTableNuocSX.Text = "Đơn giá";
            }
        }

    }
}
