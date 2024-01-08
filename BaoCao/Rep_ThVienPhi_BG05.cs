using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_ThVienPhi_BG05 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_ThVienPhi_BG05()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            //txtMaKP.DataBindings.Add("Text", DataSource, "MaKP");
            colTenKP.DataBindings.Add("Text", DataSource, "TenKP");
            colSoBN.DataBindings.Add("Text", DataSource, "SoBN").FormatString = DungChung.Bien.FormatString[1];
            colSoBNT.DataBindings.Add("Text", DataSource, "SoBN").FormatString = DungChung.Bien.FormatString[1];
            colSoNDT.DataBindings.Add("Text", DataSource, "SoNDT").FormatString = DungChung.Bien.FormatString[1];
            colSoNDTT.DataBindings.Add("Text", DataSource, "SoNDT").FormatString = DungChung.Bien.FormatString[1];
            //colDV1.DataBindings.Add("Text", DataSource, "DV1").FormatString = DungChung.Bien.FormatString[1];
            //colDV2.DataBindings.Add("Text", DataSource, "DV2").FormatString = DungChung.Bien.FormatString[1];
            //colDV3.DataBindings.Add("Text", DataSource, "DV3").FormatString = DungChung.Bien.FormatString[1];
            //colDV4.DataBindings.Add("Text", DataSource, "DV4").FormatString = DungChung.Bien.FormatString[1];
            //colDV5.DataBindings.Add("Text", DataSource, "DV5").FormatString = DungChung.Bien.FormatString[1];
            //colDV6.DataBindings.Add("Text", DataSource, "DV6").FormatString = DungChung.Bien.FormatString[1];
            //colDV7.DataBindings.Add("Text", DataSource, "DV7").FormatString = DungChung.Bien.FormatString[1];
            //colDV8.DataBindings.Add("Text", DataSource, "DV8").FormatString = DungChung.Bien.FormatString[1];
            //colDV9.DataBindings.Add("Text", DataSource, "DV9").FormatString = DungChung.Bien.FormatString[1];
            //colDV10.DataBindings.Add("Text", DataSource, "DV10").FormatString = DungChung.Bien.FormatString[1];
            //colDV11.DataBindings.Add("Text", DataSource, "DV11").FormatString = DungChung.Bien.FormatString[1];
            //colDV12.DataBindings.Add("Text", DataSource, "DV12").FormatString = DungChung.Bien.FormatString[1];
            //colDV13.DataBindings.Add("Text", DataSource, "DV13").FormatString = DungChung.Bien.FormatString[1];
            //colDV14.DataBindings.Add("Text", DataSource, "DV14").FormatString = DungChung.Bien.FormatString[1];
            //colTong.DataBindings.Add("Text", DataSource, "TongTien").FormatString = DungChung.Bien.FormatString[1];

            colT1.DataBindings.Add("Text", DataSource, "DV1").FormatString = DungChung.Bien.FormatString[1];
            colT2.DataBindings.Add("Text", DataSource, "DV2").FormatString = DungChung.Bien.FormatString[1];
            colT3.DataBindings.Add("Text", DataSource, "DV3").FormatString = DungChung.Bien.FormatString[1];
            colT4.DataBindings.Add("Text", DataSource, "DV4").FormatString = DungChung.Bien.FormatString[1];
            colT5.DataBindings.Add("Text", DataSource, "DV5").FormatString = DungChung.Bien.FormatString[1];
            colT6.DataBindings.Add("Text", DataSource, "DV6").FormatString = DungChung.Bien.FormatString[1];
            colT7.DataBindings.Add("Text", DataSource, "DV7").FormatString = DungChung.Bien.FormatString[1];
            colT8.DataBindings.Add("Text", DataSource, "DV8").FormatString = DungChung.Bien.FormatString[1];
            colT9.DataBindings.Add("Text", DataSource, "DV9").FormatString = DungChung.Bien.FormatString[1];
            colT10.DataBindings.Add("Text", DataSource, "DV10").FormatString = DungChung.Bien.FormatString[1];
            colT11.DataBindings.Add("Text", DataSource, "DV11").FormatString = DungChung.Bien.FormatString[1];
            colT12.DataBindings.Add("Text", DataSource, "DV12").FormatString = DungChung.Bien.FormatString[1];
            colT13.DataBindings.Add("Text", DataSource, "DV13").FormatString = DungChung.Bien.FormatString[1];
            colT14.DataBindings.Add("Text", DataSource, "DV14").FormatString = DungChung.Bien.FormatString[1];
            colT15.DataBindings.Add("Text", DataSource, "DV15").FormatString = DungChung.Bien.FormatString[1];
            colT16.DataBindings.Add("Text", DataSource, "DV16").FormatString = DungChung.Bien.FormatString[1];
            colT17.DataBindings.Add("Text", DataSource, "DV17").FormatString = DungChung.Bien.FormatString[1];
            colT18.DataBindings.Add("Text", DataSource, "DV18").FormatString = DungChung.Bien.FormatString[1];
           // colT19.DataBindings.Add("Text", DataSource, "DV19").FormatString = DungChung.Bien.FormatString[1];
            colTongT.DataBindings.Add("Text", DataSource, "TongTien").FormatString = DungChung.Bien.FormatString[1];

        }

        private void colTenKP_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (this.GetCurrentColumnValue("MaKP").ToString() != null && GetCurrentColumnValue("MaKP").ToString()!="")
            //{
            //    _makp = GetCurrentColumnValue("MaKP").ToString();
            //    var q = from kp in dataContext.KPhongs.Where(p => p.MaKP==_makp) select new { kp.TenKP };
            //    if (q.Count() > 0)
            //    {
            //        colTenKP.Text = q.First().TenKP;
            //    }
            //    else colTenKP.Text = "";
            //}
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colGiamDoc.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            double v;
            if (this.GetCurrentColumnValue("DV1") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV1"));
                if (v == 0)
                {
                    colDV1.Text = "";
                }
                else { colDV1.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV2") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV2"));
                if (v == 0)
                {
                    colDV2.Text = "";
                }
                else { colDV2.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV3") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV3"));
                if (v == 0)
                {
                    colDV3.Text = "";
                }
                else { colDV3.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV4") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV4"));
                if (v == 0)
                {
                    colDV4.Text = "";
                }
                else { colDV4.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV5") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV5"));
                if (v == 0)
                {
                    colDV5.Text = "";
                }
                else { colDV5.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV6") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV6"));
                if (v == 0)
                {
                    colDV6.Text = "";
                }
                else { colDV6.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV7") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV7"));
                if (v == 0)
                {
                    colDV7.Text = "";
                }
                else { colDV7.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV8") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV8"));
                if (v == 0)
                {
                    colDV8.Text = "";
                }
                else { colDV8.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV9") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV9"));
                if (v == 0)
                {
                    colDV9.Text = "";
                }
                else { colDV9.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV10") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV10"));
                if (v == 0)
                {
                    colDV10.Text = "";
                }
                else { colDV10.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV11") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV11"));
                if (v == 0)
                {
                    colDV11.Text = "";
                }
                else { colDV11.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV12") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV12"));
                if (v == 0)
                {
                    colDV12.Text = "";
                }
                else { colDV12.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV13") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV13"));
                if (v == 0)
                {
                    colDV13.Text = "";
                }
                else { colDV13.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV14") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV14"));
                if (v == 0)
                {
                    colDV14.Text = "";
                }
                else { colDV14.Text = v.ToString("#,##"); }

            }
            if (this.GetCurrentColumnValue("DV15") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV15"));
                if (v == 0)
                {
                    colDV15.Text = "";
                }
                else { colDV15.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV16") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV16"));
                if (v == 0)
                {
                    colDV16.Text = "";
                }
                else { colDV16.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV17") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV17"));
                if (v == 0)
                {
                    colDV17.Text = "";
                }
                else { colDV17.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV18") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV18"));
                if (v == 0)
                {
                    colDV18.Text = "";
                }
                else { colDV18.Text = v.ToString("#,##"); }
            }

            if (this.GetCurrentColumnValue("tongtien") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("tongtien"));
                if (v == 0)
                {
                    colTong.Text = "";
                }
                else { colTong.Text = v.ToString("#,##"); }
            }
        }

        private void xrTable1_BeforePrint(object sender, CancelEventArgs e)
        {

        }

    }
}
