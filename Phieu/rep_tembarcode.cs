using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class rep_tembarcode : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_tembarcode()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "14017")
            {
                this.SubBand1.Visible = false;
                this.SubBand2_14017.Visible = true;
            }
        }

     
    }
}
