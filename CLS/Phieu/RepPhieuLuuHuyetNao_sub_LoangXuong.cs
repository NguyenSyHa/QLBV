using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;


namespace QLBV.BaoCao
{
    public partial class RepPhieuLuuHuyetNao_sub_LoangXuong : DevExpress.XtraReports.UI.XtraReport
    {
        public double BMD;
        public double SCore;
        public RepPhieuLuuHuyetNao_sub_LoangXuong()
        {
            InitializeComponent();
        }     
        // int row = 0;
    
        internal void dataBinding()
        {
            if (BMD != -1000)
            {
                if (BMD >= 100)
                    colBMD1.Text = BMD.ToString();
                else
                    colBMD2.Text = BMD.ToString();
            }
            if (SCore != -1000)
            {
                if (SCore >= -1)
                    colScore1.Text = SCore.ToString("#,0");
                else if (SCore >= -2.5 && SCore < -1)
                    colScore2.Text = SCore.ToString("#,0");
                else
                    colScore3.Text = SCore.ToString("#,0");
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
        }
    }
}
