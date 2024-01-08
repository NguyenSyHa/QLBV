using System;using QLBV_Database;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace QLBV.BaoCao
{
    public partial class rep_NXT_24009 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_NXT_24009()
        {
            InitializeComponent();
        }
        bool hiennhom = true;
        bool HTTrongNgoaiNuoc = false;
        string[] arr;
        public rep_NXT_24009(bool a, bool nk, string[] ar)
        {
            InitializeComponent();
            hiennhom = a;
            HTTrongNgoaiNuoc = nk;
            arr = ar;
            
        }
        public void BindingData()
        {
            cell1.DataBindings.Add("Text", DataSource, "MaTam");
            cell2.DataBindings.Add("Text", DataSource, "TenHC");
            cell3.DataBindings.Add("Text", DataSource, "TenHamLuong");
            cell4.DataBindings.Add("Text", DataSource, "DonVi");
            if (celll5.Text != "" && celll5.Text != "Tất cả" && celll5.Text != "Tồn ĐK" && celll5.Text != "Nhập" && celll5.Text != "Xuất" && celll5.Text != "Tồn CK")
                cell5.DataBindings.Add("Text", DataSource, arr[0]);
            if (celll6.Text != "" && celll6.Text != "Tất cả" && celll6.Text != "Tồn ĐK" && celll6.Text != "Nhập" && celll6.Text != "Xuất" && celll6.Text != "Tồn CK")
                cell6.DataBindings.Add("Text", DataSource, arr[1]);
            if (celll7.Text != "" && celll7.Text != "Tất cả" && celll7.Text != "Tồn ĐK" && celll7.Text != "Nhập" && celll7.Text != "Xuất" && celll7.Text != "Tồn CK")
                cell7.DataBindings.Add("Text", DataSource, arr[2]);
            if (celll8.Text != "" && celll8.Text != "Tất cả" && celll8.Text != "Tồn ĐK" && celll8.Text != "Nhập" && celll8.Text != "Xuất" && celll8.Text != "Tồn CK")
                cell8.DataBindings.Add("Text", DataSource, arr[3]);
            if (celll9.Text != "" && celll9.Text != "Tất cả" && celll9.Text != "Tồn ĐK" && celll9.Text != "Nhập" && celll9.Text != "Xuất" && celll9.Text != "Tồn CK")
                cell9.DataBindings.Add("Text", DataSource, arr[4]);
            if (celll10.Text != "" && celll10.Text != "Tất cả" && celll10.Text != "Tồn ĐK" && celll10.Text != "Nhập" && celll10.Text != "Xuất" && celll10.Text != "Tồn CK")
                cell10.DataBindings.Add("Text", DataSource, arr[5]);
            if (celll11.Text != "" && celll11.Text != "Tất cả" && celll11.Text != "Tồn ĐK" && celll11.Text != "Nhập" && celll11.Text != "Xuất" && celll11.Text != "Tồn CK")
                cell11.DataBindings.Add("Text", DataSource, arr[6]);
            if (celll12.Text != "" && celll12.Text != "Tất cả" && celll12.Text != "Tồn ĐK" && celll12.Text != "Nhập" && celll12.Text != "Xuất" && celll12.Text != "Tồn CK")
                cell12.DataBindings.Add("Text", DataSource, arr[7]);
            if (celll13.Text != "" && celll13.Text != "Tất cả" && celll13.Text != "Tồn ĐK" && celll13.Text != "Nhập" && celll13.Text != "Xuất" && celll13.Text != "Tồn CK")
                cell13.DataBindings.Add("Text", DataSource, arr[8]);
            if (celll14.Text != "" && celll14.Text != "Tất cả" && celll14.Text != "Tồn ĐK" && celll14.Text != "Nhập" && celll14.Text != "Xuất" && celll14.Text != "Tồn CK")
                cell14.DataBindings.Add("Text", DataSource, arr[9]);
            if (celll15.Text != "" && celll15.Text != "Tất cả" && celll15.Text != "Tồn ĐK" && celll15.Text != "Nhập" && celll15.Text != "Xuất" && celll15.Text != "Tồn CK")
                cell15.DataBindings.Add("Text", DataSource, arr[10]);
            if (celll16.Text != "" && celll16.Text != "Tất cả" && celll16.Text != "Tồn ĐK" && celll16.Text != "Nhập" && celll16.Text != "Xuất" && celll16.Text != "Tồn CK")
                cell16.DataBindings.Add("Text", DataSource, arr[11]);
            if (celll17.Text != "" && celll17.Text != "Tất cả" && celll17.Text != "Tồn ĐK" && celll17.Text != "Nhập" && celll17.Text != "Xuất" && celll17.Text != "Tồn CK")
                cell17.DataBindings.Add("Text", DataSource, arr[12]);
            if (celll5.Text == "Tồn ĐK" || celll5.Text == "Nhập" || celll5.Text == "Xuất" || celll5.Text == "Tồn CK")
            {
                cellll5.DataBindings.Add("Text", DataSource, arr[0]).FormatString = DungChung.Bien.FormatString[0];
                cellll5.Summary.Func = SummaryFunc.Sum;
                cellll5.Summary.Running = SummaryRunning.Report;
                
                cell5.DataBindings.Add("Text", DataSource, arr[0]).FormatString = DungChung.Bien.FormatString[0];
            }
            if (celll6.Text == "Tồn ĐK" || celll6.Text == "Nhập" || celll6.Text == "Xuất" || celll6.Text == "Tồn CK")
            {
                cellll6.DataBindings.Add("Text", DataSource, arr[1]).FormatString = DungChung.Bien.FormatString[0];
                cellll6.Summary.Func = SummaryFunc.Sum;
                cellll6.Summary.Running = SummaryRunning.Report;
               
                cell6.DataBindings.Add("Text", DataSource, arr[1]).FormatString = DungChung.Bien.FormatString[0];
            }
            if (celll7.Text == "Tồn ĐK" || celll7.Text == "Nhập" || celll7.Text == "Xuất" || celll7.Text == "Tồn CK")
            {
                cellll7.Summary.Func = SummaryFunc.Sum;
                cellll7.Summary.Running = SummaryRunning.Report;
                cellll7.DataBindings.Add("Text", DataSource, arr[2]).FormatString = DungChung.Bien.FormatString[0];
                cell7.DataBindings.Add("Text", DataSource, arr[2]).FormatString = DungChung.Bien.FormatString[0];
            }
            if (celll8.Text == "Tồn ĐK" || celll8.Text == "Nhập" || celll8.Text == "Xuất" || celll8.Text == "Tồn CK")
            {
                cellll8.Summary.Func = SummaryFunc.Sum;
                cellll8.Summary.Running = SummaryRunning.Report;
                cellll8.DataBindings.Add("Text", DataSource, arr[3]).FormatString = DungChung.Bien.FormatString[0];
                cell8.DataBindings.Add("Text", DataSource, arr[3]).FormatString = DungChung.Bien.FormatString[0];
            }
            if (celll9.Text == "Tồn ĐK" || celll9.Text == "Nhập" || celll9.Text == "Xuất" || celll9.Text == "Tồn CK")
            {
                cellll9.Summary.Func = SummaryFunc.Sum;
                cellll9.Summary.Running = SummaryRunning.Report;
                cellll9.DataBindings.Add("Text", DataSource, arr[4]).FormatString = DungChung.Bien.FormatString[0];
                cell9.DataBindings.Add("Text", DataSource, arr[4]).FormatString = DungChung.Bien.FormatString[0];
            }
            if (celll10.Text == "Tồn ĐK" || celll10.Text == "Nhập" || celll10.Text == "Xuất" || celll10.Text == "Tồn CK")
            {
                cellll10.Summary.Func = SummaryFunc.Sum;
                cellll10.Summary.Running = SummaryRunning.Report;
                cellll10.DataBindings.Add("Text", DataSource, arr[5]).FormatString = DungChung.Bien.FormatString[0];
                cell10.DataBindings.Add("Text", DataSource, arr[5]).FormatString = DungChung.Bien.FormatString[0];
            }
            if (celll11.Text == "Tồn ĐK" || celll11.Text == "Nhập" || celll11.Text == "Xuất" || celll11.Text == "Tồn CK")
            {
                cellll11.Summary.Func = SummaryFunc.Sum;
                cellll11.Summary.Running = SummaryRunning.Report;
                cellll11.DataBindings.Add("Text", DataSource, arr[6]).FormatString = DungChung.Bien.FormatString[0];
                cell11.DataBindings.Add("Text", DataSource, arr[6]).FormatString = DungChung.Bien.FormatString[0];
            }
            if (celll12.Text == "Tồn ĐK" || celll12.Text == "Nhập" || celll12.Text == "Xuất" || celll12.Text == "Tồn CK")
            {
                cellll12.Summary.Func = SummaryFunc.Sum;
                cellll12.Summary.Running = SummaryRunning.Report;
                cellll12.DataBindings.Add("Text", DataSource, arr[7]).FormatString = DungChung.Bien.FormatString[0];
                cell12.DataBindings.Add("Text", DataSource, arr[7]).FormatString = DungChung.Bien.FormatString[0];
            }
            if (celll13.Text == "Tồn ĐK" || celll13.Text == "Nhập" || celll13.Text == "Xuất" || celll13.Text == "Tồn CK")
            {
                cellll13.Summary.Func = SummaryFunc.Sum;
                cellll13.Summary.Running = SummaryRunning.Report;
                cellll13.DataBindings.Add("Text", DataSource, arr[8]).FormatString = DungChung.Bien.FormatString[0];
                cell13.DataBindings.Add("Text", DataSource, arr[8]).FormatString = DungChung.Bien.FormatString[0];
            }
            if (celll14.Text == "Tồn ĐK" || celll14.Text == "Nhập" || celll14.Text == "Xuất" || celll14.Text == "Tồn CK")
            {
                cellll14.Summary.Func = SummaryFunc.Sum;
                cellll14.Summary.Running = SummaryRunning.Report;
                cellll14.DataBindings.Add("Text", DataSource, arr[9]).FormatString = DungChung.Bien.FormatString[0];
                cell14.DataBindings.Add("Text", DataSource, arr[9]).FormatString = DungChung.Bien.FormatString[0];
            }
            if (celll15.Text == "Tồn ĐK" || celll15.Text == "Nhập" || celll15.Text == "Xuất" || celll15.Text == "Tồn CK")
            {
                cellll15.Summary.Func = SummaryFunc.Sum;
                cellll15.Summary.Running = SummaryRunning.Report;
                cellll15.DataBindings.Add("Text", DataSource, arr[10]).FormatString = DungChung.Bien.FormatString[0];
                cell15.DataBindings.Add("Text", DataSource, arr[10]).FormatString = DungChung.Bien.FormatString[0];
            }
            if (celll16.Text == "Tồn ĐK" || celll16.Text == "Nhập" || celll16.Text == "Xuất" || celll16.Text == "Tồn CK")
            {
                cellll16.Summary.Func = SummaryFunc.Sum;
                cellll16.Summary.Running = SummaryRunning.Report;
                cellll16.DataBindings.Add("Text", DataSource, arr[11]).FormatString = DungChung.Bien.FormatString[0];
                cell16.DataBindings.Add("Text", DataSource, arr[11]).FormatString = DungChung.Bien.FormatString[0];
            }
            if (celll17.Text == "Tồn ĐK" || celll16.Text == "Nhập" || celll16.Text == "Xuất" || celll16.Text == "Tồn CK")
            {
                cellll17.Summary.Func = SummaryFunc.Sum;
                cellll17.Summary.Running = SummaryRunning.Report;
                cellll17.DataBindings.Add("Text", DataSource, arr[12]).FormatString = DungChung.Bien.FormatString[0];
                cell17.DataBindings.Add("Text", DataSource, arr[12]).FormatString = DungChung.Bien.FormatString[0];
            }
        }
                       

        private void TopMargin_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ;
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;
            celll1.Text = "Mã NB";
            celll2.Text = "Tên HC";
            celll3.Text = "Tên Dược";
            celll4.Text = "ĐVT";
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
  
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }
    }
}
