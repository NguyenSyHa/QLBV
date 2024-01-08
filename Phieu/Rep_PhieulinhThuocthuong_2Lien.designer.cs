namespace QLBV.BaoCao
{
    partial class Rep_PhieulinhThuocthuong_2Lien
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.SoPL = new DevExpress.XtraReports.Parameters.Parameter();
            this.Ngaythang = new DevExpress.XtraReports.Parameters.Parameter();
            this.KhoLinh = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrSubreport2 = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubreport1 = new DevExpress.XtraReports.UI.XRSubreport();
            this.rep_SubPLLien11 = new QLBV.BaoCao.Rep_SubPLLien1();
            this.LoaiPhieu = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.rep_SubPLLien11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreport2,
            this.xrSubreport1});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 22.29166F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 23.95833F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportFooter
            // 
            this.ReportFooter.HeightF = 0F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // SoPL
            // 
            this.SoPL.Name = "SoPL";
            // 
            // Ngaythang
            // 
            this.Ngaythang.Name = "Ngaythang";
            // 
            // KhoLinh
            // 
            this.KhoLinh.Name = "KhoLinh";
            // 
            // xrSubreport2
            // 
            this.xrSubreport2.Id = 0;
            this.xrSubreport2.LocationFloat = new DevExpress.Utils.PointFloat(548.125F, 0F);
            this.xrSubreport2.Name = "xrSubreport2";
            this.xrSubreport2.ReportSource = new QLBV.BaoCao.Rep_SubPLLien2();
            this.xrSubreport2.SizeF = new System.Drawing.SizeF(539.8749F, 23F);
            this.xrSubreport2.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.xrSubreport2_BeforePrint);
            // 
            // xrSubreport1
            // 
            this.xrSubreport1.Id = 0;
            this.xrSubreport1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrSubreport1.Name = "xrSubreport1";
            this.xrSubreport1.ReportSource = new QLBV.BaoCao.Rep_SubPLLien1();
            this.xrSubreport1.SizeF = new System.Drawing.SizeF(539.8748F, 25F);
            this.xrSubreport1.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.xrSubreport1_BeforePrint);
            // 
            // rep_SubPLLien11
            // 
            this.rep_SubPLLien11.Margins = new System.Drawing.Printing.Margins(11, 11, 17, 100);
            this.rep_SubPLLien11.Name = "rep_SubPLLien11";
            this.rep_SubPLLien11.PageHeight = 827;
            this.rep_SubPLLien11.PageWidth = 583;
            this.rep_SubPLLien11.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A5;
            this.rep_SubPLLien11.Version = "13.1";
            // 
            // LoaiPhieu
            // 
            this.LoaiPhieu.Name = "LoaiPhieu";
            // 
            // Rep_PhieulinhThuocthuong_2Lien
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportFooter});
            this.Landscape = true;
            this.Margins = new DevExpress.Drawing.DXMargins(39, 40, 22, 24);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.SoPL,
            this.Ngaythang,
            this.KhoLinh,
            this.LoaiPhieu});
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.rep_SubPLLien11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport2;
        public DevExpress.XtraReports.UI.XRSubreport xrSubreport1;
        public DevExpress.XtraReports.Parameters.Parameter SoPL;
        private Rep_SubPLLien1 rep_SubPLLien11;
        public DevExpress.XtraReports.Parameters.Parameter Ngaythang;
        public DevExpress.XtraReports.Parameters.Parameter KhoLinh;
        public DevExpress.XtraReports.Parameters.Parameter LoaiPhieu;
    }
}
