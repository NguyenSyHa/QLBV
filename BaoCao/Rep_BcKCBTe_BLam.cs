using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcKCBTe_BLam : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcKCBTe_BLam()
        {
            InitializeComponent();
        }
      
      private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colTTDV.Text = DungChung.Bien.GiamDoc;
        }

      private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
      {
          colTenDV.Text = DungChung.Bien.TenCQ;

          if (KP1.Value.ToString() != null && KP1.Value.ToString() != "")
          {
              double _kp = Convert.ToDouble(SL1.Value);
              colKP1.Text = _kp.ToString("#,##");
          }
          else colKP1.Text = "";
          if (KP2.Value.ToString() != null && KP2.Value.ToString() != "")
          {
              double _kp = Convert.ToDouble(KP2.Value);
              colKP2.Text = _kp.ToString("#,##");
          }
          else colKP2.Text = "";
          if (KP3.Value.ToString() != null && KP3.Value.ToString() != "")
          {
              double _kp = Convert.ToDouble(KP3.Value);
              colKP3.Text = _kp.ToString("#,##");
          }
          else colKP3.Text = "";
         
          if (KP4.Value.ToString() != null && KP4.Value.ToString() != "")
          {
              double _kp = Convert.ToDouble(KP3.Value);
              colKP4.Text = _kp.ToString("#,##");
          }
          else colKP5.Text = "";
          if (KP5.Value.ToString() != null && KP5.Value.ToString() != "")
          {
              double _kp = Convert.ToDouble(KP5.Value);
              colKP5.Text = _kp.ToString("#,##");
          }
          else colKP5.Text = "";
          if (KP6.Value.ToString() != null && KP6.Value.ToString() != "")
          {
              double _kp = Convert.ToDouble(KP6.Value);
              colKP6.Text = _kp.ToString("#,##");
          }
          else colKP6.Text = "";
      }
    }
}
