using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_ThVPhi_TU_TTTU_BG : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_ThVPhi_TU_TTTU_BG()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colNTN.DataBindings.Add("Text", DataSource, "NTN").FormatString = "{0:dd/MM}";
            colVP.DataBindings.Add("Text", DataSource, "VP").FormatString = DungChung.Bien.FormatString[1];
            colTU.DataBindings.Add("Text", DataSource, "TU").FormatString = DungChung.Bien.FormatString[1];
            colTT.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
            colCL.DataBindings.Add("Text", DataSource, "CL").FormatString = DungChung.Bien.FormatString[1];
            colVPt.DataBindings.Add("Text", DataSource, "VP").FormatString = DungChung.Bien.FormatString[1];
            colTUt.DataBindings.Add("Text", DataSource, "TU").FormatString = DungChung.Bien.FormatString[1];
            colTTt.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
            colCLt.DataBindings.Add("Text", DataSource, "CL").FormatString = DungChung.Bien.FormatString[1];
          
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            //colTongT.Text = tong.ToString("0,0");
        
        }

       
       
    }
}
