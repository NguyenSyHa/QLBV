
using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class repPhieuSieuAmThai_4D5D_12345 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuSieuAmThai_4D5D_12345()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV =="12345" || DungChung.Bien.MaBV == "24297")
            xrPictureBox6.Image = DungChung.Ham.GetLogo();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.DiaDanh != "")
            //    celDiaDanh.Text = DungChung.Bien.DiaDanh + ", ";
        }

        public void hienthiKetQua(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("</br>\n", ";");
                List<string> listKq = str.Split(';').ToList();
                foreach (XRTableRow row in xrTable1)
                {
                    foreach (XRTableCell cel in row)
                    {
                        for (int i = 0; i < 41; i++)
                        {
                            if (cel.Name == "tbc" + (i + 1).ToString() && listKq.Count > i)
                            {
                                cel.Text = "• " + listKq.Skip(i).First();
                            }
                        }

                    }
                }
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

            //this.xrPictureBox1.ImageUrl = DuongDan.Value.ToString();
            //if (paramDuongDan2.Value != null && !String.IsNullOrEmpty(paramDuongDan2.Value.ToString()))
            //{
            //    this.xrPictureBox2.ImageUrl = paramDuongDan2.Value.ToString();
            //}

            //if (Macb.Value != null)
            //{
            //    colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            //}

        }
    }
}
