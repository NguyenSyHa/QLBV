using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SokhambenhA4_TT27 : DevExpress.XtraReports.UI.XtraReport
    {
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int a = 0;
        private bool htDotuoi = false;
        private bool htNgay = false;
        public Rep_SokhambenhA4_TT27()
        {
            InitializeComponent();
        }
        public Rep_SokhambenhA4_TT27(int _a, bool htDotuoi, bool htNgay)
        {
            InitializeComponent();
            a = _a;
            this.htDotuoi = htDotuoi;
            this.htNgay = htNgay;
        }
        public Rep_SokhambenhA4_TT27(int _a)
        {
            InitializeComponent();
            a = _a;
           
        }
        public void BindingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN");        
            colNam.DataBindings.Add("Text", DataSource, "tuoin");
            colNu.DataBindings.Add("Text", DataSource, "tuoinu");
            colDiachi.DataBindings.Add("Text", DataSource, "Diachi");
            colSothe.DataBindings.Add("Text", DataSource, "Sothe");
            colNgheNghiep.DataBindings.Add("Text", DataSource, "NgheNgiep1");
            colDanToc.DataBindings.Add("Text", DataSource, "DanToc1");
            colTrieuChung.DataBindings.Add("Text", DataSource, "TrieuChung1");
            colChanDoan.DataBindings.Add("Text", DataSource, "ticd");
            colPPDT.DataBindings.Add("Text", DataSource, "Sothe");
            colBS.DataBindings.Add("Text", DataSource, "TenBS");
            cell_GhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
            colPPDT.DataBindings.Add("Text", DataSource, "cddt");
          
            if (htDotuoi)
            {
                celDoTuoi.DataBindings.Add("Text", DataSource, "DoTuoi");
                GroupHeader2.GroupFields.Add(new GroupField("DoTuoi"));
            }

            if (htNgay)
            {
                celNgayKham.DataBindings.Add("Text", DataSource, "nkb").FormatString = "{0: dd/MM/yyyy}";
                GroupHeader1.GroupFields.Add(new GroupField("nkb"));

            }

        }

        
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            if (DungChung.Bien.MaBV == "30002")
                xrLabel1.Text = "";
            //if (htNgay)
            //{
            //    DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            //    xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            //    xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            //    this.colSTT.Summary = xrSummary1;
            //}
            //else
            //{
            //    DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            //    xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            //    xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            //    this.colSTT.Summary = xrSummary1;
            //}
        }

        private void xrTableCell4_BeforePrint(object sender, CancelEventArgs e)
        {
            if (a == 2)
            { xrTableCell7.Text = "Chẩn đoán"; }
            else
            { xrTableCell7.Text = "Nơi giới thiệu"; }
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (htDotuoi)
                GroupHeader2.Visible = true;
            else
                GroupHeader2.Visible = false;
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (htNgay)
                GroupHeader1.Visible = true;
            else
                GroupHeader1.Visible = false;

            _stt = 1;
        }
        int _stt = 1;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            colSTT.Text = _stt.ToString();
            _stt++;
        }

        private void colNam_BeforePrint(object sender, CancelEventArgs e)
        {
            int mabn = 0;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && colNam.Text != "")
            {
                var bn = _data.BenhNhans.Where(p => p.SThe == colSothe.Text).First();
                if (bn != null)
                {
                    mabn = bn.MaBNhan;
                }
                colNam.Text = DungChung.Ham.TuoitheoThang(_data, mabn, "12-30");
            }
        }

        private void colNu_BeforePrint(object sender, CancelEventArgs e)
        {
            int mabn = 0;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && colNu.Text != "")
            {
                var bn = _data.BenhNhans.Where(p => p.SThe == colSothe.Text).First();
                if (bn != null)
                {
                    mabn = bn.MaBNhan;
                }
                colNu.Text = DungChung.Ham.TuoitheoThang(_data, mabn, "12-30");
            }
        }
    }
}
