namespace QLBV.BaoCao
{
    partial class Rep_bia_SoTHCLS
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
            this.label4 = new DevExpress.XtraReports.UI.XRLabel();
            this.KPTH = new DevExpress.XtraReports.Parameters.Parameter();
            this.crossBandBox2 = new DevExpress.XtraReports.UI.XRCrossBandBox();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.CQ = new DevExpress.XtraReports.Parameters.Parameter();
            this.label6 = new DevExpress.XtraReports.UI.XRLabel();
            this.label5 = new DevExpress.XtraReports.UI.XRLabel();
            this.label3 = new DevExpress.XtraReports.UI.XRLabel();
            this.txtDenNgay = new DevExpress.XtraReports.UI.XRLabel();
            this.TuNgay = new DevExpress.XtraReports.Parameters.Parameter();
            this.txtTuNgay = new DevExpress.XtraReports.UI.XRLabel();
            this.DenNgay = new DevExpress.XtraReports.Parameters.Parameter();
            this.label2 = new DevExpress.XtraReports.UI.XRLabel();
            this.label1 = new DevExpress.XtraReports.UI.XRLabel();
            this.txtTenSo = new DevExpress.XtraReports.UI.XRLabel();
            this.TenSo = new DevExpress.XtraReports.Parameters.Parameter();
            this.txtCQ = new DevExpress.XtraReports.UI.XRLabel();
            this.txtCQCQ = new DevExpress.XtraReports.UI.XRLabel();
            this.CQCQ = new DevExpress.XtraReports.Parameters.Parameter();
            this.crossBandBox1 = new DevExpress.XtraReports.UI.XRCrossBandBox();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // label4
            // 
            this.label4.LocationFloat = new DevExpress.Utils.PointFloat(21.28398F, 830.8286F);
            this.label4.Name = "label4";
            this.label4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label4.SizeF = new System.Drawing.SizeF(131.9445F, 23F);
            this.label4.StylePriority.UseTextAlignment = false;
            this.label4.Text = "- Bắt đầu sử dụng ngày";
            this.label4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // KPTH
            // 
            this.KPTH.Description = "KPTH";
            this.KPTH.Name = "KPTH";
            // 
            // crossBandBox2
            // 
            this.crossBandBox2.BorderWidth = 2F;
            this.crossBandBox2.EndBand = this.Detail;
            this.crossBandBox2.EndPointFloat = new DevExpress.Utils.PointFloat(13.77529F, 969.522F);
            this.crossBandBox2.LocationFloat = new DevExpress.Utils.PointFloat(13.77529F, 12.4901F);
            this.crossBandBox2.Name = "crossBandBox2";
            this.crossBandBox2.StartBand = this.Detail;
            this.crossBandBox2.StartPointFloat = new DevExpress.Utils.PointFloat(13.77529F, 12.4901F);
            this.crossBandBox2.WidthF = 601.8699F;
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.label6,
            this.label5,
            this.label4,
            this.label3,
            this.txtDenNgay,
            this.txtTuNgay,
            this.label2,
            this.label1,
            this.txtTenSo,
            this.txtCQ,
            this.txtCQCQ});
            this.Detail.HeightF = 980.6553F;
            this.Detail.Name = "Detail";
            this.Detail.StylePriority.UseTextAlignment = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.CQ, "Text", "")});
            this.xrLabel1.Font = new DevExpress.Drawing.DXFont("Times New Roman", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(151.9384F, 52.08551F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(325.3225F, 31.65847F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Bệnh viện ...";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // CQ
            // 
            this.CQ.Description = "CQ";
            this.CQ.Name = "CQ";
            // 
            // label6
            // 
            this.label6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.KPTH, "Text", "")});
            this.label6.Font = new DevExpress.Drawing.DXFont("Times New Roman", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.label6.LocationFloat = new DevExpress.Utils.PointFloat(134.6501F, 432.4437F);
            this.label6.Name = "label6";
            this.label6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label6.SizeF = new System.Drawing.SizeF(362.4026F, 23F);
            this.label6.StylePriority.UseFont = false;
            this.label6.StylePriority.UseTextAlignment = false;
            this.label6.Text = "Khoa";
            this.label6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.LocationFloat = new DevExpress.Utils.PointFloat(21.28398F, 853.8286F);
            this.label5.Name = "label5";
            this.label5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label5.SizeF = new System.Drawing.SizeF(151.1364F, 23.00006F);
            this.label5.StylePriority.UseTextAlignment = false;
            this.label5.Text = "- Hết sổ, nộp lưu trữ ngày";
            this.label5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new DevExpress.Drawing.DXFont("Times New Roman", 12F);
            this.label3.LocationFloat = new DevExpress.Utils.PointFloat(496.2576F, 29.0855F);
            this.label3.Name = "label3";
            this.label3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label3.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.label3.StylePriority.UseFont = false;
            this.label3.Text = "MS:.............";
            // 
            // txtDenNgay
            // 
            this.txtDenNgay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.TuNgay, "Text", "")});
            this.txtDenNgay.LocationFloat = new DevExpress.Utils.PointFloat(153.2285F, 830.8286F);
            this.txtDenNgay.Name = "txtDenNgay";
            this.txtDenNgay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtDenNgay.SizeF = new System.Drawing.SizeF(167.8408F, 22.99988F);
            this.txtDenNgay.StylePriority.UseTextAlignment = false;
            this.txtDenNgay.Text = "Đến ngày";
            this.txtDenNgay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // TuNgay
            // 
            this.TuNgay.Description = "TuNgay";
            this.TuNgay.Name = "TuNgay";
            // 
            // txtTuNgay
            // 
            this.txtTuNgay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.DenNgay, "Text", "")});
            this.txtTuNgay.LocationFloat = new DevExpress.Utils.PointFloat(172.4204F, 853.8287F);
            this.txtTuNgay.Name = "txtTuNgay";
            this.txtTuNgay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtTuNgay.SizeF = new System.Drawing.SizeF(167.8406F, 23F);
            this.txtTuNgay.StylePriority.UseTextAlignment = false;
            this.txtTuNgay.Text = "Từ ngày";
            this.txtTuNgay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // DenNgay
            // 
            this.DenNgay.Description = "DenNgay";
            this.DenNgay.Name = "DenNgay";
            // 
            // label2
            // 
            this.label2.Font = new DevExpress.Drawing.DXFont("Times New Roman", 10F, ((DevExpress.Drawing.DXFontStyle)((DevExpress.Drawing.DXFontStyle.Bold | DevExpress.Drawing.DXFontStyle.Italic))));
            this.label2.LocationFloat = new DevExpress.Utils.PointFloat(21.28398F, 667.6248F);
            this.label2.Multiline = true;
            this.label2.Name = "label2";
            this.label2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.label2.SizeF = new System.Drawing.SizeF(492.1157F, 59.51465F);
            this.label2.StylePriority.UseFont = false;
            this.label2.StylePriority.UsePadding = false;
            this.label2.StylePriority.UseTextAlignment = false;
            this.label2.Text = "- In khổ A3 gấp đôi, trang đầu in như trang bìa\r\n- Bên trong, từ trang 2 và 3, cứ" +
    " 2 trang một, in biểu nội dung ở trang sau, kẻ dòng\r\n- Ngày, tháng ghi giữa tran" +
    "g, hết ngày kẻ ngang ghi tiếp";
            this.label2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // label1
            // 
            this.label1.Font = new DevExpress.Drawing.DXFont("Times New Roman", 9.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.label1.LocationFloat = new DevExpress.Utils.PointFloat(21.28398F, 641.7266F);
            this.label1.Multiline = true;
            this.label1.Name = "label1";
            this.label1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label1.SizeF = new System.Drawing.SizeF(151.1364F, 25.89813F);
            this.label1.StylePriority.UseFont = false;
            this.label1.StylePriority.UseTextAlignment = false;
            this.label1.Text = "Hướng dẫn:";
            this.label1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtTenSo
            // 
            this.txtTenSo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.TenSo, "Text", "")});
            this.txtTenSo.Font = new DevExpress.Drawing.DXFont("Times New Roman", 22F, DevExpress.Drawing.DXFontStyle.Bold);
            this.txtTenSo.LocationFloat = new DevExpress.Utils.PointFloat(33.20877F, 312.5552F);
            this.txtTenSo.Name = "txtTenSo";
            this.txtTenSo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtTenSo.SizeF = new System.Drawing.SizeF(563.0488F, 51.50873F);
            this.txtTenSo.StylePriority.UseFont = false;
            this.txtTenSo.Text = "txtTenSo";
            // 
            // TenSo
            // 
            this.TenSo.Description = "TenSo";
            this.TenSo.Name = "TenSo";
            // 
            // txtCQ
            // 
            this.txtCQ.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.CQ, "Text", "")});
            this.txtCQ.Font = new DevExpress.Drawing.DXFont("Times New Roman", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.txtCQ.LocationFloat = new DevExpress.Utils.PointFloat(134.6501F, 402.9782F);
            this.txtCQ.Name = "txtCQ";
            this.txtCQ.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtCQ.SizeF = new System.Drawing.SizeF(362.4026F, 29.46548F);
            this.txtCQ.StylePriority.UseFont = false;
            this.txtCQ.StylePriority.UseTextAlignment = false;
            this.txtCQ.Text = "Bệnh viện ...";
            this.txtCQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtCQCQ
            // 
            this.txtCQCQ.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.CQCQ, "Text", "")});
            this.txtCQCQ.Font = new DevExpress.Drawing.DXFont("Times New Roman", 12F, DevExpress.Drawing.DXFontStyle.Bold);
            this.txtCQCQ.LocationFloat = new DevExpress.Utils.PointFloat(151.9384F, 29.08551F);
            this.txtCQCQ.Name = "txtCQCQ";
            this.txtCQCQ.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtCQCQ.SizeF = new System.Drawing.SizeF(325.3225F, 23F);
            this.txtCQCQ.StylePriority.UseFont = false;
            this.txtCQCQ.Text = "Sở y tế";
            // 
            // CQCQ
            // 
            this.CQCQ.Description = "CQCQ";
            this.CQCQ.Name = "CQCQ";
            // 
            // crossBandBox1
            // 
            this.crossBandBox1.BorderWidth = 2F;
            this.crossBandBox1.EndBand = this.Detail;
            this.crossBandBox1.EndPointFloat = new DevExpress.Utils.PointFloat(6.367303F, 976.1833F);
            this.crossBandBox1.LocationFloat = new DevExpress.Utils.PointFloat(6.367303F, 7.986071F);
            this.crossBandBox1.Name = "crossBandBox1";
            this.crossBandBox1.StartBand = this.Detail;
            this.crossBandBox1.StartPointFloat = new DevExpress.Utils.PointFloat(6.367303F, 7.986071F);
            this.crossBandBox1.WidthF = 616.7865F;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 90.33208F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 94.48047F;
            this.TopMargin.Name = "TopMargin";
            // 
            // Rep_bia_SoTHCLS
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.Detail,
            this.BottomMargin});
            this.CrossBandControls.AddRange(new DevExpress.XtraReports.UI.XRCrossBandControl[] {
            this.crossBandBox2,
            this.crossBandBox1});
            this.Margins = new DevExpress.Drawing.DXMargins(100, 100, 94, 90);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.CQCQ,
            this.TenSo,
            this.CQ,
            this.KPTH,
            this.TuNgay,
            this.DenNgay});
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
        private DevExpress.XtraReports.UI.XRLabel label4;
        private DevExpress.XtraReports.UI.XRCrossBandBox crossBandBox2;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel label6;
        private DevExpress.XtraReports.UI.XRLabel label5;
        private DevExpress.XtraReports.UI.XRLabel label3;
        private DevExpress.XtraReports.UI.XRLabel txtDenNgay;
        private DevExpress.XtraReports.UI.XRLabel txtTuNgay;
        private DevExpress.XtraReports.UI.XRLabel label2;
        private DevExpress.XtraReports.UI.XRLabel label1;
        private DevExpress.XtraReports.UI.XRLabel txtTenSo;
        private DevExpress.XtraReports.UI.XRLabel txtCQ;
        private DevExpress.XtraReports.UI.XRLabel txtCQCQ;
        private DevExpress.XtraReports.UI.XRCrossBandBox crossBandBox1;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        public DevExpress.XtraReports.Parameters.Parameter KPTH;
        public DevExpress.XtraReports.Parameters.Parameter TuNgay;
        public DevExpress.XtraReports.Parameters.Parameter DenNgay;
        public DevExpress.XtraReports.Parameters.Parameter TenSo;
        public DevExpress.XtraReports.Parameters.Parameter CQ;
        public DevExpress.XtraReports.Parameters.Parameter CQCQ;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
    }
}
