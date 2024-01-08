namespace QLBV.ChucNang
{
    partial class Update_MaDV
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
            this.txtMaCu = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtMaMoi = new DevExpress.XtraEditors.TextEdit();
            this.btnDoiMa = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaCu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaMoi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMaCu
            // 
            this.txtMaCu.Location = new System.Drawing.Point(155, 28);
            this.txtMaCu.Name = "txtMaCu";
            this.txtMaCu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaCu.Properties.Appearance.Options.UseFont = true;
            this.txtMaCu.Properties.ReadOnly = true;
            this.txtMaCu.Size = new System.Drawing.Size(160, 26);
            this.txtMaCu.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(60, 31);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 19);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Mã DV cũ:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(60, 63);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(85, 19);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Mã DV mới:";
            // 
            // txtMaMoi
            // 
            this.txtMaMoi.Location = new System.Drawing.Point(155, 60);
            this.txtMaMoi.Name = "txtMaMoi";
            this.txtMaMoi.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaMoi.Properties.Appearance.Options.UseFont = true;
            this.txtMaMoi.Size = new System.Drawing.Size(160, 26);
            this.txtMaMoi.TabIndex = 2;
            // 
            // btnDoiMa
            // 
            this.btnDoiMa.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnDoiMa.Appearance.Options.UseFont = true;
            this.btnDoiMa.Location = new System.Drawing.Point(240, 95);
            this.btnDoiMa.Name = "btnDoiMa";
            this.btnDoiMa.Size = new System.Drawing.Size(75, 23);
            this.btnDoiMa.TabIndex = 4;
            this.btnDoiMa.Text = "Đổi mã";
            this.btnDoiMa.Click += new System.EventHandler(this.btnDoiMa_Click);
            // 
            // Update_MaDV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 130);
            this.Controls.Add(this.btnDoiMa);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtMaMoi);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtMaCu);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Update_MaDV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update_MaDV";
            this.Load += new System.EventHandler(this.Update_MaDV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMaCu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaMoi.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtMaCu;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtMaMoi;
        private DevExpress.XtraEditors.SimpleButton btnDoiMa;
    }
}