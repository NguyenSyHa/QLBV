using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QLBV_Library;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoiTMH_27183 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNoiSoiTMH_27183()
        {
            InitializeComponent();
            changeStyle();
        }

        public void changeStyle()
        {
            if (DungChung.Bien.MaBV == "27194")
            {
                xrTableCell3.Text = "Tai trái";
                xrTableCell19.Text = "Tai phải";
                xrTableCell35.Text = "Mũi";
                xrTableCell9.Text = "Họng";
                xrLabel22.Visible = false;
                xpic5.Visible = false;
                xrLabel17.LocationF = new Point(274, 0);
                xpic3.LocationF = new Point(274, 24);
            }
        }

        public void hienthi(string ketqua, string duongdan) 
        {
            string[] arrKQ = QLBV_Ham.LayChuoi('|', ketqua);
            string[] arrDD = QLBV_Ham.LayChuoi('|', duongdan);
          
            for (int i = 0; i < arrKQ.Length; i++)
            {
                string kq = arrKQ[i].Replace("\r\n", Environment.NewLine);
                //List<string> lkq = arrKQ[i].Split(';').ToList();                 
                // kq = string.Join(Environment.NewLine, lkq.Select(p => p));
                switch (i)
	            {
                    case 0:
                        celKQtai.Text = kq;
                        xpic1.ImageUrl = arrDD[i];
                        break;
                    case 1:
                        if(celKQtai.Text != null)
                        {
                            if (DungChung.Bien.MaBV != "27194")
                            {
                                celKQtai.Text = celKQtai.Text + " " + kq;
                            }
                            else
                            {
                                celKQmui.Text = kq;
                            }
                        }
                        else
                        {
                            celKQtai.Text = kq;
                        }                           
                        break;      
                    case 2:
                        if (DungChung.Bien.MaBV != "27194")
                        {
                            celKQmui.Text = kq;
                        }
                        else
                        {
                            celKQvom.Text =kq;
                        }
                        xpic3.ImageUrl = arrDD[i];

                        break;
                    case 3:
                        if (celKQmui.Text != null)
                        {
                            if (DungChung.Bien.MaBV != "27194")
                            {
                                celKQmui.Text = celKQmui.Text + " " + kq;
                            }
                        }
                        else
                        {
                            celKQmui.Text = kq;
                        }
                        break;
                    case 4:
                        //celKQhong.Text = kq;
                        //xpic5.ImageUrl = arrDD[i];
                        //break;
                        if (DungChung.Bien.MaBV != "27194")
                        {
                            celKQvom.Text = kq;
                        }
                        xpic5.ImageUrl = arrDD[i];
                        break;
                    case 5:
                        celKQhong.Text = kq;
                        xpic6.ImageUrl = arrDD[i];
                        break;

                }
            }
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27194")
            {
                SDT.Text = "Số điện thoại: 0989770704";
            }
          
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        }


        private void xrTableCell17_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void celKQ1_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27194")
            {
                xrLabel23.Text = "Họng - Thanh quản";
            }
        }
    }
}
