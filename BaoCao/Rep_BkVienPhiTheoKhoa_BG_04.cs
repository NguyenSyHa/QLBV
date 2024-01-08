using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BkVienPhiTheoKhoa_BG_04 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BkVienPhiTheoKhoa_BG_04()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            txtMaBNhan.DataBindings.Add("Text", DataSource, "MaBN");
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBN");
            colDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            colNgayVao.DataBindings.Add("Text", DataSource, "NgayVao");
            colNgayRV.DataBindings.Add("Text", DataSource, "NgayRa");
            colSoNDT.DataBindings.Add("Text", DataSource, "SoNDT");
            //colDe1.DataBindings.Add("Text", DataSource, "DV1").FormatString = DungChung.Bien.FormatString[1];
            //colDe2.DataBindings.Add("Text", DataSource, "DV2").FormatString = DungChung.Bien.FormatString[1];
            //colDe3.DataBindings.Add("Text", DataSource, "DV3").FormatString = DungChung.Bien.FormatString[1];
            //colDe4.DataBindings.Add("Text", DataSource, "DV4").FormatString = DungChung.Bien.FormatString[1];
            //colDe5.DataBindings.Add("Text", DataSource, "DV5").FormatString = DungChung.Bien.FormatString[1];
            //colDe6.DataBindings.Add("Text", DataSource, "DV6").FormatString = DungChung.Bien.FormatString[1];
            //colDe7.DataBindings.Add("Text", DataSource, "DV7").FormatString = DungChung.Bien.FormatString[1];
            //colDe8.DataBindings.Add("Text", DataSource, "DV8").FormatString = DungChung.Bien.FormatString[1];
            //colDe9.DataBindings.Add("Text", DataSource, "DV9").FormatString = DungChung.Bien.FormatString[1];
            //colDe10.DataBindings.Add("Text", DataSource, "DV10").FormatString = DungChung.Bien.FormatString[1];
            //colDe11.DataBindings.Add("Text", DataSource, "DV11").FormatString = DungChung.Bien.FormatString[1];
            //colDe12.DataBindings.Add("Text", DataSource, "DV12").FormatString = DungChung.Bien.FormatString[1];
            //colDe13.DataBindings.Add("Text", DataSource, "DV13").FormatString = DungChung.Bien.FormatString[1];
            //colTong.DataBindings.Add("Text", DataSource, "TongTien").FormatString = DungChung.Bien.FormatString[1];

            colSoNDTT.DataBindings.Add("Text", DataSource, "SoNDT");
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
            colTongT.DataBindings.Add("Text", DataSource, "TongTien").FormatString = DungChung.Bien.FormatString[1];

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
         }

        private void colNgayVV_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (GetCurrentColumnValue("NgayVao")!=null && GetCurrentColumnValue("NgayVao").ToString().Length>10)
            //{
            //    colNgayVao.Text = txtNgayVao.Text.Substring(0,5);
            //}
        }

        private void colNgayRV_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (GetCurrentColumnValue("NgayRa") != null && GetCurrentColumnValue("NgayRa").ToString().Length > 10)
            //{
            //    colNgayRV.Text = txtNgayRa.Text.Substring(0, 5);
            //}
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
                    colDe1.Text = "";
                }
                else { colDe1.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV2") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV2"));
                if (v == 0)
                {
                    colDe2.Text = "";
                }
                else { colDe2.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV3") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV3"));
                if (v == 0)
                {
                    colDe3.Text = "";
                }
                else { colDe3.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV4") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV4"));
                if (v == 0)
                {
                    colDe4.Text = "";
                }
                else { colDe4.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV5") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV5"));
                if (v == 0)
                {
                    colDe5.Text = "";
                }
                else { colDe5.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV6") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV6"));
                if (v == 0)
                {
                    colDe6.Text = "";
                }
                else { colDe6.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV7") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV7"));
                if (v == 0)
                {
                    colDe7.Text = "";
                }
                else { colDe7.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV8") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV8"));
                if (v == 0)
                {
                    colDe8.Text = "";
                }
                else { colDe8.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV9") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV9"));
                if (v == 0)
                {
                    colDe9.Text = "";
                }
                else { colDe9.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV10") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV10"));
                if (v == 0)
                {
                    colDe10.Text = "";
                }
                else { colDe10.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV11") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV11"));
                if (v == 0)
                {
                    colDe11.Text = "";
                }
                else { colDe11.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV12") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV12"));
                if (v == 0)
                {
                    colDe12.Text = "";
                }
                else { colDe12.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV13") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV13"));
                if (v == 0)
                {
                    colDe13.Text = "";
                }
                else { colDe13.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV14") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV14"));
                if (v == 0)
                {
                    colDe14.Text = "";
                }
                else { colDe14.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV15") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV15"));
                if (v == 0)
                {
                    colDe15.Text = "";
                }
                else { colDe15.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV16") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV16"));
                if (v == 0)
                {
                    colDe16.Text = "";
                }
                else { colDe16.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV17") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV17"));
                if (v == 0)
                {
                    colDe17.Text = "";
                }
                else { colDe17.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("DV18") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("DV18"));
                if (v == 0)
                {
                    colDe18.Text = "";
                }
                else { colDe18.Text = v.ToString("#,##"); }
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
    }
}
