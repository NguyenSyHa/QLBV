using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_XNRoiLoanDongMau_30003 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_XNRoiLoanDongMau_30003()
        {
            InitializeComponent();
        }

      

        internal void BindingData()
        {
            lblTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            lblSTTG.DataBindings.Add("Text", DataSource, "STT");
           // celTenDV.DataBindings.Add("Text", DataSource, "TenDVct");
            celDonvi.DataBindings.Add("Text", DataSource, "DonVi");
            celChiSoBT.DataBindings.Add("Text", DataSource, "TSBT");
            celKQ.DataBindings.Add("Text", DataSource, "KetQua");
            celCheck.DataBindings.Add("Text", DataSource, "check");
            GroupHeader1.GroupFields.Add(new GroupField("STT"));
        }

        private void xrTableCell17_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            num++;
            if (num == 1)
            {
                celTenDV.Text = tenDV;
                celSTT.Text = GroupNum.ToString();
            }
            else
            {
                celTenDV.Text = "";
                celSTT.Text = "";
            }
           
             if (this.GetCurrentColumnValue("count") != null)
             {
                 int stt = Convert.ToInt32(this.GetCurrentColumnValue("count"));
                 if(num == stt)
                 {
                     celCheck.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right))); 
                     celDonvi.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right))); 
                     celChiSoBT.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
                     celKQ.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
                 }
                 else
                 {
                     celCheck.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
                     celDonvi.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
                     celChiSoBT.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
                     celKQ.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
                 }
             }
        }
        string tenDV = "";
        int num = 0;
        private void xrTableRow15_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void xrLine3_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        int GroupNum = 0;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            num = 0;
            GroupNum++;

            if (this.GetCurrentColumnValue("TenDV") != null)
            {
                tenDV = "";
                tenDV = this.GetCurrentColumnValue("TenDV").ToString();
            }
        }
    }
}
