using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcTKSoPhieuLinhTheoKhoa : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcTKSoPhieuLinhTheoKhoa()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public void BindingData()
        {
            colKP.DataBindings.Add("Text", DataSource, "tenkp");
            colSoPL.DataBindings.Add("Text", DataSource, "SoPL");
            colKieuDon.DataBindings.Add("Text", DataSource, "KieuDon");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colSL.DataBindings.Add("Text", DataSource, "SL").FormatString = DungChung.Bien.FormatString[0];
        
            GroupHeader2.GroupFields.Add(new GroupField("tenkp"));
            GroupHeader1.GroupFields.Add(new GroupField("SoPL"));
      
        }

       
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Convert.ToInt32(InTong.Value) == 1)
            {
                Detail.Visible = false;
            }
        }
    }
}
