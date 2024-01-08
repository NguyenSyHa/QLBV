using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BBKiemNhap_Sub_24012 : DevExpress.XtraReports.UI.XtraReport
    {
        int Mau = 0;
        public rep_BBKiemNhap_Sub_24012(int mau = 0)
        {
            Mau = mau; 
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (CVCB7.Value != null && CVCB7.Value != "")
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
            }
            else
            {
                SubBand2.Visible = true;
                SubBand1.Visible = false;
            }
            if (Mau == 2)
            {
                SubBand2.Visible = false;
                SubBand1.Visible = false;
                SubBand3_24012_Mau2.Visible = true;
            }
        }
    }
}
