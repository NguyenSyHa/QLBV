using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BaoCaoNBChuyenDi : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BaoCaoNBChuyenDi()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenCK.DataBindings.Add("Text", DataSource, "TenCK");
            colTSLKham.DataBindings.Add("Text", DataSource, "SLKham");
            colSLKham.DataBindings.Add("Text", DataSource, "SLKham");
            colSLDieutri.DataBindings.Add("Text", DataSource, "SLDieutri");
            colTSLDieutri.DataBindings.Add("Text", DataSource, "SLDieutri");
            colSLChuyen.DataBindings.Add("Text", DataSource, "SLChuyen");
            colTSLChuyen.DataBindings.Add("Text", DataSource, "SLChuyen");
            //colTyLe.DataBindings.Add("Text", DataSource, "TyLe");
            colBHYT.DataBindings.Add("Text", DataSource, "BHYT");
            colTBHYT.DataBindings.Add("Text", DataSource, "BHYT");
            col1a.DataBindings.Add("Text", DataSource, "SL1a");
            colT1a.DataBindings.Add("Text", DataSource, "SL1a");
            col1b.DataBindings.Add("Text", DataSource, "SL1b");
            colT1b.DataBindings.Add("Text", DataSource, "SL1b");
            col2.DataBindings.Add("Text", DataSource, "SL2");
            colT2.DataBindings.Add("Text", DataSource, "SL2");
            col3.DataBindings.Add("Text", DataSource, "SL3");
            colT3.DataBindings.Add("Text", DataSource, "SL3");
            col4.DataBindings.Add("Text", DataSource, "SL4");
            colT4.DataBindings.Add("Text", DataSource, "SL4");
            col5.DataBindings.Add("Text", DataSource, "SL5");
            colT5.DataBindings.Add("Text", DataSource, "SL5");
            colTuyen1.DataBindings.Add("Text", DataSource, "Tuyen1");
            colTTuyen1.DataBindings.Add("Text", DataSource, "Tuyen1");
            colTuyen2.DataBindings.Add("Text", DataSource, "Tuyen2");
            colTTuyen2.DataBindings.Add("Text", DataSource, "Tuyen2");
            colTuyen3.DataBindings.Add("Text", DataSource, "Tuyen3");
            colTTuyen3.DataBindings.Add("Text", DataSource, "Tuyen3");
            colTuyen4.DataBindings.Add("Text", DataSource, "Tuyen4");
            colTTuyen4.DataBindings.Add("Text", DataSource, "Tuyen4");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void colSLChuyen_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("SLKham") != null && this.GetCurrentColumnValue("SLKham").ToString() != "" && this.GetCurrentColumnValue("SLDieutri") != null && this.GetCurrentColumnValue("SLDieutri").ToString() != "" && this.GetCurrentColumnValue("SLChuyen") != null && this.GetCurrentColumnValue("SLChuyen").ToString() != "")
            {
                double T = Convert.ToDouble(this.GetCurrentColumnValue("SLChuyen").ToString()) / (Convert.ToDouble(this.GetCurrentColumnValue("SLKham").ToString()) + Convert.ToDouble(this.GetCurrentColumnValue("SLDieutri").ToString()))*100;
                T = Math.Round(T, 1);
                colTyLe.Text = T.ToString();
            }
        }
    }
}

