namespace QLBV.FormThamSo
{
    partial class Frm_BC_BenhNhanNoiTruBH
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BC_BenhNhanNoiTruBH));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.BtnReport = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.chkBHYT = new DevExpress.XtraEditors.CheckEdit();
            this.chkDichvu = new DevExpress.XtraEditors.CheckEdit();
            this.txtTungay = new System.Windows.Forms.DateTimePicker();
            this.txtDenngay = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.chkBHYT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDichvu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl2.Appearance.Font")));
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // BtnReport
            // 
            this.BtnReport.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("BtnReport.Appearance.Font")));
            this.BtnReport.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.BtnReport, "BtnReport");
            this.BtnReport.Name = "BtnReport";
            this.BtnReport.Click += new System.EventHandler(this.BtnReport_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl3.Appearance.Font")));
            this.labelControl3.LineLocation = DevExpress.XtraEditors.LineLocation.Bottom;
            this.labelControl3.LineVisible = true;
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Name = "labelControl3";
            // 
            // chkBHYT
            // 
            resources.ApplyResources(this.chkBHYT, "chkBHYT");
            this.chkBHYT.Name = "chkBHYT";
            this.chkBHYT.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("chkBHYT.Properties.Appearance.Font")));
            this.chkBHYT.Properties.Appearance.Options.UseFont = true;
            this.chkBHYT.Properties.Caption = resources.GetString("chkBHYT.Properties.Caption");
            // 
            // chkDichvu
            // 
            resources.ApplyResources(this.chkDichvu, "chkDichvu");
            this.chkDichvu.Name = "chkDichvu";
            this.chkDichvu.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("chkDichvu.Properties.Appearance.Font")));
            this.chkDichvu.Properties.Appearance.Options.UseFont = true;
            this.chkDichvu.Properties.Caption = resources.GetString("chkDichvu.Properties.Caption");
            // 
            // txtTungay
            // 
            resources.ApplyResources(this.txtTungay, "txtTungay");
            this.txtTungay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtTungay.Name = "txtTungay";
            this.txtTungay.Value = new System.DateTime(2019, 10, 26, 10, 42, 14, 0);
            // 
            // txtDenngay
            // 
            resources.ApplyResources(this.txtDenngay, "txtDenngay");
            this.txtDenngay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDenngay.Name = "txtDenngay";
            this.txtDenngay.Value = new System.DateTime(2019, 10, 26, 10, 42, 0, 0);
            // 
            // Frm_BC_BenhNhanNoiTruBH
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtDenngay);
            this.Controls.Add(this.txtTungay);
            this.Controls.Add(this.chkDichvu);
            this.Controls.Add(this.chkBHYT);
            this.Controls.Add(this.BtnReport);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BC_BenhNhanNoiTruBH";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.Frm_BC_BenhNhanNoiTruBH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkBHYT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDichvu.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton BtnReport;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit chkBHYT;
        private DevExpress.XtraEditors.CheckEdit chkDichvu;
        private System.Windows.Forms.DateTimePicker txtTungay;
        private System.Windows.Forms.DateTimePicker txtDenngay;
    }
}