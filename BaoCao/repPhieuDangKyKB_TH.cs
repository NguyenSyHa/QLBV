using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class repPhieuDangKyKB_TH : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuDangKyKB_TH()
        {
            InitializeComponent();
        }

        private void repPhieuDangKyKB_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
            QLBV_Database.QLBVEntities _data=new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var tencb = _data.CanBoes.Where(p => p.MaCB== (DungChung.Bien.MaCB)).ToList();
            if (tencb.Count > 0)
            {
                colCBTNhan.Text = tencb.First().TenCB.ToString();
            }
            if (SThe.Value != null && SThe.Value.ToString().Length==15)
            {
                colSthe.Text = SThe.Value.ToString().Substring(0, 2) + "-" + SThe.Value.ToString().Substring(2, 1) + "-" + SThe.Value.ToString().Substring(3, 2) + "-" + SThe.Value.ToString().Substring(5, 2) + "-" + SThe.Value.ToString().Substring(7, 3) + "-" + SThe.Value.ToString().Substring(10, 5);
            }
          
        }
        
    }
}
