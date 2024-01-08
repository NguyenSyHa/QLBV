using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcTinhHinhBenhTatICD10_CL : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcTinhHinhBenhTatICD10_CL()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            txtma.DataBindings.Add("Text", DataSource, "Ma");
            colTenCB.DataBindings.Add("Text", DataSource, "TenCB");
            colTenICD.DataBindings.Add("Text", DataSource, "TenWHO");
            colMaICD.DataBindings.Add("Text",DataSource,"MaICD");


            col1r.DataBindings.Add("Text", DataSource, "i1");
            col2r.DataBindings.Add("Text", DataSource, "i2");
            col3r.DataBindings.Add("Text", DataSource, "i3");
            col4r.DataBindings.Add("Text", DataSource, "i4");
            col5r.DataBindings.Add("Text", DataSource, "i5");
            col6r.DataBindings.Add("Text", DataSource, "i6");
            col7r.DataBindings.Add("Text", DataSource, "i7");
            col8r.DataBindings.Add("Text", DataSource, "i8");
            col9r.DataBindings.Add("Text", DataSource, "i9");
            col10r.DataBindings.Add("Text", DataSource, "i10");
            col11r.DataBindings.Add("Text", DataSource, "i11");
            col12r.DataBindings.Add("Text", DataSource, "i12");
          
            col1g.DataBindings.Add("Text", DataSource, "i1");
            col2g.DataBindings.Add("Text", DataSource, "i2");
            col3g.DataBindings.Add("Text", DataSource, "i3");
            col4g.DataBindings.Add("Text", DataSource, "i4");
            col5g.DataBindings.Add("Text", DataSource, "i5");
            col6g.DataBindings.Add("Text", DataSource, "i6");
            col7g.DataBindings.Add("Text", DataSource, "i7");
            col8g.DataBindings.Add("Text", DataSource, "i8");
            col9g.DataBindings.Add("Text", DataSource, "i9");
            col10g.DataBindings.Add("Text", DataSource, "i10");
            col11g.DataBindings.Add("Text", DataSource, "i11");
            col12g.DataBindings.Add("Text", DataSource, "i12");
          

            GroupHeader1.GroupFields.Add(new GroupField("Ma"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colTTDV.Text = DungChung.Bien.GiamDoc;
        }

      
        private void colKBTS_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            int _i1 = Convert.ToInt32(this.GetCurrentColumnValue("i1"));
            if(_i1>0)
            { col1.Text = _i1.ToString("#,##"); }
            else { col1.Text = ""; }
            int _i2 = Convert.ToInt32(this.GetCurrentColumnValue("i2"));
            if (_i2 > 0)
            { col2.Text = _i2.ToString("#,##"); }
            else { col2.Text = ""; }
            int _i3 = Convert.ToInt32(this.GetCurrentColumnValue("i3"));
            if (_i3 > 0)
            { col3.Text = _i3.ToString("#,##"); }
            else { col3.Text = ""; }
            int _i4 = Convert.ToInt32(this.GetCurrentColumnValue("i4"));
            if (_i4 > 0)
            { col4.Text = _i4.ToString("#,##"); }
            else { col4.Text = ""; }
            int _i5 = Convert.ToInt32(this.GetCurrentColumnValue("i5"));
            if (_i5 > 0)
            { col5.Text = _i5.ToString("#,##"); }
            else { col5.Text = ""; }
            int _i6 = Convert.ToInt32(this.GetCurrentColumnValue("i6"));
            if (_i6 > 0)
            { col6.Text = _i6.ToString("#,##"); }
            else { col6.Text = ""; }
            int _i7 = Convert.ToInt32(this.GetCurrentColumnValue("i7"));
            if (_i7 > 0)
            { col7.Text = _i7.ToString("#,##"); }
            else { col7.Text = ""; }
            int _i8 = Convert.ToInt32(this.GetCurrentColumnValue("i8"));
            if (_i8 > 0)
            { col8.Text = _i8.ToString("#,##"); }
            else { col8.Text = ""; }
            int _i9 = Convert.ToInt32(this.GetCurrentColumnValue("i9"));
            if (_i9 > 0)
            { col9.Text = _i9.ToString("#,##"); }
            else { col9.Text = ""; }
            int _i10 = Convert.ToInt32(this.GetCurrentColumnValue("i10"));
            if (_i10 > 0)
            { col10.Text = _i10.ToString("#,##"); }
            else { col10.Text = ""; }
            int _i11 = Convert.ToInt32(this.GetCurrentColumnValue("i11"));
            if (_i11 > 0)
            { col11.Text = _i11.ToString("#,##"); }
            else { col11.Text = ""; }
            int _i12 = Convert.ToInt32(this.GetCurrentColumnValue("i12"));
            if (_i12 > 0)
            { col12.Text = _i12.ToString("#,##"); }
            else { col12.Text = ""; }
           
        }
    }
}
