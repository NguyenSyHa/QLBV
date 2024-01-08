namespace QLBV.ChucNang
{
    partial class frm_LuuSerialThietBi
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
            this.txtSerial = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btCapNhat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerial.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSerial
            // 
            this.txtSerial.Location = new System.Drawing.Point(70, 30);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtSerial.Properties.Appearance.Options.UseFont = true;
            this.txtSerial.Size = new System.Drawing.Size(257, 20);
            this.txtSerial.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(14, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(35, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Serial:";
            // 
            // btCapNhat
            // 
            this.btCapNhat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btCapNhat.Appearance.Options.UseFont = true;
            this.btCapNhat.Location = new System.Drawing.Point(70, 72);
            this.btCapNhat.Name = "btCapNhat";
            this.btCapNhat.Size = new System.Drawing.Size(257, 23);
            this.btCapNhat.TabIndex = 2;
            this.btCapNhat.Text = "Cập nhật";
            this.btCapNhat.Click += new System.EventHandler(this.btCapNhat_Click);
            // 
            // frm_LuuSerialThietBi
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 115);
            this.Controls.Add(this.btCapNhat);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtSerial);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_LuuSerialThietBi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật serial thiết bị ngoại vi";
            this.Load += new System.EventHandler(this.frm_LuuSerialThietBi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSerial.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtSerial;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btCapNhat;
    }
}