using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_SokhambenhA4_30002 : DevExpress.XtraReports.UI.XtraReport
    {
        int a = 0, _ngay=0;
        private bool htDotuoi = false;
        private bool htNgay = false;
        public Rep_SokhambenhA4_30002()
        {
            InitializeComponent();
        }
        public Rep_SokhambenhA4_30002(int _a, bool htDotuoi, bool htNgay,int ngay)
        {
            InitializeComponent();
            a = _a;
            _ngay = ngay;
            this.htDotuoi = htDotuoi;
            this.htNgay = htNgay;
        }
        public Rep_SokhambenhA4_30002(int _a)
        {
            InitializeComponent();
            a = _a;
           
        }
        public void BindingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
           // txtMaBN.DataBindings.Add("Text", DataSource, "maBN");
            colNam.DataBindings.Add("Text", DataSource, "tuoin");
            colNu.DataBindings.Add("Text", DataSource, "tuoinu");
            colDiachi.DataBindings.Add("Text", DataSource, "Diachi");
            colSothe.DataBindings.Add("Text", DataSource, "Sothe");
            //colNoiGT.DataBindings.Add("Text", DataSource, "NoiGT");
            //colCDTuyenduoi.DataBindings.Add("Text", DataSource, "CDNoiGT");
            if (a == 2)
            { colCDKKB.DataBindings.Add("Text", DataSource, "ticd"); }
            else
            { colCDKKB.DataBindings.Add("Text", DataSource, "noigt"); }
            colCD.DataBindings.Add("Text", DataSource, "cddt");
            colCDDT.DataBindings.Add("Text", DataSource, "ticd");
            colVVien.DataBindings.Add("Text", DataSource, "VV1");
            colTuyentren.DataBindings.Add("Text", DataSource, "TT1");
            colTuyenduoi.DataBindings.Add("Text", DataSource, "TD1");
            colNgoaitru.DataBindings.Add("Text", DataSource, "NT1");
            colVenha.DataBindings.Add("Text", DataSource, "VN1");
            //colTT.DataBindings.Add("Text", DataSource, "TT1");
            colChuyenkhoa.DataBindings.Add("Text", DataSource, "KhamCK1");
            colBS.DataBindings.Add("Text", DataSource, "TenBS");
            cell_ngayKham.DataBindings.Add("Text", DataSource, "ngay").FormatString = "{0:HH:mm dd/MM}";
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
            if (_ngay == 0)
                cell_TD_SoNgayDT.Text = "Ngày ra viện";
            else if (_ngay == 1)
                cell_TD_SoNgayDT.Text = "Ngày thanh toán";
            else if (_ngay == 2)
                cell_TD_SoNgayDT.Text = "Ngày khám";

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


    }
}
