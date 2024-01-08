using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Data;
using System.Collections.Generic;
namespace QLBV.BaoCao
{
    public partial class rep_ChamCongPhauThuat : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_ChamCongPhauThuat()
        {
            InitializeComponent();
        }
        //List<CanBo> _lcanbo = new List<CanBo>();
        public void BindingData() 
        {
            colTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            colBHYT.DataBindings.Add("Text", DataSource, "SThe");
            colPhauThuat.DataBindings.Add("Text", DataSource, "TenDV");
            if (Convert.ToInt32(DeTrang.Value) != 1)
            {
                colNgayLam.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0:dd/MM}";
                colPTVChinh.DataBindings.Add("Text", DataSource, "TenCB");
                colLoai.DataBindings.Add("Text", DataSource, "Loai");
            }
        }

        private void colPTVChinh_BeforePrint(object sender, CancelEventArgs e)
        {
            //string macb = "";
            //if (GetCurrentColumnValue("MaCB") != null)
            //    macb = GetCurrentColumnValue("MaCB").ToString();
            //_lcanbo.Where(p => p.MaCB== (macb)).Select(p => p.TenCB).ToList();
            //if (_lcanbo.Count > 0)
            //    colPTVChinh.Text = _lcanbo.First().TenCB;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //_lcanbo = _data.CanBoes.ToList();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("DSCBTH") != null)
	        {
		        string dscb = this.GetCurrentColumnValue("DSCBTH").ToString();
                string[] arrDSCB = QLBV_Library.QLBV_Ham.LayChuoi(';', dscb);
                string[] tenCB = new string[10];
                for (int i = 0; i < arrDSCB.Count(); i++)
                {
                    switch (i)
                    {
                        case 0:
                            tenCB = arrDSCB[0].Split(' ');
                            if (tenCB.Count() >= 4)
                            {
                                colPTVPhu.Text = tenCB[tenCB.Count() - 2] + " " + tenCB[tenCB.Count() - 1];
                            }
                            else
                            {
                                colPTVPhu.Text = tenCB[tenCB.Count() - 1];
                            }
                            break;
                        case 1:
                            tenCB = arrDSCB[1].Split(' ');
                            if (tenCB.Count() >= 4)
                            {
                                colGMChinh.Text = tenCB[tenCB.Count() - 2] + " " + tenCB[tenCB.Count() - 1];
                            }
                            else
                            {
                                colGMChinh.Text = tenCB[tenCB.Count() - 1];
                            }
                            break;
                        case 2:
                            tenCB = arrDSCB[2].Split(' ');
                            if (tenCB.Count() >= 4)
                            {
                                colGMPhu.Text = tenCB[tenCB.Count() - 2] + " " + tenCB[tenCB.Count() - 1];
                            }
                            else
                            {
                                colGMPhu.Text = tenCB[tenCB.Count() - 1 ];
                            }
                            break;
                        case 3:
                            tenCB = arrDSCB[3].Split(' ');
                            if (tenCB.Count() >= 4)
                            {
                                colGiupViec.Text = tenCB[tenCB.Count() - 2] + " " + tenCB[tenCB.Count() - 1];
                            }
                            else
                            {
                                colGiupViec.Text = tenCB[tenCB.Count() - 1];
                            }
                            break;
                        default:
                            break;
                    }
                }
	        }
          
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12121")
                SubBand1.Visible = true;
        }

        private void SubBand2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022")
            {
                xrLabel6.Visible = false;
                xrLabel7.Visible = false;
                xrTableCell14.Text = "PHÒNG KẾ HOẠCH TỔNG HỢP";
                xrTableCell16.Text = "GIÁM ĐỐC";
            }
        }
    }
}
