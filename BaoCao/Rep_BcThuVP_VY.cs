using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BcThuVP_VY : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcThuVP_VY()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        public void BindingData()
        {
            colTenTN.DataBindings.Add("Text", DataSource, "TenTN");
            txt1.DataBindings.Add("Text", DataSource, "ST1").FormatString = DungChung.Bien.FormatString[1];
            txt2.DataBindings.Add("Text", DataSource, "ST2").FormatString = DungChung.Bien.FormatString[1];
            txt3.DataBindings.Add("Text", DataSource, "ST3").FormatString = DungChung.Bien.FormatString[1];
            txt4.DataBindings.Add("Text", DataSource, "ST4").FormatString = DungChung.Bien.FormatString[1];
            txt5.DataBindings.Add("Text", DataSource, "ST5").FormatString = DungChung.Bien.FormatString[1];
            txt5.DataBindings.Add("Text", DataSource, "ST6").FormatString = DungChung.Bien.FormatString[1];
            txt7.DataBindings.Add("Text", DataSource, "ST7").FormatString = DungChung.Bien.FormatString[1];
            txt8.DataBindings.Add("Text", DataSource, "ST8").FormatString = DungChung.Bien.FormatString[1];
            txt9.DataBindings.Add("Text", DataSource, "ST9").FormatString = DungChung.Bien.FormatString[1];
            txt10.DataBindings.Add("Text", DataSource, "ST10").FormatString = DungChung.Bien.FormatString[1];
            txt11.DataBindings.Add("Text", DataSource, "ST11").FormatString = DungChung.Bien.FormatString[1];
            txt12.DataBindings.Add("Text", DataSource, "ST12").FormatString = DungChung.Bien.FormatString[1];
            txt13.DataBindings.Add("Text", DataSource, "ST13").FormatString = DungChung.Bien.FormatString[1];
            colKPTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            colKPTC1.DataBindings.Add("Text", DataSource, "ST1").FormatString = DungChung.Bien.FormatString[1];
            colKPTC2.DataBindings.Add("Text", DataSource, "ST2").FormatString = DungChung.Bien.FormatString[1];
            colKPTC3.DataBindings.Add("Text", DataSource, "ST3").FormatString = DungChung.Bien.FormatString[1];
            colKPTC4.DataBindings.Add("Text", DataSource, "ST4").FormatString = DungChung.Bien.FormatString[1];
            colKPTC5.DataBindings.Add("Text", DataSource, "ST5").FormatString = DungChung.Bien.FormatString[1];
            colKPTC6.DataBindings.Add("Text", DataSource, "ST6").FormatString = DungChung.Bien.FormatString[1];
            colKPTC7.DataBindings.Add("Text", DataSource, "ST7").FormatString = DungChung.Bien.FormatString[1];
            colKPTC8.DataBindings.Add("Text", DataSource, "ST8").FormatString = DungChung.Bien.FormatString[1];
            colKPTC9.DataBindings.Add("Text", DataSource, "ST9").FormatString = DungChung.Bien.FormatString[1];
            colKPTC10.DataBindings.Add("Text", DataSource, "ST10").FormatString = DungChung.Bien.FormatString[1];
            colKPTC11.DataBindings.Add("Text", DataSource, "ST11").FormatString = DungChung.Bien.FormatString[1];
            colKPTC12.DataBindings.Add("Text", DataSource, "ST12").FormatString = DungChung.Bien.FormatString[1];
            colKPTC13.DataBindings.Add("Text", DataSource, "ST13").FormatString = DungChung.Bien.FormatString[1];
            colKPTCTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
  
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colTTDV.Text = DungChung.Bien.GiamDoc;
            colKTT.Text = DungChung.Bien.KeToanTruong;
            colNLB.Text = DungChung.Bien.KeToanTruong;
        }

        private void xrLabel1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("st1") != null)
            { double t = Convert.ToDouble(this.GetCurrentColumnValue("st1"));
            if (t == 0)
            { colKP1.Text = ""; }
            else
            { colKP1.Text = t.ToString("#,#"); }
            }
            if (this.GetCurrentColumnValue("st2") != null)
            {
                double t = Convert.ToDouble(this.GetCurrentColumnValue("st2"));
                if (t == 0)
                { colKP2.Text = ""; }
                else
                { colKP2.Text = t.ToString("#,#"); }
            }
            if (this.GetCurrentColumnValue("st3") != null)
            {
                double t = Convert.ToDouble(this.GetCurrentColumnValue("st3"));
                if (t == 0)
                { colKP3.Text = ""; }
                else
                { colKP3.Text = t.ToString("#,#"); }
            }
            if (this.GetCurrentColumnValue("st4") != null)
            {
                double t = Convert.ToDouble(this.GetCurrentColumnValue("st4"));
                if (t == 0)
                { colKP4.Text = ""; }
                else
                { colKP4.Text = t.ToString("#,#"); }
            }
            if (this.GetCurrentColumnValue("st5") != null)
            {
                double t = Convert.ToDouble(this.GetCurrentColumnValue("st5"));
                if (t == 0)
                { colKP5.Text = ""; }
                else
                { colKP5.Text = t.ToString("#,#"); }
            }
            if (this.GetCurrentColumnValue("st6") != null)
            {
                double t = Convert.ToDouble(this.GetCurrentColumnValue("st6"));
                if (t == 0)
                { colKP6.Text = ""; }
                else
                { colKP6.Text = t.ToString("#,#"); }
            }
            if (this.GetCurrentColumnValue("st7") != null)
            {
                double t = Convert.ToDouble(this.GetCurrentColumnValue("st7"));
                if (t == 0)
                { colKP7.Text = ""; }
                else
                { colKP7.Text = t.ToString("#,#"); }
            }
            if (this.GetCurrentColumnValue("st8") != null)
            {
                double t = Convert.ToDouble(this.GetCurrentColumnValue("st8"));
                if (t == 0)
                { colKP8.Text = ""; }
                else
                { colKP8.Text = t.ToString("#,#"); }
            }
            if (this.GetCurrentColumnValue("st9") != null)
            {
                double t = Convert.ToDouble(this.GetCurrentColumnValue("st9"));
                if (t == 0)
                { colKP9.Text = ""; }
                else
                { colKP9.Text = t.ToString("#,#"); }
            }
            if (this.GetCurrentColumnValue("st10") != null)
            {
                double t = Convert.ToDouble(this.GetCurrentColumnValue("st9"));
                if (t == 0)
                { colKP10.Text = ""; }
                else
                { colKP10.Text = t.ToString("#,#"); }
            }
            if (this.GetCurrentColumnValue("st11") != null)
            {
                double t = Convert.ToDouble(this.GetCurrentColumnValue("st11"));
                if (t == 0)
                { colKP11.Text = ""; }
                else
                { colKP11.Text = t.ToString("#,#"); }
            }
            if (this.GetCurrentColumnValue("st12") != null)
            {
                double t = Convert.ToDouble(this.GetCurrentColumnValue("st12"));
                if (t == 0)
                { colKP12.Text = ""; }
                else
                { colKP12.Text = t.ToString("#,#"); }
            }
            if (this.GetCurrentColumnValue("st13") != null)
            {
                double t = Convert.ToDouble(this.GetCurrentColumnValue("st13"));
                if (t == 0)
                { colKP13.Text = ""; }
                else
                { colKP13.Text = t.ToString("#,#"); }
            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            int makp1=0;
            if (this.MaKP1.Value != null)
            {
                makp1 = Convert.ToInt32( this.MaKP1.Value);
            }
            int makp2 = 0;
            if (this.MaKP2.Value != null)
            {
                makp2 =Convert.ToInt32( this.MaKP2.Value);
            }
            int makp3 = 0;
            if (this.MaKP3.Value != null)
            {
                makp3 = Convert.ToInt32( this.MaKP3.Value);
            }
            int makp4 = 0;
            if (this.MaKP4.Value != null)
            {
                makp4 = Convert.ToInt32( this.MaKP4.Value);
            }
            int makp5 = 0;
            if (this.MaKP5.Value != null)
            {
                makp5 =Convert.ToInt32( this.MaKP5.Value);
            }
            int makp6 = 0;
            if (this.MaKP6.Value != null)
            {
                makp6 = Convert.ToInt32( this.MaKP6.Value);
            }
            int makp7 = 0;
            if (this.MaKP7.Value != null)
            {
                makp7 = Convert.ToInt32( this.MaKP7.Value);
            }
            int makp8 = 0;
            if (this.MaKP8.Value != null)
            {
                makp8 = Convert.ToInt32( this.MaKP8.Value);
            }
            int makp9 = 0;
            if (this.MaKP9.Value != null)
            {
                makp9 = Convert.ToInt32( this.MaKP9.Value);
            }
            int makp10 = 0;
            if (this.MaKP10.Value != null)
            {
                makp10 =Convert.ToInt32( this.MaKP10.Value);
            }
            int makp11 = 0;
            if (this.MaKP11.Value != null)
            {
                makp11 = Convert.ToInt32( this.MaKP11.Value);
            }
            int makp12 = 0;
            if (this.MaKP12.Value != null)
            {
                makp12 =Convert.ToInt32( this.MaKP12.Value);
            }
            //string makp13 = "";
            //if (this.MaKP13.Value != null)
            //{
            //    makp13 = this.MaKP13.Value.ToString();
            //}
            var qndt = (from kb in _Data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay)
                        group new { kb} by new { kb.MaKP } into kq
                        select new { MaKP = kq.Key.MaKP, SoBN = kq.Select(p=>p.kb.MaBNhan).Count() }).ToList();
            if (qndt.Count > 0)
            {
                colBNDT1.Text = qndt.Where(p => p.MaKP == makp1).Sum(p => p.SoBN).ToString();
                colBNDT2.Text = qndt.Where(p => p.MaKP == makp2).Sum(p => p.SoBN).ToString();
                colBNDT3.Text = qndt.Where(p => p.MaKP == makp3).Sum(p => p.SoBN).ToString();
                colBNDT4.Text = qndt.Where(p => p.MaKP == makp4).Sum(p => p.SoBN).ToString();
                colBNDT5.Text = qndt.Where(p => p.MaKP == makp5).Sum(p => p.SoBN).ToString();
                colBNDT6.Text = qndt.Where(p => p.MaKP == makp6).Sum(p => p.SoBN).ToString();
                colBNDT7.Text = qndt.Where(p => p.MaKP == makp7).Sum(p => p.SoBN).ToString();
                colBNDT8.Text = qndt.Where(p => p.MaKP == makp8).Sum(p => p.SoBN).ToString();
                colBNDT9.Text = qndt.Where(p => p.MaKP == makp9).Sum(p => p.SoBN).ToString();
                colBNDT10.Text = qndt.Where(p => p.MaKP == makp10).Sum(p => p.SoBN).ToString();
                colBNDT11.Text = qndt.Where(p => p.MaKP == makp11).Sum(p => p.SoBN).ToString();
                colBNDT12.Text = qndt.Where(p => p.MaKP == makp12).Sum(p => p.SoBN).ToString();
                
            }
            else
            {
                colBNDT1.Text = "";
                colBNDT2.Text = "";
                colBNDT3.Text = "";
                colBNDT4.Text = "";
                colBNDT5.Text = "";
                colBNDT6.Text = "";
                colBNDT7.Text = "";
                colBNDT8.Text = "";
                colBNDT9.Text = "";
                colBNDT10.Text = "";
                colBNDT11.Text = "";
                colBNDT12.Text = "";
            }
            if (this.NNT.Value.ToString() == "1")
            {
                var qngt = (from kb in _Data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay)
                            join kp in _Data.KPhongs.Where(p => p.PLoai.Contains("Phòng khám")) on kb.MaKP equals kp.MaKP
                            group new { kb } by new { kb.MaKP } into kq
                            select new {  SoBN = kq.Select(p => p.kb.MaBNhan).Count() }).ToList();
                if (qngt.Count > 0)
                {
                    colBNDT13.Text = qngt.Sum(p => p.SoBN).ToString();
                }
                else { colBNDT13.Text = ""; }
            }
       
        }

    }
}
