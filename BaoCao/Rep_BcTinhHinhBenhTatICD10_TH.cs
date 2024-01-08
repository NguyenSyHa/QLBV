using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcTinhHinhBenhTatICD10_TH : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcTinhHinhBenhTatICD10_TH()
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
            col13r.DataBindings.Add("Text", DataSource, "i13");
            col14r.DataBindings.Add("Text", DataSource, "i14");
            col15r.DataBindings.Add("Text", DataSource, "i15");
            col16r.DataBindings.Add("Text", DataSource, "i16");
            col17r.DataBindings.Add("Text", DataSource, "i17");
            col18r.DataBindings.Add("Text", DataSource, "i18");
            col19r.DataBindings.Add("Text", DataSource, "i19");
            col20r.DataBindings.Add("Text", DataSource, "i20");
            col21r.DataBindings.Add("Text", DataSource, "i21");

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
            col13g.DataBindings.Add("Text", DataSource, "i13");
            col14g.DataBindings.Add("Text", DataSource, "i14");
            col15g.DataBindings.Add("Text", DataSource, "i15");
            col16g.DataBindings.Add("Text", DataSource, "i16");
            col17g.DataBindings.Add("Text", DataSource, "i17");
            col18g.DataBindings.Add("Text", DataSource, "i18");
            col19g.DataBindings.Add("Text", DataSource, "i19");
            col20g.DataBindings.Add("Text", DataSource, "i20");
            col21g.DataBindings.Add("Text", DataSource, "i21");

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
            int _i13 = Convert.ToInt32(this.GetCurrentColumnValue("i13"));
            if (_i13 > 0)
            { col13.Text = _i13.ToString("#,##"); }
            else { col13.Text = ""; }
            int _i14 = Convert.ToInt32(this.GetCurrentColumnValue("i14"));
            if (_i14 > 0)
            { col14.Text = _i14.ToString("#,##"); }
            else { col14.Text = ""; }
            int _i15 = Convert.ToInt32(this.GetCurrentColumnValue("i15"));
            if (_i15 > 0)
            { col15.Text = _i15.ToString("#,##"); }
            else { col15.Text = ""; }
            int _i16 = Convert.ToInt32(this.GetCurrentColumnValue("i16"));
            if (_i16 > 0)
            { col16.Text = _i16.ToString("#,##"); }
            else { col16.Text = ""; }
            int _i17 = Convert.ToInt32(this.GetCurrentColumnValue("i17"));
            if (_i17 > 0)
            { col17.Text = _i17.ToString("#,##"); }
            else { col17.Text = ""; }
            int _i18 = Convert.ToInt32(this.GetCurrentColumnValue("i18"));
            if (_i18 > 0)
            { col18.Text = _i18.ToString("#,##"); }
            else { col18.Text = ""; }
            int _i19 = Convert.ToInt32(this.GetCurrentColumnValue("i19"));
            if (_i19 > 0)
            { col19.Text = _i19.ToString("#,##"); }
            else { col19.Text = ""; }
            int _i20 = Convert.ToInt32(this.GetCurrentColumnValue("i20"));
            if (_i20 > 0)
            { col20.Text = _i20.ToString("#,##"); }
            else { col20.Text = ""; }
            int _i21 = Convert.ToInt32(this.GetCurrentColumnValue("i21"));
            if (_i21 > 0)
            { col21.Text = _i21.ToString("#,##"); }
            else { col21.Text = ""; }
       

        }
    }
}
