using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;


namespace QLBV.BaoCao
{
    public partial class RepPhieuLuuHuyetNao_sub_DopplerXuyenSo : DevExpress.XtraReports.UI.XtraReport
    {
        private int lengthSource = 0;
        public RepPhieuLuuHuyetNao_sub_DopplerXuyenSo()
        {
            InitializeComponent();
        }
        string[] td1 = CLS.InPhieu.td_lhn;
       // string[] td2 = new string[] { "Q-a (Sec)", "α(Sec)", "β(Sec)", "T(Sec)", "α/T(Sec)", "A(mm)", "Chỉ số" }; 
        // int row = 0;
        int count = 0;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            count++;
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
                    for (int c = 1; c <= 10; c++)
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
                if (count >= lengthSource)
                {
                    int k = 0;
                    string _strDVcuoi = "";
                    string _strDetail = "";
                    foreach (XRTableCell cell in xrTableRow2)
                    {
                        k++;
                        if (cell.Name == "celDvct")
                        {
                            //lblTenDV.Text = this.GetCurrentColumnValue("TenDVct") == null ? "" : this.GetCurrentColumnValue("TenDVct").ToString();
                            _strDVcuoi += this.GetCurrentColumnValue("TenDVct") == null ? "" : this.GetCurrentColumnValue("TenDVct").ToString() + " :";
                        }
                    }

                    // string celName = cell.Name;
                    for (int c = 1; c <= 10; c++)
                    {
                        int num = c - 1;
                        bool kt = num < arr.Length;
                        if (kt)
                        {
                            if (arr[num] != "")
                            {
                                _strDetail += td1[num] + ": " + arr[num] + ". ";
                            }
                        }

                    }
                    lblTenDV.Text = _strDVcuoi;
                    lblDetails.Text = _strDetail;
                    lblTenDV.Visible = false;
                    //xrTableRow2.Visible = false;
                }

                else
                    xrTableRow2.Visible = true;
            }
            else
            {
                foreach (XRTableCell cell in xrTableRow2)
                {
                    string celName = cell.Name;
                        
                        if(cell.Name != "celDvct")
                            cell.Text = "";
                }
            }

        }



        internal void dataBinding(int lengthSource)
        {
            celDvct.DataBindings.Add("Text", DataSource, "TenDVct");
            this.lengthSource = lengthSource;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
            foreach (XRTableCell cell in xrTableRow4)
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
