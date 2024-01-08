namespace QLBV.Giaodien
{
    partial class us_AnhNen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(us_AnhNen));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.txtTenCQ = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenCQ.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.txtTenCQ);
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1024, 600);
            this.panelControl1.TabIndex = 0;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Size = new System.Drawing.Size(1024, 600);
            this.pictureEdit1.TabIndex = 0;
            // 
            // txtTenCQ
            // 
            this.txtTenCQ.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTenCQ.EditValue = "CT CP PT GIẢI PHÁP PHẦN MỀM VIỆT";
            this.txtTenCQ.Enabled = false;
            this.txtTenCQ.Location = new System.Drawing.Point(0, 0);
            this.txtTenCQ.Name = "txtTenCQ";
            this.txtTenCQ.Properties.AllowFocused = false;
            this.txtTenCQ.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.txtTenCQ.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenCQ.Properties.Appearance.ForeColor = System.Drawing.Color.DarkRed;
            this.txtTenCQ.Properties.Appearance.Options.UseBackColor = true;
            this.txtTenCQ.Properties.Appearance.Options.UseFont = true;
            this.txtTenCQ.Properties.Appearance.Options.UseForeColor = true;
            this.txtTenCQ.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTenCQ.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTenCQ.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenCQ.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Maroon;
            this.txtTenCQ.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtTenCQ.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtTenCQ.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtTenCQ.Size = new System.Drawing.Size(1024, 44);
            this.txtTenCQ.TabIndex = 2;
            // 
            // us_AnhNen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.panelControl1);
            this.Name = "us_AnhNen";
            this.Size = new System.Drawing.Size(1024, 600);
            this.Load += new System.EventHandler(this.us_AnhNen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenCQ.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.TextEdit txtTenCQ;
    }
}
