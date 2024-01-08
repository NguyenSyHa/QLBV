namespace QLBV.FormThamSo
{
    partial class frm_MessgeBox
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.mmMesshow = new DevExpress.XtraEditors.LabelControl();
            this.ptInfomation = new DevExpress.XtraEditors.PictureEdit();
            this.ptWarning = new DevExpress.XtraEditors.PictureEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptInfomation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptWarning.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.mmMesshow);
            this.panelControl1.Controls.Add(this.ptInfomation);
            this.panelControl1.Controls.Add(this.ptWarning);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(678, 215);
            this.panelControl1.TabIndex = 1;
            // 
            // mmMesshow
            // 
            this.mmMesshow.Appearance.BackColor = System.Drawing.Color.White;
            this.mmMesshow.Appearance.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmMesshow.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.mmMesshow.Appearance.Options.UseBackColor = true;
            this.mmMesshow.Appearance.Options.UseFont = true;
            this.mmMesshow.Appearance.Options.UseForeColor = true;
            this.mmMesshow.Appearance.Options.UseTextOptions = true;
            this.mmMesshow.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.mmMesshow.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.mmMesshow.Location = new System.Drawing.Point(133, 0);
            this.mmMesshow.Name = "mmMesshow";
            this.mmMesshow.Size = new System.Drawing.Size(553, 212);
            this.mmMesshow.TabIndex = 3;
            // 
            // ptInfomation
            // 
            this.ptInfomation.EditValue = global::QLBV.Properties.Resources.apply_16x16;
            this.ptInfomation.Location = new System.Drawing.Point(1, 0);
            this.ptInfomation.Name = "ptInfomation";
            this.ptInfomation.Properties.ReadOnly = true;
            this.ptInfomation.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.ptInfomation.Size = new System.Drawing.Size(131, 215);
            this.ptInfomation.TabIndex = 3;
            // 
            // ptWarning
            // 
            this.ptWarning.EditValue = global::QLBV.Properties.Resources.apply_16x16;
            this.ptWarning.Location = new System.Drawing.Point(0, 0);
            this.ptWarning.Name = "ptWarning";
            this.ptWarning.Properties.ReadOnly = true;
            this.ptWarning.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.ptWarning.Size = new System.Drawing.Size(131, 215);
            this.ptWarning.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.Location = new System.Drawing.Point(599, 221);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "OK";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // frm_MessgeBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 245);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_MessgeBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông báo";
            this.Load += new System.EventHandler(this.frm_MessgeBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptInfomation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptWarning.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PictureEdit ptWarning;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.PictureEdit ptInfomation;
        private DevExpress.XtraEditors.LabelControl mmMesshow;
    }
}