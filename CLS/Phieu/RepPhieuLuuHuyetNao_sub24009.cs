using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;


namespace QLBV.BaoCao
{
    public partial class RepPhieuLuuHuyetNao_sub24009 : DevExpress.XtraReports.UI.XtraReport
    {
        public RepPhieuLuuHuyetNao_sub24009()
        {
            InitializeComponent();
        }
        string[] td1 = new string[] { "a(giây) 0,16-0,20", "b(giây) 0,05-0,13", "c(giây) 0,5-0,80", "c(giây) 0,55-0,93", "b/T % 10-20", "Amin", "Bmin", "Chỉ số LHN >= 1", "Vml/ph 192-364" }; // tiêu đề
       // string[] td2 = new string[] { "Q-a (Sec)", "α(Sec)", "β(Sec)", "T(Sec)", "α/T(Sec)", "A(mm)", "Chỉ số" }; 
        // int row = 0;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("KetQua") != null)
            {
                string dskq = this.GetCurrentColumnValue("KetQua").ToString();
                // nếu chuỗi kết thúc bằng dấu ";" thì bỏ dấu ";" đi
                if (dskq.Length > 0 && dskq.Substring(dskq.Length - 1, 1) == ";")
                    dskq = dskq.Substring(0, dskq.Length - 1);
                //-------------------------------------------------------------------
                string[] arr = dskq.Split(';');

                foreach (XRTableCell cell in xrTableRow2)
                {
                    string celName = cell.Name;
                    for (int c = 1; c <= 9; c++)
                    {
                        int num = c - 1;
                        bool kt = num < arr.Length;
                        if (kt)
                        {
                            if (celName == "cel1" + c.ToString())
                            {
                                cell.Text = arr[num];
                                break;
                            }
                        }
                        else
                        {
                            cell.Text = "";
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (XRTableCell cell in xrTableRow2)
                {                    
                        if (cell.Name != "celDvct")
                        {
                            cell.Text = "";
                        }                    
                }
            }

        }



        internal void dataBinding()
        {
            celDvct.DataBindings.Add("Text", DataSource, "TenDVct");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
            foreach (XRTableCell cell in xrTableRow3)
            {
                for (int i = 1; i <= td1.Length; i++)
                {
                    string celName = cell.Name;

                    if (celName == "celTit" + i.ToString())
                    {
                        cell.Text = td1[i - 1];
                    }

                }
            }
        }
    }
}
