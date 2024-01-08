using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;


namespace QLBV.BaoCao
{
    public partial class RepPhieuLuuHuyetNao_sub : DevExpress.XtraReports.UI.XtraReport
    {
     
        public RepPhieuLuuHuyetNao_sub()
        {
            InitializeComponent();
        }
        string kq = "";
        public RepPhieuLuuHuyetNao_sub(string ketqua)
        {
            InitializeComponent();
            kq = ketqua;
        }
        string[] td1 = new string[] { "α", "α/T(%)", "Am", "A/C", "VBC(ml/p)", "Ghi chú" }; // tiêu đề
       // string[] td2 = new string[] { "Q-a (Sec)", "α(Sec)", "β(Sec)", "T(Sec)", "α/T(Sec)", "A(mm)", "Chỉ số" }; 
        // int row = 0;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (kq != null)
            {
                //string dskq = this.GetCurrentColumnValue("KetQua").ToString();
                string dskq = kq;
                // nếu chuỗi kết thúc bằng dấu ";" thì bỏ dấu ";" đi
                if (dskq.Length >= 1 && dskq.Substring(dskq.Length - 1, 1) == ";")
                    dskq = dskq.Substring(0, dskq.Length - 1);
                //-------------------------------------------------------------------
                string[] arr = dskq.Split(';');

                foreach (XRTableCell cell in xrTableRow2)
                {
                    string celName = cell.Name;
                    for (int c = 1; c <= 6; c++)
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
