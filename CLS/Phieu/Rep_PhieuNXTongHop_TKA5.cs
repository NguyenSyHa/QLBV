using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNXTongHop_TKA5 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNXTongHop_TKA5()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
 
        public void BindingData()
        {
            colTenNhomDV.DataBindings.Add("Text", DataSource, "TenTN");
            colYCKT.DataBindings.Add("Text", DataSource, "TenDV");
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            string macb = "";
            if (MaCB.Value != null)
                macb = MaCB.Value.ToString();
            var bsdt = (from bs in DataContect.CanBoes
                        where (bs.MaCB.Equals(macb))
                        select new { bs.TenCB }).ToList();
            if (bsdt.Count > 0)
            {
                colTenBSDT.Text = bsdt.First().TenCB;
            }
            if (DungChung.Bien.MaBV == "30004")
                colTenBSDT.Visible = false;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            //colTenCQ.Text = DungChung.Bien.TenCQ;
        }

    }
}
