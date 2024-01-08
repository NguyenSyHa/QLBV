using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;


namespace QLBV.BaoCao
{
    public partial class RepPhieuLuuHuyetNao_sub_12121 : DevExpress.XtraReports.UI.XtraReport
    {
       
        public RepPhieuLuuHuyetNao_sub_12121()
        {
            InitializeComponent();
        }     
        // int row = 0;
    
      

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                xrLabel2.Font = new Font(xrLabel2.Font, FontStyle.Regular);
                xrLabel1.Font = new Font(xrLabel1.Font, FontStyle.Regular);
            }
        }
    }
}
