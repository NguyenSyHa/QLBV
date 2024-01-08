using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;


namespace QLBV.BaoCao
{
    public partial class RepPhieuLuuHuyetNao_sub_19048 : DevExpress.XtraReports.UI.XtraReport
    {
        public RepPhieuLuuHuyetNao_sub_19048()
        {
            InitializeComponent();
        }     
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
                    for (int c = 1; c <= 7; c++)
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
                    string celName = cell.Name;

                    if (cell.Name != "celDvct")
                        cell.Text = "";
                }
            }

        }
        internal void dataBinding()
        {
            celDvct.DataBindings.Add("Text", DataSource, "TenDVct");
        }

     
    }
}
