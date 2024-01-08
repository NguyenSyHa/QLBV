using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcKCBNguoiCaoTuoi_BLac : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcKCBNguoiCaoTuoi_BLac()
        {
            InitializeComponent();
        }
      
      private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }

      private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
      {
          colTenDV.Text = DungChung.Bien.TenCQ;

          if (KB3.Value.ToString() != null && KB1.Value.ToString() != "")
          {
              double _kp = Convert.ToDouble(KB3.Value);
              xrTableCell6.Text = _kp.ToString("#,#");
          }
          else xrTableCell6.Text = "";
          if (DTNT3.Value.ToString() != null && DTNT3.Value.ToString() != "")
          {
              double _kp = Convert.ToDouble(DTNT3.Value);
              xrTableCell22.Text = _kp.ToString("#,#");
          }
          else xrTableCell22.Text = "";
          if (DTNoiT3.Value.ToString() != null && DTNoiT3.Value.ToString() != "")
          {
              double _kp = Convert.ToDouble(DTNoiT3.Value);
              xrTableCell33.Text = _kp.ToString("#,#");
          }
          else xrTableCell33.Text = "";
      }
    }
}
