
using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class repPhieuSieuAm_Mau4D_27183_2Thai : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuSieuAm_Mau4D_27183_2Thai()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];  
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
                str = str.Replace( "</br>\n", ";");
                List<string> listKq = str.Split(';').ToList();
                foreach (XRTableRow row in xrTable1)
                {
                    foreach (XRTableCell cel in row)
                    {
                        for (int i = 0; i < 35; i++)
                        {
                            if (cel.Name == "cel" + (i + 1).ToString() && listKq.Count > i)
                            {
                                cel.Text = listKq.Skip(i).First();
                            }
                        }

                    }
                }
            }
        }

        public void hienthiKetQua27183(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                //str = str.Replace("</br>\n", ";");
                List<string> listKq = str.Split(';').ToList();
                foreach (XRTableRow row in xrTable1)
                {
                    foreach (XRTableCell cel in row)
                    {
                        for (int i = 0; i < 35; i++)
                        {
                            if (cel.Name == "cel" + (i + 1).ToString() && listKq.Count > i)
                            {
                                cel.Text = listKq.Skip(i).First();
                            }
                        }

                    }
                }
            }
        }
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
           
            this.xrPictureBox1.ImageUrl = DuongDan.Value.ToString();
            if (paramDuongDan2.Value != null && !String.IsNullOrEmpty(paramDuongDan2.Value.ToString()))
            {
                this.xrPictureBox2.ImageUrl = paramDuongDan2.Value.ToString();
            }
            
            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
           
        }
    }
}
